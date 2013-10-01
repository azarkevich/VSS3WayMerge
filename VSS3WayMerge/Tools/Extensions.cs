using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vss3WayMerge.Tools
{
	public static class Extensions
	{
		public static Task<T> ShowDialog<T>(this Task<T> task, IWin32Window parent, string waitFor, CancellationTokenSource ct)
		{
			return CancellableWait.Wait(task, parent, waitFor, ct, true);
		}

		public static Task ShowDialog(this Task task, IWin32Window parent, string waitFor, CancellationTokenSource ct)
		{
			var typedTask = task.ContinueWith(_ => new object(), TaskContinuationOptions.ExecuteSynchronously);
			return CancellableWait.Wait(typedTask, parent, waitFor, ct, true);
		}
	}
}
