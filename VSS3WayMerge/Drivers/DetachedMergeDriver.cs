using System;
using System.IO;

namespace Vss3WayMerge.Drivers
{
	class DetachedMergeDriver : IMergeDriver
	{
		readonly string _baseDir;

		public DetachedMergeDriver(string baseDir)
		{
			_baseDir = baseDir.Replace('/', '\\');
		}

		public void InitHead(VssChangeAtom ca)
		{
			// rel to merge destination root
			var relPath = ca.Spec.TrimStart("$/".ToCharArray()).Replace('/', '\\');
			var path = Path.Combine(_baseDir, relPath);

			// can be only mine
			if (!File.Exists(path))
				throw new ApplicationException("Mine head does not exists: " + path);

			ca.MinePath = path;
		}

		public void InitBase(VssChangeAtom ca)
		{
			// can't get base
		}

		public void Dispose()
		{
		}
	}
}
