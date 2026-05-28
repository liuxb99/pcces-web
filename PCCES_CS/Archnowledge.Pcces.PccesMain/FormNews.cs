using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Archnowledge.Pcces.PccesMain;

public class FormNews : Form
{
	private Container components = null;

	public FormNews()
	{
		InitializeComponent();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(536, 373);
		base.Name = "FormNews";
		this.Text = "最新消息";
	}
}
