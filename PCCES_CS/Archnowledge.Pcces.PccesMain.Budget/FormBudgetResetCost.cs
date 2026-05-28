using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.Bid;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.DomainModule.BusinessLogical;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.DomainModule.LogicalBase;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetResetCost : Form
{
	private FormStatus FORM_STATUS = FormStatus.Iinitial;

	private string F_UserID;

	private PccesFormAction F_ActionName;

	private double F_TotalAmount;

	private double F_OldTotalAmount;

	private string F_ProjectCode;

	private Archnowledge.Pcces.DomainModule.LogicalBase.Project theProject;

	private IContainer components;

	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_A;

	private UltraTabPageControl Tab_B;

	private UltraButton ultraButton4;

	private UltraButton BtnPick;

	private UltraTabPageControl Tab_C;

	private Panel panel1;

	private UltraButton ultraButton2;

	private UltraLabel ultraLabel4;

	private UltraLabel lbMessage;

	private UltraLabel ultraLabel16;

	private GroupBox groupBox1;

	private UltraTextEditor txtRatio;

	private UltraTextEditor txtAmount;

	private UltraLabel ultraLabel13;

	private UltraTextEditor txtM;

	private UltraLabel ultraLabel14;

	private UltraLabel ultraLabel11;

	private UltraTextEditor txtE;

	private UltraLabel ultraLabel12;

	private UltraLabel ultraLabel10;

	private UltraTextEditor txtL;

	private UltraLabel ultraLabel9;

	private UltraLabel ultraLabel8;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel5;

	private UltraLabel lblTotal;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel1;

	private RadioButton RB3;

	private RadioButton RB2;

	private RadioButton RB1;

	private GroupBox groupBox2;

	private UltraOptionSet OptionSet1;

	private UltraCheckEditor CB_UnRestoreCost;

	private System.Windows.Forms.ToolTip toolTip1;

	private UltraLabel ultraLabel17;

	private UltraLabel ultraLabel18;

	private UltraLabel ultraLabel19;

	private GroupBox groupBox3;

	private UltraOptionSet OptionSet2;

	private UltraLabel lblOldTotal;

	private UltraLabel ultraLabel21;

	public Panel panel5;

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

	public PccesFormAction _ActionName
	{
		get
		{
			return F_ActionName;
		}
		set
		{
			F_ActionName = value;
			switch (F_ActionName)
			{
			case PccesFormAction.BUD:
				theProject = new BudProject();
				break;
			case PccesFormAction.BID:
				theProject = new BidProject();
				break;
			case PccesFormAction.BUDEXE:
				break;
			}
		}
	}

	public double _TotalAmount
	{
		get
		{
			return F_TotalAmount;
		}
		set
		{
			F_TotalAmount = value;
		}
	}

	public double _OldTotalAmount
	{
		get
		{
			return F_OldTotalAmount;
		}
		set
		{
			F_OldTotalAmount = value;
		}
	}

	public FormBudgetResetCost()
	{
		InitializeComponent();
	}

	private void ProgressEventHandler(string Message)
	{
		Application.DoEvents();
		lbMessage.Text = Message;
	}

	private void FormBudgetResetCost_Load(object sender, EventArgs e)
	{
		string sRestoreCostFirst = CommonMethods.GetIniValue("FormBudget", "RestoreCostFirst");
		if (sRestoreCostFirst.ToUpper() == "TRUE")
		{
			CB_UnRestoreCost.Checked = true;
		}
		else
		{
			CB_UnRestoreCost.Checked = false;
		}
		if (F_OldTotalAmount != 0.0)
		{
			lblOldTotal.Text = $"{F_OldTotalAmount:N0}";
		}
		else
		{
			lblOldTotal.Text = $"{F_TotalAmount:N0}";
		}
		lblTotal.Text = $"{F_TotalAmount:N0}";
		txtAmount.Text = $"{F_TotalAmount:N0}";
		txtRatio.Text = $"{100:N6}";
		lblTotal.Focus();
		if (Convert.ToDouble(lblTotal.Text) == 0.0)
		{
			txtRatio.Enabled = false;
			txtAmount.Enabled = false;
			MessageBox.Show(this, "總價為 0 ，無法進行總價調整。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			Close();
		}
		txtRatio.Appearance.BackColor = Color.White;
		txtAmount.Appearance.BackColor = Color.White;
		txtL.Appearance.BackColor = Color.White;
		txtE.Appearance.BackColor = Color.White;
		txtM.Appearance.BackColor = Color.White;
	}

	private void txtRatio_Leave(object sender, EventArgs e)
	{
		RB1.Checked = true;
		double theRation = 0.0;
		try
		{
			theRation = Convert.ToDouble(txtRatio.Text);
			txtAmount.Text = $"{F_TotalAmount * theRation / 100.0:N0}";
		}
		catch (Exception ex)
		{
			MessageBox.Show(this, "輸入比例有誤，請重新輸入! " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtRatio.Focus();
		}
	}

	private void txtAmount_Leave(object sender, EventArgs e)
	{
		RB1.Checked = true;
		double theAmount = 0.0;
		try
		{
			theAmount = Convert.ToDouble(txtAmount.Text);
			txtRatio.Text = $"{theAmount / F_TotalAmount * 100.0:N6}";
		}
		catch (Exception ex)
		{
			MessageBox.Show(this, "輸入比例有誤，請重新輸入! " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtAmount.Focus();
		}
	}

	private void txtL_Leave(object sender, EventArgs e)
	{
		RB2.Checked = true;
		double theRate = 0.0;
		try
		{
			theRate = Convert.ToDouble((sender as Control).Text);
		}
		catch (Exception ex)
		{
			MessageBox.Show(this, "輸入比例有誤，請重新輸入! " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			(sender as Control).Focus();
		}
	}

	private void BtnPick_Click(object sender, EventArgs e)
	{
		bool EnableNewCalculateCost = false;
		if (theProject != null)
		{
			Archnowledge.Pcces.DomainModule.General.PubProject thePubProject = new Archnowledge.Pcces.DomainModule.General.PubProject();
			EnableNewCalculateCost = thePubProject.GetPubProjectEnableNewCalculateCost(F_ProjectCode);
		}
		if (EnableNewCalculateCost)
		{
			DoNewCalculate();
		}
		else
		{
			DoOldCalculate();
		}
	}

	private void DoNewCalculate()
	{
		CommonMethods.WriteIniValue("FormBudget", "RestoreCostFirst", CB_UnRestoreCost.Checked ? "True" : "False");
		FORM_STATUS = FormStatus.Iinitial;
		Cursor = Cursors.WaitCursor;
		Tab_B.Tab.Selected = true;
		ExecResult ER = new ExecResult();
		Application.DoEvents();
		DiscountCalculate theDiscountCalculate = new DiscountCalculate(F_ActionName, F_ProjectCode, 0);
		theDiscountCalculate.ps_IsRestoreCostFirst = (CB_UnRestoreCost.Checked ? "Y" : "N");
		bool IsCalculateOnce = true;
		try
		{
			if (Convert.ToInt16(OptionSet1.Value) != 1)
			{
				IsCalculateOnce = false;
			}
			if (RB1.Checked)
			{
				BudProject theProject = null;
				theProject = new BudProject("Pcces");
				decimal shareVDF1 = 0m;
				int shareVDF1sNo = 0;
				theProject.GetShareVDF1(F_ProjectCode, out shareVDF1, out shareVDF1sNo);
				theProject.UpdateShareVDF1(F_ProjectCode, 0m, shareVDF1sNo);
				ER = theDiscountCalculate.SetAmountFaster(Convert.ToDouble(txtAmount.Text), IsCalculateOnce, ProgressEventHandler, F_ActionName);
			}
			else if (RB2.Checked)
			{
				BudProject theProject = null;
				theProject = new BudProject("Pcces");
				decimal shareVDF1 = 0m;
				int shareVDF1sNo = 0;
				theProject.GetShareVDF1(F_ProjectCode, out shareVDF1, out shareVDF1sNo);
				theProject.UpdateShareVDF1(F_ProjectCode, 0m, shareVDF1sNo);
				if (OptionSet2.CheckedIndex == 0)
				{
					theDiscountCalculate.SetLemCost(Convert.ToDecimal(txtL.Text), Convert.ToDecimal(txtE.Text), Convert.ToDecimal(txtM.Text), ProgressEventHandler);
				}
				else
				{
					theDiscountCalculate.SetLemCostByRate(Convert.ToDecimal(txtL.Text), Convert.ToDecimal(txtE.Text), Convert.ToDecimal(txtM.Text), ProgressEventHandler);
				}
			}
			else if (RB3.Checked)
			{
				BudProject theProject = null;
				theProject = new BudProject("Pcces");
				decimal shareVDF1 = 0m;
				int shareVDF1sNo = 0;
				theProject.GetShareVDF1(F_ProjectCode, out shareVDF1, out shareVDF1sNo);
				theProject.UpdateShareVDF1(F_ProjectCode, 0m, shareVDF1sNo);
				theDiscountCalculate.RestoreCost(ProgressEventHandler);
			}
			if (ER.ReturnCode == 0)
			{
				ItemCalculate theItemCalculate = new ItemCalculate(F_ActionName, F_ProjectCode, 0);
				ER = theItemCalculate.CalculateAll(IncludeResource: true, IncludeMrs: true, ProgressEventHandler, null);
			}
		}
		catch (Exception ex)
		{
			ER.ReturnCode = 1;
			ER.Message = "總價調整失敗 : " + ex.Message;
		}
		if (ER.ReturnCode != 0)
		{
			try
			{
				theDiscountCalculate.RestoreCost(ProgressEventHandler);
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetResetCost.cs" + ex.Message);
			}
			ultraLabel16.Text = "總價調整失敗";
			ultraLabel19.Text = "請檢查目前專案的相關設定是否正確 : " + ER.Message;
			ultraLabel16.Appearance.ForeColor = Color.Red;
			ultraLabel19.Appearance.ForeColor = Color.Red;
			ultraLabel19.Visible = true;
			Tab_C.Tab.Selected = true;
			Cursor = Cursors.Default;
			FORM_STATUS = FormStatus.Active;
		}
		Tab_C.Tab.Selected = true;
		Cursor = Cursors.Default;
		FORM_STATUS = FormStatus.Active;
	}

	private void DoOldCalculate()
	{
		CommonMethods.WriteIniValue("FormBudget", "RestoreCostFirst", CB_UnRestoreCost.Checked ? "True" : "False");
		FORM_STATUS = FormStatus.Iinitial;
		Cursor = Cursors.WaitCursor;
		Tab_B.Tab.Selected = true;
		bool bExecResult = true;
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("WinForm 總價調整");
		Application.DoEvents();
		string AppLocation = AppDomain.CurrentDomain.BaseDirectory;
		string IsOldReCal = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BDGT", "IsOldReCal");
		string srckind = CommonMethods.GetActionNameString(F_ActionName);
		if (srckind == "BID")
		{
			Archnowledge.Pcces.BUDClass.Project projcom = new Archnowledge.Pcces.BUDClass.Project(aArr);
			projcom.ps_srckind = srckind;
			DataTable dt = projcom.ListItem_eight("", F_ProjectCode);
			if (dt.Rows.Count > 0 && dt.Rows[0]["ReCalType"].ToString().Trim() == "" && dt.Rows[0]["printMode"].ToString() != "")
			{
				string readPrintMode = dt.Rows[0]["printMode"].ToString().Trim();
				string tmpPrintMode = readPrintMode.Substring(37, 1);
				IsOldReCal = ((tmpPrintMode == "0") ? "FALSE" : ((!(tmpPrintMode == "1")) ? "THIRD" : "TRUE"));
			}
			projcom = null;
			dt = null;
		}
		string sType = GetReCalType();
		if (sType != "")
		{
			IsOldReCal = sType;
		}
		ReSetCost RST_CST = new ReSetCost(aArr);
		RST_CST.ps_IsRestoreCostFirst = (CB_UnRestoreCost.Checked ? "Y" : "N");
		RST_CST.ps_IsOldReCalc = IsOldReCal.ToUpper();
		if (RB1.Checked)
		{
			bExecResult = RST_CST.SetAmount(F_ProjectCode, CommonMethods.GetActionNameString(F_ActionName), Convert.ToDouble(txtAmount.Text), Convert.ToInt16(OptionSet1.Value));
		}
		else if (RB2.Checked)
		{
			bExecResult = ((OptionSet2.CheckedIndex != 0) ? RST_CST.SetLemCostByRate(F_ProjectCode, CommonMethods.GetActionNameString(F_ActionName), Convert.ToDouble(txtL.Text), Convert.ToDouble(txtE.Text), Convert.ToDouble(txtM.Text)) : RST_CST.SetLemCost(F_ProjectCode, CommonMethods.GetActionNameString(F_ActionName), Convert.ToDouble(txtL.Text), Convert.ToDouble(txtE.Text), Convert.ToDouble(txtM.Text)));
		}
		else if (RB3.Checked)
		{
			try
			{
				RST_CST.RestoreCost(F_ProjectCode, CommonMethods.GetActionNameString(F_ActionName));
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetResetCost.cs" + ex.Message);
			}
		}
		if (!bExecResult)
		{
			try
			{
				RST_CST.RestoreCost(F_ProjectCode, CommonMethods.GetActionNameString(F_ActionName));
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetResetCost.cs" + ex.Message);
			}
			ultraLabel16.Text = "總價調整失敗";
			ultraLabel19.Text = "請檢查目前專案的相關設定是否正確，或請確定可以完成重新總計";
			ultraLabel16.Appearance.ForeColor = Color.Red;
			ultraLabel19.Appearance.ForeColor = Color.Red;
			ultraLabel19.Visible = true;
			Tab_C.Tab.Selected = true;
			Cursor = Cursors.Default;
			FORM_STATUS = FormStatus.Active;
			return;
		}
		try
		{
			Archnowledge.Pcces.BUDClass.ItemA dbItemA = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
			dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			if (IsOldReCal.ToUpper() == "TRUE")
			{
				dbItemA.ReCalcCost2(F_ProjectCode, mode: true, noShare: true);
			}
			else if (IsOldReCal.ToUpper() == "FALSE")
			{
				dbItemA.ReCalcCost2(F_ProjectCode);
			}
			else
			{
				dbItemA.ps_SmallCalcuMode = "THIRD";
				dbItemA.ReCalcCost2(F_ProjectCode, mode: true, noShare: true);
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetResetCost.cs" + ex.Message);
		}
		Tab_C.Tab.Selected = true;
		Cursor = Cursors.Default;
		FORM_STATUS = FormStatus.Active;
	}

	private void ultraButton4_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
		FORM_STATUS = FormStatus.Edit;
	}

	private void txtAmount_ValueChanged(object sender, EventArgs e)
	{
		if (FORM_STATUS == FormStatus.Edit || !txtAmount.IsInEditMode)
		{
			return;
		}
		double theAmount = 0.0;
		try
		{
			theAmount = Math.Abs(Convert.ToDouble(txtAmount.Text));
			txtRatio.Text = $"{theAmount / F_TotalAmount * 100.0:N6}";
		}
		catch (Exception ex)
		{
			MessageBox.Show(this, "輸入比例有誤，請重新輸入! " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtAmount.Focus();
		}
	}

	private void txtRatio_ValueChanged(object sender, EventArgs e)
	{
		if (FORM_STATUS == FormStatus.Iinitial || FORM_STATUS == FormStatus.Edit || !txtRatio.IsInEditMode)
		{
			return;
		}
		RB1.Checked = true;
		double theRation = 0.0;
		try
		{
			theRation = Convert.ToDouble(txtRatio.Text);
			txtAmount.Text = $"{F_TotalAmount * theRation / 100.0:N0}";
		}
		catch (Exception ex)
		{
			MessageBox.Show(this, "輸入比例有誤，請重新輸入! " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtRatio.Focus();
		}
	}

	private void FormBudgetResetCost_Activated(object sender, EventArgs e)
	{
		if (FORM_STATUS == FormStatus.Iinitial)
		{
			FORM_STATUS = FormStatus.Active;
		}
	}

	private void txtAmount_Validating(object sender, CancelEventArgs e)
	{
		try
		{
			Convert.ToDecimal(txtAmount.Text.Trim());
		}
		catch (Exception)
		{
			MessageBox.Show(this, "輸入的數值有問題，請檢查", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			e.Cancel = true;
		}
	}

	private void ultraLabel18_Click(object sender, EventArgs e)
	{
		CB_UnRestoreCost.Checked = !CB_UnRestoreCost.Checked;
	}

	private string GetReCalType()
	{
		string AppLocation = AppDomain.CurrentDomain.BaseDirectory;
		string iNum = "1";
		string rtnStr = "";
		string sSQL = "Select ReCalType from " + CommonMethods.GetActionNameString(F_ActionName) + "Project where projectCode = '" + F_ProjectCode + "'";
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("取pccescode的值");
		ModifyDB ModDB = new ModifyDB(F_ProjectCode, aArr);
		DataTable DT = new DataTable();
		DT = ModDB.DBList(sSQL);
		if (DT.Rows.Count > 0)
		{
			iNum = DT.Rows[0]["ReCalType"].ToString().Trim();
		}
		switch (iNum)
		{
		case "1":
			rtnStr = "FALSE";
			break;
		case "2":
			rtnStr = "TRUE";
			break;
		case "3":
			rtnStr = "THIRD";
			break;
		}
		ModDB = null;
		aArr = null;
		if (iNum == "")
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "BDGT", "IsOldReCal", "FALSE");
		}
		return rtnStr;
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetResetCost));
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
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
		Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.OptionSet1 = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraButton4 = new Infragistics.Win.Misc.UltraButton();
		this.BtnPick = new Infragistics.Win.Misc.UltraButton();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.lblOldTotal = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel21 = new Infragistics.Win.Misc.UltraLabel();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.OptionSet2 = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.ultraLabel18 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel17 = new Infragistics.Win.Misc.UltraLabel();
		this.CB_UnRestoreCost = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.txtM = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtE = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtRatio = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtL = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtAmount = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.RB2 = new System.Windows.Forms.RadioButton();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
		this.RB3 = new System.Windows.Forms.RadioButton();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.lblTotal = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.RB1 = new System.Windows.Forms.RadioButton();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.lbMessage = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_C = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraLabel19 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel16 = new Infragistics.Win.Misc.UltraLabel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.Tab_A.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.OptionSet1).BeginInit();
		this.panel5.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.OptionSet2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtM).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtE).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtL).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAmount).BeginInit();
		this.Tab_B.SuspendLayout();
		this.Tab_C.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.groupBox2);
		this.Tab_A.Controls.Add(this.panel5);
		this.Tab_A.Controls.Add(this.groupBox1);
		this.Tab_A.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Tab_A.Location = new System.Drawing.Point(0, 0);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(518, 512);
		this.groupBox2.Controls.Add(this.OptionSet1);
		this.groupBox2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.groupBox2.Location = new System.Drawing.Point(8, 400);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(500, 72);
		this.groupBox2.TabIndex = 28;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "總價比例調整選項";
		appearance1.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.OptionSet1.Appearance = appearance1;
		this.OptionSet1.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.OptionSet1.CheckedIndex = 0;
		this.OptionSet1.ItemAppearance = appearance2;
		this.OptionSet1.ItemOrigin = new System.Drawing.Point(15, 0);
		valueListItem1.DataValue = "1";
		valueListItem1.DisplayText = "只計算一次(較快)";
		valueListItem2.DataValue = "9999";
		valueListItem2.DisplayText = "使用逼近法計算(比較精準，較慢)";
		this.OptionSet1.Items.Add(valueListItem1);
		this.OptionSet1.Items.Add(valueListItem2);
		this.OptionSet1.ItemSpacingHorizontal = 20;
		this.OptionSet1.Location = new System.Drawing.Point(16, 32);
		this.OptionSet1.Name = "OptionSet1";
		this.OptionSet1.Size = new System.Drawing.Size(468, 24);
		this.OptionSet1.TabIndex = 0;
		this.OptionSet1.Text = "只計算一次(較快)";
		this.panel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panel5.Controls.Add(this.ultraButton4);
		this.panel5.Controls.Add(this.BtnPick);
		this.panel5.Location = new System.Drawing.Point(0, 478);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(518, 38);
		this.panel5.TabIndex = 4;
		this.ultraButton4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton4.Appearance = appearance3;
		this.ultraButton4.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton4.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		appearance4.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance4.BackColor2 = System.Drawing.Color.White;
		appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.ultraButton4.HotTrackAppearance = appearance4;
		this.ultraButton4.HotTracking = true;
		this.ultraButton4.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton4.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton4.Location = new System.Drawing.Point(434, 3);
		this.ultraButton4.Name = "ultraButton4";
		this.ultraButton4.ShowFocusRect = false;
		this.ultraButton4.ShowOutline = false;
		this.ultraButton4.Size = new System.Drawing.Size(80, 28);
		this.ultraButton4.SupportThemes = false;
		this.ultraButton4.TabIndex = 10;
		this.ultraButton4.Text = "取消";
		this.ultraButton4.Click += new System.EventHandler(ultraButton4_Click);
		this.BtnPick.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance5.Image = resources.GetObject("appearance5.Image");
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnPick.Appearance = appearance5;
		this.BtnPick.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		appearance6.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance6.BackColor2 = System.Drawing.Color.White;
		appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.BtnPick.HotTrackAppearance = appearance6;
		this.BtnPick.HotTracking = true;
		this.BtnPick.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnPick.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnPick.Location = new System.Drawing.Point(352, 3);
		this.BtnPick.Name = "BtnPick";
		this.BtnPick.ShowFocusRect = false;
		this.BtnPick.ShowOutline = false;
		this.BtnPick.Size = new System.Drawing.Size(80, 28);
		this.BtnPick.SupportThemes = false;
		this.BtnPick.TabIndex = 9;
		this.BtnPick.Text = "確定";
		this.BtnPick.Click += new System.EventHandler(BtnPick_Click);
		this.groupBox1.BackColor = System.Drawing.Color.Transparent;
		this.groupBox1.Controls.Add(this.lblOldTotal);
		this.groupBox1.Controls.Add(this.ultraLabel21);
		this.groupBox1.Controls.Add(this.groupBox3);
		this.groupBox1.Controls.Add(this.ultraLabel18);
		this.groupBox1.Controls.Add(this.ultraLabel17);
		this.groupBox1.Controls.Add(this.CB_UnRestoreCost);
		this.groupBox1.Controls.Add(this.txtM);
		this.groupBox1.Controls.Add(this.txtE);
		this.groupBox1.Controls.Add(this.txtRatio);
		this.groupBox1.Controls.Add(this.txtL);
		this.groupBox1.Controls.Add(this.txtAmount);
		this.groupBox1.Controls.Add(this.ultraLabel7);
		this.groupBox1.Controls.Add(this.ultraLabel5);
		this.groupBox1.Controls.Add(this.ultraLabel1);
		this.groupBox1.Controls.Add(this.RB2);
		this.groupBox1.Controls.Add(this.ultraLabel12);
		this.groupBox1.Controls.Add(this.ultraLabel2);
		this.groupBox1.Controls.Add(this.ultraLabel11);
		this.groupBox1.Controls.Add(this.RB3);
		this.groupBox1.Controls.Add(this.ultraLabel8);
		this.groupBox1.Controls.Add(this.lblTotal);
		this.groupBox1.Controls.Add(this.ultraLabel14);
		this.groupBox1.Controls.Add(this.ultraLabel9);
		this.groupBox1.Controls.Add(this.ultraLabel13);
		this.groupBox1.Controls.Add(this.RB1);
		this.groupBox1.Controls.Add(this.ultraLabel3);
		this.groupBox1.Controls.Add(this.ultraLabel6);
		this.groupBox1.Controls.Add(this.ultraLabel10);
		this.groupBox1.Location = new System.Drawing.Point(8, 4);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(500, 392);
		this.groupBox1.TabIndex = 27;
		this.groupBox1.TabStop = false;
		appearance7.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblOldTotal.Appearance = appearance7;
		this.lblOldTotal.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.lblOldTotal.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblOldTotal.Location = new System.Drawing.Point(200, 76);
		this.lblOldTotal.Name = "lblOldTotal";
		this.lblOldTotal.Size = new System.Drawing.Size(172, 23);
		this.lblOldTotal.TabIndex = 33;
		this.lblOldTotal.Text = "0";
		appearance8.TextHAlign = Infragistics.Win.HAlign.Right;
		this.ultraLabel21.Appearance = appearance8;
		this.ultraLabel21.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraLabel21.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel21.Location = new System.Drawing.Point(40, 76);
		this.ultraLabel21.Name = "ultraLabel21";
		this.ultraLabel21.Size = new System.Drawing.Size(139, 23);
		this.ultraLabel21.TabIndex = 32;
		this.ultraLabel21.Text = "最原始總金額:";
		this.groupBox3.Controls.Add(this.OptionSet2);
		this.groupBox3.Location = new System.Drawing.Point(236, 240);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(244, 84);
		this.groupBox3.TabIndex = 31;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "調價選項";
		appearance9.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
		appearance9.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.OptionSet2.Appearance = appearance9;
		this.OptionSet2.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.OptionSet2.CheckedIndex = 0;
		this.OptionSet2.ItemAppearance = appearance10;
		this.OptionSet2.ItemOrigin = new System.Drawing.Point(15, 0);
		valueListItem3.DataValue = "1";
		valueListItem3.DisplayText = "依工項代碼開頭字母調價";
		valueListItem4.DataValue = "9999";
		valueListItem4.DisplayText = "依工項各自比率調價";
		this.OptionSet2.Items.Add(valueListItem3);
		this.OptionSet2.Items.Add(valueListItem4);
		this.OptionSet2.ItemSpacingHorizontal = 20;
		this.OptionSet2.Location = new System.Drawing.Point(8, 28);
		this.OptionSet2.Name = "OptionSet2";
		this.OptionSet2.Size = new System.Drawing.Size(224, 44);
		this.OptionSet2.TabIndex = 30;
		this.OptionSet2.Text = "依工項代碼開頭字母調價";
		this.ultraLabel18.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel18.Location = new System.Drawing.Point(260, 16);
		this.ultraLabel18.Name = "ultraLabel18";
		this.ultraLabel18.Size = new System.Drawing.Size(228, 28);
		this.ultraLabel18.TabIndex = 29;
		this.ultraLabel18.Text = "依目前總價來進行調價，打折前不先回復成最原始總價";
		this.toolTip1.SetToolTip(this.ultraLabel18, "勾選此一方式：使用者將以最後調出來的價格，再繼續進行調價(即可累進式調價)。建議先在專案管理將此預算或標單複製一份");
		this.ultraLabel18.Click += new System.EventHandler(ultraLabel18_Click);
		appearance11.ForeColor = System.Drawing.Color.Red;
		this.ultraLabel17.Appearance = appearance11;
		this.ultraLabel17.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel17.Location = new System.Drawing.Point(258, 47);
		this.ultraLabel17.Name = "ultraLabel17";
		this.ultraLabel17.Size = new System.Drawing.Size(228, 23);
		this.ultraLabel17.TabIndex = 28;
		this.ultraLabel17.Text = "(勾選此種方式無法回復到最原始的總價)";
		appearance12.FontData.SizeInPoints = 9f;
		appearance12.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.CB_UnRestoreCost.Appearance = appearance12;
		this.CB_UnRestoreCost.Location = new System.Drawing.Point(242, 10);
		this.CB_UnRestoreCost.Name = "CB_UnRestoreCost";
		this.CB_UnRestoreCost.Size = new System.Drawing.Size(16, 26);
		this.CB_UnRestoreCost.TabIndex = 27;
		this.toolTip1.SetToolTip(this.CB_UnRestoreCost, "勾選此一方式：使用者將以最後調出來的價格，再繼續進行調價(即可累進式調價)。建議先在專案管理將此預算或標單複製一份");
		appearance13.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance13.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
		appearance13.FontData.Italic = Infragistics.Win.DefaultableBoolean.False;
		appearance13.FontData.Name = "細明體";
		appearance13.FontData.SizeInPoints = 11.25f;
		appearance13.FontData.Strikeout = Infragistics.Win.DefaultableBoolean.False;
		appearance13.FontData.Underline = Infragistics.Win.DefaultableBoolean.False;
		appearance13.TextHAlign = Infragistics.Win.HAlign.Right;
		this.txtM.Appearance = appearance13;
		this.txtM.AutoSize = true;
		this.txtM.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.txtM.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.txtM.Location = new System.Drawing.Point(116, 300);
		this.txtM.Name = "txtM";
		this.txtM.Size = new System.Drawing.Size(80, 24);
		this.txtM.TabIndex = 21;
		this.txtM.Text = "100";
		this.txtM.Leave += new System.EventHandler(txtL_Leave);
		appearance14.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance14.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
		appearance14.FontData.Italic = Infragistics.Win.DefaultableBoolean.False;
		appearance14.FontData.Name = "細明體";
		appearance14.FontData.SizeInPoints = 11.25f;
		appearance14.FontData.Strikeout = Infragistics.Win.DefaultableBoolean.False;
		appearance14.FontData.Underline = Infragistics.Win.DefaultableBoolean.False;
		appearance14.TextHAlign = Infragistics.Win.HAlign.Right;
		this.txtE.Appearance = appearance14;
		this.txtE.AutoSize = true;
		this.txtE.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.txtE.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.txtE.Location = new System.Drawing.Point(116, 272);
		this.txtE.Name = "txtE";
		this.txtE.Size = new System.Drawing.Size(80, 24);
		this.txtE.TabIndex = 18;
		this.txtE.Text = "100";
		this.txtE.Leave += new System.EventHandler(txtL_Leave);
		appearance15.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance15.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
		appearance15.FontData.Italic = Infragistics.Win.DefaultableBoolean.False;
		appearance15.FontData.Name = "細明體";
		appearance15.FontData.SizeInPoints = 11.25f;
		appearance15.FontData.Strikeout = Infragistics.Win.DefaultableBoolean.False;
		appearance15.FontData.Underline = Infragistics.Win.DefaultableBoolean.False;
		appearance15.TextHAlign = Infragistics.Win.HAlign.Right;
		this.txtRatio.Appearance = appearance15;
		this.txtRatio.AutoSize = true;
		this.txtRatio.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.txtRatio.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.txtRatio.Location = new System.Drawing.Point(188, 120);
		this.txtRatio.MaxLength = 10;
		this.txtRatio.Name = "txtRatio";
		this.txtRatio.Size = new System.Drawing.Size(184, 24);
		this.txtRatio.TabIndex = 26;
		this.txtRatio.Text = "0";
		this.txtRatio.ValueChanged += new System.EventHandler(txtRatio_ValueChanged);
		this.txtRatio.Leave += new System.EventHandler(txtRatio_Leave);
		appearance16.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance16.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
		appearance16.FontData.Italic = Infragistics.Win.DefaultableBoolean.False;
		appearance16.FontData.Name = "細明體";
		appearance16.FontData.SizeInPoints = 11.25f;
		appearance16.FontData.Strikeout = Infragistics.Win.DefaultableBoolean.False;
		appearance16.FontData.Underline = Infragistics.Win.DefaultableBoolean.False;
		appearance16.TextHAlign = Infragistics.Win.HAlign.Right;
		this.txtL.Appearance = appearance16;
		this.txtL.AutoSize = true;
		this.txtL.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.txtL.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.txtL.Location = new System.Drawing.Point(116, 244);
		this.txtL.Name = "txtL";
		this.txtL.Size = new System.Drawing.Size(80, 24);
		this.txtL.TabIndex = 15;
		this.txtL.Text = "100";
		this.txtL.Leave += new System.EventHandler(txtL_Leave);
		appearance17.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance17.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
		appearance17.FontData.Italic = Infragistics.Win.DefaultableBoolean.False;
		appearance17.FontData.Name = "細明體";
		appearance17.FontData.SizeInPoints = 11.25f;
		appearance17.FontData.Strikeout = Infragistics.Win.DefaultableBoolean.False;
		appearance17.FontData.Underline = Infragistics.Win.DefaultableBoolean.False;
		appearance17.TextHAlign = Infragistics.Win.HAlign.Right;
		this.txtAmount.Appearance = appearance17;
		this.txtAmount.AutoSize = true;
		this.txtAmount.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.txtAmount.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.txtAmount.Location = new System.Drawing.Point(188, 148);
		this.txtAmount.Name = "txtAmount";
		this.txtAmount.Size = new System.Drawing.Size(208, 24);
		this.txtAmount.TabIndex = 25;
		this.txtAmount.Validating += new System.ComponentModel.CancelEventHandler(txtAmount_Validating);
		this.txtAmount.ValueChanged += new System.EventHandler(txtAmount_ValueChanged);
		this.txtAmount.Leave += new System.EventHandler(txtAmount_Leave);
		appearance18.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance18.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel7.Appearance = appearance18;
		this.ultraLabel7.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraLabel7.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel7.Location = new System.Drawing.Point(40, 148);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(139, 23);
		this.ultraLabel7.TabIndex = 11;
		this.ultraLabel7.Text = "調整後總金額:";
		appearance19.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance19.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance19;
		this.ultraLabel5.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraLabel5.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel5.Location = new System.Drawing.Point(40, 124);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(139, 23);
		this.ultraLabel5.TabIndex = 8;
		this.ultraLabel5.Text = "調整比例:";
		this.ultraLabel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(28, 48);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(176, 23);
		this.ultraLabel1.TabIndex = 3;
		this.ultraLabel1.Text = "依據比例來調整總金額";
		this.RB2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.RB2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.RB2.Location = new System.Drawing.Point(12, 196);
		this.RB2.Name = "RB2";
		this.RB2.Size = new System.Drawing.Size(216, 24);
		this.RB2.TabIndex = 1;
		this.RB2.Text = "人機料比例調整";
		this.RB2.UseVisualStyleBackColor = false;
		appearance20.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance20.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel12.Appearance = appearance20;
		this.ultraLabel12.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraLabel12.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel12.Location = new System.Drawing.Point(68, 276);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(44, 23);
		this.ultraLabel12.TabIndex = 17;
		this.ultraLabel12.Text = "機具:";
		this.ultraLabel2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraLabel2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel2.Location = new System.Drawing.Point(28, 360);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(384, 23);
		this.ultraLabel2.TabIndex = 5;
		this.ultraLabel2.Text = "將總金額回復為前一次打折前的金額";
		appearance21.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel11.Appearance = appearance21;
		this.ultraLabel11.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraLabel11.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel11.Location = new System.Drawing.Point(200, 272);
		this.ultraLabel11.Name = "ultraLabel11";
		this.ultraLabel11.Size = new System.Drawing.Size(16, 23);
		this.ultraLabel11.TabIndex = 19;
		this.ultraLabel11.Text = "%";
		this.RB3.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.RB3.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.RB3.Location = new System.Drawing.Point(12, 336);
		this.RB3.Name = "RB3";
		this.RB3.Size = new System.Drawing.Size(216, 24);
		this.RB3.TabIndex = 2;
		this.RB3.Text = "總價回復";
		this.RB3.UseVisualStyleBackColor = false;
		appearance22.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance22.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel8.Appearance = appearance22;
		this.ultraLabel8.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraLabel8.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel8.Location = new System.Drawing.Point(68, 244);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(44, 23);
		this.ultraLabel8.TabIndex = 13;
		this.ultraLabel8.Text = "人工:";
		appearance23.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance23.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblTotal.Appearance = appearance23;
		this.lblTotal.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.lblTotal.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblTotal.Location = new System.Drawing.Point(200, 100);
		this.lblTotal.Name = "lblTotal";
		this.lblTotal.Size = new System.Drawing.Size(172, 23);
		this.lblTotal.TabIndex = 7;
		this.lblTotal.Text = "0";
		appearance24.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance24.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel14.Appearance = appearance24;
		this.ultraLabel14.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraLabel14.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel14.Location = new System.Drawing.Point(68, 304);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(44, 23);
		this.ultraLabel14.TabIndex = 20;
		this.ultraLabel14.Text = "材料:";
		this.ultraLabel9.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraLabel9.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel9.Location = new System.Drawing.Point(28, 220);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(384, 23);
		this.ultraLabel9.TabIndex = 14;
		this.ultraLabel9.Text = "依據人工、機具、材料比例來調整總金額";
		appearance25.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel13.Appearance = appearance25;
		this.ultraLabel13.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraLabel13.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel13.Location = new System.Drawing.Point(200, 304);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(12, 23);
		this.ultraLabel13.TabIndex = 22;
		this.ultraLabel13.Text = "%";
		this.RB1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.RB1.Checked = true;
		this.RB1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.RB1.Location = new System.Drawing.Point(12, 24);
		this.RB1.Name = "RB1";
		this.RB1.Size = new System.Drawing.Size(216, 24);
		this.RB1.TabIndex = 0;
		this.RB1.TabStop = true;
		this.RB1.Text = "總價比例調整";
		this.RB1.UseVisualStyleBackColor = false;
		appearance26.TextHAlign = Infragistics.Win.HAlign.Right;
		this.ultraLabel3.Appearance = appearance26;
		this.ultraLabel3.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraLabel3.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel3.Location = new System.Drawing.Point(40, 100);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(139, 23);
		this.ultraLabel3.TabIndex = 6;
		this.ultraLabel3.Text = "目前總金額:";
		appearance27.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel6.Appearance = appearance27;
		this.ultraLabel6.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(376, 120);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(20, 23);
		this.ultraLabel6.TabIndex = 10;
		this.ultraLabel6.Text = "%";
		appearance28.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel10.Appearance = appearance28;
		this.ultraLabel10.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraLabel10.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel10.Location = new System.Drawing.Point(200, 244);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(16, 23);
		this.ultraLabel10.TabIndex = 16;
		this.ultraLabel10.Text = "%";
		this.Tab_B.Controls.Add(this.lbMessage);
		this.Tab_B.Controls.Add(this.ultraLabel4);
		this.Tab_B.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Tab_B.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(518, 554);
		this.lbMessage.Location = new System.Drawing.Point(36, 104);
		this.lbMessage.Name = "lbMessage";
		this.lbMessage.Size = new System.Drawing.Size(408, 23);
		this.lbMessage.TabIndex = 1;
		this.lbMessage.Text = "這個動作會花些時間，請稍候。";
		this.ultraLabel4.Location = new System.Drawing.Point(36, 76);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(408, 23);
		this.ultraLabel4.TabIndex = 0;
		this.ultraLabel4.Text = "總價調整運算中....";
		this.Tab_C.Controls.Add(this.ultraLabel19);
		this.Tab_C.Controls.Add(this.ultraLabel16);
		this.Tab_C.Controls.Add(this.panel1);
		this.Tab_C.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Tab_C.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_C.Name = "Tab_C";
		this.Tab_C.Size = new System.Drawing.Size(518, 554);
		appearance29.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance29.ForeColor = System.Drawing.Color.Red;
		this.ultraLabel19.Appearance = appearance29;
		this.ultraLabel19.Location = new System.Drawing.Point(24, 196);
		this.ultraLabel19.Name = "ultraLabel19";
		this.ultraLabel19.Size = new System.Drawing.Size(468, 86);
		this.ultraLabel19.TabIndex = 7;
		this.ultraLabel19.Text = "總價調整運算完畢";
		this.ultraLabel19.Visible = false;
		appearance30.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance30.TextHAlign = Infragistics.Win.HAlign.Center;
		this.ultraLabel16.Appearance = appearance30;
		this.ultraLabel16.Location = new System.Drawing.Point(24, 168);
		this.ultraLabel16.Name = "ultraLabel16";
		this.ultraLabel16.Size = new System.Drawing.Size(468, 23);
		this.ultraLabel16.TabIndex = 6;
		this.ultraLabel16.Text = "總價調整運算完畢";
		this.panel1.Controls.Add(this.ultraButton2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 518);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(518, 36);
		this.panel1.TabIndex = 5;
		this.ultraButton2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance31.Image = resources.GetObject("appearance31.Image");
		appearance31.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton2.Appearance = appearance31;
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton2.DialogResult = System.Windows.Forms.DialogResult.OK;
		appearance32.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance32.BackColor2 = System.Drawing.Color.White;
		appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.ultraButton2.HotTrackAppearance = appearance32;
		this.ultraButton2.HotTracking = true;
		this.ultraButton2.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton2.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton2.Location = new System.Drawing.Point(426, 3);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(88, 31);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 9;
		this.ultraButton2.Text = "確定";
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_A);
		this.Tab_Ctrl.Controls.Add(this.Tab_B);
		this.Tab_Ctrl.Controls.Add(this.Tab_C);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(518, 512);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabIndex = 0;
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "tab1";
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "tab2";
		ultraTab3.TabPage = this.Tab_C;
		ultraTab3.Text = "tab3";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[3] { ultraTab1, ultraTab2, ultraTab3 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(518, 512);
		this.toolTip1.AutoPopDelay = 15000;
		this.toolTip1.InitialDelay = 500;
		this.toolTip1.ReshowDelay = 100;
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		this.AutoSize = true;
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.ultraButton4;
		base.ClientSize = new System.Drawing.Size(518, 512);
		base.Controls.Add(this.Tab_Ctrl);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormBudgetResetCost";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "總價調整";
		base.Load += new System.EventHandler(FormBudgetResetCost_Load);
		base.Activated += new System.EventHandler(FormBudgetResetCost_Activated);
		this.Tab_A.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.OptionSet1).EndInit();
		this.panel5.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.OptionSet2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtM).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtE).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtL).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAmount).EndInit();
		this.Tab_B.ResumeLayout(false);
		this.Tab_C.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
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
}
