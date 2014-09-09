using System;
using System.Collections.Generic;
using System.IO;
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

			// offset to content
			public readonly long Offset;

			// content size
			public readonly int Size;

			public CacheEntry(string key, string notes, long offset, int size)
			{
				Key = key;
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
				if (arr.Length != 4)
				{
					Console.Error.WriteLine("Bad cache line: " + line);
					Environment.Exit(-1);
				}

				for (var i = 0; i < 2; i++)
				{
					if(arr[i] == "")
						arr[i] = null;
				}

				var ce = new CacheEntry(arr[0], arr[1], long.Parse(arr[2]), int.Parse(arr[3]));

				_cacheIndex[ce.Key] = ce;
			}

			_packWrite = new FileStream(Path.Combine(baseDir, "data"), FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
			_packWrite.Seek(0, SeekOrigin.End);

			_packRead = new FileStream(Path.Combine(baseDir, "data"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		}

		Stream _packWrite;
		Stream _packRead;

		void WriteIndexEntry(CacheEntry ce)
		{
			_indexWriter.WriteLine("{0}	{1}	{2}	{3}", ce.Key, ce.Notes, ce.Offset, ce.Size);
			_indexWriter.Flush();
		}

		public void AddChunk(string key, byte[] data)
		{
			lock (this)
			{
				var ce = new CacheEntry(key, null, _packWrite.Position, data.Length);
				
				_packWrite.Write(data, 0, data.Length);
				_packWrite.Flush();

				_cacheIndex[ce.Key] = ce;

				WriteIndexEntry(ce);
			}
		}

		public byte[] GetChunk(string key)
		{
			lock (this)
			{
				CacheEntry ce;
				if (!_cacheIndex.TryGetValue(key, out ce))
					return null;

				var buff = new byte[ce.Size];
				_packRead.Seek(ce.Offset, SeekOrigin.Begin);
				_packRead.Read(buff, 0, ce.Size);

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
