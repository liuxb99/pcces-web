using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.DomainModule.BudExe;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.DomainModule.General;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinSchedule;
using Infragistics.Win.UltraWinSchedule.CalendarCombo;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.Budget.BudgetChange;

public class FormBudgetChangeInfo : Form
{
	public enum Mode
	{
		ReadOnly,
		Edit,
		New
	}

	private string projectCode;

	private string targetProjectCode;

	private string userID;

	private int version;

	private Mode openMode = Mode.ReadOnly;

	private bool changeManagement;

	private BudgetType.Types budgetType;

	public bool PickFromEstimateCost = false;

	public DataSet CostEstimateCombinedBudItem;

	private IContainer components = null;

	private UltraButton btnOK;

	private UltraButton btnCancel;

	private TextBox tbPurpose;

	private TextBox tbDescription;

	private TextBox tbContent;

	private Label lbPurpose;

	private Label lbDescription;

	private Label lbContent;

	private Label lbChangeNum;

	private Label lbChangeVersion;

	private Label lbChagedDate;

	private UltraCalendarCombo calendarChangedDate;

	private Label lbPersonInCharge;

	private TextBox tbPersonInCharge;

	private Label lbEOT;

	private UltraNumericEditor NEEOT;

	private Label lbChangeNo;

	private TextBox tbChangeNo;

	private Label lbFinishDate;

	private UltraCalendarCombo calendarFinishDate;

	private Label lbAccountability;

	private TextBox tbAccountability;

	private Label lbReason;

	private TextBox tbReason;

	private UltraButton btnPickFromEstimateCost;

	private UltraTabControl TabCtrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_A;

	private UltraTabPageControl Tab_B;

	private C1FlexGrid grid1;

	public string _projectCode
	{
		get
		{
			return projectCode;
		}
		set
		{
			projectCode = value;
		}
	}

	public string _TargetProjectCode
	{
		get
		{
			return targetProjectCode;
		}
		set
		{
			targetProjectCode = value;
		}
	}

	public string _userID
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

	public int _version
	{
		get
		{
			return version;
		}
		set
		{
			version = value;
		}
	}

	public Mode _openMode
	{
		get
		{
			return openMode;
		}
		set
		{
			openMode = value;
		}
	}

	public BudgetType.Types _budgetType
	{
		get
		{
			return budgetType;
		}
		set
		{
			budgetType = value;
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

	public bool ChangeManagement
	{
		set
		{
			changeManagement = value;
		}
	}

	public FormBudgetChangeInfo()
	{
		InitializeComponent();
	}

	private void FormBudgetChangeInfo_Load(object sender, EventArgs e)
	{
		lbChangeVersion.Text = version.ToString();
		if (openMode == Mode.ReadOnly || openMode == Mode.Edit)
		{
			LoadData();
		}
		else
		{
			calendarChangedDate.Value = DateTime.Now;
			calendarFinishDate.Value = DateTime.Now;
			if (SysConfig.SysEnableNewAttResponsibility)
			{
				grid1[1, "Responsibility"] = "(1-a)可追加";
				grid1[1, "Amount"] = "";
				grid1[2, "Responsibility"] = "(1-b)不可追加";
				grid1[2, "Amount"] = "";
				grid1[3, "Responsibility"] = "(2)投標自算及預算編製因素";
				grid1[3, "Amount"] = "";
				grid1[4, "Responsibility"] = "(3)設計圖面的衝突";
				grid1[4, "Amount"] = "";
				grid1[5, "Responsibility"] = "(4)工地需求，施工因素及工法變更";
				grid1[5, "Amount"] = "";
				grid1[6, "Responsibility"] = "(5)拆工料，單價，單位及發包方式變更";
				grid1[6, "Amount"] = "";
				grid1[7, "Responsibility"] = "(6)應扣包商款項";
				grid1[7, "Amount"] = "";
				grid1[8, "Responsibility"] = "(7)天然災害";
				grid1[8, "Amount"] = "";
			}
		}
		SetupComponentStatus();
		UIStringChange();
	}

	private void UIStringChange()
	{
		if (SysConfig.SysComsLoginID == "22132814")
		{
			lbChangeNum.Text = "變更項次：";
			lbChagedDate.Text = "入電腦日：";
			lbChangeNo.Text = "申請單編號：";
			lbPurpose.Text = "工程項目：";
			lbDescription.Text = "原因說明：";
			lbContent.Text = "業變單編號：";
			lbReason.Text = "備註：";
		}
	}

	private void SetupComponentStatus()
	{
		if (SysConfig.SysChangeManagement && budgetType == (BudgetType.Types)0)
		{
			btnPickFromEstimateCost.Visible = true;
		}
		if (openMode == Mode.ReadOnly)
		{
			btnOK.Visible = false;
			btnCancel.Text = "關閉";
			calendarChangedDate.ReadOnly = true;
			NEEOT.ReadOnly = true;
			tbChangeNo.ReadOnly = true;
			calendarFinishDate.ReadOnly = true;
			tbPersonInCharge.ReadOnly = true;
			tbPurpose.ReadOnly = true;
			tbDescription.ReadOnly = true;
			tbContent.ReadOnly = true;
			tbAccountability.ReadOnly = true;
			tbReason.ReadOnly = true;
		}
		else if (openMode == Mode.Edit)
		{
			Text = "變更版次資訊";
		}
		if (SysConfig.SysEnableNewAttResponsibility)
		{
			TabCtrl.SelectedTab = Tab_B.Tab;
		}
		else
		{
			TabCtrl.SelectedTab = Tab_A.Tab;
		}
	}

	private void LoadData()
	{
		BudExeProject budExeProject = new BudExeProject();
		DataSet dsBudExeProject = budExeProject.GetProjectByVersion(projectCode, version);
		DataRow drBudExeProject = dsBudExeProject.Tables[0].Rows[0];
		calendarChangedDate.Value = drBudExeProject["ChangeDate"];
		NEEOT.Value = drBudExeProject["EOT"];
		tbChangeNo.Text = drBudExeProject["ChangeNo"].ToString();
		calendarFinishDate.Value = drBudExeProject["FinishDate"];
		tbPersonInCharge.Text = drBudExeProject["PersonInCharge"].ToString();
		tbPurpose.Text = drBudExeProject["Purpose"].ToString();
		tbDescription.Text = drBudExeProject["Description"].ToString();
		tbContent.Text = drBudExeProject["Content"].ToString();
		tbAccountability.Text = drBudExeProject["Accountability"].ToString();
		tbReason.Text = drBudExeProject["Reason"].ToString();
		if (SysConfig.SysEnableNewAttResponsibility)
		{
			grid1[1, "Responsibility"] = "(1-a)可追加";
			grid1[1, "Amount"] = ((drBudExeProject["Responsibilty01"] == DBNull.Value) ? "" : ((object)ArchConvert.Obj2Decimal(drBudExeProject["Responsibilty01"])));
			grid1[2, "Responsibility"] = "(1-b)不可追加";
			grid1[2, "Amount"] = ((drBudExeProject["Responsibilty02"] == DBNull.Value) ? "" : ((object)ArchConvert.Obj2Decimal(drBudExeProject["Responsibilty02"])));
			grid1[3, "Responsibility"] = "(2)投標自算及預算編製因素";
			grid1[3, "Amount"] = ((drBudExeProject["Responsibilty03"] == DBNull.Value) ? "" : ((object)ArchConvert.Obj2Decimal(drBudExeProject["Responsibilty03"])));
			grid1[4, "Responsibility"] = "(3)設計圖面的衝突";
			grid1[4, "Amount"] = ((drBudExeProject["Responsibilty04"] == DBNull.Value) ? "" : ((object)ArchConvert.Obj2Decimal(drBudExeProject["Responsibilty04"])));
			grid1[5, "Responsibility"] = "(4)工地需求，施工因素及工法變更";
			grid1[5, "Amount"] = ((drBudExeProject["Responsibilty05"] == DBNull.Value) ? "" : ((object)ArchConvert.Obj2Decimal(drBudExeProject["Responsibilty05"])));
			grid1[6, "Responsibility"] = "(5)拆工料，單價，單位及發包方式變更";
			grid1[6, "Amount"] = ((drBudExeProject["Responsibilty06"] == DBNull.Value) ? "" : ((object)ArchConvert.Obj2Decimal(drBudExeProject["Responsibilty06"])));
			grid1[7, "Responsibility"] = "(6)應扣包商款項";
			grid1[7, "Amount"] = ((drBudExeProject["Responsibilty07"] == DBNull.Value) ? "" : ((object)ArchConvert.Obj2Decimal(drBudExeProject["Responsibilty07"])));
			grid1[8, "Responsibility"] = "(7)天然災害";
			grid1[8, "Amount"] = ((drBudExeProject["Responsibilty08"] == DBNull.Value) ? "" : ((object)ArchConvert.Obj2Decimal(drBudExeProject["Responsibilty08"])));
		}
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		ExecResult ER = new ExecResult();
		if (changeManagement)
		{
			ER = AddCostEstimation();
		}
		else if (openMode == Mode.New)
		{
			ER = AddBudgetChange();
		}
		else if (openMode == Mode.Edit)
		{
			ER = EditBudgetChange();
		}
		if (ER.ReturnCode != 0)
		{
			MessageBox.Show(ER.Message);
		}
		else
		{
			base.DialogResult = DialogResult.OK;
		}
	}

	private ExecResult AddBudgetChange()
	{
		BudExeProject budExeProject = new BudExeProject();
		object Rep01 = (SysConfig.SysEnableNewAttResponsibility ? grid1[1, "Amount"] : DBNull.Value);
		object Rep2 = (SysConfig.SysEnableNewAttResponsibility ? grid1[2, "Amount"] : DBNull.Value);
		object Rep3 = (SysConfig.SysEnableNewAttResponsibility ? grid1[3, "Amount"] : DBNull.Value);
		object Rep4 = (SysConfig.SysEnableNewAttResponsibility ? grid1[4, "Amount"] : DBNull.Value);
		object Rep5 = (SysConfig.SysEnableNewAttResponsibility ? grid1[5, "Amount"] : DBNull.Value);
		object Rep6 = (SysConfig.SysEnableNewAttResponsibility ? grid1[6, "Amount"] : DBNull.Value);
		object Rep7 = (SysConfig.SysEnableNewAttResponsibility ? grid1[7, "Amount"] : DBNull.Value);
		object Rep8 = (SysConfig.SysEnableNewAttResponsibility ? grid1[8, "Amount"] : DBNull.Value);
		if (grid1[1, "Amount"] == null || grid1[2, "Amount"] == null || grid1[3, "Amount"] == null || grid1[4, "Amount"] == null || grid1[5, "Amount"] == null || grid1[6, "Amount"] == null || grid1[7, "Amount"] == null || grid1[8, "Amount"] == null)
		{
			Rep01 = DBNull.Value;
			Rep2 = DBNull.Value;
			Rep3 = DBNull.Value;
			Rep4 = DBNull.Value;
			Rep5 = DBNull.Value;
			Rep6 = DBNull.Value;
			Rep7 = DBNull.Value;
			Rep8 = DBNull.Value;
		}
		else
		{
			if (grid1[1, "Amount"].ToString() == "")
			{
				Rep01 = DBNull.Value;
			}
			if (grid1[2, "Amount"].ToString() == "")
			{
				Rep2 = DBNull.Value;
			}
			if (grid1[3, "Amount"].ToString() == "")
			{
				Rep3 = DBNull.Value;
			}
			if (grid1[4, "Amount"].ToString() == "")
			{
				Rep4 = DBNull.Value;
			}
			if (grid1[5, "Amount"].ToString() == "")
			{
				Rep5 = DBNull.Value;
			}
			if (grid1[6, "Amount"].ToString() == "")
			{
				Rep6 = DBNull.Value;
			}
			if (grid1[7, "Amount"].ToString() == "")
			{
				Rep7 = DBNull.Value;
			}
			if (grid1[8, "Amount"].ToString() == "")
			{
				Rep8 = DBNull.Value;
			}
		}
		ExecResult ER = budExeProject.AddProject(projectCode, version, tbChangeNo.Text.Trim(), calendarChangedDate.Value, NEEOT.Value, calendarFinishDate.Value, tbPersonInCharge.Text.Trim(), tbPurpose.Text.Trim(), tbDescription.Text.Trim(), tbContent.Text.Trim(), 0, tbAccountability.Text.Trim(), tbReason.Text.Trim(), userID, DateTime.Now, Rep01, Rep2, Rep3, Rep4, Rep5, Rep6, Rep7, Rep8);
		if (ER.ReturnCode != 0)
		{
			ER.Message = "新增預算變更歷史版次資料失敗(AddProject)：" + ER.Message;
		}
		else
		{
			BudItemA budItemA = new BudItemA();
			budItemA.LockBudItemA(projectCode, Lock: true);
			BudProjMrsA budProjMrsA = new BudProjMrsA();
			budProjMrsA.LockBudProjMrsA(projectCode, Lock: true);
			BudProject budProject = new BudProject();
			ER = budProject.CopyProjectToBudExe(projectCode, version - 1);
			if (ER.ReturnCode == 0 && PickFromEstimateCost)
			{
				DataSet dsBudItemA = new DataSet();
				dsBudItemA.Tables.Add(CostEstimateCombinedBudItem.Tables[0].Copy());
				ER = budItemA.GetDatasetUpdate(dsBudItemA);
				if (ER.ReturnCode != 0)
				{
					ER.Message = "更新合併預估成本至新期版次失敗(budItemA)：" + ER.Message;
				}
				else
				{
					BudItemB budItemB = new BudItemB();
					DataSet dsBudItemB = new DataSet();
					dsBudItemB.Tables.Add(CostEstimateCombinedBudItem.Tables[1].Copy());
					ER = budItemB.GetDatasetUpdate(dsBudItemB);
					if (ER.ReturnCode != 0)
					{
						ER.Message = "更新合併預估成本至新期版次失敗(budItemB)：" + ER.Message;
					}
					else
					{
						BudItemC budItemC = new BudItemC();
						DataSet dsBudItemC = new DataSet();
						dsBudItemC.Tables.Add(CostEstimateCombinedBudItem.Tables[2].Copy());
						ER = budItemC.GetDatasetUpdate(dsBudItemC);
						if (ER.ReturnCode != 0)
						{
							ER.Message = "更新合併預估成本至新期版次失敗(budItemC)：" + ER.Message;
						}
						else
						{
							DataSet dsBudProjMrsA = new DataSet();
							dsBudProjMrsA.Tables.Add(CostEstimateCombinedBudItem.Tables[3].Copy());
							ER = budProjMrsA.UpdateProjMrsA(dsBudProjMrsA);
							if (ER.ReturnCode != 0)
							{
								ER.Message = "更新合併預估成本至新期版次失敗(budProjMrsA)：" + ER.Message;
							}
							else
							{
								BudProjMrsB budProjMrsB = new BudProjMrsB();
								DataSet dsBudProjMrsB = new DataSet();
								dsBudProjMrsB.Tables.Add(CostEstimateCombinedBudItem.Tables[4].Copy());
								ER = budProjMrsB.UpdateProjMrsB(dsBudProjMrsB);
								if (ER.ReturnCode != 0)
								{
									ER.Message = "更新合併預估成本至新期版次失敗(budProjMrsB)：" + ER.Message;
								}
								else
								{
									BudProjMrsC budProjMrsC = new BudProjMrsC();
									DataSet dsBudProjMrsC = new DataSet();
									dsBudProjMrsC.Tables.Add(CostEstimateCombinedBudItem.Tables[5].Copy());
									ER = budProjMrsC.UpdateProjMrsCByPccesCode(dsBudProjMrsC);
									if (ER.ReturnCode != 0)
									{
										ER.Message = "更新合併預估成本至新期版次失敗(dsBudProjMrsC)：" + ER.Message;
									}
								}
							}
						}
					}
				}
			}
		}
		return ER;
	}

	private ExecResult EditBudgetChange()
	{
		object Rep01 = (SysConfig.SysEnableNewAttResponsibility ? grid1[1, "Amount"] : DBNull.Value);
		object Rep2 = (SysConfig.SysEnableNewAttResponsibility ? grid1[2, "Amount"] : DBNull.Value);
		object Rep3 = (SysConfig.SysEnableNewAttResponsibility ? grid1[3, "Amount"] : DBNull.Value);
		object Rep4 = (SysConfig.SysEnableNewAttResponsibility ? grid1[4, "Amount"] : DBNull.Value);
		object Rep5 = (SysConfig.SysEnableNewAttResponsibility ? grid1[5, "Amount"] : DBNull.Value);
		object Rep6 = (SysConfig.SysEnableNewAttResponsibility ? grid1[6, "Amount"] : DBNull.Value);
		object Rep7 = (SysConfig.SysEnableNewAttResponsibility ? grid1[7, "Amount"] : DBNull.Value);
		object Rep8 = (SysConfig.SysEnableNewAttResponsibility ? grid1[8, "Amount"] : DBNull.Value);
		if (grid1[1, "Amount"].ToString() == "")
		{
			Rep01 = DBNull.Value;
		}
		if (grid1[2, "Amount"].ToString() == "")
		{
			Rep2 = DBNull.Value;
		}
		if (grid1[3, "Amount"].ToString() == "")
		{
			Rep3 = DBNull.Value;
		}
		if (grid1[4, "Amount"].ToString() == "")
		{
			Rep4 = DBNull.Value;
		}
		if (grid1[5, "Amount"].ToString() == "")
		{
			Rep5 = DBNull.Value;
		}
		if (grid1[6, "Amount"].ToString() == "")
		{
			Rep6 = DBNull.Value;
		}
		if (grid1[7, "Amount"].ToString() == "")
		{
			Rep7 = DBNull.Value;
		}
		if (grid1[8, "Amount"].ToString() == "")
		{
			Rep8 = DBNull.Value;
		}
		BudExeProject budExeProject = new BudExeProject();
		return budExeProject.UpdateProject(projectCode, version, tbChangeNo.Text.Trim(), calendarChangedDate.Value, NEEOT.Value, calendarFinishDate.Value, tbPersonInCharge.Text.Trim(), tbPurpose.Text.Trim(), tbDescription.Text.Trim(), tbContent.Text.Trim(), tbAccountability.Text.Trim(), tbReason.Text.Trim(), userID, DateTime.Now, Rep01, Rep2, Rep3, Rep4, Rep5, Rep6, Rep7, Rep8);
	}

	private ExecResult AddCostEstimation()
	{
		ExecResult ER = new ExecResult();
		string budgetTypeNumber = ArchConvert.Obj2String((int)budgetType);
		BudProject budProject = new BudProject();
		if (budgetType == BudgetType.Types.CostEstimation)
		{
			targetProjectCode = Utility.GetGuid("Est");
			ER = budProject.CopyBudProject(projectCode, targetProjectCode, budgetTypeNumber, Locked: false);
			if (ER.ReturnCode == 0)
			{
				BudItemA budItemA = new BudItemA();
				ER = budItemA.ClearQty(targetProjectCode);
			}
		}
		else
		{
			if (budgetType != BudgetType.Types.CostQuotationMerged)
			{
				throw new Exception("Illegal BudgetType");
			}
			targetProjectCode = Utility.GetGuid("Mer");
			PubProject pubProject = new PubProject();
			ER = pubProject.CopyPubProject(projectCode, targetProjectCode);
			if (ER.ReturnCode != 0)
			{
				ER.Message = "新增一期變更管理版次失敗：" + ER.Message;
				return ER;
			}
		}
		if (ER.ReturnCode != 0)
		{
			ER.Message = "新增一期變更管理版次失敗：" + ER.Message;
			return ER;
		}
		BudProjectCodeMapping budProjectCodeMapping = new BudProjectCodeMapping();
		ER = budProjectCodeMapping.AddBudProjectCodeMapping(projectCode, targetProjectCode, budgetTypeNumber, version, Approved: false, (DateTime)calendarChangedDate.Value, tbPersonInCharge.Text.Trim(), tbPurpose.Text.Trim(), tbDescription.Text.Trim(), tbContent.Text.Trim(), tbAccountability.Text.Trim(), tbReason.Text.Trim(), userID, string.Empty);
		if (ER.ReturnCode != 0)
		{
			ER.Message = "新增一期變更管理版次失敗：" + ER.Message;
		}
		return ER;
	}

	private void btnPickFromEstimateCost_Click(object sender, EventArgs e)
	{
		FormCostEst2BudgetChange CostEst2BudgetChange = new FormCostEst2BudgetChange(userID, projectCode);
		if (CostEst2BudgetChange.ShowDialog() == DialogResult.OK)
		{
			CostEstimateCombinedBudItem = CostEst2BudgetChange._CostEstimateCombinedBudItem;
			PickFromEstimateCost = true;
		}
		CostEst2BudgetChange.Dispose();
		CostEst2BudgetChange = null;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.BudgetChange.FormBudgetChangeInfo));
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton1 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton2 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton3 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.tbAccountability = new System.Windows.Forms.TextBox();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.grid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
		this.btnOK = new Infragistics.Win.Misc.UltraButton();
		this.btnCancel = new Infragistics.Win.Misc.UltraButton();
		this.tbPurpose = new System.Windows.Forms.TextBox();
		this.tbDescription = new System.Windows.Forms.TextBox();
		this.tbContent = new System.Windows.Forms.TextBox();
		this.lbPurpose = new System.Windows.Forms.Label();
		this.lbDescription = new System.Windows.Forms.Label();
		this.lbContent = new System.Windows.Forms.Label();
		this.lbChangeNum = new System.Windows.Forms.Label();
		this.lbChangeVersion = new System.Windows.Forms.Label();
		this.lbChagedDate = new System.Windows.Forms.Label();
		this.calendarChangedDate = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.lbPersonInCharge = new System.Windows.Forms.Label();
		this.tbPersonInCharge = new System.Windows.Forms.TextBox();
		this.lbEOT = new System.Windows.Forms.Label();
		this.NEEOT = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
		this.lbChangeNo = new System.Windows.Forms.Label();
		this.tbChangeNo = new System.Windows.Forms.TextBox();
		this.lbFinishDate = new System.Windows.Forms.Label();
		this.calendarFinishDate = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.lbAccountability = new System.Windows.Forms.Label();
		this.lbReason = new System.Windows.Forms.Label();
		this.tbReason = new System.Windows.Forms.TextBox();
		this.btnPickFromEstimateCost = new Infragistics.Win.Misc.UltraButton();
		this.TabCtrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.Tab_A.SuspendLayout();
		this.Tab_B.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.grid1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.calendarChangedDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NEEOT).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.calendarFinishDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TabCtrl).BeginInit();
		this.TabCtrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.tbAccountability);
		this.Tab_A.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(542, 153);
		this.tbAccountability.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tbAccountability.Location = new System.Drawing.Point(3, 0);
		this.tbAccountability.MaxLength = 200;
		this.tbAccountability.Multiline = true;
		this.tbAccountability.Name = "tbAccountability";
		this.tbAccountability.Size = new System.Drawing.Size(538, 150);
		this.tbAccountability.TabIndex = 31;
		this.Tab_B.Controls.Add(this.grid1);
		this.Tab_B.Location = new System.Drawing.Point(0, 0);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(542, 153);
		this.grid1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.grid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.grid1.ColumnInfo = resources.GetString("grid1.ColumnInfo");
		this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.grid1.ExtendLastCol = true;
		this.grid1.Font = new System.Drawing.Font("細明體", 11f);
		this.grid1.ForeColor = System.Drawing.Color.Black;
		this.grid1.Location = new System.Drawing.Point(0, 0);
		this.grid1.Name = "grid1";
		this.grid1.Rows.Count = 9;
		this.grid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.grid1.ShowCursor = true;
		this.grid1.ShowSort = false;
		this.grid1.Size = new System.Drawing.Size(542, 153);
		this.grid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("grid1.Styles"));
		this.grid1.TabIndex = 3;
		this.grid1.Tree.Column = 2;
		this.grid1.Tree.LineColor = System.Drawing.Color.Gray;
		this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnOK.Appearance = appearance1;
		this.btnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOK.Font = new System.Drawing.Font("新細明體", 11f);
		this.btnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.btnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.btnOK.Location = new System.Drawing.Point(365, 733);
		this.btnOK.Name = "btnOK";
		this.btnOK.ShowFocusRect = false;
		this.btnOK.ShowOutline = false;
		this.btnOK.Size = new System.Drawing.Size(88, 31);
		this.btnOK.SupportThemes = false;
		this.btnOK.TabIndex = 8;
		this.btnOK.Text = "確定";
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnCancel.Appearance = appearance2;
		this.btnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Font = new System.Drawing.Font("新細明體", 11f);
		this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btnCancel.Location = new System.Drawing.Point(459, 733);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.ShowFocusRect = false;
		this.btnCancel.ShowOutline = false;
		this.btnCancel.Size = new System.Drawing.Size(88, 31);
		this.btnCancel.SupportThemes = false;
		this.btnCancel.TabIndex = 9;
		this.btnCancel.Text = "取消";
		this.tbPurpose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tbPurpose.Location = new System.Drawing.Point(12, 133);
		this.tbPurpose.MaxLength = 200;
		this.tbPurpose.Multiline = true;
		this.tbPurpose.Name = "tbPurpose";
		this.tbPurpose.Size = new System.Drawing.Size(538, 75);
		this.tbPurpose.TabIndex = 11;
		this.tbDescription.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tbDescription.Location = new System.Drawing.Point(12, 240);
		this.tbDescription.MaxLength = 200;
		this.tbDescription.Multiline = true;
		this.tbDescription.Name = "tbDescription";
		this.tbDescription.Size = new System.Drawing.Size(538, 75);
		this.tbDescription.TabIndex = 12;
		this.tbContent.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tbContent.Location = new System.Drawing.Point(12, 345);
		this.tbContent.MaxLength = 200;
		this.tbContent.Multiline = true;
		this.tbContent.Name = "tbContent";
		this.tbContent.Size = new System.Drawing.Size(538, 75);
		this.tbContent.TabIndex = 13;
		this.lbPurpose.Font = new System.Drawing.Font("新細明體", 11f);
		this.lbPurpose.Location = new System.Drawing.Point(12, 110);
		this.lbPurpose.Name = "lbPurpose";
		this.lbPurpose.Size = new System.Drawing.Size(110, 15);
		this.lbPurpose.TabIndex = 14;
		this.lbPurpose.Text = "主旨：";
		this.lbDescription.Font = new System.Drawing.Font("新細明體", 11f);
		this.lbDescription.Location = new System.Drawing.Point(12, 218);
		this.lbDescription.Name = "lbDescription";
		this.lbDescription.Size = new System.Drawing.Size(110, 15);
		this.lbDescription.TabIndex = 15;
		this.lbDescription.Text = "說明：";
		this.lbContent.Font = new System.Drawing.Font("新細明體", 11f);
		this.lbContent.Location = new System.Drawing.Point(12, 323);
		this.lbContent.Name = "lbContent";
		this.lbContent.Size = new System.Drawing.Size(110, 15);
		this.lbContent.TabIndex = 16;
		this.lbContent.Text = "內容：";
		this.lbChangeNum.Font = new System.Drawing.Font("新細明體", 11f);
		this.lbChangeNum.Location = new System.Drawing.Point(12, 20);
		this.lbChangeNum.Name = "lbChangeNum";
		this.lbChangeNum.Size = new System.Drawing.Size(82, 15);
		this.lbChangeNum.TabIndex = 17;
		this.lbChangeNum.Text = "變更次別：";
		this.lbChangeVersion.AutoSize = true;
		this.lbChangeVersion.Font = new System.Drawing.Font("新細明體", 11f);
		this.lbChangeVersion.Location = new System.Drawing.Point(136, 19);
		this.lbChangeVersion.Name = "lbChangeVersion";
		this.lbChangeVersion.Size = new System.Drawing.Size(51, 15);
		this.lbChangeVersion.TabIndex = 18;
		this.lbChangeVersion.Text = "Version";
		this.lbChagedDate.Font = new System.Drawing.Font("新細明體", 11f);
		this.lbChagedDate.Location = new System.Drawing.Point(12, 50);
		this.lbChagedDate.Name = "lbChagedDate";
		this.lbChagedDate.Size = new System.Drawing.Size(82, 15);
		this.lbChagedDate.TabIndex = 19;
		this.lbChagedDate.Text = "變更日期：";
		dateButton1.Caption = "Today";
		this.calendarChangedDate.DateButtons.Add(dateButton1);
		this.calendarChangedDate.Location = new System.Drawing.Point(139, 46);
		this.calendarChangedDate.Name = "calendarChangedDate";
		this.calendarChangedDate.NonAutoSizeHeight = 21;
		this.calendarChangedDate.Size = new System.Drawing.Size(130, 21);
		this.calendarChangedDate.TabIndex = 20;
		this.calendarChangedDate.Value = new System.DateTime(2013, 10, 29, 0, 0, 0, 0);
		this.lbPersonInCharge.Font = new System.Drawing.Font("新細明體", 11f);
		this.lbPersonInCharge.Location = new System.Drawing.Point(287, 50);
		this.lbPersonInCharge.Name = "lbPersonInCharge";
		this.lbPersonInCharge.Size = new System.Drawing.Size(67, 15);
		this.lbPersonInCharge.TabIndex = 21;
		this.lbPersonInCharge.Text = "負責人：";
		this.tbPersonInCharge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tbPersonInCharge.Location = new System.Drawing.Point(405, 46);
		this.tbPersonInCharge.Name = "tbPersonInCharge";
		this.tbPersonInCharge.Size = new System.Drawing.Size(130, 22);
		this.tbPersonInCharge.TabIndex = 22;
		this.lbEOT.Font = new System.Drawing.Font("新細明體", 11f);
		this.lbEOT.Location = new System.Drawing.Point(287, 80);
		this.lbEOT.Name = "lbEOT";
		this.lbEOT.Size = new System.Drawing.Size(112, 15);
		this.lbEOT.TabIndex = 23;
		this.lbEOT.Text = "本次延長工期：";
		this.NEEOT.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.NEEOT.Location = new System.Drawing.Point(405, 77);
		this.NEEOT.Name = "NEEOT";
		this.NEEOT.PromptChar = ' ';
		this.NEEOT.Size = new System.Drawing.Size(130, 21);
		this.NEEOT.TabIndex = 24;
		this.lbChangeNo.Font = new System.Drawing.Font("新細明體", 11f);
		this.lbChangeNo.Location = new System.Drawing.Point(287, 20);
		this.lbChangeNo.Name = "lbChangeNo";
		this.lbChangeNo.Size = new System.Drawing.Size(110, 15);
		this.lbChangeNo.TabIndex = 25;
		this.lbChangeNo.Text = "變更文號：";
		this.tbChangeNo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tbChangeNo.Location = new System.Drawing.Point(405, 14);
		this.tbChangeNo.Name = "tbChangeNo";
		this.tbChangeNo.Size = new System.Drawing.Size(130, 22);
		this.tbChangeNo.TabIndex = 26;
		this.lbFinishDate.Font = new System.Drawing.Font("新細明體", 11f);
		this.lbFinishDate.Location = new System.Drawing.Point(12, 80);
		this.lbFinishDate.Name = "lbFinishDate";
		this.lbFinishDate.Size = new System.Drawing.Size(127, 15);
		this.lbFinishDate.TabIndex = 27;
		this.lbFinishDate.Text = "變更後完工日期：";
		dateButton2.Caption = "Today";
		dateButton3.Caption = "Today";
		this.calendarFinishDate.DateButtons.Add(dateButton2);
		this.calendarFinishDate.DateButtons.Add(dateButton3);
		this.calendarFinishDate.Location = new System.Drawing.Point(139, 77);
		this.calendarFinishDate.Name = "calendarFinishDate";
		this.calendarFinishDate.NonAutoSizeHeight = 21;
		this.calendarFinishDate.Size = new System.Drawing.Size(130, 21);
		this.calendarFinishDate.TabIndex = 28;
		this.calendarFinishDate.Value = new System.DateTime(2013, 10, 29, 0, 0, 0, 0);
		this.lbAccountability.Font = new System.Drawing.Font("新細明體", 11f);
		this.lbAccountability.Location = new System.Drawing.Point(12, 430);
		this.lbAccountability.Name = "lbAccountability";
		this.lbAccountability.Size = new System.Drawing.Size(82, 15);
		this.lbAccountability.TabIndex = 32;
		this.lbAccountability.Text = "責任歸屬：";
		this.lbReason.Font = new System.Drawing.Font("新細明體", 11f);
		this.lbReason.Location = new System.Drawing.Point(12, 607);
		this.lbReason.Name = "lbReason";
		this.lbReason.Size = new System.Drawing.Size(110, 15);
		this.lbReason.TabIndex = 34;
		this.lbReason.Text = "原因：";
		this.tbReason.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tbReason.Location = new System.Drawing.Point(12, 629);
		this.tbReason.MaxLength = 200;
		this.tbReason.Multiline = true;
		this.tbReason.Name = "tbReason";
		this.tbReason.Size = new System.Drawing.Size(538, 98);
		this.tbReason.TabIndex = 33;
		this.btnPickFromEstimateCost.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnPickFromEstimateCost.Appearance = appearance3;
		this.btnPickFromEstimateCost.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnPickFromEstimateCost.Font = new System.Drawing.Font("新細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnPickFromEstimateCost.ImageSize = new System.Drawing.Size(20, 20);
		this.btnPickFromEstimateCost.ImageTransparentColor = System.Drawing.Color.White;
		this.btnPickFromEstimateCost.Location = new System.Drawing.Point(9, 733);
		this.btnPickFromEstimateCost.Name = "btnPickFromEstimateCost";
		this.btnPickFromEstimateCost.ShowFocusRect = false;
		this.btnPickFromEstimateCost.ShowOutline = false;
		this.btnPickFromEstimateCost.Size = new System.Drawing.Size(159, 31);
		this.btnPickFromEstimateCost.SupportThemes = false;
		this.btnPickFromEstimateCost.TabIndex = 36;
		this.btnPickFromEstimateCost.Text = "挑選已核可之預估成本";
		this.btnPickFromEstimateCost.Visible = false;
		this.btnPickFromEstimateCost.Click += new System.EventHandler(btnPickFromEstimateCost_Click);
		this.TabCtrl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TabCtrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.TabCtrl.Controls.Add(this.Tab_A);
		this.TabCtrl.Controls.Add(this.Tab_B);
		this.TabCtrl.Location = new System.Drawing.Point(10, 451);
		this.TabCtrl.Name = "TabCtrl";
		this.TabCtrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.TabCtrl.Size = new System.Drawing.Size(542, 153);
		this.TabCtrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.TabCtrl.TabIndex = 37;
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "tab1";
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "tab2";
		this.TabCtrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2] { ultraTab1, ultraTab2 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(542, 153);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(559, 771);
		base.Controls.Add(this.TabCtrl);
		base.Controls.Add(this.btnPickFromEstimateCost);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.lbReason);
		base.Controls.Add(this.btnOK);
		base.Controls.Add(this.tbReason);
		base.Controls.Add(this.lbAccountability);
		base.Controls.Add(this.calendarFinishDate);
		base.Controls.Add(this.lbFinishDate);
		base.Controls.Add(this.tbChangeNo);
		base.Controls.Add(this.lbChangeNo);
		base.Controls.Add(this.NEEOT);
		base.Controls.Add(this.lbEOT);
		base.Controls.Add(this.tbPersonInCharge);
		base.Controls.Add(this.lbPersonInCharge);
		base.Controls.Add(this.calendarChangedDate);
		base.Controls.Add(this.lbChagedDate);
		base.Controls.Add(this.lbChangeVersion);
		base.Controls.Add(this.lbChangeNum);
		base.Controls.Add(this.lbContent);
		base.Controls.Add(this.lbDescription);
		base.Controls.Add(this.lbPurpose);
		base.Controls.Add(this.tbContent);
		base.Controls.Add(this.tbDescription);
		base.Controls.Add(this.tbPurpose);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MinimizeBox = false;
		base.Name = "FormBudgetChangeInfo";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "變更版次資訊";
		base.Load += new System.EventHandler(FormBudgetChangeInfo_Load);
		this.Tab_A.ResumeLayout(false);
		this.Tab_A.PerformLayout();
		this.Tab_B.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.grid1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.calendarChangedDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NEEOT).EndInit();
		((System.ComponentModel.ISupportInitialize)this.calendarFinishDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TabCtrl).EndInit();
		this.TabCtrl.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
