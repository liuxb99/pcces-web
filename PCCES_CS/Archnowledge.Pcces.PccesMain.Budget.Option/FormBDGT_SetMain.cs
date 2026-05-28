using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTabs;

namespace Archnowledge.Pcces.PccesMain.Budget.Option;

public class FormBDGT_SetMain : Form
{
	private DataTable dtProjectVersions = new DataTable();

	private DataTable dtCNTVersions = new DataTable();

	private string userID;

	private string projectCode;

	private string FormActionName = "bud";

	private IContainer components;

	private Panel panel1;

	private Panel panel9;

	private GroupBox groupBox5;

	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraButton DeleteSnapshot;

	private UltraButton ultraButton2;

	private UltraTabPageControl ultraTabPageControl2;

	private Panel panel4;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel8;

	private C1PictureBox c1PictureBox5;

	private Panel panel6;

	private Panel panel2;

	private GridBudget gridProjectVersions;

	private C1PictureBox c1PictureBox6;

	private UltraTabPageControl ultraTabPageControl1;

	private Panel panel5;

	private GridBudget gridCNTVersions;

	private Panel panel7;

	private UltraButton DeleteSnapshotCNT;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private C1PictureBox c1PictureBox1;

	private C1PictureBox c1PictureBox2;

	private Panel panel3;

	public string _UserID
	{
		get
		{
			return userID;
		}
		set
		{
			userID = value;
		}
	}

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

	public string _ActionName
	{
		get
		{
			return FormActionName;
		}
		set
		{
			FormActionName = value;
			if (value != null && value.ToUpper().Trim() == "BID")
			{
				ultraLabel7.Text = "\u3000儲存標單版本刪除";
				ultraLabel8.Text = "儲存標單設定";
				ultraTabPageControl2.Tab.Text = "儲存標單設定";
			}
		}
	}

	public FormBDGT_SetMain()
	{
		InitializeComponent();
	}

	private void FormBDGT_OptionMain_Load(object sender, EventArgs e)
	{
		LoadData();
		DataBinding();
	}

	private void LoadData()
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("複製所有項目有回復原來的功能--" + projectCode);
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PROJ.ps_projectCode = projectCode;
		PROJ.ps_srckind = FormActionName;
		dtProjectVersions = PROJ.ListItemTmp();
		if (dtProjectVersions.Rows.Count == 0)
		{
			DeleteSnapshot.Enabled = false;
		}
		PROJ.ps_projectCode = projectCode;
		PROJ.ps_srckind = "CNT";
		dtCNTVersions = PROJ.ListItemTmp();
		if (dtProjectVersions.Rows.Count == 0)
		{
			DeleteSnapshotCNT.Enabled = false;
		}
		PROJ = null;
		aArr = null;
	}

	private void DataBinding()
	{
		gridProjectVersions.Rows.Count = dtProjectVersions.Rows.Count + 1;
		if (dtProjectVersions.Rows.Count > 0)
		{
			for (int i = 0; i < dtProjectVersions.Rows.Count; i++)
			{
				gridProjectVersions[i + 1, "Selected"] = false;
				gridProjectVersions[i + 1, "version"] = dtProjectVersions.Rows[i]["version"].ToString().Trim();
				gridProjectVersions[i + 1, "NewDate"] = dtProjectVersions.Rows[i]["NewDate"];
				gridProjectVersions[i + 1, "memo"] = dtProjectVersions.Rows[i]["memo"].ToString().Trim();
			}
		}
		gridCNTVersions.Rows.Count = dtCNTVersions.Rows.Count + 1;
		if (dtCNTVersions.Rows.Count > 0)
		{
			for (int i = 0; i < dtCNTVersions.Rows.Count; i++)
			{
				gridCNTVersions[i + 1, "Selected"] = false;
				gridCNTVersions[i + 1, "version"] = PubTools.Str2Int(dtCNTVersions.Rows[i]["version"].ToString()) - 50000;
				gridCNTVersions[i + 1, "NewDate"] = dtCNTVersions.Rows[i]["NewDate"];
				gridCNTVersions[i + 1, "memo"] = dtCNTVersions.Rows[i]["memo"].ToString().Trim();
			}
		}
	}

	private void DeleteSnapshot_Click(object sender, EventArgs e)
	{
		bool IsSelected = false;
		for (int i = 1; i < gridProjectVersions.Rows.Count; i++)
		{
			if ((bool)gridProjectVersions[i, "Selected"])
			{
				IsSelected = true;
				break;
			}
		}
		if (!IsSelected)
		{
			MessageBox.Show(this, "請先選擇要刪除的舊版預算書！", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else
		{
			if (MessageBox.Show(this, "請確認是否刪除？", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
			{
				return;
			}
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(userID);
			aArr.Add("刪除專案");
			Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
			PROJ.ps_projectCode = projectCode;
			PROJ.ps_srckind = FormActionName;
			string version = string.Empty;
			for (int rowIndex = gridProjectVersions.Rows.Count - 1; rowIndex > 0; rowIndex--)
			{
				if ((bool)gridProjectVersions[rowIndex, "Selected"])
				{
					version = gridProjectVersions[rowIndex, "version"].ToString();
					PROJ.DeleProjTmp(projectCode, version);
					gridProjectVersions.Rows.Remove(rowIndex);
				}
			}
		}
	}

	private void DeleteSnapshotCNT_Click(object sender, EventArgs e)
	{
		bool IsSelected = false;
		for (int i = 1; i < gridCNTVersions.Rows.Count; i++)
		{
			if ((bool)gridCNTVersions[i, "Selected"])
			{
				IsSelected = true;
				break;
			}
		}
		if (!IsSelected)
		{
			MessageBox.Show(this, "請先選擇要刪除的舊版契約書！", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else
		{
			if (MessageBox.Show(this, "請確認是否刪除？", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
			{
				return;
			}
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(userID);
			aArr.Add("刪除專案");
			Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
			PROJ.ps_projectCode = projectCode;
			PROJ.ps_srckind = "CNT";
			string version = string.Empty;
			for (int rowIndex = gridCNTVersions.Rows.Count - 1; rowIndex > 0; rowIndex--)
			{
				if ((bool)gridCNTVersions[rowIndex, "Selected"])
				{
					version = gridCNTVersions[rowIndex, "version"].ToString();
					PROJ.DeleProjTmp(projectCode, version);
					gridCNTVersions.Rows.Remove(rowIndex);
				}
			}
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.Option.FormBDGT_SetMain));
		this.ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel6 = new System.Windows.Forms.Panel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.DeleteSnapshot = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.c1PictureBox5 = new C1.Win.C1Input.C1PictureBox();
		this.c1PictureBox6 = new C1.Win.C1Input.C1PictureBox();
		this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel5 = new System.Windows.Forms.Panel();
		this.panel7 = new System.Windows.Forms.Panel();
		this.DeleteSnapshotCNT = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.c1PictureBox1 = new C1.Win.C1Input.C1PictureBox();
		this.c1PictureBox2 = new C1.Win.C1Input.C1PictureBox();
		this.panel3 = new System.Windows.Forms.Panel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.panel9 = new System.Windows.Forms.Panel();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.gridProjectVersions = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.gridCNTVersions = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.ultraTabPageControl2.SuspendLayout();
		this.panel4.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox6).BeginInit();
		this.ultraTabPageControl1.SuspendLayout();
		this.panel5.SuspendLayout();
		this.panel7.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox2).BeginInit();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		this.panel9.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridProjectVersions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridCNTVersions).BeginInit();
		base.SuspendLayout();
		this.ultraTabPageControl2.Controls.Add(this.panel6);
		this.ultraTabPageControl2.Controls.Add(this.panel4);
		this.ultraTabPageControl2.Location = new System.Drawing.Point(120, 1);
		this.ultraTabPageControl2.Name = "ultraTabPageControl2";
		this.ultraTabPageControl2.Size = new System.Drawing.Size(599, 520);
		this.panel6.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel6.Location = new System.Drawing.Point(0, 0);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(8, 520);
		this.panel6.TabIndex = 36;
		this.panel4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel4.Controls.Add(this.gridProjectVersions);
		this.panel4.Controls.Add(this.panel2);
		this.panel4.Controls.Add(this.ultraLabel7);
		this.panel4.Controls.Add(this.ultraLabel8);
		this.panel4.Controls.Add(this.c1PictureBox5);
		this.panel4.Controls.Add(this.c1PictureBox6);
		this.panel4.Location = new System.Drawing.Point(24, 8);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(560, 456);
		this.panel4.TabIndex = 35;
		this.panel2.Controls.Add(this.DeleteSnapshot);
		this.panel2.Location = new System.Drawing.Point(22, 128);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(506, 328);
		this.panel2.TabIndex = 25;
		this.DeleteSnapshot.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.DeleteSnapshot.Appearance = appearance1;
		this.DeleteSnapshot.BackColor = System.Drawing.SystemColors.Control;
		this.DeleteSnapshot.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.DeleteSnapshot.Font = new System.Drawing.Font("細明體", 11f);
		this.DeleteSnapshot.ImageSize = new System.Drawing.Size(20, 20);
		this.DeleteSnapshot.ImageTransparentColor = System.Drawing.Color.White;
		this.DeleteSnapshot.Location = new System.Drawing.Point(368, 288);
		this.DeleteSnapshot.Name = "DeleteSnapshot";
		this.DeleteSnapshot.ShowFocusRect = false;
		this.DeleteSnapshot.ShowOutline = false;
		this.DeleteSnapshot.Size = new System.Drawing.Size(128, 31);
		this.DeleteSnapshot.SupportThemes = false;
		this.DeleteSnapshot.TabIndex = 8;
		this.DeleteSnapshot.Text = "刪除選取項目";
		this.DeleteSnapshot.Click += new System.EventHandler(DeleteSnapshot_Click);
		this.ultraLabel7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance2.BackColor = System.Drawing.Color.FromArgb(221, 231, 238);
		appearance2.ForeColor = System.Drawing.Color.Navy;
		this.ultraLabel7.Appearance = appearance2;
		this.ultraLabel7.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel7.Font = new System.Drawing.Font("標楷體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel7.Location = new System.Drawing.Point(20, 64);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(512, 23);
		this.ultraLabel7.TabIndex = 24;
		this.ultraLabel7.Text = "\u3000儲存預算書版本刪除";
		appearance3.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance3.FontData.SizeInPoints = 14f;
		appearance3.ForeColor = System.Drawing.Color.Navy;
		appearance3.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance3.ImageBackground");
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel8.Appearance = appearance3;
		this.ultraLabel8.Location = new System.Drawing.Point(85, 0);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(208, 59);
		this.ultraLabel8.TabIndex = 23;
		this.ultraLabel8.Text = "儲存預算書設定";
		this.c1PictureBox5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.c1PictureBox5.Image = (System.Drawing.Image)resources.GetObject("c1PictureBox5.Image");
		this.c1PictureBox5.Location = new System.Drawing.Point(328, 0);
		this.c1PictureBox5.Name = "c1PictureBox5";
		this.c1PictureBox5.Size = new System.Drawing.Size(227, 59);
		this.c1PictureBox5.TabIndex = 2;
		this.c1PictureBox5.TabStop = false;
		this.c1PictureBox6.Dock = System.Windows.Forms.DockStyle.Top;
		this.c1PictureBox6.Image = (System.Drawing.Image)resources.GetObject("c1PictureBox6.Image");
		this.c1PictureBox6.Location = new System.Drawing.Point(0, 0);
		this.c1PictureBox6.Name = "c1PictureBox6";
		this.c1PictureBox6.Size = new System.Drawing.Size(556, 59);
		this.c1PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.c1PictureBox6.TabIndex = 1;
		this.c1PictureBox6.TabStop = false;
		this.ultraTabPageControl1.Controls.Add(this.panel5);
		this.ultraTabPageControl1.Controls.Add(this.panel3);
		this.ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabPageControl1.Name = "ultraTabPageControl1";
		this.ultraTabPageControl1.Size = new System.Drawing.Size(599, 520);
		this.panel5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel5.Controls.Add(this.gridCNTVersions);
		this.panel5.Controls.Add(this.panel7);
		this.panel5.Controls.Add(this.ultraLabel1);
		this.panel5.Controls.Add(this.ultraLabel2);
		this.panel5.Controls.Add(this.c1PictureBox1);
		this.panel5.Controls.Add(this.c1PictureBox2);
		this.panel5.Location = new System.Drawing.Point(24, 8);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(560, 456);
		this.panel5.TabIndex = 38;
		this.panel7.Controls.Add(this.DeleteSnapshotCNT);
		this.panel7.Location = new System.Drawing.Point(22, 128);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(506, 328);
		this.panel7.TabIndex = 25;
		this.DeleteSnapshotCNT.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance4.Image = resources.GetObject("appearance4.Image");
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.DeleteSnapshotCNT.Appearance = appearance4;
		this.DeleteSnapshotCNT.BackColor = System.Drawing.SystemColors.Control;
		this.DeleteSnapshotCNT.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.DeleteSnapshotCNT.Font = new System.Drawing.Font("細明體", 11f);
		this.DeleteSnapshotCNT.ImageSize = new System.Drawing.Size(20, 20);
		this.DeleteSnapshotCNT.ImageTransparentColor = System.Drawing.Color.White;
		this.DeleteSnapshotCNT.Location = new System.Drawing.Point(368, 288);
		this.DeleteSnapshotCNT.Name = "DeleteSnapshotCNT";
		this.DeleteSnapshotCNT.ShowFocusRect = false;
		this.DeleteSnapshotCNT.ShowOutline = false;
		this.DeleteSnapshotCNT.Size = new System.Drawing.Size(128, 31);
		this.DeleteSnapshotCNT.SupportThemes = false;
		this.DeleteSnapshotCNT.TabIndex = 8;
		this.DeleteSnapshotCNT.Text = "刪除選取項目";
		this.DeleteSnapshotCNT.Click += new System.EventHandler(DeleteSnapshotCNT_Click);
		this.ultraLabel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance5.BackColor = System.Drawing.Color.FromArgb(221, 231, 238);
		appearance5.ForeColor = System.Drawing.Color.Navy;
		this.ultraLabel1.Appearance = appearance5;
		this.ultraLabel1.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel1.Font = new System.Drawing.Font("標楷體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(20, 64);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(512, 23);
		this.ultraLabel1.TabIndex = 24;
		this.ultraLabel1.Text = "\u3000儲存契約書版本刪除";
		appearance6.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance6.FontData.SizeInPoints = 14f;
		appearance6.ForeColor = System.Drawing.Color.Navy;
		appearance6.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance6.ImageBackground");
		appearance6.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance6;
		this.ultraLabel2.Location = new System.Drawing.Point(85, 0);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(208, 59);
		this.ultraLabel2.TabIndex = 23;
		this.ultraLabel2.Text = "儲存契約書設定";
		this.c1PictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.c1PictureBox1.Image = (System.Drawing.Image)resources.GetObject("c1PictureBox1.Image");
		this.c1PictureBox1.Location = new System.Drawing.Point(328, 0);
		this.c1PictureBox1.Name = "c1PictureBox1";
		this.c1PictureBox1.Size = new System.Drawing.Size(227, 59);
		this.c1PictureBox1.TabIndex = 2;
		this.c1PictureBox1.TabStop = false;
		this.c1PictureBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.c1PictureBox2.Image = (System.Drawing.Image)resources.GetObject("c1PictureBox2.Image");
		this.c1PictureBox2.Location = new System.Drawing.Point(0, 0);
		this.c1PictureBox2.Name = "c1PictureBox2";
		this.c1PictureBox2.Size = new System.Drawing.Size(556, 59);
		this.c1PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.c1PictureBox2.TabIndex = 1;
		this.c1PictureBox2.TabStop = false;
		this.panel3.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel3.Location = new System.Drawing.Point(0, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(8, 520);
		this.panel3.TabIndex = 37;
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.Tab_Ctrl);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(720, 522);
		this.panel1.TabIndex = 0;
		appearance7.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance7.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		appearance7.ForeColor = System.Drawing.Color.White;
		this.Tab_Ctrl.ActiveTabAppearance = appearance7;
		appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		this.Tab_Ctrl.Appearance = appearance8;
		this.Tab_Ctrl.BackColor = System.Drawing.Color.White;
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.ultraTabPageControl2);
		this.Tab_Ctrl.Controls.Add(this.ultraTabPageControl1);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(720, 522);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Flat;
		this.Tab_Ctrl.TabIndex = 29;
		this.Tab_Ctrl.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.LeftTop;
		ultraTab1.TabPage = this.ultraTabPageControl2;
		ultraTab1.Text = "儲存預算書設定";
		ultraTab2.TabPage = this.ultraTabPageControl1;
		ultraTab2.Text = "儲存契約書設定";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2] { ultraTab1, ultraTab2 });
		this.Tab_Ctrl.TextOrientation = Infragistics.Win.UltraWinTabs.TextOrientation.Horizontal;
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(599, 520);
		this.panel9.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel9.Controls.Add(this.ultraButton2);
		this.panel9.Controls.Add(this.groupBox5);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel9.Location = new System.Drawing.Point(0, 478);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(720, 44);
		this.panel9.TabIndex = 22;
		this.ultraButton2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance9.Image = resources.GetObject("appearance9.Image");
		appearance9.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton2.Appearance = appearance9;
		this.ultraButton2.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton2.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton2.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton2.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton2.Location = new System.Drawing.Point(610, 8);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(88, 31);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 7;
		this.ultraButton2.Text = "關閉";
		this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox5.Location = new System.Drawing.Point(0, 0);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(720, 4);
		this.groupBox5.TabIndex = 3;
		this.groupBox5.TabStop = false;
		this.gridProjectVersions._ExcelFileName = "";
		this.gridProjectVersions._ExcelSheeName = "";
		this.gridProjectVersions._IsOpenExcelAfterExport = false;
		this.gridProjectVersions.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridProjectVersions.ColumnInfo = resources.GetString("gridProjectVersions.ColumnInfo");
		this.gridProjectVersions.ExtendLastCol = true;
		this.gridProjectVersions.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
		this.gridProjectVersions.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridProjectVersions.ForeColor = System.Drawing.Color.Black;
		this.gridProjectVersions.Location = new System.Drawing.Point(26, 128);
		this.gridProjectVersions.Name = "gridProjectVersions";
		this.gridProjectVersions.Rows.Count = 1;
		this.gridProjectVersions.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.gridProjectVersions.ShowCursor = true;
		this.gridProjectVersions.ShowToolTipOnNarrowColumn = true;
		this.gridProjectVersions.Size = new System.Drawing.Size(506, 280);
		this.gridProjectVersions.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridProjectVersions.Styles"));
		this.gridProjectVersions.TabIndex = 5;
		this.gridProjectVersions.Tree.Column = 1;
		this.gridProjectVersions.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridCNTVersions._ExcelFileName = "";
		this.gridCNTVersions._ExcelSheeName = "";
		this.gridCNTVersions._IsOpenExcelAfterExport = false;
		this.gridCNTVersions.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridCNTVersions.ColumnInfo = resources.GetString("gridCNTVersions.ColumnInfo");
		this.gridCNTVersions.ExtendLastCol = true;
		this.gridCNTVersions.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
		this.gridCNTVersions.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridCNTVersions.ForeColor = System.Drawing.Color.Black;
		this.gridCNTVersions.Location = new System.Drawing.Point(26, 128);
		this.gridCNTVersions.Name = "gridCNTVersions";
		this.gridCNTVersions.Rows.Count = 1;
		this.gridCNTVersions.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.gridCNTVersions.ShowCursor = true;
		this.gridCNTVersions.ShowToolTipOnNarrowColumn = true;
		this.gridCNTVersions.Size = new System.Drawing.Size(506, 280);
		this.gridCNTVersions.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridCNTVersions.Styles"));
		this.gridCNTVersions.TabIndex = 5;
		this.gridCNTVersions.Tree.Column = 1;
		this.gridCNTVersions.Tree.LineColor = System.Drawing.Color.Gray;
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(720, 522);
		base.ControlBox = false;
		base.Controls.Add(this.panel9);
		base.Controls.Add(this.panel1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.MinimizeBox = false;
		base.Name = "FormBDGT_SetMain";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "設定";
		base.Load += new System.EventHandler(FormBDGT_OptionMain_Load);
		this.ultraTabPageControl2.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox6).EndInit();
		this.ultraTabPageControl1.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox2).EndInit();
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).EndInit();
		this.Tab_Ctrl.ResumeLayout(false);
		this.panel9.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridProjectVersions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridCNTVersions).EndInit();
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
