using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.PccesUpdateServices;
using Archnowledge.Pcces.PccesMain.Report.WebDownload;
using Archnowledge.Pcces.REPClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using C1.Win.C1Input;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinProgressBar;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.Report;

public class FormInvReportCheck : Form
{
	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_A;

	private UltraTabPageControl Tab_B;

	private Panel panel3;

	private C1PictureBox c1PictureBox1;

	private UltraLabel ultraLabel1;

	private GroupBox groupBox1;

	private Panel panel2;

	private UltraLabel ultraLabel2;

	private Panel panel7;

	private GroupBox groupBox4;

	private IContainer components;

	private Panel panel5;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel6;

	public GridMrsBase Grid1;

	private UltraButton B_Btn_Next;

	private UltraButton B_Btn_Cncl;

	private UltraTabPageControl Tab_C;

	private UltraTabPageControl Tab_D;

	private UltraLabel ultraLabel3;

	private UltraProgressBar ProgressBar1;

	private string FORM_STATUS = "INI";

	private string F_ReportKind;

	private DataTable DT1 = new DataTable();

	private Panel panel1;

	private GroupBox groupBox2;

	private UltraButton ultraButton2;

	private Panel panel4;

	private UltraLabel lbl_Message;

	private UltraLabel ultraLabel12;

	private string F_UserID;

	public string _ReportKind
	{
		get
		{
			return F_ReportKind;
		}
		set
		{
			F_ReportKind = value;
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

	public FormInvReportCheck()
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
		this.components = new System.ComponentModel.Container();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Report.FormInvReportCheck));
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
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
		this.Grid1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.panel7 = new System.Windows.Forms.Panel();
		this.B_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.B_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.Tab_C = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ProgressBar1 = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
		this.Tab_D = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel4 = new System.Windows.Forms.Panel();
		this.lbl_Message = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.Tab_A.SuspendLayout();
		this.panel3.SuspendLayout();
		this.panel2.SuspendLayout();
		this.Tab_B.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Grid1).BeginInit();
		this.panel5.SuspendLayout();
		this.panel7.SuspendLayout();
		this.Tab_C.SuspendLayout();
		this.Tab_D.SuspendLayout();
		this.panel4.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.panel3);
		this.Tab_A.Controls.Add(this.panel2);
		this.Tab_A.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(584, 341);
		this.panel3.BackColor = System.Drawing.Color.White;
		this.panel3.Controls.Add(this.c1PictureBox1);
		this.panel3.Controls.Add(this.ultraLabel1);
		this.panel3.Controls.Add(this.groupBox1);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 60);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(584, 281);
		this.panel3.TabIndex = 25;
		this.c1PictureBox1.Image = (System.Drawing.Image)resources.GetObject("c1PictureBox1.Image");
		this.c1PictureBox1.Location = new System.Drawing.Point(189, 64);
		this.c1PictureBox1.Name = "c1PictureBox1";
		this.c1PictureBox1.Size = new System.Drawing.Size(200, 160);
		this.c1PictureBox1.TabIndex = 5;
		this.c1PictureBox1.TabStop = false;
		appearance1.TextHAlign = Infragistics.Win.HAlign.Center;
		this.ultraLabel1.Appearance = appearance1;
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(12, 32);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(564, 20);
		this.ultraLabel1.TabIndex = 4;
		this.ultraLabel1.Text = "比對中...";
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(584, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.panel2.BackColor = System.Drawing.Color.White;
		this.panel2.Controls.Add(this.ultraLabel2);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(584, 60);
		this.panel2.TabIndex = 24;
		appearance2.BackColor = System.Drawing.Color.White;
		this.ultraLabel2.Appearance = appearance2;
		this.ultraLabel2.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel2.Location = new System.Drawing.Point(16, 12);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel2.TabIndex = 2;
		this.ultraLabel2.Text = "比對線上規則表...";
		this.Tab_B.Controls.Add(this.Grid1);
		this.Tab_B.Controls.Add(this.panel5);
		this.Tab_B.Controls.Add(this.panel7);
		this.Tab_B.Location = new System.Drawing.Point(0, 0);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(584, 341);
		this.Grid1._ExcelFileName = "";
		this.Grid1._ExcelSheeName = "";
		this.Grid1._IsOpenExcelAfterExport = false;
		this.Grid1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.Grid1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.Grid1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.Grid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.Grid1.ColumnInfo = "6,1,0,0,0,110,Columns:0{Width:17;Name:\"RowIndicator\";AllowDragging:False;AllowResizing:False;AllowEditing:False;DataType:System.Int32;TextAlign:RightTop;TextAlignFixed:GeneralTop;}\t1{Width:50;Name:\"Check\";Caption:\"勾選\";DataType:System.Boolean;TextAlign:GeneralBottom;TextAlignFixed:GeneralTop;ImageAlign:CenterCenter;}\t2{Width:200;Name:\"RptDesc\";Caption:\"報表名稱\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t3{Width:250;Name:\"RptTitle\";Caption:\"報表格式\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t4{Name:\"RptZIP\";Caption:\"檔案名稱\";Visible:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t5{Width:300;Name:\"RptURL\";Caption:\"網頁路徑\";Visible:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t";
		this.Grid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Grid1.ExtendLastCol = true;
		this.Grid1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Grid1.ForeColor = System.Drawing.Color.Black;
		this.Grid1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.Grid1.IsProcessUndo = false;
		this.Grid1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.Grid1.Location = new System.Drawing.Point(0, 60);
		this.Grid1.Name = "Grid1";
		this.Grid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.Grid1.ShowCursor = true;
		this.Grid1.ShowToolTipOnNarrowColumn = true;
		this.Grid1.Size = new System.Drawing.Size(584, 237);
		this.Grid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11.25pt;BackColor:237, 243, 254;ForeColor:Black;TextAlign:GeneralBottom;Border:Flat,1,Silver,Both;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;TextAlign:GeneralTop;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;TextAlign:GeneralCenter;ImageAlign:CenterCenter;Border:None,1,Black,Both;Format:\"###,###,###,##0\";}\tFocus{Font:細明體, 10pt, style=Bold;BackColor:102, 153, 255;Margins:0, 0, 0, 0;TextAlign:GeneralCenter;Border:Double,1,102, 153, 255,Both;}\tSearch{BackColor:Highlight;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.Grid1.TabIndex = 25;
		this.Grid1.UndoMax = 10;
		this.panel5.BackColor = System.Drawing.Color.White;
		this.panel5.Controls.Add(this.ultraLabel7);
		this.panel5.Controls.Add(this.ultraLabel6);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(584, 60);
		this.panel5.TabIndex = 24;
		appearance3.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance3;
		this.ultraLabel7.Location = new System.Drawing.Point(45, 29);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(360, 20);
		this.ultraLabel7.TabIndex = 3;
		this.ultraLabel7.Text = "目前有下表中的報表格式可供下載";
		appearance4.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance4;
		this.ultraLabel6.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(12, 8);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 2;
		this.ultraLabel6.Text = "可下載的報表格式";
		this.panel7.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel7.Controls.Add(this.B_Btn_Next);
		this.panel7.Controls.Add(this.groupBox4);
		this.panel7.Controls.Add(this.B_Btn_Cncl);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel7.Location = new System.Drawing.Point(0, 297);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(584, 44);
		this.panel7.TabIndex = 23;
		this.B_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance5.Image = resources.GetObject("appearance5.Image");
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Next.Appearance = appearance5;
		this.B_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Next.Location = new System.Drawing.Point(398, 9);
		this.B_Btn_Next.Name = "B_Btn_Next";
		this.B_Btn_Next.ShowFocusRect = false;
		this.B_Btn_Next.ShowOutline = false;
		this.B_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Next.SupportThemes = false;
		this.B_Btn_Next.TabIndex = 4;
		this.B_Btn_Next.Text = "下一步";
		this.B_Btn_Next.Click += new System.EventHandler(B_Btn_Next_Click);
		this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox4.Location = new System.Drawing.Point(0, 0);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(584, 8);
		this.groupBox4.TabIndex = 3;
		this.groupBox4.TabStop = false;
		this.B_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance6.Image = resources.GetObject("appearance6.Image");
		appearance6.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Cncl.Appearance = appearance6;
		this.B_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.B_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Cncl.Location = new System.Drawing.Point(489, 9);
		this.B_Btn_Cncl.Name = "B_Btn_Cncl";
		this.B_Btn_Cncl.ShowFocusRect = false;
		this.B_Btn_Cncl.ShowOutline = false;
		this.B_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Cncl.SupportThemes = false;
		this.B_Btn_Cncl.TabIndex = 2;
		this.B_Btn_Cncl.Text = "取消";
		this.Tab_C.Controls.Add(this.ultraLabel3);
		this.Tab_C.Controls.Add(this.ProgressBar1);
		this.Tab_C.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_C.Name = "Tab_C";
		this.Tab_C.Size = new System.Drawing.Size(584, 341);
		this.ultraLabel3.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel3.Location = new System.Drawing.Point(24, 56);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(360, 20);
		this.ultraLabel3.TabIndex = 4;
		this.ultraLabel3.Text = "報表封裝檔解壓縮中，請耐心等候...";
		this.ProgressBar1.Location = new System.Drawing.Point(48, 200);
		this.ProgressBar1.Name = "ProgressBar1";
		this.ProgressBar1.Size = new System.Drawing.Size(496, 23);
		this.ProgressBar1.TabIndex = 0;
		this.ProgressBar1.Text = "[Formatted]";
		this.Tab_D.Controls.Add(this.panel4);
		this.Tab_D.Controls.Add(this.panel1);
		this.Tab_D.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_D.Name = "Tab_D";
		this.Tab_D.Size = new System.Drawing.Size(584, 341);
		this.panel4.BackColor = System.Drawing.Color.White;
		this.panel4.Controls.Add(this.lbl_Message);
		this.panel4.Controls.Add(this.ultraLabel12);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel4.Location = new System.Drawing.Point(0, 0);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(584, 297);
		this.panel4.TabIndex = 26;
		appearance7.BackColor = System.Drawing.Color.White;
		this.lbl_Message.Appearance = appearance7;
		this.lbl_Message.Location = new System.Drawing.Point(56, 88);
		this.lbl_Message.Name = "lbl_Message";
		this.lbl_Message.Size = new System.Drawing.Size(408, 20);
		this.lbl_Message.TabIndex = 13;
		this.lbl_Message.Text = "你已經成功建立一個新的專案。";
		appearance8.BackColor = System.Drawing.Color.White;
		this.ultraLabel12.Appearance = appearance8;
		this.ultraLabel12.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel12.Location = new System.Drawing.Point(40, 32);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel12.TabIndex = 12;
		this.ultraLabel12.Text = "恭禧您!";
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.groupBox2);
		this.panel1.Controls.Add(this.ultraButton2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 297);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(584, 44);
		this.panel1.TabIndex = 24;
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(584, 8);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.ultraButton2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance9.Image = resources.GetObject("appearance9.Image");
		appearance9.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton2.Appearance = appearance9;
		this.ultraButton2.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton2.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.ultraButton2.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton2.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton2.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton2.Location = new System.Drawing.Point(489, 9);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(88, 31);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 2;
		this.ultraButton2.Text = "確定";
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_A);
		this.Tab_Ctrl.Controls.Add(this.Tab_B);
		this.Tab_Ctrl.Controls.Add(this.Tab_C);
		this.Tab_Ctrl.Controls.Add(this.Tab_D);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(584, 341);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabIndex = 0;
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "tab1";
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "tab2";
		ultraTab3.TabPage = this.Tab_C;
		ultraTab3.Text = "tab3";
		ultraTab4.TabPage = this.Tab_D;
		ultraTab4.Text = "tab4";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[4] { ultraTab1, ultraTab2, ultraTab3, ultraTab4 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(584, 341);
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.B_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(584, 341);
		base.Controls.Add(this.Tab_Ctrl);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormInvReportCheck";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "計價報表線上更新";
		base.Load += new System.EventHandler(FormInvReportCheck_Load);
		base.Activated += new System.EventHandler(FormInvReportCheck_Activated);
		this.Tab_A.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.Tab_B.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Grid1).EndInit();
		this.panel5.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		this.Tab_C.ResumeLayout(false);
		this.Tab_D.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).EndInit();
		this.Tab_Ctrl.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void FormInvReportCheck_Load(object sender, EventArgs e)
	{
	}

	private void FormInvReportCheck_Activated(object sender, EventArgs e)
	{
		if (FORM_STATUS == "INI")
		{
			StopForAWhile(20);
			GetDataFromWebService();
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

	private void GetDataFromWebService()
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
		DataSet DS11 = serviceRequest.InvReportList(F_ReportKind);
		DT1 = DS11.Tables[0].Copy();
		BindToGrid();
	}

	private void BindToGrid()
	{
		Grid1.Rows.Count = DT1.Rows.Count + 1;
		for (int i = 0; i < DT1.Rows.Count; i++)
		{
			Grid1[i + 1, "Check"] = false;
			Grid1[i + 1, "RptDesc"] = DT1.Rows[i]["RptDesc"].ToString().Trim();
			Grid1[i + 1, "RptTitle"] = DT1.Rows[i]["RptTitle"].ToString().Trim();
			Grid1[i + 1, "RptZIP"] = DT1.Rows[i]["RptZIP"].ToString().Trim();
			Grid1[i + 1, "RptURL"] = DT1.Rows[i]["RptURL"].ToString().Trim();
		}
		if (Grid1.Rows.Count <= 1)
		{
			B_Btn_Next.Enabled = false;
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

	private void B_Btn_Next_Click(object sender, EventArgs e)
	{
		string sExtractPath = CommonMethods.ExtractFilePath(Application.ExecutablePath);
		int iPickCount = 0;
		for (int i = 1; i < Grid1.Rows.Count; i++)
		{
			if ((bool)Grid1.Rows[i]["Check"])
			{
				iPickCount++;
			}
		}
		if (iPickCount == 0)
		{
			MessageBox.Show(this, "請先勾選要下載的報表，再按下一步。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		ProgressBar1.Minimum = 0;
		ProgressBar1.Maximum = iPickCount;
		Tab_C.Tab.Selected = true;
		for (int i = 1; i < Grid1.Rows.Count; i++)
		{
			if ((bool)Grid1.Rows[i]["Check"])
			{
				string sFileName = Grid1.Rows[i]["RptZIP"].ToString();
				DownLoading(Grid1.Rows[i]["RptURL"].ToString(), sExtractPath + sFileName);
				ProgressBar1.Value++;
				Application.DoEvents();
				Thread.Sleep(500);
				Application.DoEvents();
			}
		}
		Tab_D.Tab.Selected = true;
		lbl_Message.Text = "你已經成功下載 " + iPickCount + " 份報表";
	}

	private void DownLoading(string url, string zipPath)
	{
		DownloadThread DnTh1 = new DownloadThread();
		DnTh1.CompleteCallback += DownloadCompleteCallback;
		DnTh1.ProgressCallback += DownloadProgressCallback;
		DnTh1.FailCallback += DownloadFailCallback;
		DnTh1.DownloadUrl = url;
		DnTh1.savePath = zipPath;
		DnTh1.iniPath = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "PccesMain.ini";
		Thread t = new Thread(DnTh1.Download);
		t.Start();
	}

	private void DownloadCompleteCallback(int byteSoFar, int totalBytes, string SavedFile)
	{
		lock (this)
		{
			string strPath = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "Report\\";
			ArrayList tmp_ALaa = new ArrayList();
			tmp_ALaa.Add("insert到pcces資料庫");
			tmp_ALaa.Add("新增報表格式");
			RepListClass RepCom = new RepListClass(tmp_ALaa);
			ExecResult ER = RepCom.AddReport(SavedFile.Trim(), strPath);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show(this, ER.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			try
			{
				FileInfo fi = new FileInfo(SavedFile);
				fi.Delete();
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Report.FormInvReportCheck.cs" + ex.Message);
			}
		}
	}

	private void DownloadProgressCallback(int byteSoFar, int totalBytes)
	{
	}

	private void DownloadFailCallback(Exception error)
	{
		MessageBox.Show(error.Message);
	}
}
