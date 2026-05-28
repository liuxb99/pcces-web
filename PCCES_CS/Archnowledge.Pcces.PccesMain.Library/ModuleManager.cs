using System;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Microsoft.Win32;

namespace Archnowledge.Pcces.PccesMain.Library;

internal class ModuleManager
{
	private const int BudgetMdoule = 1;

	private const int BidMdoule = 2;

	private const int CommonMdoule = 4;

	private const int ContractModule = 8;

	private int FirstRun = 0;

	private int StartModule = 0;

	public bool IsFirstRun
	{
		get
		{
			int Value = FirstRun;
			if (Value == 1)
			{
				return true;
			}
			return false;
		}
		set
		{
			if (value)
			{
				SetIniValue("FirstRun", 1);
				FirstRun = 1;
			}
			else
			{
				SetIniValue("FirstRun", 0);
				FirstRun = 0;
			}
		}
	}

	public bool EnableBudgetMdoule
	{
		get
		{
			return GetModuleValue(1);
		}
		set
		{
			SetModuleValue(1, value);
		}
	}

	public bool EnableBidMdoule
	{
		get
		{
			return GetModuleValue(2);
		}
		set
		{
			SetModuleValue(2, value);
		}
	}

	public bool EnableCommonMdoule
	{
		get
		{
			return GetModuleValue(4);
		}
		set
		{
			SetModuleValue(4, value);
		}
	}

	public bool EnableContractModule
	{
		get
		{
			return GetModuleValue(8);
		}
		set
		{
			SetModuleValue(8, value);
		}
	}

	public ModuleManager()
	{
		FirstRun = GetIniValue("FirstRun", 1);
		StartModule = GetIniValue("StartModule", 7);
	}

	private int GetRegistryKey(string Key)
	{
		int retValue = 0;
		try
		{
			RegistryKey OurKey = Registry.LocalMachine;
			RegistryKey PccesKey = OurKey.OpenSubKey("SOFTWARE\\Archnowledge\\Pcces");
			if (PccesKey == null)
			{
				PccesKey = OurKey.CreateSubKey("SOFTWARE\\Archnowledge\\Pcces");
			}
			if (PccesKey != null)
			{
				try
				{
					object Value = PccesKey.GetValue(Key);
					if (Value != null)
					{
						retValue = (int)Value;
					}
				}
				catch (Exception ex)
				{
					DebugUtil.OutputDebugString("讀取 Registry key [" + Key + "] 失敗: " + ex.Message);
				}
			}
			else
			{
				DebugUtil.OutputDebugString("讀取 Registry [SOFTWARE\\Archnowledge\\Pcces] 建立失敗");
			}
		}
		catch (Exception ex)
		{
			DebugUtil.OutputDebugString("讀取 Registry key [" + Key + "] 失敗: " + ex.Message);
		}
		return retValue;
	}

	private void SetRegistryKey(string Key, int Value)
	{
		try
		{
			RegistryKey OurKey = Registry.LocalMachine;
			RegistryKey PccesKey = OurKey.OpenSubKey("SOFTWARE\\Archnowledge\\Pcces");
			if (PccesKey == null)
			{
				PccesKey = OurKey.CreateSubKey("SOFTWARE\\Archnowledge\\Pcces");
			}
			if (PccesKey != null)
			{
				try
				{
					PccesKey.SetValue(Key, Value, RegistryValueKind.DWord);
					return;
				}
				catch (Exception ex)
				{
					DebugUtil.OutputDebugString("設定 Registry key [" + Key + "] 失敗: " + ex.Message);
					return;
				}
			}
			DebugUtil.OutputDebugString("設定 Registry [SOFTWARE\\Archnowledge\\Pcces] 建立失敗");
		}
		catch (Exception ex)
		{
			DebugUtil.OutputDebugString("設定 Registry key [" + Key + "] 失敗: " + ex.Message);
		}
	}

	private int GetIniValue(string Key, int DefaultValue)
	{
		int retValue = 0;
		string sIniFileName = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "PccesMain.ini";
		try
		{
			string Value = CommonMethods.IniReadValue(sIniFileName, "StartModule", Key);
			return int.Parse(Value);
		}
		catch (Exception)
		{
			CommonMethods.IniWriteValue(sIniFileName, "StartModule", Key, DefaultValue.ToString());
			return DefaultValue;
		}
	}

	private void SetIniValue(string Key, int Value)
	{
		string sIniFileName = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "PccesMain.ini";
		CommonMethods.IniWriteValue(sIniFileName, "StartModule", Key, Value.ToString());
	}

	private bool GetModuleValue(int ModuleID)
	{
		int Value = StartModule;
		if ((Value & ModuleID) == ModuleID)
		{
			return true;
		}
		return false;
	}

	private void SetModuleValue(int ModuleID, bool Enable)
	{
		int Value = StartModule;
		if ((Value & ModuleID) == ModuleID && !Enable)
		{
			Value -= ModuleID;
		}
		else if ((Value & ModuleID) != ModuleID && Enable)
		{
			Value += ModuleID;
		}
		SetIniValue("StartModule", Value);
		StartModule = Value;
	}
}
