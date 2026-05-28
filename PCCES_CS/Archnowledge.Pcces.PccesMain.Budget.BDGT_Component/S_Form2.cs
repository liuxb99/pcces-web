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

namespace Archnowledge.Pcces.PccesMain.Budget.BDGT_Component;

public class S_Form2 : Form
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

	private string F_ProjectCode = "";

	private string F_printNoVarname = "";

	private Archnowledge.Pcces.DomainModule.General.PubProject thePubProject = new Archnowledge.Pcces.DomainModule.General.PubProject();

	private IContainer components = null;

	private UltraButton btnCheckFormula;

	private UltraLabel lbFormula;

	private UltraLabel ultraLabel1;

	private UltraTabControl ultraTabControl1;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage2;

	private UltraTabPageControl ultraTabPageControl1;

	private UltraButton ultraButton2;

	private C1FlexGrid gridItemB;

	private Panel panel1;

	private UltraButton BtnPick;

	private UltraTabPageControl ultraTabPageControl2;

	private C1FlexGrid gridItemC;

	private Panel panel3;

	private UltraButton ultraButton1;

	private UltraLabel ultraLabel5;

	private UltraTextEditor txt_Rate;

	private UltraLabel ultraLabel4;

	private UltraTextEditor txtUpper;

	private UltraLabel ultraLabel3;

	private UltraTextEditor txtLower;

	private Panel panel2;

	private UltraButton ultraButton3;

	private TextBox txtFormula;

	public string _ProjectCode
	{
		get
		{
			return F_ProjectCode;
		}
		set
		{
			F_ProjectCode = value;
		}
	}

	public string _printNoVarname
	{
		get
		{
			return F_printNoVarname;
		}
		set
		{
			F_printNoVarname = value;
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

	public S_Form2()
	{
		InitializeComponent();
		CellStyle cs1 = gridItemC.Styles.Add("EditMode");
		cs1.DataType = typeof(Image);
		cs1.ImageAlign = ImageAlignEnum.RightCenter;
	}

	private void S_Form2_Load(object sender, EventArgs e)
	{
		txtLower.Text = "";
		txtUpper.Text = "";
		txt_Rate.Text = "";
		projectCode = F_ProjectCode;
		F_ParentPrintNo = F_printNoVarname;
		F_ParentsNo = "0";
		if (F_ActionName == PccesFormAction.BUD)
		{
			gridItemB.Cols["VarSign"].Visible = true;
		}
		LoadingData();
		BindDataToGrid();
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
		dtItemC = dbItemC.ListItem("", projectCode, "", F_printNoVarname);
		if (dtItemC.Columns.IndexOf("formula") >= 0)
		{
			EnableFormula = true;
		}
		else
		{
			EnableFormula = false;
		}
		txtFormula.Visible = EnableFormula;
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
		if (gridItemC.Rows.Count == 1)
		{
			MessageBox.Show(this, "完全沒有分段計價條件便會關閉此功能", CommonMethods.GetFormTypeTitle(FormType.Budget), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
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
			CommonMethods.LogFile("Pcces46", "M", "BDGT_Component.S_Form2.cs" + ex.Message);
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
			CommonMethods.LogFile("Pcces46", "M", "BDGT_Component.S_Form2.cs" + ex.Message);
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
			CommonMethods.LogFile("Pcces46", "M", "BDGT_Component.S_Form2.cs" + ex.Message);
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
				CommonMethods.LogFile("Pcces46", "M", "BDGT_Component.S_Form2.cs" + ex.Message);
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
				CommonMethods.LogFile("Pcces46", "M", "BDGT_Component.S_Form2.cs" + ex.Message);
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
				CommonMethods.LogFile("Pcces46", "M", "BDGT_Component.S_Form2.cs" + ex.Message);
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
			CommonMethods.LogFile("Pcces46", "M", "BDGT_Component.S_Form2.cs" + ex.Message);
		}
	}

	private void gridItemC_MouseDown(object sender, MouseEventArgs e)
	{
		int rowIndex = gridItemC.MouseRow;
		if (gridItemC.MouseRow > 0 && gridItemC.MouseCol > 0)
		{
			gridItemC.Row = rowIndex;
		}
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

	private void txtLower_ValueChanged(object sender, EventArgs e)
	{
	}

	private void ultraButton3_Click(object sender, EventArgs e)
	{
		DeleteSection();
	}

	private void ultraButton2_Click_1(object sender, EventArgs e)
	{
	}

	private void gridItemC_AfterEdit_1(object sender, RowColEventArgs e)
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
		string TableName = "BudItemC";
		if (F_ActionName == PccesFormAction.BID)
		{
			TableName = "BidItemC";
		}
		else if (F_ActionName == PccesFormAction.SplitContract)
		{
			TableName = "subItemC";
		}
		else if (F_ActionName == PccesFormAction.SubChange)
		{
			TableName = "subChgItemC";
		}
		else if (F_ActionName == PccesFormAction.BUDEXE)
		{
			TableName = "budExeItemC";
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
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.BDGT_Component.S_Form2));
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
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.gridItemB = new C1.Win.C1FlexGrid.C1FlexGrid();
		this.panel1 = new System.Windows.Forms.Panel();
		this.BtnPick = new Infragistics.Win.Misc.UltraButton();
		this.ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.gridItemC = new C1.Win.C1FlexGrid.C1FlexGrid();
		this.panel3 = new System.Windows.Forms.Panel();
		this.ultraButton3 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.txt_Rate = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.txtUpper = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.txtLower = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.panel2 = new System.Windows.Forms.Panel();
		this.btnCheckFormula = new Infragistics.Win.Misc.UltraButton();
		this.lbFormula = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.ultraTabSharedControlsPage2 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.txtFormula = new System.Windows.Forms.TextBox();
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
		base.SuspendLayout();
		this.ultraTabPageControl1.Controls.Add(this.ultraButton2);
		this.ultraTabPageControl1.Controls.Add(this.gridItemB);
		this.ultraTabPageControl1.Controls.Add(this.panel1);
		this.ultraTabPageControl1.Controls.Add(this.BtnPick);
		this.ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
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
		this.ultraButton2.Click += new System.EventHandler(ultraButton2_Click_1);
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
		this.ultraTabPageControl2.Controls.Add(this.gridItemC);
		this.ultraTabPageControl2.Controls.Add(this.panel3);
		this.ultraTabPageControl2.Controls.Add(this.panel2);
		this.ultraTabPageControl2.Location = new System.Drawing.Point(2, 26);
		this.ultraTabPageControl2.Name = "ultraTabPageControl2";
		this.ultraTabPageControl2.Size = new System.Drawing.Size(680, 174);
		this.gridItemC.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.gridItemC.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
		this.gridItemC.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridItemC.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.gridItemC.ColumnInfo = resources.GetString("gridItemC.ColumnInfo");
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
		this.gridItemC.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridItemC_AfterEdit_1);
		this.panel3.Controls.Add(this.ultraButton3);
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
		appearance5.TextVAlign = Infragistics.Win.VAlign.Top;
		this.ultraButton3.Appearance = appearance5;
		this.ultraButton3.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		appearance6.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance6.BackColor2 = System.Drawing.Color.White;
		appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.ultraButton3.HotTrackAppearance = appearance6;
		this.ultraButton3.HotTracking = true;
		this.ultraButton3.Location = new System.Drawing.Point(605, 4);
		this.ultraButton3.Name = "ultraButton3";
		this.ultraButton3.ShowFocusRect = false;
		this.ultraButton3.ShowOutline = false;
		this.ultraButton3.Size = new System.Drawing.Size(72, 25);
		this.ultraButton3.SupportThemes = false;
		this.ultraButton3.TabIndex = 7;
		this.ultraButton3.Text = "刪除";
		this.ultraButton3.Click += new System.EventHandler(ultraButton3_Click);
		appearance7.TextVAlign = Infragistics.Win.VAlign.Top;
		this.ultraButton1.Appearance = appearance7;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		appearance8.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance8.BackColor2 = System.Drawing.Color.White;
		appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.ultraButton1.HotTrackAppearance = appearance8;
		this.ultraButton1.HotTracking = true;
		this.ultraButton1.Location = new System.Drawing.Point(527, 4);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(72, 25);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 6;
		this.ultraButton1.Text = "新增";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		appearance9.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance9;
		this.ultraLabel5.Location = new System.Drawing.Point(488, 4);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(20, 23);
		this.ultraLabel5.TabIndex = 5;
		this.ultraLabel5.Text = "%";
		this.txt_Rate.AutoSize = true;
		this.txt_Rate.Location = new System.Drawing.Point(402, 4);
		this.txt_Rate.Name = "txt_Rate";
		this.txt_Rate.Size = new System.Drawing.Size(80, 24);
		this.txt_Rate.TabIndex = 4;
		this.txt_Rate.Text = "[txt_Rate]";
		appearance10.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance10.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance10;
		this.ultraLabel4.Location = new System.Drawing.Point(334, 5);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(72, 23);
		this.ultraLabel4.TabIndex = 3;
		this.ultraLabel4.Text = "變動費率";
		this.txtUpper.AutoSize = true;
		this.txtUpper.Location = new System.Drawing.Point(202, 4);
		this.txtUpper.Name = "txtUpper";
		this.txtUpper.Size = new System.Drawing.Size(103, 24);
		this.txtUpper.TabIndex = 2;
		this.txtUpper.Text = "[txtUpper]";
		appearance11.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance11.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance11;
		this.ultraLabel3.Location = new System.Drawing.Point(115, 5);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(92, 23);
		this.ultraLabel3.TabIndex = 1;
		this.ultraLabel3.Text = "< 金額 ≦";
		this.txtLower.AutoSize = true;
		this.txtLower.Location = new System.Drawing.Point(8, 4);
		this.txtLower.Name = "txtLower";
		this.txtLower.Size = new System.Drawing.Size(101, 24);
		this.txtLower.TabIndex = 0;
		this.txtLower.Text = "[txtLower]";
		this.txtLower.ValueChanged += new System.EventHandler(txtLower_ValueChanged);
		this.panel2.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(680, 8);
		this.panel2.TabIndex = 2;
		this.btnCheckFormula.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnCheckFormula.Font = new System.Drawing.Font("細明體", 11.25f);
		appearance12.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance12.BackColor2 = System.Drawing.Color.White;
		appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.btnCheckFormula.HotTrackAppearance = appearance12;
		this.btnCheckFormula.Location = new System.Drawing.Point(590, 12);
		this.btnCheckFormula.Name = "btnCheckFormula";
		this.btnCheckFormula.ShowFocusRect = false;
		this.btnCheckFormula.ShowOutline = false;
		this.btnCheckFormula.Size = new System.Drawing.Size(108, 28);
		this.btnCheckFormula.SupportThemes = false;
		this.btnCheckFormula.TabIndex = 18;
		this.btnCheckFormula.Text = "公式檢查";
		this.btnCheckFormula.Click += new System.EventHandler(btnCheckFormula_Click);
		appearance13.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbFormula.Appearance = appearance13;
		this.lbFormula.Font = new System.Drawing.Font("細明體", 11.25f);
		this.lbFormula.Location = new System.Drawing.Point(286, 30);
		this.lbFormula.Name = "lbFormula";
		this.lbFormula.Size = new System.Drawing.Size(285, 28);
		this.lbFormula.TabIndex = 17;
		this.lbFormula.Text = "公式說明:[Value]為該分段計算後金額 ";
		this.lbFormula.Visible = false;
		appearance14.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance14;
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 11.25f);
		this.ultraLabel1.Location = new System.Drawing.Point(4, 12);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(276, 23);
		this.ultraLabel1.TabIndex = 16;
		this.ultraLabel1.Text = "單價=加總項目總金額x分段比率";
		appearance15.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance15.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance15.ForeColor = System.Drawing.Color.Black;
		appearance15.TextVAlign = Infragistics.Win.VAlign.Top;
		this.ultraTabControl1.ActiveTabAppearance = appearance15;
		appearance16.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance16.BackColor2 = System.Drawing.Color.FromArgb(102, 153, 255);
		appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance16.TextVAlign = Infragistics.Win.VAlign.Top;
		this.ultraTabControl1.Appearance = appearance16;
		appearance17.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance17.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraTabControl1.ClientAreaAppearance = appearance17;
		this.ultraTabControl1.Controls.Add(this.ultraTabSharedControlsPage1);
		this.ultraTabControl1.Controls.Add(this.ultraTabSharedControlsPage2);
		this.ultraTabControl1.Controls.Add(this.ultraTabPageControl1);
		this.ultraTabControl1.Controls.Add(this.ultraTabPageControl2);
		this.ultraTabControl1.FlatMode = true;
		this.ultraTabControl1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraTabControl1.HotTrack = true;
		this.ultraTabControl1.InterTabSpacing = new Infragistics.Win.DefaultableInteger(0);
		this.ultraTabControl1.Location = new System.Drawing.Point(9, 46);
		this.ultraTabControl1.MultiRowSelectionStyle = Infragistics.Win.UltraWinTabs.MultiRowSelectionStyle.SwapRow;
		this.ultraTabControl1.Name = "ultraTabControl1";
		this.ultraTabControl1.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.ultraTabControl1.ShowButtonSeparators = true;
		this.ultraTabControl1.Size = new System.Drawing.Size(684, 202);
		this.ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
		this.ultraTabControl1.TabIndex = 19;
		this.ultraTabControl1.TabPadding = new System.Drawing.Size(1, 3);
		appearance18.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance18.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance18.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		appearance18.BorderColor3DBase = System.Drawing.Color.FromArgb(96, 145, 234);
		ultraTab1.ActiveAppearance = appearance18;
		appearance19.BorderColor = System.Drawing.Color.Transparent;
		ultraTab1.Appearance = appearance19;
		ultraTab1.FixedWidth = 120;
		ultraTab1.TabPage = this.ultraTabPageControl1;
		ultraTab1.Text = "加總項目";
		ultraTab1.Visible = false;
		appearance20.TextVAlign = Infragistics.Win.VAlign.Top;
		ultraTab2.Appearance = appearance20;
		ultraTab2.FixedWidth = 130;
		ultraTab2.TabPage = this.ultraTabPageControl2;
		ultraTab2.Text = "分段計算公式";
		this.ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2] { ultraTab1, ultraTab2 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(680, 174);
		this.ultraTabSharedControlsPage2.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage2.Name = "ultraTabSharedControlsPage2";
		this.ultraTabSharedControlsPage2.Size = new System.Drawing.Size(680, 174);
		this.txtFormula.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.txtFormula.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.txtFormula.Font = new System.Drawing.Font("細明體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.txtFormula.Location = new System.Drawing.Point(286, 16);
		this.txtFormula.Name = "txtFormula";
		this.txtFormula.ReadOnly = true;
		this.txtFormula.Size = new System.Drawing.Size(285, 20);
		this.txtFormula.TabIndex = 20;
		this.txtFormula.Text = "公式說明:[Value]為該分段計算後金額 ";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.ClientSize = new System.Drawing.Size(705, 255);
		base.Controls.Add(this.txtFormula);
		base.Controls.Add(this.ultraTabControl1);
		base.Controls.Add(this.btnCheckFormula);
		base.Controls.Add(this.lbFormula);
		base.Controls.Add(this.ultraLabel1);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "S_Form2";
		base.Load += new System.EventHandler(S_Form2_Load);
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
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
