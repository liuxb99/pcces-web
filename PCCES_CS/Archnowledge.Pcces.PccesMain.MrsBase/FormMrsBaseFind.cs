using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.MrsBase;

public class FormMrsBaseFind : Form
{
	private string F_KeyWord = "";

	private Label label1;

	private UltraButton ultraButton1;

	private Label label2;

	private UltraButton ultraButton2;

	private Label label3;

	public UltraComboEditor cboFind_Cols;

	public UltraComboEditor cboFind_Locway;

	private UltraComboEditor cbFind;

	private UltraCheckEditor chkIsCaseSensitive;

	private Container components = null;

	public FormMrsBaseFind()
	{
		InitializeComponent();
	}

	private void ultraButton2_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		if ((base.Owner as frmMrsBase).gridMrsBase1.Rows.Count > 1)
		{
			int iStart = (base.Owner as frmMrsBase).gridMrsBase1.Row + 1;
			if (F_KeyWord != cbFind.Text.Trim())
			{
				iStart = 1;
				F_KeyWord = cbFind.Text.Trim();
			}
			string FieldName = cboFind_Cols.SelectedItem.DataValue.ToString().Trim();
			string FindKind = (string)cboFind_Locway.SelectedItem.DataValue;
			(base.Owner as frmMrsBase).Do_Find2(F_KeyWord, FieldName, FindKind);
		}
	}

	private void ultraButton1_Click_Tmp(object sender, EventArgs e)
	{
		bool IFindWay = false;
		int iFind = -1;
		int iStart = (base.Owner as frmMrsBase).gridMrsBase1.Row + 1;
		if (F_KeyWord != cbFind.Text.Trim())
		{
			iStart = 1;
			F_KeyWord = cbFind.Text.Trim();
		}
		int iColLookup = (base.Owner as frmMrsBase).gridMrsBase1.Cols[cboFind_Cols.SelectedItem.DataValue.ToString()].SafeIndex;
		string FindKind = (string)cboFind_Locway.SelectedItem.DataValue;
		if (FindKind == "FULL" || FindKind == "PREFIX")
		{
			if ((string)cboFind_Locway.SelectedItem.DataValue == "FULL")
			{
				IFindWay = true;
			}
			if ((string)cboFind_Locway.SelectedItem.DataValue == "PREFIX")
			{
				IFindWay = false;
			}
			iFind = (base.Owner as frmMrsBase).gridMrsBase1.FindRow(cbFind.Text.Trim(), iStart, iColLookup, chkIsCaseSensitive.Checked, IFindWay, wrap: false);
		}
		else if (FindKind == "PARTIAL")
		{
			for (int i = iStart; i < (base.Owner as frmMrsBase).gridMrsBase1.Rows.Count; i++)
			{
				iFind = (base.Owner as frmMrsBase).gridMrsBase1[i, iColLookup].ToString().IndexOf(cbFind.Text.Trim());
				if (iFind > -1)
				{
					iFind = i;
					break;
				}
			}
		}
		if (iFind > -1)
		{
			(base.Owner as frmMrsBase).gridMrsBase1.Row = iFind;
			(base.Owner as frmMrsBase).gridMrsBase1.Select();
			(base.Owner as frmMrsBase).gridMrsBase1.TopRow = iFind;
			bool IsExist = false;
			for (int i = 0; i < cbFind.Items.Count; i++)
			{
				if (cbFind.Items[i].DisplayText.Trim() == cbFind.Text.Trim())
				{
					IsExist = true;
					break;
				}
			}
			if (!IsExist)
			{
				cbFind.Items.Add(cbFind.Text.Trim());
			}
		}
		else if (iFind == -1)
		{
			MessageBox.Show("已完成搜尋資料，找不到搜尋目標。", "尋找", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void cbFind_ValueChanged(object sender, EventArgs e)
	{
		if (cbFind.Text.Trim() == "")
		{
			ultraButton1.Enabled = false;
		}
		else
		{
			ultraButton1.Enabled = true;
		}
	}

	private void cbFind_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ultraButton1_Click(this, EventArgs.Empty);
		}
	}

	private void FormMrsBaseFind_FormClosing(object sender, FormClosingEventArgs e)
	{
		(base.Owner as frmMrsBase).ultraToolbarsManager1.Tools["mnuWork_Delete"].SharedProps.Shortcut = Shortcut.Del;
	}

	private void FormMrsBaseFind_Activated(object sender, EventArgs e)
	{
		(base.Owner as frmMrsBase).ultraToolbarsManager1.Tools["mnuWork_Delete"].SharedProps.Shortcut = Shortcut.None;
	}

	private void cbFind_Validating(object sender, CancelEventArgs e)
	{
		try
		{
			if (cbFind.Text.Length > 0 && !CommonMethods.CheckValidString(cbFind.Text))
			{
				e.Cancel = true;
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBaseFind.cs" + ex.Message);
		}
	}

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
		this.cbFind = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.label1 = new System.Windows.Forms.Label();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.label2 = new System.Windows.Forms.Label();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.label3 = new System.Windows.Forms.Label();
		this.chkIsCaseSensitive = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.cboFind_Cols = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.cboFind_Locway = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)this.cbFind).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboFind_Cols).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboFind_Locway).BeginInit();
		base.SuspendLayout();
		this.cbFind.Location = new System.Drawing.Point(108, 13);
		this.cbFind.Name = "cbFind";
		this.cbFind.Size = new System.Drawing.Size(228, 21);
		this.cbFind.TabIndex = 0;
		this.cbFind.Text = null;
		this.cbFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(cbFind_KeyPress);
		this.cbFind.Validating += new System.ComponentModel.CancelEventHandler(cbFind_Validating);
		this.cbFind.ValueChanged += new System.EventHandler(cbFind_ValueChanged);
		this.label1.Location = new System.Drawing.Point(16, 15);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(88, 17);
		this.label1.TabIndex = 1;
		this.label1.Text = "尋找目標(&N)：";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton1.Location = new System.Drawing.Point(376, 11);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(112, 26);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 2;
		this.ultraButton1.Text = "尋找下一筆(&F)";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		this.label2.Location = new System.Drawing.Point(16, 42);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(88, 16);
		this.label2.TabIndex = 3;
		this.label2.Text = "查詢(&L)：";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton2.Location = new System.Drawing.Point(376, 40);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(112, 26);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 5;
		this.ultraButton2.Text = "取消";
		this.ultraButton2.Click += new System.EventHandler(ultraButton2_Click);
		this.label3.Location = new System.Drawing.Point(16, 68);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(88, 16);
		this.label3.TabIndex = 6;
		this.label3.Text = "符合(&H)：";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.chkIsCaseSensitive.Enabled = false;
		this.chkIsCaseSensitive.Location = new System.Drawing.Point(108, 92);
		this.chkIsCaseSensitive.Name = "chkIsCaseSensitive";
		this.chkIsCaseSensitive.Size = new System.Drawing.Size(156, 20);
		this.chkIsCaseSensitive.TabIndex = 8;
		this.chkIsCaseSensitive.Text = "大小寫須符合(&C)";
		this.chkIsCaseSensitive.UseMnemonics = true;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.cboFind_Cols.Appearance = appearance1;
		this.cboFind_Cols.Location = new System.Drawing.Point(108, 39);
		this.cboFind_Cols.Name = "cboFind_Cols";
		this.cboFind_Cols.Size = new System.Drawing.Size(152, 21);
		this.cboFind_Cols.TabIndex = 9;
		this.cboFind_Cols.Text = null;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.cboFind_Locway.Appearance = appearance2;
		valueListItem1.DataValue = "FULL";
		valueListItem1.DisplayText = "整個欄位";
		valueListItem2.DataValue = "PREFIX";
		valueListItem2.DisplayText = "欄位的開頭";
		valueListItem3.DataValue = "PARTIAL";
		valueListItem3.DisplayText = "欄位的任何部份";
		this.cboFind_Locway.Items.Add(valueListItem1);
		this.cboFind_Locway.Items.Add(valueListItem2);
		this.cboFind_Locway.Items.Add(valueListItem3);
		this.cboFind_Locway.Location = new System.Drawing.Point(108, 64);
		this.cboFind_Locway.Name = "cboFind_Locway";
		this.cboFind_Locway.Size = new System.Drawing.Size(152, 21);
		this.cboFind_Locway.TabIndex = 10;
		this.cboFind_Locway.Text = "欄位開頭";
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
		base.CancelButton = this.ultraButton2;
		base.ClientSize = new System.Drawing.Size(498, 119);
		base.Controls.Add(this.cboFind_Locway);
		base.Controls.Add(this.cboFind_Cols);
		base.Controls.Add(this.chkIsCaseSensitive);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.ultraButton2);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.ultraButton1);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.cbFind);
		this.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.KeyPreview = true;
		base.Name = "FormMrsBaseFind";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "尋找";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormMrsBaseFind_FormClosing);
		base.Activated += new System.EventHandler(FormMrsBaseFind_Activated);
		((System.ComponentModel.ISupportInitialize)this.cbFind).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboFind_Cols).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboFind_Locway).EndInit();
		base.ResumeLayout(false);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}
}
