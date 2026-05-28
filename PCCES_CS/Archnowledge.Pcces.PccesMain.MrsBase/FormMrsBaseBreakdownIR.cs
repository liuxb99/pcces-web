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

namespace Archnowledge.Pcces.PccesMain.MrsBase;

public class FormMrsBaseBreakdownIR : Form
{
	private Panel panel1;

	private UltraLabel lblChild;

	private UltraLabel lblParent;

	private Panel panel2;

	private UltraButton ultraButton1;

	private UltraButton ultraButton3;

	private Panel panel3;

	private GridBudget c1FlexGrid1;

	private IContainer components;

	private string F_UserID;

	private string ls_ParentCode = "";

	private string ls_PubCode = "";

	private string ls_srcKind = "";

	private string ls_ProjectCode = "";

	private string ls_ListNo = "";

	private DataTable ldt_Analysis = new DataTable();

	private string F_ProjectCode = "";

	private PccesFormAction F_ActionName;

	private string F_ParentCode;

	private string F_ParentPccesCode;

	private string F_ParentCName;

	private string F_PubCode;

	private string F_SonPccesCode;

	private string F_SonCName;

	private string F_ListNo;

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

	public PccesFormAction _ActionName
	{
		get
		{
			return F_ActionName;
		}
		set
		{
			F_ActionName = value;
		}
	}

	public string _ParentCode
	{
		get
		{
			return F_ParentCode;
		}
		set
		{
			F_ParentCode = value;
		}
	}

	public string _ParentPccesCode
	{
		get
		{
			return F_ParentPccesCode;
		}
		set
		{
			F_ParentPccesCode = value;
		}
	}

	public string _ParentCName
	{
		get
		{
			return F_ParentCName;
		}
		set
		{
			F_ParentCName = value;
		}
	}

	public string _PubCode
	{
		get
		{
			return F_PubCode;
		}
		set
		{
			F_PubCode = value;
		}
	}

	public string _SonPccesCode
	{
		get
		{
			return F_SonPccesCode;
		}
		set
		{
			F_SonPccesCode = value;
		}
	}

	public string _SonCName
	{
		get
		{
			return F_SonCName;
		}
		set
		{
			F_SonCName = value;
		}
	}

	public string _ListNo
	{
		get
		{
			return F_ListNo;
		}
		set
		{
			F_ListNo = value;
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.MrsBase.FormMrsBaseBreakdownIR));
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.lblChild = new Infragistics.Win.Misc.UltraLabel();
		this.lblParent = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton3 = new Infragistics.Win.Misc.UltraButton();
		this.panel3 = new System.Windows.Forms.Panel();
		this.c1FlexGrid1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).BeginInit();
		base.SuspendLayout();
		this.panel1.Controls.Add(this.lblChild);
		this.panel1.Controls.Add(this.lblParent);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(580, 72);
		this.panel1.TabIndex = 4;
		appearance1.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance1.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance1.FontData.Name = "新細明體";
		appearance1.FontData.SizeInPoints = 12f;
		appearance1.ForeColor = System.Drawing.Color.White;
		appearance1.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblChild.Appearance = appearance1;
		this.lblChild.Dock = System.Windows.Forms.DockStyle.Top;
		this.lblChild.Font = new System.Drawing.Font("細明體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblChild.Location = new System.Drawing.Point(0, 36);
		this.lblChild.Name = "lblChild";
		this.lblChild.Size = new System.Drawing.Size(580, 36);
		this.lblChild.TabIndex = 5;
		this.lblChild.Text = "子項:";
		appearance2.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance2.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance2.FontData.Name = "新細明體";
		appearance2.FontData.SizeInPoints = 12f;
		appearance2.ForeColor = System.Drawing.Color.White;
		appearance2.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblParent.Appearance = appearance2;
		this.lblParent.Dock = System.Windows.Forms.DockStyle.Top;
		this.lblParent.Font = new System.Drawing.Font("細明體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblParent.Location = new System.Drawing.Point(0, 0);
		this.lblParent.Name = "lblParent";
		this.lblParent.Size = new System.Drawing.Size(580, 36);
		this.lblParent.TabIndex = 4;
		this.lblParent.Text = "父項:";
		this.panel2.Controls.Add(this.ultraButton1);
		this.panel2.Controls.Add(this.ultraButton3);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 369);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(580, 40);
		this.panel2.TabIndex = 5;
		this.ultraButton1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance3.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance3.Image = resources.GetObject("appearance3.Image");
		this.ultraButton1.Appearance = appearance3;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.ultraButton1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton1.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton1.Location = new System.Drawing.Point(396, 5);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(88, 31);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 9;
		this.ultraButton1.Text = "確 定";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		this.ultraButton3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance4.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance4.Image = resources.GetObject("appearance4.Image");
		this.ultraButton3.Appearance = appearance4;
		this.ultraButton3.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton3.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton3.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton3.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton3.Location = new System.Drawing.Point(488, 5);
		this.ultraButton3.Name = "ultraButton3";
		this.ultraButton3.ShowFocusRect = false;
		this.ultraButton3.ShowOutline = false;
		this.ultraButton3.Size = new System.Drawing.Size(88, 31);
		this.ultraButton3.SupportThemes = false;
		this.ultraButton3.TabIndex = 8;
		this.ultraButton3.Text = "取 消";
		this.panel3.Controls.Add(this.c1FlexGrid1);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 72);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(580, 297);
		this.panel3.TabIndex = 6;
		this.c1FlexGrid1._ExcelFileName = "";
		this.c1FlexGrid1._ExcelSheeName = "";
		this.c1FlexGrid1._IsOpenExcelAfterExport = false;
		this.c1FlexGrid1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.c1FlexGrid1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.c1FlexGrid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.c1FlexGrid1.ColumnInfo = "7,1,0,0,0,110,Columns:0{Width:20;Name:\"RowIndicator\";DataType:System.Int32;TextAlign:RightCenter;}\t1{Width:40;Name:\"IsCheck\";Caption:\"勾選\";DataType:System.Boolean;ImageAlign:CenterCenter;}\t2{Width:50;Name:\"ListNo\";Caption:\"項次\";AllowEditing:False;DataType:System.Int32;TextAlign:RightCenter;}\t3{Width:100;Name:\"PccesCode\";Caption:\"工項代碼\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t4{Width:200;Name:\"CName\";Caption:\"工項名稱\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t5{Width:80;Name:\"UnitName\";Caption:\"單位\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t6{Name:\"PubCode\";Caption:\"PubCode\";AllowEditing:False;DataType:System.Int32;TextAlign:RightCenter;}\t";
		this.c1FlexGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1FlexGrid1.ExtendLastCol = true;
		this.c1FlexGrid1.ForeColor = System.Drawing.SystemColors.WindowText;
		this.c1FlexGrid1.Location = new System.Drawing.Point(0, 0);
		this.c1FlexGrid1.Name = "c1FlexGrid1";
		this.c1FlexGrid1.Rows.Count = 1;
		this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.c1FlexGrid1.ShowToolTipOnNarrowColumn = true;
		this.c1FlexGrid1.Size = new System.Drawing.Size(580, 297);
		this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11.25pt;BackColor:237, 243, 254;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;ForeColor:ControlText;Border:Flat,1,ControlDark,Both;}\tHighlight{BackColor:102, 153, 255;ForeColor:Black;}\tFocus{BackColor:102, 153, 255;}\tSearch{BackColor:Highlight;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.c1FlexGrid1.TabIndex = 4;
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.ultraButton3;
		base.ClientSize = new System.Drawing.Size(580, 409);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		base.KeyPreview = true;
		base.Name = "FormMrsBaseBreakdownIR";
		this.Text = "加總項目設定";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormMrsBaseBreakdownIR_KeyDown);
		base.Load += new System.EventHandler(FormMrsBaseBreakdownIR_Load);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).EndInit();
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

	public FormMrsBaseBreakdownIR()
	{
		InitializeComponent();
	}

	private void HideCols(bool IsHide)
	{
		if (IsHide)
		{
			c1FlexGrid1.Cols["PubCode"].Visible = false;
		}
	}

	private void SetTitle()
	{
		lblParent.Text = "父項: " + F_ParentPccesCode + "【" + F_ParentCName + "】";
		lblChild.Text = "子項: " + F_SonPccesCode + "【" + F_SonCName + "】";
	}

	private void FormMrsBaseBreakdownIR_Load(object sender, EventArgs e)
	{
		ls_ParentCode = F_ParentCode;
		ls_PubCode = F_PubCode;
		ls_srcKind = CommonMethods.GetActionNameString(F_ActionName);
		ls_ProjectCode = F_ProjectCode;
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(SetTotal) 單價分析公式設定");
		int li_ParentCode = PubTools.Str2Int(ls_ParentCode);
		int li_PubCode = PubTools.Str2Int(ls_PubCode);
		int li_ListNo = PubTools.Str2Int(F_ListNo);
		MrsBaseB MrsBCom = new MrsBaseB(tmp_AL1);
		MrsBCom.ps_srckind = ls_srcKind;
		MrsBCom.ps_projectcode = ls_ProjectCode;
		DataTable ldt_Tmp = MrsBCom.ListItem(li_ParentCode);
		foreach (DataRow dr in ldt_Tmp.Rows)
		{
			if (PubTools.GetAppSet_Bool("UseNewMrsB"))
			{
				if (PubTools.Str2Int(dr["ListNo"].ToString()) == li_PubCode)
				{
					li_ListNo = li_PubCode;
					li_PubCode = PubTools.Str2Int(dr["PubCode"].ToString());
					break;
				}
			}
			else if (PubTools.Str2Int(dr["PubCode"].ToString()) == li_PubCode)
			{
				li_ListNo = PubTools.Str2Int(dr["ListNo"].ToString());
			}
		}
		ls_ListNo = li_ListNo.ToString();
		ls_PubCode = li_PubCode.ToString();
		SetTitle();
		MrsBaseC MrsCCom = new MrsBaseC(tmp_AL1);
		MrsCCom.ps_srckind = ls_srcKind;
		MrsCCom.ps_projectcode = ls_ProjectCode;
		if (PubTools.GetAppSet_Bool("UseNewMrsB"))
		{
			ldt_Analysis = MrsCCom.ListItem(li_ParentCode, li_PubCode, li_ListNo);
		}
		else
		{
			ldt_Analysis = MrsCCom.ListItem(li_ParentCode, li_PubCode);
		}
		MrsCCom = null;
		BindToGrid();
	}

	private void BindToGrid()
	{
		c1FlexGrid1.Rows.Count = ldt_Analysis.Rows.Count + 1;
		for (int i = 0; i < ldt_Analysis.Rows.Count; i++)
		{
			c1FlexGrid1[i + 1, "IsCheck"] = (bool)ldt_Analysis.Rows[i]["Chk"];
			c1FlexGrid1[i + 1, "ListNo"] = ldt_Analysis.Rows[i]["ListNo"].ToString().Trim();
			c1FlexGrid1[i + 1, "PccesCode"] = ldt_Analysis.Rows[i]["PccesCode"].ToString().Trim();
			c1FlexGrid1[i + 1, "cName"] = ldt_Analysis.Rows[i]["cName"].ToString().Trim();
			c1FlexGrid1[i + 1, "UnitName"] = ldt_Analysis.Rows[i]["UnitName"].ToString().Trim();
			c1FlexGrid1[i + 1, "PubCode"] = ldt_Analysis.Rows[i]["PubCode"].ToString().Trim();
		}
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
		DataTable AnalysisDT = ldt_Analysis.Copy();
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(SetTotal) 單價分析公式設定");
		MrsBaseC MrsCCom = new MrsBaseC(tmp_AL1);
		MrsCCom.ps_srckind = ls_srcKind;
		MrsCCom.ps_projectcode = ls_ProjectCode;
		int li_PubListNo = PubTools.Str2Int(ls_ListNo);
		int li_ParentCode = PubTools.Str2Int(ls_ParentCode);
		int li_PubCode = PubTools.Str2Int(ls_PubCode);
		if (PubTools.GetAppSet_Bool("UseNewMrsB"))
		{
			MrsCCom.DeleItemAll(li_ParentCode, li_PubCode, li_PubListNo);
		}
		else
		{
			MrsCCom.DeleItemAll(li_ParentCode, li_PubCode);
		}
		for (int i = 1; i < c1FlexGrid1.Rows.Count; i++)
		{
			AnalysisDT.Rows[i - 1]["Chk"] = (bool)c1FlexGrid1.Rows[i]["IsCheck"];
		}
		foreach (DataRow dr in AnalysisDT.Rows)
		{
			if ((bool)dr["Chk"])
			{
				int li_ItemCode = PubTools.Str2Int(dr["pubCode"].ToString());
				int li_ItemListNo = PubTools.Str2Int(dr["ListNo"].ToString());
				MrsCCom.InseItem(li_ParentCode, li_PubCode, li_ItemCode, li_PubListNo, li_ItemListNo);
			}
		}
		MrsCCom = null;
		PubTools.WriteRoughlyLog(tmp_AL1);
	}

	private void FormMrsBaseBreakdownIR_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.End)
		{
			c1FlexGrid1.Row = c1FlexGrid1.Rows.Count - 1;
		}
		if (e.KeyCode == Keys.Home)
		{
			c1FlexGrid1.Row = 1;
		}
	}
}
