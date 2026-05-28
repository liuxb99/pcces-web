using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.Budget;
using Archnowledge.Pcces.PccesMain.BudgetChange;
using Archnowledge.Pcces.PccesMain.Compare;
using Archnowledge.Pcces.PccesMain.Invoice;
using Archnowledge.Pcces.PccesMain.Library;
using Archnowledge.Pcces.PccesMain.MrsBase;
using Archnowledge.Pcces.PccesMain.Project;
using Archnowledge.Pcces.PccesMain.SplitContract;
using Archnowledge.Pcces.PccesMain.SubClose;
using Archnowledge.Pcces.PccesMain.SubFinal;
using Archnowledge.Pcces.PccesMain.SysMaintain;
using Archnowledge.Pcces.PccesMain.SysPlugin;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.ArchControls;

public class FunctionButtons : UserControl
{
	private FunctionOpenMode F_CurrOpenMode = FunctionOpenMode.Budget;

	private string F_ActiveFunction = "";

	private string F_UserID;

	private string F_UserName = "";

	private string F_ServerName = "localhost";

	public bool optionTabSelected = false;

	private LeftPanelStatus FuncsBtnStaus = LeftPanelStatus.None;

	private IContainer components = null;

	private ImageList imageList1;

	private ImageList imgButtons;

	public UltraButton BtnMain4;

	public UltraButton BtnFunc2;

	public UltraButton BtnFunc8;

	public UltraButton BtnFunc7;

	public UltraButton BtnMain2;

	public UltraButton BtnFunc9;

	public UltraButton BtnFunc10;

	public UltraButton BtnFunc6;

	public UltraButton BtnFunc11;

	public UltraButton BtnFunc12;

	public UltraButton BtnFunc13;

	public UltraButton BtnFunc1;

	private Panel pnModuleFlow;

	private LinkLabel linkModuleFlowMap;

	private UltraPictureBox ultraPictureBox1;

	private Panel panel1;

	private LinkLabel linkLabel1;

	private UltraPictureBox PicBox;

	public UltraButton BtnMain1;

	public UltraButton BtnFunc5;

	public UltraButton BtnFunc3;

	public UltraButton BtnMain3;

	public UltraButton BtnFuncBidImport;

	public UltraButton BtnFunc4;

	public FunctionOpenMode _CurrOpenMode
	{
		get
		{
			return F_CurrOpenMode;
		}
		set
		{
			F_CurrOpenMode = value;
			OPEN_MODE_CHECK();
		}
	}

	public string _ActiveFunction
	{
		get
		{
			return F_ActiveFunction;
		}
		set
		{
			F_ActiveFunction = value;
			SetActiveFunction();
		}
	}

	public LeftPanelStatus ButtonOwner
	{
		set
		{
			FuncsBtnStaus = value;
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

	public string _UserName
	{
		get
		{
			return F_UserName;
		}
		set
		{
			F_UserName = value;
		}
	}

	public string _ServerName
	{
		get
		{
			return F_ServerName;
		}
		set
		{
			F_ServerName = value;
		}
	}

	public FunctionButtons()
	{
		InitializeComponent();
	}

	private void FunctionButtons_Load(object sender, EventArgs e)
	{
		CorrectRatio();
	}

	private void linkModuleFlowMap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		FormModuleFlowMap FM_ModuleFlowMap = new FormModuleFlowMap();
		if (FM_ModuleFlowMap.ShowDialog() == DialogResult.OK)
		{
			switch (FM_ModuleFlowMap.PressedButtonID)
			{
			case ModuleFlowMapButtonID.CreateBidFile:
				DoCreateBudgetBidFile(isBudget: false);
				break;
			case ModuleFlowMapButtonID.CreateBudgetFile:
				DoCreateBudgetBidFile(isBudget: true);
				break;
			case ModuleFlowMapButtonID.CreateProject:
				DoProjectCreateImport(isCreate: true);
				break;
			case ModuleFlowMapButtonID.ProjectImport:
				DoProjectCreateImport(isCreate: false);
				break;
			case ModuleFlowMapButtonID.BidImport:
				BtnFuncBidImport_Click(null, null);
				break;
			case ModuleFlowMapButtonID.EditBid:
				BtnFunc4_Click(null, null);
				break;
			case ModuleFlowMapButtonID.EditBudget:
				BtnFunc5_Click(null, null);
				break;
			case ModuleFlowMapButtonID.EditContract:
				BtnFunc9_Click(null, null);
				break;
			case ModuleFlowMapButtonID.EstimateEvaluate:
				BtnFunc10_Click(null, null);
				break;
			case ModuleFlowMapButtonID.MarBaseCreate:
				BtnFunc2_Click(null, null);
				break;
			}
		}
		FM_ModuleFlowMap.Dispose();
		FM_ModuleFlowMap = null;
	}

	private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		if (F_UserID != "PccesUser" && !DBClass.ChkAuthority(F_UserID, "F0010007"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F0010007") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		GC.Collect();
		if (!(base.ParentForm is frmPccesMain))
		{
			base.ParentForm.Enabled = false;
		}
		Cursor = Cursors.WaitCursor;
		HideAllChild();
		bool IsFormExist = false;
		if (base.ParentForm is frmPccesMain)
		{
			Form[] mdiChildren = base.ParentForm.MdiChildren;
			foreach (Form frm in mdiChildren)
			{
				if (frm is frmSysMaintain)
				{
					IsFormExist = true;
					(base.ParentForm as frmPccesMain).LeftPanel.Width = 0;
					frm.Show();
					(frm as frmSysMaintain).Tab_G.Tab.Selected = true;
					break;
				}
				if (!(frm is FormPanel) && !(frm is FormPanel2) && !(frm is FormPanel3))
				{
					frm.Close();
					frm.Dispose();
				}
			}
			if (!IsFormExist)
			{
				frmSysMaintain FM_SysMaintain = new frmSysMaintain();
				FM_SysMaintain._UserID = (base.ParentForm as frmPccesMain)._UserID;
				FM_SysMaintain._UserName = (base.ParentForm as frmPccesMain)._UserName;
				FM_SysMaintain._ServerName = (base.ParentForm as frmPccesMain)._ServerName;
				FM_SysMaintain._HasRegistered = HasRegistered();
				FM_SysMaintain.MdiParent = base.ParentForm;
				FM_SysMaintain.Show();
				(base.ParentForm as frmPccesMain).LeftPanel.Width = 0;
				FM_SysMaintain.Tab_G.Tab.Selected = true;
			}
		}
		else if (base.ParentForm is frmSysMaintain)
		{
			base.ParentForm.Enabled = true;
			base.ParentForm.Show();
			base.ParentForm.BringToFront();
		}
		else
		{
			Form[] mdiChildren = base.ParentForm.ParentForm.MdiChildren;
			foreach (Form frm in mdiChildren)
			{
				if (frm is frmSysMaintain)
				{
					IsFormExist = true;
					frm.Show();
					base.ParentForm.Close();
					(frm as frmSysMaintain).Tab_G.Tab.Selected = true;
					break;
				}
			}
			if (!IsFormExist)
			{
				frmSysMaintain FM_SysMaintain = new frmSysMaintain();
				FM_SysMaintain._UserID = (base.ParentForm.ParentForm as frmPccesMain)._UserID;
				FM_SysMaintain._UserName = (base.ParentForm.ParentForm as frmPccesMain)._UserName;
				FM_SysMaintain._ServerName = (base.ParentForm.ParentForm as frmPccesMain)._ServerName;
				FM_SysMaintain._HasRegistered = HasRegistered();
				FM_SysMaintain.MdiParent = base.ParentForm.ParentForm;
				(base.ParentForm.ParentForm as frmPccesMain).LeftPanel.Width = 0;
				Thread.Sleep(500);
				base.ParentForm.Close();
				FM_SysMaintain.Show();
				FM_SysMaintain.Tab_G.Tab.Selected = true;
			}
		}
		Cursor = Cursors.Default;
	}

	private void BtnMain1_Click(object sender, EventArgs e)
	{
		if (F_CurrOpenMode != FunctionOpenMode.Budget)
		{
			HideAllButton();
			BtnFunc5.Visible = true;
			ProcessMessage();
			BtnFunc3.Visible = true;
			ProcessMessage();
			F_CurrOpenMode = FunctionOpenMode.Budget;
		}
	}

	public void BtnFunc5_Click(object sender, EventArgs e)
	{
		CreateFormProject();
	}

	public void BtnFunc3_Click(object sender, EventArgs e)
	{
		CreateFormBudgetByBUD();
	}

	private void BtnMain3_Click(object sender, EventArgs e)
	{
		if (F_CurrOpenMode != FunctionOpenMode.Bid)
		{
			HideAllButton();
			BtnFuncBidImport.Visible = true;
			ProcessMessage();
			BtnFunc4.Visible = true;
			ProcessMessage();
			F_CurrOpenMode = FunctionOpenMode.Bid;
		}
	}

	private void BtnFuncBidImport_Click(object sender, EventArgs e)
	{
		if (!DBClass.ChkAuthority(F_UserID, "F00500010002"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F00500010002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		FormProject FM_PROJ = CreateFormProject();
		formNewProjectWizard FM_NEW_PROJ_WZD = new formNewProjectWizard();
		FM_NEW_PROJ_WZD._UserID = F_UserID;
		FM_NEW_PROJ_WZD._IniMode = "2";
		FM_NEW_PROJ_WZD._IsAddOn = "BID";
		FM_NEW_PROJ_WZD.ShowDialog(FM_PROJ);
		FM_NEW_PROJ_WZD.Dispose();
		FM_NEW_PROJ_WZD = null;
		GC.Collect();
		FM_PROJ.GetNewData();
		FM_PROJ.BindDataToGrid();
		FM_PROJ.LocateToSpecificRow();
	}

	public void BtnFunc4_Click(object sender, EventArgs e)
	{
		CreateFormBudgetByBID();
	}

	public void BtnMain4_Click(object sender, EventArgs e)
	{
		if (F_CurrOpenMode != FunctionOpenMode.Common)
		{
			HideAllButton();
			BtnFunc2.Visible = true;
			ProcessMessage();
			BtnFunc8.Visible = true;
			ProcessMessage();
			BtnFunc7.Visible = true;
			ProcessMessage();
			F_CurrOpenMode = FunctionOpenMode.Common;
		}
	}

	public void BtnFunc2_Click(object sender, EventArgs e)
	{
		if (!IsCanSwitchForm())
		{
			return;
		}
		F_CurrOpenMode = FunctionOpenMode.Common;
		lock (this)
		{
			if (!DBClass.ChkAuthority(F_UserID, "F002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			GC.Collect();
			if (!(base.ParentForm is frmPccesMain))
			{
				base.ParentForm.Enabled = false;
			}
			base.Enabled = false;
			FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
			FM_INFO._InfoString = "基本資料庫維護載入中，請稍候! ";
			FM_INFO.Show();
			Application.DoEvents();
			Cursor = Cursors.WaitCursor;
			HideAllChild();
			bool IsFormExist = false;
			if (base.ParentForm is frmPccesMain)
			{
				Form[] mdiChildren = base.ParentForm.MdiChildren;
				foreach (Form frm in mdiChildren)
				{
					if (frm is frmMrsBase)
					{
						IsFormExist = true;
						(frm as frmMrsBase)._UserID = (base.ParentForm as frmPccesMain)._UserID;
						(frm as frmMrsBase)._UserName = (base.ParentForm as frmPccesMain)._UserName;
						(frm as frmMrsBase)._ServerName = (base.ParentForm as frmPccesMain)._ServerName;
						(frm as frmMrsBase)._HasRegistered = HasRegistered();
						frm.Show();
						break;
					}
					if (!(frm is FormPanel) && !(frm is FormPanel2) && !(frm is FormPanel3))
					{
						frm.Close();
						frm.Dispose();
					}
				}
				if (!IsFormExist)
				{
					frmMrsBase FM_MRS = new frmMrsBase();
					FM_MRS._UserID = (base.ParentForm as frmPccesMain)._UserID;
					FM_MRS._UserName = (base.ParentForm as frmPccesMain)._UserName;
					FM_MRS._ServerName = (base.ParentForm as frmPccesMain)._ServerName;
					FM_MRS._HasRegistered = HasRegistered();
					FM_MRS.MdiParent = base.ParentForm;
					FM_MRS.Show();
					(base.ParentForm as frmPccesMain).LeftPanel.Width = 0;
				}
			}
			else if (base.ParentForm is frmMrsBase)
			{
				base.ParentForm.Enabled = true;
				base.ParentForm.Show();
				base.ParentForm.BringToFront();
			}
			else
			{
				Form[] mdiChildren = base.ParentForm.ParentForm.MdiChildren;
				foreach (Form frm in mdiChildren)
				{
					if (frm is frmMrsBase)
					{
						IsFormExist = true;
						(frm as frmMrsBase)._UserID = (base.ParentForm.ParentForm as frmPccesMain)._UserID;
						(frm as frmMrsBase)._UserName = (base.ParentForm.ParentForm as frmPccesMain)._UserName;
						(frm as frmMrsBase)._ServerName = (base.ParentForm.ParentForm as frmPccesMain)._ServerName;
						(frm as frmMrsBase)._HasRegistered = HasRegistered();
						frm.Show();
						base.ParentForm.Close();
						break;
					}
				}
				if (!IsFormExist)
				{
					frmMrsBase FM_MRS = new frmMrsBase();
					FM_MRS._UserID = (base.ParentForm.ParentForm as frmPccesMain)._UserID;
					FM_MRS._UserName = (base.ParentForm.ParentForm as frmPccesMain)._UserName;
					FM_MRS._ServerName = (base.ParentForm.ParentForm as frmPccesMain)._ServerName;
					FM_MRS._HasRegistered = HasRegistered();
					FM_MRS.MdiParent = base.ParentForm.ParentForm;
					FM_MRS.Show();
					(base.ParentForm.ParentForm as frmPccesMain).LeftPanel.Width = 0;
					base.ParentForm.Dispose();
				}
			}
			Cursor = Cursors.Default;
			base.Enabled = true;
			FM_INFO.Close();
			FM_INFO.Dispose();
		}
	}

	public void BtnFunc8_Click(object sender, EventArgs e)
	{
		if (!IsCanSwitchForm())
		{
			return;
		}
		F_CurrOpenMode = FunctionOpenMode.Common;
		if (!DBClass.ChkAuthority(F_UserID, "F008"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F008") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		GC.Collect();
		if (!(base.ParentForm is frmPccesMain))
		{
			base.ParentForm.Enabled = false;
		}
		Cursor = Cursors.WaitCursor;
		HideAllChild();
		bool IsFormExist = false;
		if (base.ParentForm is frmPccesMain)
		{
			Form[] mdiChildren = base.ParentForm.MdiChildren;
			foreach (Form frm in mdiChildren)
			{
				if (frm is FormCompareItm)
				{
					IsFormExist = true;
					frm.Show();
					(base.ParentForm as frmPccesMain).LeftPanel.Width = 0;
					break;
				}
				if (!(frm is FormPanel) && !(frm is FormPanel2) && !(frm is FormPanel3))
				{
					frm.Close();
					frm.Dispose();
				}
			}
			if (!IsFormExist)
			{
				FormCompareItm FM_CMP_ITM = new FormCompareItm();
				FM_CMP_ITM._UserID = (base.ParentForm as frmPccesMain)._UserID;
				FM_CMP_ITM._UserName = (base.ParentForm as frmPccesMain)._UserName;
				FM_CMP_ITM._ServerName = (base.ParentForm as frmPccesMain)._ServerName;
				FM_CMP_ITM._HasRegistered = HasRegistered();
				FM_CMP_ITM.MdiParent = base.ParentForm;
				FM_CMP_ITM.Show();
				(base.ParentForm as frmPccesMain).LeftPanel.Width = 0;
			}
		}
		else if (base.ParentForm is FormCompareItm)
		{
			base.ParentForm.Enabled = true;
			base.ParentForm.Show();
			base.ParentForm.BringToFront();
		}
		else
		{
			Form[] mdiChildren = base.ParentForm.ParentForm.MdiChildren;
			foreach (Form frm in mdiChildren)
			{
				if (frm is FormCompareItm)
				{
					IsFormExist = true;
					frm.Show();
					base.ParentForm.Close();
					break;
				}
			}
			if (!IsFormExist)
			{
				FormCompareItm FM_CMP_ITM = new FormCompareItm();
				FM_CMP_ITM._UserID = (base.ParentForm.ParentForm as frmPccesMain)._UserID;
				FM_CMP_ITM._UserName = (base.ParentForm.ParentForm as frmPccesMain)._UserName;
				FM_CMP_ITM._ServerName = (base.ParentForm.ParentForm as frmPccesMain)._ServerName;
				FM_CMP_ITM._HasRegistered = HasRegistered();
				FM_CMP_ITM.MdiParent = base.ParentForm.ParentForm;
				(base.ParentForm.ParentForm as frmPccesMain).LeftPanel.Width = 0;
				base.ParentForm.Close();
				FM_CMP_ITM.Show();
			}
		}
		Cursor = Cursors.Default;
	}

	public void BtnFunc7_Click(object sender, EventArgs e)
	{
		if (!IsCanSwitchForm())
		{
			return;
		}
		F_CurrOpenMode = FunctionOpenMode.Common;
		if (!DBClass.ChkAuthority(F_UserID, "F007"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F007") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		GC.Collect();
		if (!(base.ParentForm is frmPccesMain))
		{
			base.ParentForm.Enabled = false;
		}
		Cursor = Cursors.WaitCursor;
		HideAllChild();
		bool IsFormExist = false;
		if (base.ParentForm is frmPccesMain)
		{
			Form[] mdiChildren = base.ParentForm.MdiChildren;
			foreach (Form frm in mdiChildren)
			{
				if (frm is FormCompareMrs)
				{
					IsFormExist = true;
					frm.Show();
					(base.ParentForm as frmPccesMain).LeftPanel.Width = 0;
					break;
				}
				if (!(frm is FormPanel) && !(frm is FormPanel2) && !(frm is FormPanel3))
				{
					frm.Close();
					frm.Dispose();
				}
			}
			if (!IsFormExist)
			{
				FormCompareMrs FM_CMP_MRS = new FormCompareMrs();
				FM_CMP_MRS._UserID = (base.ParentForm as frmPccesMain)._UserID;
				FM_CMP_MRS._UserName = (base.ParentForm as frmPccesMain)._UserName;
				FM_CMP_MRS._ServerName = (base.ParentForm as frmPccesMain)._ServerName;
				FM_CMP_MRS._HasRegistered = HasRegistered();
				FM_CMP_MRS.MdiParent = base.ParentForm;
				FM_CMP_MRS.Show();
				(base.ParentForm as frmPccesMain).LeftPanel.Width = 0;
			}
		}
		else if (base.ParentForm is FormCompareMrs)
		{
			base.ParentForm.Enabled = true;
			base.ParentForm.Show();
			base.ParentForm.BringToFront();
		}
		else
		{
			Form[] mdiChildren = base.ParentForm.ParentForm.MdiChildren;
			foreach (Form frm in mdiChildren)
			{
				if (frm is FormCompareMrs)
				{
					IsFormExist = true;
					frm.Show();
					base.ParentForm.Close();
					break;
				}
			}
			if (!IsFormExist)
			{
				FormCompareMrs FM_CMP_MRS = new FormCompareMrs();
				FM_CMP_MRS._UserID = (base.ParentForm.ParentForm as frmPccesMain)._UserID;
				FM_CMP_MRS._UserName = (base.ParentForm.ParentForm as frmPccesMain)._UserName;
				FM_CMP_MRS._ServerName = (base.ParentForm.ParentForm as frmPccesMain)._ServerName;
				FM_CMP_MRS._HasRegistered = HasRegistered();
				FM_CMP_MRS.MdiParent = base.ParentForm.ParentForm;
				(base.ParentForm.ParentForm as frmPccesMain).LeftPanel.Width = 0;
				base.ParentForm.Close();
				FM_CMP_MRS.Show();
			}
		}
		Cursor = Cursors.Default;
	}

	public void BtnMain2_Click(object sender, EventArgs e)
	{
		if (F_CurrOpenMode != FunctionOpenMode.Invoice)
		{
			HideAllButton();
			BtnFunc9.Visible = true;
			ProcessMessage();
			BtnFunc6.Visible = true;
			ProcessMessage();
			BtnFunc10.Visible = true;
			ProcessMessage();
			BtnFunc11.Visible = true;
			ProcessMessage();
			BtnFunc12.Visible = true;
			ProcessMessage();
			F_CurrOpenMode = FunctionOpenMode.Invoice;
		}
	}

	public void BtnFunc9_Click(object sender, EventArgs e)
	{
		if (!IsCanSwitchForm())
		{
			return;
		}
		F_CurrOpenMode = FunctionOpenMode.Invoice;
		if (!DBClass.ChkAuthority(F_UserID, "F009"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F009") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (!(base.ParentForm is frmPccesMain))
		{
			base.ParentForm.Enabled = false;
		}
		Cursor = Cursors.WaitCursor;
		GC.Collect();
		HideAllChild();
		if (base.ParentForm is frmPccesMain)
		{
			Form[] mdiChildren = base.ParentForm.MdiChildren;
			foreach (Form frm in mdiChildren)
			{
				if (!(frm is FormPanel) && !(frm is FormPanel2) && !(frm is FormPanel3))
				{
					frm.Close();
					frm.Dispose();
				}
			}
			FormBudgetProjectPick FM_BDGT_PPK = new FormBudgetProjectPick();
			FM_BDGT_PPK._ActionName = PccesFormAction.SplitContract;
			FM_BDGT_PPK._UserID = F_UserID;
			FM_BDGT_PPK._HasRegistered = HasRegistered();
			FM_BDGT_PPK.ShowDialog(base.ParentForm);
			FM_BDGT_PPK.Dispose();
			FM_BDGT_PPK = null;
		}
		else if (base.ParentForm is FormSplitContract)
		{
			base.ParentForm.Enabled = true;
			base.ParentForm.Show();
			base.ParentForm.BringToFront();
		}
		else
		{
			FormBudgetProjectPick FM_BDGT_PPK = new FormBudgetProjectPick();
			FM_BDGT_PPK._ActionName = PccesFormAction.SplitContract;
			FM_BDGT_PPK._UserID = F_UserID;
			FM_BDGT_PPK._HasRegistered = HasRegistered();
			if (FM_BDGT_PPK.ShowDialog(base.ParentForm.ParentForm) == DialogResult.Cancel)
			{
				base.ParentForm.Enabled = true;
			}
			else
			{
				Form[] mdiChildren = base.ParentForm.ParentForm.MdiChildren;
				foreach (Form frm in mdiChildren)
				{
					if (!(frm is FormPanel) && !(frm is FormPanel2) && !(frm is FormPanel3) && !(frm is FormSplitContract))
					{
						frm.Close();
						frm.Dispose();
					}
				}
			}
			FM_BDGT_PPK.Dispose();
			FM_BDGT_PPK = null;
		}
		Cursor = Cursors.Default;
	}

	public void BtnFunc10_Click(object sender, EventArgs e)
	{
		if (!IsCanSwitchForm())
		{
			return;
		}
		F_CurrOpenMode = FunctionOpenMode.Invoice;
		if (!DBClass.ChkAuthority(F_UserID, "F010"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F010") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (!(base.ParentForm is frmPccesMain))
		{
			base.ParentForm.Enabled = false;
		}
		Cursor = Cursors.WaitCursor;
		GC.Collect();
		HideAllChild();
		if (base.ParentForm is frmPccesMain)
		{
			Form[] mdiChildren = base.ParentForm.MdiChildren;
			foreach (Form frm in mdiChildren)
			{
				if (!(frm is FormPanel) && !(frm is FormPanel2) && !(frm is FormPanel3))
				{
					frm.Close();
					frm.Dispose();
				}
			}
			FormBudgetProjectPick FM_BDGT_PPK = new FormBudgetProjectPick();
			FM_BDGT_PPK._ActionName = PccesFormAction.Invoice;
			FM_BDGT_PPK._UserID = F_UserID;
			FM_BDGT_PPK._HasRegistered = HasRegistered();
			FM_BDGT_PPK.ShowDialog(base.ParentForm);
			FM_BDGT_PPK.Dispose();
			FM_BDGT_PPK = null;
		}
		else if (base.ParentForm is FormInvoice)
		{
			base.ParentForm.Enabled = true;
			base.ParentForm.Show();
			base.ParentForm.BringToFront();
		}
		else
		{
			FormBudgetProjectPick FM_BDGT_PPK = new FormBudgetProjectPick();
			FM_BDGT_PPK._ActionName = PccesFormAction.Invoice;
			FM_BDGT_PPK._UserID = F_UserID;
			FM_BDGT_PPK._HasRegistered = HasRegistered();
			if (FM_BDGT_PPK.ShowDialog(base.ParentForm.ParentForm) == DialogResult.Cancel)
			{
				base.ParentForm.Enabled = true;
			}
			else
			{
				Form[] mdiChildren = base.ParentForm.ParentForm.MdiChildren;
				foreach (Form frm in mdiChildren)
				{
					if (!(frm is FormPanel) && !(frm is FormPanel2) && !(frm is FormPanel3) && !(frm is FormInvoice))
					{
						frm.Close();
						frm.Dispose();
					}
				}
			}
			FM_BDGT_PPK.Dispose();
			FM_BDGT_PPK = null;
		}
		Cursor = Cursors.Default;
	}

	public void BtnFunc6_Click(object sender, EventArgs e)
	{
		if (!IsCanSwitchForm())
		{
			return;
		}
		F_CurrOpenMode = FunctionOpenMode.Invoice;
		if (!DBClass.ChkAuthority(F_UserID, "F011"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F011") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (!(base.ParentForm is frmPccesMain))
		{
			base.ParentForm.Enabled = false;
		}
		Cursor = Cursors.WaitCursor;
		GC.Collect();
		HideAllChild();
		if (base.ParentForm is frmPccesMain)
		{
			Form[] mdiChildren = base.ParentForm.MdiChildren;
			foreach (Form frm in mdiChildren)
			{
				if (!(frm is FormPanel) && !(frm is FormPanel2) && !(frm is FormPanel3))
				{
					frm.Close();
					frm.Dispose();
				}
			}
			FormBudgetProjectPick FM_BDGT_PPK = new FormBudgetProjectPick();
			FM_BDGT_PPK._ActionName = PccesFormAction.BudgetChange;
			FM_BDGT_PPK._UserID = F_UserID;
			FM_BDGT_PPK._HasRegistered = HasRegistered();
			FM_BDGT_PPK.ShowDialog(base.ParentForm);
			FM_BDGT_PPK.Dispose();
			FM_BDGT_PPK = null;
		}
		else if (base.ParentForm is FormBudgetChange)
		{
			base.ParentForm.Enabled = true;
			base.ParentForm.Show();
			base.ParentForm.BringToFront();
		}
		else
		{
			FormBudgetProjectPick FM_BDGT_PPK = new FormBudgetProjectPick();
			FM_BDGT_PPK._ActionName = PccesFormAction.BudgetChange;
			FM_BDGT_PPK._UserID = F_UserID;
			FM_BDGT_PPK._HasRegistered = HasRegistered();
			if (FM_BDGT_PPK.ShowDialog(base.ParentForm.ParentForm) == DialogResult.Cancel)
			{
				base.ParentForm.Enabled = true;
			}
			else
			{
				Form[] mdiChildren = base.ParentForm.ParentForm.MdiChildren;
				foreach (Form frm in mdiChildren)
				{
					if (!(frm is FormPanel) && !(frm is FormPanel2) && !(frm is FormPanel3) && !(frm is FormBudgetChange))
					{
						frm.Close();
						frm.Dispose();
					}
				}
			}
			FM_BDGT_PPK.Dispose();
			FM_BDGT_PPK = null;
		}
		Cursor = Cursors.Default;
	}

	public void BtnFunc11_Click(object sender, EventArgs e)
	{
		if (!IsCanSwitchForm())
		{
			return;
		}
		F_CurrOpenMode = FunctionOpenMode.Invoice;
		if (!DBClass.ChkAuthority(F_UserID, "F012"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F012") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (!(base.ParentForm is frmPccesMain))
		{
			base.ParentForm.Enabled = false;
		}
		Cursor = Cursors.WaitCursor;
		GC.Collect();
		HideAllChild();
		if (base.ParentForm is frmPccesMain)
		{
			Form[] mdiChildren = base.ParentForm.MdiChildren;
			foreach (Form frm in mdiChildren)
			{
				if (!(frm is FormPanel) && !(frm is FormPanel2) && !(frm is FormPanel3))
				{
					frm.Close();
					frm.Dispose();
				}
			}
			FormBudgetProjectPick FM_BDGT_PPK = new FormBudgetProjectPick();
			FM_BDGT_PPK._ActionName = PccesFormAction.SubClose;
			FM_BDGT_PPK._UserID = F_UserID;
			FM_BDGT_PPK._HasRegistered = HasRegistered();
			FM_BDGT_PPK.ShowDialog(base.ParentForm);
			FM_BDGT_PPK.Dispose();
			FM_BDGT_PPK = null;
		}
		else if (base.ParentForm is FormSubClose)
		{
			base.ParentForm.Enabled = true;
			base.ParentForm.Show();
			base.ParentForm.BringToFront();
		}
		else
		{
			FormBudgetProjectPick FM_BDGT_PPK = new FormBudgetProjectPick();
			FM_BDGT_PPK._ActionName = PccesFormAction.SubClose;
			FM_BDGT_PPK._UserID = F_UserID;
			FM_BDGT_PPK._HasRegistered = HasRegistered();
			if (FM_BDGT_PPK.ShowDialog(base.ParentForm.ParentForm) == DialogResult.Cancel)
			{
				base.ParentForm.Enabled = true;
			}
			else
			{
				Form[] mdiChildren = base.ParentForm.ParentForm.MdiChildren;
				foreach (Form frm in mdiChildren)
				{
					if (!(frm is FormPanel) && !(frm is FormPanel2) && !(frm is FormPanel3) && !(frm is FormSubClose))
					{
						frm.Close();
						frm.Dispose();
					}
				}
			}
			FM_BDGT_PPK.Dispose();
			FM_BDGT_PPK = null;
		}
		Cursor = Cursors.Default;
	}

	public void BtnFunc12_Click(object sender, EventArgs e)
	{
		if (!IsCanSwitchForm())
		{
			return;
		}
		F_CurrOpenMode = FunctionOpenMode.Invoice;
		OPEN_MODE_CHECK();
		if (!(base.ParentForm is frmPccesMain))
		{
			base.ParentForm.Enabled = false;
		}
		Cursor = Cursors.WaitCursor;
		GC.Collect();
		HideAllChild();
		if (base.ParentForm is frmPccesMain)
		{
			Form[] mdiChildren = base.ParentForm.MdiChildren;
			foreach (Form frm in mdiChildren)
			{
				if (!(frm is FormPanel) && !(frm is FormPanel2) && !(frm is FormPanel3))
				{
					frm.Close();
					frm.Dispose();
				}
			}
			FormBudgetProjectPick FM_BDGT_PPK = new FormBudgetProjectPick();
			FM_BDGT_PPK._ActionName = PccesFormAction.SubFinal;
			FM_BDGT_PPK._UserID = F_UserID;
			FM_BDGT_PPK._HasRegistered = HasRegistered();
			FM_BDGT_PPK.ShowDialog(base.ParentForm);
			FM_BDGT_PPK.Dispose();
			FM_BDGT_PPK = null;
		}
		else if (base.ParentForm is FormSubFinal)
		{
			base.ParentForm.Enabled = true;
			base.ParentForm.Show();
			base.ParentForm.BringToFront();
		}
		else
		{
			FormBudgetProjectPick FM_BDGT_PPK = new FormBudgetProjectPick();
			FM_BDGT_PPK._ActionName = PccesFormAction.SubFinal;
			FM_BDGT_PPK._UserID = F_UserID;
			FM_BDGT_PPK._HasRegistered = HasRegistered();
			if (FM_BDGT_PPK.ShowDialog(base.ParentForm.ParentForm) == DialogResult.Cancel)
			{
				base.ParentForm.Enabled = true;
			}
			else
			{
				Form[] mdiChildren = base.ParentForm.ParentForm.MdiChildren;
				foreach (Form frm in mdiChildren)
				{
					if (!(frm is FormPanel) && !(frm is FormPanel2) && !(frm is FormPanel3) && !(frm is FormSubFinal))
					{
						frm.Close();
						frm.Dispose();
					}
				}
			}
			FM_BDGT_PPK.Dispose();
			FM_BDGT_PPK = null;
		}
		Cursor = Cursors.Default;
	}

	public void BtnFunc13_Click(object sender, EventArgs e)
	{
		lock (this)
		{
			if (!DBClass.ChkAuthority(F_UserID, "F006"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F006") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			GC.Collect();
			if (!(base.ParentForm is frmPccesMain))
			{
				base.ParentForm.Enabled = false;
			}
			Cursor = Cursors.WaitCursor;
			HideAllChild();
			bool IsFormExist = false;
			if (base.ParentForm is frmPccesMain)
			{
				Form[] mdiChildren = base.ParentForm.MdiChildren;
				foreach (Form frm in mdiChildren)
				{
					if (frm is FormSysPlugin)
					{
						IsFormExist = true;
						(base.ParentForm as frmPccesMain).LeftPanel.Width = 0;
						frm.Show();
						break;
					}
					if (!(frm is FormPanel) && !(frm is FormPanel2) && !(frm is FormPanel3))
					{
						frm.Close();
						frm.Dispose();
					}
				}
				if (!IsFormExist)
				{
					FormSysPlugin FM_SysPlugin = new FormSysPlugin();
					FM_SysPlugin._UserID = (base.ParentForm as frmPccesMain)._UserID;
					FM_SysPlugin._UserName = (base.ParentForm as frmPccesMain)._UserName;
					FM_SysPlugin._ServerName = (base.ParentForm as frmPccesMain)._ServerName;
					FM_SysPlugin._HasRegistered = HasRegistered();
					FM_SysPlugin.MdiParent = base.ParentForm;
					FM_SysPlugin.Show();
					(base.ParentForm as frmPccesMain).LeftPanel.Width = 0;
				}
			}
			else if (base.ParentForm is FormSysPlugin)
			{
				base.ParentForm.Enabled = true;
				base.ParentForm.Show();
				base.ParentForm.BringToFront();
			}
			else
			{
				Form[] mdiChildren = base.ParentForm.ParentForm.MdiChildren;
				foreach (Form frm in mdiChildren)
				{
					if (frm is FormSysPlugin)
					{
						IsFormExist = true;
						frm.Show();
						base.ParentForm.Close();
						break;
					}
				}
				if (!IsFormExist)
				{
					FormSysPlugin FM_SysPlugin = new FormSysPlugin();
					FM_SysPlugin._UserID = (base.ParentForm.ParentForm as frmPccesMain)._UserID;
					FM_SysPlugin._UserName = (base.ParentForm.ParentForm as frmPccesMain)._UserName;
					FM_SysPlugin._ServerName = (base.ParentForm.ParentForm as frmPccesMain)._ServerName;
					FM_SysPlugin._HasRegistered = HasRegistered();
					FM_SysPlugin.MdiParent = base.ParentForm.ParentForm;
					(base.ParentForm.ParentForm as frmPccesMain).LeftPanel.Width = 0;
					Thread.Sleep(500);
					base.ParentForm.Close();
					FM_SysPlugin.Show();
				}
			}
		}
		Cursor = Cursors.Default;
	}

	public void BtnFunc1_Click(object sender, EventArgs e)
	{
		if (F_UserID != "PccesUser" && !DBClass.ChkAuthority(F_UserID, "F001"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		GC.Collect();
		if (!(base.ParentForm is frmPccesMain))
		{
			base.ParentForm.Enabled = false;
		}
		Cursor = Cursors.WaitCursor;
		HideAllChild();
		bool IsFormExist = false;
		if (base.ParentForm is frmPccesMain)
		{
			Form[] mdiChildren = base.ParentForm.MdiChildren;
			foreach (Form frm in mdiChildren)
			{
				if (frm is frmSysMaintain)
				{
					IsFormExist = true;
					(base.ParentForm as frmPccesMain).LeftPanel.Width = 0;
					if (optionTabSelected)
					{
						optionTabSelected = false;
						(frm as frmSysMaintain).Tab_Z.Tab.Selected = true;
					}
					frm.Show();
					break;
				}
				if (!(frm is FormPanel) && !(frm is FormPanel2) && !(frm is FormPanel3))
				{
					frm.Close();
					frm.Dispose();
				}
			}
			if (!IsFormExist)
			{
				frmSysMaintain FM_SysMaintain = new frmSysMaintain();
				FM_SysMaintain._UserID = (base.ParentForm as frmPccesMain)._UserID;
				FM_SysMaintain._UserName = (base.ParentForm as frmPccesMain)._UserName;
				FM_SysMaintain._ServerName = (base.ParentForm as frmPccesMain)._ServerName;
				FM_SysMaintain._HasRegistered = HasRegistered();
				FM_SysMaintain.MdiParent = base.ParentForm;
				if (optionTabSelected)
				{
					optionTabSelected = false;
					FM_SysMaintain.Tab_Z.Tab.Selected = true;
				}
				FM_SysMaintain.Show();
				(base.ParentForm as frmPccesMain).LeftPanel.Width = 0;
			}
		}
		else if (base.ParentForm is frmSysMaintain)
		{
			base.ParentForm.Enabled = true;
			base.ParentForm.Show();
			base.ParentForm.BringToFront();
		}
		else
		{
			Form[] mdiChildren = base.ParentForm.ParentForm.MdiChildren;
			foreach (Form frm in mdiChildren)
			{
				if (frm is frmSysMaintain)
				{
					IsFormExist = true;
					if (optionTabSelected)
					{
						optionTabSelected = false;
						(frm as frmSysMaintain).Tab_Z.Tab.Selected = true;
					}
					frm.Show();
					base.ParentForm.Close();
					break;
				}
			}
			if (!IsFormExist)
			{
				frmSysMaintain FM_SysMaintain = new frmSysMaintain();
				FM_SysMaintain._UserID = (base.ParentForm.ParentForm as frmPccesMain)._UserID;
				FM_SysMaintain._UserName = (base.ParentForm.ParentForm as frmPccesMain)._UserName;
				FM_SysMaintain._ServerName = (base.ParentForm.ParentForm as frmPccesMain)._ServerName;
				FM_SysMaintain._HasRegistered = HasRegistered();
				FM_SysMaintain.MdiParent = base.ParentForm.ParentForm;
				(base.ParentForm.ParentForm as frmPccesMain).LeftPanel.Width = 0;
				Thread.Sleep(500);
				base.ParentForm.Close();
				if (optionTabSelected)
				{
					optionTabSelected = false;
					FM_SysMaintain.Tab_Z.Tab.Selected = true;
				}
				FM_SysMaintain.Show();
			}
		}
		Cursor = Cursors.Default;
	}

	public void OPEN_MODE_CHECK()
	{
		ModuleManager oManager = new ModuleManager();
		HideAllButton();
		if (F_CurrOpenMode == FunctionOpenMode.Budget && oManager.EnableBudgetMdoule)
		{
			BtnFunc5.Visible = oManager.EnableBudgetMdoule;
			BtnFunc3.Visible = oManager.EnableBudgetMdoule;
		}
		else if (F_CurrOpenMode == FunctionOpenMode.Invoice && oManager.EnableContractModule)
		{
			BtnFunc9.Visible = oManager.EnableBudgetMdoule;
			BtnFunc10.Visible = oManager.EnableBudgetMdoule;
			BtnFunc6.Visible = oManager.EnableBudgetMdoule;
			BtnFunc11.Visible = oManager.EnableBudgetMdoule;
			BtnFunc12.Visible = oManager.EnableBudgetMdoule;
		}
		else if (F_CurrOpenMode == FunctionOpenMode.Bid && oManager.EnableBidMdoule)
		{
			BtnFuncBidImport.Visible = oManager.EnableBidMdoule;
			BtnFunc4.Visible = oManager.EnableBidMdoule;
		}
		else if (F_CurrOpenMode == FunctionOpenMode.Common && oManager.EnableBidMdoule)
		{
			BtnFunc2.Visible = oManager.EnableCommonMdoule;
			BtnFunc8.Visible = oManager.EnableCommonMdoule;
			BtnFunc7.Visible = oManager.EnableCommonMdoule;
		}
		if (oManager.EnableBudgetMdoule || oManager.EnableBidMdoule || oManager.EnableContractModule)
		{
			pnModuleFlow.Visible = true;
		}
		else
		{
			pnModuleFlow.Visible = false;
		}
		BtnMain1.Visible = oManager.EnableBudgetMdoule;
		BtnMain2.Visible = oManager.EnableContractModule;
		BtnMain3.Visible = oManager.EnableBidMdoule;
		BtnMain4.Visible = oManager.EnableCommonMdoule;
	}

	private void SetActiveFunction()
	{
		switch (F_ActiveFunction.ToUpper())
		{
		case "MRSBASE":
			BtnFunc2.Appearance.Image = imageList1.Images[13];
			BtnFunc2.Appearance.ImageBackground = imgButtons.Images[2];
			break;
		case "PROJECT":
			BtnFunc5.Appearance.Image = imageList1.Images[4];
			BtnFunc5.Appearance.ImageBackground = imgButtons.Images[2];
			break;
		case "BUD":
			BtnFunc3.Appearance.Image = imageList1.Images[10];
			BtnFunc3.Appearance.ImageBackground = imgButtons.Images[2];
			break;
		case "COMPAREITEM":
			BtnFunc8.Appearance.Image = imageList1.Images[22];
			BtnFunc8.Appearance.ImageBackground = imgButtons.Images[2];
			break;
		case "COMPAREMRS":
			BtnFunc7.Appearance.Image = imageList1.Images[19];
			BtnFunc7.Appearance.ImageBackground = imgButtons.Images[2];
			break;
		case "BID":
			BtnFuncBidImport.Appearance.Image = imageList1.Images[7];
			BtnFuncBidImport.Appearance.ImageBackground = imgButtons.Images[2];
			break;
		case "SPLIT_CONTRACT":
			BtnFunc9.Appearance.Image = imageList1.Images[25];
			BtnFunc9.Appearance.ImageBackground = imgButtons.Images[2];
			break;
		case "BDGT_CHANGE":
			BtnFunc6.Appearance.Image = imageList1.Images[28];
			BtnFunc6.Appearance.ImageBackground = imgButtons.Images[2];
			break;
		case "SYSMAINTAIN":
			BtnFunc1.Appearance.Image = imageList1.Images[1];
			BtnFunc1.Appearance.ImageBackground = imgButtons.Images[5];
			break;
		}
	}

	private void CorrectRatio()
	{
		try
		{
			_ = base.ParentForm.Handle;
			bool flag = 1 == 0;
			double ratio = CommonMethods.GetWindowRatio(base.ParentForm.Handle);
			if (ratio == 1.0)
			{
				return;
			}
			foreach (Control Cn in base.Controls)
			{
				Cn.Font = new Font(Cn.Name, (float)((double)Cn.Font.Size * ratio));
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "FunctionButtons.cs--> CorrectRatio()" + ex.Message);
		}
	}

	private void DoCreateBudgetBidFile(bool isBudget)
	{
		frmBudget FM_BDGT = null;
		((!isBudget) ? CreateFormBudgetByBID() : CreateFormBudgetByBUD())?.Do_BudBidFileDigital();
	}

	private void DoProjectCreateImport(bool isCreate)
	{
		FormProject FM_PROJ = CreateFormProject();
		if (FM_PROJ != null)
		{
			if (isCreate)
			{
				FM_PROJ.ExecuteNewProject("0", InitCreateProject: true);
			}
			else
			{
				FM_PROJ.ExecuteNewProject("0", InitCreateProject: false);
			}
		}
	}

	private void HideAllChild()
	{
		Form[] ownedForms = base.ParentForm.OwnedForms;
		foreach (Form frm in ownedForms)
		{
			Archnowledge.Common.DebugUtil.OutputDebugString("HideAllChild ParentForm:[" + base.ParentForm.Text + "]， OwnedForms :[" + frm.Text + "]");
			frm.Close();
		}
	}

	private FormProject CreateFormProject()
	{
		FormProject FM_PROJ = null;
		if (!IsCanSwitchForm())
		{
			return null;
		}
		F_CurrOpenMode = FunctionOpenMode.Budget;
		if (!DBClass.ChkAuthority(F_UserID, "F005"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F005") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
		GC.Collect();
		FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
		FM_INFO._InfoString = "專案目錄載入中，請稍候! ";
		FM_INFO.Show();
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		if (!(base.ParentForm is frmPccesMain))
		{
			base.ParentForm.Enabled = false;
		}
		HideAllChild();
		bool IsFormExist = false;
		if (base.ParentForm is frmPccesMain)
		{
			Form[] mdiChildren = base.ParentForm.MdiChildren;
			foreach (Form frm in mdiChildren)
			{
				if (frm is FormProject)
				{
					IsFormExist = true;
					(base.ParentForm as frmPccesMain).LeftPanel.Width = 0;
					(frm as FormProject)._UserID = (base.ParentForm as frmPccesMain)._UserID;
					(frm as FormProject)._UserName = (base.ParentForm as frmPccesMain)._UserName;
					(frm as FormProject)._ServerName = (base.ParentForm as frmPccesMain)._ServerName;
					(frm as FormProject)._HasRegistered = HasRegistered();
					frm.Show();
					FM_PROJ = frm as FormProject;
					break;
				}
				if (!(frm is FormPanel) && !(frm is FormPanel2) && !(frm is FormPanel3))
				{
					frm.Close();
					frm.Dispose();
				}
			}
			if (!IsFormExist)
			{
				FM_PROJ = new FormProject();
				FM_PROJ._UserID = (base.ParentForm as frmPccesMain)._UserID;
				FM_PROJ._UserName = (base.ParentForm as frmPccesMain)._UserName;
				FM_PROJ._ServerName = (base.ParentForm as frmPccesMain)._ServerName;
				FM_PROJ._HasRegistered = HasRegistered();
				FM_PROJ.MdiParent = base.ParentForm;
				FM_PROJ.Show();
				(base.ParentForm as frmPccesMain).LeftPanel.Width = 0;
			}
		}
		else if (base.ParentForm is FormProject)
		{
			base.ParentForm.Enabled = true;
			base.ParentForm.Show();
			base.ParentForm.BringToFront();
			FM_PROJ = base.ParentForm as FormProject;
		}
		else
		{
			Form[] mdiChildren = base.ParentForm.ParentForm.MdiChildren;
			foreach (Form frm in mdiChildren)
			{
				if (frm is FormProject)
				{
					IsFormExist = true;
					(frm as FormProject)._UserID = (base.ParentForm.ParentForm as frmPccesMain)._UserID;
					(frm as FormProject)._UserName = (base.ParentForm.ParentForm as frmPccesMain)._UserName;
					(frm as FormProject)._ServerName = (base.ParentForm.ParentForm as frmPccesMain)._ServerName;
					(frm as FormProject)._HasRegistered = HasRegistered();
					frm.Show();
					base.ParentForm.Close();
					FM_PROJ = frm as FormProject;
					break;
				}
			}
			if (!IsFormExist)
			{
				FM_PROJ = new FormProject();
				FM_PROJ._UserID = (base.ParentForm.ParentForm as frmPccesMain)._UserID;
				FM_PROJ._UserName = (base.ParentForm.ParentForm as frmPccesMain)._UserName;
				FM_PROJ._ServerName = (base.ParentForm.ParentForm as frmPccesMain)._ServerName;
				FM_PROJ._HasRegistered = HasRegistered();
				FM_PROJ.MdiParent = base.ParentForm.ParentForm;
				FM_PROJ.Show();
				(base.ParentForm.ParentForm as frmPccesMain).LeftPanel.Width = 0;
				base.ParentForm.Close();
			}
		}
		Cursor = Cursors.Default;
		FM_INFO.Close();
		FM_INFO.Dispose();
		return FM_PROJ;
	}

	private bool IsCanSwitchForm()
	{
		bool IsFormExist = true;
		if (base.ParentForm is frmBudget)
		{
			bool IsReCal = true;
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(F_UserID);
			aArr.Add("是否重新總計的旗標" + (base.ParentForm as frmBudget)._ProjectCode);
			DataTable DTEight = new DataTable();
			Archnowledge.Pcces.BUDClass.Project dbEight = new Archnowledge.Pcces.BUDClass.Project(aArr);
			dbEight.ps_srckind = CommonMethods.GetActionNameString((base.ParentForm as frmBudget)._ActionName);
			DTEight = dbEight.ListItem_eight("", (base.ParentForm as frmBudget)._ProjectCode);
			if (DTEight.Rows.Count > 0 && DTEight.Rows[0]["IsReCal"].ToString() == "Y")
			{
				IsReCal = false;
			}
			if (!IsReCal)
			{
				DialogResult Result = MessageBox.Show(this, "功能切換：\n\n詳細表資料有異動過，\n\n建議您先執行【重新總計】後，再切換其他功能，\n\n若您想要：\n\n(1)立刻執行【重新總計】，請按[是/Yes]\n(2)忽略不理會，請按[否/No]\n(3)取消，請按[取消/Cancel]", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
				if (Result == DialogResult.Yes)
				{
					(base.ParentForm as frmBudget)._Execute_Do_ReCal_All();
					(base.ParentForm as frmBudget)._IsHasConfirmReCal = true;
					IsFormExist = true;
				}
				if (Result == DialogResult.No)
				{
					(base.ParentForm as frmBudget)._IsHasConfirmReCal = true;
					IsFormExist = true;
				}
				if (Result == DialogResult.Cancel)
				{
					(base.ParentForm as frmBudget)._IsHasConfirmReCal = false;
					IsFormExist = false;
				}
			}
		}
		return IsFormExist;
	}

	private bool HasRegistered()
	{
		return (CommonMethods.GetIniValue("Register", "RegID").Trim() != "") ? true : false;
	}

	private void HideAllButton()
	{
		BtnFunc5.Visible = false;
		BtnFunc3.Visible = false;
		BtnFunc8.Visible = false;
		BtnFunc7.Visible = false;
		BtnFuncBidImport.Visible = false;
		BtnFunc2.Visible = false;
		BtnFunc9.Visible = false;
		BtnFunc10.Visible = false;
		BtnFunc6.Visible = false;
		BtnFunc11.Visible = false;
		BtnFunc12.Visible = false;
		BtnFunc4.Visible = false;
	}

	private void ProcessMessage()
	{
		Application.DoEvents();
	}

	private frmBudget CreateFormBudgetByBUD()
	{
		frmBudget FM_BDGT = null;
		if (base.ParentForm is frmBudget && (base.ParentForm as frmBudget)._ActionName != PccesFormAction.BUD && !IsCanSwitchForm())
		{
			return null;
		}
		F_CurrOpenMode = FunctionOpenMode.Budget;
		if (!DBClass.ChkAuthority(F_UserID, "F003"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
		GC.Collect();
		HideAllChild();
		if (base.ParentForm is frmPccesMain)
		{
			Form[] mdiChildren = base.ParentForm.MdiChildren;
			foreach (Form frm in mdiChildren)
			{
				if (!(frm is FormPanel) && !(frm is FormPanel2) && !(frm is FormPanel3))
				{
					frm.Close();
					frm.Dispose();
				}
			}
			FormBudgetProjectPick FM_BDGT_PPK = new FormBudgetProjectPick();
			FM_BDGT_PPK._ActionName = PccesFormAction.BUD;
			FM_BDGT_PPK._UserID = F_UserID;
			FM_BDGT_PPK._HasRegistered = HasRegistered();
			FM_BDGT_PPK.ShowDialog(base.ParentForm);
			FM_BDGT = FM_BDGT_PPK._FormBudget;
			FM_BDGT_PPK.Dispose();
			FM_BDGT = null;
		}
		else if (base.ParentForm is frmBudget && (base.ParentForm as frmBudget)._ActionName == PccesFormAction.BUD)
		{
			FM_BDGT = base.ParentForm as frmBudget;
		}
		else
		{
			FormBudgetProjectPick FM_BDGT_PPK = new FormBudgetProjectPick();
			FM_BDGT_PPK._ActionName = PccesFormAction.BUD;
			FM_BDGT_PPK._UserID = F_UserID;
			FM_BDGT_PPK._HasRegistered = HasRegistered();
			FM_BDGT_PPK.ShowDialog(base.ParentForm.ParentForm);
			FM_BDGT = FM_BDGT_PPK._FormBudget;
			FM_BDGT_PPK.Dispose();
			FM_BDGT_PPK = null;
		}
		return FM_BDGT;
	}

	private frmBudget CreateFormBudgetByBID()
	{
		frmBudget FM_BDGT = null;
		if (base.ParentForm is frmBudget && (base.ParentForm as frmBudget)._ActionName != PccesFormAction.BID && !IsCanSwitchForm())
		{
			return null;
		}
		F_CurrOpenMode = FunctionOpenMode.Bid;
		if (!DBClass.ChkAuthority(F_UserID, "F004"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
		GC.Collect();
		HideAllChild();
		if (base.ParentForm is frmPccesMain)
		{
			Form[] mdiChildren = base.ParentForm.MdiChildren;
			foreach (Form frm in mdiChildren)
			{
				if (!(frm is FormPanel) && !(frm is FormPanel2) && !(frm is FormPanel3))
				{
					frm.Close();
					frm.Dispose();
				}
			}
			FormBudgetProjectPick FM_BDGT_PPK = new FormBudgetProjectPick();
			FM_BDGT_PPK._ActionName = PccesFormAction.BID;
			FM_BDGT_PPK._UserID = F_UserID;
			FM_BDGT_PPK._HasRegistered = HasRegistered();
			FM_BDGT_PPK.ShowDialog(base.ParentForm);
			FM_BDGT = FM_BDGT_PPK._FormBudget;
			FM_BDGT_PPK.Dispose();
			FM_BDGT_PPK = null;
		}
		else if (base.ParentForm is frmBudget && (base.ParentForm as frmBudget)._ActionName == PccesFormAction.BID)
		{
			FM_BDGT = base.ParentForm as frmBudget;
		}
		else
		{
			FormBudgetProjectPick FM_BDGT_PPK = new FormBudgetProjectPick();
			FM_BDGT_PPK._ActionName = PccesFormAction.BID;
			FM_BDGT_PPK._UserID = F_UserID;
			FM_BDGT_PPK._HasRegistered = HasRegistered();
			FM_BDGT_PPK.ShowDialog(base.ParentForm.ParentForm);
			FM_BDGT = FM_BDGT_PPK._FormBudget;
			FM_BDGT_PPK.Dispose();
			FM_BDGT_PPK = null;
		}
		return FM_BDGT;
	}

	public void DisableButtons()
	{
		BtnFunc1.Enabled = false;
		BtnFunc2.Enabled = false;
		BtnFunc3.Enabled = false;
		BtnFuncBidImport.Enabled = false;
		BtnFunc5.Enabled = false;
		BtnFunc6.Enabled = false;
		BtnFunc7.Enabled = false;
		BtnFunc8.Enabled = false;
		BtnFunc9.Enabled = false;
		BtnFunc10.Enabled = false;
		BtnFunc11.Enabled = false;
		BtnFunc12.Enabled = false;
		BtnFunc4.Enabled = false;
	}

	public void EnableButtons()
	{
		BtnFunc1.Enabled = true;
		BtnFunc2.Enabled = true;
		BtnFunc3.Enabled = true;
		BtnFuncBidImport.Enabled = true;
		BtnFunc5.Enabled = true;
		BtnFunc6.Enabled = true;
		BtnFunc7.Enabled = true;
		BtnFunc8.Enabled = true;
		BtnFunc9.Enabled = true;
		BtnFunc10.Enabled = true;
		BtnFunc11.Enabled = true;
		BtnFunc12.Enabled = true;
		BtnFunc4.Enabled = true;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.ArchControls.FunctionButtons));
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
		Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.imgButtons = new System.Windows.Forms.ImageList(this.components);
		this.BtnMain4 = new Infragistics.Win.Misc.UltraButton();
		this.BtnFunc2 = new Infragistics.Win.Misc.UltraButton();
		this.BtnFunc8 = new Infragistics.Win.Misc.UltraButton();
		this.BtnFunc7 = new Infragistics.Win.Misc.UltraButton();
		this.BtnMain2 = new Infragistics.Win.Misc.UltraButton();
		this.BtnFunc9 = new Infragistics.Win.Misc.UltraButton();
		this.BtnFunc10 = new Infragistics.Win.Misc.UltraButton();
		this.BtnFunc6 = new Infragistics.Win.Misc.UltraButton();
		this.BtnFunc11 = new Infragistics.Win.Misc.UltraButton();
		this.BtnFunc12 = new Infragistics.Win.Misc.UltraButton();
		this.BtnFunc13 = new Infragistics.Win.Misc.UltraButton();
		this.BtnFunc1 = new Infragistics.Win.Misc.UltraButton();
		this.pnModuleFlow = new System.Windows.Forms.Panel();
		this.linkModuleFlowMap = new System.Windows.Forms.LinkLabel();
		this.ultraPictureBox1 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.linkLabel1 = new System.Windows.Forms.LinkLabel();
		this.PicBox = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.BtnMain1 = new Infragistics.Win.Misc.UltraButton();
		this.BtnFunc5 = new Infragistics.Win.Misc.UltraButton();
		this.BtnFunc3 = new Infragistics.Win.Misc.UltraButton();
		this.BtnMain3 = new Infragistics.Win.Misc.UltraButton();
		this.BtnFuncBidImport = new Infragistics.Win.Misc.UltraButton();
		this.BtnFunc4 = new Infragistics.Win.Misc.UltraButton();
		this.pnModuleFlow.SuspendLayout();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "");
		this.imageList1.Images.SetKeyName(1, "");
		this.imageList1.Images.SetKeyName(2, "");
		this.imageList1.Images.SetKeyName(3, "");
		this.imageList1.Images.SetKeyName(4, "");
		this.imageList1.Images.SetKeyName(5, "");
		this.imageList1.Images.SetKeyName(6, "");
		this.imageList1.Images.SetKeyName(7, "");
		this.imageList1.Images.SetKeyName(8, "");
		this.imageList1.Images.SetKeyName(9, "");
		this.imageList1.Images.SetKeyName(10, "");
		this.imageList1.Images.SetKeyName(11, "");
		this.imageList1.Images.SetKeyName(12, "");
		this.imageList1.Images.SetKeyName(13, "");
		this.imageList1.Images.SetKeyName(14, "");
		this.imageList1.Images.SetKeyName(15, "");
		this.imageList1.Images.SetKeyName(16, "");
		this.imageList1.Images.SetKeyName(17, "");
		this.imageList1.Images.SetKeyName(18, "");
		this.imageList1.Images.SetKeyName(19, "");
		this.imageList1.Images.SetKeyName(20, "");
		this.imageList1.Images.SetKeyName(21, "");
		this.imageList1.Images.SetKeyName(22, "");
		this.imageList1.Images.SetKeyName(23, "");
		this.imageList1.Images.SetKeyName(24, "");
		this.imageList1.Images.SetKeyName(25, "");
		this.imageList1.Images.SetKeyName(26, "");
		this.imageList1.Images.SetKeyName(27, "");
		this.imageList1.Images.SetKeyName(28, "");
		this.imageList1.Images.SetKeyName(29, "");
		this.imageList1.Images.SetKeyName(30, "");
		this.imageList1.Images.SetKeyName(31, "");
		this.imageList1.Images.SetKeyName(32, "");
		this.imageList1.Images.SetKeyName(33, "");
		this.imageList1.Images.SetKeyName(34, "");
		this.imageList1.Images.SetKeyName(35, "");
		this.imageList1.Images.SetKeyName(36, "");
		this.imageList1.Images.SetKeyName(37, "");
		this.imageList1.Images.SetKeyName(38, "");
		this.imageList1.Images.SetKeyName(39, "");
		this.imageList1.Images.SetKeyName(40, "");
		this.imageList1.Images.SetKeyName(41, "");
		this.imgButtons.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imgButtons.ImageStream");
		this.imgButtons.TransparentColor = System.Drawing.Color.Transparent;
		this.imgButtons.Images.SetKeyName(0, "");
		this.imgButtons.Images.SetKeyName(1, "");
		this.imgButtons.Images.SetKeyName(2, "");
		this.imgButtons.Images.SetKeyName(3, "");
		this.imgButtons.Images.SetKeyName(4, "");
		this.imgButtons.Images.SetKeyName(5, "");
		appearance1.BackColor = System.Drawing.Color.White;
		appearance1.BackColor2 = System.Drawing.Color.Silver;
		appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance1.BorderColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance1.ForeColor = System.Drawing.Color.FromArgb(0, 51, 94);
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance1.ImageBackground");
		appearance1.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnMain4.Appearance = appearance1;
		this.BtnMain4.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
		this.BtnMain4.Cursor = System.Windows.Forms.Cursors.Hand;
		this.BtnMain4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.BtnMain4.FlatMode = true;
		this.BtnMain4.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		appearance2.BorderColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance2.ForeColor = System.Drawing.Color.FromArgb(0, 51, 94);
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance2.ImageBackground");
		appearance2.ImageBackgroundOrigin = Infragistics.Win.ImageBackgroundOrigin.Client;
		this.BtnMain4.HotTrackAppearance = appearance2;
		this.BtnMain4.HotTracking = true;
		this.BtnMain4.ImageList = this.imageList1;
		this.BtnMain4.ImageSize = new System.Drawing.Size(24, 24);
		this.BtnMain4.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnMain4.Location = new System.Drawing.Point(0, 247);
		this.BtnMain4.Name = "BtnMain4";
		this.BtnMain4.Padding = new System.Drawing.Size(5, 0);
		appearance3.BorderColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance3.ForeColor = System.Drawing.Color.FromArgb(0, 51, 94);
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance3.ImageBackground");
		this.BtnMain4.PressedAppearance = appearance3;
		this.BtnMain4.ShapeImage = (System.Drawing.Image)resources.GetObject("BtnMain4.ShapeImage");
		this.BtnMain4.ShowFocusRect = false;
		this.BtnMain4.ShowOutline = false;
		this.BtnMain4.Size = new System.Drawing.Size(160, 32);
		this.BtnMain4.TabIndex = 41;
		this.BtnMain4.TabStop = false;
		this.BtnMain4.Text = "共用";
		this.BtnMain4.Click += new System.EventHandler(BtnMain4_Click);
		appearance4.BackColor = System.Drawing.Color.White;
		appearance4.BackColor2 = System.Drawing.Color.Silver;
		appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance4.BorderColor = System.Drawing.Color.FromArgb(167, 131, 60);
		appearance4.ForeColor = System.Drawing.Color.FromArgb(128, 99, 1);
		appearance4.Image = 12;
		appearance4.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance4.ImageBackground");
		appearance4.TextHAlign = Infragistics.Win.HAlign.Left;
		this.BtnFunc2.Appearance = appearance4;
		this.BtnFunc2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
		this.BtnFunc2.Cursor = System.Windows.Forms.Cursors.Hand;
		this.BtnFunc2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.BtnFunc2.FlatMode = true;
		this.BtnFunc2.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance5.BorderColor = System.Drawing.Color.FromArgb(167, 131, 60);
		appearance5.Image = 14;
		appearance5.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance5.ImageBackground");
		appearance5.ImageBackgroundOrigin = Infragistics.Win.ImageBackgroundOrigin.Client;
		this.BtnFunc2.HotTrackAppearance = appearance5;
		this.BtnFunc2.HotTracking = true;
		this.BtnFunc2.ImageList = this.imageList1;
		this.BtnFunc2.ImageSize = new System.Drawing.Size(24, 24);
		this.BtnFunc2.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnFunc2.Location = new System.Drawing.Point(0, 279);
		this.BtnFunc2.Name = "BtnFunc2";
		this.BtnFunc2.Padding = new System.Drawing.Size(5, 0);
		appearance6.BorderColor = System.Drawing.Color.FromArgb(167, 131, 60);
		appearance6.Image = 13;
		appearance6.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance6.ImageBackground");
		this.BtnFunc2.PressedAppearance = appearance6;
		this.BtnFunc2.ShapeImage = (System.Drawing.Image)resources.GetObject("BtnFunc2.ShapeImage");
		this.BtnFunc2.ShowFocusRect = false;
		this.BtnFunc2.ShowOutline = false;
		this.BtnFunc2.Size = new System.Drawing.Size(160, 32);
		this.BtnFunc2.TabIndex = 26;
		this.BtnFunc2.TabStop = false;
		this.BtnFunc2.Text = "  基本資料庫維護";
		this.BtnFunc2.Click += new System.EventHandler(BtnFunc2_Click);
		appearance7.BorderColor = System.Drawing.Color.FromArgb(167, 131, 60);
		appearance7.ForeColor = System.Drawing.Color.FromArgb(128, 99, 1);
		appearance7.Image = 21;
		appearance7.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance7.ImageBackground");
		appearance7.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnFunc8.Appearance = appearance7;
		this.BtnFunc8.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
		this.BtnFunc8.Cursor = System.Windows.Forms.Cursors.Hand;
		this.BtnFunc8.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.BtnFunc8.FlatMode = true;
		this.BtnFunc8.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance8.Image = 23;
		appearance8.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance8.ImageBackground");
		appearance8.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnFunc8.HotTrackAppearance = appearance8;
		this.BtnFunc8.HotTracking = true;
		this.BtnFunc8.ImageList = this.imageList1;
		this.BtnFunc8.ImageSize = new System.Drawing.Size(24, 24);
		this.BtnFunc8.Location = new System.Drawing.Point(0, 311);
		this.BtnFunc8.Name = "BtnFunc8";
		this.BtnFunc8.Padding = new System.Drawing.Size(5, 0);
		appearance9.Image = 22;
		appearance9.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance9.ImageBackground");
		appearance9.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnFunc8.PressedAppearance = appearance9;
		this.BtnFunc8.ShapeImage = (System.Drawing.Image)resources.GetObject("BtnFunc8.ShapeImage");
		this.BtnFunc8.ShowFocusRect = false;
		this.BtnFunc8.ShowOutline = false;
		this.BtnFunc8.Size = new System.Drawing.Size(160, 32);
		this.BtnFunc8.TabIndex = 31;
		this.BtnFunc8.TabStop = false;
		this.BtnFunc8.Text = "  歷史工程單位造價";
		this.BtnFunc8.Click += new System.EventHandler(BtnFunc8_Click);
		appearance10.BorderColor = System.Drawing.Color.FromArgb(167, 131, 60);
		appearance10.ForeColor = System.Drawing.Color.FromArgb(128, 99, 1);
		appearance10.Image = 18;
		appearance10.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance10.ImageBackground");
		appearance10.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance10.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnFunc7.Appearance = appearance10;
		this.BtnFunc7.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
		this.BtnFunc7.Cursor = System.Windows.Forms.Cursors.Hand;
		this.BtnFunc7.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.BtnFunc7.FlatMode = true;
		this.BtnFunc7.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance11.Image = 20;
		appearance11.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance11.ImageBackground");
		appearance11.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnFunc7.HotTrackAppearance = appearance11;
		this.BtnFunc7.HotTracking = true;
		this.BtnFunc7.ImageList = this.imageList1;
		this.BtnFunc7.ImageSize = new System.Drawing.Size(24, 24);
		this.BtnFunc7.Location = new System.Drawing.Point(0, 343);
		this.BtnFunc7.Name = "BtnFunc7";
		this.BtnFunc7.Padding = new System.Drawing.Size(5, 0);
		appearance12.Image = 19;
		appearance12.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance12.ImageBackground");
		appearance12.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnFunc7.PressedAppearance = appearance12;
		this.BtnFunc7.ShapeImage = (System.Drawing.Image)resources.GetObject("BtnFunc7.ShapeImage");
		this.BtnFunc7.ShowFocusRect = false;
		this.BtnFunc7.ShowOutline = false;
		this.BtnFunc7.Size = new System.Drawing.Size(160, 32);
		this.BtnFunc7.TabIndex = 30;
		this.BtnFunc7.TabStop = false;
		this.BtnFunc7.Text = "  經費審查比對";
		this.BtnFunc7.Click += new System.EventHandler(BtnFunc7_Click);
		appearance13.BackColor = System.Drawing.Color.White;
		appearance13.BackColor2 = System.Drawing.Color.Silver;
		appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance13.BorderColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance13.ForeColor = System.Drawing.Color.FromArgb(0, 51, 94);
		appearance13.Image = resources.GetObject("appearance13.Image");
		appearance13.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance13.ImageBackground");
		appearance13.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance13.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnMain2.Appearance = appearance13;
		this.BtnMain2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
		this.BtnMain2.Cursor = System.Windows.Forms.Cursors.Hand;
		this.BtnMain2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.BtnMain2.FlatMode = true;
		this.BtnMain2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		appearance14.BorderColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance14.ForeColor = System.Drawing.Color.FromArgb(0, 51, 94);
		appearance14.Image = resources.GetObject("appearance14.Image");
		appearance14.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance14.ImageBackground");
		appearance14.ImageBackgroundOrigin = Infragistics.Win.ImageBackgroundOrigin.Client;
		this.BtnMain2.HotTrackAppearance = appearance14;
		this.BtnMain2.HotTracking = true;
		this.BtnMain2.ImageList = this.imageList1;
		this.BtnMain2.ImageSize = new System.Drawing.Size(24, 24);
		this.BtnMain2.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnMain2.Location = new System.Drawing.Point(0, 375);
		this.BtnMain2.Name = "BtnMain2";
		this.BtnMain2.Padding = new System.Drawing.Size(5, 0);
		appearance15.BorderColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance15.ForeColor = System.Drawing.Color.FromArgb(0, 51, 94);
		appearance15.Image = resources.GetObject("appearance15.Image");
		appearance15.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance15.ImageBackground");
		this.BtnMain2.PressedAppearance = appearance15;
		this.BtnMain2.ShapeImage = (System.Drawing.Image)resources.GetObject("BtnMain2.ShapeImage");
		this.BtnMain2.ShowFocusRect = false;
		this.BtnMain2.ShowOutline = false;
		this.BtnMain2.Size = new System.Drawing.Size(160, 32);
		this.BtnMain2.TabIndex = 34;
		this.BtnMain2.TabStop = false;
		this.BtnMain2.Text = "估驗計價";
		this.BtnMain2.Click += new System.EventHandler(BtnMain2_Click);
		appearance16.BackColor = System.Drawing.Color.White;
		appearance16.BackColor2 = System.Drawing.Color.Silver;
		appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance16.BorderColor = System.Drawing.Color.FromArgb(167, 131, 60);
		appearance16.ForeColor = System.Drawing.Color.FromArgb(128, 99, 1);
		appearance16.Image = 24;
		appearance16.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance16.ImageBackground");
		appearance16.TextHAlign = Infragistics.Win.HAlign.Left;
		this.BtnFunc9.Appearance = appearance16;
		this.BtnFunc9.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
		this.BtnFunc9.Cursor = System.Windows.Forms.Cursors.Hand;
		this.BtnFunc9.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.BtnFunc9.FlatMode = true;
		this.BtnFunc9.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance17.BorderColor = System.Drawing.Color.FromArgb(167, 131, 60);
		appearance17.Image = 26;
		appearance17.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance17.ImageBackground");
		appearance17.ImageBackgroundOrigin = Infragistics.Win.ImageBackgroundOrigin.Client;
		this.BtnFunc9.HotTrackAppearance = appearance17;
		this.BtnFunc9.HotTracking = true;
		this.BtnFunc9.ImageList = this.imageList1;
		this.BtnFunc9.ImageSize = new System.Drawing.Size(24, 24);
		this.BtnFunc9.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnFunc9.Location = new System.Drawing.Point(0, 407);
		this.BtnFunc9.Name = "BtnFunc9";
		this.BtnFunc9.Padding = new System.Drawing.Size(5, 0);
		appearance18.BorderColor = System.Drawing.Color.FromArgb(167, 131, 60);
		appearance18.Image = 25;
		appearance18.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance18.ImageBackground");
		this.BtnFunc9.PressedAppearance = appearance18;
		this.BtnFunc9.ShapeImage = (System.Drawing.Image)resources.GetObject("BtnFunc9.ShapeImage");
		this.BtnFunc9.ShowFocusRect = false;
		this.BtnFunc9.ShowOutline = false;
		this.BtnFunc9.Size = new System.Drawing.Size(160, 32);
		this.BtnFunc9.TabIndex = 32;
		this.BtnFunc9.TabStop = false;
		this.BtnFunc9.Text = "  契約編製";
		this.BtnFunc9.Visible = false;
		this.BtnFunc9.Click += new System.EventHandler(BtnFunc9_Click);
		appearance19.BackColor = System.Drawing.Color.White;
		appearance19.BackColor2 = System.Drawing.Color.Silver;
		appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance19.BorderColor = System.Drawing.Color.FromArgb(167, 131, 60);
		appearance19.ForeColor = System.Drawing.Color.FromArgb(128, 99, 1);
		appearance19.Image = 30;
		appearance19.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance19.ImageBackground");
		appearance19.TextHAlign = Infragistics.Win.HAlign.Left;
		this.BtnFunc10.Appearance = appearance19;
		this.BtnFunc10.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
		this.BtnFunc10.Cursor = System.Windows.Forms.Cursors.Hand;
		this.BtnFunc10.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.BtnFunc10.FlatMode = true;
		this.BtnFunc10.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance20.BorderColor = System.Drawing.Color.FromArgb(167, 131, 60);
		appearance20.Image = 32;
		appearance20.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance20.ImageBackground");
		appearance20.ImageBackgroundOrigin = Infragistics.Win.ImageBackgroundOrigin.Client;
		this.BtnFunc10.HotTrackAppearance = appearance20;
		this.BtnFunc10.HotTracking = true;
		this.BtnFunc10.ImageList = this.imageList1;
		this.BtnFunc10.ImageSize = new System.Drawing.Size(24, 24);
		this.BtnFunc10.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnFunc10.Location = new System.Drawing.Point(0, 439);
		this.BtnFunc10.Name = "BtnFunc10";
		this.BtnFunc10.Padding = new System.Drawing.Size(5, 0);
		appearance21.BorderColor = System.Drawing.Color.FromArgb(167, 131, 60);
		appearance21.Image = 31;
		appearance21.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance21.ImageBackground");
		this.BtnFunc10.PressedAppearance = appearance21;
		this.BtnFunc10.ShapeImage = (System.Drawing.Image)resources.GetObject("BtnFunc10.ShapeImage");
		this.BtnFunc10.ShowFocusRect = false;
		this.BtnFunc10.ShowOutline = false;
		this.BtnFunc10.Size = new System.Drawing.Size(160, 32);
		this.BtnFunc10.TabIndex = 35;
		this.BtnFunc10.TabStop = false;
		this.BtnFunc10.Text = "  估驗記錄";
		this.BtnFunc10.Visible = false;
		this.BtnFunc10.Click += new System.EventHandler(BtnFunc10_Click);
		appearance22.BorderColor = System.Drawing.Color.FromArgb(167, 131, 60);
		appearance22.ForeColor = System.Drawing.Color.FromArgb(128, 99, 1);
		appearance22.Image = 27;
		appearance22.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance22.ImageBackground");
		appearance22.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance22.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnFunc6.Appearance = appearance22;
		this.BtnFunc6.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
		this.BtnFunc6.Cursor = System.Windows.Forms.Cursors.Hand;
		this.BtnFunc6.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.BtnFunc6.FlatMode = true;
		this.BtnFunc6.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance23.BorderColor = System.Drawing.Color.FromArgb(167, 131, 60);
		appearance23.Image = 29;
		appearance23.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance23.ImageBackground");
		appearance23.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnFunc6.HotTrackAppearance = appearance23;
		this.BtnFunc6.HotTracking = true;
		this.BtnFunc6.ImageList = this.imageList1;
		this.BtnFunc6.ImageSize = new System.Drawing.Size(24, 24);
		this.BtnFunc6.Location = new System.Drawing.Point(0, 471);
		this.BtnFunc6.Name = "BtnFunc6";
		this.BtnFunc6.Padding = new System.Drawing.Size(5, 0);
		appearance24.BorderColor = System.Drawing.Color.FromArgb(167, 131, 60);
		appearance24.Image = 28;
		appearance24.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance24.ImageBackground");
		appearance24.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnFunc6.PressedAppearance = appearance24;
		this.BtnFunc6.ShapeImage = (System.Drawing.Image)resources.GetObject("BtnFunc6.ShapeImage");
		this.BtnFunc6.ShowFocusRect = false;
		this.BtnFunc6.ShowOutline = false;
		this.BtnFunc6.Size = new System.Drawing.Size(160, 32);
		this.BtnFunc6.TabIndex = 29;
		this.BtnFunc6.TabStop = false;
		this.BtnFunc6.Text = "  契約變更";
		this.BtnFunc6.Visible = false;
		this.BtnFunc6.Click += new System.EventHandler(BtnFunc6_Click);
		appearance25.BackColor = System.Drawing.Color.White;
		appearance25.BackColor2 = System.Drawing.Color.Silver;
		appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance25.BorderColor = System.Drawing.Color.FromArgb(167, 131, 60);
		appearance25.ForeColor = System.Drawing.Color.FromArgb(128, 99, 1);
		appearance25.Image = 33;
		appearance25.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance25.ImageBackground");
		appearance25.TextHAlign = Infragistics.Win.HAlign.Left;
		this.BtnFunc11.Appearance = appearance25;
		this.BtnFunc11.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
		this.BtnFunc11.Cursor = System.Windows.Forms.Cursors.Hand;
		this.BtnFunc11.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.BtnFunc11.FlatMode = true;
		this.BtnFunc11.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance26.BorderColor = System.Drawing.Color.FromArgb(167, 131, 60);
		appearance26.Image = 35;
		appearance26.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance26.ImageBackground");
		appearance26.ImageBackgroundOrigin = Infragistics.Win.ImageBackgroundOrigin.Client;
		this.BtnFunc11.HotTrackAppearance = appearance26;
		this.BtnFunc11.HotTracking = true;
		this.BtnFunc11.ImageList = this.imageList1;
		this.BtnFunc11.ImageSize = new System.Drawing.Size(24, 24);
		this.BtnFunc11.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnFunc11.Location = new System.Drawing.Point(0, 503);
		this.BtnFunc11.Name = "BtnFunc11";
		this.BtnFunc11.Padding = new System.Drawing.Size(5, 0);
		appearance27.BorderColor = System.Drawing.Color.FromArgb(167, 131, 60);
		appearance27.Image = 34;
		appearance27.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance27.ImageBackground");
		this.BtnFunc11.PressedAppearance = appearance27;
		this.BtnFunc11.ShapeImage = (System.Drawing.Image)resources.GetObject("BtnFunc11.ShapeImage");
		this.BtnFunc11.ShowFocusRect = false;
		this.BtnFunc11.ShowOutline = false;
		this.BtnFunc11.Size = new System.Drawing.Size(160, 32);
		this.BtnFunc11.TabIndex = 36;
		this.BtnFunc11.TabStop = false;
		this.BtnFunc11.Text = "  結算";
		this.BtnFunc11.Visible = false;
		this.BtnFunc11.Click += new System.EventHandler(BtnFunc11_Click);
		appearance28.BackColor = System.Drawing.Color.White;
		appearance28.BackColor2 = System.Drawing.Color.Silver;
		appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance28.BorderColor = System.Drawing.Color.FromArgb(167, 131, 60);
		appearance28.ForeColor = System.Drawing.Color.FromArgb(128, 99, 1);
		appearance28.Image = 36;
		appearance28.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance28.ImageBackground");
		appearance28.TextHAlign = Infragistics.Win.HAlign.Left;
		this.BtnFunc12.Appearance = appearance28;
		this.BtnFunc12.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
		this.BtnFunc12.Cursor = System.Windows.Forms.Cursors.Hand;
		this.BtnFunc12.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.BtnFunc12.FlatMode = true;
		this.BtnFunc12.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance29.BorderColor = System.Drawing.Color.FromArgb(167, 131, 60);
		appearance29.Image = 38;
		appearance29.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance29.ImageBackground");
		appearance29.ImageBackgroundOrigin = Infragistics.Win.ImageBackgroundOrigin.Client;
		this.BtnFunc12.HotTrackAppearance = appearance29;
		this.BtnFunc12.HotTracking = true;
		this.BtnFunc12.ImageList = this.imageList1;
		this.BtnFunc12.ImageSize = new System.Drawing.Size(24, 24);
		this.BtnFunc12.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnFunc12.Location = new System.Drawing.Point(0, 535);
		this.BtnFunc12.Name = "BtnFunc12";
		this.BtnFunc12.Padding = new System.Drawing.Size(5, 0);
		appearance30.BorderColor = System.Drawing.Color.FromArgb(167, 131, 60);
		appearance30.Image = 37;
		appearance30.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance30.ImageBackground");
		this.BtnFunc12.PressedAppearance = appearance30;
		this.BtnFunc12.ShapeImage = (System.Drawing.Image)resources.GetObject("BtnFunc12.ShapeImage");
		this.BtnFunc12.ShowFocusRect = false;
		this.BtnFunc12.ShowOutline = false;
		this.BtnFunc12.Size = new System.Drawing.Size(160, 32);
		this.BtnFunc12.TabIndex = 37;
		this.BtnFunc12.TabStop = false;
		this.BtnFunc12.Text = "  決算";
		this.BtnFunc12.Visible = false;
		this.BtnFunc12.Click += new System.EventHandler(BtnFunc12_Click);
		appearance31.BackColor = System.Drawing.Color.White;
		appearance31.BackColor2 = System.Drawing.Color.Silver;
		appearance31.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance31.BorderColor = System.Drawing.Color.FromArgb(78, 151, 76);
		appearance31.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance31.FontData.SizeInPoints = 11f;
		appearance31.ForeColor = System.Drawing.Color.FromArgb(0, 51, 94);
		appearance31.Image = 39;
		appearance31.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance31.ImageBackground");
		appearance31.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance31.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnFunc13.Appearance = appearance31;
		this.BtnFunc13.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
		this.BtnFunc13.Cursor = System.Windows.Forms.Cursors.Hand;
		this.BtnFunc13.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.BtnFunc13.FlatMode = true;
		this.BtnFunc13.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance32.BorderColor = System.Drawing.Color.FromArgb(78, 151, 76);
		appearance32.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance32.FontData.SizeInPoints = 11f;
		appearance32.ForeColor = System.Drawing.Color.FromArgb(0, 51, 94);
		appearance32.Image = 41;
		appearance32.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance32.ImageBackground");
		appearance32.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnFunc13.HotTrackAppearance = appearance32;
		this.BtnFunc13.HotTracking = true;
		this.BtnFunc13.ImageList = this.imageList1;
		this.BtnFunc13.ImageSize = new System.Drawing.Size(24, 24);
		this.BtnFunc13.Location = new System.Drawing.Point(0, 567);
		this.BtnFunc13.Name = "BtnFunc13";
		this.BtnFunc13.Padding = new System.Drawing.Size(5, 0);
		appearance33.BorderColor = System.Drawing.Color.FromArgb(78, 151, 76);
		appearance33.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance33.FontData.SizeInPoints = 11f;
		appearance33.ForeColor = System.Drawing.Color.FromArgb(0, 51, 94);
		appearance33.Image = 40;
		appearance33.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance33.ImageBackground");
		appearance33.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnFunc13.PressedAppearance = appearance33;
		this.BtnFunc13.ShapeImage = (System.Drawing.Image)resources.GetObject("BtnFunc13.ShapeImage");
		this.BtnFunc13.ShowFocusRect = false;
		this.BtnFunc13.ShowOutline = false;
		this.BtnFunc13.Size = new System.Drawing.Size(160, 32);
		this.BtnFunc13.TabIndex = 39;
		this.BtnFunc13.TabStop = false;
		this.BtnFunc13.Text = "外掛程式";
		this.BtnFunc13.Click += new System.EventHandler(BtnFunc13_Click);
		appearance34.BackColor = System.Drawing.Color.White;
		appearance34.BackColor2 = System.Drawing.Color.Silver;
		appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance34.BorderColor = System.Drawing.Color.FromArgb(78, 151, 76);
		appearance34.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance34.FontData.SizeInPoints = 11f;
		appearance34.ForeColor = System.Drawing.Color.FromArgb(0, 51, 94);
		appearance34.Image = 0;
		appearance34.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance34.ImageBackground");
		appearance34.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance34.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnFunc1.Appearance = appearance34;
		this.BtnFunc1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
		this.BtnFunc1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.BtnFunc1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.BtnFunc1.FlatMode = true;
		this.BtnFunc1.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance35.BorderColor = System.Drawing.Color.FromArgb(78, 151, 76);
		appearance35.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance35.FontData.SizeInPoints = 11f;
		appearance35.ForeColor = System.Drawing.Color.FromArgb(0, 51, 94);
		appearance35.Image = 2;
		appearance35.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance35.ImageBackground");
		appearance35.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnFunc1.HotTrackAppearance = appearance35;
		this.BtnFunc1.HotTracking = true;
		this.BtnFunc1.ImageList = this.imageList1;
		this.BtnFunc1.ImageSize = new System.Drawing.Size(24, 24);
		this.BtnFunc1.Location = new System.Drawing.Point(0, 599);
		this.BtnFunc1.Name = "BtnFunc1";
		this.BtnFunc1.Padding = new System.Drawing.Size(5, 0);
		appearance36.BorderColor = System.Drawing.Color.FromArgb(78, 151, 76);
		appearance36.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance36.FontData.SizeInPoints = 11f;
		appearance36.ForeColor = System.Drawing.Color.FromArgb(0, 51, 94);
		appearance36.Image = 1;
		appearance36.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance36.ImageBackground");
		appearance36.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnFunc1.PressedAppearance = appearance36;
		this.BtnFunc1.ShapeImage = (System.Drawing.Image)resources.GetObject("BtnFunc1.ShapeImage");
		this.BtnFunc1.ShowFocusRect = false;
		this.BtnFunc1.ShowOutline = false;
		this.BtnFunc1.Size = new System.Drawing.Size(160, 32);
		this.BtnFunc1.TabIndex = 25;
		this.BtnFunc1.TabStop = false;
		this.BtnFunc1.Text = "系統維護";
		this.BtnFunc1.Click += new System.EventHandler(BtnFunc1_Click);
		this.pnModuleFlow.Controls.Add(this.linkModuleFlowMap);
		this.pnModuleFlow.Controls.Add(this.ultraPictureBox1);
		this.pnModuleFlow.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.pnModuleFlow.Location = new System.Drawing.Point(0, -1);
		this.pnModuleFlow.Name = "pnModuleFlow";
		this.pnModuleFlow.Size = new System.Drawing.Size(160, 28);
		this.pnModuleFlow.TabIndex = 52;
		this.linkModuleFlowMap.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.linkModuleFlowMap.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.linkModuleFlowMap.Location = new System.Drawing.Point(32, 5);
		this.linkModuleFlowMap.Name = "linkModuleFlowMap";
		this.linkModuleFlowMap.Size = new System.Drawing.Size(107, 16);
		this.linkModuleFlowMap.TabIndex = 16;
		((System.Windows.Forms.Label)this.linkModuleFlowMap).TabStop = true;
		this.linkModuleFlowMap.Text = "流程操作";
		this.linkModuleFlowMap.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkModuleFlowMap_LinkClicked);
		this.ultraPictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.ultraPictureBox1.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox1.Image = resources.GetObject("ultraPictureBox1.Image");
		this.ultraPictureBox1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraPictureBox1.Location = new System.Drawing.Point(9, 1);
		this.ultraPictureBox1.Name = "ultraPictureBox1";
		this.ultraPictureBox1.Size = new System.Drawing.Size(20, 20);
		this.ultraPictureBox1.TabIndex = 17;
		this.panel1.Controls.Add(this.linkLabel1);
		this.panel1.Controls.Add(this.PicBox);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 27);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(160, 28);
		this.panel1.TabIndex = 48;
		this.linkLabel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.linkLabel1.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.linkLabel1.Location = new System.Drawing.Point(32, 5);
		this.linkLabel1.Name = "linkLabel1";
		this.linkLabel1.Size = new System.Drawing.Size(107, 16);
		this.linkLabel1.TabIndex = 16;
		((System.Windows.Forms.Label)this.linkLabel1).TabStop = true;
		this.linkLabel1.Text = "資料庫管理及切換";
		this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
		this.PicBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.PicBox.BorderShadowColor = System.Drawing.Color.Empty;
		this.PicBox.Image = resources.GetObject("PicBox.Image");
		this.PicBox.ImageTransparentColor = System.Drawing.Color.White;
		this.PicBox.Location = new System.Drawing.Point(9, 1);
		this.PicBox.Name = "PicBox";
		this.PicBox.Size = new System.Drawing.Size(20, 20);
		this.PicBox.TabIndex = 17;
		appearance37.BackColor = System.Drawing.Color.White;
		appearance37.BackColor2 = System.Drawing.Color.Silver;
		appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance37.BorderColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance37.ForeColor = System.Drawing.Color.FromArgb(0, 51, 94);
		appearance37.Image = resources.GetObject("appearance37.Image");
		appearance37.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance37.ImageBackground");
		appearance37.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance37.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnMain1.Appearance = appearance37;
		this.BtnMain1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
		this.BtnMain1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.BtnMain1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.BtnMain1.FlatMode = true;
		this.BtnMain1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		appearance38.BorderColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance38.ForeColor = System.Drawing.Color.FromArgb(0, 51, 94);
		appearance38.Image = resources.GetObject("appearance38.Image");
		appearance38.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance38.ImageBackground");
		appearance38.ImageBackgroundOrigin = Infragistics.Win.ImageBackgroundOrigin.Client;
		this.BtnMain1.HotTrackAppearance = appearance38;
		this.BtnMain1.HotTracking = true;
		this.BtnMain1.ImageList = this.imageList1;
		this.BtnMain1.ImageSize = new System.Drawing.Size(24, 24);
		this.BtnMain1.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnMain1.Location = new System.Drawing.Point(0, 55);
		this.BtnMain1.Name = "BtnMain1";
		this.BtnMain1.Padding = new System.Drawing.Size(5, 0);
		appearance39.BorderColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance39.ForeColor = System.Drawing.Color.FromArgb(0, 51, 94);
		appearance39.Image = resources.GetObject("appearance39.Image");
		appearance39.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance39.ImageBackground");
		this.BtnMain1.PressedAppearance = appearance39;
		this.BtnMain1.ShapeImage = (System.Drawing.Image)resources.GetObject("BtnMain1.ShapeImage");
		this.BtnMain1.ShowFocusRect = false;
		this.BtnMain1.ShowOutline = false;
		this.BtnMain1.Size = new System.Drawing.Size(160, 32);
		this.BtnMain1.TabIndex = 47;
		this.BtnMain1.TabStop = false;
		this.BtnMain1.Text = "預算編製";
		this.BtnMain1.Click += new System.EventHandler(BtnMain1_Click);
		appearance40.BorderColor = System.Drawing.Color.FromArgb(167, 131, 60);
		appearance40.ForeColor = System.Drawing.Color.FromArgb(128, 99, 1);
		appearance40.Image = 3;
		appearance40.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance40.ImageBackground");
		appearance40.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance40.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnFunc5.Appearance = appearance40;
		this.BtnFunc5.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
		this.BtnFunc5.Cursor = System.Windows.Forms.Cursors.Hand;
		this.BtnFunc5.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.BtnFunc5.FlatMode = true;
		this.BtnFunc5.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance41.Image = 5;
		appearance41.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance41.ImageBackground");
		appearance41.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnFunc5.HotTrackAppearance = appearance41;
		this.BtnFunc5.HotTracking = true;
		this.BtnFunc5.ImageList = this.imageList1;
		this.BtnFunc5.ImageSize = new System.Drawing.Size(24, 24);
		this.BtnFunc5.Location = new System.Drawing.Point(0, 87);
		this.BtnFunc5.Name = "BtnFunc5";
		this.BtnFunc5.Padding = new System.Drawing.Size(5, 0);
		appearance42.Image = 4;
		appearance42.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance42.ImageBackground");
		appearance42.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnFunc5.PressedAppearance = appearance42;
		this.BtnFunc5.ShapeImage = (System.Drawing.Image)resources.GetObject("BtnFunc5.ShapeImage");
		this.BtnFunc5.ShowFocusRect = false;
		this.BtnFunc5.ShowOutline = false;
		this.BtnFunc5.Size = new System.Drawing.Size(160, 32);
		this.BtnFunc5.TabIndex = 46;
		this.BtnFunc5.TabStop = false;
		this.BtnFunc5.Text = "  專案目錄";
		this.BtnFunc5.Click += new System.EventHandler(BtnFunc5_Click);
		appearance43.BorderColor = System.Drawing.Color.FromArgb(167, 131, 60);
		appearance43.ForeColor = System.Drawing.Color.FromArgb(128, 99, 1);
		appearance43.Image = 9;
		appearance43.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance43.ImageBackground");
		appearance43.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance43.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnFunc3.Appearance = appearance43;
		this.BtnFunc3.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
		this.BtnFunc3.Cursor = System.Windows.Forms.Cursors.Hand;
		this.BtnFunc3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.BtnFunc3.FlatMode = true;
		this.BtnFunc3.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance44.Image = 11;
		appearance44.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance44.ImageBackground");
		appearance44.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnFunc3.HotTrackAppearance = appearance44;
		this.BtnFunc3.HotTracking = true;
		this.BtnFunc3.ImageList = this.imageList1;
		this.BtnFunc3.ImageSize = new System.Drawing.Size(24, 24);
		this.BtnFunc3.Location = new System.Drawing.Point(0, 119);
		this.BtnFunc3.Name = "BtnFunc3";
		this.BtnFunc3.Padding = new System.Drawing.Size(5, 0);
		appearance45.Image = 10;
		appearance45.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance45.ImageBackground");
		appearance45.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnFunc3.PressedAppearance = appearance45;
		this.BtnFunc3.ShapeImage = (System.Drawing.Image)resources.GetObject("BtnFunc3.ShapeImage");
		this.BtnFunc3.ShowFocusRect = false;
		this.BtnFunc3.ShowOutline = false;
		this.BtnFunc3.Size = new System.Drawing.Size(160, 32);
		this.BtnFunc3.TabIndex = 45;
		this.BtnFunc3.TabStop = false;
		this.BtnFunc3.Text = "  預算書編製";
		this.BtnFunc3.Click += new System.EventHandler(BtnFunc3_Click);
		appearance46.BackColor = System.Drawing.Color.White;
		appearance46.BackColor2 = System.Drawing.Color.Silver;
		appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance46.BorderColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance46.ForeColor = System.Drawing.Color.FromArgb(0, 51, 94);
		appearance46.Image = resources.GetObject("appearance46.Image");
		appearance46.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance46.ImageBackground");
		appearance46.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance46.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnMain3.Appearance = appearance46;
		this.BtnMain3.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
		this.BtnMain3.Cursor = System.Windows.Forms.Cursors.Hand;
		this.BtnMain3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.BtnMain3.FlatMode = true;
		this.BtnMain3.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		appearance47.BorderColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance47.ForeColor = System.Drawing.Color.FromArgb(0, 51, 94);
		appearance47.Image = resources.GetObject("appearance47.Image");
		appearance47.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance47.ImageBackground");
		appearance47.ImageBackgroundOrigin = Infragistics.Win.ImageBackgroundOrigin.Client;
		this.BtnMain3.HotTrackAppearance = appearance47;
		this.BtnMain3.HotTracking = true;
		this.BtnMain3.ImageList = this.imageList1;
		this.BtnMain3.ImageSize = new System.Drawing.Size(24, 24);
		this.BtnMain3.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnMain3.Location = new System.Drawing.Point(0, 151);
		this.BtnMain3.Name = "BtnMain3";
		this.BtnMain3.Padding = new System.Drawing.Size(5, 0);
		appearance48.BorderColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance48.ForeColor = System.Drawing.Color.FromArgb(0, 51, 94);
		appearance48.Image = resources.GetObject("appearance48.Image");
		appearance48.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance48.ImageBackground");
		this.BtnMain3.PressedAppearance = appearance48;
		this.BtnMain3.ShapeImage = (System.Drawing.Image)resources.GetObject("BtnMain3.ShapeImage");
		this.BtnMain3.ShowFocusRect = false;
		this.BtnMain3.ShowOutline = false;
		this.BtnMain3.Size = new System.Drawing.Size(160, 32);
		this.BtnMain3.TabIndex = 49;
		this.BtnMain3.TabStop = false;
		this.BtnMain3.Text = "投標編製";
		this.BtnMain3.Click += new System.EventHandler(BtnMain3_Click);
		appearance49.BorderColor = System.Drawing.Color.FromArgb(167, 131, 60);
		appearance49.ForeColor = System.Drawing.Color.FromArgb(128, 99, 1);
		appearance49.Image = 6;
		appearance49.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance49.ImageBackground");
		appearance49.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance49.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnFuncBidImport.Appearance = appearance49;
		this.BtnFuncBidImport.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
		this.BtnFuncBidImport.Cursor = System.Windows.Forms.Cursors.Hand;
		this.BtnFuncBidImport.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.BtnFuncBidImport.FlatMode = true;
		this.BtnFuncBidImport.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance50.Image = 8;
		appearance50.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance50.ImageBackground");
		appearance50.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnFuncBidImport.HotTrackAppearance = appearance50;
		this.BtnFuncBidImport.HotTracking = true;
		this.BtnFuncBidImport.ImageList = this.imageList1;
		this.BtnFuncBidImport.ImageSize = new System.Drawing.Size(24, 24);
		this.BtnFuncBidImport.Location = new System.Drawing.Point(0, 183);
		this.BtnFuncBidImport.Name = "BtnFuncBidImport";
		this.BtnFuncBidImport.Padding = new System.Drawing.Size(5, 0);
		appearance51.Image = 7;
		appearance51.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance51.ImageBackground");
		appearance51.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnFuncBidImport.PressedAppearance = appearance51;
		this.BtnFuncBidImport.ShapeImage = (System.Drawing.Image)resources.GetObject("BtnFuncBidImport.ShapeImage");
		this.BtnFuncBidImport.ShowFocusRect = false;
		this.BtnFuncBidImport.ShowOutline = false;
		this.BtnFuncBidImport.Size = new System.Drawing.Size(160, 32);
		this.BtnFuncBidImport.TabIndex = 51;
		this.BtnFuncBidImport.TabStop = false;
		this.BtnFuncBidImport.Text = "  標單轉入";
		this.BtnFuncBidImport.Click += new System.EventHandler(BtnFuncBidImport_Click);
		appearance52.BorderColor = System.Drawing.Color.FromArgb(167, 131, 60);
		appearance52.ForeColor = System.Drawing.Color.FromArgb(128, 99, 1);
		appearance52.Image = 6;
		appearance52.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance52.ImageBackground");
		appearance52.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance52.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnFunc4.Appearance = appearance52;
		this.BtnFunc4.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
		this.BtnFunc4.Cursor = System.Windows.Forms.Cursors.Hand;
		this.BtnFunc4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.BtnFunc4.FlatMode = true;
		this.BtnFunc4.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance53.Image = 8;
		appearance53.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance53.ImageBackground");
		appearance53.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnFunc4.HotTrackAppearance = appearance53;
		this.BtnFunc4.HotTracking = true;
		this.BtnFunc4.ImageList = this.imageList1;
		this.BtnFunc4.ImageSize = new System.Drawing.Size(24, 24);
		this.BtnFunc4.Location = new System.Drawing.Point(0, 215);
		this.BtnFunc4.Name = "BtnFunc4";
		this.BtnFunc4.Padding = new System.Drawing.Size(5, 0);
		appearance54.Image = 7;
		appearance54.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance54.ImageBackground");
		appearance54.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnFunc4.PressedAppearance = appearance54;
		this.BtnFunc4.ShapeImage = (System.Drawing.Image)resources.GetObject("BtnFunc4.ShapeImage");
		this.BtnFunc4.ShowFocusRect = false;
		this.BtnFunc4.ShowOutline = false;
		this.BtnFunc4.Size = new System.Drawing.Size(160, 32);
		this.BtnFunc4.TabIndex = 50;
		this.BtnFunc4.TabStop = false;
		this.BtnFunc4.Text = "  標單填寫";
		this.BtnFunc4.Click += new System.EventHandler(BtnFunc4_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.pnModuleFlow);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.BtnMain1);
		base.Controls.Add(this.BtnFunc5);
		base.Controls.Add(this.BtnFunc3);
		base.Controls.Add(this.BtnMain3);
		base.Controls.Add(this.BtnFuncBidImport);
		base.Controls.Add(this.BtnFunc4);
		base.Controls.Add(this.BtnMain4);
		base.Controls.Add(this.BtnFunc2);
		base.Controls.Add(this.BtnFunc8);
		base.Controls.Add(this.BtnFunc7);
		base.Controls.Add(this.BtnMain2);
		base.Controls.Add(this.BtnFunc9);
		base.Controls.Add(this.BtnFunc10);
		base.Controls.Add(this.BtnFunc6);
		base.Controls.Add(this.BtnFunc11);
		base.Controls.Add(this.BtnFunc12);
		base.Controls.Add(this.BtnFunc13);
		base.Controls.Add(this.BtnFunc1);
		base.Name = "FunctionButtons";
		base.Size = new System.Drawing.Size(160, 631);
		base.Load += new System.EventHandler(FunctionButtons_Load);
		this.pnModuleFlow.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
