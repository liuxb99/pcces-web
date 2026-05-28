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
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinStatusBar;

namespace Archnowledge.Pcces.PccesMain.MrsBase;

public class FormMrsBase_DeleteMessage : Form
{
	private UltraLabel ultraLabel1;

	private UltraButton ultraButton1;

	private IContainer components;

	private Panel panel1;

	private UltraStatusBar ultraStatusBar1;

	public GridMrsBase GridUnit1;

	private UltraButton D_Btn_Yes;

	private UltraButton D_Btn_No;

	private UltraButton D_Btn_OK;

	private UltraPictureBox PicQuestion;

	private UltraPictureBox PicWarning;

	private string F_Message = "";

	private DataTable F_DTCannotDelete = new DataTable();

	private MessageBoxIcon F_MessageIcon = MessageBoxIcon.Question;

	private int F_iSel = 0;

	private string F_SrcKind = "MRS";

	public string _Message
	{
		get
		{
			return F_Message;
		}
		set
		{
			F_Message = value;
		}
	}

	public DataTable _DTCannotDelete
	{
		set
		{
			F_DTCannotDelete = value;
		}
	}

	public int _iSel
	{
		get
		{
			return F_iSel;
		}
		set
		{
			F_iSel = value;
		}
	}

	public MessageBoxIcon _MessageIcon
	{
		set
		{
			F_MessageIcon = value;
		}
	}

	public string _SrcKind
	{
		get
		{
			return F_SrcKind;
		}
		set
		{
			F_SrcKind = value;
		}
	}

	public FormMrsBase_DeleteMessage()
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
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.MrsBase.FormMrsBase_DeleteMessage));
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.D_Btn_Yes = new Infragistics.Win.Misc.UltraButton();
		this.D_Btn_No = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.GridUnit1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.PicQuestion = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.D_Btn_OK = new Infragistics.Win.Misc.UltraButton();
		this.PicWarning = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).BeginInit();
		base.SuspendLayout();
		this.ultraLabel1.Location = new System.Drawing.Point(64, 12);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(424, 80);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "[Message]";
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Yes.Appearance = appearance1;
		this.D_Btn_Yes.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Yes.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.D_Btn_Yes.DialogResult = System.Windows.Forms.DialogResult.Yes;
		this.D_Btn_Yes.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.D_Btn_Yes.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Yes.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Yes.Location = new System.Drawing.Point(157, 99);
		this.D_Btn_Yes.Name = "D_Btn_Yes";
		this.D_Btn_Yes.ShowFocusRect = false;
		this.D_Btn_Yes.ShowOutline = false;
		this.D_Btn_Yes.Size = new System.Drawing.Size(88, 28);
		this.D_Btn_Yes.SupportThemes = false;
		this.D_Btn_Yes.TabIndex = 2;
		this.D_Btn_Yes.Text = "是(&Y)";
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_No.Appearance = appearance2;
		this.D_Btn_No.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_No.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.D_Btn_No.DialogResult = System.Windows.Forms.DialogResult.No;
		this.D_Btn_No.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.D_Btn_No.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_No.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_No.Location = new System.Drawing.Point(248, 99);
		this.D_Btn_No.Name = "D_Btn_No";
		this.D_Btn_No.ShowFocusRect = false;
		this.D_Btn_No.ShowOutline = false;
		this.D_Btn_No.Size = new System.Drawing.Size(88, 28);
		this.D_Btn_No.SupportThemes = false;
		this.D_Btn_No.TabIndex = 3;
		this.D_Btn_No.Text = "否(&N)";
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraButton1.Appearance = appearance3;
		this.ultraButton1.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton1.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton1.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton1.Location = new System.Drawing.Point(396, 99);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(88, 28);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 4;
		this.ultraButton1.Text = "詳細資料...";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.GridUnit1);
		this.panel1.Controls.Add(this.ultraStatusBar1);
		this.panel1.Location = new System.Drawing.Point(7, 144);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(478, 160);
		this.panel1.TabIndex = 5;
		this.GridUnit1._ExcelSheeName = "";
		this.GridUnit1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.GridUnit1.AllowEditing = false;
		this.GridUnit1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridUnit1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.GridUnit1.ColumnInfo = "4,1,0,0,0,110,Columns:0{Width:17;Name:\"RowIndicator\";AllowDragging:False;AllowResizing:False;AllowEditing:False;DataType:System.Int32;TextAlign:RightTop;TextAlignFixed:GeneralTop;}\t1{Width:120;Name:\"UserName\";Caption:\"使用者\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t2{Width:150;Name:\"PccesCode\";Caption:\"工項代碼\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t3{Width:100;Name:\"CName\";Caption:\"工項名稱\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t";
		this.GridUnit1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridUnit1.ExtendLastCol = true;
		this.GridUnit1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridUnit1.ForeColor = System.Drawing.Color.Black;
		this.GridUnit1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.GridUnit1.IsProcessUndo = false;
		this.GridUnit1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.GridUnit1.Location = new System.Drawing.Point(0, 0);
		this.GridUnit1.Name = "GridUnit1";
		this.GridUnit1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.GridUnit1.ShowCursor = true;
		this.GridUnit1.ShowToolTipOnNarrowColumn = true;
		this.GridUnit1.Size = new System.Drawing.Size(476, 135);
		this.GridUnit1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11.25pt;BackColor:237, 243, 254;ForeColor:Black;TextAlign:GeneralBottom;Border:Flat,1,Silver,Both;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;TextAlign:GeneralTop;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;TextAlign:GeneralCenter;ImageAlign:CenterCenter;Border:None,1,Black,Both;Format:\"###,###,###,##0\";}\tFocus{Font:細明體, 11.25pt;BackColor:102, 153, 255;Margins:0, 0, 0, 0;TextAlign:GeneralCenter;Border:None,1,Black,Both;}\tSearch{BackColor:Highlight;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.GridUnit1.TabIndex = 12;
		this.GridUnit1.UndoMax = 10;
		appearance4.BackColor = System.Drawing.SystemColors.Control;
		appearance4.FontData.SizeInPoints = 11f;
		this.ultraStatusBar1.Appearance = appearance4;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 135);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		ultraStatusPanel1.Text = "資料筆數:";
		ultraStatusPanel1.Width = 200;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[1] { ultraStatusPanel1 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(476, 23);
		this.ultraStatusBar1.SupportThemes = false;
		this.ultraStatusBar1.TabIndex = 13;
		this.ultraStatusBar1.Text = "ultraStatusBar1";
		this.PicQuestion.BorderShadowColor = System.Drawing.Color.Empty;
		this.PicQuestion.Image = resources.GetObject("PicQuestion.Image");
		this.PicQuestion.ImageTransparentColor = System.Drawing.Color.FromArgb(209, 205, 211);
		this.PicQuestion.Location = new System.Drawing.Point(16, 8);
		this.PicQuestion.Name = "PicQuestion";
		this.PicQuestion.ScaleImage = Infragistics.Win.ScaleImage.Always;
		this.PicQuestion.Size = new System.Drawing.Size(40, 48);
		this.PicQuestion.TabIndex = 6;
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_OK.Appearance = appearance5;
		this.D_Btn_OK.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_OK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.D_Btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.D_Btn_OK.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.D_Btn_OK.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_OK.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_OK.Location = new System.Drawing.Point(205, 99);
		this.D_Btn_OK.Name = "D_Btn_OK";
		this.D_Btn_OK.ShowFocusRect = false;
		this.D_Btn_OK.ShowOutline = false;
		this.D_Btn_OK.Size = new System.Drawing.Size(88, 28);
		this.D_Btn_OK.SupportThemes = false;
		this.D_Btn_OK.TabIndex = 7;
		this.D_Btn_OK.Text = "確定(&O)";
		this.PicWarning.BorderShadowColor = System.Drawing.Color.Empty;
		this.PicWarning.Image = resources.GetObject("PicWarning.Image");
		this.PicWarning.ImageTransparentColor = System.Drawing.Color.FromArgb(209, 205, 211);
		this.PicWarning.Location = new System.Drawing.Point(16, 8);
		this.PicWarning.Name = "PicWarning";
		this.PicWarning.ScaleImage = Infragistics.Win.ScaleImage.Always;
		this.PicWarning.Size = new System.Drawing.Size(40, 48);
		this.PicWarning.TabIndex = 8;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.ClientSize = new System.Drawing.Size(492, 140);
		base.Controls.Add(this.PicWarning);
		base.Controls.Add(this.D_Btn_OK);
		base.Controls.Add(this.PicQuestion);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.ultraButton1);
		base.Controls.Add(this.D_Btn_No);
		base.Controls.Add(this.D_Btn_Yes);
		base.Controls.Add(this.ultraLabel1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormMrsBase_DeleteMessage";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		base.Load += new System.EventHandler(FormMrsBase_DeleteMessage_Load);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).EndInit();
		base.ResumeLayout(false);
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		if (base.Height > 172)
		{
			base.Height = 172;
		}
		else if (base.Height < 344)
		{
			base.Height = 344;
		}
	}

	private void FormMrsBase_DeleteMessage_Load(object sender, EventArgs e)
	{
		if (F_DTCannotDelete.Rows.Count == 1 && F_iSel == 1)
		{
			D_Btn_Yes.Visible = false;
			D_Btn_No.Visible = false;
			D_Btn_OK.Visible = true;
		}
		else
		{
			D_Btn_Yes.Visible = true;
			D_Btn_No.Visible = true;
			D_Btn_OK.Visible = false;
		}
		ultraLabel1.Text = F_Message;
		if (F_MessageIcon == MessageBoxIcon.Question)
		{
			PicWarning.Visible = false;
			PicQuestion.Visible = true;
		}
		else if (F_MessageIcon == MessageBoxIcon.Exclamation)
		{
			PicWarning.Visible = true;
			PicQuestion.Visible = false;
		}
		if (F_SrcKind.ToUpper() != "MRS")
		{
			GridUnit1.Cols[2].Caption = "項次";
			GridUnit1.Cols[3].Caption = "項目及說明";
		}
		else
		{
			GridUnit1.Cols[2].Caption = "工項代碼";
			GridUnit1.Cols[3].Caption = "工項名稱";
		}
		BindToGrid();
	}

	private void BindToGrid()
	{
		GridUnit1.Rows.Count = F_DTCannotDelete.Rows.Count + 1;
		for (int i = 0; i < F_DTCannotDelete.Rows.Count; i++)
		{
			GridUnit1[i + 1, "UserName"] = "(" + F_DTCannotDelete.Rows[i]["UserID"].ToString().Trim() + ")" + F_DTCannotDelete.Rows[i]["UserName"].ToString().Trim();
			GridUnit1[i + 1, "PccesCode"] = F_DTCannotDelete.Rows[i]["PccesCode"].ToString().Trim();
			GridUnit1[i + 1, "CName"] = F_DTCannotDelete.Rows[i]["CName"].ToString().Trim();
		}
		ultraStatusBar1.Panels[0].Text = "資料筆數：" + F_DTCannotDelete.Rows.Count;
	}
}
