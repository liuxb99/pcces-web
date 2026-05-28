using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.Budget.BDGT_Component;

public class L_Form : UserControl
{
	private UltraLabel ultraLabel1;

	private Container components = null;

	private UltraTextEditor txtCost;

	private PccesFormAction F_ActionName;

	private string F_UserID;

	private int F_Issue;

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

	public int _Issue
	{
		get
		{
			return F_Issue;
		}
		set
		{
			F_Issue = value;
		}
	}

	public string _UserID
	{
		get
		{
			return F_UserID;
		}
		set
		{
			F_UserID = value;
		}
	}

	public string _txtCost => txtCost.Text.Trim();

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.txtCost = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)this.txtCost).BeginInit();
		base.SuspendLayout();
		appearance1.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance1;
		this.ultraLabel1.Location = new System.Drawing.Point(3, 3);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(68, 23);
		this.ultraLabel1.TabIndex = 1;
		this.ultraLabel1.Text = "單價 =";
		this.txtCost.AutoSize = true;
		this.txtCost.Location = new System.Drawing.Point(64, 4);
		this.txtCost.Name = "txtCost";
		this.txtCost.Size = new System.Drawing.Size(268, 21);
		this.txtCost.TabIndex = 4;
		this.txtCost.Text = "0";
		this.txtCost.Validating += new System.ComponentModel.CancelEventHandler(txtCost_Validating);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.Controls.Add(this.txtCost);
		base.Controls.Add(this.ultraLabel1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Name = "L_Form";
		base.Size = new System.Drawing.Size(700, 230);
		base.Load += new System.EventHandler(L_Form_Load);
		((System.ComponentModel.ISupportInitialize)this.txtCost).EndInit();
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

	public void SetCostInputEnabled(bool Enable)
	{
		txtCost.Enabled = Enable;
	}

	public L_Form()
	{
		InitializeComponent();
	}

	private void L_Form_Load(object sender, EventArgs e)
	{
		txtCost.Text = (base.ParentForm as FormBudgetEditMain).ItemCost.ToString();
	}

	private void txtCost_Validating(object sender, CancelEventArgs e)
	{
		try
		{
			Convert.ToDouble(txtCost.Text.Trim());
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "BDGT_Component.L_Form.cs" + ex.Message);
			MessageBox.Show(this, "金額有誤。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtCost.Focus();
		}
	}
}
