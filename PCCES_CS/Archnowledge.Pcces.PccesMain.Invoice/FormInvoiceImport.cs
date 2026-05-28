using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.CTRClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.SysMaintain;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.Invoice;

public class FormInvoiceImport : Form
{
	private Panel panel2;

	private GroupBox groupBox2;

	private UltraButton B_Btn_Cncl;

	private UltraButton B_Btn_Next;

	private Panel panel1;

	private Panel panel5;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel6;

	private Panel panel3;

	private UltraLabel ultraLabel1;

	private UltraButton ultraButton4;

	private GroupBox groupBox1;

	private Panel panel4;

	private Panel panel6;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel3;

	private Panel panel7;

	private GridBudget Grid1;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel2;

	private GridBudget gridBudget2;

	private OpenFileDialog openFileDialog1;

	private UltraTextEditor txtImpDirFile;

	private IContainer components;

	private string F_ProjectCode;

	private string F_UserID;

	private ArrayList F_IssueList;

	private DataSet lds_temp = new DataSet();

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

	public ArrayList _IssueList
	{
		get
		{
			return F_IssueList;
		}
		set
		{
			F_IssueList = value;
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Invoice.FormInvoiceImport));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		this.panel2 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.B_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.B_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.panel4 = new System.Windows.Forms.Panel();
		this.panel6 = new System.Windows.Forms.Panel();
		this.gridBudget2 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.panel7 = new System.Windows.Forms.Panel();
		this.Grid1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.ultraButton4 = new Infragistics.Win.Misc.UltraButton();
		this.txtImpDirFile = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.panel2.SuspendLayout();
		this.panel1.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.panel4.SuspendLayout();
		this.panel6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridBudget2).BeginInit();
		this.panel7.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Grid1).BeginInit();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtImpDirFile).BeginInit();
		this.panel5.SuspendLayout();
		base.SuspendLayout();
		this.panel2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel2.Controls.Add(this.groupBox2);
		this.panel2.Controls.Add(this.B_Btn_Cncl);
		this.panel2.Controls.Add(this.B_Btn_Next);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 509);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(792, 44);
		this.panel2.TabIndex = 14;
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(792, 8);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.B_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Cncl.Appearance = appearance1;
		this.B_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.B_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Cncl.Location = new System.Drawing.Point(696, 9);
		this.B_Btn_Cncl.Name = "B_Btn_Cncl";
		this.B_Btn_Cncl.ShowFocusRect = false;
		this.B_Btn_Cncl.ShowOutline = false;
		this.B_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Cncl.SupportThemes = false;
		this.B_Btn_Cncl.TabIndex = 2;
		this.B_Btn_Cncl.Text = "取消";
		this.B_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Next.Appearance = appearance2;
		this.B_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Next.Location = new System.Drawing.Point(606, 9);
		this.B_Btn_Next.Name = "B_Btn_Next";
		this.B_Btn_Next.ShowFocusRect = false;
		this.B_Btn_Next.ShowOutline = false;
		this.B_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Next.SupportThemes = false;
		this.B_Btn_Next.TabIndex = 1;
		this.B_Btn_Next.Text = "轉入";
		this.B_Btn_Next.Click += new System.EventHandler(B_Btn_Next_Click);
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.groupBox1);
		this.panel1.Controls.Add(this.panel3);
		this.panel1.Controls.Add(this.panel5);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(792, 509);
		this.panel1.TabIndex = 15;
		this.groupBox1.Controls.Add(this.panel4);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.groupBox1.Location = new System.Drawing.Point(0, 88);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(792, 421);
		this.groupBox1.TabIndex = 15;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "資料預覽";
		this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel4.Controls.Add(this.panel6);
		this.panel4.Controls.Add(this.panel7);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel4.Location = new System.Drawing.Point(3, 21);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(786, 397);
		this.panel4.TabIndex = 0;
		this.panel6.Controls.Add(this.gridBudget2);
		this.panel6.Controls.Add(this.ultraLabel4);
		this.panel6.Controls.Add(this.ultraLabel3);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel6.Location = new System.Drawing.Point(0, 90);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(784, 305);
		this.panel6.TabIndex = 3;
		this.gridBudget2._ExcelFileName = "";
		this.gridBudget2._ExcelSheeName = "";
		this.gridBudget2._IsOpenExcelAfterExport = false;
		this.gridBudget2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridBudget2.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.gridBudget2.ColumnInfo = "14,1,0,0,0,110,Columns:0{Width:14;Name:\"RowIndicator\";AllowEditing:False;DataType:System.Int32;TextAlign:RightTop;TextAlignFixed:GeneralTop;}\t1{Width:100;AllowSorting:False;Name:\"ItemNo\";Caption:\"項次\";AllowDragging:False;AllowEditing:False;DataType:System.String;TextAlign:LeftTop;TextAlignFixed:GeneralTop;}\t2{Width:200;Name:\"CName\";Caption:\"項目及說明\";AllowEditing:False;DataType:System.String;TextAlign:GeneralBottom;TextAlignFixed:GeneralTop;}\t3{Width:70;Name:\"UnitName\";Caption:\"單位\";AllowEditing:False;DataType:System.String;TextAlign:LeftTop;TextAlignFixed:GeneralTop;}\t4{Width:100;Name:\"Cost\";Caption:\"單價\";AllowEditing:False;DataType:System.Decimal;Format:\"###,###,###,##0.00\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t5{Width:100;Name:\"Qty\";Caption:\"原契約數量\";AllowEditing:False;DataType:System.Decimal;Format:\"###,###,###,##0.000\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t6{Name:\"Amount\";Caption:\"原契約複價\";DataType:System.Decimal;Format:\"###,###,###,##0\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t7{Width:100;Name:\"this_qty\";Caption:\"本期完成數量\";DataType:System.Decimal;Format:\"###,###,###,##0.000\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t8{Width:100;Name:\"this_amt\";Caption:\"本期完成金額\";DataType:System.Decimal;Format:\"###,###,###,##0\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t9{Width:100;Name:\"acc_qty\";Caption:\"累計完成數量\";AllowEditing:False;DataType:System.Decimal;Format:\"###,###,###,##0.000\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t10{Width:100;Name:\"acc_amt\";Caption:\"累計完成金額\";AllowEditing:False;DataType:System.Decimal;Format:\"###,###,###,##0\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t11{Width:100;Name:\"acc_prec\";Caption:\"累計進度\";DataType:System.Decimal;TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t12{Name:\"Kind\";Caption:\"類別\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t13{Name:\"PrintNo\";Caption:\"項次代碼\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t";
		this.gridBudget2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridBudget2.ExtendLastCol = true;
		this.gridBudget2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridBudget2.ForeColor = System.Drawing.Color.Black;
		this.gridBudget2.Location = new System.Drawing.Point(0, 30);
		this.gridBudget2.Name = "gridBudget2";
		this.gridBudget2.Rows.Count = 1;
		this.gridBudget2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.gridBudget2.ShowCursor = true;
		this.gridBudget2.ShowSort = false;
		this.gridBudget2.ShowToolTipOnNarrowColumn = true;
		this.gridBudget2.Size = new System.Drawing.Size(784, 275);
		this.gridBudget2.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11.25pt;BackColor:237, 243, 254;ForeColor:Black;TextAlign:GeneralTop;Border:Flat,1,Silver,Both;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;TextAlign:GeneralCenter;Format:\"###,###,###,##0\";}\tFocus{Font:細明體, 9.75pt;BackColor:White;Border:Double,1,96, 145, 234,Both;}\tSearch{Font:細明體, 9.75pt;BackColor:White;ForeColor:HighlightText;Border:Double,1,96, 145, 234,Both;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.gridBudget2.TabIndex = 20;
		this.gridBudget2.Tree.Column = 1;
		this.gridBudget2.Tree.LineColor = System.Drawing.Color.Gray;
		appearance3.ForeColor = System.Drawing.Color.White;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance3;
		this.ultraLabel4.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel4.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel4.Location = new System.Drawing.Point(10, 8);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(198, 19);
		this.ultraLabel4.TabIndex = 19;
		this.ultraLabel4.Text = "計價內容";
		this.ultraLabel3.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel3.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel3.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(784, 30);
		this.ultraLabel3.TabIndex = 18;
		this.panel7.Controls.Add(this.Grid1);
		this.panel7.Controls.Add(this.ultraLabel5);
		this.panel7.Controls.Add(this.ultraLabel2);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel7.Location = new System.Drawing.Point(0, 0);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(784, 90);
		this.panel7.TabIndex = 2;
		this.Grid1._ExcelFileName = "";
		this.Grid1._ExcelSheeName = "";
		this.Grid1._IsOpenExcelAfterExport = false;
		this.Grid1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.Rows;
		this.Grid1.AllowEditing = false;
		this.Grid1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.Grid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.Grid1.ColumnInfo = "15,1,0,0,0,110,Columns:0{Width:14;Name:\"RowIndicator\";AllowEditing:False;DataType:System.Int32;TextAlign:RightTop;TextAlignFixed:GeneralTop;}\t1{Width:38;AllowSorting:False;Name:\"Queue\";Caption:\"期別\";AllowDragging:False;DataType:System.Int32;TextAlign:RightCenter;TextAlignFixed:GeneralTop;}\t2{Width:90;Name:\"date_rece\";Caption:\"啟始日\";DataType:System.DateTime;Format:\"d\";TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t3{Width:90;Name:\"date_insp\";Caption:\"結束日\";DataType:System.DateTime;Format:\"d\";TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t4{Width:60;Name:\"This_Prec\";Caption:\"完工%\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t5{Name:\"AccTotal\";Caption:\"工程款\";DataType:System.Decimal;Format:\"###,###,###,##0.\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t6{Name:\"Advancepay\";Caption:\"預付款\";DataType:System.Decimal;Format:\"###,###,###,##0.\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t7{Name:\"Advance\";Caption:\"扣回預付款\";DataType:System.Decimal;Format:\"###,###,###,##0.\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t8{Name:\"Reserve\";Caption:\"保留款\";DataType:System.Decimal;Format:\"###,###,###,##0.\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t9{Name:\"Reservertn\";Caption:\"退回保留款\";DataType:System.Decimal;Format:\"###,###,###,##0.\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t10{Name:\"Material\";Caption:\"預付材料款\";DataType:System.Decimal;Format:\"###,###,###,##0.\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t11{Name:\"IndexMat\";Caption:\"物價指數調整款\";DataType:System.Decimal;Format:\"###,###,###,##0.\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t12{Name:\"Deduct\";Caption:\"其他應扣款\";DataType:System.Decimal;Format:\"###,###,###,##0.\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t13{Name:\"AccAdd\";Caption:\"其他應加款\";DataType:System.Decimal;Format:\"###,###,###,##0.\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t14{Name:\"Realpay\";Caption:\"實付款\";DataType:System.Decimal;Format:\"###,###,###,##0.\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t";
		this.Grid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Grid1.ExtendLastCol = true;
		this.Grid1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Grid1.ForeColor = System.Drawing.Color.Black;
		this.Grid1.Location = new System.Drawing.Point(0, 30);
		this.Grid1.Name = "Grid1";
		this.Grid1.Rows.Count = 1;
		this.Grid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.Grid1.ShowCursor = true;
		this.Grid1.ShowSort = false;
		this.Grid1.ShowToolTipOnNarrowColumn = true;
		this.Grid1.Size = new System.Drawing.Size(784, 60);
		this.Grid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11.25pt;BackColor:237, 243, 254;ForeColor:Black;TextAlign:GeneralTop;Border:Flat,1,Silver,Both;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;TextAlign:GeneralCenter;Format:\"###,###,###,##0\";}\tFocus{Font:細明體, 11.25pt;BackColor:102, 153, 255;TextAlign:GeneralCenter;Border:None,1,Black,Both;}\tSearch{Font:細明體, 9.75pt;BackColor:White;ForeColor:HighlightText;Border:Double,1,96, 145, 234,Both;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.Grid1.TabIndex = 19;
		this.Grid1.Tree.Column = 1;
		this.Grid1.Tree.LineColor = System.Drawing.Color.Gray;
		appearance4.ForeColor = System.Drawing.Color.White;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance4;
		this.ultraLabel5.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel5.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel5.Location = new System.Drawing.Point(10, 7);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(686, 19);
		this.ultraLabel5.TabIndex = 18;
		this.ultraLabel5.Text = "計價期別資訊";
		this.ultraLabel2.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel2.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel2.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(784, 30);
		this.ultraLabel2.TabIndex = 17;
		this.panel3.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.panel3.Controls.Add(this.ultraButton4);
		this.panel3.Controls.Add(this.txtImpDirFile);
		this.panel3.Controls.Add(this.ultraLabel1);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel3.Location = new System.Drawing.Point(0, 48);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(792, 40);
		this.panel3.TabIndex = 14;
		this.ultraButton4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance5.FontData.Name = "Arial";
		appearance5.FontData.SizeInPoints = 8f;
		this.ultraButton4.Appearance = appearance5;
		this.ultraButton4.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton4.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton4.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton4.Location = new System.Drawing.Point(734, 8);
		this.ultraButton4.Name = "ultraButton4";
		this.ultraButton4.ShowFocusRect = false;
		this.ultraButton4.ShowOutline = false;
		this.ultraButton4.Size = new System.Drawing.Size(48, 24);
		this.ultraButton4.SupportThemes = false;
		this.ultraButton4.TabIndex = 5;
		this.ultraButton4.Text = "瀏覽...";
		this.ultraButton4.Click += new System.EventHandler(ultraButton4_Click);
		this.txtImpDirFile.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance6.FontData.Name = "細明體";
		appearance6.FontData.SizeInPoints = 11f;
		this.txtImpDirFile.Appearance = appearance6;
		this.txtImpDirFile.Location = new System.Drawing.Point(101, 9);
		this.txtImpDirFile.Name = "txtImpDirFile";
		this.txtImpDirFile.Size = new System.Drawing.Size(634, 24);
		this.txtImpDirFile.TabIndex = 4;
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(16, 12);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(80, 20);
		this.ultraLabel1.TabIndex = 3;
		this.ultraLabel1.Text = "匯入檔案:";
		this.panel5.BackColor = System.Drawing.Color.White;
		this.panel5.Controls.Add(this.ultraLabel7);
		this.panel5.Controls.Add(this.ultraLabel6);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(792, 48);
		this.panel5.TabIndex = 13;
		appearance7.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance7;
		this.ultraLabel7.Location = new System.Drawing.Point(47, 30);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(408, 14);
		this.ultraLabel7.TabIndex = 3;
		this.ultraLabel7.Text = "請先挑選要匯入的檔案,預覽確認後，再按右下角【轉入】按鈕執行轉入動作";
		appearance8.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance8;
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(16, 8);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 2;
		this.ultraLabel6.Text = "匯入檔案挑選及預覽";
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.CancelButton = this.B_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(792, 553);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel2);
		base.KeyPreview = true;
		base.Name = "FormInvoiceImport";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "計價資料匯入";
		this.panel2.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.panel6.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridBudget2).EndInit();
		this.panel7.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Grid1).EndInit();
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtImpDirFile).EndInit();
		this.panel5.ResumeLayout(false);
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

	public FormInvoiceImport()
	{
		InitializeComponent();
		CellStyle CS1 = gridBudget2.Styles.Add("AnalysisColor");
		CellStyle CS9 = gridBudget2.Styles.Add("IsSharedColor");
		CellStyle CS10 = gridBudget2.Styles.Add("MainColor");
		CS1.ForeColor = Color.Red;
		CS10.ForeColor = Color.Blue;
		CS9.ForeColor = Color.Plum;
		HideCols(IsHide: true);
	}

	private void HideCols(bool IsHide)
	{
		if (IsHide)
		{
			gridBudget2.Cols["Kind"].Visible = false;
			gridBudget2.Cols["PrintNo"].Visible = false;
		}
	}

	private void ultraButton4_Click(object sender, EventArgs e)
	{
		string sFilter = "XML files (*.xml)|*.xml";
		openFileDialog1.Filter = sFilter;
		openFileDialog1.RestoreDirectory = true;
		if (openFileDialog1.ShowDialog() == DialogResult.OK)
		{
			txtImpDirFile.Text = openFileDialog1.FileName;
		}
		LoadDataFromFile(txtImpDirFile.Text);
	}

	private void LoadDataFromFile(string sFile)
	{
		try
		{
			ultraLabel5.Text = "計價期別資訊";
			Grid1.Rows.Count = 1;
			gridBudget2.Rows.Count = 1;
			DataTableCollection tablesCol = lds_temp.Tables;
			for (int i = tablesCol.Count - 1; i >= 0; i--)
			{
				tablesCol.Remove(tablesCol[i].TableName);
			}
			lds_temp.ReadXml(sFile);
			int i2 = lds_temp.Tables.IndexOf("SubMfq");
			int i3 = lds_temp.Tables.IndexOf("SubAcc");
			if (lds_temp.Tables.IndexOf("SubMfq") <= -1 || lds_temp.Tables.IndexOf("SubAcc") <= -1)
			{
				MessageBox.Show(this, "挑選的檔案格式不正確，請確認(1)。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			BindToGrid1(lds_temp.Tables["SubAcc"]);
			BindToGrid2(lds_temp.Tables["SubMfq"]);
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Invoice.FormInvoiceImport.cs" + ex.Message);
			MessageBox.Show(this, "挑選的檔案格式不正確，請確認(2)。\n\n" + ex.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void BindToGrid1(DataTable DT1)
	{
		Grid1.Rows.Count = DT1.Rows.Count + 1;
		ultraLabel5.Text = "計價期別資訊(專案代號:" + DT1.Rows[0]["project"].ToString().Trim() + ")";
		Grid1[1, "Queue"] = DT1.Rows[0]["queue"].ToString().Trim();
		Grid1[1, "date_Rece"] = DT1.Rows[0]["date_rece"];
		Grid1[1, "date_Insp"] = DT1.Rows[0]["date_insp"];
		Grid1[1, "This_Prec"] = string.Format("{0:N2}", DT1.Rows[0]["this_prec"]) + "%";
		Grid1[1, "AccTotal"] = DT1.Rows[0]["AccTotal"];
		Grid1[1, "Advancepay"] = DT1.Rows[0]["total_advancepay"];
		Grid1[1, "Advance"] = DT1.Rows[0]["total_advance"];
		Grid1[1, "Reserve"] = DT1.Rows[0]["total_Reserve"];
		Grid1[1, "Reservertn"] = DT1.Rows[0]["total_Reservertn"];
		Grid1[1, "Material"] = DT1.Rows[0]["total_Material"];
		Grid1[1, "IndexMat"] = DT1.Rows[0]["total_IndexMat"];
		Grid1[1, "Deduct"] = DT1.Rows[0]["total_Deduct"];
		Grid1[1, "AccAdd"] = DT1.Rows[0]["total_AccAdd"];
		Grid1[1, "Realpay"] = DT1.Rows[0]["total_Realpay"];
	}

	private void BindToGrid2(DataTable DT1)
	{
		gridBudget2.Rows.Count = DT1.Rows.Count + 1;
		string sKind = "";
		for (int i = 0; i < DT1.Rows.Count; i++)
		{
			sKind = ((DT1.Rows[i]["kind"].ToString().Length > 0) ? DT1.Rows[i]["kind"].ToString().ToUpper().Trim() : "");
			switch (sKind)
			{
			default:
				if (!(sKind == "U"))
				{
					break;
				}
				goto case "B";
			case "B":
			case "L":
			case "F":
			case "S":
			case "Z":
				gridBudget2.Rows[i + 1].Style = gridBudget2.Styles["MainColor"];
				break;
			}
			gridBudget2[i + 1, "ItemNo"] = DT1.Rows[i]["itemno"].ToString().Trim();
			gridBudget2[i + 1, "CName"] = DT1.Rows[i]["cname"].ToString().Trim();
			gridBudget2[i + 1, "UnitName"] = DT1.Rows[i]["itemunit"].ToString().Trim();
			gridBudget2[i + 1, "Cost"] = DT1.Rows[i]["itemcost"];
			gridBudget2[i + 1, "Qty"] = DT1.Rows[i]["itemqty"];
			gridBudget2[i + 1, "Amount"] = PubTools.Str2Double(DT1.Rows[i]["itemcost"]) * PubTools.Str2Double(DT1.Rows[i]["itemqty"]);
			gridBudget2[i + 1, "this_qty"] = DT1.Rows[i]["Quantity"];
			gridBudget2[i + 1, "this_amt"] = DT1.Rows[i]["tom_amt"];
			gridBudget2[i + 1, "acc_qty"] = DT1.Rows[i]["Acc_Qty"];
			gridBudget2[i + 1, "acc_amt"] = DT1.Rows[i]["Acc_Amt"];
			gridBudget2[i + 1, "acc_prec"] = DT1.Rows[i]["Acc_Prec"];
			gridBudget2[i + 1, "Kind"] = DT1.Rows[i]["Kind"];
			gridBudget2[i + 1, "PrintNo"] = DT1.Rows[i]["itemdes"].ToString().Trim();
			if (gridBudget2[i + 1, "Kind"] != null)
			{
				gridBudget2.Rows[i + 1].IsNode = true;
			}
			if (DT1.Rows[i]["itemdes"].ToString().Trim() == "".PadLeft(32, '9'))
			{
				gridBudget2.Rows[i + 1].Node.Level = 1;
			}
			else
			{
				gridBudget2.Rows[i + 1].Node.Level = Convert.ToInt32(DT1.Rows[i]["itemdes"].ToString().Trim().Length / 4);
			}
		}
	}

	private void B_Btn_Next_Click(object sender, EventArgs e)
	{
		bool IsOverwrite = true;
		if (lds_temp.Tables.IndexOf("SubMfq") > -1 && lds_temp.Tables.IndexOf("SubAcc") > -1)
		{
			int theMaxNo = 0;
			if (F_IssueList.Count > 0)
			{
				theMaxNo = PubTools.Str2Int(F_IssueList[F_IssueList.Count - 1]);
			}
			int InputNo = PubTools.Str2Int(lds_temp.Tables["SubAcc"].Rows[0]["Queue"]);
			if (theMaxNo <= InputNo)
			{
				if (theMaxNo == InputNo)
				{
					string sMessage1 = "請注意：系統已存在要轉入的當期估驗資料，如果選擇【確定轉入】將直接覆蓋系統中的資料，並且無法復原!";
					if (MessageBox.Show(this, sMessage1 + "\n\n確定轉入並覆現有資料?", "訊問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
					{
						IsOverwrite = false;
					}
				}
			}
			else
			{
				string sMessage2 = "第 " + InputNo + " 期計價資料已鎖住，無再轉入這期的資料!";
				MessageBox.Show(this, sMessage2, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			if (IsOverwrite)
			{
				FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
				FM_INFO.TopMost = true;
				FM_INFO._InfoString = "計價資料轉入中，請稍候! ";
				FM_INFO.Owner = this;
				FM_INFO.Show();
				FM_INFO.BringToFront();
				Application.DoEvents();
				Cursor = Cursors.WaitCursor;
				DataTable AccDT = lds_temp.Tables["SubAcc"].Copy();
				DataTable MfqDT = lds_temp.Tables["SubMfq"].Copy();
				ArrayList tmp_AL1 = new ArrayList();
				tmp_AL1 = new ArrayList();
				tmp_AL1.Add(F_UserID);
				tmp_AL1.Add("(subacc) 估驗計價轉入");
				sub_acc AccCom = new sub_acc(tmp_AL1);
				AccCom.ps_prjcode = F_ProjectCode;
				int tmp = AccCom.InputXML(AccDT, MfqDT);
				Application.DoEvents();
				FM_INFO.Close();
				FM_INFO.Dispose();
				if (tmp == -2)
				{
					string sMessage3 = "要轉入的資料不正確，請檢查！";
					MessageBox.Show(this, sMessage3, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				Cursor = Cursors.Default;
				string sMessage4 = "轉入完成！";
				MessageBox.Show(this, sMessage4, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				base.DialogResult = DialogResult.OK;
			}
		}
		else
		{
			MessageBox.Show(this, "轉入的檔案格式不正確!(3)!", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}
}
