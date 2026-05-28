using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.PccesUpdateServices;
using Archnowledge.Pcces.PccesMain.ShellLib;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.ArchControls;

public class OnlineList : UserControl
{
	private bool hasRegistered = false;

	private string F_Flag = "";

	private int serverport;

	private NetworkStream ns;

	private StreamReader sr;

	private TcpClient clientsocket;

	private Thread receive = null;

	private string serveraddress;

	private string clientname;

	private bool connected = false;

	private string F_FunctionName = "";

	private string userID;

	private string userName = "";

	private string F_OnlineName = "";

	private string serverName = "localhost";

	private string F_TRY_Flag = "";

	private Panel panel1;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel3;

	private UltraButton btnSwitchUser;

	private ListBox lbChatters;

	private Panel panel2;

	private Container components = null;

	private UltraLabel lblUser;

	private Panel panel3;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel5;

	private UltraButton btnUpdate;

	private UltraLabel lblVer;

	private UltraLabel ultraLabel6;

	private UltraButton btnChangePassword;

	private UltraLabel lbRegistered;

	private UltraLabel lblDB;

	private Label label1;

	private PictureBox pictureBoxNewspaper;

	public bool _HasRegistered
	{
		get
		{
			return hasRegistered;
		}
		set
		{
			hasRegistered = value;
			if (!hasRegistered)
			{
				lbRegistered.Visible = true;
				return;
			}
			lbRegistered.Text = "【已註冊】";
			lbRegistered.Appearance.FontData.Underline = DefaultableBoolean.False;
			lbRegistered.Appearance.ForeColor = Color.Black;
			lbRegistered.Appearance.Cursor = Cursors.Default;
		}
	}

	public string _ServerName
	{
		get
		{
			return serverName;
		}
		set
		{
			serverName = value;
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

	public string _UserID
	{
		get
		{
			return userID;
		}
		set
		{
			userID = value;
			if (userID == "PccAdmin")
			{
				btnChangePassword.Visible = false;
				btnSwitchUser.Visible = false;
				userID = "Anonymous";
				lblUser.Text = "【" + userID + "】" + userName;
			}
			else
			{
				btnChangePassword.Visible = true;
				btnSwitchUser.Visible = true;
				lblUser.Text = "【" + userID + "】" + userName;
			}
		}
	}

	public string _UserName
	{
		get
		{
			return userName;
		}
		set
		{
			userName = value;
			if (userID == "PccAdmin")
			{
				userID = "Anonymous";
				lblUser.Text = "【" + userID + "】" + userName;
			}
			else
			{
				lblUser.Text = "【" + userID + "】" + userName;
			}
		}
	}

	public string _TRY_Flag
	{
		get
		{
			return F_TRY_Flag;
		}
		set
		{
			F_TRY_Flag = value;
		}
	}

	public OnlineList()
	{
		InitializeComponent();
		serverport = 9409;
		serveraddress = "localhost";
		lblVer.Text = "版本:【" + PccesVersion.PccesAssemblyVersion + "】";
	}

	public bool Connect()
	{
		bool RetV = true;
		serveraddress = serverName;
		F_OnlineName = "[PCC46_" + F_FunctionName + "]";
		clientname = "【" + userID + "】" + userName + F_OnlineName;
		if (!EstablishConnection())
		{
			RetV = false;
		}
		if (connected)
		{
			RegisterWithServer();
			receive = new Thread(ReceiveChat);
			receive.Start();
		}
		return RetV;
	}

	private void ReceiveChat()
	{
		bool keepalive = true;
		while (keepalive)
		{
			try
			{
				byte[] buffer = new byte[2048];
				ns.Read(buffer, 0, buffer.Length);
				string chatter = Encoding.UTF8.GetString(buffer);
				string[] tokens = chatter.Split('|');
				if (tokens[0] == "CHAT")
				{
				}
				if (tokens[0] == "PRIV")
				{
				}
				if (tokens[0] == "JOIN")
				{
					string newguy = tokens[1].Trim('\r', '\n', '\0');
					if (newguy.IndexOf(F_OnlineName) > -1)
					{
						newguy = newguy.Substring(0, newguy.IndexOf(F_OnlineName));
						lbChatters.Items.Add(newguy);
					}
				}
				if (tokens[0] == "GONE")
				{
					string WhoLeft = tokens[1].Trim('\r', '\n', '\0');
					if (WhoLeft.IndexOf(F_OnlineName) > -1)
					{
						lbChatters.Items.Remove(WhoLeft.Substring(0, WhoLeft.IndexOf(F_OnlineName)));
					}
				}
				if (tokens[0] == "QUIT")
				{
					ns.Close();
					clientsocket.Close();
					keepalive = false;
					string sWarn = "[線上使用者管理員] host:" + serverName + " 已經停止服務\n已無法繼續正確執行 Pcces Win 4.3 ，主程式即將關閉";
					if (!(base.ParentForm is FormSplash))
					{
						MessageBox.Show(this, sWarn + base.ParentForm.Name, "斷線", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					connected = false;
					TerminateSystem();
				}
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "ArchControls.OnlineList--> ReceiveChat()" + ex.Message);
			}
		}
	}

	private void RegisterWithServer()
	{
		try
		{
			string command = "CONN|" + clientname;
			byte[] outbytes = Encoding.UTF8.GetBytes(command.ToCharArray());
			ns.Write(outbytes, 0, outbytes.Length);
			string serverresponse = sr.ReadLine();
			serverresponse.Trim();
			string[] tokens = serverresponse.Split('|');
			if (tokens[0] == "LIST")
			{
			}
			string UserRealName = "";
			for (int n = 1; n < tokens.Length - 1; n++)
			{
				UserRealName = tokens[n].Trim('\r', '\n', '\0');
				if (UserRealName.IndexOf(F_OnlineName) > -1)
				{
					UserRealName = UserRealName.Substring(0, UserRealName.IndexOf(F_OnlineName));
					lbChatters.Items.Add(UserRealName);
				}
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "ArchControls.OnlineList--> RegisterWithServer()" + ex.Message);
			MessageBox.Show("登入錯誤", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private bool EstablishConnection()
	{
		bool RetV = true;
		string ChatServer = CommonMethods.GetIniValue("User", "ChatServer");
		if (ChatServer.ToUpper() == "FALSE")
		{
			return RetV;
		}
		try
		{
			clientsocket = new TcpClient(serveraddress, serverport);
			ns = clientsocket.GetStream();
			sr = new StreamReader(ns, Encoding.UTF8);
			connected = true;
		}
		catch
		{
			if (F_TRY_Flag == "" && F_Flag != "CLOSE")
			{
				MessageBox.Show("無法連結到 [多人共用連線伺服器]\n請確認[多人共用連線伺服器] " + serverName + " 已經啟動\n\n再重新執行[Pcces Win 4.3 ]", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			RetV = false;
		}
		return RetV;
	}

	private void QuitChat()
	{
		if (connected)
		{
			try
			{
				string command = "GONE|" + clientname;
				byte[] outbytes = Encoding.UTF8.GetBytes(command.ToCharArray());
				ns.Write(outbytes, 0, outbytes.Length);
				clientsocket.Close();
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "ArchControls.OnlineList--> QuitChat() " + ex.Message);
			}
		}
		if (receive != null && receive.IsAlive)
		{
			receive.Abort();
		}
	}

	public void Disconnect()
	{
		QuitChat();
		if (receive != null && receive.IsAlive)
		{
			receive.Abort();
		}
	}

	public void Disconnect(string FLAG)
	{
		F_Flag = FLAG;
		QuitChat();
		if (receive != null && receive.IsAlive)
		{
			receive.Abort();
		}
	}

	private void btnChangePassword_Click(object sender, EventArgs e)
	{
		FormChangeUserInfo FM_CHG_USR = new FormChangeUserInfo();
		FM_CHG_USR._UserID = userID;
		FM_CHG_USR.ShowDialog(this);
		FM_CHG_USR.Dispose();
		FM_CHG_USR = null;
	}

	private void btnSwitchUser_Click(object sender, EventArgs e)
	{
		string question = "切換使用者必須登出系統，要繼續執行？";
		if (MessageBox.Show(this, question, "使用者切換", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			TerminateSystem();
		}
	}

	private void TerminateSystem()
	{
		if (base.ParentForm is frmPccesMain)
		{
			ShellExecute SHExe = new ShellExecute();
			SHExe.OwnerHandle = base.Handle;
			SHExe.Path = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "PccesMain.exe";
			SHExe.Execute();
			SHExe = null;
			(base.ParentForm as frmPccesMain)._FORM_STATUS = "CLOSE";
			(base.ParentForm as frmPccesMain).Close();
		}
		else if (base.ParentForm.ParentForm is frmPccesMain)
		{
			ShellExecute SHExe = new ShellExecute();
			SHExe.OwnerHandle = base.Handle;
			SHExe.Path = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "PccesMain.exe";
			SHExe.Execute();
			SHExe = null;
			(base.ParentForm.ParentForm as frmPccesMain)._FORM_STATUS = "CLOSE";
			(base.ParentForm.ParentForm as frmPccesMain).Close();
		}
	}

	private void btnUpdate_Click(object sender, EventArgs e)
	{
		FormPccesUpdate formPccesUpdate = new FormPccesUpdate();
		DialogResult result = formPccesUpdate.ShowDialog();
		if (result == DialogResult.OK)
		{
			QuitChat();
			Process.Start(formPccesUpdate.getUpdateFileName());
			if (base.ParentForm is frmPccesMain)
			{
				(base.ParentForm as frmPccesMain).TerminatePCCES();
			}
			else if (base.ParentForm.ParentForm is frmPccesMain)
			{
				(base.ParentForm.ParentForm as frmPccesMain).TerminatePCCES();
			}
		}
	}

	private void OnlineList_Load(object sender, EventArgs e)
	{
		CorrectRatio();
		CheckDBServerSite();
	}

	private void CorrectRatio()
	{
		base.Width = 160;
		double ratio = CommonMethods.GetWindowRatio(base.ParentForm.Handle);
		if (ratio != 1.0)
		{
			ultraLabel6.Font = new Font(ultraLabel6.Font.Name, (float)((double)ultraLabel6.Font.Size * ratio));
			lblVer.Font = new Font(lblVer.Font.Name, (float)((double)lblVer.Font.Size * ratio));
			ultraLabel2.Font = new Font(ultraLabel2.Font.Name, (float)((double)ultraLabel2.Font.Size * ratio));
			lblUser.Font = new Font(lblUser.Font.Name, (float)((double)lblUser.Font.Size * ratio));
			ultraLabel3.Font = new Font(ultraLabel3.Font.Name, (float)((double)ultraLabel3.Font.Size * ratio));
			lbChatters.Font = new Font(lbChatters.Font.Name, (float)((double)lbChatters.Font.Size * ratio));
			btnUpdate.Appearance.FontData.SizeInPoints = (float)((double)btnUpdate.Font.Size * ratio);
			btnSwitchUser.Appearance.FontData.SizeInPoints = (float)((double)btnSwitchUser.Font.Size * ratio);
			btnChangePassword.Appearance.FontData.SizeInPoints = (float)((double)btnChangePassword.Font.Size * ratio);
			panel3.Size = new Size(150, (int)(22.0 * ratio));
			panel3.Top = (int)((double)panel3.Top * ratio);
			ultraLabel5.Size = new Size(150, (int)(49.0 * ratio));
			ultraLabel5.Top = panel3.Top + panel3.Height;
			panel1.Size = new Size(150, (int)(22.0 * ratio));
			panel1.Top = (int)((double)panel1.Top * ratio);
			lblUser.Top = panel1.Top + panel1.Height;
			panel2.Size = new Size(150, (int)(22.0 * ratio));
			panel2.Top = lblUser.Top + lblUser.Height + 10;
			lbChatters.Top = panel2.Top + panel2.Height + 2;
			ultraLabel1.Top = panel2.Top + panel2.Height;
			btnUpdate.Top = ultraLabel5.Top + ultraLabel5.Height - btnUpdate.Height;
			lblVer.Top = ultraLabel5.Top + 5;
			btnChangePassword.Top = lblUser.Top + lblUser.Height - btnChangePassword.Height;
		}
	}

	private void lbRegistered_Click(object sender, EventArgs e)
	{
		if (!hasRegistered)
		{
			FormRegister FM_REG = new FormRegister();
			if (FM_REG.ShowDialog() == DialogResult.OK)
			{
				hasRegistered = true;
				lbRegistered.Visible = false;
			}
			FM_REG.Dispose();
			FM_REG = null;
		}
	}

	private void CheckDBServerSite()
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
					lblDB.Text = "遠端資料庫";
					return;
				}
				break;
			case "localhost":
			case "(local)":
			case ".":
				break;
			}
		}
		lblDB.Text = "本機資料庫";
	}

	private void lblDB_Click(object sender, EventArgs e)
	{
		DBClass DBCLS = new DBClass();
		string serverName = DBCLS.GetDBConnectionServer().ToLower();
		switch (serverName)
		{
		default:
			if (!(serverName == "127.0.0.1"))
			{
				break;
			}
			goto case "localhost";
		case "localhost":
		case "(local)":
		case ".":
			serverName = "(local)";
			break;
		}
		MessageBox.Show("資料伺服器：【" + serverName.Trim() + "】 ", "資料庫位置訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
	}

	private void pictureBoxNewspaper_Click(object sender, EventArgs e)
	{
		string Year = (DateTime.Now.Year - 1911).ToString();
		string Month = DateTime.Now.Month.ToString().PadLeft(2, '0');
		string webAddress = "http://pcces.archnowledge.com/csi/Default.aspx?FunID=Fun_12_13";
		webAddress = "https://pcces.pcc.gov.tw/csi/Default.aspx?FunID=Fun_12_13";
		try
		{
			Process.Start(webAddress);
			AddNewspaperVisitorCount();
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

	private void AddNewspaperVisitorCount()
	{
		Update updateService = new Update();
		string webServiceRoute = CommonMethods.GetIniValue("DownloadInfo", "webServiceRoute");
		if (webServiceRoute == string.Empty)
		{
			webServiceRoute = "http://bisc.archnowledge.com/pccesupdateservices/Update.asmx";
		}
		updateService.Url = webServiceRoute;
		if (CommonMethods.GetIniValue("ProxyInfo", "usingProxy").Trim().ToLower() == "true")
		{
			updateService.Proxy = GetProxy();
		}
		try
		{
			updateService.AddNewspaperVisitorCount();
		}
		catch (Exception)
		{
		}
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

	private void pictureBox_MouseEnter(object sender, EventArgs e)
	{
		Cursor = Cursors.Hand;
	}

	private void pictureBox_MouseLeave(object sender, EventArgs e)
	{
		Cursor = Cursors.Default;
	}

	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.ArchControls.OnlineList));
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
		this.panel1 = new System.Windows.Forms.Panel();
		this.btnSwitchUser = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.lbChatters = new System.Windows.Forms.ListBox();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.lblUser = new Infragistics.Win.Misc.UltraLabel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.lblVer = new Infragistics.Win.Misc.UltraLabel();
		this.btnUpdate = new Infragistics.Win.Misc.UltraButton();
		this.btnChangePassword = new Infragistics.Win.Misc.UltraButton();
		this.lbRegistered = new Infragistics.Win.Misc.UltraLabel();
		this.lblDB = new Infragistics.Win.Misc.UltraLabel();
		this.pictureBoxNewspaper = new System.Windows.Forms.PictureBox();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBoxNewspaper).BeginInit();
		base.SuspendLayout();
		this.panel1.BackgroundImage = (System.Drawing.Image)resources.GetObject("panel1.BackgroundImage");
		this.panel1.Controls.Add(this.btnSwitchUser);
		this.panel1.Controls.Add(this.ultraLabel2);
		this.panel1.Location = new System.Drawing.Point(5, 84);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(150, 22);
		this.panel1.TabIndex = 0;
		appearance1.BackColor = System.Drawing.Color.FromArgb(147, 183, 215);
		appearance1.BackColor2 = System.Drawing.Color.FromArgb(147, 183, 215);
		appearance1.FontData.Name = "Arial";
		appearance1.FontData.SizeInPoints = 8f;
		this.btnSwitchUser.Appearance = appearance1;
		this.btnSwitchUser.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.btnSwitchUser.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnSwitchUser.Dock = System.Windows.Forms.DockStyle.Right;
		this.btnSwitchUser.Location = new System.Drawing.Point(106, 0);
		this.btnSwitchUser.Name = "btnSwitchUser";
		this.btnSwitchUser.ShowFocusRect = false;
		this.btnSwitchUser.ShowOutline = false;
		this.btnSwitchUser.Size = new System.Drawing.Size(44, 22);
		this.btnSwitchUser.SupportThemes = false;
		this.btnSwitchUser.TabIndex = 1;
		this.btnSwitchUser.Text = "切換";
		this.btnSwitchUser.Click += new System.EventHandler(btnSwitchUser_Click);
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel2.Appearance = appearance2;
		this.ultraLabel2.BackColor = System.Drawing.Color.Transparent;
		this.ultraLabel2.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel2.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(52, 22);
		this.ultraLabel2.TabIndex = 0;
		this.ultraLabel2.Text = "  使用者";
		this.lbChatters.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lbChatters.ItemHeight = 12;
		this.lbChatters.Location = new System.Drawing.Point(8, 185);
		this.lbChatters.Name = "lbChatters";
		this.lbChatters.Size = new System.Drawing.Size(144, 24);
		this.lbChatters.TabIndex = 2;
		appearance3.BorderColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel1.Appearance = appearance3;
		this.ultraLabel1.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.Solid;
		this.ultraLabel1.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.None;
		this.ultraLabel1.Location = new System.Drawing.Point(5, 182);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(150, 30);
		this.ultraLabel1.TabIndex = 1;
		this.panel2.BackgroundImage = (System.Drawing.Image)resources.GetObject("panel2.BackgroundImage");
		this.panel2.Controls.Add(this.ultraLabel3);
		this.panel2.Location = new System.Drawing.Point(5, 162);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(150, 22);
		this.panel2.TabIndex = 3;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel3.Appearance = appearance4;
		this.ultraLabel3.BackColor = System.Drawing.Color.Transparent;
		this.ultraLabel3.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel3.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(104, 22);
		this.ultraLabel3.TabIndex = 0;
		this.ultraLabel3.Text = "  線上使用者";
		appearance5.BorderColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance5.TextVAlign = Infragistics.Win.VAlign.Top;
		this.lblUser.Appearance = appearance5;
		this.lblUser.BackColor = System.Drawing.Color.White;
		this.lblUser.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.Solid;
		this.lblUser.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.None;
		this.lblUser.Location = new System.Drawing.Point(5, 104);
		this.lblUser.Name = "lblUser";
		this.lblUser.Padding = new System.Drawing.Size(0, 4);
		this.lblUser.Size = new System.Drawing.Size(150, 49);
		this.lblUser.TabIndex = 4;
		this.panel3.BackgroundImage = (System.Drawing.Image)resources.GetObject("panel3.BackgroundImage");
		this.panel3.Controls.Add(this.label1);
		this.panel3.Controls.Add(this.ultraLabel6);
		this.panel3.Controls.Add(this.ultraLabel4);
		this.panel3.Location = new System.Drawing.Point(5, 4);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(150, 22);
		this.panel3.TabIndex = 5;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.label1.ForeColor = System.Drawing.Color.Yellow;
		this.label1.Location = new System.Drawing.Point(32, 4);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(108, 16);
		this.label1.TabIndex = 13;
		appearance6.Image = resources.GetObject("appearance6.Image");
		appearance6.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel6.Appearance = appearance6;
		this.ultraLabel6.BackColor = System.Drawing.Color.Transparent;
		this.ultraLabel6.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel6.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.ultraLabel6.Location = new System.Drawing.Point(8, 0);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(84, 22);
		this.ultraLabel6.TabIndex = 1;
		this.ultraLabel6.Text = "  線上更新";
		appearance7.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel4.Appearance = appearance7;
		this.ultraLabel4.BackColor = System.Drawing.Color.Transparent;
		this.ultraLabel4.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel4.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.ultraLabel4.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(8, 22);
		this.ultraLabel4.TabIndex = 0;
		appearance8.BorderColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel5.Appearance = appearance8;
		this.ultraLabel5.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.Solid;
		this.ultraLabel5.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.None;
		this.ultraLabel5.Location = new System.Drawing.Point(5, 24);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(150, 49);
		this.ultraLabel5.TabIndex = 1;
		this.lblVer.Location = new System.Drawing.Point(10, 32);
		this.lblVer.Name = "lblVer";
		this.lblVer.Size = new System.Drawing.Size(138, 12);
		this.lblVer.TabIndex = 7;
		this.lblVer.Text = "版本:";
		appearance9.BackColor = System.Drawing.Color.FromArgb(147, 183, 215);
		appearance9.BackColor2 = System.Drawing.Color.FromArgb(147, 183, 215);
		appearance9.FontData.Name = "Arial";
		appearance9.FontData.SizeInPoints = 8f;
		this.btnUpdate.Appearance = appearance9;
		this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.btnUpdate.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnUpdate.Location = new System.Drawing.Point(84, 49);
		this.btnUpdate.Name = "btnUpdate";
		this.btnUpdate.ShowFocusRect = false;
		this.btnUpdate.ShowOutline = false;
		this.btnUpdate.Size = new System.Drawing.Size(68, 22);
		this.btnUpdate.SupportThemes = false;
		this.btnUpdate.TabIndex = 8;
		this.btnUpdate.Text = "線上更新";
		this.btnUpdate.Click += new System.EventHandler(btnUpdate_Click);
		appearance10.BackColor = System.Drawing.Color.FromArgb(147, 183, 215);
		appearance10.BackColor2 = System.Drawing.Color.FromArgb(147, 183, 215);
		appearance10.FontData.Name = "Arial";
		appearance10.FontData.SizeInPoints = 8f;
		this.btnChangePassword.Appearance = appearance10;
		this.btnChangePassword.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.btnChangePassword.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnChangePassword.Location = new System.Drawing.Point(84, 129);
		this.btnChangePassword.Name = "btnChangePassword";
		this.btnChangePassword.ShowFocusRect = false;
		this.btnChangePassword.ShowOutline = false;
		this.btnChangePassword.Size = new System.Drawing.Size(68, 22);
		this.btnChangePassword.SupportThemes = false;
		this.btnChangePassword.TabIndex = 9;
		this.btnChangePassword.Text = "變更密碼";
		this.btnChangePassword.Click += new System.EventHandler(btnChangePassword_Click);
		appearance11.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance11.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance11.ForeColor = System.Drawing.Color.Red;
		this.lbRegistered.Appearance = appearance11;
		this.lbRegistered.Location = new System.Drawing.Point(13, 55);
		this.lbRegistered.Name = "lbRegistered";
		this.lbRegistered.Size = new System.Drawing.Size(69, 12);
		this.lbRegistered.TabIndex = 10;
		this.lbRegistered.Text = "【未註冊】";
		this.lbRegistered.Click += new System.EventHandler(lbRegistered_Click);
		appearance12.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance12.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance12.ForeColor = System.Drawing.Color.Blue;
		this.lblDB.Appearance = appearance12;
		this.lblDB.BackColor = System.Drawing.Color.White;
		this.lblDB.Location = new System.Drawing.Point(13, 134);
		this.lblDB.Name = "lblDB";
		this.lblDB.Size = new System.Drawing.Size(69, 12);
		this.lblDB.TabIndex = 11;
		this.lblDB.Text = "本機資料庫";
		this.lblDB.Click += new System.EventHandler(lblDB_Click);
		this.pictureBoxNewspaper.Image = (System.Drawing.Image)resources.GetObject("pictureBoxNewspaper.Image");
		this.pictureBoxNewspaper.Location = new System.Drawing.Point(5, 218);
		this.pictureBoxNewspaper.Name = "pictureBoxNewspaper";
		this.pictureBoxNewspaper.Size = new System.Drawing.Size(150, 35);
		this.pictureBoxNewspaper.TabIndex = 12;
		this.pictureBoxNewspaper.TabStop = false;
		this.pictureBoxNewspaper.MouseLeave += new System.EventHandler(pictureBox_MouseLeave);
		this.pictureBoxNewspaper.Click += new System.EventHandler(pictureBoxNewspaper_Click);
		this.pictureBoxNewspaper.MouseEnter += new System.EventHandler(pictureBox_MouseEnter);
		this.AutoSize = true;
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.Controls.Add(this.pictureBoxNewspaper);
		base.Controls.Add(this.lblDB);
		base.Controls.Add(this.lbRegistered);
		base.Controls.Add(this.btnChangePassword);
		base.Controls.Add(this.btnUpdate);
		base.Controls.Add(this.lblVer);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.lbChatters);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.lblUser);
		base.Controls.Add(this.ultraLabel1);
		base.Controls.Add(this.ultraLabel5);
		base.Name = "OnlineList";
		base.Size = new System.Drawing.Size(160, 256);
		base.Load += new System.EventHandler(OnlineList_Load);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBoxNewspaper).EndInit();
		base.ResumeLayout(false);
	}

	protected override void Dispose(bool disposing)
	{
		try
		{
			QuitChat();
			if (receive != null && receive.IsAlive)
			{
				receive.Abort();
			}
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}
		catch
		{
		}
	}
}
