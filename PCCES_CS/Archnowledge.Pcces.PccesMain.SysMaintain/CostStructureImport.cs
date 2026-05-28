using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.DomainModule.MrsBase;
using Archnowledge.Pcces.STDClass;

namespace Archnowledge.Pcces.PccesMain.SysMaintain;

internal class CostStructureImport
{
	public static ExecResult Import(string UserID, bool OnlyStructure, string[] CostStructureTypes, FormSys_G_Info1 FM_INFO, ChgStrEventHandler eventHandler, ref int Progress)
	{
		Progress = 0;
		ChgStru.OutputHandlerMessage(eventHandler, "初始化成本架構", ref Progress);
		ExecResult ER = new ExecResult();
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(UserID);
		tmp_AL1.Add("(ChangDatabase) 建立成本架構");
		ModifyDB ModDB = new ModifyDB("", tmp_AL1);
		string sPath = AppDomain.CurrentDomain.BaseDirectory + "CostStructure\\CostStructureMrs";
		string[] CostStructureMrsFiles = Directory.GetFiles(sPath, "*.txt");
		sPath = AppDomain.CurrentDomain.BaseDirectory + "CostStructure\\CostStructureInfo";
		string[] CostStructureInfoFiles = Directory.GetFiles(sPath, "*.txt");
		FM_INFO._MaxValue = CostStructureMrsFiles.Length + CostStructureInfoFiles.Length;
		Archnowledge.Pcces.DomainModule.MrsBase.CostStructureImport oImport = new Archnowledge.Pcces.DomainModule.MrsBase.CostStructureImport();
		if (!OnlyStructure)
		{
			MrsBaseA mrsBaseA = new MrsBaseA();
			try
			{
				Application.DoEvents();
				foreach (string CurrFile in CostStructureMrsFiles)
				{
					if (CostStructureTypes == null || CostStructureTypes.Length == 0 || IsTypeSelected(CurrFile, CostStructureTypes))
					{
						ChgStru.OutputHandlerMessage(eventHandler, Path.GetFileNameWithoutExtension(CurrFile), ref Progress);
						ER = oImport.ImportCostStructureMrs(CurrFile);
						if (ER.ReturnCode != 0)
						{
							return ER;
						}
						ER = mrsBaseA.SyncMrsBaseAAnalysisWithMrsBaseB();
						if (ER.ReturnCode != 0)
						{
							return ER;
						}
					}
				}
				Application.DoEvents();
			}
			catch
			{
			}
		}
		try
		{
			foreach (string CurrFile in CostStructureInfoFiles)
			{
				ChgStru.OutputHandlerMessage(eventHandler, Path.GetFileNameWithoutExtension(CurrFile), ref Progress);
				ER = ((CostStructureTypes == null || CostStructureTypes.Length == 0 || IsTypeSelected(CurrFile, CostStructureTypes)) ? oImport.ImportCostStructureInfo(CurrFile, OnlyStructure) : oImport.ImportCostStructureInfo(CurrFile, OnlyStructure: true));
				if (ER.ReturnCode != 0)
				{
					return ER;
				}
			}
			Application.DoEvents();
		}
		catch
		{
		}
		Progress = 100;
		ChgStru.OutputHandlerMessage(eventHandler, "初始化成本架構 - 完成", ref Progress);
		return ER;
	}

	private static bool IsTypeSelected(string CurrFile, string[] CostStructureTypes)
	{
		string EndSymbol = "】";
		string FileName = Path.GetFileNameWithoutExtension(CurrFile);
		string TypeName = FileName.Substring(1, FileName.IndexOf(EndSymbol) - 1);
		foreach (string type in CostStructureTypes)
		{
			if (type == TypeName)
			{
				return true;
			}
		}
		return false;
	}
}
