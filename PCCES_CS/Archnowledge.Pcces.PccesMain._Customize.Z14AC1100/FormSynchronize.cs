using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.Railway1;
using Archnowledge.Pcces.STDClass;
using Archnowledge.Pcces.TRAClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinProgressBar;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain._Customize.Z14AC1100;

public class FormSynchronize : Form
{
	private IContainer components;

	private bool IsSyncOK = false;

	private SystemCom SYSCOM;

	private string F_UserID = "";

	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_A;

	private UltraTabPageControl Tab_B;

	private Panel panel9;

	private GroupBox groupBox5;

	private UltraButton A_Btn_Cncl;

	private UltraButton A_Btn_Next;

	private UltraButton A_Btn_Prev;

	private UltraLabel ultraLabel7;

	public GridMrsBase Grid1;

	private UltraTabPageControl Tab_C;

	private Timer timer1;

	private Panel panel1;

	private GroupBox groupBox1;

	private UltraButton ultraButton1;

	private UltraButton ultraButton3;

	private Panel panel2;

	private GroupBox groupBox2;

	private UltraButton ultraButton4;

	private UltraLabel ultraLabel14;

	private UltraLabel ultraLabel13;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel5;

	private UltraLabel lblMessage1;

	private UltraLabel lblMessage2;

	private UltraProgressBar ProgressBar1;

	private UltraProgressBar ProgressBar2;

	private UltraTabPageControl Tab_D;

	private UltraTabPageControl Tab_E;

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

	public FormSynchronize()
	{
		InitializeComponent();
	}

	private void A_Btn_Next_Click(object sender, EventArgs e)
	{
		Tab_C.Tab.Selected = true;
		Application.DoEvents();
		ProcessSync();
		if (IsSyncOK)
		{
			Tab_E.Tab.Selected = true;
		}
		else
		{
			Tab_D.Tab.Selected = true;
		}
	}

	private void ultraButton3_Click(object sender, EventArgs e)
	{
		Tab_A.Tab.Selected = true;
	}

	private void ProcessSync()
	{
		UltraLabel ultraLabel = lblMessage1;
		string text = (lblMessage2.Text = "資料正從SERVER下載中，請稍候!");
		ultraLabel.Text = text;
		Cursor = Cursors.WaitCursor;
		Application.DoEvents();
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		string sConnStr = DBCLS.GetMultiUserConnection(F_UserID);
		Grid1.Redraw = false;
		TRA_Service SRV1 = new TRA_Service();
		SRV1.Url = Archnowledge.Pcces.STDClass.PubTools.GetAppSet_String("TRA_Service_URL");
		DataSet DS_MRS = SRV1.OutputMrs("", Archnowledge.Pcces.STDClass.PubTools.KeyEnc(Archnowledge.Pcces.STDClass.PubTools.GetAppSet_String("PID")));
		Grid1.DataSource = DS_MRS.Tables[0];
		Grid1.Redraw = true;
		Grid1.Visible = true;
		UltraLabel ultraLabel2 = lblMessage1;
		text = (lblMessage2.Text = "資料正在同步中，請稍候!");
		ultraLabel2.Text = text;
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		timer1.Enabled = true;
		SYSCOM = new SystemCom();
		IsSyncOK = SYSCOM.SynchMrs(sConnStr, DS_MRS);
		timer1.Enabled = false;
		Cursor = Cursors.Default;
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		try
		{
			UltraProgressBar progressBar = ProgressBar1;
			int maximum = (ProgressBar2.Maximum = SYSCOM._TotalRows);
			progressBar.Maximum = maximum;
			UltraProgressBar progressBar2 = ProgressBar1;
			maximum = (ProgressBar2.Minimum = 0);
			progressBar2.Minimum = maximum;
			UltraProgressBar progressBar3 = ProgressBar1;
			maximum = (ProgressBar2.Value = SYSCOM._CurrentRow);
			progressBar3.Value = maximum;
		}
		catch
		{
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain._Customize.Z14AC1100.FormSynchronize));
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
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.panel9 = new System.Windows.Forms.Panel();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ProgressBar1 = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
		this.lblMessage1 = new Infragistics.Win.Misc.UltraLabel();
		this.Grid1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.Tab_C = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ProgressBar2 = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
		this.lblMessage2 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_D = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton3 = new Infragistics.Win.Misc.UltraButton();
		this.Tab_E = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.ultraButton4 = new Infragistics.Win.Misc.UltraButton();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.Tab_A.SuspendLayout();
		this.panel9.SuspendLayout();
		this.Tab_B.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Grid1).BeginInit();
		this.Tab_C.SuspendLayout();
		this.Tab_D.SuspendLayout();
		this.panel1.SuspendLayout();
		this.Tab_E.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.ultraLabel7);
		this.Tab_A.Controls.Add(this.panel9);
		this.Tab_A.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(552, 373);
		appearance1.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance1;
		this.ultraLabel7.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel7.Location = new System.Drawing.Point(16, 45);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(512, 20);
		this.ultraLabel7.TabIndex = 24;
		this.ultraLabel7.Text = "歡迎使用基本資料庫同步精靈，接下來我們將引導您一步一步同步資料";
		this.panel9.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel9.Controls.Add(this.groupBox5);
		this.panel9.Controls.Add(this.A_Btn_Cncl);
		this.panel9.Controls.Add(this.A_Btn_Next);
		this.panel9.Controls.Add(this.A_Btn_Prev);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel9.Location = new System.Drawing.Point(0, 329);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(552, 44);
		this.panel9.TabIndex = 23;
		this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox5.Location = new System.Drawing.Point(0, 0);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(552, 8);
		this.groupBox5.TabIndex = 3;
		this.groupBox5.TabStop = false;
		this.A_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Cncl.Appearance = appearance2;
		this.A_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Cncl.Location = new System.Drawing.Point(456, 9);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.ShowFocusRect = false;
		this.A_Btn_Cncl.ShowOutline = false;
		this.A_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Cncl.SupportThemes = false;
		this.A_Btn_Cncl.TabIndex = 2;
		this.A_Btn_Cncl.Text = "結束";
		this.A_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Next.Appearance = appearance3;
		this.A_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Next.Location = new System.Drawing.Point(364, 9);
		this.A_Btn_Next.Name = "A_Btn_Next";
		this.A_Btn_Next.ShowFocusRect = false;
		this.A_Btn_Next.ShowOutline = false;
		this.A_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Next.SupportThemes = false;
		this.A_Btn_Next.TabIndex = 1;
		this.A_Btn_Next.Text = "下一步";
		this.A_Btn_Next.Click += new System.EventHandler(A_Btn_Next_Click);
		this.A_Btn_Prev.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance4.Image = resources.GetObject("appearance4.Image");
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Prev.Appearance = appearance4;
		this.A_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Prev.Location = new System.Drawing.Point(272, 9);
		this.A_Btn_Prev.Name = "A_Btn_Prev";
		this.A_Btn_Prev.ShowFocusRect = false;
		this.A_Btn_Prev.ShowOutline = false;
		this.A_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Prev.SupportThemes = false;
		this.A_Btn_Prev.TabIndex = 0;
		this.A_Btn_Prev.Text = "上一步";
		this.A_Btn_Prev.Visible = false;
		this.Tab_B.Controls.Add(this.ProgressBar1);
		this.Tab_B.Controls.Add(this.lblMessage1);
		this.Tab_B.Controls.Add(this.Grid1);
		this.Tab_B.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(552, 373);
		this.ProgressBar1.Location = new System.Drawing.Point(8, 35);
		this.ProgressBar1.Name = "ProgressBar1";
		this.ProgressBar1.Size = new System.Drawing.Size(536, 23);
		this.ProgressBar1.TabIndex = 28;
		this.ProgressBar1.Text = "[Formatted]";
		appearance5.BackColor = System.Drawing.Color.White;
		this.lblMessage1.Appearance = appearance5;
		this.lblMessage1.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lblMessage1.Location = new System.Drawing.Point(8, 13);
		this.lblMessage1.Name = "lblMessage1";
		this.lblMessage1.Size = new System.Drawing.Size(512, 20);
		this.lblMessage1.TabIndex = 25;
		this.lblMessage1.Text = "資料正在同步中，請稍候!!";
		this.Grid1._ExcelSheeName = "";
		this.Grid1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.Grid1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.Grid1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.Grid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.Grid1.ColumnInfo = "4,1,0,0,0,110,Columns:0{Width:17;Name:\"RowIndicator\";AllowDragging:False;AllowResizing:False;AllowEditing:False;DataType:System.Int32;TextAlign:RightTop;TextAlignFixed:GeneralTop;}\t1{Width:110;Name:\"MainCode\";Caption:\"主辦單位編號\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t2{Width:300;Name:\"MainName\";Caption:\"主辦單位名稱(中文)\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t3{Width:300;Name:\"MainNameE\";Caption:\"主辦單位名稱(English)\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t";
		this.Grid1.ExtendLastCol = true;
		this.Grid1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Grid1.ForeColor = System.Drawing.Color.Black;
		this.Grid1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.Grid1.IsProcessUndo = false;
		this.Grid1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.Grid1.Location = new System.Drawing.Point(8, 64);
		this.Grid1.Name = "Grid1";
		this.Grid1.Rows.Count = 1;
		this.Grid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.Grid1.ShowCursor = true;
		this.Grid1.ShowToolTipOnNarrowColumn = true;
		this.Grid1.Size = new System.Drawing.Size(536, 304);
		this.Grid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11.25pt;BackColor:237, 243, 254;ForeColor:Black;TextAlign:GeneralBottom;Border:Flat,1,Silver,Both;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;TextAlign:GeneralTop;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;TextAlign:GeneralCenter;ImageAlign:CenterCenter;Border:None,1,Black,Both;Format:\"###,###,###,##0\";}\tFocus{Font:細明體, 10pt, style=Bold;BackColor:White;Margins:0, 0, 0, 0;TextAlign:GeneralCenter;Border:Double,1,96, 145, 234,Both;}\tSearch{BackColor:Highlight;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.Grid1.TabIndex = 8;
		this.Grid1.UndoMax = 10;
		this.Grid1.Visible = false;
		this.Tab_C.Controls.Add(this.ProgressBar2);
		this.Tab_C.Controls.Add(this.lblMessage2);
		this.Tab_C.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_C.Name = "Tab_C";
		this.Tab_C.Size = new System.Drawing.Size(552, 373);
		this.ProgressBar2.Location = new System.Drawing.Point(32, 136);
		this.ProgressBar2.Name = "ProgressBar2";
		this.ProgressBar2.Size = new System.Drawing.Size(496, 23);
		this.ProgressBar2.TabIndex = 27;
		this.ProgressBar2.Text = "[Formatted]";
		appearance6.BackColor = System.Drawing.Color.White;
		this.lblMessage2.Appearance = appearance6;
		this.lblMessage2.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lblMessage2.Location = new System.Drawing.Point(19, 56);
		this.lblMessage2.Name = "lblMessage2";
		this.lblMessage2.Size = new System.Drawing.Size(512, 20);
		this.lblMessage2.TabIndex = 26;
		this.lblMessage2.Text = "資料正在同步中，請稍候!!";
		this.Tab_D.Controls.Add(this.ultraLabel3);
		this.Tab_D.Controls.Add(this.ultraLabel4);
		this.Tab_D.Controls.Add(this.ultraLabel5);
		this.Tab_D.Controls.Add(this.panel1);
		this.Tab_D.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_D.Name = "Tab_D";
		this.Tab_D.Size = new System.Drawing.Size(552, 373);
		this.ultraLabel3.Location = new System.Drawing.Point(48, 128);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(472, 120);
		this.ultraLabel3.TabIndex = 33;
		this.ultraLabel3.Text = "[原因]";
		this.ultraLabel4.Location = new System.Drawing.Point(48, 80);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel4.TabIndex = 32;
		this.ultraLabel4.Text = "基本資料同步失敗";
		appearance7.ForeColor = System.Drawing.Color.Red;
		this.ultraLabel5.Appearance = appearance7;
		this.ultraLabel5.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel5.Location = new System.Drawing.Point(16, 32);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(224, 23);
		this.ultraLabel5.TabIndex = 31;
		this.ultraLabel5.Text = "失敗";
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.groupBox1);
		this.panel1.Controls.Add(this.ultraButton1);
		this.panel1.Controls.Add(this.ultraButton3);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 329);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(552, 44);
		this.panel1.TabIndex = 24;
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(552, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.ultraButton1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance8.Image = resources.GetObject("appearance8.Image");
		appearance8.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton1.Appearance = appearance8;
		this.ultraButton1.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton1.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton1.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton1.Location = new System.Drawing.Point(364, 9);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(88, 31);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 2;
		this.ultraButton1.Text = "結束";
		this.ultraButton3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance9.Image = resources.GetObject("appearance9.Image");
		appearance9.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton3.Appearance = appearance9;
		this.ultraButton3.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton3.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton3.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton3.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton3.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton3.Location = new System.Drawing.Point(272, 9);
		this.ultraButton3.Name = "ultraButton3";
		this.ultraButton3.ShowFocusRect = false;
		this.ultraButton3.ShowOutline = false;
		this.ultraButton3.Size = new System.Drawing.Size(88, 31);
		this.ultraButton3.SupportThemes = false;
		this.ultraButton3.TabIndex = 0;
		this.ultraButton3.Text = "上一步";
		this.ultraButton3.Visible = false;
		this.ultraButton3.Click += new System.EventHandler(ultraButton3_Click);
		this.Tab_E.Controls.Add(this.ultraLabel14);
		this.Tab_E.Controls.Add(this.ultraLabel13);
		this.Tab_E.Controls.Add(this.ultraLabel6);
		this.Tab_E.Controls.Add(this.panel2);
		this.Tab_E.Location = new System.Drawing.Point(0, 0);
		this.Tab_E.Name = "Tab_E";
		this.Tab_E.Size = new System.Drawing.Size(552, 373);
		this.ultraLabel14.Location = new System.Drawing.Point(56, 128);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel14.TabIndex = 30;
		this.ultraLabel14.Text = "若要結束精靈，請按一下[完成]。";
		this.ultraLabel13.Location = new System.Drawing.Point(56, 80);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel13.TabIndex = 29;
		this.ultraLabel13.Text = "你已經完成基本資料同步。";
		this.ultraLabel6.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(24, 32);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(224, 23);
		this.ultraLabel6.TabIndex = 28;
		this.ultraLabel6.Text = "恭禧您!";
		this.panel2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel2.Controls.Add(this.groupBox2);
		this.panel2.Controls.Add(this.ultraButton4);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 329);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(552, 44);
		this.panel2.TabIndex = 25;
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(552, 8);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.ultraButton4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance10.Image = resources.GetObject("appearance10.Image");
		appearance10.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton4.Appearance = appearance10;
		this.ultraButton4.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton4.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton4.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.ultraButton4.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton4.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton4.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton4.Location = new System.Drawing.Point(364, 9);
		this.ultraButton4.Name = "ultraButton4";
		this.ultraButton4.ShowFocusRect = false;
		this.ultraButton4.ShowOutline = false;
		this.ultraButton4.Size = new System.Drawing.Size(88, 31);
		this.ultraButton4.SupportThemes = false;
		this.ultraButton4.TabIndex = 2;
		this.ultraButton4.Text = "完成";
		this.Tab_Ctrl.BackColor = System.Drawing.Color.White;
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_A);
		this.Tab_Ctrl.Controls.Add(this.Tab_B);
		this.Tab_Ctrl.Controls.Add(this.Tab_C);
		this.Tab_Ctrl.Controls.Add(this.Tab_D);
		this.Tab_Ctrl.Controls.Add(this.Tab_E);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(552, 373);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabIndex = 23;
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "tab1";
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "tab2";
		ultraTab3.Key = "3";
		ultraTab3.TabPage = this.Tab_C;
		ultraTab3.Text = "tab3";
		ultraTab4.TabPage = this.Tab_D;
		ultraTab4.Text = "tab4";
		ultraTab5.TabPage = this.Tab_E;
		ultraTab5.Text = "tab5";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[5] { ultraTab1, ultraTab2, ultraTab3, ultraTab4, ultraTab5 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(552, 373);
		this.timer1.Enabled = true;
		this.timer1.Interval = 1000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 18);
		base.ClientSize = new System.Drawing.Size(552, 373);
		base.Controls.Add(this.Tab_Ctrl);
		this.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Name = "FormSynchronize";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "基本資料庫同步";
		this.Tab_A.ResumeLayout(false);
		this.panel9.ResumeLayout(false);
		this.Tab_B.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Grid1).EndInit();
		this.Tab_C.ResumeLayout(false);
		this.Tab_D.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.Tab_E.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
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
}
