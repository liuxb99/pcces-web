using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.PccesMain.Budget;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.ArchControls.ProjectInfoSummaryControls;

public class Tunnel : SummaryControlBase
{
	private IContainer components = null;

	private Label label9;

	private Label label8;

	private Label label7;

	private Label label6;

	private Label label5;

	private Label label4;

	private Label label3;

	private Label label1;

	private UltraTextEditor CostPerLane;

	private UltraTextEditor CostPerMeter;

	private UltraTextEditor CostPerSquareMeter;

	private UltraTextEditor LaneNumber;

	private UltraTextEditor BackfillAmount;

	private UltraTextEditor ExcavationAmount;

	private UltraTextEditor ClearWidth;

	private UltraTextEditor TotalLength;

	private Label lbCostPerLane;

	private Label lbCostPerMeter;

	private Label lbCostPerSquareMeter;

	private Label lbLaneNumber;

	private Label lbBackfillAmount;

	private Label lbExcavationAmount;

	private Label lbClearWidth;

	private Label lbTotalLength;

	private UltraTextEditor Method;

	private Label lbMethod;

	private UltraComboEditor Material;

	private UltraComboEditor RoadType;

	private Label label10;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	public Tunnel(FormBudgetProjectInfo budgetProjectInfo)
	{
		InitializeComponent();
		ControlDataSet.ReadXmlSchema(XSDFileDirectory + "TunnelDataSet.xsd");
		base.budgetProjectInfo = budgetProjectInfo;
	}

	public override bool IsRequiredFilled()
	{
		if (RoadType.SelectedIndex == -1 || Material.SelectedIndex == -1 || Method.Text == string.Empty || TotalLength.Text == string.Empty || ClearWidth.Text == string.Empty || ExcavationAmount.Text == string.Empty || BackfillAmount.Text == string.Empty || LaneNumber.Text == string.Empty)
		{
			return false;
		}
		return true;
	}

	protected override void ReCalculate(object sender, EventArgs e)
	{
		double dTotalLength = String2Double(TotalLength.Text);
		double dClearWidth = String2Double(ClearWidth.Text);
		double dLaneNumber = String2Double(LaneNumber.Text);
		CostPerSquareMeter.Text = Convert.ToString(PubTools.ARound(F_Amount / dTotalLength / dClearWidth, 2L));
		CostPerMeter.Text = Convert.ToString(PubTools.ARound(F_Amount / dTotalLength, 2L));
		CostPerLane.Text = Convert.ToString(PubTools.ARound(F_Amount / dTotalLength / dLaneNumber, 2L));
		if (budgetProjectInfo != null)
		{
			budgetProjectInfo.UpdateProjectScopeUnit("元/M2");
			budgetProjectInfo.UpdateProjectScopeValue(CostPerSquareMeter.Text);
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
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem10 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem11 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem12 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem13 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem14 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem15 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem16 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem17 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem18 = new Infragistics.Win.ValueListItem();
		this.label9 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.CostPerLane = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.CostPerMeter = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.CostPerSquareMeter = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.LaneNumber = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.BackfillAmount = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ExcavationAmount = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ClearWidth = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.TotalLength = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lbCostPerLane = new System.Windows.Forms.Label();
		this.lbCostPerMeter = new System.Windows.Forms.Label();
		this.lbCostPerSquareMeter = new System.Windows.Forms.Label();
		this.lbLaneNumber = new System.Windows.Forms.Label();
		this.lbBackfillAmount = new System.Windows.Forms.Label();
		this.lbExcavationAmount = new System.Windows.Forms.Label();
		this.lbClearWidth = new System.Windows.Forms.Label();
		this.lbTotalLength = new System.Windows.Forms.Label();
		this.Method = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lbMethod = new System.Windows.Forms.Label();
		this.Material = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.RoadType = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.label10 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		((System.ComponentModel.ISupportInitialize)base.ControlDataSet).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.CostPerLane).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.CostPerMeter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.CostPerSquareMeter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.LaneNumber).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.BackfillAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ExcavationAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ClearWidth).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TotalLength).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Method).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Material).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.RoadType).BeginInit();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(399, 250);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(57, 12);
		this.label9.TabIndex = 70;
		this.label9.Text = "元/車道/M";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(399, 222);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(54, 12);
		this.label8.TabIndex = 69;
		this.label8.Text = "元/M隧道";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(399, 193);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(36, 12);
		this.label7.TabIndex = 68;
		this.label7.Text = "元/M2";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(399, 164);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(17, 12);
		this.label6.TabIndex = 67;
		this.label6.Text = "道";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(399, 135);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(21, 12);
		this.label5.TabIndex = 66;
		this.label5.Text = "M2";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(401, 48);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(15, 12);
		this.label4.TabIndex = 65;
		this.label4.Text = "M";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(399, 106);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(21, 12);
		this.label3.TabIndex = 64;
		this.label3.Text = "M2";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(401, 77);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(15, 12);
		this.label1.TabIndex = 63;
		this.label1.Text = "M";
		appearance10.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.CostPerLane.Appearance = appearance10;
		this.CostPerLane.AutoSize = true;
		this.CostPerLane.Location = new System.Drawing.Point(177, 245);
		this.CostPerLane.MaxLength = 10;
		this.CostPerLane.Name = "CostPerLane";
		this.CostPerLane.ReadOnly = true;
		this.CostPerLane.Size = new System.Drawing.Size(216, 21);
		this.CostPerLane.TabIndex = 62;
		appearance11.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.CostPerMeter.Appearance = appearance11;
		this.CostPerMeter.AutoSize = true;
		this.CostPerMeter.Location = new System.Drawing.Point(177, 216);
		this.CostPerMeter.MaxLength = 10;
		this.CostPerMeter.Name = "CostPerMeter";
		this.CostPerMeter.ReadOnly = true;
		this.CostPerMeter.Size = new System.Drawing.Size(216, 21);
		this.CostPerMeter.TabIndex = 61;
		appearance12.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.CostPerSquareMeter.Appearance = appearance12;
		this.CostPerSquareMeter.AutoSize = true;
		this.CostPerSquareMeter.Location = new System.Drawing.Point(177, 187);
		this.CostPerSquareMeter.MaxLength = 10;
		this.CostPerSquareMeter.Name = "CostPerSquareMeter";
		this.CostPerSquareMeter.ReadOnly = true;
		this.CostPerSquareMeter.Size = new System.Drawing.Size(216, 21);
		this.CostPerSquareMeter.TabIndex = 60;
		appearance13.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.LaneNumber.Appearance = appearance13;
		this.LaneNumber.AutoSize = true;
		this.LaneNumber.Location = new System.Drawing.Point(177, 158);
		this.LaneNumber.MaxLength = 10;
		this.LaneNumber.Name = "LaneNumber";
		this.LaneNumber.Size = new System.Drawing.Size(216, 21);
		this.LaneNumber.TabIndex = 59;
		this.LaneNumber.ValueChanged += new System.EventHandler(ReCalculate);
		appearance14.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BackfillAmount.Appearance = appearance14;
		this.BackfillAmount.AutoSize = true;
		this.BackfillAmount.Location = new System.Drawing.Point(177, 129);
		this.BackfillAmount.MaxLength = 10;
		this.BackfillAmount.Name = "BackfillAmount";
		this.BackfillAmount.Size = new System.Drawing.Size(216, 21);
		this.BackfillAmount.TabIndex = 58;
		this.BackfillAmount.ValueChanged += new System.EventHandler(ReCalculate);
		appearance15.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ExcavationAmount.Appearance = appearance15;
		this.ExcavationAmount.AutoSize = true;
		this.ExcavationAmount.Location = new System.Drawing.Point(177, 100);
		this.ExcavationAmount.MaxLength = 10;
		this.ExcavationAmount.Name = "ExcavationAmount";
		this.ExcavationAmount.Size = new System.Drawing.Size(216, 21);
		this.ExcavationAmount.TabIndex = 57;
		this.ExcavationAmount.ValueChanged += new System.EventHandler(ReCalculate);
		appearance16.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ClearWidth.Appearance = appearance16;
		this.ClearWidth.AutoSize = true;
		this.ClearWidth.Location = new System.Drawing.Point(177, 71);
		this.ClearWidth.MaxLength = 10;
		this.ClearWidth.Name = "ClearWidth";
		this.ClearWidth.Size = new System.Drawing.Size(216, 21);
		this.ClearWidth.TabIndex = 56;
		this.ClearWidth.ValueChanged += new System.EventHandler(ReCalculate);
		appearance17.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.TotalLength.Appearance = appearance17;
		this.TotalLength.AutoSize = true;
		this.TotalLength.Location = new System.Drawing.Point(177, 42);
		this.TotalLength.MaxLength = 10;
		this.TotalLength.Name = "TotalLength";
		this.TotalLength.Size = new System.Drawing.Size(216, 21);
		this.TotalLength.TabIndex = 55;
		this.TotalLength.ValueChanged += new System.EventHandler(ReCalculate);
		this.lbCostPerLane.AutoSize = true;
		this.lbCostPerLane.Location = new System.Drawing.Point(13, 250);
		this.lbCostPerLane.Name = "lbCostPerLane";
		this.lbCostPerLane.Size = new System.Drawing.Size(143, 12);
		this.lbCostPerLane.TabIndex = 54;
		this.lbCostPerLane.Text = "單位造價(計畫總經費/A/C)";
		this.lbCostPerMeter.AutoSize = true;
		this.lbCostPerMeter.Location = new System.Drawing.Point(13, 221);
		this.lbCostPerMeter.Name = "lbCostPerMeter";
		this.lbCostPerMeter.Size = new System.Drawing.Size(132, 12);
		this.lbCostPerMeter.TabIndex = 53;
		this.lbCostPerMeter.Text = "單位造價(計畫總經費/A)";
		this.lbCostPerSquareMeter.AutoSize = true;
		this.lbCostPerSquareMeter.Location = new System.Drawing.Point(13, 192);
		this.lbCostPerSquareMeter.Name = "lbCostPerSquareMeter";
		this.lbCostPerSquareMeter.Size = new System.Drawing.Size(143, 12);
		this.lbCostPerSquareMeter.TabIndex = 52;
		this.lbCostPerSquareMeter.Text = "單位造價(計畫總經費/A/B)";
		this.lbLaneNumber.AutoSize = true;
		this.lbLaneNumber.Location = new System.Drawing.Point(13, 163);
		this.lbLaneNumber.Name = "lbLaneNumber";
		this.lbLaneNumber.Size = new System.Drawing.Size(69, 12);
		this.lbLaneNumber.TabIndex = 51;
		this.lbLaneNumber.Text = "車道數量(C)";
		this.lbBackfillAmount.AutoSize = true;
		this.lbBackfillAmount.Location = new System.Drawing.Point(13, 134);
		this.lbBackfillAmount.Name = "lbBackfillAmount";
		this.lbBackfillAmount.Size = new System.Drawing.Size(53, 12);
		this.lbBackfillAmount.TabIndex = 50;
		this.lbBackfillAmount.Text = "總填方量";
		this.lbExcavationAmount.AutoSize = true;
		this.lbExcavationAmount.Location = new System.Drawing.Point(13, 105);
		this.lbExcavationAmount.Name = "lbExcavationAmount";
		this.lbExcavationAmount.Size = new System.Drawing.Size(53, 12);
		this.lbExcavationAmount.TabIndex = 49;
		this.lbExcavationAmount.Text = "總挖方量";
		this.lbClearWidth.AutoSize = true;
		this.lbClearWidth.Location = new System.Drawing.Point(13, 76);
		this.lbClearWidth.Name = "lbClearWidth";
		this.lbClearWidth.Size = new System.Drawing.Size(69, 12);
		this.lbClearWidth.TabIndex = 48;
		this.lbClearWidth.Text = "隧道淨寬(B)";
		this.lbTotalLength.AutoSize = true;
		this.lbTotalLength.Location = new System.Drawing.Point(13, 47);
		this.lbTotalLength.Name = "lbTotalLength";
		this.lbTotalLength.Size = new System.Drawing.Size(69, 12);
		this.lbTotalLength.TabIndex = 47;
		this.lbTotalLength.Text = "隧道總長(A)";
		appearance18.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.Method.Appearance = appearance18;
		this.Method.AutoSize = true;
		this.Method.Location = new System.Drawing.Point(177, 13);
		this.Method.Name = "Method";
		this.Method.Size = new System.Drawing.Size(216, 21);
		this.Method.TabIndex = 45;
		this.lbMethod.AutoSize = true;
		this.lbMethod.Location = new System.Drawing.Point(13, 18);
		this.lbMethod.Name = "lbMethod";
		this.lbMethod.Size = new System.Drawing.Size(53, 12);
		this.lbMethod.TabIndex = 44;
		this.lbMethod.Text = "使用工法";
		this.Material.AutoSize = true;
		valueListItem10.DataValue = "瀝青混凝土(AC)";
		valueListItem10.DisplayText = "瀝青混凝土(AC)";
		valueListItem11.DataValue = "鋼筋混凝土(RC)";
		valueListItem11.DisplayText = "鋼筋混凝土(RC)";
		valueListItem12.DataValue = "其他";
		this.Material.Items.Add(valueListItem10);
		this.Material.Items.Add(valueListItem11);
		this.Material.Items.Add(valueListItem12);
		this.Material.Location = new System.Drawing.Point(272, 14);
		this.Material.Name = "Material";
		this.Material.Size = new System.Drawing.Size(121, 21);
		this.Material.TabIndex = 91;
		this.Material.Text = null;
		this.RoadType.AutoSize = true;
		valueListItem13.DataValue = "國道";
		valueListItem13.DisplayText = "國道";
		valueListItem14.DataValue = "省道";
		valueListItem14.DisplayText = "省道";
		valueListItem15.DataValue = "快速道路";
		valueListItem15.DisplayText = "快速道路";
		valueListItem16.DataValue = "縣道";
		valueListItem16.DisplayText = "縣道";
		valueListItem17.DataValue = "一般市區道路";
		valueListItem17.DisplayText = "一般市區道路";
		valueListItem18.DataValue = "其他";
		this.RoadType.Items.Add(valueListItem13);
		this.RoadType.Items.Add(valueListItem14);
		this.RoadType.Items.Add(valueListItem15);
		this.RoadType.Items.Add(valueListItem16);
		this.RoadType.Items.Add(valueListItem17);
		this.RoadType.Items.Add(valueListItem18);
		this.RoadType.Location = new System.Drawing.Point(135, 14);
		this.RoadType.Name = "RoadType";
		this.RoadType.Size = new System.Drawing.Size(121, 21);
		this.RoadType.TabIndex = 90;
		this.RoadType.Text = null;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(13, 18);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(53, 12);
		this.label10.TabIndex = 89;
		this.label10.Text = "道路類型";
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.Material);
		this.groupBox1.Controls.Add(this.RoadType);
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(507, 41);
		this.groupBox1.TabIndex = 92;
		this.groupBox1.TabStop = false;
		this.groupBox2.Controls.Add(this.lbMethod);
		this.groupBox2.Controls.Add(this.Method);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.lbTotalLength);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.lbClearWidth);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.lbExcavationAmount);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.lbBackfillAmount);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.lbLaneNumber);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.lbCostPerSquareMeter);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.lbCostPerMeter);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.lbCostPerLane);
		this.groupBox2.Controls.Add(this.CostPerLane);
		this.groupBox2.Controls.Add(this.TotalLength);
		this.groupBox2.Controls.Add(this.CostPerMeter);
		this.groupBox2.Controls.Add(this.ClearWidth);
		this.groupBox2.Controls.Add(this.CostPerSquareMeter);
		this.groupBox2.Controls.Add(this.ExcavationAmount);
		this.groupBox2.Controls.Add(this.LaneNumber);
		this.groupBox2.Controls.Add(this.BackfillAmount);
		this.groupBox2.Location = new System.Drawing.Point(0, 41);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(507, 272);
		this.groupBox2.TabIndex = 93;
		this.groupBox2.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "Tunnel";
		base.Size = new System.Drawing.Size(676, 356);
		((System.ComponentModel.ISupportInitialize)base.ControlDataSet).EndInit();
		((System.ComponentModel.ISupportInitialize)this.CostPerLane).EndInit();
		((System.ComponentModel.ISupportInitialize)this.CostPerMeter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.CostPerSquareMeter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.LaneNumber).EndInit();
		((System.ComponentModel.ISupportInitialize)this.BackfillAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ExcavationAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ClearWidth).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TotalLength).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Method).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Material).EndInit();
		((System.ComponentModel.ISupportInitialize)this.RoadType).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}
}
