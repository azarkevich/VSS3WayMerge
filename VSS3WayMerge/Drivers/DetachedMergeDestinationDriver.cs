using System;
using System.IO;
using Vss3WayMerge.Interfaces;

namespace Vss3WayMerge.Drivers
{
	class DetachedMergeDestinationDriver : IMergeDestinationDriver
	{
		readonly IErrorNotification _errNotify;
		readonly string _mergeDestination;

		public DetachedMergeDestinationDriver(IErrorNotification errNotify, string destinationRoot)
		{
			_errNotify = errNotify;
			_mergeDestination = (destinationRoot ?? "").Replace('/', '\\');
		}

		public bool InitDriver()
		{
			try
			{
				if (!Directory.Exists(_mergeDestination))
					Directory.CreateDirectory(_mergeDestination);

				return true;
			}
			catch (Exception ex)
			{
				_errNotify.NotifyError(ex.Message, "", "Init detached driver");
				return false;
			}
		}

		public void Init(VssChangeAtom ca)
		{
			// rel to merge destination root
			var mp = Path.Combine(_mergeDestination, ca.MineSpecSafe.TrimStart("$/".ToCharArray()).Replace('/', '\\'));

			if(File.Exists(mp))
				ca.MergedPath = mp;
		}

		public void Reset(VssChangeAtom ca)
		{
			ca.Status = Status.Nothing;
			ca.StatusDetails = null;

			if (ca.MergedPath == null)
				return;

			try
			{
				File.Delete(ca.MergedPath);
				ca.MergedPath = null;
			}
			catch (Exception ex)
			{
				_errNotify.NotifyError(ex.Message, ca.MineSpecSafe, "Reset");
				ca.Status = Status.Error;
				ca.StatusDetails = "Reset: " + ex.Message;
			}
		}

		public bool EnsureMergeDestination(VssChangeAtom ca)
		{
			try
			{
				if (ca.MergedPath != null)
					return true;

				// rel to merge destination root
				ca.MergedPath = Path.Combine(_mergeDestination, ca.MineSpecSafe.TrimStart("$/".ToCharArray()).Replace('/', '\\'));
				var dir = Path.GetDirectoryName(ca.MergedPath);
				if (!Directory.Exists(dir))
					Directory.CreateDirectory(dir);

				return true;
			}
			catch (Exception ex)
			{
				_errNotify.NotifyError(ex.Message, ca.MineSpecSafe, "Merge Destination");
				ca.Status = Status.Error;
				ca.StatusDetails = "Merge destination: " + ex.Message;
				return false;
			}
		}
	}
}
