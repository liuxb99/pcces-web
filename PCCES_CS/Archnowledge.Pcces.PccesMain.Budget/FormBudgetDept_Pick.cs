using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.Budget.Option;
using Archnowledge.Pcces.PccesMain.SysMaintain;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetDept_Pick : Form
{
	private IContainer components;

	private UltraToolbarsManager toolbarManager;

	private Panel panel1;

	private Panel panel2;

	private Panel panel3;

	private GridBudget gridMainUnit;

	private UltraButton btnOK;

	private UltraButton btnCancel;

	private UltraToolbarsDockArea _FormBudgetDept_Pick_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormBudgetDept_Pick_Toolbars_Dock_Area_Right;

	private UltraToolbarsDockArea _FormBudgetDept_Pick_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormBudgetDept_Pick_Toolbars_Dock_Area_Bottom;

	private string UserID;

	private string FormOwner;

	private string lastSearchKeyWord;

	public string _UserID
	{
		get
		{
			return UserID;
		}
		set
		{
			UserID = value;
		}
	}

	public string _OwnerName
	{
		get
		{
			return FormOwner;
		}
		set
		{
			FormOwner = value;
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetDept_Pick));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool1");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("lbSearch");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool1 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("ddlSearchList");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Go");
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("lbSearch");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool2 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("ddlSearchList");
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Go");
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.btnOK = new Infragistics.Win.Misc.UltraButton();
		this.btnCancel = new Infragistics.Win.Misc.UltraButton();
		this.panel3 = new System.Windows.Forms.Panel();
		this.gridMainUnit = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.toolbarManager = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.panel2.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridMainUnit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.toolbarManager).BeginInit();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 27);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(492, 0);
		this.panel1.TabIndex = 0;
		this.panel2.Controls.Add(this.btnOK);
		this.panel2.Controls.Add(this.btnCancel);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 269);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(492, 36);
		this.panel2.TabIndex = 1;
		this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnOK.Appearance = appearance1;
		this.btnOK.BackColor = System.Drawing.SystemColors.Control;
		this.btnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnOK.Font = new System.Drawing.Font("細明體", 11f);
		this.btnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.btnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.btnOK.Location = new System.Drawing.Point(304, 4);
		this.btnOK.Name = "btnOK";
		this.btnOK.ShowFocusRect = false;
		this.btnOK.ShowOutline = false;
		this.btnOK.Size = new System.Drawing.Size(88, 31);
		this.btnOK.SupportThemes = false;
		this.btnOK.TabIndex = 6;
		this.btnOK.Text = "確定";
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnCancel.Appearance = appearance2;
		this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
		this.btnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Font = new System.Drawing.Font("細明體", 11f);
		this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btnCancel.Location = new System.Drawing.Point(396, 4);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.ShowFocusRect = false;
		this.btnCancel.ShowOutline = false;
		this.btnCancel.Size = new System.Drawing.Size(88, 31);
		this.btnCancel.SupportThemes = false;
		this.btnCancel.TabIndex = 5;
		this.btnCancel.Text = "取消";
		this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel3.Controls.Add(this.gridMainUnit);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 27);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(492, 242);
		this.panel3.TabIndex = 2;
		this.gridMainUnit._ExcelFileName = "";
		this.gridMainUnit._ExcelSheeName = "";
		this.gridMainUnit._IsOpenExcelAfterExport = false;
		this.gridMainUnit.AllowEditing = false;
		this.gridMainUnit.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridMainUnit.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.gridMainUnit.ColumnInfo = resources.GetString("gridMainUnit.ColumnInfo");
		this.gridMainUnit.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridMainUnit.ExtendLastCol = true;
		this.gridMainUnit.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
		this.gridMainUnit.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridMainUnit.ForeColor = System.Drawing.Color.Black;
		this.gridMainUnit.Location = new System.Drawing.Point(0, 0);
		this.gridMainUnit.Name = "gridMainUnit";
		this.gridMainUnit.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.gridMainUnit.ShowCursor = true;
		this.gridMainUnit.ShowToolTipOnNarrowColumn = true;
		this.gridMainUnit.Size = new System.Drawing.Size(490, 240);
		this.gridMainUnit.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridMainUnit.Styles"));
		this.gridMainUnit.TabIndex = 2;
		this.gridMainUnit.Tree.Column = 1;
		this.gridMainUnit.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridMainUnit.DoubleClick += new System.EventHandler(gridMainUnit_DoubleClick);
		appearance3.FontData.Name = "Arial";
		appearance3.FontData.SizeInPoints = 9f;
		this.toolbarManager.Appearance = appearance3;
		appearance4.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance4.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.toolbarManager.DockAreaAppearance = appearance4;
		this.toolbarManager.DockWithinContainer = this;
		this.toolbarManager.LockToolbars = true;
		appearance5.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance5.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance5.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.toolbarManager.MenuSettings.HotTrackAppearance = appearance5;
		appearance6.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance6.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.toolbarManager.MenuSettings.IconAreaAppearance = appearance6;
		appearance7.BackColor = System.Drawing.Color.White;
		appearance7.BackColor2 = System.Drawing.Color.White;
		this.toolbarManager.MenuSettings.ToolAppearance = appearance7;
		this.toolbarManager.ShowFullMenusDelay = 500;
		this.toolbarManager.ShowQuickCustomizeButton = false;
		this.toolbarManager.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.Text = "Tool1";
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { labelTool1, comboBoxTool1, buttonTool1 });
		this.toolbarManager.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[1] { ultraToolbar1 });
		appearance8.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance8.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.toolbarManager.ToolbarSettings.Appearance = appearance8;
		appearance9.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance9.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance9.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.toolbarManager.ToolbarSettings.HotTrackAppearance = appearance9;
		labelTool2.SharedProps.Caption = "尋找:";
		labelTool2.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool2.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool2.SharedProps.Caption = "尋找下拉";
		comboBoxTool2.SharedProps.Width = 200;
		comboBoxTool2.ValueList = valueList1;
		appearance10.Image = resources.GetObject("appearance10.Image");
		buttonTool2.SharedProps.AppearancesSmall.Appearance = appearance10;
		buttonTool2.SharedProps.Caption = "Go";
		this.toolbarManager.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { labelTool2, comboBoxTool2, buttonTool2 });
		this.toolbarManager.ToolKeyDown += new Infragistics.Win.UltraWinToolbars.ToolKeyEventHandler(toolbarManager_ToolKeyDown);
		this.toolbarManager.BeforeToolbarListDropdown += new Infragistics.Win.UltraWinToolbars.BeforeToolbarListDropdownEventHandler(toolbarManager_BeforeToolbarListDropdown);
		this.toolbarManager.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(toolbarManager_ToolClick);
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 27);
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Left.Name = "_FormBudgetDept_Pick_Toolbars_Dock_Area_Left";
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 278);
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Left.ToolbarsManager = this.toolbarManager;
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(492, 27);
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Right.Name = "_FormBudgetDept_Pick_Toolbars_Dock_Area_Right";
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 278);
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Right.ToolbarsManager = this.toolbarManager;
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Top.Name = "_FormBudgetDept_Pick_Toolbars_Dock_Area_Top";
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(492, 27);
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Top.ToolbarsManager = this.toolbarManager;
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 305);
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Bottom.Name = "_FormBudgetDept_Pick_Toolbars_Dock_Area_Bottom";
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(492, 0);
		this._FormBudgetDept_Pick_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.toolbarManager;
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(492, 305);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this._FormBudgetDept_Pick_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormBudgetDept_Pick_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormBudgetDept_Pick_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormBudgetDept_Pick_Toolbars_Dock_Area_Bottom);
		base.KeyPreview = true;
		base.Name = "FormBudgetDept_Pick";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "主辦機關挑選";
		base.Load += new System.EventHandler(FormBudgetDept_Pick_Load);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormBudgetDept_Pick_FormClosing);
		this.panel2.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridMainUnit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.toolbarManager).EndInit();
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

	public FormBudgetDept_Pick()
	{
		InitializeComponent();
	}

	private void FormBudgetDept_Pick_Load(object sender, EventArgs e)
	{
		BindToGrid();
		LoadingScreen();
	}

	private void BindToGrid()
	{
		MainUnit mainUnit = new MainUnit();
		DataSet dsMainUnit = mainUnit.GetAllMainUnit();
		DataTable dtMainUnit = dsMainUnit.Tables[0];
		gridMainUnit.Rows.Count = dtMainUnit.Rows.Count + 1;
		for (int i = 0; i < dtMainUnit.Rows.Count; i++)
		{
			gridMainUnit[i + 1, "MainCode"] = dtMainUnit.Rows[i]["mainCode"].ToString();
			gridMainUnit[i + 1, "MainName"] = dtMainUnit.Rows[i]["mainName"].ToString();
		}
		gridMainUnit.AutoSizeCols();
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("Dept_Pick", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("Dept_Pick", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("Dept_Pick", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("Dept_Pick", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("Dept_Pick", "PK_Height"));
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

	private void btnOK_Click(object sender, EventArgs e)
	{
		Form ActiveForm = base.Owner.ActiveMdiChild;
		if (FormOwner == "FormSys_G")
		{
			if (ActiveForm is frmSysMaintain)
			{
				(ActiveForm as frmSysMaintain)._MainCode_G = gridMainUnit[gridMainUnit.Row, "MainCode"].ToString();
				(ActiveForm as frmSysMaintain)._MainName_G = gridMainUnit[gridMainUnit.Row, "MainName"].ToString();
			}
		}
		else if (FormOwner == "ProjectInfo")
		{
			(base.Owner as FormBudgetProjectInfo)._UserID = UserID;
			(base.Owner as FormBudgetProjectInfo)._MainCode = gridMainUnit[gridMainUnit.Row, "MainCode"].ToString();
			(base.Owner as FormBudgetProjectInfo)._MainName = gridMainUnit[gridMainUnit.Row, "MainName"].ToString();
		}
		else if (FormOwner == "FormSys_Z")
		{
			if (ActiveForm is frmSysMaintain)
			{
				(ActiveForm as frmSysMaintain)._MainCode_G = gridMainUnit[gridMainUnit.Row, "MainCode"].ToString();
				(ActiveForm as frmSysMaintain)._MainName_G = gridMainUnit[gridMainUnit.Row, "MainName"].ToString();
			}
		}
		else if (FormOwner == "OptionMain")
		{
			(base.Owner as FormBDGT_OptionMain)._UserID = UserID;
			(base.Owner as FormBDGT_OptionMain)._MainCode = gridMainUnit[gridMainUnit.Row, "MainCode"].ToString();
			(base.Owner as FormBDGT_OptionMain)._MainName = gridMainUnit[gridMainUnit.Row, "MainName"].ToString();
		}
	}

	private void toolbarManager_ToolClick(object sender, ToolClickEventArgs e)
	{
		if (e.Tool.Key == "Go")
		{
			DoSearch();
		}
	}

	private void DoSearch()
	{
		int searchStartRow = gridMainUnit.Row + 1;
		string searchText = ((ComboBoxTool)toolbarManager.Tools["ddlSearchList"]).Text.Trim();
		if (!CommonMethods.CheckValidString(searchText) || searchText.Trim() == string.Empty)
		{
			return;
		}
		if (lastSearchKeyWord != searchText.Trim())
		{
			searchStartRow = 1;
			lastSearchKeyWord = searchText.Trim();
		}
		else
		{
			searchStartRow = gridMainUnit.Row + 1;
		}
		for (int i = searchStartRow; i < gridMainUnit.Rows.Count; i++)
		{
			for (int j = 0; j < gridMainUnit.Cols.Count; j++)
			{
				if (gridMainUnit[i, j] != null && gridMainUnit[i, j].ToString().Contains(searchText))
				{
					gridMainUnit.Row = i;
					AddSearchKeywordList();
					return;
				}
			}
		}
	}

	private void AddSearchKeywordList()
	{
		ValueListItemsCollection searchKeyworkList = ((ComboBoxTool)toolbarManager.Tools["ddlSearchList"]).ValueList.ValueListItems;
		for (int i = 0; i < searchKeyworkList.Count; i++)
		{
			if (searchKeyworkList[i].DisplayText.Trim() == lastSearchKeyWord.Trim())
			{
				return;
			}
		}
		searchKeyworkList.Add(lastSearchKeyWord, lastSearchKeyWord);
	}

	private void toolbarManager_ToolKeyDown(object sender, ToolKeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && e.Tool.Key == "ddlSearchList")
		{
			DoSearch();
		}
	}

	private void gridMainUnit_DoubleClick(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
		btnOK_Click(this, EventArgs.Empty);
		Close();
	}

	private void toolbarManager_BeforeToolbarListDropdown(object sender, BeforeToolbarListDropdownEventArgs e)
	{
		e.Cancel = true;
	}

	private void FormBudgetDept_Pick_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("Dept_Pick", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("Dept_Pick", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("Dept_Pick", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("Dept_Pick", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("Dept_Pick", "WindowState", base.WindowState.ToString());
	}
}
