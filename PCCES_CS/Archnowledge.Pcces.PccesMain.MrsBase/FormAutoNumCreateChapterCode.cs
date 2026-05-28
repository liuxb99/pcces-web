using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1Sizer;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.MrsBase;

public class FormAutoNumCreateChapterCode : Form
{
	private const string CallFormHelp = "FormAutoNumCreateChapterCode";

	private Panel panel8;

	private UltraButton A1_Btn_Cncl;

	private GroupBox groupBox4;

	private UltraButton D_Btn_Fnsh;

	private Panel panel1;

	private C1Sizer c1Sizer1;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel5;

	private UltraComboEditor cboType;

	private UltraComboEditor cboResType;

	private UltraTextEditor txtItemCode;

	private UltraTextEditor txtCName;

	private UltraComboEditor cboIsShow;

	private UltraLabel ultraLabel6;

	private NumericUpDown BlankRows;

	private Container components = null;

	private string F_UserID = "";

	private string F_DEPT_ID = "";

	private bool Is_WinFormFlag = false;

	private FormStatus FORM_STATUS = FormStatus.Iinitial;

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

	public string _DEPT_ID
	{
		get
		{
			return F_DEPT_ID;
		}
		set
		{
			F_DEPT_ID = value;
		}
	}

	public FormAutoNumCreateChapterCode()
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
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.MrsBase.FormAutoNumCreateChapterCode));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem6 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem7 = new Infragistics.Win.ValueListItem();
		this.panel8 = new System.Windows.Forms.Panel();
		this.A1_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.D_Btn_Fnsh = new Infragistics.Win.Misc.UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
		this.BlankRows = new System.Windows.Forms.NumericUpDown();
		this.txtItemCode = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.cboResType = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.cboType = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.txtCName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.cboIsShow = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.panel8.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1Sizer1).BeginInit();
		this.c1Sizer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.BlankRows).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtItemCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboResType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboIsShow).BeginInit();
		base.SuspendLayout();
		this.panel8.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel8.Controls.Add(this.A1_Btn_Cncl);
		this.panel8.Controls.Add(this.groupBox4);
		this.panel8.Controls.Add(this.D_Btn_Fnsh);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel8.Location = new System.Drawing.Point(0, 243);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(464, 44);
		this.panel8.TabIndex = 20;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A1_Btn_Cncl.Appearance = appearance1;
		this.A1_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A1_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A1_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A1_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A1_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A1_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A1_Btn_Cncl.Location = new System.Drawing.Point(235, 10);
		this.A1_Btn_Cncl.Name = "A1_Btn_Cncl";
		this.A1_Btn_Cncl.ShowFocusRect = false;
		this.A1_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A1_Btn_Cncl.SupportThemes = false;
		this.A1_Btn_Cncl.TabIndex = 4;
		this.A1_Btn_Cncl.Text = "取消";
		this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox4.Location = new System.Drawing.Point(0, 0);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(464, 8);
		this.groupBox4.TabIndex = 3;
		this.groupBox4.TabStop = false;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Fnsh.Appearance = appearance2;
		this.D_Btn_Fnsh.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Fnsh.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Fnsh.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Fnsh.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Fnsh.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Fnsh.Location = new System.Drawing.Point(143, 10);
		this.D_Btn_Fnsh.Name = "D_Btn_Fnsh";
		this.D_Btn_Fnsh.ShowFocusRect = false;
		this.D_Btn_Fnsh.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Fnsh.SupportThemes = false;
		this.D_Btn_Fnsh.TabIndex = 1;
		this.D_Btn_Fnsh.Text = "存檔";
		this.D_Btn_Fnsh.Click += new System.EventHandler(D_Btn_Fnsh_Click);
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.Controls.Add(this.c1Sizer1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(464, 243);
		this.panel1.TabIndex = 21;
		this.c1Sizer1.AllowDrop = true;
		this.c1Sizer1.Controls.Add(this.BlankRows);
		this.c1Sizer1.Controls.Add(this.txtItemCode);
		this.c1Sizer1.Controls.Add(this.cboResType);
		this.c1Sizer1.Controls.Add(this.cboType);
		this.c1Sizer1.Controls.Add(this.ultraLabel1);
		this.c1Sizer1.Controls.Add(this.ultraLabel2);
		this.c1Sizer1.Controls.Add(this.ultraLabel3);
		this.c1Sizer1.Controls.Add(this.ultraLabel4);
		this.c1Sizer1.Controls.Add(this.ultraLabel5);
		this.c1Sizer1.Controls.Add(this.txtCName);
		this.c1Sizer1.Controls.Add(this.cboIsShow);
		this.c1Sizer1.Controls.Add(this.ultraLabel6);
		this.c1Sizer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1Sizer1.GridDefinition = "5.76131687242798:False:False;12.3456790123457:False:True;12.3456790123457:False:True;12.3456790123457:False:True;12.3456790123457:False:True;12.3456790123457:False:True;12.3456790123457:False:True;5.34979423868313:False:False;\t2.37068965517241:False:False;31.6810344827586:False:True;23.0603448275862:False:False;10.3448275862069:False:False;6.25:False:False;17.2413793103448:False:False;2.1551724137931:False:False;";
		this.c1Sizer1.Location = new System.Drawing.Point(0, 0);
		this.c1Sizer1.Name = "c1Sizer1";
		this.c1Sizer1.Size = new System.Drawing.Size(464, 243);
		this.c1Sizer1.TabIndex = 0;
		this.c1Sizer1.TabStop = false;
		this.BlankRows.Location = new System.Drawing.Point(170, 192);
		this.BlankRows.Name = "BlankRows";
		this.BlankRows.Size = new System.Drawing.Size(107, 25);
		this.BlankRows.TabIndex = 4;
		this.BlankRows.Value = new decimal(new int[4] { 20, 0, 0, 0 });
		this.txtItemCode.Location = new System.Drawing.Point(170, 90);
		this.txtItemCode.MaxLength = 6;
		this.txtItemCode.Name = "txtItemCode";
		this.txtItemCode.Size = new System.Drawing.Size(159, 24);
		this.txtItemCode.TabIndex = 3;
		this.cboResType.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		valueListItem1.DataValue = "Y";
		valueListItem1.DisplayText = "Y.有";
		valueListItem2.DataValue = "N";
		valueListItem2.DisplayText = "N.無";
		this.cboResType.Items.Add(valueListItem1);
		this.cboResType.Items.Add(valueListItem2);
		this.cboResType.Location = new System.Drawing.Point(170, 56);
		this.cboResType.Name = "cboResType";
		this.cboResType.Size = new System.Drawing.Size(107, 24);
		this.cboResType.TabIndex = 2;
		this.cboResType.Text = "N.無";
		this.cboType.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		valueListItem3.DataValue = "16";
		valueListItem3.DisplayText = "16 大類";
		valueListItem4.DataValue = "E";
		valueListItem4.DisplayText = "E  機具";
		valueListItem5.DataValue = "L";
		valueListItem5.DisplayText = "L  人工";
		this.cboType.Items.Add(valueListItem3);
		this.cboType.Items.Add(valueListItem4);
		this.cboType.Items.Add(valueListItem5);
		this.cboType.Location = new System.Drawing.Point(170, 22);
		this.cboType.Name = "cboType";
		this.cboType.Size = new System.Drawing.Size(107, 24);
		this.cboType.TabIndex = 1;
		this.cboType.Text = "16 大類";
		this.cboType.ValueChanged += new System.EventHandler(cboType_ValueChanged);
		this.ultraLabel1.Location = new System.Drawing.Point(19, 90);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(147, 30);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "編碼/章碼：";
		this.ultraLabel2.Location = new System.Drawing.Point(19, 124);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(147, 30);
		this.ultraLabel2.TabIndex = 0;
		this.ultraLabel2.Text = "編碼/章碼名稱：";
		this.ultraLabel2.Click += new System.EventHandler(ultraLabel2_Click);
		this.ultraLabel3.Location = new System.Drawing.Point(19, 56);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(147, 30);
		this.ultraLabel3.TabIndex = 0;
		this.ultraLabel3.Text = "是否有資源碼 M：";
		this.ultraLabel4.Location = new System.Drawing.Point(19, 22);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(147, 30);
		this.ultraLabel4.TabIndex = 0;
		this.ultraLabel4.Text = "編碼類別：";
		this.ultraLabel5.Location = new System.Drawing.Point(19, 158);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(258, 30);
		this.ultraLabel5.TabIndex = 0;
		this.ultraLabel5.Text = "是否將綱要編碼名稱加入編碼結果：";
		this.txtCName.Location = new System.Drawing.Point(170, 124);
		this.txtCName.MaxLength = 40;
		this.txtCName.Name = "txtCName";
		this.txtCName.Size = new System.Drawing.Size(276, 24);
		this.txtCName.TabIndex = 3;
		this.cboIsShow.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		valueListItem6.DataValue = "Y";
		valueListItem6.DisplayText = "Y.是";
		valueListItem7.DataValue = "N";
		valueListItem7.DisplayText = "N.否";
		this.cboIsShow.Items.Add(valueListItem6);
		this.cboIsShow.Items.Add(valueListItem7);
		this.cboIsShow.Location = new System.Drawing.Point(281, 158);
		this.cboIsShow.Name = "cboIsShow";
		this.cboIsShow.Size = new System.Drawing.Size(81, 24);
		this.cboIsShow.TabIndex = 2;
		this.cboIsShow.Text = "Y.是";
		this.ultraLabel6.Location = new System.Drawing.Point(19, 192);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(147, 30);
		this.ultraLabel6.TabIndex = 0;
		this.ultraLabel6.Text = "預先產生空白列數：";
		base.AcceptButton = this.D_Btn_Fnsh;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.CancelButton = this.A1_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(464, 287);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel8);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormAutoNumCreateChapterCode";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "新增/編輯綱要編碼";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormAutoNumCreateChapterCode_KeyDown);
		base.Load += new System.EventHandler(FormAutoNumCreateChapterCode_Load);
		base.Activated += new System.EventHandler(FormAutoNumCreateChapterCode_Activated);
		this.panel8.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1Sizer1).EndInit();
		this.c1Sizer1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.BlankRows).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtItemCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboResType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboIsShow).EndInit();
		base.ResumeLayout(false);
	}

	private void ultraLabel2_Click(object sender, EventArgs e)
	{
	}

	private void D_Btn_Fnsh_Click(object sender, EventArgs e)
	{
		for (int i = 0; i < txtItemCode.Text.Length; i++)
		{
			if (!CommonMethods.EngNumValid(txtItemCode.Text[i]))
			{
				MessageBox.Show(this, "不可輸入非數字或英文字母及的字", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtItemCode.Focus();
				return;
			}
		}
		if (cboType.Value.ToString() == "16")
		{
			if (txtItemCode.Text.Length != 5)
			{
				MessageBox.Show(this, "編碼/章碼必須是 5 位數", "警示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				txtItemCode.Focus();
				return;
			}
		}
		else if (txtItemCode.Text.Length != 6)
		{
			MessageBox.Show(this, "編碼/章碼必須是 6 位數", "警示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			txtItemCode.Focus();
			return;
		}
		if (txtCName.Text.Trim() == "")
		{
			MessageBox.Show(this, "編碼/章碼名稱不可空白", "警示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			txtCName.Focus();
		}
		else
		{
			SaveData();
			(base.Owner as FormAutoNum)._NewCustomCode = txtItemCode.Text.Trim();
			base.DialogResult = DialogResult.OK;
		}
	}

	private void SaveData()
	{
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = "PccAdmin";
		DataTable DT_Tmp1 = DBCLS.GetUserDefine("Select * from AutoNumA Where ItemCode ='" + txtItemCode.Text.Trim() + "' And WinFormFlag='" + F_DEPT_ID + "' ");
		if (DT_Tmp1.Rows.Count > 0)
		{
			return;
		}
		string sSQL = "Insert Into AutoNumA(itemCode,levelNo,cName,IsShow,parent,WinFormFlag,AltUnit)values(?,?,?,?,?,?,?)";
		OleDbCommand odCmd = new OleDbCommand();
		odCmd.CommandText = sSQL;
		odCmd.Parameters.Clear();
		odCmd.Parameters.Add("P1", OleDbType.Char, 20);
		odCmd.Parameters.Add("P2", OleDbType.Char, 1);
		odCmd.Parameters.Add("P3", OleDbType.Char, 40);
		odCmd.Parameters.Add("P4", OleDbType.Char, 1);
		odCmd.Parameters.Add("P5", OleDbType.Char, 10);
		odCmd.Parameters.Add("P6", OleDbType.Char, 10);
		odCmd.Parameters.Add("P7", OleDbType.VarChar, 10);
		odCmd.Parameters["P1"].Value = txtItemCode.Text.Trim();
		odCmd.Parameters["P2"].Value = "2";
		odCmd.Parameters["P3"].Value = txtCName.Text.Trim();
		odCmd.Parameters["P4"].Value = ((cboIsShow.Value.ToString() == "Y") ? "*" : "");
		if (cboType.Value.ToString() == "16")
		{
			odCmd.Parameters["P5"].Value = txtItemCode.Text.Substring(0, 2);
		}
		else if (cboType.Value.ToString() == "E")
		{
			odCmd.Parameters["P5"].Value = "E";
		}
		else if (cboType.Value.ToString() == "L")
		{
			odCmd.Parameters["P5"].Value = "L";
		}
		odCmd.Parameters["P6"].Value = F_DEPT_ID.Trim();
		odCmd.Parameters["P7"].Value = "";
		DBCLS.ExecuteOleDbCommand(odCmd);
		int MaxRows = (int)(BlankRows.Value + 3m);
		string resCode = "";
		if (cboType.Value.ToString().Trim() == "M" && cboResType.Value.ToString() == "Y")
		{
			resCode = "M";
		}
		else if (cboType.Value.ToString().Trim() == "E")
		{
			resCode = "E";
		}
		else if (cboType.Value.ToString().Trim() == "L")
		{
			resCode = "L";
		}
		string sSQL2 = "";
		for (int i = 6; i <= 11; i++)
		{
			if (i < 11)
			{
				for (int j = 4; j <= MaxRows; j++)
				{
					string text = sSQL2;
					sSQL2 = text + "Insert Into AutoNumB(ChapCode,Code,CodeSection,MinRow,MaxRow,SelfRow,Content,resType,IsCustom,Version) values('" + txtItemCode.Text.Trim() + "',' ','" + i.ToString().PadLeft(2, '0') + "',4," + MaxRows + "," + j + ",";
					sSQL2 = ((cboType.SelectedIndex <= 0 || i != 6 || j != 4) ? (sSQL2 + "'',") : (sSQL2 + "'" + txtCName.Text.Trim() + "',"));
					object obj = sSQL2;
					sSQL2 = string.Concat(obj, "'", resCode, "','Y','", F_DEPT_ID, "')", '\r');
				}
			}
			else
			{
				for (int j = 4; j <= MaxRows; j++)
				{
					object obj = sSQL2;
					sSQL2 = string.Concat(obj, "Insert Into AutoNumB(ChapCode,Code,CodeSection,MinRow,MaxRow,SelfRow,Content,resType,IsCustom,Version) values('", txtItemCode.Text.Trim(), "',' ','RM',4,", MaxRows.ToString(), ",", j.ToString(), ",'','", resCode, "','Y','", F_DEPT_ID, "')", '\r');
				}
			}
		}
		DBCLS.ExecuteCommand(sSQL2);
	}

	private void cboType_ValueChanged(object sender, EventArgs e)
	{
		if (cboType.Value.ToString() != "16")
		{
			cboResType.SelectedIndex = 1;
			cboIsShow.Enabled = true;
			cboResType.Enabled = false;
		}
		else
		{
			cboIsShow.Enabled = false;
			cboIsShow.SelectedIndex = 0;
			cboResType.Enabled = true;
		}
	}

	private void FormAutoNumCreateChapterCode_Load(object sender, EventArgs e)
	{
		ArrayList aArr = new ArrayList();
		aArr.Add("PccAdmin");
		aArr.Add("自動編碼--新增自訂的章碼");
		ModifyDB StdCom = new ModifyDB("", aArr);
		DataTable DT_AutoA = StdCom.DBList("sp_PKeys 'AutoNumA'");
		for (int i = 0; i < DT_AutoA.Rows.Count; i++)
		{
			if (DT_AutoA.Rows[i]["COLUMN_NAME"].ToString().ToUpper() == "WINFORMFLAG")
			{
				Is_WinFormFlag = true;
			}
		}
	}

	private void FormAutoNumCreateChapterCode_Activated(object sender, EventArgs e)
	{
		if (FORM_STATUS == FormStatus.Iinitial)
		{
			if (!Is_WinFormFlag)
			{
				D_Btn_Fnsh.Enabled = false;
				MessageBox.Show(this, "你的資料庫，目前的[自動編碼]資料表，不能滿足新增自訂章碼的需求。\n\n請洽客服部!", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			FORM_STATUS = FormStatus.Active;
		}
	}

	private void FormAutoNumCreateChapterCode_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormAutoNumCreateChapterCode");
		}
	}
}
