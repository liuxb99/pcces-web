using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.PccesMain.About;
using Archnowledge.Pcces.PccesMain.Library;
using Archnowledge.Pcces.PccesMain.Project;
using Archnowledge.Pcces.PccesMain.ShellLib;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain;

public class FormPanel2 : Form
{
	private const string CallFormHelp = "FormPanel2";

	private bool Is_CustomBackground = false;

	private string F_UserID;

	private IContainer components;

	private UltraPictureBox ultraPictureBox1;

	private ImageList imageList1;

	private UltraPictureBox img800;

	private UltraPictureBox img1000;

	public Panel PNL1;

	private Panel PNL2;

	private UltraLabel lblDescript;

	private UltraButton FuncBtn1;

	private UltraButton FuncBtn6;

	private UltraButton FuncBtn5;

	private UltraButton FuncBtn7;

	private UltraButton FuncBtn2;

	private UltraButton FuncBtn8;

	private UltraButton FuncBtn4;

	private UltraButton FuncBtn3;

	private UltraLabel lblFuncName;

	private UltraButton FuncBtn9;

	private Panel panel1;

	private UltraPictureBox ultraPictureBox2;

	private UltraLabel ultraLabel13;

	private UltraLabel lblUseDatabase;

	private Panel panel2;

	private UltraPictureBox ultraPictureBox3;

	private UltraPictureBox ultraPictureBox4;

	private UltraLabel ultraLabel3;

	private LinkLabel linkLabel1;

	private UltraLabel ultraLabel2;

	private UltraPictureBox ultraPictureBox5;

	private Panel panel3;

	private Label label1;

	private UltraButton FuncBtn10;

	private UltraButton FuncBtn11;

	private UltraButton FuncBtn12;

	private UltraLabel ultraLabel1;

	private Timer timer1;

	private UltraButton FuncBtnBidImport;

	private UltraLabel linkQuestionnaire;

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

	public FormPanel2()
	{
		InitializeComponent();
	}

	private void FormPanel2_SizeChanged(object sender, EventArgs e)
	{
		decimal H_Btm = base.Height * 357 / 1024;
		decimal HH = ((decimal)(base.Height - 287) + H_Btm) * 380m;
		HH /= 1024m;
		PNL1.Top = (int)HH - 20;
		PNL2.Top = PNL1.Top - 10;
		if (base.Width < 800)
		{
			ultraPictureBox1.Image = img800.Image;
			PNL1.Left = (((base.Width - 610) / 2 <= 0) ? 8 : ((base.Width - 610) / 2));
			PNL2.Visible = false;
			ultraPictureBox1.Visible = true;
		}
		else
		{
			ultraPictureBox1.Image = img1000.Image;
			PNL1.Left = (((base.Width - 800) / 2 <= 0) ? 5 : ((base.Width - 800) / 2));
			PNL2.Left = PNL1.Left + 562;
			PNL2.Visible = true;
			ultraPictureBox1.Visible = true;
			PNL1.Visible = !Is_CustomBackground;
			PNL2.Visible = !Is_CustomBackground;
		}
		lblUseDatabase.Left = PNL1.Left + 1;
		lblUseDatabase.Top = PNL1.Top + PNL1.Height + 32;
	}

	private void FormPanel2_Load(object sender, EventArgs e)
	{
		CorrectRatio();
		lblFuncName.Text = "";
		lblDescript.Text = "";
		SysUser oSysUser = new SysUser();
		string DatabaseDesc = oSysUser.GetSysUserDatabaseDesc(F_UserID);
		if (DatabaseDesc.Trim() != "")
		{
			lblUseDatabase.Text = "目前資料庫:【" + DatabaseDesc.Trim() + "】";
			lblUseDatabase.Visible = true;
		}
		if (File.Exists("MainBackground.jpg"))
		{
			img1000.Image = Image.FromFile("MainBackground.jpg");
			Is_CustomBackground = true;
			lblUseDatabase.Appearance.ImageBackground = null;
			lblUseDatabase.Visible = false;
		}
		UpdateMenu();
	}

	public void UpdateMenu()
	{
		ModuleManager oManager = new ModuleManager();
		FuncBtn5.Visible = oManager.EnableBudgetMdoule;
		FuncBtn3.Visible = oManager.EnableBudgetMdoule;
		FuncBtn9.Visible = oManager.EnableContractModule;
		FuncBtn10.Visible = oManager.EnableContractModule;
		FuncBtn6.Visible = oManager.EnableContractModule;
		FuncBtn11.Visible = oManager.EnableContractModule;
		FuncBtn12.Visible = oManager.EnableContractModule;
		FuncBtn4.Visible = oManager.EnableBidMdoule;
		FuncBtnBidImport.Visible = oManager.EnableBidMdoule;
		FuncBtn2.Visible = oManager.EnableCommonMdoule;
		FuncBtn8.Visible = oManager.EnableCommonMdoule;
		FuncBtn7.Visible = oManager.EnableCommonMdoule;
	}

	private void CorrectRatio()
	{
		try
		{
			double ratio = CommonMethods.GetWindowRatio(base.Handle);
			if (ratio == 1.0)
			{
				return;
			}
			foreach (Control Cn in base.Controls)
			{
				Cn.Font = new Font(Cn.Font.Name, (float)((double)Cn.Font.Size * ratio));
			}
			foreach (Control Cn in PNL1.Controls)
			{
				(Cn as UltraButton).Appearance.FontData.SizeInPoints = (float)((double)(Cn as UltraButton).Appearance.FontData.SizeInPoints * ratio);
			}
			foreach (Control Cn in PNL2.Controls)
			{
				Cn.Font = new Font(Cn.Font.Name, (float)((double)Cn.Font.Size * ratio));
			}
			foreach (Control Cn in panel1.Controls)
			{
				Cn.Font = new Font(Cn.Font.Name, (float)((double)Cn.Font.Size * ratio));
			}
			foreach (Control Cn in panel2.Controls)
			{
				Cn.Font = new Font(Cn.Font.Name, (float)((double)Cn.Font.Size * ratio));
			}
			foreach (Control Cn in panel3.Controls)
			{
				Cn.Font = new Font(Cn.Font.Name, (float)((double)Cn.Font.Size * ratio));
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "FormPanel2.cs" + ex.Message);
			Console.Write(ex.Message);
		}
	}

	private void FuncBtn2_MouseEnter(object sender, EventArgs e)
	{
		switch ((sender as Control).Name)
		{
		case "FuncBtn2":
			lblFuncName.Text = "基本資料庫維護";
			lblDescript.Text = "提供基本工項資料庫的及單價分析編輯功能，以供預算書編製時引用，亦提供自動編碼功能自工程會頒佈之工項編碼規則表組合出工項編碼、名稱及單位。";
			break;
		case "FuncBtn5":
			lblFuncName.Text = "專案目錄";
			lblDescript.Text = "本功能為專案目錄之管理，編製預算或空白標單前須先成立專案，此選項提供轉入、刪除專案及瀏覽專案屬性的功能。";
			break;
		case "FuncBtn4":
			lblFuncName.Text = "投標單填寫";
			lblDescript.Text = "提供投標廠商轉入業主提供之空白標單後，進行投標單製作，產出標單詳細表、單價分析表、資源統計表及標單電子檔。";
			break;
		case "FuncBtn3":
			lblFuncName.Text = "預算書編製";
			lblDescript.Text = "本功能提供招標機關進行預算書的編製，產出預算書總表、詳細表、單價分析表、資源統計表及空白電子標單檔，其特色在於可引用基本工項資料庫或既存其他專案之工項及單價分析資料，亦可將依規定格式自編的Excel預算檔轉入系統，以減少輸入時間，除此外更提供預算併標及分標功能";
			break;
		case "FuncBtn6":
			lblFuncName.Text = "契約變更";
			lblDescript.Text = "本功能提供招標機關於契約書核定後契約數量之追加或追減作業，產生變更總表、契約書詳細表、單價分析表。";
			break;
		case "FuncBtn9":
			lblFuncName.Text = "契約編製";
			lblDescript.Text = "本功能提供招標機關進行決標後之契約書核定作業，產生契約書總表、契約書詳細表、契約書單價分析表。";
			break;
		case "FuncBtn10":
			lblFuncName.Text = "估驗記錄";
			lblDescript.Text = "本功能提供新增每期估驗計價記錄，可產出分期估驗總表、明細表。";
			break;
		case "FuncBtn7":
			lblFuncName.Text = "經費審查比對";
			lblDescript.Text = "提供在同一「基本工項資料庫」下，多個工程專案與比對基準專案間之工程項目的單價及其單價分析內容比對功能。本功能的特色是可設定比對精度及指定比對方式。";
			break;
		case "FuncBtn8":
			lblFuncName.Text = "歷史工程單位造價";
			lblDescript.Text = "提供在同一「基本工項資料庫」下，多個工程專案與比對基準專案間之工程項目的數量、單價、複價比對功能。";
			break;
		case "FuncBtn1":
			lblFuncName.Text = "系統維護";
			lblDescript.Text = "本功能提供「主辦單位資料維護」、、「廠商資料維護」、「公司資料行情」、「常用字串設定」、「系統訊息」、「專案權限管理」、「帳號權限設定」、「資料庫切換」等功能。";
			break;
		case "FuncBtnSwitch":
			lblFuncName.Text = "面板切換";
			lblDescript.Text = "提供使用者自行切換自己喜愛的首頁面板。";
			break;
		default:
			lblFuncName.Text = "";
			lblDescript.Text = "";
			break;
		}
	}

	private void FuncBtn2_MouseLeave(object sender, EventArgs e)
	{
		lblFuncName.Text = "";
		lblDescript.Text = "";
	}

	private void FuncBtn5_Click(object sender, EventArgs e)
	{
		PNL1.Enabled = false;
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc5_Click(this, EventArgs.Empty);
		PNL1.Enabled = true;
	}

	private void FuncBtn2_Click(object sender, EventArgs e)
	{
		PNL1.Enabled = false;
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc2_Click(this, EventArgs.Empty);
		PNL1.Enabled = true;
	}

	private void FuncBtn4_Click(object sender, EventArgs e)
	{
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc4_Click(this, EventArgs.Empty);
	}

	private void FuncBtn3_Click(object sender, EventArgs e)
	{
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc3_Click(this, EventArgs.Empty);
	}

	private void FuncBtn6_Click(object sender, EventArgs e)
	{
		PNL1.Enabled = false;
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc6_Click(this, EventArgs.Empty);
		PNL1.Enabled = true;
	}

	private void FuncBtn9_Click(object sender, EventArgs e)
	{
		PNL1.Enabled = false;
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc9_Click(this, EventArgs.Empty);
		PNL1.Enabled = true;
	}

	private void FuncBtn10_Click(object sender, EventArgs e)
	{
		PNL1.Enabled = false;
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc10_Click(this, EventArgs.Empty);
		PNL1.Enabled = true;
	}

	private void FuncBtn7_Click(object sender, EventArgs e)
	{
		PNL1.Enabled = false;
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc7_Click(this, EventArgs.Empty);
		PNL1.Enabled = true;
	}

	private void FuncBtn8_Click(object sender, EventArgs e)
	{
		PNL1.Enabled = false;
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc8_Click(this, EventArgs.Empty);
		PNL1.Enabled = true;
	}

	private void FuncBtn1_Click(object sender, EventArgs e)
	{
		PNL1.Enabled = false;
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc1_Click(this, EventArgs.Empty);
		PNL1.Enabled = true;
	}

	private void FuncBtn11_Click(object sender, EventArgs e)
	{
		PNL1.Enabled = false;
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc11_Click(this, EventArgs.Empty);
		PNL1.Enabled = true;
	}

	private void FuncBtn12_Click(object sender, EventArgs e)
	{
		PNL1.Enabled = false;
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc12_Click(this, EventArgs.Empty);
		PNL1.Enabled = true;
	}

	private void BtnAbout_Click(object sender, EventArgs e)
	{
		FormAbout FM_ABT = new FormAbout();
		FM_ABT.ShowDialog();
	}

	private void FuncBtnBidImport_Click(object sender, EventArgs e)
	{
		formNewProjectWizard FM_NEW_PROJ_WZD = new formNewProjectWizard();
		FM_NEW_PROJ_WZD._UserID = F_UserID;
		FM_NEW_PROJ_WZD._IniMode = "2";
		FM_NEW_PROJ_WZD._IsAddOn = "BID";
		FM_NEW_PROJ_WZD.ShowDialog(this);
		FM_NEW_PROJ_WZD.Dispose();
		FM_NEW_PROJ_WZD = null;
		GC.Collect();
	}

	private void ultraLabel13_Click(object sender, EventArgs e)
	{
		FormPanelPick FM_PNL_PK = new FormPanelPick();
		FM_PNL_PK._OriginalHomeID = "2";
		DialogResult theResult = FM_PNL_PK.ShowDialog();
		FM_PNL_PK.Close();
		FM_PNL_PK.Dispose();
		FM_PNL_PK = null;
		if (theResult != DialogResult.OK)
		{
			return;
		}
		string sIniFileName = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "PccesMain.ini";
		string sHomeID = CommonMethods.IniReadValue(sIniFileName, "HomePanel", "Home");
		if (sHomeID.Trim() != "2")
		{
			if (sHomeID == "1")
			{
				FormPanel FM_PNL1 = new FormPanel();
				FM_PNL1._UserID = F_UserID;
				FM_PNL1.MdiParent = base.ParentForm;
				FM_PNL1.Show();
			}
			if (sHomeID == "3")
			{
				FormPanel3 FM_PNL3 = new FormPanel3();
				FM_PNL3._UserID = F_UserID;
				FM_PNL3.MdiParent = base.ParentForm;
				FM_PNL3.Show();
			}
			Close();
		}
	}

	private void linkLabel1_MouseEnter(object sender, EventArgs e)
	{
		linkLabel1.LinkColor = Color.Orange;
	}

	private void linkLabel1_MouseLeave(object sender, EventArgs e)
	{
		linkLabel1.LinkColor = Color.White;
	}

	private void ultraLabel2_Click(object sender, EventArgs e)
	{
		ShellExecute SHExe = new ShellExecute();
		SHExe.OwnerHandle = base.Handle;
		SHExe.Path = "http://pcces.pcc.gov.tw/CSInew/Default.aspx?FunID=Fun_7&SearchType=H";
		SHExe.Execute();
	}

	private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		ShellExecute SHExe = new ShellExecute();
		SHExe.OwnerHandle = base.Handle;
		SHExe.Path = "mailto:service@archnowledge.com";
		SHExe.Execute();
	}

	private void FormPanel2_Activated(object sender, EventArgs e)
	{
		(base.ParentForm as frmPccesMain).Text = "PCCES Win 4.3 ";
		PNL1.Enabled = true;
		lblFuncName.Text = "";
		lblDescript.Text = "";
		SysUser oSysUser = new SysUser();
		string DatabaseDesc = oSysUser.GetSysUserDatabaseDesc(F_UserID);
		if (DatabaseDesc.Trim() != "")
		{
			lblUseDatabase.Text = "目前資料庫:【" + DatabaseDesc.Trim() + "】";
			lblUseDatabase.Visible = true;
		}
	}

	private void ultraLabel1_Click(object sender, EventArgs e)
	{
		FormUpdateInfo FM_UPDINFO = new FormUpdateInfo();
		FM_UPDINFO.ShowDialog();
		FM_UPDINFO.Close();
		FM_UPDINFO.Dispose();
		FM_UPDINFO = null;
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		if (linkQuestionnaire.Appearance.ForeColor == Color.White)
		{
			linkQuestionnaire.Appearance.ForeColor = Color.Gold;
		}
		else
		{
			linkQuestionnaire.Appearance.ForeColor = Color.White;
		}
	}

	private void FormPanel2_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormPanel2");
		}
	}

	private void linkQuestionnaire_Click(object sender, EventArgs e)
	{
		string queryString = GetQueryString();
		string webAddress = "http://pcces.pcc.gov.tw/csinew/Default.aspx?FunID=Fun_12_11&q=" + queryString;
		try
		{
			Process.Start(webAddress);
		}
		catch (Win32Exception ex)
		{
			if (ex.ErrorCode == -2147467259)
			{
				MessageBox.Show(ex.Message);
			}
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.Message);
		}
	}

	private string GetQueryString()
	{
		string user = CommonMethods.GetIniValue("Register", "UserName");
		string email = CommonMethods.GetIniValue("Register", "EMAIL");
		string company = CommonMethods.GetIniValue("Register", "CompanyName");
		string department = CommonMethods.GetIniValue("Register", "Dept");
		string telephone = CommonMethods.GetIniValue("Register", "TEL");
		string queryString = user + ";" + email + ";" + company + ";" + department + ";" + telephone + ";" + DateTime.Now.ToString("MMddHHmm");
		return CommonMethods.EncryptDESInUTF8(queryString, "ARCH1313", "13139409");
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.FormPanel2));
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
		this.ultraPictureBox1 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.PNL1 = new System.Windows.Forms.Panel();
		this.FuncBtnBidImport = new Infragistics.Win.Misc.UltraButton();
		this.FuncBtn12 = new Infragistics.Win.Misc.UltraButton();
		this.FuncBtn11 = new Infragistics.Win.Misc.UltraButton();
		this.FuncBtn10 = new Infragistics.Win.Misc.UltraButton();
		this.FuncBtn9 = new Infragistics.Win.Misc.UltraButton();
		this.FuncBtn1 = new Infragistics.Win.Misc.UltraButton();
		this.FuncBtn6 = new Infragistics.Win.Misc.UltraButton();
		this.FuncBtn5 = new Infragistics.Win.Misc.UltraButton();
		this.FuncBtn7 = new Infragistics.Win.Misc.UltraButton();
		this.FuncBtn2 = new Infragistics.Win.Misc.UltraButton();
		this.FuncBtn8 = new Infragistics.Win.Misc.UltraButton();
		this.FuncBtn4 = new Infragistics.Win.Misc.UltraButton();
		this.FuncBtn3 = new Infragistics.Win.Misc.UltraButton();
		this.PNL2 = new System.Windows.Forms.Panel();
		this.lblDescript = new Infragistics.Win.Misc.UltraLabel();
		this.lblFuncName = new Infragistics.Win.Misc.UltraLabel();
		this.img800 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.img1000 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.linkQuestionnaire = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.linkLabel1 = new System.Windows.Forms.LinkLabel();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraPictureBox2 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.lblUseDatabase = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.ultraPictureBox4 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.ultraPictureBox3 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.ultraPictureBox5 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.panel3 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.PNL1.SuspendLayout();
		this.PNL2.SuspendLayout();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel3.SuspendLayout();
		base.SuspendLayout();
		appearance1.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance1.ImageBackground");
		appearance1.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Tiled;
		this.ultraPictureBox1.Appearance = appearance1;
		this.ultraPictureBox1.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox1.BorderShadowDepth = 0;
		this.ultraPictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraPictureBox1.Image = resources.GetObject("ultraPictureBox1.Image");
		this.ultraPictureBox1.Location = new System.Drawing.Point(0, 0);
		this.ultraPictureBox1.Name = "ultraPictureBox1";
		this.ultraPictureBox1.ScaleImage = Infragistics.Win.ScaleImage.Never;
		this.ultraPictureBox1.Size = new System.Drawing.Size(1512, 746);
		this.ultraPictureBox1.TabIndex = 0;
		this.ultraPictureBox1.Visible = false;
		this.PNL1.BackColor = System.Drawing.Color.Transparent;
		this.PNL1.Controls.Add(this.FuncBtnBidImport);
		this.PNL1.Controls.Add(this.FuncBtn12);
		this.PNL1.Controls.Add(this.FuncBtn11);
		this.PNL1.Controls.Add(this.FuncBtn10);
		this.PNL1.Controls.Add(this.FuncBtn9);
		this.PNL1.Controls.Add(this.FuncBtn1);
		this.PNL1.Controls.Add(this.FuncBtn6);
		this.PNL1.Controls.Add(this.FuncBtn5);
		this.PNL1.Controls.Add(this.FuncBtn7);
		this.PNL1.Controls.Add(this.FuncBtn2);
		this.PNL1.Controls.Add(this.FuncBtn8);
		this.PNL1.Controls.Add(this.FuncBtn4);
		this.PNL1.Controls.Add(this.FuncBtn3);
		this.PNL1.Location = new System.Drawing.Point(372, 248);
		this.PNL1.Name = "PNL1";
		this.PNL1.Size = new System.Drawing.Size(444, 274);
		this.PNL1.TabIndex = 1;
		appearance2.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance2.FontData.Name = "Arial";
		appearance2.FontData.SizeInPoints = 9f;
		appearance2.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance2.ImageBackground");
		appearance2.TextHAlign = Infragistics.Win.HAlign.Left;
		this.FuncBtnBidImport.Appearance = appearance2;
		this.FuncBtnBidImport.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
		appearance3.BorderAlpha = Infragistics.Win.Alpha.Transparent;
		appearance3.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance3.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance3.ImageBackground");
		this.FuncBtnBidImport.HotTrackAppearance = appearance3;
		this.FuncBtnBidImport.HotTracking = true;
		this.FuncBtnBidImport.Location = new System.Drawing.Point(8, 122);
		this.FuncBtnBidImport.Name = "FuncBtnBidImport";
		this.FuncBtnBidImport.ShapeImage = (System.Drawing.Image)resources.GetObject("FuncBtnBidImport.ShapeImage");
		this.FuncBtnBidImport.ShowFocusRect = false;
		this.FuncBtnBidImport.ShowOutline = false;
		this.FuncBtnBidImport.Size = new System.Drawing.Size(161, 28);
		this.FuncBtnBidImport.TabIndex = 13;
		this.FuncBtnBidImport.Text = "\u3000\u3000 標單轉入";
		this.FuncBtnBidImport.Click += new System.EventHandler(FuncBtnBidImport_Click);
		appearance4.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance4.FontData.Name = "Arial";
		appearance4.FontData.SizeInPoints = 9f;
		appearance4.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance4.ImageBackground");
		appearance4.TextHAlign = Infragistics.Win.HAlign.Left;
		this.FuncBtn12.Appearance = appearance4;
		this.FuncBtn12.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
		appearance5.BorderAlpha = Infragistics.Win.Alpha.Transparent;
		appearance5.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance5.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance5.ImageBackground");
		this.FuncBtn12.HotTrackAppearance = appearance5;
		this.FuncBtn12.HotTracking = true;
		this.FuncBtn12.Location = new System.Drawing.Point(260, 177);
		this.FuncBtn12.Name = "FuncBtn12";
		this.FuncBtn12.ShapeImage = (System.Drawing.Image)resources.GetObject("FuncBtn12.ShapeImage");
		this.FuncBtn12.ShowFocusRect = false;
		this.FuncBtn12.ShowOutline = false;
		this.FuncBtn12.Size = new System.Drawing.Size(161, 28);
		this.FuncBtn12.TabIndex = 12;
		this.FuncBtn12.Text = "\u3000\u3000 決算";
		this.FuncBtn12.MouseLeave += new System.EventHandler(FuncBtn2_MouseLeave);
		this.FuncBtn12.Click += new System.EventHandler(FuncBtn12_Click);
		this.FuncBtn12.MouseEnter += new System.EventHandler(FuncBtn2_MouseEnter);
		appearance6.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance6.FontData.Name = "Arial";
		appearance6.FontData.SizeInPoints = 9f;
		appearance6.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance6.ImageBackground");
		appearance6.TextHAlign = Infragistics.Win.HAlign.Left;
		this.FuncBtn11.Appearance = appearance6;
		this.FuncBtn11.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
		appearance7.BorderAlpha = Infragistics.Win.Alpha.Transparent;
		appearance7.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance7.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance7.ImageBackground");
		this.FuncBtn11.HotTrackAppearance = appearance7;
		this.FuncBtn11.HotTracking = true;
		this.FuncBtn11.Location = new System.Drawing.Point(260, 141);
		this.FuncBtn11.Name = "FuncBtn11";
		this.FuncBtn11.ShapeImage = (System.Drawing.Image)resources.GetObject("FuncBtn11.ShapeImage");
		this.FuncBtn11.ShowFocusRect = false;
		this.FuncBtn11.ShowOutline = false;
		this.FuncBtn11.Size = new System.Drawing.Size(161, 28);
		this.FuncBtn11.TabIndex = 11;
		this.FuncBtn11.Text = "\u3000\u3000 結算";
		this.FuncBtn11.MouseLeave += new System.EventHandler(FuncBtn2_MouseLeave);
		this.FuncBtn11.Click += new System.EventHandler(FuncBtn11_Click);
		this.FuncBtn11.MouseEnter += new System.EventHandler(FuncBtn2_MouseEnter);
		appearance8.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance8.FontData.Name = "Arial";
		appearance8.FontData.SizeInPoints = 9f;
		appearance8.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance8.ImageBackground");
		appearance8.TextHAlign = Infragistics.Win.HAlign.Left;
		this.FuncBtn10.Appearance = appearance8;
		this.FuncBtn10.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
		appearance9.BorderAlpha = Infragistics.Win.Alpha.Transparent;
		appearance9.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance9.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance9.ImageBackground");
		this.FuncBtn10.HotTrackAppearance = appearance9;
		this.FuncBtn10.HotTracking = true;
		this.FuncBtn10.Location = new System.Drawing.Point(260, 69);
		this.FuncBtn10.Name = "FuncBtn10";
		this.FuncBtn10.ShapeImage = (System.Drawing.Image)resources.GetObject("FuncBtn10.ShapeImage");
		this.FuncBtn10.ShowFocusRect = false;
		this.FuncBtn10.ShowOutline = false;
		this.FuncBtn10.Size = new System.Drawing.Size(161, 28);
		this.FuncBtn10.TabIndex = 10;
		this.FuncBtn10.Text = "\u3000\u3000 估驗記錄";
		this.FuncBtn10.MouseLeave += new System.EventHandler(FuncBtn2_MouseLeave);
		this.FuncBtn10.Click += new System.EventHandler(FuncBtn10_Click);
		this.FuncBtn10.MouseEnter += new System.EventHandler(FuncBtn2_MouseEnter);
		appearance10.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance10.FontData.Name = "Arial";
		appearance10.FontData.SizeInPoints = 9f;
		appearance10.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance10.ImageBackground");
		appearance10.TextHAlign = Infragistics.Win.HAlign.Left;
		this.FuncBtn9.Appearance = appearance10;
		this.FuncBtn9.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
		appearance11.BorderAlpha = Infragistics.Win.Alpha.Transparent;
		appearance11.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance11.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance11.ImageBackground");
		this.FuncBtn9.HotTrackAppearance = appearance11;
		this.FuncBtn9.HotTracking = true;
		this.FuncBtn9.Location = new System.Drawing.Point(260, 32);
		this.FuncBtn9.Name = "FuncBtn9";
		this.FuncBtn9.ShapeImage = (System.Drawing.Image)resources.GetObject("FuncBtn9.ShapeImage");
		this.FuncBtn9.ShowFocusRect = false;
		this.FuncBtn9.ShowOutline = false;
		this.FuncBtn9.Size = new System.Drawing.Size(161, 28);
		this.FuncBtn9.TabIndex = 9;
		this.FuncBtn9.Text = "\u3000\u3000 契約編製";
		this.FuncBtn9.MouseLeave += new System.EventHandler(FuncBtn2_MouseLeave);
		this.FuncBtn9.Click += new System.EventHandler(FuncBtn9_Click);
		this.FuncBtn9.MouseEnter += new System.EventHandler(FuncBtn2_MouseEnter);
		appearance12.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance12.FontData.Name = "Arial";
		appearance12.FontData.SizeInPoints = 9f;
		appearance12.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance12.ImageBackground");
		appearance12.TextHAlign = Infragistics.Win.HAlign.Left;
		this.FuncBtn1.Appearance = appearance12;
		this.FuncBtn1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
		appearance13.BorderAlpha = Infragistics.Win.Alpha.Transparent;
		appearance13.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance13.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance13.ImageBackground");
		this.FuncBtn1.HotTrackAppearance = appearance13;
		this.FuncBtn1.HotTracking = true;
		this.FuncBtn1.Location = new System.Drawing.Point(260, 234);
		this.FuncBtn1.Name = "FuncBtn1";
		this.FuncBtn1.ShapeImage = (System.Drawing.Image)resources.GetObject("FuncBtn1.ShapeImage");
		this.FuncBtn1.ShowFocusRect = false;
		this.FuncBtn1.ShowOutline = false;
		this.FuncBtn1.Size = new System.Drawing.Size(161, 28);
		this.FuncBtn1.TabIndex = 7;
		this.FuncBtn1.Text = "\u3000\u3000 系統維護";
		this.FuncBtn1.MouseLeave += new System.EventHandler(FuncBtn2_MouseLeave);
		this.FuncBtn1.Click += new System.EventHandler(FuncBtn1_Click);
		this.FuncBtn1.MouseEnter += new System.EventHandler(FuncBtn2_MouseEnter);
		appearance14.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance14.FontData.Name = "Arial";
		appearance14.FontData.SizeInPoints = 9f;
		appearance14.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance14.ImageBackground");
		appearance14.TextHAlign = Infragistics.Win.HAlign.Left;
		this.FuncBtn6.Appearance = appearance14;
		this.FuncBtn6.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
		appearance15.BorderAlpha = Infragistics.Win.Alpha.Transparent;
		appearance15.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance15.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance15.ImageBackground");
		this.FuncBtn6.HotTrackAppearance = appearance15;
		this.FuncBtn6.HotTracking = true;
		this.FuncBtn6.Location = new System.Drawing.Point(260, 105);
		this.FuncBtn6.Name = "FuncBtn6";
		this.FuncBtn6.ShapeImage = (System.Drawing.Image)resources.GetObject("FuncBtn6.ShapeImage");
		this.FuncBtn6.ShowFocusRect = false;
		this.FuncBtn6.ShowOutline = false;
		this.FuncBtn6.Size = new System.Drawing.Size(161, 28);
		this.FuncBtn6.TabIndex = 6;
		this.FuncBtn6.Text = "\u3000\u3000 契約變更";
		this.FuncBtn6.MouseLeave += new System.EventHandler(FuncBtn2_MouseLeave);
		this.FuncBtn6.Click += new System.EventHandler(FuncBtn6_Click);
		this.FuncBtn6.MouseEnter += new System.EventHandler(FuncBtn2_MouseEnter);
		appearance16.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance16.FontData.Name = "Arial";
		appearance16.FontData.SizeInPoints = 9f;
		appearance16.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance16.ImageBackground");
		appearance16.TextHAlign = Infragistics.Win.HAlign.Left;
		this.FuncBtn5.Appearance = appearance16;
		this.FuncBtn5.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
		appearance17.BorderAlpha = Infragistics.Win.Alpha.Transparent;
		appearance17.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance17.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance17.ImageBackground");
		this.FuncBtn5.HotTrackAppearance = appearance17;
		this.FuncBtn5.HotTracking = true;
		this.FuncBtn5.Location = new System.Drawing.Point(8, 8);
		this.FuncBtn5.Name = "FuncBtn5";
		this.FuncBtn5.ShapeImage = (System.Drawing.Image)resources.GetObject("FuncBtn5.ShapeImage");
		this.FuncBtn5.ShowFocusRect = false;
		this.FuncBtn5.ShowOutline = false;
		this.FuncBtn5.Size = new System.Drawing.Size(161, 28);
		this.FuncBtn5.TabIndex = 5;
		this.FuncBtn5.Text = "\u3000\u3000 專案目錄";
		this.FuncBtn5.MouseLeave += new System.EventHandler(FuncBtn2_MouseLeave);
		this.FuncBtn5.Click += new System.EventHandler(FuncBtn5_Click);
		this.FuncBtn5.MouseEnter += new System.EventHandler(FuncBtn2_MouseEnter);
		appearance18.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance18.FontData.Name = "Arial";
		appearance18.FontData.SizeInPoints = 9f;
		appearance18.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance18.ImageBackground");
		appearance18.TextHAlign = Infragistics.Win.HAlign.Left;
		this.FuncBtn7.Appearance = appearance18;
		this.FuncBtn7.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
		appearance19.BorderAlpha = Infragistics.Win.Alpha.Transparent;
		appearance19.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance19.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance19.ImageBackground");
		this.FuncBtn7.HotTrackAppearance = appearance19;
		this.FuncBtn7.HotTracking = true;
		this.FuncBtn7.Location = new System.Drawing.Point(8, 234);
		this.FuncBtn7.Name = "FuncBtn7";
		this.FuncBtn7.ShapeImage = (System.Drawing.Image)resources.GetObject("FuncBtn7.ShapeImage");
		this.FuncBtn7.ShowFocusRect = false;
		this.FuncBtn7.ShowOutline = false;
		this.FuncBtn7.Size = new System.Drawing.Size(161, 28);
		this.FuncBtn7.TabIndex = 4;
		this.FuncBtn7.Text = "\u3000\u3000 經費審查比對";
		this.FuncBtn7.MouseLeave += new System.EventHandler(FuncBtn2_MouseLeave);
		this.FuncBtn7.Click += new System.EventHandler(FuncBtn7_Click);
		this.FuncBtn7.MouseEnter += new System.EventHandler(FuncBtn2_MouseEnter);
		appearance20.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance20.FontData.Name = "Arial";
		appearance20.FontData.SizeInPoints = 9f;
		appearance20.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance20.ImageBackground");
		appearance20.TextHAlign = Infragistics.Win.HAlign.Left;
		this.FuncBtn2.Appearance = appearance20;
		this.FuncBtn2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
		appearance21.BorderAlpha = Infragistics.Win.Alpha.Transparent;
		appearance21.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance21.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance21.ImageBackground");
		this.FuncBtn2.HotTrackAppearance = appearance21;
		this.FuncBtn2.HotTracking = true;
		this.FuncBtn2.Location = new System.Drawing.Point(8, 166);
		this.FuncBtn2.Name = "FuncBtn2";
		this.FuncBtn2.ShapeImage = (System.Drawing.Image)resources.GetObject("FuncBtn2.ShapeImage");
		this.FuncBtn2.ShowFocusRect = false;
		this.FuncBtn2.ShowOutline = false;
		this.FuncBtn2.Size = new System.Drawing.Size(161, 28);
		this.FuncBtn2.TabIndex = 3;
		this.FuncBtn2.Text = "\u3000\u3000 基本資料庫維護";
		this.FuncBtn2.MouseLeave += new System.EventHandler(FuncBtn2_MouseLeave);
		this.FuncBtn2.Click += new System.EventHandler(FuncBtn2_Click);
		this.FuncBtn2.MouseEnter += new System.EventHandler(FuncBtn2_MouseEnter);
		appearance22.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance22.FontData.Name = "Arial";
		appearance22.FontData.SizeInPoints = 9f;
		appearance22.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance22.ImageBackground");
		appearance22.TextHAlign = Infragistics.Win.HAlign.Left;
		this.FuncBtn8.Appearance = appearance22;
		this.FuncBtn8.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
		appearance23.BorderAlpha = Infragistics.Win.Alpha.Transparent;
		appearance23.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance23.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance23.ImageBackground");
		this.FuncBtn8.HotTrackAppearance = appearance23;
		this.FuncBtn8.HotTracking = true;
		this.FuncBtn8.Location = new System.Drawing.Point(8, 200);
		this.FuncBtn8.Name = "FuncBtn8";
		this.FuncBtn8.ShapeImage = (System.Drawing.Image)resources.GetObject("FuncBtn8.ShapeImage");
		this.FuncBtn8.ShowFocusRect = false;
		this.FuncBtn8.ShowOutline = false;
		this.FuncBtn8.Size = new System.Drawing.Size(161, 28);
		this.FuncBtn8.TabIndex = 2;
		this.FuncBtn8.Text = "\u3000\u3000 歷史工程單位造價";
		this.FuncBtn8.MouseLeave += new System.EventHandler(FuncBtn2_MouseLeave);
		this.FuncBtn8.Click += new System.EventHandler(FuncBtn8_Click);
		this.FuncBtn8.MouseEnter += new System.EventHandler(FuncBtn2_MouseEnter);
		appearance24.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance24.FontData.Name = "Arial";
		appearance24.FontData.SizeInPoints = 9f;
		appearance24.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance24.ImageBackground");
		appearance24.TextHAlign = Infragistics.Win.HAlign.Left;
		this.FuncBtn4.Appearance = appearance24;
		this.FuncBtn4.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
		appearance25.BorderAlpha = Infragistics.Win.Alpha.Transparent;
		appearance25.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance25.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance25.ImageBackground");
		this.FuncBtn4.HotTrackAppearance = appearance25;
		this.FuncBtn4.HotTracking = true;
		this.FuncBtn4.Location = new System.Drawing.Point(8, 86);
		this.FuncBtn4.Name = "FuncBtn4";
		this.FuncBtn4.ShapeImage = (System.Drawing.Image)resources.GetObject("FuncBtn4.ShapeImage");
		this.FuncBtn4.ShowFocusRect = false;
		this.FuncBtn4.ShowOutline = false;
		this.FuncBtn4.Size = new System.Drawing.Size(161, 28);
		this.FuncBtn4.TabIndex = 1;
		this.FuncBtn4.Text = "\u3000\u3000 投標單填寫";
		this.FuncBtn4.MouseLeave += new System.EventHandler(FuncBtn2_MouseLeave);
		this.FuncBtn4.Click += new System.EventHandler(FuncBtn4_Click);
		this.FuncBtn4.MouseEnter += new System.EventHandler(FuncBtn2_MouseEnter);
		appearance26.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance26.FontData.Name = "Arial";
		appearance26.FontData.SizeInPoints = 9f;
		appearance26.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance26.ImageBackground");
		appearance26.TextHAlign = Infragistics.Win.HAlign.Left;
		this.FuncBtn3.Appearance = appearance26;
		this.FuncBtn3.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
		appearance27.BorderAlpha = Infragistics.Win.Alpha.Transparent;
		appearance27.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance27.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance27.ImageBackground");
		this.FuncBtn3.HotTrackAppearance = appearance27;
		this.FuncBtn3.HotTracking = true;
		this.FuncBtn3.Location = new System.Drawing.Point(8, 44);
		this.FuncBtn3.Name = "FuncBtn3";
		this.FuncBtn3.ShapeImage = (System.Drawing.Image)resources.GetObject("FuncBtn3.ShapeImage");
		this.FuncBtn3.ShowFocusRect = false;
		this.FuncBtn3.ShowOutline = false;
		this.FuncBtn3.Size = new System.Drawing.Size(161, 28);
		this.FuncBtn3.TabIndex = 0;
		this.FuncBtn3.Text = "\u3000\u3000 預算書編製";
		this.FuncBtn3.MouseLeave += new System.EventHandler(FuncBtn2_MouseLeave);
		this.FuncBtn3.Click += new System.EventHandler(FuncBtn3_Click);
		this.FuncBtn3.MouseEnter += new System.EventHandler(FuncBtn2_MouseEnter);
		this.PNL2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.PNL2.Controls.Add(this.lblDescript);
		this.PNL2.Controls.Add(this.lblFuncName);
		this.PNL2.Location = new System.Drawing.Point(736, 362);
		this.PNL2.Name = "PNL2";
		this.PNL2.Size = new System.Drawing.Size(228, 140);
		this.PNL2.TabIndex = 2;
		this.lblDescript.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lblDescript.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblDescript.Location = new System.Drawing.Point(0, 23);
		this.lblDescript.Name = "lblDescript";
		this.lblDescript.Size = new System.Drawing.Size(228, 117);
		this.lblDescript.TabIndex = 0;
		this.lblDescript.Text = "[功能說明]";
		appearance28.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance28.FontData.Name = "Arial";
		appearance28.FontData.SizeInPoints = 9f;
		this.lblFuncName.Appearance = appearance28;
		this.lblFuncName.Dock = System.Windows.Forms.DockStyle.Top;
		this.lblFuncName.Location = new System.Drawing.Point(0, 0);
		this.lblFuncName.Name = "lblFuncName";
		this.lblFuncName.Size = new System.Drawing.Size(228, 23);
		this.lblFuncName.TabIndex = 2;
		this.lblFuncName.Text = "ultraLabel1";
		this.img800.BorderShadowColor = System.Drawing.Color.Empty;
		this.img800.Image = resources.GetObject("img800.Image");
		this.img800.Location = new System.Drawing.Point(604, 716);
		this.img800.Name = "img800";
		this.img800.Size = new System.Drawing.Size(100, 50);
		this.img800.TabIndex = 3;
		this.img800.Visible = false;
		this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
		this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.img1000.BorderShadowColor = System.Drawing.Color.Empty;
		this.img1000.Image = resources.GetObject("img1000.Image");
		this.img1000.Location = new System.Drawing.Point(716, 716);
		this.img1000.Name = "img1000";
		this.img1000.Size = new System.Drawing.Size(100, 50);
		this.img1000.TabIndex = 4;
		this.img1000.Visible = false;
		this.panel1.BackColor = System.Drawing.Color.FromArgb(7, 72, 87);
		this.panel1.Controls.Add(this.linkQuestionnaire);
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Controls.Add(this.ultraLabel2);
		this.panel1.Controls.Add(this.ultraLabel3);
		this.panel1.Controls.Add(this.linkLabel1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 709);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(1512, 37);
		this.panel1.TabIndex = 5;
		appearance29.Cursor = System.Windows.Forms.Cursors.Default;
		appearance29.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance29.FontData.Name = "Verdana";
		appearance29.FontData.SizeInPoints = 10f;
		appearance29.ForeColor = System.Drawing.Color.White;
		appearance29.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.linkQuestionnaire.Appearance = appearance29;
		this.linkQuestionnaire.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 136);
		appearance30.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance30.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance30.FontData.Name = "Verdana";
		appearance30.FontData.SizeInPoints = 10f;
		appearance30.ForeColor = System.Drawing.Color.Orange;
		this.linkQuestionnaire.HotTrackAppearance = appearance30;
		this.linkQuestionnaire.HotTracking = true;
		this.linkQuestionnaire.Location = new System.Drawing.Point(88, 12);
		this.linkQuestionnaire.Name = "linkQuestionnaire";
		this.linkQuestionnaire.Size = new System.Drawing.Size(83, 15);
		this.linkQuestionnaire.TabIndex = 39;
		this.linkQuestionnaire.Text = "使用者問卷";
		this.linkQuestionnaire.Visible = false;
		this.linkQuestionnaire.Click += new System.EventHandler(linkQuestionnaire_Click);
		appearance31.Cursor = System.Windows.Forms.Cursors.Default;
		appearance31.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance31.FontData.Name = "Verdana";
		appearance31.FontData.SizeInPoints = 10f;
		appearance31.ForeColor = System.Drawing.Color.White;
		appearance31.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance31;
		this.ultraLabel1.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 136);
		appearance32.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance32.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance32.FontData.Name = "Verdana";
		appearance32.FontData.SizeInPoints = 10f;
		appearance32.ForeColor = System.Drawing.Color.Orange;
		this.ultraLabel1.HotTrackAppearance = appearance32;
		this.ultraLabel1.HotTracking = true;
		this.ultraLabel1.Location = new System.Drawing.Point(8, 12);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(74, 15);
		this.ultraLabel1.TabIndex = 38;
		this.ultraLabel1.Text = "最新消息";
		this.ultraLabel1.Visible = false;
		this.ultraLabel1.Click += new System.EventHandler(ultraLabel1_Click);
		this.ultraLabel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance33.Cursor = System.Windows.Forms.Cursors.Default;
		appearance33.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance33.FontData.Name = "Verdana";
		appearance33.FontData.SizeInPoints = 10f;
		appearance33.ForeColor = System.Drawing.Color.White;
		appearance33.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance33;
		appearance34.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance34.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance34.FontData.Name = "Verdana";
		appearance34.FontData.SizeInPoints = 10f;
		appearance34.ForeColor = System.Drawing.Color.Orange;
		this.ultraLabel2.HotTrackAppearance = appearance34;
		this.ultraLabel2.HotTracking = true;
		this.ultraLabel2.Location = new System.Drawing.Point(860, 12);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(120, 15);
		this.ultraLabel2.TabIndex = 37;
		this.ultraLabel2.Text = "PCCES客服中心";
		this.ultraLabel2.Click += new System.EventHandler(ultraLabel2_Click);
		this.ultraLabel3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance35.BackColor = System.Drawing.Color.Transparent;
		appearance35.BackColor2 = System.Drawing.Color.Transparent;
		appearance35.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance35.ForeColor = System.Drawing.Color.White;
		appearance35.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance35.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance35;
		this.ultraLabel3.Font = new System.Drawing.Font("Verdana", 9f);
		appearance36.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance36.FontData.Underline = Infragistics.Win.DefaultableBoolean.False;
		appearance36.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel3.HotTrackAppearance = appearance36;
		this.ultraLabel3.Location = new System.Drawing.Point(996, 8);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(284, 25);
		this.ultraLabel3.TabIndex = 36;
		this.ultraLabel3.Text = "TEL:(02)2708-8090    FAX:(02)2708-8659";
		this.linkLabel1.ActiveLinkColor = System.Drawing.Color.Orange;
		this.linkLabel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.linkLabel1.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.linkLabel1.LinkColor = System.Drawing.Color.White;
		this.linkLabel1.Location = new System.Drawing.Point(1287, 7);
		this.linkLabel1.Name = "linkLabel1";
		this.linkLabel1.Size = new System.Drawing.Size(217, 23);
		this.linkLabel1.TabIndex = 35;
		((System.Windows.Forms.Label)this.linkLabel1).TabStop = true;
		this.linkLabel1.Text = "Email:service@archnowledge.com";
		this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.linkLabel1.VisitedLinkColor = System.Drawing.Color.White;
		this.linkLabel1.MouseLeave += new System.EventHandler(linkLabel1_MouseLeave);
		this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
		this.linkLabel1.MouseEnter += new System.EventHandler(linkLabel1_MouseEnter);
		appearance37.BackColor = System.Drawing.Color.Transparent;
		appearance37.BackColor2 = System.Drawing.Color.Transparent;
		appearance37.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance37.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance37.ForeColor = System.Drawing.Color.Navy;
		appearance37.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance37.ImageBackground");
		appearance37.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance37.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel13.Appearance = appearance37;
		this.ultraLabel13.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance38.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance38.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance38.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel13.HotTrackAppearance = appearance38;
		this.ultraLabel13.HotTracking = true;
		this.ultraLabel13.Location = new System.Drawing.Point(2, 4);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(80, 24);
		this.ultraLabel13.TabIndex = 28;
		this.ultraLabel13.Text = "面板切換";
		this.ultraLabel13.Click += new System.EventHandler(ultraLabel13_Click);
		this.ultraPictureBox2.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox2.BorderShadowDepth = 0;
		this.ultraPictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraPictureBox2.Image = resources.GetObject("ultraPictureBox2.Image");
		this.ultraPictureBox2.Location = new System.Drawing.Point(0, 0);
		this.ultraPictureBox2.Name = "ultraPictureBox2";
		this.ultraPictureBox2.ScaleImage = Infragistics.Win.ScaleImage.Never;
		this.ultraPictureBox2.Size = new System.Drawing.Size(1512, 746);
		this.ultraPictureBox2.TabIndex = 0;
		this.ultraPictureBox2.Visible = false;
		appearance39.ForeColor = System.Drawing.Color.Red;
		appearance39.ImageAlpha = Infragistics.Win.Alpha.Transparent;
		appearance39.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance39.ImageBackground");
		appearance39.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.lblUseDatabase.Appearance = appearance39;
		this.lblUseDatabase.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblUseDatabase.Location = new System.Drawing.Point(176, 680);
		this.lblUseDatabase.Name = "lblUseDatabase";
		this.lblUseDatabase.Size = new System.Drawing.Size(600, 24);
		this.lblUseDatabase.TabIndex = 30;
		this.lblUseDatabase.Text = "目前資料庫:";
		this.lblUseDatabase.WrapText = false;
		this.panel2.BackgroundImage = (System.Drawing.Image)resources.GetObject("panel2.BackgroundImage");
		this.panel2.Controls.Add(this.ultraPictureBox4);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(1512, 55);
		this.panel2.TabIndex = 32;
		this.ultraPictureBox4.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox4.Dock = System.Windows.Forms.DockStyle.Right;
		this.ultraPictureBox4.Image = resources.GetObject("ultraPictureBox4.Image");
		this.ultraPictureBox4.Location = new System.Drawing.Point(1208, 0);
		this.ultraPictureBox4.Name = "ultraPictureBox4";
		this.ultraPictureBox4.ScaleImage = Infragistics.Win.ScaleImage.Always;
		this.ultraPictureBox4.Size = new System.Drawing.Size(304, 55);
		this.ultraPictureBox4.TabIndex = 34;
		this.ultraPictureBox3.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox3.Image = resources.GetObject("ultraPictureBox3.Image");
		this.ultraPictureBox3.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.ultraPictureBox3.Location = new System.Drawing.Point(0, 0);
		this.ultraPictureBox3.Name = "ultraPictureBox3";
		this.ultraPictureBox3.ScaleImage = Infragistics.Win.ScaleImage.Always;
		this.ultraPictureBox3.Size = new System.Drawing.Size(252, 70);
		this.ultraPictureBox3.TabIndex = 33;
		this.ultraPictureBox5.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox5.Dock = System.Windows.Forms.DockStyle.Right;
		this.ultraPictureBox5.Image = resources.GetObject("ultraPictureBox5.Image");
		this.ultraPictureBox5.Location = new System.Drawing.Point(924, 0);
		this.ultraPictureBox5.Name = "ultraPictureBox5";
		this.ultraPictureBox5.ScaleImage = Infragistics.Win.ScaleImage.Always;
		this.ultraPictureBox5.Size = new System.Drawing.Size(588, 48);
		this.ultraPictureBox5.TabIndex = 35;
		this.panel3.BackgroundImage = (System.Drawing.Image)resources.GetObject("panel3.BackgroundImage");
		this.panel3.Controls.Add(this.ultraPictureBox5);
		this.panel3.Controls.Add(this.ultraLabel13);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel3.Location = new System.Drawing.Point(0, 661);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(1512, 48);
		this.panel3.TabIndex = 36;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.FromArgb(7, 72, 87);
		this.label1.Font = new System.Drawing.Font("Arial", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.White;
		this.label1.Location = new System.Drawing.Point(128, 39);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(68, 19);
		this.label1.TabIndex = 37;
		this.label1.Text = "Win 4.3 ";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.timer1.Enabled = true;
		this.timer1.Interval = 200;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.ClientSize = new System.Drawing.Size(1512, 746);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.ultraPictureBox3);
		base.Controls.Add(this.lblUseDatabase);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.PNL2);
		base.Controls.Add(this.PNL1);
		base.Controls.Add(this.img1000);
		base.Controls.Add(this.img800);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.ultraPictureBox1);
		base.Controls.Add(this.ultraPictureBox2);
		base.KeyPreview = true;
		base.Name = "FormPanel2";
		this.Text = "FormPanel2";
		base.Load += new System.EventHandler(FormPanel2_Load);
		base.SizeChanged += new System.EventHandler(FormPanel2_SizeChanged);
		base.Activated += new System.EventHandler(FormPanel2_Activated);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormPanel2_KeyDown);
		this.PNL1.ResumeLayout(false);
		this.PNL2.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
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
