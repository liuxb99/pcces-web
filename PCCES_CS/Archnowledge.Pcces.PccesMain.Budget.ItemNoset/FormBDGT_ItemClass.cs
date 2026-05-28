using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.MrsBase;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.Budget.ItemNoset;

public class FormBDGT_ItemClass : Form
{
	private Panel panel2;

	private UltraButton ultraButton6;

	private UltraButton A_Btn_Cncl;

	private Panel panel1;

	private UltraButton btnOK;

	private UltraButton btnCancel;

	private GridBudget GridUnit1;

	private IContainer components;

	private DataTable DT1 = new DataTable();

	private DataTable DT = new DataTable();

	private int iPubCode = -1;

	private string F_UserID;

	private string F_status;

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

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.ItemNoset.FormBDGT_ItemClass));
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		this.GridUnit1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.btnOK = new Infragistics.Win.Misc.UltraButton();
		this.btnCancel = new Infragistics.Win.Misc.UltraButton();
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
		this.GridUnit1.ColumnInfo = resources.GetString("GridUnit1.ColumnInfo");
		this.GridUnit1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridUnit1.ExtendLastCol = true;
		this.GridUnit1.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
		this.GridUnit1.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridUnit1.ForeColor = System.Drawing.Color.Black;
		this.GridUnit1.Location = new System.Drawing.Point(0, 0);
		this.GridUnit1.Name = "GridUnit1";
		this.GridUnit1.Rows.Count = 19;
		this.GridUnit1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.GridUnit1.ShowCursor = true;
		this.GridUnit1.ShowToolTipOnNarrowColumn = true;
		this.GridUnit1.Size = new System.Drawing.Size(344, 330);
		this.GridUnit1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("GridUnit1.Styles"));
		this.GridUnit1.TabIndex = 4;
		this.GridUnit1.Tree.Column = 1;
		this.GridUnit1.Tree.LineColor = System.Drawing.Color.Gray;
		this.panel2.Controls.Add(this.panel1);
		this.panel2.Controls.Add(this.ultraButton6);
		this.panel2.Controls.Add(this.A_Btn_Cncl);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 330);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(344, 36);
		this.panel2.TabIndex = 3;
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.btnOK);
		this.panel1.Controls.Add(this.btnCancel);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(344, 36);
		this.panel1.TabIndex = 7;
		this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnOK.Appearance = appearance1;
		this.btnOK.BackColor = System.Drawing.SystemColors.Control;
		this.btnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOK.Font = new System.Drawing.Font("細明體", 11f);
		this.btnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.btnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.btnOK.Location = new System.Drawing.Point(156, 4);
		this.btnOK.Name = "btnOK";
		this.btnOK.ShowFocusRect = false;
		this.btnOK.ShowOutline = false;
		this.btnOK.Size = new System.Drawing.Size(88, 31);
		this.btnOK.SupportThemes = false;
		this.btnOK.TabIndex = 6;
		this.btnOK.Text = "確定";
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
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
		this.btnCancel.Location = new System.Drawing.Point(248, 4);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.ShowFocusRect = false;
		this.btnCancel.ShowOutline = false;
		this.btnCancel.Size = new System.Drawing.Size(88, 31);
		this.btnCancel.SupportThemes = false;
		this.btnCancel.TabIndex = 5;
		this.btnCancel.Text = "取消";
		this.ultraButton6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton6.Appearance = appearance3;
		this.ultraButton6.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton6.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton6.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.ultraButton6.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton6.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton6.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton6.Location = new System.Drawing.Point(156, 4);
		this.ultraButton6.Name = "ultraButton6";
		this.ultraButton6.ShowFocusRect = false;
		this.ultraButton6.ShowOutline = false;
		this.ultraButton6.Size = new System.Drawing.Size(88, 31);
		this.ultraButton6.SupportThemes = false;
		this.ultraButton6.TabIndex = 6;
		this.ultraButton6.Text = "確定";
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
		this.A_Btn_Cncl.Location = new System.Drawing.Point(248, 4);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.ShowFocusRect = false;
		this.A_Btn_Cncl.ShowOutline = false;
		this.A_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Cncl.SupportThemes = false;
		this.A_Btn_Cncl.TabIndex = 5;
		this.A_Btn_Cncl.Text = "取消";
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(344, 366);
		base.Controls.Add(this.GridUnit1);
		base.Controls.Add(this.panel2);
		base.Name = "FormBDGT_ItemClass";
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "類別挑選";
		base.Load += new System.EventHandler(FormBDGT_ItemClass_Load);
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

	public FormBDGT_ItemClass()
	{
		InitializeComponent();
	}

	private void FormBDGT_ItemClass_Load(object sender, EventArgs e)
	{
		if (!(F_status != "PickList"))
		{
			return;
		}
		LoadRightData();
		BindToGrid();
		if (!(F_status != "choose"))
		{
			return;
		}
		string PickNum = CommonMethods.IniReadValue(F_SettingPick, "PickType", "PickName");
		if (!(PickNum != string.Empty))
		{
			return;
		}
		string[] s = PickNum.Split(',');
		if (s.Length > 0)
		{
			for (int i = 0; i < s.Length; i++)
			{
				BindToGrid(PubTools.Str2Int(s[i]));
			}
		}
	}

	private void LoadRightData()
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("(UserDefind_Show) 顯示常用字串資料");
		UserDefind UserCom = new UserDefind(aArr);
		DT1 = UserCom.ListItem("Class");
		string ls_selectstr = "Select * from MrsY where pubCode=" + iPubCode + "";
		ModifyDB StdCom = new ModifyDB("", aArr);
		DT = StdCom.DBList(ls_selectstr);
		StdCom = null;
	}

	private void BindToGrid()
	{
		string sNo = "";
		GridUnit1.Rows.Count = DT1.Rows.Count + 1;
		for (int i = 0; i < DT1.Rows.Count; i++)
		{
			DataView dv = new DataView(DT);
			GridUnit1[i + 1, "sNo"] = DT1.Rows[i]["sNo"].ToString().Trim();
			GridUnit1[i + 1, "MainName"] = DT1.Rows[i]["cString"].ToString().Trim();
			sNo = DT1.Rows[i]["sNo"].ToString().Trim();
			dv.RowFilter = "numberCode = '" + sNo + "'";
			if (dv.Count > 0)
			{
				GridUnit1[i + 1, "Selected"] = true;
			}
			else
			{
				GridUnit1[i + 1, "Selected"] = false;
			}
		}
		GridUnit1.AutoSizeCols();
	}

	private void BindToGrid(int rowNumber)
	{
		GridUnit1[rowNumber, "Selected"] = true;
		GridUnit1.AutoSizeCols();
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		string strClass = "";
		string numberCode = "";
		string sTableName = "";
		string strpubCode = "";
		DataTable DTClass = new DataTable();
		if (F_status == "choose")
		{
			sTableName = "MrsA";
			DeleClassMrsA(iPubCode, sTableName);
			sTableName = "MrsY";
			DeleClassMrsA(iPubCode, sTableName);
			for (int i = 1; i < GridUnit1.Rows.Count; i++)
			{
				if ((bool)GridUnit1[i, "Selected"])
				{
					strClass = strClass + GridUnit1[i, "MainName"].ToString() + ",";
					numberCode = GridUnit1[i, "sNo"].ToString();
					InsClassMrsY(iPubCode, Convert.ToInt32(numberCode));
				}
			}
			if (strClass.Length > 0)
			{
				strClass = strClass.Substring(0, strClass.Length - 1);
				InsClassMrsA(iPubCode, strClass);
			}
			(base.Owner as FormMrsBaseEdit)._Cstring = strClass;
		}
		else if (F_status == "search1")
		{
			for (int i = 1; i < GridUnit1.Rows.Count; i++)
			{
				if ((bool)GridUnit1[i, "Selected"])
				{
					numberCode = numberCode + GridUnit1[i, "sNo"].ToString() + ",";
				}
			}
			if (numberCode.Length <= 0)
			{
				MessageBox.Show(this, "尚未選定挑選類別", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CommonMethods.IniWriteValue(F_SettingPick, "PickType", "PickName", numberCode);
				return;
			}
			numberCode = numberCode.Substring(0, numberCode.Length - 1);
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(F_UserID);
			aArr.Add("(UserDefind_Show) 顯示常用字串資料");
			string ls_selectstr = "select Distinct A.* from mrsA A inner join MrsY B on A.pubcode=B.pubcode where B.numberCode in (" + numberCode + ")";
			ModifyDB StdCom = new ModifyDB("", aArr);
			DTClass = StdCom.DBList(ls_selectstr);
			StdCom = null;
			if (DTClass.Rows.Count > 0)
			{
				for (int i = 0; i < DTClass.Rows.Count; i++)
				{
					strpubCode = strpubCode + DTClass.Rows[i]["pubCode"].ToString().Trim() + ",";
				}
			}
			if (strpubCode.Length > 0)
			{
				strpubCode = strpubCode.Substring(0, strpubCode.Length - 1);
			}
			if (F_status == "search1")
			{
				(base.Owner as frmMrsBase)._Cstring = strpubCode;
			}
			CommonMethods.IniWriteValue(F_SettingPick, "PickType", "PickName", numberCode);
		}
		else if (F_status == "search2")
		{
			for (int i = 1; i < GridUnit1.Rows.Count; i++)
			{
				if ((bool)GridUnit1[i, "Selected"])
				{
					numberCode = numberCode + GridUnit1[i, "sNo"].ToString() + ",";
				}
			}
			if (numberCode.Length > 0)
			{
				numberCode = numberCode.Substring(0, numberCode.Length - 1);
			}
			CommonMethods.IniWriteValue(F_SettingPick, "PickType", "PickName", numberCode);
		}
		else if (F_PickList.Count > 0)
		{
			for (int i = 0; i < F_PickList.Count; i++)
			{
				sTableName = "MrsA";
				DeleClassMrsA(PubTools.Str2Int(F_PickList[i].ToString().Trim()), sTableName);
				sTableName = "MrsY";
				DeleClassMrsA(PubTools.Str2Int(F_PickList[i].ToString().Trim()), sTableName);
				for (int j = 1; j < GridUnit1.Rows.Count; j++)
				{
					if ((bool)GridUnit1[j, "Selected"])
					{
						strClass = strClass + GridUnit1[j, "MainName"].ToString() + ",";
						numberCode = GridUnit1[j, "sNo"].ToString();
						InsClassMrsY(PubTools.Str2Int(F_PickList[i].ToString().Trim()), Convert.ToInt32(numberCode));
					}
				}
			}
		}
		base.DialogResult = DialogResult.OK;
	}

	private void DeleClassMrsA(int PubCode, string TableName)
	{
		ArrayList lal_LogData = new ArrayList();
		if (PubCode != -1)
		{
			string ls_selectstr = "delete ";
			ls_selectstr = ls_selectstr + TableName + " where pubCode=" + PubCode + "";
			lal_LogData.Add(F_UserID);
			lal_LogData.Add("刪除類別名稱");
			ModifyDB StdCom = new ModifyDB("", lal_LogData);
			StdCom.DBDele(ls_selectstr);
			StdCom = null;
		}
		lal_LogData = null;
	}

	private void InsClassMrsA(int PubCode, string strClass)
	{
		ArrayList lal_LogData = new ArrayList();
		string ls_selectstr = "insert into MrsA (pubCode,cString) values (" + PubCode + ",'" + strClass + "')";
		lal_LogData.Add(F_UserID);
		lal_LogData.Add("新增類別名稱");
		ModifyDB StdCom = new ModifyDB("", lal_LogData);
		StdCom.DBInse(ls_selectstr);
		StdCom = null;
		lal_LogData = null;
	}

	private void InsClassMrsY(int PubCode, int numberCode)
	{
		ArrayList lal_LogData = new ArrayList();
		string ls_selectstr = "insert into MrsY (pubCode,numberCode) values (" + PubCode + "," + numberCode + ")";
		lal_LogData.Add(F_UserID);
		lal_LogData.Add("新增類別名稱");
		ModifyDB StdCom = new ModifyDB("", lal_LogData);
		StdCom.DBInse(ls_selectstr);
		StdCom = null;
		lal_LogData = null;
	}
}
