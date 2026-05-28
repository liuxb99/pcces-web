using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.Budget.ItemNoset;
using Archnowledge.Pcces.PccesMain.SysMaintain;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinToolbars;
using Infragistics.Win.UltraWinTree;

namespace Archnowledge.Pcces.PccesMain.MrsBase.PickFromOther;

public class FormMrsBase_PickFromOtherDB : Form
{
	private const string CallFormHelp = "FormMrsBase_PickFromOtherDB";

	private IContainer components;

	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_A;

	private Panel panel5;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel6;

	private Panel panel2;

	private GroupBox groupBox2;

	public GridMrsBase GridUnit1;

	private UltraButton A_Btn_Cncl;

	private UltraButton A_Btn_Next;

	private UltraTabPageControl Tab_B;

	private Panel panel1;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private Panel panel3;

	private GroupBox groupBox1;

	private UltraButton ultraButton1;

	private UltraButton D_Btn_Fnsh;

	private Panel panel4;

	private UltraButton BtnGoHomeB;

	private UltraLabel lblDBName;

	private ImageList imageList2;

	private Panel panel6;

	private Panel panel7;

	private Panel panel9;

	private UltraButton BtnRemove;

	private UltraButton BtnAdd;

	private Splitter splitter2;

	private Panel panel10;

	private GridBudget c1FlexGrid2;

	private Splitter splitter1;

	private Panel panel11;

	private UltraTree ultraTree1;

	private UltraLabel ultraLabel5;

	private UltraLabel lblChoose;

	private Panel panel8;

	private GridBudget c1FlexGrid1;

	private UltraLabel lblMrsBase;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Bottom;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Right;

	private CheckBox chk_reapt;

	private UltraStatusBar ultraStatusBar1;

	private UltraStatusBar ultraStatusBar2;

	private string F_KeyWord = "";

	private DataTable DT_Nodes = new DataTable();

	private DataTable DT_Leaves = new DataTable();

	private DataTable DT_Leaves12 = new DataTable();

	private string F_NowKey = "00";

	private DataTable DT1 = new DataTable();

	private DataTable DT_MrsA = new DataTable();

	private MrsBaseA dbMrsBase;

	private string F_UserID;

	private string ExtraCri = "";

	private string F_TempUseDB = "";

	private string F_dbDesc = "";

	private string F_dbName = "";

	private string F_CurrentDBName = "";

	private string F_Cstring;

	private string F_SettingPick = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "MrsBase.ini";

	private int F_MainQty = 0;

	private int F_MainCst = 0;

	private int F_MainAmt = 0;

	private int F_AnaQty = 0;

	private int F_AnaCst = 0;

	private int F_AnaAmt = 0;

	private int GridCols = 0;

	private UltraToolbarsManager ultraToolbarsManager1;

	private object[,] GridColsSquence;

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

	public string _CurrentDBName
	{
		get
		{
			return F_CurrentDBName;
		}
		set
		{
			F_CurrentDBName = value;
		}
	}

	public string _Cstring
	{
		get
		{
			return F_Cstring;
		}
		set
		{
			F_Cstring = value;
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.MrsBase.PickFromOther.FormMrsBase_PickFromOtherDB));
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.OptionSet optionSet1 = new Infragistics.Win.UltraWinToolbars.OptionSet("Tools");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool1");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool1 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool1 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuUsual", "Tools");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool2 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuGroup", "Tools");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool3 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuListAll", "Tools");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool4 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuAnalysis", "Tools");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool5 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuGeneral", "Tools");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PickType");
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool2 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Popup1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool6 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuUsual", "Tools");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool7 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuAnalysis", "Tools");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool8 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuGeneral", "Tools");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool9 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuListAll", "Tools");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool10 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuGroup", "Tools");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PickType");
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel4 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTree.Override _override1 = new Infragistics.Win.UltraWinTree.Override();
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraStatusBar2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.GridUnit1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.panel2 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel6 = new System.Windows.Forms.Panel();
		this.panel7 = new System.Windows.Forms.Panel();
		this._FormSys_B_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this._FormSys_B_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.panel8 = new System.Windows.Forms.Panel();
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.c1FlexGrid1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.lblMrsBase = new Infragistics.Win.Misc.UltraLabel();
		this.panel9 = new System.Windows.Forms.Panel();
		this.BtnRemove = new Infragistics.Win.Misc.UltraButton();
		this.BtnAdd = new Infragistics.Win.Misc.UltraButton();
		this.splitter2 = new System.Windows.Forms.Splitter();
		this.panel10 = new System.Windows.Forms.Panel();
		this.c1FlexGrid2 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.lblChoose = new Infragistics.Win.Misc.UltraLabel();
		this._FormSys_B_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.splitter1 = new System.Windows.Forms.Splitter();
		this.panel11 = new System.Windows.Forms.Panel();
		this.ultraTree1 = new Infragistics.Win.UltraWinTree.UltraTree();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.BtnGoHomeB = new Infragistics.Win.Misc.UltraButton();
		this.lblDBName = new Infragistics.Win.Misc.UltraLabel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.chk_reapt = new System.Windows.Forms.CheckBox();
		this.D_Btn_Fnsh = new Infragistics.Win.Misc.UltraButton();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.Tab_A.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).BeginInit();
		this.panel2.SuspendLayout();
		this.panel5.SuspendLayout();
		this.Tab_B.SuspendLayout();
		this.panel6.SuspendLayout();
		this.panel7.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		this.panel8.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).BeginInit();
		this.panel9.SuspendLayout();
		this.panel10.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid2).BeginInit();
		this.panel11.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraTree1).BeginInit();
		this.panel4.SuspendLayout();
		this.panel3.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.ultraStatusBar2);
		this.Tab_A.Controls.Add(this.GridUnit1);
		this.Tab_A.Controls.Add(this.panel2);
		this.Tab_A.Controls.Add(this.panel5);
		this.Tab_A.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(782, 558);
		appearance1.BackColor = System.Drawing.SystemColors.Control;
		appearance1.FontData.Name = "細明體";
		appearance1.FontData.SizeInPoints = 11f;
		this.ultraStatusBar2.Appearance = appearance1;
		this.ultraStatusBar2.Location = new System.Drawing.Point(0, 491);
		this.ultraStatusBar2.Name = "ultraStatusBar2";
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Text = "資料筆數:";
		ultraStatusPanel1.Width = 180;
		appearance2.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance2.ForeColor = System.Drawing.Color.Blue;
		ultraStatusPanel2.Appearance = appearance2;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.MarqueeInfo.IsActive = true;
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		ultraStatusPanel2.Style = Infragistics.Win.UltraWinStatusBar.PanelStyle.Marquee;
		ultraStatusPanel2.Width = 101;
		appearance3.TextHAlign = Infragistics.Win.HAlign.Right;
		ultraStatusPanel3.Appearance = appearance3;
		ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel3.Text = "客服電話:(02)2716-5561";
		ultraStatusPanel3.Width = 200;
		this.ultraStatusBar2.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[3] { ultraStatusPanel1, ultraStatusPanel2, ultraStatusPanel3 });
		this.ultraStatusBar2.Size = new System.Drawing.Size(782, 23);
		this.ultraStatusBar2.SupportThemes = false;
		this.ultraStatusBar2.TabIndex = 23;
		this.ultraStatusBar2.Text = "ultraStatusBar2";
		this.GridUnit1._ExcelFileName = "";
		this.GridUnit1._ExcelSheeName = "";
		this.GridUnit1._IsOpenExcelAfterExport = false;
		this.GridUnit1.AllowEditing = false;
		this.GridUnit1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.GridUnit1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridUnit1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.GridUnit1.ColumnInfo = resources.GetString("GridUnit1.ColumnInfo");
		this.GridUnit1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.GridUnit1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridUnit1.ExtendLastCol = true;
		this.GridUnit1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridUnit1.ForeColor = System.Drawing.Color.Black;
		this.GridUnit1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.GridUnit1.IsProcessUndo = false;
		this.GridUnit1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.GridUnit1.Location = new System.Drawing.Point(0, 48);
		this.GridUnit1.Name = "GridUnit1";
		this.GridUnit1.Rows.Count = 1;
		this.GridUnit1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.GridUnit1.ShowCursor = true;
		this.GridUnit1.ShowToolTipOnNarrowColumn = true;
		this.GridUnit1.Size = new System.Drawing.Size(782, 466);
		this.GridUnit1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("GridUnit1.Styles"));
		this.GridUnit1.TabIndex = 16;
		this.GridUnit1.UndoMax = 10;
		this.GridUnit1.Click += new System.EventHandler(GridUnit1_Click);
		this.GridUnit1.MouseMove += new System.Windows.Forms.MouseEventHandler(GridUnit1_MouseMove);
		this.panel2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel2.Controls.Add(this.groupBox2);
		this.panel2.Controls.Add(this.A_Btn_Cncl);
		this.panel2.Controls.Add(this.A_Btn_Next);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 514);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(782, 44);
		this.panel2.TabIndex = 15;
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(782, 8);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.A_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance4.Image = resources.GetObject("appearance4.Image");
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Cncl.Appearance = appearance4;
		this.A_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Cncl.Location = new System.Drawing.Point(689, 10);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.ShowFocusRect = false;
		this.A_Btn_Cncl.ShowOutline = false;
		this.A_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Cncl.SupportThemes = false;
		this.A_Btn_Cncl.TabIndex = 2;
		this.A_Btn_Cncl.Text = "取消";
		this.A_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance5.Image = resources.GetObject("appearance5.Image");
		appearance5.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Next.Appearance = appearance5;
		this.A_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Next.Location = new System.Drawing.Point(597, 10);
		this.A_Btn_Next.Name = "A_Btn_Next";
		this.A_Btn_Next.ShowFocusRect = false;
		this.A_Btn_Next.ShowOutline = false;
		this.A_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Next.SupportThemes = false;
		this.A_Btn_Next.TabIndex = 1;
		this.A_Btn_Next.Text = "下一步";
		this.A_Btn_Next.Click += new System.EventHandler(A_Btn_Next_Click);
		this.panel5.BackColor = System.Drawing.Color.White;
		this.panel5.Controls.Add(this.ultraLabel7);
		this.panel5.Controls.Add(this.ultraLabel6);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(782, 48);
		this.panel5.TabIndex = 14;
		appearance6.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance6;
		this.ultraLabel7.Location = new System.Drawing.Point(44, 27);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel7.TabIndex = 3;
		this.ultraLabel7.Text = "請挑選要選用的資料庫來源";
		appearance7.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance7;
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(12, 7);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 2;
		this.ultraLabel6.Text = "工項來源";
		this.Tab_B.Controls.Add(this.panel6);
		this.Tab_B.Controls.Add(this.panel4);
		this.Tab_B.Controls.Add(this.panel3);
		this.Tab_B.Controls.Add(this.panel1);
		this.Tab_B.Location = new System.Drawing.Point(0, 0);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(782, 558);
		this.panel6.Controls.Add(this.panel7);
		this.panel6.Controls.Add(this.splitter1);
		this.panel6.Controls.Add(this.panel11);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel6.Location = new System.Drawing.Point(0, 76);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(782, 438);
		this.panel6.TabIndex = 18;
		this.panel7.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Bottom);
		this.panel7.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Left);
		this.panel7.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Right);
		this.panel7.Controls.Add(this.panel8);
		this.panel7.Controls.Add(this.panel9);
		this.panel7.Controls.Add(this.splitter2);
		this.panel7.Controls.Add(this.panel10);
		this.panel7.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Top);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel7.Location = new System.Drawing.Point(185, 0);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(597, 438);
		this.panel7.TabIndex = 2;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 257);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Name = "_FormSys_B_Toolbars_Dock_Area_Bottom";
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(597, 0);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		appearance8.FontData.Name = "Arial";
		appearance8.FontData.SizeInPoints = 9f;
		this.ultraToolbarsManager1.Appearance = appearance8;
		appearance9.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance9.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.DockAreaAppearance = appearance9;
		this.ultraToolbarsManager1.DockWithinContainer = this;
		this.ultraToolbarsManager1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraToolbarsManager1.LockToolbars = true;
		appearance10.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance10.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance10.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.MenuSettings.HotTrackAppearance = appearance10;
		appearance11.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance11.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsManager1.MenuSettings.IconAreaAppearance = appearance11;
		appearance12.BackColor = System.Drawing.Color.White;
		appearance12.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.MenuSettings.ToolAppearance = appearance12;
		optionSet1.AllowAllUp = false;
		this.ultraToolbarsManager1.OptionSets.Add(optionSet1);
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
		ultraToolbar1.Settings.AllowDockTop = Infragistics.Win.DefaultableBoolean.True;
		ultraToolbar1.Text = "Tool1";
		labelTool1.InstanceProps.IsFirstInGroup = true;
		stateButtonTool2.InstanceProps.IsFirstInGroup = true;
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[9] { labelTool1, comboBoxTool1, buttonTool1, stateButtonTool1, stateButtonTool2, stateButtonTool3, stateButtonTool4, stateButtonTool5, buttonTool2 });
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[1] { ultraToolbar1 });
		appearance13.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance13.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance13;
		appearance14.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance14.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance14.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance14;
		appearance15.Image = resources.GetObject("appearance15.Image");
		buttonTool3.SharedProps.AppearancesSmall.Appearance = appearance15;
		buttonTool3.SharedProps.Caption = "刪除";
		buttonTool3.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool3.SharedProps.Shortcut = System.Windows.Forms.Shortcut.Del;
		labelTool2.SharedProps.Caption = "尋找:";
		labelTool2.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool2.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool2.SharedProps.Caption = "輸入關鍵字";
		comboBoxTool2.SharedProps.Width = 200;
		comboBoxTool2.ValueList = valueList1;
		appearance16.Image = resources.GetObject("appearance16.Image");
		buttonTool4.SharedProps.AppearancesSmall.Appearance = appearance16;
		buttonTool4.SharedProps.Caption = "Go";
		popupMenuTool1.SharedProps.Caption = "右鍵功能表";
		popupMenuTool1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[1] { buttonTool5 });
		stateButtonTool6.OptionSetKey = "Tools";
		stateButtonTool6.SharedProps.Caption = "檢視常用工項";
		stateButtonTool6.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool7.OptionSetKey = "Tools";
		stateButtonTool7.SharedProps.Caption = "有單價分析";
		stateButtonTool7.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool8.OptionSetKey = "Tools";
		stateButtonTool8.SharedProps.Caption = "無單價分析";
		stateButtonTool8.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool9.OptionSetKey = "Tools";
		stateButtonTool9.SharedProps.Caption = "顯示所有項目";
		stateButtonTool9.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool10.OptionSetKey = "Tools";
		stateButtonTool10.SharedProps.Caption = "只顯示選定的類別";
		stateButtonTool10.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool10.SharedProps.Enabled = false;
		stateButtonTool10.SharedProps.Visible = false;
		buttonTool6.SharedProps.Caption = "類別篩選";
		buttonTool6.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[11]
		{
			buttonTool3, labelTool2, comboBoxTool2, buttonTool4, popupMenuTool1, stateButtonTool6, stateButtonTool7, stateButtonTool8, stateButtonTool9, stateButtonTool10,
			buttonTool6
		});
		this.ultraToolbarsManager1.ToolKeyPress += new Infragistics.Win.UltraWinToolbars.ToolKeyPressEventHandler(ultraToolbarsManager1_ToolKeyPress);
		this.ultraToolbarsManager1.BeforeToolbarListDropdown += new Infragistics.Win.UltraWinToolbars.BeforeToolbarListDropdownEventHandler(ultraToolbarsManager1_BeforeToolbarListDropdown);
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this.ultraToolbarsManager1.AfterToolDeactivate += new Infragistics.Win.UltraWinToolbars.ToolEventHandler(ultraToolbarsManager1_AfterToolDeactivate);
		this.ultraToolbarsManager1.AfterToolActivate += new Infragistics.Win.UltraWinToolbars.ToolEventHandler(ultraToolbarsManager1_AfterToolActivate);
		this._FormSys_B_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormSys_B_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 27);
		this._FormSys_B_Toolbars_Dock_Area_Left.Name = "_FormSys_B_Toolbars_Dock_Area_Left";
		this._FormSys_B_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 230);
		this._FormSys_B_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormSys_B_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(597, 27);
		this._FormSys_B_Toolbars_Dock_Area_Right.Name = "_FormSys_B_Toolbars_Dock_Area_Right";
		this._FormSys_B_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 230);
		this._FormSys_B_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel8.Controls.Add(this.ultraStatusBar1);
		this.panel8.Controls.Add(this.c1FlexGrid1);
		this.panel8.Controls.Add(this.lblMrsBase);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel8.Location = new System.Drawing.Point(0, 27);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(597, 230);
		this.panel8.TabIndex = 7;
		appearance17.BackColor = System.Drawing.SystemColors.Control;
		appearance17.FontData.SizeInPoints = 11f;
		this.ultraStatusBar1.Appearance = appearance17;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 203);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		ultraStatusPanel4.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel4.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		ultraStatusPanel4.Text = "資料筆數:";
		ultraStatusPanel4.Width = 200;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[1] { ultraStatusPanel4 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(595, 25);
		this.ultraStatusBar1.SupportThemes = false;
		this.ultraStatusBar1.TabIndex = 12;
		this.ultraStatusBar1.Text = "ultraStatusBar1";
		this.c1FlexGrid1._ExcelFileName = "";
		this.c1FlexGrid1._ExcelSheeName = "";
		this.c1FlexGrid1._IsOpenExcelAfterExport = false;
		this.c1FlexGrid1.AllowEditing = false;
		this.c1FlexGrid1.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
		this.c1FlexGrid1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.c1FlexGrid1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.c1FlexGrid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.c1FlexGrid1.ColumnInfo = resources.GetString("c1FlexGrid1.ColumnInfo");
		this.c1FlexGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1FlexGrid1.ExtendLastCol = true;
		this.c1FlexGrid1.ForeColor = System.Drawing.SystemColors.WindowText;
		this.c1FlexGrid1.Location = new System.Drawing.Point(0, 28);
		this.c1FlexGrid1.Name = "c1FlexGrid1";
		this.c1FlexGrid1.Rows.Count = 1;
		this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.c1FlexGrid1.ShowToolTipOnNarrowColumn = true;
		this.c1FlexGrid1.Size = new System.Drawing.Size(595, 200);
		this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid1.Styles"));
		this.c1FlexGrid1.TabIndex = 1;
		appearance18.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance18.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance18.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.lblMrsBase.Appearance = appearance18;
		this.lblMrsBase.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.lblMrsBase.Dock = System.Windows.Forms.DockStyle.Top;
		this.lblMrsBase.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblMrsBase.Location = new System.Drawing.Point(0, 0);
		this.lblMrsBase.Name = "lblMrsBase";
		this.lblMrsBase.Size = new System.Drawing.Size(595, 28);
		this.lblMrsBase.TabIndex = 0;
		this.lblMrsBase.Text = "基本資料庫";
		this.panel9.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel9.Controls.Add(this.BtnRemove);
		this.panel9.Controls.Add(this.BtnAdd);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel9.Location = new System.Drawing.Point(0, 257);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(597, 32);
		this.panel9.TabIndex = 5;
		this.panel9.Resize += new System.EventHandler(panel9_Resize);
		appearance19.FontData.Name = "Arial";
		appearance19.FontData.SizeInPoints = 9f;
		appearance19.Image = resources.GetObject("appearance19.Image");
		this.BtnRemove.Appearance = appearance19;
		this.BtnRemove.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.BtnRemove.Location = new System.Drawing.Point(305, 2);
		this.BtnRemove.Name = "BtnRemove";
		this.BtnRemove.ShowFocusRect = false;
		this.BtnRemove.ShowOutline = false;
		this.BtnRemove.Size = new System.Drawing.Size(68, 28);
		this.BtnRemove.SupportThemes = false;
		this.BtnRemove.TabIndex = 1;
		this.BtnRemove.Text = "移除";
		this.BtnRemove.Click += new System.EventHandler(BtnRemove_Click);
		appearance20.FontData.Name = "Arial";
		appearance20.FontData.SizeInPoints = 9f;
		appearance20.Image = resources.GetObject("appearance20.Image");
		this.BtnAdd.Appearance = appearance20;
		this.BtnAdd.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.BtnAdd.Location = new System.Drawing.Point(232, 2);
		this.BtnAdd.Name = "BtnAdd";
		this.BtnAdd.ShowFocusRect = false;
		this.BtnAdd.ShowOutline = false;
		this.BtnAdd.Size = new System.Drawing.Size(68, 28);
		this.BtnAdd.SupportThemes = false;
		this.BtnAdd.TabIndex = 0;
		this.BtnAdd.Text = "加入";
		this.BtnAdd.Click += new System.EventHandler(BtnAdd_Click);
		this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.splitter2.Location = new System.Drawing.Point(0, 289);
		this.splitter2.Name = "splitter2";
		this.splitter2.Size = new System.Drawing.Size(597, 5);
		this.splitter2.TabIndex = 4;
		this.splitter2.TabStop = false;
		this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel10.Controls.Add(this.c1FlexGrid2);
		this.panel10.Controls.Add(this.lblChoose);
		this.panel10.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel10.Location = new System.Drawing.Point(0, 294);
		this.panel10.Name = "panel10";
		this.panel10.Size = new System.Drawing.Size(597, 144);
		this.panel10.TabIndex = 3;
		this.c1FlexGrid2._ExcelFileName = "";
		this.c1FlexGrid2._ExcelSheeName = "";
		this.c1FlexGrid2._IsOpenExcelAfterExport = false;
		this.c1FlexGrid2.AllowEditing = false;
		this.c1FlexGrid2.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
		this.c1FlexGrid2.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.c1FlexGrid2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.c1FlexGrid2.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.c1FlexGrid2.ColumnInfo = resources.GetString("c1FlexGrid2.ColumnInfo");
		this.c1FlexGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1FlexGrid2.ExtendLastCol = true;
		this.c1FlexGrid2.ForeColor = System.Drawing.SystemColors.WindowText;
		this.c1FlexGrid2.Location = new System.Drawing.Point(0, 28);
		this.c1FlexGrid2.Name = "c1FlexGrid2";
		this.c1FlexGrid2.Rows.Count = 1;
		this.c1FlexGrid2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.c1FlexGrid2.ShowToolTipOnNarrowColumn = true;
		this.c1FlexGrid2.Size = new System.Drawing.Size(595, 114);
		this.c1FlexGrid2.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid2.Styles"));
		this.c1FlexGrid2.TabIndex = 2;
		appearance21.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance21.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance21.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.lblChoose.Appearance = appearance21;
		this.lblChoose.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.lblChoose.Dock = System.Windows.Forms.DockStyle.Top;
		this.lblChoose.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblChoose.Location = new System.Drawing.Point(0, 0);
		this.lblChoose.Name = "lblChoose";
		this.lblChoose.Size = new System.Drawing.Size(595, 28);
		this.lblChoose.TabIndex = 0;
		this.lblChoose.Text = "已選用工項";
		this._FormSys_B_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormSys_B_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormSys_B_Toolbars_Dock_Area_Top.Name = "_FormSys_B_Toolbars_Dock_Area_Top";
		this._FormSys_B_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(597, 27);
		this._FormSys_B_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this.splitter1.Location = new System.Drawing.Point(180, 0);
		this.splitter1.Name = "splitter1";
		this.splitter1.Size = new System.Drawing.Size(5, 438);
		this.splitter1.TabIndex = 1;
		this.splitter1.TabStop = false;
		this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel11.Controls.Add(this.ultraTree1);
		this.panel11.Controls.Add(this.ultraLabel5);
		this.panel11.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel11.Location = new System.Drawing.Point(0, 0);
		this.panel11.Name = "panel11";
		this.panel11.Size = new System.Drawing.Size(180, 438);
		this.panel11.TabIndex = 0;
		appearance22.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraTree1.Appearance = appearance22;
		this.ultraTree1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		this.ultraTree1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraTree1.HideSelection = false;
		this.ultraTree1.Indent = 15;
		this.ultraTree1.Location = new System.Drawing.Point(0, 28);
		this.ultraTree1.Name = "ultraTree1";
		_override1.SelectionType = Infragistics.Win.UltraWinTree.SelectType.Single;
		this.ultraTree1.Override = _override1;
		this.ultraTree1.Size = new System.Drawing.Size(178, 408);
		this.ultraTree1.TabIndex = 1;
		this.ultraTree1.Click += new System.EventHandler(ultraTree1_Click);
		this.ultraTree1.AfterSelect += new Infragistics.Win.UltraWinTree.AfterNodeSelectEventHandler(ultraTree1_AfterSelect);
		appearance23.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance23.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance23.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel5.Appearance = appearance23;
		this.ultraLabel5.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel5.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel5.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(178, 28);
		this.ultraLabel5.TabIndex = 0;
		this.ultraLabel5.Text = "工程綱要";
		this.panel4.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel4.Controls.Add(this.BtnGoHomeB);
		this.panel4.Controls.Add(this.lblDBName);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel4.Location = new System.Drawing.Point(0, 48);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(782, 28);
		this.panel4.TabIndex = 17;
		this.BtnGoHomeB.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance24.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance24.ForeColor = System.Drawing.Color.White;
		appearance24.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnGoHomeB.Appearance = appearance24;
		this.BtnGoHomeB.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button;
		this.BtnGoHomeB.Font = new System.Drawing.Font("細明體", 9f);
		appearance25.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance25.ForeColor = System.Drawing.Color.Yellow;
		this.BtnGoHomeB.HotTrackAppearance = appearance25;
		this.BtnGoHomeB.HotTracking = true;
		this.BtnGoHomeB.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnGoHomeB.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnGoHomeB.Location = new System.Drawing.Point(637, 3);
		this.BtnGoHomeB.Name = "BtnGoHomeB";
		this.BtnGoHomeB.ShowFocusRect = false;
		this.BtnGoHomeB.ShowOutline = false;
		this.BtnGoHomeB.Size = new System.Drawing.Size(140, 23);
		this.BtnGoHomeB.SupportThemes = false;
		this.BtnGoHomeB.TabIndex = 1;
		this.BtnGoHomeB.Text = "重新挑選「資料庫」";
		this.BtnGoHomeB.Click += new System.EventHandler(BtnGoHomeB_Click);
		appearance26.ForeColor = System.Drawing.Color.White;
		appearance26.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.lblDBName.Appearance = appearance26;
		this.lblDBName.Dock = System.Windows.Forms.DockStyle.Left;
		this.lblDBName.Location = new System.Drawing.Point(0, 0);
		this.lblDBName.Name = "lblDBName";
		this.lblDBName.Size = new System.Drawing.Size(608, 28);
		this.lblDBName.TabIndex = 0;
		this.lblDBName.Text = "資料庫名稱:";
		this.panel3.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel3.Controls.Add(this.chk_reapt);
		this.panel3.Controls.Add(this.D_Btn_Fnsh);
		this.panel3.Controls.Add(this.groupBox1);
		this.panel3.Controls.Add(this.ultraButton1);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel3.Location = new System.Drawing.Point(0, 514);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(782, 44);
		this.panel3.TabIndex = 16;
		this.chk_reapt.ForeColor = System.Drawing.Color.Red;
		this.chk_reapt.Location = new System.Drawing.Point(8, 16);
		this.chk_reapt.Name = "chk_reapt";
		this.chk_reapt.Size = new System.Drawing.Size(280, 24);
		this.chk_reapt.TabIndex = 5;
		this.chk_reapt.Text = "遇相同工項代碼時，採覆蓋方式";
		this.chk_reapt.CheckedChanged += new System.EventHandler(chk_reapt_CheckedChanged);
		this.D_Btn_Fnsh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance27.Image = resources.GetObject("appearance27.Image");
		appearance27.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Fnsh.Appearance = appearance27;
		this.D_Btn_Fnsh.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Fnsh.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Fnsh.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Fnsh.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Fnsh.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Fnsh.Location = new System.Drawing.Point(598, 10);
		this.D_Btn_Fnsh.Name = "D_Btn_Fnsh";
		this.D_Btn_Fnsh.ShowFocusRect = false;
		this.D_Btn_Fnsh.ShowOutline = false;
		this.D_Btn_Fnsh.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Fnsh.SupportThemes = false;
		this.D_Btn_Fnsh.TabIndex = 4;
		this.D_Btn_Fnsh.Text = "確定";
		this.D_Btn_Fnsh.Click += new System.EventHandler(D_Btn_Fnsh_Click);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(782, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.ultraButton1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance28.Image = resources.GetObject("appearance28.Image");
		appearance28.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton1.Appearance = appearance28;
		this.ultraButton1.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton1.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton1.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton1.Location = new System.Drawing.Point(689, 10);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(88, 31);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 2;
		this.ultraButton1.Text = "取消";
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Controls.Add(this.ultraLabel2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(782, 48);
		this.panel1.TabIndex = 15;
		appearance29.BackColor = System.Drawing.Color.White;
		this.ultraLabel1.Appearance = appearance29;
		this.ultraLabel1.Location = new System.Drawing.Point(44, 27);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(652, 20);
		this.ultraLabel1.TabIndex = 3;
		this.ultraLabel1.Text = "請挑選要引用的工項 (配合鍵盤Ctrl、Shift來作多項選取，建議先讓視窗最大化)";
		appearance30.BackColor = System.Drawing.Color.White;
		this.ultraLabel2.Appearance = appearance30;
		this.ultraLabel2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel2.Location = new System.Drawing.Point(12, 7);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel2.TabIndex = 2;
		this.ultraLabel2.Text = "工項挑選";
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_A);
		this.Tab_Ctrl.Controls.Add(this.Tab_B);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(782, 558);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabIndex = 15;
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "tab1";
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "tab2";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2] { ultraTab1, ultraTab2 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(782, 558);
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Magenta;
		this.imageList2.Images.SetKeyName(0, "");
		this.imageList2.Images.SetKeyName(1, "");
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 18);
		base.ClientSize = new System.Drawing.Size(782, 558);
		base.Controls.Add(this.Tab_Ctrl);
		this.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.MinimizeBox = false;
		base.Name = "FormMrsBase_PickFromOtherDB";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "自其他資料庫選用";
		base.Load += new System.EventHandler(FormMrsBase_PickFromOtherDB_Load);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormMrsBase_PickFromOtherDB_FormClosing);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormMrsBase_PickFromOtherDB_KeyDown);
		this.Tab_A.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).EndInit();
		this.panel2.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		this.Tab_B.ResumeLayout(false);
		this.panel6.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
		this.panel8.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).EndInit();
		this.panel9.ResumeLayout(false);
		this.panel10.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid2).EndInit();
		this.panel11.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraTree1).EndInit();
		this.panel4.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
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

	public FormMrsBase_PickFromOtherDB()
	{
		InitializeComponent();
		GridCols = c1FlexGrid1.Cols.Count;
		GridColsSquence = new object[GridCols, 8];
		c1FlexGrid1.Glyphs[GlyphEnum.Checked] = imageList2.Images[0];
		c1FlexGrid1.Glyphs[GlyphEnum.Unchecked] = imageList2.Images[1];
		c1FlexGrid2.Glyphs[GlyphEnum.Checked] = imageList2.Images[0];
		c1FlexGrid2.Glyphs[GlyphEnum.Unchecked] = imageList2.Images[1];
		HideCols(IsHide: true);
		RememberColsProps();
	}

	private void HideCols(bool IsHide)
	{
		if (IsHide)
		{
			c1FlexGrid1.Cols["PubCode"].Visible = false;
			c1FlexGrid1.Cols["AnalysisQty"].Visible = false;
			c1FlexGrid1.Cols["CostKind"].Visible = false;
			c1FlexGrid1.Cols["extendCode"].Visible = false;
			c1FlexGrid1.Cols["memo"].Visible = false;
			c1FlexGrid1.Cols["rate"].Visible = false;
			c1FlexGrid1.Cols["resCode"].Visible = false;
			c1FlexGrid1.Cols["resType"].Visible = false;
			c1FlexGrid1.Cols["xNameE"].Visible = false;
			c1FlexGrid1.Cols["xNameC"].Visible = false;
			c1FlexGrid1.Cols["State"].Visible = false;
			c1FlexGrid1.Cols["usrQty"].Visible = false;
			c1FlexGrid1.Cols["usrAmt"].Visible = false;
			c1FlexGrid1.Cols["Show"].Visible = false;
			c1FlexGrid1.Cols["Post"].Visible = false;
			c1FlexGrid2.Cols["PubCode"].Visible = false;
			c1FlexGrid2.Cols["AnalysisQty"].Visible = false;
			c1FlexGrid2.Cols["CostKind"].Visible = false;
			c1FlexGrid2.Cols["extendCode"].Visible = false;
			c1FlexGrid2.Cols["memo"].Visible = false;
			c1FlexGrid2.Cols["rate"].Visible = false;
			c1FlexGrid2.Cols["resCode"].Visible = false;
			c1FlexGrid2.Cols["resType"].Visible = false;
			c1FlexGrid2.Cols["xNameE"].Visible = false;
			c1FlexGrid2.Cols["xNameC"].Visible = false;
			c1FlexGrid2.Cols["State"].Visible = false;
			c1FlexGrid2.Cols["usrQty"].Visible = false;
			c1FlexGrid2.Cols["usrAmt"].Visible = false;
			c1FlexGrid2.Cols["Show"].Visible = false;
			c1FlexGrid2.Cols["Post"].Visible = false;
		}
	}

	private void SettingDecimal()
	{
		DataTable DTDecimal = new DataTable();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("WinFORM 基本工料");
		Archnowledge.Pcces.BUDClass.PubDecimal dbDecimal = new Archnowledge.Pcces.BUDClass.PubDecimal(aArr);
		DTDecimal = dbDecimal.ListItem("", "");
		if (DTDecimal.Rows.Count > 0)
		{
			F_MainQty = Convert.ToInt32(DTDecimal.Rows[0]["itemQty"]);
			F_MainCst = Convert.ToInt32(DTDecimal.Rows[0]["itemCost"]);
			F_MainAmt = Convert.ToInt32(DTDecimal.Rows[0]["itemAmt"]);
			F_AnaQty = Convert.ToInt32(DTDecimal.Rows[0]["analysisQty"]);
			F_AnaCst = Convert.ToInt32(DTDecimal.Rows[0]["analysisCost"]);
			F_AnaAmt = Convert.ToInt32(DTDecimal.Rows[0]["analysisAmt"]);
		}
		else
		{
			F_MainQty = 0;
			F_MainCst = 0;
			F_MainAmt = 0;
			F_AnaQty = 3;
			F_AnaCst = 2;
			F_AnaAmt = 2;
		}
	}

	private void RememberColsProps()
	{
		for (int i = 0; i < GridCols; i++)
		{
			GridColsSquence[i, 0] = c1FlexGrid1.Cols[i].Name;
			GridColsSquence[i, 1] = c1FlexGrid1.Cols[i].Caption;
			GridColsSquence[i, 2] = c1FlexGrid1.Cols[i].Width;
			GridColsSquence[i, 3] = c1FlexGrid1.Cols[i].DataType.ToString();
			GridColsSquence[i, 4] = c1FlexGrid1.Cols[i].Visible;
			GridColsSquence[i, 5] = c1FlexGrid1.Cols[i].Format;
			GridColsSquence[i, 6] = c1FlexGrid1.Cols[i].AllowEditing;
			if (c1FlexGrid1.Cols[i].Name == "Cost")
			{
				if (F_MainCst > 0)
				{
					GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_MainCst, '0');
				}
				else
				{
					GridColsSquence[i, 5] = "###,###,###,##0";
				}
			}
			GridColsSquence[i, 7] = c1FlexGrid1.Cols[i].TextAlign;
		}
	}

	private void SetGridColumn()
	{
		for (int i = 0; i < GridCols; i++)
		{
			c1FlexGrid1.Cols[i].Name = (string)GridColsSquence[i, 0];
			c1FlexGrid1.Cols[i].Caption = (string)GridColsSquence[i, 1];
			c1FlexGrid1.Cols[i].Width = (int)GridColsSquence[i, 2];
			c1FlexGrid1.Cols[i].DataType = Type.GetType((string)GridColsSquence[i, 3]);
			c1FlexGrid1.Cols[i].Visible = (bool)GridColsSquence[i, 4];
			c1FlexGrid1.Cols[i].Format = (string)GridColsSquence[i, 5];
			c1FlexGrid1.Cols[i].AllowEditing = (bool)GridColsSquence[i, 6];
		}
	}

	private void GetMrsBase(string sWhere)
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("WinFORM 基本工料");
		dbMrsBase = new MrsBaseA(F_UserID, aArr);
		dbMrsBase.ps_srckind = "MRS";
		dbMrsBase.ps_projectcode = "";
		Cursor = Cursors.WaitCursor;
		if (sWhere.Trim() == "")
		{
			DT_MrsA = dbMrsBase.ListItem();
		}
		else
		{
			DT_MrsA = dbMrsBase.ListItem(sWhere);
		}
		aArr = null;
		BindToGrid_MrsA();
	}

	private void LoadData()
	{
		GeneralManager oManager = new GeneralManager();
		DataSet dsSysPccesSlave;
		ExecResult ER = oManager.GetSysPccesSlave(F_UserID, out dsSysPccesSlave);
		if (ER.ReturnCode != 0)
		{
			MessageBox.Show(this, "資料庫有未知問題發生 : " + ER.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		else
		{
			BindToGrid(dsSysPccesSlave.Tables[0]);
		}
	}

	private void ProcessTree()
	{
		Get_NodesData();
		Get_LeavesData();
		ultraTree1.Nodes.Clear();
		UltraTreeNode node = ultraTree1.Nodes.Add("ROOT", "預算工項綱要");
		PopulateLevel1(node);
		ultraTree1.Nodes[0].Expanded = true;
		ultraTree1.PerformAction(UltraTreeAction.FirstNode, shift: false, control: false);
		ultraTree1.PerformAction(UltraTreeAction.NextNode, shift: false, control: false);
	}

	private void Get_NodesData()
	{
		DBClass DBClass1 = new DBClass();
		DT_Nodes = DBClass1.GetAutoNumA1();
	}

	private void Get_LeavesData()
	{
		DBClass DBClass1 = new DBClass();
		DT_Leaves = DBClass1.GetAutoNumA2();
		DT_Leaves12 = DBClass1.GetAutoNumA2_12();
		DBClass1 = null;
	}

	private void PopulateLevel1(UltraTreeNode treeNode)
	{
		treeNode.Nodes.Clear();
		UltraTreeNode node = null;
		foreach (DataRow row in DT_Nodes.Rows)
		{
			string itemCode = row["itemCode"] as string;
			string cName = row["itemCode"].ToString().Trim() + " " + row["cName"].ToString().Trim();
			node = treeNode.Nodes.Add(itemCode, cName.Trim());
			PopulateLevel2(node);
		}
	}

	private void PopulateLevel2(UltraTreeNode treeNode)
	{
		if (treeNode.Level <= 1 && !(treeNode.Key == "00"))
		{
			treeNode.Nodes.Clear();
			string filterExp = " substring(itemCode,1," + treeNode.Key.Length + ") = '" + treeNode.Key + "'";
			string sortExp = " itemCode ASC ";
			DataRow[] rows = null;
			rows = ((treeNode.Key.Length != 1) ? DT_Leaves.Select(filterExp, sortExp) : DT_Leaves12.Select(filterExp, sortExp));
			UltraTreeNode node = null;
			string itemCode = "";
			string cName = "";
			DataRow[] array = rows;
			foreach (DataRow row in array)
			{
				itemCode = row["itemCode"] as string;
				cName = row["itemCode"].ToString().Trim() + " " + row["cName"].ToString().Trim();
				node = treeNode.Nodes.Add(itemCode, cName);
				node.Tag = new ExtendedNodeInfo(typeof(string), "itemCode");
			}
		}
	}

	private void ultraTree1_Click(object sender, EventArgs e)
	{
		if (ultraTree1.SelectedNodes.Count > 0)
		{
			int iFIND = c1FlexGrid1.FindRow(ultraTree1.SelectedNodes[0].Key, 1, c1FlexGrid1.Cols["PccesCode"].SafeIndex, caseSensitive: false, fullMatch: false, wrap: false);
			if (iFIND > -1)
			{
				c1FlexGrid1.Row = iFIND;
			}
		}
	}

	private void BindToGrid(DataTable dtSysPccesSlave)
	{
		CellStyle CSDatabaseName = GridUnit1.Styles.Add("MainColor");
		CSDatabaseName.ForeColor = Color.Blue;
		CSDatabaseName.Font = new Font(GridUnit1.Font, FontStyle.Bold);
		CellStyle CSError = GridUnit1.Styles.Add("ErrorColor");
		CSError.BackColor = Color.Tomato;
		GridUnit1.Rows.Count = 1;
		GridUnit1.Redraw = false;
		foreach (DataRow theRow in dtSysPccesSlave.Rows)
		{
			if (!(theRow["ChkUse"].ToString().Trim() == "1"))
			{
				Row GridRow = GridUnit1.Rows.Add();
				GridRow.IsNode = true;
				GridRow.Node.Level = 1;
				GridRow.Node.Collapsed = true;
				CellRange rgDB1 = GridUnit1.GetCellRange(GridRow.Index, GridUnit1.Cols["dbDesc"].SafeIndex);
				CellRange rgDB2 = GridUnit1.GetCellRange(GridRow.Index, GridUnit1.Cols["dbName"].SafeIndex);
				CellStyle style = (rgDB2.Style = CSDatabaseName);
				rgDB1.Style = style;
				string DatabaseName = theRow["dbcName"].ToString().Trim();
				string DatabaseDesc = (string)(GridRow["dbDesc"] = theRow["dbcDesc"].ToString().Trim());
				GridRow["dbName"] = DatabaseName;
				if (DatabaseDesc.IndexOf("ERROR") > -1)
				{
					CellRange rgError = GridUnit1.GetCellRange(GridRow.Index, 1, GridRow.Index, GridUnit1.Cols.Count - 1);
					rgError.Style = CSError;
				}
			}
		}
		GridUnit1.Redraw = true;
		int Count = GridUnit1.Rows.Count - 1;
		ultraStatusBar2.Panels[0].Text = "資料筆數：" + Count;
	}

	private void BindToGrid_MrsA()
	{
		Cursor = Cursors.WaitCursor;
		RememberColsProps();
		DataView DV1 = DT_MrsA.DefaultView;
		c1FlexGrid1.Redraw = false;
		lblMrsBase.Text = "基本資料庫(" + DV1.Count + ")";
		DV1.Sort = " pccesCode ASC ";
		CellStyle CS1 = c1FlexGrid1.Styles.Add("AnalysisColor");
		CellStyle CS2 = c1FlexGrid1.Styles.Add("LEMColor");
		CellStyle CS3 = c1FlexGrid1.Styles.Add("WColor");
		CellStyle CS4 = c1FlexGrid1.Styles.Add("ZColor");
		CellStyle CS5 = c1FlexGrid1.Styles.Add("DollarColor");
		CellStyle CS6 = c1FlexGrid1.Styles.Add("PercentColor");
		CS1.ForeColor = Color.Red;
		CS2.ForeColor = Color.Teal;
		CS3.ForeColor = Color.Purple;
		CS4.ForeColor = Color.Teal;
		CS4.BackColor = Color.LemonChiffon;
		CS5.ForeColor = Color.Green;
		CS6.ForeColor = Color.Blue;
		c1FlexGrid1.Clear(ClearFlags.All);
		c1FlexGrid1.Select(0, 0);
		c1FlexGrid1.Rows.Count = DV1.Count + 1;
		SetGridColumn();
		string sItemClass = "";
		string sCostKind = "";
		for (int i = 0; i < DV1.Count; i++)
		{
			sItemClass = DV1[i]["pccesCode"].ToString().Substring(0, 1);
			c1FlexGrid1[i + 1, "PccesCode"] = DV1[i]["pccesCode"].ToString().Trim();
			if (sItemClass == "L" || sItemClass == "E" || sItemClass == "M")
			{
				c1FlexGrid1.Rows[i + 1].Style = c1FlexGrid1.Styles["LEMColor"];
			}
			else if (sItemClass == "W")
			{
				c1FlexGrid1.Rows[i + 1].Style = c1FlexGrid1.Styles["WColor"];
			}
			switch (sCostKind)
			{
			case "$":
				c1FlexGrid1.Rows[i + 1].Style = c1FlexGrid1.Styles["DollarColor"];
				break;
			case "%":
				c1FlexGrid1.Rows[i + 1].Style = c1FlexGrid1.Styles["PercentColor"];
				break;
			default:
				if (!(sCostKind == "#"))
				{
					break;
				}
				goto case "Z";
			case "Z":
				c1FlexGrid1.Rows[i + 1].Style = c1FlexGrid1.Styles["ZColor"];
				break;
			}
			c1FlexGrid1[i + 1, "CName"] = DV1[i]["cName"].ToString();
			if (DV1[i]["analysis"].ToString().Trim() == "1")
			{
				c1FlexGrid1[i + 1, "Analysis"] = true;
				c1FlexGrid1.Rows[i + 1].Style = c1FlexGrid1.Styles["AnalysisColor"];
			}
			else
			{
				c1FlexGrid1[i + 1, "Analysis"] = false;
			}
			c1FlexGrid1[i + 1, "resCode"] = DV1[i]["resCode"];
			c1FlexGrid1[i + 1, "PubCode"] = DV1[i]["pubCode"];
			c1FlexGrid1[i + 1, "eName"] = DV1[i]["eName"];
			c1FlexGrid1[i + 1, "Memo"] = DV1[i]["memo"];
			c1FlexGrid1[i + 1, "UnitName"] = DV1[i]["unitName"];
			c1FlexGrid1[i + 1, "resType"] = DV1[i]["resType"];
			c1FlexGrid1[i + 1, "LRate"] = DV1[i]["lRate"];
			c1FlexGrid1[i + 1, "ERate"] = DV1[i]["eRate"];
			c1FlexGrid1[i + 1, "MRate"] = DV1[i]["mRate"];
			c1FlexGrid1[i + 1, "WRate"] = DV1[i]["wRate"];
			c1FlexGrid1[i + 1, "Cost"] = DV1[i]["cost"];
			c1FlexGrid1[i + 1, "AnalysisQty"] = DV1[i]["analysisQty"];
			c1FlexGrid1[i + 1, "Rate"] = DV1[i]["rate"];
			c1FlexGrid1[i + 1, "CostKind"] = DV1[i]["costKind"];
			c1FlexGrid1[i + 1, "XNameC"] = DV1[i]["xNameC"];
			c1FlexGrid1[i + 1, "XNameE"] = DV1[i]["xNameE"];
			c1FlexGrid1[i + 1, "eUnit"] = DV1[i]["eUnit"];
			c1FlexGrid1[i + 1, "extendCode"] = DV1[i]["extendCode"];
			c1FlexGrid1[i + 1, "State"] = DV1[i]["state"];
			c1FlexGrid1[i + 1, "usrQty"] = DV1[i]["usrQty"];
			c1FlexGrid1[i + 1, "usrAmt"] = DV1[i]["usrAmt"];
			c1FlexGrid1[i + 1, "Show"] = DV1[i]["Show"];
			c1FlexGrid1[i + 1, "Post"] = DV1[i]["Post"];
		}
		ultraStatusBar1.Panels[0].Text = "資料筆數:" + DV1.Count;
		Cursor = Cursors.Default;
		c1FlexGrid1.Redraw = true;
	}

	private void FormMrsBase_PickFromOtherDB_Load(object sender, EventArgs e)
	{
		string sIniFileName = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "PccesMain.ini";
		string Status = CommonMethods.IniReadValue(sIniFileName, "chk_reapt", "State");
		SettingDecimal();
		SysUser oSysUser = new SysUser();
		F_CurrentDBName = oSysUser.GetSysUserDatabaseName(F_UserID);
		ProcessTree();
		LoadData();
		GridUnit1.Select();
		if (Status == "True")
		{
			chk_reapt.Checked = true;
		}
		else
		{
			chk_reapt.Checked = false;
		}
	}

	private void GridUnit1_Click(object sender, EventArgs e)
	{
		if (GridUnit1.Row > 0)
		{
			F_dbDesc = GridUnit1[GridUnit1.Row, "dbDesc"].ToString().Trim();
			F_dbName = GridUnit1[GridUnit1.Row, "dbName"].ToString().Trim();
			lblDBName.Text = "  資料庫名稱: " + F_dbDesc + "【" + F_dbName + "】";
			F_TempUseDB = GridUnit1[GridUnit1.Row, "dbName"].ToString();
			FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
			FM_INFO._InfoString = " 基本資料庫載入中，請稍候! ";
			FM_INFO.Show();
			Application.DoEvents();
			SysUser oSysUser = new SysUser();
			oSysUser.SetSysUserDatabaseName(F_UserID, F_dbName);
			Cursor = Cursors.WaitCursor;
			GridUnit1.Enabled = false;
			panel2.Enabled = false;
			GetMrsBase("");
			GridUnit1.Enabled = true;
			panel2.Enabled = true;
			Cursor = Cursors.Default;
			FM_INFO.Close();
			FM_INFO.Dispose();
			Tab_B.Tab.Selected = true;
		}
	}

	private void GridUnit1_MouseMove(object sender, MouseEventArgs e)
	{
		int rowIndex = GridUnit1.MouseRow;
		GridUnit1.Row = rowIndex;
		GridUnit1.Select();
	}

	private void BtnGoHomeB_Click(object sender, EventArgs e)
	{
		Tab_A.Tab.Selected = true;
	}

	private void panel9_Resize(object sender, EventArgs e)
	{
		BtnAdd.Left = panel9.Width / 2 - BtnAdd.Width - 5;
		BtnRemove.Left = panel9.Width / 2 + 5;
	}

	private void BtnAdd_Click(object sender, EventArgs e)
	{
		for (int i = 1; i < c1FlexGrid1.Rows.Count; i++)
		{
			if (c1FlexGrid1.Rows[i].Selected)
			{
				if (CheckIsAlreadyExist(c1FlexGrid1[i, "pccesCode"].ToString().Trim()) > -1)
				{
					MessageBox.Show(this, "有重覆項目:\n" + c1FlexGrid1[i, "pccesCode"].ToString().Trim() + " " + c1FlexGrid1[i, "cName"].ToString().Trim(), "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					AddIntoGrid2(i);
				}
			}
		}
		lblChoose.Text = "已選用工項(" + (c1FlexGrid2.Rows.Count - 1) + ")";
	}

	private void BtnRemove_Click(object sender, EventArgs e)
	{
		for (int i = c1FlexGrid2.Rows.Count - 1; i > 0; i--)
		{
			if (c1FlexGrid2.Rows[i].Selected)
			{
				c1FlexGrid2.RemoveItem(i);
			}
		}
		lblChoose.Text = "已選用工項(" + (c1FlexGrid2.Rows.Count - 1) + ")";
	}

	private int CheckIsAlreadyExist(string iPubCode)
	{
		int RetV = -1;
		for (int i = 1; i < c1FlexGrid2.Rows.Count; i++)
		{
			if (c1FlexGrid2[i, "pccesCode"].ToString().Trim() == iPubCode)
			{
				RetV = i;
				break;
			}
		}
		return RetV;
	}

	private void AddIntoGrid2(int IndicateRow)
	{
		c1FlexGrid2.Rows.Count++;
		for (int i = 0; i < c1FlexGrid1.Cols.Count; i++)
		{
			c1FlexGrid2[c1FlexGrid2.Rows.Count - 1, c1FlexGrid1.Cols[i].Name] = c1FlexGrid1[IndicateRow, i];
		}
		c1FlexGrid2[c1FlexGrid2.Rows.Count - 1, "DbName"] = F_dbName;
	}

	private void c1FlexGrid1_DoubleClick(object sender, EventArgs e)
	{
		BtnAdd_Click(this, EventArgs.Empty);
	}

	private void D_Btn_Fnsh_Click(object sender, EventArgs e)
	{
		DataTable DT_WItems = new DataTable("SrcMrsBase");
		DT_WItems.Columns.Add("PccesCode", Type.GetType("System.String"));
		DT_WItems.Columns.Add("Analysis", Type.GetType("System.String"));
		DT_WItems.Columns.Add("cName", Type.GetType("System.String"));
		DT_WItems.Columns.Add("PubCode", Type.GetType("System.String"));
		DT_WItems.Columns.Add("DbName", Type.GetType("System.String"));
		DT_WItems.Columns.Add("ProjectCode", Type.GetType("System.String"));
		if (c1FlexGrid2.Rows.Count > 1)
		{
			for (int i = 1; i < c1FlexGrid2.Rows.Count; i++)
			{
				DataRow DR = DT_WItems.NewRow();
				DR["PccesCode"] = c1FlexGrid2[i, "PccesCode"].ToString().Trim();
				DR["Analysis"] = (((bool)c1FlexGrid2[i, "Analysis"]) ? "1" : "0");
				DR["cName"] = c1FlexGrid2[i, "cName"].ToString().Trim();
				DR["PubCode"] = c1FlexGrid2[i, "pubCode"].ToString().Trim();
				DR["DbName"] = c1FlexGrid2[i, "DbName"].ToString().Trim();
				DR["ProjectCode"] = "";
				DT_WItems.Rows.Add(DR);
			}
			FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
			FM_INFO._InfoString = " 資料轉入處理中，請稍候! ";
			FM_INFO.Show();
			Application.DoEvents();
			Cursor = Cursors.WaitCursor;
			panel6.Enabled = false;
			ClsPickFromOtherDB CLSPFO = new ClsPickFromOtherDB();
			CLSPFO._UserID = F_UserID;
			CLSPFO._CurrentDBName = F_CurrentDBName;
			CLSPFO._ActionName = PccesFormAction.MrsBase;
			CLSPFO._DT_SrcForProc = DT_WItems;
			if (!chk_reapt.Checked)
			{
				CLSPFO._DBProcessType = "0";
			}
			else
			{
				CLSPFO._DBProcessType = "1";
			}
			if (CLSPFO.ExecuteProcess(DT_WItems))
			{
				base.DialogResult = DialogResult.OK;
			}
			else
			{
				MessageBox.Show(this, "引用失敗，請重試!", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				panel6.Enabled = true;
			}
			CLSPFO = null;
			Cursor = Cursors.Default;
			FM_INFO.Close();
			FM_INFO.Dispose();
		}
		else
		{
			MessageBox.Show(this, "請先選定要引用的工項，再按確定執行轉入。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void A_Btn_Next_Click(object sender, EventArgs e)
	{
		GridUnit1_Click(sender, e);
	}

	private void Do_ToolBarFind()
	{
		if (c1FlexGrid1.Rows.Count <= 1)
		{
			return;
		}
		int iStart = c1FlexGrid1.Row + 1;
		string sSearchText = ((ComboBoxTool)ultraToolbarsManager1.Tools["mnu_Cbo1"]).Text.Trim();
		if (!CommonMethods.CheckValidString(sSearchText))
		{
			return;
		}
		if (F_KeyWord != sSearchText.Trim())
		{
			iStart = 1;
			F_KeyWord = sSearchText.Trim();
		}
		else
		{
			iStart = c1FlexGrid1.Row + 1;
		}
		if (sSearchText.Trim() == "")
		{
			return;
		}
		for (int i = iStart; i < c1FlexGrid1.Rows.Count; i++)
		{
			for (int j = 1; j < c1FlexGrid1.Cols.Count; j++)
			{
				if (!c1FlexGrid1.Cols[j].Visible || c1FlexGrid1[i, j] == null || c1FlexGrid1[i, j].ToString().IndexOf(sSearchText) <= -1)
				{
					continue;
				}
				c1FlexGrid1.Row = i;
				c1FlexGrid1.Select();
				int iFondCount = 0;
				int iListCount = ((ComboBoxTool)ultraToolbarsManager1.Tools["mnu_Cbo1"]).ValueList.ValueListItems.Count;
				for (int k = 0; k < iListCount; k++)
				{
					if (((ComboBoxTool)ultraToolbarsManager1.Tools["mnu_Cbo1"]).ValueList.ValueListItems[k].DisplayText.Trim() == sSearchText.Trim())
					{
						iFondCount++;
					}
				}
				if (iFondCount == 0)
				{
					((ComboBoxTool)ultraToolbarsManager1.Tools["mnu_Cbo1"]).ValueList.ValueListItems.Add(sSearchText, sSearchText);
				}
				return;
			}
		}
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		switch (e.Tool.Key)
		{
		case "mnu_Go":
			Do_ToolBarFind();
			break;
		case "mnuUsual":
			Do_Usual();
			break;
		case "mnuListAll":
			ExtraCri = "";
			GetMrsBase(ExtraCri);
			break;
		case "mnuAnalysis":
			SpecialFilter();
			break;
		case "mnuGeneral":
			SpecialFilter();
			break;
		case "PickType":
			Do_PickClass();
			((StateButtonTool)ultraToolbarsManager1.Tools["mnuGroup"]).Checked = true;
			break;
		}
	}

	private void Do_Usual()
	{
		ExtraCri = " show = '1'";
		GetMrsBase(ExtraCri);
	}

	private void SpecialFilter()
	{
		if ((ultraToolbarsManager1.Tools["mnuAnalysis"] as StateButtonTool).Checked && (ultraToolbarsManager1.Tools["mnuGeneral"] as StateButtonTool).Checked)
		{
			ExtraCri = "";
			GetMrsBase(ExtraCri);
		}
		else if (!(ultraToolbarsManager1.Tools["mnuAnalysis"] as StateButtonTool).Checked && (ultraToolbarsManager1.Tools["mnuGeneral"] as StateButtonTool).Checked)
		{
			ExtraCri = " analysis != '1' ";
			GetMrsBase(ExtraCri);
		}
		else if ((ultraToolbarsManager1.Tools["mnuAnalysis"] as StateButtonTool).Checked && !(ultraToolbarsManager1.Tools["mnuGeneral"] as StateButtonTool).Checked)
		{
			ExtraCri = " analysis = '1' ";
			GetMrsBase(ExtraCri);
		}
		else if (!(ultraToolbarsManager1.Tools["mnuAnalysis"] as StateButtonTool).Checked && !(ultraToolbarsManager1.Tools["mnuGeneral"] as StateButtonTool).Checked)
		{
			ExtraCri = "";
			GetMrsBase(ExtraCri);
		}
	}

	private void Do_PickClass()
	{
		FormBDGT_ItemClass FM_ITMSET_Class = new FormBDGT_ItemClass();
		FM_ITMSET_Class._UserID = F_UserID;
		FM_ITMSET_Class.Owner = this;
		FM_ITMSET_Class._status = "search2";
		if (FM_ITMSET_Class.ShowDialog() == DialogResult.OK)
		{
			ExtraCri = Do_PickType();
			GetMrsBase(ExtraCri);
		}
		FM_ITMSET_Class.Close();
		FM_ITMSET_Class.Dispose();
		FM_ITMSET_Class = null;
	}

	private string Do_PickType()
	{
		string RetV = " and 1=1 ";
		DataTable DTClass = new DataTable();
		string sNum = CommonMethods.IniReadValue(F_SettingPick, "PickType", "PickName");
		string strpubCode = "";
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("(UserDefind_Show) 顯示常用字串資料");
		if (sNum.Length > 0)
		{
			string ls_selectstr = "select Distinct A.* from mrsA A inner join MrsY B on A.pubcode=B.pubcode where B.numberCode in (" + sNum + ")";
			ModifyDB StdCom = new ModifyDB("", aArr);
			DTClass = StdCom.DBList(ls_selectstr);
			StdCom = null;
			if (DTClass.Rows.Count > 0)
			{
				for (int i = 0; i < DTClass.Rows.Count; i++)
				{
					strpubCode = strpubCode + DTClass.Rows[i]["pubCode"].ToString().Trim() + ",";
				}
			}
			if (strpubCode.Length > 0)
			{
				strpubCode = strpubCode.Substring(0, strpubCode.Length - 1);
			}
		}
		if (strpubCode.Length > 0)
		{
			return " pubCode in (" + strpubCode + ") ";
		}
		return " pubCode = ''";
	}

	private void ultraToolbarsManager1_ToolKeyPress(object sender, ToolKeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && e.Tool.Key == "mnu_Cbo1")
		{
			Do_ToolBarFind();
		}
	}

	private void ultraToolbarsManager1_AfterToolActivate(object sender, ToolEventArgs e)
	{
		if (e.Tool.Key == "mnu_Cbo1")
		{
			((ButtonTool)ultraToolbarsManager1.Tools["mnuDelete"]).SharedProps.Shortcut = Shortcut.None;
		}
		else
		{
			((ButtonTool)ultraToolbarsManager1.Tools["mnuDelete"]).SharedProps.Shortcut = Shortcut.Del;
		}
	}

	private void ultraToolbarsManager1_AfterToolDeactivate(object sender, ToolEventArgs e)
	{
		((ButtonTool)ultraToolbarsManager1.Tools["mnuDelete"]).SharedProps.Shortcut = Shortcut.Del;
	}

	private void ultraToolbarsManager1_BeforeToolbarListDropdown(object sender, BeforeToolbarListDropdownEventArgs e)
	{
		e.Cancel = true;
	}

	private void chk_reapt_CheckedChanged(object sender, EventArgs e)
	{
		if (chk_reapt.Checked)
		{
			CommonMethods.WriteIniValue("chk_reapt", "State", "True");
		}
		else
		{
			CommonMethods.WriteIniValue("chk_reapt", "State", "False");
		}
	}

	private void FormMrsBase_PickFromOtherDB_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormMrsBase_PickFromOtherDB");
		}
	}

	private void FormMrsBase_PickFromOtherDB_FormClosing(object sender, FormClosingEventArgs e)
	{
		SysUser oSysUser = new SysUser();
		oSysUser.SetSysUserDatabaseName(F_UserID, F_CurrentDBName);
	}

	private void ultraTree1_AfterSelect(object sender, SelectEventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		if (e.NewSelections[0].Key != "ROOT")
		{
			if (e.NewSelections[0].Key.Length >= 2)
			{
				if (e.NewSelections[0].Key.Substring(0, 2).Trim() != F_NowKey || c1FlexGrid1.Rows.Count <= 1)
				{
					F_NowKey = e.NewSelections[0].Key.Substring(0, 2);
					ExtraCri = " Left(pccesCode,2) = '" + F_NowKey + "' ";
					if (((StateButtonTool)ultraToolbarsManager1.Tools["mnuGroup"]).Checked)
					{
						GetMrsBase(ExtraCri);
						ultraTree1_Click(sender, EventArgs.Empty);
					}
					else
					{
						for (int i = 1; i < c1FlexGrid1.Rows.Count; i++)
						{
							if (c1FlexGrid1[i, "PccesCode"] != null && c1FlexGrid1[i, "PccesCode"].ToString().IndexOf(F_NowKey) > -1)
							{
								c1FlexGrid1.Row = i;
								c1FlexGrid1.Select();
								c1FlexGrid1.TopRow = i;
								break;
							}
						}
					}
				}
			}
			else if (e.NewSelections[0].Key.Substring(0, 1).Trim() != F_NowKey || c1FlexGrid1.Rows.Count <= 1)
			{
				F_NowKey = e.NewSelections[0].Key.Substring(0, 1);
				if (F_NowKey == "M")
				{
					ExtraCri = " Left(pccesCode,1) = '" + F_NowKey + "' or  Left(pccesCode,1) = 'm' ";
				}
				else if (F_NowKey == "L")
				{
					ExtraCri = " Left(pccesCode,1) = '" + F_NowKey + "' or  Left(pccesCode,1) = 'l' ";
				}
				else if (F_NowKey == "E")
				{
					ExtraCri = " Left(pccesCode,1) = '" + F_NowKey + "' or  Left(pccesCode,1) = 'e' ";
				}
				else if (F_NowKey == "W")
				{
					ExtraCri = " Left(pccesCode,1) = '" + F_NowKey + "' or  Left(pccesCode,1) = 'w' ";
				}
				else
				{
					ExtraCri = " Left(pccesCode,1) = '" + F_NowKey + "' ";
				}
				if (((StateButtonTool)ultraToolbarsManager1.Tools["mnuGroup"]).Checked)
				{
					GetMrsBase(ExtraCri);
				}
				else
				{
					for (int i = 1; i < c1FlexGrid1.Rows.Count; i++)
					{
						if (c1FlexGrid1[i, "PccesCode"] != null && c1FlexGrid1[i, "PccesCode"].ToString().IndexOf(F_NowKey) > -1)
						{
							c1FlexGrid1.Row = i;
							c1FlexGrid1.Select();
							c1FlexGrid1.TopRow = i;
							break;
						}
					}
				}
			}
		}
		Cursor = Cursors.Default;
	}
}
