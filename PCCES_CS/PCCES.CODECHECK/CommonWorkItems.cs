using System;
using System.Data;
using Archnowledge.Pcces.PccesMain;

namespace PCCES.CODECHECK;

internal class CommonWorkItems
{
	private CodeFitter cf;

	public CommonWorkItems()
	{
		Initialized();
	}

	private void Initialized()
	{
		cf = new CodeFitter();
	}

	public void checkDB()
	{
		DBClass dbC = new DBClass();
		DataTable dtMrsBaseA = dbC.GetMrsBaseA();
		foreach (DataRow dr in dtMrsBaseA.Rows)
		{
			string strName;
			string strUnit;
			bool bResult = cf.ValidateCode(dr["pccesCode"].ToString(), out strName, out strUnit);
			if (bResult)
			{
				Console.WriteLine("Test Result" + bResult + ", Pcces Code = " + dr["pccesCode"].ToString() + "," + dr["cName"].ToString() + "," + strName + dr["unitName"].ToString() + "," + strUnit);
			}
		}
	}
}
