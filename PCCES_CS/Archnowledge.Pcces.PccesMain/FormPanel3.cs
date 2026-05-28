using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.PccesMain.About;
using Archnowledge.Pcces.PccesMain.MrsBase;
using Archnowledge.Pcces.PccesMain.ShellLib;
using C1.Win.C1Sizer;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain;

public class FormPanel3 : Form
{
	private const string CallFormHelp = "FormPanel3";

	private string F_UserID;

	private IContainer components;

	private C1Sizer c1Sizer1;

	private UltraButton ultraButton2;

	private UltraButton ultraButton4;

	private UltraButton ultraButton6;

	private UltraButton ultraButton7;

	private UltraButton ultraButton9;

	private UltraButton ultraButton13;

	private UltraButton ultraButton14;

	private UltraButton ultraButton15;

	private UltraPictureBox ultraPictureBox3;

	private UltraPictureBox ultraPictureBox4;

	private UltraPictureBox ultraPictureBox5;

	private UltraPictureBox ultraPictureBox6;

	private UltraPictureBox ultraPictureBox7;

	private UltraLabel ultraLabel1;

	private UltraPictureBox ultraPictureBox8;

	private Panel panel1;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel3;

	private Timer timer1;

	private C1Sizer c1Sizer2;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel5;

	private UltraLabel lblTime;

	private UltraLabel lblDate;

	private LinkLabel linkLabel1;

	private LinkLabel linkLabel2;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel7;

	private UltraButton FuncBtn7;

	private UltraButton FuncBtn2;

	private UltraButton FuncBtn8;

	private UltraButton FuncBtn6;

	private UltraButton FuncBtn9;

	private UltraButton FuncBtn1;

	private UltraButton FuncBtn10;

	private UltraButton FuncBtn5;

	private UltraLabel ultraLabel8;

	private UltraLabel lblUseDatabase;

	private UltraPictureBox ultraPictureBox9;

	private UltraLabel ultraLabel13;

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

	public FormPanel3()
	{
		InitializeComponent();
	}

	private void FormPanel3_Resize(object sender, EventArgs e)
	{
		c1Sizer1.Grid.Rows[0].Size = 50;
		c1Sizer1.Grid.Rows[1].Size = base.Height * 15 / 573;
		c1Sizer1.Grid.Rows[2].Size = base.Height * 15 / 573;
		c1Sizer1.Grid.Rows[3].Size = base.Height * 123 / 573;
		c1Sizer1.Grid.Rows[4].Size = base.Height * 25 / 573;
		c1Sizer1.Grid.Rows[5].Size = base.Height * 50 / 573;
		c1Sizer1.Grid.Rows[6].Size = base.Height * 25 / 573;
		c1Sizer1.Grid.Rows[7].Size = base.Height * 50 / 573;
		c1Sizer1.Grid.Rows[8].Size = base.Height * 35 / 573;
		c1Sizer1.Grid.Rows[9].Size = base.Height * 50 / 573;
		c1Sizer1.Grid.Rows[10].Size = base.Height * 25 / 573;
		c1Sizer1.Grid.Rows[11].Size = base.Height * 50 / 573;
		c1Sizer1.Grid.Rows[12].Size = base.Height - c1Sizer1.Grid.Rows[0].Size - c1Sizer1.Grid.Rows[1].Size - c1Sizer1.Grid.Rows[2].Size - c1Sizer1.Grid.Rows[3].Size - c1Sizer1.Grid.Rows[4].Size - c1Sizer1.Grid.Rows[5].Size - c1Sizer1.Grid.Rows[6].Size - c1Sizer1.Grid.Rows[7].Size - c1Sizer1.Grid.Rows[8].Size - c1Sizer1.Grid.Rows[9].Size - c1Sizer1.Grid.Rows[10].Size - c1Sizer1.Grid.Rows[11].Size;
		if (base.Width < 700)
		{
			foreach (Control Ctrl in c1Sizer1.Controls)
			{
				if (Ctrl is UltraButton)
				{
					(Ctrl as UltraButton).Appearance.FontData.SizeInPoints = 9f;
				}
			}
		}
		else
		{
			foreach (Control Ctrl in c1Sizer1.Controls)
			{
				if (Ctrl is UltraButton)
				{
					(Ctrl as UltraButton).Appearance.FontData.SizeInPoints = 11f;
				}
			}
		}
		c1Sizer1.Refresh();
	}

	private void FormPanel3_Load(object sender, EventArgs e)
	{
		lblDate.Text = "中華民國" + (DateTime.Now.Year - 1911) + "年" + DateTime.Now.Month + "月" + DateTime.Now.Day + "日";
		lblTime.Text = "現在時間: " + DateTime.Now.ToLongTimeString();
		SysUser oSysUser = new SysUser();
		string DatabaseDesc = oSysUser.GetSysUserDatabaseDesc(F_UserID);
		if (DatabaseDesc.Trim() != "")
		{
			lblUseDatabase.Text = "目前資料庫:【" + DatabaseDesc.Trim() + "】";
			lblUseDatabase.Visible = true;
		}
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		lblTime.Text = DateTime.Now.ToLongTimeString();
	}

	private void ultraLabel4_Click(object sender, EventArgs e)
	{
	}

	private void FuncBtn10_Click(object sender, EventArgs e)
	{
		base.ParentForm.Close();
	}

	private void FuncBtn1_Click(object sender, EventArgs e)
	{
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc1_Click(this, EventArgs.Empty);
	}

	private void FuncBtn7_Click(object sender, EventArgs e)
	{
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc7_Click(this, EventArgs.Empty);
	}

	private void FuncBtn8_Click(object sender, EventArgs e)
	{
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc8_Click(this, EventArgs.Empty);
	}

	private void FuncBtn6_Click(object sender, EventArgs e)
	{
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc10_Click(this, EventArgs.Empty);
	}

	private void FuncBtn5_Click(object sender, EventArgs e)
	{
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc5_Click(this, EventArgs.Empty);
	}

	private void FuncBtn2_Click(object sender, EventArgs e)
	{
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc2_Click(this, EventArgs.Empty);
	}

	private void ultraButton13_Click(object sender, EventArgs e)
	{
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc3_Click(this, EventArgs.Empty);
	}

	private void ultraButton7_Click(object sender, EventArgs e)
	{
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc6_Click(this, EventArgs.Empty);
	}

	private void FuncBtn9_Click(object sender, EventArgs e)
	{
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc9_Click(this, EventArgs.Empty);
	}

	private void ultraButton6_Click(object sender, EventArgs e)
	{
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc4_Click(this, EventArgs.Empty);
	}

	private void ultraButton2_Click(object sender, EventArgs e)
	{
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc11_Click(this, EventArgs.Empty);
	}

	private void ultraLabel13_Click(object sender, EventArgs e)
	{
		FormPanelPick FM_PNL_PK = new FormPanelPick();
		FM_PNL_PK._OriginalHomeID = "3";
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
		if (sHomeID.Trim() != "3")
		{
			if (sHomeID == "1")
			{
				FormPanel FM_PNL1 = new FormPanel();
				FM_PNL1._UserID = F_UserID;
				FM_PNL1.MdiParent = base.ParentForm;
				FM_PNL1.Show();
			}
			if (sHomeID == "2")
			{
				FormPanel2 FM_PNL2 = new FormPanel2();
				FM_PNL2._UserID = F_UserID;
				FM_PNL2.MdiParent = base.ParentForm;
				FM_PNL2.Show();
			}
			Close();
		}
	}

	private void ultraButton14_Click(object sender, EventArgs e)
	{
		FormAbout FM_ABT = new FormAbout();
		FM_ABT.ShowDialog(this);
		FM_ABT.Close();
		FM_ABT.Dispose();
		FM_ABT = null;
	}

	private void ultraButton4_Click(object sender, EventArgs e)
	{
		if (File.Exists("C:\\QTS\\MENU.XLS"))
		{
			ShellExecute SHExe = new ShellExecute();
			SHExe.OwnerHandle = base.Handle;
			SHExe.Path = "C:\\QTS\\MENU.XLS";
			if (!SHExe.Execute())
			{
				MessageBox.Show(this, "你未安裝 Excel, 無法使用「工程數量計算」功能", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			SHExe = null;
		}
		else
		{
			MessageBox.Show(this, "C:\\QTS\\MENU.XLS，檔案不存在。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void linkLabel1_Click(object sender, EventArgs e)
	{
		ShellExecute SHExe = new ShellExecute();
		SHExe.OwnerHandle = base.Handle;
		SHExe.Path = "http://pcces.pcc.gov.tw/CSInew/Default.aspx?FunID=Fun_7&SearchType=H";
		SHExe.Execute();
	}

	private void linkLabel2_Click(object sender, EventArgs e)
	{
		ShellExecute SHExe = new ShellExecute();
		SHExe.OwnerHandle = base.Handle;
		SHExe.Path = "mailto:service@archnowledge.com";
		SHExe.Execute();
	}

	private void ultraButton15_Click(object sender, EventArgs e)
	{
		ShellExecute SHExe = new ShellExecute();
		SHExe.OwnerHandle = base.Handle;
		SHExe.Path = "http://pcces.pcc.gov.tw/csi/Default.aspx?FunID=Fun_7";
		SHExe.Execute();
	}

	private void ultraButton9_Click(object sender, EventArgs e)
	{
		FormConCost FM_COST = new FormConCost();
		FM_COST._UserID = F_UserID;
		FM_COST.Owner = this;
		FM_COST.ShowDialog();
		FM_COST.Close();
		FM_COST.Dispose();
		FM_COST = null;
	}

	private void FormPanel3_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormPanel3");
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.FormPanel3));
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
		this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.c1Sizer2 = new C1.Win.C1Sizer.C1Sizer();
		this.linkLabel1 = new System.Windows.Forms.LinkLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.lblTime = new Infragistics.Win.Misc.UltraLabel();
		this.lblDate = new Infragistics.Win.Misc.UltraLabel();
		this.linkLabel2 = new System.Windows.Forms.LinkLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.lblUseDatabase = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraPictureBox8 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.FuncBtn5 = new Infragistics.Win.Misc.UltraButton();
		this.FuncBtn7 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton4 = new Infragistics.Win.Misc.UltraButton();
		this.FuncBtn2 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton6 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton7 = new Infragistics.Win.Misc.UltraButton();
		this.FuncBtn8 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton9 = new Infragistics.Win.Misc.UltraButton();
		this.FuncBtn6 = new Infragistics.Win.Misc.UltraButton();
		this.FuncBtn9 = new Infragistics.Win.Misc.UltraButton();
		this.FuncBtn1 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton13 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton14 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton15 = new Infragistics.Win.Misc.UltraButton();
		this.FuncBtn10 = new Infragistics.Win.Misc.UltraButton();
		this.ultraPictureBox3 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.ultraPictureBox4 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.ultraPictureBox5 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.ultraPictureBox6 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.ultraPictureBox7 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.ultraPictureBox9 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		((System.ComponentModel.ISupportInitialize)this.c1Sizer1).BeginInit();
		this.c1Sizer1.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1Sizer2).BeginInit();
		this.c1Sizer2.SuspendLayout();
		base.SuspendLayout();
		this.c1Sizer1.AllowDrop = true;
		this.c1Sizer1.Controls.Add(this.ultraLabel13);
		this.c1Sizer1.Controls.Add(this.panel1);
		this.c1Sizer1.Controls.Add(this.ultraPictureBox8);
		this.c1Sizer1.Controls.Add(this.ultraLabel1);
		this.c1Sizer1.Controls.Add(this.FuncBtn5);
		this.c1Sizer1.Controls.Add(this.FuncBtn7);
		this.c1Sizer1.Controls.Add(this.ultraButton2);
		this.c1Sizer1.Controls.Add(this.ultraButton4);
		this.c1Sizer1.Controls.Add(this.FuncBtn2);
		this.c1Sizer1.Controls.Add(this.ultraButton6);
		this.c1Sizer1.Controls.Add(this.ultraButton7);
		this.c1Sizer1.Controls.Add(this.FuncBtn8);
		this.c1Sizer1.Controls.Add(this.ultraButton9);
		this.c1Sizer1.Controls.Add(this.FuncBtn6);
		this.c1Sizer1.Controls.Add(this.FuncBtn9);
		this.c1Sizer1.Controls.Add(this.FuncBtn1);
		this.c1Sizer1.Controls.Add(this.ultraButton13);
		this.c1Sizer1.Controls.Add(this.ultraButton14);
		this.c1Sizer1.Controls.Add(this.ultraButton15);
		this.c1Sizer1.Controls.Add(this.FuncBtn10);
		this.c1Sizer1.Controls.Add(this.ultraPictureBox3);
		this.c1Sizer1.Controls.Add(this.ultraPictureBox4);
		this.c1Sizer1.Controls.Add(this.ultraPictureBox5);
		this.c1Sizer1.Controls.Add(this.ultraPictureBox6);
		this.c1Sizer1.Controls.Add(this.ultraPictureBox7);
		this.c1Sizer1.Controls.Add(this.ultraPictureBox9);
		this.c1Sizer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1Sizer1.GridDefinition = resources.GetString("c1Sizer1.GridDefinition");
		this.c1Sizer1.Location = new System.Drawing.Point(0, 0);
		this.c1Sizer1.Name = "c1Sizer1";
		this.c1Sizer1.Size = new System.Drawing.Size(792, 573);
		this.c1Sizer1.TabIndex = 0;
		this.c1Sizer1.TabStop = false;
		appearance1.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance1.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance1.FontData.SizeInPoints = 11f;
		appearance1.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance1.ForeColor = System.Drawing.Color.White;
		appearance1.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel13.Appearance = appearance1;
		appearance2.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance2.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance2.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel13.HotTrackAppearance = appearance2;
		this.ultraLabel13.HotTracking = true;
		this.ultraLabel13.Location = new System.Drawing.Point(18, 538);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(155, 31);
		this.ultraLabel13.TabIndex = 26;
		this.ultraLabel13.Text = "面板切換";
		this.ultraLabel13.Click += new System.EventHandler(ultraLabel13_Click);
		this.panel1.BackColor = System.Drawing.Color.FromArgb(132, 0, 0);
		this.panel1.Controls.Add(this.c1Sizer2);
		this.panel1.Controls.Add(this.ultraLabel2);
		this.panel1.Controls.Add(this.ultraLabel3);
		this.panel1.Location = new System.Drawing.Point(419, 76);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(355, 160);
		this.panel1.TabIndex = 5;
		this.c1Sizer2.AllowDrop = true;
		this.c1Sizer2.Controls.Add(this.linkLabel1);
		this.c1Sizer2.Controls.Add(this.ultraLabel6);
		this.c1Sizer2.Controls.Add(this.ultraLabel5);
		this.c1Sizer2.Controls.Add(this.lblTime);
		this.c1Sizer2.Controls.Add(this.lblDate);
		this.c1Sizer2.Controls.Add(this.linkLabel2);
		this.c1Sizer2.Controls.Add(this.ultraLabel4);
		this.c1Sizer2.Controls.Add(this.ultraLabel7);
		this.c1Sizer2.Controls.Add(this.ultraLabel8);
		this.c1Sizer2.Controls.Add(this.lblUseDatabase);
		this.c1Sizer2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1Sizer2.GridDefinition = resources.GetString("c1Sizer2.GridDefinition");
		this.c1Sizer2.Location = new System.Drawing.Point(0, 0);
		this.c1Sizer2.Name = "c1Sizer2";
		this.c1Sizer2.Size = new System.Drawing.Size(355, 160);
		this.c1Sizer2.TabIndex = 2;
		this.c1Sizer2.TabStop = false;
		this.linkLabel1.Font = new System.Drawing.Font("細明體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.linkLabel1.Location = new System.Drawing.Point(117, 74);
		this.linkLabel1.Name = "linkLabel1";
		this.linkLabel1.Size = new System.Drawing.Size(234, 25);
		this.linkLabel1.TabIndex = 9;
		((System.Windows.Forms.Label)this.linkLabel1).TabStop = true;
		this.linkLabel1.Text = "pcces.archnowledge.com";
		this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.linkLabel1.Click += new System.EventHandler(linkLabel1_Click);
		appearance3.ForeColor = System.Drawing.Color.White;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel6.Appearance = appearance3;
		this.ultraLabel6.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(23, 103);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(90, 24);
		this.ultraLabel6.TabIndex = 8;
		this.ultraLabel6.Text = "電子信箱:";
		appearance4.ForeColor = System.Drawing.Color.White;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance4;
		this.ultraLabel5.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel5.Location = new System.Drawing.Point(23, 74);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(90, 25);
		this.ultraLabel5.TabIndex = 7;
		this.ultraLabel5.Text = "詢問網址:";
		appearance5.ForeColor = System.Drawing.Color.White;
		appearance5.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblTime.Appearance = appearance5;
		this.lblTime.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblTime.Location = new System.Drawing.Point(117, 45);
		this.lblTime.Name = "lblTime";
		this.lblTime.Size = new System.Drawing.Size(234, 25);
		this.lblTime.TabIndex = 6;
		this.lblTime.Text = "現在時間:";
		appearance6.ForeColor = System.Drawing.Color.White;
		appearance6.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblDate.Appearance = appearance6;
		this.lblDate.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblDate.Location = new System.Drawing.Point(117, 17);
		this.lblDate.Name = "lblDate";
		this.lblDate.Size = new System.Drawing.Size(234, 24);
		this.lblDate.TabIndex = 5;
		this.lblDate.Text = "今天是:";
		this.linkLabel2.Font = new System.Drawing.Font("細明體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.linkLabel2.Location = new System.Drawing.Point(117, 103);
		this.linkLabel2.Name = "linkLabel2";
		this.linkLabel2.Size = new System.Drawing.Size(234, 24);
		this.linkLabel2.TabIndex = 9;
		((System.Windows.Forms.Label)this.linkLabel2).TabStop = true;
		this.linkLabel2.Text = "service@archnowledge.com";
		this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.linkLabel2.Click += new System.EventHandler(linkLabel2_Click);
		appearance7.ForeColor = System.Drawing.Color.White;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance7;
		this.ultraLabel4.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel4.Location = new System.Drawing.Point(23, 17);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(208, 24);
		this.ultraLabel4.TabIndex = 5;
		this.ultraLabel4.Text = "今天是:";
		this.ultraLabel4.Click += new System.EventHandler(ultraLabel4_Click);
		appearance8.ForeColor = System.Drawing.Color.White;
		appearance8.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel7.Appearance = appearance8;
		this.ultraLabel7.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel7.Location = new System.Drawing.Point(23, 45);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(90, 25);
		this.ultraLabel7.TabIndex = 6;
		this.ultraLabel7.Text = "現在時間:";
		appearance9.ForeColor = System.Drawing.Color.White;
		appearance9.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel8.Appearance = appearance9;
		this.ultraLabel8.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel8.Location = new System.Drawing.Point(23, 131);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(90, 25);
		this.ultraLabel8.TabIndex = 8;
		this.ultraLabel8.Text = "目前資料庫:";
		appearance10.ForeColor = System.Drawing.Color.Yellow;
		appearance10.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblUseDatabase.Appearance = appearance10;
		this.lblUseDatabase.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblUseDatabase.Location = new System.Drawing.Point(117, 131);
		this.lblUseDatabase.Name = "lblUseDatabase";
		this.lblUseDatabase.Size = new System.Drawing.Size(234, 25);
		this.lblUseDatabase.TabIndex = 8;
		appearance11.BorderColor3DBase = System.Drawing.Color.White;
		this.ultraLabel2.Appearance = appearance11;
		this.ultraLabel2.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraLabel2.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(355, 160);
		this.ultraLabel2.TabIndex = 0;
		appearance12.ForeColor = System.Drawing.Color.White;
		appearance12.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance12;
		this.ultraLabel3.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel3.Location = new System.Drawing.Point(32, 44);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(60, 23);
		this.ultraLabel3.TabIndex = 1;
		this.ultraLabel3.Text = "今天是:";
		this.ultraPictureBox8.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox8.Image = resources.GetObject("ultraPictureBox8.Image");
		this.ultraPictureBox8.Location = new System.Drawing.Point(18, 76);
		this.ultraPictureBox8.MaintainAspectRatio = false;
		this.ultraPictureBox8.Name = "ultraPictureBox8";
		this.ultraPictureBox8.ScaleImage = Infragistics.Win.ScaleImage.Always;
		this.ultraPictureBox8.Size = new System.Drawing.Size(353, 160);
		this.ultraPictureBox8.TabIndex = 4;
		appearance13.BackColor = System.Drawing.Color.FromArgb(132, 0, 0);
		appearance13.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance13.FontData.Name = "標楷體";
		appearance13.FontData.SizeInPoints = 30f;
		appearance13.ForeColor = System.Drawing.Color.White;
		appearance13.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance13.TextVAlign = Infragistics.Win.VAlign.Top;
		this.ultraLabel1.Appearance = appearance13;
		this.ultraLabel1.Location = new System.Drawing.Point(4, 4);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(784, 50);
		this.ultraLabel1.TabIndex = 3;
		this.ultraLabel1.Text = "公共工程經費電腦估價系統";
		appearance14.ForeColor = System.Drawing.Color.Green;
		this.FuncBtn5.Appearance = appearance14;
		this.FuncBtn5.BackColor = System.Drawing.SystemColors.Control;
		this.FuncBtn5.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.FuncBtn5.Location = new System.Drawing.Point(221, 324);
		this.FuncBtn5.Name = "FuncBtn5";
		this.FuncBtn5.ShowFocusRect = false;
		this.FuncBtn5.ShowOutline = false;
		this.FuncBtn5.Size = new System.Drawing.Size(150, 50);
		this.FuncBtn5.SupportThemes = false;
		this.FuncBtn5.TabIndex = 1;
		this.FuncBtn5.Text = "專案目錄";
		this.FuncBtn5.Click += new System.EventHandler(FuncBtn5_Click);
		appearance15.ForeColor = System.Drawing.Color.Blue;
		this.FuncBtn7.Appearance = appearance15;
		this.FuncBtn7.BackColor = System.Drawing.SystemColors.Control;
		this.FuncBtn7.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.FuncBtn7.Location = new System.Drawing.Point(221, 484);
		this.FuncBtn7.Name = "FuncBtn7";
		this.FuncBtn7.ShowFocusRect = false;
		this.FuncBtn7.ShowOutline = false;
		this.FuncBtn7.Size = new System.Drawing.Size(150, 50);
		this.FuncBtn7.SupportThemes = false;
		this.FuncBtn7.TabIndex = 0;
		this.FuncBtn7.Text = "經費審查比對";
		this.FuncBtn7.Click += new System.EventHandler(FuncBtn7_Click);
		appearance16.ForeColor = System.Drawing.Color.Blue;
		this.ultraButton2.Appearance = appearance16;
		this.ultraButton2.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton2.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton2.Location = new System.Drawing.Point(18, 484);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(155, 50);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 0;
		this.ultraButton2.Text = "結算與決算";
		this.ultraButton2.Click += new System.EventHandler(ultraButton2_Click);
		appearance17.ForeColor = System.Drawing.Color.Blue;
		this.ultraButton4.Appearance = appearance17;
		this.ultraButton4.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton4.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton4.Location = new System.Drawing.Point(18, 259);
		this.ultraButton4.Name = "ultraButton4";
		this.ultraButton4.ShowFocusRect = false;
		this.ultraButton4.ShowOutline = false;
		this.ultraButton4.Size = new System.Drawing.Size(155, 50);
		this.ultraButton4.SupportThemes = false;
		this.ultraButton4.TabIndex = 1;
		this.ultraButton4.Text = "工程數量計算";
		this.ultraButton4.Click += new System.EventHandler(ultraButton4_Click);
		appearance18.ForeColor = System.Drawing.Color.Blue;
		this.FuncBtn2.Appearance = appearance18;
		this.FuncBtn2.BackColor = System.Drawing.SystemColors.Control;
		this.FuncBtn2.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.FuncBtn2.Location = new System.Drawing.Point(221, 259);
		this.FuncBtn2.Name = "FuncBtn2";
		this.FuncBtn2.ShowFocusRect = false;
		this.FuncBtn2.ShowOutline = false;
		this.FuncBtn2.Size = new System.Drawing.Size(150, 50);
		this.FuncBtn2.SupportThemes = false;
		this.FuncBtn2.TabIndex = 1;
		this.FuncBtn2.Text = "基本資料庫維護";
		this.FuncBtn2.Click += new System.EventHandler(FuncBtn2_Click);
		appearance19.ForeColor = System.Drawing.Color.Green;
		this.ultraButton6.Appearance = appearance19;
		this.ultraButton6.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton6.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton6.Location = new System.Drawing.Point(18, 324);
		this.ultraButton6.Name = "ultraButton6";
		this.ultraButton6.ShowFocusRect = false;
		this.ultraButton6.ShowOutline = false;
		this.ultraButton6.Size = new System.Drawing.Size(155, 50);
		this.ultraButton6.SupportThemes = false;
		this.ultraButton6.TabIndex = 1;
		this.ultraButton6.Text = "投標單填寫";
		this.ultraButton6.Click += new System.EventHandler(ultraButton6_Click);
		appearance20.ForeColor = System.Drawing.Color.FromArgb(132, 0, 0);
		this.ultraButton7.Appearance = appearance20;
		this.ultraButton7.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton7.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton7.Location = new System.Drawing.Point(18, 417);
		this.ultraButton7.Name = "ultraButton7";
		this.ultraButton7.ShowOutline = false;
		this.ultraButton7.Size = new System.Drawing.Size(155, 50);
		this.ultraButton7.SupportThemes = false;
		this.ultraButton7.TabIndex = 1;
		this.ultraButton7.Text = "契約變更";
		this.ultraButton7.Click += new System.EventHandler(ultraButton7_Click);
		appearance21.ForeColor = System.Drawing.Color.Blue;
		this.FuncBtn8.Appearance = appearance21;
		this.FuncBtn8.BackColor = System.Drawing.SystemColors.Control;
		this.FuncBtn8.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.FuncBtn8.Location = new System.Drawing.Point(419, 484);
		this.FuncBtn8.Name = "FuncBtn8";
		this.FuncBtn8.ShowFocusRect = false;
		this.FuncBtn8.ShowOutline = false;
		this.FuncBtn8.Size = new System.Drawing.Size(152, 50);
		this.FuncBtn8.SupportThemes = false;
		this.FuncBtn8.TabIndex = 1;
		this.FuncBtn8.Text = "歷史工程單位造價";
		this.FuncBtn8.Click += new System.EventHandler(FuncBtn8_Click);
		appearance22.ForeColor = System.Drawing.Color.Blue;
		this.ultraButton9.Appearance = appearance22;
		this.ultraButton9.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton9.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton9.Location = new System.Drawing.Point(419, 259);
		this.ultraButton9.Name = "ultraButton9";
		this.ultraButton9.ShowFocusRect = false;
		this.ultraButton9.ShowOutline = false;
		this.ultraButton9.Size = new System.Drawing.Size(152, 50);
		this.ultraButton9.SupportThemes = false;
		this.ultraButton9.TabIndex = 1;
		this.ultraButton9.Text = "營建物價";
		this.ultraButton9.Click += new System.EventHandler(ultraButton9_Click);
		appearance23.ForeColor = System.Drawing.Color.FromArgb(132, 0, 0);
		this.FuncBtn6.Appearance = appearance23;
		this.FuncBtn6.BackColor = System.Drawing.SystemColors.Control;
		this.FuncBtn6.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.FuncBtn6.Location = new System.Drawing.Point(221, 417);
		this.FuncBtn6.Name = "FuncBtn6";
		this.FuncBtn6.ShowFocusRect = false;
		this.FuncBtn6.ShowOutline = false;
		this.FuncBtn6.Size = new System.Drawing.Size(150, 50);
		this.FuncBtn6.SupportThemes = false;
		this.FuncBtn6.TabIndex = 1;
		this.FuncBtn6.Text = "估驗紀錄";
		this.FuncBtn6.Click += new System.EventHandler(FuncBtn6_Click);
		appearance24.ForeColor = System.Drawing.Color.FromArgb(132, 0, 0);
		this.FuncBtn9.Appearance = appearance24;
		this.FuncBtn9.BackColor = System.Drawing.SystemColors.Control;
		this.FuncBtn9.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.FuncBtn9.Location = new System.Drawing.Point(419, 417);
		this.FuncBtn9.Name = "FuncBtn9";
		this.FuncBtn9.ShowFocusRect = false;
		this.FuncBtn9.ShowOutline = false;
		this.FuncBtn9.Size = new System.Drawing.Size(152, 50);
		this.FuncBtn9.SupportThemes = false;
		this.FuncBtn9.TabIndex = 1;
		this.FuncBtn9.Text = "契約編製";
		this.FuncBtn9.Click += new System.EventHandler(FuncBtn9_Click);
		appearance25.ForeColor = System.Drawing.Color.Blue;
		this.FuncBtn1.Appearance = appearance25;
		this.FuncBtn1.BackColor = System.Drawing.SystemColors.Control;
		this.FuncBtn1.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.FuncBtn1.Location = new System.Drawing.Point(619, 259);
		this.FuncBtn1.Name = "FuncBtn1";
		this.FuncBtn1.ShowFocusRect = false;
		this.FuncBtn1.ShowOutline = false;
		this.FuncBtn1.Size = new System.Drawing.Size(155, 50);
		this.FuncBtn1.SupportThemes = false;
		this.FuncBtn1.TabIndex = 1;
		this.FuncBtn1.Text = "系統維護";
		this.FuncBtn1.Click += new System.EventHandler(FuncBtn1_Click);
		appearance26.ForeColor = System.Drawing.Color.Green;
		this.ultraButton13.Appearance = appearance26;
		this.ultraButton13.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton13.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton13.Location = new System.Drawing.Point(419, 324);
		this.ultraButton13.Name = "ultraButton13";
		this.ultraButton13.ShowFocusRect = false;
		this.ultraButton13.ShowOutline = false;
		this.ultraButton13.Size = new System.Drawing.Size(152, 50);
		this.ultraButton13.SupportThemes = false;
		this.ultraButton13.TabIndex = 1;
		this.ultraButton13.Text = "預算書編製";
		this.ultraButton13.Click += new System.EventHandler(ultraButton13_Click);
		appearance27.ForeColor = System.Drawing.Color.Red;
		this.ultraButton14.Appearance = appearance27;
		this.ultraButton14.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton14.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton14.Location = new System.Drawing.Point(619, 324);
		this.ultraButton14.Name = "ultraButton14";
		this.ultraButton14.ShowFocusRect = false;
		this.ultraButton14.ShowOutline = false;
		this.ultraButton14.Size = new System.Drawing.Size(155, 50);
		this.ultraButton14.SupportThemes = false;
		this.ultraButton14.TabIndex = 1;
		this.ultraButton14.Text = "關於本系統";
		this.ultraButton14.Click += new System.EventHandler(ultraButton14_Click);
		appearance28.ForeColor = System.Drawing.Color.Red;
		this.ultraButton15.Appearance = appearance28;
		this.ultraButton15.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton15.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton15.Location = new System.Drawing.Point(619, 417);
		this.ultraButton15.Name = "ultraButton15";
		this.ultraButton15.ShowFocusRect = false;
		this.ultraButton15.ShowOutline = false;
		this.ultraButton15.Size = new System.Drawing.Size(155, 50);
		this.ultraButton15.SupportThemes = false;
		this.ultraButton15.TabIndex = 1;
		this.ultraButton15.Text = "說明";
		this.ultraButton15.Click += new System.EventHandler(ultraButton15_Click);
		appearance29.ForeColor = System.Drawing.Color.Red;
		this.FuncBtn10.Appearance = appearance29;
		this.FuncBtn10.BackColor = System.Drawing.SystemColors.Control;
		this.FuncBtn10.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.FuncBtn10.Location = new System.Drawing.Point(619, 484);
		this.FuncBtn10.Name = "FuncBtn10";
		this.FuncBtn10.ShowFocusRect = false;
		this.FuncBtn10.ShowOutline = false;
		this.FuncBtn10.Size = new System.Drawing.Size(155, 50);
		this.FuncBtn10.SupportThemes = false;
		this.FuncBtn10.TabIndex = 1;
		this.FuncBtn10.Text = "離開";
		this.FuncBtn10.Click += new System.EventHandler(FuncBtn10_Click);
		this.ultraPictureBox3.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox3.Image = resources.GetObject("ultraPictureBox3.Image");
		this.ultraPictureBox3.Location = new System.Drawing.Point(375, 324);
		this.ultraPictureBox3.Name = "ultraPictureBox3";
		this.ultraPictureBox3.Size = new System.Drawing.Size(40, 50);
		this.ultraPictureBox3.TabIndex = 2;
		this.ultraPictureBox4.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox4.Image = resources.GetObject("ultraPictureBox4.Image");
		this.ultraPictureBox4.Location = new System.Drawing.Point(375, 417);
		this.ultraPictureBox4.Name = "ultraPictureBox4";
		this.ultraPictureBox4.Size = new System.Drawing.Size(40, 50);
		this.ultraPictureBox4.TabIndex = 2;
		this.ultraPictureBox5.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox5.Image = resources.GetObject("ultraPictureBox5.Image");
		this.ultraPictureBox5.Location = new System.Drawing.Point(375, 259);
		this.ultraPictureBox5.Name = "ultraPictureBox5";
		this.ultraPictureBox5.Size = new System.Drawing.Size(40, 50);
		this.ultraPictureBox5.TabIndex = 2;
		this.ultraPictureBox6.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox6.Image = resources.GetObject("ultraPictureBox6.Image");
		this.ultraPictureBox6.Location = new System.Drawing.Point(177, 417);
		this.ultraPictureBox6.Name = "ultraPictureBox6";
		this.ultraPictureBox6.Size = new System.Drawing.Size(40, 50);
		this.ultraPictureBox6.TabIndex = 2;
		this.ultraPictureBox7.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox7.Image = resources.GetObject("ultraPictureBox7.Image");
		this.ultraPictureBox7.Location = new System.Drawing.Point(419, 378);
		this.ultraPictureBox7.Name = "ultraPictureBox7";
		this.ultraPictureBox7.Size = new System.Drawing.Size(152, 35);
		this.ultraPictureBox7.TabIndex = 2;
		this.ultraPictureBox9.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox9.Image = resources.GetObject("ultraPictureBox9.Image");
		this.ultraPictureBox9.Location = new System.Drawing.Point(177, 324);
		this.ultraPictureBox9.Name = "ultraPictureBox9";
		this.ultraPictureBox9.Size = new System.Drawing.Size(40, 50);
		this.ultraPictureBox9.TabIndex = 2;
		this.timer1.Enabled = true;
		this.timer1.Interval = 1000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		this.BackColor = System.Drawing.Color.Navy;
		base.ClientSize = new System.Drawing.Size(792, 573);
		base.Controls.Add(this.c1Sizer1);
		base.KeyPreview = true;
		base.Name = "FormPanel3";
		this.Text = "FormPanel3";
		base.Load += new System.EventHandler(FormPanel3_Load);
		base.Resize += new System.EventHandler(FormPanel3_Resize);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormPanel3_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.c1Sizer1).EndInit();
		this.c1Sizer1.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1Sizer2).EndInit();
		this.c1Sizer2.ResumeLayout(false);
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
