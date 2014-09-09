using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using SourceSafeTypeLib;
using Vss3WayMerge.VJP;
using vsslib;

namespace Vss3WayMerge.Core
{
	class ScanForBaseline
	{
		readonly VSSDatabase _vss;

		public ScanForBaseline(VSSDatabase vss)
		{
			_vss = vss;
		}

		class ItemHistoryEntry
		{
			public int VssVersion;
			public DateTime Modified;
			public VssChangeType Action;

			public ItemHistoryEntry()
			{
			}

			public ItemHistoryEntry(string s)
			{
				var arr = s.Split('\t');
				VssVersion = Int32.Parse(arr[0]);
				Action = (VssChangeType)Enum.Parse(typeof(VssChangeType), arr[1]);
				Modified = new DateTime(long.Parse(arr[2]), DateTimeKind.Utc);
			}

			public override string ToString()
			{
				return string.Format("{0}	{1}	{2}", VssVersion, Action, Modified.Ticks);
			}
		}

		class Item
		{
			public string Spec;
			public List<ItemHistoryEntry> History;

			public Item()
			{
			}

			public Item(string spec, string serialized)
			{
				Spec = spec;
				History = serialized
					.Split('\n')
					.Select(l => l.Trim())
					.Where(l => l != "")
					.Select(l => new ItemHistoryEntry(l))
					.ToList()
				;
			}

			public override string ToString()
			{
				var sb = new StringBuilder();

				History.ForEach(e => sb.AppendLine(e.ToString()));

				return sb.ToString();
			}
		}

		VssFileCache _cache;

		public List<string> Scan(string project, DateTime baseTime)
		{
			var ret = new List<string>();
			
			var vssItem = _vss.VSSItem[project];
			using (_cache = new VssFileCache(".rev-cache", _vss.SrcSafeIni))
			{
				ScanForHistory(vssItem, ret);
			}

			return null;
		}

		void ScanForHistory(IVSSItem vssItem, List<string> ret)
		{
			var spec = vssItem.Spec;
			if (vssItem.Type == 0)
			{
				foreach (IVSSItem child in vssItem.Items)
				{
					ScanForHistory(child, ret);
				}
			}
			else
			{
				Item item;
				var cachedData = _cache.GetFilePath(vssItem.Spec, vssItem.VersionNumber, vssItem.VSSVersion.Date.Ticks);
				if (cachedData != null)
				{
					item = new Item(spec, File.ReadAllText(cachedData));
				}
				else
				{
					item = new Item {
						Spec = spec,
						History = LoadHistory(vssItem)
					};

					_cache.AddFileContent(vssItem.Spec, vssItem.VersionNumber, vssItem.VSSVersion.Date.Ticks, item.ToString());
				}
			}
		}

		static List<ItemHistoryEntry> LoadHistory(IVSSItem vssItem)
		{
			var ret = new List<ItemHistoryEntry>(vssItem.Versions.Count);

			foreach (IVSSVersion vssVersion in vssItem.Versions)
			{
				var spec = vssItem.Spec;
				var action = vssVersion.Action;

				VssChangeType ct;

				if(action.StartsWith("Checked in "))
					ct = VssChangeType.CheckIn;
				else if (action.StartsWith("Created "))
					ct = VssChangeType.Created;
				else if (action.StartsWith("Labeled "))
					ct = VssChangeType.Labeled;
				else if (action.StartsWith("Branched at version "))
					ct = VssChangeType.Branched;
				else if (action.StartsWith("Archived versions of "))
					ct = VssChangeType.Branched;
				else if (action.StartsWith("Rollback to version "))
					ct = VssChangeType.Branched;
				else
				{
					return null;
				}

				if (ct == VssChangeType.Labeled)
					continue;

				var histEnt = new ItemHistoryEntry {
					Action = ct,
					Modified = vssVersion.Date,
					VssVersion = vssVersion.VersionNumber
				};
				
				ret.Add(histEnt);
			}
			return ret;
		}
	}
}
