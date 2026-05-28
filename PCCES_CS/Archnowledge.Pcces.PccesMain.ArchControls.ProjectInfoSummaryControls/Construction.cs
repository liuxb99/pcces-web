using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.PccesMain.Budget;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.ArchControls.ProjectInfoSummaryControls;

public class Construction : SummaryControlBase
{
	private IContainer components = null;

	private Label lbFloorAboveGround;

	private UltraTextEditor FloorAboveGround;

	private Label label2;

	private Label lbAboveGroundArea;

	private Label lbFloorUnderGround;

	private Label lbUnderGroundArea;

	private Label lbTotalFloor;

	private Label lbGrossFloorArea;

	private Label lbGroundFloorArea;

	private Label lbBasementArea;

	private Label lbCostPerUnit;

	private UltraTextEditor AboveGroundArea;

	private UltraTextEditor FloorUnderGround;

	private UltraTextEditor UnderGroundArea;

	private UltraTextEditor TotalFloor;

	private UltraTextEditor GrossFloorArea;

	private UltraTextEditor GroundFloorArea;

	private UltraTextEditor BasementArea;

	private UltraTextEditor CostPerUnit;

	private Label label1;

	private Label label3;

	private Label label4;

	private Label label5;

	private Label label6;

	private Label label7;

	private Label label8;

	private Label label9;

	private Label label10;

	private UltraComboEditor Material;

	private UltraComboEditor ConstructionType;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

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
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem6 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem7 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem8 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem9 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem10 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem11 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem12 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem13 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem14 = new Infragistics.Win.ValueListItem();
		this.lbFloorAboveGround = new System.Windows.Forms.Label();
		this.FloorAboveGround = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.label2 = new System.Windows.Forms.Label();
		this.lbAboveGroundArea = new System.Windows.Forms.Label();
		this.lbFloorUnderGround = new System.Windows.Forms.Label();
		this.lbUnderGroundArea = new System.Windows.Forms.Label();
		this.lbTotalFloor = new System.Windows.Forms.Label();
		this.lbGrossFloorArea = new System.Windows.Forms.Label();
		this.lbGroundFloorArea = new System.Windows.Forms.Label();
		this.lbBasementArea = new System.Windows.Forms.Label();
		this.lbCostPerUnit = new System.Windows.Forms.Label();
		this.AboveGroundArea = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.FloorUnderGround = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.UnderGroundArea = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.TotalFloor = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.GrossFloorArea = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.GroundFloorArea = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.BasementArea = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.CostPerUnit = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.label1 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.Material = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ConstructionType = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		((System.ComponentModel.ISupportInitialize)base.ControlDataSet).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.FloorAboveGround).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.AboveGroundArea).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.FloorUnderGround).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UnderGroundArea).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TotalFloor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.GrossFloorArea).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.GroundFloorArea).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.BasementArea).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.CostPerUnit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Material).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ConstructionType).BeginInit();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.lbFloorAboveGround.AutoSize = true;
		this.lbFloorAboveGround.Location = new System.Drawing.Point(13, 18);
		this.lbFloorAboveGround.Name = "lbFloorAboveGround";
		this.lbFloorAboveGround.Size = new System.Drawing.Size(93, 12);
		this.lbFloorAboveGround.TabIndex = 0;
		this.lbFloorAboveGround.Text = "地面上樓層數(A)";
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.FloorAboveGround.Appearance = appearance1;
		this.FloorAboveGround.AutoSize = true;
		this.FloorAboveGround.Location = new System.Drawing.Point(177, 13);
		this.FloorAboveGround.Name = "FloorAboveGround";
		this.FloorAboveGround.Size = new System.Drawing.Size(216, 21);
		this.FloorAboveGround.TabIndex = 18;
		this.FloorAboveGround.ValueChanged += new System.EventHandler(ReCalculate);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(399, 19);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(17, 12);
		this.label2.TabIndex = 19;
		this.label2.Text = "樓";
		this.lbAboveGroundArea.AutoSize = true;
		this.lbAboveGroundArea.Location = new System.Drawing.Point(13, 47);
		this.lbAboveGroundArea.Name = "lbAboveGroundArea";
		this.lbAboveGroundArea.Size = new System.Drawing.Size(117, 12);
		this.lbAboveGroundArea.TabIndex = 20;
		this.lbAboveGroundArea.Text = "地面上樓地板面積(B)";
		this.lbFloorUnderGround.AutoSize = true;
		this.lbFloorUnderGround.Location = new System.Drawing.Point(13, 76);
		this.lbFloorUnderGround.Name = "lbFloorUnderGround";
		this.lbFloorUnderGround.Size = new System.Drawing.Size(93, 12);
		this.lbFloorUnderGround.TabIndex = 21;
		this.lbFloorUnderGround.Text = "地面下樓層數(C)";
		this.lbUnderGroundArea.AutoSize = true;
		this.lbUnderGroundArea.Location = new System.Drawing.Point(13, 105);
		this.lbUnderGroundArea.Name = "lbUnderGroundArea";
		this.lbUnderGroundArea.Size = new System.Drawing.Size(117, 12);
		this.lbUnderGroundArea.TabIndex = 22;
		this.lbUnderGroundArea.Text = "地面下樓地板面積(D)";
		this.lbTotalFloor.AutoSize = true;
		this.lbTotalFloor.Location = new System.Drawing.Point(13, 134);
		this.lbTotalFloor.Name = "lbTotalFloor";
		this.lbTotalFloor.Size = new System.Drawing.Size(96, 12);
		this.lbTotalFloor.TabIndex = 23;
		this.lbTotalFloor.Text = "總樓層數(E=A+C)";
		this.lbGrossFloorArea.AutoSize = true;
		this.lbGrossFloorArea.Location = new System.Drawing.Point(13, 163);
		this.lbGrossFloorArea.Name = "lbGrossFloorArea";
		this.lbGrossFloorArea.Size = new System.Drawing.Size(119, 12);
		this.lbGrossFloorArea.TabIndex = 24;
		this.lbGrossFloorArea.Text = "總樓地板面積(F=B+D)";
		this.lbGroundFloorArea.AutoSize = true;
		this.lbGroundFloorArea.Location = new System.Drawing.Point(13, 192);
		this.lbGroundFloorArea.Name = "lbGroundFloorArea";
		this.lbGroundFloorArea.Size = new System.Drawing.Size(53, 12);
		this.lbGroundFloorArea.TabIndex = 25;
		this.lbGroundFloorArea.Text = "基地面積";
		this.lbBasementArea.AutoSize = true;
		this.lbBasementArea.Location = new System.Drawing.Point(13, 221);
		this.lbBasementArea.Name = "lbBasementArea";
		this.lbBasementArea.Size = new System.Drawing.Size(65, 12);
		this.lbBasementArea.TabIndex = 26;
		this.lbBasementArea.Text = "地下室面積";
		this.lbCostPerUnit.AutoSize = true;
		this.lbCostPerUnit.Location = new System.Drawing.Point(13, 250);
		this.lbCostPerUnit.Name = "lbCostPerUnit";
		this.lbCostPerUnit.Size = new System.Drawing.Size(130, 12);
		this.lbCostPerUnit.TabIndex = 27;
		this.lbCostPerUnit.Text = "單位造價(計畫總經費/F)";
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.AboveGroundArea.Appearance = appearance2;
		this.AboveGroundArea.AutoSize = true;
		this.AboveGroundArea.Location = new System.Drawing.Point(177, 42);
		this.AboveGroundArea.MaxLength = 10;
		this.AboveGroundArea.Name = "AboveGroundArea";
		this.AboveGroundArea.Size = new System.Drawing.Size(216, 21);
		this.AboveGroundArea.TabIndex = 28;
		this.AboveGroundArea.ValueChanged += new System.EventHandler(ReCalculate);
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.FloorUnderGround.Appearance = appearance3;
		this.FloorUnderGround.AutoSize = true;
		this.FloorUnderGround.Location = new System.Drawing.Point(177, 71);
		this.FloorUnderGround.MaxLength = 10;
		this.FloorUnderGround.Name = "FloorUnderGround";
		this.FloorUnderGround.Size = new System.Drawing.Size(216, 21);
		this.FloorUnderGround.TabIndex = 29;
		this.FloorUnderGround.ValueChanged += new System.EventHandler(ReCalculate);
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.UnderGroundArea.Appearance = appearance4;
		this.UnderGroundArea.AutoSize = true;
		this.UnderGroundArea.Location = new System.Drawing.Point(177, 100);
		this.UnderGroundArea.MaxLength = 10;
		this.UnderGroundArea.Name = "UnderGroundArea";
		this.UnderGroundArea.Size = new System.Drawing.Size(216, 21);
		this.UnderGroundArea.TabIndex = 30;
		this.UnderGroundArea.ValueChanged += new System.EventHandler(ReCalculate);
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.TotalFloor.Appearance = appearance5;
		this.TotalFloor.AutoSize = true;
		this.TotalFloor.Location = new System.Drawing.Point(177, 129);
		this.TotalFloor.MaxLength = 10;
		this.TotalFloor.Name = "TotalFloor";
		this.TotalFloor.ReadOnly = true;
		this.TotalFloor.Size = new System.Drawing.Size(216, 21);
		this.TotalFloor.TabIndex = 31;
		appearance6.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.GrossFloorArea.Appearance = appearance6;
		this.GrossFloorArea.AutoSize = true;
		this.GrossFloorArea.Location = new System.Drawing.Point(177, 158);
		this.GrossFloorArea.MaxLength = 10;
		this.GrossFloorArea.Name = "GrossFloorArea";
		this.GrossFloorArea.ReadOnly = true;
		this.GrossFloorArea.Size = new System.Drawing.Size(216, 21);
		this.GrossFloorArea.TabIndex = 32;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.GroundFloorArea.Appearance = appearance7;
		this.GroundFloorArea.AutoSize = true;
		this.GroundFloorArea.Location = new System.Drawing.Point(177, 187);
		this.GroundFloorArea.MaxLength = 10;
		this.GroundFloorArea.Name = "GroundFloorArea";
		this.GroundFloorArea.Size = new System.Drawing.Size(216, 21);
		this.GroundFloorArea.TabIndex = 33;
		appearance8.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BasementArea.Appearance = appearance8;
		this.BasementArea.AutoSize = true;
		this.BasementArea.Location = new System.Drawing.Point(177, 216);
		this.BasementArea.MaxLength = 10;
		this.BasementArea.Name = "BasementArea";
		this.BasementArea.Size = new System.Drawing.Size(216, 21);
		this.BasementArea.TabIndex = 34;
		appearance9.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.CostPerUnit.Appearance = appearance9;
		this.CostPerUnit.AutoSize = true;
		this.CostPerUnit.Location = new System.Drawing.Point(177, 245);
		this.CostPerUnit.MaxLength = 10;
		this.CostPerUnit.Name = "CostPerUnit";
		this.CostPerUnit.ReadOnly = true;
		this.CostPerUnit.Size = new System.Drawing.Size(216, 21);
		this.CostPerUnit.TabIndex = 35;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(399, 77);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(17, 12);
		this.label1.TabIndex = 36;
		this.label1.Text = "樓";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(399, 135);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(17, 12);
		this.label3.TabIndex = 37;
		this.label3.Text = "樓";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(399, 48);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(21, 12);
		this.label4.TabIndex = 38;
		this.label4.Text = "M2";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(399, 106);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(21, 12);
		this.label5.TabIndex = 39;
		this.label5.Text = "M2";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(399, 164);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(21, 12);
		this.label6.TabIndex = 40;
		this.label6.Text = "M2";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(399, 193);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(21, 12);
		this.label7.TabIndex = 41;
		this.label7.Text = "M2";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(399, 222);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(21, 12);
		this.label8.TabIndex = 42;
		this.label8.Text = "M2";
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(399, 250);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(36, 12);
		this.label9.TabIndex = 43;
		this.label9.Text = "元/M2";
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(13, 18);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(41, 12);
		this.label10.TabIndex = 44;
		this.label10.Text = "構造別";
		this.Material.AutoSize = true;
		valueListItem1.DataValue = "鋼骨混凝土(SRC)";
		valueListItem1.DisplayText = "鋼骨混凝土(SRC)";
		valueListItem2.DataValue = "鋼骨結構(SS)";
		valueListItem2.DisplayText = "鋼骨結構(SS)";
		valueListItem3.DataValue = "鋼筋混凝土(RC)";
		valueListItem3.DisplayText = "鋼筋混凝土(RC)";
		valueListItem4.DataValue = "加強磚造";
		valueListItem4.DisplayText = "加強磚造";
		valueListItem5.DataValue = "其他";
		valueListItem5.DisplayText = "其他";
		this.Material.Items.Add(valueListItem1);
		this.Material.Items.Add(valueListItem2);
		this.Material.Items.Add(valueListItem3);
		this.Material.Items.Add(valueListItem4);
		this.Material.Items.Add(valueListItem5);
		this.Material.Location = new System.Drawing.Point(135, 14);
		this.Material.Name = "Material";
		this.Material.Size = new System.Drawing.Size(121, 21);
		this.Material.TabIndex = 45;
		this.Material.Text = null;
		this.ConstructionType.AutoSize = true;
		valueListItem6.DataValue = "教室";
		valueListItem6.DisplayText = "教室";
		valueListItem7.DataValue = "辦公室";
		valueListItem7.DisplayText = "辦公室";
		valueListItem8.DataValue = "住宅";
		valueListItem8.DisplayText = "住宅";
		valueListItem9.DataValue = "宿舍";
		valueListItem9.DisplayText = "宿舍";
		valueListItem10.DataValue = "圖書館";
		valueListItem10.DisplayText = "圖書館";
		valueListItem11.DataValue = "活動中心(體育場所、集會場所)";
		valueListItem11.DisplayText = "活動中心(體育場所、集會場所)";
		valueListItem12.DataValue = "停車場";
		valueListItem12.DisplayText = "停車場";
		valueListItem13.DataValue = "醫院";
		valueListItem13.DisplayText = "醫院";
		valueListItem14.DataValue = "其他";
		this.ConstructionType.Items.Add(valueListItem6);
		this.ConstructionType.Items.Add(valueListItem7);
		this.ConstructionType.Items.Add(valueListItem8);
		this.ConstructionType.Items.Add(valueListItem9);
		this.ConstructionType.Items.Add(valueListItem10);
		this.ConstructionType.Items.Add(valueListItem11);
		this.ConstructionType.Items.Add(valueListItem12);
		this.ConstructionType.Items.Add(valueListItem13);
		this.ConstructionType.Items.Add(valueListItem14);
		this.ConstructionType.Location = new System.Drawing.Point(272, 14);
		this.ConstructionType.Name = "ConstructionType";
		this.ConstructionType.Size = new System.Drawing.Size(121, 21);
		this.ConstructionType.TabIndex = 46;
		this.ConstructionType.Text = null;
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.ConstructionType);
		this.groupBox1.Controls.Add(this.Material);
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(507, 41);
		this.groupBox1.TabIndex = 96;
		this.groupBox1.TabStop = false;
		this.groupBox2.Controls.Add(this.lbFloorAboveGround);
		this.groupBox2.Controls.Add(this.FloorAboveGround);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.lbAboveGroundArea);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.lbFloorUnderGround);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.lbUnderGroundArea);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.lbTotalFloor);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.lbGrossFloorArea);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.lbGroundFloorArea);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.lbBasementArea);
		this.groupBox2.Controls.Add(this.CostPerUnit);
		this.groupBox2.Controls.Add(this.lbCostPerUnit);
		this.groupBox2.Controls.Add(this.BasementArea);
		this.groupBox2.Controls.Add(this.AboveGroundArea);
		this.groupBox2.Controls.Add(this.GroundFloorArea);
		this.groupBox2.Controls.Add(this.FloorUnderGround);
		this.groupBox2.Controls.Add(this.GrossFloorArea);
		this.groupBox2.Controls.Add(this.UnderGroundArea);
		this.groupBox2.Controls.Add(this.TotalFloor);
		this.groupBox2.Location = new System.Drawing.Point(0, 41);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(507, 272);
		this.groupBox2.TabIndex = 97;
		this.groupBox2.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "Construction";
		base.Size = new System.Drawing.Size(668, 322);
		((System.ComponentModel.ISupportInitialize)base.ControlDataSet).EndInit();
		((System.ComponentModel.ISupportInitialize)this.FloorAboveGround).EndInit();
		((System.ComponentModel.ISupportInitialize)this.AboveGroundArea).EndInit();
		((System.ComponentModel.ISupportInitialize)this.FloorUnderGround).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UnderGroundArea).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TotalFloor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.GrossFloorArea).EndInit();
		((System.ComponentModel.ISupportInitialize)this.GroundFloorArea).EndInit();
		((System.ComponentModel.ISupportInitialize)this.BasementArea).EndInit();
		((System.ComponentModel.ISupportInitialize)this.CostPerUnit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Material).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ConstructionType).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}

	public Construction(FormBudgetProjectInfo budgetProjectInfo)
	{
		InitializeComponent();
		ControlDataSet.ReadXmlSchema(XSDFileDirectory + "ConstructionDataSet.xsd");
		base.budgetProjectInfo = budgetProjectInfo;
	}

	public override bool IsRequiredFilled()
	{
		if (Material.SelectedIndex == -1 || ConstructionType.SelectedIndex == -1 || FloorAboveGround.Text == string.Empty || AboveGroundArea.Text == string.Empty || FloorUnderGround.Text == string.Empty || UnderGroundArea.Text == string.Empty || GroundFloorArea.Text == string.Empty || BasementArea.Text == string.Empty)
		{
			return false;
		}
		return true;
	}

	protected override void ReCalculate(object sender, EventArgs e)
	{
		TotalFloor.Text = Convert.ToString(PubTools.Str2Double(FloorAboveGround.Text) + PubTools.Str2Double(FloorUnderGround.Text));
		GrossFloorArea.Text = Convert.ToString(PubTools.Str2Double(AboveGroundArea.Text) + PubTools.Str2Double(UnderGroundArea.Text));
		double dGrossFloorArea = String2Double(GrossFloorArea.Text);
		CostPerUnit.Text = Convert.ToString(PubTools.ARound(F_Amount / dGrossFloorArea, 2L));
		if (budgetProjectInfo != null)
		{
			budgetProjectInfo.UpdateProjectScopeUnit("M2");
			budgetProjectInfo.UpdateProjectScopeValue(GrossFloorArea.Text);
		}
	}
}
