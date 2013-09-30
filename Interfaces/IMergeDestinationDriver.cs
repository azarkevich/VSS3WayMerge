namespace Vss3WayMerge.Interfaces
{
	interface IMergeDestinationDriver
	{
		bool InitDriver();

		void Init(VssChangeAtom ca);
		void Reset(VssChangeAtom ca);
		bool EnsureMergeDestination(VssChangeAtom ca);
	}
}
