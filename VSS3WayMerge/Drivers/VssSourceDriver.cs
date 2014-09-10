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

		public void InitHead(VssChangeAtom ca)
		{
			if (_theirsSide)
			{
				if (ca.TheirsHead == 0)
					ca.TheirsHead = _db.VSSItem[ca.Spec].VersionNumber;

				ca.TheirsPath = LoadContent(ca, "theirs", ca.TheirsHead);
			}
			else
			{
				if (ca.MineHead == 0)
					ca.MineHead = _db.VSSItem[ca.Spec].VersionNumber;

				ca.MinePath = LoadContent(ca, "mine", ca.MineHead);
			}
		}

		public void InitBase(VssChangeAtom ca)
		{
			if (_theirsSide)
			{
				ca.TheirsBasePath = LoadContent(ca, "base.of.theirs", ca.BaseVersion);
			}
			else
			{
				ca.MineBasePath = LoadContent(ca, "base.of.mine", ca.BaseVersion);
			}
		}

		string LoadContent(VssChangeAtom ca, string infix, int version = -1)
		{
			var item = _db.VSSItem[ca.Spec];

			if (version != -1)
			{
				if (item.VersionNumber != version)
				{
					if (item.VersionNumber < version)
						throw new ApplicationException(string.Format("Verson {0} as '{1}' absent. Head version: {2}", version, infix, item.VersionNumber));

					item = item.Version[version];
				}
			}

			var localSpec = Path.Combine(ca.TempDir, Path.GetFileNameWithoutExtension(ca.Spec) + "." + infix + Path.GetExtension(ca.Spec));

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
