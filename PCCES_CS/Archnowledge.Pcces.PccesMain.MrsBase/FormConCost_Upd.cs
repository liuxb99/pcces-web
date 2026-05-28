using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.PccesUpdateServices;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using C1.Win.C1Input;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinProgressBar;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.MrsBase;

public class FormConCost_Upd : Form
{
	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_A;

	private Panel panel3;

	private C1PictureBox c1PictureBox1;

	private UltraLabel ultraLabel1;

	private GroupBox groupBox1;

	private Panel panel2;

	private UltraLabel ultraLabel2;

	private UltraTabPageControl Tab_B;

	private Panel panel1;

	private Panel panel7;

	private UltraButton D_Btn_Fnsh;

	private GroupBox groupBox4;

	private UltraButton C_Btn_Cncl;

	private Panel panel5;

	private UltraLabel ultraLabel6;

	private UltraTabPageControl Tab_C;

	private Panel panel6;

	private UltraLabel ultraLabel4;

	private Panel panel4;

	private UltraButton ultraButton1;

	private GroupBox groupBox2;

	private UltraTabPageControl Tab_B2;

	private Panel panel8;

	private UltraLabel ultraLabel8;

	private IContainer components;

	private UltraLabel ultraLabel7;

	public GridMrsBase Grid1;

	private string FORM_STATUS = "INI";

	private Panel panel9;

	private UltraProgressBar ProgressBar1;

	private UltraLabel ultraLabel3;

	private UltraProgressBar ProgressBar2;

	private string F_UserID;

	private Cesprice CesPriceCom;

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

	public FormConCost_Upd()
	{
		InitializeComponent();
		Grid1.Cols[3].Visible = false;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.MrsBase.FormConCost_Upd));
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
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel3 = new System.Windows.Forms.Panel();
		this.c1PictureBox1 = new C1.Win.C1Input.C1PictureBox();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.panel2 = new System.Windows.Forms.Panel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel7 = new System.Windows.Forms.Panel();
		this.D_Btn_Fnsh = new Infragistics.Win.Misc.UltraButton();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.C_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_B2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel8 = new System.Windows.Forms.Panel();
		this.panel9 = new System.Windows.Forms.Panel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ProgressBar1 = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_C = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel6 = new System.Windows.Forms.Panel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.ProgressBar2 = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
		this.Grid1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.Tab_A.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox1).BeginInit();
		this.panel2.SuspendLayout();
		this.Tab_B.SuspendLayout();
		this.panel1.SuspendLayout();
		this.panel7.SuspendLayout();
		this.panel5.SuspendLayout();
		this.Tab_B2.SuspendLayout();
		this.panel8.SuspendLayout();
		this.panel9.SuspendLayout();
		this.Tab_C.SuspendLayout();
		this.panel6.SuspendLayout();
		this.panel4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Grid1).BeginInit();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.panel3);
		this.Tab_A.Controls.Add(this.panel2);
		this.Tab_A.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(593, 572);
		this.panel3.BackColor = System.Drawing.Color.White;
		this.panel3.Controls.Add(this.c1PictureBox1);
		this.panel3.Controls.Add(this.ultraLabel1);
		this.panel3.Controls.Add(this.groupBox1);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 90);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(593, 482);
		this.panel3.TabIndex = 23;
		this.c1PictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.c1PictureBox1.Image = (System.Drawing.Image)resources.GetObject("c1PictureBox1.Image");
		this.c1PictureBox1.Location = new System.Drawing.Point(133, 89);
		this.c1PictureBox1.Name = "c1PictureBox1";
		this.c1PictureBox1.Size = new System.Drawing.Size(329, 147);
		this.c1PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
		this.c1PictureBox1.TabIndex = 5;
		this.c1PictureBox1.TabStop = false;
		this.ultraLabel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance1.TextHAlign = Infragistics.Win.HAlign.Center;
		this.ultraLabel1.Appearance = appearance1;
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(12, 57);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(569, 20);
		this.ultraLabel1.TabIndex = 4;
		this.ultraLabel1.Text = "讀取中...";
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(593, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.panel2.BackColor = System.Drawing.Color.White;
		this.panel2.Controls.Add(this.ultraLabel2);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(593, 90);
		this.panel2.TabIndex = 22;
		appearance2.BackColor = System.Drawing.Color.White;
		this.ultraLabel2.Appearance = appearance2;
		this.ultraLabel2.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel2.Location = new System.Drawing.Point(16, 12);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel2.TabIndex = 2;
		this.ultraLabel2.Text = "讀取年月區別資料...";
		this.Tab_B.Controls.Add(this.panel1);
		this.Tab_B.Controls.Add(this.panel7);
		this.Tab_B.Controls.Add(this.panel5);
		this.Tab_B.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(593, 572);
		this.panel1.Controls.Add(this.Grid1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 90);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(593, 438);
		this.panel1.TabIndex = 23;
		this.panel7.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel7.Controls.Add(this.D_Btn_Fnsh);
		this.panel7.Controls.Add(this.groupBox4);
		this.panel7.Controls.Add(this.C_Btn_Cncl);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel7.Location = new System.Drawing.Point(0, 528);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(593, 44);
		this.panel7.TabIndex = 22;
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Fnsh.Appearance = appearance3;
		this.D_Btn_Fnsh.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Fnsh.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Fnsh.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Fnsh.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Fnsh.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Fnsh.Location = new System.Drawing.Point(407, 10);
		this.D_Btn_Fnsh.Name = "D_Btn_Fnsh";
		this.D_Btn_Fnsh.ShowFocusRect = false;
		this.D_Btn_Fnsh.ShowOutline = false;
		this.D_Btn_Fnsh.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Fnsh.SupportThemes = false;
		this.D_Btn_Fnsh.TabIndex = 4;
		this.D_Btn_Fnsh.Text = "下一步";
		this.D_Btn_Fnsh.Click += new System.EventHandler(D_Btn_Fnsh_Click);
		this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox4.Location = new System.Drawing.Point(0, 0);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(593, 8);
		this.groupBox4.TabIndex = 3;
		this.groupBox4.TabStop = false;
		appearance4.Image = resources.GetObject("appearance4.Image");
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.C_Btn_Cncl.Appearance = appearance4;
		this.C_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.C_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.C_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.C_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.C_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.C_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.C_Btn_Cncl.Location = new System.Drawing.Point(497, 10);
		this.C_Btn_Cncl.Name = "C_Btn_Cncl";
		this.C_Btn_Cncl.ShowFocusRect = false;
		this.C_Btn_Cncl.ShowOutline = false;
		this.C_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.C_Btn_Cncl.SupportThemes = false;
		this.C_Btn_Cncl.TabIndex = 2;
		this.C_Btn_Cncl.Text = "取消";
		this.panel5.BackColor = System.Drawing.Color.White;
		this.panel5.Controls.Add(this.ultraLabel7);
		this.panel5.Controls.Add(this.ultraLabel6);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(593, 90);
		this.panel5.TabIndex = 21;
		appearance5.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance5;
		this.ultraLabel7.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Font = new System.Drawing.Font("新細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel7.Location = new System.Drawing.Point(34, 29);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(547, 55);
		this.ultraLabel7.TabIndex = 3;
		this.ultraLabel7.Text = "請勾選要下載的年月區別資料，建議一次勾選一項。下載的期別若再勾選下載一次會刪除資料庫裡原有同期別的資料。";
		appearance6.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance6;
		this.ultraLabel6.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(12, 8);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 2;
		this.ultraLabel6.Text = "可下載的公共工程價格資料庫年月區別";
		this.Tab_B2.Controls.Add(this.panel8);
		this.Tab_B2.Location = new System.Drawing.Point(0, 0);
		this.Tab_B2.Name = "Tab_B2";
		this.Tab_B2.Size = new System.Drawing.Size(593, 572);
		this.panel8.BackColor = System.Drawing.Color.White;
		this.panel8.Controls.Add(this.panel9);
		this.panel8.Controls.Add(this.ultraLabel8);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel8.Location = new System.Drawing.Point(0, 0);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(593, 572);
		this.panel8.TabIndex = 22;
		this.panel9.Controls.Add(this.ProgressBar2);
		this.panel9.Controls.Add(this.ultraLabel3);
		this.panel9.Controls.Add(this.ProgressBar1);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel9.Location = new System.Drawing.Point(0, 90);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(593, 482);
		this.panel9.TabIndex = 3;
		appearance7.BackColor = System.Drawing.Color.White;
		this.ultraLabel3.Appearance = appearance7;
		this.ultraLabel3.BackColor = System.Drawing.Color.White;
		this.ultraLabel3.Font = new System.Drawing.Font("新細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel3.Location = new System.Drawing.Point(23, 17);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(360, 39);
		this.ultraLabel3.TabIndex = 5;
		this.ultraLabel3.Text = "因為透過WebService來傳送資料，依每月資料量的多寡所需時間會不同，請耐心等候。";
		this.ProgressBar1.Location = new System.Drawing.Point(20, 184);
		this.ProgressBar1.Name = "ProgressBar1";
		this.ProgressBar1.Size = new System.Drawing.Size(553, 23);
		this.ProgressBar1.TabIndex = 4;
		this.ProgressBar1.Text = "[Formatted]";
		appearance8.BackColor = System.Drawing.Color.White;
		this.ultraLabel8.Appearance = appearance8;
		this.ultraLabel8.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel8.Location = new System.Drawing.Point(12, 8);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel8.TabIndex = 2;
		this.ultraLabel8.Text = "公共工程價格資料庫下載中...";
		this.Tab_C.Controls.Add(this.panel6);
		this.Tab_C.Controls.Add(this.panel4);
		this.Tab_C.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_C.Name = "Tab_C";
		this.Tab_C.Size = new System.Drawing.Size(593, 572);
		this.panel6.BackColor = System.Drawing.Color.White;
		this.panel6.Controls.Add(this.ultraLabel4);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel6.Location = new System.Drawing.Point(0, 0);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(593, 528);
		this.panel6.TabIndex = 24;
		appearance9.BackColor = System.Drawing.Color.White;
		this.ultraLabel4.Appearance = appearance9;
		this.ultraLabel4.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel4.Location = new System.Drawing.Point(64, 48);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(360, 20);
		this.ultraLabel4.TabIndex = 2;
		this.ultraLabel4.Text = "下載完成";
		this.panel4.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel4.Controls.Add(this.ultraButton1);
		this.panel4.Controls.Add(this.groupBox2);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(0, 528);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(593, 44);
		this.panel4.TabIndex = 23;
		appearance10.Image = resources.GetObject("appearance10.Image");
		appearance10.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton1.Appearance = appearance10;
		this.ultraButton1.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.ultraButton1.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton1.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton1.Location = new System.Drawing.Point(407, 10);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(88, 31);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 4;
		this.ultraButton1.Text = "確定";
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(593, 8);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_A);
		this.Tab_Ctrl.Controls.Add(this.Tab_B);
		this.Tab_Ctrl.Controls.Add(this.Tab_C);
		this.Tab_Ctrl.Controls.Add(this.Tab_B2);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(593, 572);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabIndex = 1;
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "tab1";
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "tab2";
		ultraTab3.TabPage = this.Tab_B2;
		ultraTab3.Text = "tab4";
		ultraTab4.TabPage = this.Tab_C;
		ultraTab4.Text = "tab3";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[4] { ultraTab1, ultraTab2, ultraTab3, ultraTab4 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(593, 572);
		this.ProgressBar2.Location = new System.Drawing.Point(19, 230);
		this.ProgressBar2.Name = "ProgressBar2";
		this.ProgressBar2.Size = new System.Drawing.Size(553, 23);
		this.ProgressBar2.TabIndex = 6;
		this.ProgressBar2.Text = "[Formatted]";
		this.Grid1._ExcelFileName = "";
		this.Grid1._ExcelSheeName = "";
		this.Grid1._IsOpenExcelAfterExport = false;
		this.Grid1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.Grid1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.Grid1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.Grid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.Grid1.ColumnInfo = resources.GetString("Grid1.ColumnInfo");
		this.Grid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Grid1.ExtendLastCol = true;
		this.Grid1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Grid1.ForeColor = System.Drawing.Color.Black;
		this.Grid1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.Grid1.IsProcessUndo = false;
		this.Grid1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.Grid1.Location = new System.Drawing.Point(0, 0);
		this.Grid1.Name = "Grid1";
		this.Grid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.Grid1.ShowCursor = true;
		this.Grid1.ShowToolTipOnNarrowColumn = true;
		this.Grid1.Size = new System.Drawing.Size(593, 438);
		this.Grid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("Grid1.Styles"));
		this.Grid1.TabIndex = 8;
		this.Grid1.UndoMax = 10;
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(593, 572);
		base.Controls.Add(this.Tab_Ctrl);
		base.Name = "FormConCost_Upd";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "公共工程價格資料庫線上更新";
		base.Load += new System.EventHandler(FormConCost_Upd_Load);
		base.Activated += new System.EventHandler(FormConCost_Upd_Activated);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormConCost_Upd_FormClosing);
		this.Tab_A.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox1).EndInit();
		this.panel2.ResumeLayout(false);
		this.Tab_B.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		this.Tab_B2.ResumeLayout(false);
		this.panel8.ResumeLayout(false);
		this.panel9.ResumeLayout(false);
		this.Tab_C.ResumeLayout(false);
		this.panel6.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).EndInit();
		this.Tab_Ctrl.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Grid1).EndInit();
		base.ResumeLayout(false);
	}

	private void FormConCost_Upd_Activated(object sender, EventArgs e)
	{
		if (FORM_STATUS == "INI")
		{
			StopForAWhile(10);
			GetDataFromWebService1();
			Tab_B.Tab.Selected = true;
			FORM_STATUS = "NOR";
		}
	}

	private void StopForAWhile(int LoopTimes)
	{
		for (int i = 0; i < LoopTimes; i++)
		{
			Thread.Sleep(100);
			Application.DoEvents();
		}
	}

	private void GetDataFromWebService1()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add("PccAdmin");
		tmp_AL1.Add("營建物價--線上更新");
		Cesprice CesPriceCom = new Cesprice(tmp_AL1);
		DataTable dt = CesPriceCom.ListGroup();
		Update Upd = new Update();
		DataTable dt2 = Upd.GetPubPriceVolumes().Tables[0];
		DataView dv = dt.DefaultView;
		dv.Sort = "cstr";
		Grid1.Rows.Count = dt2.Rows.Count + 1;
		for (int i = 0; i < dt2.Rows.Count; i++)
		{
			string tmpYear = dt2.Rows[i]["years"].ToString().Trim();
			string tmpMonth = dt2.Rows[i]["months"].ToString().Trim();
			string tmpLocation = dt2.Rows[i]["location"].ToString().Trim();
			string tmpKindName = dt2.Rows[i]["KindName"].ToString().Trim();
			string tmpstr = tmpYear + tmpMonth + tmpLocation + tmpKindName;
			string ls_cstr1 = tmpYear + "年" + tmpMonth + "月  " + tmpLocation;
			ls_cstr1 = ((!(tmpLocation == "離")) ? (ls_cstr1 + "區 " + tmpKindName) : (ls_cstr1 + "島 " + tmpKindName));
			if (dv.Find(tmpstr) > -1)
			{
				ls_cstr1 += "(已下載過)";
			}
			Grid1[i + 1, "Check"] = false;
			Grid1[i + 1, "CValue"] = tmpstr;
			Grid1[i + 1, "CString"] = ls_cstr1;
			Grid1[i + 1, "years"] = tmpYear;
			Grid1[i + 1, "months"] = tmpMonth;
			Grid1[i + 1, "location"] = tmpLocation;
			Application.DoEvents();
		}
	}

	private void FormConCost_Upd_Load(object sender, EventArgs e)
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add("PccAdmin");
		tmp_AL1.Add("營建物價--線上更新");
		CesPriceCom = new Cesprice(tmp_AL1);
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("FormConCost_Upd", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("FormConCost_Upd", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("FormConCost_Upd", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("FormConCost_Upd", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("FormConCost_Upd", "PK_Height"));
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
	}

	private void D_Btn_Fnsh_Click(object sender, EventArgs e)
	{
		int iCheckCount = 0;
		for (int i = 1; i < Grid1.Rows.Count; i++)
		{
			if ((bool)Grid1[i, "Check"])
			{
				iCheckCount++;
			}
		}
		if (iCheckCount == 0)
		{
			MessageBox.Show(this, "請先勾選要更新的項目", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		Tab_B2.Tab.Selected = true;
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		ProgressBar1.Minimum = 0;
		ProgressBar1.Maximum = iCheckCount;
		ProgressBar1.Value = 0;
		Update Upd = new Update();
		string ConnKey = "13139409" + $"{DateTime.Now:yyyyMMdd}";
		for (int i = 1; i < Grid1.Rows.Count; i++)
		{
			if ((bool)Grid1[i, "Check"])
			{
				DataTable dt2 = Upd.GetPubPriceDataset(Grid1[i, "years"].ToString(), Grid1[i, "months"].ToString(), Grid1[i, "location"].ToString()).Tables[0];
				Thread newThread = new Thread(ExecInputXML);
				newThread.Start(dt2);
				Thread.Sleep(2000);
				while (CesPriceCom._CurrentRecordIndex < CesPriceCom._TotalRecords)
				{
					ProgressBar2.Minimum = 0;
					ProgressBar2.Maximum = CesPriceCom._TotalRecords;
					ProgressBar2.Value = CesPriceCom._CurrentRecordIndex;
					Application.DoEvents();
				}
				ProgressBar1.Value++;
				Application.DoEvents();
			}
		}
		Tab_C.Tab.Selected = true;
		Cursor = Cursors.Default;
	}

	private void GetProgressInfo()
	{
		while (CesPriceCom._CurrentRecordIndex > CesPriceCom._TotalRecords)
		{
			ProgressBar2.Minimum = 0;
			ProgressBar2.Maximum = CesPriceCom._TotalRecords;
			ProgressBar2.Value = CesPriceCom._CurrentRecordIndex;
		}
	}

	private void ExecInputXML(object DT)
	{
		CesPriceCom.InputXml(DT as DataTable);
	}

	private void FormConCost_Upd_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("FormConCost_Upd", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("FormConCost_Upd", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("FormConCost_Upd", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("FormConCost_Upd", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("FormConCost_Upd", "WindowState", base.WindowState.ToString());
	}
}
