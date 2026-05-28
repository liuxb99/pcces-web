using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.PccesUpdateServices;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.SysMaintain;

public class FormSys_G1 : Form
{
	private const string CallFormHelp = "FormSys_G1";

	private Panel panel7;

	private UltraButton D_Btn_Fnsh;

	private GroupBox groupBox4;

	private UltraButton C_Btn_Cncl;

	private Panel panel6;

	private UltraLabel ultraLabel4;

	private Panel panel1;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel1;

	private UltraLabel lblDataBaseDesc;

	private UltraLabel ultraLabel2;

	private UltraComboEditor cboAutoNumDept;

	private UltraLabel ultraLabel5;

	private Container components = null;

	private string F_UserID;

	private string F_DataBaseDesc;

	private string F_DataBaseName;

	private string F_sSNO;

	private UltraLabel lblDataBaseName;

	private UltraButton ultraButton3;

	private UltraLabel ultraLabel6;

	private GroupBox groupBox1;

	private string F_DeptID = "";

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

	public string _DataBaseDesc
	{
		get
		{
			return F_DataBaseDesc;
		}
		set
		{
			F_DataBaseDesc = value;
		}
	}

	public string _DataBaseName
	{
		get
		{
			return F_DataBaseName;
		}
		set
		{
			F_DataBaseName = value;
		}
	}

	public FormSys_G1()
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.SysMaintain.FormSys_G1));
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
		this.panel7 = new System.Windows.Forms.Panel();
		this.D_Btn_Fnsh = new Infragistics.Win.Misc.UltraButton();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.C_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.panel6 = new System.Windows.Forms.Panel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton3 = new Infragistics.Win.Misc.UltraButton();
		this.cboAutoNumDept = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.lblDataBaseName = new Infragistics.Win.Misc.UltraLabel();
		this.lblDataBaseDesc = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.panel7.SuspendLayout();
		this.panel6.SuspendLayout();
		this.panel1.SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboAutoNumDept).BeginInit();
		base.SuspendLayout();
		this.panel7.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel7.Controls.Add(this.D_Btn_Fnsh);
		this.panel7.Controls.Add(this.groupBox4);
		this.panel7.Controls.Add(this.C_Btn_Cncl);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel7.Location = new System.Drawing.Point(0, 353);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(472, 44);
		this.panel7.TabIndex = 23;
		this.D_Btn_Fnsh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Fnsh.Appearance = appearance1;
		this.D_Btn_Fnsh.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Fnsh.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Fnsh.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Fnsh.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Fnsh.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Fnsh.Location = new System.Drawing.Point(286, 9);
		this.D_Btn_Fnsh.Name = "D_Btn_Fnsh";
		this.D_Btn_Fnsh.ShowFocusRect = false;
		this.D_Btn_Fnsh.ShowOutline = false;
		this.D_Btn_Fnsh.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Fnsh.SupportThemes = false;
		this.D_Btn_Fnsh.TabIndex = 4;
		this.D_Btn_Fnsh.Text = "確定";
		this.D_Btn_Fnsh.Click += new System.EventHandler(D_Btn_Fnsh_Click);
		this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox4.Location = new System.Drawing.Point(0, 0);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(472, 8);
		this.groupBox4.TabIndex = 3;
		this.groupBox4.TabStop = false;
		this.C_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.C_Btn_Cncl.Appearance = appearance2;
		this.C_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.C_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.C_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.C_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.C_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.C_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.C_Btn_Cncl.Location = new System.Drawing.Point(376, 9);
		this.C_Btn_Cncl.Name = "C_Btn_Cncl";
		this.C_Btn_Cncl.ShowFocusRect = false;
		this.C_Btn_Cncl.ShowOutline = false;
		this.C_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.C_Btn_Cncl.SupportThemes = false;
		this.C_Btn_Cncl.TabIndex = 2;
		this.C_Btn_Cncl.Text = "取消";
		this.panel6.BackColor = System.Drawing.Color.White;
		this.panel6.Controls.Add(this.panel1);
		this.panel6.Controls.Add(this.ultraLabel4);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel6.Location = new System.Drawing.Point(0, 0);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(472, 353);
		this.panel6.TabIndex = 25;
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.groupBox1);
		this.panel1.Controls.Add(this.ultraLabel6);
		this.panel1.Controls.Add(this.ultraButton3);
		this.panel1.Controls.Add(this.cboAutoNumDept);
		this.panel1.Controls.Add(this.ultraLabel2);
		this.panel1.Controls.Add(this.lblDataBaseName);
		this.panel1.Controls.Add(this.lblDataBaseDesc);
		this.panel1.Controls.Add(this.ultraLabel3);
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 41);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(472, 312);
		this.panel1.TabIndex = 3;
		this.groupBox1.Controls.Add(this.ultraLabel5);
		this.groupBox1.Location = new System.Drawing.Point(19, 179);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(437, 123);
		this.groupBox1.TabIndex = 15;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "注意事項";
		appearance3.ForeColor = System.Drawing.Color.Red;
		this.ultraLabel5.Appearance = appearance3;
		this.ultraLabel5.Location = new System.Drawing.Point(12, 32);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(408, 76);
		this.ultraLabel5.TabIndex = 12;
		this.ultraLabel5.Text = "自動編碼規則表，在自訂新碼後，會歸屬於一個機關代碼，將來使用者編出來的工作要項只會存入到現在所應對的資料庫中。因此在對應後按下確定鈕之後，就再也不能更改回來了，請注意操作!!";
		appearance4.FontData.SizeInPoints = 9f;
		appearance4.ForeColor = System.Drawing.Color.Green;
		this.ultraLabel6.Appearance = appearance4;
		this.ultraLabel6.Location = new System.Drawing.Point(32, 136);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(424, 40);
		this.ultraLabel6.TabIndex = 14;
		this.ultraLabel6.Text = "如果機關代碼下拉後，沒有找到你要的機關代碼，可以使用【線上更新】取得最新列表。";
		appearance5.FontData.SizeInPoints = 9f;
		this.ultraButton3.Appearance = appearance5;
		this.ultraButton3.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton3.Location = new System.Drawing.Point(379, 76);
		this.ultraButton3.Name = "ultraButton3";
		this.ultraButton3.ShowFocusRect = false;
		this.ultraButton3.ShowOutline = false;
		this.ultraButton3.Size = new System.Drawing.Size(75, 24);
		this.ultraButton3.TabIndex = 13;
		this.ultraButton3.Text = "線上更新";
		this.ultraButton3.Click += new System.EventHandler(ultraButton3_Click);
		appearance6.FontData.Name = "細明體";
		appearance6.FontData.SizeInPoints = 11f;
		this.cboAutoNumDept.Appearance = appearance6;
		this.cboAutoNumDept.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		this.cboAutoNumDept.Location = new System.Drawing.Point(32, 104);
		this.cboAutoNumDept.Name = "cboAutoNumDept";
		this.cboAutoNumDept.Size = new System.Drawing.Size(424, 24);
		this.cboAutoNumDept.TabIndex = 11;
		this.cboAutoNumDept.Text = null;
		appearance7.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance7;
		this.ultraLabel2.Location = new System.Drawing.Point(3, 75);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(240, 23);
		this.ultraLabel2.TabIndex = 10;
		this.ultraLabel2.Text = "資料庫對應自動編碼的所屬機關:";
		appearance8.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance8.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblDataBaseName.Appearance = appearance8;
		this.lblDataBaseName.Location = new System.Drawing.Point(128, 42);
		this.lblDataBaseName.Name = "lblDataBaseName";
		this.lblDataBaseName.Size = new System.Drawing.Size(328, 23);
		this.lblDataBaseName.TabIndex = 9;
		this.lblDataBaseName.Text = "[lblDataBaseName]";
		appearance9.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance9.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblDataBaseDesc.Appearance = appearance9;
		this.lblDataBaseDesc.Location = new System.Drawing.Point(128, 16);
		this.lblDataBaseDesc.Name = "lblDataBaseDesc";
		this.lblDataBaseDesc.Size = new System.Drawing.Size(328, 23);
		this.lblDataBaseDesc.TabIndex = 8;
		this.lblDataBaseDesc.Text = "[lblDataBaseDesc]";
		appearance10.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance10.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance10;
		this.ultraLabel3.Location = new System.Drawing.Point(8, 42);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(112, 23);
		this.ultraLabel3.TabIndex = 7;
		this.ultraLabel3.Text = "資料庫別名:";
		appearance11.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance11.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance11;
		this.ultraLabel1.Location = new System.Drawing.Point(8, 16);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(112, 23);
		this.ultraLabel1.TabIndex = 6;
		this.ultraLabel1.Text = "資料所屬機關:";
		appearance12.BackColor = System.Drawing.Color.White;
		this.ultraLabel4.Appearance = appearance12;
		this.ultraLabel4.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel4.Location = new System.Drawing.Point(16, 14);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel4.TabIndex = 2;
		this.ultraLabel4.Text = "對應設定";
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.ClientSize = new System.Drawing.Size(472, 397);
		base.Controls.Add(this.panel6);
		base.Controls.Add(this.panel7);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormSys_G1";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "資料庫對應的自動編碼";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormSys_G1_KeyDown);
		base.Load += new System.EventHandler(FormSys_G1_Load);
		this.panel7.ResumeLayout(false);
		this.panel6.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.cboAutoNumDept).EndInit();
		base.ResumeLayout(false);
	}

	private void FormSys_G1_Load(object sender, EventArgs e)
	{
		lblDataBaseDesc.Text = F_DataBaseDesc;
		lblDataBaseName.Text = F_DataBaseName;
		GetAutoNumDeptList();
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = "PccAdmin";
		F_sSNO = DBCLS.GetUserDefine_String("Select sno From UserDefind Where Kind='DataBase' And cString='" + F_DataBaseName + "' ", "sno");
		F_DeptID = DBCLS.GetUserDefine_String("Select cString From UserDefind Where Kind='DataAuto' And sno='" + F_sSNO + "' ", "cString");
		if (!(F_DeptID.Trim() != ""))
		{
			return;
		}
		cboAutoNumDept.Enabled = false;
		for (int i = 0; i < cboAutoNumDept.Items.Count; i++)
		{
			if (cboAutoNumDept.Items[i].DataValue.ToString().Trim() == F_DeptID)
			{
				cboAutoNumDept.SelectedIndex = i;
				break;
			}
		}
	}

	private void GetAutoNumDeptList()
	{
		cboAutoNumDept.Items.Clear();
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = "PccAdmin";
		DataTable DT_AutoNumC = DBCLS.GetUserDefine("Select * from AutoNumC Order By DeptID");
		if (DT_AutoNumC.Rows.Count > 0)
		{
			for (int i = 0; i < DT_AutoNumC.Rows.Count; i++)
			{
				ValueListItem LstItm = new ValueListItem();
				LstItm.DataValue = DT_AutoNumC.Rows[i]["DeptID"].ToString();
				LstItm.DisplayText = DT_AutoNumC.Rows[i]["DeptID"].ToString().Trim() + ":" + DT_AutoNumC.Rows[i]["DeptName"].ToString();
				cboAutoNumDept.Items.Add(LstItm);
			}
		}
	}

	private void D_Btn_Fnsh_Click(object sender, EventArgs e)
	{
		if (cboAutoNumDept.Text.Trim() == "")
		{
			base.DialogResult = DialogResult.Cancel;
			return;
		}
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = "PccAdmin";
		if (F_DeptID.Trim() == "")
		{
			DBCLS.ExecuteCommand("Insert Into UserDefind(Kind,cString,sno) values('DataAuto','" + cboAutoNumDept.SelectedItem.DataValue.ToString().Trim() + "'," + F_sSNO + ")");
		}
		base.DialogResult = DialogResult.OK;
	}

	private void ultraButton3_Click(object sender, EventArgs e)
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
		DataSet DS11 = serviceRequest.AutoNumC();
		DataTable DT1 = DS11.Tables[0].Copy();
		DBClass DBCLS = new DBClass();
		if (DBCLS.UpdateAutoNumC(DT1))
		{
			MessageBox.Show(this, "更新完畢!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			GetAutoNumDeptList();
		}
		else
		{
			MessageBox.Show(this, "更新失敗!\n請確認網路連結正常。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
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

	private void FormSys_G1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormSys_G1");
		}
	}
}
