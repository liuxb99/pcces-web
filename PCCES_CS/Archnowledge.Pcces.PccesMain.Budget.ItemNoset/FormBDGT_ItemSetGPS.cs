using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.PccesMain.ArchControls;
using C1.Win.C1FlexGrid;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.Budget.ItemNoset;

public class FormBDGT_ItemSetGPS : Form
{
	private string projectCode;

	private DataSet DSGPSLocation;

	private int MaxSno = 0;

	private GPSLocation GPSLocation;

	private IContainer components;

	private UltraLabel ultraLabel2;

	public GridMrsBase GridGPSLocation;

	private Panel panel9;

	private UltraButton btnCancel;

	private GroupBox groupBox5;

	private Panel panel5;

	private UltraButton btnInsert;

	private UltraButton btnDelete;

	private UltraButton btnOK;

	public string _ProjectCode
	{
		get
		{
			return projectCode;
		}
		set
		{
			projectCode = value;
		}
	}

	public FormBDGT_ItemSetGPS()
	{
		InitializeComponent();
		GPSLocation = new GPSLocation();
	}

	private void FormBDGT_ItemSetGPS_Load(object sender, EventArgs e)
	{
		DSGPSLocation = GPSLocation.GetGPSLocation(projectCode);
		DataTable DTGPSLocation = DSGPSLocation.Tables["GPSLocation"];
		int RowCount = DTGPSLocation.Rows.Count;
		if (RowCount > 0)
		{
			MaxSno = ArchConvert.Obj2Int(DTGPSLocation.Rows[RowCount - 1]["Sno"]);
		}
		GridGPSLocation.Rows.Count = 1;
		foreach (DataRow row in DTGPSLocation.Rows)
		{
			Row GridRow = GridGPSLocation.Rows.Add();
			GridRow["X"] = row["X"];
			GridRow["Y"] = row["Y"];
			GridRow["Sno"] = row["Sno"];
		}
	}

	private void btnInsert_Click(object sender, EventArgs e)
	{
		Row GridRow = GridGPSLocation.Rows.Add();
		GridRow["Sno"] = ++MaxSno;
		DataRow row = DSGPSLocation.Tables["GPSLocation"].NewRow();
		row["projectCode"] = projectCode;
		row["Sno"] = GridRow["Sno"];
		DSGPSLocation.Tables["GPSLocation"].Rows.Add(row);
	}

	private void btnDelete_Click(object sender, EventArgs e)
	{
		int selectedItemNumber = GridGPSLocation.SelectedItems;
		if (selectedItemNumber <= 0)
		{
			return;
		}
		DialogResult result = MessageBox.Show(this, "確定要刪除選取的 " + selectedItemNumber + " 筆項目？", "刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (result != DialogResult.Yes)
		{
			return;
		}
		DataView DVGPSLocation = new DataView(DSGPSLocation.Tables[0]);
		DVGPSLocation.Sort = "Sno";
		for (int index = GridGPSLocation.Rows.Count - 1; index > 0; index--)
		{
			if (GridGPSLocation.Rows[index].Selected)
			{
				DVGPSLocation[DVGPSLocation.Find(GridGPSLocation.Rows[index]["Sno"])].Delete();
				GridGPSLocation.Rows.Remove(index);
			}
		}
		DVGPSLocation.Dispose();
		DVGPSLocation = null;
	}

	private void GridGPSLocation_AfterEdit(object sender, RowColEventArgs e)
	{
		DataView DVGPSLocation = new DataView(DSGPSLocation.Tables["GPSLocation"]);
		DVGPSLocation.RowFilter = "Sno = '" + GridGPSLocation[e.Row, "Sno"].ToString() + "'";
		string editColumnName = GridGPSLocation.Cols[e.Col].Name;
		DVGPSLocation[0][editColumnName] = GridGPSLocation[e.Row, e.Col];
	}

	private void GridGPSLocation_ValidateEdit(object sender, ValidateEditEventArgs e)
	{
		if (!double.TryParse(GridGPSLocation.Editor.Text, out var _))
		{
			MessageBox.Show("請輸入數字！", "注意", MessageBoxButtons.OK);
			e.Cancel = true;
			return;
		}
		string columnToBeChecked = string.Empty;
		if (e.Col == 1)
		{
			columnToBeChecked = "Y";
		}
		else if (e.Col == 2)
		{
			columnToBeChecked = "X";
		}
		if (GridGPSLocation[e.Row, columnToBeChecked] != null && HasDuplication(e, columnToBeChecked))
		{
			MessageBox.Show("輸入座標重複，請重新輸入！", "注意", MessageBoxButtons.OK);
			e.Cancel = true;
		}
	}

	private bool HasDuplication(ValidateEditEventArgs e, string columnToBeChecked)
	{
		double inputCoordinate = ArchConvert.Obj2Double(GridGPSLocation.Editor.Text);
		double columnValueToBeChecked = ArchConvert.Obj2Double(GridGPSLocation[e.Row, columnToBeChecked]);
		foreach (Row row in (IEnumerable)GridGPSLocation.Rows)
		{
			if (row.Index != e.Row && ArchConvert.Obj2Double(row[e.Col]) == inputCoordinate && ArchConvert.Obj2Double(row[columnToBeChecked]) == columnValueToBeChecked)
			{
				return true;
			}
		}
		return false;
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		if (NoMissingCoordinate())
		{
			RemoveEmptyRow();
			GPSLocation.UpdateGPSLocation(DSGPSLocation);
			Close();
		}
		else
		{
			MessageBox.Show("尚有未填座標，請確認！");
		}
	}

	private bool NoMissingCoordinate()
	{
		foreach (DataRow row in DSGPSLocation.Tables[0].Rows)
		{
			if (row.RowState != DataRowState.Deleted && ((row["X"] == DBNull.Value && row["Y"] != DBNull.Value) || (row["X"] != DBNull.Value && row["Y"] == DBNull.Value)))
			{
				return false;
			}
		}
		return true;
	}

	private void RemoveEmptyRow()
	{
		DataTable DTGPSLocation = DSGPSLocation.Tables[0];
		for (int row = DTGPSLocation.Rows.Count - 1; row >= 0; row--)
		{
			if (DTGPSLocation.Rows[row].RowState != DataRowState.Deleted && DTGPSLocation.Rows[row]["X"] == DBNull.Value && DTGPSLocation.Rows[row]["Y"] == DBNull.Value)
			{
				DTGPSLocation.Rows[row].Delete();
			}
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.ItemNoset.FormBDGT_ItemSetGPS));
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.panel9 = new System.Windows.Forms.Panel();
		this.btnOK = new Infragistics.Win.Misc.UltraButton();
		this.btnCancel = new Infragistics.Win.Misc.UltraButton();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.panel5 = new System.Windows.Forms.Panel();
		this.btnInsert = new Infragistics.Win.Misc.UltraButton();
		this.btnDelete = new Infragistics.Win.Misc.UltraButton();
		this.GridGPSLocation = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.panel9.SuspendLayout();
		this.panel5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridGPSLocation).BeginInit();
		base.SuspendLayout();
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel2.Appearance = appearance1;
		this.ultraLabel2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraLabel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel2.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(328, 28);
		this.ultraLabel2.TabIndex = 3;
		this.panel9.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel9.Controls.Add(this.btnOK);
		this.panel9.Controls.Add(this.btnCancel);
		this.panel9.Controls.Add(this.groupBox5);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel9.Location = new System.Drawing.Point(0, 330);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(328, 40);
		this.panel9.TabIndex = 23;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnOK.Appearance = appearance2;
		this.btnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOK.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance3.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance3.BackColor2 = System.Drawing.Color.White;
		appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.btnOK.HotTrackAppearance = appearance3;
		this.btnOK.HotTracking = true;
		this.btnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.btnOK.Location = new System.Drawing.Point(137, 8);
		this.btnOK.Name = "btnOK";
		this.btnOK.ShowFocusRect = false;
		this.btnOK.ShowOutline = false;
		this.btnOK.Size = new System.Drawing.Size(90, 28);
		this.btnOK.SupportThemes = false;
		this.btnOK.TabIndex = 13;
		this.btnOK.Text = "確  定";
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance4.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance4.Image = resources.GetObject("appearance4.Image");
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnCancel.Appearance = appearance4;
		this.btnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btnCancel.Location = new System.Drawing.Point(233, 7);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.ShowFocusRect = false;
		this.btnCancel.ShowOutline = false;
		this.btnCancel.Size = new System.Drawing.Size(90, 28);
		this.btnCancel.SupportThemes = false;
		this.btnCancel.TabIndex = 6;
		this.btnCancel.Text = "取  消";
		this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox5.Location = new System.Drawing.Point(0, 0);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(328, 4);
		this.groupBox5.TabIndex = 3;
		this.groupBox5.TabStop = false;
		this.panel5.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel5.Controls.Add(this.btnInsert);
		this.panel5.Controls.Add(this.btnDelete);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel5.Location = new System.Drawing.Point(0, 294);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(328, 36);
		this.panel5.TabIndex = 24;
		appearance5.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance5.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance5.Image = resources.GetObject("appearance5.Image");
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnInsert.Appearance = appearance5;
		this.btnInsert.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnInsert.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnInsert.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnInsert.ImageSize = new System.Drawing.Size(20, 20);
		this.btnInsert.ImageTransparentColor = System.Drawing.Color.White;
		this.btnInsert.Location = new System.Drawing.Point(184, 5);
		this.btnInsert.Name = "btnInsert";
		this.btnInsert.ShowFocusRect = false;
		this.btnInsert.ShowOutline = false;
		this.btnInsert.Size = new System.Drawing.Size(64, 28);
		this.btnInsert.SupportThemes = false;
		this.btnInsert.TabIndex = 11;
		this.btnInsert.Text = "插入";
		this.btnInsert.Click += new System.EventHandler(btnInsert_Click);
		appearance6.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance6.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance6.Image = resources.GetObject("appearance6.Image");
		appearance6.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnDelete.Appearance = appearance6;
		this.btnDelete.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnDelete.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnDelete.ImageSize = new System.Drawing.Size(20, 20);
		this.btnDelete.ImageTransparentColor = System.Drawing.Color.White;
		this.btnDelete.Location = new System.Drawing.Point(256, 5);
		this.btnDelete.Name = "btnDelete";
		this.btnDelete.ShowFocusRect = false;
		this.btnDelete.ShowOutline = false;
		this.btnDelete.Size = new System.Drawing.Size(64, 28);
		this.btnDelete.SupportThemes = false;
		this.btnDelete.TabIndex = 8;
		this.btnDelete.Text = "刪除";
		this.btnDelete.Click += new System.EventHandler(btnDelete_Click);
		this.GridGPSLocation._ExcelFileName = "";
		this.GridGPSLocation._ExcelSheeName = "";
		this.GridGPSLocation._IsOpenExcelAfterExport = false;
		this.GridGPSLocation.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
		this.GridGPSLocation.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
		this.GridGPSLocation.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridGPSLocation.ColumnInfo = resources.GetString("GridGPSLocation.ColumnInfo");
		this.GridGPSLocation.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridGPSLocation.ExtendLastCol = true;
		this.GridGPSLocation.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridGPSLocation.ForeColor = System.Drawing.Color.Black;
		this.GridGPSLocation.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.GridGPSLocation.IsProcessUndo = false;
		this.GridGPSLocation.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.GridGPSLocation.Location = new System.Drawing.Point(0, 28);
		this.GridGPSLocation.Name = "GridGPSLocation";
		this.GridGPSLocation.Rows.Count = 1;
		this.GridGPSLocation.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.GridGPSLocation.ShowCursor = true;
		this.GridGPSLocation.ShowToolTipOnNarrowColumn = true;
		this.GridGPSLocation.Size = new System.Drawing.Size(328, 342);
		this.GridGPSLocation.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("GridGPSLocation.Styles"));
		this.GridGPSLocation.TabIndex = 10;
		this.GridGPSLocation.UndoMax = 10;
		this.GridGPSLocation.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(GridGPSLocation_AfterEdit);
		this.GridGPSLocation.ValidateEdit += new C1.Win.C1FlexGrid.ValidateEditEventHandler(GridGPSLocation_ValidateEdit);
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(328, 370);
		base.Controls.Add(this.panel5);
		base.Controls.Add(this.panel9);
		base.Controls.Add(this.GridGPSLocation);
		base.Controls.Add(this.ultraLabel2);
		base.Name = "FormBDGT_ItemSetGPS";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "工程所在地之 GPS 座標";
		base.Load += new System.EventHandler(FormBDGT_ItemSetGPS_Load);
		this.panel9.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridGPSLocation).EndInit();
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
