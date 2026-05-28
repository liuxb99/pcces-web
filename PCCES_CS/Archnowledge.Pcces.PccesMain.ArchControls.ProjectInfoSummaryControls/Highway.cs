using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.PccesMain.Budget;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.ArchControls.ProjectInfoSummaryControls;

public class Highway : SummaryControlBase
{
	private IContainer components = null;

	private Label label6;

	private Label label5;

	private Label label4;

	private Label label3;

	private Label label1;

	private UltraTextEditor CostPerLane;

	private UltraTextEditor CostPerSquareMeter;

	private UltraTextEditor CostPerMeter;

	private UltraTextEditor LaneNumber;

	private UltraTextEditor RoadWidth;

	private Label lbCostPerLane;

	private Label lbCostPerMeter;

	private Label lbCostPerSquareMeter;

	private Label lbLaneNumber;

	private Label lbRoadWidth;

	private UltraTextEditor TotalLength;

	private Label lbTotalLength;

	private Label label2;

	private UltraComboEditor Material;

	private UltraComboEditor RoadType;

	private Label label10;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	public Highway(FormBudgetProjectInfo budgetProjectInfo)
	{
		InitializeComponent();
		ControlDataSet.ReadXmlSchema(XSDFileDirectory + "HighwayDataSet.xsd");
		base.budgetProjectInfo = budgetProjectInfo;
	}

	public override bool IsRequiredFilled()
	{
		if (RoadType.SelectedIndex == -1 || Material.SelectedIndex == -1 || TotalLength.Text == string.Empty || RoadWidth.Text == string.Empty || LaneNumber.Text == string.Empty)
		{
			return false;
		}
		return true;
	}

	protected override void ReCalculate(object sender, EventArgs e)
	{
		double dTotalLength = String2Double(TotalLength.Text);
		double dRoadWidth = String2Double(RoadWidth.Text);
		double dLaneNumber = String2Double(LaneNumber.Text);
		CostPerSquareMeter.Text = Convert.ToString(PubTools.ARound(F_Amount / dTotalLength / dRoadWidth, 2L));
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
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem28 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem29 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem30 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem31 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem32 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem33 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem34 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem35 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem36 = new Infragistics.Win.ValueListItem();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.CostPerLane = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.CostPerSquareMeter = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.CostPerMeter = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.LaneNumber = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.RoadWidth = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lbCostPerLane = new System.Windows.Forms.Label();
		this.lbCostPerMeter = new System.Windows.Forms.Label();
		this.lbCostPerSquareMeter = new System.Windows.Forms.Label();
		this.lbLaneNumber = new System.Windows.Forms.Label();
		this.lbRoadWidth = new System.Windows.Forms.Label();
		this.TotalLength = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lbTotalLength = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.Material = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.RoadType = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.label10 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		((System.ComponentModel.ISupportInitialize)base.ControlDataSet).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.CostPerLane).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.CostPerSquareMeter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.CostPerMeter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.LaneNumber).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.RoadWidth).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TotalLength).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Material).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.RoadType).BeginInit();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(399, 216);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(57, 12);
		this.label6.TabIndex = 84;
		this.label6.Text = "元/車道/M";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(399, 146);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(36, 12);
		this.label5.TabIndex = 83;
		this.label5.Text = "元/M2";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(399, 76);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(15, 12);
		this.label4.TabIndex = 82;
		this.label4.Text = "M";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(399, 181);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(54, 12);
		this.label3.TabIndex = 81;
		this.label3.Text = "元/M道路";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(399, 111);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(17, 12);
		this.label1.TabIndex = 80;
		this.label1.Text = "道";
		appearance19.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.CostPerLane.Appearance = appearance19;
		this.CostPerLane.AutoSize = true;
		this.CostPerLane.Location = new System.Drawing.Point(177, 212);
		this.CostPerLane.MaxLength = 10;
		this.CostPerLane.Name = "CostPerLane";
		this.CostPerLane.ReadOnly = true;
		this.CostPerLane.Size = new System.Drawing.Size(216, 21);
		this.CostPerLane.TabIndex = 79;
		appearance20.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.CostPerSquareMeter.Appearance = appearance20;
		this.CostPerSquareMeter.AutoSize = true;
		this.CostPerSquareMeter.Location = new System.Drawing.Point(177, 137);
		this.CostPerSquareMeter.MaxLength = 10;
		this.CostPerSquareMeter.Name = "CostPerSquareMeter";
		this.CostPerSquareMeter.ReadOnly = true;
		this.CostPerSquareMeter.Size = new System.Drawing.Size(216, 21);
		this.CostPerSquareMeter.TabIndex = 78;
		appearance21.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.CostPerMeter.Appearance = appearance21;
		this.CostPerMeter.AutoSize = true;
		this.CostPerMeter.Location = new System.Drawing.Point(177, 172);
		this.CostPerMeter.MaxLength = 10;
		this.CostPerMeter.Name = "CostPerMeter";
		this.CostPerMeter.ReadOnly = true;
		this.CostPerMeter.Size = new System.Drawing.Size(216, 21);
		this.CostPerMeter.TabIndex = 77;
		appearance22.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.LaneNumber.Appearance = appearance22;
		this.LaneNumber.AutoSize = true;
		this.LaneNumber.Location = new System.Drawing.Point(177, 107);
		this.LaneNumber.MaxLength = 10;
		this.LaneNumber.Name = "LaneNumber";
		this.LaneNumber.Size = new System.Drawing.Size(216, 21);
		this.LaneNumber.TabIndex = 76;
		this.LaneNumber.ValueChanged += new System.EventHandler(ReCalculate);
		appearance23.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.RoadWidth.Appearance = appearance23;
		this.RoadWidth.AutoSize = true;
		this.RoadWidth.Location = new System.Drawing.Point(177, 72);
		this.RoadWidth.MaxLength = 10;
		this.RoadWidth.Name = "RoadWidth";
		this.RoadWidth.Size = new System.Drawing.Size(216, 21);
		this.RoadWidth.TabIndex = 75;
		this.RoadWidth.ValueChanged += new System.EventHandler(ReCalculate);
		this.lbCostPerLane.AutoSize = true;
		this.lbCostPerLane.Location = new System.Drawing.Point(13, 217);
		this.lbCostPerLane.Name = "lbCostPerLane";
		this.lbCostPerLane.Size = new System.Drawing.Size(143, 12);
		this.lbCostPerLane.TabIndex = 74;
		this.lbCostPerLane.Text = "單位造價(計畫總經費/A/C)";
		this.lbCostPerMeter.AutoSize = true;
		this.lbCostPerMeter.Location = new System.Drawing.Point(13, 182);
		this.lbCostPerMeter.Name = "lbCostPerMeter";
		this.lbCostPerMeter.Size = new System.Drawing.Size(132, 12);
		this.lbCostPerMeter.TabIndex = 73;
		this.lbCostPerMeter.Text = "單位造價(計畫總經費/A)";
		this.lbCostPerSquareMeter.AutoSize = true;
		this.lbCostPerSquareMeter.Location = new System.Drawing.Point(13, 147);
		this.lbCostPerSquareMeter.Name = "lbCostPerSquareMeter";
		this.lbCostPerSquareMeter.Size = new System.Drawing.Size(143, 12);
		this.lbCostPerSquareMeter.TabIndex = 72;
		this.lbCostPerSquareMeter.Text = "單位造價(計畫總經費/A/B)";
		this.lbLaneNumber.AutoSize = true;
		this.lbLaneNumber.Location = new System.Drawing.Point(13, 112);
		this.lbLaneNumber.Name = "lbLaneNumber";
		this.lbLaneNumber.Size = new System.Drawing.Size(69, 12);
		this.lbLaneNumber.TabIndex = 71;
		this.lbLaneNumber.Text = "車道數量(C)";
		this.lbRoadWidth.AutoSize = true;
		this.lbRoadWidth.Location = new System.Drawing.Point(13, 77);
		this.lbRoadWidth.Name = "lbRoadWidth";
		this.lbRoadWidth.Size = new System.Drawing.Size(57, 12);
		this.lbRoadWidth.TabIndex = 70;
		this.lbRoadWidth.Text = "道路寬(B)";
		appearance24.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.TotalLength.Appearance = appearance24;
		this.TotalLength.AutoSize = true;
		this.TotalLength.Location = new System.Drawing.Point(177, 37);
		this.TotalLength.Name = "TotalLength";
		this.TotalLength.Size = new System.Drawing.Size(216, 21);
		this.TotalLength.TabIndex = 69;
		this.TotalLength.ValueChanged += new System.EventHandler(ReCalculate);
		this.lbTotalLength.AutoSize = true;
		this.lbTotalLength.Location = new System.Drawing.Point(13, 42);
		this.lbTotalLength.Name = "lbTotalLength";
		this.lbTotalLength.Size = new System.Drawing.Size(69, 12);
		this.lbTotalLength.TabIndex = 68;
		this.lbTotalLength.Text = "道路總長(A)";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(399, 41);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(15, 12);
		this.label2.TabIndex = 85;
		this.label2.Text = "M";
		this.Material.AutoSize = true;
		valueListItem28.DataValue = "瀝青混凝土(AC)";
		valueListItem28.DisplayText = "瀝青混凝土(AC)";
		valueListItem29.DataValue = "鋼筋混凝土(RC)";
		valueListItem29.DisplayText = "鋼筋混凝土(RC)";
		valueListItem30.DataValue = "其他";
		this.Material.Items.Add(valueListItem28);
		this.Material.Items.Add(valueListItem29);
		this.Material.Items.Add(valueListItem30);
		this.Material.Location = new System.Drawing.Point(272, 14);
		this.Material.Name = "Material";
		this.Material.Size = new System.Drawing.Size(121, 21);
		this.Material.TabIndex = 88;
		this.Material.Text = null;
		this.RoadType.AutoSize = true;
		valueListItem31.DataValue = "國道";
		valueListItem31.DisplayText = "國道";
		valueListItem32.DataValue = "省道";
		valueListItem32.DisplayText = "省道";
		valueListItem33.DataValue = "快速道路";
		valueListItem33.DisplayText = "快速道路";
		valueListItem34.DataValue = "縣道";
		valueListItem34.DisplayText = "縣道";
		valueListItem35.DataValue = "一般市區道路";
		valueListItem35.DisplayText = "一般市區道路";
		valueListItem36.DataValue = "其他";
		this.RoadType.Items.Add(valueListItem31);
		this.RoadType.Items.Add(valueListItem32);
		this.RoadType.Items.Add(valueListItem33);
		this.RoadType.Items.Add(valueListItem34);
		this.RoadType.Items.Add(valueListItem35);
		this.RoadType.Items.Add(valueListItem36);
		this.RoadType.Location = new System.Drawing.Point(135, 14);
		this.RoadType.Name = "RoadType";
		this.RoadType.Size = new System.Drawing.Size(121, 21);
		this.RoadType.TabIndex = 87;
		this.RoadType.Text = null;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(13, 18);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(53, 12);
		this.label10.TabIndex = 86;
		this.label10.Text = "道路類型";
		this.groupBox1.Controls.Add(this.RoadType);
		this.groupBox1.Controls.Add(this.Material);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(507, 41);
		this.groupBox1.TabIndex = 97;
		this.groupBox1.TabStop = false;
		this.groupBox2.Controls.Add(this.lbTotalLength);
		this.groupBox2.Controls.Add(this.TotalLength);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.lbRoadWidth);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.lbLaneNumber);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.lbCostPerSquareMeter);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.lbCostPerMeter);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.lbCostPerLane);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.RoadWidth);
		this.groupBox2.Controls.Add(this.CostPerLane);
		this.groupBox2.Controls.Add(this.LaneNumber);
		this.groupBox2.Controls.Add(this.CostPerSquareMeter);
		this.groupBox2.Controls.Add(this.CostPerMeter);
		this.groupBox2.Location = new System.Drawing.Point(0, 41);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(507, 272);
		this.groupBox2.TabIndex = 98;
		this.groupBox2.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "Highway";
		base.Size = new System.Drawing.Size(676, 356);
		((System.ComponentModel.ISupportInitialize)base.ControlDataSet).EndInit();
		((System.ComponentModel.ISupportInitialize)this.CostPerLane).EndInit();
		((System.ComponentModel.ISupportInitialize)this.CostPerSquareMeter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.CostPerMeter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.LaneNumber).EndInit();
		((System.ComponentModel.ISupportInitialize)this.RoadWidth).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TotalLength).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Material).EndInit();
		((System.ComponentModel.ISupportInitialize)this.RoadType).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}
}
