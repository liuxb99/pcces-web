using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.PccesMain.ArchControls;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinStatusBar;

namespace Archnowledge.Pcces.PccesMain.Budget.BudgetChange;

public class FormBudgetChangeResponsibility : Form
{
	private IContainer components = null;

	private UltraLabel lbInstruction;

	private UltraLabel lbTitle;

	private Panel panelGrid;

	private GridBudget gridQtyChangeResponsibility;

	private UltraStatusBar statusBar;

	private Panel panelTop;

	private Panel panelBottom;

	private UltraButton btnCancel;

	private UltraButton btnOK;

	private GroupBox gbButtons;

	private ImageList imageList;

	private UltraLabel lbTargetItem;

	private string projectCode;

	private int version;

	private int sNo;

	private string itemNo;

	private string itemName;

	private double totalQty = 0.0;

	private double originalQty = 0.0;

	private DataSet dsBudgetChangeResponsibility;

	private BudgetChangeResponsibility budgetChangeResponsibility;

	private bool viewMode = false;

	public double TotalQty => totalQty;

	public double OriginalQty => originalQty;

	public string ItemNo
	{
		set
		{
			itemNo = value;
		}
	}

	public string ItemName
	{
		set
		{
			itemName = value;
		}
	}

	public bool ViewMode
	{
		set
		{
			viewMode = value;
		}
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
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.BudgetChange.FormBudgetChangeResponsibility));
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		this.lbInstruction = new Infragistics.Win.Misc.UltraLabel();
		this.lbTitle = new Infragistics.Win.Misc.UltraLabel();
		this.panelGrid = new System.Windows.Forms.Panel();
		this.gridQtyChangeResponsibility = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.statusBar = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.panelTop = new System.Windows.Forms.Panel();
		this.panelBottom = new System.Windows.Forms.Panel();
		this.btnCancel = new Infragistics.Win.Misc.UltraButton();
		this.btnOK = new Infragistics.Win.Misc.UltraButton();
		this.gbButtons = new System.Windows.Forms.GroupBox();
		this.imageList = new System.Windows.Forms.ImageList(this.components);
		this.lbTargetItem = new Infragistics.Win.Misc.UltraLabel();
		this.panelGrid.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridQtyChangeResponsibility).BeginInit();
		this.panelTop.SuspendLayout();
		this.panelBottom.SuspendLayout();
		base.SuspendLayout();
		appearance1.BackColor = System.Drawing.Color.White;
		this.lbInstruction.Appearance = appearance1;
		this.lbInstruction.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbInstruction.Location = new System.Drawing.Point(26, 29);
		this.lbInstruction.Name = "lbInstruction";
		this.lbInstruction.Size = new System.Drawing.Size(337, 37);
		this.lbInstruction.TabIndex = 6;
		this.lbInstruction.Text = "請分別輸入各責任歸屬變更數量，按確定後會將數量加總寫入詳細表工項之數量欄位。";
		appearance2.BackColor = System.Drawing.Color.White;
		this.lbTitle.Appearance = appearance2;
		this.lbTitle.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lbTitle.Location = new System.Drawing.Point(10, 8);
		this.lbTitle.Name = "lbTitle";
		this.lbTitle.Size = new System.Drawing.Size(352, 20);
		this.lbTitle.TabIndex = 5;
		this.lbTitle.Text = "變更責任歸屬設定";
		this.panelGrid.Controls.Add(this.gridQtyChangeResponsibility);
		this.panelGrid.Controls.Add(this.statusBar);
		this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panelGrid.Location = new System.Drawing.Point(0, 113);
		this.panelGrid.Name = "panelGrid";
		this.panelGrid.Size = new System.Drawing.Size(368, 281);
		this.panelGrid.TabIndex = 15;
		this.gridQtyChangeResponsibility._ExcelFileName = "";
		this.gridQtyChangeResponsibility._ExcelSheeName = "";
		this.gridQtyChangeResponsibility._IsOpenExcelAfterExport = false;
		this.gridQtyChangeResponsibility.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridQtyChangeResponsibility.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridQtyChangeResponsibility.ColumnInfo = resources.GetString("gridQtyChangeResponsibility.ColumnInfo");
		this.gridQtyChangeResponsibility.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridQtyChangeResponsibility.ExtendLastCol = true;
		this.gridQtyChangeResponsibility.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridQtyChangeResponsibility.ForeColor = System.Drawing.Color.Black;
		this.gridQtyChangeResponsibility.Location = new System.Drawing.Point(0, 0);
		this.gridQtyChangeResponsibility.Name = "gridQtyChangeResponsibility";
		this.gridQtyChangeResponsibility.Rows.Count = 1;
		this.gridQtyChangeResponsibility.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridQtyChangeResponsibility.ShowCursor = true;
		this.gridQtyChangeResponsibility.ShowSort = false;
		this.gridQtyChangeResponsibility.ShowToolTipOnNarrowColumn = true;
		this.gridQtyChangeResponsibility.Size = new System.Drawing.Size(368, 255);
		this.gridQtyChangeResponsibility.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridQtyChangeResponsibility.Styles"));
		this.gridQtyChangeResponsibility.TabIndex = 1;
		this.gridQtyChangeResponsibility.Tree.Column = 1;
		this.gridQtyChangeResponsibility.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridQtyChangeResponsibility.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridQtyChangeResponsibility_AfterEdit);
		appearance3.FontData.SizeInPoints = 11f;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.statusBar.Appearance = appearance3;
		this.statusBar.Location = new System.Drawing.Point(0, 255);
		this.statusBar.Name = "statusBar";
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		ultraStatusPanel1.Text = "數量加總：0";
		appearance4.TextHAlign = Infragistics.Win.HAlign.Right;
		ultraStatusPanel2.Appearance = appearance4;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.Text = "客服電話：(02)2716-5561";
		ultraStatusPanel2.Width = 200;
		this.statusBar.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[2] { ultraStatusPanel1, ultraStatusPanel2 });
		this.statusBar.Size = new System.Drawing.Size(368, 26);
		this.statusBar.TabIndex = 2;
		this.panelTop.BackColor = System.Drawing.Color.White;
		this.panelTop.Controls.Add(this.lbTargetItem);
		this.panelTop.Controls.Add(this.lbInstruction);
		this.panelTop.Controls.Add(this.lbTitle);
		this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
		this.panelTop.Location = new System.Drawing.Point(0, 0);
		this.panelTop.Name = "panelTop";
		this.panelTop.Size = new System.Drawing.Size(368, 113);
		this.panelTop.TabIndex = 14;
		this.panelBottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panelBottom.Controls.Add(this.btnCancel);
		this.panelBottom.Controls.Add(this.btnOK);
		this.panelBottom.Controls.Add(this.gbButtons);
		this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panelBottom.Location = new System.Drawing.Point(0, 394);
		this.panelBottom.Name = "panelBottom";
		this.panelBottom.Size = new System.Drawing.Size(368, 44);
		this.panelBottom.TabIndex = 13;
		this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance6.Image = resources.GetObject("appearance6.Image");
		appearance6.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnCancel.Appearance = appearance6;
		this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
		this.btnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Font = new System.Drawing.Font("細明體", 11f);
		this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btnCancel.Location = new System.Drawing.Point(275, 10);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.ShowFocusRect = false;
		this.btnCancel.ShowOutline = false;
		this.btnCancel.Size = new System.Drawing.Size(88, 31);
		this.btnCancel.SupportThemes = false;
		this.btnCancel.TabIndex = 5;
		this.btnCancel.Text = "取消";
		this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance7.Image = resources.GetObject("appearance7.Image");
		appearance7.ImageHAlign = Infragistics.Win.HAlign.Left;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnOK.Appearance = appearance7;
		this.btnOK.BackColor = System.Drawing.SystemColors.Control;
		this.btnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOK.Font = new System.Drawing.Font("細明體", 11f);
		this.btnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.btnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.btnOK.Location = new System.Drawing.Point(183, 10);
		this.btnOK.Name = "btnOK";
		this.btnOK.ShowFocusRect = false;
		this.btnOK.ShowOutline = false;
		this.btnOK.Size = new System.Drawing.Size(88, 31);
		this.btnOK.SupportThemes = false;
		this.btnOK.TabIndex = 4;
		this.btnOK.Text = "確定";
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.gbButtons.Dock = System.Windows.Forms.DockStyle.Top;
		this.gbButtons.Location = new System.Drawing.Point(0, 0);
		this.gbButtons.Name = "gbButtons";
		this.gbButtons.Size = new System.Drawing.Size(368, 8);
		this.gbButtons.TabIndex = 3;
		this.gbButtons.TabStop = false;
		this.imageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList.ImageStream");
		this.imageList.TransparentColor = System.Drawing.Color.White;
		this.imageList.Images.SetKeyName(0, "");
		appearance8.BackColor = System.Drawing.Color.White;
		this.lbTargetItem.Appearance = appearance8;
		this.lbTargetItem.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbTargetItem.Location = new System.Drawing.Point(10, 75);
		this.lbTargetItem.Name = "lbTargetItem";
		this.lbTargetItem.Size = new System.Drawing.Size(352, 32);
		this.lbTargetItem.TabIndex = 7;
		this.lbTargetItem.Text = "變更工項：";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(368, 438);
		base.Controls.Add(this.panelGrid);
		base.Controls.Add(this.panelTop);
		base.Controls.Add(this.panelBottom);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormBudgetChangeResponsibility";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "變更責任歸屬";
		base.Load += new System.EventHandler(FormBudgetChangeResponsibility_Load);
		this.panelGrid.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridQtyChangeResponsibility).EndInit();
		this.panelTop.ResumeLayout(false);
		this.panelBottom.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public FormBudgetChangeResponsibility(string projectCode, int version, int sNo)
	{
		InitializeComponent();
		this.projectCode = projectCode;
		this.version = version;
		this.sNo = sNo;
		budgetChangeResponsibility = new BudgetChangeResponsibility();
		GetData();
		Data2Grid();
	}

	private void FormBudgetChangeResponsibility_Load(object sender, EventArgs e)
	{
		lbTargetItem.Text = $"變更工項：{itemNo}. {itemName}";
		if (viewMode)
		{
			gridQtyChangeResponsibility.Cols["Qty"].AllowEditing = false;
			btnOK.Visible = false;
			btnCancel.Text = "關閉";
		}
		SetColumnHeaderEditSymbol();
	}

	private void GetData()
	{
		dsBudgetChangeResponsibility = budgetChangeResponsibility.GetBudgetChangeResponsibility(projectCode, version, sNo);
		DataTable dtBudgetChangeResponsibility = dsBudgetChangeResponsibility.Tables["BudgetChangeResponsibility"];
		UserDefined userDefined = new UserDefined();
		DataSet dsDepartment = userDefined.GetUserDefinedByKind("BudgetChangeResponsibility");
		foreach (DataRow drDepartment in dsDepartment.Tables[0].Rows)
		{
			if (dtBudgetChangeResponsibility.Select("Department = '" + drDepartment["cString"].ToString() + "'").Length == 0)
			{
				DataRow drBudgetChangeResponsibility = dtBudgetChangeResponsibility.NewRow();
				drBudgetChangeResponsibility["ProjectCode"] = projectCode;
				drBudgetChangeResponsibility["Version"] = version;
				drBudgetChangeResponsibility["SNo"] = sNo;
				drBudgetChangeResponsibility["Department"] = ArchConvert.Obj2String(drDepartment["cString"]);
				drBudgetChangeResponsibility["Qty"] = 0;
				drBudgetChangeResponsibility["DepartmentOrder"] = ArchConvert.Obj2Int(drDepartment["sNo"]);
				dtBudgetChangeResponsibility.Rows.Add(drBudgetChangeResponsibility);
			}
		}
	}

	private void Data2Grid()
	{
		DataTable dtBudgetChangeResponsibility = dsBudgetChangeResponsibility.Tables["BudgetChangeResponsibility"];
		gridQtyChangeResponsibility.Rows.Count = dtBudgetChangeResponsibility.Rows.Count + 1;
		DataView dvBudgetChangeResponsibility = dtBudgetChangeResponsibility.DefaultView;
		dvBudgetChangeResponsibility.Sort = "DepartmentOrder ASC";
		for (int rowIndex = 0; rowIndex < dvBudgetChangeResponsibility.Count; rowIndex++)
		{
			gridQtyChangeResponsibility[rowIndex + 1, "Department"] = dvBudgetChangeResponsibility[rowIndex]["Department"];
			gridQtyChangeResponsibility[rowIndex + 1, "Qty"] = dvBudgetChangeResponsibility[rowIndex]["Qty"];
		}
		CalculateTotalQty();
	}

	private void SetColumnHeaderEditSymbol()
	{
		CellStyle csEditMode = gridQtyChangeResponsibility.Styles.Add("EditMode");
		csEditMode.DataType = typeof(Image);
		csEditMode.ImageAlign = ImageAlignEnum.RightCenter;
		for (int i = 1; i < gridQtyChangeResponsibility.Cols.Count; i++)
		{
			if (gridQtyChangeResponsibility.Cols[i].AllowEditing)
			{
				CellRange cellRange = gridQtyChangeResponsibility.GetCellRange(0, i);
				cellRange.Style = gridQtyChangeResponsibility.Styles["EditMode"];
				cellRange.Image = imageList.Images[0];
			}
		}
	}

	private void gridQtyChangeResponsibility_AfterEdit(object sender, RowColEventArgs e)
	{
		DataRow[] MatchedRows = dsBudgetChangeResponsibility.Tables["BudgetChangeResponsibility"].Select("Department = '" + ArchConvert.Obj2String(gridQtyChangeResponsibility[e.Row, "Department"]) + "'");
		if (MatchedRows.Length > 0)
		{
			MatchedRows[0]["Qty"] = gridQtyChangeResponsibility[e.Row, "Qty"];
		}
		CalculateTotalQty();
	}

	private void CalculateTotalQty()
	{
		originalQty = TotalQty;
		totalQty = 0.0;
		for (int rowIndex = 1; rowIndex < gridQtyChangeResponsibility.Rows.Count; rowIndex++)
		{
			totalQty += ArchConvert.Obj2Double(gridQtyChangeResponsibility[rowIndex, "Qty"]);
		}
		statusBar.Panels[0].Text = "數量加總：" + totalQty;
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		DataTable dtBudgetChangeResponsibility = dsBudgetChangeResponsibility.Tables["BudgetChangeResponsibility"];
		for (int rowIndex = dtBudgetChangeResponsibility.Rows.Count - 1; rowIndex >= 0; rowIndex--)
		{
			if (ArchConvert.Obj2Double(dtBudgetChangeResponsibility.Rows[rowIndex]["Qty"]) == 0.0)
			{
				dtBudgetChangeResponsibility.Rows[rowIndex].Delete();
			}
		}
		ExecResult ER = budgetChangeResponsibility.UpdateBudgetChangeResponsibility(dsBudgetChangeResponsibility);
		if (ER.ReturnCode != 0)
		{
			MessageBox.Show(ER.Message);
		}
		else
		{
			base.DialogResult = DialogResult.OK;
		}
	}
}
