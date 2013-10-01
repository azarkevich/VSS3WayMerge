namespace Vss3WayMerge
{
	public enum Status
	{
		Nothing,
		Error,
		Merged,
		Conflicted,
		ResolvedAsMine,
		ResolvedAsTheirs,
		MergedNoChanges,
		Unmergeable,
	}
}
