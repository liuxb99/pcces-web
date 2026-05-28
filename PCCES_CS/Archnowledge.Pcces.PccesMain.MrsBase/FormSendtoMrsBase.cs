using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.DomainModule.MrsBase;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.MrsBase;

public class FormSendtoMrsBase : Form
{
	private Panel panelMiddle;

	private UltraButton btnOK;

	private UltraButton btnCancel;

	private RadioButton rbOverrideExistingItem;

	private Container components = null;

	private RadioButton rbIgnoreExistingItem;

	private UltraLabel lbItemCountText;

	private UltraLabel lbItemCount;

	private UltraLabel lbKindName;

	private UltraLabel lbSendToText;

	private Label lbInstruction;

	private string userID;

	private string title;

	private string KindName;

	private DataSet dsCesPrice;

	public string _UserID
	{
		get
		{
			return userID;
		}
		set
		{
			userID = value;
		}
	}

	public string _titleName
	{
		get
		{
			return title;
		}
		set
		{
			title = value;
		}
	}

	public string _KindName
	{
		get
		{
			return KindName;
		}
		set
		{
			KindName = value;
		}
	}

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		this.panelMiddle = new System.Windows.Forms.Panel();
		this.lbInstruction = new System.Windows.Forms.Label();
		this.lbItemCount = new Infragistics.Win.Misc.UltraLabel();
		this.rbIgnoreExistingItem = new System.Windows.Forms.RadioButton();
		this.lbItemCountText = new Infragistics.Win.Misc.UltraLabel();
		this.rbOverrideExistingItem = new System.Windows.Forms.RadioButton();
		this.btnOK = new Infragistics.Win.Misc.UltraButton();
		this.btnCancel = new Infragistics.Win.Misc.UltraButton();
		this.lbKindName = new Infragistics.Win.Misc.UltraLabel();
		this.lbSendToText = new Infragistics.Win.Misc.UltraLabel();
		this.panelMiddle.SuspendLayout();
		base.SuspendLayout();
		this.panelMiddle.BackColor = System.Drawing.Color.White;
		this.panelMiddle.Controls.Add(this.lbInstruction);
		this.panelMiddle.Controls.Add(this.lbItemCount);
		this.panelMiddle.Controls.Add(this.rbIgnoreExistingItem);
		this.panelMiddle.Controls.Add(this.lbItemCountText);
		this.panelMiddle.Controls.Add(this.rbOverrideExistingItem);
		this.panelMiddle.Location = new System.Drawing.Point(0, 55);
		this.panelMiddle.Name = "panelMiddle";
		this.panelMiddle.Size = new System.Drawing.Size(358, 136);
		this.panelMiddle.TabIndex = 9;
		this.lbInstruction.ForeColor = System.Drawing.Color.Red;
		this.lbInstruction.Location = new System.Drawing.Point(55, 62);
		this.lbInstruction.Name = "lbInstruction";
		this.lbInstruction.Size = new System.Drawing.Size(293, 13);
		this.lbInstruction.TabIndex = 15;
		this.lbInstruction.Text = "若為含單價分析之工項，則單價不覆蓋";
		appearance1.BackColor = System.Drawing.Color.White;
		this.lbItemCount.Appearance = appearance1;
		this.lbItemCount.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lbItemCount.Location = new System.Drawing.Point(75, 110);
		this.lbItemCount.Name = "lbItemCount";
		this.lbItemCount.Size = new System.Drawing.Size(80, 20);
		this.lbItemCount.TabIndex = 14;
		this.rbIgnoreExistingItem.BackColor = System.Drawing.Color.White;
		this.rbIgnoreExistingItem.Font = new System.Drawing.Font("細明體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.rbIgnoreExistingItem.Location = new System.Drawing.Point(35, 80);
		this.rbIgnoreExistingItem.Name = "rbIgnoreExistingItem";
		this.rbIgnoreExistingItem.Size = new System.Drawing.Size(309, 26);
		this.rbIgnoreExistingItem.TabIndex = 6;
		this.rbIgnoreExistingItem.Text = "遇相同工項編號時則略過";
		this.rbIgnoreExistingItem.UseVisualStyleBackColor = false;
		appearance2.BackColor = System.Drawing.Color.White;
		this.lbItemCountText.Appearance = appearance2;
		this.lbItemCountText.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lbItemCountText.Location = new System.Drawing.Point(29, 110);
		this.lbItemCountText.Name = "lbItemCountText";
		this.lbItemCountText.Size = new System.Drawing.Size(52, 20);
		this.lbItemCountText.TabIndex = 13;
		this.lbItemCountText.Text = "筆數：";
		this.rbOverrideExistingItem.BackColor = System.Drawing.Color.White;
		this.rbOverrideExistingItem.Checked = true;
		this.rbOverrideExistingItem.Font = new System.Drawing.Font("細明體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.rbOverrideExistingItem.Location = new System.Drawing.Point(35, 16);
		this.rbOverrideExistingItem.Name = "rbOverrideExistingItem";
		this.rbOverrideExistingItem.Size = new System.Drawing.Size(309, 43);
		this.rbOverrideExistingItem.TabIndex = 5;
		this.rbOverrideExistingItem.TabStop = true;
		this.rbOverrideExistingItem.Text = "遇相同工項編號時則覆蓋名稱、單位及單價";
		this.rbOverrideExistingItem.UseVisualStyleBackColor = false;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnOK.Appearance = appearance3;
		this.btnOK.BackColor = System.Drawing.SystemColors.Control;
		this.btnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnOK.Font = new System.Drawing.Font("細明體", 11f);
		this.btnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.btnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.btnOK.Location = new System.Drawing.Point(154, 197);
		this.btnOK.Name = "btnOK";
		this.btnOK.ShowFocusRect = false;
		this.btnOK.ShowOutline = false;
		this.btnOK.Size = new System.Drawing.Size(96, 32);
		this.btnOK.SupportThemes = false;
		this.btnOK.TabIndex = 10;
		this.btnOK.Text = "確定執行";
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnCancel.Appearance = appearance4;
		this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
		this.btnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Font = new System.Drawing.Font("細明體", 11f);
		this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btnCancel.Location = new System.Drawing.Point(256, 197);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.ShowFocusRect = false;
		this.btnCancel.ShowOutline = false;
		this.btnCancel.Size = new System.Drawing.Size(88, 32);
		this.btnCancel.SupportThemes = false;
		this.btnCancel.TabIndex = 11;
		this.btnCancel.Text = "取消";
		this.lbKindName.Font = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbKindName.Location = new System.Drawing.Point(87, 10);
		this.lbKindName.Name = "lbKindName";
		this.lbKindName.Size = new System.Drawing.Size(247, 37);
		this.lbKindName.TabIndex = 13;
		this.lbSendToText.Font = new System.Drawing.Font("細明體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lbSendToText.Location = new System.Drawing.Point(25, 20);
		this.lbSendToText.Name = "lbSendToText";
		this.lbSendToText.Size = new System.Drawing.Size(56, 16);
		this.lbSendToText.TabIndex = 14;
		this.lbSendToText.Text = "傳送：";
		base.AcceptButton = this.btnOK;
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(356, 234);
		base.Controls.Add(this.lbSendToText);
		base.Controls.Add(this.lbKindName);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.btnOK);
		base.Controls.Add(this.panelMiddle);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormSendtoMrsBase";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "傳送至基本資料庫";
		base.Load += new System.EventHandler(FormSendtoMrsBase_Load);
		this.panelMiddle.ResumeLayout(false);
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

	public FormSendtoMrsBase()
	{
		InitializeComponent();
	}

	private void FormSendtoMrsBase_Load(object sender, EventArgs e)
	{
		CesPrice cesPrice = new CesPrice();
		dsCesPrice = cesPrice.GetCesPriceByKindName(KindName);
		lbItemCount.Text = dsCesPrice.Tables[0].Rows.Count.ToString();
		lbKindName.Text = title;
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		DialogResult result = MessageBox.Show(this, "確定執行？", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (result != DialogResult.Yes)
		{
			return;
		}
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		dsCesPrice.Tables[0].AcceptChanges();
		foreach (DataRow row in dsCesPrice.Tables[0].Rows)
		{
			row.SetAdded();
		}
		MrsBaseA mrsBaseA = new MrsBaseA();
		ExecResult ER = mrsBaseA.UpdateMrsBaseAForCesPrice(dsCesPrice, userID, rbOverrideExistingItem.Checked);
		if (ER.ReturnCode == 0)
		{
			MessageBox.Show(this, "傳送成功！", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else
		{
			MessageBox.Show(this, "傳送失敗！" + ER.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		Cursor = Cursors.Default;
	}
}
