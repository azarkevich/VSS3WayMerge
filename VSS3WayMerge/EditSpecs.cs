using System.Windows.Forms;

namespace Vss3WayMerge
{
	public partial class EditSpecs : Form
	{
		public string Specs
		{
			get
			{
				return textBoxSpecs.Text;
			}
		}

		public EditSpecs(string specs)
		{
			InitializeComponent();

			textBoxSpecs.Text = specs;
		}
	}
}
