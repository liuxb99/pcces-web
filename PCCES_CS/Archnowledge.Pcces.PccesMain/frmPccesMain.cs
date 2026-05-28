using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Management;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DatabaseAccess;
using Archnowledge.Pcces.DomainModule.DatabaseChange;
using Archnowledge.Pcces.DomainModule.DatabaseUpgrade;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.Library;
using Archnowledge.Pcces.PccesMain.PccesUpdateServices;
using Archnowledge.Pcces.PccesMain.SysMaintain;
using Archnowledge.Pcces.PowerClass;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win.UltraWinTabbedMdi;
using Infragistics.Win.UltraWinTabs;
using Microsoft.Win32;

namespace Archnowledge.Pcces.PccesMain;

public class frmPccesMain : Form
{
	private UltraTabbedMdiManager ultraTabbedMdiManager1;

	private IContainer components;

	private string FORM_STATUS = "INI";

	private string Frm_FunctionName = "PccesMain";

	private RegistryKey local_machine = Registry.LocalMachine;

	private string pccesINIFile = "PccesMain.ini";

	private string F_Freeze = "";

	private int iAlertCount = 0;

	private bool F_LoginIsCancel = false;

	private string F_HomePanel = "2";

	private bool F_HasRegistered;

	private string F_UserID;

	private string F_UserName = "";

	private string F_ServerName = "";

	private bool F_PreConnectOK = false;

	private bool FunctionListChanged = false;

	private FormSplash FM_SPL;

	public Panel LeftPanel;

	private OnlineList onlineList1;

	public FunctionButtons functionButtons1;

	private FormPanel FM_PNL1;

	private FormPanel2 FM_PNL2;

	private FormPanel3 FM_PNL3;

	private System.Windows.Forms.Timer timer1;

	private FormSys_G_Info1 FM_INFO = null;

	public bool _LoginIsCancel
	{
		get
		{
			return F_LoginIsCancel;
		}
		set
		{
			F_LoginIsCancel = value;
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
			onlineList1._UserID = F_UserID;
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
			onlineList1._UserName = F_UserName;
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

	public bool _PreConnectOK
	{
		get
		{
			return F_PreConnectOK;
		}
		set
		{
			F_PreConnectOK = value;
		}
	}

	public string _FORM_STATUS
	{
		get
		{
			return FORM_STATUS;
		}
		set
		{
			FORM_STATUS = value;
		}
	}

	public void DisableMain()
	{
		F_Freeze = "FREEZE";
		string sIniFileName = CommonMethods.ExtractFilePath(Application.ExecutablePath) + pccesINIFile;
		F_HomePanel = CommonMethods.IniReadValue(sIniFileName, "HomePanel", "Home");
		if (F_HomePanel == "")
		{
			F_HomePanel = "2";
		}
		if (F_HomePanel == "2")
		{
			try
			{
				FM_PNL2.PNL1.Enabled = false;
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "FormPccesMain.cs" + ex.Message);
			}
		}
	}

	public void EnableMain()
	{
		F_Freeze = "";
		string sIniFileName = CommonMethods.ExtractFilePath(Application.ExecutablePath) + pccesINIFile;
		F_HomePanel = CommonMethods.IniReadValue(sIniFileName, "HomePanel", "Home");
		if (F_HomePanel == "")
		{
			F_HomePanel = "2";
		}
		if (F_HomePanel == "2")
		{
			try
			{
				FM_PNL2.PNL1.Enabled = true;
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "FormPccesMain.cs" + ex.Message);
			}
		}
	}

	public frmPccesMain()
	{
		CommonMethods.LogFile("Pcces46", "M", "CheckPccesUser:A");
		InitializeComponent();
		CommonMethods.LogFile("Pcces46", "M", "CheckPccesUser:B");
		functionButtons1.ButtonOwner = LeftPanelStatus.None;
		CommonMethods.LogFile("Pcces46", "M", "CheckPccesUser:C");
		F_ServerName = PubTools.GetAppSet_String("ServerName");
		CommonMethods.LogFile("Pcces46", "M", "CheckPccesUser:D");
		FM_SPL = new FormSplash();
		FM_SPL.Owner = this;
		FM_SPL.Show();
		Application.DoEvents();
		LoadingForm();
		CommonMethods.LogFile("Pcces46", "M", "CheckPccesUser:E");
	}

	private static bool CheckPccesUser()
	{
		bool HasPccesUserExisted = false;
		try
		{
			CommonMethods.LogFile("Pcces46", "M", "CheckPccesUser:1");
			string PccesConnectionString = ConfigurationManager.ConnectionStrings["Pcces"].ConnectionString;
			CommonMethods.LogFile("Pcces46", "M", "CheckPccesUser:2");
			PccesBaseHelper baseHelper = new PccesBaseHelper(PccesConnectionString);
			CommonMethods.LogFile("Pcces46", "M", "CheckPccesUser:3");
			baseHelper.RemoveNotExistPcces();
			CommonMethods.LogFile("Pcces46", "M", "CheckPccesUser:4");
			baseHelper.CheckPccesUser();
			CommonMethods.LogFile("Pcces46", "M", "CheckPccesUser:5");
			HasPccesUserExisted = true;
			CommonMethods.LogFile("Pcces46", "M", "CheckPccesUser:6");
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "FormPccesMain.cs" + ex.Message);
		}
		return HasPccesUserExisted;
	}

	public void LoadingForm()
	{
		if (!F_PreConnectOK)
		{
			return;
		}
		DetectSQLMem();
		string sIniFileName = AppDomain.CurrentDomain.BaseDirectory + pccesINIFile;
		F_HomePanel = CommonMethods.IniReadValue(sIniFileName, "HomePanel", "Home");
		if (F_HomePanel == "")
		{
			F_HomePanel = "2";
		}
		if (F_HomePanel == "1")
		{
			bool IsFound = false;
			Form[] mdiChildren = base.MdiChildren;
			foreach (Form F1 in mdiChildren)
			{
				if (F1 is FormPanel)
				{
					IsFound = true;
					break;
				}
			}
			if (!IsFound)
			{
				FM_PNL1 = new FormPanel();
				FM_PNL1._UserID = F_UserID;
				FM_PNL1.MdiParent = this;
				FM_PNL1.Show();
			}
		}
		else if (F_HomePanel == "2")
		{
			bool IsFound = false;
			Form[] mdiChildren = base.MdiChildren;
			foreach (Form F1 in mdiChildren)
			{
				if (F1 is FormPanel2)
				{
					IsFound = true;
					break;
				}
			}
			if (!IsFound)
			{
				FM_PNL2 = new FormPanel2();
				FM_PNL2._UserID = F_UserID;
				FM_PNL2.MdiParent = this;
				FM_PNL2.Show();
			}
		}
		else
		{
			if (!(F_HomePanel == "3"))
			{
				return;
			}
			bool IsFound = false;
			Form[] mdiChildren = base.MdiChildren;
			foreach (Form F1 in mdiChildren)
			{
				if (F1 is FormPanel3)
				{
					IsFound = true;
					break;
				}
			}
			if (!IsFound)
			{
				FM_PNL3 = new FormPanel3();
				FM_PNL3._UserID = F_UserID;
				FM_PNL3.MdiParent = this;
				FM_PNL3.Show();
			}
		}
	}

	[STAThread]
	private static void Main(string[] args)
	{
		CommonMethods.LogFile("Pcces46", "M", "程式進入點");
		if (!CheckPccesUser())
		{
			MessageBox.Show("無法正常開啟資料庫，請確定資料庫連線是否正確，指定的資料庫是否為正確定的 PCCES 資料庫。系統將自動停止目前的程式，並且呼叫 ConfigEditor，重新設定資料庫連線。", "資料庫錯誤");
			Process.Start("ConfigEditor.exe");
			Application.Exit();
			return;
		}
		if (Archnowledge.Pcces.DatabaseAccess.DatabaseAccess.GetSQLVersion() == "2000")
		{
			MessageBox.Show("此版不支援SQL 2000", "Pcces Win 4.3", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		Mutex mutex = new Mutex(initiallyOwned: true, "PccesMain");
		if (mutex.WaitOne(0, exitContext: false))
		{
			CultureInfo zh_TWCustomCultrueInfo = new CultureInfo("zh-TW");
			zh_TWCustomCultrueInfo.DateTimeFormat.Calendar = new GregorianCalendar(GregorianCalendarTypes.Localized);
			Thread.CurrentThread.CurrentCulture = zh_TWCustomCultrueInfo;
			Application.Run(new frmPccesMain());
		}
		else
		{
			MessageBox.Show("已有相同 Pcces 程式正在執行中...\n無法重複執行！", "Pcces Win 4.3", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void DetectSQLMem()
	{
		string sIsSQLMemAdjusted = CommonMethods.GetIniValue("SQL", "MemAdjusted").ToUpper();
		if (!IsLocalDB() || !(sIsSQLMemAdjusted != "TRUE"))
		{
			return;
		}
		try
		{
			double dTotalSize = 0.0;
			dTotalSize = CommonMethods.Get_Physical_Memory();
			int IMem = 0;
			if (dTotalSize > 0.0 && dTotalSize <= 128.0)
			{
				IMem = 64;
			}
			if (dTotalSize > 128.0 && dTotalSize <= 256.0)
			{
				IMem = 128;
			}
			if (dTotalSize > 256.0 && dTotalSize <= 512.0)
			{
				IMem = 256;
			}
			if (dTotalSize > 512.0)
			{
				IMem = 384;
			}
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = F_UserID;
			DBCLS.ExecuteCommand("EXEC sp_configure 'show advanced options', 1" + '\r' + "RECONFIGURE WITH OVERRIDE");
			DBCLS.ExecuteCommand("EXEC sp_configure 'max server memory (MB)'," + IMem.ToString() + '\r');
			DBCLS.ExecuteCommand("EXEC sp_configure 'show advanced options', 0" + '\r' + "RECONFIGURE WITH OVERRIDE");
			DBCLS = null;
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "FormPccesMain.cs" + ex.Message);
			Console.Write(ex.Message);
		}
		CommonMethods.WriteIniValue("SQL", "MemAdjusted", "TRUE");
	}

	private bool IsLocalDB()
	{
		DBClass DBCLS = new DBClass();
		string serverName = DBCLS.GetDBConnectionServer().ToLower();
		if (!serverName.StartsWith(Environment.MachineName.ToLower()))
		{
			switch (serverName)
			{
			default:
				if (!serverName.StartsWith(".\\") && !(serverName == "127.0.0.1"))
				{
					return false;
				}
				break;
			case "localhost":
			case "(local)":
			case ".":
				break;
			}
		}
		return true;
	}

	private void frmPccesMain_Load(object sender, EventArgs e)
	{
		ultraTabbedMdiManager1.TabGroupSettings.TabStyle = TabStyle.Wizard;
		FM_SPL.Close();
		string ChatServer = CommonMethods.GetIniValue("User", "ChatServer");
		if ((ChatServer.ToUpper() != "FALSE" || ChatServer == "") && !F_PreConnectOK)
		{
			FORM_STATUS = "CLOSE";
			Close();
			return;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("HomePanel", "LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("HomePanel", "LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("HomePanel", "Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("HomePanel", "Height"));
		string sStatus = CommonMethods.GetIniValue("HomePanel", "FormStatus");
		if (sStatus != "")
		{
			if (sStatus == "NORMAL")
			{
				base.WindowState = FormWindowState.Normal;
			}
			else
			{
				base.WindowState = FormWindowState.Maximized;
			}
		}
		else
		{
			base.WindowState = FormWindowState.Maximized;
		}
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
		Text = "PCCES Win 4.3 ";
		onlineList1._ServerName = PubTools.GetAppSet_String("ServerName");
		onlineList1._FunctionName = Frm_FunctionName;
		if (!onlineList1.Connect() && ChatServer.ToUpper() != "FALSE")
		{
			FORM_STATUS = "CLOSE";
			Close();
		}
	}

	private void frmPccesMain_FormClosing(object sender, FormClosingEventArgs e)
	{
		CommonMethods.WriteIniValue("HomePanel", "LocationX", base.Location.X.ToString());
		CommonMethods.WriteIniValue("HomePanel", "LocationY", base.Location.Y.ToString());
		CommonMethods.WriteIniValue("HomePanel", "Width", base.Size.Width.ToString());
		CommonMethods.WriteIniValue("HomePanel", "Height", base.Size.Height.ToString());
		CommonMethods.WriteIniValue("HomePanel", "FormStatus", (base.WindowState == FormWindowState.Normal) ? "NORMAL" : "MAX");
		if (F_Freeze.Trim() != "")
		{
			e.Cancel = true;
		}
		else
		{
			if (FORM_STATUS == "CLOSE")
			{
				return;
			}
			if (FORM_STATUS == "BDGT_DONT_CLOSE")
			{
				e.Cancel = true;
			}
			else if (DialogResult.Yes == MessageBox.Show(this, "確定要結束 Pcces Win 4.3？", "PCCES Win 4.3 ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
				Form[] mdiChildren = base.MdiChildren;
				foreach (Form frm in mdiChildren)
				{
					frm.Close();
				}
			}
			else
			{
				e.Cancel = true;
			}
		}
	}

	public string GetUpdateVersion()
	{
		string NowVer = "1.0";
		string sToDay = $"{DateTime.Today:d}";
		string sDate = CommonMethods.GetIniValue("UpdateCheck", "CheckDate");
		bool IsShouldCheck = false;
		if (sDate.Trim() == "")
		{
			IsShouldCheck = true;
		}
		else
		{
			try
			{
				IsShouldCheck = !(Convert.ToDateTime(sDate) >= DateTime.Today);
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "FormPccesMain.cs" + ex.Message);
				IsShouldCheck = true;
			}
		}
		if (IsShouldCheck)
		{
			Update serviceRequest = new Update();
			string webServiceRoute = CommonMethods.GetIniValue("DownloadInfo", "webServiceRoute");
			if (webServiceRoute == "")
			{
				webServiceRoute = "http://bisc.archnowledge.com/pccesupdateservices/Update.asmx";
			}
			serviceRequest.Url = webServiceRoute;
			if (CommonMethods.GetIniValue("ProxyInfo", "usingProxy").Trim().ToLower() == "true")
			{
				serviceRequest.Proxy = GetProxy();
			}
			NowVer = serviceRequest.GetPccesVersion();
			try
			{
				string RegID = CommonMethods.GetIniValue("Register", "RegID");
				if (RegID != "")
				{
					string UserName = CommonMethods.GetIniValue("Register", "UserName");
					string EMail = CommonMethods.GetIniValue("Register", "EMail");
					string MAC = ArchNet.GetMacAddress();
					if (!serviceRequest.IsStillValid(RegID, UserName, EMail, MAC))
					{
						NowVer = "1.0";
						string sMess = "您的註冊資訊被更動過，是否清空註冊資訊？";
						if (UserName.Length >= 3 && UserName.Substring(0, 3).ToUpper() == "TR-")
						{
							sMess = "您的教育訓練帳號已過期，是否清空註冊資訊？";
						}
						if (MessageBox.Show(this, sMess, "警示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
						{
							CommonMethods.WriteIniValue("Register", "RegID", "");
							CommonMethods.WriteIniValue("Register", "UserName", "");
							CommonMethods.WriteIniValue("Register", "EMail", "");
							CommonMethods.WriteIniValue("Register", "CompanyName", "");
							CommonMethods.WriteIniValue("Register", "Dept", "");
							CommonMethods.WriteIniValue("Register", "TEL", "");
						}
					}
				}
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "FormPccesMain.cs" + ex.Message);
				Console.Write(ex.Message);
			}
		}
		if (IsShouldCheck)
		{
			CommonMethods.WriteIniValue("UpdateCheck", "CheckDate", sToDay);
		}
		return NowVer;
	}

	private WebProxy GetProxy()
	{
		WebProxy proxy = new WebProxy();
		string port = CommonMethods.GetIniValue("ProxyInfo", "port");
		string account = CommonMethods.GetIniValue("ProxyInfo", "account");
		string password = CommonMethods.GetIniValue("ProxyInfo", "password");
		string address = CommonMethods.GetIniValue("ProxyInfo", "address");
		proxy.Address = new Uri(address + ":" + port);
		proxy.Credentials = new NetworkCredential(account, password);
		return proxy;
	}

	private void CheckUpdate()
	{
		if (F_LoginIsCancel || iAlertCount > 0 || !(FORM_STATUS == "ACT"))
		{
			return;
		}
		iAlertCount++;
		try
		{
			string latestVersion = GetUpdateVersion();
			string PccesAssemblyVersion = PccesVersion.PccesAssemblyVersion;
			if (PccesVersion.CompareVersion(latestVersion, PccesAssemblyVersion))
			{
				FORM_STATUS = "NOR";
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "FormPccesMain.cs" + ex.Message);
		}
	}

	private void sendUpdateInfo()
	{
		string webServiceRoute = CommonMethods.GetIniValue("DownloadInfo", "webServiceRoute");
		if (webServiceRoute == "")
		{
			webServiceRoute = "http://bisc.archnowledge.com/pccesupdateservices/Update.asmx";
		}
		object obj = webServiceRoute;
		webServiceRoute = string.Concat(obj, "?OSversion=", Environment.OSVersion.Platform, "&MachineName=", Environment.MachineName, "&UserName=", CommonMethods.GetIniValue("Register", "UserName"), "&EMail=", CommonMethods.GetIniValue("Register", "EMail"), "&MACAddress=", GetMacAddress(), "&InternalIP=", CommonMethods.GetIPAddress(), "&CurrentVer=", PccesVersion.PccesAssemblyVersion);
		WebRequest testRequest = WebRequest.Create(webServiceRoute);
		if (CommonMethods.GetIniValue("ProxyInfo", "usingProxy").Trim().ToLower() == "true")
		{
			testRequest.Proxy = GetProxy();
		}
		else
		{
			testRequest.UseDefaultCredentials = true;
		}
		try
		{
			testRequest.GetResponse();
		}
		catch (Exception ex)
		{
			Archnowledge.Pcces.CommonClass.DebugUtil.OutputDebugString("傳送更新紀錄失敗。" + ex.Message);
		}
	}

	public string GetMacAddress()
	{
		string sIP = "";
		string sMAC = "";
		ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
		ManagementObjectCollection queryCollection = query.Get();
		foreach (ManagementObject mo in queryCollection)
		{
			if ((bool)mo["IPEnabled"])
			{
				string[] addresses = (string[])mo["IPAddress"];
				string[] subnets = (string[])mo["IPSubnet"];
				if (addresses[0].ToString() != "")
				{
					sIP = addresses[0];
					sMAC = mo["MacAddress"].ToString();
					break;
				}
			}
		}
		return sMAC;
	}

	private bool HasRegistered()
	{
		return (CommonMethods.GetIniValue("Register", "RegID").Trim() != "") ? true : false;
	}

	private void frmPccesMain_Activated(object sender, EventArgs e)
	{
		if (FORM_STATUS == "INI")
		{
			if (F_PreConnectOK)
			{
				if (F_HomePanel == "1")
				{
					FM_PNL1.Show();
					FM_PNL1.BringToFront();
				}
				if (F_HomePanel == "2")
				{
					FM_PNL2.Show();
					FM_PNL2.BringToFront();
				}
				if (F_HomePanel == "3")
				{
					FM_PNL3.Show();
					FM_PNL3.BringToFront();
				}
				FORM_STATUS = "ACT";
			}
		}
		else if (FORM_STATUS == "CLOSE")
		{
			Close();
		}
		else if (FORM_STATUS == "ACT")
		{
			CheckUpdate();
			FORM_STATUS = "NOR";
		}
	}

	private void ProgressEventHandler(string Message, ref int Progress)
	{
		if (FM_INFO != null)
		{
			FM_INFO.SetValue(Message, Progress);
		}
	}

	private void functionButtons1_Load(object sender, EventArgs e)
	{
		if (!F_PreConnectOK)
		{
			FORM_STATUS = "CLOSE";
			return;
		}
		ArrayList tmp_AL = new ArrayList();
		tmp_AL.Add("System");
		tmp_AL.Add("檢查資料庫檔案結構");
		tmp_AL[1] = "檢查權限";
		StaffClass StaffCom = new StaffClass(tmp_AL);
		int chkStaff = StaffCom.WishRunLogon();
		F_ServerName = PubTools.GetAppSet_String("ServerName");
		if (chkStaff < 0)
		{
			F_UserID = "PccAdmin";
			F_UserName = "匿名登入";
			onlineList1._UserID = F_UserID;
			onlineList1._UserName = F_UserName;
		}
		else if (chkStaff == 2)
		{
			string sWarning = "未設定系統管理者帳號！\n系統自動產生為：PccesUser\n密碼為：12345";
			MessageBox.Show(this, sWarning, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			FORM_STATUS = "CLOSE";
		}
		else
		{
			FormLogin FM_LGN = new FormLogin();
			FM_LGN.Owner = this;
			if (FM_LGN.ShowDialog(this) == DialogResult.OK)
			{
				onlineList1._UserID = F_UserID;
				onlineList1._UserName = F_UserName;
				FM_INFO = new FormSys_G_Info1();
				FM_INFO._InfoString = "檢查系統使用的【資料庫結構】中...\n新版本更新後的第一次會比較久\n，請耐心等候! ";
				FM_INFO.Show();
				Application.DoEvents();
				ExecResult ER = CheckDatabaseVersion();
				if (ER.ReturnCode != 0)
				{
					MessageBox.Show(ER.Message, "錯誤");
				}
				FM_INFO.Close();
				FM_INFO.Dispose();
				FM_INFO = null;
				if (ER.ReturnCode == 0)
				{
					CheckVersion();
				}
				if (!CheckIsRegistered())
				{
					int iWarningTimes = PubTools.Str2Int(CommonMethods.GetIniValue("Register", "WarningTimes"));
					if (iWarningTimes < 1000 && MessageBox.Show(this, "您尚未註冊，您現在要執行註冊嗎？\n\n您亦可稍後再進行註冊，\n要註冊時可點選畫面左上角[未註冊]。", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						FormRegister FM_REG = new FormRegister();
						if (FM_REG.ShowDialog() == DialogResult.OK)
						{
							F_HasRegistered = true;
						}
						FM_REG.Close();
						FM_REG.Dispose();
						FM_REG = null;
					}
					else
					{
						CommonMethods.WriteIniValue("Register", "WarningTimes", (iWarningTimes + 1).ToString());
						F_HasRegistered = false;
					}
				}
				else if (GetDisabled())
				{
					CommonMethods.WriteIniValue("Register", "RegID", "");
					F_HasRegistered = false;
					int iWarningTimes = PubTools.Str2Int(CommonMethods.GetIniValue("Register", "WarningTimes"));
					if (MessageBox.Show(this, "您尚未註冊或是您上次註冊時填寫的資料無法辨識，\n\n請您再重新註冊，\n要註冊時可點選畫面左上角[未註冊]。\n\n現在要進行註冊嗎？", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						FormRegister FM_REG = new FormRegister();
						if (FM_REG.ShowDialog() == DialogResult.OK)
						{
							F_HasRegistered = true;
						}
						FM_REG.Close();
						FM_REG.Dispose();
						FM_REG = null;
					}
					else
					{
						CommonMethods.WriteIniValue("Register", "WarningTimes", (iWarningTimes + 1).ToString());
						F_HasRegistered = false;
					}
				}
				else
				{
					F_HasRegistered = true;
				}
				onlineList1._HasRegistered = F_HasRegistered;
				FM_LGN.Close();
				FM_LGN = null;
			}
			else
			{
				FORM_STATUS = "CLOSE";
			}
		}
		if (FORM_STATUS != "CLOSE")
		{
			functionButtons1._UserID = F_UserID;
			functionButtons1._UserName = F_UserName;
			functionButtons1._ServerName = F_ServerName;
			if (F_HomePanel == "1")
			{
				FM_PNL1._UserID = F_UserID;
			}
			if (F_HomePanel == "2")
			{
				FM_PNL2._UserID = F_UserID;
			}
			if (F_HomePanel == "3")
			{
				FM_PNL3._UserID = F_UserID;
			}
			ModuleManager oManager = new ModuleManager();
			if (oManager.IsFirstRun)
			{
				FormModuleSetup FM_ModuleSetup = new FormModuleSetup();
				if (FM_ModuleSetup.ShowDialog() == DialogResult.OK)
				{
					UpdateMenu();
				}
				FM_ModuleSetup.Close();
				FM_ModuleSetup.Dispose();
				FM_ModuleSetup = null;
			}
		}
		Thread t1 = new Thread(CheckAutoNumUpdate);
		t1.Start();
	}

	private void CheckAutoNumUpdate()
	{
		Thread.Sleep(300);
		string sToDay = $"{DateTime.Today:d}";
		string sDate = CommonMethods.GetIniValue("AutoNumUpdateCheck", "CheckDate");
		bool IsShouldCheck = false;
		if (sDate.Trim() == "")
		{
			IsShouldCheck = true;
		}
		else
		{
			try
			{
				IsShouldCheck = !(Convert.ToDateTime(sDate) == DateTime.Today);
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormaAutoNum.cs" + ex.Message);
				IsShouldCheck = true;
			}
		}
		bool flag = 0 == 0;
	}

	private bool IsNeedToUpdate()
	{
		Cursor = Cursors.WaitCursor;
		bool RetV = false;
		try
		{
			Application.DoEvents();
			Update serviceRequest = new Update();
			Application.DoEvents();
			string webServiceRoute = CommonMethods.GetIniValue("DownloadInfo", "webServiceRoute");
			if (webServiceRoute == "")
			{
				webServiceRoute = "http://bisc.archnowledge.com/pccesupdateservices/Update.asmx";
			}
			serviceRequest.Url = webServiceRoute;
			if (CommonMethods.GetIniValue("ProxyInfo", "usingProxy").Trim().ToLower() == "true")
			{
				serviceRequest.Proxy = GetProxy();
			}
			Application.DoEvents();
			DataSet DS11 = serviceRequest.AutoNumUpd();
			DataSet DSList = DS11.Clone();
			DBClass DBCLS = new DBClass();
			DataTable DT1 = DBCLS.GetUserDefine("Select * from AutoNumUpd");
			DT1.CaseSensitive = true;
			for (int i = 0; i < DS11.Tables[0].Rows.Count; i++)
			{
				DataView DV33 = DT1.DefaultView;
				DateTime DD1 = Convert.ToDateTime(DS11.Tables[0].Rows[i]["ReleaseDate"]);
				string sDate = DD1.Month + "/" + DD1.Day + "/" + DD1.Year;
				string sFLT = "ItemCode ='" + DS11.Tables[0].Rows[i]["ItemCode"].ToString().Trim() + "' And ReleaseDate >= #" + sDate + "# ";
				DV33.RowFilter = sFLT;
				if (DV33.Count == 0)
				{
					RetV = true;
					break;
				}
			}
			DBCLS = null;
			DT1 = null;
			Application.DoEvents();
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormaAutoNum.cs" + ex.Message);
		}
		Cursor = Cursors.Default;
		return RetV;
	}

	private ExecResult CheckDatabaseVersion()
	{
		int Progress = 0;
		string PccesConnectionString = ConfigurationManager.ConnectionStrings["Pcces"].ConnectionString;
		string PccesDatabase = ConnectionStringUtility.GetItem(PccesConnectionString, "Initial Catalog");
		ExecResult ER = new ExecResult();
		UserDefined oUserDefined = new UserDefined();
		int PccesMasterDatabaseVerion = oUserDefined.GetPccesMasterDatabaseVersion();
		if (PccesMasterDatabaseVerion < 1)
		{
			ChgStrSysUser.AlterDataTable(PccesConnectionString);
			ChgStrSysUser.CreateStoredProcedure(PccesConnectionString);
			ChgStrFor42Version.AlterDataTable(PccesConnectionString);
		}
		if (PccesMasterDatabaseVerion < 2)
		{
			ChgStrAutoNum.AlterDataTable(PccesConnectionString);
			ChgStrAutoNum.CreateStoredProcedure(PccesConnectionString);
			ChgStrLogin_Log.AlterDataTable(PccesConnectionString);
		}
		if (PccesMasterDatabaseVerion < 3)
		{
			ChgStrMainUnit.CreateStoredProcedure(PccesConnectionString);
		}
		if (PccesMasterDatabaseVerion < 4)
		{
			ChgStrSublet.AlterDataTable(PccesConnectionString);
			ChgStrSublet.CreateStoredProcedure(PccesConnectionString);
		}
		if (PccesMasterDatabaseVerion < 5)
		{
			ChgStrFuncList.CreateStoredProcedure(PccesConnectionString);
			ChgStrWinUserFuncs.CreateStoredProcedure(PccesConnectionString);
		}
		if (PccesMasterDatabaseVerion < 6)
		{
			ChgStrSysPccesSlave.CreateStoredProcedure(PccesConnectionString);
			oUserDefined.SetPccesMasterDatabaseVersion(6);
		}
		if (PccesMasterDatabaseVerion < 7)
		{
			ChgStrAutoNum.AlterDataTable(PccesConnectionString);
			ChgStrAutoNum.CreateStoredProcedure(PccesConnectionString);
			oUserDefined.SetPccesMasterDatabaseVersion(7);
		}
		string PccesMasterMirror = oUserDefined.GetPccesMasterMirror();
		if (PccesMasterMirror == "")
		{
			if (MessageBox.Show("[" + PccesDatabase + "] 為舊版本資料庫，系統會對此資料庫建立 [複本資料庫]，再以此 [複本資料庫] 進行資料庫版本昇級。", "建立 [複本資料庫]", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
			{
				Close();
				ER.Message = "不執行版本更新";
				ER.ReturnCode = 1;
			}
			PccesBaseHelper baseHelper = new PccesBaseHelper(PccesConnectionString);
			string BackupPath = baseHelper.GetDatabasePath() + "\\";
			ER = DatabaseBackupRestore.BackupDatabase(baseHelper, BackupPath, PccesDatabase, out var BackupFile, ProgressEventHandler, ref Progress);
			if (ER.ReturnCode == 0)
			{
				string NewDatabasename = "";
				BackupFile = BackupPath + BackupFile;
				ER = DatabaseBackupRestore.RestoreDatabase(baseHelper, BackupPath, BackupFile, PccesDatabase, out NewDatabasename, ProgressEventHandler, ref Progress);
				if (ER.ReturnCode == 0)
				{
					ChgStrSysPccesSlave.CreateDataTable(PccesConnectionString);
					ChgStrSysPccesSlave.CreateStoredProcedure(PccesConnectionString);
					ChgStrFor42Version.AlterDataTable(PccesConnectionString);
					SysPccesSlave oPccesSlave = new SysPccesSlave();
					oPccesSlave.SetPccesSlaveMirror(PccesDatabase, NewDatabasename);
					oPccesSlave.AddSysPccesSlave(NewDatabasename, "參考基本資料庫");
					SysUser oSysUser = new SysUser();
					oSysUser.SetSysUserDatabaseName(F_UserID, NewDatabasename);
					oUserDefined.SetPccesMasterMirror(NewDatabasename);
					PccesMasterMirror = NewDatabasename;
				}
			}
			if (File.Exists(BackupFile))
			{
				File.Delete(BackupFile);
			}
		}
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("資料庫檢查");
		ModifyDB modifyDB = new ModifyDB("", aArr);
		string PccesMaster = oUserDefined.GetPccesMaster();
		if (PccesMaster == string.Empty)
		{
			MessageBox.Show("此 Pcces 為舊版資料庫，系統會自動建立新版參考基本資料庫。", "建立 [新版參考基本資料庫]", MessageBoxButtons.OK);
			PccesMaster = CreatePccesMasterDatabase(PccesConnectionString);
		}
		modifyDB.ChkDataBase(PccesMaster, ProgressEventHandler, ref Progress);
		string DefaultPccesDatabase = ((!(PccesMasterMirror != "")) ? PccesDatabase : PccesMasterMirror);
		if (ER.ReturnCode == 0)
		{
			ChgStrSysPccesSlave.CreateDataTable(PccesConnectionString);
			SysUser oSysUser = new SysUser();
			SysPccesSlave oPccesSlave = new SysPccesSlave();
			string DatabaseName = oSysUser.GetSysUserDatabaseName(F_UserID);
			if (!oPccesSlave.ExistsSysPccesSlave(DatabaseName))
			{
				oSysUser.SetSysUserDatabaseName(F_UserID, DefaultPccesDatabase);
			}
			ConnectionStringUtility connectionUtility = null;
			if (PccesMasterMirror.ToUpper() == PccesDatabase.ToUpper())
			{
				connectionUtility = new ConnectionStringUtility(PccesConnectionString);
				ER = modifyDB.ChkDataBase(PccesDatabase, ProgressEventHandler, ref Progress);
			}
			if (ER.ReturnCode == 0)
			{
				connectionUtility = new ConnectionStringUtility(modifyDB.SQLConnectionString);
				ER = modifyDB.ChkDataBase(DatabaseName, ProgressEventHandler, ref Progress);
			}
			if (ER.ReturnCode != 0 && connectionUtility != null)
			{
				string err = "資料庫 [" + connectionUtility.Database + "] 無法執行正確的版本檢查\n\n " + ER.Message + "\n\n先設定資料庫為 " + DefaultPccesDatabase + "\\n\n\n是否要繼續執行？";
				if (MessageBox.Show(err, "資料庫版本太新", MessageBoxButtons.YesNo) == DialogResult.No)
				{
					ER.Message = err;
					oSysUser.SetSysUserDatabaseName(F_UserID, DefaultPccesDatabase);
				}
				else
				{
					ER.ReturnCode = 0;
					ER.Message = "";
				}
			}
		}
		else
		{
			ER.Message = "資料庫無法執行正確的版本檢查\n\n " + ER.Message + "\n\n先設定資料庫為 " + PccesDatabase;
			ER.ReturnCode = 2;
		}
		return ER;
	}

	private string CreatePccesMasterDatabase(string PccesConnectionString)
	{
		string PccesMasterDabaseName = "Pcces43";
		string PccesBackupFilePath = AppDomain.CurrentDomain.BaseDirectory + "DBTemp\\PccesTemplate.bak";
		PccesBaseHelper baseHelper = new PccesBaseHelper(PccesConnectionString);
		string DatabaseFolder = baseHelper.GetDatabasePath() + "\\";
		if (baseHelper.ExistsDatabase(PccesMasterDabaseName))
		{
			for (int i = 0; i < 100; i++)
			{
				PccesMasterDabaseName = PccesMasterDabaseName + "_" + i;
				if (!baseHelper.ExistsDatabase(PccesMasterDabaseName))
				{
					break;
				}
			}
		}
		ExecResult ERResotre = baseHelper.Restore(DatabaseFolder, PccesBackupFilePath, PccesMasterDabaseName);
		if (ERResotre.ReturnCode != 0)
		{
			MessageBox.Show(ERResotre.Message, "建立 4.3 版 PCCES 資料庫失敗！");
			return string.Empty;
		}
		SysPccesSlave sysPccesSlave = new SysPccesSlave();
		sysPccesSlave.AddSysPccesSlave(PccesMasterDabaseName, "PCCES4.3 參考基本資料庫");
		UserDefined userDefined = new UserDefined();
		userDefined.SetPccesMaster(PccesMasterDabaseName);
		return PccesMasterDabaseName;
	}

	private void CheckVersion()
	{
		string PccesAssemblyVersion = PccesVersion.PccesAssemblyVersion;
		string INIVerion = CommonMethods.GetIniValue("Version", "Build");
		if (INIVerion != PccesAssemblyVersion)
		{
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = "PccAdmin";
			DBCLS.ExecuteCommand("Delete from UserSettings");
			ExecResult ER = UpdateFunctionList();
			if (ER.ReturnCode == 0)
			{
				CommonMethods.WriteIniValue("Version", "Build", PccesAssemblyVersion);
			}
			else
			{
				MessageBox.Show("更新功能清單失敗！" + ER.Message);
			}
			sendUpdateInfo();
			if (FunctionListChanged)
			{
				MessageBox.Show(this, "因為新增功能，以致使用者權限設定項目有增加，\n可能會有部份功能您無法執行。\n\n請洽PCCES系統管理員或請您自行重新設定權限。\n\n【單機】使用時無需理會此一訊息。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private ExecResult UpdateFunctionList()
	{
		string FunctionListXMLPath = AppDomain.CurrentDomain.BaseDirectory + "FuncList.XML";
		DataSet dsFunctionList = new DataSet();
		dsFunctionList.ReadXml(FunctionListXMLPath);
		UserDefined userDefined = new UserDefined();
		int DBFuncListVersion = userDefined.GetFuncListVersion();
		int XMLFuncListVersion = ArchConvert.Obj2Int(dsFunctionList.Tables["FuncList"].Rows[0]["Version"]);
		ExecResult ER = new ExecResult();
		if (XMLFuncListVersion > DBFuncListVersion)
		{
			FuncList funcList = new FuncList();
			ER = funcList.ImportFuncList(dsFunctionList);
			if (ER.ReturnCode == 0)
			{
				FunctionListChanged = true;
				ER = userDefined.SetFuncListVersion(XMLFuncListVersion);
			}
			WinUserFuncs winUserFuncs = new WinUserFuncs();
			if (ER.ReturnCode == 0)
			{
				ER = winUserFuncs.GrantUserAllPrivilege("PCCES");
			}
		}
		return ER;
	}

	public void UpdateMenu()
	{
		functionButtons1.OPEN_MODE_CHECK();
		if (F_HomePanel == "2")
		{
			FM_PNL2._UserID = F_UserID;
			FM_PNL2.UpdateMenu();
		}
	}

	public void TerminatePCCES()
	{
		onlineList1.Disconnect();
		FORM_STATUS = "CLOSE";
		Close();
	}

	public bool CheckIsRegistered()
	{
		bool RetV = true;
		string RegID = CommonMethods.GetIniValue("Register", "RegID");
		if (RegID.Trim() == "")
		{
			RetV = false;
		}
		return RetV;
	}

	private bool GetDisabled()
	{
		bool RetV = false;
		try
		{
			Update serviceRequest = new Update();
			string webServiceRoute = CommonMethods.GetIniValue("DownloadInfo", "webServiceRoute");
			if (webServiceRoute == "")
			{
				webServiceRoute = "http://bisc.archnowledge.com/pccesupdateservices/Update.asmx";
			}
			serviceRequest.Url = webServiceRoute;
			if (CommonMethods.GetIniValue("ProxyInfo", "usingProxy").Trim().ToLower() == "true")
			{
				serviceRequest.Proxy = GetProxy();
			}
			string RegID = CommonMethods.GetIniValue("Register", "RegID");
			RetV = !serviceRequest.IsApproved(RegID);
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "FormPccesMain.cs" + ex.Message);
			Console.Write(ex.Message);
		}
		return RetV;
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		GC.Collect();
	}

	private void HideAllChild()
	{
		Form[] ownedForms = base.ParentForm.OwnedForms;
		foreach (Form frm in ownedForms)
		{
			frm.Close();
			frm.Dispose();
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.frmPccesMain));
		this.ultraTabbedMdiManager1 = new Infragistics.Win.UltraWinTabbedMdi.UltraTabbedMdiManager(this.components);
		this.LeftPanel = new System.Windows.Forms.Panel();
		this.onlineList1 = new Archnowledge.Pcces.PccesMain.ArchControls.OnlineList();
		this.functionButtons1 = new Archnowledge.Pcces.PccesMain.ArchControls.FunctionButtons();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		((System.ComponentModel.ISupportInitialize)this.ultraTabbedMdiManager1).BeginInit();
		this.LeftPanel.SuspendLayout();
		base.SuspendLayout();
		this.ultraTabbedMdiManager1.MdiParent = this;
		this.ultraTabbedMdiManager1.TabGroupSettings.TabStyle = Infragistics.Win.UltraWinTabs.TabStyle.Wizard;
		this.LeftPanel.Controls.Add(this.onlineList1);
		this.LeftPanel.Controls.Add(this.functionButtons1);
		this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
		this.LeftPanel.Location = new System.Drawing.Point(0, 0);
		this.LeftPanel.Name = "LeftPanel";
		this.LeftPanel.Size = new System.Drawing.Size(160, 573);
		this.LeftPanel.TabIndex = 3;
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
		this.onlineList1.TabIndex = 4;
		this.functionButtons1._ActiveFunction = "";
		this.functionButtons1._CurrOpenMode = Archnowledge.Pcces.CommonClass.FunctionOpenMode.Budget;
		this.functionButtons1._ServerName = "localhost";
		this.functionButtons1._UserID = "PccesAdmin";
		this.functionButtons1._UserName = "";
		this.functionButtons1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.functionButtons1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.functionButtons1.Location = new System.Drawing.Point(0, 0);
		this.functionButtons1.Name = "functionButtons1";
		this.functionButtons1.Size = new System.Drawing.Size(160, 573);
		this.functionButtons1.TabIndex = 3;
		this.functionButtons1.Load += new System.EventHandler(functionButtons1_Load);
		this.timer1.Interval = 300000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(792, 573);
		base.Controls.Add(this.LeftPanel);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.IsMdiContainer = true;
		base.Name = "frmPccesMain";
		this.Text = "PCCES Win 4.3 ";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmPccesMain_Load);
		base.Activated += new System.EventHandler(frmPccesMain_Activated);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmPccesMain_FormClosing);
		((System.ComponentModel.ISupportInitialize)this.ultraTabbedMdiManager1).EndInit();
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
