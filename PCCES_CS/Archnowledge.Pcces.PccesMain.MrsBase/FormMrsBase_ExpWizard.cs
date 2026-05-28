using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.CommonClass.MrsBase;
using Archnowledge.Pcces.DomainModule.ExportExcel;
using Archnowledge.Pcces.DomainModule.MrsBase;
using Archnowledge.Pcces.PccesMain.ShellLib;
using Archnowledge.Pcces.STDClass;
using Aspose.Cells;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinProgressBar;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.MrsBase;

public class FormMrsBase_ExpWizard : Form
{
	private const string CallFormHelp = "FormMrsBase_ExpWizard";

	private IContainer components;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabControl TabCtrl;

	private UltraTabPageControl Tab_A;

	private UltraTabPageControl Tab_B;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private RadioButton RB1;

	private UltraLabel ultraLabel3;

	private RadioButton RB2;

	private Panel panel1;

	private GroupBox groupBox1;

	private UltraButton A_Btn_Cncl;

	private UltraButton A_Btn_Next;

	private UltraButton A_Btn_Prev;

	private Panel panel5;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel6;

	private Panel panel2;

	private GroupBox groupBox2;

	private Panel panel3;

	private UltraButton ultraButton4;

	private UltraTextEditor txtExpDirFile;

	private UltraLabel ultraLabel5;

	private Panel panel6;

	private GroupBox groupBox3;

	private Panel panel4;

	private UltraLabel ultraLabel9;

	private Panel panel7;

	private GroupBox groupBox4;

	private UltraLabel ultraLabel12;

	private UltraLabel ultraLabel13;

	private UltraLabel ultraLabel14;

	private UltraTabPageControl Tab_C;

	private UltraTabPageControl Tab_D;

	private UltraButton B_Btn_Cncl;

	private UltraButton B_Btn_Next;

	private UltraButton B_Btn_Prev;

	private UltraButton D_Btn_Fnsh;

	private UltraButton D_Btn_Prev;

	private UltraButton C_Btn_Cncl;

	private UltraButton C_Btn_Next;

	private UltraButton C_Btn_Prev;

	private UltraProgressBar Prog1;

	private SaveFileDialog saveFileDialog1;

	private Timer timer1;

	private UltraLabel lblProg1;

	private UltraLabel lblWait;

	private UltraLabel lblRB2;

	private UltraButton BtnExcelOpen;

	private UltraLabel lblEXCEL;

	private PccesFormAction F_ActionName;

	private string F_ProjectCode = "";

	private string F_UserID;

	private int F_ProgPos = 0;

	private int F_ProgMax = 0;

	private int F_ProgMin = 0;

	private ExportType F_ExportType;

	private DataTable F_DT_ExpDatasAll;

	private DataTable F_DT_ExpDatas;

	private DataSet dsPwrSet;

	public PccesFormAction _ActionName
	{
		get
		{
			return F_ActionName;
		}
		set
		{
			F_ActionName = value;
		}
	}

	public string _ProjectCode
	{
		get
		{
			return F_ProjectCode;
		}
		set
		{
			F_ProjectCode = value;
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

	public DataTable _DT_ExpDatas
	{
		get
		{
			return F_DT_ExpDatas;
		}
		set
		{
			F_DT_ExpDatas = value;
		}
	}

	public ExportType _ExportType
	{
		get
		{
			return F_ExportType;
		}
		set
		{
			F_ExportType = value;
		}
	}

	public DataSet _dsPwrSet
	{
		get
		{
			return dsPwrSet;
		}
		set
		{
			dsPwrSet = value;
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.MrsBase.FormMrsBase_ExpWizard));
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
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.lblRB2 = new Infragistics.Win.Misc.UltraLabel();
		this.RB2 = new System.Windows.Forms.RadioButton();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.RB1 = new System.Windows.Forms.RadioButton();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel3 = new System.Windows.Forms.Panel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton4 = new Infragistics.Win.Misc.UltraButton();
		this.txtExpDirFile = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.panel2 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.B_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.B_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.B_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_C = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.lblWait = new Infragistics.Win.Misc.UltraLabel();
		this.Prog1 = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
		this.panel7 = new System.Windows.Forms.Panel();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.C_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.C_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.C_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.lblProg1 = new Infragistics.Win.Misc.UltraLabel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_D = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.BtnExcelOpen = new Infragistics.Win.Misc.UltraButton();
		this.lblEXCEL = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.panel6 = new System.Windows.Forms.Panel();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.D_Btn_Fnsh = new Infragistics.Win.Misc.UltraButton();
		this.D_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.TabCtrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.Tab_A.SuspendLayout();
		this.panel1.SuspendLayout();
		this.Tab_B.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtExpDirFile).BeginInit();
		this.panel2.SuspendLayout();
		this.panel5.SuspendLayout();
		this.Tab_C.SuspendLayout();
		this.panel7.SuspendLayout();
		this.panel4.SuspendLayout();
		this.Tab_D.SuspendLayout();
		this.panel6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TabCtrl).BeginInit();
		this.TabCtrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.panel1);
		this.Tab_A.Controls.Add(this.lblRB2);
		this.Tab_A.Controls.Add(this.RB2);
		this.Tab_A.Controls.Add(this.ultraLabel3);
		this.Tab_A.Controls.Add(this.RB1);
		this.Tab_A.Controls.Add(this.ultraLabel2);
		this.Tab_A.Controls.Add(this.ultraLabel1);
		this.Tab_A.Location = new System.Drawing.Point(0, 0);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(516, 369);
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.groupBox1);
		this.panel1.Controls.Add(this.A_Btn_Cncl);
		this.panel1.Controls.Add(this.A_Btn_Next);
		this.panel1.Controls.Add(this.A_Btn_Prev);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 325);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(516, 44);
		this.panel1.TabIndex = 9;
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(516, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Cncl.Appearance = appearance1;
		this.A_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Cncl.Location = new System.Drawing.Point(416, 9);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.ShowFocusRect = false;
		this.A_Btn_Cncl.ShowOutline = false;
		this.A_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Cncl.SupportThemes = false;
		this.A_Btn_Cncl.TabIndex = 2;
		this.A_Btn_Cncl.Text = "取消";
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Next.Appearance = appearance2;
		this.A_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Next.Location = new System.Drawing.Point(324, 9);
		this.A_Btn_Next.Name = "A_Btn_Next";
		this.A_Btn_Next.ShowFocusRect = false;
		this.A_Btn_Next.ShowOutline = false;
		this.A_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Next.SupportThemes = false;
		this.A_Btn_Next.TabIndex = 1;
		this.A_Btn_Next.Text = "下一步";
		this.A_Btn_Next.Click += new System.EventHandler(A_Btn_Next_Click);
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.ImageHAlign = Infragistics.Win.HAlign.Left;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Prev.Appearance = appearance3;
		this.A_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Prev.Location = new System.Drawing.Point(232, 9);
		this.A_Btn_Prev.Name = "A_Btn_Prev";
		this.A_Btn_Prev.ShowFocusRect = false;
		this.A_Btn_Prev.ShowOutline = false;
		this.A_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Prev.SupportThemes = false;
		this.A_Btn_Prev.TabIndex = 0;
		this.A_Btn_Prev.Text = "上一步";
		this.A_Btn_Prev.Visible = false;
		appearance4.BackColor = System.Drawing.Color.White;
		this.lblRB2.Appearance = appearance4;
		this.lblRB2.Location = new System.Drawing.Point(64, 171);
		this.lblRB2.Name = "lblRB2";
		this.lblRB2.Size = new System.Drawing.Size(445, 20);
		this.lblRB2.TabIndex = 8;
		this.lblRB2.Text = "僅會將您所選取的資料匯出";
		this.RB2.BackColor = System.Drawing.Color.White;
		this.RB2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.RB2.Location = new System.Drawing.Point(48, 140);
		this.RB2.Name = "RB2";
		this.RB2.Size = new System.Drawing.Size(168, 24);
		this.RB2.TabIndex = 7;
		this.RB2.Text = "選定範圍";
		appearance5.BackColor = System.Drawing.Color.White;
		this.ultraLabel3.Appearance = appearance5;
		this.ultraLabel3.Location = new System.Drawing.Point(63, 100);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(445, 20);
		this.ultraLabel3.TabIndex = 6;
		this.ultraLabel3.Text = "會將資料庫中，所有資料全部匯出，不僅只有現看到的資料筆數";
		this.RB1.BackColor = System.Drawing.Color.White;
		this.RB1.Checked = true;
		this.RB1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.RB1.Location = new System.Drawing.Point(48, 74);
		this.RB1.Name = "RB1";
		this.RB1.Size = new System.Drawing.Size(168, 24);
		this.RB1.TabIndex = 3;
		this.RB1.TabStop = true;
		this.RB1.Text = "全部";
		appearance6.BackColor = System.Drawing.Color.White;
		this.ultraLabel2.Appearance = appearance6;
		this.ultraLabel2.Location = new System.Drawing.Point(43, 52);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel2.TabIndex = 2;
		this.ultraLabel2.Text = "你要以哪種方式匯出資料?";
		appearance7.BackColor = System.Drawing.Color.White;
		this.ultraLabel1.Appearance = appearance7;
		this.ultraLabel1.Location = new System.Drawing.Point(8, 16);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(500, 20);
		this.ultraLabel1.TabIndex = 1;
		this.ultraLabel1.Text = "歡迎使用基本資料匯出精靈，接下來我們將引導您一步一步建立匯出動作";
		this.Tab_B.Controls.Add(this.panel3);
		this.Tab_B.Controls.Add(this.panel2);
		this.Tab_B.Controls.Add(this.panel5);
		this.Tab_B.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(516, 369);
		this.panel3.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel3.Controls.Add(this.ultraLabel5);
		this.panel3.Controls.Add(this.ultraButton4);
		this.panel3.Controls.Add(this.txtExpDirFile);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 60);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(516, 265);
		this.panel3.TabIndex = 14;
		this.ultraLabel5.Location = new System.Drawing.Point(11, 48);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel5.TabIndex = 4;
		this.ultraLabel5.Text = "存放的目錄及檔名:";
		appearance8.FontData.Name = "Arial";
		appearance8.FontData.SizeInPoints = 8f;
		this.ultraButton4.Appearance = appearance8;
		this.ultraButton4.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton4.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton4.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton4.Location = new System.Drawing.Point(459, 71);
		this.ultraButton4.Name = "ultraButton4";
		this.ultraButton4.ShowFocusRect = false;
		this.ultraButton4.ShowOutline = false;
		this.ultraButton4.Size = new System.Drawing.Size(48, 24);
		this.ultraButton4.SupportThemes = false;
		this.ultraButton4.TabIndex = 1;
		this.ultraButton4.Text = "瀏覽...";
		this.ultraButton4.Click += new System.EventHandler(ultraButton4_Click);
		appearance9.FontData.Name = "細明體";
		appearance9.FontData.SizeInPoints = 11f;
		this.txtExpDirFile.Appearance = appearance9;
		this.txtExpDirFile.Location = new System.Drawing.Point(12, 72);
		this.txtExpDirFile.Name = "txtExpDirFile";
		this.txtExpDirFile.Size = new System.Drawing.Size(448, 24);
		this.txtExpDirFile.TabIndex = 0;
		this.panel2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel2.Controls.Add(this.groupBox2);
		this.panel2.Controls.Add(this.B_Btn_Cncl);
		this.panel2.Controls.Add(this.B_Btn_Next);
		this.panel2.Controls.Add(this.B_Btn_Prev);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 325);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(516, 44);
		this.panel2.TabIndex = 13;
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(516, 8);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		appearance10.Image = resources.GetObject("appearance10.Image");
		appearance10.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Cncl.Appearance = appearance10;
		this.B_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.B_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Cncl.Location = new System.Drawing.Point(416, 9);
		this.B_Btn_Cncl.Name = "B_Btn_Cncl";
		this.B_Btn_Cncl.ShowFocusRect = false;
		this.B_Btn_Cncl.ShowOutline = false;
		this.B_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Cncl.SupportThemes = false;
		this.B_Btn_Cncl.TabIndex = 2;
		this.B_Btn_Cncl.Text = "取消";
		appearance11.Image = resources.GetObject("appearance11.Image");
		appearance11.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance11.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Next.Appearance = appearance11;
		this.B_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Next.Location = new System.Drawing.Point(324, 9);
		this.B_Btn_Next.Name = "B_Btn_Next";
		this.B_Btn_Next.ShowFocusRect = false;
		this.B_Btn_Next.ShowOutline = false;
		this.B_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Next.SupportThemes = false;
		this.B_Btn_Next.TabIndex = 1;
		this.B_Btn_Next.Text = "下一步";
		this.B_Btn_Next.Click += new System.EventHandler(B_Btn_Next_Click);
		appearance12.Image = resources.GetObject("appearance12.Image");
		appearance12.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Prev.Appearance = appearance12;
		this.B_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Prev.Location = new System.Drawing.Point(232, 9);
		this.B_Btn_Prev.Name = "B_Btn_Prev";
		this.B_Btn_Prev.ShowFocusRect = false;
		this.B_Btn_Prev.ShowOutline = false;
		this.B_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Prev.SupportThemes = false;
		this.B_Btn_Prev.TabIndex = 0;
		this.B_Btn_Prev.Text = "上一步";
		this.B_Btn_Prev.Click += new System.EventHandler(B_Btn_Prev_Click);
		this.panel5.BackColor = System.Drawing.Color.White;
		this.panel5.Controls.Add(this.ultraLabel7);
		this.panel5.Controls.Add(this.ultraLabel6);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(516, 60);
		this.panel5.TabIndex = 12;
		appearance13.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance13;
		this.ultraLabel7.Location = new System.Drawing.Point(47, 34);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel7.TabIndex = 3;
		this.ultraLabel7.Text = "請挑選匯出存放的目錄及檔案名稱";
		appearance14.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance14;
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(16, 12);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 2;
		this.ultraLabel6.Text = "資料匯出路徑及檔案名稱";
		this.Tab_C.Controls.Add(this.lblWait);
		this.Tab_C.Controls.Add(this.Prog1);
		this.Tab_C.Controls.Add(this.panel7);
		this.Tab_C.Controls.Add(this.lblProg1);
		this.Tab_C.Controls.Add(this.panel4);
		this.Tab_C.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_C.Name = "Tab_C";
		this.Tab_C.Size = new System.Drawing.Size(516, 369);
		this.lblWait.Location = new System.Drawing.Point(16, 81);
		this.lblWait.Name = "lblWait";
		this.lblWait.Size = new System.Drawing.Size(476, 20);
		this.lblWait.TabIndex = 17;
		this.lblWait.Text = "正在準備匯出的資料，這個動作會花些時間，請稍候。";
		this.Prog1.Location = new System.Drawing.Point(20, 128);
		this.Prog1.Name = "Prog1";
		this.Prog1.Size = new System.Drawing.Size(476, 23);
		this.Prog1.SupportThemes = false;
		this.Prog1.TabIndex = 16;
		this.Prog1.Text = "[Formatted]";
		this.Prog1.Visible = false;
		this.panel7.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel7.Controls.Add(this.groupBox4);
		this.panel7.Controls.Add(this.C_Btn_Cncl);
		this.panel7.Controls.Add(this.C_Btn_Next);
		this.panel7.Controls.Add(this.C_Btn_Prev);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel7.Location = new System.Drawing.Point(0, 325);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(516, 44);
		this.panel7.TabIndex = 15;
		this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox4.Location = new System.Drawing.Point(0, 0);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(516, 8);
		this.groupBox4.TabIndex = 3;
		this.groupBox4.TabStop = false;
		appearance15.Image = resources.GetObject("appearance15.Image");
		appearance15.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.C_Btn_Cncl.Appearance = appearance15;
		this.C_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.C_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.C_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.C_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.C_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.C_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.C_Btn_Cncl.Location = new System.Drawing.Point(416, 9);
		this.C_Btn_Cncl.Name = "C_Btn_Cncl";
		this.C_Btn_Cncl.ShowFocusRect = false;
		this.C_Btn_Cncl.ShowOutline = false;
		this.C_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.C_Btn_Cncl.SupportThemes = false;
		this.C_Btn_Cncl.TabIndex = 2;
		this.C_Btn_Cncl.Text = "取消";
		this.C_Btn_Cncl.Visible = false;
		appearance16.Image = resources.GetObject("appearance16.Image");
		appearance16.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance16.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.C_Btn_Next.Appearance = appearance16;
		this.C_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.C_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.C_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.C_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.C_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.C_Btn_Next.Location = new System.Drawing.Point(324, 9);
		this.C_Btn_Next.Name = "C_Btn_Next";
		this.C_Btn_Next.ShowFocusRect = false;
		this.C_Btn_Next.ShowOutline = false;
		this.C_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.C_Btn_Next.SupportThemes = false;
		this.C_Btn_Next.TabIndex = 1;
		this.C_Btn_Next.Text = "下一步";
		this.C_Btn_Next.Visible = false;
		appearance17.Image = resources.GetObject("appearance17.Image");
		appearance17.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.C_Btn_Prev.Appearance = appearance17;
		this.C_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.C_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.C_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.C_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.C_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.C_Btn_Prev.Location = new System.Drawing.Point(232, 9);
		this.C_Btn_Prev.Name = "C_Btn_Prev";
		this.C_Btn_Prev.ShowFocusRect = false;
		this.C_Btn_Prev.ShowOutline = false;
		this.C_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.C_Btn_Prev.SupportThemes = false;
		this.C_Btn_Prev.TabIndex = 0;
		this.C_Btn_Prev.Text = "上一步";
		this.C_Btn_Prev.Visible = false;
		this.lblProg1.Location = new System.Drawing.Point(16, 104);
		this.lblProg1.Name = "lblProg1";
		this.lblProg1.Size = new System.Drawing.Size(144, 20);
		this.lblProg1.TabIndex = 14;
		this.lblProg1.Text = "正在轉出基本資料";
		this.lblProg1.Visible = false;
		this.panel4.BackColor = System.Drawing.Color.White;
		this.panel4.Controls.Add(this.ultraLabel9);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel4.Location = new System.Drawing.Point(0, 0);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(516, 60);
		this.panel4.TabIndex = 13;
		appearance18.BackColor = System.Drawing.Color.White;
		this.ultraLabel9.Appearance = appearance18;
		this.ultraLabel9.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel9.Location = new System.Drawing.Point(16, 12);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel9.TabIndex = 2;
		this.ultraLabel9.Text = "資料匯出中...";
		this.Tab_D.Controls.Add(this.BtnExcelOpen);
		this.Tab_D.Controls.Add(this.lblEXCEL);
		this.Tab_D.Controls.Add(this.ultraLabel14);
		this.Tab_D.Controls.Add(this.ultraLabel13);
		this.Tab_D.Controls.Add(this.ultraLabel12);
		this.Tab_D.Controls.Add(this.panel6);
		this.Tab_D.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_D.Name = "Tab_D";
		this.Tab_D.Size = new System.Drawing.Size(516, 369);
		appearance19.FontData.Name = "Arial";
		appearance19.FontData.SizeInPoints = 8f;
		this.BtnExcelOpen.Appearance = appearance19;
		this.BtnExcelOpen.BackColor = System.Drawing.SystemColors.Control;
		this.BtnExcelOpen.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.BtnExcelOpen.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.BtnExcelOpen.Location = new System.Drawing.Point(37, 162);
		this.BtnExcelOpen.Name = "BtnExcelOpen";
		this.BtnExcelOpen.ShowFocusRect = false;
		this.BtnExcelOpen.ShowOutline = false;
		this.BtnExcelOpen.Size = new System.Drawing.Size(88, 24);
		this.BtnExcelOpen.SupportThemes = false;
		this.BtnExcelOpen.TabIndex = 23;
		this.BtnExcelOpen.Text = "直接開啟：";
		this.BtnExcelOpen.Visible = false;
		this.BtnExcelOpen.Click += new System.EventHandler(BtnExcelOpen_Click);
		appearance20.ForeColor = System.Drawing.Color.Red;
		this.lblEXCEL.Appearance = appearance20;
		this.lblEXCEL.Location = new System.Drawing.Point(132, 163);
		this.lblEXCEL.Name = "lblEXCEL";
		this.lblEXCEL.Size = new System.Drawing.Size(368, 43);
		this.lblEXCEL.TabIndex = 22;
		this.lblEXCEL.Visible = false;
		appearance21.BackColor = System.Drawing.Color.White;
		this.ultraLabel14.Appearance = appearance21;
		this.ultraLabel14.Location = new System.Drawing.Point(36, 116);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel14.TabIndex = 13;
		this.ultraLabel14.Text = "若要結束精靈，請按一下[完成]。";
		appearance22.BackColor = System.Drawing.Color.White;
		this.ultraLabel13.Appearance = appearance22;
		this.ultraLabel13.Location = new System.Drawing.Point(36, 64);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel13.TabIndex = 12;
		this.ultraLabel13.Text = "你已經成功匯出資料。";
		appearance23.BackColor = System.Drawing.Color.White;
		this.ultraLabel12.Appearance = appearance23;
		this.ultraLabel12.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel12.Location = new System.Drawing.Point(20, 20);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel12.TabIndex = 11;
		this.ultraLabel12.Text = "恭禧您!";
		this.panel6.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel6.Controls.Add(this.groupBox3);
		this.panel6.Controls.Add(this.D_Btn_Fnsh);
		this.panel6.Controls.Add(this.D_Btn_Prev);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel6.Location = new System.Drawing.Point(0, 325);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(516, 44);
		this.panel6.TabIndex = 10;
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox3.Location = new System.Drawing.Point(0, 0);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(516, 8);
		this.groupBox3.TabIndex = 3;
		this.groupBox3.TabStop = false;
		appearance24.Image = resources.GetObject("appearance24.Image");
		appearance24.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Fnsh.Appearance = appearance24;
		this.D_Btn_Fnsh.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Fnsh.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Fnsh.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.D_Btn_Fnsh.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Fnsh.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Fnsh.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Fnsh.Location = new System.Drawing.Point(324, 9);
		this.D_Btn_Fnsh.Name = "D_Btn_Fnsh";
		this.D_Btn_Fnsh.ShowFocusRect = false;
		this.D_Btn_Fnsh.ShowOutline = false;
		this.D_Btn_Fnsh.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Fnsh.SupportThemes = false;
		this.D_Btn_Fnsh.TabIndex = 1;
		this.D_Btn_Fnsh.Text = "完成";
		this.D_Btn_Fnsh.Click += new System.EventHandler(D_Btn_Fnsh_Click);
		appearance25.Image = resources.GetObject("appearance25.Image");
		appearance25.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Prev.Appearance = appearance25;
		this.D_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Prev.Location = new System.Drawing.Point(232, 9);
		this.D_Btn_Prev.Name = "D_Btn_Prev";
		this.D_Btn_Prev.ShowFocusRect = false;
		this.D_Btn_Prev.ShowOutline = false;
		this.D_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Prev.SupportThemes = false;
		this.D_Btn_Prev.TabIndex = 0;
		this.D_Btn_Prev.Text = "上一步";
		this.D_Btn_Prev.Visible = false;
		this.D_Btn_Prev.Click += new System.EventHandler(D_Btn_Prev_Click);
		this.TabCtrl.BackColor = System.Drawing.Color.White;
		this.TabCtrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.TabCtrl.Controls.Add(this.Tab_A);
		this.TabCtrl.Controls.Add(this.Tab_B);
		this.TabCtrl.Controls.Add(this.Tab_C);
		this.TabCtrl.Controls.Add(this.Tab_D);
		this.TabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TabCtrl.Location = new System.Drawing.Point(0, 0);
		this.TabCtrl.Name = "TabCtrl";
		this.TabCtrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.TabCtrl.Size = new System.Drawing.Size(516, 369);
		this.TabCtrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.TabCtrl.TabIndex = 0;
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "tab1";
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "tab2";
		ultraTab3.TabPage = this.Tab_C;
		ultraTab3.Text = "tab3";
		ultraTab4.TabPage = this.Tab_D;
		ultraTab4.Text = "tab4";
		this.TabCtrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[4] { ultraTab1, ultraTab2, ultraTab3, ultraTab4 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(516, 369);
		this.saveFileDialog1.RestoreDirectory = true;
		this.timer1.Enabled = true;
		this.timer1.Interval = 1000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.CancelButton = this.A_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(516, 369);
		base.Controls.Add(this.TabCtrl);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormMrsBase_ExpWizard";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "匯出";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormMrsBase_ExpWizard_KeyDown);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormMrsBase_ExpWizard_FormClosing);
		base.KeyPress += new System.Windows.Forms.KeyPressEventHandler(FormMrsBase_ExpWizard_KeyPress);
		base.Load += new System.EventHandler(FormMrsBase_ExpWizard_Load);
		this.Tab_A.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.Tab_B.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtExpDirFile).EndInit();
		this.panel2.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		this.Tab_C.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.Tab_D.ResumeLayout(false);
		this.panel6.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.TabCtrl).EndInit();
		this.TabCtrl.ResumeLayout(false);
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

	public FormMrsBase_ExpWizard()
	{
		InitializeComponent();
	}

	private void A_Btn_Next_Click(object sender, EventArgs e)
	{
		Tab_B.Tab.Selected = true;
	}

	private void B_Btn_Next_Click(object sender, EventArgs e)
	{
		Prog1.Minimum = 0;
		Prog1.Maximum = 0;
		if (txtExpDirFile.Text.Trim() == "")
		{
			MessageBox.Show(this, "請先選定存放的目錄及檔名!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtExpDirFile.Focus();
			return;
		}
		if (F_ExportType == ExportType.Excel && CommonMethods.ExtractExtFileName(txtExpDirFile.Text.Trim()).ToUpper() != "XLS")
		{
			MessageBox.Show(this, "你選定的檔案，副檔名不是XLS，請重設!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtExpDirFile.Focus();
			return;
		}
		if (F_ExportType == ExportType.XML && CommonMethods.ExtractExtFileName(txtExpDirFile.Text.Trim()).ToUpper() != "XML")
		{
			MessageBox.Show(this, "你選定的檔案，副檔名不是XML，請重設!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtExpDirFile.Focus();
			return;
		}
		if (CommonMethods.ExtractFileNoExtName(txtExpDirFile.Text.Trim()) == "")
		{
			MessageBox.Show(this, "你選定的檔案，沒有檔名，請重設!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtExpDirFile.Focus();
			return;
		}
		if (txtExpDirFile.Text.IndexOf("/") >= 0)
		{
			MessageBox.Show(this, "你選定的檔案，不可以包含特殊字元 '/'，請重設!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtExpDirFile.Focus();
			return;
		}
		Cursor = Cursors.WaitCursor;
		if (RB1.Checked)
		{
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(F_UserID);
			aArr.Add("WinFORM 基本工料");
			Recost recost1 = new Recost(aArr);
			recost1.ps_prjcode = F_ProjectCode;
			recost1.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			Archnowledge.Pcces.BUDClass.MrsBaseA dbMrsBase = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
			dbMrsBase.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			dbMrsBase.ps_projectcode = F_ProjectCode;
			F_DT_ExpDatasAll = dbMrsBase.ListItem();
			F_DT_ExpDatasAll.Columns.Add("chk", Type.GetType("System.String"));
			for (int i = 0; i < F_DT_ExpDatasAll.Rows.Count; i++)
			{
				F_DT_ExpDatasAll.Rows[i]["chk"] = "1";
			}
		}
		Tab_C.Tab.Selected = true;
		Application.DoEvents();
		ExecuteExport();
		if (CommonMethods.ExtractExtFileName(txtExpDirFile.Text.Trim()).ToUpper() == "XLS")
		{
			BtnExcelOpen.Visible = false;
			lblEXCEL.Visible = false;
			lblEXCEL.Text = txtExpDirFile.Text.Trim();
		}
		else
		{
			BtnExcelOpen.Visible = false;
			lblEXCEL.Visible = false;
		}
		Tab_D.Tab.Selected = true;
		Cursor = Cursors.Default;
	}

	private void D_Btn_Prev_Click(object sender, EventArgs e)
	{
		Tab_B.Tab.Selected = true;
	}

	private void B_Btn_Prev_Click(object sender, EventArgs e)
	{
		Tab_A.Tab.Selected = true;
	}

	private void ultraButton4_Click(object sender, EventArgs e)
	{
		string sFilter = "";
		switch (F_ExportType)
		{
		case ExportType.DBF:
			sFilter = "dBaseIII files (*.dbf)|*.dbf";
			break;
		case ExportType.Excel:
			sFilter = "Microsoft Excel 97/2000 files (*.xls)|*.xls";
			break;
		case ExportType.XML:
			sFilter = "XML files (*.xml)|*.xml";
			break;
		}
		saveFileDialog1.Filter = sFilter;
		saveFileDialog1.RestoreDirectory = true;
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			txtExpDirFile.Text = saveFileDialog1.FileName;
		}
	}

	private void ExecuteExport()
	{
		if (F_ExportType == ExportType.Excel)
		{
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(F_UserID);
			aArr.Add("WinFORM 基本工料");
			Output_Com OUT_COM = new Output_Com(aArr);
			OUT_COM.dsPwrSet = dsPwrSet;
			ArrayList ExpArray = (RB1.Checked ? ((!(F_ProjectCode.Trim() != "")) ? OUT_COM.OutExcel(F_DT_ExpDatasAll) : OUT_COM.OutExcel(F_DT_ExpDatasAll, CommonMethods.GetActionNameString(F_ActionName), F_ProjectCode)) : ((!(F_ProjectCode.Trim() != "")) ? OUT_COM.OutExcel(F_DT_ExpDatas) : OUT_COM.OutExcel(F_DT_ExpDatas, CommonMethods.GetActionNameString(F_ActionName), F_ProjectCode)));
			DataSet DS1 = new DataSet("Export_DataSet");
			DS1.Tables.Add(((DataSet)ExpArray[0]).Tables[0].Copy());
			DS1.Tables.Add(((DataSet)ExpArray[1]).Tables[0].Copy());
			F_ProgMin = 0;
			F_ProgMax = DS1.Tables[0].Rows.Count + DS1.Tables[1].Rows.Count;
			Prog1.Maximum = F_ProgMax;
			Prog1.Minimum = F_ProgMin;
			lblProg1.Visible = true;
			Prog1.Visible = true;
			lblWait.Visible = false;
			ExportExcel(DS1, txtExpDirFile.Text.Trim());
		}
		else if (F_ExportType == ExportType.XML)
		{
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(F_UserID);
			aArr.Add("WinFORM 基本工料");
			Archnowledge.Pcces.BUDClass.MrsBaseA MrsACom = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
			Archnowledge.Pcces.DomainModule.MrsBase.MrsBaseA mrsBaseA = new Archnowledge.Pcces.DomainModule.MrsBase.MrsBaseA();
			DataTable dtMrsBaseA = mrsBaseA.GetMrsBaseAForXmlExport().Tables[0];
			dtMrsBaseA.TableName = "mrsbaseA";
			Archnowledge.Pcces.BUDClass.MrsBaseB MrsBCom = new Archnowledge.Pcces.BUDClass.MrsBaseB(aArr);
			MrsBCom.ps_projectcode = F_ProjectCode;
			MrsBCom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			DataTable ldt2 = MrsBCom.OutputItem();
			MrsBCom = null;
			ldt2.TableName = "mrsbaseB";
			DataSet ds = new DataSet();
			ds.Tables.Add(dtMrsBaseA.Copy());
			ds.Tables.Add(ldt2.Copy());
			ds.WriteXml(txtExpDirFile.Text.Trim());
			if (1 == 0)
			{
				MessageBox.Show(this, "資料轉出失敗!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				PubTools.WriteRoughlyLog(aArr);
			}
		}
	}

	public void ExportExcel(DataSet dsMrsBase, string FileName)
	{
		Aspose.Cells.License license = new Aspose.Cells.License();
		license.SetLicense("Aspose.Custom.lic");
		Excel excel = new Excel();
		excel.Worksheets.Add();
		Worksheet sheetMrsBaseA = excel.Worksheets[0];
		Worksheet sheetMrsBaseB = excel.Worksheets[1];
		sheetMrsBaseA.Name = "基本工項";
		sheetMrsBaseB.Name = "分析工項";
		string FontFace = "新細明體";
		int styleIndex = excel.Styles.Add();
		Style styleHeader = excel.Styles[styleIndex];
		styleHeader.Font.IsBold = true;
		styleHeader.Font.Color = Color.FromArgb(255, 0, 0);
		styleHeader.ForegroundColor = Color.FromArgb(0, 204, 255);
		styleHeader.Pattern = BackgroundType.Solid;
		styleHeader.Font.Size = 12;
		styleHeader.Font.Name = FontFace;
		SetAllBorders(styleHeader, CellBorderType.Thin);
		styleIndex = excel.Styles.Add();
		Style styleFirstColumn = excel.Styles[styleIndex];
		styleFirstColumn.Font.Size = 12;
		styleFirstColumn.Font.Name = FontFace;
		styleFirstColumn.ForegroundColor = Color.FromArgb(255, 204, 153);
		styleFirstColumn.Pattern = BackgroundType.Solid;
		styleFirstColumn.Number = 49;
		SetAllBorders(styleFirstColumn, CellBorderType.Thin);
		styleIndex = excel.Styles.Add();
		Style styleThirdColumn = excel.Styles[styleIndex];
		styleThirdColumn.Font.Size = 12;
		styleThirdColumn.Font.Name = FontFace;
		styleThirdColumn.ForegroundColor = Color.FromArgb(255, 255, 153);
		styleThirdColumn.Pattern = BackgroundType.Solid;
		styleThirdColumn.Number = 49;
		SetAllBorders(styleThirdColumn, CellBorderType.Thin);
		styleIndex = excel.Styles.Add();
		Style styleOther = excel.Styles[styleIndex];
		styleOther.Font.Size = 12;
		styleOther.Font.Name = FontFace;
		SetAllBorders(styleOther, CellBorderType.Thin);
		styleIndex = excel.Styles.Add();
		Style styleText = excel.Styles[styleIndex];
		styleText.Font.Size = 12;
		styleText.Font.Name = FontFace;
		styleText.Number = 49;
		SetAllBorders(styleText, CellBorderType.Thin);
		DataTable dtMrsBaseA = dsMrsBase.Tables[0];
		DataTable dtMrsBaseB = dsMrsBase.Tables[1];
		List<int> NumberColumns = new List<int>(new int[6] { 8, 9, 10, 11, 12, 17 });
		ImportDataTable(sheetMrsBaseA, dtMrsBaseA, NumberColumns);
		NumberColumns = new List<int>(new int[8] { 4, 5, 6, 7, 8, 10, 11, 12 });
		ImportDataTable(sheetMrsBaseB, dtMrsBaseB, NumberColumns);
		int mrsBaseARowCount = dtMrsBaseA.Rows.Count;
		int mrsBaseBRowCount = dtMrsBaseB.Rows.Count;
		sheetMrsBaseA.Cells.CreateRange(0, 0, mrsBaseARowCount, dtMrsBaseA.Columns.Count).Style = styleOther;
		sheetMrsBaseA.Cells.CreateRange(0, 0, 1, dtMrsBaseA.Columns.Count).Style = styleHeader;
		sheetMrsBaseA.Cells.CreateRange("A2", "A" + mrsBaseARowCount).Style = styleFirstColumn;
		sheetMrsBaseA.Cells.CreateRange("C2", "C" + mrsBaseARowCount).Style = styleThirdColumn;
		string[] TextColumns = new string[8] { "B", "D", "E", "F", "G", "O", "P", "S" };
		string[] array = TextColumns;
		foreach (string column in array)
		{
			sheetMrsBaseA.Cells.CreateRange(column + "2", column + mrsBaseARowCount).Style = styleText;
		}
		sheetMrsBaseB.Cells.CreateRange(0, 0, mrsBaseBRowCount, dtMrsBaseB.Columns.Count).Style = styleOther;
		sheetMrsBaseB.Cells.CreateRange(0, 0, 1, dtMrsBaseB.Columns.Count).Style = styleHeader;
		if (mrsBaseBRowCount > 1)
		{
			sheetMrsBaseB.Cells.CreateRange("A2", "A" + mrsBaseBRowCount).Style = styleFirstColumn;
			sheetMrsBaseB.Cells.CreateRange("C2", "C" + mrsBaseBRowCount).Style = styleThirdColumn;
		}
		AsposeCellsHelper.AutoFitCells(sheetMrsBaseA);
		AsposeCellsHelper.AutoFitCells(sheetMrsBaseB);
		excel.Save(FileName);
	}

	private void SetAllBorders(Style style, CellBorderType borderType)
	{
		style.Borders[BorderType.TopBorder].LineStyle = borderType;
		style.Borders[BorderType.RightBorder].LineStyle = borderType;
		style.Borders[BorderType.BottomBorder].LineStyle = borderType;
		style.Borders[BorderType.LeftBorder].LineStyle = borderType;
	}

	private void ImportDataTable(Worksheet sheetMrsBaseA, DataTable dtMrsBaseA, List<int> NumberColumns)
	{
		for (int rowIndex = 0; rowIndex < dtMrsBaseA.Rows.Count; rowIndex++)
		{
			for (int columnIndex = 0; columnIndex < dtMrsBaseA.Columns.Count; columnIndex++)
			{
				object value = dtMrsBaseA.Rows[rowIndex][columnIndex];
				if (rowIndex != 0 && NumberColumns.Contains(columnIndex) && value != DBNull.Value && value.ToString() != string.Empty)
				{
					sheetMrsBaseA.Cells[rowIndex, columnIndex].PutValue(ArchConvert.Obj2Double(value));
				}
				else
				{
					sheetMrsBaseA.Cells[rowIndex, columnIndex].PutValue(value);
				}
			}
		}
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		Application.DoEvents();
		Prog1.Value = F_ProgPos;
	}

	private void FormMrsBase_ExpWizard_Load(object sender, EventArgs e)
	{
		if (F_ExportType == ExportType.XML)
		{
			RB2.Visible = false;
			lblRB2.Visible = false;
		}
		LoadingScreen();
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("MrsBase_Exp", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("MrsBase_Exp", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("MrsBase_Exp", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("MrsBase_Exp", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("MrsBase_Exp", "PK_Height"));
		if (iSiz_W > 0)
		{
			base.Width = iSiz_W;
		}
		if (iSiz_H > 0)
		{
			base.Height = iSiz_H;
		}
	}

	private void D_Btn_Fnsh_Click(object sender, EventArgs e)
	{
		GC.Collect();
		base.DialogResult = DialogResult.OK;
	}

	private void BtnExcelOpen_Click(object sender, EventArgs e)
	{
		ShellExecute SHExe = new ShellExecute();
		SHExe.OwnerHandle = base.Handle;
		SHExe.Parameters = lblEXCEL.Text;
		SHExe.Path = lblEXCEL.Text;
		SHExe.Execute();
	}

	private void FormMrsBase_ExpWizard_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			if (Tab_A.Tab.Active)
			{
				A_Btn_Next_Click(this, e);
			}
			if (Tab_B.Tab.Active)
			{
				B_Btn_Next_Click(this, e);
			}
			if (Tab_D.Tab.Active)
			{
				D_Btn_Fnsh_Click(this, e);
			}
		}
		if (e.KeyChar == '\u001b')
		{
			if (Tab_A.Tab.Active)
			{
				base.DialogResult = DialogResult.Cancel;
			}
			if (Tab_B.Tab.Active)
			{
				base.DialogResult = DialogResult.Cancel;
			}
			if (Tab_D.Tab.Active)
			{
				D_Btn_Fnsh_Click(this, e);
			}
		}
	}

	private void FormMrsBase_ExpWizard_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("MrsBase_Exp", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("MrsBase_Exp", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("MrsBase_Exp", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("MrsBase_Exp", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("MrsBase_Exp", "WindowState", base.WindowState.ToString());
	}

	private void FormMrsBase_ExpWizard_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormMrsBase_ExpWizard");
		}
	}
}
