using System;
using System.ComponentModel;
using C1.Win.C1FlexGrid;

namespace Archnowledge.Pcces.PccesMain.ArchControls;

public class ArchFlexGrid : C1FlexGrid
{
	private Container components = null;

	private object[] UndoStack;

	private object[] UndoTemp;

	private string sUndo = string.Empty;

	private int iCurrRow = 0;

	private int iCurrCol = 0;

	private string GRIDMode = "NOR";

	private int iUndoIndex = 0;

	private int iUsedIndex = 0;

	private int F_SelectedItems = 0;

	private int F_UndoMax = 10;

	public int SelectedItems
	{
		get
		{
			Cal_SelectedRows();
			return F_SelectedItems;
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
			if (iUndoIndex >= 0 && iUndoIndex != iUsedIndex)
			{
				return true;
			}
			return false;
		}
	}

	public ArchFlexGrid(IContainer container)
	{
		container.Add(this);
		InitializeComponent();
		UndoStack = new object[F_UndoMax];
		UndoTemp = new object[F_UndoMax];
	}

	public ArchFlexGrid()
	{
		InitializeComponent();
		UndoStack = new object[F_UndoMax];
		UndoTemp = new object[F_UndoMax];
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		((System.ComponentModel.ISupportInitialize)this).BeginInit();
		base.LeaveCell += new System.EventHandler(ArchFlexGrid_LeaveCell);
		base.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(ArchFlexGrid_StartEdit);
		((System.ComponentModel.ISupportInitialize)this).EndInit();
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

	public virtual void Undo()
	{
		if (iUndoIndex > 0)
		{
			iUndoIndex--;
			string[] s = UndoStack[iUndoIndex].ToString().Split('|');
			int iRow = Convert.ToInt32(s[1]);
			int iCol = Convert.ToInt32(s[2]);
			Select(iRow, iCol);
			base[iRow, iCol] = s[3];
		}
	}

	public virtual void Redo()
	{
		if (iUndoIndex >= 0 && iUndoIndex <= F_UndoMax && iUndoIndex < iUsedIndex)
		{
			string[] s = UndoStack[iUndoIndex].ToString().Split('|');
			int iRow = Convert.ToInt32(s[1]);
			int iCol = Convert.ToInt32(s[2]);
			Select(iRow, iCol);
			base[iRow, iCol] = s[4];
			iUndoIndex++;
		}
	}

	private void ArchFlexGrid_StartEdit(object sender, RowColEventArgs e)
	{
		GRIDMode = "EDT";
		iCurrRow = e.Row;
		iCurrCol = e.Col;
		sUndo = "EDIT|";
		sUndo = sUndo + e.Row + "|";
		sUndo = sUndo + e.Col + "|";
		if (base[e.Row, e.Col] != null)
		{
			sUndo = sUndo + base[e.Row, e.Col].ToString() + "|";
		}
		else
		{
			sUndo += "|";
		}
	}

	private void ArchFlexGrid_LeaveCell(object sender, EventArgs e)
	{
		if (!(GRIDMode == "EDT"))
		{
			return;
		}
		if (iUndoIndex == F_UndoMax)
		{
			UndoStack.CopyTo(UndoTemp, 0);
			for (int i = 0; i < UndoTemp.Length - 1; i++)
			{
				UndoStack[i] = UndoTemp[i + 1];
			}
			sUndo += base[iCurrRow, iCurrCol].ToString();
			UndoStack[iUndoIndex - 1] = sUndo;
		}
		else
		{
			sUndo += base[iCurrRow, iCurrCol].ToString();
			UndoStack[iUndoIndex] = sUndo;
			iUndoIndex++;
		}
		if (iUndoIndex >= iUsedIndex)
		{
			iUsedIndex = iUndoIndex;
		}
		GRIDMode = "NOR";
	}
}
