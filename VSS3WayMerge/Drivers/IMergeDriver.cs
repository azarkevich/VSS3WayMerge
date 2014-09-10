using System;
namespace Vss3WayMerge.Drivers
{
	interface IMergeDriver : IDisposable
	{
		void InitHead(VssChangeAtom ca);
		void InitBase(VssChangeAtom ca);
	}
}
