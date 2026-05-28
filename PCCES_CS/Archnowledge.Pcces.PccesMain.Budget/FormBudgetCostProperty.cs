using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.Bid;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.DomainModule.CostStructure;
using Archnowledge.Pcces.DomainModule.LogicalBase;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetCostProperty : Form
{
	private IContainer components = null;

	private Panel panel2;

	private Panel panel3;

	private Label lbCostUID;

	private Label label1;

	private Label lbCoststructureName;

	private Label lbProperty1;

	private Label lbProperty2;

	private Label lbProperty3;

	private Label label2;

	private UltraTextEditor tbProperty1;

	private UltraTextEditor tbProperty2;

	private UltraTextEditor tbProperty3;

	private Label label3;

	private Label lbCostUnit;

	private Label label4;

	private Label lbMemo;

	private Panel panel9;

	private GroupBox gbButtons;

	private UltraButton btnCancel;

	private UltraButton btnOK;

	private string UserID;

	private DataSet dsItemA;

	private string ProjectCode;

	private PccesFormAction FormActionName;

	private string CostStructureUID;

	private string CostStructureType;

	private int SNo;

	private CostStructure costStructure = new CostStructure();

	private ItemA itemA = new ItemA();

	public string _UserID
	{
		set
		{
			UserID = value;
		}
	}

	public string _ProjectCode
	{
		set
		{
			ProjectCode = value;
		}
	}

	public string _CostType
	{
		set
		{
			CostStructureType = value;
		}
	}

	public int _sNO
	{
		set
		{
			SNo = value;
		}
	}

	public PccesFormAction _ActionName
	{
		set
		{
			FormActionName = value;
		}
	}

	public string _CostUID
	{
		set
		{
			CostStructureUID = value;
		}
	}

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetCostProperty));
		Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
		this.panel2 = new System.Windows.Forms.Panel();
		this.lbMemo = new System.Windows.Forms.Label();
		this.lbCostUnit = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.tbProperty3 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tbProperty2 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tbProperty1 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lbCoststructureName = new System.Windows.Forms.Label();
		this.lbProperty3 = new System.Windows.Forms.Label();
		this.lbProperty2 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.lbProperty1 = new System.Windows.Forms.Label();
		this.lbCostUID = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.panel3 = new System.Windows.Forms.Panel();
		this.panel9 = new System.Windows.Forms.Panel();
		this.gbButtons = new System.Windows.Forms.GroupBox();
		this.btnCancel = new Infragistics.Win.Misc.UltraButton();
		this.btnOK = new Infragistics.Win.Misc.UltraButton();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.tbProperty3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbProperty2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbProperty1).BeginInit();
		this.panel3.SuspendLayout();
		this.panel9.SuspendLayout();
		base.SuspendLayout();
		this.panel2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel2.Controls.Add(this.lbMemo);
		this.panel2.Controls.Add(this.lbCostUnit);
		this.panel2.Controls.Add(this.label4);
		this.panel2.Controls.Add(this.label3);
		this.panel2.Controls.Add(this.tbProperty3);
		this.panel2.Controls.Add(this.tbProperty2);
		this.panel2.Controls.Add(this.tbProperty1);
		this.panel2.Controls.Add(this.lbCoststructureName);
		this.panel2.Controls.Add(this.lbProperty3);
		this.panel2.Controls.Add(this.lbProperty2);
		this.panel2.Controls.Add(this.label2);
		this.panel2.Controls.Add(this.lbProperty1);
		this.panel2.Controls.Add(this.lbCostUID);
		this.panel2.Controls.Add(this.label1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(452, 261);
		this.panel2.TabIndex = 1;
		this.lbMemo.AutoSize = true;
		this.lbMemo.Location = new System.Drawing.Point(166, 222);
		this.lbMemo.Name = "lbMemo";
		this.lbMemo.Size = new System.Drawing.Size(0, 15);
		this.lbMemo.TabIndex = 13;
		this.lbCostUnit.AutoSize = true;
		this.lbCostUnit.Location = new System.Drawing.Point(166, 189);
		this.lbCostUnit.Name = "lbCostUnit";
		this.lbCostUnit.Size = new System.Drawing.Size(0, 15);
		this.lbCostUnit.TabIndex = 12;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(15, 189);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(71, 15);
		this.label4.TabIndex = 11;
		this.label4.Text = "成本單位";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(15, 222);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(39, 15);
		this.label3.TabIndex = 9;
		this.label3.Text = "備註";
		this.tbProperty3.AutoSize = true;
		this.tbProperty3.Location = new System.Drawing.Point(168, 154);
		this.tbProperty3.Name = "tbProperty3";
		this.tbProperty3.Size = new System.Drawing.Size(258, 21);
		this.tbProperty3.TabIndex = 3;
		this.tbProperty2.AutoSize = true;
		this.tbProperty2.Location = new System.Drawing.Point(168, 120);
		this.tbProperty2.Name = "tbProperty2";
		this.tbProperty2.Size = new System.Drawing.Size(258, 21);
		this.tbProperty2.TabIndex = 2;
		this.tbProperty1.AutoSize = true;
		this.tbProperty1.Location = new System.Drawing.Point(168, 86);
		this.tbProperty1.Name = "tbProperty1";
		this.tbProperty1.Size = new System.Drawing.Size(258, 21);
		this.tbProperty1.TabIndex = 1;
		this.lbCoststructureName.AutoSize = true;
		this.lbCoststructureName.Location = new System.Drawing.Point(166, 57);
		this.lbCoststructureName.Name = "lbCoststructureName";
		this.lbCoststructureName.Size = new System.Drawing.Size(0, 15);
		this.lbCoststructureName.TabIndex = 3;
		this.lbProperty3.AutoSize = true;
		this.lbProperty3.Location = new System.Drawing.Point(15, 156);
		this.lbProperty3.Name = "lbProperty3";
		this.lbProperty3.Size = new System.Drawing.Size(95, 15);
		this.lbProperty3.TabIndex = 2;
		this.lbProperty3.Text = "成本架屬性3";
		this.lbProperty2.AutoSize = true;
		this.lbProperty2.Location = new System.Drawing.Point(15, 123);
		this.lbProperty2.Name = "lbProperty2";
		this.lbProperty2.Size = new System.Drawing.Size(95, 15);
		this.lbProperty2.TabIndex = 2;
		this.lbProperty2.Text = "成本架屬性2";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(15, 57);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(135, 15);
		this.label2.TabIndex = 2;
		this.label2.Text = "成本架構項目名稱";
		this.lbProperty1.AutoSize = true;
		this.lbProperty1.Location = new System.Drawing.Point(15, 90);
		this.lbProperty1.Name = "lbProperty1";
		this.lbProperty1.Size = new System.Drawing.Size(95, 15);
		this.lbProperty1.TabIndex = 0;
		this.lbProperty1.Text = "成本架屬性1";
		this.lbCostUID.AutoSize = true;
		this.lbCostUID.Location = new System.Drawing.Point(166, 24);
		this.lbCostUID.Name = "lbCostUID";
		this.lbCostUID.Size = new System.Drawing.Size(0, 15);
		this.lbCostUID.TabIndex = 1;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(15, 24);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(103, 15);
		this.label1.TabIndex = 0;
		this.label1.Text = "成本架構編碼";
		this.panel3.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel3.Controls.Add(this.panel9);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel3.Location = new System.Drawing.Point(0, 261);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(452, 41);
		this.panel3.TabIndex = 2;
		this.panel9.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel9.Controls.Add(this.gbButtons);
		this.panel9.Controls.Add(this.btnCancel);
		this.panel9.Controls.Add(this.btnOK);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel9.Location = new System.Drawing.Point(0, -3);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(452, 44);
		this.panel9.TabIndex = 24;
		this.gbButtons.Dock = System.Windows.Forms.DockStyle.Top;
		this.gbButtons.Location = new System.Drawing.Point(0, 0);
		this.gbButtons.Name = "gbButtons";
		this.gbButtons.Size = new System.Drawing.Size(452, 8);
		this.gbButtons.TabIndex = 3;
		this.gbButtons.TabStop = false;
		this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance33.Image = resources.GetObject("appearance33.Image");
		appearance33.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnCancel.Appearance = appearance33;
		this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
		this.btnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Font = new System.Drawing.Font("細明體", 11f);
		this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btnCancel.Location = new System.Drawing.Point(356, 10);
		this.btnCancel.Name = "A1_Btn_Cncl";
		this.btnCancel.ShowFocusRect = false;
		this.btnCancel.ShowOutline = false;
		this.btnCancel.Size = new System.Drawing.Size(88, 31);
		this.btnCancel.SupportThemes = false;
		this.btnCancel.TabIndex = 2;
		this.btnCancel.TabStop = false;
		this.btnCancel.Text = "取消";
		this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance34.Image = resources.GetObject("appearance34.Image");
		appearance34.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnOK.Appearance = appearance34;
		this.btnOK.BackColor = System.Drawing.SystemColors.Control;
		this.btnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOK.Font = new System.Drawing.Font("細明體", 11f);
		this.btnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.btnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.btnOK.Location = new System.Drawing.Point(264, 10);
		this.btnOK.Name = "A1_Btn_Next";
		this.btnOK.ShowFocusRect = false;
		this.btnOK.ShowOutline = false;
		this.btnOK.Size = new System.Drawing.Size(88, 31);
		this.btnOK.SupportThemes = false;
		this.btnOK.TabIndex = 1;
		this.btnOK.TabStop = false;
		this.btnOK.Text = "確定";
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(452, 302);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel3);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormBudgetCostProperty";
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "編輯專案成本架構屬性";
		base.Load += new System.EventHandler(FormBudgetCostProperty_Load);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.tbProperty3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbProperty2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbProperty1).EndInit();
		this.panel3.ResumeLayout(false);
		this.panel9.ResumeLayout(false);
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

	public FormBudgetCostProperty()
	{
		InitializeComponent();
	}

	private void FormBudgetCostProperty_Load(object sender, EventArgs e)
	{
		if (FormActionName == PccesFormAction.BID)
		{
			tbProperty1.Enabled = false;
			tbProperty2.Enabled = false;
			tbProperty3.Enabled = false;
			itemA = new BidItemA();
		}
		else if (FormActionName == PccesFormAction.BUD)
		{
			itemA = new BudItemA();
		}
		DataToForm();
	}

	private void DataToForm()
	{
		lbCostUID.Text = CostStructureUID;
		DataSet dsCostStructure = costStructure.GetCostStructureByCostUID(CostStructureUID);
		if (dsCostStructure.Tables["CostStructure"].Rows.Count > 0)
		{
			DataRow drCostStructure = dsCostStructure.Tables["CostStructure"].Rows[0];
			lbCoststructureName.Text = drCostStructure["cName"].ToString();
			lbProperty1.Text = drCostStructure["Property1"].ToString();
			lbProperty2.Text = drCostStructure["Property2"].ToString();
			lbProperty3.Text = drCostStructure["Property3"].ToString();
			lbCostUnit.Text = drCostStructure["CostUnit"].ToString();
			lbMemo.Text = drCostStructure["Memo"].ToString();
			tbProperty1.Visible = lbProperty1.Text != string.Empty;
			tbProperty2.Visible = lbProperty2.Text != string.Empty;
			tbProperty3.Visible = lbProperty3.Text != string.Empty;
			dsItemA = itemA.GetItemABySNo(ProjectCode, SNo);
			DataRow drItemA = dsItemA.Tables["ItemA"].Rows[0];
			tbProperty1.Text = drItemA["Property1"].ToString();
			tbProperty2.Text = drItemA["Property2"].ToString();
			tbProperty3.Text = drItemA["Property3"].ToString();
		}
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		UpdateCostStructureProperties();
		base.DialogResult = DialogResult.OK;
	}

	private void UpdateCostStructureProperties()
	{
		if (FormActionName == PccesFormAction.BUD)
		{
			DataRow drItemA = dsItemA.Tables["ItemA"].Rows[0];
			drItemA["Property1"] = tbProperty1.Text;
			drItemA["Property2"] = tbProperty2.Text;
			drItemA["Property3"] = tbProperty3.Text;
			ExecResult ER = itemA.UpdateCostStructureProperties(dsItemA);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show("更新失敗！\n" + ER.Message);
			}
		}
	}
}
