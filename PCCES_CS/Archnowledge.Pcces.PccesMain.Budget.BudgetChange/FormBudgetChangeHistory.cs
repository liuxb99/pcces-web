using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.BudExe;
using Archnowledge.Pcces.DomainModule.ExportExcel;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.PccesMain.MrsBase;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.Budget.BudgetChange;

public class FormBudgetChangeHistory : Form
{
	private string ProjectCode;

	private string UserID;

	private string ProjectName;

	private DataSet dsBudExeItemA;

	private BudExeItemA budExeItemA = new BudExeItemA();

	private IContainer components = null;

	private Panel PanelBudgetApproval;

	private Panel PanelButton;

	private RadioButton rBDisplayChanged;

	private UltraToolbarsManager ToolbarsMenu;

	private Panel PanelMenu;

	private UltraToolbarsDockArea _PanelMenu_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _PanelMenu_Toolbars_Dock_Area_Right;

	private UltraToolbarsDockArea _PanelMenu_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _PanelMenu_Toolbars_Dock_Area_Bottom;

	private RadioButton rBDisplayAll;

	private SplitContainer splitMenuAndGrid;

	private C1FlexGrid gridBudExeItemA;

	private SplitContainer splitGrid;

	private C1FlexGrid gridBudgetChangeProject;

	private Panel panelHeader;

	private Label lbProject;

	private UltraStatusBar statusBar;

	private UltraButton btnClose;

	private ImageList imageList;

	private SaveFileDialog saveReportDialog;

	public string _ProjectCode
	{
		get
		{
			return ProjectCode;
		}
		set
		{
			ProjectCode = value;
		}
	}

	public string _ProjectName
	{
		get
		{
			return ProjectName;
		}
		set
		{
			ProjectName = value;
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

	public FormBudgetChangeHistory()
	{
		InitializeComponent();
	}

	private void FormBudgetChangeHistory_Load(object sender, EventArgs e)
	{
		DataToGridBudgetChangeProject();
		rBDisplayAll.Checked = true;
		if (gridBudgetChangeProject.Rows.Count <= 2)
		{
			ToolbarsMenu.Tools["ProduceBudgetChangeReport"].SharedProps.Enabled = false;
		}
		if (SysConfig.SysChangeManagement)
		{
			ToolbarsMenu.Tools["ProduceBudgetChangeDetailReport"].SharedProps.Visible = true;
			ToolbarsMenu.Tools["ProduceBudgetChangeDetailReport"].SharedProps.Enabled = false;
		}
		gridBudgetChangeProject.SelChange += gridBudgetChangeProject_SelChange;
		rBDisplayAll.CheckedChanged += rBDisplay_CheckedChanged;
		GetData();
		DataToGridBudExeItemA(showChangedOnly: false);
		ToolbarsMenu.SetContextMenuUltra(gridBudExeItemA, "RightClickMenu");
	}

	private void DataToGridBudgetChangeProject()
	{
		BudExeProject budExeProject = new BudExeProject();
		DataSet dsBudExeProject = budExeProject.GetProject(ProjectCode);
		gridBudgetChangeProject.Redraw = false;
		gridBudgetChangeProject.Rows.Count = dsBudExeProject.Tables[0].Rows.Count;
		decimal PreviewAmount = 0m;
		for (int rowIndex = 0; rowIndex < dsBudExeProject.Tables[0].Rows.Count - 1; rowIndex++)
		{
			Row gridRow = gridBudgetChangeProject.Rows[rowIndex + 1];
			DataRow row = dsBudExeProject.Tables[0].Rows[rowIndex];
			gridRow["ChangeVersion"] = row["Version"];
			gridRow["ChangeDate"] = row["ChangeDate"];
			gridRow["Amount"] = row["Amount"];
			gridRow["Purpose"] = row["Purpose"];
			if (SysConfig.SysChangeManagement)
			{
				if (rowIndex > 0)
				{
					gridRow["ReapportionmentTotalAmount"] = Convert.ToDecimal(row["Amount"]) - PreviewAmount;
				}
				PreviewAmount = Convert.ToDecimal(row["Amount"]);
				gridRow["COMSExpandBudget"] = ((row["COMSExpandBudget"].ToString() == "True") ? "是" : "否");
			}
		}
		gridBudgetChangeProject.Redraw = true;
	}

	private void SetupGridStyle()
	{
		CellStyle csTransparent = gridBudExeItemA.Styles.Add("Transparent");
		csTransparent.ForeColor = Color.Transparent;
		CellStyle csAnalysisItem = gridBudExeItemA.Styles.Add("AnalysisItem");
		csAnalysisItem.ForeColor = Color.Red;
		CellStyle csMainItem = gridBudExeItemA.Styles.Add("MainItem");
		csMainItem.ForeColor = Color.Blue;
	}

	private void DataToGridBudExeItemA(bool showChangedOnly)
	{
		gridBudExeItemA.Rows.Count = 1;
		SetupGridStyle();
		gridBudExeItemA.Redraw = false;
		gridBudExeItemA.Rows.Count = dsBudExeItemA.Tables[0].Rows.Count + 1;
		statusBar.Panels[0].Text = "資料筆數：" + dsBudExeItemA.Tables[0].Rows.Count;
		string workItemType = string.Empty;
		string analysis = "";
		int maxLevel = 0;
		int gridRowIndex = 1;
		int showChangedOnlyCount = 0;
		for (int rowIndex = 0; rowIndex < dsBudExeItemA.Tables[0].Rows.Count; rowIndex++)
		{
			Row gridRow = gridBudExeItemA.Rows[gridRowIndex];
			DataRow row = dsBudExeItemA.Tables[0].Rows[rowIndex];
			if (showChangedOnly && row["ReApportionmentQty"] != DBNull.Value && ArchConvert.Obj2Double(row["ReApportionmentQty"]) == 0.0 && row["ReApportionmentAmount"] != DBNull.Value && ArchConvert.Obj2Double(row["ReApportionmentAmount"]) == 0.0)
			{
				continue;
			}
			try
			{
				workItemType = row["kind"].ToString().ToUpper();
				analysis = row["analysis"].ToString().Trim();
				switch (workItemType)
				{
				default:
					if (!(workItemType == "U"))
					{
						switch (analysis)
						{
						case "1":
						{
							gridRow.Style = gridBudExeItemA.Styles["AnalysisItem"];
							CellRange rg = default(CellRange);
							if (!showChangedOnly)
							{
								rg = gridBudExeItemA.GetCellRange(rowIndex + 1, gridBudExeItemA.Cols["Analysis"].SafeIndex);
							}
							else
							{
								rg = gridBudExeItemA.GetCellRange(showChangedOnlyCount + 1, gridBudExeItemA.Cols["Analysis"].SafeIndex);
								showChangedOnlyCount++;
							}
							rg.Style = gridBudExeItemA.Styles["img"];
							rg.Image = imageList.Images[0];
							break;
						}
						case "0":
						case "":
							if (showChangedOnly)
							{
								showChangedOnlyCount++;
							}
							break;
						}
						break;
					}
					goto case "B";
				case "B":
				case "L":
				case "F":
				case "S":
				case "Z":
					gridRow.Style = gridBudExeItemA.Styles["MainItem"];
					showChangedOnlyCount++;
					break;
				}
				gridRow["ItemNo"] = row["ItemNo"];
				gridRow["CName"] = row["cName"];
				gridRow["UnitName"] = row["unitName"];
				gridRow["PccesCode"] = row["pccesCode"];
				gridRow["PubCode"] = row["PubCode"];
				gridRow["SNo"] = row["sNo"];
				gridRow["Kind"] = workItemType;
				int QtyDec = ArchConvert.Obj2Int(row["QtyDec"]);
				int CostDec = ArchConvert.Obj2Int(row["CostDec"]);
				int AmtDec = ArchConvert.Obj2Int(row["AmtDec"]);
				if (row["CostKind"].ToString() != "#")
				{
					gridRow["Qty"] = row["qty"];
					gridRow["Cost"] = row["cost"];
					gridRow["Amount"] = row["amount"];
					CellStyle QtyDecStyle = gridBudExeItemA.Styles.Add("QtyDecStyle" + QtyDec);
					QtyDecStyle.Format = ((QtyDec > 0) ? ("###,###,###,##0." + "0".PadLeft(QtyDec, '0')) : "###,###,###,##0");
					gridBudExeItemA.SetCellStyle(rowIndex + 1, gridBudExeItemA.Cols["Qty"].SafeIndex, QtyDecStyle);
					gridBudExeItemA.SetCellStyle(rowIndex + 1, gridBudExeItemA.Cols["ReApportionmentQty"].SafeIndex, QtyDecStyle);
					CellStyle CostDecStyle = gridBudExeItemA.Styles.Add("CostDecStyle" + CostDec);
					CostDecStyle.Format = ((CostDec > 0) ? ("###,###,###,##0." + "0".PadLeft(CostDec, '0')) : "###,###,###,##0");
					gridBudExeItemA.SetCellStyle(rowIndex + 1, gridBudExeItemA.Cols["Cost"].SafeIndex, CostDecStyle);
					CellStyle AmyDecStyle = gridBudExeItemA.Styles.Add("AmtDec" + AmtDec);
					AmyDecStyle.Format = ((AmtDec > 0) ? ("###,###,###,##0." + "0".PadLeft(AmtDec, '0')) : "###,###,###,##0");
					gridBudExeItemA.SetCellStyle(rowIndex + 1, gridBudExeItemA.Cols["Amount"].SafeIndex, AmyDecStyle);
				}
				gridRow["ReApportionmentQty"] = row["ReApportionmentQty"];
				gridRow["ReApportionmentAmount"] = row["ReApportionmentAmount"];
				gridRow["BudgetChangeReason"] = row["BudgetChangeReason"];
				gridRow["VersionHistory"] = row["VersionHistory"];
				gridRow.IsNode = true;
				string printNo = row["PrintNo"].ToString().Trim();
				if (printNo == "".PadLeft(32, '9'))
				{
					gridRow.Node.Level = 1;
				}
				else if (printNo.Length == 4 && row["Kind"].ToString().Trim() == "Z" && rowIndex == dsBudExeItemA.Tables[0].Rows.Count - 1)
				{
					gridRow.Node.Level = 1;
				}
				else
				{
					gridRow.Node.Level = Convert.ToInt32(printNo.Length / 4);
				}
				if (gridRow.Node.Level > maxLevel)
				{
					maxLevel = gridRow.Node.Level;
				}
				gridRowIndex++;
			}
			catch (Exception ex)
			{
				MessageBox.Show("DataToGridBudExeItemA Error : " + ex.Message);
			}
		}
		gridBudExeItemA.Rows.Count = gridRowIndex;
		InitializeMenuLevel(maxLevel);
		gridBudExeItemA.Redraw = true;
	}

	private void gridBudgetChangeProject_SelChange(object sender, EventArgs e)
	{
		bool isOriginalBudget = gridBudgetChangeProject.Row == 1;
		ToolbarsMenu.Tools["BudgetChangeInfo"].SharedProps.Enabled = !isOriginalBudget;
		rBDisplayChanged.Enabled = !isOriginalBudget;
		ToolbarsMenu.Tools["ProduceBudgetChangeDetailReport"].SharedProps.Enabled = gridBudgetChangeProject.Row > 1;
		GetData();
		DataToGridBudExeItemA(rBDisplayChanged.Checked);
	}

	private void GetData()
	{
		int version = ArchConvert.Obj2Int(gridBudgetChangeProject.Rows[gridBudgetChangeProject.Row]["ChangeVersion"]);
		dsBudExeItemA = budExeItemA.GetItemA(ProjectCode, version);
	}

	private void InitializeMenuLevel(int MaxLevel)
	{
		for (int index = 1; index <= 8; index++)
		{
			if (index < MaxLevel)
			{
				((StateButtonTool)ToolbarsMenu.Tools["Level" + index]).SharedProps.Enabled = true;
			}
			else if (index == MaxLevel)
			{
				((StateButtonTool)ToolbarsMenu.Tools["Level" + index]).Checked = true;
				((StateButtonTool)ToolbarsMenu.Tools["Level" + index]).SharedProps.Enabled = true;
			}
			else
			{
				((StateButtonTool)ToolbarsMenu.Tools["Level" + index]).SharedProps.Enabled = false;
			}
		}
	}

	private void ToolbarsMenu_ToolClick(object sender, ToolClickEventArgs e)
	{
		switch (e.Tool.Key)
		{
		case "Go":
			gridBudExeItemA.Row = getKeywordMatchedRowIndex(gridBudExeItemA.Row + 1, ((TextBoxTool)ToolbarsMenu.Tools["keyword"]).Text.Trim());
			break;
		case "BudgetChangeInfo":
			OpenFormBudgetChangeInfo();
			break;
		case "ProduceBudgetChangeReport":
			saveReportDialog.FileName = "歷次變更比較表";
			if (saveReportDialog.ShowDialog() == DialogResult.OK)
			{
				ProduceBudgetChangeReport();
			}
			break;
		case "ViewBudgetChangeResponsibility":
			OpenFormBudgetChangeResponsibility();
			break;
		case "ProduceBudgetChangeDetailReport":
			saveReportDialog.FileName = "預算變更明細表";
			if (saveReportDialog.ShowDialog() == DialogResult.OK)
			{
				ProduceBudgetChangeDetailReport();
			}
			break;
		default:
			if (e.Tool.Key.StartsWith("Level"))
			{
				int Level = int.Parse(e.Tool.Key.Substring(5, 1));
				gridBudExeItemA.Tree.Show(Level);
			}
			break;
		}
	}

	private int getKeywordMatchedRowIndex(int startRow, string keyword)
	{
		if (keyword == string.Empty)
		{
			return gridBudExeItemA.Row;
		}
		for (int row = startRow; row < gridBudExeItemA.Rows.Count; row++)
		{
			for (int column = 1; column < gridBudExeItemA.Cols.Count - 1; column++)
			{
				if (gridBudExeItemA.Rows[row][column] != null && gridBudExeItemA.Rows[row][column].ToString().Contains(keyword))
				{
					return row;
				}
			}
		}
		return (startRow != 1) ? getKeywordMatchedRowIndex(1, keyword) : gridBudExeItemA.Row;
	}

	private void OpenFormBudgetChangeInfo()
	{
		FormBudgetChangeInfo formBudgetChangeInfo = new FormBudgetChangeInfo();
		formBudgetChangeInfo._projectCode = ProjectCode;
		formBudgetChangeInfo._version = gridBudgetChangeProject.Row - 1;
		if (SysConfig.SysChangeManagement)
		{
			formBudgetChangeInfo._openMode = FormBudgetChangeInfo.Mode.Edit;
			formBudgetChangeInfo._userID = UserID;
		}
		else
		{
			formBudgetChangeInfo._openMode = FormBudgetChangeInfo.Mode.ReadOnly;
		}
		formBudgetChangeInfo.ShowDialog();
		formBudgetChangeInfo.Dispose();
		formBudgetChangeInfo = null;
	}

	private void ProduceBudgetChangeReport()
	{
		BudgetChangeHistoryReporterGenerator reportGenerator = new BudgetChangeHistoryReporterGenerator();
		ExecResult ER = reportGenerator.ProduceBudgetChangeReport(saveReportDialog.FileName, ProjectCode, gridBudgetChangeProject.Rows.Count - 1);
		if (ER.ReturnCode == 0)
		{
			MessageBox.Show("產出歷次變更比較表成功！");
		}
		else
		{
			MessageBox.Show(ER.Message);
		}
	}

	private void ProduceBudgetChangeDetailReport()
	{
		bool IsSetCostEmpty = false;
		if (MessageBox.Show(this, "是否不列印單價?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
		{
			IsSetCostEmpty = true;
		}
		HistoryExecutiveBudgetDetailReport report = new HistoryExecutiveBudgetDetailReport();
		int version = ArchConvert.Obj2Int(gridBudgetChangeProject.Rows[gridBudgetChangeProject.Row]["ChangeVersion"]);
		ExecResult ER = report.ProduceHistoryExecutiveBudgetDetailReport(saveReportDialog.FileName, ProjectCode, ProjectName, version, IsSetCostEmpty);
		if (ER.ReturnCode == 0)
		{
			FormOpenExcel _OpenExcel = new FormOpenExcel();
			_OpenExcel.filepath = saveReportDialog.FileName;
			_OpenExcel.ResetLable();
			_OpenExcel.ShowDialog();
			_OpenExcel.Close();
			_OpenExcel.Dispose();
		}
		else
		{
			MessageBox.Show(ER.Message);
		}
	}

	private void OpenFormBudgetChangeResponsibility()
	{
		int version = ArchConvert.Obj2Int(gridBudgetChangeProject.Rows[gridBudgetChangeProject.Row]["ChangeVersion"]);
		Row gridRow = gridBudExeItemA.Rows[gridBudExeItemA.Row];
		int sNo = ArchConvert.Obj2Int(gridRow["sNo"]);
		FormBudgetChangeResponsibility formBudgetChangeResponsibility = new FormBudgetChangeResponsibility(ProjectCode, version, sNo);
		formBudgetChangeResponsibility.ViewMode = true;
		formBudgetChangeResponsibility.ItemNo = ArchConvert.Obj2String(gridRow["ItemNo"]);
		formBudgetChangeResponsibility.ItemName = ArchConvert.Obj2String(gridRow["cName"]);
		formBudgetChangeResponsibility.ShowDialog();
		formBudgetChangeResponsibility.Dispose();
		formBudgetChangeResponsibility = null;
	}

	private void ToolbarsMenu_ToolKeyPress(object sender, ToolKeyPressEventArgs e)
	{
		if (e.Tool.Key == "Keyword" && e.KeyChar == '\r')
		{
			gridBudExeItemA.Row = getKeywordMatchedRowIndex(gridBudExeItemA.Row + 1, ((TextBoxTool)ToolbarsMenu.Tools["keyword"]).Text.Trim());
		}
	}

	private void rBDisplay_CheckedChanged(object sender, EventArgs e)
	{
		DataToGridBudExeItemA(rBDisplayChanged.Checked);
	}

	private void gridBudExeItemA_MouseDown(object sender, MouseEventArgs e)
	{
		int rowIndex = gridBudExeItemA.MouseRow;
		if (e.Button == MouseButtons.Right && rowIndex != 0)
		{
			gridBudExeItemA.Row = rowIndex;
		}
	}

	private void gridBudExeItemA_Click(object sender, EventArgs e)
	{
		if (gridBudExeItemA.MouseRow > 0 && gridBudExeItemA.MouseCol > 0)
		{
			int rowIndex = gridBudExeItemA.MouseRow;
			int colIndex = gridBudExeItemA.MouseCol;
			if (gridBudExeItemA.Cols[colIndex].Name == "Analysis" && gridBudExeItemA.GetCellRange(rowIndex, gridBudExeItemA.Cols["Analysis"].SafeIndex).Image != null)
			{
				int version = ArchConvert.Obj2Int(gridBudgetChangeProject.Rows[gridBudgetChangeProject.Row]["ChangeVersion"]);
				FormMrsBaseBreakdown formMrsBaseBreakdown = new FormMrsBaseBreakdown();
				formMrsBaseBreakdown.PubCode = (int)gridBudExeItemA[gridBudExeItemA.Row, "pubCode"];
				formMrsBaseBreakdown.ProjectCode = ProjectCode;
				formMrsBaseBreakdown._ActionName = PccesFormAction.BUDEXE;
				formMrsBaseBreakdown._Issue = version;
				formMrsBaseBreakdown._UserID = UserID;
				formMrsBaseBreakdown._Istemplate = true;
				formMrsBaseBreakdown._IsUseIR = true;
				formMrsBaseBreakdown._Istemplate = true;
				formMrsBaseBreakdown.Owner = this;
				formMrsBaseBreakdown.ShowDialog();
				formMrsBaseBreakdown.Close();
				formMrsBaseBreakdown.Dispose();
				formMrsBaseBreakdown = null;
			}
		}
	}

	private void gridBudExeItemA_MouseMove(object sender, MouseEventArgs e)
	{
		try
		{
			int RowIndex = gridBudExeItemA.MouseRow;
			int ColIndex = gridBudExeItemA.MouseCol;
			Archnowledge.Common.DebugUtil.OutputDebugString("gridBudExeItemA_MouseMove (" + RowIndex + "," + ColIndex + ")");
			if (RowIndex > 0 && ColIndex > 0)
			{
				Row GridRow = gridBudExeItemA.Rows[RowIndex];
				string ColumnName = gridBudExeItemA.Cols[ColIndex].Name;
				Cursor = Cursors.Default;
				if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right && ColumnName == "Analysis" && GridRow["Analysis"] != null && ArchConvert.Obj2Bool(GridRow["Analysis"]))
				{
					Cursor = Cursors.Hand;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("gridBudExeItemA_MouseMove Error:" + ex.Message);
		}
	}

	private void gridBudExeItemA_AfterSelChange(object sender, RangeEventArgs e)
	{
		try
		{
			int RowIndex = gridBudExeItemA.MouseRow;
			int ColIndex = gridBudExeItemA.MouseCol;
			Archnowledge.Common.DebugUtil.OutputDebugString("gridBudExeItemA_MouseMove (" + RowIndex + "," + ColIndex + ")");
			if (RowIndex <= 0 || ColIndex <= 0 || RowIndex > gridBudExeItemA.Rows.Count - 1)
			{
				ToolbarsMenu.SetContextMenuUltra(gridBudExeItemA, null);
				return;
			}
			ToolbarsMenu.Tools["ViewBudgetChangeResponsibility"].SharedProps.Enabled = ArchConvert.Obj2String(gridBudExeItemA[gridBudExeItemA.Row, "Kind"]) == "W";
			ToolbarsMenu.SetContextMenuUltra(gridBudExeItemA, "RightClickMenu");
		}
		catch (Exception ex)
		{
			MessageBox.Show("gridBudExeItemA_AfterSelChange Error:" + ex.Message);
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.OptionSet optionSet2 = new Infragistics.Win.UltraWinToolbars.OptionSet("Switch");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar2 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("ShowAndDisplay");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool11 = new Infragistics.Win.UltraWinToolbars.ButtonTool("BudgetChangeInfo");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool12 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ProduceBudgetChangeReport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool13 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ProduceBudgetChangeDetailReport");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool5 = new Infragistics.Win.UltraWinToolbars.LabelTool("lbSort");
		Infragistics.Win.UltraWinToolbars.TextBoxTool textBoxTool3 = new Infragistics.Win.UltraWinToolbars.TextBoxTool("Keyword");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool14 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Go");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool6 = new Infragistics.Win.UltraWinToolbars.LabelTool("Level");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool17 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level1", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool18 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level2", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool19 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level3", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool20 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level4", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool21 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level5", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool22 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level6", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool23 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level7", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool24 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level8", "Switch");
		Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool5 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("CCShow");
		Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool6 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("CCShowChanged");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool7 = new Infragistics.Win.UltraWinToolbars.LabelTool("lbSort");
		Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool7 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("CCShow");
		Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool8 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("CCShowChanged");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool8 = new Infragistics.Win.UltraWinToolbars.LabelTool("Level");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool25 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level1", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool26 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level2", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool27 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level3", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool28 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level4", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool29 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level5", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool30 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level6", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool31 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level7", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool32 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level8", "Switch");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool15 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ProduceBudgetChangeReport");
		Infragistics.Win.UltraWinToolbars.TextBoxTool textBoxTool4 = new Infragistics.Win.UltraWinToolbars.TextBoxTool("Keyword");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool16 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Go");
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool17 = new Infragistics.Win.UltraWinToolbars.ButtonTool("BudgetChangeInfo");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool18 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ViewBudgetChangeResponsibility");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool2 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("RightClickMenu");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool19 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ViewBudgetChangeResponsibility");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool20 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ProduceBudgetChangeDetailReport");
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.BudgetChange.FormBudgetChangeHistory));
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel4 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel5 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel6 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		this.rBDisplayAll = new System.Windows.Forms.RadioButton();
		this.rBDisplayChanged = new System.Windows.Forms.RadioButton();
		this.PanelBudgetApproval = new System.Windows.Forms.Panel();
		this.splitMenuAndGrid = new System.Windows.Forms.SplitContainer();
		this.PanelMenu = new System.Windows.Forms.Panel();
		this.panelHeader = new System.Windows.Forms.Panel();
		this.lbProject = new System.Windows.Forms.Label();
		this._PanelMenu_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.ToolbarsMenu = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this.imageList = new System.Windows.Forms.ImageList(this.components);
		this._PanelMenu_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._PanelMenu_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._PanelMenu_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.splitGrid = new System.Windows.Forms.SplitContainer();
		this.gridBudgetChangeProject = new C1.Win.C1FlexGrid.C1FlexGrid();
		this.gridBudExeItemA = new C1.Win.C1FlexGrid.C1FlexGrid();
		this.statusBar = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.PanelButton = new System.Windows.Forms.Panel();
		this.btnClose = new Infragistics.Win.Misc.UltraButton();
		this.saveReportDialog = new System.Windows.Forms.SaveFileDialog();
		this.PanelBudgetApproval.SuspendLayout();
		this.splitMenuAndGrid.Panel1.SuspendLayout();
		this.splitMenuAndGrid.Panel2.SuspendLayout();
		this.splitMenuAndGrid.SuspendLayout();
		this.PanelMenu.SuspendLayout();
		this.panelHeader.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ToolbarsMenu).BeginInit();
		this.splitGrid.Panel1.SuspendLayout();
		this.splitGrid.Panel2.SuspendLayout();
		this.splitGrid.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridBudgetChangeProject).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridBudExeItemA).BeginInit();
		this.PanelButton.SuspendLayout();
		base.SuspendLayout();
		this.rBDisplayAll.AutoSize = true;
		this.rBDisplayAll.BackColor = System.Drawing.Color.Transparent;
		this.rBDisplayAll.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rBDisplayAll.Font = new System.Drawing.Font("新細明體", 9f);
		this.rBDisplayAll.Location = new System.Drawing.Point(752, 15);
		this.rBDisplayAll.Name = "rBDisplayAll";
		this.rBDisplayAll.Size = new System.Drawing.Size(85, 19);
		this.rBDisplayAll.TabIndex = 14;
		this.rBDisplayAll.TabStop = true;
		this.rBDisplayAll.Text = "全部顯示";
		this.rBDisplayAll.UseVisualStyleBackColor = false;
		this.rBDisplayChanged.AutoSize = true;
		this.rBDisplayChanged.BackColor = System.Drawing.Color.Transparent;
		this.rBDisplayChanged.Font = new System.Drawing.Font("新細明體", 9f);
		this.rBDisplayChanged.Location = new System.Drawing.Point(843, 15);
		this.rBDisplayChanged.Name = "rBDisplayChanged";
		this.rBDisplayChanged.Size = new System.Drawing.Size(130, 19);
		this.rBDisplayChanged.TabIndex = 15;
		this.rBDisplayChanged.TabStop = true;
		this.rBDisplayChanged.Text = "只顯示變更項目";
		this.rBDisplayChanged.UseVisualStyleBackColor = false;
		this.PanelBudgetApproval.BackColor = System.Drawing.Color.Transparent;
		this.PanelBudgetApproval.Controls.Add(this.splitMenuAndGrid);
		this.PanelBudgetApproval.Controls.Add(this.PanelButton);
		this.PanelBudgetApproval.Cursor = System.Windows.Forms.Cursors.Default;
		this.PanelBudgetApproval.Dock = System.Windows.Forms.DockStyle.Fill;
		this.PanelBudgetApproval.Font = new System.Drawing.Font("新細明體", 11f);
		this.PanelBudgetApproval.Location = new System.Drawing.Point(0, 0);
		this.PanelBudgetApproval.Name = "PanelBudgetApproval";
		this.PanelBudgetApproval.Size = new System.Drawing.Size(1117, 730);
		this.PanelBudgetApproval.TabIndex = 0;
		this.splitMenuAndGrid.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitMenuAndGrid.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		this.splitMenuAndGrid.IsSplitterFixed = true;
		this.splitMenuAndGrid.Location = new System.Drawing.Point(0, 0);
		this.splitMenuAndGrid.Name = "splitMenuAndGrid";
		this.splitMenuAndGrid.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitMenuAndGrid.Panel1.Controls.Add(this.PanelMenu);
		this.splitMenuAndGrid.Panel1MinSize = 0;
		this.splitMenuAndGrid.Panel2.Controls.Add(this.splitGrid);
		this.splitMenuAndGrid.Panel2MinSize = 0;
		this.splitMenuAndGrid.Size = new System.Drawing.Size(1117, 687);
		this.splitMenuAndGrid.SplitterDistance = 60;
		this.splitMenuAndGrid.SplitterWidth = 1;
		this.splitMenuAndGrid.TabIndex = 19;
		this.PanelMenu.Controls.Add(this.panelHeader);
		this.PanelMenu.Controls.Add(this._PanelMenu_Toolbars_Dock_Area_Left);
		this.PanelMenu.Controls.Add(this._PanelMenu_Toolbars_Dock_Area_Right);
		this.PanelMenu.Controls.Add(this._PanelMenu_Toolbars_Dock_Area_Top);
		this.PanelMenu.Controls.Add(this._PanelMenu_Toolbars_Dock_Area_Bottom);
		this.PanelMenu.Dock = System.Windows.Forms.DockStyle.Top;
		this.PanelMenu.Location = new System.Drawing.Point(0, 0);
		this.PanelMenu.Name = "PanelMenu";
		this.PanelMenu.Size = new System.Drawing.Size(1117, 56);
		this.PanelMenu.TabIndex = 18;
		this.panelHeader.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.panelHeader.Controls.Add(this.lbProject);
		this.panelHeader.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panelHeader.Location = new System.Drawing.Point(0, 27);
		this.panelHeader.Name = "panelHeader";
		this.panelHeader.Size = new System.Drawing.Size(1117, 29);
		this.panelHeader.TabIndex = 4;
		this.lbProject.AutoSize = true;
		this.lbProject.Location = new System.Drawing.Point(13, 7);
		this.lbProject.Name = "lbProject";
		this.lbProject.Size = new System.Drawing.Size(82, 15);
		this.lbProject.TabIndex = 0;
		this.lbProject.Text = "目前專案：";
		this._PanelMenu_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._PanelMenu_Toolbars_Dock_Area_Left.BackColor = System.Drawing.SystemColors.Control;
		this._PanelMenu_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._PanelMenu_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._PanelMenu_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 27);
		this._PanelMenu_Toolbars_Dock_Area_Left.Name = "_PanelMenu_Toolbars_Dock_Area_Left";
		this._PanelMenu_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 29);
		this._PanelMenu_Toolbars_Dock_Area_Left.ToolbarsManager = this.ToolbarsMenu;
		this.ToolbarsMenu.AlwaysShowFullMenus = true;
		appearance4.FontData.Name = "Arial";
		appearance4.FontData.SizeInPoints = 9f;
		this.ToolbarsMenu.Appearance = appearance4;
		this.ToolbarsMenu.DockWithinContainer = this.PanelMenu;
		this.ToolbarsMenu.ImageListSmall = this.imageList;
		this.ToolbarsMenu.LockToolbars = true;
		this.ToolbarsMenu.MenuSettings.IsSideStripVisible = Infragistics.Win.DefaultableBoolean.False;
		optionSet2.AllowAllUp = false;
		this.ToolbarsMenu.OptionSets.Add(optionSet2);
		this.ToolbarsMenu.ShowFullMenusDelay = 500;
		this.ToolbarsMenu.ShowQuickCustomizeButton = false;
		this.ToolbarsMenu.ShowToolTips = false;
		this.ToolbarsMenu.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar2.DockedColumn = 0;
		ultraToolbar2.DockedRow = 0;
		ultraToolbar2.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
		ultraToolbar2.Text = "ShowAndDisplay";
		buttonTool12.InstanceProps.IsFirstInGroup = true;
		labelTool5.InstanceProps.IsFirstInGroup = true;
		textBoxTool3.InstanceProps.Width = 141;
		labelTool6.InstanceProps.IsFirstInGroup = true;
		stateButtonTool17.Checked = true;
		controlContainerTool5.Control = this.rBDisplayAll;
		controlContainerTool5.InstanceProps.IsFirstInGroup = true;
		controlContainerTool5.InstanceProps.Width = 87;
		controlContainerTool6.Control = this.rBDisplayChanged;
		ultraToolbar2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[17]
		{
			buttonTool11, buttonTool12, buttonTool13, labelTool5, textBoxTool3, buttonTool14, labelTool6, stateButtonTool17, stateButtonTool18, stateButtonTool19,
			stateButtonTool20, stateButtonTool21, stateButtonTool22, stateButtonTool23, stateButtonTool24, controlContainerTool5, controlContainerTool6
		});
		this.ToolbarsMenu.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[1] { ultraToolbar2 });
		this.ToolbarsMenu.ToolbarSettings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
		this.ToolbarsMenu.ToolbarSettings.AllowDockBottom = Infragistics.Win.DefaultableBoolean.False;
		this.ToolbarsMenu.ToolbarSettings.AllowDockLeft = Infragistics.Win.DefaultableBoolean.False;
		this.ToolbarsMenu.ToolbarSettings.AllowDockRight = Infragistics.Win.DefaultableBoolean.False;
		this.ToolbarsMenu.ToolbarSettings.AllowDockTop = Infragistics.Win.DefaultableBoolean.False;
		this.ToolbarsMenu.ToolbarSettings.AllowFloating = Infragistics.Win.DefaultableBoolean.False;
		this.ToolbarsMenu.ToolbarSettings.AllowHiding = Infragistics.Win.DefaultableBoolean.False;
		labelTool7.SharedProps.Caption = "尋找：";
		controlContainerTool7.Control = this.rBDisplayAll;
		controlContainerTool7.SharedProps.Caption = "CCShowAll";
		controlContainerTool7.SharedProps.MaxWidth = 100;
		controlContainerTool7.SharedProps.MinWidth = 100;
		controlContainerTool7.SharedProps.Width = 87;
		controlContainerTool7.VerticalDisplayStyle = Infragistics.Win.UltraWinToolbars.VerticalDisplayStyle.Hide;
		controlContainerTool8.Control = this.rBDisplayChanged;
		controlContainerTool8.SharedProps.Caption = "CCShowChanged";
		labelTool8.SharedProps.Caption = "階層:";
		labelTool8.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool25.Checked = true;
		stateButtonTool25.OptionSetKey = "Switch";
		stateButtonTool25.SharedProps.Caption = "1";
		stateButtonTool25.SharedProps.Category = "LevelSwitch";
		stateButtonTool25.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool26.OptionSetKey = "Switch";
		stateButtonTool26.SharedProps.Caption = "2";
		stateButtonTool26.SharedProps.Category = "LevelSwitch";
		stateButtonTool26.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool27.OptionSetKey = "Switch";
		stateButtonTool27.SharedProps.Caption = "3";
		stateButtonTool27.SharedProps.Category = "LevelSwitch";
		stateButtonTool27.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool28.OptionSetKey = "Switch";
		stateButtonTool28.SharedProps.Caption = "4";
		stateButtonTool28.SharedProps.Category = "LevelSwitch";
		stateButtonTool28.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool29.OptionSetKey = "Switch";
		stateButtonTool29.SharedProps.Caption = "5";
		stateButtonTool29.SharedProps.Category = "LevelSwitch";
		stateButtonTool29.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool30.OptionSetKey = "Switch";
		stateButtonTool30.SharedProps.Caption = "6";
		stateButtonTool30.SharedProps.Category = "LevelSwitch";
		stateButtonTool30.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool31.OptionSetKey = "Switch";
		stateButtonTool31.SharedProps.Caption = "7";
		stateButtonTool31.SharedProps.Category = "LevelSwitch";
		stateButtonTool31.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool32.OptionSetKey = "Switch";
		stateButtonTool32.SharedProps.Caption = "8";
		stateButtonTool32.SharedProps.Category = "LevelSwitch";
		stateButtonTool32.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool15.SharedProps.Caption = "歷次變更比較表";
		buttonTool15.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		textBoxTool4.SharedProps.Caption = "Keyword";
		appearance5.Image = resources.GetObject("appearance5.Image");
		buttonTool16.SharedProps.AppearancesSmall.Appearance = appearance5;
		buttonTool16.SharedProps.Caption = "執行尋找";
		buttonTool17.SharedProps.Caption = "變更版次資訊";
		buttonTool17.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool17.SharedProps.Enabled = false;
		buttonTool18.SharedProps.Caption = "責任歸屬";
		popupMenuTool2.SharedProps.Caption = "右鍵選單";
		popupMenuTool2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[1] { buttonTool19 });
		buttonTool20.SharedProps.Caption = "預算變更明細表";
		buttonTool20.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool20.SharedProps.Visible = false;
		this.ToolbarsMenu.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[19]
		{
			labelTool7, controlContainerTool7, controlContainerTool8, labelTool8, stateButtonTool25, stateButtonTool26, stateButtonTool27, stateButtonTool28, stateButtonTool29, stateButtonTool30,
			stateButtonTool31, stateButtonTool32, buttonTool15, textBoxTool4, buttonTool16, buttonTool17, buttonTool18, popupMenuTool2, buttonTool20
		});
		this.ToolbarsMenu.ToolKeyPress += new Infragistics.Win.UltraWinToolbars.ToolKeyPressEventHandler(ToolbarsMenu_ToolKeyPress);
		this.ToolbarsMenu.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ToolbarsMenu_ToolClick);
		this.imageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList.ImageStream");
		this.imageList.TransparentColor = System.Drawing.Color.Magenta;
		this.imageList.Images.SetKeyName(0, "btn_272.bmp");
		this._PanelMenu_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._PanelMenu_Toolbars_Dock_Area_Right.BackColor = System.Drawing.SystemColors.Control;
		this._PanelMenu_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._PanelMenu_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._PanelMenu_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(1117, 27);
		this._PanelMenu_Toolbars_Dock_Area_Right.Name = "_PanelMenu_Toolbars_Dock_Area_Right";
		this._PanelMenu_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 29);
		this._PanelMenu_Toolbars_Dock_Area_Right.ToolbarsManager = this.ToolbarsMenu;
		this._PanelMenu_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._PanelMenu_Toolbars_Dock_Area_Top.BackColor = System.Drawing.SystemColors.Control;
		this._PanelMenu_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._PanelMenu_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._PanelMenu_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._PanelMenu_Toolbars_Dock_Area_Top.Name = "_PanelMenu_Toolbars_Dock_Area_Top";
		this._PanelMenu_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(1117, 27);
		this._PanelMenu_Toolbars_Dock_Area_Top.ToolbarsManager = this.ToolbarsMenu;
		this._PanelMenu_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._PanelMenu_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.SystemColors.Control;
		this._PanelMenu_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._PanelMenu_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._PanelMenu_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 56);
		this._PanelMenu_Toolbars_Dock_Area_Bottom.Name = "_PanelMenu_Toolbars_Dock_Area_Bottom";
		this._PanelMenu_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(1117, 0);
		this._PanelMenu_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ToolbarsMenu;
		this.splitGrid.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitGrid.Location = new System.Drawing.Point(0, 0);
		this.splitGrid.Name = "splitGrid";
		this.splitGrid.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitGrid.Panel1.Controls.Add(this.gridBudgetChangeProject);
		this.splitGrid.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.splitGrid.Panel2.Controls.Add(this.gridBudExeItemA);
		this.splitGrid.Panel2.Controls.Add(this.statusBar);
		this.splitGrid.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.splitGrid.Size = new System.Drawing.Size(1117, 626);
		this.splitGrid.SplitterDistance = 210;
		this.splitGrid.TabIndex = 2;
		this.gridBudgetChangeProject.AllowEditing = false;
		this.gridBudgetChangeProject.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridBudgetChangeProject.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridBudgetChangeProject.ColumnInfo = resources.GetString("gridBudgetChangeProject.ColumnInfo");
		this.gridBudgetChangeProject.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridBudgetChangeProject.Font = new System.Drawing.Font("細明體", 11f);
		this.gridBudgetChangeProject.ForeColor = System.Drawing.Color.Black;
		this.gridBudgetChangeProject.Location = new System.Drawing.Point(0, 0);
		this.gridBudgetChangeProject.Name = "gridBudgetChangeProject";
		this.gridBudgetChangeProject.Rows.Count = 1;
		this.gridBudgetChangeProject.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.gridBudgetChangeProject.ShowCursor = true;
		this.gridBudgetChangeProject.ShowSort = false;
		this.gridBudgetChangeProject.Size = new System.Drawing.Size(1117, 210);
		this.gridBudgetChangeProject.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridBudgetChangeProject.Styles"));
		this.gridBudgetChangeProject.TabIndex = 2;
		this.gridBudgetChangeProject.Tree.Column = 2;
		this.gridBudgetChangeProject.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridBudExeItemA.AllowEditing = false;
		this.gridBudExeItemA.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridBudExeItemA.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridBudExeItemA.ColumnInfo = resources.GetString("gridBudExeItemA.ColumnInfo");
		this.gridBudExeItemA.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridBudExeItemA.Font = new System.Drawing.Font("細明體", 11f);
		this.gridBudExeItemA.ForeColor = System.Drawing.Color.Black;
		this.gridBudExeItemA.Location = new System.Drawing.Point(0, 0);
		this.gridBudExeItemA.Name = "gridBudExeItemA";
		this.gridBudExeItemA.Rows.Count = 1;
		this.gridBudExeItemA.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.gridBudExeItemA.ShowCursor = true;
		this.gridBudExeItemA.ShowSort = false;
		this.gridBudExeItemA.Size = new System.Drawing.Size(1117, 386);
		this.gridBudExeItemA.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridBudExeItemA.Styles"));
		this.gridBudExeItemA.TabIndex = 1;
		this.gridBudExeItemA.Tree.Column = 1;
		this.gridBudExeItemA.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridBudExeItemA.Click += new System.EventHandler(gridBudExeItemA_Click);
		this.gridBudExeItemA.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(gridBudExeItemA_AfterSelChange);
		this.gridBudExeItemA.MouseDown += new System.Windows.Forms.MouseEventHandler(gridBudExeItemA_MouseDown);
		this.gridBudExeItemA.MouseMove += new System.Windows.Forms.MouseEventHandler(gridBudExeItemA_MouseMove);
		this.statusBar.Location = new System.Drawing.Point(0, 386);
		this.statusBar.Name = "statusBar";
		ultraStatusPanel4.Key = "DataCount";
		ultraStatusPanel4.Text = "資料筆數：";
		ultraStatusPanel4.Width = 150;
		ultraStatusPanel5.Key = "Reapportionment";
		ultraStatusPanel5.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		ultraStatusPanel6.Key = "Phone";
		ultraStatusPanel6.Text = "客服電話：(02)2716-5561";
		ultraStatusPanel6.Width = 180;
		this.statusBar.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[3] { ultraStatusPanel4, ultraStatusPanel5, ultraStatusPanel6 });
		this.statusBar.Size = new System.Drawing.Size(1117, 26);
		this.statusBar.TabIndex = 3;
		this.PanelButton.Controls.Add(this.btnClose);
		this.PanelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.PanelButton.Location = new System.Drawing.Point(0, 687);
		this.PanelButton.Name = "PanelButton";
		this.PanelButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.PanelButton.Size = new System.Drawing.Size(1117, 43);
		this.PanelButton.TabIndex = 9;
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		appearance6.Image = resources.GetObject("appearance1.Image");
		appearance6.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnClose.Appearance = appearance6;
		this.btnClose.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnClose.ImageSize = new System.Drawing.Size(20, 20);
		this.btnClose.ImageTransparentColor = System.Drawing.Color.White;
		this.btnClose.Location = new System.Drawing.Point(1020, 6);
		this.btnClose.Margin = new System.Windows.Forms.Padding(0);
		this.btnClose.Name = "btnClose";
		this.btnClose.ShowFocusRect = false;
		this.btnClose.ShowOutline = false;
		this.btnClose.Size = new System.Drawing.Size(88, 31);
		this.btnClose.SupportThemes = false;
		this.btnClose.TabIndex = 8;
		this.btnClose.Text = "關閉";
		this.saveReportDialog.FileName = "歷次變更比較表.xls";
		this.saveReportDialog.Filter = "Excel 活頁簿|*.xls";
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.btnClose;
		base.ClientSize = new System.Drawing.Size(1117, 730);
		base.Controls.Add(this.PanelBudgetApproval);
		this.Font = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Margin = new System.Windows.Forms.Padding(4);
		base.Name = "FormBudgetChangeHistory";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		this.Text = "預算變更歷史版次";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(FormBudgetChangeHistory_Load);
		this.PanelBudgetApproval.ResumeLayout(false);
		this.splitMenuAndGrid.Panel1.ResumeLayout(false);
		this.splitMenuAndGrid.Panel2.ResumeLayout(false);
		this.splitMenuAndGrid.ResumeLayout(false);
		this.PanelMenu.ResumeLayout(false);
		this.panelHeader.ResumeLayout(false);
		this.panelHeader.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ToolbarsMenu).EndInit();
		this.splitGrid.Panel1.ResumeLayout(false);
		this.splitGrid.Panel2.ResumeLayout(false);
		this.splitGrid.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridBudgetChangeProject).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridBudExeItemA).EndInit();
		this.PanelButton.ResumeLayout(false);
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
