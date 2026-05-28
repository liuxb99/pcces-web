using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.DomainModule.Bid;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.MrsBase;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormChangeToCompanyCode : Form
{
	private IContainer components = null;

	private Panel panel2;

	private Panel panel3;

	private GridBudget gridDetailList;

	private UltraButton btnOK;

	private UltraToolbarsManager toolbarsManager;

	private UltraStatusBar statusBar;

	private UltraToolbarsDockArea _FormChangeToCompanyCode_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormChangeToCompanyCode_Toolbars_Dock_Area_Right;

	private UltraToolbarsDockArea _FormChangeToCompanyCode_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormChangeToCompanyCode_Toolbars_Dock_Area_Bottom;

	private Panel panelProjectCode;

	private UltraLabel lbProjectCode;

	private ImageList imageList;

	private UltraButton btnCancel;

	private UltraLabel lbUnchangedCodeItem;

	private UltraLabel ultraLabel5;

	private string userID;

	private string projectCode;

	private string projectName;

	private string sourceDatabase;

	private BidWorkItemMapping bidWorkItemMapping = new BidWorkItemMapping();

	private string[] companyWorkItemArray;

	public string _userID
	{
		get
		{
			return userID;
		}
		set
		{
			userID = value;
		}
	}

	public string _projectCode
	{
		get
		{
			return projectCode;
		}
		set
		{
			projectCode = value;
		}
	}

	public string _projectName
	{
		get
		{
			return projectName;
		}
		set
		{
			projectName = value;
		}
	}

	public string[] _companyWorkItemArray
	{
		get
		{
			return companyWorkItemArray;
		}
		set
		{
			companyWorkItemArray = value;
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormChangeToCompanyCode));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.OptionSet optionSet1 = new Infragistics.Win.UltraWinToolbars.OptionSet("surName");
		Infragistics.Win.UltraWinToolbars.OptionSet optionSet2 = new Infragistics.Win.UltraWinToolbars.OptionSet("Switch");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("尋找");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("lbSearch");
		Infragistics.Win.UltraWinToolbars.TextBoxTool textBoxTool1 = new Infragistics.Win.UltraWinToolbars.TextBoxTool("keyword");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("search");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("lbLevel");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool1 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level1", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool2 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level2", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool3 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level3", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool4 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level4", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool5 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level5", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool6 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level6", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool7 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level7", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool8 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level8", "Switch");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("nextUnchangedItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("pickCompanyCode");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("autoChangeCode");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool3 = new Infragistics.Win.UltraWinToolbars.LabelTool("lbSearch");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("search");
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool4 = new Infragistics.Win.UltraWinToolbars.LabelTool("lbLevel");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool9 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level1", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool10 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level2", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool11 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level3", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool12 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level4", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool13 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level5", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool14 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level6", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool15 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level7", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool16 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level8", "Switch");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("nextUnchangedItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("pickCompanyCode");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("autoChangeCode");
		Infragistics.Win.UltraWinToolbars.TextBoxTool textBoxTool2 = new Infragistics.Win.UltraWinToolbars.TextBoxTool("keyword");
		this.panel2 = new System.Windows.Forms.Panel();
		this.lbUnchangedCodeItem = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.btnCancel = new Infragistics.Win.Misc.UltraButton();
		this.btnOK = new Infragistics.Win.Misc.UltraButton();
		this.panel3 = new System.Windows.Forms.Panel();
		this.gridDetailList = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.panelProjectCode = new System.Windows.Forms.Panel();
		this.lbProjectCode = new Infragistics.Win.Misc.UltraLabel();
		this.statusBar = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.toolbarsManager = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.imageList = new System.Windows.Forms.ImageList(this.components);
		this.panel2.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridDetailList).BeginInit();
		this.panelProjectCode.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.toolbarsManager).BeginInit();
		base.SuspendLayout();
		this.panel2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel2.Controls.Add(this.lbUnchangedCodeItem);
		this.panel2.Controls.Add(this.ultraLabel5);
		this.panel2.Controls.Add(this.btnCancel);
		this.panel2.Controls.Add(this.btnOK);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 565);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(992, 46);
		this.panel2.TabIndex = 1;
		this.lbUnchangedCodeItem.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lbUnchangedCodeItem.BackColor = System.Drawing.Color.Khaki;
		this.lbUnchangedCodeItem.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.Solid;
		this.lbUnchangedCodeItem.Location = new System.Drawing.Point(20, 11);
		this.lbUnchangedCodeItem.Name = "lbUnchangedCodeItem";
		this.lbUnchangedCodeItem.Size = new System.Drawing.Size(56, 25);
		this.lbUnchangedCodeItem.TabIndex = 21;
		this.ultraLabel5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.ultraLabel5.Font = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel5.Location = new System.Drawing.Point(82, 14);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(148, 23);
		this.ultraLabel5.TabIndex = 22;
		this.ultraLabel5.Text = "：尚未換碼的工項";
		this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnCancel.Appearance = appearance1;
		this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
		this.btnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Font = new System.Drawing.Font("細明體", 11f);
		this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btnCancel.Location = new System.Drawing.Point(892, 8);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.ShowFocusRect = false;
		this.btnCancel.ShowOutline = false;
		this.btnCancel.Size = new System.Drawing.Size(88, 31);
		this.btnCancel.SupportThemes = false;
		this.btnCancel.TabIndex = 12;
		this.btnCancel.Text = "取消";
		this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnOK.Appearance = appearance2;
		this.btnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnOK.Font = new System.Drawing.Font("細明體", 11f);
		this.btnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.btnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.btnOK.Location = new System.Drawing.Point(798, 8);
		this.btnOK.Name = "btnOK";
		this.btnOK.ShowFocusRect = false;
		this.btnOK.ShowOutline = false;
		this.btnOK.Size = new System.Drawing.Size(88, 31);
		this.btnOK.SupportThemes = false;
		this.btnOK.TabIndex = 11;
		this.btnOK.Text = "確定";
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.panel3.Controls.Add(this.gridDetailList);
		this.panel3.Controls.Add(this.panelProjectCode);
		this.panel3.Controls.Add(this.statusBar);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 27);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(992, 538);
		this.panel3.TabIndex = 2;
		this.gridDetailList._ExcelFileName = "";
		this.gridDetailList._ExcelSheeName = "";
		this.gridDetailList._IsOpenExcelAfterExport = false;
		this.gridDetailList.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridDetailList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridDetailList.ColumnInfo = resources.GetString("gridDetailList.ColumnInfo");
		this.gridDetailList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridDetailList.ExtendLastCol = true;
		this.gridDetailList.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridDetailList.ForeColor = System.Drawing.Color.Black;
		this.gridDetailList.Location = new System.Drawing.Point(0, 30);
		this.gridDetailList.Name = "gridDetailList";
		this.gridDetailList.Rows.Count = 1;
		this.gridDetailList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.gridDetailList.ShowCursor = true;
		this.gridDetailList.ShowSort = false;
		this.gridDetailList.ShowToolTipOnNarrowColumn = true;
		this.gridDetailList.Size = new System.Drawing.Size(992, 482);
		this.gridDetailList.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridDetailList.Styles"));
		this.gridDetailList.TabIndex = 1;
		this.gridDetailList.Tree.Column = 1;
		this.gridDetailList.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridDetailList.SelChange += new System.EventHandler(gridDetailList_SelChange);
		this.panelProjectCode.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.panelProjectCode.Controls.Add(this.lbProjectCode);
		this.panelProjectCode.Dock = System.Windows.Forms.DockStyle.Top;
		this.panelProjectCode.Location = new System.Drawing.Point(0, 0);
		this.panelProjectCode.Name = "panelProjectCode";
		this.panelProjectCode.Size = new System.Drawing.Size(992, 30);
		this.panelProjectCode.TabIndex = 5;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbProjectCode.Appearance = appearance3;
		this.lbProjectCode.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.lbProjectCode.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbProjectCode.Location = new System.Drawing.Point(12, 7);
		this.lbProjectCode.Name = "lbProjectCode";
		this.lbProjectCode.Size = new System.Drawing.Size(527, 19);
		this.lbProjectCode.TabIndex = 14;
		this.lbProjectCode.Text = "原專案代號：";
		appearance4.FontData.SizeInPoints = 11f;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.statusBar.Appearance = appearance4;
		this.statusBar.Location = new System.Drawing.Point(0, 512);
		this.statusBar.Name = "statusBar";
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraStatusPanel1.Appearance = appearance5;
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Key = "RowsCount";
		ultraStatusPanel1.Text = "資料筆數：";
		ultraStatusPanel1.Width = 180;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		appearance6.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
		appearance6.BackColor2 = System.Drawing.Color.Navy;
		appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		ultraStatusPanel2.ProgressBarInfo.Appearance = appearance6;
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		appearance7.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraStatusPanel3.Appearance = appearance7;
		ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel3.Text = "客服電話：(02)2716-5561";
		ultraStatusPanel3.Width = 200;
		this.statusBar.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[3] { ultraStatusPanel1, ultraStatusPanel2, ultraStatusPanel3 });
		this.statusBar.Size = new System.Drawing.Size(992, 26);
		this.statusBar.SupportThemes = false;
		this.statusBar.TabIndex = 4;
		this.statusBar.Text = "ultraStatusBar1";
		appearance8.FontData.Name = "Arial";
		appearance8.FontData.SizeInPoints = 9f;
		this.toolbarsManager.Appearance = appearance8;
		appearance9.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance9.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.toolbarsManager.DockAreaAppearance = appearance9;
		this.toolbarsManager.DockWithinContainer = this;
		this.toolbarsManager.ImageTransparentColor = System.Drawing.Color.White;
		this.toolbarsManager.LockToolbars = true;
		appearance10.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance10.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance10.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.toolbarsManager.MenuSettings.HotTrackAppearance = appearance10;
		appearance11.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance11.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.toolbarsManager.MenuSettings.IconAreaAppearance = appearance11;
		appearance12.BackColor = System.Drawing.Color.White;
		appearance12.BackColor2 = System.Drawing.Color.White;
		this.toolbarsManager.MenuSettings.ToolAppearance = appearance12;
		optionSet1.AllowAllUp = false;
		optionSet2.AllowAllUp = false;
		this.toolbarsManager.OptionSets.Add(optionSet1);
		this.toolbarsManager.OptionSets.Add(optionSet2);
		this.toolbarsManager.ShowFullMenusDelay = 500;
		this.toolbarsManager.ShowQuickCustomizeButton = false;
		this.toolbarsManager.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.IsMainMenuBar = true;
		ultraToolbar1.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
		ultraToolbar1.Text = "工具列";
		textBoxTool1.InstanceProps.Width = 165;
		labelTool2.InstanceProps.IsFirstInGroup = true;
		stateButtonTool1.Checked = true;
		buttonTool2.InstanceProps.IsFirstInGroup = true;
		buttonTool3.InstanceProps.IsFirstInGroup = true;
		buttonTool4.InstanceProps.IsFirstInGroup = true;
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[15]
		{
			labelTool1, textBoxTool1, buttonTool1, labelTool2, stateButtonTool1, stateButtonTool2, stateButtonTool3, stateButtonTool4, stateButtonTool5, stateButtonTool6,
			stateButtonTool7, stateButtonTool8, buttonTool2, buttonTool3, buttonTool4
		});
		this.toolbarsManager.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[1] { ultraToolbar1 });
		this.toolbarsManager.ToolbarSettings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
		this.toolbarsManager.ToolbarSettings.AllowHiding = Infragistics.Win.DefaultableBoolean.False;
		labelTool3.SharedProps.Caption = "尋找:";
		labelTool3.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		appearance13.Image = resources.GetObject("appearance13.Image");
		buttonTool5.SharedProps.AppearancesSmall.Appearance = appearance13;
		buttonTool5.SharedProps.Caption = "執行尋找";
		labelTool4.SharedProps.Caption = "階層:";
		labelTool4.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool9.Checked = true;
		stateButtonTool9.OptionSetKey = "Switch";
		stateButtonTool9.SharedProps.Caption = "1";
		stateButtonTool9.SharedProps.Category = "LevelSwitch";
		stateButtonTool9.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool10.OptionSetKey = "Switch";
		stateButtonTool10.SharedProps.Caption = "2";
		stateButtonTool10.SharedProps.Category = "LevelSwitch";
		stateButtonTool10.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool11.OptionSetKey = "Switch";
		stateButtonTool11.SharedProps.Caption = "3";
		stateButtonTool11.SharedProps.Category = "LevelSwitch";
		stateButtonTool11.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool12.OptionSetKey = "Switch";
		stateButtonTool12.SharedProps.Caption = "4";
		stateButtonTool12.SharedProps.Category = "LevelSwitch";
		stateButtonTool12.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool13.OptionSetKey = "Switch";
		stateButtonTool13.SharedProps.Caption = "5";
		stateButtonTool13.SharedProps.Category = "LevelSwitch";
		stateButtonTool13.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool14.OptionSetKey = "Switch";
		stateButtonTool14.SharedProps.Caption = "6";
		stateButtonTool14.SharedProps.Category = "LevelSwitch";
		stateButtonTool14.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool15.OptionSetKey = "Switch";
		stateButtonTool15.SharedProps.Caption = "7";
		stateButtonTool15.SharedProps.Category = "LevelSwitch";
		stateButtonTool15.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool16.OptionSetKey = "Switch";
		stateButtonTool16.SharedProps.Caption = "8";
		stateButtonTool16.SharedProps.Category = "LevelSwitch";
		stateButtonTool16.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool6.SharedProps.Caption = "下一筆未換碼工項";
		buttonTool6.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool7.SharedProps.Caption = "挑選對應工項";
		buttonTool7.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool8.SharedProps.Caption = "自動對應工項";
		buttonTool8.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		textBoxTool2.MaxLength = 80;
		textBoxTool2.SharedProps.Caption = "輸入關鍵字";
		this.toolbarsManager.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[15]
		{
			labelTool3, buttonTool5, labelTool4, stateButtonTool9, stateButtonTool10, stateButtonTool11, stateButtonTool12, stateButtonTool13, stateButtonTool14, stateButtonTool15,
			stateButtonTool16, buttonTool6, buttonTool7, buttonTool8, textBoxTool2
		});
		this.toolbarsManager.ToolKeyPress += new Infragistics.Win.UltraWinToolbars.ToolKeyPressEventHandler(toolbarsManager_ToolKeyPress);
		this.toolbarsManager.BeforeToolbarListDropdown += new Infragistics.Win.UltraWinToolbars.BeforeToolbarListDropdownEventHandler(toolbarsManager_BeforeToolbarListDropdown);
		this.toolbarsManager.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(toolbarsManager_ToolClick);
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(992, 27);
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Right.Name = "_FormChangeToCompanyCode_Toolbars_Dock_Area_Right";
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 584);
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Right.ToolbarsManager = this.toolbarsManager;
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 27);
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Left.Name = "_FormChangeToCompanyCode_Toolbars_Dock_Area_Left";
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 584);
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Left.ToolbarsManager = this.toolbarsManager;
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 611);
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Bottom.Name = "_FormChangeToCompanyCode_Toolbars_Dock_Area_Bottom";
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(992, 0);
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.toolbarsManager;
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Top.Name = "_FormChangeToCompanyCode_Toolbars_Dock_Area_Top";
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(992, 27);
		this._FormChangeToCompanyCode_Toolbars_Dock_Area_Top.ToolbarsManager = this.toolbarsManager;
		this.imageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList.ImageStream");
		this.imageList.TransparentColor = System.Drawing.Color.White;
		this.imageList.Images.SetKeyName(0, "");
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(992, 611);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this._FormChangeToCompanyCode_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormChangeToCompanyCode_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormChangeToCompanyCode_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormChangeToCompanyCode_Toolbars_Dock_Area_Bottom);
		base.Name = "FormChangeToCompanyCode";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		this.Text = "業主碼換公司碼";
		base.Load += new System.EventHandler(FormChangeToCompanyCode_Load);
		this.panel2.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridDetailList).EndInit();
		this.panelProjectCode.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.toolbarsManager).EndInit();
		base.ResumeLayout(false);
	}

	public FormChangeToCompanyCode()
	{
		InitializeComponent();
	}

	private void FormChangeToCompanyCode_Load(object sender, EventArgs e)
	{
		lbProjectCode.Text = "目前專案：【" + projectCode + "】" + projectName;
		BidProject bidProject = new BidProject();
		sourceDatabase = bidProject.GetProject(projectCode).Tables[0].Rows[0]["sourceDatabase"].ToString().Trim();
		BidItemA itemA = new BidItemA();
		DataSet dsItemA = itemA.GetItemAWithCompanyCode(projectCode);
		DataToGrid(dsItemA);
	}

	private void DataToGrid(DataSet dsItemA)
	{
		SetupCellStyle();
		DataTable dtItemA = dsItemA.Tables[0];
		gridDetailList.Rows.Count = dtItemA.Rows.Count + 1;
		statusBar.Panels[0].Text = "資料筆數：" + dtItemA.Rows.Count;
		string itemType = string.Empty;
		int maxLevel = 1;
		gridDetailList.Redraw = false;
		for (int i = 0; i < dtItemA.Rows.Count; i++)
		{
			Row gridRow = gridDetailList.Rows[i + 1];
			try
			{
				itemType = (string)(gridRow["ItemType"] = dtItemA.Rows[i]["kind"].ToString());
				switch (itemType)
				{
				default:
					if (!(itemType == "U"))
					{
						break;
					}
					goto case "B";
				case "B":
				case "L":
				case "F":
				case "S":
				case "Z":
					gridRow.Style = gridDetailList.Styles["MainItem"];
					break;
				}
				FillInOwnerWorkItemData(i + 1, dtItemA.Rows[i]["ItemNo"].ToString().Trim(), dtItemA.Rows[i]["cName"].ToString().Trim(), dtItemA.Rows[i]["unitName"].ToString().Trim(), dtItemA.Rows[i]["pccesCode"].ToString().Trim(), dtItemA.Rows[i]["analysis"].ToString().Trim());
				FillInCompanyWorkItemData(i + 1, dtItemA.Rows[i]["extendCode"].ToString().Trim(), dtItemA.Rows[i]["CompanyCName"].ToString().Trim(), dtItemA.Rows[i]["CompanyUnitName"].ToString().Trim(), dtItemA.Rows[i]["CompanyAnalysis"].ToString().Trim());
				if (itemType == "W" && gridRow["ExtendCode"].ToString() == string.Empty)
				{
					gridRow.Style = gridDetailList.Styles["UnChangedCodeItem"];
				}
				gridRow.IsNode = true;
				string PrintNo = dtItemA.Rows[i]["PrintNo"].ToString().Trim();
				if (PrintNo == "".PadLeft(32, '9'))
				{
					gridRow.Node.Level = 1;
				}
				else if (PrintNo.Length == 4 && dtItemA.Rows[i]["Kind"].ToString().Trim() == "Z" && i == dtItemA.Rows.Count - 1)
				{
					gridRow.Node.Level = 1;
				}
				else
				{
					gridRow.Node.Level = Convert.ToInt32(PrintNo.Length / 4);
					if (gridRow.Node.Level != ArchConvert.Obj2Int(dtItemA.Rows[i]["LevelNo"]))
					{
						MessageBox.Show(ArchConvert.Obj2String(dtItemA.Rows[i]["ItemNo"]) + " 在資料庫中的階層資訊不一致 (LevelNo 及 PrintNo不一致)，請確定顯示是否正確。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
				if (gridRow.Node.Level > maxLevel)
				{
					maxLevel = gridRow.Node.Level;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("DataToGrid Error : " + ex.Message);
			}
		}
		gridDetailList.Redraw = true;
		SetBtnPickCompanyCodeStatus();
		UpdateLevelButtonStatus(maxLevel);
	}

	private void SetupCellStyle()
	{
		CellStyle csAnalysis = gridDetailList.Styles.Add("AnalysisItem");
		csAnalysis.ForeColor = Color.Red;
		CellStyle csMainItem = gridDetailList.Styles.Add("MainItem");
		csMainItem.ForeColor = Color.Blue;
		CellStyle csUnChangedCodeItem = gridDetailList.Styles.Add("UnChangedCodeItem");
		csUnChangedCodeItem.BackColor = lbUnchangedCodeItem.BackColor;
	}

	private void FillInOwnerWorkItemData(int rowIndex, string ItemNo, string ownerPccesCode, string cName, string unitName, string isAnalysis)
	{
		gridDetailList[rowIndex, "ItemNo"] = ItemNo;
		gridDetailList[rowIndex, "CName"] = ownerPccesCode;
		gridDetailList[rowIndex, "UnitName"] = unitName;
		gridDetailList[rowIndex, "PccesCode"] = ownerPccesCode;
		if (isAnalysis == "1")
		{
			CellRange crOriginal = gridDetailList.GetCellRange(rowIndex, gridDetailList.Cols["itemNo"].SafeIndex, rowIndex, gridDetailList.Cols["analysis"].SafeIndex);
			crOriginal.Style = gridDetailList.Styles["AnalysisItem"];
			CellRange cellRange = gridDetailList.GetCellRange(rowIndex, gridDetailList.Cols["analysis"].SafeIndex);
			cellRange.Style = gridDetailList.Styles["img"];
			cellRange.Image = imageList.Images[0];
		}
	}

	private void FillInCompanyWorkItemData(int rowIndex, string companyPccesCode, string cName, string unitName, string isAnalysis)
	{
		gridDetailList[rowIndex, "ExtendCode"] = companyPccesCode;
		gridDetailList[rowIndex, "CompanyCName"] = cName;
		gridDetailList[rowIndex, "CompanyUnitName"] = unitName;
		if (isAnalysis == "1")
		{
			CellRange crCompany = gridDetailList.GetCellRange(rowIndex, gridDetailList.Cols["ExtendCode"].SafeIndex, rowIndex, gridDetailList.Cols["CompanyAnalysis"].SafeIndex);
			crCompany.Style = gridDetailList.Styles["AnalysisItem"];
			CellRange cellRange = gridDetailList.GetCellRange(rowIndex, gridDetailList.Cols["CompanyAnalysis"].SafeIndex);
			cellRange.Style = gridDetailList.Styles["img"];
			cellRange.Image = imageList.Images[0];
		}
	}

	private void UpdateLevelButtonStatus(int level)
	{
		if (level > 0 && level < 9)
		{
			((StateButtonTool)toolbarsManager.Tools["Level" + level]).Checked = true;
			for (int i = 1; i < 9; i++)
			{
				((StateButtonTool)toolbarsManager.Tools["Level" + i]).SharedProps.Enabled = i <= level;
			}
		}
	}

	private void toolbarsManager_ToolClick(object sender, ToolClickEventArgs e)
	{
		switch (e.Tool.Key)
		{
		case "search":
			gridDetailList.Row = getKeywordMatchedRowIndex(gridDetailList.Row + 1, ((TextBoxTool)toolbarsManager.Tools["keyword"]).Text.Trim());
			break;
		case "Level1":
		case "Level2":
		case "Level3":
		case "Level4":
		case "Level5":
		case "Level6":
		case "Level7":
		case "Level8":
			gridDetailList.Tree.Show(ArchConvert.Obj2Int(e.Tool.Key[5].ToString()));
			break;
		case "nextUnchangedItem":
			gridDetailList.Row = getNextUnchangedItemIndex(gridDetailList.Row + 1);
			break;
		case "pickCompanyCode":
			OpenPickCompanyCodeWindow();
			break;
		case "autoChangeCode":
		{
			string warningMessage = "執行自動對應會依據既有的資料將業主碼對應至公司碼，確定執行？";
			DialogResult result = MessageBox.Show(this, warningMessage, "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (result == DialogResult.Yes)
			{
				AutoChangeToCompanyCode();
				MessageBox.Show(this, "自動對應完成！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			break;
		}
		}
	}

	private void toolbarsManager_ToolKeyPress(object sender, ToolKeyPressEventArgs e)
	{
		if (e.Tool.Key == "keyword" && e.KeyChar == '\r')
		{
			gridDetailList.Row = getKeywordMatchedRowIndex(gridDetailList.Row + 1, ((TextBoxTool)toolbarsManager.Tools["keyword"]).Text.Trim());
		}
	}

	private int getNextUnchangedItemIndex(int startRow)
	{
		for (int i = startRow; i < gridDetailList.Rows.Count; i++)
		{
			if (gridDetailList.Rows[i]["ItemType"].ToString() == "W" && gridDetailList.Rows[i]["ExtendCode"].ToString() == string.Empty)
			{
				return i;
			}
		}
		return (startRow != 1) ? getNextUnchangedItemIndex(1) : gridDetailList.Row;
	}

	private int getKeywordMatchedRowIndex(int startRow, string keyword)
	{
		if (keyword == string.Empty)
		{
			return gridDetailList.Row;
		}
		for (int row = startRow; row < gridDetailList.Rows.Count; row++)
		{
			for (int column = 1; column < gridDetailList.Cols.Count - 1; column++)
			{
				if (gridDetailList.Rows[row][column] != null && gridDetailList.Rows[row][column].ToString().Contains(keyword))
				{
					return row;
				}
			}
		}
		return (startRow != 1) ? getKeywordMatchedRowIndex(1, keyword) : gridDetailList.Row;
	}

	private void OpenPickCompanyCodeWindow()
	{
		FormMrsBaseBreakdown_Addnew formPickCompanyCode = new FormMrsBaseBreakdown_Addnew();
		formPickCompanyCode._ChangeCodeMode = true;
		formPickCompanyCode._UserID = userID;
		DialogResult result = formPickCompanyCode.ShowDialog(this);
		if (result == DialogResult.OK && companyWorkItemArray != null)
		{
			string pccesCode = gridDetailList[gridDetailList.Row, "pccesCode"].ToString();
			for (int rowIndex = 1; rowIndex < gridDetailList.Rows.Count; rowIndex++)
			{
				if (gridDetailList[rowIndex, "pccesCode"].ToString() == pccesCode)
				{
					gridDetailList.Rows[rowIndex].Style = null;
					FillInCompanyWorkItemData(rowIndex, companyWorkItemArray[0], companyWorkItemArray[1], companyWorkItemArray[2], companyWorkItemArray[3]);
				}
			}
		}
		companyWorkItemArray = null;
		formPickCompanyCode.Dispose();
		formPickCompanyCode = null;
	}

	private void AutoChangeToCompanyCode()
	{
		DataSet dsWorkItemMapping = bidWorkItemMapping.GetWorkItemMappingForAutoChangeCode(projectCode, sourceDatabase);
		DataTable dtWorkItemMapping = dsWorkItemMapping.Tables[0];
		dtWorkItemMapping.PrimaryKey = new DataColumn[1] { dtWorkItemMapping.Columns["OwnerPccesCode"] };
		for (int rowIndex = 1; rowIndex < gridDetailList.Rows.Count; rowIndex++)
		{
			DataRow row = dtWorkItemMapping.Rows.Find(gridDetailList[rowIndex, "pccesCode"]);
			if (row != null)
			{
				gridDetailList.Rows[rowIndex].Style = null;
				FillInCompanyWorkItemData(rowIndex, row["CompanyPccesCode"].ToString(), row["cName"].ToString(), row["unitName"].ToString(), row["analysis"].ToString());
			}
		}
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		DataSet dsWorkItemMapping = bidWorkItemMapping.GetWorkItemMapping(projectCode, sourceDatabase);
		DataTable dtWorkItemMapping = dsWorkItemMapping.Tables[0];
		dtWorkItemMapping.PrimaryKey = new DataColumn[1] { dtWorkItemMapping.Columns["OwnerPccesCode"] };
		for (int rowIndex = 1; rowIndex < gridDetailList.Rows.Count; rowIndex++)
		{
			Row gridRow = gridDetailList.Rows[rowIndex];
			string companyPccesCode = gridRow["ExtendCode"].ToString();
			if (!(gridRow["ItemType"].ToString() != "W") && !(companyPccesCode == string.Empty))
			{
				DataRow row = dtWorkItemMapping.Rows.Find(gridRow["PccesCode"]);
				if (row == null)
				{
					DataRow newRow = dtWorkItemMapping.NewRow();
					newRow["ProjectCode"] = projectCode;
					newRow["OwnerPccesCode"] = gridRow["PccesCode"];
					newRow["CompanyPccesCode"] = companyPccesCode;
					dtWorkItemMapping.Rows.Add(newRow);
				}
				else
				{
					row["CompanyPccesCode"] = companyPccesCode;
				}
			}
		}
		bidWorkItemMapping.GetDatasetUpdate(dsWorkItemMapping, sourceDatabase);
		foreach (DataRow row in dtWorkItemMapping.Rows)
		{
			row.SetModified();
		}
		BidProjMrsA bidProjMrsA = new BidProjMrsA();
		bidProjMrsA.UpdateProjMrsAForCompanyCode(dsWorkItemMapping);
	}

	private void gridDetailList_SelChange(object sender, EventArgs e)
	{
		SetBtnPickCompanyCodeStatus();
	}

	private void SetBtnPickCompanyCodeStatus()
	{
		if (gridDetailList.Rows[gridDetailList.Row]["ItemType"] != null)
		{
			((ButtonTool)toolbarsManager.Tools["pickCompanyCode"]).SharedProps.Enabled = gridDetailList.Rows[gridDetailList.Row]["ItemType"].ToString() == "W";
		}
	}

	private void toolbarsManager_BeforeToolbarListDropdown(object sender, BeforeToolbarListDropdownEventArgs e)
	{
		e.Cancel = true;
	}
}
