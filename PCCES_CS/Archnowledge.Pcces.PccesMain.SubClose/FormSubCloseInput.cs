using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.CTRClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.STDClass;
using AxThreed;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.SubClose;

public class FormSubCloseInput : Form
{
	private UltraToolbarsManager ultraToolbarsManager1;

	private IContainer components;

	private Panel panel1;

	private UltraButton D_Btn_Fnsh;

	private GroupBox groupBox1;

	private Panel panel2;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Bottom;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Right;

	private GridBudget gridBudget1;

	private Panel panel7;

	private UltraLabel lblTotal;

	private UltraLabel ultraLabel8;

	private AxSSPanel axSSPanel2;

	private UltraStatusBar ultraStatusBar1;

	private ImageList imageList2;

	private UltraButton A_Btn_Cncl;

	private string F_UserID;

	private string F_ProjectCode;

	private string F_SubProjectCode = "";

	private string F_Queue = "9999";

	private string ls_prjcode;

	private string ls_subproj;

	private string ls_Queue;

	private DataTable MfqDT;

	private bool lb_Lock = false;

	private int GridCols;

	private object[,] GridColsSquence;

	private string FORM_STATUS = "INI";

	private int F_MainQty = 0;

	private int F_MainCst = 0;

	private int F_MainAmt = 0;

	private int F_AnaQty = 0;

	private int F_AnaCst = 0;

	private int F_AnaAmt = 0;

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

	public string _SubProjectCode
	{
		get
		{
			return F_SubProjectCode;
		}
		set
		{
			F_SubProjectCode = value;
		}
	}

	public string _Queue
	{
		get
		{
			return F_Queue;
		}
		set
		{
			F_Queue = value;
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.SubClose.FormSubCloseInput));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.OptionSet optionSet1 = new Infragistics.Win.UltraWinToolbars.OptionSet("switch");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCalcuInput");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuCalcu");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnuLevel");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool1 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_1", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool2 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_2", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool3 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_3", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool4 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_4", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool5 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_5", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool6 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_6", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool7 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_7", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool8 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_8", "switch");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar2 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Menu1");
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool2 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuFile_CNT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuClose");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool3 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuEdit_CNT");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool4 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuCalcu");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool5 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuView_CNT");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool6 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuTool_CNT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuClose");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool7 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuCalcu");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCalcuInv");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCalcuCnt");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCalcuInput");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCalcuInv");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCalcuCnt");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCalcuInput");
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnuLevel");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool9 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_1", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool10 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_2", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool11 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_3", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool12 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_4", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool13 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_5", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool14 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_6", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool15 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_7", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool16 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_8", "switch");
		this.panel1 = new System.Windows.Forms.Panel();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.D_Btn_Fnsh = new Infragistics.Win.Misc.UltraButton();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.panel2 = new System.Windows.Forms.Panel();
		this.gridBudget1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.panel7 = new System.Windows.Forms.Panel();
		this.lblTotal = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.axSSPanel2 = new AxThreed.AxSSPanel();
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this._FormSys_B_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this._FormSys_B_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridBudget1).BeginInit();
		this.panel7.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.axSSPanel2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.A_Btn_Cncl);
		this.panel1.Controls.Add(this.D_Btn_Fnsh);
		this.panel1.Controls.Add(this.groupBox1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 513);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(792, 40);
		this.panel1.TabIndex = 12;
		this.A_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Cncl.Appearance = appearance1;
		this.A_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Cncl.Location = new System.Drawing.Point(696, 7);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.ShowFocusRect = false;
		this.A_Btn_Cncl.ShowOutline = false;
		this.A_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Cncl.SupportThemes = false;
		this.A_Btn_Cncl.TabIndex = 5;
		this.A_Btn_Cncl.Text = "取消";
		this.D_Btn_Fnsh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Fnsh.Appearance = appearance2;
		this.D_Btn_Fnsh.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Fnsh.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Fnsh.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.D_Btn_Fnsh.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Fnsh.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Fnsh.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Fnsh.Location = new System.Drawing.Point(607, 7);
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
		this.groupBox1.Size = new System.Drawing.Size(792, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.panel2.Controls.Add(this.gridBudget1);
		this.panel2.Controls.Add(this.panel7);
		this.panel2.Controls.Add(this.ultraStatusBar1);
		this.panel2.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Bottom);
		this.panel2.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Left);
		this.panel2.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Right);
		this.panel2.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Top);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(792, 513);
		this.panel2.TabIndex = 13;
		this.gridBudget1._ExcelFileName = "";
		this.gridBudget1._ExcelSheeName = "";
		this.gridBudget1._IsOpenExcelAfterExport = false;
		this.gridBudget1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridBudget1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.gridBudget1.ColumnInfo = resources.GetString("gridBudget1.ColumnInfo");
		this.ultraToolbarsManager1.SetContextMenuUltra(this.gridBudget1, "PopCNT");
		this.gridBudget1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridBudget1.ExtendLastCol = true;
		this.gridBudget1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridBudget1.ForeColor = System.Drawing.Color.Black;
		this.gridBudget1.Location = new System.Drawing.Point(0, 25);
		this.gridBudget1.Name = "gridBudget1";
		this.gridBudget1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridBudget1.ShowCursor = true;
		this.gridBudget1.ShowSort = false;
		this.gridBudget1.ShowToolTipOnNarrowColumn = true;
		this.gridBudget1.Size = new System.Drawing.Size(792, 434);
		this.gridBudget1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridBudget1.Styles"));
		this.gridBudget1.TabIndex = 11;
		this.gridBudget1.Tree.Column = 1;
		this.gridBudget1.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridBudget1.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(gridBudget1_AfterRowColChange);
		this.panel7.Controls.Add(this.lblTotal);
		this.panel7.Controls.Add(this.ultraLabel8);
		this.panel7.Controls.Add(this.axSSPanel2);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel7.Location = new System.Drawing.Point(0, 459);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(792, 28);
		this.panel7.TabIndex = 13;
		this.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance11.ForeColor = System.Drawing.Color.Blue;
		appearance11.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance11.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblTotal.Appearance = appearance11;
		this.lblTotal.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.lblTotal.Font = new System.Drawing.Font("Courier New", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTotal.Location = new System.Drawing.Point(64, 5);
		this.lblTotal.Name = "lblTotal";
		this.lblTotal.Size = new System.Drawing.Size(679, 19);
		this.lblTotal.TabIndex = 14;
		appearance12.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		appearance12.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel8.Appearance = appearance12;
		this.ultraLabel8.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.ultraLabel8.Font = new System.Drawing.Font("Courier New", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.ultraLabel8.Location = new System.Drawing.Point(4, 5);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(74, 19);
		this.ultraLabel8.TabIndex = 13;
		this.ultraLabel8.Text = "總計：";
		this.axSSPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.axSSPanel2.Location = new System.Drawing.Point(0, 0);
		this.axSSPanel2.Name = "axSSPanel2";
		this.axSSPanel2.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("axSSPanel2.OcxState");
		this.axSSPanel2.Size = new System.Drawing.Size(792, 28);
		this.axSSPanel2.TabIndex = 1;
		appearance13.FontData.SizeInPoints = 11f;
		appearance13.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraStatusBar1.Appearance = appearance13;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 487);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		this.ultraStatusBar1.Padding = new Infragistics.Win.UltraWinStatusBar.UIElementMargins(0, 2, 0, 0);
		appearance14.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraStatusPanel1.Appearance = appearance14;
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Key = "RowsCount";
		ultraStatusPanel1.Text = "資料筆數：";
		ultraStatusPanel1.Width = 200;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.Key = "ProgressBar";
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[2] { ultraStatusPanel1, ultraStatusPanel2 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(792, 26);
		this.ultraStatusBar1.TabIndex = 12;
		this.ultraStatusBar1.Text = "ultraStatusBar1";
		this._FormSys_B_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 513);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Name = "_FormSys_B_Toolbars_Dock_Area_Bottom";
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(792, 0);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		appearance15.FontData.Name = "Arial";
		appearance15.FontData.SizeInPoints = 9f;
		this.ultraToolbarsManager1.Appearance = appearance15;
		appearance16.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance16.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.DockAreaAppearance = appearance16;
		this.ultraToolbarsManager1.DockWithinContainer = this;
		this.ultraToolbarsManager1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraToolbarsManager1.LockToolbars = true;
		appearance17.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance17.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance17.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.MenuSettings.HotTrackAppearance = appearance17;
		appearance18.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance18.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsManager1.MenuSettings.IconAreaAppearance = appearance18;
		appearance19.BackColor = System.Drawing.Color.White;
		appearance19.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.MenuSettings.ToolAppearance = appearance19;
		optionSet1.AllowAllUp = false;
		this.ultraToolbarsManager1.OptionSets.Add(optionSet1);
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager1.ShowQuickCustomizeButton = false;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
		ultraToolbar1.Settings.AllowDockTop = Infragistics.Win.DefaultableBoolean.True;
		ultraToolbar1.Text = "Tool1";
		popupMenuTool1.InstanceProps.IsFirstInGroup = true;
		labelTool1.InstanceProps.IsFirstInGroup = true;
		stateButtonTool1.Checked = true;
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[11]
		{
			buttonTool1, popupMenuTool1, labelTool1, stateButtonTool1, stateButtonTool2, stateButtonTool3, stateButtonTool4, stateButtonTool5, stateButtonTool6, stateButtonTool7,
			stateButtonTool8
		});
		ultraToolbar2.DockedColumn = 0;
		ultraToolbar2.DockedRow = 0;
		ultraToolbar2.Text = "Menu1";
		ultraToolbar2.Visible = false;
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[2] { ultraToolbar1, ultraToolbar2 });
		appearance20.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance20.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance20;
		appearance21.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance21.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance21.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance21;
		popupMenuTool2.SharedProps.Caption = "檔案(&F)";
		popupMenuTool2.SharedProps.Category = "合約";
		buttonTool2.InstanceProps.IsFirstInGroup = true;
		popupMenuTool2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[1] { buttonTool2 });
		popupMenuTool3.SharedProps.Caption = "編輯(&E)";
		popupMenuTool3.SharedProps.Category = "合約";
		popupMenuTool4.InstanceProps.IsFirstInGroup = true;
		popupMenuTool3.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[1] { popupMenuTool4 });
		popupMenuTool5.SharedProps.Caption = "檢視(&V)";
		popupMenuTool5.SharedProps.Category = "合約";
		popupMenuTool6.SharedProps.Caption = "工具(&T)";
		popupMenuTool6.SharedProps.Category = "合約";
		buttonTool3.SharedProps.Caption = "結束";
		buttonTool3.SharedProps.Category = "合約";
		popupMenuTool7.SharedProps.Caption = "編輯結算數量/金額";
		popupMenuTool7.SharedProps.Category = "編輯";
		popupMenuTool7.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool6.InstanceProps.IsFirstInGroup = true;
		popupMenuTool7.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool4, buttonTool5, buttonTool6 });
		buttonTool7.SharedProps.Caption = "填入估驗數量/金額";
		buttonTool7.SharedProps.Category = "編輯";
		buttonTool8.SharedProps.Caption = "填入契約數量/金額";
		buttonTool8.SharedProps.Category = "編輯";
		appearance22.Image = resources.GetObject("appearance10.Image");
		buttonTool9.SharedProps.AppearancesSmall.Appearance = appearance22;
		buttonTool9.SharedProps.Caption = "重新總計";
		buttonTool9.SharedProps.Category = "編輯";
		buttonTool9.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		labelTool2.SharedProps.Caption = "階層:";
		stateButtonTool9.Checked = true;
		stateButtonTool9.OptionSetKey = "switch";
		stateButtonTool9.SharedProps.Caption = "1";
		stateButtonTool9.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool10.OptionSetKey = "switch";
		stateButtonTool10.SharedProps.Caption = "2";
		stateButtonTool10.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool11.OptionSetKey = "switch";
		stateButtonTool11.SharedProps.Caption = "3";
		stateButtonTool11.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool12.OptionSetKey = "switch";
		stateButtonTool12.SharedProps.Caption = "4";
		stateButtonTool12.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool13.OptionSetKey = "switch";
		stateButtonTool13.SharedProps.Caption = "5";
		stateButtonTool13.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool14.OptionSetKey = "switch";
		stateButtonTool14.SharedProps.Caption = "6";
		stateButtonTool14.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool15.OptionSetKey = "switch";
		stateButtonTool15.SharedProps.Caption = "7";
		stateButtonTool15.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool16.OptionSetKey = "switch";
		stateButtonTool16.SharedProps.Caption = "8";
		stateButtonTool16.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[18]
		{
			popupMenuTool2, popupMenuTool3, popupMenuTool5, popupMenuTool6, buttonTool3, popupMenuTool7, buttonTool7, buttonTool8, buttonTool9, labelTool2,
			stateButtonTool9, stateButtonTool10, stateButtonTool11, stateButtonTool12, stateButtonTool13, stateButtonTool14, stateButtonTool15, stateButtonTool16
		});
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this._FormSys_B_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormSys_B_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 25);
		this._FormSys_B_Toolbars_Dock_Area_Left.Name = "_FormSys_B_Toolbars_Dock_Area_Left";
		this._FormSys_B_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 488);
		this._FormSys_B_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormSys_B_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(792, 25);
		this._FormSys_B_Toolbars_Dock_Area_Right.Name = "_FormSys_B_Toolbars_Dock_Area_Right";
		this._FormSys_B_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 488);
		this._FormSys_B_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormSys_B_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormSys_B_Toolbars_Dock_Area_Top.Name = "_FormSys_B_Toolbars_Dock_Area_Top";
		this._FormSys_B_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(792, 25);
		this._FormSys_B_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.White;
		this.imageList2.Images.SetKeyName(0, "");
		this.imageList2.Images.SetKeyName(1, "");
		this.imageList2.Images.SetKeyName(2, "");
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(792, 553);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		base.KeyPreview = true;
		base.Name = "FormSubCloseInput";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "自行輸入數量/金額";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(FormSubCloseInput_Load);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridBudget1).EndInit();
		this.panel7.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.axSSPanel2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
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

	public FormSubCloseInput()
	{
		InitializeComponent();
		GridCols = gridBudget1.Cols.Count;
		GridColsSquence = new object[GridCols, 10];
		gridBudget1.Glyphs[GlyphEnum.Checked] = imageList2.Images[0];
		gridBudget1.Glyphs[GlyphEnum.Unchecked] = imageList2.Images[1];
		CellStyle cs = gridBudget1.Styles.Add("img");
		cs.DataType = typeof(Image);
		CellStyle cs2 = gridBudget1.Styles.Add("EditMode");
		cs2.DataType = typeof(Image);
		cs2.ImageAlign = ImageAlignEnum.RightCenter;
	}

	private void FormSubCloseInput_Load(object sender, EventArgs e)
	{
		ls_Queue = F_Queue;
		SettingDecimal();
		LoadData();
	}

	private void LoadData()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(SubClose) 契約結算");
		ls_prjcode = F_ProjectCode;
		ls_subproj = F_SubProjectCode;
		sub_acc AccCom = new sub_acc(tmp_AL1);
		lb_Lock = AccCom.GetLockMode(ls_Queue, ls_subproj, ls_prjcode);
		submfq MfqCom = new submfq(tmp_AL1);
		MfqDT = MfqCom.ListCloseItem("", ls_Queue, ls_subproj, ls_prjcode);
		MfqCom = null;
		if (lb_Lock)
		{
		}
		AccCom.ps_prjcode = ls_prjcode;
		AccCom.ps_subcode = ls_subproj;
		AccCom.ps_queue = ls_Queue;
		AccCom.ps_date_insp = PubTools.ChgDateStr(DateTime.Now.ToString());
		AccCom.ps_date_rece = PubTools.ChgDateStr(DateTime.Now.ToString());
		AccCom.ps_this_prec = "0";
		AccCom.InseItem();
		AccCom = null;
		PubTools.WriteRoughlyLog(tmp_AL1);
		BindToGrid();
	}

	private void BindToGrid()
	{
		int iLevel = 0;
		FORM_STATUS = "BINDING";
		DataTable DT1 = MfqDT.Copy();
		ultraToolbarsManager1.BeginUpdate();
		ultraToolbarsManager1.Enabled = false;
		RememberColsProps();
		CellStyle CS1 = gridBudget1.Styles.Add("AnalysisColor");
		CellStyle CS9 = gridBudget1.Styles.Add("IsSharedColor");
		CellStyle CS10 = gridBudget1.Styles.Add("MainColor");
		CS1.ForeColor = Color.Red;
		CS10.ForeColor = Color.Blue;
		CS9.ForeColor = Color.Plum;
		gridBudget1.Clear(ClearFlags.All);
		gridBudget1.Select(0, 0);
		ultraStatusBar1.Panels[0].Text = "資料筆數：" + DT1.Rows.Count;
		int iRows = DT1.Rows.Count + 1;
		gridBudget1.Rows.Count = iRows;
		SetGridColumn();
		double aTotal = 0.0;
		for (int i = 0; i < DT1.Rows.Count; i++)
		{
			string sKind = DT1.Rows[i]["Kind"].ToString().Trim();
			switch (sKind)
			{
			default:
				if (!(sKind == "U"))
				{
					break;
				}
				goto case "B";
			case "B":
			case "L":
			case "F":
			case "S":
			case "Z":
				gridBudget1.Rows[i + 1].Style = gridBudget1.Styles["MainColor"];
				break;
			}
			gridBudget1[i + 1, "Kind"] = DT1.Rows[i]["Kind"].ToString().Trim();
			gridBudget1[i + 1, "ItemNo"] = DT1.Rows[i]["ItemNo"].ToString().Trim();
			gridBudget1[i + 1, "CName"] = DT1.Rows[i]["cName"].ToString().Trim();
			gridBudget1[i + 1, "UnitName"] = ((DT1.Rows[i]["ItemUnit"] == DBNull.Value) ? "" : DT1.Rows[i]["ItemUnit"].ToString().Trim());
			gridBudget1[i + 1, "ItemQty"] = DT1.Rows[i]["itemqty"];
			gridBudget1[i + 1, "ItemAmt"] = PubTools.Str2Double(DT1.Rows[i]["itemqty"]) * PubTools.Str2Double(DT1.Rows[i]["itemcost"]);
			gridBudget1[i + 1, "AccQty"] = DT1.Rows[i]["Acc_Qty"];
			gridBudget1[i + 1, "AccAmt"] = DT1.Rows[i]["Acc_Amt"];
			gridBudget1[i + 1, "Acc_Prec"] = string.Format("{0:N2}", DT1.Rows[i]["Acc_Prec"]) + "%";
			gridBudget1[i + 1, "Cost"] = DT1.Rows[i]["itemCost"];
			gridBudget1[i + 1, "Pre_Qty"] = PubTools.Str2Double(DT1.Rows[i]["Pre_Qty"]);
			gridBudget1[i + 1, "Pre_Amt"] = PubTools.Str2Double(DT1.Rows[i]["Pre_Amt"]);
			gridBudget1[i + 1, "ChgQty"] = DT1.Rows[i]["chgqty"];
			gridBudget1[i + 1, "ChgAmt"] = PubTools.Str2Double(DT1.Rows[i]["chgqty"]) * PubTools.Str2Double(DT1.Rows[i]["chgcost"]);
			gridBudget1[i + 1, "PrintNo"] = DT1.Rows[i]["itemdes"].ToString().Trim();
			double Diff = PubTools.Str2Double(PubTools.ARound(DT1.Rows[i]["Acc_Amt"], F_MainAmt)) - PubTools.Str2Double(PubTools.ARound(DT1.Rows[i]["itemqty"], F_MainQty)) * PubTools.Str2Double(PubTools.ARound(DT1.Rows[i]["itemcost"], F_MainCst));
			gridBudget1[i + 1, "Diff"] = Diff;
			if (Diff < 0.0)
			{
				CellRange cg = gridBudget1.GetCellRange(i + 1, gridBudget1.Cols["Diff"].SafeIndex, i + 1, gridBudget1.Cols["Diff"].SafeIndex);
				cg.Style = CS1;
			}
			gridBudget1.Rows[i + 1].IsNode = true;
			if (DT1.Rows[i]["itemdes"].ToString().Trim() == "".PadLeft(32, '9'))
			{
				gridBudget1.Rows[i + 1].Node.Level = 1;
				aTotal = PubTools.Str2Double(PubTools.ARound(DT1.Rows[i]["Acc_Amt"], F_MainAmt));
			}
			else
			{
				gridBudget1.Rows[i + 1].Node.Level = Convert.ToInt32(DT1.Rows[i]["itemdes"].ToString().Trim().Length / 4);
			}
			if (gridBudget1.Rows[i + 1].Node.Level > iLevel)
			{
				iLevel = gridBudget1.Rows[i + 1].Node.Level;
			}
		}
		SwitchToCorrectLevelStatus(iLevel);
		SetColsEditSymbol();
		lblTotal.Text = string.Format("{0:N" + F_MainAmt + "}", aTotal);
		ultraToolbarsManager1.Enabled = true;
		ultraToolbarsManager1.EndUpdate();
		FORM_STATUS = "ACT";
	}

	private void SettingDecimal()
	{
		DataTable DTDecimal = new DataTable();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add(CommonMethods.GetFormTypeTitle(FormType.MrsBaseAnalysis));
		PubDecimal dbDecimal = new PubDecimal(aArr);
		dbDecimal.ps_projectCode = F_ProjectCode;
		DTDecimal = dbDecimal.ListItem("", F_ProjectCode);
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
			GridColsSquence[i, 0] = gridBudget1.Cols[i].Name;
			GridColsSquence[i, 1] = gridBudget1.Cols[i].Caption;
			GridColsSquence[i, 2] = gridBudget1.Cols[i].Width;
			if (gridBudget1.Cols[i].Name == "AnaImg")
			{
				GridColsSquence[i, 3] = typeof(Image);
			}
			else
			{
				GridColsSquence[i, 3] = gridBudget1.Cols[i].DataType;
			}
			GridColsSquence[i, 4] = gridBudget1.Cols[i].Visible;
			GridColsSquence[i, 5] = gridBudget1.Cols[i].Format;
			GridColsSquence[i, 6] = gridBudget1.Cols[i].AllowEditing;
			if (gridBudget1.Cols[i].Name == "ItemQty" || gridBudget1.Cols[i].Name == "AccQty" || gridBudget1.Cols[i].Name == "Pre_Qty" || gridBudget1.Cols[i].Name == "ChgQty")
			{
				if (F_MainQty > 0)
				{
					GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_MainQty, '0');
				}
				else
				{
					GridColsSquence[i, 5] = "###,###,###,##0";
				}
			}
			if (gridBudget1.Cols[i].Name == "Cost")
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
			if (gridBudget1.Cols[i].Name == "ItemAmt" || gridBudget1.Cols[i].Name == "AccAmt" || gridBudget1.Cols[i].Name == "Diff" || gridBudget1.Cols[i].Name == "Pre_Amt" || gridBudget1.Cols[i].Name == "ChgAmt")
			{
				if (F_MainAmt > 0)
				{
					GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_MainAmt, '0');
				}
				else
				{
					GridColsSquence[i, 5] = "###,###,###,##0";
				}
			}
			GridColsSquence[i, 7] = gridBudget1.Cols[i].TextAlign;
		}
	}

	private void SetGridColumn()
	{
		for (int i = 0; i < GridCols; i++)
		{
			gridBudget1.Cols[i].Name = (string)GridColsSquence[i, 0];
			gridBudget1.Cols[i].Caption = (string)GridColsSquence[i, 1];
			gridBudget1.Cols[i].Width = (int)GridColsSquence[i, 2];
			gridBudget1.Cols[i].DataType = (Type)GridColsSquence[i, 3];
			gridBudget1.Cols[i].Visible = (bool)GridColsSquence[i, 4];
			gridBudget1.Cols[i].Format = (string)GridColsSquence[i, 5];
			gridBudget1.Cols[i].AllowEditing = (bool)GridColsSquence[i, 6];
			gridBudget1.Cols[i].TextAlign = (TextAlignEnum)GridColsSquence[i, 7];
		}
	}

	private void SetColsEditSymbol()
	{
		for (int i = 1; i < gridBudget1.Cols.Count; i++)
		{
			if (gridBudget1.Cols[i].AllowEditing)
			{
				CellRange rg = gridBudget1.GetCellRange(0, i);
				rg.Style = gridBudget1.Styles["EditMode"];
				rg.Image = imageList2.Images[2];
			}
		}
	}

	private void D_Btn_Fnsh_Click(object sender, EventArgs e)
	{
		SaveDataToTable();
		SaveToDB();
		base.DialogResult = DialogResult.OK;
	}

	private void SaveDataToTable()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(SubClose) 契約結算--存檔");
		submfq MfqCom = new submfq(tmp_AL1);
		for (int i = 1; i < gridBudget1.Rows.Count; i++)
		{
			double ld_ItemQty = PubTools.Str2Double(gridBudget1[i, "ItemQty"]);
			double ld_ItemCost = PubTools.Str2Double(gridBudget1[i, "Cost"]);
			string ls_Unit = gridBudget1[i, "UnitName"].ToString().Trim();
			double ld_qty = 0.0;
			double ld_Amt = 0.0;
			double ld_Accqty = PubTools.Str2Double(gridBudget1[i, "Pre_Qty"]);
			double ld_Acccost = PubTools.Str2Double(gridBudget1[i, "Pre_Amt"]);
			if (ld_ItemQty == 1.0 && ls_Unit == "式")
			{
				ld_qty = ld_Accqty + 1.0;
				ld_Amt = PubTools.Str2Double(gridBudget1[i, "AccAmt"]);
			}
			else
			{
				ld_qty = PubTools.Str2Double(gridBudget1[i, "AccQty"]);
				ld_Amt = PubTools.ARound(ld_ItemCost * ld_qty, 2L);
			}
			DataRow dr1 = MfqDT.Rows[i - 1];
			dr1["quantity"] = ld_qty - ld_Accqty;
			dr1["tom_amt"] = ld_Amt - ld_Acccost;
			dr1["Acc_Qty"] = ld_qty;
			dr1["Acc_Amt"] = ld_Amt;
		}
	}

	private void SaveToDB()
	{
		Cursor = Cursors.WaitCursor;
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(SubClose) 契約結算");
		sub_acc AccCom = new sub_acc(tmp_AL1);
		MfqDT = AccCom.ReTotal2(MfqDT, ls_Queue, ls_subproj, ls_prjcode);
		AccCom = null;
		submfq MfqCom = new submfq(tmp_AL1);
		foreach (DataRow dr in MfqDT.Rows)
		{
			MfqCom.ps_quantity = dr["quantity"].ToString();
			MfqCom.ps_tom_amt = dr["tom_amt"].ToString();
			MfqCom.ps_itemdes = dr["itemdes"].ToString();
			MfqCom.ps_itemno = dr["qucode"].ToString();
			MfqCom.ps_prjcode = dr["project"].ToString();
			MfqCom.ps_subcode = dr["sproj"].ToString();
			MfqCom.UpdItem();
		}
		MfqCom = null;
		Cursor = Cursors.Default;
	}

	private void SaveToDB_Inv()
	{
		Cursor = Cursors.WaitCursor;
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(SubClose) 契約結算");
		submfq MfqCom = new submfq(tmp_AL1);
		MfqDT = MfqCom.ListItem("", ls_Queue, ls_subproj, ls_prjcode);
		foreach (DataRow dr in MfqDT.Rows)
		{
			MfqCom.ps_quantity = "0";
			MfqCom.ps_tom_amt = "0";
			MfqCom.ps_itemdes = dr["itemdes"].ToString();
			MfqCom.ps_itemno = dr["qucode"].ToString();
			MfqCom.ps_prjcode = dr["project"].ToString();
			MfqCom.ps_subcode = dr["sproj"].ToString();
			MfqCom.UpdItem();
		}
		MfqCom = null;
		Cursor = Cursors.Default;
	}

	private void SaveToDB_Cnt()
	{
		Cursor = Cursors.WaitCursor;
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(SubClose) 契約結算");
		submfq MfqCom = new submfq(tmp_AL1);
		MfqDT = MfqCom.ListCloseItem("", ls_Queue, ls_subproj, ls_prjcode);
		foreach (DataRow dr in MfqDT.Rows)
		{
			double ld_qty = PubTools.Str2Double(dr["chgqty"].ToString());
			double ld_cost = PubTools.Str2Double(dr["chgcost"].ToString());
			double ld_Amt = PubTools.ARound(PubTools.ARound(ld_qty, F_MainQty) * PubTools.ARound(ld_cost, F_MainCst), 2L);
			double ld_Accqty = PubTools.Str2Double(dr["Pre_Qty"].ToString());
			double ld_Acccost = PubTools.Str2Double(dr["Pre_Amt"].ToString());
			MfqCom.ps_quantity = (ld_qty - ld_Accqty).ToString();
			MfqCom.ps_tom_amt = (ld_Amt - ld_Acccost).ToString();
			MfqCom.ps_itemdes = dr["itemdes"].ToString();
			MfqCom.ps_itemno = dr["qucode"].ToString();
			MfqCom.ps_prjcode = dr["project"].ToString();
			MfqCom.ps_subcode = dr["sproj"].ToString();
			MfqCom.UpdItem();
		}
		MfqCom = null;
		Cursor = Cursors.Default;
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		Do_MenuAction(e.Tool.Key);
	}

	private void Do_MenuAction(string KeyID)
	{
		switch (KeyID)
		{
		case "mnuCalcuInv":
			Do_CalcuInv();
			break;
		case "mnuCalcuCnt":
			Do_CalcuCnt();
			break;
		case "mnuCalcuInput":
			Do_CalcuInput();
			break;
		case "mnuLevel_1":
			gridBudget1.Tree.Show(1);
			break;
		case "mnuLevel_2":
			gridBudget1.Tree.Show(2);
			break;
		case "mnuLevel_3":
			gridBudget1.Tree.Show(3);
			break;
		case "mnuLevel_4":
			gridBudget1.Tree.Show(4);
			break;
		case "mnuLevel_5":
			gridBudget1.Tree.Show(5);
			break;
		case "mnuLevel_6":
			gridBudget1.Tree.Show(6);
			break;
		case "mnuLevel_7":
			gridBudget1.Tree.Show(7);
			break;
		case "mnuLevel_8":
			gridBudget1.Tree.Show(8);
			break;
		}
	}

	private void Do_CalcuInput()
	{
		SaveDataToTable();
		SaveToDB();
		LoadData();
	}

	private void Do_CalcuInv()
	{
		SaveToDB_Inv();
		LoadData();
	}

	private void Do_CalcuCnt()
	{
		SaveToDB_Cnt();
		LoadData();
	}

	private void gridBudget1_AfterRowColChange(object sender, RangeEventArgs e)
	{
		if (!(FORM_STATUS == "BINDING"))
		{
			if (gridBudget1[gridBudget1.Row, "kind"].ToString().Trim().ToUpper() == "B")
			{
				gridBudget1.Col = 0;
			}
			else if (gridBudget1[gridBudget1.Row, "kind"].ToString().Trim().ToUpper() == "Z")
			{
				gridBudget1.Col = 0;
			}
		}
	}

	private void SwitchToCorrectLevelStatus(int iLvl)
	{
		if (iLvl <= 0 || iLvl >= 9)
		{
			return;
		}
		((StateButtonTool)ultraToolbarsManager1.Tools["mnuLevel_" + iLvl]).Checked = true;
		for (int i = 1; i < 9; i++)
		{
			if (i <= iLvl)
			{
				((StateButtonTool)ultraToolbarsManager1.Tools["mnuLevel_" + i]).SharedProps.Enabled = true;
			}
			else
			{
				((StateButtonTool)ultraToolbarsManager1.Tools["mnuLevel_" + i]).SharedProps.Enabled = false;
			}
		}
	}
}
