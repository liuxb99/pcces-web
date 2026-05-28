using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.MrsBase;
using C1.Win.C1FlexGrid;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetRes_CNameCorrect : Form
{
	private IContainer components = null;

	private Panel panel1;

	private Label label1;

	private UltraButton BtnOK;

	private UltraButton BtnCancel;

	private Label label2;

	private Label lbl_ItemCount;

	private ProgressBar progressBar1;

	public GridMrsBase gridMrsBase1;

	private Button btnSelectAll;

	private CheckBox checkBox1;

	private int F_TotalItemCount = 0;

	private int F_CorrectableItemCount = 0;

	private string F_UserID = "";

	private string F_ProjectCode = "";

	private PccesFormAction FormActionName;

	public int _TotalItemCount
	{
		get
		{
			return F_TotalItemCount;
		}
		set
		{
			F_TotalItemCount = value;
		}
	}

	public int _CorrectableItemCount
	{
		get
		{
			return F_CorrectableItemCount;
		}
		set
		{
			F_CorrectableItemCount = value;
		}
	}

	public PccesFormAction _FormActionName
	{
		get
		{
			return FormActionName;
		}
		set
		{
			FormActionName = value;
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
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetRes_CNameCorrect));
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.BtnOK = new Infragistics.Win.Misc.UltraButton();
		this.BtnCancel = new Infragistics.Win.Misc.UltraButton();
		this.label2 = new System.Windows.Forms.Label();
		this.lbl_ItemCount = new System.Windows.Forms.Label();
		this.progressBar1 = new System.Windows.Forms.ProgressBar();
		this.btnSelectAll = new System.Windows.Forms.Button();
		this.gridMrsBase1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase1).BeginInit();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.Controls.Add(this.label1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(937, 50);
		this.panel1.TabIndex = 0;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.label1.Location = new System.Drawing.Point(21, 14);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(359, 15);
		this.label1.TabIndex = 0;
		this.label1.Text = "將工項名稱修正為：正確率檢查後之正確工項名稱";
		this.BtnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		appearance5.Image = resources.GetObject("appearance5.Image");
		this.BtnOK.Appearance = appearance5;
		this.BtnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnOK.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance6.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance6.BackColor2 = System.Drawing.Color.White;
		appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.BtnOK.HotTrackAppearance = appearance6;
		this.BtnOK.HotTracking = true;
		this.BtnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnOK.Location = new System.Drawing.Point(708, 513);
		this.BtnOK.Name = "BtnOK";
		this.BtnOK.ShowFocusRect = false;
		this.BtnOK.Size = new System.Drawing.Size(119, 31);
		this.BtnOK.SupportThemes = false;
		this.BtnOK.TabIndex = 5;
		this.BtnOK.Text = "確定執行";
		this.BtnOK.Click += new System.EventHandler(BtnOK_Click);
		this.BtnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		appearance7.Image = resources.GetObject("appearance7.Image");
		this.BtnCancel.Appearance = appearance7;
		this.BtnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.BtnCancel.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance8.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance8.BackColor2 = System.Drawing.Color.White;
		appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.BtnCancel.HotTrackAppearance = appearance8;
		this.BtnCancel.HotTracking = true;
		this.BtnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnCancel.Location = new System.Drawing.Point(828, 513);
		this.BtnCancel.Name = "BtnCancel";
		this.BtnCancel.ShowFocusRect = false;
		this.BtnCancel.Size = new System.Drawing.Size(94, 31);
		this.BtnCancel.SupportThemes = false;
		this.BtnCancel.TabIndex = 4;
		this.BtnCancel.Text = "取消";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.label2.Location = new System.Drawing.Point(12, 67);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(135, 15);
		this.label2.TabIndex = 6;
		this.label2.Text = "可修正之工項數：";
		this.lbl_ItemCount.AutoSize = true;
		this.lbl_ItemCount.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbl_ItemCount.Location = new System.Drawing.Point(152, 66);
		this.lbl_ItemCount.Name = "lbl_ItemCount";
		this.lbl_ItemCount.Size = new System.Drawing.Size(14, 15);
		this.lbl_ItemCount.TabIndex = 7;
		this.lbl_ItemCount.Text = "0";
		this.progressBar1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.progressBar1.Location = new System.Drawing.Point(142, 107);
		this.progressBar1.Name = "progressBar1";
		this.progressBar1.Size = new System.Drawing.Size(778, 23);
		this.progressBar1.TabIndex = 8;
		this.btnSelectAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnSelectAll.Location = new System.Drawing.Point(24, 504);
		this.btnSelectAll.Name = "btnSelectAll";
		this.btnSelectAll.Size = new System.Drawing.Size(46, 25);
		this.btnSelectAll.TabIndex = 10;
		this.btnSelectAll.Text = "全選";
		this.btnSelectAll.UseVisualStyleBackColor = true;
		this.btnSelectAll.Visible = false;
		this.btnSelectAll.Click += new System.EventHandler(btnSelectAll_Click);
		this.gridMrsBase1._ExcelFileName = "";
		this.gridMrsBase1._ExcelSheeName = "";
		this.gridMrsBase1._IsOpenExcelAfterExport = false;
		this.gridMrsBase1.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
		this.gridMrsBase1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.gridMrsBase1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gridMrsBase1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridMrsBase1.ColumnInfo = resources.GetString("gridMrsBase1.ColumnInfo");
		this.gridMrsBase1.ExtendLastCol = true;
		this.gridMrsBase1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridMrsBase1.ForeColor = System.Drawing.Color.Black;
		this.gridMrsBase1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.gridMrsBase1.IsProcessUndo = false;
		this.gridMrsBase1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.gridMrsBase1.Location = new System.Drawing.Point(12, 138);
		this.gridMrsBase1.Name = "gridMrsBase1";
		this.gridMrsBase1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridMrsBase1.ShowCursor = true;
		this.gridMrsBase1.ShowToolTipOnNarrowColumn = true;
		this.gridMrsBase1.Size = new System.Drawing.Size(910, 360);
		this.gridMrsBase1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridMrsBase1.Styles"));
		this.gridMrsBase1.TabIndex = 9;
		this.gridMrsBase1.UndoMax = 10;
		this.checkBox1.AutoSize = true;
		this.checkBox1.Location = new System.Drawing.Point(63, 111);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(48, 16);
		this.checkBox1.TabIndex = 11;
		this.checkBox1.Text = "全選";
		this.checkBox1.UseVisualStyleBackColor = true;
		this.checkBox1.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
		base.AcceptButton = this.BtnOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.BtnCancel;
		base.ClientSize = new System.Drawing.Size(937, 555);
		base.Controls.Add(this.checkBox1);
		base.Controls.Add(this.btnSelectAll);
		base.Controls.Add(this.gridMrsBase1);
		base.Controls.Add(this.progressBar1);
		base.Controls.Add(this.lbl_ItemCount);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.BtnOK);
		base.Controls.Add(this.BtnCancel);
		base.Controls.Add(this.panel1);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormBudgetRes_CNameCorrect";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "名稱修正";
		base.Load += new System.EventHandler(FormBudgetRes_CNameCorrect_Load);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public FormBudgetRes_CNameCorrect()
	{
		InitializeComponent();
		base.Width = (int)((double)Screen.PrimaryScreen.WorkingArea.Width * 0.85);
	}

	private void FormBudgetRes_CNameCorrect_Load(object sender, EventArgs e)
	{
		lbl_ItemCount.Text = F_CorrectableItemCount.ToString();
		progressBar1.Maximum = F_TotalItemCount;
		progressBar1.Minimum = 0;
		progressBar1.Value = 0;
		if (F_CorrectableItemCount == 0)
		{
			BtnOK.Enabled = false;
			gridMrsBase1.Rows.Count = 1;
			return;
		}
		gridMrsBase1.Rows.Count = F_CorrectableItemCount + 1;
		int iRow = 0;
		if (FormActionName == PccesFormAction.MrsBase)
		{
			DBClass DBCls = new DBClass();
			DBCls._FS_UserID = F_UserID;
			DataTable DT_MrsBase = DBCls.GetUserDefine("Select pubCode,PccesCode,CName,surName,unitName,usrQty,Cost,usrAmt,Correct,Confirm,CompareErrState,CorrectCName,CorrectUnitName from mrsBaseA Order By PccesCode");
			DataView DV = DT_MrsBase.DefaultView;
			for (int i = 1; i < DV.Count; i++)
			{
				if (!(DV[i]["CorrectCName"].ToString().Trim() == "") || !(DV[i]["CorrectUnitName"].ToString().Trim() == ""))
				{
					iRow++;
					gridMrsBase1[iRow, "check"] = false;
					gridMrsBase1[iRow, "pubCode"] = DV[i]["pubCode"];
					gridMrsBase1[iRow, "PccesCode"] = DV[i]["PccesCode"];
					gridMrsBase1[iRow, "CName"] = DV[i]["CName"];
					gridMrsBase1[iRow, "surName"] = DV[i]["surName"];
					gridMrsBase1[iRow, "unitName"] = DV[i]["unitName"];
					gridMrsBase1[iRow, "usrQty"] = DV[i]["usrQty"];
					gridMrsBase1[iRow, "Cost"] = DV[i]["Cost"];
					gridMrsBase1[iRow, "usrAmt"] = DV[i]["usrAmt"];
					gridMrsBase1[iRow, "Correct"] = DV[i]["Correct"];
					gridMrsBase1[iRow, "Confirm"] = DV[i]["Confirm"];
					gridMrsBase1[iRow, "CompareErrState"] = DV[i]["CompareErrState"];
					gridMrsBase1[iRow, "CorrectCName"] = DV[i]["CorrectCName"];
					gridMrsBase1[iRow, "CorrectUnitName"] = DV[i]["CorrectUnitName"];
				}
			}
			gridMrsBase1.Cols["PccesCode"].Style.BackColor = Color.YellowGreen;
			gridMrsBase1.Cols["CName"].Style.BackColor = Color.YellowGreen;
			gridMrsBase1.Cols["unitName"].Style.BackColor = Color.YellowGreen;
			gridMrsBase1.Cols["CorrectCName"].Style.BackColor = Color.LightYellow;
			gridMrsBase1.Cols["CorrectUnitName"].Style.BackColor = Color.LightYellow;
			return;
		}
		for (int i = 1; i < (base.Owner as FormBudgetRes).gridMrsBase1.Rows.Count; i++)
		{
			if (!((base.Owner as FormBudgetRes).gridMrsBase1[i, "CorrectCName"].ToString().Trim() == "") || !((base.Owner as FormBudgetRes).gridMrsBase1[i, "CorrectUnitName"].ToString().Trim() == ""))
			{
				iRow++;
				gridMrsBase1[iRow, "check"] = false;
				gridMrsBase1[iRow, "pubCode"] = (base.Owner as FormBudgetRes).gridMrsBase1[i, "pubCode"];
				gridMrsBase1[iRow, "PccesCode"] = (base.Owner as FormBudgetRes).gridMrsBase1[i, "PccesCode"];
				gridMrsBase1[iRow, "CName"] = (base.Owner as FormBudgetRes).gridMrsBase1[i, "CName"];
				gridMrsBase1[iRow, "surName"] = (base.Owner as FormBudgetRes).gridMrsBase1[i, "surName"];
				gridMrsBase1[iRow, "unitName"] = (base.Owner as FormBudgetRes).gridMrsBase1[i, "unitName"];
				gridMrsBase1[iRow, "usrQty"] = (base.Owner as FormBudgetRes).gridMrsBase1[i, "usrQty"];
				gridMrsBase1[iRow, "Cost"] = (base.Owner as FormBudgetRes).gridMrsBase1[i, "Cost"];
				gridMrsBase1[iRow, "usrAmt"] = (base.Owner as FormBudgetRes).gridMrsBase1[i, "usrAmt"];
				gridMrsBase1[iRow, "Correct"] = (base.Owner as FormBudgetRes).gridMrsBase1[i, "Correct"];
				gridMrsBase1[iRow, "Confirm"] = (base.Owner as FormBudgetRes).gridMrsBase1[i, "Confirm"];
				gridMrsBase1[iRow, "CompareErrState"] = (base.Owner as FormBudgetRes).gridMrsBase1[i, "CompareErrState"];
				gridMrsBase1[iRow, "CorrectCName"] = (base.Owner as FormBudgetRes).gridMrsBase1[i, "CorrectCName"];
				gridMrsBase1[iRow, "CorrectUnitName"] = (base.Owner as FormBudgetRes).gridMrsBase1[i, "CorrectUnitName"];
			}
		}
		gridMrsBase1.Cols["PccesCode"].Style.BackColor = Color.YellowGreen;
		gridMrsBase1.Cols["CName"].Style.BackColor = Color.YellowGreen;
		gridMrsBase1.Cols["unitName"].Style.BackColor = Color.YellowGreen;
		gridMrsBase1.Cols["CorrectCName"].Style.BackColor = Color.LightYellow;
		gridMrsBase1.Cols["CorrectUnitName"].Style.BackColor = Color.LightYellow;
	}

	private void BtnOK_Click(object sender, EventArgs e)
	{
		int iSel = 0;
		for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
		{
			if ((bool)gridMrsBase1[i, "check"])
			{
				iSel++;
			}
		}
		if (iSel == 0)
		{
			MessageBox.Show(this, "尚未勾選任何項目", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (FormActionName == PccesFormAction.MrsBase)
		{
			if ((base.Owner as frmMrsBase).gridMrsBase1.Rows.Count <= 1 || MessageBox.Show(this, "確定要修正名稱及單位?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
			{
				return;
			}
		}
		else if ((base.Owner as FormBudgetRes).gridMrsBase1.Rows.Count <= 1 || MessageBox.Show(this, "確定要修正名稱及單位?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
		{
			return;
		}
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("修正名稱及單位--" + F_ProjectCode + "(" + IPStr + ")");
		MrsBaseA MRSA = new MrsBaseA(F_UserID, aArr);
		MRSA.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		MRSA.ps_projectcode = F_ProjectCode;
		ItemA itemA = new ItemA(aArr);
		itemA.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		itemA.ps_projectCode = F_ProjectCode;
		progressBar1.Visible = true;
		Cursor = Cursors.WaitCursor;
		for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
		{
			progressBar1.Value = i;
			Application.DoEvents();
			Cursor = Cursors.WaitCursor;
			int iNeedToUpdate = 0;
			if (!(bool)gridMrsBase1[i, "check"])
			{
				continue;
			}
			if (gridMrsBase1[i, "CorrectCName"].ToString() != "")
			{
				MRSA.ps_cName = gridMrsBase1[i, "CorrectCName"].ToString();
				MRSA.ps_correctCName = "";
				itemA.ps_cName = gridMrsBase1[i, "CorrectCName"].ToString();
				iNeedToUpdate++;
			}
			if (gridMrsBase1[i, "CorrectUnitName"].ToString() != "")
			{
				MRSA.ps_unitName = gridMrsBase1[i, "CorrectUnitName"].ToString();
				MRSA.ps_correctUnitName = "";
				itemA.ps_unitName = gridMrsBase1[i, "CorrectUnitName"].ToString();
				iNeedToUpdate++;
			}
			if (iNeedToUpdate > 0)
			{
				MRSA.ps_pccesCode = gridMrsBase1[i, "pccesCode"].ToString();
				MRSA.ps_pubCode = gridMrsBase1[i, "pubCode"].ToString();
				itemA.ps_PccesCode = gridMrsBase1[i, "pccesCode"].ToString();
				itemA.ps_pubCode = gridMrsBase1[i, "pubCode"].ToString();
				MRSA.UpdItem();
				if (FormActionName != PccesFormAction.MrsBase)
				{
					itemA.UpdItemByPccesCode();
				}
				Thread.Sleep(10);
			}
			gridMrsBase1[i, "check"] = false;
			MRSA.ps_cName = null;
			MRSA.ps_correctCName = null;
			itemA.ps_cName = null;
			MRSA.ps_unitName = null;
			MRSA.ps_correctUnitName = null;
			itemA.ps_unitName = null;
			MRSA.ps_pccesCode = null;
			MRSA.ps_pubCode = null;
			itemA.ps_PccesCode = null;
			itemA.ps_pubCode = null;
		}
		MRSA = null;
		itemA = null;
		Cursor = Cursors.Default;
		progressBar1.Visible = false;
		MessageBox.Show(this, "名稱修正完成!!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		base.DialogResult = DialogResult.OK;
	}

	private void btnSelectAll_Click(object sender, EventArgs e)
	{
		if (gridMrsBase1.Rows.Count != 1)
		{
			for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
			{
				gridMrsBase1[i, "check"] = true;
			}
		}
	}

	private void checkBox1_CheckedChanged(object sender, EventArgs e)
	{
		int iCount = 0;
		gridMrsBase1.Redraw = false;
		for (int i = 0; i < gridMrsBase1.Rows.Count - 1; i++)
		{
			if (gridMrsBase1[i + 1, "check"] != null && (bool)gridMrsBase1[i + 1, "check"])
			{
				iCount++;
			}
		}
		if (iCount < gridMrsBase1.Rows.Count - 1)
		{
			for (int i = 0; i < gridMrsBase1.Rows.Count - 1; i++)
			{
				gridMrsBase1[i + 1, "check"] = true;
			}
		}
		else
		{
			for (int i = 0; i < gridMrsBase1.Rows.Count - 1; i++)
			{
				gridMrsBase1[i + 1, "check"] = false;
			}
		}
		gridMrsBase1.Redraw = true;
	}
}
