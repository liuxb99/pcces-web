using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.MrsBase;

public class FormConCost : Form
{
	private UltraToolbarsManager ultraToolbarsManager1;

	private IContainer components;

	private Panel panel1;

	private UltraButton D_Btn_Fnsh;

	private GroupBox groupBox1;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Bottom;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Right;

	private Panel panel2;

	private Panel panel3;

	private Splitter splitter1;

	private Panel panel4;

	private UltraLabel ultraLabel1;

	public GridMrsBase Grid1;

	private C1FlexGrid c1FlexGrid1;

	private Panel panel5;

	private UltraStatusBar ultraStatusBar1;

	private UltraToolbarsDockArea ultraToolbarsDockArea1;

	private UltraToolbarsDockArea ultraToolbarsDockArea2;

	private UltraToolbarsDockArea ultraToolbarsDockArea3;

	private UltraToolbarsDockArea ultraToolbarsDockArea5;

	private UltraToolbarsDockArea ultraToolbarsDockArea6;

	private Panel panel1_Fill_Panel;

	private UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Bottom;

	private string F_UserID;

	private string BIND_FLAG1 = "";

	private string F_KeyWord = "";

	private DataView DV;

	private string sPccesCode = "";

	private string sCName = "";

	private string sUnitName = "";

	private string sSurName = "";

	private string sflag = "";

	private string sCost = "";

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

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.MrsBase.FormConCost));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.OptionSet optionSet1 = new Infragistics.Win.UltraWinToolbars.OptionSet("ViewType");
		Infragistics.Win.UltraWinToolbars.OptionSet optionSet2 = new Infragistics.Win.UltraWinToolbars.OptionSet("View");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool1");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool1 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuView_ItemAll", "View");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool2 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuW", "View");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool3 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuL", "View");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool4 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuE", "View");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool5 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuM", "View");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool6 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuMisc", "View");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool1 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFilter");
		Infragistics.Win.UltraWinToolbars.TextBoxTool textBoxTool1 = new Infragistics.Win.UltraWinToolbars.TextBoxTool("Other_QueryText");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_GoFilter");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar2 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Menu");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuFile");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool2 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuView");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool3 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuTool");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar3 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("PopupMenuTool1");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar4 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("UltraToolbar1");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool7 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuExistItems", "View");
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool3 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool2 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool4 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuFile");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExit");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool5 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuView");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool6 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuViewKind");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool7 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuViewKind");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool8 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuW", "View");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool9 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuL", "View");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool10 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuE", "View");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool11 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuM", "View");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool12 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuMisc", "View");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool13 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuW", "View");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool14 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuL", "View");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool15 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuE", "View");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool16 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuM", "View");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool17 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuMisc", "View");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool8 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuTool");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUpdate");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSendtoMrsBase");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUpdate");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSendtoMrsBase");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool9 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopupMenuTool1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool10 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MnuImport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool11 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUpdateCost");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool12 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MnuDel");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool13 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MnuImport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool14 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MnuDel");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool18 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuView_ItemAll", "View");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool4 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFilter");
		Infragistics.Win.UltraWinToolbars.TextBoxTool textBoxTool2 = new Infragistics.Win.UltraWinToolbars.TextBoxTool("Other_QueryText");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool15 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_GoFilter");
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool19 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuExistItems", "View");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool16 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUpdateCost");
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel1_Fill_Panel = new System.Windows.Forms.Panel();
		this.D_Btn_Fnsh = new Infragistics.Win.Misc.UltraButton();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this.Grid1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
		this._FormSys_B_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.splitter1 = new System.Windows.Forms.Splitter();
		this.panel3 = new System.Windows.Forms.Panel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraToolbarsDockArea1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.ultraToolbarsDockArea2 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.ultraToolbarsDockArea3 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.ultraToolbarsDockArea5 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.ultraToolbarsDockArea6 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._panel1_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.panel1.SuspendLayout();
		this.panel1_Fill_Panel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Grid1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).BeginInit();
		this.panel2.SuspendLayout();
		this.panel4.SuspendLayout();
		this.panel5.SuspendLayout();
		this.panel3.SuspendLayout();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.panel1_Fill_Panel);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 496);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(966, 45);
		this.panel1.TabIndex = 12;
		this.panel1_Fill_Panel.Controls.Add(this.D_Btn_Fnsh);
		this.panel1_Fill_Panel.Controls.Add(this.groupBox1);
		this.panel1_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
		this.panel1_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1_Fill_Panel.Location = new System.Drawing.Point(0, 0);
		this.panel1_Fill_Panel.Name = "panel1_Fill_Panel";
		this.panel1_Fill_Panel.Size = new System.Drawing.Size(966, 45);
		this.panel1_Fill_Panel.TabIndex = 0;
		this.D_Btn_Fnsh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Fnsh.Appearance = appearance1;
		this.D_Btn_Fnsh.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Fnsh.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Fnsh.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.D_Btn_Fnsh.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Fnsh.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Fnsh.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Fnsh.Location = new System.Drawing.Point(870, 10);
		this.D_Btn_Fnsh.Name = "D_Btn_Fnsh";
		this.D_Btn_Fnsh.ShowFocusRect = false;
		this.D_Btn_Fnsh.ShowOutline = false;
		this.D_Btn_Fnsh.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Fnsh.SupportThemes = false;
		this.D_Btn_Fnsh.TabIndex = 4;
		this.D_Btn_Fnsh.Text = "結束";
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(966, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		appearance2.FontData.Name = "Arial";
		appearance2.FontData.SizeInPoints = 9f;
		this.ultraToolbarsManager1.Appearance = appearance2;
		appearance3.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance3.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.DockAreaAppearance = appearance3;
		this.ultraToolbarsManager1.DockWithinContainer = this;
		this.ultraToolbarsManager1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraToolbarsManager1.LockToolbars = true;
		appearance9.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance9.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance9.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.MenuSettings.HotTrackAppearance = appearance9;
		appearance10.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance10.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsManager1.MenuSettings.IconAreaAppearance = appearance10;
		appearance11.BackColor = System.Drawing.Color.White;
		appearance11.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.MenuSettings.ToolAppearance = appearance11;
		optionSet2.AllowAllUp = false;
		this.ultraToolbarsManager1.OptionSets.Add(optionSet1);
		this.ultraToolbarsManager1.OptionSets.Add(optionSet2);
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 1;
		ultraToolbar1.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
		ultraToolbar1.Settings.AllowDockTop = Infragistics.Win.DefaultableBoolean.True;
		ultraToolbar1.Text = "Tool1";
		stateButtonTool1.InstanceProps.IsFirstInGroup = true;
		stateButtonTool1.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool2.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool3.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool4.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool5.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool6.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		labelTool1.InstanceProps.IsFirstInGroup = true;
		textBoxTool1.InstanceProps.Width = 113;
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[12]
		{
			stateButtonTool1, stateButtonTool2, stateButtonTool3, stateButtonTool4, stateButtonTool5, stateButtonTool6, labelTool1, comboBoxTool1, buttonTool1, labelTool2,
			textBoxTool1, buttonTool2
		});
		ultraToolbar2.DockedColumn = 0;
		ultraToolbar2.DockedRow = 0;
		ultraToolbar2.IsMainMenuBar = true;
		ultraToolbar2.Text = "Menu";
		ultraToolbar2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { popupMenuTool1, popupMenuTool2, popupMenuTool3 });
		ultraToolbar3.DockedColumn = 0;
		ultraToolbar3.DockedRow = 1;
		ultraToolbar3.Text = "PopupMenuTool1";
		ultraToolbar3.Visible = false;
		ultraToolbar4.DockedColumn = 0;
		ultraToolbar4.DockedRow = 2;
		ultraToolbar4.Text = "UltraToolbar1";
		stateButtonTool7.Checked = true;
		stateButtonTool7.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		ultraToolbar4.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[1] { stateButtonTool7 });
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[4] { ultraToolbar1, ultraToolbar2, ultraToolbar3, ultraToolbar4 });
		appearance12.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance12.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance12;
		appearance13.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance13.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance13.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance13;
		appearance14.BackColor = System.Drawing.Color.FromArgb(255, 128, 0);
		appearance14.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.ToolbarSettings.PressedAppearance = appearance14;
		labelTool3.SharedProps.Caption = "尋找:";
		labelTool3.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool2.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool2.SharedProps.Caption = "輸入關鍵字";
		comboBoxTool2.SharedProps.Width = 200;
		comboBoxTool2.ValueList = valueList1;
		appearance15.Image = resources.GetObject("appearance15.Image");
		buttonTool3.SharedProps.AppearancesSmall.Appearance = appearance15;
		buttonTool3.SharedProps.Caption = "Go";
		popupMenuTool4.SharedProps.Caption = "檔案(&F)";
		popupMenuTool4.SharedProps.Category = "檔案";
		popupMenuTool4.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[1] { buttonTool4 });
		buttonTool5.SharedProps.Caption = "結束公共工程價格資料庫";
		buttonTool5.SharedProps.Category = "檔案";
		popupMenuTool5.SharedProps.Caption = "檢視(&V)";
		popupMenuTool5.SharedProps.Category = "檢視";
		popupMenuTool5.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[1] { popupMenuTool6 });
		popupMenuTool7.SharedProps.Caption = "顯示項目類別";
		popupMenuTool7.SharedProps.Category = "檢視";
		stateButtonTool8.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool9.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool10.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool11.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool12.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		popupMenuTool7.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[5] { stateButtonTool8, stateButtonTool9, stateButtonTool10, stateButtonTool11, stateButtonTool12 });
		stateButtonTool13.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool13.OptionSetKey = "View";
		stateButtonTool13.SharedProps.Caption = "工項";
		stateButtonTool13.SharedProps.Category = "檢視";
		stateButtonTool13.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool14.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool14.OptionSetKey = "View";
		stateButtonTool14.SharedProps.Caption = "人工";
		stateButtonTool14.SharedProps.Category = "檢視";
		stateButtonTool14.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool15.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool15.OptionSetKey = "View";
		stateButtonTool15.SharedProps.Caption = "機具";
		stateButtonTool15.SharedProps.Category = "檢視";
		stateButtonTool15.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool16.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool16.OptionSetKey = "View";
		stateButtonTool16.SharedProps.Caption = "材料";
		stateButtonTool16.SharedProps.Category = "檢視";
		stateButtonTool16.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool17.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool17.OptionSetKey = "View";
		stateButtonTool17.SharedProps.Caption = "雜項";
		stateButtonTool17.SharedProps.Category = "檢視";
		stateButtonTool17.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		popupMenuTool8.SharedProps.Caption = "工具(&T)";
		popupMenuTool8.SharedProps.Category = "工具";
		popupMenuTool8.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool6, buttonTool7 });
		buttonTool8.SharedProps.Caption = "線上更新...";
		buttonTool8.SharedProps.Category = "工具";
		buttonTool9.SharedProps.Caption = "全部傳送至基本資料庫...";
		buttonTool9.SharedProps.Category = "工具";
		popupMenuTool9.SharedProps.Caption = "PopupMenuTool1";
		buttonTool11.InstanceProps.IsFirstInGroup = true;
		popupMenuTool9.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool10, buttonTool11, buttonTool12 });
		buttonTool13.SharedProps.Caption = "新增至基本資料庫";
		buttonTool14.SharedProps.Caption = "刪除";
		stateButtonTool18.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool18.OptionSetKey = "View";
		stateButtonTool18.SharedProps.Caption = "全部工項";
		stateButtonTool18.SharedProps.Category = "檢視";
		stateButtonTool18.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		labelTool4.SharedProps.Caption = "篩選:";
		labelTool4.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		textBoxTool2.SharedProps.Caption = "Other_QueryText";
		appearance16.Image = resources.GetObject("appearance16.Image");
		buttonTool15.SharedProps.AppearancesSmall.Appearance = appearance16;
		buttonTool15.SharedProps.Caption = "Go";
		stateButtonTool19.Checked = true;
		stateButtonTool19.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool19.OptionSetKey = "View";
		stateButtonTool19.SharedProps.Caption = "工項基本資料庫已存在之工項";
		stateButtonTool19.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool16.SharedProps.Caption = "回傳價格至基本資料庫";
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[24]
		{
			labelTool3, comboBoxTool2, buttonTool3, popupMenuTool4, buttonTool5, popupMenuTool5, popupMenuTool7, stateButtonTool13, stateButtonTool14, stateButtonTool15,
			stateButtonTool16, stateButtonTool17, popupMenuTool8, buttonTool8, buttonTool9, popupMenuTool9, buttonTool13, buttonTool14, stateButtonTool18, labelTool4,
			textBoxTool2, buttonTool15, stateButtonTool19, buttonTool16
		});
		this.ultraToolbarsManager1.ToolKeyPress += new Infragistics.Win.UltraWinToolbars.ToolKeyPressEventHandler(ultraToolbarsManager1_ToolKeyPress);
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this.Grid1._ExcelFileName = "";
		this.Grid1._ExcelSheeName = "";
		this.Grid1._IsOpenExcelAfterExport = false;
		this.Grid1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.Grid1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.Grid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.Grid1.ColumnInfo = resources.GetString("Grid1.ColumnInfo");
		this.ultraToolbarsManager1.SetContextMenuUltra(this.Grid1, "PopupMenuTool1");
		this.Grid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Grid1.ExtendLastCol = true;
		this.Grid1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Grid1.ForeColor = System.Drawing.Color.Black;
		this.Grid1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.Grid1.IsProcessUndo = false;
		this.Grid1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.Grid1.Location = new System.Drawing.Point(0, 0);
		this.Grid1.Name = "Grid1";
		this.Grid1.Rows.Count = 1;
		this.Grid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.Grid1.ShowCursor = true;
		this.Grid1.ShowToolTipOnNarrowColumn = true;
		this.Grid1.Size = new System.Drawing.Size(748, 389);
		this.Grid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("Grid1.Styles"));
		this.Grid1.TabIndex = 9;
		this.Grid1.UndoMax = 10;
		this.Grid1.Click += new System.EventHandler(Grid1_Click);
		this.Grid1.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(Grid1_AfterSelChange);
		this.Grid1.MouseLeave += new System.EventHandler(Grid1_MouseLeave);
		this.Grid1.MouseDown += new System.Windows.Forms.MouseEventHandler(Grid1_MouseDown);
		this.c1FlexGrid1.BackColor = System.Drawing.SystemColors.Window;
		this.c1FlexGrid1.ColumnInfo = "2,0,0,0,0,110,Columns:0{DataType:System.String;TextAlign:LeftCenter;}\t1{DataType:System.String;TextAlign:LeftCenter;}\t";
		this.ultraToolbarsManager1.SetContextMenuUltra(this.c1FlexGrid1, "PopupMenuTool1");
		this.c1FlexGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1FlexGrid1.ExtendLastCol = true;
		this.c1FlexGrid1.ForeColor = System.Drawing.SystemColors.WindowText;
		this.c1FlexGrid1.Location = new System.Drawing.Point(0, 30);
		this.c1FlexGrid1.Name = "c1FlexGrid1";
		this.c1FlexGrid1.Rows.Count = 1;
		this.c1FlexGrid1.Rows.Fixed = 0;
		this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.c1FlexGrid1.Size = new System.Drawing.Size(206, 385);
		this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid1.Styles"));
		this.c1FlexGrid1.TabIndex = 1;
		this.c1FlexGrid1.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(c1FlexGrid1_AfterSelChange);
		this.c1FlexGrid1.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(c1FlexGrid1_AfterRowColChange);
		this.c1FlexGrid1.MouseLeave += new System.EventHandler(c1FlexGrid1_MouseLeave);
		this.c1FlexGrid1.MouseDown += new System.Windows.Forms.MouseEventHandler(c1FlexGrid1_MouseDown);
		this._FormSys_B_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormSys_B_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormSys_B_Toolbars_Dock_Area_Top.Name = "_FormSys_B_Toolbars_Dock_Area_Top";
		this._FormSys_B_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(966, 79);
		this._FormSys_B_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 541);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Name = "_FormSys_B_Toolbars_Dock_Area_Bottom";
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(966, 0);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormSys_B_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 79);
		this._FormSys_B_Toolbars_Dock_Area_Left.Name = "_FormSys_B_Toolbars_Dock_Area_Left";
		this._FormSys_B_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 462);
		this._FormSys_B_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormSys_B_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(966, 79);
		this._FormSys_B_Toolbars_Dock_Area_Right.Name = "_FormSys_B_Toolbars_Dock_Area_Right";
		this._FormSys_B_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 462);
		this._FormSys_B_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this.panel2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel2.Controls.Add(this.panel4);
		this.panel2.Controls.Add(this.splitter1);
		this.panel2.Controls.Add(this.panel3);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 79);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(966, 417);
		this.panel2.TabIndex = 21;
		this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel4.Controls.Add(this.Grid1);
		this.panel4.Controls.Add(this.panel5);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel4.Location = new System.Drawing.Point(216, 0);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(750, 417);
		this.panel4.TabIndex = 2;
		this.panel5.BackColor = System.Drawing.SystemColors.Control;
		this.panel5.Controls.Add(this.ultraStatusBar1);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel5.Location = new System.Drawing.Point(0, 389);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(748, 26);
		this.panel5.TabIndex = 10;
		appearance17.FontData.SizeInPoints = 11f;
		this.ultraStatusBar1.Appearance = appearance17;
		this.ultraStatusBar1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 0);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		appearance18.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
		appearance18.BackColor2 = System.Drawing.Color.Navy;
		appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		ultraStatusPanel1.ProgressBarInfo.Appearance = appearance18;
		ultraStatusPanel1.Text = "資料筆數：";
		ultraStatusPanel1.Width = 200;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		appearance19.BackColor = System.Drawing.Color.LightSlateGray;
		appearance19.BackColor2 = System.Drawing.Color.DarkBlue;
		appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		ultraStatusPanel2.ProgressBarInfo.FillAppearance = appearance19;
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		ultraStatusPanel2.Style = Infragistics.Win.UltraWinStatusBar.PanelStyle.Progress;
		ultraStatusPanel2.Width = 0;
		appearance20.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance20.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraStatusPanel3.Appearance = appearance20;
		ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel3.Text = "客服電話:(02)2716-5561";
		ultraStatusPanel3.Width = 200;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[3] { ultraStatusPanel1, ultraStatusPanel2, ultraStatusPanel3 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(748, 26);
		this.ultraStatusBar1.SupportThemes = false;
		this.ultraStatusBar1.TabIndex = 0;
		this.ultraStatusBar1.Text = "ultraStatusBar1";
		this.splitter1.Location = new System.Drawing.Point(208, 0);
		this.splitter1.Name = "splitter1";
		this.splitter1.Size = new System.Drawing.Size(8, 417);
		this.splitter1.TabIndex = 1;
		this.splitter1.TabStop = false;
		this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel3.Controls.Add(this.c1FlexGrid1);
		this.panel3.Controls.Add(this.ultraLabel1);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel3.Location = new System.Drawing.Point(0, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(208, 417);
		this.panel3.TabIndex = 0;
		appearance21.ForeColor = System.Drawing.Color.White;
		appearance21.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel1.Appearance = appearance21;
		this.ultraLabel1.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel1.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel1.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(206, 30);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "年月/區別";
		this.ultraToolbarsDockArea1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this.ultraToolbarsDockArea1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsDockArea1.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this.ultraToolbarsDockArea1.ForeColor = System.Drawing.SystemColors.ControlText;
		this.ultraToolbarsDockArea1.Location = new System.Drawing.Point(0, 541);
		this.ultraToolbarsDockArea1.Name = "ultraToolbarsDockArea1";
		this.ultraToolbarsDockArea1.Size = new System.Drawing.Size(966, 0);
		this.ultraToolbarsDockArea1.ToolbarsManager = this.ultraToolbarsManager1;
		this.ultraToolbarsDockArea2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this.ultraToolbarsDockArea2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsDockArea2.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this.ultraToolbarsDockArea2.ForeColor = System.Drawing.SystemColors.ControlText;
		this.ultraToolbarsDockArea2.Location = new System.Drawing.Point(0, 79);
		this.ultraToolbarsDockArea2.Name = "ultraToolbarsDockArea2";
		this.ultraToolbarsDockArea2.Size = new System.Drawing.Size(0, 462);
		this.ultraToolbarsDockArea2.ToolbarsManager = this.ultraToolbarsManager1;
		this.ultraToolbarsDockArea3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this.ultraToolbarsDockArea3.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsDockArea3.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this.ultraToolbarsDockArea3.ForeColor = System.Drawing.SystemColors.ControlText;
		this.ultraToolbarsDockArea3.Location = new System.Drawing.Point(966, 79);
		this.ultraToolbarsDockArea3.Name = "ultraToolbarsDockArea3";
		this.ultraToolbarsDockArea3.Size = new System.Drawing.Size(0, 462);
		this.ultraToolbarsDockArea3.ToolbarsManager = this.ultraToolbarsManager1;
		this.ultraToolbarsDockArea5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this.ultraToolbarsDockArea5.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsDockArea5.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this.ultraToolbarsDockArea5.ForeColor = System.Drawing.SystemColors.ControlText;
		this.ultraToolbarsDockArea5.Location = new System.Drawing.Point(0, 79);
		this.ultraToolbarsDockArea5.Name = "ultraToolbarsDockArea5";
		this.ultraToolbarsDockArea5.Size = new System.Drawing.Size(0, 462);
		this.ultraToolbarsDockArea5.ToolbarsManager = this.ultraToolbarsManager1;
		this.ultraToolbarsDockArea6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this.ultraToolbarsDockArea6.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsDockArea6.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this.ultraToolbarsDockArea6.ForeColor = System.Drawing.SystemColors.ControlText;
		this.ultraToolbarsDockArea6.Location = new System.Drawing.Point(966, 79);
		this.ultraToolbarsDockArea6.Name = "ultraToolbarsDockArea6";
		this.ultraToolbarsDockArea6.Size = new System.Drawing.Size(0, 462);
		this.ultraToolbarsDockArea6.ToolbarsManager = this.ultraToolbarsManager1;
		this._panel1_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._panel1_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._panel1_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._panel1_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._panel1_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 0);
		this._panel1_Toolbars_Dock_Area_Bottom.Name = "_panel1_Toolbars_Dock_Area_Bottom";
		this._panel1_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(0, 0);
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.ClientSize = new System.Drawing.Size(966, 541);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.ultraToolbarsDockArea6);
		base.Controls.Add(this.ultraToolbarsDockArea3);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Left);
		base.Controls.Add(this.ultraToolbarsDockArea2);
		base.Controls.Add(this.ultraToolbarsDockArea5);
		base.Controls.Add(this.ultraToolbarsDockArea1);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Bottom);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormConCost";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "公共工程價格資料庫";
		base.Load += new System.EventHandler(FormConCost_Load);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormConCost_FormClosing);
		this.panel1.ResumeLayout(false);
		this.panel1_Fill_Panel.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Grid1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).EndInit();
		this.panel2.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
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

	public FormConCost()
	{
		InitializeComponent();
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		Do_MenuAction(e.Tool.Key);
	}

	private void Do_MenuAction(string KeyID)
	{
		switch (KeyID)
		{
		case "mnuExit":
			Do_Exit();
			break;
		case "mnuW":
			if (sflag == "")
			{
				GetAndBindData(sType: true, "");
			}
			break;
		case "mnuL":
			if (sflag == "")
			{
				GetAndBindData(sType: true, "");
			}
			break;
		case "mnuE":
			if (sflag == "")
			{
				GetAndBindData(sType: true, "");
			}
			break;
		case "mnuM":
			if (sflag == "")
			{
				GetAndBindData(sType: true, "");
			}
			break;
		case "mnuMisc":
			if (sflag == "")
			{
				GetAndBindData(sType: true, "");
			}
			break;
		case "mnuView_ItemAll":
			if ((ultraToolbarsManager1.Tools["mnuView_ItemAll"] as StateButtonTool).Checked)
			{
				GetAndBindData(sType: false, "");
			}
			break;
		case "mnuUpdate":
			if (!DBClass.ChkAuthority(F_UserID, "F002000500130001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F002000500130001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execute_Update();
			}
			break;
		case "mnu_Go":
			Do_ToolBarFind();
			break;
		case "mnuSendtoMrsBase":
			Do_SendtoMrsBase();
			break;
		case "MnuImport":
			Do_SingleToMrsBase();
			break;
		case "MnuDel":
			DeleteCesPrice();
			break;
		case "mnu_GoFilter":
		{
			string sfilter = "";
			if (((TextBoxTool)ultraToolbarsManager1.Tools["Other_QueryText"]).Text != "")
			{
				sfilter = ((TextBoxTool)ultraToolbarsManager1.Tools["Other_QueryText"]).Text;
			}
			if (sflag == "")
			{
				GetAndBindData(sType: true, sfilter);
			}
			break;
		}
		case "mnuExistItems":
			DoExistItemsFilter();
			break;
		case "mnuUpdateCost":
			Do_UpdateCost();
			break;
		}
	}

	private void Do_ToolBarFind()
	{
		if (Grid1.Rows.Count <= 1)
		{
			return;
		}
		int iStart = Grid1.Row + 1;
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
			iStart = Grid1.Row + 1;
		}
		if (sSearchText.Trim() == "")
		{
			return;
		}
		for (int i = iStart; i < Grid1.Rows.Count; i++)
		{
			for (int j = 1; j < Grid1.Cols.Count; j++)
			{
				if (Grid1[i, j] == null || Grid1[i, j].ToString().IndexOf(sSearchText) <= -1)
				{
					continue;
				}
				Grid1.Row = i;
				Grid1.Select();
				Grid1.TopRow = i;
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

	private void Execute_Update()
	{
		FormConCost_Upd FM = new FormConCost_Upd();
		FM.Owner = this;
		if (FM.ShowDialog() == DialogResult.OK)
		{
			GetLeftData();
		}
	}

	private void Do_SendtoMrsBase()
	{
		FormSendtoMrsBase FMSendtoM = new FormSendtoMrsBase();
		FMSendtoM._titleName = c1FlexGrid1[c1FlexGrid1.Row, 1].ToString().Trim();
		FMSendtoM._UserID = F_UserID;
		FMSendtoM._KindName = c1FlexGrid1[c1FlexGrid1.Row, 0].ToString().Trim();
		FMSendtoM.ShowDialog();
		FMSendtoM.Close();
		FMSendtoM.Dispose();
		FMSendtoM = null;
	}

	private void Do_SingleToMrsBase()
	{
		if (sPccesCode.Length == 0)
		{
			MessageBox.Show(this, "請先選擇工項", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		int iSelCount = Grid1.SelectedItems;
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("WinFORM 基本工料");
		MrsBaseA dbMrsBase = new MrsBaseA(F_UserID, aArr);
		dbMrsBase.ps_srckind = "MRS";
		dbMrsBase.ps_projectcode = "";
		for (int i = 1; i < Grid1.Rows.Count; i++)
		{
			if (Grid1.Rows[i].Selected && Grid1.Rows[i].Visible)
			{
				dbMrsBase.ps_pccesCode = Grid1[i, "PccesCode"].ToString();
				dbMrsBase.ps_cName = Grid1[i, "cName"].ToString();
				dbMrsBase.ps_unitName = Grid1[i, "UnitName"].ToString();
				dbMrsBase.ps_surName = Grid1[i, "surName"].ToString();
				dbMrsBase.ps_cost = Grid1[i, "Cost"].ToString();
				dbMrsBase.ps_xNameC = Grid1[i, "location"].ToString();
				int iTransationState = dbMrsBase.InseItem();
				if (iTransationState == -2 && MessageBox.Show(this, "已有【" + Grid1[i, "PccesCode"].ToString() + "】相同工項代碼資料存在，是否覆蓋?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					iTransationState = dbMrsBase.UpdItem();
				}
			}
		}
		MessageBox.Show(this, "新增成功", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Question);
	}

	private void Do_UpdateCost()
	{
		if (sPccesCode.Length == 0)
		{
			MessageBox.Show(this, "請先選擇工項", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		int iSelCount = Grid1.SelectedItems;
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("WinFORM 基本工料");
		MrsBaseA dbMrsBase = new MrsBaseA(F_UserID, aArr);
		dbMrsBase.ps_srckind = "MRS";
		dbMrsBase.ps_projectcode = "";
		for (int i = 1; i < Grid1.Rows.Count; i++)
		{
			if (Grid1.Rows[i].Selected && Grid1.Rows[i].Visible)
			{
				dbMrsBase.ps_pccesCode = Grid1[i, "PccesCode"].ToString();
				dbMrsBase.ps_cost = Grid1[i, "Cost"].ToString();
				dbMrsBase.ps_xNameC = Grid1[i, "location"].ToString();
				int iTransationState = dbMrsBase.UpdItem();
			}
		}
		MessageBox.Show(this, "更新完成", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Question);
	}

	private bool DeleteCesPrice()
	{
		bool bRetV = false;
		try
		{
			ArrayList tmp_AL1 = new ArrayList();
			tmp_AL1.Add("PccAdmin");
			tmp_AL1.Add("營建物價查詢--依選定的項目抓出營建物價資料");
			ModifyDB StdCom = new ModifyDB("", tmp_AL1);
			string ls_Filter = c1FlexGrid1[c1FlexGrid1.Row, 0].ToString().Trim();
			string l_Message = "您確定要刪除【" + ls_Filter + "】此筆嗎?";
			if (MessageBox.Show(this, l_Message, "警示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
			{
				string ls_selectstr = "Delete CesPrice  where years+months+location+KindName = '" + ls_Filter + "' ";
				StdCom.DBDele(ls_selectstr);
				GetLeftData();
				BindToGrid();
				bRetV = true;
			}
		}
		catch
		{
			bRetV = false;
		}
		return bRetV;
	}

	private void Do_Exit()
	{
		string sWarning = "確定要結束 ?";
		if (MessageBox.Show(this, sWarning, "營建物價", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			Close();
		}
	}

	private void DoExistItemsFilter()
	{
		Cursor = Cursors.WaitCursor;
		Grid1.Redraw = false;
		DataView DVSelf = (base.Owner as frmMrsBase).DV1;
		int iCount = 0;
		for (int i = 1; i < Grid1.Rows.Count; i++)
		{
			DVSelf.RowFilter = "PccesCode ='" + Grid1[i, "PccesCode"].ToString().Trim() + "'";
			if (DVSelf.Count > 0)
			{
				Grid1.Rows[i].Visible = true;
				iCount++;
			}
			else
			{
				Grid1.Rows[i].Visible = false;
			}
		}
		Grid1.Redraw = true;
		ultraStatusBar1.Panels[0].Text = "資料筆數:" + iCount.ToString().Trim();
		Cursor = Cursors.Default;
	}

	private void FormConCost_Load(object sender, EventArgs e)
	{
		int deskHeight = (int)((double)Screen.PrimaryScreen.Bounds.Height * 0.8);
		int deskWidth = (int)((double)Screen.PrimaryScreen.Bounds.Width * 0.9);
		base.Width = deskWidth;
		base.Height = deskHeight;
		base.Left = Screen.PrimaryScreen.Bounds.Left + (int)((double)Screen.PrimaryScreen.Bounds.Width * 0.05);
		base.Top = Screen.PrimaryScreen.Bounds.Top + (int)((double)Screen.PrimaryScreen.Bounds.Height * 0.1);
		GetLeftData();
		LoadingScreen();
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("FormConCost", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("FormConCost", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("FormConCost", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("FormConCost", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("FormConCost", "PK_Height"));
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

	private void GetLeftData()
	{
		BIND_FLAG1 = "BINDING";
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add("PccAdmin");
		tmp_AL1.Add("營建物價查詢");
		Cesprice CesPriceCom = new Cesprice(tmp_AL1);
		DataTable dt = CesPriceCom.ListGroup();
		c1FlexGrid1.Cols[0].Visible = false;
		c1FlexGrid1.Rows.Count = dt.Rows.Count;
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string ls_cstr = dt.Rows[i]["cstr"].ToString().Trim();
			string ls_cstr2 = "";
			if (ls_cstr.Trim() != "")
			{
				ls_cstr2 = ls_cstr.Substring(0, 4) + "年" + ls_cstr.Substring(4, 2) + "月  " + ls_cstr.Substring(6, 1);
				ls_cstr2 = ((!(ls_cstr.Substring(6, 1) == "離")) ? (ls_cstr2 + "區 " + ls_cstr.Substring(7)) : (ls_cstr2 + "島 " + ls_cstr.Substring(7)));
			}
			c1FlexGrid1[i, 0] = ls_cstr;
			c1FlexGrid1[i, 1] = ls_cstr2;
		}
		BIND_FLAG1 = "";
		if (dt.Rows.Count > 0)
		{
			c1FlexGrid1.Select(0, 0);
			GetAndBindData(sType: false, "");
		}
	}

	private void c1FlexGrid1_AfterRowColChange(object sender, RangeEventArgs e)
	{
		if ((ultraToolbarsManager1.Tools["mnuView_ItemAll"] as StateButtonTool).Checked)
		{
			string sfilter = "";
			if (((TextBoxTool)ultraToolbarsManager1.Tools["Other_QueryText"]).Text != "")
			{
				sfilter = ((TextBoxTool)ultraToolbarsManager1.Tools["Other_QueryText"]).Text;
			}
			if (sfilter == "")
			{
				GetAndBindData(sType: false, sfilter);
			}
			else
			{
				GetAndBindData(sType: true, sfilter);
			}
		}
		else
		{
			GetAndBindData(sType: true, "");
		}
	}

	private void GetAndBindData(bool sType, string sfilter)
	{
		if (BIND_FLAG1 == "" && c1FlexGrid1.Row >= 0)
		{
			ArrayList tmp_AL1 = new ArrayList();
			tmp_AL1.Add("PccAdmin");
			tmp_AL1.Add("營建物價查詢--依選定的項目抓出營建物價資料");
			Cesprice CesPriceCom = new Cesprice(tmp_AL1);
			string ls_Filter = c1FlexGrid1[c1FlexGrid1.Row, 0].ToString().Trim();
			DataTable dt = CesPriceCom.ListItem("", ls_Filter);
			DV = dt.DefaultView;
			DV.Sort = "PccesCode";
			string ls_GetFilter = "";
			if (sType)
			{
				ls_GetFilter = Get_Filter();
				ls_GetFilter = ((!(ls_GetFilter.Trim() != "1=2")) ? ("CName like '%" + sfilter + "%'") : (ls_GetFilter + " and CName like '%" + sfilter + "%'"));
				DV.RowFilter = ls_GetFilter;
				(ultraToolbarsManager1.Tools["mnuView_ItemAll"] as StateButtonTool).Checked = false;
			}
			else
			{
				sflag = "Binding";
				(ultraToolbarsManager1.Tools["mnuW"] as StateButtonTool).Checked = false;
				(ultraToolbarsManager1.Tools["mnuL"] as StateButtonTool).Checked = false;
				(ultraToolbarsManager1.Tools["mnuE"] as StateButtonTool).Checked = false;
				(ultraToolbarsManager1.Tools["mnuM"] as StateButtonTool).Checked = false;
				(ultraToolbarsManager1.Tools["mnuMisc"] as StateButtonTool).Checked = false;
				(ultraToolbarsManager1.Tools["mnuView_ItemAll"] as StateButtonTool).Checked = true;
				sflag = "";
			}
			BindToGrid();
		}
	}

	private void BindToGrid()
	{
		Grid1.Rows.Count = 1;
		ultraStatusBar1.Panels[1].ProgressBarInfo.Label = "[Formatted]";
		Cursor = Cursors.WaitCursor;
		Grid1.Rows.Count = DV.Count + 1;
		ultraStatusBar1.Panels[1].ProgressBarInfo.Maximum = DV.Count;
		ultraStatusBar1.Panels[1].ProgressBarInfo.Minimum = 0;
		for (int i = 0; i < DV.Count; i++)
		{
			Grid1[i + 1, "PccesCode"] = DV[i]["PccesCode"].ToString().Trim();
			Grid1[i + 1, "CName"] = DV[i]["cName"].ToString().Trim();
			Grid1[i + 1, "UnitName"] = DV[i]["UnitName"].ToString().Trim();
			Grid1[i + 1, "Cost"] = PubTools.KeyDec(DV[i]["EncCost"].ToString());
			Grid1[i + 1, "surName"] = DV[i]["surName"];
			Grid1[i + 1, "location"] = DV[i]["location"];
			if (i % 20 == 0)
			{
				ultraStatusBar1.Panels[1].ProgressBarInfo.Value = i + 1;
				Application.DoEvents();
				Cursor = Cursors.WaitCursor;
			}
		}
		ultraStatusBar1.Panels[0].Text = "資料筆數:" + DV.Count.ToString().Trim();
		Cursor = Cursors.Default;
		ultraStatusBar1.Panels[1].ProgressBarInfo.Value = 0;
		ultraStatusBar1.Panels[1].ProgressBarInfo.Label = "";
	}

	private string Get_Filter()
	{
		string ls_RtnVal = "";
		string ls_kind1 = "";
		if ((ultraToolbarsManager1.Tools["mnuW"] as StateButtonTool).Checked)
		{
			ls_kind1 += " or ( not PccesCode like 'L%' and not PccesCode like 'E%' and not PccesCode like 'M%' and not PccesCode like 'W%' )";
		}
		if ((ultraToolbarsManager1.Tools["mnuL"] as StateButtonTool).Checked)
		{
			ls_kind1 += " or PccesCode like 'L%' ";
		}
		if ((ultraToolbarsManager1.Tools["mnuE"] as StateButtonTool).Checked)
		{
			ls_kind1 += " or PccesCode like 'E%' ";
		}
		if ((ultraToolbarsManager1.Tools["mnuM"] as StateButtonTool).Checked)
		{
			ls_kind1 += " or PccesCode like 'M%' ";
		}
		if ((ultraToolbarsManager1.Tools["mnuMisc"] as StateButtonTool).Checked)
		{
			ls_kind1 += " or PccesCode like 'W%' ";
		}
		if (ls_kind1.Length > 0)
		{
			return " ( " + ls_kind1.Substring(3) + " ) ";
		}
		return " 1=2 ";
	}

	private void ultraToolbarsManager1_ToolKeyPress(object sender, ToolKeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && e.Tool.Key == "mnu_Cbo1")
		{
			Do_ToolBarFind();
		}
	}

	private void FormConCost_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("FormConCost", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("FormConCost", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("FormConCost", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("FormConCost", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("FormConCost", "WindowState", base.WindowState.ToString());
	}

	private void Grid1_Click(object sender, EventArgs e)
	{
		if (Grid1.Row >= 0)
		{
			sPccesCode = Grid1[Grid1.Row, "PccesCode"].ToString().Trim();
			sCName = Grid1[Grid1.Row, "CName"].ToString().Trim();
			sCost = Grid1[Grid1.Row, "Cost"].ToString().Trim();
			sUnitName = Grid1[Grid1.Row, "UnitName"].ToString().Trim();
			sSurName = Grid1[Grid1.Row, "surName"].ToString().Trim();
		}
	}

	private void Grid1_AfterSelChange(object sender, RangeEventArgs e)
	{
		if (Grid1.Row >= 0)
		{
			ultraToolbarsManager1.Tools["MnuImport"].SharedProps.Visible = true;
			ultraToolbarsManager1.Tools["MnuDel"].SharedProps.Visible = false;
		}
	}

	private void c1FlexGrid1_AfterSelChange(object sender, RangeEventArgs e)
	{
		if (c1FlexGrid1.Row >= 0)
		{
			ultraToolbarsManager1.Tools["MnuImport"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["MnuDel"].SharedProps.Visible = true;
		}
	}

	private void c1FlexGrid1_MouseDown(object sender, MouseEventArgs e)
	{
		if (Grid1.Row >= 0)
		{
			ultraToolbarsManager1.Tools["MnuImport"].SharedProps.Visible = true;
			ultraToolbarsManager1.Tools["MnuDel"].SharedProps.Visible = false;
		}
		if (c1FlexGrid1.Row >= 0)
		{
			ultraToolbarsManager1.Tools["MnuImport"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["MnuDel"].SharedProps.Visible = true;
		}
	}

	private void c1FlexGrid1_MouseLeave(object sender, EventArgs e)
	{
		ultraToolbarsManager1.Tools["MnuDel"].SharedProps.Visible = false;
	}

	private void Grid1_MouseLeave(object sender, EventArgs e)
	{
		ultraToolbarsManager1.Tools["MnuImport"].SharedProps.Visible = false;
	}

	private void Grid1_MouseDown(object sender, MouseEventArgs e)
	{
		if (Grid1.Row >= 0)
		{
			ultraToolbarsManager1.Tools["MnuImport"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["MnuDel"].SharedProps.Visible = true;
		}
		if (c1FlexGrid1.Row >= 0)
		{
			ultraToolbarsManager1.Tools["MnuImport"].SharedProps.Visible = true;
			ultraToolbarsManager1.Tools["MnuDel"].SharedProps.Visible = false;
		}
	}
}
