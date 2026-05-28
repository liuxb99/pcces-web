using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.STDClass;
using AxThreed;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.SysMaintain;

public class frmSysMaintain : Form
{
	private string F_MainDeptCode_G = "";

	private string F_MainDeptName_G = "";

	private string F_PccesCode_D = "";

	private string F_PccesName_D = "";

	private string F_PccesUnit_D = "";

	private string F_PubCode_D = "";

	private string F_Invoice_No = "";

	private string F_Title = "";

	private bool IsRegistered;

	private string UserID;

	private string UserName = "";

	private string F_FunctionName = "SysMaintain";

	private string ServerName = "localhost";

	private IContainer components;

	private Panel pnl_spliter;

	private UltraButton Btn_Splt;

	private AxSSPanel ssp_Lower;

	private AxSSPanel ssp_Bottom;

	private AxSSPanel ssp_Upper;

	private AxSSPanel ssp_Top;

	private Panel panel1;

	private Panel panel2;

	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	public UltraTabPageControl Tab_A;

	public UltraTabPageControl Tab_B;

	public UltraTabPageControl Tab_C;

	private UltraLabel ultraLabel1;

	public UltraTabPageControl Tab_D;

	private Panel panel3;

	private Panel panel4;

	private Panel panel5;

	private Panel panel6;

	private Panel panel7;

	private Panel panel8;

	private Panel PNL_CHD_A;

	private Panel PNL_CHD_B;

	private Panel PNL_CHD_C;

	private Panel PNL_CHD_D;

	private Panel PNL_CHD_E;

	private Panel PNL_CHD_F;

	private ImageList iglst_splt_Btn;

	private Panel LeftPanel;

	public UltraTabPageControl Tab_E;

	private UltraTabPageControl Tab_F;

	private OnlineList onlineList1;

	public FunctionButtons functionButtons1;

	public UltraTabPageControl Tab_G;

	private Panel panel9;

	private Panel PNL_CHD_G;

	private Panel panel11;

	private Panel PNL_CHD_I;

	public UltraTabPageControl Tab_I;

	private UltraTabPageControl Tab_J;

	private Panel panel12;

	private Panel panel10;

	private UltraLabel ultraLabel3;

	private UltraLabel lbl_B;

	private UltraLabel lbl_C;

	private UltraLabel lbl_D;

	private UltraLabel lbl_E;

	private UltraLabel lbl_A;

	private UltraLabel lbl_F;

	private UltraLabel lbl_I;

	private UltraLabel lbl_G;

	public UltraTabPageControl Tab_Home;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel6;

	private UltraButton BtnGoHomeB;

	private UltraButton BtnGoHomeC;

	private UltraButton BtnGoHomeD;

	private UltraButton BtnGoHomeE;

	private UltraLabel ultraLabel7;

	private UltraButton BtnGoHomeF;

	private UltraLabel ultraLabel8;

	private UltraButton BtnGoHomeA;

	private UltraLabel ultraLabel9;

	private UltraButton BtnGoHomeI;

	private UltraLabel ultraLabel10;

	private UltraButton BtnGoHomeG;

	private UltraLabel ultraLabel11;

	private UltraButton BtnGoHomeJ;

	private Panel PNL_CHD_J;

	private UltraLabel ultraLabel12;

	private UltraLabel ultraLabel13;

	private UltraLabel lbl_Z;

	public UltraTabPageControl Tab_Z;

	private Panel panel13;

	private UltraButton ultraButton1;

	private UltraLabel ultraLabel14;

	private Panel PNL_CHD_Z;

	private UltraLabel lbl_CommonModule;

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
			return IsRegistered;
		}
		set
		{
			IsRegistered = value;
		}
	}

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

	public string _UserName
	{
		get
		{
			return UserName;
		}
		set
		{
			UserName = value;
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
			return ServerName;
		}
		set
		{
			ServerName = value;
		}
	}

	public frmSysMaintain()
	{
		InitializeComponent();
		functionButtons1.ButtonOwner = LeftPanelStatus.SystemMain;
	}

	private void frmSysMaintain_Load(object sender, EventArgs e)
	{
		base.ParentForm.Text = "PCCES Win 4.3 【系統維護】";
		functionButtons1._UserID = UserID;
		functionButtons1._UserName = UserName;
		functionButtons1._ServerName = ServerName;
		functionButtons1._ActiveFunction = "SYSMAINTAIN";
		onlineList1.Disconnect();
		onlineList1._UserID = UserID;
		onlineList1._UserName = UserName;
		onlineList1._ServerName = ServerName;
		onlineList1._FunctionName = F_FunctionName;
		onlineList1._HasRegistered = IsRegistered;
		onlineList1.Connect();
		bool IsCanChangeDB = PubTools.GetAppSet_Bool("CanChangeDataBase");
		bool IsUseSystemLog = PubTools.GetAppSet_Bool("UseSystemLog");
		if (!IsCanChangeDB)
		{
			Tab_G.Tab.Visible = false;
		}
		if (!IsUseSystemLog)
		{
			lbl_F.Visible = false;
		}
		if (PubTools.GetAppSet_Bool("OptionSet"))
		{
			lbl_Z.Visible = true;
		}
		else
		{
			lbl_Z.Visible = false;
		}
	}

	private void frmSysMaintain_Resize(object sender, EventArgs e)
	{
		int TotalH = pnl_spliter.Height;
		int iHeight = (TotalH - 3 - 3 - 57) / 2;
		ssp_Upper.Height = iHeight;
		ssp_Lower.Height = iHeight;
	}

	private void Tab_Ctrl_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
	{
		if (Tab_B.Tab.Active)
		{
			if (!DBClass.ChkAuthority(UserID, "F0010001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F0010001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Tab_Home.Tab.Selected = true;
			}
			else if (PNL_CHD_B.Controls.Count == 0)
			{
				FormSys_B FM_SYS_B = new FormSys_B();
				FM_SYS_B._UserID = _UserID;
				PNL_CHD_B.Controls.Clear();
				FM_SYS_B.Dock = DockStyle.Fill;
				PNL_CHD_B.Controls.Add(FM_SYS_B);
			}
			else
			{
				(PNL_CHD_B.Controls[0] as FormSys_B).ReloadData();
			}
		}
		else if (Tab_C.Tab.Active)
		{
			if (!DBClass.ChkAuthority(UserID, "F0010002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F0010002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Tab_Home.Tab.Selected = true;
			}
			else if (PNL_CHD_C.Controls.Count == 0)
			{
				FormSys_C FM_SYS_C = new FormSys_C();
				FM_SYS_C._UserID = _UserID;
				PNL_CHD_C.Controls.Clear();
				FM_SYS_C.Dock = DockStyle.Fill;
				PNL_CHD_C.Controls.Add(FM_SYS_C);
			}
			else
			{
				(PNL_CHD_C.Controls[0] as FormSys_C).ReloadData();
			}
		}
		else if (Tab_D.Tab.Active)
		{
			if (!DBClass.ChkAuthority(UserID, "F0010003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F0010003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Tab_Home.Tab.Selected = true;
			}
			else if (PNL_CHD_D.Controls.Count == 0)
			{
				FormSys_D FM_SYS_D = new FormSys_D();
				FM_SYS_D._UserID = _UserID;
				PNL_CHD_D.Controls.Clear();
				FM_SYS_D.Dock = DockStyle.Fill;
				PNL_CHD_D.Controls.Add(FM_SYS_D);
			}
			else
			{
				(PNL_CHD_D.Controls[0] as FormSys_D).ReloadDara();
			}
		}
		else if (Tab_E.Tab.Active)
		{
			if (!DBClass.ChkAuthority(UserID, "F0010004"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F0010004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Tab_Home.Tab.Selected = true;
			}
			else if (PNL_CHD_E.Controls.Count == 0)
			{
				FormSys_E FM_SYS_E = new FormSys_E();
				FM_SYS_E._UserID = _UserID;
				PNL_CHD_E.Controls.Clear();
				FM_SYS_E.Dock = DockStyle.Fill;
				PNL_CHD_E.Controls.Add(FM_SYS_E);
			}
			else
			{
				(PNL_CHD_E.Controls[0] as FormSys_E).ReloadData();
			}
		}
		else if (Tab_F.Tab.Active)
		{
			if (UserID != "PccesUser" && !DBClass.ChkAuthority(UserID, "F0010005"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F0010005") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Tab_Home.Tab.Selected = true;
			}
			else if (PNL_CHD_F.Controls.Count == 0)
			{
				FormSys_F FM_SYS_F = new FormSys_F();
				FM_SYS_F._UserID = _UserID;
				PNL_CHD_F.Controls.Clear();
				FM_SYS_F.Dock = DockStyle.Fill;
				PNL_CHD_F.Controls.Add(FM_SYS_F);
			}
		}
		else if (Tab_A.Tab.Active)
		{
			if (UserID != "PccesUser" && !DBClass.ChkAuthority(UserID, "F0010006"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F0010006") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Tab_Home.Tab.Selected = true;
			}
			else if (PNL_CHD_A.Controls.Count == 0)
			{
				FormSys_A FM_SYS_A = new FormSys_A();
				FM_SYS_A._UserID = _UserID;
				PNL_CHD_A.Controls.Clear();
				FM_SYS_A.Dock = DockStyle.Fill;
				PNL_CHD_A.Controls.Add(FM_SYS_A);
			}
		}
		else if (Tab_G.Tab.Active)
		{
			if (UserID != "PccesUser" && !DBClass.ChkAuthority(UserID, "F0010007"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F0010007") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Tab_Home.Tab.Selected = true;
			}
			else if (PNL_CHD_G.Controls.Count == 0)
			{
				FormSys_G FM_SYS_G = new FormSys_G();
				FM_SYS_G._UserID = _UserID;
				PNL_CHD_G.Controls.Clear();
				FM_SYS_G.Dock = DockStyle.Fill;
				PNL_CHD_G.Controls.Add(FM_SYS_G);
			}
		}
		else if (Tab_I.Tab.Active)
		{
			if (UserID != "PccesUser" && !DBClass.ChkAuthority(UserID, "F0010009"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F0010009") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Tab_Home.Tab.Selected = true;
			}
			else if (PNL_CHD_I.Controls.Count == 0)
			{
				FormSys_I FM_SYS_I = new FormSys_I();
				FM_SYS_I._UserID = _UserID;
				PNL_CHD_I.Controls.Clear();
				FM_SYS_I.Dock = DockStyle.Fill;
				PNL_CHD_I.Controls.Add(FM_SYS_I);
			}
		}
		else if (Tab_Z.Tab.Active)
		{
			if (UserID != "PccesUser" && !DBClass.ChkAuthority(UserID, "F0010999"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F0010999") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Tab_Home.Tab.Selected = true;
			}
			else if (PNL_CHD_Z.Controls.Count == 0)
			{
				FormSys_Z FM_SYS_Z = new FormSys_Z();
				FM_SYS_Z._UserID = _UserID;
				PNL_CHD_Z.Controls.Clear();
				FM_SYS_Z.Dock = DockStyle.Fill;
				PNL_CHD_Z.Controls.Add(FM_SYS_Z);
			}
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

	private void tabLabel_Click(object sender, EventArgs e)
	{
		string senderName = ((UltraLabel)sender).Name;
		string tabNumber = senderName.Substring(senderName.Length - 1);
		UltraTabPageControl selectedTab = (UltraTabPageControl)Tab_Ctrl.Controls["Tab_" + tabNumber];
		if (selectedTab != null)
		{
			selectedTab.Tab.Selected = true;
		}
	}

	private void lbl_G_Click(object sender, EventArgs e)
	{
		bool IsUseNew = PubTools.GetAppSet_Bool("UseNewChangDataBase");
		bool IsCanChange = PubTools.GetAppSet_Bool("CanChangeDataBase");
		if (!IsUseNew || !IsCanChange)
		{
			MessageBox.Show(this, "目前的程式設定狀態，不能使用切換資料庫，\n請洽網管人員或 PCCES 客服！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else
		{
			Tab_G.Tab.Selected = true;
		}
	}

	private void BtnGoHome_Click(object sender, EventArgs e)
	{
		Tab_Home.Tab.Selected = true;
	}

	private void ultraLabel13_Click(object sender, EventArgs e)
	{
		string sWarning = "確定要結束系統維護 ?";
		if (MessageBox.Show(this, sWarning, "系統維護", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			if (base.ParentForm is frmPccesMain Parent)
			{
				Parent.LeftPanel.Width = 160;
				Parent.UpdateMenu();
			}
			Close();
		}
	}

	private void lbl_CommonModule_Click(object sender, EventArgs e)
	{
		FormModuleSetup FM_ModuleSetup = new FormModuleSetup();
		if (FM_ModuleSetup.ShowDialog() == DialogResult.OK)
		{
			functionButtons1.OPEN_MODE_CHECK();
		}
		FM_ModuleSetup.Close();
		FM_ModuleSetup.Dispose();
		FM_ModuleSetup = null;
	}

	private void Tab_Ctrl_Resize(object sender, EventArgs e)
	{
		frmSysMaintain_Resize(sender, e);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.SysMaintain.frmSysMaintain));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab7 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab8 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab9 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab10 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab11 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.Appearance appearance61 = new Infragistics.Win.Appearance();
		this.Tab_Home = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.lbl_CommonModule = new Infragistics.Win.Misc.UltraLabel();
		this.lbl_Z = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.lbl_G = new Infragistics.Win.Misc.UltraLabel();
		this.lbl_I = new Infragistics.Win.Misc.UltraLabel();
		this.lbl_A = new Infragistics.Win.Misc.UltraLabel();
		this.lbl_F = new Infragistics.Win.Misc.UltraLabel();
		this.lbl_E = new Infragistics.Win.Misc.UltraLabel();
		this.lbl_D = new Infragistics.Win.Misc.UltraLabel();
		this.lbl_C = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.lbl_B = new Infragistics.Win.Misc.UltraLabel();
		this.panel10 = new System.Windows.Forms.Panel();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.PNL_CHD_B = new System.Windows.Forms.Panel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.BtnGoHomeB = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_C = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.PNL_CHD_C = new System.Windows.Forms.Panel();
		this.panel5 = new System.Windows.Forms.Panel();
		this.BtnGoHomeC = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_D = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.PNL_CHD_D = new System.Windows.Forms.Panel();
		this.panel6 = new System.Windows.Forms.Panel();
		this.BtnGoHomeD = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_E = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.PNL_CHD_E = new System.Windows.Forms.Panel();
		this.panel7 = new System.Windows.Forms.Panel();
		this.BtnGoHomeE = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_F = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.PNL_CHD_F = new System.Windows.Forms.Panel();
		this.panel8 = new System.Windows.Forms.Panel();
		this.BtnGoHomeF = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.PNL_CHD_A = new System.Windows.Forms.Panel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.BtnGoHomeA = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_I = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.PNL_CHD_I = new System.Windows.Forms.Panel();
		this.panel11 = new System.Windows.Forms.Panel();
		this.BtnGoHomeI = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_G = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.PNL_CHD_G = new System.Windows.Forms.Panel();
		this.panel9 = new System.Windows.Forms.Panel();
		this.BtnGoHomeG = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_J = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.PNL_CHD_J = new System.Windows.Forms.Panel();
		this.panel12 = new System.Windows.Forms.Panel();
		this.BtnGoHomeJ = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_Z = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.PNL_CHD_Z = new System.Windows.Forms.Panel();
		this.panel13 = new System.Windows.Forms.Panel();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.pnl_spliter = new System.Windows.Forms.Panel();
		this.Btn_Splt = new Infragistics.Win.Misc.UltraButton();
		this.ssp_Lower = new AxThreed.AxSSPanel();
		this.ssp_Bottom = new AxThreed.AxSSPanel();
		this.ssp_Upper = new AxThreed.AxSSPanel();
		this.ssp_Top = new AxThreed.AxSSPanel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.panel2 = new System.Windows.Forms.Panel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.iglst_splt_Btn = new System.Windows.Forms.ImageList(this.components);
		this.LeftPanel = new System.Windows.Forms.Panel();
		this.functionButtons1 = new Archnowledge.Pcces.PccesMain.ArchControls.FunctionButtons();
		this.onlineList1 = new Archnowledge.Pcces.PccesMain.ArchControls.OnlineList();
		this.Tab_Home.SuspendLayout();
		this.Tab_B.SuspendLayout();
		this.panel4.SuspendLayout();
		this.Tab_C.SuspendLayout();
		this.panel5.SuspendLayout();
		this.Tab_D.SuspendLayout();
		this.panel6.SuspendLayout();
		this.Tab_E.SuspendLayout();
		this.panel7.SuspendLayout();
		this.Tab_F.SuspendLayout();
		this.panel8.SuspendLayout();
		this.Tab_A.SuspendLayout();
		this.panel3.SuspendLayout();
		this.Tab_I.SuspendLayout();
		this.panel11.SuspendLayout();
		this.Tab_G.SuspendLayout();
		this.panel9.SuspendLayout();
		this.Tab_J.SuspendLayout();
		this.panel12.SuspendLayout();
		this.Tab_Z.SuspendLayout();
		this.panel13.SuspendLayout();
		this.pnl_spliter.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ssp_Lower).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Bottom).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Upper).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Top).BeginInit();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		this.panel2.SuspendLayout();
		this.LeftPanel.SuspendLayout();
		base.SuspendLayout();
		this.Tab_Home.Controls.Add(this.lbl_CommonModule);
		this.Tab_Home.Controls.Add(this.lbl_Z);
		this.Tab_Home.Controls.Add(this.ultraLabel13);
		this.Tab_Home.Controls.Add(this.ultraLabel12);
		this.Tab_Home.Controls.Add(this.lbl_G);
		this.Tab_Home.Controls.Add(this.lbl_I);
		this.Tab_Home.Controls.Add(this.lbl_A);
		this.Tab_Home.Controls.Add(this.lbl_F);
		this.Tab_Home.Controls.Add(this.lbl_E);
		this.Tab_Home.Controls.Add(this.lbl_D);
		this.Tab_Home.Controls.Add(this.lbl_C);
		this.Tab_Home.Controls.Add(this.ultraLabel3);
		this.Tab_Home.Controls.Add(this.lbl_B);
		this.Tab_Home.Controls.Add(this.panel10);
		this.Tab_Home.Location = new System.Drawing.Point(0, 0);
		this.Tab_Home.Name = "Tab_Home";
		this.Tab_Home.Size = new System.Drawing.Size(625, 546);
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbl_CommonModule.Appearance = appearance1;
		this.lbl_CommonModule.BackColor = System.Drawing.Color.Transparent;
		appearance2.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance2.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance2.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.lbl_CommonModule.HotTrackAppearance = appearance2;
		this.lbl_CommonModule.HotTracking = true;
		this.lbl_CommonModule.ImageSize = new System.Drawing.Size(20, 20);
		this.lbl_CommonModule.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.lbl_CommonModule.Location = new System.Drawing.Point(272, 202);
		this.lbl_CommonModule.Name = "lbl_CommonModule";
		this.lbl_CommonModule.Size = new System.Drawing.Size(157, 20);
		this.lbl_CommonModule.TabIndex = 15;
		this.lbl_CommonModule.Text = " 常用模組";
		this.lbl_CommonModule.Click += new System.EventHandler(lbl_CommonModule_Click);
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbl_Z.Appearance = appearance3;
		this.lbl_Z.BackColor = System.Drawing.Color.Transparent;
		appearance4.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance4.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance4.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.lbl_Z.HotTrackAppearance = appearance4;
		this.lbl_Z.HotTracking = true;
		this.lbl_Z.ImageSize = new System.Drawing.Size(20, 20);
		this.lbl_Z.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.lbl_Z.Location = new System.Drawing.Point(50, 292);
		this.lbl_Z.Name = "lbl_Z";
		this.lbl_Z.Size = new System.Drawing.Size(103, 20);
		this.lbl_Z.TabIndex = 14;
		this.lbl_Z.Text = " 選項/設定";
		this.lbl_Z.Visible = false;
		this.lbl_Z.Click += new System.EventHandler(tabLabel_Click);
		this.ultraLabel13.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance5.Image = resources.GetObject("appearance5.Image");
		appearance5.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel13.Appearance = appearance5;
		this.ultraLabel13.BackColor = System.Drawing.Color.Transparent;
		appearance6.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance6.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance6.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel13.HotTrackAppearance = appearance6;
		this.ultraLabel13.HotTracking = true;
		this.ultraLabel13.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraLabel13.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.ultraLabel13.Location = new System.Drawing.Point(486, 40);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(124, 24);
		this.ultraLabel13.TabIndex = 13;
		this.ultraLabel13.Text = "結束系統維護";
		this.ultraLabel13.Click += new System.EventHandler(ultraLabel13_Click);
		this.ultraLabel12.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance7.BorderColor = System.Drawing.Color.FromArgb(255, 128, 0);
		this.ultraLabel12.Appearance = appearance7;
		this.ultraLabel12.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.ultraLabel12.Location = new System.Drawing.Point(13, 75);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(596, 2);
		this.ultraLabel12.TabIndex = 12;
		appearance8.Image = resources.GetObject("appearance8.Image");
		appearance8.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbl_G.Appearance = appearance8;
		this.lbl_G.BackColor = System.Drawing.Color.Transparent;
		appearance9.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance9.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance9.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.lbl_G.HotTrackAppearance = appearance9;
		this.lbl_G.HotTracking = true;
		this.lbl_G.ImageSize = new System.Drawing.Size(20, 20);
		this.lbl_G.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.lbl_G.Location = new System.Drawing.Point(272, 168);
		this.lbl_G.Name = "lbl_G";
		this.lbl_G.Size = new System.Drawing.Size(157, 20);
		this.lbl_G.TabIndex = 11;
		this.lbl_G.Text = " 資料庫管理及切換";
		this.lbl_G.Click += new System.EventHandler(lbl_G_Click);
		appearance10.Image = resources.GetObject("appearance10.Image");
		appearance10.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbl_I.Appearance = appearance10;
		this.lbl_I.BackColor = System.Drawing.Color.Transparent;
		appearance11.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance11.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance11.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.lbl_I.HotTrackAppearance = appearance11;
		this.lbl_I.HotTracking = true;
		this.lbl_I.ImageSize = new System.Drawing.Size(20, 20);
		this.lbl_I.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.lbl_I.Location = new System.Drawing.Point(272, 134);
		this.lbl_I.Name = "lbl_I";
		this.lbl_I.Size = new System.Drawing.Size(126, 20);
		this.lbl_I.TabIndex = 10;
		this.lbl_I.Text = " 專案權限設定";
		this.lbl_I.Click += new System.EventHandler(tabLabel_Click);
		appearance12.Image = resources.GetObject("appearance12.Image");
		appearance12.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbl_A.Appearance = appearance12;
		this.lbl_A.BackColor = System.Drawing.Color.Transparent;
		appearance13.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance13.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance13.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.lbl_A.HotTrackAppearance = appearance13;
		this.lbl_A.HotTracking = true;
		this.lbl_A.ImageSize = new System.Drawing.Size(20, 20);
		this.lbl_A.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.lbl_A.Location = new System.Drawing.Point(272, 100);
		this.lbl_A.Name = "lbl_A";
		this.lbl_A.Size = new System.Drawing.Size(126, 20);
		this.lbl_A.TabIndex = 9;
		this.lbl_A.Text = " 帳號權限管理";
		this.lbl_A.Click += new System.EventHandler(tabLabel_Click);
		appearance14.Image = resources.GetObject("appearance14.Image");
		appearance14.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbl_F.Appearance = appearance14;
		this.lbl_F.BackColor = System.Drawing.Color.Transparent;
		appearance15.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance15.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance15.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.lbl_F.HotTrackAppearance = appearance15;
		this.lbl_F.HotTracking = true;
		this.lbl_F.ImageSize = new System.Drawing.Size(20, 20);
		this.lbl_F.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.lbl_F.Location = new System.Drawing.Point(50, 236);
		this.lbl_F.Name = "lbl_F";
		this.lbl_F.Size = new System.Drawing.Size(95, 20);
		this.lbl_F.TabIndex = 8;
		this.lbl_F.Text = " 系統訊息";
		this.lbl_F.Click += new System.EventHandler(tabLabel_Click);
		appearance16.Image = resources.GetObject("appearance16.Image");
		appearance16.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbl_E.Appearance = appearance16;
		this.lbl_E.BackColor = System.Drawing.Color.Transparent;
		appearance17.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance17.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance17.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.lbl_E.HotTrackAppearance = appearance17;
		this.lbl_E.HotTracking = true;
		this.lbl_E.ImageSize = new System.Drawing.Size(20, 20);
		this.lbl_E.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.lbl_E.Location = new System.Drawing.Point(50, 202);
		this.lbl_E.Name = "lbl_E";
		this.lbl_E.Size = new System.Drawing.Size(126, 20);
		this.lbl_E.TabIndex = 7;
		this.lbl_E.Text = " 常用字串設定";
		this.lbl_E.Click += new System.EventHandler(tabLabel_Click);
		appearance18.Image = resources.GetObject("appearance18.Image");
		appearance18.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbl_D.Appearance = appearance18;
		this.lbl_D.BackColor = System.Drawing.Color.Transparent;
		appearance19.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance19.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance19.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.lbl_D.HotTrackAppearance = appearance19;
		this.lbl_D.HotTracking = true;
		this.lbl_D.ImageSize = new System.Drawing.Size(20, 20);
		this.lbl_D.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.lbl_D.Location = new System.Drawing.Point(50, 168);
		this.lbl_D.Name = "lbl_D";
		this.lbl_D.Size = new System.Drawing.Size(126, 20);
		this.lbl_D.TabIndex = 6;
		this.lbl_D.Text = " 公司資料行情";
		this.lbl_D.Click += new System.EventHandler(tabLabel_Click);
		appearance20.Image = resources.GetObject("appearance20.Image");
		appearance20.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbl_C.Appearance = appearance20;
		this.lbl_C.BackColor = System.Drawing.Color.Transparent;
		appearance21.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance21.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance21.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.lbl_C.HotTrackAppearance = appearance21;
		this.lbl_C.HotTracking = true;
		this.lbl_C.ImageSize = new System.Drawing.Size(20, 20);
		this.lbl_C.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.lbl_C.Location = new System.Drawing.Point(50, 134);
		this.lbl_C.Name = "lbl_C";
		this.lbl_C.Size = new System.Drawing.Size(126, 20);
		this.lbl_C.TabIndex = 5;
		this.lbl_C.Text = " 廠商資料維護";
		this.lbl_C.Click += new System.EventHandler(tabLabel_Click);
		appearance22.Image = resources.GetObject("appearance22.Image");
		appearance22.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance22;
		this.ultraLabel3.BackColor = System.Drawing.Color.Transparent;
		this.ultraLabel3.Font = new System.Drawing.Font("細明體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel3.ImageSize = new System.Drawing.Size(48, 48);
		this.ultraLabel3.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.ultraLabel3.Location = new System.Drawing.Point(20, 16);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(224, 52);
		this.ultraLabel3.TabIndex = 4;
		this.ultraLabel3.Text = " 系統維護";
		appearance23.Image = resources.GetObject("appearance23.Image");
		appearance23.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbl_B.Appearance = appearance23;
		this.lbl_B.BackColor = System.Drawing.Color.Transparent;
		appearance24.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance24.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance24.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.lbl_B.HotTrackAppearance = appearance24;
		this.lbl_B.HotTracking = true;
		this.lbl_B.ImageSize = new System.Drawing.Size(20, 20);
		this.lbl_B.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.lbl_B.Location = new System.Drawing.Point(50, 100);
		this.lbl_B.Name = "lbl_B";
		this.lbl_B.Size = new System.Drawing.Size(126, 20);
		this.lbl_B.TabIndex = 3;
		this.lbl_B.Text = " 主辦單位維護";
		this.lbl_B.Click += new System.EventHandler(tabLabel_Click);
		this.panel10.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel10.Location = new System.Drawing.Point(0, 0);
		this.panel10.Name = "panel10";
		this.panel10.Size = new System.Drawing.Size(625, 4);
		this.panel10.TabIndex = 2;
		this.Tab_B.Controls.Add(this.PNL_CHD_B);
		this.Tab_B.Controls.Add(this.panel4);
		this.Tab_B.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(625, 546);
		this.PNL_CHD_B.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.PNL_CHD_B.Dock = System.Windows.Forms.DockStyle.Fill;
		this.PNL_CHD_B.Location = new System.Drawing.Point(0, 28);
		this.PNL_CHD_B.Name = "PNL_CHD_B";
		this.PNL_CHD_B.Size = new System.Drawing.Size(625, 518);
		this.PNL_CHD_B.TabIndex = 2;
		this.panel4.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel4.Controls.Add(this.BtnGoHomeB);
		this.panel4.Controls.Add(this.ultraLabel2);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel4.Location = new System.Drawing.Point(0, 0);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(625, 28);
		this.panel4.TabIndex = 1;
		this.BtnGoHomeB.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance25.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance25.ForeColor = System.Drawing.Color.White;
		appearance25.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnGoHomeB.Appearance = appearance25;
		this.BtnGoHomeB.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button;
		this.BtnGoHomeB.Font = new System.Drawing.Font("細明體", 9f);
		appearance26.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance26.ForeColor = System.Drawing.Color.Yellow;
		this.BtnGoHomeB.HotTrackAppearance = appearance26;
		this.BtnGoHomeB.HotTracking = true;
		this.BtnGoHomeB.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnGoHomeB.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnGoHomeB.Location = new System.Drawing.Point(480, 3);
		this.BtnGoHomeB.Name = "BtnGoHomeB";
		this.BtnGoHomeB.ShowFocusRect = false;
		this.BtnGoHomeB.ShowOutline = false;
		this.BtnGoHomeB.Size = new System.Drawing.Size(140, 23);
		this.BtnGoHomeB.SupportThemes = false;
		this.BtnGoHomeB.TabIndex = 1;
		this.BtnGoHomeB.Text = "返回「系統維護」";
		this.BtnGoHomeB.Click += new System.EventHandler(BtnGoHome_Click);
		appearance27.ForeColor = System.Drawing.Color.White;
		appearance27.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance27;
		this.ultraLabel2.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel2.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(152, 28);
		this.ultraLabel2.TabIndex = 0;
		this.ultraLabel2.Text = "主辦單位維護";
		this.Tab_C.Controls.Add(this.PNL_CHD_C);
		this.Tab_C.Controls.Add(this.panel5);
		this.Tab_C.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_C.Name = "Tab_C";
		this.Tab_C.Size = new System.Drawing.Size(625, 546);
		this.PNL_CHD_C.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.PNL_CHD_C.Dock = System.Windows.Forms.DockStyle.Fill;
		this.PNL_CHD_C.Location = new System.Drawing.Point(0, 28);
		this.PNL_CHD_C.Name = "PNL_CHD_C";
		this.PNL_CHD_C.Size = new System.Drawing.Size(625, 518);
		this.PNL_CHD_C.TabIndex = 2;
		this.panel5.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel5.Controls.Add(this.BtnGoHomeC);
		this.panel5.Controls.Add(this.ultraLabel4);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(625, 28);
		this.panel5.TabIndex = 1;
		this.BtnGoHomeC.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance28.Cursor = System.Windows.Forms.Cursors.Default;
		appearance28.ForeColor = System.Drawing.Color.White;
		appearance28.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnGoHomeC.Appearance = appearance28;
		this.BtnGoHomeC.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button;
		this.BtnGoHomeC.Font = new System.Drawing.Font("細明體", 9f);
		appearance29.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance29.ForeColor = System.Drawing.Color.Yellow;
		this.BtnGoHomeC.HotTrackAppearance = appearance29;
		this.BtnGoHomeC.HotTracking = true;
		this.BtnGoHomeC.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnGoHomeC.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnGoHomeC.Location = new System.Drawing.Point(480, 3);
		this.BtnGoHomeC.Name = "BtnGoHomeC";
		this.BtnGoHomeC.ShowFocusRect = false;
		this.BtnGoHomeC.ShowOutline = false;
		this.BtnGoHomeC.Size = new System.Drawing.Size(140, 23);
		this.BtnGoHomeC.SupportThemes = false;
		this.BtnGoHomeC.TabIndex = 2;
		this.BtnGoHomeC.Text = "返回「系統維護」";
		this.BtnGoHomeC.Click += new System.EventHandler(BtnGoHome_Click);
		appearance30.ForeColor = System.Drawing.Color.White;
		appearance30.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance30;
		this.ultraLabel4.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel4.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(152, 28);
		this.ultraLabel4.TabIndex = 1;
		this.ultraLabel4.Text = "廠商資料維護";
		this.Tab_D.Controls.Add(this.PNL_CHD_D);
		this.Tab_D.Controls.Add(this.panel6);
		this.Tab_D.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_D.Name = "Tab_D";
		this.Tab_D.Size = new System.Drawing.Size(625, 546);
		this.PNL_CHD_D.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.PNL_CHD_D.Dock = System.Windows.Forms.DockStyle.Fill;
		this.PNL_CHD_D.Location = new System.Drawing.Point(0, 28);
		this.PNL_CHD_D.Name = "PNL_CHD_D";
		this.PNL_CHD_D.Size = new System.Drawing.Size(625, 518);
		this.PNL_CHD_D.TabIndex = 2;
		this.panel6.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel6.Controls.Add(this.BtnGoHomeD);
		this.panel6.Controls.Add(this.ultraLabel5);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel6.Location = new System.Drawing.Point(0, 0);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(625, 28);
		this.panel6.TabIndex = 1;
		this.BtnGoHomeD.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance31.Cursor = System.Windows.Forms.Cursors.Default;
		appearance31.ForeColor = System.Drawing.Color.White;
		appearance31.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnGoHomeD.Appearance = appearance31;
		this.BtnGoHomeD.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button;
		this.BtnGoHomeD.Font = new System.Drawing.Font("細明體", 9f);
		appearance32.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance32.ForeColor = System.Drawing.Color.Yellow;
		this.BtnGoHomeD.HotTrackAppearance = appearance32;
		this.BtnGoHomeD.HotTracking = true;
		this.BtnGoHomeD.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnGoHomeD.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnGoHomeD.Location = new System.Drawing.Point(480, 3);
		this.BtnGoHomeD.Name = "BtnGoHomeD";
		this.BtnGoHomeD.ShowFocusRect = false;
		this.BtnGoHomeD.ShowOutline = false;
		this.BtnGoHomeD.Size = new System.Drawing.Size(140, 23);
		this.BtnGoHomeD.SupportThemes = false;
		this.BtnGoHomeD.TabIndex = 3;
		this.BtnGoHomeD.Text = "返回「系統維護」";
		this.BtnGoHomeD.Click += new System.EventHandler(BtnGoHome_Click);
		appearance33.ForeColor = System.Drawing.Color.White;
		appearance33.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance33;
		this.ultraLabel5.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel5.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(152, 28);
		this.ultraLabel5.TabIndex = 2;
		this.ultraLabel5.Text = "公司資料行情";
		this.Tab_E.Controls.Add(this.PNL_CHD_E);
		this.Tab_E.Controls.Add(this.panel7);
		this.Tab_E.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_E.Name = "Tab_E";
		this.Tab_E.Size = new System.Drawing.Size(625, 546);
		this.PNL_CHD_E.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.PNL_CHD_E.Dock = System.Windows.Forms.DockStyle.Fill;
		this.PNL_CHD_E.Location = new System.Drawing.Point(0, 28);
		this.PNL_CHD_E.Name = "PNL_CHD_E";
		this.PNL_CHD_E.Size = new System.Drawing.Size(625, 518);
		this.PNL_CHD_E.TabIndex = 2;
		this.panel7.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel7.Controls.Add(this.BtnGoHomeE);
		this.panel7.Controls.Add(this.ultraLabel6);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel7.Location = new System.Drawing.Point(0, 0);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(625, 28);
		this.panel7.TabIndex = 1;
		this.BtnGoHomeE.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance34.Cursor = System.Windows.Forms.Cursors.Default;
		appearance34.ForeColor = System.Drawing.Color.White;
		appearance34.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnGoHomeE.Appearance = appearance34;
		this.BtnGoHomeE.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button;
		this.BtnGoHomeE.Font = new System.Drawing.Font("細明體", 9f);
		appearance35.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance35.ForeColor = System.Drawing.Color.Yellow;
		this.BtnGoHomeE.HotTrackAppearance = appearance35;
		this.BtnGoHomeE.HotTracking = true;
		this.BtnGoHomeE.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnGoHomeE.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnGoHomeE.Location = new System.Drawing.Point(480, 3);
		this.BtnGoHomeE.Name = "BtnGoHomeE";
		this.BtnGoHomeE.ShowFocusRect = false;
		this.BtnGoHomeE.ShowOutline = false;
		this.BtnGoHomeE.Size = new System.Drawing.Size(140, 23);
		this.BtnGoHomeE.SupportThemes = false;
		this.BtnGoHomeE.TabIndex = 4;
		this.BtnGoHomeE.Text = "返回「系統維護」";
		this.BtnGoHomeE.Click += new System.EventHandler(BtnGoHome_Click);
		appearance36.ForeColor = System.Drawing.Color.White;
		appearance36.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel6.Appearance = appearance36;
		this.ultraLabel6.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel6.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(152, 28);
		this.ultraLabel6.TabIndex = 3;
		this.ultraLabel6.Text = "常用字串設定";
		this.Tab_F.Controls.Add(this.PNL_CHD_F);
		this.Tab_F.Controls.Add(this.panel8);
		this.Tab_F.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_F.Name = "Tab_F";
		this.Tab_F.Size = new System.Drawing.Size(625, 546);
		this.PNL_CHD_F.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.PNL_CHD_F.Dock = System.Windows.Forms.DockStyle.Fill;
		this.PNL_CHD_F.Location = new System.Drawing.Point(0, 28);
		this.PNL_CHD_F.Name = "PNL_CHD_F";
		this.PNL_CHD_F.Size = new System.Drawing.Size(625, 518);
		this.PNL_CHD_F.TabIndex = 2;
		this.panel8.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel8.Controls.Add(this.BtnGoHomeF);
		this.panel8.Controls.Add(this.ultraLabel7);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel8.Location = new System.Drawing.Point(0, 0);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(625, 28);
		this.panel8.TabIndex = 1;
		this.BtnGoHomeF.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance37.Cursor = System.Windows.Forms.Cursors.Default;
		appearance37.ForeColor = System.Drawing.Color.White;
		appearance37.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnGoHomeF.Appearance = appearance37;
		this.BtnGoHomeF.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button;
		this.BtnGoHomeF.Font = new System.Drawing.Font("細明體", 9f);
		appearance38.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance38.ForeColor = System.Drawing.Color.Yellow;
		this.BtnGoHomeF.HotTrackAppearance = appearance38;
		this.BtnGoHomeF.HotTracking = true;
		this.BtnGoHomeF.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnGoHomeF.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnGoHomeF.Location = new System.Drawing.Point(480, 3);
		this.BtnGoHomeF.Name = "BtnGoHomeF";
		this.BtnGoHomeF.ShowFocusRect = false;
		this.BtnGoHomeF.ShowOutline = false;
		this.BtnGoHomeF.Size = new System.Drawing.Size(140, 23);
		this.BtnGoHomeF.SupportThemes = false;
		this.BtnGoHomeF.TabIndex = 5;
		this.BtnGoHomeF.Text = "返回「系統維護」";
		this.BtnGoHomeF.Click += new System.EventHandler(BtnGoHome_Click);
		appearance39.ForeColor = System.Drawing.Color.White;
		appearance39.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel7.Appearance = appearance39;
		this.ultraLabel7.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel7.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(152, 28);
		this.ultraLabel7.TabIndex = 4;
		this.ultraLabel7.Text = "系統訊息";
		this.Tab_A.Controls.Add(this.PNL_CHD_A);
		this.Tab_A.Controls.Add(this.panel3);
		this.Tab_A.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(625, 546);
		this.PNL_CHD_A.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.PNL_CHD_A.Dock = System.Windows.Forms.DockStyle.Fill;
		this.PNL_CHD_A.Location = new System.Drawing.Point(0, 28);
		this.PNL_CHD_A.Name = "PNL_CHD_A";
		this.PNL_CHD_A.Size = new System.Drawing.Size(625, 518);
		this.PNL_CHD_A.TabIndex = 1;
		this.panel3.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel3.Controls.Add(this.BtnGoHomeA);
		this.panel3.Controls.Add(this.ultraLabel8);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel3.Location = new System.Drawing.Point(0, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(625, 28);
		this.panel3.TabIndex = 0;
		this.BtnGoHomeA.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance40.Cursor = System.Windows.Forms.Cursors.Default;
		appearance40.ForeColor = System.Drawing.Color.White;
		appearance40.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnGoHomeA.Appearance = appearance40;
		this.BtnGoHomeA.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button;
		this.BtnGoHomeA.Font = new System.Drawing.Font("細明體", 9f);
		appearance41.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance41.ForeColor = System.Drawing.Color.Yellow;
		this.BtnGoHomeA.HotTrackAppearance = appearance41;
		this.BtnGoHomeA.HotTracking = true;
		this.BtnGoHomeA.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnGoHomeA.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnGoHomeA.Location = new System.Drawing.Point(480, 3);
		this.BtnGoHomeA.Name = "BtnGoHomeA";
		this.BtnGoHomeA.ShowFocusRect = false;
		this.BtnGoHomeA.ShowOutline = false;
		this.BtnGoHomeA.Size = new System.Drawing.Size(140, 23);
		this.BtnGoHomeA.SupportThemes = false;
		this.BtnGoHomeA.TabIndex = 6;
		this.BtnGoHomeA.Text = "返回「系統維護」";
		this.BtnGoHomeA.Click += new System.EventHandler(BtnGoHome_Click);
		appearance42.ForeColor = System.Drawing.Color.White;
		appearance42.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel8.Appearance = appearance42;
		this.ultraLabel8.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel8.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(152, 28);
		this.ultraLabel8.TabIndex = 5;
		this.ultraLabel8.Text = "帳號權限管理";
		this.Tab_I.Controls.Add(this.PNL_CHD_I);
		this.Tab_I.Controls.Add(this.panel11);
		this.Tab_I.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_I.Name = "Tab_I";
		this.Tab_I.Size = new System.Drawing.Size(625, 546);
		this.PNL_CHD_I.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.PNL_CHD_I.Dock = System.Windows.Forms.DockStyle.Fill;
		this.PNL_CHD_I.Location = new System.Drawing.Point(0, 28);
		this.PNL_CHD_I.Name = "PNL_CHD_I";
		this.PNL_CHD_I.Size = new System.Drawing.Size(625, 518);
		this.PNL_CHD_I.TabIndex = 2;
		this.panel11.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel11.Controls.Add(this.BtnGoHomeI);
		this.panel11.Controls.Add(this.ultraLabel9);
		this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel11.Location = new System.Drawing.Point(0, 0);
		this.panel11.Name = "panel11";
		this.panel11.Size = new System.Drawing.Size(625, 28);
		this.panel11.TabIndex = 1;
		this.BtnGoHomeI.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance43.Cursor = System.Windows.Forms.Cursors.Default;
		appearance43.ForeColor = System.Drawing.Color.White;
		appearance43.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnGoHomeI.Appearance = appearance43;
		this.BtnGoHomeI.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button;
		this.BtnGoHomeI.Font = new System.Drawing.Font("細明體", 9f);
		appearance44.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance44.ForeColor = System.Drawing.Color.Yellow;
		this.BtnGoHomeI.HotTrackAppearance = appearance44;
		this.BtnGoHomeI.HotTracking = true;
		this.BtnGoHomeI.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnGoHomeI.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnGoHomeI.Location = new System.Drawing.Point(480, 3);
		this.BtnGoHomeI.Name = "BtnGoHomeI";
		this.BtnGoHomeI.ShowFocusRect = false;
		this.BtnGoHomeI.ShowOutline = false;
		this.BtnGoHomeI.Size = new System.Drawing.Size(140, 23);
		this.BtnGoHomeI.SupportThemes = false;
		this.BtnGoHomeI.TabIndex = 7;
		this.BtnGoHomeI.Text = "返回「系統維護」";
		this.BtnGoHomeI.Click += new System.EventHandler(BtnGoHome_Click);
		appearance45.ForeColor = System.Drawing.Color.White;
		appearance45.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel9.Appearance = appearance45;
		this.ultraLabel9.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel9.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(152, 28);
		this.ultraLabel9.TabIndex = 6;
		this.ultraLabel9.Text = "專案權限管理";
		this.Tab_G.Controls.Add(this.PNL_CHD_G);
		this.Tab_G.Controls.Add(this.panel9);
		this.Tab_G.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_G.Name = "Tab_G";
		this.Tab_G.Size = new System.Drawing.Size(625, 546);
		this.PNL_CHD_G.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.PNL_CHD_G.Dock = System.Windows.Forms.DockStyle.Fill;
		this.PNL_CHD_G.Location = new System.Drawing.Point(0, 28);
		this.PNL_CHD_G.Name = "PNL_CHD_G";
		this.PNL_CHD_G.Size = new System.Drawing.Size(625, 518);
		this.PNL_CHD_G.TabIndex = 2;
		this.panel9.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel9.Controls.Add(this.BtnGoHomeG);
		this.panel9.Controls.Add(this.ultraLabel10);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel9.Location = new System.Drawing.Point(0, 0);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(625, 28);
		this.panel9.TabIndex = 1;
		this.BtnGoHomeG.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance46.Cursor = System.Windows.Forms.Cursors.Default;
		appearance46.ForeColor = System.Drawing.Color.White;
		appearance46.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnGoHomeG.Appearance = appearance46;
		this.BtnGoHomeG.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button;
		this.BtnGoHomeG.Font = new System.Drawing.Font("細明體", 9f);
		appearance47.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance47.ForeColor = System.Drawing.Color.Yellow;
		this.BtnGoHomeG.HotTrackAppearance = appearance47;
		this.BtnGoHomeG.HotTracking = true;
		this.BtnGoHomeG.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnGoHomeG.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnGoHomeG.Location = new System.Drawing.Point(480, 3);
		this.BtnGoHomeG.Name = "BtnGoHomeG";
		this.BtnGoHomeG.ShowFocusRect = false;
		this.BtnGoHomeG.ShowOutline = false;
		this.BtnGoHomeG.Size = new System.Drawing.Size(140, 23);
		this.BtnGoHomeG.SupportThemes = false;
		this.BtnGoHomeG.TabIndex = 8;
		this.BtnGoHomeG.Text = "返回「系統維護」";
		this.BtnGoHomeG.Click += new System.EventHandler(BtnGoHome_Click);
		appearance48.ForeColor = System.Drawing.Color.White;
		appearance48.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel10.Appearance = appearance48;
		this.ultraLabel10.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel10.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(152, 28);
		this.ultraLabel10.TabIndex = 7;
		this.ultraLabel10.Text = "資料庫管理及切換";
		this.Tab_J.Controls.Add(this.PNL_CHD_J);
		this.Tab_J.Controls.Add(this.panel12);
		this.Tab_J.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_J.Name = "Tab_J";
		this.Tab_J.Size = new System.Drawing.Size(625, 546);
		this.PNL_CHD_J.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.PNL_CHD_J.Dock = System.Windows.Forms.DockStyle.Fill;
		this.PNL_CHD_J.Location = new System.Drawing.Point(0, 28);
		this.PNL_CHD_J.Name = "PNL_CHD_J";
		this.PNL_CHD_J.Size = new System.Drawing.Size(625, 518);
		this.PNL_CHD_J.TabIndex = 3;
		this.panel12.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel12.Controls.Add(this.BtnGoHomeJ);
		this.panel12.Controls.Add(this.ultraLabel11);
		this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel12.Location = new System.Drawing.Point(0, 0);
		this.panel12.Name = "panel12";
		this.panel12.Size = new System.Drawing.Size(625, 28);
		this.panel12.TabIndex = 2;
		this.BtnGoHomeJ.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance49.Cursor = System.Windows.Forms.Cursors.Default;
		appearance49.ForeColor = System.Drawing.Color.White;
		appearance49.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnGoHomeJ.Appearance = appearance49;
		this.BtnGoHomeJ.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button;
		this.BtnGoHomeJ.Font = new System.Drawing.Font("細明體", 9f);
		appearance50.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance50.ForeColor = System.Drawing.Color.Yellow;
		this.BtnGoHomeJ.HotTrackAppearance = appearance50;
		this.BtnGoHomeJ.HotTracking = true;
		this.BtnGoHomeJ.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnGoHomeJ.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnGoHomeJ.Location = new System.Drawing.Point(480, 3);
		this.BtnGoHomeJ.Name = "BtnGoHomeJ";
		this.BtnGoHomeJ.ShowFocusRect = false;
		this.BtnGoHomeJ.ShowOutline = false;
		this.BtnGoHomeJ.Size = new System.Drawing.Size(140, 23);
		this.BtnGoHomeJ.SupportThemes = false;
		this.BtnGoHomeJ.TabIndex = 9;
		this.BtnGoHomeJ.Text = "返回「系統維護」";
		this.BtnGoHomeJ.Click += new System.EventHandler(BtnGoHome_Click);
		appearance51.ForeColor = System.Drawing.Color.White;
		appearance51.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel11.Appearance = appearance51;
		this.ultraLabel11.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel11.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel11.Name = "ultraLabel11";
		this.ultraLabel11.Size = new System.Drawing.Size(152, 28);
		this.ultraLabel11.TabIndex = 8;
		this.ultraLabel11.Text = "線上更新";
		this.Tab_Z.Controls.Add(this.PNL_CHD_Z);
		this.Tab_Z.Controls.Add(this.panel13);
		this.Tab_Z.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_Z.Name = "Tab_Z";
		this.Tab_Z.Size = new System.Drawing.Size(625, 546);
		this.PNL_CHD_Z.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.PNL_CHD_Z.Dock = System.Windows.Forms.DockStyle.Fill;
		this.PNL_CHD_Z.Location = new System.Drawing.Point(0, 28);
		this.PNL_CHD_Z.Name = "PNL_CHD_Z";
		this.PNL_CHD_Z.Size = new System.Drawing.Size(625, 518);
		this.PNL_CHD_Z.TabIndex = 4;
		this.panel13.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel13.Controls.Add(this.ultraButton1);
		this.panel13.Controls.Add(this.ultraLabel14);
		this.panel13.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel13.Location = new System.Drawing.Point(0, 0);
		this.panel13.Name = "panel13";
		this.panel13.Size = new System.Drawing.Size(625, 28);
		this.panel13.TabIndex = 3;
		this.ultraButton1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance52.Cursor = System.Windows.Forms.Cursors.Default;
		appearance52.ForeColor = System.Drawing.Color.White;
		appearance52.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton1.Appearance = appearance52;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button;
		this.ultraButton1.Font = new System.Drawing.Font("細明體", 9f);
		appearance53.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance53.ForeColor = System.Drawing.Color.Yellow;
		this.ultraButton1.HotTrackAppearance = appearance53;
		this.ultraButton1.HotTracking = true;
		this.ultraButton1.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton1.Location = new System.Drawing.Point(480, 3);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(140, 23);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 9;
		this.ultraButton1.Text = "返回「系統維護」";
		this.ultraButton1.Click += new System.EventHandler(BtnGoHome_Click);
		appearance54.ForeColor = System.Drawing.Color.White;
		appearance54.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel14.Appearance = appearance54;
		this.ultraLabel14.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel14.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(152, 28);
		this.ultraLabel14.TabIndex = 8;
		this.ultraLabel14.Text = "選項/設定";
		this.pnl_spliter.BackColor = System.Drawing.Color.LightGray;
		this.pnl_spliter.Controls.Add(this.Btn_Splt);
		this.pnl_spliter.Controls.Add(this.ssp_Lower);
		this.pnl_spliter.Controls.Add(this.ssp_Bottom);
		this.pnl_spliter.Controls.Add(this.ssp_Upper);
		this.pnl_spliter.Controls.Add(this.ssp_Top);
		this.pnl_spliter.Dock = System.Windows.Forms.DockStyle.Left;
		this.pnl_spliter.Location = new System.Drawing.Point(160, 0);
		this.pnl_spliter.Name = "pnl_spliter";
		this.pnl_spliter.Size = new System.Drawing.Size(7, 548);
		this.pnl_spliter.TabIndex = 2;
		appearance55.BorderColor = System.Drawing.Color.Transparent;
		appearance55.BorderColor3DBase = System.Drawing.Color.Transparent;
		appearance55.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance55.ImageBackground");
		this.Btn_Splt.Appearance = appearance55;
		this.Btn_Splt.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Borderless;
		this.Btn_Splt.Cursor = System.Windows.Forms.Cursors.Hand;
		this.Btn_Splt.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Btn_Splt.ImageSize = new System.Drawing.Size(7, 57);
		this.Btn_Splt.Location = new System.Drawing.Point(0, 252);
		this.Btn_Splt.Name = "Btn_Splt";
		this.Btn_Splt.ShapeImage = (System.Drawing.Image)resources.GetObject("Btn_Splt.ShapeImage");
		this.Btn_Splt.ShowFocusRect = false;
		this.Btn_Splt.ShowOutline = false;
		this.Btn_Splt.Size = new System.Drawing.Size(7, 37);
		this.Btn_Splt.TabIndex = 5;
		this.Btn_Splt.Click += new System.EventHandler(Btn_Splt_Click);
		this.ssp_Lower.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ssp_Lower.Location = new System.Drawing.Point(0, 289);
		this.ssp_Lower.Name = "ssp_Lower";
		this.ssp_Lower.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Lower.OcxState");
		this.ssp_Lower.Size = new System.Drawing.Size(7, 256);
		this.ssp_Lower.TabIndex = 3;
		this.ssp_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ssp_Bottom.Location = new System.Drawing.Point(0, 545);
		this.ssp_Bottom.Name = "ssp_Bottom";
		this.ssp_Bottom.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Bottom.OcxState");
		this.ssp_Bottom.Size = new System.Drawing.Size(7, 3);
		this.ssp_Bottom.TabIndex = 4;
		this.ssp_Upper.Dock = System.Windows.Forms.DockStyle.Top;
		this.ssp_Upper.Location = new System.Drawing.Point(0, 3);
		this.ssp_Upper.Name = "ssp_Upper";
		this.ssp_Upper.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Upper.OcxState");
		this.ssp_Upper.Size = new System.Drawing.Size(7, 249);
		this.ssp_Upper.TabIndex = 2;
		this.ssp_Top.Dock = System.Windows.Forms.DockStyle.Top;
		this.ssp_Top.Location = new System.Drawing.Point(0, 0);
		this.ssp_Top.Name = "ssp_Top";
		this.ssp_Top.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Top.OcxState");
		this.ssp_Top.Size = new System.Drawing.Size(7, 3);
		this.ssp_Top.TabIndex = 1;
		this.panel1.Controls.Add(this.Tab_Ctrl);
		this.panel1.Controls.Add(this.panel2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel1.Location = new System.Drawing.Point(167, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(625, 548);
		this.panel1.TabIndex = 3;
		appearance56.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance56.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance56.FontData.Name = "Arial";
		appearance56.FontData.SizeInPoints = 9f;
		this.Tab_Ctrl.Appearance = appearance56;
		appearance57.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance57.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.Tab_Ctrl.ClientAreaAppearance = appearance57;
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_A);
		this.Tab_Ctrl.Controls.Add(this.Tab_B);
		this.Tab_Ctrl.Controls.Add(this.Tab_C);
		this.Tab_Ctrl.Controls.Add(this.Tab_D);
		this.Tab_Ctrl.Controls.Add(this.Tab_E);
		this.Tab_Ctrl.Controls.Add(this.Tab_F);
		this.Tab_Ctrl.Controls.Add(this.Tab_G);
		this.Tab_Ctrl.Controls.Add(this.Tab_I);
		this.Tab_Ctrl.Controls.Add(this.Tab_J);
		this.Tab_Ctrl.Controls.Add(this.Tab_Home);
		this.Tab_Ctrl.Controls.Add(this.Tab_Z);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.FlatMode = true;
		this.Tab_Ctrl.HotTrack = true;
		appearance58.BackColor = System.Drawing.Color.FromArgb(102, 153, 255);
		appearance58.BackColor2 = System.Drawing.Color.FromArgb(102, 153, 255);
		this.Tab_Ctrl.HotTrackAppearance = appearance58;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 2);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		appearance59.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance59.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.Tab_Ctrl.SelectedTabAppearance = appearance59;
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(625, 546);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabButtonStyle = Infragistics.Win.UIElementButtonStyle.PopupSoftBorderless;
		appearance60.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance60.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.Tab_Ctrl.TabHeaderAreaAppearance = appearance60;
		this.Tab_Ctrl.TabIndex = 1;
		this.Tab_Ctrl.TabPadding = new System.Drawing.Size(1, 10);
		this.Tab_Ctrl.TabPageMargins.Bottom = 0;
		this.Tab_Ctrl.TabPageMargins.Left = 0;
		this.Tab_Ctrl.TabPageMargins.Right = 0;
		this.Tab_Ctrl.TabPageMargins.Top = 0;
		ultraTab1.TabPage = this.Tab_Home;
		ultraTab1.Text = "主控面板";
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "主辦單位維護";
		ultraTab3.TabPage = this.Tab_C;
		ultraTab3.Text = "廠商資料維護";
		ultraTab4.TabPage = this.Tab_D;
		ultraTab4.Text = "公司資料行情";
		ultraTab5.TabPage = this.Tab_E;
		ultraTab5.Text = "常用字串設定";
		ultraTab6.TabPage = this.Tab_F;
		ultraTab6.Text = "系統訊息";
		ultraTab7.TabPage = this.Tab_A;
		ultraTab7.Text = "帳號權限管理";
		ultraTab8.TabPage = this.Tab_I;
		ultraTab8.Text = "專案權限設定";
		ultraTab9.TabPage = this.Tab_G;
		ultraTab9.Text = "資料庫維護";
		ultraTab10.TabPage = this.Tab_J;
		ultraTab10.Text = "線上更新";
		ultraTab11.TabPage = this.Tab_Z;
		ultraTab11.Text = "選項/設定";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[11]
		{
			ultraTab1, ultraTab2, ultraTab3, ultraTab4, ultraTab5, ultraTab6, ultraTab7, ultraTab8, ultraTab9, ultraTab10,
			ultraTab11
		});
		this.Tab_Ctrl.SelectedTabChanged += new Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventHandler(Tab_Ctrl_SelectedTabChanged);
		this.Tab_Ctrl.Resize += new System.EventHandler(Tab_Ctrl_Resize);
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(625, 546);
		this.panel2.Controls.Add(this.ultraLabel1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(625, 2);
		this.panel2.TabIndex = 0;
		this.panel2.Visible = false;
		appearance61.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance61.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance61.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance61.FontData.Name = "Times New Roman";
		appearance61.FontData.SizeInPoints = 12f;
		appearance61.ForeColor = System.Drawing.Color.White;
		appearance61.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance61;
		this.ultraLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraLabel1.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(625, 2);
		this.ultraLabel1.TabIndex = 0;
		this.iglst_splt_Btn.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("iglst_splt_Btn.ImageStream");
		this.iglst_splt_Btn.TransparentColor = System.Drawing.Color.Transparent;
		this.iglst_splt_Btn.Images.SetKeyName(0, "");
		this.iglst_splt_Btn.Images.SetKeyName(1, "");
		this.iglst_splt_Btn.Images.SetKeyName(2, "");
		this.iglst_splt_Btn.Images.SetKeyName(3, "");
		this.LeftPanel.Controls.Add(this.functionButtons1);
		this.LeftPanel.Controls.Add(this.onlineList1);
		this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
		this.LeftPanel.Location = new System.Drawing.Point(0, 0);
		this.LeftPanel.Name = "LeftPanel";
		this.LeftPanel.Size = new System.Drawing.Size(160, 548);
		this.LeftPanel.TabIndex = 4;
		this.functionButtons1._ActiveFunction = "";
		this.functionButtons1._CurrOpenMode = Archnowledge.Pcces.CommonClass.FunctionOpenMode.Budget;
		this.functionButtons1._ServerName = "localhost";
		this.functionButtons1._UserID = "PccesAdmin";
		this.functionButtons1._UserName = "";
		this.functionButtons1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.functionButtons1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.functionButtons1.Location = new System.Drawing.Point(0, 256);
		this.functionButtons1.Name = "functionButtons1";
		this.functionButtons1.Size = new System.Drawing.Size(160, 292);
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
		base.ClientSize = new System.Drawing.Size(792, 548);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.pnl_spliter);
		base.Controls.Add(this.LeftPanel);
		base.KeyPreview = true;
		base.Name = "frmSysMaintain";
		this.Text = "FormSysMaintain";
		base.Load += new System.EventHandler(frmSysMaintain_Load);
		base.Resize += new System.EventHandler(frmSysMaintain_Resize);
		this.Tab_Home.ResumeLayout(false);
		this.Tab_B.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.Tab_C.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		this.Tab_D.ResumeLayout(false);
		this.panel6.ResumeLayout(false);
		this.Tab_E.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		this.Tab_F.ResumeLayout(false);
		this.panel8.ResumeLayout(false);
		this.Tab_A.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		this.Tab_I.ResumeLayout(false);
		this.panel11.ResumeLayout(false);
		this.Tab_G.ResumeLayout(false);
		this.panel9.ResumeLayout(false);
		this.Tab_J.ResumeLayout(false);
		this.panel12.ResumeLayout(false);
		this.Tab_Z.ResumeLayout(false);
		this.panel13.ResumeLayout(false);
		this.pnl_spliter.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ssp_Lower).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Bottom).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Upper).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Top).EndInit();
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).EndInit();
		this.Tab_Ctrl.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.LeftPanel.ResumeLayout(false);
		this.LeftPanel.PerformLayout();
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
