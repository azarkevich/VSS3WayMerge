using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Vss3WayMerge.VJP
{
	// class 'play' node changes and returns list of nodes each with its own changes list
	public class VssJournalPlayer
	{
		public List<List<VssNodeChange>> GroupBySpec(IEnumerable<VssNodeChange> changes)
		{
			return changes
				.GroupBy(c => c.Spec.ToLowerInvariant())
				.Select(g => g.ToList())
				.OrderBy(list => list[0].Spec)
				.ToList()
			;
		}

		public List<VssNodeInfo> Play(IEnumerable<string> journals)
		{
			var allChanges = new List<VssNodeChange>();

			foreach (var j in journals)
			{
				Console.WriteLine("J: {0}", j);

				using(var sr = new StreamReader(j))
				{
					allChanges.AddRange(JournalParser.Parse(sr));
				}
			}

			foreach (var c in allChanges.Where(c => c.Type == VssChangeType.Moved || c.Type == VssChangeType.Renamed).ToArray())
			{
				// emulate 'delete' for moved and renamed items
				allChanges.Add(new VssNodeChange { Spec = c.PrevSpec, Type = VssChangeType.DeletedByMove });
			}

			var grouped = GroupBySpec(allChanges);
			var changes = new List<VssNodeInfo>();

			foreach (var clist in grouped)
			{
				var inf = new VssNodeInfo { Spec = clist[0].Spec };

				foreach (var c in clist)
				{
					// not interesed:
					if (c.Type == VssChangeType.Labeled || c.Type == VssChangeType.Purged)
						continue;

					switch (c.Type)
					{
						case VssChangeType.Archived:
							inf.IsArchived = true;
							break;

						case VssChangeType.Created:
						case VssChangeType.Added:
							inf.IsAdded = true;
							break;

						case VssChangeType.Branched:
							inf.IsBranched = true;
							break;

						case VssChangeType.Rollback:
							inf.IsRolledback = true;
							break;

						case VssChangeType.CheckIn:
							inf.IsModified = true;
							if (!inf.FirstChange.HasValue)
								inf.FirstChange = c.Version;
							inf.LatestVersion = Math.Max(inf.LatestVersion, c.Version);
							break;

						case VssChangeType.Destroyed:
						case VssChangeType.Deleted:
							inf.IsDeleted = true;
							break;

						case VssChangeType.DeletedByMove:
							inf.IsDeleted = true;
							inf.IsDeletedByMove = true;
							break;

						case VssChangeType.Recovered:
							// if file was deleted and then recovered - this actions annigilate
							// if file recovered without delete - it file should be handled manually
							if(inf.IsDeleted)
								inf.IsDeleted = false;
							else
								inf.IsRecovered = true;
							break;

						case VssChangeType.Moved:
							inf.MovedFrom = c.PrevSpec;
							break;

						case VssChangeType.Copy:
							inf.CopiedFrom = c.PrevSpec;
							break;

						case VssChangeType.Renamed:
							inf.MovedFrom = c.PrevSpec;
							break;

						case VssChangeType.Shared:
							inf.SharedFrom = c.PrevSpec;
							break;
					}

					inf.Changes.Add(c);
				}

				if(inf.Changes.Count > 0)
					changes.Add(inf);
			}

			return changes;
		}
	}
}

