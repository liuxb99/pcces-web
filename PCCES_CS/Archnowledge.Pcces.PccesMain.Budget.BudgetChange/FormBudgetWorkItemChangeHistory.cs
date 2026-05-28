using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.DomainModule.BudExe;
using Archnowledge.Pcces.PccesMain.ArchControls;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.Budget.BudgetChange;

public class FormBudgetWorkItemChangeHistory : Form
{
	private string F_UserID;

	private string F_ProjectCode;

	private int F_PubCode;

	private IContainer components;

	private Panel panelTop;

	private UltraLabel ulPccesCode;

	private UltraLabel ulName;

	private UltraLabel ulUnit;

	private UltraLabel ulShowPccesCode;

	private UltraLabel ulShowName;

	private UltraLabel ulShowUnit;

	private GridBudget GridUnit1;

	private Panel panel1Bottom;

	private UltraButton btnOK;

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

	public int _PubCode
	{
		get
		{
			return F_PubCode;
		}
		set
		{
			F_PubCode = value;
		}
	}

	public FormBudgetWorkItemChangeHistory()
	{
		InitializeComponent();
	}

	private void FormBudgetWorkItemChangeHistory_Load(object sender, EventArgs e)
	{
		DataSet ds = GetData();
		BindToGrid(ds);
	}

	private DataSet GetData()
	{
		BudExeProjMrsA budExeProjMrsA = new BudExeProjMrsA();
		return budExeProjMrsA.GetBudExeProjMrsAAllVersionByPubCode(F_ProjectCode, F_PubCode);
	}

	private void BindToGrid(DataSet ds)
	{
		DataTable dtBudProjMrsA = ds.Tables["BudProjMrsA"];
		DataTable dtBudExeProjMrsA = ds.Tables["BudExeProjMrsA"];
		GridUnit1.Rows.Count = dtBudProjMrsA.Rows.Count + dtBudExeProjMrsA.Rows.Count + 1;
		for (int i = 0; i < dtBudExeProjMrsA.Rows.Count; i++)
		{
			int version = ArchConvert.Obj2Int(dtBudExeProjMrsA.Rows[i]["version"]);
			GridUnit1[i + 1, "version"] = ((version == 0) ? "原預算" : $"第 {version} 期變更");
			GridUnit1[i + 1, "usrQty"] = dtBudExeProjMrsA.Rows[i]["usrQty"].ToString().Trim();
		}
		for (int i = 0; i < dtBudProjMrsA.Rows.Count; i++)
		{
			GridUnit1[i + 1 + dtBudExeProjMrsA.Rows.Count, "version"] = $"第 {dtBudExeProjMrsA.Rows.Count} 期變更";
			GridUnit1[i + 1 + dtBudExeProjMrsA.Rows.Count, "usrQty"] = dtBudProjMrsA.Rows[i]["usrQty"].ToString().Trim();
			ulShowName.Text = ArchConvert.Obj2String(dtBudProjMrsA.Rows[0]["cName"]);
			ulShowPccesCode.Text = ArchConvert.Obj2String(dtBudProjMrsA.Rows[0]["pccesCode"]);
			ulShowUnit.Text = ArchConvert.Obj2String(dtBudProjMrsA.Rows[0]["unitName"]);
		}
		GridUnit1.AutoSizeCols();
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.BudgetChange.FormBudgetWorkItemChangeHistory));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		this.ulPccesCode = new Infragistics.Win.Misc.UltraLabel();
		this.GridUnit1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.ulName = new Infragistics.Win.Misc.UltraLabel();
		this.ulUnit = new Infragistics.Win.Misc.UltraLabel();
		this.btnOK = new Infragistics.Win.Misc.UltraButton();
		this.panel1Bottom = new System.Windows.Forms.Panel();
		this.panelTop = new System.Windows.Forms.Panel();
		this.ulShowUnit = new Infragistics.Win.Misc.UltraLabel();
		this.ulShowPccesCode = new Infragistics.Win.Misc.UltraLabel();
		this.ulShowName = new Infragistics.Win.Misc.UltraLabel();
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).BeginInit();
		this.panel1Bottom.SuspendLayout();
		this.panelTop.SuspendLayout();
		base.SuspendLayout();
		this.ulPccesCode.Font = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ulPccesCode.Location = new System.Drawing.Point(8, 38);
		this.ulPccesCode.Name = "ulPccesCode";
		this.ulPccesCode.Size = new System.Drawing.Size(90, 20);
		this.ulPccesCode.TabIndex = 25;
		this.ulPccesCode.Text = "工項代碼：";
		this.GridUnit1._ExcelFileName = "";
		this.GridUnit1._ExcelSheeName = "";
		this.GridUnit1._IsOpenExcelAfterExport = false;
		this.GridUnit1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridUnit1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.GridUnit1.ColumnInfo = resources.GetString("GridUnit1.ColumnInfo");
		this.GridUnit1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridUnit1.ExtendLastCol = true;
		this.GridUnit1.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
		this.GridUnit1.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridUnit1.ForeColor = System.Drawing.Color.Black;
		this.GridUnit1.Location = new System.Drawing.Point(0, 68);
		this.GridUnit1.Name = "GridUnit1";
		this.GridUnit1.Rows.Count = 1;
		this.GridUnit1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.GridUnit1.ShowCursor = true;
		this.GridUnit1.ShowToolTipOnNarrowColumn = true;
		this.GridUnit1.Size = new System.Drawing.Size(495, 274);
		this.GridUnit1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("GridUnit1.Styles"));
		this.GridUnit1.TabIndex = 4;
		this.GridUnit1.Tree.Column = 1;
		this.GridUnit1.Tree.LineColor = System.Drawing.Color.Gray;
		this.ulName.Font = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ulName.Location = new System.Drawing.Point(8, 12);
		this.ulName.Name = "ulName";
		this.ulName.Size = new System.Drawing.Size(90, 20);
		this.ulName.TabIndex = 25;
		this.ulName.Text = "工項名稱：";
		this.ulUnit.Font = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ulUnit.Location = new System.Drawing.Point(320, 38);
		this.ulUnit.Name = "ulUnit";
		this.ulUnit.Size = new System.Drawing.Size(60, 20);
		this.ulUnit.TabIndex = 26;
		this.ulUnit.Text = "單位：";
		this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnOK.Appearance = appearance2;
		this.btnOK.BackColor = System.Drawing.SystemColors.Control;
		this.btnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnOK.Font = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.btnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.btnOK.Location = new System.Drawing.Point(404, 3);
		this.btnOK.Name = "btnOK";
		this.btnOK.ShowFocusRect = false;
		this.btnOK.ShowOutline = false;
		this.btnOK.Size = new System.Drawing.Size(88, 31);
		this.btnOK.SupportThemes = false;
		this.btnOK.TabIndex = 5;
		this.btnOK.Text = "關閉";
		this.panel1Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1Bottom.Controls.Add(this.btnOK);
		this.panel1Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1Bottom.Location = new System.Drawing.Point(0, 342);
		this.panel1Bottom.Name = "panel1Bottom";
		this.panel1Bottom.Size = new System.Drawing.Size(495, 36);
		this.panel1Bottom.TabIndex = 7;
		this.panelTop.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panelTop.Controls.Add(this.ulShowUnit);
		this.panelTop.Controls.Add(this.ulShowPccesCode);
		this.panelTop.Controls.Add(this.ulShowName);
		this.panelTop.Controls.Add(this.ulUnit);
		this.panelTop.Controls.Add(this.ulPccesCode);
		this.panelTop.Controls.Add(this.ulName);
		this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
		this.panelTop.Location = new System.Drawing.Point(0, 0);
		this.panelTop.Name = "panelTop";
		this.panelTop.Size = new System.Drawing.Size(495, 68);
		this.panelTop.TabIndex = 8;
		this.ulShowUnit.Font = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ulShowUnit.Location = new System.Drawing.Point(372, 38);
		this.ulShowUnit.Name = "ulShowUnit";
		this.ulShowUnit.Size = new System.Drawing.Size(93, 20);
		this.ulShowUnit.TabIndex = 29;
		this.ulShowUnit.Text = "單位";
		this.ulShowPccesCode.Font = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ulShowPccesCode.Location = new System.Drawing.Point(90, 38);
		this.ulShowPccesCode.Name = "ulShowPccesCode";
		this.ulShowPccesCode.Size = new System.Drawing.Size(170, 20);
		this.ulShowPccesCode.TabIndex = 28;
		this.ulShowPccesCode.Text = "工項代碼";
		this.ulShowName.Font = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ulShowName.Location = new System.Drawing.Point(90, 12);
		this.ulShowName.Name = "ulShowName";
		this.ulShowName.Size = new System.Drawing.Size(388, 20);
		this.ulShowName.TabIndex = 27;
		this.ulShowName.Text = "工項名稱";
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(495, 378);
		base.Controls.Add(this.GridUnit1);
		base.Controls.Add(this.panel1Bottom);
		base.Controls.Add(this.panelTop);
		base.Name = "FormBudgetWorkItemChangeHistory";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "歷次變更紀錄查詢";
		base.Load += new System.EventHandler(FormBudgetWorkItemChangeHistory_Load);
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).EndInit();
		this.panel1Bottom.ResumeLayout(false);
		this.panelTop.ResumeLayout(false);
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
}
