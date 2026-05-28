using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.DomainModule.Coms;
using Archnowledge.Pcces.PccesMain.ArchControls;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinStatusBar;

namespace Archnowledge.Pcces.PccesMain.Budget.BudgetChange;

public class FormBudgetChangeInfoPicker : Form
{
	private IContainer components = null;

	private UltraButton btnCancel;

	private UltraButton btnOK;

	private Panel panelGrid;

	private GridBudget gridBudgetChangeInfo;

	private UltraStatusBar statusBar;

	private GroupBox gbButtons;

	private Panel panelTop;

	private Panel panelBottom;

	private ImageList imageList;

	private Label lbDescription;

	private string projectCode;

	public string Purpose;

	public string Reason;

	public string Description;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.BudgetChange.FormBudgetChangeInfoPicker));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		this.btnCancel = new Infragistics.Win.Misc.UltraButton();
		this.btnOK = new Infragistics.Win.Misc.UltraButton();
		this.panelGrid = new System.Windows.Forms.Panel();
		this.gridBudgetChangeInfo = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.statusBar = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.gbButtons = new System.Windows.Forms.GroupBox();
		this.panelTop = new System.Windows.Forms.Panel();
		this.panelBottom = new System.Windows.Forms.Panel();
		this.imageList = new System.Windows.Forms.ImageList(this.components);
		this.lbDescription = new System.Windows.Forms.Label();
		this.panelGrid.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridBudgetChangeInfo).BeginInit();
		this.panelTop.SuspendLayout();
		this.panelBottom.SuspendLayout();
		base.SuspendLayout();
		this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnCancel.Appearance = appearance1;
		this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
		this.btnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Font = new System.Drawing.Font("細明體", 11f);
		this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btnCancel.Location = new System.Drawing.Point(545, 10);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.ShowFocusRect = false;
		this.btnCancel.ShowOutline = false;
		this.btnCancel.Size = new System.Drawing.Size(88, 31);
		this.btnCancel.SupportThemes = false;
		this.btnCancel.TabIndex = 5;
		this.btnCancel.Text = "取消";
		this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.ImageHAlign = Infragistics.Win.HAlign.Left;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnOK.Appearance = appearance2;
		this.btnOK.BackColor = System.Drawing.SystemColors.Control;
		this.btnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOK.Font = new System.Drawing.Font("細明體", 11f);
		this.btnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.btnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.btnOK.Location = new System.Drawing.Point(453, 10);
		this.btnOK.Name = "btnOK";
		this.btnOK.ShowFocusRect = false;
		this.btnOK.ShowOutline = false;
		this.btnOK.Size = new System.Drawing.Size(88, 31);
		this.btnOK.SupportThemes = false;
		this.btnOK.TabIndex = 4;
		this.btnOK.Text = "確定";
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.panelGrid.Controls.Add(this.gridBudgetChangeInfo);
		this.panelGrid.Controls.Add(this.statusBar);
		this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panelGrid.Location = new System.Drawing.Point(0, 54);
		this.panelGrid.Name = "panelGrid";
		this.panelGrid.Size = new System.Drawing.Size(638, 330);
		this.panelGrid.TabIndex = 18;
		this.gridBudgetChangeInfo._ExcelFileName = "";
		this.gridBudgetChangeInfo._ExcelSheeName = "";
		this.gridBudgetChangeInfo._IsOpenExcelAfterExport = false;
		this.gridBudgetChangeInfo.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridBudgetChangeInfo.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridBudgetChangeInfo.ColumnInfo = resources.GetString("gridBudgetChangeInfo.ColumnInfo");
		this.gridBudgetChangeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridBudgetChangeInfo.ExtendLastCol = true;
		this.gridBudgetChangeInfo.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridBudgetChangeInfo.ForeColor = System.Drawing.Color.Black;
		this.gridBudgetChangeInfo.Location = new System.Drawing.Point(0, 0);
		this.gridBudgetChangeInfo.Name = "gridBudgetChangeInfo";
		this.gridBudgetChangeInfo.Rows.Count = 1;
		this.gridBudgetChangeInfo.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridBudgetChangeInfo.ShowCursor = true;
		this.gridBudgetChangeInfo.ShowSort = false;
		this.gridBudgetChangeInfo.ShowToolTipOnNarrowColumn = true;
		this.gridBudgetChangeInfo.Size = new System.Drawing.Size(638, 304);
		this.gridBudgetChangeInfo.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridBudgetChangeInfo.Styles"));
		this.gridBudgetChangeInfo.TabIndex = 1;
		this.gridBudgetChangeInfo.Tree.Column = 1;
		this.gridBudgetChangeInfo.Tree.LineColor = System.Drawing.Color.Gray;
		appearance3.FontData.SizeInPoints = 11f;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.statusBar.Appearance = appearance3;
		this.statusBar.Location = new System.Drawing.Point(0, 304);
		this.statusBar.Name = "statusBar";
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		appearance4.TextHAlign = Infragistics.Win.HAlign.Right;
		ultraStatusPanel2.Appearance = appearance4;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.Text = "客服電話：(02)2716-5561";
		ultraStatusPanel2.Width = 200;
		this.statusBar.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[2] { ultraStatusPanel1, ultraStatusPanel2 });
		this.statusBar.Size = new System.Drawing.Size(638, 26);
		this.statusBar.TabIndex = 2;
		this.gbButtons.Dock = System.Windows.Forms.DockStyle.Top;
		this.gbButtons.Location = new System.Drawing.Point(0, 0);
		this.gbButtons.Name = "gbButtons";
		this.gbButtons.Size = new System.Drawing.Size(638, 8);
		this.gbButtons.TabIndex = 3;
		this.gbButtons.TabStop = false;
		this.panelTop.BackColor = System.Drawing.Color.White;
		this.panelTop.Controls.Add(this.lbDescription);
		this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
		this.panelTop.Location = new System.Drawing.Point(0, 0);
		this.panelTop.Name = "panelTop";
		this.panelTop.Size = new System.Drawing.Size(638, 54);
		this.panelTop.TabIndex = 17;
		this.panelBottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panelBottom.Controls.Add(this.btnCancel);
		this.panelBottom.Controls.Add(this.btnOK);
		this.panelBottom.Controls.Add(this.gbButtons);
		this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panelBottom.Location = new System.Drawing.Point(0, 384);
		this.panelBottom.Name = "panelBottom";
		this.panelBottom.Size = new System.Drawing.Size(638, 44);
		this.panelBottom.TabIndex = 16;
		this.imageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList.ImageStream");
		this.imageList.TransparentColor = System.Drawing.Color.White;
		this.imageList.Images.SetKeyName(0, "");
		this.lbDescription.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lbDescription.Location = new System.Drawing.Point(12, 13);
		this.lbDescription.Name = "lbDescription";
		this.lbDescription.Size = new System.Drawing.Size(267, 22);
		this.lbDescription.TabIndex = 0;
		this.lbDescription.Text = "請挑選已核可之預算變更申請。";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(638, 428);
		base.Controls.Add(this.panelGrid);
		base.Controls.Add(this.panelTop);
		base.Controls.Add(this.panelBottom);
		base.Name = "FormBudgetChangeInfoPicker";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		this.Text = "已核可預算變更";
		base.Load += new System.EventHandler(FormBudgetChangeInfoPicker_Load);
		this.panelGrid.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridBudgetChangeInfo).EndInit();
		this.panelTop.ResumeLayout(false);
		this.panelBottom.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public FormBudgetChangeInfoPicker(string projectCode)
	{
		InitializeComponent();
		this.projectCode = projectCode;
	}

	private void FormBudgetChangeInfoPicker_Load(object sender, EventArgs e)
	{
		ExecResult ER = new ExecResult();
		CtrServiceHelper ctrServiceHelper = new CtrServiceHelper();
		DataSet dsBudChgApply = ctrServiceHelper.GetBudChgApplyByProjectCode(projectCode, out ER);
		if (ER.ReturnCode != 0)
		{
			MessageBox.Show(ER.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			base.DialogResult = DialogResult.Abort;
		}
		DataRowCollection drsBudChgApply = dsBudChgApply.Tables[0].Rows;
		gridBudgetChangeInfo.Rows.Count = drsBudChgApply.Count + 1;
		for (int rowIndex = 0; rowIndex < drsBudChgApply.Count; rowIndex++)
		{
			Row gridRow = gridBudgetChangeInfo.Rows[rowIndex + 1];
			DataRow drBudgetChangeInfo = drsBudChgApply[rowIndex];
			gridRow["Num"] = drBudgetChangeInfo["Num"];
			gridRow["ChangTitle"] = drBudgetChangeInfo["ChangTitle"];
			gridRow["EffectReason"] = drBudgetChangeInfo["EffectReason"];
			gridRow["CMemo"] = drBudgetChangeInfo["CMemo"];
		}
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		if (gridBudgetChangeInfo.Rows.Selected.Count > 0)
		{
			Row selectedRow = gridBudgetChangeInfo.Rows.Selected[0];
			Purpose = ArchConvert.Obj2String(selectedRow["ChangTitle"]);
			Reason = ArchConvert.Obj2String(selectedRow["EffectReason"]);
			Description = ArchConvert.Obj2String(selectedRow["CMemo"]);
			base.DialogResult = DialogResult.OK;
		}
		else
		{
			MessageBox.Show("尚未挑選任一項目！");
		}
	}
}
