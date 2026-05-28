using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.General;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain;

public class FormPanel : Form
{
	private const string CallFormHelp = "FormPanel";

	private string F_UserID;

	private Container components = null;

	private Panel panel1;

	private PictureBox pictureBox1;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel8;

	private UltraLabel ultraLabel9;

	private UltraLabel ultraLabel11;

	private UltraButton Btn8;

	private UltraButton Btn7;

	private UltraButton Btn11;

	private UltraButton Btn10;

	private UltraButton Btn5;

	private UltraButton Btn4;

	private UltraButton Btn3;

	private UltraButton Btn2;

	private UltraButton Btn1;

	private UltraLabel ultraLabel10;

	private UltraLabel ultraLabel13;

	private UltraLabel lblUseDatabase;

	private UltraButton Btn13;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel12;

	private UltraButton Btn14;

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

	public FormPanel()
	{
		InitializeComponent();
	}

	private void FormPanel_Resize(object sender, EventArgs e)
	{
		panel1.Left = FindForm().Width / 2 - panel1.Width / 2;
		panel1.Top = FindForm().Height / 2 - panel1.Height / 2;
	}

	private void Btn1_Click(object sender, EventArgs e)
	{
		panel1.Enabled = false;
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc5_Click(this, EventArgs.Empty);
		panel1.Enabled = true;
	}

	private void Btn2_Click(object sender, EventArgs e)
	{
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc3_Click(this, EventArgs.Empty);
	}

	private void Btn3_Click(object sender, EventArgs e)
	{
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc4_Click(this, EventArgs.Empty);
	}

	private void Btn4_Click(object sender, EventArgs e)
	{
		panel1.Enabled = false;
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc2_Click(this, EventArgs.Empty);
		panel1.Enabled = true;
	}

	private void Btn5_Click(object sender, EventArgs e)
	{
		panel1.Enabled = false;
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc1_Click(this, EventArgs.Empty);
		panel1.Enabled = true;
	}

	private void Btn6_Click(object sender, EventArgs e)
	{
		panel1.Enabled = false;
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc6_Click(this, EventArgs.Empty);
		panel1.Enabled = true;
	}

	private void Btn10_Click(object sender, EventArgs e)
	{
		panel1.Enabled = false;
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc7_Click(this, EventArgs.Empty);
		panel1.Enabled = true;
	}

	private void Btn11_Click(object sender, EventArgs e)
	{
		panel1.Enabled = false;
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc8_Click(this, EventArgs.Empty);
		panel1.Enabled = true;
	}

	private void Btn7_Click(object sender, EventArgs e)
	{
		panel1.Enabled = false;
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc9_Click(this, EventArgs.Empty);
		panel1.Enabled = true;
	}

	private void BtnSwitch_Click(object sender, EventArgs e)
	{
		FormPanelPick FM_PNL_PK = new FormPanelPick();
		FM_PNL_PK._OriginalHomeID = "1";
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
		if (sHomeID.Trim() != "1")
		{
			if (sHomeID == "2")
			{
				FormPanel2 FM_PNL2 = new FormPanel2();
				FM_PNL2._UserID = F_UserID;
				FM_PNL2.MdiParent = base.ParentForm;
				FM_PNL2.Show();
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

	private void FormPanel_Load(object sender, EventArgs e)
	{
		SysUser oSysUser = new SysUser();
		string DatabaseDesc = oSysUser.GetSysUserDatabaseDesc(F_UserID);
		if (DatabaseDesc.Trim() != "")
		{
			lblUseDatabase.Text = "目前資料庫:【" + DatabaseDesc.Trim() + "】";
			lblUseDatabase.Visible = true;
		}
	}

	private void Btn13_Click(object sender, EventArgs e)
	{
		panel1.Enabled = false;
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc10_Click(this, EventArgs.Empty);
		panel1.Enabled = true;
	}

	private void Btn14_Click(object sender, EventArgs e)
	{
		panel1.Enabled = false;
		(base.ParentForm as frmPccesMain).functionButtons1.BtnFunc11_Click(this, EventArgs.Empty);
		panel1.Enabled = true;
	}

	private void FormPanel_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormPanel");
		}
	}

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.FormPanel));
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
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.Btn14 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.Btn13 = new Infragistics.Win.Misc.UltraButton();
		this.lblUseDatabase = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
		this.Btn8 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.Btn7 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.Btn11 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.Btn10 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.Btn5 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.Btn4 = new Infragistics.Win.Misc.UltraButton();
		this.Btn3 = new Infragistics.Win.Misc.UltraButton();
		this.Btn2 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.Btn1 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.ultraLabel12);
		this.panel1.Controls.Add(this.Btn14);
		this.panel1.Controls.Add(this.ultraLabel7);
		this.panel1.Controls.Add(this.Btn13);
		this.panel1.Controls.Add(this.lblUseDatabase);
		this.panel1.Controls.Add(this.ultraLabel13);
		this.panel1.Controls.Add(this.ultraLabel11);
		this.panel1.Controls.Add(this.Btn8);
		this.panel1.Controls.Add(this.ultraLabel10);
		this.panel1.Controls.Add(this.Btn7);
		this.panel1.Controls.Add(this.ultraLabel9);
		this.panel1.Controls.Add(this.Btn11);
		this.panel1.Controls.Add(this.ultraLabel8);
		this.panel1.Controls.Add(this.Btn10);
		this.panel1.Controls.Add(this.ultraLabel6);
		this.panel1.Controls.Add(this.Btn5);
		this.panel1.Controls.Add(this.ultraLabel5);
		this.panel1.Controls.Add(this.ultraLabel4);
		this.panel1.Controls.Add(this.ultraLabel3);
		this.panel1.Controls.Add(this.Btn4);
		this.panel1.Controls.Add(this.Btn3);
		this.panel1.Controls.Add(this.Btn2);
		this.panel1.Controls.Add(this.ultraLabel2);
		this.panel1.Controls.Add(this.Btn1);
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Controls.Add(this.pictureBox1);
		this.panel1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(602, 436);
		this.panel1.TabIndex = 0;
		appearance1.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel12.Appearance = appearance1;
		appearance2.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance2.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance2.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel12.HotTrackAppearance = appearance2;
		this.ultraLabel12.HotTracking = true;
		this.ultraLabel12.Location = new System.Drawing.Point(360, 245);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(83, 20);
		this.ultraLabel12.TabIndex = 30;
		this.ultraLabel12.Text = "結算與決算";
		this.ultraLabel12.Click += new System.EventHandler(Btn14_Click);
		appearance3.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance3.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance3.ImageBackground");
		this.Btn14.Appearance = appearance3;
		this.Btn14.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
		appearance4.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance4.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance4.ImageBackground");
		this.Btn14.HotTrackAppearance = appearance4;
		this.Btn14.HotTracking = true;
		this.Btn14.ImageSize = new System.Drawing.Size(36, 36);
		this.Btn14.Location = new System.Drawing.Point(320, 235);
		this.Btn14.Name = "Btn14";
		this.Btn14.ShapeImage = (System.Drawing.Image)resources.GetObject("Btn14.ShapeImage");
		this.Btn14.ShowFocusRect = false;
		this.Btn14.ShowOutline = false;
		this.Btn14.Size = new System.Drawing.Size(36, 36);
		this.Btn14.TabIndex = 29;
		this.Btn14.Click += new System.EventHandler(Btn14_Click);
		appearance5.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel7.Appearance = appearance5;
		this.ultraLabel7.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance6.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance6.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance6.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel7.HotTrackAppearance = appearance6;
		this.ultraLabel7.HotTracking = true;
		this.ultraLabel7.Location = new System.Drawing.Point(360, 157);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(67, 20);
		this.ultraLabel7.TabIndex = 28;
		this.ultraLabel7.Text = "估驗計錄";
		this.ultraLabel7.Click += new System.EventHandler(Btn13_Click);
		appearance7.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance7.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance7.ImageBackground");
		this.Btn13.Appearance = appearance7;
		this.Btn13.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
		appearance8.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance8.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance8.ImageBackground");
		this.Btn13.HotTrackAppearance = appearance8;
		this.Btn13.HotTracking = true;
		this.Btn13.ImageSize = new System.Drawing.Size(36, 36);
		this.Btn13.Location = new System.Drawing.Point(320, 147);
		this.Btn13.Name = "Btn13";
		this.Btn13.ShapeImage = (System.Drawing.Image)resources.GetObject("Btn13.ShapeImage");
		this.Btn13.ShowFocusRect = false;
		this.Btn13.ShowOutline = false;
		this.Btn13.Size = new System.Drawing.Size(36, 36);
		this.Btn13.TabIndex = 27;
		this.Btn13.Click += new System.EventHandler(Btn13_Click);
		appearance9.BackColor = System.Drawing.Color.FromArgb(202, 237, 245);
		appearance9.BackColor2 = System.Drawing.Color.FromArgb(202, 237, 245);
		appearance9.ForeColor = System.Drawing.Color.Red;
		this.lblUseDatabase.Appearance = appearance9;
		this.lblUseDatabase.Location = new System.Drawing.Point(92, 412);
		this.lblUseDatabase.Name = "lblUseDatabase";
		this.lblUseDatabase.Size = new System.Drawing.Size(496, 23);
		this.lblUseDatabase.TabIndex = 26;
		this.lblUseDatabase.Text = "目前資料庫:";
		appearance10.BackColor = System.Drawing.Color.FromArgb(202, 237, 245);
		appearance10.BackColor2 = System.Drawing.Color.FromArgb(202, 237, 245);
		appearance10.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance10.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance10.ForeColor = System.Drawing.Color.Navy;
		appearance10.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance10.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel13.Appearance = appearance10;
		appearance11.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance11.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance11.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel13.HotTrackAppearance = appearance11;
		this.ultraLabel13.HotTracking = true;
		this.ultraLabel13.Location = new System.Drawing.Point(1, 408);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(67, 20);
		this.ultraLabel13.TabIndex = 25;
		this.ultraLabel13.Text = "面板切換";
		this.ultraLabel13.Click += new System.EventHandler(BtnSwitch_Click);
		appearance12.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance12.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel11.Appearance = appearance12;
		appearance13.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance13.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance13.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel11.HotTrackAppearance = appearance13;
		this.ultraLabel11.HotTracking = true;
		this.ultraLabel11.Location = new System.Drawing.Point(360, 201);
		this.ultraLabel11.Name = "ultraLabel11";
		this.ultraLabel11.Size = new System.Drawing.Size(67, 20);
		this.ultraLabel11.TabIndex = 21;
		this.ultraLabel11.Text = "契約變更";
		this.ultraLabel11.Click += new System.EventHandler(Btn6_Click);
		appearance14.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance14.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance14.ImageBackground");
		this.Btn8.Appearance = appearance14;
		this.Btn8.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
		appearance15.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance15.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance15.ImageBackground");
		this.Btn8.HotTrackAppearance = appearance15;
		this.Btn8.HotTracking = true;
		this.Btn8.ImageSize = new System.Drawing.Size(36, 36);
		this.Btn8.Location = new System.Drawing.Point(320, 191);
		this.Btn8.Name = "Btn8";
		this.Btn8.ShapeImage = (System.Drawing.Image)resources.GetObject("Btn8.ShapeImage");
		this.Btn8.ShowFocusRect = false;
		this.Btn8.ShowOutline = false;
		this.Btn8.Size = new System.Drawing.Size(36, 36);
		this.Btn8.TabIndex = 20;
		this.Btn8.Click += new System.EventHandler(Btn6_Click);
		appearance16.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance16.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel10.Appearance = appearance16;
		this.ultraLabel10.BackColor = System.Drawing.Color.White;
		this.ultraLabel10.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance17.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance17.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance17.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel10.HotTrackAppearance = appearance17;
		this.ultraLabel10.HotTracking = true;
		this.ultraLabel10.Location = new System.Drawing.Point(360, 112);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(67, 20);
		this.ultraLabel10.TabIndex = 19;
		this.ultraLabel10.Text = "契約編製";
		this.ultraLabel10.Click += new System.EventHandler(Btn7_Click);
		appearance18.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance18.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance18.ImageBackground");
		this.Btn7.Appearance = appearance18;
		this.Btn7.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
		appearance19.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance19.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance19.ImageBackground");
		this.Btn7.HotTrackAppearance = appearance19;
		this.Btn7.HotTracking = true;
		this.Btn7.ImageSize = new System.Drawing.Size(36, 36);
		this.Btn7.Location = new System.Drawing.Point(320, 104);
		this.Btn7.Name = "Btn7";
		this.Btn7.ShapeImage = (System.Drawing.Image)resources.GetObject("Btn7.ShapeImage");
		this.Btn7.ShowFocusRect = false;
		this.Btn7.ShowOutline = false;
		this.Btn7.Size = new System.Drawing.Size(36, 36);
		this.Btn7.TabIndex = 18;
		this.Btn7.Click += new System.EventHandler(Btn7_Click);
		appearance20.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance20.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel9.Appearance = appearance20;
		appearance21.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance21.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance21.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel9.HotTrackAppearance = appearance21;
		this.ultraLabel9.HotTracking = true;
		this.ultraLabel9.Location = new System.Drawing.Point(80, 245);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(129, 20);
		this.ultraLabel9.TabIndex = 17;
		this.ultraLabel9.Text = "歷史工程單位造價";
		this.ultraLabel9.Click += new System.EventHandler(Btn11_Click);
		appearance22.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance22.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance22.ImageBackground");
		this.Btn11.Appearance = appearance22;
		this.Btn11.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
		appearance23.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance23.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance23.ImageBackground");
		this.Btn11.HotTrackAppearance = appearance23;
		this.Btn11.HotTracking = true;
		this.Btn11.ImageSize = new System.Drawing.Size(36, 36);
		this.Btn11.Location = new System.Drawing.Point(40, 235);
		this.Btn11.Name = "Btn11";
		this.Btn11.ShapeImage = (System.Drawing.Image)resources.GetObject("Btn11.ShapeImage");
		this.Btn11.ShowFocusRect = false;
		this.Btn11.ShowOutline = false;
		this.Btn11.Size = new System.Drawing.Size(36, 36);
		this.Btn11.TabIndex = 16;
		this.Btn11.Click += new System.EventHandler(Btn11_Click);
		appearance24.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance24.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel8.Appearance = appearance24;
		appearance25.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance25.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance25.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel8.HotTrackAppearance = appearance25;
		this.ultraLabel8.HotTracking = true;
		this.ultraLabel8.Location = new System.Drawing.Point(80, 289);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(98, 20);
		this.ultraLabel8.TabIndex = 15;
		this.ultraLabel8.Text = "經費審查比對";
		this.ultraLabel8.Click += new System.EventHandler(Btn10_Click);
		appearance26.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance26.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance26.ImageBackground");
		this.Btn10.Appearance = appearance26;
		this.Btn10.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
		appearance27.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance27.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance27.ImageBackground");
		this.Btn10.HotTrackAppearance = appearance27;
		this.Btn10.HotTracking = true;
		this.Btn10.ImageSize = new System.Drawing.Size(36, 36);
		this.Btn10.Location = new System.Drawing.Point(40, 279);
		this.Btn10.Name = "Btn10";
		this.Btn10.ShapeImage = (System.Drawing.Image)resources.GetObject("Btn10.ShapeImage");
		this.Btn10.ShowFocusRect = false;
		this.Btn10.ShowOutline = false;
		this.Btn10.Size = new System.Drawing.Size(36, 36);
		this.Btn10.TabIndex = 14;
		this.Btn10.Click += new System.EventHandler(Btn10_Click);
		appearance28.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance28.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel6.Appearance = appearance28;
		appearance29.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance29.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance29.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel6.HotTrackAppearance = appearance29;
		this.ultraLabel6.HotTracking = true;
		this.ultraLabel6.Location = new System.Drawing.Point(360, 360);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(67, 20);
		this.ultraLabel6.TabIndex = 11;
		this.ultraLabel6.Text = "系統維護";
		this.ultraLabel6.Click += new System.EventHandler(Btn5_Click);
		appearance30.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance30.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance30.ImageBackground");
		this.Btn5.Appearance = appearance30;
		this.Btn5.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
		appearance31.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance31.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance31.ImageBackground");
		this.Btn5.HotTrackAppearance = appearance31;
		this.Btn5.HotTracking = true;
		this.Btn5.ImageSize = new System.Drawing.Size(36, 36);
		this.Btn5.Location = new System.Drawing.Point(320, 352);
		this.Btn5.Name = "Btn5";
		this.Btn5.ShapeImage = (System.Drawing.Image)resources.GetObject("Btn5.ShapeImage");
		this.Btn5.ShowFocusRect = false;
		this.Btn5.ShowOutline = false;
		this.Btn5.Size = new System.Drawing.Size(36, 36);
		this.Btn5.TabIndex = 10;
		this.Btn5.Click += new System.EventHandler(Btn5_Click);
		appearance32.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance32.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel5.Appearance = appearance32;
		appearance33.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance33.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance33.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel5.HotTrackAppearance = appearance33;
		this.ultraLabel5.HotTracking = true;
		this.ultraLabel5.Location = new System.Drawing.Point(80, 69);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(114, 20);
		this.ultraLabel5.TabIndex = 9;
		this.ultraLabel5.Text = "基本資料庫維護";
		this.ultraLabel5.Click += new System.EventHandler(Btn4_Click);
		appearance34.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance34.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance34;
		appearance35.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance35.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance35.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel4.HotTrackAppearance = appearance35;
		this.ultraLabel4.HotTracking = true;
		this.ultraLabel4.Location = new System.Drawing.Point(80, 360);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(83, 20);
		this.ultraLabel4.TabIndex = 8;
		this.ultraLabel4.Text = "投標單填寫";
		this.ultraLabel4.Click += new System.EventHandler(Btn3_Click);
		appearance36.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance36.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel3.Appearance = appearance36;
		appearance37.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance37.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance37.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel3.HotTrackAppearance = appearance37;
		this.ultraLabel3.HotTracking = true;
		this.ultraLabel3.Location = new System.Drawing.Point(80, 201);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(83, 20);
		this.ultraLabel3.TabIndex = 7;
		this.ultraLabel3.Text = "預算書編製";
		this.ultraLabel3.Click += new System.EventHandler(Btn2_Click);
		appearance38.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance38.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance38.ImageBackground");
		this.Btn4.Appearance = appearance38;
		this.Btn4.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
		appearance39.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance39.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance39.ImageBackground");
		this.Btn4.HotTrackAppearance = appearance39;
		this.Btn4.HotTracking = true;
		this.Btn4.ImageSize = new System.Drawing.Size(36, 36);
		this.Btn4.Location = new System.Drawing.Point(40, 62);
		this.Btn4.Name = "Btn4";
		this.Btn4.ShapeImage = (System.Drawing.Image)resources.GetObject("Btn4.ShapeImage");
		this.Btn4.ShowFocusRect = false;
		this.Btn4.ShowOutline = false;
		this.Btn4.Size = new System.Drawing.Size(36, 36);
		this.Btn4.TabIndex = 6;
		this.Btn4.Click += new System.EventHandler(Btn4_Click);
		appearance40.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance40.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance40.ImageBackground");
		this.Btn3.Appearance = appearance40;
		this.Btn3.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
		appearance41.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance41.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance41.ImageBackground");
		this.Btn3.HotTrackAppearance = appearance41;
		this.Btn3.HotTracking = true;
		this.Btn3.ImageSize = new System.Drawing.Size(36, 36);
		this.Btn3.Location = new System.Drawing.Point(40, 352);
		this.Btn3.Name = "Btn3";
		this.Btn3.ShapeImage = (System.Drawing.Image)resources.GetObject("Btn3.ShapeImage");
		this.Btn3.ShowFocusRect = false;
		this.Btn3.ShowOutline = false;
		this.Btn3.Size = new System.Drawing.Size(36, 36);
		this.Btn3.TabIndex = 5;
		this.Btn3.Click += new System.EventHandler(Btn3_Click);
		appearance42.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance42.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance42.ImageBackground");
		this.Btn2.Appearance = appearance42;
		this.Btn2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
		appearance43.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance43.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance43.ImageBackground");
		this.Btn2.HotTrackAppearance = appearance43;
		this.Btn2.HotTracking = true;
		this.Btn2.ImageSize = new System.Drawing.Size(36, 36);
		this.Btn2.Location = new System.Drawing.Point(40, 191);
		this.Btn2.Name = "Btn2";
		this.Btn2.ShapeImage = (System.Drawing.Image)resources.GetObject("Btn2.ShapeImage");
		this.Btn2.ShowFocusRect = false;
		this.Btn2.ShowOutline = false;
		this.Btn2.Size = new System.Drawing.Size(36, 36);
		this.Btn2.TabIndex = 4;
		this.Btn2.Click += new System.EventHandler(Btn2_Click);
		appearance44.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance44.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel2.Appearance = appearance44;
		appearance45.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance45.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
		appearance45.ForeColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance45.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel2.HotTrackAppearance = appearance45;
		this.ultraLabel2.HotTracking = true;
		this.ultraLabel2.Location = new System.Drawing.Point(80, 157);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(67, 20);
		this.ultraLabel2.TabIndex = 3;
		this.ultraLabel2.Text = "專案目錄";
		this.ultraLabel2.Click += new System.EventHandler(Btn1_Click);
		appearance46.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance46.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance46.ImageBackground");
		this.Btn1.Appearance = appearance46;
		this.Btn1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
		appearance47.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance47.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance47.ImageBackground");
		this.Btn1.HotTrackAppearance = appearance47;
		this.Btn1.HotTracking = true;
		this.Btn1.ImageSize = new System.Drawing.Size(36, 36);
		this.Btn1.Location = new System.Drawing.Point(40, 147);
		this.Btn1.Name = "Btn1";
		this.Btn1.ShapeImage = (System.Drawing.Image)resources.GetObject("Btn1.ShapeImage");
		this.Btn1.ShowFocusRect = false;
		this.Btn1.ShowOutline = false;
		this.Btn1.Size = new System.Drawing.Size(36, 36);
		this.Btn1.TabIndex = 2;
		this.Btn1.Click += new System.EventHandler(Btn1_Click);
		appearance48.BackColor = System.Drawing.Color.FromArgb(202, 237, 245);
		appearance48.BackColor2 = System.Drawing.Color.FromArgb(202, 237, 245);
		this.ultraLabel1.Appearance = appearance48;
		this.ultraLabel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ultraLabel1.Location = new System.Drawing.Point(0, 404);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(600, 30);
		this.ultraLabel1.TabIndex = 1;
		this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		this.pictureBox1.Location = new System.Drawing.Point(0, 0);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(600, 50);
		this.pictureBox1.TabIndex = 0;
		this.pictureBox1.TabStop = false;
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		this.BackColor = System.Drawing.SystemColors.AppWorkspace;
		base.ClientSize = new System.Drawing.Size(708, 553);
		base.Controls.Add(this.panel1);
		base.KeyPreview = true;
		base.Name = "FormPanel";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "FormPanel";
		base.Load += new System.EventHandler(FormPanel_Load);
		base.Resize += new System.EventHandler(FormPanel_Resize);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormPanel_KeyDown);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
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
