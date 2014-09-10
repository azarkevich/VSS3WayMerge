using System;
using System.Windows.Forms;
using Vss3WayMerge.Properties;

namespace Vss3WayMerge
{
	public partial class ScanRules : Form
	{
		public ScanRules()
		{
			InitializeComponent();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			Settings.Default.Save();
		}
	}
}
