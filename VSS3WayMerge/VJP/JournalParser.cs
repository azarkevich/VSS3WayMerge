using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Vss3WayMerge.VJP
{
	public class JournalParser
	{
		public static IEnumerable<VssNodeChange> Parse(TextReader sr)
		{
			// finite state machine
			var state = 0;

			VssNodeChange cur = null;
			string spec = null;
			var version = 0;
			string author = null;
			while (true)
			{
				var line = sr.ReadLine();
				if (line == null)
					break;

				line = line.Trim();

				if (line == "")
				{
					if (cur != null)
						yield return cur;

					cur = null;
					state = 0;
					continue;
				}

				// state 'wait spec'
				if (state == 0)
				{
					Debug.Assert(line.StartsWith("$"));

					spec = line;

					version = 0;
					author = null;

					state = 1;

					continue;
				}

				// state 'wait version, user, action, comment'
				if (state == 1)
				{
					if (line.StartsWith("Version:"))
					{
						version = Int32.Parse(line.Substring("Version:".Length).Trim());
						continue;
					}
					if (line.StartsWith("User:"))
					{
						author = line.Substring("User:".Length).Trim().Split(' ')[0];
						continue;
					}
					if (line.StartsWith("Comment:"))
					{
						Debug.Assert(cur != null);
						cur.Comment = line.Substring("Comment:".Length).Trim();
						state = 2;
						continue;
					}

					// parse action
					Debug.Assert(spec != null);
					string prevSpec;
					var type = ParseAction(line, ref spec, out prevSpec);

					cur = new VssNodeChange
					{
						Type = type,
						Spec = spec,
						PrevSpec = prevSpec,
						Author = author,
						Version = version
					};
				}

				// comment continuation
				if (state == 2)
				{
					cur.Comment = cur.Comment + line.Trim();
					continue;
				}
			}

			// last change if available
			if (cur != null)
				yield return cur;
		}

		struct ActionParser
		{
			public string Token;
			public bool EndsWith;
			public bool StartsWith;
			public VssChangeType Type;
		}

		readonly static ActionParser[] Parsers = new[] {
			new ActionParser { Token = "Labeled", StartsWith = true, Type = VssChangeType.Labeled },
			new ActionParser { Token = "Restored version ", StartsWith = true, Type = VssChangeType.Restored },
			new ActionParser { Token = "Archived version ", StartsWith = true, Type = VssChangeType.Restored },
			
			new ActionParser { Token = " added", EndsWith = true, Type = VssChangeType.Added },
			new ActionParser { Token = " deleted", EndsWith = true, Type = VssChangeType.Deleted },
			new ActionParser { Token = " created", EndsWith = true, Type = VssChangeType.Created },
			new ActionParser { Token = " branched", EndsWith = true, Type = VssChangeType.Branched },
			new ActionParser { Token = " destroyed", EndsWith = true, Type = VssChangeType.Destroyed },
			new ActionParser { Token = " purged", EndsWith = true, Type = VssChangeType.Purged },
			new ActionParser { Token = " recovered", EndsWith = true, Type = VssChangeType.Recovered },
			
			new ActionParser { Token = " moved to ", Type = VssChangeType.Moved },
			new ActionParser { Token = " renamed to ", Type = VssChangeType.Renamed },
			new ActionParser { Token = " shared from ", Type = VssChangeType.Shared },
			new ActionParser { Token = " copied from ", Type = VssChangeType.Copy },
		};

		static VssChangeType ParseAction(string line, ref string specForFix, out string prevSpec)
		{
			prevSpec = null;

			if (line == "Checked in")
				return VssChangeType.CheckIn;

			if (line == "Rolled back")
				return VssChangeType.Rollback;

			if (specForFix == "$/")
				specForFix = "$";

			VssChangeType? type = null;
			for (var i = 0; i < Parsers.Length; i++)
			{
				var p = Parsers[i];

				if (p.EndsWith && line.EndsWith(p.Token))
				{
					Debug.Assert(!type.HasValue);
					type = p.Type;
					specForFix = specForFix + "/" + line.Substring(0, line.Length - p.Token.Length).TrimEnd('/');
				}

				if (p.StartsWith && line.StartsWith(p.Token))
				{
					Debug.Assert(!type.HasValue);
					type = p.Type;
					if (p.Type != VssChangeType.Labeled && p.Type != VssChangeType.Restored && p.Type != VssChangeType.Archived)
					{
						specForFix = specForFix + "/" + line.Substring(0, p.Token.Length).TrimEnd('/');
					}
				}

				if (!p.EndsWith && !p.StartsWith && line.Contains(p.Token))
				{
					var ind = line.IndexOf(p.Token);
					type = p.Type;
					if (p.Type == VssChangeType.Shared || p.Type == VssChangeType.Copy)
					{
						prevSpec = line.Substring(ind + p.Token.Length);
						specForFix = specForFix + "/" + line.Substring(0, ind);
					}
					else if (p.Type == VssChangeType.Renamed)
					{
						prevSpec = specForFix + "/" + line.Substring(0, ind);
						specForFix = specForFix + "/" + line.Substring(ind + p.Token.Length).TrimEnd('/');
					}
					else if (p.Type == VssChangeType.Moved)
					{
						prevSpec = specForFix + "/" + line.Substring(0, ind);
						specForFix = line.Substring(ind + p.Token.Length).TrimEnd('/') + "/" + line.Substring(0, ind);

						// moved with rename. but new name doesn't known
						if (specForFix == prevSpec)
						{
							specForFix += "/?????";
						}
					}
				}
			}

			if (type.HasValue)
				return type.Value;

			Console.WriteLine("Unknown: {0}", line);

			Environment.Exit(-1);

			return VssChangeType.CheckIn;
		}
	}
}
