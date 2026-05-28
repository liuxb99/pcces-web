using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.Budget.ItemNoset;
using Archnowledge.Pcces.PccesMain.Library;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetItemNo : Form
{
	private string PID = string.Empty;

	private FormStatus FORM_STATUS = FormStatus.Iinitial;

	private PccesFormAction FormActionName;

	private string UserID;

	private string projectCode;

	private string printMode = string.Empty;

	private ItemNoSettingManager theItemNoSettingManager;

	private GroupBox groupBox2;

	private GroupBox groupBox5;

	private UltraButton A1_Btn_Cncl;

	private UltraButton A1_Btn_Next;

	private GroupBox groupBox3;

	private GroupBox groupBox1;

	private UltraComboEditor ddlLevel8;

	private UltraComboEditor ddlLevel7;

	private UltraComboEditor ddlLevel6;

	private UltraComboEditor ddlLevel5;

	private UltraComboEditor ddlLevel4;

	private UltraComboEditor ddlLevel3;

	private UltraComboEditor ddlLevel2;

	private UltraComboEditor ddlLevel1;

	private UltraLabel ultraLabel8;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel2;

	private UltraOptionSet opItemNoReorderType;

	private UltraButton btnCustomItemNoSet;

	private UltraLabel ultraLabel9;

	private UltraLabel ultraLabel10;

	private UltraLabel ultraLabel11;

	private UltraLabel ultraLabel12;

	private UltraLabel ultraLabel13;

	private UltraLabel ultraLabel14;

	private UltraLabel ultraLabel15;

	private UltraLabel ultraLabel19;

	private UltraLabel ultraLabel20;

	private UltraOptionSet opItemNoReorderRange;

	private UltraLabel lblSMP23;

	private UltraLabel lblSMP22;

	private UltraLabel lblSMP21;

	private UltraLabel lblSMP13;

	private UltraLabel lblSMP12;

	private UltraLabel lblSMP11;

	private UltraComboEditor ddlSymbol;

	private UltraCheckEditor ckCombinedWithSymbol;

	private Container components = null;

	public Panel panel9;

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

	public string _UserID
	{
		get
		{
			return UserID;
		}
		set
		{
			UserID = value;
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

	public FormBudgetItemNo()
	{
		InitializeComponent();
		PID = ConfigurationManager.AppSettings["PID"];
	}

	private void FormBudgetItemNo_Load(object sender, EventArgs e)
	{
		theItemNoSettingManager = new ItemNoSettingManager(_ProjectCode);
		theItemNoSettingManager.GetItemNoSetting(out var level1, out var level2, out var level3, out var level4, out var level5, out var level6, out var level7, out var level8, out var AssemType, out var Type, out var IsSymbol, out var Symbol);
		if (AssemType == "1")
		{
			opItemNoReorderType.CheckedIndex = 0;
		}
		else
		{
			opItemNoReorderType.CheckedIndex = 1;
		}
		if (Type.Trim().ToUpper() == "ALL")
		{
			opItemNoReorderRange.CheckedIndex = 0;
		}
		else if (Type.Trim().ToUpper() == "M")
		{
			opItemNoReorderRange.CheckedIndex = 1;
		}
		else if (Type.Trim().ToUpper() == "W")
		{
			opItemNoReorderRange.CheckedIndex = 2;
		}
		if (IsSymbol.ToUpper() == "Y")
		{
			ckCombinedWithSymbol.Checked = true;
		}
		else
		{
			ckCombinedWithSymbol.Checked = false;
		}
		ddlSymbol.Text = Symbol;
		LoadIntoCombo();
		if (level1 != "")
		{
			FindIndexOfCombo(ref ddlLevel1, level1.Trim());
		}
		if (level2 != "")
		{
			FindIndexOfCombo(ref ddlLevel2, level2.Trim());
		}
		if (level3 != "")
		{
			FindIndexOfCombo(ref ddlLevel3, level3.Trim());
		}
		if (level4 != "")
		{
			FindIndexOfCombo(ref ddlLevel4, level4.Trim());
		}
		if (level5 != "")
		{
			FindIndexOfCombo(ref ddlLevel5, level5.Trim());
		}
		if (level6 != "")
		{
			FindIndexOfCombo(ref ddlLevel6, level6.Trim());
		}
		if (level7 != "")
		{
			FindIndexOfCombo(ref ddlLevel7, level7.Trim());
		}
		if (level8 != "")
		{
			FindIndexOfCombo(ref ddlLevel8, level8.Trim());
		}
		opItemNoReorderType_ValueChanged(this, EventArgs.Empty);
		Do_AssembleSample();
		CorrectRatio();
		if (PID != null && PID.Trim() == "Z14AC1100")
		{
			opItemNoReorderType.CheckedIndex = 1;
			ckCombinedWithSymbol.Checked = false;
		}
		FORM_STATUS = FormStatus.Active;
	}

	private void CorrectRatio()
	{
		double ratio = CommonMethods.GetWindowRatio(base.Handle);
		if (ratio != 1.0)
		{
			opItemNoReorderRange.Font = new Font(opItemNoReorderRange.Font.Name, (float)((double)opItemNoReorderRange.Font.Size * ratio));
			opItemNoReorderType.Font = new Font(opItemNoReorderType.Font.Name, (float)((double)opItemNoReorderType.Font.Size * ratio));
			ddlLevel1.Font = new Font(ddlLevel1.Font.Name, (float)((double)ddlLevel1.Font.Size * ratio));
			ddlLevel2.Font = new Font(ddlLevel2.Font.Name, (float)((double)ddlLevel2.Font.Size * ratio));
			ddlLevel3.Font = new Font(ddlLevel3.Font.Name, (float)((double)ddlLevel3.Font.Size * ratio));
			ddlLevel4.Font = new Font(ddlLevel4.Font.Name, (float)((double)ddlLevel4.Font.Size * ratio));
			ddlLevel5.Font = new Font(ddlLevel5.Font.Name, (float)((double)ddlLevel5.Font.Size * ratio));
			ddlLevel6.Font = new Font(ddlLevel6.Font.Name, (float)((double)ddlLevel6.Font.Size * ratio));
			ddlLevel7.Font = new Font(ddlLevel7.Font.Name, (float)((double)ddlLevel7.Font.Size * ratio));
			ddlLevel8.Font = new Font(ddlLevel8.Font.Name, (float)((double)ddlLevel8.Font.Size * ratio));
			ckCombinedWithSymbol.Font = new Font(ckCombinedWithSymbol.Font.Name, (float)((double)ckCombinedWithSymbol.Font.Size * ratio));
			ddlSymbol.Font = new Font(ddlSymbol.Font.Name, (float)((double)ddlSymbol.Font.Size * ratio));
		}
	}

	private void FindIndexOfCombo(ref UltraComboEditor cboXX, string sKey)
	{
		for (int i = 0; i < cboXX.Items.Count; i++)
		{
			if (sKey == cboXX.Items[i].DataValue.ToString().Trim())
			{
				cboXX.SelectedIndex = i;
				break;
			}
		}
	}

	private void Do_AssembleSample()
	{
		if (FORM_STATUS == FormStatus.Normal)
		{
			if (ddlLevel1.SelectedIndex < 0)
			{
				lblSMP11.Text = "第一階: 第一階";
			}
			else
			{
				lblSMP11.Text = "第一階: " + ddlLevel1.Items[ddlLevel1.SelectedIndex].DisplayText;
			}
			if (ddlLevel2.SelectedIndex < 0)
			{
				lblSMP12.Text = "第二階: 第二階";
			}
			else
			{
				lblSMP12.Text = "第二階: " + ddlLevel2.Items[ddlLevel2.SelectedIndex].DisplayText;
			}
			if (ddlLevel3.SelectedIndex < 0)
			{
				lblSMP13.Text = "第三階: 第三階";
			}
			else
			{
				lblSMP13.Text = "第三階: " + ddlLevel3.Items[ddlLevel3.SelectedIndex].DisplayText;
			}
			if (ddlLevel1.SelectedIndex < 0)
			{
				lblSMP21.Text = "第一階: 第一階";
			}
			else
			{
				lblSMP21.Text = "第一階: " + ddlLevel1.Items[ddlLevel1.SelectedIndex].DisplayText;
			}
			if (ddlLevel2.SelectedIndex < 0)
			{
				lblSMP22.Text = "第二階: 第一階+第二階";
			}
			else
			{
				lblSMP22.Text = "第二階: " + Sample2();
			}
			if (ddlLevel3.SelectedIndex < 0)
			{
				lblSMP23.Text = "第三階: 第一階+第二階+第三階";
			}
			else
			{
				lblSMP23.Text = "第三階: " + Sample3();
			}
		}
	}

	private string Sample2()
	{
		string RetV = "";
		string symbol = (ckCombinedWithSymbol.Checked ? ddlSymbol.Text : "");
		if (ddlLevel1.SelectedIndex >= 0)
		{
			RetV = RetV + ((ddlLevel1.Items[ddlLevel1.SelectedIndex].DisplayText.Substring(0, 1) == ",") ? "" : ddlLevel1.Items[ddlLevel1.SelectedIndex].DisplayText.Substring(0, 1)) + symbol;
		}
		if (ddlLevel2.SelectedIndex >= 0)
		{
			RetV += ((ddlLevel2.Items[ddlLevel2.SelectedIndex].DisplayText.Substring(0, 1) == ",") ? "" : ddlLevel2.Items[ddlLevel2.SelectedIndex].DisplayText.Substring(0, 1));
		}
		RetV += ",";
		if (ddlLevel1.SelectedIndex >= 0)
		{
			RetV = RetV + ((ddlLevel1.Items[ddlLevel1.SelectedIndex].DisplayText.Substring(2, 1) == ",") ? "" : ddlLevel1.Items[ddlLevel1.SelectedIndex].DisplayText.Substring(2, 1)) + symbol;
		}
		if (ddlLevel2.SelectedIndex >= 0)
		{
			RetV += ((ddlLevel2.Items[ddlLevel2.SelectedIndex].DisplayText.Substring(2, 1) == ",") ? "" : ddlLevel2.Items[ddlLevel2.SelectedIndex].DisplayText.Substring(2, 1));
		}
		RetV += ",";
		if (ddlLevel1.SelectedIndex >= 0)
		{
			RetV = RetV + ((ddlLevel1.Items[ddlLevel1.SelectedIndex].DisplayText.Substring(4, 1) == ",") ? "" : ddlLevel1.Items[ddlLevel1.SelectedIndex].DisplayText.Substring(4, 1)) + symbol;
		}
		if (ddlLevel2.SelectedIndex >= 0)
		{
			RetV += ((ddlLevel2.Items[ddlLevel2.SelectedIndex].DisplayText.Substring(4, 1) == ",") ? "" : ddlLevel2.Items[ddlLevel2.SelectedIndex].DisplayText.Substring(4, 1));
		}
		RetV += ",";
		return RetV + "...";
	}

	private string Sample3()
	{
		string RetV = "";
		string sSymbol = (ckCombinedWithSymbol.Checked ? ddlSymbol.Text : "");
		if (ddlLevel1.SelectedIndex >= 0)
		{
			RetV = RetV + ((ddlLevel1.Items[ddlLevel1.SelectedIndex].DisplayText.Substring(0, 1) == ",") ? "" : ddlLevel1.Items[ddlLevel1.SelectedIndex].DisplayText.Substring(0, 1)) + sSymbol;
		}
		if (ddlLevel2.SelectedIndex >= 0)
		{
			RetV = RetV + ((ddlLevel2.Items[ddlLevel2.SelectedIndex].DisplayText.Substring(0, 1) == ",") ? "" : ddlLevel2.Items[ddlLevel2.SelectedIndex].DisplayText.Substring(0, 1)) + sSymbol;
		}
		if (ddlLevel3.SelectedIndex >= 0)
		{
			RetV += ((ddlLevel3.Items[ddlLevel3.SelectedIndex].DisplayText.Substring(0, 1) == ",") ? "" : ddlLevel3.Items[ddlLevel3.SelectedIndex].DisplayText.Substring(0, 1));
		}
		RetV += ",";
		if (ddlLevel1.SelectedIndex >= 0)
		{
			RetV = RetV + ((ddlLevel1.Items[ddlLevel1.SelectedIndex].DisplayText.Substring(2, 1) == ",") ? "" : ddlLevel1.Items[ddlLevel1.SelectedIndex].DisplayText.Substring(2, 1)) + sSymbol;
		}
		if (ddlLevel2.SelectedIndex >= 0)
		{
			RetV = RetV + ((ddlLevel2.Items[ddlLevel2.SelectedIndex].DisplayText.Substring(2, 1) == ",") ? "" : ddlLevel2.Items[ddlLevel2.SelectedIndex].DisplayText.Substring(2, 1)) + sSymbol;
		}
		if (ddlLevel3.SelectedIndex >= 0)
		{
			RetV += ((ddlLevel3.Items[ddlLevel3.SelectedIndex].DisplayText.Substring(2, 1) == ",") ? "" : ddlLevel3.Items[ddlLevel3.SelectedIndex].DisplayText.Substring(2, 1));
		}
		RetV += ",";
		if (ddlLevel1.SelectedIndex >= 0)
		{
			RetV = RetV + ((ddlLevel1.Items[ddlLevel1.SelectedIndex].DisplayText.Substring(4, 1) == ",") ? "" : ddlLevel1.Items[ddlLevel1.SelectedIndex].DisplayText.Substring(4, 1)) + sSymbol;
		}
		if (ddlLevel2.SelectedIndex >= 0)
		{
			RetV = RetV + ((ddlLevel2.Items[ddlLevel2.SelectedIndex].DisplayText.Substring(4, 1) == ",") ? "" : ddlLevel2.Items[ddlLevel2.SelectedIndex].DisplayText.Substring(4, 1)) + sSymbol;
		}
		if (ddlLevel3.SelectedIndex >= 0)
		{
			RetV += ((ddlLevel3.Items[ddlLevel3.SelectedIndex].DisplayText.Substring(4, 1) == ",") ? "" : ddlLevel3.Items[ddlLevel3.SelectedIndex].DisplayText.Substring(4, 1));
		}
		RetV += ",";
		return RetV + "...";
	}

	private void LoadIntoCombo()
	{
		ddlLevel1.Items.Clear();
		ddlLevel2.Items.Clear();
		ddlLevel3.Items.Clear();
		ddlLevel4.Items.Clear();
		ddlLevel5.Items.Clear();
		ddlLevel6.Items.Clear();
		ddlLevel7.Items.Clear();
		ddlLevel8.Items.Clear();
		DBClass DBCLS = new DBClass();
		DataTable DT_1 = DBCLS.GetItemNameForCombo();
		for (int i = 0; i < DT_1.Rows.Count; i++)
		{
			ddlLevel1.Items.Add(DT_1.Rows[i]["Kind"].ToString().Trim(), DT_1.Rows[i]["Sample"].ToString().Trim());
			ddlLevel2.Items.Add(DT_1.Rows[i]["Kind"].ToString().Trim(), DT_1.Rows[i]["Sample"].ToString().Trim());
			ddlLevel3.Items.Add(DT_1.Rows[i]["Kind"].ToString().Trim(), DT_1.Rows[i]["Sample"].ToString().Trim());
			ddlLevel4.Items.Add(DT_1.Rows[i]["Kind"].ToString().Trim(), DT_1.Rows[i]["Sample"].ToString().Trim());
			ddlLevel5.Items.Add(DT_1.Rows[i]["Kind"].ToString().Trim(), DT_1.Rows[i]["Sample"].ToString().Trim());
			ddlLevel6.Items.Add(DT_1.Rows[i]["Kind"].ToString().Trim(), DT_1.Rows[i]["Sample"].ToString().Trim());
			ddlLevel7.Items.Add(DT_1.Rows[i]["Kind"].ToString().Trim(), DT_1.Rows[i]["Sample"].ToString().Trim());
			ddlLevel8.Items.Add(DT_1.Rows[i]["Kind"].ToString().Trim(), DT_1.Rows[i]["Sample"].ToString().Trim());
		}
		DBCLS = null;
	}

	public void SaveSettings()
	{
		theItemNoSettingManager.SaveItemNoSetting(ddlLevel1.Value.ToString(), ddlLevel2.Value.ToString(), ddlLevel3.Value.ToString(), ddlLevel4.Value.ToString(), ddlLevel5.Value.ToString(), ddlLevel6.Value.ToString(), ddlLevel7.Value.ToString(), ddlLevel8.Value.ToString(), opItemNoReorderType.Value.ToString(), opItemNoReorderRange.Value.ToString(), ckCombinedWithSymbol.Checked ? "Y" : "N", ddlSymbol.Items[ddlSymbol.SelectedIndex].ToString());
	}

	private void A1_Btn_Next_Click(object sender, EventArgs e)
	{
		SaveSettings();
		base.DialogResult = DialogResult.OK;
		Close();
	}

	private void cbo01_SelectionChanged(object sender, EventArgs e)
	{
		ddlSymbol.Enabled = (ckCombinedWithSymbol.Checked ? true : false);
		Do_AssembleSample();
	}

	private void FormBudgetItemNo_Activated(object sender, EventArgs e)
	{
		if (FORM_STATUS == FormStatus.Active)
		{
			FORM_STATUS = FormStatus.Normal;
			Do_AssembleSample();
		}
	}

	private void opItemNoReorderType_ValueChanged(object sender, EventArgs e)
	{
		ckCombinedWithSymbol.Enabled = ((opItemNoReorderType.CheckedIndex != 0) ? true : false);
	}

	private void btnCustomItemNoSet_Click(object sender, EventArgs e)
	{
		FormBDGT_ItemSetMaintain FM_ITMSET_MNTN = new FormBDGT_ItemSetMaintain();
		FM_ITMSET_MNTN.ShowDialog(this);
		FM_ITMSET_MNTN.Dispose();
		FM_ITMSET_MNTN = null;
		FormBudgetItemNo_Load(this, EventArgs.Empty);
	}

	private void opItemNoReorderRange_ValueChanged(object sender, EventArgs e)
	{
		if (opItemNoReorderRange.CheckedIndex != 0)
		{
			opItemNoReorderType.CheckedIndex = 0;
			opItemNoReorderType.Enabled = false;
		}
		else
		{
			opItemNoReorderType.Enabled = true;
		}
	}

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetItemNo));
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
		Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem6 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem7 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem8 = new Infragistics.Win.ValueListItem();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.btnCustomItemNoSet = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ddlLevel8 = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ddlLevel7 = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ddlLevel6 = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ddlLevel5 = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ddlLevel4 = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ddlLevel3 = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ddlLevel2 = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ddlLevel1 = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.panel9 = new System.Windows.Forms.Panel();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.A1_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.A1_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.ultraLabel20 = new Infragistics.Win.Misc.UltraLabel();
		this.lblSMP23 = new Infragistics.Win.Misc.UltraLabel();
		this.lblSMP22 = new Infragistics.Win.Misc.UltraLabel();
		this.lblSMP21 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel19 = new Infragistics.Win.Misc.UltraLabel();
		this.lblSMP13 = new Infragistics.Win.Misc.UltraLabel();
		this.lblSMP12 = new Infragistics.Win.Misc.UltraLabel();
		this.lblSMP11 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel15 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.ddlSymbol = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ckCombinedWithSymbol = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.opItemNoReorderType = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.opItemNoReorderRange = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ddlLevel8).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ddlLevel7).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ddlLevel6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ddlLevel5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ddlLevel4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ddlLevel3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ddlLevel2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ddlLevel1).BeginInit();
		this.panel9.SuspendLayout();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ddlSymbol).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.opItemNoReorderType).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.opItemNoReorderRange).BeginInit();
		base.SuspendLayout();
		this.groupBox2.Controls.Add(this.btnCustomItemNoSet);
		this.groupBox2.Controls.Add(this.ultraLabel8);
		this.groupBox2.Controls.Add(this.ultraLabel7);
		this.groupBox2.Controls.Add(this.ultraLabel5);
		this.groupBox2.Controls.Add(this.ultraLabel4);
		this.groupBox2.Controls.Add(this.ultraLabel3);
		this.groupBox2.Controls.Add(this.ultraLabel1);
		this.groupBox2.Controls.Add(this.ultraLabel6);
		this.groupBox2.Controls.Add(this.ultraLabel2);
		this.groupBox2.Controls.Add(this.ddlLevel8);
		this.groupBox2.Controls.Add(this.ddlLevel7);
		this.groupBox2.Controls.Add(this.ddlLevel6);
		this.groupBox2.Controls.Add(this.ddlLevel5);
		this.groupBox2.Controls.Add(this.ddlLevel4);
		this.groupBox2.Controls.Add(this.ddlLevel3);
		this.groupBox2.Controls.Add(this.ddlLevel2);
		this.groupBox2.Controls.Add(this.ddlLevel1);
		this.groupBox2.ForeColor = System.Drawing.Color.Navy;
		this.groupBox2.Location = new System.Drawing.Point(8, 88);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(314, 392);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "主項大類項次編號";
		this.btnCustomItemNoSet.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnCustomItemNoSet.Appearance = appearance1;
		this.btnCustomItemNoSet.BackColor = System.Drawing.SystemColors.Control;
		this.btnCustomItemNoSet.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button;
		this.btnCustomItemNoSet.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnCustomItemNoSet.Location = new System.Drawing.Point(179, 330);
		this.btnCustomItemNoSet.Name = "btnCustomItemNoSet";
		this.btnCustomItemNoSet.Size = new System.Drawing.Size(116, 28);
		this.btnCustomItemNoSet.TabIndex = 48;
		this.btnCustomItemNoSet.Text = "自定項次編號...";
		this.btnCustomItemNoSet.Click += new System.EventHandler(btnCustomItemNoSet_Click);
		appearance2.ForeColor = System.Drawing.Color.Black;
		appearance2.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel8.Appearance = appearance2;
		this.ultraLabel8.Location = new System.Drawing.Point(12, 28);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(60, 23);
		this.ultraLabel8.TabIndex = 47;
		this.ultraLabel8.Text = "第一階:";
		appearance3.ForeColor = System.Drawing.Color.Black;
		appearance3.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel7.Appearance = appearance3;
		this.ultraLabel7.Location = new System.Drawing.Point(12, 61);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(60, 23);
		this.ultraLabel7.TabIndex = 46;
		this.ultraLabel7.Text = "第二階:";
		appearance4.ForeColor = System.Drawing.Color.Black;
		appearance4.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance4;
		this.ultraLabel5.Location = new System.Drawing.Point(12, 94);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(60, 23);
		this.ultraLabel5.TabIndex = 45;
		this.ultraLabel5.Text = "第三階:";
		appearance5.ForeColor = System.Drawing.Color.Black;
		appearance5.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance5.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance5;
		this.ultraLabel4.Location = new System.Drawing.Point(12, 127);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(60, 23);
		this.ultraLabel4.TabIndex = 44;
		this.ultraLabel4.Text = "第四階:";
		appearance6.ForeColor = System.Drawing.Color.Black;
		appearance6.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance6.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance6;
		this.ultraLabel3.Location = new System.Drawing.Point(12, 160);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(60, 23);
		this.ultraLabel3.TabIndex = 43;
		this.ultraLabel3.Text = "第五階:";
		appearance7.ForeColor = System.Drawing.Color.Black;
		appearance7.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance7;
		this.ultraLabel1.Location = new System.Drawing.Point(12, 193);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(60, 23);
		this.ultraLabel1.TabIndex = 42;
		this.ultraLabel1.Text = "第六階:";
		appearance8.ForeColor = System.Drawing.Color.Black;
		appearance8.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance8.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel6.Appearance = appearance8;
		this.ultraLabel6.Location = new System.Drawing.Point(12, 226);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(60, 23);
		this.ultraLabel6.TabIndex = 41;
		this.ultraLabel6.Text = "第七階:";
		appearance9.ForeColor = System.Drawing.Color.Black;
		appearance9.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance9.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance9;
		this.ultraLabel2.Location = new System.Drawing.Point(12, 259);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(60, 23);
		this.ultraLabel2.TabIndex = 40;
		this.ultraLabel2.Text = "第八階:";
		appearance10.FontData.Name = "細明體";
		appearance10.FontData.SizeInPoints = 11f;
		appearance10.ForeColor = System.Drawing.Color.Black;
		this.ddlLevel8.Appearance = appearance10;
		this.ddlLevel8.AutoSize = true;
		this.ddlLevel8.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		this.ddlLevel8.Location = new System.Drawing.Point(76, 259);
		this.ddlLevel8.Name = "ddlLevel8";
		this.ddlLevel8.Size = new System.Drawing.Size(220, 24);
		this.ddlLevel8.TabIndex = 39;
		this.ddlLevel8.Text = null;
		appearance11.FontData.Name = "細明體";
		appearance11.FontData.SizeInPoints = 11f;
		appearance11.ForeColor = System.Drawing.Color.Black;
		this.ddlLevel7.Appearance = appearance11;
		this.ddlLevel7.AutoSize = true;
		this.ddlLevel7.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		this.ddlLevel7.Location = new System.Drawing.Point(76, 226);
		this.ddlLevel7.Name = "ddlLevel7";
		this.ddlLevel7.Size = new System.Drawing.Size(220, 24);
		this.ddlLevel7.TabIndex = 38;
		this.ddlLevel7.Text = null;
		appearance12.FontData.Name = "細明體";
		appearance12.FontData.SizeInPoints = 11f;
		appearance12.ForeColor = System.Drawing.Color.Black;
		this.ddlLevel6.Appearance = appearance12;
		this.ddlLevel6.AutoSize = true;
		this.ddlLevel6.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		this.ddlLevel6.Location = new System.Drawing.Point(76, 193);
		this.ddlLevel6.Name = "ddlLevel6";
		this.ddlLevel6.Size = new System.Drawing.Size(220, 24);
		this.ddlLevel6.TabIndex = 37;
		this.ddlLevel6.Text = null;
		appearance13.FontData.Name = "細明體";
		appearance13.FontData.SizeInPoints = 11f;
		appearance13.ForeColor = System.Drawing.Color.Black;
		this.ddlLevel5.Appearance = appearance13;
		this.ddlLevel5.AutoSize = true;
		this.ddlLevel5.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		this.ddlLevel5.Location = new System.Drawing.Point(76, 160);
		this.ddlLevel5.Name = "ddlLevel5";
		this.ddlLevel5.Size = new System.Drawing.Size(220, 24);
		this.ddlLevel5.TabIndex = 36;
		this.ddlLevel5.Text = null;
		appearance14.FontData.Name = "細明體";
		appearance14.FontData.SizeInPoints = 11f;
		appearance14.ForeColor = System.Drawing.Color.Black;
		this.ddlLevel4.Appearance = appearance14;
		this.ddlLevel4.AutoSize = true;
		this.ddlLevel4.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		this.ddlLevel4.Location = new System.Drawing.Point(76, 127);
		this.ddlLevel4.Name = "ddlLevel4";
		this.ddlLevel4.Size = new System.Drawing.Size(220, 24);
		this.ddlLevel4.TabIndex = 35;
		this.ddlLevel4.Text = null;
		appearance15.FontData.Name = "細明體";
		appearance15.FontData.SizeInPoints = 11f;
		appearance15.ForeColor = System.Drawing.Color.Black;
		this.ddlLevel3.Appearance = appearance15;
		this.ddlLevel3.AutoSize = true;
		this.ddlLevel3.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		this.ddlLevel3.Location = new System.Drawing.Point(76, 94);
		this.ddlLevel3.Name = "ddlLevel3";
		this.ddlLevel3.Size = new System.Drawing.Size(220, 24);
		this.ddlLevel3.TabIndex = 34;
		this.ddlLevel3.Text = null;
		this.ddlLevel3.SelectionChanged += new System.EventHandler(cbo01_SelectionChanged);
		appearance16.FontData.Name = "細明體";
		appearance16.FontData.SizeInPoints = 11f;
		appearance16.ForeColor = System.Drawing.Color.Black;
		this.ddlLevel2.Appearance = appearance16;
		this.ddlLevel2.AutoSize = true;
		this.ddlLevel2.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		this.ddlLevel2.Location = new System.Drawing.Point(76, 61);
		this.ddlLevel2.Name = "ddlLevel2";
		this.ddlLevel2.Size = new System.Drawing.Size(220, 24);
		this.ddlLevel2.TabIndex = 33;
		this.ddlLevel2.Text = null;
		this.ddlLevel2.SelectionChanged += new System.EventHandler(cbo01_SelectionChanged);
		appearance17.FontData.Name = "細明體";
		appearance17.FontData.SizeInPoints = 11f;
		appearance17.ForeColor = System.Drawing.Color.Black;
		this.ddlLevel1.Appearance = appearance17;
		this.ddlLevel1.AutoSize = true;
		this.ddlLevel1.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		this.ddlLevel1.Location = new System.Drawing.Point(76, 28);
		this.ddlLevel1.Name = "ddlLevel1";
		this.ddlLevel1.Size = new System.Drawing.Size(220, 24);
		this.ddlLevel1.TabIndex = 32;
		this.ddlLevel1.Text = null;
		this.ddlLevel1.SelectionChanged += new System.EventHandler(cbo01_SelectionChanged);
		this.panel9.AutoSize = true;
		this.panel9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panel9.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel9.Controls.Add(this.groupBox5);
		this.panel9.Controls.Add(this.A1_Btn_Cncl);
		this.panel9.Controls.Add(this.A1_Btn_Next);
		this.panel9.Location = new System.Drawing.Point(0, 485);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(660, 43);
		this.panel9.TabIndex = 23;
		this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox5.Location = new System.Drawing.Point(0, 0);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(660, 8);
		this.groupBox5.TabIndex = 3;
		this.groupBox5.TabStop = false;
		this.A1_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance18.Image = resources.GetObject("appearance18.Image");
		appearance18.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A1_Btn_Cncl.Appearance = appearance18;
		this.A1_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A1_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A1_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A1_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A1_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A1_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A1_Btn_Cncl.Location = new System.Drawing.Point(564, 9);
		this.A1_Btn_Cncl.Name = "A1_Btn_Cncl";
		this.A1_Btn_Cncl.ShowFocusRect = false;
		this.A1_Btn_Cncl.ShowOutline = false;
		this.A1_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A1_Btn_Cncl.SupportThemes = false;
		this.A1_Btn_Cncl.TabIndex = 2;
		this.A1_Btn_Cncl.Text = "取消";
		this.A1_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance19.Image = resources.GetObject("appearance19.Image");
		appearance19.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A1_Btn_Next.Appearance = appearance19;
		this.A1_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.A1_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A1_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.A1_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.A1_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.A1_Btn_Next.Location = new System.Drawing.Point(472, 9);
		this.A1_Btn_Next.Name = "A1_Btn_Next";
		this.A1_Btn_Next.ShowFocusRect = false;
		this.A1_Btn_Next.ShowOutline = false;
		this.A1_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.A1_Btn_Next.SupportThemes = false;
		this.A1_Btn_Next.TabIndex = 1;
		this.A1_Btn_Next.Text = "確定";
		this.A1_Btn_Next.Click += new System.EventHandler(A1_Btn_Next_Click);
		this.groupBox3.Controls.Add(this.ultraLabel20);
		this.groupBox3.Controls.Add(this.lblSMP23);
		this.groupBox3.Controls.Add(this.lblSMP22);
		this.groupBox3.Controls.Add(this.lblSMP21);
		this.groupBox3.Controls.Add(this.ultraLabel19);
		this.groupBox3.Controls.Add(this.lblSMP13);
		this.groupBox3.Controls.Add(this.lblSMP12);
		this.groupBox3.Controls.Add(this.lblSMP11);
		this.groupBox3.Controls.Add(this.ultraLabel13);
		this.groupBox3.Controls.Add(this.ultraLabel14);
		this.groupBox3.Controls.Add(this.ultraLabel15);
		this.groupBox3.Controls.Add(this.ultraLabel12);
		this.groupBox3.Controls.Add(this.ultraLabel11);
		this.groupBox3.Controls.Add(this.ultraLabel10);
		this.groupBox3.Controls.Add(this.ultraLabel9);
		this.groupBox3.Controls.Add(this.ddlSymbol);
		this.groupBox3.Controls.Add(this.ckCombinedWithSymbol);
		this.groupBox3.Controls.Add(this.opItemNoReorderType);
		this.groupBox3.ForeColor = System.Drawing.Color.Navy;
		this.groupBox3.Location = new System.Drawing.Point(336, 88);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(314, 392);
		this.groupBox3.TabIndex = 24;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "項次編號方式";
		appearance20.ForeColor = System.Drawing.Color.Black;
		appearance20.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance20.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel20.Appearance = appearance20;
		this.ultraLabel20.Location = new System.Drawing.Point(8, 364);
		this.ultraLabel20.Name = "ultraLabel20";
		this.ultraLabel20.Size = new System.Drawing.Size(278, 23);
		this.ultraLabel20.TabIndex = 62;
		this.ultraLabel20.Text = "…以此類推";
		appearance21.ForeColor = System.Drawing.Color.Black;
		appearance21.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance21.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblSMP23.Appearance = appearance21;
		this.lblSMP23.Location = new System.Drawing.Point(8, 340);
		this.lblSMP23.Name = "lblSMP23";
		this.lblSMP23.Size = new System.Drawing.Size(300, 23);
		this.lblSMP23.TabIndex = 61;
		this.lblSMP23.Text = "第三階:";
		appearance22.ForeColor = System.Drawing.Color.Black;
		appearance22.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance22.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblSMP22.Appearance = appearance22;
		this.lblSMP22.Location = new System.Drawing.Point(8, 316);
		this.lblSMP22.Name = "lblSMP22";
		this.lblSMP22.Size = new System.Drawing.Size(296, 23);
		this.lblSMP22.TabIndex = 60;
		this.lblSMP22.Text = "第二階:";
		appearance23.ForeColor = System.Drawing.Color.Black;
		appearance23.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance23.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblSMP21.Appearance = appearance23;
		this.lblSMP21.Location = new System.Drawing.Point(8, 292);
		this.lblSMP21.Name = "lblSMP21";
		this.lblSMP21.Size = new System.Drawing.Size(300, 23);
		this.lblSMP21.TabIndex = 59;
		this.lblSMP21.Text = "第一階:";
		appearance24.ForeColor = System.Drawing.Color.Black;
		appearance24.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance24.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel19.Appearance = appearance24;
		this.ultraLabel19.Location = new System.Drawing.Point(10, 207);
		this.ultraLabel19.Name = "ultraLabel19";
		this.ultraLabel19.Size = new System.Drawing.Size(278, 23);
		this.ultraLabel19.TabIndex = 58;
		this.ultraLabel19.Text = "…以此類推";
		appearance25.ForeColor = System.Drawing.Color.Black;
		appearance25.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance25.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblSMP13.Appearance = appearance25;
		this.lblSMP13.Location = new System.Drawing.Point(10, 184);
		this.lblSMP13.Name = "lblSMP13";
		this.lblSMP13.Size = new System.Drawing.Size(282, 23);
		this.lblSMP13.TabIndex = 57;
		this.lblSMP13.Text = "第三階:";
		appearance26.ForeColor = System.Drawing.Color.Black;
		appearance26.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance26.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblSMP12.Appearance = appearance26;
		this.lblSMP12.Location = new System.Drawing.Point(10, 161);
		this.lblSMP12.Name = "lblSMP12";
		this.lblSMP12.Size = new System.Drawing.Size(282, 23);
		this.lblSMP12.TabIndex = 56;
		this.lblSMP12.Text = "第二階:";
		appearance27.ForeColor = System.Drawing.Color.Black;
		appearance27.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance27.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblSMP11.Appearance = appearance27;
		this.lblSMP11.Location = new System.Drawing.Point(10, 138);
		this.lblSMP11.Name = "lblSMP11";
		this.lblSMP11.Size = new System.Drawing.Size(282, 23);
		this.lblSMP11.TabIndex = 55;
		this.lblSMP11.Text = "第一階:";
		appearance28.ForeColor = System.Drawing.Color.Black;
		appearance28.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance28.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel13.Appearance = appearance28;
		this.ultraLabel13.Location = new System.Drawing.Point(8, 272);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(296, 20);
		this.ultraLabel13.TabIndex = 54;
		this.ultraLabel13.Text = "以累加方式來做項次編號，編號方式如下:";
		appearance29.ForeColor = System.Drawing.Color.Black;
		appearance29.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance29.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel14.Appearance = appearance29;
		this.ultraLabel14.Location = new System.Drawing.Point(85, 252);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(227, 23);
		this.ultraLabel14.TabIndex = 53;
		this.ultraLabel14.Text = "則是依據您所設定的各階編號，";
		appearance30.ForeColor = System.Drawing.Color.Red;
		appearance30.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance30.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel15.Appearance = appearance30;
		this.ultraLabel15.Location = new System.Drawing.Point(7, 252);
		this.ultraLabel15.Name = "ultraLabel15";
		this.ultraLabel15.Size = new System.Drawing.Size(84, 23);
		this.ultraLabel15.TabIndex = 52;
		this.ultraLabel15.Text = "\"組合編號\"";
		appearance31.ForeColor = System.Drawing.Color.Black;
		appearance31.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance31.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel12.Appearance = appearance31;
		this.ultraLabel12.Location = new System.Drawing.Point(10, 116);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(283, 23);
		this.ultraLabel12.TabIndex = 51;
		this.ultraLabel12.Text = "號來做項次編號，編號方式如下:";
		appearance32.ForeColor = System.Drawing.Color.Black;
		appearance32.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance32.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel11.Appearance = appearance32;
		this.ultraLabel11.Location = new System.Drawing.Point(87, 96);
		this.ultraLabel11.Name = "ultraLabel11";
		this.ultraLabel11.Size = new System.Drawing.Size(209, 23);
		this.ultraLabel11.TabIndex = 50;
		this.ultraLabel11.Text = "是直接依照您所設定的各階編";
		appearance33.ForeColor = System.Drawing.Color.Red;
		appearance33.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance33.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel10.Appearance = appearance33;
		this.ultraLabel10.Location = new System.Drawing.Point(9, 96);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(84, 23);
		this.ultraLabel10.TabIndex = 49;
		this.ultraLabel10.Text = "\"獨立編號\"";
		appearance34.ForeColor = System.Drawing.Color.Black;
		appearance34.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance34.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel9.Appearance = appearance34;
		this.ultraLabel9.Location = new System.Drawing.Point(9, 76);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(60, 23);
		this.ultraLabel9.TabIndex = 48;
		this.ultraLabel9.Text = "說明:";
		appearance35.ForeColor = System.Drawing.Color.Black;
		this.ddlSymbol.Appearance = appearance35;
		this.ddlSymbol.AutoSize = true;
		this.ddlSymbol.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		valueListItem1.DataValue = ".";
		valueListItem1.DisplayText = ".";
		valueListItem2.DataValue = "-";
		valueListItem2.DisplayText = "-";
		valueListItem3.DataValue = ",";
		valueListItem3.DisplayText = ",";
		this.ddlSymbol.Items.Add(valueListItem1);
		this.ddlSymbol.Items.Add(valueListItem2);
		this.ddlSymbol.Items.Add(valueListItem3);
		this.ddlSymbol.Location = new System.Drawing.Point(221, 44);
		this.ddlSymbol.Name = "ddlSymbol";
		this.ddlSymbol.Size = new System.Drawing.Size(77, 21);
		this.ddlSymbol.TabIndex = 33;
		this.ddlSymbol.Text = null;
		this.ddlSymbol.SelectionChanged += new System.EventHandler(cbo01_SelectionChanged);
		this.ckCombinedWithSymbol.Location = new System.Drawing.Point(100, 47);
		this.ckCombinedWithSymbol.Name = "ckCombinedWithSymbol";
		this.ckCombinedWithSymbol.Size = new System.Drawing.Size(128, 20);
		this.ckCombinedWithSymbol.TabIndex = 1;
		this.ckCombinedWithSymbol.Text = "加入組合符號:";
		this.ckCombinedWithSymbol.CheckedChanged += new System.EventHandler(cbo01_SelectionChanged);
		this.opItemNoReorderType.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.opItemNoReorderType.CheckedIndex = 0;
		this.opItemNoReorderType.ItemAppearance = appearance36;
		valueListItem4.DataValue = "1";
		valueListItem4.DisplayText = "獨立編號";
		valueListItem5.DataValue = "2";
		valueListItem5.DisplayText = "組合編號";
		this.opItemNoReorderType.Items.Add(valueListItem4);
		this.opItemNoReorderType.Items.Add(valueListItem5);
		this.opItemNoReorderType.Location = new System.Drawing.Point(10, 28);
		this.opItemNoReorderType.Name = "opItemNoReorderType";
		this.opItemNoReorderType.Size = new System.Drawing.Size(82, 44);
		this.opItemNoReorderType.TabIndex = 0;
		this.opItemNoReorderType.Text = "獨立編號";
		this.opItemNoReorderType.ValueChanged += new System.EventHandler(opItemNoReorderType_ValueChanged);
		this.groupBox1.Controls.Add(this.opItemNoReorderRange);
		this.groupBox1.ForeColor = System.Drawing.Color.Navy;
		this.groupBox1.Location = new System.Drawing.Point(8, 8);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(642, 72);
		this.groupBox1.TabIndex = 25;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "項次重整範圍";
		this.opItemNoReorderRange.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.opItemNoReorderRange.CheckedIndex = 0;
		this.opItemNoReorderRange.ItemAppearance = appearance37;
		valueListItem6.DataValue = "ALL";
		valueListItem6.DisplayText = "全部排序(主項+工項)";
		valueListItem7.DataValue = "M";
		valueListItem7.DisplayText = "僅排序主類大項";
		valueListItem8.DataValue = "W";
		valueListItem8.DisplayText = "僅排序工項";
		this.opItemNoReorderRange.Items.Add(valueListItem6);
		this.opItemNoReorderRange.Items.Add(valueListItem7);
		this.opItemNoReorderRange.Items.Add(valueListItem8);
		this.opItemNoReorderRange.ItemSpacingHorizontal = 40;
		this.opItemNoReorderRange.Location = new System.Drawing.Point(24, 32);
		this.opItemNoReorderRange.Name = "opItemNoReorderRange";
		this.opItemNoReorderRange.Size = new System.Drawing.Size(600, 24);
		this.opItemNoReorderRange.TabIndex = 1;
		this.opItemNoReorderRange.Text = "全部排序(主項+工項)";
		this.opItemNoReorderRange.ValueChanged += new System.EventHandler(opItemNoReorderRange_ValueChanged);
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.A1_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(660, 529);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.panel9);
		base.Controls.Add(this.groupBox2);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormBudgetItemNo";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "項次編號";
		base.Load += new System.EventHandler(FormBudgetItemNo_Load);
		base.Activated += new System.EventHandler(FormBudgetItemNo_Activated);
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ddlLevel8).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ddlLevel7).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ddlLevel6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ddlLevel5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ddlLevel4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ddlLevel3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ddlLevel2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ddlLevel1).EndInit();
		this.panel9.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ddlSymbol).EndInit();
		((System.ComponentModel.ISupportInitialize)this.opItemNoReorderType).EndInit();
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.opItemNoReorderRange).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
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
