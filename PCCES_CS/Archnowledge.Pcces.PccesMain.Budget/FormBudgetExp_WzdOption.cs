using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.Bid;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.DomainModule.LogicalBase;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinSchedule;
using Infragistics.Win.UltraWinSchedule.CalendarCombo;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetExp_WzdOption : Form
{
	private Panel panel4;

	private UltraButton btnFinish;

	private UltraButton btnCancel;

	private Panel panel1;

	private GroupBox gbSummry;

	private GroupBox gbDetail;

	private GroupBox gbCostBreakdownList;

	private Panel Pnl_PntLevel;

	private NumericUpDown ddlSummaryPrintLevel;

	private UltraLabel lbSummaryPrintLevel;

	private Panel Pnl_Memo;

	private UltraCheckEditor chkDetailListShowRemark;

	private UltraLabel ultraLabel1;

	private UltraCheckEditor chkDetailListShowPccesCode;

	private UltraCheckEditor chkDetailListShowAnalysisItemSymbol;

	private UltraCheckEditor chkDetailListShowUnofficialItemCode;

	private Panel panel2;

	private UltraLabel ultraLabel2;

	private Panel Pnl_Sort;

	private UltraOptionSet opSortOption;

	private UltraLabel ultraLabel3;

	private UltraOptionSet opDuplicationOption;

	private UltraLabel ultraLabel4;

	private UltraCheckEditor chkBreakdownListShowRemark;

	private UltraCheckEditor chkBreakdownListShowPccesCode;

	private UltraCheckEditor chkBreakdownListShowAnalysisItemSymbol;

	private UltraCheckEditor chkBreakdownListShowUnofficialItemCode;

	private UltraCheckEditor chkHalfPageFormat;

	private Panel panelSheetVisibilityOption;

	private UltraCheckEditor chkOutputSummary;

	private UltraCheckEditor chkOutputDetailList;

	private UltraCheckEditor chkOutputBreakdownList;

	private UltraCheckEditor chkOutputResourceList;

	private Container components = null;

	private UltraCheckEditor chkSummaryIncludeWorkItem;

	private UltraCheckEditor chkDuplicateAnalysisItemInDetailList;

	private UltraCheckEditor chkSkipCommentItem;

	private UltraCheckEditor chkSkipSubTotalItem;

	private GroupBox groupBox2;

	private UltraCheckEditor chkDetailListDisplayMainItemDetail;

	private UltraComboEditor ddlDetailListAnalysisItemMark;

	private UltraComboEditor ddlBreakdownListAnalysisItemMark;

	private UltraOptionSet opSortItems;

	private GroupBox gbOption;

	private UltraCheckEditor chkBidPrintSummary;

	private UltraCheckEditor chkBudgetPrintPrice;

	private UltraCheckEditor chkWithEnglish;

	private UltraCheckEditor chkPrintDate;

	private UltraCalendarCombo cldPrintDate;

	private UltraCheckEditor chkBidFooterPrintBidder;

	private UltraButton btnPageBreakSetup;

	private UltraComboEditor ddlExcelFont;

	private UltraLabel ultraLabel17;

	private UltraLabel ultraLabel16;

	private UltraCheckEditor chkShrinkToFit;

	private UltraCheckEditor chkNoBorderInLineBreak;

	private UltraLabel lbRequestBidPrintQty;

	private Panel panelRequestPrintQty;

	private UltraCheckEditor chkPrintMiscellaneaQty;

	private UltraCheckEditor chkPrintLaborQty;

	private UltraCheckEditor chkPrintEquipmentQty;

	private UltraCheckEditor chkPrintMaterialQty;

	private UltraCheckEditor chkDetailListShowAnalysisItemCode;

	private UltraCheckEditor chkBidFooterPrintVendorInfo;

	private UltraCheckEditor chkEnableOldExportExcel;

	private UltraCheckEditor chkDetailListPrintProjectDescription;

	private UltraCheckEditor chkTakePlaceByMaxValue;

	private UltraCheckEditor chkShowCodeCorrectRate;

	private UltraCheckEditor chkSummaryPrintProjectDescription;

	private string projectCode;

	private string userID;

	private PccesFormAction FormActionName;

	private string iniFilePath = AppDomain.CurrentDomain.BaseDirectory;

	private Archnowledge.Pcces.DomainModule.LogicalBase.Project project = null;

	private DataSet dsProject;

	private string printMode;

	private bool outputBudget;

	private bool isSubmit = false;

	private bool isPreview = false;

	public string _ProjectCode
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

	public bool _OutputBudget
	{
		get
		{
			return outputBudget;
		}
		set
		{
			outputBudget = value;
		}
	}

	public bool _IsSubmit
	{
		get
		{
			return isSubmit;
		}
		set
		{
			isSubmit = value;
		}
	}

	public bool _IsPreview
	{
		set
		{
			isPreview = value;
		}
	}

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetExp_WzdOption));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton1 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem6 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem7 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem8 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem9 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem10 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		this.panel4 = new System.Windows.Forms.Panel();
		this.btnFinish = new Infragistics.Win.Misc.UltraButton();
		this.btnCancel = new Infragistics.Win.Misc.UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.gbOption = new System.Windows.Forms.GroupBox();
		this.chkEnableOldExportExcel = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkBidFooterPrintVendorInfo = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.ddlExcelFont = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ultraLabel17 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel16 = new Infragistics.Win.Misc.UltraLabel();
		this.chkShrinkToFit = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkNoBorderInLineBreak = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkBidFooterPrintBidder = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkBidPrintSummary = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkBudgetPrintPrice = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkWithEnglish = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkPrintDate = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.cldPrintDate = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.gbCostBreakdownList = new System.Windows.Forms.GroupBox();
		this.chkTakePlaceByMaxValue = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.Pnl_Sort = new System.Windows.Forms.Panel();
		this.opSortItems = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.chkSkipCommentItem = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkSkipSubTotalItem = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkDuplicateAnalysisItemInDetailList = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.opSortOption = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.opDuplicationOption = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.chkHalfPageFormat = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panelRequestPrintQty = new System.Windows.Forms.Panel();
		this.lbRequestBidPrintQty = new Infragistics.Win.Misc.UltraLabel();
		this.chkPrintMiscellaneaQty = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkPrintLaborQty = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkPrintEquipmentQty = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkPrintMaterialQty = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.ddlBreakdownListAnalysisItemMark = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.chkBreakdownListShowRemark = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.chkBreakdownListShowPccesCode = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkBreakdownListShowAnalysisItemSymbol = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkBreakdownListShowUnofficialItemCode = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.gbDetail = new System.Windows.Forms.GroupBox();
		this.Pnl_Memo = new System.Windows.Forms.Panel();
		this.chkShowCodeCorrectRate = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkDetailListPrintProjectDescription = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkDetailListShowAnalysisItemCode = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.ddlDetailListAnalysisItemMark = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.chkDetailListDisplayMainItemDetail = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.btnPageBreakSetup = new Infragistics.Win.Misc.UltraButton();
		this.chkDetailListShowRemark = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.chkDetailListShowPccesCode = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkDetailListShowAnalysisItemSymbol = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkDetailListShowUnofficialItemCode = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.gbSummry = new System.Windows.Forms.GroupBox();
		this.chkSummaryIncludeWorkItem = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.panelSheetVisibilityOption = new System.Windows.Forms.Panel();
		this.chkOutputResourceList = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkOutputBreakdownList = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkOutputDetailList = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkOutputSummary = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.Pnl_PntLevel = new System.Windows.Forms.Panel();
		this.ddlSummaryPrintLevel = new System.Windows.Forms.NumericUpDown();
		this.lbSummaryPrintLevel = new Infragistics.Win.Misc.UltraLabel();
		this.chkSummaryPrintProjectDescription = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.panel4.SuspendLayout();
		this.panel1.SuspendLayout();
		this.gbOption.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ddlExcelFont).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cldPrintDate).BeginInit();
		this.gbCostBreakdownList.SuspendLayout();
		this.Pnl_Sort.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.opSortItems).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.opSortOption).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.opDuplicationOption).BeginInit();
		this.panel2.SuspendLayout();
		this.panelRequestPrintQty.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ddlBreakdownListAnalysisItemMark).BeginInit();
		this.gbDetail.SuspendLayout();
		this.Pnl_Memo.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ddlDetailListAnalysisItemMark).BeginInit();
		this.gbSummry.SuspendLayout();
		this.panelSheetVisibilityOption.SuspendLayout();
		this.Pnl_PntLevel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ddlSummaryPrintLevel).BeginInit();
		base.SuspendLayout();
		this.panel4.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel4.Controls.Add(this.btnFinish);
		this.panel4.Controls.Add(this.btnCancel);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(0, 666);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(666, 44);
		this.panel4.TabIndex = 11;
		this.btnFinish.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnFinish.Appearance = appearance1;
		this.btnFinish.BackColor = System.Drawing.SystemColors.Control;
		this.btnFinish.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnFinish.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnFinish.Font = new System.Drawing.Font("細明體", 11f);
		this.btnFinish.ImageSize = new System.Drawing.Size(20, 20);
		this.btnFinish.ImageTransparentColor = System.Drawing.Color.White;
		this.btnFinish.Location = new System.Drawing.Point(474, 7);
		this.btnFinish.Name = "btnFinish";
		this.btnFinish.ShowFocusRect = false;
		this.btnFinish.ShowOutline = false;
		this.btnFinish.Size = new System.Drawing.Size(88, 31);
		this.btnFinish.SupportThemes = false;
		this.btnFinish.TabIndex = 6;
		this.btnFinish.Text = "完成";
		this.btnFinish.Click += new System.EventHandler(btnFinish_Click);
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
		this.btnCancel.Location = new System.Drawing.Point(569, 7);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.ShowFocusRect = false;
		this.btnCancel.ShowOutline = false;
		this.btnCancel.Size = new System.Drawing.Size(88, 31);
		this.btnCancel.SupportThemes = false;
		this.btnCancel.TabIndex = 5;
		this.btnCancel.Text = "取消";
		this.panel1.AutoScroll = true;
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.gbOption);
		this.panel1.Controls.Add(this.gbCostBreakdownList);
		this.panel1.Controls.Add(this.gbDetail);
		this.panel1.Controls.Add(this.gbSummry);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(666, 666);
		this.panel1.TabIndex = 12;
		this.gbOption.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gbOption.Controls.Add(this.chkEnableOldExportExcel);
		this.gbOption.Controls.Add(this.chkBidFooterPrintVendorInfo);
		this.gbOption.Controls.Add(this.ddlExcelFont);
		this.gbOption.Controls.Add(this.ultraLabel17);
		this.gbOption.Controls.Add(this.ultraLabel16);
		this.gbOption.Controls.Add(this.chkShrinkToFit);
		this.gbOption.Controls.Add(this.chkNoBorderInLineBreak);
		this.gbOption.Controls.Add(this.chkBidFooterPrintBidder);
		this.gbOption.Controls.Add(this.chkBidPrintSummary);
		this.gbOption.Controls.Add(this.chkBudgetPrintPrice);
		this.gbOption.Controls.Add(this.chkWithEnglish);
		this.gbOption.Controls.Add(this.chkPrintDate);
		this.gbOption.Controls.Add(this.cldPrintDate);
		this.gbOption.Location = new System.Drawing.Point(9, 6);
		this.gbOption.Name = "gbOption";
		this.gbOption.Size = new System.Drawing.Size(648, 201);
		this.gbOption.TabIndex = 43;
		this.gbOption.TabStop = false;
		this.gbOption.Text = "Excel 格式設定";
		this.chkEnableOldExportExcel.Enabled = false;
		this.chkEnableOldExportExcel.Location = new System.Drawing.Point(285, 23);
		this.chkEnableOldExportExcel.Name = "chkEnableOldExportExcel";
		this.chkEnableOldExportExcel.Size = new System.Drawing.Size(192, 20);
		this.chkEnableOldExportExcel.TabIndex = 59;
		this.chkEnableOldExportExcel.Text = "此次輸出使用 4.2 版方法";
		this.chkBidFooterPrintVendorInfo.Location = new System.Drawing.Point(12, 175);
		this.chkBidFooterPrintVendorInfo.Name = "chkBidFooterPrintVendorInfo";
		this.chkBidFooterPrintVendorInfo.Size = new System.Drawing.Size(241, 20);
		this.chkBidFooterPrintVendorInfo.TabIndex = 58;
		this.chkBidFooterPrintVendorInfo.Text = "標單表尾列印投標廠商資料";
		this.ddlExcelFont.AutoSize = true;
		this.ddlExcelFont.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		valueListItem1.DataValue = "1";
		valueListItem1.DisplayText = "細明體";
		valueListItem2.DataValue = "2";
		valueListItem2.DisplayText = "標楷體";
		this.ddlExcelFont.Items.Add(valueListItem1);
		this.ddlExcelFont.Items.Add(valueListItem2);
		this.ddlExcelFont.Location = new System.Drawing.Point(286, 156);
		this.ddlExcelFont.Name = "ddlExcelFont";
		this.ddlExcelFont.Size = new System.Drawing.Size(305, 24);
		this.ddlExcelFont.TabIndex = 57;
		this.ddlExcelFont.Text = null;
		this.ultraLabel17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ultraLabel17.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel17.Location = new System.Drawing.Point(286, 134);
		this.ultraLabel17.Name = "ultraLabel17";
		this.ultraLabel17.Size = new System.Drawing.Size(315, 16);
		this.ultraLabel17.TabIndex = 56;
		this.ultraLabel17.Text = "(建議字型只使用細明體或標楷體)";
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel16.Appearance = appearance3;
		this.ultraLabel16.Location = new System.Drawing.Point(286, 105);
		this.ultraLabel16.Name = "ultraLabel16";
		this.ultraLabel16.Size = new System.Drawing.Size(179, 23);
		this.ultraLabel16.TabIndex = 55;
		this.ultraLabel16.Text = "EXCEL 輸出使用字型：";
		this.chkShrinkToFit.Checked = true;
		this.chkShrinkToFit.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkShrinkToFit.Location = new System.Drawing.Point(285, 51);
		this.chkShrinkToFit.Name = "chkShrinkToFit";
		this.chkShrinkToFit.Size = new System.Drawing.Size(192, 20);
		this.chkShrinkToFit.TabIndex = 43;
		this.chkShrinkToFit.Text = "縮小字型以適合欄寬";
		this.chkNoBorderInLineBreak.Checked = true;
		this.chkNoBorderInLineBreak.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkNoBorderInLineBreak.Location = new System.Drawing.Point(285, 79);
		this.chkNoBorderInLineBreak.Name = "chkNoBorderInLineBreak";
		this.chkNoBorderInLineBreak.Size = new System.Drawing.Size(192, 20);
		this.chkNoBorderInLineBreak.TabIndex = 42;
		this.chkNoBorderInLineBreak.Text = "折行項目不畫中間線";
		this.chkBidFooterPrintBidder.Checked = true;
		this.chkBidFooterPrintBidder.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkBidFooterPrintBidder.Location = new System.Drawing.Point(12, 131);
		this.chkBidFooterPrintBidder.Name = "chkBidFooterPrintBidder";
		this.chkBidFooterPrintBidder.Size = new System.Drawing.Size(241, 38);
		this.chkBidFooterPrintBidder.TabIndex = 41;
		this.chkBidFooterPrintBidder.Text = "每張標單表尾要列印投標廠商和負責人(不勾選只列印最後一頁)";
		this.chkBidPrintSummary.Checked = true;
		this.chkBidPrintSummary.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkBidPrintSummary.Location = new System.Drawing.Point(12, 105);
		this.chkBidPrintSummary.Name = "chkBidPrintSummary";
		this.chkBidPrintSummary.Size = new System.Drawing.Size(120, 20);
		this.chkBidPrintSummary.TabIndex = 39;
		this.chkBidPrintSummary.Text = "標單列印總表";
		this.chkBudgetPrintPrice.Checked = true;
		this.chkBudgetPrintPrice.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkBudgetPrintPrice.Location = new System.Drawing.Point(12, 79);
		this.chkBudgetPrintPrice.Name = "chkBudgetPrintPrice";
		this.chkBudgetPrintPrice.Size = new System.Drawing.Size(137, 20);
		this.chkBudgetPrintPrice.TabIndex = 38;
		this.chkBudgetPrintPrice.Text = "預算書列印價格";
		this.chkWithEnglish.Location = new System.Drawing.Point(12, 23);
		this.chkWithEnglish.Name = "chkWithEnglish";
		this.chkWithEnglish.Size = new System.Drawing.Size(144, 20);
		this.chkWithEnglish.TabIndex = 16;
		this.chkWithEnglish.Text = "Excel 中英並列";
		this.chkPrintDate.Checked = true;
		this.chkPrintDate.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkPrintDate.Location = new System.Drawing.Point(12, 51);
		this.chkPrintDate.Name = "chkPrintDate";
		this.chkPrintDate.Size = new System.Drawing.Size(84, 20);
		this.chkPrintDate.TabIndex = 17;
		this.chkPrintDate.Text = "列印日期";
		this.chkPrintDate.CheckedChanged += new System.EventHandler(chkPrintDate_CheckedChanged);
		dateButton1.Caption = "今天";
		this.cldPrintDate.DateButtons.Add(dateButton1);
		this.cldPrintDate.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.cldPrintDate.Location = new System.Drawing.Point(99, 48);
		this.cldPrintDate.Name = "cldPrintDate";
		this.cldPrintDate.NonAutoSizeHeight = 21;
		this.cldPrintDate.NullDateLabel = "";
		this.cldPrintDate.Size = new System.Drawing.Size(137, 21);
		this.cldPrintDate.TabIndex = 36;
		this.cldPrintDate.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.cldPrintDate.Value = resources.GetObject("cldPrintDate.Value");
		this.cldPrintDate.WeekNumbersVisible = true;
		this.gbCostBreakdownList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gbCostBreakdownList.Controls.Add(this.chkTakePlaceByMaxValue);
		this.gbCostBreakdownList.Controls.Add(this.Pnl_Sort);
		this.gbCostBreakdownList.Controls.Add(this.panel2);
		this.gbCostBreakdownList.Location = new System.Drawing.Point(9, 403);
		this.gbCostBreakdownList.Name = "gbCostBreakdownList";
		this.gbCostBreakdownList.Size = new System.Drawing.Size(648, 260);
		this.gbCostBreakdownList.TabIndex = 2;
		this.gbCostBreakdownList.TabStop = false;
		this.gbCostBreakdownList.Text = "單價分析表";
		this.chkTakePlaceByMaxValue.Checked = true;
		this.chkTakePlaceByMaxValue.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkTakePlaceByMaxValue.Location = new System.Drawing.Point(20, 234);
		this.chkTakePlaceByMaxValue.Name = "chkTakePlaceByMaxValue";
		this.chkTakePlaceByMaxValue.Size = new System.Drawing.Size(620, 20);
		this.chkTakePlaceByMaxValue.TabIndex = 16;
		this.chkTakePlaceByMaxValue.Text = "Excel 單價分析表中人工、機具、材料、雜項之分類單價加總調整為=工項單價";
		this.Pnl_Sort.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.Pnl_Sort.Controls.Add(this.opSortItems);
		this.Pnl_Sort.Controls.Add(this.groupBox2);
		this.Pnl_Sort.Controls.Add(this.chkDuplicateAnalysisItemInDetailList);
		this.Pnl_Sort.Controls.Add(this.opSortOption);
		this.Pnl_Sort.Controls.Add(this.ultraLabel3);
		this.Pnl_Sort.Controls.Add(this.opDuplicationOption);
		this.Pnl_Sort.Controls.Add(this.ultraLabel4);
		this.Pnl_Sort.Controls.Add(this.chkHalfPageFormat);
		this.Pnl_Sort.Location = new System.Drawing.Point(272, 17);
		this.Pnl_Sort.Name = "Pnl_Sort";
		this.Pnl_Sort.Size = new System.Drawing.Size(368, 214);
		this.Pnl_Sort.TabIndex = 14;
		this.opSortItems.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.opSortItems.CheckedIndex = 0;
		this.opSortItems.ItemAppearance = appearance4;
		valueListItem3.DataValue = "Default Item";
		valueListItem3.DisplayText = "項次依流水號編輯";
		valueListItem4.DataValue = "ValueListItem1";
		valueListItem4.DisplayText = "項次依原項次編輯";
		this.opSortItems.Items.Add(valueListItem3);
		this.opSortItems.Items.Add(valueListItem4);
		this.opSortItems.ItemSpacingVertical = 1;
		this.opSortItems.Location = new System.Drawing.Point(222, 5);
		this.opSortItems.Name = "opSortItems";
		this.opSortItems.Size = new System.Drawing.Size(141, 48);
		this.opSortItems.TabIndex = 21;
		this.opSortItems.Text = "項次依流水號編輯";
		this.opSortItems.Visible = false;
		this.groupBox2.Controls.Add(this.chkSkipCommentItem);
		this.groupBox2.Controls.Add(this.chkSkipSubTotalItem);
		this.groupBox2.Location = new System.Drawing.Point(222, 131);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(132, 80);
		this.groupBox2.TabIndex = 20;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "中英並列格式";
		this.chkSkipCommentItem.Location = new System.Drawing.Point(9, 24);
		this.chkSkipCommentItem.Name = "chkSkipCommentItem";
		this.chkSkipCommentItem.Size = new System.Drawing.Size(120, 20);
		this.chkSkipCommentItem.TabIndex = 18;
		this.chkSkipCommentItem.Text = "說明項不編號";
		this.chkSkipSubTotalItem.Location = new System.Drawing.Point(9, 48);
		this.chkSkipSubTotalItem.Name = "chkSkipSubTotalItem";
		this.chkSkipSubTotalItem.Size = new System.Drawing.Size(120, 20);
		this.chkSkipSubTotalItem.TabIndex = 19;
		this.chkSkipSubTotalItem.Text = "小計項不編號";
		this.chkDuplicateAnalysisItemInDetailList.Location = new System.Drawing.Point(9, 134);
		this.chkDuplicateAnalysisItemInDetailList.Name = "chkDuplicateAnalysisItemInDetailList";
		this.chkDuplicateAnalysisItemInDetailList.Size = new System.Drawing.Size(192, 44);
		this.chkDuplicateAnalysisItemInDetailList.TabIndex = 17;
		this.chkDuplicateAnalysisItemInDetailList.Text = "重複列印詳細表項目單價分析";
		this.opSortOption.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.opSortOption.CheckedIndex = 0;
		this.opSortOption.ItemAppearance = appearance5;
		valueListItem5.DataValue = "Default Item";
		valueListItem5.DisplayText = "依項次代碼排序";
		valueListItem6.DataValue = "ValueListItem1";
		valueListItem6.DisplayText = "依工項代碼排序";
		this.opSortOption.Items.Add(valueListItem5);
		this.opSortOption.Items.Add(valueListItem6);
		this.opSortOption.ItemSpacingVertical = 1;
		this.opSortOption.Location = new System.Drawing.Point(94, 5);
		this.opSortOption.Name = "opSortOption";
		this.opSortOption.Size = new System.Drawing.Size(132, 50);
		this.opSortOption.TabIndex = 5;
		this.opSortOption.Text = "依項次代碼排序";
		this.opSortOption.ValueChanged += new System.EventHandler(opSort_ValueChanged);
		appearance6.TextHAlign = Infragistics.Win.HAlign.Right;
		this.ultraLabel3.Appearance = appearance6;
		this.ultraLabel3.Location = new System.Drawing.Point(5, 9);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(83, 23);
		this.ultraLabel3.TabIndex = 7;
		this.ultraLabel3.Text = "排序方式：";
		this.opDuplicationOption.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.opDuplicationOption.CheckedIndex = 1;
		this.opDuplicationOption.ItemAppearance = appearance7;
		valueListItem7.DataValue = "Default Item";
		valueListItem7.DisplayText = "重複列印";
		valueListItem8.DataValue = "ValueListItem1";
		valueListItem8.DisplayText = "不重複列印";
		this.opDuplicationOption.Items.Add(valueListItem7);
		this.opDuplicationOption.Items.Add(valueListItem8);
		this.opDuplicationOption.ItemSpacingVertical = 1;
		this.opDuplicationOption.Location = new System.Drawing.Point(94, 69);
		this.opDuplicationOption.Name = "opDuplicationOption";
		this.opDuplicationOption.Size = new System.Drawing.Size(115, 45);
		this.opDuplicationOption.TabIndex = 6;
		this.opDuplicationOption.Text = "不重複列印";
		this.opDuplicationOption.ValueChanged += new System.EventHandler(opRepeat_ValueChanged);
		appearance8.TextHAlign = Infragistics.Win.HAlign.Right;
		this.ultraLabel4.Appearance = appearance8;
		this.ultraLabel4.Location = new System.Drawing.Point(5, 73);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(83, 18);
		this.ultraLabel4.TabIndex = 8;
		this.ultraLabel4.Text = "重複項目：";
		this.chkHalfPageFormat.Checked = true;
		this.chkHalfPageFormat.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkHalfPageFormat.Location = new System.Drawing.Point(9, 184);
		this.chkHalfPageFormat.Name = "chkHalfPageFormat";
		this.chkHalfPageFormat.Size = new System.Drawing.Size(192, 20);
		this.chkHalfPageFormat.TabIndex = 15;
		this.chkHalfPageFormat.Text = "中文使用半頁格式";
		this.panel2.Controls.Add(this.panelRequestPrintQty);
		this.panel2.Controls.Add(this.ddlBreakdownListAnalysisItemMark);
		this.panel2.Controls.Add(this.chkBreakdownListShowRemark);
		this.panel2.Controls.Add(this.ultraLabel2);
		this.panel2.Controls.Add(this.chkBreakdownListShowPccesCode);
		this.panel2.Controls.Add(this.chkBreakdownListShowAnalysisItemSymbol);
		this.panel2.Controls.Add(this.chkBreakdownListShowUnofficialItemCode);
		this.panel2.Location = new System.Drawing.Point(8, 17);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(249, 214);
		this.panel2.TabIndex = 13;
		this.panelRequestPrintQty.Controls.Add(this.lbRequestBidPrintQty);
		this.panelRequestPrintQty.Controls.Add(this.chkPrintMiscellaneaQty);
		this.panelRequestPrintQty.Controls.Add(this.chkPrintLaborQty);
		this.panelRequestPrintQty.Controls.Add(this.chkPrintEquipmentQty);
		this.panelRequestPrintQty.Controls.Add(this.chkPrintMaterialQty);
		this.panelRequestPrintQty.Location = new System.Drawing.Point(4, 124);
		this.panelRequestPrintQty.Name = "panelRequestPrintQty";
		this.panelRequestPrintQty.Size = new System.Drawing.Size(241, 91);
		this.panelRequestPrintQty.TabIndex = 59;
		this.lbRequestBidPrintQty.Location = new System.Drawing.Point(4, 12);
		this.lbRequestBidPrintQty.Name = "lbRequestBidPrintQty";
		this.lbRequestBidPrintQty.Size = new System.Drawing.Size(184, 23);
		this.lbRequestBidPrintQty.TabIndex = 54;
		this.lbRequestBidPrintQty.Text = "空白標單是否列印數量：";
		this.chkPrintMiscellaneaQty.Checked = true;
		this.chkPrintMiscellaneaQty.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkPrintMiscellaneaQty.Location = new System.Drawing.Point(104, 67);
		this.chkPrintMiscellaneaQty.Name = "chkPrintMiscellaneaQty";
		this.chkPrintMiscellaneaQty.Size = new System.Drawing.Size(71, 20);
		this.chkPrintMiscellaneaQty.TabIndex = 58;
		this.chkPrintMiscellaneaQty.Text = "雜項";
		this.chkPrintLaborQty.Checked = true;
		this.chkPrintLaborQty.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkPrintLaborQty.Location = new System.Drawing.Point(8, 41);
		this.chkPrintLaborQty.Name = "chkPrintLaborQty";
		this.chkPrintLaborQty.Size = new System.Drawing.Size(71, 20);
		this.chkPrintLaborQty.TabIndex = 55;
		this.chkPrintLaborQty.Text = "人工";
		this.chkPrintEquipmentQty.Checked = true;
		this.chkPrintEquipmentQty.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkPrintEquipmentQty.Location = new System.Drawing.Point(8, 67);
		this.chkPrintEquipmentQty.Name = "chkPrintEquipmentQty";
		this.chkPrintEquipmentQty.Size = new System.Drawing.Size(71, 20);
		this.chkPrintEquipmentQty.TabIndex = 57;
		this.chkPrintEquipmentQty.Text = "機具";
		this.chkPrintMaterialQty.Checked = true;
		this.chkPrintMaterialQty.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkPrintMaterialQty.Location = new System.Drawing.Point(104, 41);
		this.chkPrintMaterialQty.Name = "chkPrintMaterialQty";
		this.chkPrintMaterialQty.Size = new System.Drawing.Size(71, 20);
		this.chkPrintMaterialQty.TabIndex = 56;
		this.chkPrintMaterialQty.Text = "材料";
		this.ddlBreakdownListAnalysisItemMark.AutoSize = true;
		this.ddlBreakdownListAnalysisItemMark.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		valueListItem9.DataValue = "1";
		valueListItem9.DisplayText = "*";
		this.ddlBreakdownListAnalysisItemMark.Items.Add(valueListItem9);
		this.ddlBreakdownListAnalysisItemMark.Location = new System.Drawing.Point(136, 76);
		this.ddlBreakdownListAnalysisItemMark.Name = "ddlBreakdownListAnalysisItemMark";
		this.ddlBreakdownListAnalysisItemMark.Size = new System.Drawing.Size(92, 24);
		this.ddlBreakdownListAnalysisItemMark.TabIndex = 53;
		this.ddlBreakdownListAnalysisItemMark.Text = null;
		this.chkBreakdownListShowRemark.Checked = true;
		this.chkBreakdownListShowRemark.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkBreakdownListShowRemark.Location = new System.Drawing.Point(12, 32);
		this.chkBreakdownListShowRemark.Name = "chkBreakdownListShowRemark";
		this.chkBreakdownListShowRemark.Size = new System.Drawing.Size(120, 20);
		this.chkBreakdownListShowRemark.TabIndex = 0;
		this.chkBreakdownListShowRemark.Text = "備註";
		this.ultraLabel2.Location = new System.Drawing.Point(8, 8);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(75, 23);
		this.ultraLabel2.TabIndex = 1;
		this.ultraLabel2.Text = "備註欄：";
		this.chkBreakdownListShowPccesCode.Checked = true;
		this.chkBreakdownListShowPccesCode.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkBreakdownListShowPccesCode.Location = new System.Drawing.Point(12, 56);
		this.chkBreakdownListShowPccesCode.Name = "chkBreakdownListShowPccesCode";
		this.chkBreakdownListShowPccesCode.Size = new System.Drawing.Size(120, 20);
		this.chkBreakdownListShowPccesCode.TabIndex = 2;
		this.chkBreakdownListShowPccesCode.Text = "工項代碼";
		this.chkBreakdownListShowAnalysisItemSymbol.Checked = true;
		this.chkBreakdownListShowAnalysisItemSymbol.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkBreakdownListShowAnalysisItemSymbol.Location = new System.Drawing.Point(12, 80);
		this.chkBreakdownListShowAnalysisItemSymbol.Name = "chkBreakdownListShowAnalysisItemSymbol";
		this.chkBreakdownListShowAnalysisItemSymbol.Size = new System.Drawing.Size(120, 20);
		this.chkBreakdownListShowAnalysisItemSymbol.TabIndex = 3;
		this.chkBreakdownListShowAnalysisItemSymbol.Text = "單價分析標記";
		this.chkBreakdownListShowUnofficialItemCode.Location = new System.Drawing.Point(12, 104);
		this.chkBreakdownListShowUnofficialItemCode.Name = "chkBreakdownListShowUnofficialItemCode";
		this.chkBreakdownListShowUnofficialItemCode.Size = new System.Drawing.Size(120, 20);
		this.chkBreakdownListShowUnofficialItemCode.TabIndex = 4;
		this.chkBreakdownListShowUnofficialItemCode.Text = "外碼";
		this.gbDetail.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gbDetail.Controls.Add(this.Pnl_Memo);
		this.gbDetail.Location = new System.Drawing.Point(272, 213);
		this.gbDetail.Name = "gbDetail";
		this.gbDetail.Size = new System.Drawing.Size(385, 184);
		this.gbDetail.TabIndex = 1;
		this.gbDetail.TabStop = false;
		this.gbDetail.Text = "詳細表";
		this.Pnl_Memo.Controls.Add(this.chkShowCodeCorrectRate);
		this.Pnl_Memo.Controls.Add(this.chkDetailListPrintProjectDescription);
		this.Pnl_Memo.Controls.Add(this.chkDetailListShowAnalysisItemCode);
		this.Pnl_Memo.Controls.Add(this.ddlDetailListAnalysisItemMark);
		this.Pnl_Memo.Controls.Add(this.chkDetailListDisplayMainItemDetail);
		this.Pnl_Memo.Controls.Add(this.btnPageBreakSetup);
		this.Pnl_Memo.Controls.Add(this.chkDetailListShowRemark);
		this.Pnl_Memo.Controls.Add(this.ultraLabel1);
		this.Pnl_Memo.Controls.Add(this.chkDetailListShowPccesCode);
		this.Pnl_Memo.Controls.Add(this.chkDetailListShowAnalysisItemSymbol);
		this.Pnl_Memo.Controls.Add(this.chkDetailListShowUnofficialItemCode);
		this.Pnl_Memo.Location = new System.Drawing.Point(9, 16);
		this.Pnl_Memo.Name = "Pnl_Memo";
		this.Pnl_Memo.Size = new System.Drawing.Size(368, 160);
		this.Pnl_Memo.TabIndex = 12;
		this.chkShowCodeCorrectRate.Checked = true;
		this.chkShowCodeCorrectRate.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkShowCodeCorrectRate.Location = new System.Drawing.Point(235, 9);
		this.chkShowCodeCorrectRate.Name = "chkShowCodeCorrectRate";
		this.chkShowCodeCorrectRate.Size = new System.Drawing.Size(120, 20);
		this.chkShowCodeCorrectRate.TabIndex = 55;
		this.chkShowCodeCorrectRate.Text = "列印編碼正率";
		this.chkShowCodeCorrectRate.Visible = false;
		this.chkShowCodeCorrectRate.Click += new System.EventHandler(chkShowCodeCorrectRate_Click);
		this.chkDetailListPrintProjectDescription.Location = new System.Drawing.Point(14, 9);
		this.chkDetailListPrintProjectDescription.Name = "chkDetailListPrintProjectDescription";
		this.chkDetailListPrintProjectDescription.Size = new System.Drawing.Size(213, 20);
		this.chkDetailListPrintProjectDescription.TabIndex = 54;
		this.chkDetailListPrintProjectDescription.Text = "列印備註說明事項";
		this.chkDetailListShowAnalysisItemCode.Location = new System.Drawing.Point(132, 84);
		this.chkDetailListShowAnalysisItemCode.Name = "chkDetailListShowAnalysisItemCode";
		this.chkDetailListShowAnalysisItemCode.Size = new System.Drawing.Size(135, 20);
		this.chkDetailListShowAnalysisItemCode.TabIndex = 53;
		this.chkDetailListShowAnalysisItemCode.Text = "單價分析項項次";
		this.ddlDetailListAnalysisItemMark.AutoSize = true;
		this.ddlDetailListAnalysisItemMark.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		valueListItem10.DataValue = "1";
		valueListItem10.DisplayText = "*";
		this.ddlDetailListAnalysisItemMark.Items.Add(valueListItem10);
		this.ddlDetailListAnalysisItemMark.Location = new System.Drawing.Point(132, 131);
		this.ddlDetailListAnalysisItemMark.Name = "ddlDetailListAnalysisItemMark";
		this.ddlDetailListAnalysisItemMark.Size = new System.Drawing.Size(92, 24);
		this.ddlDetailListAnalysisItemMark.TabIndex = 52;
		this.ddlDetailListAnalysisItemMark.Text = null;
		this.chkDetailListDisplayMainItemDetail.Location = new System.Drawing.Point(14, 35);
		this.chkDetailListDisplayMainItemDetail.Name = "chkDetailListDisplayMainItemDetail";
		this.chkDetailListDisplayMainItemDetail.Size = new System.Drawing.Size(213, 20);
		this.chkDetailListDisplayMainItemDetail.TabIndex = 25;
		this.chkDetailListDisplayMainItemDetail.Text = "一般主項顯示數量單價複價";
		appearance9.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnPageBreakSetup.Appearance = appearance9;
		this.btnPageBreakSetup.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.btnPageBreakSetup.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnPageBreakSetup.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnPageBreakSetup.Location = new System.Drawing.Point(233, 32);
		this.btnPageBreakSetup.Name = "btnPageBreakSetup";
		this.btnPageBreakSetup.ShowFocusRect = false;
		this.btnPageBreakSetup.ShowOutline = false;
		this.btnPageBreakSetup.Size = new System.Drawing.Size(88, 23);
		this.btnPageBreakSetup.SupportThemes = false;
		this.btnPageBreakSetup.TabIndex = 24;
		this.btnPageBreakSetup.Text = "跳頁設定...";
		this.btnPageBreakSetup.Click += new System.EventHandler(btnPageBreak_Click);
		this.chkDetailListShowRemark.Checked = true;
		this.chkDetailListShowRemark.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkDetailListShowRemark.Location = new System.Drawing.Point(13, 84);
		this.chkDetailListShowRemark.Name = "chkDetailListShowRemark";
		this.chkDetailListShowRemark.Size = new System.Drawing.Size(113, 20);
		this.chkDetailListShowRemark.TabIndex = 0;
		this.chkDetailListShowRemark.Text = "備註";
		this.ultraLabel1.Location = new System.Drawing.Point(9, 62);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(83, 23);
		this.ultraLabel1.TabIndex = 1;
		this.ultraLabel1.Text = "備註欄：";
		this.chkDetailListShowPccesCode.Checked = true;
		this.chkDetailListShowPccesCode.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkDetailListShowPccesCode.Location = new System.Drawing.Point(13, 108);
		this.chkDetailListShowPccesCode.Name = "chkDetailListShowPccesCode";
		this.chkDetailListShowPccesCode.Size = new System.Drawing.Size(113, 20);
		this.chkDetailListShowPccesCode.TabIndex = 2;
		this.chkDetailListShowPccesCode.Text = "工項代碼";
		this.chkDetailListShowAnalysisItemSymbol.Checked = true;
		this.chkDetailListShowAnalysisItemSymbol.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkDetailListShowAnalysisItemSymbol.Location = new System.Drawing.Point(13, 134);
		this.chkDetailListShowAnalysisItemSymbol.Name = "chkDetailListShowAnalysisItemSymbol";
		this.chkDetailListShowAnalysisItemSymbol.Size = new System.Drawing.Size(120, 20);
		this.chkDetailListShowAnalysisItemSymbol.TabIndex = 3;
		this.chkDetailListShowAnalysisItemSymbol.Text = "單價分析標記";
		this.chkDetailListShowUnofficialItemCode.Location = new System.Drawing.Point(132, 108);
		this.chkDetailListShowUnofficialItemCode.Name = "chkDetailListShowUnofficialItemCode";
		this.chkDetailListShowUnofficialItemCode.Size = new System.Drawing.Size(120, 20);
		this.chkDetailListShowUnofficialItemCode.TabIndex = 4;
		this.chkDetailListShowUnofficialItemCode.Text = "外碼";
		this.gbSummry.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gbSummry.Controls.Add(this.chkSummaryPrintProjectDescription);
		this.gbSummry.Controls.Add(this.chkSummaryIncludeWorkItem);
		this.gbSummry.Controls.Add(this.panelSheetVisibilityOption);
		this.gbSummry.Controls.Add(this.Pnl_PntLevel);
		this.gbSummry.Location = new System.Drawing.Point(9, 213);
		this.gbSummry.Name = "gbSummry";
		this.gbSummry.Size = new System.Drawing.Size(257, 184);
		this.gbSummry.TabIndex = 0;
		this.gbSummry.TabStop = false;
		this.gbSummry.Text = "總表";
		this.chkSummaryIncludeWorkItem.Checked = true;
		this.chkSummaryIncludeWorkItem.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkSummaryIncludeWorkItem.Location = new System.Drawing.Point(16, 32);
		this.chkSummaryIncludeWorkItem.Name = "chkSummaryIncludeWorkItem";
		this.chkSummaryIncludeWorkItem.Size = new System.Drawing.Size(120, 20);
		this.chkSummaryIncludeWorkItem.TabIndex = 24;
		this.chkSummaryIncludeWorkItem.Text = "包含工作要項";
		this.panelSheetVisibilityOption.Controls.Add(this.chkOutputResourceList);
		this.panelSheetVisibilityOption.Controls.Add(this.chkOutputBreakdownList);
		this.panelSheetVisibilityOption.Controls.Add(this.chkOutputDetailList);
		this.panelSheetVisibilityOption.Controls.Add(this.chkOutputSummary);
		this.panelSheetVisibilityOption.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panelSheetVisibilityOption.Location = new System.Drawing.Point(8, 132);
		this.panelSheetVisibilityOption.Name = "panelSheetVisibilityOption";
		this.panelSheetVisibilityOption.Size = new System.Drawing.Size(204, 44);
		this.panelSheetVisibilityOption.TabIndex = 23;
		this.panelSheetVisibilityOption.Visible = false;
		this.chkOutputResourceList.Checked = true;
		this.chkOutputResourceList.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkOutputResourceList.Location = new System.Drawing.Point(104, 24);
		this.chkOutputResourceList.Name = "chkOutputResourceList";
		this.chkOutputResourceList.Size = new System.Drawing.Size(88, 20);
		this.chkOutputResourceList.TabIndex = 3;
		this.chkOutputResourceList.Text = "資源統計表";
		this.chkOutputBreakdownList.Checked = true;
		this.chkOutputBreakdownList.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkOutputBreakdownList.Location = new System.Drawing.Point(8, 24);
		this.chkOutputBreakdownList.Name = "chkOutputBreakdownList";
		this.chkOutputBreakdownList.Size = new System.Drawing.Size(96, 20);
		this.chkOutputBreakdownList.TabIndex = 2;
		this.chkOutputBreakdownList.Text = "單價分析表";
		this.chkOutputDetailList.Checked = true;
		this.chkOutputDetailList.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkOutputDetailList.Location = new System.Drawing.Point(104, 0);
		this.chkOutputDetailList.Name = "chkOutputDetailList";
		this.chkOutputDetailList.Size = new System.Drawing.Size(64, 20);
		this.chkOutputDetailList.TabIndex = 1;
		this.chkOutputDetailList.Text = "詳細表";
		this.chkOutputSummary.Checked = true;
		this.chkOutputSummary.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkOutputSummary.Location = new System.Drawing.Point(8, 0);
		this.chkOutputSummary.Name = "chkOutputSummary";
		this.chkOutputSummary.Size = new System.Drawing.Size(48, 20);
		this.chkOutputSummary.TabIndex = 0;
		this.chkOutputSummary.Text = "總表";
		this.Pnl_PntLevel.Controls.Add(this.ddlSummaryPrintLevel);
		this.Pnl_PntLevel.Controls.Add(this.lbSummaryPrintLevel);
		this.Pnl_PntLevel.Location = new System.Drawing.Point(12, 58);
		this.Pnl_PntLevel.Name = "Pnl_PntLevel";
		this.Pnl_PntLevel.Size = new System.Drawing.Size(144, 36);
		this.Pnl_PntLevel.TabIndex = 22;
		this.ddlSummaryPrintLevel.Location = new System.Drawing.Point(80, 5);
		this.ddlSummaryPrintLevel.Maximum = new decimal(new int[4] { 6, 0, 0, 0 });
		this.ddlSummaryPrintLevel.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.ddlSummaryPrintLevel.Name = "ddlSummaryPrintLevel";
		this.ddlSummaryPrintLevel.Size = new System.Drawing.Size(56, 25);
		this.ddlSummaryPrintLevel.TabIndex = 19;
		this.ddlSummaryPrintLevel.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.ddlSummaryPrintLevel.KeyDown += new System.Windows.Forms.KeyEventHandler(ddlSummaryPrintLevel_KeyDown);
		this.lbSummaryPrintLevel.Location = new System.Drawing.Point(3, 8);
		this.lbSummaryPrintLevel.Name = "lbSummaryPrintLevel";
		this.lbSummaryPrintLevel.Size = new System.Drawing.Size(76, 23);
		this.lbSummaryPrintLevel.TabIndex = 17;
		this.lbSummaryPrintLevel.Text = "列印層數:";
		this.chkSummaryPrintProjectDescription.Location = new System.Drawing.Point(12, 100);
		this.chkSummaryPrintProjectDescription.Name = "chkSummaryPrintProjectDescription";
		this.chkSummaryPrintProjectDescription.Size = new System.Drawing.Size(213, 20);
		this.chkSummaryPrintProjectDescription.TabIndex = 55;
		this.chkSummaryPrintProjectDescription.Text = "列印總表說明事項";
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 18);
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(666, 710);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel4);
		this.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.MinimizeBox = false;
		base.Name = "FormBudgetExp_WzdOption";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Excel 輸出選項";
		base.Load += new System.EventHandler(FormBudgetExp_WzdOption_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormBudgetExp_WzdOption_KeyDown);
		this.panel4.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.gbOption.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ddlExcelFont).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cldPrintDate).EndInit();
		this.gbCostBreakdownList.ResumeLayout(false);
		this.Pnl_Sort.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.opSortItems).EndInit();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.opSortOption).EndInit();
		((System.ComponentModel.ISupportInitialize)this.opDuplicationOption).EndInit();
		this.panel2.ResumeLayout(false);
		this.panelRequestPrintQty.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ddlBreakdownListAnalysisItemMark).EndInit();
		this.gbDetail.ResumeLayout(false);
		this.Pnl_Memo.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ddlDetailListAnalysisItemMark).EndInit();
		this.gbSummry.ResumeLayout(false);
		this.panelSheetVisibilityOption.ResumeLayout(false);
		this.Pnl_PntLevel.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ddlSummaryPrintLevel).EndInit();
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

	public FormBudgetExp_WzdOption()
	{
		InitializeComponent();
	}

	private void FormBudgetExp_WzdOption_Load(object sender, EventArgs e)
	{
		if (FormActionName == PccesFormAction.BID)
		{
			project = new BidProject();
		}
		else
		{
			project = new BudProject();
		}
		dsProject = project.GetProject(projectCode);
		SetUpComponentVisibilityAndDefaultValue();
		CorrectRatio();
		LoadExcelOptions();
	}

	private void SetUpComponentVisibilityAndDefaultValue()
	{
		if (isSubmit)
		{
			gbOption.Enabled = false;
			gbDetail.Enabled = false;
			gbSummry.Enabled = false;
			gbCostBreakdownList.Enabled = false;
		}
		else if (FormActionName != PccesFormAction.BUD)
		{
			if (FormActionName != PccesFormAction.BID)
			{
				chkWithEnglish.Visible = false;
				chkBidFooterPrintBidder.Visible = false;
				chkBidFooterPrintVendorInfo.Visible = false;
				chkDetailListPrintProjectDescription.Visible = false;
				chkSummaryPrintProjectDescription.Visible = false;
				btnPageBreakSetup.Visible = false;
				if (FormActionName == PccesFormAction.SplitContract)
				{
					chkDetailListPrintProjectDescription.Visible = true;
					chkSummaryPrintProjectDescription.Visible = true;
				}
			}
			chkBudgetPrintPrice.Text = "列印價格";
			chkBidPrintSummary.Visible = false;
			panelRequestPrintQty.Visible = false;
			if (FormActionName == PccesFormAction.Invoice)
			{
				base.Height = 457;
				gbCostBreakdownList.Visible = false;
				chkDetailListDisplayMainItemDetail.Visible = false;
				chkBidPrintSummary.Visible = false;
				chkBidPrintSummary.Checked = true;
			}
		}
		else if (outputBudget)
		{
			chkBidPrintSummary.Visible = false;
			chkBidFooterPrintBidder.Visible = false;
			chkBidFooterPrintVendorInfo.Visible = false;
			panelRequestPrintQty.Visible = false;
		}
		else
		{
			chkBudgetPrintPrice.Visible = false;
			chkBidFooterPrintVendorInfo.Visible = false;
		}
		cldPrintDate.Value = DateTime.Today;
		ddlExcelFont.SelectedIndex = 0;
		ddlDetailListAnalysisItemMark.SelectedIndex = 0;
		ddlBreakdownListAnalysisItemMark.SelectedIndex = 0;
	}

	private void LoadExcelOptions()
	{
		if (dsProject.Tables[0].Rows.Count > 0 && dsProject.Tables[0].Rows[0]["printMode"].ToString() != string.Empty)
		{
			printMode = dsProject.Tables[0].Rows[0]["printMode"].ToString();
			chkSummaryIncludeWorkItem.Checked = StringToBoolean(printMode.Substring(0, 1));
			ddlSummaryPrintLevel.Value = ArchConvert.Obj2Int(printMode.Substring(1, 1));
			chkDetailListDisplayMainItemDetail.Checked = StringToBoolean(printMode.Substring(5, 1));
			chkDetailListShowRemark.Checked = StringToBoolean(printMode.Substring(10, 1));
			chkDetailListShowPccesCode.Checked = StringToBoolean(printMode.Substring(11, 1));
			chkDetailListShowAnalysisItemSymbol.Checked = StringToBoolean(printMode.Substring(12, 1));
			chkDetailListShowUnofficialItemCode.Checked = StringToBoolean(printMode.Substring(14, 1));
			chkBreakdownListShowRemark.Checked = StringToBoolean(printMode.Substring(20, 1));
			chkBreakdownListShowPccesCode.Checked = StringToBoolean(printMode.Substring(21, 1));
			chkBreakdownListShowAnalysisItemSymbol.Checked = StringToBoolean(printMode.Substring(22, 1));
			chkBreakdownListShowUnofficialItemCode.Checked = StringToBoolean(printMode.Substring(24, 1));
			opSortOption.CheckedIndex = ArchConvert.Obj2Int(printMode.Substring(25, 1));
			opDuplicationOption.CheckedIndex = ((ArchConvert.Obj2Int(printMode.Substring(26, 1)) == 0) ? 1 : 0);
			chkDuplicateAnalysisItemInDetailList.Checked = StringToBoolean(printMode.Substring(27, 1));
			chkHalfPageFormat.Checked = StringToBoolean(printMode.Substring(28, 1));
			chkSkipCommentItem.Checked = StringToBoolean(printMode.Substring(29, 1));
			chkSkipSubTotalItem.Checked = StringToBoolean(printMode.Substring(30, 1));
			chkShrinkToFit.Checked = StringToBoolean(printMode.Substring(35, 1));
			chkNoBorderInLineBreak.Checked = StringToBoolean(printMode.Substring(36, 1));
			ddlExcelFont.Text = printMode.Substring(39, 3);
			if (printMode.Length > 47)
			{
				chkPrintLaborQty.Checked = StringToBoolean(printMode.Substring(47, 1));
				chkPrintEquipmentQty.Checked = StringToBoolean(printMode.Substring(48, 1));
				chkPrintMaterialQty.Checked = StringToBoolean(printMode.Substring(49, 1));
				chkPrintMiscellaneaQty.Checked = StringToBoolean(printMode.Substring(50, 1));
				chkWithEnglish.Checked = StringToBoolean(printMode.Substring(51, 1));
				chkPrintDate.Checked = StringToBoolean(printMode.Substring(52, 1));
				cldPrintDate.Value = ArchConvert.Obj2DateTime(printMode.Substring(53, 10));
				chkBidPrintSummary.Checked = StringToBoolean(printMode.Substring(63, 1));
				chkBidFooterPrintBidder.Checked = StringToBoolean(printMode.Substring(64, 1));
				chkDetailListShowAnalysisItemCode.Checked = StringToBoolean(printMode.Substring(65, 1));
				chkDetailListPrintProjectDescription.Checked = printMode.Length > 66 && StringToBoolean(printMode.Substring(66, 1));
			}
			if (printMode.Length > 67)
			{
				chkTakePlaceByMaxValue.Checked = printMode.Length > 67 && StringToBoolean(printMode.Substring(67, 1));
			}
			if (printMode.Length > 68)
			{
				chkShowCodeCorrectRate.Checked = printMode.Length > 68 && StringToBoolean(printMode.Substring(68, 1));
			}
			if (printMode.Length > 69)
			{
				chkSummaryPrintProjectDescription.Checked = printMode.Length > 69 && StringToBoolean(printMode.Substring(69, 1));
			}
		}
	}

	private void btnFinish_Click(object sender, EventArgs e)
	{
		string exportSheets = BooleanToString(chkOutputSummary.Checked) + BooleanToString(chkOutputDetailList.Checked) + BooleanToString(chkOutputBreakdownList.Checked) + BooleanToString(chkOutputResourceList.Checked);
		(base.Owner as FormBudgetExp_Wzd)._SummaryLevel = (int)ddlSummaryPrintLevel.Value;
		(base.Owner as FormBudgetExp_Wzd)._SummaryIsIncWrkItm = chkSummaryIncludeWorkItem.Checked;
		(base.Owner as FormBudgetExp_Wzd)._IsDetMemo = chkDetailListShowRemark.Checked;
		(base.Owner as FormBudgetExp_Wzd)._IsDetPccesCode = chkDetailListShowPccesCode.Checked;
		(base.Owner as FormBudgetExp_Wzd)._IsDetAnaMark = chkDetailListShowAnalysisItemSymbol.Checked;
		(base.Owner as FormBudgetExp_Wzd)._DetAnaMark = ddlDetailListAnalysisItemMark.Text.Trim();
		(base.Owner as FormBudgetExp_Wzd)._IsDetExtCode = chkDetailListShowUnofficialItemCode.Checked;
		(base.Owner as FormBudgetExp_Wzd)._IsAnaMemo = chkBreakdownListShowRemark.Checked;
		(base.Owner as FormBudgetExp_Wzd)._IsAnaPccesCode = chkBreakdownListShowPccesCode.Checked;
		(base.Owner as FormBudgetExp_Wzd)._IsAnaAnaMark = chkBreakdownListShowAnalysisItemSymbol.Checked;
		(base.Owner as FormBudgetExp_Wzd)._AnaMark = ddlBreakdownListAnalysisItemMark.Text.Trim();
		(base.Owner as FormBudgetExp_Wzd)._IsAnaExtCode = chkBreakdownListShowUnofficialItemCode.Checked;
		(base.Owner as FormBudgetExp_Wzd)._IsAnaHalfPage = chkHalfPageFormat.Checked;
		(base.Owner as FormBudgetExp_Wzd)._IsRepeatDetailAnalysis = chkDuplicateAnalysisItemInDetailList.Checked;
		(base.Owner as FormBudgetExp_Wzd)._IsSkipCommentItem = chkSkipCommentItem.Checked;
		(base.Owner as FormBudgetExp_Wzd)._IsSkipSubtotalItem = chkSkipSubTotalItem.Checked;
		(base.Owner as FormBudgetExp_Wzd)._AnaSortOrder = opSortOption.CheckedIndex.ToString();
		(base.Owner as FormBudgetExp_Wzd)._AnaSortOrderDet = opSortItems.CheckedIndex.ToString();
		(base.Owner as FormBudgetExp_Wzd)._AnaRepeat = ((opDuplicationOption.CheckedIndex == 0) ? "1" : "0");
		(base.Owner as FormBudgetExp_Wzd)._ExportSheets = exportSheets;
		(base.Owner as FormBudgetExp_Wzd)._NoMiddle = chkNoBorderInLineBreak.Checked;
		(base.Owner as FormBudgetExp_Wzd)._AutoShrink = chkShrinkToFit.Checked;
		(base.Owner as FormBudgetExp_Wzd)._Ismainprice = chkDetailListDisplayMainItemDetail.Checked;
		(base.Owner as FormBudgetExp_Wzd)._ExcelFontName = ddlExcelFont.Text;
		(base.Owner as FormBudgetExp_Wzd)._WithEnglish = chkWithEnglish.Checked;
		(base.Owner as FormBudgetExp_Wzd)._PrintDate = chkPrintDate.Checked;
		(base.Owner as FormBudgetExp_Wzd)._DatePrinted = (DateTime)cldPrintDate.Value;
		(base.Owner as FormBudgetExp_Wzd)._BudgetPrintPrice = chkBudgetPrintPrice.Checked;
		(base.Owner as FormBudgetExp_Wzd)._BidPrintSummary = chkBidPrintSummary.Checked;
		(base.Owner as FormBudgetExp_Wzd)._BidFooterPrintBidder = chkBidFooterPrintBidder.Checked;
		(base.Owner as FormBudgetExp_Wzd)._BidFooterPrintVendorInfo = chkBidFooterPrintVendorInfo.Checked;
		(base.Owner as FormBudgetExp_Wzd)._PrintLaborQty = chkPrintLaborQty.Checked;
		(base.Owner as FormBudgetExp_Wzd)._PrintEquipmentQty = chkPrintEquipmentQty.Checked;
		(base.Owner as FormBudgetExp_Wzd)._PrintMaterialQty = chkPrintMaterialQty.Checked;
		(base.Owner as FormBudgetExp_Wzd)._PrintMiscellaneaQty = chkPrintMiscellaneaQty.Checked;
		(base.Owner as FormBudgetExp_Wzd)._DetailListShowAnalysisItemCode = chkDetailListShowAnalysisItemCode.Checked;
		(base.Owner as FormBudgetExp_Wzd)._DetailListPrintProjectDescription = chkDetailListPrintProjectDescription.Checked;
		(base.Owner as FormBudgetExp_Wzd)._EnableOldExportExcel = chkEnableOldExportExcel.Checked;
		(base.Owner as FormBudgetExp_Wzd)._TakePlaceByMaxValue = chkTakePlaceByMaxValue.Checked;
		(base.Owner as FormBudgetExp_Wzd)._ShowCodeCorrectRate = chkShowCodeCorrectRate.Checked;
		(base.Owner as FormBudgetExp_Wzd)._SummaryPrintProjectDescription = chkSummaryPrintProjectDescription.Checked;
		if (FormActionName == PccesFormAction.BUD)
		{
			dsProject.Tables[0].Rows[0]["printMode"] = AssemblePrintMode();
			project.GetDatasetUpdate(dsProject);
			return;
		}
		(base.Owner as FormBudgetExp_Wzd)._ExcelSettingsFromDB = false;
		if (FormActionName == PccesFormAction.BID && printMode != null && printMode != string.Empty)
		{
			string tempPrintMode = string.Empty;
			tempPrintMode = AssemblePrintMode();
			printMode = printMode.Remove(37, 2).Remove(32, 1).Remove(15, 1);
			tempPrintMode = tempPrintMode.Remove(37, 2).Remove(32, 1).Remove(15, 1);
			if ((printMode.Length != tempPrintMode.Length && printMode != tempPrintMode.Substring(0, 43)) || (printMode.Length == tempPrintMode.Length && printMode != tempPrintMode))
			{
				string warningMessage = "注意：標單和預算書輸出列印選項不同！";
				MessageBox.Show(this, warningMessage, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
	}

	private string AssemblePrintMode()
	{
		string IsOldReCal = CommonMethods.IniReadValue(iniFilePath + "OptionSet.ini", "BDGT", "IsOldReCal");
		string AssemType = CommonMethods.IniReadValue(iniFilePath + "PccesMain.ini", "AutoItemNo", "AssemType");
		string printMode = string.Empty;
		printMode = BooleanToString(chkSummaryIncludeWorkItem.Checked) + ddlSummaryPrintLevel.Value + "000";
		string text = printMode;
		printMode = text + BooleanToString(chkDetailListDisplayMainItemDetail.Checked) + "0000" + BooleanToString(chkDetailListShowRemark.Checked) + BooleanToString(chkDetailListShowPccesCode.Checked) + BooleanToString(chkDetailListShowAnalysisItemSymbol.Checked) + ((ddlDetailListAnalysisItemMark.Text != string.Empty && chkDetailListShowAnalysisItemSymbol.Checked) ? ddlDetailListAnalysisItemMark.Text : "0") + BooleanToString(chkDetailListShowUnofficialItemCode.Checked) + "10000";
		object obj = printMode;
		printMode = string.Concat(obj, BooleanToString(chkBreakdownListShowRemark.Checked), BooleanToString(chkBreakdownListShowPccesCode.Checked), BooleanToString(chkBreakdownListShowAnalysisItemSymbol.Checked), (ddlBreakdownListAnalysisItemMark.Text != string.Empty && chkBreakdownListShowAnalysisItemSymbol.Checked) ? ddlBreakdownListAnalysisItemMark.Text : "0", BooleanToString(chkBreakdownListShowUnofficialItemCode.Checked), opSortOption.CheckedIndex.ToString(), (opDuplicationOption.CheckedIndex == 0) ? "1" : "0", BooleanToString(chkDuplicateAnalysisItemInDetailList.Checked), BooleanToString(chkHalfPageFormat.Checked), BooleanToString(chkSkipCommentItem.Checked), BooleanToString(chkSkipSubTotalItem.Checked), opSortItems.CheckedIndex, "000");
		printMode = printMode + BooleanToString(chkShrinkToFit.Checked) + BooleanToString(chkNoBorderInLineBreak.Checked);
		printMode = printMode + "01" + ddlExcelFont.Text + "00000";
		text = printMode;
		printMode = text + BooleanToString(chkPrintLaborQty.Checked) + BooleanToString(chkPrintEquipmentQty.Checked) + BooleanToString(chkPrintMaterialQty.Checked) + BooleanToString(chkPrintMiscellaneaQty.Checked) + BooleanToString(chkWithEnglish.Checked) + BooleanToString(chkPrintDate.Checked) + ((DateTime)cldPrintDate.Value).ToString("yyyy/MM/dd") + BooleanToString(chkBidPrintSummary.Checked) + BooleanToString(chkBidFooterPrintBidder.Checked) + BooleanToString(chkDetailListShowAnalysisItemCode.Checked) + BooleanToString(chkDetailListPrintProjectDescription.Checked) + BooleanToString(chkTakePlaceByMaxValue.Checked);
		printMode += BooleanToString(chkShowCodeCorrectRate.Checked);
		return printMode + BooleanToString(chkSummaryPrintProjectDescription.Checked);
	}

	private bool StringToBoolean(string ZeroOrOne)
	{
		return ZeroOrOne == "1";
	}

	private string BooleanToString(bool booleanValue)
	{
		return booleanValue ? "1" : "0";
	}

	private void opSort_ValueChanged(object sender, EventArgs e)
	{
		if (opSortOption.CheckedIndex == 1)
		{
			opDuplicationOption.CheckedIndex = 1;
			opDuplicationOption.Enabled = false;
		}
		else
		{
			opSortItems.Visible = false;
			opDuplicationOption.Enabled = true;
		}
		if (opSortOption.CheckedIndex == 0 && opDuplicationOption.CheckedIndex == 1)
		{
			chkDuplicateAnalysisItemInDetailList.Enabled = true;
		}
		else
		{
			chkDuplicateAnalysisItemInDetailList.Enabled = false;
		}
	}

	private void ddlSummaryPrintLevel_KeyDown(object sender, KeyEventArgs e)
	{
		if (!isPreview && e.Alt && e.KeyCode == Keys.F12)
		{
			panelSheetVisibilityOption.Visible = !panelSheetVisibilityOption.Visible;
		}
	}

	private void btnPageBreak_Click(object sender, EventArgs e)
	{
		FormBudgetPageBreak FM_PG_BK = new FormBudgetPageBreak();
		FM_PG_BK._UserID = userID;
		FM_PG_BK._ProjectCode = projectCode;
		FM_PG_BK._ActionName = FormActionName;
		FM_PG_BK.Owner = this;
		FM_PG_BK.ShowDialog();
		FM_PG_BK.Close();
		FM_PG_BK.Dispose();
		FM_PG_BK = null;
	}

	private void opRepeat_ValueChanged(object sender, EventArgs e)
	{
		if (opSortOption.CheckedIndex == 0 && opDuplicationOption.CheckedIndex == 1)
		{
			chkDuplicateAnalysisItemInDetailList.Enabled = true;
		}
		else
		{
			chkDuplicateAnalysisItemInDetailList.Enabled = false;
		}
	}

	private void CorrectRatio()
	{
		double ratio = CommonMethods.GetWindowRatio(base.Handle);
		if (ratio != 1.0)
		{
			gbSummry.Font = new Font(gbSummry.Font.Name, (float)((double)gbSummry.Font.Size * ratio));
			gbDetail.Font = new Font(gbDetail.Font.Name, (float)((double)gbDetail.Font.Size * ratio));
			gbCostBreakdownList.Font = new Font(gbCostBreakdownList.Font.Name, (float)((double)gbCostBreakdownList.Font.Size * ratio));
			panel4.Font = new Font(panel4.Font.Name, (float)((double)panel4.Font.Size * ratio));
		}
	}

	private void chkPrintDate_CheckedChanged(object sender, EventArgs e)
	{
		cldPrintDate.Enabled = chkPrintDate.Checked;
	}

	private void FormBudgetExp_WzdOption_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F11 && e.Modifiers == Keys.Control)
		{
			chkEnableOldExportExcel.Enabled = true;
			chkShowCodeCorrectRate.Visible = true;
		}
	}

	private void chkShowCodeCorrectRate_Click(object sender, EventArgs e)
	{
		if (chkShowCodeCorrectRate.Checked)
		{
			if (MessageBox.Show(this, "若取消勾選, 總表及詳細表將不會列印編碼正確率, 確定取消勾選嗎?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
			{
				chkShowCodeCorrectRate.Checked = true;
			}
			else
			{
				chkShowCodeCorrectRate.Checked = false;
			}
		}
	}
}
