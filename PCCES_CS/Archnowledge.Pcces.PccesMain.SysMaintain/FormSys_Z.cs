using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.Budget;
using Archnowledge.Pcces.PccesMain.ShellLib;
using Archnowledge.Pcces.REPClass;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.SysMaintain;

public class FormSys_Z : UserControl
{
	private const string FileIni = "OptionSet.ini";

	private const string pccesINIFile = "PccesMain.ini";

	private DataTable DT_1 = new DataTable();

	private string AppLocation = "";

	private string userID;

	private string F_Dept;

	private string F_BackupFolder = "";

	private UltraStatusBar ultraStatusBar1;

	private Panel panel1;

	private UltraLabel ultraLabel2;

	private UltraButton btnSave;

	private UltraCheckEditor chkUseNewMrsB;

	private UltraCheckEditor chkBDGT_AutoSave;

	private NumericUpDown BDGT_Duration;

	private UltraCheckEditor chk_DeleteAutoSave;

	private UltraLabel ultraLabel1;

	private GroupBox groupBox4;

	private UltraButton ultraButton1;

	private UltraButton ultraButton2;

	private UltraLabel ultraLabel3;

	private UltraTextEditor txtReportPack;

	private OpenFileDialog openFileDialog1;

	private UltraButton BtnRecover;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel5;

	private UltraButton ultraButton3;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel7;

	private UltraCheckEditor chk_forOldReCal;

	private UltraLabel ultraLabel8;

	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl tabGeneralSetting;

	private UltraTabPageControl tbBudgetAndBidSetting;

	private UltraTabPageControl tabAnalysisSetting;

	private UltraTabPageControl Tab_D;

	private UltraLabel ultraLabel9;

	private GroupBox groupBox7;

	private GroupBox groupBox8;

	private UltraLabel ultraLabel10;

	private GroupBox groupBox9;

	private UltraLabel ultraLabel11;

	private GroupBox groupBox10;

	private UltraLabel ultraLabel12;

	private GroupBox groupBox1;

	private UltraLabel ultraLabel13;

	private GroupBox groupBox2;

	private UltraLabel ultraLabel14;

	private C1FlexGrid gridSettingsList;

	private UltraTabPageControl Tab_E;

	private GroupBox groupBox3;

	private UltraLabel ultraLabel15;

	private UltraLabel ultraLabel16;

	private UltraFontNameEditor cboExlFont;

	private UltraLabel ultraLabel17;

	private UltraLabel ultraLabel18;

	private UltraLabel ultraLabel19;

	private UltraLabel ultraLabel21;

	private UltraCheckEditor chk_forDeleteNoUsedItem;

	private UltraLabel ultraLabel22;

	private UltraCheckEditor chk_Ana_UseNewOpen;

	private GroupBox groupBox6;

	private UltraLabel ultraLabel23;

	private UltraCheckEditor chkMrs_LoadMethod;

	private UltraLabel ultraLabel24;

	private UltraCheckEditor chkMrs_AutoChangeRate;

	private UltraLabel ultraLabel25;

	private UltraTabPageControl tabMrsBaseSetting;

	private UltraTabPageControl tabDatabaseSetting;

	private GroupBox groupBox11;

	private UltraLabel ultraLabel27;

	private UltraLabel ultraLabel26;

	private UltraLabel lblPhysicalMem;

	private UltraLabel ultraLabel29;

	private UltraLabel lblSQLMem;

	private UltraLabel ultraLabel28;

	private GroupBox groupBox12;

	private UltraButton BtnMemApply;

	private NumericUpDown numSQLMem;

	private FolderBrowserDialog folderBrowserDialog1;

	private UltraButton BtnChangePath;

	private GroupBox groupBox13;

	private UltraOptionSet rd_forOldReCal;

	private UltraButton ultraButton4;

	private UltraTextEditor txtMainInstituite;

	private UltraCheckEditor chkMrsBItem;

	private UltraCheckEditor chk_Number;

	private UltraCheckEditor chk_IsEight;

	private UltraCheckEditor chk_IsTooltip;

	private UltraCheckEditor chk_AutoNum;

	private GroupBox gp_Restore;

	private UltraCheckEditor chk_Restore;

	private UltraLabel ultraLabel30;

	private UltraCheckEditor chkAnalyis;

	private UltraCheckEditor chkIsDetail;

	private Container components = null;

	private UltraTabPageControl tabProxySetting;

	private GroupBox groupBox14;

	private UltraLabel lbProxySetting;

	private GroupBox gbAuthority;

	private UltraCheckEditor chkNeedAutority;

	private TextBox tbPassword;

	private TextBox tbAccount;

	private Label label8;

	private Label label10;

	private GroupBox gbProxySetting;

	private UltraCheckEditor cbUseProxy;

	private TextBox tbPort;

	private TextBox tbAddress;

	private Label label3;

	private Label label9;

	private UltraButton btnTestConnection;

	private UltraCheckEditor chkUseCostStructure;

	private GroupBox groupBox5;

	private UltraLabel lbCostStructure;

	private UltraLabel ultraLabel20;

	private UltraLabel ultraLabel32;

	private UltraLabel ultraLabel31;

	private GroupBox gpGreenItem;

	private UltraLabel lbGreenItem;

	private Label lbGreenMethod;

	private Label lbGreenEnv;

	private TextBox tbGreenEnergy;

	private TextBox tbGreenMaterial;

	private Label lbGreenEnergy;

	private Label lbGreenMaterial;

	private TextBox tbGreenMethod;

	private TextBox tbGreenEnv;

	private UltraLabel lbGreenItemDescription;

	private UltraLabel lbGreenDesciptionTitle;

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

	public FormSys_Z()
	{
		InitializeComponent();
	}

	private void Load_CommonData()
	{
		string F_IsAddOn = CommonMethods.IniReadValue(AppLocation + "PccesMain.ini", "AddOn", "OperationType");
		string sAllowIsEight = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "CommonData", "AllowIsEight");
		string sAllowIsTooltip = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "CommonData", "AllowIsTooltip");
		string sAllowRestore = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "CommonData", "sAllowRestore");
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("自動編碼--主辦單位載入");
		MainUnitCom MN_UNT_CM = new MainUnitCom(aArr);
		DT_1 = MN_UNT_CM.ListItem("");
		string sDEPT_ID = (F_Dept = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "CommonData", "DEPT_ID"));
		DataView dv = new DataView(DT_1);
		dv.RowFilter = "MainCode='" + sDEPT_ID + "'";
		if (dv.Count > 0)
		{
			txtMainInstituite.Text = dv[0]["MainCode"].ToString().Trim() + ":" + dv[0]["MainName"].ToString().Trim();
		}
		if (sAllowIsEight.ToUpper() == "TRUE")
		{
			chk_IsEight.Checked = true;
		}
		if (sAllowIsTooltip.ToUpper() == "TRUE")
		{
			chk_IsTooltip.Checked = true;
		}
		if (F_IsAddOn.ToUpper() == "BID")
		{
			if (sAllowRestore.ToUpper() == "TRUE")
			{
				chk_Restore.Checked = true;
			}
			chk_Restore.Visible = true;
			gp_Restore.Visible = true;
		}
		else
		{
			chk_Restore.Visible = false;
			gp_Restore.Visible = false;
		}
		chkUseCostStructure.Checked = PubTools.GetAppSet_Bool("UseCostStructure");
		LoadGreenItemSetting();
	}

	private void LoadGreenItemSetting()
	{
		string greenEnv = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "CommonData", "GreenEnv");
		string greenMethod = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "CommonData", "GreenMethod");
		string greenMaterial = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "CommonData", "GreenMaterial");
		string greenEnergy = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "CommonData", "GreenEnergy");
		tbGreenEnv.Text = ((greenEnv == string.Empty) ? "綠色環境" : greenEnv);
		tbGreenMethod.Text = ((greenMethod == string.Empty) ? "綠色工法" : greenMethod);
		tbGreenMaterial.Text = ((greenMaterial == string.Empty) ? "綠色材料" : greenMaterial);
		tbGreenEnergy.Text = ((greenEnergy == string.Empty) ? "綠色能源" : greenEnergy);
	}

	private void Load_BreakDown()
	{
		string sAllowRepeatItem = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BreakDownData", "AllowRepeatItem");
		string sAllowSortItem = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BreakDownData", "AllowSort");
		string IschkAnalyis = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BreakDownData", "NoMessage");
		string sDetailMaster = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BreakDownData", "DetailMaster");
		if (sAllowRepeatItem.ToUpper() == "TRUE")
		{
			chkUseNewMrsB.Checked = true;
		}
		else
		{
			chkUseNewMrsB.Checked = false;
		}
		if (sAllowSortItem.ToUpper() == "TRUE")
		{
			chkMrsBItem.Checked = true;
		}
		else
		{
			chkMrsBItem.Checked = false;
		}
		if (IschkAnalyis.ToUpper() == "TRUE")
		{
			chkAnalyis.Checked = true;
		}
		else
		{
			chkAnalyis.Checked = false;
		}
		if (sDetailMaster.ToUpper() == "TRUE")
		{
			chkIsDetail.Checked = true;
		}
		else
		{
			chkIsDetail.Checked = false;
		}
	}

	private void Load_BDGT()
	{
		string sIsAutoSave = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BDGT", "IsAutoSave");
		if (sIsAutoSave.ToUpper() == "TRUE")
		{
			chkBDGT_AutoSave.Checked = true;
		}
		else
		{
			chkBDGT_AutoSave.Checked = false;
		}
		string sAutoSaveDuration = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BDGT", "AutoSaveDuration");
		BDGT_Duration.Value = PubTools.Str2Decimal(sAutoSaveDuration);
		string sIsEidtNumber = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BDGT", "IsEidtNumber");
		if (sIsEidtNumber.ToUpper() == "TRUE")
		{
			chk_Number.Checked = true;
		}
		else
		{
			chk_Number.Checked = false;
		}
		string sIsOldReCal = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BDGT", "IsOldReCal");
		switch (sIsOldReCal.ToUpper())
		{
		case "FALSE":
			rd_forOldReCal.CheckedIndex = 0;
			break;
		case "TRUE":
			rd_forOldReCal.CheckedIndex = 1;
			break;
		case "THIRD":
			rd_forOldReCal.CheckedIndex = 2;
			break;
		}
		string sBDGT_BackupPath = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BDGT", "BackupPath");
		if (sBDGT_BackupPath == "")
		{
			F_BackupFolder = AppLocation + "Backup\\";
			ultraLabel21.Text = "(備份的存放路徑是 " + F_BackupFolder + ")";
		}
		else
		{
			F_BackupFolder = sBDGT_BackupPath;
			ultraLabel21.Text = "(備份的存放路徑是 " + F_BackupFolder + ")";
		}
		string sIsAutoNumber = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BDGT", "IsAutoNumber");
		if (sIsAutoNumber.ToUpper() == "TRUE")
		{
			chk_AutoNum.Checked = true;
		}
		else
		{
			chk_AutoNum.Checked = false;
		}
	}

	private void Load_Digital()
	{
		string sFontName = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "Digital", "XlsFont");
		if (sFontName == "")
		{
			cboExlFont.Text = "細明體";
		}
		else
		{
			cboExlFont.Text = sFontName;
		}
	}

	private void Load_Memory()
	{
		uint physicalMemory = CommonMethods.Get_Physical_Memory();
		lblPhysicalMem.Text = physicalMemory + " MB";
		numSQLMem.Maximum = ((physicalMemory > 2048) ? 2048u : physicalMemory);
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = userID;
		DBCLS.ExecuteCommand("EXEC sp_configure 'show advanced options', 1" + '\r' + "RECONFIGURE WITH OVERRIDE");
		DataTable DT_Mem = DBCLS.GetUserDefine("EXEC sp_configure 'max server memory (MB)'" + '\r');
		DBCLS.ExecuteCommand("EXEC sp_configure 'show advanced options', 0" + '\r' + "RECONFIGURE WITH OVERRIDE");
		DBCLS = null;
		try
		{
			numSQLMem.Value = PubTools.Str2Decimal(DT_Mem.Rows[0]["run_value"]);
		}
		catch
		{
			numSQLMem.Value = ((physicalMemory > 2048) ? 2048u : physicalMemory);
		}
	}

	private void SaveCommonData()
	{
		bool IsEight = chk_IsEight.Checked;
		bool IsTooltip = chk_IsTooltip.Checked;
		bool IsRestore = chk_Restore.Checked;
		string sDEPT_ID = (base.ParentForm as frmSysMaintain)._MainCode_G;
		if (sDEPT_ID == "" && F_Dept != "")
		{
			sDEPT_ID = F_Dept;
		}
		CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "CommonData", "DEPT_ID", sDEPT_ID);
		if (IsEight)
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "CommonData", "AllowIsEight", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "CommonData", "AllowIsEight", "FALSE");
		}
		if (IsTooltip)
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "CommonData", "AllowIsTooltip", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "CommonData", "AllowIsTooltip", "FALSE");
		}
		if (IsRestore)
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "CommonData", "sAllowRestore", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "CommonData", "sAllowRestore", "FALSE");
		}
		SaveUseCoststructureSetting();
		SaveGreenItemSetting();
	}

	private void SaveUseCoststructureSetting()
	{
		Configuration appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
		AppSettingsSection appSettings = (AppSettingsSection)appConfig.GetSection("appSettings");
		if (appSettings.Settings["UseCostStructure"] == null)
		{
			appSettings.Settings.Add("UseCostStructure", "true");
		}
		appSettings.Settings["UseCostStructure"].Value = chkUseCostStructure.Checked.ToString();
		appConfig.Save();
		ConfigurationManager.RefreshSection("appSettings");
	}

	private void SaveGreenItemSetting()
	{
		CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "CommonData", "GreenEnv", tbGreenEnv.Text.Trim());
		CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "CommonData", "GreenMethod", tbGreenMethod.Text.Trim());
		CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "CommonData", "GreenMaterial", tbGreenMaterial.Text.Trim());
		CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "CommonData", "GreenEnergy", tbGreenEnergy.Text.Trim());
	}

	private void Save_BreakDown()
	{
		bool IsRepeat = chkUseNewMrsB.Checked;
		bool IschkMrsBItem = chkMrsBItem.Checked;
		bool IschkAnalyis = chkAnalyis.Checked;
		bool IsDetailMaster = chkIsDetail.Checked;
		if (IsRepeat)
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "BreakDownData", "AllowRepeatItem", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "BreakDownData", "AllowRepeatItem", "FALSE");
		}
		if (chk_Ana_UseNewOpen.Checked)
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "BreakDownData", "UseNewOpen", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "BreakDownData", "UseNewOpen", "FALSE");
		}
		if (IschkMrsBItem)
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "BreakDownData", "AllowSort", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "BreakDownData", "AllowSort", "FALSE");
		}
		if (IschkAnalyis)
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "BreakDownData", "NoMessage", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "BreakDownData", "NoMessage", "FALSE");
		}
		if (IsDetailMaster)
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "BreakDownData", "DetailMaster", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "BreakDownData", "DetailMaster", "FALSE");
		}
	}

	private void Save_BDGT()
	{
		if (chkBDGT_AutoSave.Checked)
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "BDGT", "IsAutoSave", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "BDGT", "IsAutoSave", "FALSE");
		}
		CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "BDGT", "AutoSaveDuration", BDGT_Duration.Value.ToString());
		if (chk_DeleteAutoSave.Checked)
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "BDGT", "IsDeleteAutoSave", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "BDGT", "IsDeleteAutoSave", "FALSE");
		}
		if (chk_Number.Checked)
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "BDGT", "IsEidtNumber", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "BDGT", "IsEidtNumber", "FALSE");
		}
		CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "BDGT", "BackupPath", F_BackupFolder);
		if (chk_AutoNum.Checked)
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "BDGT", "IsAutoNumber", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "BDGT", "IsAutoNumber", "FALSE");
		}
	}

	private void Save_Digital()
	{
		string sFontName = cboExlFont.Text.Trim();
		CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "Digital", "XlsFont", sFontName);
	}

	private void FormSys_Z_Load(object sender, EventArgs e)
	{
		AppLocation = AppDomain.CurrentDomain.BaseDirectory;
		LoadSettings();
		gridSettingsList.Rows.Count = 6;
		gridSettingsList[0, 0] = "一般";
		gridSettingsList[1, 0] = "預算書編製";
		gridSettingsList[2, 0] = "單價分析";
		gridSettingsList[3, 0] = "計價";
		gridSettingsList[4, 0] = "代理伺服器";
		DBClass DBCLS = new DBClass();
		string serverName = DBCLS.GetDBConnectionServer().ToLower();
		if (serverName.StartsWith(Environment.MachineName.ToLower()))
		{
			gridSettingsList[5, 0] = "資料庫記憶體配置";
		}
		else
		{
			switch (serverName)
			{
			default:
				if (!serverName.StartsWith(".\\") && !(serverName == "127.0.0.1"))
				{
					break;
				}
				goto case "localhost";
			case "localhost":
			case "(local)":
			case ".":
				gridSettingsList[5, 0] = "資料庫記憶體配置";
				break;
			}
		}
		DBCLS = null;
		gridSettingsList.Row = 0;
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		SaveSettings();
		MessageBox.Show(this, "儲存完畢！", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void SaveSettings()
	{
		SaveCommonData();
		Save_BreakDown();
		Save_BDGT();
		Save_Digital();
		SaveProxySetting();
	}

	private void SaveProxySetting()
	{
		string port = tbPort.Text.Trim().ToLower();
		string account = tbAccount.Text.Trim();
		string password = tbPassword.Text.Trim();
		string address = tbAddress.Text.Trim().ToLower();
		if (cbUseProxy.Checked && chkNeedAutority.Checked)
		{
			WriteProxySetting(useProxy: true, address, port, account, password);
		}
		else if (cbUseProxy.Checked)
		{
			WriteProxySetting(useProxy: true, address, port, string.Empty, string.Empty);
		}
		else
		{
			WriteProxySetting(useProxy: false, string.Empty, string.Empty, string.Empty, string.Empty);
		}
	}

	private void WriteProxySetting(bool useProxy, string address, string port, string account, string password)
	{
		CommonMethods.IniWriteValue(AppLocation + "PccesMain.ini", "ProxyInfo", "usingProxy", useProxy ? "true" : "false");
		CommonMethods.IniWriteValue(AppLocation + "PccesMain.ini", "ProxyInfo", "address", address);
		CommonMethods.IniWriteValue(AppLocation + "PccesMain.ini", "ProxyInfo", "port", port);
		CommonMethods.IniWriteValue(AppLocation + "PccesMain.ini", "ProxyInfo", "account", account);
		CommonMethods.IniWriteValue(AppLocation + "PccesMain.ini", "ProxyInfo", "password", password);
	}

	private void LoadSettings()
	{
		Load_CommonData();
		Load_BreakDown();
		Load_BDGT();
		Load_Digital();
		LoadProxySetting();
	}

	private void LoadProxySetting()
	{
		string useProxy = CommonMethods.IniReadValue(AppLocation + "PccesMain.ini", "ProxyInfo", "usingProxy");
		string port = CommonMethods.IniReadValue(AppLocation + "PccesMain.ini", "ProxyInfo", "port");
		string account = CommonMethods.IniReadValue(AppLocation + "PccesMain.ini", "ProxyInfo", "account");
		string password = CommonMethods.IniReadValue(AppLocation + "PccesMain.ini", "ProxyInfo", "password");
		string address = CommonMethods.IniReadValue(AppLocation + "PccesMain.ini", "ProxyInfo", "address");
		if (useProxy.Trim().ToUpper() == "TRUE")
		{
			cbUseProxy.Checked = true;
			tbAddress.Text = address;
			tbPort.Text = port;
			if (account.Trim() != string.Empty)
			{
				chkNeedAutority.Checked = true;
				tbAccount.Text = account;
				tbPassword.Text = password;
			}
		}
	}

	private void BDGT_Duration_ValueChanged(object sender, EventArgs e)
	{
		if (BDGT_Duration.Value == 0m)
		{
			chkBDGT_AutoSave.Checked = false;
		}
		else
		{
			chkBDGT_AutoSave.Checked = true;
		}
	}

	private void chkBDGT_AutoSave_CheckedChanged(object sender, EventArgs e)
	{
		if (chkBDGT_AutoSave.Checked)
		{
			BDGT_Duration.Value = 10m;
		}
		else
		{
			BDGT_Duration.Value = 0m;
		}
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		string sFilter = "報表封裝檔 (*.arch)|*.arch";
		openFileDialog1.Filter = sFilter;
		openFileDialog1.RestoreDirectory = true;
		if (openFileDialog1.ShowDialog() == DialogResult.OK)
		{
			txtReportPack.Text = openFileDialog1.FileName;
		}
	}

	private void ultraButton2_Click(object sender, EventArgs e)
	{
		if (txtReportPack.Text.Trim() == "")
		{
			MessageBox.Show(this, "請先挑選要轉入的報表封裝檔。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		string strPath = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "Report\\";
		ArrayList tmp_ALaa = new ArrayList();
		tmp_ALaa.Add(userID);
		tmp_ALaa.Add("新增報表格式");
		RepListClass RepCom = new RepListClass(tmp_ALaa);
		ExecResult ER = RepCom.AddReport(txtReportPack.Text.Trim(), strPath);
		if (ER.ReturnCode == 0)
		{
			MessageBox.Show(this, "轉入成功。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else
		{
			MessageBox.Show(this, "轉入失敗，\n請先確認挑選的封裝檔檔案無誤。\n" + ER.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void BtnRecover_Click(object sender, EventArgs e)
	{
		CommonMethods.WriteIniValue("BreakDown", "LocationX", "0");
		CommonMethods.WriteIniValue("BreakDown", "LocationY", "0");
		CommonMethods.WriteIniValue("BreakDown", "Width", "0");
		CommonMethods.WriteIniValue("BreakDown", "Height", "0");
		CommonMethods.WriteIniValue("HomePanel", "LocationX", "0");
		CommonMethods.WriteIniValue("HomePanel", "LocationY", "0");
		CommonMethods.WriteIniValue("HomePanel", "Width", "0");
		CommonMethods.WriteIniValue("HomePanel", "Height", "0");
		CommonMethods.WriteIniValue("MrsBase", "PK_LocationX", "0");
		CommonMethods.WriteIniValue("MrsBase", "PK_LocationY", "0");
		CommonMethods.WriteIniValue("MrsBase", "PK_Width", "0");
		CommonMethods.WriteIniValue("MrsBase", "PK_Height", "0");
		MessageBox.Show(this, "完成回復！", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void ultraButton3_Click(object sender, EventArgs e)
	{
		CommonMethods.WriteIniValue("Register", "RegID", "");
		CommonMethods.WriteIniValue("Register", "UserName", "");
		CommonMethods.WriteIniValue("Register", "EMAIL", "");
		CommonMethods.WriteIniValue("Register", "CompanyName", "");
		CommonMethods.WriteIniValue("Register", "Dept", "");
		CommonMethods.WriteIniValue("Register", "TEL", "");
		MessageBox.Show(this, "完成清空註冊資訊！", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void GridList_AfterRowColChange(object sender, RangeEventArgs e)
	{
		Tab_Ctrl.SelectedTab = Tab_Ctrl.Tabs[gridSettingsList.Row];
	}

	private void ultraStatusBar1_PanelClick(object sender, PanelClickEventArgs e)
	{
		if (e.Panel.Index == 1)
		{
			ShellExecute SHExe = new ShellExecute();
			SHExe.OwnerHandle = base.Handle;
			SHExe.Path = "http://pcces.archnowledge.com/pccesfaq/";
			SHExe.Execute();
		}
	}

	private void BtnMemApply_Click(object sender, EventArgs e)
	{
		try
		{
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = userID;
			DBCLS.ExecuteCommand("EXEC sp_configure 'show advanced options', 1" + '\r' + "RECONFIGURE WITH OVERRIDE");
			DBCLS.ExecuteCommand("EXEC sp_configure 'max server memory (MB)'," + numSQLMem.Value.ToString() + '\r');
			DBCLS.ExecuteCommand("EXEC sp_configure 'show advanced options', 0" + '\r' + "RECONFIGURE WITH OVERRIDE");
			DBCLS = null;
			MessageBox.Show(this, "套用成功\n\n已經配置成: " + numSQLMem.Value + " MB", "成功", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "SysMaintain.FormSys_Z.cs" + ex.Message);
			MessageBox.Show(this, "套用失敗\n" + ex.Message, "失敗", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void Tab_Ctrl_ActiveTabChanged(object sender, ActiveTabChangedEventArgs e)
	{
		if (e.Tab.Key == "Tab_G")
		{
			Load_Memory();
		}
	}

	private void BtnChangePath_Click(object sender, EventArgs e)
	{
		if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
		{
			F_BackupFolder = folderBrowserDialog1.SelectedPath;
			ultraLabel21.Text = "(備份的存放路徑是 " + F_BackupFolder + ")";
		}
	}

	private void ultraButton4_Click(object sender, EventArgs e)
	{
		if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
		{
			string LastChar = folderBrowserDialog1.SelectedPath.Substring(folderBrowserDialog1.SelectedPath.Length - 1);
			if (LastChar != "\\")
			{
				F_BackupFolder = folderBrowserDialog1.SelectedPath + "\\";
			}
			else
			{
				F_BackupFolder = folderBrowserDialog1.SelectedPath;
			}
			ultraLabel21.Text = "(備份的存放路徑是 " + F_BackupFolder + ")";
		}
	}

	private void BDGT_Duration_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Alt && e.KeyCode == Keys.C)
		{
			ultraButton4_Click(sender, null);
		}
	}

	private void ultraButton4_Click_1(object sender, EventArgs e)
	{
		FormBudgetDept_Pick FM_BDGT_DEPT_PK = new FormBudgetDept_Pick();
		FM_BDGT_DEPT_PK._UserID = userID;
		FM_BDGT_DEPT_PK._OwnerName = "FormSys_Z";
		if (FM_BDGT_DEPT_PK.ShowDialog(this) == DialogResult.OK)
		{
			txtMainInstituite.Text = (base.ParentForm as frmSysMaintain)._MainCode_G + ":" + (base.ParentForm as frmSysMaintain)._MainName_G;
		}
		FM_BDGT_DEPT_PK.Close();
		FM_BDGT_DEPT_PK.Dispose();
		FM_BDGT_DEPT_PK = null;
	}

	private void btnTestConnection_Click(object sender, EventArgs e)
	{
		Cursor = Cursors.AppStarting;
		WebRequest testRequest = WebRequest.Create("http://www.google.com.tw");
		WebProxy proxy = new WebProxy();
		string port = tbPort.Text.Trim().ToLower();
		string account = tbAccount.Text.Trim();
		string password = tbPassword.Text.Trim();
		string address = tbAddress.Text.Trim().ToLower();
		WebResponse response = null;
		try
		{
			if (cbUseProxy.Checked)
			{
				proxy.Address = new Uri(address + ":" + port);
				testRequest.Proxy = proxy;
				if (chkNeedAutority.Checked)
				{
					proxy.Credentials = new NetworkCredential(account, password);
				}
			}
			testRequest.Timeout = 5000;
			response = testRequest.GetResponse();
			MessageBox.Show("網路連線測試成功！", "注意", MessageBoxButtons.OK, MessageBoxIcon.None);
		}
		catch (Exception ex)
		{
			Cursor = Cursors.Default;
			MessageBox.Show("網路連線測試失敗！" + ex.Message, "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		finally
		{
			response?.Close();
			Cursor = Cursors.Default;
		}
	}

	private void cbUseProxy_CheckedChanged(object sender, EventArgs e)
	{
		if (cbUseProxy.Checked)
		{
			tbAddress.Enabled = true;
			tbPort.Enabled = true;
		}
		else
		{
			chkNeedAutority.Checked = false;
			tbAddress.Enabled = false;
			tbPort.Enabled = false;
		}
	}

	private void chkNeedAutority_CheckedChanged(object sender, EventArgs e)
	{
		if (chkNeedAutority.Checked)
		{
			cbUseProxy.Checked = true;
			tbAccount.Enabled = true;
			tbPassword.Enabled = true;
		}
		else
		{
			tbAccount.Enabled = false;
			tbPassword.Enabled = false;
		}
	}

	private void tbAddress_Leave(object sender, EventArgs e)
	{
		string address = tbAddress.Text.Trim().ToLower();
		if (address.Length > 7 && address.Substring(0, 7) != "http://")
		{
			address = "http://" + address;
			tbAddress.Text = address;
		}
	}

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
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
		Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
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
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab7 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab8 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.SysMaintain.FormSys_Z));
		this.tabGeneralSetting = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.lbGreenItemDescription = new Infragistics.Win.Misc.UltraLabel();
		this.lbGreenDesciptionTitle = new Infragistics.Win.Misc.UltraLabel();
		this.tbGreenEnergy = new System.Windows.Forms.TextBox();
		this.tbGreenMaterial = new System.Windows.Forms.TextBox();
		this.lbGreenEnergy = new System.Windows.Forms.Label();
		this.lbGreenMaterial = new System.Windows.Forms.Label();
		this.tbGreenMethod = new System.Windows.Forms.TextBox();
		this.tbGreenEnv = new System.Windows.Forms.TextBox();
		this.lbGreenMethod = new System.Windows.Forms.Label();
		this.lbGreenEnv = new System.Windows.Forms.Label();
		this.gpGreenItem = new System.Windows.Forms.GroupBox();
		this.ultraLabel32 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel31 = new Infragistics.Win.Misc.UltraLabel();
		this.chkUseCostStructure = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.lbCostStructure = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel30 = new Infragistics.Win.Misc.UltraLabel();
		this.chk_Restore = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk_IsTooltip = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk_IsEight = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.txtMainInstituite = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraButton4 = new Infragistics.Win.Misc.UltraButton();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.groupBox7 = new System.Windows.Forms.GroupBox();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton3 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.BtnRecover = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.gp_Restore = new System.Windows.Forms.GroupBox();
		this.lbGreenItem = new Infragistics.Win.Misc.UltraLabel();
		this.tbBudgetAndBidSetting = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.chk_Number = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.groupBox13 = new System.Windows.Forms.GroupBox();
		this.rd_forOldReCal = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.BtnChangePath = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel22 = new Infragistics.Win.Misc.UltraLabel();
		this.chk_forDeleteNoUsedItem = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.ultraLabel21 = new Infragistics.Win.Misc.UltraLabel();
		this.groupBox8 = new System.Windows.Forms.GroupBox();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.chk_DeleteAutoSave = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.BDGT_Duration = new System.Windows.Forms.NumericUpDown();
		this.chkBDGT_AutoSave = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk_forOldReCal = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.chk_AutoNum = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.tabAnalysisSetting = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.chkIsDetail = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkAnalyis = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkMrsBItem = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk_Ana_UseNewOpen = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.groupBox9 = new System.Windows.Forms.GroupBox();
		this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
		this.chkUseNewMrsB = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.Tab_D = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.groupBox10 = new System.Windows.Forms.GroupBox();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.ultraLabel20 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.txtReportPack = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tabProxySetting = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.btnTestConnection = new Infragistics.Win.Misc.UltraButton();
		this.gbAuthority = new System.Windows.Forms.GroupBox();
		this.chkNeedAutority = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.tbPassword = new System.Windows.Forms.TextBox();
		this.tbAccount = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.gbProxySetting = new System.Windows.Forms.GroupBox();
		this.cbUseProxy = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.tbPort = new System.Windows.Forms.TextBox();
		this.tbAddress = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.groupBox14 = new System.Windows.Forms.GroupBox();
		this.lbProxySetting = new Infragistics.Win.Misc.UltraLabel();
		this.tabDatabaseSetting = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.BtnMemApply = new Infragistics.Win.Misc.UltraButton();
		this.groupBox12 = new System.Windows.Forms.GroupBox();
		this.numSQLMem = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel28 = new Infragistics.Win.Misc.UltraLabel();
		this.lblSQLMem = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel29 = new Infragistics.Win.Misc.UltraLabel();
		this.lblPhysicalMem = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel26 = new Infragistics.Win.Misc.UltraLabel();
		this.groupBox11 = new System.Windows.Forms.GroupBox();
		this.ultraLabel27 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_E = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraLabel17 = new Infragistics.Win.Misc.UltraLabel();
		this.cboExlFont = new Infragistics.Win.UltraWinEditors.UltraFontNameEditor();
		this.ultraLabel16 = new Infragistics.Win.Misc.UltraLabel();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.ultraLabel15 = new Infragistics.Win.Misc.UltraLabel();
		this.tabMrsBaseSetting = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraLabel25 = new Infragistics.Win.Misc.UltraLabel();
		this.chkMrs_AutoChangeRate = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.ultraLabel24 = new Infragistics.Win.Misc.UltraLabel();
		this.chkMrs_LoadMethod = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.ultraLabel23 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.panel1 = new System.Windows.Forms.Panel();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.btnSave = new Infragistics.Win.Misc.UltraButton();
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.gridSettingsList = new C1.Win.C1FlexGrid.C1FlexGrid();
		this.ultraLabel18 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel19 = new Infragistics.Win.Misc.UltraLabel();
		this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
		this.tabGeneralSetting.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtMainInstituite).BeginInit();
		this.tbBudgetAndBidSetting.SuspendLayout();
		this.groupBox13.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rd_forOldReCal).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.BDGT_Duration).BeginInit();
		this.tabAnalysisSetting.SuspendLayout();
		this.Tab_D.SuspendLayout();
		this.groupBox4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtReportPack).BeginInit();
		this.tabProxySetting.SuspendLayout();
		this.gbAuthority.SuspendLayout();
		this.gbProxySetting.SuspendLayout();
		this.tabDatabaseSetting.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.numSQLMem).BeginInit();
		this.Tab_E.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboExlFont).BeginInit();
		this.tabMrsBaseSetting.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridSettingsList).BeginInit();
		base.SuspendLayout();
		this.tabGeneralSetting.Controls.Add(this.lbGreenItemDescription);
		this.tabGeneralSetting.Controls.Add(this.lbGreenDesciptionTitle);
		this.tabGeneralSetting.Controls.Add(this.tbGreenEnergy);
		this.tabGeneralSetting.Controls.Add(this.tbGreenMaterial);
		this.tabGeneralSetting.Controls.Add(this.lbGreenEnergy);
		this.tabGeneralSetting.Controls.Add(this.lbGreenMaterial);
		this.tabGeneralSetting.Controls.Add(this.tbGreenMethod);
		this.tabGeneralSetting.Controls.Add(this.tbGreenEnv);
		this.tabGeneralSetting.Controls.Add(this.lbGreenMethod);
		this.tabGeneralSetting.Controls.Add(this.lbGreenEnv);
		this.tabGeneralSetting.Controls.Add(this.gpGreenItem);
		this.tabGeneralSetting.Controls.Add(this.ultraLabel32);
		this.tabGeneralSetting.Controls.Add(this.ultraLabel31);
		this.tabGeneralSetting.Controls.Add(this.chkUseCostStructure);
		this.tabGeneralSetting.Controls.Add(this.groupBox5);
		this.tabGeneralSetting.Controls.Add(this.lbCostStructure);
		this.tabGeneralSetting.Controls.Add(this.ultraLabel30);
		this.tabGeneralSetting.Controls.Add(this.chk_Restore);
		this.tabGeneralSetting.Controls.Add(this.chk_IsTooltip);
		this.tabGeneralSetting.Controls.Add(this.chk_IsEight);
		this.tabGeneralSetting.Controls.Add(this.txtMainInstituite);
		this.tabGeneralSetting.Controls.Add(this.ultraButton4);
		this.tabGeneralSetting.Controls.Add(this.groupBox2);
		this.tabGeneralSetting.Controls.Add(this.ultraLabel14);
		this.tabGeneralSetting.Controls.Add(this.groupBox1);
		this.tabGeneralSetting.Controls.Add(this.ultraLabel13);
		this.tabGeneralSetting.Controls.Add(this.groupBox7);
		this.tabGeneralSetting.Controls.Add(this.ultraLabel2);
		this.tabGeneralSetting.Controls.Add(this.ultraLabel9);
		this.tabGeneralSetting.Controls.Add(this.ultraButton3);
		this.tabGeneralSetting.Controls.Add(this.ultraLabel6);
		this.tabGeneralSetting.Controls.Add(this.ultraLabel7);
		this.tabGeneralSetting.Controls.Add(this.BtnRecover);
		this.tabGeneralSetting.Controls.Add(this.ultraLabel5);
		this.tabGeneralSetting.Controls.Add(this.ultraLabel4);
		this.tabGeneralSetting.Controls.Add(this.gp_Restore);
		this.tabGeneralSetting.Controls.Add(this.lbGreenItem);
		this.tabGeneralSetting.Location = new System.Drawing.Point(0, 0);
		this.tabGeneralSetting.Name = "tabGeneralSetting";
		this.tabGeneralSetting.Size = new System.Drawing.Size(580, 604);
		this.lbGreenItemDescription.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lbGreenItemDescription.Location = new System.Drawing.Point(71, 519);
		this.lbGreenItemDescription.Name = "lbGreenItemDescription";
		this.lbGreenItemDescription.Size = new System.Drawing.Size(493, 23);
		this.lbGreenItemDescription.TabIndex = 62;
		this.lbGreenItemDescription.Text = "此處可自行定義綠色內涵指標顯示名稱";
		appearance1.ForeColor = System.Drawing.Color.Red;
		this.lbGreenDesciptionTitle.Appearance = appearance1;
		this.lbGreenDesciptionTitle.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lbGreenDesciptionTitle.Location = new System.Drawing.Point(9, 519);
		this.lbGreenDesciptionTitle.Name = "lbGreenDesciptionTitle";
		this.lbGreenDesciptionTitle.Size = new System.Drawing.Size(56, 23);
		this.lbGreenDesciptionTitle.TabIndex = 61;
		this.lbGreenDesciptionTitle.Text = "說明：";
		this.tbGreenEnergy.Location = new System.Drawing.Point(396, 482);
		this.tbGreenEnergy.Name = "tbGreenEnergy";
		this.tbGreenEnergy.Size = new System.Drawing.Size(163, 25);
		this.tbGreenEnergy.TabIndex = 60;
		this.tbGreenMaterial.Location = new System.Drawing.Point(396, 451);
		this.tbGreenMaterial.Name = "tbGreenMaterial";
		this.tbGreenMaterial.Size = new System.Drawing.Size(163, 25);
		this.tbGreenMaterial.TabIndex = 59;
		this.lbGreenEnergy.AutoSize = true;
		this.lbGreenEnergy.Location = new System.Drawing.Point(287, 487);
		this.lbGreenEnergy.Name = "lbGreenEnergy";
		this.lbGreenEnergy.Size = new System.Drawing.Size(103, 15);
		this.lbGreenEnergy.TabIndex = 58;
		this.lbGreenEnergy.Text = "指標四名稱：";
		this.lbGreenMaterial.AutoSize = true;
		this.lbGreenMaterial.Location = new System.Drawing.Point(287, 457);
		this.lbGreenMaterial.Name = "lbGreenMaterial";
		this.lbGreenMaterial.Size = new System.Drawing.Size(103, 15);
		this.lbGreenMaterial.TabIndex = 57;
		this.lbGreenMaterial.Text = "指標三名稱：";
		this.tbGreenMethod.Location = new System.Drawing.Point(118, 482);
		this.tbGreenMethod.Name = "tbGreenMethod";
		this.tbGreenMethod.Size = new System.Drawing.Size(163, 25);
		this.tbGreenMethod.TabIndex = 56;
		this.tbGreenEnv.Location = new System.Drawing.Point(118, 451);
		this.tbGreenEnv.Name = "tbGreenEnv";
		this.tbGreenEnv.Size = new System.Drawing.Size(163, 25);
		this.tbGreenEnv.TabIndex = 55;
		this.lbGreenMethod.AutoSize = true;
		this.lbGreenMethod.Location = new System.Drawing.Point(9, 489);
		this.lbGreenMethod.Name = "lbGreenMethod";
		this.lbGreenMethod.Size = new System.Drawing.Size(103, 15);
		this.lbGreenMethod.TabIndex = 54;
		this.lbGreenMethod.Text = "指標二名稱：";
		this.lbGreenEnv.AutoSize = true;
		this.lbGreenEnv.Location = new System.Drawing.Point(9, 459);
		this.lbGreenEnv.Name = "lbGreenEnv";
		this.lbGreenEnv.Size = new System.Drawing.Size(103, 15);
		this.lbGreenEnv.TabIndex = 53;
		this.lbGreenEnv.Text = "指標一名稱：";
		this.gpGreenItem.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gpGreenItem.Location = new System.Drawing.Point(9, 438);
		this.gpGreenItem.Name = "gpGreenItem";
		this.gpGreenItem.Size = new System.Drawing.Size(555, 8);
		this.gpGreenItem.TabIndex = 51;
		this.gpGreenItem.TabStop = false;
		appearance2.ForeColor = System.Drawing.Color.Red;
		this.ultraLabel32.Appearance = appearance2;
		this.ultraLabel32.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel32.Location = new System.Drawing.Point(136, 372);
		this.ultraLabel32.Name = "ultraLabel32";
		this.ultraLabel32.Size = new System.Drawing.Size(56, 23);
		this.ultraLabel32.TabIndex = 50;
		this.ultraLabel32.Text = "說明：";
		this.ultraLabel31.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ultraLabel31.Location = new System.Drawing.Point(200, 372);
		this.ultraLabel31.Name = "ultraLabel31";
		this.ultraLabel31.Size = new System.Drawing.Size(360, 46);
		this.ultraLabel31.TabIndex = 49;
		this.ultraLabel31.Text = "現有成本架構功能係供測試用，未來整合各機關需求後將更完備";
		this.chkUseCostStructure.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.chkUseCostStructure.Location = new System.Drawing.Point(16, 372);
		this.chkUseCostStructure.Name = "chkUseCostStructure";
		this.chkUseCostStructure.Size = new System.Drawing.Size(551, 20);
		this.chkUseCostStructure.TabIndex = 48;
		this.chkUseCostStructure.Text = "啟動成本架構";
		this.groupBox5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox5.Location = new System.Drawing.Point(9, 348);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(555, 8);
		this.groupBox5.TabIndex = 47;
		this.groupBox5.TabStop = false;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbCostStructure.Appearance = appearance3;
		this.lbCostStructure.Location = new System.Drawing.Point(9, 332);
		this.lbCostStructure.Name = "lbCostStructure";
		this.lbCostStructure.Size = new System.Drawing.Size(160, 23);
		this.lbCostStructure.TabIndex = 46;
		this.lbCostStructure.Text = "成本架構";
		this.ultraLabel30.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance4.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
		appearance4.FontData.SizeInPoints = 9f;
		appearance4.ForeColor = System.Drawing.Color.Green;
		this.ultraLabel30.Appearance = appearance4;
		this.ultraLabel30.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel30.Location = new System.Drawing.Point(24, 72);
		this.ultraLabel30.Name = "ultraLabel30";
		this.ultraLabel30.Size = new System.Drawing.Size(536, 16);
		this.ultraLabel30.TabIndex = 45;
		this.ultraLabel30.Text = "(新增專案時，預設此機關單位會自動帶入專案基本資訊內之主辦單位資料欄位)";
		this.chk_Restore.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.chk_Restore.Location = new System.Drawing.Point(16, 572);
		this.chk_Restore.Name = "chk_Restore";
		this.chk_Restore.Size = new System.Drawing.Size(551, 20);
		this.chk_Restore.TabIndex = 44;
		this.chk_Restore.Text = "將操作畫面變更為預算編輯模式";
		this.chk_Restore.Visible = false;
		this.chk_IsTooltip.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.chk_IsTooltip.Location = new System.Drawing.Point(16, 128);
		this.chk_IsTooltip.Name = "chk_IsTooltip";
		this.chk_IsTooltip.Size = new System.Drawing.Size(551, 20);
		this.chk_IsTooltip.TabIndex = 43;
		this.chk_IsTooltip.Text = "在資料列上，當欄位不夠寬時不自動顯示提示標籤(Tooltip)";
		this.chk_IsEight.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.chk_IsEight.Location = new System.Drawing.Point(16, 104);
		this.chk_IsEight.Name = "chk_IsEight";
		this.chk_IsEight.Size = new System.Drawing.Size(551, 20);
		this.chk_IsEight.TabIndex = 42;
		this.chk_IsEight.Text = "製作預算書時，不要顯示大宗資材的警示語";
		this.txtMainInstituite.AutoSize = true;
		this.txtMainInstituite.Location = new System.Drawing.Point(120, 40);
		this.txtMainInstituite.Name = "txtMainInstituite";
		this.txtMainInstituite.Size = new System.Drawing.Size(440, 21);
		this.txtMainInstituite.TabIndex = 41;
		appearance5.FontData.Name = "Arial";
		this.ultraButton4.Appearance = appearance5;
		this.ultraButton4.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton4.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton4.Location = new System.Drawing.Point(560, 40);
		this.ultraButton4.Name = "ultraButton4";
		this.ultraButton4.ShowFocusRect = false;
		this.ultraButton4.ShowOutline = false;
		this.ultraButton4.Size = new System.Drawing.Size(24, 24);
		this.ultraButton4.SupportThemes = false;
		this.ultraButton4.TabIndex = 37;
		this.ultraButton4.Text = "...";
		this.ultraButton4.Click += new System.EventHandler(ultraButton4_Click_1);
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Location = new System.Drawing.Point(8, 264);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(555, 8);
		this.groupBox2.TabIndex = 31;
		this.groupBox2.TabStop = false;
		appearance6.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel14.Appearance = appearance6;
		this.ultraLabel14.Location = new System.Drawing.Point(8, 248);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(160, 23);
		this.ultraLabel14.TabIndex = 30;
		this.ultraLabel14.Text = "回復對話框設定值";
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Location = new System.Drawing.Point(8, 176);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(555, 8);
		this.groupBox1.TabIndex = 29;
		this.groupBox1.TabStop = false;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel13.Appearance = appearance7;
		this.ultraLabel13.Location = new System.Drawing.Point(8, 160);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(160, 23);
		this.ultraLabel13.TabIndex = 28;
		this.ultraLabel13.Text = "清空線上註冊資訊";
		this.groupBox7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox7.Location = new System.Drawing.Point(9, 25);
		this.groupBox7.Name = "groupBox7";
		this.groupBox7.Size = new System.Drawing.Size(555, 8);
		this.groupBox7.TabIndex = 22;
		this.groupBox7.TabStop = false;
		this.ultraLabel2.Location = new System.Drawing.Point(8, 46);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(120, 23);
		this.ultraLabel2.TabIndex = 20;
		this.ultraLabel2.Text = "預設機關代號：";
		appearance8.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel9.Appearance = appearance8;
		this.ultraLabel9.Location = new System.Drawing.Point(8, 8);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(48, 23);
		this.ultraLabel9.TabIndex = 3;
		this.ultraLabel9.Text = "一般";
		appearance9.FontData.Name = "細明體";
		appearance9.FontData.SizeInPoints = 11f;
		appearance9.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton3.Appearance = appearance9;
		this.ultraButton3.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton3.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton3.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton3.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton3.Location = new System.Drawing.Point(8, 192);
		this.ultraButton3.Name = "ultraButton3";
		this.ultraButton3.ShowFocusRect = false;
		this.ultraButton3.ShowOutline = false;
		this.ultraButton3.Size = new System.Drawing.Size(120, 27);
		this.ultraButton3.SupportThemes = false;
		this.ultraButton3.TabIndex = 22;
		this.ultraButton3.Text = "立即清空";
		this.ultraButton3.Click += new System.EventHandler(ultraButton3_Click);
		appearance10.ForeColor = System.Drawing.Color.Red;
		this.ultraLabel6.Appearance = appearance10;
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(136, 192);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(56, 23);
		this.ultraLabel6.TabIndex = 24;
		this.ultraLabel6.Text = "說明：";
		this.ultraLabel7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ultraLabel7.Location = new System.Drawing.Point(200, 192);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(360, 37);
		this.ultraLabel7.TabIndex = 25;
		this.ultraLabel7.Text = "如果你原本註冊資料不完整，想重新註冊，請執行[立即清空]來幫你清空原本的註冊資訊";
		appearance11.FontData.Name = "細明體";
		appearance11.FontData.SizeInPoints = 11f;
		appearance11.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnRecover.Appearance = appearance11;
		this.BtnRecover.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnRecover.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.BtnRecover.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnRecover.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnRecover.Location = new System.Drawing.Point(8, 280);
		this.BtnRecover.Name = "BtnRecover";
		this.BtnRecover.ShowFocusRect = false;
		this.BtnRecover.ShowOutline = false;
		this.BtnRecover.Size = new System.Drawing.Size(120, 27);
		this.BtnRecover.SupportThemes = false;
		this.BtnRecover.TabIndex = 21;
		this.BtnRecover.Text = "立即回復";
		this.BtnRecover.Click += new System.EventHandler(BtnRecover_Click);
		appearance12.ForeColor = System.Drawing.Color.Red;
		this.ultraLabel5.Appearance = appearance12;
		this.ultraLabel5.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel5.Location = new System.Drawing.Point(136, 280);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(56, 23);
		this.ultraLabel5.TabIndex = 23;
		this.ultraLabel5.Text = "說明：";
		this.ultraLabel4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ultraLabel4.Location = new System.Drawing.Point(200, 280);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(360, 46);
		this.ultraLabel4.TabIndex = 22;
		this.ultraLabel4.Text = "如果某些對話框所記錄的位置超出你的螢幕解析度範圍，請執行[立即回復]來幫你還原至最初狀態";
		this.gp_Restore.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gp_Restore.Location = new System.Drawing.Point(8, 548);
		this.gp_Restore.Name = "gp_Restore";
		this.gp_Restore.Size = new System.Drawing.Size(555, 8);
		this.gp_Restore.TabIndex = 31;
		this.gp_Restore.TabStop = false;
		this.gp_Restore.Visible = false;
		appearance13.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbGreenItem.Appearance = appearance13;
		this.lbGreenItem.Location = new System.Drawing.Point(9, 422);
		this.lbGreenItem.Name = "lbGreenItem";
		this.lbGreenItem.Size = new System.Drawing.Size(160, 23);
		this.lbGreenItem.TabIndex = 52;
		this.lbGreenItem.Text = "綠色內涵指標名稱";
		this.tbBudgetAndBidSetting.Controls.Add(this.chk_Number);
		this.tbBudgetAndBidSetting.Controls.Add(this.groupBox13);
		this.tbBudgetAndBidSetting.Controls.Add(this.BtnChangePath);
		this.tbBudgetAndBidSetting.Controls.Add(this.ultraLabel22);
		this.tbBudgetAndBidSetting.Controls.Add(this.chk_forDeleteNoUsedItem);
		this.tbBudgetAndBidSetting.Controls.Add(this.ultraLabel21);
		this.tbBudgetAndBidSetting.Controls.Add(this.groupBox8);
		this.tbBudgetAndBidSetting.Controls.Add(this.ultraLabel10);
		this.tbBudgetAndBidSetting.Controls.Add(this.ultraLabel1);
		this.tbBudgetAndBidSetting.Controls.Add(this.chk_DeleteAutoSave);
		this.tbBudgetAndBidSetting.Controls.Add(this.BDGT_Duration);
		this.tbBudgetAndBidSetting.Controls.Add(this.chkBDGT_AutoSave);
		this.tbBudgetAndBidSetting.Controls.Add(this.chk_forOldReCal);
		this.tbBudgetAndBidSetting.Controls.Add(this.ultraLabel8);
		this.tbBudgetAndBidSetting.Controls.Add(this.chk_AutoNum);
		this.tbBudgetAndBidSetting.Location = new System.Drawing.Point(-10000, -10000);
		this.tbBudgetAndBidSetting.Name = "tbBudgetAndBidSetting";
		this.tbBudgetAndBidSetting.Size = new System.Drawing.Size(580, 604);
		this.chk_Number.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.chk_Number.Location = new System.Drawing.Point(9, 93);
		this.chk_Number.Name = "chk_Number";
		this.chk_Number.Size = new System.Drawing.Size(551, 20);
		this.chk_Number.TabIndex = 34;
		this.chk_Number.Text = "編製時，插入工項後不自動執行項次重整";
		this.groupBox13.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox13.Controls.Add(this.rd_forOldReCal);
		this.groupBox13.ForeColor = System.Drawing.Color.Blue;
		this.groupBox13.Location = new System.Drawing.Point(8, 480);
		this.groupBox13.Name = "groupBox13";
		this.groupBox13.Size = new System.Drawing.Size(560, 123);
		this.groupBox13.TabIndex = 33;
		this.groupBox13.TabStop = false;
		this.groupBox13.Text = "重新總計時，工作要項與單價分析精度不同產生差額處理方式";
		this.groupBox13.Visible = false;
		this.rd_forOldReCal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.rd_forOldReCal.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.rd_forOldReCal.CheckedIndex = 0;
		this.rd_forOldReCal.ItemAppearance = appearance14;
		valueListItem1.DataValue = "TRUE";
		valueListItem1.DisplayText = "單價分析子項一定要有雜項作攤提";
		valueListItem2.DataValue = "FALSE";
		valueListItem2.DisplayText = "單價分析子項有雜項時則作攤提；沒雜項時則不攤提";
		valueListItem3.DataValue = "THIRD";
		valueListItem3.DisplayText = "一律不作攤提";
		this.rd_forOldReCal.Items.Add(valueListItem1);
		this.rd_forOldReCal.Items.Add(valueListItem2);
		this.rd_forOldReCal.Items.Add(valueListItem3);
		this.rd_forOldReCal.ItemSpacingVertical = 5;
		this.rd_forOldReCal.Location = new System.Drawing.Point(16, 59);
		this.rd_forOldReCal.Name = "rd_forOldReCal";
		this.rd_forOldReCal.Size = new System.Drawing.Size(528, 72);
		this.rd_forOldReCal.TabIndex = 0;
		this.rd_forOldReCal.Text = "單價分析子項一定要有雜項作攤提";
		appearance15.TextVAlign = Infragistics.Win.VAlign.Top;
		this.BtnChangePath.Appearance = appearance15;
		this.BtnChangePath.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.BtnChangePath.Location = new System.Drawing.Point(440, 64);
		this.BtnChangePath.Name = "BtnChangePath";
		this.BtnChangePath.Size = new System.Drawing.Size(120, 23);
		this.BtnChangePath.TabIndex = 32;
		this.BtnChangePath.Text = "變更路徑(&C)...";
		this.BtnChangePath.Visible = false;
		this.BtnChangePath.Click += new System.EventHandler(ultraButton4_Click);
		this.ultraLabel22.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance16.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
		appearance16.FontData.SizeInPoints = 9f;
		appearance16.ForeColor = System.Drawing.Color.Green;
		this.ultraLabel22.Appearance = appearance16;
		this.ultraLabel22.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel22.Location = new System.Drawing.Point(24, 268);
		this.ultraLabel22.Name = "ultraLabel22";
		this.ultraLabel22.Size = new System.Drawing.Size(536, 32);
		this.ultraLabel22.TabIndex = 30;
		this.ultraLabel22.Text = "(效能較差，但是當你再從基本工項資料庫引用同一編碼的工項時不會有困擾)建議，不勾選時，請先作一次重新總計再引用基本工項資料庫的工項";
		this.ultraLabel22.Visible = false;
		this.chk_forDeleteNoUsedItem.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.chk_forDeleteNoUsedItem.Location = new System.Drawing.Point(8, 244);
		this.chk_forDeleteNoUsedItem.Name = "chk_forDeleteNoUsedItem";
		this.chk_forDeleteNoUsedItem.Size = new System.Drawing.Size(551, 20);
		this.chk_forDeleteNoUsedItem.TabIndex = 29;
		this.chk_forDeleteNoUsedItem.Text = "編製時，刪除工項後自動檢查該工項是否已經沒有被引用";
		this.chk_forDeleteNoUsedItem.Visible = false;
		this.ultraLabel21.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance17.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
		appearance17.FontData.SizeInPoints = 9f;
		appearance17.ForeColor = System.Drawing.Color.Green;
		this.ultraLabel21.Appearance = appearance17;
		this.ultraLabel21.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel21.Location = new System.Drawing.Point(24, 67);
		this.ultraLabel21.Name = "ultraLabel21";
		this.ultraLabel21.Size = new System.Drawing.Size(536, 16);
		this.ultraLabel21.TabIndex = 28;
		this.groupBox8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox8.Location = new System.Drawing.Point(9, 25);
		this.groupBox8.Name = "groupBox8";
		this.groupBox8.Size = new System.Drawing.Size(555, 8);
		this.groupBox8.TabIndex = 24;
		this.groupBox8.TabStop = false;
		appearance18.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel10.Appearance = appearance18;
		this.ultraLabel10.Location = new System.Drawing.Point(8, 8);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(208, 23);
		this.ultraLabel10.TabIndex = 23;
		this.ultraLabel10.Text = "預算書編製/標單填寫";
		appearance19.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance19;
		this.ultraLabel1.Location = new System.Drawing.Point(246, 40);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(144, 23);
		this.ultraLabel1.TabIndex = 3;
		this.ultraLabel1.Text = "(不支援Win98/ME)";
		this.chk_DeleteAutoSave.Location = new System.Drawing.Point(441, 40);
		this.chk_DeleteAutoSave.Name = "chk_DeleteAutoSave";
		this.chk_DeleteAutoSave.Size = new System.Drawing.Size(111, 20);
		this.chk_DeleteAutoSave.TabIndex = 2;
		this.chk_DeleteAutoSave.Text = "刪除預算書項目之前，自動備份";
		this.chk_DeleteAutoSave.Visible = false;
		this.BDGT_Duration.Location = new System.Drawing.Point(160, 39);
		this.BDGT_Duration.Maximum = new decimal(new int[4] { 120, 0, 0, 0 });
		this.BDGT_Duration.Name = "BDGT_Duration";
		this.BDGT_Duration.Size = new System.Drawing.Size(80, 25);
		this.BDGT_Duration.TabIndex = 1;
		this.BDGT_Duration.Value = new decimal(new int[4] { 10, 0, 0, 0 });
		this.BDGT_Duration.ValueChanged += new System.EventHandler(BDGT_Duration_ValueChanged);
		this.BDGT_Duration.KeyDown += new System.Windows.Forms.KeyEventHandler(BDGT_Duration_KeyDown);
		this.chkBDGT_AutoSave.Location = new System.Drawing.Point(9, 43);
		this.chkBDGT_AutoSave.Name = "chkBDGT_AutoSave";
		this.chkBDGT_AutoSave.Size = new System.Drawing.Size(168, 20);
		this.chkBDGT_AutoSave.TabIndex = 0;
		this.chkBDGT_AutoSave.Text = "自動備份時間間隔";
		this.chkBDGT_AutoSave.CheckedChanged += new System.EventHandler(chkBDGT_AutoSave_CheckedChanged);
		this.chk_forOldReCal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.chk_forOldReCal.Location = new System.Drawing.Point(8, 312);
		this.chk_forOldReCal.Name = "chk_forOldReCal";
		this.chk_forOldReCal.Size = new System.Drawing.Size(551, 20);
		this.chk_forOldReCal.TabIndex = 4;
		this.chk_forOldReCal.Text = "重新總計時，工作要項與單價分析精度不同產生差額不予理會";
		this.chk_forOldReCal.Visible = false;
		this.ultraLabel8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance20.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
		appearance20.FontData.SizeInPoints = 9f;
		appearance20.ForeColor = System.Drawing.Color.Green;
		this.ultraLabel8.Appearance = appearance20;
		this.ultraLabel8.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel8.Location = new System.Drawing.Point(24, 336);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(536, 16);
		this.ultraLabel8.TabIndex = 24;
		this.ultraLabel8.Text = "(將造成資源統計表金額加總≠詳細表總價)";
		this.ultraLabel8.Visible = false;
		this.chk_AutoNum.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.chk_AutoNum.Location = new System.Drawing.Point(9, 125);
		this.chk_AutoNum.Name = "chk_AutoNum";
		this.chk_AutoNum.Size = new System.Drawing.Size(551, 20);
		this.chk_AutoNum.TabIndex = 34;
		this.chk_AutoNum.Text = "重新總計時，自動執行項次重整";
		this.tabAnalysisSetting.Controls.Add(this.chkIsDetail);
		this.tabAnalysisSetting.Controls.Add(this.chkAnalyis);
		this.tabAnalysisSetting.Controls.Add(this.chkMrsBItem);
		this.tabAnalysisSetting.Controls.Add(this.chk_Ana_UseNewOpen);
		this.tabAnalysisSetting.Controls.Add(this.groupBox9);
		this.tabAnalysisSetting.Controls.Add(this.ultraLabel11);
		this.tabAnalysisSetting.Controls.Add(this.chkUseNewMrsB);
		this.tabAnalysisSetting.Location = new System.Drawing.Point(-10000, -10000);
		this.tabAnalysisSetting.Name = "tabAnalysisSetting";
		this.tabAnalysisSetting.Size = new System.Drawing.Size(580, 604);
		this.chkIsDetail.Location = new System.Drawing.Point(8, 136);
		this.chkIsDetail.Name = "chkIsDetail";
		this.chkIsDetail.Size = new System.Drawing.Size(528, 20);
		this.chkIsDetail.TabIndex = 63;
		this.chkIsDetail.Text = "列印單價分析項目，詳細表有相同單價分析則以詳細表出現的順序為主";
		this.chkAnalyis.Location = new System.Drawing.Point(8, 104);
		this.chkAnalyis.Name = "chkAnalyis";
		this.chkAnalyis.Size = new System.Drawing.Size(392, 20);
		this.chkAnalyis.TabIndex = 62;
		this.chkAnalyis.Text = "零星工料通常輸入%，勾選則若輸入單價將不再提醒";
		this.chkMrsBItem.Location = new System.Drawing.Point(8, 72);
		this.chkMrsBItem.Name = "chkMrsBItem";
		this.chkMrsBItem.Size = new System.Drawing.Size(312, 20);
		this.chkMrsBItem.TabIndex = 26;
		this.chkMrsBItem.Text = "列印單價分析項目照流水號排序";
		this.chk_Ana_UseNewOpen.Location = new System.Drawing.Point(8, 168);
		this.chk_Ana_UseNewOpen.Name = "chk_Ana_UseNewOpen";
		this.chk_Ana_UseNewOpen.Size = new System.Drawing.Size(312, 20);
		this.chk_Ana_UseNewOpen.TabIndex = 25;
		this.chk_Ana_UseNewOpen.Text = "使用新的開啟單價分析方式";
		this.chk_Ana_UseNewOpen.Visible = false;
		this.groupBox9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox9.Location = new System.Drawing.Point(9, 25);
		this.groupBox9.Name = "groupBox9";
		this.groupBox9.Size = new System.Drawing.Size(555, 8);
		this.groupBox9.TabIndex = 24;
		this.groupBox9.TabStop = false;
		appearance21.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel11.Appearance = appearance21;
		this.ultraLabel11.Location = new System.Drawing.Point(8, 8);
		this.ultraLabel11.Name = "ultraLabel11";
		this.ultraLabel11.Size = new System.Drawing.Size(120, 23);
		this.ultraLabel11.TabIndex = 23;
		this.ultraLabel11.Text = "單價分析";
		this.chkUseNewMrsB.Location = new System.Drawing.Point(8, 42);
		this.chkUseNewMrsB.Name = "chkUseNewMrsB";
		this.chkUseNewMrsB.Size = new System.Drawing.Size(312, 20);
		this.chkUseNewMrsB.TabIndex = 0;
		this.chkUseNewMrsB.Text = "允許插入重複(相同編碼)的分析子項";
		this.Tab_D.Controls.Add(this.groupBox10);
		this.Tab_D.Controls.Add(this.ultraLabel12);
		this.Tab_D.Controls.Add(this.groupBox4);
		this.Tab_D.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_D.Name = "Tab_D";
		this.Tab_D.Size = new System.Drawing.Size(580, 604);
		this.groupBox10.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox10.Location = new System.Drawing.Point(9, 25);
		this.groupBox10.Name = "groupBox10";
		this.groupBox10.Size = new System.Drawing.Size(555, 8);
		this.groupBox10.TabIndex = 26;
		this.groupBox10.TabStop = false;
		appearance22.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel12.Appearance = appearance22;
		this.ultraLabel12.Location = new System.Drawing.Point(8, 8);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(48, 23);
		this.ultraLabel12.TabIndex = 25;
		this.ultraLabel12.Text = "計價";
		this.groupBox4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox4.Controls.Add(this.ultraLabel20);
		this.groupBox4.Controls.Add(this.ultraLabel3);
		this.groupBox4.Controls.Add(this.ultraButton2);
		this.groupBox4.Controls.Add(this.ultraButton1);
		this.groupBox4.Controls.Add(this.txtReportPack);
		this.groupBox4.Location = new System.Drawing.Point(8, 48);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(560, 145);
		this.groupBox4.TabIndex = 25;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "手動轉入計價報表";
		this.ultraLabel20.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance23.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
		appearance23.FontData.SizeInPoints = 9f;
		appearance23.ForeColor = System.Drawing.Color.Green;
		this.ultraLabel20.Appearance = appearance23;
		this.ultraLabel20.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel20.Location = new System.Drawing.Point(13, 101);
		this.ultraLabel20.Name = "ultraLabel20";
		this.ultraLabel20.Size = new System.Drawing.Size(405, 32);
		this.ultraLabel20.TabIndex = 31;
		this.ultraLabel20.Text = "報表格式請至技術資料庫網站 -> PCCES -> 經費電腦估價系統 PCCES 下載 -> 估驗計價報表下載";
		this.ultraLabel3.Location = new System.Drawing.Point(13, 27);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(123, 22);
		this.ultraLabel3.TabIndex = 21;
		this.ultraLabel3.Text = "計價報表轉入：";
		this.ultraButton2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance24.FontData.Name = "細明體";
		appearance24.FontData.SizeInPoints = 11f;
		appearance24.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton2.Appearance = appearance24;
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton2.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton2.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton2.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton2.Location = new System.Drawing.Point(424, 101);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(120, 27);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 20;
		this.ultraButton2.Text = "轉入";
		this.ultraButton2.Click += new System.EventHandler(ultraButton2_Click);
		this.ultraButton1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.ultraButton1.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton1.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ultraButton1.Location = new System.Drawing.Point(488, 60);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(56, 25);
		this.ultraButton1.TabIndex = 1;
		this.ultraButton1.Text = "瀏覽...";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		this.txtReportPack.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtReportPack.AutoSize = true;
		this.txtReportPack.Location = new System.Drawing.Point(96, 62);
		this.txtReportPack.Name = "txtReportPack";
		this.txtReportPack.Size = new System.Drawing.Size(392, 21);
		this.txtReportPack.TabIndex = 0;
		this.tabProxySetting.Controls.Add(this.btnTestConnection);
		this.tabProxySetting.Controls.Add(this.gbAuthority);
		this.tabProxySetting.Controls.Add(this.gbProxySetting);
		this.tabProxySetting.Controls.Add(this.groupBox14);
		this.tabProxySetting.Controls.Add(this.lbProxySetting);
		this.tabProxySetting.Location = new System.Drawing.Point(-10000, -10000);
		this.tabProxySetting.Name = "tabProxySetting";
		this.tabProxySetting.Size = new System.Drawing.Size(580, 604);
		this.btnTestConnection.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance25.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnTestConnection.Appearance = appearance25;
		this.btnTestConnection.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnTestConnection.ImageSize = new System.Drawing.Size(20, 20);
		this.btnTestConnection.ImageTransparentColor = System.Drawing.Color.White;
		this.btnTestConnection.Location = new System.Drawing.Point(473, 372);
		this.btnTestConnection.Name = "btnTestConnection";
		this.btnTestConnection.ShowFocusRect = false;
		this.btnTestConnection.ShowOutline = false;
		this.btnTestConnection.Size = new System.Drawing.Size(93, 31);
		this.btnTestConnection.SupportThemes = false;
		this.btnTestConnection.TabIndex = 31;
		this.btnTestConnection.Text = "測試連線";
		this.btnTestConnection.Click += new System.EventHandler(btnTestConnection_Click);
		this.gbAuthority.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gbAuthority.Controls.Add(this.chkNeedAutority);
		this.gbAuthority.Controls.Add(this.tbPassword);
		this.gbAuthority.Controls.Add(this.tbAccount);
		this.gbAuthority.Controls.Add(this.label8);
		this.gbAuthority.Controls.Add(this.label10);
		this.gbAuthority.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gbAuthority.Location = new System.Drawing.Point(11, 215);
		this.gbAuthority.Name = "gbAuthority";
		this.gbAuthority.Size = new System.Drawing.Size(555, 144);
		this.gbAuthority.TabIndex = 30;
		this.gbAuthority.TabStop = false;
		this.gbAuthority.Text = "HTTP 授權";
		this.chkNeedAutority.Location = new System.Drawing.Point(12, 24);
		this.chkNeedAutority.Name = "chkNeedAutority";
		this.chkNeedAutority.Size = new System.Drawing.Size(408, 24);
		this.chkNeedAutority.TabIndex = 9;
		this.chkNeedAutority.Text = "需要授權才能透過我的防火牆或 Proxy 伺服器進行連線";
		this.chkNeedAutority.CheckedChanged += new System.EventHandler(chkNeedAutority_CheckedChanged);
		this.tbPassword.Enabled = false;
		this.tbPassword.Location = new System.Drawing.Point(96, 104);
		this.tbPassword.Name = "tbPassword";
		this.tbPassword.PasswordChar = '*';
		this.tbPassword.Size = new System.Drawing.Size(320, 25);
		this.tbPassword.TabIndex = 6;
		this.tbAccount.Enabled = false;
		this.tbAccount.Location = new System.Drawing.Point(96, 64);
		this.tbAccount.Name = "tbAccount";
		this.tbAccount.Size = new System.Drawing.Size(320, 25);
		this.tbAccount.TabIndex = 5;
		this.label8.Location = new System.Drawing.Point(8, 64);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(88, 24);
		this.label8.TabIndex = 1;
		this.label8.Text = "使用者名稱";
		this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label10.Location = new System.Drawing.Point(8, 104);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(80, 24);
		this.label10.TabIndex = 3;
		this.label10.Text = "密碼";
		this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.gbProxySetting.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gbProxySetting.Controls.Add(this.cbUseProxy);
		this.gbProxySetting.Controls.Add(this.tbPort);
		this.gbProxySetting.Controls.Add(this.tbAddress);
		this.gbProxySetting.Controls.Add(this.label3);
		this.gbProxySetting.Controls.Add(this.label9);
		this.gbProxySetting.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gbProxySetting.Location = new System.Drawing.Point(11, 49);
		this.gbProxySetting.Name = "gbProxySetting";
		this.gbProxySetting.Size = new System.Drawing.Size(555, 144);
		this.gbProxySetting.TabIndex = 29;
		this.gbProxySetting.TabStop = false;
		this.gbProxySetting.Text = "HTTP Proxy 設定";
		this.cbUseProxy.Location = new System.Drawing.Point(11, 24);
		this.cbUseProxy.Name = "cbUseProxy";
		this.cbUseProxy.Size = new System.Drawing.Size(400, 32);
		this.cbUseProxy.TabIndex = 8;
		this.cbUseProxy.Text = "使用 Proxy 伺服器進行 HTTP 連線";
		this.cbUseProxy.CheckedChanged += new System.EventHandler(cbUseProxy_CheckedChanged);
		this.tbPort.Enabled = false;
		this.tbPort.Location = new System.Drawing.Point(96, 104);
		this.tbPort.Name = "tbPort";
		this.tbPort.Size = new System.Drawing.Size(80, 25);
		this.tbPort.TabIndex = 5;
		this.tbAddress.Enabled = false;
		this.tbAddress.Location = new System.Drawing.Point(96, 64);
		this.tbAddress.Name = "tbAddress";
		this.tbAddress.Size = new System.Drawing.Size(320, 25);
		this.tbAddress.TabIndex = 4;
		this.tbAddress.Leave += new System.EventHandler(tbAddress_Leave);
		this.label3.Location = new System.Drawing.Point(8, 64);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(80, 24);
		this.label3.TabIndex = 0;
		this.label3.Text = "位址";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label9.Location = new System.Drawing.Point(8, 104);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(80, 24);
		this.label9.TabIndex = 2;
		this.label9.Text = "連接埠";
		this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.groupBox14.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox14.Location = new System.Drawing.Point(11, 25);
		this.groupBox14.Name = "groupBox14";
		this.groupBox14.Size = new System.Drawing.Size(555, 8);
		this.groupBox14.TabIndex = 28;
		this.groupBox14.TabStop = false;
		appearance26.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbProxySetting.Appearance = appearance26;
		this.lbProxySetting.Location = new System.Drawing.Point(8, 8);
		this.lbProxySetting.Name = "lbProxySetting";
		this.lbProxySetting.Size = new System.Drawing.Size(88, 23);
		this.lbProxySetting.TabIndex = 27;
		this.lbProxySetting.Text = "代理伺服器";
		this.tabDatabaseSetting.Controls.Add(this.BtnMemApply);
		this.tabDatabaseSetting.Controls.Add(this.groupBox12);
		this.tabDatabaseSetting.Controls.Add(this.numSQLMem);
		this.tabDatabaseSetting.Controls.Add(this.ultraLabel28);
		this.tabDatabaseSetting.Controls.Add(this.lblSQLMem);
		this.tabDatabaseSetting.Controls.Add(this.ultraLabel29);
		this.tabDatabaseSetting.Controls.Add(this.lblPhysicalMem);
		this.tabDatabaseSetting.Controls.Add(this.ultraLabel26);
		this.tabDatabaseSetting.Controls.Add(this.groupBox11);
		this.tabDatabaseSetting.Controls.Add(this.ultraLabel27);
		this.tabDatabaseSetting.Location = new System.Drawing.Point(-10000, -10000);
		this.tabDatabaseSetting.Name = "tabDatabaseSetting";
		this.tabDatabaseSetting.Size = new System.Drawing.Size(580, 604);
		appearance27.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnMemApply.Appearance = appearance27;
		this.BtnMemApply.BackColor = System.Drawing.SystemColors.Control;
		this.BtnMemApply.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button;
		this.BtnMemApply.Location = new System.Drawing.Point(432, 85);
		this.BtnMemApply.Name = "BtnMemApply";
		this.BtnMemApply.ShowFocusRect = false;
		this.BtnMemApply.ShowOutline = false;
		this.BtnMemApply.Size = new System.Drawing.Size(75, 24);
		this.BtnMemApply.SupportThemes = false;
		this.BtnMemApply.TabIndex = 40;
		this.BtnMemApply.Text = "套用";
		this.BtnMemApply.Click += new System.EventHandler(BtnMemApply_Click);
		this.groupBox12.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox12.Location = new System.Drawing.Point(8, 208);
		this.groupBox12.Name = "groupBox12";
		this.groupBox12.Size = new System.Drawing.Size(555, 8);
		this.groupBox12.TabIndex = 39;
		this.groupBox12.TabStop = false;
		this.numSQLMem.Location = new System.Drawing.Point(278, 86);
		this.numSQLMem.Maximum = new decimal(new int[4] { 2048, 0, 0, 0 });
		this.numSQLMem.Minimum = new decimal(new int[4] { 32, 0, 0, 0 });
		this.numSQLMem.Name = "numSQLMem";
		this.numSQLMem.Size = new System.Drawing.Size(120, 25);
		this.numSQLMem.TabIndex = 38;
		this.numSQLMem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.numSQLMem.Value = new decimal(new int[4] { 128, 0, 0, 0 });
		this.ultraLabel28.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance28.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
		appearance28.FontData.SizeInPoints = 9f;
		appearance28.ForeColor = System.Drawing.Color.Green;
		this.ultraLabel28.Appearance = appearance28;
		this.ultraLabel28.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel28.Location = new System.Drawing.Point(24, 128);
		this.ultraLabel28.Name = "ultraLabel28";
		this.ultraLabel28.Size = new System.Drawing.Size(536, 72);
		this.ultraLabel28.TabIndex = 37;
		this.ultraLabel28.Text = "說明：使用者可自行調整SQL記憶體的使用量。\r\n\u3000\u3000\u3000假設你有256MB 的RAM，建議配置量約128MB。\r\n\r\n為何要調整SQL記憶體使用量：SQL 資料庫為了加速存取資料，會無限地持續擴增佔用PC上的記憶體，卻造成其他程式執行效能變差";
		appearance29.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblSQLMem.Appearance = appearance29;
		this.lblSQLMem.Location = new System.Drawing.Point(404, 88);
		this.lblSQLMem.Name = "lblSQLMem";
		this.lblSQLMem.Size = new System.Drawing.Size(28, 23);
		this.lblSQLMem.TabIndex = 36;
		this.lblSQLMem.Text = "MB";
		appearance30.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel29.Appearance = appearance30;
		this.ultraLabel29.Location = new System.Drawing.Point(24, 88);
		this.ultraLabel29.Name = "ultraLabel29";
		this.ultraLabel29.Size = new System.Drawing.Size(232, 23);
		this.ultraLabel29.TabIndex = 35;
		this.ultraLabel29.Text = "配置給SQL使用的記憶體上限值:";
		appearance31.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance31.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblPhysicalMem.Appearance = appearance31;
		this.lblPhysicalMem.Location = new System.Drawing.Point(264, 50);
		this.lblPhysicalMem.Name = "lblPhysicalMem";
		this.lblPhysicalMem.Size = new System.Drawing.Size(160, 23);
		this.lblPhysicalMem.TabIndex = 34;
		this.lblPhysicalMem.Text = "MB";
		appearance32.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel26.Appearance = appearance32;
		this.ultraLabel26.Location = new System.Drawing.Point(24, 50);
		this.ultraLabel26.Name = "ultraLabel26";
		this.ultraLabel26.Size = new System.Drawing.Size(176, 23);
		this.ultraLabel26.TabIndex = 33;
		this.ultraLabel26.Text = "目前 PC 的實體記憶體:";
		this.groupBox11.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox11.Location = new System.Drawing.Point(8, 32);
		this.groupBox11.Name = "groupBox11";
		this.groupBox11.Size = new System.Drawing.Size(555, 8);
		this.groupBox11.TabIndex = 32;
		this.groupBox11.TabStop = false;
		appearance33.ForeColor = System.Drawing.Color.Blue;
		appearance33.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel27.Appearance = appearance33;
		this.ultraLabel27.Location = new System.Drawing.Point(8, 8);
		this.ultraLabel27.Name = "ultraLabel27";
		this.ultraLabel27.Size = new System.Drawing.Size(144, 23);
		this.ultraLabel27.TabIndex = 31;
		this.ultraLabel27.Text = "資料庫記憶體配置";
		this.Tab_E.Controls.Add(this.ultraLabel17);
		this.Tab_E.Controls.Add(this.cboExlFont);
		this.Tab_E.Controls.Add(this.ultraLabel16);
		this.Tab_E.Controls.Add(this.groupBox3);
		this.Tab_E.Controls.Add(this.ultraLabel15);
		this.Tab_E.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_E.Name = "Tab_E";
		this.Tab_E.Size = new System.Drawing.Size(580, 604);
		this.ultraLabel17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance34.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
		appearance34.FontData.SizeInPoints = 9f;
		appearance34.ForeColor = System.Drawing.Color.Green;
		this.ultraLabel17.Appearance = appearance34;
		this.ultraLabel17.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel17.Location = new System.Drawing.Point(160, 69);
		this.ultraLabel17.Name = "ultraLabel17";
		this.ultraLabel17.Size = new System.Drawing.Size(400, 16);
		this.ultraLabel17.TabIndex = 31;
		this.ultraLabel17.Text = "(建議字型只使用細明體或標楷體)";
		this.cboExlFont.AutoSize = true;
		this.cboExlFont.Location = new System.Drawing.Point(158, 40);
		this.cboExlFont.Name = "cboExlFont";
		this.cboExlFont.Size = new System.Drawing.Size(250, 21);
		this.cboExlFont.TabIndex = 30;
		this.cboExlFont.Text = null;
		appearance35.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel16.Appearance = appearance35;
		this.ultraLabel16.Location = new System.Drawing.Point(8, 40);
		this.ultraLabel16.Name = "ultraLabel16";
		this.ultraLabel16.Size = new System.Drawing.Size(160, 23);
		this.ultraLabel16.TabIndex = 29;
		this.ultraLabel16.Text = "EXCEL輸出使用字型:";
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox3.Location = new System.Drawing.Point(11, 25);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(555, 8);
		this.groupBox3.TabIndex = 28;
		this.groupBox3.TabStop = false;
		appearance36.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel15.Appearance = appearance36;
		this.ultraLabel15.Location = new System.Drawing.Point(8, 8);
		this.ultraLabel15.Name = "ultraLabel15";
		this.ultraLabel15.Size = new System.Drawing.Size(144, 23);
		this.ultraLabel15.TabIndex = 27;
		this.ultraLabel15.Text = "電子標單製作";
		this.tabMrsBaseSetting.Controls.Add(this.ultraLabel25);
		this.tabMrsBaseSetting.Controls.Add(this.chkMrs_AutoChangeRate);
		this.tabMrsBaseSetting.Controls.Add(this.ultraLabel24);
		this.tabMrsBaseSetting.Controls.Add(this.chkMrs_LoadMethod);
		this.tabMrsBaseSetting.Controls.Add(this.groupBox6);
		this.tabMrsBaseSetting.Controls.Add(this.ultraLabel23);
		this.tabMrsBaseSetting.Location = new System.Drawing.Point(-10000, -10000);
		this.tabMrsBaseSetting.Name = "tabMrsBaseSetting";
		this.tabMrsBaseSetting.Size = new System.Drawing.Size(580, 604);
		this.ultraLabel25.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance37.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
		appearance37.FontData.SizeInPoints = 9f;
		appearance37.ForeColor = System.Drawing.Color.Green;
		this.ultraLabel25.Appearance = appearance37;
		this.ultraLabel25.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel25.Location = new System.Drawing.Point(24, 80);
		this.ultraLabel25.Name = "ultraLabel25";
		this.ultraLabel25.Size = new System.Drawing.Size(536, 24);
		this.ultraLabel25.TabIndex = 34;
		this.ultraLabel25.Text = "如:0321000001-->M0321000001，材料比率自動預設成100%";
		this.chkMrs_AutoChangeRate.Location = new System.Drawing.Point(8, 56);
		this.chkMrs_AutoChangeRate.Name = "chkMrs_AutoChangeRate";
		this.chkMrs_AutoChangeRate.Size = new System.Drawing.Size(552, 20);
		this.chkMrs_AutoChangeRate.TabIndex = 33;
		this.chkMrs_AutoChangeRate.Text = "開啟工項編輯畫面時，工項代碼變更時，自動依開頭英文字母給定比率";
		this.ultraLabel24.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance38.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
		appearance38.FontData.SizeInPoints = 9f;
		appearance38.ForeColor = System.Drawing.Color.Green;
		this.ultraLabel24.Appearance = appearance38;
		this.ultraLabel24.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel24.Location = new System.Drawing.Point(24, 128);
		this.ultraLabel24.Name = "ultraLabel24";
		this.ultraLabel24.Size = new System.Drawing.Size(536, 24);
		this.ultraLabel24.TabIndex = 32;
		this.ultraLabel24.Text = "(工項載入的效能加速，但載入過程中畫面會變成空白)";
		this.ultraLabel24.Visible = false;
		this.chkMrs_LoadMethod.Location = new System.Drawing.Point(8, 104);
		this.chkMrs_LoadMethod.Name = "chkMrs_LoadMethod";
		this.chkMrs_LoadMethod.Size = new System.Drawing.Size(312, 20);
		this.chkMrs_LoadMethod.TabIndex = 31;
		this.chkMrs_LoadMethod.Text = "使用快速載入的方法";
		this.chkMrs_LoadMethod.Visible = false;
		this.groupBox6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox6.Location = new System.Drawing.Point(8, 32);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(555, 8);
		this.groupBox6.TabIndex = 30;
		this.groupBox6.TabStop = false;
		appearance39.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel23.Appearance = appearance39;
		this.ultraLabel23.Location = new System.Drawing.Point(8, 8);
		this.ultraLabel23.Name = "ultraLabel23";
		this.ultraLabel23.Size = new System.Drawing.Size(144, 23);
		this.ultraLabel23.TabIndex = 29;
		this.ultraLabel23.Text = "工項基本資料庫";
		appearance40.BackColor = System.Drawing.SystemColors.Control;
		appearance40.FontData.SizeInPoints = 11f;
		this.ultraStatusBar1.Appearance = appearance40;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 681);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Width = 200;
		appearance41.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance41.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		ultraStatusPanel2.Appearance = appearance41;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.MarqueeInfo.Delay = 200;
		ultraStatusPanel2.MarqueeInfo.IsActive = true;
		ultraStatusPanel2.MarqueeInfo.MarqueeScrollAmount = 3;
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		ultraStatusPanel2.Style = Infragistics.Win.UltraWinStatusBar.PanelStyle.Marquee;
		appearance42.TextHAlign = Infragistics.Win.HAlign.Right;
		ultraStatusPanel3.Appearance = appearance42;
		ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel3.Text = "客服電話:(02)2708-8090";
		ultraStatusPanel3.Width = 200;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[3] { ultraStatusPanel1, ultraStatusPanel2, ultraStatusPanel3 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(752, 23);
		this.ultraStatusBar1.SupportThemes = false;
		this.ultraStatusBar1.TabIndex = 17;
		this.ultraStatusBar1.Text = "ultraStatusBar1";
		this.ultraStatusBar1.PanelClick += new Infragistics.Win.UltraWinStatusBar.PanelClickEventHandler(ultraStatusBar1_PanelClick);
		this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel1.AutoScroll = true;
		this.panel1.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel1.Controls.Add(this.Tab_Ctrl);
		this.panel1.Location = new System.Drawing.Point(152, 40);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(584, 608);
		this.panel1.TabIndex = 18;
		this.Tab_Ctrl.BackColor = System.Drawing.Color.White;
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.tabGeneralSetting);
		this.Tab_Ctrl.Controls.Add(this.tbBudgetAndBidSetting);
		this.Tab_Ctrl.Controls.Add(this.tabAnalysisSetting);
		this.Tab_Ctrl.Controls.Add(this.Tab_D);
		this.Tab_Ctrl.Controls.Add(this.Tab_E);
		this.Tab_Ctrl.Controls.Add(this.tabMrsBaseSetting);
		this.Tab_Ctrl.Controls.Add(this.tabDatabaseSetting);
		this.Tab_Ctrl.Controls.Add(this.tabProxySetting);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(580, 604);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabIndex = 28;
		ultraTab1.TabPage = this.tabGeneralSetting;
		ultraTab1.Text = "一般";
		ultraTab2.TabPage = this.tbBudgetAndBidSetting;
		ultraTab2.Text = "預算書編製";
		ultraTab3.TabPage = this.tabAnalysisSetting;
		ultraTab3.Text = "單價分析";
		ultraTab4.TabPage = this.Tab_D;
		ultraTab4.Text = "計價";
		ultraTab5.TabPage = this.tabProxySetting;
		ultraTab5.Text = "代理伺服器";
		ultraTab6.Key = "Tab_G";
		ultraTab6.TabPage = this.tabDatabaseSetting;
		ultraTab6.Text = "tab2";
		ultraTab7.TabPage = this.Tab_E;
		ultraTab7.Text = "電子標單";
		ultraTab8.TabPage = this.tabMrsBaseSetting;
		ultraTab8.Text = "tab1";
		ultraTab8.Visible = false;
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[8] { ultraTab1, ultraTab2, ultraTab3, ultraTab4, ultraTab5, ultraTab6, ultraTab7, ultraTab8 });
		this.Tab_Ctrl.ActiveTabChanged += new Infragistics.Win.UltraWinTabControl.ActiveTabChangedEventHandler(Tab_Ctrl_ActiveTabChanged);
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(580, 604);
		this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		appearance43.FontData.Name = "細明體";
		appearance43.FontData.SizeInPoints = 11f;
		appearance43.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnSave.Appearance = appearance43;
		this.btnSave.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnSave.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnSave.ImageSize = new System.Drawing.Size(20, 20);
		this.btnSave.ImageTransparentColor = System.Drawing.Color.White;
		this.btnSave.Location = new System.Drawing.Point(661, 651);
		this.btnSave.Name = "btnSave";
		this.btnSave.ShowFocusRect = false;
		this.btnSave.ShowOutline = false;
		this.btnSave.Size = new System.Drawing.Size(75, 27);
		this.btnSave.SupportThemes = false;
		this.btnSave.TabIndex = 19;
		this.btnSave.Text = "存檔";
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.gridSettingsList.AllowEditing = false;
		this.gridSettingsList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.gridSettingsList.BackColor = System.Drawing.SystemColors.Window;
		this.gridSettingsList.ColumnInfo = "1,0,0,0,0,110,Columns:0{DataType:System.String;TextAlign:LeftCenter;}\t";
		this.gridSettingsList.ExtendLastCol = true;
		this.gridSettingsList.ForeColor = System.Drawing.SystemColors.WindowText;
		this.gridSettingsList.Location = new System.Drawing.Point(16, 40);
		this.gridSettingsList.Name = "gridSettingsList";
		this.gridSettingsList.Rows.Count = 4;
		this.gridSettingsList.Rows.Fixed = 0;
		this.gridSettingsList.Size = new System.Drawing.Size(128, 608);
		this.gridSettingsList.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridSettingsList.Styles"));
		this.gridSettingsList.TabIndex = 20;
		this.gridSettingsList.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(GridList_AfterRowColChange);
		this.ultraLabel18.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ultraLabel18.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel18.Location = new System.Drawing.Point(14, 19);
		this.ultraLabel18.Name = "ultraLabel18";
		this.ultraLabel18.Size = new System.Drawing.Size(120, 16);
		this.ultraLabel18.TabIndex = 32;
		this.ultraLabel18.Text = "類別:";
		this.ultraLabel19.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ultraLabel19.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel19.Location = new System.Drawing.Point(152, 19);
		this.ultraLabel19.Name = "ultraLabel19";
		this.ultraLabel19.Size = new System.Drawing.Size(472, 16);
		this.ultraLabel19.TabIndex = 33;
		this.ultraLabel19.Text = "提示:當您做完設定值變更後，記得要按一下右下角的[存檔]按鈕";
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.Controls.Add(this.ultraLabel19);
		base.Controls.Add(this.ultraLabel18);
		base.Controls.Add(this.gridSettingsList);
		base.Controls.Add(this.btnSave);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.ultraStatusBar1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Name = "FormSys_Z";
		base.Size = new System.Drawing.Size(752, 704);
		base.Load += new System.EventHandler(FormSys_Z_Load);
		this.tabGeneralSetting.ResumeLayout(false);
		this.tabGeneralSetting.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtMainInstituite).EndInit();
		this.tbBudgetAndBidSetting.ResumeLayout(false);
		this.groupBox13.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rd_forOldReCal).EndInit();
		((System.ComponentModel.ISupportInitialize)this.BDGT_Duration).EndInit();
		this.tabAnalysisSetting.ResumeLayout(false);
		this.Tab_D.ResumeLayout(false);
		this.groupBox4.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtReportPack).EndInit();
		this.tabProxySetting.ResumeLayout(false);
		this.gbAuthority.ResumeLayout(false);
		this.gbAuthority.PerformLayout();
		this.gbProxySetting.ResumeLayout(false);
		this.gbProxySetting.PerformLayout();
		this.tabDatabaseSetting.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.numSQLMem).EndInit();
		this.Tab_E.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.cboExlFont).EndInit();
		this.tabMrsBaseSetting.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).EndInit();
		this.Tab_Ctrl.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridSettingsList).EndInit();
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
