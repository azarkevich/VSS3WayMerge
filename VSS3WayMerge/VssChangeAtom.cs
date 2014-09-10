namespace Vss3WayMerge
{
	class VssChangeAtom
	{
		public string Spec;
		public string MineSpec;
		public string MineSpecSafe
		{
			get
			{
				return MineSpec ?? Spec;
			}
		}

		public bool BasesDiffer;
		public int BaseVersion;
		public string MineBasePath;
		public string TheirsBasePath;

		public int MineHead;
		public int TheirsHead;

		public string TempDir;

		public string MinePath;
		public string TheirsPath;
		public string MergedPath;

		public Status Status = Status.Nothing;
		public string StatusDetails;

		public string OriginalLine { get; set; }

		public bool IsMerged
		{
			get
			{
				return Status == Status.Merged || Status == Status.MergedNoChanges || Status == Status.ResolvedAsMine || Status == Status.ResolvedAsTheirs;
			}
		}
	}
}
