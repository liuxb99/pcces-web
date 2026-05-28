using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.PccesMain;

namespace PCCES.CODECHECK;

internal class CodeValidator
{
	private DataTable _dtAutoNumB;

	private DataTable _dtAutoNumA;

	private string _strBizRule;

	private string _strMinRow;

	private string _strMaxRow;

	private string _strSelfRow;

	private string _strFullCode;

	private string _strChapCode;

	private string _strChapCodeName;

	private string _strCodePrefix;

	private string _strCodeParsed;

	private string _strName;

	private string _strUnit;

	private string _strRM;

	private bool _bSkipNextCode;

	private bool _bGroup;

	private string _strPostCodes;

	private string _strStar;

	private string _strCompareErrState;

	public string[,] AlternativeUnit;

	private string F_UserID;

	private DBClass DBCLS = new DBClass();

	private string F_ProjectCode;

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

	public string _UserID
	{
		get
		{
			return F_UserID;
		}
		set
		{
			F_UserID = value;
			DBCLS._FS_UserID = F_UserID;
		}
	}

	private void Initialized()
	{
		_strMinRow = "";
		_strMaxRow = "";
		_strSelfRow = "";
		_strFullCode = "";
		_strChapCode = "";
		_strCodePrefix = "";
		_strCodeParsed = "";
		_strName = "";
		_strUnit = "";
		_strRM = "";
		_strChapCodeName = "";
		_bSkipNextCode = false;
		_bGroup = false;
		_strPostCodes = "";
		_strStar = "";
		_strCompareErrState = "";
	}

	public CodeValidator(DataTable dtAutoNumA, DataTable dtAutoNumB)
	{
		AlternativeUnit = new string[5, 6];
		AlternativeUnit[0, 0] = "M";
		AlternativeUnit[0, 1] = "m";
		AlternativeUnit[0, 2] = "公尺";
		AlternativeUnit[0, 3] = "米";
		AlternativeUnit[0, 4] = "";
		AlternativeUnit[0, 5] = "";
		AlternativeUnit[1, 0] = "M2";
		AlternativeUnit[1, 1] = "m2";
		AlternativeUnit[1, 2] = "平方公尺";
		AlternativeUnit[1, 3] = "平方米";
		AlternativeUnit[1, 4] = "";
		AlternativeUnit[1, 5] = "";
		AlternativeUnit[2, 0] = "M3";
		AlternativeUnit[2, 1] = "m3";
		AlternativeUnit[2, 2] = "立方公尺";
		AlternativeUnit[2, 3] = "立方米";
		AlternativeUnit[2, 4] = "";
		AlternativeUnit[2, 5] = "";
		AlternativeUnit[3, 0] = "T";
		AlternativeUnit[3, 1] = "t";
		AlternativeUnit[3, 2] = "公噸";
		AlternativeUnit[3, 3] = "噸";
		AlternativeUnit[3, 4] = "";
		AlternativeUnit[3, 5] = "";
		AlternativeUnit[4, 0] = "KG";
		AlternativeUnit[4, 1] = "Kg";
		AlternativeUnit[4, 2] = "kg";
		AlternativeUnit[4, 3] = "公斤";
		AlternativeUnit[4, 4] = "千克";
		AlternativeUnit[4, 5] = "兛";
		_dtAutoNumA = dtAutoNumA;
		_dtAutoNumB = dtAutoNumB;
		_strBizRule = SetupBizRule();
		_dtAutoNumB.CaseSensitive = true;
	}

	public bool ValidateCode(string strCode, out string strName, out string strUnit, out string strCompareErrState, out string strChapCodeCorrect, out string strNameAlt, out string strChapName)
	{
		bool bResult = false;
		Initialized();
		strName = "";
		strNameAlt = "";
		strChapName = "";
		strUnit = "";
		strCompareErrState = "";
		strChapCodeCorrect = "";
		string sExt = "";
		if (char.IsNumber(strCode, 0) && strCode.Length >= 5)
		{
			_strChapCode = strCode.Substring(0, 5);
			if (strCode.Length < 10)
			{
				_strCompareErrState += "工項編碼長度不足";
			}
		}
		else
		{
			string tmp1Code = strCode.Substring(0, 1);
			if ("LEMW".IndexOf(tmp1Code) <= -1)
			{
				_strCompareErrState += "非正常編碼(開頭不是L,E,M,W)";
				strChapCodeCorrect = "否";
			}
			else if (tmp1Code == "M")
			{
				if (strCode.Length >= 6)
				{
					_strChapCode = strCode.Substring(1, 5);
				}
				if (strCode.Length < 11)
				{
					_strCompareErrState += "工項編碼長度不足";
				}
			}
			else
			{
				_strChapCode = "";
				switch (tmp1Code)
				{
				case "W":
					if (strCode.Length < 11)
					{
						_strCompareErrState += "工項編碼長度不足";
					}
					break;
				case "L":
					if (strCode.Length < 13)
					{
						_strCompareErrState += "工項編碼長度不足";
					}
					break;
				case "E":
					if (strCode.Length < 13)
					{
						_strCompareErrState += "工項編碼長度不足";
					}
					break;
				}
			}
		}
		if (_strChapCode != "")
		{
			string strCriteria = "itemCode = '" + _strChapCode + "'";
			DataRow[] DRs = _dtAutoNumA.Select(strCriteria);
			if (DRs.Length <= 0)
			{
				strCompareErrState = ((_strCompareErrState.Trim() == "") ? "綱要編碼錯誤" : (_strCompareErrState + "，綱要編碼錯誤"));
				strChapCodeCorrect = "否";
				return bResult;
			}
			sExt = DRs[0]["Ext"].ToString();
		}
		bool 是特殊章篇 = false;
		string 特殊章篇最後一碼 = "";
		try
		{
			if (char.IsNumber(strCode, 0))
			{
				_strChapCode = strCode.Substring(0, 5);
				if (IsInParticularCode(_strChapCode) && strCode.Length == 11)
				{
					是特殊章篇 = true;
					特殊章篇最後一碼 = strCode.Substring(10);
					strCode = strCode.Substring(0, strCode.Length - 1);
				}
			}
			else
			{
				string tmp1Code = strCode.Substring(0, 1);
				if (tmp1Code == "M")
				{
					_strChapCode = strCode.Substring(1, 5);
					if (IsInParticularCode(_strChapCode) && strCode.Length == 12)
					{
						是特殊章篇 = true;
						特殊章篇最後一碼 = strCode.Substring(11);
						strCode = tmp1Code + strCode.Substring(1, strCode.Length - 1);
					}
				}
			}
			bResult = true;
		}
		catch
		{
			bResult = false;
		}
		bool flag = false;
		if (strCode.Length == 11 && strCode.Substring(0, 1) == "M")
		{
			if (是特殊章篇)
			{
				if (strCode.Substring(6) == "00000")
				{
					strCompareErrState = "不符編碼規則";
					DataTable DT_X = new DataTable();
					DT_X = ((F_ProjectCode == null || !(F_ProjectCode != "")) ? DBCLS.GetUserDefine("Select pccesCode from MrsBaseA Where pccesCode like '" + getPrefixCode(strCode) + "%'") : DBCLS.GetUserDefine("Select pccesCode from budProjMrsA Where projectCode=N'" + F_ProjectCode + "' and pccesCode like '" + getPrefixCode(strCode) + "%'"));
					if (DT_X.Rows.Count != 1)
					{
						DataView DV = DT_X.DefaultView;
						DV.Sort = "pccesCode Asc";
						for (int i = 0; i < DV.Count && (i != 0 || !(DV[i]["pccesCode"].ToString() == strCode)); i++)
						{
							if (i > 0 && DV[i]["pccesCode"].ToString() == strCode)
							{
								bResult = false;
								break;
							}
						}
					}
				}
			}
			else if (strCode.Substring(6, 4) == "0000" && strCode.Substring(strCode.Length - 1) == "0")
			{
				strCompareErrState = "不符編碼規則";
				DataTable DT_X = new DataTable();
				DT_X = ((F_ProjectCode == null || !(F_ProjectCode != "")) ? DBCLS.GetUserDefine("Select pccesCode from MrsBaseA Where pccesCode like '" + getPrefixCode(strCode) + "%'") : DBCLS.GetUserDefine("Select pccesCode from budProjMrsA Where projectCode=N'" + F_ProjectCode + "' and pccesCode like '" + getPrefixCode(strCode) + "%'"));
				if (DT_X.Rows.Count != 1)
				{
					DataView DV = DT_X.DefaultView;
					DV.Sort = "pccesCode Asc";
					for (int i = 0; i < DV.Count && (i != 0 || !(DV[i]["pccesCode"].ToString() == strCode)); i++)
					{
						if (i > 0 && DV[i]["pccesCode"].ToString() == strCode)
						{
							bResult = false;
							break;
						}
					}
				}
			}
		}
		else if (strCode.Length == 10 && (strCode.Substring(0, 1) == "0" || strCode.Substring(0, 1) == "1"))
		{
			if (是特殊章篇)
			{
				if (strCode.Substring(5) == "00000")
				{
					strCompareErrState = "不符編碼規則";
					DataTable DT_X = new DataTable();
					DT_X = ((F_ProjectCode == null || !(F_ProjectCode != "")) ? DBCLS.GetUserDefine("Select pccesCode from MrsBaseA Where pccesCode like '" + getPrefixCode(strCode) + "%'") : DBCLS.GetUserDefine("Select pccesCode from budProjMrsA Where projectCode=N'" + F_ProjectCode + "' and pccesCode like '" + getPrefixCode(strCode) + "%'"));
					if (DT_X.Rows.Count != 1)
					{
						DataView DV = DT_X.DefaultView;
						DV.Sort = "pccesCode Asc";
						for (int i = 0; i < DV.Count && (i != 0 || !(DV[i]["pccesCode"].ToString() == strCode)); i++)
						{
							if (i > 0 && DV[i]["pccesCode"].ToString() == strCode)
							{
								bResult = false;
								break;
							}
						}
					}
				}
			}
			else if (strCode.Substring(5, 4) == "0000" && strCode.Substring(strCode.Length - 1) == "0")
			{
				strCompareErrState = "不符編碼規則";
				DataTable DT_X = new DataTable();
				DT_X = ((F_ProjectCode == null || !(F_ProjectCode != "")) ? DBCLS.GetUserDefine("Select pccesCode from MrsBaseA Where pccesCode like '" + getPrefixCode(strCode) + "%'") : DBCLS.GetUserDefine("Select pccesCode from budProjMrsA Where projectCode=N'" + F_ProjectCode + "' and pccesCode like '" + getPrefixCode(strCode) + "%'"));
				if (DT_X.Rows.Count != 1)
				{
					DataView DV = DT_X.DefaultView;
					DV.Sort = "pccesCode Asc";
					for (int i = 0; i < DV.Count && (i != 0 || !(DV[i]["pccesCode"].ToString() == strCode)); i++)
					{
						if (i > 0 && DV[i]["pccesCode"].ToString() == strCode)
						{
							bResult = false;
							break;
						}
					}
				}
			}
		}
		else if (sExt != "12")
		{
			try
			{
				if (strCode.Substring(0, 1) == "M")
				{
					if (是特殊章篇)
					{
						if (strCode.Substring(6) == "00000")
						{
							strCompareErrState = "不符編碼規則";
							DataTable DT_X = new DataTable();
							DT_X = ((F_ProjectCode == null || !(F_ProjectCode != "")) ? DBCLS.GetUserDefine("Select pccesCode from MrsBaseA Where pccesCode like '" + getPrefixCode(strCode) + "%'") : DBCLS.GetUserDefine("Select pccesCode from budProjMrsA Where projectCode=N'" + F_ProjectCode + "' and pccesCode like '" + getPrefixCode(strCode) + "%'"));
							if (DT_X.Rows.Count != 1)
							{
								DataView DV = DT_X.DefaultView;
								DV.Sort = "pccesCode Asc";
								for (int i = 0; i < DV.Count && (i != 0 || !(DV[i]["pccesCode"].ToString() == strCode)); i++)
								{
									if (i > 0 && DV[i]["pccesCode"].ToString() == strCode)
									{
										bResult = false;
										break;
									}
								}
							}
						}
					}
					else if (strCode.Substring(6, 4) == "0000" && strCode.Substring(strCode.Length - 1) == "0")
					{
						strCompareErrState = "不符編碼規則";
						DataTable DT_X = new DataTable();
						DT_X = ((F_ProjectCode == null || !(F_ProjectCode != "")) ? DBCLS.GetUserDefine("Select pccesCode from MrsBaseA Where pccesCode like '" + getPrefixCode(strCode) + "%'") : DBCLS.GetUserDefine("Select pccesCode from budProjMrsA Where projectCode=N'" + F_ProjectCode + "' and pccesCode like '" + getPrefixCode(strCode) + "%'"));
						if (DT_X.Rows.Count != 1)
						{
							DataView DV = DT_X.DefaultView;
							DV.Sort = "pccesCode Asc";
							for (int i = 0; i < DV.Count && (i != 0 || !(DV[i]["pccesCode"].ToString() == strCode)); i++)
							{
								if (i > 0 && DV[i]["pccesCode"].ToString() == strCode)
								{
									bResult = false;
									break;
								}
							}
						}
					}
				}
				else if (strCode.Substring(0, 1) == "0" || strCode.Substring(0, 1) == "1")
				{
					if (是特殊章篇)
					{
						if (strCode.Substring(5) == "00000")
						{
							strCompareErrState = "不符編碼規則";
							DataTable DT_X = new DataTable();
							DT_X = ((F_ProjectCode == null || !(F_ProjectCode != "")) ? DBCLS.GetUserDefine("Select pccesCode from MrsBaseA Where pccesCode like '" + getPrefixCode(strCode) + "%'") : DBCLS.GetUserDefine("Select pccesCode from budProjMrsA Where projectCode=N'" + F_ProjectCode + "' and pccesCode like '" + getPrefixCode(strCode) + "%'"));
							if (DT_X.Rows.Count != 1)
							{
								DataView DV = DT_X.DefaultView;
								DV.Sort = "pccesCode Asc";
								for (int i = 0; i < DV.Count && (i != 0 || !(DV[i]["pccesCode"].ToString() == strCode)); i++)
								{
									if (i > 0 && DV[i]["pccesCode"].ToString() == strCode)
									{
										bResult = false;
										break;
									}
								}
							}
						}
					}
					else if (strCode.Substring(5, 4) == "0000" && strCode.Substring(strCode.Length - 1) == "0")
					{
						strCompareErrState = "不符編碼規則";
						DataTable DT_X = new DataTable();
						DT_X = ((F_ProjectCode == null || !(F_ProjectCode != "")) ? DBCLS.GetUserDefine("Select pccesCode from MrsBaseA Where pccesCode like '" + getPrefixCode(strCode) + "%'") : DBCLS.GetUserDefine("Select pccesCode from budProjMrsA Where projectCode=N'" + F_ProjectCode + "' and pccesCode like '" + getPrefixCode(strCode) + "%'"));
						if (DT_X.Rows.Count != 1)
						{
							DataView DV = DT_X.DefaultView;
							DV.Sort = "pccesCode Asc";
							for (int i = 0; i < DV.Count && (i != 0 || !(DV[i]["pccesCode"].ToString() == strCode)); i++)
							{
								if (i > 0 && DV[i]["pccesCode"].ToString() == strCode)
								{
									bResult = false;
									break;
								}
							}
						}
					}
				}
			}
			catch
			{
				bResult = false;
			}
		}
		else if (strCode.Length >= 13 && strCode.Substring(0, 1) == "M")
		{
			if (是特殊章篇)
			{
				if (strCode.Substring(6) == "0000000")
				{
					strCompareErrState = "不符編碼規則";
					DataTable DT_X = new DataTable();
					DT_X = ((F_ProjectCode == null || !(F_ProjectCode != "")) ? DBCLS.GetUserDefine("Select pccesCode from MrsBaseA Where pccesCode like '" + getPrefixCode(strCode) + "%'") : DBCLS.GetUserDefine("Select pccesCode from budProjMrsA Where projectCode=N'" + F_ProjectCode + "' and pccesCode like '" + getPrefixCode(strCode) + "%'"));
					if (DT_X.Rows.Count != 1)
					{
						DataView DV = DT_X.DefaultView;
						DV.Sort = "pccesCode Asc";
						for (int i = 0; i < DV.Count && (i != 0 || !(DV[i]["pccesCode"].ToString() == strCode)); i++)
						{
							if (i > 0 && DV[i]["pccesCode"].ToString() == strCode)
							{
								bResult = false;
								break;
							}
						}
					}
				}
			}
			else if (strCode.Substring(6, 6) == "000000" && strCode.Substring(strCode.Length - 1) == "0")
			{
				strCompareErrState = "不符編碼規則";
				DataTable DT_X = new DataTable();
				DT_X = ((F_ProjectCode == null || !(F_ProjectCode != "")) ? DBCLS.GetUserDefine("Select pccesCode from MrsBaseA Where pccesCode like '" + getPrefixCode(strCode) + "%'") : DBCLS.GetUserDefine("Select pccesCode from budProjMrsA Where projectCode=N'" + F_ProjectCode + "' and pccesCode like '" + getPrefixCode(strCode) + "%'"));
				if (DT_X.Rows.Count != 1)
				{
					DataView DV = DT_X.DefaultView;
					DV.Sort = "pccesCode Asc";
					for (int i = 0; i < DV.Count && (i != 0 || !(DV[i]["pccesCode"].ToString() == strCode)); i++)
					{
						if (i > 0 && DV[i]["pccesCode"].ToString() == strCode)
						{
							bResult = false;
							break;
						}
					}
				}
			}
		}
		else if (strCode.Length >= 12 && (strCode.Substring(0, 1) == "0" || strCode.Substring(0, 1) == "1"))
		{
			if (是特殊章篇)
			{
				if (strCode.Substring(5) == "0000000")
				{
					strCompareErrState = "不符編碼規則";
					DataTable DT_X = new DataTable();
					DT_X = ((F_ProjectCode == null || !(F_ProjectCode != "")) ? DBCLS.GetUserDefine("Select pccesCode from MrsBaseA Where pccesCode like '" + getPrefixCode(strCode) + "%'") : DBCLS.GetUserDefine("Select pccesCode from budProjMrsA Where projectCode=N'" + F_ProjectCode + "' and pccesCode like '" + getPrefixCode(strCode) + "%'"));
					if (DT_X.Rows.Count != 1)
					{
						DataView DV = DT_X.DefaultView;
						DV.Sort = "pccesCode Asc";
						for (int i = 0; i < DV.Count && (i != 0 || !(DV[i]["pccesCode"].ToString() == strCode)); i++)
						{
							if (i > 0 && DV[i]["pccesCode"].ToString() == strCode)
							{
								bResult = false;
								break;
							}
						}
					}
				}
			}
			else if (strCode.Substring(5, 6) == "000000" && strCode.Substring(strCode.Length - 1) == "0")
			{
				strCompareErrState = "不符編碼規則";
				DataTable DT_X = new DataTable();
				DT_X = ((F_ProjectCode == null || !(F_ProjectCode != "")) ? DBCLS.GetUserDefine("Select pccesCode from MrsBaseA Where pccesCode like '" + getPrefixCode(strCode) + "%'") : DBCLS.GetUserDefine("Select pccesCode from budProjMrsA Where projectCode=N'" + F_ProjectCode + "' and pccesCode like '" + getPrefixCode(strCode) + "%'"));
				if (DT_X.Rows.Count != 1)
				{
					DataView DV = DT_X.DefaultView;
					DV.Sort = "pccesCode Asc";
					for (int i = 0; i < DV.Count && (i != 0 || !(DV[i]["pccesCode"].ToString() == strCode)); i++)
					{
						if (i > 0 && DV[i]["pccesCode"].ToString() == strCode)
						{
							bResult = false;
							break;
						}
					}
				}
			}
		}
		else if (sExt == "12")
		{
			try
			{
				if (strCode.Substring(0, 1) == "M")
				{
					if (是特殊章篇)
					{
						if (strCode.Substring(6) == "0000000")
						{
							strCompareErrState = "不符編碼規則";
							DataTable DT_X = new DataTable();
							DT_X = ((F_ProjectCode == null || !(F_ProjectCode != "")) ? DBCLS.GetUserDefine("Select pccesCode from MrsBaseA Where pccesCode like '" + getPrefixCode(strCode) + "%'") : DBCLS.GetUserDefine("Select pccesCode from budProjMrsA Where projectCode=N'" + F_ProjectCode + "' and pccesCode like '" + getPrefixCode(strCode) + "%'"));
							if (DT_X.Rows.Count != 1)
							{
								DataView DV = DT_X.DefaultView;
								DV.Sort = "pccesCode Asc";
								for (int i = 0; i < DV.Count && (i != 0 || !(DV[i]["pccesCode"].ToString() == strCode)); i++)
								{
									if (i > 0 && DV[i]["pccesCode"].ToString() == strCode)
									{
										bResult = false;
										break;
									}
								}
							}
						}
					}
					else if (strCode.Substring(6, 6) == "000000" && strCode.Substring(strCode.Length - 1) == "0")
					{
						strCompareErrState = "不符編碼規則";
						DataTable DT_X = new DataTable();
						DT_X = ((F_ProjectCode == null || !(F_ProjectCode != "")) ? DBCLS.GetUserDefine("Select pccesCode from MrsBaseA Where pccesCode like '" + getPrefixCode(strCode) + "%'") : DBCLS.GetUserDefine("Select pccesCode from budProjMrsA Where projectCode=N'" + F_ProjectCode + "' and pccesCode like '" + getPrefixCode(strCode) + "%'"));
						if (DT_X.Rows.Count != 1)
						{
							DataView DV = DT_X.DefaultView;
							DV.Sort = "pccesCode Asc";
							for (int i = 0; i < DV.Count && (i != 0 || !(DV[i]["pccesCode"].ToString() == strCode)); i++)
							{
								if (i > 0 && DV[i]["pccesCode"].ToString() == strCode)
								{
									bResult = false;
									break;
								}
							}
						}
					}
				}
				else if (strCode.Substring(0, 1) == "0" || strCode.Substring(0, 1) == "1")
				{
					if (是特殊章篇)
					{
						if (strCode.Substring(5) == "0000000")
						{
							strCompareErrState = "不符編碼規則";
							DataTable DT_X = new DataTable();
							DT_X = ((F_ProjectCode == null || !(F_ProjectCode != "")) ? DBCLS.GetUserDefine("Select pccesCode from MrsBaseA Where pccesCode like '" + getPrefixCode(strCode) + "%'") : DBCLS.GetUserDefine("Select pccesCode from budProjMrsA Where projectCode=N'" + F_ProjectCode + "' and pccesCode like '" + getPrefixCode(strCode) + "%'"));
							if (DT_X.Rows.Count != 1)
							{
								DataView DV = DT_X.DefaultView;
								DV.Sort = "pccesCode Asc";
								for (int i = 0; i < DV.Count && (i != 0 || !(DV[i]["pccesCode"].ToString() == strCode)); i++)
								{
									if (i > 0 && DV[i]["pccesCode"].ToString() == strCode)
									{
										bResult = false;
										break;
									}
								}
							}
						}
					}
					else if (strCode.Substring(5, 6) == "000000" && strCode.Substring(strCode.Length - 1) == "0")
					{
						strCompareErrState = "不符編碼規則";
						DataTable DT_X = new DataTable();
						DT_X = ((F_ProjectCode == null || !(F_ProjectCode != "")) ? DBCLS.GetUserDefine("Select pccesCode from MrsBaseA Where pccesCode like '" + getPrefixCode(strCode) + "%'") : DBCLS.GetUserDefine("Select pccesCode from budProjMrsA Where projectCode=N'" + F_ProjectCode + "' and pccesCode like '" + getPrefixCode(strCode) + "%'"));
						if (DT_X.Rows.Count != 1)
						{
							DataView DV = DT_X.DefaultView;
							DV.Sort = "pccesCode Asc";
							for (int i = 0; i < DV.Count && (i != 0 || !(DV[i]["pccesCode"].ToString() == strCode)); i++)
							{
								if (i > 0 && DV[i]["pccesCode"].ToString() == strCode)
								{
									bResult = false;
									break;
								}
							}
						}
					}
				}
			}
			catch
			{
				bResult = false;
			}
		}
		if (bResult)
		{
			_strFullCode = strCode;
		}
		if (bResult)
		{
			bResult = ((sExt == "") ? ValidateByBizRule(_strFullCode) : (!(sExt == "12") || true));
		}
		if (bResult)
		{
			ParseCode(strCode);
		}
		if (bResult)
		{
			bResult = ValidateChapCode(_strChapCode, sExt);
		}
		GetChapCodeName(_strChapCode);
		strChapName = _strChapCodeName;
		if (_strFullCode == null || _strFullCode == "")
		{
			if (strCompareErrState == "不符編碼規則" && _strCompareErrState == "")
			{
				strCompareErrState = strCompareErrState;
			}
			else
			{
				strCompareErrState = _strCompareErrState;
			}
			return bResult;
		}
		if (_strFullCode.Substring(0, 1) != "L" && _strFullCode.Substring(0, 1) != "E")
		{
			string sDislocaton = "";
			if (bResult)
			{
				bool chkResult = false;
				chkResult = ValidateCodeByCodeSection_New(_strChapCode, out var scName, out var sUnit, sExt, out sDislocaton);
				if (chkResult)
				{
					strName = RemoveLastCommon(scName);
					strNameAlt = strName;
					strName = ((_strStar == "*") ? (_strChapCodeName + "，" + strName) : strName);
					if (_strStar != "*" && _strPostCodes.Substring(0, 1) == "0")
					{
						strName = _strChapCodeName + "，" + strName;
					}
					if (strName.Trim() == "")
					{
						strName = _strChapCodeName;
					}
					strName = RemoveLastCommon(strName);
					strName = ((_strCodePrefix == "M") ? ("產品，" + strName) : strName);
					strUnit = sUnit.Replace("L.M3依CNS387 建築用砂之順序", "L.M3").Replace("根括套管、錨碇設備", "根").Replace("具CNS3220-3  R2163-3", "具");
				}
				bResult = chkResult;
				if (sDislocaton != "")
				{
					sDislocaton = RemoveLastCommon(sDislocaton);
					bResult = false;
				}
			}
			strCompareErrState = _strCompareErrState + ((sDislocaton != "") ? sDislocaton : "");
			if (是特殊章篇 && !(特殊章篇最後一碼 == "1"))
			{
				bResult = false;
				strCompareErrState += "特殊章篇末碼應為【1】";
			}
			if (bResult)
			{
				if (sExt == "")
				{
					if (char.IsNumber(_strFullCode, 0))
					{
						if (_strFullCode.Length != 10 && _strFullCode.Length < 10)
						{
							strCompareErrState = "不符編碼規則";
							bResult = false;
						}
					}
					else if (_strFullCode.Length != 11 && _strFullCode.Length < 11)
					{
						strCompareErrState = "不符編碼規則";
						bResult = false;
					}
				}
				else if (sExt == "12")
				{
					if (char.IsNumber(_strFullCode, 0))
					{
						if (_strFullCode.Length != 12 && _strFullCode.Length < 12)
						{
							strCompareErrState = "不符編碼規則";
							bResult = false;
						}
					}
					else if (_strFullCode.Length != 13 && _strFullCode.Length < 13)
					{
						strCompareErrState = "不符編碼規則";
						bResult = false;
					}
				}
			}
			return bResult;
		}
		if ((_strCodePrefix.ToUpper() == "E" || _strCodePrefix.ToUpper() == "L") && _strFullCode.Length < 13)
		{
			bResult = false;
		}
		if (bResult)
		{
			bResult = ValidateCodeByCodeSection(_strChapCode, "06");
		}
		if (bResult)
		{
			bResult = ValidateCodeByCodeSection(_strChapCode, "07");
		}
		if (bResult)
		{
			bResult = ValidateCodeByCodeSection(_strChapCode, "08");
		}
		if (bResult)
		{
			bResult = ValidateCodeByCodeSection(_strChapCode, "09");
		}
		if (bResult)
		{
			bResult = ValidateCodeByCodeSection(_strChapCode, "10");
		}
		if (bResult)
		{
			bResult = ValidateCodeByCodeSection(_strChapCode, "11");
		}
		if (bResult)
		{
			bResult = FormatNameAndUnit();
		}
		if (bResult)
		{
			_strCompareErrState = "";
		}
		if (bResult)
		{
			if (_strCodePrefix.ToUpper() == "E" || _strCodePrefix.ToUpper() == "L")
			{
				string sUnitCode = "";
				if (_strFullCode.Length >= 13)
				{
					sUnitCode = _strFullCode.Substring(12, 1);
				}
				bResult = GetLEUnit(_strChapCode, sUnitCode);
			}
			if (_strFullCode.Length < 6)
			{
				strName = RemoveLastCommon(_strName);
				strUnit = _strUnit;
				strCompareErrState = "工項編碼長度不足";
				return false;
			}
			if (bResult && _strFullCode.Substring(1, 5) != "00000")
			{
				DataRow[] dr_autoA = _dtAutoNumA.Select("itemCode='" + _strFullCode.Substring(1, 5) + "'");
				if (dr_autoA.Length > 0)
				{
					_strName = dr_autoA[0]["cName"].ToString().Trim() + "，" + _strName;
				}
			}
		}
		strName = RemoveLastCommon(_strName);
		strUnit = _strUnit;
		strCompareErrState = _strCompareErrState;
		if (bResult)
		{
			if (_strCodePrefix.ToUpper() == "E" || _strCodePrefix.ToUpper() == "L")
			{
				if ((_strFullCode.Length <= 13 || _strFullCode.IndexOf("-") <= 12) && _strFullCode.Length != 13)
				{
					strCompareErrState = "不符編碼規則";
					bResult = false;
				}
			}
			else if (sExt == "")
			{
				if (char.IsNumber(_strFullCode, 0))
				{
					if ((_strFullCode.Length <= 10 || _strFullCode.IndexOf("-") <= 9) && _strFullCode.Length != 10)
					{
						strCompareErrState = "不符編碼規則";
						bResult = false;
					}
				}
				else if ((_strFullCode.Length <= 11 || _strFullCode.IndexOf("-") <= 10) && _strFullCode.Length != 11)
				{
					strCompareErrState = "不符編碼規則";
					bResult = false;
				}
			}
			else if (sExt == "12")
			{
				if (char.IsNumber(_strFullCode, 0))
				{
					if ((_strFullCode.Length <= 12 || _strFullCode.IndexOf("-") <= 11) && _strFullCode.Length != 12)
					{
						strCompareErrState = "不符編碼規則";
						bResult = false;
					}
				}
				else if ((_strFullCode.Length <= 13 || _strFullCode.IndexOf("-") <= 12) && _strFullCode.Length != 13)
				{
					strCompareErrState = "不符編碼規則";
					bResult = false;
				}
			}
		}
		return bResult;
	}

	private string getPrefixCode(string strCode)
	{
		string retV = "";
		if (strCode.Length >= 10 && strCode.Substring(0, 1) == "M")
		{
			return strCode.Substring(0, 10);
		}
		if (strCode.Length >= 9 && (strCode.Substring(0, 1) == "0" || strCode.Substring(0, 1) == "1"))
		{
			return strCode.Substring(0, 10);
		}
		return strCode;
	}

	private bool IsInParticularCode(string chapCode)
	{
		bool retV = false;
		switch (chapCode)
		{
		default:
			if (!(chapCode == "10213"))
			{
				break;
			}
			goto case "02922";
		case "02922":
		case "02923":
		case "02928":
		case "02931":
		case "02938":
		case "02932":
		case "02933":
		case "02934":
			retV = true;
			break;
		}
		return retV;
	}

	private string RemoveLastCommon(string inputStr)
	{
		if (inputStr.Length <= 0)
		{
			return inputStr;
		}
		string tmp1 = inputStr.Substring(inputStr.Length - 1);
		return (!(tmp1 == "，") && !(tmp1 == ",")) ? inputStr : inputStr.Substring(0, inputStr.Length - 1);
	}

	private string SetupBizRule()
	{
		string strPattern = "";
		strPattern += "\\b[ELM]\\d{5}\\w{5,7}\\b|";
		strPattern += "\\bW01271\\w{5}\\b|";
		return strPattern + "\\b\\d{5}\\w{5}\\b";
	}

	private bool ValidateByBizRule(string strCode)
	{
		Regex re = new Regex(_strBizRule, RegexOptions.IgnoreCase);
		return true;
	}

	private void ParseCode(string strCode)
	{
		if (char.IsNumber(strCode, 0))
		{
			_strCodePrefix = "";
			if (strCode.Length >= 5)
			{
				_strChapCode = strCode.Substring(0, 5);
			}
			else
			{
				_strChapCode = strCode.Substring(0);
			}
		}
		else
		{
			_strCodePrefix = strCode.Substring(0, 1);
			if (strCode.Length >= 6)
			{
				_strChapCode = strCode.Substring(1, 5);
			}
			else
			{
				_strChapCode = strCode.Substring(1);
			}
		}
		_strCodeParsed = _strCodePrefix + _strChapCode;
		try
		{
			_strPostCodes = _strFullCode.Substring(_strCodeParsed.Length);
		}
		catch
		{
			_strPostCodes = "";
		}
	}

	private string GetNextCode(int i)
	{
		string strTemp = _strFullCode.Substring(_strCodeParsed.Length, i);
		_strCodeParsed += strTemp;
		return strTemp;
	}

	private void GetChapCodeName(string strChapCode)
	{
		string criteria = "itemCode = '" + strChapCode + "'";
		DataRow[] dr = _dtAutoNumA.Select(criteria);
		if (dr.Length == 1)
		{
			_strChapCodeName = dr[0]["cName"].ToString().Trim();
			_strStar = dr[0]["IsShow"].ToString();
			if (dr[0]["IsShow"].ToString() == "*")
			{
				_strName = _strChapCodeName + "，";
			}
		}
	}

	private bool ValidateChapCode(string strChapCode, string strExt)
	{
		bool bRet = false;
		string criteria = "ChapCode = '" + strChapCode + "'";
		DataRow[] dr = _dtAutoNumB.Select(criteria);
		bRet = dr.Length > 0;
		if (bRet && _strCodePrefix != "")
		{
			bRet = _strCodePrefix == dr[0]["resType"].ToString();
			if (!bRet)
			{
				_strCompareErrState += ((_strCompareErrState.Trim() == "") ? "資源碼錯誤" : "，資源碼錯誤");
			}
		}
		if (_strCodePrefix == "L")
		{
			if (_strChapCode == "00000")
			{
				bRet = true;
				_strChapCode = "0000";
			}
			else
			{
				DataRow[] DR_Ls = _dtAutoNumA.Select("itemCode = '" + strChapCode + "'");
				if (DR_Ls.Length > 0)
				{
					bRet = true;
					_strChapCode = "0000";
				}
			}
		}
		if (_strCodePrefix == "E")
		{
			if (_strChapCode == "00000")
			{
				bRet = true;
				_strChapCode = _strFullCode.Substring(6, 2);
			}
			else
			{
				DataRow[] DR_Ls = _dtAutoNumA.Select("itemCode = '" + strChapCode + "'");
				if (DR_Ls.Length > 0)
				{
					bRet = true;
					_strChapCode = _strFullCode.Substring(6, 2);
				}
			}
		}
		return bRet;
	}

	private bool GetRM(string strChapCode)
	{
		bool bRet = true;
		string criteria = "ChapCode = '" + strChapCode + "' AND Content <>'' AND CodeSection = 'RM'";
		DataRow[] dr = _dtAutoNumB.Select(criteria);
		if (dr.Length > 0)
		{
			_strRM = dr[0]["Content"].ToString().Trim();
			if (_strRM.Contains("單位:"))
			{
				int start = Convert.ToInt32(_strRM.LastIndexOf(":")) + 1;
				_strUnit = _strRM.Substring(start, _strRM.Length - start);
			}
			if (_strRM.Contains("單位："))
			{
				int start = Convert.ToInt32(_strRM.LastIndexOf("：")) + 1;
				_strUnit = _strRM.Substring(start, _strRM.Length - start);
			}
			if (string.IsNullOrEmpty(_strUnit) && _strFullCode.EndsWith("0"))
			{
				bRet = false;
			}
		}
		return bRet;
	}

	private bool GetLEUnit(string strChapCode, string strUnitCode)
	{
		bool bRet = true;
		string strCriteria = "ChapCode = '" + strChapCode + "' And Code='" + strUnitCode + "' And CodeSection='11'";
		try
		{
			DataRow[] dr = _dtAutoNumB.Select(strCriteria);
			if (dr.Length == 1)
			{
				_strUnit = dr[0]["Content"].ToString();
				if (string.IsNullOrEmpty(_strUnit) && _strFullCode.EndsWith("0"))
				{
					bRet = false;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("GetEUnit _dtAutoNumB==null:" + ((_dtAutoNumB == null) ? "Y " : "N ") + ex.Message + "\nstrCriteria=" + strCriteria);
		}
		return bRet;
	}

	private bool GetAltUnit(string strChapCode)
	{
		bool bRet = true;
		string strCriteria = "itemCode = '" + strChapCode + "'";
		try
		{
			DataRow[] dr = _dtAutoNumA.Select(strCriteria);
			if (dr.Length == 1)
			{
				_strUnit = dr[0]["AltUnit"].ToString();
				if (string.IsNullOrEmpty(_strUnit) && _strFullCode.EndsWith("0"))
				{
					bRet = false;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("GetAltUnit _dtAutoNumA==null:" + ((_dtAutoNumA == null) ? "Y " : "N ") + ex.Message + "\nstrCriteria=" + strCriteria);
		}
		return bRet;
	}

	private bool FormatNameAndUnit()
	{
		string strName = "";
		string[] strTemp = _strName.Split("，".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
		int j = strTemp.Length;
		bool bRet = GetAltUnit(_strChapCode);
		if (j > 1)
		{
			int i;
			for (i = 0; i < j - 1; i++)
			{
				strName += strTemp[i].ToString();
				strName += "，";
			}
			if (!string.IsNullOrEmpty(_strUnit))
			{
				strName += strTemp[i].ToString();
			}
			else
			{
				_strUnit = strTemp[i].ToString().Trim();
			}
		}
		_strName = strName.TrimEnd("，".ToCharArray());
		return bRet;
	}

	private bool ValidateCodeByCodeSection(string strChapCode, string strCodeSection)
	{
		bool bRet = true;
		if (_bSkipNextCode)
		{
			_bSkipNextCode = false;
			return bRet;
		}
		string strCriteria = "ChapCode = '" + strChapCode + "' AND CodeSection = '" + strCodeSection + "'";
		DataRow[] dr = _dtAutoNumB.Select(strCriteria, "MinRow");
		int i = dr.Length;
		if (i > 0)
		{
			if (dr[0]["MinRow"].ToString() != dr[i - 1]["MinRow"].ToString())
			{
				if (!string.IsNullOrEmpty(_strSelfRow) && _bGroup)
				{
					string text = strCriteria;
					strCriteria = text + " AND MaxRow >= '" + _strSelfRow + "' AND MinRow <= '" + _strSelfRow + "'";
					dr = _dtAutoNumB.Select(strCriteria);
				}
				_bGroup = true;
			}
			int l = 0;
			try
			{
				l = dr[0]["Code"].ToString().Length;
			}
			catch (Exception)
			{
			}
			if (l > 1 && (string.IsNullOrEmpty(_strCodePrefix) || _strCodePrefix == "M" || strCodeSection != "06"))
			{
				_bSkipNextCode = true;
			}
			string strCode = GetNextCode(l);
			if (strCodeSection == "06" && _strName == "" && strCode == "0")
			{
				_strName = _strChapCodeName + "，";
			}
			strCriteria = strCriteria + " AND Code = '" + strCode + "'";
			DataRow[] dr2 = _dtAutoNumB.Select(strCriteria);
			int j = dr2.Length;
			if (j == 1)
			{
				_strMinRow = dr2[0]["MinRow"].ToString();
				_strMaxRow = dr2[0]["MaxRow"].ToString();
				_strSelfRow = dr2[0]["SelfRow"].ToString();
				_strName += ((dr2[0]["Content"].ToString() != "") ? (dr2[0]["Content"].ToString() + "，") : "");
			}
			else
			{
				bRet = false;
			}
		}
		return bRet;
	}

	private bool ValidateCodeByCodeSection_New(string ChapCode, out string pCName, out string pUnit, string ExtStr, out string DisLocation)
	{
		DisLocation = "";
		pCName = "";
		pUnit = "";
		if (_strPostCodes.Length < 1)
		{
			return false;
		}
		string strCriteria06 = "ChapCode = '" + ChapCode + "' AND CodeSection = '06' AND Code='" + _strPostCodes.Substring(0, 1) + "'";
		DataRow[] dr06 = _dtAutoNumB.Select(strCriteria06, "MinRow");
		if (dr06.Length > 0)
		{
			int MaxRow06 = ArchConvert.Obj2Int(dr06[0]["MaxRow"]);
			int MinRow06 = ArchConvert.Obj2Int(dr06[0]["MinRow"]);
			pCName += ((dr06[0]["Content"].ToString().Trim() == "") ? "" : (dr06[0]["Content"].ToString() + "，"));
			if (_strPostCodes.Length < 2)
			{
				return false;
			}
			string strCriteria7 = "ChapCode = '" + ChapCode + "' AND CodeSection = '07' AND Code='" + _strPostCodes.Substring(1, 1) + "'";
			DataRow[] dr7 = _dtAutoNumB.Select(strCriteria7, "MinRow");
			int MaxRow7 = -1;
			int MinRow7 = -1;
			if (dr7.Length > 0)
			{
				bool IsValid07 = true;
				for (int i = 0; i < dr7.Length; i++)
				{
					if (ArchConvert.Obj2Int(dr7[i]["MinRow"]) == MaxRow06 && ArchConvert.Obj2Int(dr7[i]["MaxRow"]) == MinRow06)
					{
						IsValid07 = true;
						MaxRow7 = ArchConvert.Obj2Int(dr7[i]["MaxRow"]);
						MinRow7 = ArchConvert.Obj2Int(dr7[i]["MinRow"]);
						pCName += ((dr7[i]["Content"].ToString().Trim() == "") ? "" : (dr7[i]["Content"].ToString() + "，"));
						break;
					}
					if (ArchConvert.Obj2Int(dr7[i]["MinRow"]) >= MaxRow06 || ArchConvert.Obj2Int(dr7[i]["MaxRow"]) <= MinRow06)
					{
						IsValid07 = false;
						continue;
					}
					IsValid07 = true;
					MaxRow7 = ArchConvert.Obj2Int(dr7[i]["MaxRow"]);
					MinRow7 = ArchConvert.Obj2Int(dr7[i]["MinRow"]);
					pCName += ((dr7[i]["Content"].ToString().Trim() == "") ? "" : (dr7[i]["Content"].ToString() + "，"));
					break;
				}
				if (!IsValid07)
				{
					if (_strPostCodes.Length < 3)
					{
						return false;
					}
					string strCriteria708 = "ChapCode = '" + ChapCode + "' AND CodeSection = '07' AND Code='" + _strPostCodes.Substring(1, 2) + "'";
					DataRow[] dr708 = _dtAutoNumB.Select(strCriteria708, "MinRow");
					int MaxRow708 = -1;
					int MinRow708 = -1;
					if (dr708.Length > 0)
					{
						bool IsValid708 = true;
						for (int i = 0; i < dr708.Length; i++)
						{
							if (ArchConvert.Obj2Int(dr708[i]["MinRow"]) >= MaxRow06 || ArchConvert.Obj2Int(dr708[i]["MaxRow"]) <= MinRow06)
							{
								IsValid708 = false;
								continue;
							}
							IsValid708 = true;
							MaxRow708 = ArchConvert.Obj2Int(dr708[i]["MaxRow"]);
							MinRow708 = ArchConvert.Obj2Int(dr708[i]["MinRow"]);
							pCName += ((dr708[i]["Content"].ToString().Trim() == "") ? "" : (dr708[i]["Content"].ToString() + "，"));
							break;
						}
						if (!IsValid708)
						{
							return false;
						}
						if (_strPostCodes.Length < 4)
						{
							return false;
						}
						string strCriteria09_78 = "ChapCode = '" + ChapCode + "' AND CodeSection = '09' AND Code='" + _strPostCodes.Substring(3, 1) + "'";
						DataRow[] dr09_78 = _dtAutoNumB.Select(strCriteria09_78, "MinRow");
						int MaxRow09_78 = -1;
						int MinRow09_78 = -1;
						if (dr09_78.Length > 0)
						{
							bool IsValid709 = true;
							for (int i = 0; i < dr09_78.Length; i++)
							{
								if (ArchConvert.Obj2Int(dr09_78[i]["MinRow"]) >= MaxRow708 || ArchConvert.Obj2Int(dr09_78[i]["MaxRow"]) <= MinRow708)
								{
									IsValid709 = false;
									continue;
								}
								IsValid709 = true;
								MaxRow09_78 = ArchConvert.Obj2Int(dr09_78[i]["MaxRow"]);
								MinRow09_78 = ArchConvert.Obj2Int(dr09_78[i]["MinRow"]);
								pCName += ((dr09_78[i]["Content"].ToString().Trim() == "") ? "" : (dr09_78[i]["Content"].ToString() + "，"));
								break;
							}
							if (!IsValid709)
							{
								return false;
							}
							if (_strPostCodes.Length < 5)
							{
								return false;
							}
							string strCriteria709 = "ChapCode = '" + ChapCode + "' AND CodeSection = '10' AND Code='" + _strPostCodes.Substring(4, 1) + "'";
							DataRow[] dr709 = _dtAutoNumB.Select(strCriteria709, "MinRow");
							int MaxRow709 = -1;
							int MinRow709 = -1;
							if (dr709.Length <= 0)
							{
								return false;
							}
							bool IsValid710 = true;
							for (int i = 0; i < dr709.Length; i++)
							{
								if (ArchConvert.Obj2Int(dr709[i]["MinRow"]) >= MaxRow09_78 || ArchConvert.Obj2Int(dr709[i]["MaxRow"]) <= MinRow09_78)
								{
									IsValid710 = false;
									continue;
								}
								IsValid710 = true;
								string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
								DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
								if (drRM.Length > 0)
								{
									bool RM_Found = false;
									for (int z = 0; z < drRM.Length; z++)
									{
										if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
										{
											pCName = pCName + dr709[i]["Content"].ToString() + "";
											pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
											RM_Found = true;
											break;
										}
									}
									if (!RM_Found)
									{
										pUnit = pUnit + dr709[i]["Content"].ToString() + "";
									}
								}
								else
								{
									pUnit = pUnit + dr709[i]["Content"].ToString() + "";
								}
								break;
							}
							if (!IsValid710)
							{
								return false;
							}
						}
						else
						{
							if (_strPostCodes.Length < 5)
							{
								return false;
							}
							string strCriteria910 = "ChapCode = '" + ChapCode + "' AND CodeSection = '09' AND Code='" + _strPostCodes.Substring(3, 2) + "'";
							DataRow[] dr910 = _dtAutoNumB.Select(strCriteria910, "MinRow");
							if (dr910.Length <= 0)
							{
								return false;
							}
							string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
							DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
							if (drRM.Length <= 0)
							{
								return false;
							}
							bool RM_Found = false;
							for (int z = 0; z < drRM.Length; z++)
							{
								if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
								{
									pCName = pCName + dr910[0]["Content"].ToString() + "";
									pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
									RM_Found = true;
									break;
								}
							}
							if (!RM_Found)
							{
								pUnit = pUnit + dr910[0]["Content"].ToString() + "";
							}
						}
						return true;
					}
					return false;
				}
				if (_strPostCodes.Length < 3)
				{
					return false;
				}
				string strCriteria911 = "ChapCode = '" + ChapCode + "' AND CodeSection = '08' AND Code='" + _strPostCodes.Substring(2, 1) + "'";
				DataRow[] dr911 = _dtAutoNumB.Select(strCriteria911, "MinRow");
				int MaxRow710 = -1;
				int MinRow710 = -1;
				if (dr911.Length > 0)
				{
					bool IsValid711 = true;
					for (int i = 0; i < dr911.Length; i++)
					{
						if (ArchConvert.Obj2Int(dr911[i]["MinRow"]) >= MaxRow7 || ArchConvert.Obj2Int(dr911[i]["MaxRow"]) <= MinRow7)
						{
							IsValid711 = false;
							continue;
						}
						if (ArchConvert.Obj2Int(dr911[i]["MinRow"]) >= MaxRow06 || ArchConvert.Obj2Int(dr911[i]["MaxRow"]) <= MinRow06)
						{
							IsValid711 = false;
							continue;
						}
						IsValid711 = true;
						MaxRow710 = ArchConvert.Obj2Int(dr911[i]["MaxRow"]);
						MinRow710 = ArchConvert.Obj2Int(dr911[i]["MinRow"]);
						pCName += ((dr911[i]["Content"].ToString().Trim() == "") ? "" : (dr911[i]["Content"].ToString() + "，"));
						break;
					}
					if (!IsValid711)
					{
						if (_strPostCodes.Length < 4)
						{
							return false;
						}
						string strCriteria912 = "ChapCode = '" + ChapCode + "' AND CodeSection = '08' AND Code='" + _strPostCodes.Substring(2, 2) + "'";
						DataRow[] dr912 = _dtAutoNumB.Select(strCriteria912, "MinRow");
						int MaxRow809 = -1;
						int MinRow809 = -1;
						if (dr912.Length > 0)
						{
							bool IsValid809 = true;
							for (int i = 0; i < dr912.Length; i++)
							{
								if (ArchConvert.Obj2Int(dr912[i]["MinRow"]) >= MaxRow7 || ArchConvert.Obj2Int(dr912[i]["MaxRow"]) <= MinRow7)
								{
									IsValid809 = false;
									continue;
								}
								if (ArchConvert.Obj2Int(dr912[i]["MinRow"]) >= MaxRow06 || ArchConvert.Obj2Int(dr912[i]["MaxRow"]) <= MinRow06)
								{
									IsValid809 = false;
									continue;
								}
								IsValid809 = true;
								MaxRow809 = ArchConvert.Obj2Int(dr912[i]["MaxRow"]);
								MinRow809 = ArchConvert.Obj2Int(dr912[i]["MinRow"]);
								pCName += ((dr912[i]["Content"].ToString().Trim() == "") ? "" : (dr912[i]["Content"].ToString() + "，"));
								break;
							}
							if (!IsValid809)
							{
								return false;
							}
							if (_strPostCodes.Length < 5)
							{
								return false;
							}
							string strCriteria709 = "ChapCode = '" + ChapCode + "' AND CodeSection = '10' AND Code='" + _strPostCodes.Substring(4, 1) + "'";
							DataRow[] dr709 = _dtAutoNumB.Select(strCriteria709, "MinRow");
							if (dr709.Length > 0)
							{
								bool IsValid710 = true;
								for (int i = 0; i < dr709.Length; i++)
								{
									if (ArchConvert.Obj2Int(dr709[i]["MinRow"]) >= MaxRow809 || ArchConvert.Obj2Int(dr709[i]["MaxRow"]) <= MinRow809)
									{
										IsValid710 = false;
										continue;
									}
									IsValid710 = true;
									string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Len(Trim(Content)) > 0";
									DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
									if (drRM.Length > 0)
									{
										bool RM_Found = false;
										for (int z = 0; z < drRM.Length; z++)
										{
											if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
											{
												pCName = pCName + dr709[i]["Content"].ToString() + "";
												pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
												RM_Found = true;
												break;
											}
										}
										if (!RM_Found)
										{
											pUnit = pUnit + dr709[i]["Content"].ToString() + "";
										}
									}
									else
									{
										pUnit = pUnit + dr709[i]["Content"].ToString() + "";
									}
									break;
								}
								if (!IsValid710)
								{
									return false;
								}
								return true;
							}
							return false;
						}
						return false;
					}
					if (_strPostCodes.Length < 4)
					{
						return false;
					}
					string strCriteria913 = "ChapCode = '" + ChapCode + "' AND CodeSection = '09' AND Code='" + _strPostCodes.Substring(3, 1) + "'";
					DataRow[] dr913 = _dtAutoNumB.Select(strCriteria913, "MinRow");
					int MaxRow810 = -1;
					int MinRow810 = -1;
					if (dr913.Length > 0)
					{
						bool IsValid709 = true;
						for (int i = 0; i < dr913.Length; i++)
						{
							if (ArchConvert.Obj2Int(dr913[i]["MinRow"]) >= MaxRow710 || ArchConvert.Obj2Int(dr913[i]["MaxRow"]) <= MinRow710)
							{
								IsValid709 = false;
								continue;
							}
							if (ArchConvert.Obj2Int(dr913[i]["MinRow"]) >= MaxRow7 || ArchConvert.Obj2Int(dr913[i]["MaxRow"]) <= MinRow7)
							{
								IsValid709 = false;
								continue;
							}
							IsValid709 = true;
							MaxRow810 = ArchConvert.Obj2Int(dr913[i]["MaxRow"]);
							MinRow810 = ArchConvert.Obj2Int(dr913[i]["MinRow"]);
							pCName += ((dr913[i]["Content"].ToString().Trim() == "") ? "" : (dr913[i]["Content"].ToString() + "，"));
							break;
						}
						if (!IsValid709)
						{
							return false;
						}
						if (_strPostCodes.Length < 5)
						{
							return false;
						}
						string strCriteria709 = "ChapCode = '" + ChapCode + "' AND CodeSection = '10' AND Code='" + _strPostCodes.Substring(4, 1) + "'";
						DataRow[] dr709 = _dtAutoNumB.Select(strCriteria709, "MinRow");
						int MaxRow709 = -1;
						int MinRow709 = -1;
						if (dr709.Length <= 0)
						{
							return false;
						}
						bool IsValid710 = true;
						for (int i = 0; i < dr709.Length; i++)
						{
							if (ArchConvert.Obj2Int(dr709[i]["MinRow"]) >= MaxRow810 || ArchConvert.Obj2Int(dr709[i]["MaxRow"]) <= MinRow810)
							{
								IsValid710 = false;
								continue;
							}
							if (ExtStr.Trim() != "12")
							{
								IsValid710 = true;
								string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
								DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
								if (drRM.Length > 0)
								{
									bool RM_Found = false;
									for (int z = 0; z < drRM.Length; z++)
									{
										if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
										{
											pCName = pCName + dr709[i]["Content"].ToString() + "";
											pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
											RM_Found = true;
											break;
										}
									}
									if (!RM_Found)
									{
										pUnit = pUnit + dr709[i]["Content"].ToString() + "";
									}
								}
								else
								{
									pUnit = pUnit + dr709[i]["Content"].ToString() + "";
								}
							}
							else
							{
								IsValid710 = true;
								MaxRow709 = ArchConvert.Obj2Int(dr709[i]["MaxRow"]);
								MinRow709 = ArchConvert.Obj2Int(dr709[i]["MinRow"]);
								pCName += ((dr709[i]["Content"].ToString().Trim() == "") ? "" : (dr709[i]["Content"].ToString() + "，"));
							}
							break;
						}
						if (!IsValid710)
						{
							return false;
						}
						if (MaxRow7 < MinRow06 || MinRow7 > MaxRow06)
						{
							DisLocation += "規則表,06-07錯位，";
						}
						if (MaxRow710 < MinRow06 || MinRow710 > MaxRow06)
						{
							DisLocation += "規則表,06-08錯位，";
						}
						if (MaxRow810 < MinRow06 || MinRow810 > MaxRow06)
						{
							DisLocation += "規則表,06-09錯位，";
						}
						if (MaxRow710 < MinRow7 || MinRow710 > MaxRow7)
						{
							DisLocation += "規則表,07-08錯位，";
						}
						if (MaxRow810 < MinRow7 || MinRow810 > MaxRow7)
						{
							DisLocation += "規則表,07-09錯位，";
						}
						if (MaxRow810 < MinRow710 || MinRow810 > MaxRow710)
						{
							DisLocation += "規則表,08-09錯位，";
						}
						if (ExtStr.Trim() == "12")
						{
							if (_strPostCodes.Length < 6)
							{
								return false;
							}
							string strCriteria914 = "ChapCode = '" + ChapCode + "' AND CodeSection = '11' AND Code='" + _strPostCodes.Substring(5, 1) + "'";
							DataRow[] dr914 = _dtAutoNumB.Select(strCriteria914, "MinRow");
							int MaxRow811 = -1;
							int MinRow811 = -1;
							if (dr914.Length > 0)
							{
								bool IsValid810 = true;
								for (int j = 0; j < dr914.Length; j++)
								{
									if (ArchConvert.Obj2Int(dr914[j]["MinRow"]) >= MaxRow709 || ArchConvert.Obj2Int(dr914[j]["MaxRow"]) <= MinRow709)
									{
										IsValid810 = false;
										continue;
									}
									if (ArchConvert.Obj2Int(dr914[j]["MinRow"]) >= MaxRow810 || ArchConvert.Obj2Int(dr914[j]["MaxRow"]) <= MinRow810)
									{
										IsValid810 = false;
										continue;
									}
									IsValid810 = true;
									MaxRow811 = ArchConvert.Obj2Int(dr914[j]["MaxRow"]);
									MinRow811 = ArchConvert.Obj2Int(dr914[j]["MinRow"]);
									pCName += ((dr914[j]["Content"].ToString().Trim() == "") ? "" : (dr914[j]["Content"].ToString() + "，"));
									break;
								}
								if (!IsValid810)
								{
									return false;
								}
								if (_strPostCodes.Length < 7)
								{
									return false;
								}
								string strCriteria915 = "ChapCode = '" + ChapCode + "' AND CodeSection = '12' AND Code='" + _strPostCodes.Substring(6, 1) + "'";
								DataRow[] dr915 = _dtAutoNumB.Select(strCriteria915, "MinRow");
								int MaxRow812 = -1;
								int MinRow812 = -1;
								if (dr915.Length > 0)
								{
									bool IsValid811 = true;
									for (int j = 0; j < dr915.Length; j++)
									{
										if (ArchConvert.Obj2Int(dr915[j]["MinRow"]) >= MaxRow811 || ArchConvert.Obj2Int(dr915[j]["MaxRow"]) <= MinRow811)
										{
											IsValid811 = false;
											continue;
										}
										IsValid811 = true;
										string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
										DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
										if (drRM.Length > 0)
										{
											bool RM_Found = false;
											for (int z = 0; z < drRM.Length; z++)
											{
												if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
												{
													pCName = pCName + dr915[j]["Content"].ToString() + "";
													pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
													RM_Found = true;
													break;
												}
											}
											if (!RM_Found)
											{
												pUnit = pUnit + dr915[j]["Content"].ToString() + "";
											}
										}
										else
										{
											pUnit = pUnit + dr915[j]["Content"].ToString() + "";
										}
										break;
									}
									if (!IsValid811)
									{
										return false;
									}
								}
								else
								{
									if (_strPostCodes.Length < 7)
									{
										return false;
									}
									string strCriteria1112 = "ChapCode = '" + ChapCode + "' AND CodeSection = '11' AND Code='" + _strPostCodes.Substring(5, 2) + "'";
									DataRow[] dr1112 = _dtAutoNumB.Select(strCriteria1112, "MinRow");
									if (dr1112.Length <= 0)
									{
										return false;
									}
									string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
									DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
									if (drRM.Length <= 0)
									{
										return false;
									}
									bool RM_Found = false;
									for (int z = 0; z < drRM.Length; z++)
									{
										if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
										{
											pCName = pCName + dr1112[0]["Content"].ToString() + "";
											pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
											RM_Found = true;
											break;
										}
									}
									if (!RM_Found)
									{
										pUnit = pUnit + dr1112[0]["Content"].ToString() + "";
									}
								}
							}
							else
							{
								if (_strPostCodes.Length < 7)
								{
									return false;
								}
								string strCriteria1112 = "ChapCode = '" + ChapCode + "' AND CodeSection = '11' AND Code='" + _strPostCodes.Substring(5, 2) + "'";
								DataRow[] dr1112 = _dtAutoNumB.Select(strCriteria1112, "MinRow");
								if (dr1112.Length <= 0)
								{
									return false;
								}
								string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
								DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
								if (drRM.Length <= 0)
								{
									return false;
								}
								bool RM_Found = false;
								for (int z = 0; z < drRM.Length; z++)
								{
									if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
									{
										pCName = pCName + dr1112[0]["Content"].ToString() + "";
										pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
										RM_Found = true;
										break;
									}
								}
								if (!RM_Found)
								{
									pUnit = pUnit + dr1112[0]["Content"].ToString() + "";
								}
							}
						}
					}
					else if (ExtStr.Trim() != "12")
					{
						if (_strPostCodes.Length < 5)
						{
							return false;
						}
						string strCriteria910 = "ChapCode = '" + ChapCode + "' AND CodeSection = '09' AND Code='" + _strPostCodes.Substring(3, 2) + "'";
						DataRow[] dr910 = _dtAutoNumB.Select(strCriteria910, "MinRow");
						if (dr910.Length <= 0)
						{
							return false;
						}
						string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
						DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
						if (drRM.Length <= 0)
						{
							return false;
						}
						bool RM_Found = false;
						for (int z = 0; z < drRM.Length; z++)
						{
							if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
							{
								pCName = pCName + dr910[0]["Content"].ToString() + "";
								pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
								RM_Found = true;
								break;
							}
						}
						if (!RM_Found)
						{
							pUnit = pUnit + dr910[0]["Content"].ToString() + "";
						}
					}
					else
					{
						if (_strPostCodes.Length < 5)
						{
							return false;
						}
						string strCriteria910 = "ChapCode = '" + ChapCode + "' AND CodeSection = '09' AND Code='" + _strPostCodes.Substring(3, 2) + "'";
						DataRow[] dr910 = _dtAutoNumB.Select(strCriteria910, "MinRow");
						int MaxRow910 = -1;
						int MinRow910 = -1;
						if (dr910.Length <= 0)
						{
							return false;
						}
						bool IsValid910 = true;
						for (int i = 0; i < dr910.Length; i++)
						{
							if (ArchConvert.Obj2Int(dr910[i]["MinRow"]) >= MaxRow710 || ArchConvert.Obj2Int(dr910[i]["MaxRow"]) <= MinRow710)
							{
								IsValid910 = false;
								continue;
							}
							if (ArchConvert.Obj2Int(dr910[i]["MinRow"]) >= MaxRow7 || ArchConvert.Obj2Int(dr910[i]["MaxRow"]) <= MinRow7)
							{
								IsValid910 = false;
								continue;
							}
							IsValid910 = true;
							MaxRow910 = ArchConvert.Obj2Int(dr910[i]["MaxRow"]);
							MaxRow910 = ArchConvert.Obj2Int(dr910[i]["MinRow"]);
							pCName += ((dr910[i]["Content"].ToString().Trim() == "") ? "" : (dr910[i]["Content"].ToString() + "，"));
							break;
						}
						if (!IsValid910)
						{
							return false;
						}
						if (_strPostCodes.Length < 6)
						{
							return false;
						}
						string strCriteria914 = "ChapCode = '" + ChapCode + "' AND CodeSection = '11' AND Code='" + _strPostCodes.Substring(5, 1) + "'";
						DataRow[] dr914 = _dtAutoNumB.Select(strCriteria914, "MinRow");
						int MaxRow811 = -1;
						int MinRow811 = -1;
						if (dr914.Length > 0)
						{
							bool IsValid810 = true;
							for (int j = 0; j < dr914.Length; j++)
							{
								if (ArchConvert.Obj2Int(dr914[j]["MinRow"]) > MaxRow910 || ArchConvert.Obj2Int(dr914[j]["MaxRow"]) <= MinRow910)
								{
									IsValid810 = false;
									continue;
								}
								if (ArchConvert.Obj2Int(dr914[j]["MinRow"]) >= MaxRow710 || ArchConvert.Obj2Int(dr914[j]["MaxRow"]) <= MinRow710)
								{
									IsValid810 = false;
									continue;
								}
								IsValid810 = true;
								MaxRow811 = ArchConvert.Obj2Int(dr914[j]["MaxRow"]);
								MinRow811 = ArchConvert.Obj2Int(dr914[j]["MinRow"]);
								pCName += ((dr914[j]["Content"].ToString().Trim() == "") ? "" : (dr914[j]["Content"].ToString() + "，"));
								break;
							}
							if (!IsValid810)
							{
								return false;
							}
							if (_strPostCodes.Length < 7)
							{
								return false;
							}
							string strCriteria915 = "ChapCode = '" + ChapCode + "' AND CodeSection = '12' AND Code='" + _strPostCodes.Substring(6, 1) + "'";
							DataRow[] dr915 = _dtAutoNumB.Select(strCriteria915, "MinRow");
							int MaxRow812 = -1;
							int MinRow812 = -1;
							if (dr915.Length > 0)
							{
								bool IsValid811 = true;
								for (int j = 0; j < dr915.Length; j++)
								{
									if (ArchConvert.Obj2Int(dr915[j]["MinRow"]) >= MaxRow811 || ArchConvert.Obj2Int(dr915[j]["MaxRow"]) <= MinRow811)
									{
										IsValid811 = false;
										continue;
									}
									IsValid811 = true;
									string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
									DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
									if (drRM.Length > 0)
									{
										bool RM_Found = false;
										for (int z = 0; z < drRM.Length; z++)
										{
											if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
											{
												pCName = pCName + dr915[j]["Content"].ToString() + "";
												pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
												RM_Found = true;
												break;
											}
										}
										if (!RM_Found)
										{
											pUnit = pUnit + dr915[j]["Content"].ToString() + "";
										}
									}
									else
									{
										pUnit = pUnit + dr915[j]["Content"].ToString() + "";
									}
									break;
								}
								if (!IsValid811)
								{
									return false;
								}
							}
							else
							{
								if (_strPostCodes.Length < 7)
								{
									return false;
								}
								string strCriteria1112 = "ChapCode = '" + ChapCode + "' AND CodeSection = '11' AND Code='" + _strPostCodes.Substring(5, 2) + "'";
								DataRow[] dr1112 = _dtAutoNumB.Select(strCriteria1112, "MinRow");
								if (dr1112.Length <= 0)
								{
									return false;
								}
								string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
								DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
								if (drRM.Length <= 0)
								{
									return false;
								}
								bool RM_Found = false;
								for (int z = 0; z < drRM.Length; z++)
								{
									if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
									{
										pCName = pCName + dr1112[0]["Content"].ToString() + "";
										pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
										RM_Found = true;
										break;
									}
								}
								if (!RM_Found)
								{
									pUnit = pUnit + dr1112[0]["Content"].ToString() + "";
								}
							}
						}
						else
						{
							if (_strPostCodes.Length < 7)
							{
								return false;
							}
							string strCriteria1112 = "ChapCode = '" + ChapCode + "' AND CodeSection = '11' AND Code='" + _strPostCodes.Substring(5, 2) + "'";
							DataRow[] dr1112 = _dtAutoNumB.Select(strCriteria1112, "MinRow");
							if (dr1112.Length <= 0)
							{
								return false;
							}
							string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
							DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
							if (drRM.Length <= 0)
							{
								return false;
							}
							bool RM_Found = false;
							for (int z = 0; z < drRM.Length; z++)
							{
								if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
								{
									pCName = pCName + dr1112[0]["Content"].ToString() + "";
									pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
									RM_Found = true;
									break;
								}
							}
							if (!RM_Found)
							{
								pUnit = pUnit + dr1112[0]["Content"].ToString() + "";
							}
						}
					}
				}
				else if (ExtStr.Trim() != "12")
				{
					if (_strPostCodes.Length < 4)
					{
						return false;
					}
					string strCriteria912 = "ChapCode = '" + ChapCode + "' AND CodeSection = '08' AND Code='" + _strPostCodes.Substring(2, 2) + "'";
					DataRow[] dr912 = _dtAutoNumB.Select(strCriteria912, "MinRow");
					int MaxRow809 = -1;
					int MinRow809 = -1;
					if (dr912.Length <= 0)
					{
						return false;
					}
					bool IsValid809 = true;
					for (int i = 0; i < dr912.Length; i++)
					{
						if (ArchConvert.Obj2Int(dr912[i]["MinRow"]) >= MaxRow7 || ArchConvert.Obj2Int(dr912[i]["MaxRow"]) <= MinRow7)
						{
							IsValid809 = false;
							continue;
						}
						if (ArchConvert.Obj2Int(dr912[i]["MinRow"]) >= MaxRow06 || ArchConvert.Obj2Int(dr912[i]["MaxRow"]) <= MinRow06)
						{
							IsValid809 = false;
							continue;
						}
						IsValid809 = true;
						MaxRow809 = ArchConvert.Obj2Int(dr912[i]["MaxRow"]);
						MinRow809 = ArchConvert.Obj2Int(dr912[i]["MinRow"]);
						pCName += ((dr912[i]["Content"].ToString().Trim() == "") ? "" : (dr912[i]["Content"].ToString() + "，"));
						break;
					}
					if (!IsValid809)
					{
						return false;
					}
					if (_strPostCodes.Length < 5)
					{
						return false;
					}
					string strCriteria709 = "ChapCode = '" + ChapCode + "' AND CodeSection = '10' AND Code='" + _strPostCodes.Substring(4, 1) + "'";
					DataRow[] dr709 = _dtAutoNumB.Select(strCriteria709, "MinRow");
					if (dr709.Length <= 0)
					{
						return false;
					}
					bool IsValid710 = true;
					for (int i = 0; i < dr709.Length; i++)
					{
						if (ArchConvert.Obj2Int(dr709[i]["MinRow"]) >= MaxRow809 || ArchConvert.Obj2Int(dr709[i]["MaxRow"]) <= MinRow809)
						{
							IsValid710 = false;
							continue;
						}
						IsValid710 = true;
						string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Len(Trim(Content)) > 0";
						DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
						if (drRM.Length > 0)
						{
							bool RM_Found = false;
							for (int z = 0; z < drRM.Length; z++)
							{
								if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
								{
									pCName = pCName + dr709[i]["Content"].ToString() + "";
									pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
									RM_Found = true;
									break;
								}
							}
							if (!RM_Found)
							{
								pUnit = pUnit + dr709[i]["Content"].ToString() + "";
							}
						}
						else
						{
							pUnit = pUnit + dr709[i]["Content"].ToString() + "";
						}
						break;
					}
					if (!IsValid710)
					{
						return false;
					}
				}
				else
				{
					if (_strPostCodes.Length < 4)
					{
						return false;
					}
					string strCriteria912 = "ChapCode = '" + ChapCode + "' AND CodeSection = '08' AND Code='" + _strPostCodes.Substring(2, 2) + "'";
					DataRow[] dr912 = _dtAutoNumB.Select(strCriteria912, "MinRow");
					int MaxRow809 = -1;
					int MinRow809 = -1;
					if (dr912.Length <= 0)
					{
						return false;
					}
					bool IsValid809 = true;
					for (int i = 0; i < dr912.Length; i++)
					{
						if (ArchConvert.Obj2Int(dr912[i]["MinRow"]) >= MaxRow7 || ArchConvert.Obj2Int(dr912[i]["MaxRow"]) <= MinRow7)
						{
							IsValid809 = false;
							continue;
						}
						if (ArchConvert.Obj2Int(dr912[i]["MinRow"]) >= MaxRow06 || ArchConvert.Obj2Int(dr912[i]["MaxRow"]) <= MinRow06)
						{
							IsValid809 = false;
							continue;
						}
						IsValid809 = true;
						MaxRow809 = ArchConvert.Obj2Int(dr912[i]["MaxRow"]);
						MinRow809 = ArchConvert.Obj2Int(dr912[i]["MinRow"]);
						pCName += ((dr912[i]["Content"].ToString().Trim() == "") ? "" : (dr912[i]["Content"].ToString() + "，"));
						break;
					}
					if (!IsValid809)
					{
						return false;
					}
					if (_strPostCodes.Length < 5)
					{
						return false;
					}
					string strCriteria709 = "ChapCode = '" + ChapCode + "' AND CodeSection = '10' AND Code='" + _strPostCodes.Substring(4, 1) + "'";
					DataRow[] dr709 = _dtAutoNumB.Select(strCriteria709, "MinRow");
					int MaxRow709 = -1;
					int MinRow709 = -1;
					if (dr709.Length > 0)
					{
						bool IsValid710 = true;
						for (int i = 0; i < dr709.Length; i++)
						{
							if (ArchConvert.Obj2Int(dr709[i]["MinRow"]) >= MaxRow809 || ArchConvert.Obj2Int(dr709[i]["MaxRow"]) <= MinRow809)
							{
								IsValid710 = false;
								continue;
							}
							IsValid710 = true;
							string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Len(Trim(Content)) > 0";
							DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
							if (drRM.Length > 0)
							{
								bool RM_Found = false;
								for (int z = 0; z < drRM.Length; z++)
								{
									if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
									{
										pCName = pCName + dr709[i]["Content"].ToString() + "";
										pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
										RM_Found = true;
										break;
									}
								}
								if (!RM_Found)
								{
									pUnit = pUnit + dr709[i]["Content"].ToString() + "";
								}
							}
							else
							{
								pUnit = pUnit + dr709[i]["Content"].ToString() + "";
							}
							break;
						}
						if (!IsValid710)
						{
							return false;
						}
					}
				}
			}
			else if (ExtStr.Trim() != "12")
			{
				if (_strPostCodes.Length < 3)
				{
					return false;
				}
				string strCriteria708 = "ChapCode = '" + ChapCode + "' AND CodeSection = '07' AND Code='" + _strPostCodes.Substring(1, 2) + "'";
				DataRow[] dr708 = _dtAutoNumB.Select(strCriteria708, "MinRow");
				int MaxRow708 = -1;
				int MinRow708 = -1;
				if (dr708.Length <= 0)
				{
					return false;
				}
				bool IsValid708 = true;
				for (int i = 0; i < dr708.Length; i++)
				{
					if (ArchConvert.Obj2Int(dr708[i]["MinRow"]) >= MaxRow06 || ArchConvert.Obj2Int(dr708[i]["MaxRow"]) <= MinRow06)
					{
						IsValid708 = false;
						continue;
					}
					IsValid708 = true;
					MaxRow708 = ArchConvert.Obj2Int(dr708[i]["MaxRow"]);
					MinRow708 = ArchConvert.Obj2Int(dr708[i]["MinRow"]);
					pCName += ((dr708[i]["Content"].ToString().Trim() == "") ? "" : (dr708[i]["Content"].ToString() + "，"));
					break;
				}
				if (!IsValid708)
				{
					return false;
				}
				if (_strPostCodes.Length < 4)
				{
					return false;
				}
				string strCriteria913 = "ChapCode = '" + ChapCode + "' AND CodeSection = '09' AND Code='" + _strPostCodes.Substring(3, 1) + "'";
				DataRow[] dr913 = _dtAutoNumB.Select(strCriteria913, "MinRow");
				int MaxRow810 = -1;
				int MinRow810 = -1;
				if (dr913.Length > 0)
				{
					bool IsValid709 = true;
					for (int i = 0; i < dr913.Length; i++)
					{
						if (ArchConvert.Obj2Int(dr913[i]["MinRow"]) >= MaxRow708 || ArchConvert.Obj2Int(dr913[i]["MaxRow"]) <= MinRow708)
						{
							IsValid709 = false;
							continue;
						}
						IsValid709 = true;
						MaxRow810 = ArchConvert.Obj2Int(dr913[i]["MaxRow"]);
						MinRow810 = ArchConvert.Obj2Int(dr913[i]["MinRow"]);
						pCName += ((dr913[i]["Content"].ToString().Trim() == "") ? "" : (dr913[i]["Content"].ToString() + "，"));
						break;
					}
					if (!IsValid709)
					{
						return false;
					}
					if (_strPostCodes.Length < 5)
					{
						return false;
					}
					string strCriteria709 = "ChapCode = '" + ChapCode + "' AND CodeSection = '10' AND Code='" + _strPostCodes.Substring(4, 1) + "'";
					DataRow[] dr709 = _dtAutoNumB.Select(strCriteria709, "MinRow");
					int MaxRow709 = -1;
					int MinRow709 = -1;
					if (dr709.Length <= 0)
					{
						return false;
					}
					bool IsValid710 = true;
					for (int i = 0; i < dr709.Length; i++)
					{
						if (ArchConvert.Obj2Int(dr709[i]["MinRow"]) >= MaxRow810 || ArchConvert.Obj2Int(dr709[i]["MaxRow"]) <= MinRow810)
						{
							IsValid710 = false;
							continue;
						}
						IsValid710 = true;
						string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
						DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
						if (drRM.Length > 0)
						{
							bool RM_Found = false;
							for (int z = 0; z < drRM.Length; z++)
							{
								if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
								{
									pCName = pCName + dr709[i]["Content"].ToString() + "";
									pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
									RM_Found = true;
									break;
								}
							}
							if (!RM_Found)
							{
								pUnit = pUnit + dr709[i]["Content"].ToString() + "";
							}
						}
						else
						{
							pUnit = pUnit + dr709[i]["Content"].ToString() + "";
						}
						break;
					}
					if (!IsValid710)
					{
						return false;
					}
				}
				else
				{
					if (_strPostCodes.Length < 5)
					{
						return false;
					}
					string strCriteria910 = "ChapCode = '" + ChapCode + "' AND CodeSection = '09' AND Code='" + _strPostCodes.Substring(3, 2) + "'";
					DataRow[] dr910 = _dtAutoNumB.Select(strCriteria910, "MinRow");
					if (dr910.Length <= 0)
					{
						return false;
					}
					string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
					DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
					if (drRM.Length <= 0)
					{
						return false;
					}
					bool RM_Found = false;
					for (int z = 0; z < drRM.Length; z++)
					{
						if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
						{
							pCName = pCName + dr910[0]["Content"].ToString() + "";
							pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
							RM_Found = true;
							break;
						}
					}
					if (!RM_Found)
					{
						pUnit = pUnit + dr910[0]["Content"].ToString() + "";
					}
				}
			}
			else
			{
				if (_strPostCodes.Length < 3)
				{
					return false;
				}
				string strCriteria708 = "ChapCode = '" + ChapCode + "' AND CodeSection = '07' AND Code='" + _strPostCodes.Substring(1, 2) + "'";
				DataRow[] dr708 = _dtAutoNumB.Select(strCriteria708, "MinRow");
				int MaxRow708 = -1;
				int MinRow708 = -1;
				if (dr708.Length <= 0)
				{
					return false;
				}
				bool IsValid708 = true;
				for (int i = 0; i < dr708.Length; i++)
				{
					if (ArchConvert.Obj2Int(dr708[i]["MinRow"]) >= MaxRow06 || ArchConvert.Obj2Int(dr708[i]["MaxRow"]) <= MinRow06)
					{
						IsValid708 = false;
						continue;
					}
					IsValid708 = true;
					MaxRow708 = ArchConvert.Obj2Int(dr708[i]["MaxRow"]);
					MinRow708 = ArchConvert.Obj2Int(dr708[i]["MinRow"]);
					pCName += ((dr708[i]["Content"].ToString().Trim() == "") ? "" : (dr708[i]["Content"].ToString() + "，"));
					break;
				}
				if (!IsValid708)
				{
					return false;
				}
				if (_strPostCodes.Length < 4)
				{
					return false;
				}
				string strCriteria913 = "ChapCode = '" + ChapCode + "' AND CodeSection = '09' AND Code='" + _strPostCodes.Substring(3, 1) + "'";
				DataRow[] dr913 = _dtAutoNumB.Select(strCriteria913, "MinRow");
				int MaxRow810 = -1;
				int MinRow810 = -1;
				if (dr913.Length > 0)
				{
					bool IsValid709 = true;
					for (int i = 0; i < dr913.Length; i++)
					{
						if (ArchConvert.Obj2Int(dr913[i]["MinRow"]) >= MaxRow708 || ArchConvert.Obj2Int(dr913[i]["MaxRow"]) <= MinRow708)
						{
							IsValid709 = false;
							continue;
						}
						IsValid709 = true;
						MaxRow810 = ArchConvert.Obj2Int(dr913[i]["MaxRow"]);
						MinRow810 = ArchConvert.Obj2Int(dr913[i]["MinRow"]);
						pCName += ((dr913[i]["Content"].ToString().Trim() == "") ? "" : (dr913[i]["Content"].ToString() + "，"));
						break;
					}
					if (!IsValid709)
					{
						return false;
					}
					if (_strPostCodes.Length < 5)
					{
						return false;
					}
					string strCriteria709 = "ChapCode = '" + ChapCode + "' AND CodeSection = '10' AND Code='" + _strPostCodes.Substring(4, 1) + "'";
					DataRow[] dr709 = _dtAutoNumB.Select(strCriteria709, "MinRow");
					int MaxRow709 = -1;
					int MinRow709 = -1;
					if (dr709.Length > 0)
					{
						bool IsValid710 = true;
						for (int i = 0; i < dr709.Length; i++)
						{
							if (ArchConvert.Obj2Int(dr709[i]["MinRow"]) >= MaxRow810 || ArchConvert.Obj2Int(dr709[i]["MaxRow"]) <= MinRow810)
							{
								IsValid710 = false;
								continue;
							}
							IsValid710 = true;
							MaxRow709 = ArchConvert.Obj2Int(dr709[i]["MaxRow"]);
							MinRow709 = ArchConvert.Obj2Int(dr709[i]["MinRow"]);
							pCName += ((dr709[i]["Content"].ToString().Trim() == "") ? "" : (dr709[i]["Content"].ToString() + "，"));
							break;
						}
						if (!IsValid710)
						{
							return false;
						}
						if (_strPostCodes.Length < 6)
						{
							return false;
						}
						string strCriteria914 = "ChapCode = '" + ChapCode + "' AND CodeSection = '11' AND Code='" + _strPostCodes.Substring(5, 1) + "'";
						DataRow[] dr914 = _dtAutoNumB.Select(strCriteria914, "MinRow");
						int MaxRow811 = -1;
						int MinRow811 = -1;
						if (dr914.Length > 0)
						{
							bool IsValid810 = true;
							for (int i = 0; i < dr914.Length; i++)
							{
								if (ArchConvert.Obj2Int(dr914[i]["MinRow"]) >= MaxRow709 || ArchConvert.Obj2Int(dr914[i]["MaxRow"]) <= MinRow709)
								{
									IsValid810 = false;
									continue;
								}
								IsValid810 = true;
								MaxRow811 = ArchConvert.Obj2Int(dr914[i]["MaxRow"]);
								MinRow811 = ArchConvert.Obj2Int(dr914[i]["MinRow"]);
								pCName += ((dr914[i]["Content"].ToString().Trim() == "") ? "" : (dr914[i]["Content"].ToString() + "，"));
								break;
							}
							if (!IsValid810)
							{
								return false;
							}
							if (_strPostCodes.Length < 7)
							{
								return false;
							}
							string strCriteria915 = "ChapCode = '" + ChapCode + "' AND CodeSection = '12' AND Code='" + _strPostCodes.Substring(6, 1) + "'";
							DataRow[] dr915 = _dtAutoNumB.Select(strCriteria915, "MinRow");
							int MaxRow812 = -1;
							int MinRow812 = -1;
							if (dr915.Length <= 0)
							{
								return false;
							}
							bool IsValid811 = true;
							for (int i = 0; i < dr915.Length; i++)
							{
								if (ArchConvert.Obj2Int(dr915[i]["MinRow"]) >= MaxRow811 || ArchConvert.Obj2Int(dr915[i]["MaxRow"]) <= MinRow811)
								{
									IsValid811 = false;
									continue;
								}
								IsValid811 = true;
								string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
								DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
								if (drRM.Length > 0)
								{
									bool RM_Found = false;
									for (int z = 0; z < drRM.Length; z++)
									{
										if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
										{
											pCName = pCName + dr915[i]["Content"].ToString() + "";
											pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
											RM_Found = true;
											break;
										}
									}
									if (!RM_Found)
									{
										pUnit = pUnit + dr915[i]["Content"].ToString() + "";
									}
								}
								else
								{
									pUnit = pUnit + dr915[i]["Content"].ToString() + "";
								}
								break;
							}
							if (!IsValid811)
							{
								return false;
							}
						}
						else
						{
							if (_strPostCodes.Length < 7)
							{
								return false;
							}
							string strCriteria1112 = "ChapCode = '" + ChapCode + "' AND CodeSection = '11' AND Code='" + _strPostCodes.Substring(5, 2) + "'";
							DataRow[] dr1112 = _dtAutoNumB.Select(strCriteria1112, "MinRow");
							int MaxRow1112 = -1;
							int MinRow1112 = -1;
							if (dr1112.Length <= 0)
							{
								return false;
							}
							bool IsValid1112 = true;
							for (int i = 0; i < dr1112.Length; i++)
							{
								if (ArchConvert.Obj2Int(dr1112[i]["MinRow"]) >= MaxRow709 || ArchConvert.Obj2Int(dr1112[i]["MaxRow"]) <= MinRow709)
								{
									IsValid1112 = false;
									continue;
								}
								IsValid1112 = true;
								MaxRow1112 = ArchConvert.Obj2Int(dr1112[i]["MaxRow"]);
								MinRow1112 = ArchConvert.Obj2Int(dr1112[i]["MinRow"]);
								pCName += ((dr1112[i]["Content"].ToString().Trim() == "") ? "" : (dr1112[i]["Content"].ToString() + "，"));
								break;
							}
							if (!IsValid1112)
							{
								return false;
							}
							string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
							DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
							if (drRM.Length > 0)
							{
								bool RM_Found = false;
								for (int z = 0; z < drRM.Length; z++)
								{
									if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
									{
										pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
										RM_Found = true;
										break;
									}
								}
								if (!RM_Found)
								{
									return false;
								}
							}
						}
					}
					else
					{
						if (_strPostCodes.Length < 6)
						{
							return false;
						}
						string strCriteria1113 = "ChapCode = '" + ChapCode + "' AND CodeSection = '10' AND Code='" + _strPostCodes.Substring(4, 2) + "'";
						DataRow[] dr1113 = _dtAutoNumB.Select(strCriteria1113, "MinRow");
						int MaxRow1113 = -1;
						int MinRow1113 = -1;
						bool IsValid1113 = true;
						for (int i = 0; i < dr1113.Length; i++)
						{
							if (ArchConvert.Obj2Int(dr1113[i]["MinRow"]) >= MaxRow810 || ArchConvert.Obj2Int(dr1113[i]["MaxRow"]) <= MinRow810)
							{
								IsValid1113 = false;
								continue;
							}
							IsValid1113 = true;
							MaxRow1113 = ArchConvert.Obj2Int(dr1113[i]["MaxRow"]);
							MinRow1113 = ArchConvert.Obj2Int(dr1113[i]["MinRow"]);
							pCName += ((dr1113[i]["Content"].ToString().Trim() == "") ? "" : (dr1113[i]["Content"].ToString() + "，"));
							break;
						}
						if (!IsValid1113)
						{
							return false;
						}
						if (_strPostCodes.Length < 7)
						{
							return false;
						}
						string strCriteria915 = "ChapCode = '" + ChapCode + "' AND CodeSection = '12' AND Code='" + _strPostCodes.Substring(6, 1) + "'";
						DataRow[] dr915 = _dtAutoNumB.Select(strCriteria915, "MinRow");
						int MaxRow812 = -1;
						int MinRow812 = -1;
						if (dr915.Length <= 0)
						{
							return false;
						}
						bool IsValid811 = true;
						for (int i = 0; i < dr915.Length; i++)
						{
							if (ArchConvert.Obj2Int(dr915[i]["MinRow"]) >= MaxRow1113 || ArchConvert.Obj2Int(dr915[i]["MaxRow"]) <= MinRow1113)
							{
								IsValid811 = false;
								continue;
							}
							IsValid811 = true;
							string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
							DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
							if (drRM.Length > 0)
							{
								bool RM_Found = false;
								for (int z = 0; z < drRM.Length; z++)
								{
									if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
									{
										pCName = pCName + dr915[i]["Content"].ToString() + "";
										pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
										RM_Found = true;
										break;
									}
								}
								if (!RM_Found)
								{
									pUnit = pUnit + dr915[i]["Content"].ToString() + "";
								}
							}
							else
							{
								pUnit = pUnit + dr915[i]["Content"].ToString() + "";
							}
							break;
						}
						if (!IsValid811)
						{
							return false;
						}
					}
				}
				else
				{
					if (_strPostCodes.Length < 5)
					{
						return false;
					}
					string strCriteria910 = "ChapCode = '" + ChapCode + "' AND CodeSection = '9' AND Code='" + _strPostCodes.Substring(3, 2) + "'";
					DataRow[] dr910 = _dtAutoNumB.Select(strCriteria910, "MinRow");
					int MaxRow910 = -1;
					int MinRow910 = -1;
					if (dr910.Length <= 0)
					{
						return false;
					}
					bool IsValid910 = true;
					for (int i = 0; i < dr910.Length; i++)
					{
						if (ArchConvert.Obj2Int(dr910[i]["MinRow"]) >= MaxRow708 || ArchConvert.Obj2Int(dr910[i]["MaxRow"]) <= MinRow708)
						{
							IsValid910 = false;
							continue;
						}
						IsValid910 = true;
						MaxRow910 = ArchConvert.Obj2Int(dr910[i]["MaxRow"]);
						MinRow910 = ArchConvert.Obj2Int(dr910[i]["MinRow"]);
						pCName += ((dr910[i]["Content"].ToString().Trim() == "") ? "" : (dr910[i]["Content"].ToString() + "，"));
						break;
					}
					if (!IsValid910)
					{
						return false;
					}
					if (_strPostCodes.Length < 6)
					{
						return false;
					}
					string strCriteria914 = "ChapCode = '" + ChapCode + "' AND CodeSection = '11' AND Code='" + _strPostCodes.Substring(5, 1) + "'";
					DataRow[] dr914 = _dtAutoNumB.Select(strCriteria914, "MinRow");
					int MaxRow811 = -1;
					int MinRow811 = -1;
					if (dr914.Length > 0)
					{
						bool IsValid810 = true;
						for (int i = 0; i < dr914.Length; i++)
						{
							if (ArchConvert.Obj2Int(dr914[i]["MinRow"]) >= MaxRow910 || ArchConvert.Obj2Int(dr914[i]["MaxRow"]) <= MinRow910)
							{
								IsValid810 = false;
								continue;
							}
							IsValid810 = true;
							MaxRow811 = ArchConvert.Obj2Int(dr914[i]["MaxRow"]);
							MinRow811 = ArchConvert.Obj2Int(dr914[i]["MinRow"]);
							pCName += ((dr914[i]["Content"].ToString().Trim() == "") ? "" : (dr914[i]["Content"].ToString() + "，"));
							break;
						}
						if (!IsValid810)
						{
							return false;
						}
						if (_strPostCodes.Length < 7)
						{
							return false;
						}
						string strCriteria915 = "ChapCode = '" + ChapCode + "' AND CodeSection = '12' AND Code='" + _strPostCodes.Substring(6, 1) + "'";
						DataRow[] dr915 = _dtAutoNumB.Select(strCriteria915, "MinRow");
						int MaxRow812 = -1;
						int MinRow812 = -1;
						if (dr915.Length <= 0)
						{
							return false;
						}
						bool IsValid811 = true;
						for (int i = 0; i < dr915.Length; i++)
						{
							if (ArchConvert.Obj2Int(dr915[i]["MinRow"]) >= MaxRow811 || ArchConvert.Obj2Int(dr915[i]["MaxRow"]) <= MinRow811)
							{
								IsValid811 = false;
								continue;
							}
							IsValid811 = true;
							string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
							DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
							if (drRM.Length > 0)
							{
								bool RM_Found = false;
								for (int z = 0; z < drRM.Length; z++)
								{
									if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
									{
										pCName = pCName + dr915[i]["Content"].ToString() + "";
										pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
										RM_Found = true;
										break;
									}
								}
								if (!RM_Found)
								{
									pUnit = pUnit + dr915[i]["Content"].ToString() + "";
								}
							}
							else
							{
								pUnit = pUnit + dr915[i]["Content"].ToString() + "";
							}
							break;
						}
						if (!IsValid811)
						{
							return false;
						}
					}
					else
					{
						if (_strPostCodes.Length < 7)
						{
							return false;
						}
						string strCriteria1112 = "ChapCode = '" + ChapCode + "' AND CodeSection = '11' AND Code='" + _strPostCodes.Substring(5, 2) + "'";
						DataRow[] dr1112 = _dtAutoNumB.Select(strCriteria1112, "MinRow");
						int MaxRow1112 = -1;
						int MinRow1112 = -1;
						if (dr1112.Length <= 0)
						{
							return false;
						}
						bool IsValid1112 = true;
						for (int i = 0; i < dr1112.Length; i++)
						{
							if (ArchConvert.Obj2Int(dr1112[i]["MinRow"]) >= MaxRow910 || ArchConvert.Obj2Int(dr1112[i]["MaxRow"]) <= MinRow910)
							{
								IsValid1112 = false;
								continue;
							}
							IsValid1112 = true;
							MaxRow1112 = ArchConvert.Obj2Int(dr1112[i]["MaxRow"]);
							MinRow1112 = ArchConvert.Obj2Int(dr1112[i]["MinRow"]);
							pCName += ((dr1112[i]["Content"].ToString().Trim() == "") ? "" : (dr1112[i]["Content"].ToString() + "，"));
							break;
						}
						if (!IsValid1112)
						{
							return false;
						}
						string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
						DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
						if (drRM.Length > 0)
						{
							bool RM_Found = false;
							for (int z = 0; z < drRM.Length; z++)
							{
								if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
								{
									pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
									RM_Found = true;
									break;
								}
							}
							if (!RM_Found)
							{
								return false;
							}
						}
					}
				}
			}
		}
		else if (ExtStr.Trim() != "12")
		{
			if (_strPostCodes.Length < 2)
			{
				return false;
			}
			string strCriteria1114 = "ChapCode = '" + ChapCode + "' AND CodeSection = '06' AND Code='" + _strPostCodes.Substring(0, 2) + "'";
			DataRow[] dr1114 = _dtAutoNumB.Select(strCriteria1114, "MinRow");
			int MaxRow1114 = -1;
			int MinRow1114 = -1;
			if (dr1114.Length <= 0)
			{
				return false;
			}
			MaxRow1114 = ArchConvert.Obj2Int(dr1114[0]["MaxRow"]);
			MinRow1114 = ArchConvert.Obj2Int(dr1114[0]["MinRow"]);
			pCName += ((dr1114[0]["Content"].ToString().Trim() == "") ? "" : (dr1114[0]["Content"].ToString() + "，"));
			if (_strPostCodes.Length < 3)
			{
				return false;
			}
			string strCriteria911 = "ChapCode = '" + ChapCode + "' AND CodeSection = '08' AND Code='" + _strPostCodes.Substring(2, 1) + "'";
			DataRow[] dr911 = _dtAutoNumB.Select(strCriteria911, "MinRow");
			int MaxRow710 = -1;
			int MinRow710 = -1;
			if (dr911.Length > 0)
			{
				bool IsValid711 = true;
				for (int i = 0; i < dr911.Length; i++)
				{
					if (ArchConvert.Obj2Int(dr911[i]["MinRow"]) >= MaxRow1114 || ArchConvert.Obj2Int(dr911[i]["MaxRow"]) <= MinRow1114)
					{
						IsValid711 = false;
						continue;
					}
					IsValid711 = true;
					MaxRow710 = ArchConvert.Obj2Int(dr911[i]["MaxRow"]);
					MinRow710 = ArchConvert.Obj2Int(dr911[i]["MinRow"]);
					pCName += ((dr911[i]["Content"].ToString().Trim() == "") ? "" : (dr911[i]["Content"].ToString() + "，"));
					break;
				}
				if (!IsValid711)
				{
					return false;
				}
				if (_strPostCodes.Length < 4)
				{
					return false;
				}
				string strCriteria913 = "ChapCode = '" + ChapCode + "' AND CodeSection = '09' AND Code='" + _strPostCodes.Substring(3, 1) + "'";
				DataRow[] dr913 = _dtAutoNumB.Select(strCriteria913, "MinRow");
				int MaxRow810 = -1;
				int MinRow810 = -1;
				if (dr913.Length > 0)
				{
					bool IsValid709 = true;
					for (int i = 0; i < dr913.Length; i++)
					{
						if (ArchConvert.Obj2Int(dr913[i]["MinRow"]) >= MaxRow710 || ArchConvert.Obj2Int(dr913[i]["MaxRow"]) <= MinRow710)
						{
							IsValid709 = false;
							continue;
						}
						IsValid709 = true;
						MaxRow810 = ArchConvert.Obj2Int(dr913[i]["MaxRow"]);
						MinRow810 = ArchConvert.Obj2Int(dr913[i]["MinRow"]);
						pCName += ((dr913[i]["Content"].ToString().Trim() == "") ? "" : (dr913[i]["Content"].ToString() + "，"));
						break;
					}
					if (!IsValid709)
					{
						return false;
					}
					if (_strPostCodes.Length < 5)
					{
						return false;
					}
					string strCriteria709 = "ChapCode = '" + ChapCode + "' AND CodeSection = '10' AND Code='" + _strPostCodes.Substring(4, 1) + "'";
					DataRow[] dr709 = _dtAutoNumB.Select(strCriteria709, "MinRow");
					int MaxRow709 = -1;
					int MinRow709 = -1;
					if (dr709.Length <= 0)
					{
						return false;
					}
					bool IsValid710 = true;
					for (int i = 0; i < dr709.Length; i++)
					{
						if (ArchConvert.Obj2Int(dr709[i]["MinRow"]) >= MaxRow810 || ArchConvert.Obj2Int(dr709[i]["MaxRow"]) <= MinRow810)
						{
							IsValid710 = false;
							continue;
						}
						IsValid710 = true;
						string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
						DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
						if (drRM.Length > 0)
						{
							bool RM_Found = false;
							for (int z = 0; z < drRM.Length; z++)
							{
								if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
								{
									pCName = pCName + dr709[i]["Content"].ToString() + "";
									pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
									RM_Found = true;
									break;
								}
							}
							if (!RM_Found)
							{
								pUnit = pUnit + dr709[i]["Content"].ToString() + "";
							}
						}
						else
						{
							pUnit = pUnit + dr709[i]["Content"].ToString() + "";
						}
						break;
					}
					if (!IsValid710)
					{
						return false;
					}
				}
				else
				{
					if (_strPostCodes.Length < 5)
					{
						return false;
					}
					string strCriteria910 = "ChapCode = '" + ChapCode + "' AND CodeSection = '09' AND Code='" + _strPostCodes.Substring(3, 2) + "'";
					DataRow[] dr910 = _dtAutoNumB.Select(strCriteria910, "MinRow");
					if (dr910.Length <= 0)
					{
						return false;
					}
					string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
					DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
					if (drRM.Length <= 0)
					{
						return false;
					}
					bool RM_Found = false;
					for (int z = 0; z < drRM.Length; z++)
					{
						if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
						{
							pCName = pCName + dr910[0]["Content"].ToString() + "";
							pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
							RM_Found = true;
							break;
						}
					}
					if (!RM_Found)
					{
						pUnit = pUnit + dr910[0]["Content"].ToString() + "";
					}
				}
			}
			else
			{
				if (_strPostCodes.Length < 4)
				{
					return false;
				}
				string strCriteria912 = "ChapCode = '" + ChapCode + "' AND CodeSection = '08' AND Code='" + _strPostCodes.Substring(2, 2) + "'";
				DataRow[] dr912 = _dtAutoNumB.Select(strCriteria912, "MinRow");
				int MaxRow809 = -1;
				int MinRow809 = -1;
				if (dr912.Length <= 0)
				{
					return false;
				}
				bool IsValid809 = true;
				for (int i = 0; i < dr912.Length; i++)
				{
					if (ArchConvert.Obj2Int(dr912[i]["MinRow"]) >= MaxRow1114 || ArchConvert.Obj2Int(dr912[i]["MaxRow"]) <= MinRow1114)
					{
						IsValid809 = false;
						continue;
					}
					IsValid809 = true;
					MaxRow809 = ArchConvert.Obj2Int(dr912[i]["MaxRow"]);
					MinRow809 = ArchConvert.Obj2Int(dr912[i]["MinRow"]);
					pCName += ((dr912[i]["Content"].ToString().Trim() == "") ? "" : (dr912[i]["Content"].ToString() + "，"));
					break;
				}
				if (!IsValid809)
				{
					return false;
				}
				if (_strPostCodes.Length < 5)
				{
					return false;
				}
				string strCriteria709 = "ChapCode = '" + ChapCode + "' AND CodeSection = '10' AND Code='" + _strPostCodes.Substring(4, 1) + "'";
				DataRow[] dr709 = _dtAutoNumB.Select(strCriteria709, "MinRow");
				if (dr709.Length <= 0)
				{
					return false;
				}
				bool IsValid710 = true;
				for (int i = 0; i < dr709.Length; i++)
				{
					if (ArchConvert.Obj2Int(dr709[i]["MinRow"]) >= MaxRow809 || ArchConvert.Obj2Int(dr709[i]["MaxRow"]) <= MinRow809)
					{
						IsValid710 = false;
						continue;
					}
					IsValid710 = true;
					string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Len(Trim(Content)) > 0";
					DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
					if (drRM.Length > 0)
					{
						bool RM_Found = false;
						for (int z = 0; z < drRM.Length; z++)
						{
							if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
							{
								pCName = pCName + dr709[i]["Content"].ToString() + "";
								pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
								RM_Found = true;
								break;
							}
						}
						if (!RM_Found)
						{
							pUnit = pUnit + dr709[i]["Content"].ToString() + "";
						}
					}
					else
					{
						pUnit = pUnit + dr709[i]["Content"].ToString() + "";
					}
					break;
				}
				if (!IsValid710)
				{
					return false;
				}
			}
		}
		else
		{
			if (_strPostCodes.Length < 2)
			{
				return false;
			}
			string strCriteria1114 = "ChapCode = '" + ChapCode + "' AND CodeSection = '06' AND Code='" + _strPostCodes.Substring(0, 2) + "'";
			DataRow[] dr1114 = _dtAutoNumB.Select(strCriteria1114, "MinRow");
			int MaxRow1114 = -1;
			int MinRow1114 = -1;
			if (dr1114.Length <= 0)
			{
				return false;
			}
			MaxRow1114 = ArchConvert.Obj2Int(dr1114[0]["MaxRow"]);
			MinRow1114 = ArchConvert.Obj2Int(dr1114[0]["MinRow"]);
			pCName += ((dr1114[0]["Content"].ToString().Trim() == "") ? "" : (dr1114[0]["Content"].ToString() + "，"));
			if (_strPostCodes.Length < 3)
			{
				return false;
			}
			string strCriteria911 = "ChapCode = '" + ChapCode + "' AND CodeSection = '08' AND Code='" + _strPostCodes.Substring(2, 1) + "'";
			DataRow[] dr911 = _dtAutoNumB.Select(strCriteria911, "MinRow");
			int MaxRow710 = -1;
			int MinRow710 = -1;
			if (dr911.Length > 0)
			{
				bool IsValid711 = true;
				for (int i = 0; i < dr911.Length; i++)
				{
					if (ArchConvert.Obj2Int(dr911[i]["MinRow"]) >= MaxRow1114 || ArchConvert.Obj2Int(dr911[i]["MaxRow"]) <= MinRow1114)
					{
						IsValid711 = false;
						continue;
					}
					IsValid711 = true;
					MaxRow710 = ArchConvert.Obj2Int(dr911[i]["MaxRow"]);
					MinRow710 = ArchConvert.Obj2Int(dr911[i]["MinRow"]);
					pCName += ((dr911[i]["Content"].ToString().Trim() == "") ? "" : (dr911[i]["Content"].ToString() + "，"));
					break;
				}
				if (!IsValid711)
				{
					return false;
				}
				if (_strPostCodes.Length < 4)
				{
					return false;
				}
				string strCriteria913 = "ChapCode = '" + ChapCode + "' AND CodeSection = '09' AND Code='" + _strPostCodes.Substring(3, 1) + "'";
				DataRow[] dr913 = _dtAutoNumB.Select(strCriteria913, "MinRow");
				int MaxRow810 = -1;
				int MinRow810 = -1;
				if (dr913.Length > 0)
				{
					bool IsValid709 = true;
					for (int i = 0; i < dr913.Length; i++)
					{
						if (ArchConvert.Obj2Int(dr913[i]["MinRow"]) >= MaxRow710 || ArchConvert.Obj2Int(dr913[i]["MaxRow"]) <= MinRow710)
						{
							IsValid709 = false;
							continue;
						}
						IsValid709 = true;
						MaxRow810 = ArchConvert.Obj2Int(dr913[i]["MaxRow"]);
						MinRow810 = ArchConvert.Obj2Int(dr913[i]["MinRow"]);
						pCName += ((dr913[i]["Content"].ToString().Trim() == "") ? "" : (dr913[i]["Content"].ToString() + "，"));
						break;
					}
					if (!IsValid709)
					{
						return false;
					}
					if (_strPostCodes.Length < 5)
					{
						return false;
					}
					string strCriteria709 = "ChapCode = '" + ChapCode + "' AND CodeSection = '10' AND Code='" + _strPostCodes.Substring(4, 1) + "'";
					DataRow[] dr709 = _dtAutoNumB.Select(strCriteria709, "MinRow");
					int MaxRow709 = -1;
					int MinRow709 = -1;
					if (dr709.Length > 0)
					{
						bool IsValid710 = true;
						for (int i = 0; i < dr709.Length; i++)
						{
							if (ArchConvert.Obj2Int(dr709[i]["MinRow"]) >= MaxRow810 || ArchConvert.Obj2Int(dr709[i]["MaxRow"]) <= MinRow810)
							{
								IsValid710 = false;
								continue;
							}
							IsValid710 = true;
							MaxRow709 = ArchConvert.Obj2Int(dr709[i]["MaxRow"]);
							MinRow709 = ArchConvert.Obj2Int(dr709[i]["MinRow"]);
							pCName += ((dr709[i]["Content"].ToString().Trim() == "") ? "" : (dr709[i]["Content"].ToString() + "，"));
							break;
						}
						if (!IsValid710)
						{
							return false;
						}
						if (_strPostCodes.Length < 6)
						{
							return false;
						}
						string strCriteria914 = "ChapCode = '" + ChapCode + "' AND CodeSection = '11' AND Code='" + _strPostCodes.Substring(5, 1) + "'";
						DataRow[] dr914 = _dtAutoNumB.Select(strCriteria914, "MinRow");
						int MaxRow811 = -1;
						int MinRow811 = -1;
						if (dr914.Length > 0)
						{
							bool IsValid810 = true;
							for (int i = 0; i < dr914.Length; i++)
							{
								if (ArchConvert.Obj2Int(dr914[i]["MinRow"]) >= MaxRow709 || ArchConvert.Obj2Int(dr914[i]["MaxRow"]) <= MinRow709)
								{
									IsValid810 = false;
									continue;
								}
								IsValid810 = true;
								MaxRow811 = ArchConvert.Obj2Int(dr914[i]["MaxRow"]);
								MinRow811 = ArchConvert.Obj2Int(dr914[i]["MinRow"]);
								pCName += ((dr914[i]["Content"].ToString().Trim() == "") ? "" : (dr914[i]["Content"].ToString() + "，"));
								break;
							}
							if (!IsValid810)
							{
								return false;
							}
							if (_strPostCodes.Length < 7)
							{
								return false;
							}
							string strCriteria915 = "ChapCode = '" + ChapCode + "' AND CodeSection = '12' AND Code='" + _strPostCodes.Substring(6, 1) + "'";
							DataRow[] dr915 = _dtAutoNumB.Select(strCriteria915, "MinRow");
							int MaxRow812 = -1;
							int MinRow812 = -1;
							if (dr915.Length <= 0)
							{
								return false;
							}
							bool IsValid811 = true;
							for (int i = 0; i < dr915.Length; i++)
							{
								if (ArchConvert.Obj2Int(dr915[i]["MinRow"]) >= MaxRow811 || ArchConvert.Obj2Int(dr915[i]["MaxRow"]) <= MinRow811)
								{
									IsValid811 = false;
									continue;
								}
								IsValid811 = true;
								string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
								DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
								if (drRM.Length > 0)
								{
									bool RM_Found = false;
									for (int z = 0; z < drRM.Length; z++)
									{
										if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
										{
											pCName = pCName + dr915[i]["Content"].ToString() + "";
											pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
											RM_Found = true;
											break;
										}
									}
									if (!RM_Found)
									{
										pUnit = pUnit + dr915[i]["Content"].ToString() + "";
									}
								}
								else
								{
									pUnit = pUnit + dr915[i]["Content"].ToString() + "";
								}
								break;
							}
							if (!IsValid811)
							{
								return false;
							}
						}
						else
						{
							if (_strPostCodes.Length < 7)
							{
								return false;
							}
							string strCriteria1112 = "ChapCode = '" + ChapCode + "' AND CodeSection = '11' AND Code='" + _strPostCodes.Substring(5, 2) + "'";
							DataRow[] dr1112 = _dtAutoNumB.Select(strCriteria1112, "MinRow");
							int MaxRow1112 = -1;
							int MinRow1112 = -1;
							if (dr1112.Length <= 0)
							{
								return false;
							}
							bool IsValid1112 = true;
							for (int i = 0; i < dr1112.Length; i++)
							{
								if (ArchConvert.Obj2Int(dr1112[i]["MinRow"]) >= MaxRow709 || ArchConvert.Obj2Int(dr1112[i]["MaxRow"]) <= MinRow709)
								{
									IsValid1112 = false;
									continue;
								}
								IsValid1112 = true;
								MaxRow1112 = ArchConvert.Obj2Int(dr1112[i]["MaxRow"]);
								MinRow1112 = ArchConvert.Obj2Int(dr1112[i]["MinRow"]);
								pCName += ((dr1112[i]["Content"].ToString().Trim() == "") ? "" : (dr1112[i]["Content"].ToString() + "，"));
								break;
							}
							if (!IsValid1112)
							{
								return false;
							}
							string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
							DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
							if (drRM.Length > 0)
							{
								bool RM_Found = false;
								for (int z = 0; z < drRM.Length; z++)
								{
									if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
									{
										pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
										RM_Found = true;
										break;
									}
								}
								if (!RM_Found)
								{
									return false;
								}
							}
						}
					}
					else
					{
						if (_strPostCodes.Length < 6)
						{
							return false;
						}
						string strCriteria1113 = "ChapCode = '" + ChapCode + "' AND CodeSection = '10' AND Code='" + _strPostCodes.Substring(4, 2) + "'";
						DataRow[] dr1113 = _dtAutoNumB.Select(strCriteria1113, "MinRow");
						int MaxRow1113 = -1;
						int MinRow1113 = -1;
						bool IsValid1113 = true;
						for (int i = 0; i < dr1113.Length; i++)
						{
							if (ArchConvert.Obj2Int(dr1113[i]["MinRow"]) >= MaxRow810 || ArchConvert.Obj2Int(dr1113[i]["MaxRow"]) <= MinRow810)
							{
								IsValid1113 = false;
								continue;
							}
							IsValid1113 = true;
							MaxRow1113 = ArchConvert.Obj2Int(dr1113[i]["MaxRow"]);
							MinRow1113 = ArchConvert.Obj2Int(dr1113[i]["MinRow"]);
							pCName += ((dr1113[i]["Content"].ToString().Trim() == "") ? "" : (dr1113[i]["Content"].ToString() + "，"));
							break;
						}
						if (!IsValid1113)
						{
							return false;
						}
						if (_strPostCodes.Length < 7)
						{
							return false;
						}
						string strCriteria915 = "ChapCode = '" + ChapCode + "' AND CodeSection = '12' AND Code='" + _strPostCodes.Substring(6, 1) + "'";
						DataRow[] dr915 = _dtAutoNumB.Select(strCriteria915, "MinRow");
						int MaxRow812 = -1;
						int MinRow812 = -1;
						if (dr915.Length <= 0)
						{
							return false;
						}
						bool IsValid811 = true;
						for (int i = 0; i < dr915.Length; i++)
						{
							if (ArchConvert.Obj2Int(dr915[i]["MinRow"]) >= MaxRow1113 || ArchConvert.Obj2Int(dr915[i]["MaxRow"]) <= MinRow1113)
							{
								IsValid811 = false;
								continue;
							}
							IsValid811 = true;
							string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
							DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
							if (drRM.Length > 0)
							{
								bool RM_Found = false;
								for (int z = 0; z < drRM.Length; z++)
								{
									if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
									{
										pCName = pCName + dr915[i]["Content"].ToString() + "";
										pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
										RM_Found = true;
										break;
									}
								}
								if (!RM_Found)
								{
									pUnit = pUnit + dr915[i]["Content"].ToString() + "";
								}
							}
							else
							{
								pUnit = pUnit + dr915[i]["Content"].ToString() + "";
							}
							break;
						}
						if (!IsValid811)
						{
							return false;
						}
					}
				}
				else
				{
					if (_strPostCodes.Length < 5)
					{
						return false;
					}
					string strCriteria910 = "ChapCode = '" + ChapCode + "' AND CodeSection = '09' AND Code='" + _strPostCodes.Substring(3, 2) + "'";
					DataRow[] dr910 = _dtAutoNumB.Select(strCriteria910, "MinRow");
					int MaxRow910 = -1;
					int MinRow910 = -1;
					if (dr910.Length <= 0)
					{
						return false;
					}
					bool IsValid910 = true;
					for (int i = 0; i < dr910.Length; i++)
					{
						if (ArchConvert.Obj2Int(dr910[i]["MinRow"]) >= MaxRow710 || ArchConvert.Obj2Int(dr910[i]["MaxRow"]) <= MinRow710)
						{
							IsValid910 = false;
							continue;
						}
						IsValid910 = true;
						MaxRow910 = ArchConvert.Obj2Int(dr910[i]["MaxRow"]);
						MinRow910 = ArchConvert.Obj2Int(dr910[i]["MinRow"]);
						pCName += ((dr910[i]["Content"].ToString().Trim() == "") ? "" : (dr910[i]["Content"].ToString() + "，"));
						break;
					}
					if (!IsValid910)
					{
						return false;
					}
					if (_strPostCodes.Length < 6)
					{
						return false;
					}
					string strCriteria914 = "ChapCode = '" + ChapCode + "' AND CodeSection = '11' AND Code='" + _strPostCodes.Substring(5, 1) + "'";
					DataRow[] dr914 = _dtAutoNumB.Select(strCriteria914, "MinRow");
					int MaxRow811 = -1;
					int MinRow811 = -1;
					if (dr914.Length > 0)
					{
						bool IsValid810 = true;
						for (int i = 0; i < dr914.Length; i++)
						{
							if (ArchConvert.Obj2Int(dr914[i]["MinRow"]) >= MaxRow910 || ArchConvert.Obj2Int(dr914[i]["MaxRow"]) <= MinRow910)
							{
								IsValid810 = false;
								continue;
							}
							IsValid810 = true;
							MaxRow811 = ArchConvert.Obj2Int(dr914[i]["MaxRow"]);
							MinRow811 = ArchConvert.Obj2Int(dr914[i]["MinRow"]);
							pCName += ((dr914[i]["Content"].ToString().Trim() == "") ? "" : (dr914[i]["Content"].ToString() + "，"));
							break;
						}
						if (!IsValid810)
						{
							return false;
						}
						if (_strPostCodes.Length < 7)
						{
							return false;
						}
						string strCriteria915 = "ChapCode = '" + ChapCode + "' AND CodeSection = '12' AND Code='" + _strPostCodes.Substring(6, 1) + "'";
						DataRow[] dr915 = _dtAutoNumB.Select(strCriteria915, "MinRow");
						int MaxRow812 = -1;
						int MinRow812 = -1;
						if (dr915.Length <= 0)
						{
							return false;
						}
						bool IsValid811 = true;
						for (int i = 0; i < dr915.Length; i++)
						{
							if (ArchConvert.Obj2Int(dr915[i]["MinRow"]) >= MaxRow811 || ArchConvert.Obj2Int(dr915[i]["MaxRow"]) <= MinRow811)
							{
								IsValid811 = false;
								continue;
							}
							IsValid811 = true;
							string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
							DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
							if (drRM.Length > 0)
							{
								bool RM_Found = false;
								for (int z = 0; z < drRM.Length; z++)
								{
									if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
									{
										pCName = pCName + dr915[i]["Content"].ToString() + "";
										pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
										RM_Found = true;
										break;
									}
								}
								if (!RM_Found)
								{
									pUnit = pUnit + dr915[i]["Content"].ToString() + "";
								}
							}
							else
							{
								pUnit = pUnit + dr915[i]["Content"].ToString() + "";
							}
							break;
						}
						if (!IsValid811)
						{
							return false;
						}
					}
					else
					{
						if (_strPostCodes.Length < 7)
						{
							return false;
						}
						string strCriteria1112 = "ChapCode = '" + ChapCode + "' AND CodeSection = '11' AND Code='" + _strPostCodes.Substring(5, 2) + "'";
						DataRow[] dr1112 = _dtAutoNumB.Select(strCriteria1112, "MinRow");
						int MaxRow1112 = -1;
						int MinRow1112 = -1;
						if (dr1112.Length <= 0)
						{
							return false;
						}
						bool IsValid1112 = true;
						for (int i = 0; i < dr1112.Length; i++)
						{
							if (ArchConvert.Obj2Int(dr1112[i]["MinRow"]) >= MaxRow910 || ArchConvert.Obj2Int(dr1112[i]["MaxRow"]) <= MinRow910)
							{
								IsValid1112 = false;
								continue;
							}
							IsValid1112 = true;
							MaxRow1112 = ArchConvert.Obj2Int(dr1112[i]["MaxRow"]);
							MinRow1112 = ArchConvert.Obj2Int(dr1112[i]["MinRow"]);
							pCName += ((dr1112[i]["Content"].ToString().Trim() == "") ? "" : (dr1112[i]["Content"].ToString() + "，"));
							break;
						}
						if (!IsValid1112)
						{
							return false;
						}
						string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
						DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
						if (drRM.Length > 0)
						{
							bool RM_Found = false;
							for (int z = 0; z < drRM.Length; z++)
							{
								if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
								{
									pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
									RM_Found = true;
									break;
								}
							}
							if (!RM_Found)
							{
								return false;
							}
						}
					}
				}
			}
			else
			{
				if (_strPostCodes.Length < 4)
				{
					return false;
				}
				string strCriteria912 = "ChapCode = '" + ChapCode + "' AND CodeSection = '8' AND Code='" + _strPostCodes.Substring(2, 2) + "'";
				DataRow[] dr912 = _dtAutoNumB.Select(strCriteria912, "MinRow");
				int MaxRow809 = -1;
				int MinRow809 = -1;
				if (dr912.Length <= 0)
				{
					return false;
				}
				bool IsValid809 = true;
				for (int i = 0; i < dr912.Length; i++)
				{
					if (ArchConvert.Obj2Int(dr912[i]["MinRow"]) >= MaxRow1114 || ArchConvert.Obj2Int(dr912[i]["MaxRow"]) <= MinRow1114)
					{
						IsValid809 = false;
						continue;
					}
					IsValid809 = true;
					MaxRow809 = ArchConvert.Obj2Int(dr912[i]["MaxRow"]);
					MinRow809 = ArchConvert.Obj2Int(dr912[i]["MinRow"]);
					pCName += ((dr912[i]["Content"].ToString().Trim() == "") ? "" : (dr912[i]["Content"].ToString() + "，"));
					break;
				}
				if (!IsValid809)
				{
					return false;
				}
				if (_strPostCodes.Length < 5)
				{
					return false;
				}
				string strCriteria709 = "ChapCode = '" + ChapCode + "' AND CodeSection = '10' AND Code='" + _strPostCodes.Substring(4, 1) + "'";
				DataRow[] dr709 = _dtAutoNumB.Select(strCriteria709, "MinRow");
				int MaxRow709 = -1;
				int MinRow709 = -1;
				if (dr709.Length > 0)
				{
					bool IsValid710 = true;
					for (int i = 0; i < dr709.Length; i++)
					{
						if (ArchConvert.Obj2Int(dr709[i]["MinRow"]) >= MaxRow809 || ArchConvert.Obj2Int(dr709[i]["MaxRow"]) <= MinRow809)
						{
							IsValid710 = false;
							continue;
						}
						IsValid710 = true;
						MaxRow709 = ArchConvert.Obj2Int(dr709[i]["MaxRow"]);
						MinRow709 = ArchConvert.Obj2Int(dr709[i]["MinRow"]);
						pCName += ((dr709[i]["Content"].ToString().Trim() == "") ? "" : (dr709[i]["Content"].ToString() + "，"));
						break;
					}
					if (!IsValid710)
					{
						return false;
					}
					if (_strPostCodes.Length < 6)
					{
						return false;
					}
					string strCriteria914 = "ChapCode = '" + ChapCode + "' AND CodeSection = '11' AND Code='" + _strPostCodes.Substring(5, 1) + "'";
					DataRow[] dr914 = _dtAutoNumB.Select(strCriteria914, "MinRow");
					int MaxRow811 = -1;
					int MinRow811 = -1;
					if (dr914.Length > 0)
					{
						bool IsValid810 = true;
						for (int i = 0; i < dr914.Length; i++)
						{
							if (ArchConvert.Obj2Int(dr914[i]["MinRow"]) >= MaxRow709 || ArchConvert.Obj2Int(dr914[i]["MaxRow"]) <= MinRow709)
							{
								IsValid810 = false;
								continue;
							}
							IsValid810 = true;
							MaxRow811 = ArchConvert.Obj2Int(dr914[i]["MaxRow"]);
							MinRow811 = ArchConvert.Obj2Int(dr914[i]["MinRow"]);
							pCName += ((dr914[i]["Content"].ToString().Trim() == "") ? "" : (dr914[i]["Content"].ToString() + "，"));
							break;
						}
						if (!IsValid810)
						{
							return false;
						}
						if (_strPostCodes.Length < 7)
						{
							return false;
						}
						string strCriteria915 = "ChapCode = '" + ChapCode + "' AND CodeSection = '12' AND Code='" + _strPostCodes.Substring(6, 1) + "'";
						DataRow[] dr915 = _dtAutoNumB.Select(strCriteria915, "MinRow");
						int MaxRow812 = -1;
						int MinRow812 = -1;
						if (dr915.Length <= 0)
						{
							return false;
						}
						bool IsValid811 = true;
						for (int i = 0; i < dr915.Length; i++)
						{
							if (ArchConvert.Obj2Int(dr915[i]["MinRow"]) >= MaxRow811 || ArchConvert.Obj2Int(dr915[i]["MaxRow"]) <= MinRow811)
							{
								IsValid811 = false;
								continue;
							}
							IsValid811 = true;
							string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
							DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
							if (drRM.Length > 0)
							{
								bool RM_Found = false;
								for (int z = 0; z < drRM.Length; z++)
								{
									if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
									{
										pCName = pCName + dr915[i]["Content"].ToString() + "";
										pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
										RM_Found = true;
										break;
									}
								}
								if (!RM_Found)
								{
									pUnit = pUnit + dr915[i]["Content"].ToString() + "";
								}
							}
							else
							{
								pUnit = pUnit + dr915[i]["Content"].ToString() + "";
							}
							break;
						}
						if (!IsValid811)
						{
							return false;
						}
					}
					else
					{
						if (_strPostCodes.Length < 7)
						{
							return false;
						}
						string strCriteria1112 = "ChapCode = '" + ChapCode + "' AND CodeSection = '11' AND Code='" + _strPostCodes.Substring(5, 2) + "'";
						DataRow[] dr1112 = _dtAutoNumB.Select(strCriteria1112, "MinRow");
						int MaxRow1112 = -1;
						int MinRow1112 = -1;
						if (dr1112.Length <= 0)
						{
							return false;
						}
						bool IsValid1112 = true;
						for (int i = 0; i < dr1112.Length; i++)
						{
							if (ArchConvert.Obj2Int(dr1112[i]["MinRow"]) >= MaxRow709 || ArchConvert.Obj2Int(dr1112[i]["MaxRow"]) <= MinRow709)
							{
								IsValid1112 = false;
								continue;
							}
							IsValid1112 = true;
							MaxRow1112 = ArchConvert.Obj2Int(dr1112[i]["MaxRow"]);
							MinRow1112 = ArchConvert.Obj2Int(dr1112[i]["MinRow"]);
							pCName += ((dr1112[i]["Content"].ToString().Trim() == "") ? "" : (dr1112[i]["Content"].ToString() + "，"));
							break;
						}
						if (!IsValid1112)
						{
							return false;
						}
						string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
						DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
						if (drRM.Length > 0)
						{
							bool RM_Found = false;
							for (int z = 0; z < drRM.Length; z++)
							{
								if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
								{
									pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
									RM_Found = true;
									break;
								}
							}
							if (!RM_Found)
							{
								return false;
							}
						}
					}
				}
				else
				{
					if (_strPostCodes.Length < 6)
					{
						return false;
					}
					string strCriteria1113 = "ChapCode = '" + ChapCode + "' AND CodeSection = '10' AND Code='" + _strPostCodes.Substring(4, 2) + "'";
					DataRow[] dr1113 = _dtAutoNumB.Select(strCriteria1113, "MinRow");
					int MaxRow1113 = -1;
					int MinRow1113 = -1;
					bool IsValid1113 = true;
					for (int i = 0; i < dr1113.Length; i++)
					{
						if (ArchConvert.Obj2Int(dr1113[i]["MinRow"]) >= MaxRow809 || ArchConvert.Obj2Int(dr1113[i]["MaxRow"]) <= MinRow809)
						{
							IsValid1113 = false;
							continue;
						}
						IsValid1113 = true;
						MaxRow1113 = ArchConvert.Obj2Int(dr1113[i]["MaxRow"]);
						MinRow1113 = ArchConvert.Obj2Int(dr1113[i]["MinRow"]);
						pCName += ((dr1113[i]["Content"].ToString().Trim() == "") ? "" : (dr1113[i]["Content"].ToString() + "，"));
						break;
					}
					if (!IsValid1113)
					{
						return false;
					}
					if (_strPostCodes.Length < 7)
					{
						return false;
					}
					string strCriteria915 = "ChapCode = '" + ChapCode + "' AND CodeSection = '12' AND Code='" + _strPostCodes.Substring(6, 1) + "'";
					DataRow[] dr915 = _dtAutoNumB.Select(strCriteria915, "MinRow");
					int MaxRow812 = -1;
					int MinRow812 = -1;
					if (dr915.Length <= 0)
					{
						return false;
					}
					bool IsValid811 = true;
					for (int i = 0; i < dr915.Length; i++)
					{
						if (ArchConvert.Obj2Int(dr915[i]["MinRow"]) >= MaxRow1113 || ArchConvert.Obj2Int(dr915[i]["MaxRow"]) <= MinRow1113)
						{
							IsValid811 = false;
							continue;
						}
						IsValid811 = true;
						string strCriteriaRM = "ChapCode = '" + ChapCode + "' AND CodeSection = 'RM' And Trim(Content) <> ''";
						DataRow[] drRM = _dtAutoNumB.Select(strCriteriaRM);
						if (drRM.Length > 0)
						{
							bool RM_Found = false;
							for (int z = 0; z < drRM.Length; z++)
							{
								if (drRM[z]["Content"].ToString().IndexOf("單位") > -1)
								{
									pCName = pCName + dr915[i]["Content"].ToString() + "";
									pUnit += drRM[z]["Content"].ToString().Replace("單位：", "").Replace("單位:", "");
									RM_Found = true;
									break;
								}
							}
							if (!RM_Found)
							{
								pUnit = pUnit + dr915[i]["Content"].ToString() + "";
							}
						}
						else
						{
							pUnit = pUnit + dr915[i]["Content"].ToString() + "";
						}
						break;
					}
					if (!IsValid811)
					{
						return false;
					}
				}
			}
		}
		return true;
	}
}
