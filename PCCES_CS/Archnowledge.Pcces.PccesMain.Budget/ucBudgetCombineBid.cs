using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.DomainModule.Bid;
using Archnowledge.Pcces.PccesMain.ArchControls;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.UltraWinStatusBar;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class ucBudgetCombineBid : UserControl
{
	private IContainer components;

	private Panel panel1;

	private GridBudget gridBidItemA;

	private ImageList imageList;

	private Panel panel2;

	private UltraStatusBar statusBar;

	private Panel panel3;

	private string userID;

	private string targetProjectCode;

	private string sourceProjectCode;

	private BidItemA bidItemA;

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

	public string _ProjectCode
	{
		get
		{
			return sourceProjectCode;
		}
		set
		{
			sourceProjectCode = value;
		}
	}

	public string _MainProjectCode
	{
		get
		{
			return targetProjectCode;
		}
		set
		{
			targetProjectCode = value;
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.ucBudgetCombineBid));
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel4 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.gridBidItemA = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.statusBar = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.imageList = new System.Windows.Forms.ImageList(this.components);
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridBidItemA).BeginInit();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.panel3);
		this.panel1.Controls.Add(this.panel2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(448, 312);
		this.panel1.TabIndex = 0;
		this.panel3.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel3.Location = new System.Drawing.Point(0, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(448, 8);
		this.panel3.TabIndex = 4;
		this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel2.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel2.Controls.Add(this.gridBidItemA);
		this.panel2.Controls.Add(this.statusBar);
		this.panel2.Location = new System.Drawing.Point(8, 14);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(433, 286);
		this.panel2.TabIndex = 3;
		this.gridBidItemA._ExcelFileName = "";
		this.gridBidItemA._ExcelSheeName = "";
		this.gridBidItemA._IsOpenExcelAfterExport = false;
		this.gridBidItemA.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridBidItemA.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridBidItemA.ColumnInfo = resources.GetString("gridBidItemA.ColumnInfo");
		this.gridBidItemA.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridBidItemA.ExtendLastCol = true;
		this.gridBidItemA.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridBidItemA.ForeColor = System.Drawing.Color.Black;
		this.gridBidItemA.Location = new System.Drawing.Point(0, 0);
		this.gridBidItemA.Name = "gridBidItemA";
		this.gridBidItemA.Rows.Count = 1;
		this.gridBidItemA.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.RowRange;
		this.gridBidItemA.ShowCursor = true;
		this.gridBidItemA.ShowSort = false;
		this.gridBidItemA.ShowToolTipOnNarrowColumn = true;
		this.gridBidItemA.Size = new System.Drawing.Size(429, 256);
		this.gridBidItemA.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridBidItemA.Styles"));
		this.gridBidItemA.TabIndex = 1;
		this.gridBidItemA.Tree.Column = 2;
		this.gridBidItemA.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridBidItemA.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridBudget1_AfterEdit);
		appearance1.FontData.SizeInPoints = 11f;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.statusBar.Appearance = appearance1;
		this.statusBar.Location = new System.Drawing.Point(0, 256);
		this.statusBar.Name = "statusBar";
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraStatusPanel1.Appearance = appearance2;
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Key = "RowsCount";
		ultraStatusPanel1.Text = "資料筆數：";
		ultraStatusPanel1.Width = 180;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.Key = "ProgressBar";
		appearance3.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
		appearance3.BackColor2 = System.Drawing.Color.Navy;
		appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		ultraStatusPanel2.ProgressBarInfo.Appearance = appearance3;
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		appearance4.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance4.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		ultraStatusPanel3.Appearance = appearance4;
		ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel3.MarqueeInfo.IsActive = true;
		ultraStatusPanel3.Style = Infragistics.Win.UltraWinStatusBar.PanelStyle.Marquee;
		ultraStatusPanel3.Width = 101;
		appearance5.TextHAlign = Infragistics.Win.HAlign.Right;
		ultraStatusPanel4.Appearance = appearance5;
		ultraStatusPanel4.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel4.Text = "客服電話:(02)2716-5561";
		ultraStatusPanel4.Width = 200;
		this.statusBar.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[4] { ultraStatusPanel1, ultraStatusPanel2, ultraStatusPanel3, ultraStatusPanel4 });
		this.statusBar.Size = new System.Drawing.Size(429, 26);
		this.statusBar.SupportThemes = false;
		this.statusBar.TabIndex = 4;
		this.statusBar.Text = "ultraStatusBar1";
		this.imageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList.ImageStream");
		this.imageList.TransparentColor = System.Drawing.Color.White;
		this.imageList.Images.SetKeyName(0, "");
		base.Controls.Add(this.panel1);
		base.Name = "ucBudgetCombineBid";
		base.Size = new System.Drawing.Size(448, 312);
		base.Load += new System.EventHandler(ucBudgetCombineBid_Load);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridBidItemA).EndInit();
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

	public ucBudgetCombineBid()
	{
		InitializeComponent();
		bidItemA = new BidItemA();
		CellStyle cs = gridBidItemA.Styles.Add("analysisImage");
		cs.DataType = typeof(Image);
	}

	private void ucBudgetCombineBid_Load(object sender, EventArgs e)
	{
		DataSet dsBidItemA = bidItemA.GetItemA(sourceProjectCode, 0);
		BindToGridBidItemA(dsBidItemA);
		statusBar.Panels[0].Text = "資料筆數：" + dsBidItemA.Tables[0].Rows.Count;
	}

	private void BindToGridBidItemA(DataSet dsBidItemA)
	{
		gridBidItemA.Redraw = false;
		CellStyle csAnalysis = gridBidItemA.Styles.Add("Analysis");
		csAnalysis.ForeColor = Color.Red;
		CellStyle csMainItem = gridBidItemA.Styles.Add("MainItem");
		csMainItem.ForeColor = Color.Blue;
		CellStyle csShared = gridBidItemA.Styles.Add("IsShared");
		csShared.ForeColor = Color.Green;
		gridBidItemA.Rows.Count = dsBidItemA.Tables[0].Rows.Count + 1;
		string kind = string.Empty;
		for (int i = 0; i < dsBidItemA.Tables[0].Rows.Count; i++)
		{
			Row gridRow = gridBidItemA.Rows[i + 1];
			DataRow drBidItemA = dsBidItemA.Tables[0].Rows[i];
			kind = drBidItemA["kind"].ToString().ToUpper().Trim();
			switch (kind)
			{
			default:
				if (!(kind == "U"))
				{
					break;
				}
				goto case "B";
			case "B":
			case "L":
			case "F":
			case "S":
			case "Z":
				gridRow.Style = gridBidItemA.Styles["MainItem"];
				break;
			}
			if (drBidItemA["analysis"].ToString().Trim() == "1")
			{
				gridRow.Style = gridBidItemA.Styles["Analysis"];
				CellRange crAnalysisImage = gridBidItemA.GetCellRange(i + 1, gridBidItemA.Cols["analysisImage"].SafeIndex);
				crAnalysisImage.Style = gridBidItemA.Styles["analysisImage"];
				crAnalysisImage.Image = imageList.Images[0];
			}
			gridRow["selected"] = false;
			gridRow["kind"] = kind;
			gridRow["ItemNo"] = drBidItemA["ItemNo"].ToString().Trim();
			gridRow["CName"] = drBidItemA["cName"].ToString().Trim();
			gridRow["UnitName"] = drBidItemA["unitName"].ToString().Trim();
			if (kind != "Z" && (!(kind == "W") || !drBidItemA["pccesCode"].ToString().StartsWith("#")))
			{
				gridRow["Qty"] = drBidItemA["qty"];
				gridRow["Cost"] = drBidItemA["cost"];
			}
			gridRow["PccesCode"] = drBidItemA["pccesCode"].ToString().Trim();
			gridRow["Memo"] = drBidItemA["memo"].ToString().Trim();
			gridRow["sNo"] = drBidItemA["sNo"].ToString().Trim();
			if (drBidItemA["kind"] != DBNull.Value)
			{
				gridRow.IsNode = true;
			}
			string printNo = drBidItemA["printNo"].ToString().Trim();
			if (printNo == "".PadLeft(32, '9') || (printNo.Length == 4 && drBidItemA["Kind"].ToString().Trim() == "Z" && i == dsBidItemA.Tables[0].Rows.Count - 1))
			{
				gridRow.Node.Level = 1;
			}
			else
			{
				gridRow.Node.Level = Convert.ToInt32(drBidItemA["PrintNo"].ToString().Trim().Length / 4);
			}
			if (drBidItemA["share"] != null && drBidItemA["share"].ToString().Trim() == "1")
			{
				gridRow.Style = gridBidItemA.Styles["IsShared"];
			}
		}
		gridBidItemA.Redraw = true;
	}

	public ExecResult ImportCost(bool isOverriden)
	{
		DataSet dsSNo = new DataSet();
		dsSNo.Tables.Add().Columns.Add("sNo", Type.GetType("System.Int32"));
		foreach (Row row in (IEnumerable)gridBidItemA.Rows)
		{
			if (row.Index != 0 && row["selected"] != null && (bool)row["selected"] && row["kind"].ToString() == "W")
			{
				DataRow drSNo = dsSNo.Tables[0].NewRow();
				drSNo["sNo"] = row["sNo"].ToString();
				dsSNo.Tables[0].Rows.Add(drSNo);
			}
		}
		return bidItemA.ImportCostFromOtherProject(dsSNo, sourceProjectCode, targetProjectCode, isOverriden);
	}

	private void gridBudget1_AfterEdit(object sender, RowColEventArgs e)
	{
		if (gridBidItemA.Cols[e.Col].Name == "selected")
		{
			int selectedLevel = gridBidItemA.Rows[e.Row].Node.Level;
			for (int i = e.Row + 1; i <= gridBidItemA.Rows.Count - 1 && gridBidItemA.Rows[i].Node.Level > selectedLevel; i++)
			{
				gridBidItemA[i, "selected"] = gridBidItemA[e.Row, "selected"];
			}
		}
	}
}
