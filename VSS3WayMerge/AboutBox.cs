using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Vss3WayMerge
{
	sealed partial class AboutBox : Form
	{
		public AboutBox()
		{
			InitializeComponent();

			Text = String.Format("About {0}", AssemblyTitle);
			labelProductName.Text = AssemblyProduct;
			labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
			labelCopyright.Text = AssemblyCopyright;
			textBoxDescription.Text = AssemblyDescription;
		}

		#region Assembly Attribute Accessors

		public string AssemblyTitle
		{
			get
			{
				var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				if (attributes.Length > 0)
				{
					var titleAttribute = (AssemblyTitleAttribute)attributes[0];
					if (titleAttribute.Title != "")
					{
						return titleAttribute.Title;
					}
				}
				return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}

		public string AssemblyVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}

		public string AssemblyDescription
		{
			get
			{
				if (File.Exists("build-info.txt"))
				{
					return "Build info:\r\n" + File.ReadAllText("build-info.txt").Replace("\n", "\r\n");
				}
				return "No build info";
			}
		}

		public string AssemblyProduct
		{
			get
			{
				var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyProductAttribute)attributes[0]).Product;
			}
		}

		public string AssemblyCopyright
		{
			get
			{
				var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
			}
		}

		#endregion

		void okButton_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
