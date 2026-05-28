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
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.PccesMain.Budget.BDGT_Component;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetEditMain : Form
{
	private const string CallFormHelp = "FormBudgetEditMain";

	private IContainer components;

	private Panel panel1;

	private Panel panel2;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private UltraOptionSet optItemType;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel3;

	private UltraCombo cboEUnit;

	private UltraCombo cboCUnit;

	private UltraLabel ultraLabel15;

	private UltraLabel ultraLabel14;

	private TextBox textBox4;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel5;

	private Panel panel3;

	private Panel panel4;

	private UltraLabel ultraLabel7;

	private UltraButton BtnOK;

	private UltraButton BtnCancel;

	private GroupBox groupBox3;

	private Panel PNL_CHILD;

	private TextBox txtItemNo;

	private TextBox txtQty;

	private UltraLabel lblRound;

	private UltraComboEditor cboRound;

	private TextBox txtMemo;

	private UltraComboEditor txtCName;

	private UltraComboEditor txtEName;

	private UltraComboEditor cboShareItem;

	private UltraLabel lblShareItem;

	private System.Windows.Forms.ToolTip toolTip1;

	private TextBox txtPccesCode;

	private UltraCheckEditor CB_PrintToAnalysis;

	private UltraLabel lblPccesCode;

	private FormStatus F_FORM_STATUS = FormStatus.Iinitial;

	private DataTable DT_Temp = new DataTable();

	private int F_Issue;

	private int iTextBeamPos = 0;

	private Control Cntrl1;

	private FormSymbol Frm = new FormSymbol();

	private string F_ShareItemSno;

	private ArrayList F_ShareItems;

	private string F_UserID;

	private PccesFormAction F_ActionName;

	private BDGT_ITEM_TYPE F_ItemType = BDGT_ITEM_TYPE.None;

	private string F_ProjectCode = "";

	private int F_Item_sNo = -1;

	private string F_Formula = "";

	private int F_ChildCount = 0;

	private string F_PrintNo = "0000";

	private decimal F_Cost = 0m;

	private decimal F_Rate = 0m;

	private string F_PrintToAnalysis;

	private string F_PccesCode;

	private bool F_IsCanPrintToAnalysis;

	private bool F_IsCostStructure = false;

	private DataTable DT_BDGT = new DataTable();

	private bool F_Istemplate = false;

	private bool F_AllowRestrictEdit = false;

	private int decItemQty;

	private int decItemCost;

	private int decItemAmt;

	private int decAnalQty;

	private int decAnalCost;

	private int decAnalAmt;

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

	public string _ShareItemSno
	{
		get
		{
			return F_ShareItemSno;
		}
		set
		{
			F_ShareItemSno = value;
		}
	}

	public ArrayList _ShareItems
	{
		get
		{
			return F_ShareItems;
		}
		set
		{
			F_ShareItems = value;
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

	public string ProjectCode
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

	public string FormulaStr
	{
		get
		{
			return F_Formula;
		}
		set
		{
			F_Formula = value;
		}
	}

	public int Item_sNo
	{
		get
		{
			return F_Item_sNo;
		}
		set
		{
			F_Item_sNo = value;
		}
	}

	public string PrintNo
	{
		get
		{
			return F_PrintNo;
		}
		set
		{
			F_PrintNo = value;
		}
	}

	public decimal ItemCost
	{
		get
		{
			return F_Cost;
		}
		set
		{
			F_Cost = value;
		}
	}

	public decimal ItemRate
	{
		get
		{
			return F_Rate;
		}
		set
		{
			F_Rate = value;
		}
	}

	public int ChildCount
	{
		set
		{
			F_ChildCount = value;
		}
	}

	public BDGT_ITEM_TYPE ItemType
	{
		get
		{
			return F_ItemType;
		}
		set
		{
			F_ItemType = value;
		}
	}

	public string _PrintToAnalysis
	{
		get
		{
			return F_PrintToAnalysis;
		}
		set
		{
			F_PrintToAnalysis = value;
		}
	}

	public string _PccesCode
	{
		get
		{
			return F_PccesCode;
		}
		set
		{
			F_PccesCode = value;
		}
	}

	public bool _IsCanPrintToAnalysis
	{
		get
		{
			return F_IsCanPrintToAnalysis;
		}
		set
		{
			F_IsCanPrintToAnalysis = value;
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

	public bool _AllowRestrictEdit
	{
		get
		{
			return F_AllowRestrictEdit;
		}
		set
		{
			F_AllowRestrictEdit = value;
		}
	}

	public bool _IsCostStructure
	{
		get
		{
			return F_IsCostStructure;
		}
		set
		{
			F_IsCostStructure = value;
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinGrid.UltraGridLayout ultraGridLayout1 = new Infragistics.Win.UltraWinGrid.UltraGridLayout();
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(86092282);
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
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
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem12 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem13 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem14 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem15 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem16 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem17 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetEditMain));
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.txtPccesCode = new System.Windows.Forms.TextBox();
		this.lblPccesCode = new Infragistics.Win.Misc.UltraLabel();
		this.CB_PrintToAnalysis = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.txtEName = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.txtCName = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.textBox4 = new System.Windows.Forms.TextBox();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.cboEUnit = new Infragistics.Win.UltraWinGrid.UltraCombo();
		this.cboCUnit = new Infragistics.Win.UltraWinGrid.UltraCombo();
		this.ultraLabel15 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.txtItemNo = new System.Windows.Forms.TextBox();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.cboShareItem = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.lblShareItem = new Infragistics.Win.Misc.UltraLabel();
		this.cboRound = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.lblRound = new Infragistics.Win.Misc.UltraLabel();
		this.txtQty = new System.Windows.Forms.TextBox();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.optItemType = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.panel3 = new System.Windows.Forms.Panel();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.PNL_CHILD = new System.Windows.Forms.Panel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.BtnOK = new Infragistics.Win.Misc.UltraButton();
		this.BtnCancel = new Infragistics.Win.Misc.UltraButton();
		this.txtMemo = new System.Windows.Forms.TextBox();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtEName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboEUnit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCUnit).BeginInit();
		this.panel2.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboShareItem).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboRound).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.optItemType).BeginInit();
		this.panel3.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.panel4.SuspendLayout();
		base.SuspendLayout();
		this.panel1.Controls.Add(this.txtPccesCode);
		this.panel1.Controls.Add(this.lblPccesCode);
		this.panel1.Controls.Add(this.CB_PrintToAnalysis);
		this.panel1.Controls.Add(this.txtEName);
		this.panel1.Controls.Add(this.txtCName);
		this.panel1.Controls.Add(this.textBox4);
		this.panel1.Controls.Add(this.ultraLabel4);
		this.panel1.Controls.Add(this.cboEUnit);
		this.panel1.Controls.Add(this.cboCUnit);
		this.panel1.Controls.Add(this.ultraLabel15);
		this.panel1.Controls.Add(this.ultraLabel14);
		this.panel1.Controls.Add(this.txtItemNo);
		this.panel1.Controls.Add(this.ultraLabel3);
		this.panel1.Controls.Add(this.ultraLabel2);
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(747, 96);
		this.panel1.TabIndex = 0;
		this.txtPccesCode.Location = new System.Drawing.Point(580, 8);
		this.txtPccesCode.Name = "txtPccesCode";
		this.txtPccesCode.Size = new System.Drawing.Size(156, 25);
		this.txtPccesCode.TabIndex = 24;
		this.txtPccesCode.Text = "[txtPccesCode]";
		this.txtPccesCode.Visible = false;
		this.lblPccesCode.Location = new System.Drawing.Point(500, 13);
		this.lblPccesCode.Name = "lblPccesCode";
		this.lblPccesCode.Size = new System.Drawing.Size(76, 18);
		this.lblPccesCode.TabIndex = 23;
		this.lblPccesCode.Text = "工項代碼:";
		this.lblPccesCode.Visible = false;
		this.CB_PrintToAnalysis.Location = new System.Drawing.Point(276, 12);
		this.CB_PrintToAnalysis.Name = "CB_PrintToAnalysis";
		this.CB_PrintToAnalysis.Size = new System.Drawing.Size(176, 20);
		this.CB_PrintToAnalysis.TabIndex = 22;
		this.CB_PrintToAnalysis.Text = "此項目列印至單價分析";
		this.toolTip1.SetToolTip(this.CB_PrintToAnalysis, "勾選後，此項目及其子項都會被視為單價分析，將不會列印在詳細表上");
		this.CB_PrintToAnalysis.Click += new System.EventHandler(CB_PrintToAnalysis_Click);
		appearance1.FontData.Name = "細明體";
		appearance1.FontData.SizeInPoints = 11f;
		this.txtEName.Appearance = appearance1;
		this.txtEName.AutoSize = true;
		this.txtEName.Location = new System.Drawing.Point(112, 66);
		this.txtEName.Name = "txtEName";
		this.txtEName.Size = new System.Drawing.Size(484, 24);
		this.txtEName.TabIndex = 21;
		this.txtEName.Text = null;
		this.txtEName.Validating += new System.ComponentModel.CancelEventHandler(txtEName_Validating);
		this.txtEName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtEName_KeyPress);
		appearance2.FontData.Name = "細明體";
		appearance2.FontData.SizeInPoints = 11f;
		this.txtCName.Appearance = appearance2;
		this.txtCName.AutoSize = true;
		this.txtCName.Location = new System.Drawing.Point(112, 38);
		this.txtCName.Name = "txtCName";
		this.txtCName.Size = new System.Drawing.Size(484, 24);
		this.txtCName.TabIndex = 20;
		this.txtCName.Text = null;
		this.txtCName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCName_KeyPress);
		this.textBox4.Location = new System.Drawing.Point(692, 8);
		this.textBox4.Name = "textBox4";
		this.textBox4.Size = new System.Drawing.Size(44, 25);
		this.textBox4.TabIndex = 19;
		this.textBox4.Text = "textBox4";
		this.textBox4.Visible = false;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance3;
		this.ultraLabel4.Location = new System.Drawing.Point(644, 12);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(44, 20);
		this.ultraLabel4.TabIndex = 18;
		this.ultraLabel4.Text = "父項:";
		this.ultraLabel4.Visible = false;
		this.cboEUnit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cboEUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
		this.cboEUnit.DisplayMember = "";
		this.cboEUnit.Location = new System.Drawing.Point(653, 66);
		this.cboEUnit.Name = "cboEUnit";
		this.cboEUnit.Size = new System.Drawing.Size(84, 24);
		this.cboEUnit.TabIndex = 17;
		this.cboEUnit.ValueMember = "";
		this.cboEUnit.Validating += new System.ComponentModel.CancelEventHandler(cboCUnit_Validating);
		this.cboEUnit.AfterCloseUp += new System.EventHandler(cboEUnit_AfterCloseUp);
		this.cboCUnit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cboCUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
		this.cboCUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Dotted;
		this.cboCUnit.DisplayLayout.BorderStyleCaption = Infragistics.Win.UIElementBorderStyle.Dashed;
		this.cboCUnit.DisplayMember = "";
		ultraGridLayout1.AutoFitColumns = true;
		valueList1.Key = "cString";
		ultraGridLayout1.ValueLists.Add(valueList1);
		this.cboCUnit.Layouts.Add(ultraGridLayout1);
		this.cboCUnit.Location = new System.Drawing.Point(653, 38);
		this.cboCUnit.Name = "cboCUnit";
		this.cboCUnit.Size = new System.Drawing.Size(84, 24);
		this.cboCUnit.TabIndex = 16;
		this.cboCUnit.ValueMember = "";
		this.cboCUnit.Validating += new System.ComponentModel.CancelEventHandler(cboCUnit_Validating);
		this.cboCUnit.AfterCloseUp += new System.EventHandler(cboCUnit_AfterCloseUp);
		this.ultraLabel15.Location = new System.Drawing.Point(608, 40);
		this.ultraLabel15.Name = "ultraLabel15";
		this.ultraLabel15.Size = new System.Drawing.Size(47, 20);
		this.ultraLabel15.TabIndex = 15;
		this.ultraLabel15.Text = "單位:";
		this.ultraLabel14.Location = new System.Drawing.Point(608, 65);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(47, 20);
		this.ultraLabel14.TabIndex = 14;
		this.ultraLabel14.Text = "Unit:";
		this.txtItemNo.Location = new System.Drawing.Point(112, 8);
		this.txtItemNo.Name = "txtItemNo";
		this.txtItemNo.Size = new System.Drawing.Size(156, 25);
		this.txtItemNo.TabIndex = 3;
		this.txtItemNo.Text = "[txtItemNo]";
		this.txtItemNo.Validating += new System.ComponentModel.CancelEventHandler(txtItemNo_Validating);
		appearance4.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance4;
		this.ultraLabel3.Location = new System.Drawing.Point(12, 64);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(100, 20);
		this.ultraLabel3.TabIndex = 2;
		this.ultraLabel3.Text = "Description:";
		appearance5.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance5;
		this.ultraLabel2.Location = new System.Drawing.Point(12, 40);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(92, 20);
		this.ultraLabel2.TabIndex = 1;
		this.ultraLabel2.Text = "項目及說明:";
		appearance6.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance6;
		this.ultraLabel1.Location = new System.Drawing.Point(12, 13);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(76, 18);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "項次:";
		this.panel2.Controls.Add(this.groupBox2);
		this.panel2.Controls.Add(this.groupBox1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 96);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(747, 116);
		this.panel2.TabIndex = 1;
		this.groupBox2.Controls.Add(this.cboShareItem);
		this.groupBox2.Controls.Add(this.lblShareItem);
		this.groupBox2.Controls.Add(this.cboRound);
		this.groupBox2.Controls.Add(this.lblRound);
		this.groupBox2.Controls.Add(this.txtQty);
		this.groupBox2.Controls.Add(this.ultraLabel5);
		this.groupBox2.Location = new System.Drawing.Point(480, 4);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(260, 108);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "數量與取位原則";
		appearance7.FontData.Name = "細明體";
		appearance7.FontData.SizeInPoints = 11f;
		this.cboShareItem.Appearance = appearance7;
		this.cboShareItem.AutoSize = true;
		this.cboShareItem.DropDownListAlignment = Infragistics.Win.DropDownListAlignment.Right;
		this.cboShareItem.DropDownListWidth = 400;
		this.cboShareItem.Location = new System.Drawing.Point(91, 77);
		this.cboShareItem.Name = "cboShareItem";
		this.cboShareItem.Size = new System.Drawing.Size(157, 24);
		this.cboShareItem.TabIndex = 24;
		this.cboShareItem.Text = "[cboShareItem]";
		appearance8.ForeColor = System.Drawing.Color.Green;
		appearance8.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblShareItem.Appearance = appearance8;
		this.lblShareItem.Location = new System.Drawing.Point(12, 80);
		this.lblShareItem.Name = "lblShareItem";
		this.lblShareItem.Size = new System.Drawing.Size(76, 20);
		this.lblShareItem.TabIndex = 23;
		this.lblShareItem.Text = "差額攤提:";
		appearance9.FontData.Name = "細明體";
		appearance9.FontData.SizeInPoints = 11f;
		this.cboRound.Appearance = appearance9;
		this.cboRound.AutoSize = true;
		valueListItem1.DataValue = "4";
		valueListItem1.DisplayText = "小數4位";
		valueListItem2.DataValue = "3";
		valueListItem2.DisplayText = "小數3位";
		valueListItem3.DataValue = "2";
		valueListItem3.DisplayText = "小數2位";
		valueListItem4.DataValue = "1";
		valueListItem4.DisplayText = "小數1位";
		valueListItem5.DataValue = "0";
		valueListItem5.DisplayText = "個\u3000位";
		valueListItem6.DataValue = "-1";
		valueListItem6.DisplayText = "拾\u3000數";
		valueListItem7.DataValue = "-2";
		valueListItem7.DisplayText = "佰\u3000位";
		valueListItem8.DataValue = "-3";
		valueListItem8.DisplayText = "千\u3000位";
		valueListItem9.DataValue = "-4";
		valueListItem9.DisplayText = "萬\u3000位";
		valueListItem10.DataValue = "-5";
		valueListItem10.DisplayText = "拾萬位";
		valueListItem11.DataValue = "-6";
		valueListItem11.DisplayText = "百萬位";
		this.cboRound.Items.Add(valueListItem1);
		this.cboRound.Items.Add(valueListItem2);
		this.cboRound.Items.Add(valueListItem3);
		this.cboRound.Items.Add(valueListItem4);
		this.cboRound.Items.Add(valueListItem5);
		this.cboRound.Items.Add(valueListItem6);
		this.cboRound.Items.Add(valueListItem7);
		this.cboRound.Items.Add(valueListItem8);
		this.cboRound.Items.Add(valueListItem9);
		this.cboRound.Items.Add(valueListItem10);
		this.cboRound.Items.Add(valueListItem11);
		this.cboRound.Location = new System.Drawing.Point(91, 49);
		this.cboRound.Name = "cboRound";
		this.cboRound.Size = new System.Drawing.Size(157, 24);
		this.cboRound.TabIndex = 22;
		this.cboRound.Text = "[cboRound]";
		appearance10.ForeColor = System.Drawing.Color.Green;
		appearance10.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblRound.Appearance = appearance10;
		this.lblRound.Location = new System.Drawing.Point(12, 53);
		this.lblRound.Name = "lblRound";
		this.lblRound.Size = new System.Drawing.Size(76, 20);
		this.lblRound.TabIndex = 21;
		this.lblRound.Text = "取位原則:";
		this.txtQty.Location = new System.Drawing.Point(91, 20);
		this.txtQty.Name = "txtQty";
		this.txtQty.Size = new System.Drawing.Size(157, 25);
		this.txtQty.TabIndex = 20;
		this.txtQty.Text = "[txtQty]";
		this.txtQty.Validating += new System.ComponentModel.CancelEventHandler(txtQty_Validating);
		appearance11.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance11;
		this.ultraLabel5.Location = new System.Drawing.Point(12, 25);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(44, 20);
		this.ultraLabel5.TabIndex = 19;
		this.ultraLabel5.Text = "數量:";
		this.groupBox1.Controls.Add(this.optItemType);
		this.groupBox1.Location = new System.Drawing.Point(7, 4);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(465, 108);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "項目種類";
		appearance12.BackColorDisabled = System.Drawing.Color.FromArgb(237, 243, 254);
		this.optItemType.Appearance = appearance12;
		this.optItemType.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.optItemType.Dock = System.Windows.Forms.DockStyle.Fill;
		appearance13.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance13.BackColorDisabled = System.Drawing.Color.FromArgb(237, 243, 254);
		this.optItemType.ItemAppearance = appearance13;
		this.optItemType.ItemOrigin = new System.Drawing.Point(10, 0);
		valueListItem12.DataValue = "B";
		valueListItem12.DisplayText = "一般主項(由下層自動累算)";
		valueListItem13.DataValue = "L";
		valueListItem13.DisplayText = "單獨計價項目(直接輸入金額)";
		valueListItem14.DataValue = "F";
		valueListItem14.DisplayText = "公式計價項目(設定公式)";
		valueListItem15.DataValue = "S";
		valueListItem15.DisplayText = "分段計價項目(分段設定公式)";
		valueListItem16.DataValue = "Z";
		valueListItem16.DisplayText = "計項(總計、小計、合計等)";
		valueListItem17.DataValue = "U";
		valueListItem17.DisplayText = "使用者自訂公式";
		this.optItemType.Items.Add(valueListItem12);
		this.optItemType.Items.Add(valueListItem13);
		this.optItemType.Items.Add(valueListItem14);
		this.optItemType.Items.Add(valueListItem15);
		this.optItemType.Items.Add(valueListItem16);
		this.optItemType.Items.Add(valueListItem17);
		this.optItemType.ItemSpacingVertical = 5;
		this.optItemType.Location = new System.Drawing.Point(3, 21);
		this.optItemType.Name = "optItemType";
		this.optItemType.Size = new System.Drawing.Size(459, 84);
		this.optItemType.TabIndex = 0;
		this.optItemType.ValueChanged += new System.EventHandler(optItemType_ValueChanged);
		this.panel3.Controls.Add(this.groupBox3);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 212);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(747, 357);
		this.panel3.TabIndex = 2;
		this.groupBox3.Controls.Add(this.PNL_CHILD);
		this.groupBox3.Location = new System.Drawing.Point(7, 3);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(733, 261);
		this.groupBox3.TabIndex = 0;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "單價";
		this.PNL_CHILD.Dock = System.Windows.Forms.DockStyle.Fill;
		this.PNL_CHILD.Location = new System.Drawing.Point(3, 21);
		this.PNL_CHILD.Name = "PNL_CHILD";
		this.PNL_CHILD.Size = new System.Drawing.Size(727, 237);
		this.PNL_CHILD.TabIndex = 0;
		this.panel4.Controls.Add(this.BtnOK);
		this.panel4.Controls.Add(this.BtnCancel);
		this.panel4.Controls.Add(this.txtMemo);
		this.panel4.Controls.Add(this.ultraLabel7);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(0, 481);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(747, 88);
		this.panel4.TabIndex = 3;
		this.BtnOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance14.Image = resources.GetObject("appearance14.Image");
		this.BtnOK.Appearance = appearance14;
		this.BtnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		appearance15.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance15.BackColor2 = System.Drawing.Color.White;
		appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.BtnOK.HotTrackAppearance = appearance15;
		this.BtnOK.HotTracking = true;
		this.BtnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnOK.Location = new System.Drawing.Point(548, 52);
		this.BtnOK.Name = "BtnOK";
		this.BtnOK.ShowFocusRect = false;
		this.BtnOK.Size = new System.Drawing.Size(94, 31);
		this.BtnOK.SupportThemes = false;
		this.BtnOK.TabIndex = 3;
		this.BtnOK.Text = "確定";
		this.BtnOK.Click += new System.EventHandler(BtnOK_Click);
		this.BtnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance16.Image = resources.GetObject("appearance16.Image");
		this.BtnCancel.Appearance = appearance16;
		this.BtnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		appearance17.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance17.BackColor2 = System.Drawing.Color.White;
		appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.BtnCancel.HotTrackAppearance = appearance17;
		this.BtnCancel.HotTracking = true;
		this.BtnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnCancel.Location = new System.Drawing.Point(644, 52);
		this.BtnCancel.Name = "BtnCancel";
		this.BtnCancel.ShowFocusRect = false;
		this.BtnCancel.Size = new System.Drawing.Size(94, 31);
		this.BtnCancel.SupportThemes = false;
		this.BtnCancel.TabIndex = 2;
		this.BtnCancel.Text = "取消";
		this.txtMemo.Location = new System.Drawing.Point(60, 9);
		this.txtMemo.Multiline = true;
		this.txtMemo.Name = "txtMemo";
		this.txtMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.txtMemo.Size = new System.Drawing.Size(676, 40);
		this.txtMemo.TabIndex = 1;
		this.txtMemo.Text = "[txtMemo]";
		this.ultraLabel7.Location = new System.Drawing.Point(12, 8);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(64, 16);
		this.ultraLabel7.TabIndex = 0;
		this.ultraLabel7.Text = "備註:";
		base.AcceptButton = this.BtnOK;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.BtnCancel;
		base.ClientSize = new System.Drawing.Size(747, 569);
		base.Controls.Add(this.panel4);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.Name = "FormBudgetEditMain";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "主項大類";
		base.Load += new System.EventHandler(FormBudgetEditMain_Load);
		base.Activated += new System.EventHandler(FormBudgetEditMain_Activated);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormBudgetEditMain_FormClosing);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormBudgetEditMain_KeyDown);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtEName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboEUnit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCUnit).EndInit();
		this.panel2.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cboShareItem).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboRound).EndInit();
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.optItemType).EndInit();
		this.panel3.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.panel4.PerformLayout();
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

	public FormBudgetEditMain()
	{
		InitializeComponent();
		F_FORM_STATUS = FormStatus.Active;
	}

	private void FormBudgetEditMain_Load(object sender, EventArgs e)
	{
		LoadUasualString();
		SetDecimal();
		GetItem_FromDB(F_Item_sNo);
		GetUnit_DataSet();
		FillColumns();
		SetItemType(F_ItemType);
		if (SysConfig.SysComsEnable)
		{
			lblPccesCode.Visible = true;
			txtPccesCode.Visible = true;
		}
		if (F_ActionName == PccesFormAction.BID)
		{
			txtItemNo.Enabled = false;
			textBox4.Enabled = false;
			txtCName.Enabled = false;
			txtEName.Enabled = false;
			cboCUnit.Enabled = false;
			cboEUnit.Enabled = false;
			txtQty.Enabled = false;
			txtMemo.Enabled = false;
		}
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		DBCLS._Issue = _Issue.ToString();
		DBCLS.ItemA_Lock(F_Item_sNo.ToString(), F_ProjectCode, CommonMethods.GetActionNameString(F_ActionName));
		if (F_PrintNo == "99999999999999999999999999999999")
		{
			txtItemNo.Enabled = false;
			textBox4.Enabled = false;
			cboCUnit.Enabled = false;
			cboEUnit.Enabled = false;
			txtQty.Enabled = false;
			cboRound.Enabled = false;
			txtMemo.Enabled = false;
			optItemType.Enabled = false;
		}
		CB_PrintToAnalysis.Checked = F_PrintToAnalysis == "1";
		CorrectRatio();
		LoadingScreen();
		Frm.OnUserRequest += UserReq;
		if (F_IsCostStructure)
		{
			txtCName.Enabled = false;
			txtEName.Enabled = false;
		}
	}

	private void SetDecimal()
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("讀取專案預算小數位數設定--" + F_ProjectCode + "");
		Archnowledge.Pcces.BUDClass.PubDecimal dbDecimal = new Archnowledge.Pcces.BUDClass.PubDecimal(aArr);
		DataTable DT1 = dbDecimal.ListItem("", F_ProjectCode);
		if (DT1.Rows.Count > 0)
		{
			decItemQty = Convert.ToInt32(DT1.Rows[0]["itemQty"]);
			decItemCost = Convert.ToInt32(DT1.Rows[0]["itemCost"]);
			decItemAmt = Convert.ToInt32(DT1.Rows[0]["itemAmt"]);
			decAnalQty = Convert.ToInt32(DT1.Rows[0]["analysisQty"]);
			decAnalCost = Convert.ToInt32(DT1.Rows[0]["analysisCost"]);
			decAnalAmt = Convert.ToInt32(DT1.Rows[0]["analysisAmt"]);
		}
		else
		{
			decItemQty = 3;
			decItemCost = 2;
			decItemAmt = 0;
			decAnalQty = 3;
			decAnalCost = 2;
			decAnalAmt = 2;
		}
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("EditMain", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("EditMain", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("EditMain", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("EditMain", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("EditMain", "PK_Height"));
		if (iLoc_X > 0 && iLoc_Y > 0)
		{
			base.Location = new Point(iLoc_X, iLoc_Y);
		}
		if (iSiz_W > 0)
		{
			base.Width = iSiz_W;
		}
		if (iSiz_H > 0)
		{
			base.Height = iSiz_H;
		}
	}

	private void CorrectRatio()
	{
		double ratio = CommonMethods.GetWindowRatio(base.Handle);
		if (ratio != 1.0)
		{
			optItemType.Font = new Font(optItemType.Font.Name, (float)((double)optItemType.Font.Size * ratio));
		}
	}

	private void LoadUasualString()
	{
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = "PccAdmin";
		DataTable DT_cName = DBCLS.GetUserDefine("Select * from UserDefind Where kind='cName' Order By sno Desc ");
		for (int i = 0; i < DT_cName.Rows.Count; i++)
		{
			txtCName.Items.Add(DT_cName.Rows[i]["cString"].ToString(), DT_cName.Rows[i]["cString"].ToString());
		}
		DataTable DT_eName = DBCLS.GetUserDefine("Select * from UserDefind Where kind='eName' Order By sno Desc ");
		for (int i = 0; i < DT_eName.Rows.Count; i++)
		{
			txtEName.Items.Add(DT_eName.Rows[i]["cString"].ToString(), DT_eName.Rows[i]["cString"].ToString());
		}
	}

	private void optItemType_ValueChanged(object sender, EventArgs e)
	{
		Reload_ChildForm(CommonMethods.GetBDGT_ItemType(optItemType.Value.ToString()));
		if (optItemType.CheckedIndex == 0)
		{
			if (F_IsCanPrintToAnalysis)
			{
				CB_PrintToAnalysis.Visible = true;
			}
			else
			{
				CB_PrintToAnalysis.Visible = false;
			}
		}
		else
		{
			CB_PrintToAnalysis.Visible = false;
		}
		if (optItemType.CheckedIndex == 2 && F_PrintToAnalysis == "1")
		{
			lblPccesCode.Visible = true;
			txtPccesCode.Visible = true;
		}
	}

	private void FillColumns()
	{
		string sItemKind = "";
		if (DT_BDGT.Rows.Count > 0)
		{
			F_PrintNo = DT_BDGT.Rows[0]["printNo"].ToString().Trim();
			F_Cost = PubTools.Str2Decimal(DT_BDGT.Rows[0]["cost"]);
			F_Rate = PubTools.Str2Decimal(DT_BDGT.Rows[0]["rate"]);
			if (F_ActionName == PccesFormAction.SubChange)
			{
				F_Cost = PubTools.Str2Decimal(DT_BDGT.Rows[0]["ChgCost"]);
			}
			txtItemNo.Text = DT_BDGT.Rows[0]["ItemNo"].ToString().Trim();
			txtCName.Text = DT_BDGT.Rows[0]["cName"].ToString().Trim();
			txtEName.Text = DT_BDGT.Rows[0]["eName"].ToString().Trim();
			txtMemo.Text = DT_BDGT.Rows[0]["memo"].ToString().Trim();
			txtPccesCode.Text = DT_BDGT.Rows[0]["pccesCode"].ToString().Trim();
			txtQty.Text = string.Format("{0:N2}", DT_BDGT.Rows[0]["qty"]);
			if (F_ActionName == PccesFormAction.SubChange)
			{
				txtQty.Text = string.Format("{0:N2}", DT_BDGT.Rows[0]["ChgQty"]);
			}
			cboCUnit.Text = DT_BDGT.Rows[0]["unitName"].ToString().Trim();
			for (int i = 0; i < cboCUnit.Rows.Count; i++)
			{
				if (cboCUnit.Rows[i].Cells[0].Text.Trim() == DT_BDGT.Rows[0]["unitName"].ToString().Trim())
				{
					cboCUnit.SelectedRow = cboCUnit.Rows[i];
					cboCUnit.Text = DT_BDGT.Rows[0]["unitName"].ToString().Trim();
					break;
				}
			}
			cboEUnit.Text = DT_BDGT.Rows[0]["eUnit"].ToString().Trim();
			for (int i = 0; i < cboEUnit.Rows.Count; i++)
			{
				if (cboEUnit.Rows[i].Cells[0].Text.Trim() == DT_BDGT.Rows[0]["eUnit"].ToString().Trim())
				{
					cboEUnit.SelectedRow = cboEUnit.Rows[i];
					cboEUnit.Text = DT_BDGT.Rows[0]["eUnit"].ToString().Trim();
					break;
				}
			}
			if (DT_BDGT.Rows[0]["kind"].ToString().Trim() == "B" && DT_BDGT.Rows[0]["setDecimal"] == DBNull.Value)
			{
				DT_BDGT.Rows[0]["setDecimal"] = decItemAmt;
			}
			for (int i = 0; i < cboRound.Items.Count; i++)
			{
				if (cboRound.Items[i].DataValue.ToString() == DT_BDGT.Rows[0]["setDecimal"].ToString())
				{
					cboRound.SelectedIndex = i;
					break;
				}
			}
			sItemKind = DT_BDGT.Rows[0]["kind"].ToString().Trim();
			SetItemType(CommonMethods.GetBDGT_ItemType(sItemKind));
			cboShareItem.Clear();
			cboShareItem.Items.Add("-1", "【未設定】");
			if (F_ShareItems.Count > 0)
			{
				for (int i = 0; i < F_ShareItems.Count; i++)
				{
					string[] sShrItm = F_ShareItems[i].ToString().Split('|');
					cboShareItem.Items.Add(sShrItm[0], sShrItm[1]);
				}
			}
			cboShareItem.SelectedIndex = 0;
			if (F_ShareItems.Count > 0 && F_ShareItemSno != "")
			{
				for (int i = 0; i < cboShareItem.Items.Count; i++)
				{
					if (cboShareItem.Items[i].DataValue.ToString().Trim() == F_ShareItemSno)
					{
						cboShareItem.SelectedIndex = i;
						break;
					}
				}
			}
			else if (F_ShareItems.Count == 0 && F_ShareItemSno != "")
			{
				UpReSetDecimal(DT_BDGT.Rows[0]["sNo"].ToString().Trim());
			}
		}
		ControlsChange(sItemKind);
	}

	private void UpReSetDecimal(string sNo)
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("判斷是否要重新總計--" + F_ProjectCode);
		ItemA dbItemA = new ItemA(aArr);
		dbItemA.ps_projectCode = F_ProjectCode;
		dbItemA.ps_sNo = sNo;
		dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbItemA.ps_setDecimal = "0";
		dbItemA.ps_Issue = F_Issue.ToString();
		dbItemA.UpdItem();
		aArr = null;
		dbItemA = null;
	}

	private void ControlsChange(string sItemKind)
	{
		if (sItemKind == "B")
		{
			lblRound.Visible = true;
			cboRound.Visible = true;
			lblShareItem.Visible = true;
			cboShareItem.Visible = true;
		}
		else
		{
			lblRound.Visible = false;
			cboRound.Visible = false;
			lblShareItem.Visible = false;
			cboShareItem.Visible = false;
		}
		if (sItemKind == "B" && F_ChildCount > 0)
		{
			optItemType.Enabled = false;
		}
		if (F_Istemplate)
		{
			lblRound.Enabled = false;
			cboRound.Enabled = false;
			lblShareItem.Enabled = false;
			cboShareItem.Enabled = false;
		}
		if (!F_AllowRestrictEdit)
		{
			return;
		}
		optItemType.Enabled = false;
		txtItemNo.Enabled = false;
		txtCName.Enabled = false;
		txtEName.Enabled = false;
		cboEUnit.Enabled = false;
		cboCUnit.Enabled = false;
		if (sItemKind == "L")
		{
			if (!(cboCUnit.Value.ToString() == "式"))
			{
				foreach (Control CTRL in PNL_CHILD.Controls)
				{
					if (CTRL is L_Form)
					{
						(CTRL as L_Form).SetCostInputEnabled(Enable: false);
					}
				}
				return;
			}
			txtQty.Enabled = false;
		}
		else if (sItemKind == "B")
		{
			txtQty.Enabled = false;
			cboRound.Enabled = false;
			cboShareItem.Enabled = false;
		}
	}

	private void GetItem_FromDB(int sNO)
	{
		string IPStr = CommonMethods.GetIPAddress();
		if (sNO != -1)
		{
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(F_UserID);
			aArr.Add("預算書讀取指定資料--" + F_ProjectCode + "(" + IPStr + ")");
			ItemA dbItemA = new ItemA(aArr);
			dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			dbItemA.ps_projectCode = F_ProjectCode;
			dbItemA.ps_Issue = F_Issue.ToString();
			DT_BDGT = dbItemA.ListItem(" sNo = " + sNO, F_ProjectCode);
		}
	}

	private void SetItemType(BDGT_ITEM_TYPE enum_ItemType)
	{
		switch (ItemType)
		{
		case BDGT_ITEM_TYPE.B:
			optItemType.CheckedIndex = 0;
			break;
		case BDGT_ITEM_TYPE.F:
			optItemType.CheckedIndex = 2;
			break;
		case BDGT_ITEM_TYPE.L:
			optItemType.CheckedIndex = 1;
			break;
		case BDGT_ITEM_TYPE.S:
			optItemType.CheckedIndex = 3;
			break;
		case BDGT_ITEM_TYPE.U:
			optItemType.CheckedIndex = 5;
			break;
		case BDGT_ITEM_TYPE.Z:
			optItemType.CheckedIndex = 4;
			break;
		}
	}

	private void Reload_ChildForm(BDGT_ITEM_TYPE enum_ItemType)
	{
		if (PNL_CHILD.Controls.Count > 0)
		{
			PNL_CHILD.Controls[0].Dispose();
		}
		PNL_CHILD.Controls.Clear();
		switch (enum_ItemType)
		{
		case BDGT_ITEM_TYPE.B:
		{
			B_Form Form6 = new B_Form();
			Form6._ActionName = F_ActionName;
			PNL_CHILD.Controls.Add(Form6);
			Form6.BringToFront();
			break;
		}
		case BDGT_ITEM_TYPE.F:
		{
			F_Form Form5 = new F_Form();
			BudProject theProject = null;
			theProject = new BudProject("Pcces");
			decimal shareVDF1 = 0m;
			int shareVDF1sNo = 0;
			theProject.GetShareVDF1(F_ProjectCode, out shareVDF1, out shareVDF1sNo);
			Form5._UserID = F_UserID;
			Form5._ActionName = F_ActionName;
			Form5._Issue = F_Issue;
			if (F_Item_sNo.ToString().CompareTo(shareVDF1sNo.ToString()) == 0)
			{
				Form5._VDF1 = shareVDF1;
				Form5.updateVisibleVDF1(toShow: true);
			}
			else
			{
				Form5._VDF1 = 0m;
				Form5.updateVisibleVDF1(toShow: false);
			}
			PNL_CHILD.Controls.Add(Form5);
			Form5.BringToFront();
			break;
		}
		case BDGT_ITEM_TYPE.L:
		{
			L_Form Form4 = new L_Form();
			Form4._ActionName = F_ActionName;
			Form4._Issue = F_Issue;
			PNL_CHILD.Controls.Add(Form4);
			Form4.BringToFront();
			break;
		}
		case BDGT_ITEM_TYPE.S:
		{
			S_Form Form3 = new S_Form();
			Form3._ActionName = F_ActionName;
			Form3._UserID = F_UserID;
			Form3._Issue = F_Issue;
			PNL_CHILD.Controls.Add(Form3);
			Form3.BringToFront();
			break;
		}
		case BDGT_ITEM_TYPE.U:
		{
			U_Form Form2 = new U_Form();
			Form2._ActionName = F_ActionName;
			Form2._UserID = F_UserID;
			Form2._Issue = F_Issue;
			PNL_CHILD.Controls.Add(Form2);
			Form2.BringToFront();
			break;
		}
		case BDGT_ITEM_TYPE.Z:
		{
			Z_Form Form1 = new Z_Form();
			Form1._ActionName = F_ActionName;
			Form1._UserID = F_UserID;
			Form1._Issue = F_Issue;
			PNL_CHILD.Controls.Add(Form1);
			Form1.BringToFront();
			break;
		}
		}
		ControlsChange(optItemType.Value.ToString());
	}

	private void GetUnit_DataSet()
	{
		DataSet DS1 = new DataSet();
		DBClass DBClass1 = new DBClass();
		DBClass1._FS_UserID = "PccAdmin";
		DT_Temp = DBClass1.GetUserDefine("Select cString as 中文單位 from UserDefind Where kind='cUnit' Order By IsNull(Times,0) Desc");
		DataRow DR = DT_Temp.NewRow();
		DR["中文單位"] = "";
		DT_Temp.Rows.Add(DR);
		DT_Temp.TableName = "cUnit";
		DS1.Tables.Add(DT_Temp.Copy());
		DT_Temp = DBClass1.GetUserDefine("Select cString as Unit from UserDefind Where kind='eUnit' Order By IsNull(Times,0) Desc");
		DR = DT_Temp.NewRow();
		DR["Unit"] = "";
		DT_Temp.Rows.Add(DR);
		DT_Temp.TableName = "eUnit";
		DS1.Tables.Add(DT_Temp.Copy());
		cboCUnit.DataSource = DS1;
		cboCUnit.DataMember = "cUnit";
		cboCUnit.DataBind();
		cboEUnit.DataSource = DS1;
		cboEUnit.DataMember = "eUnit";
		cboEUnit.DataBind();
	}

	private void FormBudgetEditMain_Activated(object sender, EventArgs e)
	{
		if (F_FORM_STATUS == FormStatus.Active)
		{
			F_FORM_STATUS = FormStatus.Normal;
		}
	}

	private void BtnOK_Click(object sender, EventArgs e)
	{
		if (cboRound.SelectedIndex == -1)
		{
			cboRound.SelectedIndex = 0;
		}
		BtnOK.Focus();
		if (PubTools.Str2Int(cboRound.SelectedItem.DataValue) < 0 && cboShareItem.SelectedIndex <= 0)
		{
			if (cboShareItem.Items.Count > 1)
			{
				MessageBox.Show(this, "請先設定攤提項目!", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBox.Show(this, "此主項大類無法設個位數以上的取位。\n因子層項目中，並沒有可攤提的項目(直接輸入項)!", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return;
		}
		if (optItemType.CheckedIndex == 1 && cboCUnit.Enabled && ArchConvert.Obj2String(cboCUnit.Text) == string.Empty)
		{
			MessageBox.Show("獨立計價項需選擇或輸入單位名稱");
			cboCUnit.Focus();
			return;
		}
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("存檔(編輯完該項,做存檔的動作)" + F_ProjectCode + "(" + IPStr + ")");
		ItemA dbItemA = new ItemA(aArr);
		dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbItemA.ps_projectCode = F_ProjectCode;
		dbItemA.ps_itemNo = txtItemNo.Text.Trim();
		dbItemA.ps_sNo = F_Item_sNo.ToString();
		dbItemA.ps_kind = GetItemKind_By_FormControlStatus();
		dbItemA.ps_cName = txtCName.Text.Trim();
		dbItemA.ps_amount = null;
		dbItemA.ps_bidCode = null;
		if (optItemType.CheckedIndex == 1)
		{
			if (F_ActionName == PccesFormAction.SubChange)
			{
				dbItemA.ps_ChgCost = Get_Real_Cost();
			}
			else
			{
				dbItemA.ps_cost = Get_Real_Cost();
			}
		}
		dbItemA.ps_dsctLock = null;
		dbItemA.ps_eName = txtEName.Text.Trim();
		dbItemA.ps_eUnit = cboEUnit.Text;
		dbItemA.ps_Formula = Get_Formula();
		if (dbItemA.ps_Formula.Trim() != "")
		{
			ExecResult ER = PubTools.ArchChkFormula2(dbItemA.ps_Formula);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show(this, "公式設定有誤，請重新設定! Error : " + ER.Message, CommonMethods.GetFormTypeTitle(FormType.Budget), MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
		}
		dbItemA.ps_levelNo = null;
		dbItemA.ps_memo = txtMemo.Text.Trim();
		if (F_ActionName == PccesFormAction.SubChange)
		{
			dbItemA.ps_ChgQty = txtQty.Text.Trim();
		}
		else
		{
			dbItemA.ps_qty = txtQty.Text.Trim();
		}
		dbItemA.ps_rate = Get_Real_Rate();
		dbItemA.ps_setDecimal = ((!cboRound.Visible) ? "4" : cboRound.SelectedItem.DataValue.ToString());
		if (dbItemA.ps_kind == "L" || dbItemA.ps_kind == "F")
		{
			dbItemA.ps_share = null;
		}
		else
		{
			dbItemA.ps_share = "DBNull";
		}
		dbItemA.ps_unitName = cboCUnit.Text;
		dbItemA.ps_printNo = F_PrintNo;
		dbItemA.ps_PrintToAnalysis = ((GetItemKind_By_FormControlStatus() == "B" && CB_PrintToAnalysis.Checked) ? "1" : "0");
		dbItemA.ps_PccesCode = txtPccesCode.Text;
		dbItemA.ps_Issue = F_Issue.ToString();
		if (cboShareItem.SelectedIndex > 0)
		{
			dbItemA.ps_ShareSno = cboShareItem.Value.ToString();
		}
		else
		{
			dbItemA.ps_ShareSno = "";
		}
		if (GetItemKind_By_FormControlStatus().ToUpper() == "Z")
		{
			dbItemA.ps_itemNo = "";
		}
		dbItemA.UpdItem();
		if (GetItemKind_By_FormControlStatus().ToUpper() != "S")
		{
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = F_UserID;
			DBCLS.ExecuteCommand("Delete From " + dbItemA.ps_srckind + "ItemC Where ProjectCode='" + F_ProjectCode + "' And PrintNo='" + F_PrintNo + "' ");
		}
		AddNewCNameString();
		AddNewENameString();
		base.DialogResult = DialogResult.OK;
	}

	private string Get_Real_Cost()
	{
		string RetV = "0";
		int checkedIndex = optItemType.CheckedIndex;
		if (checkedIndex == 1)
		{
			foreach (Control CTRL in PNL_CHILD.Controls)
			{
				if (CTRL is L_Form)
				{
					RetV = (CTRL as L_Form)._txtCost;
				}
			}
		}
		return RetV;
	}

	private string Get_Real_Rate()
	{
		string RetV = "0";
		int checkedIndex = optItemType.CheckedIndex;
		if (checkedIndex == 2)
		{
			foreach (Control CTRL in PNL_CHILD.Controls)
			{
				if (CTRL is F_Form)
				{
					RetV = (CTRL as F_Form)._txtRate;
				}
			}
		}
		return RetV;
	}

	private string Get_Formula()
	{
		string RetV = "";
		int checkedIndex = optItemType.CheckedIndex;
		if (checkedIndex == 5)
		{
			foreach (Control CTRL in PNL_CHILD.Controls)
			{
				if (CTRL is U_Form)
				{
					RetV = (CTRL as U_Form)._txtFormula;
				}
			}
		}
		return RetV;
	}

	private string GetItemKind_By_FormControlStatus()
	{
		string RetV = "B";
		switch (optItemType.CheckedIndex)
		{
		case 0:
			RetV = "B";
			break;
		case 1:
			RetV = "L";
			break;
		case 2:
			RetV = "F";
			break;
		case 3:
			RetV = "S";
			break;
		case 4:
			RetV = "Z";
			break;
		case 5:
			RetV = "U";
			break;
		}
		return RetV;
	}

	private void FormBudgetEditMain_FormClosing(object sender, FormClosingEventArgs e)
	{
		Frm.Close();
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		DBCLS.ItemA_UnLock(F_Item_sNo.ToString(), F_ProjectCode, CommonMethods.GetActionNameString(F_ActionName));
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("EditMain", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("EditMain", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("EditMain", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("EditMain", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("EditMain", "WindowState", base.WindowState.ToString());
	}

	private void txtItemNo_Validating(object sender, CancelEventArgs e)
	{
		if (!CommonMethods.CheckValidString((sender as TextBox).Text))
		{
			e.Cancel = true;
		}
		if (!CommonMethods.IsStrByteLenValid(txtItemNo.Text, 30))
		{
			MessageBox.Show(this, "項次的長度不可超過 30 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtItemNo.Focus();
		}
		else if (!CommonMethods.IsStrByteLenValid(txtEName.Text, 200))
		{
			MessageBox.Show(this, "Description 的長度不可超過 200 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtEName.Focus();
		}
	}

	private void txtQty_Validating(object sender, CancelEventArgs e)
	{
		if (!CommonMethods.CheckValidString((sender as TextBox).Text))
		{
			e.Cancel = true;
		}
		double dQty = 0.0;
		try
		{
			dQty = Convert.ToDouble(txtQty.Text);
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetEditMain.cs" + ex.Message);
			MessageBox.Show(this, "輸入的數量有誤。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtQty.Focus();
		}
	}

	private void cboCUnit_Validating(object sender, CancelEventArgs e)
	{
		if (cboCUnit.Text != null)
		{
			if (!CommonMethods.CheckValidString(cboCUnit.Text))
			{
				e.Cancel = true;
			}
			if (!CommonMethods.IsStrByteLenValid(cboCUnit.Text, 10))
			{
				MessageBox.Show(this, "單位的長度不可超過 10 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				cboCUnit.Focus();
				return;
			}
		}
		if (cboEUnit.Text != null)
		{
			if (!CommonMethods.CheckValidString(cboEUnit.Text))
			{
				e.Cancel = true;
			}
			if (!CommonMethods.IsStrByteLenValid(cboEUnit.Text, 20))
			{
				MessageBox.Show(this, "Unit 的長度不可超過 20 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				cboEUnit.Focus();
			}
		}
	}

	private void txtCName_Validating(object sender, CancelEventArgs e)
	{
		if (txtCName.Text != null)
		{
			if (!CommonMethods.CheckValidString(txtCName.Text))
			{
				e.Cancel = true;
			}
			if (!CommonMethods.IsStrByteLenValid(txtCName.Text, 200))
			{
				MessageBox.Show(this, "項目及說明的長度不可超過 200 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtCName.Focus();
			}
		}
	}

	private void txtEName_Validating(object sender, CancelEventArgs e)
	{
		if (txtEName.Text != null)
		{
			if (!CommonMethods.CheckValidString(txtEName.Text))
			{
				e.Cancel = true;
			}
			if (!CommonMethods.IsStrByteLenValid(txtEName.Text, 200))
			{
				MessageBox.Show(this, "Description 的長度不可超過 200 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtEName.Focus();
			}
		}
	}

	private void txtCName_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtCName.Text != null)
		{
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = "PccAdmin";
			int iCount = PubTools.Str2Int(DBCLS.GetUserDefine_String("Select Count(*) as iCount From UserDefind Where Kind='cName' And cString ='" + txtCName.Text.Trim() + "' ", "iCount"));
			if (iCount <= 0)
			{
				ArrayList aArr = new ArrayList();
				aArr.Clear();
				aArr.Add(F_UserID);
				aArr.Add("(UserDefind_Show) 新增常用字串資料");
				string sKind = "cName";
				UserDefind UserCom = new UserDefind(aArr);
				UserCom.ps_sNo = (UserCom.GetMaxSno(sKind) + 1).ToString();
				UserCom.ps_Kind = sKind;
				UserCom.ps_cString = txtCName.Text.Trim();
				UserCom.InseItem();
			}
		}
	}

	private void AddNewCNameString()
	{
		if (txtCName.Text == null || txtCName.Text.Trim() == string.Empty)
		{
			return;
		}
		try
		{
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = "PccAdmin";
			int iCount = PubTools.Str2Int(DBCLS.GetUserDefine_String("Select Count(*) as iCount From UserDefind Where Kind='cName' And cString ='" + txtCName.Text.Trim() + "' ", "iCount"));
			if (iCount <= 0)
			{
				ArrayList aArr = new ArrayList();
				aArr.Clear();
				aArr.Add(F_UserID);
				aArr.Add("(UserDefind_Show) 新增常用字串資料");
				string sKind = "cName";
				UserDefind UserCom = new UserDefind(aArr);
				UserCom.ps_sNo = (UserCom.GetMaxSno(sKind) + 1).ToString();
				UserCom.ps_Kind = sKind;
				UserCom.ps_cString = txtCName.Text.Trim();
				UserCom.InseItem();
			}
		}
		catch (Exception ex)
		{
			Archnowledge.Pcces.CommonClass.DebugUtil.OutputDebugString("自動加入常用項目Error：" + ex.Message);
		}
	}

	private void txtEName_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtEName.Text != null)
		{
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = "PccAdmin";
			int iCount = PubTools.Str2Int(DBCLS.GetUserDefine_String("Select Count(*) as iCount From UserDefind Where Kind='eName' And cString ='" + txtEName.Text.Trim() + "' ", "iCount"));
			if (iCount <= 0)
			{
				ArrayList aArr = new ArrayList();
				aArr.Clear();
				aArr.Add(F_UserID);
				aArr.Add("(UserDefind_Show) 新增常用字串資料");
				string sKind = "eName";
				UserDefind UserCom = new UserDefind(aArr);
				UserCom.ps_sNo = (UserCom.GetMaxSno(sKind) + 1).ToString();
				UserCom.ps_Kind = sKind;
				UserCom.ps_cString = txtEName.Text.Trim();
				UserCom.InseItem();
			}
		}
	}

	private void AddNewENameString()
	{
		if (txtEName.Text == null || txtEName.Text.Trim() == string.Empty)
		{
			return;
		}
		try
		{
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = "PccAdmin";
			int iCount = PubTools.Str2Int(DBCLS.GetUserDefine_String("Select Count(*) as iCount From UserDefind Where Kind='eName' And cString ='" + txtEName.Text.Trim() + "' ", "iCount"));
			if (iCount <= 0)
			{
				ArrayList aArr = new ArrayList();
				aArr.Clear();
				aArr.Add(F_UserID);
				aArr.Add("(UserDefind_Show) 新增常用字串資料");
				string sKind = "eName";
				UserDefind UserCom = new UserDefind(aArr);
				UserCom.ps_sNo = (UserCom.GetMaxSno(sKind) + 1).ToString();
				UserCom.ps_Kind = sKind;
				UserCom.ps_cString = txtEName.Text.Trim();
				UserCom.InseItem();
			}
		}
		catch (Exception ex)
		{
			Archnowledge.Pcces.CommonClass.DebugUtil.OutputDebugString("自動加入常用項目Error：" + ex.Message);
		}
	}

	private void cboCUnit_AfterCloseUp(object sender, EventArgs e)
	{
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = "PccAdmin";
		DBCLS.ExecuteCommand("Update UserDefind Set Times = IsNull(Times,0) + 1 Where RTrim(Kind) = 'cUnit' And RTrim(cString) = '" + cboCUnit.Text.ToString() + "' ");
	}

	private void cboEUnit_AfterCloseUp(object sender, EventArgs e)
	{
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = "PccAdmin";
		DBCLS.ExecuteCommand("Update UserDefind Set Times = IsNull(Times,0) + 1 Where RTrim(Kind) = 'eUnit' And RTrim(cString) = '" + cboEUnit.Text.ToString() + "' ");
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
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetEditMain.cs" + ex.Message);
			Console.Write(ex.Message);
		}
	}

	private void FormBudgetEditMain_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.F1)
		{
			Frm.Show();
			Frm.BringToFront();
		}
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormBudgetEditMain");
		}
	}

	private void CB_PrintToAnalysis_Click(object sender, EventArgs e)
	{
		BudPCalsCustomVar budPCalsCustomVar = new BudPCalsCustomVar();
		DataSet dsBudPCalsCustomVar = budPCalsCustomVar.GetPCalsCustomVar(ProjectCode, 0);
		if (dsBudPCalsCustomVar.Tables[0].Rows.Count <= 0)
		{
			MessageBox.Show(this, "尚未建立任何『自訂變數項』不可勾選!!", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			CB_PrintToAnalysis.Checked = false;
		}
	}
}
