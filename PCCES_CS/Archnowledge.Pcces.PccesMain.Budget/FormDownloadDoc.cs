using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.ShellLib;
using Archnowledge.Pcces.PccesMain.WSCode;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinProgressBar;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormDownloadDoc : Form
{
	private enum FileOverWriteOptions
	{
		Yes,
		YesToAll,
		No,
		NoToAll
	}

	private const int F_CustomizedServerTimeout = 300000;

	private static string F_ProjectCode;

	private static string F_DB;

	private static string F_UserID;

	private DataTable DT = new DataTable();

	private string sProjectCode = "";

	private string F_ConnectionString = "";

	private bool F_IsCustomizedMode = false;

	private Uri F_CustomizedServerUri;

	private NetworkCredential F_CustomizedServerCredential;

	private bool F_CustomizedServerUsePassive = false;

	private DataTable F_CustomizedServerFile;

	private string F_Edition = "";

	private string checkfirst = "";

	private int iRecord = 1;

	private bool IsAllSelect = false;

	private int iRecordCount = 0;

	private UltraToolbarsManager ultraToolbarsManager2;

	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_A;

	private Label label1;

	private Panel panel7;

	private GroupBox groupBox3;

	private UltraButton A_Btn_Cncl;

	private UltraButton A_Btn_Next;

	private UltraTabPageControl Tab_B;

	private Splitter splitter2;

	private Panel panel6;

	private C1FlexGrid Grid1;

	private UltraLabel lblName;

	private UltraToolbarsDockArea _panel6_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _panel6_Toolbars_Dock_Area_Right;

	private UltraToolbarsDockArea _panel6_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _panel6_Toolbars_Dock_Area_Bottom;

	private UltraStatusBar ultraStatusBar1;

	private Panel panel2;

	private C1FlexGrid gridProjectUsr;

	private UltraLabel ultraLabel2;

	private UltraToolbarsDockArea _panel2_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _panel2_Toolbars_Dock_Area_Right;

	private UltraToolbarsDockArea _panel2_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _panel2_Toolbars_Dock_Area_Bottom;

	private Panel panel3;

	private Panel panel5;

	private Panel panel1;

	private Panel panel4;

	private GroupBox groupBox2;

	private Label lblDBName;

	private UltraTabPageControl Tab_C;

	private UltraProgressBar Prog1;

	private Panel panel8;

	private GroupBox groupBox4;

	private UltraButton ultraButton5;

	private UltraButton ultraButton6;

	private UltraProgressBar Prog2;

	private Panel panel9;

	private ImageList imageList2;

	private IContainer components;

	private UltraButton ultraButton7;

	private UltraButton ultraButton8;

	private UltraToolbarsManager ultraToolbarsManager1;

	private UltraToolbarsDockArea _DownloadDoc_Toolbars_Dock_Area_Left;

	private Panel panel2_Fill_Panel;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel3;

	private Label label2;

	private Label label3;

	private UltraStatusBar ultraStatusBar3;

	private Panel panel15;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel7;

	private Label label4;

	private Label label5;

	protected OleDbDataAdapter DbAdpt;

	public FormDownloadDoc(string ProjectCode, string DatabaseName, string UserID)
	{
		InitializeComponent();
		F_ProjectCode = ProjectCode;
		F_DB = DatabaseName;
		F_UserID = UserID;
		string ConnectionStringOri = GetPccesMasterConnectionString();
		ConnectionStringUtility connUtility = new ConnectionStringUtility(ConnectionStringOri);
		F_ConnectionString = connUtility.GetSqlConnectionString(DatabaseName);
		F_CustomizedServerFile = new DataTable();
		F_CustomizedServerFile.Columns.Add("FileName", typeof(string));
		F_CustomizedServerFile.Columns.Add("ChapterNo", typeof(string));
		F_CustomizedServerFile.Columns.Add("ChapterName", typeof(string));
		F_CustomizedServerFile.Columns.Add("FileEdition", typeof(string));
		if (F_IsCustomizedMode = ArchConvert.Obj2Bool(ConfigurationManager.AppSettings["DownloadDocCustomizedMode"]))
		{
			string downloadDescription = PubTools.GetAppSet_String("DownloadDocCustomizedServerDescription");
			if (downloadDescription != string.Empty)
			{
				ultraToolbarsManager1.Tools["MnuCustomizedDownloadAllProject"].SharedProps.Caption = "下載(" + downloadDescription + ")";
				ultraToolbarsManager2.Tools["MnuCustomizedDownloadSingleProject"].SharedProps.Caption = "下載(" + downloadDescription + ")";
				Grid1.Cols["DocCustomized"].Caption = downloadDescription;
			}
			ultraToolbarsManager1.Tools["MnuAllDownLoad"].SharedProps.Caption = "下載(工程會)";
			ultraToolbarsManager2.Tools["MnuDownLoad"].SharedProps.Caption = "下載(工程會)";
			try
			{
				GetCustomizedDocList();
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		ultraToolbarsManager1.Tools["MnuCustomizedDownloadAllProject"].SharedProps.Visible = F_IsCustomizedMode;
		ultraToolbarsManager2.Tools["MnuCustomizedDownloadSingleProject"].SharedProps.Visible = F_IsCustomizedMode;
		Grid1.Cols["DocPcc"].Visible = F_IsCustomizedMode;
		Grid1.Cols["DocCustomized"].Visible = F_IsCustomizedMode;
	}

	private void GetCustomizedDocList()
	{
		F_CustomizedServerCredential = new NetworkCredential(ArchConvert.Obj2String(ConfigurationManager.AppSettings["DownloadDocCustomizedServerUserName"]), ArchConvert.Obj2String(ConfigurationManager.AppSettings["DownloadDocCustomizedServerPassword"]));
		F_CustomizedServerUsePassive = ArchConvert.Obj2Bool(ConfigurationManager.AppSettings["DownloadDocCustomizedServerUsePassive"]);
		try
		{
			F_CustomizedServerUri = new Uri(ArchConvert.Obj2String(ConfigurationManager.AppSettings["DownloadDocCustomizedServer"]));
		}
		catch
		{
			throw new Exception("server 位址錯誤，請檢查AppSettings[\"DownloadDocCustomizedServer\"]");
		}
		if (F_CustomizedServerUri.Scheme.ToUpper() == "HTTP")
		{
			GetCustomizedDocListByHttp();
		}
		else
		{
			GetCustomizedDocListByFtp();
		}
	}

	private void GetCustomizedDocListByHttp()
	{
		HtmlElementCollection links;
		try
		{
			WebBrowser webBrowser = new WebBrowser();
			bool docCompleted = false;
			webBrowser.DocumentCompleted += delegate
			{
				docCompleted = true;
			};
			webBrowser.Navigate(F_CustomizedServerUri);
			while (!docCompleted)
			{
				Application.DoEvents();
			}
			links = webBrowser.Document.Links;
		}
		catch (Exception ex)
		{
			throw new Exception("錯誤，錯誤訊息如下：\n錯誤訊息：" + ex.Message);
		}
		foreach (HtmlElement link in links)
		{
			string line = link.InnerText;
			if (line == null)
			{
				continue;
			}
			Archnowledge.Common.DebugUtil.OutputDebugString(line);
			if (!(Path.GetExtension(line).ToUpper() == ".DOC"))
			{
				continue;
			}
			string[] data = Path.GetFileNameWithoutExtension(line).Split(' ');
			if (data.Length == 2)
			{
				string[] nameEdition = data[1].Split('-');
				if (nameEdition.Length == 2)
				{
					DataRow dr = F_CustomizedServerFile.NewRow();
					dr["FileName"] = Path.GetFileName(line);
					dr["ChapterNo"] = data[0];
					dr["ChapterName"] = nameEdition[0];
					dr["FileEdition"] = nameEdition[1];
					F_CustomizedServerFile.Rows.Add(dr);
				}
			}
		}
	}

	private void GetCustomizedDocListByFtp()
	{
		FtpWebRequest request = GetFtpWebRequest("NLST", string.Empty);
		try
		{
			using FtpWebResponse response = (FtpWebResponse)request.GetResponse();
			using StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
			for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
			{
				Archnowledge.Common.DebugUtil.OutputDebugString(line);
				if (Path.GetExtension(line).ToUpper() == ".DOC")
				{
					string[] data = Path.GetFileNameWithoutExtension(line).Split('_');
					if (data.Length == 3)
					{
						DataRow dr = F_CustomizedServerFile.NewRow();
						dr["FileName"] = Path.GetFileName(line);
						dr["ChapterNo"] = data[0];
						dr["ChapterName"] = data[1];
						dr["FileEdition"] = data[2];
						F_CustomizedServerFile.Rows.Add(dr);
					}
				}
			}
		}
		catch (WebException ex)
		{
			throw new Exception(string.Concat("施工規範下載server無法連線\n錯誤訊息：", ex.Message, "\n主機回應狀態：", ex.Status, "\n請稍候重試一次"));
		}
		catch (Exception ex2)
		{
			throw new Exception("錯誤，錯誤訊息如下：\n錯誤訊息：" + ex2.Message);
		}
		finally
		{
			request = null;
		}
	}

	private ExecResult GetCustomizedDoc(string StrCode, string sPath, out string allPath, out string edition, ref FileOverWriteOptions lastOption)
	{
		ExecResult ER = new ExecResult();
		DataView dv = F_CustomizedServerFile.DefaultView;
		dv.RowFilter = "ChapterNo='" + StrCode + "'";
		if (dv.Count == 0)
		{
			ER.ReturnCode = 1;
			ER.Message = "無此規範可以下載";
			allPath = string.Empty;
			edition = string.Empty;
			return ER;
		}
		string filename = dv[0]["FileName"].ToString();
		edition = dv[0]["FileEdition"].ToString();
		string chapterName = dv[0]["ChapterName"].ToString();
		allPath = $"{sPath}\\{StrCode}_{chapterName}_{F_Edition}.doc";
		if (CheckFileExists(StrCode, allPath, ref lastOption))
		{
			if (F_CustomizedServerUri.Scheme.ToUpper() == "HTTP")
			{
				return GetCustomizedDocByHttp(filename, allPath);
			}
			return GetCustomizedDocByFtp(filename, allPath);
		}
		ER.ReturnCode = 2;
		return ER;
	}

	private ExecResult GetCustomizedDocByHttp(string filename, string allPath)
	{
		ExecResult ER = new ExecResult();
		try
		{
			if (!F_CustomizedServerUri.AbsoluteUri.Trim().EndsWith("/"))
			{
				filename = "/" + filename;
			}
			WebRequest request = WebRequest.Create(new Uri(F_CustomizedServerUri.AbsoluteUri + filename));
			request.Method = "GET";
			request.Timeout = 300000;
			if (CommonMethods.GetIniValue("ProxyInfo", "usingProxy").Trim().ToLower() == "true")
			{
				request.Proxy = GetProxy();
			}
			using WebResponse response = request.GetResponse();
			WriteFile(allPath, response.GetResponseStream());
		}
		catch (Exception ex)
		{
			ER.ReturnCode = -1;
			ER.Message = ex.Message;
		}
		return ER;
	}

	private ExecResult GetCustomizedDocByFtp(string filename, string allPath)
	{
		ExecResult ER = new ExecResult();
		FtpWebRequest request = GetFtpWebRequest("RETR", filename);
		try
		{
			using FtpWebResponse response = (FtpWebResponse)request.GetResponse();
			WriteFile(allPath, response.GetResponseStream());
		}
		catch (Exception ex)
		{
			ER.ReturnCode = -1;
			ER.Message = ex.Message;
		}
		return ER;
	}

	private void WriteFile(string allPath, Stream responseStream)
	{
		using (responseStream)
		{
			using FileStream newFile = new FileStream(allPath, FileMode.Create);
			byte[] buffer = new byte[1024];
			for (int bytesRead = responseStream.Read(buffer, 0, 1024); bytesRead != 0; bytesRead = responseStream.Read(buffer, 0, 1024))
			{
				newFile.Write(buffer, 0, bytesRead);
			}
		}
	}

	private FtpWebRequest GetFtpWebRequest(string webRequestMethods, string ftpFileName)
	{
		if (!F_CustomizedServerUri.AbsoluteUri.Trim().EndsWith("/"))
		{
			ftpFileName = "/" + ftpFileName;
		}
		FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(F_CustomizedServerUri.AbsoluteUri + ftpFileName));
		request.Credentials = F_CustomizedServerCredential;
		request.Method = webRequestMethods;
		request.Timeout = 300000;
		request.ReadWriteTimeout = 300000;
		request.UsePassive = F_CustomizedServerUsePassive;
		request.KeepAlive = false;
		if (CommonMethods.GetIniValue("ProxyInfo", "usingProxy").Trim().ToLower() == "true")
		{
			request.Proxy = GetProxy();
		}
		return request;
	}

	private WebProxy GetProxy()
	{
		WebProxy myProxy = new WebProxy();
		string port = CommonMethods.GetIniValue("ProxyInfo", "port");
		string account = CommonMethods.GetIniValue("ProxyInfo", "account");
		string password = CommonMethods.GetIniValue("ProxyInfo", "password");
		string address = CommonMethods.GetIniValue("ProxyInfo", "address");
		myProxy.Address = new Uri(address + ":" + port);
		myProxy.Credentials = new NetworkCredential(account, password);
		return myProxy;
	}

	private string GetPccesMasterConnectionString()
	{
		string Path = AppDomain.CurrentDomain.BaseDirectory;
		ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
		fileMap.ExeConfigFilename = Path + "PccesMain.exe.config";
		Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
		return config.ConnectionStrings.ConnectionStrings["Pcces"].ConnectionString;
	}

	private DataTable ListItem(string strSQL, bool flag, string DatabaseName)
	{
		if (strSQL != "")
		{
			DataSet ds = SqlUtility.ExecDataSet(F_ConnectionString, strSQL);
			if (ds.Tables.Count > 0)
			{
				return ds.Tables[0];
			}
		}
		return null;
	}

	private void frmUser_Load(object sender, EventArgs e)
	{
		checkfirst = "start";
		CreateTable();
		Tab_A.Tab.Selected = true;
		BindToUserProject(F_UserID, F_DB, flag: true);
		DownloadDoc_Resize(null, null);
	}

	public int Str2Int(object ls_Number)
	{
		try
		{
			return Str2Int(ls_Number.ToString());
		}
		catch
		{
			return 0;
		}
	}

	private void BindToUserProject(string sUser, string DB, bool flag)
	{
		string sSQL = "SELECT DISTINCT a.projectCode, a.projectNameC, a.projectNameE, a.projectAddress FROM budproject a  JOIN ProjAuthority b on a.projectcode = b.projectcode  WHERE b.UserID = '" + sUser + "' and a.projectcode = '" + F_ProjectCode + "'";
		DataTable dtProject = ListItem(sSQL, flag, DB);
		if (dtProject != null)
		{
			gridProjectUsr.Rows.Count = dtProject.Rows.Count + 1;
			int i = 0;
			foreach (DataRow r in dtProject.Rows)
			{
				gridProjectUsr[i + 1, "ProjectCode"] = r["projectCode"].ToString().Trim();
				gridProjectUsr[i + 1, "CName"] = r["projectNameC"].ToString().Trim();
				gridProjectUsr[i + 1, "EName"] = r["projectNameE"].ToString().Trim();
				gridProjectUsr[i + 1, "Address"] = r["projectAddress"].ToString().Trim();
				gridProjectUsr[i + 1, "Selected"] = false;
			}
			ultraStatusBar3.Panels[0].Text = "資料筆數:" + dtProject.Rows.Count.ToString().Trim();
		}
		if (checkfirst == "start")
		{
			if (gridProjectUsr.Row > 0)
			{
				gridProjectUsr.Row = 1;
			}
			c1FlexGrid1_AfterRowColChange(null, null);
			checkfirst = "";
		}
	}

	private void GetAndBindData(bool flag, string DB)
	{
		string sSQL = "select distinct publicCode = CASE  WHEN SUBSTRING(pccesCode, 1, 1) = 'L' THEN SUBSTRING(pccesCode, 2, 5)  WHEN SUBSTRING(pccesCode, 1, 1) = 'E' THEN SUBSTRING(pccesCode, 2, 5)  WHEN SUBSTRING(pccesCode, 1, 1) = 'M' THEN SUBSTRING(pccesCode, 2, 5)  WHEN SUBSTRING(pccesCode, 1, 1) = 'W' THEN SUBSTRING(pccesCode, 2, 5)  ELSE SUBSTRING(pccesCode, 1, 5)  END  from budprojmrsA where projectCode = '" + sProjectCode + "' ";
		DT = ListItem(sSQL, flag: true, DB);
		if (flag)
		{
			BindToGrid(DB);
		}
	}

	private void BindToGrid(string DB)
	{
		Cursor = Cursors.WaitCursor;
		Grid1.Rows.Count = 1;
		CellStyle CSB = Grid1.Styles.Add("AdjustRed");
		CSB.BackColor = Color.Pink;
		CellStyle CSA = Grid1.Styles.Add("Adjustment");
		CSA.BackColor = Color.Gold;
		string sSQL = "Select * from AddOnDownLoad where projectCode = '" + sProjectCode + "'";
		DataTable DTDB = ListItem(sSQL, flag: true, DB);
		DataView dvDownloaded = new DataView(DTDB);
		DataView dvInUse = new DataView(DT);
		dvInUse.RowFilter = "publicCode not like 'Z%'";
		string codeString = string.Empty;
		for (int i = 0; i < dvInUse.Count; i++)
		{
			codeString = codeString + "'" + dvInUse[i]["publicCode"].ToString().Trim() + "',";
		}
		codeString = codeString.Substring(0, codeString.Length - 1);
		DataView dvCustomized = new DataView(F_CustomizedServerFile);
		dvCustomized.RowFilter = "ChapterNo in (" + codeString + ")";
		Archnowledge.Pcces.PccesMain.WSCode.WSCode ws = new Archnowledge.Pcces.PccesMain.WSCode.WSCode();
		ws.Url = "http://pcces.archnowledge.com/csinew/WSCode.asmx";
		ws.Url = "https://pcces.pcc.gov.tw/csinew/WSCode.asmx";
		DataSet ds = ws.GetChapterInfo(codeString);
		DataView dvPcc = new DataView(ds.Tables[0]);
		Grid1.Rows.Count = DT.Rows.Count + 1;
		lblName.Text = "工程代碼： " + sProjectCode + " ";
		for (int i = 0; i < dvInUse.Count; i++)
		{
			Grid1[i + 1, "projectCode"] = sProjectCode;
			Grid1[i + 1, "publicCode"] = dvInUse[i]["publicCode"].ToString().Trim();
			string text = (dvCustomized.RowFilter = "ChapterNo = '" + dvInUse[i]["publicCode"].ToString().Trim() + "'");
			text = (dvPcc.RowFilter = text);
			dvDownloaded.RowFilter = text;
			Grid1[i + 1, "DocPcc"] = dvPcc.Count > 0;
			Grid1[i + 1, "DocCustomized"] = dvCustomized.Count > 0;
			if (dvPcc.Count > 0)
			{
				Grid1[i + 1, "ChapterName"] = dvPcc[0]["ChapterName"].ToString().Trim();
			}
			else if (dvCustomized.Count > 0)
			{
				Grid1[i + 1, "ChapterName"] = dvCustomized[0]["ChapterName"].ToString().Trim();
			}
			else
			{
				Grid1.Rows[i + 1].Style = Grid1.Styles["AdjustRed"];
				Grid1[i + 1, "ChapterName"] = "";
			}
			if (dvDownloaded.Count > 0)
			{
				Grid1.Rows[i + 1].Style = Grid1.Styles["Adjustment"];
				Grid1[i + 1, "fileName"] = dvDownloaded[0]["TFileName"].ToString().Trim();
				Grid1[i + 1, "openDate"] = dvDownloaded[0]["OpenDate"].ToString().Trim();
				Grid1[i + 1, "version"] = dvDownloaded[0]["version"].ToString().Trim();
				Grid1[i + 1, "FileEdition"] = dvDownloaded[0]["FileEdition"].ToString().Trim();
			}
			else
			{
				Grid1[i + 1, "fileName"] = "";
				Grid1[i + 1, "openDate"] = "";
				Grid1[i + 1, "version"] = "";
				Grid1[i + 1, "FileEdition"] = "";
			}
		}
		Grid1.Row = iRecord;
		iRecordCount = dvInUse.Count + 1;
		ultraStatusBar1.Panels[0].Text = "資料筆數:" + dvInUse.Count.ToString().Trim();
		Cursor = Cursors.Default;
	}

	private void Do_DownLoad2(bool fromPcc)
	{
		if (Grid1.Row < 0)
		{
			return;
		}
		string sPath = CreateDirPath(sProjectCode, F_DB);
		bool writeDatabse = false;
		string allPath = string.Empty;
		string StrCode = string.Empty;
		string chapterName = string.Empty;
		Cursor = Cursors.WaitCursor;
		FileOverWriteOptions lastOption = FileOverWriteOptions.Yes;
		for (int i = 1; i < iRecordCount; i++)
		{
			if (!Grid1.Rows[i].Selected)
			{
				continue;
			}
			StrCode = Grid1[i, "publicCode"].ToString().Trim();
			chapterName = Grid1[i, "ChapterName"].ToString().Trim();
			iRecord = Grid1.Row;
			if (fromPcc)
			{
				try
				{
					Archnowledge.Pcces.PccesMain.WSCode.WSCode ws = new Archnowledge.Pcces.PccesMain.WSCode.WSCode();
					ws.Url = "http://pcces.archnowledge.com/csinew/WSCode.asmx";
					ws.Url = "https://pcces.pcc.gov.tw/csinew/WSCode.asmx";
					byte[] FileContent = new byte[0];
					F_Edition = ws.ReEdition(StrCode);
					allPath = $"{sPath}\\{StrCode}_{chapterName}_{F_Edition}.doc";
					FileContent = ws.ReDataDoc(StrCode);
					writeDatabse = FileContent.Length != 0 && WriteToFile(StrCode, allPath, ref FileContent, ref lastOption);
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, "呼叫服務發生錯誤，訊息如下：\n" + ex.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					writeDatabse = false;
				}
			}
			else
			{
				ExecResult ER = GetCustomizedDoc(StrCode, sPath, out allPath, out F_Edition, ref lastOption);
				writeDatabse = ER.ReturnCode == 0;
				if (ER.ReturnCode == -1)
				{
					MessageBox.Show(this, "下載檔案失敗，錯誤訊息如下：\n" + ER.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			if (writeDatabse)
			{
				string sSQL = "";
				sSQL = "Delete AddOnDownLoad where chapterNo = '" + StrCode + "'  and projectCode = '" + sProjectCode + "' and DBName = '" + F_DB + "'";
				SqlUtility.ExecSQL(F_ConnectionString, sSQL);
				sSQL = "Insert into AddOnDownLoad(chapterNo,TFileName,SaveDate,FileEdition,projectCode,DBName) Values  ('" + StrCode + "','" + Path.GetFileName(allPath) + "','" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + F_Edition + "','" + sProjectCode + "','" + F_DB + "')";
				SqlUtility.ExecSQL(F_ConnectionString, sSQL);
			}
		}
		Cursor = Cursors.Default;
	}

	private bool CheckFileExists(string chapterCode, string allPath, ref FileOverWriteOptions lastOption)
	{
		string existFileName = string.Empty;
		bool deleteExistFile = false;
		string[] filesInDirectory = Directory.GetFiles(Path.GetDirectoryName(allPath), "*.doc", SearchOption.TopDirectoryOnly);
		for (int i = 0; i < filesInDirectory.Length; i++)
		{
			string fileName = Path.GetFileName(filesInDirectory[i]);
			if (fileName.Split('_').Length == 3 && fileName.Split('_')[0] == chapterCode)
			{
				existFileName = filesInDirectory[i];
				break;
			}
		}
		if (existFileName == string.Empty)
		{
			return true;
		}
		if (lastOption == FileOverWriteOptions.NoToAll)
		{
			return false;
		}
		if (lastOption == FileOverWriteOptions.YesToAll)
		{
			deleteExistFile = true;
		}
		else
		{
			FormDownloadDocDialog dialog = new FormDownloadDocDialog(chapterCode, existFileName);
			switch (dialog.ShowDialog())
			{
			case DialogResult.Yes:
				lastOption = FileOverWriteOptions.Yes;
				break;
			case DialogResult.OK:
				lastOption = FileOverWriteOptions.YesToAll;
				break;
			case DialogResult.No:
				lastOption = FileOverWriteOptions.No;
				break;
			case DialogResult.Ignore:
				lastOption = FileOverWriteOptions.NoToAll;
				break;
			}
			deleteExistFile = ((lastOption == FileOverWriteOptions.Yes || lastOption == FileOverWriteOptions.YesToAll) ? true : false);
		}
		if (deleteExistFile)
		{
			try
			{
				File.Delete(existFileName);
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, "無法刪除" + existFileName + "\n，錯誤訊息如下：\n" + ex.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
		}
		return false;
	}

	private bool WriteToFile(string chapterCode, string strPath, ref byte[] Buffer, ref FileOverWriteOptions lastOption)
	{
		if (CheckFileExists(chapterCode, strPath, ref lastOption))
		{
			FileStream newFile = new FileStream(strPath, FileMode.Create);
			newFile.Write(Buffer, 0, Buffer.Length);
			newFile.Close();
			return true;
		}
		return false;
	}

	private void ShellExc(string sFileName)
	{
		try
		{
			ShellExecute SHExe = new ShellExecute();
			SHExe.OwnerHandle = base.Handle;
			SHExe.Path = sFileName;
			SHExe.Execute();
		}
		catch (Exception ex)
		{
			MessageBox.Show("檔案無法開啟\n\n" + ex.Message);
		}
	}

	private void c1FlexGrid1_AfterRowColChange(object sender, RangeEventArgs e)
	{
		if (IsAllSelect || gridProjectUsr.Row < 0)
		{
			return;
		}
		try
		{
			sProjectCode = gridProjectUsr[gridProjectUsr.Row, "projectCode"].ToString().Trim();
			GetAndBindData(flag: true, F_DB);
		}
		catch (Exception ex)
		{
			ultraStatusBar1.Panels[0].Text = "資料筆數:0";
			Console.Write(ex.Message);
		}
	}

	private void A_Btn_Next_Click(object sender, EventArgs e)
	{
		Tab_B.Tab.Selected = true;
	}

	private void A_Btn_Cncl_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		Do_MenuAction(e.Tool.Key);
	}

	private void Do_MenuAction(string KeyID)
	{
		switch (KeyID)
		{
		case "MnuDownLoad":
			Do_DownLoad2(fromPcc: true);
			sProjectCode = gridProjectUsr[gridProjectUsr.Row, "projectCode"].ToString().Trim();
			GetAndBindData(flag: true, F_DB);
			MessageBox.Show(this, "下載完畢!!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			break;
		case "MnuCustomizedDownloadSingleProject":
			Do_DownLoad2(fromPcc: false);
			sProjectCode = gridProjectUsr[gridProjectUsr.Row, "projectCode"].ToString().Trim();
			GetAndBindData(flag: true, F_DB);
			MessageBox.Show(this, "下載完畢!!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			break;
		case "MnuOpen":
			Do_Open();
			break;
		case "MnuAllDownLoad":
			Do_AllDownLoad(fromPcc: true);
			break;
		case "MnuCustomizedDownloadAllProject":
			Do_AllDownLoad(fromPcc: false);
			break;
		case "MnuAllSelect":
			IsAllSelect = true;
			Do_AllSelect();
			IsAllSelect = false;
			break;
		case "MnuSelect":
			Do_Select();
			break;
		}
	}

	private void CreateTable()
	{
		StringBuilder SB = new StringBuilder();
		try
		{
			if (!SqlUtility.isTableExist(F_ConnectionString, "AddOnDownLoad"))
			{
				SB.Append("\r\nCREATE TABLE [dbo].[AddOnDownLoad](\r\n\t[Sno] [int] IDENTITY(1,1) NOT NULL,\r\n\t[projectCode] [char](25) NOT NULL,\r\n\t[ChapterNo] [varchar](50) NOT NULL,\r\n\t[TFileName] [varchar](50) NULL,\r\n\t[SaveDate] [datetime] NULL,\r\n\t[OpenDate] [datetime] NULL,\r\n\t[DBName] [varchar](200) NULL,\r\n\t[FileEdition] [varchar](53) NULL,\r\n\t[version] [int] NULL\r\n) ON [PRIMARY]");
				SqlUtility.ExecSQL(F_ConnectionString, SB.ToString());
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(this, "建立資料表失敗 : " + ex.Message, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void Do_Open()
	{
		if (Grid1.Row >= 0)
		{
			string AllPath = "";
			string Sqlstr = "";
			int iVersion = 0;
			string sChapterNo = Grid1[Grid1.Row, "publicCode"].ToString().Trim();
			Sqlstr = "select * from AddOnDownLoad where  projectCode = '" + sProjectCode.Trim() + "' and DBName = '" + F_DB + "'  and ChapterNo = '" + sChapterNo + "'";
			DataTable DTBase = ListItem(Sqlstr, flag: true, F_DB);
			if (DTBase.Rows.Count > 0)
			{
				iVersion = ((!(DTBase.Rows[0]["version"].ToString() == "")) ? Convert.ToInt32(DTBase.Rows[0]["version"].ToString()) : 0);
				iVersion++;
			}
			string sPath = CreateDirPath(sProjectCode, F_DB);
			string sName = Grid1[Grid1.Row, "fileName"].ToString().Trim();
			AllPath = sPath + "\\" + sName;
			ShellExc(AllPath);
			string sSQL = "";
			sSQL = "Update AddOnDownLoad set OpenDate = '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "',version = " + iVersion + " where projectCode = '" + sProjectCode.Trim() + "' and DBName = '" + F_DB + "' and ChapterNo = '" + sChapterNo + "'";
			SqlUtility.ExecSQL(F_ConnectionString, sSQL);
			iRecord = Grid1.Row;
			c1FlexGrid1_AfterRowColChange(null, null);
		}
	}

	private void Grid1_MouseDown(object sender, MouseEventArgs e)
	{
		if (Grid1.Row >= 0)
		{
			if (Grid1[Grid1.Row, "fileName"].ToString().Trim() == "")
			{
				ultraToolbarsManager2.Tools["mnuOpen"].SharedProps.Enabled = false;
			}
			else
			{
				ultraToolbarsManager2.Tools["mnuOpen"].SharedProps.Enabled = true;
			}
			if (Grid1[Grid1.Row, "ChapterName"].ToString().Trim() == "")
			{
				ultraToolbarsManager2.Tools["MnuDownLoad"].SharedProps.Enabled = false;
			}
			else
			{
				ultraToolbarsManager2.Tools["MnuDownLoad"].SharedProps.Enabled = true;
			}
		}
	}

	private string CreateDirPath(string projectCode, string DB)
	{
		string AddOnPath = AppDomain.CurrentDomain.BaseDirectory + "AddOn\\" + DB + "\\" + projectCode;
		if (!Directory.Exists(AddOnPath))
		{
			Directory.CreateDirectory(AddOnPath);
		}
		return AddOnPath;
	}

	private void Do_AllDownLoad(bool fromPcc)
	{
		Tab_C.Tab.Selected = true;
		DataTable DTTemp = new DataTable();
		Application.DoEvents();
		bool writeDatabse = false;
		Prog1.Maximum = gridProjectUsr.Rows.Count;
		Prog1.Minimum = 0;
		Prog1.Value = 0;
		Cursor = Cursors.WaitCursor;
		FileOverWriteOptions lastOption = FileOverWriteOptions.Yes;
		for (int i = 1; i < gridProjectUsr.Rows.Count; i++)
		{
			Prog1.Value = i;
			Application.DoEvents();
			if (!gridProjectUsr.Rows[i].Selected)
			{
				continue;
			}
			string sPrjCode = gridProjectUsr[i, "projectCode"].ToString().Trim();
			string sPath = "";
			sPath = CreateDirPath(sPrjCode, F_DB);
			string sSQL = "select distinct publicCode = CASE  WHEN SUBSTRING(pccesCode, 1, 1) = 'L' THEN SUBSTRING(pccesCode, 2, 5)  WHEN SUBSTRING(pccesCode, 1, 1) = 'E' THEN SUBSTRING(pccesCode, 2, 5)  WHEN SUBSTRING(pccesCode, 1, 1) = 'M' THEN SUBSTRING(pccesCode, 2, 5)  WHEN SUBSTRING(pccesCode, 1, 1) = 'W' THEN SUBSTRING(pccesCode, 2, 5)  ELSE SUBSTRING(pccesCode, 1, 5)  END  from budprojmrsA where projectCode = '" + sPrjCode + "' ";
			DTTemp = ListItem(sSQL, flag: true, F_DB);
			DataView DV1 = new DataView(DTTemp);
			DV1.RowFilter = "publicCode not like 'Z%'";
			string codeString = string.Empty;
			for (int j = 0; j < DV1.Count; j++)
			{
				codeString = codeString + "'" + DV1[j]["publicCode"].ToString().Trim() + "',";
			}
			codeString = codeString.Substring(0, codeString.Length - 1);
			Archnowledge.Pcces.PccesMain.WSCode.WSCode ws = new Archnowledge.Pcces.PccesMain.WSCode.WSCode();
			ws.Url = "http://pcces.archnowledge.com/csinew/WSCode.asmx";
			ws.Url = "https://pcces.pcc.gov.tw/csinew/WSCode.asmx";
			DataSet ds = ws.GetChapterInfo(codeString);
			DataView dvPcc = new DataView(ds.Tables[0]);
			Prog2.Maximum = DV1.Count;
			Prog2.Minimum = 0;
			for (int j = 0; j < DV1.Count; j++)
			{
				string StrCode = string.Empty;
				string chapterName = string.Empty;
				string allPath = string.Empty;
				Prog2.Value = j;
				Application.DoEvents();
				StrCode = DV1[j]["publicCode"].ToString().Trim();
				if (fromPcc)
				{
					try
					{
						dvPcc.RowFilter = "ChapterNo = '" + StrCode + "'";
						if (dvPcc.Count == 0)
						{
							return;
						}
						chapterName = dvPcc[0]["ChapterName"].ToString().Trim();
						F_Edition = ws.ReEdition(StrCode);
						allPath = $"{sPath}\\{StrCode}_{chapterName}_{F_Edition}.doc";
						byte[] FileContent = new byte[0];
						FileContent = ws.ReDataDoc(StrCode);
						writeDatabse = FileContent.Length != 0 && WriteToFile(StrCode, allPath, ref FileContent, ref lastOption);
					}
					catch (Exception ex)
					{
						writeDatabse = false;
						Console.Write(ex.Message);
					}
				}
				else
				{
					ExecResult ER = GetCustomizedDoc(StrCode, sPath, out allPath, out F_Edition, ref lastOption);
					writeDatabse = ER.ReturnCode == 0;
					if (ER.ReturnCode == -1)
					{
						MessageBox.Show(this, "下載檔案失敗，錯誤訊息如下：\n" + ER.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
				if (writeDatabse)
				{
					sSQL = "Delete AddOnDownLoad where chapterNo = '" + StrCode + "'  and projectCode = '" + sPrjCode + "' and DBName = '" + F_DB + "'";
					SqlUtility.ExecSQL(F_ConnectionString, sSQL);
					sSQL = "Insert into AddOnDownLoad(chapterNo,TFileName,SaveDate,FileEdition,projectCode,DBName) Values  ('" + StrCode + "','" + Path.GetFileName(allPath) + "','" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + F_Edition + "','" + sPrjCode + "','" + F_DB + "')";
					SqlUtility.ExecSQL(F_ConnectionString, sSQL);
				}
			}
			DV1 = null;
		}
		Cursor = Cursors.Default;
		MessageBox.Show(this, "下載完畢!!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		Prog2.Value = 0;
		Prog1.Value = 0;
		Tab_B.Tab.Selected = true;
	}

	private void ultraButton6_Click(object sender, EventArgs e)
	{
		Tab_B.Tab.Selected = true;
	}

	private void Do_AllSelect()
	{
		for (int i = 0; i < gridProjectUsr.Rows.Count; i++)
		{
			gridProjectUsr.Rows[i].Selected = true;
		}
	}

	private void Do_Select()
	{
		for (int i = 0; i < Grid1.Rows.Count; i++)
		{
			Grid1.Rows[i].Selected = true;
		}
	}

	private void ultraButton8_Click(object sender, EventArgs e)
	{
		Tab_A.Tab.Selected = true;
	}

	private void DownloadDoc_Resize(object sender, EventArgs e)
	{
		int TotalH = base.Width;
		int iHeight = (TotalH - 5) * 2 / 5;
		panel6.Width = iHeight;
		gridProjectUsr.Cols[1].Width = (int)((double)gridProjectUsr.Width * 0.18);
		gridProjectUsr.Cols[2].Width = (int)((double)gridProjectUsr.Width * 0.1);
		gridProjectUsr.Cols[3].Width = (int)((double)gridProjectUsr.Width * 0.6);
		gridProjectUsr.Cols[4].Width = (int)((double)gridProjectUsr.Width * 0.1);
		Grid1.Cols[1].Width = (int)((double)Grid1.Width * 0.5);
		Grid1.Cols[2].Width = (int)((double)Grid1.Width * 0.42);
	}

	private void A_Btn_Next_Click_1(object sender, EventArgs e)
	{
		lblDBName.Text = "【" + F_DB.ToString() + "】";
		Tab_B.Tab.Selected = true;
	}

	private void ultraToolbarsManager_BeforeToolbarListDropdown(object sender, BeforeToolbarListDropdownEventArgs e)
	{
		e.Cancel = true;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormDownloadDoc));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Mu");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MnuAllSelect");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MnuAllDownLoad");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MnuCustomizedDownloadAllProject");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Menu");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MnuAllDownLoad");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MnuAllSelect");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MnuAllDownLoad");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MnuAllSelect");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MnuCustomizedDownloadAllProject");
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar2 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Menu");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MnuSelect");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool10 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MnuDownLoad");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool11 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MnuCustomizedDownloadSingleProject");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool12 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MnuOpen");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool2 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopupMenuTool1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool13 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MnuDownLoad");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool14 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MnuCustomizedDownloadSingleProject");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool15 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MnuOpen");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool16 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MnuDownLoad");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool17 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MnuOpen");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool18 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MnuSelect");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool19 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MnuCustomizedDownloadSingleProject");
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.label5 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.panel7 = new System.Windows.Forms.Panel();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel2_Fill_Panel = new System.Windows.Forms.Panel();
		this.gridProjectUsr = new C1.Win.C1FlexGrid.C1FlexGrid();
		this.ultraStatusBar3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this._panel2_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this._panel2_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._panel2_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._panel2_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.splitter2 = new System.Windows.Forms.Splitter();
		this.panel6 = new System.Windows.Forms.Panel();
		this.Grid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
		this.lblName = new Infragistics.Win.Misc.UltraLabel();
		this._panel6_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.ultraToolbarsManager2 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this._panel6_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._panel6_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._panel6_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.panel3 = new System.Windows.Forms.Panel();
		this.panel5 = new System.Windows.Forms.Panel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.label4 = new System.Windows.Forms.Label();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.label2 = new System.Windows.Forms.Label();
		this.ultraButton7 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton8 = new Infragistics.Win.Misc.UltraButton();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.lblDBName = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.Tab_C = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.Prog1 = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
		this.panel8 = new System.Windows.Forms.Panel();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.ultraButton5 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton6 = new Infragistics.Win.Misc.UltraButton();
		this.Prog2 = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
		this.panel9 = new System.Windows.Forms.Panel();
		this.panel15 = new System.Windows.Forms.Panel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this._DownloadDoc_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.Tab_A.SuspendLayout();
		this.panel7.SuspendLayout();
		this.Tab_B.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel2_Fill_Panel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridProjectUsr).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		this.panel6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Grid1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager2).BeginInit();
		this.panel1.SuspendLayout();
		this.panel4.SuspendLayout();
		this.Tab_C.SuspendLayout();
		this.panel8.SuspendLayout();
		this.panel9.SuspendLayout();
		this.panel15.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.label5);
		this.Tab_A.Controls.Add(this.label1);
		this.Tab_A.Controls.Add(this.panel7);
		this.Tab_A.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(792, 566);
		this.label5.Font = new System.Drawing.Font("新細明體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label5.ForeColor = System.Drawing.Color.Red;
		this.label5.Location = new System.Drawing.Point(176, 216);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(472, 104);
		this.label5.TabIndex = 13;
		this.label5.Text = "訊息：本人已充分瞭解本綱要規範尚非屬施工標準規範，應用本綱要規範時需就擬發包之工程特性，全面檢視本綱要規範之內容是否足可符合發包工程之需要，不致盲目照抄用。";
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label1.Font = new System.Drawing.Font("新細明體", 20f);
		this.label1.Location = new System.Drawing.Point(184, 112);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(448, 72);
		this.label1.TabIndex = 12;
		this.label1.Text = "歡迎使用預算書下載編碼相關規範!!";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.panel7.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel7.Controls.Add(this.groupBox3);
		this.panel7.Controls.Add(this.A_Btn_Cncl);
		this.panel7.Controls.Add(this.A_Btn_Next);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel7.Location = new System.Drawing.Point(0, 522);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(792, 44);
		this.panel7.TabIndex = 11;
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox3.Location = new System.Drawing.Point(0, 0);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(792, 8);
		this.groupBox3.TabIndex = 3;
		this.groupBox3.TabStop = false;
		this.A_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Cncl.Appearance = appearance1;
		this.A_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Cncl.Location = new System.Drawing.Point(696, 9);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.ShowFocusRect = false;
		this.A_Btn_Cncl.ShowOutline = false;
		this.A_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Cncl.SupportThemes = false;
		this.A_Btn_Cncl.TabIndex = 2;
		this.A_Btn_Cncl.Text = "關閉";
		this.A_Btn_Cncl.Click += new System.EventHandler(A_Btn_Cncl_Click);
		this.A_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Next.Appearance = appearance2;
		this.A_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Next.Location = new System.Drawing.Point(604, 9);
		this.A_Btn_Next.Name = "A_Btn_Next";
		this.A_Btn_Next.ShowFocusRect = false;
		this.A_Btn_Next.ShowOutline = false;
		this.A_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Next.SupportThemes = false;
		this.A_Btn_Next.TabIndex = 1;
		this.A_Btn_Next.Text = "下一步";
		this.A_Btn_Next.Click += new System.EventHandler(A_Btn_Next_Click_1);
		this.Tab_B.Controls.Add(this.panel2);
		this.Tab_B.Controls.Add(this.splitter2);
		this.Tab_B.Controls.Add(this.panel6);
		this.Tab_B.Controls.Add(this.panel3);
		this.Tab_B.Controls.Add(this.panel5);
		this.Tab_B.Controls.Add(this.panel1);
		this.Tab_B.Controls.Add(this.panel4);
		this.Tab_B.Location = new System.Drawing.Point(0, 0);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(792, 566);
		this.panel2.Controls.Add(this.panel2_Fill_Panel);
		this.panel2.Controls.Add(this._panel2_Toolbars_Dock_Area_Left);
		this.panel2.Controls.Add(this._panel2_Toolbars_Dock_Area_Right);
		this.panel2.Controls.Add(this._panel2_Toolbars_Dock_Area_Top);
		this.panel2.Controls.Add(this._panel2_Toolbars_Dock_Area_Bottom);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Font = new System.Drawing.Font("新細明體", 12f);
		this.panel2.Location = new System.Drawing.Point(24, 88);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(464, 430);
		this.panel2.TabIndex = 20;
		this.panel2_Fill_Panel.Controls.Add(this.gridProjectUsr);
		this.panel2_Fill_Panel.Controls.Add(this.ultraStatusBar3);
		this.panel2_Fill_Panel.Controls.Add(this.ultraLabel2);
		this.panel2_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
		this.panel2_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2_Fill_Panel.Font = new System.Drawing.Font("新細明體", 12f);
		this.panel2_Fill_Panel.Location = new System.Drawing.Point(0, 31);
		this.panel2_Fill_Panel.Name = "panel2_Fill_Panel";
		this.panel2_Fill_Panel.Size = new System.Drawing.Size(464, 399);
		this.panel2_Fill_Panel.TabIndex = 23;
		this.gridProjectUsr.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridProjectUsr.ColumnInfo = resources.GetString("gridProjectUsr.ColumnInfo");
		this.ultraToolbarsManager1.SetContextMenuUltra(this.gridProjectUsr, "Menu");
		this.gridProjectUsr.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridProjectUsr.ExtendLastCol = true;
		this.gridProjectUsr.Font = new System.Drawing.Font("新細明體", 12f);
		this.gridProjectUsr.ForeColor = System.Drawing.SystemColors.WindowText;
		this.gridProjectUsr.Location = new System.Drawing.Point(0, 30);
		this.gridProjectUsr.Name = "gridProjectUsr";
		this.gridProjectUsr.Rows.Count = 1;
		this.gridProjectUsr.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridProjectUsr.ShowCursor = true;
		this.gridProjectUsr.Size = new System.Drawing.Size(464, 337);
		this.gridProjectUsr.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridProjectUsr.Styles"));
		this.gridProjectUsr.TabIndex = 18;
		this.gridProjectUsr.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(c1FlexGrid1_AfterRowColChange);
		this.ultraStatusBar3.Location = new System.Drawing.Point(0, 367);
		this.ultraStatusBar3.Name = "ultraStatusBar3";
		ultraStatusPanel1.Text = "資料筆數：";
		ultraStatusPanel1.Width = 200;
		this.ultraStatusBar3.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[1] { ultraStatusPanel1 });
		this.ultraStatusBar3.Size = new System.Drawing.Size(464, 32);
		this.ultraStatusBar3.TabIndex = 19;
		this.ultraStatusBar3.Text = "ultraStatusBar3";
		appearance4.ForeColor = System.Drawing.Color.White;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel2.Appearance = appearance4;
		this.ultraLabel2.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel2.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel2.Font = new System.Drawing.Font("新細明體", 12f);
		this.ultraLabel2.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(464, 30);
		this.ultraLabel2.TabIndex = 15;
		this.ultraLabel2.Text = "專案列表";
		this._panel2_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._panel2_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(158, 190, 245);
		this._panel2_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._panel2_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._panel2_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 31);
		this._panel2_Toolbars_Dock_Area_Left.Name = "_panel2_Toolbars_Dock_Area_Left";
		this._panel2_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 399);
		this._panel2_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		appearance5.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance5.FontData.Name = "細明體";
		appearance5.FontData.SizeInPoints = 12f;
		this.ultraToolbarsManager1.Appearance = appearance5;
		this.ultraToolbarsManager1.DockWithinContainer = this.panel2;
		this.ultraToolbarsManager1.LockToolbars = true;
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager1.ShowQuickCustomizeButton = false;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.Text = "Menu";
		buttonTool2.InstanceProps.IsFirstInGroup = true;
		buttonTool3.InstanceProps.IsFirstInGroup = true;
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool1, buttonTool2, buttonTool3 });
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[1] { ultraToolbar1 });
		popupMenuTool1.SharedProps.Caption = "PopupMenuTool1";
		popupMenuTool1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool4, buttonTool5 });
		buttonTool6.SharedProps.Caption = "下載";
		buttonTool6.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool7.SharedProps.Caption = "全選";
		buttonTool7.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool8.SharedProps.Caption = "下載(處本部)";
		buttonTool8.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { popupMenuTool1, buttonTool6, buttonTool7, buttonTool8 });
		this.ultraToolbarsManager1.BeforeToolbarListDropdown += new Infragistics.Win.UltraWinToolbars.BeforeToolbarListDropdownEventHandler(ultraToolbarsManager_BeforeToolbarListDropdown);
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this._panel2_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._panel2_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(158, 190, 245);
		this._panel2_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._panel2_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._panel2_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(464, 31);
		this._panel2_Toolbars_Dock_Area_Right.Name = "_panel2_Toolbars_Dock_Area_Right";
		this._panel2_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 399);
		this._panel2_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this._panel2_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._panel2_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(158, 190, 245);
		this._panel2_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._panel2_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._panel2_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._panel2_Toolbars_Dock_Area_Top.Name = "_panel2_Toolbars_Dock_Area_Top";
		this._panel2_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(464, 31);
		this._panel2_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this._panel2_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._panel2_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(158, 190, 245);
		this._panel2_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._panel2_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._panel2_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 430);
		this._panel2_Toolbars_Dock_Area_Bottom.Name = "_panel2_Toolbars_Dock_Area_Bottom";
		this._panel2_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(464, 0);
		this._panel2_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
		this.splitter2.Location = new System.Drawing.Point(488, 88);
		this.splitter2.Name = "splitter2";
		this.splitter2.Size = new System.Drawing.Size(8, 430);
		this.splitter2.TabIndex = 21;
		this.splitter2.TabStop = false;
		this.panel6.Controls.Add(this.Grid1);
		this.panel6.Controls.Add(this.lblName);
		this.panel6.Controls.Add(this._panel6_Toolbars_Dock_Area_Left);
		this.panel6.Controls.Add(this._panel6_Toolbars_Dock_Area_Right);
		this.panel6.Controls.Add(this._panel6_Toolbars_Dock_Area_Top);
		this.panel6.Controls.Add(this._panel6_Toolbars_Dock_Area_Bottom);
		this.panel6.Controls.Add(this.ultraStatusBar1);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel6.Font = new System.Drawing.Font("新細明體", 12f);
		this.panel6.Location = new System.Drawing.Point(496, 88);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(280, 430);
		this.panel6.TabIndex = 19;
		this.Grid1.AllowEditing = false;
		this.Grid1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.Grid1.ColumnInfo = resources.GetString("Grid1.ColumnInfo");
		this.ultraToolbarsManager2.SetContextMenuUltra(this.Grid1, "PopupMenuTool1");
		this.Grid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Grid1.ExtendLastCol = true;
		this.Grid1.Font = new System.Drawing.Font("新細明體", 12f);
		this.Grid1.ForeColor = System.Drawing.SystemColors.WindowText;
		this.Grid1.Location = new System.Drawing.Point(0, 63);
		this.Grid1.Name = "Grid1";
		this.Grid1.Rows.Count = 1;
		this.Grid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.Grid1.ShowCursor = true;
		this.Grid1.Size = new System.Drawing.Size(280, 335);
		this.Grid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("Grid1.Styles"));
		this.Grid1.TabIndex = 0;
		this.Grid1.MouseDown += new System.Windows.Forms.MouseEventHandler(Grid1_MouseDown);
		appearance6.ForeColor = System.Drawing.Color.White;
		appearance6.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.lblName.Appearance = appearance6;
		this.lblName.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.lblName.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.lblName.Dock = System.Windows.Forms.DockStyle.Top;
		this.lblName.Font = new System.Drawing.Font("新細明體", 12f);
		this.lblName.Location = new System.Drawing.Point(0, 31);
		this.lblName.Name = "lblName";
		this.lblName.Size = new System.Drawing.Size(280, 32);
		this.lblName.TabIndex = 18;
		this.lblName.Text = "工程代碼";
		this._panel6_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._panel6_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(158, 190, 245);
		this._panel6_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._panel6_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._panel6_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 31);
		this._panel6_Toolbars_Dock_Area_Left.Name = "_panel6_Toolbars_Dock_Area_Left";
		this._panel6_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 367);
		this._panel6_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager2;
		appearance7.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance7.FontData.SizeInPoints = 12f;
		this.ultraToolbarsManager2.Appearance = appearance7;
		this.ultraToolbarsManager2.DockWithinContainer = this.panel6;
		this.ultraToolbarsManager2.LockToolbars = true;
		this.ultraToolbarsManager2.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager2.ShowQuickCustomizeButton = false;
		this.ultraToolbarsManager2.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar2.DockedColumn = 0;
		ultraToolbar2.DockedRow = 0;
		ultraToolbar2.Text = "Menu";
		buttonTool10.InstanceProps.IsFirstInGroup = true;
		buttonTool11.InstanceProps.IsFirstInGroup = true;
		buttonTool12.InstanceProps.IsFirstInGroup = true;
		ultraToolbar2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { buttonTool9, buttonTool10, buttonTool11, buttonTool12 });
		this.ultraToolbarsManager2.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[1] { ultraToolbar2 });
		popupMenuTool2.SharedProps.Caption = "PopupMenuTool1";
		popupMenuTool2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool13, buttonTool14, buttonTool15 });
		buttonTool16.SharedProps.Caption = "下載";
		buttonTool16.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool17.SharedProps.Caption = "開啟檔案";
		buttonTool17.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool17.SharedProps.Enabled = false;
		buttonTool18.SharedProps.Caption = "全選";
		buttonTool18.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool19.SharedProps.Caption = "下載(處本部)";
		buttonTool19.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		this.ultraToolbarsManager2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[5] { popupMenuTool2, buttonTool16, buttonTool17, buttonTool18, buttonTool19 });
		this.ultraToolbarsManager2.BeforeToolbarListDropdown += new Infragistics.Win.UltraWinToolbars.BeforeToolbarListDropdownEventHandler(ultraToolbarsManager_BeforeToolbarListDropdown);
		this.ultraToolbarsManager2.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this._panel6_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._panel6_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(158, 190, 245);
		this._panel6_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._panel6_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._panel6_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(280, 31);
		this._panel6_Toolbars_Dock_Area_Right.Name = "_panel6_Toolbars_Dock_Area_Right";
		this._panel6_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 367);
		this._panel6_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager2;
		this._panel6_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._panel6_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(158, 190, 245);
		this._panel6_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._panel6_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._panel6_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._panel6_Toolbars_Dock_Area_Top.Name = "_panel6_Toolbars_Dock_Area_Top";
		this._panel6_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(280, 31);
		this._panel6_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager2;
		this._panel6_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._panel6_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(158, 190, 245);
		this._panel6_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._panel6_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._panel6_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 398);
		this._panel6_Toolbars_Dock_Area_Bottom.Name = "_panel6_Toolbars_Dock_Area_Bottom";
		this._panel6_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(280, 0);
		this._panel6_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager2;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 398);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		ultraStatusPanel2.Text = "資料筆數：";
		ultraStatusPanel2.Width = 200;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[1] { ultraStatusPanel2 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(280, 32);
		this.ultraStatusBar1.TabIndex = 17;
		this.ultraStatusBar1.Text = "ultraStatusBar1";
		this.panel3.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel3.Location = new System.Drawing.Point(0, 88);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(24, 430);
		this.panel3.TabIndex = 11;
		this.panel5.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel5.Location = new System.Drawing.Point(776, 88);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(16, 430);
		this.panel5.TabIndex = 11;
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.Controls.Add(this.ultraLabel3);
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(792, 88);
		this.panel1.TabIndex = 9;
		appearance8.BackColor = System.Drawing.Color.White;
		this.ultraLabel3.Appearance = appearance8;
		this.ultraLabel3.Font = new System.Drawing.Font("新細明體", 12f);
		this.ultraLabel3.Location = new System.Drawing.Point(56, 36);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(664, 36);
		this.ultraLabel3.TabIndex = 6;
		this.ultraLabel3.Text = "在專案列表中選擇您所需要的專案編碼規範下載，可一次將整個專案相關的編碼文件下載 亦可在工程代碼表中只選擇您所需要的相關編碼文件下載";
		appearance9.BackColor = System.Drawing.Color.White;
		this.ultraLabel1.Appearance = appearance9;
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 12f, System.Drawing.FontStyle.Bold);
		this.ultraLabel1.Location = new System.Drawing.Point(24, 10);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel1.TabIndex = 4;
		this.ultraLabel1.Text = "請先挑選專案下載相關規範";
		this.panel4.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel4.Controls.Add(this.label4);
		this.panel4.Controls.Add(this.ultraLabel7);
		this.panel4.Controls.Add(this.ultraLabel4);
		this.panel4.Controls.Add(this.label2);
		this.panel4.Controls.Add(this.ultraButton7);
		this.panel4.Controls.Add(this.ultraButton8);
		this.panel4.Controls.Add(this.groupBox2);
		this.panel4.Controls.Add(this.lblDBName);
		this.panel4.Controls.Add(this.label3);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(0, 518);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(792, 48);
		this.panel4.TabIndex = 12;
		this.label4.Font = new System.Drawing.Font("新細明體", 12f);
		this.label4.ForeColor = System.Drawing.Color.Red;
		this.label4.Location = new System.Drawing.Point(432, 18);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(160, 16);
		this.label4.TabIndex = 21;
		this.label4.Text = "無此編碼的下載文件";
		this.ultraLabel7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.ultraLabel7.BackColor = System.Drawing.Color.Pink;
		this.ultraLabel7.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.Solid;
		this.ultraLabel7.Location = new System.Drawing.Point(400, 17);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(24, 20);
		this.ultraLabel7.TabIndex = 20;
		this.ultraLabel4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.ultraLabel4.BackColor = System.Drawing.Color.Gold;
		this.ultraLabel4.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.Solid;
		this.ultraLabel4.Location = new System.Drawing.Point(272, 17);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(24, 20);
		this.ultraLabel4.TabIndex = 19;
		this.label2.Font = new System.Drawing.Font("新細明體", 12f);
		this.label2.Location = new System.Drawing.Point(8, 16);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(56, 23);
		this.label2.TabIndex = 13;
		this.label2.Text = "資料庫：";
		this.ultraButton7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		appearance10.Image = resources.GetObject("appearance9.Image");
		appearance10.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton7.Appearance = appearance10;
		this.ultraButton7.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton7.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton7.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton7.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton7.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton7.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton7.Location = new System.Drawing.Point(696, 12);
		this.ultraButton7.Name = "ultraButton7";
		this.ultraButton7.ShowFocusRect = false;
		this.ultraButton7.ShowOutline = false;
		this.ultraButton7.Size = new System.Drawing.Size(88, 31);
		this.ultraButton7.SupportThemes = false;
		this.ultraButton7.TabIndex = 12;
		this.ultraButton7.Text = "關閉";
		this.ultraButton7.Click += new System.EventHandler(A_Btn_Cncl_Click);
		this.ultraButton8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		appearance11.Image = resources.GetObject("appearance10.Image");
		appearance11.ImageHAlign = Infragistics.Win.HAlign.Left;
		appearance11.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton8.Appearance = appearance11;
		this.ultraButton8.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton8.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton8.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton8.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton8.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton8.Location = new System.Drawing.Point(600, 13);
		this.ultraButton8.Name = "ultraButton8";
		this.ultraButton8.ShowFocusRect = false;
		this.ultraButton8.ShowOutline = false;
		this.ultraButton8.Size = new System.Drawing.Size(88, 31);
		this.ultraButton8.SupportThemes = false;
		this.ultraButton8.TabIndex = 11;
		this.ultraButton8.Text = "上一步";
		this.ultraButton8.Click += new System.EventHandler(ultraButton8_Click);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(792, 8);
		this.groupBox2.TabIndex = 10;
		this.groupBox2.TabStop = false;
		this.lblDBName.Font = new System.Drawing.Font("新細明體", 12f);
		this.lblDBName.Location = new System.Drawing.Point(67, 16);
		this.lblDBName.Name = "lblDBName";
		this.lblDBName.Size = new System.Drawing.Size(197, 23);
		this.lblDBName.TabIndex = 9;
		this.lblDBName.Text = "[lblDBName]";
		this.label3.Font = new System.Drawing.Font("新細明體", 12f);
		this.label3.ForeColor = System.Drawing.Color.Red;
		this.label3.Location = new System.Drawing.Point(304, 18);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(80, 16);
		this.label3.TabIndex = 9;
		this.label3.Text = "已下載過";
		this.Tab_C.Controls.Add(this.Prog1);
		this.Tab_C.Controls.Add(this.panel8);
		this.Tab_C.Controls.Add(this.Prog2);
		this.Tab_C.Controls.Add(this.panel9);
		this.Tab_C.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_C.Name = "Tab_C";
		this.Tab_C.Size = new System.Drawing.Size(792, 566);
		this.Prog1.Location = new System.Drawing.Point(136, 224);
		this.Prog1.Name = "Prog1";
		this.Prog1.Size = new System.Drawing.Size(520, 23);
		this.Prog1.Style = Infragistics.Win.UltraWinProgressBar.ProgressBarStyle.SegmentedPartial;
		this.Prog1.TabIndex = 32;
		this.Prog1.Text = "[Formatted]";
		this.panel8.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel8.Controls.Add(this.groupBox4);
		this.panel8.Controls.Add(this.ultraButton5);
		this.panel8.Controls.Add(this.ultraButton6);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel8.Location = new System.Drawing.Point(0, 518);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(792, 48);
		this.panel8.TabIndex = 13;
		this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox4.Location = new System.Drawing.Point(0, 0);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(792, 8);
		this.groupBox4.TabIndex = 10;
		this.groupBox4.TabStop = false;
		this.ultraButton5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance12.Image = resources.GetObject("appearance11.Image");
		appearance12.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton5.Appearance = appearance12;
		this.ultraButton5.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton5.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton5.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton5.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton5.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton5.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton5.Location = new System.Drawing.Point(688, 9);
		this.ultraButton5.Name = "ultraButton5";
		this.ultraButton5.ShowFocusRect = false;
		this.ultraButton5.ShowOutline = false;
		this.ultraButton5.Size = new System.Drawing.Size(88, 31);
		this.ultraButton5.SupportThemes = false;
		this.ultraButton5.TabIndex = 8;
		this.ultraButton5.Text = "關閉";
		this.ultraButton5.Visible = false;
		appearance13.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton6.Appearance = appearance13;
		this.ultraButton6.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton6.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton6.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton6.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton6.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton6.Location = new System.Drawing.Point(592, 9);
		this.ultraButton6.Name = "ultraButton6";
		this.ultraButton6.ShowFocusRect = false;
		this.ultraButton6.ShowOutline = false;
		this.ultraButton6.Size = new System.Drawing.Size(88, 31);
		this.ultraButton6.SupportThemes = false;
		this.ultraButton6.TabIndex = 8;
		this.ultraButton6.Text = "完成";
		this.ultraButton6.Visible = false;
		this.Prog2.Location = new System.Drawing.Point(136, 320);
		this.Prog2.Name = "Prog2";
		this.Prog2.Size = new System.Drawing.Size(520, 23);
		this.Prog2.Style = Infragistics.Win.UltraWinProgressBar.ProgressBarStyle.SegmentedPartial;
		this.Prog2.TabIndex = 32;
		this.Prog2.Text = "[Formatted]";
		this.panel9.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel9.Controls.Add(this.panel15);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel9.Location = new System.Drawing.Point(0, 0);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(792, 566);
		this.panel9.TabIndex = 33;
		this.panel15.BackColor = System.Drawing.Color.White;
		this.panel15.Controls.Add(this.ultraLabel5);
		this.panel15.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel15.Location = new System.Drawing.Point(0, 0);
		this.panel15.Name = "panel15";
		this.panel15.Size = new System.Drawing.Size(792, 88);
		this.panel15.TabIndex = 10;
		appearance14.BackColor = System.Drawing.Color.White;
		this.ultraLabel5.Appearance = appearance14;
		this.ultraLabel5.Font = new System.Drawing.Font("細明體", 12f, System.Drawing.FontStyle.Bold);
		this.ultraLabel5.Location = new System.Drawing.Point(24, 32);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel5.TabIndex = 4;
		this.ultraLabel5.Text = "下載中，請稍候…";
		this.Tab_Ctrl.BackColor = System.Drawing.Color.White;
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_A);
		this.Tab_Ctrl.Controls.Add(this.Tab_B);
		this.Tab_Ctrl.Controls.Add(this.Tab_C);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(792, 566);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabIndex = 2;
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "tab1";
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "tab2";
		ultraTab3.TabPage = this.Tab_C;
		ultraTab3.Text = "tab3";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[3] { ultraTab1, ultraTab2, ultraTab3 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(792, 566);
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Magenta;
		this.imageList2.Images.SetKeyName(0, "");
		this.imageList2.Images.SetKeyName(1, "");
		this._DownloadDoc_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._DownloadDoc_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(158, 190, 245);
		this._DownloadDoc_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._DownloadDoc_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._DownloadDoc_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 0);
		this._DownloadDoc_Toolbars_Dock_Area_Left.Name = "_DownloadDoc_Toolbars_Dock_Area_Left";
		this._DownloadDoc_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 566);
		this._DownloadDoc_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(792, 566);
		base.Controls.Add(this.Tab_Ctrl);
		base.Controls.Add(this._DownloadDoc_Toolbars_Dock_Area_Left);
		base.Name = "FormDownloadDoc";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "下載編碼相關規範程式";
		base.Load += new System.EventHandler(frmUser_Load);
		base.Resize += new System.EventHandler(DownloadDoc_Resize);
		this.Tab_A.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		this.Tab_B.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel2_Fill_Panel.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridProjectUsr).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
		this.panel6.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Grid1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager2).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.Tab_C.ResumeLayout(false);
		this.panel8.ResumeLayout(false);
		this.panel9.ResumeLayout(false);
		this.panel15.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).EndInit();
		this.Tab_Ctrl.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
