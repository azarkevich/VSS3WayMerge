using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reactive.Disposables;

namespace Vss3WayMerge.Tools
{
	public partial class CancellableWait : Form
	{
		public static Task<T> Wait<T>(Task<T> task, IWin32Window parent, string waitFor, CancellationTokenSource ct, bool modalDialog)
		{
			if(task.Status == TaskStatus.RanToCompletion || task.Status == TaskStatus.Faulted || task.Status == TaskStatus.Canceled)
				return task;

			var dlg = new CancellableWait(waitFor);

			var close = new SingleAssignmentDisposable();

			task.ContinueWith(t => close.Dispose(), TaskScheduler.FromCurrentSynchronizationContext());

			close.Disposable = Disposable.Create(dlg.Close);

			dlg.Closed += (_, __) => {
				if (dlg.DialogResult == DialogResult.Abort)
				{
					ct.Cancel();
				}
			};

			if (modalDialog)
				dlg.ShowDialog(parent);
			else
				dlg.Show(parent);

			return task;
		}

		public CancellableWait(string waitFor)
		{
			InitializeComponent();

			if(Parent == null)
				StartPosition = FormStartPosition.CenterScreen;

			labelWaitFor.Text = waitFor;
		}
	}
}
