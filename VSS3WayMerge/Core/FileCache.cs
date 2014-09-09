using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace vcslib
{
	public class FileCache
	{
		public class CacheEntry
		{
			public readonly string Key;

			// Some notes
			public readonly string Notes;

			// path to content
			public readonly string PackPath;

			// offset to content
			public readonly long Offset;

			// content size
			public readonly int Size;

			public CacheEntry(string key, string notes, string packPath, long offset, int size)
			{
				Key = key;
				PackPath = packPath;
				Notes = notes;
				Offset = offset;
				Size = size;
			}
		}

		readonly FileStream _indexStream;
		readonly StreamWriter _indexWriter;
		readonly Dictionary<string, CacheEntry> _cacheIndex = new Dictionary<string, CacheEntry>();

		public FileCache(string baseDir)
		{
			if(!Directory.Exists(baseDir))
				Directory.CreateDirectory(baseDir);

			var indexFile = Path.Combine(baseDir, ".index");
			_indexStream = new FileStream(indexFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);

			ReadIndex(baseDir);

			_indexWriter = new StreamWriter(_indexStream, Encoding.UTF8);
		}

		void ReadIndex(string baseDir)
		{
			var reader = new StreamReader(_indexStream, Encoding.UTF8);
			string line;
			while ((line = reader.ReadLine()) != null)
			{
				var arr = line.Split('\t');
				if (arr.Length != 5)
				{
					Console.Error.WriteLine("Bad cache line: " + line);
					Environment.Exit(-1);
				}

				for (var i = 0; i < 3; i++)
				{
					if(arr[i] == "")
						arr[i] = null;
				}

				var ce = new CacheEntry(arr[0], arr[1], arr[2], long.Parse(arr[3]), int.Parse(arr[4]));

				_cacheIndex[ce.Key] = ce;
			}

			var fname = DateTimeOffset.Now.Ticks.ToString(CultureInfo.InvariantCulture);

			_packPath = Path.Combine(baseDir, "" + fname.Last(), fname);
			Directory.CreateDirectory(Path.GetDirectoryName(_packPath));

			_packWrite = new FileStream(_packPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
			_packWrite.Seek(0, SeekOrigin.End);
		}

		string _packPath;
		Stream _packWrite;

		void WriteIndexEntry(CacheEntry ce)
		{
			_indexWriter.WriteLine("{0}	{1}	{2}	{3}	{4}", ce.Key, ce.Notes, ce.PackPath, ce.Offset, ce.Size);
			_indexWriter.Flush();
		}

		public void AddChunk(string key, byte[] data)
		{
			lock (this)
			{
				var ce = new CacheEntry(key, null, _packPath, _packWrite.Position, data.Length);
				
				_packWrite.Write(data, 0, data.Length);
				_packWrite.Flush();

				_cacheIndex[ce.Key] = ce;

				WriteIndexEntry(ce);
			}
		}

		public byte[] GetChunk(string key)
		{
			CacheEntry ce;
			lock (this)
			{
				if (!_cacheIndex.TryGetValue(key, out ce))
					return null;
			}

			using (var fs = new FileStream(ce.PackPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				var buff = new byte[ce.Size];
				fs.Seek(ce.Offset, SeekOrigin.Begin);
				fs.Read(buff, 0, ce.Size);

				return buff;
			}
		}

		public CacheEntry GetFileInfo(string key)
		{
			lock(this)
			{
				CacheEntry ret;

				_cacheIndex.TryGetValue(key, out ret);

				return ret;
			}
		}

		public void Dispose()
		{
			_indexStream.Close();
			_packWrite.Dispose();
		}
	}
}
