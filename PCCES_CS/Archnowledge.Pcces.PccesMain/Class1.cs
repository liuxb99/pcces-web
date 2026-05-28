using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ADODB;
using ADOX;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.STDClass;

namespace Archnowledge.Pcces.PccesMain;

public class Class1
{
	private string ls_UserID;

	public Class1(string UserID)
	{
		ls_UserID = UserID;
	}

	public string[] GetCstring(string stringValue, int columnWidth)
	{
		if (columnWidth == 0 || stringValue.Trim() == string.Empty)
		{
			return new string[1] { stringValue };
		}
		int tmp = cfldline(stringValue, columnWidth);
		string[] returnValue = new string[tmp];
		string ls_PrintSChar = PubTools.GetAppSet_String("cPrintSChar");
		stringValue = stringValue.Trim();
		int li_len = PubTools.LenC(stringValue);
		if (li_len <= columnWidth)
		{
			returnValue[0] = stringValue;
		}
		else
		{
			int x = 0;
			string ls_temp = "";
			do
			{
				ls_temp = PubTools.AddSpece(stringValue, columnWidth + 1).Trim();
				string ls_tmp1 = stringValue.Substring(0, ls_temp.Length);
				if (ls_temp != ls_tmp1)
				{
					ls_temp = stringValue.Substring(0, ls_tmp1.Length - 1);
				}
				string ls_ftemp = ls_temp;
				try
				{
					while (ls_PrintSChar.IndexOf(ls_temp.Substring(ls_temp.Length - 1)) > -1)
					{
						ls_temp = ls_temp.Substring(0, ls_temp.Length - 1);
					}
				}
				catch
				{
					ls_temp = ls_ftemp;
				}
				returnValue[x] = ls_temp;
				x++;
				stringValue = stringValue.Substring(ls_temp.Length);
				li_len = PubTools.LenC(stringValue);
			}
			while (li_len > columnWidth);
			if (li_len > 0)
			{
				returnValue[x] = stringValue;
				x++;
			}
		}
		return returnValue;
	}

	public string[] GetEstring(string col_str, int col_len)
	{
		int tmp = fldline(col_str, col_len);
		string[] RtnVal = new string[tmp + 1];
		string ls_PrintSChar = " " + PubTools.GetAppSet_String("PrintSChar");
		col_str = col_str.Trim();
		int li_len = PubTools.LenC(col_str);
		if (li_len <= col_len)
		{
			RtnVal[0] = col_str;
		}
		else
		{
			int x = 0;
			string ls_temp = "";
			do
			{
				ls_temp = PubTools.AddSpece(col_str, col_len + 1);
				int li_end = -1;
				for (int i = 0; i < ls_PrintSChar.Length; i++)
				{
					string ls_tmpchar = ls_PrintSChar[i].ToString();
					int li_tmpend = ls_temp.LastIndexOf(ls_tmpchar);
					if (li_tmpend > li_end)
					{
						li_end = li_tmpend;
					}
				}
				if (li_end < 0)
				{
					RtnVal[x] = col_str.Substring(0, ls_temp.Length);
					col_str = col_str.Substring(ls_temp.Length);
					x++;
				}
				else
				{
					RtnVal[x] = col_str.Substring(0, li_end + 1);
					col_str = col_str.Substring(li_end + 1);
					x++;
				}
				li_len = PubTools.LenC(col_str);
			}
			while (li_len > col_len);
			if (li_len > 0)
			{
				RtnVal[x] = col_str.Substring(0, col_str.Length);
				x++;
			}
		}
		if (RtnVal[RtnVal.Length - 1] == null)
		{
			string[] aabbc = RtnVal;
			RtnVal = new string[aabbc.Length - 1];
			for (int i = 0; i < aabbc.Length - 1; i++)
			{
				RtnVal[i] = aabbc[i];
			}
		}
		return RtnVal;
	}

	public int cfldline(string col_str, int col_len)
	{
		string ls_PrintSChar = PubTools.GetAppSet_String("cPrintSChar");
		col_str = col_str.Trim();
		int rtnval = 0;
		int li_len = PubTools.LenC(col_str);
		if (li_len <= col_len)
		{
			rtnval = 1;
		}
		else
		{
			string ls_temp = "";
			do
			{
				ls_temp = PubTools.AddSpece(col_str, col_len + 1).Trim();
				string ls_tmp1 = col_str.Substring(0, ls_temp.Length);
				if (ls_temp != ls_tmp1)
				{
					ls_temp = col_str.Substring(0, ls_tmp1.Length - 1);
				}
				string ls_ftmp = ls_temp;
				try
				{
					while (ls_PrintSChar.IndexOf(ls_temp.Substring(ls_temp.Length - 1)) > -1)
					{
						ls_temp = ls_temp.Substring(0, ls_temp.Length - 1);
					}
				}
				catch
				{
					ls_temp = ls_ftmp;
				}
				col_str = col_str.Substring(ls_temp.Length);
				rtnval++;
				li_len = PubTools.LenC(col_str);
			}
			while (li_len > col_len);
			if (li_len > 0)
			{
				rtnval++;
			}
		}
		return rtnval;
	}

	public int fldline(string col_str, int col_len)
	{
		string ls_PrintSChar = " " + PubTools.GetAppSet_String("PrintSChar");
		bool trydata = PubTools.GetAppSet_Bool("DarDebugMode");
		StreamWriter writer = null;
		if (trydata)
		{
			try
			{
				writer = new StreamWriter("C:\\temp\\RptData.log", append: true, Encoding.GetEncoding(950));
				writer.WriteLine("Input String  --> " + col_str);
				Thread.Sleep(1);
				Application.DoEvents();
			}
			catch
			{
			}
		}
		col_str = col_str.Trim();
		int rtnval = 0;
		int li_len = PubTools.LenC(col_str);
		if (li_len <= col_len)
		{
			if (trydata)
			{
				try
				{
					writer.WriteLine("OutPut String --> " + col_str + " : " + li_len);
					writer.WriteLine("OutPut Lins   --> 1");
					writer.WriteLine();
				}
				catch
				{
				}
			}
			rtnval = 1;
		}
		else
		{
			string ls_temp = "";
			do
			{
				ls_temp = PubTools.AddSpece(col_str, col_len + 1);
				if (trydata)
				{
					try
					{
						writer.WriteLine("Substr String --> " + ls_temp);
					}
					catch
					{
					}
				}
				int li_end = -1;
				for (int i = 0; i < ls_PrintSChar.Length; i++)
				{
					string ls_tmpchar = ls_PrintSChar[i].ToString();
					int li_tmpend = ls_temp.LastIndexOf(ls_tmpchar);
					if (li_tmpend > li_end)
					{
						li_end = li_tmpend;
					}
				}
				if (li_end < 0)
				{
					if (trydata)
					{
						try
						{
							writer.WriteLine("OutPut String --> " + col_str.Substring(0, ls_temp.Length) + " : " + ls_temp.Length);
						}
						catch
						{
						}
					}
					col_str = col_str.Substring(ls_temp.Length);
				}
				else
				{
					if (trydata)
					{
						try
						{
							writer.WriteLine("OutPut String --> " + col_str.Substring(0, li_end) + " : " + li_end);
						}
						catch
						{
						}
					}
					if (li_end == 0)
					{
						break;
					}
					col_str = col_str.Substring(li_end);
				}
				rtnval++;
				li_len = PubTools.LenC(col_str);
			}
			while (li_len > col_len);
			if (li_len > 0)
			{
				if (trydata)
				{
					try
					{
						writer.WriteLine("OutPut String --> " + col_str + " : " + li_len);
					}
					catch
					{
					}
				}
				rtnval++;
			}
			if (trydata)
			{
				try
				{
					writer.WriteLine("OutPut Lins   --> " + rtnval);
					writer.WriteLine();
				}
				catch
				{
				}
			}
		}
		if (trydata)
		{
			try
			{
				writer.Close();
				Thread.Sleep(1);
				Application.DoEvents();
			}
			catch
			{
			}
		}
		return rtnval;
	}

	public ExecResult CreateReport(DataSet dsSource, string TableName, string MDBName, string sFTP)
	{
		ExecResult ER = new ExecResult();
		if (dsSource.Tables.Count == 0 || dsSource.Tables[0].Rows.Count == 0)
		{
			ER.ReturnCode = 1;
			ER.Message = "系統並沒有找到任何資料。";
		}
		else
		{
			try
			{
				string[] mdbList = Directory.GetFiles(sFTP, "*.MDB");
				string[] array = mdbList;
				foreach (string File in array)
				{
					FileInfo fileInfo = new FileInfo(File);
					fileInfo.Delete();
				}
				string[] ldbList = Directory.GetFiles(sFTP, "*.LDB");
				array = ldbList;
				foreach (string File in array)
				{
					FileInfo fileInfo = new FileInfo(File);
					fileInfo.Delete();
				}
			}
			catch (Exception ex)
			{
				ER.ReturnCode = 1;
				ER.Message = "刪除暫存檔失敗，請手動刪除 [" + sFTP + "] 下的 *.mdb 及 *.ldb，訊息 ： " + ex.Message;
			}
			if (ER.ReturnCode == 0)
			{
				Catalog Access = new CatalogClass();
				object missing = Missing.Value;
				string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + MDBName + ";Mode=ReadWrite";
				string strConn2 = "Driver={Microsoft Access Driver (*.mdb)}; DBQ=" + MDBName;
				try
				{
					int FieldSize = 0;
					Access.Create(strConn);
					foreach (DataTable DT in dsSource.Tables)
					{
						Table TB = new TableClass();
						TB.Name = DT.TableName;
						for (int j = 0; j < DT.Columns.Count; j++)
						{
							FieldSize = 0;
							ADOX.DataTypeEnum AccFiledType;
							switch (DT.Columns[j].DataType.FullName)
							{
							case "System.Byte[]":
								AccFiledType = ADOX.DataTypeEnum.adInteger;
								break;
							case "System.Int64":
								AccFiledType = ADOX.DataTypeEnum.adBigInt;
								break;
							case "System.Int32":
								AccFiledType = ADOX.DataTypeEnum.adInteger;
								break;
							case "System.Int16":
								AccFiledType = ADOX.DataTypeEnum.adInteger;
								break;
							case "System.UInt64":
								AccFiledType = ADOX.DataTypeEnum.adUnsignedBigInt;
								break;
							case "System.UInt32":
								AccFiledType = ADOX.DataTypeEnum.adUnsignedInt;
								break;
							case "System.UInt16":
								AccFiledType = ADOX.DataTypeEnum.adUnsignedInt;
								break;
							case "System.Double":
								AccFiledType = ADOX.DataTypeEnum.adDouble;
								break;
							case "System.Decimal":
								AccFiledType = ADOX.DataTypeEnum.adDouble;
								break;
							case "System.String":
								FieldSize = 255;
								AccFiledType = ADOX.DataTypeEnum.adWChar;
								break;
							case "System.DateTime":
								FieldSize = 8;
								AccFiledType = ADOX.DataTypeEnum.adDate;
								break;
							default:
								FieldSize = 255;
								AccFiledType = ADOX.DataTypeEnum.adWChar;
								break;
							}
							TB.Columns.Append(DT.Columns[j].ColumnName, AccFiledType, FieldSize);
							TB.Columns[j].Attributes = ColumnAttributesEnum.adColNullable;
						}
						Access.Tables.Append(TB);
						TB = null;
					}
					Marshal.FinalReleaseComObject(Access.Tables);
					Marshal.FinalReleaseComObject(Access.ActiveConnection);
					Marshal.FinalReleaseComObject(Access);
					Access = null;
					Recordset rsMyTable = new RecordsetClass();
					Connection accessConnection = new ConnectionClass();
					accessConnection.ConnectionString = strConn2;
					accessConnection.Open(strConn, "Admin", "", 0);
					rsMyTable.ActiveConnection = accessConnection;
					foreach (DataTable DT in dsSource.Tables)
					{
						TableName = DT.TableName;
						rsMyTable.Open(TableName, accessConnection, CursorTypeEnum.adOpenForwardOnly, LockTypeEnum.adLockPessimistic, 0);
						for (int k = 0; k < DT.Rows.Count; k++)
						{
							rsMyTable.AddNew(missing, missing);
							for (int j = 0; j < DT.Columns.Count; j++)
							{
								if (DT.Rows[k][j].Equals(DBNull.Value))
								{
									switch (DT.Columns[j].DataType.FullName)
									{
									case "System.Byte[]":
										rsMyTable.Fields[j].Value = 0;
										break;
									case "System.Int64":
										rsMyTable.Fields[j].Value = 0;
										break;
									case "System.Int32":
										rsMyTable.Fields[j].Value = 0;
										break;
									case "System.Int16":
										rsMyTable.Fields[j].Value = 0;
										break;
									case "System.UInt64":
										rsMyTable.Fields[j].Value = 0;
										break;
									case "System.UInt32":
										rsMyTable.Fields[j].Value = 0;
										break;
									case "System.UInt16":
										rsMyTable.Fields[j].Value = 0;
										break;
									case "System.Double":
										rsMyTable.Fields[j].Value = 0;
										break;
									case "System.String":
										rsMyTable.Fields[j].Value = "";
										break;
									case "System.DateTime":
										rsMyTable.Fields[j].Value = DBNull.Value;
										break;
									case "System.Decimal":
										rsMyTable.Fields[j].Value = 0;
										break;
									default:
										rsMyTable.Fields[j].Value = "";
										break;
									}
									continue;
								}
								try
								{
									rsMyTable.Fields[j].Value = DT.Rows[k][j].ToString();
								}
								catch
								{
									try
									{
										switch (DT.Columns[j].DataType.FullName)
										{
										case "System.Byte[]":
											rsMyTable.Fields[j].Value = 0;
											break;
										case "System.Int64":
											rsMyTable.Fields[j].Value = 0;
											break;
										case "System.Int32":
											rsMyTable.Fields[j].Value = 0;
											break;
										case "System.Int16":
											rsMyTable.Fields[j].Value = 0;
											break;
										case "System.UInt64":
											rsMyTable.Fields[j].Value = 0;
											break;
										case "System.UInt32":
											rsMyTable.Fields[j].Value = 0;
											break;
										case "System.UInt16":
											rsMyTable.Fields[j].Value = 0;
											break;
										case "System.Double":
											rsMyTable.Fields[j].Value = 0;
											break;
										case "System.String":
											rsMyTable.Fields[j].Value = "";
											break;
										case "System.DateTime":
											rsMyTable.Fields[j].Value = DBNull.Value;
											break;
										case "System.Decimal":
											rsMyTable.Fields[j].Value = 0;
											break;
										default:
											rsMyTable.Fields[j].Value = "";
											break;
										}
									}
									catch (Exception ex)
									{
										throw new Exception("1: " + ex.Message);
									}
								}
							}
							rsMyTable.Update(missing, missing);
						}
						rsMyTable.Close();
					}
					accessConnection.Close();
					Marshal.FinalReleaseComObject(rsMyTable);
					rsMyTable = null;
					accessConnection = null;
				}
				catch (Exception ex)
				{
					ER.ReturnCode = 1;
					ER.Message = "建立 報表資料失敗，訊息 ： " + ex.Message;
				}
			}
		}
		return ER;
	}

	public static bool IsValidDate(int iYear, int iMonth, int iDay)
	{
		bool bResult = false;
		DateTime dDt = DateTime.Now;
		try
		{
			dDt = Convert.ToDateTime(iYear + "/" + iMonth + "/" + iDay);
			return true;
		}
		catch
		{
			return false;
		}
	}

	public static bool IsValidDate(string sYear, string sMonth, string sDay)
	{
		bool bResult = false;
		DateTime dDt = DateTime.Now;
		try
		{
			dDt = Convert.ToDateTime(sYear + "/" + sMonth + "/" + sDay);
			return true;
		}
		catch
		{
			return false;
		}
	}

	public static bool IsValidNum(string sNumStr)
	{
		bool bResult = false;
		try
		{
			double aNum = Convert.ToDouble(sNumStr);
			return true;
		}
		catch
		{
			return false;
		}
	}

	public static bool IsValidDate(string sStrDate)
	{
		bool bResult = false;
		if (sStrDate == "")
		{
			return true;
		}
		if (sStrDate.Trim().Length == 8)
		{
			DateTime dDt = DateTime.Now;
			try
			{
				dDt = Convert.ToDateTime(sStrDate.Substring(0, 4) + "/" + sStrDate.Substring(4, 2) + "/" + sStrDate.Substring(6, 2));
				return true;
			}
			catch
			{
				return false;
			}
		}
		try
		{
			DateTime.Parse(sStrDate);
			return true;
		}
		catch
		{
			return false;
		}
	}

	public static bool IsValidStringLength(string sString, int Length)
	{
		bool bResult = false;
		int li_len = PubTools.LenC(sString);
		if (li_len > Length)
		{
			return false;
		}
		return true;
	}

	public static string get_cmp_Ename(string tm_maincode, string ls_UserID)
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add("System");
		tmp_AL1.Add("讀取主辦單位名稱");
		string ls_mainname = "";
		MainUnitCom MainCom = new MainUnitCom(tmp_AL1);
		ls_mainname = MainCom.Get_Main_EName(tm_maincode);
		MainCom = null;
		return ls_mainname;
	}

	public static string get_cmp_name(string tm_maincode, string ls_UserID)
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add("System");
		tmp_AL1.Add("讀取主辦單位名稱");
		string ls_mainname = "";
		MainUnitCom MainCom = new MainUnitCom(tmp_AL1);
		ls_mainname = MainCom.Get_Main_Name(tm_maincode);
		MainCom = null;
		return ls_mainname;
	}

	public static double ARound(double x, long y)
	{
		double RetV = 0.0;
		if (x < 0.0)
		{
			return (double)(long)(x * Math.Pow(10.0, y) - 0.5) / Math.Pow(10.0, y);
		}
		return (double)(long)(x * Math.Pow(10.0, y) + 0.5) / Math.Pow(10.0, y);
	}

	public static bool ChkPower(string ls_power, string kind)
	{
		bool renval = false;
		return kind.ToUpper() switch
		{
			"G" => ls_power.Substring(0, 1) == "1" || ls_power.Substring(1, 1) == "1" || ls_power.Substring(2, 1) == "1" || ls_power.Substring(3, 1) == "1", 
			"U" => ls_power.Substring(1, 1) == "1", 
			"I" => ls_power.Substring(2, 1) == "1", 
			"D" => ls_power.Substring(3, 1) == "1", 
			_ => false, 
		};
	}

	public static int Str2Int(string v1)
	{
		int renval = 0;
		try
		{
			return int.Parse(v1);
		}
		catch
		{
			return 0;
		}
	}

	public static double Str2Double(string v1)
	{
		double renval = 0.0;
		try
		{
			return double.Parse(v1);
		}
		catch
		{
			return 0.0;
		}
	}

	public static DateTime Str2DateTime(string v1)
	{
		DateTime renval = DateTime.Parse("1800/1/1");
		try
		{
			return DateTime.Parse(v1);
		}
		catch
		{
			return DateTime.Parse("1800/1/1");
		}
	}
}
