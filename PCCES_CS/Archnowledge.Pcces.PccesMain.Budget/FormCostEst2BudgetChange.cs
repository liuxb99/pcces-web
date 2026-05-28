using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.PccesMain.ArchControls;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinProgressBar;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormCostEst2BudgetChange : Form
{
	private IContainer components;

	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private Panel panel4;

	private Panel panel7;

	private UltraButton btnRemove;

	private UltraButton btnSelect;

	private UltraButton btnRemoveAll;

	private UltraButton btnSelectAll;

	private Panel panel5;

	private GridBudget gridSourceProject;

	private UltraLabel ultraLabel3;

	private Panel panel10;

	private Panel panel6;

	private GridBudget gridProjectSelected;

	private Panel panel8;

	private UltraButton btnMoveDown;

	private UltraButton btnMoveUp;

	private UltraLabel ultraLabel4;

	private Panel panel9;

	private Panel panel2;

	private UltraButton btnPickProjectCancel;

	private Panel panel1;

	private UltraLabel ultraLabel2;

	private Panel panel14;

	private UltraButton btnWelcomeCancel;

	private UltraTabPageControl Tab_PickProject;

	private UltraTabPageControl Tab_Working;

	private UltraTabPageControl Tab_Welcome;

	private UltraTabPageControl Tab_End;

	private UltraButton btnPickProjectNext;

	private UltraButton btnWelcomeNext;

	private Panel panel15;

	private UltraButton btnEndNext;

	private UltraProgressBar progressBar;

	private Label label1;

	private Label label2;

	private Label label3;

	private Label label4;

	private Label label8;

	private Label label7;

	private Label label6;

	private Label label5;

	private Label label9;

	private string userID;

	private string parentProjectCode;

	private DataSet CostEstimateCombinedBudItem;

	public string _UserID
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

	public string _ParentProjectCode
	{
		get
		{
			return parentProjectCode;
		}
		set
		{
			parentProjectCode = value;
		}
	}

	public DataSet _CostEstimateCombinedBudItem
	{
		get
		{
			return CostEstimateCombinedBudItem;
		}
		set
		{
			CostEstimateCombinedBudItem = value;
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormCostEst2BudgetChange));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_Welcome = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.label1 = new System.Windows.Forms.Label();
		this.panel14 = new System.Windows.Forms.Panel();
		this.btnWelcomeNext = new Infragistics.Win.Misc.UltraButton();
		this.btnWelcomeCancel = new Infragistics.Win.Misc.UltraButton();
		this.Tab_PickProject = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel4 = new System.Windows.Forms.Panel();
		this.panel7 = new System.Windows.Forms.Panel();
		this.btnRemove = new Infragistics.Win.Misc.UltraButton();
		this.btnSelect = new Infragistics.Win.Misc.UltraButton();
		this.btnRemoveAll = new Infragistics.Win.Misc.UltraButton();
		this.btnSelectAll = new Infragistics.Win.Misc.UltraButton();
		this.panel5 = new System.Windows.Forms.Panel();
		this.gridSourceProject = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.panel10 = new System.Windows.Forms.Panel();
		this.panel6 = new System.Windows.Forms.Panel();
		this.gridProjectSelected = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.panel8 = new System.Windows.Forms.Panel();
		this.btnMoveDown = new Infragistics.Win.Misc.UltraButton();
		this.btnMoveUp = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.panel9 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.btnPickProjectNext = new Infragistics.Win.Misc.UltraButton();
		this.btnPickProjectCancel = new Infragistics.Win.Misc.UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_Working = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.progressBar = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
		this.Tab_End = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel15 = new System.Windows.Forms.Panel();
		this.btnEndNext = new Infragistics.Win.Misc.UltraButton();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.Tab_Welcome.SuspendLayout();
		this.panel14.SuspendLayout();
		this.Tab_PickProject.SuspendLayout();
		this.panel4.SuspendLayout();
		this.panel7.SuspendLayout();
		this.panel5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridSourceProject).BeginInit();
		this.panel6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridProjectSelected).BeginInit();
		this.panel8.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel1.SuspendLayout();
		this.Tab_Working.SuspendLayout();
		this.Tab_End.SuspendLayout();
		this.panel15.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_Welcome.Controls.Add(this.label3);
		this.Tab_Welcome.Controls.Add(this.label2);
		this.Tab_Welcome.Controls.Add(this.label1);
		this.Tab_Welcome.Controls.Add(this.panel14);
		this.Tab_Welcome.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_Welcome.Name = "Tab_Welcome";
		this.Tab_Welcome.Size = new System.Drawing.Size(782, 556);
		this.label1.BackColor = System.Drawing.Color.White;
		this.label1.Location = new System.Drawing.Point(45, 119);
		this.label1.Margin = new System.Windows.Forms.Padding(0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(701, 17);
		this.label1.TabIndex = 11;
		this.label1.Text = "請選擇要合併的版次，系統會自動幫您合併。";
		this.panel14.Controls.Add(this.btnWelcomeNext);
		this.panel14.Controls.Add(this.btnWelcomeCancel);
		this.panel14.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel14.Location = new System.Drawing.Point(0, 510);
		this.panel14.Name = "panel14";
		this.panel14.Size = new System.Drawing.Size(782, 46);
		this.panel14.TabIndex = 6;
		this.btnWelcomeNext.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnWelcomeNext.Appearance = appearance1;
		this.btnWelcomeNext.BackColor = System.Drawing.SystemColors.Control;
		this.btnWelcomeNext.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnWelcomeNext.Font = new System.Drawing.Font("細明體", 11f);
		this.btnWelcomeNext.ImageSize = new System.Drawing.Size(20, 20);
		this.btnWelcomeNext.ImageTransparentColor = System.Drawing.Color.White;
		this.btnWelcomeNext.Location = new System.Drawing.Point(588, 8);
		this.btnWelcomeNext.Name = "btnWelcomeNext";
		this.btnWelcomeNext.ShowFocusRect = false;
		this.btnWelcomeNext.ShowOutline = false;
		this.btnWelcomeNext.Size = new System.Drawing.Size(88, 31);
		this.btnWelcomeNext.SupportThemes = false;
		this.btnWelcomeNext.TabIndex = 4;
		this.btnWelcomeNext.Text = "下一步";
		this.btnWelcomeNext.Click += new System.EventHandler(btnWelcomeNext_Click);
		this.btnWelcomeCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnWelcomeCancel.Appearance = appearance2;
		this.btnWelcomeCancel.BackColor = System.Drawing.SystemColors.Control;
		this.btnWelcomeCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnWelcomeCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnWelcomeCancel.Font = new System.Drawing.Font("細明體", 11f);
		this.btnWelcomeCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btnWelcomeCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btnWelcomeCancel.Location = new System.Drawing.Point(682, 8);
		this.btnWelcomeCancel.Name = "btnWelcomeCancel";
		this.btnWelcomeCancel.ShowFocusRect = false;
		this.btnWelcomeCancel.ShowOutline = false;
		this.btnWelcomeCancel.Size = new System.Drawing.Size(88, 31);
		this.btnWelcomeCancel.SupportThemes = false;
		this.btnWelcomeCancel.TabIndex = 3;
		this.btnWelcomeCancel.Text = "取消";
		this.Tab_PickProject.Controls.Add(this.panel4);
		this.Tab_PickProject.Controls.Add(this.panel2);
		this.Tab_PickProject.Controls.Add(this.panel1);
		this.Tab_PickProject.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_PickProject.Name = "Tab_PickProject";
		this.Tab_PickProject.Size = new System.Drawing.Size(782, 556);
		this.panel4.Controls.Add(this.panel7);
		this.panel4.Controls.Add(this.panel5);
		this.panel4.Controls.Add(this.panel10);
		this.panel4.Controls.Add(this.panel6);
		this.panel4.Controls.Add(this.panel9);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel4.Location = new System.Drawing.Point(0, 60);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(782, 450);
		this.panel4.TabIndex = 7;
		this.panel7.BackColor = System.Drawing.Color.White;
		this.panel7.Controls.Add(this.btnRemove);
		this.panel7.Controls.Add(this.btnSelect);
		this.panel7.Controls.Add(this.btnRemoveAll);
		this.panel7.Controls.Add(this.btnSelectAll);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel7.Location = new System.Drawing.Point(339, 0);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(104, 450);
		this.panel7.TabIndex = 2;
		appearance3.FontData.SizeInPoints = 9f;
		this.btnRemove.Appearance = appearance3;
		this.btnRemove.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnRemove.Location = new System.Drawing.Point(10, 227);
		this.btnRemove.Name = "btnRemove";
		this.btnRemove.ShowFocusRect = false;
		this.btnRemove.ShowOutline = false;
		this.btnRemove.Size = new System.Drawing.Size(85, 30);
		this.btnRemove.SupportThemes = false;
		this.btnRemove.TabIndex = 4;
		this.btnRemove.Text = "< 移除";
		this.btnRemove.Click += new System.EventHandler(btnRemove_Click);
		appearance4.FontData.SizeInPoints = 9f;
		this.btnSelect.Appearance = appearance4;
		this.btnSelect.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnSelect.Location = new System.Drawing.Point(10, 194);
		this.btnSelect.Name = "btnSelect";
		this.btnSelect.ShowFocusRect = false;
		this.btnSelect.ShowOutline = false;
		this.btnSelect.Size = new System.Drawing.Size(85, 30);
		this.btnSelect.SupportThemes = false;
		this.btnSelect.TabIndex = 3;
		this.btnSelect.Text = "選取 >";
		this.btnSelect.Click += new System.EventHandler(btnSelect_Click);
		appearance5.FontData.SizeInPoints = 9f;
		this.btnRemoveAll.Appearance = appearance5;
		this.btnRemoveAll.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnRemoveAll.Location = new System.Drawing.Point(10, 260);
		this.btnRemoveAll.Name = "btnRemoveAll";
		this.btnRemoveAll.ShowFocusRect = false;
		this.btnRemoveAll.ShowOutline = false;
		this.btnRemoveAll.Size = new System.Drawing.Size(85, 30);
		this.btnRemoveAll.SupportThemes = false;
		this.btnRemoveAll.TabIndex = 1;
		this.btnRemoveAll.Text = "<< 全部移除";
		this.btnRemoveAll.Click += new System.EventHandler(btnRemoveAll_Click);
		appearance6.FontData.SizeInPoints = 9f;
		this.btnSelectAll.Appearance = appearance6;
		this.btnSelectAll.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnSelectAll.Location = new System.Drawing.Point(10, 161);
		this.btnSelectAll.Name = "btnSelectAll";
		this.btnSelectAll.ShowFocusRect = false;
		this.btnSelectAll.ShowOutline = false;
		this.btnSelectAll.Size = new System.Drawing.Size(85, 30);
		this.btnSelectAll.SupportThemes = false;
		this.btnSelectAll.TabIndex = 0;
		this.btnSelectAll.Text = "全選 >>";
		this.btnSelectAll.Click += new System.EventHandler(btnSelectAll_Click);
		this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel5.Controls.Add(this.gridSourceProject);
		this.panel5.Controls.Add(this.ultraLabel3);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel5.Location = new System.Drawing.Point(15, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(324, 450);
		this.panel5.TabIndex = 9;
		this.gridSourceProject._ExcelFileName = "";
		this.gridSourceProject._ExcelSheeName = "";
		this.gridSourceProject._IsOpenExcelAfterExport = false;
		this.gridSourceProject.AllowEditing = false;
		this.gridSourceProject.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridSourceProject.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridSourceProject.ColumnInfo = resources.GetString("gridSourceProject.ColumnInfo");
		this.gridSourceProject.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridSourceProject.ExtendLastCol = true;
		this.gridSourceProject.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
		this.gridSourceProject.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridSourceProject.ForeColor = System.Drawing.Color.Black;
		this.gridSourceProject.Location = new System.Drawing.Point(0, 28);
		this.gridSourceProject.Name = "gridSourceProject";
		this.gridSourceProject.Rows.Count = 1;
		this.gridSourceProject.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridSourceProject.ShowCursor = true;
		this.gridSourceProject.ShowToolTipOnNarrowColumn = true;
		this.gridSourceProject.Size = new System.Drawing.Size(322, 420);
		this.gridSourceProject.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridSourceProject.Styles"));
		this.gridSourceProject.TabIndex = 1;
		this.gridSourceProject.Tree.Column = 1;
		this.gridSourceProject.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridSourceProject.DoubleClick += new System.EventHandler(gridSourceProject_DoubleClick);
		appearance7.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance7.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel3.Appearance = appearance7;
		this.ultraLabel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel3.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(322, 28);
		this.ultraLabel3.TabIndex = 2;
		this.ultraLabel3.Text = "已核可預估成本版次列表";
		this.panel10.BackColor = System.Drawing.Color.White;
		this.panel10.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel10.Location = new System.Drawing.Point(0, 0);
		this.panel10.Name = "panel10";
		this.panel10.Size = new System.Drawing.Size(15, 450);
		this.panel10.TabIndex = 8;
		this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel6.Controls.Add(this.gridProjectSelected);
		this.panel6.Controls.Add(this.panel8);
		this.panel6.Controls.Add(this.ultraLabel4);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel6.Location = new System.Drawing.Point(443, 0);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(324, 450);
		this.panel6.TabIndex = 1;
		this.gridProjectSelected._ExcelFileName = "";
		this.gridProjectSelected._ExcelSheeName = "";
		this.gridProjectSelected._IsOpenExcelAfterExport = false;
		this.gridProjectSelected.AllowEditing = false;
		this.gridProjectSelected.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridProjectSelected.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridProjectSelected.ColumnInfo = resources.GetString("gridProjectSelected.ColumnInfo");
		this.gridProjectSelected.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridProjectSelected.ExtendLastCol = true;
		this.gridProjectSelected.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
		this.gridProjectSelected.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridProjectSelected.ForeColor = System.Drawing.Color.Black;
		this.gridProjectSelected.Location = new System.Drawing.Point(0, 28);
		this.gridProjectSelected.Name = "gridProjectSelected";
		this.gridProjectSelected.Rows.Count = 1;
		this.gridProjectSelected.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridProjectSelected.ShowCursor = true;
		this.gridProjectSelected.ShowToolTipOnNarrowColumn = true;
		this.gridProjectSelected.Size = new System.Drawing.Size(322, 388);
		this.gridProjectSelected.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridProjectSelected.Styles"));
		this.gridProjectSelected.TabIndex = 5;
		this.gridProjectSelected.Tree.Column = 1;
		this.gridProjectSelected.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridProjectSelected.DoubleClick += new System.EventHandler(gridProjectSelected_DoubleClick);
		this.panel8.Controls.Add(this.btnMoveDown);
		this.panel8.Controls.Add(this.btnMoveUp);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel8.Location = new System.Drawing.Point(0, 416);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(322, 32);
		this.panel8.TabIndex = 4;
		appearance8.FontData.SizeInPoints = 9f;
		appearance8.Image = resources.GetObject("appearance8.Image");
		appearance8.ImageVAlign = Infragistics.Win.VAlign.Middle;
		appearance8.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.btnMoveDown.Appearance = appearance8;
		this.btnMoveDown.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnMoveDown.Location = new System.Drawing.Point(163, 2);
		this.btnMoveDown.Name = "btnMoveDown";
		this.btnMoveDown.ShowFocusRect = false;
		this.btnMoveDown.ShowOutline = false;
		this.btnMoveDown.Size = new System.Drawing.Size(60, 28);
		this.btnMoveDown.SupportThemes = false;
		this.btnMoveDown.TabIndex = 7;
		this.btnMoveDown.Text = "下移";
		this.btnMoveDown.Click += new System.EventHandler(btnMoveDown_Click);
		appearance9.FontData.SizeInPoints = 9f;
		appearance9.Image = resources.GetObject("appearance9.Image");
		appearance9.ImageVAlign = Infragistics.Win.VAlign.Middle;
		appearance9.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.btnMoveUp.Appearance = appearance9;
		this.btnMoveUp.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnMoveUp.Location = new System.Drawing.Point(100, 2);
		this.btnMoveUp.Name = "btnMoveUp";
		this.btnMoveUp.ShowFocusRect = false;
		this.btnMoveUp.ShowOutline = false;
		this.btnMoveUp.Size = new System.Drawing.Size(60, 28);
		this.btnMoveUp.SupportThemes = false;
		this.btnMoveUp.TabIndex = 6;
		this.btnMoveUp.Text = "上移";
		this.btnMoveUp.Click += new System.EventHandler(btnMoveUp_Click);
		appearance10.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance10.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance10.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel4.Appearance = appearance10;
		this.ultraLabel4.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.ultraLabel4.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel4.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(322, 28);
		this.ultraLabel4.TabIndex = 3;
		this.ultraLabel4.Text = "已選取的版次";
		this.panel9.BackColor = System.Drawing.Color.White;
		this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel9.Location = new System.Drawing.Point(767, 0);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(15, 450);
		this.panel9.TabIndex = 7;
		this.panel2.Controls.Add(this.label9);
		this.panel2.Controls.Add(this.btnPickProjectNext);
		this.panel2.Controls.Add(this.btnPickProjectCancel);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 510);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(782, 46);
		this.panel2.TabIndex = 5;
		this.btnPickProjectNext.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance11.Image = resources.GetObject("appearance11.Image");
		appearance11.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance11.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnPickProjectNext.Appearance = appearance11;
		this.btnPickProjectNext.BackColor = System.Drawing.SystemColors.Control;
		this.btnPickProjectNext.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnPickProjectNext.Font = new System.Drawing.Font("細明體", 11f);
		this.btnPickProjectNext.ImageSize = new System.Drawing.Size(20, 20);
		this.btnPickProjectNext.ImageTransparentColor = System.Drawing.Color.White;
		this.btnPickProjectNext.Location = new System.Drawing.Point(588, 8);
		this.btnPickProjectNext.Name = "btnPickProjectNext";
		this.btnPickProjectNext.ShowFocusRect = false;
		this.btnPickProjectNext.ShowOutline = false;
		this.btnPickProjectNext.Size = new System.Drawing.Size(88, 31);
		this.btnPickProjectNext.SupportThemes = false;
		this.btnPickProjectNext.TabIndex = 4;
		this.btnPickProjectNext.Text = "下一步";
		this.btnPickProjectNext.Click += new System.EventHandler(btnPickProjectNext_Click);
		this.btnPickProjectCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance12.Image = resources.GetObject("appearance12.Image");
		appearance12.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnPickProjectCancel.Appearance = appearance12;
		this.btnPickProjectCancel.BackColor = System.Drawing.SystemColors.Control;
		this.btnPickProjectCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnPickProjectCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnPickProjectCancel.Font = new System.Drawing.Font("細明體", 11f);
		this.btnPickProjectCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btnPickProjectCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btnPickProjectCancel.Location = new System.Drawing.Point(682, 8);
		this.btnPickProjectCancel.Name = "btnPickProjectCancel";
		this.btnPickProjectCancel.ShowFocusRect = false;
		this.btnPickProjectCancel.ShowOutline = false;
		this.btnPickProjectCancel.Size = new System.Drawing.Size(88, 31);
		this.btnPickProjectCancel.SupportThemes = false;
		this.btnPickProjectCancel.TabIndex = 3;
		this.btnPickProjectCancel.Text = "取消";
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.ultraLabel2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(782, 60);
		this.panel1.TabIndex = 4;
		this.ultraLabel2.Location = new System.Drawing.Point(14, 12);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(688, 20);
		this.ultraLabel2.TabIndex = 1;
		this.ultraLabel2.Text = "挑選要合併的版次";
		this.Tab_Working.Controls.Add(this.label4);
		this.Tab_Working.Controls.Add(this.progressBar);
		this.Tab_Working.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_Working.Name = "Tab_Working";
		this.Tab_Working.Size = new System.Drawing.Size(782, 556);
		this.progressBar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.progressBar.Location = new System.Drawing.Point(67, 267);
		this.progressBar.Name = "progressBar";
		this.progressBar.Size = new System.Drawing.Size(648, 23);
		this.progressBar.TabIndex = 1;
		this.progressBar.Text = "[Formatted]";
		this.Tab_End.Controls.Add(this.label8);
		this.Tab_End.Controls.Add(this.label7);
		this.Tab_End.Controls.Add(this.label6);
		this.Tab_End.Controls.Add(this.label5);
		this.Tab_End.Controls.Add(this.panel15);
		this.Tab_End.Location = new System.Drawing.Point(0, 0);
		this.Tab_End.Name = "Tab_End";
		this.Tab_End.Size = new System.Drawing.Size(782, 556);
		this.panel15.Controls.Add(this.btnEndNext);
		this.panel15.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel15.Location = new System.Drawing.Point(0, 510);
		this.panel15.Name = "panel15";
		this.panel15.Size = new System.Drawing.Size(782, 46);
		this.panel15.TabIndex = 8;
		this.btnEndNext.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance13.Image = resources.GetObject("appearance13.Image");
		appearance13.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnEndNext.Appearance = appearance13;
		this.btnEndNext.BackColor = System.Drawing.SystemColors.Control;
		this.btnEndNext.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnEndNext.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnEndNext.Font = new System.Drawing.Font("細明體", 11f);
		this.btnEndNext.ImageSize = new System.Drawing.Size(20, 20);
		this.btnEndNext.ImageTransparentColor = System.Drawing.Color.White;
		this.btnEndNext.Location = new System.Drawing.Point(682, 8);
		this.btnEndNext.Name = "btnEndNext";
		this.btnEndNext.ShowFocusRect = false;
		this.btnEndNext.ShowOutline = false;
		this.btnEndNext.Size = new System.Drawing.Size(88, 31);
		this.btnEndNext.SupportThemes = false;
		this.btnEndNext.TabIndex = 4;
		this.btnEndNext.Text = "完成";
		this.btnEndNext.Click += new System.EventHandler(btnEndNext_Click);
		appearance14.BackColor = System.Drawing.Color.White;
		this.Tab_Ctrl.Appearance = appearance14;
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_PickProject);
		this.Tab_Ctrl.Controls.Add(this.Tab_Working);
		this.Tab_Ctrl.Controls.Add(this.Tab_Welcome);
		this.Tab_Ctrl.Controls.Add(this.Tab_End);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(782, 556);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabIndex = 0;
		ultraTab1.TabPage = this.Tab_Welcome;
		ultraTab1.Text = "Page1";
		ultraTab2.TabPage = this.Tab_PickProject;
		ultraTab2.Text = "tab1";
		ultraTab3.TabPage = this.Tab_Working;
		ultraTab3.Text = "tab3";
		ultraTab4.TabPage = this.Tab_End;
		ultraTab4.Text = "tab4";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[4] { ultraTab1, ultraTab2, ultraTab3, ultraTab4 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(782, 556);
		this.label2.BackColor = System.Drawing.Color.White;
		this.label2.Location = new System.Drawing.Point(41, 96);
		this.label2.Margin = new System.Windows.Forms.Padding(0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(128, 20);
		this.label2.TabIndex = 12;
		this.label2.Text = "說明:";
		this.label3.BackColor = System.Drawing.Color.White;
		this.label3.Location = new System.Drawing.Point(26, 32);
		this.label3.Margin = new System.Windows.Forms.Padding(0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(588, 20);
		this.label3.TabIndex = 13;
		this.label3.Text = "歡迎使用合併預估成本精靈，接下來我們將引導您一步一步來執行";
		this.label4.BackColor = System.Drawing.Color.White;
		this.label4.Location = new System.Drawing.Point(25, 43);
		this.label4.Margin = new System.Windows.Forms.Padding(0);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(588, 20);
		this.label4.TabIndex = 14;
		this.label4.Text = "合併中，請稍候";
		this.label5.BackColor = System.Drawing.Color.White;
		this.label5.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.label5.Location = new System.Drawing.Point(25, 32);
		this.label5.Margin = new System.Windows.Forms.Padding(0);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(588, 20);
		this.label5.TabIndex = 21;
		this.label5.Text = "恭禧您！";
		this.label6.BackColor = System.Drawing.Color.White;
		this.label6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label6.Location = new System.Drawing.Point(41, 69);
		this.label6.Margin = new System.Windows.Forms.Padding(0);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(588, 20);
		this.label6.TabIndex = 22;
		this.label6.Text = "你已經成功完成合併已核可預估成本。";
		this.label7.BackColor = System.Drawing.Color.White;
		this.label7.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label7.Location = new System.Drawing.Point(41, 129);
		this.label7.Margin = new System.Windows.Forms.Padding(0);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(588, 20);
		this.label7.TabIndex = 23;
		this.label7.Text = "若要結束精靈，請按一下[完成]。";
		this.label8.BackColor = System.Drawing.Color.White;
		this.label8.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label8.Location = new System.Drawing.Point(41, 188);
		this.label8.Margin = new System.Windows.Forms.Padding(0);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(588, 20);
		this.label8.TabIndex = 24;
		this.label8.Text = "回到列表畫面時，請選擇合併的專案。";
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(36, 8);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(200, 15);
		this.label9.TabIndex = 5;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.btnWelcomeCancel;
		base.ClientSize = new System.Drawing.Size(782, 556);
		base.Controls.Add(this.Tab_Ctrl);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormCostEst2BudgetChange";
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "合併已核可預估成本";
		base.Load += new System.EventHandler(FormCostEst2BudgetChange_Load);
		this.Tab_Welcome.ResumeLayout(false);
		this.panel14.ResumeLayout(false);
		this.Tab_PickProject.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridSourceProject).EndInit();
		this.panel6.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridProjectSelected).EndInit();
		this.panel8.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.panel1.ResumeLayout(false);
		this.Tab_Working.ResumeLayout(false);
		this.Tab_End.ResumeLayout(false);
		this.panel15.ResumeLayout(false);
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

	public FormCostEst2BudgetChange(string userID, string parentProjectCode)
	{
		InitializeComponent();
		this.userID = userID;
		this.parentProjectCode = parentProjectCode;
	}

	private void FormCostEst2BudgetChange_Load(object sender, EventArgs e)
	{
		BudProjectCodeMapping budProjectCodeMapping = new BudProjectCodeMapping();
		int IsType = 5;
		DataSet dsProject = budProjectCodeMapping.GetBudProjectCodeMappingByParentProjectCode(parentProjectCode, IsType, 1);
		SourceCostQuoteProject sourceCostQuoteProject = new SourceCostQuoteProject();
		DataSet dsSourceCostQuoteProject = sourceCostQuoteProject.GetSourceCostQuoteProjectByParentCode(parentProjectCode);
		BindToGridProjectList(dsProject, dsSourceCostQuoteProject);
	}

	private void BindToGridProjectList(DataSet dsProject, DataSet dsSourceCostQuoteProject)
	{
		CellStyle styleSelected = gridSourceProject.Styles.Add("Selected");
		styleSelected.BackColor = Color.Gold;
		gridSourceProject.Rows.Count = dsProject.Tables[0].Rows.Count + 1;
		DataView dvSourceCostQuoteProject = new DataView(dsSourceCostQuoteProject.Tables[0]);
		gridSourceProject.Rows.Count = dsProject.Tables[0].Rows.Count + 1;
		gridSourceProject.Redraw = false;
		for (int i = 0; i < dsProject.Tables[0].Rows.Count; i++)
		{
			DataRow row = dsProject.Tables[0].Rows[i];
			string projectCode = row["ProjectCode"].ToString();
			gridSourceProject[i + 1, "ProjectCode"] = projectCode;
			gridSourceProject[i + 1, "Version"] = row["Version"].ToString();
			gridSourceProject[i + 1, "InsertDate"] = row["InsertDate"];
			gridSourceProject[i + 1, "PersonInCharge"] = row["PersonInCharge"].ToString();
			gridSourceProject[i + 1, "Purpose"] = row["Purpose"].ToString();
			dvSourceCostQuoteProject.RowFilter = $"CostQuoteProjectCode='{projectCode}'";
			if (dvSourceCostQuoteProject.Count > 0)
			{
				gridSourceProject.Rows[i + 1].Style = styleSelected;
			}
		}
		dvSourceCostQuoteProject.Dispose();
		dvSourceCostQuoteProject = null;
		gridSourceProject.Redraw = true;
		gridSourceProject.AutoSizeCols();
	}

	private void btnSelectAll_Click(object sender, EventArgs e)
	{
		MoveAllRows(gridSourceProject, gridProjectSelected);
	}

	private void btnRemoveAll_Click(object sender, EventArgs e)
	{
		MoveAllRows(gridProjectSelected, gridSourceProject);
	}

	private void MoveAllRows(GridBudget source, GridBudget target)
	{
		target.Redraw = false;
		foreach (Row row in (IEnumerable)source.Rows)
		{
			if (row.Index != 0)
			{
				Row targetRow = target.Rows.Add();
				targetRow["ProjectCode"] = row["ProjectCode"];
				targetRow["Version"] = row["Version"];
				targetRow["InsertDate"] = row["InsertDate"];
				targetRow["PersonInCharge"] = row["PersonInCharge"];
				targetRow["Purpose"] = row["Purpose"];
				targetRow.Style = row.Style;
			}
		}
		target.Redraw = true;
		source.Rows.Count = 1;
		source.AutoSizeCols();
		target.AutoSizeCols();
	}

	private void gridSourceProject_DoubleClick(object sender, EventArgs e)
	{
		MoveRow(gridSourceProject, gridProjectSelected);
	}

	private void btnSelect_Click(object sender, EventArgs e)
	{
		MoveRow(gridSourceProject, gridProjectSelected);
	}

	private void gridProjectSelected_DoubleClick(object sender, EventArgs e)
	{
		MoveRow(gridProjectSelected, gridSourceProject);
	}

	private void btnRemove_Click(object sender, EventArgs e)
	{
		if (gridProjectSelected.SelectedRowCount == 0)
		{
			MessageBox.Show(this, "請先選取要移除的專案！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			MoveRow(gridProjectSelected, gridSourceProject);
		}
	}

	private void MoveRow(GridBudget source, GridBudget target)
	{
		for (int i = source.Rows.Count - 1; i > 0; i--)
		{
			Row sourceRow = source.Rows[i];
			if (sourceRow.Selected)
			{
				Row targetRow = target.Rows.Add();
				targetRow["ProjectCode"] = sourceRow["ProjectCode"];
				targetRow["Version"] = sourceRow["Version"];
				targetRow["InsertDate"] = sourceRow["InsertDate"];
				targetRow["PersonInCharge"] = sourceRow["PersonInCharge"];
				targetRow["Purpose"] = sourceRow["Purpose"];
				targetRow.Style = sourceRow.Style;
				source.RemoveItem(i);
			}
		}
		source.AutoSizeCols();
		target.AutoSizeCols();
	}

	private void btnMoveUp_Click(object sender, EventArgs e)
	{
		RowCollection selectedRows = gridProjectSelected.Rows.Selected;
		if (selectedRows.Count == 0 || selectedRows[0].Index == 1)
		{
			return;
		}
		foreach (Row row in (IEnumerable)selectedRows)
		{
			row.Move(row.Index - 1);
		}
	}

	private void btnMoveDown_Click(object sender, EventArgs e)
	{
		RowCollection selectedRows = gridProjectSelected.Rows.Selected;
		if (selectedRows.Count != 0 && selectedRows[selectedRows.Count - 1].Index != gridProjectSelected.Rows.Count - 1)
		{
			for (int i = selectedRows.Count - 1; i >= 0; i--)
			{
				selectedRows[i].Move(selectedRows[i].Index + 1);
			}
		}
	}

	private void btnWelcomeNext_Click(object sender, EventArgs e)
	{
		Tab_PickProject.Tab.Selected = true;
	}

	private void btnPickProjectNext_Click(object sender, EventArgs e)
	{
		if (gridProjectSelected.Rows.Count <= 1)
		{
			MessageBox.Show(this, "請至少選擇一個專案！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		bool warning = false;
		for (int i = 1; i < gridProjectSelected.Rows.Count; i++)
		{
			if (gridProjectSelected.Rows[i].Style != null)
			{
				warning = true;
				break;
			}
		}
		if (warning && MessageBox.Show("挑選了已經被合併過的預估成本，是否繼續？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
		{
			return;
		}
		Cursor = Cursors.WaitCursor;
		Tab_Working.Tab.Selected = true;
		progressBar.Maximum = gridProjectSelected.Rows.Count - 1;
		BudProject budProject = new BudProject();
		string[] mergeProjectCode = new string[gridProjectSelected.Rows.Count];
		for (int i = 1; i < gridProjectSelected.Rows.Count; i++)
		{
			mergeProjectCode[i - 1] = gridProjectSelected[i, "ProjectCode"].ToString().Trim();
		}
		if (!budProject.CheckMergeProjPccesCodeConfirmed(mergeProjectCode))
		{
			MessageBox.Show("挑選之欲合併專案中有新增工項的PccesCode尚未確認,請確認後再合併");
			Cursor = Cursors.Default;
			Tab_PickProject.Tab.Selected = true;
			return;
		}
		try
		{
			DataSet CreateNewVersionByCostEstimateCombine = new DataSet();
			ExecResult ER = budProject.CreateNewVersionByCostEstimateCombined(mergeProjectCode, parentProjectCode, CreateNewVersionByCostEstimateCombine);
			if (ER.ReturnCode == 0)
			{
				_CostEstimateCombinedBudItem = CreateNewVersionByCostEstimateCombine;
			}
			else
			{
				MessageBox.Show(this, "CreateNewVersionByCostEstimateCombined Error : " + ER.Message, "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			Tab_End.Tab.Selected = true;
		}
		catch (Exception ex)
		{
			MessageBox.Show("btnPickProjectNext_Click Error : " + ex.Message);
		}
		Cursor = Cursors.Default;
	}

	private void btnEndNext_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
	}

	private void btn_C_Prev_Click(object sender, EventArgs e)
	{
		Tab_PickProject.Tab.Selected = true;
	}
}
