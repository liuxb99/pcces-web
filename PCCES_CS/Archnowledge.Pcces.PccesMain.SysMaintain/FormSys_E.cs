using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.SysMaintain;

public class FormSys_E : UserControl
{
	private DataTable DT1 = new DataTable();

	private DataTable DT_Left = new DataTable();

	private string LeftData_Load_Status = "INI";

	private int iAuthorityMSG_Count = 0;

	private string userID;

	private UltraToolbarsManager ultraToolbarsManager1;

	private IContainer components;

	private Panel panel1;

	private Splitter splitter1;

	private Panel panel2;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private Panel panel3;

	private UltraLabel ultraLabel3;

	private UltraTextEditor txtNewWord;

	private UltraButton ultraButton1;

	private Panel pnl_P2;

	private Panel panel4;

	public GridMrsBase GridUnit1;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Bottom;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Right;

	private UltraLabel lblTypeStr;

	public GridMrsBase GridLeft;

	private UltraStatusBar ultraStatusBar1;

	private ImageList imageList2;

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

	public FormSys_E()
	{
		InitializeComponent();
		CellStyle cs1 = GridUnit1.Styles.Add("EditMode");
		cs1.DataType = typeof(Image);
		cs1.ImageAlign = ImageAlignEnum.RightCenter;
	}

	private void FormSys_E_Load(object sender, EventArgs e)
	{
		ReloadData();
	}

	public void ReloadData()
	{
		lblTypeStr.Text = "";
		GridUnit1.Cols["TypeStr"].Caption = " ";
		LoadLeftData();
		if (GridLeft.Rows.Count > 0)
		{
			LoadRightData();
			GridLeft.Select();
		}
	}

	private void LoadLeftData()
	{
		if (GridLeft.Rows.Count == 50)
		{
			bool IsPwrSet = ArchConvert.Obj2Bool(SysConfig.SysEnablePwrSet);
			GridLeft.Rows.Count = 0;
			GridLeft.AddItem("cName\t項次及說明(主項大類中文)");
			GridLeft.AddItem("eName\t項次及說明(主項大類英文)");
			GridLeft.AddItem("cUnit\t單位(中文)");
			GridLeft.AddItem("eUnit\t單位(英文)");
			GridLeft.AddItem("RptFooter\t報表簽核欄");
			GridLeft.AddItem("Class\t類別");
			if (IsPwrSet)
			{
				GridLeft.AddItem("PwrSet\t發包權限");
			}
			GridLeft.AddItem("BudgetChangeResponsibility\t責任歸屬");
			LeftData_Load_Status = "ACT";
		}
	}

	private void GridLeft_AfterSelChange(object sender, RangeEventArgs e)
	{
		if (!(LeftData_Load_Status != "ACT"))
		{
			LoadRightData();
		}
	}

	private void LoadRightData()
	{
		GridUnit1.Cols["TypeStr"].Caption = " " + GridLeft[GridLeft.Row, "TypeStr"].ToString();
		lblTypeStr.Text = GridLeft[GridLeft.Row, "TypeStr"].ToString();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("(UserDefind_Show) 顯示常用字串資料");
		UserDefind UserCom = new UserDefind(aArr);
		UserCom.IsEnableCOMS = SysConfig.SysComsEnable;
		DT1 = UserCom.ListItem(GridLeft[GridLeft.Row, "TypeCode"].ToString().Trim());
		ultraStatusBar1.Panels[0].Text = "資料筆數：" + DT1.Rows.Count;
		BindToGrid();
	}

	private void BindToGrid()
	{
		GridUnit1.Rows.Count = DT1.Rows.Count + 1;
		for (int i = 0; i < DT1.Rows.Count; i++)
		{
			GridUnit1[i + 1, "sNo"] = DT1.Rows[i]["sNo"].ToString().Trim();
			GridUnit1[i + 1, "TypeStr"] = DT1.Rows[i]["cString"].ToString().Trim();
		}
		SetColsEditSymbol(ref GridUnit1);
		GridUnit1.AutoSizeCols();
	}

	private void SetColsEditSymbol(ref GridMrsBase C1FlexGrid)
	{
		for (int i = 1; i < C1FlexGrid.Cols.Count; i++)
		{
			if (C1FlexGrid.Cols[i].AllowEditing)
			{
				CellRange rg = C1FlexGrid.GetCellRange(0, i);
				rg.Style = C1FlexGrid.Styles["EditMode"];
				rg.Image = imageList2.Images[2];
			}
		}
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		AddCommonString();
	}

	private void AddCommonString()
	{
		if (!DBClass.ChkAuthority(userID, "F00100040001"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F00100040001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (GridLeft.Row < 0 || GridLeft[GridLeft.Row, "TypeStr"].ToString().Trim() == "")
		{
			string sWarning = "請先選定一個字串類別!!";
			MessageBox.Show(this, sWarning, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (txtNewWord.Text.Trim() == "")
		{
			string sWarning = "無法新增空白字串!!";
			MessageBox.Show(this, sWarning, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtNewWord.Focus();
			return;
		}
		string sKind = GridLeft[GridLeft.Row, "TypeCode"].ToString().Trim();
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = "PccAdmin";
		int iCount = PubTools.Str2Int(DBCLS.GetUserDefine_String("Select Count(*) as iCount From UserDefind Where Kind='eName' And cString ='" + sKind + "' ", "iCount"));
		if (iCount > 0)
		{
			MessageBox.Show(this, "已有相同字串存在。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("(UserDefind_Show) 新增常用字串資料");
		UserDefind UserCom = new UserDefind(aArr);
		UserCom.IsEnableCOMS = SysConfig.SysComsEnable;
		UserCom.ps_sNo = UserCom.GetMaxSno(sKind).ToString();
		UserCom.ps_Kind = GridLeft[GridLeft.Row, "TypeCode"].ToString().Trim();
		UserCom.ps_cString = txtNewWord.Text.Trim();
		UserCom.InseItem();
		DT1 = UserCom.ListItem(GridLeft[GridLeft.Row, "TypeCode"].ToString().Trim());
		ultraStatusBar1.Panels[0].Text = "資料筆數：" + DT1.Rows.Count;
		txtNewWord.Text = string.Empty;
		BindToGrid();
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		if (!DBClass.ChkAuthority(userID, "F00100030002"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F00100030002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			if (!(e.Tool.Key == "mnuDelete"))
			{
				return;
			}
			string sQues = "是否確定要刪除 ?";
			if (MessageBox.Show(this, sQues, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				ArrayList aArr = new ArrayList();
				aArr.Clear();
				aArr.Add(userID);
				aArr.Add("常用字串資料--刪除");
				UserDefind UserCom = new UserDefind(aArr);
				UserCom.IsEnableCOMS = SysConfig.SysComsEnable;
				for (int i = GridUnit1.Rows.Count - 1; i >= 1; i--)
				{
					if (GridUnit1.Rows[i].Selected)
					{
						UserCom.DeleItem(GridUnit1[i, "sNo"].ToString().Trim(), GridLeft[GridLeft.Row, "TypeCode"].ToString().Trim());
					}
				}
				LoadRightData();
			}
			GridUnit1.RowSel = -1;
		}
	}

	private void txtNewWord_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			AddCommonString();
		}
	}

	private void GridUnit1_AfterEdit(object sender, RowColEventArgs e)
	{
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = "PccAdmin";
		string sKind = GridLeft[GridLeft.Row, "TypeCode"].ToString().Trim();
		string sSQL = "Update UserDefind Set cString ='" + GridUnit1[e.Row, "TypeStr"].ToString().Trim() + "'  Where Kind = '" + sKind + "' And sno = " + GridUnit1[e.Row, "sNo"].ToString();
		DBCLS.ExecuteCommand(sSQL);
	}

	private void GridUnit1_BeforeEdit(object sender, RowColEventArgs e)
	{
		if (GridUnit1.Cols[e.Col].Name == "TypeStr" && !DBClass.ChkAuthority(userID, "F001000400030001"))
		{
			iAuthorityMSG_Count++;
			if (iAuthorityMSG_Count <= 1)
			{
				MessageBox.Show(this, DBClass.GetFuncName("F001000400030001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			iAuthorityMSG_Count = 0;
			e.Cancel = true;
			GridUnit1.Col = 0;
		}
	}

	private void GridUnit1_MouseDown(object sender, MouseEventArgs e)
	{
		int rowIndex = GridUnit1.MouseRow;
		int colIndex = GridUnit1.MouseCol;
		GridUnit1.Row = GridUnit1.MouseRow;
		if (GridUnit1.Row <= 0 || rowIndex <= 0 || colIndex <= 0)
		{
			ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = false;
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = true;
		}
	}

	private void ultraToolbarsManager1_BeforeToolbarListDropdown(object sender, BeforeToolbarListDropdownEventArgs e)
	{
		e.Cancel = true;
	}

	private void txtNewWord_Validating(object sender, CancelEventArgs e)
	{
		if (!CommonMethods.CheckValidString((sender as UltraTextEditor).Text))
		{
			e.Cancel = true;
		}
		if (!CommonMethods.IsStrByteLenValid(txtNewWord.Text, 200))
		{
			MessageBox.Show(this, "字串的長度不可超過 200 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtNewWord.Focus();
		}
	}

	private void txtNewWord_Enter(object sender, EventArgs e)
	{
		((ButtonTool)ultraToolbarsManager1.Tools["mnuDelete"]).SharedProps.Shortcut = Shortcut.None;
	}

	private void txtNewWord_Leave(object sender, EventArgs e)
	{
		((ButtonTool)ultraToolbarsManager1.Tools["mnuDelete"]).SharedProps.Shortcut = Shortcut.Del;
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.SysMaintain.FormSys_E));
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool1");
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool1 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Popup1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.GridLeft = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.splitter1 = new System.Windows.Forms.Splitter();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.GridUnit1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this._FormSys_B_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this._FormSys_B_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.pnl_P2 = new System.Windows.Forms.Panel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.txtNewWord = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lblTypeStr = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridLeft).BeginInit();
		this.panel2.SuspendLayout();
		this.panel4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtNewWord).BeginInit();
		base.SuspendLayout();
		this.panel1.Controls.Add(this.GridLeft);
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(160, 428);
		this.panel1.TabIndex = 0;
		this.GridLeft._ExcelFileName = "";
		this.GridLeft._ExcelSheeName = "";
		this.GridLeft._IsOpenExcelAfterExport = false;
		this.GridLeft.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.GridLeft.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridLeft.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.GridLeft.ColumnInfo = resources.GetString("GridLeft.ColumnInfo");
		this.GridLeft.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridLeft.ExtendLastCol = true;
		this.GridLeft.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridLeft.ForeColor = System.Drawing.Color.Black;
		this.GridLeft.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.GridLeft.IsProcessUndo = false;
		this.GridLeft.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.GridLeft.Location = new System.Drawing.Point(0, 28);
		this.GridLeft.Name = "GridLeft";
		this.GridLeft.Rows.Fixed = 0;
		this.GridLeft.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.GridLeft.ShowCursor = true;
		this.GridLeft.ShowToolTipOnNarrowColumn = true;
		this.GridLeft.Size = new System.Drawing.Size(160, 400);
		this.GridLeft.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("GridLeft.Styles"));
		this.GridLeft.TabIndex = 9;
		this.GridLeft.UndoMax = 10;
		this.GridLeft.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(GridLeft_AfterSelChange);
		appearance1.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance1.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance1.ForeColor = System.Drawing.Color.White;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel1.Appearance = appearance1;
		this.ultraLabel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel1.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(160, 28);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = " 常用字串類別";
		this.splitter1.Location = new System.Drawing.Point(160, 0);
		this.splitter1.Name = "splitter1";
		this.splitter1.Size = new System.Drawing.Size(5, 428);
		this.splitter1.TabIndex = 1;
		this.splitter1.TabStop = false;
		this.panel2.Controls.Add(this.panel4);
		this.panel2.Controls.Add(this.pnl_P2);
		this.panel2.Controls.Add(this.panel3);
		this.panel2.Controls.Add(this.ultraLabel2);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(165, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(435, 428);
		this.panel2.TabIndex = 2;
		this.panel4.Controls.Add(this.GridUnit1);
		this.panel4.Controls.Add(this.ultraStatusBar1);
		this.panel4.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Bottom);
		this.panel4.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Left);
		this.panel4.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Right);
		this.panel4.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Top);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel4.Location = new System.Drawing.Point(0, 89);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(435, 339);
		this.panel4.TabIndex = 4;
		this.GridUnit1._ExcelFileName = "";
		this.GridUnit1._ExcelSheeName = "";
		this.GridUnit1._IsOpenExcelAfterExport = false;
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
		this.GridUnit1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.GridUnit1.ShowCursor = true;
		this.GridUnit1.ShowToolTipOnNarrowColumn = true;
		this.GridUnit1.Size = new System.Drawing.Size(435, 316);
		this.GridUnit1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("GridUnit1.Styles"));
		this.GridUnit1.TabIndex = 8;
		this.GridUnit1.UndoMax = 10;
		this.GridUnit1.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(GridUnit1_AfterEdit);
		this.GridUnit1.MouseDown += new System.Windows.Forms.MouseEventHandler(GridUnit1_MouseDown);
		this.GridUnit1.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(GridUnit1_BeforeEdit);
		appearance11.BackColor = System.Drawing.SystemColors.Control;
		appearance11.FontData.SizeInPoints = 11f;
		this.ultraStatusBar1.Appearance = appearance11;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 316);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Text = "資料筆數:";
		ultraStatusPanel1.Width = 200;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		appearance12.TextHAlign = Infragistics.Win.HAlign.Right;
		ultraStatusPanel3.Appearance = appearance12;
		ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel3.Text = "客服電話:(02)2708-8090";
		ultraStatusPanel3.Width = 200;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[3] { ultraStatusPanel1, ultraStatusPanel2, ultraStatusPanel3 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(435, 23);
		this.ultraStatusBar1.SupportThemes = false;
		this.ultraStatusBar1.TabIndex = 15;
		this.ultraStatusBar1.Text = "ultraStatusBar1";
		this._FormSys_B_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 339);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Name = "_FormSys_B_Toolbars_Dock_Area_Bottom";
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(435, 0);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		appearance13.FontData.Name = "Arial";
		appearance13.FontData.SizeInPoints = 9f;
		this.ultraToolbarsManager1.Appearance = appearance13;
		appearance14.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance14.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.DockAreaAppearance = appearance14;
		this.ultraToolbarsManager1.DockWithinContainer = this;
		this.ultraToolbarsManager1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraToolbarsManager1.LockToolbars = true;
		appearance15.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance15.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance15.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.MenuSettings.HotTrackAppearance = appearance15;
		appearance16.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance16.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsManager1.MenuSettings.IconAreaAppearance = appearance16;
		appearance17.BackColor = System.Drawing.Color.White;
		appearance17.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.MenuSettings.ToolAppearance = appearance17;
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
		ultraToolbar1.Settings.AllowDockTop = Infragistics.Win.DefaultableBoolean.True;
		ultraToolbar1.Text = "Tool1";
		ultraToolbar1.Visible = false;
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[1] { ultraToolbar1 });
		appearance18.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance18.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance18;
		appearance19.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance19.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance19.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance19;
		appearance20.Image = resources.GetObject("appearance9.Image");
		buttonTool1.SharedProps.AppearancesSmall.Appearance = appearance20;
		buttonTool1.SharedProps.Caption = "刪除";
		buttonTool1.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool1.SharedProps.Shortcut = System.Windows.Forms.Shortcut.Del;
		labelTool1.SharedProps.Caption = "尋找:";
		labelTool1.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool1.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool1.SharedProps.Caption = "輸入關鍵字";
		comboBoxTool1.SharedProps.Width = 200;
		comboBoxTool1.ValueList = valueList1;
		appearance21.Image = resources.GetObject("appearance10.Image");
		buttonTool2.SharedProps.AppearancesSmall.Appearance = appearance21;
		buttonTool2.SharedProps.Caption = "Go";
		popupMenuTool1.SharedProps.Caption = "右鍵功能表";
		popupMenuTool1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[1] { buttonTool3 });
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[5] { buttonTool1, labelTool1, comboBoxTool1, buttonTool2, popupMenuTool1 });
		this.ultraToolbarsManager1.BeforeToolbarListDropdown += new Infragistics.Win.UltraWinToolbars.BeforeToolbarListDropdownEventHandler(ultraToolbarsManager1_BeforeToolbarListDropdown);
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this._FormSys_B_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormSys_B_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 0);
		this._FormSys_B_Toolbars_Dock_Area_Left.Name = "_FormSys_B_Toolbars_Dock_Area_Left";
		this._FormSys_B_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 339);
		this._FormSys_B_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormSys_B_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(435, 0);
		this._FormSys_B_Toolbars_Dock_Area_Right.Name = "_FormSys_B_Toolbars_Dock_Area_Right";
		this._FormSys_B_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 339);
		this._FormSys_B_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormSys_B_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormSys_B_Toolbars_Dock_Area_Top.Name = "_FormSys_B_Toolbars_Dock_Area_Top";
		this._FormSys_B_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(435, 0);
		this._FormSys_B_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this.pnl_P2.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnl_P2.Location = new System.Drawing.Point(0, 84);
		this.pnl_P2.Name = "pnl_P2";
		this.pnl_P2.Size = new System.Drawing.Size(435, 5);
		this.pnl_P2.TabIndex = 3;
		this.panel3.Controls.Add(this.ultraButton1);
		this.panel3.Controls.Add(this.txtNewWord);
		this.panel3.Controls.Add(this.lblTypeStr);
		this.panel3.Controls.Add(this.ultraLabel3);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel3.Location = new System.Drawing.Point(0, 28);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(435, 56);
		this.panel3.TabIndex = 2;
		this.ultraButton1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance22.Image = resources.GetObject("appearance13.Image");
		appearance22.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraButton1.Appearance = appearance22;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton1.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton1.Location = new System.Drawing.Point(352, 27);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(75, 27);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 9;
		this.ultraButton1.Text = "新增";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		this.txtNewWord.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtNewWord.AutoSize = true;
		this.txtNewWord.Location = new System.Drawing.Point(10, 28);
		this.txtNewWord.MaxLength = 200;
		this.txtNewWord.Name = "txtNewWord";
		this.txtNewWord.Size = new System.Drawing.Size(342, 21);
		this.txtNewWord.TabIndex = 3;
		this.txtNewWord.Validating += new System.ComponentModel.CancelEventHandler(txtNewWord_Validating);
		this.txtNewWord.Leave += new System.EventHandler(txtNewWord_Leave);
		this.txtNewWord.Enter += new System.EventHandler(txtNewWord_Enter);
		this.txtNewWord.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNewWord_KeyPress);
		appearance23.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblTypeStr.Appearance = appearance23;
		this.lblTypeStr.Location = new System.Drawing.Point(40, 7);
		this.lblTypeStr.Name = "lblTypeStr";
		this.lblTypeStr.Size = new System.Drawing.Size(369, 23);
		this.lblTypeStr.TabIndex = 2;
		this.lblTypeStr.Text = "[新增]";
		appearance24.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance24;
		this.ultraLabel3.Location = new System.Drawing.Point(8, 7);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(36, 23);
		this.ultraLabel3.TabIndex = 1;
		this.ultraLabel3.Text = "新增";
		appearance25.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance25.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance25.ForeColor = System.Drawing.Color.White;
		appearance25.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel2.Appearance = appearance25;
		this.ultraLabel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel2.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(435, 28);
		this.ultraLabel2.TabIndex = 1;
		this.ultraLabel2.Text = " 常用字串內容設定";
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Magenta;
		this.imageList2.Images.SetKeyName(0, "");
		this.imageList2.Images.SetKeyName(1, "");
		this.imageList2.Images.SetKeyName(2, "");
		this.imageList2.Images.SetKeyName(3, "");
		this.imageList2.Images.SetKeyName(4, "");
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.splitter1);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Name = "FormSys_E";
		base.Size = new System.Drawing.Size(600, 428);
		base.Load += new System.EventHandler(FormSys_E_Load);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridLeft).EndInit();
		this.panel2.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtNewWord).EndInit();
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
}
