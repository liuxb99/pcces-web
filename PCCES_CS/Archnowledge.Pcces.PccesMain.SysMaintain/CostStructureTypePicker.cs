using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.PccesMain.ArchControls;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.SysMaintain;

public class CostStructureTypePicker : Form
{
	private IContainer components = null;

	private Panel panelTitle;

	private UltraLabel ultraLabel7;

	private Panel panelButtons;

	private GroupBox groupBox1;

	private UltraButton btnCancel;

	private UltraButton btnOK;

	private Panel panel3;

	private GridBudget gridCostStructureType;

	public string[] SelectedTypes;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.SysMaintain.CostStructureTypePicker));
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		this.panelTitle = new System.Windows.Forms.Panel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.panelButtons = new System.Windows.Forms.Panel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnCancel = new Infragistics.Win.Misc.UltraButton();
		this.btnOK = new Infragistics.Win.Misc.UltraButton();
		this.panel3 = new System.Windows.Forms.Panel();
		this.gridCostStructureType = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.panelTitle.SuspendLayout();
		this.panelButtons.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridCostStructureType).BeginInit();
		base.SuspendLayout();
		this.panelTitle.BackColor = System.Drawing.Color.White;
		this.panelTitle.Controls.Add(this.ultraLabel7);
		this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.panelTitle.Location = new System.Drawing.Point(0, 0);
		this.panelTitle.Name = "panelTitle";
		this.panelTitle.Size = new System.Drawing.Size(342, 39);
		this.panelTitle.TabIndex = 12;
		appearance1.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance1;
		this.ultraLabel7.Font = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel7.Location = new System.Drawing.Point(12, 12);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(242, 24);
		this.ultraLabel7.TabIndex = 6;
		this.ultraLabel7.Text = "請挑選要匯入的成本架構類別";
		this.panelButtons.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panelButtons.Controls.Add(this.groupBox1);
		this.panelButtons.Controls.Add(this.btnCancel);
		this.panelButtons.Controls.Add(this.btnOK);
		this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panelButtons.Location = new System.Drawing.Point(0, 297);
		this.panelButtons.Name = "panelButtons";
		this.panelButtons.Size = new System.Drawing.Size(342, 44);
		this.panelButtons.TabIndex = 13;
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(342, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnCancel.Appearance = appearance2;
		this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
		this.btnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Font = new System.Drawing.Font("細明體", 11f);
		this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btnCancel.Location = new System.Drawing.Point(242, 10);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.ShowFocusRect = false;
		this.btnCancel.ShowOutline = false;
		this.btnCancel.Size = new System.Drawing.Size(88, 31);
		this.btnCancel.SupportThemes = false;
		this.btnCancel.TabIndex = 2;
		this.btnCancel.Text = "取消";
		this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.ImageHAlign = Infragistics.Win.HAlign.Left;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnOK.Appearance = appearance3;
		this.btnOK.BackColor = System.Drawing.SystemColors.Control;
		this.btnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOK.Font = new System.Drawing.Font("細明體", 11f);
		this.btnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.btnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.btnOK.Location = new System.Drawing.Point(148, 10);
		this.btnOK.Name = "btnOK";
		this.btnOK.ShowFocusRect = false;
		this.btnOK.ShowOutline = false;
		this.btnOK.Size = new System.Drawing.Size(88, 31);
		this.btnOK.SupportThemes = false;
		this.btnOK.TabIndex = 1;
		this.btnOK.Text = "確定";
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.panel3.Controls.Add(this.gridCostStructureType);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 39);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(342, 258);
		this.panel3.TabIndex = 14;
		this.gridCostStructureType._ExcelFileName = "";
		this.gridCostStructureType._ExcelSheeName = "";
		this.gridCostStructureType._IsOpenExcelAfterExport = false;
		this.gridCostStructureType.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridCostStructureType.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridCostStructureType.ColumnInfo = resources.GetString("gridCostStructureType.ColumnInfo");
		this.gridCostStructureType.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridCostStructureType.ExtendLastCol = true;
		this.gridCostStructureType.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridCostStructureType.ForeColor = System.Drawing.Color.Black;
		this.gridCostStructureType.Location = new System.Drawing.Point(0, 0);
		this.gridCostStructureType.Name = "gridCostStructureType";
		this.gridCostStructureType.Rows.Count = 1;
		this.gridCostStructureType.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridCostStructureType.ShowCursor = true;
		this.gridCostStructureType.ShowSort = false;
		this.gridCostStructureType.ShowToolTipOnNarrowColumn = true;
		this.gridCostStructureType.Size = new System.Drawing.Size(342, 258);
		this.gridCostStructureType.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridCostStructureType.Styles"));
		this.gridCostStructureType.TabIndex = 2;
		this.gridCostStructureType.Tree.Column = 1;
		this.gridCostStructureType.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridCostStructureType.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridCostStructureType_AfterEdit);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(342, 341);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panelButtons);
		base.Controls.Add(this.panelTitle);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "CostStructureTypePicker";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "成本架構類別挑選";
		this.panelTitle.ResumeLayout(false);
		this.panelButtons.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridCostStructureType).EndInit();
		base.ResumeLayout(false);
	}

	public CostStructureTypePicker()
	{
		InitializeComponent();
		DataToGrid();
	}

	private void DataToGrid()
	{
		string FilePath = AppDomain.CurrentDomain.BaseDirectory + "CostStructure\\CostStructureMrs";
		string[] CostStructureMrsFiles = Directory.GetFiles(FilePath, "*.txt");
		gridCostStructureType.Rows.Count = CostStructureMrsFiles.Length + 1;
		for (int i = 0; i < CostStructureMrsFiles.Length; i++)
		{
			gridCostStructureType.Rows[i + 1]["Select"] = true;
			gridCostStructureType.Rows[i + 1]["Type"] = GetCostStructureType(CostStructureMrsFiles[i]);
		}
		gridCostStructureType.SetCellCheck(0, 1, CheckEnum.Checked);
		gridCostStructureType.SetData(0, 1, "勾選", coerce: false);
	}

	private string GetCostStructureType(string FullFileName)
	{
		string EndSymbol = "】";
		string FileName = Path.GetFileNameWithoutExtension(FullFileName);
		return FileName.Substring(1, FileName.IndexOf(EndSymbol) - 1);
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		List<string> Types = new List<string>();
		foreach (Row row in (IEnumerable)gridCostStructureType.Rows)
		{
			if (ArchConvert.Obj2Bool(row["Select"]))
			{
				Types.Add(row["Type"].ToString());
			}
		}
		if (Types.Count == 0)
		{
			MessageBox.Show("尚未選取任一類別！");
			return;
		}
		SelectedTypes = Types.ToArray();
		base.DialogResult = DialogResult.OK;
	}

	private void gridCostStructureType_AfterEdit(object sender, RowColEventArgs e)
	{
		if (e.Row == 0 && e.Col == 1)
		{
			CheckEnum CheckStatus = gridCostStructureType.GetCellCheck(e.Row, e.Col);
			for (int i = 1; i < gridCostStructureType.Rows.Count; i++)
			{
				gridCostStructureType.SetCellCheck(i, 1, CheckStatus);
			}
			return;
		}
		for (int i = 1; i < gridCostStructureType.Rows.Count; i++)
		{
			if (!(bool)gridCostStructureType[i, "Select"])
			{
				gridCostStructureType.SetCellCheck(0, 1, CheckEnum.Unchecked);
				return;
			}
		}
		gridCostStructureType.SetCellCheck(0, 1, CheckEnum.Checked);
	}
}
