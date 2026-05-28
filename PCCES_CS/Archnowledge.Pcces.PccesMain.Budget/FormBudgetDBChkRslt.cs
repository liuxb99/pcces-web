using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.PccesMain.ArchControls;
using C1.Win.C1FlexGrid;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetDBChkRslt : Form
{
	private IContainer components;

	private Panel panel2;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel6;

	private Panel panel1;

	private Panel panel3;

	private GridBudget FlexGrid1;

	private GroupBox groupBox6;

	private UltraButton D_Btn_Next;

	private UltraButton ultraButton1;

	private UltraButton ultraButton2;

	private SaveFileDialog saveFileDialog1;

	private DataTable F_DT_DBChk = new DataTable();

	public DataTable _DT_DBChk
	{
		get
		{
			return F_DT_DBChk;
		}
		set
		{
			F_DT_DBChk = value;
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetDBChkRslt));
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		this.panel2 = new System.Windows.Forms.Panel();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.FlexGrid1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.panel3 = new System.Windows.Forms.Panel();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.D_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
		this.panel2.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.FlexGrid1).BeginInit();
		this.panel3.SuspendLayout();
		base.SuspendLayout();
		this.panel2.BackColor = System.Drawing.Color.White;
		this.panel2.Controls.Add(this.ultraButton2);
		this.panel2.Controls.Add(this.ultraLabel7);
		this.panel2.Controls.Add(this.ultraLabel6);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(772, 72);
		this.panel2.TabIndex = 1;
		this.ultraButton2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.ultraButton2.Location = new System.Drawing.Point(664, 32);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.Size = new System.Drawing.Size(96, 32);
		this.ultraButton2.TabIndex = 5;
		this.ultraButton2.Text = "匯出EXCEL";
		this.ultraButton2.Click += new System.EventHandler(ultraButton2_Click);
		appearance1.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance1;
		this.ultraLabel7.Location = new System.Drawing.Point(38, 31);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(426, 33);
		this.ultraLabel7.TabIndex = 4;
		this.ultraLabel7.Text = "目前專案與基本資料庫，以下項目有名稱或單位不同的狀況，請先修正後再做資料重整";
		appearance2.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance2;
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(21, 10);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 3;
		this.ultraLabel6.Text = "檢查結果";
		this.panel1.Controls.Add(this.FlexGrid1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 72);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(772, 165);
		this.panel1.TabIndex = 2;
		this.FlexGrid1._ExcelFileName = "";
		this.FlexGrid1._ExcelSheeName = "";
		this.FlexGrid1._IsOpenExcelAfterExport = false;
		this.FlexGrid1.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free;
		this.FlexGrid1.BackColor = System.Drawing.Color.White;
		this.FlexGrid1.ColumnInfo = "6,0,0,0,0,110,Columns:0{Width:100;Name:\"PccesCode1\";Caption:\"項次\";AllowDragging:False;AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t1{Width:200;Name:\"CName1\";Caption:\"選目及說明\";AllowDragging:False;AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t2{Width:80;Name:\"Unitname1\";Caption:\"單位\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t3{Width:100;Name:\"PccesCode2\";Caption:\"預算數量\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t4{Width:200;Name:\"CName2\";Caption:\"預算單價\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t5{Width:80;Name:\"Unitname2\";Caption:\"分標數量\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t";
		this.FlexGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.FlexGrid1.ExtendLastCol = true;
		this.FlexGrid1.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.FlexGrid1.ForeColor = System.Drawing.Color.Black;
		this.FlexGrid1.Location = new System.Drawing.Point(0, 0);
		this.FlexGrid1.Name = "FlexGrid1";
		this.FlexGrid1.Rows.Fixed = 2;
		this.FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.FlexGrid1.ShowToolTipOnNarrowColumn = true;
		this.FlexGrid1.Size = new System.Drawing.Size(772, 165);
		this.FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11pt;BackColor:White;ForeColor:Black;Border:Flat,1,Silver,Both;}\tFixed{BackColor:225, 247, 223;TextAlign:GeneralTop;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;ForeColor:White;}\tFocus{Font:細明體, 9.75pt, style=Bold;BackColor:102, 153, 255;Border:Flat,1,102, 153, 255,Both;}\tSearch{BackColor:Highlight;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:AppWorkspace;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.FlexGrid1.TabIndex = 6;
		this.FlexGrid1.Tree.Column = 1;
		this.FlexGrid1.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.SimpleLeaf;
		this.panel3.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel3.Controls.Add(this.ultraButton1);
		this.panel3.Controls.Add(this.D_Btn_Next);
		this.panel3.Controls.Add(this.groupBox6);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel3.Location = new System.Drawing.Point(0, 237);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(772, 48);
		this.panel3.TabIndex = 3;
		this.ultraButton1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton1.Appearance = appearance3;
		this.ultraButton1.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton1.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton1.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton1.Location = new System.Drawing.Point(675, 12);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(88, 31);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 7;
		this.ultraButton1.Text = "關閉";
		this.D_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance4.Image = resources.GetObject("appearance4.Image");
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Next.Appearance = appearance4;
		this.D_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Next.DialogResult = System.Windows.Forms.DialogResult.Ignore;
		this.D_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Next.Location = new System.Drawing.Point(392, 12);
		this.D_Btn_Next.Name = "D_Btn_Next";
		this.D_Btn_Next.ShowFocusRect = false;
		this.D_Btn_Next.ShowOutline = false;
		this.D_Btn_Next.Size = new System.Drawing.Size(280, 31);
		this.D_Btn_Next.SupportThemes = false;
		this.D_Btn_Next.TabIndex = 6;
		this.D_Btn_Next.Text = "忽略檢查檢結果，立即資料庫重整";
		this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox6.Location = new System.Drawing.Point(0, 0);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(772, 8);
		this.groupBox6.TabIndex = 5;
		this.groupBox6.TabStop = false;
		this.saveFileDialog1.DefaultExt = "xls";
		this.saveFileDialog1.Filter = "EXCEL 檔案|*.xls";
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.CancelButton = this.ultraButton1;
		base.ClientSize = new System.Drawing.Size(772, 285);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel2);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormBudgetDBChkRslt";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "檢查結果";
		base.Load += new System.EventHandler(FormBudgetDBChkRslt_Load);
		this.panel2.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.FlexGrid1).EndInit();
		this.panel3.ResumeLayout(false);
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

	public FormBudgetDBChkRslt()
	{
		InitializeComponent();
	}

	private void FormBudgetDBChkRslt_Load(object sender, EventArgs e)
	{
		FlexGrid1.Rows[0].AllowMerging = true;
		GridBudget flexGrid = FlexGrid1;
		GridBudget flexGrid2 = FlexGrid1;
		object obj = (FlexGrid1[0, 2] = "專案工項");
		obj = (flexGrid2[0, 1] = obj);
		flexGrid[0, 0] = obj;
		GridBudget flexGrid3 = FlexGrid1;
		GridBudget flexGrid4 = FlexGrid1;
		obj = (FlexGrid1[0, 5] = "基本資料庫工項");
		obj = (flexGrid4[0, 4] = obj);
		flexGrid3[0, 3] = obj;
		FlexGrid1.Cols[0].AllowMerging = true;
		FlexGrid1.Cols[1].AllowMerging = true;
		FlexGrid1.Cols[2].AllowMerging = true;
		FlexGrid1.Cols[3].AllowMerging = true;
		FlexGrid1.Cols[4].AllowMerging = true;
		FlexGrid1.Cols[5].AllowMerging = true;
		FlexGrid1[1, 0] = "工項代碼";
		FlexGrid1[1, 1] = "工項名稱";
		FlexGrid1[1, 2] = "單位";
		FlexGrid1[1, 3] = "工項代碼";
		FlexGrid1[1, 4] = "工項名稱";
		FlexGrid1[1, 5] = "單位";
		BindToGrid();
	}

	private void BindToGrid()
	{
		FlexGrid1.Rows.Count = F_DT_DBChk.Rows.Count + 2;
		for (int i = 0; i < F_DT_DBChk.Rows.Count; i++)
		{
			FlexGrid1[i + 2, "PccesCode1"] = F_DT_DBChk.Rows[i]["PccesCode1"].ToString().Trim();
			FlexGrid1[i + 2, "PccesCode2"] = F_DT_DBChk.Rows[i]["PccesCode2"].ToString().Trim();
			FlexGrid1[i + 2, "CName1"] = F_DT_DBChk.Rows[i]["CName1"].ToString().Trim();
			FlexGrid1[i + 2, "CName2"] = F_DT_DBChk.Rows[i]["CName2"].ToString().Trim();
			FlexGrid1[i + 2, "UnitName1"] = F_DT_DBChk.Rows[i]["UnitName1"].ToString().Trim();
			FlexGrid1[i + 2, "UnitName2"] = F_DT_DBChk.Rows[i]["UnitName2"].ToString().Trim();
		}
	}

	private void ultraButton2_Click(object sender, EventArgs e)
	{
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			FlexGrid1.SaveExcel(saveFileDialog1.FileName, "NotCorrect", FileFlags.IncludeFixedCells);
		}
	}
}
