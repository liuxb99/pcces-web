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
using AxThreed;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.Compare;

public class FormCompareMrs : Form
{
	private UltraToolbarsManager ultraToolbarsManager1;

	private IContainer components;

	private string[] ls_Val = new string[10];

	private string[] ls_skind = new string[10];

	private bool F_IsRunCompare = false;

	private int F_MainQty = 0;

	private int F_MainCst = 0;

	private int F_MainAmt = 0;

	private int F_AnaQty = 0;

	private int F_AnaCst = 0;

	private int F_AnaAmt = 0;

	private bool F_HasRegistered;

	private string F_UserID;

	private string F_UserName = "";

	private string F_FunctionName = "CompareMrs";

	private string F_ServerName = "localhost";

	private LeftPanelMode MidPanelMode = LeftPanelMode.Open;

	private int GridCols = 15;

	private object[,] GridColsSquence;

	private PccesFormAction F_ActionName = PccesFormAction.BUD;

	private DataTable DT1 = new DataTable();

	private DataTable DT_DP = new DataTable();

	private string F_KeyWord = "";

	private Panel LeftPanel;

	private OnlineList onlineList1;

	public FunctionButtons functionButtons1;

	private Panel pnl_spliter;

	private UltraButton Btn_Splt;

	private AxSSPanel ssp_Lower;

	private AxSSPanel ssp_Bottom;

	private AxSSPanel ssp_Upper;

	private AxSSPanel ssp_Top;

	private Panel panel1;

	private UltraLabel ultraLabel10;

	private UltraLabel ultraLabel1;

	private Panel FormCompareMrs_Fill_Panel;

	private Panel panel4;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel3;

	private UltraStatusBar ultraStatusBar1;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Bottom;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Right;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel5;

	private UltraComboEditor dpBase;

	private UltraLabel ultraLabel9;

	private UltraCheckEditor chkMrs;

	private ImageList iglst_splt_Btn;

	private PictureBox pictureBox1;

	private PictureBox pictureBox2;

	private UltraButton BtnExecute;

	private System.Windows.Forms.ToolTip toolTip1;

	private GridBudget gridBudget1;

	private Panel PNL_UPPER;

	private ImageList imageList2;

	private ImageList iglst_splt_Btn2;

	private Panel Pnl_Spliter_Hor;

	private UltraButton Btn_SpltHor;

	private AxSSPanel ssp_Righter;

	private AxSSPanel ssp_Right;

	private AxSSPanel ssp_Lefter;

	private AxSSPanel ssp_Left;

	private PictureBox pictureBox3;

	private UltraLabel ultraLabel6;

	private C1FlexGrid GridCmp;

	private UltraCheckEditor chkDiff;

	private PictureBox pictureBox4;

	private UltraLabel ultraLabel7;

	private UltraOptionSet Op1;

	private SaveFileDialog saveFileDialog1;

	private UltraCheckEditor chkPrice;

	public bool _HasRegistered
	{
		get
		{
			return F_HasRegistered;
		}
		set
		{
			F_HasRegistered = value;
		}
	}

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

	public string _UserName
	{
		get
		{
			return F_UserName;
		}
		set
		{
			F_UserName = value;
		}
	}

	public string _FunctionName
	{
		get
		{
			return F_FunctionName;
		}
		set
		{
			F_FunctionName = value;
		}
	}

	public string _ServerName
	{
		get
		{
			return F_ServerName;
		}
		set
		{
			F_ServerName = value;
		}
	}

	public FormCompareMrs()
	{
		InitializeComponent();
		GridCols = gridBudget1.Cols.Count;
		GridColsSquence = new object[GridCols, 10];
		CellStyle cs = gridBudget1.Styles.Add("img");
		cs.DataType = typeof(Image);
		HideCols(IsHide: true);
		RememberColsProps();
	}

	private void SettingDecimal()
	{
		DataTable DTDecimal = new DataTable();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add(CommonMethods.GetFormTypeTitle(FormType.Budget));
		PubDecimal dbDecimal = new PubDecimal(aArr);
		DTDecimal = dbDecimal.ListItem("", "");
		if (DTDecimal.Rows.Count > 0)
		{
			F_MainQty = Convert.ToInt32(DTDecimal.Rows[0]["itemQty"]);
			F_MainCst = Convert.ToInt32(DTDecimal.Rows[0]["itemCost"]);
			F_MainAmt = Convert.ToInt32(DTDecimal.Rows[0]["itemAmt"]);
			F_AnaQty = Convert.ToInt32(DTDecimal.Rows[0]["analysisQty"]);
			F_AnaCst = Convert.ToInt32(DTDecimal.Rows[0]["analysisCost"]);
			F_AnaAmt = Convert.ToInt32(DTDecimal.Rows[0]["analysisAmt"]);
		}
		else
		{
			F_MainQty = 3;
			F_MainCst = 0;
			F_MainAmt = 0;
			F_AnaQty = 3;
			F_AnaCst = 2;
			F_AnaAmt = 2;
		}
	}

	private void HideCols(bool IsHide)
	{
		if (IsHide)
		{
			gridBudget1.Cols["ProjectCode"].Visible = false;
			gridBudget1.Cols["PubCode"].Visible = false;
			gridBudget1.Cols["Analysis"].Visible = false;
		}
	}

	private void RememberColsProps()
	{
		for (int i = 0; i < GridCols; i++)
		{
			GridColsSquence[i, 0] = gridBudget1.Cols[i].Name;
			GridColsSquence[i, 1] = gridBudget1.Cols[i].Caption;
			GridColsSquence[i, 2] = gridBudget1.Cols[i].Width;
			if (gridBudget1.Cols[i].Name == "AnaImg")
			{
				GridColsSquence[i, 3] = typeof(Image);
			}
			else
			{
				GridColsSquence[i, 3] = gridBudget1.Cols[i].DataType;
			}
			GridColsSquence[i, 4] = gridBudget1.Cols[i].Visible;
			GridColsSquence[i, 5] = gridBudget1.Cols[i].Format;
			GridColsSquence[i, 6] = gridBudget1.Cols[i].AllowEditing;
			if ((object)gridBudget1.Cols[i].DataType == Type.GetType("System.Decimal"))
			{
				if (F_MainCst > 0)
				{
					GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_MainCst, '0');
				}
				else
				{
					GridColsSquence[i, 5] = "###,###,###,##0";
				}
			}
			GridColsSquence[i, 7] = gridBudget1.Cols[i].TextAlign;
			GridColsSquence[i, 8] = gridBudget1.Cols[i].AllowDragging;
			GridColsSquence[i, 9] = gridBudget1.Cols[i].AllowResizing;
		}
	}

	private void SetGridColumn()
	{
		for (int i = 0; i < GridCols; i++)
		{
			gridBudget1.Cols[i].Name = (string)GridColsSquence[i, 0];
			gridBudget1.Cols[i].Caption = (string)GridColsSquence[i, 1];
			gridBudget1.Cols[i].Width = (int)GridColsSquence[i, 2];
			gridBudget1.Cols[i].DataType = (Type)GridColsSquence[i, 3];
			gridBudget1.Cols[i].Visible = (bool)GridColsSquence[i, 4];
			gridBudget1.Cols[i].Format = (string)GridColsSquence[i, 5];
			gridBudget1.Cols[i].AllowEditing = (bool)GridColsSquence[i, 6];
			gridBudget1.Cols[i].TextAlign = (TextAlignEnum)GridColsSquence[i, 7];
			gridBudget1.Cols[i].AllowDragging = (bool)GridColsSquence[i, 8];
			gridBudget1.Cols[i].AllowResizing = (bool)GridColsSquence[i, 9];
		}
	}

	private void FormCompareMrs_Resize(object sender, EventArgs e)
	{
		int TotalH = pnl_spliter.Height;
		int iHeight = (TotalH - 3 - 3 - 57) / 2;
		ssp_Upper.Height = iHeight;
		ssp_Lower.Height = iHeight;
		int TotalW = Pnl_Spliter_Hor.Width;
		int iWidth = (TotalW - 3 - 3 - 57) / 2;
		ssp_Lefter.Width = iWidth;
		ssp_Righter.Width = iWidth;
	}

	private void Btn_SpltHor_Click(object sender, EventArgs e)
	{
		if (PNL_UPPER.Height == 0)
		{
			PNL_UPPER.Height = 220;
			MidPanelMode = LeftPanelMode.Open;
			Btn_SpltHor.Appearance.ImageBackground = iglst_splt_Btn2.Images[0];
			ultraToolbarsManager1.Tools["mnuOpenPanel"].SharedProps.Caption = "隱藏比對條件";
		}
		else
		{
			PNL_UPPER.Height = 0;
			MidPanelMode = LeftPanelMode.Close;
			Btn_SpltHor.Appearance.ImageBackground = iglst_splt_Btn2.Images[2];
			ultraToolbarsManager1.Tools["mnuOpenPanel"].SharedProps.Caption = "設定比對條件";
		}
	}

	private void Btn_Splt_Click(object sender, EventArgs e)
	{
		if (LeftPanel.Width == 0)
		{
			LeftPanel.Width = 160;
			Btn_Splt.Appearance.ImageBackground = iglst_splt_Btn.Images[0];
		}
		else
		{
			LeftPanel.Width = 0;
			Btn_Splt.Appearance.ImageBackground = iglst_splt_Btn.Images[2];
		}
		FormCompareMrs_Resize(this, EventArgs.Empty);
	}

	private void FormCompareMrs_Load(object sender, EventArgs e)
	{
		base.ParentForm.Text = "PCCES Win 4.3 【經費審查比對】";
		functionButtons1._UserID = F_UserID;
		functionButtons1._UserName = F_UserName;
		functionButtons1._ServerName = F_ServerName;
		functionButtons1._CurrOpenMode = FunctionOpenMode.Common;
		functionButtons1._ActiveFunction = "COMPAREMRS";
		onlineList1._UserID = F_UserID;
		onlineList1._UserName = F_UserName;
		onlineList1._ServerName = F_ServerName;
		onlineList1._FunctionName = F_FunctionName;
		onlineList1._HasRegistered = F_HasRegistered;
		onlineList1.Connect();
		SettingDecimal();
		ControlsClear();
		LoadData();
		BindToDropDown();
	}

	private void ControlsClear()
	{
		dpBase.Items.Clear();
		dpBase.Text = "";
		((ComboBoxTool)ultraToolbarsManager1.Tools["mnuCbo_Show"]).SelectedIndex = 0;
		((ComboBoxTool)ultraToolbarsManager1.Tools["List1"]).SelectedIndex = 1;
		((TextBoxTool)ultraToolbarsManager1.Tools["txtDiff"]).Text = "5";
	}

	private void LoadData()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(Chk_Cost1) 經費審查比對");
		if (F_ActionName == PccesFormAction.None)
		{
			Archnowledge.Pcces.BUDClass.Project ProjCom = new Archnowledge.Pcces.BUDClass.Project(tmp_AL1);
			ProjCom.ps_srckind = "BUD";
			DT_DP = ProjCom.ListItem();
			DT_DP.Columns.Add("srcKind", Type.GetType("System.String"));
			for (int i = 0; i < DT_DP.Rows.Count; i++)
			{
				DT_DP.Rows[i]["srcKind"] = "預";
			}
			ProjCom.ps_srckind = "BID";
			DataTable DT_TTMP = ProjCom.ListItem();
			DT_TTMP.Columns.Add("srcKind", Type.GetType("System.String"));
			for (int j = 0; j < DT_TTMP.Rows.Count; j++)
			{
				DataRow DR = DT_DP.NewRow();
				for (int i = 0; i < DT_TTMP.Columns.Count; i++)
				{
					if (DT_TTMP.Columns[i].ColumnName.ToUpper() == "SRCKIND")
					{
						DR["srcKind"] = "標";
					}
					else if (DT_DP.Columns.Contains(DT_TTMP.Columns[i].ColumnName))
					{
						DR[DT_TTMP.Columns[i].ColumnName] = DT_TTMP.Rows[j][DT_TTMP.Columns[i].ColumnName];
					}
				}
				DT_DP.Rows.Add(DR);
			}
		}
		else
		{
			Archnowledge.Pcces.BUDClass.Project ProjCom = new Archnowledge.Pcces.BUDClass.Project(tmp_AL1);
			ProjCom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			DT_DP = ProjCom.ListItem();
			DT_DP.Columns.Add("srcKind", Type.GetType("System.String"));
			for (int i = 0; i < DT_DP.Rows.Count; i++)
			{
				DT_DP.Rows[i]["srcKind"] = ((ProjCom.ps_srckind.ToUpper() == "BUD") ? "預" : "標");
			}
		}
	}

	private void BindToDropDown()
	{
		string sProjectCode = "";
		string sProjectNameC = "";
		string sSrcKind = "";
		dpBase.Items.Clear();
		GridCmp.Rows.Count = DT_DP.Rows.Count + 1;
		for (int i = 0; i < DT_DP.Rows.Count; i++)
		{
			sProjectCode = DT_DP.Rows[i]["projectCode"].ToString().Trim();
			sProjectNameC = DT_DP.Rows[i]["projectNameC"].ToString().Trim();
			sSrcKind = DT_DP.Rows[i]["srcKind"].ToString().Trim();
			dpBase.Items.Add(sProjectCode, "[" + sSrcKind + "](" + sProjectCode + ")" + sProjectNameC);
			GridCmp[i + 1, "Check"] = false;
			GridCmp[i + 1, "srcKind"] = sSrcKind;
			GridCmp[i + 1, "ProjectCode"] = sProjectCode;
			GridCmp[i + 1, "ProjectNameC"] = sProjectNameC;
		}
	}

	private void BtnExecute_Click(object sender, EventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		if (Do_Compare())
		{
			BindToGrid();
			ProcessCols();
			F_IsRunCompare = true;
		}
		Cursor = Cursors.Default;
	}

	private bool Do_Compare()
	{
		bool Ret = true;
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Clear();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(Chk_Cost1) 經費審查比對");
		if (dpBase.Value == null)
		{
			MessageBox.Show(this, "請先挑選基準標案", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		string ls_ValS = dpBase.Value.ToString().Trim();
		string ls_skindS = ((dpBase.Text.IndexOf("[標]") > -1) ? "BID" : "BUD");
		int iidx = -1;
		ls_Val[0] = (ls_Val[1] = (ls_Val[2] = (ls_Val[3] = (ls_Val[4] = (ls_Val[5] = (ls_Val[6] = (ls_Val[7] = (ls_Val[8] = (ls_Val[9] = "")))))))));
		ls_skind[0] = (ls_skind[1] = (ls_skind[2] = (ls_skind[3] = (ls_skind[4] = (ls_skind[5] = (ls_skind[6] = (ls_skind[7] = (ls_skind[8] = (ls_skind[9] = "")))))))));
		for (int i = 1; i < GridCmp.Rows.Count; i++)
		{
			if (iidx >= 9)
			{
				break;
			}
			if ((bool)GridCmp[i, "Check"])
			{
				iidx++;
				ls_Val[iidx] = GridCmp[i, "ProjectCode"].ToString();
				ls_skind[iidx] = ((GridCmp[i, "srcKind"].ToString().Trim() == "預") ? "BUD" : "BID");
			}
		}
		if (iidx == -1)
		{
			MessageBox.Show(this, "請勾選比對案", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		HisPrice hisCom = new HisPrice(tmp_AL1);
		string sSrcKind = CommonMethods.GetActionNameString(F_ActionName);
		hisCom.ps_ShowMisc = chkPrice.Checked;
		DT1 = hisCom.chkcostData(ls_ValS, ls_skindS, ls_Val[0], ls_skind[0], ls_Val[1], ls_skind[1], ls_Val[2], ls_skind[2], ls_Val[3], ls_skind[3], ls_Val[4], ls_skind[4], ls_Val[5], ls_skind[5], ls_Val[6], ls_skind[6], ls_Val[7], ls_skind[7], ls_Val[8], ls_skind[8], ls_Val[9], ls_skind[9]);
		return Ret;
	}

	private void BindToGrid()
	{
		ultraToolbarsManager1.BeginUpdate();
		ultraToolbarsManager1.Enabled = false;
		gridBudget1.Redraw = false;
		RememberColsProps();
		CellStyle CS1 = gridBudget1.Styles.Add("AnalysisColor");
		CellStyle CS9 = gridBudget1.Styles.Add("IsSharedColor");
		CS1.ForeColor = Color.Red;
		CS9.ForeColor = Color.Plum;
		gridBudget1.Clear(ClearFlags.All);
		gridBudget1.Select(0, 0);
		DataView DV1 = DT1.DefaultView;
		DV1.RowFilter = GetFilterString();
		int iRows = DV1.Count + 1;
		gridBudget1.Rows.Count = iRows;
		gridBudget1.Select(0, 0);
		SetGridColumn();
		ultraStatusBar1.Panels[0].Text = "資料筆數：" + DV1.Count;
		for (int i = 0; i < DV1.Count; i++)
		{
			if (DV1[i]["analysis"].ToString().Trim() == "1")
			{
				gridBudget1[i + 1, "Analysis"] = true;
				gridBudget1.Rows[i + 1].Style = gridBudget1.Styles["AnalysisColor"];
				CellRange rg = gridBudget1.GetCellRange(i + 1, gridBudget1.Cols["AnaImg"].SafeIndex);
				rg.Style = gridBudget1.Styles["img"];
				rg.Image = imageList2.Images[0];
			}
			else
			{
				gridBudget1[i + 1, "Analysis"] = false;
			}
			gridBudget1[i + 1, "ProjectCode"] = DV1[i]["projectCode"].ToString();
			gridBudget1[i + 1, "PubCode"] = DV1[i]["PubCode"].ToString();
			gridBudget1[i + 1, "PccesCode"] = DV1[i]["pccesCode"].ToString();
			gridBudget1[i + 1, "CName"] = DV1[i]["cName"].ToString();
			gridBudget1[i + 1, "UnitName"] = DV1[i]["unitName"].ToString();
			gridBudget1[i + 1, "ChkCostS"] = DV1[i]["chkCostS"].ToString();
			gridBudget1[i + 1, "ChkCost1"] = DV1[i]["chkCost1"].ToString();
			gridBudget1[i + 1, "ChkCost2"] = DV1[i]["chkCost2"].ToString();
			gridBudget1[i + 1, "ChkCost3"] = DV1[i]["chkCost3"].ToString();
			gridBudget1[i + 1, "ChkCost4"] = DV1[i]["chkCost4"].ToString();
			gridBudget1[i + 1, "ChkCost5"] = DV1[i]["chkCost5"].ToString();
			gridBudget1[i + 1, "ChkCost6"] = DV1[i]["chkCost6"].ToString();
			gridBudget1[i + 1, "ChkCost7"] = DV1[i]["chkCost7"].ToString();
			gridBudget1[i + 1, "ChkCost8"] = DV1[i]["chkCost8"].ToString();
			gridBudget1[i + 1, "ChkCost9"] = DV1[i]["chkCost9"].ToString();
			gridBudget1[i + 1, "Diff1"] = DV1[i]["diff1"].ToString();
			gridBudget1[i + 1, "Diff2"] = DV1[i]["diff2"].ToString();
			gridBudget1[i + 1, "Diff3"] = DV1[i]["diff3"].ToString();
			gridBudget1[i + 1, "Diff4"] = DV1[i]["diff4"].ToString();
			gridBudget1[i + 1, "Diff5"] = DV1[i]["diff5"].ToString();
			gridBudget1[i + 1, "Diff6"] = DV1[i]["diff6"].ToString();
			gridBudget1[i + 1, "Diff7"] = DV1[i]["diff7"].ToString();
			gridBudget1[i + 1, "Diff8"] = DV1[i]["diff8"].ToString();
			gridBudget1[i + 1, "Diff9"] = DV1[i]["diff9"].ToString();
			gridBudget1[i + 1, "DiffMrs"] = (PubTools.Str2Decimal(DV1[i]["MrsCost"].ToString()) - PubTools.Str2Decimal(DV1[i]["chkCostS"].ToString())).ToString();
			gridBudget1[i + 1, "AvgCost"] = DV1[i]["AvgCost"].ToString();
			gridBudget1[i + 1, "MrsCost"] = DV1[i]["MrsCost"].ToString();
		}
		gridBudget1.Redraw = true;
		gridBudget1.Invalidate();
		ultraToolbarsManager1.Enabled = true;
		ultraToolbarsManager1.EndUpdate();
	}

	private void ProcessCols()
	{
		gridBudget1.Rows[0].Height = 40;
		gridBudget1.Cols["ChkCostS"].Caption = "基準案\n" + dpBase.Value.ToString();
		gridBudget1.Cols["ChkCost1"].Caption = "比對[1]\n" + ls_Val[0];
		gridBudget1.Cols["ChkCost1"].Visible = ((ls_Val[0].Trim() != "") ? true : false);
		gridBudget1.Cols["ChkCost2"].Caption = "比對[2]\n" + ls_Val[1];
		gridBudget1.Cols["ChkCost2"].Visible = ((ls_Val[1].Trim() != "") ? true : false);
		gridBudget1.Cols["ChkCost3"].Caption = "比對[3]\n" + ls_Val[2];
		gridBudget1.Cols["ChkCost3"].Visible = ((ls_Val[2].Trim() != "") ? true : false);
		gridBudget1.Cols["ChkCost4"].Caption = "比對[4]\n" + ls_Val[3];
		gridBudget1.Cols["ChkCost4"].Visible = ((ls_Val[3].Trim() != "") ? true : false);
		gridBudget1.Cols["ChkCost5"].Caption = "比對[5]\n" + ls_Val[4];
		gridBudget1.Cols["ChkCost5"].Visible = ((ls_Val[4].Trim() != "") ? true : false);
		gridBudget1.Cols["ChkCost6"].Caption = "比對[6]\n" + ls_Val[5];
		gridBudget1.Cols["ChkCost6"].Visible = ((ls_Val[5].Trim() != "") ? true : false);
		gridBudget1.Cols["ChkCost7"].Caption = "比對[7]\n" + ls_Val[6];
		gridBudget1.Cols["ChkCost7"].Visible = ((ls_Val[6].Trim() != "") ? true : false);
		gridBudget1.Cols["ChkCost8"].Caption = "比對[8]\n" + ls_Val[7];
		gridBudget1.Cols["ChkCost8"].Visible = ((ls_Val[7].Trim() != "") ? true : false);
		gridBudget1.Cols["ChkCost9"].Caption = "比對[9]\n" + ls_Val[8];
		gridBudget1.Cols["ChkCost9"].Visible = ((ls_Val[8].Trim() != "") ? true : false);
		gridBudget1.Cols["ChkCost0"].Caption = "比對[10]\n" + ls_Val[9];
		gridBudget1.Cols["ChkCost0"].Visible = ((ls_Val[9].Trim() != "") ? true : false);
		gridBudget1.Cols["Diff1"].Caption = "差值[1]\n" + ls_Val[0];
		gridBudget1.Cols["Diff1"].Visible = ((ls_Val[0].Trim() != "") ? true : false);
		gridBudget1.Cols["Diff2"].Caption = "差值[2]\n" + ls_Val[1];
		gridBudget1.Cols["Diff2"].Visible = ((ls_Val[1].Trim() != "") ? true : false);
		gridBudget1.Cols["Diff3"].Caption = "差值[3]\n" + ls_Val[2];
		gridBudget1.Cols["Diff3"].Visible = ((ls_Val[2].Trim() != "") ? true : false);
		gridBudget1.Cols["Diff4"].Caption = "差值[4]\n" + ls_Val[3];
		gridBudget1.Cols["Diff4"].Visible = ((ls_Val[3].Trim() != "") ? true : false);
		gridBudget1.Cols["Diff5"].Caption = "差值[5]\n" + ls_Val[4];
		gridBudget1.Cols["Diff5"].Visible = ((ls_Val[4].Trim() != "") ? true : false);
		gridBudget1.Cols["Diff6"].Caption = "差值[6]\n" + ls_Val[5];
		gridBudget1.Cols["Diff6"].Visible = ((ls_Val[5].Trim() != "") ? true : false);
		gridBudget1.Cols["Diff7"].Caption = "差值[7]\n" + ls_Val[6];
		gridBudget1.Cols["Diff7"].Visible = ((ls_Val[6].Trim() != "") ? true : false);
		gridBudget1.Cols["Diff8"].Caption = "差值[8]\n" + ls_Val[7];
		gridBudget1.Cols["Diff8"].Visible = ((ls_Val[7].Trim() != "") ? true : false);
		gridBudget1.Cols["Diff9"].Caption = "差值[9]\n" + ls_Val[8];
		gridBudget1.Cols["Diff9"].Visible = ((ls_Val[8].Trim() != "") ? true : false);
		gridBudget1.Cols["Diff0"].Caption = "差值[10]\n" + ls_Val[9];
		gridBudget1.Cols["Diff0"].Visible = ((ls_Val[9].Trim() != "") ? true : false);
		gridBudget1.Cols["MrsCost"].Caption = "比對\n工項基本資料庫";
		gridBudget1.Cols["DiffMrs"].Caption = "差值\n工項基本資料庫";
		gridBudget1.Cols.Frozen = 8;
		Mrs_Is_Visible();
		Diff_Is_Visible();
	}

	private void Mrs_Is_Visible()
	{
		if (!chkMrs.Checked)
		{
			gridBudget1.Cols["MrsCost"].Visible = false;
			gridBudget1.Cols["DiffMrs"].Visible = false;
		}
		else
		{
			gridBudget1.Cols["MrsCost"].Visible = true;
			gridBudget1.Cols["DiffMrs"].Visible = true;
		}
	}

	private void Diff_Is_Visible()
	{
		if (chkDiff.Checked)
		{
			gridBudget1.Cols["Diff1"].Visible = false;
			gridBudget1.Cols["Diff2"].Visible = false;
			gridBudget1.Cols["Diff3"].Visible = false;
			gridBudget1.Cols["Diff4"].Visible = false;
			gridBudget1.Cols["Diff5"].Visible = false;
			gridBudget1.Cols["Diff6"].Visible = false;
			gridBudget1.Cols["Diff7"].Visible = false;
			gridBudget1.Cols["Diff8"].Visible = false;
			gridBudget1.Cols["Diff9"].Visible = false;
			gridBudget1.Cols["Diff0"].Visible = false;
			return;
		}
		gridBudget1.Cols["Diff1"].Visible = true;
		gridBudget1.Cols["Diff2"].Visible = true;
		gridBudget1.Cols["Diff3"].Visible = true;
		gridBudget1.Cols["Diff4"].Visible = true;
		gridBudget1.Cols["Diff5"].Visible = true;
		gridBudget1.Cols["Diff6"].Visible = true;
		gridBudget1.Cols["Diff7"].Visible = true;
		gridBudget1.Cols["Diff8"].Visible = true;
		gridBudget1.Cols["Diff9"].Visible = true;
		gridBudget1.Cols["Diff0"].Visible = true;
		if (ls_Val[0].Trim() == "")
		{
			gridBudget1.Cols["Diff1"].Visible = false;
		}
		if (ls_Val[1].Trim() == "")
		{
			gridBudget1.Cols["Diff2"].Visible = false;
		}
		if (ls_Val[2].Trim() == "")
		{
			gridBudget1.Cols["Diff3"].Visible = false;
		}
		if (ls_Val[3].Trim() == "")
		{
			gridBudget1.Cols["Diff4"].Visible = false;
		}
		if (ls_Val[4].Trim() == "")
		{
			gridBudget1.Cols["Diff5"].Visible = false;
		}
		if (ls_Val[5].Trim() == "")
		{
			gridBudget1.Cols["Diff6"].Visible = false;
		}
		if (ls_Val[6].Trim() == "")
		{
			gridBudget1.Cols["Diff7"].Visible = false;
		}
		if (ls_Val[7].Trim() == "")
		{
			gridBudget1.Cols["Diff8"].Visible = false;
		}
		if (ls_Val[8].Trim() == "")
		{
			gridBudget1.Cols["Diff9"].Visible = false;
		}
		if (ls_Val[9].Trim() == "")
		{
			gridBudget1.Cols["Diff0"].Visible = false;
		}
	}

	private void Do_ShowDiff()
	{
		CellStyle cs_IR = gridBudget1.Styles.Add("AnalysisColor");
		CellStyle cs_Org = gridBudget1.Styles.Add("IsSharedColor");
		CellStyle cs_Diff = gridBudget1.Styles.Add("DiffColor");
		cs_Diff.BackColor = Color.LightGreen;
		int iCri = ((ComboBoxTool)ultraToolbarsManager1.Tools["List1"]).SelectedIndex;
		string sCompare = "GT";
		if (iCri == 0)
		{
			sCompare = "NO";
		}
		if (iCri == 1)
		{
			sCompare = "GT";
		}
		if (iCri == 2)
		{
			sCompare = "LT";
		}
		double dDiff = 0.0;
		string sDiffValue = ((TextBoxTool)ultraToolbarsManager1.Tools["txtDiff"]).Text;
		if (sDiffValue.Trim() == "")
		{
			MessageBox.Show(this, "請先輸入差異值。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		try
		{
			dDiff = Convert.ToDouble(sDiffValue) / 100.0;
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Compare.FormCompareMrs.cs" + ex.Message);
			MessageBox.Show(this, "請輸入正確的差異值。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		for (int i = 1; i < gridBudget1.Rows.Count; i++)
		{
			double dBase = PubTools.Str2Double(gridBudget1[i, "ChkCostS"]);
			for (int j = 0; j <= 9; j++)
			{
				if (!gridBudget1.Cols["Diff" + j].Visible)
				{
					continue;
				}
				double dComp = PubTools.Str2Double(gridBudget1[i, "Diff" + j]);
				CellRange cg = gridBudget1.GetCellRange(i, gridBudget1.Cols["Diff" + j].SafeIndex, i, gridBudget1.Cols["Diff" + j].SafeIndex);
				if (dBase == 0.0 || sCompare == "NO")
				{
					continue;
				}
				if (sCompare == "GT")
				{
					if (Math.Abs(dComp / dBase) >= dDiff)
					{
						cg.Style = cs_Diff;
					}
				}
				else if (sCompare == "LT" && Math.Abs(dComp / dBase) <= dDiff)
				{
					cg.Style = cs_Diff;
				}
			}
		}
	}

	private string GetFilterString()
	{
		string tmp = " 1=1 ";
		switch (((ComboBoxTool)ultraToolbarsManager1.Tools["mnuCbo_Show"]).Value.ToString())
		{
		case "0":
			tmp += "";
			break;
		case "1":
		{
			tmp += " and ( ";
			string sCri = "";
			if (ls_Val[0].Trim() != "")
			{
				sCri += " chkcost1 <> chkcosts or";
			}
			if (ls_Val[1].Trim() != "")
			{
				sCri += " chkcost2 <> chkcosts or";
			}
			if (ls_Val[2].Trim() != "")
			{
				sCri += " chkcost3 <> chkcosts or";
			}
			if (ls_Val[3].Trim() != "")
			{
				sCri += " chkcost4 <> chkcosts or";
			}
			if (ls_Val[4].Trim() != "")
			{
				sCri += " chkcost5 <> chkcosts or";
			}
			if (ls_Val[5].Trim() != "")
			{
				sCri += " chkcost6 <> chkcosts or";
			}
			if (ls_Val[6].Trim() != "")
			{
				sCri += " chkcost7 <> chkcosts or";
			}
			if (ls_Val[7].Trim() != "")
			{
				sCri += " chkcost8 <> chkcosts or";
			}
			if (ls_Val[8].Trim() != "")
			{
				sCri += " chkcost9 <> chkcosts or";
			}
			if (ls_Val[9].Trim() != "")
			{
				sCri += " chkcost0 <> chkcosts or";
			}
			if (sCri.Trim().Length > 0)
			{
				sCri = sCri.Substring(0, sCri.Length - 2);
			}
			tmp = tmp + sCri + " ) ";
			break;
		}
		case "2":
		{
			tmp += " and ( ";
			string sCri = "";
			if (ls_Val[0].Trim() != "")
			{
				sCri += " chkcost1 is null or";
			}
			if (ls_Val[1].Trim() != "")
			{
				sCri += " chkcost2 is null or";
			}
			if (ls_Val[2].Trim() != "")
			{
				sCri += " chkcost3 is null or";
			}
			if (ls_Val[3].Trim() != "")
			{
				sCri += " chkcost4 is null or";
			}
			if (ls_Val[4].Trim() != "")
			{
				sCri += " chkcost5 is null or";
			}
			if (ls_Val[5].Trim() != "")
			{
				sCri += " chkcost6 is null or";
			}
			if (ls_Val[6].Trim() != "")
			{
				sCri += " chkcost7 is null or";
			}
			if (ls_Val[7].Trim() != "")
			{
				sCri += " chkcost8 is null or";
			}
			if (ls_Val[8].Trim() != "")
			{
				sCri += " chkcost9 is null or";
			}
			if (ls_Val[9].Trim() != "")
			{
				sCri += " chkcost0 is null or";
			}
			if (sCri.Trim().Length > 0)
			{
				sCri = sCri.Substring(0, sCri.Length - 2);
			}
			tmp = tmp + sCri + " ) ";
			break;
		}
		case "3":
			tmp += " and (Analysis = '1') ";
			break;
		case "4":
			tmp += " and (Analysis <> '1') ";
			break;
		case "5":
			tmp += " and (SubString(pccesCode,1,1) = 'L') ";
			break;
		case "6":
			tmp += " and (SubString(pccesCode,1,1) = 'E') ";
			break;
		case "7":
			tmp += " and (SubString(pccesCode,1,1) = 'M') ";
			break;
		case "8":
			tmp += " and (SubString(pccesCode,1,1) = 'W') ";
			break;
		}
		if (((StateButtonTool)ultraToolbarsManager1.Tools["mnuStrange"]).Checked)
		{
			tmp += " and ( ";
			string sCri = "";
			if (ls_Val[0].Trim() != "")
			{
				sCri += " chkcost1 <> chkcosts or";
			}
			if (ls_Val[1].Trim() != "")
			{
				sCri += " chkcost2 <> chkcosts or";
			}
			if (ls_Val[2].Trim() != "")
			{
				sCri += " chkcost3 <> chkcosts or";
			}
			if (ls_Val[3].Trim() != "")
			{
				sCri += " chkcost4 <> chkcosts or";
			}
			if (ls_Val[4].Trim() != "")
			{
				sCri += " chkcost5 <> chkcosts or";
			}
			if (ls_Val[5].Trim() != "")
			{
				sCri += " chkcost6 <> chkcosts or";
			}
			if (ls_Val[6].Trim() != "")
			{
				sCri += " chkcost7 <> chkcosts or";
			}
			if (ls_Val[7].Trim() != "")
			{
				sCri += " chkcost8 <> chkcosts or";
			}
			if (ls_Val[8].Trim() != "")
			{
				sCri += " chkcost9 <> chkcosts or";
			}
			if (ls_Val[9].Trim() != "")
			{
				sCri += " chkcost0 <> chkcosts or";
			}
			if (sCri.Trim().Length > 0)
			{
				sCri = sCri.Substring(0, sCri.Length - 2);
			}
			tmp = tmp + sCri + " ) ";
		}
		return tmp;
	}

	private void ultraToolbarsManager1_AfterToolCloseup(object sender, ToolDropdownEventArgs e)
	{
		string key = e.Tool.Key;
		if (key != null && key == "mnuCbo_Show")
		{
			Do_Compare2();
		}
	}

	private void Do_Compare2()
	{
		if (F_IsRunCompare)
		{
			BindToGrid();
		}
		else
		{
			MessageBox.Show(this, "請先執行比對，再來作切換", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		switch (e.Tool.Key)
		{
		case "mnu_Go":
			Do_ToolBarFind();
			break;
		case "mnuOpenPanel":
			Do_OpenPanel();
			break;
		case "mnuDoCompare":
			Execute_CompareAnalysis();
			break;
		case "mnuExport":
			Do_Export();
			break;
		case "mnuStrange":
			if (!F_IsRunCompare)
			{
				((StateButtonTool)ultraToolbarsManager1.Tools["mnuStrange"]).Checked = false;
			}
			else
			{
				Do_Compare2();
			}
			break;
		case "mnuDiffExec":
			Do_ShowDiff();
			break;
		}
	}

	private void Do_OpenPanel()
	{
		Btn_SpltHor_Click(this, EventArgs.Empty);
		dpBase.Focus();
	}

	private void Execute_CompareAnalysis()
	{
		if (!(bool)gridBudget1[gridBudget1.Row, "Analysis"])
		{
			MessageBox.Show(this, "該選取資料不是單價分析項目", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		FormCompareMrsAna FM_CMP_MA = new FormCompareMrsAna();
		FM_CMP_MA._ActionName = F_ActionName;
		FM_CMP_MA._UserID = F_UserID;
		FM_CMP_MA._PccesCode = gridBudget1[gridBudget1.Row, "PccesCode"].ToString().Trim();
		FM_CMP_MA._ProjectCode1 = dpBase.Value.ToString();
		string sProj = GetSelectedColProjectCode();
		FM_CMP_MA._ProjectCode2 = ((sProj == "") ? FM_CMP_MA._ProjectCode1 : sProj);
		FM_CMP_MA.ShowDialog();
		FM_CMP_MA.Close();
		FM_CMP_MA.Dispose();
		FM_CMP_MA = null;
	}

	private string GetSelectedColProjectCode()
	{
		string RetV = "";
		string sCol = gridBudget1.Cols[gridBudget1.MouseCol].Name;
		int iIndex = -1;
		try
		{
			iIndex = Convert.ToInt16(sCol.Substring(sCol.Length - 1));
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Compare.FormCompareMrs.cs" + ex.Message);
			if (sCol == "MrsCost" || sCol == "DiffMrs")
			{
				return "MRS";
			}
		}
		if (iIndex > -1)
		{
			RetV = ((iIndex != 0) ? ls_Val[iIndex - 1] : ls_Val[9]);
		}
		return RetV;
	}

	private void Do_Export()
	{
		string sFilter = "Microsoft Excel 97/2000 files (*.xls)|*.xls";
		saveFileDialog1.Filter = sFilter;
		saveFileDialog1.RestoreDirectory = true;
		saveFileDialog1.FileName = "經費審查比對";
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			gridBudget1._ExcelFileName = saveFileDialog1.FileName;
			gridBudget1._ExcelSheeName = "經費審查比對";
			gridBudget1._IsOpenExcelAfterExport = true;
			gridBudget1.ExecuteExport(c1GridExportType.Excel);
		}
	}

	private void Do_ToolBarFind()
	{
		if (gridBudget1.Rows.Count <= 1)
		{
			return;
		}
		int iStart = gridBudget1.Row + 1;
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
			iStart = gridBudget1.Row + 1;
		}
		if (sSearchText.Trim() == "")
		{
			return;
		}
		for (int i = iStart; i < gridBudget1.Rows.Count; i++)
		{
			for (int j = 1; j < gridBudget1.Cols.Count; j++)
			{
				if (gridBudget1[i, j] == null || gridBudget1[i, j].ToString().IndexOf(sSearchText) <= -1)
				{
					continue;
				}
				gridBudget1.Row = i;
				gridBudget1.Select();
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

	private void Btn_SpltHor_MouseEnter(object sender, EventArgs e)
	{
		if (MidPanelMode == LeftPanelMode.Open)
		{
			Btn_SpltHor.Appearance.ImageBackground = iglst_splt_Btn2.Images[1];
		}
		else
		{
			Btn_SpltHor.Appearance.ImageBackground = iglst_splt_Btn2.Images[3];
		}
	}

	private void Btn_SpltHor_MouseLeave(object sender, EventArgs e)
	{
		if (MidPanelMode == LeftPanelMode.Open)
		{
			Btn_SpltHor.Appearance.ImageBackground = iglst_splt_Btn2.Images[0];
		}
		else
		{
			Btn_SpltHor.Appearance.ImageBackground = iglst_splt_Btn2.Images[2];
		}
	}

	private void FormCompareMrs_Activated(object sender, EventArgs e)
	{
		base.ParentForm.Text = "PCCES Win 4.3 【經費審查比對】";
	}

	private void ultraToolbarsManager1_BeforeToolbarListDropdown(object sender, BeforeToolbarListDropdownEventArgs e)
	{
		e.Cancel = true;
	}

	private void chkDiff_CheckedChanged(object sender, EventArgs e)
	{
		Diff_Is_Visible();
	}

	private void chkMrs_CheckedChanged(object sender, EventArgs e)
	{
		Mrs_Is_Visible();
	}

	private void Op1_ValueChanged(object sender, EventArgs e)
	{
		if (Op1.CheckedIndex == 0)
		{
			F_ActionName = PccesFormAction.BUD;
		}
		else if (Op1.CheckedIndex == 1)
		{
			F_ActionName = PccesFormAction.BID;
		}
		else
		{
			F_ActionName = PccesFormAction.None;
		}
		ControlsClear();
		LoadData();
		BindToDropDown();
		gridBudget1.Rows.Count = 1;
	}

	private void ultraToolbarsManager1_ToolValueChanged(object sender, ToolEventArgs e)
	{
		if (!(e.Tool.Key == "mnu_Cbo1"))
		{
			return;
		}
		string sSearchText = ((TextBoxTool)ultraToolbarsManager1.Tools["mnu_Cbo1"]).Text.Trim();
		bool IsSearchName = false;
		for (int ii = 0; ii < sSearchText.Length; ii++)
		{
			if (sSearchText[ii] > '\u007f')
			{
				IsSearchName = true;
				break;
			}
		}
		int iFind = -1;
		int iStart = 1;
		int iColLookup = (IsSearchName ? gridBudget1.Cols["CName"].SafeIndex : gridBudget1.Cols["PccesCode"].SafeIndex);
		iFind = gridBudget1.FindRow(sSearchText.ToString(), iStart, iColLookup, caseSensitive: false, fullMatch: false, wrap: false);
		if (iFind > -1)
		{
			gridBudget1.Row = iFind;
		}
	}

	private void gridBudget1_MouseDown(object sender, MouseEventArgs e)
	{
		int rowIndex = gridBudget1.MouseRow;
		int colIndex = gridBudget1.MouseCol;
		gridBudget1.Col = colIndex;
		gridBudget1.Row = rowIndex;
	}

	private void gridBudget1_Resize(object sender, EventArgs e)
	{
		FormCompareMrs_Resize(sender, e);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Compare.FormCompareMrs));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem12 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem13 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem14 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuOpenPanel");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExport");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool1 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("lblShowItem");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool2 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnuCbo_Show");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool1 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuStrange", "");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar2 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool2");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool3 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnuDiff");
		Infragistics.Win.UltraWinToolbars.TextBoxTool textBoxTool1 = new Infragistics.Win.UltraWinToolbars.TextBoxTool("txtDiff");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool3 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("List1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDiffExec");
		Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool4 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool4 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Popup1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDoCompare");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuOpenPanel");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDoCompare");
		Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExport");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool5 = new Infragistics.Win.UltraWinToolbars.LabelTool("lblShowItem");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool5 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnuCbo_Show");
		Infragistics.Win.ValueList valueList2 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.ValueListItem valueListItem15 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem16 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem17 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem18 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem19 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem20 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem21 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem22 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool6 = new Infragistics.Win.UltraWinToolbars.LabelTool("lblRatio");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool6 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnuCbo_Differ");
		Infragistics.Win.ValueList valueList3 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool2 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuStrange", "");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool7 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnuDiff");
		Infragistics.Win.UltraWinToolbars.TextBoxTool textBoxTool2 = new Infragistics.Win.UltraWinToolbars.TextBoxTool("txtDiff");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool7 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("List1");
		Infragistics.Win.ValueList valueList4 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.ValueListItem valueListItem23 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem24 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem25 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool10 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDiffExec");
		Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
		this.LeftPanel = new System.Windows.Forms.Panel();
		this.onlineList1 = new Archnowledge.Pcces.PccesMain.ArchControls.OnlineList();
		this.functionButtons1 = new Archnowledge.Pcces.PccesMain.ArchControls.FunctionButtons();
		this.pnl_spliter = new System.Windows.Forms.Panel();
		this.Btn_Splt = new Infragistics.Win.Misc.UltraButton();
		this.ssp_Lower = new AxThreed.AxSSPanel();
		this.ssp_Bottom = new AxThreed.AxSSPanel();
		this.ssp_Upper = new AxThreed.AxSSPanel();
		this.ssp_Top = new AxThreed.AxSSPanel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.FormCompareMrs_Fill_Panel = new System.Windows.Forms.Panel();
		this.gridBudget1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.panel4 = new System.Windows.Forms.Panel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.Pnl_Spliter_Hor = new System.Windows.Forms.Panel();
		this.Btn_SpltHor = new Infragistics.Win.Misc.UltraButton();
		this.ssp_Righter = new AxThreed.AxSSPanel();
		this.ssp_Right = new AxThreed.AxSSPanel();
		this.ssp_Lefter = new AxThreed.AxSSPanel();
		this.ssp_Left = new AxThreed.AxSSPanel();
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.PNL_UPPER = new System.Windows.Forms.Panel();
		this.chkPrice = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.Op1 = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.pictureBox4 = new System.Windows.Forms.PictureBox();
		this.chkDiff = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.GridCmp = new C1.Win.C1FlexGrid.C1FlexGrid();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.pictureBox3 = new System.Windows.Forms.PictureBox();
		this.BtnExecute = new Infragistics.Win.Misc.UltraButton();
		this.pictureBox2 = new System.Windows.Forms.PictureBox();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.chkMrs = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.dpBase = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this._FormSys_B_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.iglst_splt_Btn = new System.Windows.Forms.ImageList(this.components);
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.iglst_splt_Btn2 = new System.Windows.Forms.ImageList(this.components);
		this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
		this.LeftPanel.SuspendLayout();
		this.pnl_spliter.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ssp_Lower).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Bottom).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Upper).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Top).BeginInit();
		this.panel1.SuspendLayout();
		this.FormCompareMrs_Fill_Panel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridBudget1).BeginInit();
		this.panel4.SuspendLayout();
		this.Pnl_Spliter_Hor.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ssp_Righter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Right).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Lefter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Left).BeginInit();
		this.PNL_UPPER.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Op1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.GridCmp).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dpBase).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		base.SuspendLayout();
		this.LeftPanel.Controls.Add(this.onlineList1);
		this.LeftPanel.Controls.Add(this.functionButtons1);
		this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
		this.LeftPanel.Location = new System.Drawing.Point(0, 0);
		this.LeftPanel.Name = "LeftPanel";
		this.LeftPanel.Size = new System.Drawing.Size(160, 509);
		this.LeftPanel.TabIndex = 1;
		this.onlineList1._FunctionName = "";
		this.onlineList1._HasRegistered = false;
		this.onlineList1._ServerName = "localhost";
		this.onlineList1._TRY_Flag = "";
		this.onlineList1._UserID = "";
		this.onlineList1._UserName = "";
		this.onlineList1.AutoSize = true;
		this.onlineList1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.onlineList1.Dock = System.Windows.Forms.DockStyle.Top;
		this.onlineList1.Location = new System.Drawing.Point(0, 0);
		this.onlineList1.Name = "onlineList1";
		this.onlineList1.Size = new System.Drawing.Size(160, 256);
		this.onlineList1.TabIndex = 4;
		this.functionButtons1._ActiveFunction = "";
		this.functionButtons1._CurrOpenMode = Archnowledge.Pcces.CommonClass.FunctionOpenMode.Budget;
		this.functionButtons1._ServerName = "localhost";
		this.functionButtons1._UserID = "PccesAdmin";
		this.functionButtons1._UserName = "";
		this.functionButtons1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.functionButtons1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.functionButtons1.Location = new System.Drawing.Point(0, 0);
		this.functionButtons1.Name = "functionButtons1";
		this.functionButtons1.Size = new System.Drawing.Size(160, 509);
		this.functionButtons1.TabIndex = 3;
		this.pnl_spliter.BackColor = System.Drawing.Color.LightGray;
		this.pnl_spliter.Controls.Add(this.Btn_Splt);
		this.pnl_spliter.Controls.Add(this.ssp_Lower);
		this.pnl_spliter.Controls.Add(this.ssp_Bottom);
		this.pnl_spliter.Controls.Add(this.ssp_Upper);
		this.pnl_spliter.Controls.Add(this.ssp_Top);
		this.pnl_spliter.Dock = System.Windows.Forms.DockStyle.Left;
		this.pnl_spliter.Location = new System.Drawing.Point(160, 0);
		this.pnl_spliter.Name = "pnl_spliter";
		this.pnl_spliter.Size = new System.Drawing.Size(7, 509);
		this.pnl_spliter.TabIndex = 3;
		appearance1.BorderColor = System.Drawing.Color.Transparent;
		appearance1.BorderColor3DBase = System.Drawing.Color.Transparent;
		appearance1.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance1.ImageBackground");
		this.Btn_Splt.Appearance = appearance1;
		this.Btn_Splt.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Borderless;
		this.Btn_Splt.Cursor = System.Windows.Forms.Cursors.Hand;
		this.Btn_Splt.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Btn_Splt.ImageSize = new System.Drawing.Size(7, 57);
		this.Btn_Splt.Location = new System.Drawing.Point(0, 228);
		this.Btn_Splt.Name = "Btn_Splt";
		this.Btn_Splt.ShapeImage = (System.Drawing.Image)resources.GetObject("Btn_Splt.ShapeImage");
		this.Btn_Splt.ShowFocusRect = false;
		this.Btn_Splt.ShowOutline = false;
		this.Btn_Splt.Size = new System.Drawing.Size(7, 56);
		this.Btn_Splt.TabIndex = 7;
		this.Btn_Splt.Click += new System.EventHandler(Btn_Splt_Click);
		this.ssp_Lower.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ssp_Lower.Location = new System.Drawing.Point(0, 284);
		this.ssp_Lower.Name = "ssp_Lower";
		this.ssp_Lower.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Lower.OcxState");
		this.ssp_Lower.Size = new System.Drawing.Size(7, 222);
		this.ssp_Lower.TabIndex = 6;
		this.ssp_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ssp_Bottom.Location = new System.Drawing.Point(0, 506);
		this.ssp_Bottom.Name = "ssp_Bottom";
		this.ssp_Bottom.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Bottom.OcxState");
		this.ssp_Bottom.Size = new System.Drawing.Size(7, 3);
		this.ssp_Bottom.TabIndex = 5;
		this.ssp_Upper.Dock = System.Windows.Forms.DockStyle.Top;
		this.ssp_Upper.Location = new System.Drawing.Point(0, 3);
		this.ssp_Upper.Name = "ssp_Upper";
		this.ssp_Upper.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Upper.OcxState");
		this.ssp_Upper.Size = new System.Drawing.Size(7, 225);
		this.ssp_Upper.TabIndex = 3;
		this.ssp_Top.Dock = System.Windows.Forms.DockStyle.Top;
		this.ssp_Top.Location = new System.Drawing.Point(0, 0);
		this.ssp_Top.Name = "ssp_Top";
		this.ssp_Top.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Top.OcxState");
		this.ssp_Top.Size = new System.Drawing.Size(7, 3);
		this.ssp_Top.TabIndex = 2;
		this.panel1.Controls.Add(this.ultraLabel10);
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(167, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(1118, 30);
		this.panel1.TabIndex = 4;
		appearance2.ForeColor = System.Drawing.Color.White;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel10.Appearance = appearance2;
		this.ultraLabel10.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel10.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel10.Location = new System.Drawing.Point(6, 7);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(74, 19);
		this.ultraLabel10.TabIndex = 14;
		this.ultraLabel10.Text = "比對條件";
		this.ultraLabel1.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel1.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraLabel1.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(1118, 30);
		this.ultraLabel1.TabIndex = 0;
		this.FormCompareMrs_Fill_Panel.Controls.Add(this.gridBudget1);
		this.FormCompareMrs_Fill_Panel.Controls.Add(this.panel4);
		this.FormCompareMrs_Fill_Panel.Controls.Add(this.Pnl_Spliter_Hor);
		this.FormCompareMrs_Fill_Panel.Controls.Add(this.ultraStatusBar1);
		this.FormCompareMrs_Fill_Panel.Controls.Add(this.PNL_UPPER);
		this.FormCompareMrs_Fill_Panel.Controls.Add(this.panel1);
		this.FormCompareMrs_Fill_Panel.Controls.Add(this.pnl_spliter);
		this.FormCompareMrs_Fill_Panel.Controls.Add(this.LeftPanel);
		this.FormCompareMrs_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
		this.FormCompareMrs_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.FormCompareMrs_Fill_Panel.Location = new System.Drawing.Point(0, 54);
		this.FormCompareMrs_Fill_Panel.Name = "FormCompareMrs_Fill_Panel";
		this.FormCompareMrs_Fill_Panel.Size = new System.Drawing.Size(1285, 509);
		this.FormCompareMrs_Fill_Panel.TabIndex = 0;
		this.gridBudget1._ExcelFileName = "";
		this.gridBudget1._ExcelSheeName = "";
		this.gridBudget1._IsOpenExcelAfterExport = false;
		this.gridBudget1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.gridBudget1.AllowEditing = false;
		this.gridBudget1.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Columns;
		this.gridBudget1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridBudget1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridBudget1.ColumnInfo = resources.GetString("gridBudget1.ColumnInfo");
		this.ultraToolbarsManager1.SetContextMenuUltra(this.gridBudget1, "Popup1");
		this.gridBudget1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridBudget1.ExtendLastCol = true;
		this.gridBudget1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridBudget1.ForeColor = System.Drawing.Color.Black;
		this.gridBudget1.Location = new System.Drawing.Point(167, 293);
		this.gridBudget1.Name = "gridBudget1";
		this.gridBudget1.Rows.Count = 1;
		this.gridBudget1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.gridBudget1.ShowCursor = true;
		this.gridBudget1.ShowToolTipOnNarrowColumn = true;
		this.gridBudget1.Size = new System.Drawing.Size(1118, 190);
		this.gridBudget1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridBudget1.Styles"));
		this.gridBudget1.TabIndex = 10;
		this.gridBudget1.Tree.Column = 1;
		this.gridBudget1.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridBudget1.MouseDown += new System.Windows.Forms.MouseEventHandler(gridBudget1_MouseDown);
		this.gridBudget1.Resize += new System.EventHandler(gridBudget1_Resize);
		this.panel4.Controls.Add(this.ultraLabel2);
		this.panel4.Controls.Add(this.ultraLabel3);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel4.Location = new System.Drawing.Point(167, 263);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(1118, 30);
		this.panel4.TabIndex = 8;
		appearance13.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance13;
		this.ultraLabel2.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.ultraLabel2.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel2.Location = new System.Drawing.Point(6, 7);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(162, 19);
		this.ultraLabel2.TabIndex = 14;
		this.ultraLabel2.Text = "經費審查比對結果";
		this.ultraLabel3.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.ultraLabel3.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ultraLabel3.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(1118, 30);
		this.ultraLabel3.TabIndex = 0;
		this.Pnl_Spliter_Hor.Controls.Add(this.Btn_SpltHor);
		this.Pnl_Spliter_Hor.Controls.Add(this.ssp_Righter);
		this.Pnl_Spliter_Hor.Controls.Add(this.ssp_Right);
		this.Pnl_Spliter_Hor.Controls.Add(this.ssp_Lefter);
		this.Pnl_Spliter_Hor.Controls.Add(this.ssp_Left);
		this.Pnl_Spliter_Hor.Dock = System.Windows.Forms.DockStyle.Top;
		this.Pnl_Spliter_Hor.Location = new System.Drawing.Point(167, 256);
		this.Pnl_Spliter_Hor.Name = "Pnl_Spliter_Hor";
		this.Pnl_Spliter_Hor.Size = new System.Drawing.Size(1118, 7);
		this.Pnl_Spliter_Hor.TabIndex = 19;
		appearance14.BorderColor = System.Drawing.Color.Transparent;
		appearance14.BorderColor3DBase = System.Drawing.Color.Transparent;
		appearance14.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance14.ImageBackground");
		this.Btn_SpltHor.Appearance = appearance14;
		this.Btn_SpltHor.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Borderless;
		this.Btn_SpltHor.Cursor = System.Windows.Forms.Cursors.Hand;
		this.Btn_SpltHor.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Btn_SpltHor.ImageSize = new System.Drawing.Size(7, 57);
		this.Btn_SpltHor.Location = new System.Drawing.Point(284, 0);
		this.Btn_SpltHor.Name = "Btn_SpltHor";
		this.Btn_SpltHor.ShapeImage = (System.Drawing.Image)resources.GetObject("Btn_SpltHor.ShapeImage");
		this.Btn_SpltHor.ShowFocusRect = false;
		this.Btn_SpltHor.ShowOutline = false;
		this.Btn_SpltHor.Size = new System.Drawing.Size(563, 7);
		this.Btn_SpltHor.TabIndex = 8;
		this.Btn_SpltHor.MouseLeave += new System.EventHandler(Btn_SpltHor_MouseLeave);
		this.Btn_SpltHor.Click += new System.EventHandler(Btn_SpltHor_Click);
		this.Btn_SpltHor.MouseEnter += new System.EventHandler(Btn_SpltHor_MouseEnter);
		this.ssp_Righter.Dock = System.Windows.Forms.DockStyle.Right;
		this.ssp_Righter.Location = new System.Drawing.Point(847, 0);
		this.ssp_Righter.Name = "ssp_Righter";
		this.ssp_Righter.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Righter.OcxState");
		this.ssp_Righter.Size = new System.Drawing.Size(268, 7);
		this.ssp_Righter.TabIndex = 7;
		this.ssp_Right.Dock = System.Windows.Forms.DockStyle.Right;
		this.ssp_Right.Location = new System.Drawing.Point(1115, 0);
		this.ssp_Right.Name = "ssp_Right";
		this.ssp_Right.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Right.OcxState");
		this.ssp_Right.Size = new System.Drawing.Size(3, 7);
		this.ssp_Right.TabIndex = 6;
		this.ssp_Lefter.Dock = System.Windows.Forms.DockStyle.Left;
		this.ssp_Lefter.Location = new System.Drawing.Point(3, 0);
		this.ssp_Lefter.Name = "ssp_Lefter";
		this.ssp_Lefter.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Lefter.OcxState");
		this.ssp_Lefter.Size = new System.Drawing.Size(281, 7);
		this.ssp_Lefter.TabIndex = 5;
		this.ssp_Left.Dock = System.Windows.Forms.DockStyle.Left;
		this.ssp_Left.Location = new System.Drawing.Point(0, 0);
		this.ssp_Left.Name = "ssp_Left";
		this.ssp_Left.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Left.OcxState");
		this.ssp_Left.Size = new System.Drawing.Size(3, 7);
		this.ssp_Left.TabIndex = 4;
		appearance15.FontData.SizeInPoints = 11f;
		appearance15.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraStatusBar1.Appearance = appearance15;
		this.ultraStatusBar1.Location = new System.Drawing.Point(167, 483);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		appearance16.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraStatusPanel1.Appearance = appearance16;
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Key = "RowsCount";
		ultraStatusPanel1.Text = "資料筆數：";
		ultraStatusPanel1.Width = 200;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.Key = "ProgressBar";
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[2] { ultraStatusPanel1, ultraStatusPanel2 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(1118, 26);
		this.ultraStatusBar1.TabIndex = 9;
		this.ultraStatusBar1.Text = "ultraStatusBar1";
		this.PNL_UPPER.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.PNL_UPPER.Controls.Add(this.chkPrice);
		this.PNL_UPPER.Controls.Add(this.Op1);
		this.PNL_UPPER.Controls.Add(this.ultraLabel7);
		this.PNL_UPPER.Controls.Add(this.pictureBox4);
		this.PNL_UPPER.Controls.Add(this.chkDiff);
		this.PNL_UPPER.Controls.Add(this.GridCmp);
		this.PNL_UPPER.Controls.Add(this.ultraLabel6);
		this.PNL_UPPER.Controls.Add(this.pictureBox3);
		this.PNL_UPPER.Controls.Add(this.BtnExecute);
		this.PNL_UPPER.Controls.Add(this.pictureBox2);
		this.PNL_UPPER.Controls.Add(this.pictureBox1);
		this.PNL_UPPER.Controls.Add(this.chkMrs);
		this.PNL_UPPER.Controls.Add(this.ultraLabel9);
		this.PNL_UPPER.Controls.Add(this.dpBase);
		this.PNL_UPPER.Controls.Add(this.ultraLabel5);
		this.PNL_UPPER.Controls.Add(this.ultraLabel4);
		this.PNL_UPPER.Dock = System.Windows.Forms.DockStyle.Top;
		this.PNL_UPPER.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.PNL_UPPER.Location = new System.Drawing.Point(167, 30);
		this.PNL_UPPER.Name = "PNL_UPPER";
		this.PNL_UPPER.Size = new System.Drawing.Size(1118, 226);
		this.PNL_UPPER.TabIndex = 5;
		this.chkPrice.Location = new System.Drawing.Point(51, 200);
		this.chkPrice.Name = "chkPrice";
		this.chkPrice.Size = new System.Drawing.Size(164, 20);
		this.chkPrice.TabIndex = 21;
		this.chkPrice.Text = "比對含變動單價";
		this.Op1.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.Op1.CheckedIndex = 0;
		this.Op1.ItemAppearance = appearance17;
		valueListItem12.DataValue = "BUD";
		valueListItem12.DisplayText = "預算書";
		valueListItem13.DataValue = "BID";
		valueListItem13.DisplayText = "標單";
		valueListItem14.DataValue = "Both";
		valueListItem14.DisplayText = "兩者";
		this.Op1.Items.Add(valueListItem12);
		this.Op1.Items.Add(valueListItem13);
		this.Op1.Items.Add(valueListItem14);
		this.Op1.ItemSpacingHorizontal = 10;
		this.Op1.ItemSpacingVertical = 10;
		this.Op1.Location = new System.Drawing.Point(52, 32);
		this.Op1.Name = "Op1";
		this.Op1.Size = new System.Drawing.Size(240, 32);
		this.Op1.TabIndex = 20;
		this.Op1.Text = "預算書";
		this.Op1.ValueChanged += new System.EventHandler(Op1_ValueChanged);
		appearance18.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance18.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel7.Appearance = appearance18;
		this.ultraLabel7.Location = new System.Drawing.Point(49, 13);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(75, 23);
		this.ultraLabel7.TabIndex = 19;
		this.ultraLabel7.Text = "選擇類別";
		this.pictureBox4.Image = (System.Drawing.Image)resources.GetObject("pictureBox4.Image");
		this.pictureBox4.Location = new System.Drawing.Point(12, 4);
		this.pictureBox4.Name = "pictureBox4";
		this.pictureBox4.Size = new System.Drawing.Size(40, 36);
		this.pictureBox4.TabIndex = 18;
		this.pictureBox4.TabStop = false;
		this.chkDiff.Location = new System.Drawing.Point(51, 178);
		this.chkDiff.Name = "chkDiff";
		this.chkDiff.Size = new System.Drawing.Size(168, 20);
		this.chkDiff.TabIndex = 17;
		this.chkDiff.Text = "差值欄位隱藏";
		this.chkDiff.CheckedChanged += new System.EventHandler(chkDiff_CheckedChanged);
		this.GridCmp.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GridCmp.BackColor = System.Drawing.Color.LightGray;
		this.GridCmp.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.GridCmp.ColumnInfo = resources.GetString("GridCmp.ColumnInfo");
		this.GridCmp.ExtendLastCol = true;
		this.GridCmp.ForeColor = System.Drawing.SystemColors.WindowText;
		this.GridCmp.Location = new System.Drawing.Point(352, 36);
		this.GridCmp.Name = "GridCmp";
		this.GridCmp.Rows.Count = 4;
		this.GridCmp.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.GridCmp.Size = new System.Drawing.Size(759, 156);
		this.GridCmp.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("GridCmp.Styles"));
		this.GridCmp.TabIndex = 16;
		appearance19.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance19.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel6.Appearance = appearance19;
		this.ultraLabel6.Location = new System.Drawing.Point(356, 12);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(200, 23);
		this.ultraLabel6.TabIndex = 15;
		this.ultraLabel6.Text = "勾選比對案(至多勾選10筆)";
		this.pictureBox3.Image = (System.Drawing.Image)resources.GetObject("pictureBox3.Image");
		this.pictureBox3.Location = new System.Drawing.Point(318, 4);
		this.pictureBox3.Name = "pictureBox3";
		this.pictureBox3.Size = new System.Drawing.Size(36, 32);
		this.pictureBox3.TabIndex = 14;
		this.pictureBox3.TabStop = false;
		appearance20.Image = resources.GetObject("appearance20.Image");
		this.BtnExecute.Appearance = appearance20;
		this.BtnExecute.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnExecute.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnExecute.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.BtnExecute.Location = new System.Drawing.Point(232, 188);
		this.BtnExecute.Name = "BtnExecute";
		this.BtnExecute.ShowFocusRect = false;
		this.BtnExecute.ShowOutline = false;
		this.BtnExecute.Size = new System.Drawing.Size(96, 31);
		this.BtnExecute.SupportThemes = false;
		this.BtnExecute.TabIndex = 13;
		this.BtnExecute.Text = "執行比對";
		this.BtnExecute.Click += new System.EventHandler(BtnExecute_Click);
		this.pictureBox2.Image = (System.Drawing.Image)resources.GetObject("pictureBox2.Image");
		this.pictureBox2.Location = new System.Drawing.Point(12, 130);
		this.pictureBox2.Name = "pictureBox2";
		this.pictureBox2.Size = new System.Drawing.Size(36, 32);
		this.pictureBox2.TabIndex = 12;
		this.pictureBox2.TabStop = false;
		this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		this.pictureBox1.Location = new System.Drawing.Point(12, 60);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(40, 36);
		this.pictureBox1.TabIndex = 11;
		this.pictureBox1.TabStop = false;
		this.chkMrs.Location = new System.Drawing.Point(51, 158);
		this.chkMrs.Name = "chkMrs";
		this.chkMrs.Size = new System.Drawing.Size(168, 20);
		this.chkMrs.TabIndex = 10;
		this.chkMrs.Text = "比對工料基本資料庫";
		this.chkMrs.CheckedChanged += new System.EventHandler(chkMrs_CheckedChanged);
		appearance21.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance21.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel9.Appearance = appearance21;
		this.ultraLabel9.Location = new System.Drawing.Point(49, 134);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(172, 23);
		this.ultraLabel9.TabIndex = 9;
		this.ultraLabel9.Text = "設定比對內容";
		this.dpBase.AutoSize = true;
		this.dpBase.DropDownListWidth = 400;
		this.dpBase.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		this.dpBase.Location = new System.Drawing.Point(95, 94);
		this.dpBase.Name = "dpBase";
		this.dpBase.Size = new System.Drawing.Size(201, 24);
		this.dpBase.TabIndex = 5;
		this.dpBase.Text = null;
		appearance22.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance22;
		this.ultraLabel5.Location = new System.Drawing.Point(16, 98);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(88, 23);
		this.ultraLabel5.TabIndex = 1;
		this.ultraLabel5.Text = "基準標案:";
		appearance23.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance23.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance23;
		this.ultraLabel4.Location = new System.Drawing.Point(49, 67);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(200, 23);
		this.ultraLabel4.TabIndex = 0;
		this.ultraLabel4.Text = "挑選基準案";
		appearance24.FontData.Name = "Arial";
		appearance24.FontData.SizeInPoints = 9f;
		this.ultraToolbarsManager1.Appearance = appearance24;
		appearance25.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance25.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.DockAreaAppearance = appearance25;
		this.ultraToolbarsManager1.DockWithinContainer = this;
		this.ultraToolbarsManager1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraToolbarsManager1.LockToolbars = true;
		appearance26.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance26.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance26.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.MenuSettings.HotTrackAppearance = appearance26;
		appearance27.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance27.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsManager1.MenuSettings.IconAreaAppearance = appearance27;
		appearance28.BackColor = System.Drawing.Color.White;
		appearance28.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.MenuSettings.ToolAppearance = appearance28;
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager1.ShowQuickCustomizeButton = false;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
		ultraToolbar1.Settings.AllowDockTop = Infragistics.Win.DefaultableBoolean.True;
		ultraToolbar1.Text = "Tool1";
		buttonTool1.InstanceProps.IsFirstInGroup = true;
		buttonTool2.InstanceProps.IsFirstInGroup = true;
		labelTool1.InstanceProps.IsFirstInGroup = true;
		labelTool2.InstanceProps.IsFirstInGroup = true;
		stateButtonTool1.InstanceProps.IsFirstInGroup = true;
		stateButtonTool1.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[8] { buttonTool1, buttonTool2, labelTool1, comboBoxTool1, buttonTool3, labelTool2, comboBoxTool2, stateButtonTool1 });
		ultraToolbar2.DockedColumn = 0;
		ultraToolbar2.DockedRow = 1;
		ultraToolbar2.Text = "Tool2";
		ultraToolbar2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { labelTool3, textBoxTool1, comboBoxTool3, buttonTool4 });
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[2] { ultraToolbar1, ultraToolbar2 });
		appearance29.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance29.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance29;
		appearance30.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance30.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance30.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance30;
		labelTool4.SharedProps.Caption = "尋找:";
		labelTool4.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool4.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool4.SharedProps.Caption = "輸入關鍵字";
		comboBoxTool4.SharedProps.Width = 120;
		comboBoxTool4.ValueList = valueList1;
		appearance31.Image = resources.GetObject("appearance10.Image");
		buttonTool5.SharedProps.AppearancesSmall.Appearance = appearance31;
		buttonTool5.SharedProps.Caption = "執行";
		popupMenuTool1.SharedProps.Caption = "右鍵功能表";
		popupMenuTool1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[1] { buttonTool6 });
		buttonTool7.SharedProps.Caption = "隱藏比對條件";
		buttonTool7.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		appearance32.Image = resources.GetObject("appearance11.Image");
		buttonTool8.SharedProps.AppearancesSmall.Appearance = appearance32;
		buttonTool8.SharedProps.Caption = "比對單價分析";
		buttonTool8.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool9.SharedProps.Caption = "匯出Excel";
		buttonTool9.SharedProps.CustomizerCaption = "匯出Excel 格式 的比對結果 ";
		buttonTool9.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool9.SharedProps.ToolTipText = " 匯出Excel 格式 的比對結果 ";
		labelTool5.SharedProps.Caption = "顯示項目:";
		labelTool5.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool5.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool5.SharedProps.Caption = "全部顯示";
		comboBoxTool5.SharedProps.Width = 120;
		valueListItem15.DataValue = "0";
		valueListItem15.DisplayText = "全部顯示";
		valueListItem16.DataValue = "2";
		valueListItem16.DisplayText = "特有工項";
		valueListItem17.DataValue = "3";
		valueListItem17.DisplayText = "有單價分析工項";
		valueListItem18.DataValue = "4";
		valueListItem18.DisplayText = "無單價分析工項";
		valueListItem19.DataValue = "5";
		valueListItem19.DisplayText = "人工";
		valueListItem20.DataValue = "6";
		valueListItem20.DisplayText = "機具";
		valueListItem21.DataValue = "7";
		valueListItem21.DisplayText = "材料";
		valueListItem22.DataValue = "8";
		valueListItem22.DisplayText = "雜項";
		valueList2.ValueListItems.Add(valueListItem15);
		valueList2.ValueListItems.Add(valueListItem16);
		valueList2.ValueListItems.Add(valueListItem17);
		valueList2.ValueListItems.Add(valueListItem18);
		valueList2.ValueListItems.Add(valueListItem19);
		valueList2.ValueListItems.Add(valueListItem20);
		valueList2.ValueListItems.Add(valueListItem21);
		valueList2.ValueListItems.Add(valueListItem22);
		comboBoxTool5.ValueList = valueList2;
		labelTool6.SharedProps.Caption = "差異百分比:";
		labelTool6.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool6.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool6.SharedProps.Caption = "差異百分比";
		comboBoxTool6.SharedProps.Width = 200;
		comboBoxTool6.ValueList = valueList3;
		stateButtonTool2.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool2.SharedProps.Caption = "單價異常項";
		stateButtonTool2.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		labelTool7.SharedProps.Caption = "差異百分比:";
		textBoxTool2.SharedProps.Caption = "差異百分比";
		textBoxTool2.SharedProps.Width = 60;
		comboBoxTool7.SharedProps.Caption = "差異條件";
		comboBoxTool7.SharedProps.Width = 80;
		valueListItem23.DataValue = "No";
		valueListItem23.DisplayText = "無差異";
		valueListItem24.DataValue = "GT";
		valueListItem24.DisplayText = "以上(含)";
		valueListItem25.DataValue = "LT";
		valueListItem25.DisplayText = "以下(含)";
		valueList4.ValueListItems.Add(valueListItem23);
		valueList4.ValueListItems.Add(valueListItem24);
		valueList4.ValueListItems.Add(valueListItem25);
		comboBoxTool7.ValueList = valueList4;
		appearance33.Image = resources.GetObject("appearance12.Image");
		buttonTool10.SharedProps.AppearancesSmall.Appearance = appearance33;
		buttonTool10.SharedProps.Caption = "執行";
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[16]
		{
			labelTool4, comboBoxTool4, buttonTool5, popupMenuTool1, buttonTool7, buttonTool8, buttonTool9, labelTool5, comboBoxTool5, labelTool6,
			comboBoxTool6, stateButtonTool2, labelTool7, textBoxTool2, comboBoxTool7, buttonTool10
		});
		this.ultraToolbarsManager1.ToolKeyPress += new Infragistics.Win.UltraWinToolbars.ToolKeyPressEventHandler(ultraToolbarsManager1_ToolKeyPress);
		this.ultraToolbarsManager1.BeforeToolbarListDropdown += new Infragistics.Win.UltraWinToolbars.BeforeToolbarListDropdownEventHandler(ultraToolbarsManager1_BeforeToolbarListDropdown);
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this.ultraToolbarsManager1.AfterToolCloseup += new Infragistics.Win.UltraWinToolbars.ToolDropdownEventHandler(ultraToolbarsManager1_AfterToolCloseup);
		this.ultraToolbarsManager1.ToolValueChanged += new Infragistics.Win.UltraWinToolbars.ToolEventHandler(ultraToolbarsManager1_ToolValueChanged);
		this._FormSys_B_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormSys_B_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormSys_B_Toolbars_Dock_Area_Top.Name = "_FormSys_B_Toolbars_Dock_Area_Top";
		this._FormSys_B_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(1285, 54);
		this._FormSys_B_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 563);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Name = "_FormSys_B_Toolbars_Dock_Area_Bottom";
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(1285, 0);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormSys_B_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 54);
		this._FormSys_B_Toolbars_Dock_Area_Left.Name = "_FormSys_B_Toolbars_Dock_Area_Left";
		this._FormSys_B_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 509);
		this._FormSys_B_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormSys_B_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(1285, 54);
		this._FormSys_B_Toolbars_Dock_Area_Right.Name = "_FormSys_B_Toolbars_Dock_Area_Right";
		this._FormSys_B_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 509);
		this._FormSys_B_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this.iglst_splt_Btn.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("iglst_splt_Btn.ImageStream");
		this.iglst_splt_Btn.TransparentColor = System.Drawing.Color.Transparent;
		this.iglst_splt_Btn.Images.SetKeyName(0, "");
		this.iglst_splt_Btn.Images.SetKeyName(1, "");
		this.iglst_splt_Btn.Images.SetKeyName(2, "");
		this.iglst_splt_Btn.Images.SetKeyName(3, "");
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.White;
		this.imageList2.Images.SetKeyName(0, "");
		this.iglst_splt_Btn2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("iglst_splt_Btn2.ImageStream");
		this.iglst_splt_Btn2.TransparentColor = System.Drawing.Color.Transparent;
		this.iglst_splt_Btn2.Images.SetKeyName(0, "");
		this.iglst_splt_Btn2.Images.SetKeyName(1, "");
		this.iglst_splt_Btn2.Images.SetKeyName(2, "");
		this.iglst_splt_Btn2.Images.SetKeyName(3, "");
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(1285, 563);
		base.Controls.Add(this.FormCompareMrs_Fill_Panel);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Bottom);
		base.KeyPreview = true;
		base.Name = "FormCompareMrs";
		this.Text = "經費審查比對";
		base.Load += new System.EventHandler(FormCompareMrs_Load);
		base.Activated += new System.EventHandler(FormCompareMrs_Activated);
		base.Resize += new System.EventHandler(FormCompareMrs_Resize);
		this.LeftPanel.ResumeLayout(false);
		this.LeftPanel.PerformLayout();
		this.pnl_spliter.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ssp_Lower).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Bottom).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Upper).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Top).EndInit();
		this.panel1.ResumeLayout(false);
		this.FormCompareMrs_Fill_Panel.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridBudget1).EndInit();
		this.panel4.ResumeLayout(false);
		this.Pnl_Spliter_Hor.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ssp_Righter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Right).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Lefter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Left).EndInit();
		this.PNL_UPPER.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Op1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.GridCmp).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dpBase).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
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
