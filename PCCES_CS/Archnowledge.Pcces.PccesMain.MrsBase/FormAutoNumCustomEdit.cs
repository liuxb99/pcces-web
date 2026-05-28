using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.MrsBase;

public class FormAutoNumCustomEdit : Form
{
	private const string CallFormHelp = "FormAutoNumCustomEdit";

	private Panel panel8;

	private UltraButton A1_Btn_Cncl;

	private GroupBox groupBox4;

	private UltraButton D_Btn_Fnsh;

	private Panel panel1;

	private UltraTextEditor txtCode;

	private UltraLabel ultraLabel3;

	private UltraLabel lblCode;

	private UltraLabel lblContent;

	private UltraTextEditor txtContent;

	private string F_CodeCol = "";

	private string F_ChapCode = "";

	private string F_CodeSection = "";

	private int F_SelfRow = -1;

	private string F_DEPT_ID = "";

	private int F_MaxRow = -1;

	private int F_MinRow = -1;

	private string F_CodeType = "";

	private Container components = null;

	public string _CodeCol
	{
		get
		{
			return F_CodeCol;
		}
		set
		{
			F_CodeCol = value;
		}
	}

	public string _ChapCode
	{
		get
		{
			return F_ChapCode;
		}
		set
		{
			F_ChapCode = value;
		}
	}

	public string _CodeSection
	{
		get
		{
			return F_CodeSection;
		}
		set
		{
			F_CodeSection = value;
		}
	}

	public int _SelfRow
	{
		get
		{
			return F_SelfRow;
		}
		set
		{
			F_SelfRow = value;
		}
	}

	public string _DEPT_ID
	{
		get
		{
			return F_DEPT_ID;
		}
		set
		{
			F_DEPT_ID = value;
		}
	}

	public int _MaxRow
	{
		get
		{
			return F_MaxRow;
		}
		set
		{
			F_MaxRow = value;
		}
	}

	public int _MinRow
	{
		get
		{
			return F_MinRow;
		}
		set
		{
			F_MinRow = value;
		}
	}

	public string _CodeType
	{
		get
		{
			return F_CodeType;
		}
		set
		{
			F_CodeType = value;
		}
	}

	public FormAutoNumCustomEdit()
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.MrsBase.FormAutoNumCustomEdit));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		this.panel8 = new System.Windows.Forms.Panel();
		this.A1_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.D_Btn_Fnsh = new Infragistics.Win.Misc.UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.txtContent = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lblContent = new Infragistics.Win.Misc.UltraLabel();
		this.txtCode = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lblCode = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.panel8.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtContent).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).BeginInit();
		base.SuspendLayout();
		this.panel8.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel8.Controls.Add(this.A1_Btn_Cncl);
		this.panel8.Controls.Add(this.groupBox4);
		this.panel8.Controls.Add(this.D_Btn_Fnsh);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel8.Location = new System.Drawing.Point(0, 129);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(424, 44);
		this.panel8.TabIndex = 19;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A1_Btn_Cncl.Appearance = appearance1;
		this.A1_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A1_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A1_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A1_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A1_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A1_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A1_Btn_Cncl.Location = new System.Drawing.Point(210, 10);
		this.A1_Btn_Cncl.Name = "A1_Btn_Cncl";
		this.A1_Btn_Cncl.ShowFocusRect = false;
		this.A1_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A1_Btn_Cncl.SupportThemes = false;
		this.A1_Btn_Cncl.TabIndex = 4;
		this.A1_Btn_Cncl.Text = "取消";
		this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox4.Location = new System.Drawing.Point(0, 0);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(424, 8);
		this.groupBox4.TabIndex = 3;
		this.groupBox4.TabStop = false;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Fnsh.Appearance = appearance2;
		this.D_Btn_Fnsh.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Fnsh.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Fnsh.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Fnsh.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Fnsh.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Fnsh.Location = new System.Drawing.Point(119, 10);
		this.D_Btn_Fnsh.Name = "D_Btn_Fnsh";
		this.D_Btn_Fnsh.ShowFocusRect = false;
		this.D_Btn_Fnsh.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Fnsh.SupportThemes = false;
		this.D_Btn_Fnsh.TabIndex = 1;
		this.D_Btn_Fnsh.Text = "存檔";
		this.D_Btn_Fnsh.Click += new System.EventHandler(D_Btn_Fnsh_Click);
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.Controls.Add(this.txtContent);
		this.panel1.Controls.Add(this.lblContent);
		this.panel1.Controls.Add(this.txtCode);
		this.panel1.Controls.Add(this.lblCode);
		this.panel1.Controls.Add(this.ultraLabel3);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(424, 129);
		this.panel1.TabIndex = 20;
		this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(panel1_Paint);
		appearance3.FontData.Name = "細明體";
		appearance3.FontData.SizeInPoints = 11f;
		this.txtContent.Appearance = appearance3;
		this.txtContent.Location = new System.Drawing.Point(113, 84);
		this.txtContent.MaxLength = 100;
		this.txtContent.Name = "txtContent";
		this.txtContent.Size = new System.Drawing.Size(296, 24);
		this.txtContent.TabIndex = 3;
		appearance4.TextHAlign = Infragistics.Win.HAlign.Right;
		this.lblContent.Appearance = appearance4;
		this.lblContent.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblContent.Location = new System.Drawing.Point(8, 86);
		this.lblContent.Name = "lblContent";
		this.lblContent.Size = new System.Drawing.Size(104, 23);
		this.lblContent.TabIndex = 2;
		this.lblContent.Text = "10 碼名稱:";
		appearance5.FontData.Name = "細明體";
		appearance5.FontData.SizeInPoints = 11f;
		this.txtCode.Appearance = appearance5;
		this.txtCode.Location = new System.Drawing.Point(113, 50);
		this.txtCode.MaxLength = 3;
		this.txtCode.Name = "txtCode";
		this.txtCode.Size = new System.Drawing.Size(80, 24);
		this.txtCode.TabIndex = 1;
		appearance6.TextHAlign = Infragistics.Win.HAlign.Right;
		this.lblCode.Appearance = appearance6;
		this.lblCode.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblCode.Location = new System.Drawing.Point(8, 52);
		this.lblCode.Name = "lblCode";
		this.lblCode.Size = new System.Drawing.Size(104, 23);
		this.lblCode.TabIndex = 0;
		this.lblCode.Text = "10 碼:";
		this.ultraLabel3.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel3.Location = new System.Drawing.Point(7, 14);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(224, 23);
		this.ultraLabel3.TabIndex = 0;
		this.ultraLabel3.Text = "請完成下列資料輸入";
		base.AcceptButton = this.D_Btn_Fnsh;
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.CancelButton = this.A1_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(424, 173);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel8);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.Name = "FormAutoNumCustomEdit";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "自動編碼--自訂規則表--自訂碼";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormAutoNumCustomEdit_KeyDown);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormAutoNumCustomEdit_FormClosing);
		base.Load += new System.EventHandler(FormAutoNumCustomEdit_Load);
		base.Activated += new System.EventHandler(FormAutoNumCustomEdit_Activated);
		this.panel8.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtContent).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).EndInit();
		base.ResumeLayout(false);
	}

	private void panel1_Paint(object sender, PaintEventArgs e)
	{
	}

	private void GetDataFromDataBase()
	{
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = "PccAdmin";
		DataTable DT_Code = DBCLS.GetUserDefine("Select Code, Content From AutoNumB Where ChapCode='" + F_ChapCode + "'    And CodeSection = '" + F_CodeSection + "'    And SelfRow = " + F_SelfRow + "    And IsCustom= 'Y'    And Version = '" + F_DEPT_ID + "' ");
		if (DT_Code.Rows.Count > 0)
		{
			txtCode.Text = DT_Code.Rows[0]["Code"].ToString().Trim();
			txtContent.Text = DT_Code.Rows[0]["Content"].ToString().Trim();
		}
	}

	private void SaveDataToDataBase()
	{
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = "PccAdmin";
		DataTable DT_Code = DBCLS.GetUserDefine("Select Code, Content From AutoNumB Where ChapCode='" + F_ChapCode + "'    And CodeSection = '" + F_CodeSection + "'    And SelfRow = " + F_SelfRow + "    And IsCustom= 'Y'    And Version = '" + F_DEPT_ID + "' ");
		if (DT_Code.Rows.Count > 0)
		{
			DBCLS.ExecuteCommand("Update AutoNumB    Set Code = '" + txtCode.Text.Trim() + "',        Content = '" + txtContent.Text.Trim() + "'  Where ChapCode='" + F_ChapCode + "'    And CodeSection = '" + F_CodeSection + "'    And SelfRow = " + F_SelfRow + "    And IsCustom= 'Y'    And Version = '" + F_DEPT_ID + "' ");
		}
		else
		{
			DBCLS.ExecuteCommand("Insert Into AutoNumB (ChapCode,Code,CodeSection,MinRow,MaxRow,SelfRow,Content,IsCustom,Version)Values('" + F_ChapCode + "','" + txtCode.Text.Trim() + "','" + F_CodeSection + "'," + F_MinRow + "," + F_MaxRow + "," + F_SelfRow + ",'" + txtContent.Text.Trim() + "', 'Y','" + F_DEPT_ID + "') ");
		}
	}

	private void FormAutoNumCustomEdit_Load(object sender, EventArgs e)
	{
		if (F_CodeType == "E" || F_CodeType == "L")
		{
			switch (F_CodeCol)
			{
			case "06":
				lblCode.Text = "02-07 碼：";
				lblContent.Text = "02-07 碼名稱：";
				break;
			case "07":
				lblCode.Text = "08 碼：";
				lblContent.Text = "08 碼名稱：";
				break;
			case "08":
				lblCode.Text = "09 碼：";
				lblContent.Text = "09 碼名稱：";
				break;
			case "09":
				lblCode.Text = "10 碼：";
				lblContent.Text = "10 碼名稱：";
				break;
			case "10":
				lblCode.Text = "11 碼：";
				lblContent.Text = "11 碼名稱：";
				break;
			}
		}
		else
		{
			lblCode.Text = F_CodeCol + " 碼：";
			lblContent.Text = F_CodeCol + " 碼名稱：";
		}
		GetDataFromDataBase();
		LoadingScreen();
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("AutoNumCustomEdit", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("AutoNumCustomEdit", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("AutoNumCustomEdit", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("AutoNumCustomEdit", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("AutoNumCustomEdit", "PK_Height"));
		if (iLoc_X > 0 && iLoc_Y > 0)
		{
			base.Location = new Point(iLoc_X, iLoc_Y);
		}
		if (iSiz_W > 0)
		{
			base.Width = iSiz_W;
		}
		if (iSiz_H > 0)
		{
			base.Height = iSiz_H;
		}
	}

	private void D_Btn_Fnsh_Click(object sender, EventArgs e)
	{
		SaveDataToDataBase();
		base.DialogResult = DialogResult.OK;
	}

	private void FormAutoNumCustomEdit_Activated(object sender, EventArgs e)
	{
		if (txtCode.Text.Trim() == "")
		{
			txtCode.Focus();
		}
		else
		{
			txtContent.Focus();
		}
	}

	private void FormAutoNumCustomEdit_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("AutoNumCustomEdit", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("AutoNumCustomEdit", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("AutoNumCustomEdit", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("AutoNumCustomEdit", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("AutoNumCustomEdit", "WindowState", base.WindowState.ToString());
	}

	private void FormAutoNumCustomEdit_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormAutoNumCustomEdit");
		}
	}
}
