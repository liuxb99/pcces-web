using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.STDClass;
using AxThreed;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetPCalsCustomVar : Form
{
	private const string CallFormHelp = "FormBudgetPCalsCustomVar";

	private UltraToolbarsManager ultraToolbarsManager1;

	private UltraToolbarsDockArea _FormMrsBaseBreakdown_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormMrsBaseBreakdown_Toolbars_Dock_Area_Bottom;

	private UltraToolbarsDockArea _FormMrsBaseBreakdown_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormMrsBaseBreakdown_Toolbars_Dock_Area_Right;

	private Panel panel1;

	private UltraButton ultraButton3;

	private Panel panel2;

	private AxSSPanel axSSPanel2;

	private UltraLabel lblLevelNo;

	private UltraStatusBar ultraStatusBar1;

	private Panel panel3;

	private UltraStatusBar ultraStatusBar2;

	private UltraLabel ultraLabel1;

	private AxSSPanel axSSPanel1;

	private Panel panel4;

	private Splitter splitter1;

	private Panel panel5;

	public GridMrsBase grid1;

	public GridMrsBase grid2;

	private IContainer components;

	private PccesFormAction F_ActionName = PccesFormAction.None;

	private FormStatus FORM_STATUS = FormStatus.Iinitial;

	private ArrayList aArr = new ArrayList();

	private PCals PCALS1;

	private string F_UserID;

	private string F_ProjectCode;

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

	public FormBudgetPCalsCustomVar()
	{
		InitializeComponent();
		HideCols(IsHide: true);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void HideCols(bool IsHide)
	{
		if (IsHide)
		{
			grid1.Cols["ProjectCode"].Visible = false;
			grid1.Cols["VarName"].Visible = false;
		}
	}

	private void Do_MenuAction(string sKey)
	{
		switch (sKey)
		{
		case "BtnNewVar":
			Execute_ItemNew();
			break;
		case "BtnEditVar":
			Execute_ItemEdit();
			break;
		case "BtnDelVar":
			Do_Delete_ItemNew();
			break;
		}
	}

	private void Execute_ItemEdit()
	{
		FormBudgetPCalsCustomEdit FM_PCL_NEW = new FormBudgetPCalsCustomEdit();
		FM_PCL_NEW._ActionName = F_ActionName;
		FM_PCL_NEW._DoWorkType = "EDIT";
		FM_PCL_NEW._UserID = F_UserID;
		FM_PCL_NEW._ProjectCode = F_ProjectCode;
		FM_PCL_NEW._VarName = grid1[grid1.Row, "VarName"].ToString();
		FM_PCL_NEW._VarAlias = grid1[grid1.Row, "VarAlias"].ToString();
		FM_PCL_NEW.ShowDialog();
		FM_PCL_NEW.Close();
		FM_PCL_NEW.Dispose();
		FM_PCL_NEW = null;
		BindToGrid1();
		if (grid1.Rows.Count > 1)
		{
			BindToGrid2(grid1[grid1.Row, "VarName"].ToString().Trim());
		}
		else
		{
			BindToGrid2("");
		}
	}

	private void Execute_ItemNew()
	{
		FormBudgetPCalsCustomEdit FM_PCL_NEW = new FormBudgetPCalsCustomEdit();
		FM_PCL_NEW._ActionName = F_ActionName;
		FM_PCL_NEW._DoWorkType = "NEW";
		FM_PCL_NEW._UserID = F_UserID;
		FM_PCL_NEW._ProjectCode = F_ProjectCode;
		FM_PCL_NEW.ShowDialog();
		FM_PCL_NEW.Close();
		FM_PCL_NEW.Dispose();
		FM_PCL_NEW = null;
		BindToGrid1();
		if (grid1.Rows.Count > 1)
		{
			BindToGrid2(grid1[grid1.Row, "VarName"].ToString().Trim());
		}
		else
		{
			BindToGrid2("");
		}
	}

	private void Do_Delete_ItemNew()
	{
		bool IsDeleteItemB = false;
		if (grid1.Row <= 0)
		{
			MessageBox.Show(this, "請先選定一要刪除的變數項目。", "訊息", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
			return;
		}
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		string sSrcKind = CommonMethods.GetActionNameString(F_ActionName);
		string sSQL = "Select * from " + sSrcKind + "PCalsCustomVar  Where VarName ='" + grid1[grid1.Row, "VarName"].ToString().Trim() + "'  And ProjectCode='" + F_ProjectCode + "' ";
		DataTable DT_Var = DBCLS.GetUserDefine(sSQL);
		if (DT_Var.Rows.Count > 0)
		{
			if (MessageBox.Show(this, "要刪除的變數項目已經被詳細表中的項目引用了，是否要繼續刪除?", "警示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			{
				return;
			}
			IsDeleteItemB = true;
		}
		if (MessageBox.Show(this, "確定要刪除選定的變數項目嗎?", "警示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			PCALS1.ps_projectCode = F_ProjectCode;
			PCALS1.DeleteSettingData(grid1[grid1.Row, "VarName"].ToString());
			if (IsDeleteItemB)
			{
				string sSQLCmd = "Delete " + sSrcKind + "ItemB Where itemCode='" + grid1[grid1.Row, "VarName"].ToString().Trim() + "'  And ProjectCode='" + F_ProjectCode + "' ";
				DBCLS.ExecuteCommand(sSQLCmd);
			}
		}
		BindToGrid1();
		if (grid1.Rows.Count > 1)
		{
			BindToGrid2(grid1[grid1.Row, "VarName"].ToString().Trim());
		}
		else
		{
			BindToGrid2("");
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("UltraToolbar1");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar2 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("HotBar1");
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("BtnNewVar");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("BtnEditVar");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("BtnDelVar");
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("BtnNewVar");
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetPCalsCustomVar));
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("BtnEditVar");
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("BtnDelVar");
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuPop1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("BtnEditVar");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("BtnDelVar");
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel4 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this.grid1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.grid2 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraButton3 = new Infragistics.Win.Misc.UltraButton();
		this.panel2 = new System.Windows.Forms.Panel();
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.lblLevelNo = new Infragistics.Win.Misc.UltraLabel();
		this.axSSPanel2 = new AxThreed.AxSSPanel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.ultraStatusBar2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.axSSPanel1 = new AxThreed.AxSSPanel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.splitter1 = new System.Windows.Forms.Splitter();
		this.panel5 = new System.Windows.Forms.Panel();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.grid1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.grid2).BeginInit();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.axSSPanel2).BeginInit();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.axSSPanel1).BeginInit();
		this.panel4.SuspendLayout();
		this.panel5.SuspendLayout();
		base.SuspendLayout();
		appearance1.ImageVAlign = Infragistics.Win.VAlign.Top;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
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
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager1.ShowQuickCustomizeButton = false;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.FloatingLocation = new System.Drawing.Point(10, 20);
		ultraToolbar1.FloatingSize = new System.Drawing.Size(95, 30);
		ultraToolbar1.Text = "右鍵選單";
		ultraToolbar1.Visible = false;
		ultraToolbar2.DockedColumn = 0;
		ultraToolbar2.DockedRow = 0;
		ultraToolbar2.Settings.CaptionPlacement = Infragistics.Win.TextPlacement.BelowImage;
		appearance6.FontData.Name = "Arial";
		appearance6.ImageVAlign = Infragistics.Win.VAlign.Top;
		appearance6.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraToolbar2.Settings.ToolAppearance = appearance6;
		ultraToolbar2.Settings.ToolDisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		ultraToolbar2.Text = "HotBar1";
		ultraToolbar2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool1, buttonTool2, buttonTool3 });
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[2] { ultraToolbar1, ultraToolbar2 });
		appearance7.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance7.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance7;
		this.ultraToolbarsManager1.ToolbarSettings.FillEntireRow = Infragistics.Win.DefaultableBoolean.True;
		appearance8.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance8.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance8.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance8;
		this.ultraToolbarsManager1.ToolbarSettings.ToolDisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		appearance9.Image = resources.GetObject("appearance9.Image");
		buttonTool4.SharedProps.AppearancesSmall.Appearance = appearance9;
		buttonTool4.SharedProps.Caption = "新增變數";
		appearance10.Image = resources.GetObject("appearance10.Image");
		buttonTool5.SharedProps.AppearancesSmall.Appearance = appearance10;
		buttonTool5.SharedProps.Caption = "編輯變數";
		appearance11.Image = resources.GetObject("appearance11.Image");
		buttonTool6.SharedProps.AppearancesSmall.Appearance = appearance11;
		buttonTool6.SharedProps.Caption = "刪除變數";
		popupMenuTool1.SharedProps.Caption = "右鍵選單";
		popupMenuTool1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool7, buttonTool8 });
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { buttonTool4, buttonTool5, buttonTool6, popupMenuTool1 });
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this.grid1._ExcelFileName = "";
		this.grid1._ExcelSheeName = "";
		this.grid1._IsOpenExcelAfterExport = false;
		this.grid1.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
		this.grid1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
		this.grid1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.grid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.grid1.ColumnInfo = "6,1,0,0,0,110,Columns:0{Width:17;Name:\"RowIndicator\";AllowDragging:False;AllowResizing:False;AllowEditing:False;DataType:System.Int32;TextAlign:RightCenter;}\t1{Name:\"ProjectCode\";Caption:\"專案代號\";DataType:System.String;TextAlign:LeftCenter;}\t2{Name:\"VarAlias\";Caption:\"變數名稱\";DataType:System.String;TextAlign:LeftCenter;}\t3{Name:\"VarName\";Caption:\"變數實名\";DataType:System.String;TextAlign:LeftCenter;}\t4{Name:\"VarRate\";Caption:\"百分比\";DataType:System.Decimal;}\t5{Name:\"VarAmount\";Caption:\"金額\";DataType:System.Decimal;Format:\"###,###,###,###,###.00\";}\t";
		this.ultraToolbarsManager1.SetContextMenuUltra(this.grid1, "mnuPop1");
		this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.grid1.ExtendLastCol = true;
		this.grid1.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.grid1.ForeColor = System.Drawing.SystemColors.WindowText;
		this.grid1.IsProcessUndo = false;
		this.grid1.Location = new System.Drawing.Point(0, 30);
		this.grid1.Name = "grid1";
		this.grid1.Rows.Count = 1;
		this.grid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.grid1.ShowCursor = true;
		this.grid1.ShowToolTipOnNarrowColumn = true;
		this.grid1.Size = new System.Drawing.Size(674, 110);
		this.grid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11pt;BackColor:237, 243, 254;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;ForeColor:ControlText;Border:Flat,1,ControlDark,Both;}\tHighlight{BackColor:102, 153, 255;ForeColor:Black;}\tFocus{Font:細明體, 10pt, style=Bold;BackColor:102, 153, 255;ForeColor:Black;Border:Double,1,96, 145, 234,Both;}\tSearch{BackColor:255, 255, 128;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,Transparent,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.grid1.TabIndex = 8;
		this.grid1.UndoMax = 5;
		this.grid1.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(grid1_AfterSelChange);
		this.grid2._ExcelFileName = "";
		this.grid2._ExcelSheeName = "";
		this.grid2._IsOpenExcelAfterExport = false;
		this.grid2.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
		this.grid2.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
		this.grid2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.grid2.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.grid2.ColumnInfo = "6,1,0,0,0,110,Columns:0{Width:17;Name:\"RowIndicator\";AllowDragging:False;AllowResizing:False;AllowEditing:False;DataType:System.Int32;TextAlign:RightCenter;}\t1{Width:60;Name:\"VarSign\";Caption:\"正負號\";DataType:System.String;TextAlign:CenterCenter;}\t2{Width:71;Name:\"ItemNo\";Caption:\"項次\";DataType:System.String;TextAlign:LeftCenter;}\t3{Width:245;Name:\"CName\";Caption:\"項目及說明\";DataType:System.String;TextAlign:LeftCenter;}\t4{Width:80;Name:\"UnitName\";Caption:\"單位\";DataType:System.String;TextAlign:LeftCenter;}\t5{Name:\"Amount\";Caption:\"金額\";DataType:System.Decimal;Format:\"###,###,###,###,###.00\";}\t";
		this.ultraToolbarsManager1.SetContextMenuUltra(this.grid2, "PopupMenu1");
		this.grid2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.grid2.Enabled = false;
		this.grid2.ExtendLastCol = true;
		this.grid2.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.grid2.ForeColor = System.Drawing.SystemColors.WindowText;
		this.grid2.IsProcessUndo = false;
		this.grid2.Location = new System.Drawing.Point(0, 30);
		this.grid2.Name = "grid2";
		this.grid2.Rows.Count = 1;
		this.grid2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.grid2.ShowCursor = true;
		this.grid2.ShowToolTipOnNarrowColumn = true;
		this.grid2.Size = new System.Drawing.Size(674, 242);
		this.grid2.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11pt;BackColor:237, 243, 254;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;ForeColor:ControlText;Border:Flat,1,ControlDark,Both;}\tHighlight{BackColor:102, 153, 255;ForeColor:Black;}\tFocus{Font:細明體, 10pt, style=Bold;BackColor:White;ForeColor:Black;Border:Double,1,96, 145, 234,Both;}\tSearch{BackColor:255, 255, 128;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,Transparent,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.grid2.TabIndex = 8;
		this.grid2.UndoMax = 5;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Top.Name = "_FormMrsBaseBreakdown_Toolbars_Dock_Area_Top";
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(692, 44);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 573);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Bottom.Name = "_FormMrsBaseBreakdown_Toolbars_Dock_Area_Bottom";
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(692, 0);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 44);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Left.Name = "_FormMrsBaseBreakdown_Toolbars_Dock_Area_Left";
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 529);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(692, 44);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Right.Name = "_FormMrsBaseBreakdown_Toolbars_Dock_Area_Right";
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 529);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.ultraButton3);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 541);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(692, 32);
		this.panel1.TabIndex = 8;
		this.ultraButton3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance12.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance12.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance12.Image = resources.GetObject("appearance12.Image");
		appearance12.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton3.Appearance = appearance12;
		this.ultraButton3.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton3.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ultraButton3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton3.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton3.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton3.Location = new System.Drawing.Point(599, 2);
		this.ultraButton3.Name = "ultraButton3";
		this.ultraButton3.ShowFocusRect = false;
		this.ultraButton3.ShowOutline = false;
		this.ultraButton3.Size = new System.Drawing.Size(90, 28);
		this.ultraButton3.SupportThemes = false;
		this.ultraButton3.TabIndex = 6;
		this.ultraButton3.Text = "結  束";
		this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel2.Controls.Add(this.grid1);
		this.panel2.Controls.Add(this.ultraStatusBar1);
		this.panel2.Controls.Add(this.lblLevelNo);
		this.panel2.Controls.Add(this.axSSPanel2);
		this.panel2.Location = new System.Drawing.Point(8, 8);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(676, 168);
		this.panel2.TabIndex = 9;
		appearance13.BackColor = System.Drawing.Color.LightGray;
		appearance13.FontData.SizeInPoints = 11f;
		appearance13.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraStatusBar1.Appearance = appearance13;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 140);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		appearance14.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraStatusPanel1.Appearance = appearance14;
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Key = "RowsCount";
		ultraStatusPanel1.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		ultraStatusPanel1.Text = "資料筆數：";
		ultraStatusPanel1.Width = 200;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[1] { ultraStatusPanel1 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(674, 26);
		this.ultraStatusBar1.SupportThemes = false;
		this.ultraStatusBar1.TabIndex = 9;
		this.ultraStatusBar1.Text = "ultraStatusBar1";
		this.lblLevelNo.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.lblLevelNo.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblLevelNo.Location = new System.Drawing.Point(8, 8);
		this.lblLevelNo.Name = "lblLevelNo";
		this.lblLevelNo.Size = new System.Drawing.Size(328, 16);
		this.lblLevelNo.TabIndex = 6;
		this.lblLevelNo.Text = "自訂變數列表";
		this.axSSPanel2.ContainingControl = this;
		this.axSSPanel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.axSSPanel2.Location = new System.Drawing.Point(0, 0);
		this.axSSPanel2.Name = "axSSPanel2";
		this.axSSPanel2.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("axSSPanel2.OcxState");
		this.axSSPanel2.Size = new System.Drawing.Size(674, 30);
		this.axSSPanel2.TabIndex = 2;
		this.panel3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel3.Controls.Add(this.grid2);
		this.panel3.Controls.Add(this.ultraStatusBar2);
		this.panel3.Controls.Add(this.ultraLabel1);
		this.panel3.Controls.Add(this.axSSPanel1);
		this.panel3.Location = new System.Drawing.Point(8, 4);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(676, 300);
		this.panel3.TabIndex = 10;
		appearance15.BackColor = System.Drawing.Color.LightGray;
		appearance15.FontData.SizeInPoints = 11f;
		appearance15.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraStatusBar2.Appearance = appearance15;
		this.ultraStatusBar2.Location = new System.Drawing.Point(0, 272);
		this.ultraStatusBar2.Name = "ultraStatusBar2";
		appearance16.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraStatusPanel2.Appearance = appearance16;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.Key = "RowsCount";
		ultraStatusPanel2.Text = "資料筆數：";
		ultraStatusPanel2.Width = 200;
		ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel3.Key = "ProgressBar";
		appearance17.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
		appearance17.BackColor2 = System.Drawing.Color.Navy;
		appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		ultraStatusPanel3.ProgressBarInfo.Appearance = appearance17;
		ultraStatusPanel3.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		appearance18.TextHAlign = Infragistics.Win.HAlign.Right;
		ultraStatusPanel4.Appearance = appearance18;
		ultraStatusPanel4.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel4.Text = "客服電話:(02)2716-5561";
		ultraStatusPanel4.Width = 200;
		this.ultraStatusBar2.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[3] { ultraStatusPanel2, ultraStatusPanel3, ultraStatusPanel4 });
		this.ultraStatusBar2.Size = new System.Drawing.Size(674, 26);
		this.ultraStatusBar2.SupportThemes = false;
		this.ultraStatusBar2.TabIndex = 9;
		this.ultraStatusBar2.Text = "ultraStatusBar1";
		this.ultraLabel1.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(8, 8);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(328, 16);
		this.ultraLabel1.TabIndex = 6;
		this.ultraLabel1.Text = "運算項目列表";
		this.axSSPanel1.ContainingControl = this;
		this.axSSPanel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.axSSPanel1.Location = new System.Drawing.Point(0, 0);
		this.axSSPanel1.Name = "axSSPanel1";
		this.axSSPanel1.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("axSSPanel1.OcxState");
		this.axSSPanel1.Size = new System.Drawing.Size(674, 30);
		this.axSSPanel1.TabIndex = 2;
		this.panel4.Controls.Add(this.panel2);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel4.Location = new System.Drawing.Point(0, 44);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(692, 180);
		this.panel4.TabIndex = 15;
		this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
		this.splitter1.Location = new System.Drawing.Point(0, 224);
		this.splitter1.Name = "splitter1";
		this.splitter1.Size = new System.Drawing.Size(692, 10);
		this.splitter1.TabIndex = 16;
		this.splitter1.TabStop = false;
		this.panel5.Controls.Add(this.panel3);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel5.Location = new System.Drawing.Point(0, 234);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(692, 307);
		this.panel5.TabIndex = 17;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.ultraButton3;
		base.ClientSize = new System.Drawing.Size(692, 573);
		base.Controls.Add(this.panel5);
		base.Controls.Add(this.splitter1);
		base.Controls.Add(this.panel4);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Bottom);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormBudgetPCalsCustomVar";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "自訂變數項維護";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormBudgetPCalsCustomVar_KeyDown);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormBudgetPCalsCustomVar_FormClosing);
		base.Load += new System.EventHandler(FormBudgetPCalsCustomVar_Load);
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.grid1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.grid2).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.axSSPanel2).EndInit();
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.axSSPanel1).EndInit();
		this.panel4.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void FormBudgetPCalsCustomVar_Load(object sender, EventArgs e)
	{
		aArr.Add(F_UserID);
		aArr.Add("PCals--自定變數項目取得");
		PCALS1 = new PCals(aArr);
		BindToGrid1();
		LoadingScreen();
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("CalsCustomVar", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("CalsCustomVar", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("CalsCustomVar", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("CalsCustomVar", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("CalsCustomVar", "PK_Height"));
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

	private void BindToGrid1()
	{
		FORM_STATUS = FormStatus.Binding;
		PCALS1.ps_projectCode = F_ProjectCode;
		DataTable DT_List = PCALS1.GetCustomVarList();
		double dAmount = 0.0;
		grid1.Rows.Count = DT_List.Rows.Count + 1;
		for (int i = 0; i < DT_List.Rows.Count; i++)
		{
			dAmount = 0.0;
			grid1[i + 1, "ProjectCode"] = DT_List.Rows[i]["ProjectCode"].ToString();
			grid1[i + 1, "VarAlias"] = DT_List.Rows[i]["VarAlias"].ToString();
			grid1[i + 1, "VarName"] = DT_List.Rows[i]["VarName"].ToString();
			grid1[i + 1, "VarRate"] = PubTools.Str2Double(DT_List.Rows[i]["VarRate"]) * 100.0;
			DataTable DT_OperList = PCALS1.GetCustomOperationList(DT_List.Rows[i]["VarName"].ToString());
			DataTable DT_ItemC = PCALS1.GetCustomItemC(DT_List.Rows[i]["VarAlias"].ToString());
			for (int j = 0; j < DT_OperList.Rows.Count; j++)
			{
				string sVarSign = DT_OperList.Rows[j]["VarSign"].ToString().Trim();
				dAmount += (double)PubTools.Str2Int(sVarSign) * PubTools.Str2Double(DT_OperList.Rows[j]["Amount"]);
			}
			if (DT_ItemC.Rows.Count > 0 && dAmount > 0.0)
			{
				decimal Amount = 0m;
				DataView dvItemC = DT_ItemC.DefaultView;
				double ld_itemcost = dAmount;
				decimal ld_amount = 0m;
				double PreUpValue = 0.0;
				for (int k = 0; k < dvItemC.Count; k++)
				{
					DataRow theItemCRow = dvItemC[k].Row;
					double ld_down = 0.0;
					if (theItemCRow["down"] != DBNull.Value && theItemCRow["down"].ToString() != "")
					{
						ld_down = double.Parse(theItemCRow["down"].ToString());
					}
					double ld_up = 0.0;
					if (theItemCRow["up"] != DBNull.Value && theItemCRow["up"].ToString() != "")
					{
						ld_up = double.Parse(theItemCRow["up"].ToString());
					}
					if (i == 0)
					{
						PreUpValue = ld_down;
					}
					if (PreUpValue != ld_down)
					{
						break;
					}
					PreUpValue = ld_up;
					double ld_rate = 0.0;
					if (theItemCRow["rate"] != DBNull.Value && theItemCRow["rate"].ToString() != "")
					{
						ld_rate = double.Parse(theItemCRow["rate"].ToString());
					}
					if (!(ld_itemcost > ld_down))
					{
						break;
					}
					decimal RangeAmount = ((!(ld_itemcost > ld_up)) ? ((decimal)((ld_itemcost - ld_down) * ld_rate / 100.0)) : ((decimal)((ld_up - ld_down) * ld_rate / 100.0)));
					if (theItemCRow.Table.Columns.IndexOf("formula") >= 0 && theItemCRow["formula"] != DBNull.Value && theItemCRow["formula"].ToString() != "")
					{
						double dlAmount = PubTools.CalcFormula3("[Value]", theItemCRow["formula"].ToString(), (double)RangeAmount, enableDefaultValue: true);
						if (double.IsNaN(dlAmount))
						{
							dlAmount = 0.0;
						}
						else
						{
							RangeAmount = (decimal)dlAmount;
						}
					}
					ld_amount += RangeAmount;
				}
				dAmount = (double)ld_amount;
			}
			grid1[i + 1, "VarAmount"] = dAmount * PubTools.Str2Double(DT_List.Rows[i]["VarRate"]);
		}
		if (grid1.Rows.Count > 1)
		{
			BindToGrid2(grid1[1, "VarName"].ToString().Trim());
		}
		ultraStatusBar1.Panels[0].Text = "資料筆數:" + DT_List.Rows.Count;
		FORM_STATUS = FormStatus.Normal;
	}

	private void BindToGrid2(string sVarName)
	{
		FORM_STATUS = FormStatus.Binding;
		PCALS1.ps_projectCode = F_ProjectCode;
		DataTable DT_OperList = PCALS1.GetCustomOperationList(sVarName);
		grid2.Rows.Count = DT_OperList.Rows.Count + 1;
		for (int i = 0; i < DT_OperList.Rows.Count; i++)
		{
			string sVarSign = DT_OperList.Rows[i]["VarSign"].ToString().Trim();
			grid2[i + 1, "VarSign"] = ((sVarSign == "1") ? "＋" : "－");
			grid2[i + 1, "ItemNo"] = DT_OperList.Rows[i]["ItemNo"].ToString();
			grid2[i + 1, "CName"] = DT_OperList.Rows[i]["CName"].ToString();
			grid2[i + 1, "UnitName"] = DT_OperList.Rows[i]["UnitName"].ToString();
			grid2[i + 1, "Amount"] = DT_OperList.Rows[i]["Amount"];
		}
		ultraStatusBar2.Panels[0].Text = "資料筆數:" + DT_OperList.Rows.Count;
		FORM_STATUS = FormStatus.Normal;
	}

	private void grid1_AfterSelChange(object sender, RangeEventArgs e)
	{
		if (FORM_STATUS != FormStatus.Binding)
		{
			if (grid1.Rows.Count > 1)
			{
				BindToGrid2(grid1[grid1.Row, "VarName"].ToString().Trim());
			}
			else
			{
				BindToGrid2("");
			}
		}
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		Do_MenuAction(e.Tool.Key);
	}

	private void FormBudgetPCalsCustomVar_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("CalsCustomVar", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("CalsCustomVar", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("CalsCustomVar", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("CalsCustomVar", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("CalsCustomVar", "WindowState", base.WindowState.ToString());
	}

	private void FormBudgetPCalsCustomVar_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormBudgetPCalsCustomVar");
		}
	}
}
