using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.Budget.ItemNoset;

public class FormBDGT_TemplateClass : Form
{
	private Panel panel2;

	private UltraButton ultraButton6;

	private UltraButton A_Btn_Cncl;

	private Panel panel1;

	private UltraButton ultraButton1;

	private UltraButton ultraButton2;

	private GridBudget GridUnit1;

	private UltraLabel ultraLabel2;

	private IContainer components;

	private DataTable DT1 = new DataTable();

	private DataTable DT = new DataTable();

	private int iPubCode = -1;

	private string F_UserID;

	private string F_status;

	private string F_ProjectCode;

	private ArrayList F_PickList = new ArrayList();

	private string F_SettingPick = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "MrsBase.ini";

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

	public int _PubCode
	{
		get
		{
			return iPubCode;
		}
		set
		{
			iPubCode = value;
		}
	}

	public string _status
	{
		get
		{
			return F_status;
		}
		set
		{
			F_status = value;
		}
	}

	public ArrayList _PickList
	{
		get
		{
			return F_PickList;
		}
		set
		{
			F_PickList = value;
		}
	}

	public string _ProjectCode
	{
		get
		{
			return F_ProjectCode;
		}
		set
		{
			F_ProjectCode = value;
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.ItemNoset.FormBDGT_TemplateClass));
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		this.GridUnit1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton6 = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).BeginInit();
		this.panel2.SuspendLayout();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.GridUnit1._ExcelFileName = "";
		this.GridUnit1._ExcelSheeName = "";
		this.GridUnit1._IsOpenExcelAfterExport = false;
		this.GridUnit1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridUnit1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.GridUnit1.ColumnInfo = "6,0,0,0,0,110,Columns:0{Width:40;Name:\"Selected\";Caption:\"選擇\";DataType:System.Boolean;TextAlignFixed:GeneralTop;ImageAlign:CenterCenter;}\t1{Width:100;Name:\"ProjectCode\";Caption:\"工程代碼\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t2{Width:250;Name:\"CName\";Caption:\"工程名稱\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t3{Name:\"EName\";Caption:\"Project Name\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t4{Width:150;Name:\"Address\";Caption:\"工程地點\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t5{Name:\"sNo\";Visible:False;DataType:System.Int32;TextAlign:RightCenter;TextAlignFixed:GeneralTop;}\t";
		this.GridUnit1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridUnit1.ExtendLastCol = true;
		this.GridUnit1.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
		this.GridUnit1.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridUnit1.ForeColor = System.Drawing.Color.Black;
		this.GridUnit1.Location = new System.Drawing.Point(0, 0);
		this.GridUnit1.Name = "GridUnit1";
		this.GridUnit1.Rows.Count = 1;
		this.GridUnit1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.GridUnit1.ShowCursor = true;
		this.GridUnit1.ShowToolTipOnNarrowColumn = true;
		this.GridUnit1.Size = new System.Drawing.Size(616, 370);
		this.GridUnit1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11pt;BackColor:237, 243, 254;ForeColor:Black;Border:Flat,1,Silver,Both;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;TextAlign:GeneralTop;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;TextAlign:LeftCenter;Format:\"###,###,###,##0\";}\tFocus{Font:細明體, 11.25pt;BackColor:102, 153, 255;Border:None,1,Black,Both;}\tSearch{Font:細明體, 9.75pt;BackColor:White;ForeColor:HighlightText;Border:Double,1,96, 145, 234,Both;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.GridUnit1.TabIndex = 4;
		this.GridUnit1.Tree.Column = 1;
		this.GridUnit1.Tree.LineColor = System.Drawing.Color.Gray;
		this.GridUnit1.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(GridUnit1_AfterEdit);
		this.panel2.Controls.Add(this.panel1);
		this.panel2.Controls.Add(this.ultraButton6);
		this.panel2.Controls.Add(this.A_Btn_Cncl);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 370);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(616, 36);
		this.panel2.TabIndex = 3;
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.ultraLabel2);
		this.panel1.Controls.Add(this.ultraButton1);
		this.panel1.Controls.Add(this.ultraButton2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(616, 36);
		this.panel1.TabIndex = 7;
		appearance1.ForeColor = System.Drawing.Color.Red;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance1;
		this.ultraLabel2.Font = new System.Drawing.Font("新細明體", 12f);
		this.ultraLabel2.Location = new System.Drawing.Point(16, 8);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(368, 23);
		this.ultraLabel2.TabIndex = 25;
		this.ultraLabel2.Text = "注意：一次只能選擇一個範本專案";
		this.ultraButton1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton1.Appearance = appearance2;
		this.ultraButton1.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton1.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton1.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton1.Location = new System.Drawing.Point(428, 4);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(88, 31);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 6;
		this.ultraButton1.Text = "確定";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		this.ultraButton2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton2.Appearance = appearance3;
		this.ultraButton2.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton2.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton2.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton2.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton2.Location = new System.Drawing.Point(520, 4);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(88, 31);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 5;
		this.ultraButton2.Text = "取消";
		this.ultraButton6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance4.Image = resources.GetObject("appearance4.Image");
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton6.Appearance = appearance4;
		this.ultraButton6.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton6.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton6.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.ultraButton6.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton6.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton6.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton6.Location = new System.Drawing.Point(428, 4);
		this.ultraButton6.Name = "ultraButton6";
		this.ultraButton6.ShowFocusRect = false;
		this.ultraButton6.ShowOutline = false;
		this.ultraButton6.Size = new System.Drawing.Size(88, 31);
		this.ultraButton6.SupportThemes = false;
		this.ultraButton6.TabIndex = 6;
		this.ultraButton6.Text = "確定";
		this.A_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance5.Image = resources.GetObject("appearance5.Image");
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Cncl.Appearance = appearance5;
		this.A_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Cncl.Location = new System.Drawing.Point(520, 4);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.ShowFocusRect = false;
		this.A_Btn_Cncl.ShowOutline = false;
		this.A_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Cncl.SupportThemes = false;
		this.A_Btn_Cncl.TabIndex = 5;
		this.A_Btn_Cncl.Text = "取消";
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(616, 406);
		base.Controls.Add(this.GridUnit1);
		base.Controls.Add(this.panel2);
		base.Name = "FormBDGT_TemplateClass";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "預算書範本";
		base.Load += new System.EventHandler(FormBDGT_TemplateClass_Load);
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).EndInit();
		this.panel2.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
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

	public FormBDGT_TemplateClass()
	{
		InitializeComponent();
	}

	private void FormBDGT_TemplateClass_Load(object sender, EventArgs e)
	{
		LoadData();
		BindToGrid();
	}

	private void LoadData()
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("(UserDefind_Show) 顯示常用字串資料");
		string ls_selectstr = "Select * from budProject where Istemplate='Y'";
		ModifyDB StdCom = new ModifyDB("", aArr);
		DT = StdCom.DBList(ls_selectstr);
		StdCom = null;
		BindToGrid();
	}

	private void BindToGrid()
	{
		GridUnit1.Rows.Count = DT.Rows.Count + 1;
		for (int i = 0; i < DT.Rows.Count; i++)
		{
			GridUnit1[i + 1, "Selected"] = false;
			GridUnit1[i + 1, "ProjectCode"] = DT.Rows[i]["projectCode"].ToString().Trim();
			GridUnit1[i + 1, "CName"] = DT.Rows[i]["projectNameC"].ToString().Trim();
			GridUnit1[i + 1, "EName"] = DT.Rows[i]["projectNameE"].ToString().Trim();
			GridUnit1[i + 1, "Address"] = DT.Rows[i]["projectAddress"].ToString().Trim();
		}
		GridUnit1.AutoSizeCols();
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		string sProject = "";
		bool IsCloneSuccess = false;
		bool IsSelected = false;
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("複製專案");
		Archnowledge.Pcces.BUDClass.Project ProjCom = new Archnowledge.Pcces.BUDClass.Project(aArr);
		ProjCom.ps_srckind = "BUD";
		for (int i = 1; i < GridUnit1.Rows.Count; i++)
		{
			if (!(bool)GridUnit1[i, "Selected"])
			{
				continue;
			}
			IsSelected = true;
			sProject = GridUnit1[i, "ProjectCode"].ToString().Trim();
			if (CheckBudItemA(F_ProjectCode))
			{
				if (MessageBox.Show(this, "此專案尚有資料，是否刪除再重新載入範本?", "訊息", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) != DialogResult.Yes)
				{
					break;
				}
				ProjCom.DeleProjComs(F_ProjectCode);
			}
			IsCloneSuccess = ProjCom.CopyProj(F_ProjectCode, sProject);
		}
		if (!IsSelected)
		{
			MessageBox.Show(this, "請先選擇一個專案", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else if (IsCloneSuccess)
		{
			MessageBox.Show(this, "載入完成", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			base.DialogResult = DialogResult.OK;
		}
	}

	private bool CheckBudItemA(string sProjectCode)
	{
		bool IsItemA = false;
		ArrayList lal_LogData = new ArrayList();
		string ls_selectstr = "select * from budItemA where projectCode = '" + sProjectCode + "'";
		lal_LogData.Add(F_UserID);
		lal_LogData.Add("查詢是否有資料");
		ModifyDB StdCom = new ModifyDB("", lal_LogData);
		DataTable DTBud = StdCom.DBList(ls_selectstr);
		StdCom = null;
		if (DTBud.Rows.Count > 0)
		{
			IsItemA = true;
		}
		lal_LogData = null;
		return IsItemA;
	}

	private void GridUnit1_AfterEdit(object sender, RowColEventArgs e)
	{
		if (GridUnit1.Row < 0)
		{
			return;
		}
		string sProjectCode = GridUnit1[GridUnit1.Row, "ProjectCode"].ToString().Trim();
		for (int i = 1; i < GridUnit1.Rows.Count; i++)
		{
			if (GridUnit1[i, "ProjectCode"].ToString().Trim() == sProjectCode)
			{
				GridUnit1[i, "Selected"] = true;
			}
			else
			{
				GridUnit1[i, "Selected"] = false;
			}
		}
	}
}
