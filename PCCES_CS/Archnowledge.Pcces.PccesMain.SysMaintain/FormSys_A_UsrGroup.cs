using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.PccesMain.ArchControls;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.SysMaintain;

public class FormSys_A_UsrGroup : Form
{
	private const string CallFormHelp = "FormSys_A_UsrGroup";

	private Panel panel1;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private Panel panel2;

	private GroupBox groupBox1;

	private UltraButton Btn_Cncl;

	private UltraButton Btn_OK;

	private Panel panel3;

	private IContainer components;

	private DBClass DBCLS = new DBClass();

	private string F_UserID;

	public GridMrsBase GridUserGroup;

	private DataTable DT_UserGroups = new DataTable();

	public string _UserID
	{
		get
		{
			return F_UserID;
		}
		set
		{
			F_UserID = value;
		}
	}

	public FormSys_A_UsrGroup()
	{
		InitializeComponent();
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.SysMaintain.FormSys_A_UsrGroup));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.Btn_OK = new Infragistics.Win.Misc.UltraButton();
		this.panel3 = new System.Windows.Forms.Panel();
		this.GridUserGroup = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridUserGroup).BeginInit();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Controls.Add(this.ultraLabel2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(512, 56);
		this.panel1.TabIndex = 1;
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(8, 8);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(204, 23);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "使用者隸屬群組維護";
		this.ultraLabel2.Location = new System.Drawing.Point(23, 32);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(321, 23);
		this.ultraLabel2.TabIndex = 0;
		this.ultraLabel2.Text = "勾選第一欄來加入或移除群組";
		this.panel2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel2.Controls.Add(this.groupBox1);
		this.panel2.Controls.Add(this.Btn_Cncl);
		this.panel2.Controls.Add(this.Btn_OK);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 329);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(512, 44);
		this.panel2.TabIndex = 11;
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(512, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.Btn_Cncl.Appearance = appearance1;
		this.Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.Btn_Cncl.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.Btn_Cncl.Location = new System.Drawing.Point(416, 10);
		this.Btn_Cncl.Name = "Btn_Cncl";
		this.Btn_Cncl.ShowFocusRect = false;
		this.Btn_Cncl.ShowOutline = false;
		this.Btn_Cncl.Size = new System.Drawing.Size(88, 28);
		this.Btn_Cncl.SupportThemes = false;
		this.Btn_Cncl.TabIndex = 2;
		this.Btn_Cncl.Text = "取消";
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.Btn_OK.Appearance = appearance2;
		this.Btn_OK.BackColor = System.Drawing.SystemColors.Control;
		this.Btn_OK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.Btn_OK.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Btn_OK.ImageSize = new System.Drawing.Size(20, 20);
		this.Btn_OK.ImageTransparentColor = System.Drawing.Color.White;
		this.Btn_OK.Location = new System.Drawing.Point(324, 10);
		this.Btn_OK.Name = "Btn_OK";
		this.Btn_OK.ShowFocusRect = false;
		this.Btn_OK.ShowOutline = false;
		this.Btn_OK.Size = new System.Drawing.Size(88, 28);
		this.Btn_OK.SupportThemes = false;
		this.Btn_OK.TabIndex = 1;
		this.Btn_OK.Text = "確定";
		this.Btn_OK.Click += new System.EventHandler(Btn_OK_Click);
		this.panel3.Controls.Add(this.GridUserGroup);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 56);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(512, 273);
		this.panel3.TabIndex = 12;
		this.GridUserGroup._ExcelFileName = "";
		this.GridUserGroup._ExcelSheeName = "";
		this.GridUserGroup._IsOpenExcelAfterExport = false;
		this.GridUserGroup.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.GridUserGroup.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
		this.GridUserGroup.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.GridUserGroup.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridUserGroup.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.GridUserGroup.ColumnInfo = "4,1,0,0,0,110,Columns:0{Width:17;Name:\"RowIndicator\";AllowDragging:False;AllowResizing:False;AllowEditing:False;DataType:System.Int32;TextAlign:RightTop;TextAlignFixed:GeneralTop;}\t1{Width:40;Name:\"IsCheck\";Caption:\"勾選\";DataType:System.Boolean;TextAlign:GeneralBottom;TextAlignFixed:GeneralTop;ImageAlign:CenterCenter;}\t2{Width:94;Name:\"GroupID\";Caption:\"群組帳號\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t3{Width:200;Name:\"GroupName\";Caption:\"群組名稱\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t";
		this.GridUserGroup.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridUserGroup.ExtendLastCol = true;
		this.GridUserGroup.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridUserGroup.ForeColor = System.Drawing.Color.Black;
		this.GridUserGroup.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.GridUserGroup.IsProcessUndo = false;
		this.GridUserGroup.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.GridUserGroup.Location = new System.Drawing.Point(0, 0);
		this.GridUserGroup.Name = "GridUserGroup";
		this.GridUserGroup.Rows.Count = 1;
		this.GridUserGroup.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.GridUserGroup.ShowCursor = true;
		this.GridUserGroup.ShowToolTipOnNarrowColumn = true;
		this.GridUserGroup.Size = new System.Drawing.Size(512, 273);
		this.GridUserGroup.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11.25pt;BackColor:237, 243, 254;ForeColor:Black;TextAlign:GeneralBottom;Border:Flat,1,Silver,Both;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;TextAlign:GeneralTop;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;TextAlign:LeftCenter;ImageAlign:CenterCenter;Border:None,1,Black,Both;Format:\"###,###,###,##0\";}\tFocus{Font:Arial, 10.5pt, style=Bold;BackColor:White;Margins:0, 0, 0, 0;Border:Double,1,96, 145, 234,Both;}\tSearch{BackColor:Highlight;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.GridUserGroup.TabIndex = 12;
		this.GridUserGroup.UndoMax = 10;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.ClientSize = new System.Drawing.Size(512, 373);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormSys_A_UsrGroup";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "隸屬變更";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormSys_A_UsrGroup_KeyDown);
		base.Load += new System.EventHandler(FormSys_A_UsrGroup_Load);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridUserGroup).EndInit();
		base.ResumeLayout(false);
	}

	private void FormSys_A_UsrGroup_Load(object sender, EventArgs e)
	{
		LoadData();
	}

	private void LoadData()
	{
		DT_UserGroups = DBCLS.GetUserGroups(F_UserID);
		BindToGrid();
	}

	private void BindToGrid()
	{
		GridUserGroup.Rows.Count = DT_UserGroups.Rows.Count + 1;
		for (int i = 0; i < DT_UserGroups.Rows.Count; i++)
		{
			GridUserGroup[i + 1, "GroupID"] = DT_UserGroups.Rows[i]["GroupID"].ToString().Trim();
			GridUserGroup[i + 1, "GroupName"] = DT_UserGroups.Rows[i]["GroupName"].ToString().Trim();
			GridUserGroup[i + 1, "IsCheck"] = ((DT_UserGroups.Rows[i]["GRP"].ToString() != "") ? true : false);
		}
		GridUserGroup.AutoSizeCols();
	}

	private void Btn_OK_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
		for (int i = 1; i < GridUserGroup.Rows.Count; i++)
		{
			if ((bool)GridUserGroup.Rows[i]["IsCheck"])
			{
				DT_UserGroups.Rows[i - 1]["GRP"] = F_UserID;
			}
			else
			{
				DT_UserGroups.Rows[i - 1]["GRP"] = "";
			}
		}
		DBCLS.UpdateUserGroups(DT_UserGroups, F_UserID);
	}

	private void FormSys_A_UsrGroup_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormSys_A_UsrGroup");
		}
	}
}
