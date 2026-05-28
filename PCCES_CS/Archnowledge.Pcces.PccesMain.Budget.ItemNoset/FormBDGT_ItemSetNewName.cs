using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.Budget.ItemNoset;

public class FormBDGT_ItemSetNewName : Form
{
	private UltraLabel ultraLabel1;

	private UltraTextEditor txtKind;

	private UltraButton A1_Btn_Cncl;

	private UltraButton A1_Btn_Next;

	private Container components = null;

	public FormBDGT_ItemSetNewName()
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
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.txtKind = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.A1_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.A1_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		((System.ComponentModel.ISupportInitialize)this.txtKind).BeginInit();
		base.SuspendLayout();
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 9f);
		this.ultraLabel1.Location = new System.Drawing.Point(8, 8);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(312, 23);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "請輸入欲新增的編號名稱";
		this.txtKind.Location = new System.Drawing.Point(23, 32);
		this.txtKind.Name = "txtKind";
		this.txtKind.Size = new System.Drawing.Size(321, 21);
		this.txtKind.TabIndex = 1;
		this.txtKind.Validating += new System.ComponentModel.CancelEventHandler(txtKind_Validating);
		this.A1_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A1_Btn_Cncl.Appearance = appearance1;
		this.A1_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A1_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A1_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A1_Btn_Cncl.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.A1_Btn_Cncl.Location = new System.Drawing.Point(256, 72);
		this.A1_Btn_Cncl.Name = "A1_Btn_Cncl";
		this.A1_Btn_Cncl.ShowFocusRect = false;
		this.A1_Btn_Cncl.ShowOutline = false;
		this.A1_Btn_Cncl.Size = new System.Drawing.Size(88, 28);
		this.A1_Btn_Cncl.SupportThemes = false;
		this.A1_Btn_Cncl.TabIndex = 4;
		this.A1_Btn_Cncl.Text = "取消";
		this.A1_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A1_Btn_Next.Appearance = appearance2;
		this.A1_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.A1_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A1_Btn_Next.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.A1_Btn_Next.Location = new System.Drawing.Point(164, 72);
		this.A1_Btn_Next.Name = "A1_Btn_Next";
		this.A1_Btn_Next.ShowFocusRect = false;
		this.A1_Btn_Next.ShowOutline = false;
		this.A1_Btn_Next.Size = new System.Drawing.Size(88, 28);
		this.A1_Btn_Next.SupportThemes = false;
		this.A1_Btn_Next.TabIndex = 3;
		this.A1_Btn_Next.Text = "確定";
		this.A1_Btn_Next.Click += new System.EventHandler(A1_Btn_Next_Click);
		base.AcceptButton = this.A1_Btn_Next;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.ClientSize = new System.Drawing.Size(356, 109);
		base.Controls.Add(this.A1_Btn_Cncl);
		base.Controls.Add(this.A1_Btn_Next);
		base.Controls.Add(this.txtKind);
		base.Controls.Add(this.ultraLabel1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Name = "FormBDGT_ItemSetNewName";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "新增編號名稱";
		base.Load += new System.EventHandler(FormBDGT_ItemSetNewName_Load);
		((System.ComponentModel.ISupportInitialize)this.txtKind).EndInit();
		base.ResumeLayout(false);
	}

	private void A1_Btn_Next_Click(object sender, EventArgs e)
	{
		DBClass DBCLS = new DBClass();
		int iResult = DBCLS.SaveItemName(txtKind.Text.Trim());
		if (iResult < 0)
		{
			MessageBox.Show(this, "已有相同名稱資料存在", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtKind.Focus();
		}
		else
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}
	}

	private void FormBDGT_ItemSetNewName_Load(object sender, EventArgs e)
	{
		txtKind.Focus();
	}

	private void txtKind_Validating(object sender, CancelEventArgs e)
	{
		if (!CommonMethods.CheckValidString((sender as UltraTextEditor).Text))
		{
			e.Cancel = true;
		}
	}
}
