using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.Bid;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.DomainModule.LogicalBase;
using Archnowledge.Pcces.PccesMain.ArchControls;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinStatusBar;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetBidSet : Form
{
	private DataTable DT1 = new DataTable();

	private string UserID;

	private string ProjectCode;

	private PccesFormAction FormActionName;

	private IContainer components;

	private Panel panel1;

	private GroupBox groupBox1;

	private UltraButton A_Btn_Cncl;

	private UltraButton A_Btn_Next;

	private Panel panel2;

	private Panel panel3;

	private GridBudget gridBudget1;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel6;

	private UltraStatusBar ultraStatusBar1;

	private Panel panel4;

	private ImageList imageList2;

	private LevelSwitchButton levelSwitchButton;

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

	public string _ProjectCode
	{
		get
		{
			return ProjectCode;
		}
		set
		{
			ProjectCode = value;
		}
	}

	public PccesFormAction _ActionName
	{
		get
		{
			return FormActionName;
		}
		set
		{
			FormActionName = value;
		}
	}

	public FormBudgetBidSet()
	{
		InitializeComponent();
		CellStyle cs1 = gridBudget1.Styles.Add("EditMode");
		cs1.DataType = typeof(Image);
		cs1.ImageAlign = ImageAlignEnum.RightCenter;
	}

	private void HideCols(bool IsHide)
	{
		if (IsHide)
		{
			gridBudget1.Cols["LevelNo"].Visible = false;
			gridBudget1.Cols["Kind"].Visible = false;
			gridBudget1.Cols["SNo"].Visible = false;
			gridBudget1.Cols["Analysis"].Visible = false;
		}
	}

	private void SetColsEditSymbol()
	{
		for (int i = 1; i < gridBudget1.Cols.Count; i++)
		{
			if (gridBudget1.Cols[i].AllowEditing)
			{
				CellRange rg = gridBudget1.GetCellRange(0, i);
				rg.Style = gridBudget1.Styles["EditMode"];
				rg.Image = imageList2.Images[1];
			}
		}
	}

	private void FormBudgetBidSet_Load(object sender, EventArgs e)
	{
		HideCols(IsHide: true);
		DT1 = GetItemABidding();
		BindToGrid();
	}

	private DataTable GetItemABidding()
	{
		DataTable RetV = new DataTable();
		string ls_selectstr = "Select b.pccescode,b.analysis, b.analysisQty, a.itemNo, a.cName, a.unitName, a.SNo, a.PrintNo, a.Kind, c.IsBid, c.IsFormulaChangeKind, c.FormulaNewName, a.levelNo ";
		ls_selectstr = ((!(CommonMethods.GetActionNameString(FormActionName).ToUpper() == "BUD")) ? (ls_selectstr + " from bidItemA a left outer join bidProjMrsA b  on a.pubcode=b.pubcode and a.projectcode=b.projectcode  left outer join bidPageBreak c  on a.SNo = c.SNo and a.ProjectCode = c.ProjectCode ") : (ls_selectstr + " from budItemA a left outer join budProjMrsA b  on a.pubcode=b.pubcode and a.projectcode=b.projectcode  left outer join budPageBreak c  on a.SNo = c.SNo and a.ProjectCode = c.ProjectCode "));
		ls_selectstr = ls_selectstr + " where a.ProjectCode = '" + ProjectCode + "' ";
		ls_selectstr += " order by a.PrintNo ";
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = UserID;
		RetV = DBCLS.GetUserDefine(ls_selectstr);
		DBCLS = null;
		return RetV;
	}

	private void BindToGrid()
	{
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = UserID;
		string sSrcKind = CommonMethods.GetActionNameString(FormActionName);
		DataTable DT_IsBid = DBCLS.GetUserDefine("Select SNo, IsBid From " + sSrcKind + "PageBreak Where ProjectCode='" + ProjectCode + "' and ( IsBid is not null and IsBid = 'Y')");
		Cursor = Cursors.WaitCursor;
		int iLevel = 0;
		ultraStatusBar1.Panels[1].ProgressBarInfo.Value = 0;
		ultraStatusBar1.Panels[1].ProgressBarInfo.Minimum = 0;
		ultraStatusBar1.Panels[1].ProgressBarInfo.Maximum = DT1.Rows.Count;
		ultraStatusBar1.Panels[1].ProgressBarInfo.ShowLabel = true;
		ultraStatusBar1.Panels[0].Text = "資料筆數 : " + DT1.Rows.Count;
		CellStyle CS0 = gridBudget1.Styles.Add("Transparent");
		CellStyle CS1 = gridBudget1.Styles.Add("AnalysisColor");
		CellStyle CS2 = gridBudget1.Styles.Add("MainColor");
		CellStyle CS9 = gridBudget1.Styles.Add("IsSharedColor");
		CellStyle CSA = gridBudget1.Styles.Add("Adjustment");
		CellStyle CSF = gridBudget1.Styles.Add("IsFormulaChangeKind");
		CS0.ForeColor = Color.Transparent;
		CS1.ForeColor = Color.Red;
		CS2.ForeColor = Color.Blue;
		CS9.ForeColor = Color.Green;
		CSA.BackColor = Color.OrangeRed;
		CSF.BackColor = Color.LightPink;
		gridBudget1.Redraw = false;
		gridBudget1.Rows.Count = DT1.Rows.Count + 1;
		string sKind = "";
		int iCount = 0;
		for (int i = 0; i < DT1.Rows.Count; i++)
		{
			gridBudget1[i + 1, "ItemNo"] = DT1.Rows[i]["itemNo"].ToString().Trim();
			gridBudget1[i + 1, "CName"] = DT1.Rows[i]["cName"].ToString().Trim();
			gridBudget1[i + 1, "UnitName"] = DT1.Rows[i]["unitName"].ToString().Trim();
			gridBudget1[i + 1, "printNo"] = DT1.Rows[i]["PrintNo"].ToString().Trim();
			gridBudget1[i + 1, "IsBid"] = DT1.Rows[i]["IsBid"].ToString().Trim() == "Y";
			if (DT_IsBid.Rows.Count == 0)
			{
				gridBudget1[i + 1, "IsBid"] = true;
			}
			gridBudget1[i + 1, "IsFormulaChangeKind"] = ((DT1.Rows[i]["IsFormulaChangeKind"] != DBNull.Value && !(DT1.Rows[i]["IsFormulaChangeKind"].ToString() == "N")) ? true : false);
			gridBudget1[i + 1, "FormulaNewName"] = ((DT1.Rows[i]["FormulaNewName"] == DBNull.Value) ? string.Empty : DT1.Rows[i]["FormulaNewName"].ToString());
			gridBudget1[i + 1, "Kind"] = DT1.Rows[i]["Kind"].ToString().Trim();
			gridBudget1[i + 1, "SNo"] = DT1.Rows[i]["sNo"].ToString().Trim();
			if (DT1.Rows[i]["Kind"].ToString().Trim() == "B" && DT1.Rows[i + 1]["Kind"].ToString().Trim() != "B")
			{
				iCount++;
			}
			gridBudget1[i + 1, "flag"] = iCount;
			gridBudget1.Rows[i + 1].IsNode = true;
			gridBudget1.Rows[i + 1].Node.Level = DT1.Rows[i]["PrintNo"].ToString().Trim().Length / 4;
			sKind = ((DT1.Rows[i]["kind"].ToString().Length > 0) ? DT1.Rows[i]["kind"].ToString().ToUpper().Trim() : "");
			switch (sKind)
			{
			default:
				if (!(sKind == "U"))
				{
					break;
				}
				goto case "B";
			case "B":
			case "L":
			case "F":
			case "S":
			case "Z":
				gridBudget1.Rows[i + 1].Style = gridBudget1.Styles["MainColor"];
				break;
			}
			if ("FSU".IndexOf(DT1.Rows[i]["Kind"].ToString().Trim()) > -1)
			{
				gridBudget1.SetCellStyle(i + 1, gridBudget1.Cols["IsFormulaChangeKind"].SafeIndex, CSF);
			}
			if (DT1.Rows[i]["analysis"].ToString().Trim() == "1")
			{
				gridBudget1[i + 1, "Analysis"] = true;
				gridBudget1.Rows[i + 1].Style = gridBudget1.Styles["AnalysisColor"];
			}
			else
			{
				gridBudget1[i + 1, "Analysis"] = false;
			}
			if (DT1.Rows[i]["PrintNo"].ToString().Trim() == "".PadLeft(32, '9'))
			{
				gridBudget1.Rows[i + 1].Node.Level = 1;
				gridBudget1[i + 1, "IsBid"] = true;
			}
			if (gridBudget1.Rows[i + 1].Node.Level > iLevel)
			{
				iLevel = gridBudget1.Rows[i + 1].Node.Level;
			}
			Application.DoEvents();
			ultraStatusBar1.Panels[1].ProgressBarInfo.Value = i + 1;
		}
		gridBudget1.Redraw = true;
		SetColsEditSymbol();
		levelSwitchButton.MaxLevel = iLevel;
		ultraStatusBar1.Panels[1].ProgressBarInfo.Value = 0;
		ultraStatusBar1.Panels[1].ProgressBarInfo.ShowLabel = false;
		Cursor = Cursors.Default;
	}

	private void A_Btn_Next_Click(object sender, EventArgs e)
	{
		string sSQL = "";
		string sSrcKind = CommonMethods.GetActionNameString(FormActionName);
		if (sSrcKind.ToUpper() == "BUD" || sSrcKind.ToUpper() == "BID")
		{
			DataSet dsBudPCalsCustomVar = null;
			PageBreak pagebreak;
			if (sSrcKind.ToUpper() == "BUD")
			{
				BudPCalsCustomVar budPCalsCustomVar = new BudPCalsCustomVar();
				dsBudPCalsCustomVar = budPCalsCustomVar.GetPCalsCustomVar(ProjectCode, 0);
				pagebreak = new BudPageBreak();
			}
			else
			{
				pagebreak = new BidPageBreak();
			}
			StringBuilder builder = new StringBuilder();
			DataSet dsPageBreak = pagebreak.GetPageBreak(ProjectCode);
			dsPageBreak.Tables[0].PrimaryKey = new DataColumn[1] { dsPageBreak.Tables[0].Columns["sNo"] };
			for (int i = 1; i < gridBudget1.Rows.Count; i++)
			{
				DataRow dr = dsPageBreak.Tables[0].Rows.Find(gridBudget1[i, "SNo"].ToString().Trim());
				if (dr == null)
				{
					dr = dsPageBreak.Tables[0].NewRow();
					dr["projectCode"] = ProjectCode;
					dr["sNo"] = gridBudget1[i, "sNo"].ToString().Trim();
					dr["IsBid"] = ((gridBudget1[i, "IsBid"] != null && (bool)gridBudget1[i, "IsBid"]) ? "Y" : "N");
					dr["IsFormulaChangeKind"] = ((gridBudget1[i, "IsFormulaChangeKind"] != null && (bool)gridBudget1[i, "IsFormulaChangeKind"]) ? "Y" : "N");
					dr["FormulaNewName"] = ((gridBudget1[i, "FormulaNewName"] != null) ? gridBudget1[i, "FormulaNewName"].ToString().Trim() : string.Empty);
					dsPageBreak.Tables[0].Rows.Add(dr);
				}
				else
				{
					dr["IsBid"] = ((gridBudget1[i, "IsBid"] != null && (bool)gridBudget1[i, "IsBid"]) ? "Y" : "N");
					dr["IsFormulaChangeKind"] = ((gridBudget1[i, "IsFormulaChangeKind"] != null && (bool)gridBudget1[i, "IsFormulaChangeKind"]) ? "Y" : "N");
					dr["FormulaNewName"] = ((gridBudget1[i, "FormulaNewName"] != null) ? gridBudget1[i, "FormulaNewName"].ToString().Trim() : string.Empty);
				}
				if (sSrcKind.ToUpper() == "BUD" && dr["IsBid"].ToString() == "N")
				{
					DataView dv = new DataView(dsBudPCalsCustomVar.Tables[0], "sNo=" + dr["sNo"].ToString(), string.Empty, DataViewRowState.CurrentRows);
					if (dv.Count > 0)
					{
						string ItemNo = "【項次】" + ArchConvert.Obj2String(gridBudget1[i, "ItemNo"]);
						string cName = "【項目】" + ArchConvert.Obj2String(gridBudget1[i, "CName"]);
						builder.Append(ItemNo + cName + "\n");
					}
				}
			}
			if (builder.Length > 0)
			{
				MessageBox.Show(builder.ToString() + "有設定自訂變數必須發包，請重新設定", "發包設定錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			ExecResult ER = pagebreak.GetDatasetUpdate(dsPageBreak);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show("錯誤：" + ER.Message);
			}
		}
		else
		{
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = UserID;
			DataTable DT_IsBid = DBCLS.GetUserDefine("Select SNo, IsBid From " + sSrcKind + "PageBreak Where ProjectCode='" + ProjectCode + "' --and ( IsBid is not null and IsBid = 'Y')");
			for (int i = 1; i < gridBudget1.Rows.Count; i++)
			{
				if (DT_IsBid.Rows.Count == 0 && (bool)gridBudget1[i, "IsBid"])
				{
					object obj = sSQL;
					sSQL = string.Concat(obj, " Insert Into ", sSrcKind, "PageBreak (ProjectCode, SNo, IsBid) values ('", ProjectCode, "',", gridBudget1[i, "SNo"].ToString(), ",'Y') ", '\r');
				}
				else if (DT_IsBid.Rows.Count > 0)
				{
					DataRow[] DR1 = DT_IsBid.Select("SNo ='" + gridBudget1[i, "SNo"].ToString().Trim() + "' ");
					if ((bool)gridBudget1[i, "IsBid"] && DR1.Length > 0)
					{
						object obj = sSQL;
						sSQL = string.Concat(obj, " Update ", sSrcKind, "PageBreak Set IsBid ='Y'  Where ProjectCode = '", ProjectCode, "' and SNo=", gridBudget1[i, "SNo"].ToString(), " ", '\r');
					}
					else if (!(bool)gridBudget1[i, "IsBid"] && DR1.Length > 0)
					{
						object obj = sSQL;
						sSQL = string.Concat(obj, " Update ", sSrcKind, "PageBreak Set IsBid = null  Where ProjectCode = '", ProjectCode, "' and SNo=", gridBudget1[i, "SNo"].ToString(), " ", '\r');
					}
					else if ((bool)gridBudget1[i, "IsBid"] && DR1.Length == 0)
					{
						object obj = sSQL;
						sSQL = string.Concat(obj, " Insert Into ", sSrcKind, "PageBreak (ProjectCode, SNo, IsBid) values ('", ProjectCode, "',", gridBudget1[i, "SNo"].ToString(), ",'Y') ", '\r');
					}
				}
				if (sSQL != "" && i % 50 == 0)
				{
					DBCLS.ExecuteCommand(sSQL);
					sSQL = "";
				}
			}
			if (sSQL != "")
			{
				DBCLS.ExecuteCommand(sSQL);
			}
			DBCLS = null;
		}
		CommonMethods.WriteIniValue("BidSet", "State", "FALSE");
		CommonMethods.WriteIniValue("BidSet", "StateAdd", "FALSE");
		base.DialogResult = DialogResult.OK;
	}

	private void gridBudget1_BeforeEdit(object sender, RowColEventArgs e)
	{
		if (e.Col == gridBudget1.Cols["FormulaNewName"].SafeIndex && (gridBudget1[e.Row, gridBudget1.Cols["IsFormulaChangeKind"].SafeIndex] == null || !Convert.ToBoolean(gridBudget1[e.Row, gridBudget1.Cols["IsFormulaChangeKind"].SafeIndex])))
		{
			e.Cancel = true;
			gridBudget1.Col = 0;
		}
	}

	private void gridBudget1_AfterEdit(object sender, RowColEventArgs e)
	{
		if (e.Row < 0)
		{
			return;
		}
		string PrintNo = gridBudget1[e.Row, "PrintNo"].ToString().Trim();
		string sPrintNo = gridBudget1[e.Row, "PrintNo"].ToString().Trim();
		bool flag = (bool)gridBudget1[e.Row, "IsBid"];
		if (e.Col == gridBudget1.Cols["IsBid"].SafeIndex)
		{
			if (!gridBudget1.Rows[e.Row].Visible)
			{
				gridBudget1[e.Row, "IsBid"] = !(bool)gridBudget1[e.Row, "IsBid"];
				e.Cancel = true;
			}
			if (e.Row == gridBudget1.Rows.Count - 1)
			{
				gridBudget1[e.Row, "IsBid"] = !(bool)gridBudget1[e.Row, "IsBid"];
				e.Cancel = true;
				return;
			}
			bool flag2 = false;
			Node LastNode = gridBudget1.Rows[e.Row].Node.GetNode(NodeTypeEnum.LastChild);
			if (LastNode == null)
			{
				if (!flag)
				{
					PrintNo = gridBudget1[e.Row, "PrintNo"].ToString().Trim();
					int oLevel = 1;
					int iLevel = 1;
					int iNum = PrintNo.Length - 4;
					if (iNum > 0)
					{
						oLevel = iNum / 4;
					}
					for (int i = 1; i < gridBudget1.Rows.Count; i++)
					{
						string TempPrintNo = gridBudget1[i, "PrintNo"].ToString().Trim();
						iNum = TempPrintNo.Length;
						if (iNum > 0)
						{
							iLevel = iNum / 4;
						}
						if (oLevel == iLevel && (bool)gridBudget1[i, "IsBid"])
						{
							return;
						}
					}
				}
				int iCount = sPrintNo.Length - 4;
				iCount /= 4;
				int j = 4;
				ArrayList aPrintNo = new ArrayList();
				for (int i = 0; i < iCount; i++)
				{
					string sNo = sPrintNo.Substring(0, j);
					aPrintNo.Add(sNo);
					j += 4;
				}
				if (aPrintNo.Count <= 0)
				{
					return;
				}
				for (int k = 0; k < aPrintNo.Count; k++)
				{
					for (int i = 1; i < gridBudget1.Rows.Count; i++)
					{
						if (aPrintNo[k].ToString().Trim() == gridBudget1[i, "PrintNo"].ToString().Trim())
						{
							gridBudget1[i, "IsBid"] = (bool)gridBudget1[e.Row, "IsBid"];
							break;
						}
					}
				}
				return;
			}
			int iii = LastNode.Row.SafeIndex;
			for (int i = e.Row + 1; i <= iii; i++)
			{
				gridBudget1[i, "IsBid"] = (bool)gridBudget1[e.Row, "IsBid"];
			}
		}
		if (gridBudget1[e.Row, "Kind"].ToString().Trim() == "B")
		{
			string LPrintNo = gridBudget1[e.Row, "PrintNo"].ToString().Trim();
			int oLevel = 1;
			int iLevel = 1;
			oLevel = LPrintNo.Length / 4;
			for (int i = 1; i < gridBudget1.Rows.Count; i++)
			{
				iLevel = gridBudget1[i, "PrintNo"].ToString().Trim().Length / 4;
				if (iLevel >= oLevel)
				{
					string TmpNo = gridBudget1[i, "PrintNo"].ToString().Trim().Substring(0, oLevel * 4);
					if (LPrintNo == TmpNo)
					{
						gridBudget1[i, "IsBid"] = (bool)gridBudget1[e.Row, "IsBid"];
					}
				}
			}
		}
		if (!flag)
		{
			PrintNo = gridBudget1[e.Row, "PrintNo"].ToString().Trim();
			int oLevel = 1;
			int iLevel = 1;
			int iNum = PrintNo.Length - 4;
			if (iNum > 0)
			{
				oLevel = iNum / 4;
			}
			for (int i = 1; i < gridBudget1.Rows.Count; i++)
			{
				string TempPrintNo = gridBudget1[i, "PrintNo"].ToString().Trim();
				iNum = TempPrintNo.Length;
				if (iNum > 0)
				{
					iLevel = iNum / 4;
				}
				if (oLevel == iLevel && (bool)gridBudget1[i, "IsBid"])
				{
					return;
				}
			}
		}
		int iCount2 = sPrintNo.Length - 4;
		iCount2 /= 4;
		int q = 4;
		ArrayList aPrintNo2 = new ArrayList();
		for (int i = 0; i < iCount2; i++)
		{
			string sNo = sPrintNo.Substring(0, q);
			aPrintNo2.Add(sNo);
			q += 4;
		}
		if (aPrintNo2.Count > 0)
		{
			for (int k = 0; k < aPrintNo2.Count; k++)
			{
				for (int i = 1; i < gridBudget1.Rows.Count; i++)
				{
					if (aPrintNo2[k].ToString().Trim() == gridBudget1[i, "PrintNo"].ToString().Trim())
					{
						gridBudget1[i, "IsBid"] = (bool)gridBudget1[e.Row, "IsBid"];
						break;
					}
				}
			}
		}
		if (e.Col == gridBudget1.Cols["IsFormulaChangeKind"].SafeIndex)
		{
			if (gridBudget1[e.Row, "kind"] == null || "FSU".IndexOf(gridBudget1[e.Row, "kind"].ToString().ToUpper().Trim()) == -1)
			{
				MessageBox.Show("只能點選設定為公式項目的主項大類");
				gridBudget1[e.Row, "IsFormulaChangeKind"] = false;
			}
			else if (gridBudget1[e.Row, gridBudget1.Cols["IsFormulaChangeKind"].SafeIndex] != null && Convert.ToBoolean(gridBudget1[e.Row, gridBudget1.Cols["IsFormulaChangeKind"].SafeIndex]) && gridBudget1[e.Row, gridBudget1.Cols["FormulaNewName"].SafeIndex] != null && gridBudget1[e.Row, gridBudget1.Cols["FormulaNewName"].SafeIndex].ToString().Trim() == string.Empty)
			{
				gridBudget1[e.Row, gridBudget1.Cols["FormulaNewName"].SafeIndex] = gridBudget1[e.Row, gridBudget1.Cols["CName"].SafeIndex];
			}
		}
	}

	private DataTable GetLikePrintNo(string PrintNo)
	{
		DataTable dt = new DataTable();
		string ls_selectstr = "Select * from budItemA where ProjectCode = '" + ProjectCode + "'  and printNo like '" + PrintNo + "%'";
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = UserID;
		return DBCLS.GetUserDefine(ls_selectstr);
	}

	private void levelSwitchButton_LevelSwitchButtonsClicked()
	{
		gridBudget1.Tree.Show(levelSwitchButton.SelectedLevel);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetBidSet));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.levelSwitchButton = new Archnowledge.Pcces.PccesMain.ArchControls.LevelSwitchButton();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.gridBudget1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel4.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridBudget1).BeginInit();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.groupBox1);
		this.panel1.Controls.Add(this.A_Btn_Cncl);
		this.panel1.Controls.Add(this.A_Btn_Next);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 422);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(672, 44);
		this.panel1.TabIndex = 10;
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(672, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.A_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Cncl.Appearance = appearance1;
		this.A_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Cncl.Location = new System.Drawing.Point(576, 9);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.ShowFocusRect = false;
		this.A_Btn_Cncl.ShowOutline = false;
		this.A_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Cncl.SupportThemes = false;
		this.A_Btn_Cncl.TabIndex = 2;
		this.A_Btn_Cncl.Text = "取消";
		this.A_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.ImageHAlign = Infragistics.Win.HAlign.Left;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Next.Appearance = appearance2;
		this.A_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Next.Location = new System.Drawing.Point(484, 9);
		this.A_Btn_Next.Name = "A_Btn_Next";
		this.A_Btn_Next.ShowFocusRect = false;
		this.A_Btn_Next.ShowOutline = false;
		this.A_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Next.SupportThemes = false;
		this.A_Btn_Next.TabIndex = 1;
		this.A_Btn_Next.Text = "確定";
		this.A_Btn_Next.Click += new System.EventHandler(A_Btn_Next_Click);
		this.panel2.BackColor = System.Drawing.Color.White;
		this.panel2.Controls.Add(this.panel4);
		this.panel2.Controls.Add(this.ultraLabel7);
		this.panel2.Controls.Add(this.ultraLabel6);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(672, 72);
		this.panel2.TabIndex = 11;
		this.panel4.Controls.Add(this.levelSwitchButton);
		this.panel4.Location = new System.Drawing.Point(16, 48);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(166, 24);
		this.panel4.TabIndex = 17;
		this.levelSwitchButton.Location = new System.Drawing.Point(0, 2);
		this.levelSwitchButton.Name = "levelSwitchButton";
		this.levelSwitchButton.Size = new System.Drawing.Size(166, 22);
		this.levelSwitchButton.TabIndex = 0;
		this.levelSwitchButton.LevelSwitchButtonsClicked += new Archnowledge.Pcces.PccesMain.ArchControls.LevelSwitchButton.LevelSwitchButtonClickHandler(levelSwitchButton_LevelSwitchButtonsClicked);
		appearance3.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance3;
		this.ultraLabel7.Location = new System.Drawing.Point(26, 29);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(622, 20);
		this.ultraLabel7.TabIndex = 6;
		this.ultraLabel7.Text = "如果你要特別指定某些項目不發包，請將不發包的項目勾選拿掉(此功能使用於製作電子檔)";
		appearance4.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance4;
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(10, 8);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 5;
		this.ultraLabel6.Text = "發包設定";
		this.panel3.Controls.Add(this.gridBudget1);
		this.panel3.Controls.Add(this.ultraStatusBar1);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 72);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(672, 350);
		this.panel3.TabIndex = 12;
		this.gridBudget1._ExcelFileName = "";
		this.gridBudget1._ExcelSheeName = "";
		this.gridBudget1._IsOpenExcelAfterExport = false;
		this.gridBudget1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridBudget1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridBudget1.ColumnInfo = resources.GetString("gridBudget1.ColumnInfo");
		this.gridBudget1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridBudget1.ExtendLastCol = true;
		this.gridBudget1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridBudget1.ForeColor = System.Drawing.Color.Black;
		this.gridBudget1.Location = new System.Drawing.Point(0, 0);
		this.gridBudget1.Name = "gridBudget1";
		this.gridBudget1.Rows.Count = 1;
		this.gridBudget1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridBudget1.ShowCursor = true;
		this.gridBudget1.ShowSort = false;
		this.gridBudget1.ShowToolTipOnNarrowColumn = true;
		this.gridBudget1.Size = new System.Drawing.Size(672, 324);
		this.gridBudget1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridBudget1.Styles"));
		this.gridBudget1.TabIndex = 1;
		this.gridBudget1.Tree.Column = 1;
		this.gridBudget1.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridBudget1.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridBudget1_AfterEdit);
		this.gridBudget1.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridBudget1_BeforeEdit);
		appearance5.FontData.SizeInPoints = 11f;
		this.ultraStatusBar1.Appearance = appearance5;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 324);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		appearance6.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
		appearance6.BackColor2 = System.Drawing.Color.Navy;
		appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		ultraStatusPanel1.ProgressBarInfo.Appearance = appearance6;
		ultraStatusPanel1.Text = "資料筆數：";
		ultraStatusPanel1.Width = 200;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		appearance7.BackColor = System.Drawing.Color.LightSlateGray;
		appearance7.BackColor2 = System.Drawing.Color.DarkBlue;
		appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		ultraStatusPanel2.ProgressBarInfo.FillAppearance = appearance7;
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		ultraStatusPanel2.Style = Infragistics.Win.UltraWinStatusBar.PanelStyle.Progress;
		ultraStatusPanel2.Width = 0;
		appearance8.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance8.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraStatusPanel3.Appearance = appearance8;
		ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel3.Text = "客服電話:(02)2708-8090";
		ultraStatusPanel3.Width = 200;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[3] { ultraStatusPanel1, ultraStatusPanel2, ultraStatusPanel3 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(672, 26);
		this.ultraStatusBar1.SupportThemes = false;
		this.ultraStatusBar1.TabIndex = 2;
		this.ultraStatusBar1.Text = "ultraStatusBar1";
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.White;
		this.imageList2.Images.SetKeyName(0, "");
		this.imageList2.Images.SetKeyName(1, "");
		this.imageList2.Images.SetKeyName(2, "");
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 18);
		base.CancelButton = this.A_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(672, 466);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormBudgetBidSet";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "發包設定";
		base.Load += new System.EventHandler(FormBudgetBidSet_Load);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridBudget1).EndInit();
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
