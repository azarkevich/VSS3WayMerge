namespace Vss3WayMerge.Drivers
{
	interface IMergeDestinationDriver
	{
		bool InitDriver();

		// initialize merge destination path
		void Init(VssChangeAtom ca);
		void Reset(VssChangeAtom ca);
		bool EnsureMergeDestination(VssChangeAtom ca);
	}
}
