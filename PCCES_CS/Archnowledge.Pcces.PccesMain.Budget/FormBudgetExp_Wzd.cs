using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.Bid;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.DomainModule.LogicalBase;
using Archnowledge.Pcces.PccesMain.MrsBase;
using Archnowledge.Pcces.PccesMain.ShellLib;
using Archnowledge.Pcces.STDClass;
using Archnowledge.Pcces.XMLClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinProgressBar;
using Infragistics.Win.UltraWinTabControl;
using PCCES.CODECHECK;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetExp_Wzd : Form
{
	private IContainer components;

	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_A;

	private UltraTabPageControl Tab_B;

	private UltraLabel ultraLabel1;

	private GroupBox groupBox1;

	private UltraButton A_Btn_Cncl;

	private UltraButton A_Btn_Next;

	private UltraButton A_Btn_Prev;

	private UltraLabel ultraLabel2;

	private RadioButton rbOutputBudget;

	private RadioButton rbOutputBid;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private Panel panel2;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel7;

	private Panel panel3;

	private GroupBox groupBox2;

	private UltraTabPageControl Tab_C;

	private Panel panel5;

	private UltraLabel ultraLabel11;

	private Panel panel7;

	private UltraLabel lblWait;

	private UltraTabPageControl Tab_D;

	private UltraLabel ultraLabel14;

	private UltraLabel ultraLabel13;

	private UltraLabel ultraLabel12;

	private GroupBox groupBox4;

	private UltraButton D_Btn_Fnsh;

	private UltraButton D_Btn_Prev;

	private UltraButton B_Btn_Cncl;

	private UltraButton B_Btn_Next;

	private UltraButton B_Btn_Prev;

	private GroupBox groupBox3;

	private UltraButton C_Btn_Cncl;

	private UltraButton C_Btn_Next;

	private UltraButton C_Btn_Prev;

	private UltraLabel ultraLabel10;

	private UltraTextEditor tbFileName;

	private UltraButton btnOpenOutputFolderBrowser;

	private UltraLabel ultraLabel15;

	private UltraCheckEditor chkIncludeCostBreakdownList;

	private UltraCheckEditor chkOutputXML;

	private UltraCheckEditor chkOutputExcel;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel8;

	private UltraTextEditor tbOutputPath;

	private FolderBrowserDialog OutputFileFolderBrowser;

	private UltraProgressBar progressBarTotal;

	private UltraProgressBar progressBarSingle;

	private UltraLabel ultraLabel9;

	private UltraLabel ultraLabel16;

	private UltraButton btnOpenDirectory;

	private System.Windows.Forms.Timer timer1;

	private UltraTabPageControl Tab_A1;

	private UltraLabel ultraLabel18;

	private UltraLabel ultraLabel19;

	private GroupBox groupBox5;

	private UltraCheckEditor chkSubmitIncludeCostBreakdownList;

	private UltraButton A1_Btn_Cncl;

	private UltraButton A1_Btn_Next;

	private UltraButton A1_Btn_Prev;

	private UltraLabel lblSaveAs;

	private UltraTextEditor txtSaveAsProjectCode;

	private UltraButton btnOpenExcelOption;

	private UltraLabel lbOutputXMLFileName;

	private UltraButton btnOpenExcel;

	private UltraLabel lbOutputExcelFileName;

	private GroupBox gbHeaderAndFooter;

	private UltraComboEditor ddlBudgetFooter;

	private UltraLabel ultraLabel20;

	private UltraLabel ultraLabel21;

	private UltraLabel ultraLabel22;

	private UltraTextEditor tbEnglishHeader;

	private UltraLabel ultraLabel23;

	private UltraTextEditor tbHeader;

	private UltraButton btnHeaderAndFooterSetting;

	private UltraLabel lblWarning2;

	private LinkLabel llbXMLStandard;

	private UltraButton btnXMLInstruction;

	private UltraCheckEditor chkOutputZMD;

	private UltraCheckEditor chkUseProjectCodeAsFileName;

	private UltraCheckEditor ultraCheckEditor1;

	private UltraCheckEditor chkOutputAliasAsItemName;

	private UltraCheckEditor chkBreakdownListLockCost;

	private UltraLabel ultraLabel17;

	private UltraTabPageControl Tab_F;

	private UltraButton btnCancel;

	private GroupBox groupBox6;

	private UltraButton btnPreview;

	private GroupBox gbPreviewHeaderAndFooter;

	private UltraComboEditor ddlPreviewBudgetFooter;

	private UltraLabel ultraLabel24;

	private UltraLabel ultraLabel25;

	private UltraLabel ultraLabel26;

	private UltraTextEditor tbPreviewEnglishHeader;

	private UltraLabel ultraLabel27;

	private UltraTextEditor tbPreviewHeader;

	private UltraButton btnPreviewExcelOption;

	private UltraCheckEditor chkPreviewIncludeCostBreakdownList;

	private Panel panel11;

	private UltraLabel ultraLabel28;

	private UltraLabel ultraLabel29;

	private Panel panel12;

	private RadioButton rbPreviewBid;

	private RadioButton rbPreviewBudget;

	private UltraLabel ultraLabel30;

	private GroupBox groupBox8;

	private UltraCheckEditor chkOutputResourceList;

	private UltraCheckEditor chkOutputBreakdownList;

	private UltraCheckEditor chkOutputDetailList;

	private UltraCheckEditor chkOutputSummary;

	private RadioButton rbOutputBlankBudget;

	public Panel panel1;

	public Panel panel4;

	public Panel panel8;

	public Panel panel6;

	public Panel panel9;

	public Panel panel10;

	private UltraCheckEditor chkXMLformat102;

	private GroupBox groupBox7;

	private Label lbl_confirmRate;

	private Label lbl_WeightFitRatio;

	private Label lbl_WeightCorrectRatio;

	private Label lbl_correctRate;

	private Label label4;

	private Label label3;

	private Label label2;

	private Label label1;

	private Label lbl_AutoNumUpd_Warn;

	private System.Windows.Forms.Timer timer2;

	private string projectCode;

	private string chgCount;

	private string invoiceCount = "";

	private string ProjectFlag;

	private string userID;

	private PccesFormAction FormActionName;

	private Conversion CNVSN;

	private string OwnerChineseName = "";

	private string OwnerEnglishName = "";

	private string ProjectChineseName = "";

	private string ProjectEnglishName = "";

	private string ProjectChineseAddress = "";

	private string ProjectEnglishAddress = "";

	private string AccountCodeLower;

	private string AccountCodeUpper;

	private string MainProjectCode = "";

	private string ProjectDescription = string.Empty;

	private bool isSubmit = false;

	private string ApplicationDirectory = AppDomain.CurrentDomain.BaseDirectory;

	private int FormNormalHeight = 390;

	private int FormExpandedHeight = 549;

	private int FormPreviewHeight = 495;

	private int FormPreviewCollapsedHeight = 300;

	private decimal dCorrectRatio = 0m;

	private decimal dFitRatio = 0m;

	private decimal dWeightCorrectRatio = 0m;

	private decimal dWeightFitRatio = 0m;

	private decimal shareVDF1 = 0m;

	private int shareVDF1sNo = 0;

	private bool IsExcelStarted = false;

	private bool ExcelSettingsFromDB = true;

	private bool Preview = false;

	private Archnowledge.Pcces.DomainModule.LogicalBase.Project project;

	private SubMemo subMemo = new SubMemo();

	private DataSet dsProject;

	private DataSet dsSubMemo;

	private string CurrentBudgetFormAction = "BUD";

	private bool ShrinkToFit = false;

	private string ExportSheets = "1111";

	private bool HalfPageFormat = false;

	private int SummaryPrintLevel = 1;

	private bool SummaryIncludeWorkItem;

	private bool DetailListShowRemark = false;

	private bool DetailListShowPccesCode = false;

	private bool DetailListShowAnalysisItemSymbol = false;

	private string DetailListAnalysisItemMark = "*";

	private bool DetailListShowUnofficialItemCode = false;

	private bool BreakdownListShowRemark = false;

	private bool BreakdownListShowPccesCode = false;

	private bool BreakdownListShowAnalysisItemSymbol = false;

	private string BreakdownListAnalysisItemMark = "*";

	private bool BreakdownListShowUnofficialItemCode = false;

	private string BreakdownListSortOption = "1";

	private string BreakdownListSortItems = "0";

	private string BreakdownListDuplicationOption = "0";

	private bool NoBorderInLineBreak = false;

	private bool DuplicateAnalysisItemInDetailList = false;

	private bool SkipCommentItem = false;

	private bool DetailListDisplayMainItemDetail = false;

	private bool SkipSubTotalItem = false;

	private string ExcelFontName;

	private bool WithEnglish = false;

	private bool PrintDate = true;

	private DateTime DatePrinted;

	private bool BudgetPrintPrice = true;

	private bool BidPrintSummary = true;

	private bool BidFooterPrintBidder = true;

	private bool BidFooterPrintVendorInfo = false;

	private bool PrintLaborQty = true;

	private bool PrintEquipmentQty = true;

	private bool PrintMaterialQty = true;

	private bool PrintMiscellaneaQty = true;

	private bool DetailListShowAnalysisItemCode = false;

	private bool EnableOldExportExcel = false;

	private bool DetailListPrintProjectDescription = false;

	private bool SummaryPrintProjectDescription = false;

	private bool IsBlankBudget = false;

	private bool TakePlaceByMaxValue = false;

	private bool ShowCodeCorrectRate = true;

	public string _queue
	{
		get
		{
			return invoiceCount;
		}
		set
		{
			invoiceCount = value;
		}
	}

	public string _ProjFLAG
	{
		get
		{
			return ProjectFlag;
		}
		set
		{
			ProjectFlag = value;
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

	public string _MainProjectCode
	{
		get
		{
			return MainProjectCode;
		}
		set
		{
			MainProjectCode = value;
		}
	}

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

	public string _DeptName
	{
		get
		{
			return OwnerChineseName;
		}
		set
		{
			OwnerChineseName = value;
		}
	}

	public string _DeptEName
	{
		get
		{
			return OwnerEnglishName;
		}
		set
		{
			OwnerEnglishName = value;
		}
	}

	public string _ProjectNameC
	{
		get
		{
			return ProjectChineseName;
		}
		set
		{
			ProjectChineseName = value;
		}
	}

	public string _ProjectNameE
	{
		get
		{
			return ProjectEnglishName;
		}
		set
		{
			ProjectEnglishName = value;
		}
	}

	public string _ProjectAddress
	{
		get
		{
			return ProjectChineseAddress;
		}
		set
		{
			ProjectChineseAddress = value;
		}
	}

	public string _ProjectEngAddress
	{
		get
		{
			return ProjectEnglishAddress;
		}
		set
		{
			ProjectEnglishAddress = value;
		}
	}

	public string _AccountCode1
	{
		get
		{
			return AccountCodeLower;
		}
		set
		{
			AccountCodeLower = value;
		}
	}

	public string _AccountCode2
	{
		get
		{
			return AccountCodeUpper;
		}
		set
		{
			AccountCodeUpper = value;
		}
	}

	public string _ProjectDescription
	{
		set
		{
			ProjectDescription = value;
		}
	}

	public string _chgCount
	{
		get
		{
			return chgCount;
		}
		set
		{
			chgCount = value;
		}
	}

	public bool _ExcelSettingsFromDB
	{
		get
		{
			return ExcelSettingsFromDB;
		}
		set
		{
			ExcelSettingsFromDB = value;
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

	public bool _Preview
	{
		set
		{
			Preview = value;
		}
	}

	public bool _AutoShrink
	{
		get
		{
			return ShrinkToFit;
		}
		set
		{
			ShrinkToFit = value;
		}
	}

	public string _ExportSheets
	{
		get
		{
			return ExportSheets;
		}
		set
		{
			ExportSheets = value;
		}
	}

	public bool _IsAnaHalfPage
	{
		get
		{
			return HalfPageFormat;
		}
		set
		{
			HalfPageFormat = value;
		}
	}

	public int _SummaryLevel
	{
		get
		{
			return SummaryPrintLevel;
		}
		set
		{
			SummaryPrintLevel = value;
		}
	}

	public bool _SummaryIsIncWrkItm
	{
		get
		{
			return SummaryIncludeWorkItem;
		}
		set
		{
			SummaryIncludeWorkItem = value;
		}
	}

	public bool _IsDetMemo
	{
		get
		{
			return DetailListShowRemark;
		}
		set
		{
			DetailListShowRemark = value;
		}
	}

	public bool _IsDetPccesCode
	{
		get
		{
			return DetailListShowPccesCode;
		}
		set
		{
			DetailListShowPccesCode = value;
		}
	}

	public bool _IsDetAnaMark
	{
		get
		{
			return DetailListShowAnalysisItemSymbol;
		}
		set
		{
			DetailListShowAnalysisItemSymbol = value;
		}
	}

	public string _DetAnaMark
	{
		get
		{
			return DetailListAnalysisItemMark;
		}
		set
		{
			DetailListAnalysisItemMark = value;
		}
	}

	public bool _IsDetExtCode
	{
		get
		{
			return DetailListShowUnofficialItemCode;
		}
		set
		{
			DetailListShowUnofficialItemCode = value;
		}
	}

	public bool _IsAnaMemo
	{
		get
		{
			return BreakdownListShowRemark;
		}
		set
		{
			BreakdownListShowRemark = value;
		}
	}

	public bool _IsAnaPccesCode
	{
		get
		{
			return BreakdownListShowPccesCode;
		}
		set
		{
			BreakdownListShowPccesCode = value;
		}
	}

	public bool _IsAnaAnaMark
	{
		get
		{
			return BreakdownListShowAnalysisItemSymbol;
		}
		set
		{
			BreakdownListShowAnalysisItemSymbol = value;
		}
	}

	public string _AnaMark
	{
		get
		{
			return BreakdownListAnalysisItemMark;
		}
		set
		{
			BreakdownListAnalysisItemMark = value;
		}
	}

	public bool _IsAnaExtCode
	{
		get
		{
			return BreakdownListShowUnofficialItemCode;
		}
		set
		{
			BreakdownListShowUnofficialItemCode = value;
		}
	}

	public string _AnaSortOrder
	{
		get
		{
			return BreakdownListSortOption;
		}
		set
		{
			BreakdownListSortOption = value;
		}
	}

	public string _AnaSortOrderDet
	{
		get
		{
			return BreakdownListSortItems;
		}
		set
		{
			BreakdownListSortItems = value;
		}
	}

	public string _AnaRepeat
	{
		get
		{
			return BreakdownListDuplicationOption;
		}
		set
		{
			BreakdownListDuplicationOption = value;
		}
	}

	public bool _NoMiddle
	{
		get
		{
			return NoBorderInLineBreak;
		}
		set
		{
			NoBorderInLineBreak = value;
		}
	}

	public bool _IsRepeatDetailAnalysis
	{
		get
		{
			return DuplicateAnalysisItemInDetailList;
		}
		set
		{
			DuplicateAnalysisItemInDetailList = value;
		}
	}

	public bool _IsSkipCommentItem
	{
		get
		{
			return SkipCommentItem;
		}
		set
		{
			SkipCommentItem = value;
		}
	}

	public bool _IsSkipSubtotalItem
	{
		get
		{
			return SkipSubTotalItem;
		}
		set
		{
			SkipSubTotalItem = value;
		}
	}

	public bool _Ismainprice
	{
		get
		{
			return DetailListDisplayMainItemDetail;
		}
		set
		{
			DetailListDisplayMainItemDetail = value;
		}
	}

	public string _ExcelFontName
	{
		get
		{
			return ExcelFontName;
		}
		set
		{
			ExcelFontName = value;
		}
	}

	public bool _WithEnglish
	{
		get
		{
			return WithEnglish;
		}
		set
		{
			WithEnglish = value;
		}
	}

	public bool _PrintDate
	{
		get
		{
			return PrintDate;
		}
		set
		{
			PrintDate = value;
		}
	}

	public DateTime _DatePrinted
	{
		get
		{
			return DatePrinted;
		}
		set
		{
			DatePrinted = value;
		}
	}

	public bool _BudgetPrintPrice
	{
		get
		{
			return BudgetPrintPrice;
		}
		set
		{
			BudgetPrintPrice = value;
		}
	}

	public bool _BidPrintSummary
	{
		get
		{
			return BidPrintSummary;
		}
		set
		{
			BidPrintSummary = value;
		}
	}

	public bool _BidFooterPrintBidder
	{
		get
		{
			return BidFooterPrintBidder;
		}
		set
		{
			BidFooterPrintBidder = value;
		}
	}

	public bool _BidFooterPrintVendorInfo
	{
		get
		{
			return BidFooterPrintVendorInfo;
		}
		set
		{
			BidFooterPrintVendorInfo = value;
		}
	}

	public bool _PrintLaborQty
	{
		get
		{
			return PrintLaborQty;
		}
		set
		{
			PrintLaborQty = value;
		}
	}

	public bool _PrintEquipmentQty
	{
		get
		{
			return PrintEquipmentQty;
		}
		set
		{
			PrintEquipmentQty = value;
		}
	}

	public bool _PrintMaterialQty
	{
		get
		{
			return PrintMaterialQty;
		}
		set
		{
			PrintMaterialQty = value;
		}
	}

	public bool _PrintMiscellaneaQty
	{
		get
		{
			return PrintMiscellaneaQty;
		}
		set
		{
			PrintMiscellaneaQty = value;
		}
	}

	public bool _DetailListShowAnalysisItemCode
	{
		get
		{
			return DetailListShowAnalysisItemCode;
		}
		set
		{
			DetailListShowAnalysisItemCode = value;
		}
	}

	public bool _EnableOldExportExcel
	{
		set
		{
			EnableOldExportExcel = value;
		}
	}

	public bool _DetailListPrintProjectDescription
	{
		set
		{
			DetailListPrintProjectDescription = value;
		}
	}

	public bool _SummaryPrintProjectDescription
	{
		set
		{
			SummaryPrintProjectDescription = value;
		}
	}

	public bool _ShowCodeCorrectRate
	{
		get
		{
			return ShowCodeCorrectRate;
		}
		set
		{
			ShowCodeCorrectRate = value;
		}
	}

	public bool _TakePlaceByMaxValue
	{
		set
		{
			TakePlaceByMaxValue = value;
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetExp_Wzd));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
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
		Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance61 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance62 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance63 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_A1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel9 = new System.Windows.Forms.Panel();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.A1_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.A1_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.A1_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel19 = new Infragistics.Win.Misc.UltraLabel();
		this.chkSubmitIncludeCostBreakdownList = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.ultraLabel18 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.rbOutputBlankBudget = new System.Windows.Forms.RadioButton();
		this.ultraLabel17 = new Infragistics.Win.Misc.UltraLabel();
		this.chkBreakdownListLockCost = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkOutputAliasAsItemName = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkIncludeCostBreakdownList = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.rbOutputBid = new System.Windows.Forms.RadioButton();
		this.rbOutputBudget = new System.Windows.Forms.RadioButton();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel4 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.B_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.btnOpenExcelOption = new Infragistics.Win.Misc.UltraButton();
		this.B_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.B_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.panel3 = new System.Windows.Forms.Panel();
		this.lbl_AutoNumUpd_Warn = new System.Windows.Forms.Label();
		this.groupBox7 = new System.Windows.Forms.GroupBox();
		this.lbl_confirmRate = new System.Windows.Forms.Label();
		this.lbl_WeightFitRatio = new System.Windows.Forms.Label();
		this.lbl_WeightCorrectRatio = new System.Windows.Forms.Label();
		this.lbl_correctRate = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.btnXMLInstruction = new Infragistics.Win.Misc.UltraButton();
		this.llbXMLStandard = new System.Windows.Forms.LinkLabel();
		this.lblWarning2 = new Infragistics.Win.Misc.UltraLabel();
		this.chkXMLformat102 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.gbHeaderAndFooter = new System.Windows.Forms.GroupBox();
		this.ddlBudgetFooter = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ultraLabel20 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel21 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel22 = new Infragistics.Win.Misc.UltraLabel();
		this.tbEnglishHeader = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel23 = new Infragistics.Win.Misc.UltraLabel();
		this.tbHeader = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.chkUseProjectCodeAsFileName = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkOutputZMD = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.btnHeaderAndFooterSetting = new Infragistics.Win.Misc.UltraButton();
		this.txtSaveAsProjectCode = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lblSaveAs = new Infragistics.Win.Misc.UltraLabel();
		this.tbOutputPath = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.chkOutputExcel = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkOutputXML = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.btnOpenOutputFolderBrowser = new Infragistics.Win.Misc.UltraButton();
		this.tbFileName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_C = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel7 = new System.Windows.Forms.Panel();
		this.ultraLabel16 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.progressBarSingle = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
		this.progressBarTotal = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
		this.panel6 = new System.Windows.Forms.Panel();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.C_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.C_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.C_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.lblWait = new Infragistics.Win.Misc.UltraLabel();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_D = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.btnOpenExcel = new Infragistics.Win.Misc.UltraButton();
		this.btnOpenDirectory = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel15 = new Infragistics.Win.Misc.UltraLabel();
		this.panel8 = new System.Windows.Forms.Panel();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.D_Btn_Fnsh = new Infragistics.Win.Misc.UltraButton();
		this.D_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.lbOutputXMLFileName = new Infragistics.Win.Misc.UltraLabel();
		this.lbOutputExcelFileName = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_F = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel12 = new System.Windows.Forms.Panel();
		this.groupBox8 = new System.Windows.Forms.GroupBox();
		this.chkOutputResourceList = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkOutputBreakdownList = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkOutputDetailList = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkOutputSummary = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.ultraLabel30 = new Infragistics.Win.Misc.UltraLabel();
		this.rbPreviewBid = new System.Windows.Forms.RadioButton();
		this.btnPreviewExcelOption = new Infragistics.Win.Misc.UltraButton();
		this.rbPreviewBudget = new System.Windows.Forms.RadioButton();
		this.chkPreviewIncludeCostBreakdownList = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.gbPreviewHeaderAndFooter = new System.Windows.Forms.GroupBox();
		this.ddlPreviewBudgetFooter = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ultraLabel24 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel25 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel26 = new Infragistics.Win.Misc.UltraLabel();
		this.tbPreviewEnglishHeader = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel27 = new Infragistics.Win.Misc.UltraLabel();
		this.tbPreviewHeader = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.panel11 = new System.Windows.Forms.Panel();
		this.ultraLabel28 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel29 = new Infragistics.Win.Misc.UltraLabel();
		this.panel10 = new System.Windows.Forms.Panel();
		this.btnPreview = new Infragistics.Win.Misc.UltraButton();
		this.btnCancel = new Infragistics.Win.Misc.UltraButton();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.OutputFileFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.ultraCheckEditor1 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.timer2 = new System.Windows.Forms.Timer(this.components);
		this.Tab_A1.SuspendLayout();
		this.panel9.SuspendLayout();
		this.Tab_A.SuspendLayout();
		this.panel1.SuspendLayout();
		this.Tab_B.SuspendLayout();
		this.panel4.SuspendLayout();
		this.panel3.SuspendLayout();
		this.groupBox7.SuspendLayout();
		this.gbHeaderAndFooter.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ddlBudgetFooter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbEnglishHeader).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbHeader).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSaveAsProjectCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbOutputPath).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbFileName).BeginInit();
		this.panel2.SuspendLayout();
		this.Tab_C.SuspendLayout();
		this.panel7.SuspendLayout();
		this.panel6.SuspendLayout();
		this.panel5.SuspendLayout();
		this.Tab_D.SuspendLayout();
		this.panel8.SuspendLayout();
		this.Tab_F.SuspendLayout();
		this.panel12.SuspendLayout();
		this.groupBox8.SuspendLayout();
		this.gbPreviewHeaderAndFooter.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ddlPreviewBudgetFooter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbPreviewEnglishHeader).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbPreviewHeader).BeginInit();
		this.panel11.SuspendLayout();
		this.panel10.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A1.Controls.Add(this.panel9);
		this.Tab_A1.Controls.Add(this.ultraLabel19);
		this.Tab_A1.Controls.Add(this.chkSubmitIncludeCostBreakdownList);
		this.Tab_A1.Controls.Add(this.ultraLabel18);
		this.Tab_A1.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_A1.Name = "Tab_A1";
		this.Tab_A1.Size = new System.Drawing.Size(586, 515);
		this.panel9.AutoSize = true;
		this.panel9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panel9.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel9.Controls.Add(this.groupBox5);
		this.panel9.Controls.Add(this.A1_Btn_Cncl);
		this.panel9.Controls.Add(this.A1_Btn_Next);
		this.panel9.Controls.Add(this.A1_Btn_Prev);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel9.Location = new System.Drawing.Point(0, 471);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(586, 44);
		this.panel9.TabIndex = 20;
		this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox5.Location = new System.Drawing.Point(0, 0);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(586, 8);
		this.groupBox5.TabIndex = 3;
		this.groupBox5.TabStop = false;
		this.A1_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A1_Btn_Cncl.Appearance = appearance1;
		this.A1_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A1_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A1_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A1_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A1_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A1_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A1_Btn_Cncl.Location = new System.Drawing.Point(492, 10);
		this.A1_Btn_Cncl.Name = "A1_Btn_Cncl";
		this.A1_Btn_Cncl.ShowFocusRect = false;
		this.A1_Btn_Cncl.ShowOutline = false;
		this.A1_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A1_Btn_Cncl.SupportThemes = false;
		this.A1_Btn_Cncl.TabIndex = 2;
		this.A1_Btn_Cncl.Text = "取消";
		this.A1_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A1_Btn_Next.Appearance = appearance2;
		this.A1_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.A1_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A1_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.A1_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.A1_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.A1_Btn_Next.Location = new System.Drawing.Point(400, 10);
		this.A1_Btn_Next.Name = "A1_Btn_Next";
		this.A1_Btn_Next.ShowFocusRect = false;
		this.A1_Btn_Next.ShowOutline = false;
		this.A1_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.A1_Btn_Next.SupportThemes = false;
		this.A1_Btn_Next.TabIndex = 1;
		this.A1_Btn_Next.Text = "下一步";
		this.A1_Btn_Next.Click += new System.EventHandler(A1_Btn_Next_Click);
		this.A1_Btn_Prev.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A1_Btn_Prev.Appearance = appearance3;
		this.A1_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.A1_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A1_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.A1_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.A1_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.A1_Btn_Prev.Location = new System.Drawing.Point(308, 10);
		this.A1_Btn_Prev.Name = "A1_Btn_Prev";
		this.A1_Btn_Prev.ShowFocusRect = false;
		this.A1_Btn_Prev.ShowOutline = false;
		this.A1_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.A1_Btn_Prev.SupportThemes = false;
		this.A1_Btn_Prev.TabIndex = 0;
		this.A1_Btn_Prev.Text = "上一步";
		this.A1_Btn_Prev.Visible = false;
		appearance4.BackColor = System.Drawing.Color.White;
		this.ultraLabel19.Appearance = appearance4;
		this.ultraLabel19.Location = new System.Drawing.Point(48, 56);
		this.ultraLabel19.Name = "ultraLabel19";
		this.ultraLabel19.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel19.TabIndex = 19;
		this.ultraLabel19.Text = "您要輸出的投標標單是否要包含單價分析";
		appearance5.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance5.FontData.Name = "細明體";
		appearance5.FontData.SizeInPoints = 11f;
		this.chkSubmitIncludeCostBreakdownList.Appearance = appearance5;
		this.chkSubmitIncludeCostBreakdownList.Checked = true;
		this.chkSubmitIncludeCostBreakdownList.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkSubmitIncludeCostBreakdownList.Location = new System.Drawing.Point(76, 96);
		this.chkSubmitIncludeCostBreakdownList.Name = "chkSubmitIncludeCostBreakdownList";
		this.chkSubmitIncludeCostBreakdownList.Size = new System.Drawing.Size(120, 20);
		this.chkSubmitIncludeCostBreakdownList.TabIndex = 18;
		this.chkSubmitIncludeCostBreakdownList.Text = "含單價分析";
		this.chkSubmitIncludeCostBreakdownList.CheckedChanged += new System.EventHandler(chkSubmitIncludeCostBreakdownList_CheckedChanged);
		appearance6.BackColor = System.Drawing.Color.White;
		this.ultraLabel18.Appearance = appearance6;
		this.ultraLabel18.Location = new System.Drawing.Point(12, 15);
		this.ultraLabel18.Name = "ultraLabel18";
		this.ultraLabel18.Size = new System.Drawing.Size(588, 20);
		this.ultraLabel18.TabIndex = 2;
		this.ultraLabel18.Text = "歡迎使用輸出電子檔精靈，接下來我們將引導您一步一步輸出資料";
		this.Tab_A.Controls.Add(this.rbOutputBlankBudget);
		this.Tab_A.Controls.Add(this.ultraLabel17);
		this.Tab_A.Controls.Add(this.chkBreakdownListLockCost);
		this.Tab_A.Controls.Add(this.chkOutputAliasAsItemName);
		this.Tab_A.Controls.Add(this.chkIncludeCostBreakdownList);
		this.Tab_A.Controls.Add(this.ultraLabel4);
		this.Tab_A.Controls.Add(this.ultraLabel3);
		this.Tab_A.Controls.Add(this.rbOutputBid);
		this.Tab_A.Controls.Add(this.rbOutputBudget);
		this.Tab_A.Controls.Add(this.ultraLabel2);
		this.Tab_A.Controls.Add(this.panel1);
		this.Tab_A.Controls.Add(this.ultraLabel1);
		this.Tab_A.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(586, 515);
		this.rbOutputBlankBudget.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.rbOutputBlankBudget.Location = new System.Drawing.Point(199, 92);
		this.rbOutputBlankBudget.Name = "rbOutputBlankBudget";
		this.rbOutputBlankBudget.Size = new System.Drawing.Size(104, 24);
		this.rbOutputBlankBudget.TabIndex = 22;
		this.rbOutputBlankBudget.Text = "空白預算書";
		this.rbOutputBlankBudget.Visible = false;
		appearance7.FontData.Name = "細明體";
		appearance7.FontData.SizeInPoints = 9f;
		this.ultraLabel17.Appearance = appearance7;
		this.ultraLabel17.Location = new System.Drawing.Point(66, 282);
		this.ultraLabel17.Name = "ultraLabel17";
		this.ultraLabel17.Size = new System.Drawing.Size(444, 15);
		this.ultraLabel17.TabIndex = 21;
		this.ultraLabel17.Text = "需於 [檔案] -> [工項名稱「別名」替換設定] 中設定欲以別名取代之工項";
		this.ultraLabel17.Visible = false;
		appearance8.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance8.FontData.Name = "細明體";
		appearance8.FontData.SizeInPoints = 11f;
		this.chkBreakdownListLockCost.Appearance = appearance8;
		this.chkBreakdownListLockCost.Location = new System.Drawing.Point(97, 196);
		this.chkBreakdownListLockCost.Name = "chkBreakdownListLockCost";
		this.chkBreakdownListLockCost.Size = new System.Drawing.Size(424, 20);
		this.chkBreakdownListLockCost.TabIndex = 20;
		this.chkBreakdownListLockCost.Text = "鎖定單價分析表，不允許使用者修改項目，只可填寫單價";
		this.chkBreakdownListLockCost.Visible = false;
		appearance9.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance9.FontData.Name = "細明體";
		appearance9.FontData.SizeInPoints = 11f;
		this.chkOutputAliasAsItemName.Appearance = appearance9;
		this.chkOutputAliasAsItemName.Location = new System.Drawing.Point(48, 258);
		this.chkOutputAliasAsItemName.Name = "chkOutputAliasAsItemName";
		this.chkOutputAliasAsItemName.Size = new System.Drawing.Size(304, 20);
		this.chkOutputAliasAsItemName.TabIndex = 18;
		this.chkOutputAliasAsItemName.Text = "以別名替代工項名稱輸出電子檔";
		this.chkOutputAliasAsItemName.Visible = false;
		appearance10.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance10.FontData.Name = "細明體";
		appearance10.FontData.SizeInPoints = 11f;
		this.chkIncludeCostBreakdownList.Appearance = appearance10;
		this.chkIncludeCostBreakdownList.Checked = true;
		this.chkIncludeCostBreakdownList.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkIncludeCostBreakdownList.Location = new System.Drawing.Point(48, 232);
		this.chkIncludeCostBreakdownList.Name = "chkIncludeCostBreakdownList";
		this.chkIncludeCostBreakdownList.Size = new System.Drawing.Size(288, 20);
		this.chkIncludeCostBreakdownList.TabIndex = 15;
		this.chkIncludeCostBreakdownList.Text = "您要輸出的電子檔是否包含單價分析";
		this.chkIncludeCostBreakdownList.CheckedChanged += new System.EventHandler(chkIncludeCostBreakdownList_CheckedChanged);
		appearance11.FontData.Name = "細明體";
		appearance11.FontData.SizeInPoints = 9f;
		this.ultraLabel4.Appearance = appearance11;
		this.ultraLabel4.Location = new System.Drawing.Point(97, 173);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(444, 23);
		this.ultraLabel4.TabIndex = 14;
		this.ultraLabel4.Text = "輸出空白標單格式，供投標廠商填標之用";
		appearance12.FontData.Name = "細明體";
		appearance12.FontData.SizeInPoints = 9f;
		this.ultraLabel3.Appearance = appearance12;
		this.ultraLabel3.Location = new System.Drawing.Point(96, 120);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(444, 23);
		this.ultraLabel3.TabIndex = 13;
		this.ultraLabel3.Text = "輸出您所編製之預算書內容，包含總表、詳細表、單價分析表、資源統計表";
		this.rbOutputBid.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.rbOutputBid.Location = new System.Drawing.Point(81, 145);
		this.rbOutputBid.Name = "rbOutputBid";
		this.rbOutputBid.Size = new System.Drawing.Size(104, 24);
		this.rbOutputBid.TabIndex = 12;
		this.rbOutputBid.Text = "空白標單";
		this.rbOutputBid.CheckedChanged += new System.EventHandler(rbOutputBid_CheckedChanged);
		this.rbOutputBudget.Checked = true;
		this.rbOutputBudget.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.rbOutputBudget.Location = new System.Drawing.Point(80, 92);
		this.rbOutputBudget.Name = "rbOutputBudget";
		this.rbOutputBudget.Size = new System.Drawing.Size(104, 24);
		this.rbOutputBudget.TabIndex = 11;
		this.rbOutputBudget.TabStop = true;
		this.rbOutputBudget.Text = "預算書";
		appearance13.BackColor = System.Drawing.Color.White;
		this.ultraLabel2.Appearance = appearance13;
		this.ultraLabel2.Location = new System.Drawing.Point(48, 56);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel2.TabIndex = 10;
		this.ultraLabel2.Text = "您要輸出哪一種格式的電子檔?";
		this.panel1.AutoSize = true;
		this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.groupBox1);
		this.panel1.Controls.Add(this.A_Btn_Cncl);
		this.panel1.Controls.Add(this.A_Btn_Next);
		this.panel1.Controls.Add(this.A_Btn_Prev);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 471);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(586, 44);
		this.panel1.TabIndex = 9;
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(586, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.A_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance14.Image = resources.GetObject("appearance14.Image");
		appearance14.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Cncl.Appearance = appearance14;
		this.A_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Cncl.Location = new System.Drawing.Point(492, 10);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.ShowFocusRect = false;
		this.A_Btn_Cncl.ShowOutline = false;
		this.A_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Cncl.SupportThemes = false;
		this.A_Btn_Cncl.TabIndex = 2;
		this.A_Btn_Cncl.Text = "取消";
		this.A_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance15.Image = resources.GetObject("appearance15.Image");
		appearance15.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance15.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Next.Appearance = appearance15;
		this.A_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Next.Location = new System.Drawing.Point(400, 10);
		this.A_Btn_Next.Name = "A_Btn_Next";
		this.A_Btn_Next.ShowFocusRect = false;
		this.A_Btn_Next.ShowOutline = false;
		this.A_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Next.SupportThemes = false;
		this.A_Btn_Next.TabIndex = 1;
		this.A_Btn_Next.Text = "下一步";
		this.A_Btn_Next.Click += new System.EventHandler(A_Btn_Next_Click);
		this.A_Btn_Prev.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance16.Image = resources.GetObject("appearance16.Image");
		appearance16.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Prev.Appearance = appearance16;
		this.A_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Prev.Location = new System.Drawing.Point(308, 10);
		this.A_Btn_Prev.Name = "A_Btn_Prev";
		this.A_Btn_Prev.ShowFocusRect = false;
		this.A_Btn_Prev.ShowOutline = false;
		this.A_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Prev.SupportThemes = false;
		this.A_Btn_Prev.TabIndex = 0;
		this.A_Btn_Prev.Text = "上一步";
		this.A_Btn_Prev.Visible = false;
		appearance17.BackColor = System.Drawing.Color.White;
		this.ultraLabel1.Appearance = appearance17;
		this.ultraLabel1.Location = new System.Drawing.Point(12, 15);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(588, 20);
		this.ultraLabel1.TabIndex = 1;
		this.ultraLabel1.Text = "歡迎使用輸出電子檔精靈，接下來我們將引導您一步一步輸出資料";
		this.Tab_B.Controls.Add(this.panel4);
		this.Tab_B.Controls.Add(this.panel3);
		this.Tab_B.Controls.Add(this.panel2);
		this.Tab_B.Location = new System.Drawing.Point(0, 0);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(586, 515);
		this.panel4.AutoSize = true;
		this.panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panel4.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel4.Controls.Add(this.groupBox2);
		this.panel4.Controls.Add(this.B_Btn_Cncl);
		this.panel4.Controls.Add(this.btnOpenExcelOption);
		this.panel4.Controls.Add(this.B_Btn_Next);
		this.panel4.Controls.Add(this.B_Btn_Prev);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(0, 471);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(586, 44);
		this.panel4.TabIndex = 10;
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(586, 8);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.B_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance18.Image = resources.GetObject("appearance18.Image");
		appearance18.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Cncl.Appearance = appearance18;
		this.B_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.B_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Cncl.Location = new System.Drawing.Point(492, 10);
		this.B_Btn_Cncl.Name = "B_Btn_Cncl";
		this.B_Btn_Cncl.ShowFocusRect = false;
		this.B_Btn_Cncl.ShowOutline = false;
		this.B_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Cncl.SupportThemes = false;
		this.B_Btn_Cncl.TabIndex = 2;
		this.B_Btn_Cncl.Text = "取消";
		this.btnOpenExcelOption.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOpenExcelOption.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnOpenExcelOption.ImageTransparentColor = System.Drawing.Color.White;
		this.btnOpenExcelOption.Location = new System.Drawing.Point(6, 11);
		this.btnOpenExcelOption.Name = "btnOpenExcelOption";
		this.btnOpenExcelOption.ShowFocusRect = false;
		this.btnOpenExcelOption.ShowOutline = false;
		this.btnOpenExcelOption.Size = new System.Drawing.Size(96, 28);
		this.btnOpenExcelOption.SupportThemes = false;
		this.btnOpenExcelOption.TabIndex = 37;
		this.btnOpenExcelOption.Text = "輸出選項...";
		this.btnOpenExcelOption.Click += new System.EventHandler(btnOpenExcepOption_Click);
		this.B_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance19.Image = resources.GetObject("appearance19.Image");
		appearance19.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance19.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Next.Appearance = appearance19;
		this.B_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Next.Location = new System.Drawing.Point(400, 10);
		this.B_Btn_Next.Name = "B_Btn_Next";
		this.B_Btn_Next.ShowFocusRect = false;
		this.B_Btn_Next.ShowOutline = false;
		this.B_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Next.SupportThemes = false;
		this.B_Btn_Next.TabIndex = 1;
		this.B_Btn_Next.Text = "下一步";
		this.B_Btn_Next.Click += new System.EventHandler(B_Btn_Next_Click);
		this.B_Btn_Prev.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance20.Image = resources.GetObject("appearance20.Image");
		appearance20.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Prev.Appearance = appearance20;
		this.B_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Prev.Location = new System.Drawing.Point(308, 10);
		this.B_Btn_Prev.Name = "B_Btn_Prev";
		this.B_Btn_Prev.ShowFocusRect = false;
		this.B_Btn_Prev.ShowOutline = false;
		this.B_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Prev.SupportThemes = false;
		this.B_Btn_Prev.TabIndex = 0;
		this.B_Btn_Prev.Text = "上一步";
		this.B_Btn_Prev.Click += new System.EventHandler(B_Btn_Prev_Click);
		this.panel3.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel3.Controls.Add(this.lbl_AutoNumUpd_Warn);
		this.panel3.Controls.Add(this.groupBox7);
		this.panel3.Controls.Add(this.btnXMLInstruction);
		this.panel3.Controls.Add(this.llbXMLStandard);
		this.panel3.Controls.Add(this.lblWarning2);
		this.panel3.Controls.Add(this.chkXMLformat102);
		this.panel3.Controls.Add(this.gbHeaderAndFooter);
		this.panel3.Controls.Add(this.chkUseProjectCodeAsFileName);
		this.panel3.Controls.Add(this.chkOutputZMD);
		this.panel3.Controls.Add(this.btnHeaderAndFooterSetting);
		this.panel3.Controls.Add(this.txtSaveAsProjectCode);
		this.panel3.Controls.Add(this.lblSaveAs);
		this.panel3.Controls.Add(this.tbOutputPath);
		this.panel3.Controls.Add(this.ultraLabel8);
		this.panel3.Controls.Add(this.ultraLabel5);
		this.panel3.Controls.Add(this.chkOutputExcel);
		this.panel3.Controls.Add(this.chkOutputXML);
		this.panel3.Controls.Add(this.btnOpenOutputFolderBrowser);
		this.panel3.Controls.Add(this.tbFileName);
		this.panel3.Controls.Add(this.ultraLabel10);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 56);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(586, 459);
		this.panel3.TabIndex = 1;
		this.lbl_AutoNumUpd_Warn.ForeColor = System.Drawing.Color.Red;
		this.lbl_AutoNumUpd_Warn.Location = new System.Drawing.Point(591, 178);
		this.lbl_AutoNumUpd_Warn.Name = "lbl_AutoNumUpd_Warn";
		this.lbl_AutoNumUpd_Warn.Size = new System.Drawing.Size(221, 76);
		this.lbl_AutoNumUpd_Warn.TabIndex = 52;
		this.lbl_AutoNumUpd_Warn.Text = "提醒：自動編碼為正確率計算來源，建議先檢查是否有自動編碼可更新並更新後, 再匯出XML電子檔。";
		this.groupBox7.Controls.Add(this.lbl_confirmRate);
		this.groupBox7.Controls.Add(this.lbl_WeightFitRatio);
		this.groupBox7.Controls.Add(this.lbl_WeightCorrectRatio);
		this.groupBox7.Controls.Add(this.lbl_correctRate);
		this.groupBox7.Controls.Add(this.label4);
		this.groupBox7.Controls.Add(this.label3);
		this.groupBox7.Controls.Add(this.label2);
		this.groupBox7.Controls.Add(this.label1);
		this.groupBox7.Location = new System.Drawing.Point(591, 42);
		this.groupBox7.Name = "groupBox7";
		this.groupBox7.Size = new System.Drawing.Size(221, 123);
		this.groupBox7.TabIndex = 51;
		this.groupBox7.TabStop = false;
		this.groupBox7.Text = "編碼正確率";
		this.groupBox7.Visible = false;
		this.lbl_confirmRate.AutoSize = true;
		this.lbl_confirmRate.Location = new System.Drawing.Point(143, 88);
		this.lbl_confirmRate.Name = "lbl_confirmRate";
		this.lbl_confirmRate.Size = new System.Drawing.Size(63, 15);
		this.lbl_confirmRate.TabIndex = 2;
		this.lbl_confirmRate.Text = "100.00%";
		this.lbl_WeightFitRatio.AutoSize = true;
		this.lbl_WeightFitRatio.Location = new System.Drawing.Point(143, 117);
		this.lbl_WeightFitRatio.Name = "lbl_WeightFitRatio";
		this.lbl_WeightFitRatio.Size = new System.Drawing.Size(63, 15);
		this.lbl_WeightFitRatio.TabIndex = 2;
		this.lbl_WeightFitRatio.Text = "100.00%";
		this.lbl_WeightFitRatio.Visible = false;
		this.lbl_WeightCorrectRatio.AutoSize = true;
		this.lbl_WeightCorrectRatio.Location = new System.Drawing.Point(143, 58);
		this.lbl_WeightCorrectRatio.Name = "lbl_WeightCorrectRatio";
		this.lbl_WeightCorrectRatio.Size = new System.Drawing.Size(63, 15);
		this.lbl_WeightCorrectRatio.TabIndex = 2;
		this.lbl_WeightCorrectRatio.Text = "100.00%";
		this.lbl_correctRate.AutoSize = true;
		this.lbl_correctRate.Location = new System.Drawing.Point(143, 28);
		this.lbl_correctRate.Name = "lbl_correctRate";
		this.lbl_correctRate.Size = new System.Drawing.Size(63, 15);
		this.lbl_correctRate.TabIndex = 2;
		this.lbl_correctRate.Text = "100.00%";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(43, 117);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(103, 15);
		this.label4.TabIndex = 1;
		this.label4.Text = "加權符合率：";
		this.label4.Visible = false;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(11, 88);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(135, 15);
		this.label3.TabIndex = 0;
		this.label3.Text = "綱要編碼正確率：";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(43, 59);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(103, 15);
		this.label2.TabIndex = 1;
		this.label2.Text = "加權正確率：";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(74, 30);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(71, 15);
		this.label1.TabIndex = 0;
		this.label1.Text = "正確率：";
		appearance21.FontData.Name = "Arial";
		appearance21.FontData.SizeInPoints = 8f;
		this.btnXMLInstruction.Appearance = appearance21;
		this.btnXMLInstruction.BackColor = System.Drawing.SystemColors.Control;
		this.btnXMLInstruction.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnXMLInstruction.Location = new System.Drawing.Point(404, 77);
		this.btnXMLInstruction.Name = "btnXMLInstruction";
		this.btnXMLInstruction.ShowFocusRect = false;
		this.btnXMLInstruction.ShowOutline = false;
		this.btnXMLInstruction.Size = new System.Drawing.Size(52, 24);
		this.btnXMLInstruction.SupportThemes = false;
		this.btnXMLInstruction.TabIndex = 47;
		this.btnXMLInstruction.Text = "說明...";
		this.btnXMLInstruction.Visible = false;
		this.btnXMLInstruction.Click += new System.EventHandler(btnXMLInstruction_Click);
		this.llbXMLStandard.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.llbXMLStandard.Location = new System.Drawing.Point(272, 82);
		this.llbXMLStandard.Name = "llbXMLStandard";
		this.llbXMLStandard.Size = new System.Drawing.Size(129, 15);
		this.llbXMLStandard.TabIndex = 46;
		((System.Windows.Forms.Label)this.llbXMLStandard).TabStop = true;
		this.llbXMLStandard.Text = "公共工程資料交換標準";
		this.llbXMLStandard.Visible = false;
		this.llbXMLStandard.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(llbXMLStandard_LinkClicked);
		appearance22.ForeColor = System.Drawing.Color.Red;
		this.lblWarning2.Appearance = appearance22;
		this.lblWarning2.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblWarning2.Location = new System.Drawing.Point(109, 83);
		this.lblWarning2.Name = "lblWarning2";
		this.lblWarning2.Size = new System.Drawing.Size(165, 17);
		this.lblWarning2.TabIndex = 45;
		this.lblWarning2.Text = "此專案目前的取位原則不符合";
		this.lblWarning2.Visible = false;
		appearance23.FontData.SizeInPoints = 9f;
		this.chkXMLformat102.Appearance = appearance23;
		this.chkXMLformat102.Checked = true;
		this.chkXMLformat102.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkXMLformat102.Enabled = false;
		this.chkXMLformat102.Location = new System.Drawing.Point(111, 61);
		this.chkXMLformat102.Name = "chkXMLformat102";
		this.chkXMLformat102.Size = new System.Drawing.Size(268, 24);
		this.chkXMLformat102.TabIndex = 50;
		this.chkXMLformat102.Text = "使用民國102年xml格式";
		this.chkXMLformat102.Visible = false;
		this.gbHeaderAndFooter.Controls.Add(this.ddlBudgetFooter);
		this.gbHeaderAndFooter.Controls.Add(this.ultraLabel20);
		this.gbHeaderAndFooter.Controls.Add(this.ultraLabel21);
		this.gbHeaderAndFooter.Controls.Add(this.ultraLabel22);
		this.gbHeaderAndFooter.Controls.Add(this.tbEnglishHeader);
		this.gbHeaderAndFooter.Controls.Add(this.ultraLabel23);
		this.gbHeaderAndFooter.Controls.Add(this.tbHeader);
		this.gbHeaderAndFooter.Location = new System.Drawing.Point(75, 138);
		this.gbHeaderAndFooter.Name = "gbHeaderAndFooter";
		this.gbHeaderAndFooter.Size = new System.Drawing.Size(450, 177);
		this.gbHeaderAndFooter.TabIndex = 41;
		this.gbHeaderAndFooter.TabStop = false;
		this.gbHeaderAndFooter.Text = "表頭及表尾";
		this.gbHeaderAndFooter.Visible = false;
		appearance24.FontData.Name = "細明體";
		appearance24.FontData.SizeInPoints = 11f;
		this.ddlBudgetFooter.Appearance = appearance24;
		this.ddlBudgetFooter.AutoSize = true;
		this.ddlBudgetFooter.Location = new System.Drawing.Point(12, 146);
		this.ddlBudgetFooter.Name = "ddlBudgetFooter";
		this.ddlBudgetFooter.Size = new System.Drawing.Size(428, 24);
		this.ddlBudgetFooter.TabIndex = 22;
		this.ddlBudgetFooter.Text = null;
		appearance25.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance25.ForeColor = System.Drawing.Color.Green;
		this.ultraLabel20.Appearance = appearance25;
		this.ultraLabel20.Location = new System.Drawing.Point(96, 127);
		this.ultraLabel20.Name = "ultraLabel20";
		this.ultraLabel20.Size = new System.Drawing.Size(227, 23);
		this.ultraLabel20.TabIndex = 23;
		this.ultraLabel20.Text = "(不適用於中英文並列格式)";
		this.ultraLabel21.Location = new System.Drawing.Point(12, 126);
		this.ultraLabel21.Name = "ultraLabel21";
		this.ultraLabel21.Size = new System.Drawing.Size(84, 23);
		this.ultraLabel21.TabIndex = 5;
		this.ultraLabel21.Text = "預算表尾:";
		this.ultraLabel22.Location = new System.Drawing.Point(8, 72);
		this.ultraLabel22.Name = "ultraLabel22";
		this.ultraLabel22.Size = new System.Drawing.Size(176, 14);
		this.ultraLabel22.TabIndex = 3;
		this.ultraLabel22.Text = "機關 / 公司英文名稱:";
		appearance26.FontData.Name = "細明體";
		appearance26.FontData.SizeInPoints = 11f;
		this.tbEnglishHeader.Appearance = appearance26;
		this.tbEnglishHeader.AutoSize = true;
		this.tbEnglishHeader.Location = new System.Drawing.Point(12, 92);
		this.tbEnglishHeader.Name = "tbEnglishHeader";
		this.tbEnglishHeader.Size = new System.Drawing.Size(428, 24);
		this.tbEnglishHeader.TabIndex = 2;
		this.ultraLabel23.Location = new System.Drawing.Point(8, 20);
		this.ultraLabel23.Name = "ultraLabel23";
		this.ultraLabel23.Size = new System.Drawing.Size(180, 16);
		this.ultraLabel23.TabIndex = 1;
		this.ultraLabel23.Text = "機關 / 公司名稱:";
		appearance27.FontData.Name = "細明體";
		appearance27.FontData.SizeInPoints = 11f;
		this.tbHeader.Appearance = appearance27;
		this.tbHeader.AutoSize = true;
		this.tbHeader.Location = new System.Drawing.Point(12, 40);
		this.tbHeader.Name = "tbHeader";
		this.tbHeader.Size = new System.Drawing.Size(428, 24);
		this.tbHeader.TabIndex = 0;
		this.chkUseProjectCodeAsFileName.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.chkUseProjectCodeAsFileName.Checked = true;
		this.chkUseProjectCodeAsFileName.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkUseProjectCodeAsFileName.Location = new System.Drawing.Point(87, 395);
		this.chkUseProjectCodeAsFileName.Name = "chkUseProjectCodeAsFileName";
		this.chkUseProjectCodeAsFileName.Size = new System.Drawing.Size(292, 20);
		this.chkUseProjectCodeAsFileName.TabIndex = 49;
		this.chkUseProjectCodeAsFileName.Text = "使用專案名稱為輸出的檔案名稱";
		this.chkUseProjectCodeAsFileName.CheckedChanged += new System.EventHandler(chkUseProjectCodeAsFileName_CheckedChanged);
		this.chkOutputZMD.Location = new System.Drawing.Point(88, 12);
		this.chkOutputZMD.Name = "chkOutputZMD";
		this.chkOutputZMD.Size = new System.Drawing.Size(346, 24);
		this.chkOutputZMD.TabIndex = 48;
		this.chkOutputZMD.Text = "工程會 PCCES 內部交換格式(*.zmd)";
		appearance28.FontData.Name = "Arial";
		appearance28.FontData.SizeInPoints = 8f;
		this.btnHeaderAndFooterSetting.Appearance = appearance28;
		this.btnHeaderAndFooterSetting.BackColor = System.Drawing.SystemColors.Control;
		this.btnHeaderAndFooterSetting.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnHeaderAndFooterSetting.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnHeaderAndFooterSetting.Location = new System.Drawing.Point(323, 106);
		this.btnHeaderAndFooterSetting.Name = "btnHeaderAndFooterSetting";
		this.btnHeaderAndFooterSetting.ShowFocusRect = false;
		this.btnHeaderAndFooterSetting.ShowOutline = false;
		this.btnHeaderAndFooterSetting.Size = new System.Drawing.Size(100, 22);
		this.btnHeaderAndFooterSetting.SupportThemes = false;
		this.btnHeaderAndFooterSetting.TabIndex = 42;
		this.btnHeaderAndFooterSetting.Text = "表頭表尾設定▼";
		this.btnHeaderAndFooterSetting.Visible = false;
		this.btnHeaderAndFooterSetting.Click += new System.EventHandler(btnHeaderAndFooterSetting_Click);
		this.txtSaveAsProjectCode.AutoSize = true;
		this.txtSaveAsProjectCode.Location = new System.Drawing.Point(478, 39);
		this.txtSaveAsProjectCode.MaxLength = 40;
		this.txtSaveAsProjectCode.Name = "txtSaveAsProjectCode";
		this.txtSaveAsProjectCode.Size = new System.Drawing.Size(96, 21);
		this.txtSaveAsProjectCode.TabIndex = 38;
		this.txtSaveAsProjectCode.Visible = false;
		appearance29.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblSaveAs.Appearance = appearance29;
		this.lblSaveAs.Location = new System.Drawing.Point(361, 42);
		this.lblSaveAs.Name = "lblSaveAs";
		this.lblSaveAs.Size = new System.Drawing.Size(114, 23);
		this.lblSaveAs.TabIndex = 37;
		this.lblSaveAs.Text = "另存專案代碼：";
		this.lblSaveAs.Visible = false;
		this.tbOutputPath.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		appearance30.FontData.Name = "細明體";
		appearance30.FontData.SizeInPoints = 11f;
		this.tbOutputPath.Appearance = appearance30;
		this.tbOutputPath.AutoSize = true;
		this.tbOutputPath.Location = new System.Drawing.Point(88, 334);
		this.tbOutputPath.Name = "tbOutputPath";
		this.tbOutputPath.Size = new System.Drawing.Size(404, 24);
		this.tbOutputPath.TabIndex = 15;
		this.tbOutputPath.Validating += new System.ComponentModel.CancelEventHandler(tbOutputPath_Validating);
		this.ultraLabel8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		appearance31.TextHAlign = Infragistics.Win.HAlign.Right;
		this.ultraLabel8.Appearance = appearance31;
		this.ultraLabel8.Location = new System.Drawing.Point(6, 340);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(84, 23);
		this.ultraLabel8.TabIndex = 14;
		this.ultraLabel8.Text = "存放路徑：";
		appearance32.TextHAlign = Infragistics.Win.HAlign.Right;
		this.ultraLabel5.Appearance = appearance32;
		this.ultraLabel5.Location = new System.Drawing.Point(26, 17);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(57, 23);
		this.ultraLabel5.TabIndex = 13;
		this.ultraLabel5.Text = "格式：";
		this.chkOutputExcel.Location = new System.Drawing.Point(88, 106);
		this.chkOutputExcel.Name = "chkOutputExcel";
		this.chkOutputExcel.Size = new System.Drawing.Size(268, 24);
		this.chkOutputExcel.TabIndex = 12;
		this.chkOutputExcel.Text = "Microsoft Excel 格式(*.xls)";
		this.chkOutputExcel.CheckedChanged += new System.EventHandler(chkOutputExcel_CheckedChanged);
		this.chkOutputXML.Location = new System.Drawing.Point(88, 42);
		this.chkOutputXML.Name = "chkOutputXML";
		this.chkOutputXML.Size = new System.Drawing.Size(287, 24);
		this.chkOutputXML.TabIndex = 11;
		this.chkOutputXML.Text = "工程會電子標單檔 xml 格式(*.xml)";
		this.chkOutputXML.CheckedChanged += new System.EventHandler(chkOutputXML_CheckedChanged);
		this.btnOpenOutputFolderBrowser.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		appearance33.FontData.Name = "Arial";
		appearance33.FontData.SizeInPoints = 8f;
		this.btnOpenOutputFolderBrowser.Appearance = appearance33;
		this.btnOpenOutputFolderBrowser.BackColor = System.Drawing.SystemColors.Control;
		this.btnOpenOutputFolderBrowser.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOpenOutputFolderBrowser.Location = new System.Drawing.Point(496, 334);
		this.btnOpenOutputFolderBrowser.Name = "btnOpenOutputFolderBrowser";
		this.btnOpenOutputFolderBrowser.ShowFocusRect = false;
		this.btnOpenOutputFolderBrowser.ShowOutline = false;
		this.btnOpenOutputFolderBrowser.Size = new System.Drawing.Size(48, 24);
		this.btnOpenOutputFolderBrowser.SupportThemes = false;
		this.btnOpenOutputFolderBrowser.TabIndex = 9;
		this.btnOpenOutputFolderBrowser.Text = "瀏覽...";
		this.btnOpenOutputFolderBrowser.Click += new System.EventHandler(btnOpenOutputFolderBrowser_Click);
		this.tbFileName.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		appearance34.FontData.Name = "細明體";
		appearance34.FontData.SizeInPoints = 11f;
		this.tbFileName.Appearance = appearance34;
		this.tbFileName.AutoSize = true;
		this.tbFileName.Location = new System.Drawing.Point(88, 365);
		this.tbFileName.Name = "tbFileName";
		this.tbFileName.Size = new System.Drawing.Size(404, 24);
		this.tbFileName.TabIndex = 8;
		this.tbFileName.Validating += new System.ComponentModel.CancelEventHandler(tbOutputPath_Validating);
		this.ultraLabel10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		appearance35.TextHAlign = Infragistics.Win.HAlign.Right;
		this.ultraLabel10.Appearance = appearance35;
		this.ultraLabel10.Location = new System.Drawing.Point(6, 371);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(84, 23);
		this.ultraLabel10.TabIndex = 7;
		this.ultraLabel10.Text = "檔案名稱：";
		this.panel2.Controls.Add(this.ultraLabel7);
		this.panel2.Controls.Add(this.ultraLabel6);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(586, 56);
		this.panel2.TabIndex = 0;
		appearance36.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance36;
		this.ultraLabel7.Location = new System.Drawing.Point(38, 31);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel7.TabIndex = 4;
		this.ultraLabel7.Text = "您可以挑選所需的格式，並設定您要存放的目錄";
		appearance37.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance37;
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(24, 10);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 3;
		this.ultraLabel6.Text = "輸出格式";
		this.Tab_C.Controls.Add(this.panel7);
		this.Tab_C.Controls.Add(this.panel5);
		this.Tab_C.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_C.Name = "Tab_C";
		this.Tab_C.Size = new System.Drawing.Size(586, 515);
		this.panel7.BackColor = System.Drawing.Color.White;
		this.panel7.Controls.Add(this.ultraLabel16);
		this.panel7.Controls.Add(this.ultraLabel9);
		this.panel7.Controls.Add(this.progressBarSingle);
		this.panel7.Controls.Add(this.progressBarTotal);
		this.panel7.Controls.Add(this.panel6);
		this.panel7.Controls.Add(this.lblWait);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel7.Location = new System.Drawing.Point(0, 56);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(586, 459);
		this.panel7.TabIndex = 12;
		this.ultraLabel16.Location = new System.Drawing.Point(30, 140);
		this.ultraLabel16.Name = "ultraLabel16";
		this.ultraLabel16.Size = new System.Drawing.Size(87, 23);
		this.ultraLabel16.TabIndex = 23;
		this.ultraLabel16.Text = "單一進度：";
		this.ultraLabel9.Location = new System.Drawing.Point(30, 103);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(87, 23);
		this.ultraLabel9.TabIndex = 22;
		this.ultraLabel9.Text = "整體進度：";
		appearance38.BackColor = System.Drawing.Color.White;
		appearance38.BackColor2 = System.Drawing.Color.White;
		this.progressBarSingle.Appearance = appearance38;
		appearance39.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
		appearance39.BackColor2 = System.Drawing.Color.Navy;
		appearance39.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		this.progressBarSingle.FillAppearance = appearance39;
		this.progressBarSingle.Location = new System.Drawing.Point(116, 136);
		this.progressBarSingle.Name = "progressBarSingle";
		this.progressBarSingle.Size = new System.Drawing.Size(428, 23);
		this.progressBarSingle.SupportThemes = false;
		this.progressBarSingle.TabIndex = 21;
		this.progressBarSingle.Text = "[Formatted]";
		appearance40.BackColor = System.Drawing.Color.White;
		appearance40.BackColor2 = System.Drawing.Color.White;
		this.progressBarTotal.Appearance = appearance40;
		appearance41.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
		appearance41.BackColor2 = System.Drawing.Color.Navy;
		appearance41.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		this.progressBarTotal.FillAppearance = appearance41;
		this.progressBarTotal.Location = new System.Drawing.Point(116, 98);
		this.progressBarTotal.Name = "progressBarTotal";
		this.progressBarTotal.Size = new System.Drawing.Size(428, 23);
		this.progressBarTotal.SupportThemes = false;
		this.progressBarTotal.TabIndex = 20;
		this.progressBarTotal.Text = "[Formatted]";
		this.panel6.AutoSize = true;
		this.panel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panel6.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel6.Controls.Add(this.groupBox3);
		this.panel6.Controls.Add(this.C_Btn_Cncl);
		this.panel6.Controls.Add(this.C_Btn_Next);
		this.panel6.Controls.Add(this.C_Btn_Prev);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel6.Location = new System.Drawing.Point(0, 415);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(586, 44);
		this.panel6.TabIndex = 19;
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox3.Location = new System.Drawing.Point(0, 0);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(586, 8);
		this.groupBox3.TabIndex = 3;
		this.groupBox3.TabStop = false;
		appearance42.Image = resources.GetObject("appearance42.Image");
		appearance42.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.C_Btn_Cncl.Appearance = appearance42;
		this.C_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.C_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.C_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.C_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.C_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.C_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.C_Btn_Cncl.Location = new System.Drawing.Point(492, 10);
		this.C_Btn_Cncl.Name = "C_Btn_Cncl";
		this.C_Btn_Cncl.ShowFocusRect = false;
		this.C_Btn_Cncl.ShowOutline = false;
		this.C_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.C_Btn_Cncl.SupportThemes = false;
		this.C_Btn_Cncl.TabIndex = 2;
		this.C_Btn_Cncl.Text = "取消";
		this.C_Btn_Cncl.Visible = false;
		appearance43.Image = resources.GetObject("appearance43.Image");
		appearance43.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance43.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.C_Btn_Next.Appearance = appearance43;
		this.C_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.C_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.C_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.C_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.C_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.C_Btn_Next.Location = new System.Drawing.Point(400, 10);
		this.C_Btn_Next.Name = "C_Btn_Next";
		this.C_Btn_Next.ShowFocusRect = false;
		this.C_Btn_Next.ShowOutline = false;
		this.C_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.C_Btn_Next.SupportThemes = false;
		this.C_Btn_Next.TabIndex = 1;
		this.C_Btn_Next.Text = "下一步";
		this.C_Btn_Next.Visible = false;
		appearance44.Image = resources.GetObject("appearance44.Image");
		appearance44.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.C_Btn_Prev.Appearance = appearance44;
		this.C_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.C_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.C_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.C_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.C_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.C_Btn_Prev.Location = new System.Drawing.Point(308, 10);
		this.C_Btn_Prev.Name = "C_Btn_Prev";
		this.C_Btn_Prev.ShowFocusRect = false;
		this.C_Btn_Prev.ShowOutline = false;
		this.C_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.C_Btn_Prev.SupportThemes = false;
		this.C_Btn_Prev.TabIndex = 0;
		this.C_Btn_Prev.Text = "上一步";
		this.C_Btn_Prev.Visible = false;
		this.lblWait.Location = new System.Drawing.Point(30, 19);
		this.lblWait.Name = "lblWait";
		this.lblWait.Size = new System.Drawing.Size(476, 20);
		this.lblWait.TabIndex = 18;
		this.lblWait.Text = "正在準備匯出的資料，這個動作會花些時間，請稍候。";
		this.panel5.Controls.Add(this.ultraLabel11);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(586, 56);
		this.panel5.TabIndex = 1;
		appearance45.BackColor = System.Drawing.Color.White;
		this.ultraLabel11.Appearance = appearance45;
		this.ultraLabel11.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel11.Location = new System.Drawing.Point(30, 21);
		this.ultraLabel11.Name = "ultraLabel11";
		this.ultraLabel11.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel11.TabIndex = 3;
		this.ultraLabel11.Text = "輸出檔案中...";
		this.Tab_D.Controls.Add(this.btnOpenExcel);
		this.Tab_D.Controls.Add(this.btnOpenDirectory);
		this.Tab_D.Controls.Add(this.ultraLabel15);
		this.Tab_D.Controls.Add(this.panel8);
		this.Tab_D.Controls.Add(this.ultraLabel14);
		this.Tab_D.Controls.Add(this.ultraLabel13);
		this.Tab_D.Controls.Add(this.ultraLabel12);
		this.Tab_D.Controls.Add(this.lbOutputXMLFileName);
		this.Tab_D.Controls.Add(this.lbOutputExcelFileName);
		this.Tab_D.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_D.Name = "Tab_D";
		this.Tab_D.Size = new System.Drawing.Size(586, 515);
		appearance46.FontData.Name = "Arial";
		appearance46.FontData.SizeInPoints = 8f;
		this.btnOpenExcel.Appearance = appearance46;
		this.btnOpenExcel.BackColor = System.Drawing.SystemColors.Control;
		this.btnOpenExcel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOpenExcel.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnOpenExcel.Location = new System.Drawing.Point(39, 136);
		this.btnOpenExcel.Name = "btnOpenExcel";
		this.btnOpenExcel.ShowFocusRect = false;
		this.btnOpenExcel.ShowOutline = false;
		this.btnOpenExcel.Size = new System.Drawing.Size(88, 24);
		this.btnOpenExcel.SupportThemes = false;
		this.btnOpenExcel.TabIndex = 21;
		this.btnOpenExcel.Text = "直接開啟：";
		this.btnOpenExcel.Visible = false;
		this.btnOpenExcel.Click += new System.EventHandler(btnOpenExcel_Click);
		appearance47.FontData.Name = "Arial";
		appearance47.FontData.SizeInPoints = 8f;
		this.btnOpenDirectory.Appearance = appearance47;
		this.btnOpenDirectory.BackColor = System.Drawing.SystemColors.Control;
		this.btnOpenDirectory.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOpenDirectory.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnOpenDirectory.Location = new System.Drawing.Point(200, 181);
		this.btnOpenDirectory.Name = "btnOpenDirectory";
		this.btnOpenDirectory.ShowFocusRect = false;
		this.btnOpenDirectory.ShowOutline = false;
		this.btnOpenDirectory.Size = new System.Drawing.Size(88, 24);
		this.btnOpenDirectory.SupportThemes = false;
		this.btnOpenDirectory.TabIndex = 20;
		this.btnOpenDirectory.Text = "開啟資料夾";
		this.btnOpenDirectory.Click += new System.EventHandler(btnOpenDirectory_Click);
		this.ultraLabel15.Location = new System.Drawing.Point(39, 187);
		this.ultraLabel15.Name = "ultraLabel15";
		this.ultraLabel15.Size = new System.Drawing.Size(168, 23);
		this.ultraLabel15.TabIndex = 18;
		this.ultraLabel15.Text = "輸出路徑及檔案名稱:";
		this.panel8.AutoSize = true;
		this.panel8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panel8.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel8.Controls.Add(this.groupBox4);
		this.panel8.Controls.Add(this.D_Btn_Fnsh);
		this.panel8.Controls.Add(this.D_Btn_Prev);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel8.Location = new System.Drawing.Point(0, 471);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(586, 44);
		this.panel8.TabIndex = 17;
		this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox4.Location = new System.Drawing.Point(0, 0);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(586, 8);
		this.groupBox4.TabIndex = 3;
		this.groupBox4.TabStop = false;
		appearance48.Image = resources.GetObject("appearance48.Image");
		appearance48.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Fnsh.Appearance = appearance48;
		this.D_Btn_Fnsh.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Fnsh.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Fnsh.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.D_Btn_Fnsh.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Fnsh.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Fnsh.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Fnsh.Location = new System.Drawing.Point(491, 10);
		this.D_Btn_Fnsh.Name = "D_Btn_Fnsh";
		this.D_Btn_Fnsh.ShowFocusRect = false;
		this.D_Btn_Fnsh.ShowOutline = false;
		this.D_Btn_Fnsh.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Fnsh.SupportThemes = false;
		this.D_Btn_Fnsh.TabIndex = 1;
		this.D_Btn_Fnsh.Text = "完成";
		this.D_Btn_Fnsh.Click += new System.EventHandler(D_Btn_Fnsh_Click);
		appearance49.Image = resources.GetObject("appearance49.Image");
		appearance49.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Prev.Appearance = appearance49;
		this.D_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Prev.Location = new System.Drawing.Point(399, 10);
		this.D_Btn_Prev.Name = "D_Btn_Prev";
		this.D_Btn_Prev.ShowFocusRect = false;
		this.D_Btn_Prev.ShowOutline = false;
		this.D_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Prev.SupportThemes = false;
		this.D_Btn_Prev.TabIndex = 0;
		this.D_Btn_Prev.Text = "上一步";
		this.D_Btn_Prev.Visible = false;
		this.D_Btn_Prev.Click += new System.EventHandler(D_Btn_Prev_Click);
		appearance50.BackColor = System.Drawing.Color.White;
		this.ultraLabel14.Appearance = appearance50;
		this.ultraLabel14.Location = new System.Drawing.Point(40, 98);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel14.TabIndex = 16;
		this.ultraLabel14.Text = "若要結束精靈，請按一下[完成]。";
		appearance51.BackColor = System.Drawing.Color.White;
		this.ultraLabel13.Appearance = appearance51;
		this.ultraLabel13.Location = new System.Drawing.Point(40, 72);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel13.TabIndex = 15;
		this.ultraLabel13.Text = "你已經成功匯出資料。";
		appearance52.BackColor = System.Drawing.Color.White;
		this.ultraLabel12.Appearance = appearance52;
		this.ultraLabel12.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel12.Location = new System.Drawing.Point(24, 28);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel12.TabIndex = 14;
		this.ultraLabel12.Text = "恭禧您!";
		this.lbOutputXMLFileName.Location = new System.Drawing.Point(40, 211);
		this.lbOutputXMLFileName.Name = "lbOutputXMLFileName";
		this.lbOutputXMLFileName.Size = new System.Drawing.Size(488, 76);
		this.lbOutputXMLFileName.TabIndex = 19;
		this.lbOutputXMLFileName.Text = "[]";
		appearance53.ForeColor = System.Drawing.Color.Red;
		appearance53.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbOutputExcelFileName.Appearance = appearance53;
		this.lbOutputExcelFileName.Location = new System.Drawing.Point(135, 119);
		this.lbOutputExcelFileName.Name = "lbOutputExcelFileName";
		this.lbOutputExcelFileName.Size = new System.Drawing.Size(392, 57);
		this.lbOutputExcelFileName.TabIndex = 19;
		this.lbOutputExcelFileName.Visible = false;
		this.Tab_F.Controls.Add(this.panel12);
		this.Tab_F.Controls.Add(this.panel11);
		this.Tab_F.Controls.Add(this.panel10);
		this.Tab_F.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_F.Name = "Tab_F";
		this.Tab_F.Size = new System.Drawing.Size(586, 515);
		this.panel12.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel12.Controls.Add(this.groupBox8);
		this.panel12.Controls.Add(this.ultraLabel30);
		this.panel12.Controls.Add(this.rbPreviewBid);
		this.panel12.Controls.Add(this.btnPreviewExcelOption);
		this.panel12.Controls.Add(this.rbPreviewBudget);
		this.panel12.Controls.Add(this.chkPreviewIncludeCostBreakdownList);
		this.panel12.Controls.Add(this.gbPreviewHeaderAndFooter);
		this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel12.Location = new System.Drawing.Point(0, 56);
		this.panel12.Name = "panel12";
		this.panel12.Size = new System.Drawing.Size(586, 415);
		this.panel12.TabIndex = 45;
		this.groupBox8.Controls.Add(this.chkOutputResourceList);
		this.groupBox8.Controls.Add(this.chkOutputBreakdownList);
		this.groupBox8.Controls.Add(this.chkOutputDetailList);
		this.groupBox8.Controls.Add(this.chkOutputSummary);
		this.groupBox8.Location = new System.Drawing.Point(318, 20);
		this.groupBox8.Name = "groupBox8";
		this.groupBox8.Size = new System.Drawing.Size(229, 85);
		this.groupBox8.TabIndex = 47;
		this.groupBox8.TabStop = false;
		this.groupBox8.Text = "報表挑選";
		this.chkOutputResourceList.Checked = true;
		this.chkOutputResourceList.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkOutputResourceList.Location = new System.Drawing.Point(114, 54);
		this.chkOutputResourceList.Name = "chkOutputResourceList";
		this.chkOutputResourceList.Size = new System.Drawing.Size(109, 20);
		this.chkOutputResourceList.TabIndex = 7;
		this.chkOutputResourceList.Text = "資源統計表";
		this.chkOutputBreakdownList.Checked = true;
		this.chkOutputBreakdownList.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkOutputBreakdownList.Location = new System.Drawing.Point(8, 54);
		this.chkOutputBreakdownList.Name = "chkOutputBreakdownList";
		this.chkOutputBreakdownList.Size = new System.Drawing.Size(100, 20);
		this.chkOutputBreakdownList.TabIndex = 6;
		this.chkOutputBreakdownList.Text = "單價分析表";
		this.chkOutputDetailList.Checked = true;
		this.chkOutputDetailList.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkOutputDetailList.Location = new System.Drawing.Point(114, 28);
		this.chkOutputDetailList.Name = "chkOutputDetailList";
		this.chkOutputDetailList.Size = new System.Drawing.Size(96, 20);
		this.chkOutputDetailList.TabIndex = 5;
		this.chkOutputDetailList.Text = "詳細表";
		this.chkOutputSummary.Checked = true;
		this.chkOutputSummary.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkOutputSummary.Location = new System.Drawing.Point(8, 28);
		this.chkOutputSummary.Name = "chkOutputSummary";
		this.chkOutputSummary.Size = new System.Drawing.Size(96, 20);
		this.chkOutputSummary.TabIndex = 4;
		this.chkOutputSummary.Text = "總表";
		appearance54.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraLabel30.Appearance = appearance54;
		this.ultraLabel30.Location = new System.Drawing.Point(24, 20);
		this.ultraLabel30.Name = "ultraLabel30";
		this.ultraLabel30.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel30.TabIndex = 46;
		this.ultraLabel30.Text = "您要預覽哪一種格式報表？";
		this.rbPreviewBid.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.rbPreviewBid.Location = new System.Drawing.Point(134, 46);
		this.rbPreviewBid.Name = "rbPreviewBid";
		this.rbPreviewBid.Size = new System.Drawing.Size(104, 24);
		this.rbPreviewBid.TabIndex = 45;
		this.rbPreviewBid.Text = "空白標單";
		this.rbPreviewBid.CheckedChanged += new System.EventHandler(rbPreviewBid_CheckedChanged);
		this.btnPreviewExcelOption.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnPreviewExcelOption.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnPreviewExcelOption.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnPreviewExcelOption.ImageTransparentColor = System.Drawing.Color.White;
		this.btnPreviewExcelOption.Location = new System.Drawing.Point(24, 380);
		this.btnPreviewExcelOption.Name = "btnPreviewExcelOption";
		this.btnPreviewExcelOption.ShowFocusRect = false;
		this.btnPreviewExcelOption.ShowOutline = false;
		this.btnPreviewExcelOption.Size = new System.Drawing.Size(96, 28);
		this.btnPreviewExcelOption.SupportThemes = false;
		this.btnPreviewExcelOption.TabIndex = 38;
		this.btnPreviewExcelOption.Text = "報表選項...";
		this.btnPreviewExcelOption.Click += new System.EventHandler(btnPreviewExcelOption_Click);
		this.rbPreviewBudget.Checked = true;
		this.rbPreviewBudget.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.rbPreviewBudget.Location = new System.Drawing.Point(38, 46);
		this.rbPreviewBudget.Name = "rbPreviewBudget";
		this.rbPreviewBudget.Size = new System.Drawing.Size(104, 24);
		this.rbPreviewBudget.TabIndex = 44;
		this.rbPreviewBudget.TabStop = true;
		this.rbPreviewBudget.Text = "預算書";
		this.rbPreviewBudget.CheckedChanged += new System.EventHandler(rbPreviewBudget_CheckedChanged);
		appearance55.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance55.FontData.Name = "細明體";
		appearance55.FontData.SizeInPoints = 11f;
		this.chkPreviewIncludeCostBreakdownList.Appearance = appearance55;
		this.chkPreviewIncludeCostBreakdownList.Checked = true;
		this.chkPreviewIncludeCostBreakdownList.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkPreviewIncludeCostBreakdownList.Location = new System.Drawing.Point(24, 85);
		this.chkPreviewIncludeCostBreakdownList.Name = "chkPreviewIncludeCostBreakdownList";
		this.chkPreviewIncludeCostBreakdownList.Size = new System.Drawing.Size(288, 20);
		this.chkPreviewIncludeCostBreakdownList.TabIndex = 43;
		this.chkPreviewIncludeCostBreakdownList.Text = "您要預覽的報表是否包含單價分析";
		this.gbPreviewHeaderAndFooter.Controls.Add(this.ddlPreviewBudgetFooter);
		this.gbPreviewHeaderAndFooter.Controls.Add(this.ultraLabel24);
		this.gbPreviewHeaderAndFooter.Controls.Add(this.ultraLabel25);
		this.gbPreviewHeaderAndFooter.Controls.Add(this.ultraLabel26);
		this.gbPreviewHeaderAndFooter.Controls.Add(this.tbPreviewEnglishHeader);
		this.gbPreviewHeaderAndFooter.Controls.Add(this.ultraLabel27);
		this.gbPreviewHeaderAndFooter.Controls.Add(this.tbPreviewHeader);
		this.gbPreviewHeaderAndFooter.Location = new System.Drawing.Point(24, 131);
		this.gbPreviewHeaderAndFooter.Name = "gbPreviewHeaderAndFooter";
		this.gbPreviewHeaderAndFooter.Size = new System.Drawing.Size(450, 177);
		this.gbPreviewHeaderAndFooter.TabIndex = 42;
		this.gbPreviewHeaderAndFooter.TabStop = false;
		this.gbPreviewHeaderAndFooter.Text = "表頭及表尾";
		appearance56.FontData.Name = "細明體";
		appearance56.FontData.SizeInPoints = 11f;
		this.ddlPreviewBudgetFooter.Appearance = appearance56;
		this.ddlPreviewBudgetFooter.AutoSize = true;
		this.ddlPreviewBudgetFooter.Location = new System.Drawing.Point(12, 146);
		this.ddlPreviewBudgetFooter.Name = "ddlPreviewBudgetFooter";
		this.ddlPreviewBudgetFooter.Size = new System.Drawing.Size(428, 24);
		this.ddlPreviewBudgetFooter.TabIndex = 22;
		this.ddlPreviewBudgetFooter.Text = null;
		appearance57.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance57.ForeColor = System.Drawing.Color.Green;
		this.ultraLabel24.Appearance = appearance57;
		this.ultraLabel24.Location = new System.Drawing.Point(96, 127);
		this.ultraLabel24.Name = "ultraLabel24";
		this.ultraLabel24.Size = new System.Drawing.Size(227, 23);
		this.ultraLabel24.TabIndex = 23;
		this.ultraLabel24.Text = "(不適用於中英文並列格式)";
		this.ultraLabel25.Location = new System.Drawing.Point(12, 126);
		this.ultraLabel25.Name = "ultraLabel25";
		this.ultraLabel25.Size = new System.Drawing.Size(84, 23);
		this.ultraLabel25.TabIndex = 5;
		this.ultraLabel25.Text = "預算表尾:";
		this.ultraLabel26.Location = new System.Drawing.Point(8, 72);
		this.ultraLabel26.Name = "ultraLabel26";
		this.ultraLabel26.Size = new System.Drawing.Size(176, 14);
		this.ultraLabel26.TabIndex = 3;
		this.ultraLabel26.Text = "機關 / 公司英文名稱:";
		appearance58.FontData.Name = "細明體";
		appearance58.FontData.SizeInPoints = 11f;
		this.tbPreviewEnglishHeader.Appearance = appearance58;
		this.tbPreviewEnglishHeader.AutoSize = true;
		this.tbPreviewEnglishHeader.Location = new System.Drawing.Point(12, 92);
		this.tbPreviewEnglishHeader.Name = "tbPreviewEnglishHeader";
		this.tbPreviewEnglishHeader.Size = new System.Drawing.Size(428, 24);
		this.tbPreviewEnglishHeader.TabIndex = 2;
		this.ultraLabel27.Location = new System.Drawing.Point(8, 20);
		this.ultraLabel27.Name = "ultraLabel27";
		this.ultraLabel27.Size = new System.Drawing.Size(180, 16);
		this.ultraLabel27.TabIndex = 1;
		this.ultraLabel27.Text = "機關 / 公司名稱:";
		appearance59.FontData.Name = "細明體";
		appearance59.FontData.SizeInPoints = 11f;
		this.tbPreviewHeader.Appearance = appearance59;
		this.tbPreviewHeader.AutoSize = true;
		this.tbPreviewHeader.Location = new System.Drawing.Point(12, 40);
		this.tbPreviewHeader.Name = "tbPreviewHeader";
		this.tbPreviewHeader.Size = new System.Drawing.Size(428, 24);
		this.tbPreviewHeader.TabIndex = 0;
		this.panel11.Controls.Add(this.ultraLabel28);
		this.panel11.Controls.Add(this.ultraLabel29);
		this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel11.Location = new System.Drawing.Point(0, 0);
		this.panel11.Name = "panel11";
		this.panel11.Size = new System.Drawing.Size(586, 56);
		this.panel11.TabIndex = 44;
		appearance60.BackColor = System.Drawing.Color.White;
		this.ultraLabel28.Appearance = appearance60;
		this.ultraLabel28.Location = new System.Drawing.Point(38, 31);
		this.ultraLabel28.Name = "ultraLabel28";
		this.ultraLabel28.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel28.TabIndex = 4;
		this.ultraLabel28.Text = "您可以挑選所需的格式。";
		appearance61.BackColor = System.Drawing.Color.White;
		this.ultraLabel29.Appearance = appearance61;
		this.ultraLabel29.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel29.Location = new System.Drawing.Point(24, 10);
		this.ultraLabel29.Name = "ultraLabel29";
		this.ultraLabel29.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel29.TabIndex = 3;
		this.ultraLabel29.Text = "預覽格式";
		this.panel10.AutoSize = true;
		this.panel10.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panel10.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel10.Controls.Add(this.btnPreview);
		this.panel10.Controls.Add(this.btnCancel);
		this.panel10.Controls.Add(this.groupBox6);
		this.panel10.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel10.Location = new System.Drawing.Point(0, 471);
		this.panel10.Name = "panel10";
		this.panel10.Size = new System.Drawing.Size(586, 44);
		this.panel10.TabIndex = 18;
		appearance62.Image = resources.GetObject("appearance62.Image");
		appearance62.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnPreview.Appearance = appearance62;
		this.btnPreview.BackColor = System.Drawing.SystemColors.Control;
		this.btnPreview.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnPreview.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnPreview.Font = new System.Drawing.Font("細明體", 11f);
		this.btnPreview.ImageSize = new System.Drawing.Size(20, 20);
		this.btnPreview.ImageTransparentColor = System.Drawing.Color.White;
		this.btnPreview.Location = new System.Drawing.Point(399, 10);
		this.btnPreview.Name = "btnPreview";
		this.btnPreview.ShowFocusRect = false;
		this.btnPreview.ShowOutline = false;
		this.btnPreview.Size = new System.Drawing.Size(88, 31);
		this.btnPreview.SupportThemes = false;
		this.btnPreview.TabIndex = 6;
		this.btnPreview.Text = "預覽";
		this.btnPreview.Click += new System.EventHandler(btnPreview_Click);
		appearance63.Image = resources.GetObject("appearance63.Image");
		appearance63.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnCancel.Appearance = appearance63;
		this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
		this.btnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Font = new System.Drawing.Font("細明體", 11f);
		this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btnCancel.Location = new System.Drawing.Point(492, 10);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.ShowFocusRect = false;
		this.btnCancel.ShowOutline = false;
		this.btnCancel.Size = new System.Drawing.Size(88, 31);
		this.btnCancel.SupportThemes = false;
		this.btnCancel.TabIndex = 5;
		this.btnCancel.Text = "取消";
		this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox6.Location = new System.Drawing.Point(0, 0);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(586, 8);
		this.groupBox6.TabIndex = 3;
		this.groupBox6.TabStop = false;
		this.Tab_Ctrl.BackColor = System.Drawing.Color.White;
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_A);
		this.Tab_Ctrl.Controls.Add(this.Tab_B);
		this.Tab_Ctrl.Controls.Add(this.Tab_C);
		this.Tab_Ctrl.Controls.Add(this.Tab_D);
		this.Tab_Ctrl.Controls.Add(this.Tab_A1);
		this.Tab_Ctrl.Controls.Add(this.Tab_F);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(586, 515);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabIndex = 0;
		ultraTab1.TabPage = this.Tab_A1;
		ultraTab1.Text = "tab5";
		ultraTab2.TabPage = this.Tab_A;
		ultraTab2.Text = "tab1";
		ultraTab3.TabPage = this.Tab_B;
		ultraTab3.Text = "tab2";
		ultraTab4.TabPage = this.Tab_C;
		ultraTab4.Text = "tab3";
		ultraTab5.TabPage = this.Tab_D;
		ultraTab5.Text = "tab4";
		ultraTab6.TabPage = this.Tab_F;
		ultraTab6.Text = "tab6";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[6] { ultraTab1, ultraTab2, ultraTab3, ultraTab4, ultraTab5, ultraTab6 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(586, 515);
		this.timer1.Enabled = true;
		this.timer1.Interval = 1000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		this.ultraCheckEditor1.Location = new System.Drawing.Point(13, 176);
		this.ultraCheckEditor1.Name = "ultraCheckEditor1";
		this.ultraCheckEditor1.Size = new System.Drawing.Size(144, 20);
		this.ultraCheckEditor1.TabIndex = 0;
		this.timer2.Interval = 750;
		this.timer2.Tick += new System.EventHandler(timer2_Tick);
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.A1_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(586, 515);
		base.Controls.Add(this.Tab_Ctrl);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormBudgetExp_Wzd";
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "製作電子檔";
		base.Load += new System.EventHandler(FormBudgetExp_Wzd_Load);
		this.Tab_A1.ResumeLayout(false);
		this.Tab_A1.PerformLayout();
		this.panel9.ResumeLayout(false);
		this.Tab_A.ResumeLayout(false);
		this.Tab_A.PerformLayout();
		this.panel1.ResumeLayout(false);
		this.Tab_B.ResumeLayout(false);
		this.Tab_B.PerformLayout();
		this.panel4.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		this.groupBox7.ResumeLayout(false);
		this.groupBox7.PerformLayout();
		this.gbHeaderAndFooter.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ddlBudgetFooter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbEnglishHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSaveAsProjectCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbOutputPath).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbFileName).EndInit();
		this.panel2.ResumeLayout(false);
		this.Tab_C.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		this.panel7.PerformLayout();
		this.panel6.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		this.Tab_D.ResumeLayout(false);
		this.Tab_D.PerformLayout();
		this.panel8.ResumeLayout(false);
		this.Tab_F.ResumeLayout(false);
		this.Tab_F.PerformLayout();
		this.panel12.ResumeLayout(false);
		this.groupBox8.ResumeLayout(false);
		this.gbPreviewHeaderAndFooter.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ddlPreviewBudgetFooter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbPreviewEnglishHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbPreviewHeader).EndInit();
		this.panel11.ResumeLayout(false);
		this.panel10.ResumeLayout(false);
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

	public FormBudgetExp_Wzd()
	{
		InitializeComponent();
		if (PubTools.GetAppSet_Bool("WaterResourcesAgency"))
		{
			rbOutputBlankBudget.Visible = true;
		}
	}

	private string GetCurrentBDGT_Type()
	{
		string sBUD_TYPE = "BUD";
		if (FormActionName == PccesFormAction.BUD)
		{
			ArrayList aArr = new ArrayList();
			aArr.Add(userID);
			aArr.Add("預算編輯--讀取目前預算編輯類型(預算書或契約書)");
			Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
			PROJ.ps_projectCode = projectCode;
			PROJ.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
			sBUD_TYPE = PROJ.GetCurrentProjectActionName(projectCode);
			PROJ = null;
		}
		return sBUD_TYPE;
	}

	private void FormBudgetExp_Wzd_Load(object sender, EventArgs e)
	{
		base.Height = FormNormalHeight;
		LoadData();
		SetupComponentVisibility();
		InitialControls();
		CurrentBudgetFormAction = GetCurrentBDGT_Type();
		if (CurrentBudgetFormAction.ToUpper() == "CNT")
		{
			rbOutputBudget.Text = "契約書";
			rbOutputBudget.ForeColor = Color.Purple;
			ultraLabel3.Text = ultraLabel3.Text.Replace("預算書", "契約書");
			rbOutputBid.Visible = false;
			ultraLabel4.Visible = false;
			chkOutputXML.Text = chkOutputXML.Text.Replace("標單檔", "契約檔");
		}
		CheckMainLItem_IsReach_ResourceItemTenPercent();
	}

	private void CheckMainLItem_IsReach_ResourceItemTenPercent()
	{
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = userID;
		Application.DoEvents();
		if (FormActionName == PccesFormAction.BUD)
		{
			DataTable DT_Details = DBCLS.GetUserDefine("Select itemNo, CName, Kind from budItemA Where Kind='L' and ProjectCode='" + projectCode + "' ");
			Application.DoEvents();
			DataTable DT_Resource = DBCLS.GetUserDefine("Select pccesCode, CName from budProjMrsA Where ProjectCode='" + projectCode + "' ");
			Application.DoEvents();
			int DetailsLCount = DT_Details.Rows.Count;
			int ResorceCount = (int)((double)DT_Resource.Rows.Count * 0.1);
			if ((DetailsLCount > ResorceCount) ? true : false)
			{
				A_Btn_Next.Enabled = false;
				Application.DoEvents();
				Thread t1 = new Thread(ShowExceedResourceCount);
				Application.DoEvents();
				Thread.Sleep(100);
				Application.DoEvents();
				t1.Start();
			}
		}
	}

	private void ShowExceedResourceCount()
	{
		MessageBox.Show(this, "注意：主項大類的單獨計價項目數量超過專案工項總數10%，\n\u3000\u3000\u3000請先修正其主項大類目種類。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
	}

	private bool Is75094900()
	{
		string sPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "75094900.dat");
		if (File.Exists(sPath))
		{
			return true;
		}
		return false;
	}

	private void LoadData()
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
		dsSubMemo = subMemo.GetSubMemo(projectCode);
		string iniFileName = ApplicationDirectory + "PccesMain.ini";
		tbOutputPath.Text = CommonMethods.IniReadValue(iniFileName, "FormBudget", "ExportPath");
		string UseProjectCodeAsFileName = CommonMethods.IniReadValue(iniFileName, "CheckBox", "State");
		chkUseProjectCodeAsFileName.Checked = UseProjectCodeAsFileName == "True";
		if (dsProject.Tables[0].Rows[0]["printMode"] != DBNull.Value && dsProject.Tables[0].Rows[0]["printMode"].ToString().StartsWith("1"))
		{
			SummaryIncludeWorkItem = true;
		}
		else
		{
			SummaryIncludeWorkItem = false;
		}
	}

	private void SetupComponentVisibility()
	{
		if (Preview)
		{
			base.Height = FormPreviewHeight;
			Text = "報表預覽";
			Tab_F.Tab.Selected = true;
		}
		else if (FormActionName == PccesFormAction.BID)
		{
			Tab_A1.Tab.Selected = true;
			isTendererInfoFilled();
			ddlBudgetFooter.Visible = false;
			ultraLabel21.Visible = false;
			ultraLabel20.Visible = false;
			chkXMLformat102.Visible = false;
		}
		else if (FormActionName == PccesFormAction.SplitContract)
		{
			Tab_A1.Tab.Selected = true;
			ultraLabel19.Text = "您要輸出的契約案件是否要包含單價分析";
			rbOutputBid.Visible = false;
			chkOutputXML.Visible = false;
			chkOutputZMD.Visible = false;
			chkOutputExcel.Checked = true;
			chkXMLformat102.Visible = false;
			ultraLabel5.Top = chkOutputExcel.Top;
		}
		else if (FormActionName == PccesFormAction.SubChange)
		{
			Tab_A1.Tab.Selected = true;
			ultraLabel19.Text = "您要輸出的契約變更案件是否要包含單價分析";
			rbOutputBid.Visible = false;
			chkOutputXML.Visible = false;
			chkOutputZMD.Visible = false;
			chkOutputExcel.Checked = true;
			chkXMLformat102.Visible = false;
			ultraLabel5.Top = chkOutputExcel.Top;
		}
		else if (FormActionName == PccesFormAction.Invoice)
		{
			Tab_B.Tab.Selected = true;
			chkOutputExcel.Checked = true;
			rbOutputBid.Visible = false;
			chkOutputXML.Visible = false;
			chkOutputZMD.Visible = false;
			chkXMLformat102.Visible = false;
			ultraLabel5.Top = chkOutputExcel.Top;
			B_Btn_Prev.Visible = false;
			GetOutputFileName();
		}
		else if (FormActionName == PccesFormAction.SubClose || FormActionName == PccesFormAction.SubFinal)
		{
			Tab_B.Tab.Selected = true;
			chkOutputExcel.Checked = true;
			rbOutputBid.Visible = false;
			chkOutputXML.Visible = false;
			B_Btn_Prev.Visible = false;
			chkOutputZMD.Visible = false;
			chkXMLformat102.Visible = false;
			ultraLabel5.Top = chkOutputExcel.Top;
			btnOpenExcelOption.Visible = false;
			GetOutputFileName();
		}
		else
		{
			if (dsProject.Tables[0].Rows[0]["IsType"].ToString() == "3")
			{
				rbOutputBid.Visible = false;
				ultraLabel4.Visible = false;
			}
			Tab_A.Tab.Selected = true;
			ddlBudgetFooter.Visible = true;
			ultraLabel21.Visible = true;
			ultraLabel20.Visible = true;
			object lockCostBreakdownList = dsProject.Tables[0].Rows[0]["IsLockAn"];
			if (lockCostBreakdownList != DBNull.Value && lockCostBreakdownList.ToString() == "Y")
			{
				chkBreakdownListLockCost.Checked = true;
			}
			else
			{
				chkBreakdownListLockCost.Checked = false;
			}
		}
		if (ProjectFlag == "Z14AC1100")
		{
			rbOutputBid.Visible = false;
			ultraLabel4.Visible = false;
			Text += "(材料處發包專用預算書)";
		}
	}

	private void isTendererInfoFilled()
	{
		if (dsSubMemo.Tables[0].Rows.Count == 0 || dsSubMemo.Tables[0].Rows[0]["FACTORY_ID"] == DBNull.Value || dsSubMemo.Tables[0].Rows[0]["FACTORY_ID"].ToString() == string.Empty)
		{
			MessageBox.Show(this, "如為使用『政府採購電子領投標系統』線上比減價，\n請先填寫廠商統一編號，再製作電子檔。\n\n[標單資訊] --> [其他資訊] --> [投標廠商]", "警示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void InitialControls()
	{
		UltraTextEditor ultraTextEditor = tbHeader;
		string text = (tbPreviewHeader.Text = OwnerChineseName);
		ultraTextEditor.Text = text;
		UltraTextEditor ultraTextEditor2 = tbEnglishHeader;
		text = (tbPreviewEnglishHeader.Text = OwnerEnglishName);
		ultraTextEditor2.Text = text;
		UltraComboEditor ultraComboEditor = ddlBudgetFooter;
		text = (ddlPreviewBudgetFooter.Text = string.Empty);
		ultraComboEditor.Text = text;
		UserDefined userDefind = new UserDefined();
		DataSet dsFooter = userDefind.GetUserDefinedByKind("RptFooter");
		foreach (DataRow row in dsFooter.Tables[0].Rows)
		{
			ddlBudgetFooter.Items.Add(row["cString"].ToString());
			ddlPreviewBudgetFooter.Items.Add(row["cString"].ToString());
		}
		if (dsFooter.Tables[0].Rows.Count > 0)
		{
			ddlBudgetFooter.SelectedIndex = 0;
			ddlPreviewBudgetFooter.SelectedIndex = 0;
		}
		dsFooter = userDefind.GetUserDefinedByKind("DefaultFooter");
		if (dsFooter.Tables[0].Rows.Count > 0)
		{
			ddlBudgetFooter.Text = dsFooter.Tables[0].Rows[0]["cString"].ToString();
			ddlPreviewBudgetFooter.Text = dsFooter.Tables[0].Rows[0]["cString"].ToString();
		}
		if (FormActionName == PccesFormAction.SplitContract)
		{
			ddlBudgetFooter.Items.Clear();
			ddlBudgetFooter.Items.Add("投標廠商：\u3000\u3000\u3000\u3000\u3000\u3000\u3000[印]\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000負責人：\u3000\u3000\u3000\u3000\u3000[印]");
			ddlBudgetFooter.SelectedIndex = 0;
			ddlBudgetFooter.Enabled = false;
		}
	}

	private void A_Btn_Next_Click(object sender, EventArgs e)
	{
		ShowWarningIfNeedRecalculation();
		if (rbOutputBudget.Checked)
		{
			chkOutputZMD.Visible = true;
			ddlBudgetFooter.Visible = true;
			tbHeader.Enabled = true;
			tbEnglishHeader.Enabled = true;
			ultraLabel21.Visible = true;
			ultraLabel20.Visible = true;
		}
		if (rbOutputBlankBudget.Checked)
		{
			chkOutputExcel.Visible = false;
			chkOutputZMD.Visible = false;
			btnOpenExcelOption.Visible = false;
			ddlBudgetFooter.Visible = true;
			tbHeader.Enabled = true;
			tbEnglishHeader.Enabled = true;
			ultraLabel21.Visible = true;
			ultraLabel20.Visible = true;
			IsBlankBudget = true;
			rbOutputBudget.Checked = true;
		}
		if (rbOutputBid.Checked)
		{
			chkXMLformat102.Visible = false;
			UpdateBreakdownListLockCost();
			string IsBidSet = CommonMethods.IniReadValue(ApplicationDirectory + "PccesMain.ini", "BidSet", "State");
			string IsBidSetAdd = CommonMethods.IniReadValue(ApplicationDirectory + "PccesMain.ini", "BidSet", "StateAdd");
			chkOutputZMD.Visible = false;
			chkOutputZMD.Checked = false;
			ddlBudgetFooter.Visible = false;
			tbHeader.Enabled = false;
			tbEnglishHeader.Enabled = false;
			ultraLabel21.Visible = false;
			ultraLabel20.Visible = false;
			bool Bidflag = false;
			if (IsBidSet == "TRUE" || IsBidSet == "")
			{
				Bidflag = true;
			}
			if (!Bidflag && IsBidSetAdd == "TRUE")
			{
				MessageBox.Show("請注意！\n\n有偵測到新增工項，請檢查發包設定是否正確勾選，\n\n請至【檔案】->【發包設定】檢查！", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			if (ContainsPccesCodeStartWithPoundSign())
			{
				MessageBox.Show(this, "檢查到開頭有【#】字號的工項代碼，\n請檢查是否為說明項，若不是請修正！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			CheckBidSetting();
			ShowSelfExecuteItemCheck();
			if (!IsProjectCitySeleted())
			{
				return;
			}
		}
		CheckVariablePriceItemExists();
		if (isOwnerSelected() && isMeterailDataFilled() && IsProjectClassificationSeleted() && IsResultSummaryFilled())
		{
			if (!IsPrecisionConfromedToStandard())
			{
				lblWarning2.Visible = true;
				llbXMLStandard.Visible = true;
				btnXMLInstruction.Visible = true;
			}
			Tab_B.Tab.Selected = true;
		}
		GetOutputFileName();
		if (Is75094900())
		{
			chkOutputZMD.Text = "中華工程PCCES 內部交換格式(*.mdb)";
		}
		if (gbHeaderAndFooter.Visible)
		{
			base.Height = FormExpandedHeight;
		}
	}

	private void ShowWarningIfNeedRecalculation()
	{
		if (dsProject.Tables[0].Rows[0]["IsReCal"].ToString() == "Y")
		{
			DialogResult Result = MessageBox.Show("偵測到資料有異動過，建議先執行【重新總計】再製作電子檔。\n是否結束電子檔製作？", "訊息", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (Result == DialogResult.Yes)
			{
				Close();
			}
		}
	}

	private void ShowSelfExecuteItemCheck()
	{
		BudPageBreak pagebreak = new BudPageBreak();
		DataTable DT_SelfExecuteItems = pagebreak.GetCheckedSelfExecuteItem(projectCode);
		if (DT_SelfExecuteItems.Rows.Count > 0)
		{
			DialogResult Result = MessageBox.Show("偵測到[自辦項目或子項]勾選了要發包，請檢查發包設定是否正確勾選，\n\n請至【檔案】->【發包設定】檢查！再製作電子檔。\n是否結束電子檔製作？", "訊息", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (Result == DialogResult.Yes)
			{
				Close();
			}
		}
	}

	private void UpdateBreakdownListLockCost()
	{
		if (chkBreakdownListLockCost.Checked)
		{
			dsProject.Tables[0].Rows[0]["IsLockAn"] = "Y";
		}
		else
		{
			dsProject.Tables[0].Rows[0]["IsLockAn"] = "N";
		}
		project.GetDatasetUpdate(dsProject);
	}

	private bool ContainsPccesCodeStartWithPoundSign()
	{
		BudProjMrsA budProjMrsA = new BudProjMrsA();
		return budProjMrsA.ContainsPccesCodeStartWithPoundSign(projectCode);
	}

	private void CheckBidSetting()
	{
		BudPageBreak budPageBreak = new BudPageBreak();
		DataSet dsWronglySetBidItem = budPageBreak.GetWronglySetBidItem(projectCode);
		if (dsWronglySetBidItem.Tables[0].Rows.Count <= 0)
		{
			return;
		}
		string Warning = string.Empty;
		foreach (DataRow row in dsWronglySetBidItem.Tables[0].Rows)
		{
			string text = Warning;
			Warning = text + "【" + row["itemNo"].ToString().Trim() + "】" + row["cName"].ToString() + Environment.NewLine;
		}
		Warning += "\n以上項目發包設定未勾選，但其子項至少一項以上有勾選發包，請確定發包設定正確！";
		MessageBox.Show(this, Warning, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
	}

	private bool IsProjectCitySeleted()
	{
		if (dsProject.Tables[0].Rows[0]["city"] == DBNull.Value || dsProject.Tables[0].Rows[0]["city"].ToString() == string.Empty)
		{
			MessageBox.Show("若要製作空白標單請先選擇\"工程所在縣市\"，再匯出電子檔。\n請至預算資訊 -> 專案基本資訊，選擇所在區域及工程所在縣市，不可空白！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		return true;
	}

	private bool isOwnerSelected()
	{
		if (dsProject.Tables[0].Rows[0]["mainCode"].ToString().Trim() == string.Empty)
		{
			MessageBox.Show("若要製作電子檔請先填寫\"主辦機關\"，再匯出電子檔。\n請至預算資訊 -> 專案基本資料，將項目數據填入，不可空白！", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		return true;
	}

	private bool isMeterailDataFilled()
	{
		return true;
	}

	private bool IsProjectClassificationSeleted()
	{
		if (dsSubMemo.Tables[0].Rows.Count == 0 || dsSubMemo.Tables[0].Rows[0]["ITEM1_NO"] == DBNull.Value || dsSubMemo.Tables[0].Rows[0]["ITEM1_NO"].ToString() == string.Empty)
		{
			MessageBox.Show("若要製作電子檔請先選擇\"主要分類\"，再匯出電子檔。\n請至預算資訊 -> 其他資訊，選擇主要分類，不可空白！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		return true;
	}

	private bool IsResultSummaryFilled()
	{
		return true;
	}

	private void CheckVariablePriceItemExists()
	{
		BudProjMrsA budProjMrsA = new BudProjMrsA();
		if (!budProjMrsA.VariablePriceItemExists(projectCode))
		{
			MessageBox.Show(this, "依『招標增列文件作業要點』規定，詳細表與資源統計表總價金額必須一致，本預算有因取位而需四捨五入致產生誤差值，但單價分析工項無 W 類雜項之項目，請於單價分析項增加一 W 類工項，以利系統自動取 W 類之項目攤提誤差值。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private bool IsPrecisionConfromedToStandard()
	{
		Archnowledge.Pcces.DomainModule.General.PubDecimal pubDecimal = new Archnowledge.Pcces.DomainModule.General.PubDecimal();
		DataSet dsPubDecimal = pubDecimal.GetPubDecimal(projectCode);
		if (dsPubDecimal.Tables[0].Rows.Count > 0)
		{
			DataRow drPubDecimal = dsPubDecimal.Tables[0].Rows[0];
			int itemCost = ArchConvert.Obj2Int(drPubDecimal["itemCost"]);
			int itemAmt = ArchConvert.Obj2Int(drPubDecimal["itemAmt"]);
			int analysisCost = ArchConvert.Obj2Int(drPubDecimal["analysisCost"]);
			int analysisAmt = ArchConvert.Obj2Int(drPubDecimal["analysisAmt"]);
			if (itemCost > 2 || itemAmt > 2 || analysisCost > 2 || analysisAmt > 2)
			{
				return false;
			}
			return true;
		}
		return true;
	}

	private void A1_Btn_Next_Click(object sender, EventArgs e)
	{
		if (FormActionName == PccesFormAction.BID)
		{
			ShowWarningIfNeedRecalculation();
		}
		Tab_B.Tab.Selected = true;
		chkOutputZMD.Visible = false;
		GetOutputFileName();
	}

	private void B_Btn_Next_Click(object sender, EventArgs e)
	{
		base.Width = 594;
		if (ExcelSettingsFromDB && !hasConfirmedPrintMode())
		{
			MessageBox.Show(this, "請先執行製作電子檔內的【Microsoft Excel 格式】-->【輸出選項...】確認匯出型態！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (FormActionName == PccesFormAction.BUD)
		{
			bool flag = false;
			BudProject theProject = new BudProject();
			DataSet DS_SelfExam = theProject.GetProject(projectCode);
			string sSExamValue = DS_SelfExam.Tables[0].Rows[0]["SelfExam"].ToString().Trim();
			if (sSExamValue == "" || sSExamValue == "000000")
			{
				FormBudgetExp_Wzd_SelfExamDiaglog FM = new FormBudgetExp_Wzd_SelfExamDiaglog();
				FM._Message = "請先執行預算書編輯【工具】→【自主檢查】以免疏忽致造成流廢標！";
				FM.Owner = this;
				if (FM.ShowDialog() == DialogResult.Cancel)
				{
					theProject = null;
					DS_SelfExam = null;
					return;
				}
			}
			else if (sSExamValue != "111111")
			{
				FormBudgetExp_Wzd_SelfExamDiaglog FM = new FormBudgetExp_Wzd_SelfExamDiaglog();
				FM._Message = "尚未完全勾選【自主檢查】的每一個項！\n請先執行預算書編輯【工具】→【自主檢查】以免疏忽致造成流廢標！";
				FM.Owner = this;
				if (FM.ShowDialog() == DialogResult.Cancel)
				{
					theProject = null;
					DS_SelfExam = null;
					return;
				}
			}
		}
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(userID);
		tmp_AL1.Add("(EXCEL輸出)使用者輸入表尾設定");
		UserDefind UserCom = new UserDefind(tmp_AL1);
		UserCom.SetDefaultFooter(ddlBudgetFooter.Text);
		int outputFormatCount = GetOutputFormatCount();
		if (outputFormatCount == 0)
		{
			string sWarning = "至少得先選定一種輸出格式。";
			MessageBox.Show(this, sWarning, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (tbFileName.Text.Trim() == "")
		{
			string sWarning = "請先給定檔案名稱。";
			MessageBox.Show(this, sWarning, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbFileName.Focus();
			return;
		}
		if (tbOutputPath.Text.Trim() == "")
		{
			string sWarning = "請先給定輸出路徑。";
			MessageBox.Show(this, sWarning, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbOutputPath.Focus();
			return;
		}
		if (!Directory.Exists(tbOutputPath.Text.Trim()))
		{
			string sWarning = "你所指定的路徑並不存在，請重新挑選。";
			MessageBox.Show(this, sWarning, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbOutputPath.Focus();
			return;
		}
		if (!CommonMethods.IsStrByteLenValid(projectCode, 40) && txtSaveAsProjectCode.Text.Trim() == "")
		{
			string sWarning = "你原有的專案代碼長度超過 40 碼，請輸入新的專案代碼。";
			MessageBox.Show(this, sWarning, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtSaveAsProjectCode.Focus();
			return;
		}
		if (chkOutputExcel.Checked && !SummaryIncludeWorkItem && FormActionName != PccesFormAction.SubClose && FormActionName != PccesFormAction.SubFinal)
		{
			DialogResult result = MessageBox.Show(this, "Excel 輸出選項[包含工作要項]未勾選，是否繼續？", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.No)
			{
				return;
			}
		}
		if (FormActionName == PccesFormAction.BUD)
		{
			BudItemA itemA = new BudItemA();
			DataSet dataSet = itemA.GetBudItemAWithItemNoLongerThan20(projectCode);
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				string Warning = string.Empty;
				foreach (DataRow row in dataSet.Tables[0].Rows)
				{
					string text = Warning;
					Warning = text + "【" + row["itemNo"].ToString().Trim() + "】" + row["cName"].ToString() + Environment.NewLine;
				}
				Warning = "\n項次長度不可超過二十個字！\n\n" + Warning + "\n請修正後再匯出電子檔。";
				MessageBox.Show(this, Warning, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			BudPageBreak budPageBreak = new BudPageBreak();
			DataSet ds = budPageBreak.GetBudPCalsWronglyIsBidItem(projectCode);
			if (ds.Tables[0].Rows.Count > 0)
			{
				string warringMessage = "";
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					warringMessage += string.Format("【自訂變數】：{0}，【項次】：{1}，【項目】：{2}\n", ds.Tables[0].Rows[i]["varalias"].ToString().Trim(), ds.Tables[0].Rows[i]["itemno"].ToString().Trim(), ds.Tables[0].Rows[i]["cname"].ToString().Trim());
				}
				warringMessage += "有設定自訂變數卻未發包，請重新設定";
				MessageBox.Show(warringMessage, "發包設定錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
		}
		if (chkUseProjectCodeAsFileName.Checked && !ignoreNameCollision())
		{
			return;
		}
		if (rbOutputBudget.Checked && chkOutputXML.Checked)
		{
			MessageBox.Show(this, "因預算書 XML 電子檔格式有異動，此 XML 電子檔匯出後，請先確認對方使用之 PCCES Win 4.3 版本已是 【4.3.1000.81】 或更新版本。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		base.Height = FormNormalHeight;
		Tab_C.Tab.Selected = true;
		Application.DoEvents();
		bool OutputSucceeded = DoExport(outputFormatCount);
		GC.Collect();
		if (OutputSucceeded)
		{
			Tab_D.Tab.Selected = true;
			bool IsAddSlash = !(tbOutputPath.Text.Trim().Substring(tbOutputPath.Text.Trim().Length - 1, 1) == "\\");
			lbOutputXMLFileName.Text = tbOutputPath.Text.Trim() + (IsAddSlash ? "\\" : "") + tbFileName.Text.Trim() + ".*";
			if (chkOutputExcel.Checked)
			{
				btnOpenExcel.Visible = true;
				lbOutputExcelFileName.Visible = true;
				lbOutputExcelFileName.Text = tbOutputPath.Text.Trim() + (IsAddSlash ? "\\" : "") + tbFileName.Text.Trim() + ".xls";
			}
		}
		else
		{
			Tab_B.Tab.Selected = true;
		}
	}

	private bool hasConfirmedPrintMode()
	{
		dsProject = project.GetProject(projectCode);
		object printMode = dsProject.Tables[0].Rows[0]["printMode"];
		if (printMode != DBNull.Value && printMode.ToString().Length > 47)
		{
			return true;
		}
		return false;
	}

	private int GetOutputFormatCount()
	{
		int count = 0;
		if (chkOutputXML.Checked)
		{
			count++;
		}
		if (chkOutputExcel.Checked)
		{
			count++;
		}
		if (chkOutputZMD.Checked)
		{
			count++;
		}
		return count;
	}

	private bool ignoreNameCollision()
	{
		string outputPath = tbOutputPath.Text.Trim();
		string exsitingFileName = string.Empty;
		string fileName = tbFileName.Text.Trim();
		if (chkOutputZMD.Checked && File.Exists(outputPath + "\\" + fileName + ".zmd"))
		{
			exsitingFileName = exsitingFileName + fileName + ".zmd" + Environment.NewLine;
		}
		if (chkOutputXML.Checked && File.Exists(outputPath + "\\" + fileName + ".xml"))
		{
			exsitingFileName = exsitingFileName + fileName + ".xml" + Environment.NewLine;
		}
		if (chkOutputExcel.Checked && File.Exists(outputPath + "\\" + fileName + ".xls"))
		{
			exsitingFileName = exsitingFileName + fileName + ".xls" + Environment.NewLine;
		}
		if (exsitingFileName == string.Empty)
		{
			return true;
		}
		DialogResult Result = MessageBox.Show(this, exsitingFileName + "\n有相同的檔名，是否覆蓋？", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		return Result == DialogResult.Yes;
	}

	private bool DoExport(int OutputFormatCount)
	{
		bool OutputResult = true;
		string directoryPath = tbOutputPath.Text.Trim();
		if (!Directory.Exists(directoryPath))
		{
			Directory.CreateDirectory(directoryPath);
		}
		progressBarTotal.Minimum = 0;
		progressBarTotal.Maximum = OutputFormatCount;
		progressBarTotal.Value = 0;
		Cursor = Cursors.WaitCursor;
		if (chkOutputXML.Checked)
		{
			progressBarSingle.Minimum = 0;
			progressBarSingle.Maximum = 100;
			progressBarSingle.Value = 0;
			BudItemA theItemA = new BudItemA();
			theItemA.CleanNegativeSno(projectCode);
			theItemA.InsertSegmentedCustomVar(projectCode);
			ExportXML(flag: true);
			theItemA.CleanNegativeSno(projectCode);
			progressBarTotal.Value++;
			Application.DoEvents();
			progressBarSingle.Value = 100;
			Application.DoEvents();
		}
		if (chkOutputExcel.Checked)
		{
			IsExcelStarted = true;
			string FilePath = GetOutputFilePath() + ".xls";
			if (ExportExcel(FilePath) != 0)
			{
				OutputResult = false;
			}
		}
		if (chkOutputZMD.Checked)
		{
			progressBarSingle.Minimum = 0;
			progressBarSingle.Maximum = 100;
			progressBarSingle.Value = 0;
			ExportXML(flag: false);
			progressBarTotal.Value++;
			Application.DoEvents();
			progressBarSingle.Value = 100;
			Application.DoEvents();
		}
		Cursor = Cursors.Default;
		return OutputResult;
	}

	private string GetOutputFilePath()
	{
		string outputPath = tbOutputPath.Text.Trim();
		string outputFileName = tbFileName.Text.Trim();
		if (outputPath.EndsWith("\\"))
		{
			return outputPath + outputFileName;
		}
		return outputPath + "\\" + outputFileName;
	}

	private void getCodeIndex()
	{
		DataTable dtResource = null;
		string strUnit = "";
		string strName = "";
		string strNameAlt = "";
		string strChapName = "";
		string strCompareErrState = "";
		string strChapCodeCorrect = "";
		int iMemoItemCount = 0;
		decimal dAmt = 0m;
		decimal iCorrect = 0m;
		decimal dCorrectTotal = 0m;
		decimal dTotal = 0m;
		decimal iFit = 0m;
		decimal dFitTotal = 0m;
		DBClass dbC = new DBClass();
		DataTable dtAutoNumB = dbC.GetAutoNumB();
		DataTable dtAutoNumA = dbC.GetAutoNumA();
		CodeValidator cCV = new CodeValidator(dtAutoNumA, dtAutoNumB);
		cCV._UserID = userID;
		cCV._ProjectCode = projectCode;
		CodeFitter cCF = new CodeFitter();
		dWeightCorrectRatio = 0m;
		dWeightCorrectRatio = 0m;
		dCorrectTotal = 0m;
		dFitTotal = 0m;
		dTotal = 0m;
		iCorrect = 0m;
		iFit = 0m;
		string sItemClass = "";
		BudProjMrsA ProjMrsA = new BudProjMrsA();
		decimal dProjectTotal = 0m;
		dtResource = ProjMrsA.GetResource(projectCode).Tables[0];
		DataView dvResource = dtResource.DefaultView;
		FormProgress FM = new FormProgress();
		FM._Max = dtResource.Rows.Count;
		FM._Min = 0;
		FM.Message = "重新計算編碼正確率中，請稍候...";
		Application.DoEvents();
		FM.TopMost = true;
		FM.Show();
		int iRowsCount = 0;
		Cursor = Cursors.WaitCursor;
		dvResource.Sort = "pccesCode Asc";
		for (int i = 0; i < dvResource.Count; i++)
		{
			iRowsCount++;
			FM.SetProgressValue(i);
			if (i % 20 == 0)
			{
				Application.DoEvents();
			}
			string PccesCode = ArchConvert.Obj2String(dvResource[i]["pccesCode"]).Trim();
			if (PccesCode.Length > 0)
			{
				sItemClass = PccesCode.Substring(0, 1);
			}
			if (dvResource[i]["costKind"].ToString() == "#" || dvResource[i]["costKind"].ToString().ToUpper() == "Z")
			{
				iMemoItemCount++;
				dvResource[i]["correct"] = "";
				dvResource[i]["CompareErrState"] = "";
				continue;
			}
			strName = "";
			strNameAlt = "";
			strChapName = "";
			strUnit = "";
			strCompareErrState = "";
			strChapCodeCorrect = "";
			bool bRet = cCV.ValidateCode(PccesCode, out strName, out strUnit, out strCompareErrState, out strChapCodeCorrect, out strNameAlt, out strChapName);
			if (bRet)
			{
				bool IsUnitGood = strUnit.Trim() == dvResource[i]["unitName"].ToString().Trim();
				if (!IsUnitGood)
				{
					IsUnitGood = CheckUnitGoodAdvanced(cCV, strUnit.Trim(), dvResource[i]["unitName"].ToString().Trim());
				}
				bool IsNameGood = dvResource[i]["cName"].ToString().Trim().IndexOf(strName.Trim(), 0) == 0;
				bool IsStarNameGood = true;
				if (strNameAlt.Trim() != "")
				{
					IsStarNameGood = strNameAlt.Trim() != "" && dvResource[i]["cName"].ToString().Trim().IndexOf(strNameAlt.Trim(), 0) == 0;
				}
				bRet = ((IsUnitGood && (IsNameGood || IsStarNameGood)) ? true : false);
			}
			if (bRet)
			{
				dvResource[i]["correct"] = '是';
			}
			else
			{
				dvResource[i]["correct"] = '否';
			}
			if (strName.Trim() == "")
			{
				if (strCompareErrState.IndexOf("細目碼錯誤") <= -1)
				{
				}
			}
			else if (dvResource[i]["CName"].ToString().Trim().IndexOf(strName.Trim(), 0) < 0)
			{
				if ((sItemClass == "E" || sItemClass == "L") && strCompareErrState.Trim() != "")
				{
					strCompareErrState += ((strCompareErrState.Trim() == "") ? "工項名稱錯誤" : "，工項名稱錯誤");
					if (strNameAlt.Trim() == "")
					{
						dvResource[i]["correct"] = '否';
					}
				}
				else
				{
					dvResource[i]["CorrectCName"] = strName;
					strCompareErrState += ((strCompareErrState.Trim() == "") ? "工項名稱錯誤" : "，工項名稱錯誤");
					if (strNameAlt.Trim() == "")
					{
						dvResource[i]["correct"] = '否';
					}
				}
			}
			else
			{
				dvResource[i]["CorrectCName"] = "";
			}
			if (strUnit.Trim().Length > 4)
			{
				strUnit = strUnit.Substring(0, 4);
			}
			if (PccesCode.Substring(PccesCode.Length - 1) == "0" && strUnit.Trim() == "")
			{
				strCompareErrState += ((strCompareErrState.Trim() == "") ? "單位碼不應為0" : "，單位碼不應為0");
				dvResource[i]["correct"] = '否';
			}
			else if (strUnit.Trim() == "")
			{
				if (strCompareErrState.IndexOf("細目碼錯誤") <= -1)
				{
					if (strCompareErrState.IndexOf("，工項名稱錯誤") > -1)
					{
						strCompareErrState = strCompareErrState.Replace("，工項名稱錯誤", "");
					}
					else if (strCompareErrState.IndexOf("工項名稱錯誤") > -1)
					{
						strCompareErrState = strCompareErrState.Replace("工項名稱錯誤", "");
					}
					if (strCompareErrState.IndexOf("綱要編碼錯誤") < 0)
					{
						strCompareErrState += ((strCompareErrState.Trim() == "") ? "細目碼錯誤" : "，細目碼錯誤");
					}
				}
			}
			else if (dvResource[i]["unitName"].ToString().Trim() != strUnit.Trim())
			{
				if (!CheckUnitGoodAdvanced(cCV, strUnit.Trim(), dvResource[i]["unitName"].ToString().Trim()))
				{
					dvResource[i]["CorrectUnitName"] = strUnit;
					strCompareErrState += ((strCompareErrState.Trim() == "") ? "單位錯誤" : "，單位錯誤");
				}
				else
				{
					dvResource[i]["CorrectUnitName"] = "";
				}
			}
			else
			{
				dvResource[i]["CorrectUnitName"] = "";
			}
			strCompareErrState = ReAssignErrorState(strCompareErrState);
			bool flag = true;
			dvResource[i]["CompareErrState"] = strCompareErrState;
			if (strCompareErrState.Trim() != "")
			{
				dvResource[i]["correct"] = '否';
				bRet = false;
			}
			dAmt = (string.IsNullOrEmpty(dvResource[i]["usrAmt"].ToString()) ? 0m : ArchConvert.Obj2Decimal(dvResource[i]["usrAmt"]));
			dTotal += ((dAmt > 0m) ? dAmt : 0m);
			dCorrectTotal += ((bRet && dAmt > 0m) ? dAmt : 0m);
			iCorrect += (decimal)(bRet ? 1 : 0);
			if (strChapCodeCorrect == "")
			{
				dvResource[i]["confirm"] = "是";
				dFitTotal += dAmt;
				++iFit;
			}
			else
			{
				dvResource[i]["confirm"] = strChapCodeCorrect;
			}
		}
		FM.Hide();
		FM.Dispose();
		Cursor = Cursors.Default;
		dProjectTotal = dTotal;
		iRowsCount -= iMemoItemCount;
		if (dtResource.Rows.Count > 0)
		{
			BudProject bp = new BudProject();
			dCorrectRatio = iCorrect / (decimal)iRowsCount * 100m;
			dFitRatio = iFit / (decimal)iRowsCount * 100m;
			dWeightCorrectRatio = ((dProjectTotal > 0m) ? (dCorrectTotal / dProjectTotal * 100m) : 0m);
			dWeightFitRatio = ((dProjectTotal > 0m) ? (dFitTotal / dProjectTotal * 100m) : 0m);
			bp.UpdateRates(projectCode, dCorrectRatio, dWeightCorrectRatio, dFitRatio, dWeightFitRatio);
		}
	}

	private string ReAssignErrorState(string sInput)
	{
		string retV = sInput;
		if (sInput == "工項名稱錯誤，單位錯誤")
		{
			retV = "工項名稱錯誤";
		}
		else
		{
			switch (sInput)
			{
			case "工項編碼長度不足，細目碼錯誤":
				retV = "細目碼錯誤";
				break;
			case "工項編碼長度不足，資源碼錯誤":
				retV = "細目碼錯誤";
				break;
			case "工項編碼長度不足，綱要編碼錯誤":
				retV = "綱要編碼錯誤";
				break;
			case "工項編碼長度不足，資源碼錯誤，細目碼錯誤":
				retV = "細目碼錯誤";
				break;
			case "綱要編碼錯誤，細目碼錯誤":
				retV = "綱要編碼錯誤";
				break;
			case "資源碼錯誤":
				retV = "細目碼錯誤";
				break;
			case "細目碼錯誤，工項名稱錯誤":
				retV = "細目碼錯誤";
				break;
			case "資源碼錯誤，細目碼錯誤":
				retV = "細目碼錯誤";
				break;
			case "不符編碼規則，細目碼錯誤":
				retV = "不符編碼規則";
				break;
			}
		}
		return retV;
	}

	private bool CheckUnitGoodAdvanced(CodeValidator cCV, string BaseUnit, string WaitToCheckUnit)
	{
		bool retV = false;
		int iItems = cCV.AlternativeUnit.GetLength(0);
		int iCheckItems = -1;
		for (int i = 0; i < iItems; i++)
		{
			if (!(cCV.AlternativeUnit[i, 0] == BaseUnit))
			{
				continue;
			}
			for (int j = 1; j < 6; j++)
			{
				if (cCV.AlternativeUnit[i, j] == WaitToCheckUnit)
				{
					retV = true;
					break;
				}
			}
		}
		return retV;
	}

	private void ExportXML(bool flag)
	{
		string sXMLKind = "2";
		string ps_ShowCost = "";
		ArrayList aArr = new ArrayList();
		aArr.Add(userID);
		aArr.Add("預算書 XML 轉出");
		Archnowledge.Pcces.BUDClass.Project projcom = new Archnowledge.Pcces.BUDClass.Project(aArr);
		projcom.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		bool toExport;
		if (rbOutputBudget.Checked)
		{
			if (IsBlankBudget)
			{
				projcom.ps_ShowCost = "0";
			}
			else
			{
				projcom.ps_ShowCost = "1";
			}
			projcom.ps_IssurName = chkOutputAliasAsItemName.Checked;
			sXMLKind = "1";
			toExport = true;
		}
		else
		{
			projcom.ps_ShowCost = "0";
			projcom.ps_IssurName = chkOutputAliasAsItemName.Checked;
			sXMLKind = "2";
			toExport = false;
		}
		ps_ShowCost = projcom.ps_ShowCost;
		if (FormActionName == PccesFormAction.BUD)
		{
			if (chkIncludeCostBreakdownList.Checked)
			{
				projcom.ps_ShowAnalysis = "1";
			}
			else
			{
				projcom.ps_ShowAnalysis = "0";
			}
		}
		else
		{
			sXMLKind = "3";
			if (chkSubmitIncludeCostBreakdownList.Checked)
			{
				projcom.ps_ShowAnalysis = "1";
			}
			else
			{
				projcom.ps_ShowAnalysis = "0";
			}
		}
		bool IsOutAnalysis = ((chkIncludeCostBreakdownList.Checked || chkSubmitIncludeCostBreakdownList.Checked) ? true : false);
		DataSet dsXML = projcom.OutputXML(projectCode, "XM1");
		projcom.ps_projectCode = dsXML.Tables["Project"].Rows[0]["projectCode"].ToString().Trim();
		if (txtSaveAsProjectCode.Text.Trim() != "")
		{
			dsXML.Tables["Project"].Rows[0]["projectCode"] = txtSaveAsProjectCode.Text.Trim();
		}
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = userID;
		DataTable DT_PGBK = DBCLS.GetUserDefine("Select SNo,IsPageBreak from " + CommonMethods.GetActionNameString(FormActionName) + "PageBreak Where ProjectCode='" + projectCode + "' ");
		for (int z = 0; z < DT_PGBK.Rows.Count; z++)
		{
			if (DT_PGBK.Rows[z]["IsPageBreak"].ToString() == "Y")
			{
				int idx = GetDTDetailRowIndex(dsXML.Tables["Items"], (int)DT_PGBK.Rows[z]["SNo"]);
				if (idx > -1)
				{
					DataRow dataRow;
					(dataRow = dsXML.Tables["Items"].Rows[idx])["memo"] = string.Concat(dataRow["memo"], "[跳頁]");
				}
			}
		}
		DBCLS = new DBClass();
		DBCLS._FS_UserID = userID;
		DT_PGBK = DBCLS.GetUserDefine("Select SNo, IsBid, IsFormulaChangeKind, FormulaNewName from " + CommonMethods.GetActionNameString(FormActionName) + "PageBreak Where ProjectCode='" + projectCode + "' and ( IsBid is not null and IsBid = 'Y') ");
		for (int z = 0; z < DT_PGBK.Rows.Count; z++)
		{
			if (!(DT_PGBK.Rows[z]["IsBid"].ToString() == "Y"))
			{
				continue;
			}
			int idx = GetDTDetailRowIndex(dsXML.Tables["Items"], (int)DT_PGBK.Rows[z]["SNo"]);
			if (idx <= -1)
			{
				continue;
			}
			DataRow dataRow;
			(dataRow = dsXML.Tables["Items"].Rows[idx])["memo"] = string.Concat(dataRow["memo"], "[發包]");
			if (sXMLKind == "2")
			{
				string kind = ((dsXML.Tables["Items"].Rows[idx]["kind"] != null) ? dsXML.Tables["Items"].Rows[idx]["kind"].ToString().ToUpper().Trim() : "#");
				if ("FUS".IndexOf(kind) > -1 && DT_PGBK.Rows[z]["IsFormulaChangeKind"] != null && DT_PGBK.Rows[z]["IsFormulaChangeKind"].ToString().ToUpper().Trim() == "Y")
				{
					dsXML.Tables["Items"].Rows[idx]["kind"] = "L";
					dsXML.Tables["Items"].Rows[idx]["cName"] = ((DT_PGBK.Rows[z]["FormulaNewName"] != null && DT_PGBK.Rows[z]["FormulaNewName"].ToString().Trim() != string.Empty) ? DT_PGBK.Rows[z]["FormulaNewName"].ToString().Trim() : dsXML.Tables["Items"].Rows[idx]["cName"]);
				}
			}
		}
		if (SysConfig.SysEnablePwrSet)
		{
			DBCLS = new DBClass();
			DBCLS._FS_UserID = userID;
			DataTable DT_P = DBCLS.GetUserDefine("Select pccesCode, PwrSet,Account from " + CommonMethods.GetActionNameString(FormActionName) + "ProjMrsA Where ProjectCode='" + projectCode + "'  and ((PwrSet is not null or PwrSet >0) or (Account is not null or Account  <> '')) ");
			for (int z = 0; z < DT_P.Rows.Count; z++)
			{
				if (DT_P.Rows[z]["PwrSet"].ToString() != "")
				{
					int idx = GetDTMrsRowIndex(dsXML.Tables["MrsBase"], DT_P.Rows[z]["pccesCode"].ToString().Trim());
					if (idx > -1)
					{
						DataRow dataRow;
						DataRow dataRow2 = (dataRow = dsXML.Tables["MrsBase"].Rows[idx]);
						object obj = dataRow["memo"];
						dataRow2["memo"] = string.Concat(obj, "[P.", DT_P.Rows[z]["PwrSet"].ToString().Trim(), ".]");
					}
					idx = GetDTMrsRowIndex(dsXML.Tables["Items"], DT_P.Rows[z]["pccesCode"].ToString().Trim());
					if (idx > -1)
					{
						DataRow dataRow;
						DataRow dataRow3 = (dataRow = dsXML.Tables["Items"].Rows[idx]);
						object obj = dataRow["memo"];
						dataRow3["memo"] = string.Concat(obj, "[P.", DT_P.Rows[z]["PwrSet"].ToString().Trim(), ".]");
					}
				}
				if (DT_P.Rows[z]["Account"].ToString() != "")
				{
					int idx = GetDTMrsRowIndex(dsXML.Tables["MrsBase"], DT_P.Rows[z]["pccesCode"].ToString().Trim());
					if (idx > -1)
					{
						DataRow dataRow;
						DataRow dataRow4 = (dataRow = dsXML.Tables["MrsBase"].Rows[idx]);
						object obj = dataRow["memo"];
						dataRow4["memo"] = string.Concat(obj, "[A.", DT_P.Rows[z]["Account"].ToString().Trim(), ".]");
					}
					idx = GetDTMrsRowIndex(dsXML.Tables["Items"], DT_P.Rows[z]["pccesCode"].ToString().Trim());
					if (idx > -1)
					{
						DataRow dataRow;
						DataRow dataRow5 = (dataRow = dsXML.Tables["Items"].Rows[idx]);
						object obj = dataRow["memo"];
						dataRow5["memo"] = string.Concat(obj, "[A.", DT_P.Rows[z]["Account"].ToString().Trim(), ".]");
					}
				}
			}
		}
		DBCLS = new DBClass();
		DBCLS._FS_UserID = userID;
		DT_PGBK = DBCLS.GetUserDefine("Select SNo,PrintToAnalysis from " + CommonMethods.GetActionNameString(FormActionName) + "ItemA Where ProjectCode='" + projectCode + "' ");
		for (int z = 0; z < DT_PGBK.Rows.Count; z++)
		{
			if (DT_PGBK.Rows[z]["PrintToAnalysis"].ToString() == "1")
			{
				int idx = GetDTDetailRowIndex(dsXML.Tables["Items"], (int)DT_PGBK.Rows[z]["SNo"]);
				if (idx > -1)
				{
					DataRow dataRow;
					(dataRow = dsXML.Tables["Items"].Rows[idx])["memo"] = string.Concat(dataRow["memo"], "[印單]");
				}
			}
		}
		if (Is75094900())
		{
			dsXML.Tables["Items"].Columns.Add("BudgetCode", Type.GetType("System.String"));
			for (int i = 0; i < dsXML.Tables["Items"].Rows.Count; i++)
			{
				string sPrintNO = dsXML.Tables["Items"].Rows[i]["printNo"].ToString();
				string sCustomPrintNo = ChagePrintNoToCustomBudgetCode(sPrintNO);
				dsXML.Tables["Items"].Rows[i]["BudgetCode"] = sCustomPrintNo;
			}
			dsXML.Tables["Analysis"].Columns.Add("BudgetCode", Type.GetType("System.String"));
			for (int i = 0; i < dsXML.Tables["Analysis"].Rows.Count; i++)
			{
				string sPccesCode = dsXML.Tables["Analysis"].Rows[i]["pubcode"].ToString();
				dsXML.Tables["Analysis"].Rows[i]["BudgetCode"] = sPccesCode;
			}
		}
		if (toExport)
		{
			dsXML.Tables["Project"].Columns.Add("CorrectRatio", typeof(string));
			dsXML.Tables["Project"].Columns.Add("FitRatio", typeof(string));
			dsXML.Tables["Project"].Columns.Add("WeightCorrectRatio", typeof(string));
			dsXML.Tables["Project"].Columns.Add("WeightFitRatio", typeof(string));
			dsXML.Tables["Project"].Rows[0]["CorrectRatio"] = dCorrectRatio.ToString("00.00") + "%";
			dsXML.Tables["Project"].Rows[0]["FitRatio"] = dFitRatio.ToString("00.00") + "%";
			dsXML.Tables["Project"].Rows[0]["WeightCorrectRatio"] = dWeightCorrectRatio.ToString("00.00") + "%";
			dsXML.Tables["Project"].Rows[0]["WeightFitRatio"] = dWeightFitRatio.ToString("00.00") + "%";
			if (FormActionName == PccesFormAction.BUD)
			{
				shareVDF1 = ArchConvert.Obj2Decimal(dsProject.Tables[0].Rows[0]["shareVDF1"]);
				shareVDF1sNo = ArchConvert.Obj2Int(dsProject.Tables[0].Rows[0]["shareVDF1sNo"]);
				dsXML.Tables["Project"].Rows[0]["shareVDF1"] = shareVDF1.ToString("00");
				dsXML.Tables["Project"].Rows[0]["shareVDF1sNo"] = shareVDF1sNo.ToString();
			}
		}
		if (flag)
		{
			FileInfo FI = new FileInfo("C:\\Program Files\\8MFree.txt");
			ChgXMLStru XMLCom = new ChgXMLStru();
			XMLCom._ProjFlag = ProjectFlag;
			if (FI.Exists)
			{
				XMLCom._EightFlag = false;
			}
			string printMode = dsXML.Tables["Project"].Rows[0]["PrintMode"].ToString();
			if (FormActionName == PccesFormAction.BUD)
			{
				XMLCom._isRequestBidAnalysisShowQtyL = StringToBoolean(printMode.Substring(47, 1));
				XMLCom._isRequestBidAnalysisShowQtyE = StringToBoolean(printMode.Substring(48, 1));
				XMLCom._isRequestBidAnalysisShowQtyM = StringToBoolean(printMode.Substring(49, 1));
				XMLCom._isRequestBidAnalysisShowQtyW = StringToBoolean(printMode.Substring(50, 1));
			}
			bool output102XMLformat = false;
			if (chkXMLformat102.Checked)
			{
				output102XMLformat = true;
			}
			XMLCom._CurrentActionName = projcom.GetCurrentProjectActionName(projcom.ps_projectCode);
			XMLCom.OutputXML1(dsXML, GetOutputFilePath() + ".xml", outItem: true, IsOutAnalysis, outResource: true, sXMLKind, ps_ShowCost, FormActionName.ToString(), output102XMLformat);
			PubTools.WriteRoughlyLog(aArr);
		}
		else
		{
			string MDBPath = ApplicationDirectory + "\\Report\\";
			string GUIDCode = Guid.NewGuid().ToString();
			SysUser oSysUser = new SysUser();
			string DBName = oSysUser.GetSysUserDatabaseName(userID);
			string DocumentPath = ApplicationDirectory + "\\AddOn\\" + DBName + "\\" + projectCode + "\\";
			CommonMethods.CreateReport(dsXML, tbFileName.Text, MDBPath + GUIDCode + ".mdb", MDBPath);
			MyZip MyZip1 = new MyZip();
			string[] Efiles = new string[2]
			{
				MDBPath + GUIDCode + ".mdb",
				DocumentPath
			};
			if (Is75094900())
			{
				File.Copy(Efiles[0], GetOutputFilePath() + ".mdb", overwrite: true);
			}
			else
			{
				MyZip1.AddFiles(GetOutputFilePath() + ".zmd", Efiles, "ARCH13139409");
			}
			GC.Collect();
		}
		projcom = null;
		Cursor = Cursors.Default;
	}

	private string ChagePrintNoToCustomBudgetCode(string inputStr)
	{
		DBClass dbC = new DBClass();
		dbC._FS_UserID = userID;
		string retV = "";
		string[] printNOs = new string[8] { "", "", "", "", "", "", "", "" };
		if (inputStr.Length > 28)
		{
			printNOs[7] = inputStr.Substring(28, 4);
		}
		if (inputStr.Length > 24)
		{
			printNOs[6] = inputStr.Substring(24, 4);
		}
		if (inputStr.Length > 20)
		{
			printNOs[5] = inputStr.Substring(20, 4);
		}
		if (inputStr.Length > 16)
		{
			printNOs[4] = inputStr.Substring(16, 4);
		}
		if (inputStr.Length > 12)
		{
			printNOs[3] = inputStr.Substring(12, 4);
		}
		if (inputStr.Length > 8)
		{
			printNOs[2] = inputStr.Substring(8, 4);
		}
		if (inputStr.Length > 4)
		{
			printNOs[1] = inputStr.Substring(4, 4);
		}
		if (inputStr.Length > 0)
		{
			printNOs[0] = inputStr.Substring(0, 4);
		}
		if (printNOs[0] != "")
		{
			printNOs[0] = "A";
		}
		if (printNOs[1] != "")
		{
			printNOs[1] = Convert.ToString((char)(PubTools.Str2Int(printNOs[1]) + 64));
		}
		if (printNOs[2] != "")
		{
			printNOs[2] = Convert.ToString((char)(PubTools.Str2Int(printNOs[2]) + 64));
		}
		string PrintNoL3 = ((inputStr.Length > 8) ? inputStr.Substring(0, 12) : inputStr);
		int iL3Count = dbC.GetPrintNoCount(projectCode, FormActionName.ToString(), PrintNoL3);
		if (printNOs[3] != "")
		{
			printNOs[3] = PubTools.Str2Int(printNOs[3]).ToString().PadLeft(iLevelLen(iL3Count), '0');
		}
		string PrintNoL4 = ((inputStr.Length > 12) ? inputStr.Substring(0, 16) : inputStr);
		int iL4Count = dbC.GetPrintNoCount(projectCode, FormActionName.ToString(), PrintNoL4);
		if (printNOs[4] != "")
		{
			printNOs[4] = PubTools.Str2Int(printNOs[4]).ToString().PadLeft(iLevelLen(iL4Count), '0');
		}
		string PrintNoL5 = ((inputStr.Length > 16) ? inputStr.Substring(0, 20) : inputStr);
		int iL5Count = dbC.GetPrintNoCount(projectCode, FormActionName.ToString(), PrintNoL5);
		if (printNOs[5] != "")
		{
			printNOs[5] = PubTools.Str2Int(printNOs[5]).ToString().PadLeft(iLevelLen(iL5Count), '0');
		}
		string PrintNoL6 = ((inputStr.Length > 20) ? inputStr.Substring(0, 24) : inputStr);
		int iL6Count = dbC.GetPrintNoCount(projectCode, FormActionName.ToString(), PrintNoL6);
		if (printNOs[6] != "")
		{
			printNOs[6] = PubTools.Str2Int(printNOs[6]).ToString().PadLeft(iLevelLen(iL6Count), '0');
		}
		string PrintNoL7 = ((inputStr.Length > 24) ? inputStr.Substring(0, 28) : inputStr);
		int iL7Count = dbC.GetPrintNoCount(projectCode, FormActionName.ToString(), PrintNoL7);
		if (printNOs[7] != "")
		{
			printNOs[7] = PubTools.Str2Int(printNOs[7]).ToString().PadLeft(iLevelLen(iL7Count), '0');
		}
		if (printNOs[0] != "")
		{
			retV += printNOs[0];
		}
		if (printNOs[1] != "")
		{
			retV += printNOs[1];
		}
		if (printNOs[2] != "")
		{
			retV += printNOs[2];
		}
		if (printNOs[3] != "")
		{
			retV = retV + "_" + printNOs[3];
		}
		if (printNOs[4] != "")
		{
			retV = retV + "_" + printNOs[4];
		}
		if (printNOs[5] != "")
		{
			retV = retV + "_" + printNOs[5];
		}
		if (printNOs[6] != "")
		{
			retV = retV + "_" + printNOs[6];
		}
		if (printNOs[7] != "")
		{
			retV = retV + "_" + printNOs[7];
		}
		if (inputStr == "99999999999999999999999999999999")
		{
			retV = "A";
		}
		return retV;
	}

	private int iLevelLen(int inputVal)
	{
		int retV = 1;
		if (inputVal >= 0 && inputVal < 9)
		{
			retV = 1;
		}
		else if (inputVal >= 10 && inputVal < 99)
		{
			retV = 2;
		}
		else if (inputVal >= 100 && inputVal < 999)
		{
			retV = 3;
		}
		else if (inputVal >= 1000 && inputVal < 9999)
		{
			retV = 4;
		}
		return retV;
	}

	private int GetDTDetailRowIndex(DataTable DT_New, int iSNo)
	{
		int RetV = -1;
		for (int i = 0; i < DT_New.Rows.Count; i++)
		{
			if (PubTools.Str2Int(DT_New.Rows[i]["sNo"]) == iSNo)
			{
				RetV = i;
				break;
			}
		}
		return RetV;
	}

	private int GetDTMrsRowIndex(DataTable DT_New, string spccesCode)
	{
		int RetV = -1;
		for (int i = 0; i < DT_New.Rows.Count; i++)
		{
			if (DT_New.Rows[i]["pccesCode"].ToString().Trim() == spccesCode)
			{
				RetV = i;
				break;
			}
		}
		return RetV;
	}

	private int ExportExcel(string FilePath)
	{
		int result = 0;
		ArrayList logArray = new ArrayList();
		logArray.Add(userID);
		logArray.Add("預算書 EXCEL 轉出");
		switch (FormActionName)
		{
		case PccesFormAction.Invoice:
		case PccesFormAction.SubClose:
		case PccesFormAction.SubFinal:
			chkIncludeCostBreakdownList.Checked = false;
			break;
		}
		SetUpExcelSettings(FilePath);
		if (ExcelSettingsFromDB)
		{
			SetUpExcelOutputOptionsFromDB();
		}
		else
		{
			SetUpExcelOutputOptions();
		}
		CNVSN.EnableOldExportExcel = EnableOldExportExcelMethod();
		result = CNVSN.ExecuteExp();
		if (CNVSN.WarningMessage != string.Empty)
		{
			MessageBox.Show(this, CNVSN.WarningMessage, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		CNVSN = null;
		switch (result)
		{
		case 1:
			MessageBox.Show(this, "印表機裝置有誤，請檢查或確認你已經安裝了印表機！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			break;
		case 2:
			MessageBox.Show(this, "無法啟動您電腦上的 EXCEL，請先確認你已經安裝 EXCEL 97 以上的版本。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			break;
		}
		GC.Collect();
		return result;
	}

	private void SetUpExcelSettings(string FilePath)
	{
		CNVSN = new Conversion();
		CNVSN._UserID = userID;
		CNVSN._ProjectCode = projectCode;
		CNVSN._SrcKind = CommonMethods.GetActionNameString(FormActionName);
		CNVSN._FileName = FilePath;
		CNVSN._ProjectNameC = ProjectChineseName;
		CNVSN._ProjectNameE = ProjectEnglishName;
		CNVSN._ProjectAddress = ProjectChineseAddress;
		CNVSN._ProjectEngAddress = ProjectEnglishAddress;
		CNVSN._AccountCode1 = AccountCodeLower;
		CNVSN._AccountCode2 = AccountCodeUpper;
		CNVSN._ReportPath = ApplicationDirectory;
		CNVSN._chgCount = chgCount;
		CNVSN._queue = invoiceCount;
		string detailMaster = CommonMethods.IniReadValue(ApplicationDirectory + "OptionSet.ini", "BreakDownData", "DetailMaster");
		CNVSN._DetailMaster = detailMaster.ToUpper() == "TRUE";
		CNVSN.OutputAliasAsItemName = chkOutputAliasAsItemName.Checked;
		CNVSN.BidFooterPrintVendorInfo = BidFooterPrintVendorInfo;
		CNVSN.ProjectDescription = ProjectDescription;
		if (CommonMethods.GetActionNameString(FormActionName).ToUpper() == "SUB")
		{
			CNVSN.ProjectDescription = dsProject.Tables[0].Rows[0]["projectDescription"].ToString();
		}
		if (Preview)
		{
			CNVSN._ShowCost = rbPreviewBudget.Checked;
			CNVSN._ExportSheets = GetPreviewSheets();
			CNVSN._ShowAnalysis = chkPreviewIncludeCostBreakdownList.Checked;
			CNVSN.DeptName = tbPreviewHeader.Text;
			CNVSN.DeptEName = tbPreviewEnglishHeader.Text;
			CNVSN.Footer = ddlPreviewBudgetFooter.Text;
			CNVSN._IsLockAn = false;
			CNVSN._IsPrice = !rbPreviewBid.Checked && BudgetPrintPrice;
		}
		else
		{
			if (rbOutputBudget.Checked)
			{
				if (IsBlankBudget)
				{
					CNVSN._ShowCost = false;
					CNVSN.IsBlankBudget = true;
				}
				else
				{
					CNVSN._ShowCost = true;
				}
			}
			CNVSN._ExportSheets = ((FormActionName == PccesFormAction.BUD) ? ExportSheets : "1111");
			CNVSN._ShowAnalysis = chkIncludeCostBreakdownList.Checked && chkSubmitIncludeCostBreakdownList.Checked;
			CNVSN.DeptName = tbHeader.Text;
			CNVSN.DeptEName = tbEnglishHeader.Text;
			if (ddlBudgetFooter.SelectedItem == null && ddlBudgetFooter.Text.Trim() == "")
			{
				CNVSN.Footer = "計算                    審核                    覆核";
				CNVSN.Footer = "";
			}
			else
			{
				CNVSN.Footer = ddlBudgetFooter.Text;
			}
			CurrentBudgetFormAction = GetCurrentBDGT_Type();
			if (CurrentBudgetFormAction.ToUpper() == "CNT")
			{
				CNVSN.Footer = "投標廠商：\u3000\u3000\u3000\u3000\u3000\u3000\u3000[印]\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000負責人：\u3000\u3000\u3000\u3000\u3000[印]";
			}
			CNVSN._IsLockAn = !rbOutputBudget.Checked && chkBreakdownListLockCost.Checked;
			if (IsBlankBudget)
			{
				CNVSN._IsPrice = false;
			}
			else
			{
				CNVSN._IsPrice = !rbOutputBid.Checked && BudgetPrintPrice;
			}
		}
		if (BidFooterPrintVendorInfo)
		{
			SetVendorInfo();
		}
	}

	private string GetPreviewSheets()
	{
		return BooleanToString(chkOutputSummary.Checked) + BooleanToString(chkOutputDetailList.Checked) + BooleanToString(chkOutputBreakdownList.Checked) + BooleanToString(chkOutputResourceList.Checked);
	}

	private void SetVendorInfo()
	{
		if (dsSubMemo.Tables[0].Rows.Count > 0)
		{
			string invoiceNo = dsSubMemo.Tables[0].Rows[0]["FACTORY_ID"].ToString();
			Archnowledge.Pcces.DomainModule.General.Sublet sublet = new Archnowledge.Pcces.DomainModule.General.Sublet();
			DataSet dsSublet = sublet.GetSublet(invoiceNo);
			if (dsSublet.Tables[0].Rows.Count > 0)
			{
				CNVSN.VendorName = dsSublet.Tables[0].Rows[0]["title"].ToString();
				CNVSN.VendorOwner = dsSublet.Tables[0].Rows[0]["boss"].ToString();
			}
		}
	}

	private void SetUpExcelOutputOptionsFromDB()
	{
		dsProject = project.GetProject(projectCode);
		if (dsProject.Tables[0].Rows.Count <= 0 || !(dsProject.Tables[0].Rows[0]["printMode"].ToString() != string.Empty))
		{
			return;
		}
		string printMode = dsProject.Tables[0].Rows[0]["printMode"].ToString();
		CNVSN._SummaryIsIncWorkItem = StringToBoolean(printMode.Substring(0, 1));
		CNVSN._SummaryLevel = ArchConvert.Obj2Int(printMode.Substring(1, 1));
		CNVSN._Ismainprice = StringToBoolean(printMode.Substring(5, 1));
		CNVSN._IsDetMemo = StringToBoolean(printMode.Substring(10, 1));
		CNVSN._IsDetPccesCode = StringToBoolean(printMode.Substring(11, 1));
		CNVSN._IsDetAnaMark = StringToBoolean(printMode.Substring(12, 1));
		CNVSN._DetAnaMark = printMode.Substring(13, 1);
		CNVSN._IsDetExtCode = StringToBoolean(printMode.Substring(14, 1));
		CNVSN._IsAnaMemo = StringToBoolean(printMode.Substring(20, 1));
		CNVSN._IsAnaPccesCode = StringToBoolean(printMode.Substring(21, 1));
		CNVSN._IsAnaAnaMark = StringToBoolean(printMode.Substring(22, 1));
		CNVSN._AnaMark = printMode.Substring(23, 1);
		CNVSN._IsAnaExtCode = StringToBoolean(printMode.Substring(24, 1));
		CNVSN._IsRepeatDetailAnalysis = StringToBoolean(printMode.Substring(27, 1));
		CNVSN._IsAnaHalfPage = StringToBoolean(printMode.Substring(28, 1));
		CNVSN._IsSkipCommentItem = StringToBoolean(printMode.Substring(29, 1));
		CNVSN._IsSkipSubTotalItem = StringToBoolean(printMode.Substring(30, 1));
		CNVSN._AnaSortOrder = printMode.Substring(25, 1);
		CNVSN._AnaRepeat = printMode.Substring(26, 1);
		CNVSN._AutoShrink = StringToBoolean(printMode.Substring(35, 1));
		CNVSN._IsNoMiddle = StringToBoolean(printMode.Substring(36, 1));
		CNVSN._sFontName = printMode.Substring(39, 3);
		if (printMode.Length > 47)
		{
			CNVSN._isRequestBidAnalysisShowQtyL = StringToBoolean(printMode.Substring(47, 1));
			CNVSN._isRequestBidAnalysisShowQtyE = StringToBoolean(printMode.Substring(48, 1));
			CNVSN._isRequestBidAnalysisShowQtyM = StringToBoolean(printMode.Substring(49, 1));
			CNVSN._isRequestBidAnalysisShowQtyW = StringToBoolean(printMode.Substring(50, 1));
			if (FormActionName == PccesFormAction.BUD || FormActionName == PccesFormAction.BID)
			{
				CNVSN._ChtNEng = StringToBoolean(printMode.Substring(51, 1));
			}
			else
			{
				CNVSN._ChtNEng = false;
			}
			CNVSN._IsShowDate = StringToBoolean(printMode.Substring(52, 1));
			CNVSN._PrintDate = ArchConvert.Obj2DateTime(printMode.Substring(53, 10));
			if (FormActionName == PccesFormAction.SubClose || FormActionName == PccesFormAction.SubFinal)
			{
				CNVSN._IsPrintSummaryInBID = false;
			}
			else
			{
				CNVSN._IsPrintSummaryInBID = StringToBoolean(printMode.Substring(63, 1));
			}
			CNVSN._BidLast = StringToBoolean(printMode.Substring(64, 1));
			CNVSN._detailListShowAnalysisItemCode = StringToBoolean(printMode.Substring(65, 1));
			CNVSN._DetailListPrintProjectDescription = printMode.Length > 66 && StringToBoolean(printMode.Substring(66, 1));
		}
		if (printMode.Length > 67)
		{
			CNVSN._TakePlaceByMaxValue = printMode.Length > 67 && StringToBoolean(printMode.Substring(67, 1));
		}
		if (printMode.Length > 68)
		{
			CNVSN._ShowCodeCorrectRate = printMode.Length > 68 && StringToBoolean(printMode.Substring(68, 1));
		}
		if (printMode.Length > 69)
		{
			CNVSN._SummaryPrintProjectDescription = printMode.Length > 69 && StringToBoolean(printMode.Substring(69, 1));
		}
	}

	private void SetUpExcelOutputOptions()
	{
		CNVSN._SummaryIsIncWorkItem = SummaryIncludeWorkItem;
		CNVSN._SummaryLevel = SummaryPrintLevel;
		CNVSN._Ismainprice = DetailListDisplayMainItemDetail;
		CNVSN._IsDetMemo = DetailListShowRemark;
		CNVSN._IsDetPccesCode = DetailListShowPccesCode;
		CNVSN._IsDetAnaMark = DetailListShowAnalysisItemSymbol;
		CNVSN._DetAnaMark = DetailListAnalysisItemMark;
		CNVSN._IsDetExtCode = DetailListShowUnofficialItemCode;
		CNVSN._IsAnaMemo = BreakdownListShowRemark;
		CNVSN._IsAnaPccesCode = BreakdownListShowPccesCode;
		CNVSN._IsAnaAnaMark = BreakdownListShowAnalysisItemSymbol;
		CNVSN._AnaMark = BreakdownListAnalysisItemMark;
		CNVSN._IsAnaExtCode = BreakdownListShowUnofficialItemCode;
		CNVSN._IsRepeatDetailAnalysis = DuplicateAnalysisItemInDetailList;
		CNVSN._IsAnaHalfPage = HalfPageFormat;
		CNVSN._IsSkipCommentItem = SkipCommentItem;
		CNVSN._IsSkipSubTotalItem = SkipSubTotalItem;
		CNVSN._AnaSortOrder = BreakdownListSortOption;
		CNVSN._AnaRepeat = BreakdownListDuplicationOption;
		CNVSN._AutoShrink = ShrinkToFit;
		CNVSN._IsNoMiddle = NoBorderInLineBreak;
		CNVSN._sFontName = ExcelFontName;
		CNVSN._isRequestBidAnalysisShowQtyL = PrintLaborQty;
		CNVSN._isRequestBidAnalysisShowQtyE = PrintEquipmentQty;
		CNVSN._isRequestBidAnalysisShowQtyM = PrintMaterialQty;
		CNVSN._isRequestBidAnalysisShowQtyW = PrintMiscellaneaQty;
		if (FormActionName == PccesFormAction.BUD || FormActionName == PccesFormAction.BID)
		{
			CNVSN._ChtNEng = WithEnglish;
		}
		else
		{
			CNVSN._ChtNEng = false;
		}
		CNVSN._IsShowDate = PrintDate;
		CNVSN._PrintDate = DatePrinted;
		CNVSN._IsPrice = BudgetPrintPrice;
		if (FormActionName == PccesFormAction.SubClose || FormActionName == PccesFormAction.SubFinal)
		{
			CNVSN._IsPrintSummaryInBID = false;
		}
		else
		{
			CNVSN._IsPrintSummaryInBID = BidPrintSummary;
		}
		CNVSN._BidLast = BidFooterPrintBidder;
		CNVSN._detailListShowAnalysisItemCode = DetailListShowAnalysisItemCode;
		CNVSN._DetailListPrintProjectDescription = DetailListPrintProjectDescription;
		CNVSN._TakePlaceByMaxValue = TakePlaceByMaxValue;
		CNVSN._ShowCodeCorrectRate = ShowCodeCorrectRate;
		CNVSN._SummaryPrintProjectDescription = SummaryPrintProjectDescription;
	}

	private bool StringToBoolean(string ZeroOrOne)
	{
		return ZeroOrOne == "1";
	}

	private string BooleanToString(bool booleanValue)
	{
		return booleanValue ? "1" : "0";
	}

	private bool EnableOldExportExcelMethod()
	{
		Archnowledge.Pcces.DomainModule.General.PubProject thePubProject = new Archnowledge.Pcces.DomainModule.General.PubProject();
		return !thePubProject.GetPubProjectEnableNewCalculateCost(projectCode) || EnableOldExportExcel;
	}

	private void chkOutputXML_CheckedChanged(object sender, EventArgs e)
	{
		if ((chkOutputXML.Checked && projectCode.Length > 40) || (chkOutputXML.Checked && MainProjectCode.Trim() != ""))
		{
			lblSaveAs.Visible = true;
			txtSaveAsProjectCode.Visible = true;
		}
		else
		{
			lblSaveAs.Visible = false;
			txtSaveAsProjectCode.Visible = false;
		}
		if (chkOutputXML.Checked)
		{
			chkXMLformat102.Enabled = true;
		}
		else
		{
			chkXMLformat102.Enabled = false;
		}
		if (FormActionName == PccesFormAction.BUD && chkOutputXML.Checked && !rbOutputBid.Checked)
		{
			groupBox7.Visible = true;
			Cursor = Cursors.WaitCursor;
			getCodeIndex();
			lbl_correctRate.Text = $"{dCorrectRatio:N2}" + " %";
			lbl_WeightCorrectRatio.Text = $"{dWeightCorrectRatio:N2}" + " %";
			lbl_confirmRate.Text = $"{dFitRatio:N2}" + " %";
			lbl_WeightFitRatio.Text = $"{dWeightFitRatio:N2}" + " %";
			base.Width = 844;
			Cursor = Cursors.Default;
			timer2.Enabled = true;
		}
	}

	private void chkOutputExcel_CheckedChanged(object sender, EventArgs e)
	{
		if (chkOutputExcel.Checked)
		{
			btnHeaderAndFooterSetting.Visible = true;
		}
		else
		{
			gbHeaderAndFooter.Visible = false;
			btnHeaderAndFooterSetting.Visible = false;
			base.Height = FormNormalHeight;
		}
		if (FormActionName == PccesFormAction.BID)
		{
			btnHeaderAndFooterSetting.Visible = false;
		}
		if (FormActionName != PccesFormAction.BID && FormActionName != PccesFormAction.BUD)
		{
			btnOpenExcelOption.Visible = true;
		}
	}

	private void B_Btn_Prev_Click(object sender, EventArgs e)
	{
		base.Height = FormNormalHeight;
		if (FormActionName == PccesFormAction.BID || FormActionName == PccesFormAction.SplitContract || FormActionName == PccesFormAction.SubChange)
		{
			Tab_A1.Tab.Selected = true;
		}
		else
		{
			Tab_A.Tab.Selected = true;
		}
	}

	private void D_Btn_Prev_Click(object sender, EventArgs e)
	{
		Tab_B.Tab.Selected = true;
		progressBarTotal.Value = 0;
		progressBarSingle.Value = 0;
	}

	private void D_Btn_Fnsh_Click(object sender, EventArgs e)
	{
		string iniFilePath = ApplicationDirectory + "PccesMain.ini";
		CommonMethods.IniWriteValue(iniFilePath, "FormBudget", "ExportPath", tbOutputPath.Text.Trim());
	}

	private void tbOutputPath_Validating(object sender, CancelEventArgs e)
	{
		if (!CommonMethods.CheckValidString((sender as UltraTextEditor).Text))
		{
			e.Cancel = true;
		}
	}

	private void btnOpenExcepOption_Click(object sender, EventArgs e)
	{
		OpenExportExcelOption(rbOutputBudget.Checked, isPreview: false);
	}

	private void btnPreviewExcelOption_Click(object sender, EventArgs e)
	{
		OpenExportExcelOption(rbPreviewBudget.Checked, isPreview: true);
	}

	private void OpenExportExcelOption(bool outputBudget, bool isPreview)
	{
		FormBudgetExp_WzdOption formBudgetExp_WzdOption = new FormBudgetExp_WzdOption();
		formBudgetExp_WzdOption._ProjectCode = projectCode;
		formBudgetExp_WzdOption._UserID = userID;
		formBudgetExp_WzdOption._ActionName = FormActionName;
		formBudgetExp_WzdOption._OutputBudget = outputBudget;
		formBudgetExp_WzdOption._IsSubmit = isSubmit;
		formBudgetExp_WzdOption._IsPreview = Preview;
		formBudgetExp_WzdOption.Owner = this;
		formBudgetExp_WzdOption.ShowDialog();
		formBudgetExp_WzdOption.Close();
		formBudgetExp_WzdOption.Dispose();
		formBudgetExp_WzdOption = null;
	}

	private void rbOutputBid_CheckedChanged(object sender, EventArgs e)
	{
		chkBreakdownListLockCost.Visible = rbOutputBid.Checked;
	}

	private void btnHeaderAndFooterSetting_Click(object sender, EventArgs e)
	{
		if (btnHeaderAndFooterSetting.Text == "表頭表尾設定▼")
		{
			base.Height = FormExpandedHeight;
			gbHeaderAndFooter.Visible = true;
			btnHeaderAndFooterSetting.Text = "表頭表尾設定▲";
		}
		else
		{
			base.Height = FormNormalHeight;
			gbHeaderAndFooter.Visible = false;
			btnHeaderAndFooterSetting.Text = "表頭表尾設定▼";
		}
	}

	private void GetOutputFileName()
	{
		string timeNow = $"{DateTime.Now:yyyyMMddHHmmss}";
		string chineseDocumentType = string.Empty;
		string englishDocumentType = string.Empty;
		switch (FormActionName)
		{
		case PccesFormAction.BUD:
			chineseDocumentType = (rbOutputBudget.Checked ? "(預算書)" : "(空白標單)");
			englishDocumentType = (rbOutputBudget.Checked ? "_ap_bdgt" : "_bp_rbid");
			break;
		case PccesFormAction.BID:
			chineseDocumentType = "(投標單)";
			englishDocumentType = "_bp_sbid";
			break;
		case PccesFormAction.SplitContract:
			chineseDocumentType = "(契約)";
			englishDocumentType = "_bp_Cnt";
			break;
		case PccesFormAction.SubChange:
			chineseDocumentType = "(契約變更)";
			englishDocumentType = "_bp_ChgCnt";
			break;
		case PccesFormAction.Invoice:
			chineseDocumentType = "(估驗計價)";
			englishDocumentType = "_bp_Payment";
			break;
		case PccesFormAction.SubClose:
			chineseDocumentType = "(契約結算)";
			englishDocumentType = "_bp_Scl";
			break;
		case PccesFormAction.SubFinal:
			chineseDocumentType = "(契約決算)";
			englishDocumentType = "_bp_Sfl";
			break;
		}
		if (FormActionName == PccesFormAction.BUD && CurrentBudgetFormAction.ToUpper() == "CNT")
		{
			chineseDocumentType = "(契約書)";
			englishDocumentType = "_ap_cnt";
		}
		if (chkUseProjectCodeAsFileName.Checked)
		{
			tbFileName.Text = Utility.ReplaceFileNameInvalidChar(chineseDocumentType + ProjectChineseName + projectCode + englishDocumentType);
			CommonMethods.WriteIniValue("CheckBox", "State", "True");
		}
		else
		{
			tbFileName.Text = "Output_" + timeNow + englishDocumentType;
			CommonMethods.WriteIniValue("CheckBox", "State", "False");
		}
	}

	private void chkUseProjectCodeAsFileName_CheckedChanged(object sender, EventArgs e)
	{
		GetOutputFileName();
	}

	private void chkIncludeCostBreakdownList_CheckedChanged(object sender, EventArgs e)
	{
		if (!chkIncludeCostBreakdownList.Checked && FormActionName != PccesFormAction.Invoice && FormActionName != PccesFormAction.SubClose && FormActionName != PccesFormAction.SubFinal)
		{
			MessageBox.Show(this, "預算書不含單價分析，匯出的資源統計表將不會出現單價分析內之工項！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void chkSubmitIncludeCostBreakdownList_CheckedChanged(object sender, EventArgs e)
	{
		if (!chkSubmitIncludeCostBreakdownList.Checked && FormActionName == PccesFormAction.BID)
		{
			MessageBox.Show(this, "注意：投標單不含單價分析，匯出的資源統計表將不會出現單價分析內之工項，\n\n會造成頁數與空白標單之資源統計表頁數不符，\n\n請先確認業主是否要列印單價分析，以免造成廢標！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void btnOpenOutputFolderBrowser_Click(object sender, EventArgs e)
	{
		OutputFileFolderBrowser.Description = "請挑選你要輸出的路徑";
		if (tbOutputPath.Text.Trim() != string.Empty)
		{
			OutputFileFolderBrowser.SelectedPath = tbOutputPath.Text.Trim();
		}
		if (OutputFileFolderBrowser.ShowDialog() == DialogResult.OK)
		{
			tbOutputPath.Text = OutputFileFolderBrowser.SelectedPath;
		}
	}

	private void btnOpenDirectory_Click(object sender, EventArgs e)
	{
		ShellExecute SHExe = new ShellExecute();
		SHExe.OwnerHandle = base.Handle;
		SHExe.Parameters = tbOutputPath.Text.Trim();
		SHExe.Path = tbOutputPath.Text.Trim();
		SHExe.Execute();
	}

	private void btnOpenExcel_Click(object sender, EventArgs e)
	{
		ShellExecute SHExe = new ShellExecute();
		SHExe.OwnerHandle = base.Handle;
		SHExe.Parameters = lbOutputExcelFileName.Text;
		SHExe.Path = lbOutputExcelFileName.Text;
		SHExe.Execute();
	}

	private void llbXMLStandard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		ShellExecute SHExe = new ShellExecute();
		SHExe.OwnerHandle = base.Handle;
		SHExe.Path = "http://210.69.177.70/XMLPlan/";
		SHExe.Execute();
	}

	private void btnXMLInstruction_Click(object sender, EventArgs e)
	{
		FormBudgetExp_Wzd_Help1 FM_HELP1 = new FormBudgetExp_Wzd_Help1();
		FM_HELP1.ShowDialog();
		FM_HELP1.Close();
		FM_HELP1.Dispose();
		FM_HELP1 = null;
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		if (IsExcelStarted && CNVSN != null)
		{
			progressBarTotal.Maximum = CNVSN._TotalProgMax;
			progressBarTotal.Minimum = CNVSN._TotalProgMin;
			progressBarTotal.Value = CNVSN._TotalProgPos;
			progressBarSingle.Text = CNVSN._ExpStatus;
			progressBarSingle.Maximum = CNVSN._ProgMax;
			progressBarSingle.Minimum = CNVSN._ProgMin;
			progressBarSingle.Value = CNVSN._ProgPos;
		}
	}

	private void btnPreview_Click(object sender, EventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		string tempPath = string.Format("{0}PccesExcelPreview{1}.xls", Path.GetTempPath(), DateTime.Now.ToString("yyyyMMddHHmmss"));
		ExportExcel(tempPath);
		Process.Start(tempPath);
		Cursor = Cursors.Default;
	}

	private void rbPreviewBudget_CheckedChanged(object sender, EventArgs e)
	{
		if (rbPreviewBudget.Checked)
		{
			gbPreviewHeaderAndFooter.Visible = true;
			base.Height = FormPreviewHeight;
		}
	}

	private void rbPreviewBid_CheckedChanged(object sender, EventArgs e)
	{
		if (rbPreviewBid.Checked)
		{
			gbPreviewHeaderAndFooter.Visible = false;
			base.Height = FormPreviewCollapsedHeight;
		}
	}

	private void timer2_Tick(object sender, EventArgs e)
	{
		lbl_AutoNumUpd_Warn.Visible = !lbl_AutoNumUpd_Warn.Visible;
	}
}
