using System.Windows.Forms;

namespace Vss3WayMerge.Interfaces
{
	interface IErrorNotification
	{
		void NotifyError(string message, string spec, string hint);
		DialogResult GetConfirmation(string message, string spec, string hints, MessageBoxButtons requestedButtons, MessageBoxDefaultButton defaultButton);
	}
}
