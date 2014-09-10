using System;
using System.Linq;
using SourceSafeTypeLib;
using Vss3WayMerge.Interfaces;

namespace Vss3WayMerge.Drivers
{
	class VssDriver : IMergeDestinationDriver
	{
		readonly IErrorNotification _errNotify;
		readonly VSSDatabase _vss;
		string _vssUsernameLwr;
		readonly string _machineNameLwr = Environment.MachineName.ToLowerInvariant();

		public VssDriver(IErrorNotification errNotify, VSSDatabase vss)
		{
			_errNotify = errNotify;
			_vss = vss;
		}

		public bool InitDriver()
		{
			try
			{
				if (string.IsNullOrWhiteSpace(_vss.VSSItem["$/"].LocalSpec))
				{
					_errNotify.NotifyError("Working dir doesn't set for 'mine' SourceSafe database.\nPlease set and try again.", "", "Init Merge Destination Driver");
					return false;
				}

				_vssUsernameLwr = _vss.Username.ToLowerInvariant();

				return true;
			}
			catch (Exception ex)
			{
				_errNotify.NotifyError(ex.Message, "", "Init Merge Destination Driver");
				return false;
			}
		}

		public void Init(VssChangeAtom ca)
		{
			try
			{
				var item = _vss.VSSItem[ca.MineSpecSafe];

				if (item.Type == 0 || item.IsCheckedOut == 0)
					return;

				var co = item.Checkouts
					.Cast<VSSCheckout>()
					.FirstOrDefault(c => c.Username.ToLowerInvariant() == _vssUsernameLwr && c.Machine.ToLowerInvariant() == _machineNameLwr)
				;

				if (co == null)
					return;

				ca.MergedPath = item.LocalSpec;
			}
			catch (Exception ex)
			{
				// ignore 'not found'. Sems item absent in mine
				if (ex.Message.Contains("File or project not found"))
					return;

				_errNotify.NotifyError(ex.Message, ca.MineSpecSafe, "Get Checkout Status");
			}
		}

		public void Reset(VssChangeAtom ca)
		{
			ca.Status = Status.Nothing;
			ca.StatusDetails = null;

			if (ca.MergedPath == null)
				return;

			try
			{
				var item = _vss.VSSItem[ca.MineSpecSafe];

				var co = item.Checkouts
					.Cast<VSSCheckout>()
					.FirstOrDefault(c => c.Username.ToLowerInvariant() == _vssUsernameLwr && c.Machine.ToLowerInvariant() == _machineNameLwr)
				;

				if (co != null)
					item.UndoCheckout();

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
			if (ca.MergedPath != null)
				return true;

			try
			{
				var item = _vss.VSSItem[ca.MineSpecSafe];

				// try find already existing checkout
				var co = item.Checkouts
					.Cast<VSSCheckout>()
					.FirstOrDefault(c => c.Username.ToLowerInvariant() == _vssUsernameLwr && c.Machine.ToLowerInvariant() == _machineNameLwr)
				;

				if (co != null)
				{
					ca.MergedPath = co.LocalSpec;
					return true;
				}

				item.Checkout();

				ca.MergedPath = item.LocalSpec;

				return true;
			}
			catch (Exception ex)
			{
				_errNotify.NotifyError(ex.Message, ca.MineSpecSafe, "Checkout merge destination");
				ca.Status = Status.Error;
				ca.StatusDetails = "Checkout Merge Destination: " + ex.Message;
				return false;
			}
		}
	}
}
