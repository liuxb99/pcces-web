using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.PccesMain.ArchControls;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.SysMaintain;

public class FormSys_D_Pick : Form
{
	private const string CallFormHelp = "FormSys_D_Pick";

	private Panel panel2;

	private UltraButton Btn_Cncl;

	private UltraButton Btn_OK;

	private Panel panel3;

	public GridMrsBase GridUnit1;

	private IContainer components;

	private UltraToolbarsManager ultraToolbarsManager1;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Bottom;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Right;

	private string F_UserID;

	private string F_KeyWord = "";

	private DataTable DT1 = new DataTable();

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

	public FormSys_D_Pick()
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.SysMaintain.FormSys_D_Pick));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool1 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool2 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Popup1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		this.panel2 = new System.Windows.Forms.Panel();
		this.Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.Btn_OK = new Infragistics.Win.Misc.UltraButton();
		this.panel3 = new System.Windows.Forms.Panel();
		this.GridUnit1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this._FormSys_B_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.panel2.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		base.SuspendLayout();
		this.panel2.Controls.Add(this.Btn_Cncl);
		this.panel2.Controls.Add(this.Btn_OK);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 297);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(500, 36);
		this.panel2.TabIndex = 2;
		this.Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.Btn_Cncl.Appearance = appearance1;
		this.Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.Btn_Cncl.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.Btn_Cncl.Location = new System.Drawing.Point(407, 3);
		this.Btn_Cncl.Name = "Btn_Cncl";
		this.Btn_Cncl.ShowFocusRect = false;
		this.Btn_Cncl.ShowOutline = false;
		this.Btn_Cncl.Size = new System.Drawing.Size(88, 28);
		this.Btn_Cncl.SupportThemes = false;
		this.Btn_Cncl.TabIndex = 4;
		this.Btn_Cncl.Text = "取消";
		this.Btn_OK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.Btn_OK.Appearance = appearance2;
		this.Btn_OK.BackColor = System.Drawing.SystemColors.Control;
		this.Btn_OK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.Btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.Btn_OK.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Btn_OK.ImageSize = new System.Drawing.Size(20, 20);
		this.Btn_OK.ImageTransparentColor = System.Drawing.Color.White;
		this.Btn_OK.Location = new System.Drawing.Point(315, 3);
		this.Btn_OK.Name = "Btn_OK";
		this.Btn_OK.ShowFocusRect = false;
		this.Btn_OK.ShowOutline = false;
		this.Btn_OK.Size = new System.Drawing.Size(88, 28);
		this.Btn_OK.SupportThemes = false;
		this.Btn_OK.TabIndex = 3;
		this.Btn_OK.Text = "確定";
		this.Btn_OK.Click += new System.EventHandler(Btn_OK_Click);
		this.panel3.Controls.Add(this.GridUnit1);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 27);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(500, 270);
		this.panel3.TabIndex = 3;
		this.GridUnit1._ExcelFileName = "";
		this.GridUnit1._ExcelSheeName = "";
		this.GridUnit1._IsOpenExcelAfterExport = false;
		this.GridUnit1.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
		this.GridUnit1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.GridUnit1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridUnit1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.GridUnit1.ColumnInfo = "4,1,0,0,0,110,Columns:0{Width:17;Name:\"RowIndicator\";AllowDragging:False;AllowResizing:False;AllowEditing:False;DataType:System.Int32;TextAlign:RightTop;TextAlignFixed:GeneralTop;}\t1{Width:110;Name:\"Invoice_No\";Caption:\"廠商統編\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t2{Width:300;Name:\"Title\";Caption:\"廠商名稱\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t3{Width:100;Name:\"Shortitle\";Caption:\"廠商簡稱\";Visible:False;AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t";
		this.GridUnit1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridUnit1.ExtendLastCol = true;
		this.GridUnit1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridUnit1.ForeColor = System.Drawing.Color.Black;
		this.GridUnit1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.GridUnit1.IsProcessUndo = false;
		this.GridUnit1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.GridUnit1.Location = new System.Drawing.Point(0, 0);
		this.GridUnit1.Name = "GridUnit1";
		this.GridUnit1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.GridUnit1.ShowCursor = true;
		this.GridUnit1.ShowToolTipOnNarrowColumn = true;
		this.GridUnit1.Size = new System.Drawing.Size(500, 270);
		this.GridUnit1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11.25pt;BackColor:237, 243, 254;ForeColor:Black;TextAlign:GeneralBottom;Border:Flat,1,Silver,Both;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;TextAlign:GeneralTop;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;TextAlign:LeftCenter;TextEffect:Flat;ImageAlign:CenterCenter;ImageSpacing:2;Trimming:None;WordWrap:False;Border:None,1,Black,Both;Display:Stack;Format:\"###,###,###,##0\";}\tFocus{Font:Arial, 10.5pt;BackColor:102, 153, 255;Margins:0, 0, 0, 0;Border:None,1,Black,Both;}\tSearch{BackColor:Highlight;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.GridUnit1.TabIndex = 9;
		this.GridUnit1.UndoMax = 10;
		this.GridUnit1.DoubleClick += new System.EventHandler(GridUnit1_DoubleClick);
		appearance3.FontData.Name = "Arial";
		appearance3.FontData.SizeInPoints = 9f;
		this.ultraToolbarsManager1.Appearance = appearance3;
		appearance4.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance4.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.DockAreaAppearance = appearance4;
		this.ultraToolbarsManager1.DockWithinContainer = this;
		this.ultraToolbarsManager1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraToolbarsManager1.LockToolbars = true;
		appearance5.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance5.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance5.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.MenuSettings.HotTrackAppearance = appearance5;
		appearance6.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance6.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsManager1.MenuSettings.IconAreaAppearance = appearance6;
		appearance7.BackColor = System.Drawing.Color.White;
		appearance7.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.MenuSettings.ToolAppearance = appearance7;
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
		ultraToolbar1.Settings.AllowDockTop = Infragistics.Win.DefaultableBoolean.True;
		ultraToolbar1.Text = "Tool1";
		labelTool1.InstanceProps.IsFirstInGroup = true;
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { buttonTool1, labelTool1, comboBoxTool1, buttonTool2 });
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[1] { ultraToolbar1 });
		appearance8.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance8.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance8;
		appearance9.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance9.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance9.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance9;
		appearance10.Image = resources.GetObject("appearance10.Image");
		buttonTool3.SharedProps.AppearancesSmall.Appearance = appearance10;
		buttonTool3.SharedProps.Caption = "刪除";
		buttonTool3.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool3.SharedProps.Visible = false;
		labelTool2.SharedProps.Caption = "尋找:";
		labelTool2.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool2.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool2.SharedProps.Caption = "輸入關鍵字";
		comboBoxTool2.SharedProps.Width = 200;
		comboBoxTool2.ValueList = valueList1;
		appearance11.Image = resources.GetObject("appearance11.Image");
		buttonTool4.SharedProps.AppearancesSmall.Appearance = appearance11;
		buttonTool4.SharedProps.Caption = "Go";
		popupMenuTool1.SharedProps.Caption = "右鍵功能表";
		popupMenuTool1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[1] { buttonTool5 });
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[5] { buttonTool3, labelTool2, comboBoxTool2, buttonTool4, popupMenuTool1 });
		this.ultraToolbarsManager1.BeforeToolbarListDropdown += new Infragistics.Win.UltraWinToolbars.BeforeToolbarListDropdownEventHandler(ultraToolbarsManager1_BeforeToolbarListDropdown);
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this.ultraToolbarsManager1.ToolKeyPress += new Infragistics.Win.UltraWinToolbars.ToolKeyPressEventHandler(ultraToolbarsManager1_ToolKeyPress);
		this._FormSys_B_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormSys_B_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormSys_B_Toolbars_Dock_Area_Top.Name = "_FormSys_B_Toolbars_Dock_Area_Top";
		this._FormSys_B_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(500, 27);
		this._FormSys_B_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 333);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Name = "_FormSys_B_Toolbars_Dock_Area_Bottom";
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(500, 0);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormSys_B_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 27);
		this._FormSys_B_Toolbars_Dock_Area_Left.Name = "_FormSys_B_Toolbars_Dock_Area_Left";
		this._FormSys_B_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 306);
		this._FormSys_B_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormSys_B_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(500, 27);
		this._FormSys_B_Toolbars_Dock_Area_Right.Name = "_FormSys_B_Toolbars_Dock_Area_Right";
		this._FormSys_B_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 306);
		this._FormSys_B_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.ClientSize = new System.Drawing.Size(500, 333);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Bottom);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.MinimizeBox = false;
		base.Name = "FormSys_D_Pick";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "廠商挑選";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormSys_D_Pick_KeyDown);
		base.Load += new System.EventHandler(FormSys_D_Pick_Load);
		this.panel2.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
		base.ResumeLayout(false);
	}

	private void FormSys_D_Pick_Load(object sender, EventArgs e)
	{
		LoadData();
		BindToGrid();
	}

	private void LoadData()
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("廠商資料--讀取");
		Archnowledge.Pcces.BUDClass.Sublet SubletCom = new Archnowledge.Pcces.BUDClass.Sublet(aArr);
		SubletCom.IsArchCOMS = SysConfig.SysComsEnable;
		DT1 = SubletCom.ListItem("");
	}

	private void BindToGrid()
	{
		GridUnit1.Rows.Count = DT1.Rows.Count + 1;
		for (int i = 0; i < DT1.Rows.Count; i++)
		{
			GridUnit1[i + 1, "Invoice_No"] = DT1.Rows[i]["invoice_no"].ToString().Trim();
			GridUnit1[i + 1, "Title"] = DT1.Rows[i]["title"].ToString().Trim();
			GridUnit1[i + 1, "Shortitle"] = DT1.Rows[i]["shortitle"].ToString().Trim();
		}
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		string key = e.Tool.Key;
		if (key != null && key == "mnu_lblFind")
		{
			Do_ToolBarFind();
		}
	}

	private void Do_ToolBarFind()
	{
		if (GridUnit1.Row <= 1)
		{
			return;
		}
		int iStart = GridUnit1.Row + 1;
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
			iStart = GridUnit1.Row + 1;
		}
		if (sSearchText.Trim() == "")
		{
			return;
		}
		for (int i = iStart; i < GridUnit1.Rows.Count; i++)
		{
			for (int j = 1; j < GridUnit1.Cols.Count; j++)
			{
				if (GridUnit1[i, j] == null || GridUnit1[i, j].ToString().IndexOf(sSearchText) <= -1)
				{
					continue;
				}
				GridUnit1.Row = i;
				GridUnit1.Select();
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

	private void ultraToolbarsManager1_ToolKeyPress(object sender, ToolKeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && e.Tool.Key == "mnu_Cbo1")
		{
			Do_ToolBarFind();
		}
	}

	private void Btn_OK_Click(object sender, EventArgs e)
	{
		if (GridUnit1.Row <= 0)
		{
			string sWarning = "請先選定一筆廠商資料";
			MessageBox.Show(this, sWarning, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		Form ActiveForm = base.Owner.ActiveMdiChild;
		if (ActiveForm is frmSysMaintain)
		{
			(ActiveForm as frmSysMaintain)._Invoice_No = GridUnit1[GridUnit1.Row, "Invoice_No"].ToString().Trim();
			(ActiveForm as frmSysMaintain)._Title = GridUnit1[GridUnit1.Row, "Title"].ToString().Trim();
		}
	}

	private void ultraToolbarsManager1_BeforeToolbarListDropdown(object sender, BeforeToolbarListDropdownEventArgs e)
	{
		e.Cancel = true;
	}

	private void FormSys_D_Pick_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormSys_D_Pick");
		}
	}

	private void GridUnit1_DoubleClick(object sender, EventArgs e)
	{
		if (GridUnit1.Row > 0)
		{
			Form ActiveForm = base.Owner.ActiveMdiChild;
			if (ActiveForm is frmSysMaintain)
			{
				(ActiveForm as frmSysMaintain)._Invoice_No = GridUnit1[GridUnit1.Row, "Invoice_No"].ToString().Trim();
				(ActiveForm as frmSysMaintain)._Title = GridUnit1[GridUnit1.Row, "Title"].ToString().Trim();
			}
			base.DialogResult = DialogResult.OK;
		}
	}
}
