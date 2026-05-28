using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.PccesMain.Budget;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.ArchControls.ProjectInfoSummaryControls;

public class SummaryControlBase : UserControl
{
	protected DataSet ControlDataSet = new DataSet();

	public string ProjectCode;

	private DataSet subMemoInfo;

	protected string XSDFileDirectory = AppDomain.CurrentDomain.BaseDirectory + "ProjectInfo\\";

	protected FormBudgetProjectInfo budgetProjectInfo;

	public double F_Amount;

	public string F_UserID;

	public virtual void SetXML()
	{
		SubMemo subMemo = new SubMemo();
		subMemoInfo = subMemo.GetSubMemo(ProjectCode);
		string XMLString = string.Empty;
		if (subMemoInfo.Tables[0].Rows.Count > 0)
		{
			XMLString = subMemoInfo.Tables[0].Rows[0]["ResultSummary"].ToString();
		}
		if (XMLString != "" && ValidateXML(XMLString))
		{
			StringReader StrReader = new StringReader(XMLString);
			ControlDataSet.Clear();
			ControlDataSet.ReadXml(StrReader);
			DataToForm();
			ReCalculate(null, null);
		}
	}

	public virtual void GetXML()
	{
		SubMemo subMemo = new SubMemo();
		subMemoInfo = subMemo.GetSubMemo(ProjectCode);
		FormToData();
		StringWriter XMLString = new StringWriter();
		ControlDataSet.WriteXml(XMLString);
		subMemoInfo.Tables[0].Rows[0]["ResultSummary"] = XMLString;
		subMemo.UpdateSubMemo(subMemoInfo);
	}

	public void SetProjectCode(string ProjectCode)
	{
		this.ProjectCode = ProjectCode;
	}

	protected virtual void DataToForm()
	{
		foreach (Control control in base.Controls)
		{
			if ((object)control.GetType() != typeof(GroupBox))
			{
				continue;
			}
			Control cnt = control;
			for (int i = 0; i < cnt.Controls.Count; i++)
			{
				if ((object)cnt.Controls[i].GetType() == typeof(UltraTextEditor))
				{
					UltraTextEditor item = (UltraTextEditor)cnt.Controls[i];
					if (ControlDataSet.Tables["InfoSummary"].Columns.Contains(item.Name))
					{
						item.Text = ControlDataSet.Tables["InfoSummary"].Rows[0][item.Name].ToString();
					}
				}
				else
				{
					if ((object)cnt.Controls[i].GetType() != typeof(UltraComboEditor))
					{
						continue;
					}
					UltraComboEditor item2 = (UltraComboEditor)cnt.Controls[i];
					if (ControlDataSet.Tables["InfoSummary"].Columns.Contains(item2.Name) && ControlDataSet.Tables["InfoSummary"].Rows[0][item2.Name].ToString() != "-1")
					{
						if (ControlDataSet.Tables["InfoSummary"].Rows[0][item2.Name] != DBNull.Value)
						{
							item2.SelectedIndex = Convert.ToInt32(ControlDataSet.Tables["InfoSummary"].Rows[0][item2.Name]);
						}
						else
						{
							item2.SelectedIndex = -1;
						}
					}
				}
			}
		}
	}

	protected virtual void FormToData()
	{
		ControlDataSet.Clear();
		DataRow row = ControlDataSet.Tables["InfoSummary"].NewRow();
		foreach (Control control in base.Controls)
		{
			if ((object)control.GetType() != typeof(GroupBox))
			{
				continue;
			}
			Control cnt = control;
			for (int i = 0; i < cnt.Controls.Count; i++)
			{
				if ((object)cnt.Controls[i].GetType() == typeof(UltraTextEditor))
				{
					UltraTextEditor item = (UltraTextEditor)cnt.Controls[i];
					if (ControlDataSet.Tables[0].Columns.Contains(item.Name) && item.Text != "")
					{
						try
						{
							row[item.Name] = item.Text;
						}
						catch
						{
						}
					}
				}
				else
				{
					if ((object)cnt.Controls[i].GetType() != typeof(UltraComboEditor))
					{
						continue;
					}
					UltraComboEditor item2 = (UltraComboEditor)cnt.Controls[i];
					if (ControlDataSet.Tables[0].Columns.Contains(item2.Name))
					{
						try
						{
							row[item2.Name] = item2.SelectedIndex;
						}
						catch
						{
						}
					}
				}
			}
		}
		ControlDataSet.Tables["InfoSummary"].Rows.Add(row);
	}

	protected virtual void ReCalculate(object sender, EventArgs e)
	{
	}

	public virtual bool IsRequiredFilled()
	{
		return false;
	}

	protected bool ValidateXML(string XMLString)
	{
		StringReader StrReader = new StringReader(XMLString);
		XmlReader reader = XmlReader.Create(StrReader);
		reader.Read();
		if (reader.Name == ControlDataSet.DataSetName)
		{
			return true;
		}
		return false;
	}

	protected void Input_Leave(object sender, EventArgs e)
	{
		string input = (sender as UltraTextEditor).Text;
		try
		{
			if (!(input == string.Empty))
			{
				Convert.ToDouble(input);
			}
		}
		catch
		{
			MessageBox.Show(this, "請輸入數字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			(sender as UltraTextEditor).Focus();
		}
	}

	protected double String2Double(string Number)
	{
		double Value = 1.0;
		try
		{
			if (Number != "")
			{
				Value = double.Parse(Number);
			}
		}
		catch
		{
		}
		return Value;
	}
}
