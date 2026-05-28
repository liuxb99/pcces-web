using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.ShellLib;
using AxThreed;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.SysPlugin;

public class FormSysPlugin : Form
{
	private const string CallFormHelp = "FormSysPlugin";

	private Panel LeftPanel;

	public FunctionButtons functionButtons1;

	private OnlineList onlineList1;

	private Panel pnl_spliter;

	private UltraButton Btn_Splt;

	private AxSSPanel ssp_Lower;

	private AxSSPanel ssp_Bottom;

	private AxSSPanel ssp_Upper;

	private AxSSPanel ssp_Top;

	private Panel panel1;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel12;

	private UltraLabel ultraLabel13;

	private string F_MainDeptCode_G = "";

	private string F_MainDeptName_G = "";

	private string F_PccesCode_D = "";

	private string F_PccesName_D = "";

	private string F_PccesUnit_D = "";

	private string F_PubCode_D = "";

	private string F_Invoice_No = "";

	private string F_Title = "";

	private bool F_HasRegistered;

	private string F_UserID;

	private string F_UserName = "";

	private string F_FunctionName = "SysMaintain";

	private string F_ServerName = "localhost";

	private ArrayList ToolLists = new ArrayList();

	private ArrayList ToolParam = new ArrayList();

	private Panel pnl_Components;

	private PictureBox picPlugIn;

	private ImageList iglst_splt_Btn;

	private GroupBox groupBox6;

	private Label label1;

	private Label label2;

	private LinkLabel linkLabel1;

	private UltraButton ultraButton1;

	private IContainer components;

	public string _PccesCode_D
	{
		get
		{
			return F_PccesCode_D;
		}
		set
		{
			F_PccesCode_D = value;
		}
	}

	public string _PccesName_D
	{
		get
		{
			return F_PccesName_D;
		}
		set
		{
			F_PccesName_D = value;
		}
	}

	public string _PccesUnit_D
	{
		get
		{
			return F_PccesUnit_D;
		}
		set
		{
			F_PccesUnit_D = value;
		}
	}

	public string _PubCode_D
	{
		get
		{
			return F_PubCode_D;
		}
		set
		{
			F_PubCode_D = value;
		}
	}

	public string _MainCode_G
	{
		get
		{
			return F_MainDeptCode_G;
		}
		set
		{
			F_MainDeptCode_G = value;
		}
	}

	public string _MainName_G
	{
		get
		{
			return F_MainDeptName_G;
		}
		set
		{
			F_MainDeptName_G = value;
		}
	}

	public string _Invoice_No
	{
		get
		{
			return F_Invoice_No;
		}
		set
		{
			F_Invoice_No = value;
		}
	}

	public string _Title
	{
		get
		{
			return F_Title;
		}
		set
		{
			F_Title = value;
		}
	}

	public bool _HasRegistered
	{
		get
		{
			return F_HasRegistered;
		}
		set
		{
			F_HasRegistered = value;
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

	public string _UserName
	{
		get
		{
			return F_UserName;
		}
		set
		{
			F_UserName = value;
		}
	}

	public string _FunctionName
	{
		get
		{
			return F_FunctionName;
		}
		set
		{
			F_FunctionName = value;
		}
	}

	public string _ServerName
	{
		get
		{
			return F_ServerName;
		}
		set
		{
			F_ServerName = value;
		}
	}

	public FormSysPlugin()
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
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.SysPlugin.FormSysPlugin));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		this.LeftPanel = new System.Windows.Forms.Panel();
		this.functionButtons1 = new Archnowledge.Pcces.PccesMain.ArchControls.FunctionButtons();
		this.onlineList1 = new Archnowledge.Pcces.PccesMain.ArchControls.OnlineList();
		this.pnl_spliter = new System.Windows.Forms.Panel();
		this.Btn_Splt = new Infragistics.Win.Misc.UltraButton();
		this.ssp_Lower = new AxThreed.AxSSPanel();
		this.ssp_Bottom = new AxThreed.AxSSPanel();
		this.ssp_Upper = new AxThreed.AxSSPanel();
		this.ssp_Top = new AxThreed.AxSSPanel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.linkLabel1 = new System.Windows.Forms.LinkLabel();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.picPlugIn = new System.Windows.Forms.PictureBox();
		this.pnl_Components = new System.Windows.Forms.Panel();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.iglst_splt_Btn = new System.Windows.Forms.ImageList(this.components);
		this.LeftPanel.SuspendLayout();
		this.pnl_spliter.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ssp_Lower).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Bottom).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Upper).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Top).BeginInit();
		this.panel1.SuspendLayout();
		this.groupBox6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.picPlugIn).BeginInit();
		base.SuspendLayout();
		this.LeftPanel.Controls.Add(this.functionButtons1);
		this.LeftPanel.Controls.Add(this.onlineList1);
		this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
		this.LeftPanel.Location = new System.Drawing.Point(0, 0);
		this.LeftPanel.Name = "LeftPanel";
		this.LeftPanel.Size = new System.Drawing.Size(160, 645);
		this.LeftPanel.TabIndex = 5;
		this.functionButtons1._ActiveFunction = "";
		this.functionButtons1._CurrOpenMode = Archnowledge.Pcces.CommonClass.FunctionOpenMode.Budget;
		this.functionButtons1._ServerName = "localhost";
		this.functionButtons1._UserID = "PccesAdmin";
		this.functionButtons1._UserName = "";
		this.functionButtons1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.functionButtons1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.functionButtons1.Location = new System.Drawing.Point(0, 256);
		this.functionButtons1.Name = "functionButtons1";
		this.functionButtons1.Size = new System.Drawing.Size(160, 389);
		this.functionButtons1.TabIndex = 6;
		this.onlineList1._FunctionName = "";
		this.onlineList1._HasRegistered = false;
		this.onlineList1._ServerName = "localhost";
		this.onlineList1._TRY_Flag = "";
		this.onlineList1._UserID = "";
		this.onlineList1._UserName = "";
		this.onlineList1.AutoSize = true;
		this.onlineList1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.onlineList1.Dock = System.Windows.Forms.DockStyle.Top;
		this.onlineList1.Location = new System.Drawing.Point(0, 0);
		this.onlineList1.Name = "onlineList1";
		this.onlineList1.Size = new System.Drawing.Size(160, 256);
		this.onlineList1.TabIndex = 5;
		this.pnl_spliter.BackColor = System.Drawing.Color.LightGray;
		this.pnl_spliter.Controls.Add(this.Btn_Splt);
		this.pnl_spliter.Controls.Add(this.ssp_Lower);
		this.pnl_spliter.Controls.Add(this.ssp_Bottom);
		this.pnl_spliter.Controls.Add(this.ssp_Upper);
		this.pnl_spliter.Controls.Add(this.ssp_Top);
		this.pnl_spliter.Dock = System.Windows.Forms.DockStyle.Left;
		this.pnl_spliter.Location = new System.Drawing.Point(160, 0);
		this.pnl_spliter.Name = "pnl_spliter";
		this.pnl_spliter.Size = new System.Drawing.Size(7, 645);
		this.pnl_spliter.TabIndex = 6;
		appearance1.BorderColor = System.Drawing.Color.Transparent;
		appearance1.BorderColor3DBase = System.Drawing.Color.Transparent;
		appearance1.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance1.ImageBackground");
		this.Btn_Splt.Appearance = appearance1;
		this.Btn_Splt.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Borderless;
		this.Btn_Splt.Cursor = System.Windows.Forms.Cursors.Hand;
		this.Btn_Splt.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Btn_Splt.ImageSize = new System.Drawing.Size(7, 57);
		this.Btn_Splt.Location = new System.Drawing.Point(0, 292);
		this.Btn_Splt.Name = "Btn_Splt";
		this.Btn_Splt.ShapeImage = (System.Drawing.Image)resources.GetObject("Btn_Splt.ShapeImage");
		this.Btn_Splt.ShowFocusRect = false;
		this.Btn_Splt.ShowOutline = false;
		this.Btn_Splt.Size = new System.Drawing.Size(7, 37);
		this.Btn_Splt.TabIndex = 5;
		this.Btn_Splt.Click += new System.EventHandler(Btn_Splt_Click);
		this.ssp_Lower.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ssp_Lower.Location = new System.Drawing.Point(0, 329);
		this.ssp_Lower.Name = "ssp_Lower";
		this.ssp_Lower.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Lower.OcxState");
		this.ssp_Lower.Size = new System.Drawing.Size(7, 313);
		this.ssp_Lower.TabIndex = 3;
		this.ssp_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ssp_Bottom.Location = new System.Drawing.Point(0, 642);
		this.ssp_Bottom.Name = "ssp_Bottom";
		this.ssp_Bottom.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Bottom.OcxState");
		this.ssp_Bottom.Size = new System.Drawing.Size(7, 3);
		this.ssp_Bottom.TabIndex = 4;
		this.ssp_Upper.Dock = System.Windows.Forms.DockStyle.Top;
		this.ssp_Upper.Location = new System.Drawing.Point(0, 3);
		this.ssp_Upper.Name = "ssp_Upper";
		this.ssp_Upper.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Upper.OcxState");
		this.ssp_Upper.Size = new System.Drawing.Size(7, 289);
		this.ssp_Upper.TabIndex = 2;
		this.ssp_Top.Dock = System.Windows.Forms.DockStyle.Top;
		this.ssp_Top.Location = new System.Drawing.Point(0, 0);
		this.ssp_Top.Name = "ssp_Top";
		this.ssp_Top.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Top.OcxState");
		this.ssp_Top.Size = new System.Drawing.Size(7, 3);
		this.ssp_Top.TabIndex = 1;
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.groupBox6);
		this.panel1.Controls.Add(this.picPlugIn);
		this.panel1.Controls.Add(this.pnl_Components);
		this.panel1.Controls.Add(this.ultraLabel13);
		this.panel1.Controls.Add(this.ultraLabel12);
		this.panel1.Controls.Add(this.ultraLabel3);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(167, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(841, 645);
		this.panel1.TabIndex = 7;
		this.panel1.Resize += new System.EventHandler(panel1_Resize);
		this.groupBox6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox6.Controls.Add(this.ultraButton1);
		this.groupBox6.Controls.Add(this.linkLabel1);
		this.groupBox6.Controls.Add(this.label2);
		this.groupBox6.Controls.Add(this.label1);
		this.groupBox6.Location = new System.Drawing.Point(24, 552);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(792, 80);
		this.groupBox6.TabIndex = 31;
		this.groupBox6.TabStop = false;
		this.ultraButton1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton1.Location = new System.Drawing.Point(680, 32);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(92, 32);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 4;
		this.ultraButton1.Text = "畫面重整";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		this.linkLabel1.Font = new System.Drawing.Font("新細明體", 11.25f);
		this.linkLabel1.Location = new System.Drawing.Point(72, 16);
		this.linkLabel1.Name = "linkLabel1";
		this.linkLabel1.Size = new System.Drawing.Size(376, 24);
		this.linkLabel1.TabIndex = 2;
		((System.Windows.Forms.Label)this.linkLabel1).TabStop = true;
		this.linkLabel1.Text = "經費電腦估價系統(PCCES) 外掛專區";
		this.linkLabel1.Click += new System.EventHandler(linkLabel1_Click);
		this.label2.Font = new System.Drawing.Font("新細明體", 11.25f);
		this.label2.Location = new System.Drawing.Point(16, 18);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(56, 24);
		this.label2.TabIndex = 1;
		this.label2.Text = "請至：";
		this.label1.Font = new System.Drawing.Font("新細明體", 11.25f);
		this.label1.Location = new System.Drawing.Point(16, 48);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(376, 24);
		this.label1.TabIndex = 0;
		this.label1.Text = "下載需要之外掛程式，下載安裝後，請按畫面重整按鈕";
		this.picPlugIn.Image = (System.Drawing.Image)resources.GetObject("picPlugIn.Image");
		this.picPlugIn.Location = new System.Drawing.Point(48, 552);
		this.picPlugIn.Name = "picPlugIn";
		this.picPlugIn.Size = new System.Drawing.Size(20, 20);
		this.picPlugIn.TabIndex = 16;
		this.picPlugIn.TabStop = false;
		this.picPlugIn.Visible = false;
		this.pnl_Components.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.pnl_Components.Location = new System.Drawing.Point(40, 112);
		this.pnl_Components.Name = "pnl_Components";
		this.pnl_Components.Size = new System.Drawing.Size(552, 432);
		this.pnl_Components.TabIndex = 15;
		this.ultraLabel13.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel13.Appearance = appearance2;
		this.ultraLabel13.BackColor = System.Drawing.Color.Transparent;
		this.ultraLabel13.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance3.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance3.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance3.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel13.HotTrackAppearance = appearance3;
		this.ultraLabel13.HotTracking = true;
		this.ultraLabel13.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraLabel13.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.ultraLabel13.Location = new System.Drawing.Point(688, 56);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(124, 24);
		this.ultraLabel13.TabIndex = 14;
		this.ultraLabel13.Text = "結束外掛程式";
		this.ultraLabel13.Click += new System.EventHandler(ultraLabel13_Click);
		this.ultraLabel12.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance4.BorderColor = System.Drawing.Color.FromArgb(255, 128, 0);
		this.ultraLabel12.Appearance = appearance4;
		this.ultraLabel12.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.ultraLabel12.Location = new System.Drawing.Point(16, 88);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(808, 3);
		this.ultraLabel12.TabIndex = 13;
		appearance5.Image = resources.GetObject("appearance5.Image");
		appearance5.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance5;
		this.ultraLabel3.BackColor = System.Drawing.Color.Transparent;
		this.ultraLabel3.Font = new System.Drawing.Font("細明體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel3.ImageSize = new System.Drawing.Size(48, 48);
		this.ultraLabel3.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraLabel3.Location = new System.Drawing.Point(32, 24);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(224, 52);
		this.ultraLabel3.TabIndex = 5;
		this.ultraLabel3.Text = "外掛程式";
		this.iglst_splt_Btn.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("iglst_splt_Btn.ImageStream");
		this.iglst_splt_Btn.TransparentColor = System.Drawing.Color.Transparent;
		this.iglst_splt_Btn.Images.SetKeyName(0, "");
		this.iglst_splt_Btn.Images.SetKeyName(1, "");
		this.iglst_splt_Btn.Images.SetKeyName(2, "");
		this.iglst_splt_Btn.Images.SetKeyName(3, "");
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(1008, 645);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.pnl_spliter);
		base.Controls.Add(this.LeftPanel);
		base.KeyPreview = true;
		base.Name = "FormSysPlugin";
		this.Text = "FormSysPlugin";
		base.Load += new System.EventHandler(FormSysPlugin_Load);
		base.Resize += new System.EventHandler(FormSysPlugin_Resize);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormSysPlugin_KeyDown);
		this.LeftPanel.ResumeLayout(false);
		this.LeftPanel.PerformLayout();
		this.pnl_spliter.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ssp_Lower).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Bottom).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Upper).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Top).EndInit();
		this.panel1.ResumeLayout(false);
		this.groupBox6.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.picPlugIn).EndInit();
		base.ResumeLayout(false);
	}

	private void ultraLabel13_Click(object sender, EventArgs e)
	{
		string sWarning = "確定要結束外掛程式 ?";
		if (MessageBox.Show(this, sWarning, "外掛程式", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			(base.ParentForm as frmPccesMain).LeftPanel.Width = 160;
			Close();
		}
	}

	private void FormSysPlugin_Load(object sender, EventArgs e)
	{
		string sIniFileName = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "PccesMain.ini";
		string F_IsAddOn = CommonMethods.IniReadValue(sIniFileName, "AddOn", "OperationType");
		string sAllowRestore = CommonMethods.IniReadValue(CommonMethods.ExtractFilePath(Application.ExecutablePath) + "OptionSet.ini", "CommonData", "sAllowRestore");
		if (sAllowRestore.ToUpper() == "TRUE")
		{
			F_IsAddOn = "";
		}
		functionButtons1._UserID = F_UserID;
		functionButtons1._UserName = F_UserName;
		functionButtons1._ServerName = F_ServerName;
		functionButtons1._ActiveFunction = "SYSMAINTAIN";
		onlineList1.Disconnect();
		onlineList1._UserID = F_UserID;
		onlineList1._UserName = F_UserName;
		onlineList1._ServerName = F_ServerName;
		onlineList1._FunctionName = F_FunctionName;
		onlineList1._HasRegistered = F_HasRegistered;
		onlineList1.Connect();
		ProcessAddOn();
	}

	private void ProcessAddOn()
	{
		string AppLocation = AppDomain.CurrentDomain.BaseDirectory;
		string FileINI = AppLocation + "Addon.ini";
		ToolLists.Clear();
		ToolParam.Clear();
		for (int i = 1; i <= 20; i++)
		{
			string sValue = CommonMethods.IniReadValue(FileINI, "Plugin", "TOOL" + i);
			if (sValue.Trim() != "")
			{
				ToolLists.Add(sValue.Substring(0, sValue.IndexOf(",")));
				ToolParam.Add(sValue.Substring(sValue.IndexOf(",") + 1));
			}
		}
		if (ToolLists.Count <= 0)
		{
			return;
		}
		int Items = ToolLists.Count;
		string FuncID = "";
		DBClass DBCLS = new DBClass();
		for (int i = 0; i < Items; i++)
		{
			UltraLabel LT = new UltraLabel();
			LT.Name = i.ToString();
			LT.Text = ToolLists[i].ToString();
			LT.Font = new Font("細明體", 11.25f);
			LT.Click += LT_Click;
			LT.Width = 180;
			LT.HotTracking = true;
			LT.HotTrackAppearance.ForeColor = Color.FromArgb(0, 102, 153);
			LT.HotTrackAppearance.Cursor = Cursors.Hand;
			LT.HotTrackAppearance.FontData.Underline = DefaultableBoolean.True;
			if (i >= 10)
			{
				FuncID = "F00600" + (i + 1);
				LT.Location = new Point(260, 10 + 30 * (i - 10));
			}
			else
			{
				FuncID = "F006000" + (i + 1);
				if (i + 1 == 10)
				{
					FuncID = "F00600" + (i + 1);
				}
				LT.Location = new Point(50, 10 + 30 * i);
			}
			DBCLS.ImportFuncListAddOn(FuncID, ToolLists[i].ToString(), i.ToString());
			DBCLS.ExecuteCommand("Delete WinUserFuncs where UserID='" + F_UserID + "' and FuncID='" + FuncID + "'");
			DBCLS.ExecuteCommand("Insert into WinUserFuncs (UserID,FuncID) values ('" + F_UserID + "','" + FuncID + "')");
			pnl_Components.Controls.Add(LT);
			PictureBox p1 = new PictureBox();
			p1.Name = "PIC" + i;
			p1.Left = LT.Left - 30;
			p1.Top = LT.Top - 5;
			p1.Image = picPlugIn.Image;
			p1.Size = new Size(20, 20);
			pnl_Components.Controls.Add(p1);
		}
	}

	private void LT_Click(object sender, EventArgs e)
	{
		string PowerRight = "";
		string sName = (sender as UltraLabel).Name;
		int iMenuIndex = Convert.ToInt32(sName);
		string sCmd = ToolParam[iMenuIndex].ToString();
		if (!(sCmd.Substring(0, 1) == "[") || !(sCmd.Substring(sCmd.Length - 1, 1) == "]"))
		{
			SysUser oSysUser = new SysUser();
			string DBName = oSysUser.GetSysUserDatabaseName(F_UserID);
			if (sCmd.IndexOf("%PJ") > -1)
			{
				sCmd = sCmd.Replace("%PJ", "ADDON");
			}
			if (sCmd.IndexOf("%DB") > -1)
			{
				sCmd = sCmd.Replace("%DB", DBName);
			}
			if (sCmd.IndexOf("%UID") > -1)
			{
				sCmd = sCmd.Replace("%UID", F_UserID);
			}
			string sPath = ((sCmd.IndexOf(" ") > -1) ? sCmd.Substring(0, sCmd.IndexOf(" ")) : sCmd);
			string sParameters = ((sCmd.IndexOf(" ") > -1) ? sCmd.Substring(sCmd.IndexOf(" ")) : "");
			PowerRight = sCmd.Substring(sCmd.Length - 8, 8);
			if (!DBClass.ChkAuthority(F_UserID, PowerRight.Trim()))
			{
				MessageBox.Show(this, DBClass.GetFuncName(PowerRight) + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			GC.Collect();
			ShellExecute SHExe = new ShellExecute();
			SHExe.OwnerHandle = base.Handle;
			SHExe.Path = sPath;
			SHExe.Parameters = sParameters;
			SHExe.Execute();
			SHExe = null;
		}
	}

	private void Btn_Splt_Click(object sender, EventArgs e)
	{
		if (LeftPanel.Width == 0)
		{
			LeftPanel.Width = 160;
			Btn_Splt.Appearance.ImageBackground = iglst_splt_Btn.Images[0];
		}
		else
		{
			LeftPanel.Width = 0;
			Btn_Splt.Appearance.ImageBackground = iglst_splt_Btn.Images[2];
		}
	}

	private void FormSysPlugin_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormSysPlugin");
		}
	}

	private void FormSysPlugin_Resize(object sender, EventArgs e)
	{
		lock (this)
		{
			int TotalH = pnl_spliter.Height;
			int iHeight = (TotalH - 3 - 3 - 57) / 2;
			ssp_Upper.Height = iHeight;
			ssp_Lower.Height = iHeight;
		}
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		FormSysPlugin_Load(null, null);
	}

	private void linkLabel1_Click(object sender, EventArgs e)
	{
		ShellExecute SHExe = new ShellExecute();
		SHExe.OwnerHandle = base.Handle;
		SHExe.Path = "http://pcces.archnowledge.com/CSI/Default.aspx?FunID=Fun_12_6";
		SHExe.Path = "https://pcces.pcc.gov.tw/CSI/Default.aspx?FunID=Fun_12_6";
		SHExe.Execute();
	}

	private void panel1_Resize(object sender, EventArgs e)
	{
		FormSysPlugin_Resize(sender, e);
	}
}
