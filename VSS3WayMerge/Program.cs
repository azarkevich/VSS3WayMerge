using System;
using System.Windows.Forms;
using Vss3WayMerge.Properties;

namespace Vss3WayMerge
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			if (Settings.Default.FirstRun)
			{
				Settings.Default.Upgrade();
				Settings.Default.FirstRun = false;
				Settings.Default.Save();
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Vss3WayMerge());
		}
	}
}
