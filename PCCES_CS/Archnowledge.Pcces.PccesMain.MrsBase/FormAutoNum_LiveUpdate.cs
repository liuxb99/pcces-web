using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.PccesUpdateServices;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using C1.Win.C1Input;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.MrsBase;

public class FormAutoNum_LiveUpdate : Form
{
	private const string CallFormHelp = "FormAutoNum_LiveUpdate";

	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private Panel panel1;

	private Panel panel7;

	private UltraButton D_Btn_Fnsh;

	private GroupBox groupBox4;

	private UltraButton C_Btn_Cncl;

	private Panel panel5;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel6;

	private Panel panel2;

	private UltraLabel ultraLabel2;

	private Panel panel3;

	private GroupBox groupBox1;

	private UltraLabel ultraLabel1;

	private C1PictureBox c1PictureBox1;

	private Panel panel4;

	private UltraButton ultraButton1;

	private GroupBox groupBox2;

	private Panel panel6;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private UltraTabPageControl Tab_A;

	private UltraTabPageControl Tab_B;

	private UltraTabPageControl Tab_C;

	public GridMrsBase GridChapter;

	private UltraTabPageControl Tab_B2;

	private Panel panel8;

	private UltraLabel ultraLabel8;

	private ImageList imageList1;

	public GridMrsBase GridProcess;

	private UltraStatusBar StatusBar1;

	private IContainer components;

	private string F_FORM_STATUS = "INI";

	private bool F_IsCustomAutoNum = false;

	private bool F_IsCustomEdit = false;

	private string F_AutoDeptID = "";

	public bool _IsCustomAutoNum
	{
		get
		{
			return F_IsCustomAutoNum;
		}
		set
		{
			F_IsCustomAutoNum = value;
		}
	}

	public bool _IsCustomEdit
	{
		get
		{
			return F_IsCustomEdit;
		}
		set
		{
			F_IsCustomEdit = value;
		}
	}

	public string _AutoDeptID
	{
		get
		{
			return F_AutoDeptID;
		}
		set
		{
			F_AutoDeptID = value;
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.MrsBase.FormAutoNum_LiveUpdate));
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
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
		this.GridChapter = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.panel7 = new System.Windows.Forms.Panel();
		this.D_Btn_Fnsh = new Infragistics.Win.Misc.UltraButton();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.C_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_B2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.GridProcess = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.StatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.panel8 = new System.Windows.Forms.Panel();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_C = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel6 = new System.Windows.Forms.Panel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.Tab_A.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox1).BeginInit();
		this.panel2.SuspendLayout();
		this.Tab_B.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridChapter).BeginInit();
		this.panel7.SuspendLayout();
		this.panel5.SuspendLayout();
		this.Tab_B2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridProcess).BeginInit();
		this.panel8.SuspendLayout();
		this.Tab_C.SuspendLayout();
		this.panel6.SuspendLayout();
		this.panel4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.panel3);
		this.Tab_A.Controls.Add(this.panel2);
		this.Tab_A.Location = new System.Drawing.Point(0, 0);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(448, 326);
		this.panel3.BackColor = System.Drawing.Color.White;
		this.panel3.Controls.Add(this.c1PictureBox1);
		this.panel3.Controls.Add(this.ultraLabel1);
		this.panel3.Controls.Add(this.groupBox1);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 60);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(448, 266);
		this.panel3.TabIndex = 23;
		this.c1PictureBox1.Image = (System.Drawing.Image)resources.GetObject("c1PictureBox1.Image");
		this.c1PictureBox1.Location = new System.Drawing.Point(116, 64);
		this.c1PictureBox1.Name = "c1PictureBox1";
		this.c1PictureBox1.Size = new System.Drawing.Size(200, 160);
		this.c1PictureBox1.TabIndex = 5;
		this.c1PictureBox1.TabStop = false;
		appearance1.TextHAlign = Infragistics.Win.HAlign.Center;
		this.ultraLabel1.Appearance = appearance1;
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(12, 32);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(428, 20);
		this.ultraLabel1.TabIndex = 4;
		this.ultraLabel1.Text = "比對中...";
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(448, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.panel2.BackColor = System.Drawing.Color.White;
		this.panel2.Controls.Add(this.ultraLabel2);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(448, 60);
		this.panel2.TabIndex = 22;
		appearance2.BackColor = System.Drawing.Color.White;
		this.ultraLabel2.Appearance = appearance2;
		this.ultraLabel2.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel2.Location = new System.Drawing.Point(16, 12);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel2.TabIndex = 2;
		this.ultraLabel2.Text = "比對線上規則表...";
		this.Tab_B.Controls.Add(this.panel1);
		this.Tab_B.Controls.Add(this.panel7);
		this.Tab_B.Controls.Add(this.panel5);
		this.Tab_B.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(448, 326);
		this.panel1.Controls.Add(this.GridChapter);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 60);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(448, 222);
		this.panel1.TabIndex = 23;
		this.GridChapter._ExcelFileName = "";
		this.GridChapter._ExcelSheeName = "";
		this.GridChapter._IsOpenExcelAfterExport = false;
		this.GridChapter.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.GridChapter.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.GridChapter.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridChapter.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.GridChapter.ColumnInfo = resources.GetString("GridChapter.ColumnInfo");
		this.GridChapter.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridChapter.ExtendLastCol = true;
		this.GridChapter.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridChapter.ForeColor = System.Drawing.Color.Black;
		this.GridChapter.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.GridChapter.IsProcessUndo = false;
		this.GridChapter.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.GridChapter.Location = new System.Drawing.Point(0, 0);
		this.GridChapter.Name = "GridChapter";
		this.GridChapter.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.GridChapter.ShowCursor = true;
		this.GridChapter.ShowToolTipOnNarrowColumn = true;
		this.GridChapter.Size = new System.Drawing.Size(448, 222);
		this.GridChapter.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("GridChapter.Styles"));
		this.GridChapter.TabIndex = 8;
		this.GridChapter.UndoMax = 10;
		this.GridChapter.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(GridChapter_AfterEdit);
		this.panel7.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel7.Controls.Add(this.D_Btn_Fnsh);
		this.panel7.Controls.Add(this.groupBox4);
		this.panel7.Controls.Add(this.C_Btn_Cncl);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel7.Location = new System.Drawing.Point(0, 282);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(448, 44);
		this.panel7.TabIndex = 22;
		this.D_Btn_Fnsh.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Fnsh.Appearance = appearance3;
		this.D_Btn_Fnsh.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Fnsh.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Fnsh.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Fnsh.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Fnsh.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Fnsh.Location = new System.Drawing.Point(262, 9);
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
		this.groupBox4.Size = new System.Drawing.Size(448, 8);
		this.groupBox4.TabIndex = 3;
		this.groupBox4.TabStop = false;
		this.C_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		appearance4.Image = resources.GetObject("appearance4.Image");
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.C_Btn_Cncl.Appearance = appearance4;
		this.C_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.C_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.C_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.C_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.C_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.C_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.C_Btn_Cncl.Location = new System.Drawing.Point(352, 9);
		this.C_Btn_Cncl.Name = "C_Btn_Cncl";
		this.C_Btn_Cncl.ShowFocusRect = false;
		this.C_Btn_Cncl.ShowOutline = false;
		this.C_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.C_Btn_Cncl.SupportThemes = false;
		this.C_Btn_Cncl.TabIndex = 2;
		this.C_Btn_Cncl.Text = "取消";
		this.C_Btn_Cncl.Click += new System.EventHandler(C_Btn_Cncl_Click);
		this.panel5.BackColor = System.Drawing.Color.White;
		this.panel5.Controls.Add(this.ultraLabel7);
		this.panel5.Controls.Add(this.ultraLabel6);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(448, 60);
		this.panel5.TabIndex = 21;
		appearance5.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance5;
		this.ultraLabel7.Location = new System.Drawing.Point(45, 29);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(360, 20);
		this.ultraLabel7.TabIndex = 3;
		this.ultraLabel7.Text = "比對你的資料庫之後，尚有下列章碼的規則表需要更新";
		appearance6.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance6;
		this.ultraLabel6.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(12, 8);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 2;
		this.ultraLabel6.Text = "可下載的規則表";
		this.Tab_B2.Controls.Add(this.GridProcess);
		this.Tab_B2.Controls.Add(this.StatusBar1);
		this.Tab_B2.Controls.Add(this.panel8);
		this.Tab_B2.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B2.Name = "Tab_B2";
		this.Tab_B2.Size = new System.Drawing.Size(448, 326);
		this.GridProcess._ExcelFileName = "";
		this.GridProcess._ExcelSheeName = "";
		this.GridProcess._IsOpenExcelAfterExport = false;
		this.GridProcess.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.GridProcess.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.GridProcess.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridProcess.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.GridProcess.ColumnInfo = resources.GetString("GridProcess.ColumnInfo");
		this.GridProcess.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridProcess.ExtendLastCol = true;
		this.GridProcess.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridProcess.ForeColor = System.Drawing.Color.Black;
		this.GridProcess.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.GridProcess.IsProcessUndo = false;
		this.GridProcess.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.GridProcess.Location = new System.Drawing.Point(0, 60);
		this.GridProcess.Name = "GridProcess";
		this.GridProcess.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.GridProcess.ShowCursor = true;
		this.GridProcess.ShowToolTipOnNarrowColumn = true;
		this.GridProcess.Size = new System.Drawing.Size(448, 234);
		this.GridProcess.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("GridProcess.Styles"));
		this.GridProcess.TabIndex = 23;
		this.GridProcess.UndoMax = 10;
		this.StatusBar1.Location = new System.Drawing.Point(0, 294);
		this.StatusBar1.Name = "StatusBar1";
		this.StatusBar1.Padding = new Infragistics.Win.UltraWinStatusBar.UIElementMargins(0, 2, 0, 0);
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Width = 250;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		this.StatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[2] { ultraStatusPanel1, ultraStatusPanel2 });
		this.StatusBar1.Size = new System.Drawing.Size(448, 32);
		this.StatusBar1.SupportThemes = false;
		this.StatusBar1.TabIndex = 24;
		this.StatusBar1.Text = "ultraStatusBar1";
		this.panel8.BackColor = System.Drawing.Color.White;
		this.panel8.Controls.Add(this.ultraLabel8);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel8.Location = new System.Drawing.Point(0, 0);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(448, 60);
		this.panel8.TabIndex = 22;
		appearance7.BackColor = System.Drawing.Color.White;
		this.ultraLabel8.Appearance = appearance7;
		this.ultraLabel8.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel8.Location = new System.Drawing.Point(12, 8);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel8.TabIndex = 2;
		this.ultraLabel8.Text = "規則表更新中...";
		this.Tab_C.Controls.Add(this.panel6);
		this.Tab_C.Controls.Add(this.panel4);
		this.Tab_C.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_C.Name = "Tab_C";
		this.Tab_C.Size = new System.Drawing.Size(448, 326);
		this.panel6.BackColor = System.Drawing.Color.White;
		this.panel6.Controls.Add(this.ultraLabel3);
		this.panel6.Controls.Add(this.ultraLabel4);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel6.Location = new System.Drawing.Point(0, 0);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(448, 282);
		this.panel6.TabIndex = 24;
		appearance8.BackColor = System.Drawing.Color.White;
		this.ultraLabel3.Appearance = appearance8;
		this.ultraLabel3.Location = new System.Drawing.Point(48, 48);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(376, 20);
		this.ultraLabel3.TabIndex = 3;
		this.ultraLabel3.Text = "比對你的資料庫之後，無需更新。";
		appearance9.BackColor = System.Drawing.Color.White;
		this.ultraLabel4.Appearance = appearance9;
		this.ultraLabel4.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel4.Location = new System.Drawing.Point(16, 16);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel4.TabIndex = 2;
		this.ultraLabel4.Text = "比對結果";
		this.panel4.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel4.Controls.Add(this.ultraButton1);
		this.panel4.Controls.Add(this.groupBox2);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(0, 282);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(448, 44);
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
		this.ultraButton1.Location = new System.Drawing.Point(352, 9);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(88, 31);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 4;
		this.ultraButton1.Text = "確定";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(448, 8);
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
		this.Tab_Ctrl.Size = new System.Drawing.Size(448, 326);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabIndex = 0;
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
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(448, 326);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.White;
		this.imageList1.Images.SetKeyName(0, "");
		this.imageList1.Images.SetKeyName(1, "");
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 18);
		base.ClientSize = new System.Drawing.Size(448, 326);
		base.Controls.Add(this.Tab_Ctrl);
		this.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormAutoNum_LiveUpdate";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "規則表線上更新";
		base.Load += new System.EventHandler(FormAutoNum_LiveUpdate_Load);
		base.Activated += new System.EventHandler(FormAutoNum_LiveUpdate_Activated);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormAutoNum_LiveUpdate_FormClosing);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormAutoNum_LiveUpdate_KeyDown);
		this.Tab_A.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox1).EndInit();
		this.panel2.ResumeLayout(false);
		this.Tab_B.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridChapter).EndInit();
		this.panel7.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		this.Tab_B2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridProcess).EndInit();
		this.panel8.ResumeLayout(false);
		this.Tab_C.ResumeLayout(false);
		this.panel6.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).EndInit();
		this.Tab_Ctrl.ResumeLayout(false);
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

	public FormAutoNum_LiveUpdate()
	{
		InitializeComponent();
		GridProcess.Glyphs[GlyphEnum.Checked] = imageList1.Images[0];
		GridProcess.Glyphs[GlyphEnum.Unchecked] = imageList1.Images[1];
		HideCols(IsHide: true);
		GridChapter.SetCellCheck(0, 1, CheckEnum.Checked);
		GridChapter.SetData(0, 1, "勾選", coerce: false);
	}

	private void HideCols(bool IsHide)
	{
		if (IsHide)
		{
			GridChapter.Cols["ActionID"].Visible = false;
			GridChapter.Cols["ReleaseDate"].Visible = false;
			GridProcess.Cols["ActionID"].Visible = false;
		}
	}

	private void FormAutoNum_LiveUpdate_Load(object sender, EventArgs e)
	{
		LoadingScreen();
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("AutoNum_LibeUpdate", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("AutoNum_LibeUpdate", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("AutoNum_LibeUpdate", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("AutoNum_LibeUpdate", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("AutoNum_LibeUpdate", "PK_Height"));
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

	private void FormAutoNum_LiveUpdate_Activated(object sender, EventArgs e)
	{
		if (F_FORM_STATUS != "ACT")
		{
			StopForAWhile(20);
			CompareData();
			F_FORM_STATUS = "ACT";
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

	private void CompareData()
	{
		Cursor = Cursors.WaitCursor;
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
		DS11 = (F_IsCustomAutoNum ? serviceRequest.AutoNumUpd2(F_AutoDeptID) : serviceRequest.AutoNumUpd());
		DataSet DSList = DS11.Clone();
		DBClass DBCLS = new DBClass();
		DataTable DT1 = DBCLS.GetUserDefine("Select * from AutoNumUpd Order By ItemCode Asc, ReleaseDate Desc");
		DT1.CaseSensitive = true;
		for (int i = 0; i < DS11.Tables[0].Rows.Count; i++)
		{
			DataView DV33 = DT1.DefaultView;
			DateTime DD1 = Convert.ToDateTime(DS11.Tables[0].Rows[i]["ReleaseDate"]);
			string sDate = DD1.Month + "/" + DD1.Day + "/" + DD1.Year;
			string sFLT = "";
			if (DS11.Tables[0].Rows[i]["ActionID"].ToString().Trim() == "")
			{
				sFLT = "ItemCode ='" + DS11.Tables[0].Rows[i]["ItemCode"].ToString().Trim() + "' And ReleaseDate >= #" + sDate + "# ";
			}
			else if (DS11.Tables[0].Rows[i]["ActionID"].ToString().Trim() == "B")
			{
				sFLT = "ItemCode ='" + DS11.Tables[0].Rows[i]["ItemCode"].ToString().Trim() + "' And ReleaseDate >= #" + sDate + "#   And ActionID='" + DS11.Tables[0].Rows[i]["ActionID"].ToString().Trim() + "' ";
			}
			else if (DS11.Tables[0].Rows[i]["ActionID"].ToString().Trim() == "D")
			{
				sFLT = "ItemCode ='" + DS11.Tables[0].Rows[i]["ItemCode"].ToString().Trim() + "' And ReleaseDate >= #" + sDate + "# ";
			}
			DV33.RowFilter = sFLT;
			if (DV33.Count == 0)
			{
				DataRow DR33 = DSList.Tables[0].NewRow();
				for (int j = 0; j < DS11.Tables[0].Columns.Count; j++)
				{
					DR33[DS11.Tables[0].Columns[j].ColumnName] = DS11.Tables[0].Rows[i][DS11.Tables[0].Columns[j].ColumnName];
				}
				DSList.Tables[0].Rows.Add(DR33);
			}
		}
		Application.DoEvents();
		if (DSList.Tables[0].Rows.Count > 0)
		{
			BindToGrid1(DSList.Tables[0]);
			Tab_B.Tab.Selected = true;
		}
		else
		{
			Tab_C.Tab.Selected = true;
		}
		Cursor = Cursors.Default;
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

	private void BindToGrid1(DataTable DT1)
	{
		GridChapter.Rows.Count = DT1.Rows.Count + 1;
		CellStyle CS1 = GridChapter.Styles.Add("RedColor");
		CS1.ForeColor = Color.Red;
		CellStyle CSDelete = GridChapter.Styles.Add("Delete");
		CSDelete.Font = new Font("細明體", 11.25f, FontStyle.Strikeout, GraphicsUnit.Point, 136);
		CSDelete.ForeColor = Color.Red;
		CSDelete.BackColor = Color.LightGray;
		for (int i = 1; i < GridChapter.Rows.Count; i++)
		{
			GridChapter[i, "Check"] = true;
			GridChapter[i, "ActionID"] = DT1.Rows[i - 1]["ActionID"].ToString().Trim();
			GridChapter[i, "itemCode"] = DT1.Rows[i - 1]["itemCode"].ToString().Trim();
			GridChapter[i, "ChapterName"] = DT1.Rows[i - 1]["ChapterName"].ToString().Trim();
			GridChapter[i, "ReleaseDate"] = DT1.Rows[i - 1]["ReleaseDate"].ToString().Trim();
			GridChapter[i, "ChangeCode"] = DT1.Rows[i - 1]["ChangeCode"].ToString().Trim();
			if (GridChapter[i, "ActionID"].ToString() == "D")
			{
				GridChapter.Rows[i].AllowEditing = false;
				GridChapter.Rows[i].Style = CSDelete;
			}
			if (DT1.Rows[i - 1]["Version"].ToString() != "0")
			{
				GridChapter.Rows[i].Style = CS1;
			}
			Application.DoEvents();
		}
	}

	private void D_Btn_Fnsh_Click(object sender, EventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		int iCount = 0;
		for (int i = 1; i < GridChapter.Rows.Count; i++)
		{
			if (GridChapter[i, "Check"] != null && (bool)GridChapter[i, "Check"])
			{
				iCount++;
			}
		}
		if (iCount == 0)
		{
			MessageBox.Show(this, "請先勾選要更新的章碼", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}
		BindToGrid2(iCount);
		Tab_B2.Tab.Selected = true;
		Retrieve();
		Cursor = Cursors.Default;
	}

	private void BindToGrid2(int RowsCount)
	{
		GridProcess.Rows.Count = RowsCount + 1;
		int iIndicator = 1;
		for (int i = 1; i < GridChapter.Rows.Count; i++)
		{
			if ((bool)GridChapter[i, "Check"])
			{
				GridProcess[iIndicator, "ActionID"] = GridChapter[i, "ActionID"].ToString().Trim();
				GridProcess[iIndicator, "itemCode"] = GridChapter[i, "itemCode"].ToString().Trim();
				GridProcess[iIndicator, "ChapterName"] = GridChapter[i, "ChapterName"].ToString().Trim();
				GridProcess[iIndicator, "ChangeCode"] = GridChapter[i, "ChangeCode"].ToString().Trim();
				iIndicator++;
			}
		}
		StatusBar1.Panels[1].Text = "勾選總筆數:" + RowsCount;
	}

	private void Retrieve()
	{
		int iSuccess = 0;
		int iFail = 0;
		StatusBar1.Panels[0].Text = "更新成功: " + iSuccess + "筆";
		for (int i = 1; i < GridProcess.Rows.Count; i++)
		{
			Application.DoEvents();
			if (ProcessDone(GridProcess[i, "itemCode"].ToString().Trim(), GridProcess[i, "ActionID"].ToString().Trim(), GridProcess[i, "ChangeCode"].ToString().Trim()))
			{
				GridProcess[i, "Check"] = true;
				iSuccess++;
				StatusBar1.Panels[0].Text = "更新成功: " + iSuccess + "筆";
				GridProcess.Row = i;
			}
			else
			{
				iFail++;
			}
			Application.DoEvents();
		}
		string sMess = "【 自動編碼規則表 】線上更新完畢\n\n";
		if (iSuccess > 0)
		{
			sMess = sMess + "成功:" + iSuccess + "筆\n";
		}
		if (iFail > 0)
		{
			sMess = sMess + "失敗:" + iFail + "筆";
		}
		MessageBox.Show(this, sMess, "完成", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		Close();
	}

	private bool ProcessDone(string itemCode, string newActionID, string changeCode)
	{
		Update serviceRequest = new Update();
		string webServiceRoute = CommonMethods.GetIniValue("DownloadInfo", "webServiceRoute");
		if (webServiceRoute == "")
		{
			webServiceRoute = "http://bisc.archnowledge.com/pccesupdateservices/Update.asmx";
		}
		serviceRequest.Timeout = 180000;
		serviceRequest.Url = webServiceRoute;
		if (CommonMethods.GetIniValue("ProxyInfo", "usingProxy").Trim().ToLower() == "true")
		{
			serviceRequest.Proxy = GetProxy();
		}
		DataSet DS_AB = serviceRequest.GetAutoNumAB(itemCode);
		if (itemCode.Length == 2 || itemCode.Length == 4)
		{
			DS_AB = serviceRequest.GetAutoNumAB_12(itemCode);
			if (itemCode == "0000" && DS_AB.Tables["AutoNumA"].Rows.Count == 0)
			{
				DataRow DR = DS_AB.Tables["AutoNumA"].NewRow();
				DR["itemCode"] = "0000";
				DR["levelNo"] = 2;
				DR["cName"] = "人力規則表";
				DR["IsShow"] = "";
				DR["parent"] = "L";
				DR["WinFormFlag"] = "2";
				DR["Ext"] = "12";
				DS_AB.Tables["AutoNumA"].Rows.Add(DR);
				if (DS_AB.Tables["AutoNumUpd"].Rows.Count > 0)
				{
					DS_AB.Tables["AutoNumUpd"].Rows[0]["changeCode"] = "12";
				}
			}
		}
		DBClass DBCLS = new DBClass();
		return DBCLS.UpdateNew_AutoNum(itemCode, DS_AB, newActionID, changeCode);
	}

	private void FormAutoNum_LiveUpdate_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("AutoNum_LibeUpdate", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("AutoNum_LibeUpdate", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("AutoNum_LibeUpdate", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("AutoNum_LibeUpdate", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("AutoNum_LibeUpdate", "WindowState", base.WindowState.ToString());
	}

	private void FormAutoNum_LiveUpdate_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormAutoNum_LiveUpdate");
		}
	}

	private void GridChapter_AfterEdit(object sender, RowColEventArgs e)
	{
		if (e.Row == 0 && e.Col == 1)
		{
			CheckEnum CheckStatus = GridChapter.GetCellCheck(e.Row, e.Col);
			for (int i = 1; i < GridChapter.Rows.Count; i++)
			{
				if (GridChapter[i, "ActionID"].ToString() != "D")
				{
					GridChapter.SetCellCheck(i, 1, CheckStatus);
				}
			}
			return;
		}
		for (int i = 1; i < GridChapter.Rows.Count; i++)
		{
			if (!(bool)GridChapter[i, "Check"])
			{
				GridChapter.SetCellCheck(0, 1, CheckEnum.Unchecked);
				return;
			}
		}
		GridChapter.SetCellCheck(0, 1, CheckEnum.Checked);
	}

	private void C_Btn_Cncl_Click(object sender, EventArgs e)
	{
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
	}
}
