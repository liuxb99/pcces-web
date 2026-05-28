using System.Collections;
using System.Collections.Generic;
using System.Data;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.DomainModule.General;

namespace Archnowledge.Pcces.PccesMain.Library;

internal class ItemNoSettingManager
{
	private DataSet dsItemNoSetting;

	private string printMode;

	private string projectCode;

	private BudProject theBudProject = new BudProject();

	private ItemNoSetting theItemNoSetting = new ItemNoSetting();

	private string AssemType = "1";

	private string Separate = "-";

	private string IsSymbol = "N";

	private string Type = "";

	private string level1 = "壹";

	private string level2 = "一";

	private string level3 = "1";

	private string level4 = "1";

	private string level5 = "1";

	private string level6 = "1";

	private string level7 = "1";

	private string level8 = "1";

	private ArrayList LevelList = new ArrayList();

	public ItemNoSettingManager(string projectCode)
	{
		this.projectCode = projectCode;
	}

	public void PrepareAssemItemNo()
	{
		GetItemNoSetting(out level1, out level2, out level3, out level4, out level5, out level6, out level7, out level8, out AssemType, out Type, out IsSymbol, out Separate);
		LevelList.Clear();
		UserDefinedDBHelper theUserDefinedDBHelper = new UserDefinedDBHelper();
		List<string> OneLevelList = new List<string>();
		LevelList.Add(OneLevelList);
		SetLevelLstContent(level1, OneLevelList, theUserDefinedDBHelper);
		OneLevelList = new List<string>();
		LevelList.Add(OneLevelList);
		SetLevelLstContent(level2, OneLevelList, theUserDefinedDBHelper);
		OneLevelList = new List<string>();
		LevelList.Add(OneLevelList);
		SetLevelLstContent(level3, OneLevelList, theUserDefinedDBHelper);
		OneLevelList = new List<string>();
		LevelList.Add(OneLevelList);
		SetLevelLstContent(level4, OneLevelList, theUserDefinedDBHelper);
		OneLevelList = new List<string>();
		LevelList.Add(OneLevelList);
		SetLevelLstContent(level5, OneLevelList, theUserDefinedDBHelper);
		OneLevelList = new List<string>();
		LevelList.Add(OneLevelList);
		SetLevelLstContent(level6, OneLevelList, theUserDefinedDBHelper);
		OneLevelList = new List<string>();
		LevelList.Add(OneLevelList);
		SetLevelLstContent(level7, OneLevelList, theUserDefinedDBHelper);
		OneLevelList = new List<string>();
		LevelList.Add(OneLevelList);
		SetLevelLstContent(level8, OneLevelList, theUserDefinedDBHelper);
	}

	private void SetLevelLstContent(string ListKind, List<string> ArrLst, UserDefinedDBHelper theUserDefinedDBHelper)
	{
		DataSet ds = theUserDefinedDBHelper.GetUserDefinedByKind(ListKind);
		if (ds.Tables[0].Rows.Count == 0)
		{
			theUserDefinedDBHelper.InsertInitialItemNo(ListKind, ds);
		}
		foreach (DataRow r in ds.Tables[0].Rows)
		{
			ArrLst.Add(r["cString"].ToString().Trim());
		}
		ds.Dispose();
		ds = null;
	}

	private string GetItemNo(int Level, int Index)
	{
		if (Level <= 8 && Level >= 1)
		{
			if (LevelList[Level - 1] is List<string> OneLevelList && OneLevelList.Count >= Index)
			{
				return OneLevelList[Index - 1];
			}
			return Index.ToString();
		}
		return "";
	}

	public string GetItemNoByPrintNo(string PrintNo, string Kind)
	{
		if (!(Type == "ALL"))
		{
			if (Type == "M")
			{
				if (Kind == "W")
				{
					return "";
				}
			}
			else if (Type == "W" && Kind != "W")
			{
				return "";
			}
		}
		string ItemNo = "";
		int Index;
		if (AssemType == "1")
		{
			string LastNum = PrintNo.Substring(PrintNo.Length - 4);
			int.TryParse(LastNum, out Index);
			int Level = PrintNo.Length / 4;
			ItemNo = GetItemNo(Level, Index);
		}
		else
		{
			int CurrentIndex = 0;
			int Level = 1;
			while (CurrentIndex < PrintNo.Length)
			{
				string No = PrintNo.Substring(CurrentIndex, 4);
				int.TryParse(No, out Index);
				if (ItemNo != "")
				{
					ItemNo += ((IsSymbol == "Y") ? Separate : "");
				}
				ItemNo += GetItemNo(Level, Index);
				CurrentIndex = 4 * Level;
				Level++;
			}
		}
		return ItemNo;
	}

	public void GetItemNoSetting(out string level1, out string level2, out string level3, out string level4, out string level5, out string level6, out string level7, out string level8, out string AssemType, out string Type, out string IsSymbol, out string Symbol)
	{
		printMode = theBudProject.GetPrintMode(projectCode);
		dsItemNoSetting = theItemNoSetting.GetItemNoSetting(projectCode);
		DataTable dtItemNoSetting = dsItemNoSetting.Tables["ItemNoSetting"];
		DataRow drItemNoSetting = null;
		if (dtItemNoSetting.Rows.Count == 0)
		{
			drItemNoSetting = dtItemNoSetting.NewRow();
			dtItemNoSetting.Rows.Add(drItemNoSetting);
			drItemNoSetting["ProjectCode"] = projectCode;
			drItemNoSetting["AssemType"] = CommonMethods.GetIniValue("AutoItemNo", "AssemType");
			if (printMode != string.Empty)
			{
				drItemNoSetting["AssemType"] = printMode.Substring(38, 1);
			}
			drItemNoSetting["Type"] = CommonMethods.GetIniValue("AutoItemNo", "Type");
			drItemNoSetting["IsSymbol"] = CommonMethods.GetIniValue("AutoItemNo", "IsSymbol");
			drItemNoSetting["Symbol"] = CommonMethods.GetIniValue("AutoItemNo", "Symbol");
			drItemNoSetting["Level1"] = CommonMethods.GetIniValue("AutoItemNo", "1");
			drItemNoSetting["Level2"] = CommonMethods.GetIniValue("AutoItemNo", "2");
			drItemNoSetting["Level3"] = CommonMethods.GetIniValue("AutoItemNo", "3");
			drItemNoSetting["Level4"] = CommonMethods.GetIniValue("AutoItemNo", "4");
			drItemNoSetting["Level5"] = CommonMethods.GetIniValue("AutoItemNo", "5");
			drItemNoSetting["Level6"] = CommonMethods.GetIniValue("AutoItemNo", "6");
			drItemNoSetting["Level7"] = CommonMethods.GetIniValue("AutoItemNo", "7");
			drItemNoSetting["Level8"] = CommonMethods.GetIniValue("AutoItemNo", "8");
			theItemNoSetting.UpdateItemNoSetting(dsItemNoSetting);
		}
		drItemNoSetting = dtItemNoSetting.Rows[0];
		level1 = drItemNoSetting["Level1"].ToString();
		level2 = drItemNoSetting["Level2"].ToString();
		level3 = drItemNoSetting["Level3"].ToString();
		level4 = drItemNoSetting["Level4"].ToString();
		level5 = drItemNoSetting["Level5"].ToString();
		level6 = drItemNoSetting["Level6"].ToString();
		level7 = drItemNoSetting["Level7"].ToString();
		level8 = drItemNoSetting["Level8"].ToString();
		AssemType = drItemNoSetting["AssemType"].ToString();
		Type = drItemNoSetting["Type"].ToString();
		IsSymbol = drItemNoSetting["IsSymbol"].ToString();
		Symbol = drItemNoSetting["Symbol"].ToString();
	}

	public void SaveItemNoSetting(string level1, string level2, string level3, string level4, string level5, string level6, string level7, string level8, string AssemType, string Type, string IsSymbol, string Symbol)
	{
		CommonMethods.WriteIniValue("AutoItemNo", "AssemType", AssemType);
		CommonMethods.WriteIniValue("AutoItemNo", "Symbol", Symbol);
		CommonMethods.WriteIniValue("AutoItemNo", "IsSymbol", IsSymbol);
		CommonMethods.WriteIniValue("AutoItemNo", "Type", Type);
		CommonMethods.WriteIniValue("AutoItemNo", "1", level1);
		CommonMethods.WriteIniValue("AutoItemNo", "2", level2);
		CommonMethods.WriteIniValue("AutoItemNo", "3", level3);
		CommonMethods.WriteIniValue("AutoItemNo", "4", level4);
		CommonMethods.WriteIniValue("AutoItemNo", "5", level5);
		CommonMethods.WriteIniValue("AutoItemNo", "6", level6);
		CommonMethods.WriteIniValue("AutoItemNo", "7", level7);
		CommonMethods.WriteIniValue("AutoItemNo", "8", level8);
		if (printMode != string.Empty)
		{
			printMode = printMode.Substring(0, 38) + AssemType + printMode.Substring(39, printMode.Length - 39);
			theBudProject.UpdatePrintMode(projectCode, printMode);
		}
		DataRow drItemNoSetting = dsItemNoSetting.Tables["ItemNoSetting"].Rows[0];
		drItemNoSetting["Level1"] = level1;
		drItemNoSetting["Level2"] = level2;
		drItemNoSetting["Level3"] = level3;
		drItemNoSetting["Level4"] = level4;
		drItemNoSetting["Level5"] = level5;
		drItemNoSetting["Level6"] = level6;
		drItemNoSetting["Level7"] = level7;
		drItemNoSetting["Level8"] = level8;
		drItemNoSetting["AssemType"] = AssemType;
		drItemNoSetting["IsSymbol"] = IsSymbol;
		drItemNoSetting["Symbol"] = Symbol;
		drItemNoSetting["Type"] = Type;
		theItemNoSetting.UpdateItemNoSetting(dsItemNoSetting);
	}
}
