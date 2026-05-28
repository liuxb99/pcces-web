using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.DomainModule.CostStructure;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetCostStructurePicker : Form
{
	private const string CallFormHelp = "FormBudgetSplit";

	private IContainer components = null;

	private Panel panel1;

	internal UltraTree treeCostStructure;

	private UltraComboEditor cmbCostStructure;

	private Label label1;

	private Panel panel16;

	private GroupBox groupBox6;

	private UltraButton D_Btn_Cncl;

	private UltraButton D_Btn_Next;

	protected CostStructure _CostStructure = new CostStructure();

	private string F_UserID;

	private DataTable CostStructureDT;

	private DataSet ItemADS;

	private DataTable ItemADT;

	private string F_ProjectCode = "";

	private string F_ActionName = "bud";

	private string F_TargetPrintNo = "";

	private DataTable DT_Clipboard = new DataTable();

	private string sIniFileName = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "PccesMain.ini";

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

	public string _ActionName
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

	public string _budPrintNo
	{
		get
		{
			return F_TargetPrintNo;
		}
		set
		{
			F_TargetPrintNo = value;
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetCostStructurePicker));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTree.Override _override1 = new Infragistics.Win.UltraWinTree.Override();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel16 = new System.Windows.Forms.Panel();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.D_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.D_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.treeCostStructure = new Infragistics.Win.UltraWinTree.UltraTree();
		this.cmbCostStructure = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.label1 = new System.Windows.Forms.Label();
		this.panel1.SuspendLayout();
		this.panel16.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.treeCostStructure).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cmbCostStructure).BeginInit();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.panel16);
		this.panel1.Controls.Add(this.treeCostStructure);
		this.panel1.Controls.Add(this.cmbCostStructure);
		this.panel1.Controls.Add(this.label1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(497, 549);
		this.panel1.TabIndex = 1;
		this.panel16.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel16.Controls.Add(this.groupBox6);
		this.panel16.Controls.Add(this.D_Btn_Cncl);
		this.panel16.Controls.Add(this.D_Btn_Next);
		this.panel16.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel16.Location = new System.Drawing.Point(0, 505);
		this.panel16.Name = "panel16";
		this.panel16.Size = new System.Drawing.Size(497, 44);
		this.panel16.TabIndex = 20;
		this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox6.Location = new System.Drawing.Point(0, 0);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(497, 8);
		this.groupBox6.TabIndex = 4;
		this.groupBox6.TabStop = false;
		this.D_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Cncl.Appearance = appearance1;
		this.D_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.D_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Cncl.Location = new System.Drawing.Point(403, 9);
		this.D_Btn_Cncl.Name = "D_Btn_Cncl";
		this.D_Btn_Cncl.ShowFocusRect = false;
		this.D_Btn_Cncl.ShowOutline = false;
		this.D_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Cncl.SupportThemes = false;
		this.D_Btn_Cncl.TabIndex = 2;
		this.D_Btn_Cncl.Text = "取消";
		this.D_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Next.Appearance = appearance2;
		this.D_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Next.Location = new System.Drawing.Point(311, 9);
		this.D_Btn_Next.Name = "D_Btn_Next";
		this.D_Btn_Next.ShowFocusRect = false;
		this.D_Btn_Next.ShowOutline = false;
		this.D_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Next.SupportThemes = false;
		this.D_Btn_Next.TabIndex = 1;
		this.D_Btn_Next.Text = "確定";
		this.D_Btn_Next.Click += new System.EventHandler(D_Btn_Next_Click);
		appearance3.BackColor = System.Drawing.Color.White;
		this.treeCostStructure.Appearance = appearance3;
		this.treeCostStructure.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.treeCostStructure.HideSelection = false;
		this.treeCostStructure.Indent = 15;
		this.treeCostStructure.Location = new System.Drawing.Point(12, 66);
		this.treeCostStructure.Name = "treeCostStructure";
		_override1.SelectionType = Infragistics.Win.UltraWinTree.SelectType.Single;
		this.treeCostStructure.Override = _override1;
		this.treeCostStructure.Size = new System.Drawing.Size(469, 423);
		this.treeCostStructure.TabIndex = 3;
		this.treeCostStructure.AfterCheck += new Infragistics.Win.UltraWinTree.AfterNodeChangedEventHandler(ultraTree1_AfterCheck);
		appearance4.FontData.Name = "細明體";
		appearance4.FontData.SizeInPoints = 9f;
		this.cmbCostStructure.Appearance = appearance4;
		this.cmbCostStructure.AutoSize = true;
		this.cmbCostStructure.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		this.cmbCostStructure.Location = new System.Drawing.Point(207, 27);
		this.cmbCostStructure.Name = "cmbCostStructure";
		this.cmbCostStructure.Size = new System.Drawing.Size(182, 21);
		this.cmbCostStructure.TabIndex = 2;
		this.cmbCostStructure.Text = null;
		this.cmbCostStructure.ValueChanged += new System.EventHandler(cmbCostStructure_ValueChanged);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("細明體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label1.Location = new System.Drawing.Point(12, 27);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(189, 19);
		this.label1.TabIndex = 0;
		this.label1.Text = "成本架構主要分類：";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(497, 549);
		base.Controls.Add(this.panel1);
		base.Name = "FormBudgetCostStructurePicker";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "成本架構挑選";
		base.Load += new System.EventHandler(FormBudgetCostStructurePicker_Load);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.panel16.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.treeCostStructure).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cmbCostStructure).EndInit();
		base.ResumeLayout(false);
	}

	public FormBudgetCostStructurePicker()
	{
		InitializeComponent();
	}

	private void FormBudgetCostStructurePicker_Load(object sender, EventArgs e)
	{
		BudItemA oItemA = new BudItemA();
		ItemADS = oItemA.GetItemA(F_ProjectCode, 0);
		if (ItemADS.Tables.Count > 0)
		{
			ItemADT = ItemADS.Tables[0];
		}
		ProcessCostStructure();
		CreateEDIT_DataTable();
	}

	private string InitialCostStructureTypeID()
	{
		string TypeID = "";
		DataView dvItemA = new DataView(ItemADT);
		dvItemA.RowFilter = "PrintNo = '" + F_TargetPrintNo + "'";
		if (dvItemA.Count > 0)
		{
			if (dvItemA[0]["TypeID"] != DBNull.Value && dvItemA[0]["TypeID"] != null && dvItemA[0]["TypeID"].ToString() != "")
			{
				TypeID = dvItemA[0]["TypeID"].ToString();
			}
		}
		else
		{
			int ChildPrintNoLen = F_TargetPrintNo.Length + 4;
			dvItemA.RowFilter = "PrintNo like '" + F_TargetPrintNo + "%' and len(trim(PrintNo)) =" + ChildPrintNoLen + " and TypeID is not null";
			if (dvItemA.Count > 0)
			{
				TypeID = dvItemA[0]["TypeID"].ToString();
			}
		}
		return TypeID;
	}

	private void ProcessCostStructure()
	{
		string TypeID = InitialCostStructureTypeID();
		DataTable dt = _CostStructure.ListItemCostType();
		cmbCostStructure.Items.Clear();
		if (dt != null)
		{
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				bool isAdd = false;
				if (TypeID != "" && TypeID == dt.Rows[i]["TypeID"].ToString().Trim())
				{
					isAdd = true;
				}
				else if (TypeID == "")
				{
					isAdd = true;
				}
				if (isAdd)
				{
					ValueListItem valueListItem = new ValueListItem();
					valueListItem.DataValue = dt.Rows[i]["TypeID"].ToString();
					valueListItem.DisplayText = dt.Rows[i]["TypeName"].ToString();
					cmbCostStructure.Items.Add(valueListItem);
				}
			}
			if (cmbCostStructure.Items.Count > 0)
			{
				cmbCostStructure.SelectedIndex = 0;
			}
		}
		else
		{
			treeCostStructure.Nodes.Clear();
		}
	}

	private void CreateEDIT_DataTable()
	{
		try
		{
			if (DT_Clipboard.Columns.IndexOf("ProjectCode") <= -1)
			{
				DT_Clipboard.Columns.Add("ProjectCode", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("ItemNo") <= -1)
			{
				DT_Clipboard.Columns.Add("ItemNo", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("CName") <= -1)
			{
				DT_Clipboard.Columns.Add("CName", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("UnitName") <= -1)
			{
				DT_Clipboard.Columns.Add("UnitName", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("Qty") <= -1)
			{
				DT_Clipboard.Columns.Add("Qty", Type.GetType("System.Decimal"));
			}
			if (DT_Clipboard.Columns.IndexOf("Lock") <= -1)
			{
				DT_Clipboard.Columns.Add("Lock", Type.GetType("System.Boolean"));
			}
			if (DT_Clipboard.Columns.IndexOf("Cost") <= -1)
			{
				DT_Clipboard.Columns.Add("Cost", Type.GetType("System.Decimal"));
			}
			if (DT_Clipboard.Columns.IndexOf("Amount") <= -1)
			{
				DT_Clipboard.Columns.Add("Amount", Type.GetType("System.Decimal"));
			}
			if (DT_Clipboard.Columns.IndexOf("PccesCode") <= -1)
			{
				DT_Clipboard.Columns.Add("PccesCode", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("Memo") <= -1)
			{
				DT_Clipboard.Columns.Add("Memo", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("EName") <= -1)
			{
				DT_Clipboard.Columns.Add("EName", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("EUnit") <= -1)
			{
				DT_Clipboard.Columns.Add("EUnit", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("Level") <= -1)
			{
				DT_Clipboard.Columns.Add("Level", Type.GetType("System.Int32"));
			}
			if (DT_Clipboard.Columns.IndexOf("Kind") <= -1)
			{
				DT_Clipboard.Columns.Add("Kind", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("Analysis") <= -1)
			{
				DT_Clipboard.Columns.Add("Analysis", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("SNo") <= -1)
			{
				DT_Clipboard.Columns.Add("SNo", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("Formula") <= -1)
			{
				DT_Clipboard.Columns.Add("Formula", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("PrintNo") <= -1)
			{
				DT_Clipboard.Columns.Add("PrintNo", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("OldPrintNo") <= -1)
			{
				DT_Clipboard.Columns.Add("OldPrintNo", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("PubCode") <= -1)
			{
				DT_Clipboard.Columns.Add("PubCode", Type.GetType("System.Int32"));
			}
			if (DT_Clipboard.Columns.IndexOf("IsShared") <= -1)
			{
				DT_Clipboard.Columns.Add("IsShared", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("IsCollaspse") <= -1)
			{
				DT_Clipboard.Columns.Add("IsCollaspse", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("DBName") <= -1)
			{
				DT_Clipboard.Columns.Add("DBName", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("surName") <= -1)
			{
				DT_Clipboard.Columns.Add("surName", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("fixPrice") <= -1)
			{
				DT_Clipboard.Columns.Add("fixPrice", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("CostUID") <= -1)
			{
				DT_Clipboard.Columns.Add("CostUID", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("ParentCostUID") <= -1)
			{
				DT_Clipboard.Columns.Add("ParentCostUID", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("CostUnit") <= -1)
			{
				DT_Clipboard.Columns.Add("CostUnit", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("IsItemA") <= -1)
			{
				DT_Clipboard.Columns.Add("IsItemA", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("TypeID") <= -1)
			{
				DT_Clipboard.Columns.Add("TypeID", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("Property1") <= -1)
			{
				DT_Clipboard.Columns.Add("Property1", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("Property2") <= -1)
			{
				DT_Clipboard.Columns.Add("Property2", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("Property3") <= -1)
			{
				DT_Clipboard.Columns.Add("Property3", Type.GetType("System.String"));
			}
			if (DT_Clipboard.Columns.IndexOf("IsItemA") <= -1)
			{
				DT_Clipboard.Columns.Add("IsItemA", Type.GetType("System.String"));
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudget.cs--CreateEDIT_DataTable" + ex.Message);
			Console.Write(ex.Message);
		}
	}

	private void cmbCostStructure_ValueChanged(object sender, EventArgs e)
	{
		string F_CostType = cmbCostStructure.SelectedItem.DataValue.ToString().Trim();
		string F_Name = cmbCostStructure.SelectedItem.DisplayText.Trim();
		CostStructureDT = _CostStructure.ListItem("", F_CostType);
		DataTable CostDTParent = _CostStructure.ListItemParent(1, F_CostType);
		treeCostStructure.Nodes.Clear();
		if (CostDTParent.Rows.Count > 0)
		{
			UltraTreeNode node = treeCostStructure.Nodes.Add("ROOT", F_Name);
			for (int i = 0; i < CostDTParent.Rows.Count; i++)
			{
				string CostStructureRootUID = CostDTParent.Rows[i]["ParentUID"].ToString().Trim();
				PopCostStructureTree(node, CostStructureDT, CostStructureRootUID);
			}
			node.ExpandAll();
		}
	}

	private void PopCostStructureTree(UltraTreeNode treeNode, DataTable DT, string ParentUID)
	{
		string filterExp = " ParentUID = '" + ParentUID + "'";
		string sortExp = " iSort ASC ";
		DataRow[] rows = null;
		rows = DT.Select(filterExp, sortExp);
		DataView DV = new DataView(DT);
		DataRow[] array = rows;
		foreach (DataRow row in array)
		{
			string itemCode = row["CostUID"] as string;
			string cName = row["cName"].ToString().Trim();
			UltraTreeNode node = treeNode.Nodes.Add(itemCode, cName);
			node.Tag = new ExtendedNodeInfo(typeof(string), "CostUID");
			DV.RowFilter = " ParentUID = '" + itemCode + "'";
			if (DV.Count == 0)
			{
				row["iRec"] = "1";
				node.Override.NodeStyle = NodeStyle.CheckBox;
				node.CheckedState = CheckState.Unchecked;
			}
			PopCostStructureTree(node, DT, itemCode);
		}
		DV.Dispose();
		DV = null;
	}

	private void ultraTree1_AfterCheck(object sender, NodeEventArgs e)
	{
		string CostUID = e.TreeNode.Key.ToString();
		DataView dv = new DataView(CostStructureDT);
		dv.RowFilter = "CostUID = '" + CostUID + "'";
		if (dv.Count > 0)
		{
			if (e.TreeNode.CheckedState == CheckState.Checked)
			{
				dv[0]["Checked"] = "1";
			}
			else
			{
				dv[0]["Checked"] = "";
			}
		}
		dv.Dispose();
		dv = null;
	}

	private void D_Btn_Next_Click(object sender, EventArgs e)
	{
		DT_Clipboard.Rows.Clear();
		GetSelectedCostStructure();
		if (DT_Clipboard.Rows.Count > 0)
		{
			AddSelectedCostStructure2ItemA();
			base.DialogResult = DialogResult.OK;
		}
		else
		{
			MessageBox.Show("你並沒有勾選任何項目", "警示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void AddSelectedCostStructure2ItemA()
	{
		string ParentPrintNo = F_TargetPrintNo;
		DataView dvItemA = new DataView(ItemADT);
		dvItemA.RowFilter = "PrintNo = '" + F_TargetPrintNo + "'";
		bool IsCostStructure = false;
		if (dvItemA.Count > 0)
		{
			string CostUID = "";
			if (dvItemA[0]["CostUID"] != DBNull.Value && dvItemA[0]["CostUID"].ToString() != "")
			{
				CostUID = dvItemA[0]["CostUID"].ToString();
				string TempParentPrintNo = "";
				for (int i = ParentPrintNo.Length / 4; i > 0; i--)
				{
					string TempPrintNo = ParentPrintNo.Substring(0, 4 * i);
					dvItemA.RowFilter = "PrintNo = '" + TempPrintNo + "'";
					if (dvItemA.Count > 0 && (dvItemA[0]["CostUID"] == DBNull.Value || dvItemA[0]["CostUID"].ToString() == ""))
					{
						TempParentPrintNo = dvItemA[0]["PrintNo"].ToString().Trim();
						break;
					}
				}
				ParentPrintNo = TempParentPrintNo;
				IsCostStructure = true;
			}
			else
			{
				int ChildPrintNoLen = ParentPrintNo.Length + 4;
				dvItemA.RowFilter = "PrintNo like '" + ParentPrintNo + "%' and len(trim(PrintNo))=" + ChildPrintNoLen + " and CostUID is not null";
				if (dvItemA.Count > 0)
				{
					IsCostStructure = true;
				}
			}
		}
		else
		{
			dvItemA.RowFilter = "len(trim(PrintNo))=4 and CostUID is not null";
			IsCostStructure = dvItemA.Count > 0;
		}
		DataView dvCost = new DataView(DT_Clipboard);
		dvCost.Sort = "PrintNo";
		if (!IsCostStructure)
		{
			CostStructure2ItemA(ParentPrintNo, ItemADT, dvCost);
		}
		else
		{
			CostStructure2CostStructure(ParentPrintNo, ItemADT, dvCost);
		}
		BudItemA oItemA = new BudItemA();
		oItemA.GetDatasetUpdate(ItemADS);
		dvItemA.Dispose();
		dvItemA = null;
	}

	private int GetLast4PrintNo(string PrintNo)
	{
		PrintNo = PrintNo.Trim();
		int NextNum = 1;
		if (PrintNo.Length >= 4)
		{
			try
			{
				string LastNum = PrintNo.Substring(PrintNo.Length - 4);
				NextNum = int.Parse(LastNum);
			}
			catch
			{
			}
		}
		return NextNum;
	}

	private void CostStructure2ItemA(string ParentPrintNo, DataTable ItemADT, DataView dvCost)
	{
		int NextNum = 0;
		int ChildPrintNoLen = ParentPrintNo.Length + 4;
		DataView dvItemA = new DataView(ItemADT);
		dvItemA.RowFilter = "PrintNo like '" + ParentPrintNo + "%' and Len(trim(PrintNo)) = " + ChildPrintNoLen;
		dvItemA.Sort = "PrintNo";
		if (dvItemA.Count > 0)
		{
			NextNum = GetLast4PrintNo(dvItemA[dvItemA.Count - 1]["PrintNo"].ToString());
		}
		string CurPrintNoPrefix = "initial";
		string NewPrintNoPrefix = "";
		for (int i = 0; i < dvCost.Count; i++)
		{
			string OldPrintNoPrefix = dvCost[i]["PrintNo"].ToString().Substring(0, 8);
			if (CurPrintNoPrefix != OldPrintNoPrefix)
			{
				NextNum++;
				CurPrintNoPrefix = OldPrintNoPrefix;
				NewPrintNoPrefix = ParentPrintNo + NextNum.ToString().PadLeft(4, '0');
			}
			InsertCostItem2ItemA(ItemADT, dvCost[i].Row, NewPrintNoPrefix + dvCost[i]["PrintNo"].ToString().Substring(8));
		}
		dvItemA.Dispose();
		dvItemA = null;
	}

	private void CostStructure2CostStructure(string ParentPrintNo, DataTable ItemADT, DataView dvCost)
	{
		DataView dvCostStructure = new DataView(CostStructureDT);
		DataView dvItemA = new DataView(ItemADT);
		string PreConstraint = "PrintNo like '" + ParentPrintNo + "%'";
		for (int i = 0; i < dvCost.Count; i++)
		{
			string CostUID = dvCost[i]["CostUID"].ToString();
			dvItemA.RowFilter = PreConstraint + " and CostUID = '" + CostUID + "'";
			if (dvItemA.Count != 0)
			{
				continue;
			}
			string TargetPrintNo = ParentPrintNo + "0000";
			string ParentCostUID = dvCost[i]["ParentCostUID"].ToString();
			dvItemA.RowFilter = PreConstraint + " and CostUID='" + ParentCostUID + "'";
			if (dvItemA.Count > 0)
			{
				TargetPrintNo = dvItemA[0]["PrintNo"].ToString().Trim() + "0000";
			}
			string CostItemPrintNo = dvCost[i]["PrintNo"].ToString();
			string CostItemParentPrintNo = CostItemPrintNo.Substring(0, CostItemPrintNo.Length - 4);
			int PrintNoLen = ParentPrintNo.Length + CostItemPrintNo.Length - 4;
			dvCostStructure.RowFilter = "Len(trim(iSort)) =" + CostItemPrintNo.Length + " and iSort < '" + CostItemPrintNo + "' and iSort like '" + CostItemParentPrintNo + "%'";
			dvCostStructure.Sort = "iSort desc";
			for (int Index = 0; Index < dvCostStructure.Count; Index++)
			{
				string theCostUID = dvCostStructure[Index]["CostUID"].ToString();
				dvItemA.RowFilter = PreConstraint + " and Len(trim(PrintNo)) = " + PrintNoLen + " and CostUID ='" + theCostUID + "'";
				if (dvItemA.Count > 0)
				{
					TargetPrintNo = dvItemA[0]["PrintNo"].ToString().Trim();
					break;
				}
			}
			InsertCostItem2ItemA(TargetPrintNo, ItemADT, dvCost[i].Row);
		}
	}

	private void InsertCostItem2ItemA(string TargetPrintNo, DataTable ItemADT, DataRow theRow)
	{
		DataView dvItemA = new DataView(ItemADT);
		string ParentPrintNo = TargetPrintNo.Substring(0, TargetPrintNo.Length - 4);
		if (ParentPrintNo != "")
		{
			dvItemA.RowFilter = "PrintNo like '" + ParentPrintNo + "%' and Len(trim(PrintNo)) =" + TargetPrintNo.Length + " and PrintNo > '" + TargetPrintNo + "'";
		}
		else
		{
			dvItemA.RowFilter = "Len(trim(PrintNo)) =" + TargetPrintNo.Length + " and PrintNo > '" + TargetPrintNo + "'";
		}
		dvItemA.Sort = "PrintNo DESC";
		string PrintNo = "";
		int Last4PrintNo;
		for (int i = 0; i < dvItemA.Count; i++)
		{
			PrintNo = dvItemA[i]["PrintNo"].ToString();
			Last4PrintNo = GetLast4PrintNo(PrintNo) + 1;
			ShiftItemA(ItemADT, dvItemA[i]["PrintNo"].ToString(), PrintNo.Substring(0, PrintNo.Length - 4) + Last4PrintNo.ToString().PadLeft(4, '0'));
		}
		dvItemA.Dispose();
		dvItemA = null;
		Last4PrintNo = GetLast4PrintNo(TargetPrintNo) + 1;
		InsertCostItem2ItemA(ItemADT, theRow, TargetPrintNo.Substring(0, TargetPrintNo.Length - 4) + Last4PrintNo.ToString().PadLeft(4, '0'));
	}

	private void ShiftItemA(DataTable ItemADT, string OldPrintNo, string NewPrintNo)
	{
		if (OldPrintNo != NewPrintNo)
		{
			DataView dvItemA = new DataView(ItemADT);
			dvItemA.RowFilter = "PrintNo like '" + OldPrintNo + "%'";
			while (dvItemA.Count > 0)
			{
				string OriginalPrintNo = dvItemA[0]["PrintNo"].ToString().Trim();
				dvItemA[0]["PrintNo"] = NewPrintNo + OriginalPrintNo.Substring(OldPrintNo.Length);
			}
		}
	}

	private void InsertCostItem2ItemA(DataTable dtItemA, DataRow drCost, string PrintNo)
	{
		DataRow newRow = dtItemA.NewRow();
		newRow["PrintNo"] = PrintNo;
		newRow["projectCode"] = _ProjectCode;
		newRow["pubCode"] = 0;
		newRow["ItemNo"] = drCost["ItemNo"];
		newRow["Kind"] = "B";
		newRow["CName"] = drCost["CName"];
		newRow["Amount"] = drCost["Amount"];
		newRow["Cost"] = drCost["Cost"];
		newRow["EName"] = drCost["EName"];
		newRow["EUnit"] = drCost["EUnit"];
		newRow["Memo"] = drCost["Memo"];
		newRow["Qty"] = drCost["Qty"];
		newRow["CostUID"] = drCost["CostUID"];
		newRow["CostUnit"] = drCost["CostUnit"];
		newRow["CostUID"] = drCost["CostUID"];
		newRow["TypeID"] = drCost["TypeID"];
		newRow["setDecimal"] = 0;
		dtItemA.Rows.Add(newRow);
	}

	private void GetSelectedCostStructure()
	{
		DataView DV = new DataView(CostStructureDT);
		DV.RowFilter = "Checked = '1'";
		for (int i = 0; i < DV.Count; i++)
		{
			AddParentByPrintOrder(DV[i]["iSort"].ToString().Trim());
		}
	}

	private void AddParentByPrintOrder(string PrintNo)
	{
		int iStart = 2;
		int iEnd = PrintNo.Length / 4;
		int iLevel = iEnd - iStart;
		string sPrintNo = "";
		for (int i = 0; i <= iLevel; i++)
		{
			sPrintNo = PrintNo.Substring(0, (iStart + i) * 4);
			if (i == iLevel)
			{
				AddASelectedRow(sPrintNo, isB: false);
			}
			else
			{
				AddASelectedRow(sPrintNo, isB: true);
			}
		}
	}

	private void AddASelectedRow(string PrintNo, bool isB)
	{
		DataView dv = new DataView(CostStructureDT);
		dv.RowFilter = "iSort='" + PrintNo + "'";
		if (dv.Count > 0)
		{
			DataView DV = new DataView(DT_Clipboard);
			DV.RowFilter = "CostUID = '" + dv[0]["CostUID"].ToString().Trim() + "'";
			if (DV.Count == 0)
			{
				DataRow dr = DT_Clipboard.NewRow();
				dr["CostUID"] = dv[0]["CostUID"];
				dr["ParentCostUID"] = dv[0]["ParentUID"];
				dr["CName"] = dv[0]["cName"];
				dr["UnitName"] = dv[0]["CostUnit"];
				dr["CostUnit"] = dv[0]["CostUnit"];
				dr["Property1"] = dv[0]["Property1"];
				dr["Property2"] = dv[0]["Property2"];
				dr["Property3"] = dv[0]["Property3"];
				dr["PrintNo"] = dv[0]["iSort"];
				dr["Qty"] = "1";
				dr["Lock"] = "false";
				dr["Cost"] = "0";
				dr["Amount"] = "0";
				if (isB)
				{
					dr["Kind"] = "B";
				}
				else
				{
					dr["Kind"] = "";
				}
				dr["IsItemA"] = "";
				dr["TypeID"] = cmbCostStructure.Items[cmbCostStructure.SelectedIndex].DataValue;
				DT_Clipboard.Rows.Add(dr);
			}
		}
		dv.Dispose();
		dv = null;
	}
}
