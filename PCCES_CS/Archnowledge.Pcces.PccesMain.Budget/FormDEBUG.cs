using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.PccesMain.ArchControls;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormDEBUG : Form
{
	private DataTable DT_DEBUG = new DataTable();

	private IContainer components = null;

	private UltraButton BtnOK;

	public GridMrsBase Grid1;

	public DataSet DisplayDataSet
	{
		set
		{
			DT_DEBUG = value.Tables[0];
		}
	}

	public FormDEBUG()
	{
		InitializeComponent();
	}

	private void FormDEBUG_Load(object sender, EventArgs e)
	{
		BindData();
	}

	private void BindData()
	{
		Grid1.Rows.Count = DT_DEBUG.Rows.Count + 1;
		for (int i = 0; i < DT_DEBUG.Rows.Count; i++)
		{
			Grid1[i + 1, "pccesCode"] = DT_DEBUG.Rows[i]["pccesCode"];
			Grid1[i + 1, "cName"] = DT_DEBUG.Rows[i]["cName"];
			Grid1[i + 1, "BudPccesCode"] = DT_DEBUG.Rows[i]["BudPccesCode"];
			Grid1[i + 1, "BudResName"] = DT_DEBUG.Rows[i]["BudResName"];
			Grid1[i + 1, "usrQty"] = DT_DEBUG.Rows[i]["usrQty"];
			Grid1[i + 1, "usrAmt"] = DT_DEBUG.Rows[i]["usrAmt"];
			Grid1[i + 1, "svrPccesCode"] = DT_DEBUG.Rows[i]["svrPccesCode"];
			Grid1[i + 1, "svrItemQty"] = DT_DEBUG.Rows[i]["svrItemQty"];
			Grid1[i + 1, "svrItemAmt"] = DT_DEBUG.Rows[i]["svrItemAmt"];
		}
	}

	private void BtnOK_Click(object sender, EventArgs e)
	{
		Close();
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
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormDEBUG));
		this.BtnOK = new Infragistics.Win.Misc.UltraButton();
		this.Grid1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		((System.ComponentModel.ISupportInitialize)this.Grid1).BeginInit();
		base.SuspendLayout();
		this.BtnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnOK.Appearance = appearance1;
		this.BtnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnOK.Location = new System.Drawing.Point(696, 368);
		this.BtnOK.Name = "BtnOK";
		this.BtnOK.ShowFocusRect = false;
		this.BtnOK.ShowOutline = false;
		this.BtnOK.Size = new System.Drawing.Size(88, 31);
		this.BtnOK.SupportThemes = false;
		this.BtnOK.TabIndex = 5;
		this.BtnOK.Text = "確定";
		this.BtnOK.Click += new System.EventHandler(BtnOK_Click);
		this.Grid1._ExcelFileName = "";
		this.Grid1._ExcelSheeName = "";
		this.Grid1._IsOpenExcelAfterExport = false;
		this.Grid1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.Grid1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.Grid1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.Grid1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.Grid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.Grid1.ColumnInfo = resources.GetString("Grid1.ColumnInfo");
		this.Grid1.ExtendLastCol = true;
		this.Grid1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Grid1.ForeColor = System.Drawing.Color.Black;
		this.Grid1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.Grid1.IsProcessUndo = false;
		this.Grid1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.Grid1.Location = new System.Drawing.Point(8, 8);
		this.Grid1.Name = "Grid1";
		this.Grid1.Rows.Count = 1;
		this.Grid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.Grid1.ShowCursor = true;
		this.Grid1.ShowToolTipOnNarrowColumn = true;
		this.Grid1.Size = new System.Drawing.Size(776, 355);
		this.Grid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("Grid1.Styles"));
		this.Grid1.TabIndex = 12;
		this.Grid1.UndoMax = 10;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.ClientSize = new System.Drawing.Size(792, 403);
		base.Controls.Add(this.Grid1);
		base.Controls.Add(this.BtnOK);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		base.Name = "FormDEBUG";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "FormDEBUG";
		base.Load += new System.EventHandler(FormDEBUG_Load);
		((System.ComponentModel.ISupportInitialize)this.Grid1).EndInit();
		base.ResumeLayout(false);
	}
}
