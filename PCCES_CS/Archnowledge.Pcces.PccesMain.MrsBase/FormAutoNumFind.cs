using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.PccesMain.ArchControls;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.MrsBase;

public class FormAutoNumFind : Form
{
	private const string CallFormHelp = "FormAutoNumFind";

	private Panel panel1;

	private UltraButton ultraButton3;

	private Panel panel2;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Bottom;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Right;

	private Panel panel3;

	private UltraLabel ultraLabel1;

	public GridMrsBase GridUnit1;

	private UltraToolbarsManager ultraToolbarsManager1;

	private IContainer components;

	private DataTable DT1 = new DataTable();

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.MrsBase.FormAutoNumFind));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool1");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool1 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool2 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Popup1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraButton3 = new Infragistics.Win.Misc.UltraButton();
		this.panel2 = new System.Windows.Forms.Panel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this.GridUnit1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this._FormSys_B_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.panel3 = new System.Windows.Forms.Panel();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).BeginInit();
		this.panel3.SuspendLayout();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.ultraButton3);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 291);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(286, 36);
		this.panel1.TabIndex = 11;
		this.ultraButton3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		appearance1.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance1.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton3.Appearance = appearance1;
		this.ultraButton3.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton3.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ultraButton3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton3.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton3.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton3.Location = new System.Drawing.Point(190, 4);
		this.ultraButton3.Name = "ultraButton3";
		this.ultraButton3.ShowFocusRect = false;
		this.ultraButton3.ShowOutline = false;
		this.ultraButton3.Size = new System.Drawing.Size(90, 28);
		this.ultraButton3.SupportThemes = false;
		this.ultraButton3.TabIndex = 6;
		this.ultraButton3.Text = "結  束";
		this.ultraButton3.Click += new System.EventHandler(ultraButton3_Click);
		this.panel2.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel2.Controls.Add(this.ultraLabel1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 27);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(286, 33);
		this.panel2.TabIndex = 12;
		appearance2.ForeColor = System.Drawing.Color.White;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance2;
		this.ultraLabel1.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(100, 33);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "查詢結果:";
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
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { labelTool1, comboBoxTool1, buttonTool1 });
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[1] { ultraToolbar1 });
		appearance8.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance8.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance8;
		appearance9.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance9.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance9.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance9;
		appearance10.Image = resources.GetObject("appearance10.Image");
		buttonTool2.SharedProps.AppearancesSmall.Appearance = appearance10;
		buttonTool2.SharedProps.Caption = "刪除";
		buttonTool2.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool2.SharedProps.Shortcut = System.Windows.Forms.Shortcut.Del;
		labelTool2.SharedProps.Caption = "關鍵字:";
		labelTool2.SharedProps.CustomizerCaption = "輸入想查詢規則表內的關鍵字";
		labelTool2.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		labelTool2.SharedProps.ToolTipText = "輸入想查詢規則表內的關鍵字";
		comboBoxTool2.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool2.SharedProps.Caption = "輸入關鍵字";
		comboBoxTool2.SharedProps.CustomizerCaption = "輸入想查詢規則表內的關鍵字";
		comboBoxTool2.SharedProps.ToolTipText = "輸入想查詢規則表內的關鍵字";
		comboBoxTool2.SharedProps.Width = 200;
		comboBoxTool2.ValueList = valueList1;
		appearance11.Image = resources.GetObject("appearance11.Image");
		buttonTool3.SharedProps.AppearancesSmall.Appearance = appearance11;
		buttonTool3.SharedProps.Caption = "Go";
		popupMenuTool1.SharedProps.Caption = "右鍵功能表";
		popupMenuTool1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[1] { buttonTool4 });
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[5] { buttonTool2, labelTool2, comboBoxTool2, buttonTool3, popupMenuTool1 });
		this.ultraToolbarsManager1.ToolKeyPress += new Infragistics.Win.UltraWinToolbars.ToolKeyPressEventHandler(ultraToolbarsManager1_ToolKeyPress);
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this.GridUnit1._ExcelFileName = "";
		this.GridUnit1._ExcelSheeName = "";
		this.GridUnit1._IsOpenExcelAfterExport = false;
		this.GridUnit1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.GridUnit1.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
		this.GridUnit1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.GridUnit1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridUnit1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.GridUnit1.ColumnInfo = resources.GetString("GridUnit1.ColumnInfo");
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
		this.GridUnit1.Size = new System.Drawing.Size(286, 231);
		this.GridUnit1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("GridUnit1.Styles"));
		this.GridUnit1.TabIndex = 8;
		this.GridUnit1.UndoMax = 10;
		this.GridUnit1.Click += new System.EventHandler(GridUnit1_Click);
		this._FormSys_B_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormSys_B_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormSys_B_Toolbars_Dock_Area_Top.Name = "_FormSys_B_Toolbars_Dock_Area_Top";
		this._FormSys_B_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(286, 27);
		this._FormSys_B_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 327);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Name = "_FormSys_B_Toolbars_Dock_Area_Bottom";
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(286, 0);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormSys_B_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 27);
		this._FormSys_B_Toolbars_Dock_Area_Left.Name = "_FormSys_B_Toolbars_Dock_Area_Left";
		this._FormSys_B_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 300);
		this._FormSys_B_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormSys_B_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(286, 27);
		this._FormSys_B_Toolbars_Dock_Area_Right.Name = "_FormSys_B_Toolbars_Dock_Area_Right";
		this._FormSys_B_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 300);
		this._FormSys_B_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this.panel3.Controls.Add(this.GridUnit1);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 60);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(286, 231);
		this.panel3.TabIndex = 21;
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.ultraButton3;
		base.ClientSize = new System.Drawing.Size(286, 327);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Bottom);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormAutoNumFind";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "自動編碼【關鍵字查詢】";
		base.Load += new System.EventHandler(FormAutoNumFind_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormAutoNumFind_KeyDown);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).EndInit();
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

	public FormAutoNumFind()
	{
		InitializeComponent();
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		string key = e.Tool.Key;
		if (key != null && key == "mnu_Go")
		{
			Do_ToolBarFind();
		}
	}

	private void LoadData(string KeyWord)
	{
		AutoNum autoNum = new AutoNum();
		DataSet dsAutoNum = autoNum.GetAutoNumByKeyword(KeyWord);
		DataRowCollection AutoNumRows = dsAutoNum.Tables["AutoNum"].Rows;
		if (AutoNumRows.Count > 0)
		{
			GridUnit1.Rows.Count = AutoNumRows.Count + 1;
			for (int i = 0; i < AutoNumRows.Count; i++)
			{
				GridUnit1[i + 1, "ChapCode"] = AutoNumRows[i]["ChapCode"].ToString().Trim();
				GridUnit1[i + 1, "cName"] = AutoNumRows[i]["cName"].ToString().Trim();
			}
		}
		if (dsAutoNum.Tables["AutoNumB_12_L"].Rows.Count > 0)
		{
			int iRowStart = 1;
			if (AutoNumRows.Count > 0)
			{
				GridUnit1.Rows.Count += dsAutoNum.Tables["AutoNumB_12_L"].Rows.Count;
				iRowStart = AutoNumRows.Count + 1;
			}
			else
			{
				iRowStart = 1;
				GridUnit1.Rows.Count = dsAutoNum.Tables["AutoNumB_12_L"].Rows.Count + 1;
			}
			GridUnit1.Cols["Code"].Visible = true;
			GridUnit1.Cols["Content"].Visible = true;
			GridUnit1.Cols["commonName"].Visible = true;
			for (int i = 0; i < dsAutoNum.Tables["AutoNumB_12_L"].Rows.Count; i++)
			{
				GridUnit1[i + iRowStart, "ChapCode"] = dsAutoNum.Tables[1].Rows[i]["ChapCode"].ToString().Trim();
				GridUnit1[i + iRowStart, "cName"] = dsAutoNum.Tables[1].Rows[i]["cName"].ToString().Trim();
				GridUnit1[i + iRowStart, "Code"] = dsAutoNum.Tables[1].Rows[i]["Code"].ToString().Trim();
				GridUnit1[i + iRowStart, "Content"] = dsAutoNum.Tables[1].Rows[i]["Content"].ToString().Trim();
				GridUnit1[i + iRowStart, "commonName"] = dsAutoNum.Tables[1].Rows[i]["commonName"].ToString().Trim();
			}
		}
		if (AutoNumRows.Count == 0 && dsAutoNum.Tables["AutoNumB_12_L"].Rows.Count == 0)
		{
			MessageBox.Show(this, "找不到資料", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void Do_ToolBarFind()
	{
		string sSearchText = ((ComboBoxTool)ultraToolbarsManager1.Tools["mnu_Cbo1"]).Text.Trim();
		if (!CommonMethods.CheckValidString(sSearchText))
		{
			return;
		}
		if (sSearchText.Trim() == "")
		{
			MessageBox.Show(this, "請先輸入要查詢的關鍵字", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}
		LoadData(sSearchText);
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
	}

	private void ultraToolbarsManager1_ToolKeyPress(object sender, ToolKeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && e.Tool.Key == "mnu_Cbo1")
		{
			Do_ToolBarFind();
		}
	}

	private void ultraButton3_Click(object sender, EventArgs e)
	{
		Hide();
		SendToBack();
		Close();
	}

	private void GridUnit1_Click(object sender, EventArgs e)
	{
		if (GridUnit1.Row >= 1)
		{
			Cursor = Cursors.WaitCursor;
			string sKeyCode = GridUnit1[GridUnit1.Row, "ChapCode"].ToString().Trim();
			string sKeyword = ((TextBoxTool)ultraToolbarsManager1.Tools["mnu_Cbo1"]).Text;
			(base.Owner as FormAutoNum).GetAutoNumB_By_Find(sKeyCode, sKeyword);
			Cursor = Cursors.Default;
		}
	}

	private void FormAutoNumFind_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormAutoNumFind");
		}
	}

	private void FormAutoNumFind_Load(object sender, EventArgs e)
	{
	}
}
