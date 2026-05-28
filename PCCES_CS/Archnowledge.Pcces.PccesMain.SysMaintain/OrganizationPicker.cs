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

public class OrganizationPicker : Form
{
	public string[][] SelectedOrganizations;

	private IContainer components = null;

	private UltraButton btnOK;

	private Panel panelGrid;

	private GridBudget gridOrganization;

	private UltraButton btnCancel;

	private GroupBox gbButtons;

	private Panel panelButtons;

	private Panel panelTitle;

	private Label lbDescription;

	private Label lbTitle;

	public OrganizationPicker()
	{
		InitializeComponent();
	}

	private void OrganizationPicker_Load(object sender, EventArgs e)
	{
		string FilePath = AppDomain.CurrentDomain.BaseDirectory + "OrganizationDatabases";
		string[] Organizations = Directory.GetFiles(FilePath, "*.xml");
		gridOrganization.Rows.Count = Organizations.Length + 1;
		for (int i = 0; i < Organizations.Length; i++)
		{
			if (Path.GetFileName(Organizations[i]).Length >= 5)
			{
				string sPrefix = Path.GetFileName(Organizations[i]).Substring(0, 5);
				if (sPrefix.ToUpper() == "PCCES")
				{
					continue;
				}
			}
			string[] CodeAndName = Path.GetFileNameWithoutExtension(Organizations[i]).Split(',');
			gridOrganization.Rows[i + 1]["Select"] = false;
			gridOrganization.Rows[i + 1]["Code"] = CodeAndName[0];
			gridOrganization.Rows[i + 1]["Name"] = CodeAndName[1];
			gridOrganization.Rows[i + 1]["Version"] = CodeAndName[2];
		}
		gridOrganization.SetCellCheck(0, 1, CheckEnum.Unchecked);
		gridOrganization.SetData(0, 1, "勾選", coerce: false);
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		List<string[]> Organizations = new List<string[]>();
		foreach (Row row in (IEnumerable)gridOrganization.Rows)
		{
			if (ArchConvert.Obj2Bool(row["Select"]))
			{
				Organizations.Add(new string[3]
				{
					row["Code"].ToString(),
					row["Name"].ToString(),
					row["Version"].ToString()
				});
			}
		}
		if (Organizations.Count == 0)
		{
			MessageBox.Show("尚未選取任一機關！");
			return;
		}
		SelectedOrganizations = Organizations.ToArray();
		base.DialogResult = DialogResult.OK;
	}

	private void gridOrganization_AfterEdit(object sender, RowColEventArgs e)
	{
		if (e.Row == 0 && e.Col == 1)
		{
			CheckEnum CheckStatus = gridOrganization.GetCellCheck(e.Row, e.Col);
			for (int i = 1; i < gridOrganization.Rows.Count; i++)
			{
				gridOrganization.SetCellCheck(i, 1, CheckStatus);
			}
			return;
		}
		for (int i = 1; i < gridOrganization.Rows.Count; i++)
		{
			if (!(bool)gridOrganization[i, "Select"])
			{
				gridOrganization.SetCellCheck(0, 1, CheckEnum.Unchecked);
				return;
			}
		}
		gridOrganization.SetCellCheck(0, 1, CheckEnum.Checked);
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
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.SysMaintain.OrganizationPicker));
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		this.btnOK = new Infragistics.Win.Misc.UltraButton();
		this.panelGrid = new System.Windows.Forms.Panel();
		this.gridOrganization = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.btnCancel = new Infragistics.Win.Misc.UltraButton();
		this.gbButtons = new System.Windows.Forms.GroupBox();
		this.panelButtons = new System.Windows.Forms.Panel();
		this.panelTitle = new System.Windows.Forms.Panel();
		this.lbDescription = new System.Windows.Forms.Label();
		this.lbTitle = new System.Windows.Forms.Label();
		this.panelGrid.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridOrganization).BeginInit();
		this.panelButtons.SuspendLayout();
		this.panelTitle.SuspendLayout();
		base.SuspendLayout();
		this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance5.Image = resources.GetObject("appearance5.Image");
		appearance5.ImageHAlign = Infragistics.Win.HAlign.Left;
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnOK.Appearance = appearance5;
		this.btnOK.BackColor = System.Drawing.SystemColors.Control;
		this.btnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOK.Font = new System.Drawing.Font("細明體", 11f);
		this.btnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.btnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.btnOK.Location = new System.Drawing.Point(247, 10);
		this.btnOK.Name = "btnOK";
		this.btnOK.ShowFocusRect = false;
		this.btnOK.ShowOutline = false;
		this.btnOK.Size = new System.Drawing.Size(88, 31);
		this.btnOK.SupportThemes = false;
		this.btnOK.TabIndex = 1;
		this.btnOK.Text = "確定";
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.panelGrid.Controls.Add(this.gridOrganization);
		this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panelGrid.Location = new System.Drawing.Point(0, 80);
		this.panelGrid.Name = "panelGrid";
		this.panelGrid.Size = new System.Drawing.Size(441, 293);
		this.panelGrid.TabIndex = 17;
		this.gridOrganization._ExcelFileName = "";
		this.gridOrganization._ExcelSheeName = "";
		this.gridOrganization._IsOpenExcelAfterExport = false;
		this.gridOrganization.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridOrganization.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridOrganization.ColumnInfo = resources.GetString("gridOrganization.ColumnInfo");
		this.gridOrganization.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridOrganization.ExtendLastCol = true;
		this.gridOrganization.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridOrganization.ForeColor = System.Drawing.Color.Black;
		this.gridOrganization.Location = new System.Drawing.Point(0, 0);
		this.gridOrganization.Name = "gridOrganization";
		this.gridOrganization.Rows.Count = 1;
		this.gridOrganization.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridOrganization.ShowCursor = true;
		this.gridOrganization.ShowSort = false;
		this.gridOrganization.ShowToolTipOnNarrowColumn = true;
		this.gridOrganization.Size = new System.Drawing.Size(441, 293);
		this.gridOrganization.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridOrganization.Styles"));
		this.gridOrganization.TabIndex = 2;
		this.gridOrganization.Tree.Column = 1;
		this.gridOrganization.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridOrganization.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridOrganization_AfterEdit);
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
		this.btnCancel.Location = new System.Drawing.Point(341, 10);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.ShowFocusRect = false;
		this.btnCancel.ShowOutline = false;
		this.btnCancel.Size = new System.Drawing.Size(88, 31);
		this.btnCancel.SupportThemes = false;
		this.btnCancel.TabIndex = 2;
		this.btnCancel.Text = "取消";
		this.gbButtons.Dock = System.Windows.Forms.DockStyle.Top;
		this.gbButtons.Location = new System.Drawing.Point(0, 0);
		this.gbButtons.Name = "gbButtons";
		this.gbButtons.Size = new System.Drawing.Size(441, 8);
		this.gbButtons.TabIndex = 3;
		this.gbButtons.TabStop = false;
		this.panelButtons.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panelButtons.Controls.Add(this.gbButtons);
		this.panelButtons.Controls.Add(this.btnCancel);
		this.panelButtons.Controls.Add(this.btnOK);
		this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panelButtons.Location = new System.Drawing.Point(0, 373);
		this.panelButtons.Name = "panelButtons";
		this.panelButtons.Size = new System.Drawing.Size(441, 44);
		this.panelButtons.TabIndex = 16;
		this.panelTitle.BackColor = System.Drawing.Color.White;
		this.panelTitle.Controls.Add(this.lbDescription);
		this.panelTitle.Controls.Add(this.lbTitle);
		this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.panelTitle.Location = new System.Drawing.Point(0, 0);
		this.panelTitle.Name = "panelTitle";
		this.panelTitle.Size = new System.Drawing.Size(441, 80);
		this.panelTitle.TabIndex = 15;
		this.lbDescription.Font = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbDescription.Location = new System.Drawing.Point(12, 37);
		this.lbDescription.Name = "lbDescription";
		this.lbDescription.Size = new System.Drawing.Size(367, 38);
		this.lbDescription.TabIndex = 8;
		this.lbDescription.Text = "此功能將分別建立各機關既有編碼暨單價分析資料庫供參考引用。";
		this.lbTitle.AutoSize = true;
		this.lbTitle.Font = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lbTitle.Location = new System.Drawing.Point(12, 9);
		this.lbTitle.Name = "lbTitle";
		this.lbTitle.Size = new System.Drawing.Size(212, 16);
		this.lbTitle.TabIndex = 7;
		this.lbTitle.Text = "請挑選要建立的機關資料庫";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(441, 417);
		base.Controls.Add(this.panelGrid);
		base.Controls.Add(this.panelButtons);
		base.Controls.Add(this.panelTitle);
		base.MinimizeBox = false;
		base.Name = "OrganizationPicker";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "建立各機關資料庫";
		base.Load += new System.EventHandler(OrganizationPicker_Load);
		this.panelGrid.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridOrganization).EndInit();
		this.panelButtons.ResumeLayout(false);
		this.panelTitle.ResumeLayout(false);
		this.panelTitle.PerformLayout();
		base.ResumeLayout(false);
	}
}
