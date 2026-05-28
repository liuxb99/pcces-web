using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.DomainModule.Coms;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.DomainModule.MrsBase;
using Archnowledge.Pcces.PccesMain.Budget;
using Archnowledge.Pcces.PccesMain.Budget.ItemNoset;
using Archnowledge.Pcces.PccesMain.Library;
using Archnowledge.Pcces.STDClass;
using AxPVLINE3DLib;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinMaskedEdit;

namespace Archnowledge.Pcces.PccesMain.MrsBase;

public class FormMrsBaseEdit : Form
{
	private IContainer components;

	private Panel panel1;

	private Panel panel2;

	private GroupBox groupBox3;

	private UltraLabel ultraLabel8;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel2;

	private UltraLabel lblCost_Rate;

	private GroupBox groupBox2;

	private GroupBox groupBox1;

	private Panel panel3;

	private Panel panel4;

	private GroupBox groupBox4;

	private UltraLabel ultraLabel9;

	private UltraLabel ultraLabel10;

	private UltraLabel ultraLabel11;

	private UltraLabel ultraLabel12;

	private UltraTextEditor txtPccesCode;

	private UltraLabel lblCode;

	private UltraTextEditor txtCName;

	private UltraTextEditor txtEName;

	private UltraLabel ultraLabel14;

	private UltraTextEditor txtMemo;

	private UltraLabel ultraLabel15;

	private UltraButton BtnCancel;

	private UltraButton BtnOK;

	private UltraComboEditor cboCostKind;

	private UltraComboEditor cboType;

	private UltraTextEditor txtExtendCode;

	private UltraCombo cboCUnit;

	private UltraCombo cboEUnit;

	private UltraOptionSet ItemTypeOp;

	private UltraComboEditor cboAnalysis;

	private UltraButton BtnPickPrice;

	private UltraCombo cboHisPrice;

	private UltraButton ultraButton1;

	private Panel panel5;

	private UltraLabel ultraLabel13;

	private AxPVLine3D axPVLine3D1;

	private Panel panel6;

	private AxPVLine3D axPVLine3D2;

	private Panel panel7;

	private UltraLabel ultraLabel16;

	private UltraLabel ultraLabel17;

	private UltraLabel ultraLabel24;

	private UltraLabel ultraLabel25;

	private UltraLabel ultraLabel26;

	private UltraLabel lblHisUpper;

	private UltraLabel lblHisLower;

	private UltraLabel lblHisAvg;

	private UltraLabel lblCesUpper;

	private UltraLabel lblCesLower;

	private UltraLabel lblCesAvg;

	private UltraNumericEditor txtCost_Rate;

	private UltraNumericEditor txtLRate;

	private UltraNumericEditor txtERate;

	private UltraNumericEditor txtMRate;

	private UltraNumericEditor txtWRate;

	private Control Cntrl1;

	private FormSymbol Frm = new FormSymbol();

	private Label lblAnalysisQty;

	private UltraLabel ultraLabel18;

	private UltraButton ultraButton2;

	private UltraTextEditor txtClass;

	private UltraTextEditor txtSurName;

	private UltraLabel ultraLabel19;

	private UltraComboEditor cboCodeLength;

	private string F_OnLineServerName;

	private int iTextBeamPos = 0;

	private string F_Cstring;

	private double F_ExternalCost;

	private string F_UserID;

	private string F_class;

	private string F_Mesbox;

	private string F_chgCount;

	private string F_Record;

	private string F_OriginalPccescode = "";

	private string F_ItemClassflag = "";

	private int F_FormHeight = 517;

	private MrsBaseEditFormType F_EditMode;

	private int iCodeLength = 0;

	private FormStatus FormStatus = FormStatus.Iinitial;

	private int iPubCode = -1;

	private string F_MainCst = "";

	private string F_CallerFormName = "";

	private PccesFormAction F_ActionName;

	private string F_ProjectCode = "";

	private string F_flag = "";

	private int F_sNO = -1;

	private string F_ParentCode;

	private Archnowledge.Pcces.BUDClass.MrsBaseA dbMrsBaseA;

	private string F_InitialRate = "0";

	private bool F_Istemplate = false;

	private bool F_IsLockAn = false;

	private bool IsCommonItem = false;

	private string area;

	private bool AllowEditCost = false;

	private string ItemCName;

	private string ItemPccesCode;

	private string ItemCostKind;

	private string ItemCost;

	private string ItemQty;

	private string ItemUnitName;

	private string F_costKind;

	private bool F_IsLocked = false;

	private bool F_IsSubmitBid = false;

	private string DefaultFormatMaskInput = "nnn.nn";

	private string CustomFormatMaskInput = "-n,nnn,nnn,nnn,nnn,nnn.nn";

	private string CostEstImpWorkItemProcessType;

	private DataSet dsUpdWorkItem;

	private bool FirstFocus;

	public bool _IsLocked
	{
		get
		{
			return F_IsLocked;
		}
		set
		{
			F_IsLocked = value;
		}
	}

	public string _costKind => F_costKind;

	public string _OnLineServerName
	{
		set
		{
			F_OnLineServerName = value;
		}
	}

	public double _ExternalCost
	{
		get
		{
			return F_ExternalCost;
		}
		set
		{
			F_ExternalCost = value;
		}
	}

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

	public string _Cstring
	{
		get
		{
			return F_Cstring;
		}
		set
		{
			F_Cstring = value;
		}
	}

	public string _ParentCode
	{
		get
		{
			return F_ParentCode;
		}
		set
		{
			F_ParentCode = value;
		}
	}

	public int _sNO
	{
		get
		{
			return F_sNO;
		}
		set
		{
			F_sNO = value;
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
		}
	}

	public string _CallerFormName
	{
		get
		{
			return F_CallerFormName;
		}
		set
		{
			F_CallerFormName = value;
		}
	}

	public string _MainCost
	{
		get
		{
			return F_MainCst;
		}
		set
		{
			F_MainCst = value;
		}
	}

	public string _Class
	{
		get
		{
			return F_class;
		}
		set
		{
			F_class = value;
		}
	}

	public string _Mesbox
	{
		get
		{
			return F_Mesbox;
		}
		set
		{
			F_Mesbox = value;
		}
	}

	public string _chgCount
	{
		get
		{
			return F_chgCount;
		}
		set
		{
			F_chgCount = value;
		}
	}

	public bool _Istemplate
	{
		get
		{
			return F_Istemplate;
		}
		set
		{
			F_Istemplate = value;
		}
	}

	public bool _IsLockAn
	{
		get
		{
			return F_IsLockAn;
		}
		set
		{
			F_IsLockAn = value;
		}
	}

	public bool _AllowEditCost
	{
		get
		{
			return AllowEditCost;
		}
		set
		{
			AllowEditCost = value;
		}
	}

	public bool _IsSubmitBid
	{
		get
		{
			return F_IsSubmitBid;
		}
		set
		{
			F_IsSubmitBid = value;
		}
	}

	public MrsBaseEditFormType _EditMode
	{
		get
		{
			return F_EditMode;
		}
		set
		{
			F_EditMode = value;
		}
	}

	public int _PubCode
	{
		get
		{
			return iPubCode;
		}
		set
		{
			iPubCode = value;
		}
	}

	public DataSet _dsUpdWorkItem
	{
		get
		{
			return dsUpdWorkItem;
		}
		set
		{
			dsUpdWorkItem = value;
		}
	}

	public string _ItemPccesCode
	{
		get
		{
			return ItemPccesCode;
		}
		set
		{
			ItemPccesCode = value;
		}
	}

	public string _ItemcName
	{
		get
		{
			return ItemCName;
		}
		set
		{
			ItemCName = value;
		}
	}

	public string _ItemCost
	{
		get
		{
			return ItemCost;
		}
		set
		{
			ItemCost = value;
		}
	}

	public string _ItemCostKind
	{
		get
		{
			return ItemCostKind;
		}
		set
		{
			ItemCostKind = value;
		}
	}

	public string _ItemQty
	{
		get
		{
			return ItemQty;
		}
		set
		{
			ItemQty = value;
		}
	}

	public string _ItemUnitName
	{
		get
		{
			return ItemUnitName;
		}
		set
		{
			ItemUnitName = value;
		}
	}

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinGrid.UltraGridLayout ultraGridLayout1 = new Infragistics.Win.UltraWinGrid.UltraGridLayout();
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(86092282);
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
		Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem6 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem7 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem8 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem9 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem10 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem11 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem12 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem13 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem14 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem15 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem16 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem17 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem18 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.MrsBase.FormMrsBaseEdit));
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.txtSurName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel19 = new Infragistics.Win.Misc.UltraLabel();
		this.txtClass = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel18 = new Infragistics.Win.Misc.UltraLabel();
		this.cboEUnit = new Infragistics.Win.UltraWinGrid.UltraCombo();
		this.cboCUnit = new Infragistics.Win.UltraWinGrid.UltraCombo();
		this.txtExtendCode = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel15 = new Infragistics.Win.Misc.UltraLabel();
		this.txtMemo = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.txtEName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtCName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lblCode = new Infragistics.Win.Misc.UltraLabel();
		this.txtPccesCode = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.txtWRate = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
		this.txtMRate = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
		this.txtERate = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
		this.txtLRate = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
		this.txtCost_Rate = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
		this.BtnPickPrice = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.lblCost_Rate = new Infragistics.Win.Misc.UltraLabel();
		this.cboHisPrice = new Infragistics.Win.UltraWinGrid.UltraCombo();
		this.cboCodeLength = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.cboAnalysis = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.cboCostKind = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.cboType = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.ItemTypeOp = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.panel3 = new System.Windows.Forms.Panel();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraLabel26 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel25 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel24 = new Infragistics.Win.Misc.UltraLabel();
		this.panel7 = new System.Windows.Forms.Panel();
		this.lblCesAvg = new Infragistics.Win.Misc.UltraLabel();
		this.lblCesLower = new Infragistics.Win.Misc.UltraLabel();
		this.lblCesUpper = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel17 = new Infragistics.Win.Misc.UltraLabel();
		this.axPVLine3D2 = new AxPVLINE3DLib.AxPVLine3D();
		this.panel6 = new System.Windows.Forms.Panel();
		this.lblHisAvg = new Infragistics.Win.Misc.UltraLabel();
		this.lblHisLower = new Infragistics.Win.Misc.UltraLabel();
		this.lblHisUpper = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel16 = new Infragistics.Win.Misc.UltraLabel();
		this.axPVLine3D1 = new AxPVLINE3DLib.AxPVLine3D();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.panel4 = new System.Windows.Forms.Panel();
		this.lblAnalysisQty = new System.Windows.Forms.Label();
		this.BtnOK = new Infragistics.Win.Misc.UltraButton();
		this.BtnCancel = new Infragistics.Win.Misc.UltraButton();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtSurName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtClass).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboEUnit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCUnit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExtendCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPccesCode).BeginInit();
		this.panel2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtWRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtERate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCost_Rate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboHisPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCodeLength).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboAnalysis).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCostKind).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboType).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ItemTypeOp).BeginInit();
		this.panel3.SuspendLayout();
		this.groupBox4.SuspendLayout();
		this.panel5.SuspendLayout();
		this.panel7.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.axPVLine3D2).BeginInit();
		this.panel6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.axPVLine3D1).BeginInit();
		this.panel4.SuspendLayout();
		base.SuspendLayout();
		this.panel1.Controls.Add(this.txtSurName);
		this.panel1.Controls.Add(this.ultraLabel19);
		this.panel1.Controls.Add(this.txtClass);
		this.panel1.Controls.Add(this.ultraButton2);
		this.panel1.Controls.Add(this.ultraLabel18);
		this.panel1.Controls.Add(this.cboEUnit);
		this.panel1.Controls.Add(this.cboCUnit);
		this.panel1.Controls.Add(this.txtExtendCode);
		this.panel1.Controls.Add(this.ultraLabel15);
		this.panel1.Controls.Add(this.txtMemo);
		this.panel1.Controls.Add(this.ultraLabel14);
		this.panel1.Controls.Add(this.txtEName);
		this.panel1.Controls.Add(this.txtCName);
		this.panel1.Controls.Add(this.lblCode);
		this.panel1.Controls.Add(this.txtPccesCode);
		this.panel1.Controls.Add(this.ultraLabel12);
		this.panel1.Controls.Add(this.ultraLabel11);
		this.panel1.Controls.Add(this.ultraLabel10);
		this.panel1.Controls.Add(this.ultraLabel9);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(635, 169);
		this.panel1.TabIndex = 3;
		this.txtSurName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtSurName.AutoSize = true;
		this.txtSurName.Location = new System.Drawing.Point(115, 139);
		this.txtSurName.Name = "txtSurName";
		this.txtSurName.Size = new System.Drawing.Size(510, 21);
		this.txtSurName.TabIndex = 39;
		this.ultraLabel19.Location = new System.Drawing.Point(12, 143);
		this.ultraLabel19.Name = "ultraLabel19";
		this.ultraLabel19.Size = new System.Drawing.Size(100, 20);
		this.ultraLabel19.TabIndex = 38;
		this.ultraLabel19.Text = "別名:";
		this.txtClass.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtClass.AutoSize = true;
		this.txtClass.Enabled = false;
		this.txtClass.Location = new System.Drawing.Point(115, 112);
		this.txtClass.Name = "txtClass";
		this.txtClass.Size = new System.Drawing.Size(490, 21);
		this.txtClass.TabIndex = 37;
		appearance1.FontData.Name = "Arial";
		this.ultraButton2.Appearance = appearance1;
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton2.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton2.Location = new System.Drawing.Point(605, 112);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(22, 24);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 36;
		this.ultraButton2.Text = "...";
		this.ultraButton2.Click += new System.EventHandler(ultraButton2_Click);
		this.ultraLabel18.Location = new System.Drawing.Point(13, 114);
		this.ultraLabel18.Name = "ultraLabel18";
		this.ultraLabel18.Size = new System.Drawing.Size(94, 20);
		this.ultraLabel18.TabIndex = 14;
		this.ultraLabel18.Text = "類別歸屬:";
		this.cboEUnit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cboEUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
		this.cboEUnit.DisplayMember = "";
		this.cboEUnit.Location = new System.Drawing.Point(527, 60);
		this.cboEUnit.Name = "cboEUnit";
		this.cboEUnit.Size = new System.Drawing.Size(97, 24);
		this.cboEUnit.TabIndex = 13;
		this.cboEUnit.ValueMember = "";
		this.cboEUnit.Validating += new System.ComponentModel.CancelEventHandler(cboEUnit_Validating);
		this.cboCUnit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cboCUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
		this.cboCUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Dotted;
		this.cboCUnit.DisplayLayout.BorderStyleCaption = Infragistics.Win.UIElementBorderStyle.Dashed;
		this.cboCUnit.DisplayMember = "";
		ultraGridLayout1.AutoFitColumns = true;
		valueList1.Key = "cString";
		ultraGridLayout1.ValueLists.Add(valueList1);
		this.cboCUnit.Layouts.Add(ultraGridLayout1);
		this.cboCUnit.Location = new System.Drawing.Point(527, 34);
		this.cboCUnit.Name = "cboCUnit";
		this.cboCUnit.Size = new System.Drawing.Size(97, 24);
		this.cboCUnit.TabIndex = 12;
		this.cboCUnit.ValueMember = "";
		this.cboCUnit.Validating += new System.ComponentModel.CancelEventHandler(cboEUnit_Validating);
		this.txtExtendCode.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtExtendCode.AutoSize = true;
		this.txtExtendCode.Location = new System.Drawing.Point(391, 7);
		this.txtExtendCode.MaxLength = 20;
		this.txtExtendCode.Name = "txtExtendCode";
		this.txtExtendCode.Size = new System.Drawing.Size(233, 21);
		this.txtExtendCode.TabIndex = 11;
		this.txtExtendCode.Validating += new System.ComponentModel.CancelEventHandler(txtPccesCode_Validating);
		this.ultraLabel15.Location = new System.Drawing.Point(483, 36);
		this.ultraLabel15.Name = "ultraLabel15";
		this.ultraLabel15.Size = new System.Drawing.Size(47, 20);
		this.ultraLabel15.TabIndex = 10;
		this.ultraLabel15.Text = "單位:";
		this.txtMemo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtMemo.AutoSize = true;
		this.txtMemo.Location = new System.Drawing.Point(115, 86);
		this.txtMemo.Name = "txtMemo";
		this.txtMemo.Size = new System.Drawing.Size(510, 21);
		this.txtMemo.TabIndex = 9;
		this.txtMemo.Validating += new System.ComponentModel.CancelEventHandler(txtPccesCode_Validating);
		this.ultraLabel14.Location = new System.Drawing.Point(483, 62);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(47, 20);
		this.ultraLabel14.TabIndex = 8;
		this.ultraLabel14.Text = "Unit:";
		this.txtEName.AutoSize = true;
		this.txtEName.Location = new System.Drawing.Point(115, 60);
		this.txtEName.Name = "txtEName";
		this.txtEName.Size = new System.Drawing.Size(364, 21);
		this.txtEName.TabIndex = 7;
		this.txtEName.Validating += new System.ComponentModel.CancelEventHandler(txtPccesCode_Validating);
		this.txtCName.AutoSize = true;
		this.txtCName.Location = new System.Drawing.Point(115, 34);
		this.txtCName.Name = "txtCName";
		this.txtCName.Size = new System.Drawing.Size(364, 21);
		this.txtCName.TabIndex = 6;
		this.txtCName.Validating += new System.ComponentModel.CancelEventHandler(txtPccesCode_Validating);
		this.lblCode.Location = new System.Drawing.Point(312, 11);
		this.lblCode.Name = "lblCode";
		this.lblCode.Size = new System.Drawing.Size(81, 20);
		this.lblCode.TabIndex = 5;
		this.lblCode.Text = "工項外碼:";
		this.txtPccesCode.AutoSize = true;
		this.txtPccesCode.Location = new System.Drawing.Point(115, 7);
		this.txtPccesCode.MaxLength = 20;
		this.txtPccesCode.Name = "txtPccesCode";
		this.txtPccesCode.Size = new System.Drawing.Size(173, 21);
		this.txtPccesCode.TabIndex = 4;
		this.txtPccesCode.Validating += new System.ComponentModel.CancelEventHandler(txtPccesCode_Validating);
		this.txtPccesCode.Leave += new System.EventHandler(txtPccesCode_Leave);
		this.txtPccesCode.KeyUp += new System.Windows.Forms.KeyEventHandler(txtPccesCode_KeyUp);
		this.ultraLabel12.Location = new System.Drawing.Point(12, 89);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(100, 20);
		this.ultraLabel12.TabIndex = 3;
		this.ultraLabel12.Text = "備註:";
		this.ultraLabel11.Location = new System.Drawing.Point(12, 62);
		this.ultraLabel11.Name = "ultraLabel11";
		this.ultraLabel11.Size = new System.Drawing.Size(100, 20);
		this.ultraLabel11.TabIndex = 2;
		this.ultraLabel11.Text = "Description:";
		this.ultraLabel10.Location = new System.Drawing.Point(12, 37);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(100, 20);
		this.ultraLabel10.TabIndex = 1;
		this.ultraLabel10.Text = "工項名稱:";
		this.ultraLabel9.Location = new System.Drawing.Point(12, 11);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(100, 20);
		this.ultraLabel9.TabIndex = 0;
		this.ultraLabel9.Text = "工項代碼:";
		this.panel2.Controls.Add(this.groupBox3);
		this.panel2.Controls.Add(this.groupBox2);
		this.panel2.Controls.Add(this.groupBox1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 169);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(635, 168);
		this.panel2.TabIndex = 4;
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox3.Controls.Add(this.txtWRate);
		this.groupBox3.Controls.Add(this.txtMRate);
		this.groupBox3.Controls.Add(this.txtERate);
		this.groupBox3.Controls.Add(this.txtLRate);
		this.groupBox3.Controls.Add(this.txtCost_Rate);
		this.groupBox3.Controls.Add(this.BtnPickPrice);
		this.groupBox3.Controls.Add(this.ultraLabel8);
		this.groupBox3.Controls.Add(this.ultraLabel7);
		this.groupBox3.Controls.Add(this.ultraLabel6);
		this.groupBox3.Controls.Add(this.ultraLabel1);
		this.groupBox3.Controls.Add(this.ultraLabel5);
		this.groupBox3.Controls.Add(this.ultraLabel4);
		this.groupBox3.Controls.Add(this.ultraLabel3);
		this.groupBox3.Controls.Add(this.ultraLabel2);
		this.groupBox3.Controls.Add(this.lblCost_Rate);
		this.groupBox3.Controls.Add(this.cboHisPrice);
		this.groupBox3.Controls.Add(this.cboCodeLength);
		this.groupBox3.ForeColor = System.Drawing.SystemColors.Highlight;
		this.groupBox3.Location = new System.Drawing.Point(347, 4);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(276, 161);
		this.groupBox3.TabIndex = 5;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "單價";
		this.txtWRate.Location = new System.Drawing.Point(123, 127);
		this.txtWRate.MaskDisplayMode = Infragistics.Win.UltraWinMaskedEdit.MaskMode.Raw;
		this.txtWRate.MaskInput = "-nnn.nn";
		this.txtWRate.Name = "txtWRate";
		this.txtWRate.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
		this.txtWRate.PromptChar = ' ';
		this.txtWRate.Size = new System.Drawing.Size(100, 21);
		this.txtWRate.TabIndex = 20;
		this.txtMRate.Location = new System.Drawing.Point(123, 100);
		this.txtMRate.MaskDisplayMode = Infragistics.Win.UltraWinMaskedEdit.MaskMode.Raw;
		this.txtMRate.MaskInput = "-nnn.nn";
		this.txtMRate.Name = "txtMRate";
		this.txtMRate.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
		this.txtMRate.PromptChar = ' ';
		this.txtMRate.Size = new System.Drawing.Size(100, 21);
		this.txtMRate.TabIndex = 19;
		this.txtERate.Location = new System.Drawing.Point(123, 74);
		this.txtERate.MaskDisplayMode = Infragistics.Win.UltraWinMaskedEdit.MaskMode.Raw;
		this.txtERate.MaskInput = "-nnn.nn";
		this.txtERate.Name = "txtERate";
		this.txtERate.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
		this.txtERate.PromptChar = ' ';
		this.txtERate.Size = new System.Drawing.Size(100, 21);
		this.txtERate.TabIndex = 18;
		this.txtLRate.Location = new System.Drawing.Point(123, 47);
		this.txtLRate.MaskDisplayMode = Infragistics.Win.UltraWinMaskedEdit.MaskMode.Raw;
		this.txtLRate.MaskInput = "-nnn.nn";
		this.txtLRate.Name = "txtLRate";
		this.txtLRate.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
		this.txtLRate.PromptChar = ' ';
		this.txtLRate.Size = new System.Drawing.Size(100, 21);
		this.txtLRate.TabIndex = 17;
		this.txtCost_Rate.Location = new System.Drawing.Point(123, 20);
		this.txtCost_Rate.Name = "txtCost_Rate";
		this.txtCost_Rate.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
		this.txtCost_Rate.PromptChar = ' ';
		this.txtCost_Rate.Size = new System.Drawing.Size(100, 21);
		this.txtCost_Rate.TabIndex = 16;
		this.txtCost_Rate.Leave += new System.EventHandler(txtCost_Rate_Leave);
		this.txtCost_Rate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCost_Rate_KeyPress);
		this.txtCost_Rate.Click += new System.EventHandler(txtCost_Rate_Click);
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnPickPrice.Appearance = appearance2;
		this.BtnPickPrice.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnPickPrice.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.BtnPickPrice.HotTracking = true;
		this.BtnPickPrice.Location = new System.Drawing.Point(423, 20);
		this.BtnPickPrice.Name = "BtnPickPrice";
		this.BtnPickPrice.ShowFocusRect = false;
		this.BtnPickPrice.ShowOutline = false;
		this.BtnPickPrice.Size = new System.Drawing.Size(29, 21);
		this.BtnPickPrice.SupportThemes = false;
		this.BtnPickPrice.TabIndex = 14;
		this.BtnPickPrice.Text = "挑選";
		this.BtnPickPrice.Visible = false;
		this.BtnPickPrice.Click += new System.EventHandler(BtnPickPrice_Click);
		this.ultraLabel8.Location = new System.Drawing.Point(225, 128);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(19, 23);
		this.ultraLabel8.TabIndex = 13;
		this.ultraLabel8.Text = "%";
		this.ultraLabel7.Location = new System.Drawing.Point(226, 102);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(19, 23);
		this.ultraLabel7.TabIndex = 12;
		this.ultraLabel7.Text = "%";
		this.ultraLabel6.Location = new System.Drawing.Point(226, 76);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(19, 23);
		this.ultraLabel6.TabIndex = 11;
		this.ultraLabel6.Text = "%";
		this.ultraLabel1.Location = new System.Drawing.Point(227, 50);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(19, 23);
		this.ultraLabel1.TabIndex = 10;
		this.ultraLabel1.Text = "%";
		appearance3.ForeColor = System.Drawing.Color.Black;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance3;
		this.ultraLabel5.Location = new System.Drawing.Point(13, 130);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(109, 23);
		this.ultraLabel5.TabIndex = 4;
		this.ultraLabel5.Text = "雜項比例(%)：";
		appearance4.ForeColor = System.Drawing.Color.Black;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance4;
		this.ultraLabel4.Location = new System.Drawing.Point(13, 104);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(109, 23);
		this.ultraLabel4.TabIndex = 3;
		this.ultraLabel4.Text = "材料比例(%)：";
		appearance5.ForeColor = System.Drawing.Color.Black;
		appearance5.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance5;
		this.ultraLabel3.Location = new System.Drawing.Point(13, 78);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(109, 23);
		this.ultraLabel3.TabIndex = 2;
		this.ultraLabel3.Text = "機具比例(%)：";
		appearance6.ForeColor = System.Drawing.Color.Black;
		appearance6.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance6;
		this.ultraLabel2.Location = new System.Drawing.Point(13, 51);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(109, 23);
		this.ultraLabel2.TabIndex = 1;
		this.ultraLabel2.Text = "人工比例(%)：";
		appearance7.ForeColor = System.Drawing.Color.Black;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblCost_Rate.Appearance = appearance7;
		this.lblCost_Rate.Location = new System.Drawing.Point(13, 25);
		this.lblCost_Rate.Name = "lblCost_Rate";
		this.lblCost_Rate.Size = new System.Drawing.Size(109, 23);
		this.lblCost_Rate.TabIndex = 0;
		this.lblCost_Rate.Text = "單價：";
		this.lblCost_Rate.TextChanged += new System.EventHandler(lblCost_Rate_TextChanged);
		this.cboHisPrice.AutoEdit = false;
		this.cboHisPrice.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
		ultraGridBand1.Override.TipStyleCell = Infragistics.Win.UltraWinGrid.TipStyle.Show;
		ultraGridBand1.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
		ultraGridBand1.UseRowLayout = true;
		this.cboHisPrice.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
		this.cboHisPrice.DisplayMember = "";
		this.cboHisPrice.Location = new System.Drawing.Point(324, 21);
		this.cboHisPrice.Name = "cboHisPrice";
		this.cboHisPrice.Size = new System.Drawing.Size(18, 24);
		this.cboHisPrice.TabIndex = 15;
		this.cboHisPrice.Text = "ultraCombo1";
		this.cboHisPrice.ValueMember = "";
		this.cboHisPrice.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(cboHisPrice_InitializeLayout);
		this.cboHisPrice.AfterCloseUp += new System.EventHandler(cboHisPrice_AfterCloseUp);
		this.cboCodeLength.AutoSize = true;
		this.cboCodeLength.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		valueListItem1.DataValue = "10";
		valueListItem1.DisplayText = "*";
		valueListItem2.DataValue = "9";
		valueListItem2.DisplayText = "9";
		valueListItem3.DataValue = "8";
		valueListItem3.DisplayText = "8";
		valueListItem4.DataValue = "7";
		valueListItem4.DisplayText = "7";
		valueListItem5.DataValue = "6";
		valueListItem5.DisplayText = "6";
		valueListItem6.DataValue = "5";
		valueListItem6.DisplayText = "5";
		this.cboCodeLength.Items.Add(valueListItem1);
		this.cboCodeLength.Items.Add(valueListItem2);
		this.cboCodeLength.Items.Add(valueListItem3);
		this.cboCodeLength.Items.Add(valueListItem4);
		this.cboCodeLength.Items.Add(valueListItem5);
		this.cboCodeLength.Items.Add(valueListItem6);
		this.cboCodeLength.Location = new System.Drawing.Point(224, 21);
		this.cboCodeLength.Name = "cboCodeLength";
		this.cboCodeLength.Nullable = false;
		this.cboCodeLength.Size = new System.Drawing.Size(40, 21);
		this.cboCodeLength.TabIndex = 2;
		this.cboCodeLength.Text = "*";
		this.cboCodeLength.SelectionChanged += new System.EventHandler(cboCodeLength_Changed);
		this.groupBox2.Controls.Add(this.cboAnalysis);
		this.groupBox2.Controls.Add(this.cboCostKind);
		this.groupBox2.Controls.Add(this.cboType);
		this.groupBox2.ForeColor = System.Drawing.SystemColors.Highlight;
		this.groupBox2.Location = new System.Drawing.Point(12, 73);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(321, 92);
		this.groupBox2.TabIndex = 4;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "工項類別";
		this.cboAnalysis.AutoSize = true;
		this.cboAnalysis.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		valueListItem7.DataValue = "";
		valueListItem7.DisplayText = "無下層單價分析";
		valueListItem8.DataValue = "1";
		valueListItem8.DisplayText = "有下層單價分析";
		this.cboAnalysis.Items.Add(valueListItem7);
		this.cboAnalysis.Items.Add(valueListItem8);
		this.cboAnalysis.Location = new System.Drawing.Point(16, 80);
		this.cboAnalysis.Name = "cboAnalysis";
		this.cboAnalysis.Nullable = false;
		this.cboAnalysis.Size = new System.Drawing.Size(292, 21);
		this.cboAnalysis.TabIndex = 2;
		this.cboAnalysis.Text = null;
		this.cboAnalysis.Visible = false;
		this.cboAnalysis.ValueChanged += new System.EventHandler(ControlStateChaged);
		this.cboCostKind.AutoSize = true;
		this.cboCostKind.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		valueListItem9.DataValue = "$";
		valueListItem9.DisplayText = "直接輸入變動單價";
		valueListItem10.DataValue = "%";
		valueListItem10.DisplayText = "以上項目小計之百分比";
		valueListItem11.DataValue = "L";
		valueListItem11.DisplayText = "以上人工項目小計之百分比";
		valueListItem12.DataValue = "E";
		valueListItem12.DisplayText = "以上機具項目小計之百分比";
		valueListItem13.DataValue = "M";
		valueListItem13.DisplayText = "以上材料項目小計之百分比";
		this.cboCostKind.Items.Add(valueListItem9);
		this.cboCostKind.Items.Add(valueListItem10);
		this.cboCostKind.Items.Add(valueListItem11);
		this.cboCostKind.Items.Add(valueListItem12);
		this.cboCostKind.Items.Add(valueListItem13);
		this.cboCostKind.Location = new System.Drawing.Point(16, 57);
		this.cboCostKind.Name = "cboCostKind";
		this.cboCostKind.Nullable = false;
		this.cboCostKind.Size = new System.Drawing.Size(292, 21);
		this.cboCostKind.TabIndex = 1;
		this.cboCostKind.Text = null;
		this.cboCostKind.ValueChanged += new System.EventHandler(ControlStateChaged);
		this.cboType.AutoSize = true;
		this.cboType.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		valueListItem14.DataValue = "0";
		valueListItem14.DisplayText = "固定單價工項";
		valueListItem15.DataValue = "1";
		valueListItem15.DisplayText = "變動單價工項";
		this.cboType.Items.Add(valueListItem14);
		this.cboType.Items.Add(valueListItem15);
		this.cboType.Location = new System.Drawing.Point(16, 26);
		this.cboType.Name = "cboType";
		this.cboType.Nullable = false;
		this.cboType.Size = new System.Drawing.Size(292, 21);
		this.cboType.TabIndex = 0;
		this.cboType.Text = null;
		this.cboType.ValueChanged += new System.EventHandler(ControlStateChaged);
		this.groupBox1.Controls.Add(this.ItemTypeOp);
		this.groupBox1.ForeColor = System.Drawing.SystemColors.Highlight;
		this.groupBox1.Location = new System.Drawing.Point(11, 4);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(322, 60);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "項目類別";
		this.ItemTypeOp.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.ItemTypeOp.CheckedIndex = 0;
		this.ItemTypeOp.ItemAppearance = appearance8;
		valueListItem16.DataValue = "Default Item";
		valueListItem16.DisplayText = "一般項目";
		valueListItem17.DataValue = "ValueListItem1";
		valueListItem17.DisplayText = "小計項目";
		valueListItem18.DataValue = "ValueListItem2";
		valueListItem18.DisplayText = "說明項目";
		this.ItemTypeOp.Items.Add(valueListItem16);
		this.ItemTypeOp.Items.Add(valueListItem17);
		this.ItemTypeOp.Items.Add(valueListItem18);
		this.ItemTypeOp.ItemSpacingHorizontal = 20;
		this.ItemTypeOp.Location = new System.Drawing.Point(15, 26);
		this.ItemTypeOp.Name = "ItemTypeOp";
		this.ItemTypeOp.Size = new System.Drawing.Size(293, 25);
		this.ItemTypeOp.TabIndex = 3;
		this.ItemTypeOp.Text = "一般項目";
		this.ItemTypeOp.ValueChanged += new System.EventHandler(ControlStateChaged);
		this.panel3.Controls.Add(this.groupBox4);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 337);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(635, 168);
		this.panel3.TabIndex = 5;
		this.groupBox4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox4.Controls.Add(this.panel5);
		this.groupBox4.Controls.Add(this.ultraButton1);
		this.groupBox4.ForeColor = System.Drawing.SystemColors.Highlight;
		this.groupBox4.Location = new System.Drawing.Point(13, 5);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(611, 156);
		this.groupBox4.TabIndex = 1;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "歷史價格";
		this.panel5.BackColor = System.Drawing.Color.WhiteSmoke;
		this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel5.Controls.Add(this.ultraLabel26);
		this.panel5.Controls.Add(this.ultraLabel25);
		this.panel5.Controls.Add(this.ultraLabel24);
		this.panel5.Controls.Add(this.panel7);
		this.panel5.Controls.Add(this.axPVLine3D2);
		this.panel5.Controls.Add(this.panel6);
		this.panel5.Controls.Add(this.axPVLine3D1);
		this.panel5.Controls.Add(this.ultraLabel13);
		this.panel5.Location = new System.Drawing.Point(7, 36);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(589, 80);
		this.panel5.TabIndex = 4;
		appearance9.BackColor = System.Drawing.Color.LightSkyBlue;
		appearance9.BackColor2 = System.Drawing.Color.AliceBlue;
		appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance9.ForeColor = System.Drawing.Color.Black;
		appearance9.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel26.Appearance = appearance9;
		this.ultraLabel26.Location = new System.Drawing.Point(431, 0);
		this.ultraLabel26.Name = "ultraLabel26";
		this.ultraLabel26.Size = new System.Drawing.Size(40, 24);
		this.ultraLabel26.TabIndex = 7;
		this.ultraLabel26.Text = "平均";
		appearance10.BackColor = System.Drawing.Color.LightSkyBlue;
		appearance10.BackColor2 = System.Drawing.Color.AliceBlue;
		appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance10.ForeColor = System.Drawing.Color.Black;
		appearance10.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel25.Appearance = appearance10;
		this.ultraLabel25.Location = new System.Drawing.Point(292, 0);
		this.ultraLabel25.Name = "ultraLabel25";
		this.ultraLabel25.Size = new System.Drawing.Size(40, 24);
		this.ultraLabel25.TabIndex = 6;
		this.ultraLabel25.Text = "最低";
		appearance11.BackColor = System.Drawing.Color.LightSkyBlue;
		appearance11.BackColor2 = System.Drawing.Color.AliceBlue;
		appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance11.ForeColor = System.Drawing.Color.Black;
		appearance11.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel24.Appearance = appearance11;
		this.ultraLabel24.Location = new System.Drawing.Point(152, 0);
		this.ultraLabel24.Name = "ultraLabel24";
		this.ultraLabel24.Size = new System.Drawing.Size(40, 24);
		this.ultraLabel24.TabIndex = 5;
		this.ultraLabel24.Text = "最高";
		this.panel7.Controls.Add(this.lblCesAvg);
		this.panel7.Controls.Add(this.lblCesLower);
		this.panel7.Controls.Add(this.lblCesUpper);
		this.panel7.Controls.Add(this.ultraLabel17);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel7.Location = new System.Drawing.Point(0, 54);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(585, 22);
		this.panel7.TabIndex = 4;
		appearance12.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance12.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblCesAvg.Appearance = appearance12;
		this.lblCesAvg.Dock = System.Windows.Forms.DockStyle.Left;
		this.lblCesAvg.Location = new System.Drawing.Point(380, 0);
		this.lblCesAvg.Name = "lblCesAvg";
		this.lblCesAvg.Size = new System.Drawing.Size(140, 22);
		this.lblCesAvg.TabIndex = 4;
		this.lblCesAvg.Text = "00.00";
		this.lblCesAvg.Visible = false;
		appearance13.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance13.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblCesLower.Appearance = appearance13;
		this.lblCesLower.Dock = System.Windows.Forms.DockStyle.Left;
		this.lblCesLower.Location = new System.Drawing.Point(240, 0);
		this.lblCesLower.Name = "lblCesLower";
		this.lblCesLower.Size = new System.Drawing.Size(140, 22);
		this.lblCesLower.TabIndex = 3;
		this.lblCesLower.Text = "00.00";
		this.lblCesLower.Visible = false;
		appearance14.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance14.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblCesUpper.Appearance = appearance14;
		this.lblCesUpper.Dock = System.Windows.Forms.DockStyle.Left;
		this.lblCesUpper.Location = new System.Drawing.Point(100, 0);
		this.lblCesUpper.Name = "lblCesUpper";
		this.lblCesUpper.Size = new System.Drawing.Size(140, 22);
		this.lblCesUpper.TabIndex = 2;
		this.lblCesUpper.Text = "00.00";
		this.lblCesUpper.Visible = false;
		appearance15.ForeColor = System.Drawing.Color.Black;
		appearance15.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel17.Appearance = appearance15;
		this.ultraLabel17.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel17.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel17.Name = "ultraLabel17";
		this.ultraLabel17.Size = new System.Drawing.Size(100, 22);
		this.ultraLabel17.TabIndex = 1;
		this.ultraLabel17.Text = " 營建物價";
		this.ultraLabel17.Visible = false;
		this.axPVLine3D2.Dock = System.Windows.Forms.DockStyle.Top;
		this.axPVLine3D2.Enabled = true;
		this.axPVLine3D2.Location = new System.Drawing.Point(0, 50);
		this.axPVLine3D2.Name = "axPVLine3D2";
		this.axPVLine3D2.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("axPVLine3D2.OcxState");
		this.axPVLine3D2.Size = new System.Drawing.Size(585, 4);
		this.axPVLine3D2.TabIndex = 3;
		this.panel6.Controls.Add(this.lblHisAvg);
		this.panel6.Controls.Add(this.lblHisLower);
		this.panel6.Controls.Add(this.lblHisUpper);
		this.panel6.Controls.Add(this.ultraLabel16);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel6.Location = new System.Drawing.Point(0, 28);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(585, 22);
		this.panel6.TabIndex = 2;
		appearance16.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance16.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblHisAvg.Appearance = appearance16;
		this.lblHisAvg.Dock = System.Windows.Forms.DockStyle.Left;
		this.lblHisAvg.Location = new System.Drawing.Point(380, 0);
		this.lblHisAvg.Name = "lblHisAvg";
		this.lblHisAvg.Size = new System.Drawing.Size(140, 22);
		this.lblHisAvg.TabIndex = 3;
		this.lblHisAvg.Text = "00.00";
		appearance17.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance17.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblHisLower.Appearance = appearance17;
		this.lblHisLower.Dock = System.Windows.Forms.DockStyle.Left;
		this.lblHisLower.Location = new System.Drawing.Point(240, 0);
		this.lblHisLower.Name = "lblHisLower";
		this.lblHisLower.Size = new System.Drawing.Size(140, 22);
		this.lblHisLower.TabIndex = 2;
		this.lblHisLower.Text = "00.00";
		appearance18.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance18.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblHisUpper.Appearance = appearance18;
		this.lblHisUpper.Dock = System.Windows.Forms.DockStyle.Left;
		this.lblHisUpper.Location = new System.Drawing.Point(100, 0);
		this.lblHisUpper.Name = "lblHisUpper";
		this.lblHisUpper.Size = new System.Drawing.Size(140, 22);
		this.lblHisUpper.TabIndex = 1;
		this.lblHisUpper.Text = "00.00";
		appearance19.ForeColor = System.Drawing.Color.Black;
		appearance19.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel16.Appearance = appearance19;
		this.ultraLabel16.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel16.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel16.Name = "ultraLabel16";
		this.ultraLabel16.Size = new System.Drawing.Size(100, 22);
		this.ultraLabel16.TabIndex = 0;
		this.ultraLabel16.Text = " 詢價價格";
		this.axPVLine3D1.Dock = System.Windows.Forms.DockStyle.Top;
		this.axPVLine3D1.Enabled = true;
		this.axPVLine3D1.Location = new System.Drawing.Point(0, 24);
		this.axPVLine3D1.Name = "axPVLine3D1";
		this.axPVLine3D1.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("axPVLine3D1.OcxState");
		this.axPVLine3D1.Size = new System.Drawing.Size(585, 4);
		this.axPVLine3D1.TabIndex = 1;
		appearance20.BackColor = System.Drawing.Color.LightSkyBlue;
		appearance20.BackColor2 = System.Drawing.Color.AliceBlue;
		appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance20.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel13.Appearance = appearance20;
		this.ultraLabel13.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel13.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(585, 24);
		this.ultraLabel13.TabIndex = 0;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton1.Location = new System.Drawing.Point(6, 120);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(92, 32);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 3;
		this.ultraButton1.Text = "歷史標案";
		this.ultraButton1.Visible = false;
		this.panel4.Controls.Add(this.lblAnalysisQty);
		this.panel4.Controls.Add(this.BtnOK);
		this.panel4.Controls.Add(this.BtnCancel);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(0, 505);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(635, 41);
		this.panel4.TabIndex = 6;
		this.lblAnalysisQty.Location = new System.Drawing.Point(270, 9);
		this.lblAnalysisQty.Name = "lblAnalysisQty";
		this.lblAnalysisQty.Size = new System.Drawing.Size(100, 23);
		this.lblAnalysisQty.TabIndex = 2;
		this.lblAnalysisQty.Text = "1";
		this.lblAnalysisQty.Visible = false;
		this.BtnOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance21.Image = resources.GetObject("appearance21.Image");
		this.BtnOK.Appearance = appearance21;
		this.BtnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnOK.Location = new System.Drawing.Point(433, 5);
		this.BtnOK.Name = "BtnOK";
		this.BtnOK.ShowFocusRect = false;
		this.BtnOK.Size = new System.Drawing.Size(94, 31);
		this.BtnOK.SupportThemes = false;
		this.BtnOK.TabIndex = 1;
		this.BtnOK.Text = "確定";
		this.BtnOK.Click += new System.EventHandler(BtnOK_Click);
		this.BtnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance22.Image = resources.GetObject("appearance22.Image");
		this.BtnCancel.Appearance = appearance22;
		this.BtnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.BtnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnCancel.Location = new System.Drawing.Point(529, 5);
		this.BtnCancel.Name = "BtnCancel";
		this.BtnCancel.ShowFocusRect = false;
		this.BtnCancel.Size = new System.Drawing.Size(94, 31);
		this.BtnCancel.SupportThemes = false;
		this.BtnCancel.TabIndex = 0;
		this.BtnCancel.Text = "取消";
		this.BtnCancel.Click += new System.EventHandler(BtnCancel_Click);
		base.AcceptButton = this.BtnOK;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.BtnCancel;
		base.ClientSize = new System.Drawing.Size(635, 546);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel4);
		this.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormMrsBaseEdit";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "工項編輯";
		base.Load += new System.EventHandler(FormMrsBaseEdit_Load);
		base.Activated += new System.EventHandler(FormMrsBaseEdit_Activated);
		base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(FormMrsBaseEdit_FormClosed);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormMrsBaseEdit_FormClosing);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormMrsBaseEdit_KeyDown);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtSurName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtClass).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboEUnit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCUnit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExtendCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMemo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPccesCode).EndInit();
		this.panel2.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtWRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtERate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCost_Rate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboHisPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCodeLength).EndInit();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.cboAnalysis).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCostKind).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboType).EndInit();
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ItemTypeOp).EndInit();
		this.panel3.ResumeLayout(false);
		this.groupBox4.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.axPVLine3D2).EndInit();
		this.panel6.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.axPVLine3D1).EndInit();
		this.panel4.ResumeLayout(false);
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

	public FormMrsBaseEdit()
	{
		InitializeComponent();
		FormStatus = FormStatus.Active;
	}

	private void BtnCancel_Click(object sender, EventArgs e)
	{
		if (F_ItemClassflag != "")
		{
			ArrayList aArr = new ArrayList();
			aArr.Add(F_UserID);
			aArr.Add("找出pubcode");
			dbMrsBaseA = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
			dbMrsBaseA.ps_srckind = "MRS";
			dbMrsBaseA.ps_projectcode = F_ProjectCode;
			dbMrsBaseA.ps_pccesCode = txtPccesCode.Text.Trim();
			dbMrsBaseA.DeleItem(iPubCode.ToString());
			dbMrsBaseA = null;
		}
		Close();
	}

	private void FormMrsBaseEdit_Load(object sender, EventArgs e)
	{
		FirstFocus = true;
		if (F_EditMode == MrsBaseEditFormType.New)
		{
			F_FormHeight = panel3.Location.Y + panel4.Size.Height + 40;
		}
		else
		{
			F_FormHeight = panel4.Location.Y + panel4.Height + 40;
		}
		base.Height = F_FormHeight;
		F_flag = "Init";
		CorrectRatio();
		GetUnitDataSet();
		cboType.SelectedIndex = 0;
		cboCostKind.SelectedIndex = 0;
		cboAnalysis.SelectedIndex = 0;
		cboAnalysis.Location = cboCostKind.Location;
		if (F_EditMode == MrsBaseEditFormType.Edit || F_EditMode == MrsBaseEditFormType.CopyToNew)
		{
			LoadData(iPubCode);
			GetHisPrice();
			if (F_EditMode == MrsBaseEditFormType.CopyToNew)
			{
				F_EditMode = MrsBaseEditFormType.New;
				F_Record = "CopyToNew";
			}
			if (F_EditMode == MrsBaseEditFormType.Edit && (F_CallerFormName == "FormBudget" || F_CallerFormName == "FormBreakDown") && cboCostKind.SelectedIndex < 1)
			{
				txtCost_Rate.Value = F_ExternalCost;
			}
			if (F_sNO > 0)
			{
				Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl theBudgetCtrl = new Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl();
				int sNo = ArchConvert.Obj2Int(F_sNO);
				bool Allow = true;
				if (F_CallerFormName == "FormBreakDown")
				{
					if (SysConfig.SysComsEnable && SysConfig.SysEditAfterBudLem.ToUpper().Trim() == "DISABLE")
					{
						F_Istemplate = theBudgetCtrl.IsWorkItemInSubPlanCart(F_ProjectCode, SysConfig.SysComsDB, txtPccesCode.Text);
						Allow = theBudgetCtrl.IsWorkItemCostCanChange(F_ProjectCode, SysConfig.SysComsDB, txtPccesCode.Text);
					}
				}
				else if (F_CallerFormName == "FormBudget")
				{
					if (SysConfig.SysEditAfterBudLem.ToUpper().Trim() == "DISABLE")
					{
						F_Istemplate = theBudgetCtrl.IsWorkItemInSubPlanCart(F_ProjectCode, SysConfig.SysComsDB, txtPccesCode.Text);
						if (F_Istemplate)
						{
							DBClass DBCls = new DBClass();
							DBCls._FS_UserID = F_UserID;
							string costKind = DBCls.GetMrsBaseACostKind(F_ProjectCode, txtPccesCode.Text.Trim(), CommonMethods.GetActionNameString(F_ActionName));
							if (costKind.Trim() != "" && F_ActionName == PccesFormAction.BUD)
							{
								F_Istemplate = theBudgetCtrl.IsChangeCostWorkItemInBudLem(F_ProjectCode, SysConfig.SysComsDB, txtPccesCode.Text, F_sNO);
							}
						}
						if (!_AllowEditCost)
						{
							Allow = theBudgetCtrl.IsWorkItemCostCanChange(F_ProjectCode, SysConfig.SysComsDB, txtPccesCode.Text);
						}
						else if (!F_Istemplate)
						{
							Allow = true;
						}
						else
						{
							BudProjMrsA theBudProjMrsA = new BudProjMrsA();
							Allow = (theBudProjMrsA.CheckWorkItemPriceCanChange(F_ProjectCode, txtPccesCode.Text) ? true : false);
						}
					}
					else if (_AllowEditCost)
					{
						Allow = true;
					}
				}
				BtnOK.Enabled = Allow;
			}
			if (!F_Istemplate)
			{
				F_Istemplate = F_IsLocked;
			}
		}
		InitFormStatus();
		SetControlEnabledState();
		Frm.OnUserRequest += UserReq;
		FormStatus = FormStatus.Active;
		if (F_Istemplate && !AllowEditCost)
		{
			BtnOK.Enabled = false;
		}
		if (F_IsSubmitBid)
		{
			BtnOK.Enabled = false;
		}
	}

	private void InitFormStatus()
	{
		if (F_MainCst.ToString().Trim() == string.Empty)
		{
			CustomFormatMaskInput = "-n,nnn,nnn,nnn,nnn,nnn.nn";
		}
		else
		{
			int digits = Convert.ToInt32(F_MainCst.ToString());
			string mask = "-n,nnn,nnn,nnn,nnn,nnn";
			if (digits > 0)
			{
				mask += ".";
				for (int i = 0; i < digits; i++)
				{
					mask += "n";
				}
			}
			CustomFormatMaskInput = mask;
		}
		string CostKind = ArchConvert.Obj2String(cboCostKind.Value);
		if (ArchConvert.Obj2String(cboType.Value) == "1")
		{
			switch (CostKind)
			{
			case "%":
			case "L":
			case "E":
				goto IL_00fa;
			}
			if (CostKind == "M")
			{
				goto IL_00fa;
			}
		}
		SetCostRateMask(IsDefault: false);
		goto IL_0110;
		IL_0110:
		switch (F_EditMode)
		{
		case MrsBaseEditFormType.CopyToNew:
			Text = "工項複製";
			break;
		case MrsBaseEditFormType.Edit:
			Text = "工項編輯";
			break;
		case MrsBaseEditFormType.New:
			Text = "工項新增";
			BtnPickPrice.Visible = false;
			cboHisPrice.Visible = false;
			break;
		}
		if (F_ActionName == PccesFormAction.BID)
		{
			ItemTypeOp.Enabled = false;
			cboCUnit.Enabled = false;
			cboEUnit.Enabled = false;
			if (F_IsLockAn)
			{
				txtLRate.Enabled = false;
				txtERate.Enabled = false;
				txtMRate.Enabled = false;
				txtWRate.Enabled = false;
			}
			txtPccesCode.Enabled = false;
			txtExtendCode.Enabled = false;
			txtCName.Enabled = false;
			txtEName.Enabled = false;
			txtMemo.Enabled = false;
			txtClass.Enabled = false;
			txtSurName.Enabled = false;
		}
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		DBCLS.MrsBase_Lock(iPubCode.ToString(), F_ProjectCode, CommonMethods.GetActionNameString(F_ActionName));
		DBCLS = null;
		return;
		IL_00fa:
		SetCostRateMask(IsDefault: true);
		goto IL_0110;
	}

	private void SetControlEnabledState()
	{
		if (cboHisPrice.Rows.Count > 0)
		{
			BtnPickPrice.Visible = true;
			cboHisPrice.Visible = true;
			if (cboAnalysis.SelectedIndex == 1)
			{
				BtnPickPrice.Visible = false;
				cboHisPrice.Visible = false;
			}
		}
		else
		{
			BtnPickPrice.Visible = false;
			cboHisPrice.Visible = false;
		}
		if (F_EditMode == MrsBaseEditFormType.Edit)
		{
			txtPccesCode.Enabled = false;
			txtPccesCode.Appearance.BackColor = Color.FromArgb(127, 127, 127);
			if (txtMemo.Text.Length == 0)
			{
				txtPccesCode.Enabled = false;
				txtCName.Enabled = false;
				txtEName.Enabled = false;
				cboCUnit.Enabled = false;
				cboEUnit.Enabled = false;
			}
			else if (txtMemo.Text.Substring(0, 1) != "#" || IsCommonItem)
			{
				txtPccesCode.Enabled = false;
				txtCName.Enabled = false;
				txtEName.Enabled = false;
				cboCUnit.Enabled = false;
				cboEUnit.Enabled = false;
			}
			else
			{
				txtPccesCode.Enabled = false;
				if (F_ActionName != PccesFormAction.BID)
				{
					txtCName.Enabled = true;
					txtEName.Enabled = true;
					txtExtendCode.Enabled = true;
					cboCUnit.Enabled = true;
					cboEUnit.Enabled = true;
				}
			}
		}
		if (F_Istemplate)
		{
			txtPccesCode.Enabled = false;
			txtCName.Enabled = false;
			txtEName.Enabled = false;
			txtExtendCode.Enabled = false;
			cboCUnit.Enabled = false;
			cboEUnit.Enabled = false;
			BtnPickPrice.Visible = false;
			cboHisPrice.Visible = false;
			txtMemo.Enabled = false;
			txtClass.Enabled = false;
			ultraButton2.Enabled = false;
			txtSurName.Enabled = false;
			groupBox1.Enabled = false;
			txtCost_Rate.Enabled = false;
			txtLRate.Enabled = false;
			txtERate.Enabled = false;
			txtMRate.Enabled = false;
			txtWRate.Enabled = false;
			BtnPickPrice.Enabled = false;
			cboType.Enabled = false;
			cboCostKind.Enabled = false;
			cboAnalysis.Enabled = false;
			cboCodeLength.Visible = false;
			if (AllowEditCost)
			{
				txtCost_Rate.Enabled = true;
			}
		}
	}

	private void Control_Status()
	{
		lblCost_Rate.Text = "單價:";
		if (ItemTypeOp.CheckedIndex == 0)
		{
			groupBox2.Enabled = true;
			groupBox3.Enabled = true;
			if (cboType.SelectedIndex == 0)
			{
				cboCostKind.Visible = false;
				cboAnalysis.Visible = true;
				if (cboAnalysis.SelectedIndex == 0)
				{
					if (F_ActionName == PccesFormAction.BID)
					{
						txtCost_Rate.Enabled = true;
						txtLRate.Enabled = false;
						txtERate.Enabled = false;
						txtMRate.Enabled = false;
						txtWRate.Enabled = false;
						BtnPickPrice.Visible = false;
						cboHisPrice.Visible = false;
					}
					else
					{
						txtCost_Rate.Enabled = true;
						txtLRate.Enabled = true;
						txtERate.Enabled = true;
						txtMRate.Enabled = true;
						txtWRate.Enabled = true;
						BtnPickPrice.Visible = true;
						cboHisPrice.Visible = true;
					}
				}
				else if (cboAnalysis.SelectedIndex == 1)
				{
					txtCost_Rate.Enabled = false;
					txtLRate.Enabled = false;
					txtERate.Enabled = false;
					txtMRate.Enabled = false;
					txtWRate.Enabled = false;
					BtnPickPrice.Visible = false;
					cboHisPrice.Visible = false;
				}
				SetCostRateMask(IsDefault: false);
			}
			else if (cboType.SelectedIndex == 1)
			{
				cboCostKind.Visible = true;
				cboAnalysis.Visible = false;
				switch (cboCostKind.SelectedIndex)
				{
				case 0:
					txtCost_Rate.Enabled = true;
					txtLRate.Enabled = true;
					txtERate.Enabled = true;
					txtMRate.Enabled = true;
					txtWRate.Enabled = true;
					SetCostRateMask(IsDefault: false);
					break;
				case 1:
					lblCost_Rate.Text = "百分比:";
					txtCost_Rate.Enabled = true;
					txtLRate.Enabled = true;
					txtERate.Enabled = true;
					txtMRate.Enabled = true;
					txtWRate.Enabled = true;
					SetCostRateMask(IsDefault: true);
					break;
				case 2:
					lblCost_Rate.Text = "百分比:";
					txtCost_Rate.Enabled = true;
					if (F_flag == "")
					{
						txtLRate.Text = "100";
						txtERate.Text = "0";
						txtMRate.Text = "0";
						txtWRate.Text = "0";
					}
					txtLRate.Enabled = true;
					txtERate.Enabled = true;
					txtMRate.Enabled = true;
					txtWRate.Enabled = true;
					SetCostRateMask(IsDefault: true);
					break;
				case 3:
					lblCost_Rate.Text = "百分比:";
					txtCost_Rate.Enabled = true;
					if (F_flag == "")
					{
						txtLRate.Text = "0";
						txtERate.Text = "100";
						txtMRate.Text = "0";
						txtWRate.Text = "0";
					}
					txtLRate.Enabled = true;
					txtERate.Enabled = true;
					txtMRate.Enabled = true;
					txtWRate.Enabled = true;
					SetCostRateMask(IsDefault: true);
					break;
				case 4:
					lblCost_Rate.Text = "百分比:";
					txtCost_Rate.Enabled = true;
					if (F_flag == "")
					{
						txtLRate.Text = "0";
						txtERate.Text = "0";
						txtMRate.Text = "100";
						txtWRate.Text = "0";
					}
					txtLRate.Enabled = true;
					txtERate.Enabled = true;
					txtMRate.Enabled = true;
					txtWRate.Enabled = true;
					SetCostRateMask(IsDefault: true);
					break;
				}
				if (F_IsLockAn && lblCost_Rate.Text == "百分比:")
				{
					txtCost_Rate.Enabled = false;
					BtnPickPrice.Enabled = false;
					BtnPickPrice.Visible = false;
				}
				F_flag = "";
			}
		}
		else if (ItemTypeOp.CheckedIndex == 1 || ItemTypeOp.CheckedIndex == 2)
		{
			groupBox2.Enabled = false;
			groupBox3.Enabled = false;
			if (ItemTypeOp.CheckedIndex == 2)
			{
				if (txtPccesCode.Text.Length <= 0 || !(txtPccesCode.Text[0].ToString() != "#") || F_EditMode != MrsBaseEditFormType.Edit)
				{
				}
				txtCost_Rate.Text = "0";
			}
		}
		if (FormStatus == FormStatus.Active && lblCost_Rate.Text != "單價:")
		{
			txtCost_Rate.Text = F_InitialRate;
		}
	}

	private void LoadData(int P_PubCode)
	{
		cboType.SelectedIndex = 0;
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("WinFORM 基本工料");
		dbMrsBaseA = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
		dbMrsBaseA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbMrsBaseA.ps_projectcode = F_ProjectCode;
		dbMrsBaseA.ps_Issue = F_chgCount;
		DataTable dtMrsMasrA = dbMrsBaseA.ListItem(" pubCode =" + P_PubCode);
		if (dtMrsMasrA.Rows.Count == 1)
		{
			F_OriginalPccescode = dtMrsMasrA.Rows[0]["pccesCode"].ToString().Trim();
			txtClass.Text = InitialMrsA(P_PubCode);
			if (dtMrsMasrA.Rows.Count > 0)
			{
				DataRow theRow = dtMrsMasrA.Rows[0];
				txtPccesCode.Text = theRow["pccesCode"].ToString().Trim();
				txtCName.Text = theRow["cName"].ToString().Trim();
				txtEName.Text = theRow["eName"].ToString().Trim();
				txtMemo.Text = theRow["memo"].ToString().Trim();
				txtExtendCode.Text = theRow["extendCode"].ToString().Trim();
				txtCost_Rate.Text = string.Format("{0:N" + F_MainCst + "}", theRow["cost"]);
				F_InitialRate = theRow["Rate"].ToString();
				txtLRate.Value = ((theRow["lRate"] == DBNull.Value) ? 0.0 : ((double)theRow["lRate"]));
				txtERate.Value = ((theRow["eRate"] == DBNull.Value) ? 0.0 : ((double)theRow["eRate"]));
				txtMRate.Value = ((theRow["mRate"] == DBNull.Value) ? 0.0 : ((double)theRow["mRate"]));
				txtWRate.Value = ((theRow["wRate"] == DBNull.Value) ? 0.0 : ((double)theRow["wRate"]));
				lblAnalysisQty.Text = ((theRow["analysisQty"].ToString().Trim() != "") ? theRow["analysisQty"].ToString() : "1");
				txtSurName.Text = theRow["surName"].ToString().Trim();
				cboCUnit.Text = theRow["unitName"].ToString().Trim();
				for (int i = 0; i < cboCUnit.Rows.Count; i++)
				{
					if (cboCUnit.Rows[i].Cells[0].Text.Trim() == theRow["unitName"].ToString().Trim())
					{
						cboCUnit.SelectedRow = cboCUnit.Rows[i];
						cboCUnit.Text = theRow["unitName"].ToString().Trim();
						break;
					}
				}
				cboEUnit.Text = theRow["eUnit"].ToString().Trim();
				for (int i = 0; i < cboEUnit.Rows.Count; i++)
				{
					if (cboEUnit.Rows[i].Cells[0].Text.Trim() == theRow["eUnit"].ToString().Trim())
					{
						cboEUnit.SelectedRow = cboEUnit.Rows[i];
						cboEUnit.Text = theRow["eUnit"].ToString().Trim();
						break;
					}
				}
				switch (theRow["costKind"].ToString().Trim().ToUpper())
				{
				case "Z":
					ItemTypeOp.CheckedIndex = 1;
					break;
				case "#":
					ItemTypeOp.CheckedIndex = 2;
					break;
				case "":
					ItemTypeOp.CheckedIndex = 0;
					break;
				case "$":
					cboCostKind.SelectedIndex = 0;
					cboType.SelectedIndex = 1;
					break;
				case "%":
					txtCost_Rate.Text = theRow["rate"].ToString().Trim().ToUpper();
					cboCostKind.SelectedIndex = 1;
					cboType.SelectedIndex = 1;
					break;
				case "L":
					txtCost_Rate.Text = theRow["rate"].ToString().Trim().ToUpper();
					cboCostKind.SelectedIndex = 2;
					cboType.SelectedIndex = 1;
					break;
				case "E":
					txtCost_Rate.Text = theRow["rate"].ToString().Trim().ToUpper();
					cboCostKind.SelectedIndex = 3;
					cboType.SelectedIndex = 1;
					break;
				case "M":
					txtCost_Rate.Text = theRow["rate"].ToString().Trim().ToUpper();
					cboCostKind.SelectedIndex = 4;
					cboType.SelectedIndex = 1;
					break;
				default:
					ItemTypeOp.CheckedIndex = 0;
					break;
				}
				if (theRow["analysis"].ToString().Trim().ToUpper() == "1")
				{
					cboAnalysis.SelectedIndex = 1;
				}
				else
				{
					cboAnalysis.SelectedIndex = 0;
				}
				if (F_ActionName == PccesFormAction.BUD && F_EditMode != MrsBaseEditFormType.CopyToNew && dtMrsMasrA.Columns.IndexOf("Lock") > 0 && theRow["Lock"] != DBNull.Value && Convert.ToBoolean(theRow["Lock"]))
				{
					F_Istemplate = true;
				}
				if (F_ActionName == PccesFormAction.BUD || F_ActionName == PccesFormAction.MrsBase)
				{
					IsCommonItem = ArchConvert.Obj2Bool(theRow["IsCommonItem"]);
				}
			}
		}
		else if (dtMrsMasrA.Rows.Count == 0)
		{
			MessageBox.Show("找不到指定的工項， pubCode = " + P_PubCode + "請連繫Administrator");
			Close();
		}
		else if (dtMrsMasrA.Rows.Count > 0)
		{
			MessageBox.Show("找到指定的工項有兩筆， pubCode = " + P_PubCode + "請連繫Administrator");
			Close();
		}
		aArr = null;
	}

	private void LoadDataByPccesCode(string P_PccesCode)
	{
		cboType.SelectedIndex = 0;
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("WinFORM 基本工料");
		dbMrsBaseA = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
		dbMrsBaseA.ps_srckind = "";
		dbMrsBaseA.ps_projectcode = F_ProjectCode;
		dbMrsBaseA.ps_Issue = F_chgCount;
		DataTable dtMrsMasrA = dbMrsBaseA.ListItem(" pccesCode ='" + P_PccesCode + "'");
		if (dtMrsMasrA.Rows.Count == 1)
		{
			int P_PubCode = Convert.ToInt32(dtMrsMasrA.Rows[0]["pubCode"]);
			txtClass.Text = InitialMrsA(P_PubCode);
			if (dtMrsMasrA.Rows.Count > 0)
			{
				DataRow theRow = dtMrsMasrA.Rows[0];
				txtPccesCode.Text = theRow["pccesCode"].ToString().Trim();
				txtCName.Text = theRow["cName"].ToString().Trim();
				txtEName.Text = theRow["eName"].ToString().Trim();
				txtMemo.Text = theRow["memo"].ToString().Trim();
				txtExtendCode.Text = theRow["extendCode"].ToString().Trim();
				txtCost_Rate.Text = string.Format("{0:N" + F_MainCst + "}", theRow["cost"]);
				F_InitialRate = theRow["Rate"].ToString();
				txtLRate.Value = ((theRow["lRate"] == DBNull.Value) ? 0.0 : ((double)theRow["lRate"]));
				txtERate.Value = ((theRow["eRate"] == DBNull.Value) ? 0.0 : ((double)theRow["eRate"]));
				txtMRate.Value = ((theRow["mRate"] == DBNull.Value) ? 0.0 : ((double)theRow["mRate"]));
				txtWRate.Value = ((theRow["wRate"] == DBNull.Value) ? 0.0 : ((double)theRow["wRate"]));
				lblAnalysisQty.Text = ((theRow["analysisQty"].ToString().Trim() != "") ? theRow["analysisQty"].ToString() : "1");
				txtSurName.Text = theRow["surName"].ToString().Trim();
				cboCUnit.Text = theRow["unitName"].ToString().Trim();
				for (int i = 0; i < cboCUnit.Rows.Count; i++)
				{
					if (cboCUnit.Rows[i].Cells[0].Text.Trim() == theRow["unitName"].ToString().Trim())
					{
						cboCUnit.SelectedRow = cboCUnit.Rows[i];
						cboCUnit.Text = theRow["unitName"].ToString().Trim();
						break;
					}
				}
				cboEUnit.Text = theRow["eUnit"].ToString().Trim();
				for (int i = 0; i < cboEUnit.Rows.Count; i++)
				{
					if (cboEUnit.Rows[i].Cells[0].Text.Trim() == theRow["eUnit"].ToString().Trim())
					{
						cboEUnit.SelectedRow = cboEUnit.Rows[i];
						cboEUnit.Text = theRow["eUnit"].ToString().Trim();
						break;
					}
				}
				switch (theRow["costKind"].ToString().Trim().ToUpper())
				{
				case "Z":
					ItemTypeOp.CheckedIndex = 1;
					break;
				case "#":
					ItemTypeOp.CheckedIndex = 2;
					break;
				case "":
					ItemTypeOp.CheckedIndex = 0;
					break;
				case "$":
					cboCostKind.SelectedIndex = 0;
					cboType.SelectedIndex = 1;
					break;
				case "%":
					txtCost_Rate.Text = theRow["rate"].ToString().Trim().ToUpper();
					cboCostKind.SelectedIndex = 1;
					cboType.SelectedIndex = 1;
					break;
				case "L":
					txtCost_Rate.Text = theRow["rate"].ToString().Trim().ToUpper();
					cboCostKind.SelectedIndex = 2;
					cboType.SelectedIndex = 1;
					break;
				case "E":
					txtCost_Rate.Text = theRow["rate"].ToString().Trim().ToUpper();
					cboCostKind.SelectedIndex = 3;
					cboType.SelectedIndex = 1;
					break;
				case "M":
					txtCost_Rate.Text = theRow["rate"].ToString().Trim().ToUpper();
					cboCostKind.SelectedIndex = 4;
					cboType.SelectedIndex = 1;
					break;
				default:
					ItemTypeOp.CheckedIndex = 0;
					break;
				}
				if (theRow["analysis"].ToString().Trim().ToUpper() == "1")
				{
					cboAnalysis.SelectedIndex = 1;
				}
				else
				{
					cboAnalysis.SelectedIndex = 0;
				}
				if (F_ActionName == PccesFormAction.BUD && F_EditMode != MrsBaseEditFormType.CopyToNew && dtMrsMasrA.Columns.IndexOf("Lock") > 0 && theRow["Lock"] != DBNull.Value && Convert.ToBoolean(theRow["Lock"]))
				{
					F_Istemplate = true;
				}
				if (F_ActionName == PccesFormAction.BUD || F_ActionName == PccesFormAction.MrsBase)
				{
					IsCommonItem = ArchConvert.Obj2Bool(theRow["IsCommonItem"]);
				}
			}
			F_EditMode = MrsBaseEditFormType.Edit;
			dsUpdWorkItem = new DataSet();
			dsUpdWorkItem.Tables.Add(dtMrsMasrA.Copy());
			SetControlEnabledState();
		}
		else if (dtMrsMasrA.Rows.Count == 0)
		{
			dsUpdWorkItem = null;
			Close();
		}
		else if (dtMrsMasrA.Rows.Count > 0)
		{
			MessageBox.Show("找到指定的工項有兩筆， pccesCode = " + P_PccesCode.ToString() + "請連繫Administrator");
			Close();
		}
		aArr = null;
	}

	private string InitialMrsA(int PubCode)
	{
		MrsA theMrsA = new MrsA();
		return theMrsA.GetString(PubCode);
	}

	private void GetUnitDataSet()
	{
		UserDefined theUserDefined = new UserDefined();
		DataSet dsCUnit = theUserDefined.GetCUnit();
		DataRow newRow = dsCUnit.Tables[0].NewRow();
		dsCUnit.Tables[0].Rows.InsertAt(newRow, 0);
		newRow["Unit"] = "";
		cboCUnit.DataSource = dsCUnit;
		cboCUnit.DataMember = "Unit";
		cboCUnit.DataBind();
		DataSet dsEUnit = theUserDefined.GetCUnit();
		newRow = dsEUnit.Tables[0].NewRow();
		dsEUnit.Tables[0].Rows.InsertAt(newRow, 0);
		newRow["Unit"] = "";
		cboEUnit.DataSource = dsEUnit;
		cboEUnit.DataMember = "Unit";
		cboEUnit.DataBind();
	}

	private void GetHisPrice()
	{
		DataTable DT_Temp = ((iCodeLength != 0 && iCodeLength != 10) ? dbMrsBaseA.List_Cost(txtPccesCode.Text.Trim(), iCodeLength) : dbMrsBaseA.List_Cost(txtPccesCode.Text.Trim()));
		cboHisPrice.DataSource = DT_Temp;
		cboHisPrice.DataBind();
		cboHisPrice.DisplayLayout.Bands[0].Override.HeaderClickAction = HeaderClickAction.SortSingle;
		cboHisPrice.DisplayLayout.Bands[0].Columns[0].Header.Caption = "單價";
		cboHisPrice.DisplayLayout.Bands[0].Columns[1].Header.Caption = "KIND";
		cboHisPrice.DisplayLayout.Bands[0].Columns[1].Hidden = true;
		cboHisPrice.DisplayLayout.Bands[0].Columns[2].Header.Caption = "來源";
		cboHisPrice.DisplayLayout.Bands[0].Columns[3].Header.Caption = "說明";
		cboHisPrice.DisplayLayout.Bands[0].Columns[4].Hidden = true;
		cboHisPrice.DisplayLayout.Bands[0].Columns[5].Header.Caption = "工項編碼";
		cboHisPrice.DisplayLayout.Bands[0].Columns[6].Header.Caption = "工項名稱";
		cboHisPrice.DisplayLayout.Bands[0].Columns[7].Header.Caption = "單位";
		cboHisPrice.DisplayLayout.Bands[0].Columns[0].Format = "N2";
		cboHisPrice.DisplayLayout.Bands[0].Columns[0].Width = 80;
		cboHisPrice.DisplayLayout.Bands[0].Columns[2].Width = 120;
		cboHisPrice.DisplayLayout.Bands[0].Columns[3].Width = 200;
		cboHisPrice.DisplayLayout.Bands[0].Columns[5].Width = 110;
		cboHisPrice.DisplayLayout.Bands[0].Columns[6].Width = 310;
		cboHisPrice.Text = "0";
		for (int i = 0; i < DT_Temp.Rows.Count; i++)
		{
			if (DT_Temp.Rows[i]["Kind"].ToString().Trim() == "Std" && DT_Temp.Rows[i]["Area"].ToString().Trim() == "詢價價格" && DT_Temp.Rows[i]["Memo"].ToString().Trim() == "最高價")
			{
				lblHisUpper.Text = string.Format("{0:N2}", DT_Temp.Rows[i]["Cost"]);
			}
			if (DT_Temp.Rows[i]["Kind"].ToString().Trim() == "Std" && DT_Temp.Rows[i]["Area"].ToString().Trim() == "詢價價格" && DT_Temp.Rows[i]["Memo"].ToString().Trim() == "最低價")
			{
				lblHisLower.Text = string.Format("{0:N2}", DT_Temp.Rows[i]["Cost"]);
			}
			if (DT_Temp.Rows[i]["Kind"].ToString().Trim() == "Std" && DT_Temp.Rows[i]["Area"].ToString().Trim() == "詢價價格" && DT_Temp.Rows[i]["Memo"].ToString().Trim() == "平均價")
			{
				lblHisAvg.Text = string.Format("{0:N2}", DT_Temp.Rows[i]["Cost"]);
			}
			if (DT_Temp.Rows[i]["Kind"].ToString().Trim() == "Std" && DT_Temp.Rows[i]["Area"].ToString().Trim() == "營建物價" && DT_Temp.Rows[i]["Memo"].ToString().Trim() == "最高價")
			{
				lblCesUpper.Text = string.Format("{0:N2}", DT_Temp.Rows[i]["Cost"]);
			}
			if (DT_Temp.Rows[i]["Kind"].ToString().Trim() == "Std" && DT_Temp.Rows[i]["Area"].ToString().Trim() == "營建物價" && DT_Temp.Rows[i]["Memo"].ToString().Trim() == "最低價")
			{
				lblCesLower.Text = string.Format("{0:N2}", DT_Temp.Rows[i]["Cost"]);
			}
			if (DT_Temp.Rows[i]["Kind"].ToString().Trim() == "Std" && DT_Temp.Rows[i]["Area"].ToString().Trim() == "營建物價" && DT_Temp.Rows[i]["Memo"].ToString().Trim() == "平均價")
			{
				lblCesAvg.Text = string.Format("{0:N2}", DT_Temp.Rows[i]["Cost"]);
			}
		}
	}

	private void ControlStateChaged(object sender, EventArgs e)
	{
		Control_Status();
	}

	private void BtnPickPrice_Click(object sender, EventArgs e)
	{
		cboHisPrice.ToggleDropdown();
	}

	private void cboCodeLength_Changed(object sender, EventArgs e)
	{
		iCodeLength = Convert.ToInt32(cboCodeLength.Value);
		GetHisPrice();
		InvokeOnClick(BtnPickPrice, EventArgs.Empty);
	}

	private void txtCost_Rate_Click(object sender, EventArgs e)
	{
		if (FirstFocus && txtCost_Rate.Value.ToString().Length <= 10)
		{
			txtCost_Rate.SelectAll();
			FirstFocus = false;
		}
	}

	private void ultraGrid1_InitializeLayout(object sender, InitializeLayoutEventArgs e)
	{
		e.Layout.Override.HeaderClickAction = HeaderClickAction.SortSingle;
	}

	private void cboHisPrice_InitializeLayout(object sender, InitializeLayoutEventArgs e)
	{
		e.Layout.Override.HeaderClickAction = HeaderClickAction.SortSingle;
	}

	private void cboHisPrice_AfterCloseUp(object sender, EventArgs e)
	{
		txtCost_Rate.Text = cboHisPrice.Text;
		if (cboHisPrice.SelectedRow != null)
		{
			area = cboHisPrice.SelectedRow.Cells[4].Text;
		}
	}

	private void BtnOK_Click(object sender, EventArgs e)
	{
		txtPccesCode.Text = txtPccesCode.Text.Trim();
		if (!CheckPccesCodeValidity())
		{
			return;
		}
		string pccesCode = txtPccesCode.Text;
		if (cboCUnit.Enabled && cboCUnit.Text.ToString().Trim() == string.Empty && ItemTypeOp.CheckedIndex != 2 && ItemTypeOp.CheckedIndex != 1)
		{
			MessageBox.Show("請先挑選或輸入單位");
			cboCUnit.Focus();
			return;
		}
		if (_CallerFormName == "FormBudget" && F_sNO > 0)
		{
			ComsWebService theComsWebService = new ComsWebService(F_ProjectCode);
			if (_AllowEditCost && SysConfig.SysIsCheckAccQtyAmt.ToUpper() == "DISABLE" && !theComsWebService.AllowChangeByAccQtyAmtByPccesCode(txtPccesCode.Text, _sNO, "式", 1m, Convert.ToDecimal(txtCost_Rate.Value), silentOnWarning: true, silentOnModify: false))
			{
				return;
			}
			if (_AllowEditCost && SysConfig.SysEditAfterBudLem.ToUpper() == "DISABLE")
			{
				Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl theBudgetCtrl = new Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl();
				DataTable dt = theBudgetCtrl.GetComsSubQtyAmt(F_ProjectCode, SysConfig.SysComsDB, F_sNO);
				if (!dt.Columns.Contains("SubQty"))
				{
					MessageBox.Show("取得COMS已發包金時發生錯誤", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					decimal SubAmtValue = ArchConvert.Obj2Decimal(dt.Rows[0]["SubAmt"]);
					if (Convert.ToDecimal(txtCost_Rate.Value) < SubAmtValue)
					{
						MessageBox.Show("修改後金額(" + txtCost_Rate.Value.ToString() + ")低於已發包金額(" + SubAmtValue + "),不可修改");
						return;
					}
				}
			}
		}
		string sCostKind = "";
		string sAnalysis = "";
		string sCost = txtCost_Rate.Text.Trim();
		string sRate = txtCost_Rate.Text.Trim();
		if (ItemTypeOp.CheckedIndex == 0)
		{
			if (cboType.SelectedIndex == 0)
			{
				if (cboAnalysis.SelectedIndex == 1)
				{
					sAnalysis = "1";
				}
			}
			else if (cboCostKind.SelectedIndex == 0)
			{
				sCostKind = "$";
			}
			else if (cboCostKind.SelectedIndex == 1)
			{
				sCostKind = "%";
			}
			else if (cboCostKind.SelectedIndex == 2)
			{
				sCostKind = "L";
			}
			else if (cboCostKind.SelectedIndex == 3)
			{
				sCostKind = "E";
			}
			else if (cboCostKind.SelectedIndex == 4)
			{
				sCostKind = "M";
			}
		}
		else if (ItemTypeOp.CheckedIndex == 1)
		{
			sCostKind = "Z";
		}
		else if (ItemTypeOp.CheckedIndex == 2)
		{
			sCostKind = "#";
		}
		double totalRate = ArchConvert.Obj2Double(txtLRate.Value) + ArchConvert.Obj2Double(txtERate.Value) + ArchConvert.Obj2Double(txtMRate.Value) + ArchConvert.Obj2Double(txtWRate.Value);
		if (totalRate > 100.001)
		{
			MessageBox.Show(this, "人機料及雜項比例總和 (" + totalRate + ") 不得大於 100% !", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtLRate.Focus();
			return;
		}
		string srckind = CommonMethods.GetActionNameString(F_ActionName);
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("WinFORM 基本工料");
		dbMrsBaseA = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
		if (F_EditMode == MrsBaseEditFormType.Edit)
		{
			dbMrsBaseA.ps_srckind = srckind;
		}
		else if (PubTools.IsMrsBaseSkip())
		{
			dbMrsBaseA.ps_srckind = srckind;
		}
		else
		{
			dbMrsBaseA.ps_srckind = "MRS";
		}
		dbMrsBaseA.ps_projectcode = F_ProjectCode;
		if (lblCost_Rate.Text.Substring(0, 2) == "單價")
		{
			sRate = "0";
			dbMrsBaseA.ps_cost = PubTools.Str2Decimal(sCost).ToString();
			dbMrsBaseA.ps_rate = PubTools.Str2Decimal(sRate).ToString();
			if (F_CallerFormName == "FormBudget" && F_sNO > 0)
			{
				ItemA dbItemA = new ItemA(aArr);
				dbItemA.ps_projectCode = F_ProjectCode;
				dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
				dbItemA.ps_PccesCode = pccesCode;
				dbItemA.ps_cName = txtCName.Text.Trim();
				dbItemA.ps_unitName = cboCUnit.Text.Trim();
				dbItemA.ps_eName = txtEName.Text.Trim();
				dbItemA.ps_eUnit = cboEUnit.Text.Trim();
				dbItemA.ps_lRate = txtLRate.Value.ToString();
				dbItemA.ps_eRate = txtERate.Value.ToString();
				dbItemA.ps_mRate = txtMRate.Value.ToString();
				dbItemA.ps_wRate = txtWRate.Value.ToString();
				dbItemA.ps_cost = sCost;
				dbItemA.ps_memo = txtMemo.Text.Trim();
				dbItemA.ps_surName = txtSurName.Text.Trim();
				dbItemA.ps_Issue = F_chgCount;
				dbItemA.ps_sNo = F_sNO.ToString();
				dbItemA.UpdItem();
				dbItemA = null;
			}
			else
			{
				bool flag = 0 == 0;
			}
		}
		else
		{
			sCost = "0";
			dbMrsBaseA.ps_cost = PubTools.Str2Decimal(sCost).ToString();
			dbMrsBaseA.ps_rate = PubTools.Str2Decimal(sRate).ToString();
		}
		dbMrsBaseA.ps_analysis = ((sAnalysis.Trim() == "") ? "0" : sAnalysis);
		dbMrsBaseA.ps_costKind = sCostKind;
		dbMrsBaseA.ps_pccesCode = pccesCode;
		dbMrsBaseA.ps_cName = txtCName.Text.Trim();
		dbMrsBaseA.ps_extendCode = txtExtendCode.Text.Trim();
		dbMrsBaseA.ps_unitName = cboCUnit.Text.Trim();
		dbMrsBaseA.ps_eName = txtEName.Text.Trim();
		dbMrsBaseA.ps_eUnit = cboEUnit.Text.Trim();
		dbMrsBaseA.ps_memo = txtMemo.Text.Trim();
		dbMrsBaseA.ps_lRate = txtLRate.Value.ToString();
		dbMrsBaseA.ps_eRate = txtERate.Value.ToString();
		dbMrsBaseA.ps_mRate = txtMRate.Value.ToString();
		dbMrsBaseA.ps_wRate = txtWRate.Value.ToString();
		dbMrsBaseA.ps_analysisQty = lblAnalysisQty.Text;
		dbMrsBaseA.ps_surName = txtSurName.Text.Trim();
		dbMrsBaseA.ps_Issue = F_chgCount;
		dbMrsBaseA.ps_xNameC = area;
		F_costKind = sCostKind;
		int iTransationState = 0;
		if (F_EditMode == MrsBaseEditFormType.Edit)
		{
			iTransationState = dbMrsBaseA.UpdItem();
			if (F_CallerFormName.ToUpper() == "FormMrsBaseBreakdown".ToUpper())
			{
				Archnowledge.Pcces.BUDClass.MrsBaseB dbMrsBaseB = new Archnowledge.Pcces.BUDClass.MrsBaseB(aArr);
				if (F_EditMode == MrsBaseEditFormType.Edit)
				{
					dbMrsBaseB.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
					dbMrsBaseB.ps_cost = Convert.ToDecimal(sCost).ToString();
					dbMrsBaseB.ps_projectcode = F_ProjectCode;
					dbMrsBaseB.ps_parentCode = F_ParentCode;
					dbMrsBaseB.ps_pubCode = iPubCode.ToString();
					dbMrsBaseB.ps_Issue = F_chgCount;
					dbMrsBaseB.UpdItem();
				}
				dbMrsBaseB = null;
			}
			if (F_CallerFormName == "FormBudget" && F_sNO < 0)
			{
				ItemA dbItemA = new ItemA(aArr);
				dbItemA.ps_projectCode = F_ProjectCode;
				dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
				dbItemA.ps_PccesCode = pccesCode;
				dbItemA.ps_cName = txtCName.Text.Trim();
				dbItemA.ps_unitName = cboCUnit.Text.Trim();
				dbItemA.ps_eName = txtEName.Text.Trim();
				dbItemA.ps_eUnit = cboEUnit.Text.Trim();
				dbItemA.ps_lRate = txtLRate.Value.ToString();
				dbItemA.ps_eRate = txtERate.Value.ToString();
				dbItemA.ps_mRate = txtMRate.Value.ToString();
				dbItemA.ps_wRate = txtWRate.Value.ToString();
				dbItemA.ps_cost = sCost;
				dbItemA.ps_memo = txtMemo.Text.Trim();
				dbItemA.ps_surName = txtSurName.Text.Trim();
				dbItemA.ps_Issue = F_chgCount;
				dbItemA.ps_sNo = F_sNO.ToString();
				dbItemA.UpdItemByPccesCode();
				dbItemA = null;
			}
		}
		else if (F_EditMode == MrsBaseEditFormType.New || F_EditMode == MrsBaseEditFormType.CopyToNew)
		{
			if (F_Mesbox != null)
			{
				if (!PubTools.IsMrsBaseSkip() && MessageBox.Show(this, "是否將複製工項新增至基本資料庫中？", "訊息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					iTransationState = dbMrsBaseA.InseItem();
					if (iTransationState == -2)
					{
						MessageBox.Show(this, "已有相同工項代碼資料存在！", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					if (F_UserID.ToUpper() == "PCCES")
					{
						dbMrsBaseA.SetPost(pccesCode.Trim(), "1");
					}
					int o_PubCode = dbMrsBaseA.Get_Pubcode(pccesCode.Trim());
					if (o_PubCode > 0)
					{
						ArrayList arrayList;
						(arrayList = aArr)[1] = string.Concat(arrayList[1], "-新增單價分析子項");
						Archnowledge.Pcces.BUDClass.MrsBaseB MrsBCom = new Archnowledge.Pcces.BUDClass.MrsBaseB(aArr);
						MrsBCom.ps_srckind = "MRS";
						MrsBCom.ps_parentCode = o_PubCode.ToString();
						MrsBCom.ps_Issue = F_chgCount;
						MrsBCom.DeleItems();
						DataTable MrsBDT = MrsBCom.ListItem(iPubCode);
						foreach (DataRow dr in MrsBDT.Rows)
						{
							MrsBCom.ps_amount = dr["Amount"].ToString();
							MrsBCom.ps_cost = dr["Cost"].ToString();
							MrsBCom.ps_listNo = dr["ListNo"].ToString();
							MrsBCom.ps_qty = dr["Qty"].ToString();
							MrsBCom.ps_pubCode = dr["pubCode"].ToString();
							MrsBCom.InseItem();
						}
						MrsBCom = null;
					}
				}
			}
			else
			{
				iTransationState = dbMrsBaseA.InseItem();
				if (iTransationState == -2)
				{
					MessageBox.Show(this, "已有相同工項代碼資料存在！", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (F_CallerFormName == "FormBudget" && F_sNO < 0)
				{
					ItemA dbItemA = new ItemA(aArr);
					dbItemA.ps_projectCode = F_ProjectCode;
					dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
					dbItemA.ps_PccesCode = pccesCode;
					dbItemA.ps_cName = txtCName.Text.Trim();
					dbItemA.ps_unitName = cboCUnit.Text.Trim();
					dbItemA.ps_eName = txtEName.Text.Trim();
					dbItemA.ps_eUnit = cboEUnit.Text.Trim();
					dbItemA.ps_lRate = txtLRate.Value.ToString();
					dbItemA.ps_eRate = txtERate.Value.ToString();
					dbItemA.ps_mRate = txtMRate.Value.ToString();
					dbItemA.ps_wRate = txtWRate.Value.ToString();
					dbItemA.ps_cost = sCost;
					dbItemA.ps_memo = txtMemo.Text.Trim();
					dbItemA.ps_surName = txtSurName.Text.Trim();
					dbItemA.ps_Issue = F_chgCount;
					dbItemA.ps_sNo = F_sNO.ToString();
					dbItemA.UpdItemByPccesCode();
					dbItemA = null;
				}
				if (F_UserID.ToUpper() == "PCCES")
				{
					dbMrsBaseA.SetPost(pccesCode.Trim(), "1");
				}
			}
			if ((F_EditMode == MrsBaseEditFormType.New || F_EditMode == MrsBaseEditFormType.CopyToNew) && sAnalysis == "1")
			{
				int i_PubCode = iPubCode;
				int o_PubCode = dbMrsBaseA.Get_Pubcode(pccesCode.Trim());
				if (i_PubCode > 0 && o_PubCode > 0)
				{
					ArrayList arrayList;
					(arrayList = aArr)[1] = string.Concat(arrayList[1], "-新增單價分析子項");
					Archnowledge.Pcces.BUDClass.MrsBaseB MrsBCom = new Archnowledge.Pcces.BUDClass.MrsBaseB(aArr);
					MrsBCom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
					MrsBCom.ps_parentCode = o_PubCode.ToString();
					MrsBCom.ps_Issue = F_chgCount;
					MrsBCom.DeleItems();
					if (srckind.ToUpper() == "BUD")
					{
						MrsBCom.ps_projectcode = F_ProjectCode;
					}
					DataTable MrsBDT = MrsBCom.ListItem(i_PubCode);
					foreach (DataRow dr in MrsBDT.Rows)
					{
						MrsBCom.ps_amount = dr["Amount"].ToString();
						MrsBCom.ps_cost = dr["Cost"].ToString();
						MrsBCom.ps_listNo = dr["ListNo"].ToString();
						MrsBCom.ps_qty = dr["Qty"].ToString();
						MrsBCom.ps_pubCode = dr["pubCode"].ToString();
						MrsBCom.InseItem();
						DBClass DBCLS = new DBClass();
						DBCLS._FS_UserID = F_UserID;
						DataTable MrsCDT = DBCLS.GetUserDefine("Select * from budProjMrsC Where ParentCode=" + i_PubCode + " And PubCode=" + MrsBCom.ps_pubCode + " and projectCode = '" + F_ProjectCode + "'");
						Archnowledge.Pcces.BUDClass.MrsBaseC MrsCCom = new Archnowledge.Pcces.BUDClass.MrsBaseC(aArr);
						MrsCCom.ps_projectcode = F_ProjectCode;
						MrsCCom.ps_chgCount = F_chgCount;
						MrsCCom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
						foreach (DataRow dr_C in MrsCDT.Rows)
						{
							MrsCCom.InseItem(PubTools.Str2Int(o_PubCode), PubTools.Str2Int(MrsBCom.ps_pubCode), PubTools.Str2Int(dr_C["itemCode"]), PubTools.Str2Int(dr_C["PubListNo"]), PubTools.Str2Int(dr_C["ItemListNo"]));
						}
						DBCLS = null;
						MrsCCom = null;
					}
					MrsBCom = null;
				}
			}
			if (F_CallerFormName.ToUpper() == "frmMrsBase".ToUpper())
			{
				Form ActiveForm = base.Owner.ActiveMdiChild;
				int o_PubCode = dbMrsBaseA.Get_Pubcode(pccesCode.Trim());
				if (ActiveForm is frmMrsBase)
				{
					(ActiveForm as frmMrsBase)._NewAddItem_PccesCode = pccesCode.Trim();
					(ActiveForm as frmMrsBase)._NewAddItem_PubCode = o_PubCode.ToString();
				}
			}
			string getpubCode = "";
			if (F_Mesbox == null)
			{
				if (F_ActionName != PccesFormAction.MrsBase || F_ActionName != PccesFormAction.None)
				{
					dbMrsBaseA.ps_pubCode = dbMrsBaseA.Get_Pubcode(pccesCode.Trim()).ToString();
					dbMrsBaseA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
					dbMrsBaseA.ps_projectcode = F_ProjectCode;
					dbMrsBaseA.InseItem();
				}
			}
			else if (F_ActionName != PccesFormAction.MrsBase || F_ActionName != PccesFormAction.None)
			{
				int iCount = dbMrsBaseA.Get_Pubcode(pccesCode.Trim());
				if (iCount == -2)
				{
					int i_PubCode = iPubCode;
					int o_PubCode = 0;
					string sSQL = "select Max(pubcode)as pubCode from budProjMrsA where projectCode ='" + F_ProjectCode + "'";
					ModifyDB ModDB = new ModifyDB(F_ProjectCode, aArr);
					DataTable DT = new DataTable();
					DT = ModDB.DBList(sSQL);
					if (DT.Rows.Count > 0)
					{
						int num = PubTools.Str2Int(DT.Rows[0]["pubcode"].ToString());
						o_PubCode = ((num < 9999900) ? 9999900 : (num + 1));
						if (i_PubCode > 0 && o_PubCode > 0)
						{
							getpubCode = o_PubCode.ToString();
							ArrayList arrayList;
							(arrayList = aArr)[1] = string.Concat(arrayList[1], "-新增單價分析子項");
							Archnowledge.Pcces.BUDClass.MrsBaseB MrsBCom = new Archnowledge.Pcces.BUDClass.MrsBaseB(aArr);
							if (srckind.ToUpper() == "BUD")
							{
								MrsBCom.ps_projectcode = F_ProjectCode;
							}
							MrsBCom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
							MrsBCom.ps_parentCode = o_PubCode.ToString();
							MrsBCom.ps_Issue = F_chgCount;
							MrsBCom.DeleItems();
							DataTable MrsBDT = MrsBCom.ListItem(i_PubCode);
							foreach (DataRow dr in MrsBDT.Rows)
							{
								MrsBCom.ps_amount = dr["Amount"].ToString();
								MrsBCom.ps_cost = dr["Cost"].ToString();
								MrsBCom.ps_listNo = dr["ListNo"].ToString();
								MrsBCom.ps_qty = dr["Qty"].ToString();
								MrsBCom.ps_pubCode = dr["pubCode"].ToString();
								MrsBCom.InseItem();
								DBClass DBCLS = new DBClass();
								DBCLS._FS_UserID = F_UserID;
								DataTable MrsCDT = DBCLS.GetUserDefine("Select * from budProjMrsC Where ParentCode=" + i_PubCode + " And PubCode=" + MrsBCom.ps_pubCode + " and projectCode = '" + F_ProjectCode + "'");
								Archnowledge.Pcces.BUDClass.MrsBaseC MrsCCom = new Archnowledge.Pcces.BUDClass.MrsBaseC(aArr);
								MrsCCom.ps_projectcode = F_ProjectCode;
								MrsCCom.ps_chgCount = F_chgCount;
								MrsCCom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
								foreach (DataRow dr_C in MrsCDT.Rows)
								{
									MrsCCom.InseItem(PubTools.Str2Int(o_PubCode), PubTools.Str2Int(MrsBCom.ps_pubCode), PubTools.Str2Int(dr_C["itemCode"]), PubTools.Str2Int(dr_C["PubListNo"]), PubTools.Str2Int(dr_C["ItemListNo"]));
								}
								DBCLS = null;
								MrsCCom = null;
							}
							MrsBCom = null;
						}
					}
				}
				if (getpubCode != "")
				{
					dbMrsBaseA.ps_pubCode = getpubCode;
				}
				else
				{
					dbMrsBaseA.ps_pubCode = dbMrsBaseA.Get_Pubcode(pccesCode.Trim()).ToString();
				}
				dbMrsBaseA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
				dbMrsBaseA.ps_projectcode = F_ProjectCode;
				dbMrsBaseA.InseItem();
			}
			if (base.Owner is FormMrsBaseBreakdown)
			{
				(base.Owner as FormMrsBaseBreakdown).NewChildPubCode = dbMrsBaseA.Get_Pubcode(pccesCode.Trim());
				(base.Owner as FormMrsBaseBreakdown).NewChildCost = Convert.ToDecimal(sCost);
				(base.Owner as FormMrsBaseBreakdown).NewChildRate = Convert.ToDecimal(sRate);
			}
			if (F_CallerFormName.ToUpper() == "FORMBUDGET")
			{
				Form ActiveForm = base.Owner.ActiveMdiChild;
				if (ActiveForm is frmBudget)
				{
					if (getpubCode != "")
					{
						(ActiveForm as frmBudget)._NewChildPubCode = PubTools.Str2Int(getpubCode);
					}
					else
					{
						(ActiveForm as frmBudget)._NewChildPubCode = dbMrsBaseA.Get_Pubcode(pccesCode.Trim());
					}
					(ActiveForm as frmBudget)._NewChildCost = Convert.ToDecimal(sCost);
					(ActiveForm as frmBudget)._NewChildRate = Convert.ToDecimal(sRate);
					(ActiveForm as frmBudget)._SurName = txtSurName.Text.Trim();
				}
			}
		}
		base.DialogResult = DialogResult.OK;
		Close();
	}

	private bool CheckPccesCodeValidity()
	{
		string Message = "";
		string pccesCode = txtPccesCode.Text.Trim();
		if (F_Record == "CopyToNew" && txtPccesCode.Text == F_OriginalPccescode)
		{
			Message = "執行複製工項代碼不可相同。";
		}
		if (pccesCode == "")
		{
			Message = "工項代碼不可以是空白。";
		}
		else if (F_EditMode == MrsBaseEditFormType.New || F_EditMode == MrsBaseEditFormType.CopyToNew)
		{
			AutoNum autoNum = new AutoNum();
			ExecResult ER = autoNum.CheckPccesCodeValidity(pccesCode);
			if (ER.ReturnCode != 0)
			{
				Message = ER.Message;
			}
			else
			{
				Archnowledge.Pcces.DomainModule.MrsBase.MrsBaseA mrsBaseA = new Archnowledge.Pcces.DomainModule.MrsBase.MrsBaseA();
				if (mrsBaseA.IsCommonItem(pccesCode))
				{
					Message = pccesCode + " 為共通性項目編碼，不得新增此工項代碼！";
				}
			}
		}
		if (Message != "")
		{
			MessageBox.Show(Message, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			txtPccesCode.Focus();
			return false;
		}
		return true;
	}

	private void FormMrsBaseEdit_FormClosing(object sender, FormClosingEventArgs e)
	{
		Frm.Close();
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		DBCLS.MrsBase_UnLock(iPubCode.ToString(), F_ProjectCode, CommonMethods.GetActionNameString(F_ActionName));
		DBCLS = null;
	}

	private void txtPccesCode_KeyUp(object sender, KeyEventArgs e)
	{
		if (F_Record == "CopyToNew")
		{
			return;
		}
		string inputPccesCode = txtPccesCode.Text.ToUpper();
		if (txtPccesCode.TextLength > 0)
		{
			if (inputPccesCode.StartsWith("L"))
			{
				txtLRate.Text = "100";
				txtERate.Text = "0";
				txtMRate.Text = "0";
				txtWRate.Text = "0";
				cboType.SelectedIndex = 0;
			}
			if (inputPccesCode.StartsWith("E"))
			{
				txtLRate.Text = "0";
				txtERate.Text = "100";
				txtMRate.Text = "0";
				txtWRate.Text = "0";
				cboType.SelectedIndex = 0;
			}
			if (inputPccesCode.StartsWith("M"))
			{
				txtLRate.Text = "0";
				txtERate.Text = "0";
				txtMRate.Text = "100";
				txtWRate.Text = "0";
				cboType.SelectedIndex = 0;
			}
			if (inputPccesCode.StartsWith("W"))
			{
				txtLRate.Text = "0";
				txtERate.Text = "0";
				txtMRate.Text = "0";
				txtWRate.Text = "100";
				cboType.SelectedIndex = 1;
			}
		}
		else
		{
			txtLRate.Text = "0";
			txtERate.Text = "0";
			txtMRate.Text = "0";
			txtWRate.Text = "0";
			cboType.SelectedIndex = 0;
		}
	}

	private void txtPccesCode_Leave(object sender, EventArgs e)
	{
		string pccesCode = txtPccesCode.Text.Trim();
		string MaterailPrefix = "產品，";
		if (pccesCode.StartsWith("M") && !txtCName.Text.StartsWith(MaterailPrefix))
		{
			if (!SysConfig.SysChangeManagement)
			{
				txtCName.Text = MaterailPrefix + txtCName.Text;
			}
		}
		else if (txtCName.Text.StartsWith(MaterailPrefix))
		{
			txtCName.Text.Remove(0, 3);
		}
	}

	private void FormMrsBaseEdit_Activated(object sender, EventArgs e)
	{
		if (FormStatus == FormStatus.Active)
		{
			FormStatus = FormStatus.Normal;
		}
	}

	private void lblCost_Rate_TextChanged(object sender, EventArgs e)
	{
		if (FormStatus == FormStatus.Normal)
		{
			if (lblCost_Rate.Text == "單價：" && F_EditMode == MrsBaseEditFormType.Edit)
			{
				cboHisPrice.Visible = true;
				BtnPickPrice.Visible = true;
			}
			else
			{
				txtCost_Rate.Text = "0";
				BtnPickPrice.Visible = false;
				cboHisPrice.Visible = false;
			}
		}
	}

	private void txtPccesCode_Validating(object sender, CancelEventArgs e)
	{
		if (!CommonMethods.IsStrByteLenValid(txtPccesCode.Text, 20))
		{
			MessageBox.Show(this, "工項代碼的長度不可超過 20 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtPccesCode.Focus();
		}
		else if (!CommonMethods.IsStrByteLenValid(txtExtendCode.Text, 20))
		{
			MessageBox.Show(this, "工項外碼的長度不可超過 20 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtExtendCode.Focus();
		}
		else if (!CommonMethods.IsStrByteLenValid(txtCName.Text, 200))
		{
			MessageBox.Show(this, "工項名稱的長度不可超過 200 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtCName.Focus();
		}
		else if (!CommonMethods.IsStrByteLenValid(txtEName.Text, 200))
		{
			MessageBox.Show(this, "Description 的長度不可超過 200 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtEName.Focus();
		}
		else if (!CommonMethods.IsStrByteLenValid(txtMemo.Text, 200))
		{
			MessageBox.Show(this, "備註的長度不可超過 200 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtMemo.Focus();
		}
		else
		{
			if (!((sender as Control).Name == "txtPccesCode"))
			{
				return;
			}
			for (int i = 0; i < txtPccesCode.Text.Length; i++)
			{
				if (i == 0 && txtPccesCode.Text[i].ToString().Trim() != "" && txtPccesCode.Text[i].ToString().Trim() == "#")
				{
					ItemTypeOp.CheckedIndex = 2;
				}
				else if ((txtPccesCode.Text[i] < '0' || txtPccesCode.Text[i] > '9') && (txtPccesCode.Text[i] < 'A' || txtPccesCode.Text[i] > 'Z') && (txtPccesCode.Text[i] < 'a' || txtPccesCode.Text[i] > 'z') && !(txtPccesCode.Text[i].ToString().Trim() == "-"))
				{
					MessageBox.Show(this, "工程代碼不可以有英文字母及數字以外的內容。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtPccesCode.Focus();
				}
			}
		}
	}

	private void cboEUnit_Validating(object sender, CancelEventArgs e)
	{
		if (cboCUnit.Text != null && !CommonMethods.IsStrByteLenValid(cboCUnit.Text, 10))
		{
			MessageBox.Show(this, "單位的長度不可超過 10 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			cboCUnit.Focus();
		}
		else if (cboEUnit.Text != null && !CommonMethods.IsStrByteLenValid(cboEUnit.Text, 20))
		{
			MessageBox.Show(this, "Unit 的長度不可超過 20 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			cboEUnit.Focus();
		}
	}

	private void FormMrsBaseEdit_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.F1)
		{
			Frm.Show();
			Frm.BringToFront();
		}
	}

	private void UserReq(object sender, EventArgs e)
	{
		UserRequestEventArgs ee = (UserRequestEventArgs)e;
		DispatchString(ee.Request.ToString());
	}

	private void DispatchString(string ssString)
	{
		try
		{
			Cntrl1 = base.ActiveControl;
			iTextBeamPos = (Cntrl1 as TextBox).SelectionStart;
			if ((Cntrl1 as TextBox).SelectedText.Length > 1)
			{
				(Cntrl1 as TextBox).Text = (Cntrl1 as TextBox).Text.Replace((Cntrl1 as TextBox).SelectedText, ssString);
			}
			else
			{
				int iPos = iTextBeamPos;
				int iLen = Cntrl1.Text.Length;
				string Str1 = Cntrl1.Text.Substring(0, iPos);
				string Str2 = Cntrl1.Text.Substring(iPos);
				Cntrl1.Text = Str1 + ssString + Str2;
			}
			iTextBeamPos++;
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBaseEdit.cs" + ex.Message);
			Console.Write(ex.Message);
		}
	}

	private void CorrectRatio()
	{
		double ratio = CommonMethods.GetWindowRatio(base.Handle);
		if (ratio != 1.0)
		{
			panel1.Font = new Font(panel1.Font.Name, (float)((double)panel1.Font.Size * ratio));
			groupBox1.Font = new Font(groupBox1.Font.Name, (float)((double)groupBox1.Font.Size * ratio));
			groupBox3.Font = new Font(groupBox3.Font.Name, (float)((double)groupBox3.Font.Size * ratio));
			groupBox2.Font = new Font(groupBox2.Font.Name, (float)((double)groupBox2.Font.Size * ratio));
			groupBox4.Font = new Font(groupBox4.Font.Name, (float)((double)groupBox4.Font.Size * ratio));
		}
	}

	private void ultraButton2_Click(object sender, EventArgs e)
	{
		if (txtPccesCode.Text.Trim() == "")
		{
			MessageBox.Show(this, "工項代碼不可空白", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (iPubCode == -1)
		{
			ArrayList aArr = new ArrayList();
			aArr.Add(F_UserID);
			aArr.Add("找出pubcode");
			dbMrsBaseA = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
			dbMrsBaseA.ps_srckind = "MRS";
			dbMrsBaseA.ps_projectcode = F_ProjectCode;
			dbMrsBaseA.ps_pccesCode = txtPccesCode.Text.Trim();
			dbMrsBaseA.InseItem();
			string sSQL = "Select pubcode from mrsBaseA where pccescode = '" + txtPccesCode.Text.Trim() + "'";
			ModifyDB ModDB = new ModifyDB(F_ProjectCode, aArr);
			DataTable DT = new DataTable();
			DT = ModDB.DBList(sSQL);
			if (DT.Rows.Count > 0)
			{
				iPubCode = PubTools.Str2Int(DT.Rows[0]["pubcode"].ToString());
			}
			F_EditMode = MrsBaseEditFormType.Edit;
			F_ItemClassflag = "Edit_ItemClass";
		}
		FormBDGT_ItemClass FM_ITMSET_Class = new FormBDGT_ItemClass();
		FM_ITMSET_Class._UserID = F_UserID;
		FM_ITMSET_Class._PubCode = iPubCode;
		FM_ITMSET_Class._status = "choose";
		if (FM_ITMSET_Class.ShowDialog(this) == DialogResult.OK)
		{
			txtClass.Text = F_Cstring;
		}
		FM_ITMSET_Class.Close();
		FM_ITMSET_Class.Dispose();
		FM_ITMSET_Class = null;
	}

	private void FormMrsBaseEdit_FormClosed(object sender, FormClosedEventArgs e)
	{
		panel1 = null;
		panel2 = null;
		groupBox3 = null;
		ultraLabel8 = null;
		ultraLabel7 = null;
		ultraLabel6 = null;
		ultraLabel1 = null;
		ultraLabel5 = null;
		ultraLabel4 = null;
		ultraLabel3 = null;
		ultraLabel2 = null;
		lblCost_Rate = null;
		groupBox2 = null;
		groupBox1 = null;
		panel3 = null;
		panel4 = null;
		groupBox4 = null;
		ultraLabel9 = null;
		ultraLabel10 = null;
		ultraLabel11 = null;
		ultraLabel12 = null;
		txtPccesCode = null;
		lblCode = null;
		txtCName = null;
		txtEName = null;
		ultraLabel14 = null;
		txtMemo = null;
		ultraLabel15 = null;
		BtnCancel = null;
		BtnOK = null;
		cboCostKind = null;
		cboType = null;
		txtExtendCode = null;
		cboCUnit = null;
		cboEUnit = null;
		ItemTypeOp = null;
		cboAnalysis = null;
		BtnPickPrice = null;
		cboHisPrice = null;
		ultraButton1 = null;
		panel5 = null;
		ultraLabel13 = null;
		axPVLine3D1 = null;
		panel6 = null;
		axPVLine3D2 = null;
		panel7 = null;
		ultraLabel16 = null;
		ultraLabel17 = null;
		ultraLabel24 = null;
		ultraLabel25 = null;
		ultraLabel26 = null;
		lblHisUpper = null;
		lblHisLower = null;
		lblHisAvg = null;
		lblCesUpper = null;
		lblCesLower = null;
		lblCesAvg = null;
		components = null;
		txtCost_Rate = null;
		txtLRate = null;
		txtERate = null;
		txtMRate = null;
		txtWRate = null;
		Frm = null;
		dbMrsBaseA = null;
		lblAnalysisQty = null;
		ultraLabel18 = null;
		ultraButton2 = null;
		txtClass = null;
		txtSurName = null;
		ultraLabel19 = null;
		GC.Collect();
	}

	private void SetCostRateMask(bool IsDefault)
	{
		if (IsDefault)
		{
			txtCost_Rate.MaskInput = DefaultFormatMaskInput;
			txtCost_Rate.FormatString = "###.##";
		}
		else
		{
			txtCost_Rate.MaskInput = CustomFormatMaskInput;
			txtCost_Rate.FormatString = "N" + F_MainCst.ToString();
		}
	}

	private void txtCost_Rate_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar.ToString() == "１" || e.KeyChar.ToString() == "２" || e.KeyChar.ToString() == "３" || e.KeyChar.ToString() == "４" || e.KeyChar.ToString() == "５" || e.KeyChar.ToString() == "６" || e.KeyChar.ToString() == "７" || e.KeyChar.ToString() == "８" || e.KeyChar.ToString() == "９" || e.KeyChar.ToString() == "０")
		{
			e.Handled = true;
		}
	}

	private void txtCost_Rate_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void txtCost_Rate_Leave(object sender, EventArgs e)
	{
		if (!(lblCost_Rate.Text == "百分比:"))
		{
		}
	}
}
