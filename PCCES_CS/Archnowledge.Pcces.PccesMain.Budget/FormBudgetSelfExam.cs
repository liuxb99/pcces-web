using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.Budget;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetSelfExam : Form
{
	private IContainer components = null;

	public Panel panel9;

	private GroupBox groupBox5;

	private UltraButton A1_Btn_Cncl;

	private UltraButton A1_Btn_Next;

	private UltraLabel ultraLabel18;

	private CheckBox checkBox1;

	private CheckBox checkBox2;

	private CheckBox checkBox3;

	private CheckBox checkBox4;

	private CheckBox checkBox5;

	private LinkLabel linkLabel1;

	private LinkLabel linkLabel2;

	private LinkLabel linkLabel3;

	private LinkLabel linkLabel4;

	private LinkLabel linkLabel5;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private CheckBox checkBox6;

	private string F_SelfExamValue = "";

	private string projectCode = "";

	private BudProjMrsA budProjMrsA = new BudProjMrsA();

	private BudProjMrsB budProjMrsB = new BudProjMrsB();

	private PccesFormAction FormActionName;

	private string F_chgCount = "0";

	public string _SelfExamValue
	{
		get
		{
			return F_SelfExamValue;
		}
		set
		{
			F_SelfExamValue = value;
		}
	}

	public string _ProjectCode
	{
		get
		{
			return projectCode;
		}
		set
		{
			projectCode = value;
		}
	}

	public PccesFormAction _FormActionName
	{
		get
		{
			return FormActionName;
		}
		set
		{
			FormActionName = value;
		}
	}

	public string _chgCount
	{
		get
		{
			return F_chgCount;
		}
		set
		{
			F_chgCount = value;
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetSelfExam));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		this.panel9 = new System.Windows.Forms.Panel();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.A1_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.A1_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel18 = new Infragistics.Win.Misc.UltraLabel();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.checkBox2 = new System.Windows.Forms.CheckBox();
		this.checkBox3 = new System.Windows.Forms.CheckBox();
		this.checkBox4 = new System.Windows.Forms.CheckBox();
		this.checkBox5 = new System.Windows.Forms.CheckBox();
		this.linkLabel1 = new System.Windows.Forms.LinkLabel();
		this.linkLabel2 = new System.Windows.Forms.LinkLabel();
		this.linkLabel3 = new System.Windows.Forms.LinkLabel();
		this.linkLabel4 = new System.Windows.Forms.LinkLabel();
		this.linkLabel5 = new System.Windows.Forms.LinkLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.checkBox6 = new System.Windows.Forms.CheckBox();
		this.panel9.SuspendLayout();
		base.SuspendLayout();
		this.panel9.AutoSize = true;
		this.panel9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panel9.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel9.Controls.Add(this.groupBox5);
		this.panel9.Controls.Add(this.A1_Btn_Cncl);
		this.panel9.Controls.Add(this.A1_Btn_Next);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel9.Location = new System.Drawing.Point(0, 310);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(792, 44);
		this.panel9.TabIndex = 22;
		this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox5.Location = new System.Drawing.Point(0, 0);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(792, 8);
		this.groupBox5.TabIndex = 3;
		this.groupBox5.TabStop = false;
		this.A1_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A1_Btn_Cncl.Appearance = appearance1;
		this.A1_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A1_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A1_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A1_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A1_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A1_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A1_Btn_Cncl.Location = new System.Drawing.Point(698, 10);
		this.A1_Btn_Cncl.Name = "A1_Btn_Cncl";
		this.A1_Btn_Cncl.ShowFocusRect = false;
		this.A1_Btn_Cncl.ShowOutline = false;
		this.A1_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A1_Btn_Cncl.SupportThemes = false;
		this.A1_Btn_Cncl.TabIndex = 2;
		this.A1_Btn_Cncl.Text = "取消";
		this.A1_Btn_Cncl.Click += new System.EventHandler(A1_Btn_Cncl_Click);
		this.A1_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A1_Btn_Next.Appearance = appearance2;
		this.A1_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.A1_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A1_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.A1_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.A1_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.A1_Btn_Next.Location = new System.Drawing.Point(580, 10);
		this.A1_Btn_Next.Name = "A1_Btn_Next";
		this.A1_Btn_Next.ShowFocusRect = false;
		this.A1_Btn_Next.ShowOutline = false;
		this.A1_Btn_Next.Size = new System.Drawing.Size(114, 31);
		this.A1_Btn_Next.SupportThemes = false;
		this.A1_Btn_Next.TabIndex = 1;
		this.A1_Btn_Next.Text = "確認儲存";
		this.A1_Btn_Next.Click += new System.EventHandler(A1_Btn_Next_Click);
		appearance3.BackColor = System.Drawing.Color.White;
		this.ultraLabel18.Appearance = appearance3;
		this.ultraLabel18.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel18.Location = new System.Drawing.Point(16, 23);
		this.ultraLabel18.Name = "ultraLabel18";
		this.ultraLabel18.Size = new System.Drawing.Size(588, 20);
		this.ultraLabel18.TabIndex = 23;
		this.ultraLabel18.Text = "歡迎使用自主檢查，請依下列項目自主檢查後勾選。";
		this.checkBox1.AutoSize = true;
		this.checkBox1.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.checkBox1.Location = new System.Drawing.Point(39, 69);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(420, 19);
		this.checkBox1.TabIndex = 24;
		this.checkBox1.Text = "１.已確認沒有：專案工項→分析子項為負數及計算錯誤項目";
		this.checkBox1.UseVisualStyleBackColor = true;
		this.checkBox2.AutoSize = true;
		this.checkBox2.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.checkBox2.Location = new System.Drawing.Point(39, 101);
		this.checkBox2.Name = "checkBox2";
		this.checkBox2.Size = new System.Drawing.Size(337, 19);
		this.checkBox2.TabIndex = 24;
		this.checkBox2.Text = "２.已確認沒有：專案工項→單價或數量為0項目";
		this.checkBox2.UseVisualStyleBackColor = true;
		this.checkBox3.AutoSize = true;
		this.checkBox3.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.checkBox3.Location = new System.Drawing.Point(39, 133);
		this.checkBox3.Name = "checkBox3";
		this.checkBox3.Size = new System.Drawing.Size(532, 19);
		this.checkBox3.TabIndex = 24;
		this.checkBox3.Text = "３.已確認沒有：專案工項→工項名稱及單位完全一樣但工項編碼出現2個以上";
		this.checkBox3.UseVisualStyleBackColor = true;
		this.checkBox4.AutoSize = true;
		this.checkBox4.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.checkBox4.Location = new System.Drawing.Point(39, 165);
		this.checkBox4.Name = "checkBox4";
		this.checkBox4.Size = new System.Drawing.Size(277, 19);
		this.checkBox4.TabIndex = 24;
		this.checkBox4.Text = "４.已確認沒有：詳細表→複價為0項目";
		this.checkBox4.UseVisualStyleBackColor = true;
		this.checkBox5.AutoSize = true;
		this.checkBox5.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.checkBox5.Location = new System.Drawing.Point(39, 197);
		this.checkBox5.Name = "checkBox5";
		this.checkBox5.Size = new System.Drawing.Size(285, 19);
		this.checkBox5.TabIndex = 24;
		this.checkBox5.Text = "５.已確認：發包設定→自辦項目不勾選";
		this.checkBox5.UseVisualStyleBackColor = true;
		this.linkLabel1.AutoSize = true;
		this.linkLabel1.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.linkLabel1.Location = new System.Drawing.Point(603, 69);
		this.linkLabel1.Name = "linkLabel1";
		this.linkLabel1.Size = new System.Drawing.Size(108, 15);
		this.linkLabel1.TabIndex = 25;
		((System.Windows.Forms.Label)this.linkLabel1).TabStop = true;
		this.linkLabel1.Text = "1.系統協助檢查";
		this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
		this.linkLabel2.AutoSize = true;
		this.linkLabel2.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.linkLabel2.Location = new System.Drawing.Point(603, 101);
		this.linkLabel2.Name = "linkLabel2";
		this.linkLabel2.Size = new System.Drawing.Size(108, 15);
		this.linkLabel2.TabIndex = 25;
		((System.Windows.Forms.Label)this.linkLabel2).TabStop = true;
		this.linkLabel2.Text = "2.系統協助檢查";
		this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabel2_LinkClicked);
		this.linkLabel3.AutoSize = true;
		this.linkLabel3.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.linkLabel3.Location = new System.Drawing.Point(603, 133);
		this.linkLabel3.Name = "linkLabel3";
		this.linkLabel3.Size = new System.Drawing.Size(108, 15);
		this.linkLabel3.TabIndex = 25;
		((System.Windows.Forms.Label)this.linkLabel3).TabStop = true;
		this.linkLabel3.Text = "3.系統協助檢查";
		this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabel3_LinkClicked);
		this.linkLabel4.AutoSize = true;
		this.linkLabel4.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.linkLabel4.Location = new System.Drawing.Point(603, 165);
		this.linkLabel4.Name = "linkLabel4";
		this.linkLabel4.Size = new System.Drawing.Size(108, 15);
		this.linkLabel4.TabIndex = 25;
		((System.Windows.Forms.Label)this.linkLabel4).TabStop = true;
		this.linkLabel4.Text = "4.系統協助檢查";
		this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabel4_LinkClicked);
		this.linkLabel5.AutoSize = true;
		this.linkLabel5.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.linkLabel5.Location = new System.Drawing.Point(603, 197);
		this.linkLabel5.Name = "linkLabel5";
		this.linkLabel5.Size = new System.Drawing.Size(108, 15);
		this.linkLabel5.TabIndex = 25;
		((System.Windows.Forms.Label)this.linkLabel5).TabStop = true;
		this.linkLabel5.Text = "5.系統協助檢查";
		this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabel5_LinkClicked);
		appearance4.BackColor = System.Drawing.Color.White;
		appearance4.ForeColor = System.Drawing.Color.Red;
		this.ultraLabel1.Appearance = appearance4;
		this.ultraLabel1.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(39, 262);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(64, 20);
		this.ultraLabel1.TabIndex = 26;
		this.ultraLabel1.Text = "提示：";
		appearance5.BackColor = System.Drawing.Color.White;
		this.ultraLabel2.Appearance = appearance5;
		this.ultraLabel2.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel2.Location = new System.Drawing.Point(95, 262);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(530, 41);
		this.ultraLabel2.TabIndex = 27;
		this.ultraLabel2.Text = "每次重新總計後，自主檢查勾選項就會被清空，建議在完成最後編製之後，再來執行『自主檢查』。";
		this.checkBox6.AutoSize = true;
		this.checkBox6.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.checkBox6.Location = new System.Drawing.Point(39, 228);
		this.checkBox6.Name = "checkBox6";
		this.checkBox6.Size = new System.Drawing.Size(601, 19);
		this.checkBox6.TabIndex = 28;
		this.checkBox6.Text = "６.已確認：所填人力工項未低於月薪新臺幣（下同）３萬元、日薪 1,364元或時薪 171元";
		this.checkBox6.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.ClientSize = new System.Drawing.Size(792, 354);
		base.Controls.Add(this.checkBox6);
		base.Controls.Add(this.ultraLabel2);
		base.Controls.Add(this.ultraLabel1);
		base.Controls.Add(this.linkLabel5);
		base.Controls.Add(this.linkLabel4);
		base.Controls.Add(this.linkLabel3);
		base.Controls.Add(this.linkLabel2);
		base.Controls.Add(this.linkLabel1);
		base.Controls.Add(this.checkBox5);
		base.Controls.Add(this.checkBox4);
		base.Controls.Add(this.checkBox3);
		base.Controls.Add(this.checkBox2);
		base.Controls.Add(this.checkBox1);
		base.Controls.Add(this.ultraLabel18);
		base.Controls.Add(this.panel9);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormBudgetSelfExam";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "自主檢查";
		base.Load += new System.EventHandler(FormBudgetSelfExam_Load);
		this.panel9.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public FormBudgetSelfExam()
	{
		InitializeComponent();
	}

	private void A1_Btn_Cncl_Click(object sender, EventArgs e)
	{
	}

	private void FormBudgetSelfExam_Load(object sender, EventArgs e)
	{
		ParseSelfExamValue();
	}

	private void ParseSelfExamValue()
	{
		if (F_SelfExamValue.Length == 5)
		{
			if (F_SelfExamValue.Substring(0, 1) == "1")
			{
				checkBox1.Checked = true;
			}
			if (F_SelfExamValue.Substring(1, 1) == "1")
			{
				checkBox2.Checked = true;
			}
			if (F_SelfExamValue.Substring(2, 1) == "1")
			{
				checkBox3.Checked = true;
			}
			if (F_SelfExamValue.Substring(3, 1) == "1")
			{
				checkBox4.Checked = true;
			}
			if (F_SelfExamValue.Substring(4, 1) == "1")
			{
				checkBox5.Checked = true;
			}
		}
		else if (F_SelfExamValue.Length == 6)
		{
			if (F_SelfExamValue.Substring(0, 1) == "1")
			{
				checkBox1.Checked = true;
			}
			if (F_SelfExamValue.Substring(1, 1) == "1")
			{
				checkBox2.Checked = true;
			}
			if (F_SelfExamValue.Substring(2, 1) == "1")
			{
				checkBox3.Checked = true;
			}
			if (F_SelfExamValue.Substring(3, 1) == "1")
			{
				checkBox4.Checked = true;
			}
			if (F_SelfExamValue.Substring(4, 1) == "1")
			{
				checkBox5.Checked = true;
			}
			if (F_SelfExamValue.Substring(5, 1) == "1")
			{
				checkBox6.Checked = true;
			}
		}
	}

	private void A1_Btn_Next_Click(object sender, EventArgs e)
	{
		string sSelfValue = "";
		sSelfValue = ((!checkBox1.Checked) ? (sSelfValue + "0") : (sSelfValue + "1"));
		sSelfValue = ((!checkBox2.Checked) ? (sSelfValue + "0") : (sSelfValue + "1"));
		sSelfValue = ((!checkBox3.Checked) ? (sSelfValue + "0") : (sSelfValue + "1"));
		sSelfValue = ((!checkBox4.Checked) ? (sSelfValue + "0") : (sSelfValue + "1"));
		sSelfValue = ((!checkBox5.Checked) ? (sSelfValue + "0") : (sSelfValue + "1"));
		sSelfValue = ((!checkBox6.Checked) ? (sSelfValue + "0") : (sSelfValue + "1"));
		F_SelfExamValue = sSelfValue;
		base.DialogResult = DialogResult.OK;
	}

	private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		int iCostZeroItems = budProjMrsA.IsThereCostEquZeroItem(projectCode);
		if (iCostZeroItems > 0)
		{
			MessageBox.Show(this, "注意：偵測到專案工項有單價或數量為\"0\"項目\n\n請使用【檢視】-->【專案工項維護】-->【檢視】-->【單價或數量為\"0\"項目】幫你篩選出有單價或數量為\"0\"項目。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			MessageBox.Show(this, "未發現：專案工項有單價或數量為\"0\"項目。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		DataTable DT_Dups = budProjMrsA.GetDuplicateItems(projectCode);
		int iRowsCount = DT_Dups.Rows.Count;
		if (iRowsCount > 0)
		{
			MessageBox.Show(this, "注意：偵測到工項名稱及單位完全一樣，但工項編碼出現2個以上，若要查詢哪些工項名稱重複\n\n請使用【檢視】-->【專案工項維護】-->【檢視】-->【工項名稱重複】幫你篩選出有重複之工項。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			MessageBox.Show(this, "未發現：工項名稱及單位完全一樣，但工項編碼出現2個以上。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		DT_Dups = null;
	}

	private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		string AppLocation = AppDomain.CurrentDomain.BaseDirectory;
		string iniFile = "OptionSet.ini";
		string IsAlertBIDZeroAmountItem = CommonMethods.IniReadValue(AppLocation + iniFile, "BID", "IsAlertBIDZeroAmountItem");
		int iAmtZero = 0;
		string sAmtZeroMessage = "";
		for (int i = 1; i < (base.Owner as frmBudget).gridBudget.Rows.Count; i++)
		{
			if ((base.Owner as frmBudget).gridBudget[i, "cName"] != null)
			{
				if ((base.Owner as frmBudget).gridBudget[i, "cName"].ToString() == "小計" && (base.Owner as frmBudget).gridBudget[i, "amount"] == null)
				{
					MessageBox.Show(this, "第" + i + "列的小計項沒有值，無法完成檢查，請先檢查該項小計以上項目是否有值?", "檢查中斷", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				if (ArchConvert.Obj2Decimal((base.Owner as frmBudget).gridBudget[i, "amount"].ToString()) == 0m)
				{
					iAmtZero++;
					string text = sAmtZeroMessage;
					sAmtZeroMessage = text + (base.Owner as frmBudget).gridBudget[i, "itemNo"].ToString() + "\t" + (base.Owner as frmBudget).gridBudget[i, "cName"].ToString() + "\t" + (base.Owner as frmBudget).gridBudget[i, "unitName"].ToString() + "\n";
				}
			}
		}
		if (iAmtZero > 0)
		{
			if (FormActionName == PccesFormAction.BID && IsAlertBIDZeroAmountItem == "TRUE")
			{
				return;
			}
			if (iAmtZero <= 40)
			{
				if (MessageBox.Show(this, "發現詳細表有 " + iAmtZero + " 項複價為\"0\"，是否要顯示詳細內容?", "警示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					MessageBox.Show(this, "您可以使用 Ctrl+C 將以下內容複製起來\n\n" + sAmtZeroMessage, "詳細表複價為0項目", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			else
			{
				MessageBox.Show(this, "發現詳細表有 " + iAmtZero + " 項複價為\"0\"，請重新檢查詳細表項目。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		else
		{
			MessageBox.Show(this, "未發現：詳細表有複價為\"0\"項目。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		BudPageBreak pagebreak = new BudPageBreak();
		DataTable DT_SelfExecuteItems = pagebreak.GetCheckedSelfExecuteItem(projectCode);
		if (DT_SelfExecuteItems.Rows.Count > 0)
		{
			MessageBox.Show("偵測到[自辦項目或子項]勾選了發包，請檢查發包設定是否正確勾選，\n\n請至【檔案】->【發包設定】檢查！", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			MessageBox.Show("未發現：[自辦項目或子項]勾選了發包。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		pagebreak = null;
	}

	private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		DataSet DS_Err = budProjMrsA.GetResource(projectCode);
		DataRow[] DR_Err = DS_Err.Tables[0].Select("CalcError = '1'");
		int iErr = DR_Err.Length;
		string sFun = CommonMethods.GetActionNameString(FormActionName);
		string sFilter = ((sFun.ToUpper() != "SUBCHG") ? "amount < 0 or cost < 0" : ("(amount < 0 or cost < 0) and chgCount =" + F_chgCount + ""));
		DataSet DS_Minus = budProjMrsB.GetProjMrsB(projectCode);
		DataRow[] DR_Minus = DS_Minus.Tables[0].Select(sFilter);
		int iMinus = DR_Minus.Length;
		if (iErr == 0 && iMinus == 0)
		{
			MessageBox.Show(this, "未發現：分析子項為負數及計算錯誤項目", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else if (iErr > 0 && iMinus > 0)
		{
			MessageBox.Show(this, "偵測到：\n計算錯誤" + iErr + "項，分析子項為負數" + iMinus + "項\n\n請使用【檢視】-->【專案工項維護】-->【計算錯誤項】及【分析子項為負】來篩選有問題的項目。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else if (iErr > 0)
		{
			MessageBox.Show(this, "偵測到：計算錯誤" + iErr + "項\n\n請使用【檢視】-->【專案工項維護】-->【計算錯誤項】來篩選有問題的項目。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else if (iMinus > 0)
		{
			MessageBox.Show(this, "偵測到：分析子項為負數" + iMinus + "項\n\n請使用【檢視】-->【專案工項維護】-->【分析子項為負】來篩選有問題的項目。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}
}
