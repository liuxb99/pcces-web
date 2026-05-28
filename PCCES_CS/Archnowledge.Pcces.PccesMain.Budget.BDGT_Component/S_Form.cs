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
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTabs;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.Budget.BDGT_Component;

public class S_Form : UserControl
{
	private int F_Issue;

	private PccesFormAction F_ActionName;

	private string F_FORM_STATUS = "INI";

	private string userID;

	private string projectCode = "";

	private string F_ParentPrintNo = "";

	private string F_ParentsNo = "";

	private DataTable F_STable1 = new DataTable();

	private DataTable dtItemC = new DataTable();

	private DataTable F_DT_Var = new DataTable();

	private bool EnableFormula = false;

	private Archnowledge.Pcces.DomainModule.General.PubProject thePubProject = new Archnowledge.Pcces.DomainModule.General.PubProject();

	private IContainer components;

	private UltraButton BtnPick;

	private UltraTabControl ultraTabControl1;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl ultraTabPageControl1;

	private C1FlexGrid gridItemB;

	private Panel panel1;

	private UltraLabel lbFormula;

	private UltraLabel ultraLabel1;

	private UltraTabPageControl ultraTabPageControl2;

	private Panel panel2;

	private Panel panel3;

	private C1FlexGrid gridItemC;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel5;

	private UltraButton ultraButton1;

	private UltraButton ultraButton2;

	private UltraTextEditor txt_Rate;

	private UltraTextEditor txtUpper;

	private UltraTextEditor txtLower;

	private UltraToolbarsManager ultraToolbarsManager1;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Bottom;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Right;

	private ImageList imageList2;

	private UltraButton btnCheckFormula;

	private TextBox textBox1;

	public int _Issue
	{
		get
		{
			return F_Issue;
		}
		set
		{
			F_Issue = value;
		}
	}

	public PccesFormAction _ActionName
	{
		get
		{
			return F_ActionName;
		}
		set
		{
			F_ActionName = value;
		}
	}

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

	public S_Form()
	{
		InitializeComponent();
		CellStyle cs1 = gridItemC.Styles.Add("EditMode");
		cs1.DataType = typeof(Image);
		cs1.ImageAlign = ImageAlignEnum.RightCenter;
	}

	private void S_Form_Load(object sender, EventArgs e)
	{
		txtLower.Text = "";
		txtUpper.Text = "";
		txt_Rate.Text = "";
		projectCode = (base.ParentForm as FormBudgetEditMain).ProjectCode;
		F_ParentPrintNo = (base.ParentForm as FormBudgetEditMain).PrintNo;
		F_ParentsNo = (base.ParentForm as FormBudgetEditMain).Item_sNo.ToString();
		if (F_ActionName == PccesFormAction.BUD)
		{
			gridItemB.Cols["VarSign"].Visible = true;
		}
		LoadingData();
		BindDataToGrid();
		PreSetRange();
		F_FORM_STATUS = "NOR";
	}

	private void PreSetRange()
	{
		if (gridItemC.Rows.Count == 1)
		{
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(userID);
			aArr.Add("檢查通過後, 將資料存起來(分段計價)");
			ItemC dbItemC = new ItemC(aArr);
			dbItemC.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			dbItemC.ps_projectCode = projectCode;
			dbItemC.ps_printNo = F_ParentPrintNo;
			dbItemC.ps_sNo = F_ParentsNo;
			dbItemC.ps_Issue = F_Issue.ToString();
			dbItemC.ps_down = "0";
			dbItemC.ps_up = "5000000";
			dbItemC.ps_rate = "3.0";
			dbItemC.InseItem();
			dbItemC.ps_down = "5000000";
			dbItemC.ps_up = "25000000";
			dbItemC.ps_rate = "1.5";
			dbItemC.InseItem();
			dbItemC.ps_down = "25000000";
			dbItemC.ps_up = "100000000";
			dbItemC.ps_rate = "1";
			dbItemC.InseItem();
			dbItemC.ps_down = "100000000";
			dbItemC.ps_up = "500000000";
			dbItemC.ps_rate = "0.7";
			dbItemC.InseItem();
			dbItemC.ps_down = "500000000";
			dbItemC.ps_up = "999999999999999999";
			dbItemC.ps_rate = "0.5";
			dbItemC.InseItem();
			LoadingData();
			BindDataToGrid();
		}
	}

	private int SelectedItems(int Index)
	{
		int RetV = 0;
		if (Index == 1)
		{
			for (int i = 1; i < gridItemB.Rows.Count; i++)
			{
				if (gridItemB.Rows[i].Selected)
				{
					RetV++;
				}
			}
		}
		else
		{
			for (int i = 1; i < gridItemC.Rows.Count; i++)
			{
				if (gridItemC.Rows[i].Selected)
				{
					RetV++;
				}
			}
		}
		return RetV;
	}

	private void LoadingData()
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("載入加總項目資料--" + projectCode);
		ItemB dbItemB = new ItemB(aArr);
		dbItemB.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbItemB.ps_parentCode = F_ParentPrintNo;
		dbItemB.ps_parentCodeSno = F_ParentsNo;
		dbItemB.ps_Issue = F_Issue.ToString();
		F_STable1 = dbItemB.ListItem("", projectCode, F_ParentsNo);
		ItemC dbItemC = new ItemC(aArr);
		dbItemC.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbItemC.ps_Issue = F_Issue.ToString();
		dtItemC = dbItemC.ListItem("", projectCode, F_ParentsNo);
		if (dtItemC.Columns.IndexOf("formula") >= 0)
		{
			EnableFormula = true;
		}
		else
		{
			EnableFormula = false;
		}
		lbFormula.Visible = EnableFormula;
		gridItemC.Cols["Formula"].Visible = EnableFormula;
		gridItemC.Cols["Formula"].Visible = thePubProject.GetPubProjectEnableNewCalculateCost(projectCode);
		PCals PCLS = new PCals(aArr);
		PCLS.ps_projectCode = projectCode;
		PCLS.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		F_DT_Var = PCLS.GetCustomVarList();
	}

	private ArrayList CheckChosenItemList()
	{
		ArrayList RetV = new ArrayList();
		for (int i = 1; i < gridItemB.Rows.Count; i++)
		{
			RetV.Add(gridItemB[i, "PrintNo"].ToString().Trim());
		}
		return RetV;
	}

	private ArrayList CheckChosenItemSign()
	{
		ArrayList RetV = new ArrayList();
		for (int i = 1; i < gridItemB.Rows.Count; i++)
		{
			string sSign = gridItemB[i, "VarSign"].ToString().Trim();
			int iSignValue = ((sSign == "＋") ? 1 : (-1));
			RetV.Add(iSignValue);
		}
		return RetV;
	}

	private void BindDataToGrid()
	{
		CellStyle CS_Cust = gridItemB.Styles.Add("CustColor");
		CS_Cust.Font = new Font("細明體", 11f, FontStyle.Bold);
		CS_Cust.ForeColor = Color.FromArgb(0, 51, 0);
		CS_Cust.BackColor = Color.FromArgb(255, 204, 153);
		gridItemB.Rows.Count = F_STable1.Rows.Count + 1;
		if (!gridItemB.Cols.Contains("parentCodeSno"))
		{
			Column C_PSno = gridItemB.Cols.Add();
			C_PSno.Name = "parentCodeSno";
			C_PSno.Visible = false;
		}
		if (!gridItemB.Cols.Contains("itemCodeSno"))
		{
			Column C_ISno = gridItemB.Cols.Add();
			C_ISno.Name = "itemCodeSno";
			C_ISno.Visible = false;
		}
		if (!gridItemC.Cols.Contains("sNo"))
		{
			Column C_CSno = gridItemC.Cols.Add();
			C_CSno.Name = "sNo";
			C_CSno.Visible = false;
		}
		for (int i = 0; i < F_STable1.Rows.Count; i++)
		{
			string sPrintNo = F_STable1.Rows[i]["PrintNo"].ToString().Trim();
			bool IsVAR = sPrintNo.Substring(0, 3).ToUpper() == "VAR";
			gridItemB[i + 1, "ItemNo"] = F_STable1.Rows[i]["ItemNo"];
			gridItemB[i + 1, "CName"] = ((!IsVAR) ? F_STable1.Rows[i]["CName"] : GetVarAliasNameByPrintNo(sPrintNo));
			gridItemB[i + 1, "PrintNo"] = F_STable1.Rows[i]["PrintNo"].ToString().Trim();
			gridItemB[i + 1, "VarSign"] = ((F_STable1.Rows[i]["VarSign"].ToString() == "-1") ? "－" : "＋");
			gridItemB[i + 1, "parentCodeSno"] = F_STable1.Rows[i]["parentCodeSno"];
			gridItemB[i + 1, "itemCodeSno"] = F_STable1.Rows[i]["itemCodeSno"];
			if (IsVAR)
			{
				gridItemB.Rows[i + 1].Style = CS_Cust;
			}
		}
		gridItemC.Rows.Count = dtItemC.Rows.Count + 1;
		for (int i = 0; i < dtItemC.Rows.Count; i++)
		{
			Row GridRow = gridItemC.Rows[i + 1];
			GridRow["Lower"] = dtItemC.Rows[i]["down"];
			GridRow["MidText"] = " < 金額 ≦ ";
			GridRow["Upper"] = dtItemC.Rows[i]["up"];
			GridRow["Rate"] = dtItemC.Rows[i]["rate"];
			if (EnableFormula)
			{
				GridRow["Formula"] = dtItemC.Rows[i]["Formula"];
			}
			GridRow["PrintNo"] = dtItemC.Rows[i]["printNo"].ToString().Trim();
			GridRow["sNo"] = dtItemC.Rows[i]["sNo"].ToString().Trim();
		}
		SetColsEditSymbol();
	}

	private string GetVarAliasNameByPrintNo(string sPrintNo)
	{
		string RetV = "";
		for (int i = 0; i < F_DT_Var.Rows.Count; i++)
		{
			if (sPrintNo.Trim() == F_DT_Var.Rows[i]["VarName"].ToString().Trim())
			{
				RetV = F_DT_Var.Rows[i]["VarAlias"].ToString();
				break;
			}
		}
		return RetV;
	}

	private void SetColsEditSymbol()
	{
		for (int i = 1; i < gridItemC.Cols.Count; i++)
		{
			if (gridItemC.Cols[i].AllowEditing)
			{
				CellRange rg = gridItemC.GetCellRange(0, i);
				rg.Style = gridItemC.Styles["EditMode"];
				rg.Image = imageList2.Images[0];
			}
		}
	}

	private void DeleteSection()
	{
		string sQuestionStr = "確定要刪除選擇的 " + SelectedItems(2) + " 筆資料 ?";
		if (MessageBox.Show(this, sQuestionStr, CommonMethods.GetFormTypeTitle(FormType.Budget), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(userID);
			aArr.Add("刪除分段計價公式--" + projectCode);
			ItemC dbItemC = new ItemC(aArr);
			dbItemC.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			dbItemC.ps_projectCode = projectCode;
			dbItemC.ps_printNo = F_ParentPrintNo;
			dbItemC.ps_sNo = F_ParentsNo;
			dbItemC.ps_Issue = F_Issue.ToString();
			for (int i = gridItemC.Rows.Count - 1; i > 0; i--)
			{
				if (gridItemC.Rows[i].Selected)
				{
					dbItemC.ps_up = gridItemC[i, "Upper"].ToString();
					dbItemC.ps_down = gridItemC[i, "Lower"].ToString();
					dbItemC.DeleItem();
				}
			}
		}
		LoadingData();
		BindDataToGrid();
	}

	private void ultraButton2_Click(object sender, EventArgs e)
	{
		string sQuestionStr = "確定要刪除選擇的 " + SelectedItems(1) + " 筆資料 ?";
		if (MessageBox.Show(this, sQuestionStr, CommonMethods.GetFormTypeTitle(FormType.Budget), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(userID);
			aArr.Add("刪除加總項目--" + projectCode);
			ItemB dbItemB = new ItemB(aArr);
			dbItemB.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			dbItemB.ps_projectCode = projectCode;
			dbItemB.ps_parentCode = F_ParentPrintNo;
			dbItemB.ps_Issue = F_Issue.ToString();
			for (int i = gridItemB.Rows.Count - 1; i > 0; i--)
			{
				if (gridItemB.Rows[i].Selected)
				{
					dbItemB.ps_parentCode = F_ParentPrintNo;
					dbItemB.ps_parentCodeSno = F_ParentsNo;
					dbItemB.ps_itemCode = gridItemB[i, "PrintNo"].ToString().Trim();
					dbItemB.ps_itemCodeSno = gridItemB[i, "itemCodeSno"].ToString().Trim();
					dbItemB.DeleItem();
				}
			}
		}
		LoadingData();
		BindDataToGrid();
	}

	private void BtnPick_Click(object sender, EventArgs e)
	{
		Form_FItem_Pick FM_FIT_PK = new Form_FItem_Pick();
		FM_FIT_PK._ActionName = F_ActionName;
		FM_FIT_PK._UserID = userID;
		FM_FIT_PK.ProjectCode = projectCode;
		FM_FIT_PK.ParentCode = F_ParentPrintNo;
		FM_FIT_PK._ParentSNo = F_ParentsNo;
		FM_FIT_PK.ChosenPrintNoList = CheckChosenItemList();
		FM_FIT_PK._ChosenItemSignList = CheckChosenItemSign();
		FM_FIT_PK._ParentSNo = (base.ParentForm as FormBudgetEditMain).Item_sNo.ToString();
		FM_FIT_PK._CallerType = "S";
		FM_FIT_PK._Issue = F_Issue;
		FM_FIT_PK.ShowDialog(this);
		FM_FIT_PK.Dispose();
		FM_FIT_PK = null;
		LoadingData();
		BindDataToGrid();
	}

	private void gridItemB_KeyDown(object sender, KeyEventArgs e)
	{
		if ((!e.Control || e.KeyCode != Keys.Delete) && e.KeyCode != Keys.Delete)
		{
		}
	}

	private void gridItemC_KeyDown(object sender, KeyEventArgs e)
	{
		if ((e.Control && e.KeyCode == Keys.Delete) || e.KeyCode == Keys.Delete)
		{
			DeleteSection();
		}
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		if (txtLower.Text.Trim() == "")
		{
			MessageBox.Show(this, "請填入[下限]金額。", CommonMethods.GetFormTypeTitle(FormType.Budget), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			txtLower.Focus();
			return;
		}
		if (txtUpper.Text.Trim() == "")
		{
			MessageBox.Show(this, "請填入[上限]金額。", CommonMethods.GetFormTypeTitle(FormType.Budget), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			txtUpper.Focus();
			return;
		}
		if (txt_Rate.Text.Trim() == "")
		{
			MessageBox.Show(this, "請填入[比率]。", CommonMethods.GetFormTypeTitle(FormType.Budget), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			txt_Rate.Focus();
			return;
		}
		try
		{
			decimal D1 = Convert.ToDecimal(txtLower.Text);
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "BDGT_Component.S_Form.cs" + ex.Message);
			MessageBox.Show(this, "下限金額不合理，請重新輸入！", CommonMethods.GetFormTypeTitle(FormType.Budget), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtLower.Focus();
			return;
		}
		try
		{
			decimal D2 = Convert.ToDecimal(txtUpper.Text);
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "BDGT_Component.S_Form.cs" + ex.Message);
			MessageBox.Show(this, "上限金額不合理，請重新輸入！", CommonMethods.GetFormTypeTitle(FormType.Budget), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtUpper.Focus();
			return;
		}
		try
		{
			decimal D3 = Convert.ToDecimal(txt_Rate.Text);
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "BDGT_Component.S_Form.cs" + ex.Message);
			MessageBox.Show(this, "比率不合理，請重新輸入！", CommonMethods.GetFormTypeTitle(FormType.Budget), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txt_Rate.Focus();
			return;
		}
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("檢查通過後, 將資料存起來(分段計價)");
		ItemC dbItemC = new ItemC(aArr);
		dbItemC.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbItemC.ps_projectCode = projectCode;
		dbItemC.ps_printNo = F_ParentPrintNo;
		dbItemC.ps_sNo = F_ParentsNo;
		dbItemC.ps_down = txtLower.Text.Trim();
		dbItemC.ps_up = txtUpper.Text.Trim();
		dbItemC.ps_rate = txt_Rate.Text.Trim();
		dbItemC.ps_Issue = F_Issue.ToString();
		try
		{
			int iStatus = dbItemC.InseItem();
			if (iStatus == -2)
			{
				MessageBox.Show(this, "資料不正確，上限及下限的設定區間需要連續，請重新確認！", CommonMethods.GetFormTypeTitle(FormType.Budget), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(this, "資料不正確，請重新確認！: " + ex.Message, CommonMethods.GetFormTypeTitle(FormType.Budget), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		LoadingData();
		BindDataToGrid();
	}

	private void txtLower_Validating(object sender, CancelEventArgs e)
	{
		if (!CommonMethods.CheckValidString((sender as UltraTextEditor).Text))
		{
			e.Cancel = true;
		}
		if ((sender as UltraTextEditor).Text.Trim() != "" && (sender as UltraTextEditor).Name == "txtLower")
		{
			try
			{
				Convert.ToDouble(txtLower.Text.Trim());
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "BDGT_Component.S_Form.cs" + ex.Message);
				MessageBox.Show(this, "下限金額不正確！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtLower.Focus();
				return;
			}
		}
		if ((sender as UltraTextEditor).Text.Trim() != "" && (sender as UltraTextEditor).Name == "txtUpper")
		{
			try
			{
				Convert.ToDouble(txtUpper.Text.Trim());
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "BDGT_Component.S_Form.cs" + ex.Message);
				MessageBox.Show(this, "上限金額不正確！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtUpper.Focus();
				return;
			}
		}
		if ((sender as UltraTextEditor).Text.Trim() != "" && (sender as UltraTextEditor).Name == "txt_Rate")
		{
			try
			{
				Convert.ToDouble(txt_Rate.Text.Trim());
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "BDGT_Component.S_Form.cs" + ex.Message);
				MessageBox.Show(this, "變動費率不正確！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txt_Rate.Focus();
				return;
			}
		}
		string sStr1 = (sender as UltraTextEditor).Text.Trim();
		try
		{
			double dValue = Convert.ToDouble(sStr1);
			if (dValue < 0.0)
			{
				MessageBox.Show(this, "數值不可小於 0！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				(sender as UltraTextEditor).Focus();
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "BDGT_Component.S_Form.cs" + ex.Message);
		}
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		string key = e.Tool.Key;
		if (key != null && key == "mnuDelete")
		{
			DeleteSection();
		}
	}

	private void gridItemC_MouseDown(object sender, MouseEventArgs e)
	{
		int rowIndex = gridItemC.MouseRow;
		if (gridItemC.MouseRow <= 0 || gridItemC.MouseCol <= 0)
		{
			ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = false;
			return;
		}
		gridItemC.Row = rowIndex;
		ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = true;
	}

	private void gridItemC_AfterEdit(object sender, RowColEventArgs e)
	{
		if (F_FORM_STATUS != "NOR")
		{
			return;
		}
		string ColName = gridItemC.Cols[e.Col].Name.ToLower();
		if (!(ColName == "rate") && !(ColName == "formula"))
		{
			return;
		}
		if (ColName == "formula" && gridItemC[e.Row, ColName].ToString().Length >= 150)
		{
			MessageBox.Show(gridItemC[e.Row, ColName].ToString() + " 公式設定長度太長，無法儲存！", "錯誤", MessageBoxButtons.OK);
			return;
		}
		string sSQL = "Update BudItemC  Set " + ColName + " ='" + gridItemC[e.Row, ColName].ToString() + "' Where ProjectCode = '" + projectCode + "' And sNo = " + F_ParentsNo + " And down = " + PubTools.Str2Double(gridItemC[e.Row, "Lower"].ToString()) + "   And up = " + PubTools.Str2Double(gridItemC[e.Row, "Upper"].ToString());
		if (F_ActionName == PccesFormAction.SubChange)
		{
			sSQL = sSQL + " and chgCount=" + F_Issue + " ";
		}
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = userID;
		DBCLS.ExecuteCommand(sSQL);
	}

	private void btnCheckFormula_Click(object sender, EventArgs e)
	{
		bool IsValid = true;
		gridItemC.Rows.Count = dtItemC.Rows.Count + 1;
		for (int i = 1; i < gridItemC.Rows.Count; i++)
		{
			string Formula = gridItemC[i, "Formula"].ToString().Trim();
			if (Formula != "")
			{
				ExecResult ER = PubTools.ArchChkFormula2("[Value]", Formula);
				if (ER.ReturnCode != 0)
				{
					IsValid = false;
					MessageBox.Show(Formula + " 公式設定不正確，你可以參使用者自訂公式之說明 : " + ER.Message, "錯誤", MessageBoxButtons.OK);
				}
			}
		}
		if (IsValid)
		{
			MessageBox.Show(this, "公式設定正確！", CommonMethods.GetFormTypeTitle(FormType.Budget), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.BDGT_Component.S_Form));
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool1 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool2 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Popup1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
		this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.gridItemB = new C1.Win.C1FlexGrid.C1FlexGrid();
		this.panel1 = new System.Windows.Forms.Panel();
		this.BtnPick = new Infragistics.Win.Misc.UltraButton();
		this.ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.gridItemC = new C1.Win.C1FlexGrid.C1FlexGrid();
		this.panel3 = new System.Windows.Forms.Panel();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.txt_Rate = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.txtUpper = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.txtLower = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.panel2 = new System.Windows.Forms.Panel();
		this.ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.lbFormula = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this._FormSys_B_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.btnCheckFormula = new Infragistics.Win.Misc.UltraButton();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.ultraTabPageControl1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridItemB).BeginInit();
		this.ultraTabPageControl2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridItemC).BeginInit();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txt_Rate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUpper).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLower).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraTabControl1).BeginInit();
		this.ultraTabControl1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		base.SuspendLayout();
		this.ultraTabPageControl1.Controls.Add(this.ultraButton2);
		this.ultraTabPageControl1.Controls.Add(this.gridItemB);
		this.ultraTabPageControl1.Controls.Add(this.panel1);
		this.ultraTabPageControl1.Controls.Add(this.BtnPick);
		this.ultraTabPageControl1.Location = new System.Drawing.Point(2, 26);
		this.ultraTabPageControl1.Name = "ultraTabPageControl1";
		this.ultraTabPageControl1.Size = new System.Drawing.Size(680, 174);
		appearance1.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraButton2.Appearance = appearance1;
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		appearance2.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance2.BackColor2 = System.Drawing.Color.White;
		appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.ultraButton2.HotTrackAppearance = appearance2;
		this.ultraButton2.HotTracking = true;
		this.ultraButton2.Location = new System.Drawing.Point(129, 146);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(128, 28);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 12;
		this.ultraButton2.Text = "刪除選擇項目";
		this.ultraButton2.Visible = false;
		this.ultraButton2.Click += new System.EventHandler(ultraButton2_Click);
		this.gridItemB.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridItemB.ColumnInfo = resources.GetString("gridItemB.ColumnInfo");
		this.gridItemB.ExtendLastCol = true;
		this.gridItemB.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridItemB.ForeColor = System.Drawing.Color.Black;
		this.gridItemB.Location = new System.Drawing.Point(0, 8);
		this.gridItemB.Name = "gridItemB";
		this.gridItemB.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridItemB.Size = new System.Drawing.Size(682, 137);
		this.gridItemB.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridItemB.Styles"));
		this.gridItemB.TabIndex = 0;
		this.gridItemB.KeyDown += new System.Windows.Forms.KeyEventHandler(gridItemB_KeyDown);
		this.panel1.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(680, 8);
		this.panel1.TabIndex = 1;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnPick.Appearance = appearance3;
		this.BtnPick.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		appearance4.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance4.BackColor2 = System.Drawing.Color.White;
		appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.BtnPick.HotTrackAppearance = appearance4;
		this.BtnPick.HotTracking = true;
		this.BtnPick.Location = new System.Drawing.Point(-1, 146);
		this.BtnPick.Name = "BtnPick";
		this.BtnPick.ShowFocusRect = false;
		this.BtnPick.ShowOutline = false;
		this.BtnPick.Size = new System.Drawing.Size(128, 28);
		this.BtnPick.SupportThemes = false;
		this.BtnPick.TabIndex = 11;
		this.BtnPick.Text = "加總項目挑選";
		this.BtnPick.Click += new System.EventHandler(BtnPick_Click);
		this.ultraTabPageControl2.Controls.Add(this.gridItemC);
		this.ultraTabPageControl2.Controls.Add(this.panel3);
		this.ultraTabPageControl2.Controls.Add(this.panel2);
		this.ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabPageControl2.Name = "ultraTabPageControl2";
		this.ultraTabPageControl2.Size = new System.Drawing.Size(680, 174);
		this.gridItemC.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.gridItemC.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
		this.gridItemC.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridItemC.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.gridItemC.ColumnInfo = resources.GetString("gridItemC.ColumnInfo");
		this.ultraToolbarsManager1.SetContextMenuUltra(this.gridItemC, "Popup1");
		this.gridItemC.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridItemC.ExtendLastCol = true;
		this.gridItemC.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridItemC.ForeColor = System.Drawing.Color.Black;
		this.gridItemC.Location = new System.Drawing.Point(0, 40);
		this.gridItemC.Name = "gridItemC";
		this.gridItemC.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridItemC.Size = new System.Drawing.Size(680, 134);
		this.gridItemC.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridItemC.Styles"));
		this.gridItemC.TabIndex = 4;
		this.gridItemC.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridItemC_AfterEdit);
		this.gridItemC.KeyDown += new System.Windows.Forms.KeyEventHandler(gridItemC_KeyDown);
		this.gridItemC.MouseDown += new System.Windows.Forms.MouseEventHandler(gridItemC_MouseDown);
		this.panel3.Controls.Add(this.ultraButton1);
		this.panel3.Controls.Add(this.ultraLabel5);
		this.panel3.Controls.Add(this.txt_Rate);
		this.panel3.Controls.Add(this.ultraLabel4);
		this.panel3.Controls.Add(this.txtUpper);
		this.panel3.Controls.Add(this.ultraLabel3);
		this.panel3.Controls.Add(this.txtLower);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel3.Location = new System.Drawing.Point(0, 8);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(680, 32);
		this.panel3.TabIndex = 3;
		appearance23.TextVAlign = Infragistics.Win.VAlign.Top;
		this.ultraButton1.Appearance = appearance23;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		appearance24.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance24.BackColor2 = System.Drawing.Color.White;
		appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.ultraButton1.HotTrackAppearance = appearance24;
		this.ultraButton1.HotTracking = true;
		this.ultraButton1.Location = new System.Drawing.Point(604, 4);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(72, 25);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 6;
		this.ultraButton1.Text = "新增";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		appearance25.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance25;
		this.ultraLabel5.Location = new System.Drawing.Point(584, 4);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(20, 23);
		this.ultraLabel5.TabIndex = 5;
		this.ultraLabel5.Text = "%";
		this.txt_Rate.AutoSize = true;
		this.txt_Rate.Location = new System.Drawing.Point(502, 4);
		this.txt_Rate.Name = "txt_Rate";
		this.txt_Rate.Size = new System.Drawing.Size(80, 21);
		this.txt_Rate.TabIndex = 4;
		this.txt_Rate.Text = "[txt_Rate]";
		this.txt_Rate.Validating += new System.ComponentModel.CancelEventHandler(txtLower_Validating);
		appearance26.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance26.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance26;
		this.ultraLabel4.Location = new System.Drawing.Point(428, 5);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(72, 23);
		this.ultraLabel4.TabIndex = 3;
		this.ultraLabel4.Text = "變動費率";
		this.txtUpper.AutoSize = true;
		this.txtUpper.Location = new System.Drawing.Point(256, 4);
		this.txtUpper.Name = "txtUpper";
		this.txtUpper.Size = new System.Drawing.Size(164, 21);
		this.txtUpper.TabIndex = 2;
		this.txtUpper.Text = "[txtUpper]";
		this.txtUpper.Validating += new System.ComponentModel.CancelEventHandler(txtLower_Validating);
		appearance27.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance27.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance27;
		this.ultraLabel3.Location = new System.Drawing.Point(156, 6);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(92, 23);
		this.ultraLabel3.TabIndex = 1;
		this.ultraLabel3.Text = "< 金額 ≦";
		this.txtLower.AutoSize = true;
		this.txtLower.Location = new System.Drawing.Point(8, 4);
		this.txtLower.Name = "txtLower";
		this.txtLower.Size = new System.Drawing.Size(136, 21);
		this.txtLower.TabIndex = 0;
		this.txtLower.Text = "[txtLower]";
		this.txtLower.Validating += new System.ComponentModel.CancelEventHandler(txtLower_Validating);
		this.panel2.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(680, 8);
		this.panel2.TabIndex = 2;
		appearance28.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance28.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance28.ForeColor = System.Drawing.Color.Black;
		appearance28.TextVAlign = Infragistics.Win.VAlign.Top;
		this.ultraTabControl1.ActiveTabAppearance = appearance28;
		appearance29.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance29.BackColor2 = System.Drawing.Color.FromArgb(102, 153, 255);
		appearance29.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance29.TextVAlign = Infragistics.Win.VAlign.Top;
		this.ultraTabControl1.Appearance = appearance29;
		appearance30.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance30.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraTabControl1.ClientAreaAppearance = appearance30;
		this.ultraTabControl1.Controls.Add(this.ultraTabSharedControlsPage1);
		this.ultraTabControl1.Controls.Add(this.ultraTabPageControl1);
		this.ultraTabControl1.Controls.Add(this.ultraTabPageControl2);
		this.ultraTabControl1.FlatMode = true;
		this.ultraTabControl1.HotTrack = true;
		this.ultraTabControl1.InterTabSpacing = new Infragistics.Win.DefaultableInteger(0);
		this.ultraTabControl1.Location = new System.Drawing.Point(13, 37);
		this.ultraTabControl1.MultiRowSelectionStyle = Infragistics.Win.UltraWinTabs.MultiRowSelectionStyle.SwapRow;
		this.ultraTabControl1.Name = "ultraTabControl1";
		this.ultraTabControl1.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.ultraTabControl1.ShowButtonSeparators = true;
		this.ultraTabControl1.Size = new System.Drawing.Size(684, 202);
		this.ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
		this.ultraTabControl1.TabIndex = 10;
		this.ultraTabControl1.TabPadding = new System.Drawing.Size(1, 3);
		appearance31.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance31.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance31.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		appearance31.BorderColor3DBase = System.Drawing.Color.FromArgb(96, 145, 234);
		ultraTab1.ActiveAppearance = appearance31;
		appearance32.BorderColor = System.Drawing.Color.Transparent;
		ultraTab1.Appearance = appearance32;
		ultraTab1.FixedWidth = 120;
		ultraTab1.TabPage = this.ultraTabPageControl1;
		ultraTab1.Text = "加總項目";
		appearance33.TextVAlign = Infragistics.Win.VAlign.Top;
		ultraTab2.Appearance = appearance33;
		ultraTab2.FixedWidth = 130;
		ultraTab2.TabPage = this.ultraTabPageControl2;
		ultraTab2.Text = "分段計算公式";
		this.ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2] { ultraTab1, ultraTab2 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(680, 174);
		appearance34.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbFormula.Appearance = appearance34;
		this.lbFormula.Location = new System.Drawing.Point(285, 3);
		this.lbFormula.Name = "lbFormula";
		this.lbFormula.Size = new System.Drawing.Size(285, 28);
		this.lbFormula.TabIndex = 9;
		this.lbFormula.Text = "公式說明:[Value]為該分段計算後金額 ";
		appearance35.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance35;
		this.ultraLabel1.Location = new System.Drawing.Point(3, 3);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(276, 23);
		this.ultraLabel1.TabIndex = 8;
		this.ultraLabel1.Text = "單價=加總項目總金額x分段比率";
		appearance36.FontData.Name = "Arial";
		appearance36.FontData.SizeInPoints = 9f;
		this.ultraToolbarsManager1.Appearance = appearance36;
		appearance37.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance37.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.DockAreaAppearance = appearance37;
		this.ultraToolbarsManager1.DockWithinContainer = this;
		this.ultraToolbarsManager1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraToolbarsManager1.LockToolbars = true;
		appearance38.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance38.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance38.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.MenuSettings.HotTrackAppearance = appearance38;
		appearance39.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance39.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsManager1.MenuSettings.IconAreaAppearance = appearance39;
		appearance40.BackColor = System.Drawing.Color.White;
		appearance40.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.MenuSettings.ToolAppearance = appearance40;
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
		ultraToolbar1.Settings.AllowDockTop = Infragistics.Win.DefaultableBoolean.True;
		ultraToolbar1.Text = "Tool1";
		labelTool1.InstanceProps.IsFirstInGroup = true;
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { buttonTool1, labelTool1, comboBoxTool1, buttonTool2 });
		ultraToolbar1.Visible = false;
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[1] { ultraToolbar1 });
		appearance41.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance41.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance41;
		appearance42.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance42.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance42.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance42;
		appearance43.Image = resources.GetObject("appearance21.Image");
		buttonTool3.SharedProps.AppearancesSmall.Appearance = appearance43;
		buttonTool3.SharedProps.Caption = "刪除";
		buttonTool3.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool3.SharedProps.Shortcut = System.Windows.Forms.Shortcut.Del;
		labelTool2.SharedProps.Caption = "尋找:";
		labelTool2.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool2.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool2.SharedProps.Caption = "輸入關鍵字";
		comboBoxTool2.SharedProps.Width = 200;
		comboBoxTool2.ValueList = valueList1;
		appearance44.Image = resources.GetObject("appearance22.Image");
		buttonTool4.SharedProps.AppearancesSmall.Appearance = appearance44;
		buttonTool4.SharedProps.Caption = "Go";
		popupMenuTool1.SharedProps.Caption = "右鍵功能表";
		popupMenuTool1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[1] { buttonTool5 });
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[5] { buttonTool3, labelTool2, comboBoxTool2, buttonTool4, popupMenuTool1 });
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this._FormSys_B_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormSys_B_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormSys_B_Toolbars_Dock_Area_Top.Name = "_FormSys_B_Toolbars_Dock_Area_Top";
		this._FormSys_B_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(700, 0);
		this._FormSys_B_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 243);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Name = "_FormSys_B_Toolbars_Dock_Area_Bottom";
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(700, 0);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormSys_B_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 0);
		this._FormSys_B_Toolbars_Dock_Area_Left.Name = "_FormSys_B_Toolbars_Dock_Area_Left";
		this._FormSys_B_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 243);
		this._FormSys_B_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormSys_B_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(700, 0);
		this._FormSys_B_Toolbars_Dock_Area_Right.Name = "_FormSys_B_Toolbars_Dock_Area_Right";
		this._FormSys_B_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 243);
		this._FormSys_B_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Magenta;
		this.imageList2.Images.SetKeyName(0, "");
		this.btnCheckFormula.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		appearance45.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance45.BackColor2 = System.Drawing.Color.White;
		appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.btnCheckFormula.HotTrackAppearance = appearance45;
		this.btnCheckFormula.Location = new System.Drawing.Point(589, 3);
		this.btnCheckFormula.Name = "btnCheckFormula";
		this.btnCheckFormula.ShowFocusRect = false;
		this.btnCheckFormula.ShowOutline = false;
		this.btnCheckFormula.Size = new System.Drawing.Size(108, 28);
		this.btnCheckFormula.SupportThemes = false;
		this.btnCheckFormula.TabIndex = 15;
		this.btnCheckFormula.Text = "公式檢查";
		this.btnCheckFormula.Click += new System.EventHandler(btnCheckFormula_Click);
		this.textBox1.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.textBox1.Location = new System.Drawing.Point(285, 6);
		this.textBox1.Multiline = true;
		this.textBox1.Name = "textBox1";
		this.textBox1.ReadOnly = true;
		this.textBox1.Size = new System.Drawing.Size(298, 49);
		this.textBox1.TabIndex = 20;
		this.textBox1.Text = "公式說明:[Value]為該分段計算後金額 \r\n公式設定範例： [Value] * 0.89";
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.Controls.Add(this.textBox1);
		base.Controls.Add(this.btnCheckFormula);
		base.Controls.Add(this.ultraTabControl1);
		base.Controls.Add(this.lbFormula);
		base.Controls.Add(this.ultraLabel1);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Bottom);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Name = "S_Form";
		base.Size = new System.Drawing.Size(700, 243);
		base.Load += new System.EventHandler(S_Form_Load);
		this.ultraTabPageControl1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridItemB).EndInit();
		this.ultraTabPageControl2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridItemC).EndInit();
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txt_Rate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUpper).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLower).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraTabControl1).EndInit();
		this.ultraTabControl1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
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
