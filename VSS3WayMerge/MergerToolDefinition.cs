namespace Vss3WayMerge
{
	class MergerToolDefinition
	{
		public string Name;
		public string Exe;
		public string Diff;
		public string Merge;

		public override string ToString()
		{
			return Name;
		}
	}
}
