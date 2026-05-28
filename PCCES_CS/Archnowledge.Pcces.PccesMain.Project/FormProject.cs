using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.CommonClass.Budget;
using Archnowledge.Pcces.DomainModule.Bid;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.DomainModule.Coms;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.DomainModule.LogicalBase;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.Budget;
using Archnowledge.Pcces.PccesMain.Budget.BudgetChange;
using Archnowledge.Pcces.PccesMain.Library;
using Archnowledge.Pcces.PccesMain.ShellLib;
using Archnowledge.Pcces.STDClass;
using AxThreed;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.Project;

public class FormProject : Form
{
	public enum ProjectFilterEnum
	{
		All,
		OnlyTemplate,
		OnlyAuthorized
	}

	private const string FileIni = "OptionSet.ini";

	private const string F_FunctionName = "ProjectManagement";

	private bool F_IsDirectOpenCNT = false;

	private string F_PID = "";

	private bool IsHasLoadedBudget = false;

	private int iAuthorityMSG_Count = 0;

	private string F_NewProjectCode = "";

	private LeftPanelMode PanelMode = LeftPanelMode.Open;

	private ArrayList ToolLists = new ArrayList();

	private ArrayList ToolParam = new ArrayList();

	private string AppLocation = "";

	private bool F_HasRegistered;

	private string F_UserID;

	private string F_UserName = "";

	private string F_ServerName = "localhost";

	private string F_KeyWord = "";

	private int GridCols = 15;

	private object[,] GridColsSquence;

	private DataTable DT1 = new DataTable();

	private UltraToolbarsManager ultraToolbarsManager1;

	private string IsNewProject = "";

	private string companyDB;

	private bool EnableCOMS = SysConfig.SysComsEnable;

	private bool OpenFormBudget = false;

	private ProjectFilterEnum SelectedProjectFilter = ProjectFilterEnum.All;

	private IContainer components;

	private Panel FormProject_Fill_Panel;

	private Panel panel2;

	private GridBudget gridProject;

	private Panel LeftPanel;

	private OnlineList onlineList1;

	public FunctionButtons functionButtons1;

	private Panel panel1;

	private Panel pnl_spliter;

	private UltraButton Btn_Splt;

	private AxSSPanel ssp_Lower;

	private AxSSPanel ssp_Bottom;

	private AxSSPanel ssp_Upper;

	private AxSSPanel ssp_Top;

	private Panel panel3;

	private UltraStatusBar ultraStatusBar1;

	private AxSSPanel ssp_GridCaption;

	private ImageList iglst_splt_Btn;

	private ImageList imageList2;

	private UltraLabel lblUseDatabase;

	private UltraToolbarsDockArea _FormProject_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormProject_Toolbars_Dock_Area_Bottom;

	private UltraToolbarsDockArea _FormProject_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormProject_Toolbars_Dock_Area_Right;

	private uccShowProject ShowProject;

	public bool _HasRegistered
	{
		get
		{
			return F_HasRegistered;
		}
		set
		{
			F_HasRegistered = value;
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

	public string _NewProjectCode
	{
		get
		{
			return F_NewProjectCode;
		}
		set
		{
			F_NewProjectCode = value;
		}
	}

	public FormProject()
	{
		InitializeComponent();
		F_PID = ConfigurationManager.AppSettings["PID"];
		GridCols = gridProject.Cols.Count;
		GridColsSquence = new object[GridCols, 10];
		CellStyle cs = gridProject.Styles.Add("img");
		cs.DataType = typeof(Image);
		cs.ImageAlign = ImageAlignEnum.CenterCenter;
		CellStyle cs2 = gridProject.Styles.Add("EditMode");
		cs2.DataType = typeof(Image);
		cs2.ImageAlign = ImageAlignEnum.RightCenter;
	}

	private void HideCols(bool IsHide)
	{
		gridProject.Cols["IsBud"].Visible = false;
		gridProject.Cols["IsBid"].Visible = false;
		gridProject.Cols["IsCNT"].Visible = false;
		gridProject.Cols["IsCanDelete"].Visible = false;
		gridProject.Cols["Template"].Visible = false;
		gridProject.Cols["BudEst"].Visible = false;
		gridProject.Cols["BudQuote"].Visible = false;
		gridProject.Cols["IsBudEst"].Visible = false;
		gridProject.Cols["IsBudQuote"].Visible = false;
		gridProject.Cols["BudEstAuth"].Visible = false;
		gridProject.Cols["BudQuoteAuth"].Visible = false;
	}

	private void RememberColsProps()
	{
		for (int i = 0; i < GridCols; i++)
		{
			GridColsSquence[i, 0] = gridProject.Cols[i].Name;
			GridColsSquence[i, 1] = gridProject.Cols[i].Caption;
			GridColsSquence[i, 2] = gridProject.Cols[i].Width;
			if (gridProject.Cols[i].Name == "BUD" || gridProject.Cols[i].Name == "BID")
			{
				GridColsSquence[i, 3] = typeof(Image);
			}
			else
			{
				GridColsSquence[i, 3] = gridProject.Cols[i].DataType;
			}
			GridColsSquence[i, 4] = gridProject.Cols[i].Visible;
			GridColsSquence[i, 5] = gridProject.Cols[i].Format;
			GridColsSquence[i, 6] = gridProject.Cols[i].AllowEditing;
			GridColsSquence[i, 7] = gridProject.Cols[i].TextAlign;
			GridColsSquence[i, 8] = gridProject.Cols[i].AllowDragging;
			GridColsSquence[i, 9] = gridProject.Cols[i].AllowResizing;
		}
	}

	private void SetGridColumn()
	{
		for (int i = 0; i < GridCols; i++)
		{
			gridProject.Cols[i].Name = (string)GridColsSquence[i, 0];
			gridProject.Cols[i].Caption = (string)GridColsSquence[i, 1];
			gridProject.Cols[i].Width = (int)GridColsSquence[i, 2];
			gridProject.Cols[i].DataType = (Type)GridColsSquence[i, 3];
			gridProject.Cols[i].Visible = (bool)GridColsSquence[i, 4];
			gridProject.Cols[i].Format = (string)GridColsSquence[i, 5];
			gridProject.Cols[i].AllowEditing = (bool)GridColsSquence[i, 6];
			gridProject.Cols[i].TextAlign = (TextAlignEnum)GridColsSquence[i, 7];
			gridProject.Cols[i].AllowDragging = (bool)GridColsSquence[i, 8];
			gridProject.Cols[i].AllowResizing = (bool)GridColsSquence[i, 9];
		}
	}

	private void Btn_Splt_Click(object sender, EventArgs e)
	{
		if (LeftPanel.Width == 0)
		{
			LeftPanel.Width = 160;
			PanelMode = LeftPanelMode.Open;
			Btn_Splt.Appearance.ImageBackground = iglst_splt_Btn.Images[0];
		}
		else
		{
			LeftPanel.Width = 0;
			PanelMode = LeftPanelMode.Close;
			Btn_Splt.Appearance.ImageBackground = iglst_splt_Btn.Images[2];
		}
	}

	private void FormProject_Resize(object sender, EventArgs e)
	{
		int TotalH = pnl_spliter.Height;
		int iHeight = (TotalH - 3 - 3 - 57) / 2;
		ssp_Upper.Height = iHeight;
		ssp_Lower.Height = iHeight;
	}

	private void Btn_Splt_MouseEnter(object sender, EventArgs e)
	{
		if (PanelMode == LeftPanelMode.Open)
		{
			Btn_Splt.Appearance.ImageBackground = iglst_splt_Btn.Images[1];
		}
		else
		{
			Btn_Splt.Appearance.ImageBackground = iglst_splt_Btn.Images[3];
		}
	}

	private void Btn_Splt_MouseLeave(object sender, EventArgs e)
	{
		if (PanelMode == LeftPanelMode.Open)
		{
			Btn_Splt.Appearance.ImageBackground = iglst_splt_Btn.Images[0];
		}
		else
		{
			Btn_Splt.Appearance.ImageBackground = iglst_splt_Btn.Images[2];
		}
	}

	private void SetColsEditSymbol()
	{
		for (int i = 1; i < gridProject.Cols.Count; i++)
		{
			if (gridProject.Cols[i].AllowEditing)
			{
				CellRange rg = gridProject.GetCellRange(0, i);
				rg.Style = gridProject.Styles["EditMode"];
				rg.Image = imageList2.Images[2];
			}
		}
	}

	public void GetNewData()
	{
		Archnowledge.Pcces.DomainModule.General.PubProject pubProject = new Archnowledge.Pcces.DomainModule.General.PubProject();
		DataSet ds = pubProject.GetProjectList(F_UserID);
		DT1 = ds.Tables[0];
	}

	public void BindDataToGrid()
	{
		BindDataToGrid(SelectedProjectFilter);
	}

	public void BindDataToGrid(ProjectFilterEnum ProjectFilter)
	{
		SelectedProjectFilter = ProjectFilter;
		lock (this)
		{
			string RecentFileBud = CommonMethods.GetIniValue("RecentFile", "BUDProject");
			string RecentFileBid = CommonMethods.GetIniValue("RecentFile", "BIDProject");
			string RecentFileCnt = CommonMethods.GetIniValue("RecentFile", "CNTProject");
			gridProject.Visible = false;
			RememberColsProps();
			gridProject.Clear(ClearFlags.All);
			gridProject.Select(0, 0);
			SetGridColumn();
			gridProject.Visible = true;
			ultraStatusBar1.Panels[0].Text = "資料筆數：" + DT1.Rows.Count;
			CellStyle CS1 = gridProject.Styles.Add("NoProjectAuth");
			CellStyle CSBUD = gridProject.Styles.Add("RecentBUD");
			CellStyle CSBID = gridProject.Styles.Add("RecentBID");
			CellStyle CSCNT = gridProject.Styles.Add("RecentCNT");
			CellStyle CSNOTPCCES = gridProject.Styles.Add("NotPCCES");
			CellStyle CSTEMPLATE = gridProject.Styles.Add("TEMPLATE");
			CSBUD.BackColor = Color.Moccasin;
			CSBID.BackColor = Color.Moccasin;
			CSCNT.BackColor = Color.Moccasin;
			CSNOTPCCES.BackColor = Color.LightGoldenrodYellow;
			CSNOTPCCES.ForeColor = Color.Red;
			CSTEMPLATE.BackColor = Color.LightYellow;
			CS1.ForeColor = Color.Gray;
			DataView dv = new DataView(DT1);
			switch (SelectedProjectFilter)
			{
			case ProjectFilterEnum.All:
				dv.RowFilter = "1=1";
				break;
			case ProjectFilterEnum.OnlyTemplate:
				dv.RowFilter = "IsTemplate='Y'";
				break;
			case ProjectFilterEnum.OnlyAuthorized:
				dv.RowFilter = "Auth='Y'";
				break;
			default:
				dv.RowFilter = "1=1";
				break;
			}
			string additionalFilter = string.Empty;
			if (!ShowProject.ShowBudType4)
			{
				additionalFilter += " AND (BudType IS NULL OR BudType <> '4')";
			}
			if (!ShowProject.ShowBidType3)
			{
				additionalFilter += " AND (BidType IS NULL OR BidType <> '3')";
			}
			dv.RowFilter += additionalFilter;
			ArrayList aArr = new ArrayList();
			aArr.Add(F_UserID);
			aArr.Add("預算編輯--讀取目前預算編輯類型(預算書或契約書)");
			Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
			PROJ.ps_srckind = "bud";
			gridProject.Rows.Count = dv.Count + 1;
			for (int i = 0; i < dv.Count; i++)
			{
				DataRowView drv = dv[i];
				gridProject[i + 1, "ProjectCode"] = PubTools.Obj2Str(drv["projectCode"]);
				gridProject[i + 1, "CName"] = PubTools.Obj2Str(drv["projCName"]);
				gridProject[i + 1, "EName"] = PubTools.Obj2Str(drv["projEName"]);
				gridProject[i + 1, "Address"] = PubTools.Obj2Str(drv["projAddress"]);
				gridProject[i + 1, "mainProj"] = PubTools.Obj2Str(drv["mainProj"]);
				gridProject[i + 1, "ProjectCodeAlias"] = PubTools.Obj2Str(drv["projectCodeAlias"]);
				gridProject[i + 1, "ProjectMemo"] = PubTools.Obj2Str(drv["projectMemo"]);
				gridProject[i + 1, "ProjectRemark"] = ((PubTools.Obj2Str(drv["bidRemark"]) != "") ? ("標單" + PubTools.Obj2Str(drv["bidRemark"])) : PubTools.Obj2Str(drv["bidRemark"])) + ((PubTools.Obj2Str(drv["budRemark"]) != "") ? ("預算" + PubTools.Obj2Str(drv["budRemark"])) : PubTools.Obj2Str(drv["budRemark"]));
				gridProject[i + 1, "IsCanDelete"] = true;
				gridProject[i + 1, "IsBudEst"] = ArchConvert.Obj2Bool(drv["BudEst"]);
				gridProject[i + 1, "BudEstAuth"] = ArchConvert.Obj2Bool(drv["BudEstAuth"]);
				gridProject[i + 1, "IsBudQuote"] = ArchConvert.Obj2Bool(drv["BudQuote"]);
				gridProject[i + 1, "BudQuoteAuth"] = ArchConvert.Obj2Bool(drv["BudQuoteAuth"]);
				if (drv["Istemplate"].ToString().ToUpper().Trim() == "Y")
				{
					CellRange rg = gridProject.GetCellRange(i + 1, gridProject.Cols["Istemplate"].SafeIndex);
					rg.Style = gridProject.Styles["img"];
					rg.Image = imageList2.Images[8];
					gridProject.Rows[i + 1].Style = gridProject.Styles["TEMPLATE"];
					gridProject[i + 1, "template"] = true;
				}
				else
				{
					gridProject[i + 1, "Istemplate"] = "";
					gridProject[i + 1, "template"] = false;
				}
				try
				{
					gridProject[i + 1, "BudFileName"] = PubTools.Obj2Str(drv["BudFileName"]);
					gridProject[i + 1, "BidFileName"] = PubTools.Obj2Str(drv["BidFileName"]);
				}
				catch (Exception ex)
				{
					CommonMethods.LogFile("Pcces46", "M", "Project.FormProject.cs" + ex.Message);
				}
				if (PubTools.Str2Int(drv["CNTCount"].ToString()) > 0)
				{
					gridProject[i + 1, "IsCNT"] = true;
					CellRange rg = gridProject.GetCellRange(i + 1, gridProject.Cols["CNT"].SafeIndex);
					rg.Style = gridProject.Styles["img"];
					rg.Image = imageList2.Images[0];
					if (drv["projectCode"].ToString().Trim() == RecentFileCnt)
					{
						CellRange rgCNT = gridProject.GetCellRange(i + 1, gridProject.Cols["CNT"].SafeIndex);
						CSCNT.ImageAlign = ImageAlignEnum.CenterCenter;
						rgCNT.Style = CSCNT;
					}
				}
				else
				{
					gridProject[i + 1, "IsCNT"] = false;
					CellRange rg = gridProject.GetCellRange(i + 1, gridProject.Cols["CNT"].SafeIndex);
					rg.Style = gridProject.Styles["img"];
					rg.Image = imageList2.Images[4];
				}
				int iCNTCount = PubTools.Str2Int(drv["CNTCount"]);
				int iBUDCount = PubTools.Str2Int(drv["BUDCount"]);
				int ibudItemsCount = PubTools.Str2Int(drv["budItemsCount"]);
				if (drv["bud"].ToString().Length > 0 && ((ibudItemsCount > 0 && iCNTCount == 0) || (ibudItemsCount > 0 && iBUDCount > 0)))
				{
					gridProject[i + 1, "IsBud"] = true;
					CellRange rg = gridProject.GetCellRange(i + 1, gridProject.Cols["BUD"].SafeIndex);
					rg.Style = gridProject.Styles["img"];
					if (drv["Auth"].ToString() == "Y")
					{
						rg.Image = imageList2.Images[0];
						PROJ.ps_projectCode = PubTools.Obj2Str(drv["projectCode"]);
					}
					else
					{
						rg.Image = imageList2.Images[5];
					}
					if (drv["projectCode"].ToString().Trim() == RecentFileBud)
					{
						CellRange rgBID = gridProject.GetCellRange(i + 1, gridProject.Cols["BUD"].SafeIndex);
						rgBID.Style = CSBUD;
					}
					if (SysConfig.SysChangeManagement)
					{
						CellRange rgBudEst = gridProject.GetCellRange(i + 1, gridProject.Cols["BudEst"].SafeIndex);
						CellRange rgBudQuote = gridProject.GetCellRange(i + 1, gridProject.Cols["BudQuote"].SafeIndex);
						rgBudEst.Style = gridProject.Styles["img"];
						rgBudEst.Image = imageList2.Images[4];
						rgBudQuote.Style = gridProject.Styles["img"];
						rgBudQuote.Image = imageList2.Images[4];
						if (drv["BudEst"].ToString() == "Y")
						{
							if (drv["BudEstAuth"].ToString() == "Y")
							{
								rgBudEst.Image = imageList2.Images[0];
							}
							else
							{
								rgBudEst.Image = imageList2.Images[5];
							}
						}
						if (drv["BudQuote"].ToString() == "Y")
						{
							if (drv["BudQuoteAuth"].ToString() == "Y")
							{
								rgBudQuote.Image = imageList2.Images[0];
							}
							else
							{
								rgBudQuote.Image = imageList2.Images[5];
							}
						}
					}
				}
				else
				{
					gridProject[i + 1, "IsBud"] = false;
					CellRange rg = gridProject.GetCellRange(i + 1, gridProject.Cols["BUD"].SafeIndex);
					rg.Style = gridProject.Styles["img"];
					rg.Image = imageList2.Images[3];
				}
				if (drv["bid"].ToString().Length > 0)
				{
					gridProject[i + 1, "IsBid"] = true;
					CellRange rg = gridProject.GetCellRange(i + 1, gridProject.Cols["BID"].SafeIndex);
					rg.Style = gridProject.Styles["img"];
					if (drv["Auth"].ToString() == "Y")
					{
						rg.Image = imageList2.Images[1];
					}
					else
					{
						rg.Image = imageList2.Images[5];
					}
					if (drv["projectCode"].ToString().Trim() == RecentFileBid)
					{
						CellRange rgBID = gridProject.GetCellRange(i + 1, gridProject.Cols["BID"].SafeIndex);
						rgBID.Style = CSBID;
					}
					if (drv["Auth"].ToString() == "Y" && PubTools.Str2DateTime(drv["CloseBidDate"]) != Convert.ToDateTime("1800/1/1"))
					{
						rg.Image = imageList2.Images[6];
						if (EnableCOMS && ArchConvert.Obj2Int(drv["BidType"]) == 1)
						{
							rg.Image = imageList2.Images[7];
						}
					}
				}
				else
				{
					gridProject[i + 1, "IsBid"] = false;
					CellRange rg = gridProject.GetCellRange(i + 1, gridProject.Cols["BID"].SafeIndex);
					rg.Style = gridProject.Styles["img"];
					rg.Image = imageList2.Images[4];
				}
				if (drv["Auth"].ToString() == "N")
				{
					gridProject[i + 1, "IsCanDelete"] = false;
					gridProject.Rows[i + 1].Style = gridProject.Styles["NoProjectAuth"];
				}
				if (gridProject[i + 1, "ProjectRemark"].ToString().Trim() != "")
				{
					gridProject.Rows[i + 1].Style = gridProject.Styles["NotPCCES"];
				}
			}
			GC.Collect();
			SetColsEditSymbol();
			FormProject_Resize(null, null);
		}
	}

	private void DoMenuAction(string MenuID)
	{
		switch (MenuID)
		{
		case "PopNewProject":
		case "mnuPopNew":
			ExecuteNewProject("0");
			break;
		case "mnuNewProject":
			ExecuteNewProject("1");
			break;
		case "mnuBindBid":
			ExecuteNewProject("5");
			break;
		case "mnuDelProject":
			ExecuteDeleteProject();
			break;
		case "mnuDelBudProject":
			DoDeleteThisBDGT("BUD");
			break;
		case "mnuDelBidProject":
			DoDeleteThisBDGT("BID");
			break;
		case "mnuCapFind":
			break;
		case "mnuKeyword":
			break;
		case "mnuGo":
			Do_ToolBarFind();
			break;
		case "Popup1":
			break;
		case "mnuImport":
			Do_ImportFile();
			break;
		case "mnuProjectExit":
			CloseThisForm();
			break;
		case "mnuExcleImport":
			ExecuteNewProject("4");
			break;
		case "mnuSplit":
			ExecuteNewProject("3");
			break;
		case "mnuClone":
			ExecuteProjectClone();
			break;
		case "mnuUpdateList":
		{
			FormUpdateInfo FM_UPDINFO = new FormUpdateInfo();
			FM_UPDINFO.ShowDialog();
			FM_UPDINFO.Close();
			FM_UPDINFO.Dispose();
			FM_UPDINFO = null;
			break;
		}
		case "mnuOnlyPower":
			BindDataToGrid(ProjectFilterEnum.OnlyAuthorized);
			break;
		case "mnuTemplate":
			CheckIstemplate("Y");
			GetNewData();
			BindDataToGrid();
			break;
		case "mnuCancelTemplate":
			CheckIstemplate("N");
			GetNewData();
			BindDataToGrid();
			break;
		case "mnuOnlyTemplate":
			BindDataToGrid(ProjectFilterEnum.OnlyTemplate);
			break;
		case "mnuViewAll":
			BindDataToGrid(ProjectFilterEnum.All);
			break;
		case "mnuBidToBud":
			ExecuteProjectBidToBud();
			break;
		case "CopyBidToCompanyDB":
			CopyBidToCompanyDB();
			break;
		}
	}

	private void ExecuteProjectClone()
	{
		try
		{
			FormProjectClone FM_PRJCLN = new FormProjectClone();
			FM_PRJCLN._UserID = F_UserID;
			FM_PRJCLN._OldProjectCode = gridProject[gridProject.Row, "ProjectCode"].ToString().Trim();
			FM_PRJCLN._OldProjectName = gridProject[gridProject.Row, "CName"].ToString().Trim();
			FM_PRJCLN._OldProjectNameE = gridProject[gridProject.Row, "EName"].ToString().Trim();
			FM_PRJCLN._OldProjectAddr = gridProject[gridProject.Row, "Address"].ToString().Trim();
			FM_PRJCLN._IsBid = (bool)gridProject[gridProject.Row, "IsBid"];
			FM_PRJCLN._IsBud = (bool)gridProject[gridProject.Row, "IsBud"];
			FM_PRJCLN.Owner = this;
			if (FM_PRJCLN.ShowDialog() == DialogResult.OK)
			{
				GetNewData();
				BindDataToGrid();
				LocateToSpecificRow();
			}
			FM_PRJCLN.Close();
			FM_PRJCLN.Dispose();
			FM_PRJCLN = null;
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Project.FormProject.cs" + ex.Message);
			MessageBox.Show(this, "請先選取一筆專案", "警示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void ExecuteProjectBidToBud()
	{
		try
		{
			FormProjectBidToBud FM_PRJCLN = new FormProjectBidToBud();
			FM_PRJCLN._UserID = F_UserID;
			FM_PRJCLN._OldProjectCode = gridProject[gridProject.Row, "ProjectCode"].ToString().Trim();
			FM_PRJCLN._OldProjectName = gridProject[gridProject.Row, "CName"].ToString().Trim();
			FM_PRJCLN._OldProjectNameE = gridProject[gridProject.Row, "EName"].ToString().Trim();
			FM_PRJCLN._OldProjectAddr = gridProject[gridProject.Row, "Address"].ToString().Trim();
			FM_PRJCLN._IsBid = (bool)gridProject[gridProject.Row, "IsBid"];
			FM_PRJCLN._IsBud = (bool)gridProject[gridProject.Row, "IsBud"];
			FM_PRJCLN.Owner = this;
			if (FM_PRJCLN.ShowDialog() == DialogResult.OK)
			{
				GetNewData();
				BindDataToGrid();
				LocateToSpecificRow();
			}
			FM_PRJCLN.Close();
			FM_PRJCLN.Dispose();
			FM_PRJCLN = null;
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Project.FormProject.cs" + ex.Message);
			MessageBox.Show(this, "請先選取一筆專案", "警示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void CloseThisForm()
	{
		string sWarning = "確定要結束專案目錄 ?";
		if (MessageBox.Show(this, sWarning, "專案目錄", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			(base.ParentForm as frmPccesMain).LeftPanel.Width = 160;
			Close();
		}
	}

	private void Do_ImportFile()
	{
		if (!DBClass.ChkAuthority(F_UserID, "F00500010002"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F00500010002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		formNewProjectWizard FM_NEW_PROJ_WZD = new formNewProjectWizard();
		FM_NEW_PROJ_WZD._UserID = F_UserID;
		FM_NEW_PROJ_WZD._IniMode = "2";
		if (FM_NEW_PROJ_WZD.ShowDialog(this) == DialogResult.OK)
		{
			GetNewData();
			BindDataToGrid();
			LocateToSpecificRow();
		}
		FM_NEW_PROJ_WZD.Close();
		FM_NEW_PROJ_WZD.Dispose();
		FM_NEW_PROJ_WZD = null;
		GC.Collect();
	}

	public void ExecuteNewProject(string sMode)
	{
		ExecuteNewProject(sMode, InitCreateProject: true);
	}

	public void ExecuteNewProject(string sMode, bool InitCreateProject)
	{
		if (sMode == "1" && !DBClass.ChkAuthority(F_UserID, "F00500010001"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F00500010001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (sMode == "3" && !DBClass.ChkAuthority(F_UserID, "F00500010003"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F00500010003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (sMode == "4" && !DBClass.ChkAuthority(F_UserID, "F00500010004"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F00500010004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (sMode == "5" && !DBClass.ChkAuthority(F_UserID, "F00500010005"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F00500010005") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		formNewProjectWizard FM_NEW_PROJ_WZD = new formNewProjectWizard();
		FM_NEW_PROJ_WZD._UserID = F_UserID;
		FM_NEW_PROJ_WZD._IniMode = sMode;
		FM_NEW_PROJ_WZD._InitCreateProject = InitCreateProject;
		if (FM_NEW_PROJ_WZD.ShowDialog(this) == DialogResult.OK)
		{
			GetNewData();
			BindDataToGrid();
			LocateToSpecificRow();
		}
		FM_NEW_PROJ_WZD.Close();
		FM_NEW_PROJ_WZD.Dispose();
		FM_NEW_PROJ_WZD = null;
		GC.Collect();
	}

	private int SelectedItems()
	{
		int RetV = 0;
		for (int i = 1; i < gridProject.Rows.Count; i++)
		{
			if (gridProject.Rows[i].Selected)
			{
				RetV++;
			}
		}
		return RetV;
	}

	private void ExecuteDeleteProject()
	{
		if (!DBClass.ChkAuthority(F_UserID, "F0050002"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F0050002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		int iSels = SelectedItems();
		if (iSels <= 0)
		{
			MessageBox.Show(this, "請先選定要刪除的專案！", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (SysConfig.SysComsEnable)
		{
			for (int i = 1; i < gridProject.Rows.Count; i++)
			{
				if (gridProject.Rows[i].Selected && !IsBudgetCanDelete(gridProject[i, "ProjectCode"].ToString().Trim()))
				{
					MessageBox.Show("專案" + gridProject[i, "ProjectCode"].ToString().Trim() + "已進入執行預算,不可刪除!");
					return;
				}
			}
		}
		string StrProjectCode = gridProject[gridProject.Row, "ProjectCode"].ToString();
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		string MsgStr = "確定要刪除選定的 " + iSels + " 筆專案嗎?\n\n這個動作將會將相關的資料一併刪除\n例如:預算書、標單、專案資源項目等...";
		int iCannotDeleteItems = 0;
		if (MessageBox.Show(this, MsgStr, "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			for (int i = 1; i < gridProject.Rows.Count; i++)
			{
				if (gridProject.Rows[i].Selected && (bool)gridProject[i, "IsCanDelete"])
				{
					string projectCode = gridProject[i, "ProjectCode"].ToString().Trim();
					ExecResult ER = DeleteProject(projectCode, "BUD");
					if (ER.ReturnCode == 0)
					{
						ER = DeleteProject(projectCode, "BID");
						if (ER.ReturnCode == 0)
						{
							Archnowledge.Pcces.DomainModule.General.PubProject pubProject = new Archnowledge.Pcces.DomainModule.General.PubProject();
							pubProject.DeletePubProject(projectCode);
						}
					}
				}
				else if (gridProject.Rows[i].Selected && !(bool)gridProject[i, "IsCanDelete"])
				{
					iCannotDeleteItems++;
				}
			}
		}
		GetNewData();
		BindDataToGrid();
		gridProject.Row = -1;
		DBCLS = null;
		if (iCannotDeleteItems > 0)
		{
			MessageBox.Show(this, "您剛才選擇要刪除的專案中，有" + iCannotDeleteItems + "筆沒有權限。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void DoDeleteThisBDGT(string srckind)
	{
		string sQuest = ((srckind == "BUD") ? "確定刪除此預算書 ?" : "確定刪除此投標單 ?");
		int iSels = SelectedItems();
		if (iSels <= 0)
		{
			MessageBox.Show(this, "請先選定要刪除的專案!!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (SysConfig.SysComsEnable)
		{
			for (int i = 1; i < gridProject.Rows.Count; i++)
			{
				if (gridProject.Rows[i].Selected && !IsBudgetCanDelete(gridProject[i, "ProjectCode"].ToString().Trim()))
				{
					MessageBox.Show("專案" + gridProject[i, "ProjectCode"].ToString().Trim() + "已進入執行預算,不可刪除!");
					return;
				}
			}
		}
		if (MessageBox.Show(this, sQuest, "刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
		{
			return;
		}
		int iCannotDeleteItems = 0;
		for (int i = 1; i < gridProject.Rows.Count; i++)
		{
			if (gridProject.Rows[i].Selected && (bool)gridProject[i, "IsCanDelete"])
			{
				string projectCode = gridProject[i, "ProjectCode"].ToString().Trim();
				DeleteProject(gridProject[i, "ProjectCode"].ToString().Trim(), srckind);
			}
			else if (gridProject.Rows[i].Selected && !(bool)gridProject[i, "IsCanDelete"])
			{
				iCannotDeleteItems++;
			}
		}
		GetNewData();
		BindDataToGrid();
		if (iCannotDeleteItems > 0)
		{
			MessageBox.Show(this, "您剛才選擇要刪除的專案中，有" + iCannotDeleteItems + "筆沒有權限。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private ExecResult DeleteProject(string projectCode, string srcKind)
	{
		Archnowledge.Pcces.DomainModule.LogicalBase.Project project = ((!(srcKind.ToUpper() == "BUD")) ? ((Archnowledge.Pcces.DomainModule.LogicalBase.Project)new BidProject()) : ((Archnowledge.Pcces.DomainModule.LogicalBase.Project)new BudProject()));
		ExecResult ER = project.RemoveProject(projectCode);
		if (ER.ReturnCode == 0)
		{
			project = ((!(srcKind.ToUpper() == "BUD")) ? ((Archnowledge.Pcces.DomainModule.LogicalBase.Project)new BudProject()) : ((Archnowledge.Pcces.DomainModule.LogicalBase.Project)new BidProject()));
			ArrayList aArr = new ArrayList();
			aArr.Add(F_UserID);
			aArr.Add("專案目錄");
			Archnowledge.Pcces.BUDClass.Project theProject = new Archnowledge.Pcces.BUDClass.Project(aArr);
			theProject.ps_srckind = srcKind.ToUpper();
			theProject.DeleProjTmp(projectCode);
			if (!project.ProjectCodeExists(projectCode))
			{
				AddOnDownLoad addOnDownLoad = new AddOnDownLoad();
				ER = addOnDownLoad.RemoveAddOnDownloadFilesByProjectCode(projectCode, F_UserID);
			}
		}
		if (ER.ReturnCode != 0)
		{
			ER.Message = "刪除專案" + projectCode + "失敗，錯誤訊息如下\n" + ER.Message;
			MessageBox.Show(this, ER.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		return ER;
	}

	private void CopyBidToCompanyDB()
	{
		if (gridProject.SelectedRowCount == 0)
		{
			MessageBox.Show(this, "請先選取一筆專案！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (!(bool)gridProject[gridProject.Row, "IsBid"])
		{
			MessageBox.Show(this, "此專案無標單！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (companyDB == string.Empty)
		{
			MessageBox.Show(this, "公司資料庫不存在，請先新增公司資料庫！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		Cursor = Cursors.WaitCursor;
		ChgStru chgStru = new ChgStru();
		chgStru.ModifyDatabaseStructure(companyDB);
		string sourceProjectCode = gridProject[gridProject.Row, "ProjectCode"].ToString();
		string targetProjectCode = GetAvailableProjectCode(sourceProjectCode, companyDB);
		SysUser sysUser = new SysUser();
		string databaseInUse = sysUser.GetSysUserDatabaseName(F_UserID);
		BidProject bidProject = new BidProject();
		bidProject.CopyBidProjectToAnotherDB(databaseInUse, companyDB, sourceProjectCode, targetProjectCode);
		ProjAuthority projAuthority = new ProjAuthority();
		projAuthority.AddProjAuthorityByUserID(targetProjectCode, F_UserID, companyDB);
		MessageBox.Show(this, "標單 " + targetProjectCode + " 於公司資料庫新增成功！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		Cursor = Cursors.Default;
	}

	private string GetAvailableProjectCode(string projectCode, string companyDB)
	{
		Archnowledge.Pcces.DomainModule.General.PubProject pubProject = new Archnowledge.Pcces.DomainModule.General.PubProject();
		if (!pubProject.ProjectCodeExists(projectCode, companyDB))
		{
			return projectCode;
		}
		string postfix = "01";
		string tempProjectCode = projectCode + "-" + postfix;
		while (true)
		{
			bool flag = true;
			if (!pubProject.ProjectCodeExists(tempProjectCode, companyDB))
			{
				break;
			}
			postfix = CommonMethods.IncrementPostfix(postfix);
			tempProjectCode = projectCode + "-" + postfix;
		}
		return tempProjectCode;
	}

	private void FormProject_Load(object sender, EventArgs e)
	{
		AppLocation = AppDomain.CurrentDomain.BaseDirectory;
		LoadSettings();
		FormProject_Resize(null, null);
		string sHideCols = CommonMethods.GetDebugValue("Project", "HideCols");
		HideCols(Convert.ToBoolean((sHideCols == "") ? "True" : sHideCols));
		base.ParentForm.Text = "PCCES Win 4.3 【專案目錄】";
		functionButtons1._UserID = F_UserID;
		functionButtons1._UserName = F_UserName;
		functionButtons1._ServerName = F_ServerName;
		functionButtons1._CurrOpenMode = FunctionOpenMode.Budget;
		functionButtons1._ActiveFunction = "PROJECT";
		onlineList1._UserID = F_UserID;
		onlineList1._UserName = F_UserName;
		onlineList1._ServerName = F_ServerName;
		onlineList1._FunctionName = "ProjectManagement";
		onlineList1._HasRegistered = F_HasRegistered;
		onlineList1.Connect();
		SysUser oSysUser = new SysUser();
		lblUseDatabase.Text = "目前資料庫：" + oSysUser.GetSysUserDatabaseDesc(F_UserID);
		try
		{
			if (F_PID != null && F_PID.Trim() == "Z14AC1100")
			{
				gridProject.Cols["ProjectCodeAlias"].Caption = "動支單號";
				gridProject.Cols["ProjectCode"].Caption = "工程號/執行號";
			}
			else
			{
				gridProject.Cols["ProjectCodeAlias"].Caption = "工程別號";
				gridProject.Cols["ProjectCode"].Caption = "工程代碼";
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Project.FormProject.cs" + ex.Message);
		}
		ShowProject.Visible = EnableCOMS;
		ultraToolbarsManager1.Tools["mnuBidToBud"].SharedProps.Visible = PubTools.Str2Boolean(CommonMethods.GetIniValue("COMS", "IsBIDtoBUD"));
		UserDefined userDefined = new UserDefined();
		companyDB = userDefined.GetPccesCompanyDB();
		if (ArchConvert.Obj2Bool(ConfigurationManager.AppSettings["EnableCompanyDB"]) && companyDB != oSysUser.GetSysUserDatabaseName(F_UserID))
		{
			ultraToolbarsManager1.Tools["CopyBidToCompanyDB"].SharedProps.Visible = true;
		}
		if (SysConfig.SysEnableCostEstAndQuotation)
		{
			gridProject.Cols["BudEst"].Visible = true;
			gridProject.Cols["BudQuote"].Visible = true;
		}
		if (SysConfig.SysSingleEditLockMode)
		{
		}
		RememberColsProps();
		GetNewData();
		BindDataToGrid();
		ProcessAddOn();
		gridProject.Col = 0;
	}

	private void LoadSettings()
	{
		try
		{
			Application.DoEvents();
			GetIniSetting();
			Application.DoEvents();
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Project.FormProject.cs" + ex.Message);
		}
	}

	private void GetIniSetting()
	{
		GridPropertySetting.LoadGridProperty(F_UserID, base.Name, gridProject);
		string sAllowIsTooltip = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "CommonData", "AllowIsTooltip");
		if (sAllowIsTooltip.ToUpper() == "TRUE")
		{
			gridProject.ShowToolTipOnNarrowColumn = false;
		}
		else
		{
			gridProject.ShowToolTipOnNarrowColumn = true;
		}
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		DoMenuAction(e.Tool.Key);
	}

	private void AutoRestoreBudgetToReplaceContract(string projectCode, PccesFormAction FormActionName)
	{
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("預算編輯--設定目前預算編輯類型(預算書或契約書)");
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PROJ.ps_projectCode = projectCode.Trim();
		PROJ.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		bool IsBudTmpExist = PROJ.IsExistBudTmpProject();
		string sCurrentActionName = PROJ.GetCurrentProjectActionName(projectCode);
		if (sCurrentActionName.ToUpper() == "CNT" && IsBudTmpExist)
		{
			string l_str = "select IsNull(Max(version), 50000) as version from tmpProject where projectCode = '" + projectCode + "'  and sKind = 'Cnt'";
			ModifyDB StdCom = new ModifyDB(projectCode, aArr);
			DataTable ldt_mytable = StdCom.DBList(l_str);
			int iMax = PubTools.Str2Int(ldt_mytable.Rows[0]["version"].ToString());
			PROJ.ps_srckind = "CNT";
			PROJ.CopyTmpProj(projectCode, (iMax + 1).ToString());
			string sBud = "Insert Into tmpProject(ProjectCode, mainCode, projectNameC, projectNameE, projectAddress, accountCode1,projectCodeAlias, accountCode2, buyMode, workMode, expectDaily, projectScope, workUnit, projamt, AutoCalcCost, UseIR, CloseBidDate, IsQtyModifiable, BudStartYear, BudEndYear, ExpectStartDate,version,sKind,NewDate,shareVDF1, shareVDF1sNo) Select '" + projectCode + "', mainCode, projectNameC, projectNameE, projectAddress, accountCode1,projectCodeAlias, accountCode2, buyMode, workMode, expectDaily, projectScope, workUnit, projamt, AutoCalcCost, UseIR, CloseBidDate, IsQtyModifiable, BudStartYear, BudEndYear, ExpectStartDate, '" + (iMax + 1) + "' as version,'CNT' as sKind,'" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' as NewDate,shareVDF1, shareVDF1sNo From " + CommonMethods.GetActionNameString(FormActionName) + "Project Where ProjectCode ='" + projectCode + "' ";
			StdCom.DBUpd(sBud);
			StdCom = null;
			int iMenuIndex = PROJ.GetMaxBudTmpProjVersion();
			PROJ.ps_projectCode = projectCode;
			PROJ.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
			PROJ.DeleProjGetTmp(projectCode, iMenuIndex.ToString());
			PROJ.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
			PROJ.SetCurrentProjectActionName(projectCode);
		}
		PROJ = null;
	}

	private void CopyBudgetToTmp(string projectCode, string saveVersion, string SrcAction)
	{
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("預算編輯--設定目前預算編輯類型(預算書或契約書)");
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		ModifyDB StdCom = new ModifyDB(projectCode, aArr);
		int iMax = PubTools.Str2Int(saveVersion);
		PROJ.ps_srckind = SrcAction;
		PROJ.CopyTmpProj(projectCode, (iMax + 1).ToString());
		string sBud = "Insert Into tmpProject(ProjectCode, mainCode, projectNameC, projectNameE, projectAddress, accountCode1,projectCodeAlias, accountCode2, buyMode, workMode, expectDaily, projectScope, workUnit, projamt, AutoCalcCost, UseIR, CloseBidDate, IsQtyModifiable, BudStartYear, BudEndYear, ExpectStartDate,version,sKind,NewDate,shareVDF1, shareVDF1sNo) Select '" + projectCode + "', mainCode, projectNameC, projectNameE, projectAddress, accountCode1,projectCodeAlias, accountCode2, buyMode, workMode, expectDaily, projectScope, workUnit, projamt, AutoCalcCost, UseIR, CloseBidDate, IsQtyModifiable, BudStartYear, BudEndYear, ExpectStartDate, '" + (iMax + 1) + "' as version,'" + SrcAction + "' as sKind,'" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' as NewDate,shareVDF1, shareVDF1sNo From budProject Where ProjectCode ='" + projectCode + "' ";
		StdCom.DBUpd(sBud);
		StdCom = null;
	}

	private void CopyDataToBudgetForEdit(string projectCode, string SrcAction)
	{
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("預算編輯--設定目前預算編輯類型(預算書或契約書)");
		Archnowledge.Pcces.BUDClass.Project project = new Archnowledge.Pcces.BUDClass.Project(aArr);
		project.ps_srckind = SrcAction;
		string lastActionName = project.GetCurrentProjectActionName(projectCode);
		if (lastActionName == "" && SrcAction == "CNT")
		{
			project.ps_projectCode = projectCode;
			string maxCntVersion = project.GetMaxCntTmpProjVersion().ToString();
			project.ps_srckind = "CNT";
			project.DeleProjGetTmp(projectCode, maxCntVersion);
		}
		else if ((!(lastActionName == "") || !(SrcAction == "BUD")) && (!(lastActionName == SrcAction) || !(SrcAction == "CNT")) && (!(lastActionName == SrcAction) || !(SrcAction == "BUD")))
		{
			if (lastActionName != SrcAction && SrcAction == "CNT")
			{
				project.ps_projectCode = projectCode;
				string maxBudVersion = project.GetMaxBudTmpProjVersion().ToString();
				CopyBudgetToTmp(projectCode, maxBudVersion, "BUD");
				project.ps_projectCode = projectCode;
				string maxCntVersion = project.GetMaxCntTmpProjVersion().ToString();
				project.ps_srckind = "CNT";
				project.DeleProjGetTmp(projectCode, maxCntVersion);
			}
			else if (lastActionName != SrcAction && SrcAction == "BUD")
			{
				project.ps_projectCode = projectCode;
				string maxCntVersion = project.GetMaxCntTmpProjVersion().ToString();
				CopyBudgetToTmp(projectCode, maxCntVersion, "CNT");
				project.ps_projectCode = projectCode;
				string maxBudVersion = project.GetMaxBudTmpProjVersion().ToString();
				project.ps_srckind = "BUD";
				project.DeleProjGetTmp(projectCode, maxBudVersion);
			}
		}
		project.ps_srckind = SrcAction;
		project.SetCurrentProjectActionName(projectCode);
	}

	private void OpenBudget(string projectCode, PccesFormAction ActName, bool IsDirectOpenCnt)
	{
		if (!OpenFormBudget)
		{
			Archnowledge.Common.DebugUtil.OutputDebugString("OpenBudget Opening projectCode=" + projectCode);
			OpenFormBudget = true;
			string srckind = CommonMethods.GetActionNameString(ActName);
			CopyDataToBudgetForEdit(projectCode, IsDirectOpenCnt ? "CNT" : "BUD");
			(base.ParentForm as frmPccesMain).LeftPanel.Width = 0;
			frmBudget FM_BDGT = new frmBudget();
			FM_BDGT.ProjectCode = ((projectCode != string.Empty) ? projectCode.Trim() : gridProject[gridProject.Row, "ProjectCode"].ToString());
			FM_BDGT.ProjectName = gridProject[gridProject.Row, "CName"].ToString();
			FM_BDGT._Istemplate = (bool)gridProject[gridProject.Row, "Template"];
			if (srckind == "BID")
			{
				FM_BDGT._Istemplate = false;
			}
			FM_BDGT._ActionName = ActName;
			FM_BDGT.MdiParent = base.ParentForm;
			FM_BDGT._UserID = F_UserID;
			FM_BDGT._UserName = F_UserName;
			FM_BDGT._ServerName = F_ServerName;
			FM_BDGT._FunctionName = ((ActName == PccesFormAction.BUD) ? "BUD" : "BID");
			FM_BDGT._IsNewProject = IsNewProject;
			FM_BDGT._IsDirectOpenCNT = F_IsDirectOpenCNT;
			if (EnableCOMS && srckind == "BID")
			{
				BidProject theBidProject = new BidProject();
				string IsLastBid = theBidProject.GetBidProjectIsType(gridProject[gridProject.Row, "ProjectCode"].ToString());
				if (IsLastBid == "1")
				{
					FM_BDGT._IsLastBid = true;
				}
			}
			ArrayList aArr = new ArrayList();
			aArr.Add(F_UserID);
			aArr.Add("專案挑選--讀取主專案");
			Archnowledge.Pcces.BUDClass.Project ProjCom = new Archnowledge.Pcces.BUDClass.Project(aArr);
			ProjCom.ps_srckind = CommonMethods.GetActionNameString(ActName);
			string sMainProjectCode = ProjCom.GetMainProj(FM_BDGT.ProjectCode).Trim();
			if (sMainProjectCode == "-1")
			{
				sMainProjectCode = "";
			}
			FM_BDGT._MainProjectCode = sMainProjectCode;
			FM_BDGT.Show();
			if (FM_BDGT.NeedClose)
			{
				FM_BDGT.Close();
				OpenFormBudget = false;
				IsHasLoadedBudget = false;
				Refresh();
			}
			else
			{
				Close();
			}
		}
		else
		{
			Archnowledge.Common.DebugUtil.OutputDebugString("OpenBudget Error Opened projectCode=" + projectCode);
		}
	}

	private void gridProject_MouseDown(object sender, MouseEventArgs e)
	{
		bool Istemp = false;
		bool IsBud = true;
		bool IsBid = true;
		bool IsCNT = false;
		int rowIndex = gridProject.RowSel;
		int colindex = gridProject.ColSel;
		try
		{
			Istemp = (bool)gridProject[rowIndex, "Template"];
			IsBud = (bool)gridProject[rowIndex, "IsBud"];
			IsBid = (bool)gridProject[rowIndex, "IsBid"];
			IsCNT = (bool)gridProject[rowIndex, "IsCNT"];
		}
		catch
		{
		}
		if (IsHasLoadedBudget)
		{
			return;
		}
		IsHasLoadedBudget = true;
		if (gridProject.Row <= 0 || rowIndex <= 0)
		{
			ultraToolbarsManager1.Tools["PopNewProject"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuDelProject"].SharedProps.Enabled = false;
			return;
		}
		ultraToolbarsManager1.Tools["PopNewProject"].SharedProps.Enabled = true;
		ultraToolbarsManager1.Tools["mnuDelProject"].SharedProps.Enabled = true;
		if (IsBud)
		{
			if (Istemp)
			{
				ultraToolbarsManager1.Tools["mnuTemplate"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuCancelTemplate"].SharedProps.Enabled = true;
			}
			else
			{
				ultraToolbarsManager1.Tools["mnuTemplate"].SharedProps.Enabled = true;
				ultraToolbarsManager1.Tools["mnuCancelTemplate"].SharedProps.Enabled = false;
			}
			ultraToolbarsManager1.Tools["mnuBidToBud"].SharedProps.Enabled = false;
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuTemplate"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuCancelTemplate"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuBidToBud"].SharedProps.Enabled = true;
		}
		ultraToolbarsManager1.Tools["mnuBidToBud"].SharedProps.Enabled = IsBid;
		string ColName = gridProject.Cols[colindex].Name;
		string StrProjectCode = gridProject[gridProject.Row, "ProjectCode"].ToString();
		string StrProjectNameC = gridProject[gridProject.Row, "CName"].ToString();
		string StrProjectNameE = gridProject[gridProject.Row, "EName"].ToString();
		string StrProjectAddr = gridProject[gridProject.Row, "Address"].ToString();
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		if (!DBCLS.GetProjectAuthority(F_UserID, StrProjectCode))
		{
			IsHasLoadedBudget = false;
			MessageBox.Show(this, "這個專案您沒有權限，無法開啟。", "專案權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			if (e.Button == MouseButtons.Right)
			{
				return;
			}
			bool flag = 0 == 0;
			if (ColName == "BUD")
			{
				if (rowIndex == -1)
				{
					return;
				}
				if (!(bool)gridProject[rowIndex, "IsBud"])
				{
					IsNewProject = "New";
					FormBudgetProjectInfo FM_BDGT_PINFO = new FormBudgetProjectInfo();
					FM_BDGT_PINFO._UserID = F_UserID;
					FM_BDGT_PINFO._OpenMode = BudgetInfoForm_OpenMode.NewBudget;
					FM_BDGT_PINFO._ProjectCode = StrProjectCode;
					FM_BDGT_PINFO._ProjectNameC = StrProjectNameC;
					FM_BDGT_PINFO._ProjectNameE = StrProjectNameE;
					FM_BDGT_PINFO._ProjectAddress = StrProjectAddr;
					FM_BDGT_PINFO._ActionName = PccesFormAction.BUD;
					DialogResult theReslut = FM_BDGT_PINFO.ShowDialog(this);
					FM_BDGT_PINFO.Close();
					FM_BDGT_PINFO.Dispose();
					FM_BDGT_PINFO = null;
					if (theReslut == DialogResult.OK)
					{
						ArrayList aArr = new ArrayList();
						aArr.Add(F_UserID);
						aArr.Add("預算編輯--設定目前預算編輯類型(預算書或契約書)");
						Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
						PROJ.ps_projectCode = StrProjectCode.Trim();
						PROJ.ps_srckind = "BUD";
						PROJ.SetCurrentProjectActionName(StrProjectCode.Trim());
						PROJ = null;
						Set22132814Decimal(StrProjectCode);
						UpdateIRSet(StrProjectCode, "BUD");
						OpenBudget(StrProjectCode, PccesFormAction.BUD, IsDirectOpenCnt: false);
						Close();
					}
				}
				else
				{
					UpdateIRSet(StrProjectCode, "BUD");
					OpenBudget(StrProjectCode, PccesFormAction.BUD, IsDirectOpenCnt: false);
				}
			}
			else if (ColName == "BudEst" && ArchConvert.Obj2Bool(gridProject[rowIndex, "IsBud"]))
			{
				if (ArchConvert.Obj2Bool(gridProject[rowIndex, "BudEstAuth"]))
				{
					FormCostEstProjectList formCostEstProjectList = new FormCostEstProjectList(StrProjectCode, F_UserID, BudgetType.Types.CostEstimation);
					if (formCostEstProjectList.ShowDialog() == DialogResult.OK)
					{
						OpenBudget(formCostEstProjectList.TargetProjectCode, PccesFormAction.BUD, IsDirectOpenCnt: false);
					}
					else
					{
						GetNewData();
						BindDataToGrid();
					}
					formCostEstProjectList.Dispose();
					formCostEstProjectList = null;
				}
				else
				{
					MessageBox.Show(this, "這個專案您沒有此功能的權限，無法開啟。", "專案權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			else if (ColName == "BudQuote" && ArchConvert.Obj2Bool(gridProject[rowIndex, "IsBud"]))
			{
				if (ArchConvert.Obj2Bool(gridProject[rowIndex, "BudQuoteAuth"]))
				{
					FormCostEstProjectList formCostEstProjectList = new FormCostEstProjectList(StrProjectCode, F_UserID, BudgetType.Types.CostQuotationMerged);
					if (formCostEstProjectList.ShowDialog() == DialogResult.OK)
					{
						OpenBudget(formCostEstProjectList.TargetProjectCode, PccesFormAction.BUD, IsDirectOpenCnt: false);
					}
					else
					{
						GetNewData();
						BindDataToGrid();
					}
					formCostEstProjectList.Dispose();
					formCostEstProjectList = null;
				}
				else
				{
					MessageBox.Show(this, "這個專案您沒有此功能的權限，無法開啟。", "專案權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			else if (ColName == "BID")
			{
				if (!(bool)gridProject[rowIndex, "IsBid"])
				{
					MessageBox.Show(this, "不可手動建立標單，\n請使用電子檔轉入。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (!(bool)gridProject[rowIndex, "IsBid"])
				{
					FormBudgetProjectInfo FM_BDGT_PINFO = new FormBudgetProjectInfo();
					FM_BDGT_PINFO._UserID = F_UserID;
					FM_BDGT_PINFO._OpenMode = BudgetInfoForm_OpenMode.NewBudget;
					FM_BDGT_PINFO._ProjectCode = StrProjectCode;
					FM_BDGT_PINFO._ProjectNameC = StrProjectNameC;
					FM_BDGT_PINFO._ProjectNameE = StrProjectNameE;
					FM_BDGT_PINFO._ProjectAddress = StrProjectAddr;
					FM_BDGT_PINFO._ActionName = PccesFormAction.BID;
					DialogResult theReslut = FM_BDGT_PINFO.ShowDialog(this);
					FM_BDGT_PINFO.Close();
					FM_BDGT_PINFO.Dispose();
					FM_BDGT_PINFO = null;
					if (theReslut == DialogResult.OK)
					{
						OpenBudget(StrProjectCode, PccesFormAction.BID, IsDirectOpenCnt: false);
						Close();
					}
				}
				else
				{
					OpenBudget(StrProjectCode, PccesFormAction.BID, IsDirectOpenCnt: false);
				}
			}
			else if (ColName == "CNT")
			{
				if (!IsCNT)
				{
					MessageBox.Show(this, "不可手動建立契約書，\n請使用電子檔轉入或在預算編製內儲存契約書版次。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				F_IsDirectOpenCNT = true;
				UpdateIRSet(StrProjectCode, "BUD");
				OpenBudget(StrProjectCode, PccesFormAction.BUD, F_IsDirectOpenCNT);
			}
			DBCLS = null;
		}
	}

	private void Set22132814Decimal(string sProject)
	{
		if (Is22132814())
		{
			DataSet dsPubDecimal = new DataSet();
			DataTable dtPubDecimal = new DataTable();
			dtPubDecimal.TableName = "PubDecimal";
			dtPubDecimal.Columns.Add("ProjectCode", Type.GetType("System.String"));
			dtPubDecimal.Columns.Add("itemQty", Type.GetType("System.Int32"));
			dtPubDecimal.Columns.Add("itemCost", Type.GetType("System.Int32"));
			dtPubDecimal.Columns.Add("itemAmt", Type.GetType("System.Int32"));
			dtPubDecimal.Columns.Add("analysisQty", Type.GetType("System.Int32"));
			dtPubDecimal.Columns.Add("analysisCost", Type.GetType("System.Int32"));
			dtPubDecimal.Columns.Add("analysisAmt", Type.GetType("System.Int32"));
			dtPubDecimal.Columns.Add("EnableItemAmt2", Type.GetType("System.Boolean"));
			DataRow DR = dtPubDecimal.NewRow();
			DR["ProjectCode"] = sProject;
			DR["itemQty"] = 2;
			DR["itemCost"] = 2;
			DR["itemAmt"] = 0;
			DR["analysisQty"] = 3;
			DR["analysisCost"] = 2;
			DR["analysisAmt"] = 2;
			DR["EnableItemAmt2"] = false;
			dtPubDecimal.Rows.Add(DR);
			dsPubDecimal.Tables.Add(dtPubDecimal);
			Archnowledge.Pcces.DomainModule.General.PubDecimal pubDecimal = new Archnowledge.Pcces.DomainModule.General.PubDecimal();
			pubDecimal.UpdatePubDecimal(dsPubDecimal);
		}
	}

	private bool Is22132814()
	{
		string sPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "22132814.dat");
		if (File.Exists(sPath))
		{
			return true;
		}
		return false;
	}

	private void gridProject_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Delete)
		{
			ExecuteDeleteProject();
		}
	}

	private void ultraToolbarsManager1_ToolKeyDown(object sender, ToolKeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && e.Tool.Key == "mnuKeyword")
		{
			Do_ToolBarFind();
		}
	}

	public void LocateToSpecificRow()
	{
		int iFind = gridProject.FindRow(F_NewProjectCode, 1, gridProject.Cols["ProjectCode"].SafeIndex, caseSensitive: false, fullMatch: false, wrap: true);
		if (iFind > 0)
		{
			gridProject.Row = iFind;
			gridProject.Select();
		}
	}

	private void Do_ToolBarFind()
	{
		int iStart = gridProject.Row + 1;
		string sSearchText = ((ComboBoxTool)ultraToolbarsManager1.Tools["mnuKeyword"]).Text.Trim();
		if (!CommonMethods.CheckValidString(sSearchText))
		{
			return;
		}
		if (F_KeyWord != sSearchText.Trim())
		{
			iStart = 1;
			F_KeyWord = sSearchText.Trim();
		}
		else
		{
			iStart = gridProject.Row + 1;
		}
		if (sSearchText.Trim() == "")
		{
			return;
		}
		for (int i = iStart; i < gridProject.Rows.Count; i++)
		{
			for (int j = 1; j < gridProject.Cols.Count; j++)
			{
				if (gridProject[i, j] == null || gridProject[i, j].ToString().IndexOf(sSearchText) <= -1)
				{
					continue;
				}
				gridProject.Row = i;
				int iFondCount = 0;
				int iListCount = ((ComboBoxTool)ultraToolbarsManager1.Tools["mnuKeyword"]).ValueList.ValueListItems.Count;
				for (int k = 0; k < iListCount; k++)
				{
					if (((ComboBoxTool)ultraToolbarsManager1.Tools["mnuKeyword"]).ValueList.ValueListItems[k].DisplayText.Trim() == sSearchText.Trim())
					{
						iFondCount++;
					}
				}
				if (iFondCount == 0)
				{
					((ComboBoxTool)ultraToolbarsManager1.Tools["mnuKeyword"]).ValueList.ValueListItems.Add(sSearchText, sSearchText);
				}
				return;
			}
		}
	}

	private void ultraToolbarsManager1_AfterToolActivate(object sender, ToolEventArgs e)
	{
		if (e.Tool.Key == "mnuKeyword")
		{
			((ButtonTool)ultraToolbarsManager1.Tools["mnuDelProject"]).SharedProps.Shortcut = Shortcut.None;
		}
		else
		{
			((ButtonTool)ultraToolbarsManager1.Tools["mnuDelProject"]).SharedProps.Shortcut = Shortcut.Del;
		}
	}

	private void ultraToolbarsManager1_AfterToolDeactivate(object sender, ToolEventArgs e)
	{
		if (ultraToolbarsManager1 != null)
		{
			((ButtonTool)ultraToolbarsManager1.Tools["mnuDelProject"]).SharedProps.Shortcut = Shortcut.Del;
		}
	}

	private void FormProject_FormClosing(object sender, FormClosingEventArgs e)
	{
		GridPropertySetting.SaveGridProperty(F_UserID, base.Name, gridProject);
	}

	private void gridProject_MouseMove(object sender, MouseEventArgs e)
	{
		if (gridProject.MouseRow > 0 && gridProject.MouseCol > 0)
		{
			int colIndex = gridProject.MouseCol;
			int rowIndex = gridProject.MouseRow;
			string gridMouseColName = gridProject.Cols[colIndex].Name;
			if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right && (bool)gridProject[rowIndex, "IsCNT"] && gridMouseColName == "CNT")
			{
				Cursor = Cursors.Hand;
			}
			else if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right && (gridMouseColName == "BUD" || gridMouseColName == "BID"))
			{
				Cursor = Cursors.Hand;
			}
			else if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right && (gridMouseColName == "BudEst" || gridMouseColName == "BudQuote") && ArchConvert.Obj2Bool(gridProject[rowIndex, "IsBud"]))
			{
				Cursor = Cursors.Hand;
			}
			else
			{
				Cursor = Cursors.Default;
			}
		}
	}

	private void gridProject_AfterSelChange(object sender, RangeEventArgs e)
	{
		IsHasLoadedBudget = false;
		if (!gridProject.Cols[gridProject.Col].AllowEditing && gridProject.ColSel > 4)
		{
			gridProject.Col = 0;
		}
	}

	private void gridProject_AfterEdit(object sender, RowColEventArgs e)
	{
		IsHasLoadedBudget = false;
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("WinFORM 基本工料");
		Archnowledge.Pcces.BUDClass.PubProject PUB_PROJ = new Archnowledge.Pcces.BUDClass.PubProject(aArr);
		PUB_PROJ.ps_projectCode = gridProject[e.Row, "ProjectCode"].ToString().Trim();
		PUB_PROJ.ps_projectNameC = gridProject[e.Row, "CName"].ToString().Trim();
		PUB_PROJ.ps_projectNameE = gridProject[e.Row, "EName"].ToString().Trim();
		PUB_PROJ.ps_projectAddress = gridProject[e.Row, "Address"].ToString().Trim();
		PUB_PROJ.ps_projectCodeAlias = gridProject[e.Row, "projectCodeAlias"].ToString().Trim();
		PUB_PROJ.ps_projectMemo = gridProject[e.Row, "projectMemo"].ToString().Trim();
		if (PUB_PROJ.UpdItem() == -2)
		{
			MessageBox.Show(this, "該筆資料已不存在, 可能已經被其他使用者刪除!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			try
			{
				if ((bool)gridProject[e.Row, "IsBud"])
				{
					Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
					PROJ.ps_projectCode = gridProject[e.Row, "ProjectCode"].ToString().Trim();
					PROJ.ps_projectNameC = gridProject[e.Row, "CName"].ToString().Trim();
					PROJ.ps_projectNameE = gridProject[e.Row, "EName"].ToString().Trim();
					PROJ.ps_projectAddress = gridProject[e.Row, "Address"].ToString().Trim();
					PROJ.ps_srckind = "BUD";
					PROJ.UpdItem();
					PROJ = null;
				}
				if ((bool)gridProject[e.Row, "IsBid"])
				{
					Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
					PROJ.ps_projectCode = gridProject[e.Row, "ProjectCode"].ToString().Trim();
					PROJ.ps_projectNameC = gridProject[e.Row, "CName"].ToString().Trim();
					PROJ.ps_projectNameE = gridProject[e.Row, "EName"].ToString().Trim();
					PROJ.ps_projectAddress = gridProject[e.Row, "Address"].ToString().Trim();
					PROJ.ps_srckind = "BID";
					PROJ.UpdItem();
					PROJ = null;
				}
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Project.FormProject.cs" + ex.Message);
			}
		}
		aArr = null;
		PUB_PROJ = null;
	}

	private void gridProject_BeforeEdit(object sender, RowColEventArgs e)
	{
		IsHasLoadedBudget = false;
		if (gridProject.Row <= 0 || gridProject.MouseCol <= 0)
		{
			return;
		}
		if (gridProject.Cols[e.Col].Name == "CName" && !DBClass.ChkAuthority(F_UserID, "F00500030001"))
		{
			iAuthorityMSG_Count++;
			if (iAuthorityMSG_Count <= 1)
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00500030001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			iAuthorityMSG_Count = 0;
			e.Cancel = true;
			gridProject.Col = 0;
		}
		else if (gridProject.Cols[e.Col].Name == "EName" && !DBClass.ChkAuthority(F_UserID, "F00500030002"))
		{
			iAuthorityMSG_Count++;
			if (iAuthorityMSG_Count <= 1)
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00500030002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			iAuthorityMSG_Count = 0;
			e.Cancel = true;
			gridProject.Col = 0;
		}
		else if (gridProject.Cols[e.Col].Name == "Address" && !DBClass.ChkAuthority(F_UserID, "F00500030003"))
		{
			iAuthorityMSG_Count++;
			if (iAuthorityMSG_Count <= 1)
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00500030003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			iAuthorityMSG_Count = 0;
			e.Cancel = true;
			gridProject.Col = 0;
		}
	}

	private void ultraToolbarsManager1_BeforeToolbarListDropdown(object sender, BeforeToolbarListDropdownEventArgs e)
	{
		e.Cancel = true;
	}

	private void gridProject_StartEdit(object sender, RowColEventArgs e)
	{
		((ButtonTool)ultraToolbarsManager1.Tools["mnuDelProject"]).SharedProps.Shortcut = Shortcut.None;
	}

	private void ultraStatusBar1_PanelClick(object sender, PanelClickEventArgs e)
	{
		if (e.Panel.Index == 1)
		{
			ShellExecute SHExe = new ShellExecute();
			SHExe.OwnerHandle = base.Handle;
			SHExe.Path = "http://pcces.archnowledge.com/pccesfaq/";
			SHExe.Execute();
			SHExe = null;
		}
	}

	private void FormProject_FormClosed(object sender, FormClosedEventArgs e)
	{
		ultraToolbarsManager1 = null;
		FormProject_Fill_Panel = null;
		_FormProject_Toolbars_Dock_Area_Left = null;
		_FormProject_Toolbars_Dock_Area_Right = null;
		_FormProject_Toolbars_Dock_Area_Top = null;
		_FormProject_Toolbars_Dock_Area_Bottom = null;
		panel2 = null;
		gridProject = null;
		LeftPanel = null;
		onlineList1 = null;
		functionButtons1 = null;
		panel1 = null;
		pnl_spliter = null;
		Btn_Splt = null;
		ssp_Lower = null;
		ssp_Bottom = null;
		ssp_Upper = null;
		ssp_Top = null;
		panel3 = null;
		ultraStatusBar1 = null;
		ssp_GridCaption = null;
		iglst_splt_Btn = null;
		imageList2 = null;
		lblUseDatabase = null;
		ToolLists = null;
		ToolParam = null;
		GridColsSquence = null;
		DT1 = null;
		GC.Collect();
	}

	private void ProcessAddOn()
	{
		string AppLocation = AppDomain.CurrentDomain.BaseDirectory;
		string FileINI = AppLocation + "Addon.ini";
		ToolLists.Clear();
		ToolParam.Clear();
		for (int i = 1; i <= 20; i++)
		{
			string sValue = CommonMethods.IniReadValue(FileINI, "PROJECT", "TOOL" + i);
			if (sValue.Trim() != "")
			{
				ToolLists.Add(sValue.Substring(0, sValue.IndexOf(",")));
				ToolParam.Add(sValue.Substring(sValue.IndexOf(",") + 1));
			}
		}
		if (ToolLists.Count > 0)
		{
			PopupMenuTool Addon = (PopupMenuTool)ultraToolbarsManager1.Tools["AddOn"];
			Addon.Tools.Clear();
			ultraToolbarsManager1.Tools["AddOn"].SharedProps.Visible = true;
			ultraToolbarsManager1.Tools["AddOn"].SharedProps.Enabled = true;
			for (int i = 0; i < ToolLists.Count; i++)
			{
				ButtonTool BT = new ButtonTool(ToolLists[i].ToString());
				BT.SharedProps.Tag = i;
				BT.SharedProps.Caption = ToolLists[i].ToString();
				BT.ToolClick += AddOnClick;
				try
				{
					ultraToolbarsManager1.Tools.Remove(BT);
				}
				catch (Exception ex)
				{
					CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBase.cs--ProcessAddOn" + ex.Message);
				}
				ultraToolbarsManager1.Tools.Add(BT);
				Addon.Tools.AddTool(ToolLists[i].ToString());
			}
		}
		else
		{
			ultraToolbarsManager1.Tools["AddOn"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["AddOn"].SharedProps.Enabled = false;
		}
	}

	private void AddOnClick(object sender, ToolClickEventArgs e)
	{
		int iMenuIndex = (int)e.Tool.SharedProps.Tag;
		string sCmd = ToolParam[iMenuIndex].ToString();
		if (!(sCmd.Substring(0, 1) == "[") || !(sCmd.Substring(sCmd.Length - 1, 1) == "]"))
		{
			SysUser oSysUser = new SysUser();
			string DBName = oSysUser.GetSysUserDatabaseName(F_UserID);
			if (sCmd.IndexOf("%PJ") > -1)
			{
				sCmd = sCmd.Replace("%PJ", "P");
			}
			if (sCmd.IndexOf("%DB") > -1)
			{
				sCmd = sCmd.Replace("%DB", DBName);
			}
			if (sCmd.IndexOf("%UID") > -1)
			{
				sCmd = sCmd.Replace("%UID", F_UserID);
			}
			string sPath = ((sCmd.IndexOf(" ") > -1) ? sCmd.Substring(0, sCmd.IndexOf(" ")) : sCmd);
			string sParameters = ((sCmd.IndexOf(" ") > -1) ? sCmd.Substring(sCmd.IndexOf(" ")) : "");
			ShellExecute SHExe = new ShellExecute();
			SHExe.OwnerHandle = base.Handle;
			SHExe.Path = sPath;
			SHExe.Parameters = sParameters;
			SHExe.Execute();
			SHExe = null;
		}
	}

	private void CheckIstemplate(string YesNo)
	{
		string sQuest = ((YesNo == "Y") ? "確定將此預算書設定成範本 ?" : "確定取消此預算書範本 ?");
		if (MessageBox.Show(this, sQuest, "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
		{
			return;
		}
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("判斷是否要設為範本");
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PROJ.ps_srckind = "bud";
		for (int i = 1; i < gridProject.Rows.Count; i++)
		{
			if (gridProject.Rows[i].Selected)
			{
				PROJ.ps_projectCode = gridProject[i, "ProjectCode"].ToString().Trim();
				PROJ.ps_Istemplate = YesNo;
				PROJ.UpdItem();
			}
		}
		aArr = null;
		PROJ = null;
	}

	private void UpdateIRSet(string sProjectCode, string sType)
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("判斷是否要重新總計--" + sProjectCode);
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PROJ.ps_projectCode = sProjectCode;
		PROJ.ps_srckind = sType;
		PROJ.SetUseIRSet(sProjectCode, "1");
		aArr = null;
		PROJ = null;
	}

	private bool IsBudgetCanDelete(string projectcode)
	{
		if (SysConfig.SysComsEnable)
		{
			Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl theBudgetCtrl = new Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl();
			if (theBudgetCtrl.IsProjectComsExecuteBudget(projectcode, SysConfig.SysComsDB))
			{
				return false;
			}
			return true;
		}
		return true;
	}

	private void gridProject_Resize(object sender, EventArgs e)
	{
		FormProject_Resize(sender, e);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Project.FormProject));
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.OptionSet optionSet1 = new Infragistics.Win.UltraWinToolbars.OptionSet("view");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool1");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuPopNew");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool2 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuPopDel");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnuCapFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool1 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnuKeyword");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuGo");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuClone");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool3 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuTemplate");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool4 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("ViewMenu");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool5 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("HelpMenu");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuBidToBud");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool6 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("AddOn");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CopyBidToCompanyDB");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar2 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("COMS_Tools");
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuNewProject");
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelProject");
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnuCapFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool2 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnuKeyword");
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuGo");
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool7 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Popup1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PopNewProject");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool8 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuPopDel");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuClone");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool10 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTemplate");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool11 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCancelTemplate");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool12 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuBidToBud");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool13 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImport");
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool9 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuPopNew");
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool14 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuNewProject");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool15 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool16 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExcleImport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool17 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSplit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool18 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuBindBid");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool19 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuProjectExit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool20 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuProjectExit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool21 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExcleImport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool22 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSplit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool23 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PopNewProject");
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool24 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuBindBid");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool25 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuClone");
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool26 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUpdateList");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool10 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("AddOn");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool27 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelBudProject");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool28 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelBidProject");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool11 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuPopDel");
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool29 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelProject");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool30 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelBudProject");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool31 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelBidProject");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool32 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTemplate");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool33 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCancelTemplate");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool12 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuTemplate");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool34 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTemplate");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool35 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCancelTemplate");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool13 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("HelpMenu");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool36 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUpdateList");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool14 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("ViewMenu");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool1 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewAll", "view");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool2 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuOnlyPower", "view");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool3 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuOnlyTemplate", "view");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool4 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuOnlyPower", "view");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool5 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuOnlyTemplate", "view");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool6 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewAll", "view");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool37 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuBidToBud");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool38 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CopyBidToCompanyDB");
		this.gridProject = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.FormProject_Fill_Panel = new System.Windows.Forms.Panel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.panel3 = new System.Windows.Forms.Panel();
		this.ShowProject = new Archnowledge.Pcces.PccesMain.Project.uccShowProject();
		this.lblUseDatabase = new Infragistics.Win.Misc.UltraLabel();
		this.ssp_GridCaption = new AxThreed.AxSSPanel();
		this.pnl_spliter = new System.Windows.Forms.Panel();
		this.Btn_Splt = new Infragistics.Win.Misc.UltraButton();
		this.ssp_Lower = new AxThreed.AxSSPanel();
		this.ssp_Bottom = new AxThreed.AxSSPanel();
		this.ssp_Upper = new AxThreed.AxSSPanel();
		this.ssp_Top = new AxThreed.AxSSPanel();
		this.LeftPanel = new System.Windows.Forms.Panel();
		this.onlineList1 = new Archnowledge.Pcces.PccesMain.ArchControls.OnlineList();
		this.functionButtons1 = new Archnowledge.Pcces.PccesMain.ArchControls.FunctionButtons();
		this.iglst_splt_Btn = new System.Windows.Forms.ImageList(this.components);
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this._FormProject_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this._FormProject_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormProject_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormProject_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		((System.ComponentModel.ISupportInitialize)this.gridProject).BeginInit();
		this.FormProject_Fill_Panel.SuspendLayout();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ssp_GridCaption).BeginInit();
		this.pnl_spliter.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ssp_Lower).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Bottom).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Upper).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Top).BeginInit();
		this.LeftPanel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		base.SuspendLayout();
		this.gridProject._ExcelFileName = "";
		this.gridProject._ExcelSheeName = "";
		this.gridProject._IsOpenExcelAfterExport = false;
		this.gridProject.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridProject.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.gridProject.ColumnInfo = resources.GetString("gridProject.ColumnInfo");
		this.ultraToolbarsManager1.SetContextMenuUltra(this.gridProject, "Popup1");
		this.gridProject.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridProject.ExtendLastCol = true;
		this.gridProject.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridProject.ForeColor = System.Drawing.Color.Black;
		this.gridProject.Location = new System.Drawing.Point(0, 0);
		this.gridProject.Name = "gridProject";
		this.gridProject.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridProject.ShowCursor = true;
		this.gridProject.ShowToolTipOnNarrowColumn = true;
		this.gridProject.Size = new System.Drawing.Size(725, 493);
		this.gridProject.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridProject.Styles"));
		this.gridProject.TabIndex = 0;
		this.gridProject.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(gridProject_AfterSelChange);
		this.gridProject.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridProject_StartEdit);
		this.gridProject.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridProject_AfterEdit);
		this.gridProject.KeyDown += new System.Windows.Forms.KeyEventHandler(gridProject_KeyDown);
		this.gridProject.MouseDown += new System.Windows.Forms.MouseEventHandler(gridProject_MouseDown);
		this.gridProject.Resize += new System.EventHandler(gridProject_Resize);
		this.gridProject.MouseMove += new System.Windows.Forms.MouseEventHandler(gridProject_MouseMove);
		this.gridProject.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridProject_BeforeEdit);
		this.FormProject_Fill_Panel.Controls.Add(this.panel1);
		this.FormProject_Fill_Panel.Controls.Add(this.pnl_spliter);
		this.FormProject_Fill_Panel.Controls.Add(this.LeftPanel);
		this.FormProject_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
		this.FormProject_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.FormProject_Fill_Panel.Location = new System.Drawing.Point(0, 27);
		this.FormProject_Fill_Panel.Name = "FormProject_Fill_Panel";
		this.FormProject_Fill_Panel.Size = new System.Drawing.Size(892, 546);
		this.FormProject_Fill_Panel.TabIndex = 0;
		this.panel1.Controls.Add(this.panel2);
		this.panel1.Controls.Add(this.ultraStatusBar1);
		this.panel1.Controls.Add(this.panel3);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(167, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(725, 546);
		this.panel1.TabIndex = 4;
		this.panel2.Controls.Add(this.gridProject);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 30);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(725, 493);
		this.panel2.TabIndex = 2;
		appearance3.FontData.SizeInPoints = 11f;
		this.ultraStatusBar1.Appearance = appearance3;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 523);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Text = "資料筆數:";
		ultraStatusPanel1.Width = 180;
		appearance4.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance4.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		ultraStatusPanel2.Appearance = appearance4;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.MarqueeInfo.IsActive = true;
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		ultraStatusPanel2.Style = Infragistics.Win.UltraWinStatusBar.PanelStyle.Marquee;
		appearance5.TextHAlign = Infragistics.Win.HAlign.Right;
		ultraStatusPanel3.Appearance = appearance5;
		ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel3.Text = "客服電話:(02)2708-8090";
		ultraStatusPanel3.Width = 200;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[3] { ultraStatusPanel1, ultraStatusPanel2, ultraStatusPanel3 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(725, 23);
		this.ultraStatusBar1.SupportThemes = false;
		this.ultraStatusBar1.TabIndex = 4;
		this.ultraStatusBar1.Text = "ultraStatusBar1";
		this.ultraStatusBar1.PanelClick += new Infragistics.Win.UltraWinStatusBar.PanelClickEventHandler(ultraStatusBar1_PanelClick);
		this.panel3.Controls.Add(this.ShowProject);
		this.panel3.Controls.Add(this.lblUseDatabase);
		this.panel3.Controls.Add(this.ssp_GridCaption);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel3.Location = new System.Drawing.Point(0, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(725, 30);
		this.panel3.TabIndex = 3;
		this.ShowProject.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.ShowProject.Dock = System.Windows.Forms.DockStyle.Right;
		this.ShowProject.Location = new System.Drawing.Point(445, 0);
		this.ShowProject.Name = "ShowProject";
		this.ShowProject.Size = new System.Drawing.Size(280, 30);
		this.ShowProject.TabIndex = 9;
		this.ShowProject.Visible = false;
		this.lblUseDatabase.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.lblUseDatabase.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblUseDatabase.Location = new System.Drawing.Point(7, 8);
		this.lblUseDatabase.Name = "lblUseDatabase";
		this.lblUseDatabase.Size = new System.Drawing.Size(432, 20);
		this.lblUseDatabase.TabIndex = 8;
		this.lblUseDatabase.Text = "目前資料庫：";
		this.ssp_GridCaption.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ssp_GridCaption.Location = new System.Drawing.Point(0, 0);
		this.ssp_GridCaption.Name = "ssp_GridCaption";
		this.ssp_GridCaption.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_GridCaption.OcxState");
		this.ssp_GridCaption.Size = new System.Drawing.Size(725, 30);
		this.ssp_GridCaption.TabIndex = 1;
		this.pnl_spliter.BackColor = System.Drawing.Color.LightGray;
		this.pnl_spliter.Controls.Add(this.Btn_Splt);
		this.pnl_spliter.Controls.Add(this.ssp_Lower);
		this.pnl_spliter.Controls.Add(this.ssp_Bottom);
		this.pnl_spliter.Controls.Add(this.ssp_Upper);
		this.pnl_spliter.Controls.Add(this.ssp_Top);
		this.pnl_spliter.Dock = System.Windows.Forms.DockStyle.Left;
		this.pnl_spliter.Location = new System.Drawing.Point(160, 0);
		this.pnl_spliter.Name = "pnl_spliter";
		this.pnl_spliter.Size = new System.Drawing.Size(7, 546);
		this.pnl_spliter.TabIndex = 5;
		appearance6.BorderColor = System.Drawing.Color.Transparent;
		appearance6.BorderColor3DBase = System.Drawing.Color.Transparent;
		appearance6.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance6.ImageBackground");
		this.Btn_Splt.Appearance = appearance6;
		this.Btn_Splt.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Borderless;
		this.Btn_Splt.Cursor = System.Windows.Forms.Cursors.Hand;
		this.Btn_Splt.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Btn_Splt.ImageSize = new System.Drawing.Size(7, 57);
		this.Btn_Splt.Location = new System.Drawing.Point(0, 220);
		this.Btn_Splt.Name = "Btn_Splt";
		this.Btn_Splt.ShapeImage = (System.Drawing.Image)resources.GetObject("Btn_Splt.ShapeImage");
		this.Btn_Splt.ShowFocusRect = false;
		this.Btn_Splt.ShowOutline = false;
		this.Btn_Splt.Size = new System.Drawing.Size(7, 71);
		this.Btn_Splt.TabIndex = 5;
		this.Btn_Splt.MouseLeave += new System.EventHandler(Btn_Splt_MouseLeave);
		this.Btn_Splt.Click += new System.EventHandler(Btn_Splt_Click);
		this.Btn_Splt.MouseEnter += new System.EventHandler(Btn_Splt_MouseEnter);
		this.ssp_Lower.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ssp_Lower.Location = new System.Drawing.Point(0, 291);
		this.ssp_Lower.Name = "ssp_Lower";
		this.ssp_Lower.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Lower.OcxState");
		this.ssp_Lower.Size = new System.Drawing.Size(7, 252);
		this.ssp_Lower.TabIndex = 3;
		this.ssp_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ssp_Bottom.Location = new System.Drawing.Point(0, 543);
		this.ssp_Bottom.Name = "ssp_Bottom";
		this.ssp_Bottom.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Bottom.OcxState");
		this.ssp_Bottom.Size = new System.Drawing.Size(7, 3);
		this.ssp_Bottom.TabIndex = 4;
		this.ssp_Upper.Dock = System.Windows.Forms.DockStyle.Top;
		this.ssp_Upper.Location = new System.Drawing.Point(0, 3);
		this.ssp_Upper.Name = "ssp_Upper";
		this.ssp_Upper.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Upper.OcxState");
		this.ssp_Upper.Size = new System.Drawing.Size(7, 217);
		this.ssp_Upper.TabIndex = 2;
		this.ssp_Top.Dock = System.Windows.Forms.DockStyle.Top;
		this.ssp_Top.Location = new System.Drawing.Point(0, 0);
		this.ssp_Top.Name = "ssp_Top";
		this.ssp_Top.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Top.OcxState");
		this.ssp_Top.Size = new System.Drawing.Size(7, 3);
		this.ssp_Top.TabIndex = 1;
		this.LeftPanel.Controls.Add(this.onlineList1);
		this.LeftPanel.Controls.Add(this.functionButtons1);
		this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
		this.LeftPanel.Location = new System.Drawing.Point(0, 0);
		this.LeftPanel.Name = "LeftPanel";
		this.LeftPanel.Size = new System.Drawing.Size(160, 546);
		this.LeftPanel.TabIndex = 3;
		this.onlineList1._FunctionName = "";
		this.onlineList1._HasRegistered = false;
		this.onlineList1._ServerName = "localhost";
		this.onlineList1._TRY_Flag = "";
		this.onlineList1._UserID = "";
		this.onlineList1._UserName = "";
		this.onlineList1.AutoSize = true;
		this.onlineList1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.onlineList1.Dock = System.Windows.Forms.DockStyle.Top;
		this.onlineList1.Location = new System.Drawing.Point(0, 0);
		this.onlineList1.Name = "onlineList1";
		this.onlineList1.Size = new System.Drawing.Size(160, 256);
		this.onlineList1.TabIndex = 3;
		this.functionButtons1._ActiveFunction = "";
		this.functionButtons1._CurrOpenMode = Archnowledge.Pcces.CommonClass.FunctionOpenMode.Budget;
		this.functionButtons1._ServerName = "localhost";
		this.functionButtons1._UserID = "PccesAdmin";
		this.functionButtons1._UserName = "";
		this.functionButtons1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.functionButtons1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.functionButtons1.Location = new System.Drawing.Point(0, 0);
		this.functionButtons1.Name = "functionButtons1";
		this.functionButtons1.Size = new System.Drawing.Size(160, 546);
		this.functionButtons1.TabIndex = 2;
		this.iglst_splt_Btn.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("iglst_splt_Btn.ImageStream");
		this.iglst_splt_Btn.TransparentColor = System.Drawing.Color.Transparent;
		this.iglst_splt_Btn.Images.SetKeyName(0, "");
		this.iglst_splt_Btn.Images.SetKeyName(1, "");
		this.iglst_splt_Btn.Images.SetKeyName(2, "");
		this.iglst_splt_Btn.Images.SetKeyName(3, "");
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Magenta;
		this.imageList2.Images.SetKeyName(0, "");
		this.imageList2.Images.SetKeyName(1, "");
		this.imageList2.Images.SetKeyName(2, "");
		this.imageList2.Images.SetKeyName(3, "");
		this.imageList2.Images.SetKeyName(4, "");
		this.imageList2.Images.SetKeyName(5, "");
		this.imageList2.Images.SetKeyName(6, "");
		this.imageList2.Images.SetKeyName(7, "");
		this.imageList2.Images.SetKeyName(8, "");
		this.imageList2.Images.SetKeyName(9, "btn_budCheck_Purple.bmp");
		this._FormProject_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormProject_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormProject_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormProject_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormProject_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormProject_Toolbars_Dock_Area_Top.Name = "_FormProject_Toolbars_Dock_Area_Top";
		this._FormProject_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(892, 27);
		this._FormProject_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		appearance7.FontData.Name = "Arial";
		appearance7.FontData.SizeInPoints = 9f;
		this.ultraToolbarsManager1.Appearance = appearance7;
		appearance8.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance8.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.DockAreaAppearance = appearance8;
		this.ultraToolbarsManager1.DockWithinContainer = this;
		this.ultraToolbarsManager1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraToolbarsManager1.LockToolbars = true;
		appearance9.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance9.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance9.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.MenuSettings.HotTrackAppearance = appearance9;
		appearance10.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance10.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsManager1.MenuSettings.IconAreaAppearance = appearance10;
		appearance11.BackColor = System.Drawing.Color.White;
		appearance11.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.MenuSettings.ToolAppearance = appearance11;
		optionSet1.AllowAllUp = false;
		this.ultraToolbarsManager1.OptionSets.Add(optionSet1);
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager1.ShowQuickCustomizeButton = false;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.Text = "工具列";
		popupMenuTool2.InstanceProps.IsFirstInGroup = true;
		buttonTool2.InstanceProps.IsFirstInGroup = true;
		popupMenuTool3.InstanceProps.IsFirstInGroup = true;
		buttonTool3.InstanceProps.IsFirstInGroup = true;
		popupMenuTool6.InstanceProps.IsFirstInGroup = true;
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[12]
		{
			popupMenuTool1, popupMenuTool2, labelTool1, comboBoxTool1, buttonTool1, buttonTool2, popupMenuTool3, popupMenuTool4, popupMenuTool5, buttonTool3,
			popupMenuTool6, buttonTool4
		});
		ultraToolbar2.DockedColumn = 0;
		ultraToolbar2.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		ultraToolbar2.DockedRow = 0;
		ultraToolbar2.Text = "COMS_Tools";
		ultraToolbar2.Visible = false;
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[2] { ultraToolbar1, ultraToolbar2 });
		appearance12.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance12.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance12;
		appearance13.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance13.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance13.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance13;
		appearance14.Image = resources.GetObject("appearance12.Image");
		buttonTool5.SharedProps.AppearancesSmall.Appearance = appearance14;
		buttonTool5.SharedProps.Caption = "建立空白專案";
		buttonTool5.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		appearance15.Image = resources.GetObject("appearance13.Image");
		buttonTool6.SharedProps.AppearancesSmall.Appearance = appearance15;
		buttonTool6.SharedProps.Caption = "刪除專案";
		buttonTool6.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool6.SharedProps.Shortcut = System.Windows.Forms.Shortcut.Del;
		labelTool2.SharedProps.Caption = "尋找:";
		labelTool2.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool2.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool2.SharedProps.Caption = "關鍵字";
		comboBoxTool2.SharedProps.Width = 200;
		comboBoxTool2.ValueList = valueList1;
		appearance16.Image = resources.GetObject("appearance14.Image");
		buttonTool7.SharedProps.AppearancesSmall.Appearance = appearance16;
		buttonTool7.SharedProps.Caption = "Go";
		popupMenuTool7.SharedProps.Caption = "右鍵功能選單";
		buttonTool8.InstanceProps.IsFirstInGroup = true;
		popupMenuTool8.InstanceProps.IsFirstInGroup = true;
		buttonTool9.InstanceProps.IsFirstInGroup = true;
		buttonTool10.InstanceProps.IsFirstInGroup = true;
		buttonTool12.InstanceProps.IsFirstInGroup = true;
		popupMenuTool7.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[6] { buttonTool8, popupMenuTool8, buttonTool9, buttonTool10, buttonTool11, buttonTool12 });
		appearance17.Image = resources.GetObject("appearance15.Image");
		buttonTool13.SharedProps.AppearancesSmall.Appearance = appearance17;
		buttonTool13.SharedProps.Caption = "XML 電子檔轉入";
		buttonTool13.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		popupMenuTool9.DropDownArrowStyle = Infragistics.Win.UltraWinToolbars.DropDownArrowStyle.Segmented;
		appearance18.Image = resources.GetObject("appearance16.Image");
		popupMenuTool9.SharedProps.AppearancesSmall.Appearance = appearance18;
		popupMenuTool9.SharedProps.Caption = "專案建立/轉入";
		popupMenuTool9.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool19.InstanceProps.IsFirstInGroup = true;
		popupMenuTool9.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[6] { buttonTool14, buttonTool15, buttonTool16, buttonTool17, buttonTool18, buttonTool19 });
		buttonTool20.SharedProps.Caption = "結束專案目錄";
		buttonTool21.SharedProps.Caption = "預算書 DIY 格式轉入";
		buttonTool21.SharedProps.Visible = false;
		buttonTool22.SharedProps.Caption = "建立分標專案";
		appearance19.Image = resources.GetObject("appearance17.Image");
		buttonTool23.SharedProps.AppearancesSmall.Appearance = appearance19;
		buttonTool23.SharedProps.Caption = "專案建立/轉入";
		buttonTool24.SharedProps.Caption = "建立併標專案";
		appearance20.Image = resources.GetObject("appearance18.Image");
		buttonTool25.SharedProps.AppearancesSmall.Appearance = appearance20;
		buttonTool25.SharedProps.Caption = "複製專案";
		buttonTool25.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool26.SharedProps.Caption = "最新消息";
		buttonTool26.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		popupMenuTool10.SharedProps.Caption = "附加工具(A)";
		popupMenuTool10.SharedProps.Visible = false;
		buttonTool27.SharedProps.Caption = "預算書";
		buttonTool27.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool28.SharedProps.Caption = "標單";
		buttonTool28.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		appearance21.Image = resources.GetObject("appearance19.Image");
		popupMenuTool11.SharedProps.AppearancesSmall.Appearance = appearance21;
		popupMenuTool11.SharedProps.Caption = "刪除專案";
		popupMenuTool11.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		popupMenuTool11.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool29, buttonTool30, buttonTool31 });
		buttonTool32.SharedProps.Caption = "設成預算書範本";
		buttonTool32.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool32.SharedProps.Enabled = false;
		buttonTool33.SharedProps.Caption = "取消預算書範本";
		buttonTool33.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool33.SharedProps.Enabled = false;
		popupMenuTool12.SharedProps.Caption = "預算書範本";
		popupMenuTool12.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		popupMenuTool12.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool34, buttonTool35 });
		popupMenuTool13.SharedProps.Caption = "說明(&H)";
		popupMenuTool13.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		popupMenuTool13.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[1] { buttonTool36 });
		popupMenuTool14.SharedProps.Caption = "檢視(&V)";
		popupMenuTool14.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		popupMenuTool14.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { stateButtonTool1, stateButtonTool2, stateButtonTool3 });
		stateButtonTool4.OptionSetKey = "view";
		stateButtonTool4.SharedProps.Caption = "只顯示有權限的專案";
		stateButtonTool4.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool5.OptionSetKey = "view";
		stateButtonTool5.SharedProps.Caption = "只顯示範本專案";
		stateButtonTool5.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool6.OptionSetKey = "view";
		stateButtonTool6.SharedProps.Caption = "顯示全部專案";
		stateButtonTool6.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool37.SharedProps.Caption = "標單轉決標預算書";
		buttonTool37.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool38.SharedProps.Caption = "複製標單至公司資料庫";
		buttonTool38.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool38.SharedProps.Visible = false;
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[29]
		{
			buttonTool5, buttonTool6, labelTool2, comboBoxTool2, buttonTool7, popupMenuTool7, buttonTool13, popupMenuTool9, buttonTool20, buttonTool21,
			buttonTool22, buttonTool23, buttonTool24, buttonTool25, buttonTool26, popupMenuTool10, buttonTool27, buttonTool28, popupMenuTool11, buttonTool32,
			buttonTool33, popupMenuTool12, popupMenuTool13, popupMenuTool14, stateButtonTool4, stateButtonTool5, stateButtonTool6, buttonTool37, buttonTool38
		});
		this.ultraToolbarsManager1.ToolKeyDown += new Infragistics.Win.UltraWinToolbars.ToolKeyEventHandler(ultraToolbarsManager1_ToolKeyDown);
		this.ultraToolbarsManager1.BeforeToolbarListDropdown += new Infragistics.Win.UltraWinToolbars.BeforeToolbarListDropdownEventHandler(ultraToolbarsManager1_BeforeToolbarListDropdown);
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this.ultraToolbarsManager1.AfterToolDeactivate += new Infragistics.Win.UltraWinToolbars.ToolEventHandler(ultraToolbarsManager1_AfterToolDeactivate);
		this.ultraToolbarsManager1.AfterToolActivate += new Infragistics.Win.UltraWinToolbars.ToolEventHandler(ultraToolbarsManager1_AfterToolActivate);
		this._FormProject_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormProject_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormProject_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormProject_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormProject_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 573);
		this._FormProject_Toolbars_Dock_Area_Bottom.Name = "_FormProject_Toolbars_Dock_Area_Bottom";
		this._FormProject_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(892, 0);
		this._FormProject_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormProject_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormProject_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormProject_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormProject_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormProject_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 27);
		this._FormProject_Toolbars_Dock_Area_Left.Name = "_FormProject_Toolbars_Dock_Area_Left";
		this._FormProject_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 546);
		this._FormProject_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormProject_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormProject_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormProject_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormProject_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormProject_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(892, 27);
		this._FormProject_Toolbars_Dock_Area_Right.Name = "_FormProject_Toolbars_Dock_Area_Right";
		this._FormProject_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 546);
		this._FormProject_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(892, 573);
		base.Controls.Add(this.FormProject_Fill_Panel);
		base.Controls.Add(this._FormProject_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormProject_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormProject_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormProject_Toolbars_Dock_Area_Bottom);
		base.KeyPreview = true;
		base.Name = "FormProject";
		this.Text = "專案目錄";
		base.Load += new System.EventHandler(FormProject_Load);
		base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(FormProject_FormClosed);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormProject_FormClosing);
		base.Resize += new System.EventHandler(FormProject_Resize);
		((System.ComponentModel.ISupportInitialize)this.gridProject).EndInit();
		this.FormProject_Fill_Panel.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ssp_GridCaption).EndInit();
		this.pnl_spliter.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ssp_Lower).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Bottom).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Upper).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Top).EndInit();
		this.LeftPanel.ResumeLayout(false);
		this.LeftPanel.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
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
