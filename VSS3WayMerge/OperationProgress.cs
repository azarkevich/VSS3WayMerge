using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vss3WayMerge
{
	public partial class OperationProgress : Form
	{
		Func<string> _getCurrentItemFunc;
		public OperationProgress(Func<string> getCurrentItemFunc)
		{
			_getCurrentItemFunc = getCurrentItemFunc;

			InitializeComponent();

			SetCurrentItem();
		}

		private void SetCurrentItem()
		{
			labelCurrentItem.Text = _getCurrentItemFunc();
		}

		void timerUpdateCurrentItem_Tick(object sender, EventArgs e)
		{
			SetCurrentItem();
		}
	}
}
