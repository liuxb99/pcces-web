using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.MrsBase;

public class FormAutosurName : Form
{
	private const string CallFormHelp = "FormAutosurName";

	private UltraToolbarsManager ultraToolbarsManager1;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Bottom;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Right;

	private Panel panel3;

	public GridMrsBase GridUnit1;

	private Panel panel1;

	private UltraButton ultraButton1;

	private UltraButton ultraButton2;

	private IContainer components;

	private DataTable DT = new DataTable();

	private DataTable DT1 = new DataTable();

	private DataView DV1 = new DataView();

	private string F_UserID;

	private string F_AutoEdit;

	private string F_TreeKey;

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

	public string _AutoEdit
	{
		get
		{
			return F_AutoEdit;
		}
		set
		{
			F_AutoEdit = value;
		}
	}

	public string _TreeKey
	{
		get
		{
			return F_TreeKey;
		}
		set
		{
			F_TreeKey = value;
		}
	}

	public FormAutosurName()
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
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.OptionSet optionSet1 = new Infragistics.Win.UltraWinToolbars.OptionSet("Switch");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool1");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool1 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("階層");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool1 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("munLevel1", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool2 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("munLevel2", "Switch");
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.MrsBase.FormAutosurName));
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool3 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool2 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool4 = new Infragistics.Win.UltraWinToolbars.LabelTool("階層");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool3 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("munLevel1", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool4 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("munLevel2", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool5 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("munLevel3", "");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool6 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("munLevel4", "");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool7 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("munLevel5", "");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool8 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("munLevel6", "");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool9 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("munLevel7", "");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool10 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("munLevel8", "");
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this.GridUnit1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this._FormSys_B_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.panel3 = new System.Windows.Forms.Panel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).BeginInit();
		this.panel3.SuspendLayout();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		appearance1.FontData.Name = "Arial";
		appearance1.FontData.SizeInPoints = 9f;
		this.ultraToolbarsManager1.Appearance = appearance1;
		appearance2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance2.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.DockAreaAppearance = appearance2;
		this.ultraToolbarsManager1.DockWithinContainer = this;
		this.ultraToolbarsManager1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraToolbarsManager1.LockToolbars = true;
		appearance3.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance3.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance3.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.MenuSettings.HotTrackAppearance = appearance3;
		appearance4.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance4.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsManager1.MenuSettings.IconAreaAppearance = appearance4;
		appearance5.BackColor = System.Drawing.Color.White;
		appearance5.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.MenuSettings.ToolAppearance = appearance5;
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
		labelTool2.InstanceProps.IsFirstInGroup = true;
		stateButtonTool2.Checked = true;
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[6] { labelTool1, comboBoxTool1, buttonTool1, labelTool2, stateButtonTool1, stateButtonTool2 });
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[1] { ultraToolbar1 });
		appearance6.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance6.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance6;
		appearance7.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance7.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance7.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance7;
		appearance8.Image = resources.GetObject("appearance8.Image");
		buttonTool2.SharedProps.AppearancesSmall.Appearance = appearance8;
		buttonTool2.SharedProps.Caption = "刪除";
		buttonTool2.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool2.SharedProps.Shortcut = System.Windows.Forms.Shortcut.Del;
		labelTool3.SharedProps.Caption = "關鍵字:";
		labelTool3.SharedProps.CustomizerCaption = "輸入想查詢規則表內的關鍵字";
		labelTool3.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		labelTool3.SharedProps.ToolTipText = "輸入想查詢規則表內的關鍵字";
		comboBoxTool2.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool2.SharedProps.Caption = "輸入關鍵字";
		comboBoxTool2.SharedProps.CustomizerCaption = "輸入想查詢規則表內的關鍵字";
		comboBoxTool2.SharedProps.ToolTipText = "輸入想查詢規則表內的關鍵字";
		comboBoxTool2.SharedProps.Width = 200;
		comboBoxTool2.ValueList = valueList1;
		appearance9.Image = resources.GetObject("appearance9.Image");
		buttonTool3.SharedProps.AppearancesSmall.Appearance = appearance9;
		buttonTool3.SharedProps.Caption = "Go";
		labelTool4.SharedProps.Caption = "階層";
		stateButtonTool3.OptionSetKey = "Switch";
		stateButtonTool3.SharedProps.Caption = "1";
		stateButtonTool3.SharedProps.Category = "LevelSwitch";
		stateButtonTool3.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool4.Checked = true;
		stateButtonTool4.OptionSetKey = "Switch";
		stateButtonTool4.SharedProps.Caption = "2";
		stateButtonTool4.SharedProps.Category = "LevelSwitch";
		stateButtonTool4.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool5.SharedProps.Caption = "3";
		stateButtonTool5.SharedProps.Category = "LevelSwitch";
		stateButtonTool5.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool6.SharedProps.Caption = "4";
		stateButtonTool6.SharedProps.Category = "LevelSwitch";
		stateButtonTool6.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool7.SharedProps.Caption = "5";
		stateButtonTool7.SharedProps.Category = "LevelSwitch";
		stateButtonTool7.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool8.SharedProps.Caption = "6";
		stateButtonTool8.SharedProps.Category = "LevelSwitch";
		stateButtonTool8.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool9.SharedProps.Caption = "7";
		stateButtonTool9.SharedProps.Category = "LevelSwitch";
		stateButtonTool9.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool10.SharedProps.Caption = "8";
		stateButtonTool10.SharedProps.Category = "LevelSwitch";
		stateButtonTool10.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[13]
		{
			buttonTool2, labelTool3, comboBoxTool2, buttonTool3, labelTool4, stateButtonTool3, stateButtonTool4, stateButtonTool5, stateButtonTool6, stateButtonTool7,
			stateButtonTool8, stateButtonTool9, stateButtonTool10
		});
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this.GridUnit1._ExcelFileName = "";
		this.GridUnit1._ExcelSheeName = "";
		this.GridUnit1._IsOpenExcelAfterExport = false;
		this.GridUnit1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.GridUnit1.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
		this.GridUnit1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.GridUnit1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridUnit1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.GridUnit1.ColumnInfo = "4,1,0,0,0,110,Columns:0{Width:17;Name:\"RowIndicator\";AllowDragging:False;AllowResizing:False;AllowEditing:False;DataType:System.Int32;TextAlign:RightTop;TextAlignFixed:GeneralTop;}\t1{Width:100;Name:\"itemCode\";Caption:\"綱要編碼\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t2{Width:140;Name:\"cName\";Caption:\"章名\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t3{Width:200;Name:\"surName\";Caption:\"別名\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t";
		this.ultraToolbarsManager1.SetContextMenuUltra(this.GridUnit1, "Popup1");
		this.GridUnit1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridUnit1.ExtendLastCol = true;
		this.GridUnit1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridUnit1.ForeColor = System.Drawing.Color.Black;
		this.GridUnit1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.GridUnit1.IsProcessUndo = false;
		this.GridUnit1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.GridUnit1.Location = new System.Drawing.Point(0, 0);
		this.GridUnit1.Name = "GridUnit1";
		this.GridUnit1.Rows.Count = 1;
		this.GridUnit1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.GridUnit1.ShowCursor = true;
		this.GridUnit1.ShowToolTipOnNarrowColumn = true;
		this.GridUnit1.Size = new System.Drawing.Size(656, 391);
		this.GridUnit1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11.25pt;BackColor:237, 243, 254;ForeColor:Black;TextAlign:GeneralBottom;Border:Flat,1,Silver,Both;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;TextAlign:GeneralTop;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;TextAlign:LeftCenter;ImageAlign:CenterCenter;Border:None,1,Black,Both;Format:\"###,###,###,##0\";}\tFocus{Font:細明體, 10.5pt, style=Bold;BackColor:White;Margins:0, 0, 0, 0;Border:Double,1,Black,Both;}\tSearch{BackColor:Highlight;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.GridUnit1.TabIndex = 8;
		this.GridUnit1.Tree.Column = 1;
		this.GridUnit1.UndoMax = 10;
		this.GridUnit1.Click += new System.EventHandler(GridUnit1_Click);
		this.GridUnit1.DoubleClick += new System.EventHandler(GridUnit1_DoubleClick);
		this._FormSys_B_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormSys_B_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormSys_B_Toolbars_Dock_Area_Top.Name = "_FormSys_B_Toolbars_Dock_Area_Top";
		this._FormSys_B_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(656, 27);
		this._FormSys_B_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 454);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Name = "_FormSys_B_Toolbars_Dock_Area_Bottom";
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(656, 0);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormSys_B_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 27);
		this._FormSys_B_Toolbars_Dock_Area_Left.Name = "_FormSys_B_Toolbars_Dock_Area_Left";
		this._FormSys_B_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 427);
		this._FormSys_B_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormSys_B_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(656, 27);
		this._FormSys_B_Toolbars_Dock_Area_Right.Name = "_FormSys_B_Toolbars_Dock_Area_Right";
		this._FormSys_B_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 427);
		this._FormSys_B_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this.panel3.Controls.Add(this.GridUnit1);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 27);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(656, 391);
		this.panel3.TabIndex = 28;
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.ultraButton1);
		this.panel1.Controls.Add(this.ultraButton2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 418);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(656, 36);
		this.panel1.TabIndex = 22;
		this.ultraButton1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance10.Image = resources.GetObject("appearance10.Image");
		appearance10.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton1.Appearance = appearance10;
		this.ultraButton1.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.ultraButton1.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton1.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton1.Location = new System.Drawing.Point(464, 3);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(88, 31);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 8;
		this.ultraButton1.Text = "確定";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		this.ultraButton2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance11.Image = resources.GetObject("appearance11.Image");
		appearance11.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton2.Appearance = appearance11;
		this.ultraButton2.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton2.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton2.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton2.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton2.Location = new System.Drawing.Point(560, 3);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(88, 31);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 7;
		this.ultraButton2.Text = "取消";
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(656, 454);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Bottom);
		base.KeyPreview = true;
		base.Name = "FormAutosurName";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "別名設定";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormAutosurName_KeyDown);
		base.Load += new System.EventHandler(FormAutosurName_Load);
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).EndInit();
		this.panel3.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void FormAutosurName_Load(object sender, EventArgs e)
	{
		LoadData();
		BindToGrid();
	}

	private void LoadData()
	{
		if (F_AutoEdit != "" && F_AutoEdit != null)
		{
			Text = "";
		}
		string ls_selectstr = "SELECT A.parent as Parent ,A.*, B.surName FROM AutoNumA A LEFT join AutoNumY B ON A.itemCode = B.ItemCode ";
		DBClass DB_CLASS = new DBClass();
		DT = DB_CLASS.GetUserDefine(ls_selectstr);
		ls_selectstr = "Select itemCode,surName from AutoNumY";
		DT1 = DB_CLASS.GetUserDefine(ls_selectstr);
		DB_CLASS = null;
		if (F_AutoEdit == "AutoNum")
		{
			GridUnit1.Cols["surName"].AllowEditing = false;
		}
	}

	private void BindToGrid()
	{
		bool DelFlag = false;
		string ls_selectstr = "SELECT A.*, B.surName FROM AutoNumA A LEFT join AutoNumY B ON A.itemCode = B.ItemCode where A.parent = 'E' or A.parent = 'L'";
		DBClass DB_CLASS = new DBClass();
		DataTable DTCN = DB_CLASS.GetUserDefine(ls_selectstr);
		DB_CLASS = null;
		for (int i = 0; i < DT.Rows.Count; i++)
		{
			if (DT.Rows[i]["parent"].ToString().Trim() == "E" || DT.Rows[i]["parent"].ToString().Trim() == "L")
			{
				DT.Rows[i].Delete();
				DelFlag = true;
			}
			else
			{
				DelFlag = false;
			}
			if (!DelFlag && (DT.Rows[i]["itemCode"].ToString().Trim() == "L" || DT.Rows[i]["itemCode"].ToString().Trim() == "E" || DT.Rows[i]["itemCode"].ToString().Trim() == "M" || DT.Rows[i]["itemCode"].ToString().Trim() == "W"))
			{
				DT.Rows[i].Delete();
			}
		}
		DV1 = DT.DefaultView;
		GridUnit1.Rows.Count = DV1.Count + 1;
		GridUnit1.Redraw = false;
		for (int i = 0; i < DV1.Count; i++)
		{
			GridUnit1.Rows[i + 1].IsNode = true;
			GridUnit1[i + 1, "itemCode"] = DV1[i]["itemCode"].ToString().Trim();
			GridUnit1[i + 1, "cName"] = DV1[i]["cName"].ToString().Trim();
			GridUnit1[i + 1, "surName"] = DV1[i]["surName"].ToString().Trim();
			GridUnit1.Rows[i + 1].Node.Level = PubTools.Str2Int(DV1[i]["WinFormFlag"].ToString().Trim());
		}
		GridUnit1.Redraw = true;
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		if (F_AutoEdit == "" || F_AutoEdit == null)
		{
			string ls_selectstr = "";
			DBClass DB_CLASS = new DBClass();
			DT1.CaseSensitive = true;
			for (int i = 0; i < GridUnit1.Rows.Count - 1; i++)
			{
				DataView dv = new DataView(DT1);
				dv.RowFilter = "itemCode = '" + GridUnit1[i + 1, "itemCode"].ToString().Trim() + "'";
				if (dv.Count > 0)
				{
					if (dv[0]["surName"].ToString() != GridUnit1[i + 1, "surName"].ToString().Trim())
					{
						ls_selectstr = "Update AutoNumY set surName = '" + GridUnit1[i + 1, "surName"].ToString().Trim() + "' where itemCode = '" + GridUnit1[i + 1, "itemCode"].ToString().Trim() + "'";
						DB_CLASS.ExecuteCommand(ls_selectstr);
					}
				}
				else
				{
					ls_selectstr = "Insert into AutoNumY (itemCode,surName) values ('" + GridUnit1[i + 1, "itemCode"].ToString().Trim() + "','" + GridUnit1[i + 1, "surName"].ToString().Trim() + "')";
					DB_CLASS.ExecuteCommand(ls_selectstr);
				}
			}
			DB_CLASS = null;
		}
		else
		{
			DBClass DBClass1 = new DBClass();
			string F_surName = DBClass1.GetSurName(F_TreeKey);
			DBClass1 = null;
			(base.Owner as FormAutoNum).F_CustomCode = GridUnit1[GridUnit1.Row, "itemCode"].ToString();
			(base.Owner as FormAutoNum).F_CustomCodeName = GridUnit1[GridUnit1.Row, "cName"].ToString();
			(base.Owner as FormAutoNum)._surName = F_surName;
		}
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		DoMenuAction(e.Tool.Key);
	}

	private void DoMenuAction(string MenuID)
	{
		switch (MenuID)
		{
		case "mnu_Go":
			Do_ToolBarFind();
			break;
		case "munLevel1":
			GridUnit1.Redraw = false;
			GridUnit1.Tree.Show(1);
			GridUnit1.Redraw = true;
			break;
		case "munLevel2":
			GridUnit1.Redraw = false;
			GridUnit1.Tree.Show(2);
			GridUnit1.Redraw = true;
			break;
		case "munLevel3":
			GridUnit1.Tree.Show(3);
			break;
		case "munLevel4":
			GridUnit1.Tree.Show(4);
			break;
		case "munLevel5":
			GridUnit1.Tree.Show(5);
			break;
		case "munLevel6":
			GridUnit1.Tree.Show(6);
			break;
		case "munLevel7":
			GridUnit1.Tree.Show(7);
			break;
		case "munLevel8":
			GridUnit1.Tree.Show(8);
			break;
		}
	}

	private void Do_ToolBarFind()
	{
		if (GridUnit1.Rows.Count > 1)
		{
			string sSearchText = ((ComboBoxTool)ultraToolbarsManager1.Tools["mnu_Cbo1"]).Text.Trim();
			Do_Find2(sSearchText, "", "");
		}
	}

	public void Do_Find2(string sText, string ssFiledName, string sFindKind)
	{
		bool IsSearchName = false;
		string sField = "itemCode";
		for (int ii = 0; ii < sText.Length; ii++)
		{
			if (sText[ii] > '\u007f')
			{
				IsSearchName = true;
				break;
			}
		}
		sField = ((!IsSearchName) ? "" : "cName");
		string[] sFields = new string[3] { "itemCode", "cName", "surName" };
		if (ssFiledName.Trim() != "")
		{
			sField = ssFiledName.Trim();
		}
		if (sField.ToUpper() == "ITEMCODE" && sFindKind == "")
		{
			sFindKind = "PREFIX";
		}
		int iStart = GridUnit1.Row + 1;
		if (iStart == 0)
		{
			iStart = 1;
		}
		int iFind = -1;
		if (GridUnit1.Rows.Count == 1)
		{
			return;
		}
		string flgBreak = "";
		for (int i = iStart - 1; i < GridUnit1.Rows.Count - 1; i++)
		{
			flgBreak = "";
			if (sFindKind.ToUpper() == "PREFIX")
			{
				if (DV1[i][sField] != null && DV1[i][sField].ToString() != "" && DV1[i][sField].ToString().Length >= sText.Length && DV1[i][sField].ToString().Substring(0, sText.Length) == sText)
				{
					iFind = i;
					break;
				}
			}
			else
			{
				for (int j = 0; j < sFields.Length; j++)
				{
					if (DV1[i][sFields[j]] != null && DV1[i][sFields[j]].ToString() != "" && DV1[i][sFields[j]].ToString().Length >= sText.Length && DV1[i][sFields[j]].ToString().IndexOf(sText) > -1)
					{
						iFind = i;
						flgBreak = "break";
						break;
					}
				}
			}
			if (flgBreak != "")
			{
				break;
			}
		}
		if (iFind > -1)
		{
			GridUnit1.Row = iFind + 1;
		}
	}

	private void FormAutosurName_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormAutosurName");
		}
	}

	private void GridUnit1_DoubleClick(object sender, EventArgs e)
	{
		if (!(F_AutoEdit == "") && F_AutoEdit != null && GridUnit1.Row >= 0 && GridUnit1[GridUnit1.Row, "itemCode"].ToString().Trim().Length >= 5)
		{
			(base.Owner as FormAutoNum).F_CustomCode = GridUnit1[GridUnit1.Row, "itemCode"].ToString();
			(base.Owner as FormAutoNum).F_CustomCodeName = GridUnit1[GridUnit1.Row, "cName"].ToString();
			base.DialogResult = DialogResult.OK;
		}
	}

	private void GridUnit1_Click(object sender, EventArgs e)
	{
		if (!(F_AutoEdit == "") && F_AutoEdit != null && GridUnit1.Row >= 0 && GridUnit1[GridUnit1.Row, "itemCode"].ToString().Trim().Length >= 5)
		{
			(base.Owner as FormAutoNum).F_CustomCode = GridUnit1[GridUnit1.Row, "itemCode"].ToString();
			(base.Owner as FormAutoNum).F_CustomCodeName = GridUnit1[GridUnit1.Row, "cName"].ToString();
			base.DialogResult = DialogResult.OK;
		}
	}
}
