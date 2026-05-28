#define DEBUG
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.ShellLib;
using Aspose.Cells;
using C1.Win.C1FlexGrid;

namespace Archnowledge.Pcces.PccesMain.ArchControls;

public class GridMrsBase : C1FlexGrid
{
	private Label _lbl;

	private Container components = null;

	private object[] UndoStack;

	private object[] UndoTemp;

	private string sUndo = string.Empty;

	private int iCurrRow = 0;

	private int iCurrCol = 0;

	private string sColName = "";

	private string GRIDMode = "NOR";

	private int iUndoIndex = 0;

	private int iUsedIndex = 0;

	private bool F_IsProcessUndo = false;

	private string F_ExcelSheeName = "";

	private string F_ExcelFileName = "";

	private bool F_IsOpenExcelAfterExport = false;

	private int F_SelectedItems = 0;

	private int F_UndoMax = 2;

	private bool F_ToolTipShow = true;

	private int _lastRow;

	private int _lastCol;

	public string _ExcelSheeName
	{
		get
		{
			return F_ExcelSheeName;
		}
		set
		{
			F_ExcelSheeName = value;
		}
	}

	public string _ExcelFileName
	{
		get
		{
			return F_ExcelFileName;
		}
		set
		{
			F_ExcelFileName = value;
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

	public bool IsProcessUndo
	{
		get
		{
			return F_IsProcessUndo;
		}
		set
		{
			F_IsProcessUndo = value;
		}
	}

	public int UndoMax
	{
		get
		{
			return F_UndoMax;
		}
		set
		{
			F_UndoMax = value;
			UndoStack = new object[F_UndoMax];
			UndoTemp = new object[F_UndoMax];
		}
	}

	public bool ShowToolTipOnNarrowColumn
	{
		get
		{
			return F_ToolTipShow;
		}
		set
		{
			F_ToolTipShow = value;
		}
	}

	public int SelectedItems
	{
		get
		{
			Cal_SelectedRows();
			return F_SelectedItems;
		}
	}

	public bool CanUndo
	{
		get
		{
			if (iUndoIndex > 0)
			{
				return true;
			}
			return false;
		}
	}

	public bool CanRedo
	{
		get
		{
			if (!F_IsProcessUndo)
			{
				return false;
			}
			if (iUndoIndex >= 0 && iUndoIndex != iUsedIndex)
			{
				return true;
			}
			return false;
		}
	}

	private void Cal_SelectedRows()
	{
		F_SelectedItems = 0;
		for (int i = 1; i < base.Rows.Count; i++)
		{
			if (base.Rows[i].Selected)
			{
				F_SelectedItems++;
			}
		}
	}

	public GridMrsBase(IContainer container)
	{
		container.Add(this);
		InitializeComponent();
		UndoStack = new object[F_UndoMax];
		UndoTemp = new object[F_UndoMax];
	}

	public GridMrsBase()
	{
		try
		{
			InitializeComponent();
			UndoStack = new object[F_UndoMax];
			UndoTemp = new object[F_UndoMax];
		}
		catch (Exception ex)
		{
			MessageBox.Show("BindToGrid Error:" + ex.Message);
		}
	}

	protected override void Dispose(bool disposing)
	{
		try
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}
		catch (Exception ex)
		{
			MessageBox.Show("BindToGrid Error:" + ex.Message);
		}
	}

	public void Undo()
	{
		try
		{
			if (F_IsProcessUndo && iUndoIndex > 0)
			{
				iUndoIndex--;
				string[] s = UndoStack[iUndoIndex].ToString().Split('|');
				int iRow = Convert.ToInt32(s[1]);
				string sCol = s[2].Trim();
				Select(iRow, base.Cols[sCol].SafeIndex);
				base[iRow, base.Cols[sCol].SafeIndex] = s[3];
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("BindToGrid Error:" + ex.Message);
		}
	}

	public void Redo()
	{
		try
		{
			if (F_IsProcessUndo)
			{
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("BindToGrid Error:" + ex.Message);
		}
	}

	private void InitializeComponent()
	{
		try
		{
			((System.ComponentModel.ISupportInitialize)this).BeginInit();
			base.MouseMove += new System.Windows.Forms.MouseEventHandler(GridMrsBase_MouseMove);
			base.LeaveCell += new System.EventHandler(GridMrsBase_LeaveCell);
			base.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(GridMrsBase_StartEdit);
			((System.ComponentModel.ISupportInitialize)this).EndInit();
		}
		catch (System.Exception ex)
		{
			System.Windows.Forms.MessageBox.Show("BindToGrid Error:" + ex.Message);
		}
	}

	public bool ExecuteExport(c1GridExportType ExpType)
	{
		bool RetV = true;
		try
		{
			if (ExpType == c1GridExportType.Excel)
			{
				Do_ExcelExport();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("BindToGrid Error:" + ex.Message);
		}
		return RetV;
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
		AsposeStyle2_1.Custom = "_-* #,##0_-;-* #,##0_-;_-* \"-\"_-;_-@_-";
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

	private void Do_ExcelExport()
	{
		try
		{
			string ssFileName = ((F_ExcelFileName != "") ? F_ExcelFileName : ("Excel_" + $"{DateTime.Now:yyyyMMddHHmmss}"));
			Aspose.Cells.License license = new Aspose.Cells.License();
			license.SetLicense("Aspose.Custom.lic");
			Excel myExcel = new Excel();
			myExcel.Worksheets.Add();
			Worksheet mySheet = myExcel.Worksheets[0];
			Cells myCells = mySheet.Cells;
			if (F_ExcelSheeName != "")
			{
				mySheet.Name = F_ExcelSheeName;
			}
			for (int i = 0; i < base.Rows.Count; i++)
			{
				int visibleColumn = 0;
				for (int j = 0; j < base.Cols.Count; j++)
				{
					if (base.Cols[j].Caption.Trim() != "" && base.Cols[j].Visible)
					{
						mySheet.Cells[i, visibleColumn].PutValue((base[i, j] != null) ? base[i, j] : "");
						visibleColumn++;
					}
				}
			}
			int visibleColumn2 = 0;
			for (int j = 0; j < base.Cols.Count; j++)
			{
				if (base.Cols[j].Caption.Trim() != "" && base.Cols[j].Visible)
				{
					string ColRange = ConvertColName(visibleColumn2);
					mySheet.Cells.CreateRange(ColRange + "1", ColRange + (base.Rows.Count + 1)).Style = SwitchStyle(base.Cols[j]);
					mySheet.AutoFitColumn(visibleColumn2);
					visibleColumn2++;
				}
			}
			myExcel.Save(ssFileName);
			myExcel = null;
			if (F_IsOpenExcelAfterExport)
			{
				ShellExc(ssFileName);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("BindToGrid Error:" + ex.Message);
		}
	}

	private void ShellExc(string sFileName)
	{
		try
		{
			ShellExecute SHExe = new ShellExecute();
			SHExe.OwnerHandle = base.Handle;
			SHExe.Path = sFileName;
			SHExe.Execute();
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "ArchControls.GridMrsBase.cs--> ShellExc()" + ex.Message);
			MessageBox.Show("檔案無法開啟\n\n" + ex.Message);
		}
	}

	private void GridMrsBase_StartEdit(object sender, RowColEventArgs e)
	{
		try
		{
			if (F_IsProcessUndo)
			{
				GRIDMode = "EDT";
				iCurrRow = e.Row;
				iCurrCol = e.Col;
				sColName = base.Cols[e.Col].Name;
				sUndo = "EDIT|";
				sUndo = sUndo + iCurrRow + "|";
				sUndo = sUndo + sColName + "|";
				if (base[iCurrRow, sColName] != null)
				{
					sUndo = sUndo + base[iCurrRow, sColName].ToString() + "|";
				}
				else
				{
					sUndo += "|";
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("BindToGrid Error:" + ex.Message);
		}
	}

	private void GridMrsBase_LeaveCell(object sender, EventArgs e)
	{
		try
		{
			if (!F_IsProcessUndo || !(GRIDMode == "EDT"))
			{
				return;
			}
			if (iUndoIndex == F_UndoMax - 1)
			{
				UndoStack.CopyTo(UndoTemp, 0);
				for (int i = 0; i < UndoTemp.Length - 1; i++)
				{
					UndoStack[i] = UndoTemp[i + 1];
				}
				if (base[iCurrRow, base.Cols[iCurrCol].Name] != null)
				{
					sUndo += base[iCurrRow, base.Cols[iCurrCol].Name].ToString();
				}
				else
				{
					sUndo += "";
				}
				UndoStack[iUndoIndex - 1] = sUndo;
			}
			else
			{
				if (base[iCurrRow, base.Cols[iCurrCol].Name] != null)
				{
					sUndo += base[iCurrRow, base.Cols[iCurrCol].Name].ToString();
				}
				else
				{
					sUndo += "";
				}
				UndoStack[iUndoIndex] = sUndo;
				iUndoIndex++;
			}
			if (iUndoIndex >= iUsedIndex)
			{
				iUsedIndex = iUndoIndex;
			}
			GRIDMode = "NOR";
		}
		catch (Exception ex)
		{
			MessageBox.Show("BindToGrid Error:" + ex.Message);
		}
	}

	public void ClearUndo()
	{
		iUndoIndex = 0;
		iUsedIndex = 0;
	}

	private void GridMrsBase_MouseMove(object sender, MouseEventArgs e)
	{
		try
		{
			Debug.WriteLine(base.Name + "_GridMrsBase_MouseMove： [MouseRow,MouseCol] = [" + MouseRow + "," + MouseCol + "]");
			string text = null;
			CellStyle s = null;
			Rectangle rc = Rectangle.Empty;
			float width = 0f;
			if (e.Button != MouseButtons.None || !F_ToolTipShow)
			{
				return;
			}
			int row = MouseRow;
			int col = MouseCol;
			if ((row == _lastRow && col == _lastCol) || base.Rows.Fixed - 1 == row || (object)base.Cols[col].DataType == Type.GetType("System.Boolean"))
			{
				return;
			}
			_lastRow = row;
			_lastCol = col;
			if (row > -1 && col > -1)
			{
				text = GetDataDisplay(row, col);
				rc = GetCellRect(row, col, show: false);
				rc.Intersect(base.ClientRectangle);
				using Graphics g = CreateGraphics();
				s = GetCellStyleDisplay(row, col);
				width = g.MeasureString(text, s.Font).Width;
				width += (float)(s.Margins.Left + s.Margins.Right + s.Border.Width);
				if (width < (float)rc.Width)
				{
					text = null;
				}
			}
			if (text != null && _lbl == null)
			{
				_lbl = new Label();
				_lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
				_lbl.BackColor = SystemColors.Info;
				_lbl.ForeColor = SystemColors.InfoText;
				_lbl.TextAlign = ContentAlignment.MiddleLeft;
				_lbl.Click += _lbl_Click;
				base.Controls.Add(_lbl);
			}
			if (_lbl != null)
			{
				_lbl.Visible = false;
				if (text != null)
				{
					rc.Width = (int)width;
					_lbl.Text = text;
					_lbl.Bounds = rc;
					_lbl.Font = s.Font;
					_lbl.Visible = F_ToolTipShow;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("GridMrsBase_MouseMove Error:" + ex.Message);
		}
	}

	private void _lbl_Click(object sender, EventArgs e)
	{
		try
		{
			_lastRow = MouseRow;
			_lastCol = MouseCol;
			if (_lastRow > 0 && _lastCol > 0)
			{
				Select(_lastRow, _lastCol);
			}
			_lbl.Visible = false;
		}
		catch (Exception ex)
		{
			MessageBox.Show("_lbl_Click Error:" + ex.Message);
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
			MessageBox.Show("BindToGrid Error:" + ex.Message);
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
			MessageBox.Show("BindToGrid Error:" + ex.Message);
		}
		return VSTR2;
	}
}
