using System;
using Archnowledge.Pcces.DomainModule.General;
using C1.Win.C1FlexGrid;

namespace Archnowledge.Pcces.PccesMain.Library;

internal class GridPropertySetting
{
	public static void SaveGridProperty(string UserID, string FormName, C1FlexGrid Grid)
	{
		string SettingValue = "";
		for (int i = 0; i < Grid.Cols.Count; i++)
		{
			Column theColumn = Grid.Cols[i];
			object obj = SettingValue;
			SettingValue = string.Concat(obj, theColumn.Name, "==", theColumn.Width, "&&");
		}
		UserSettings oUserSettings = new UserSettings();
		oUserSettings.SetSysUserDatabaseName(UserID, "_" + FormName, SettingValue);
	}

	public static void LoadGridProperty(string UserID, string FormName, C1FlexGrid Grid)
	{
		UserSettings oUserSettings = new UserSettings();
		string SettingValue = oUserSettings.GetSysUserDatabaseName(UserID, "_" + FormName);
		string[] LimitorA = new string[1] { "&&" };
		string[] Items = SettingValue.Split(LimitorA, StringSplitOptions.RemoveEmptyEntries);
		if (Items.Length != Grid.Cols.Count)
		{
			return;
		}
		int i = 0;
		while (Items != null && i < Items.Length)
		{
			string[] LimitorB = new string[1] { "==" };
			string[] Setting = Items[i].Split(LimitorB, StringSplitOptions.RemoveEmptyEntries);
			if (Setting.Length == 2)
			{
				Column theColumn = Grid.Cols[Setting[0]];
				if (theColumn != null)
				{
					if (theColumn.Index != i)
					{
						theColumn.Move(i);
					}
					int Width = 0;
					int.TryParse(Setting[1], out Width);
					if (theColumn.Width != Width)
					{
						theColumn.Width = Width;
					}
				}
			}
			i++;
		}
	}
}
