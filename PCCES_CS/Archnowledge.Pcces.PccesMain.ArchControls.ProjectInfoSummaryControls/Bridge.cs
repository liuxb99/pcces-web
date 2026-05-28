using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.PccesMain.Budget;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.ArchControls.ProjectInfoSummaryControls;

public class Bridge : SummaryControlBase
{
	private IContainer components = null;

	private Label label9;

	private Label label8;

	private Label label7;

	private Label label6;

	private Label label5;

	private Label label3;

	private Label label1;

	private UltraTextEditor CostPerMeter;

	private UltraTextEditor CostPerSquareMeter;

	private UltraTextEditor LaneNumber;

	private UltraTextEditor Span;

	private UltraTextEditor BridgeHeight;

	private UltraTextEditor ClearWidth;

	private UltraTextEditor TotalLength;

	private UltraTextEditor Method;

	private Label lbCostPerMeter;

	private Label lbCostPerSquareMeter;

	private Label lbLaneNumber;

	private Label lbSpan;

	private Label lbBridgeHeight;

	private Label lbClearWidth;

	private Label lbTotalLength;

	private Label lbMethod;

	private UltraTextEditor Structure;

	private Label lbStructure;

	private Label lbCostPerLane;

	private UltraTextEditor CostPerLane;

	private Label label11;

	private UltraComboEditor Material;

	private UltraComboEditor RoadType;

	private Label label10;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	public Bridge(FormBudgetProjectInfo budgetProjectInfo)
	{
		InitializeComponent();
		ControlDataSet.ReadXmlSchema(XSDFileDirectory + "BridgeDataSet.xsd");
		base.budgetProjectInfo = budgetProjectInfo;
	}

	public override bool IsRequiredFilled()
	{
		if (RoadType.SelectedIndex == -1 || Material.SelectedIndex == -1 || Structure.Text == string.Empty || Method.Text == string.Empty || TotalLength.Text == string.Empty || ClearWidth.Text == string.Empty || BridgeHeight.Text == string.Empty || Span.Text == string.Empty || LaneNumber.Text == string.Empty)
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
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
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
		this.label3 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.CostPerMeter = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.CostPerSquareMeter = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.LaneNumber = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.Span = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.BridgeHeight = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ClearWidth = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.TotalLength = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.Method = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lbCostPerMeter = new System.Windows.Forms.Label();
		this.lbCostPerSquareMeter = new System.Windows.Forms.Label();
		this.lbLaneNumber = new System.Windows.Forms.Label();
		this.lbSpan = new System.Windows.Forms.Label();
		this.lbBridgeHeight = new System.Windows.Forms.Label();
		this.lbClearWidth = new System.Windows.Forms.Label();
		this.lbTotalLength = new System.Windows.Forms.Label();
		this.lbMethod = new System.Windows.Forms.Label();
		this.Structure = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lbStructure = new System.Windows.Forms.Label();
		this.lbCostPerLane = new System.Windows.Forms.Label();
		this.CostPerLane = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.label11 = new System.Windows.Forms.Label();
		this.Material = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.RoadType = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.label10 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		((System.ComponentModel.ISupportInitialize)base.ControlDataSet).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.CostPerMeter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.CostPerSquareMeter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.LaneNumber).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Span).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.BridgeHeight).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ClearWidth).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TotalLength).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Method).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Structure).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.CostPerLane).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Material).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.RoadType).BeginInit();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(399, 219);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(54, 12);
		this.label9.TabIndex = 70;
		this.label9.Text = "元/M道路";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(399, 194);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(36, 12);
		this.label8.TabIndex = 69;
		this.label8.Text = "元/M2";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(399, 169);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(17, 12);
		this.label7.TabIndex = 68;
		this.label7.Text = "道";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(399, 144);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(15, 12);
		this.label6.TabIndex = 67;
		this.label6.Text = "M";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(399, 94);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(15, 12);
		this.label5.TabIndex = 66;
		this.label5.Text = "M";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(399, 119);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(15, 12);
		this.label3.TabIndex = 64;
		this.label3.Text = "M";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(399, 69);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(15, 12);
		this.label1.TabIndex = 63;
		this.label1.Text = "M";
		appearance11.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.CostPerMeter.Appearance = appearance11;
		this.CostPerMeter.AutoSize = true;
		this.CostPerMeter.Location = new System.Drawing.Point(177, 213);
		this.CostPerMeter.MaxLength = 10;
		this.CostPerMeter.Name = "CostPerMeter";
		this.CostPerMeter.ReadOnly = true;
		this.CostPerMeter.Size = new System.Drawing.Size(216, 21);
		this.CostPerMeter.TabIndex = 62;
		appearance12.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.CostPerSquareMeter.Appearance = appearance12;
		this.CostPerSquareMeter.AutoSize = true;
		this.CostPerSquareMeter.Location = new System.Drawing.Point(177, 188);
		this.CostPerSquareMeter.MaxLength = 10;
		this.CostPerSquareMeter.Name = "CostPerSquareMeter";
		this.CostPerSquareMeter.ReadOnly = true;
		this.CostPerSquareMeter.Size = new System.Drawing.Size(216, 21);
		this.CostPerSquareMeter.TabIndex = 61;
		appearance13.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.LaneNumber.Appearance = appearance13;
		this.LaneNumber.AutoSize = true;
		this.LaneNumber.Location = new System.Drawing.Point(177, 163);
		this.LaneNumber.MaxLength = 10;
		this.LaneNumber.Name = "LaneNumber";
		this.LaneNumber.Size = new System.Drawing.Size(216, 21);
		this.LaneNumber.TabIndex = 60;
		this.LaneNumber.ValueChanged += new System.EventHandler(ReCalculate);
		appearance14.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.Span.Appearance = appearance14;
		this.Span.AutoSize = true;
		this.Span.Location = new System.Drawing.Point(177, 138);
		this.Span.MaxLength = 10;
		this.Span.Name = "Span";
		this.Span.Size = new System.Drawing.Size(216, 21);
		this.Span.TabIndex = 59;
		appearance15.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BridgeHeight.Appearance = appearance15;
		this.BridgeHeight.AutoSize = true;
		this.BridgeHeight.Location = new System.Drawing.Point(177, 113);
		this.BridgeHeight.MaxLength = 10;
		this.BridgeHeight.Name = "BridgeHeight";
		this.BridgeHeight.Size = new System.Drawing.Size(216, 21);
		this.BridgeHeight.TabIndex = 58;
		appearance16.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ClearWidth.Appearance = appearance16;
		this.ClearWidth.AutoSize = true;
		this.ClearWidth.Location = new System.Drawing.Point(177, 88);
		this.ClearWidth.MaxLength = 10;
		this.ClearWidth.Name = "ClearWidth";
		this.ClearWidth.Size = new System.Drawing.Size(216, 21);
		this.ClearWidth.TabIndex = 57;
		this.ClearWidth.ValueChanged += new System.EventHandler(ReCalculate);
		appearance17.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.TotalLength.Appearance = appearance17;
		this.TotalLength.AutoSize = true;
		this.TotalLength.Location = new System.Drawing.Point(177, 63);
		this.TotalLength.MaxLength = 10;
		this.TotalLength.Name = "TotalLength";
		this.TotalLength.Size = new System.Drawing.Size(216, 21);
		this.TotalLength.TabIndex = 56;
		this.TotalLength.ValueChanged += new System.EventHandler(ReCalculate);
		appearance18.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.Method.Appearance = appearance18;
		this.Method.AutoSize = true;
		this.Method.Location = new System.Drawing.Point(177, 38);
		this.Method.MaxLength = 10;
		this.Method.Name = "Method";
		this.Method.Size = new System.Drawing.Size(216, 21);
		this.Method.TabIndex = 55;
		this.lbCostPerMeter.AutoSize = true;
		this.lbCostPerMeter.Location = new System.Drawing.Point(13, 218);
		this.lbCostPerMeter.Name = "lbCostPerMeter";
		this.lbCostPerMeter.Size = new System.Drawing.Size(132, 12);
		this.lbCostPerMeter.TabIndex = 54;
		this.lbCostPerMeter.Text = "單位造價(計畫總經費/A)";
		this.lbCostPerSquareMeter.AutoSize = true;
		this.lbCostPerSquareMeter.Location = new System.Drawing.Point(13, 193);
		this.lbCostPerSquareMeter.Name = "lbCostPerSquareMeter";
		this.lbCostPerSquareMeter.Size = new System.Drawing.Size(143, 12);
		this.lbCostPerSquareMeter.TabIndex = 53;
		this.lbCostPerSquareMeter.Text = "單位造價(計畫總經費/A/B)";
		this.lbLaneNumber.AutoSize = true;
		this.lbLaneNumber.Location = new System.Drawing.Point(13, 168);
		this.lbLaneNumber.Name = "lbLaneNumber";
		this.lbLaneNumber.Size = new System.Drawing.Size(69, 12);
		this.lbLaneNumber.TabIndex = 52;
		this.lbLaneNumber.Text = "車道數量(C)";
		this.lbSpan.AutoSize = true;
		this.lbSpan.Location = new System.Drawing.Point(13, 143);
		this.lbSpan.Name = "lbSpan";
		this.lbSpan.Size = new System.Drawing.Size(29, 12);
		this.lbSpan.TabIndex = 51;
		this.lbSpan.Text = "跨距";
		this.lbBridgeHeight.AutoSize = true;
		this.lbBridgeHeight.Location = new System.Drawing.Point(13, 118);
		this.lbBridgeHeight.Name = "lbBridgeHeight";
		this.lbBridgeHeight.Size = new System.Drawing.Size(29, 12);
		this.lbBridgeHeight.TabIndex = 50;
		this.lbBridgeHeight.Text = "高度";
		this.lbClearWidth.AutoSize = true;
		this.lbClearWidth.Location = new System.Drawing.Point(13, 93);
		this.lbClearWidth.Name = "lbClearWidth";
		this.lbClearWidth.Size = new System.Drawing.Size(69, 12);
		this.lbClearWidth.TabIndex = 49;
		this.lbClearWidth.Text = "橋梁淨寬(B)";
		this.lbTotalLength.AutoSize = true;
		this.lbTotalLength.Location = new System.Drawing.Point(13, 68);
		this.lbTotalLength.Name = "lbTotalLength";
		this.lbTotalLength.Size = new System.Drawing.Size(69, 12);
		this.lbTotalLength.TabIndex = 48;
		this.lbTotalLength.Text = "橋梁總長(A)";
		this.lbMethod.AutoSize = true;
		this.lbMethod.Location = new System.Drawing.Point(13, 43);
		this.lbMethod.Name = "lbMethod";
		this.lbMethod.Size = new System.Drawing.Size(53, 12);
		this.lbMethod.TabIndex = 47;
		this.lbMethod.Text = "使用工法";
		appearance19.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.Structure.Appearance = appearance19;
		this.Structure.AutoSize = true;
		this.Structure.Location = new System.Drawing.Point(177, 13);
		this.Structure.Name = "Structure";
		this.Structure.Size = new System.Drawing.Size(216, 21);
		this.Structure.TabIndex = 45;
		this.lbStructure.AutoSize = true;
		this.lbStructure.Location = new System.Drawing.Point(13, 18);
		this.lbStructure.Name = "lbStructure";
		this.lbStructure.Size = new System.Drawing.Size(53, 12);
		this.lbStructure.TabIndex = 44;
		this.lbStructure.Text = "結構型式";
		this.lbCostPerLane.AutoSize = true;
		this.lbCostPerLane.Location = new System.Drawing.Point(13, 243);
		this.lbCostPerLane.Name = "lbCostPerLane";
		this.lbCostPerLane.Size = new System.Drawing.Size(143, 12);
		this.lbCostPerLane.TabIndex = 71;
		this.lbCostPerLane.Text = "單位造價(計畫總經費/A/C)";
		appearance20.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.CostPerLane.Appearance = appearance20;
		this.CostPerLane.AutoSize = true;
		this.CostPerLane.Location = new System.Drawing.Point(177, 238);
		this.CostPerLane.MaxLength = 10;
		this.CostPerLane.Name = "CostPerLane";
		this.CostPerLane.ReadOnly = true;
		this.CostPerLane.Size = new System.Drawing.Size(216, 21);
		this.CostPerLane.TabIndex = 72;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(399, 244);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(57, 12);
		this.label11.TabIndex = 73;
		this.label11.Text = "元/車道/M";
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
		this.Material.TabIndex = 94;
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
		this.RoadType.TabIndex = 93;
		this.RoadType.Text = null;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(13, 18);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(53, 12);
		this.label10.TabIndex = 92;
		this.label10.Text = "道路類型";
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.Material);
		this.groupBox1.Controls.Add(this.RoadType);
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(507, 41);
		this.groupBox1.TabIndex = 95;
		this.groupBox1.TabStop = false;
		this.groupBox2.Controls.Add(this.lbStructure);
		this.groupBox2.Controls.Add(this.Structure);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Controls.Add(this.lbMethod);
		this.groupBox2.Controls.Add(this.CostPerLane);
		this.groupBox2.Controls.Add(this.lbTotalLength);
		this.groupBox2.Controls.Add(this.lbCostPerLane);
		this.groupBox2.Controls.Add(this.lbClearWidth);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.lbBridgeHeight);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.lbSpan);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.lbLaneNumber);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.lbCostPerSquareMeter);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.lbCostPerMeter);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.Method);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.TotalLength);
		this.groupBox2.Controls.Add(this.CostPerMeter);
		this.groupBox2.Controls.Add(this.ClearWidth);
		this.groupBox2.Controls.Add(this.CostPerSquareMeter);
		this.groupBox2.Controls.Add(this.BridgeHeight);
		this.groupBox2.Controls.Add(this.LaneNumber);
		this.groupBox2.Controls.Add(this.Span);
		this.groupBox2.Location = new System.Drawing.Point(0, 41);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(507, 269);
		this.groupBox2.TabIndex = 96;
		this.groupBox2.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "Bridge";
		base.Size = new System.Drawing.Size(682, 356);
		((System.ComponentModel.ISupportInitialize)base.ControlDataSet).EndInit();
		((System.ComponentModel.ISupportInitialize)this.CostPerMeter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.CostPerSquareMeter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.LaneNumber).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Span).EndInit();
		((System.ComponentModel.ISupportInitialize)this.BridgeHeight).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ClearWidth).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TotalLength).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Method).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Structure).EndInit();
		((System.ComponentModel.ISupportInitialize)this.CostPerLane).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Material).EndInit();
		((System.ComponentModel.ISupportInitialize)this.RoadType).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}
}
