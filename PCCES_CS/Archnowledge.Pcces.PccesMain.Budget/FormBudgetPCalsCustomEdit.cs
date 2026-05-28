using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.PccesMain.Budget.BDGT_Component;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetPCalsCustomEdit : Form
{
	private const string CallFormHelp = "FormBudgetPCalsCustomEdit";

	private Panel panel9;

	private GroupBox groupBox5;

	private UltraButton A1_Btn_Cncl;

	private UltraButton A1_Btn_Next;

	private UltraLabel ultraLabel1;

	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_A;

	private UltraTabPageControl Tab_B;

	private UltraTextEditor txtCustomName;

	private GroupBox groupBox1;

	private UltraLabel ultraLabel2;

	private C1FlexGrid c1FlexGrid1;

	private IContainer components;

	private ImageList imageList2;

	private PictureBox pictureBox1;

	private PictureBox pictureBox2;

	private UltraLabel ultraLabel3;

	private PccesFormAction F_ActionName = PccesFormAction.None;

	private string F_ProjectCode = "";

	private string F_VarName = "";

	private string F_VarAlias = "";

	private string F_DoWorkType = "EDIT";

	private string F_UserID = "";

	private DataTable DT_FPick = new DataTable();

	private PCals PCALS1;

	private UltraTextEditor txtRate;

	private UltraLabel ultraLabel4;

	private UltraButton editSegmentation;

	private UltraLabel lbl_ItemC;

	private Timer timer1;

	private ArrayList F_AList = new ArrayList();

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

	public string _ProjectCode
	{
		get
		{
			return F_ProjectCode;
		}
		set
		{
			F_ProjectCode = value;
		}
	}

	public string _VarName
	{
		get
		{
			return F_VarName;
		}
		set
		{
			F_VarName = value;
		}
	}

	public string _VarAlias
	{
		get
		{
			return F_VarAlias;
		}
		set
		{
			F_VarAlias = value;
		}
	}

	public string _DoWorkType
	{
		get
		{
			return F_DoWorkType;
		}
		set
		{
			F_DoWorkType = value;
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

	public FormBudgetPCalsCustomEdit()
	{
		InitializeComponent();
		CellStyle cs1 = c1FlexGrid1.Styles.Add("EditMode");
		cs1.DataType = typeof(Image);
		cs1.ImageAlign = ImageAlignEnum.RightCenter;
		HideCols(IsHide: true);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void HideCols(bool IsHide)
	{
		if (IsHide)
		{
			c1FlexGrid1.Cols["SNo"].Visible = false;
			c1FlexGrid1.Cols["PrintNo"].Visible = false;
			c1FlexGrid1.Cols["CanCheck"].Visible = false;
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetPCalsCustomEdit));
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.txtRate = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.pictureBox2 = new System.Windows.Forms.PictureBox();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.txtCustomName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel9 = new System.Windows.Forms.Panel();
		this.lbl_ItemC = new Infragistics.Win.Misc.UltraLabel();
		this.editSegmentation = new Infragistics.Win.Misc.UltraButton();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.A1_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.A1_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.Tab_A.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCustomName).BeginInit();
		this.panel9.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.txtRate);
		this.Tab_A.Controls.Add(this.ultraLabel4);
		this.Tab_A.Controls.Add(this.ultraLabel3);
		this.Tab_A.Controls.Add(this.pictureBox2);
		this.Tab_A.Controls.Add(this.pictureBox1);
		this.Tab_A.Controls.Add(this.c1FlexGrid1);
		this.Tab_A.Controls.Add(this.ultraLabel2);
		this.Tab_A.Controls.Add(this.groupBox1);
		this.Tab_A.Controls.Add(this.txtCustomName);
		this.Tab_A.Controls.Add(this.ultraLabel1);
		this.Tab_A.Location = new System.Drawing.Point(0, 0);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(584, 451);
		this.txtRate.AutoSize = true;
		this.txtRate.Location = new System.Drawing.Point(344, 69);
		this.txtRate.Name = "txtRate";
		this.txtRate.Size = new System.Drawing.Size(64, 21);
		this.txtRate.TabIndex = 29;
		this.txtRate.Text = "100";
		this.txtRate.Validating += new System.ComponentModel.CancelEventHandler(txtRate_Validating);
		appearance1.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance1;
		this.ultraLabel4.Location = new System.Drawing.Point(409, 70);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(32, 23);
		this.ultraLabel4.TabIndex = 28;
		this.ultraLabel4.Text = "%";
		this.ultraLabel3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		appearance2.BackColor = System.Drawing.Color.White;
		appearance2.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
		appearance2.ForeColor = System.Drawing.Color.OrangeRed;
		this.ultraLabel3.Appearance = appearance2;
		this.ultraLabel3.Location = new System.Drawing.Point(10, 408);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(552, 20);
		this.ultraLabel3.TabIndex = 27;
		this.ultraLabel3.Text = "說明:先勾選要加總的項目，再點選[± 號]欄位變換加減";
		this.pictureBox2.Image = (System.Drawing.Image)resources.GetObject("pictureBox2.Image");
		this.pictureBox2.Location = new System.Drawing.Point(16, 64);
		this.pictureBox2.Name = "pictureBox2";
		this.pictureBox2.Size = new System.Drawing.Size(32, 32);
		this.pictureBox2.TabIndex = 26;
		this.pictureBox2.TabStop = false;
		this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		this.pictureBox1.Location = new System.Drawing.Point(16, 8);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(32, 32);
		this.pictureBox1.TabIndex = 25;
		this.pictureBox1.TabStop = false;
		this.c1FlexGrid1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.c1FlexGrid1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.c1FlexGrid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.c1FlexGrid1.ColumnInfo = resources.GetString("c1FlexGrid1.ColumnInfo");
		this.c1FlexGrid1.ExtendLastCol = true;
		this.c1FlexGrid1.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.c1FlexGrid1.ForeColor = System.Drawing.Color.Black;
		this.c1FlexGrid1.Location = new System.Drawing.Point(12, 104);
		this.c1FlexGrid1.Name = "c1FlexGrid1";
		this.c1FlexGrid1.Size = new System.Drawing.Size(556, 298);
		this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid1.Styles"));
		this.c1FlexGrid1.TabIndex = 24;
		this.c1FlexGrid1.Tree.Column = 2;
		this.c1FlexGrid1.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.SimpleLeaf;
		this.c1FlexGrid1.Click += new System.EventHandler(c1FlexGrid1_Click);
		this.c1FlexGrid1.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(c1FlexGrid1_AfterEdit);
		this.c1FlexGrid1.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(c1FlexGrid1_CellChanged);
		appearance3.BackColor = System.Drawing.Color.White;
		this.ultraLabel2.Appearance = appearance3;
		this.ultraLabel2.Location = new System.Drawing.Point(59, 72);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(293, 20);
		this.ultraLabel2.TabIndex = 23;
		this.ultraLabel2.Text = "自訂變數金額 = 挑選的運算項目總金額 x ";
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Location = new System.Drawing.Point(10, 48);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(566, 8);
		this.groupBox1.TabIndex = 22;
		this.groupBox1.TabStop = false;
		this.txtCustomName.AutoSize = true;
		this.txtCustomName.Location = new System.Drawing.Point(180, 15);
		this.txtCustomName.Name = "txtCustomName";
		this.txtCustomName.Size = new System.Drawing.Size(272, 21);
		this.txtCustomName.TabIndex = 21;
		appearance4.BackColor = System.Drawing.Color.White;
		this.ultraLabel1.Appearance = appearance4;
		this.ultraLabel1.Location = new System.Drawing.Point(59, 18);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(120, 20);
		this.ultraLabel1.TabIndex = 20;
		this.ultraLabel1.Text = "自訂變數項名稱:";
		this.Tab_B.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(584, 451);
		this.panel9.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel9.Controls.Add(this.lbl_ItemC);
		this.panel9.Controls.Add(this.editSegmentation);
		this.panel9.Controls.Add(this.groupBox5);
		this.panel9.Controls.Add(this.A1_Btn_Cncl);
		this.panel9.Controls.Add(this.A1_Btn_Next);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel9.Location = new System.Drawing.Point(0, 451);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(584, 45);
		this.panel9.TabIndex = 21;
		appearance5.BackColor = System.Drawing.Color.White;
		appearance5.ForeColor = System.Drawing.Color.Red;
		this.lbl_ItemC.Appearance = appearance5;
		this.lbl_ItemC.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lbl_ItemC.Location = new System.Drawing.Point(185, 18);
		this.lbl_ItemC.Name = "lbl_ItemC";
		this.lbl_ItemC.Size = new System.Drawing.Size(189, 20);
		this.lbl_ItemC.TabIndex = 21;
		this.lbl_ItemC.Text = "<-有使用分段計價公式";
		this.editSegmentation.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance6.ImageHAlign = Infragistics.Win.HAlign.Left;
		appearance6.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.editSegmentation.Appearance = appearance6;
		this.editSegmentation.BackColor = System.Drawing.SystemColors.Control;
		this.editSegmentation.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button3DOldStyle;
		this.editSegmentation.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.editSegmentation.ImageSize = new System.Drawing.Size(20, 20);
		this.editSegmentation.ImageTransparentColor = System.Drawing.Color.White;
		this.editSegmentation.Location = new System.Drawing.Point(12, 12);
		this.editSegmentation.Name = "editSegmentation";
		this.editSegmentation.ShowFocusRect = false;
		this.editSegmentation.ShowOutline = false;
		this.editSegmentation.Size = new System.Drawing.Size(167, 31);
		this.editSegmentation.SupportThemes = false;
		this.editSegmentation.TabIndex = 4;
		this.editSegmentation.Text = "編輯分段計價公式...";
		this.editSegmentation.Click += new System.EventHandler(editSegmentation_Click);
		this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox5.Location = new System.Drawing.Point(0, 0);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(584, 10);
		this.groupBox5.TabIndex = 3;
		this.groupBox5.TabStop = false;
		this.A1_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance7.Image = resources.GetObject("appearance7.Image");
		appearance7.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A1_Btn_Cncl.Appearance = appearance7;
		this.A1_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A1_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A1_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A1_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A1_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A1_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A1_Btn_Cncl.Location = new System.Drawing.Point(488, 9);
		this.A1_Btn_Cncl.Name = "A1_Btn_Cncl";
		this.A1_Btn_Cncl.ShowFocusRect = false;
		this.A1_Btn_Cncl.ShowOutline = false;
		this.A1_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A1_Btn_Cncl.SupportThemes = false;
		this.A1_Btn_Cncl.TabIndex = 2;
		this.A1_Btn_Cncl.Text = "取消";
		this.A1_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance8.Image = resources.GetObject("appearance8.Image");
		appearance8.ImageHAlign = Infragistics.Win.HAlign.Left;
		appearance8.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A1_Btn_Next.Appearance = appearance8;
		this.A1_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.A1_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A1_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.A1_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.A1_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.A1_Btn_Next.Location = new System.Drawing.Point(396, 9);
		this.A1_Btn_Next.Name = "A1_Btn_Next";
		this.A1_Btn_Next.ShowFocusRect = false;
		this.A1_Btn_Next.ShowOutline = false;
		this.A1_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.A1_Btn_Next.SupportThemes = false;
		this.A1_Btn_Next.TabIndex = 1;
		this.A1_Btn_Next.Text = "確定";
		this.A1_Btn_Next.Click += new System.EventHandler(A1_Btn_Next_Click);
		this.Tab_Ctrl.BackColor = System.Drawing.Color.White;
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_A);
		this.Tab_Ctrl.Controls.Add(this.Tab_B);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(584, 451);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabIndex = 23;
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "tab1";
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "tab2";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2] { ultraTab1, ultraTab2 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(584, 451);
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.White;
		this.imageList2.Images.SetKeyName(0, "");
		this.imageList2.Images.SetKeyName(1, "");
		this.imageList2.Images.SetKeyName(2, "");
		this.timer1.Interval = 350;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 18);
		this.BackColor = System.Drawing.Color.White;
		base.CancelButton = this.A1_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(584, 496);
		base.Controls.Add(this.Tab_Ctrl);
		base.Controls.Add(this.panel9);
		this.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormBudgetPCalsCustomEdit";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "自訂變數項編輯";
		base.Load += new System.EventHandler(FormBudgetPCalsCustomEdit_Load);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormBudgetPCalsCustomEdit_FormClosing);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormBudgetPCalsCustomEdit_KeyDown);
		this.Tab_A.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCustomName).EndInit();
		this.panel9.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).EndInit();
		this.Tab_Ctrl.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void FormBudgetPCalsCustomEdit_Load(object sender, EventArgs e)
	{
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("預算書PCals自訂變數項目加總項目挑選" + F_ProjectCode + "(" + IPStr + ")");
		ItemA dbItemA = new ItemA(aArr);
		dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbItemA.ps_projectCode = F_ProjectCode;
		DT_FPick = dbItemA.ListItem("", F_ProjectCode);
		DataTable DT_OperList = new DataTable();
		PCALS1 = new PCals(aArr);
		PCALS1.ps_projectCode = F_ProjectCode;
		if (!(_DoWorkType == "NEW"))
		{
			txtCustomName.Text = F_VarAlias;
			DT_OperList = PCALS1.GetCustomOperationList(F_VarName);
		}
		CellRange rg1 = c1FlexGrid1.GetCellRange(0, 0);
		rg1.Style = c1FlexGrid1.Styles["EditMode"];
		rg1.Image = imageList2.Images[1];
		CellRange rg2 = c1FlexGrid1.GetCellRange(0, 1);
		rg2.Style = c1FlexGrid1.Styles["EditMode"];
		rg2.Image = imageList2.Images[1];
		BindDataToGrid(DT_OperList);
		LoadingScreen();
		CheckIsUseItemC();
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("CalsCustomEdit", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("CalsCustomEdit", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("CalsCustomEdit", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("CalsCustomEdit", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("CalsCustomEdit", "PK_Height"));
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

	private void BindDataToGrid(DataTable DT1)
	{
		int iLevel = 0;
		c1FlexGrid1.Rows.Count = DT_FPick.Rows.Count + 1;
		int iidx = -1;
		string sTmpStr = "";
		CellStyle CS2 = c1FlexGrid1.Styles.Add("MainColor");
		CellStyle CSZ = c1FlexGrid1.Styles.Add("ZColor");
		CS2.ForeColor = Color.Blue;
		CSZ.ForeColor = Color.Green;
		if (DT1.Rows.Count > 0)
		{
			txtRate.Text = (PubTools.Str2Double(DT1.Rows[0]["VarRate"]) * 100.0).ToString();
		}
		for (int i = 0; i < DT_FPick.Rows.Count; i++)
		{
			c1FlexGrid1.Rows[i + 1].IsNode = true;
			sTmpStr = DT_FPick.Rows[i]["PrintNo"].ToString().Trim();
			c1FlexGrid1[i + 1, "ItemNo"] = DT_FPick.Rows[i]["ItemNo"];
			c1FlexGrid1[i + 1, "CName"] = DT_FPick.Rows[i]["CName"];
			c1FlexGrid1[i + 1, "PrintNo"] = DT_FPick.Rows[i]["PrintNo"].ToString().Trim();
			c1FlexGrid1[i + 1, "SNo"] = DT_FPick.Rows[i]["SNo"].ToString();
			if (DT_FPick.Rows[i]["kind"].ToString() == "Z")
			{
				c1FlexGrid1.Rows[i + 1].Style = CSZ;
			}
			else if (DT_FPick.Rows[i]["kind"].ToString() != "W")
			{
				c1FlexGrid1.Rows[i + 1].Style = CS2;
			}
			if (iidx > -1)
			{
				c1FlexGrid1[i + 1, "CanCheck"] = false;
			}
			else
			{
				c1FlexGrid1[i + 1, "CanCheck"] = true;
			}
			string st1 = DT_FPick.Rows[i]["PrintNo"].ToString().Trim();
			if (F_DoWorkType == "EDIT" && FindSno(DT1, DT_FPick.Rows[i]["SNo"].ToString().Trim()) > -1)
			{
				c1FlexGrid1[i + 1, "IsCheck"] = true;
				c1FlexGrid1[i + 1, "VarSign"] = ((GetSpeciVarSign(DT1, DT_FPick.Rows[i]["SNo"].ToString().Trim()) == "1") ? "＋" : "－");
			}
			else
			{
				c1FlexGrid1[i + 1, "IsCheck"] = false;
				c1FlexGrid1[i + 1, "VarSign"] = "";
			}
			c1FlexGrid1.Rows[i + 1].Node.Level = Convert.ToInt32(DT_FPick.Rows[i]["PrintNo"].ToString().Trim().Length / 4);
			if (c1FlexGrid1.Rows[i + 1].Node.Level > iLevel)
			{
				iLevel = c1FlexGrid1.Rows[i + 1].Node.Level;
			}
		}
	}

	private string GetSpeciVarSign(DataTable DT_Find, string sNO)
	{
		string RetV = "";
		for (int i = 0; i < DT_Find.Rows.Count; i++)
		{
			if (DT_Find.Rows[i]["SNo"].ToString().Trim() == sNO)
			{
				RetV = DT_Find.Rows[i]["VarSign"].ToString();
				break;
			}
		}
		return RetV;
	}

	private int FindSno(DataTable DT_Find, string sNO)
	{
		int RetV = -1;
		for (int i = 0; i < DT_Find.Rows.Count; i++)
		{
			if (DT_Find.Rows[i]["SNo"].ToString().Trim() == sNO)
			{
				RetV = i;
				break;
			}
		}
		return RetV;
	}

	private void c1FlexGrid1_AfterEdit(object sender, RowColEventArgs e)
	{
		if (e.Col != 0 || e.Row <= 0)
		{
			return;
		}
		for (int i = c1FlexGrid1.Selection.r1; i <= c1FlexGrid1.Selection.r2; i++)
		{
			if (!(bool)c1FlexGrid1[i, "IsCheck"])
			{
				c1FlexGrid1[i, "VarSign"] = "";
			}
			else
			{
				c1FlexGrid1[i, "VarSign"] = "＋";
			}
		}
	}

	private void c1FlexGrid1_Click(object sender, EventArgs e)
	{
		if (c1FlexGrid1.MouseRow <= 0 || c1FlexGrid1.MouseCol <= 0)
		{
			return;
		}
		int rowIndex = c1FlexGrid1.MouseRow;
		if (c1FlexGrid1.Col == 1 && (bool)c1FlexGrid1[rowIndex, "IsCheck"])
		{
			string sSign = c1FlexGrid1[rowIndex, "VarSign"].ToString().Trim();
			if (sSign == "＋")
			{
				c1FlexGrid1[rowIndex, "VarSign"] = "－";
			}
			else if (sSign == "－")
			{
				c1FlexGrid1[rowIndex, "VarSign"] = "＋";
			}
		}
	}

	private void A1_Btn_Next_Click(object sender, EventArgs e)
	{
		txtCustomName.Text = txtCustomName.Text.Trim();
		if (txtCustomName.Text == "")
		{
			MessageBox.Show(this, "變數名稱不可空白。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		bool isNumeric = true;
		for (int i = 0; i < txtCustomName.Text.Length; i++)
		{
			try
			{
				int i2 = int.Parse(txtCustomName.Text.Substring(i, 1));
			}
			catch (Exception)
			{
				isNumeric = false;
				break;
			}
		}
		if (isNumeric)
		{
			MessageBox.Show(this, "變數名稱不可全為數字！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		BudPCalsCustomVar budPCalsCustomVar = new BudPCalsCustomVar();
		string varName = budPCalsCustomVar.PCalsCustomVarName(F_ProjectCode, txtCustomName.Text);
		if ((_DoWorkType == "NEW" || txtCustomName.Text.CompareTo(F_VarAlias) != 0) && varName.Length > 0)
		{
			MessageBox.Show(this, "此變數名稱已存在！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		DataTable DT_Sign = new DataTable();
		DT_Sign.Columns.Add("SNo", Type.GetType("System.String"));
		DT_Sign.Columns.Add("VarSign", Type.GetType("System.String"));
		DT_Sign.Columns.Add("PrintNo", Type.GetType("System.String"));
		DT_Sign.Columns.Add("VarRate", Type.GetType("System.Double"));
		for (int i = 1; i < c1FlexGrid1.Rows.Count; i++)
		{
			if ((bool)c1FlexGrid1[i, "IsCheck"])
			{
				DataRow DR = DT_Sign.NewRow();
				DR["SNo"] = c1FlexGrid1[i, "SNo"];
				DR["VarSign"] = c1FlexGrid1[i, "VarSign"];
				DR["PrintNo"] = c1FlexGrid1[i, "PrintNo"].ToString().Trim();
				DR["VarRate"] = PubTools.Str2Double(txtRate.Text) / 100.0;
				DT_Sign.Rows.Add(DR);
			}
		}
		PCALS1.ps_projectCode = F_ProjectCode;
		PCALS1.ps_VarAlias = txtCustomName.Text;
		PCALS1.ps_VarName = F_VarName;
		PCALS1.SaveSettingData(DT_Sign);
		base.DialogResult = DialogResult.OK;
	}

	private void c1FlexGrid1_CellChanged(object sender, RowColEventArgs e)
	{
	}

	private void txtRate_Validating(object sender, CancelEventArgs e)
	{
		try
		{
			Convert.ToDouble(txtRate.Text.Trim());
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetPCalsCustomEdit.cs.cs" + ex.Message);
			MessageBox.Show(this, "比率有誤。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtRate.Focus();
		}
	}

	private void FormBudgetPCalsCustomEdit_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("CalsCustomEdit", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("CalsCustomEdit", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("CalsCustomEdit", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("CalsCustomEdit", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("CalsCustomEdit", "WindowState", base.WindowState.ToString());
	}

	private void FormBudgetPCalsCustomEdit_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormBudgetPCalsCustomEdit");
		}
	}

	private void editSegmentation_Click(object sender, EventArgs e)
	{
		S_Form2 Form2 = new S_Form2();
		Form2._ActionName = F_ActionName;
		Form2._UserID = F_UserID;
		Form2._ProjectCode = F_ProjectCode;
		Form2._printNoVarname = txtCustomName.Text.Trim();
		if (Form2.ShowDialog() == DialogResult.OK)
		{
		}
		Form2 = null;
		CheckIsUseItemC();
	}

	private void CheckIsUseItemC()
	{
		DataTable DT_ItemC = PCALS1.GetCustomItemC(F_VarAlias);
		if (DT_ItemC.Rows.Count > 0)
		{
			editSegmentation.Appearance.BackColor = Color.LightGreen;
			timer1.Enabled = true;
		}
		else
		{
			editSegmentation.Appearance.BackColor = default(Color);
			timer1.Enabled = false;
			lbl_ItemC.Visible = false;
		}
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		lbl_ItemC.Visible = !lbl_ItemC.Visible;
	}
}
