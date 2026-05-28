using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.CommonClass.MrsBase;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.DomainModule.MrsBase;
using Archnowledge.Pcces.STDClass;
using Aspose.Cells;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinProgressBar;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.MrsBase;

public class FormMrsBase_ImpWizard : Form
{
	private bool IsChangeCNameAndUnitName = false;

	private PccesFormAction F_ActionName;

	private string F_ProjectCode = "";

	private ImportType F_ImportType;

	private string F_UserID;

	private DataSet dsPwrSet = null;

	private string F_SourceFile = "";

	private bool F_IsAutoExecute = false;

	private IContainer components;

	private UltraTabControl TabCtrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_A;

	private Panel panel1;

	private GroupBox groupBox1;

	private UltraButton A_Btn_Cncl;

	private UltraButton A_Btn_Next;

	private UltraButton A_Btn_Prev;

	private RadioButton RB2;

	private UltraLabel ultraLabel3;

	private RadioButton RB1;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel1;

	private UltraTabPageControl Tab_B;

	private Panel panel3;

	private UltraLabel ultraLabel5;

	private UltraButton ultraButton4;

	private Panel panel2;

	private GroupBox groupBox2;

	private UltraButton B_Btn_Cncl;

	private UltraButton B_Btn_Next;

	private UltraButton B_Btn_Prev;

	private Panel panel5;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel6;

	private UltraTabPageControl Tab_C;

	private UltraLabel lblWait;

	private UltraProgressBar Prog1;

	private Panel panel7;

	private GroupBox groupBox4;

	private UltraButton C_Btn_Cncl;

	private UltraButton C_Btn_Next;

	private UltraButton C_Btn_Prev;

	private UltraLabel lblProg1;

	private Panel panel4;

	private UltraLabel ultraLabel9;

	private UltraTabPageControl Tab_D;

	private UltraLabel ultraLabel14;

	private UltraLabel ultraLabel13;

	private UltraLabel ultraLabel12;

	private Panel panel6;

	private GroupBox groupBox3;

	private UltraButton D_Btn_Fnsh;

	private UltraButton D_Btn_Prev;

	private Timer timer1;

	private OpenFileDialog openFileDialog1;

	private UltraTextEditor txtImpDirFile;

	private UltraLabel ultraLabel4;

	private Label lblWanring;

	public bool _IsChangeCNameAndUnitName
	{
		get
		{
			return IsChangeCNameAndUnitName;
		}
		set
		{
			IsChangeCNameAndUnitName = true;
		}
	}

	public PccesFormAction _ActionName
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

	public ImportType _ImportType
	{
		get
		{
			return F_ImportType;
		}
		set
		{
			F_ImportType = value;
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

	public DataSet _dsPwrSet
	{
		get
		{
			return dsPwrSet;
		}
		set
		{
			dsPwrSet = value;
		}
	}

	public string _SourceFile
	{
		get
		{
			return F_SourceFile;
		}
		set
		{
			F_SourceFile = value;
		}
	}

	public bool _IsAutoExecute
	{
		get
		{
			return F_IsAutoExecute;
		}
		set
		{
			F_IsAutoExecute = value;
		}
	}

	public FormMrsBase_ImpWizard()
	{
		InitializeComponent();
	}

	private void ultraButton4_Click(object sender, EventArgs e)
	{
		string sFilter = "";
		switch (F_ImportType)
		{
		case ImportType.DBF:
			sFilter = "dBaseIII files (*.dbf)|*.dbf";
			break;
		case ImportType.Excel:
			sFilter = "Microsoft Excel 97/2000 files (*.xls)|*.xls";
			break;
		case ImportType.XML:
			sFilter = "XML files (*.xml)|*.xml";
			break;
		}
		openFileDialog1.Filter = sFilter;
		openFileDialog1.RestoreDirectory = true;
		if (openFileDialog1.ShowDialog() == DialogResult.OK)
		{
			txtImpDirFile.Text = openFileDialog1.FileName;
		}
	}

	private void A_Btn_Next_Click(object sender, EventArgs e)
	{
		Tab_B.Tab.Selected = true;
	}

	private void B_Btn_Next_Click(object sender, EventArgs e)
	{
		if (txtImpDirFile.Text.Trim() == "")
		{
			MessageBox.Show(this, "請先選定來源檔的目錄及檔名!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtImpDirFile.Focus();
			return;
		}
		if (!File.Exists(txtImpDirFile.Text.Trim()))
		{
			MessageBox.Show(this, "挑選的檔案不存在!", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtImpDirFile.Focus();
			return;
		}
		Tab_C.Tab.Selected = true;
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		bool importSuceeded = ExecuteImport();
		Cursor = Cursors.Default;
		if (importSuceeded)
		{
			Tab_D.Tab.Selected = true;
		}
		else
		{
			Tab_B.Tab.Selected = true;
		}
	}

	private void B_Btn_Prev_Click(object sender, EventArgs e)
	{
		Tab_A.Tab.Selected = true;
	}

	private void D_Btn_Prev_Click(object sender, EventArgs e)
	{
		Tab_B.Tab.Selected = true;
	}

	private bool ExecuteImport()
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("WinFORM 基本工料 匯入");
		if (F_ImportType == ImportType.Excel)
		{
			if (RB2.Checked)
			{
				Output_Com OUT_COM = new Output_Com(aArr);
				OUT_COM.dsPwrSet = dsPwrSet;
				if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
				{
					OUT_COM.SpecialMode = "ON";
					Text += " SpecialMode:ON";
					Application.DoEvents();
				}
				else
				{
					OUT_COM.SpecialMode = "OFF";
				}
				ArrayList TempArray = GetMrsBaseDS_FromExcelFile(txtImpDirFile.Text.Trim());
				if (TempArray == null)
				{
					return false;
				}
				DataSet DS_MrsBaseA = (DataSet)TempArray[0];
				DataSet DS_MrsBaseB = (DataSet)TempArray[1];
				if (F_ProjectCode.Trim() != "")
				{
					OUT_COM.InExcel(DS_MrsBaseA, DS_MrsBaseB, CommonMethods.GetActionNameString(F_ActionName), F_ProjectCode);
				}
				else
				{
					OUT_COM.InExcel(DS_MrsBaseA, DS_MrsBaseB);
				}
			}
			else if (RB1.Checked)
			{
				ArrayList arrayList;
				(arrayList = aArr)[1] = string.Concat(arrayList[1], "-EXCEL(DIY）格式轉入");
				Archnowledge.Pcces.BUDClass.MrsBaseA MrsACom = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
				MrsACom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
				DataTable MrsDT = MrsACom.ListItem("");
				DataView MrsDV = MrsDT.DefaultView;
				MrsDV.Sort = "PccesCode";
				Excel excel = new Excel();
				SetAsposeLicense();
				DataTable InputDt = new DataTable();
				try
				{
					excel.Open(txtImpDirFile.Text.Trim());
					Worksheet worksheet = excel.Worksheets[0];
					InputDt = worksheet.Cells.ExportDataTable(0, 0, worksheet.Cells.MaxDataRow + 1, worksheet.Cells.MaxDataColumn + 1, exportColumnName: true);
				}
				catch (Exception ex)
				{
					CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBase_ImpWizard.cs" + ex.Message);
					MessageBox.Show(this, "轉入來源的檔案格式不正確或檔案正在使用中！\n" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return false;
				}
				InputDt.Columns.Add("PccesCode", Type.GetType("System.String"));
				InputDt.Columns.Add("PubCode", Type.GetType("System.Int64"));
				InputDt.Columns.Add("AddThis", Type.GetType("System.String"));
				string[] mustExistsColumns = new string[6] { "工項名稱", "單位", "單價", "種類", "百分比", "備註" };
				string[] array = mustExistsColumns;
				foreach (string columnName in array)
				{
					if (!InputDt.Columns.Contains(columnName))
					{
						MessageBox.Show($"轉入來源的檔案格式不正確，無【{columnName}】欄位！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return false;
					}
				}
				int iFlag = 0;
				string ls_PccesCode = "Z" + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
				MrsDV.RowFilter = "substring(pccescode,1,5) = '" + ls_PccesCode + "'";
				if (MrsDV.Count > 0)
				{
					iFlag = PubTools.Str2Int(MrsDV[MrsDV.Count - 1]["pccescode"].ToString().Substring(5));
				}
				int i2 = 0;
				foreach (DataRow dr in InputDt.Rows)
				{
					i2++;
					string ls_cName = dr["工項名稱"].ToString().Trim();
					string ls_cUnit = dr["單位"].ToString().Trim();
					MrsDV.RowFilter = string.Format("cName='{0}' AND UnitName='{1}'", ls_cName.Replace("'", "''"), ls_cUnit.Replace("'", "''"));
					string ls_costkind;
					string ls_memo;
					if (MrsDV.Count > 0)
					{
						dr["PccesCode"] = MrsDV[0]["PccesCode"];
						dr["PubCode"] = MrsDV[0]["PubCode"];
						dr["AddThis"] = "N";
						MrsACom.ps_pccesCode = MrsDV[0]["PccesCode"].ToString();
						MrsACom.ps_cName = null;
						MrsACom.ps_unitName = null;
						try
						{
							MrsACom.ps_eName = dr["英文名稱"].ToString();
						}
						catch
						{
							MrsACom.ps_eName = null;
						}
						try
						{
							MrsACom.ps_eUnit = dr["英文單位"].ToString();
						}
						catch
						{
							MrsACom.ps_eUnit = null;
						}
						MrsACom.ps_cost = dr["單價"].ToString().Replace(",", "");
						ls_costkind = dr["種類"].ToString().ToUpper().Trim();
						switch (ls_costkind)
						{
						default:
							if (!(ls_costkind == "M"))
							{
								if (ls_costkind == "Y")
								{
									MrsACom.ps_analysis = "1";
									MrsACom.ps_costKind = "";
									MrsACom.ps_rate = "0";
								}
								else
								{
									MrsACom.ps_analysis = "0";
									MrsACom.ps_costKind = "";
									MrsACom.ps_rate = "0";
								}
								break;
							}
							goto case "%";
						case "%":
						case "$":
						case "L":
						case "E":
							MrsACom.ps_costKind = ls_costkind;
							MrsACom.ps_rate = dr["百分比"].ToString().Replace(",", "");
							MrsACom.ps_analysis = "0";
							break;
						}
						ls_memo = dr["備註"].ToString();
						MrsACom.ps_memo = ls_memo;
						MrsACom.UpdItem();
						MrsACom.SetPost(MrsDV[0]["PccesCode"].ToString(), "1");
						continue;
					}
					string ls_nCode = (MrsACom.ps_pccesCode = ls_PccesCode + (iFlag + 1).ToString().PadLeft(5, '0'));
					MrsACom.ps_cName = ls_cName;
					MrsACom.ps_unitName = ls_cUnit;
					MrsACom.ps_cost = dr["單價"].ToString().Replace(",", "");
					try
					{
						MrsACom.ps_eName = dr["英文名稱"].ToString();
					}
					catch
					{
						MrsACom.ps_eName = null;
					}
					try
					{
						MrsACom.ps_eUnit = dr["英文單位"].ToString();
					}
					catch
					{
						MrsACom.ps_eUnit = null;
					}
					ls_costkind = dr["種類"].ToString().ToUpper();
					switch (ls_costkind)
					{
					default:
						if (!(ls_costkind == "M"))
						{
							if (ls_costkind == "Y")
							{
								MrsACom.ps_analysis = "1";
								MrsACom.ps_costKind = "";
								MrsACom.ps_rate = "0";
								MrsACom.ps_analysisQty = dr["數量"].ToString();
							}
							else
							{
								MrsACom.ps_analysis = "0";
								MrsACom.ps_costKind = "";
								MrsACom.ps_rate = "0";
							}
							break;
						}
						goto case "%";
					case "%":
					case "$":
					case "L":
					case "E":
						MrsACom.ps_costKind = ls_costkind;
						MrsACom.ps_rate = dr["百分比"].ToString().Replace(",", "");
						MrsACom.ps_analysis = "0";
						break;
					}
					ls_memo = dr["備註"].ToString();
					if (ls_memo.Length > 0)
					{
						if (ls_memo.Substring(0, 1) != "#")
						{
							ls_memo = "#," + ls_memo;
						}
					}
					else
					{
						ls_memo = "#" + ls_memo;
					}
					MrsACom.ps_memo = ls_memo;
					MrsACom.InseItem();
					MrsACom.SetPost(ls_nCode, "0");
					int li_npubcode = MrsACom.Get_Pubcode(ls_nCode);
					dr["PccesCode"] = ls_nCode;
					dr["PubCode"] = li_npubcode;
					dr["AddThis"] = "Y";
					iFlag++;
					DataRow ndr = MrsDT.NewRow();
					ndr["PccesCode"] = ls_nCode;
					ndr["cName"] = ls_cName;
					ndr["UnitName"] = ls_cUnit;
					ndr["PubCode"] = li_npubcode;
					MrsDT.Rows.Add(ndr);
				}
				MrsACom = null;
				Archnowledge.Pcces.BUDClass.MrsBaseB MrsBCom = new Archnowledge.Pcces.BUDClass.MrsBaseB(aArr);
				MrsBCom.ps_srckind = "MRS";
				iFlag = 0;
				foreach (DataRow dr in InputDt.Rows)
				{
					string ls_costkind = dr["種類"].ToString().ToUpper();
					if (ls_costkind == "Y")
					{
						iFlag = 0;
						MrsBCom.ps_parentCode = dr["PubCode"].ToString();
						MrsBCom.DeleItems();
						continue;
					}
					MrsBCom.ps_pubCode = dr["PubCode"].ToString();
					MrsBCom.ps_listNo = (iFlag + 1).ToString();
					MrsBCom.ps_cost = dr["單價"].ToString().Replace(",", "");
					MrsBCom.ps_qty = dr["數量"].ToString().Replace(",", "");
					MrsBCom.InseItem();
					iFlag++;
				}
				PubTools.WriteRoughlyLog(aArr);
			}
		}
		else if (F_ImportType == ImportType.XML)
		{
			DataSet dsImportXML = new DataSet();
			try
			{
				dsImportXML.ReadXml(txtImpDirFile.Text.Trim());
				if (dsImportXML.Tables.IndexOf("mrsBaseA") > -1 && dsImportXML.Tables["mrsBaseA"].Columns.IndexOf("IsSkipImportMrsBase") == -1)
				{
					dsImportXML.Tables["mrsBaseA"].Columns.Add("IsSkipImportMrsBase", Type.GetType("System.String"));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, "轉入來源的檔案格式不正確！\n" + ex.Message, "匯入", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			MrsBaseManager mrsBaseManager = new MrsBaseManager();
			ExecResult ER = mrsBaseManager.ImportMrsBaseDataSet(dsImportXML);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show(ER.Message);
			}
			string sIniFileName = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "PccesMain.ini";
			string sIsImport2CesPrice = CommonMethods.IniReadValue(sIniFileName, "CESPRICE", "IsImport2CesPrice");
			if (sIsImport2CesPrice.ToUpper() == "TRUE")
			{
				Archnowledge.Pcces.BUDClass.MrsBaseA mrsBaseA = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
				mrsBaseA.ps_srckind = "Mrs";
				mrsBaseA.ps_accountCode2 = "cesPriceMode";
				if (dsImportXML.Tables[0].TableName.ToUpper() == "TABLE")
				{
					mrsBaseA.InputXML(dsImportXML.Tables[1], NoUpd: false);
				}
				else
				{
					mrsBaseA.InputXML(dsImportXML.Tables[0], NoUpd: false);
				}
			}
		}
		return true;
	}

	private ArrayList GetMrsBaseDS_FromExcelFile(string sourceFile)
	{
		DataSet mrsXML = new DataSet();
		DataSet temp = new DataSet();
		mrsXML.Tables.Add("基本工項");
		Excel excel = new Excel();
		SetAsposeLicense();
		try
		{
			excel.Open(sourceFile);
			Worksheet worksheet = excel.Worksheets["基本工項"];
			temp.Tables.Add(worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.MaxDataRow + 1, worksheet.Cells.MaxDataColumn + 1, exportColumnName: true));
			temp.Tables[0].TableName = "基本工項";
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBase_ImpWizard.cs" + ex.Message);
			string sErr = "使用的匯入檔案格式有誤或檔案正在使用中\n請先確認你所使用的EXCEL第一個頁次名稱是基本工項\n第二個頁次名稱是分析工項";
			MessageBox.Show(this, sErr, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return null;
		}
		int iCount = 20;
		mrsXML.Tables["基本工項"].Columns.Add("NewCode");
		mrsXML.Tables["基本工項"].Columns.Add("OldCode");
		mrsXML.Tables["基本工項"].Columns.Add("cName");
		mrsXML.Tables["基本工項"].Columns.Add("eName");
		mrsXML.Tables["基本工項"].Columns.Add("cUnit");
		mrsXML.Tables["基本工項"].Columns.Add("eUnit");
		mrsXML.Tables["基本工項"].Columns.Add("Spec");
		mrsXML.Tables["基本工項"].Columns.Add("Analysis");
		mrsXML.Tables["基本工項"].Columns.Add("Cost");
		mrsXML.Tables["基本工項"].Columns.Add("eRate");
		mrsXML.Tables["基本工項"].Columns.Add("lRate");
		mrsXML.Tables["基本工項"].Columns.Add("mRate");
		mrsXML.Tables["基本工項"].Columns.Add("wRate");
		mrsXML.Tables["基本工項"].Columns.Add("Memo");
		mrsXML.Tables["基本工項"].Columns.Add("UpdDT");
		mrsXML.Tables["基本工項"].Columns.Add("extendCode");
		mrsXML.Tables["基本工項"].Columns.Add("kind");
		mrsXML.Tables["基本工項"].Columns.Add("Rate");
		mrsXML.Tables["基本工項"].Columns.Add("changFlag");
		if (temp.Tables["基本工項"].Columns.IndexOf("別名") >= 0)
		{
			mrsXML.Tables["基本工項"].Columns.Add("surName");
		}
		else
		{
			iCount = 19;
		}
		if (SysConfig.SysEnablePwrSet)
		{
			mrsXML.Tables["基本工項"].Columns.Add("PwrSet");
			mrsXML.Tables["基本工項"].Columns.Add("Account");
			iCount = ((iCount != 19) ? 22 : 21);
		}
		if (temp.Tables["基本工項"].Columns.IndexOf("俗名") >= 0)
		{
			mrsXML.Tables["基本工項"].Columns.Add("commonName");
			iCount++;
		}
		DataRow myRow = mrsXML.Tables["基本工項"].NewRow();
		myRow[0] = "基本工項新碼";
		myRow[1] = "基本工項代碼";
		myRow[2] = "工項中文名稱";
		myRow[3] = "工項英文名稱";
		myRow[4] = "單位";
		myRow[5] = "英文單位";
		myRow[6] = "規格";
		myRow[7] = "單價分析";
		myRow[8] = "單價";
		myRow[9] = "機具百分率";
		myRow[10] = "人工百分率";
		myRow[11] = "材料百分率";
		myRow[12] = "雜項百分率";
		myRow[13] = "備註欄";
		myRow[14] = "登錄時間";
		myRow[15] = "工項外碼";
		myRow[16] = "項目種類";
		myRow[17] = "百分比";
		myRow[18] = "單價變動旗標";
		if (temp.Tables["基本工項"].Columns.IndexOf("別名") >= 0)
		{
			myRow[19] = "別名";
		}
		if (SysConfig.SysEnablePwrSet)
		{
			if (temp.Tables["基本工項"].Columns.IndexOf("別名") >= 0)
			{
				myRow[20] = "發包權限";
				myRow[21] = "會計科目碼";
			}
			else
			{
				myRow[19] = "發包權限";
				myRow[20] = "會計科目碼";
			}
		}
		if (mrsXML.Tables["基本工項"].Columns.IndexOf("commonName") > -1)
		{
			myRow["commonName"] = "俗名";
		}
		mrsXML.Tables["基本工項"].Rows.Add(myRow);
		for (int i = 0; i < temp.Tables["基本工項"].Rows.Count; i++)
		{
			DataRow entryRow = mrsXML.Tables["基本工項"].NewRow();
			for (int j = 0; j < iCount; j++)
			{
				try
				{
					if (j == 9 || j == 10 || j == 11 || j == 12)
					{
						entryRow[j] = PubTools.Str2Decimal(temp.Tables["基本工項"].Rows[i][myRow[j].ToString()]) * 100m;
					}
					else
					{
						entryRow[j] = temp.Tables["基本工項"].Rows[i][myRow[j].ToString()];
					}
				}
				catch
				{
				}
			}
			mrsXML.Tables["基本工項"].Rows.Add(entryRow);
		}
		DataSet anaXML = new DataSet();
		anaXML.Tables.Add("分析工項");
		try
		{
			excel.Open(sourceFile);
			Worksheet worksheet = excel.Worksheets["分析工項"];
			temp.Tables.Add(worksheet.Cells.ExportDataTableAsString(0, 0, worksheet.Cells.MaxDataRow + 1, worksheet.Cells.MaxDataColumn + 1, exportColumnName: true));
			temp.Tables[1].TableName = "分析工項";
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBase_ImpWizard.cs" + ex.Message);
			string sErr = "使用的匯入檔案格式有誤\n請先確認你所使用的EXCEL第一個頁次名稱是基本工項\n第二個頁次名稱是分析工項";
			MessageBox.Show(this, sErr, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		anaXML.Tables["分析工項"].Columns.Add("ParentName");
		anaXML.Tables["分析工項"].Columns.Add("ChildName");
		anaXML.Tables["分析工項"].Columns.Add("ParentCode");
		anaXML.Tables["分析工項"].Columns.Add("ChildParentName");
		anaXML.Tables["分析工項"].Columns.Add("MinResQty");
		anaXML.Tables["分析工項"].Columns.Add("Qty");
		anaXML.Tables["分析工項"].Columns.Add("ManResQty");
		anaXML.Tables["分析工項"].Columns.Add("Cost");
		anaXML.Tables["分析工項"].Columns.Add("Amount");
		anaXML.Tables["分析工項"].Columns.Add("Memo");
		anaXML.Tables["分析工項"].Columns.Add("ListNo");
		anaXML.Tables["分析工項"].Columns.Add("TmpListNo");
		anaXML.Tables["分析工項"].Columns.Add("Rate");
		DataRow anaRow = anaXML.Tables["分析工項"].NewRow();
		anaRow[0] = "基本工項名稱";
		anaRow[1] = "分析工項名稱";
		anaRow[2] = "基本工項代碼";
		anaRow[3] = "分析工項代碼";
		anaRow[4] = "最低資源數量";
		anaRow[5] = "數量";
		anaRow[6] = "最高資源數量";
		anaRow[7] = "分析工項單價";
		anaRow[8] = "分析工項複價";
		anaRow[9] = "備註";
		anaRow[10] = "分析順序";
		anaRow[11] = "分析順序暫存欄";
		anaRow[12] = "百分比";
		anaXML.Tables["分析工項"].Rows.Add(anaRow);
		for (int k = 0; k < temp.Tables["分析工項"].Rows.Count; k++)
		{
			DataRow insertRow = anaXML.Tables["分析工項"].NewRow();
			for (int l = 0; l < temp.Tables["分析工項"].Columns.Count; l++)
			{
				if (l <= 12)
				{
					insertRow[l] = temp.Tables["分析工項"].Rows[k][l];
				}
			}
			anaXML.Tables["分析工項"].Rows.Add(insertRow);
		}
		ArrayList DS_Array = new ArrayList();
		DS_Array.Add(mrsXML);
		DS_Array.Add(anaXML);
		return DS_Array;
	}

	private static void SetAsposeLicense()
	{
		Aspose.Cells.License license = new Aspose.Cells.License();
		license.SetLicense("Aspose.Custom.lic");
	}

	private void FormMrsBase_ImpWizard_Load(object sender, EventArgs e)
	{
		if (F_ImportType == ImportType.XML)
		{
			B_Btn_Prev.Visible = false;
			Tab_B.Tab.Selected = true;
			if (F_IsAutoExecute && F_SourceFile != "")
			{
				txtImpDirFile.Text = F_SourceFile;
				B_Btn_Next_Click(sender, e);
			}
		}
		if (F_ActionName != PccesFormAction.MrsBase)
		{
			lblWanring.Visible = true;
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.MrsBase.FormMrsBase_ImpWizard));
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
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.lblWanring = new System.Windows.Forms.Label();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.RB2 = new System.Windows.Forms.RadioButton();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.RB1 = new System.Windows.Forms.RadioButton();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel3 = new System.Windows.Forms.Panel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton4 = new Infragistics.Win.Misc.UltraButton();
		this.txtImpDirFile = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.panel2 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.B_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.B_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.B_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_C = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.lblWait = new Infragistics.Win.Misc.UltraLabel();
		this.Prog1 = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
		this.panel7 = new System.Windows.Forms.Panel();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.C_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.C_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.C_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.lblProg1 = new Infragistics.Win.Misc.UltraLabel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_D = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.panel6 = new System.Windows.Forms.Panel();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.D_Btn_Fnsh = new Infragistics.Win.Misc.UltraButton();
		this.D_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.TabCtrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.Tab_A.SuspendLayout();
		this.panel1.SuspendLayout();
		this.Tab_B.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtImpDirFile).BeginInit();
		this.panel2.SuspendLayout();
		this.panel5.SuspendLayout();
		this.Tab_C.SuspendLayout();
		this.panel7.SuspendLayout();
		this.panel4.SuspendLayout();
		this.Tab_D.SuspendLayout();
		this.panel6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TabCtrl).BeginInit();
		this.TabCtrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.lblWanring);
		this.Tab_A.Controls.Add(this.ultraLabel4);
		this.Tab_A.Controls.Add(this.panel1);
		this.Tab_A.Controls.Add(this.RB2);
		this.Tab_A.Controls.Add(this.ultraLabel3);
		this.Tab_A.Controls.Add(this.RB1);
		this.Tab_A.Controls.Add(this.ultraLabel2);
		this.Tab_A.Controls.Add(this.ultraLabel1);
		this.Tab_A.Location = new System.Drawing.Point(0, 0);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(516, 369);
		this.lblWanring.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lblWanring.ForeColor = System.Drawing.Color.Red;
		this.lblWanring.Location = new System.Drawing.Point(44, 228);
		this.lblWanring.Name = "lblWanring";
		this.lblWanring.Size = new System.Drawing.Size(420, 23);
		this.lblWanring.TabIndex = 11;
		this.lblWanring.Text = "* 此匯入功能只作工作要項的換碼，不作資料轉入。";
		this.lblWanring.Visible = false;
		appearance1.BackColor = System.Drawing.Color.White;
		this.ultraLabel4.Appearance = appearance1;
		this.ultraLabel4.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel4.Location = new System.Drawing.Point(63, 182);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(365, 32);
		this.ultraLabel4.TabIndex = 10;
		this.ultraLabel4.Text = "係指由PCCES基本資料庫之欄位結構之Excel格式轉入資料";
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.groupBox1);
		this.panel1.Controls.Add(this.A_Btn_Cncl);
		this.panel1.Controls.Add(this.A_Btn_Next);
		this.panel1.Controls.Add(this.A_Btn_Prev);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 325);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(516, 44);
		this.panel1.TabIndex = 9;
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(516, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Cncl.Appearance = appearance2;
		this.A_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Cncl.Location = new System.Drawing.Point(416, 9);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.ShowFocusRect = false;
		this.A_Btn_Cncl.ShowOutline = false;
		this.A_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Cncl.SupportThemes = false;
		this.A_Btn_Cncl.TabIndex = 2;
		this.A_Btn_Cncl.Text = "取消";
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Next.Appearance = appearance3;
		this.A_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Next.Location = new System.Drawing.Point(324, 9);
		this.A_Btn_Next.Name = "A_Btn_Next";
		this.A_Btn_Next.ShowFocusRect = false;
		this.A_Btn_Next.ShowOutline = false;
		this.A_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Next.SupportThemes = false;
		this.A_Btn_Next.TabIndex = 1;
		this.A_Btn_Next.Text = "下一步";
		this.A_Btn_Next.Click += new System.EventHandler(A_Btn_Next_Click);
		appearance4.Image = resources.GetObject("appearance4.Image");
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Prev.Appearance = appearance4;
		this.A_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Prev.Location = new System.Drawing.Point(232, 9);
		this.A_Btn_Prev.Name = "A_Btn_Prev";
		this.A_Btn_Prev.ShowFocusRect = false;
		this.A_Btn_Prev.ShowOutline = false;
		this.A_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Prev.SupportThemes = false;
		this.A_Btn_Prev.TabIndex = 0;
		this.A_Btn_Prev.Text = "上一步";
		this.A_Btn_Prev.Visible = false;
		this.RB2.BackColor = System.Drawing.Color.White;
		this.RB2.Checked = true;
		this.RB2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.RB2.Location = new System.Drawing.Point(48, 154);
		this.RB2.Name = "RB2";
		this.RB2.Size = new System.Drawing.Size(276, 24);
		this.RB2.TabIndex = 7;
		this.RB2.TabStop = true;
		this.RB2.Text = "基本資料庫格式檔";
		appearance5.BackColor = System.Drawing.Color.White;
		this.ultraLabel3.Appearance = appearance5;
		this.ultraLabel3.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel3.Location = new System.Drawing.Point(63, 100);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(365, 32);
		this.ultraLabel3.TabIndex = 6;
		this.ultraLabel3.Text = "係指由單價分析(DIY)製作之 Excel格式；系統自動由單價分析表產生基本工項資料";
		this.RB1.BackColor = System.Drawing.Color.White;
		this.RB1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.RB1.Location = new System.Drawing.Point(48, 74);
		this.RB1.Name = "RB1";
		this.RB1.Size = new System.Drawing.Size(272, 24);
		this.RB1.TabIndex = 3;
		this.RB1.Text = "單價分析格式檔(DIY格式)";
		appearance6.BackColor = System.Drawing.Color.White;
		this.ultraLabel2.Appearance = appearance6;
		this.ultraLabel2.Location = new System.Drawing.Point(43, 52);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel2.TabIndex = 2;
		this.ultraLabel2.Text = "你要使用哪種格式匯入資料?";
		appearance7.BackColor = System.Drawing.Color.White;
		this.ultraLabel1.Appearance = appearance7;
		this.ultraLabel1.Location = new System.Drawing.Point(8, 16);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(500, 20);
		this.ultraLabel1.TabIndex = 1;
		this.ultraLabel1.Text = "歡迎使用基本資料匯入精靈，接下來我們將引導您一步一步匯入資料";
		this.Tab_B.Controls.Add(this.panel3);
		this.Tab_B.Controls.Add(this.panel2);
		this.Tab_B.Controls.Add(this.panel5);
		this.Tab_B.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(516, 369);
		this.panel3.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel3.Controls.Add(this.ultraLabel5);
		this.panel3.Controls.Add(this.ultraButton4);
		this.panel3.Controls.Add(this.txtImpDirFile);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 60);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(516, 265);
		this.panel3.TabIndex = 14;
		this.ultraLabel5.Location = new System.Drawing.Point(11, 48);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel5.TabIndex = 4;
		this.ultraLabel5.Text = "來源檔的目錄及檔名:";
		appearance8.FontData.Name = "Arial";
		appearance8.FontData.SizeInPoints = 8f;
		this.ultraButton4.Appearance = appearance8;
		this.ultraButton4.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton4.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton4.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton4.Location = new System.Drawing.Point(459, 71);
		this.ultraButton4.Name = "ultraButton4";
		this.ultraButton4.ShowFocusRect = false;
		this.ultraButton4.ShowOutline = false;
		this.ultraButton4.Size = new System.Drawing.Size(48, 24);
		this.ultraButton4.SupportThemes = false;
		this.ultraButton4.TabIndex = 1;
		this.ultraButton4.Text = "瀏覽...";
		this.ultraButton4.Click += new System.EventHandler(ultraButton4_Click);
		appearance9.FontData.Name = "細明體";
		appearance9.FontData.SizeInPoints = 11f;
		this.txtImpDirFile.Appearance = appearance9;
		this.txtImpDirFile.Location = new System.Drawing.Point(10, 72);
		this.txtImpDirFile.Name = "txtImpDirFile";
		this.txtImpDirFile.Size = new System.Drawing.Size(450, 24);
		this.txtImpDirFile.TabIndex = 0;
		this.panel2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel2.Controls.Add(this.groupBox2);
		this.panel2.Controls.Add(this.B_Btn_Cncl);
		this.panel2.Controls.Add(this.B_Btn_Next);
		this.panel2.Controls.Add(this.B_Btn_Prev);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 325);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(516, 44);
		this.panel2.TabIndex = 13;
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(516, 8);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		appearance10.Image = resources.GetObject("appearance10.Image");
		appearance10.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Cncl.Appearance = appearance10;
		this.B_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.B_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Cncl.Location = new System.Drawing.Point(416, 9);
		this.B_Btn_Cncl.Name = "B_Btn_Cncl";
		this.B_Btn_Cncl.ShowFocusRect = false;
		this.B_Btn_Cncl.ShowOutline = false;
		this.B_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Cncl.SupportThemes = false;
		this.B_Btn_Cncl.TabIndex = 2;
		this.B_Btn_Cncl.Text = "取消";
		appearance11.Image = resources.GetObject("appearance11.Image");
		appearance11.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance11.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Next.Appearance = appearance11;
		this.B_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Next.Location = new System.Drawing.Point(324, 9);
		this.B_Btn_Next.Name = "B_Btn_Next";
		this.B_Btn_Next.ShowFocusRect = false;
		this.B_Btn_Next.ShowOutline = false;
		this.B_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Next.SupportThemes = false;
		this.B_Btn_Next.TabIndex = 1;
		this.B_Btn_Next.Text = "下一步";
		this.B_Btn_Next.Click += new System.EventHandler(B_Btn_Next_Click);
		appearance12.Image = resources.GetObject("appearance12.Image");
		appearance12.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Prev.Appearance = appearance12;
		this.B_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Prev.Location = new System.Drawing.Point(232, 9);
		this.B_Btn_Prev.Name = "B_Btn_Prev";
		this.B_Btn_Prev.ShowFocusRect = false;
		this.B_Btn_Prev.ShowOutline = false;
		this.B_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Prev.SupportThemes = false;
		this.B_Btn_Prev.TabIndex = 0;
		this.B_Btn_Prev.Text = "上一步";
		this.B_Btn_Prev.Click += new System.EventHandler(B_Btn_Prev_Click);
		this.panel5.BackColor = System.Drawing.Color.White;
		this.panel5.Controls.Add(this.ultraLabel7);
		this.panel5.Controls.Add(this.ultraLabel6);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(516, 60);
		this.panel5.TabIndex = 12;
		appearance13.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance13;
		this.ultraLabel7.Location = new System.Drawing.Point(47, 34);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel7.TabIndex = 3;
		this.ultraLabel7.Text = "請挑選匯入的資料來源檔所存放位置";
		appearance14.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance14;
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(16, 12);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 2;
		this.ultraLabel6.Text = "資料匯入來源檔案挑選";
		this.Tab_C.Controls.Add(this.lblWait);
		this.Tab_C.Controls.Add(this.Prog1);
		this.Tab_C.Controls.Add(this.panel7);
		this.Tab_C.Controls.Add(this.lblProg1);
		this.Tab_C.Controls.Add(this.panel4);
		this.Tab_C.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_C.Name = "Tab_C";
		this.Tab_C.Size = new System.Drawing.Size(516, 369);
		this.lblWait.Location = new System.Drawing.Point(16, 81);
		this.lblWait.Name = "lblWait";
		this.lblWait.Size = new System.Drawing.Size(476, 20);
		this.lblWait.TabIndex = 17;
		this.lblWait.Text = "正在準備匯入的資料，這個動作會花些時間，請稍候。";
		appearance15.BackColor = System.Drawing.Color.White;
		appearance15.BackColor2 = System.Drawing.Color.White;
		appearance15.FontData.Name = "細明體";
		appearance15.FontData.SizeInPoints = 11f;
		this.Prog1.Appearance = appearance15;
		appearance16.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
		appearance16.BackColor2 = System.Drawing.Color.Navy;
		appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		this.Prog1.FillAppearance = appearance16;
		this.Prog1.Location = new System.Drawing.Point(20, 128);
		this.Prog1.Name = "Prog1";
		this.Prog1.Size = new System.Drawing.Size(476, 23);
		this.Prog1.SupportThemes = false;
		this.Prog1.TabIndex = 16;
		this.Prog1.Text = "[Formatted]";
		this.Prog1.Visible = false;
		this.panel7.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel7.Controls.Add(this.groupBox4);
		this.panel7.Controls.Add(this.C_Btn_Cncl);
		this.panel7.Controls.Add(this.C_Btn_Next);
		this.panel7.Controls.Add(this.C_Btn_Prev);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel7.Location = new System.Drawing.Point(0, 325);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(516, 44);
		this.panel7.TabIndex = 15;
		this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox4.Location = new System.Drawing.Point(0, 0);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(516, 8);
		this.groupBox4.TabIndex = 3;
		this.groupBox4.TabStop = false;
		appearance17.Image = resources.GetObject("appearance17.Image");
		appearance17.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.C_Btn_Cncl.Appearance = appearance17;
		this.C_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.C_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.C_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.C_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.C_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.C_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.C_Btn_Cncl.Location = new System.Drawing.Point(416, 9);
		this.C_Btn_Cncl.Name = "C_Btn_Cncl";
		this.C_Btn_Cncl.ShowFocusRect = false;
		this.C_Btn_Cncl.ShowOutline = false;
		this.C_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.C_Btn_Cncl.SupportThemes = false;
		this.C_Btn_Cncl.TabIndex = 2;
		this.C_Btn_Cncl.Text = "取消";
		this.C_Btn_Cncl.Visible = false;
		appearance18.Image = resources.GetObject("appearance18.Image");
		appearance18.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance18.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.C_Btn_Next.Appearance = appearance18;
		this.C_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.C_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.C_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.C_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.C_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.C_Btn_Next.Location = new System.Drawing.Point(324, 9);
		this.C_Btn_Next.Name = "C_Btn_Next";
		this.C_Btn_Next.ShowFocusRect = false;
		this.C_Btn_Next.ShowOutline = false;
		this.C_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.C_Btn_Next.SupportThemes = false;
		this.C_Btn_Next.TabIndex = 1;
		this.C_Btn_Next.Text = "下一步";
		this.C_Btn_Next.Visible = false;
		appearance19.Image = resources.GetObject("appearance19.Image");
		appearance19.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.C_Btn_Prev.Appearance = appearance19;
		this.C_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.C_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.C_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.C_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.C_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.C_Btn_Prev.Location = new System.Drawing.Point(232, 9);
		this.C_Btn_Prev.Name = "C_Btn_Prev";
		this.C_Btn_Prev.ShowFocusRect = false;
		this.C_Btn_Prev.ShowOutline = false;
		this.C_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.C_Btn_Prev.SupportThemes = false;
		this.C_Btn_Prev.TabIndex = 0;
		this.C_Btn_Prev.Text = "上一步";
		this.C_Btn_Prev.Visible = false;
		this.lblProg1.Location = new System.Drawing.Point(16, 104);
		this.lblProg1.Name = "lblProg1";
		this.lblProg1.Size = new System.Drawing.Size(144, 20);
		this.lblProg1.TabIndex = 14;
		this.lblProg1.Text = "正在轉入基本資料";
		this.lblProg1.Visible = false;
		this.panel4.BackColor = System.Drawing.Color.White;
		this.panel4.Controls.Add(this.ultraLabel9);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel4.Location = new System.Drawing.Point(0, 0);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(516, 60);
		this.panel4.TabIndex = 13;
		appearance20.BackColor = System.Drawing.Color.White;
		this.ultraLabel9.Appearance = appearance20;
		this.ultraLabel9.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel9.Location = new System.Drawing.Point(16, 12);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel9.TabIndex = 2;
		this.ultraLabel9.Text = "資料匯入中...";
		this.Tab_D.Controls.Add(this.ultraLabel14);
		this.Tab_D.Controls.Add(this.ultraLabel13);
		this.Tab_D.Controls.Add(this.ultraLabel12);
		this.Tab_D.Controls.Add(this.panel6);
		this.Tab_D.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_D.Name = "Tab_D";
		this.Tab_D.Size = new System.Drawing.Size(516, 369);
		appearance21.BackColor = System.Drawing.Color.White;
		this.ultraLabel14.Appearance = appearance21;
		this.ultraLabel14.Location = new System.Drawing.Point(36, 116);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel14.TabIndex = 13;
		this.ultraLabel14.Text = "若要結束精靈，請按一下[完成]。";
		appearance22.BackColor = System.Drawing.Color.White;
		this.ultraLabel13.Appearance = appearance22;
		this.ultraLabel13.Location = new System.Drawing.Point(36, 64);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel13.TabIndex = 12;
		this.ultraLabel13.Text = "你已經成功匯入資料。";
		appearance23.BackColor = System.Drawing.Color.White;
		this.ultraLabel12.Appearance = appearance23;
		this.ultraLabel12.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel12.Location = new System.Drawing.Point(20, 20);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel12.TabIndex = 11;
		this.ultraLabel12.Text = "恭禧您!";
		this.panel6.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel6.Controls.Add(this.groupBox3);
		this.panel6.Controls.Add(this.D_Btn_Fnsh);
		this.panel6.Controls.Add(this.D_Btn_Prev);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel6.Location = new System.Drawing.Point(0, 325);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(516, 44);
		this.panel6.TabIndex = 10;
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox3.Location = new System.Drawing.Point(0, 0);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(516, 8);
		this.groupBox3.TabIndex = 3;
		this.groupBox3.TabStop = false;
		appearance24.Image = resources.GetObject("appearance24.Image");
		appearance24.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Fnsh.Appearance = appearance24;
		this.D_Btn_Fnsh.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Fnsh.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Fnsh.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.D_Btn_Fnsh.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Fnsh.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Fnsh.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Fnsh.Location = new System.Drawing.Point(324, 9);
		this.D_Btn_Fnsh.Name = "D_Btn_Fnsh";
		this.D_Btn_Fnsh.ShowFocusRect = false;
		this.D_Btn_Fnsh.ShowOutline = false;
		this.D_Btn_Fnsh.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Fnsh.SupportThemes = false;
		this.D_Btn_Fnsh.TabIndex = 1;
		this.D_Btn_Fnsh.Text = "完成";
		appearance25.Image = resources.GetObject("appearance25.Image");
		appearance25.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Prev.Appearance = appearance25;
		this.D_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Prev.Location = new System.Drawing.Point(232, 9);
		this.D_Btn_Prev.Name = "D_Btn_Prev";
		this.D_Btn_Prev.ShowFocusRect = false;
		this.D_Btn_Prev.ShowOutline = false;
		this.D_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Prev.SupportThemes = false;
		this.D_Btn_Prev.TabIndex = 0;
		this.D_Btn_Prev.Text = "上一步";
		this.D_Btn_Prev.Visible = false;
		this.D_Btn_Prev.Click += new System.EventHandler(D_Btn_Prev_Click);
		this.TabCtrl.BackColor = System.Drawing.Color.White;
		this.TabCtrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.TabCtrl.Controls.Add(this.Tab_A);
		this.TabCtrl.Controls.Add(this.Tab_B);
		this.TabCtrl.Controls.Add(this.Tab_C);
		this.TabCtrl.Controls.Add(this.Tab_D);
		this.TabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TabCtrl.Location = new System.Drawing.Point(0, 0);
		this.TabCtrl.Name = "TabCtrl";
		this.TabCtrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.TabCtrl.Size = new System.Drawing.Size(516, 369);
		this.TabCtrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.TabCtrl.TabIndex = 1;
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "tab1";
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "tab2";
		ultraTab3.TabPage = this.Tab_C;
		ultraTab3.Text = "tab3";
		ultraTab4.TabPage = this.Tab_D;
		ultraTab4.Text = "tab4";
		this.TabCtrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[4] { ultraTab1, ultraTab2, ultraTab3, ultraTab4 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(516, 369);
		this.timer1.Enabled = true;
		this.timer1.Interval = 1000;
		base.ShowIcon = false;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.CancelButton = this.A_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(516, 369);
		base.Controls.Add(this.TabCtrl);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormMrsBase_ImpWizard";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "匯入";
		base.Load += new System.EventHandler(FormMrsBase_ImpWizard_Load);
		this.Tab_A.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.Tab_B.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtImpDirFile).EndInit();
		this.panel2.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		this.Tab_C.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.Tab_D.ResumeLayout(false);
		this.panel6.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.TabCtrl).EndInit();
		this.TabCtrl.ResumeLayout(false);
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
