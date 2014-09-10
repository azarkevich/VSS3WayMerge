using System;
using System.Diagnostics;
using System.IO;

namespace Vss3WayMerge
{
	public class TempManager
	{
		public void PrepareTemp(string rootTempDir)
		{
			var thisProcessId = Process.GetCurrentProcess().Id;

			// silently clear previous temp files
			foreach (var fse in Directory.EnumerateFileSystemEntries(rootTempDir))
			{
				try
				{
					if (File.Exists(fse))
					{
						File.SetAttributes(fse, FileAttributes.Normal);
						File.Delete(fse);
					}
					else if (Directory.Exists(fse))
					{
						var dirName = Path.GetFileName(fse);
						int id;
						if (Int32.TryParse(dirName, out id) && id != thisProcessId)
						{
							// do not delete temp of already running instances
							try
							{
								var p = Process.GetProcessById(id);
								if (!p.HasExited)
									continue;
							}
							catch
							{
							}
						}

						foreach (var subfse in Directory.EnumerateFileSystemEntries(fse, "*.*", SearchOption.AllDirectories))
						{
							if (File.Exists(subfse))
								File.SetAttributes(subfse, FileAttributes.Normal);
						}

						Directory.Delete(fse, true);
					}
				}
				catch
				{
				}
			}
		}
	}
}