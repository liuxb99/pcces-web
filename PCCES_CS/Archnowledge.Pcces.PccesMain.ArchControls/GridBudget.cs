using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Aspose.Cells;
using C1.Win.C1FlexGrid;

namespace Archnowledge.Pcces.PccesMain.ArchControls;

public class GridBudget : C1FlexGrid
{
	private Container components = null;

	private ToolTip toolTip = null;

	private int lastRowIndex = 0;

	private int lastColIndex = 0;

	private string excelSheetName = string.Empty;

	private string excelFileName = string.Empty;

	private bool F_IsOpenExcelAfterExport = false;

	private bool showToolTipOnNarrowColumn = true;

	public string _ExcelSheeName
	{
		get
		{
			return excelSheetName;
		}
		set
		{
			excelSheetName = value;
		}
	}

	public string _ExcelFileName
	{
		get
		{
			return excelFileName;
		}
		set
		{
			excelFileName = value;
		}
	}

	public bool _IsOpenExcelAfterExport
	{
		get
		{
			return F_IsOpenExcelAfterExport;
		}
		set
		{
			F_IsOpenExcelAfterExport = value;
		}
	}

	public bool ShowToolTipOnNarrowColumn
	{
		get
		{
			return showToolTipOnNarrowColumn;
		}
		set
		{
			showToolTipOnNarrowColumn = value;
		}
	}

	public int SelectedRowCount => base.Rows.Selected.Count;

	private void InitializeComponent()
	{
		((System.ComponentModel.ISupportInitialize)this).BeginInit();
		base.MouseMove += new System.Windows.Forms.MouseEventHandler(GridBudget_MouseMove);
		this.toolTip = new System.Windows.Forms.ToolTip();
		this.toolTip.InitialDelay = 1;
		((System.ComponentModel.ISupportInitialize)this).EndInit();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	public GridBudget(IContainer container)
	{
		container.Add(this);
		InitializeComponent();
	}

	public GridBudget()
	{
		InitializeComponent();
	}

	public void ExecuteExport(c1GridExportType ExpType)
	{
		if (ExpType == c1GridExportType.Excel)
		{
			Do_ExcelExport2();
		}
	}

	private Style SwitchStyle(C1.Win.C1FlexGrid.Column myCol)
	{
		Aspose.Cells.License license = new Aspose.Cells.License();
		license.SetLicense("Aspose.Custom.lic");
		Excel myExcel = new Excel();
		int iStyleIndex = myExcel.Styles.Add();
		Style AsposeStyle1 = myExcel.Styles[iStyleIndex];
		iStyleIndex = myExcel.Styles.Add();
		Style AsposeStyle2_1 = myExcel.Styles[iStyleIndex];
		AsposeStyle2_1.Custom = "_-* #,##0." + Replicate("0", 4) + "_-;-* #,##0." + Replicate("0", 4) + "_-;_-* \"-\"_-;_-@_-";
		iStyleIndex = myExcel.Styles.Add();
		Style AsposeStyle2_2 = myExcel.Styles[iStyleIndex];
		iStyleIndex = myExcel.Styles.Add();
		Style AsposeStyle3 = myExcel.Styles[iStyleIndex];
		AsposeStyle3.Custom = "yyyy/mm/dd";
		Style RetV = AsposeStyle1;
		if ((object)myCol.DataType == Type.GetType("System.String"))
		{
			RetV = AsposeStyle1;
		}
		else if ((object)myCol.DataType == Type.GetType("System.Int16") || (object)myCol.DataType == Type.GetType("System.Int64") || (object)myCol.DataType == Type.GetType("System.Int32") || (object)myCol.DataType == Type.GetType("System.Double") || (object)myCol.DataType == Type.GetType("System.Decimal"))
		{
			RetV = AsposeStyle2_1;
		}
		else if ((object)myCol.DataType == Type.GetType("System.DateTime"))
		{
			RetV = AsposeStyle3;
		}
		return RetV;
	}

	private Style SwitchStyle2(C1.Win.C1FlexGrid.Column myCol)
	{
		Aspose.Cells.License license = new Aspose.Cells.License();
		license.SetLicense("Aspose.Custom.lic");
		Excel myExcel = new Excel();
		int iStyleIndex = myExcel.Styles.Add();
		Style AsposeStyle1 = myExcel.Styles[iStyleIndex];
		AsposeStyle1.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
		AsposeStyle1.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
		AsposeStyle1.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
		AsposeStyle1.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
		AsposeStyle1.IsTextWrapped = true;
		iStyleIndex = myExcel.Styles.Add();
		Style AsposeStyle2_1 = myExcel.Styles[iStyleIndex];
		AsposeStyle2_1.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
		AsposeStyle2_1.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
		AsposeStyle2_1.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
		AsposeStyle2_1.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
		AsposeStyle2_1.Custom = "_-* #,##0." + Replicate("0", 4) + "_-;-* #,##0." + Replicate("0", 4) + "_-;_-* \"-\"_-;_-@_-";
		iStyleIndex = myExcel.Styles.Add();
		Style AsposeStyle2_2 = myExcel.Styles[iStyleIndex];
		AsposeStyle2_2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
		AsposeStyle2_2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
		AsposeStyle2_2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
		AsposeStyle2_2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
		iStyleIndex = myExcel.Styles.Add();
		Style AsposeStyle3 = myExcel.Styles[iStyleIndex];
		AsposeStyle3.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
		AsposeStyle3.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
		AsposeStyle3.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
		AsposeStyle3.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
		AsposeStyle3.Custom = "yyyy/mm/dd";
		Style RetV = AsposeStyle1;
		if ((object)myCol.DataType == Type.GetType("System.String"))
		{
			RetV = AsposeStyle1;
		}
		else if ((object)myCol.DataType == Type.GetType("System.Int16") || (object)myCol.DataType == Type.GetType("System.Int64") || (object)myCol.DataType == Type.GetType("System.Int32") || (object)myCol.DataType == Type.GetType("System.Double") || (object)myCol.DataType == Type.GetType("System.Decimal"))
		{
			RetV = AsposeStyle2_1;
		}
		else if ((object)myCol.DataType == Type.GetType("System.DateTime"))
		{
			RetV = AsposeStyle3;
		}
		return RetV;
	}

	private void Do_ExcelExport2()
	{
		string ssFileName = ((excelFileName != "") ? excelFileName : ("Excel_" + $"{DateTime.Now:yyyyMMddHHmmss}"));
		Aspose.Cells.License license = new Aspose.Cells.License();
		license.SetLicense("Aspose.Custom.lic");
		Excel myExcel = new Excel();
		int iStyleIndex = myExcel.Styles.Add();
		Style myStyleDet_Header = myExcel.Styles[iStyleIndex];
		myStyleDet_Header.Font.Size = 14;
		myStyleDet_Header.Font.IsBold = true;
		myStyleDet_Header.Font.Name = "新細明體";
		myStyleDet_Header.HorizontalAlignment = TextAlignmentType.Center;
		myStyleDet_Header.VerticalAlignment = TextAlignmentType.Center;
		myStyleDet_Header.IsTextWrapped = true;
		myExcel.Worksheets.Add();
		Worksheet mySheet = myExcel.Worksheets[0];
		mySheet.PageSetup.SetHeader(2, "&\"新細明體,標準\"&9 第 &P 頁 共 &N 頁 \n\n 日期：" + DateTime.Today.Date.ToString("yyyy/MM/dd"));
		mySheet.PageSetup.SetHeader(1, "經費審查比對");
		mySheet.PageSetup.Orientation = PageOrientationType.Landscape;
		Cells myCells = mySheet.Cells;
		if (excelSheetName != "")
		{
			mySheet.Name = excelSheetName;
		}
		mySheet.Cells.SetColumnWidth(3, 14.0);
		mySheet.Cells.SetColumnWidth(4, 34.0);
		mySheet.Cells.SetColumnWidth(5, 5.0);
		mySheet.Cells.SetColumnWidth(6, 6.5);
		mySheet.Cells.SetColumnWidth(7, 6.5);
		mySheet.Cells.SetColumnWidth(8, 28.0);
		mySheet.Cells.SetColumnWidth(9, 28.0);
		mySheet.Cells.SetColumnWidth(10, 28.0);
		mySheet.Cells.SetColumnWidth(11, 28.0);
		mySheet.Cells.SetColumnWidth(12, 28.0);
		mySheet.Cells.SetColumnWidth(13, 28.0);
		mySheet.Cells.SetColumnWidth(14, 28.0);
		mySheet.Cells.SetColumnWidth(15, 28.0);
		mySheet.Cells.SetColumnWidth(16, 28.0);
		mySheet.Cells.SetColumnWidth(17, 28.0);
		mySheet.Cells.SetColumnWidth(18, 28.0);
		mySheet.Cells.SetColumnWidth(19, 28.0);
		mySheet.Cells.SetColumnWidth(20, 28.0);
		mySheet.Cells.SetColumnWidth(21, 28.0);
		mySheet.Cells.SetColumnWidth(22, 28.0);
		mySheet.Cells.SetColumnWidth(23, 28.0);
		mySheet.Cells.SetColumnWidth(24, 28.0);
		mySheet.Cells.SetColumnWidth(25, 28.0);
		mySheet.Cells.SetColumnWidth(31, 20.0);
		for (int i = 0; i < base.Rows.Count; i++)
		{
			for (int j = 0; j < base.Cols.Count; j++)
			{
				if (base.Cols[j].Visible)
				{
					mySheet.Cells[i, j].PutValue((base[i, j] != null) ? base[i, j] : "");
				}
				else
				{
					mySheet.Cells.SetColumnWidth((byte)j, 0.0);
				}
			}
		}
		for (int j = 0; j < base.Cols.Count; j++)
		{
			string ColRange = ConvertColName(j);
			mySheet.Cells.CreateRange(ColRange + "1", ColRange + (base.Rows.Count + 1)).Style = SwitchStyle2(base.Cols[j]);
			mySheet.AutoFitRow(j);
		}
		myExcel.Save(ssFileName);
		myExcel = null;
		if (F_IsOpenExcelAfterExport)
		{
			OpenFile(ssFileName);
		}
	}

	private void OpenFile(string FileName)
	{
		try
		{
			Process.Start(FileName);
		}
		catch (Exception ex)
		{
			MessageBox.Show("檔案無法開啟！" + ex.Message);
		}
	}

	private void GridBudget_MouseMove(object sender, MouseEventArgs e)
	{
		try
		{
			if (e.Button != MouseButtons.None || !showToolTipOnNarrowColumn || MouseCol <= 0 || MouseRow <= 0 || (object)base.Cols[MouseCol].DataType == Type.GetType("System.Boolean"))
			{
				return;
			}
			int rowIndex = MouseRow;
			int colIndex = MouseCol;
			if (rowIndex == lastRowIndex && colIndex == lastColIndex)
			{
				return;
			}
			lastRowIndex = rowIndex;
			lastColIndex = colIndex;
			Rectangle rectangle = GetCellRect(rowIndex, colIndex, show: false);
			string toolTipText = GetDataDisplay(rowIndex, colIndex);
			using Graphics gridGraphics = CreateGraphics();
			CellStyle style = GetCellStyleDisplay(rowIndex, colIndex);
			float width = gridGraphics.MeasureString(toolTipText, style.Font).Width;
			if (width >= (float)rectangle.Width)
			{
				toolTip.SetToolTip(this, toolTipText);
			}
			else
			{
				toolTip.RemoveAll();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("GridBudget_MouseMove Error:" + ex.Message);
		}
	}

	private string ConvertColName(int ColIndex)
	{
		string RetV = "";
		try
		{
			if (ColIndex <= 25)
			{
				RetV = Convert.ToString((char)(ColIndex + 65));
			}
			else if (ColIndex > 25 && ColIndex % 26 == 0)
			{
				int II1 = ColIndex / 26;
				RetV = Convert.ToString((char)(II1 + 64)) + Convert.ToString((char)(II1 + 64));
			}
			else
			{
				int II1 = ColIndex / 26;
				int II2 = ColIndex % 26;
				RetV = Convert.ToString((char)(II1 + 64)) + Convert.ToString((char)(II2 + 65));
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("ConvertColName Error:" + ex.Message);
		}
		return RetV;
	}

	private string Replicate(string VSTR1, int VLEN)
	{
		string VSTR2 = null;
		try
		{
			for (int j = 0; j < VLEN; j++)
			{
				VSTR2 += VSTR1;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Replicate Error:" + ex.Message);
		}
		return VSTR2;
	}
}
