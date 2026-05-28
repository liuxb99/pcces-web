using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using C1.Win.C1FlexGrid;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.Budget.ItemNoset;

public class FormBDGT_ItemSetMaintain : Form
{
	private IContainer components;

	private UltraToolbarsManager toolbarsManager;

	private Panel panel9;

	private GroupBox groupBox5;

	private Panel panel1;

	private Splitter splitter1;

	private Panel panel2;

	private Panel panel3;

	private UltraLabel lbItemNoSetTitle;

	private UltraButton btnFinish;

	private Panel panel4;

	private UltraButton btnNewItemNoSet;

	private Panel panel6;

	public GridMrsBase gridItemNo;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Bottom;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Right;

	private UltraToolbarsDockArea ultraToolbarsDockArea1;

	private UltraToolbarsDockArea ultraToolbarsDockArea2;

	private UltraToolbarsDockArea ultraToolbarsDockArea3;

	private Panel panel7;

	private ListBox ItemNoSetList;

	private UltraButton btnDeleteItemNoSet;

	private UltraLabel lbItemNoTitle;

	private Panel panel5;

	private UltraButton btnDeleteItemNo;

	private UltraButton btnMoveDown;

	private UltraButton btnMoveUp;

	private ListBox listBox2;

	private UltraButton btnInsertItemNo;

	private SaveFileDialog saveItemNoSetXMLFileDialog;

	private UltraButton btnImportItemNoSet;

	private OpenFileDialog openItemNoSetXMLFileDialog;

	private FormStatus FORM_STATUS = FormStatus.Iinitial;

	private string ItemNoSetSelected = string.Empty;

	private DataTable dtItemNo = new DataTable();

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.ItemNoset.FormBDGT_ItemSetMaintain));
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUp");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDown");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopupRight");
		Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUp");
		Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDown");
		Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool2 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopupLeft");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelItemName");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExport");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool3 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopupRight");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUp");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool10 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDown");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool11 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuInsNew");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool12 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool13 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelItemName");
		Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool14 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuInsNew");
		Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool15 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExport");
		Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
		this.panel9 = new System.Windows.Forms.Panel();
		this.btnFinish = new Infragistics.Win.Misc.UltraButton();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel7 = new System.Windows.Forms.Panel();
		this.listBox2 = new System.Windows.Forms.ListBox();
		this.ItemNoSetList = new System.Windows.Forms.ListBox();
		this.ultraToolbarsDockArea2 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.ultraToolbarsDockArea3 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.panel4 = new System.Windows.Forms.Panel();
		this.btnImportItemNoSet = new Infragistics.Win.Misc.UltraButton();
		this.btnDeleteItemNoSet = new Infragistics.Win.Misc.UltraButton();
		this.btnNewItemNoSet = new Infragistics.Win.Misc.UltraButton();
		this.lbItemNoSetTitle = new Infragistics.Win.Misc.UltraLabel();
		this.ultraToolbarsDockArea1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.splitter1 = new System.Windows.Forms.Splitter();
		this.panel2 = new System.Windows.Forms.Panel();
		this._FormSys_B_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.toolbarsManager = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this.gridItemNo = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this._FormSys_B_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.panel6 = new System.Windows.Forms.Panel();
		this.panel5 = new System.Windows.Forms.Panel();
		this.btnInsertItemNo = new Infragistics.Win.Misc.UltraButton();
		this.btnMoveUp = new Infragistics.Win.Misc.UltraButton();
		this.btnMoveDown = new Infragistics.Win.Misc.UltraButton();
		this.btnDeleteItemNo = new Infragistics.Win.Misc.UltraButton();
		this.panel3 = new System.Windows.Forms.Panel();
		this.lbItemNoTitle = new Infragistics.Win.Misc.UltraLabel();
		this._FormSys_B_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.saveItemNoSetXMLFileDialog = new System.Windows.Forms.SaveFileDialog();
		this.openItemNoSetXMLFileDialog = new System.Windows.Forms.OpenFileDialog();
		this.panel9.SuspendLayout();
		this.panel1.SuspendLayout();
		this.panel7.SuspendLayout();
		this.panel4.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.toolbarsManager).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridItemNo).BeginInit();
		this.panel6.SuspendLayout();
		this.panel5.SuspendLayout();
		this.panel3.SuspendLayout();
		base.SuspendLayout();
		this.panel9.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel9.Controls.Add(this.btnFinish);
		this.panel9.Controls.Add(this.groupBox5);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel9.Location = new System.Drawing.Point(0, 373);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(492, 40);
		this.panel9.TabIndex = 22;
		this.btnFinish.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance1.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnFinish.Appearance = appearance1;
		this.btnFinish.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnFinish.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnFinish.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnFinish.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnFinish.ImageSize = new System.Drawing.Size(20, 20);
		this.btnFinish.Location = new System.Drawing.Point(397, 9);
		this.btnFinish.Name = "btnFinish";
		this.btnFinish.ShowFocusRect = false;
		this.btnFinish.ShowOutline = false;
		this.btnFinish.Size = new System.Drawing.Size(90, 28);
		this.btnFinish.SupportThemes = false;
		this.btnFinish.TabIndex = 6;
		this.btnFinish.Text = "結  束";
		this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox5.Location = new System.Drawing.Point(0, 0);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(492, 8);
		this.groupBox5.TabIndex = 3;
		this.groupBox5.TabStop = false;
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.panel7);
		this.panel1.Controls.Add(this.ultraToolbarsDockArea2);
		this.panel1.Controls.Add(this.ultraToolbarsDockArea3);
		this.panel1.Controls.Add(this.panel4);
		this.panel1.Controls.Add(this.lbItemNoSetTitle);
		this.panel1.Controls.Add(this.ultraToolbarsDockArea1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(200, 373);
		this.panel1.TabIndex = 23;
		this.panel7.Controls.Add(this.listBox2);
		this.panel7.Controls.Add(this.ItemNoSetList);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel7.Location = new System.Drawing.Point(0, 28);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(198, 307);
		this.panel7.TabIndex = 18;
		this.listBox2.ItemHeight = 15;
		this.listBox2.Location = new System.Drawing.Point(56, 184);
		this.listBox2.Name = "listBox2";
		this.listBox2.Size = new System.Drawing.Size(128, 94);
		this.listBox2.TabIndex = 1;
		this.listBox2.Visible = false;
		this.toolbarsManager.SetContextMenuUltra(this.ItemNoSetList, "PopupLeft");
		this.ItemNoSetList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ItemNoSetList.ItemHeight = 15;
		this.ItemNoSetList.Items.AddRange(new object[3] { "1", "2", "3" });
		this.ItemNoSetList.Location = new System.Drawing.Point(0, 0);
		this.ItemNoSetList.Name = "ItemNoSetList";
		this.ItemNoSetList.Size = new System.Drawing.Size(198, 304);
		this.ItemNoSetList.TabIndex = 0;
		this.ItemNoSetList.SelectedIndexChanged += new System.EventHandler(ItemNoSetList_SelectedIndexChanged);
		this.ultraToolbarsDockArea2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this.ultraToolbarsDockArea2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsDockArea2.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this.ultraToolbarsDockArea2.ForeColor = System.Drawing.SystemColors.ControlText;
		this.ultraToolbarsDockArea2.Location = new System.Drawing.Point(0, 28);
		this.ultraToolbarsDockArea2.Name = "ultraToolbarsDockArea2";
		this.ultraToolbarsDockArea2.Size = new System.Drawing.Size(0, 307);
		this.ultraToolbarsDockArea3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this.ultraToolbarsDockArea3.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsDockArea3.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this.ultraToolbarsDockArea3.ForeColor = System.Drawing.SystemColors.ControlText;
		this.ultraToolbarsDockArea3.Location = new System.Drawing.Point(198, 28);
		this.ultraToolbarsDockArea3.Name = "ultraToolbarsDockArea3";
		this.ultraToolbarsDockArea3.Size = new System.Drawing.Size(0, 307);
		this.panel4.Controls.Add(this.btnImportItemNoSet);
		this.panel4.Controls.Add(this.btnDeleteItemNoSet);
		this.panel4.Controls.Add(this.btnNewItemNoSet);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(0, 335);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(198, 36);
		this.panel4.TabIndex = 11;
		appearance19.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance19.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance19.Image = resources.GetObject("appearance19.Image");
		appearance19.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnImportItemNoSet.Appearance = appearance19;
		this.btnImportItemNoSet.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnImportItemNoSet.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnImportItemNoSet.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnImportItemNoSet.ImageSize = new System.Drawing.Size(20, 20);
		this.btnImportItemNoSet.ImageTransparentColor = System.Drawing.Color.White;
		this.btnImportItemNoSet.Location = new System.Drawing.Point(132, 4);
		this.btnImportItemNoSet.Name = "btnImportItemNoSet";
		this.btnImportItemNoSet.ShowFocusRect = false;
		this.btnImportItemNoSet.ShowOutline = false;
		this.btnImportItemNoSet.Size = new System.Drawing.Size(64, 28);
		this.btnImportItemNoSet.SupportThemes = false;
		this.btnImportItemNoSet.TabIndex = 9;
		this.btnImportItemNoSet.Text = "匯入";
		this.btnImportItemNoSet.Click += new System.EventHandler(btnImportItemNoSet_Click);
		appearance20.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance20.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance20.Image = resources.GetObject("appearance20.Image");
		appearance20.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnDeleteItemNoSet.Appearance = appearance20;
		this.btnDeleteItemNoSet.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnDeleteItemNoSet.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnDeleteItemNoSet.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnDeleteItemNoSet.ImageSize = new System.Drawing.Size(20, 20);
		this.btnDeleteItemNoSet.ImageTransparentColor = System.Drawing.Color.White;
		this.btnDeleteItemNoSet.Location = new System.Drawing.Point(68, 4);
		this.btnDeleteItemNoSet.Name = "btnDeleteItemNoSet";
		this.btnDeleteItemNoSet.ShowFocusRect = false;
		this.btnDeleteItemNoSet.ShowOutline = false;
		this.btnDeleteItemNoSet.Size = new System.Drawing.Size(64, 28);
		this.btnDeleteItemNoSet.SupportThemes = false;
		this.btnDeleteItemNoSet.TabIndex = 8;
		this.btnDeleteItemNoSet.Text = "刪除";
		this.btnDeleteItemNoSet.Click += new System.EventHandler(btnDeleteItemNoSet_Click);
		appearance21.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance21.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance21.Image = resources.GetObject("appearance21.Image");
		appearance21.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnNewItemNoSet.Appearance = appearance21;
		this.btnNewItemNoSet.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnNewItemNoSet.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnNewItemNoSet.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnNewItemNoSet.ImageSize = new System.Drawing.Size(20, 20);
		this.btnNewItemNoSet.ImageTransparentColor = System.Drawing.Color.White;
		this.btnNewItemNoSet.Location = new System.Drawing.Point(4, 4);
		this.btnNewItemNoSet.Name = "btnNewItemNoSet";
		this.btnNewItemNoSet.ShowFocusRect = false;
		this.btnNewItemNoSet.ShowOutline = false;
		this.btnNewItemNoSet.Size = new System.Drawing.Size(64, 28);
		this.btnNewItemNoSet.SupportThemes = false;
		this.btnNewItemNoSet.TabIndex = 7;
		this.btnNewItemNoSet.Text = "新增";
		this.btnNewItemNoSet.Click += new System.EventHandler(btnNewItemNoSet_Click);
		appearance22.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.lbItemNoSetTitle.Appearance = appearance22;
		this.lbItemNoSetTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.lbItemNoSetTitle.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbItemNoSetTitle.Location = new System.Drawing.Point(0, 0);
		this.lbItemNoSetTitle.Name = "lbItemNoSetTitle";
		this.lbItemNoSetTitle.Size = new System.Drawing.Size(198, 28);
		this.lbItemNoSetTitle.TabIndex = 1;
		this.lbItemNoSetTitle.Text = "編號樣式:";
		this.ultraToolbarsDockArea1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this.ultraToolbarsDockArea1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsDockArea1.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this.ultraToolbarsDockArea1.ForeColor = System.Drawing.SystemColors.ControlText;
		this.ultraToolbarsDockArea1.Location = new System.Drawing.Point(0, 371);
		this.ultraToolbarsDockArea1.Name = "ultraToolbarsDockArea1";
		this.ultraToolbarsDockArea1.Size = new System.Drawing.Size(198, 0);
		this.splitter1.Location = new System.Drawing.Point(200, 0);
		this.splitter1.Name = "splitter1";
		this.splitter1.Size = new System.Drawing.Size(5, 373);
		this.splitter1.TabIndex = 24;
		this.splitter1.TabStop = false;
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel2.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Bottom);
		this.panel2.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Left);
		this.panel2.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Right);
		this.panel2.Controls.Add(this.panel6);
		this.panel2.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Top);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(205, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(287, 373);
		this.panel2.TabIndex = 25;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 371);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Name = "_FormSys_B_Toolbars_Dock_Area_Bottom";
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(285, 0);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.toolbarsManager;
		appearance23.FontData.Name = "Arial";
		appearance23.FontData.SizeInPoints = 9f;
		this.toolbarsManager.Appearance = appearance23;
		appearance24.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance24.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.toolbarsManager.DockAreaAppearance = appearance24;
		this.toolbarsManager.DockWithinContainer = this;
		this.toolbarsManager.ImageTransparentColor = System.Drawing.Color.White;
		this.toolbarsManager.LockToolbars = true;
		appearance25.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance25.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance25.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.toolbarsManager.MenuSettings.HotTrackAppearance = appearance25;
		appearance26.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance26.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.toolbarsManager.MenuSettings.IconAreaAppearance = appearance26;
		appearance27.BackColor = System.Drawing.Color.White;
		appearance27.BackColor2 = System.Drawing.Color.White;
		this.toolbarsManager.MenuSettings.ToolAppearance = appearance27;
		this.toolbarsManager.ShowFullMenusDelay = 500;
		this.toolbarsManager.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
		ultraToolbar1.Settings.AllowDockTop = Infragistics.Win.DefaultableBoolean.True;
		ultraToolbar1.Text = "Tool1";
		buttonTool3.InstanceProps.IsFirstInGroup = true;
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { buttonTool1, buttonTool2, buttonTool3, popupMenuTool1 });
		ultraToolbar1.Visible = false;
		this.toolbarsManager.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[1] { ultraToolbar1 });
		appearance28.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance28.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.toolbarsManager.ToolbarSettings.Appearance = appearance28;
		appearance29.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance29.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance29.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.toolbarsManager.ToolbarSettings.HotTrackAppearance = appearance29;
		appearance30.Image = resources.GetObject("appearance14.Image");
		buttonTool4.SharedProps.AppearancesSmall.Appearance = appearance30;
		buttonTool4.SharedProps.Caption = "刪除";
		buttonTool4.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		appearance31.Image = resources.GetObject("appearance15.Image");
		buttonTool5.SharedProps.AppearancesSmall.Appearance = appearance31;
		buttonTool5.SharedProps.Caption = "上移";
		buttonTool5.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		appearance32.Image = resources.GetObject("appearance16.Image");
		buttonTool6.SharedProps.AppearancesSmall.Appearance = appearance32;
		buttonTool6.SharedProps.Caption = "下移";
		buttonTool6.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		popupMenuTool2.SharedProps.Caption = "左邊選單";
		popupMenuTool2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool7, buttonTool8 });
		popupMenuTool3.SharedProps.Caption = "右邊選單";
		buttonTool11.InstanceProps.IsFirstInGroup = true;
		buttonTool12.InstanceProps.IsFirstInGroup = true;
		popupMenuTool3.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { buttonTool9, buttonTool10, buttonTool11, buttonTool12 });
		appearance33.Image = resources.GetObject("appearance17.Image");
		buttonTool13.SharedProps.AppearancesSmall.Appearance = appearance33;
		buttonTool13.SharedProps.Caption = "刪除編號名稱";
		appearance34.Image = resources.GetObject("appearance18.Image");
		buttonTool14.SharedProps.AppearancesSmall.Appearance = appearance34;
		buttonTool14.SharedProps.Caption = "插入";
		buttonTool15.SharedProps.Caption = "匯出...";
		this.toolbarsManager.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[8] { buttonTool4, buttonTool5, buttonTool6, popupMenuTool2, popupMenuTool3, buttonTool13, buttonTool14, buttonTool15 });
		this.toolbarsManager.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(toolbarsManager_ToolClick);
		this.gridItemNo._ExcelFileName = "";
		this.gridItemNo._ExcelSheeName = "";
		this.gridItemNo._IsOpenExcelAfterExport = false;
		this.gridItemNo.AllowAddNew = true;
		this.gridItemNo.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
		this.gridItemNo.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
		this.gridItemNo.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridItemNo.ColumnInfo = resources.GetString("gridItemNo.ColumnInfo");
		this.toolbarsManager.SetContextMenuUltra(this.gridItemNo, "PopupRight");
		this.gridItemNo.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridItemNo.ExtendLastCol = true;
		this.gridItemNo.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridItemNo.ForeColor = System.Drawing.Color.Black;
		this.gridItemNo.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.gridItemNo.IsProcessUndo = false;
		this.gridItemNo.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.gridItemNo.Location = new System.Drawing.Point(0, 28);
		this.gridItemNo.Name = "gridItemNo";
		this.gridItemNo.Rows.Count = 31;
		this.gridItemNo.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridItemNo.ShowCursor = true;
		this.gridItemNo.ShowToolTipOnNarrowColumn = true;
		this.gridItemNo.Size = new System.Drawing.Size(285, 307);
		this.gridItemNo.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridItemNo.Styles"));
		this.gridItemNo.TabIndex = 9;
		this.gridItemNo.UndoMax = 10;
		this.gridItemNo.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridItemNo_AfterEdit);
		this.gridItemNo.MouseDown += new System.Windows.Forms.MouseEventHandler(gridItemNo_MouseDown);
		this._FormSys_B_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormSys_B_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 0);
		this._FormSys_B_Toolbars_Dock_Area_Left.Name = "_FormSys_B_Toolbars_Dock_Area_Left";
		this._FormSys_B_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 371);
		this._FormSys_B_Toolbars_Dock_Area_Left.ToolbarsManager = this.toolbarsManager;
		this._FormSys_B_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormSys_B_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(285, 0);
		this._FormSys_B_Toolbars_Dock_Area_Right.Name = "_FormSys_B_Toolbars_Dock_Area_Right";
		this._FormSys_B_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 371);
		this._FormSys_B_Toolbars_Dock_Area_Right.ToolbarsManager = this.toolbarsManager;
		this.panel6.Controls.Add(this.gridItemNo);
		this.panel6.Controls.Add(this.panel5);
		this.panel6.Controls.Add(this.panel3);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel6.Location = new System.Drawing.Point(0, 0);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(285, 371);
		this.panel6.TabIndex = 2;
		this.panel5.Controls.Add(this.btnInsertItemNo);
		this.panel5.Controls.Add(this.btnMoveUp);
		this.panel5.Controls.Add(this.btnMoveDown);
		this.panel5.Controls.Add(this.btnDeleteItemNo);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel5.Location = new System.Drawing.Point(0, 335);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(285, 36);
		this.panel5.TabIndex = 12;
		appearance35.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance35.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance35.Image = resources.GetObject("appearance4.Image");
		appearance35.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnInsertItemNo.Appearance = appearance35;
		this.btnInsertItemNo.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnInsertItemNo.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnInsertItemNo.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnInsertItemNo.ImageSize = new System.Drawing.Size(20, 20);
		this.btnInsertItemNo.ImageTransparentColor = System.Drawing.Color.White;
		this.btnInsertItemNo.Location = new System.Drawing.Point(150, 4);
		this.btnInsertItemNo.Name = "btnInsertItemNo";
		this.btnInsertItemNo.ShowFocusRect = false;
		this.btnInsertItemNo.ShowOutline = false;
		this.btnInsertItemNo.Size = new System.Drawing.Size(64, 28);
		this.btnInsertItemNo.SupportThemes = false;
		this.btnInsertItemNo.TabIndex = 11;
		this.btnInsertItemNo.Text = "插入";
		this.btnInsertItemNo.Click += new System.EventHandler(btnInsertItemNo_Click);
		appearance36.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance36.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance36.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance36.Image = resources.GetObject("appearance5.Image");
		appearance36.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnMoveUp.Appearance = appearance36;
		this.btnMoveUp.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnMoveUp.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnMoveUp.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnMoveUp.ImageSize = new System.Drawing.Size(20, 20);
		this.btnMoveUp.ImageTransparentColor = System.Drawing.Color.White;
		this.btnMoveUp.Location = new System.Drawing.Point(8, 4);
		this.btnMoveUp.Name = "btnMoveUp";
		this.btnMoveUp.ShowFocusRect = false;
		this.btnMoveUp.ShowOutline = false;
		this.btnMoveUp.Size = new System.Drawing.Size(68, 28);
		this.btnMoveUp.SupportThemes = false;
		this.btnMoveUp.TabIndex = 10;
		this.btnMoveUp.Text = "上移";
		this.btnMoveUp.Click += new System.EventHandler(btnMoveUp_Click);
		appearance37.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance37.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance37.Image = resources.GetObject("appearance6.Image");
		appearance37.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnMoveDown.Appearance = appearance37;
		this.btnMoveDown.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnMoveDown.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnMoveDown.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnMoveDown.ImageSize = new System.Drawing.Size(20, 20);
		this.btnMoveDown.ImageTransparentColor = System.Drawing.Color.White;
		this.btnMoveDown.Location = new System.Drawing.Point(76, 4);
		this.btnMoveDown.Name = "btnMoveDown";
		this.btnMoveDown.ShowFocusRect = false;
		this.btnMoveDown.ShowOutline = false;
		this.btnMoveDown.Size = new System.Drawing.Size(68, 28);
		this.btnMoveDown.SupportThemes = false;
		this.btnMoveDown.TabIndex = 9;
		this.btnMoveDown.Text = "下移";
		this.btnMoveDown.Click += new System.EventHandler(btnMoveDown_Click);
		appearance38.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance38.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance38.Image = resources.GetObject("appearance7.Image");
		appearance38.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnDeleteItemNo.Appearance = appearance38;
		this.btnDeleteItemNo.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnDeleteItemNo.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnDeleteItemNo.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnDeleteItemNo.ImageSize = new System.Drawing.Size(20, 20);
		this.btnDeleteItemNo.ImageTransparentColor = System.Drawing.Color.White;
		this.btnDeleteItemNo.Location = new System.Drawing.Point(214, 4);
		this.btnDeleteItemNo.Name = "btnDeleteItemNo";
		this.btnDeleteItemNo.ShowFocusRect = false;
		this.btnDeleteItemNo.ShowOutline = false;
		this.btnDeleteItemNo.Size = new System.Drawing.Size(64, 28);
		this.btnDeleteItemNo.SupportThemes = false;
		this.btnDeleteItemNo.TabIndex = 8;
		this.btnDeleteItemNo.Text = "刪除";
		this.btnDeleteItemNo.Click += new System.EventHandler(btnDeleteItemNo_Click);
		this.panel3.Controls.Add(this.lbItemNoTitle);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel3.Location = new System.Drawing.Point(0, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(285, 28);
		this.panel3.TabIndex = 0;
		appearance39.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.lbItemNoTitle.Appearance = appearance39;
		this.lbItemNoTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.lbItemNoTitle.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbItemNoTitle.Location = new System.Drawing.Point(0, 0);
		this.lbItemNoTitle.Name = "lbItemNoTitle";
		this.lbItemNoTitle.Size = new System.Drawing.Size(285, 28);
		this.lbItemNoTitle.TabIndex = 2;
		this.lbItemNoTitle.Text = "編號樣式內容:";
		this._FormSys_B_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormSys_B_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormSys_B_Toolbars_Dock_Area_Top.Name = "_FormSys_B_Toolbars_Dock_Area_Top";
		this._FormSys_B_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(285, 0);
		this._FormSys_B_Toolbars_Dock_Area_Top.ToolbarsManager = this.toolbarsManager;
		this.saveItemNoSetXMLFileDialog.Filter = "XML files (*.xml)|*.xml";
		this.saveItemNoSetXMLFileDialog.RestoreDirectory = true;
		this.openItemNoSetXMLFileDialog.Filter = "XML files (*.xml)|*.xml";
		this.openItemNoSetXMLFileDialog.RestoreDirectory = true;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.ClientSize = new System.Drawing.Size(492, 413);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.splitter1);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel9);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormBDGT_ItemSetMaintain";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "自訂項次編號";
		base.Load += new System.EventHandler(FormBDGT_ItemSetMaintain_Load);
		base.Activated += new System.EventHandler(FormBDGT_ItemSetMaintain_Activated);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormBDGT_ItemSetMaintain_FormClosing);
		this.panel9.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.toolbarsManager).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridItemNo).EndInit();
		this.panel6.ResumeLayout(false);
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

	public FormBDGT_ItemSetMaintain()
	{
		InitializeComponent();
	}

	private void FormBDGT_ItemSetMaintain_Load(object sender, EventArgs e)
	{
		CorrectRatio();
		BindToItemNoSetList();
		FORM_STATUS = FormStatus.Active;
	}

	private void BindToItemNoSetList()
	{
		ItemNoSetList.Items.Clear();
		listBox2.Items.Clear();
		DBClass DBCLS = new DBClass();
		DataTable dtItemNoSet = DBCLS.GetItemNameForCombo();
		DBCLS = null;
		for (int i = 0; i < dtItemNoSet.Rows.Count; i++)
		{
			ItemNoSetList.Items.Add(dtItemNoSet.Rows[i]["Sample"]);
			listBox2.Items.Add(dtItemNoSet.Rows[i]["Kind"]);
		}
		if (ItemNoSetList.Items.Count > 0)
		{
			ItemNoSetList.SelectedIndex = 0;
			listBox2.SelectedIndex = 0;
			ItemNoSetSelected = listBox2.Items[listBox2.SelectedIndex].ToString();
		}
	}

	private void BindToGrid()
	{
		gridItemNo.Rows.Count = dtItemNo.Rows.Count + 2;
		for (int i = 1; i < gridItemNo.Rows.Count; i++)
		{
			if (i <= dtItemNo.Rows.Count)
			{
				gridItemNo[i, "RowIndicator"] = i;
				gridItemNo[i, "cString"] = dtItemNo.Rows[i - 1]["cString"].ToString().Trim();
			}
			else
			{
				gridItemNo[i, "RowIndicator"] = "";
				gridItemNo[i, "cString"] = "";
			}
		}
	}

	private void ReBindMidGrid()
	{
		if (ItemNoSetList.Items.Count > 0)
		{
			DBClass DBCLS = new DBClass();
			dtItemNo = DBCLS.GetItemNoList(listBox2.Items[ItemNoSetList.SelectedIndex].ToString().Trim());
			DBCLS = null;
			BindToGrid();
		}
	}

	private void gridItemNo_AfterEdit(object sender, RowColEventArgs e)
	{
		Reorder();
	}

	private void Reorder()
	{
		for (int i = 1; i < gridItemNo.Rows.Count - 1; i++)
		{
			gridItemNo[i, "RowIndicator"] = i;
		}
	}

	private void btnNewItemNoSet_Click(object sender, EventArgs e)
	{
		FormBudgetItemNo_New FM_BDGT_ITM_NW = new FormBudgetItemNo_New();
		if (FM_BDGT_ITM_NW.ShowDialog(this) == DialogResult.OK)
		{
			BindToItemNoSetList();
		}
		FM_BDGT_ITM_NW.Close();
		FM_BDGT_ITM_NW.Dispose();
		FM_BDGT_ITM_NW = null;
	}

	private void toolbarsManager_ToolClick(object sender, ToolClickEventArgs e)
	{
		switch (e.Tool.Key)
		{
		case "mnuDelete":
			Do_Delete();
			break;
		case "mnuUp":
			btnMoveUp_Click(this, EventArgs.Empty);
			break;
		case "mnuDown":
			btnMoveDown_Click(this, EventArgs.Empty);
			break;
		case "mnuSave":
			Do_SaveData();
			break;
		case "mnuDelItemName":
			Do_DeleteItemNo();
			break;
		case "mnuInsNew":
			btnInsertItemNo_Click(this, EventArgs.Empty);
			break;
		case "mnuExport":
			Do_Export();
			break;
		}
	}

	private void Do_SaveData()
	{
		DataTable DT_Num = new DataTable();
		DT_Num.Columns.Add("kind", Type.GetType("System.String"));
		DT_Num.Columns.Add("cString", Type.GetType("System.String"));
		DT_Num.Columns.Add("sno", Type.GetType("System.Int64"));
		for (int i = 1; i < gridItemNo.Rows.Count; i++)
		{
			if (gridItemNo.Rows[i]["RowIndicator"] != null && !(gridItemNo.Rows[i]["RowIndicator"].ToString() == ""))
			{
				DataRow DR = DT_Num.NewRow();
				DR["kind"] = ItemNoSetSelected.Trim();
				DR["cString"] = gridItemNo.Rows[i]["cString"].ToString().Trim();
				DR["sno"] = (Convert.ToInt32(gridItemNo.Rows[i]["RowIndicator"]) + 200000).ToString();
				DT_Num.Rows.Add(DR);
			}
		}
		DBClass DBCLS = new DBClass();
		DBCLS.SaveItemNo(DT_Num, ItemNoSetSelected.Trim());
		DBCLS = null;
	}

	private void Do_Delete()
	{
		int seletedItemNumber = gridItemNo.SelectedItems;
		if (seletedItemNumber > 0 && MessageBox.Show(this, "確定要刪除選取的 " + seletedItemNumber + " 筆項目？", "刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			for (int i = gridItemNo.Rows.Count - 1; i >= 1; i--)
			{
				if (gridItemNo.Rows[i].Selected)
				{
					gridItemNo.Rows.Remove(i);
				}
			}
		}
		Reorder();
	}

	private void Do_DeleteItemNo()
	{
		if (ItemNoSetSelected.Contains("ArchItemNo"))
		{
			MessageBox.Show(this, "系統預設項，不可刪除。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else if (MessageBox.Show(this, "確定要刪除選取的筆項目？\n這個動作會將對應的編號序列字串一併刪除。", "刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			DBClass DBCLS = new DBClass();
			int iResult = DBCLS.DeleteItemName(ItemNoSetSelected.Trim());
			DBCLS = null;
			BindToItemNoSetList();
		}
	}

	private void Do_Export()
	{
		DataSet ds = new DataSet();
		string XmlFileName = string.Empty;
		if (saveItemNoSetXMLFileDialog.ShowDialog() == DialogResult.OK)
		{
			XmlFileName = saveItemNoSetXMLFileDialog.FileName;
			DBClass DBCLS = new DBClass();
			DataTable dt = DBCLS.GetItemNoList(ItemNoSetSelected.Trim());
			ds.Tables.Add(dt);
			DataTable mytable = new DataTable();
			DataColumn myColumn = new DataColumn();
			DataRow myRow = mytable.NewRow();
			myColumn.ColumnName = "Name";
			myColumn.DataType = Type.GetType("System.String");
			mytable.Columns.Add(myColumn);
			myRow = mytable.NewRow();
			myRow["Name"] = "序號";
			mytable.Rows.Add(myRow);
			ds.Tables.Add(mytable);
			ds.WriteXml(XmlFileName);
		}
	}

	private void ItemNoSetList_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (FORM_STATUS != FormStatus.Delete && ItemNoSetSelected.Trim() != string.Empty)
		{
			Do_SaveData();
		}
		ItemNoSetSelected = listBox2.Items[ItemNoSetList.SelectedIndex].ToString().Trim();
		ReBindMidGrid();
	}

	private void btnDeleteItemNo_Click(object sender, EventArgs e)
	{
		Do_Delete();
	}

	private void btnDeleteItemNoSet_Click(object sender, EventArgs e)
	{
		FORM_STATUS = FormStatus.Delete;
		Do_DeleteItemNo();
		FORM_STATUS = FormStatus.Normal;
	}

	private void btnMoveUp_Click(object sender, EventArgs e)
	{
		try
		{
			ArrayList SelItems = new ArrayList();
			int iIdx = -1;
			for (int i = 1; i < gridItemNo.Rows.Count; i++)
			{
				if (gridItemNo.Rows[i].Selected)
				{
					SelItems.Add(gridItemNo[i, "RowIndicator"]);
				}
			}
			for (int i = 0; i < SelItems.Count; i++)
			{
				iIdx = gridItemNo.FindRow(SelItems[i], 1, gridItemNo.Cols["RowIndicator"].SafeIndex, wrap: false);
				if (iIdx == 1)
				{
					break;
				}
				if (iIdx > -1)
				{
					gridItemNo.Rows[iIdx].Move(iIdx - 1);
				}
			}
			for (int i = 0; i < SelItems.Count; i++)
			{
				gridItemNo.Rows[Get_RealRow2(SelItems[i].ToString())].Selected = true;
			}
			gridItemNo.Select();
			Reorder();
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBDGT_ItemSetMaintain.cs" + ex.Message);
		}
	}

	private void btnMoveDown_Click(object sender, EventArgs e)
	{
		try
		{
			ArrayList SelItems = new ArrayList();
			int iIdx = -1;
			for (int i = 1; i < gridItemNo.Rows.Count; i++)
			{
				if (gridItemNo.Rows[i].Selected)
				{
					SelItems.Add(gridItemNo[i, "RowIndicator"]);
				}
			}
			for (int i = SelItems.Count - 1; i >= 0; i--)
			{
				iIdx = gridItemNo.FindRow(SelItems[i], 1, gridItemNo.Cols["RowIndicator"].SafeIndex, wrap: false);
				if (iIdx == gridItemNo.Rows.Count - 1)
				{
					break;
				}
				if (iIdx > -1)
				{
					gridItemNo.Rows[iIdx].Move(iIdx + 1);
				}
			}
			for (int i = 0; i < SelItems.Count; i++)
			{
				gridItemNo.Rows[Get_RealRow2(SelItems[i].ToString())].Selected = true;
			}
			gridItemNo.Select();
			Reorder();
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBDGT_ItemSetMaintain.cs" + ex.Message);
		}
	}

	private int Get_RealRow2(string sIndic)
	{
		int RetV = -1;
		for (int i = 1; i < gridItemNo.Rows.Count; i++)
		{
			if (gridItemNo[i, "RowIndicator"].ToString() == sIndic)
			{
				RetV = i;
				break;
			}
		}
		return RetV;
	}

	private void FormBDGT_ItemSetMaintain_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (ItemNoSetSelected.Trim() != string.Empty)
		{
			Do_SaveData();
		}
	}

	private void btnInsertItemNo_Click(object sender, EventArgs e)
	{
		int i = gridItemNo.Row;
		gridItemNo.AddItem("", gridItemNo.Row);
		gridItemNo.Row = i;
		gridItemNo.Col = 1;
		gridItemNo.Select();
		gridItemNo.StartEditing();
	}

	private void gridItemNo_MouseDown(object sender, MouseEventArgs e)
	{
		int rowIndex = gridItemNo.MouseRow;
		gridItemNo.Row = rowIndex;
	}

	private void FormBDGT_ItemSetMaintain_Activated(object sender, EventArgs e)
	{
		if (FORM_STATUS == FormStatus.Active)
		{
			FORM_STATUS = FormStatus.Normal;
		}
	}

	private void CorrectRatio()
	{
		double ratio = CommonMethods.GetWindowRatio(base.Handle);
		if (ratio != 1.0)
		{
			panel5.Font = new Font(panel5.Font.Name, (float)((double)panel5.Font.Size * ratio));
			panel4.Font = new Font(panel4.Font.Name, (float)((double)panel4.Font.Size * ratio));
			btnNewItemNoSet.Font = new Font(btnNewItemNoSet.Font.Name, (float)((double)btnNewItemNoSet.Font.Size * ratio));
			btnDeleteItemNoSet.Font = new Font(btnDeleteItemNoSet.Font.Name, (float)((double)btnDeleteItemNoSet.Font.Size * ratio));
			btnMoveUp.Font = new Font(btnMoveUp.Font.Name, (float)((double)btnMoveUp.Font.Size * ratio));
			btnMoveDown.Font = new Font(btnMoveDown.Font.Name, (float)((double)btnMoveDown.Font.Size * ratio));
			btnInsertItemNo.Font = new Font(btnInsertItemNo.Font.Name, (float)((double)btnInsertItemNo.Font.Size * ratio));
			btnDeleteItemNo.Font = new Font(btnDeleteItemNo.Font.Name, (float)((double)btnDeleteItemNo.Font.Size * ratio));
			btnFinish.Font = new Font(btnFinish.Font.Name, (float)((double)btnFinish.Font.Size * ratio));
		}
	}

	private void btnImportItemNoSet_Click(object sender, EventArgs e)
	{
		bool importFlag = false;
		DataSet ds = new DataSet();
		string XmlFileName = "";
		if (openItemNoSetXMLFileDialog.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		XmlFileName = openItemNoSetXMLFileDialog.FileName;
		if (!File.Exists(XmlFileName))
		{
			MessageBox.Show(this, "挑選的檔案不存在!", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		try
		{
			ds.ReadXml(XmlFileName);
		}
		catch (Exception)
		{
			importFlag = true;
		}
		if (ds.Tables[1].Rows.Count > 0)
		{
			if (ds.Tables[1].Rows[0]["Name"].ToString().Trim() != "序號")
			{
				importFlag = true;
			}
		}
		else
		{
			importFlag = true;
		}
		if (importFlag)
		{
			MessageBox.Show(this, "挑選的 XML 格式不正確！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		string KeyWord = $"{DateTime.Now:yyyyMMddHHmmss}";
		DataTable DT_Num = new DataTable();
		DT_Num.Columns.Add("kind", Type.GetType("System.String"));
		DT_Num.Columns.Add("cString", Type.GetType("System.String"));
		DT_Num.Columns.Add("sno", Type.GetType("System.Int64"));
		for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
		{
			DataRow DR = DT_Num.NewRow();
			DR["kind"] = KeyWord;
			DR["cString"] = ds.Tables[0].Rows[i]["cString"].ToString().Trim();
			DR["sno"] = (i + 200001).ToString();
			DT_Num.Rows.Add(DR);
		}
		DBClass DBCLS = new DBClass();
		DBCLS.SaveItemNo(DT_Num, KeyWord);
		DBCLS = null;
		BindToItemNoSetList();
	}
}
