using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.CTRClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.Invoice;

public class FormInvoiceSummary : Form
{
	private IContainer components;

	private Panel panel16;

	private GroupBox groupBox6;

	private UltraButton D_Btn_Next;

	private Panel panel1;

	private Panel panel2;

	private GridBudget Grid1;

	private string F_ProjectCode;

	private string F_SubProjectCode;

	private string F_UserID;

	private DataTable ldt_AccList;

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

	public string _SubProjectCode
	{
		get
		{
			return F_SubProjectCode;
		}
		set
		{
			F_SubProjectCode = value;
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

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Invoice.FormInvoiceSummary));
		this.panel16 = new System.Windows.Forms.Panel();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.D_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.Grid1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel16.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Grid1).BeginInit();
		base.SuspendLayout();
		this.panel16.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel16.Controls.Add(this.groupBox6);
		this.panel16.Controls.Add(this.D_Btn_Next);
		this.panel16.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel16.Location = new System.Drawing.Point(0, 393);
		this.panel16.Name = "panel16";
		this.panel16.Size = new System.Drawing.Size(784, 44);
		this.panel16.TabIndex = 21;
		this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox6.Location = new System.Drawing.Point(0, 0);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(784, 8);
		this.groupBox6.TabIndex = 4;
		this.groupBox6.TabStop = false;
		this.D_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Next.Appearance = appearance1;
		this.D_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Next.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.D_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Next.Location = new System.Drawing.Point(690, 9);
		this.D_Btn_Next.Name = "D_Btn_Next";
		this.D_Btn_Next.ShowFocusRect = false;
		this.D_Btn_Next.ShowOutline = false;
		this.D_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Next.SupportThemes = false;
		this.D_Btn_Next.TabIndex = 1;
		this.D_Btn_Next.Text = "確定";
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.Grid1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 35);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(784, 358);
		this.panel1.TabIndex = 22;
		this.Grid1._ExcelFileName = "";
		this.Grid1._ExcelSheeName = "";
		this.Grid1._IsOpenExcelAfterExport = false;
		this.Grid1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.Rows;
		this.Grid1.AllowEditing = false;
		this.Grid1.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictAll;
		this.Grid1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.Grid1.BackColor = System.Drawing.Color.FromArgb(255, 224, 192);
		this.Grid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.Grid1.ColumnInfo = "16,1,0,0,0,110,Columns:0{Width:14;Name:\"RowIndicator\";AllowEditing:False;DataType:System.Int32;TextAlign:RightTop;TextAlignFixed:GeneralTop;}\t1{Width:38;AllowSorting:False;Name:\"Queue\";Caption:\"期別\";AllowDragging:False;DataType:System.Int32;TextAlign:CenterCenter;TextAlignFixed:GeneralTop;}\t2{Width:90;Name:\"date_rece\";Caption:\"估驗啟始日\";DataType:System.DateTime;Format:\"d\";TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t3{Width:90;Name:\"date_insp\";Caption:\"估驗結束日\";DataType:System.DateTime;Format:\"d\";TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t4{Width:88;Name:\"this_prec\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;ImageAlign:CenterCenter;}\t5{Width:68;Name:\"total_prec\";Caption:\"完工%\";DataType:System.Decimal;TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t6{Caption:\"工程款\";DataType:System.Decimal;Format:\"###,###,###,##0.\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t7{Caption:\"預付款\";DataType:System.Decimal;Format:\"###,###,###,##0.\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t8{Caption:\"扣回預付款\";DataType:System.Decimal;Format:\"###,###,###,##0.\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t9{Caption:\"保留款\";DataType:System.Decimal;Format:\"###,###,###,##0.\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t10{Caption:\"退回保留款\";DataType:System.Decimal;Format:\"###,###,###,##0.\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t11{Caption:\"預付材料款\";DataType:System.Decimal;Format:\"###,###,###,##0.\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t12{Width:118;Caption:\"物價指數調整款\";DataType:System.Decimal;Format:\"###,###,###,##0.\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t13{Caption:\"其他應扣款\";DataType:System.Decimal;Format:\"###,###,###,##0.\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t14{Caption:\"其他應增款\";DataType:System.Decimal;Format:\"###,###,###,##0.\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t15{Caption:\"實付款\";DataType:System.Decimal;Format:\"###,###,###,##0.\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t";
		this.Grid1.ExtendLastCol = true;
		this.Grid1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Grid1.ForeColor = System.Drawing.Color.Black;
		this.Grid1.Location = new System.Drawing.Point(8, 8);
		this.Grid1.Name = "Grid1";
		this.Grid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.Grid1.ShowCursor = true;
		this.Grid1.ShowSort = false;
		this.Grid1.ShowToolTipOnNarrowColumn = true;
		this.Grid1.Size = new System.Drawing.Size(768, 344);
		this.Grid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11.25pt;BackColor:255, 224, 192;ForeColor:Black;TextAlign:GeneralTop;Border:Flat,1,Silver,Both;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;TextAlign:GeneralCenter;Format:\"###,###,###,##0\";}\tFocus{Font:細明體, 11.25pt;BackColor:102, 153, 255;TextAlign:GeneralCenter;Border:None,1,Black,Both;}\tSearch{Font:細明體, 9.75pt;BackColor:White;ForeColor:HighlightText;Border:Double,1,96, 145, 234,Both;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.Grid1.TabIndex = 2;
		this.Grid1.Tree.Column = 1;
		this.Grid1.Tree.LineColor = System.Drawing.Color.Gray;
		this.panel2.BackColor = System.Drawing.Color.White;
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(784, 35);
		this.panel2.TabIndex = 0;
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.CancelButton = this.D_Btn_Next;
		base.ClientSize = new System.Drawing.Size(784, 437);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel16);
		base.Controls.Add(this.panel2);
		base.KeyPreview = true;
		base.MinimizeBox = false;
		base.Name = "FormInvoiceSummary";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "各期估驗彙整查詢";
		base.Load += new System.EventHandler(FormInvoiceSummary_Load);
		this.panel16.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Grid1).EndInit();
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

	public FormInvoiceSummary()
	{
		InitializeComponent();
	}

	private void FormInvoiceSummary_Load(object sender, EventArgs e)
	{
		SetGrid();
		LoadData();
	}

	private void SetGrid()
	{
		Grid1.Cols.Frozen = 4;
	}

	private void LoadData()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(subacc) 刪除-估驗計價");
		sub_acc SubAccCom = new sub_acc(tmp_AL1);
		ldt_AccList = SubAccCom.ListItem("", F_SubProjectCode, F_ProjectCode);
		SubAccCom = null;
		DataBind();
	}

	private void DataBind()
	{
		Grid1.Rows.Count = ldt_AccList.Rows.Count * 2 + 1;
		Grid1.Cols[0].AllowMerging = true;
		Grid1.Cols[1].AllowMerging = true;
		Grid1.Cols[2].AllowMerging = true;
		Grid1.Cols[3].AllowMerging = true;
		for (int i = 0; i < ldt_AccList.Rows.Count; i++)
		{
			Row row = Grid1.Rows[i * 2 + 1];
			bool allowMerging = (Grid1.Rows[i * 2 + 2].AllowMerging = true);
			row.AllowMerging = allowMerging;
			GridBudget grid = Grid1;
			int row2 = i * 2 + 1;
			object value = (Grid1[i * 2 + 2, 1] = ldt_AccList.Rows[i]["Queue"]);
			grid[row2, 1] = value;
			GridBudget grid2 = Grid1;
			int row3 = i * 2 + 1;
			value = (Grid1[i * 2 + 2, 2] = ldt_AccList.Rows[i]["date_rece"]);
			grid2[row3, 2] = value;
			GridBudget grid3 = Grid1;
			int row4 = i * 2 + 1;
			value = (Grid1[i * 2 + 2, 3] = ldt_AccList.Rows[i]["date_insp"]);
			grid3[row4, 3] = value;
			Row row5 = Grid1.Rows[i * 2 + 1];
			allowMerging = (Grid1.Rows[i * 2 + 2].AllowMerging = false);
			row5.AllowMerging = allowMerging;
			Grid1[i * 2 + 1, 4] = "本期估驗";
			Grid1[i * 2 + 2, 4] = "本期累計";
			Grid1[i * 2 + 1, 5] = ldt_AccList.Rows[i]["This_Prec"];
			Grid1[i * 2 + 2, 5] = ldt_AccList.Rows[i]["total_Prec"];
			Grid1[i * 2 + 1, 6] = ldt_AccList.Rows[i]["AccTotal"];
			Grid1[i * 2 + 2, 6] = ldt_AccList.Rows[i]["total_AccTotal"];
			Grid1[i * 2 + 1, 7] = ldt_AccList.Rows[i]["Advancepay"];
			Grid1[i * 2 + 2, 7] = ldt_AccList.Rows[i]["total_Advancepay"];
			Grid1[i * 2 + 1, 8] = ldt_AccList.Rows[i]["advance"];
			Grid1[i * 2 + 2, 8] = ldt_AccList.Rows[i]["total_advance"];
			Grid1[i * 2 + 1, 9] = ldt_AccList.Rows[i]["reserve"];
			Grid1[i * 2 + 2, 9] = ldt_AccList.Rows[i]["total_reserve"];
			Grid1[i * 2 + 1, 10] = ldt_AccList.Rows[i]["Reservertn"];
			Grid1[i * 2 + 2, 10] = ldt_AccList.Rows[i]["total_Reservertn"];
			Grid1[i * 2 + 1, 11] = ldt_AccList.Rows[i]["material"];
			Grid1[i * 2 + 2, 11] = ldt_AccList.Rows[i]["total_material"];
			Grid1[i * 2 + 1, 12] = ldt_AccList.Rows[i]["indexmat"];
			Grid1[i * 2 + 2, 12] = ldt_AccList.Rows[i]["total_indexmat"];
			Grid1[i * 2 + 1, 13] = ldt_AccList.Rows[i]["deduct"];
			Grid1[i * 2 + 2, 13] = ldt_AccList.Rows[i]["total_deduct"];
			Grid1[i * 2 + 1, 14] = ldt_AccList.Rows[i]["accadd"];
			Grid1[i * 2 + 2, 14] = ldt_AccList.Rows[i]["total_accadd"];
			Grid1[i * 2 + 1, 15] = ldt_AccList.Rows[i]["Realpay"];
			Grid1[i * 2 + 2, 15] = ldt_AccList.Rows[i]["total_Realpay"];
		}
	}
}
