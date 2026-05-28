using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.Budget.BDGT_Component;

public class B_Form : UserControl
{
	private UltraLabel ultraLabel1;

	private Container components = null;

	private PccesFormAction F_ActionName;

	public PccesFormAction _ActionName
	{
		get
		{
			return F_ActionName;
		}
		set
		{
			F_ActionName = value;
		}
	}

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		base.SuspendLayout();
		appearance1.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance1;
		this.ultraLabel1.Location = new System.Drawing.Point(3, 3);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(536, 23);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "單價 =\u3000下層自動累算";
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.Controls.Add(this.ultraLabel1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Name = "B_Form";
		base.Size = new System.Drawing.Size(700, 230);
		base.ResumeLayout(false);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	public B_Form()
	{
		InitializeComponent();
	}
}
