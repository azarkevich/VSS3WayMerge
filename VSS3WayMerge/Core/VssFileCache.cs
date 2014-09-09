using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using vcslib;

namespace vsslib
{
	public class VssFileCache : IDisposable
	{
		readonly FileCache _cache;

		readonly string _vssDbPath;

		public VssFileCache(string tempDir, string vssDbPath)
		{
			_vssDbPath = vssDbPath.Replace('/', '\\').Trim().TrimEnd('\\').ToLowerInvariant();
			_cache = new FileCache(tempDir);
		}

		public string GetContent(string spec, int ver, long timestamp)
		{
			var data = _cache.GetChunk(MakeKey(spec, ver, timestamp));

			if (data == null)
				return null;

			return Encoding.UTF8.GetString(data);
		}

		public void AddContent(string spec, int ver, long timestamp, string content)
		{
			_cache.AddChunk(MakeKey(spec, ver, timestamp), Encoding.UTF8.GetBytes(content));
		}

		SHA1Managed _hashAlgo;

		string MakeKey(string spec, int ver, long timeStamp)
		{
			spec = spec.Replace('\\', '/').Trim().TrimEnd('/').ToLowerInvariant();

			var key = _vssDbPath + "#" + spec + "@" + ver + "!" + timeStamp;

			if (_hashAlgo == null)
				_hashAlgo = new SHA1Managed();

			key = Convert.ToBase64String(_hashAlgo.ComputeHash(new MemoryStream(Encoding.UTF8.GetBytes(key))));

			return key;
		}

		public void Dispose()
		{
			_cache.Dispose();
		}
	}
}
