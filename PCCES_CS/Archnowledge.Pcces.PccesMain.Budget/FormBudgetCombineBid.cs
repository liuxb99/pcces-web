using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.DomainModule.Bid;
using Archnowledge.Pcces.PccesMain.ArchControls;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinProgressBar;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetCombineBid : Form
{
	private string userID;

	private string projectCode;

	private IContainer components;

	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private Panel panel4;

	private Panel panel7;

	private UltraButton btnRemove;

	private UltraButton btnSelect;

	private UltraButton btnRemoveAll;

	private UltraButton btnSelectAll;

	private Panel panel5;

	private GridBudget gridSourceProject;

	private UltraLabel ultraLabel3;

	private Panel panel10;

	private Panel panel6;

	private GridBudget gridProjectSelected;

	private Panel panel8;

	private UltraButton btnMoveDown;

	private UltraButton btnMoveUp;

	private UltraLabel ultraLabel4;

	private Panel panel9;

	private Panel panel2;

	private UltraButton btn_B_Cancel;

	private Panel panel1;

	private UltraLabel ultraLabel2;

	private Panel panel11;

	private UltraLabel ultraLabel1;

	private Panel panel12;

	private Panel panel13;

	private UltraButton ultraButton8;

	private UltraTabControl tabProjectList;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage2;

	private Panel panel14;

	private UltraButton btnCancel;

	private UltraTabPageControl Tab_B;

	private UltraTabPageControl Tab_C;

	private UltraTabPageControl Tab_D;

	private UltraTabPageControl Tab_A;

	private UltraLabel ultraLabel6;

	private UltraTabPageControl Tab_E;

	private UltraButton btn_B_Next;

	private UltraButton btn_C_Next;

	private UltraButton btn_A_Next;

	private Panel panel15;

	private UltraButton btnOK;

	private UltraButton btn_C_Prev;

	private UltraProgressBar progressBar;

	private UltraLabel ultraLabel18;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel14;

	private UltraLabel ultraLabel13;

	private UltraLabel ultraLabel12;

	private UltraLabel ultraLabel8;

	private UltraLabel ultraLabel9;

	private UltraLabel ultraLabel10;

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

	public FormBudgetCombineBid()
	{
		InitializeComponent();
	}

	private void FormBudgetCombine_Load(object sender, EventArgs e)
	{
		BidProject bidProject = new BidProject();
		DataSet dsProjectList = bidProject.GetProjectList();
		BindToGridProjectList(dsProjectList);
	}

	private void BindToGridProjectList(DataSet dsProjectList)
	{
		DataRowCollection drProjectList = dsProjectList.Tables[0].Rows;
		gridSourceProject.Rows.Count = drProjectList.Count;
		int index = 1;
		foreach (DataRow row in drProjectList)
		{
			if (!(projectCode.Trim() == row["projectCode"].ToString().Trim()))
			{
				gridSourceProject[index, "ProjectCode"] = row["projectCode"].ToString();
				gridSourceProject[index, "ProjectNameC"] = row["ProjectNameC"].ToString();
				index++;
			}
		}
		gridSourceProject.AutoSizeCols();
	}

	private void btnSelectAll_Click(object sender, EventArgs e)
	{
		MoveAllRows(gridSourceProject, gridProjectSelected);
	}

	private void btnRemoveAll_Click(object sender, EventArgs e)
	{
		MoveAllRows(gridProjectSelected, gridSourceProject);
	}

	private void MoveAllRows(GridBudget source, GridBudget target)
	{
		target.Redraw = false;
		foreach (Row row in (IEnumerable)source.Rows)
		{
			if (row.Index != 0)
			{
				Row targetRow = target.Rows.Add();
				targetRow["ProjectCode"] = row["ProjectCode"];
				targetRow["ProjectNameC"] = row["ProjectNameC"];
			}
		}
		target.Redraw = true;
		source.Rows.Count = 1;
		source.AutoSizeCols();
		target.AutoSizeCols();
	}

	private void gridSourceProject_DoubleClick(object sender, EventArgs e)
	{
		MoveRow(gridSourceProject, gridProjectSelected);
	}

	private void btnSelect_Click(object sender, EventArgs e)
	{
		MoveRow(gridSourceProject, gridProjectSelected);
	}

	private void gridProjectSelected_DoubleClick(object sender, EventArgs e)
	{
		MoveRow(gridProjectSelected, gridSourceProject);
	}

	private void btnRemove_Click(object sender, EventArgs e)
	{
		if (gridProjectSelected.SelectedRowCount == 0)
		{
			MessageBox.Show(this, "請先選取要移除的專案！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			MoveRow(gridProjectSelected, gridSourceProject);
		}
	}

	private void MoveRow(GridBudget source, GridBudget target)
	{
		for (int i = source.Rows.Count - 1; i > 0; i--)
		{
			Row sourceRow = source.Rows[i];
			if (sourceRow.Selected)
			{
				Row targetRow = target.Rows.Add();
				targetRow["ProjectCode"] = sourceRow["ProjectCode"];
				targetRow["ProjectNameC"] = sourceRow["ProjectNameC"];
				source.RemoveItem(i);
			}
		}
		source.AutoSizeCols();
		target.AutoSizeCols();
	}

	private void btnMoveUp_Click(object sender, EventArgs e)
	{
		RowCollection selectedRows = gridProjectSelected.Rows.Selected;
		if (selectedRows.Count == 0 || selectedRows[0].Index == 1)
		{
			return;
		}
		foreach (Row row in (IEnumerable)selectedRows)
		{
			row.Move(row.Index - 1);
		}
	}

	private void btnMoveDown_Click(object sender, EventArgs e)
	{
		RowCollection selectedRows = gridProjectSelected.Rows.Selected;
		if (selectedRows.Count != 0 && selectedRows[selectedRows.Count - 1].Index != gridProjectSelected.Rows.Count - 1)
		{
			for (int i = selectedRows.Count - 1; i >= 0; i--)
			{
				selectedRows[i].Move(selectedRows[i].Index + 1);
			}
		}
	}

	private void btn_A_Next_Click(object sender, EventArgs e)
	{
		Tab_B.Tab.Selected = true;
	}

	private void btn_B_Next_Click(object sender, EventArgs e)
	{
		if (gridProjectSelected.Rows.Count <= 1)
		{
			MessageBox.Show(this, "請至少選擇一個專案！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		Cursor = Cursors.WaitCursor;
		tabProjectList.Tabs.Clear();
		for (int i = 1; i < gridProjectSelected.Rows.Count; i++)
		{
			string key = gridProjectSelected[i, "ProjectCode"].ToString().Trim();
			string title = gridProjectSelected[i, "ProjectNameC"].ToString().Trim();
			tabProjectList.Tabs.Add(key, "(" + key + ")" + title);
			ucBudgetCombineBid bidItemSelector = new ucBudgetCombineBid();
			bidItemSelector.Dock = DockStyle.Fill;
			bidItemSelector._ProjectCode = key;
			bidItemSelector._MainProjectCode = projectCode;
			bidItemSelector._UserID = userID;
			tabProjectList.Tabs[key].TabPage.Controls.Add(bidItemSelector);
		}
		Tab_C.Tab.Selected = true;
		Cursor = Cursors.Default;
	}

	private void btn_C_Next_Click(object sender, EventArgs e)
	{
		DialogResult dialogResult = MessageBox.Show(this, "是否覆蓋已填寫單價之工項？", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		bool isOverriden = dialogResult == DialogResult.Yes;
		Cursor = Cursors.WaitCursor;
		Application.DoEvents();
		Tab_D.Tab.Selected = true;
		progressBar.Minimum = 0;
		progressBar.Maximum = tabProjectList.Tabs.Count;
		for (int i = tabProjectList.Tabs.Count - 1; i >= 0; i--)
		{
			ExecResult ER = ((ucBudgetCombineBid)tabProjectList.Tabs[i].TabPage.Controls[0]).ImportCost(isOverriden);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show("引用失敗：" + ER.Message);
				Close();
				break;
			}
			progressBar.Value++;
		}
		Tab_E.Tab.Selected = true;
		Cursor = Cursors.Default;
	}

	private void btn_C_Prev_Click(object sender, EventArgs e)
	{
		Tab_B.Tab.Selected = true;
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetCombineBid));
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel18 = new Infragistics.Win.Misc.UltraLabel();
		this.panel14 = new System.Windows.Forms.Panel();
		this.btn_A_Next = new Infragistics.Win.Misc.UltraButton();
		this.btnCancel = new Infragistics.Win.Misc.UltraButton();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel4 = new System.Windows.Forms.Panel();
		this.panel7 = new System.Windows.Forms.Panel();
		this.btnRemove = new Infragistics.Win.Misc.UltraButton();
		this.btnSelect = new Infragistics.Win.Misc.UltraButton();
		this.btnRemoveAll = new Infragistics.Win.Misc.UltraButton();
		this.btnSelectAll = new Infragistics.Win.Misc.UltraButton();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.panel10 = new System.Windows.Forms.Panel();
		this.panel6 = new System.Windows.Forms.Panel();
		this.panel8 = new System.Windows.Forms.Panel();
		this.btnMoveDown = new Infragistics.Win.Misc.UltraButton();
		this.btnMoveUp = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.panel9 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.btn_B_Next = new Infragistics.Win.Misc.UltraButton();
		this.btn_B_Cancel = new Infragistics.Win.Misc.UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_C = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel12 = new System.Windows.Forms.Panel();
		this.tabProjectList = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage2 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.panel13 = new System.Windows.Forms.Panel();
		this.btn_C_Prev = new Infragistics.Win.Misc.UltraButton();
		this.btn_C_Next = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton8 = new Infragistics.Win.Misc.UltraButton();
		this.panel11 = new System.Windows.Forms.Panel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_D = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.progressBar = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_E = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.panel15 = new System.Windows.Forms.Panel();
		this.btnOK = new Infragistics.Win.Misc.UltraButton();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.gridSourceProject = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.gridProjectSelected = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.Tab_A.SuspendLayout();
		this.panel14.SuspendLayout();
		this.Tab_B.SuspendLayout();
		this.panel4.SuspendLayout();
		this.panel7.SuspendLayout();
		this.panel5.SuspendLayout();
		this.panel6.SuspendLayout();
		this.panel8.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel1.SuspendLayout();
		this.Tab_C.SuspendLayout();
		this.panel12.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.tabProjectList).BeginInit();
		this.tabProjectList.SuspendLayout();
		this.panel13.SuspendLayout();
		this.panel11.SuspendLayout();
		this.Tab_D.SuspendLayout();
		this.Tab_E.SuspendLayout();
		this.panel15.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridSourceProject).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridProjectSelected).BeginInit();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.ultraLabel7);
		this.Tab_A.Controls.Add(this.ultraLabel5);
		this.Tab_A.Controls.Add(this.ultraLabel18);
		this.Tab_A.Controls.Add(this.panel14);
		this.Tab_A.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(782, 556);
		appearance1.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance1;
		this.ultraLabel7.Location = new System.Drawing.Point(47, 124);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(701, 20);
		this.ultraLabel7.TabIndex = 10;
		this.ultraLabel7.Text = "這裡的標單併標，不會進行工項合併，而是以開啟中的專案為主，將其他挑選的專案的單價引用進來。";
		appearance2.BackColor = System.Drawing.Color.White;
		this.ultraLabel5.Appearance = appearance2;
		this.ultraLabel5.Location = new System.Drawing.Point(48, 96);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(128, 20);
		this.ultraLabel5.TabIndex = 9;
		this.ultraLabel5.Text = "說明:";
		appearance3.BackColor = System.Drawing.Color.White;
		this.ultraLabel18.Appearance = appearance3;
		this.ultraLabel18.Location = new System.Drawing.Point(26, 32);
		this.ultraLabel18.Name = "ultraLabel18";
		this.ultraLabel18.Size = new System.Drawing.Size(588, 20);
		this.ultraLabel18.TabIndex = 8;
		this.ultraLabel18.Text = "歡迎使用標單併標精靈，接下來我們將引導您一步一步來執行";
		this.panel14.Controls.Add(this.btn_A_Next);
		this.panel14.Controls.Add(this.btnCancel);
		this.panel14.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel14.Location = new System.Drawing.Point(0, 510);
		this.panel14.Name = "panel14";
		this.panel14.Size = new System.Drawing.Size(782, 46);
		this.panel14.TabIndex = 6;
		this.btn_A_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance4.Image = resources.GetObject("appearance4.Image");
		appearance4.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btn_A_Next.Appearance = appearance4;
		this.btn_A_Next.BackColor = System.Drawing.SystemColors.Control;
		this.btn_A_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btn_A_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.btn_A_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.btn_A_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.btn_A_Next.Location = new System.Drawing.Point(588, 8);
		this.btn_A_Next.Name = "btn_A_Next";
		this.btn_A_Next.ShowFocusRect = false;
		this.btn_A_Next.ShowOutline = false;
		this.btn_A_Next.Size = new System.Drawing.Size(88, 31);
		this.btn_A_Next.SupportThemes = false;
		this.btn_A_Next.TabIndex = 4;
		this.btn_A_Next.Text = "下一步";
		this.btn_A_Next.Click += new System.EventHandler(btn_A_Next_Click);
		this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance5.Image = resources.GetObject("appearance5.Image");
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnCancel.Appearance = appearance5;
		this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
		this.btnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Font = new System.Drawing.Font("細明體", 11f);
		this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btnCancel.Location = new System.Drawing.Point(682, 8);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.ShowFocusRect = false;
		this.btnCancel.ShowOutline = false;
		this.btnCancel.Size = new System.Drawing.Size(88, 31);
		this.btnCancel.SupportThemes = false;
		this.btnCancel.TabIndex = 3;
		this.btnCancel.Text = "取消";
		this.Tab_B.Controls.Add(this.panel4);
		this.Tab_B.Controls.Add(this.panel2);
		this.Tab_B.Controls.Add(this.panel1);
		this.Tab_B.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(782, 556);
		this.panel4.Controls.Add(this.panel7);
		this.panel4.Controls.Add(this.panel5);
		this.panel4.Controls.Add(this.panel10);
		this.panel4.Controls.Add(this.panel6);
		this.panel4.Controls.Add(this.panel9);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel4.Location = new System.Drawing.Point(0, 60);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(782, 450);
		this.panel4.TabIndex = 7;
		this.panel7.BackColor = System.Drawing.Color.White;
		this.panel7.Controls.Add(this.btnRemove);
		this.panel7.Controls.Add(this.btnSelect);
		this.panel7.Controls.Add(this.btnRemoveAll);
		this.panel7.Controls.Add(this.btnSelectAll);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel7.Location = new System.Drawing.Point(339, 0);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(104, 450);
		this.panel7.TabIndex = 2;
		appearance6.FontData.SizeInPoints = 9f;
		this.btnRemove.Appearance = appearance6;
		this.btnRemove.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnRemove.Location = new System.Drawing.Point(10, 227);
		this.btnRemove.Name = "btnRemove";
		this.btnRemove.ShowFocusRect = false;
		this.btnRemove.ShowOutline = false;
		this.btnRemove.Size = new System.Drawing.Size(85, 30);
		this.btnRemove.SupportThemes = false;
		this.btnRemove.TabIndex = 4;
		this.btnRemove.Text = "< 移除";
		this.btnRemove.Click += new System.EventHandler(btnRemove_Click);
		appearance7.FontData.SizeInPoints = 9f;
		this.btnSelect.Appearance = appearance7;
		this.btnSelect.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnSelect.Location = new System.Drawing.Point(10, 194);
		this.btnSelect.Name = "btnSelect";
		this.btnSelect.ShowFocusRect = false;
		this.btnSelect.ShowOutline = false;
		this.btnSelect.Size = new System.Drawing.Size(85, 30);
		this.btnSelect.SupportThemes = false;
		this.btnSelect.TabIndex = 3;
		this.btnSelect.Text = "選取 >";
		this.btnSelect.Click += new System.EventHandler(btnSelect_Click);
		appearance8.FontData.SizeInPoints = 9f;
		this.btnRemoveAll.Appearance = appearance8;
		this.btnRemoveAll.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnRemoveAll.Location = new System.Drawing.Point(10, 260);
		this.btnRemoveAll.Name = "btnRemoveAll";
		this.btnRemoveAll.ShowFocusRect = false;
		this.btnRemoveAll.ShowOutline = false;
		this.btnRemoveAll.Size = new System.Drawing.Size(85, 30);
		this.btnRemoveAll.SupportThemes = false;
		this.btnRemoveAll.TabIndex = 1;
		this.btnRemoveAll.Text = "<< 全部移除";
		this.btnRemoveAll.Click += new System.EventHandler(btnRemoveAll_Click);
		appearance9.FontData.SizeInPoints = 9f;
		this.btnSelectAll.Appearance = appearance9;
		this.btnSelectAll.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnSelectAll.Location = new System.Drawing.Point(10, 161);
		this.btnSelectAll.Name = "btnSelectAll";
		this.btnSelectAll.ShowFocusRect = false;
		this.btnSelectAll.ShowOutline = false;
		this.btnSelectAll.Size = new System.Drawing.Size(85, 30);
		this.btnSelectAll.SupportThemes = false;
		this.btnSelectAll.TabIndex = 0;
		this.btnSelectAll.Text = "全選 >>";
		this.btnSelectAll.Click += new System.EventHandler(btnSelectAll_Click);
		this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel5.Controls.Add(this.gridSourceProject);
		this.panel5.Controls.Add(this.ultraLabel3);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel5.Location = new System.Drawing.Point(15, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(324, 450);
		this.panel5.TabIndex = 9;
		appearance10.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance10.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance10.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel3.Appearance = appearance10;
		this.ultraLabel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel3.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(322, 28);
		this.ultraLabel3.TabIndex = 2;
		this.ultraLabel3.Text = "專案列表";
		this.panel10.BackColor = System.Drawing.Color.White;
		this.panel10.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel10.Location = new System.Drawing.Point(0, 0);
		this.panel10.Name = "panel10";
		this.panel10.Size = new System.Drawing.Size(15, 450);
		this.panel10.TabIndex = 8;
		this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel6.Controls.Add(this.gridProjectSelected);
		this.panel6.Controls.Add(this.panel8);
		this.panel6.Controls.Add(this.ultraLabel4);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel6.Location = new System.Drawing.Point(443, 0);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(324, 450);
		this.panel6.TabIndex = 1;
		this.panel8.Controls.Add(this.btnMoveDown);
		this.panel8.Controls.Add(this.btnMoveUp);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel8.Location = new System.Drawing.Point(0, 416);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(322, 32);
		this.panel8.TabIndex = 4;
		appearance11.FontData.SizeInPoints = 9f;
		appearance11.Image = resources.GetObject("appearance11.Image");
		appearance11.ImageVAlign = Infragistics.Win.VAlign.Middle;
		appearance11.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.btnMoveDown.Appearance = appearance11;
		this.btnMoveDown.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnMoveDown.Location = new System.Drawing.Point(163, 2);
		this.btnMoveDown.Name = "btnMoveDown";
		this.btnMoveDown.ShowFocusRect = false;
		this.btnMoveDown.ShowOutline = false;
		this.btnMoveDown.Size = new System.Drawing.Size(60, 28);
		this.btnMoveDown.SupportThemes = false;
		this.btnMoveDown.TabIndex = 7;
		this.btnMoveDown.Text = "下移";
		this.btnMoveDown.Click += new System.EventHandler(btnMoveDown_Click);
		appearance12.FontData.SizeInPoints = 9f;
		appearance12.Image = resources.GetObject("appearance12.Image");
		appearance12.ImageVAlign = Infragistics.Win.VAlign.Middle;
		appearance12.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.btnMoveUp.Appearance = appearance12;
		this.btnMoveUp.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnMoveUp.Location = new System.Drawing.Point(100, 2);
		this.btnMoveUp.Name = "btnMoveUp";
		this.btnMoveUp.ShowFocusRect = false;
		this.btnMoveUp.ShowOutline = false;
		this.btnMoveUp.Size = new System.Drawing.Size(60, 28);
		this.btnMoveUp.SupportThemes = false;
		this.btnMoveUp.TabIndex = 6;
		this.btnMoveUp.Text = "上移";
		this.btnMoveUp.Click += new System.EventHandler(btnMoveUp_Click);
		appearance13.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance13.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance13.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel4.Appearance = appearance13;
		this.ultraLabel4.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.ultraLabel4.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel4.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(322, 28);
		this.ultraLabel4.TabIndex = 3;
		this.ultraLabel4.Text = "已選取的專案";
		this.panel9.BackColor = System.Drawing.Color.White;
		this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel9.Location = new System.Drawing.Point(767, 0);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(15, 450);
		this.panel9.TabIndex = 7;
		this.panel2.Controls.Add(this.ultraLabel10);
		this.panel2.Controls.Add(this.btn_B_Next);
		this.panel2.Controls.Add(this.btn_B_Cancel);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 510);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(782, 46);
		this.panel2.TabIndex = 5;
		appearance14.ForeColor = System.Drawing.Color.FromArgb(0, 51, 153);
		this.ultraLabel10.Appearance = appearance14;
		this.ultraLabel10.Location = new System.Drawing.Point(12, 8);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(553, 35);
		this.ultraLabel10.TabIndex = 1;
		this.ultraLabel10.Text = "合併專案時，當工項代碼有重覆時會依挑選專案的順序，以先挑選的專案取代後挑的專案中的單價！";
		this.btn_B_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance15.Image = resources.GetObject("appearance15.Image");
		appearance15.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance15.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btn_B_Next.Appearance = appearance15;
		this.btn_B_Next.BackColor = System.Drawing.SystemColors.Control;
		this.btn_B_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btn_B_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.btn_B_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.btn_B_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.btn_B_Next.Location = new System.Drawing.Point(588, 8);
		this.btn_B_Next.Name = "btn_B_Next";
		this.btn_B_Next.ShowFocusRect = false;
		this.btn_B_Next.ShowOutline = false;
		this.btn_B_Next.Size = new System.Drawing.Size(88, 31);
		this.btn_B_Next.SupportThemes = false;
		this.btn_B_Next.TabIndex = 4;
		this.btn_B_Next.Text = "下一步";
		this.btn_B_Next.Click += new System.EventHandler(btn_B_Next_Click);
		this.btn_B_Cancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance16.Image = resources.GetObject("appearance16.Image");
		appearance16.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btn_B_Cancel.Appearance = appearance16;
		this.btn_B_Cancel.BackColor = System.Drawing.SystemColors.Control;
		this.btn_B_Cancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btn_B_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btn_B_Cancel.Font = new System.Drawing.Font("細明體", 11f);
		this.btn_B_Cancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btn_B_Cancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btn_B_Cancel.Location = new System.Drawing.Point(682, 8);
		this.btn_B_Cancel.Name = "btn_B_Cancel";
		this.btn_B_Cancel.ShowFocusRect = false;
		this.btn_B_Cancel.ShowOutline = false;
		this.btn_B_Cancel.Size = new System.Drawing.Size(88, 31);
		this.btn_B_Cancel.SupportThemes = false;
		this.btn_B_Cancel.TabIndex = 3;
		this.btn_B_Cancel.Text = "取消";
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.ultraLabel9);
		this.panel1.Controls.Add(this.ultraLabel2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(782, 60);
		this.panel1.TabIndex = 4;
		this.ultraLabel9.Location = new System.Drawing.Point(31, 35);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(688, 20);
		this.ultraLabel9.TabIndex = 2;
		this.ultraLabel9.Text = "如果遇到相同工項代碼時，單價引用會以較先選定的專案為主";
		this.ultraLabel2.Location = new System.Drawing.Point(14, 12);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(688, 20);
		this.ultraLabel2.TabIndex = 1;
		this.ultraLabel2.Text = "挑選要引用單價的專案";
		this.Tab_C.Controls.Add(this.panel12);
		this.Tab_C.Controls.Add(this.panel13);
		this.Tab_C.Controls.Add(this.panel11);
		this.Tab_C.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_C.Name = "Tab_C";
		this.Tab_C.Size = new System.Drawing.Size(782, 556);
		this.panel12.Controls.Add(this.tabProjectList);
		this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel12.Location = new System.Drawing.Point(0, 56);
		this.panel12.Name = "panel12";
		this.panel12.Size = new System.Drawing.Size(782, 454);
		this.panel12.TabIndex = 6;
		appearance17.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance17.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance17.ForeColor = System.Drawing.Color.Black;
		this.tabProjectList.ActiveTabAppearance = appearance17;
		appearance18.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance18.BackColor2 = System.Drawing.Color.FromArgb(102, 153, 255);
		appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		this.tabProjectList.Appearance = appearance18;
		this.tabProjectList.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance19.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance19.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.tabProjectList.ClientAreaAppearance = appearance19;
		this.tabProjectList.Controls.Add(this.ultraTabSharedControlsPage2);
		this.tabProjectList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabProjectList.Location = new System.Drawing.Point(0, 0);
		this.tabProjectList.Name = "tabProjectList";
		appearance20.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance20.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance20.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		appearance20.BorderColor3DBase = System.Drawing.Color.FromArgb(96, 145, 234);
		this.tabProjectList.SelectedTabAppearance = appearance20;
		this.tabProjectList.SharedControlsPage = this.ultraTabSharedControlsPage2;
		this.tabProjectList.Size = new System.Drawing.Size(782, 454);
		this.tabProjectList.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
		appearance21.BackColor = System.Drawing.Color.White;
		this.tabProjectList.TabHeaderAreaAppearance = appearance21;
		this.tabProjectList.TabIndex = 0;
		this.ultraTabSharedControlsPage2.Location = new System.Drawing.Point(2, 21);
		this.ultraTabSharedControlsPage2.Name = "ultraTabSharedControlsPage2";
		this.ultraTabSharedControlsPage2.Size = new System.Drawing.Size(778, 431);
		this.panel13.Controls.Add(this.btn_C_Prev);
		this.panel13.Controls.Add(this.btn_C_Next);
		this.panel13.Controls.Add(this.ultraButton8);
		this.panel13.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel13.Location = new System.Drawing.Point(0, 510);
		this.panel13.Name = "panel13";
		this.panel13.Size = new System.Drawing.Size(782, 46);
		this.panel13.TabIndex = 7;
		this.btn_C_Prev.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance22.Image = resources.GetObject("appearance22.Image");
		appearance22.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btn_C_Prev.Appearance = appearance22;
		this.btn_C_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.btn_C_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btn_C_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.btn_C_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.btn_C_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.btn_C_Prev.Location = new System.Drawing.Point(494, 8);
		this.btn_C_Prev.Name = "btn_C_Prev";
		this.btn_C_Prev.ShowFocusRect = false;
		this.btn_C_Prev.ShowOutline = false;
		this.btn_C_Prev.Size = new System.Drawing.Size(88, 31);
		this.btn_C_Prev.SupportThemes = false;
		this.btn_C_Prev.TabIndex = 5;
		this.btn_C_Prev.Text = "上一步";
		this.btn_C_Prev.Click += new System.EventHandler(btn_C_Prev_Click);
		this.btn_C_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance23.Image = resources.GetObject("appearance23.Image");
		appearance23.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance23.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btn_C_Next.Appearance = appearance23;
		this.btn_C_Next.BackColor = System.Drawing.SystemColors.Control;
		this.btn_C_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btn_C_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.btn_C_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.btn_C_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.btn_C_Next.Location = new System.Drawing.Point(588, 8);
		this.btn_C_Next.Name = "btn_C_Next";
		this.btn_C_Next.ShowFocusRect = false;
		this.btn_C_Next.ShowOutline = false;
		this.btn_C_Next.Size = new System.Drawing.Size(88, 31);
		this.btn_C_Next.SupportThemes = false;
		this.btn_C_Next.TabIndex = 4;
		this.btn_C_Next.Text = "下一步";
		this.btn_C_Next.Click += new System.EventHandler(btn_C_Next_Click);
		this.ultraButton8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance24.Image = resources.GetObject("appearance24.Image");
		appearance24.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton8.Appearance = appearance24;
		this.ultraButton8.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton8.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton8.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton8.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton8.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton8.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton8.Location = new System.Drawing.Point(682, 8);
		this.ultraButton8.Name = "ultraButton8";
		this.ultraButton8.ShowFocusRect = false;
		this.ultraButton8.ShowOutline = false;
		this.ultraButton8.Size = new System.Drawing.Size(88, 31);
		this.ultraButton8.SupportThemes = false;
		this.ultraButton8.TabIndex = 3;
		this.ultraButton8.Text = "取消";
		this.panel11.BackColor = System.Drawing.Color.White;
		this.panel11.Controls.Add(this.ultraLabel1);
		this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel11.Location = new System.Drawing.Point(0, 0);
		this.panel11.Name = "panel11";
		this.panel11.Size = new System.Drawing.Size(782, 56);
		this.panel11.TabIndex = 5;
		this.ultraLabel1.Location = new System.Drawing.Point(14, 20);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(688, 20);
		this.ultraLabel1.TabIndex = 1;
		this.ultraLabel1.Text = "請分別勾選各專案要引用的工項";
		this.Tab_D.Controls.Add(this.progressBar);
		this.Tab_D.Controls.Add(this.ultraLabel6);
		this.Tab_D.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_D.Name = "Tab_D";
		this.Tab_D.Size = new System.Drawing.Size(782, 556);
		this.progressBar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.progressBar.Location = new System.Drawing.Point(67, 267);
		this.progressBar.Name = "progressBar";
		this.progressBar.Size = new System.Drawing.Size(648, 23);
		this.progressBar.TabIndex = 1;
		this.progressBar.Text = "[Formatted]";
		this.ultraLabel6.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Location = new System.Drawing.Point(28, 36);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(176, 23);
		this.ultraLabel6.TabIndex = 0;
		this.ultraLabel6.Text = "轉入中，請稍候";
		this.Tab_E.Controls.Add(this.ultraLabel8);
		this.Tab_E.Controls.Add(this.ultraLabel14);
		this.Tab_E.Controls.Add(this.ultraLabel13);
		this.Tab_E.Controls.Add(this.ultraLabel12);
		this.Tab_E.Controls.Add(this.panel15);
		this.Tab_E.Location = new System.Drawing.Point(0, 0);
		this.Tab_E.Name = "Tab_E";
		this.Tab_E.Size = new System.Drawing.Size(782, 556);
		appearance25.BackColor = System.Drawing.Color.White;
		this.ultraLabel8.Appearance = appearance25;
		this.ultraLabel8.Location = new System.Drawing.Point(44, 200);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel8.TabIndex = 20;
		this.ultraLabel8.Text = "回到標單填寫畫面時，請執行一次重新總計。";
		appearance26.BackColor = System.Drawing.Color.White;
		this.ultraLabel14.Appearance = appearance26;
		this.ultraLabel14.Location = new System.Drawing.Point(44, 124);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel14.TabIndex = 19;
		this.ultraLabel14.Text = "若要結束精靈，請按一下[完成]。";
		appearance27.BackColor = System.Drawing.Color.White;
		this.ultraLabel13.Appearance = appearance27;
		this.ultraLabel13.Location = new System.Drawing.Point(44, 80);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel13.TabIndex = 18;
		this.ultraLabel13.Text = "你已經成功完成標單併標。";
		appearance28.BackColor = System.Drawing.Color.White;
		this.ultraLabel12.Appearance = appearance28;
		this.ultraLabel12.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel12.Location = new System.Drawing.Point(28, 36);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel12.TabIndex = 17;
		this.ultraLabel12.Text = "恭禧您！";
		this.panel15.Controls.Add(this.btnOK);
		this.panel15.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel15.Location = new System.Drawing.Point(0, 510);
		this.panel15.Name = "panel15";
		this.panel15.Size = new System.Drawing.Size(782, 46);
		this.panel15.TabIndex = 8;
		this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance29.Image = resources.GetObject("appearance29.Image");
		appearance29.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnOK.Appearance = appearance29;
		this.btnOK.BackColor = System.Drawing.SystemColors.Control;
		this.btnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnOK.Font = new System.Drawing.Font("細明體", 11f);
		this.btnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.btnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.btnOK.Location = new System.Drawing.Point(682, 8);
		this.btnOK.Name = "btnOK";
		this.btnOK.ShowFocusRect = false;
		this.btnOK.ShowOutline = false;
		this.btnOK.Size = new System.Drawing.Size(88, 31);
		this.btnOK.SupportThemes = false;
		this.btnOK.TabIndex = 4;
		this.btnOK.Text = "完成";
		appearance30.BackColor = System.Drawing.Color.White;
		this.Tab_Ctrl.Appearance = appearance30;
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_B);
		this.Tab_Ctrl.Controls.Add(this.Tab_C);
		this.Tab_Ctrl.Controls.Add(this.Tab_D);
		this.Tab_Ctrl.Controls.Add(this.Tab_A);
		this.Tab_Ctrl.Controls.Add(this.Tab_E);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(782, 556);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabIndex = 0;
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "Page1";
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "tab1";
		ultraTab3.TabPage = this.Tab_C;
		ultraTab3.Text = "tab2";
		ultraTab4.TabPage = this.Tab_D;
		ultraTab4.Text = "tab3";
		ultraTab5.TabPage = this.Tab_E;
		ultraTab5.Text = "tab4";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[5] { ultraTab1, ultraTab2, ultraTab3, ultraTab4, ultraTab5 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(782, 556);
		this.gridSourceProject._ExcelFileName = "";
		this.gridSourceProject._ExcelSheeName = "";
		this.gridSourceProject._IsOpenExcelAfterExport = false;
		this.gridSourceProject.AllowEditing = false;
		this.gridSourceProject.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridSourceProject.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridSourceProject.ColumnInfo = resources.GetString("gridSourceProject.ColumnInfo");
		this.gridSourceProject.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridSourceProject.ExtendLastCol = true;
		this.gridSourceProject.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
		this.gridSourceProject.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridSourceProject.ForeColor = System.Drawing.Color.Black;
		this.gridSourceProject.Location = new System.Drawing.Point(0, 28);
		this.gridSourceProject.Name = "gridSourceProject";
		this.gridSourceProject.Rows.Count = 1;
		this.gridSourceProject.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridSourceProject.ShowCursor = true;
		this.gridSourceProject.ShowToolTipOnNarrowColumn = true;
		this.gridSourceProject.Size = new System.Drawing.Size(322, 420);
		this.gridSourceProject.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridSourceProject.Styles"));
		this.gridSourceProject.TabIndex = 1;
		this.gridSourceProject.Tree.Column = 1;
		this.gridSourceProject.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridSourceProject.DoubleClick += new System.EventHandler(gridSourceProject_DoubleClick);
		this.gridProjectSelected._ExcelFileName = "";
		this.gridProjectSelected._ExcelSheeName = "";
		this.gridProjectSelected._IsOpenExcelAfterExport = false;
		this.gridProjectSelected.AllowEditing = false;
		this.gridProjectSelected.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridProjectSelected.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridProjectSelected.ColumnInfo = resources.GetString("gridProjectSelected.ColumnInfo");
		this.gridProjectSelected.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridProjectSelected.ExtendLastCol = true;
		this.gridProjectSelected.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
		this.gridProjectSelected.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridProjectSelected.ForeColor = System.Drawing.Color.Black;
		this.gridProjectSelected.Location = new System.Drawing.Point(0, 28);
		this.gridProjectSelected.Name = "gridProjectSelected";
		this.gridProjectSelected.Rows.Count = 1;
		this.gridProjectSelected.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridProjectSelected.ShowCursor = true;
		this.gridProjectSelected.ShowToolTipOnNarrowColumn = true;
		this.gridProjectSelected.Size = new System.Drawing.Size(322, 388);
		this.gridProjectSelected.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridProjectSelected.Styles"));
		this.gridProjectSelected.TabIndex = 5;
		this.gridProjectSelected.Tree.Column = 1;
		this.gridProjectSelected.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridProjectSelected.DoubleClick += new System.EventHandler(gridProjectSelected_DoubleClick);
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(782, 556);
		base.Controls.Add(this.Tab_Ctrl);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormBudgetCombineBid";
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "標單併標";
		base.Load += new System.EventHandler(FormBudgetCombine_Load);
		this.Tab_A.ResumeLayout(false);
		this.panel14.ResumeLayout(false);
		this.Tab_B.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		this.panel6.ResumeLayout(false);
		this.panel8.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.Tab_C.ResumeLayout(false);
		this.panel12.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.tabProjectList).EndInit();
		this.tabProjectList.ResumeLayout(false);
		this.panel13.ResumeLayout(false);
		this.panel11.ResumeLayout(false);
		this.Tab_D.ResumeLayout(false);
		this.Tab_E.ResumeLayout(false);
		this.panel15.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).EndInit();
		this.Tab_Ctrl.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridSourceProject).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridProjectSelected).EndInit();
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
