using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.DomainModule.Coms;
using Archnowledge.Pcces.DomainModule.CostEstQuoation;
using Archnowledge.Pcces.DomainModule.ExportExcel;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.PccesMain.ArchControls;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.Budget.BudgetChange;

public class FormCostEstProjectList : Form
{
	private string projectCode;

	private string userID;

	private string targetProjectCode;

	private BudgetType.Types budgetType;

	public Hashtable SnoMapping = new Hashtable();

	private IContainer components = null;

	private UltraLabel lbTitle;

	private ImageList imageList;

	private UltraButton btnCancel;

	private Panel panelForm;

	private Panel panelGrid;

	private UltraToolbarsDockArea _FormBudgetProjectPick_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormBudgetProjectPick_Toolbars_Dock_Area_Bottom;

	private UltraToolbarsDockArea _FormBudgetProjectPick_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormBudgetProjectPick_Toolbars_Dock_Area_Right;

	private GridBudget gridCostEstimation;

	private UltraButton btnApply;

	private UltraButton btnPrintBudgetCostEstimationRecord;

	private SaveFileDialog saveFileDialog;

	private UltraButton btnComsApplyDetail;

	public string TargetProjectCode => targetProjectCode;

	public FormCostEstProjectList(string projectCode, string userID, BudgetType.Types budgetType)
	{
		InitializeComponent();
		this.projectCode = projectCode;
		this.userID = userID;
		this.budgetType = budgetType;
		switch (budgetType)
		{
		case BudgetType.Types.CostEstimation:
			lbTitle.Text = "  變更管理 - 預估成本列表";
			gridCostEstimation.Cols["CostQuoteMerged"].Visible = false;
			gridCostEstimation.Cols["CostQuoteMergedApproved"].Visible = false;
			gridCostEstimation.Cols["CostEstVersion"].Visible = false;
			btnPrintBudgetCostEstimationRecord.Visible = true;
			if (SysConfig.SysEstimationOnlyCOMS)
			{
				btnApply.Visible = false;
			}
			break;
		case BudgetType.Types.CostQuotationMerged:
			lbTitle.Text = "  變更管理 - 對業主報價列表";
			gridCostEstimation.Cols["CostEst"].Visible = false;
			gridCostEstimation.Cols["CostEstApproved"].Visible = false;
			btnComsApplyDetail.Visible = false;
			break;
		default:
			gridCostEstimation.Cols["CostEst"].Visible = false;
			gridCostEstimation.Cols["CostEstApproved"].Visible = false;
			gridCostEstimation.Cols["CostQuoteMerged"].Visible = false;
			gridCostEstimation.Cols["CostQuoteMergedApproved"].Visible = false;
			gridCostEstimation.Cols["CostEstVersion"].Visible = false;
			btnApply.Visible = false;
			btnComsApplyDetail.Visible = false;
			btnCancel.Text = "關閉";
			break;
		}
	}

	private void FormCostEstProjectList_Load(object sender, EventArgs e)
	{
		LoadData2Drid();
	}

	private void LoadData2Drid()
	{
		DataSet dsProjectCodeMapping = GetData();
		Data2Grid(dsProjectCodeMapping);
	}

	private DataSet GetData()
	{
		BudProjectCodeMapping budProjectCodeMapping = new BudProjectCodeMapping();
		if (budgetType == BudgetType.Types.CostEstimation)
		{
			return budProjectCodeMapping.GetEstimationProject(projectCode);
		}
		if (budgetType == BudgetType.Types.CostQuotationMerged)
		{
			return budProjectCodeMapping.GetQuotationMergedProject(projectCode);
		}
		SourceCostQuoteProject sourceCostQuoteProject = new SourceCostQuoteProject();
		return sourceCostQuoteProject.GetSourceCostQuoteProjectRelation(projectCode);
	}

	private void Data2Grid(DataSet dsProjectCodeMapping)
	{
		DataTable dtProjectCodeMapping = dsProjectCodeMapping.Tables["BudProjectCodeMapping"];
		gridCostEstimation.Rows.Count = dtProjectCodeMapping.Rows.Count + 1;
		Image uncheckImage = imageList.Images[0];
		Image checkImage = imageList.Images[1];
		DataView dvSourceCostQuoteProject = null;
		if (budgetType == BudgetType.Types.CostQuotationMerged)
		{
			SourceCostQuoteProject sourceCostQuoteProject = new SourceCostQuoteProject();
			DataSet dsSourceCostQuoteProject = sourceCostQuoteProject.GetSourceCostQuoteProjectByParentCode(projectCode);
			dvSourceCostQuoteProject = new DataView(dsSourceCostQuoteProject.Tables[0]);
		}
		for (int rowIndex = 0; rowIndex < dtProjectCodeMapping.Rows.Count; rowIndex++)
		{
			Row gridRow = gridCostEstimation.Rows[rowIndex + 1];
			DataRow drProjectCodeMapping = dtProjectCodeMapping.Rows[rowIndex];
			gridRow["Version"] = drProjectCodeMapping["Version"];
			gridRow["ApplyDate"] = drProjectCodeMapping["ApplyDate"];
			gridRow["Purpose"] = drProjectCodeMapping["Purpose"];
			gridRow["Description"] = drProjectCodeMapping["Description"];
			gridRow["Content"] = drProjectCodeMapping["Content"];
			gridRow["Accountability"] = drProjectCodeMapping["Accountability"];
			gridRow["Reason"] = drProjectCodeMapping["Reason"];
			if (budgetType == BudgetType.Types.CostEstimation)
			{
				string CostEstProjectCode = (string)(gridRow["CostEstProjectCode"] = ArchConvert.Obj2String(drProjectCodeMapping["CostEstProjectCode"]));
				gridCostEstimation.SetCellImage(rowIndex + 1, gridCostEstimation.Cols["costEst"].Index, (CostEstProjectCode != "") ? checkImage : uncheckImage);
				gridRow["CostEstApproved"] = ArchConvert.Obj2Bool(drProjectCodeMapping["CostEstApproved"]);
			}
			else
			{
				if (budgetType != BudgetType.Types.CostQuotationMerged)
				{
					continue;
				}
				string CostQuoteMergedProjectCode = (string)(gridRow["CostQuoteMergedProjectCode"] = drProjectCodeMapping["CostQuoteMergedProjectCode"].ToString());
				gridCostEstimation.SetCellImage(rowIndex + 1, gridCostEstimation.Cols["CostQuoteMerged"].Index, (CostQuoteMergedProjectCode != "") ? checkImage : uncheckImage);
				gridRow["CostQuoteMergedApproved"] = ArchConvert.Obj2Bool(drProjectCodeMapping["CostQuoteMergedApproved"]);
				if (dvSourceCostQuoteProject != null)
				{
					dvSourceCostQuoteProject.RowFilter = $"ProjectCode='{CostQuoteMergedProjectCode}'";
					string InVersion = "";
					for (int j = 0; j < dvSourceCostQuoteProject.Count; j++)
					{
						InVersion = ((InVersion.Length <= 0) ? dvSourceCostQuoteProject[j]["CostQuoteVersion"].ToString() : (InVersion + "," + dvSourceCostQuoteProject[j]["CostQuoteVersion"].ToString()));
					}
					gridRow["CostEstVersion"] = InVersion;
				}
			}
		}
		if (dvSourceCostQuoteProject != null)
		{
			dvSourceCostQuoteProject = null;
		}
	}

	private void btnApply_Click(object sender, EventArgs e)
	{
		FormBudgetChangeInfo formBudgetChangeInfo = new FormBudgetChangeInfo();
		formBudgetChangeInfo._projectCode = projectCode;
		formBudgetChangeInfo._userID = userID;
		formBudgetChangeInfo._version = gridCostEstimation.Rows.Count;
		formBudgetChangeInfo.ChangeManagement = true;
		formBudgetChangeInfo._budgetType = budgetType;
		formBudgetChangeInfo._openMode = FormBudgetChangeInfo.Mode.New;
		if (formBudgetChangeInfo.ShowDialog() == DialogResult.OK)
		{
			if (budgetType == BudgetType.Types.CostQuotationMerged)
			{
				FormBudgetCombineQuote formBudgetCombineQuote = new FormBudgetCombineQuote(userID, projectCode, formBudgetChangeInfo._TargetProjectCode);
				if (formBudgetCombineQuote.ShowDialog() == DialogResult.Cancel)
				{
					DeleteUnfinishedProject(formBudgetChangeInfo._TargetProjectCode);
				}
				formBudgetCombineQuote.Dispose();
				formBudgetCombineQuote = null;
			}
		}
		else
		{
			DeleteUnfinishedProject(formBudgetChangeInfo._TargetProjectCode);
		}
		LoadData2Drid();
		formBudgetChangeInfo.Dispose();
		formBudgetChangeInfo = null;
	}

	private void DeleteUnfinishedProject(string projectCodeToDelete)
	{
		if (!string.IsNullOrEmpty(projectCodeToDelete))
		{
			BudProject budProject = new BudProject();
			ExecResult ER = budProject.RemoveProject(projectCodeToDelete);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show("刪除未完成的BudProject失敗：" + ER.Message);
			}
			PubProject pubProject = new PubProject();
			pubProject.DeletePubProject(projectCodeToDelete);
			BudProjectCodeMapping budProjectCodeMapping = new BudProjectCodeMapping();
			ER = budProjectCodeMapping.DeleteBudProjectCodeMapping(projectCodeToDelete);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show("刪除未完成的BudProjectCodeMapping失敗：" + ER.Message);
			}
		}
	}

	private void gridCostEstimation_MouseMove(object sender, MouseEventArgs e)
	{
		if (gridCostEstimation.MouseRow > 0 && gridCostEstimation.MouseCol > 0)
		{
			int colIndex = gridCostEstimation.MouseCol;
			string columnName = gridCostEstimation.Cols[colIndex].Name;
			if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right && (columnName == "CostEst" || columnName == "CostQuoteMerged"))
			{
				Cursor = Cursors.Hand;
			}
		}
	}

	private void gridCostEstimation_Click(object sender, EventArgs e)
	{
		if (gridCostEstimation.MouseRow <= 0 || gridCostEstimation.MouseCol <= 0)
		{
			return;
		}
		int rowIndex = gridCostEstimation.MouseRow;
		int colIndex = gridCostEstimation.MouseCol;
		string columnName = gridCostEstimation.Cols[colIndex].Name;
		Row gridRow = gridCostEstimation.Rows[rowIndex];
		if (!(columnName == "CostEst") && !(columnName == "CostQuoteMerged"))
		{
			return;
		}
		if (columnName == "CostQuoteMerged")
		{
			if (gridRow["CostEstVersion"] == null || gridRow["CostEstVersion"].ToString() == "")
			{
				if (MessageBox.Show("此筆資料並不完整，系統無法開啟，是否要刪除 ?", "警告", MessageBoxButtons.OKCancel) == DialogResult.OK)
				{
					string CostQuoteMergedProjectCode = gridRow["CostQuoteMergedProjectCode"].ToString();
					DeleteUnfinishedProject(CostQuoteMergedProjectCode);
					LoadData2Drid();
				}
			}
			else
			{
				targetProjectCode = gridRow[columnName + "ProjectCode"].ToString();
				base.DialogResult = DialogResult.OK;
			}
		}
		else
		{
			targetProjectCode = gridRow[columnName + "ProjectCode"].ToString();
			base.DialogResult = DialogResult.OK;
		}
	}

	private void btnPrintBudgetCostEstimationRecord_Click(object sender, EventArgs e)
	{
		if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			BudgetCostEstimationRecordReport reporter = new BudgetCostEstimationRecordReport();
			ExecResult ER = reporter.ProduceReport(saveFileDialog.FileName, projectCode);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show(ER.Message);
			}
			else
			{
				MessageBox.Show("產出工地預算變更記錄表成功！");
			}
		}
	}

	private void btnComsApplyDetail_Click(object sender, EventArgs e)
	{
		ExecResult ER = new ExecResult();
		FormComsApplyDetailList theFormComsApplyDetailList = new FormComsApplyDetailList();
		ChangeManagementServiceHelper theChangeManagementServiceHelper = new ChangeManagementServiceHelper();
		DataSet ComsApplyDetailList = theChangeManagementServiceHelper.GetBudChgApplyList(projectCode, out ER);
		BudProjectDBHelper theBudProjectDBHelper = new BudProjectDBHelper();
		DataSet dsbudproject = theBudProjectDBHelper.GetBudProject(projectCode);
		string projectNameC = ArchConvert.Obj2String(dsbudproject.Tables[0].Rows[0]["projectNameC"]);
		theFormComsApplyDetailList._ComsApplyDetailList = ComsApplyDetailList;
		theFormComsApplyDetailList._projectcode = projectCode;
		theFormComsApplyDetailList._projectName = projectNameC;
		string[] BCA_UIDs = null;
		if (theFormComsApplyDetailList.ShowDialog() == DialogResult.OK)
		{
			BCA_UIDs = theFormComsApplyDetailList._BCA_UID;
			theFormComsApplyDetailList.Dispose();
			theFormComsApplyDetailList = null;
			CostEstimation theCostEstimation = new CostEstimation(projectCode, userID);
			DataSet ds1 = new DataSet();
			ds1 = ComsApplyDetailList.Clone();
			for (int i = 0; i < ComsApplyDetailList.Tables[0].Rows.Count; i++)
			{
				bool select = false;
				string[] array = BCA_UIDs;
				foreach (string s in array)
				{
					if (ComsApplyDetailList.Tables[0].Rows[i]["BCA_UID"].ToString().Trim() == s.Trim())
					{
						select = true;
						break;
					}
				}
				if (select)
				{
					DataRow row = ds1.Tables[0].NewRow();
					for (int k = 0; k < ds1.Tables[0].Columns.Count; k++)
					{
						row[k] = ComsApplyDetailList.Tables[0].Rows[i][k];
					}
					ds1.Tables[0].Rows.Add(row);
				}
			}
			ER = theCostEstimation.CreateCostEstimation(ds1, out var _);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show("轉入預估成本失敗:" + ER.Message);
			}
		}
		DataSet dsProjectCodeMapping = GetData();
		Data2Grid(dsProjectCodeMapping);
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
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.BudgetChange.FormCostEstProjectList));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		this.lbTitle = new Infragistics.Win.Misc.UltraLabel();
		this.imageList = new System.Windows.Forms.ImageList(this.components);
		this.btnCancel = new Infragistics.Win.Misc.UltraButton();
		this.panelForm = new System.Windows.Forms.Panel();
		this.btnComsApplyDetail = new Infragistics.Win.Misc.UltraButton();
		this.btnPrintBudgetCostEstimationRecord = new Infragistics.Win.Misc.UltraButton();
		this.btnApply = new Infragistics.Win.Misc.UltraButton();
		this.panelGrid = new System.Windows.Forms.Panel();
		this.gridCostEstimation = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
		this.panelForm.SuspendLayout();
		this.panelGrid.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridCostEstimation).BeginInit();
		base.SuspendLayout();
		appearance1.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance1.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance1.FontData.Name = "新細明體";
		appearance1.FontData.SizeInPoints = 12f;
		appearance1.ForeColor = System.Drawing.Color.White;
		appearance1.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbTitle.Appearance = appearance1;
		this.lbTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.lbTitle.Font = new System.Drawing.Font("細明體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbTitle.Location = new System.Drawing.Point(0, 0);
		this.lbTitle.Name = "lbTitle";
		this.lbTitle.Size = new System.Drawing.Size(798, 48);
		this.lbTitle.TabIndex = 0;
		this.lbTitle.Text = "  變更管理 - 預估成本列表";
		this.imageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList.ImageStream");
		this.imageList.TransparentColor = System.Drawing.Color.White;
		this.imageList.Images.SetKeyName(0, "btn_budBlank.bmp");
		this.imageList.Images.SetKeyName(1, "btn_budCheck.bmp");
		this.imageList.Images.SetKeyName(2, "Execute2.bmp");
		this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.ImageHAlign = Infragistics.Win.HAlign.Left;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnCancel.Appearance = appearance2;
		this.btnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Font = new System.Drawing.Font("細明體", 11f);
		this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btnCancel.Location = new System.Drawing.Point(699, 412);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.ShowFocusRect = false;
		this.btnCancel.ShowOutline = false;
		this.btnCancel.Size = new System.Drawing.Size(88, 31);
		this.btnCancel.SupportThemes = false;
		this.btnCancel.TabIndex = 9;
		this.btnCancel.Text = "取消";
		this.panelForm.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panelForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panelForm.Controls.Add(this.btnComsApplyDetail);
		this.panelForm.Controls.Add(this.btnPrintBudgetCostEstimationRecord);
		this.panelForm.Controls.Add(this.btnApply);
		this.panelForm.Controls.Add(this.btnCancel);
		this.panelForm.Controls.Add(this.panelGrid);
		this.panelForm.Controls.Add(this.lbTitle);
		this.panelForm.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panelForm.Location = new System.Drawing.Point(0, 0);
		this.panelForm.Name = "panelForm";
		this.panelForm.Size = new System.Drawing.Size(800, 448);
		this.panelForm.TabIndex = 5;
		this.btnComsApplyDetail.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		appearance3.ImageHAlign = Infragistics.Win.HAlign.Left;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnComsApplyDetail.Appearance = appearance3;
		this.btnComsApplyDetail.BackColor = System.Drawing.SystemColors.Control;
		this.btnComsApplyDetail.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnComsApplyDetail.Font = new System.Drawing.Font("細明體", 11f);
		this.btnComsApplyDetail.ImageSize = new System.Drawing.Size(20, 20);
		this.btnComsApplyDetail.ImageTransparentColor = System.Drawing.Color.White;
		this.btnComsApplyDetail.Location = new System.Drawing.Point(172, 412);
		this.btnComsApplyDetail.Name = "btnComsApplyDetail";
		this.btnComsApplyDetail.ShowFocusRect = false;
		this.btnComsApplyDetail.ShowOutline = false;
		this.btnComsApplyDetail.Size = new System.Drawing.Size(217, 31);
		this.btnComsApplyDetail.SupportThemes = false;
		this.btnComsApplyDetail.TabIndex = 30;
		this.btnComsApplyDetail.Text = "由 COMS 匯入已核可變更申請";
		this.btnComsApplyDetail.Click += new System.EventHandler(btnComsApplyDetail_Click);
		this.btnPrintBudgetCostEstimationRecord.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnPrintBudgetCostEstimationRecord.Appearance = appearance4;
		this.btnPrintBudgetCostEstimationRecord.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnPrintBudgetCostEstimationRecord.Font = new System.Drawing.Font("細明體", 11f);
		this.btnPrintBudgetCostEstimationRecord.ImageSize = new System.Drawing.Size(20, 20);
		this.btnPrintBudgetCostEstimationRecord.ImageTransparentColor = System.Drawing.Color.White;
		this.btnPrintBudgetCostEstimationRecord.Location = new System.Drawing.Point(11, 412);
		this.btnPrintBudgetCostEstimationRecord.Name = "btnPrintBudgetCostEstimationRecord";
		this.btnPrintBudgetCostEstimationRecord.ShowFocusRect = false;
		this.btnPrintBudgetCostEstimationRecord.ShowOutline = false;
		this.btnPrintBudgetCostEstimationRecord.Size = new System.Drawing.Size(155, 31);
		this.btnPrintBudgetCostEstimationRecord.SupportThemes = false;
		this.btnPrintBudgetCostEstimationRecord.TabIndex = 29;
		this.btnPrintBudgetCostEstimationRecord.Text = "工地預算變更紀錄表";
		this.btnPrintBudgetCostEstimationRecord.Visible = false;
		this.btnPrintBudgetCostEstimationRecord.Click += new System.EventHandler(btnPrintBudgetCostEstimationRecord_Click);
		this.btnApply.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		appearance5.Image = resources.GetObject("appearance5.Image");
		appearance5.ImageHAlign = Infragistics.Win.HAlign.Left;
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnApply.Appearance = appearance5;
		this.btnApply.BackColor = System.Drawing.SystemColors.Control;
		this.btnApply.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnApply.Font = new System.Drawing.Font("細明體", 11f);
		this.btnApply.ImageSize = new System.Drawing.Size(20, 20);
		this.btnApply.ImageTransparentColor = System.Drawing.Color.White;
		this.btnApply.Location = new System.Drawing.Point(605, 412);
		this.btnApply.Name = "btnApply";
		this.btnApply.ShowFocusRect = false;
		this.btnApply.ShowOutline = false;
		this.btnApply.Size = new System.Drawing.Size(88, 31);
		this.btnApply.SupportThemes = false;
		this.btnApply.TabIndex = 28;
		this.btnApply.Text = "新增";
		this.btnApply.Click += new System.EventHandler(btnApply_Click);
		this.panelGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panelGrid.Controls.Add(this.gridCostEstimation);
		this.panelGrid.Location = new System.Drawing.Point(11, 66);
		this.panelGrid.Name = "panelGrid";
		this.panelGrid.Size = new System.Drawing.Size(776, 342);
		this.panelGrid.TabIndex = 8;
		this.gridCostEstimation._ExcelFileName = "";
		this.gridCostEstimation._ExcelSheeName = "";
		this.gridCostEstimation._IsOpenExcelAfterExport = false;
		this.gridCostEstimation.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridCostEstimation.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridCostEstimation.ColumnInfo = resources.GetString("gridCostEstimation.ColumnInfo");
		this.gridCostEstimation.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridCostEstimation.ExtendLastCol = true;
		this.gridCostEstimation.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridCostEstimation.ForeColor = System.Drawing.Color.Black;
		this.gridCostEstimation.Location = new System.Drawing.Point(0, 0);
		this.gridCostEstimation.Name = "gridCostEstimation";
		this.gridCostEstimation.Rows.Count = 1;
		this.gridCostEstimation.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridCostEstimation.ShowCursor = true;
		this.gridCostEstimation.ShowSort = false;
		this.gridCostEstimation.ShowToolTipOnNarrowColumn = true;
		this.gridCostEstimation.Size = new System.Drawing.Size(774, 340);
		this.gridCostEstimation.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridCostEstimation.Styles"));
		this.gridCostEstimation.TabIndex = 2;
		this.gridCostEstimation.Tree.Column = 1;
		this.gridCostEstimation.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridCostEstimation.Click += new System.EventHandler(gridCostEstimation_Click);
		this.gridCostEstimation.MouseMove += new System.Windows.Forms.MouseEventHandler(gridCostEstimation_MouseMove);
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Top.BackColor = System.Drawing.SystemColors.Control;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Top.Name = "_FormBudgetProjectPick_Toolbars_Dock_Area_Top";
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(800, 0);
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.SystemColors.Control;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 448);
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Bottom.Name = "_FormBudgetProjectPick_Toolbars_Dock_Area_Bottom";
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(800, 0);
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Left.BackColor = System.Drawing.SystemColors.Control;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 0);
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Left.Name = "_FormBudgetProjectPick_Toolbars_Dock_Area_Left";
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 448);
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Right.BackColor = System.Drawing.SystemColors.Control;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(800, 0);
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Right.Name = "_FormBudgetProjectPick_Toolbars_Dock_Area_Right";
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 448);
		this.saveFileDialog.FileName = "工地預算變更紀錄表";
		this.saveFileDialog.Filter = "Excel 活頁簿|*.xls";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(800, 448);
		base.Controls.Add(this.panelForm);
		base.Controls.Add(this._FormBudgetProjectPick_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormBudgetProjectPick_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormBudgetProjectPick_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormBudgetProjectPick_Toolbars_Dock_Area_Bottom);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "FormCostEstProjectList";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "預估成本預算書";
		base.Load += new System.EventHandler(FormCostEstProjectList_Load);
		this.panelForm.ResumeLayout(false);
		this.panelGrid.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridCostEstimation).EndInit();
		base.ResumeLayout(false);
	}
}
