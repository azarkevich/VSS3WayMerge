namespace Vss3WayMerge
{
	class MergerToolsManager
	{
		public MergerToolDefinition[] GetMergerTools()
		{
			var tsvn = new MergerToolDefinition {
				Name = "TortoiseMerge",
				Exe = @"C:\Program Files\TortoiseSVN\bin\TortoiseMerge.exe",
				Diff = "/base:$LEFT$ /mine:$RIGHT$",
				Diff3 = "/base:$BASE$ /theirs:$LEFT$ /mine:$RIGHT$",
				Merge = "/base:$BASE$ /theirs:$LEFT$ /mine:$RIGHT$ /merged:$MERGED$",
			};

			var p4merge = new MergerToolDefinition {
				Name = "Perforce P4Merge",
				Exe = @"C:\Program Files\Perforce\p4merge.exe",
				Diff = "$LEFT$ $RIGHT$",
				Diff3 = "$BASE$ $LEFT$ $RIGHT$",
				Merge = "$BASE$ $LEFT$ $RIGHT$ $MERGED$",
			};

			var bcmp = new MergerToolDefinition {
				Name = "Beyond Compare 3",
				Exe = @"C:\Program Files (x86)\Beyond Compare 3\BCompare.exe",
				Diff = "$LEFT$ $RIGHT$ \"/lefttitle=$LTITLE$\" \"/righttitle=$RTITLE$\"",
				Diff3 = "$LEFT$ $RIGHT$ $BASE$ \"/lefttitle=$LTITLE$\" \"/righttitle=$RTITLE$\" \"/centertitle=$BTITLE$\"",
				Merge = "$LEFT$ $RIGHT$ $BASE$ $MERGED$ \"/lefttitle=$LTITLE$\" \"/righttitle=$RTITLE$\" \"/centertitle=$BTITLE$\" \"/outputtitle=$MTITLE$\"",
			};

			var tgit = new MergerToolDefinition {
				Name = "TortoiseGit",
				Exe = @"C:\Program Files\TortoiseGit\bin\TortoiseGitMerge.exe",
				Diff = "/base:$LEFT$ /mine:$RIGHT$",
				Diff3 = "/base:$BASE$ /theirs:$LEFT$ /mine:$RIGHT$",
				Merge = "/base:$BASE$ /theirs:$LEFT$ /mine:$RIGHT$ /merged:$MERGED$",
			};

			return new[] { bcmp, tsvn, p4merge, tgit };
		}
	}
}
