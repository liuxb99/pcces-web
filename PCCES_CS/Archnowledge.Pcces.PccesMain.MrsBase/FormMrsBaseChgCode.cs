using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.DomainModule.MrsBase;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.MrsBase;

public class FormMrsBaseChgCode : Form
{
	private Panel panel8;

	private GroupBox gbButtons;

	private UltraButton btnOK;

	private UltraButton btnCancel;

	private Panel panel1;

	private UltraLabel lbInstruction;

	private UltraTextEditor tbNewPccesCode;

	private GroupBox gbOldData;

	private Label lbPccesCodeText;

	private Label lbWorkItemNameText;

	private Label lbOriginalPccesCode;

	private UltraLabel lbWorkItemName;

	private Container components = null;

	private string userID = "";

	private int pubCode = -1;

	private string originalPccesCode = "";

	private string workItemName = "";

	private PccesFormAction FormActionName;

	private string projectCode;

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

	public int _PubCode
	{
		get
		{
			return pubCode;
		}
		set
		{
			pubCode = value;
		}
	}

	public string _PccesCode
	{
		get
		{
			return originalPccesCode;
		}
		set
		{
			originalPccesCode = value.Trim();
		}
	}

	public string _CName
	{
		get
		{
			return workItemName;
		}
		set
		{
			workItemName = value;
		}
	}

	public PccesFormAction _ActionName
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
			return projectCode;
		}
		set
		{
			projectCode = value;
		}
	}

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.MrsBase.FormMrsBaseChgCode));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		this.panel8 = new System.Windows.Forms.Panel();
		this.btnCancel = new Infragistics.Win.Misc.UltraButton();
		this.gbButtons = new System.Windows.Forms.GroupBox();
		this.btnOK = new Infragistics.Win.Misc.UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.gbOldData = new System.Windows.Forms.GroupBox();
		this.lbWorkItemName = new Infragistics.Win.Misc.UltraLabel();
		this.lbOriginalPccesCode = new System.Windows.Forms.Label();
		this.lbWorkItemNameText = new System.Windows.Forms.Label();
		this.lbPccesCodeText = new System.Windows.Forms.Label();
		this.tbNewPccesCode = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lbInstruction = new Infragistics.Win.Misc.UltraLabel();
		this.panel8.SuspendLayout();
		this.panel1.SuspendLayout();
		this.gbOldData.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.tbNewPccesCode).BeginInit();
		base.SuspendLayout();
		this.panel8.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel8.Controls.Add(this.btnCancel);
		this.panel8.Controls.Add(this.gbButtons);
		this.panel8.Controls.Add(this.btnOK);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel8.Location = new System.Drawing.Point(0, 172);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(408, 44);
		this.panel8.TabIndex = 18;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnCancel.Appearance = appearance1;
		this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
		this.btnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Font = new System.Drawing.Font("細明體", 11f);
		this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btnCancel.Location = new System.Drawing.Point(308, 10);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.ShowFocusRect = false;
		this.btnCancel.Size = new System.Drawing.Size(88, 31);
		this.btnCancel.SupportThemes = false;
		this.btnCancel.TabIndex = 4;
		this.btnCancel.Text = "取消";
		this.gbButtons.Dock = System.Windows.Forms.DockStyle.Top;
		this.gbButtons.Location = new System.Drawing.Point(0, 0);
		this.gbButtons.Name = "gbButtons";
		this.gbButtons.Size = new System.Drawing.Size(408, 8);
		this.gbButtons.TabIndex = 3;
		this.gbButtons.TabStop = false;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnOK.Appearance = appearance2;
		this.btnOK.BackColor = System.Drawing.SystemColors.Control;
		this.btnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOK.Font = new System.Drawing.Font("細明體", 11f);
		this.btnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.btnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.btnOK.Location = new System.Drawing.Point(214, 10);
		this.btnOK.Name = "btnOK";
		this.btnOK.ShowFocusRect = false;
		this.btnOK.Size = new System.Drawing.Size(88, 31);
		this.btnOK.SupportThemes = false;
		this.btnOK.TabIndex = 1;
		this.btnOK.Text = "確定";
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.Controls.Add(this.gbOldData);
		this.panel1.Controls.Add(this.tbNewPccesCode);
		this.panel1.Controls.Add(this.lbInstruction);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(408, 172);
		this.panel1.TabIndex = 19;
		this.gbOldData.Controls.Add(this.lbWorkItemName);
		this.gbOldData.Controls.Add(this.lbOriginalPccesCode);
		this.gbOldData.Controls.Add(this.lbWorkItemNameText);
		this.gbOldData.Controls.Add(this.lbPccesCodeText);
		this.gbOldData.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.gbOldData.Location = new System.Drawing.Point(16, 64);
		this.gbOldData.Name = "gbOldData";
		this.gbOldData.Size = new System.Drawing.Size(376, 96);
		this.gbOldData.TabIndex = 2;
		this.gbOldData.TabStop = false;
		this.gbOldData.Text = "原始資料";
		this.lbWorkItemName.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbWorkItemName.Location = new System.Drawing.Point(88, 48);
		this.lbWorkItemName.Name = "lbWorkItemName";
		this.lbWorkItemName.Size = new System.Drawing.Size(280, 42);
		this.lbWorkItemName.TabIndex = 3;
		this.lbWorkItemName.Text = "[WorkItemName]";
		this.lbOriginalPccesCode.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbOriginalPccesCode.Location = new System.Drawing.Point(88, 24);
		this.lbOriginalPccesCode.Name = "lbOriginalPccesCode";
		this.lbOriginalPccesCode.Size = new System.Drawing.Size(280, 23);
		this.lbOriginalPccesCode.TabIndex = 2;
		this.lbOriginalPccesCode.Text = "[OriginalPccesCode]";
		this.lbWorkItemNameText.Location = new System.Drawing.Point(8, 48);
		this.lbWorkItemNameText.Name = "lbWorkItemNameText";
		this.lbWorkItemNameText.Size = new System.Drawing.Size(88, 23);
		this.lbWorkItemNameText.TabIndex = 1;
		this.lbWorkItemNameText.Text = "工項名稱:";
		this.lbPccesCodeText.Location = new System.Drawing.Point(8, 24);
		this.lbPccesCodeText.Name = "lbPccesCodeText";
		this.lbPccesCodeText.Size = new System.Drawing.Size(88, 23);
		this.lbPccesCodeText.TabIndex = 0;
		this.lbPccesCodeText.Text = "工項代碼:";
		this.tbNewPccesCode.AutoSize = true;
		this.tbNewPccesCode.Location = new System.Drawing.Point(16, 35);
		this.tbNewPccesCode.Name = "tbNewPccesCode";
		this.tbNewPccesCode.Size = new System.Drawing.Size(376, 24);
		this.tbNewPccesCode.TabIndex = 1;
		this.lbInstruction.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lbInstruction.Location = new System.Drawing.Point(12, 11);
		this.lbInstruction.Name = "lbInstruction";
		this.lbInstruction.Size = new System.Drawing.Size(152, 23);
		this.lbInstruction.TabIndex = 0;
		this.lbInstruction.Text = "請輸入新的編碼";
		base.AcceptButton = this.btnOK;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(408, 216);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel8);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormMrsBaseChgCode";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "單筆換碼";
		base.Load += new System.EventHandler(FormMrsBaseChgCode_Load);
		base.Activated += new System.EventHandler(FormMrsBaseChgCode_Activated);
		this.panel8.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.gbOldData.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.tbNewPccesCode).EndInit();
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

	public FormMrsBaseChgCode()
	{
		InitializeComponent();
	}

	private void FormMrsBaseChgCode_Load(object sender, EventArgs e)
	{
		tbNewPccesCode.Text = originalPccesCode;
		lbOriginalPccesCode.Text = originalPccesCode;
		lbWorkItemName.Text = workItemName;
	}

	private void FormMrsBaseChgCode_Activated(object sender, EventArgs e)
	{
		tbNewPccesCode.Focus();
		tbNewPccesCode.SelectAll();
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		string AppLocation = AppDomain.CurrentDomain.BaseDirectory;
		if (tbNewPccesCode.Text.Trim() == originalPccesCode.Trim())
		{
			MessageBox.Show(this, "您沒有更改編碼，請重新確認。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbNewPccesCode.Focus();
		}
		else
		{
			if (!CheckPccesCodeValidity())
			{
				return;
			}
			if (!CommonMethods.IsStrByteLenValid(tbNewPccesCode.Text, 20))
			{
				MessageBox.Show(this, "新給定的碼的長度不可超過 20 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				tbNewPccesCode.Focus();
				return;
			}
			string sWhereProj = " and ProjectCode='" + projectCode + "' ";
			string ls_fn = "";
			string ls_fn2 = "";
			if (FormActionName == PccesFormAction.MrsBase)
			{
				ls_fn = "MrsBase";
				sWhereProj = "";
			}
			else if (FormActionName == PccesFormAction.BUD)
			{
				ls_fn = "budProjMrs";
				ls_fn2 = "budItem";
			}
			else if (FormActionName == PccesFormAction.BID)
			{
				ls_fn = "bidProjMrs";
				ls_fn2 = "bidItem";
			}
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = userID;
			DataTable DT_Exist = DBCLS.GetUserDefine("Select * From " + ls_fn + "A Where RTrim(PccesCode)='" + tbNewPccesCode.Text.Trim() + "' " + sWhereProj);
			DataTable DT_Old = DBCLS.GetUserDefine("Select * From " + ls_fn + "A Where pubCode=" + pubCode.ToString().Trim() + " " + sWhereProj);
			if (DT_Exist.Rows.Count > 0)
			{
				string sQuest = "已有相同代碼的工項存在，是否置換？\n" + DT_Exist.Rows[0]["PccesCode"].ToString().Trim() + "\n" + DT_Exist.Rows[0]["CName"].ToString().Trim() + "\n" + DT_Exist.Rows[0]["unitName"].ToString().Trim();
				if (MessageBox.Show(this, sQuest, originalPccesCode + ":" + workItemName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					string s1 = "Update " + ls_fn + "A Set cName='" + DT_Old.Rows[0]["cName"].ToString().Trim() + "',  pccesCode='" + DT_Exist.Rows[0]["pccesCode"].ToString() + "',  unitName ='" + DT_Old.Rows[0]["unitName"].ToString().Trim() + "',  Analysis ='" + DT_Old.Rows[0]["analysis"].ToString().Trim() + "',  costKind ='" + DT_Old.Rows[0]["costKind"].ToString().Trim() + "',  lRate    =" + DT_Old.Rows[0]["lRate"].ToString().Trim() + ",  eRate    =" + DT_Old.Rows[0]["eRate"].ToString().Trim() + ",  mRate    =" + DT_Old.Rows[0]["mRate"].ToString().Trim() + ",  wRate    =" + DT_Old.Rows[0]["wRate"].ToString().Trim() + ",  rate     =" + DT_Old.Rows[0]["rate"].ToString().Trim() + ",  cost     =" + DT_Old.Rows[0]["cost"].ToString().Trim() + ",  analysisQty  =" + DT_Old.Rows[0]["analysisQty"].ToString().Trim() + "  Where pubCode =" + DT_Exist.Rows[0]["pubCode"].ToString() + sWhereProj;
					DBCLS.ExecuteCommand(s1);
					string s2 = "Delete " + ls_fn + "B Where parentCode = " + DT_Exist.Rows[0]["pubCode"].ToString() + sWhereProj;
					DBCLS.ExecuteCommand(s2);
					s2 = "Update " + ls_fn + "B Set parentCode =" + DT_Exist.Rows[0]["pubCode"].ToString() + " Where parentCode = " + DT_Old.Rows[0]["pubCode"].ToString() + sWhereProj;
					DBCLS.ExecuteCommand(s2);
					s2 = "Update " + ls_fn + "B Set pubCode =" + DT_Exist.Rows[0]["pubCode"].ToString() + " Where pubCode = " + DT_Old.Rows[0]["pubCode"].ToString() + sWhereProj;
					DBCLS.ExecuteCommand(s2);
					string s3 = "Delete " + ls_fn + "C Where parentCode = " + DT_Exist.Rows[0]["pubCode"].ToString() + sWhereProj;
					DBCLS.ExecuteCommand(s3);
					s3 = "Update " + ls_fn + "C Set parentCode =" + DT_Exist.Rows[0]["pubCode"].ToString() + " Where parentCode = " + DT_Old.Rows[0]["pubCode"].ToString() + sWhereProj;
					DBCLS.ExecuteCommand(s3);
					s3 = "Update " + ls_fn + "C Set pubCode =" + DT_Exist.Rows[0]["pubCode"].ToString() + " Where pubCode = " + DT_Old.Rows[0]["pubCode"].ToString() + sWhereProj;
					DBCLS.ExecuteCommand(s3);
					string s6 = "Delete " + ls_fn + "A Where pubCode =" + DT_Old.Rows[0]["pubCode"].ToString() + sWhereProj;
					DBCLS.ExecuteCommand(s6);
					if (FormActionName != PccesFormAction.MrsBase)
					{
						DBCLS.ExecuteCommand("Update " + ls_fn2 + "A Set pccesCode ='" + tbNewPccesCode.Text.Trim() + "', pubCode ='" + DT_Exist.Rows[0]["pubCode"].ToString() + "' Where pubCode ='" + pubCode + "' " + sWhereProj);
					}
					if (FormActionName == PccesFormAction.MrsBase)
					{
						int iOld = (base.Owner as frmMrsBase).gridMrsBase1.FindRow(DT_Old.Rows[0]["pubCode"].ToString(), 1, (base.Owner as frmMrsBase).gridMrsBase1.Cols["PubCode"].SafeIndex, caseSensitive: true, fullMatch: true, wrap: false);
						int iExist = (base.Owner as frmMrsBase).gridMrsBase1.FindRow(DT_Exist.Rows[0]["pubCode"].ToString(), 1, (base.Owner as frmMrsBase).gridMrsBase1.Cols["PubCode"].SafeIndex, caseSensitive: true, fullMatch: true, wrap: false);
						if (iOld > -1 && iExist > -1)
						{
							(base.Owner as frmMrsBase).gridMrsBase1.RemoveItem(iOld);
							iExist = (base.Owner as frmMrsBase).gridMrsBase1.FindRow(DT_Exist.Rows[0]["pubCode"].ToString(), 1, (base.Owner as frmMrsBase).gridMrsBase1.Cols["PubCode"].SafeIndex, caseSensitive: true, fullMatch: true, wrap: false);
							(base.Owner as frmMrsBase).ReLoad_OneRow((int)DT_Exist.Rows[0]["pubCode"], iExist);
						}
					}
				}
				else
				{
					base.DialogResult = DialogResult.Cancel;
				}
			}
			else
			{
				DBCLS.ExecuteCommand("Update " + ls_fn + "A Set pccesCode ='" + tbNewPccesCode.Text.Trim() + "' Where pubCode =" + pubCode + sWhereProj);
				if (tbNewPccesCode.Text.Trim().Length > 0)
				{
					string sPccesCode = tbNewPccesCode.Text.Trim();
					if (sPccesCode.Substring(0, 1).ToUpper() == "L")
					{
						DBCLS.ExecuteCommand("Update " + ls_fn + "A Set lRate ='100',eRate ='0',mRate ='0',wRate ='0' Where pubCode =" + pubCode + sWhereProj);
					}
					else if (sPccesCode.Substring(0, 1).ToUpper() == "E")
					{
						DBCLS.ExecuteCommand("Update " + ls_fn + "A Set lRate ='0',eRate ='100',mRate ='0',wRate ='0' Where pubCode =" + pubCode + sWhereProj);
					}
					else if (sPccesCode.Substring(0, 1).ToUpper() == "M")
					{
						DBCLS.ExecuteCommand("Update " + ls_fn + "A Set lRate ='0',eRate ='0',mRate ='100',wRate ='0' Where pubCode =" + pubCode + sWhereProj);
					}
					else if (sPccesCode.Substring(0, 1).ToUpper() == "W")
					{
						DBCLS.ExecuteCommand("Update " + ls_fn + "A Set lRate ='0',eRate ='0',mRate ='0',wRate ='100' Where pubCode =" + pubCode + sWhereProj);
					}
				}
				if (FormActionName != PccesFormAction.MrsBase)
				{
					DBCLS.ExecuteCommand("Update " + ls_fn2 + "A Set pccesCode ='" + tbNewPccesCode.Text.Trim() + "' Where pccesCode ='" + originalPccesCode.ToString() + "' " + sWhereProj);
				}
				if (FormActionName == PccesFormAction.MrsBase)
				{
					int iiDexx = (base.Owner as frmMrsBase).gridMrsBase1.FindRow(pubCode.ToString(), 1, (base.Owner as frmMrsBase).gridMrsBase1.Cols["PubCode"].SafeIndex, caseSensitive: true, fullMatch: true, wrap: false);
					(base.Owner as frmMrsBase).ReLoad_OneRow(pubCode, iiDexx);
				}
			}
			base.DialogResult = DialogResult.OK;
		}
	}

	private bool CheckPccesCodeValidity()
	{
		string pccesCode = tbNewPccesCode.Text.Trim();
		AutoNum autoNum = new AutoNum();
		ExecResult ER = autoNum.CheckPccesCodeValidity(pccesCode);
		if (ER.ReturnCode != 0)
		{
			MessageBox.Show(this, ER.Message, "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbNewPccesCode.Focus();
			return false;
		}
		MrsBaseA mrsBaseA = new MrsBaseA();
		if (mrsBaseA.IsCommonItem(pccesCode))
		{
			MessageBox.Show(this, pccesCode + " 為共通性項目編碼，不得換為此工項代碼！", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			tbNewPccesCode.Focus();
			return false;
		}
		return true;
	}
}
