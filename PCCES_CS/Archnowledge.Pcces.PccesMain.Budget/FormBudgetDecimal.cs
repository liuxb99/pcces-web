using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.General;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetDecimal : Form
{
	private string UserID;

	private string ProjectCode = string.Empty;

	private DataSet dsPubDecimal;

	private string iniFilePath = AppDomain.CurrentDomain.BaseDirectory + "OptionSet.ini";

	private PubDecimal pubDecimal;

	private UltraLabel lbTitle;

	private GroupBox gbDetailList;

	private GroupBox gbAnalysisList;

	private UltraLabel ultraLabel2;

	private NumericUpDown DetailListQty;

	private NumericUpDown DetailListCost;

	private UltraLabel ultraLabel3;

	private NumericUpDown DetailListAmount;

	private UltraLabel ultraLabel4;

	private NumericUpDown AnaListAmount;

	private UltraLabel ultraLabel5;

	private NumericUpDown AnaListCost;

	private UltraLabel ultraLabel6;

	private NumericUpDown AnaListQty;

	private UltraLabel ultraLabel7;

	private UltraLabel lbInstruction;

	private UltraButton btnOK;

	private UltraButton btnCancel;

	private CheckBox cbInterlock;

	private Container components = null;

	private CheckBox EnableItemAmt2;

	private UltraLabel ultraLabel1;

	public string _UserID
	{
		get
		{
			return UserID;
		}
		set
		{
			UserID = value;
		}
	}

	public FormBudgetDecimal(string ProjectCode)
	{
		InitializeComponent();
		this.ProjectCode = ProjectCode;
		pubDecimal = new PubDecimal();
	}

	private void FormBudgetDecimal_Load(object sender, EventArgs e)
	{
		CorrectRatio();
		dsPubDecimal = pubDecimal.GetPubDecimal(ProjectCode);
		DataTable dtPubDecimal = dsPubDecimal.Tables[0];
		if (dtPubDecimal.Rows.Count == 0)
		{
			DataRow newRow = dtPubDecimal.NewRow();
			newRow["ProjectCode"] = ProjectCode;
			newRow["itemQty"] = 3;
			newRow["itemCost"] = 0;
			newRow["itemAmt"] = 0;
			newRow["analysisQty"] = 3;
			newRow["analysisCost"] = 2;
			newRow["analysisAmt"] = 2;
			newRow["EnableItemAmt2"] = false;
			dtPubDecimal.Rows.Add(newRow);
			pubDecimal.UpdatePubDecimal(dsPubDecimal);
		}
		DetailListQty.Value = ArchConvert.Obj2Int(dtPubDecimal.Rows[0]["itemQty"]);
		DetailListCost.Value = ArchConvert.Obj2Int(dtPubDecimal.Rows[0]["itemCost"]);
		DetailListAmount.Value = ArchConvert.Obj2Int(dtPubDecimal.Rows[0]["itemAmt"]);
		AnaListQty.Value = ArchConvert.Obj2Int(dtPubDecimal.Rows[0]["analysisQty"]);
		AnaListCost.Value = ArchConvert.Obj2Int(dtPubDecimal.Rows[0]["analysisCost"]);
		AnaListAmount.Value = ArchConvert.Obj2Int(dtPubDecimal.Rows[0]["analysisAmt"]);
		EnableItemAmt2.Checked = ArchConvert.Obj2Bool(dtPubDecimal.Rows[0]["EnableItemAmt2"]);
		string sIsInterLock = CommonMethods.IniReadValue(iniFilePath, "BDGT", "IsDecimalInterlock");
		if (sIsInterLock.ToUpper() == "TRUE")
		{
			cbInterlock.Checked = true;
		}
		else
		{
			cbInterlock.Checked = false;
		}
		if (Is22132814())
		{
			btnOK.Enabled = false;
		}
	}

	private bool Is22132814()
	{
		string sPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "22132814.dat");
		if (File.Exists(sPath))
		{
			return true;
		}
		return false;
	}

	private void CorrectRatio()
	{
		double ratio = CommonMethods.GetWindowRatio(base.Handle);
		if (ratio != 1.0)
		{
			gbDetailList.Font = new Font(gbDetailList.Font.Name, (float)((double)gbDetailList.Font.Size * ratio));
			gbAnalysisList.Font = new Font(gbAnalysisList.Font.Name, (float)((double)gbAnalysisList.Font.Size * ratio));
			lbInstruction.Font = new Font(lbInstruction.Font.Name, (float)((double)lbInstruction.Font.Size * ratio));
			ultraLabel1.Font = new Font(ultraLabel1.Font.Name, (float)((double)ultraLabel1.Font.Size * ratio));
		}
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		DataTable dtPubDecimal = dsPubDecimal.Tables[0];
		dtPubDecimal.Rows[0]["itemQty"] = DetailListQty.Value;
		dtPubDecimal.Rows[0]["itemCost"] = DetailListCost.Value;
		dtPubDecimal.Rows[0]["itemAmt"] = DetailListAmount.Value;
		dtPubDecimal.Rows[0]["analysisQty"] = AnaListQty.Value;
		dtPubDecimal.Rows[0]["analysisCost"] = AnaListCost.Value;
		dtPubDecimal.Rows[0]["analysisAmt"] = AnaListAmount.Value;
		dtPubDecimal.Rows[0]["EnableItemAmt2"] = EnableItemAmt2.Checked;
		if (cbInterlock.Checked)
		{
			CommonMethods.IniWriteValue(iniFilePath, "BDGT", "IsDecimalInterlock", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(iniFilePath, "BDGT", "IsDecimalInterlock", "FALSE");
		}
		switch (MessageBox.Show("是否將詳細表及單價分析表之各項取位原則設定與此表(詳細表及單價分析表)取位一致？\n\n是：全部重新異動每一項目小數取位原則。\n\u3000\u3000詳細表－－－＞『數量：" + DetailListQty.Value + "\u3000單價：" + DetailListCost.Value + "\u3000複價：" + DetailListAmount.Value + "』\n\u3000\u3000單價分析表－＞『數量：" + AnaListQty.Value + "\u3000單價：" + AnaListCost.Value + "\u3000複價：" + AnaListAmount.Value + "』\n否：異動目前每一項目之小數取位原則，但不異動使用者調整過的小數取位。", "詢問", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
		{
		case DialogResult.Yes:
			pubDecimal.ResetBudProjectDecimalPlaceSettings(ProjectCode);
			base.DialogResult = DialogResult.OK;
			Close();
			break;
		case DialogResult.No:
			base.DialogResult = DialogResult.OK;
			Close();
			break;
		case DialogResult.Cancel:
			return;
		}
		ExecResult ER = pubDecimal.UpdatePubDecimal(dsPubDecimal);
		if (ER.ReturnCode != 0)
		{
			MessageBox.Show(ER.Message, "錯誤", MessageBoxButtons.OK);
		}
	}

	private void CheckedAndValueChanged(object sender, EventArgs e)
	{
		if (cbInterlock.Checked)
		{
			AnaListAmount.Value = DetailListCost.Value;
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetDecimal));
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		this.lbTitle = new Infragistics.Win.Misc.UltraLabel();
		this.gbDetailList = new System.Windows.Forms.GroupBox();
		this.DetailListAmount = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.DetailListCost = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.DetailListQty = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.gbAnalysisList = new System.Windows.Forms.GroupBox();
		this.AnaListAmount = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.AnaListCost = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.AnaListQty = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.lbInstruction = new Infragistics.Win.Misc.UltraLabel();
		this.btnOK = new Infragistics.Win.Misc.UltraButton();
		this.btnCancel = new Infragistics.Win.Misc.UltraButton();
		this.cbInterlock = new System.Windows.Forms.CheckBox();
		this.EnableItemAmt2 = new System.Windows.Forms.CheckBox();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.gbDetailList.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DetailListAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.DetailListCost).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.DetailListQty).BeginInit();
		this.gbAnalysisList.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.AnaListAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.AnaListCost).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.AnaListQty).BeginInit();
		base.SuspendLayout();
		this.lbTitle.Location = new System.Drawing.Point(12, 8);
		this.lbTitle.Name = "lbTitle";
		this.lbTitle.Size = new System.Drawing.Size(416, 23);
		this.lbTitle.TabIndex = 0;
		this.lbTitle.Text = "請設定下列項目的小數位數";
		this.gbDetailList.Controls.Add(this.DetailListAmount);
		this.gbDetailList.Controls.Add(this.ultraLabel4);
		this.gbDetailList.Controls.Add(this.DetailListCost);
		this.gbDetailList.Controls.Add(this.ultraLabel3);
		this.gbDetailList.Controls.Add(this.DetailListQty);
		this.gbDetailList.Controls.Add(this.ultraLabel2);
		this.gbDetailList.Location = new System.Drawing.Point(12, 32);
		this.gbDetailList.Name = "gbDetailList";
		this.gbDetailList.Size = new System.Drawing.Size(224, 112);
		this.gbDetailList.TabIndex = 1;
		this.gbDetailList.TabStop = false;
		this.gbDetailList.Text = "詳細表";
		this.DetailListAmount.Font = new System.Drawing.Font("Arial", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.DetailListAmount.Location = new System.Drawing.Point(148, 76);
		this.DetailListAmount.Maximum = new decimal(new int[4] { 4, 0, 0, 0 });
		this.DetailListAmount.Name = "DetailListAmount";
		this.DetailListAmount.Size = new System.Drawing.Size(56, 25);
		this.DetailListAmount.TabIndex = 6;
		this.ultraLabel4.Location = new System.Drawing.Point(16, 80);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(128, 23);
		this.ultraLabel4.TabIndex = 5;
		this.ultraLabel4.Text = "複價\u3000小數位數:";
		this.DetailListCost.Font = new System.Drawing.Font("Arial", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.DetailListCost.Location = new System.Drawing.Point(148, 48);
		this.DetailListCost.Maximum = new decimal(new int[4] { 4, 0, 0, 0 });
		this.DetailListCost.Name = "DetailListCost";
		this.DetailListCost.Size = new System.Drawing.Size(56, 25);
		this.DetailListCost.TabIndex = 4;
		this.DetailListCost.ValueChanged += new System.EventHandler(CheckedAndValueChanged);
		this.ultraLabel3.Location = new System.Drawing.Point(16, 52);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(128, 23);
		this.ultraLabel3.TabIndex = 3;
		this.ultraLabel3.Text = "單價\u3000小數位數:";
		this.DetailListQty.Font = new System.Drawing.Font("Arial", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.DetailListQty.Location = new System.Drawing.Point(148, 20);
		this.DetailListQty.Maximum = new decimal(new int[4] { 4, 0, 0, 0 });
		this.DetailListQty.Name = "DetailListQty";
		this.DetailListQty.Size = new System.Drawing.Size(56, 25);
		this.DetailListQty.TabIndex = 2;
		this.ultraLabel2.Location = new System.Drawing.Point(16, 24);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(128, 23);
		this.ultraLabel2.TabIndex = 0;
		this.ultraLabel2.Text = "數量\u3000小數位數:";
		this.gbAnalysisList.Controls.Add(this.AnaListAmount);
		this.gbAnalysisList.Controls.Add(this.ultraLabel5);
		this.gbAnalysisList.Controls.Add(this.AnaListCost);
		this.gbAnalysisList.Controls.Add(this.ultraLabel6);
		this.gbAnalysisList.Controls.Add(this.AnaListQty);
		this.gbAnalysisList.Controls.Add(this.ultraLabel7);
		this.gbAnalysisList.Location = new System.Drawing.Point(248, 32);
		this.gbAnalysisList.Name = "gbAnalysisList";
		this.gbAnalysisList.Size = new System.Drawing.Size(220, 112);
		this.gbAnalysisList.TabIndex = 2;
		this.gbAnalysisList.TabStop = false;
		this.gbAnalysisList.Text = "單價分析表工項";
		this.AnaListAmount.Font = new System.Drawing.Font("Arial", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.AnaListAmount.Location = new System.Drawing.Point(144, 76);
		this.AnaListAmount.Maximum = new decimal(new int[4] { 4, 0, 0, 0 });
		this.AnaListAmount.Name = "AnaListAmount";
		this.AnaListAmount.Size = new System.Drawing.Size(56, 25);
		this.AnaListAmount.TabIndex = 12;
		this.AnaListAmount.ValueChanged += new System.EventHandler(CheckedAndValueChanged);
		this.ultraLabel5.Location = new System.Drawing.Point(12, 80);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(128, 23);
		this.ultraLabel5.TabIndex = 11;
		this.ultraLabel5.Text = "複價\u3000小數位數:";
		this.AnaListCost.Font = new System.Drawing.Font("Arial", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.AnaListCost.Location = new System.Drawing.Point(144, 48);
		this.AnaListCost.Maximum = new decimal(new int[4] { 4, 0, 0, 0 });
		this.AnaListCost.Name = "AnaListCost";
		this.AnaListCost.Size = new System.Drawing.Size(56, 25);
		this.AnaListCost.TabIndex = 10;
		this.ultraLabel6.Location = new System.Drawing.Point(12, 52);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(128, 23);
		this.ultraLabel6.TabIndex = 9;
		this.ultraLabel6.Text = "單價\u3000小數位數:";
		this.AnaListQty.Font = new System.Drawing.Font("Arial", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.AnaListQty.Location = new System.Drawing.Point(144, 20);
		this.AnaListQty.Maximum = new decimal(new int[4] { 4, 0, 0, 0 });
		this.AnaListQty.Name = "AnaListQty";
		this.AnaListQty.Size = new System.Drawing.Size(56, 25);
		this.AnaListQty.TabIndex = 8;
		this.ultraLabel7.Location = new System.Drawing.Point(12, 24);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(128, 23);
		this.ultraLabel7.TabIndex = 7;
		this.ultraLabel7.Text = "數量\u3000小數位數:";
		appearance1.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.lbInstruction.Appearance = appearance1;
		this.lbInstruction.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbInstruction.Location = new System.Drawing.Point(28, 215);
		this.lbInstruction.Name = "lbInstruction";
		this.lbInstruction.Size = new System.Drawing.Size(444, 34);
		this.lbInstruction.TabIndex = 3;
		this.lbInstruction.Text = "\r\n有下層單價分析之工作項目之分析單價,係依詳細表之小數設定四捨五入 ";
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.btnOK.Appearance = appearance2;
		this.btnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOK.Font = new System.Drawing.Font("細明體", 11f);
		this.btnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.btnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.btnOK.Location = new System.Drawing.Point(292, 273);
		this.btnOK.Name = "btnOK";
		this.btnOK.ShowFocusRect = false;
		this.btnOK.ShowOutline = false;
		this.btnOK.Size = new System.Drawing.Size(88, 31);
		this.btnOK.SupportThemes = false;
		this.btnOK.TabIndex = 9;
		this.btnOK.Text = "確定";
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.btnCancel.Appearance = appearance3;
		this.btnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Font = new System.Drawing.Font("細明體", 11f);
		this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btnCancel.Location = new System.Drawing.Point(380, 273);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.ShowFocusRect = false;
		this.btnCancel.ShowOutline = false;
		this.btnCancel.Size = new System.Drawing.Size(88, 31);
		this.btnCancel.SupportThemes = false;
		this.btnCancel.TabIndex = 8;
		this.btnCancel.Text = "取消";
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.cbInterlock.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.cbInterlock.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.cbInterlock.Location = new System.Drawing.Point(29, 155);
		this.cbInterlock.Name = "cbInterlock";
		this.cbInterlock.Size = new System.Drawing.Size(419, 24);
		this.cbInterlock.TabIndex = 13;
		this.cbInterlock.Text = "【詳細表--單價】取位與【單價分析表--複價】取位連動。";
		this.cbInterlock.CheckedChanged += new System.EventHandler(CheckedAndValueChanged);
		this.EnableItemAmt2.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.EnableItemAmt2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.EnableItemAmt2.Location = new System.Drawing.Point(29, 185);
		this.EnableItemAmt2.Name = "EnableItemAmt2";
		this.EnableItemAmt2.Size = new System.Drawing.Size(443, 24);
		this.EnableItemAmt2.TabIndex = 14;
		this.EnableItemAmt2.Text = "【詳細表--複價】取位為 0 時，在【詳細表】中以小數兩位顯示。";
		appearance4.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.ultraLabel1.Appearance = appearance4;
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(12, 227);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(24, 22);
		this.ultraLabel1.TabIndex = 15;
		this.ultraLabel1.Text = "●";
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(484, 316);
		base.Controls.Add(this.lbInstruction);
		base.Controls.Add(this.ultraLabel1);
		base.Controls.Add(this.EnableItemAmt2);
		base.Controls.Add(this.cbInterlock);
		base.Controls.Add(this.btnOK);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.gbAnalysisList);
		base.Controls.Add(this.gbDetailList);
		base.Controls.Add(this.lbTitle);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormBudgetDecimal";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "設定小數位數";
		base.Load += new System.EventHandler(FormBudgetDecimal_Load);
		this.gbDetailList.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.DetailListAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.DetailListCost).EndInit();
		((System.ComponentModel.ISupportInitialize)this.DetailListQty).EndInit();
		this.gbAnalysisList.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.AnaListAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.AnaListCost).EndInit();
		((System.ComponentModel.ISupportInitialize)this.AnaListQty).EndInit();
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
}
