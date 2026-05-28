using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.Budget;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.MrsBase.Bookmark;

public class FormMrsBase_BookmarkRemove : Form
{
	private IContainer components;

	private Panel panel5;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel6;

	private Panel panel2;

	private GroupBox groupBox2;

	private UltraButton A_Btn_Cncl;

	private UltraButton D_Btn_Fnsh;

	public GridMrsBase GridUnit1;

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.MrsBase.Bookmark.FormMrsBase_BookmarkRemove));
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.D_Btn_Fnsh = new Infragistics.Win.Misc.UltraButton();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.GridUnit1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.panel5.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).BeginInit();
		base.SuspendLayout();
		this.panel5.BackColor = System.Drawing.Color.White;
		this.panel5.Controls.Add(this.ultraLabel7);
		this.panel5.Controls.Add(this.ultraLabel6);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(418, 48);
		this.panel5.TabIndex = 15;
		appearance1.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance1;
		this.ultraLabel7.Location = new System.Drawing.Point(44, 27);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel7.TabIndex = 3;
		this.ultraLabel7.Text = "請挑選要清除的項目";
		appearance2.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance2;
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(12, 7);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 2;
		this.ultraLabel6.Text = "清除書籤";
		this.panel2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel2.Controls.Add(this.D_Btn_Fnsh);
		this.panel2.Controls.Add(this.groupBox2);
		this.panel2.Controls.Add(this.A_Btn_Cncl);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 258);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(418, 44);
		this.panel2.TabIndex = 16;
		this.D_Btn_Fnsh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Fnsh.Appearance = appearance3;
		this.D_Btn_Fnsh.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Fnsh.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Fnsh.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Fnsh.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Fnsh.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Fnsh.Location = new System.Drawing.Point(234, 10);
		this.D_Btn_Fnsh.Name = "D_Btn_Fnsh";
		this.D_Btn_Fnsh.ShowFocusRect = false;
		this.D_Btn_Fnsh.ShowOutline = false;
		this.D_Btn_Fnsh.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Fnsh.SupportThemes = false;
		this.D_Btn_Fnsh.TabIndex = 6;
		this.D_Btn_Fnsh.Text = "確定";
		this.D_Btn_Fnsh.Click += new System.EventHandler(D_Btn_Fnsh_Click);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(418, 8);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.A_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance4.Image = resources.GetObject("appearance4.Image");
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Cncl.Appearance = appearance4;
		this.A_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Cncl.Location = new System.Drawing.Point(325, 10);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.ShowFocusRect = false;
		this.A_Btn_Cncl.ShowOutline = false;
		this.A_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Cncl.SupportThemes = false;
		this.A_Btn_Cncl.TabIndex = 2;
		this.A_Btn_Cncl.Text = "取消";
		this.GridUnit1._ExcelFileName = "";
		this.GridUnit1._ExcelSheeName = "";
		this.GridUnit1._IsOpenExcelAfterExport = false;
		this.GridUnit1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.GridUnit1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridUnit1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.GridUnit1.ColumnInfo = "3,0,0,0,0,110,Columns:0{Width:40;Name:\"Check\";Caption:\"勾選\";AllowDragging:False;DataType:System.Boolean;TextAlign:RightTop;TextAlignFixed:GeneralTop;ImageAlign:CenterCenter;}\t1{Name:\"Code\";Caption:\"書籤代碼\";AllowDragging:False;AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t2{Width:300;Name:\"Bookmark\";Caption:\"書籤內容\";AllowDragging:False;AllowEditing:False;DataType:System.String;TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t";
		this.GridUnit1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridUnit1.ExtendLastCol = true;
		this.GridUnit1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridUnit1.ForeColor = System.Drawing.Color.Black;
		this.GridUnit1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.GridUnit1.IsProcessUndo = false;
		this.GridUnit1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.GridUnit1.Location = new System.Drawing.Point(0, 48);
		this.GridUnit1.Name = "GridUnit1";
		this.GridUnit1.Rows.Count = 1;
		this.GridUnit1.ShowCursor = true;
		this.GridUnit1.ShowToolTipOnNarrowColumn = true;
		this.GridUnit1.Size = new System.Drawing.Size(418, 210);
		this.GridUnit1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11.25pt;BackColor:237, 243, 254;ForeColor:Black;TextAlign:GeneralBottom;Border:Flat,1,Silver,Both;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;TextAlign:GeneralTop;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;TextAlign:GeneralCenter;ImageAlign:CenterCenter;Border:None,1,Black,Both;}\tFocus{BackColor:102, 153, 255;Margins:0, 0, 0, 0;TextAlign:GeneralCenter;Border:None,1,Black,Both;}\tSearch{BackColor:Highlight;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.GridUnit1.TabIndex = 17;
		this.GridUnit1.UndoMax = 10;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.CancelButton = this.A_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(418, 302);
		base.Controls.Add(this.GridUnit1);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel5);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormMrsBase_BookmarkRemove";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "清除書籤";
		base.Load += new System.EventHandler(FormMrsBase_BookmarkRemove_Load);
		this.panel5.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).EndInit();
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

	public FormMrsBase_BookmarkRemove()
	{
		InitializeComponent();
	}

	private void FormMrsBase_BookmarkRemove_Load(object sender, EventArgs e)
	{
		int iRows = 0;
		string sBookmark = "";
		iRows = ((!(base.Owner is frmMrsBase)) ? ((ComboBoxTool)(base.Owner as frmBudget).toolbarsManager.Tools["BookmarkList"]).ValueList.ValueListItems.Count : ((ComboBoxTool)(base.Owner as frmMrsBase).ultraToolbarsManager1.Tools["Other_cboBookmarks"]).ValueList.ValueListItems.Count);
		GridUnit1.Rows.Count = iRows + 1;
		for (int i = 0; i < iRows; i++)
		{
			sBookmark = ((!(base.Owner is frmMrsBase)) ? ((ComboBoxTool)(base.Owner as frmBudget).toolbarsManager.Tools["BookmarkList"]).ValueList.ValueListItems[i].ToString() : ((ComboBoxTool)(base.Owner as frmMrsBase).ultraToolbarsManager1.Tools["Other_cboBookmarks"]).ValueList.ValueListItems[i].ToString());
			int iSep = sBookmark.IndexOf(":");
			GridUnit1[i + 1, "Check"] = false;
			GridUnit1[i + 1, "Code"] = sBookmark.Substring(0, iSep);
			GridUnit1[i + 1, "Bookmark"] = sBookmark.Substring(iSep + 1).Trim();
		}
	}

	private void D_Btn_Fnsh_Click(object sender, EventArgs e)
	{
		for (int i = GridUnit1.Rows.Count; i > 1; i--)
		{
			if ((bool)GridUnit1[i - 1, "Check"])
			{
				if (base.Owner is frmMrsBase)
				{
					((ComboBoxTool)(base.Owner as frmMrsBase).ultraToolbarsManager1.Tools["Other_cboBookmarks"]).ValueList.ValueListItems.Remove(i - 2);
				}
				else
				{
					((ComboBoxTool)(base.Owner as frmBudget).toolbarsManager.Tools["BookmarkList"]).ValueList.ValueListItems.Remove(i - 2);
				}
			}
		}
		base.DialogResult = DialogResult.OK;
		Close();
	}
}
