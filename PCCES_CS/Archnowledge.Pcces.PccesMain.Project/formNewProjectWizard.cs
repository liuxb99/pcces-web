using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Archnowledge.Common;
using Archnowledge.DatabaseAccess;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.Bid;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.DomainModule.LogicalBase;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.SysMaintain;
using Archnowledge.Pcces.STDClass;
using Archnowledge.Pcces.XML;
using Archnowledge.Pcces.XML.AuthenticationException;
using Archnowledge.Pcces.XMLClass;
using C1.C1Excel;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinProgressBar;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTabs;

namespace Archnowledge.Pcces.PccesMain.Project;

public class formNewProjectWizard : Form
{
	private IContainer components;

	private UltraTabControl WizardTabs;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private Panel panel2;

	private UltraTabPageControl Tab_A;

	private UltraTabPageControl Tab_B;

	private UltraTabPageControl Tab_C;

	private UltraTabPageControl Tab_D;

	private UltraTabPageControl Tab_E;

	private UltraTabPageControl Tab_F;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private RadioButton RB1;

	private RadioButton RB2;

	private RadioButton RB3;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel5;

	private UltraButton A_Btn_Prev;

	private UltraButton A_Btn_Next;

	private UltraButton A_Btn_Cncl;

	private Panel panel3;

	private UltraButton B_Btn_Cncl;

	private UltraButton B_Btn_Next;

	private UltraButton B_Btn_Prev;

	private Panel panel4;

	private Panel panel5;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel9;

	private UltraTextEditor txtProjectCode;

	private UltraTextEditor txtProjectCName;

	private UltraTextEditor txtProjectEName;

	private UltraLabel ultraLabel10;

	private UltraTextEditor txtProjectAddress;

	private UltraLabel ultraLabel11;

	private GroupBox groupBox3;

	private UltraButton F_Btn_Fnsh;

	private UltraButton F_Btn_Prev;

	private UltraLabel ultraLabel12;

	private UltraLabel ultraLabel13;

	private UltraLabel ultraLabel14;

	private Panel panel7;

	private UltraLabel ultraLabel15;

	private UltraLabel ultraLabel16;

	private GroupBox groupBox4;

	private Panel panel9;

	private UltraLabel ultraLabel17;

	private UltraTextEditor txtPxfin;

	private UltraButton BtnChgDir;

	private OpenFileDialog openFileDialog1;

	private UltraButton E_Btn_Cncl;

	private UltraButton E_Btn_Next;

	private UltraButton E_Btn_Prev;

	private Panel panel10;

	private UltraLabel ultraLabel18;

	private UltraLabel ultraLabel19;

	private Panel panel12;

	private GroupBox groupBox5;

	private UltraButton C_Btn_Cncl;

	private UltraButton C_Btn_Next;

	private UltraButton C_Btn_Prev;

	private Panel panel11;

	private UltraButton ultraButton2;

	private Panel panel13;

	private GridBudget c1FlexGrid1;

	private Panel panel14;

	private UltraLabel ultraLabel20;

	private UltraLabel ultraLabel21;

	private UltraLabel ultraLabel22;

	private UltraLabel ultraLabel23;

	private UltraLabel ultraLabel24;

	private UltraComboEditor cbFind;

	private ImageList imageList1;

	private UltraButton ultraButton1;

	private Panel panel15;

	private UltraLabel ultraLabel25;

	private UltraLabel ultraLabel26;

	private GroupBox groupBox6;

	private UltraButton D_Btn_Cncl;

	private UltraButton D_Btn_Next;

	private UltraButton D_Btn_Prev;

	private Panel panel17;

	private Panel panel18;

	private UltraLabel lblTitle;

	private GridBudget c1FlexGrid2;

	private UltraLabel ultraLabel27;

	private RadioButton RB4;

	private Panel panel19;

	private UltraLabel ultraLabel28;

	private UltraLabel ultraLabel29;

	private Panel panel20;

	private UltraButton BtnChgDirG;

	private UltraTextEditor txtExcelin;

	private UltraLabel ultraLabel30;

	private GroupBox groupBox7;

	private UltraButton G_Btn_Cncl;

	private UltraButton G_Btn_Next;

	private UltraButton G_Btn_Prev;

	private UltraTabPageControl Tab_G;

	private UltraTabPageControl Tab_H;

	private Panel panel22;

	private GroupBox groupBox8;

	private Panel panel23;

	private UltraLabel ultraLabel32;

	private UltraProgressBar Prog1;

	private UltraLabel lblWait;

	private Panel panel24;

	private UltraLabel ultraLabel31;

	private UltraLabel ultraLabel33;

	private UltraLabel ultraLabel34;

	private UltraTabPageControl Tab_I;

	private UltraLabel ultraLabel35;

	private UltraLabel ultraLabel36;

	private UltraLabel ultraLabel37;

	private GroupBox groupBox9;

	private UltraButton ultraButton3;

	private GroupBox gpMessage;

	private UltraLabel ultraLabel38;

	private Panel panel26;

	private GroupBox groupBox10;

	private UltraButton J_Btn_Cncl;

	private UltraButton J_Btn_Next;

	private UltraButton J_Btn_Prev;

	private UltraTabPageControl Tab_J;

	private Panel panel27;

	private UltraLabel ultraLabel39;

	private GridMrsBase GridRail1;

	private PictureBox pictureBox1;

	private UltraTextEditor txtAA;

	private PictureBox pictureBox2;

	private UltraLabel ultraLabel40;

	private UltraButton ultraButton5;

	private PictureBox pictureBox3;

	private UltraLabel ultraLabel41;

	private UltraButton btnJ_LoadEXCEL;

	private UltraTextEditor txtProjectCodeAlias;

	private UltraLabel lblProjectCode;

	private UltraLabel lblProjectCodeAlias;

	private UltraLabel ultraLabel8;

	private UltraLabel ultraLabel42;

	private UltraTextEditor txtProjectMemo;

	private RadioButton RB5;

	private Panel panel28;

	private UltraLabel ultraLabel43;

	private Panel panel29;

	private GridBudget GridSource;

	private UltraLabel ultraLabel44;

	private Panel panel30;

	private GridBudget GridDestination;

	private Panel panel31;

	private UltraButton ultraButton4;

	private UltraButton ultraButton6;

	private UltraLabel ultraLabel45;

	private Panel panel32;

	private UltraButton ultraButton7;

	private UltraButton ultraButton8;

	private UltraButton ultraButton9;

	private UltraButton BtnAll;

	private Panel panel33;

	private Panel panel34;

	private GroupBox groupBox11;

	private UltraButton ultraButton10;

	private UltraButton ultraButton11;

	private UltraButton ultraButton12;

	private Panel panel35;

	private Panel panel36;

	private UltraTabPageControl Tab_K;

	private UltraLabel ultraLabel46;

	private UltraLabel ultraLabel47;

	public Panel panel1;

	public Panel panel6;

	public Panel panel8;

	public Panel panel16;

	public Panel panel21;

	public Panel panel25;

	private CheckBox chkGodMode;

	private string F_PID = "";

	private string F_KeyWord = "";

	private string F_NewProjectCode = "";

	private string F_UserID;

	private string F_SPLT_STATUS = "INI";

	private bool F_IsSplitSucceeded = false;

	private DataTable DT_bud = new DataTable();

	private string F_SubProjectCode = "";

	private string F_ProjectCode = "";

	private string F_ProjectNameC = "";

	private int OptionSet = 1;

	private PccesFormAction F_ActionName;

	private string F_IniMode = "";

	private int GridCols = 15;

	private object[,] GridColsSquence;

	private int F_MainQty = 0;

	private int F_MainCst = 0;

	private int F_MainAmt = 0;

	private int F_AnaQty = 0;

	private int F_AnaCst = 0;

	private int F_AnaAmt = 0;

	private string F_IsAddOn = "";

	private string F_CProjectName = "";

	private string F_OldProjectCode = "";

	private string importdoctype;

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

	public string _IniMode
	{
		get
		{
			return F_IniMode;
		}
		set
		{
			F_IniMode = value;
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

	public string _IsAddOn
	{
		get
		{
			return F_IsAddOn;
		}
		set
		{
			F_IsAddOn = value;
		}
	}

	public bool _InitCreateProject
	{
		set
		{
			if (value)
			{
				RB1.Checked = true;
				return;
			}
			RB1.Checked = false;
			RB2.Checked = true;
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Project.formNewProjectWizard));
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
		Infragistics.Win.Appearance appearance85 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab7 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab8 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab9 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab10 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab11 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraLabel46 = new Infragistics.Win.Misc.UltraLabel();
		this.RB5 = new System.Windows.Forms.RadioButton();
		this.ultraLabel27 = new Infragistics.Win.Misc.UltraLabel();
		this.RB4 = new System.Windows.Forms.RadioButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.RB3 = new System.Windows.Forms.RadioButton();
		this.RB2 = new System.Windows.Forms.RadioButton();
		this.RB1 = new System.Windows.Forms.RadioButton();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraLabel34 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel33 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.txtProjectMemo = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel42 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.txtProjectCodeAlias = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lblProjectCodeAlias = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel31 = new Infragistics.Win.Misc.UltraLabel();
		this.txtProjectAddress = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
		this.txtProjectEName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.txtProjectCName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtProjectCode = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.lblProjectCode = new Infragistics.Win.Misc.UltraLabel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.B_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.B_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.B_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.Tab_C = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel11 = new System.Windows.Forms.Panel();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.cbFind = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ultraLabel24 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.panel13 = new System.Windows.Forms.Panel();
		this.c1FlexGrid1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.panel14 = new System.Windows.Forms.Panel();
		this.ultraLabel20 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel21 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel22 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel23 = new Infragistics.Win.Misc.UltraLabel();
		this.panel12 = new System.Windows.Forms.Panel();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.C_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.C_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.C_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.panel10 = new System.Windows.Forms.Panel();
		this.ultraLabel18 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel19 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_D = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel18 = new System.Windows.Forms.Panel();
		this.c1FlexGrid2 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.panel17 = new System.Windows.Forms.Panel();
		this.lblTitle = new Infragistics.Win.Misc.UltraLabel();
		this.panel16 = new System.Windows.Forms.Panel();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.D_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.D_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.D_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.panel15 = new System.Windows.Forms.Panel();
		this.ultraLabel25 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel26 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_E = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel9 = new System.Windows.Forms.Panel();
		this.chkGodMode = new System.Windows.Forms.CheckBox();
		this.gpMessage = new System.Windows.Forms.GroupBox();
		this.ultraLabel38 = new Infragistics.Win.Misc.UltraLabel();
		this.BtnChgDir = new Infragistics.Win.Misc.UltraButton();
		this.txtPxfin = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel17 = new Infragistics.Win.Misc.UltraLabel();
		this.panel8 = new System.Windows.Forms.Panel();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.E_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.E_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.E_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.panel7 = new System.Windows.Forms.Panel();
		this.ultraLabel15 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel16 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_F = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.panel6 = new System.Windows.Forms.Panel();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.F_Btn_Fnsh = new Infragistics.Win.Misc.UltraButton();
		this.F_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.Tab_G = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel20 = new System.Windows.Forms.Panel();
		this.panel21 = new System.Windows.Forms.Panel();
		this.groupBox7 = new System.Windows.Forms.GroupBox();
		this.G_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.G_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.G_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.BtnChgDirG = new Infragistics.Win.Misc.UltraButton();
		this.txtExcelin = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel30 = new Infragistics.Win.Misc.UltraLabel();
		this.panel19 = new System.Windows.Forms.Panel();
		this.ultraLabel28 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel29 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_H = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel24 = new System.Windows.Forms.Panel();
		this.lblWait = new Infragistics.Win.Misc.UltraLabel();
		this.Prog1 = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
		this.panel23 = new System.Windows.Forms.Panel();
		this.ultraLabel32 = new Infragistics.Win.Misc.UltraLabel();
		this.panel22 = new System.Windows.Forms.Panel();
		this.groupBox8 = new System.Windows.Forms.GroupBox();
		this.Tab_I = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel25 = new System.Windows.Forms.Panel();
		this.groupBox9 = new System.Windows.Forms.GroupBox();
		this.ultraButton3 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel35 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel36 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel37 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_J = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.GridRail1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.panel27 = new System.Windows.Forms.Panel();
		this.ultraLabel41 = new Infragistics.Win.Misc.UltraLabel();
		this.pictureBox3 = new System.Windows.Forms.PictureBox();
		this.ultraButton5 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel40 = new Infragistics.Win.Misc.UltraLabel();
		this.pictureBox2 = new System.Windows.Forms.PictureBox();
		this.btnJ_LoadEXCEL = new Infragistics.Win.Misc.UltraButton();
		this.txtAA = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.ultraLabel39 = new Infragistics.Win.Misc.UltraLabel();
		this.panel26 = new System.Windows.Forms.Panel();
		this.groupBox10 = new System.Windows.Forms.GroupBox();
		this.J_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.J_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.J_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.Tab_K = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel33 = new System.Windows.Forms.Panel();
		this.panel32 = new System.Windows.Forms.Panel();
		this.ultraButton7 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton8 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton9 = new Infragistics.Win.Misc.UltraButton();
		this.BtnAll = new Infragistics.Win.Misc.UltraButton();
		this.panel29 = new System.Windows.Forms.Panel();
		this.GridSource = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.ultraLabel44 = new Infragistics.Win.Misc.UltraLabel();
		this.panel30 = new System.Windows.Forms.Panel();
		this.GridDestination = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.panel31 = new System.Windows.Forms.Panel();
		this.ultraButton4 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton6 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel45 = new Infragistics.Win.Misc.UltraLabel();
		this.panel35 = new System.Windows.Forms.Panel();
		this.panel36 = new System.Windows.Forms.Panel();
		this.panel34 = new System.Windows.Forms.Panel();
		this.ultraLabel47 = new Infragistics.Win.Misc.UltraLabel();
		this.groupBox11 = new System.Windows.Forms.GroupBox();
		this.ultraButton10 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton11 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton12 = new Infragistics.Win.Misc.UltraButton();
		this.panel28 = new System.Windows.Forms.Panel();
		this.ultraLabel43 = new Infragistics.Win.Misc.UltraLabel();
		this.WizardTabs = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.panel2 = new System.Windows.Forms.Panel();
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.Tab_A.SuspendLayout();
		this.panel1.SuspendLayout();
		this.Tab_B.SuspendLayout();
		this.panel5.SuspendLayout();
		this.panel4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtProjectMemo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtProjectCodeAlias).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtProjectAddress).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtProjectEName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtProjectCName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtProjectCode).BeginInit();
		this.panel3.SuspendLayout();
		this.Tab_C.SuspendLayout();
		this.panel11.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cbFind).BeginInit();
		this.panel13.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).BeginInit();
		this.panel14.SuspendLayout();
		this.panel12.SuspendLayout();
		this.panel10.SuspendLayout();
		this.Tab_D.SuspendLayout();
		this.panel18.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid2).BeginInit();
		this.panel17.SuspendLayout();
		this.panel16.SuspendLayout();
		this.panel15.SuspendLayout();
		this.Tab_E.SuspendLayout();
		this.panel9.SuspendLayout();
		this.gpMessage.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtPxfin).BeginInit();
		this.panel8.SuspendLayout();
		this.panel7.SuspendLayout();
		this.Tab_F.SuspendLayout();
		this.panel6.SuspendLayout();
		this.Tab_G.SuspendLayout();
		this.panel20.SuspendLayout();
		this.panel21.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtExcelin).BeginInit();
		this.panel19.SuspendLayout();
		this.Tab_H.SuspendLayout();
		this.panel24.SuspendLayout();
		this.panel23.SuspendLayout();
		this.panel22.SuspendLayout();
		this.Tab_I.SuspendLayout();
		this.panel25.SuspendLayout();
		this.Tab_J.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridRail1).BeginInit();
		this.panel27.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAA).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		this.panel26.SuspendLayout();
		this.Tab_K.SuspendLayout();
		this.panel33.SuspendLayout();
		this.panel32.SuspendLayout();
		this.panel29.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridSource).BeginInit();
		this.panel30.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridDestination).BeginInit();
		this.panel31.SuspendLayout();
		this.panel34.SuspendLayout();
		this.panel28.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.WizardTabs).BeginInit();
		this.WizardTabs.SuspendLayout();
		this.panel2.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.ultraLabel46);
		this.Tab_A.Controls.Add(this.RB5);
		this.Tab_A.Controls.Add(this.ultraLabel27);
		this.Tab_A.Controls.Add(this.RB4);
		this.Tab_A.Controls.Add(this.panel1);
		this.Tab_A.Controls.Add(this.ultraLabel5);
		this.Tab_A.Controls.Add(this.ultraLabel4);
		this.Tab_A.Controls.Add(this.ultraLabel3);
		this.Tab_A.Controls.Add(this.RB3);
		this.Tab_A.Controls.Add(this.RB2);
		this.Tab_A.Controls.Add(this.RB1);
		this.Tab_A.Controls.Add(this.ultraLabel2);
		this.Tab_A.Controls.Add(this.ultraLabel1);
		this.Tab_A.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(664, 536);
		appearance1.BackColor = System.Drawing.Color.White;
		this.ultraLabel46.Appearance = appearance1;
		this.ultraLabel46.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel46.Location = new System.Drawing.Point(72, 340);
		this.ultraLabel46.Name = "ultraLabel46";
		this.ultraLabel46.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel46.TabIndex = 12;
		this.ultraLabel46.Text = "由空白專案建立欲併標專案";
		this.RB5.BackColor = System.Drawing.Color.White;
		this.RB5.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.RB5.Location = new System.Drawing.Point(56, 316);
		this.RB5.Name = "RB5";
		this.RB5.Size = new System.Drawing.Size(168, 24);
		this.RB5.TabIndex = 11;
		this.RB5.Text = "建立併標專案";
		this.RB5.UseVisualStyleBackColor = false;
		appearance2.BackColor = System.Drawing.Color.White;
		this.ultraLabel27.Appearance = appearance2;
		this.ultraLabel27.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel27.Location = new System.Drawing.Point(440, 264);
		this.ultraLabel27.Name = "ultraLabel27";
		this.ultraLabel27.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel27.TabIndex = 10;
		this.ultraLabel27.Text = "轉入特定的預算書 EXCEL DIY 格式檔案";
		this.ultraLabel27.Visible = false;
		this.RB4.BackColor = System.Drawing.Color.White;
		this.RB4.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.RB4.Location = new System.Drawing.Point(424, 240);
		this.RB4.Name = "RB4";
		this.RB4.Size = new System.Drawing.Size(296, 24);
		this.RB4.TabIndex = 9;
		this.RB4.Text = "預算書 Excel DIY 格式轉入";
		this.RB4.UseVisualStyleBackColor = false;
		this.RB4.Visible = false;
		this.panel1.AutoSize = true;
		this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panel1.Controls.Add(this.groupBox1);
		this.panel1.Controls.Add(this.A_Btn_Cncl);
		this.panel1.Controls.Add(this.A_Btn_Next);
		this.panel1.Controls.Add(this.A_Btn_Prev);
		this.panel1.Location = new System.Drawing.Point(0, 492);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(664, 43);
		this.panel1.TabIndex = 8;
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(664, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.A_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Cncl.Appearance = appearance3;
		this.A_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Cncl.Location = new System.Drawing.Point(564, 9);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.ShowFocusRect = false;
		this.A_Btn_Cncl.ShowOutline = false;
		this.A_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Cncl.SupportThemes = false;
		this.A_Btn_Cncl.TabIndex = 2;
		this.A_Btn_Cncl.Text = "取消";
		this.A_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance4.Image = resources.GetObject("appearance4.Image");
		appearance4.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Next.Appearance = appearance4;
		this.A_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Next.Location = new System.Drawing.Point(472, 9);
		this.A_Btn_Next.Name = "A_Btn_Next";
		this.A_Btn_Next.ShowFocusRect = false;
		this.A_Btn_Next.ShowOutline = false;
		this.A_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Next.SupportThemes = false;
		this.A_Btn_Next.TabIndex = 1;
		this.A_Btn_Next.Text = "下一步";
		this.A_Btn_Next.Click += new System.EventHandler(A_Btn_Next_Click);
		this.A_Btn_Prev.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance5.Image = resources.GetObject("appearance5.Image");
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Prev.Appearance = appearance5;
		this.A_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Prev.Location = new System.Drawing.Point(380, 9);
		this.A_Btn_Prev.Name = "A_Btn_Prev";
		this.A_Btn_Prev.ShowFocusRect = false;
		this.A_Btn_Prev.ShowOutline = false;
		this.A_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Prev.SupportThemes = false;
		this.A_Btn_Prev.TabIndex = 0;
		this.A_Btn_Prev.Text = "上一步";
		this.A_Btn_Prev.Visible = false;
		appearance6.BackColor = System.Drawing.Color.White;
		this.ultraLabel5.Appearance = appearance6;
		this.ultraLabel5.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel5.Location = new System.Drawing.Point(72, 268);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel5.TabIndex = 7;
		this.ultraLabel5.Text = "由主專案建立欲分標專案";
		appearance7.BackColor = System.Drawing.Color.White;
		this.ultraLabel4.Appearance = appearance7;
		this.ultraLabel4.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel4.Location = new System.Drawing.Point(72, 188);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel4.TabIndex = 6;
		this.ultraLabel4.Text = "轉入預算書, 空白標單之 xml 格式檔案及新版本 zmd 格式";
		appearance8.BackColor = System.Drawing.Color.White;
		this.ultraLabel3.Appearance = appearance8;
		this.ultraLabel3.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel3.Location = new System.Drawing.Point(72, 114);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel3.TabIndex = 5;
		this.ultraLabel3.Text = "新增空白的預算書專案";
		this.RB3.BackColor = System.Drawing.Color.White;
		this.RB3.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.RB3.Location = new System.Drawing.Point(56, 244);
		this.RB3.Name = "RB3";
		this.RB3.Size = new System.Drawing.Size(168, 24);
		this.RB3.TabIndex = 4;
		this.RB3.Text = "建立分標專案";
		this.RB3.UseVisualStyleBackColor = false;
		this.RB3.CheckedChanged += new System.EventHandler(RB1_CheckedChanged);
		this.RB2.BackColor = System.Drawing.Color.White;
		this.RB2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.RB2.Location = new System.Drawing.Point(56, 160);
		this.RB2.Name = "RB2";
		this.RB2.Size = new System.Drawing.Size(336, 24);
		this.RB2.TabIndex = 3;
		this.RB2.Text = "XML 電子檔轉入(含內部交換 ZMD格式)";
		this.RB2.UseVisualStyleBackColor = false;
		this.RB2.CheckedChanged += new System.EventHandler(RB1_CheckedChanged);
		this.RB1.BackColor = System.Drawing.Color.White;
		this.RB1.Checked = true;
		this.RB1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.RB1.Location = new System.Drawing.Point(56, 88);
		this.RB1.Name = "RB1";
		this.RB1.Size = new System.Drawing.Size(168, 24);
		this.RB1.TabIndex = 2;
		this.RB1.TabStop = true;
		this.RB1.Text = "建立空白專案";
		this.RB1.UseVisualStyleBackColor = false;
		this.RB1.CheckedChanged += new System.EventHandler(RB1_CheckedChanged);
		appearance9.BackColor = System.Drawing.Color.White;
		this.ultraLabel2.Appearance = appearance9;
		this.ultraLabel2.Location = new System.Drawing.Point(52, 64);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel2.TabIndex = 1;
		this.ultraLabel2.Text = "你要以哪種方式新增專案?";
		appearance10.BackColor = System.Drawing.Color.White;
		this.ultraLabel1.Appearance = appearance10;
		this.ultraLabel1.Location = new System.Drawing.Point(24, 24);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(588, 20);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "歡迎使用新增專案精靈，接下來我們將引導您一步一步建立專案";
		this.Tab_B.Controls.Add(this.panel5);
		this.Tab_B.Controls.Add(this.panel4);
		this.Tab_B.Controls.Add(this.panel3);
		this.Tab_B.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(664, 536);
		this.panel5.BackColor = System.Drawing.Color.White;
		this.panel5.Controls.Add(this.ultraLabel34);
		this.panel5.Controls.Add(this.ultraLabel33);
		this.panel5.Controls.Add(this.ultraLabel7);
		this.panel5.Controls.Add(this.ultraLabel6);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(664, 60);
		this.panel5.TabIndex = 11;
		this.ultraLabel34.Location = new System.Drawing.Point(268, 35);
		this.ultraLabel34.Name = "ultraLabel34";
		this.ultraLabel34.Size = new System.Drawing.Size(100, 17);
		this.ultraLabel34.TabIndex = 13;
		this.ultraLabel34.Text = "為必填欄位)";
		appearance11.ForeColor = System.Drawing.Color.Red;
		this.ultraLabel33.Appearance = appearance11;
		this.ultraLabel33.Location = new System.Drawing.Point(256, 37);
		this.ultraLabel33.Name = "ultraLabel33";
		this.ultraLabel33.Size = new System.Drawing.Size(16, 16);
		this.ultraLabel33.TabIndex = 12;
		this.ultraLabel33.Text = "*";
		appearance12.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance12;
		this.ultraLabel7.Location = new System.Drawing.Point(47, 34);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(217, 20);
		this.ultraLabel7.TabIndex = 3;
		this.ultraLabel7.Text = "你可以填寫新專案的基本資料(";
		appearance13.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance13;
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(16, 12);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 2;
		this.ultraLabel6.Text = "專案基本資料";
		this.panel4.Controls.Add(this.txtProjectMemo);
		this.panel4.Controls.Add(this.ultraLabel42);
		this.panel4.Controls.Add(this.ultraLabel8);
		this.panel4.Controls.Add(this.txtProjectCodeAlias);
		this.panel4.Controls.Add(this.lblProjectCodeAlias);
		this.panel4.Controls.Add(this.ultraLabel31);
		this.panel4.Controls.Add(this.txtProjectAddress);
		this.panel4.Controls.Add(this.ultraLabel11);
		this.panel4.Controls.Add(this.txtProjectEName);
		this.panel4.Controls.Add(this.ultraLabel10);
		this.panel4.Controls.Add(this.txtProjectCName);
		this.panel4.Controls.Add(this.txtProjectCode);
		this.panel4.Controls.Add(this.ultraLabel9);
		this.panel4.Controls.Add(this.lblProjectCode);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel4.Location = new System.Drawing.Point(0, 0);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(664, 493);
		this.panel4.TabIndex = 10;
		this.txtProjectMemo.AutoSize = true;
		this.txtProjectMemo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtProjectMemo.Location = new System.Drawing.Point(52, 367);
		this.txtProjectMemo.MaxLength = 200;
		this.txtProjectMemo.Multiline = true;
		this.txtProjectMemo.Name = "txtProjectMemo";
		this.txtProjectMemo.Size = new System.Drawing.Size(564, 45);
		this.txtProjectMemo.TabIndex = 18;
		this.txtProjectMemo.Text = "[txtProjectMemo]";
		this.ultraLabel42.Location = new System.Drawing.Point(48, 349);
		this.ultraLabel42.Name = "ultraLabel42";
		this.ultraLabel42.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel42.TabIndex = 17;
		this.ultraLabel42.Text = "說明:";
		this.ultraLabel8.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel8.Location = new System.Drawing.Point(52, 468);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel8.TabIndex = 16;
		this.ultraLabel8.Text = "(若機關內有需要使用另一組工程代碼，可使用此一欄位)";
		this.txtProjectCodeAlias.AlphaBlendMode = Infragistics.Win.AlphaBlendMode.Disabled;
		appearance14.FontData.Name = "細明體";
		appearance14.FontData.SizeInPoints = 11f;
		this.txtProjectCodeAlias.Appearance = appearance14;
		this.txtProjectCodeAlias.AutoSize = true;
		this.txtProjectCodeAlias.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtProjectCodeAlias.Location = new System.Drawing.Point(52, 440);
		this.txtProjectCodeAlias.MaxLength = 40;
		this.txtProjectCodeAlias.Name = "txtProjectCodeAlias";
		this.txtProjectCodeAlias.Size = new System.Drawing.Size(564, 24);
		this.txtProjectCodeAlias.TabIndex = 14;
		this.txtProjectCodeAlias.Text = "[txtProjectCodeAlias]";
		this.lblProjectCodeAlias.Location = new System.Drawing.Point(48, 422);
		this.lblProjectCodeAlias.Name = "lblProjectCodeAlias";
		this.lblProjectCodeAlias.Size = new System.Drawing.Size(408, 20);
		this.lblProjectCodeAlias.TabIndex = 13;
		this.lblProjectCodeAlias.Text = "工程別號:";
		appearance15.ForeColor = System.Drawing.Color.Red;
		this.ultraLabel31.Appearance = appearance15;
		this.ultraLabel31.Location = new System.Drawing.Point(36, 68);
		this.ultraLabel31.Name = "ultraLabel31";
		this.ultraLabel31.Size = new System.Drawing.Size(16, 23);
		this.ultraLabel31.TabIndex = 12;
		this.ultraLabel31.Text = "*";
		this.txtProjectAddress.AutoSize = true;
		this.txtProjectAddress.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtProjectAddress.Location = new System.Drawing.Point(52, 292);
		this.txtProjectAddress.MaxLength = 200;
		this.txtProjectAddress.Multiline = true;
		this.txtProjectAddress.Name = "txtProjectAddress";
		this.txtProjectAddress.Size = new System.Drawing.Size(564, 45);
		this.txtProjectAddress.TabIndex = 11;
		this.txtProjectAddress.Text = "[txtProjectAddress]";
		this.txtProjectAddress.Validating += new System.ComponentModel.CancelEventHandler(txtProjectCode_Validating);
		this.ultraLabel11.Location = new System.Drawing.Point(48, 275);
		this.ultraLabel11.Name = "ultraLabel11";
		this.ultraLabel11.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel11.TabIndex = 10;
		this.ultraLabel11.Text = "工程地點:";
		this.txtProjectEName.AutoSize = true;
		this.txtProjectEName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtProjectEName.Location = new System.Drawing.Point(52, 216);
		this.txtProjectEName.MaxLength = 200;
		this.txtProjectEName.Multiline = true;
		this.txtProjectEName.Name = "txtProjectEName";
		this.txtProjectEName.Size = new System.Drawing.Size(564, 45);
		this.txtProjectEName.TabIndex = 9;
		this.txtProjectEName.Text = "[txtProjectEName]";
		this.txtProjectEName.Validating += new System.ComponentModel.CancelEventHandler(txtProjectCode_Validating);
		this.ultraLabel10.Location = new System.Drawing.Point(48, 198);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel10.TabIndex = 8;
		this.ultraLabel10.Text = "Project Name (English):";
		this.txtProjectCName.AutoSize = true;
		this.txtProjectCName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtProjectCName.Location = new System.Drawing.Point(52, 142);
		this.txtProjectCName.MaxLength = 200;
		this.txtProjectCName.Multiline = true;
		this.txtProjectCName.Name = "txtProjectCName";
		this.txtProjectCName.Size = new System.Drawing.Size(564, 45);
		this.txtProjectCName.TabIndex = 7;
		this.txtProjectCName.Text = "[txtProjectCName]";
		this.txtProjectCName.Validating += new System.ComponentModel.CancelEventHandler(txtProjectCode_Validating);
		this.txtProjectCName.Leave += new System.EventHandler(txtProjectCName_Leave);
		this.txtProjectCode.AlphaBlendMode = Infragistics.Win.AlphaBlendMode.Disabled;
		appearance16.FontData.Name = "細明體";
		appearance16.FontData.SizeInPoints = 11f;
		this.txtProjectCode.Appearance = appearance16;
		this.txtProjectCode.AutoSize = true;
		this.txtProjectCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtProjectCode.Location = new System.Drawing.Point(52, 88);
		this.txtProjectCode.MaxLength = 40;
		this.txtProjectCode.Name = "txtProjectCode";
		this.txtProjectCode.Size = new System.Drawing.Size(564, 24);
		this.txtProjectCode.TabIndex = 6;
		this.txtProjectCode.Text = "[txtProjectCode]";
		this.txtProjectCode.Validating += new System.ComponentModel.CancelEventHandler(txtProjectCode_Validating);
		this.txtProjectCode.Leave += new System.EventHandler(txtProjectCode_Leave);
		this.ultraLabel9.Location = new System.Drawing.Point(48, 124);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel9.TabIndex = 5;
		this.ultraLabel9.Text = "工程名稱:";
		this.lblProjectCode.Location = new System.Drawing.Point(48, 68);
		this.lblProjectCode.Name = "lblProjectCode";
		this.lblProjectCode.Size = new System.Drawing.Size(408, 20);
		this.lblProjectCode.TabIndex = 4;
		this.lblProjectCode.Text = "工程代碼:";
		this.panel3.AutoSize = true;
		this.panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panel3.Controls.Add(this.groupBox2);
		this.panel3.Controls.Add(this.B_Btn_Cncl);
		this.panel3.Controls.Add(this.B_Btn_Next);
		this.panel3.Controls.Add(this.B_Btn_Prev);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel3.Location = new System.Drawing.Point(0, 493);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(664, 43);
		this.panel3.TabIndex = 9;
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(664, 8);
		this.groupBox2.TabIndex = 4;
		this.groupBox2.TabStop = false;
		this.B_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance17.Image = resources.GetObject("appearance17.Image");
		appearance17.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Cncl.Appearance = appearance17;
		this.B_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Cncl.Location = new System.Drawing.Point(564, 9);
		this.B_Btn_Cncl.Name = "B_Btn_Cncl";
		this.B_Btn_Cncl.ShowFocusRect = false;
		this.B_Btn_Cncl.ShowOutline = false;
		this.B_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Cncl.SupportThemes = false;
		this.B_Btn_Cncl.TabIndex = 2;
		this.B_Btn_Cncl.Text = "取消";
		this.B_Btn_Cncl.Click += new System.EventHandler(B_Btn_Cncl_Click);
		this.B_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance18.Image = resources.GetObject("appearance18.Image");
		appearance18.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance18.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Next.Appearance = appearance18;
		this.B_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Next.Location = new System.Drawing.Point(472, 9);
		this.B_Btn_Next.Name = "B_Btn_Next";
		this.B_Btn_Next.ShowFocusRect = false;
		this.B_Btn_Next.ShowOutline = false;
		this.B_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Next.SupportThemes = false;
		this.B_Btn_Next.TabIndex = 1;
		this.B_Btn_Next.Text = "下一步";
		this.B_Btn_Next.Click += new System.EventHandler(B_Btn_Next_Click);
		this.B_Btn_Prev.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance19.Image = resources.GetObject("appearance19.Image");
		appearance19.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Prev.Appearance = appearance19;
		this.B_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Prev.Location = new System.Drawing.Point(380, 9);
		this.B_Btn_Prev.Name = "B_Btn_Prev";
		this.B_Btn_Prev.ShowFocusRect = false;
		this.B_Btn_Prev.ShowOutline = false;
		this.B_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Prev.SupportThemes = false;
		this.B_Btn_Prev.TabIndex = 0;
		this.B_Btn_Prev.Text = "上一步";
		this.B_Btn_Prev.Click += new System.EventHandler(B_Btn_Prev_Click);
		this.Tab_C.Controls.Add(this.panel11);
		this.Tab_C.Controls.Add(this.panel12);
		this.Tab_C.Controls.Add(this.panel10);
		this.Tab_C.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_C.Name = "Tab_C";
		this.Tab_C.Size = new System.Drawing.Size(664, 536);
		this.panel11.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel11.Controls.Add(this.ultraButton1);
		this.panel11.Controls.Add(this.cbFind);
		this.panel11.Controls.Add(this.ultraLabel24);
		this.panel11.Controls.Add(this.ultraButton2);
		this.panel11.Controls.Add(this.panel13);
		this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel11.Location = new System.Drawing.Point(0, 60);
		this.panel11.Name = "panel11";
		this.panel11.Size = new System.Drawing.Size(664, 433);
		this.panel11.TabIndex = 15;
		appearance20.Image = 0;
		this.ultraButton1.Appearance = appearance20;
		this.ultraButton1.BackColor = System.Drawing.Color.Transparent;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.PopupBorderless;
		this.ultraButton1.ImageList = this.imageList1;
		this.ultraButton1.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton1.Location = new System.Drawing.Point(628, 4);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(24, 24);
		this.ultraButton1.TabIndex = 12;
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
		this.imageList1.Images.SetKeyName(0, "");
		appearance21.FontData.SizeInPoints = 11f;
		this.cbFind.Appearance = appearance21;
		this.cbFind.AutoSize = true;
		this.cbFind.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		appearance22.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		this.cbFind.ButtonAppearance = appearance22;
		this.cbFind.ButtonStyle = Infragistics.Win.UIElementButtonStyle.PopupBorderless;
		this.cbFind.Location = new System.Drawing.Point(492, 7);
		this.cbFind.Name = "cbFind";
		this.cbFind.Size = new System.Drawing.Size(137, 20);
		this.cbFind.TabIndex = 11;
		this.cbFind.Text = null;
		this.cbFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(cbFind_KeyPress);
		this.ultraLabel24.Location = new System.Drawing.Point(450, 8);
		this.ultraLabel24.Name = "ultraLabel24";
		this.ultraLabel24.Size = new System.Drawing.Size(40, 20);
		this.ultraLabel24.TabIndex = 10;
		this.ultraLabel24.Text = "尋找:";
		appearance23.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance23.BackColor2 = System.Drawing.Color.Silver;
		appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		this.ultraButton2.Appearance = appearance23;
		this.ultraButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton2.Location = new System.Drawing.Point(588, 412);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.Size = new System.Drawing.Size(92, 28);
		this.ultraButton2.TabIndex = 9;
		this.ultraButton2.Text = "取消";
		this.ultraButton2.Visible = false;
		this.panel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel13.Controls.Add(this.c1FlexGrid1);
		this.panel13.Controls.Add(this.panel14);
		this.panel13.Location = new System.Drawing.Point(8, 40);
		this.panel13.Name = "panel13";
		this.panel13.Size = new System.Drawing.Size(648, 356);
		this.panel13.TabIndex = 8;
		this.c1FlexGrid1._ExcelFileName = "";
		this.c1FlexGrid1._ExcelSheeName = "";
		this.c1FlexGrid1._IsOpenExcelAfterExport = false;
		this.c1FlexGrid1.AllowEditing = false;
		this.c1FlexGrid1.BackColor = System.Drawing.Color.White;
		this.c1FlexGrid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.c1FlexGrid1.ColumnInfo = resources.GetString("c1FlexGrid1.ColumnInfo");
		this.c1FlexGrid1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.c1FlexGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1FlexGrid1.ExtendLastCol = true;
		this.c1FlexGrid1.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
		this.c1FlexGrid1.ForeColor = System.Drawing.SystemColors.WindowText;
		this.c1FlexGrid1.Location = new System.Drawing.Point(0, 36);
		this.c1FlexGrid1.Name = "c1FlexGrid1";
		this.c1FlexGrid1.Rows.Fixed = 0;
		this.c1FlexGrid1.Rows.MinSize = 25;
		this.c1FlexGrid1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.c1FlexGrid1.ShowToolTipOnNarrowColumn = false;
		this.c1FlexGrid1.Size = new System.Drawing.Size(646, 318);
		this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid1.Styles"));
		this.c1FlexGrid1.TabIndex = 8;
		this.c1FlexGrid1.Click += new System.EventHandler(c1FlexGrid1_Click);
		this.c1FlexGrid1.MouseMove += new System.Windows.Forms.MouseEventHandler(c1FlexGrid1_MouseMove);
		this.panel14.Controls.Add(this.ultraLabel20);
		this.panel14.Controls.Add(this.ultraLabel21);
		this.panel14.Controls.Add(this.ultraLabel22);
		this.panel14.Controls.Add(this.ultraLabel23);
		this.panel14.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel14.Location = new System.Drawing.Point(0, 0);
		this.panel14.Name = "panel14";
		this.panel14.Size = new System.Drawing.Size(646, 36);
		this.panel14.TabIndex = 7;
		appearance24.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance24.BackColor2 = System.Drawing.Color.FromArgb(225, 247, 223);
		appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance24.FontData.Name = "細明體";
		appearance24.FontData.SizeInPoints = 11f;
		appearance24.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance24.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel20.Appearance = appearance24;
		this.ultraLabel20.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraLabel20.Location = new System.Drawing.Point(452, 0);
		this.ultraLabel20.Name = "ultraLabel20";
		this.ultraLabel20.Size = new System.Drawing.Size(194, 36);
		this.ultraLabel20.TabIndex = 2;
		this.ultraLabel20.Text = "工程地址";
		appearance25.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance25.BackColor2 = System.Drawing.Color.FromArgb(225, 247, 223);
		appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance25.FontData.Name = "細明體";
		appearance25.FontData.SizeInPoints = 11f;
		appearance25.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance25.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel21.Appearance = appearance25;
		this.ultraLabel21.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel21.Location = new System.Drawing.Point(132, 0);
		this.ultraLabel21.Name = "ultraLabel21";
		this.ultraLabel21.Size = new System.Drawing.Size(320, 36);
		this.ultraLabel21.TabIndex = 1;
		this.ultraLabel21.Text = "工程名稱";
		appearance26.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance26.BackColor2 = System.Drawing.Color.FromArgb(225, 247, 223);
		appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance26.FontData.Name = "細明體";
		appearance26.FontData.SizeInPoints = 11f;
		appearance26.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance26.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel22.Appearance = appearance26;
		this.ultraLabel22.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel22.Location = new System.Drawing.Point(28, 0);
		this.ultraLabel22.Name = "ultraLabel22";
		this.ultraLabel22.Size = new System.Drawing.Size(104, 36);
		this.ultraLabel22.TabIndex = 3;
		this.ultraLabel22.Text = "工項代碼";
		appearance27.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance27.BackColor2 = System.Drawing.Color.FromArgb(225, 247, 223);
		appearance27.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance27.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance27.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel23.Appearance = appearance27;
		this.ultraLabel23.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel23.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel23.Name = "ultraLabel23";
		this.ultraLabel23.Size = new System.Drawing.Size(28, 36);
		this.ultraLabel23.TabIndex = 0;
		this.panel12.AutoSize = true;
		this.panel12.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panel12.Controls.Add(this.groupBox5);
		this.panel12.Controls.Add(this.C_Btn_Cncl);
		this.panel12.Controls.Add(this.C_Btn_Next);
		this.panel12.Controls.Add(this.C_Btn_Prev);
		this.panel12.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel12.Location = new System.Drawing.Point(0, 493);
		this.panel12.Name = "panel12";
		this.panel12.Size = new System.Drawing.Size(664, 43);
		this.panel12.TabIndex = 14;
		this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox5.Location = new System.Drawing.Point(0, 0);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(664, 8);
		this.groupBox5.TabIndex = 4;
		this.groupBox5.TabStop = false;
		this.C_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance28.Image = resources.GetObject("appearance28.Image");
		appearance28.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.C_Btn_Cncl.Appearance = appearance28;
		this.C_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.C_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.C_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.C_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.C_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.C_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.C_Btn_Cncl.Location = new System.Drawing.Point(564, 9);
		this.C_Btn_Cncl.Name = "C_Btn_Cncl";
		this.C_Btn_Cncl.ShowFocusRect = false;
		this.C_Btn_Cncl.ShowOutline = false;
		this.C_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.C_Btn_Cncl.SupportThemes = false;
		this.C_Btn_Cncl.TabIndex = 2;
		this.C_Btn_Cncl.Text = "取消";
		this.C_Btn_Cncl.Click += new System.EventHandler(C_Btn_Cncl_Click);
		this.C_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance29.Image = resources.GetObject("appearance29.Image");
		appearance29.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance29.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.C_Btn_Next.Appearance = appearance29;
		this.C_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.C_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.C_Btn_Next.Enabled = false;
		this.C_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.C_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.C_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.C_Btn_Next.Location = new System.Drawing.Point(472, 9);
		this.C_Btn_Next.Name = "C_Btn_Next";
		this.C_Btn_Next.ShowFocusRect = false;
		this.C_Btn_Next.ShowOutline = false;
		this.C_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.C_Btn_Next.SupportThemes = false;
		this.C_Btn_Next.TabIndex = 1;
		this.C_Btn_Next.Text = "下一步";
		this.C_Btn_Next.Click += new System.EventHandler(C_Btn_Next_Click);
		this.C_Btn_Prev.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance30.Image = resources.GetObject("appearance30.Image");
		appearance30.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.C_Btn_Prev.Appearance = appearance30;
		this.C_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.C_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.C_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.C_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.C_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.C_Btn_Prev.Location = new System.Drawing.Point(380, 9);
		this.C_Btn_Prev.Name = "C_Btn_Prev";
		this.C_Btn_Prev.ShowFocusRect = false;
		this.C_Btn_Prev.ShowOutline = false;
		this.C_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.C_Btn_Prev.SupportThemes = false;
		this.C_Btn_Prev.TabIndex = 0;
		this.C_Btn_Prev.Text = "上一步";
		this.C_Btn_Prev.Click += new System.EventHandler(C_Btn_Prev_Click);
		this.panel10.BackColor = System.Drawing.Color.White;
		this.panel10.Controls.Add(this.ultraLabel18);
		this.panel10.Controls.Add(this.ultraLabel19);
		this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel10.Location = new System.Drawing.Point(0, 0);
		this.panel10.Name = "panel10";
		this.panel10.Size = new System.Drawing.Size(664, 60);
		this.panel10.TabIndex = 12;
		appearance31.BackColor = System.Drawing.Color.White;
		this.ultraLabel18.Appearance = appearance31;
		this.ultraLabel18.Location = new System.Drawing.Point(47, 34);
		this.ultraLabel18.Name = "ultraLabel18";
		this.ultraLabel18.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel18.TabIndex = 3;
		this.ultraLabel18.Text = "您可以挑選欲進行分標的主專案";
		appearance32.BackColor = System.Drawing.Color.White;
		this.ultraLabel19.Appearance = appearance32;
		this.ultraLabel19.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel19.Location = new System.Drawing.Point(16, 12);
		this.ultraLabel19.Name = "ultraLabel19";
		this.ultraLabel19.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel19.TabIndex = 2;
		this.ultraLabel19.Text = "主專案挑選";
		this.Tab_D.Controls.Add(this.panel18);
		this.Tab_D.Controls.Add(this.panel17);
		this.Tab_D.Controls.Add(this.panel16);
		this.Tab_D.Controls.Add(this.panel15);
		this.Tab_D.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_D.Name = "Tab_D";
		this.Tab_D.Size = new System.Drawing.Size(664, 536);
		this.panel18.Controls.Add(this.c1FlexGrid2);
		this.panel18.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel18.Location = new System.Drawing.Point(0, 96);
		this.panel18.Name = "panel18";
		this.panel18.Size = new System.Drawing.Size(664, 397);
		this.panel18.TabIndex = 17;
		this.c1FlexGrid2._ExcelFileName = "";
		this.c1FlexGrid2._ExcelSheeName = "";
		this.c1FlexGrid2._IsOpenExcelAfterExport = false;
		this.c1FlexGrid2.BackColor = System.Drawing.Color.White;
		this.c1FlexGrid2.ColumnInfo = resources.GetString("c1FlexGrid2.ColumnInfo");
		this.c1FlexGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1FlexGrid2.ExtendLastCol = true;
		this.c1FlexGrid2.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.c1FlexGrid2.ForeColor = System.Drawing.Color.Black;
		this.c1FlexGrid2.Location = new System.Drawing.Point(0, 0);
		this.c1FlexGrid2.Name = "c1FlexGrid2";
		this.c1FlexGrid2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.c1FlexGrid2.ShowToolTipOnNarrowColumn = true;
		this.c1FlexGrid2.Size = new System.Drawing.Size(664, 397);
		this.c1FlexGrid2.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid2.Styles"));
		this.c1FlexGrid2.TabIndex = 5;
		this.c1FlexGrid2.Tree.Column = 1;
		this.c1FlexGrid2.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.SimpleLeaf;
		this.c1FlexGrid2.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(c1FlexGrid2_AfterSelChange);
		this.c1FlexGrid2.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(c1FlexGrid2_AfterEdit);
		this.c1FlexGrid2.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(c1FlexGrid2_BeforeEdit);
		this.panel17.Controls.Add(this.lblTitle);
		this.panel17.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel17.Location = new System.Drawing.Point(0, 60);
		this.panel17.Name = "panel17";
		this.panel17.Size = new System.Drawing.Size(664, 36);
		this.panel17.TabIndex = 16;
		appearance33.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance33.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance33.FontData.Name = "新細明體";
		appearance33.FontData.SizeInPoints = 12f;
		appearance33.ForeColor = System.Drawing.Color.White;
		appearance33.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance33.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblTitle.Appearance = appearance33;
		this.lblTitle.Font = new System.Drawing.Font("細明體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTitle.Location = new System.Drawing.Point(0, 0);
		this.lblTitle.Name = "lblTitle";
		this.lblTitle.Size = new System.Drawing.Size(664, 36);
		this.lblTitle.TabIndex = 1;
		this.lblTitle.Text = "主專案:";
		this.panel16.AutoSize = true;
		this.panel16.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panel16.Controls.Add(this.groupBox6);
		this.panel16.Controls.Add(this.D_Btn_Cncl);
		this.panel16.Controls.Add(this.D_Btn_Next);
		this.panel16.Controls.Add(this.D_Btn_Prev);
		this.panel16.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel16.Location = new System.Drawing.Point(0, 493);
		this.panel16.Name = "panel16";
		this.panel16.Size = new System.Drawing.Size(664, 43);
		this.panel16.TabIndex = 15;
		this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox6.Location = new System.Drawing.Point(0, 0);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(664, 8);
		this.groupBox6.TabIndex = 4;
		this.groupBox6.TabStop = false;
		this.D_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance34.Image = resources.GetObject("appearance34.Image");
		appearance34.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Cncl.Appearance = appearance34;
		this.D_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Cncl.Location = new System.Drawing.Point(564, 9);
		this.D_Btn_Cncl.Name = "D_Btn_Cncl";
		this.D_Btn_Cncl.ShowFocusRect = false;
		this.D_Btn_Cncl.ShowOutline = false;
		this.D_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Cncl.SupportThemes = false;
		this.D_Btn_Cncl.TabIndex = 2;
		this.D_Btn_Cncl.Text = "取消";
		this.D_Btn_Cncl.Click += new System.EventHandler(C_Btn_Cncl_Click);
		this.D_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance35.Image = resources.GetObject("appearance35.Image");
		appearance35.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance35.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Next.Appearance = appearance35;
		this.D_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Next.Location = new System.Drawing.Point(472, 9);
		this.D_Btn_Next.Name = "D_Btn_Next";
		this.D_Btn_Next.ShowFocusRect = false;
		this.D_Btn_Next.ShowOutline = false;
		this.D_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Next.SupportThemes = false;
		this.D_Btn_Next.TabIndex = 1;
		this.D_Btn_Next.Text = "下一步";
		this.D_Btn_Next.Click += new System.EventHandler(D_Btn_Next_Click);
		this.D_Btn_Prev.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance36.Image = resources.GetObject("appearance36.Image");
		appearance36.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Prev.Appearance = appearance36;
		this.D_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Prev.Location = new System.Drawing.Point(380, 9);
		this.D_Btn_Prev.Name = "D_Btn_Prev";
		this.D_Btn_Prev.ShowFocusRect = false;
		this.D_Btn_Prev.ShowOutline = false;
		this.D_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Prev.SupportThemes = false;
		this.D_Btn_Prev.TabIndex = 0;
		this.D_Btn_Prev.Text = "上一步";
		this.D_Btn_Prev.Click += new System.EventHandler(D_Btn_Prev_Click);
		this.panel15.BackColor = System.Drawing.Color.White;
		this.panel15.Controls.Add(this.ultraLabel25);
		this.panel15.Controls.Add(this.ultraLabel26);
		this.panel15.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel15.Location = new System.Drawing.Point(0, 0);
		this.panel15.Name = "panel15";
		this.panel15.Size = new System.Drawing.Size(664, 60);
		this.panel15.TabIndex = 13;
		appearance37.BackColor = System.Drawing.Color.White;
		this.ultraLabel25.Appearance = appearance37;
		this.ultraLabel25.Location = new System.Drawing.Point(47, 34);
		this.ultraLabel25.Name = "ultraLabel25";
		this.ultraLabel25.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel25.TabIndex = 3;
		this.ultraLabel25.Text = "您可以由主專案的預算書詳細表中挑選欲分標的項目";
		appearance38.BackColor = System.Drawing.Color.White;
		this.ultraLabel26.Appearance = appearance38;
		this.ultraLabel26.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel26.Location = new System.Drawing.Point(16, 12);
		this.ultraLabel26.Name = "ultraLabel26";
		this.ultraLabel26.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel26.TabIndex = 2;
		this.ultraLabel26.Text = "分標專案預算書詳細表項目挑選";
		this.Tab_E.Controls.Add(this.panel9);
		this.Tab_E.Controls.Add(this.panel8);
		this.Tab_E.Controls.Add(this.panel7);
		this.Tab_E.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_E.Name = "Tab_E";
		this.Tab_E.Size = new System.Drawing.Size(664, 536);
		this.panel9.Controls.Add(this.chkGodMode);
		this.panel9.Controls.Add(this.gpMessage);
		this.panel9.Controls.Add(this.BtnChgDir);
		this.panel9.Controls.Add(this.txtPxfin);
		this.panel9.Controls.Add(this.ultraLabel17);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel9.Location = new System.Drawing.Point(0, 60);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(664, 433);
		this.panel9.TabIndex = 14;
		this.chkGodMode.AutoSize = true;
		this.chkGodMode.Location = new System.Drawing.Point(420, 95);
		this.chkGodMode.Name = "chkGodMode";
		this.chkGodMode.Size = new System.Drawing.Size(202, 19);
		this.chkGodMode.TabIndex = 12;
		this.chkGodMode.Text = "匯入時將標單轉成預算書";
		this.chkGodMode.UseVisualStyleBackColor = true;
		this.chkGodMode.Visible = false;
		this.gpMessage.Controls.Add(this.ultraLabel38);
		this.gpMessage.Location = new System.Drawing.Point(188, 124);
		this.gpMessage.Name = "gpMessage";
		this.gpMessage.Size = new System.Drawing.Size(292, 150);
		this.gpMessage.TabIndex = 11;
		this.gpMessage.TabStop = false;
		this.gpMessage.Visible = false;
		appearance39.TextHAlign = Infragistics.Win.HAlign.Center;
		this.ultraLabel38.Appearance = appearance39;
		this.ultraLabel38.Location = new System.Drawing.Point(8, 56);
		this.ultraLabel38.Name = "ultraLabel38";
		this.ultraLabel38.Size = new System.Drawing.Size(272, 50);
		this.ultraLabel38.TabIndex = 0;
		this.ultraLabel38.Text = "電子檔轉入中...";
		appearance40.FontData.Name = "Arial";
		appearance40.FontData.SizeInPoints = 8f;
		this.BtnChgDir.Appearance = appearance40;
		this.BtnChgDir.BackColor = System.Drawing.SystemColors.Control;
		this.BtnChgDir.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.BtnChgDir.Location = new System.Drawing.Point(574, 65);
		this.BtnChgDir.Name = "BtnChgDir";
		this.BtnChgDir.ShowFocusRect = false;
		this.BtnChgDir.ShowOutline = false;
		this.BtnChgDir.Size = new System.Drawing.Size(48, 24);
		this.BtnChgDir.SupportThemes = false;
		this.BtnChgDir.TabIndex = 10;
		this.BtnChgDir.Text = "瀏覽...";
		this.BtnChgDir.Click += new System.EventHandler(BtnChgDir_Click);
		appearance41.FontData.Name = "細明體";
		appearance41.FontData.SizeInPoints = 11f;
		this.txtPxfin.Appearance = appearance41;
		this.txtPxfin.AutoSize = true;
		this.txtPxfin.Location = new System.Drawing.Point(51, 66);
		this.txtPxfin.Name = "txtPxfin";
		this.txtPxfin.Size = new System.Drawing.Size(524, 24);
		this.txtPxfin.TabIndex = 6;
		this.ultraLabel17.Location = new System.Drawing.Point(48, 44);
		this.ultraLabel17.Name = "ultraLabel17";
		this.ultraLabel17.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel17.TabIndex = 5;
		this.ultraLabel17.Text = "欲轉入的電子檔:";
		this.panel8.AutoSize = true;
		this.panel8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panel8.Controls.Add(this.groupBox4);
		this.panel8.Controls.Add(this.E_Btn_Cncl);
		this.panel8.Controls.Add(this.E_Btn_Next);
		this.panel8.Controls.Add(this.E_Btn_Prev);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel8.Location = new System.Drawing.Point(0, 493);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(664, 43);
		this.panel8.TabIndex = 13;
		this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox4.Location = new System.Drawing.Point(0, 0);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(664, 8);
		this.groupBox4.TabIndex = 4;
		this.groupBox4.TabStop = false;
		this.E_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance42.Image = resources.GetObject("appearance42.Image");
		appearance42.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.E_Btn_Cncl.Appearance = appearance42;
		this.E_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.E_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.E_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.E_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.E_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.E_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.E_Btn_Cncl.Location = new System.Drawing.Point(564, 9);
		this.E_Btn_Cncl.Name = "E_Btn_Cncl";
		this.E_Btn_Cncl.ShowFocusRect = false;
		this.E_Btn_Cncl.ShowOutline = false;
		this.E_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.E_Btn_Cncl.SupportThemes = false;
		this.E_Btn_Cncl.TabIndex = 2;
		this.E_Btn_Cncl.Text = "取消";
		this.E_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance43.Image = resources.GetObject("appearance43.Image");
		appearance43.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance43.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.E_Btn_Next.Appearance = appearance43;
		this.E_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.E_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.E_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.E_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.E_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.E_Btn_Next.Location = new System.Drawing.Point(472, 9);
		this.E_Btn_Next.Name = "E_Btn_Next";
		this.E_Btn_Next.ShowFocusRect = false;
		this.E_Btn_Next.ShowOutline = false;
		this.E_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.E_Btn_Next.SupportThemes = false;
		this.E_Btn_Next.TabIndex = 1;
		this.E_Btn_Next.Text = "下一步";
		this.E_Btn_Next.Click += new System.EventHandler(E_Btn_Next_Click);
		this.E_Btn_Prev.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance44.Image = resources.GetObject("appearance44.Image");
		appearance44.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.E_Btn_Prev.Appearance = appearance44;
		this.E_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.E_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.E_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.E_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.E_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.E_Btn_Prev.Location = new System.Drawing.Point(380, 9);
		this.E_Btn_Prev.Name = "E_Btn_Prev";
		this.E_Btn_Prev.ShowFocusRect = false;
		this.E_Btn_Prev.ShowOutline = false;
		this.E_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.E_Btn_Prev.SupportThemes = false;
		this.E_Btn_Prev.TabIndex = 0;
		this.E_Btn_Prev.Text = "上一步";
		this.E_Btn_Prev.Click += new System.EventHandler(E_Btn_Prev_Click);
		this.panel7.BackColor = System.Drawing.Color.White;
		this.panel7.Controls.Add(this.ultraLabel15);
		this.panel7.Controls.Add(this.ultraLabel16);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel7.Location = new System.Drawing.Point(0, 0);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(664, 60);
		this.panel7.TabIndex = 12;
		appearance45.BackColor = System.Drawing.Color.White;
		this.ultraLabel15.Appearance = appearance45;
		this.ultraLabel15.Location = new System.Drawing.Point(47, 34);
		this.ultraLabel15.Name = "ultraLabel15";
		this.ultraLabel15.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel15.TabIndex = 3;
		this.ultraLabel15.Text = "您可以挑選欲轉入的電子標單";
		appearance46.BackColor = System.Drawing.Color.White;
		this.ultraLabel16.Appearance = appearance46;
		this.ultraLabel16.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel16.Location = new System.Drawing.Point(16, 12);
		this.ultraLabel16.Name = "ultraLabel16";
		this.ultraLabel16.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel16.TabIndex = 2;
		this.ultraLabel16.Text = "電子標單挑選";
		this.Tab_F.Controls.Add(this.ultraLabel14);
		this.Tab_F.Controls.Add(this.ultraLabel13);
		this.Tab_F.Controls.Add(this.ultraLabel12);
		this.Tab_F.Controls.Add(this.panel6);
		this.Tab_F.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_F.Name = "Tab_F";
		this.Tab_F.Size = new System.Drawing.Size(664, 536);
		appearance47.BackColor = System.Drawing.Color.White;
		this.ultraLabel14.Appearance = appearance47;
		this.ultraLabel14.Location = new System.Drawing.Point(40, 148);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel14.TabIndex = 12;
		this.ultraLabel14.Text = "若要結束精靈，請按一下[完成]。";
		appearance48.BackColor = System.Drawing.Color.White;
		this.ultraLabel13.Appearance = appearance48;
		this.ultraLabel13.Location = new System.Drawing.Point(40, 88);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel13.TabIndex = 11;
		this.ultraLabel13.Text = "你已經成功建立一個新的專案。";
		appearance49.BackColor = System.Drawing.Color.White;
		this.ultraLabel12.Appearance = appearance49;
		this.ultraLabel12.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel12.Location = new System.Drawing.Point(28, 36);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel12.TabIndex = 10;
		this.ultraLabel12.Text = "恭禧您!";
		this.panel6.AutoSize = true;
		this.panel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panel6.Controls.Add(this.groupBox3);
		this.panel6.Controls.Add(this.F_Btn_Fnsh);
		this.panel6.Controls.Add(this.F_Btn_Prev);
		this.panel6.Location = new System.Drawing.Point(0, 492);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(664, 43);
		this.panel6.TabIndex = 9;
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox3.Location = new System.Drawing.Point(0, 0);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(664, 8);
		this.groupBox3.TabIndex = 3;
		this.groupBox3.TabStop = false;
		this.F_Btn_Fnsh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance50.Image = resources.GetObject("appearance50.Image");
		appearance50.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.F_Btn_Fnsh.Appearance = appearance50;
		this.F_Btn_Fnsh.BackColor = System.Drawing.SystemColors.Control;
		this.F_Btn_Fnsh.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.F_Btn_Fnsh.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.F_Btn_Fnsh.Font = new System.Drawing.Font("細明體", 11f);
		this.F_Btn_Fnsh.ImageSize = new System.Drawing.Size(20, 20);
		this.F_Btn_Fnsh.ImageTransparentColor = System.Drawing.Color.White;
		this.F_Btn_Fnsh.Location = new System.Drawing.Point(472, 9);
		this.F_Btn_Fnsh.Name = "F_Btn_Fnsh";
		this.F_Btn_Fnsh.ShowFocusRect = false;
		this.F_Btn_Fnsh.ShowOutline = false;
		this.F_Btn_Fnsh.Size = new System.Drawing.Size(88, 31);
		this.F_Btn_Fnsh.SupportThemes = false;
		this.F_Btn_Fnsh.TabIndex = 1;
		this.F_Btn_Fnsh.Text = "完成";
		this.F_Btn_Fnsh.Click += new System.EventHandler(F_Btn_Fnsh_Click);
		this.F_Btn_Prev.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance51.Image = resources.GetObject("appearance51.Image");
		appearance51.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.F_Btn_Prev.Appearance = appearance51;
		this.F_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.F_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.F_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.F_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.F_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.F_Btn_Prev.Location = new System.Drawing.Point(380, 9);
		this.F_Btn_Prev.Name = "F_Btn_Prev";
		this.F_Btn_Prev.ShowFocusRect = false;
		this.F_Btn_Prev.ShowOutline = false;
		this.F_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.F_Btn_Prev.SupportThemes = false;
		this.F_Btn_Prev.TabIndex = 0;
		this.F_Btn_Prev.Text = "上一步";
		this.F_Btn_Prev.Click += new System.EventHandler(F_Btn_Prev_Click);
		this.Tab_G.Controls.Add(this.panel20);
		this.Tab_G.Controls.Add(this.panel19);
		this.Tab_G.Location = new System.Drawing.Point(0, 0);
		this.Tab_G.Name = "Tab_G";
		this.Tab_G.Size = new System.Drawing.Size(664, 536);
		this.panel20.Controls.Add(this.panel21);
		this.panel20.Controls.Add(this.BtnChgDirG);
		this.panel20.Controls.Add(this.txtExcelin);
		this.panel20.Controls.Add(this.ultraLabel30);
		this.panel20.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel20.Location = new System.Drawing.Point(0, 60);
		this.panel20.Name = "panel20";
		this.panel20.Size = new System.Drawing.Size(664, 476);
		this.panel20.TabIndex = 15;
		this.panel21.AutoSize = true;
		this.panel21.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panel21.Controls.Add(this.groupBox7);
		this.panel21.Controls.Add(this.G_Btn_Cncl);
		this.panel21.Controls.Add(this.G_Btn_Next);
		this.panel21.Controls.Add(this.G_Btn_Prev);
		this.panel21.Location = new System.Drawing.Point(0, 432);
		this.panel21.Name = "panel21";
		this.panel21.Size = new System.Drawing.Size(664, 43);
		this.panel21.TabIndex = 14;
		this.groupBox7.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox7.Location = new System.Drawing.Point(0, 0);
		this.groupBox7.Name = "groupBox7";
		this.groupBox7.Size = new System.Drawing.Size(664, 8);
		this.groupBox7.TabIndex = 4;
		this.groupBox7.TabStop = false;
		this.G_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance52.Image = resources.GetObject("appearance52.Image");
		appearance52.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.G_Btn_Cncl.Appearance = appearance52;
		this.G_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.G_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.G_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.G_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.G_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.G_Btn_Cncl.Location = new System.Drawing.Point(564, 9);
		this.G_Btn_Cncl.Name = "G_Btn_Cncl";
		this.G_Btn_Cncl.ShowFocusRect = false;
		this.G_Btn_Cncl.ShowOutline = false;
		this.G_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.G_Btn_Cncl.SupportThemes = false;
		this.G_Btn_Cncl.TabIndex = 2;
		this.G_Btn_Cncl.Text = "取消";
		this.G_Btn_Cncl.Click += new System.EventHandler(G_Btn_Cncl_Click);
		this.G_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance53.Image = resources.GetObject("appearance53.Image");
		appearance53.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance53.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.G_Btn_Next.Appearance = appearance53;
		this.G_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.G_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.G_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.G_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.G_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.G_Btn_Next.Location = new System.Drawing.Point(472, 9);
		this.G_Btn_Next.Name = "G_Btn_Next";
		this.G_Btn_Next.ShowFocusRect = false;
		this.G_Btn_Next.ShowOutline = false;
		this.G_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.G_Btn_Next.SupportThemes = false;
		this.G_Btn_Next.TabIndex = 1;
		this.G_Btn_Next.Text = "下一步";
		this.G_Btn_Next.Click += new System.EventHandler(G_Btn_Next_Click);
		this.G_Btn_Prev.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance54.Image = resources.GetObject("appearance54.Image");
		appearance54.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.G_Btn_Prev.Appearance = appearance54;
		this.G_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.G_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.G_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.G_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.G_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.G_Btn_Prev.Location = new System.Drawing.Point(380, 9);
		this.G_Btn_Prev.Name = "G_Btn_Prev";
		this.G_Btn_Prev.ShowFocusRect = false;
		this.G_Btn_Prev.ShowOutline = false;
		this.G_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.G_Btn_Prev.SupportThemes = false;
		this.G_Btn_Prev.TabIndex = 0;
		this.G_Btn_Prev.Text = "上一步";
		appearance55.FontData.Name = "Arial";
		appearance55.FontData.SizeInPoints = 8f;
		this.BtnChgDirG.Appearance = appearance55;
		this.BtnChgDirG.BackColor = System.Drawing.SystemColors.Control;
		this.BtnChgDirG.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.BtnChgDirG.Location = new System.Drawing.Point(574, 65);
		this.BtnChgDirG.Name = "BtnChgDirG";
		this.BtnChgDirG.ShowFocusRect = false;
		this.BtnChgDirG.ShowOutline = false;
		this.BtnChgDirG.Size = new System.Drawing.Size(48, 24);
		this.BtnChgDirG.SupportThemes = false;
		this.BtnChgDirG.TabIndex = 10;
		this.BtnChgDirG.Text = "瀏覽...";
		this.BtnChgDirG.Click += new System.EventHandler(BtnChgDirG_Click);
		appearance56.FontData.Name = "細明體";
		appearance56.FontData.SizeInPoints = 11f;
		this.txtExcelin.Appearance = appearance56;
		this.txtExcelin.AutoSize = true;
		this.txtExcelin.Location = new System.Drawing.Point(51, 66);
		this.txtExcelin.Name = "txtExcelin";
		this.txtExcelin.Size = new System.Drawing.Size(524, 24);
		this.txtExcelin.TabIndex = 6;
		this.ultraLabel30.Location = new System.Drawing.Point(48, 44);
		this.ultraLabel30.Name = "ultraLabel30";
		this.ultraLabel30.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel30.TabIndex = 5;
		this.ultraLabel30.Text = "欲轉入的預算書Excel檔:";
		this.panel19.BackColor = System.Drawing.Color.White;
		this.panel19.Controls.Add(this.ultraLabel28);
		this.panel19.Controls.Add(this.ultraLabel29);
		this.panel19.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel19.Location = new System.Drawing.Point(0, 0);
		this.panel19.Name = "panel19";
		this.panel19.Size = new System.Drawing.Size(664, 60);
		this.panel19.TabIndex = 13;
		appearance57.BackColor = System.Drawing.Color.White;
		this.ultraLabel28.Appearance = appearance57;
		this.ultraLabel28.Location = new System.Drawing.Point(47, 34);
		this.ultraLabel28.Name = "ultraLabel28";
		this.ultraLabel28.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel28.TabIndex = 3;
		this.ultraLabel28.Text = "您可以挑選欲轉入的預算書Excel檔";
		appearance58.BackColor = System.Drawing.Color.White;
		this.ultraLabel29.Appearance = appearance58;
		this.ultraLabel29.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel29.Location = new System.Drawing.Point(16, 12);
		this.ultraLabel29.Name = "ultraLabel29";
		this.ultraLabel29.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel29.TabIndex = 2;
		this.ultraLabel29.Text = "預算書Excel檔案挑選";
		this.Tab_H.Controls.Add(this.panel24);
		this.Tab_H.Controls.Add(this.panel23);
		this.Tab_H.Controls.Add(this.panel22);
		this.Tab_H.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_H.Name = "Tab_H";
		this.Tab_H.Size = new System.Drawing.Size(664, 536);
		this.panel24.Controls.Add(this.lblWait);
		this.panel24.Controls.Add(this.Prog1);
		this.panel24.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel24.Location = new System.Drawing.Point(0, 60);
		this.panel24.Name = "panel24";
		this.panel24.Size = new System.Drawing.Size(664, 432);
		this.panel24.TabIndex = 25;
		this.lblWait.Location = new System.Drawing.Point(28, 32);
		this.lblWait.Name = "lblWait";
		this.lblWait.Size = new System.Drawing.Size(476, 20);
		this.lblWait.TabIndex = 23;
		this.lblWait.Text = "正在準備轉入的資料，這個動作會花些時間，請稍候。";
		this.Prog1.Location = new System.Drawing.Point(28, 56);
		this.Prog1.Name = "Prog1";
		this.Prog1.Size = new System.Drawing.Size(608, 23);
		this.Prog1.SupportThemes = false;
		this.Prog1.TabIndex = 24;
		this.Prog1.Text = "[Formatted]";
		this.panel23.BackColor = System.Drawing.Color.White;
		this.panel23.Controls.Add(this.ultraLabel32);
		this.panel23.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel23.Location = new System.Drawing.Point(0, 0);
		this.panel23.Name = "panel23";
		this.panel23.Size = new System.Drawing.Size(664, 60);
		this.panel23.TabIndex = 22;
		appearance59.BackColor = System.Drawing.Color.White;
		this.ultraLabel32.Appearance = appearance59;
		this.ultraLabel32.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel32.Location = new System.Drawing.Point(16, 12);
		this.ultraLabel32.Name = "ultraLabel32";
		this.ultraLabel32.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel32.TabIndex = 2;
		this.ultraLabel32.Text = "預算書Excel轉入";
		this.panel22.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel22.Controls.Add(this.groupBox8);
		this.panel22.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel22.Location = new System.Drawing.Point(0, 492);
		this.panel22.Name = "panel22";
		this.panel22.Size = new System.Drawing.Size(664, 44);
		this.panel22.TabIndex = 21;
		this.groupBox8.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox8.Location = new System.Drawing.Point(0, 0);
		this.groupBox8.Name = "groupBox8";
		this.groupBox8.Size = new System.Drawing.Size(664, 8);
		this.groupBox8.TabIndex = 3;
		this.groupBox8.TabStop = false;
		this.Tab_I.Controls.Add(this.panel25);
		this.Tab_I.Controls.Add(this.ultraLabel35);
		this.Tab_I.Controls.Add(this.ultraLabel36);
		this.Tab_I.Controls.Add(this.ultraLabel37);
		this.Tab_I.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_I.Name = "Tab_I";
		this.Tab_I.Size = new System.Drawing.Size(664, 536);
		this.panel25.AutoSize = true;
		this.panel25.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panel25.Controls.Add(this.groupBox9);
		this.panel25.Controls.Add(this.ultraButton3);
		this.panel25.Location = new System.Drawing.Point(0, 492);
		this.panel25.Name = "panel25";
		this.panel25.Size = new System.Drawing.Size(664, 43);
		this.panel25.TabIndex = 16;
		this.groupBox9.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox9.Location = new System.Drawing.Point(0, 0);
		this.groupBox9.Name = "groupBox9";
		this.groupBox9.Size = new System.Drawing.Size(664, 8);
		this.groupBox9.TabIndex = 3;
		this.groupBox9.TabStop = false;
		this.ultraButton3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance60.Image = resources.GetObject("appearance60.Image");
		appearance60.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton3.Appearance = appearance60;
		this.ultraButton3.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton3.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton3.DialogResult = System.Windows.Forms.DialogResult.Abort;
		this.ultraButton3.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton3.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton3.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton3.Location = new System.Drawing.Point(472, 9);
		this.ultraButton3.Name = "ultraButton3";
		this.ultraButton3.ShowFocusRect = false;
		this.ultraButton3.ShowOutline = false;
		this.ultraButton3.Size = new System.Drawing.Size(88, 31);
		this.ultraButton3.SupportThemes = false;
		this.ultraButton3.TabIndex = 1;
		this.ultraButton3.Text = "確定";
		appearance61.BackColor = System.Drawing.Color.White;
		this.ultraLabel35.Appearance = appearance61;
		this.ultraLabel35.Location = new System.Drawing.Point(36, 152);
		this.ultraLabel35.Name = "ultraLabel35";
		this.ultraLabel35.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel35.TabIndex = 15;
		this.ultraLabel35.Text = "請先檢查欲轉入的檔案格式正確，之後再重新轉入一次。";
		appearance62.BackColor = System.Drawing.Color.White;
		this.ultraLabel36.Appearance = appearance62;
		this.ultraLabel36.Location = new System.Drawing.Point(36, 92);
		this.ultraLabel36.Name = "ultraLabel36";
		this.ultraLabel36.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel36.TabIndex = 14;
		this.ultraLabel36.Text = "剛才的轉入動作失敗。";
		appearance63.BackColor = System.Drawing.Color.White;
		appearance63.ForeColor = System.Drawing.Color.Red;
		this.ultraLabel37.Appearance = appearance63;
		this.ultraLabel37.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel37.Location = new System.Drawing.Point(24, 40);
		this.ultraLabel37.Name = "ultraLabel37";
		this.ultraLabel37.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel37.TabIndex = 13;
		this.ultraLabel37.Text = "失敗";
		this.Tab_J.Controls.Add(this.GridRail1);
		this.Tab_J.Controls.Add(this.panel27);
		this.Tab_J.Controls.Add(this.panel26);
		this.Tab_J.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_J.Name = "Tab_J";
		this.Tab_J.Size = new System.Drawing.Size(664, 536);
		this.GridRail1._ExcelFileName = "";
		this.GridRail1._ExcelSheeName = "";
		this.GridRail1._IsOpenExcelAfterExport = false;
		this.GridRail1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.GridRail1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridRail1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.GridRail1.ColumnInfo = resources.GetString("GridRail1.ColumnInfo");
		this.GridRail1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridRail1.ExtendLastCol = true;
		this.GridRail1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridRail1.ForeColor = System.Drawing.Color.Black;
		this.GridRail1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.GridRail1.IsProcessUndo = false;
		this.GridRail1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.GridRail1.Location = new System.Drawing.Point(0, 228);
		this.GridRail1.Name = "GridRail1";
		this.GridRail1.Rows.Count = 1;
		this.GridRail1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.GridRail1.ShowCursor = true;
		this.GridRail1.ShowToolTipOnNarrowColumn = true;
		this.GridRail1.Size = new System.Drawing.Size(664, 264);
		this.GridRail1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("GridRail1.Styles"));
		this.GridRail1.TabIndex = 24;
		this.GridRail1.UndoMax = 10;
		this.GridRail1.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(GridRail1_AfterSelChange);
		this.panel27.BackColor = System.Drawing.Color.White;
		this.panel27.Controls.Add(this.ultraLabel41);
		this.panel27.Controls.Add(this.pictureBox3);
		this.panel27.Controls.Add(this.ultraButton5);
		this.panel27.Controls.Add(this.ultraLabel40);
		this.panel27.Controls.Add(this.pictureBox2);
		this.panel27.Controls.Add(this.btnJ_LoadEXCEL);
		this.panel27.Controls.Add(this.txtAA);
		this.panel27.Controls.Add(this.pictureBox1);
		this.panel27.Controls.Add(this.ultraLabel39);
		this.panel27.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel27.Location = new System.Drawing.Point(0, 0);
		this.panel27.Name = "panel27";
		this.panel27.Size = new System.Drawing.Size(664, 228);
		this.panel27.TabIndex = 23;
		appearance64.BackColor = System.Drawing.Color.White;
		this.ultraLabel41.Appearance = appearance64;
		this.ultraLabel41.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel41.Location = new System.Drawing.Point(48, 197);
		this.ultraLabel41.Name = "ultraLabel41";
		this.ultraLabel41.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel41.TabIndex = 19;
		this.ultraLabel41.Text = "點選你要使用的動支單號";
		this.pictureBox3.Image = (System.Drawing.Image)resources.GetObject("pictureBox3.Image");
		this.pictureBox3.Location = new System.Drawing.Point(8, 185);
		this.pictureBox3.Name = "pictureBox3";
		this.pictureBox3.Size = new System.Drawing.Size(40, 36);
		this.pictureBox3.TabIndex = 18;
		this.pictureBox3.TabStop = false;
		this.ultraButton5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance65.Image = resources.GetObject("appearance65.Image");
		appearance65.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance65.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton5.Appearance = appearance65;
		this.ultraButton5.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton5.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton5.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton5.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton5.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton5.Location = new System.Drawing.Point(48, 128);
		this.ultraButton5.Name = "ultraButton5";
		this.ultraButton5.ShowFocusRect = false;
		this.ultraButton5.ShowOutline = false;
		this.ultraButton5.Size = new System.Drawing.Size(88, 31);
		this.ultraButton5.SupportThemes = false;
		this.ultraButton5.TabIndex = 17;
		this.ultraButton5.Text = "載入";
		this.ultraButton5.Click += new System.EventHandler(ultraButton5_Click);
		appearance66.BackColor = System.Drawing.Color.White;
		this.ultraLabel40.Appearance = appearance66;
		this.ultraLabel40.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel40.Location = new System.Drawing.Point(48, 104);
		this.ultraLabel40.Name = "ultraLabel40";
		this.ultraLabel40.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel40.TabIndex = 16;
		this.ultraLabel40.Text = "執行載入動支單號檔";
		this.pictureBox2.Image = (System.Drawing.Image)resources.GetObject("pictureBox2.Image");
		this.pictureBox2.Location = new System.Drawing.Point(8, 96);
		this.pictureBox2.Name = "pictureBox2";
		this.pictureBox2.Size = new System.Drawing.Size(40, 36);
		this.pictureBox2.TabIndex = 15;
		this.pictureBox2.TabStop = false;
		appearance67.FontData.Name = "Arial";
		appearance67.FontData.SizeInPoints = 8f;
		this.btnJ_LoadEXCEL.Appearance = appearance67;
		this.btnJ_LoadEXCEL.BackColor = System.Drawing.SystemColors.Control;
		this.btnJ_LoadEXCEL.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.btnJ_LoadEXCEL.Location = new System.Drawing.Point(599, 39);
		this.btnJ_LoadEXCEL.Name = "btnJ_LoadEXCEL";
		this.btnJ_LoadEXCEL.ShowFocusRect = false;
		this.btnJ_LoadEXCEL.ShowOutline = false;
		this.btnJ_LoadEXCEL.Size = new System.Drawing.Size(48, 24);
		this.btnJ_LoadEXCEL.SupportThemes = false;
		this.btnJ_LoadEXCEL.TabIndex = 14;
		this.btnJ_LoadEXCEL.Text = "瀏覽...";
		this.btnJ_LoadEXCEL.Click += new System.EventHandler(btnJ_LoadEXCEL_Click);
		appearance68.FontData.Name = "細明體";
		this.txtAA.Appearance = appearance68;
		this.txtAA.AutoSize = true;
		this.txtAA.Location = new System.Drawing.Point(49, 40);
		this.txtAA.Name = "txtAA";
		this.txtAA.Size = new System.Drawing.Size(551, 21);
		this.txtAA.TabIndex = 13;
		this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		this.pictureBox1.Location = new System.Drawing.Point(8, 8);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(40, 36);
		this.pictureBox1.TabIndex = 12;
		this.pictureBox1.TabStop = false;
		appearance69.BackColor = System.Drawing.Color.White;
		this.ultraLabel39.Appearance = appearance69;
		this.ultraLabel39.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel39.Location = new System.Drawing.Point(48, 15);
		this.ultraLabel39.Name = "ultraLabel39";
		this.ultraLabel39.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel39.TabIndex = 2;
		this.ultraLabel39.Text = "請挑選AA系統轉出之動支單號檔案";
		this.panel26.Controls.Add(this.groupBox10);
		this.panel26.Controls.Add(this.J_Btn_Cncl);
		this.panel26.Controls.Add(this.J_Btn_Next);
		this.panel26.Controls.Add(this.J_Btn_Prev);
		this.panel26.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel26.Location = new System.Drawing.Point(0, 492);
		this.panel26.Name = "panel26";
		this.panel26.Size = new System.Drawing.Size(664, 44);
		this.panel26.TabIndex = 10;
		this.groupBox10.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox10.Location = new System.Drawing.Point(0, 0);
		this.groupBox10.Name = "groupBox10";
		this.groupBox10.Size = new System.Drawing.Size(664, 8);
		this.groupBox10.TabIndex = 4;
		this.groupBox10.TabStop = false;
		this.J_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance70.Image = resources.GetObject("appearance70.Image");
		appearance70.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.J_Btn_Cncl.Appearance = appearance70;
		this.J_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.J_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.J_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.J_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.J_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.J_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.J_Btn_Cncl.Location = new System.Drawing.Point(564, 9);
		this.J_Btn_Cncl.Name = "J_Btn_Cncl";
		this.J_Btn_Cncl.ShowFocusRect = false;
		this.J_Btn_Cncl.ShowOutline = false;
		this.J_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.J_Btn_Cncl.SupportThemes = false;
		this.J_Btn_Cncl.TabIndex = 2;
		this.J_Btn_Cncl.Text = "取消";
		this.J_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance71.Image = resources.GetObject("appearance71.Image");
		appearance71.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance71.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.J_Btn_Next.Appearance = appearance71;
		this.J_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.J_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.J_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.J_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.J_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.J_Btn_Next.Location = new System.Drawing.Point(472, 9);
		this.J_Btn_Next.Name = "J_Btn_Next";
		this.J_Btn_Next.ShowFocusRect = false;
		this.J_Btn_Next.ShowOutline = false;
		this.J_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.J_Btn_Next.SupportThemes = false;
		this.J_Btn_Next.TabIndex = 1;
		this.J_Btn_Next.Text = "下一步";
		this.J_Btn_Next.Click += new System.EventHandler(J_Btn_Next_Click);
		this.J_Btn_Prev.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance72.Image = resources.GetObject("appearance72.Image");
		appearance72.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.J_Btn_Prev.Appearance = appearance72;
		this.J_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.J_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.J_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.J_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.J_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.J_Btn_Prev.Location = new System.Drawing.Point(380, 9);
		this.J_Btn_Prev.Name = "J_Btn_Prev";
		this.J_Btn_Prev.ShowFocusRect = false;
		this.J_Btn_Prev.ShowOutline = false;
		this.J_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.J_Btn_Prev.SupportThemes = false;
		this.J_Btn_Prev.TabIndex = 0;
		this.J_Btn_Prev.Text = "上一步";
		this.J_Btn_Prev.Click += new System.EventHandler(J_Btn_Prev_Click);
		this.Tab_K.Controls.Add(this.panel33);
		this.Tab_K.Controls.Add(this.panel34);
		this.Tab_K.Controls.Add(this.panel28);
		this.Tab_K.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_K.Name = "Tab_K";
		this.Tab_K.Size = new System.Drawing.Size(664, 536);
		this.panel33.Controls.Add(this.panel32);
		this.panel33.Controls.Add(this.panel29);
		this.panel33.Controls.Add(this.panel30);
		this.panel33.Controls.Add(this.panel35);
		this.panel33.Controls.Add(this.panel36);
		this.panel33.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel33.Location = new System.Drawing.Point(0, 40);
		this.panel33.Name = "panel33";
		this.panel33.Size = new System.Drawing.Size(664, 452);
		this.panel33.TabIndex = 13;
		this.panel32.Controls.Add(this.ultraButton7);
		this.panel32.Controls.Add(this.ultraButton8);
		this.panel32.Controls.Add(this.ultraButton9);
		this.panel32.Controls.Add(this.BtnAll);
		this.panel32.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel32.Location = new System.Drawing.Point(284, 0);
		this.panel32.Name = "panel32";
		this.panel32.Size = new System.Drawing.Size(94, 452);
		this.panel32.TabIndex = 12;
		appearance73.FontData.SizeInPoints = 9f;
		this.ultraButton7.Appearance = appearance73;
		this.ultraButton7.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton7.Location = new System.Drawing.Point(7, 203);
		this.ultraButton7.Name = "ultraButton7";
		this.ultraButton7.ShowFocusRect = false;
		this.ultraButton7.ShowOutline = false;
		this.ultraButton7.Size = new System.Drawing.Size(80, 30);
		this.ultraButton7.SupportThemes = false;
		this.ultraButton7.TabIndex = 4;
		this.ultraButton7.Text = "< 移除";
		this.ultraButton7.Click += new System.EventHandler(ultraButton7_Click);
		appearance74.FontData.SizeInPoints = 9f;
		this.ultraButton8.Appearance = appearance74;
		this.ultraButton8.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton8.Location = new System.Drawing.Point(7, 168);
		this.ultraButton8.Name = "ultraButton8";
		this.ultraButton8.ShowFocusRect = false;
		this.ultraButton8.ShowOutline = false;
		this.ultraButton8.Size = new System.Drawing.Size(80, 30);
		this.ultraButton8.SupportThemes = false;
		this.ultraButton8.TabIndex = 3;
		this.ultraButton8.Text = "選取 >";
		this.ultraButton8.Click += new System.EventHandler(ultraButton8_Click);
		appearance75.FontData.SizeInPoints = 9f;
		this.ultraButton9.Appearance = appearance75;
		this.ultraButton9.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton9.Location = new System.Drawing.Point(7, 240);
		this.ultraButton9.Name = "ultraButton9";
		this.ultraButton9.ShowFocusRect = false;
		this.ultraButton9.ShowOutline = false;
		this.ultraButton9.Size = new System.Drawing.Size(80, 30);
		this.ultraButton9.SupportThemes = false;
		this.ultraButton9.TabIndex = 1;
		this.ultraButton9.Text = "<< 全部移除";
		this.ultraButton9.Click += new System.EventHandler(ultraButton9_Click);
		appearance76.FontData.SizeInPoints = 9f;
		this.BtnAll.Appearance = appearance76;
		this.BtnAll.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnAll.Location = new System.Drawing.Point(7, 132);
		this.BtnAll.Name = "BtnAll";
		this.BtnAll.ShowFocusRect = false;
		this.BtnAll.ShowOutline = false;
		this.BtnAll.Size = new System.Drawing.Size(80, 30);
		this.BtnAll.SupportThemes = false;
		this.BtnAll.TabIndex = 0;
		this.BtnAll.Text = "全選 >>";
		this.BtnAll.Click += new System.EventHandler(BtnAll_Click);
		this.panel29.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel29.Controls.Add(this.GridSource);
		this.panel29.Controls.Add(this.ultraLabel44);
		this.panel29.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel29.Location = new System.Drawing.Point(12, 0);
		this.panel29.Name = "panel29";
		this.panel29.Size = new System.Drawing.Size(272, 452);
		this.panel29.TabIndex = 10;
		this.GridSource._ExcelFileName = "";
		this.GridSource._ExcelSheeName = "";
		this.GridSource._IsOpenExcelAfterExport = false;
		this.GridSource.AllowEditing = false;
		this.GridSource.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridSource.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.GridSource.ColumnInfo = resources.GetString("GridSource.ColumnInfo");
		this.GridSource.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridSource.ExtendLastCol = true;
		this.GridSource.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
		this.GridSource.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridSource.ForeColor = System.Drawing.Color.Black;
		this.GridSource.Location = new System.Drawing.Point(0, 28);
		this.GridSource.Name = "GridSource";
		this.GridSource.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.GridSource.ShowCursor = true;
		this.GridSource.ShowToolTipOnNarrowColumn = true;
		this.GridSource.Size = new System.Drawing.Size(270, 422);
		this.GridSource.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("GridSource.Styles"));
		this.GridSource.TabIndex = 1;
		this.GridSource.Tree.Column = 1;
		this.GridSource.Tree.LineColor = System.Drawing.Color.Gray;
		appearance77.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance77.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance77.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel44.Appearance = appearance77;
		this.ultraLabel44.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel44.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel44.Name = "ultraLabel44";
		this.ultraLabel44.Size = new System.Drawing.Size(270, 28);
		this.ultraLabel44.TabIndex = 2;
		this.ultraLabel44.Text = "專案列表";
		this.panel30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel30.Controls.Add(this.GridDestination);
		this.panel30.Controls.Add(this.panel31);
		this.panel30.Controls.Add(this.ultraLabel45);
		this.panel30.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel30.Location = new System.Drawing.Point(378, 0);
		this.panel30.Name = "panel30";
		this.panel30.Size = new System.Drawing.Size(274, 452);
		this.panel30.TabIndex = 11;
		this.GridDestination._ExcelFileName = "";
		this.GridDestination._ExcelSheeName = "";
		this.GridDestination._IsOpenExcelAfterExport = false;
		this.GridDestination.AllowEditing = false;
		this.GridDestination.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridDestination.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.GridDestination.ColumnInfo = resources.GetString("GridDestination.ColumnInfo");
		this.GridDestination.ExtendLastCol = true;
		this.GridDestination.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
		this.GridDestination.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridDestination.ForeColor = System.Drawing.Color.Black;
		this.GridDestination.Location = new System.Drawing.Point(0, 28);
		this.GridDestination.Name = "GridDestination";
		this.GridDestination.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.GridDestination.ShowCursor = true;
		this.GridDestination.ShowToolTipOnNarrowColumn = true;
		this.GridDestination.Size = new System.Drawing.Size(272, 390);
		this.GridDestination.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("GridDestination.Styles"));
		this.GridDestination.TabIndex = 5;
		this.GridDestination.Tree.Column = 1;
		this.GridDestination.Tree.LineColor = System.Drawing.Color.Gray;
		this.panel31.Controls.Add(this.ultraButton4);
		this.panel31.Controls.Add(this.ultraButton6);
		this.panel31.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel31.Location = new System.Drawing.Point(0, 418);
		this.panel31.Name = "panel31";
		this.panel31.Size = new System.Drawing.Size(272, 32);
		this.panel31.TabIndex = 4;
		appearance78.FontData.SizeInPoints = 9f;
		appearance78.Image = resources.GetObject("appearance78.Image");
		appearance78.ImageVAlign = Infragistics.Win.VAlign.Middle;
		appearance78.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraButton4.Appearance = appearance78;
		this.ultraButton4.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.ultraButton4.Location = new System.Drawing.Point(175, 2);
		this.ultraButton4.Name = "ultraButton4";
		this.ultraButton4.ShowFocusRect = false;
		this.ultraButton4.ShowOutline = false;
		this.ultraButton4.Size = new System.Drawing.Size(60, 28);
		this.ultraButton4.SupportThemes = false;
		this.ultraButton4.TabIndex = 7;
		this.ultraButton4.Text = "下移";
		this.ultraButton4.Click += new System.EventHandler(ultraButton4_Click);
		appearance79.FontData.SizeInPoints = 9f;
		appearance79.Image = resources.GetObject("appearance79.Image");
		appearance79.ImageVAlign = Infragistics.Win.VAlign.Middle;
		appearance79.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraButton6.Appearance = appearance79;
		this.ultraButton6.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.ultraButton6.Location = new System.Drawing.Point(107, 2);
		this.ultraButton6.Name = "ultraButton6";
		this.ultraButton6.ShowFocusRect = false;
		this.ultraButton6.ShowOutline = false;
		this.ultraButton6.Size = new System.Drawing.Size(60, 28);
		this.ultraButton6.SupportThemes = false;
		this.ultraButton6.TabIndex = 6;
		this.ultraButton6.Text = "上移";
		this.ultraButton6.Click += new System.EventHandler(ultraButton6_Click);
		appearance80.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance80.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance80.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel45.Appearance = appearance80;
		this.ultraLabel45.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.ultraLabel45.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel45.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel45.Name = "ultraLabel45";
		this.ultraLabel45.Size = new System.Drawing.Size(272, 28);
		this.ultraLabel45.TabIndex = 3;
		this.ultraLabel45.Text = "已選取的專案";
		this.panel35.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel35.Location = new System.Drawing.Point(0, 0);
		this.panel35.Name = "panel35";
		this.panel35.Size = new System.Drawing.Size(12, 452);
		this.panel35.TabIndex = 13;
		this.panel36.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel36.Location = new System.Drawing.Point(652, 0);
		this.panel36.Name = "panel36";
		this.panel36.Size = new System.Drawing.Size(12, 452);
		this.panel36.TabIndex = 14;
		this.panel34.Controls.Add(this.ultraLabel47);
		this.panel34.Controls.Add(this.groupBox11);
		this.panel34.Controls.Add(this.ultraButton10);
		this.panel34.Controls.Add(this.ultraButton11);
		this.panel34.Controls.Add(this.ultraButton12);
		this.panel34.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel34.Location = new System.Drawing.Point(0, 492);
		this.panel34.Name = "panel34";
		this.panel34.Size = new System.Drawing.Size(664, 44);
		this.panel34.TabIndex = 14;
		appearance81.ForeColor = System.Drawing.Color.FromArgb(0, 51, 153);
		this.ultraLabel47.Appearance = appearance81;
		this.ultraLabel47.Font = new System.Drawing.Font("細明體", 9.25f);
		this.ultraLabel47.Location = new System.Drawing.Point(8, 8);
		this.ultraLabel47.Name = "ultraLabel47";
		this.ultraLabel47.Size = new System.Drawing.Size(368, 32);
		this.ultraLabel47.TabIndex = 7;
		this.ultraLabel47.Text = "合併專案時，當工項代碼有重覆時會依挑選專案的順序，以先挑選的專案取代後挑的專案中的單位、單價、單價分析等資料！";
		this.groupBox11.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox11.Location = new System.Drawing.Point(0, 0);
		this.groupBox11.Name = "groupBox11";
		this.groupBox11.Size = new System.Drawing.Size(664, 8);
		this.groupBox11.TabIndex = 4;
		this.groupBox11.TabStop = false;
		this.ultraButton10.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance82.Image = resources.GetObject("appearance82.Image");
		appearance82.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton10.Appearance = appearance82;
		this.ultraButton10.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton10.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton10.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton10.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton10.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton10.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton10.Location = new System.Drawing.Point(564, 9);
		this.ultraButton10.Name = "ultraButton10";
		this.ultraButton10.ShowFocusRect = false;
		this.ultraButton10.ShowOutline = false;
		this.ultraButton10.Size = new System.Drawing.Size(88, 31);
		this.ultraButton10.SupportThemes = false;
		this.ultraButton10.TabIndex = 2;
		this.ultraButton10.Text = "取消";
		this.ultraButton11.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance83.Image = resources.GetObject("appearance83.Image");
		appearance83.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance83.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton11.Appearance = appearance83;
		this.ultraButton11.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton11.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton11.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton11.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton11.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton11.Location = new System.Drawing.Point(472, 9);
		this.ultraButton11.Name = "ultraButton11";
		this.ultraButton11.ShowFocusRect = false;
		this.ultraButton11.ShowOutline = false;
		this.ultraButton11.Size = new System.Drawing.Size(88, 31);
		this.ultraButton11.SupportThemes = false;
		this.ultraButton11.TabIndex = 1;
		this.ultraButton11.Text = "下一步";
		this.ultraButton11.Click += new System.EventHandler(ultraButton11_Click);
		this.ultraButton12.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance84.Image = resources.GetObject("appearance84.Image");
		appearance84.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton12.Appearance = appearance84;
		this.ultraButton12.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton12.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton12.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton12.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton12.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton12.Location = new System.Drawing.Point(380, 9);
		this.ultraButton12.Name = "ultraButton12";
		this.ultraButton12.ShowFocusRect = false;
		this.ultraButton12.ShowOutline = false;
		this.ultraButton12.Size = new System.Drawing.Size(88, 31);
		this.ultraButton12.SupportThemes = false;
		this.ultraButton12.TabIndex = 0;
		this.ultraButton12.Text = "上一步";
		this.ultraButton12.Click += new System.EventHandler(ultraButton12_Click);
		this.panel28.Controls.Add(this.ultraLabel43);
		this.panel28.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel28.Location = new System.Drawing.Point(0, 0);
		this.panel28.Name = "panel28";
		this.panel28.Size = new System.Drawing.Size(664, 40);
		this.panel28.TabIndex = 5;
		this.ultraLabel43.Location = new System.Drawing.Point(16, 16);
		this.ultraLabel43.Name = "ultraLabel43";
		this.ultraLabel43.Size = new System.Drawing.Size(152, 20);
		this.ultraLabel43.TabIndex = 1;
		this.ultraLabel43.Text = "挑選要合併的專案";
		appearance85.BackColor = System.Drawing.Color.White;
		this.WizardTabs.Appearance = appearance85;
		this.WizardTabs.Controls.Add(this.Tab_D);
		this.WizardTabs.Controls.Add(this.ultraTabSharedControlsPage1);
		this.WizardTabs.Controls.Add(this.Tab_A);
		this.WizardTabs.Controls.Add(this.Tab_B);
		this.WizardTabs.Controls.Add(this.Tab_C);
		this.WizardTabs.Controls.Add(this.Tab_E);
		this.WizardTabs.Controls.Add(this.Tab_F);
		this.WizardTabs.Controls.Add(this.Tab_G);
		this.WizardTabs.Controls.Add(this.Tab_H);
		this.WizardTabs.Controls.Add(this.Tab_I);
		this.WizardTabs.Controls.Add(this.Tab_J);
		this.WizardTabs.Controls.Add(this.Tab_K);
		this.WizardTabs.Dock = System.Windows.Forms.DockStyle.Fill;
		this.WizardTabs.Location = new System.Drawing.Point(0, 0);
		this.WizardTabs.Name = "WizardTabs";
		this.WizardTabs.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.WizardTabs.Size = new System.Drawing.Size(664, 536);
		this.WizardTabs.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.WizardTabs.TabIndex = 0;
		this.WizardTabs.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft;
		ultraTab1.Key = "Tab_A";
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "A";
		ultraTab2.Key = "Tab_B";
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "B";
		ultraTab3.Key = "Tab_C";
		ultraTab3.TabPage = this.Tab_C;
		ultraTab3.Text = "C";
		ultraTab4.Key = "Tab_D";
		ultraTab4.TabPage = this.Tab_D;
		ultraTab4.Text = "D";
		ultraTab5.Key = "Tab_E";
		ultraTab5.TabPage = this.Tab_E;
		ultraTab5.Text = "E";
		ultraTab6.Key = "Tab_F";
		ultraTab6.TabPage = this.Tab_F;
		ultraTab6.Text = "F";
		ultraTab7.Key = "Tab_G";
		ultraTab7.TabPage = this.Tab_G;
		ultraTab7.Text = "G";
		ultraTab8.Key = "Tab_H";
		ultraTab8.TabPage = this.Tab_H;
		ultraTab8.Text = "H";
		ultraTab9.Key = "Tab_I";
		ultraTab9.TabPage = this.Tab_I;
		ultraTab9.Text = "I";
		ultraTab10.Key = "Tab_J";
		ultraTab10.TabPage = this.Tab_J;
		ultraTab10.Text = "J";
		ultraTab11.Key = "Tab_K";
		ultraTab11.TabPage = this.Tab_K;
		ultraTab11.Text = "K";
		this.WizardTabs.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[11]
		{
			ultraTab1, ultraTab2, ultraTab3, ultraTab4, ultraTab5, ultraTab6, ultraTab7, ultraTab8, ultraTab9, ultraTab10,
			ultraTab11
		});
		this.WizardTabs.ActiveTabChanged += new Infragistics.Win.UltraWinTabControl.ActiveTabChangedEventHandler(WizardTabs_ActiveTabChanged);
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(664, 536);
		this.panel2.Controls.Add(this.WizardTabs);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(664, 536);
		this.panel2.TabIndex = 2;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.A_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(664, 536);
		base.Controls.Add(this.panel2);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "formNewProjectWizard";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "專案建立及轉入精靈";
		base.Load += new System.EventHandler(formNewProjectWizard_Load);
		base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(formNewProjectWizard_FormClosed);
		this.Tab_A.ResumeLayout(false);
		this.Tab_A.PerformLayout();
		this.panel1.ResumeLayout(false);
		this.Tab_B.ResumeLayout(false);
		this.Tab_B.PerformLayout();
		this.panel5.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtProjectMemo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtProjectCodeAlias).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtProjectAddress).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtProjectEName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtProjectCName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtProjectCode).EndInit();
		this.panel3.ResumeLayout(false);
		this.Tab_C.ResumeLayout(false);
		this.Tab_C.PerformLayout();
		this.panel11.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.cbFind).EndInit();
		this.panel13.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).EndInit();
		this.panel14.ResumeLayout(false);
		this.panel12.ResumeLayout(false);
		this.panel10.ResumeLayout(false);
		this.Tab_D.ResumeLayout(false);
		this.Tab_D.PerformLayout();
		this.panel18.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid2).EndInit();
		this.panel17.ResumeLayout(false);
		this.panel16.ResumeLayout(false);
		this.panel15.ResumeLayout(false);
		this.Tab_E.ResumeLayout(false);
		this.Tab_E.PerformLayout();
		this.panel9.ResumeLayout(false);
		this.panel9.PerformLayout();
		this.gpMessage.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtPxfin).EndInit();
		this.panel8.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		this.Tab_F.ResumeLayout(false);
		this.Tab_F.PerformLayout();
		this.panel6.ResumeLayout(false);
		this.Tab_G.ResumeLayout(false);
		this.panel20.ResumeLayout(false);
		this.panel20.PerformLayout();
		this.panel21.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtExcelin).EndInit();
		this.panel19.ResumeLayout(false);
		this.Tab_H.ResumeLayout(false);
		this.panel24.ResumeLayout(false);
		this.panel23.ResumeLayout(false);
		this.panel22.ResumeLayout(false);
		this.Tab_I.ResumeLayout(false);
		this.Tab_I.PerformLayout();
		this.panel25.ResumeLayout(false);
		this.Tab_J.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridRail1).EndInit();
		this.panel27.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBox3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAA).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		this.panel26.ResumeLayout(false);
		this.Tab_K.ResumeLayout(false);
		this.panel33.ResumeLayout(false);
		this.panel32.ResumeLayout(false);
		this.panel29.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridSource).EndInit();
		this.panel30.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridDestination).EndInit();
		this.panel31.ResumeLayout(false);
		this.panel34.ResumeLayout(false);
		this.panel28.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.WizardTabs).EndInit();
		this.WizardTabs.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
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

	public formNewProjectWizard()
	{
		InitializeComponent();
		F_PID = ConfigurationManager.AppSettings["PID"];
		string sHideCols = CommonMethods.GetDebugValue("formNewProjectWizard", "HideCols");
		HideCols(Convert.ToBoolean((sHideCols == "") ? "True" : sHideCols));
		GridCols = c1FlexGrid2.Cols.Count;
		GridColsSquence = new object[GridCols, 10];
	}

	private void A_Btn_Next_Click(object sender, EventArgs e)
	{
		switch (OptionSet)
		{
		case 1:
			if (!DBClass.ChkAuthority(F_UserID, "F00500010001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00500010001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				break;
			}
			Tab_B.Tab.Selected = true;
			txtProjectCode.Focus();
			break;
		case 2:
			if (!DBClass.ChkAuthority(F_UserID, "F00500010002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00500010002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Tab_E.Tab.Selected = true;
			}
			break;
		case 3:
			if (!DBClass.ChkAuthority(F_UserID, "F00500010003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00500010003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				break;
			}
			Tab_B.Tab.Selected = true;
			txtProjectCode.Focus();
			break;
		case 4:
			if (!DBClass.ChkAuthority(F_UserID, "F00500010004"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00500010004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				break;
			}
			MessageBox.Show(this, "預算書Excel DIY格式提供至2006年12月底", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			Tab_B.Tab.Selected = true;
			txtProjectCode.Focus();
			break;
		case 5:
			if (!DBClass.ChkAuthority(F_UserID, "F00500010005"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00500010005") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				break;
			}
			Tab_B.Tab.Selected = true;
			txtProjectCode.Focus();
			break;
		}
	}

	private void B_Btn_Next_Click(object sender, EventArgs e)
	{
		if (txtProjectCode.Text.Trim() == "")
		{
			MessageBox.Show(this, "[專案代號] 沒有填寫，請確認。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtProjectCode.Focus();
			return;
		}
		if (OptionSet == 1)
		{
			int iAction = InsertProjectToDB();
			if (iAction == -2)
			{
				MessageBox.Show(this, "已經有相同 [專案代號] 資料存在，\n請重新載入專案目錄。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
		}
		if (OptionSet == 3)
		{
			int iAction = InsertProjectToDB();
			if (iAction == -2)
			{
				MessageBox.Show(this, "已經有相同 [專案代號] 資料存在，\n請重新載入專案目錄。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Archnowledge.Pcces.DomainModule.General.PubProject pubProject = new Archnowledge.Pcces.DomainModule.General.PubProject();
			DataSet ds = pubProject.GetProjectList(F_UserID);
			DataView dv = ds.Tables[0].DefaultView;
			dv.RowFilter = "Bud IS NOT NULL";
			CellStyle CS8 = c1FlexGrid1.Styles.Add("NoProjectAuth");
			CS8.ForeColor = Color.Gray;
			c1FlexGrid1.Rows.Count = dv.Count;
			for (int i = 0; i < dv.Count; i++)
			{
				c1FlexGrid1[i, "ProjectCode"] = dv[i]["ProjectCode"].ToString().Trim();
				c1FlexGrid1[i, "projCName"] = dv[i]["projCName"].ToString().Trim();
				c1FlexGrid1[i, "projAddress"] = dv[i]["projAddress"].ToString().Trim();
				if (!ArchConvert.Obj2Bool(dv[i]["Auth"]))
				{
					c1FlexGrid1.Rows[i].Style = c1FlexGrid1.Styles["NoProjectAuth"];
				}
				c1FlexGrid1.AutoSizeRow(i);
			}
		}
		if (OptionSet == 4)
		{
			int iAction = InsertProjectToDB();
			if (iAction == -2)
			{
				MessageBox.Show(this, "已經有相同 [專案代號] 資料存在，\n請重新載入專案目錄。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
		}
		if (OptionSet == 5)
		{
			int iAction = InsertProjectToDB();
			if (iAction == -2)
			{
				MessageBox.Show(this, "已經有相同 [專案代號] 資料存在，\n請重新載入專案目錄。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Archnowledge.Pcces.DomainModule.General.PubProject pubProject = new Archnowledge.Pcces.DomainModule.General.PubProject();
			DataSet ds = pubProject.GetProjectList(F_UserID);
			DataView dv = ds.Tables[0].DefaultView;
			dv.RowFilter = "Bud IS NOT NULL";
			GridSource.Rows.Count = dv.Count + 1;
			for (int i = 0; i < dv.Count; i++)
			{
				GridSource[i + 1, "ProjectCode"] = dv[i]["projectCode"].ToString();
				GridSource[i + 1, "ProjectNameC"] = dv[i]["ProjCName"].ToString();
			}
			GridSource.AutoSizeCols();
			GridDestination.Rows.Count = 1;
		}
		switch (OptionSet)
		{
		case 1:
			Tab_F.Tab.Selected = true;
			break;
		case 2:
			Tab_E.Tab.Selected = true;
			break;
		case 3:
			Tab_C.Tab.Selected = true;
			c1FlexGrid1.Select();
			break;
		case 4:
			Tab_G.Tab.Selected = true;
			break;
		case 5:
			Tab_K.Tab.Selected = true;
			break;
		}
	}

	private void B_Btn_Prev_Click(object sender, EventArgs e)
	{
		switch (OptionSet)
		{
		case 1:
			Tab_A.Tab.Selected = true;
			break;
		case 2:
			Tab_A.Tab.Selected = true;
			break;
		case 3:
			Tab_A.Tab.Selected = true;
			break;
		case 4:
			Tab_A.Tab.Selected = true;
			break;
		case 5:
			Tab_A.Tab.Selected = true;
			break;
		}
	}

	private void F_Btn_Prev_Click(object sender, EventArgs e)
	{
		switch (OptionSet)
		{
		case 1:
			Tab_B.Tab.Selected = true;
			break;
		case 2:
			Tab_E.Tab.Selected = true;
			break;
		case 3:
			Tab_D.Tab.Selected = true;
			break;
		}
	}

	private void C_Btn_Prev_Click(object sender, EventArgs e)
	{
		DeleteNewProject();
		Tab_B.Tab.Selected = true;
	}

	private void C_Btn_Next_Click(object sender, EventArgs e)
	{
		F_ProjectCode = c1FlexGrid1[c1FlexGrid1.Row, "ProjectCode"].ToString().Trim();
		F_ProjectNameC = c1FlexGrid1[c1FlexGrid1.Row, "projCName"].ToString().Trim();
		F_SubProjectCode = txtProjectCode.Text.Trim();
		lblTitle.Text = "主專案:【" + F_ProjectCode + "】" + F_ProjectNameC;
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("建立分標--主專案關係建立");
		Archnowledge.Pcces.BUDClass.Project ProjCom = new Archnowledge.Pcces.BUDClass.Project(aArr);
		ProjCom.ps_srckind = "bud";
		ProjCom.ps_projectCode = F_SubProjectCode;
		ProjCom.ps_mainProj = F_ProjectCode;
		ProjCom.ps_projectNameC = txtProjectCName.Text;
		ProjCom.InseItem();
		ProjCom = null;
		aArr = null;
		SettingDecimal();
		RememberColsProps();
		LoadMainProjectData();
		base.MaximizeBox = true;
		Application.DoEvents();
		Tab_D.Tab.Selected = true;
	}

	private void D_Btn_Prev_Click(object sender, EventArgs e)
	{
		base.MaximizeBox = false;
		base.WindowState = FormWindowState.Normal;
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("建立分標--主專案關係建立[清空]");
		Archnowledge.Pcces.BUDClass.Project ProjCom = new Archnowledge.Pcces.BUDClass.Project(aArr);
		ProjCom.ps_srckind = "bud";
		ProjCom.ps_projectCode = F_SubProjectCode;
		ProjCom.ps_mainProj = "";
		ProjCom.UpdItem();
		ProjCom = null;
		aArr = null;
		Tab_C.Tab.Selected = true;
	}

	private void D_Btn_Next_Click(object sender, EventArgs e)
	{
		base.MaximizeBox = false;
		base.WindowState = FormWindowState.Normal;
		Do_SaveCheckItem();
		SaveProjectInfo();
		Tab_F.Tab.Selected = true;
	}

	private void HideCols(bool IsHide)
	{
		if (IsHide)
		{
			c1FlexGrid2.Cols["PrintNo"].Visible = false;
			c1FlexGrid2.Cols["CanCheck"].Visible = false;
			c1FlexGrid2.Cols["SNo"].Visible = false;
			c1FlexGrid2.Cols["PccesCode"].Visible = false;
			c1FlexGrid2.Cols["PubCode"].Visible = false;
			c1FlexGrid2.Cols["Kind"].Visible = false;
		}
	}

	private void RB1_CheckedChanged(object sender, EventArgs e)
	{
		if (RB1.Checked)
		{
			OptionSet = 1;
		}
		if (RB2.Checked)
		{
			OptionSet = 2;
		}
		if (RB3.Checked)
		{
			OptionSet = 3;
		}
		if (RB4.Checked)
		{
			OptionSet = 4;
		}
		if (RB5.Checked)
		{
			OptionSet = 5;
		}
	}

	private void ClearControlText()
	{
		txtProjectAddress.Text = "";
		txtProjectCName.Text = "";
		txtProjectEName.Text = "";
		txtProjectCode.Text = "";
		txtProjectCodeAlias.Text = "";
		txtProjectMemo.Text = "";
		if (F_PID != null && F_PID.Trim() == "Z14AC1100")
		{
			lblProjectCode.Text = "工程號/執行號：";
			lblProjectCodeAlias.Text = "動支單號：";
		}
	}

	private void formNewProjectWizard_Load(object sender, EventArgs e)
	{
		ClearControlText();
		if (F_IniMode == "1")
		{
			RB1.Checked = true;
			Tab_B.Tab.Selected = true;
			B_Btn_Prev.Visible = false;
		}
		if (F_IniMode == "2")
		{
			RB2.Checked = true;
			Tab_E.Tab.Selected = true;
			E_Btn_Prev.Visible = false;
		}
		if (F_IniMode == "3")
		{
			RB3.Checked = true;
			Tab_B.Tab.Selected = true;
			B_Btn_Prev.Visible = false;
		}
		if (F_IniMode == "4")
		{
			RB4.Checked = true;
			Tab_B.Tab.Selected = true;
			B_Btn_Prev.Visible = false;
		}
		if (F_IniMode == "5")
		{
			RB5.Checked = true;
			Tab_B.Tab.Selected = true;
			B_Btn_Prev.Visible = false;
		}
		if (Is75094900())
		{
			chkGodMode.Visible = true;
		}
		else
		{
			chkGodMode.Visible = false;
		}
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

	private int InsertProjectToDB()
	{
		int ActID = 0;
		try
		{
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(F_UserID);
			aArr.Add("專案資料加進資料庫中");
			Archnowledge.Pcces.BUDClass.PubProject PUB_PROJ = new Archnowledge.Pcces.BUDClass.PubProject(aArr);
			PUB_PROJ.ps_projectCode = txtProjectCode.Text.Trim();
			PUB_PROJ.ps_projectNameC = txtProjectCName.Text.Trim();
			PUB_PROJ.ps_projectNameE = txtProjectEName.Text.Trim();
			PUB_PROJ.ps_projectAddress = txtProjectAddress.Text.Trim();
			PUB_PROJ.ps_projectCodeAlias = txtProjectCodeAlias.Text.Trim();
			PUB_PROJ.ps_projectMemo = txtProjectMemo.Text.Trim();
			ActID = PUB_PROJ.InseItem();
			PUB_PROJ = null;
			if (ActID > 0)
			{
				DBClass DBCLS = new DBClass();
				DBCLS._FS_UserID = F_UserID;
				DBCLS.ExecuteCommand("Insert Into ProjAuthority(ProjectCode, UserID) values('" + txtProjectCode.Text.Trim() + "', '" + F_UserID + "')");
				DBCLS = null;
				Archnowledge.Pcces.BUDClass.PubDecimal dbDecimal = new Archnowledge.Pcces.BUDClass.PubDecimal(aArr);
				dbDecimal.ps_projectCode = txtProjectCode.Text.Trim();
				dbDecimal.ps_itemQty = "3";
				dbDecimal.ps_itemCost = "0";
				dbDecimal.ps_itemAmt = "0";
				dbDecimal.ps_analysisQty = "3";
				dbDecimal.ps_analysisCost = "2";
				dbDecimal.ps_analysisAmt = "2";
				dbDecimal.InseItem();
				dbDecimal = null;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("InsertProjectToDB 專案建立失敗 : " + ex.Message);
			ActID = -2;
		}
		return ActID;
	}

	private void BtnChgDir_Click(object sender, EventArgs e)
	{
		openFileDialog1.RestoreDirectory = true;
		openFileDialog1.Filter = "工程會電子標單檔(*.xml)|*.xml;*.pccesbak|工程會電子標單檔 xml 格式(*.xml)|*.xml|工程會新版電子標單檔 zmd 格式(*.zmd)|*.zmd";
		if (openFileDialog1.ShowDialog() == DialogResult.OK)
		{
			txtPxfin.Text = openFileDialog1.FileName;
		}
	}

	private void E_Btn_Prev_Click(object sender, EventArgs e)
	{
		switch (OptionSet)
		{
		case 1:
			Tab_A.Tab.Selected = true;
			break;
		case 2:
			Tab_A.Tab.Selected = true;
			break;
		case 3:
			Tab_A.Tab.Selected = true;
			break;
		}
	}

	private void E_Btn_Next_Click(object sender, EventArgs e)
	{
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("電子檔轉入");
		if (txtPxfin.Text.Trim() == "")
		{
			string sWarning = "請先挑選要轉入的檔案！";
			MessageBox.Show(this, sWarning, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (!File.Exists(txtPxfin.Text.Trim()))
		{
			string sWarning = "挑選的檔案不存在，確認後再執行！";
			MessageBox.Show(this, sWarning, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		Cursor = Cursors.WaitCursor;
		string fileName = CommonMethods.ExtractExtFileName(txtPxfin.Text.Trim()).ToUpper();
		if (fileName == "XML" || fileName == "PCCESBAK")
		{
			bool isOldXML = false;
			string AppName = "";
			try
			{
				isOldXML = IsOldXML(out AppName);
			}
			catch (Exception ex)
			{
				MessageBox.Show("轉入來源格式不正確！\n" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Cursor = Cursors.Default;
				return;
			}
			if (IsOldXML(txtPxfin.Text.Trim()))
			{
				ImportXMLInOldWay();
			}
			else
			{
				ImportXML(AppName);
			}
		}
		else if (fileName == "ZMD")
		{
			MyZip MyZip1 = new MyZip();
			MyZip1.Open(txtPxfin.Text.Trim(), "ARCH13139409");
			FileList[] sAcc = MyZip1.GetFileList();
			ExecResult ER = MyZip1.Extract(Application.StartupPath + "\\Report\\");
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show("解壓縮失敗！" + ER.Message);
				Cursor = Cursors.Default;
				return;
			}
			if (sAcc.Length <= 0)
			{
				MessageBox.Show(this, "電子檔損毀，請檢查後再執行匯入!", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Cursor = Cursors.Default;
				return;
			}
			if (sAcc[0].FileName.ToUpper().IndexOf(".MDB") <= 0)
			{
				MessageBox.Show(this, "電子內容有誤，請檢查後再執行匯入!", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Cursor = Cursors.Default;
				return;
			}
			MyZip1 = null;
			ultraLabel38.Text = "預算書電子檔轉入中...";
			gpMessage.Visible = true;
			Application.DoEvents();
			string sPath = CommonMethods.ExtractFilePath(Application.StartupPath + "\\Report\\");
			string sFileName = sPath + CommonMethods.ExtractFileName(sAcc[0].FileName.Trim());
			string sKey = "";
			string ls_IsCheckOutFile = "N";
			string XML_MODE = "XM1";
			if (sFileName.Length >= 4)
			{
				string Str1 = CommonMethods.ExtractFileNoExtName(sFileName);
				sKey = ((Str1.Length < 4) ? Str1 : Str1.Substring(Str1.Length - 4));
			}
			DataSet DS1 = CommonMethods.ImportAccess(sFileName);
			if (DS1.Tables["Project"].Columns.IndexOf("CloseBidDate") < 0)
			{
				DS1.Tables["Project"].Columns.Add("CloseBidDate", Type.GetType("System.DateTime"));
				DS1.Tables["Project"].Rows[0]["CloseBidDate"] = Convert.ToDateTime("1800/1/1");
			}
			if (DS1.Tables["Project"].Columns.IndexOf("CheckOut") < 0)
			{
				DS1.Tables["Project"].Columns.Add("CheckOut", Type.GetType("System.String"));
				DS1.Tables["Project"].Rows[0]["CheckOut"] = "N";
			}
			if (DS1.Tables["Project"].Rows[0]["CheckOut"].ToString().ToUpper() == "CKOUT")
			{
				ls_IsCheckOutFile = "Y";
			}
			string ssKey2 = "";
			try
			{
				ssKey2 = DS1.Tables["Project"].Rows[0]["srcKind"].ToString().ToUpper();
			}
			catch (Exception ex2)
			{
				CommonMethods.LogFile("Pcces46", "M", "Project.formNewProjectWizard.cs" + ex2.Message);
				ssKey2 = sKey;
			}
			if (F_PID != null && !(F_PID.Trim() == "") && F_PID.Trim() == "Z14AC1100")
			{
				try
				{
					if (!(DS1.Tables["Project"].Rows[0]["PccCodeCert"].ToString().ToUpper() == "PCCCODECERT"))
					{
						MessageBox.Show(this, "本電子檔非發包用預算書\n不執行轉入。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						gpMessage.Visible = false;
						Cursor = Cursors.Default;
						return;
					}
				}
				catch (Exception ex2)
				{
					CommonMethods.LogFile("Pcces46", "M", "Project.formNewProjectWizard.cs" + ex2.Message);
					MessageBox.Show(this, "本電子檔非發包用預算書\n不執行轉入。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					gpMessage.Visible = false;
					Cursor = Cursors.Default;
					return;
				}
			}
			Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
			PROJ.ps_srckind = "BUD";
			PROJ.ps_ImportType = "ZMD";
			ssKey2 = "BUD";
			string sRet = PROJ.InputXML(DS1, XML_MODE);
			if (sRet.IndexOf("\\n") > -1)
			{
				sRet = sRet.Substring(0, sRet.IndexOf("\\n")) + "\n\n" + sRet.Substring(sRet.IndexOf("\\n") + 2);
			}
			if (sRet.IndexOf("【（") <= -1)
			{
				gpMessage.Visible = false;
				MessageBox.Show(this, "轉入失敗!\n" + sRet, "轉入結果", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Cursor = Cursors.Default;
				return;
			}
			if (sRet.Trim() == "編碼錯誤！無法轉入！")
			{
				gpMessage.Visible = false;
				MessageBox.Show(this, sRet.Trim(), "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Cursor = Cursors.Default;
				return;
			}
			if (sRet.Trim() == "無工程代碼！無法轉入！")
			{
				gpMessage.Visible = false;
				MessageBox.Show(this, sRet.Trim(), "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Cursor = Cursors.Default;
				return;
			}
			string MessageBUD = ((sRet.Trim() == "") ? "\n 預算轉入成功!" : "\n 轉入成功!");
			string MessageBID = ((sRet.Trim() == "") ? "\n 標單轉入成功!" : "\n 標單轉入成功!");
			if (ssKey2.ToUpper() == "BUD")
			{
				MessageBox.Show(this, sRet.Trim() + MessageBUD, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				MessageBox.Show(this, sRet.Trim() + MessageBID, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			int iPos1 = sRet.IndexOf("（");
			int iPos2 = sRet.IndexOf("）");
			string NewProjCode = (F_NewProjectCode = sRet.Substring(iPos1 + 1, iPos2 - iPos1 - 1));
			try
			{
				foreach (DataRow dr in DS1.Tables["Items"].Rows)
				{
					if (dr["memo"].ToString().IndexOf("[跳頁]") > -1)
					{
						string sSQL = "Insert Into " + PROJ.ps_srckind + "PageBreak (ProjectCode, SNo, IsPageBreak) values ('" + F_NewProjectCode + "'," + dr["sNo"].ToString() + ",'Y') ";
						if (dr.Table.Columns.IndexOf("itemKey") > -1)
						{
							sSQL = "Insert Into " + PROJ.ps_srckind + "PageBreak (ProjectCode, SNo, IsPageBreak) values ('" + F_NewProjectCode + "'," + dr["itemKey"].ToString() + ",'Y') ";
						}
						ModifyDB ModDB = new ModifyDB(F_NewProjectCode, aArr);
						ModDB.DBInse(sSQL);
						ModDB = null;
					}
				}
			}
			catch (Exception ex2)
			{
				CommonMethods.LogFile("Pcces46", "M", "Project.formNewProjectWizard.cs" + ex2.Message);
				Console.Write(ex2.Message);
			}
			try
			{
				foreach (DataRow dr in DS1.Tables["Items"].Rows)
				{
					try
					{
						if (dr["memo"].ToString().IndexOf("[發包]") > -1)
						{
							string sSQL = "Insert Into " + PROJ.ps_srckind + "PageBreak (ProjectCode, SNo, IsBid) values ('" + F_NewProjectCode + "'," + dr["sNo"].ToString() + ",'Y') ";
							if (dr.Table.Columns.IndexOf("itemKey") > -1)
							{
								sSQL = "Insert Into " + PROJ.ps_srckind + "PageBreak (ProjectCode, SNo, IsBid) values ('" + F_NewProjectCode + "'," + dr["itemKey"].ToString() + ",'Y') ";
							}
							ModifyDB ModDB = new ModifyDB(F_NewProjectCode, aArr);
							ModDB.DBInse(sSQL);
							ModDB = null;
						}
					}
					catch
					{
						string sSQL = "Update " + PROJ.ps_srckind + "PageBreak set IsBid = 'Y'  where projectCode='" + F_NewProjectCode + "'and sNo= '" + dr["sNo"].ToString() + "' ";
						if (dr.Table.Columns.IndexOf("itemKey") > -1)
						{
							sSQL = "Update " + PROJ.ps_srckind + "PageBreak set IsBid = 'Y'  where projectCode='" + F_NewProjectCode + "'and sNo= '" + dr["itemKey"].ToString() + "' ";
						}
						ModifyDB ModDB = new ModifyDB(F_NewProjectCode, aArr);
						ModDB.DBUpd(sSQL);
						ModDB = null;
					}
				}
			}
			catch (Exception ex2)
			{
				CommonMethods.LogFile("Pcces46", "M", "Project.formNewProjectWizard.cs" + ex2.Message);
				Console.Write(ex2.Message);
			}
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = F_UserID;
			try
			{
				if (DS1.Tables["Tenderer"].Rows.Count > 0)
				{
					sub_memo memocom = new sub_memo(aArr);
					memocom.ps_prjcode = F_NewProjectCode;
					memocom.ps_subcode = "";
					memocom.ps_factory_id = DS1.Tables["Tenderer"].Rows[0]["invoice_no"].ToString().Trim();
					memocom.ps_item1_no = "01";
					memocom.ps_expectdaily = "1";
					memocom.ps_loc_no = "1";
					memocom.ps_PROJ_PROPERTY = "1";
					memocom.ps_item2_no = "000000000000000000000";
					memocom.ps_PFOJ_UPR1 = "0";
					memocom.ps_PFOJ_UPR2 = "0";
					memocom.ps_PFOJ_UPR3 = "0";
					memocom.ps_PFOJ_UPR4 = "0";
					memocom.ps_PFOJ_UPR5 = "0";
					memocom.InseItem();
					memocom = null;
				}
			}
			catch (Exception ex2)
			{
				CommonMethods.LogFile("Pcces46", "M", "Project.formNewProjectWizard.cs" + ex2.Message);
			}
			Archnowledge.Pcces.BUDClass.Project PJ1 = new Archnowledge.Pcces.BUDClass.Project(aArr);
			PJ1.ps_srckind = ssKey2.Trim();
			PJ1.ps_projectCode = F_NewProjectCode;
			PJ1.ps_FileName = CommonMethods.ExtractFileName(sFileName);
			PJ1.UpdItem();
			try
			{
				DBCLS.ExecuteCommand("Insert Into ProjAuthority(ProjectCode, UserID) values('" + NewProjCode + "', '" + F_UserID + "')");
				DBCLS = null;
			}
			catch (Exception ex2)
			{
				CommonMethods.LogFile("Pcces46", "M", "Project.formNewProjectWizard.cs" + ex2.Message);
			}
			PJ1 = null;
			DirectoryInfo directory = new DirectoryInfo(Application.StartupPath + "\\Report\\" + NewProjCode.Split('-')[0]);
			if (directory.Exists)
			{
				SysUser oSysUser = new SysUser();
				string DBName = oSysUser.GetSysUserDatabaseName(F_UserID);
				string DocumentPath = AppDomain.CurrentDomain.BaseDirectory + "\\AddOn\\" + DBName + "\\" + NewProjCode + "\\";
				Directory.CreateDirectory(DocumentPath);
				FileInfo[] files = directory.GetFiles();
				foreach (FileInfo file in files)
				{
					File.Move(file.FullName, DocumentPath + file.Name);
				}
				AddOnDownLoad addOnDownLoad = new AddOnDownLoad();
				addOnDownLoad.UpdateDBName(DBName);
			}
			PROJ = null;
			if (ls_IsCheckOutFile == "Y")
			{
				if (ssKey2.ToUpper() == "BID")
				{
					try
					{
						DBCLS = new DBClass();
						DBCLS._FS_UserID = F_UserID;
						DBCLS.ExecuteCommand("Update bidProject set CloseBidDate = null Where projectCode='" + F_NewProjectCode + "'");
						DBCLS = null;
					}
					catch (Exception ex2)
					{
						CommonMethods.LogFile("Pcces46", "M", "Project.formNewProjectWizard.cs" + ex2.Message);
						Console.Write(ex2.Message);
					}
				}
				MessageBox.Show(this, "注意\n\n此電子檔是【簽出/簽入】專用\n非一般標準電子檔，\n應用上請特別留意。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			switch (OptionSet)
			{
			case 1:
				Tab_F.Tab.Selected = true;
				break;
			case 2:
				Tab_F.Tab.Selected = true;
				break;
			case 3:
				Tab_F.Tab.Selected = true;
				break;
			}
		}
		else
		{
			MessageBox.Show(this, "轉入來源格式不正確！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		Cursor = Cursors.Default;
	}

	private bool IsOldXML(string FileName)
	{
		bool retV = false;
		TextReader FS = new StreamReader(FileName);
		string Line = FS.ReadLine();
		Line = FS.ReadLine();
		int iIndex = Line.IndexOf("applicationVersion");
		int iStart = Line.IndexOf("\"", iIndex);
		int iEnd = Line.IndexOf("\"", iStart + 1);
		string sVer = Line.Substring(iStart + 1, iEnd - iStart - 1);
		if (sVer.IndexOf("4.") != 0)
		{
			retV = true;
		}
		FS.Close();
		FS.Dispose();
		return retV;
	}

	private void ImportXML(string AppName)
	{
		ultraLabel38.Text = "\nXML 電子檔轉入中...";
		gpMessage.Visible = true;
		Application.DoEvents();
		bool importSucceeded = false;
		XMLValidator validator = new XMLValidator();
		string XSDFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\Report";
		string Message = validator.Validate(txtPxfin.Text.Trim(), XSDFilePath);
		if (Message != string.Empty)
		{
			MessageBox.Show(this, "轉入來源格式不正確！\n" + Message, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			gpMessage.Visible = false;
			Cursor = Cursors.Default;
			return;
		}
		XMLImporter importer = new XMLImporter(txtPxfin.Text.Trim());
		if (F_PID != null && F_PID.Trim() == "Z14AC1100")
		{
			importer.SetTaiwanRailwayFlag();
		}
		FileInfo fi = new FileInfo("C:\\temp\\ArchGodMode.On");
		if (fi.Exists)
		{
			importer._IsGodModeOn = true;
		}
		if (Is75094900() && chkGodMode.Checked)
		{
			importer._IsGodModeOn = true;
		}
		string errorMessage = string.Empty;
		string documentType = (importdoctype = importer.GetDocumentType());
		F_OldProjectCode = importer.GetProjectCode();
		string xmlVersion = importer.GetVersion();
		if (XMLVersionHigher(xmlVersion, AppName))
		{
			gpMessage.Visible = false;
			Cursor = Cursors.Default;
			return;
		}
		bool authenticationFailed = false;
		try
		{
			importer._IsSkipImportMrsBase = PubTools.IsMrsBaseSkip();
			importer.Import(skipAuthentication: false);
			importSucceeded = true;
		}
		catch (PccesCodeNotApprovedException)
		{
			MessageBox.Show(this, "本電子檔非發包用預算書，不執行轉入。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			gpMessage.Visible = false;
			Cursor = Cursors.Default;
			return;
		}
		catch (AuthenticationFailedException)
		{
			if (documentType == "budget" || documentType == "contract")
			{
				DialogResult result = MessageBox.Show(this, "動態驗證碼錯誤，請問是否繼續執行轉入？", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (result != DialogResult.Yes)
				{
					gpMessage.Visible = false;
					Cursor = Cursors.Default;
					return;
				}
				authenticationFailed = true;
				Application.DoEvents();
				importer.SetAuthenticationFailed();
				importer.Import(skipAuthentication: true);
				importSucceeded = true;
			}
			else if (documentType == "request" || documentType == "submit")
			{
				authenticationFailed = true;
				importer.SetAuthenticationFailed();
				importer.Import(skipAuthentication: true);
				importSucceeded = true;
			}
		}
		string projectCode = importer.GetProjectCode();
		if (importSucceeded)
		{
			if (documentType == "budget" || documentType == "contract")
			{
				BudItemA theItemA = new BudItemA();
				theItemA.UpdateBudItemCprintNoDue2SegCusVar(projectCode);
				theItemA.CleanNegativeSno(projectCode);
			}
			else if (documentType == "request" || documentType == "submit")
			{
				BidItemA theItemA2 = new BidItemA();
				theItemA2.UpdateBudItemCprintNoDue2SegCusVar(projectCode);
				theItemA2.CleanNegativeSno(projectCode);
			}
			if (documentType == "contract")
			{
				ArrayList aArr = new ArrayList();
				aArr.Add(F_UserID);
				aArr.Add("預算編輯--設定目前預算編輯類型(預算書或契約書)");
				Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
				PROJ.ps_projectCode = projectCode;
				PROJ.ps_srckind = "Cnt";
				PROJ.SetCurrentProjectActionName(projectCode);
				PROJ = null;
			}
		}
		string importMessage = (importSucceeded ? "轉入成功！" : ("轉入失敗！\n" + errorMessage));
		switch (documentType)
		{
		case "budget":
			importMessage = "預算書" + importMessage;
			break;
		case "contract":
			importMessage = "契約書" + importMessage;
			break;
		default:
			if (!(documentType == "submit"))
			{
				break;
			}
			goto case "request";
		case "request":
			importMessage = "標單" + importMessage;
			break;
		}
		string projectMessage = (importSucceeded ? ("【（" + projectCode + "） " + importer.GetContractTitle().Trim() + "】") : string.Empty);
		string message = projectMessage + Environment.NewLine + importMessage;
		if (!importer.IsOutputFromPcces())
		{
			message = "本電子檔非 PCCES 產生，資料正確性請冾原投標廠商！\n\n" + message;
		}
		if (authenticationFailed)
		{
			message = "動態驗證碼錯誤，檔案可能已遭他人修改！\n\n" + message;
		}
		MessageBox.Show(this, message, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		if (importSucceeded)
		{
			AddProjectAuthority(projectCode);
			F_NewProjectCode = projectCode;
			Tab_F.Tab.Selected = true;
			if (documentType != "budget" && F_OldProjectCode.Trim() != F_NewProjectCode.Trim())
			{
				ArrayList aArr = new ArrayList();
				aArr.Add(F_UserID);
				aArr.Add("預算編輯--設定目前預算編輯類型(預算書或契約書)");
				Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
				PROJ.ps_projectCode = F_NewProjectCode.Trim();
				PROJ.ps_srckind = "CNT";
				string l_str = "select IsNull(Max(version), 50000) as version from tmpProject where projectCode = '" + F_NewProjectCode + "'  and sKind = 'Cnt'";
				ModifyDB StdCom = new ModifyDB(F_NewProjectCode, aArr);
				DataTable ldt_mytable = StdCom.DBList(l_str);
				int iMax = PubTools.Str2Int(ldt_mytable.Rows[0]["version"].ToString());
				PROJ.ps_srckind = "CNT";
				PROJ.CopyTmpProj(F_NewProjectCode, (iMax + 1).ToString());
				string sBud = "Insert Into tmpProject(ProjectCode, mainCode, projectNameC, projectNameE, projectAddress, accountCode1,projectCodeAlias, accountCode2, buyMode, workMode, expectDaily, projectScope, workUnit, projamt, AutoCalcCost, UseIR, CloseBidDate, IsQtyModifiable, BudStartYear, BudEndYear, ExpectStartDate,version,sKind,NewDate,shareVDF1, shareVDF1sNo) Select '" + projectCode + "', mainCode, projectNameC, projectNameE, projectAddress, accountCode1,projectCodeAlias, accountCode2, buyMode, workMode, expectDaily, projectScope, workUnit, projamt, AutoCalcCost, UseIR, CloseBidDate, IsQtyModifiable, BudStartYear, BudEndYear, ExpectStartDate, '" + (iMax + 1) + "' as version,'CNT' as sKind,'" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' as NewDate,shareVDF1, shareVDF1sNo From budProject Where ProjectCode ='" + F_NewProjectCode + "' ";
				StdCom.DBUpd(sBud);
				StdCom = null;
				Archnowledge.Pcces.DomainModule.LogicalBase.Project project = new BudProject();
				ExecResult ER = project.RemoveProject(F_NewProjectCode);
				if (documentType == "contract")
				{
					MessageBox.Show(this, "發現使用中的契約已有相同的專案代碼，系統自動改成【" + F_NewProjectCode.Trim() + "】。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					MessageBox.Show(this, "發現使用中的標單已有相同的專案代碼，系統自動改成【" + F_NewProjectCode.Trim() + "】。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			else if (documentType == "contract")
			{
				ArrayList aArr = new ArrayList();
				aArr.Add(F_UserID);
				aArr.Add("預算編輯--設定目前預算編輯類型(預算書或契約書)");
				Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
				PROJ.ps_projectCode = F_NewProjectCode.Trim();
				PROJ.ps_srckind = "CNT";
				string l_str = "select IsNull(Max(version), 50000) as version from tmpProject where projectCode = '" + F_NewProjectCode + "'  and sKind = 'Cnt'";
				ModifyDB StdCom = new ModifyDB(F_NewProjectCode, aArr);
				DataTable ldt_mytable = StdCom.DBList(l_str);
				int iMax = PubTools.Str2Int(ldt_mytable.Rows[0]["version"].ToString());
				PROJ.ps_srckind = "CNT";
				PROJ.CopyTmpProj(F_NewProjectCode, (iMax + 1).ToString());
				string sBud = "Insert Into tmpProject(ProjectCode, mainCode, projectNameC, projectNameE, projectAddress, accountCode1,projectCodeAlias, accountCode2, buyMode, workMode, expectDaily, projectScope, workUnit, projamt, AutoCalcCost, UseIR, CloseBidDate, IsQtyModifiable, BudStartYear, BudEndYear, ExpectStartDate,version,sKind,NewDate,memo,shareVDF1, shareVDF1sNo) Select '" + F_NewProjectCode + "', mainCode, projectNameC, projectNameE, projectAddress, accountCode1,projectCodeAlias, accountCode2, buyMode, workMode, expectDaily, projectScope, workUnit, projamt, AutoCalcCost, UseIR, CloseBidDate, IsQtyModifiable, BudStartYear, BudEndYear, ExpectStartDate, '" + (iMax + 1) + "' as version,'CNT' as sKind,'" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' as NewDate,'FromXMLFile',shareVDF1, shareVDF1sNo From budProject Where ProjectCode ='" + F_NewProjectCode + "' ";
				StdCom.DBUpd(sBud);
				StdCom = null;
				Archnowledge.Pcces.DomainModule.LogicalBase.Project project = new BudProject();
				ExecResult ER = project.RemoveProject(F_NewProjectCode);
			}
		}
		gpMessage.Visible = false;
		Cursor = Cursors.Default;
	}

	private bool XMLVersionHigher(string xmlVersion, string AppName)
	{
		bool returnValue = false;
		string pccesVersion = PccesVersion.PccesAssemblyVersion;
		if (PccesVersion.CompareVersion(xmlVersion, pccesVersion) && AppName.ToUpper() == "PCCES")
		{
			string warningMessage = "此電子檔版本 (" + xmlVersion + ") 高於目前程式版本 (" + PccesVersion.PccesAssemblyVersion + ")，建議更新程式後再執行匯入。\n是否繼續執行匯入？";
			if (MessageBox.Show(this, warningMessage, "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
			{
				returnValue = true;
			}
		}
		else if (AppName.ToUpper() != "PCCES")
		{
			string warningMessage = "此 XML 檔並非 PCCES 製作, 匯入後計算結果可能不等於原總價金額";
			MessageBox.Show(this, warningMessage, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			returnValue = false;
		}
		return returnValue;
	}

	private void AddProjectAuthority(string projectCode)
	{
		ProjAuthority projAuthority = new ProjAuthority();
		DataSet dsProjAuthority = projAuthority.GetProjAuthorityByUserID(string.Empty);
		DataRow drProjAuthority = dsProjAuthority.Tables[0].NewRow();
		drProjAuthority["ProjectCode"] = projectCode;
		drProjAuthority["UserID"] = F_UserID;
		dsProjAuthority.Tables[0].Rows.Add(drProjAuthority);
		projAuthority.UpdateProjAuthority(dsProjAuthority);
	}

	private void RemoveProject(string documentType, string projectCode)
	{
		Archnowledge.Pcces.DomainModule.LogicalBase.Project project = null;
		if (documentType == "budget" || documentType == "contract")
		{
			project = new BudProject();
		}
		else if (documentType == "request" || documentType == "submit")
		{
			project = new BidProject();
		}
		ExecResult ER = project.RemoveProject(projectCode);
		if (ER.ReturnCode != 0)
		{
			Archnowledge.Pcces.CommonClass.DebugUtil.OutputDebugString(ER.Message);
		}
	}

	private bool IsOldXML(out string AppName)
	{
		using XmlReader reader = XmlReader.Create(txtPxfin.Text.Trim());
		reader.ReadToFollowing("ETenderSheet");
		AppName = reader.GetAttribute("applicationName");
		if (reader.GetAttribute("applicationName") == "Pcces" && reader.GetAttribute("signatureSealStamp") != null)
		{
			return false;
		}
		return true;
	}

	private void ImportXMLInOldWay()
	{
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("電子檔轉入");
		ultraLabel38.Text = "XML 電子檔轉入中...\n此為舊版格式的 XML 檔案，\n轉入時間可能較長，請稍候。";
		gpMessage.Visible = true;
		Application.DoEvents();
		string sFileName = CommonMethods.ExtractFileName(txtPxfin.Text.Trim());
		string sKey = "";
		string ls_Mode = "";
		string ls_IsCheckOutFile = "N";
		string XML_MODE = "XML";
		if (sFileName.Length >= 4)
		{
			string Str1 = CommonMethods.ExtractFileNoExtName(sFileName);
			sKey = ((Str1.Length < 4) ? Str1 : Str1.Substring(Str1.Length - 4));
		}
		DataSet DS1 = new DataSet();
		bool isOldXML = true;
		TextReader FS = new StreamReader(txtPxfin.Text.Trim());
		string Line = FS.ReadLine();
		Line = FS.ReadLine();
		if (Line.IndexOf("<ETenderSheet") == 0)
		{
			isOldXML = false;
		}
		FS.Close();
		FS.Dispose();
		if (isOldXML)
		{
			DS1.ReadXml(txtPxfin.Text.Trim());
			XML_MODE = "XML";
		}
		else
		{
			ChgXMLStru XMLCom = new ChgXMLStru();
			string strPath1 = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "Report\\XSD\\";
			ls_Mode = XMLCom.CheckXML(txtPxfin.Text.Trim(), strPath1);
			if (ls_Mode != "")
			{
				ls_Mode = "";
				strPath1 = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "Report\\XSD_Budget\\";
				ls_Mode = XMLCom.CheckXML(txtPxfin.Text.Trim(), strPath1);
			}
			if (ls_Mode == "")
			{
				XMLCom._UserID = F_UserID;
				DS1 = XMLCom.InputXML1(txtPxfin.Text.Trim());
				XML_MODE = "XM1";
			}
			XMLCom = null;
		}
		if (ls_Mode.Length != 0)
		{
			MessageBox.Show(this, "此檔案不是專案格式檔，請確認後再執行！\n" + ls_Mode, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			gpMessage.Visible = false;
			Cursor = Cursors.Default;
			return;
		}
		if (DS1.Tables.IndexOf("Project") == -1)
		{
			MessageBox.Show(this, "此檔案不是專案格式檔，請確認後再執行！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			gpMessage.Visible = false;
			Cursor = Cursors.Default;
			return;
		}
		if (DS1.Tables["Project"].Columns.IndexOf("CloseBidDate") < 0)
		{
			DS1.Tables["Project"].Columns.Add("CloseBidDate", Type.GetType("System.DateTime"));
			DS1.Tables["Project"].Rows[0]["CloseBidDate"] = Convert.ToDateTime("1800/1/1");
		}
		if (DS1.Tables["Project"].Columns.IndexOf("CheckOut") < 0)
		{
			DS1.Tables["Project"].Columns.Add("CheckOut", Type.GetType("System.String"));
			DS1.Tables["Project"].Rows[0]["CheckOut"] = "N";
		}
		if (DS1.Tables["Project"].Rows[0]["CheckOut"].ToString().ToUpper() == "CKOUT")
		{
			ls_IsCheckOutFile = "Y";
		}
		if (DS1.Tables["Project"].Columns.Contains("srcKind") && DS1.Tables["Project"].Rows.Count == 1)
		{
			if (DS1.Tables["Project"].Rows[0]["srcKind"].ToString() == "BUD")
			{
				importdoctype = "budget";
			}
			else if (DS1.Tables["Project"].Rows[0]["srcKind"].ToString() == "BID")
			{
				importdoctype = "request";
			}
		}
		string ssKey2 = "";
		try
		{
			F_OldProjectCode = DS1.Tables["Project"].Rows[0]["projectCode"].ToString();
			F_CProjectName = DS1.Tables["Project"].Rows[0]["projectNameC"].ToString();
			ssKey2 = DS1.Tables["Project"].Rows[0]["srcKind"].ToString().ToUpper();
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Project.formNewProjectWizard.cs" + ex.Message);
			ssKey2 = sKey;
		}
		if (F_PID != null && !(F_PID.Trim() == "") && F_PID.Trim() == "Z14AC1100")
		{
			try
			{
				if (!(DS1.Tables["Project"].Rows[0]["PccCodeCert"].ToString().ToUpper() == "PCCCODECERT"))
				{
					MessageBox.Show(this, "本電子檔非發包用預算書\n不執行轉入。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					gpMessage.Visible = false;
					Cursor = Cursors.Default;
					return;
				}
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Project.formNewProjectWizard.cs" + ex.Message);
				MessageBox.Show(this, "本電子檔非發包用預算書\n不執行轉入。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				gpMessage.Visible = false;
				Cursor = Cursors.Default;
				return;
			}
		}
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PROJ.ps_srckind = ((sKey.ToUpper() == "BDGT") ? "BUD" : "BID");
		PROJ.ps_ImportType = "XML";
		if (chkGodMode.Checked)
		{
			PROJ.IsGodMode = true;
		}
		string sRet = PROJ.InputXML(DS1, XML_MODE);
		if (ssKey2.ToUpper() == "BUD" && sRet.IndexOf("動態驗證碼錯誤") > -1)
		{
			DialogResult result = MessageBox.Show(this, "動態驗証碼錯誤！是否繼續匯入？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result != DialogResult.Yes)
			{
				gpMessage.Visible = false;
				Cursor = Cursors.Default;
				return;
			}
			PROJ.forceImport = true;
			sRet = PROJ.InputXML(DS1, XML_MODE);
		}
		if (sRet.IndexOf("\\n") > -1)
		{
			sRet = sRet.Substring(0, sRet.IndexOf("\\n")) + "\n\n" + sRet.Substring(sRet.IndexOf("\\n") + 2);
		}
		if (sRet.IndexOf("【（") <= -1)
		{
			gpMessage.Visible = false;
			MessageBox.Show(this, "轉入失敗!\n" + sRet, "轉入結果", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			Cursor = Cursors.Default;
			return;
		}
		if (sRet.Trim() == "編碼錯誤！無法轉入！")
		{
			gpMessage.Visible = false;
			MessageBox.Show(this, sRet.Trim(), "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			Cursor = Cursors.Default;
			return;
		}
		if (sRet.Trim() == "無工程代碼！無法轉入！")
		{
			gpMessage.Visible = false;
			MessageBox.Show(this, sRet.Trim(), "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			Cursor = Cursors.Default;
			return;
		}
		string warningMessage = "\n\n滙入之 XML 資料為 4.2 版，您的 PCCES 為 4.3 版(較新)。\n請於預算資訊補輸入必填欄位(如：成果概要，4.2 版無此資訊)。\n若使用成本架構，則詳細表之成本架構項屬性值亦需補輸入。";
		string MessageBUD = ((sRet.Trim() == "") ? ("\n 預算轉入成功！" + warningMessage) : ("\n 轉入成功！" + warningMessage));
		string MessageBID = ((sRet.Trim() == "") ? "\n 標單轉入成功！" : "\n 標單轉入成功！");
		if (ssKey2.ToUpper() == "BUD")
		{
			MessageBox.Show(this, sRet.Trim() + MessageBUD, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else
		{
			MessageBox.Show(this, sRet.Trim() + MessageBID, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		int iPos1 = sRet.IndexOf("（");
		int iPos2 = sRet.IndexOf("）");
		string NewProjCode = (F_NewProjectCode = sRet.Substring(iPos1 + 1, iPos2 - iPos1 - 1));
		ModifyDB ModDB = new ModifyDB(F_NewProjectCode, aArr);
		ConnectionManager.AddConnectionItemList("Pcces", "System.Data.SqlClient", ModDB.SQLConnectionString);
		PageBreak thePageBreak = null;
		thePageBreak = ((!(PROJ.ps_srckind.ToLower() == "bid")) ? ((PageBreak)new BudPageBreak()) : ((PageBreak)new BidPageBreak()));
		DataSet ds = thePageBreak.GetPageBreak(F_NewProjectCode);
		foreach (DataRow dr in DS1.Tables["Items"].Rows)
		{
			string IsPageBreak = "";
			string IsBid = "";
			if (dr["memo"] == DBNull.Value)
			{
				continue;
			}
			if (dr["memo"].ToString().IndexOf("[跳頁]") > -1)
			{
				IsPageBreak = "Y";
			}
			if (dr["memo"].ToString().IndexOf("[發包]") > -1)
			{
				IsBid = "Y";
			}
			if (IsPageBreak == "Y" || IsBid == "Y")
			{
				int SNo = 0;
				try
				{
					SNo = ((dr.Table.Columns.IndexOf("itemKey") <= -1) ? int.Parse(dr["sNo"].ToString()) : int.Parse(dr["itemKey"].ToString()));
				}
				catch (Exception ex)
				{
					CommonMethods.LogFile("Pcces46", "M", "Project.formNewProjectWizard.cs" + ex.Message);
					Console.Write(ex.Message);
				}
				DataRow newRow = ds.Tables[0].NewRow();
				newRow["ProjectCode"] = F_NewProjectCode;
				newRow["SNo"] = SNo;
				newRow["IsPageBreak"] = IsPageBreak;
				newRow["IsBid"] = IsBid;
				ds.Tables[0].Rows.Add(newRow);
			}
		}
		ExecResult ER = thePageBreak.GetDatasetUpdate(ds);
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		try
		{
			if (DS1.Tables["Tenderer"].Rows.Count > 0)
			{
				sub_memo memocom = new sub_memo(aArr);
				memocom.ps_prjcode = F_NewProjectCode;
				memocom.ps_subcode = "";
				memocom.ps_factory_id = DS1.Tables["Tenderer"].Rows[0]["invoice_no"].ToString().Trim();
				memocom.ps_item1_no = "01";
				memocom.ps_expectdaily = "1";
				memocom.ps_loc_no = "1";
				memocom.ps_PROJ_PROPERTY = "1";
				memocom.ps_item2_no = "000000000000000000000";
				memocom.ps_PFOJ_UPR1 = "0";
				memocom.ps_PFOJ_UPR2 = "0";
				memocom.ps_PFOJ_UPR3 = "0";
				memocom.ps_PFOJ_UPR4 = "0";
				memocom.ps_PFOJ_UPR5 = "0";
				memocom.InseItem();
				memocom = null;
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Project.formNewProjectWizard.cs" + ex.Message);
		}
		Archnowledge.Pcces.BUDClass.Project PJ1 = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PJ1.ps_srckind = ssKey2.Trim();
		PJ1.ps_projectCode = F_NewProjectCode;
		PJ1.ps_FileName = CommonMethods.ExtractFileName(sFileName);
		PJ1.UpdItem();
		Archnowledge.Pcces.DomainModule.General.PubProject pubProject = new Archnowledge.Pcces.DomainModule.General.PubProject();
		pubProject.UpdatePubProjectEnableNewCalculateCost(NewProjCode, enableNewCalculateCost: false);
		try
		{
			DBCLS.ExecuteCommand("Delete ProjAuthority where ProjectCode='" + NewProjCode + "' and UserID='" + F_UserID + "'");
			DBCLS.ExecuteCommand("Insert Into ProjAuthority(ProjectCode, UserID) values('" + NewProjCode + "', '" + F_UserID + "')");
			DBCLS = null;
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Project.formNewProjectWizard.cs" + ex.Message);
		}
		PJ1 = null;
		if (ls_IsCheckOutFile == "Y")
		{
			if (ssKey2.ToUpper() == "BID")
			{
				try
				{
					DBCLS = new DBClass();
					DBCLS._FS_UserID = F_UserID;
					DBCLS.ExecuteCommand("Update bidProject set CloseBidDate = null Where projectCode='" + F_NewProjectCode + "'");
					DBCLS = null;
				}
				catch (Exception ex)
				{
					CommonMethods.LogFile("Pcces46", "M", "Project.formNewProjectWizard.cs" + ex.Message);
					Console.Write(ex.Message);
				}
			}
			MessageBox.Show(this, "注意\n\n此電子檔是【簽出/簽入】專用\n非一般標準電子檔，\n應用上請特別留意。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		gpMessage.Visible = false;
		switch (OptionSet)
		{
		case 1:
			Tab_F.Tab.Selected = true;
			break;
		case 2:
			Tab_F.Tab.Selected = true;
			break;
		case 3:
			Tab_F.Tab.Selected = true;
			break;
		}
	}

	private void LoadMainProjectData()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		if (F_ProjectCode.Length > 0)
		{
			tmp_AL1.Add("(Get_MainProjItems) 分標選自主專案的預算書");
		}
		else
		{
			tmp_AL1.Add("(Get_MainProjItems) 併標選自子專案的預算書");
		}
		if (F_ProjectCode.Length > 0)
		{
			Archnowledge.Pcces.BUDClass.ItemA ItemACom = new Archnowledge.Pcces.BUDClass.ItemA(tmp_AL1);
			ItemACom.ps_srckind = "bud";
			DT_bud = ItemACom.SeleItem1("", F_SubProjectCode, F_ProjectCode);
			BindToGrid();
			ItemACom = null;
		}
		tmp_AL1 = null;
	}

	private void BindToGrid()
	{
		F_SPLT_STATUS = "EDT";
		c1FlexGrid2.Visible = false;
		RememberColsProps();
		CellStyle CS1 = c1FlexGrid2.Styles.Add("AnalysisColor");
		CellStyle CS9 = c1FlexGrid2.Styles.Add("IsSharedColor");
		CS1.ForeColor = Color.Red;
		CS9.ForeColor = Color.Plum;
		CellStyle CS_EDT = c1FlexGrid2.Styles.Add("EDT");
		CS_EDT.BackColor = Color.Orange;
		CellStyle CS_NOTEDT = c1FlexGrid2.Styles.Add("NOTEDT");
		CS_NOTEDT.BackColor = Color.LightGray;
		c1FlexGrid2.Clear(ClearFlags.All);
		c1FlexGrid2.Rows.Count = DT_bud.Rows.Count + 1;
		SetGridColumn();
		c1FlexGrid2.Cols["Unitname"].Style = c1FlexGrid2.Styles["NOTEDT"];
		c1FlexGrid2.Cols["qty"].Style = c1FlexGrid2.Styles["NOTEDT"];
		c1FlexGrid2.Cols["cost"].Style = c1FlexGrid2.Styles["NOTEDT"];
		c1FlexGrid2.Cols["RemainQty"].Style = c1FlexGrid2.Styles["NOTEDT"];
		c1FlexGrid2.Cols["RemainCost"].Style = c1FlexGrid2.Styles["NOTEDT"];
		for (int i = 0; i < DT_bud.Rows.Count; i++)
		{
			c1FlexGrid2[i + 1, "IsCheck"] = DT_bud.Rows[i]["chk"].ToString().Trim() == "1";
			c1FlexGrid2[i + 1, "ItemNo"] = DT_bud.Rows[i]["ItemNo"].ToString().Trim();
			c1FlexGrid2[i + 1, "CName"] = DT_bud.Rows[i]["CName"].ToString().Trim();
			c1FlexGrid2[i + 1, "Unitname"] = DT_bud.Rows[i]["unitName"].ToString().Trim();
			c1FlexGrid2[i + 1, "qty"] = DT_bud.Rows[i]["qty"].ToString().Trim();
			c1FlexGrid2[i + 1, "cost"] = DT_bud.Rows[i]["cost"].ToString().Trim();
			c1FlexGrid2[i + 1, "RemainQty"] = DT_bud.Rows[i]["RemainQty"].ToString().Trim();
			c1FlexGrid2[i + 1, "RemainCost"] = DT_bud.Rows[i]["RemainCost"].ToString().Trim();
			c1FlexGrid2[i + 1, "SplQty"] = DT_bud.Rows[i]["ThisQty"].ToString().Trim();
			c1FlexGrid2[i + 1, "SplCost"] = DT_bud.Rows[i]["ThisCost"].ToString().Trim();
			c1FlexGrid2[i + 1, "PrintNo"] = DT_bud.Rows[i]["PrintNo"].ToString().Trim();
			c1FlexGrid2[i + 1, "CanCheck"] = true;
			c1FlexGrid2[i + 1, "SNo"] = DT_bud.Rows[i]["SNo"].ToString().Trim();
			c1FlexGrid2[i + 1, "PccesCode"] = DT_bud.Rows[i]["PccesCode"].ToString().Trim();
			c1FlexGrid2[i + 1, "PubCode"] = DT_bud.Rows[i]["PubCode"].ToString().Trim();
			c1FlexGrid2[i + 1, "Kind"] = DT_bud.Rows[i]["Kind"].ToString().Trim();
			if ((DT_bud.Rows[i]["qty"].ToString().Trim() == "1" && DT_bud.Rows[i]["unitName"].ToString().Trim() == "式") || DT_bud.Rows[i]["kind"].ToString().Trim() == "B")
			{
				CellRange Rg = c1FlexGrid2.GetCellRange(i + 1, c1FlexGrid2.Cols["SplQty"].SafeIndex);
				Rg.Style = c1FlexGrid2.Styles["NOTEDT"];
				if (PubTools.Str2Double(DT_bud.Rows[i]["ThisCost"].ToString()) <= 0.0)
				{
					CellRange Rg2 = c1FlexGrid2.GetCellRange(i + 1, c1FlexGrid2.Cols["SplCost"].SafeIndex);
					Rg2.Style = c1FlexGrid2.Styles["NOTEDT"];
				}
			}
			else
			{
				CellRange Rg = c1FlexGrid2.GetCellRange(i + 1, c1FlexGrid2.Cols["SplCost"].SafeIndex);
				Rg.Style = c1FlexGrid2.Styles["NOTEDT"];
				if (PubTools.Str2Double(DT_bud.Rows[i]["ThisQty"].ToString()) <= 0.0)
				{
					CellRange Rg2 = c1FlexGrid2.GetCellRange(i + 1, c1FlexGrid2.Cols["SplQty"].SafeIndex);
					Rg2.Style = c1FlexGrid2.Styles["NOTEDT"];
				}
			}
			c1FlexGrid2.Rows[i + 1].IsNode = true;
			if (DT_bud.Rows[i]["PrintNo"].ToString().Trim() == "".PadLeft(32, '9'))
			{
				c1FlexGrid2.Rows[i + 1].Node.Level = 1;
			}
			else
			{
				c1FlexGrid2.Rows[i + 1].Node.Level = Convert.ToInt32(DT_bud.Rows[i]["PrintNo"].ToString().Trim().Length / 4);
			}
		}
		c1FlexGrid2.Cols.Frozen = 3;
		F_SPLT_STATUS = "NOR";
		c1FlexGrid2.Visible = true;
	}

	private void SettingDecimal()
	{
		DataTable DTDecimal = new DataTable();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add(CommonMethods.GetFormTypeTitle(FormType.Budget));
		Archnowledge.Pcces.BUDClass.PubDecimal dbDecimal = new Archnowledge.Pcces.BUDClass.PubDecimal(aArr);
		DTDecimal = dbDecimal.ListItem("", F_ProjectCode);
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
		aArr = null;
		dbDecimal = null;
		DTDecimal = null;
	}

	private void RememberColsProps()
	{
		for (int i = 0; i < GridCols; i++)
		{
			GridColsSquence[i, 0] = c1FlexGrid2.Cols[i].Name;
			GridColsSquence[i, 1] = c1FlexGrid2.Cols[i].Caption;
			GridColsSquence[i, 2] = c1FlexGrid2.Cols[i].Width;
			if (c1FlexGrid2.Cols[i].Name == "AnaImg")
			{
				GridColsSquence[i, 3] = typeof(Image);
			}
			else
			{
				GridColsSquence[i, 3] = c1FlexGrid2.Cols[i].DataType;
			}
			GridColsSquence[i, 4] = c1FlexGrid2.Cols[i].Visible;
			GridColsSquence[i, 5] = c1FlexGrid2.Cols[i].Format;
			GridColsSquence[i, 6] = c1FlexGrid2.Cols[i].AllowEditing;
			if (c1FlexGrid2.Cols[i].Name == "qty")
			{
				if (F_MainQty > 0)
				{
					GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_MainQty, '0');
				}
				else
				{
					GridColsSquence[i, 5] = "###,###,###,##0";
				}
			}
			if (c1FlexGrid2.Cols[i].Name == "cost")
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
			if (c1FlexGrid2.Cols[i].Name == "RemainQty")
			{
				if (F_MainQty > 0)
				{
					GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_MainQty, '0');
				}
				else
				{
					GridColsSquence[i, 5] = "###,###,###,##0";
				}
			}
			if (c1FlexGrid2.Cols[i].Name == "RemainCost")
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
			if (c1FlexGrid2.Cols[i].Name == "SplQty")
			{
				if (F_MainQty > 0)
				{
					GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_MainQty, '0');
				}
				else
				{
					GridColsSquence[i, 5] = "###,###,###,##0";
				}
			}
			if (c1FlexGrid2.Cols[i].Name == "SplCost")
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
			GridColsSquence[i, 7] = c1FlexGrid2.Cols[i].TextAlign;
			GridColsSquence[i, 8] = c1FlexGrid2.Cols[i].AllowDragging;
			GridColsSquence[i, 9] = c1FlexGrid2.Cols[i].AllowResizing;
		}
	}

	private void SetGridColumn()
	{
		for (int i = 0; i < GridCols; i++)
		{
			c1FlexGrid2.Cols[i].Name = (string)GridColsSquence[i, 0];
			c1FlexGrid2.Cols[i].Caption = (string)GridColsSquence[i, 1];
			c1FlexGrid2.Cols[i].Width = (int)GridColsSquence[i, 2];
			c1FlexGrid2.Cols[i].DataType = (Type)GridColsSquence[i, 3];
			c1FlexGrid2.Cols[i].Visible = (bool)GridColsSquence[i, 4];
			c1FlexGrid2.Cols[i].Format = (string)GridColsSquence[i, 5];
			c1FlexGrid2.Cols[i].AllowEditing = (bool)GridColsSquence[i, 6];
			c1FlexGrid2.Cols[i].TextAlign = (TextAlignEnum)GridColsSquence[i, 7];
			c1FlexGrid2.Cols[i].AllowDragging = (bool)GridColsSquence[i, 8];
			c1FlexGrid2.Cols[i].AllowResizing = (bool)GridColsSquence[i, 9];
		}
	}

	private void c1FlexGrid1_MouseMove(object sender, MouseEventArgs e)
	{
		int rowIndex = c1FlexGrid1.MouseRow;
		c1FlexGrid1.Row = rowIndex;
	}

	private void c1FlexGrid1_Click(object sender, EventArgs e)
	{
		string sProjectCode = c1FlexGrid1[c1FlexGrid1.Row, "ProjectCode"].ToString().Trim();
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		if (!DBCLS.GetProjectAuthority(F_UserID, sProjectCode))
		{
			MessageBox.Show(this, "這個專案您沒有權限，無法開啟。", "專案權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			DBCLS = null;
		}
		else
		{
			DBCLS = null;
			C_Btn_Next_Click(this, EventArgs.Empty);
		}
	}

	private void DeleteNewProject()
	{
		int ActID = 0;
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("WinFORM 基本工料");
		Archnowledge.Pcces.BUDClass.PubProject PUB_PROJ = new Archnowledge.Pcces.BUDClass.PubProject(aArr);
		ActID = PUB_PROJ.DeleAll(txtProjectCode.Text.Trim());
		aArr = null;
		PUB_PROJ = null;
	}

	private void C_Btn_Cncl_Click(object sender, EventArgs e)
	{
		if (!F_IsSplitSucceeded)
		{
			DeleteNewProject();
			base.DialogResult = DialogResult.Cancel;
			Close();
		}
	}

	private void Do_SaveCheckItem()
	{
		for (int i = 1; i < c1FlexGrid2.Rows.Count; i++)
		{
			if ((bool)c1FlexGrid2[i, "IsCheck"])
			{
				DT_bud.Rows[i - 1]["chk"] = "1";
				DT_bud.Rows[i - 1]["ThisQty"] = PubTools.Str2Double(c1FlexGrid2[i, "SplQty"].ToString());
				DT_bud.Rows[i - 1]["ThisCost"] = PubTools.Str2Double(c1FlexGrid2[i, "SplCost"].ToString());
			}
			else
			{
				DT_bud.Rows[i - 1]["chk"] = "0";
			}
		}
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("WinFORM 基本工料");
		Archnowledge.Pcces.BUDClass.ItemA ItemACom = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
		ItemACom.ps_srckind = "bud";
		ItemACom.CopyItemA(F_SubProjectCode, DT_bud, F_ProjectCode);
		ItemACom = null;
		PubTools.WriteRoughlyLog(aArr);
		aArr = null;
	}

	private void SaveProjectInfo()
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("WIN FORM");
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PROJ.ps_projectCode = F_SubProjectCode;
		PROJ.ps_srckind = "bud";
		PROJ.InseItem();
		try
		{
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = F_UserID;
			DBCLS.ExecuteCommand("Insert Into ProjAuthority(ProjectCode, UserID) values('" + F_SubProjectCode + "', '" + F_UserID + "')");
			DBCLS = null;
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Project.formNewProjectWizard.cs" + ex.Message);
		}
		PROJ = null;
		aArr = null;
	}

	private void c1FlexGrid2_AfterSelChange(object sender, RangeEventArgs e)
	{
		if (F_SPLT_STATUS != "NOR")
		{
			return;
		}
		int rowIndex = c1FlexGrid2.MouseRow;
		int colIndex = c1FlexGrid2.MouseCol;
		if ((c1FlexGrid2[rowIndex, "qty"].ToString().Trim() == "1" && c1FlexGrid2[rowIndex, "unitName"].ToString().Trim() == "式") || c1FlexGrid2[rowIndex, "Kind"].ToString().Trim() == "B")
		{
			if (c1FlexGrid2.Cols[colIndex].Name != "SplCost")
			{
				c1FlexGrid2.Col = 0;
			}
			else if (PubTools.Str2Double(c1FlexGrid2[rowIndex, "SplCost"].ToString()) <= 0.0)
			{
				c1FlexGrid2.Col = 0;
			}
		}
		else if (c1FlexGrid2.Cols[colIndex].Name != "SplQty")
		{
			c1FlexGrid2.Col = 0;
		}
		else if (PubTools.Str2Double(c1FlexGrid2[rowIndex, "SplQty"].ToString()) <= 0.0)
		{
			c1FlexGrid2.Col = 0;
		}
	}

	private void c1FlexGrid2_BeforeEdit(object sender, RowColEventArgs e)
	{
		if (F_SPLT_STATUS != "NOR" || c1FlexGrid2.Row <= 0 || c1FlexGrid2.MouseRow <= 0 || c1FlexGrid2.MouseCol <= 0)
		{
			return;
		}
		int rowIndex = c1FlexGrid2.MouseRow;
		int colIndex = c1FlexGrid2.MouseCol;
		if (c1FlexGrid2.Cols[colIndex].Name == "IsCheck")
		{
			return;
		}
		if ((c1FlexGrid2[rowIndex, "qty"].ToString().Trim() == "1" && c1FlexGrid2[rowIndex, "unitName"].ToString().Trim() == "式") || c1FlexGrid2[rowIndex, "Kind"].ToString().Trim() == "B")
		{
			if (c1FlexGrid2.Cols[colIndex].Name != "SplCost")
			{
				c1FlexGrid2.Col = 0;
				e.Cancel = true;
			}
		}
		else if (c1FlexGrid2.Cols[colIndex].Name != "SplQty")
		{
			c1FlexGrid2.Col = 0;
			e.Cancel = true;
		}
	}

	private void c1FlexGrid2_AfterEdit(object sender, RowColEventArgs e)
	{
		if (c1FlexGrid2.Cols[c1FlexGrid2.MouseCol].Name != "IsCheck")
		{
			c1FlexGrid2[e.Row, "IsCheck"] = true;
		}
		try
		{
			if ((bool)c1FlexGrid2[e.Row, "IsCheck"])
			{
				Node LastNode = c1FlexGrid2.Rows[c1FlexGrid2.Row].Node.GetNode(NodeTypeEnum.LastChild);
				for (int i = c1FlexGrid2.Row; i <= LastNode.Row.SafeIndex; i++)
				{
					c1FlexGrid2[i, "IsCheck"] = true;
				}
			}
			else
			{
				Node LastNode = c1FlexGrid2.Rows[c1FlexGrid2.Row].Node.GetNode(NodeTypeEnum.LastChild);
				for (int i = c1FlexGrid2.Row; i <= LastNode.Row.SafeIndex; i++)
				{
					c1FlexGrid2[i, "IsCheck"] = false;
				}
			}
			string sPrintNo = c1FlexGrid2[e.Row, "PrintNo"].ToString().Trim();
			int iCount = sPrintNo.Length;
			iCount /= 4;
			for (int i = 1; i < c1FlexGrid2.Rows.Count; i++)
			{
				string realPrintNo = c1FlexGrid2[i, "PrintNo"].ToString().Trim();
				int realiCount = realPrintNo.Length / 4;
				if (realiCount >= iCount)
				{
					switch (realiCount - iCount)
					{
					case 1:
						realPrintNo = realPrintNo.Substring(0, realPrintNo.Length - 4);
						break;
					case 2:
						realPrintNo = realPrintNo.Substring(0, realPrintNo.Length - 8);
						break;
					case 3:
						realPrintNo = realPrintNo.Substring(0, realPrintNo.Length - 12);
						break;
					case 4:
						realPrintNo = realPrintNo.Substring(0, realPrintNo.Length - 16);
						break;
					case 5:
						realPrintNo = realPrintNo.Substring(0, realPrintNo.Length - 20);
						break;
					case 6:
						realPrintNo = realPrintNo.Substring(0, realPrintNo.Length - 24);
						break;
					case 7:
						realPrintNo = realPrintNo.Substring(0, realPrintNo.Length - 28);
						break;
					case 8:
						realPrintNo = realPrintNo.Substring(0, realPrintNo.Length - 32);
						break;
					}
					if (sPrintNo == realPrintNo.Trim())
					{
						c1FlexGrid2[i, "IsCheck"] = (bool)c1FlexGrid2[e.Row, "IsCheck"];
					}
				}
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Project.formNewProjectWizard.cs" + ex.Message);
		}
	}

	private void F_Btn_Fnsh_Click(object sender, EventArgs e)
	{
		if (base.Owner == null || base.Owner.ActiveMdiChild == null)
		{
			return;
		}
		Form ActiveForm = base.Owner.ActiveMdiChild;
		if (ActiveForm is FormProject)
		{
			if (RB2.Checked)
			{
				(ActiveForm as FormProject)._NewProjectCode = F_NewProjectCode.Trim();
				((importdoctype == "budget") ? ((Archnowledge.Pcces.DomainModule.LogicalBase.Project)new BudProject()) : ((Archnowledge.Pcces.DomainModule.LogicalBase.Project)((!(importdoctype == "request") && !(importdoctype == "request")) ? null : new BidProject())))?.InitParentSno(F_NewProjectCode);
			}
			else
			{
				(ActiveForm as FormProject)._NewProjectCode = txtProjectCode.Text.Trim();
				BudItemA buda = new BudItemA();
				buda.MakeupNullsNo(txtProjectCode.Text.Trim());
				buda.UpdateParentSno(txtProjectCode.Text.Trim());
			}
		}
		if (F_IsAddOn == "BID")
		{
			base.DialogResult = DialogResult.OK;
			(base.Owner as frmPccesMain).LeftPanel.Width = 0;
			Close();
		}
	}

	private void WizardTabs_ActiveTabChanged(object sender, ActiveTabChangedEventArgs e)
	{
		string key = WizardTabs.ActiveTab.Key;
		if (key != null && key == "Tab_E")
		{
			RB2.Checked = true;
		}
	}

	private void BtnChgDirG_Click(object sender, EventArgs e)
	{
		openFileDialog1.RestoreDirectory = true;
		openFileDialog1.Filter = "預算書 Excel 檔(*.xls)|*.xls|預算書 Excel 檔(*.xls)|*.xls";
		if (openFileDialog1.ShowDialog() == DialogResult.OK)
		{
			txtExcelin.Text = openFileDialog1.FileName;
		}
	}

	private bool Do_Import_DIY(string sProjCode)
	{
		bool RetV = true;
		OleDbConnection oCon = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtExcelin.Text.Trim() + ";Extended Properties=Excel 8.0;Persist Security Info=False");
		string SQLStr = "select *,(select count(項次代碼) from [Sheet1$] WHERE 項次代碼 = A.項次代碼 group by 項次代碼) AS ACOUNT from [Sheet1$] A order by 項次代碼";
		OleDbDataAdapter oDA = new OleDbDataAdapter(SQLStr, oCon);
		DataTable InputDt = new DataTable();
		try
		{
			oDA.Fill(InputDt);
			RetV = true;
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Project.formNewProjectWizard.cs" + ex.Message);
			string sWarning = "轉入來源的檔案格式不正確，請重新挑選!";
			MessageBox.Show(this, sWarning, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			Tab_G.Tab.Selected = true;
			return false;
		}
		if (InputDt.Columns.IndexOf("項次") < 0)
		{
			MessageBox.Show(this, "轉入來源的檔案格式不正確!無【項次】欄位!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		if (InputDt.Columns.IndexOf("項目") < 0)
		{
			MessageBox.Show(this, "轉入來源的檔案格式不正確!無【項目】欄位!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		if (InputDt.Columns.IndexOf("單位") < 0)
		{
			MessageBox.Show(this, "轉入來源的檔案格式不正確!無【單位】欄位!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		if (InputDt.Columns.IndexOf("數量") < 0)
		{
			MessageBox.Show(this, "轉入來源的檔案格式不正確!無【數量】欄位!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		if (InputDt.Columns.IndexOf("單價") < 0)
		{
			MessageBox.Show(this, "轉入來源的檔案格式不正確!無【單價】欄位!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		if (InputDt.Columns.IndexOf("複價") < 0)
		{
			MessageBox.Show(this, "轉入來源的檔案格式不正確!無【複價】欄位!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		if (InputDt.Columns.IndexOf("備註") < 0)
		{
			MessageBox.Show(this, "轉入來源的檔案格式不正確!無【備註】欄位!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		if (InputDt.Columns.IndexOf("種類") < 0)
		{
			MessageBox.Show(this, "轉入來源的檔案格式不正確!無【種類】欄位!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		if (InputDt.Columns.IndexOf("項次代碼") < 0)
		{
			MessageBox.Show(this, "轉入來源的檔案格式不正確!無【項次代碼】欄位!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		if (InputDt.Columns.IndexOf("百分比") < 0)
		{
			MessageBox.Show(this, "轉入來源的檔案格式不正確!無【百分比】欄位!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		bool lb_kind = false;
		bool lb_PrintNo = false;
		bool lb_Data = false;
		int i = 0;
		foreach (DataRow dr in InputDt.Rows)
		{
			i++;
			string ls_kind = dr["種類"].ToString().Trim().ToUpper();
			if (ls_kind.Length == 0 || "BFLSZW".IndexOf(ls_kind) == -1)
			{
				lb_kind = true;
			}
			string ls_pintno = dr["項次代碼"].ToString().Trim();
			if (ls_pintno.Length == 0 || dr["ACOUNT"].ToString() != "1")
			{
				lb_PrintNo = true;
			}
			string ls_Data = dr["項目"].ToString().Trim() + dr["單位"].ToString().Trim();
			if (ls_Data.Length == 0)
			{
				lb_Data = true;
			}
		}
		if (lb_kind)
		{
			MessageBox.Show(this, "轉入來源的資料不正確!【種類】欄位資料有誤或未輸入!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		if (lb_PrintNo)
		{
			MessageBox.Show(this, "轉入來源的資料不正確!【項次代碼】欄位資料有誤或未輸入!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		if (lb_Data)
		{
			MessageBox.Show(this, "轉入來源的資料不正確!【項目】欄位資料有誤或未輸入!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		InputDt.Columns.Add("PccesCode", Type.GetType("System.String"));
		InputDt.Columns.Add("PubCode", Type.GetType("System.Int64"));
		InputDt.Columns.Add("AddThis", Type.GetType("System.String"));
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("DIY 轉入");
		MrsBaseA MrsACom = new MrsBaseA(F_UserID, aArr);
		MrsACom.ps_srckind = "MRS";
		DataTable MrsDT = MrsACom.ListItem("");
		DataView MrsDV = MrsDT.DefaultView;
		MrsDV.Sort = "PccesCode";
		int iFlag = 0;
		string ls_PccesCode = "Z" + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
		MrsDV.RowFilter = "substring(pccescode,1,5) = '" + ls_PccesCode + "'";
		if (MrsDV.Count > 0)
		{
			iFlag = PubTools.Str2Int(MrsDV[MrsDV.Count - 1]["pccescode"].ToString().Substring(5));
		}
		Archnowledge.Pcces.BUDClass.ItemA ItemACom = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
		ItemACom.ps_srckind = "BUD";
		ItemACom.ps_projectCode = sProjCode;
		int li_sNo = ItemACom.getMaxNo(sProjCode);
		Prog1.Maximum = InputDt.Rows.Count;
		Prog1.Minimum = 0;
		foreach (DataRow dr in InputDt.Rows)
		{
			Prog1.Value++;
			Application.DoEvents();
			string ls_kind = dr["種類"].ToString().Trim().ToUpper();
			if (ls_kind == "W")
			{
				string ls_cName = dr["項目"].ToString().Trim();
				string ls_cUnit = dr["單位"].ToString().Trim();
				MrsDV.RowFilter = "cName='" + ls_cName + "' and UnitName='" + ls_cUnit + "'";
				if (MrsDV.Count == 0)
				{
					MrsACom.ps_srckind = "MRS";
					MrsACom.ps_projectcode = null;
					string ls_nCode = ls_PccesCode + (iFlag + 1).ToString().PadLeft(5, '0');
					DataRow ndr = MrsDT.NewRow();
					MrsACom.ps_pccesCode = ls_nCode;
					ndr["PccesCode"] = ls_nCode;
					MrsACom.ps_cName = ls_cName;
					ndr["cName"] = ls_cName;
					MrsACom.ps_unitName = ls_cUnit;
					ndr["UnitName"] = ls_cUnit;
					MrsACom.ps_cost = dr["單價"].ToString().Replace(",", "");
					ndr["cost"] = PubTools.Str2Double(dr["單價"].ToString().Replace(",", ""));
					try
					{
						MrsACom.ps_eName = dr["英文名稱"].ToString();
						ndr["eName"] = dr["英文名稱"].ToString();
					}
					catch
					{
						MrsACom.ps_eName = null;
					}
					try
					{
						MrsACom.ps_eUnit = dr["英文單位"].ToString();
						ndr["eUnit"] = dr["英文單位"].ToString();
					}
					catch
					{
						MrsACom.ps_eUnit = null;
					}
					MrsACom.ps_analysis = "0";
					ndr["analysis"] = "0";
					MrsACom.ps_costKind = "";
					ndr["costKind"] = "";
					MrsACom.ps_rate = "0";
					ndr["rate"] = 0;
					string ls_memo = dr["備註"].ToString();
					if (ls_memo.Length > 0)
					{
						if (ls_memo.Substring(0, 1) != "#")
						{
							ls_memo = "#," + ls_memo;
						}
					}
					else
					{
						ls_memo = "#" + ls_memo;
					}
					MrsACom.ps_memo = ls_memo;
					ndr["memo"] = ls_memo;
					MrsACom.InseItem();
					MrsACom.SetPost(ls_nCode, "0");
					int li_npubcode = MrsACom.Get_Pubcode(ls_nCode);
					iFlag++;
					ndr["PubCode"] = li_npubcode;
					MrsDT.Rows.Add(ndr);
					MrsDV.RowFilter = "cName='" + ls_cName + "' and UnitName='" + ls_cUnit + "'";
				}
				MrsACom.ps_srckind = "BUD";
				MrsACom.ps_projectcode = sProjCode;
				MrsACom.ps_pubCode = MrsDV[0]["pubCode"].ToString();
				MrsACom.ps_pccesCode = MrsDV[0]["pccesCode"].ToString();
				MrsACom.ps_cName = MrsDV[0]["cName"].ToString();
				MrsACom.ps_unitName = MrsDV[0]["unitName"].ToString();
				MrsACom.ps_cost = MrsDV[0]["cost"].ToString();
				MrsACom.ps_eName = MrsDV[0]["eName"].ToString();
				MrsACom.ps_eUnit = MrsDV[0]["eUnit"].ToString();
				MrsACom.ps_analysis = MrsDV[0]["analysis"].ToString();
				MrsACom.ps_costKind = MrsDV[0]["costKind"].ToString();
				MrsACom.ps_rate = MrsDV[0]["rate"].ToString();
				MrsACom.ps_memo = MrsDV[0]["memo"].ToString();
				MrsACom.ps_xNameC = MrsDV[0]["xNameC"].ToString();
				MrsACom.ps_accountCode1 = MrsDV[0]["accountCode1"].ToString();
				MrsACom.ps_accountCode2 = MrsDV[0]["accountCode2"].ToString();
				MrsACom.ps_analysisQty = MrsDV[0]["analysisQty"].ToString();
				MrsACom.ps_eRate = MrsDV[0]["eRate"].ToString();
				MrsACom.ps_extendCode = MrsDV[0]["extendCode"].ToString();
				MrsACom.ps_lRate = MrsDV[0]["lRate"].ToString();
				MrsACom.ps_mRate = MrsDV[0]["mRate"].ToString();
				MrsACom.ps_wRate = MrsDV[0]["wRate"].ToString();
				MrsACom.ps_xNameE = MrsDV[0]["xNameE"].ToString();
				MrsACom.ps_resType = MrsDV[0]["resType"].ToString();
				MrsACom.ps_resCode = MrsDV[0]["resCode"].ToString();
				MrsACom.InseItem();
				ItemACom.ps_pubCode = MrsDV[0]["pubCode"].ToString();
				ItemACom.ps_eRate = MrsDV[0]["eRate"].ToString();
				ItemACom.ps_lRate = MrsDV[0]["lRate"].ToString();
				ItemACom.ps_mRate = MrsDV[0]["mRate"].ToString();
				ItemACom.ps_wRate = MrsDV[0]["wRate"].ToString();
			}
			else
			{
				ItemACom.ps_pubCode = "0";
				ItemACom.ps_eRate = null;
				ItemACom.ps_lRate = null;
				ItemACom.ps_mRate = null;
				ItemACom.ps_wRate = null;
			}
			ItemACom.ps_amount = dr["複價"].ToString();
			ItemACom.ps_cName = dr["項目"].ToString();
			ItemACom.ps_cost = dr["單價"].ToString();
			try
			{
				ItemACom.ps_eName = dr["英文名稱"].ToString();
			}
			catch
			{
				ItemACom.ps_eName = null;
			}
			try
			{
				ItemACom.ps_eUnit = dr["英文單位"].ToString();
			}
			catch
			{
				ItemACom.ps_eUnit = null;
			}
			ItemACom.ps_itemNo = dr["項次"].ToString();
			ItemACom.ps_kind = dr["種類"].ToString();
			ItemACom.ps_levelNo = ((ItemACom.ps_printNo = dr["項次代碼"].ToString().Trim()).Length / 4).ToString();
			ItemACom.ps_memo = dr["備註"].ToString();
			ItemACom.ps_qty = dr["數量"].ToString();
			ItemACom.ps_rate = dr["百分比"].ToString();
			ItemACom.ps_sNo = (li_sNo + 1).ToString();
			ItemACom.ps_unitName = dr["單位"].ToString();
			ItemACom.InseItem();
			li_sNo++;
		}
		MrsACom = null;
		MrsBaseB mrscom = new MrsBaseB(aArr);
		mrscom.ps_srckind = "BUD";
		mrscom.ReAnalysis(sProjCode);
		mrscom = null;
		DataTable dt = ItemACom.ListItem("", sProjCode);
		ItemACom = null;
		PubTools.WriteRoughlyLog(aArr);
		oDA = null;
		InputDt = null;
		return RetV;
	}

	private void G_Btn_Next_Click(object sender, EventArgs e)
	{
		if (txtExcelin.Text.Trim() == "")
		{
			MessageBox.Show(this, "請先挑選欲轉入的檔案。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		Tab_H.Tab.Selected = true;
		Application.DoEvents();
		if (Do_Import_DIY(txtProjectCode.Text.Trim()))
		{
			Tab_F.Tab.Selected = true;
			F_Btn_Prev.Visible = false;
		}
		else
		{
			DeleteNewProject();
			Tab_I.Tab.Selected = true;
		}
	}

	private void G_Btn_Cncl_Click(object sender, EventArgs e)
	{
		DeleteNewProject();
		base.DialogResult = DialogResult.Cancel;
		Close();
	}

	private void txtProjectCode_Validating(object sender, CancelEventArgs e)
	{
		if (!CommonMethods.CheckValidString((sender as UltraTextEditor).Text))
		{
			e.Cancel = true;
		}
		else
		{
			if (base.DialogResult == DialogResult.Cancel)
			{
				return;
			}
			for (int i = 0; i < txtProjectCode.Text.Length; i++)
			{
				string IsCHT = "TRUE";
				if (IsCHT != "TRUE" && !CommonMethods.EngNumValid(txtProjectCode.Text[i]))
				{
					MessageBox.Show(this, "不可輸入非數字或英文字", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtProjectCode.Focus();
					return;
				}
			}
			if (!CommonMethods.IsStrByteLenValid(txtProjectCName.Text, 200))
			{
				MessageBox.Show(this, "工程名稱的長度不可超過 200 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtProjectCName.Focus();
			}
			else if (!CommonMethods.IsStrByteLenValid(txtProjectEName.Text, 200))
			{
				MessageBox.Show(this, "Project Name (English)的長度不可超過 200 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtProjectEName.Focus();
			}
			else if (!CommonMethods.IsStrByteLenValid(txtProjectAddress.Text, 200))
			{
				MessageBox.Show(this, "工程地點的長度不可超過 200 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtProjectAddress.Focus();
			}
			else if (!CommonMethods.IsStrByteLenValid(txtProjectCodeAlias.Text, 40))
			{
				MessageBox.Show(this, lblProjectCodeAlias.Text + "的長度不可超過 40 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtProjectCodeAlias.Focus();
			}
		}
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		if (cbFind.Text == null || c1FlexGrid1.Rows.Count <= 1)
		{
			return;
		}
		int iStart = c1FlexGrid1.Row + 1;
		string sSearchText = cbFind.Text.Trim();
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
			iStart = c1FlexGrid1.Row + 1;
		}
		if (sSearchText.Trim() == "")
		{
			return;
		}
		for (int i = iStart; i < c1FlexGrid1.Rows.Count; i++)
		{
			for (int j = 1; j < c1FlexGrid1.Cols.Count; j++)
			{
				if (c1FlexGrid1[i, j] == null || c1FlexGrid1[i, j].ToString().ToUpper().IndexOf(sSearchText.ToUpper()) <= -1)
				{
					continue;
				}
				c1FlexGrid1.Row = i;
				c1FlexGrid1.Select();
				int iFondCount = 0;
				int iListCount = cbFind.Items.Count;
				for (int k = 0; k < iListCount; k++)
				{
					if (cbFind.Items[k].DisplayText.Trim() == sSearchText.Trim())
					{
						iFondCount++;
					}
				}
				if (iFondCount == 0)
				{
					cbFind.Items.Add(sSearchText, sSearchText);
				}
				return;
			}
		}
	}

	private void cbFind_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ultraButton1_Click(sender, e);
		}
	}

	private void J_Btn_Prev_Click(object sender, EventArgs e)
	{
		switch (OptionSet)
		{
		case 1:
			Tab_A.Tab.Selected = true;
			break;
		case 2:
			Tab_A.Tab.Selected = true;
			break;
		case 3:
			Tab_A.Tab.Selected = true;
			break;
		case 4:
			Tab_A.Tab.Selected = true;
			break;
		}
	}

	private void J_Btn_Next_Click(object sender, EventArgs e)
	{
		if (txtProjectCode.Text.Trim() == "")
		{
			MessageBox.Show(this, "請先挑選一個動支單號。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (OptionSet == 1)
		{
			int iAction = InsertProjectToDB();
			if (iAction == -2)
			{
				MessageBox.Show(this, "已經有相同 [專案代號] 資料存在，\n請重新載入專案目錄。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
		}
		if (OptionSet == 3)
		{
			int iAction = InsertProjectToDB();
			if (iAction == -2)
			{
				MessageBox.Show(this, "已經有相同 [專案代號] 資料存在，\n請重新載入專案目錄。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Archnowledge.Pcces.DomainModule.General.PubProject pubProject = new Archnowledge.Pcces.DomainModule.General.PubProject();
			DataSet ds = pubProject.GetProjectList(F_UserID);
			DataView dv = ds.Tables[0].DefaultView;
			dv.RowFilter = "Bud IS NOT NULL AND (MainProj IS NULL OR TRIM(MainProj)='' OR TRIM(MainProj)=TRIM(ProjectCode))";
			CellStyle CS8 = c1FlexGrid1.Styles.Add("NoProjectAuth");
			CS8.ForeColor = Color.Gray;
			c1FlexGrid1.Rows.Count = dv.Count;
			for (int i = 0; i < dv.Count; i++)
			{
				c1FlexGrid1[i, "ProjectCode"] = dv[i]["ProjectCode"].ToString().Trim();
				c1FlexGrid1[i, "projCName"] = dv[i]["projCName"].ToString().Trim();
				c1FlexGrid1[i, "projAddress"] = dv[i]["ProjectAddress"].ToString().Trim();
				if (!ArchConvert.Obj2Bool(dv[i]["Auth"]))
				{
					c1FlexGrid1.Rows[i].Style = c1FlexGrid1.Styles["NoProjectAuth"];
				}
				c1FlexGrid1.AutoSizeRow(i);
			}
		}
		if (OptionSet == 4)
		{
			int iAction = InsertProjectToDB();
			if (iAction == -2)
			{
				MessageBox.Show(this, "已經有相同 [專案代號] 資料存在，\n請重新載入專案目錄。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
		}
		switch (OptionSet)
		{
		case 1:
			Tab_F.Tab.Selected = true;
			break;
		case 2:
			Tab_E.Tab.Selected = true;
			break;
		case 3:
			Tab_C.Tab.Selected = true;
			c1FlexGrid1.Select();
			break;
		case 4:
			Tab_G.Tab.Selected = true;
			break;
		}
	}

	private void GridRail1_AfterSelChange(object sender, RangeEventArgs e)
	{
		txtProjectCode.Text = GridRail1[GridRail1.Row, "MainCode"].ToString();
		txtProjectCName.Text = GridRail1[GridRail1.Row, "ProjNameC"].ToString();
	}

	private void btnJ_LoadEXCEL_Click(object sender, EventArgs e)
	{
		openFileDialog1.RestoreDirectory = true;
		openFileDialog1.Filter = "AA系統動支單號檔(*.xls)|*.xls";
		if (openFileDialog1.ShowDialog() == DialogResult.OK)
		{
			txtAA.Text = openFileDialog1.FileName;
		}
	}

	private void ultraButton5_Click(object sender, EventArgs e)
	{
		if (txtAA.Text.Trim() == "")
		{
			MessageBox.Show(this, "請先挑選欲轉入的檔案。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (txtAA.Text.Trim().ToUpper().IndexOf(".XLS") <= 0)
		{
			MessageBox.Show(this, "您挑選的檔案不是有效的EXCEL檔案，請重新挑選。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		try
		{
			C1XLBook C1Book1 = new C1XLBook();
			C1Book1.Load(txtAA.Text);
			for (int i = 1; i < 65535; i++)
			{
				XLCell Cell1 = C1Book1.Sheets[0].GetCell(i, 0);
				XLCell Cell2 = C1Book1.Sheets[0].GetCell(i, 1);
				XLCell Cell3 = C1Book1.Sheets[0].GetCell(i, 2);
				if (Cell1 == null || Cell1.Value.ToString() == "")
				{
					break;
				}
				try
				{
					GridRail1.Rows.Count = i + 1;
				}
				catch (Exception ex)
				{
					CommonMethods.LogFile("Pcces46", "M", "Project.formNewProjectWizard.cs" + ex.Message);
				}
				GridRail1[i, "MainCode"] = Cell1.Value.ToString();
				GridRail1[i, "PCode"] = Cell2.Value.ToString();
				GridRail1[i, "ProjNameC"] = Cell3.Value.ToString();
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Project.formNewProjectWizard.cs" + ex.Message);
			MessageBox.Show(this, "檔案內容有誤\n\n" + ex.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void ultraButton11_Click(object sender, EventArgs e)
	{
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("【併標】專案挑選--" + txtProjectCode.Text.Trim() + "(" + IPStr + ")");
		Archnowledge.Pcces.BUDClass.ItemA ItemACom = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
		ItemACom.ps_srckind = "BUD";
		Archnowledge.Pcces.BUDClass.Project ProjCom = new Archnowledge.Pcces.BUDClass.Project(aArr);
		ProjCom.ps_srckind = "BUD";
		FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
		FM_INFO._InfoString = "併標中，請稍候…! ";
		FM_INFO.Show();
		Application.DoEvents();
		Prog1.Minimum = 0;
		Prog1.Maximum = GridDestination.Rows.Count - 1;
		Prog1.Value = 0;
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PROJ.ps_projectCode = txtProjectCode.Text.Trim();
		PROJ.ps_srckind = "bud";
		PROJ.ps_projectNameC = txtProjectCName.Text;
		PROJ.ps_projectNameE = txtProjectEName.Text;
		PROJ.ps_projectAddress = txtProjectAddress.Text;
		PROJ.InseItem();
		for (int i = 1; i < GridDestination.Rows.Count; i++)
		{
			Prog1.Value++;
			string exp_projcode = GridDestination[i, "ProjectCode"].ToString();
			string imp_projcode = txtProjectCode.Text.Trim();
			ItemACom.CopyItemA(imp_projcode, exp_projcode);
			ProjCom.ps_projectCode = exp_projcode;
			ProjCom.ps_mainProj = imp_projcode;
			ProjCom.UpdItem();
			Application.DoEvents();
		}
		FM_INFO.Close();
		FM_INFO.Dispose();
		ItemACom = null;
		ProjCom = null;
		PROJ = null;
		base.MaximizeBox = false;
		base.WindowState = FormWindowState.Normal;
		PubTools.WriteRoughlyLog(aArr);
		aArr = null;
		Tab_F.Tab.Selected = true;
	}

	private void BtnAll_Click(object sender, EventArgs e)
	{
		string sStr = "";
		for (int i = GridSource.Rows.Count - 1; i > 0; i--)
		{
			sStr = GridSource[i, 0].ToString() + "\t" + GridSource[i, 1].ToString();
			GridDestination.AddItem(sStr, 1);
			GridSource.RemoveItem(i);
		}
		GridSource.AutoSizeCols();
		GridDestination.AutoSizeCols();
	}

	private void ultraButton8_Click(object sender, EventArgs e)
	{
		string sStr = "";
		for (int i = GridSource.Rows.Count - 1; i > 0; i--)
		{
			if (GridSource.Rows[i].Selected)
			{
				sStr = GridSource[i, 0].ToString() + "\t" + GridSource[i, 1].ToString();
				GridDestination.AddItem(sStr);
				GridSource.RemoveItem(i);
			}
		}
		GridSource.AutoSizeCols();
		GridDestination.AutoSizeCols();
	}

	private void ultraButton7_Click(object sender, EventArgs e)
	{
		if (GridDestination.SelectedRowCount == 0)
		{
			string sWarning = "請先選取要移除的專案!";
			MessageBox.Show(this, sWarning, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		string sStr = "";
		for (int i = GridDestination.Rows.Count - 1; i > 0; i--)
		{
			if (GridDestination.Rows[i].Selected)
			{
				sStr = GridDestination[i, 0].ToString() + "\t" + GridDestination[i, 1].ToString();
				GridSource.AddItem(sStr);
				GridDestination.RemoveItem(i);
			}
		}
		GridSource.AutoSizeCols();
		GridDestination.AutoSizeCols();
	}

	private void ultraButton9_Click(object sender, EventArgs e)
	{
		string sStr = "";
		for (int i = GridDestination.Rows.Count - 1; i > 0; i--)
		{
			sStr = GridDestination[i, 0].ToString() + "\t" + GridDestination[i, 1].ToString();
			GridSource.AddItem(sStr, 1);
			GridDestination.RemoveItem(i);
		}
		GridSource.AutoSizeCols();
		GridDestination.AutoSizeCols();
	}

	private void ultraButton6_Click(object sender, EventArgs e)
	{
		ArrayList SelItems = new ArrayList();
		int iIdx = -1;
		for (int i = 1; i < GridDestination.Rows.Count; i++)
		{
			if (GridDestination.Rows[i].Selected)
			{
				SelItems.Add(GridDestination[i, "ProjectCode"]);
			}
		}
		for (int i = 0; i < SelItems.Count; i++)
		{
			iIdx = GridDestination.FindRow((string)SelItems[i], 1, GridDestination.Cols["ProjectCode"].SafeIndex, wrap: false);
			if (iIdx == 1)
			{
				break;
			}
			if (iIdx > -1)
			{
				GridDestination.Rows[iIdx].Move(iIdx - 1);
			}
		}
		for (int i = 0; i < SelItems.Count; i++)
		{
			GridDestination.Rows[Get_RealRow2(SelItems[i].ToString())].Selected = true;
		}
	}

	private void ultraButton4_Click(object sender, EventArgs e)
	{
		ArrayList SelItems = new ArrayList();
		int iIdx = -1;
		for (int i = 1; i < GridDestination.Rows.Count; i++)
		{
			if (GridDestination.Rows[i].Selected)
			{
				SelItems.Add(GridDestination[i, "ProjectCode"]);
			}
		}
		for (int i = SelItems.Count - 1; i >= 0; i--)
		{
			iIdx = GridDestination.FindRow((string)SelItems[i], 1, GridDestination.Cols["ProjectCode"].SafeIndex, wrap: false);
			if (iIdx == GridDestination.Rows.Count - 1)
			{
				break;
			}
			if (iIdx > -1)
			{
				GridDestination.Rows[iIdx].Move(iIdx + 1);
			}
		}
		for (int i = 0; i < SelItems.Count; i++)
		{
			GridDestination.Rows[Get_RealRow2(SelItems[i].ToString())].Selected = true;
		}
	}

	private int Get_RealRow2(string sPubCode)
	{
		int RetV = -1;
		for (int i = 1; i < GridDestination.Rows.Count; i++)
		{
			if (GridDestination[i, "ProjectCode"].ToString() == sPubCode)
			{
				RetV = i;
				break;
			}
		}
		return RetV;
	}

	private void ultraButton12_Click(object sender, EventArgs e)
	{
		DeleteNewProject();
		Tab_B.Tab.Selected = true;
	}

	private void txtProjectCode_Leave(object sender, EventArgs e)
	{
		string projectName = txtProjectCode.Text.Trim();
		if (projectName.IndexOf("\\") > -1)
		{
			projectName = projectName.Replace("\\", "_");
		}
		if (projectName.IndexOf(":") > -1)
		{
			projectName = projectName.Replace(":", "_");
		}
		if (projectName.IndexOf("/") > -1)
		{
			projectName = projectName.Replace("/", "_");
		}
		if (projectName.IndexOf("*") > -1)
		{
			projectName = projectName.Replace("*", "_");
		}
		if (projectName.IndexOf("?") > -1)
		{
			projectName = projectName.Replace("?", "_");
		}
		if (projectName.IndexOf("<") > -1)
		{
			projectName = projectName.Replace("<", "_");
		}
		if (projectName.IndexOf(">") > -1)
		{
			projectName = projectName.Replace(">", "_");
		}
		if (projectName.IndexOf("|") > -1)
		{
			projectName = projectName.Replace("|", "_");
		}
		txtProjectCode.Text = projectName;
		if (!CommonMethods.IsStrByteLenValid(txtProjectCode.Text, 40))
		{
			MessageBox.Show(this, lblProjectCode.Text + "的長度不可超過 40 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtProjectCode.Focus();
		}
	}

	private void formNewProjectWizard_FormClosed(object sender, FormClosedEventArgs e)
	{
		WizardTabs = null;
		ultraTabSharedControlsPage1 = null;
		panel2 = null;
		Tab_A = null;
		Tab_B = null;
		Tab_C = null;
		Tab_D = null;
		Tab_E = null;
		Tab_F = null;
		ultraLabel1 = null;
		ultraLabel2 = null;
		RB1 = null;
		RB2 = null;
		RB3 = null;
		ultraLabel3 = null;
		ultraLabel4 = null;
		ultraLabel5 = null;
		panel1 = null;
		A_Btn_Prev = null;
		A_Btn_Next = null;
		A_Btn_Cncl = null;
		panel3 = null;
		B_Btn_Cncl = null;
		B_Btn_Next = null;
		B_Btn_Prev = null;
		panel4 = null;
		panel5 = null;
		groupBox1 = null;
		groupBox2 = null;
		ultraLabel6 = null;
		ultraLabel7 = null;
		ultraLabel9 = null;
		txtProjectCode = null;
		txtProjectCName = null;
		txtProjectEName = null;
		ultraLabel10 = null;
		txtProjectAddress = null;
		ultraLabel11 = null;
		panel6 = null;
		groupBox3 = null;
		F_Btn_Fnsh = null;
		F_Btn_Prev = null;
		ultraLabel12 = null;
		ultraLabel13 = null;
		ultraLabel14 = null;
		panel7 = null;
		ultraLabel15 = null;
		ultraLabel16 = null;
		panel8 = null;
		groupBox4 = null;
		panel9 = null;
		ultraLabel17 = null;
		txtPxfin = null;
		BtnChgDir = null;
		openFileDialog1 = null;
		E_Btn_Cncl = null;
		E_Btn_Next = null;
		E_Btn_Prev = null;
		panel10 = null;
		ultraLabel18 = null;
		ultraLabel19 = null;
		panel12 = null;
		groupBox5 = null;
		C_Btn_Cncl = null;
		C_Btn_Next = null;
		C_Btn_Prev = null;
		panel11 = null;
		ultraButton2 = null;
		panel13 = null;
		c1FlexGrid1 = null;
		panel14 = null;
		ultraLabel20 = null;
		ultraLabel21 = null;
		ultraLabel22 = null;
		ultraLabel23 = null;
		ultraLabel24 = null;
		cbFind = null;
		imageList1 = null;
		ultraButton1 = null;
		panel15 = null;
		ultraLabel25 = null;
		ultraLabel26 = null;
		panel16 = null;
		groupBox6 = null;
		D_Btn_Cncl = null;
		D_Btn_Next = null;
		D_Btn_Prev = null;
		panel17 = null;
		panel18 = null;
		lblTitle = null;
		c1FlexGrid2 = null;
		DT_bud = null;
		GC.Collect();
	}

	private void txtProjectCName_Leave(object sender, EventArgs e)
	{
		string projectName = txtProjectCName.Text.Trim();
		if (projectName.IndexOf("\\") > -1)
		{
			projectName = projectName.Replace("\\", "_");
		}
		if (projectName.IndexOf(":") > -1)
		{
			projectName = projectName.Replace(":", "_");
		}
		if (projectName.IndexOf("/") > -1)
		{
			projectName = projectName.Replace("/", "_");
		}
		if (projectName.IndexOf("*") > -1)
		{
			projectName = projectName.Replace("*", "_");
		}
		if (projectName.IndexOf("?") > -1)
		{
			projectName = projectName.Replace("?", "_");
		}
		if (projectName.IndexOf("<") > -1)
		{
			projectName = projectName.Replace("<", "_");
		}
		if (projectName.IndexOf(">") > -1)
		{
			projectName = projectName.Replace(">", "_");
		}
		if (projectName.IndexOf("|") > -1)
		{
			projectName = projectName.Replace("|", "_");
		}
		txtProjectCName.Text = projectName;
	}

	private void B_Btn_Cncl_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
	}
}
