using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetItemNo_New : Form
{
	private const string CallFormHelp = "FormBudgetItemNo_New";

	private Panel panel9;

	private GroupBox groupBox5;

	private UltraButton A1_Btn_Cncl;

	private Panel panel1;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private Panel panel2;

	public GridMrsBase GridUnit1;

	private UltraButton Btn_OK;

	private IContainer components;

	public FormBudgetItemNo_New()
	{
		InitializeComponent();
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetItemNo_New));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		this.panel9 = new System.Windows.Forms.Panel();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.A1_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.Btn_OK = new Infragistics.Win.Misc.UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.GridUnit1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.panel9.SuspendLayout();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).BeginInit();
		base.SuspendLayout();
		this.panel9.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel9.Controls.Add(this.groupBox5);
		this.panel9.Controls.Add(this.A1_Btn_Cncl);
		this.panel9.Controls.Add(this.Btn_OK);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel9.Location = new System.Drawing.Point(0, 285);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(332, 44);
		this.panel9.TabIndex = 21;
		this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox5.Location = new System.Drawing.Point(0, 0);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(332, 8);
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
		this.A1_Btn_Cncl.Location = new System.Drawing.Point(236, 9);
		this.A1_Btn_Cncl.Name = "A1_Btn_Cncl";
		this.A1_Btn_Cncl.ShowFocusRect = false;
		this.A1_Btn_Cncl.ShowOutline = false;
		this.A1_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A1_Btn_Cncl.SupportThemes = false;
		this.A1_Btn_Cncl.TabIndex = 2;
		this.A1_Btn_Cncl.Text = "取消";
		this.Btn_OK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.Btn_OK.Appearance = appearance2;
		this.Btn_OK.BackColor = System.Drawing.SystemColors.Control;
		this.Btn_OK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.Btn_OK.Font = new System.Drawing.Font("細明體", 11f);
		this.Btn_OK.ImageSize = new System.Drawing.Size(20, 20);
		this.Btn_OK.ImageTransparentColor = System.Drawing.Color.White;
		this.Btn_OK.Location = new System.Drawing.Point(144, 9);
		this.Btn_OK.Name = "Btn_OK";
		this.Btn_OK.ShowFocusRect = false;
		this.Btn_OK.ShowOutline = false;
		this.Btn_OK.Size = new System.Drawing.Size(88, 31);
		this.Btn_OK.SupportThemes = false;
		this.Btn_OK.TabIndex = 1;
		this.Btn_OK.Text = "確定";
		this.Btn_OK.Click += new System.EventHandler(Btn_OK_Click);
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.Controls.Add(this.ultraLabel2);
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(332, 52);
		this.panel1.TabIndex = 22;
		this.ultraLabel2.Location = new System.Drawing.Point(24, 28);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(296, 16);
		this.ultraLabel2.TabIndex = 1;
		this.ultraLabel2.Text = "請直接編輯編號樣式內容";
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(8, 8);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(100, 16);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "新增樣式";
		this.panel2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel2.Controls.Add(this.GridUnit1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 52);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(332, 233);
		this.panel2.TabIndex = 23;
		this.GridUnit1._ExcelFileName = "";
		this.GridUnit1._ExcelSheeName = "";
		this.GridUnit1._IsOpenExcelAfterExport = false;
		this.GridUnit1.AllowAddNew = true;
		this.GridUnit1.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
		this.GridUnit1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
		this.GridUnit1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridUnit1.ColumnInfo = "2,1,0,0,0,110,Columns:0{Width:60;Name:\"RowIndicator\";Caption:\"順序\";AllowDragging:False;AllowResizing:False;AllowEditing:False;DataType:System.Int32;TextAlign:RightCenter;TextAlignFixed:CenterTop;}\t1{Name:\"cString\";Caption:\" 項次編號\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t";
		this.GridUnit1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridUnit1.ExtendLastCol = true;
		this.GridUnit1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridUnit1.ForeColor = System.Drawing.Color.Black;
		this.GridUnit1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.GridUnit1.IsProcessUndo = false;
		this.GridUnit1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.GridUnit1.Location = new System.Drawing.Point(0, 0);
		this.GridUnit1.Name = "GridUnit1";
		this.GridUnit1.Rows.Count = 8;
		this.GridUnit1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.GridUnit1.ShowCursor = true;
		this.GridUnit1.ShowToolTipOnNarrowColumn = true;
		this.GridUnit1.Size = new System.Drawing.Size(332, 233);
		this.GridUnit1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11.25pt;BackColor:237, 243, 254;ForeColor:Black;TextAlign:GeneralBottom;Border:Flat,1,Silver,Both;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;TextAlign:GeneralTop;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;TextAlign:LeftCenter;ImageAlign:CenterCenter;Border:None,1,Black,Both;Format:\"###,###,###,##0\";}\tFocus{Font:細明體, 10pt, style=Bold;BackColor:White;Margins:0, 0, 0, 0;TextAlign:LeftCenter;Border:Double,1,102, 153, 255,Both;}\tSearch{BackColor:Highlight;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.GridUnit1.TabIndex = 10;
		this.GridUnit1.UndoMax = 10;
		this.GridUnit1.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(GridUnit1_AfterEdit);
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.CancelButton = this.A1_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(332, 329);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel9);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormBudgetItemNo_New";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "新增樣式";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormBudgetItemNo_New_KeyDown);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormBudgetItemNo_New_FormClosing);
		base.Load += new System.EventHandler(FormBudgetItemNo_New_Load);
		this.panel9.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).EndInit();
		base.ResumeLayout(false);
	}

	private void Btn_OK_Click(object sender, EventArgs e)
	{
		for (int i = 0; i <= 2; i++)
		{
			if (GridUnit1.Rows[i]["cString"] == null || GridUnit1.Rows[i]["cString"].ToString() == "")
			{
				MessageBox.Show(this, "請完成至少 3 項項次編號 !", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
		}
		DataTable DT_Num = new DataTable();
		DT_Num.Columns.Add("kind", Type.GetType("System.String"));
		DT_Num.Columns.Add("cString", Type.GetType("System.String"));
		DT_Num.Columns.Add("sno", Type.GetType("System.Int64"));
		string KeyWord = $"{DateTime.Now:yyyyMMddHHmmss}";
		for (int i = 1; i < GridUnit1.Rows.Count; i++)
		{
			if (GridUnit1.Rows[i]["RowIndicator"] != null && !(GridUnit1.Rows[i]["RowIndicator"].ToString() == "") && GridUnit1.Rows[i]["cString"] != null && !(GridUnit1.Rows[i]["cString"].ToString() == ""))
			{
				DataRow DR = DT_Num.NewRow();
				DR["kind"] = KeyWord;
				DR["cString"] = GridUnit1.Rows[i]["cString"].ToString().Trim();
				DR["sno"] = (Convert.ToInt32(GridUnit1.Rows[i]["RowIndicator"]) + 200000).ToString();
				DT_Num.Rows.Add(DR);
			}
		}
		DBClass DBCLS = new DBClass();
		DBCLS.SaveItemNo(DT_Num, KeyWord);
		DBCLS = null;
		base.DialogResult = DialogResult.OK;
		Close();
	}

	private void GridUnit1_AfterEdit(object sender, RowColEventArgs e)
	{
		ReNum();
	}

	private void ReNum()
	{
		for (int i = 1; i < GridUnit1.Rows.Count - 1; i++)
		{
			GridUnit1[i, "RowIndicator"] = i.ToString();
		}
	}

	private void FormBudgetItemNo_New_Load(object sender, EventArgs e)
	{
		LoadingScreen();
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("ItemNo", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("ItemNo", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("ItemNo", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("ItemNo", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("ItemNo", "PK_Height"));
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

	private void FormBudgetItemNo_New_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("ItemNo", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("ItemNo", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("ItemNo", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("ItemNo", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("ItemNo", "WindowState", base.WindowState.ToString());
	}

	private void FormBudgetItemNo_New_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormBudgetItemNo_New");
		}
	}
}
