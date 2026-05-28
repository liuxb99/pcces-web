using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Common.Compress;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.CommonClass.Budget;
using Archnowledge.Pcces.DomainModule.Bid;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.DomainModule.LogicalBase;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.ArchControls.ProjectInfoSummaryControls;
using Archnowledge.Pcces.PccesMain.Budget.ItemNoset;
using Archnowledge.Pcces.PccesMain.SysMaintain;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using C1.Win.C1Sizer;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinSchedule;
using Infragistics.Win.UltraWinSchedule.CalendarCombo;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetProjectInfo : Form
{
	private const string iniFile = "OptionSet.ini";

	private IContainer components;

	private Panel panel1;

	private Panel panel3;

	private UltraTabControl Tab_ProjInfo;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_Basic;

	private UltraTabPageControl Tab_Other;

	private Panel panel4;

	private Panel panel5;

	private UltraLabel ultraLabel1;

	private UltraLabel lbChineseProjectName;

	private UltraButton btnOK;

	private UltraButton btnCancel;

	private Panel panel6;

	private Panel panel7;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel8;

	private UltraLabel ultraLabel9;

	private UltraLabel ultraLabel10;

	private UltraLabel ultraLabel11;

	private UltraLabel ultraLabel12;

	private UltraLabel ultraLabel13;

	private UltraLabel ultraLabel14;

	private UltraLabel ultraLabel15;

	private UltraLabel ultraLabel16;

	private UltraLabel ultraLabel17;

	private UltraLabel ultraLabel18;

	private UltraTabControl ultraTabControl1;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage2;

	private UltraTabPageControl SubTab1;

	private UltraTabPageControl SubTab2;

	private UltraTabPageControl SubTab3;

	private UltraTabPageControl SubTab4;

	private Panel panel8;

	private Panel panel9;

	private Panel panel10;

	private Panel panel11;

	private GroupBox gbConstructionType;

	private GroupBox gbBudget;

	private UltraLabel ultraLabel19;

	private UltraLabel ultraLabel20;

	private Panel panel12;

	private UltraLabel ultraLabel24;

	private UltraLabel ultraLabel25;

	private UltraLabel ultraLabel26;

	private UltraLabel ultraLabel27;

	private UltraLabel ultraLabel28;

	private UltraLabel ultraLabel29;

	private UltraLabel ultraLabel30;

	private UltraLabel ultraLabel31;

	private UltraLabel ultraLabel32;

	private UltraLabel ultraLabel33;

	private UltraLabel ultraLabel34;

	private UltraTextEditor tbBudEndYear;

	private UltraTextEditor tbWorkUnit;

	private UltraTextEditor tbBudStartYear;

	private UltraTextEditor tbWorkMode;

	private UltraTextEditor tbAccountCodeUpper;

	private UltraTextEditor tbExpectDuration;

	private UltraTextEditor tbAccountCodeLower;

	private UltraTextEditor tbBuyMode;

	private UltraTextEditor tbEnglishProjectName;

	private UltraTextEditor tbChineseProjectName;

	private UltraTextEditor tbProjectCode;

	private UltraTextEditor tbMainInstituteCode;

	private UltraTextEditor tbMainInstituite;

	private UltraButton btnPickMainInstitute;

	private UltraCalendarCombo txtExpectStartDate;

	private UltraComboEditor ddlProjectClassification;

	private Panel panel13;

	private C1Sizer c1Sizer1;

	private GridBudget gridCostKind;

	private UltraTextEditor txtMemo1_1;

	private UltraTextEditor txtMemo1_2;

	private UltraTextEditor txtMemo1_3;

	private UltraTextEditor txtMemo1_4;

	private UltraTextEditor txtMemo1_5;

	private UltraTextEditor txtMemo1_6;

	private UltraTextEditor txtMemo1_7;

	private UltraTextEditor txtMemo1_8;

	private UltraTextEditor txtMemo1_9;

	private UltraTextEditor txtMemo1_10;

	private UltraLabel lblMemo1_1;

	private UltraLabel lblMemo1_2;

	private UltraLabel lblMemo1_3;

	private UltraLabel lblMemo1_4;

	private UltraLabel lblMemo1_5;

	private UltraLabel lblMemo1_6;

	private UltraLabel lblMemo1_7;

	private UltraLabel lblMemo1_8;

	private UltraLabel lblMemo1_9;

	private UltraLabel lblMemo1_10;

	private C1Sizer c1Sizer2;

	private UltraTextEditor txtMemo2_1;

	private UltraTextEditor txtMemo2_2;

	private UltraTextEditor txtMemo2_3;

	private UltraTextEditor txtMemo2_4;

	private UltraTextEditor txtMemo2_5;

	private UltraLabel lblMemo2_1;

	private UltraLabel lblMemo2_2;

	private UltraLabel lblMemo2_3;

	private UltraLabel lblMemo2_4;

	private UltraLabel lblMemo2_5;

	private C1Sizer c1Sizer3;

	private UltraTextEditor txtMemo3_1;

	private UltraTextEditor txtMemo3_2;

	private UltraTextEditor txtMemo3_3;

	private UltraTextEditor txtMemo3_4;

	private UltraLabel lblMemo3_1;

	private UltraLabel lblMemo3_2;

	private UltraLabel lblMemo3_3;

	private UltraLabel lblMemo3_4;

	private Panel PNL_LOWER_1;

	private UltraLabel lblMainProjectCode;

	private Panel PNL_LOWER_2;

	private Panel PNL_LOWER_3;

	private UltraTextEditor txt7;

	private UltraTextEditor txt6;

	private UltraTextEditor txt5;

	private UltraTextEditor txt4;

	private UltraTextEditor txt3;

	private UltraCheckEditor chk19;

	private UltraCheckEditor chk18;

	private UltraCheckEditor chk17;

	private UltraCheckEditor chk16;

	private UltraCheckEditor chk15;

	private UltraCheckEditor chk14;

	private UltraCheckEditor chk13;

	private UltraCheckEditor chk12;

	private UltraCheckEditor chk11;

	private UltraCheckEditor chk10;

	private UltraCheckEditor chk09;

	private UltraCheckEditor chk08;

	private UltraCheckEditor chk07;

	private UltraCheckEditor chk06;

	private UltraCheckEditor chk05;

	private UltraCheckEditor chk04;

	private UltraCheckEditor chk03;

	private UltraCheckEditor chk02;

	private UltraCheckEditor chk01;

	private UltraCalendarCombo Combo5;

	private UltraCalendarCombo Combo4;

	private UltraCalendarCombo Combo3;

	private UltraCalendarCombo Combo2;

	private UltraCalendarCombo Combo1;

	private UltraTextEditor tbProjectScope;

	private System.Windows.Forms.ToolTip toolTip1;

	private GroupBox groupBox3;

	private Label label1;

	private Label label2;

	private Label label3;

	private Label label4;

	private Label label5;

	private Label label6;

	private Label label7;

	private Label label8;

	private Label label9;

	private UltraLabel ultraLabel35;

	private UltraButton btnEditGPSLocation;

	private UltraCalendarCombo txtExpectFinishDate;

	private UltraTextEditor tbM2;

	private UltraTextEditor tbM3;

	private UltraTextEditor tbM4;

	private UltraTextEditor tbM7;

	private UltraTextEditor tbM6;

	private UltraTextEditor tbM8;

	private UltraTextEditor tbM5;

	private UltraTextEditor tbM1;

	private UltraComboEditor ddlProjectArea;

	private UltraLabel ultraLabel23;

	private UltraComboEditor ddlProjectCity;

	private UltraTextEditor tbProjectAddress;

	private UltraLabel ultraLabel36;

	private UltraTabPageControl ultraTabPageControl2;

	private Panel pnSummary;

	private UltraLabel ultraLabel37;

	private UltraComboEditor ddlBudType;

	private UltraLabel lbBudType;

	private UltraLabel ultraLabel38;

	private UltraLabel ultraLabel42;

	private UltraLabel ultraLabel41;

	private UltraLabel ultraLabel40;

	private UltraLabel ultraLabel39;

	private UltraLabel lbCostWithTax;

	private UltraLabel lbCostWithoutTax;

	private UltraLabel lbProjectAddress;

	private UltraLabel lbMainInstitute;

	private UltraLabel lbProjectName;

	private UltraLabel lbMainKind;

	private UltraTabPageControl ultraTabPageControl3;

	private UltraLabel ultraLabel43;

	private UltraButton btnUpload;

	private UltraButton btnDownload;

	private C1FlexGrid gridDocumentFiles;

	private OpenFileDialog openUploadFileDialog;

	private SaveFileDialog saveDownloadFileDialog;

	private GroupBox groupBox4;

	private Timer doubleClickTimer;

	private UltraLabel lbProjectScopeUnit;

	private UltraComboEditor ddlDurationType;

	private GroupBox gbTendererInfo;

	private Panel panel14;

	private UltraLabel ultraLabel22;

	private UltraButton btnPickInvoiceNo;

	private UltraCombo gridSublet;

	private UltraTextEditor tbVendorName;

	private UltraTextEditor tbVendorInvoiceNo;

	private UltraLabel ultraLabel21;

	private UltraCalendarCombo ddlExpectDuration;

	private UltraButton btnGenerateCatalog;

	private UltraButton btnDownloadDoc;

	private UltraTextEditor tbOwner;

	private UltraLabel lbOwner;

	private UltraTabPageControl ultraTabPageControl4;

	private UltraTextEditor tbProjectDescription;

	private Label lbMainUnitRequired;

	private Label lbCityRequired;

	private Label lbProjectClassificationRequired;

	private GroupBox gbGreenItem;

	private UltraLabel lbGreenEnergyRatio;

	private UltraLabel lbGreenMaterialRatio;

	private UltraTextEditor tbGreenMethodRatio;

	private UltraTextEditor tbGreenEnvRatio;

	private UltraLabel lbGreenMethodRatio;

	private UltraLabel lbGreenEnvRatio;

	private UltraTextEditor tbGreenEnergyRatio;

	private UltraTextEditor tbGreenMaterialRatio;

	private UltraButton btnRenameGreenRatio;

	private UltraCheckEditor chk21;

	private UltraCheckEditor chk20;

	private UltraCheckEditor chkStage6;

	private UltraCheckEditor chkStage5;

	private UltraCheckEditor chkStage4;

	private UltraCheckEditor chkStage3;

	private UltraCheckEditor chkStage2;

	private UltraCheckEditor chkStage1;

	private UltraTextEditor tbGreenTotalRatio;

	private UltraLabel lbGreenTotalRatio;

	private UltraCheckEditor chk22;

	private UltraTabPageControl ultraTabPageControl5;

	private Label label10;

	private C1FlexGrid gridWraDocumentFiles;

	private UltraButton btnSaveFileList;

	private UltraButton btnDownloadFile;

	private UltraButton btnUploadFile;

	private UltraButton btnFrontCover;

	private UltraButton btnSpecificationAddFrontCover;

	private UltraButton btnSpecificationFrontCover;

	private UltraButton btnDownloadFileList;

	private UltraButton btnCompressionAndDownload;

	private UltraButton btnChangeNo;

	private Timer WRAdoubleClickTimer;

	public Panel panel2;

	private UltraTextEditor txtWeightedCorrectRate;

	private UltraLabel ultraLabel45;

	private UltraTextEditor txtCorrectRate;

	private UltraLabel ultraLabel44;

	private UltraTextEditor txtWeightedConfirmRate;

	private UltraLabel ultraLabel46;

	private UltraTextEditor txtConfirmRate;

	private UltraLabel ultraLabel47;

	private string originalProjectScope = string.Empty;

	private bool projectScopeChanged = false;

	private string UserID;

	private int alertCount = 0;

	private string MainInstituteCode;

	private string MainInstituteName;

	private int tabShowedWhenLoad = 1;

	private FormStatus F_FormStatus = FormStatus.Iinitial;

	private BudgetInfoForm_OpenMode OpenMode;

	private PccesFormAction FormActionName;

	private string ProjectCode;

	private string ChineseProjectName;

	private string EnglishProjectName;

	private string ProjectAddress;

	private string ProjectCity = string.Empty;

	private string documentsFilePath;

	public bool jumpToSysmaintain;

	private bool isFirstClick = true;

	private bool isDoubleClick = false;

	private int milliseconds = 0;

	private Archnowledge.Pcces.DomainModule.LogicalBase.Project project = null;

	private Archnowledge.Pcces.DomainModule.LogicalBase.CostKind costKind = null;

	private Annexe annexe = null;

	private SubMemo subMemo = null;

	private Archnowledge.Pcces.DomainModule.General.Sublet sublet = null;

	private Archnowledge.Pcces.DomainModule.General.PubProject pubProject = null;

	private DataSet dsProject = null;

	private DataSet dsCostKind = null;

	private DataSet dsAnnexe = null;

	private DataSet dsSubMemo = null;

	private DataSet dsPubProject = null;

	private static List<string> NorternCity = new List<string>(new string[8] { "臺北市", "新北市", "基隆市", "宜蘭縣", "桃園市", "桃園縣", "新竹市", "新竹縣" });

	private static List<string> CentralCity = new List<string>(new string[6] { "臺中市", "臺中縣", "苗栗縣", "南投縣", "彰化縣", "雲林縣" });

	private static List<string> SouthernCity = new List<string>(new string[8] { "高雄市", "高雄縣", "嘉義縣", "嘉義市", "臺南縣", "臺南市", "屏東縣", "屏東市" });

	private static List<string> EasternCity = new List<string>(new string[2] { "花蓮縣", "臺東縣" });

	private static List<string> OffshoreCity = new List<string>(new string[3] { "金門縣", "連江縣", "澎湖縣" });

	private static List<string>[] CityAndCounty = new List<string>[5] { NorternCity, CentralCity, SouthernCity, EasternCity, OffshoreCity };

	public bool _ChangeProjectScope => projectScopeChanged;

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

	public string _MainCode
	{
		get
		{
			return MainInstituteCode;
		}
		set
		{
			MainInstituteCode = value;
		}
	}

	public string _MainName
	{
		get
		{
			return MainInstituteName;
		}
		set
		{
			MainInstituteName = value;
		}
	}

	public int _iShowUp_FirstIndex
	{
		get
		{
			return tabShowedWhenLoad;
		}
		set
		{
			tabShowedWhenLoad = value;
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

	public BudgetInfoForm_OpenMode _OpenMode
	{
		get
		{
			return OpenMode;
		}
		set
		{
			OpenMode = value;
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

	public string _ProjectNameC
	{
		get
		{
			return ChineseProjectName;
		}
		set
		{
			ChineseProjectName = value;
		}
	}

	public string _ProjectNameE
	{
		get
		{
			return EnglishProjectName;
		}
		set
		{
			EnglishProjectName = value;
		}
	}

	public string _ProjectAddress
	{
		get
		{
			return ProjectAddress;
		}
		set
		{
			ProjectAddress = value;
		}
	}

	public string _City
	{
		get
		{
			return ProjectCity;
		}
		set
		{
			ProjectCity = value;
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetProjectInfo));
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
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
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton1 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem6 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem7 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem8 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem9 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem10 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem11 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem12 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton2 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton3 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
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
		Infragistics.Win.Appearance appearance64 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance65 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance66 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance67 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance68 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance69 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance70 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance71 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance72 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance73 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance74 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance75 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance76 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance77 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance78 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance79 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance80 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance81 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance82 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance83 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance84 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton4 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.Appearance appearance85 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance86 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton5 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.Appearance appearance87 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance88 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton6 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.Appearance appearance89 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance90 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton7 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.Appearance appearance91 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance92 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton8 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.Appearance appearance93 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance94 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance95 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance96 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance97 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance98 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance99 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance100 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance101 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance102 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance103 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance104 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance105 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem13 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem14 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem15 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem16 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem17 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem18 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem19 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem20 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem21 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem22 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem23 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem24 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem25 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem26 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem27 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem28 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem29 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem30 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem31 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem32 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem33 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem34 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem35 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem36 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem37 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem38 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem39 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem40 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance106 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance107 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance108 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance109 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance110 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance111 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance112 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance113 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance114 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance115 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance116 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance117 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance118 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance119 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance120 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance121 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance122 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance123 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance124 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance125 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance126 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance127 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance128 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance129 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance130 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance131 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance132 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance133 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance134 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance135 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance136 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance137 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance138 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance139 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance140 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance141 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance142 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance143 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance144 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance145 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance146 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance147 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab7 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab8 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab9 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab10 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.SubTab1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel13 = new System.Windows.Forms.Panel();
		this.gridCostKind = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.panel8 = new System.Windows.Forms.Panel();
		this.SubTab2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.PNL_LOWER_1 = new System.Windows.Forms.Panel();
		this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
		this.lblMemo1_1 = new Infragistics.Win.Misc.UltraLabel();
		this.txtMemo1_1 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtMemo1_2 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtMemo1_3 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtMemo1_4 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtMemo1_5 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtMemo1_6 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtMemo1_7 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtMemo1_8 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtMemo1_9 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtMemo1_10 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lblMemo1_2 = new Infragistics.Win.Misc.UltraLabel();
		this.lblMemo1_3 = new Infragistics.Win.Misc.UltraLabel();
		this.lblMemo1_4 = new Infragistics.Win.Misc.UltraLabel();
		this.lblMemo1_5 = new Infragistics.Win.Misc.UltraLabel();
		this.lblMemo1_6 = new Infragistics.Win.Misc.UltraLabel();
		this.lblMemo1_7 = new Infragistics.Win.Misc.UltraLabel();
		this.lblMemo1_8 = new Infragistics.Win.Misc.UltraLabel();
		this.lblMemo1_9 = new Infragistics.Win.Misc.UltraLabel();
		this.lblMemo1_10 = new Infragistics.Win.Misc.UltraLabel();
		this.panel9 = new System.Windows.Forms.Panel();
		this.SubTab3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.PNL_LOWER_2 = new System.Windows.Forms.Panel();
		this.c1Sizer2 = new C1.Win.C1Sizer.C1Sizer();
		this.lblMemo2_1 = new Infragistics.Win.Misc.UltraLabel();
		this.txtMemo2_1 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtMemo2_2 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtMemo2_3 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtMemo2_4 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtMemo2_5 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lblMemo2_2 = new Infragistics.Win.Misc.UltraLabel();
		this.lblMemo2_3 = new Infragistics.Win.Misc.UltraLabel();
		this.lblMemo2_4 = new Infragistics.Win.Misc.UltraLabel();
		this.lblMemo2_5 = new Infragistics.Win.Misc.UltraLabel();
		this.panel10 = new System.Windows.Forms.Panel();
		this.SubTab4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.PNL_LOWER_3 = new System.Windows.Forms.Panel();
		this.c1Sizer3 = new C1.Win.C1Sizer.C1Sizer();
		this.lblMemo3_1 = new Infragistics.Win.Misc.UltraLabel();
		this.txtMemo3_1 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtMemo3_2 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtMemo3_3 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtMemo3_4 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lblMemo3_2 = new Infragistics.Win.Misc.UltraLabel();
		this.lblMemo3_3 = new Infragistics.Win.Misc.UltraLabel();
		this.lblMemo3_4 = new Infragistics.Win.Misc.UltraLabel();
		this.panel11 = new System.Windows.Forms.Panel();
		this.ultraTabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.tbProjectDescription = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.Tab_Basic = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel7 = new System.Windows.Forms.Panel();
		this.ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage2 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.panel6 = new System.Windows.Forms.Panel();
		this.txtWeightedConfirmRate = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel46 = new Infragistics.Win.Misc.UltraLabel();
		this.txtConfirmRate = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel47 = new Infragistics.Win.Misc.UltraLabel();
		this.txtWeightedCorrectRate = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel45 = new Infragistics.Win.Misc.UltraLabel();
		this.txtCorrectRate = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel44 = new Infragistics.Win.Misc.UltraLabel();
		this.lbCityRequired = new System.Windows.Forms.Label();
		this.lbMainUnitRequired = new System.Windows.Forms.Label();
		this.ddlExpectDuration = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ddlDurationType = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.lbProjectScopeUnit = new Infragistics.Win.Misc.UltraLabel();
		this.ddlBudType = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.lbBudType = new Infragistics.Win.Misc.UltraLabel();
		this.tbProjectAddress = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel36 = new Infragistics.Win.Misc.UltraLabel();
		this.ddlProjectCity = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ddlProjectArea = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ultraLabel23 = new Infragistics.Win.Misc.UltraLabel();
		this.btnEditGPSLocation = new Infragistics.Win.Misc.UltraButton();
		this.txtExpectFinishDate = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ultraLabel35 = new Infragistics.Win.Misc.UltraLabel();
		this.tbProjectScope = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lblMainProjectCode = new Infragistics.Win.Misc.UltraLabel();
		this.txtExpectStartDate = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.btnPickMainInstitute = new Infragistics.Win.Misc.UltraButton();
		this.tbMainInstituite = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel18 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel17 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel16 = new Infragistics.Win.Misc.UltraLabel();
		this.tbBudEndYear = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tbWorkUnit = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel15 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.tbBudStartYear = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.tbWorkMode = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.tbAccountCodeUpper = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tbExpectDuration = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tbAccountCodeLower = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tbBuyMode = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tbEnglishProjectName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tbChineseProjectName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tbProjectCode = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tbMainInstituteCode = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.Tab_Other = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.gbGreenItem = new System.Windows.Forms.GroupBox();
		this.tbGreenTotalRatio = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lbGreenTotalRatio = new Infragistics.Win.Misc.UltraLabel();
		this.btnRenameGreenRatio = new Infragistics.Win.Misc.UltraButton();
		this.tbGreenEnergyRatio = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tbGreenMaterialRatio = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lbGreenEnergyRatio = new Infragistics.Win.Misc.UltraLabel();
		this.lbGreenMaterialRatio = new Infragistics.Win.Misc.UltraLabel();
		this.tbGreenMethodRatio = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tbGreenEnvRatio = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lbGreenMethodRatio = new Infragistics.Win.Misc.UltraLabel();
		this.lbGreenEnvRatio = new Infragistics.Win.Misc.UltraLabel();
		this.gbTendererInfo = new System.Windows.Forms.GroupBox();
		this.panel14 = new System.Windows.Forms.Panel();
		this.tbOwner = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lbOwner = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel22 = new Infragistics.Win.Misc.UltraLabel();
		this.btnPickInvoiceNo = new Infragistics.Win.Misc.UltraButton();
		this.gridSublet = new Infragistics.Win.UltraWinGrid.UltraCombo();
		this.tbVendorName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tbVendorInvoiceNo = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel21 = new Infragistics.Win.Misc.UltraLabel();
		this.gbBudget = new System.Windows.Forms.GroupBox();
		this.Combo5 = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ultraLabel34 = new Infragistics.Win.Misc.UltraLabel();
		this.Combo4 = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ultraLabel33 = new Infragistics.Win.Misc.UltraLabel();
		this.Combo3 = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ultraLabel32 = new Infragistics.Win.Misc.UltraLabel();
		this.Combo2 = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ultraLabel31 = new Infragistics.Win.Misc.UltraLabel();
		this.Combo1 = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ultraLabel30 = new Infragistics.Win.Misc.UltraLabel();
		this.txt7 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txt6 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txt5 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txt4 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txt3 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel29 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel28 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel27 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel26 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel25 = new Infragistics.Win.Misc.UltraLabel();
		this.gbConstructionType = new System.Windows.Forms.GroupBox();
		this.chkStage6 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkStage5 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkStage4 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkStage3 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkStage2 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkStage1 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.lbProjectClassificationRequired = new System.Windows.Forms.Label();
		this.panel12 = new System.Windows.Forms.Panel();
		this.chk22 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk21 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk20 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk19 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk18 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk17 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk16 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk15 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk14 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk13 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk12 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk11 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk10 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk09 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk08 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk07 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk06 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk05 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk04 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk03 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk02 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk01 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.ultraLabel20 = new Infragistics.Win.Misc.UltraLabel();
		this.ddlProjectClassification = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ultraLabel19 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel24 = new Infragistics.Win.Misc.UltraLabel();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.lbMainKind = new Infragistics.Win.Misc.UltraLabel();
		this.lbProjectName = new Infragistics.Win.Misc.UltraLabel();
		this.lbCostWithTax = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel37 = new Infragistics.Win.Misc.UltraLabel();
		this.lbCostWithoutTax = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel38 = new Infragistics.Win.Misc.UltraLabel();
		this.lbProjectAddress = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel39 = new Infragistics.Win.Misc.UltraLabel();
		this.lbMainInstitute = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel40 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel41 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel42 = new Infragistics.Win.Misc.UltraLabel();
		this.pnSummary = new System.Windows.Forms.Panel();
		this.ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.btnGenerateCatalog = new Infragistics.Win.Misc.UltraButton();
		this.btnDownloadDoc = new Infragistics.Win.Misc.UltraButton();
		this.gridDocumentFiles = new C1.Win.C1FlexGrid.C1FlexGrid();
		this.btnUpload = new Infragistics.Win.Misc.UltraButton();
		this.btnDownload = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel43 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraTabPageControl5 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.btnChangeNo = new Infragistics.Win.Misc.UltraButton();
		this.btnCompressionAndDownload = new Infragistics.Win.Misc.UltraButton();
		this.btnSpecificationAddFrontCover = new Infragistics.Win.Misc.UltraButton();
		this.btnSpecificationFrontCover = new Infragistics.Win.Misc.UltraButton();
		this.btnFrontCover = new Infragistics.Win.Misc.UltraButton();
		this.btnUploadFile = new Infragistics.Win.Misc.UltraButton();
		this.btnSaveFileList = new Infragistics.Win.Misc.UltraButton();
		this.btnDownloadFile = new Infragistics.Win.Misc.UltraButton();
		this.btnDownloadFileList = new Infragistics.Win.Misc.UltraButton();
		this.gridWraDocumentFiles = new C1.Win.C1FlexGrid.C1FlexGrid();
		this.label10 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.tbM2 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tbM3 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tbM4 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tbM7 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tbM6 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tbM8 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tbM5 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tbM1 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.label9 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.lbChineseProjectName = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.btnCancel = new Infragistics.Win.Misc.UltraButton();
		this.btnOK = new Infragistics.Win.Misc.UltraButton();
		this.panel3 = new System.Windows.Forms.Panel();
		this.Tab_ProjInfo = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.openUploadFileDialog = new System.Windows.Forms.OpenFileDialog();
		this.saveDownloadFileDialog = new System.Windows.Forms.SaveFileDialog();
		this.doubleClickTimer = new System.Windows.Forms.Timer(this.components);
		this.WRAdoubleClickTimer = new System.Windows.Forms.Timer(this.components);
		this.SubTab1.SuspendLayout();
		this.panel13.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridCostKind).BeginInit();
		this.SubTab2.SuspendLayout();
		this.PNL_LOWER_1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1Sizer1).BeginInit();
		this.c1Sizer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtMemo1_1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo1_2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo1_3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo1_4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo1_5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo1_6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo1_7).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo1_8).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo1_9).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo1_10).BeginInit();
		this.SubTab3.SuspendLayout();
		this.PNL_LOWER_2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1Sizer2).BeginInit();
		this.c1Sizer2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtMemo2_1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo2_2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo2_3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo2_4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo2_5).BeginInit();
		this.SubTab4.SuspendLayout();
		this.PNL_LOWER_3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1Sizer3).BeginInit();
		this.c1Sizer3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtMemo3_1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo3_2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo3_3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo3_4).BeginInit();
		this.ultraTabPageControl4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.tbProjectDescription).BeginInit();
		this.Tab_Basic.SuspendLayout();
		this.panel7.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraTabControl1).BeginInit();
		this.ultraTabControl1.SuspendLayout();
		this.panel6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtWeightedConfirmRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtConfirmRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtWeightedCorrectRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCorrectRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ddlExpectDuration).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ddlDurationType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ddlBudType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbProjectAddress).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ddlProjectCity).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ddlProjectArea).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExpectFinishDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbProjectScope).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExpectStartDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbMainInstituite).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbBudEndYear).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbWorkUnit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbBudStartYear).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbWorkMode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbAccountCodeUpper).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbExpectDuration).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbAccountCodeLower).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbBuyMode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbEnglishProjectName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbChineseProjectName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbProjectCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbMainInstituteCode).BeginInit();
		this.Tab_Other.SuspendLayout();
		this.gbGreenItem.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.tbGreenTotalRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbGreenEnergyRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbGreenMaterialRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbGreenMethodRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbGreenEnvRatio).BeginInit();
		this.gbTendererInfo.SuspendLayout();
		this.panel14.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.tbOwner).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridSublet).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbVendorName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbVendorInvoiceNo).BeginInit();
		this.gbBudget.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Combo5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Combo4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Combo3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Combo2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Combo1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txt7).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txt6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txt5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txt4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txt3).BeginInit();
		this.gbConstructionType.SuspendLayout();
		this.panel12.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ddlProjectClassification).BeginInit();
		this.ultraTabPageControl2.SuspendLayout();
		this.groupBox4.SuspendLayout();
		this.ultraTabPageControl3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridDocumentFiles).BeginInit();
		this.ultraTabPageControl5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridWraDocumentFiles).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.tbM2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbM3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbM4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbM7).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbM6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbM8).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbM5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbM1).BeginInit();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_ProjInfo).BeginInit();
		this.Tab_ProjInfo.SuspendLayout();
		base.SuspendLayout();
		this.SubTab1.Controls.Add(this.panel13);
		this.SubTab1.Controls.Add(this.panel8);
		this.SubTab1.Location = new System.Drawing.Point(2, 27);
		this.SubTab1.Name = "SubTab1";
		this.SubTab1.Size = new System.Drawing.Size(668, 163);
		this.panel13.Controls.Add(this.gridCostKind);
		this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel13.Location = new System.Drawing.Point(0, 8);
		this.panel13.Name = "panel13";
		this.panel13.Size = new System.Drawing.Size(668, 155);
		this.panel13.TabIndex = 4;
		this.gridCostKind._ExcelFileName = "";
		this.gridCostKind._ExcelSheeName = "";
		this.gridCostKind._IsOpenExcelAfterExport = false;
		this.gridCostKind.AllowEditing = false;
		this.gridCostKind.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridCostKind.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.gridCostKind.ColumnInfo = resources.GetString("gridCostKind.ColumnInfo");
		this.gridCostKind.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridCostKind.ExtendLastCol = true;
		this.gridCostKind.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridCostKind.ForeColor = System.Drawing.Color.Black;
		this.gridCostKind.Location = new System.Drawing.Point(0, 0);
		this.gridCostKind.Name = "gridCostKind";
		this.gridCostKind.Rows.Count = 1;
		this.gridCostKind.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.gridCostKind.ShowCursor = true;
		this.gridCostKind.ShowSort = false;
		this.gridCostKind.ShowToolTipOnNarrowColumn = true;
		this.gridCostKind.Size = new System.Drawing.Size(668, 155);
		this.gridCostKind.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridCostKind.Styles"));
		this.gridCostKind.TabIndex = 1;
		this.gridCostKind.Tree.Column = 1;
		this.gridCostKind.Tree.LineColor = System.Drawing.Color.Gray;
		this.panel8.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel8.Location = new System.Drawing.Point(0, 0);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(668, 8);
		this.panel8.TabIndex = 3;
		this.SubTab2.Controls.Add(this.PNL_LOWER_1);
		this.SubTab2.Controls.Add(this.panel9);
		this.SubTab2.Location = new System.Drawing.Point(-10000, -10000);
		this.SubTab2.Name = "SubTab2";
		this.SubTab2.Size = new System.Drawing.Size(668, 163);
		this.PNL_LOWER_1.AutoScroll = true;
		this.PNL_LOWER_1.Controls.Add(this.c1Sizer1);
		this.PNL_LOWER_1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.PNL_LOWER_1.Location = new System.Drawing.Point(0, 8);
		this.PNL_LOWER_1.Name = "PNL_LOWER_1";
		this.PNL_LOWER_1.Size = new System.Drawing.Size(668, 155);
		this.PNL_LOWER_1.TabIndex = 4;
		this.c1Sizer1.AllowDrop = true;
		this.c1Sizer1.Controls.Add(this.lblMemo1_1);
		this.c1Sizer1.Controls.Add(this.txtMemo1_1);
		this.c1Sizer1.Controls.Add(this.txtMemo1_2);
		this.c1Sizer1.Controls.Add(this.txtMemo1_3);
		this.c1Sizer1.Controls.Add(this.txtMemo1_4);
		this.c1Sizer1.Controls.Add(this.txtMemo1_5);
		this.c1Sizer1.Controls.Add(this.txtMemo1_6);
		this.c1Sizer1.Controls.Add(this.txtMemo1_7);
		this.c1Sizer1.Controls.Add(this.txtMemo1_8);
		this.c1Sizer1.Controls.Add(this.txtMemo1_9);
		this.c1Sizer1.Controls.Add(this.txtMemo1_10);
		this.c1Sizer1.Controls.Add(this.lblMemo1_2);
		this.c1Sizer1.Controls.Add(this.lblMemo1_3);
		this.c1Sizer1.Controls.Add(this.lblMemo1_4);
		this.c1Sizer1.Controls.Add(this.lblMemo1_5);
		this.c1Sizer1.Controls.Add(this.lblMemo1_6);
		this.c1Sizer1.Controls.Add(this.lblMemo1_7);
		this.c1Sizer1.Controls.Add(this.lblMemo1_8);
		this.c1Sizer1.Controls.Add(this.lblMemo1_9);
		this.c1Sizer1.Controls.Add(this.lblMemo1_10);
		this.c1Sizer1.GridDefinition = resources.GetString("c1Sizer1.GridDefinition");
		this.c1Sizer1.Location = new System.Drawing.Point(0, 4);
		this.c1Sizer1.Name = "c1Sizer1";
		this.c1Sizer1.Size = new System.Drawing.Size(648, 280);
		this.c1Sizer1.TabIndex = 0;
		this.c1Sizer1.TabStop = false;
		appearance1.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblMemo1_1.Appearance = appearance1;
		this.lblMemo1_1.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblMemo1_1.Location = new System.Drawing.Point(4, 4);
		this.lblMemo1_1.Name = "lblMemo1_1";
		this.lblMemo1_1.Size = new System.Drawing.Size(20, 23);
		this.lblMemo1_1.TabIndex = 1;
		this.lblMemo1_1.Text = "01";
		this.txtMemo1_1.AutoSize = true;
		this.txtMemo1_1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtMemo1_1.Location = new System.Drawing.Point(28, 4);
		this.txtMemo1_1.MaxLength = 100;
		this.txtMemo1_1.Name = "txtMemo1_1";
		this.txtMemo1_1.Size = new System.Drawing.Size(616, 24);
		this.txtMemo1_1.TabIndex = 0;
		this.txtMemo1_2.AutoSize = true;
		this.txtMemo1_2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtMemo1_2.Location = new System.Drawing.Point(28, 31);
		this.txtMemo1_2.MaxLength = 100;
		this.txtMemo1_2.Name = "txtMemo1_2";
		this.txtMemo1_2.Size = new System.Drawing.Size(616, 24);
		this.txtMemo1_2.TabIndex = 0;
		this.txtMemo1_3.AutoSize = true;
		this.txtMemo1_3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtMemo1_3.Location = new System.Drawing.Point(28, 59);
		this.txtMemo1_3.MaxLength = 100;
		this.txtMemo1_3.Name = "txtMemo1_3";
		this.txtMemo1_3.Size = new System.Drawing.Size(616, 24);
		this.txtMemo1_3.TabIndex = 0;
		this.txtMemo1_4.AutoSize = true;
		this.txtMemo1_4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtMemo1_4.Location = new System.Drawing.Point(28, 87);
		this.txtMemo1_4.MaxLength = 100;
		this.txtMemo1_4.Name = "txtMemo1_4";
		this.txtMemo1_4.Size = new System.Drawing.Size(616, 24);
		this.txtMemo1_4.TabIndex = 0;
		this.txtMemo1_5.AutoSize = true;
		this.txtMemo1_5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtMemo1_5.Location = new System.Drawing.Point(28, 115);
		this.txtMemo1_5.MaxLength = 100;
		this.txtMemo1_5.Name = "txtMemo1_5";
		this.txtMemo1_5.Size = new System.Drawing.Size(616, 24);
		this.txtMemo1_5.TabIndex = 0;
		this.txtMemo1_6.AutoSize = true;
		this.txtMemo1_6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtMemo1_6.Location = new System.Drawing.Point(28, 143);
		this.txtMemo1_6.MaxLength = 100;
		this.txtMemo1_6.Name = "txtMemo1_6";
		this.txtMemo1_6.Size = new System.Drawing.Size(616, 24);
		this.txtMemo1_6.TabIndex = 0;
		this.txtMemo1_7.AutoSize = true;
		this.txtMemo1_7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtMemo1_7.Location = new System.Drawing.Point(28, 171);
		this.txtMemo1_7.MaxLength = 100;
		this.txtMemo1_7.Name = "txtMemo1_7";
		this.txtMemo1_7.Size = new System.Drawing.Size(616, 24);
		this.txtMemo1_7.TabIndex = 0;
		this.txtMemo1_8.AutoSize = true;
		this.txtMemo1_8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtMemo1_8.Location = new System.Drawing.Point(28, 198);
		this.txtMemo1_8.MaxLength = 100;
		this.txtMemo1_8.Name = "txtMemo1_8";
		this.txtMemo1_8.Size = new System.Drawing.Size(616, 24);
		this.txtMemo1_8.TabIndex = 0;
		this.txtMemo1_9.AutoSize = true;
		this.txtMemo1_9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtMemo1_9.Location = new System.Drawing.Point(28, 226);
		this.txtMemo1_9.MaxLength = 100;
		this.txtMemo1_9.Name = "txtMemo1_9";
		this.txtMemo1_9.Size = new System.Drawing.Size(616, 24);
		this.txtMemo1_9.TabIndex = 0;
		this.txtMemo1_10.AutoSize = true;
		this.txtMemo1_10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtMemo1_10.Location = new System.Drawing.Point(28, 253);
		this.txtMemo1_10.MaxLength = 100;
		this.txtMemo1_10.Name = "txtMemo1_10";
		this.txtMemo1_10.Size = new System.Drawing.Size(616, 24);
		this.txtMemo1_10.TabIndex = 0;
		appearance2.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblMemo1_2.Appearance = appearance2;
		this.lblMemo1_2.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblMemo1_2.Location = new System.Drawing.Point(4, 31);
		this.lblMemo1_2.Name = "lblMemo1_2";
		this.lblMemo1_2.Size = new System.Drawing.Size(20, 24);
		this.lblMemo1_2.TabIndex = 1;
		this.lblMemo1_2.Text = "02";
		appearance3.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblMemo1_3.Appearance = appearance3;
		this.lblMemo1_3.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblMemo1_3.Location = new System.Drawing.Point(4, 59);
		this.lblMemo1_3.Name = "lblMemo1_3";
		this.lblMemo1_3.Size = new System.Drawing.Size(20, 24);
		this.lblMemo1_3.TabIndex = 1;
		this.lblMemo1_3.Text = "03";
		appearance4.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblMemo1_4.Appearance = appearance4;
		this.lblMemo1_4.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblMemo1_4.Location = new System.Drawing.Point(4, 87);
		this.lblMemo1_4.Name = "lblMemo1_4";
		this.lblMemo1_4.Size = new System.Drawing.Size(20, 24);
		this.lblMemo1_4.TabIndex = 1;
		this.lblMemo1_4.Text = "04";
		appearance5.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance5.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblMemo1_5.Appearance = appearance5;
		this.lblMemo1_5.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblMemo1_5.Location = new System.Drawing.Point(4, 115);
		this.lblMemo1_5.Name = "lblMemo1_5";
		this.lblMemo1_5.Size = new System.Drawing.Size(20, 24);
		this.lblMemo1_5.TabIndex = 1;
		this.lblMemo1_5.Text = "05";
		appearance6.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance6.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblMemo1_6.Appearance = appearance6;
		this.lblMemo1_6.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblMemo1_6.Location = new System.Drawing.Point(4, 143);
		this.lblMemo1_6.Name = "lblMemo1_6";
		this.lblMemo1_6.Size = new System.Drawing.Size(20, 24);
		this.lblMemo1_6.TabIndex = 1;
		this.lblMemo1_6.Text = "06";
		appearance7.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblMemo1_7.Appearance = appearance7;
		this.lblMemo1_7.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblMemo1_7.Location = new System.Drawing.Point(4, 171);
		this.lblMemo1_7.Name = "lblMemo1_7";
		this.lblMemo1_7.Size = new System.Drawing.Size(20, 23);
		this.lblMemo1_7.TabIndex = 1;
		this.lblMemo1_7.Text = "07";
		appearance8.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance8.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblMemo1_8.Appearance = appearance8;
		this.lblMemo1_8.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblMemo1_8.Location = new System.Drawing.Point(4, 198);
		this.lblMemo1_8.Name = "lblMemo1_8";
		this.lblMemo1_8.Size = new System.Drawing.Size(20, 24);
		this.lblMemo1_8.TabIndex = 1;
		this.lblMemo1_8.Text = "08";
		appearance9.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance9.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblMemo1_9.Appearance = appearance9;
		this.lblMemo1_9.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblMemo1_9.Location = new System.Drawing.Point(4, 226);
		this.lblMemo1_9.Name = "lblMemo1_9";
		this.lblMemo1_9.Size = new System.Drawing.Size(20, 23);
		this.lblMemo1_9.TabIndex = 1;
		this.lblMemo1_9.Text = "09";
		appearance10.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance10.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblMemo1_10.Appearance = appearance10;
		this.lblMemo1_10.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblMemo1_10.Location = new System.Drawing.Point(4, 253);
		this.lblMemo1_10.Name = "lblMemo1_10";
		this.lblMemo1_10.Size = new System.Drawing.Size(20, 23);
		this.lblMemo1_10.TabIndex = 1;
		this.lblMemo1_10.Text = "10";
		this.panel9.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel9.Location = new System.Drawing.Point(0, 0);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(668, 8);
		this.panel9.TabIndex = 3;
		this.SubTab3.Controls.Add(this.PNL_LOWER_2);
		this.SubTab3.Controls.Add(this.panel10);
		this.SubTab3.Location = new System.Drawing.Point(-10000, -10000);
		this.SubTab3.Name = "SubTab3";
		this.SubTab3.Size = new System.Drawing.Size(668, 163);
		this.PNL_LOWER_2.AutoScroll = true;
		this.PNL_LOWER_2.Controls.Add(this.c1Sizer2);
		this.PNL_LOWER_2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.PNL_LOWER_2.Location = new System.Drawing.Point(0, 8);
		this.PNL_LOWER_2.Name = "PNL_LOWER_2";
		this.PNL_LOWER_2.Size = new System.Drawing.Size(668, 155);
		this.PNL_LOWER_2.TabIndex = 5;
		this.c1Sizer2.AllowDrop = true;
		this.c1Sizer2.Controls.Add(this.lblMemo2_1);
		this.c1Sizer2.Controls.Add(this.txtMemo2_1);
		this.c1Sizer2.Controls.Add(this.txtMemo2_2);
		this.c1Sizer2.Controls.Add(this.txtMemo2_3);
		this.c1Sizer2.Controls.Add(this.txtMemo2_4);
		this.c1Sizer2.Controls.Add(this.txtMemo2_5);
		this.c1Sizer2.Controls.Add(this.lblMemo2_2);
		this.c1Sizer2.Controls.Add(this.lblMemo2_3);
		this.c1Sizer2.Controls.Add(this.lblMemo2_4);
		this.c1Sizer2.Controls.Add(this.lblMemo2_5);
		this.c1Sizer2.GridDefinition = resources.GetString("c1Sizer2.GridDefinition");
		this.c1Sizer2.Location = new System.Drawing.Point(0, 4);
		this.c1Sizer2.Name = "c1Sizer2";
		this.c1Sizer2.Size = new System.Drawing.Size(645, 140);
		this.c1Sizer2.TabIndex = 0;
		this.c1Sizer2.TabStop = false;
		appearance11.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance11.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblMemo2_1.Appearance = appearance11;
		this.lblMemo2_1.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblMemo2_1.Location = new System.Drawing.Point(4, 4);
		this.lblMemo2_1.Name = "lblMemo2_1";
		this.lblMemo2_1.Size = new System.Drawing.Size(20, 24);
		this.lblMemo2_1.TabIndex = 2;
		this.lblMemo2_1.Text = "01";
		this.txtMemo2_1.AutoSize = true;
		this.txtMemo2_1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtMemo2_1.Location = new System.Drawing.Point(28, 4);
		this.txtMemo2_1.MaxLength = 100;
		this.txtMemo2_1.Name = "txtMemo2_1";
		this.txtMemo2_1.Size = new System.Drawing.Size(613, 24);
		this.txtMemo2_1.TabIndex = 0;
		this.txtMemo2_2.AutoSize = true;
		this.txtMemo2_2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtMemo2_2.Location = new System.Drawing.Point(28, 32);
		this.txtMemo2_2.MaxLength = 100;
		this.txtMemo2_2.Name = "txtMemo2_2";
		this.txtMemo2_2.Size = new System.Drawing.Size(613, 24);
		this.txtMemo2_2.TabIndex = 0;
		this.txtMemo2_3.AutoSize = true;
		this.txtMemo2_3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtMemo2_3.Location = new System.Drawing.Point(28, 59);
		this.txtMemo2_3.MaxLength = 100;
		this.txtMemo2_3.Name = "txtMemo2_3";
		this.txtMemo2_3.Size = new System.Drawing.Size(613, 24);
		this.txtMemo2_3.TabIndex = 0;
		this.txtMemo2_4.AutoSize = true;
		this.txtMemo2_4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtMemo2_4.Location = new System.Drawing.Point(28, 86);
		this.txtMemo2_4.MaxLength = 100;
		this.txtMemo2_4.Name = "txtMemo2_4";
		this.txtMemo2_4.Size = new System.Drawing.Size(613, 24);
		this.txtMemo2_4.TabIndex = 0;
		this.txtMemo2_5.AutoSize = true;
		this.txtMemo2_5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtMemo2_5.Location = new System.Drawing.Point(28, 113);
		this.txtMemo2_5.MaxLength = 100;
		this.txtMemo2_5.Name = "txtMemo2_5";
		this.txtMemo2_5.Size = new System.Drawing.Size(613, 24);
		this.txtMemo2_5.TabIndex = 0;
		appearance12.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance12.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblMemo2_2.Appearance = appearance12;
		this.lblMemo2_2.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblMemo2_2.Location = new System.Drawing.Point(4, 32);
		this.lblMemo2_2.Name = "lblMemo2_2";
		this.lblMemo2_2.Size = new System.Drawing.Size(20, 23);
		this.lblMemo2_2.TabIndex = 2;
		this.lblMemo2_2.Text = "02";
		appearance13.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance13.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblMemo2_3.Appearance = appearance13;
		this.lblMemo2_3.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblMemo2_3.Location = new System.Drawing.Point(4, 59);
		this.lblMemo2_3.Name = "lblMemo2_3";
		this.lblMemo2_3.Size = new System.Drawing.Size(20, 23);
		this.lblMemo2_3.TabIndex = 2;
		this.lblMemo2_3.Text = "03";
		appearance14.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance14.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblMemo2_4.Appearance = appearance14;
		this.lblMemo2_4.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblMemo2_4.Location = new System.Drawing.Point(4, 86);
		this.lblMemo2_4.Name = "lblMemo2_4";
		this.lblMemo2_4.Size = new System.Drawing.Size(20, 23);
		this.lblMemo2_4.TabIndex = 2;
		this.lblMemo2_4.Text = "04";
		appearance15.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance15.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblMemo2_5.Appearance = appearance15;
		this.lblMemo2_5.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblMemo2_5.Location = new System.Drawing.Point(4, 113);
		this.lblMemo2_5.Name = "lblMemo2_5";
		this.lblMemo2_5.Size = new System.Drawing.Size(20, 23);
		this.lblMemo2_5.TabIndex = 2;
		this.lblMemo2_5.Text = "05";
		this.panel10.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel10.Location = new System.Drawing.Point(0, 0);
		this.panel10.Name = "panel10";
		this.panel10.Size = new System.Drawing.Size(668, 8);
		this.panel10.TabIndex = 3;
		this.SubTab4.Controls.Add(this.PNL_LOWER_3);
		this.SubTab4.Controls.Add(this.panel11);
		this.SubTab4.Location = new System.Drawing.Point(-10000, -10000);
		this.SubTab4.Name = "SubTab4";
		this.SubTab4.Size = new System.Drawing.Size(668, 163);
		this.PNL_LOWER_3.Controls.Add(this.c1Sizer3);
		this.PNL_LOWER_3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.PNL_LOWER_3.Location = new System.Drawing.Point(0, 8);
		this.PNL_LOWER_3.Name = "PNL_LOWER_3";
		this.PNL_LOWER_3.Size = new System.Drawing.Size(668, 155);
		this.PNL_LOWER_3.TabIndex = 5;
		this.c1Sizer3.AllowDrop = true;
		this.c1Sizer3.Controls.Add(this.lblMemo3_1);
		this.c1Sizer3.Controls.Add(this.txtMemo3_1);
		this.c1Sizer3.Controls.Add(this.txtMemo3_2);
		this.c1Sizer3.Controls.Add(this.txtMemo3_3);
		this.c1Sizer3.Controls.Add(this.txtMemo3_4);
		this.c1Sizer3.Controls.Add(this.lblMemo3_2);
		this.c1Sizer3.Controls.Add(this.lblMemo3_3);
		this.c1Sizer3.Controls.Add(this.lblMemo3_4);
		this.c1Sizer3.GridDefinition = "20.5357142857143:False:False;20.5357142857143:False:False;20.5357142857143:False:False;20.5357142857143:False:False;\t3.01659125188537:False:True;95.1734539969834:False:False;";
		this.c1Sizer3.Location = new System.Drawing.Point(1, 3);
		this.c1Sizer3.Name = "c1Sizer3";
		this.c1Sizer3.Size = new System.Drawing.Size(663, 112);
		this.c1Sizer3.TabIndex = 0;
		this.c1Sizer3.TabStop = false;
		appearance16.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance16.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblMemo3_1.Appearance = appearance16;
		this.lblMemo3_1.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblMemo3_1.Location = new System.Drawing.Point(4, 4);
		this.lblMemo3_1.Name = "lblMemo3_1";
		this.lblMemo3_1.Size = new System.Drawing.Size(20, 23);
		this.lblMemo3_1.TabIndex = 3;
		this.lblMemo3_1.Text = "01";
		this.txtMemo3_1.AutoSize = true;
		this.txtMemo3_1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtMemo3_1.Location = new System.Drawing.Point(28, 4);
		this.txtMemo3_1.MaxLength = 100;
		this.txtMemo3_1.Name = "txtMemo3_1";
		this.txtMemo3_1.Size = new System.Drawing.Size(631, 24);
		this.txtMemo3_1.TabIndex = 1;
		this.txtMemo3_2.AutoSize = true;
		this.txtMemo3_2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtMemo3_2.Location = new System.Drawing.Point(28, 31);
		this.txtMemo3_2.MaxLength = 100;
		this.txtMemo3_2.Name = "txtMemo3_2";
		this.txtMemo3_2.Size = new System.Drawing.Size(631, 24);
		this.txtMemo3_2.TabIndex = 1;
		this.txtMemo3_3.AutoSize = true;
		this.txtMemo3_3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtMemo3_3.Location = new System.Drawing.Point(28, 58);
		this.txtMemo3_3.MaxLength = 100;
		this.txtMemo3_3.Name = "txtMemo3_3";
		this.txtMemo3_3.Size = new System.Drawing.Size(631, 24);
		this.txtMemo3_3.TabIndex = 1;
		this.txtMemo3_4.AutoSize = true;
		this.txtMemo3_4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtMemo3_4.Location = new System.Drawing.Point(28, 85);
		this.txtMemo3_4.MaxLength = 100;
		this.txtMemo3_4.Name = "txtMemo3_4";
		this.txtMemo3_4.Size = new System.Drawing.Size(631, 24);
		this.txtMemo3_4.TabIndex = 1;
		appearance17.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance17.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblMemo3_2.Appearance = appearance17;
		this.lblMemo3_2.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblMemo3_2.Location = new System.Drawing.Point(4, 31);
		this.lblMemo3_2.Name = "lblMemo3_2";
		this.lblMemo3_2.Size = new System.Drawing.Size(20, 23);
		this.lblMemo3_2.TabIndex = 3;
		this.lblMemo3_2.Text = "02";
		appearance18.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance18.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblMemo3_3.Appearance = appearance18;
		this.lblMemo3_3.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblMemo3_3.Location = new System.Drawing.Point(4, 58);
		this.lblMemo3_3.Name = "lblMemo3_3";
		this.lblMemo3_3.Size = new System.Drawing.Size(20, 23);
		this.lblMemo3_3.TabIndex = 3;
		this.lblMemo3_3.Text = "03";
		appearance19.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance19.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblMemo3_4.Appearance = appearance19;
		this.lblMemo3_4.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblMemo3_4.Location = new System.Drawing.Point(4, 85);
		this.lblMemo3_4.Name = "lblMemo3_4";
		this.lblMemo3_4.Size = new System.Drawing.Size(20, 23);
		this.lblMemo3_4.TabIndex = 3;
		this.lblMemo3_4.Text = "04";
		this.panel11.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel11.Location = new System.Drawing.Point(0, 0);
		this.panel11.Name = "panel11";
		this.panel11.Size = new System.Drawing.Size(668, 8);
		this.panel11.TabIndex = 3;
		this.ultraTabPageControl4.Controls.Add(this.tbProjectDescription);
		this.ultraTabPageControl4.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabPageControl4.Name = "ultraTabPageControl4";
		this.ultraTabPageControl4.Size = new System.Drawing.Size(668, 163);
		this.tbProjectDescription.AcceptsReturn = true;
		this.tbProjectDescription.AlwaysInEditMode = true;
		this.tbProjectDescription.AutoSize = true;
		this.tbProjectDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.tbProjectDescription.Location = new System.Drawing.Point(3, 3);
		this.tbProjectDescription.MaxLength = 4000;
		this.tbProjectDescription.Multiline = true;
		this.tbProjectDescription.Name = "tbProjectDescription";
		this.tbProjectDescription.Scrollbars = System.Windows.Forms.ScrollBars.Vertical;
		this.tbProjectDescription.Size = new System.Drawing.Size(662, 157);
		this.tbProjectDescription.TabIndex = 2;
		this.Tab_Basic.Controls.Add(this.panel7);
		this.Tab_Basic.Controls.Add(this.panel6);
		this.Tab_Basic.Controls.Add(this.panel4);
		this.Tab_Basic.Location = new System.Drawing.Point(2, 29);
		this.Tab_Basic.Name = "Tab_Basic";
		this.Tab_Basic.Size = new System.Drawing.Size(682, 547);
		this.panel7.Controls.Add(this.ultraTabControl1);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel7.Location = new System.Drawing.Point(0, 349);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(682, 198);
		this.panel7.TabIndex = 4;
		appearance20.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance20.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance20.ForeColor = System.Drawing.Color.White;
		this.ultraTabControl1.ActiveTabAppearance = appearance20;
		appearance21.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance21.BackColor2 = System.Drawing.Color.FromArgb(102, 153, 255);
		appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		this.ultraTabControl1.Appearance = appearance21;
		appearance22.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance22.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraTabControl1.ClientAreaAppearance = appearance22;
		this.ultraTabControl1.Controls.Add(this.ultraTabSharedControlsPage2);
		this.ultraTabControl1.Controls.Add(this.SubTab1);
		this.ultraTabControl1.Controls.Add(this.SubTab2);
		this.ultraTabControl1.Controls.Add(this.SubTab3);
		this.ultraTabControl1.Controls.Add(this.SubTab4);
		this.ultraTabControl1.Controls.Add(this.ultraTabPageControl4);
		this.ultraTabControl1.Location = new System.Drawing.Point(4, 4);
		this.ultraTabControl1.Name = "ultraTabControl1";
		this.ultraTabControl1.SharedControlsPage = this.ultraTabSharedControlsPage2;
		this.ultraTabControl1.Size = new System.Drawing.Size(672, 192);
		this.ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
		this.ultraTabControl1.TabIndex = 0;
		this.ultraTabControl1.TabPadding = new System.Drawing.Size(1, 2);
		ultraTab1.TabPage = this.SubTab1;
		ultraTab1.Text = "工程經費預算款項";
		ultraTab2.TabPage = this.SubTab2;
		ultraTab2.Text = "工程概要";
		ultraTab3.TabPage = this.SubTab3;
		ultraTab3.Text = "經費來源";
		ultraTab4.TabPage = this.SubTab4;
		ultraTab4.Text = "附件";
		ultraTab5.TabPage = this.ultraTabPageControl4;
		ultraTab5.Text = "備註說明事項";
		this.ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[5] { ultraTab1, ultraTab2, ultraTab3, ultraTab4, ultraTab5 });
		this.ultraTabSharedControlsPage2.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage2.Name = "ultraTabSharedControlsPage2";
		this.ultraTabSharedControlsPage2.Size = new System.Drawing.Size(668, 163);
		this.panel6.Controls.Add(this.txtWeightedConfirmRate);
		this.panel6.Controls.Add(this.ultraLabel46);
		this.panel6.Controls.Add(this.txtConfirmRate);
		this.panel6.Controls.Add(this.ultraLabel47);
		this.panel6.Controls.Add(this.txtWeightedCorrectRate);
		this.panel6.Controls.Add(this.ultraLabel45);
		this.panel6.Controls.Add(this.txtCorrectRate);
		this.panel6.Controls.Add(this.ultraLabel44);
		this.panel6.Controls.Add(this.lbCityRequired);
		this.panel6.Controls.Add(this.lbMainUnitRequired);
		this.panel6.Controls.Add(this.ddlExpectDuration);
		this.panel6.Controls.Add(this.ddlDurationType);
		this.panel6.Controls.Add(this.lbProjectScopeUnit);
		this.panel6.Controls.Add(this.ddlBudType);
		this.panel6.Controls.Add(this.lbBudType);
		this.panel6.Controls.Add(this.tbProjectAddress);
		this.panel6.Controls.Add(this.ultraLabel36);
		this.panel6.Controls.Add(this.ddlProjectCity);
		this.panel6.Controls.Add(this.ddlProjectArea);
		this.panel6.Controls.Add(this.ultraLabel23);
		this.panel6.Controls.Add(this.btnEditGPSLocation);
		this.panel6.Controls.Add(this.txtExpectFinishDate);
		this.panel6.Controls.Add(this.ultraLabel35);
		this.panel6.Controls.Add(this.tbProjectScope);
		this.panel6.Controls.Add(this.lblMainProjectCode);
		this.panel6.Controls.Add(this.txtExpectStartDate);
		this.panel6.Controls.Add(this.btnPickMainInstitute);
		this.panel6.Controls.Add(this.tbMainInstituite);
		this.panel6.Controls.Add(this.ultraLabel18);
		this.panel6.Controls.Add(this.ultraLabel17);
		this.panel6.Controls.Add(this.ultraLabel16);
		this.panel6.Controls.Add(this.tbBudEndYear);
		this.panel6.Controls.Add(this.tbWorkUnit);
		this.panel6.Controls.Add(this.ultraLabel15);
		this.panel6.Controls.Add(this.ultraLabel14);
		this.panel6.Controls.Add(this.tbBudStartYear);
		this.panel6.Controls.Add(this.ultraLabel13);
		this.panel6.Controls.Add(this.tbWorkMode);
		this.panel6.Controls.Add(this.ultraLabel12);
		this.panel6.Controls.Add(this.tbAccountCodeUpper);
		this.panel6.Controls.Add(this.tbExpectDuration);
		this.panel6.Controls.Add(this.tbAccountCodeLower);
		this.panel6.Controls.Add(this.tbBuyMode);
		this.panel6.Controls.Add(this.tbEnglishProjectName);
		this.panel6.Controls.Add(this.tbChineseProjectName);
		this.panel6.Controls.Add(this.tbProjectCode);
		this.panel6.Controls.Add(this.tbMainInstituteCode);
		this.panel6.Controls.Add(this.ultraLabel11);
		this.panel6.Controls.Add(this.ultraLabel10);
		this.panel6.Controls.Add(this.ultraLabel9);
		this.panel6.Controls.Add(this.ultraLabel8);
		this.panel6.Controls.Add(this.ultraLabel7);
		this.panel6.Controls.Add(this.ultraLabel6);
		this.panel6.Controls.Add(this.ultraLabel5);
		this.panel6.Controls.Add(this.ultraLabel4);
		this.panel6.Controls.Add(this.ultraLabel3);
		this.panel6.Controls.Add(this.ultraLabel2);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel6.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel6.Location = new System.Drawing.Point(0, 8);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(682, 341);
		this.panel6.TabIndex = 3;
		appearance23.BackColor = System.Drawing.Color.White;
		appearance23.BackColor2 = System.Drawing.Color.White;
		appearance23.BackColorDisabled = System.Drawing.Color.White;
		appearance23.BackColorDisabled2 = System.Drawing.Color.White;
		appearance23.ForeColor = System.Drawing.Color.Black;
		appearance23.ForeColorDisabled = System.Drawing.Color.Black;
		appearance23.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.txtWeightedConfirmRate.Appearance = appearance23;
		this.txtWeightedConfirmRate.AutoSize = true;
		this.txtWeightedConfirmRate.Enabled = false;
		this.txtWeightedConfirmRate.Location = new System.Drawing.Point(448, 279);
		this.txtWeightedConfirmRate.MaxLength = 200;
		this.txtWeightedConfirmRate.Name = "txtWeightedConfirmRate";
		this.txtWeightedConfirmRate.Size = new System.Drawing.Size(77, 21);
		this.txtWeightedConfirmRate.TabIndex = 73;
		this.toolTip1.SetToolTip(this.txtWeightedConfirmRate, "施工地點是 Excel 或報表匯出的資料");
		this.txtWeightedConfirmRate.Visible = false;
		appearance24.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel46.Appearance = appearance24;
		this.ultraLabel46.Location = new System.Drawing.Point(370, 284);
		this.ultraLabel46.Name = "ultraLabel46";
		this.ultraLabel46.Size = new System.Drawing.Size(88, 16);
		this.ultraLabel46.TabIndex = 72;
		this.ultraLabel46.Text = "加權符合率:";
		this.ultraLabel46.Visible = false;
		appearance25.BackColor = System.Drawing.Color.White;
		appearance25.BackColor2 = System.Drawing.Color.White;
		appearance25.BackColorDisabled = System.Drawing.Color.White;
		appearance25.BackColorDisabled2 = System.Drawing.Color.White;
		appearance25.ForeColor = System.Drawing.Color.Black;
		appearance25.ForeColorDisabled = System.Drawing.Color.Black;
		appearance25.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.txtConfirmRate.Appearance = appearance25;
		this.txtConfirmRate.AutoSize = true;
		this.txtConfirmRate.Enabled = false;
		this.txtConfirmRate.Location = new System.Drawing.Point(589, 314);
		this.txtConfirmRate.MaxLength = 200;
		this.txtConfirmRate.Name = "txtConfirmRate";
		this.txtConfirmRate.Size = new System.Drawing.Size(77, 21);
		this.txtConfirmRate.TabIndex = 71;
		this.toolTip1.SetToolTip(this.txtConfirmRate, "施工地點是 Excel 或報表匯出的資料");
		appearance26.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel47.Appearance = appearance26;
		this.ultraLabel47.Location = new System.Drawing.Point(485, 318);
		this.ultraLabel47.Name = "ultraLabel47";
		this.ultraLabel47.Size = new System.Drawing.Size(101, 16);
		this.ultraLabel47.TabIndex = 70;
		this.ultraLabel47.Text = "綱要編碼正確率:";
		appearance27.BackColor = System.Drawing.Color.White;
		appearance27.BackColor2 = System.Drawing.Color.White;
		appearance27.BackColorDisabled = System.Drawing.Color.White;
		appearance27.BackColorDisabled2 = System.Drawing.Color.White;
		appearance27.ForeColor = System.Drawing.Color.Black;
		appearance27.ForeColorDisabled = System.Drawing.Color.Black;
		appearance27.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.txtWeightedCorrectRate.Appearance = appearance27;
		this.txtWeightedCorrectRate.AutoSize = true;
		this.txtWeightedCorrectRate.Enabled = false;
		this.txtWeightedCorrectRate.Location = new System.Drawing.Point(293, 313);
		this.txtWeightedCorrectRate.MaxLength = 200;
		this.txtWeightedCorrectRate.Name = "txtWeightedCorrectRate";
		this.txtWeightedCorrectRate.Size = new System.Drawing.Size(77, 21);
		this.txtWeightedCorrectRate.TabIndex = 69;
		this.toolTip1.SetToolTip(this.txtWeightedCorrectRate, "施工地點是 Excel 或報表匯出的資料");
		appearance28.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel45.Appearance = appearance28;
		this.ultraLabel45.Location = new System.Drawing.Point(215, 318);
		this.ultraLabel45.Name = "ultraLabel45";
		this.ultraLabel45.Size = new System.Drawing.Size(88, 16);
		this.ultraLabel45.TabIndex = 68;
		this.ultraLabel45.Text = "加權正確率:";
		appearance29.BackColor = System.Drawing.Color.White;
		appearance29.BackColor2 = System.Drawing.Color.White;
		appearance29.BackColorDisabled = System.Drawing.Color.White;
		appearance29.BackColorDisabled2 = System.Drawing.Color.White;
		appearance29.ForeColor = System.Drawing.Color.Black;
		appearance29.ForeColorDisabled = System.Drawing.Color.Black;
		appearance29.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.txtCorrectRate.Appearance = appearance29;
		this.txtCorrectRate.AutoSize = true;
		this.txtCorrectRate.Enabled = false;
		this.txtCorrectRate.Location = new System.Drawing.Point(105, 313);
		this.txtCorrectRate.MaxLength = 200;
		this.txtCorrectRate.Name = "txtCorrectRate";
		this.txtCorrectRate.Size = new System.Drawing.Size(77, 21);
		this.txtCorrectRate.TabIndex = 67;
		this.toolTip1.SetToolTip(this.txtCorrectRate, "施工地點是 Excel 或報表匯出的資料");
		appearance30.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel44.Appearance = appearance30;
		this.ultraLabel44.Location = new System.Drawing.Point(12, 318);
		this.ultraLabel44.Name = "ultraLabel44";
		this.ultraLabel44.Size = new System.Drawing.Size(88, 16);
		this.ultraLabel44.TabIndex = 66;
		this.ultraLabel44.Text = "編碼正確率:";
		this.lbCityRequired.AutoSize = true;
		this.lbCityRequired.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbCityRequired.ForeColor = System.Drawing.Color.Red;
		this.lbCityRequired.Location = new System.Drawing.Point(3, 133);
		this.lbCityRequired.Name = "lbCityRequired";
		this.lbCityRequired.Size = new System.Drawing.Size(11, 12);
		this.lbCityRequired.TabIndex = 65;
		this.lbCityRequired.Text = "*";
		this.lbMainUnitRequired.AutoSize = true;
		this.lbMainUnitRequired.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbMainUnitRequired.ForeColor = System.Drawing.Color.Red;
		this.lbMainUnitRequired.Location = new System.Drawing.Point(4, 8);
		this.lbMainUnitRequired.Name = "lbMainUnitRequired";
		this.lbMainUnitRequired.Size = new System.Drawing.Size(11, 12);
		this.lbMainUnitRequired.TabIndex = 64;
		this.lbMainUnitRequired.Text = "*";
		dateButton1.Caption = "Today";
		this.ddlExpectDuration.DateButtons.Add(dateButton1);
		this.ddlExpectDuration.Location = new System.Drawing.Point(104, 248);
		this.ddlExpectDuration.Name = "ddlExpectDuration";
		this.ddlExpectDuration.NonAutoSizeHeight = 21;
		this.ddlExpectDuration.Size = new System.Drawing.Size(100, 21);
		this.ddlExpectDuration.TabIndex = 61;
		this.ddlExpectDuration.Value = new System.DateTime(2011, 1, 10, 0, 0, 0, 0);
		this.ddlDurationType.AutoSize = true;
		this.ddlDurationType.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		valueListItem1.DataValue = "0";
		valueListItem1.DisplayText = "工作天";
		valueListItem2.DataValue = "1";
		valueListItem2.DisplayText = "日曆天";
		valueListItem3.DataValue = "2";
		valueListItem3.DisplayText = "限期完成";
		this.ddlDurationType.Items.Add(valueListItem1);
		this.ddlDurationType.Items.Add(valueListItem2);
		this.ddlDurationType.Items.Add(valueListItem3);
		this.ddlDurationType.Location = new System.Drawing.Point(232, 247);
		this.ddlDurationType.Name = "ddlDurationType";
		this.ddlDurationType.Size = new System.Drawing.Size(100, 21);
		this.ddlDurationType.TabIndex = 59;
		this.ddlDurationType.Text = null;
		this.ddlDurationType.AfterCloseUp += new System.EventHandler(ddlDurationType_AfterCloseUp);
		appearance31.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbProjectScopeUnit.Appearance = appearance31;
		this.lbProjectScopeUnit.Location = new System.Drawing.Point(621, 126);
		this.lbProjectScopeUnit.Name = "lbProjectScopeUnit";
		this.lbProjectScopeUnit.Size = new System.Drawing.Size(45, 20);
		this.lbProjectScopeUnit.TabIndex = 58;
		this.ddlBudType.AutoSize = true;
		this.ddlBudType.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		valueListItem4.DataValue = "1";
		valueListItem4.DisplayText = "一般預算";
		valueListItem5.DataValue = "2";
		valueListItem5.DisplayText = "執行預算";
		valueListItem6.DataValue = "3";
		valueListItem6.DisplayText = "決標預算";
		valueListItem7.DataValue = "4";
		valueListItem7.DisplayText = "發包預算";
		this.ddlBudType.Items.Add(valueListItem4);
		this.ddlBudType.Items.Add(valueListItem5);
		this.ddlBudType.Items.Add(valueListItem6);
		this.ddlBudType.Items.Add(valueListItem7);
		this.ddlBudType.Location = new System.Drawing.Point(104, 272);
		this.ddlBudType.Name = "ddlBudType";
		this.ddlBudType.Size = new System.Drawing.Size(92, 21);
		this.ddlBudType.TabIndex = 56;
		this.ddlBudType.Text = null;
		this.ddlBudType.Visible = false;
		appearance32.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbBudType.Appearance = appearance32;
		this.lbBudType.Location = new System.Drawing.Point(12, 273);
		this.lbBudType.Name = "lbBudType";
		this.lbBudType.Size = new System.Drawing.Size(88, 20);
		this.lbBudType.TabIndex = 55;
		this.lbBudType.Text = "預算型態:";
		this.lbBudType.Visible = false;
		appearance33.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.tbProjectAddress.Appearance = appearance33;
		this.tbProjectAddress.AutoSize = true;
		this.tbProjectAddress.Location = new System.Drawing.Point(104, 176);
		this.tbProjectAddress.MaxLength = 200;
		this.tbProjectAddress.Name = "tbProjectAddress";
		this.tbProjectAddress.Size = new System.Drawing.Size(228, 21);
		this.tbProjectAddress.TabIndex = 54;
		this.toolTip1.SetToolTip(this.tbProjectAddress, "施工地點是 Excel 或報表匯出的資料");
		appearance34.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel36.Appearance = appearance34;
		this.ultraLabel36.Location = new System.Drawing.Point(12, 181);
		this.ultraLabel36.Name = "ultraLabel36";
		this.ultraLabel36.Size = new System.Drawing.Size(88, 16);
		this.ultraLabel36.TabIndex = 53;
		this.ultraLabel36.Text = "施工地點:";
		this.ddlProjectCity.AutoSize = true;
		this.ddlProjectCity.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		this.ddlProjectCity.Location = new System.Drawing.Point(104, 152);
		this.ddlProjectCity.Name = "ddlProjectCity";
		this.ddlProjectCity.Size = new System.Drawing.Size(228, 21);
		this.ddlProjectCity.TabIndex = 52;
		this.ddlProjectCity.Text = null;
		this.ddlProjectCity.ValueChanged += new System.EventHandler(ddlProjectCity_ValueChanged);
		this.ddlProjectArea.AutoSize = true;
		this.ddlProjectArea.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		valueListItem8.DataValue = "1";
		valueListItem8.DisplayText = "北";
		valueListItem9.DataValue = "2";
		valueListItem9.DisplayText = "中";
		valueListItem10.DataValue = "3";
		valueListItem10.DisplayText = "南";
		valueListItem11.DataValue = "4";
		valueListItem11.DisplayText = "東";
		valueListItem12.DataValue = "5";
		valueListItem12.DisplayText = "離島";
		this.ddlProjectArea.Items.Add(valueListItem8);
		this.ddlProjectArea.Items.Add(valueListItem9);
		this.ddlProjectArea.Items.Add(valueListItem10);
		this.ddlProjectArea.Items.Add(valueListItem11);
		this.ddlProjectArea.Items.Add(valueListItem12);
		this.ddlProjectArea.Location = new System.Drawing.Point(104, 128);
		this.ddlProjectArea.Name = "ddlProjectArea";
		this.ddlProjectArea.Size = new System.Drawing.Size(92, 21);
		this.ddlProjectArea.TabIndex = 51;
		this.ddlProjectArea.Text = null;
		this.ddlProjectArea.ValueChanged += new System.EventHandler(ddlProjectArea_ValueChanged);
		appearance35.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel23.Appearance = appearance35;
		this.ultraLabel23.Location = new System.Drawing.Point(12, 130);
		this.ultraLabel23.Name = "ultraLabel23";
		this.ultraLabel23.Size = new System.Drawing.Size(88, 20);
		this.ultraLabel23.TabIndex = 50;
		this.ultraLabel23.Text = "所在區域:";
		this.btnEditGPSLocation.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance36.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnEditGPSLocation.Appearance = appearance36;
		this.btnEditGPSLocation.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.btnEditGPSLocation.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnEditGPSLocation.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance37.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance37.BackColor2 = System.Drawing.Color.White;
		appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.btnEditGPSLocation.HotTrackAppearance = appearance37;
		this.btnEditGPSLocation.Location = new System.Drawing.Point(552, 272);
		this.btnEditGPSLocation.Name = "btnEditGPSLocation";
		this.btnEditGPSLocation.ShowFocusRect = false;
		this.btnEditGPSLocation.ShowOutline = false;
		this.btnEditGPSLocation.Size = new System.Drawing.Size(116, 28);
		this.btnEditGPSLocation.SupportThemes = false;
		this.btnEditGPSLocation.TabIndex = 49;
		this.btnEditGPSLocation.Text = "工程座標編修...";
		this.btnEditGPSLocation.Click += new System.EventHandler(btnEditGPSLocation_Click);
		dateButton2.Caption = "今天";
		this.txtExpectFinishDate.DateButtons.Add(dateButton2);
		this.txtExpectFinishDate.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.txtExpectFinishDate.Location = new System.Drawing.Point(448, 224);
		this.txtExpectFinishDate.Name = "txtExpectFinishDate";
		this.txtExpectFinishDate.NonAutoSizeHeight = 21;
		this.txtExpectFinishDate.NullDateLabel = "";
		this.txtExpectFinishDate.Size = new System.Drawing.Size(220, 21);
		this.txtExpectFinishDate.TabIndex = 39;
		this.txtExpectFinishDate.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.txtExpectFinishDate.Value = resources.GetObject("txtExpectFinishDate.Value");
		this.txtExpectFinishDate.WeekNumbersVisible = true;
		appearance38.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance38.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel35.Appearance = appearance38;
		this.ultraLabel35.Location = new System.Drawing.Point(352, 229);
		this.ultraLabel35.Name = "ultraLabel35";
		this.ultraLabel35.Size = new System.Drawing.Size(92, 16);
		this.ultraLabel35.TabIndex = 38;
		this.ultraLabel35.Text = "預定完工日:";
		this.tbProjectScope.AutoSize = true;
		this.tbProjectScope.Location = new System.Drawing.Point(448, 128);
		this.tbProjectScope.Name = "tbProjectScope";
		this.tbProjectScope.Size = new System.Drawing.Size(170, 21);
		this.tbProjectScope.TabIndex = 37;
		this.tbProjectScope.Validating += new System.ComponentModel.CancelEventHandler(inputNumber_Validating);
		appearance39.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblMainProjectCode.Appearance = appearance39;
		this.lblMainProjectCode.Location = new System.Drawing.Point(105, 103);
		this.lblMainProjectCode.Name = "lblMainProjectCode";
		this.lblMainProjectCode.Size = new System.Drawing.Size(109, 14);
		this.lblMainProjectCode.TabIndex = 36;
		this.lblMainProjectCode.Text = "[mainProjectCode]";
		dateButton3.Caption = "今天";
		this.txtExpectStartDate.DateButtons.Add(dateButton3);
		this.txtExpectStartDate.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.txtExpectStartDate.Location = new System.Drawing.Point(448, 200);
		this.txtExpectStartDate.Name = "txtExpectStartDate";
		this.txtExpectStartDate.NonAutoSizeHeight = 21;
		this.txtExpectStartDate.NullDateLabel = "";
		this.txtExpectStartDate.Size = new System.Drawing.Size(220, 21);
		this.txtExpectStartDate.TabIndex = 35;
		this.txtExpectStartDate.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.txtExpectStartDate.Value = resources.GetObject("txtExpectStartDate.Value");
		this.txtExpectStartDate.WeekNumbersVisible = true;
		appearance40.FontData.Name = "Arial";
		this.btnPickMainInstitute.Appearance = appearance40;
		this.btnPickMainInstitute.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnPickMainInstitute.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.btnPickMainInstitute.Location = new System.Drawing.Point(647, 5);
		this.btnPickMainInstitute.Name = "btnPickMainInstitute";
		this.btnPickMainInstitute.ShowFocusRect = false;
		this.btnPickMainInstitute.ShowOutline = false;
		this.btnPickMainInstitute.Size = new System.Drawing.Size(24, 20);
		this.btnPickMainInstitute.SupportThemes = false;
		this.btnPickMainInstitute.TabIndex = 34;
		this.btnPickMainInstitute.Text = "...";
		this.btnPickMainInstitute.Click += new System.EventHandler(btnPickMainInstitute_Click);
		appearance41.BackColorDisabled = System.Drawing.Color.White;
		appearance41.BackColorDisabled2 = System.Drawing.Color.White;
		appearance41.FontData.Name = "細明體";
		appearance41.FontData.SizeInPoints = 10f;
		appearance41.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.tbMainInstituite.Appearance = appearance41;
		this.tbMainInstituite.AutoSize = true;
		this.tbMainInstituite.Enabled = false;
		this.tbMainInstituite.FlatMode = true;
		this.tbMainInstituite.Location = new System.Drawing.Point(223, 5);
		this.tbMainInstituite.Name = "tbMainInstituite";
		this.tbMainInstituite.Size = new System.Drawing.Size(425, 20);
		this.tbMainInstituite.TabIndex = 33;
		this.tbMainInstituite.Text = "[主辦機關名稱]";
		appearance42.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel18.Appearance = appearance42;
		this.ultraLabel18.Location = new System.Drawing.Point(208, 228);
		this.ultraLabel18.Name = "ultraLabel18";
		this.ultraLabel18.Size = new System.Drawing.Size(17, 16);
		this.ultraLabel18.TabIndex = 32;
		this.ultraLabel18.Text = "～";
		appearance43.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance43.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel17.Appearance = appearance43;
		this.ultraLabel17.Location = new System.Drawing.Point(636, 180);
		this.ultraLabel17.Name = "ultraLabel17";
		this.ultraLabel17.Size = new System.Drawing.Size(32, 16);
		this.ultraLabel17.TabIndex = 31;
		this.ultraLabel17.Text = "年度";
		appearance44.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance44.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel16.Appearance = appearance44;
		this.ultraLabel16.Location = new System.Drawing.Point(516, 180);
		this.ultraLabel16.Name = "ultraLabel16";
		this.ultraLabel16.Size = new System.Drawing.Size(44, 16);
		this.ultraLabel16.TabIndex = 30;
		this.ultraLabel16.Text = "年度～";
		appearance45.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.tbBudEndYear.Appearance = appearance45;
		this.tbBudEndYear.AutoSize = true;
		this.tbBudEndYear.Location = new System.Drawing.Point(568, 176);
		this.tbBudEndYear.MaxLength = 4;
		this.tbBudEndYear.Name = "tbBudEndYear";
		this.tbBudEndYear.Size = new System.Drawing.Size(64, 21);
		this.tbBudEndYear.TabIndex = 29;
		this.tbBudEndYear.Validating += new System.ComponentModel.CancelEventHandler(inputText_Validating);
		this.tbBudEndYear.Leave += new System.EventHandler(inputText_Leave);
		appearance46.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.tbWorkUnit.Appearance = appearance46;
		this.tbWorkUnit.AutoSize = true;
		this.tbWorkUnit.Location = new System.Drawing.Point(448, 248);
		this.tbWorkUnit.MaxLength = 20;
		this.tbWorkUnit.Name = "tbWorkUnit";
		this.tbWorkUnit.Size = new System.Drawing.Size(220, 21);
		this.tbWorkUnit.TabIndex = 28;
		this.tbWorkUnit.Validating += new System.ComponentModel.CancelEventHandler(inputText_Validating);
		appearance47.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance47.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel15.Appearance = appearance47;
		this.ultraLabel15.Location = new System.Drawing.Point(352, 253);
		this.ultraLabel15.Name = "ultraLabel15";
		this.ultraLabel15.Size = new System.Drawing.Size(92, 16);
		this.ultraLabel15.TabIndex = 27;
		this.ultraLabel15.Text = "工程單位:";
		appearance48.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance48.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel14.Appearance = appearance48;
		this.ultraLabel14.Location = new System.Drawing.Point(352, 205);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(92, 16);
		this.ultraLabel14.TabIndex = 25;
		this.ultraLabel14.Text = "預計開工日:";
		appearance49.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.tbBudStartYear.Appearance = appearance49;
		this.tbBudStartYear.AutoSize = true;
		this.tbBudStartYear.Location = new System.Drawing.Point(448, 176);
		this.tbBudStartYear.MaxLength = 4;
		this.tbBudStartYear.Name = "tbBudStartYear";
		this.tbBudStartYear.Size = new System.Drawing.Size(64, 21);
		this.tbBudStartYear.TabIndex = 24;
		this.tbBudStartYear.Validating += new System.ComponentModel.CancelEventHandler(inputText_Validating);
		this.tbBudStartYear.Leave += new System.EventHandler(inputText_Leave);
		appearance50.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance50.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel13.Appearance = appearance50;
		this.ultraLabel13.Location = new System.Drawing.Point(352, 181);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(92, 16);
		this.ultraLabel13.TabIndex = 23;
		this.ultraLabel13.Text = "預算年度:";
		appearance51.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.tbWorkMode.Appearance = appearance51;
		this.tbWorkMode.AutoSize = true;
		this.tbWorkMode.Location = new System.Drawing.Point(448, 152);
		this.tbWorkMode.MaxLength = 10;
		this.tbWorkMode.Name = "tbWorkMode";
		this.tbWorkMode.Size = new System.Drawing.Size(220, 21);
		this.tbWorkMode.TabIndex = 22;
		this.tbWorkMode.Validating += new System.ComponentModel.CancelEventHandler(inputText_Validating);
		appearance52.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance52.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel12.Appearance = appearance52;
		this.ultraLabel12.Location = new System.Drawing.Point(352, 157);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(92, 16);
		this.ultraLabel12.TabIndex = 21;
		this.ultraLabel12.Text = "施工方式:";
		appearance53.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.tbAccountCodeUpper.Appearance = appearance53;
		this.tbAccountCodeUpper.AutoSize = true;
		this.tbAccountCodeUpper.Location = new System.Drawing.Point(232, 224);
		this.tbAccountCodeUpper.MaxLength = 20;
		this.tbAccountCodeUpper.Name = "tbAccountCodeUpper";
		this.tbAccountCodeUpper.Size = new System.Drawing.Size(100, 21);
		this.tbAccountCodeUpper.TabIndex = 20;
		this.tbAccountCodeUpper.Validating += new System.ComponentModel.CancelEventHandler(inputText_Validating);
		this.tbAccountCodeUpper.Leave += new System.EventHandler(inputText_Leave);
		appearance54.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.tbExpectDuration.Appearance = appearance54;
		this.tbExpectDuration.AutoSize = true;
		this.tbExpectDuration.Location = new System.Drawing.Point(104, 248);
		this.tbExpectDuration.MaxLength = 9;
		this.tbExpectDuration.Name = "tbExpectDuration";
		this.tbExpectDuration.Size = new System.Drawing.Size(100, 21);
		this.tbExpectDuration.TabIndex = 18;
		this.tbExpectDuration.Validating += new System.ComponentModel.CancelEventHandler(inputText_Validating);
		this.tbExpectDuration.Leave += new System.EventHandler(inputText_Leave);
		appearance55.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.tbAccountCodeLower.Appearance = appearance55;
		this.tbAccountCodeLower.AutoSize = true;
		this.tbAccountCodeLower.Location = new System.Drawing.Point(104, 224);
		this.tbAccountCodeLower.MaxLength = 20;
		this.tbAccountCodeLower.Name = "tbAccountCodeLower";
		this.tbAccountCodeLower.Size = new System.Drawing.Size(100, 21);
		this.tbAccountCodeLower.TabIndex = 17;
		this.tbAccountCodeLower.Validating += new System.ComponentModel.CancelEventHandler(inputText_Validating);
		this.tbAccountCodeLower.Leave += new System.EventHandler(inputText_Leave);
		appearance56.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.tbBuyMode.Appearance = appearance56;
		this.tbBuyMode.AutoSize = true;
		this.tbBuyMode.Location = new System.Drawing.Point(104, 200);
		this.tbBuyMode.MaxLength = 10;
		this.tbBuyMode.Name = "tbBuyMode";
		this.tbBuyMode.Size = new System.Drawing.Size(228, 21);
		this.tbBuyMode.TabIndex = 16;
		this.tbBuyMode.Validating += new System.ComponentModel.CancelEventHandler(inputText_Validating);
		this.tbBuyMode.Leave += new System.EventHandler(inputText_Leave);
		appearance57.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.tbEnglishProjectName.Appearance = appearance57;
		this.tbEnglishProjectName.AutoSize = true;
		this.tbEnglishProjectName.Location = new System.Drawing.Point(104, 76);
		this.tbEnglishProjectName.Name = "tbEnglishProjectName";
		this.tbEnglishProjectName.Size = new System.Drawing.Size(568, 21);
		this.tbEnglishProjectName.TabIndex = 14;
		appearance58.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.tbChineseProjectName.Appearance = appearance58;
		this.tbChineseProjectName.AutoSize = true;
		this.tbChineseProjectName.Location = new System.Drawing.Point(104, 52);
		this.tbChineseProjectName.Name = "tbChineseProjectName";
		this.tbChineseProjectName.Size = new System.Drawing.Size(568, 21);
		this.tbChineseProjectName.TabIndex = 13;
		appearance59.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.tbProjectCode.Appearance = appearance59;
		this.tbProjectCode.AutoSize = true;
		this.tbProjectCode.Location = new System.Drawing.Point(104, 28);
		this.tbProjectCode.Name = "tbProjectCode";
		this.tbProjectCode.ReadOnly = true;
		this.tbProjectCode.Size = new System.Drawing.Size(228, 21);
		this.tbProjectCode.TabIndex = 12;
		appearance60.BackColorDisabled = System.Drawing.Color.White;
		appearance60.BackColorDisabled2 = System.Drawing.Color.White;
		appearance60.FontData.SizeInPoints = 10f;
		appearance60.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.tbMainInstituteCode.Appearance = appearance60;
		this.tbMainInstituteCode.AutoSize = true;
		this.tbMainInstituteCode.Enabled = false;
		this.tbMainInstituteCode.FlatMode = true;
		this.tbMainInstituteCode.Location = new System.Drawing.Point(104, 5);
		this.tbMainInstituteCode.Name = "tbMainInstituteCode";
		this.tbMainInstituteCode.Size = new System.Drawing.Size(116, 20);
		this.tbMainInstituteCode.TabIndex = 11;
		this.tbMainInstituteCode.Text = "[主辦機關代碼]";
		appearance61.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel11.Appearance = appearance61;
		this.ultraLabel11.Location = new System.Drawing.Point(384, 132);
		this.ultraLabel11.Name = "ultraLabel11";
		this.ultraLabel11.Size = new System.Drawing.Size(64, 16);
		this.ultraLabel11.TabIndex = 9;
		this.ultraLabel11.Text = "工程規模:";
		appearance62.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel10.Appearance = appearance62;
		this.ultraLabel10.Location = new System.Drawing.Point(12, 253);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(88, 16);
		this.ultraLabel10.TabIndex = 8;
		this.ultraLabel10.Text = "預計工期:";
		appearance63.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel9.Appearance = appearance63;
		this.ultraLabel9.Location = new System.Drawing.Point(12, 229);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(88, 16);
		this.ultraLabel9.TabIndex = 7;
		this.ultraLabel9.Text = "會計科目:";
		appearance64.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel8.Appearance = appearance64;
		this.ultraLabel8.Location = new System.Drawing.Point(12, 205);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(88, 16);
		this.ultraLabel8.TabIndex = 6;
		this.ultraLabel8.Text = "發包方式:";
		appearance65.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel7.Appearance = appearance65;
		this.ultraLabel7.Location = new System.Drawing.Point(12, 157);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(88, 16);
		this.ultraLabel7.TabIndex = 5;
		this.ultraLabel7.Text = "工程所在縣市:";
		appearance66.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel6.Appearance = appearance66;
		this.ultraLabel6.Location = new System.Drawing.Point(12, 104);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(88, 16);
		this.ultraLabel6.TabIndex = 4;
		this.ultraLabel6.Text = "主工程代碼:";
		appearance67.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance67;
		this.ultraLabel5.Location = new System.Drawing.Point(12, 80);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(88, 16);
		this.ultraLabel5.TabIndex = 3;
		this.ultraLabel5.Text = "Project Name:";
		appearance68.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance68;
		this.ultraLabel4.Location = new System.Drawing.Point(12, 57);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(88, 16);
		this.ultraLabel4.TabIndex = 2;
		this.ultraLabel4.Text = "工程名稱:";
		appearance69.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance69;
		this.ultraLabel3.Location = new System.Drawing.Point(12, 33);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(88, 16);
		this.ultraLabel3.TabIndex = 1;
		this.ultraLabel3.Text = "工程代碼:";
		appearance70.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance70;
		this.ultraLabel2.Location = new System.Drawing.Point(12, 8);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(88, 16);
		this.ultraLabel2.TabIndex = 0;
		this.ultraLabel2.Text = "主辦單位:";
		this.panel4.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel4.Location = new System.Drawing.Point(0, 0);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(682, 8);
		this.panel4.TabIndex = 2;
		this.Tab_Other.Controls.Add(this.gbGreenItem);
		this.Tab_Other.Controls.Add(this.gbTendererInfo);
		this.Tab_Other.Controls.Add(this.gbBudget);
		this.Tab_Other.Controls.Add(this.gbConstructionType);
		this.Tab_Other.Controls.Add(this.panel5);
		this.Tab_Other.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_Other.Name = "Tab_Other";
		this.Tab_Other.Size = new System.Drawing.Size(682, 547);
		this.gbGreenItem.Controls.Add(this.tbGreenTotalRatio);
		this.gbGreenItem.Controls.Add(this.lbGreenTotalRatio);
		this.gbGreenItem.Controls.Add(this.btnRenameGreenRatio);
		this.gbGreenItem.Controls.Add(this.tbGreenEnergyRatio);
		this.gbGreenItem.Controls.Add(this.tbGreenMaterialRatio);
		this.gbGreenItem.Controls.Add(this.lbGreenEnergyRatio);
		this.gbGreenItem.Controls.Add(this.lbGreenMaterialRatio);
		this.gbGreenItem.Controls.Add(this.tbGreenMethodRatio);
		this.gbGreenItem.Controls.Add(this.tbGreenEnvRatio);
		this.gbGreenItem.Controls.Add(this.lbGreenMethodRatio);
		this.gbGreenItem.Controls.Add(this.lbGreenEnvRatio);
		this.gbGreenItem.Location = new System.Drawing.Point(8, 410);
		this.gbGreenItem.Name = "gbGreenItem";
		this.gbGreenItem.Size = new System.Drawing.Size(668, 97);
		this.gbGreenItem.TabIndex = 23;
		this.gbGreenItem.TabStop = false;
		this.gbGreenItem.Text = "綠色內涵";
		appearance71.FontData.Name = "細明體";
		appearance71.FontData.SizeInPoints = 9f;
		this.tbGreenTotalRatio.Appearance = appearance71;
		this.tbGreenTotalRatio.AutoSize = true;
		this.tbGreenTotalRatio.Location = new System.Drawing.Point(434, 74);
		this.tbGreenTotalRatio.MaxLength = 50;
		this.tbGreenTotalRatio.Name = "tbGreenTotalRatio";
		this.tbGreenTotalRatio.ReadOnly = true;
		this.tbGreenTotalRatio.Size = new System.Drawing.Size(206, 21);
		this.tbGreenTotalRatio.TabIndex = 56;
		appearance72.TextHAlign = Infragistics.Win.HAlign.Left;
		this.lbGreenTotalRatio.Appearance = appearance72;
		this.lbGreenTotalRatio.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbGreenTotalRatio.Location = new System.Drawing.Point(337, 79);
		this.lbGreenTotalRatio.Name = "lbGreenTotalRatio";
		this.lbGreenTotalRatio.Size = new System.Drawing.Size(110, 16);
		this.lbGreenTotalRatio.TabIndex = 55;
		this.lbGreenTotalRatio.Text = "總佔比：";
		this.btnRenameGreenRatio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance73.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnRenameGreenRatio.Appearance = appearance73;
		this.btnRenameGreenRatio.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.btnRenameGreenRatio.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnRenameGreenRatio.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance74.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance74.BackColor2 = System.Drawing.Color.White;
		appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.btnRenameGreenRatio.HotTrackAppearance = appearance74;
		this.btnRenameGreenRatio.HotTracking = true;
		this.btnRenameGreenRatio.Location = new System.Drawing.Point(3, 71);
		this.btnRenameGreenRatio.Name = "btnRenameGreenRatio";
		this.btnRenameGreenRatio.ShowFocusRect = false;
		this.btnRenameGreenRatio.ShowOutline = false;
		this.btnRenameGreenRatio.Size = new System.Drawing.Size(135, 24);
		this.btnRenameGreenRatio.SupportThemes = false;
		this.btnRenameGreenRatio.TabIndex = 54;
		this.btnRenameGreenRatio.Text = "修改綠色內涵指標名稱";
		this.btnRenameGreenRatio.Click += new System.EventHandler(btnRenameGreenRatio_Click);
		appearance75.FontData.Name = "細明體";
		appearance75.FontData.SizeInPoints = 9f;
		this.tbGreenEnergyRatio.Appearance = appearance75;
		this.tbGreenEnergyRatio.AutoSize = true;
		this.tbGreenEnergyRatio.Location = new System.Drawing.Point(434, 47);
		this.tbGreenEnergyRatio.MaxLength = 50;
		this.tbGreenEnergyRatio.Name = "tbGreenEnergyRatio";
		this.tbGreenEnergyRatio.ReadOnly = true;
		this.tbGreenEnergyRatio.Size = new System.Drawing.Size(206, 21);
		this.tbGreenEnergyRatio.TabIndex = 17;
		appearance76.FontData.Name = "細明體";
		appearance76.FontData.SizeInPoints = 9f;
		this.tbGreenMaterialRatio.Appearance = appearance76;
		this.tbGreenMaterialRatio.AutoSize = true;
		this.tbGreenMaterialRatio.Location = new System.Drawing.Point(434, 20);
		this.tbGreenMaterialRatio.MaxLength = 50;
		this.tbGreenMaterialRatio.Name = "tbGreenMaterialRatio";
		this.tbGreenMaterialRatio.ReadOnly = true;
		this.tbGreenMaterialRatio.Size = new System.Drawing.Size(206, 21);
		this.tbGreenMaterialRatio.TabIndex = 16;
		appearance77.TextHAlign = Infragistics.Win.HAlign.Left;
		this.lbGreenEnergyRatio.Appearance = appearance77;
		this.lbGreenEnergyRatio.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbGreenEnergyRatio.Location = new System.Drawing.Point(337, 52);
		this.lbGreenEnergyRatio.Name = "lbGreenEnergyRatio";
		this.lbGreenEnergyRatio.Size = new System.Drawing.Size(110, 16);
		this.lbGreenEnergyRatio.TabIndex = 15;
		this.lbGreenEnergyRatio.Text = "綠色能源佔比：";
		appearance78.TextHAlign = Infragistics.Win.HAlign.Left;
		this.lbGreenMaterialRatio.Appearance = appearance78;
		this.lbGreenMaterialRatio.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbGreenMaterialRatio.Location = new System.Drawing.Point(337, 26);
		this.lbGreenMaterialRatio.Name = "lbGreenMaterialRatio";
		this.lbGreenMaterialRatio.Size = new System.Drawing.Size(91, 16);
		this.lbGreenMaterialRatio.TabIndex = 13;
		this.lbGreenMaterialRatio.Text = "綠色材料佔比：";
		appearance79.FontData.Name = "細明體";
		appearance79.FontData.SizeInPoints = 9f;
		this.tbGreenMethodRatio.Appearance = appearance79;
		this.tbGreenMethodRatio.AutoSize = true;
		this.tbGreenMethodRatio.Location = new System.Drawing.Point(106, 47);
		this.tbGreenMethodRatio.MaxLength = 50;
		this.tbGreenMethodRatio.Name = "tbGreenMethodRatio";
		this.tbGreenMethodRatio.ReadOnly = true;
		this.tbGreenMethodRatio.Size = new System.Drawing.Size(206, 21);
		this.tbGreenMethodRatio.TabIndex = 9;
		appearance80.FontData.Name = "細明體";
		appearance80.FontData.SizeInPoints = 9f;
		this.tbGreenEnvRatio.Appearance = appearance80;
		this.tbGreenEnvRatio.AutoSize = true;
		this.tbGreenEnvRatio.Location = new System.Drawing.Point(106, 20);
		this.tbGreenEnvRatio.MaxLength = 50;
		this.tbGreenEnvRatio.Name = "tbGreenEnvRatio";
		this.tbGreenEnvRatio.ReadOnly = true;
		this.tbGreenEnvRatio.Size = new System.Drawing.Size(206, 21);
		this.tbGreenEnvRatio.TabIndex = 8;
		this.lbGreenMethodRatio.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbGreenMethodRatio.Location = new System.Drawing.Point(7, 52);
		this.lbGreenMethodRatio.Name = "lbGreenMethodRatio";
		this.lbGreenMethodRatio.Size = new System.Drawing.Size(91, 16);
		this.lbGreenMethodRatio.TabIndex = 4;
		this.lbGreenMethodRatio.Text = "綠色工法佔比：";
		this.lbGreenEnvRatio.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbGreenEnvRatio.Location = new System.Drawing.Point(7, 26);
		this.lbGreenEnvRatio.Name = "lbGreenEnvRatio";
		this.lbGreenEnvRatio.Size = new System.Drawing.Size(91, 16);
		this.lbGreenEnvRatio.TabIndex = 3;
		this.lbGreenEnvRatio.Text = "綠色環境佔比：";
		this.gbTendererInfo.Controls.Add(this.panel14);
		this.gbTendererInfo.Location = new System.Drawing.Point(8, 22);
		this.gbTendererInfo.Name = "gbTendererInfo";
		this.gbTendererInfo.Size = new System.Drawing.Size(668, 142);
		this.gbTendererInfo.TabIndex = 5;
		this.gbTendererInfo.TabStop = false;
		this.gbTendererInfo.Text = "投標廠商基本資料";
		this.panel14.Controls.Add(this.tbOwner);
		this.panel14.Controls.Add(this.lbOwner);
		this.panel14.Controls.Add(this.ultraLabel22);
		this.panel14.Controls.Add(this.btnPickInvoiceNo);
		this.panel14.Controls.Add(this.gridSublet);
		this.panel14.Controls.Add(this.tbVendorName);
		this.panel14.Controls.Add(this.tbVendorInvoiceNo);
		this.panel14.Controls.Add(this.ultraLabel21);
		this.panel14.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel14.Location = new System.Drawing.Point(3, 21);
		this.panel14.Name = "panel14";
		this.panel14.Size = new System.Drawing.Size(662, 99);
		this.panel14.TabIndex = 17;
		this.tbOwner.AutoSize = true;
		this.tbOwner.Location = new System.Drawing.Point(98, 66);
		this.tbOwner.Name = "tbOwner";
		this.tbOwner.Size = new System.Drawing.Size(142, 24);
		this.tbOwner.TabIndex = 20;
		this.toolTip1.SetToolTip(this.tbOwner, "此處存放廠商資料");
		appearance81.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbOwner.Appearance = appearance81;
		this.lbOwner.Location = new System.Drawing.Point(6, 70);
		this.lbOwner.Name = "lbOwner";
		this.lbOwner.Size = new System.Drawing.Size(84, 20);
		this.lbOwner.TabIndex = 19;
		this.lbOwner.Text = "負責人：";
		appearance82.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel22.Appearance = appearance82;
		this.ultraLabel22.Location = new System.Drawing.Point(6, 40);
		this.ultraLabel22.Name = "ultraLabel22";
		this.ultraLabel22.Size = new System.Drawing.Size(84, 20);
		this.ultraLabel22.TabIndex = 18;
		this.ultraLabel22.Text = "廠商名稱：";
		this.btnPickInvoiceNo.BackColor = System.Drawing.Color.Silver;
		this.btnPickInvoiceNo.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.btnPickInvoiceNo.Font = new System.Drawing.Font("Times New Roman", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnPickInvoiceNo.Location = new System.Drawing.Point(213, 6);
		this.btnPickInvoiceNo.Name = "btnPickInvoiceNo";
		this.btnPickInvoiceNo.ShowFocusRect = false;
		this.btnPickInvoiceNo.ShowOutline = false;
		this.btnPickInvoiceNo.Size = new System.Drawing.Size(27, 24);
		this.btnPickInvoiceNo.SupportThemes = false;
		this.btnPickInvoiceNo.TabIndex = 16;
		this.btnPickInvoiceNo.Text = "...";
		this.toolTip1.SetToolTip(this.btnPickInvoiceNo, "按此鈕挑選廠商統一編號");
		this.btnPickInvoiceNo.Click += new System.EventHandler(btnPickInvoiceNo_Click);
		this.gridSublet.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
		this.gridSublet.DisplayMember = "";
		this.gridSublet.Location = new System.Drawing.Point(218, 6);
		this.gridSublet.Name = "gridSublet";
		this.gridSublet.Size = new System.Drawing.Size(8, 24);
		this.gridSublet.TabIndex = 17;
		this.gridSublet.ValueMember = "";
		this.gridSublet.AfterCloseUp += new System.EventHandler(gridSublet_AfterCloseUp);
		this.tbVendorName.AutoSize = true;
		this.tbVendorName.Location = new System.Drawing.Point(98, 36);
		this.tbVendorName.Name = "tbVendorName";
		this.tbVendorName.Size = new System.Drawing.Size(432, 24);
		this.tbVendorName.TabIndex = 8;
		this.toolTip1.SetToolTip(this.tbVendorName, "此處存放廠商資料");
		this.tbVendorInvoiceNo.AutoSize = true;
		this.tbVendorInvoiceNo.Location = new System.Drawing.Point(98, 6);
		this.tbVendorInvoiceNo.Name = "tbVendorInvoiceNo";
		this.tbVendorInvoiceNo.Size = new System.Drawing.Size(116, 24);
		this.tbVendorInvoiceNo.TabIndex = 7;
		this.toolTip1.SetToolTip(this.tbVendorInvoiceNo, "此處填寫投標商統一編號");
		appearance83.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel21.Appearance = appearance83;
		this.ultraLabel21.Location = new System.Drawing.Point(6, 10);
		this.ultraLabel21.Name = "ultraLabel21";
		this.ultraLabel21.Size = new System.Drawing.Size(84, 20);
		this.ultraLabel21.TabIndex = 5;
		this.ultraLabel21.Text = "統一編號：";
		this.gbBudget.Controls.Add(this.Combo5);
		this.gbBudget.Controls.Add(this.ultraLabel34);
		this.gbBudget.Controls.Add(this.Combo4);
		this.gbBudget.Controls.Add(this.ultraLabel33);
		this.gbBudget.Controls.Add(this.Combo3);
		this.gbBudget.Controls.Add(this.ultraLabel32);
		this.gbBudget.Controls.Add(this.Combo2);
		this.gbBudget.Controls.Add(this.ultraLabel31);
		this.gbBudget.Controls.Add(this.Combo1);
		this.gbBudget.Controls.Add(this.ultraLabel30);
		this.gbBudget.Controls.Add(this.txt7);
		this.gbBudget.Controls.Add(this.txt6);
		this.gbBudget.Controls.Add(this.txt5);
		this.gbBudget.Controls.Add(this.txt4);
		this.gbBudget.Controls.Add(this.txt3);
		this.gbBudget.Controls.Add(this.ultraLabel29);
		this.gbBudget.Controls.Add(this.ultraLabel28);
		this.gbBudget.Controls.Add(this.ultraLabel27);
		this.gbBudget.Controls.Add(this.ultraLabel26);
		this.gbBudget.Controls.Add(this.ultraLabel25);
		this.gbBudget.Location = new System.Drawing.Point(8, 338);
		this.gbBudget.Name = "gbBudget";
		this.gbBudget.Size = new System.Drawing.Size(668, 156);
		this.gbBudget.TabIndex = 4;
		this.gbBudget.TabStop = false;
		this.gbBudget.Text = "預算及底價";
		appearance84.FontData.SizeInPoints = 9f;
		this.Combo5.Appearance = appearance84;
		dateButton4.Caption = "今天";
		this.Combo5.DateButtons.Add(dateButton4);
		this.Combo5.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.Combo5.Location = new System.Drawing.Point(464, 124);
		this.Combo5.Name = "Combo5";
		this.Combo5.NonAutoSizeHeight = 21;
		this.Combo5.NullDateLabel = "";
		this.Combo5.Size = new System.Drawing.Size(188, 21);
		this.Combo5.TabIndex = 22;
		this.Combo5.Value = resources.GetObject("Combo5.Value");
		this.Combo5.WeekNumbersVisible = true;
		appearance85.TextHAlign = Infragistics.Win.HAlign.Left;
		this.ultraLabel34.Appearance = appearance85;
		this.ultraLabel34.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel34.Location = new System.Drawing.Point(337, 128);
		this.ultraLabel34.Name = "ultraLabel34";
		this.ultraLabel34.Size = new System.Drawing.Size(97, 16);
		this.ultraLabel34.TabIndex = 21;
		this.ultraLabel34.Text = "決標價核定日期:";
		appearance86.FontData.SizeInPoints = 9f;
		this.Combo4.Appearance = appearance86;
		dateButton5.Caption = "今天";
		this.Combo4.DateButtons.Add(dateButton5);
		this.Combo4.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.Combo4.Location = new System.Drawing.Point(464, 99);
		this.Combo4.Name = "Combo4";
		this.Combo4.NonAutoSizeHeight = 21;
		this.Combo4.NullDateLabel = "";
		this.Combo4.Size = new System.Drawing.Size(188, 21);
		this.Combo4.TabIndex = 20;
		this.Combo4.Value = resources.GetObject("Combo4.Value");
		this.Combo4.WeekNumbersVisible = true;
		appearance87.TextHAlign = Infragistics.Win.HAlign.Left;
		this.ultraLabel33.Appearance = appearance87;
		this.ultraLabel33.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel33.Location = new System.Drawing.Point(337, 104);
		this.ultraLabel33.Name = "ultraLabel33";
		this.ultraLabel33.Size = new System.Drawing.Size(110, 16);
		this.ultraLabel33.TabIndex = 19;
		this.ultraLabel33.Text = "核定底價核定日期:";
		appearance88.FontData.SizeInPoints = 9f;
		this.Combo3.Appearance = appearance88;
		dateButton6.Caption = "今天";
		this.Combo3.DateButtons.Add(dateButton6);
		this.Combo3.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.Combo3.Location = new System.Drawing.Point(464, 74);
		this.Combo3.Name = "Combo3";
		this.Combo3.NonAutoSizeHeight = 21;
		this.Combo3.NullDateLabel = "";
		this.Combo3.Size = new System.Drawing.Size(188, 21);
		this.Combo3.TabIndex = 18;
		this.Combo3.Value = resources.GetObject("Combo3.Value");
		this.Combo3.WeekNumbersVisible = true;
		appearance89.TextHAlign = Infragistics.Win.HAlign.Left;
		this.ultraLabel32.Appearance = appearance89;
		this.ultraLabel32.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel32.Location = new System.Drawing.Point(337, 78);
		this.ultraLabel32.Name = "ultraLabel32";
		this.ultraLabel32.Size = new System.Drawing.Size(110, 16);
		this.ultraLabel32.TabIndex = 17;
		this.ultraLabel32.Text = "預估底價核定日期:";
		appearance90.FontData.SizeInPoints = 9f;
		this.Combo2.Appearance = appearance90;
		dateButton7.Caption = "今天";
		this.Combo2.DateButtons.Add(dateButton7);
		this.Combo2.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.Combo2.Location = new System.Drawing.Point(464, 49);
		this.Combo2.Name = "Combo2";
		this.Combo2.NonAutoSizeHeight = 21;
		this.Combo2.NullDateLabel = "";
		this.Combo2.Size = new System.Drawing.Size(188, 21);
		this.Combo2.TabIndex = 16;
		this.Combo2.Value = resources.GetObject("Combo2.Value");
		this.Combo2.WeekNumbersVisible = true;
		appearance91.TextHAlign = Infragistics.Win.HAlign.Left;
		this.ultraLabel31.Appearance = appearance91;
		this.ultraLabel31.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel31.Location = new System.Drawing.Point(337, 52);
		this.ultraLabel31.Name = "ultraLabel31";
		this.ultraLabel31.Size = new System.Drawing.Size(110, 16);
		this.ultraLabel31.TabIndex = 15;
		this.ultraLabel31.Text = "發包預算核定日期:";
		appearance92.FontData.SizeInPoints = 9f;
		this.Combo1.Appearance = appearance92;
		dateButton8.Caption = "今天";
		this.Combo1.DateButtons.Add(dateButton8);
		this.Combo1.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.Combo1.Location = new System.Drawing.Point(464, 24);
		this.Combo1.Name = "Combo1";
		this.Combo1.NonAutoSizeHeight = 21;
		this.Combo1.NullDateLabel = "";
		this.Combo1.Size = new System.Drawing.Size(188, 21);
		this.Combo1.TabIndex = 14;
		this.Combo1.Value = resources.GetObject("Combo1.Value");
		this.Combo1.WeekNumbersVisible = true;
		appearance93.TextHAlign = Infragistics.Win.HAlign.Left;
		this.ultraLabel30.Appearance = appearance93;
		this.ultraLabel30.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel30.Location = new System.Drawing.Point(337, 28);
		this.ultraLabel30.Name = "ultraLabel30";
		this.ultraLabel30.Size = new System.Drawing.Size(110, 16);
		this.ultraLabel30.TabIndex = 13;
		this.ultraLabel30.Text = "設計預算核定日期:";
		appearance94.FontData.Name = "細明體";
		appearance94.FontData.SizeInPoints = 9f;
		this.txt7.Appearance = appearance94;
		this.txt7.AutoSize = true;
		this.txt7.Location = new System.Drawing.Point(88, 124);
		this.txt7.MaxLength = 50;
		this.txt7.Name = "txt7";
		this.txt7.Size = new System.Drawing.Size(232, 21);
		this.txt7.TabIndex = 12;
		this.txt7.Validating += new System.ComponentModel.CancelEventHandler(inputNumber_Validating);
		appearance95.FontData.Name = "細明體";
		appearance95.FontData.SizeInPoints = 9f;
		this.txt6.Appearance = appearance95;
		this.txt6.AutoSize = true;
		this.txt6.Location = new System.Drawing.Point(88, 99);
		this.txt6.MaxLength = 50;
		this.txt6.Name = "txt6";
		this.txt6.Size = new System.Drawing.Size(232, 21);
		this.txt6.TabIndex = 11;
		this.txt6.Validating += new System.ComponentModel.CancelEventHandler(inputNumber_Validating);
		appearance96.FontData.Name = "細明體";
		appearance96.FontData.SizeInPoints = 9f;
		this.txt5.Appearance = appearance96;
		this.txt5.AutoSize = true;
		this.txt5.Location = new System.Drawing.Point(88, 74);
		this.txt5.MaxLength = 50;
		this.txt5.Name = "txt5";
		this.txt5.Size = new System.Drawing.Size(232, 21);
		this.txt5.TabIndex = 10;
		this.txt5.Validating += new System.ComponentModel.CancelEventHandler(inputNumber_Validating);
		appearance97.FontData.Name = "細明體";
		appearance97.FontData.SizeInPoints = 9f;
		this.txt4.Appearance = appearance97;
		this.txt4.AutoSize = true;
		this.txt4.Location = new System.Drawing.Point(88, 49);
		this.txt4.MaxLength = 50;
		this.txt4.Name = "txt4";
		this.txt4.Size = new System.Drawing.Size(232, 21);
		this.txt4.TabIndex = 9;
		this.txt4.Validating += new System.ComponentModel.CancelEventHandler(inputNumber_Validating);
		appearance98.FontData.Name = "細明體";
		appearance98.FontData.SizeInPoints = 9f;
		this.txt3.Appearance = appearance98;
		this.txt3.AutoSize = true;
		this.txt3.Location = new System.Drawing.Point(88, 24);
		this.txt3.MaxLength = 50;
		this.txt3.Name = "txt3";
		this.txt3.Size = new System.Drawing.Size(232, 21);
		this.txt3.TabIndex = 8;
		this.txt3.Validating += new System.ComponentModel.CancelEventHandler(inputNumber_Validating);
		this.ultraLabel29.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel29.Location = new System.Drawing.Point(7, 128);
		this.ultraLabel29.Name = "ultraLabel29";
		this.ultraLabel29.Size = new System.Drawing.Size(48, 16);
		this.ultraLabel29.TabIndex = 7;
		this.ultraLabel29.Text = "決標價:";
		this.ultraLabel28.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel28.Location = new System.Drawing.Point(7, 104);
		this.ultraLabel28.Name = "ultraLabel28";
		this.ultraLabel28.Size = new System.Drawing.Size(60, 16);
		this.ultraLabel28.TabIndex = 6;
		this.ultraLabel28.Text = "核定底價:";
		this.ultraLabel27.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel27.Location = new System.Drawing.Point(7, 78);
		this.ultraLabel27.Name = "ultraLabel27";
		this.ultraLabel27.Size = new System.Drawing.Size(60, 16);
		this.ultraLabel27.TabIndex = 5;
		this.ultraLabel27.Text = "預估底價:";
		this.ultraLabel26.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel26.Location = new System.Drawing.Point(7, 53);
		this.ultraLabel26.Name = "ultraLabel26";
		this.ultraLabel26.Size = new System.Drawing.Size(60, 16);
		this.ultraLabel26.TabIndex = 4;
		this.ultraLabel26.Text = "發包預算:";
		this.ultraLabel25.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel25.Location = new System.Drawing.Point(7, 28);
		this.ultraLabel25.Name = "ultraLabel25";
		this.ultraLabel25.Size = new System.Drawing.Size(60, 16);
		this.ultraLabel25.TabIndex = 3;
		this.ultraLabel25.Text = "設計預算:";
		this.gbConstructionType.Controls.Add(this.chkStage6);
		this.gbConstructionType.Controls.Add(this.chkStage5);
		this.gbConstructionType.Controls.Add(this.chkStage4);
		this.gbConstructionType.Controls.Add(this.chkStage3);
		this.gbConstructionType.Controls.Add(this.chkStage2);
		this.gbConstructionType.Controls.Add(this.chkStage1);
		this.gbConstructionType.Controls.Add(this.lbProjectClassificationRequired);
		this.gbConstructionType.Controls.Add(this.panel12);
		this.gbConstructionType.Controls.Add(this.ultraLabel20);
		this.gbConstructionType.Controls.Add(this.ddlProjectClassification);
		this.gbConstructionType.Controls.Add(this.ultraLabel19);
		this.gbConstructionType.Controls.Add(this.ultraLabel24);
		this.gbConstructionType.Location = new System.Drawing.Point(8, 128);
		this.gbConstructionType.Name = "gbConstructionType";
		this.gbConstructionType.Size = new System.Drawing.Size(668, 204);
		this.gbConstructionType.TabIndex = 3;
		this.gbConstructionType.TabStop = false;
		this.gbConstructionType.Text = "工程分類";
		appearance99.FontData.SizeInPoints = 9f;
		this.chkStage6.Appearance = appearance99;
		this.chkStage6.Location = new System.Drawing.Point(464, 53);
		this.chkStage6.Name = "chkStage6";
		this.chkStage6.Size = new System.Drawing.Size(136, 18);
		this.chkStage6.TabIndex = 71;
		this.chkStage6.Text = "發包及施工階段";
		appearance100.FontData.SizeInPoints = 9f;
		this.chkStage5.Appearance = appearance100;
		this.chkStage5.Location = new System.Drawing.Point(464, 37);
		this.chkStage5.Name = "chkStage5";
		this.chkStage5.Size = new System.Drawing.Size(136, 18);
		this.chkStage5.TabIndex = 70;
		this.chkStage5.Text = "詳細設計階段";
		appearance101.FontData.SizeInPoints = 9f;
		this.chkStage4.Appearance = appearance101;
		this.chkStage4.Location = new System.Drawing.Point(464, 21);
		this.chkStage4.Name = "chkStage4";
		this.chkStage4.Size = new System.Drawing.Size(136, 18);
		this.chkStage4.TabIndex = 69;
		this.chkStage4.Text = "基本設計階段";
		appearance102.FontData.SizeInPoints = 9f;
		this.chkStage3.Appearance = appearance102;
		this.chkStage3.Location = new System.Drawing.Point(348, 53);
		this.chkStage3.Name = "chkStage3";
		this.chkStage3.Size = new System.Drawing.Size(136, 18);
		this.chkStage3.TabIndex = 68;
		this.chkStage3.Text = "綜合規劃階段";
		appearance103.FontData.SizeInPoints = 9f;
		this.chkStage2.Appearance = appearance103;
		this.chkStage2.Location = new System.Drawing.Point(348, 37);
		this.chkStage2.Name = "chkStage2";
		this.chkStage2.Size = new System.Drawing.Size(136, 18);
		this.chkStage2.TabIndex = 67;
		this.chkStage2.Text = "先期規劃階段";
		appearance104.FontData.SizeInPoints = 9f;
		this.chkStage1.Appearance = appearance104;
		this.chkStage1.Location = new System.Drawing.Point(348, 21);
		this.chkStage1.Name = "chkStage1";
		this.chkStage1.Size = new System.Drawing.Size(136, 18);
		this.chkStage1.TabIndex = 66;
		this.chkStage1.Text = "可行性評估階段";
		this.lbProjectClassificationRequired.AutoSize = true;
		this.lbProjectClassificationRequired.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbProjectClassificationRequired.ForeColor = System.Drawing.Color.Red;
		this.lbProjectClassificationRequired.Location = new System.Drawing.Point(5, 24);
		this.lbProjectClassificationRequired.Name = "lbProjectClassificationRequired";
		this.lbProjectClassificationRequired.Size = new System.Drawing.Size(11, 12);
		this.lbProjectClassificationRequired.TabIndex = 65;
		this.lbProjectClassificationRequired.Text = "*";
		this.panel12.BackColor = System.Drawing.Color.White;
		this.panel12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel12.Controls.Add(this.chk22);
		this.panel12.Controls.Add(this.chk21);
		this.panel12.Controls.Add(this.chk20);
		this.panel12.Controls.Add(this.chk19);
		this.panel12.Controls.Add(this.chk18);
		this.panel12.Controls.Add(this.chk17);
		this.panel12.Controls.Add(this.chk16);
		this.panel12.Controls.Add(this.chk15);
		this.panel12.Controls.Add(this.chk14);
		this.panel12.Controls.Add(this.chk13);
		this.panel12.Controls.Add(this.chk12);
		this.panel12.Controls.Add(this.chk11);
		this.panel12.Controls.Add(this.chk10);
		this.panel12.Controls.Add(this.chk09);
		this.panel12.Controls.Add(this.chk08);
		this.panel12.Controls.Add(this.chk07);
		this.panel12.Controls.Add(this.chk06);
		this.panel12.Controls.Add(this.chk05);
		this.panel12.Controls.Add(this.chk04);
		this.panel12.Controls.Add(this.chk03);
		this.panel12.Controls.Add(this.chk02);
		this.panel12.Controls.Add(this.chk01);
		this.panel12.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel12.Location = new System.Drawing.Point(16, 72);
		this.panel12.Name = "panel12";
		this.panel12.Size = new System.Drawing.Size(636, 124);
		this.panel12.TabIndex = 3;
		this.chk22.Location = new System.Drawing.Point(486, 52);
		this.chk22.Name = "chk22";
		this.chk22.Size = new System.Drawing.Size(136, 20);
		this.chk22.TabIndex = 21;
		this.chk22.Text = "其他工程";
		this.chk21.Location = new System.Drawing.Point(486, 36);
		this.chk21.Name = "chk21";
		this.chk21.Size = new System.Drawing.Size(136, 20);
		this.chk21.TabIndex = 20;
		this.chk21.Text = "交控工程";
		this.chk20.Location = new System.Drawing.Point(486, 20);
		this.chk20.Name = "chk20";
		this.chk20.Size = new System.Drawing.Size(136, 20);
		this.chk20.TabIndex = 19;
		this.chk20.Text = "土地重劃工程";
		this.chk19.Location = new System.Drawing.Point(486, 4);
		this.chk19.Name = "chk19";
		this.chk19.Size = new System.Drawing.Size(136, 20);
		this.chk19.TabIndex = 18;
		this.chk19.Text = "工業區開發工程";
		this.chk18.Location = new System.Drawing.Point(319, 84);
		this.chk18.Name = "chk18";
		this.chk18.Size = new System.Drawing.Size(136, 20);
		this.chk18.TabIndex = 17;
		this.chk18.Text = "建築工程";
		this.chk17.Location = new System.Drawing.Point(319, 68);
		this.chk17.Name = "chk17";
		this.chk17.Size = new System.Drawing.Size(136, 20);
		this.chk17.TabIndex = 16;
		this.chk17.Text = "山坡地開發";
		this.chk16.Location = new System.Drawing.Point(319, 52);
		this.chk16.Name = "chk16";
		this.chk16.Size = new System.Drawing.Size(136, 20);
		this.chk16.TabIndex = 15;
		this.chk16.Text = "土方資源場工程";
		this.chk15.Location = new System.Drawing.Point(319, 36);
		this.chk15.Name = "chk15";
		this.chk15.Size = new System.Drawing.Size(136, 20);
		this.chk15.TabIndex = 14;
		this.chk15.Text = "掩埋場工程";
		this.chk14.Location = new System.Drawing.Point(319, 20);
		this.chk14.Name = "chk14";
		this.chk14.Size = new System.Drawing.Size(136, 20);
		this.chk14.TabIndex = 13;
		this.chk14.Text = "焚化廠工程";
		this.chk13.Location = new System.Drawing.Point(319, 4);
		this.chk13.Name = "chk13";
		this.chk13.Size = new System.Drawing.Size(136, 20);
		this.chk13.TabIndex = 12;
		this.chk13.Text = "污水處理廠工程";
		this.chk12.Location = new System.Drawing.Point(166, 84);
		this.chk12.Name = "chk12";
		this.chk12.Size = new System.Drawing.Size(136, 20);
		this.chk12.TabIndex = 11;
		this.chk12.Text = "下水道工程";
		this.chk11.Location = new System.Drawing.Point(166, 68);
		this.chk11.Name = "chk11";
		this.chk11.Size = new System.Drawing.Size(136, 20);
		this.chk11.TabIndex = 10;
		this.chk11.Text = "河川整治工程";
		this.chk10.Location = new System.Drawing.Point(166, 52);
		this.chk10.Name = "chk10";
		this.chk10.Size = new System.Drawing.Size(136, 20);
		this.chk10.TabIndex = 9;
		this.chk10.Text = "自來水工程";
		this.chk09.Location = new System.Drawing.Point(166, 36);
		this.chk09.Name = "chk09";
		this.chk09.Size = new System.Drawing.Size(136, 20);
		this.chk09.TabIndex = 8;
		this.chk09.Text = "水力發電工程";
		this.chk08.Location = new System.Drawing.Point(166, 20);
		this.chk08.Name = "chk08";
		this.chk08.Size = new System.Drawing.Size(136, 20);
		this.chk08.TabIndex = 7;
		this.chk08.Text = "水庫工程";
		this.chk07.Location = new System.Drawing.Point(166, 4);
		this.chk07.Name = "chk07";
		this.chk07.Size = new System.Drawing.Size(136, 20);
		this.chk07.TabIndex = 6;
		this.chk07.Text = "港灣工程";
		this.chk06.Location = new System.Drawing.Point(8, 84);
		this.chk06.Name = "chk06";
		this.chk06.Size = new System.Drawing.Size(136, 20);
		this.chk06.TabIndex = 5;
		this.chk06.Text = "機場工程";
		this.chk05.Location = new System.Drawing.Point(8, 68);
		this.chk05.Name = "chk05";
		this.chk05.Size = new System.Drawing.Size(136, 20);
		this.chk05.TabIndex = 4;
		this.chk05.Text = "捷運系統工程";
		this.chk04.Location = new System.Drawing.Point(8, 52);
		this.chk04.Name = "chk04";
		this.chk04.Size = new System.Drawing.Size(136, 20);
		this.chk04.TabIndex = 3;
		this.chk04.Text = "隧道工程";
		this.chk03.Location = new System.Drawing.Point(8, 36);
		this.chk03.Name = "chk03";
		this.chk03.Size = new System.Drawing.Size(136, 20);
		this.chk03.TabIndex = 2;
		this.chk03.Text = "橋梁工程";
		this.chk02.Location = new System.Drawing.Point(8, 20);
		this.chk02.Name = "chk02";
		this.chk02.Size = new System.Drawing.Size(136, 20);
		this.chk02.TabIndex = 1;
		this.chk02.Text = "鐵路工程";
		this.chk01.Location = new System.Drawing.Point(8, 4);
		this.chk01.Name = "chk01";
		this.chk01.Size = new System.Drawing.Size(136, 20);
		this.chk01.TabIndex = 0;
		this.chk01.Text = "公路工程";
		this.ultraLabel20.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel20.Location = new System.Drawing.Point(14, 53);
		this.ultraLabel20.Name = "ultraLabel20";
		this.ultraLabel20.Size = new System.Drawing.Size(210, 12);
		this.ultraLabel20.TabIndex = 2;
		this.ultraLabel20.Text = "次要分類:(可多選)";
		appearance105.FontData.Name = "細明體";
		appearance105.FontData.SizeInPoints = 9f;
		this.ddlProjectClassification.Appearance = appearance105;
		this.ddlProjectClassification.AutoSize = true;
		this.ddlProjectClassification.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		valueListItem13.DataValue = "01";
		valueListItem13.DisplayText = "建築工程";
		valueListItem14.DataValue = "02";
		valueListItem14.DisplayText = "隧道工程";
		valueListItem15.DataValue = "03";
		valueListItem15.DisplayText = "自來水工程";
		valueListItem16.DataValue = "04";
		valueListItem16.DisplayText = "機場工程";
		valueListItem17.DataValue = "05";
		valueListItem17.DisplayText = "高速公路工程";
		valueListItem18.DataValue = "06";
		valueListItem18.DisplayText = "橋梁工程";
		valueListItem19.DataValue = "07";
		valueListItem19.DisplayText = "捷運系統工程";
		valueListItem20.DataValue = "08";
		valueListItem20.DisplayText = "發電工程";
		valueListItem21.DataValue = "09";
		valueListItem21.DisplayText = "公路工程";
		valueListItem22.DataValue = "10";
		valueListItem22.DisplayText = "污水處理廠工程";
		valueListItem23.DataValue = "11";
		valueListItem23.DisplayText = "市區道路工程";
		valueListItem24.DataValue = "12";
		valueListItem24.DisplayText = "高速鐵路工程";
		valueListItem25.DataValue = "13";
		valueListItem25.DisplayText = "鐵路工程";
		valueListItem26.DataValue = "14";
		valueListItem26.DisplayText = "明挖覆蓋隊道工程";
		valueListItem27.DataValue = "15";
		valueListItem27.DisplayText = "港灣工程";
		valueListItem28.DataValue = "16";
		valueListItem28.DisplayText = "水庫工程";
		valueListItem29.DataValue = "17";
		valueListItem29.DisplayText = "河川整治工程";
		valueListItem30.DataValue = "18";
		valueListItem30.DisplayText = "灌溉排水工程";
		valueListItem31.DataValue = "19";
		valueListItem31.DisplayText = "海洋放流工程";
		valueListItem32.DataValue = "20";
		valueListItem32.DisplayText = "焚化廠工程";
		valueListItem33.DataValue = "21";
		valueListItem33.DisplayText = "掩埋場工程";
		valueListItem34.DataValue = "22";
		valueListItem34.DisplayText = "土地重劃工程";
		valueListItem35.DataValue = "23";
		valueListItem35.DisplayText = "水力發電工程";
		valueListItem36.DataValue = "24";
		valueListItem36.DisplayText = "下水道工程";
		valueListItem37.DataValue = "25";
		valueListItem37.DisplayText = "土方資源場工程";
		valueListItem38.DataValue = "26";
		valueListItem38.DisplayText = "山坡地開發";
		valueListItem39.DataValue = "27";
		valueListItem39.DisplayText = "工業區開發工程";
		valueListItem40.DataValue = "28";
		valueListItem40.DisplayText = "其他工程";
		this.ddlProjectClassification.Items.Add(valueListItem13);
		this.ddlProjectClassification.Items.Add(valueListItem14);
		this.ddlProjectClassification.Items.Add(valueListItem15);
		this.ddlProjectClassification.Items.Add(valueListItem16);
		this.ddlProjectClassification.Items.Add(valueListItem17);
		this.ddlProjectClassification.Items.Add(valueListItem18);
		this.ddlProjectClassification.Items.Add(valueListItem19);
		this.ddlProjectClassification.Items.Add(valueListItem20);
		this.ddlProjectClassification.Items.Add(valueListItem21);
		this.ddlProjectClassification.Items.Add(valueListItem22);
		this.ddlProjectClassification.Items.Add(valueListItem23);
		this.ddlProjectClassification.Items.Add(valueListItem24);
		this.ddlProjectClassification.Items.Add(valueListItem25);
		this.ddlProjectClassification.Items.Add(valueListItem26);
		this.ddlProjectClassification.Items.Add(valueListItem27);
		this.ddlProjectClassification.Items.Add(valueListItem28);
		this.ddlProjectClassification.Items.Add(valueListItem29);
		this.ddlProjectClassification.Items.Add(valueListItem30);
		this.ddlProjectClassification.Items.Add(valueListItem31);
		this.ddlProjectClassification.Items.Add(valueListItem32);
		this.ddlProjectClassification.Items.Add(valueListItem33);
		this.ddlProjectClassification.Items.Add(valueListItem34);
		this.ddlProjectClassification.Items.Add(valueListItem35);
		this.ddlProjectClassification.Items.Add(valueListItem36);
		this.ddlProjectClassification.Items.Add(valueListItem37);
		this.ddlProjectClassification.Items.Add(valueListItem38);
		this.ddlProjectClassification.Items.Add(valueListItem39);
		this.ddlProjectClassification.Items.Add(valueListItem40);
		this.ddlProjectClassification.Location = new System.Drawing.Point(109, 20);
		this.ddlProjectClassification.Name = "ddlProjectClassification";
		this.ddlProjectClassification.Size = new System.Drawing.Size(144, 21);
		this.ddlProjectClassification.TabIndex = 1;
		this.ddlProjectClassification.Text = null;
		this.ddlProjectClassification.ValueChanged += new System.EventHandler(CboMainKind_ValueChanged);
		this.ultraLabel19.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel19.Location = new System.Drawing.Point(16, 24);
		this.ultraLabel19.Name = "ultraLabel19";
		this.ultraLabel19.Size = new System.Drawing.Size(87, 12);
		this.ultraLabel19.TabIndex = 0;
		this.ultraLabel19.Text = "主要工程分類:";
		appearance106.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel24.Appearance = appearance106;
		this.ultraLabel24.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel24.Location = new System.Drawing.Point(279, 24);
		this.ultraLabel24.Name = "ultraLabel24";
		this.ultraLabel24.Size = new System.Drawing.Size(63, 20);
		this.ultraLabel24.TabIndex = 13;
		this.ultraLabel24.Text = "計畫階段:";
		this.panel5.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(682, 8);
		this.panel5.TabIndex = 2;
		this.ultraTabPageControl2.Controls.Add(this.groupBox4);
		this.ultraTabPageControl2.Controls.Add(this.pnSummary);
		this.ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabPageControl2.Name = "ultraTabPageControl2";
		this.ultraTabPageControl2.Size = new System.Drawing.Size(682, 547);
		this.groupBox4.Controls.Add(this.lbMainKind);
		this.groupBox4.Controls.Add(this.lbProjectName);
		this.groupBox4.Controls.Add(this.lbCostWithTax);
		this.groupBox4.Controls.Add(this.ultraLabel37);
		this.groupBox4.Controls.Add(this.lbCostWithoutTax);
		this.groupBox4.Controls.Add(this.ultraLabel38);
		this.groupBox4.Controls.Add(this.lbProjectAddress);
		this.groupBox4.Controls.Add(this.ultraLabel39);
		this.groupBox4.Controls.Add(this.lbMainInstitute);
		this.groupBox4.Controls.Add(this.ultraLabel40);
		this.groupBox4.Controls.Add(this.ultraLabel41);
		this.groupBox4.Controls.Add(this.ultraLabel42);
		this.groupBox4.Location = new System.Drawing.Point(3, 0);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(676, 116);
		this.groupBox4.TabIndex = 16;
		this.groupBox4.TabStop = false;
		this.lbMainKind.Location = new System.Drawing.Point(95, 91);
		this.lbMainKind.Name = "lbMainKind";
		this.lbMainKind.Size = new System.Drawing.Size(189, 19);
		this.lbMainKind.TabIndex = 15;
		this.lbProjectName.Location = new System.Drawing.Point(95, 16);
		this.lbProjectName.Name = "lbProjectName";
		this.lbProjectName.Size = new System.Drawing.Size(189, 19);
		this.lbProjectName.TabIndex = 10;
		this.lbCostWithTax.Location = new System.Drawing.Point(466, 41);
		this.lbCostWithTax.Name = "lbCostWithTax";
		this.lbCostWithTax.Size = new System.Drawing.Size(189, 19);
		this.lbCostWithTax.TabIndex = 14;
		this.ultraLabel37.Location = new System.Drawing.Point(17, 91);
		this.ultraLabel37.Name = "ultraLabel37";
		this.ultraLabel37.Size = new System.Drawing.Size(80, 19);
		this.ultraLabel37.TabIndex = 2;
		this.ultraLabel37.Text = "主要分類:";
		this.lbCostWithoutTax.Location = new System.Drawing.Point(466, 16);
		this.lbCostWithoutTax.Name = "lbCostWithoutTax";
		this.lbCostWithoutTax.Size = new System.Drawing.Size(189, 19);
		this.lbCostWithoutTax.TabIndex = 13;
		this.ultraLabel38.Location = new System.Drawing.Point(17, 16);
		this.ultraLabel38.Name = "ultraLabel38";
		this.ultraLabel38.Size = new System.Drawing.Size(80, 19);
		this.ultraLabel38.TabIndex = 5;
		this.ultraLabel38.Text = "計畫名稱:";
		this.lbProjectAddress.Location = new System.Drawing.Point(95, 66);
		this.lbProjectAddress.Name = "lbProjectAddress";
		this.lbProjectAddress.Size = new System.Drawing.Size(189, 19);
		this.lbProjectAddress.TabIndex = 12;
		this.ultraLabel39.Location = new System.Drawing.Point(17, 41);
		this.ultraLabel39.Name = "ultraLabel39";
		this.ultraLabel39.Size = new System.Drawing.Size(80, 19);
		this.ultraLabel39.TabIndex = 6;
		this.ultraLabel39.Text = "機關名稱:";
		this.lbMainInstitute.Location = new System.Drawing.Point(95, 42);
		this.lbMainInstitute.Name = "lbMainInstitute";
		this.lbMainInstitute.Size = new System.Drawing.Size(189, 19);
		this.lbMainInstitute.TabIndex = 11;
		this.ultraLabel40.Location = new System.Drawing.Point(17, 66);
		this.ultraLabel40.Name = "ultraLabel40";
		this.ultraLabel40.Size = new System.Drawing.Size(80, 19);
		this.ultraLabel40.TabIndex = 7;
		this.ultraLabel40.Text = "基地位址:";
		this.ultraLabel41.Location = new System.Drawing.Point(320, 16);
		this.ultraLabel41.Name = "ultraLabel41";
		this.ultraLabel41.Size = new System.Drawing.Size(140, 19);
		this.ultraLabel41.TabIndex = 8;
		this.ultraLabel41.Text = "計畫總經費(未稅):";
		this.ultraLabel42.Location = new System.Drawing.Point(320, 42);
		this.ultraLabel42.Name = "ultraLabel42";
		this.ultraLabel42.Size = new System.Drawing.Size(140, 19);
		this.ultraLabel42.TabIndex = 9;
		this.ultraLabel42.Text = "計畫總經費(含稅):";
		this.pnSummary.Location = new System.Drawing.Point(3, 116);
		this.pnSummary.Name = "pnSummary";
		this.pnSummary.Size = new System.Drawing.Size(676, 391);
		this.pnSummary.TabIndex = 4;
		this.ultraTabPageControl3.Controls.Add(this.btnGenerateCatalog);
		this.ultraTabPageControl3.Controls.Add(this.btnDownloadDoc);
		this.ultraTabPageControl3.Controls.Add(this.gridDocumentFiles);
		this.ultraTabPageControl3.Controls.Add(this.btnUpload);
		this.ultraTabPageControl3.Controls.Add(this.btnDownload);
		this.ultraTabPageControl3.Controls.Add(this.ultraLabel43);
		this.ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabPageControl3.Name = "ultraTabPageControl3";
		this.ultraTabPageControl3.Size = new System.Drawing.Size(682, 547);
		this.btnGenerateCatalog.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance107.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnGenerateCatalog.Appearance = appearance107;
		this.btnGenerateCatalog.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.btnGenerateCatalog.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnGenerateCatalog.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance108.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance108.BackColor2 = System.Drawing.Color.White;
		appearance108.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.btnGenerateCatalog.HotTrackAppearance = appearance108;
		this.btnGenerateCatalog.HotTracking = true;
		this.btnGenerateCatalog.Location = new System.Drawing.Point(342, 453);
		this.btnGenerateCatalog.Name = "btnGenerateCatalog";
		this.btnGenerateCatalog.ShowFocusRect = false;
		this.btnGenerateCatalog.ShowOutline = false;
		this.btnGenerateCatalog.Size = new System.Drawing.Size(120, 33);
		this.btnGenerateCatalog.SupportThemes = false;
		this.btnGenerateCatalog.TabIndex = 53;
		this.btnGenerateCatalog.Text = "製作封面、目錄";
		this.btnGenerateCatalog.Click += new System.EventHandler(btnGenerateCatalog_Click);
		this.btnDownloadDoc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnDownloadDoc.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.btnDownloadDoc.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnDownloadDoc.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnDownloadDoc.HotTracking = true;
		this.btnDownloadDoc.Location = new System.Drawing.Point(177, 453);
		this.btnDownloadDoc.Name = "btnDownloadDoc";
		this.btnDownloadDoc.ShowFocusRect = false;
		this.btnDownloadDoc.ShowOutline = false;
		this.btnDownloadDoc.Size = new System.Drawing.Size(159, 33);
		this.btnDownloadDoc.SupportThemes = false;
		this.btnDownloadDoc.TabIndex = 53;
		this.btnDownloadDoc.Text = "下載施工補充說明書";
		this.btnDownloadDoc.Visible = false;
		this.gridDocumentFiles.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridDocumentFiles.ColumnInfo = resources.GetString("gridDocumentFiles.ColumnInfo");
		this.gridDocumentFiles.ForeColor = System.Drawing.SystemColors.WindowText;
		this.gridDocumentFiles.Location = new System.Drawing.Point(24, 50);
		this.gridDocumentFiles.Name = "gridDocumentFiles";
		this.gridDocumentFiles.Rows.Count = 1;
		this.gridDocumentFiles.Size = new System.Drawing.Size(642, 397);
		this.gridDocumentFiles.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridDocumentFiles.Styles"));
		this.gridDocumentFiles.TabIndex = 52;
		this.gridDocumentFiles.MouseDown += new System.Windows.Forms.MouseEventHandler(FilesGrid_MouseDown);
		this.gridDocumentFiles.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(FilesGrid_CellChanged);
		this.btnUpload.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance109.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnUpload.Appearance = appearance109;
		this.btnUpload.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.btnUpload.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnUpload.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance110.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance110.BackColor2 = System.Drawing.Color.White;
		appearance110.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.btnUpload.HotTrackAppearance = appearance110;
		this.btnUpload.HotTracking = true;
		this.btnUpload.Location = new System.Drawing.Point(570, 453);
		this.btnUpload.Name = "btnUpload";
		this.btnUpload.ShowFocusRect = false;
		this.btnUpload.ShowOutline = false;
		this.btnUpload.Size = new System.Drawing.Size(96, 33);
		this.btnUpload.SupportThemes = false;
		this.btnUpload.TabIndex = 51;
		this.btnUpload.Text = "上傳檔案";
		this.btnUpload.Click += new System.EventHandler(btnUpload_Click);
		this.btnDownload.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance111.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnDownload.Appearance = appearance111;
		this.btnDownload.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.btnDownload.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnDownload.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance112.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance112.BackColor2 = System.Drawing.Color.White;
		appearance112.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.btnDownload.HotTrackAppearance = appearance112;
		this.btnDownload.HotTracking = true;
		this.btnDownload.Location = new System.Drawing.Point(468, 453);
		this.btnDownload.Name = "btnDownload";
		this.btnDownload.ShowFocusRect = false;
		this.btnDownload.ShowOutline = false;
		this.btnDownload.Size = new System.Drawing.Size(96, 33);
		this.btnDownload.SupportThemes = false;
		this.btnDownload.TabIndex = 50;
		this.btnDownload.Text = "壓縮並下載";
		this.btnDownload.Click += new System.EventHandler(btnDownload_Click);
		this.ultraLabel43.Location = new System.Drawing.Point(24, 25);
		this.ultraLabel43.Name = "ultraLabel43";
		this.ultraLabel43.Size = new System.Drawing.Size(276, 19);
		this.ultraLabel43.TabIndex = 6;
		this.ultraLabel43.Text = "上傳招標文件檔案至PCCES之此專案下：";
		this.ultraTabPageControl5.Controls.Add(this.btnChangeNo);
		this.ultraTabPageControl5.Controls.Add(this.btnCompressionAndDownload);
		this.ultraTabPageControl5.Controls.Add(this.btnSpecificationAddFrontCover);
		this.ultraTabPageControl5.Controls.Add(this.btnSpecificationFrontCover);
		this.ultraTabPageControl5.Controls.Add(this.btnFrontCover);
		this.ultraTabPageControl5.Controls.Add(this.btnUploadFile);
		this.ultraTabPageControl5.Controls.Add(this.btnSaveFileList);
		this.ultraTabPageControl5.Controls.Add(this.btnDownloadFile);
		this.ultraTabPageControl5.Controls.Add(this.btnDownloadFileList);
		this.ultraTabPageControl5.Controls.Add(this.gridWraDocumentFiles);
		this.ultraTabPageControl5.Controls.Add(this.label10);
		this.ultraTabPageControl5.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabPageControl5.Name = "ultraTabPageControl5";
		this.ultraTabPageControl5.Size = new System.Drawing.Size(682, 547);
		this.btnChangeNo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance113.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnChangeNo.Appearance = appearance113;
		this.btnChangeNo.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.btnChangeNo.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnChangeNo.Enabled = false;
		this.btnChangeNo.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance114.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance114.BackColor2 = System.Drawing.Color.White;
		appearance114.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.btnChangeNo.HotTrackAppearance = appearance114;
		this.btnChangeNo.HotTracking = true;
		this.btnChangeNo.Location = new System.Drawing.Point(536, 403);
		this.btnChangeNo.Name = "btnChangeNo";
		this.btnChangeNo.ShowFocusRect = false;
		this.btnChangeNo.ShowOutline = false;
		this.btnChangeNo.Size = new System.Drawing.Size(124, 33);
		this.btnChangeNo.SupportThemes = false;
		this.btnChangeNo.TabIndex = 62;
		this.btnChangeNo.Text = "重新編號";
		this.btnChangeNo.Click += new System.EventHandler(btnChangeNo_Click);
		this.btnCompressionAndDownload.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance115.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnCompressionAndDownload.Appearance = appearance115;
		this.btnCompressionAndDownload.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.btnCompressionAndDownload.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnCompressionAndDownload.Enabled = false;
		this.btnCompressionAndDownload.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance116.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance116.BackColor2 = System.Drawing.Color.White;
		appearance116.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.btnCompressionAndDownload.HotTrackAppearance = appearance116;
		this.btnCompressionAndDownload.HotTracking = true;
		this.btnCompressionAndDownload.Location = new System.Drawing.Point(541, 457);
		this.btnCompressionAndDownload.Name = "btnCompressionAndDownload";
		this.btnCompressionAndDownload.ShowFocusRect = false;
		this.btnCompressionAndDownload.ShowOutline = false;
		this.btnCompressionAndDownload.Size = new System.Drawing.Size(119, 33);
		this.btnCompressionAndDownload.SupportThemes = false;
		this.btnCompressionAndDownload.TabIndex = 61;
		this.btnCompressionAndDownload.Text = "壓縮並下載";
		this.btnCompressionAndDownload.Click += new System.EventHandler(btnCompressionAndDownload_Click);
		this.btnSpecificationAddFrontCover.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance117.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnSpecificationAddFrontCover.Appearance = appearance117;
		this.btnSpecificationAddFrontCover.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.btnSpecificationAddFrontCover.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnSpecificationAddFrontCover.Enabled = false;
		this.btnSpecificationAddFrontCover.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance118.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance118.BackColor2 = System.Drawing.Color.White;
		appearance118.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.btnSpecificationAddFrontCover.HotTrackAppearance = appearance118;
		this.btnSpecificationAddFrontCover.HotTracking = true;
		this.btnSpecificationAddFrontCover.Location = new System.Drawing.Point(324, 457);
		this.btnSpecificationAddFrontCover.Name = "btnSpecificationAddFrontCover";
		this.btnSpecificationAddFrontCover.ShowFocusRect = false;
		this.btnSpecificationAddFrontCover.ShowOutline = false;
		this.btnSpecificationAddFrontCover.Size = new System.Drawing.Size(212, 33);
		this.btnSpecificationAddFrontCover.SupportThemes = false;
		this.btnSpecificationAddFrontCover.TabIndex = 60;
		this.btnSpecificationAddFrontCover.Text = "製作施工補充說明書封面目錄";
		this.btnSpecificationAddFrontCover.Click += new System.EventHandler(btnSpecificationAddFrontCover_Click);
		this.btnSpecificationFrontCover.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance119.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnSpecificationFrontCover.Appearance = appearance119;
		this.btnSpecificationFrontCover.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.btnSpecificationFrontCover.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnSpecificationFrontCover.Enabled = false;
		this.btnSpecificationFrontCover.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance120.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance120.BackColor2 = System.Drawing.Color.White;
		appearance120.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.btnSpecificationFrontCover.HotTrackAppearance = appearance120;
		this.btnSpecificationFrontCover.HotTracking = true;
		this.btnSpecificationFrontCover.Location = new System.Drawing.Point(147, 457);
		this.btnSpecificationFrontCover.Name = "btnSpecificationFrontCover";
		this.btnSpecificationFrontCover.ShowFocusRect = false;
		this.btnSpecificationFrontCover.ShowOutline = false;
		this.btnSpecificationFrontCover.Size = new System.Drawing.Size(171, 33);
		this.btnSpecificationFrontCover.SupportThemes = false;
		this.btnSpecificationFrontCover.TabIndex = 59;
		this.btnSpecificationFrontCover.Text = "製作施工規範封面目錄";
		this.btnSpecificationFrontCover.Click += new System.EventHandler(btnSpecificationFrontCover_Click);
		this.btnFrontCover.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance121.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnFrontCover.Appearance = appearance121;
		this.btnFrontCover.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.btnFrontCover.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnFrontCover.Enabled = false;
		this.btnFrontCover.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance122.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance122.BackColor2 = System.Drawing.Color.White;
		appearance122.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.btnFrontCover.HotTrackAppearance = appearance122;
		this.btnFrontCover.HotTracking = true;
		this.btnFrontCover.Location = new System.Drawing.Point(3, 457);
		this.btnFrontCover.Name = "btnFrontCover";
		this.btnFrontCover.ShowFocusRect = false;
		this.btnFrontCover.ShowOutline = false;
		this.btnFrontCover.Size = new System.Drawing.Size(138, 33);
		this.btnFrontCover.SupportThemes = false;
		this.btnFrontCover.TabIndex = 58;
		this.btnFrontCover.Text = "製作封面目錄";
		this.btnFrontCover.Click += new System.EventHandler(btnFrontCover_Click);
		this.btnUploadFile.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance123.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnUploadFile.Appearance = appearance123;
		this.btnUploadFile.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.btnUploadFile.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnUploadFile.Enabled = false;
		this.btnUploadFile.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance124.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance124.BackColor2 = System.Drawing.Color.White;
		appearance124.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.btnUploadFile.HotTrackAppearance = appearance124;
		this.btnUploadFile.HotTracking = true;
		this.btnUploadFile.Location = new System.Drawing.Point(276, 403);
		this.btnUploadFile.Name = "btnUploadFile";
		this.btnUploadFile.ShowFocusRect = false;
		this.btnUploadFile.ShowOutline = false;
		this.btnUploadFile.Size = new System.Drawing.Size(124, 33);
		this.btnUploadFile.SupportThemes = false;
		this.btnUploadFile.TabIndex = 57;
		this.btnUploadFile.Text = "上傳檔案";
		this.btnUploadFile.Click += new System.EventHandler(btnUploadFile_Click);
		this.btnSaveFileList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance125.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnSaveFileList.Appearance = appearance125;
		this.btnSaveFileList.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.btnSaveFileList.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnSaveFileList.Enabled = false;
		this.btnSaveFileList.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance126.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance126.BackColor2 = System.Drawing.Color.White;
		appearance126.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.btnSaveFileList.HotTrackAppearance = appearance126;
		this.btnSaveFileList.HotTracking = true;
		this.btnSaveFileList.Location = new System.Drawing.Point(406, 403);
		this.btnSaveFileList.Name = "btnSaveFileList";
		this.btnSaveFileList.ShowFocusRect = false;
		this.btnSaveFileList.ShowOutline = false;
		this.btnSaveFileList.Size = new System.Drawing.Size(124, 33);
		this.btnSaveFileList.SupportThemes = false;
		this.btnSaveFileList.TabIndex = 56;
		this.btnSaveFileList.Text = "儲存檔案";
		this.btnSaveFileList.Click += new System.EventHandler(btnSaveFileList_Click);
		this.btnDownloadFile.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance127.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnDownloadFile.Appearance = appearance127;
		this.btnDownloadFile.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.btnDownloadFile.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnDownloadFile.Enabled = false;
		this.btnDownloadFile.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance128.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance128.BackColor2 = System.Drawing.Color.White;
		appearance128.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.btnDownloadFile.HotTrackAppearance = appearance128;
		this.btnDownloadFile.HotTracking = true;
		this.btnDownloadFile.Location = new System.Drawing.Point(147, 403);
		this.btnDownloadFile.Name = "btnDownloadFile";
		this.btnDownloadFile.ShowFocusRect = false;
		this.btnDownloadFile.ShowOutline = false;
		this.btnDownloadFile.Size = new System.Drawing.Size(123, 33);
		this.btnDownloadFile.SupportThemes = false;
		this.btnDownloadFile.TabIndex = 55;
		this.btnDownloadFile.Text = "下載標準文件";
		this.btnDownloadFile.Click += new System.EventHandler(btnDownloadFile_Click);
		this.btnDownloadFileList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance129.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnDownloadFileList.Appearance = appearance129;
		this.btnDownloadFileList.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.btnDownloadFileList.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnDownloadFileList.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance130.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance130.BackColor2 = System.Drawing.Color.White;
		appearance130.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.btnDownloadFileList.HotTrackAppearance = appearance130;
		this.btnDownloadFileList.HotTracking = true;
		this.btnDownloadFileList.Location = new System.Drawing.Point(3, 403);
		this.btnDownloadFileList.Name = "btnDownloadFileList";
		this.btnDownloadFileList.ShowFocusRect = false;
		this.btnDownloadFileList.ShowOutline = false;
		this.btnDownloadFileList.Size = new System.Drawing.Size(138, 33);
		this.btnDownloadFileList.SupportThemes = false;
		this.btnDownloadFileList.TabIndex = 54;
		this.btnDownloadFileList.Text = "匯入標準文件列表";
		this.btnDownloadFileList.Click += new System.EventHandler(btnDownloadFileList_Click);
		this.gridWraDocumentFiles.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridWraDocumentFiles.ColumnInfo = resources.GetString("gridWraDocumentFiles.ColumnInfo");
		this.gridWraDocumentFiles.ForeColor = System.Drawing.SystemColors.WindowText;
		this.gridWraDocumentFiles.Location = new System.Drawing.Point(3, 53);
		this.gridWraDocumentFiles.Name = "gridWraDocumentFiles";
		this.gridWraDocumentFiles.Rows.Count = 1;
		this.gridWraDocumentFiles.Size = new System.Drawing.Size(676, 344);
		this.gridWraDocumentFiles.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridWraDocumentFiles.Styles"));
		this.gridWraDocumentFiles.TabIndex = 53;
		this.gridWraDocumentFiles.MouseDown += new System.Windows.Forms.MouseEventHandler(WRAFilesGrid_MouseDown);
		this.gridWraDocumentFiles.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(WRAFilesGrid_CellChanged);
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(21, 25);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(303, 15);
		this.label10.TabIndex = 0;
		this.label10.Text = "上傳施工規範文件檔案至PCCES此專案下：";
		this.groupBox3.Controls.Add(this.tbM2);
		this.groupBox3.Controls.Add(this.tbM3);
		this.groupBox3.Controls.Add(this.tbM4);
		this.groupBox3.Controls.Add(this.tbM7);
		this.groupBox3.Controls.Add(this.tbM6);
		this.groupBox3.Controls.Add(this.tbM8);
		this.groupBox3.Controls.Add(this.tbM5);
		this.groupBox3.Controls.Add(this.tbM1);
		this.groupBox3.Controls.Add(this.label9);
		this.groupBox3.Controls.Add(this.label8);
		this.groupBox3.Controls.Add(this.label7);
		this.groupBox3.Controls.Add(this.label6);
		this.groupBox3.Controls.Add(this.label5);
		this.groupBox3.Controls.Add(this.label4);
		this.groupBox3.Controls.Add(this.label3);
		this.groupBox3.Controls.Add(this.label2);
		this.groupBox3.Controls.Add(this.label1);
		this.groupBox3.Location = new System.Drawing.Point(12, 12);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(656, 456);
		this.groupBox3.TabIndex = 0;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "八大類資材統計資料";
		appearance131.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.tbM2.Appearance = appearance131;
		this.tbM2.AutoSize = true;
		this.tbM2.Location = new System.Drawing.Point(260, 128);
		this.tbM2.MaxLength = 10;
		this.tbM2.Name = "tbM2";
		this.tbM2.Size = new System.Drawing.Size(216, 21);
		this.tbM2.TabIndex = 18;
		this.tbM2.Validating += new System.ComponentModel.CancelEventHandler(inputNumber_Validating);
		appearance132.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.tbM3.Appearance = appearance132;
		this.tbM3.AutoSize = true;
		this.tbM3.Location = new System.Drawing.Point(260, 168);
		this.tbM3.MaxLength = 10;
		this.tbM3.Name = "tbM3";
		this.tbM3.Size = new System.Drawing.Size(216, 21);
		this.tbM3.TabIndex = 19;
		this.tbM3.Validating += new System.ComponentModel.CancelEventHandler(inputNumber_Validating);
		appearance133.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.tbM4.Appearance = appearance133;
		this.tbM4.AutoSize = true;
		this.tbM4.Location = new System.Drawing.Point(260, 212);
		this.tbM4.MaxLength = 10;
		this.tbM4.Name = "tbM4";
		this.tbM4.Size = new System.Drawing.Size(216, 21);
		this.tbM4.TabIndex = 20;
		this.tbM4.Validating += new System.ComponentModel.CancelEventHandler(inputNumber_Validating);
		appearance134.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.tbM7.Appearance = appearance134;
		this.tbM7.AutoSize = true;
		this.tbM7.Location = new System.Drawing.Point(260, 340);
		this.tbM7.MaxLength = 10;
		this.tbM7.Name = "tbM7";
		this.tbM7.Size = new System.Drawing.Size(216, 21);
		this.tbM7.TabIndex = 23;
		this.tbM7.Validating += new System.ComponentModel.CancelEventHandler(inputNumber_Validating);
		appearance135.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.tbM6.Appearance = appearance135;
		this.tbM6.AutoSize = true;
		this.tbM6.Location = new System.Drawing.Point(260, 300);
		this.tbM6.MaxLength = 10;
		this.tbM6.Name = "tbM6";
		this.tbM6.Size = new System.Drawing.Size(216, 21);
		this.tbM6.TabIndex = 22;
		this.tbM6.Validating += new System.ComponentModel.CancelEventHandler(inputNumber_Validating);
		appearance136.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.tbM8.Appearance = appearance136;
		this.tbM8.AutoSize = true;
		this.tbM8.Location = new System.Drawing.Point(260, 380);
		this.tbM8.MaxLength = 10;
		this.tbM8.Name = "tbM8";
		this.tbM8.Size = new System.Drawing.Size(216, 21);
		this.tbM8.TabIndex = 24;
		this.tbM8.Validating += new System.ComponentModel.CancelEventHandler(inputNumber_Validating);
		appearance137.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.tbM5.Appearance = appearance137;
		this.tbM5.AutoSize = true;
		this.tbM5.Location = new System.Drawing.Point(260, 256);
		this.tbM5.MaxLength = 10;
		this.tbM5.Name = "tbM5";
		this.tbM5.Size = new System.Drawing.Size(216, 21);
		this.tbM5.TabIndex = 21;
		this.tbM5.Validating += new System.ComponentModel.CancelEventHandler(inputNumber_Validating);
		appearance138.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.tbM1.Appearance = appearance138;
		this.tbM1.AutoSize = true;
		this.tbM1.Location = new System.Drawing.Point(260, 88);
		this.tbM1.MaxLength = 10;
		this.tbM1.Name = "tbM1";
		this.tbM1.Size = new System.Drawing.Size(216, 21);
		this.tbM1.TabIndex = 17;
		this.tbM1.Validating += new System.ComponentModel.CancelEventHandler(inputNumber_Validating);
		this.label9.Location = new System.Drawing.Point(40, 216);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(176, 23);
		this.label9.TabIndex = 8;
		this.label9.Text = "M03052 水泥(T)：";
		this.label8.Location = new System.Drawing.Point(40, 260);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(160, 23);
		this.label8.TabIndex = 7;
		this.label8.Text = "M023192 級配(M3)：";
		this.label7.Location = new System.Drawing.Point(40, 172);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(200, 23);
		this.label7.TabIndex = 6;
		this.label7.Text = "M02742 瀝青混凝土(M3)：";
		this.label6.Location = new System.Drawing.Point(40, 304);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(152, 23);
		this.label6.TabIndex = 5;
		this.label6.Text = "M04061 砂(M3)：";
		this.label5.Location = new System.Drawing.Point(40, 384);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(168, 23);
		this.label5.TabIndex = 4;
		this.label5.Text = "M06124 型鋼(T)：";
		this.label4.Location = new System.Drawing.Point(40, 132);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(200, 23);
		this.label4.TabIndex = 3;
		this.label4.Text = "M033101 機拌混凝土(M3)：";
		this.label3.Location = new System.Drawing.Point(40, 344);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(160, 23);
		this.label3.TabIndex = 2;
		this.label3.Text = "M03210 鋼筋(T)：";
		this.label2.Location = new System.Drawing.Point(40, 92);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(204, 23);
		this.label2.TabIndex = 1;
		this.label2.Text = "M033102 預拌混凝土(M3)：";
		this.label1.Location = new System.Drawing.Point(24, 40);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(244, 23);
		this.label1.TabIndex = 0;
		this.label1.Text = "請完成以下各項資材統計數據：";
		this.panel1.Controls.Add(this.lbChineseProjectName);
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(702, 32);
		this.panel1.TabIndex = 0;
		appearance139.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbChineseProjectName.Appearance = appearance139;
		this.lbChineseProjectName.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbChineseProjectName.Location = new System.Drawing.Point(72, 6);
		this.lbChineseProjectName.Name = "lbChineseProjectName";
		this.lbChineseProjectName.Size = new System.Drawing.Size(604, 23);
		this.lbChineseProjectName.TabIndex = 1;
		this.lbChineseProjectName.Text = "[lblProjectCName1]";
		appearance140.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance140;
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(8, 6);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(60, 23);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "專案: ";
		this.panel2.AutoSize = true;
		this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panel2.Controls.Add(this.btnCancel);
		this.panel2.Controls.Add(this.btnOK);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 625);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(702, 38);
		this.panel2.TabIndex = 1;
		appearance141.Image = resources.GetObject("appearance141.Image");
		appearance141.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnCancel.Appearance = appearance141;
		this.btnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance142.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance142.BackColor2 = System.Drawing.Color.White;
		appearance142.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.btnCancel.HotTrackAppearance = appearance142;
		this.btnCancel.HotTracking = true;
		this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btnCancel.Location = new System.Drawing.Point(612, 4);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.ShowFocusRect = false;
		this.btnCancel.ShowOutline = false;
		this.btnCancel.Size = new System.Drawing.Size(80, 31);
		this.btnCancel.SupportThemes = false;
		this.btnCancel.TabIndex = 13;
		this.btnCancel.Text = "取消";
		appearance143.Image = resources.GetObject("appearance143.Image");
		appearance143.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnOK.Appearance = appearance143;
		this.btnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOK.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance144.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance144.BackColor2 = System.Drawing.Color.White;
		appearance144.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.btnOK.HotTrackAppearance = appearance144;
		this.btnOK.HotTracking = true;
		this.btnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.btnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.btnOK.Location = new System.Drawing.Point(528, 4);
		this.btnOK.Name = "btnOK";
		this.btnOK.ShowFocusRect = false;
		this.btnOK.ShowOutline = false;
		this.btnOK.Size = new System.Drawing.Size(80, 31);
		this.btnOK.SupportThemes = false;
		this.btnOK.TabIndex = 12;
		this.btnOK.Text = "確定";
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.panel3.AutoScroll = true;
		this.panel3.Controls.Add(this.Tab_ProjInfo);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 32);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(702, 593);
		this.panel3.TabIndex = 2;
		appearance145.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance145.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance145.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance145.ForeColor = System.Drawing.Color.White;
		appearance145.TextVAlign = Infragistics.Win.VAlign.Top;
		this.Tab_ProjInfo.ActiveTabAppearance = appearance145;
		this.Tab_ProjInfo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance146.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance146.BackColor2 = System.Drawing.Color.FromArgb(102, 153, 255);
		appearance146.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		this.Tab_ProjInfo.Appearance = appearance146;
		appearance147.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance147.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.Tab_ProjInfo.ClientAreaAppearance = appearance147;
		this.Tab_ProjInfo.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_ProjInfo.Controls.Add(this.Tab_Basic);
		this.Tab_ProjInfo.Controls.Add(this.Tab_Other);
		this.Tab_ProjInfo.Controls.Add(this.ultraTabPageControl2);
		this.Tab_ProjInfo.Controls.Add(this.ultraTabPageControl3);
		this.Tab_ProjInfo.Controls.Add(this.ultraTabPageControl5);
		this.Tab_ProjInfo.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Tab_ProjInfo.Location = new System.Drawing.Point(8, 8);
		this.Tab_ProjInfo.Name = "Tab_ProjInfo";
		this.Tab_ProjInfo.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_ProjInfo.Size = new System.Drawing.Size(686, 578);
		this.Tab_ProjInfo.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
		this.Tab_ProjInfo.TabIndex = 0;
		this.Tab_ProjInfo.TabPadding = new System.Drawing.Size(1, 3);
		ultraTab6.Key = "Tab_Basic";
		ultraTab6.TabPage = this.Tab_Basic;
		ultraTab6.Text = "專案基本資訊";
		ultraTab7.Key = "Tab_Other";
		ultraTab7.TabPage = this.Tab_Other;
		ultraTab7.Text = "其他資訊";
		ultraTab8.TabPage = this.ultraTabPageControl2;
		ultraTab8.Text = "成果概要";
		ultraTab9.TabPage = this.ultraTabPageControl3;
		ultraTab9.Text = "招標文件";
		ultraTab10.TabPage = this.ultraTabPageControl5;
		ultraTab10.Text = "預算書文件";
		ultraTab10.Visible = false;
		this.Tab_ProjInfo.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[5] { ultraTab6, ultraTab7, ultraTab8, ultraTab9, ultraTab10 });
		this.Tab_ProjInfo.SelectedTabChanged += new Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventHandler(Tab_ProjInfo_SelectedTabChanged);
		this.ultraTabSharedControlsPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(682, 547);
		this.openUploadFileDialog.Filter = "所有檔案|*.*";
		this.openUploadFileDialog.Title = "請選擇上傳的檔案";
		this.saveDownloadFileDialog.Filter = "zip 壓縮檔|*.zip";
		this.saveDownloadFileDialog.Title = "儲存檔案";
		this.doubleClickTimer.Tick += new System.EventHandler(doubleClickTimer_Tick);
		this.WRAdoubleClickTimer.Tick += new System.EventHandler(WRAdoubleClickTimer_Tick);
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(702, 663);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.MinimizeBox = false;
		base.Name = "FormBudgetProjectInfo";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "預算資訊";
		base.Load += new System.EventHandler(FormBudgetProjectInfo_Load);
		base.Activated += new System.EventHandler(FormBudgetProjectInfo_Activated);
		this.SubTab1.ResumeLayout(false);
		this.panel13.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridCostKind).EndInit();
		this.SubTab2.ResumeLayout(false);
		this.PNL_LOWER_1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1Sizer1).EndInit();
		this.c1Sizer1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtMemo1_1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo1_2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo1_3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo1_4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo1_5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo1_6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo1_7).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo1_8).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo1_9).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo1_10).EndInit();
		this.SubTab3.ResumeLayout(false);
		this.PNL_LOWER_2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1Sizer2).EndInit();
		this.c1Sizer2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtMemo2_1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo2_2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo2_3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo2_4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo2_5).EndInit();
		this.SubTab4.ResumeLayout(false);
		this.PNL_LOWER_3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1Sizer3).EndInit();
		this.c1Sizer3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtMemo3_1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo3_2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo3_3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo3_4).EndInit();
		this.ultraTabPageControl4.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.tbProjectDescription).EndInit();
		this.Tab_Basic.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraTabControl1).EndInit();
		this.ultraTabControl1.ResumeLayout(false);
		this.panel6.ResumeLayout(false);
		this.panel6.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtWeightedConfirmRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtConfirmRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtWeightedCorrectRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCorrectRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ddlExpectDuration).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ddlDurationType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ddlBudType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbProjectAddress).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ddlProjectCity).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ddlProjectArea).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExpectFinishDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbProjectScope).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExpectStartDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbMainInstituite).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbBudEndYear).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbWorkUnit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbBudStartYear).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbWorkMode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbAccountCodeUpper).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbExpectDuration).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbAccountCodeLower).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbBuyMode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbEnglishProjectName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbChineseProjectName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbProjectCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbMainInstituteCode).EndInit();
		this.Tab_Other.ResumeLayout(false);
		this.gbGreenItem.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.tbGreenTotalRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbGreenEnergyRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbGreenMaterialRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbGreenMethodRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbGreenEnvRatio).EndInit();
		this.gbTendererInfo.ResumeLayout(false);
		this.panel14.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.tbOwner).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridSublet).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbVendorName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbVendorInvoiceNo).EndInit();
		this.gbBudget.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Combo5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Combo4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Combo3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Combo2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Combo1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txt7).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txt6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txt5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txt4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txt3).EndInit();
		this.gbConstructionType.ResumeLayout(false);
		this.gbConstructionType.PerformLayout();
		this.panel12.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ddlProjectClassification).EndInit();
		this.ultraTabPageControl2.ResumeLayout(false);
		this.groupBox4.ResumeLayout(false);
		this.ultraTabPageControl3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridDocumentFiles).EndInit();
		this.ultraTabPageControl5.ResumeLayout(false);
		this.ultraTabPageControl5.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gridWraDocumentFiles).EndInit();
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.tbM2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbM3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbM4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbM7).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbM6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbM8).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbM5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbM1).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tab_ProjInfo).EndInit();
		this.Tab_ProjInfo.ResumeLayout(false);
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

	public FormBudgetProjectInfo()
	{
		InitializeComponent();
		F_FormStatus = FormStatus.Active;
	}

	private void FormBudgetProjectInfo_Load(object sender, EventArgs e)
	{
		btnGenerateCatalog.Visible = ArchConvert.Obj2Bool(ConfigurationManager.AppSettings["DownloadDocCustomizedMode"]);
		gridDocumentFiles.ContextMenu = new ContextMenu(new MenuItem[1]
		{
			new MenuItem("刪除檔案", FilesGrid_Delete)
		});
		gridWraDocumentFiles.ContextMenu = new ContextMenu(new MenuItem[2]
		{
			new MenuItem("刪除", WRAFilesGrid_Delete),
			new MenuItem("更改章節名稱", WRAFilesGrid_ChangeChapterName)
		});
		CorrectRatio();
		LoadingData();
		ClearTextControls();
		FillData();
		if (FormActionName == PccesFormAction.BID)
		{
			Text = "標單資訊";
			Tab_ProjInfo.Tabs[2].Visible = false;
			Tab_ProjInfo.Tabs[3].Visible = false;
			gbBudget.Visible = false;
			gbTendererInfo.Location = new Point(8, 70);
			gbConstructionType.Location = new Point(8, 250);
			lbMainUnitRequired.Visible = false;
			lbCityRequired.Visible = false;
			lbProjectClassificationRequired.Visible = false;
			gbGreenItem.Visible = false;
			txtCorrectRate.Visible = false;
			txtWeightedCorrectRate.Visible = false;
			txtConfirmRate.Visible = false;
			txtWeightedConfirmRate.Visible = false;
			ultraLabel44.Visible = false;
			ultraLabel45.Visible = false;
			ultraLabel46.Visible = false;
			ultraLabel47.Visible = false;
			tbProjectCode.Enabled = false;
			tbChineseProjectName.Enabled = false;
			btnPickMainInstitute.Enabled = false;
		}
		else if (FormActionName == PccesFormAction.BUD)
		{
			gbTendererInfo.Visible = false;
			gbConstructionType.Location = new Point(8, 20);
			gbBudget.Location = new Point(8, 240);
			LoadGreenItemSetting();
		}
		Tab_ProjInfo.Tabs[4].Visible = false;
		if (!PubTools.GetAppSet_Bool("WaterResourcesAgency"))
		{
			return;
		}
		Tab_ProjInfo.Tabs[4].Visible = true;
		SysUser oSysUser = new SysUser();
		string CurrentDBName = oSysUser.GetSysUserDatabaseName(UserID);
		string AddOnPath = AppDomain.CurrentDomain.BaseDirectory + "WRAAddOn\\" + CurrentDBName + "\\" + ProjectCode;
		if (!File.Exists(AddOnPath + "\\List.txt"))
		{
			return;
		}
		DirectoryInfo directory = new DirectoryInfo(AddOnPath);
		if (directory.GetFiles().Length > 3)
		{
			if (File.Exists(AddOnPath + "\\List.txt"))
			{
				using StreamReader sr = new StreamReader(AddOnPath + "\\List.txt", Encoding.GetEncoding("Big5"));
				int rowindex = 1;
				char[] splitChars = new char[2] { '.', ',' };
				string line;
				while ((line = sr.ReadLine()) != null && line != "")
				{
					gridWraDocumentFiles.Rows.Count = gridWraDocumentFiles.Rows.Count + 1;
					string[] words = line.Split(splitChars);
					gridWraDocumentFiles.Rows[rowindex]["ChapNo"] = words[0];
					gridWraDocumentFiles.Rows[rowindex]["ChapName"] = words[1];
					gridWraDocumentFiles.Rows[rowindex]["StdDocName"] = words[2];
					gridWraDocumentFiles.Rows[rowindex]["StdFileNameExtend"] = "." + words[3];
					gridWraDocumentFiles.Rows[rowindex]["ProjectDocName"] = words[4];
					gridWraDocumentFiles.Rows[rowindex]["ProjectFilenameExtend"] = "." + words[5];
					gridWraDocumentFiles.Rows[rowindex]["AutoNo"] = words[6];
					rowindex++;
				}
			}
		}
		else
		{
			using StreamReader sr = new StreamReader(AddOnPath + "\\List.txt", Encoding.GetEncoding("Big5"));
			int rowindex = 1;
			char[] splitChars = new char[2] { '.', ',' };
			string line;
			while ((line = sr.ReadLine()) != null && line != "")
			{
				gridWraDocumentFiles.Rows.Count = gridWraDocumentFiles.Rows.Count + 1;
				string[] words = line.Split(splitChars);
				gridWraDocumentFiles.Rows[rowindex]["ChapNo"] = words[0];
				gridWraDocumentFiles.Rows[rowindex]["ChapName"] = words[1];
				gridWraDocumentFiles.Rows[rowindex]["StdDocName"] = words[2];
				gridWraDocumentFiles.Rows[rowindex]["StdFileNameExtend"] = "." + words[3];
				rowindex++;
			}
		}
		btnDownloadFile.Enabled = true;
		btnUploadFile.Enabled = true;
		btnDownloadFile.Enabled = true;
		btnChangeNo.Enabled = true;
		btnSaveFileList.Enabled = true;
		btnFrontCover.Enabled = true;
		btnSpecificationFrontCover.Enabled = true;
		btnSpecificationAddFrontCover.Enabled = true;
		btnCompressionAndDownload.Enabled = true;
	}

	private void LoadGreenItemSetting()
	{
		string AppLocation = AppDomain.CurrentDomain.BaseDirectory;
		string FileIni = "OptionSet.ini";
		string greenEnv = CommonMethods.IniReadValue(AppLocation + FileIni, "CommonData", "GreenEnv");
		string greenMethod = CommonMethods.IniReadValue(AppLocation + FileIni, "CommonData", "GreenMethod");
		string greenMaterial = CommonMethods.IniReadValue(AppLocation + FileIni, "CommonData", "GreenMaterial");
		string greenEnergy = CommonMethods.IniReadValue(AppLocation + FileIni, "CommonData", "GreenEnergy");
		lbGreenEnvRatio.Text = ((greenEnv == string.Empty) ? "綠色環境佔比：" : (greenEnv + "佔比："));
		lbGreenMethodRatio.Text = ((greenMethod == string.Empty) ? "綠色工法佔比：" : (greenMethod + "佔比："));
		lbGreenMaterialRatio.Text = ((greenMaterial == string.Empty) ? "綠色材料佔比：" : (greenMaterial + "佔比："));
		lbGreenEnergyRatio.Text = ((greenEnergy == string.Empty) ? "綠色能源佔比：" : (greenEnergy + "佔比："));
	}

	private void FormBudgetProjectInfo_Activated(object sender, EventArgs e)
	{
		if (alertCount > 0 || F_FormStatus != FormStatus.Active)
		{
			return;
		}
		try
		{
			if (OpenMode == BudgetInfoForm_OpenMode.NewBudget)
			{
				if (FormActionName == PccesFormAction.BUD)
				{
					alertCount++;
					MessageBox.Show(this, "此專案尚未建立預算書資料，請先填寫預算書基本資料。\n\n完成後按[確定]存檔 \n或是[取消]放棄填寫 ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					alertCount++;
					MessageBox.Show(this, "此專案尚未建立標單資料，請先填寫標單基本資料。\n\n完成後按[確定]存檔 \n或是[取消]放棄填寫 ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				string AppLocation = AppDomain.CurrentDomain.BaseDirectory;
				string FileINI = AppLocation + "OptionSet.ini";
				string S_DEPT_ID = CommonMethods.IniReadValue(FileINI, "CommonData", "DEPT_ID");
				if (S_DEPT_ID.Trim().Length > 1)
				{
					ArrayList aArr = new ArrayList();
					aArr.Clear();
					aArr.Add(UserID);
					aArr.Add("專案資訊--主辦單位載入");
					MainUnitCom MN_UNT_CM = new MainUnitCom(aArr);
					DataTable DT_MainCode = null;
					DT_MainCode = MN_UNT_CM.ListItem(" MainCode ='" + S_DEPT_ID.Trim() + "' ");
					if (DT_MainCode.Rows.Count > 0)
					{
						tbMainInstituteCode.Text = DT_MainCode.Rows[0]["mainCode"].ToString().Trim();
						tbMainInstituite.Text = DT_MainCode.Rows[0]["mainName"].ToString().Trim();
					}
				}
				btnRenameGreenRatio.Enabled = false;
			}
			if (CommonMethods.IniReadValue(AppDomain.CurrentDomain.BaseDirectory + "OptionSet.ini", "CommonData", "ShowGreenOptions").ToUpper() == "TRUE")
			{
				gbGreenItem.Visible = true;
			}
			else
			{
				gbGreenItem.Visible = false;
			}
		}
		finally
		{
			F_FormStatus = FormStatus.Normal;
		}
		F_FormStatus = FormStatus.Normal;
		BringToFront();
	}

	private void CorrectRatio()
	{
		double ratio = CommonMethods.GetWindowRatio(base.Handle);
		if (ratio == 1.0)
		{
			return;
		}
		foreach (Control Cn in c1Sizer1.Controls)
		{
			Cn.Font = new Font(Cn.Font.Name, (float)((double)Cn.Font.Size * ratio));
		}
		foreach (Control Cn in c1Sizer2.Controls)
		{
			Cn.Font = new Font(Cn.Font.Name, (float)((double)Cn.Font.Size * ratio));
		}
		foreach (Control Cn in c1Sizer3.Controls)
		{
			Cn.Font = new Font(Cn.Font.Name, (float)((double)Cn.Font.Size * ratio));
		}
	}

	private void LoadingData()
	{
		if (FormActionName == PccesFormAction.BUD)
		{
			project = new BudProject();
			costKind = new BudCostKind();
			annexe = new BudAnnexe();
		}
		else if (FormActionName == PccesFormAction.BID)
		{
			project = new BidProject();
			costKind = new BidCostKind();
			annexe = new BidAnnexe();
		}
		subMemo = new SubMemo();
		sublet = new Archnowledge.Pcces.DomainModule.General.Sublet();
		pubProject = new Archnowledge.Pcces.DomainModule.General.PubProject();
		dsProject = project.GetProject(ProjectCode);
		dsCostKind = costKind.GetCostKind(ProjectCode);
		dsAnnexe = annexe.GetAnnexe(ProjectCode);
		dsSubMemo = subMemo.GetSubMemo(ProjectCode);
	}

	private void ClearTextControls()
	{
		foreach (Control txt in panel6.Controls)
		{
			if (txt is UltraTextEditor)
			{
				((UltraTextEditor)txt).Text = "";
			}
		}
		foreach (Control txt in Tab_Other.Controls)
		{
			if (txt is UltraTextEditor)
			{
				((UltraTextEditor)txt).Text = "";
			}
		}
		foreach (Control txt in gbBudget.Controls)
		{
			if (txt is UltraTextEditor)
			{
				((UltraTextEditor)txt).Text = "";
			}
		}
	}

	private void FillData()
	{
		if (OpenMode == BudgetInfoForm_OpenMode.NewBudget)
		{
			lbChineseProjectName.Text = ChineseProjectName.Trim();
			tbProjectCode.Text = ProjectCode.Trim();
			tbChineseProjectName.Text = ChineseProjectName.Trim();
			tbEnglishProjectName.Text = EnglishProjectName.Trim();
			tbProjectAddress.Text = ProjectAddress.Trim();
			ddlProjectCity.Text = ProjectCity.Trim();
			lblMainProjectCode.Text = "";
			ddlDurationType.SelectedIndex = 0;
			tbM1.Text = "";
			tbM2.Text = "";
			tbM3.Text = "";
			tbM4.Text = "";
			tbM5.Text = "";
			tbM6.Text = "";
			tbM7.Text = "";
			tbM8.Text = "";
			ddlDurationType.SelectedIndex = 0;
			ddlExpectDuration.Visible = false;
			tbExpectDuration.Visible = true;
		}
		else
		{
			ddlDurationType.SelectedIndex = 0;
			if (dsProject.Tables[0].Rows.Count > 0)
			{
				DataRow drProject = dsProject.Tables[0].Rows[0];
				lbChineseProjectName.Text = drProject["projectNameC"].ToString().Trim();
				tbProjectCode.Text = drProject["projectCode"].ToString().Trim();
				tbChineseProjectName.Text = drProject["projectNameC"].ToString().Trim();
				tbEnglishProjectName.Text = drProject["projectNameE"].ToString().Trim();
				ProjectCity = drProject["city"].ToString().Trim();
				if (ProjectCity != string.Empty)
				{
					ddlProjectArea.SelectedIndex = getAreaIndex(ProjectCity);
					ddlProjectCity.SelectedIndex = getProjectAddressIndex(ProjectCity);
				}
				tbProjectAddress.Text = drProject["projectAddress"].ToString().Trim();
				tbMainInstituteCode.Text = drProject["mainCode"].ToString().Trim();
				tbMainInstituite.Text = drProject["mainCName"].ToString().Trim();
				if (tbMainInstituite.Text.Trim() == string.Empty && tbMainInstituteCode.Text.Trim().Length > 0)
				{
					MainUnit theMainUnit = new MainUnit();
					DataSet dsMainUnit = theMainUnit.GetMainUnit(tbMainInstituteCode.Text.Trim());
					if (dsMainUnit.Tables[0].Rows.Count == 1)
					{
						tbMainInstituite.Text = dsMainUnit.Tables[0].Rows[0]["mainName"].ToString();
						drProject["mainCName"] = tbMainInstituite.Text;
						project.GetDatasetUpdate(dsProject);
					}
				}
				tbAccountCodeLower.Text = drProject["accountCode1"].ToString().Trim();
				tbAccountCodeUpper.Text = drProject["accountCode2"].ToString().Trim();
				tbBuyMode.Text = drProject["buyMode"].ToString().Trim();
				tbWorkMode.Text = drProject["workMode"].ToString().Trim();
				tbBudStartYear.Text = drProject["budStartYear"].ToString().Trim();
				tbBudEndYear.Text = drProject["budEndYear"].ToString().Trim();
				txtExpectStartDate.Text = FormatExpectDate(drProject["expectStartDate"].ToString());
				txtExpectFinishDate.Text = FormatExpectDate(drProject["expectFinishDate"].ToString());
				tbProjectScope.Text = drProject["projectScope"].ToString().Trim();
				originalProjectScope = tbProjectScope.Text;
				tbWorkUnit.Text = drProject["workUnit"].ToString().Trim();
				lblMainProjectCode.Text = drProject["mainProj"].ToString().Trim();
				tbProjectDescription.Text = drProject["projectDescription"].ToString().Trim();
				if (FormActionName == PccesFormAction.BUD)
				{
					txtCorrectRate.Text = drProject["correctRate"].ToString();
					txtWeightedCorrectRate.Text = drProject["weightedCorrectRate"].ToString();
					txtConfirmRate.Text = drProject["confirmRate"].ToString();
					txtWeightedConfirmRate.Text = drProject["weightedConfirmRate"].ToString();
					BudProject budProject = new BudProject();
					decimal greenEnvRatio = budProject.GetProjectGreenRatio(_ProjectCode, "IsGreenItem") * 100m;
					decimal greenMethodRatio = budProject.GetProjectGreenRatio(_ProjectCode, "IsGreenMethod") * 100m;
					decimal greenMaterialRatio = budProject.GetProjectGreenRatio(_ProjectCode, "IsGreenMaterial") * 100m;
					decimal greenEnergyRatio = budProject.GetProjectGreenRatio(_ProjectCode, "IsGreenEnergy") * 100m;
					decimal greenTotalRatio = budProject.GetProjectGreenRatio(_ProjectCode, string.Empty) * 100m;
					tbGreenEnvRatio.Text = ArchConvert.Obj2String(Math.Round(greenEnvRatio, 2, MidpointRounding.AwayFromZero)) + "%";
					tbGreenMethodRatio.Text = ArchConvert.Obj2String(Math.Round(greenMethodRatio, 2, MidpointRounding.AwayFromZero)) + "%";
					tbGreenMaterialRatio.Text = ArchConvert.Obj2String(Math.Round(greenMaterialRatio, 2, MidpointRounding.AwayFromZero)) + "%";
					tbGreenEnergyRatio.Text = ArchConvert.Obj2String(Math.Round(greenEnergyRatio, 2, MidpointRounding.AwayFromZero)) + "%";
					tbGreenTotalRatio.Text = ArchConvert.Obj2String(Math.Round(greenTotalRatio, 2, MidpointRounding.AwayFromZero)) + "%";
				}
				tbM1.Text = drProject["eightM1"].ToString().Trim();
				tbM2.Text = drProject["eightM2"].ToString().Trim();
				tbM3.Text = drProject["eightM3"].ToString().Trim();
				tbM4.Text = drProject["eightM4"].ToString().Trim();
				tbM5.Text = drProject["eightM5"].ToString().Trim();
				tbM6.Text = drProject["eightM6"].ToString().Trim();
				tbM7.Text = drProject["eightM7"].ToString().Trim();
				tbM8.Text = drProject["eightM8"].ToString().Trim();
				int budgetType = ArchConvert.Obj2Int(drProject["IsType"].ToString().Trim());
				if (drProject["IsType"].ToString().Trim() == string.Empty)
				{
					budgetType = 1;
				}
				ddlBudType.SelectedIndex = budgetType - 1;
			}
			if (SysConfig.SysComsEnable)
			{
				lbBudType.Visible = true;
				ddlBudType.Visible = true;
			}
			else
			{
				lbBudType.Visible = false;
				ddlBudType.Visible = false;
			}
			BindToGridCostKind();
			FillAnnexe();
			SysUser oSysUser = new SysUser();
			string DBName = oSysUser.GetSysUserDatabaseName(UserID);
			documentsFilePath = AppDomain.CurrentDomain.BaseDirectory + (AppDomain.CurrentDomain.BaseDirectory.EndsWith("\\") ? "" : "\\") + "AddOn\\" + DBName + "\\" + ProjectCode + "\\";
			BindToFilesGrid();
			if (FormActionName == PccesFormAction.BID)
			{
				panel6.Enabled = true;
				PNL_LOWER_1.Enabled = false;
				PNL_LOWER_2.Enabled = false;
				PNL_LOWER_3.Enabled = false;
				gbConstructionType.Enabled = false;
				gbBudget.Enabled = false;
				groupBox3.Enabled = false;
				tbExpectDuration.Enabled = false;
				ddlDurationType.Enabled = false;
				ddlProjectArea.Enabled = false;
				chkStage1.Enabled = false;
				chkStage2.Enabled = false;
				chkStage3.Enabled = false;
				chkStage4.Enabled = false;
				chkStage5.Enabled = false;
				chkStage6.Enabled = false;
				lbBudType.Visible = false;
				ddlBudType.Visible = false;
				tbProjectDescription.ReadOnly = true;
				tbMainInstituteCode.Enabled = false;
				tbMainInstituite.Enabled = false;
				ddlProjectArea.Enabled = false;
				ddlProjectCity.Enabled = false;
				tbProjectAddress.Enabled = false;
				tbBuyMode.Enabled = false;
				tbAccountCodeLower.Enabled = false;
				tbAccountCodeUpper.Enabled = false;
				tbExpectDuration.Enabled = false;
				btnEditGPSLocation.Enabled = false;
				tbProjectScope.Enabled = false;
				tbWorkMode.Enabled = false;
				tbBudStartYear.Enabled = false;
				tbBudEndYear.Enabled = false;
				txtExpectStartDate.Enabled = false;
				txtExpectFinishDate.Enabled = false;
				tbWorkUnit.Enabled = false;
			}
		}
		FillOtherInformation();
		switch (tabShowedWhenLoad)
		{
		case 1:
			Tab_Basic.Tab.Selected = true;
			break;
		case 2:
			break;
		case 3:
			Tab_Other.Tab.Selected = true;
			break;
		}
	}

	private string FormatExpectDate(string date)
	{
		date = date.Trim();
		if (date.Length >= 8)
		{
			string year = date.Substring(0, 4);
			string month = date.Substring(4, 2);
			string day = date.Substring(6, 2);
			return year + "/" + month + "/" + day;
		}
		return string.Empty;
	}

	private void BindToGridCostKind()
	{
		DataTable dtCostKind = dsCostKind.Tables[0];
		gridCostKind.Rows.Count = dtCostKind.Rows.Count + 1;
		for (int i = 0; i < dtCostKind.Rows.Count; i++)
		{
			gridCostKind[i + 1, "kind"] = dtCostKind.Rows[i]["kind"].ToString();
			gridCostKind[i + 1, "cost"] = dtCostKind.Rows[i]["cost"].ToString();
			gridCostKind[i + 1, "memo"] = dtCostKind.Rows[i]["memo"].ToString();
		}
	}

	private void FillAnnexe()
	{
		FillInAnnexeTextBox(c1Sizer1, "1", 10);
		FillInAnnexeTextBox(c1Sizer2, "2", 5);
		FillInAnnexeTextBox(c1Sizer3, "3", 4);
	}

	private void FillInAnnexeTextBox(C1Sizer c1Sizer, string kind, int count)
	{
		DataView dvAnnexe = new DataView(dsAnnexe.Tables[0]);
		dvAnnexe.RowFilter = "kind = '" + kind + "'";
		if (dvAnnexe.Count >= count)
		{
			for (int i = 0; i < count; i++)
			{
				Control textBox = c1Sizer.Controls.Find("txtMemo" + kind.ToString() + "_" + (i + 1), searchAllChildren: false)[0];
				textBox.Text = dvAnnexe[i]["memo"].ToString();
				textBox.Tag = dvAnnexe[i]["itemNo"].ToString();
			}
			dvAnnexe.Dispose();
			dvAnnexe = null;
		}
	}

	private void FillOtherInformation()
	{
		if (dsSubMemo.Tables[0].Rows.Count == 0)
		{
			DataRow row = dsSubMemo.Tables[0].NewRow();
			dsSubMemo.Tables[0].Rows.Add(row);
			return;
		}
		DataRow drSubMemo = dsSubMemo.Tables[0].Rows[0];
		string expectDuration = drSubMemo["expectdaily"].ToString().Trim();
		if (expectDuration.Length > 8)
		{
			ddlDurationType.SelectedIndex = ArchConvert.Obj2Int(expectDuration.Substring(8, 1));
			if (ddlDurationType.SelectedIndex == 2)
			{
				ddlExpectDuration.Value = PubTools.Str2DateTime(expectDuration.Substring(0, 8));
				ddlExpectDuration.Top = tbExpectDuration.Top;
				ddlExpectDuration.Visible = true;
				tbExpectDuration.Visible = false;
			}
			else
			{
				tbExpectDuration.Text = expectDuration.Substring(0, 8).Trim();
				ddlExpectDuration.Visible = false;
				tbExpectDuration.Visible = true;
			}
		}
		else
		{
			ddlDurationType.SelectedIndex = 0;
			ddlExpectDuration.Visible = false;
			tbExpectDuration.Visible = true;
		}
		tbVendorInvoiceNo.Text = drSubMemo["factory_id"].ToString().Trim();
		FillVendorInfo(tbVendorInvoiceNo.Text);
		if (drSubMemo["item1_no"].ToString().Trim() != string.Empty)
		{
			ddlProjectClassification.SelectedIndex = ArchConvert.Obj2Int(drSubMemo["item1_no"]) - 1;
		}
		string ls_item2 = drSubMemo["item2_no"].ToString().PadRight(23, '0');
		chk01.Checked = ls_item2[0] == '1';
		chk02.Checked = ls_item2[1] == '1';
		chk03.Checked = ls_item2[2] == '1';
		chk04.Checked = ls_item2[3] == '1';
		chk05.Checked = ls_item2[4] == '1';
		chk06.Checked = ls_item2[5] == '1';
		chk07.Checked = ls_item2[6] == '1';
		chk08.Checked = ls_item2[7] == '1';
		chk09.Checked = ls_item2[8] == '1';
		chk10.Checked = ls_item2[9] == '1';
		chk11.Checked = ls_item2[10] == '1';
		chk12.Checked = ls_item2[11] == '1';
		chk13.Checked = ls_item2[12] == '1';
		chk14.Checked = ls_item2[13] == '1';
		chk15.Checked = ls_item2[14] == '1';
		chk16.Checked = ls_item2[15] == '1';
		chk17.Checked = ls_item2[16] == '1';
		chk18.Checked = ls_item2[17] == '1';
		chk19.Checked = ls_item2[18] == '1';
		chk20.Checked = ls_item2[19] == '1';
		chk21.Checked = ls_item2[20] == '1';
		chk22.Checked = ls_item2[21] == '1';
		txt3.Text = drSubMemo["PFOJ_UPR1"].ToString();
		txt4.Text = drSubMemo["PFOJ_UPR2"].ToString();
		txt5.Text = drSubMemo["PFOJ_UPR3"].ToString();
		txt6.Text = drSubMemo["PFOJ_UPR4"].ToString();
		txt7.Text = drSubMemo["PFOJ_UPR5"].ToString();
		Combo1.Text = drSubMemo["DATE_UPR1"].ToString();
		Combo2.Text = drSubMemo["DATE_UPR2"].ToString();
		Combo3.Text = drSubMemo["DATE_UPR3"].ToString();
		Combo4.Text = drSubMemo["DATE_UPR4"].ToString();
		Combo5.Text = drSubMemo["DATE_UPR5"].ToString();
		string projectStage = ArchConvert.Obj2String(drSubMemo["PROJ_PROPERTY"]).PadRight(6, '0');
		chkStage1.Checked = GetBooleanFromNumber(projectStage[0]);
		chkStage2.Checked = GetBooleanFromNumber(projectStage[1]);
		chkStage3.Checked = GetBooleanFromNumber(projectStage[2]);
		chkStage4.Checked = GetBooleanFromNumber(projectStage[3]);
		chkStage5.Checked = GetBooleanFromNumber(projectStage[4]);
		chkStage6.Checked = GetBooleanFromNumber(projectStage[5]);
	}

	private bool GetBooleanFromNumber(char number)
	{
		return number == '1';
	}

	private void FillVendorInfo(string InvoiceNo)
	{
		string companyName = string.Empty;
		DataTable dtSublet = sublet.GetSublet(InvoiceNo).Tables[0];
		if (dtSublet.Rows.Count > 0)
		{
			tbVendorName.Text = dtSublet.Rows[0]["title"].ToString().Trim();
			tbOwner.Text = dtSublet.Rows[0]["boss"].ToString().Trim();
		}
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		if (IsMainInstituteCodeFilled() && IsProjectCityFilled() && (FormActionName != PccesFormAction.BUD || IsProjectClassificationFilled()))
		{
			checkVerdorInvoiceNo();
			if (FormActionName == PccesFormAction.BID && tbVendorInvoiceNo.Text.Trim() == string.Empty)
			{
				MessageBox.Show(this, "注意：\n此一標單尚未給定投標商資訊！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			if (originalProjectScope != tbProjectScope.Text)
			{
				projectScopeChanged = true;
			}
			WriteToDatabase();
			base.DialogResult = DialogResult.OK;
		}
	}

	private void WriteToDatabase()
	{
		UpdateProject();
		UpdatePubProject();
		UpdateAnnexe();
		UpdateSubMemo();
		if (ddlProjectClassification.Value != null)
		{
			WriteInfoSummaryToXML();
		}
	}

	private void checkVerdorInvoiceNo()
	{
		if (tbVendorInvoiceNo.Text.Trim() != string.Empty)
		{
			DataSet dsSublet = sublet.GetSublet(tbVendorInvoiceNo.Text);
			DataRow drSublet;
			if (dsSublet.Tables[0].Rows.Count == 0)
			{
				drSublet = dsSublet.Tables[0].NewRow();
				dsSublet.Tables[0].Rows.Add(drSublet);
			}
			else
			{
				drSublet = dsSublet.Tables[0].Rows[0];
			}
			drSublet["invoice_no"] = tbVendorInvoiceNo.Text.Trim();
			drSublet["title"] = tbVendorName.Text.Trim();
			drSublet["boss"] = tbOwner.Text.Trim();
			sublet.UpdateSublet(dsSublet);
		}
	}

	private void UpdateProject()
	{
		DataRow drProject;
		if (OpenMode == BudgetInfoForm_OpenMode.NewBudget)
		{
			drProject = dsProject.Tables[0].NewRow();
			dsProject.Tables[0].Rows.Add(drProject);
			drProject["ReCalType"] = "1";
			drProject["enableCustomizedVariable"] = "1";
		}
		else
		{
			drProject = dsProject.Tables[0].Rows[0];
		}
		dsPubProject = pubProject.GetPubProjectByProjectCode(ProjectCode);
		if (dsPubProject.Tables[0].Rows.Count > 0)
		{
			drProject["projectCodeAlias"] = dsPubProject.Tables[0].Rows[0]["projectCodeAlias"];
		}
		drProject["projectCode"] = ProjectCode;
		drProject["mainCode"] = tbMainInstituteCode.Text;
		drProject["projectNameC"] = tbChineseProjectName.Text;
		drProject["projectNameE"] = tbEnglishProjectName.Text;
		drProject["projectAddress"] = tbProjectAddress.Text;
		drProject["city"] = ddlProjectCity.Text;
		drProject["accountCode1"] = tbAccountCodeLower.Text;
		drProject["accountCode2"] = tbAccountCodeUpper.Text;
		drProject["budStartYear"] = tbBudStartYear.Text;
		drProject["budEndYear"] = tbBudEndYear.Text;
		drProject["buyMode"] = tbBuyMode.Text;
		drProject["workMode"] = tbWorkMode.Text;
		drProject["expectStartDate"] = $"{txtExpectStartDate.Value:yyyyMMdd}";
		drProject["expectFinishDate"] = $"{txtExpectFinishDate.Value:yyyyMMdd}";
		drProject["projectScope"] = ArchConvert.Obj2Double(tbProjectScope.Text);
		drProject["workUnit"] = tbWorkUnit.Text;
		drProject["mainProj"] = lblMainProjectCode.Text;
		drProject["mainCName"] = tbMainInstituite.Text;
		drProject["UseIR"] = "1";
		drProject["projectDescription"] = tbProjectDescription.Text;
		drProject["eightM1"] = returnDBNullIfEmpty(tbM1.Text);
		drProject["eightM2"] = returnDBNullIfEmpty(tbM2.Text);
		drProject["eightM3"] = returnDBNullIfEmpty(tbM3.Text);
		drProject["eightM4"] = returnDBNullIfEmpty(tbM4.Text);
		drProject["eightM5"] = returnDBNullIfEmpty(tbM5.Text);
		drProject["eightM6"] = returnDBNullIfEmpty(tbM6.Text);
		drProject["eightM7"] = returnDBNullIfEmpty(tbM7.Text);
		drProject["eightM8"] = returnDBNullIfEmpty(tbM8.Text);
		if (SysConfig.SysComsEnable && FormActionName == PccesFormAction.BUD)
		{
			drProject["IsType"] = ((ddlBudType.SelectedIndex == -1) ? 1 : (ddlBudType.SelectedIndex + 1));
		}
		project.GetDatasetUpdate(dsProject);
	}

	private object returnDBNullIfEmpty(string value)
	{
		if (value == string.Empty)
		{
			return DBNull.Value;
		}
		return ArchConvert.Obj2Double(value);
	}

	private void UpdatePubProject()
	{
		DataRow drPubProject = dsPubProject.Tables[0].Rows[0];
		drPubProject["projectCode"] = tbProjectCode.Text;
		drPubProject["projCName"] = tbChineseProjectName.Text;
		drPubProject["projEName"] = tbEnglishProjectName.Text;
		drPubProject["projAddress"] = tbProjectAddress.Text;
		drPubProject["city"] = ddlProjectCity.Text;
		pubProject.UpdatePubProject(dsPubProject);
	}

	private void UpdateAnnexe()
	{
		WriteAnnexeToDataSet(c1Sizer1);
		WriteAnnexeToDataSet(c1Sizer2);
		WriteAnnexeToDataSet(c1Sizer3);
		annexe.UpdateAnnexe(dsAnnexe);
	}

	private void WriteAnnexeToDataSet(C1Sizer c1Sizer)
	{
		foreach (Control textBox in c1Sizer.Controls)
		{
			if (textBox.Name.StartsWith("txtMemo"))
			{
				if (textBox.Tag != null)
				{
					DataView dvAnnexe = new DataView(dsAnnexe.Tables[0]);
					dvAnnexe.RowFilter = "itemNo = " + textBox.Tag.ToString();
					dvAnnexe[0]["memo"] = textBox.Text;
				}
				else
				{
					DataRow drAnnexe = dsAnnexe.Tables[0].NewRow();
					drAnnexe["projectCode"] = ProjectCode;
					drAnnexe["kind"] = textBox.Name[7].ToString();
					drAnnexe["memo"] = textBox.Text;
					dsAnnexe.Tables[0].Rows.Add(drAnnexe);
				}
			}
		}
	}

	private void UpdateSubMemo()
	{
		DataRow drSubMemo = dsSubMemo.Tables[0].Rows[0];
		drSubMemo["PROJECTCODE"] = ProjectCode;
		drSubMemo["SPROJ"] = " ";
		drSubMemo["FACTORY_ID"] = tbVendorInvoiceNo.Text.Trim();
		drSubMemo["ITEM1_NO"] = ddlProjectClassification.Value;
		drSubMemo["loc_no"] = ddlProjectArea.Value;
		drSubMemo["PROJ_PROPERTY"] = AssembleProjectStage();
		drSubMemo["PFOJ_UPR1"] = txt3.Text;
		drSubMemo["PFOJ_UPR2"] = txt4.Text;
		drSubMemo["PFOJ_UPR3"] = txt5.Text;
		drSubMemo["PFOJ_UPR4"] = txt6.Text;
		drSubMemo["PFOJ_UPR5"] = txt7.Text;
		drSubMemo["DATE_UPR1"] = Combo1.Value;
		drSubMemo["DATE_UPR2"] = Combo2.Value;
		drSubMemo["DATE_UPR3"] = Combo3.Value;
		drSubMemo["DATE_UPR4"] = Combo4.Value;
		drSubMemo["DATE_UPR5"] = Combo5.Value;
		if (ddlDurationType.SelectedIndex == 2)
		{
			drSubMemo["EXPECTDAILY"] = $"{ddlExpectDuration.Value:yyyyMMdd}" + ddlDurationType.Value.ToString();
		}
		else
		{
			drSubMemo["EXPECTDAILY"] = tbExpectDuration.Text.Trim().PadRight(8, ' ') + ddlDurationType.Value.ToString();
		}
		string subClassification = string.Empty;
		subClassification += (chk01.Checked ? "1" : "0");
		subClassification += (chk02.Checked ? "1" : "0");
		subClassification += (chk03.Checked ? "1" : "0");
		subClassification += (chk04.Checked ? "1" : "0");
		subClassification += (chk05.Checked ? "1" : "0");
		subClassification += (chk06.Checked ? "1" : "0");
		subClassification += (chk07.Checked ? "1" : "0");
		subClassification += (chk08.Checked ? "1" : "0");
		subClassification += (chk09.Checked ? "1" : "0");
		subClassification += (chk10.Checked ? "1" : "0");
		subClassification += (chk11.Checked ? "1" : "0");
		subClassification += (chk12.Checked ? "1" : "0");
		subClassification += (chk13.Checked ? "1" : "0");
		subClassification += (chk14.Checked ? "1" : "0");
		subClassification += (chk15.Checked ? "1" : "0");
		subClassification += (chk16.Checked ? "1" : "0");
		subClassification += (chk17.Checked ? "1" : "0");
		subClassification += (chk18.Checked ? "1" : "0");
		subClassification += (chk19.Checked ? "1" : "0");
		subClassification += (chk20.Checked ? "1" : "0");
		subClassification += (chk21.Checked ? "1" : "0");
		subClassification += (chk22.Checked ? "1" : "0");
		drSubMemo["ITEM2_NO"] = subClassification;
		subMemo.UpdateSubMemo(dsSubMemo);
	}

	private string AssembleProjectStage()
	{
		return GetNumberFromBoolean(chkStage1.Checked) + GetNumberFromBoolean(chkStage2.Checked) + GetNumberFromBoolean(chkStage3.Checked) + GetNumberFromBoolean(chkStage4.Checked) + GetNumberFromBoolean(chkStage5.Checked) + GetNumberFromBoolean(chkStage6.Checked);
	}

	private string GetNumberFromBoolean(bool boolean)
	{
		return boolean ? "1" : "0";
	}

	private bool IsMainInstituteCodeFilled()
	{
		if (tbMainInstituteCode.Text.Trim() == "")
		{
			MessageBox.Show(this, "請選擇主辦單位！", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbMainInstituite.Focus();
			return false;
		}
		return true;
	}

	private bool IsProjectCityFilled()
	{
		if (ddlProjectCity.Text == "")
		{
			Tab_ProjInfo.SelectedTab = Tab_ProjInfo.Tabs["Tab_Basic"];
			MessageBox.Show(this, "請選擇【所在區域】及【工程所在縣市】", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			ddlProjectArea.Focus();
			return false;
		}
		return true;
	}

	private bool IsProjectClassificationFilled()
	{
		if (ddlProjectClassification.Text == null)
		{
			Tab_ProjInfo.SelectedTab = Tab_ProjInfo.Tabs["Tab_Other"];
			MessageBox.Show(this, "請選擇【主要工程分類】", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			ddlProjectClassification.Focus();
			return false;
		}
		return true;
	}

	private void btnPickMainInstitute_Click(object sender, EventArgs e)
	{
		FormBudgetDept_Pick FM_BDGT_DEPT_PK = new FormBudgetDept_Pick();
		FM_BDGT_DEPT_PK._UserID = UserID;
		FM_BDGT_DEPT_PK._OwnerName = "ProjectInfo";
		if (FM_BDGT_DEPT_PK.ShowDialog(this) == DialogResult.OK)
		{
			tbMainInstituteCode.Text = MainInstituteCode;
			tbMainInstituite.Text = MainInstituteName;
		}
		FM_BDGT_DEPT_PK.Close();
		FM_BDGT_DEPT_PK.Dispose();
		FM_BDGT_DEPT_PK = null;
	}

	private void ddlDurationType_AfterCloseUp(object sender, EventArgs e)
	{
		if (ddlDurationType.SelectedIndex == 2)
		{
			tbExpectDuration.Visible = false;
			ddlExpectDuration.Top = tbExpectDuration.Top;
			ddlExpectDuration.Visible = true;
		}
		else
		{
			tbExpectDuration.Visible = true;
			ddlExpectDuration.Visible = false;
		}
	}

	private void inputText_Validating(object sender, CancelEventArgs e)
	{
		if (!CommonMethods.CheckValidString((sender as UltraTextEditor).Text))
		{
			e.Cancel = true;
		}
		if (!CommonMethods.IsStrByteLenValid(tbBuyMode.Text, 10))
		{
			MessageBox.Show(this, "發包方式的長度不可超過 10 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbProjectCode.Focus();
		}
		else if (!CommonMethods.IsStrByteLenValid(tbWorkMode.Text, 10))
		{
			MessageBox.Show(this, "施工方式的長度不可超過 10 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbWorkMode.Focus();
		}
		else if (!CommonMethods.IsStrByteLenValid(tbAccountCodeLower.Text, 20))
		{
			MessageBox.Show(this, "會計科目的長度不可超過 20 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbAccountCodeLower.Focus();
		}
		else if (!CommonMethods.IsStrByteLenValid(tbAccountCodeUpper.Text, 20))
		{
			MessageBox.Show(this, "會計科目的長度不可超過 20 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbAccountCodeUpper.Focus();
		}
		else if (!CommonMethods.IsStrByteLenValid(tbBudStartYear.Text, 4))
		{
			MessageBox.Show(this, "預算年度的長度不可超過 4 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbBudStartYear.Focus();
		}
		else if (!CommonMethods.IsStrByteLenValid(tbBudEndYear.Text, 4))
		{
			MessageBox.Show(this, "預算年度的長度不可超過 4 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbBudEndYear.Focus();
		}
		else if (!CommonMethods.IsStrByteLenValid(tbWorkUnit.Text, 20))
		{
			MessageBox.Show(this, "工程單位的長度不可超過 20 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbWorkUnit.Focus();
		}
		else if (!CommonMethods.IsStrByteLenValid(tbVendorInvoiceNo.Text, 10))
		{
			MessageBox.Show(this, "投標廠商統編的長度不可超過 10 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbVendorInvoiceNo.Focus();
		}
		else if (!CommonMethods.IsStrByteLenValid(tbVendorName.Text, 60))
		{
			MessageBox.Show(this, "投標廠商名稱的長度不可超過 60 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbVendorName.Focus();
		}
	}

	private void inputNumber_Validating(object sender, CancelEventArgs e)
	{
		UltraTextEditor textEditor = (UltraTextEditor)sender;
		if (!(textEditor.Text.Trim() == string.Empty) && !double.TryParse(textEditor.Text, out var _))
		{
			MessageBox.Show(this, "請輸入數字！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			textEditor.Focus();
		}
	}

	private void btnPickInvoiceNo_Click(object sender, EventArgs e)
	{
		DataTable dtSublet = sublet.GetAllSubletForProjectInfo().Tables[0];
		gridSublet.DataSource = dtSublet;
		gridSublet.DataBind();
		gridSublet.DisplayLayout.Bands[0].Override.HeaderClickAction = HeaderClickAction.SortSingle;
		gridSublet.DisplayLayout.Bands[0].Columns[0].Header.Caption = "統一編號";
		gridSublet.DisplayLayout.Bands[0].Columns[1].Header.Caption = "公司名稱";
		gridSublet.DisplayLayout.Bands[0].Columns[2].Header.Caption = "負責人";
		gridSublet.DisplayLayout.Bands[0].Columns[3].Header.Caption = "負責人身份證字號";
		gridSublet.DisplayLayout.Bands[0].Columns[0].Width = 75;
		gridSublet.DisplayLayout.Bands[0].Columns[1].Width = 155;
		gridSublet.DisplayLayout.Bands[0].Columns[2].Width = 65;
		gridSublet.DisplayLayout.Bands[0].Columns[3].Width = 110;
		gridSublet.ToggleDropdown();
	}

	private void gridSublet_AfterCloseUp(object sender, EventArgs e)
	{
		if (gridSublet.Text != string.Empty)
		{
			tbVendorInvoiceNo.Text = gridSublet.Text.Trim();
			tbVendorName.Text = gridSublet.SelectedRow.Cells[1].Text.Trim();
			tbOwner.Text = gridSublet.SelectedRow.Cells[2].Text.Trim();
		}
	}

	private void btnEditGPSLocation_Click(object sender, EventArgs e)
	{
		FormBDGT_ItemSetGPS FM_ITMSET_GPS = new FormBDGT_ItemSetGPS();
		FM_ITMSET_GPS._ProjectCode = ProjectCode;
		FM_ITMSET_GPS.ShowDialog(this);
		FM_ITMSET_GPS.Close();
		FM_ITMSET_GPS.Dispose();
		FM_ITMSET_GPS = null;
	}

	private void ddlProjectArea_ValueChanged(object sender, EventArgs e)
	{
		ddlProjectCity.Items.Clear();
		string Area = ddlProjectArea.Text;
		List<string> Cities = NorternCity;
		switch (Area)
		{
		case "北":
			Cities = NorternCity;
			break;
		case "中":
			Cities = CentralCity;
			break;
		case "南":
			Cities = SouthernCity;
			break;
		case "東":
			Cities = EasternCity;
			break;
		case "離島":
			Cities = OffshoreCity;
			break;
		}
		for (int index = 0; index < Cities.Count; index++)
		{
			ddlProjectCity.Items.Add(index.ToString(), Cities[index]);
		}
		if (ddlProjectCity.SelectedIndex == -1)
		{
			ddlProjectCity.SelectedIndex = 0;
		}
	}

	private int getProjectAddressIndex(string ProjectCity)
	{
		List<string>[] cityAndCounty = CityAndCounty;
		foreach (List<string> Cities in cityAndCounty)
		{
			if (Cities.Contains(ProjectCity))
			{
				return Cities.IndexOf(ProjectCity);
			}
		}
		return 0;
	}

	private int getAreaIndex(string ProjectCity)
	{
		for (int index = 0; index < CityAndCounty.Length; index++)
		{
			if (CityAndCounty[index].Contains(ProjectCity))
			{
				return index;
			}
		}
		return 0;
	}

	private void inputText_Leave(object sender, EventArgs e)
	{
		string inputText = (sender as UltraTextEditor).Text;
		if (inputText.Contains("#"))
		{
			MessageBox.Show(this, "不可輸入【#】，請重新輸入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			(sender as UltraTextEditor).Focus();
		}
	}

	private void ddlProjectCity_ValueChanged(object sender, EventArgs e)
	{
		string projectAddress = tbProjectAddress.Text;
		if (projectAddress != null)
		{
			if (!projectAddress.StartsWith(ddlProjectCity.Text))
			{
				tbProjectAddress.Text = ddlProjectCity.Text.Trim();
			}
		}
		else
		{
			tbProjectAddress.Text = ddlProjectCity.Text;
		}
	}

	private void CboMainKind_ValueChanged(object sender, EventArgs e)
	{
		SummaryControlBase controlBase = null;
		bool isExist = false;
		switch (ddlProjectClassification.SelectedItem.DataValue.ToString())
		{
		case "01":
			controlBase = IsSummaryControlExist("Archnowledge.Pcces.PccesMain.ArchControls.ProjectInfoSummaryControls.Construction", out isExist);
			if (controlBase == null)
			{
				controlBase = new Construction(this);
			}
			break;
		case "02":
			controlBase = IsSummaryControlExist("Archnowledge.Pcces.PccesMain.ArchControls.ProjectInfoSummaryControls.Tunnel", out isExist);
			if (controlBase == null)
			{
				controlBase = new Tunnel(this);
			}
			break;
		case "05":
			controlBase = IsSummaryControlExist("Archnowledge.Pcces.PccesMain.ArchControls.ProjectInfoSummaryControls.Highway", out isExist);
			if (controlBase == null)
			{
				controlBase = new Highway(this);
			}
			break;
		case "06":
			controlBase = IsSummaryControlExist("Archnowledge.Pcces.PccesMain.ArchControls.ProjectInfoSummaryControls.Bridge", out isExist);
			if (controlBase == null)
			{
				controlBase = new Bridge(this);
			}
			break;
		}
		BudItemA budItemA = new BudItemA();
		double TotalAmount = budItemA.GetItemAAmount(ProjectCode, 0);
		double Tax = budItemA.GetTax(ProjectCode);
		lbCostWithTax.Text = $"{TotalAmount:N0}";
		lbCostWithoutTax.Text = $"{TotalAmount - Tax:N0}";
		if (controlBase != null)
		{
			if (!isExist)
			{
				controlBase.Name = "SummaryControl";
				pnSummary.Controls.Clear();
				pnSummary.Controls.Add(controlBase);
				controlBase.Dock = DockStyle.Fill;
			}
			controlBase.SetProjectCode(ProjectCode);
			controlBase.F_Amount = TotalAmount;
			controlBase.SetXML();
			controlBase.F_UserID = UserID;
			controlBase.Visible = true;
			lbProjectScopeUnit.Visible = true;
		}
		else
		{
			pnSummary.Controls.Clear();
			lbProjectScopeUnit.Visible = false;
		}
	}

	private SummaryControlBase IsSummaryControlExist(string ControlTypeName, out bool isExist)
	{
		isExist = false;
		SummaryControlBase retSummaryControl = null;
		foreach (Control theControl in pnSummary.Controls)
		{
			if (theControl.GetType().ToString() == ControlTypeName)
			{
				isExist = true;
				retSummaryControl = theControl as SummaryControlBase;
			}
			theControl.Visible = false;
		}
		return retSummaryControl;
	}

	private void WriteInfoSummaryToXML()
	{
		SummaryControlBase controlBase = (SummaryControlBase)pnSummary.Controls["SummaryControl"];
		SubMemo subMemo = new SubMemo();
		if (controlBase != null)
		{
			controlBase.GetXML();
			if (controlBase.IsRequiredFilled())
			{
				subMemo.UpdateIsResultSummaryRequiredFilled(ProjectCode, IsResultSummaryRequiredFilled: true);
			}
			else
			{
				subMemo.UpdateIsResultSummaryRequiredFilled(ProjectCode, IsResultSummaryRequiredFilled: false);
			}
			return;
		}
		DataSet dsSubMemo = subMemo.GetSubMemo(ProjectCode);
		if (dsSubMemo.Tables[0].Rows.Count > 0)
		{
			dsSubMemo.Tables[0].Rows[0]["ResultSummary"] = null;
			dsSubMemo.Tables[0].Rows[0]["isResultSummaryRequiredFilled"] = 1;
		}
		subMemo.UpdateSubMemo(dsSubMemo);
	}

	private void Tab_ProjInfo_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
	{
		if (e.Tab.Index == 3)
		{
			lbProjectName.Text = tbChineseProjectName.Text;
			lbMainInstitute.Text = tbMainInstituite.Text;
			lbProjectAddress.Text = tbProjectAddress.Text;
			lbMainKind.Text = ddlProjectClassification.Text;
		}
	}

	private void BindToFilesGrid()
	{
		gridDocumentFiles.Rows.RemoveRange(1, gridDocumentFiles.Rows.Count - 1);
		if (Directory.Exists(documentsFilePath))
		{
			DirectoryInfo directory = new DirectoryInfo(documentsFilePath);
			int ID = 1;
			FileInfo[] files = directory.GetFiles();
			foreach (FileInfo file in files)
			{
				gridDocumentFiles.AddItem(new object[5]
				{
					null,
					ID++,
					Path.GetFileNameWithoutExtension(file.Name),
					Path.GetExtension(file.Name),
					Path.GetFileNameWithoutExtension(file.Name)
				});
			}
		}
	}

	private void btnUpload_Click(object sender, EventArgs e)
	{
		if (openUploadFileDialog.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		if (!Directory.Exists(documentsFilePath))
		{
			Directory.CreateDirectory(documentsFilePath);
		}
		if (File.Exists(documentsFilePath + Path.GetFileName(openUploadFileDialog.FileName)))
		{
			if (MessageBox.Show("檔案已存在，是否覆蓋？", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			{
				return;
			}
			try
			{
				File.Copy(openUploadFileDialog.FileName, documentsFilePath + Path.GetFileName(openUploadFileDialog.FileName), overwrite: true);
				MessageBox.Show("上傳完成！");
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			finally
			{
				BindToFilesGrid();
			}
		}
		File.Copy(openUploadFileDialog.FileName, documentsFilePath + Path.GetFileName(openUploadFileDialog.FileName));
		MessageBox.Show("上傳完成！");
		BindToFilesGrid();
	}

	private void btnDownload_Click(object sender, EventArgs e)
	{
		ZipUtility ZipUtil = new ZipUtility();
		if (saveDownloadFileDialog.ShowDialog() == DialogResult.OK)
		{
			DirectoryInfo directory = new DirectoryInfo(documentsFilePath);
			string[] Files = new string[directory.GetFiles().Length];
			int index = 0;
			FileInfo[] files = directory.GetFiles();
			foreach (FileInfo file in files)
			{
				Files[index++] = file.FullName;
			}
			ZipUtil.ZipFile(Files, saveDownloadFileDialog.FileName);
			MessageBox.Show("下載完成");
		}
	}

	private void FilesGrid_CellChanged(object sender, RowColEventArgs e)
	{
		if (e.Col != 2 || gridDocumentFiles.Rows[e.Row]["OFileName"] == null)
		{
			return;
		}
		string SourceFileName = gridDocumentFiles.Rows[e.Row]["OFileName"].ToString();
		string TargetFileName = gridDocumentFiles.Rows[e.Row]["FileName"].ToString();
		string FileExtension = gridDocumentFiles.Rows[e.Row]["FileExtension"].ToString();
		if (TargetFileName != SourceFileName && File.Exists(documentsFilePath + SourceFileName + FileExtension))
		{
			try
			{
				File.Move(documentsFilePath + SourceFileName + FileExtension, documentsFilePath + TargetFileName + FileExtension);
				gridDocumentFiles.Rows[e.Row]["OFileName"] = gridDocumentFiles.Rows[e.Row]["FileName"];
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
		}
		MessageBox.Show("找不到檔案！請確認 " + documentsFilePath + SourceFileName + FileExtension + " 檔案存在。");
		BindToFilesGrid();
	}

	private void FilesGrid_Delete(object sender, EventArgs e)
	{
		string fileName = gridDocumentFiles.Rows[gridDocumentFiles.Row][2].ToString() + gridDocumentFiles.Rows[gridDocumentFiles.Row][3].ToString();
		DialogResult dialogResult = MessageBox.Show("你確定要刪除 " + fileName + " ？", "確認刪除檔案", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
		AddOnDownLoad addOnDownload = new AddOnDownLoad();
		if (dialogResult != DialogResult.OK)
		{
			return;
		}
		try
		{
			File.Delete(documentsFilePath + fileName);
			addOnDownload.DeleteAddOnDownloadByFileName(ProjectCode, fileName);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
		finally
		{
			BindToFilesGrid();
		}
	}

	private void FilesGrid_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			if (isFirstClick)
			{
				isFirstClick = false;
				doubleClickTimer.Start();
			}
			else if (milliseconds < SystemInformation.DoubleClickTime)
			{
				isDoubleClick = true;
			}
		}
		else if (e.Button == MouseButtons.Right)
		{
			int rowIndex = gridDocumentFiles.MouseRow;
			int colIndex = gridDocumentFiles.MouseCol;
			if (colIndex >= gridDocumentFiles.Cols.Fixed && rowIndex >= gridDocumentFiles.Rows.Fixed)
			{
				gridDocumentFiles.Col = colIndex;
				gridDocumentFiles.Row = rowIndex;
			}
		}
	}

	private void doubleClickTimer_Tick(object sender, EventArgs e)
	{
		milliseconds += 100;
		if (milliseconds < SystemInformation.DoubleClickTime)
		{
			return;
		}
		doubleClickTimer.Stop();
		if (!isDoubleClick && gridDocumentFiles.MouseRow != -1)
		{
			string FileName = gridDocumentFiles.Rows[gridDocumentFiles.MouseRow][2].ToString() + gridDocumentFiles.Rows[gridDocumentFiles.MouseRow][3].ToString();
			try
			{
				Process.Start(documentsFilePath + FileName);
			}
			catch (Exception)
			{
				Process process = new Process();
				string filecommand = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\shell32.dll,OpenAs_RunDLL " + documentsFilePath + FileName;
				ProcessStartInfo processStartInfo = new ProcessStartInfo("rundll32.exe", filecommand);
				process.StartInfo = processStartInfo;
				process.Start();
			}
		}
		isFirstClick = true;
		isDoubleClick = false;
		milliseconds = 0;
	}

	public void UpdateProjectScopeValue(string Value)
	{
		tbProjectScope.Text = Value;
	}

	public void UpdateProjectScopeUnit(string Value)
	{
		lbProjectScopeUnit.Text = Value;
	}

	private void btnGenerateCatalog_Click(object sender, EventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		ProcessStartInfo catalogGenerater = new ProcessStartInfo();
		catalogGenerater.FileName = "CatalogGenerator.exe";
		catalogGenerater.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
		catalogGenerater.WindowStyle = ProcessWindowStyle.Hidden;
		catalogGenerater.Arguments = $"\"{documentsFilePath}#_#{tbProjectCode.Text}#_#{lbChineseProjectName.Text}#_#{tbWorkUnit.Text}\"";
		try
		{
			Process newProcess = Process.Start(catalogGenerater);
			newProcess.WaitForExit(600000);
		}
		catch (Exception ex)
		{
			MessageBox.Show("錯誤：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		finally
		{
			BindToFilesGrid();
			Cursor = Cursors.Default;
		}
		Cursor = Cursors.Default;
	}

	private void btnRenameGreenRatio_Click(object sender, EventArgs e)
	{
		WriteToDatabase();
		jumpToSysmaintain = true;
		Close();
	}

	private void btnDownloadFileList_Click(object sender, EventArgs e)
	{
		SysUser oSysUser = new SysUser();
		string CurrentDBName = oSysUser.GetSysUserDatabaseName(UserID);
		string AddOnPath = CreateDirPath(ProjectCode, CurrentDBName);
		string DownloadLink = PubTools.GetAppSet_String("WaterResourcesAgency_URL");
		if (File.Exists(AddOnPath + "\\List.txt"))
		{
			if (MessageBox.Show("已有下載列表，是否重新下載?(重新下載會刪除原有列表!)", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			{
				return;
			}
			for (int i = 1; i < gridWraDocumentFiles.Rows.Count; i++)
			{
				File.Delete(AddOnPath + "\\" + gridWraDocumentFiles.Rows[i]["StdDocName"].ToString() + gridWraDocumentFiles.Rows[i]["StdFileNameExtend"].ToString());
			}
			try
			{
				FtpWebRequest request = GetRequest(DownloadLink + "List.txt");
				request.Method = "RETR";
				request.Proxy = GlobalProxySelection.GetEmptyWebProxy();
				WriteStream(request.GetResponse().GetResponseStream(), File.Create(AddOnPath + "\\List.txt"));
				using (StreamReader sr = new StreamReader(AddOnPath + "\\List.txt", Encoding.GetEncoding("Big5")))
				{
					for (int i = gridWraDocumentFiles.Rows.Count - 1; i > 0; i--)
					{
						gridWraDocumentFiles.Rows.Remove(i);
					}
					int rowindex = 1;
					char[] splitChars = new char[2] { '.', ',' };
					string line;
					while ((line = sr.ReadLine()) != null)
					{
						gridWraDocumentFiles.Rows.Count = gridWraDocumentFiles.Rows.Count + 1;
						string[] words = line.Split(splitChars);
						gridWraDocumentFiles.Rows[rowindex]["ChapNo"] = words[0];
						gridWraDocumentFiles.Rows[rowindex]["ChapName"] = words[1];
						gridWraDocumentFiles.Rows[rowindex]["StdDocName"] = words[2];
						gridWraDocumentFiles.Rows[rowindex]["StdFileNameExtend"] = "." + words[3];
						rowindex++;
					}
				}
				File.Copy(AddOnPath + "\\List.txt", AddOnPath + "\\List_bak.txt", overwrite: true);
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show("無法下載檔案，錯誤訊息:" + ex.Message);
				return;
			}
		}
		try
		{
			FtpWebRequest request = GetRequest(DownloadLink + "List.txt");
			request.Method = "RETR";
			request.Proxy = GlobalProxySelection.GetEmptyWebProxy();
			WriteStream(request.GetResponse().GetResponseStream(), File.Create(AddOnPath + "\\List.txt"));
			using (StreamReader sr = new StreamReader(AddOnPath + "\\List.txt", Encoding.GetEncoding("Big5")))
			{
				int rowindex = 1;
				char[] splitChars = new char[2] { '.', ',' };
				string line;
				while ((line = sr.ReadLine()) != null && line != "")
				{
					gridWraDocumentFiles.Rows.Count = gridWraDocumentFiles.Rows.Count + 1;
					string[] words = line.Split(splitChars);
					gridWraDocumentFiles.Rows[rowindex]["ChapNo"] = words[0];
					gridWraDocumentFiles.Rows[rowindex]["ChapName"] = words[1];
					gridWraDocumentFiles.Rows[rowindex]["StdDocName"] = words[2];
					gridWraDocumentFiles.Rows[rowindex]["StdFileNameExtend"] = "." + words[3];
					rowindex++;
				}
				btnDownloadFile.Enabled = true;
				btnUploadFile.Enabled = true;
				btnDownloadFile.Enabled = true;
				btnChangeNo.Enabled = true;
				btnSaveFileList.Enabled = true;
				btnFrontCover.Enabled = true;
				btnSpecificationFrontCover.Enabled = true;
				btnSpecificationAddFrontCover.Enabled = true;
				btnCompressionAndDownload.Enabled = true;
			}
			File.Copy(AddOnPath + "\\List.txt", AddOnPath + "\\List_bak.txt", overwrite: true);
		}
		catch (Exception ex)
		{
			MessageBox.Show("無法下載檔案，錯誤訊息:" + ex.Message);
		}
	}

	private string CreateDirPath(string projectCode, string DB)
	{
		string AddOnPath = AppDomain.CurrentDomain.BaseDirectory + "WRAAddOn\\" + DB + "\\" + projectCode;
		if (!Directory.Exists(AddOnPath))
		{
			Directory.CreateDirectory(AddOnPath);
		}
		return AddOnPath;
	}

	private void WRAFilesGrid_Delete(object sender, EventArgs e)
	{
		string fileName = gridWraDocumentFiles.Rows[gridWraDocumentFiles.Row][3].ToString() + gridWraDocumentFiles.Rows[gridWraDocumentFiles.Row][4].ToString();
		DialogResult dialogResult = MessageBox.Show("你確定要刪除 " + fileName + " ？", "確認刪除檔案", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
		if (dialogResult == DialogResult.OK)
		{
			SysUser oSysUser = new SysUser();
			string CurrentDBName = oSysUser.GetSysUserDatabaseName(UserID);
			string AddOnPath = CreateDirPath(ProjectCode, CurrentDBName);
			File.Delete(AddOnPath + "\\" + gridWraDocumentFiles.Rows[gridWraDocumentFiles.Row]["StdDocName"].ToString() + gridWraDocumentFiles.Rows[gridWraDocumentFiles.Row]["StdFileNameExtend"].ToString());
			gridWraDocumentFiles.Rows.Remove(gridWraDocumentFiles.Rows[gridWraDocumentFiles.Row]);
			SaveFileList();
		}
	}

	private void btnDownloadFile_Click(object sender, EventArgs e)
	{
		SysUser oSysUser = new SysUser();
		string CurrentDBName = oSysUser.GetSysUserDatabaseName(UserID);
		string AddOnPath = CreateDirPath(ProjectCode, CurrentDBName);
		FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
		FM_INFO._InfoString = "下載中，請稍候! ";
		FM_INFO.Show();
		Application.DoEvents();
		for (int i = 1; i < gridWraDocumentFiles.Rows.Count; i++)
		{
			if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "WRAAddOn\\" + CurrentDBName + "\\" + ProjectCode + "\\" + gridWraDocumentFiles.Rows[i]["StdDocName"].ToString() + gridWraDocumentFiles.Rows[i]["StdFileNameExtend"].ToString()))
			{
				continue;
			}
			try
			{
				string DownloadLink = PubTools.GetAppSet_String("WaterResourcesAgency_URL");
				FtpWebRequest request = GetRequest(DownloadLink + gridWraDocumentFiles.Rows[i]["StdDocName"].ToString() + gridWraDocumentFiles.Rows[i]["StdFileNameExtend"].ToString());
				request.Method = "RETR";
				request.Proxy = GlobalProxySelection.GetEmptyWebProxy();
				WriteStream(request.GetResponse().GetResponseStream(), File.Create(AddOnPath + "\\" + gridWraDocumentFiles.Rows[i]["StdDocName"].ToString() + gridWraDocumentFiles.Rows[i]["StdFileNameExtend"].ToString()));
				if (i < 10)
				{
					gridWraDocumentFiles.Rows[i]["AutoNo"] = "0" + i;
				}
				else
				{
					gridWraDocumentFiles.Rows[i]["AutoNo"] = i;
				}
				gridWraDocumentFiles.Rows[i]["ProjectDocName"] = gridWraDocumentFiles.Rows[i]["StdDocName"];
				gridWraDocumentFiles.Rows[i]["ProjectFileNameExtend"] = gridWraDocumentFiles.Rows[i]["StdFileNameExtend"];
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "錯誤  " + gridWraDocumentFiles.Rows[i]["StdDocName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		FM_INFO.Close();
		FM_INFO.Dispose();
		SaveFileList();
		btnUploadFile.Enabled = true;
		btnChangeNo.Enabled = true;
		btnDownloadFile.Enabled = true;
		btnSaveFileList.Enabled = true;
		btnFrontCover.Enabled = true;
		btnSpecificationFrontCover.Enabled = true;
		btnSpecificationAddFrontCover.Enabled = true;
		btnCompressionAndDownload.Enabled = true;
	}

	private void btnUploadFile_Click(object sender, EventArgs e)
	{
		WRAPickUpChapterDialog WRAPickUpDialog = new WRAPickUpChapterDialog();
		WRAPickUpDialog.UserID = UserID;
		WRAPickUpDialog.ProjectCode = ProjectCode;
		WRAPickUpDialog.FileList2lboxFileList();
		if (WRAPickUpDialog.ShowDialog() == DialogResult.OK && openUploadFileDialog.ShowDialog() == DialogResult.OK)
		{
			SysUser oSysUser = new SysUser();
			string DB = oSysUser.GetSysUserDatabaseName(UserID);
			string AddOnPath = CreateDirPath(ProjectCode, DB);
			if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "WRAAddOn\\" + DB + "\\" + ProjectCode + "\\" + Path.GetFileName(openUploadFileDialog.FileName)))
			{
				if (MessageBox.Show("檔案已存在，是否覆蓋？", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					try
					{
						File.Copy(openUploadFileDialog.FileName, AppDomain.CurrentDomain.BaseDirectory + "WRAAddOn\\" + DB + "\\" + ProjectCode + "\\" + Path.GetFileName(openUploadFileDialog.FileName), overwrite: true);
						MessageBox.Show("上傳完成！");
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message + "  " + Path.GetFileName(openUploadFileDialog.FileName), "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
			}
			else
			{
				File.Copy(openUploadFileDialog.FileName, AppDomain.CurrentDomain.BaseDirectory + "WRAAddOn\\" + DB + "\\" + ProjectCode + "\\" + Path.GetFileName(openUploadFileDialog.FileName));
				MessageBox.Show("上傳完成！");
				string WRAFilelist = WRAPickUpDialog.WRAFilelist;
				string filename = openUploadFileDialog.SafeFileName;
				char[] splitChars = new char[2] { '.', ',' };
				string[] chapter = WRAFilelist.Split(splitChars);
				string[] words = filename.Split(splitChars);
				gridWraDocumentFiles.Rows.Count = gridWraDocumentFiles.Rows.Count + 1;
				gridWraDocumentFiles.Rows[gridWraDocumentFiles.Rows.Count - 1]["StdDocName"] = words[0];
				gridWraDocumentFiles.Rows[gridWraDocumentFiles.Rows.Count - 1]["ProjectDocName"] = words[0];
				gridWraDocumentFiles.Rows[gridWraDocumentFiles.Rows.Count - 1]["StdFilenameExtend"] = "." + words[1];
				gridWraDocumentFiles.Rows[gridWraDocumentFiles.Rows.Count - 1]["ProjectFilenameExtend"] = "." + words[1];
				if (chapter[0] != "其它文件(手動輸入)")
				{
					gridWraDocumentFiles.Rows[gridWraDocumentFiles.Rows.Count - 1]["AutoNo"] = chapter[0];
					gridWraDocumentFiles.Rows[gridWraDocumentFiles.Rows.Count - 1]["ChapNo"] = chapter[1];
					gridWraDocumentFiles.Rows[gridWraDocumentFiles.Rows.Count - 1]["ChapName"] = chapter[2];
				}
				else if (gridWraDocumentFiles.Rows.Count - 1 < 10)
				{
					gridWraDocumentFiles.Rows[gridWraDocumentFiles.Rows.Count - 1]["AutoNo"] = "0" + (gridWraDocumentFiles.Rows.Count - 1);
				}
				else
				{
					gridWraDocumentFiles.Rows[gridWraDocumentFiles.Rows.Count - 1]["AutoNo"] = gridWraDocumentFiles.Rows.Count - 1;
				}
				SaveFileList();
			}
		}
		WRAPickUpDialog.Close();
		WRAPickUpDialog.Dispose();
	}

	private void WRAFilesGrid_ChangeChapterName(object sender, EventArgs e)
	{
		WRAPickUpChapterDialog WRAPickUpDialog = new WRAPickUpChapterDialog();
		WRAPickUpDialog.UserID = UserID;
		WRAPickUpDialog.ProjectCode = ProjectCode;
		WRAPickUpDialog.FileList2lboxFileList();
		if (WRAPickUpDialog.ShowDialog() == DialogResult.OK)
		{
			string WRAFilelist = WRAPickUpDialog.WRAFilelist;
			char[] splitChars = new char[2] { '.', ',' };
			string[] chapter = WRAFilelist.Split(splitChars);
			if (chapter[0] != "其它文件(手動輸入)")
			{
				gridWraDocumentFiles.Rows[gridWraDocumentFiles.Row]["AutoNo"] = chapter[0];
				gridWraDocumentFiles.Rows[gridWraDocumentFiles.Row]["ChapNo"] = chapter[1];
				gridWraDocumentFiles.Rows[gridWraDocumentFiles.Row]["ChapName"] = chapter[2];
			}
			else
			{
				gridWraDocumentFiles.Rows[gridWraDocumentFiles.Row]["AutoNo"] = gridWraDocumentFiles.Rows.Count - 1;
				gridWraDocumentFiles.Rows[gridWraDocumentFiles.Row]["ChapNo"] = "";
				gridWraDocumentFiles.Rows[gridWraDocumentFiles.Row]["ChapName"] = "";
			}
			SaveFileList();
		}
		WRAPickUpDialog.Close();
		WRAPickUpDialog.Dispose();
	}

	private void WRAFilesGrid_CellChanged(object sender, RowColEventArgs e)
	{
		if (e.Col != 6 || gridWraDocumentFiles.Rows[e.Row]["ProjectFilenameExtend"] == null)
		{
			return;
		}
		SysUser oSysUser = new SysUser();
		string CurrentDBName = oSysUser.GetSysUserDatabaseName(UserID);
		string AddOnPath = CreateDirPath(ProjectCode, CurrentDBName);
		string SourceFileName = gridWraDocumentFiles.Rows[e.Row]["StdDocName"].ToString();
		string TargetFileName = gridWraDocumentFiles.Rows[e.Row]["ProjectDocName"].ToString();
		string FileExtension = gridWraDocumentFiles.Rows[e.Row]["StdFilenameExtend"].ToString();
		if (TargetFileName != SourceFileName && File.Exists(AddOnPath + "\\" + SourceFileName + FileExtension))
		{
			try
			{
				File.Move(AddOnPath + "\\" + SourceFileName + FileExtension, AddOnPath + "\\" + TargetFileName + FileExtension);
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
		}
		MessageBox.Show("找不到檔案！請確認 " + documentsFilePath + SourceFileName + FileExtension + " 檔案存在。");
	}

	private void SaveFileList()
	{
		SysUser oSysUser = new SysUser();
		string CurrentDBName = oSysUser.GetSysUserDatabaseName(UserID);
		string AddOnPath = CreateDirPath(ProjectCode, CurrentDBName);
		using StreamWriter sw = new StreamWriter(AddOnPath + "\\List.txt", append: false, Encoding.GetEncoding("Big5"));
		try
		{
			for (int i = 1; i < gridWraDocumentFiles.Rows.Count; i++)
			{
				if (gridWraDocumentFiles.Rows[i]["ChapNo"] == null)
				{
					sw.WriteLine(",," + gridWraDocumentFiles.Rows[i]["StdDocName"].ToString() + gridWraDocumentFiles.Rows[i]["StdFileNameExtend"].ToString() + "," + gridWraDocumentFiles.Rows[i]["ProjectDocName"].ToString() + gridWraDocumentFiles.Rows[i]["ProjectFilenameExtend"].ToString() + "," + gridWraDocumentFiles.Rows[i]["AutoNo"].ToString());
				}
				else
				{
					sw.WriteLine(gridWraDocumentFiles.Rows[i]["ChapNo"].ToString() + "," + gridWraDocumentFiles.Rows[i]["ChapName"].ToString() + "," + gridWraDocumentFiles.Rows[i]["StdDocName"].ToString() + gridWraDocumentFiles.Rows[i]["StdFileNameExtend"].ToString() + "," + gridWraDocumentFiles.Rows[i]["ProjectDocName"].ToString() + gridWraDocumentFiles.Rows[i]["ProjectFilenameExtend"].ToString() + "," + gridWraDocumentFiles.Rows[i]["AutoNo"].ToString());
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnSaveFileList_Click(object sender, EventArgs e)
	{
		SaveFileList();
	}

	private void btnChangeNo_Click(object sender, EventArgs e)
	{
		for (int i = 1; i < gridWraDocumentFiles.Rows.Count; i++)
		{
			if (gridWraDocumentFiles.Rows[i]["AutoNo"] == null || gridWraDocumentFiles.Rows[i]["AutoNo"].ToString() == string.Empty)
			{
				gridWraDocumentFiles.Rows[i]["AutoNo"] = i;
			}
		}
		gridWraDocumentFiles.Sort(SortFlags.Ascending, 5);
		for (int i = 1; i < gridWraDocumentFiles.Rows.Count; i++)
		{
			if (i < 10)
			{
				gridWraDocumentFiles.Rows[i]["AutoNo"] = "0" + i;
			}
			else
			{
				gridWraDocumentFiles.Rows[i]["AutoNo"] = i;
			}
		}
	}

	private void WRAFilesGrid_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left && gridWraDocumentFiles.ColSel == 6)
		{
			if (isFirstClick)
			{
				isFirstClick = false;
				WRAdoubleClickTimer.Start();
			}
			else if (milliseconds < SystemInformation.DoubleClickTime)
			{
				isDoubleClick = true;
			}
		}
		else if (e.Button == MouseButtons.Right && gridWraDocumentFiles.MouseCol >= gridWraDocumentFiles.Cols.Fixed && gridWraDocumentFiles.MouseRow >= gridWraDocumentFiles.Rows.Fixed)
		{
			gridWraDocumentFiles.Col = gridWraDocumentFiles.MouseCol;
			gridWraDocumentFiles.Row = gridWraDocumentFiles.MouseRow;
		}
	}

	private void WRAdoubleClickTimer_Tick(object sender, EventArgs e)
	{
		milliseconds += 100;
		if (milliseconds < SystemInformation.DoubleClickTime)
		{
			return;
		}
		WRAdoubleClickTimer.Stop();
		if (!isDoubleClick)
		{
			SysUser oSysUser = new SysUser();
			string CurrentDBName = oSysUser.GetSysUserDatabaseName(UserID);
			string AddOnPath = CreateDirPath(ProjectCode, CurrentDBName);
			if (gridWraDocumentFiles.MouseRow != -1)
			{
				string FileName = gridWraDocumentFiles.Rows[gridWraDocumentFiles.MouseRow]["ProjectDocName"].ToString() + gridWraDocumentFiles.Rows[gridWraDocumentFiles.MouseRow]["ProjectFilenameExtend"].ToString();
				try
				{
					Process.Start(AddOnPath + "\\" + FileName);
				}
				catch (Exception)
				{
					Process process = new Process();
					string filecommand = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\shell32.dll,OpenAs_RunDLL " + AddOnPath + "\\" + FileName;
					ProcessStartInfo processStartInfo = new ProcessStartInfo("rundll32.exe", filecommand);
					process.StartInfo = processStartInfo;
					process.Start();
				}
			}
		}
		isFirstClick = true;
		isDoubleClick = false;
		milliseconds = 0;
	}

	private void btnFrontCover_Click(object sender, EventArgs e)
	{
		SysUser oSysUser = new SysUser();
		string CurrentDBName = oSysUser.GetSysUserDatabaseName(UserID);
		string AddOnPath = CreateDirPath(ProjectCode, CurrentDBName);
		Cursor = Cursors.WaitCursor;
		bool HaveFront = false;
		ProcessStartInfo WraFrontCoverGenerator = new ProcessStartInfo();
		WraFrontCoverGenerator.FileName = "WraFrontCoverGenerator.exe";
		WraFrontCoverGenerator.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
		WraFrontCoverGenerator.WindowStyle = ProcessWindowStyle.Hidden;
		WraFrontCoverGenerator.Arguments = string.Format("\"{0}#_#{1}\"", AddOnPath, "FrontCover");
		try
		{
			Process newProcess = Process.Start(WraFrontCoverGenerator);
			newProcess.WaitForExit(600000);
		}
		catch (Exception ex)
		{
			MessageBox.Show("錯誤：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		for (int i = 1; i < gridWraDocumentFiles.Rows.Count; i++)
		{
			if (gridWraDocumentFiles.Rows[i]["StdDocName"].ToString() == "目錄")
			{
				HaveFront = true;
			}
		}
		if (!HaveFront)
		{
			gridWraDocumentFiles.Rows.Count = gridWraDocumentFiles.Rows.Count + 1;
			gridWraDocumentFiles.Rows[gridWraDocumentFiles.Rows.Count - 1]["ChapNo"] = "目錄";
			gridWraDocumentFiles.Rows[gridWraDocumentFiles.Rows.Count - 1]["ChapName"] = "預算書目錄";
			gridWraDocumentFiles.Rows[gridWraDocumentFiles.Rows.Count - 1]["StdDocName"] = "預算書目錄";
			gridWraDocumentFiles.Rows[gridWraDocumentFiles.Rows.Count - 1]["StdFileNameExtend"] = ".doc";
			gridWraDocumentFiles.Rows[gridWraDocumentFiles.Rows.Count - 1]["ProjectDocName"] = "預算書目錄";
			gridWraDocumentFiles.Rows[gridWraDocumentFiles.Rows.Count - 1]["ProjectFilenameExtend"] = ".doc";
			gridWraDocumentFiles.Rows[gridWraDocumentFiles.Rows.Count - 1]["AutoNo"] = "01";
		}
		MessageBox.Show("預算書目錄製作完成!");
		Cursor = Cursors.Default;
	}

	private void btnSpecificationFrontCover_Click(object sender, EventArgs e)
	{
		SysUser oSysUser = new SysUser();
		string CurrentDBName = oSysUser.GetSysUserDatabaseName(UserID);
		string AddOnPath = CreateDirPath(ProjectCode, CurrentDBName);
		Cursor = Cursors.WaitCursor;
		ProcessStartInfo WraFrontCoverGenerator = new ProcessStartInfo();
		WraFrontCoverGenerator.FileName = "WraFrontCoverGenerator.exe";
		WraFrontCoverGenerator.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
		WraFrontCoverGenerator.WindowStyle = ProcessWindowStyle.Hidden;
		WraFrontCoverGenerator.Arguments = string.Format("\"{0}#_#{1}\"", AddOnPath, "SpecificationFrontCover");
		try
		{
			Process newProcess = Process.Start(WraFrontCoverGenerator);
			newProcess.WaitForExit(600000);
			MessageBox.Show("施工規範附件目錄製作完成!");
		}
		catch (Exception ex)
		{
			MessageBox.Show("錯誤：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		Cursor = Cursors.Default;
	}

	private void btnSpecificationAddFrontCover_Click(object sender, EventArgs e)
	{
		SysUser oSysUser = new SysUser();
		string CurrentDBName = oSysUser.GetSysUserDatabaseName(UserID);
		string AddOnPath = CreateDirPath(ProjectCode, CurrentDBName);
		Cursor = Cursors.WaitCursor;
		ProcessStartInfo WraFrontCoverGenerator = new ProcessStartInfo();
		WraFrontCoverGenerator.FileName = "WraFrontCoverGenerator.exe";
		WraFrontCoverGenerator.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
		WraFrontCoverGenerator.WindowStyle = ProcessWindowStyle.Hidden;
		WraFrontCoverGenerator.Arguments = string.Format("\"{0}#_#{1}\"", AddOnPath, "SpecificationAddFrontCover");
		try
		{
			Process newProcess = Process.Start(WraFrontCoverGenerator);
			newProcess.WaitForExit(600000);
			MessageBox.Show("施工規範附件目錄製作完成!");
		}
		catch (Exception ex)
		{
			MessageBox.Show("錯誤：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		Cursor = Cursors.Default;
	}

	private void btnCompressionAndDownload_Click(object sender, EventArgs e)
	{
		ZipUtility ZipUtil = new ZipUtility();
		SysUser oSysUser = new SysUser();
		string CurrentDBName = oSysUser.GetSysUserDatabaseName(UserID);
		string AddOnPath = CreateDirPath(ProjectCode, CurrentDBName);
		if (saveDownloadFileDialog.ShowDialog() == DialogResult.OK)
		{
			DirectoryInfo directory = new DirectoryInfo(AddOnPath);
			string[] Files = new string[directory.GetFiles().Length];
			int index = 0;
			FileInfo[] files = directory.GetFiles();
			foreach (FileInfo file in files)
			{
				Files[index++] = file.FullName;
			}
			ZipUtil.ZipFile(Files, saveDownloadFileDialog.FileName);
			MessageBox.Show("下載完成");
		}
	}

	private FtpWebRequest GetRequest(string url)
	{
		string UsrID = PubTools.GetAppSet_String("WaterResourcesAgency_ID");
		string PW = PubTools.GetAppSet_String("WaterResourcesAgency_PW");
		FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(url);
		ftpRequest.Credentials = new NetworkCredential(UsrID, PW);
		ftpRequest.KeepAlive = true;
		return ftpRequest;
	}

	private void WriteStream(Stream orgStream, Stream desStream)
	{
		byte[] buffer = new byte[20480];
		int num;
		while ((num = orgStream.Read(buffer, 0, buffer.Length)) > 0)
		{
			desStream.Write(buffer, 0, num);
		}
		orgStream.Close();
		desStream.Close();
	}
}
