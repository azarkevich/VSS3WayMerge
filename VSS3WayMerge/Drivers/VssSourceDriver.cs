using System;
using System.IO;
using SourceSafeTypeLib;

namespace Vss3WayMerge.Drivers
{
	class VssSourceDriver : IMergeDriver
	{
		readonly IVSSDatabase _db;
		readonly bool _theirsSide;

		public VssSourceDriver(IVSSDatabase db, bool theirsSide)
		{
			_db = db;
			_theirsSide = theirsSide;
		}

		string GetSpec(VssChangeAtom ca)
		{
			return _theirsSide ? ca.Spec : ca.MineSpecSafe;
		}

		int GetHeadVersion(VssChangeAtom ca)
		{
			return _theirsSide ? ca.TheirsHead : ca.MineHead;
		}

		void SetHeadVersion(VssChangeAtom ca, int head)
		{
			if (_theirsSide)
				ca.TheirsHead = head;
			else
				ca.MineHead = head;
		}

		public void InitHead(VssChangeAtom ca)
		{
			if (GetHeadVersion(ca) == 0)
				SetHeadVersion(ca, _db.VSSItem[GetSpec(ca)].VersionNumber);

			ca.TheirsPath = LoadContent(ca, _theirsSide ? "theirs": "mine", ca.TheirsHead);
		}

		public void InitBase(VssChangeAtom ca)
		{
			ca.TheirsBasePath = LoadContent(ca, _theirsSide ? "base.of.theirs" : "base.of.mine", ca.BaseVersion);
		}

		string LoadContent(VssChangeAtom ca, string infix, int version = -1)
		{
			var spec = GetSpec(ca);

			var item = _db.VSSItem[spec];

			if (version != -1)
			{
				if (item.VersionNumber != version)
				{
					if (item.VersionNumber < version)
						throw new ApplicationException(string.Format("Verson {0} as '{1}' absent. Head version: {2}", version, infix, item.VersionNumber));

					item = item.Version[version];
				}
			}

			var localSpec = Path.Combine(ca.TempDir, Path.GetFileNameWithoutExtension(spec) + "." + infix + Path.GetExtension(spec));

			item.Get(localSpec, (int)VSSFlags.VSSFLAG_FORCEDIRNO | (int)VSSFlags.VSSFLAG_USERRONO | (int)VSSFlags.VSSFLAG_REPREPLACE);

			File.SetAttributes(localSpec, FileAttributes.ReadOnly);

			return localSpec;
		}

		public void Dispose()
		{
			_db.Close();
		}
	}
}
