using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.PccesMain.ArchControls;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.MrsBase;

public class FormCommMrsImport : Form
{
	private DataTable F_ImpData;

	private string F_BeforeEditCode = "";

	private IContainer components = null;

	public GridMrsBase gridMrsBase;

	private Panel panel5;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel6;

	public Panel panel6;

	private GroupBox groupBox3;

	private UltraButton BtnOK;

	private UltraButton BtnCancel;

	private UltraLabel lbl_Count;

	private PictureBox pictureBox1;

	private PictureBox pictureBox2;

	private Button button2;

	private Button button1;

	public DataTable _ImpData
	{
		get
		{
			return F_ImpData;
		}
		set
		{
			F_ImpData = value;
		}
	}

	public FormCommMrsImport()
	{
		InitializeComponent();
		CellStyle cellStyle = gridMrsBase.Styles.Add("EditMode");
		cellStyle.DataType = typeof(Image);
		cellStyle.ImageAlign = ImageAlignEnum.RightCenter;
		CellStyle cellEdited = gridMrsBase.Styles.Add("AfterEdit");
		cellEdited.ForeColor = Color.Red;
		cellEdited.Font = new Font("新細明體", 12f, FontStyle.Bold);
	}

	private void FormCommMrsImport_Load(object sender, EventArgs e)
	{
		F_ImpData.CaseSensitive = true;
		MergerCols();
		BindData();
		ResizeCols();
	}

	private void MergerCols()
	{
		Row row = gridMrsBase.Rows[0];
		bool allowMerging = (gridMrsBase.Rows[1].AllowMerging = true);
		row.AllowMerging = allowMerging;
		GridMrsBase obj = gridMrsBase;
		GridMrsBase obj2 = gridMrsBase;
		object obj3 = (gridMrsBase[0, 2] = "共通及對照性項目");
		obj3 = (obj2[0, 1] = obj3);
		obj[0, 0] = obj3;
		GridMrsBase obj6 = gridMrsBase;
		GridMrsBase obj7 = gridMrsBase;
		GridMrsBase obj8 = gridMrsBase;
		obj3 = (gridMrsBase[0, 8] = "工項基本資料");
		obj3 = (obj8[0, 7] = obj3);
		obj3 = (obj7[0, 6] = obj3);
		obj6[0, 5] = obj3;
		gridMrsBase[0, 3] = "轉入";
		gridMrsBase.SetCellImage(1, gridMrsBase.Cols["Import"].SafeIndex, pictureBox1.Image);
		gridMrsBase[1, 0] = "工項代碼";
		gridMrsBase[1, 1] = "工項名稱";
		gridMrsBase[1, 2] = "單位";
		gridMrsBase[1, 6] = "新碼";
		gridMrsBase[1, 5] = "工項代碼(原)";
		gridMrsBase[1, 7] = "工項名稱";
		gridMrsBase[1, 8] = "單位";
		gridMrsBase.Cols["Method"].Visible = false;
		gridMrsBase.Cols["ImportValue"].Visible = false;
		CellStyle Style01 = gridMrsBase.Styles.Add("CommonStyle01");
		Style01.BackColor = Color.LightPink;
		CellStyle Style2 = gridMrsBase.Styles.Add("Trans01");
		Style2.BackColor = Color.LightYellow;
		gridMrsBase.SetCellStyle(0, 0, Style01);
		gridMrsBase.SetCellStyle(1, 0, Style01);
		gridMrsBase.SetCellStyle(1, 1, Style01);
		gridMrsBase.SetCellStyle(1, 2, Style01);
		gridMrsBase.SetCellStyle(0, 3, Style2);
		gridMrsBase.SetCellStyle(1, 3, Style2);
		CellRange rg = gridMrsBase.GetCellRange(1, gridMrsBase.Cols["ChangeCode"].SafeIndex);
		rg.Style = gridMrsBase.Styles["EditMode"];
		rg.Image = pictureBox2.Image;
		gridMrsBase.Cols[0].AllowSorting = true;
		gridMrsBase.Cols["ChangeCode"].AllowEditing = true;
	}

	private void ResizeCols()
	{
		int iTotalWidth = gridMrsBase.Width;
		int iChangeCodeWidth = 100;
		int iSide = (iTotalWidth - 70 - iChangeCodeWidth) / 2;
		int iPccesCodeWidth = (int)((double)iSide * 0.3);
		int iCNameWidth = (int)((double)iSide * 0.55);
		int iUnitNameWidth = (int)((double)iSide * 0.15);
		gridMrsBase.Cols["pccesCode_Comm"].Width = iPccesCodeWidth;
		gridMrsBase.Cols["pccesCode_Mrs"].Width = iPccesCodeWidth;
		gridMrsBase.Cols["cName_Comm"].Width = iCNameWidth;
		gridMrsBase.Cols["cName_Mrs"].Width = iCNameWidth;
		gridMrsBase.Cols["unitName_Comm"].Width = iUnitNameWidth;
		gridMrsBase.Cols["unitName_Mrs"].Width = iUnitNameWidth;
		gridMrsBase.Cols["ChangeCode"].Width = iChangeCodeWidth;
		gridMrsBase.Cols["Import"].Width = iTotalWidth - iPccesCodeWidth * 2 - iCNameWidth * 2 - iUnitNameWidth * 2 - iChangeCodeWidth;
	}

	private void BindData()
	{
		Cursor = Cursors.WaitCursor;
		gridMrsBase.Redraw = false;
		gridMrsBase.Rows.Count = F_ImpData.Rows.Count + 2;
		for (int i = 0; i < F_ImpData.Rows.Count; i++)
		{
			gridMrsBase[i + 2, "pccesCode_Comm"] = F_ImpData.Rows[i]["pccesCode_Comm"];
			gridMrsBase[i + 2, "cName_Comm"] = F_ImpData.Rows[i]["cName_Comm"];
			gridMrsBase[i + 2, "unitName_Comm"] = F_ImpData.Rows[i]["unitName_Comm"];
			gridMrsBase[i + 2, "pccesCode_Mrs"] = F_ImpData.Rows[i]["pccesCode_Mrs"];
			gridMrsBase[i + 2, "cName_Mrs"] = F_ImpData.Rows[i]["cName_Mrs"];
			gridMrsBase[i + 2, "unitName_Mrs"] = F_ImpData.Rows[i]["unitName_Mrs"];
			gridMrsBase[i + 2, "Import"] = F_ImpData.Rows[i]["Import"];
			gridMrsBase[i + 2, "Method"] = F_ImpData.Rows[i]["Method"];
			if (F_ImpData.Rows[i]["pccesCode_Mrs"] != null && F_ImpData.Rows[i]["pccesCode_Mrs"].ToString() != "")
			{
				F_ImpData.Rows[i]["ChangeCode"] = F_ImpData.Rows[i]["pccesCode_Mrs"];
				gridMrsBase[i + 2, "ChangeCode"] = F_ImpData.Rows[i]["pccesCode_Mrs"];
			}
			if (gridMrsBase[i + 2, "pccesCode_Comm"].ToString() == "")
			{
				gridMrsBase.SetCellCheck(i + 2, gridMrsBase.Cols["Import"].SafeIndex, CheckEnum.TSUnchecked);
			}
			else if (gridMrsBase[i + 2, "pccesCode_Comm"].ToString() != "" && gridMrsBase[i + 2, "pccesCode_Comm"].ToString() == gridMrsBase[i + 2, "pccesCode_Mrs"].ToString() && gridMrsBase[i + 2, "cName_Comm"].ToString() == gridMrsBase[i + 2, "cName_Mrs"].ToString() && gridMrsBase[i + 2, "unitName_Comm"].ToString() == gridMrsBase[i + 2, "unitName_Mrs"].ToString())
			{
				gridMrsBase.SetCellCheck(i + 2, gridMrsBase.Cols["Import"].SafeIndex, CheckEnum.TSUnchecked);
			}
		}
		gridMrsBase.Redraw = true;
		Cursor = Cursors.Default;
		lbl_Count.Text = "資料筆數：" + F_ImpData.Rows.Count;
	}

	private void FormCommMrsImport_ResizeEnd(object sender, EventArgs e)
	{
	}

	private void FormCommMrsImport_Resize(object sender, EventArgs e)
	{
		ResizeCols();
	}

	private void BtnOK_Click(object sender, EventArgs e)
	{
		if (MessageBox.Show(this, "確定轉入勾選項目嗎?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
		{
			return;
		}
		FormProgress FM_Prog = new FormProgress();
		FM_Prog._Min = 0;
		FM_Prog._Max = gridMrsBase.Rows.Count + 1;
		FM_Prog.Message = "資料彙整中...";
		FM_Prog.SetProgressValue(0);
		FM_Prog.Show();
		Cursor = Cursors.WaitCursor;
		for (int i = 2; i < gridMrsBase.Rows.Count; i++)
		{
			if (i % 50 == 0)
			{
				FM_Prog.SetProgressValue(i);
				Application.DoEvents();
				Cursor = Cursors.WaitCursor;
			}
			if ((bool)gridMrsBase[i, "Import"])
			{
				DataRow[] DR = F_ImpData.Select("pccesCode_Comm='" + gridMrsBase.Rows[i]["pccesCode_Comm"].ToString() + "'");
				if (DR.Length > 0)
				{
					DR[0]["Import"] = true;
					if (DR[0]["pccesCode_Mrs"].ToString() == "")
					{
						DR[0]["Method"] = "New";
					}
					else if (gridMrsBase[i, "pccesCode_Mrs"] != null && gridMrsBase[i, "ChangeCode"] != null && gridMrsBase[i, "pccesCode_Mrs"].ToString() != gridMrsBase[i, "ChangeCode"].ToString())
					{
						DR[0]["Method"] = "Change";
						DR[0]["ChangeCode"] = gridMrsBase[i, "ChangeCode"];
					}
				}
			}
			else
			{
				DataRow[] DR = F_ImpData.Select("pccesCode_Comm='" + gridMrsBase.Rows[i]["pccesCode_Comm"].ToString() + "'");
				if (DR.Length > 0)
				{
					DR[0]["Import"] = false;
				}
			}
		}
		FM_Prog.Close();
		FM_Prog.Dispose();
		FM_Prog = null;
		Cursor = Cursors.Default;
		base.DialogResult = DialogResult.OK;
	}

	private void gridMrsBase_AfterEdit(object sender, RowColEventArgs e)
	{
		if (e.Col == gridMrsBase.Cols["Import"].SafeIndex)
		{
			return;
		}
		for (int i = 2; i < gridMrsBase.Rows.Count; i++)
		{
			if (i != e.Row && gridMrsBase[i, "pccesCode_Mrs"].ToString() == gridMrsBase[e.Row, "ChangeCode"].ToString())
			{
				MessageBox.Show(this, "修改後的碼與既有的其他工項衝突，取消這一筆的修改!!", "衝突", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				gridMrsBase[e.Row, "ChangeCode"] = F_BeforeEditCode;
				e.Cancel = true;
				return;
			}
		}
		if (gridMrsBase[e.Row, e.Col].ToString() != gridMrsBase[e.Row, "pccesCode_Mrs"].ToString())
		{
			CellRange rg = gridMrsBase.GetCellRange(e.Row, gridMrsBase.Cols["ChangeCode"].SafeIndex);
			rg.Style = gridMrsBase.Styles["AfterEdit"];
		}
		else if (e.Row % 2 != 0)
		{
			CellRange rg = gridMrsBase.GetCellRange(e.Row, gridMrsBase.Cols["ChangeCode"].SafeIndex);
			rg.Style = gridMrsBase.Styles["Normal"];
		}
		else
		{
			CellRange rg = gridMrsBase.GetCellRange(e.Row, gridMrsBase.Cols["ChangeCode"].SafeIndex);
			rg.Style = gridMrsBase.Styles["Alternate"];
		}
	}

	private void gridMrsBase_BeforeEdit(object sender, RowColEventArgs e)
	{
		if (gridMrsBase[e.Row, "pccesCode_Comm"].ToString() == "" && e.Col == gridMrsBase.Cols["Import"].SafeIndex)
		{
			e.Cancel = true;
		}
		if (gridMrsBase[e.Row, "pccesCode_Comm"].ToString() != "" && gridMrsBase[e.Row, "pccesCode_Comm"].ToString() == gridMrsBase[e.Row, "pccesCode_Mrs"].ToString() && gridMrsBase[e.Row, "cName_Comm"].ToString() == gridMrsBase[e.Row, "cName_Mrs"].ToString() && gridMrsBase[e.Row, "unitName_Comm"].ToString() == gridMrsBase[e.Row, "unitName_Mrs"].ToString())
		{
			e.Cancel = true;
		}
	}

	private void gridMrsBase_StartEdit(object sender, RowColEventArgs e)
	{
		F_BeforeEditCode = gridMrsBase[e.Row, e.Col].ToString();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		for (int i = 2; i < gridMrsBase.Rows.Count; i++)
		{
			if (!(gridMrsBase[i, "pccesCode_Comm"].ToString() == "") && (!(gridMrsBase[i, "pccesCode_Comm"].ToString() != "") || !(gridMrsBase[i, "pccesCode_Comm"].ToString() == gridMrsBase[i, "pccesCode_Mrs"].ToString()) || !(gridMrsBase[i, "cName_Comm"].ToString() == gridMrsBase[i, "cName_Mrs"].ToString()) || !(gridMrsBase[i, "unitName_Comm"].ToString() == gridMrsBase[i, "unitName_Mrs"].ToString())))
			{
				gridMrsBase[i, gridMrsBase.Cols["Import"].SafeIndex] = true;
			}
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
		for (int i = 2; i < gridMrsBase.Rows.Count; i++)
		{
			gridMrsBase[i, gridMrsBase.Cols["Import"].SafeIndex] = false;
		}
	}

	private void BtnCancel_Click(object sender, EventArgs e)
	{
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
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.MrsBase.FormCommMrsImport));
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		this.panel5 = new System.Windows.Forms.Panel();
		this.button2 = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.panel6 = new System.Windows.Forms.Panel();
		this.pictureBox2 = new System.Windows.Forms.PictureBox();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.lbl_Count = new Infragistics.Win.Misc.UltraLabel();
		this.BtnCancel = new Infragistics.Win.Misc.UltraButton();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.BtnOK = new Infragistics.Win.Misc.UltraButton();
		this.gridMrsBase = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.panel5.SuspendLayout();
		this.panel6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase).BeginInit();
		base.SuspendLayout();
		this.panel5.BackColor = System.Drawing.Color.White;
		this.panel5.Controls.Add(this.button2);
		this.panel5.Controls.Add(this.button1);
		this.panel5.Controls.Add(this.ultraLabel7);
		this.panel5.Controls.Add(this.ultraLabel6);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(792, 60);
		this.panel5.TabIndex = 21;
		this.button2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.button2.Location = new System.Drawing.Point(712, 31);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(75, 23);
		this.button2.TabIndex = 5;
		this.button2.Text = "全不選";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.button1.Location = new System.Drawing.Point(712, 5);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 4;
		this.button1.Text = "全選";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		appearance1.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance1;
		this.ultraLabel7.Location = new System.Drawing.Point(47, 34);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(519, 20);
		this.ultraLabel7.TabIndex = 3;
		this.ultraLabel7.Text = "你可以點擊[轉入]欄位來選定是否轉入";
		appearance2.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance2;
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(16, 12);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 2;
		this.ultraLabel6.Text = "匯入共通項目";
		this.panel6.AutoSize = true;
		this.panel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panel6.Controls.Add(this.pictureBox2);
		this.panel6.Controls.Add(this.pictureBox1);
		this.panel6.Controls.Add(this.lbl_Count);
		this.panel6.Controls.Add(this.BtnCancel);
		this.panel6.Controls.Add(this.groupBox3);
		this.panel6.Controls.Add(this.BtnOK);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel6.Location = new System.Drawing.Point(0, 500);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(792, 43);
		this.panel6.TabIndex = 22;
		this.pictureBox2.Image = (System.Drawing.Image)resources.GetObject("pictureBox2.Image");
		this.pictureBox2.Location = new System.Drawing.Point(401, 14);
		this.pictureBox2.Name = "pictureBox2";
		this.pictureBox2.Size = new System.Drawing.Size(17, 17);
		this.pictureBox2.TabIndex = 24;
		this.pictureBox2.TabStop = false;
		this.pictureBox2.Visible = false;
		this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		this.pictureBox1.Location = new System.Drawing.Point(378, 14);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(17, 17);
		this.pictureBox1.TabIndex = 23;
		this.pictureBox1.TabStop = false;
		this.pictureBox1.Visible = false;
		appearance3.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.lbl_Count.Appearance = appearance3;
		this.lbl_Count.Location = new System.Drawing.Point(12, 14);
		this.lbl_Count.Name = "lbl_Count";
		this.lbl_Count.Size = new System.Drawing.Size(164, 20);
		this.lbl_Count.TabIndex = 5;
		this.lbl_Count.Text = "資料筆數：";
		this.BtnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance4.Image = resources.GetObject("appearance4.Image");
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnCancel.Appearance = appearance4;
		this.BtnCancel.BackColor = System.Drawing.SystemColors.Control;
		this.BtnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.BtnCancel.Font = new System.Drawing.Font("細明體", 11f);
		this.BtnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnCancel.Location = new System.Drawing.Point(692, 9);
		this.BtnCancel.Name = "BtnCancel";
		this.BtnCancel.ShowFocusRect = false;
		this.BtnCancel.ShowOutline = false;
		this.BtnCancel.Size = new System.Drawing.Size(88, 31);
		this.BtnCancel.SupportThemes = false;
		this.BtnCancel.TabIndex = 4;
		this.BtnCancel.Text = "取消";
		this.BtnCancel.Click += new System.EventHandler(BtnCancel_Click);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox3.Location = new System.Drawing.Point(0, 0);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(792, 8);
		this.groupBox3.TabIndex = 3;
		this.groupBox3.TabStop = false;
		this.BtnOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance5.Image = resources.GetObject("appearance5.Image");
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnOK.Appearance = appearance5;
		this.BtnOK.BackColor = System.Drawing.SystemColors.Control;
		this.BtnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.BtnOK.Font = new System.Drawing.Font("細明體", 11f);
		this.BtnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnOK.Location = new System.Drawing.Point(575, 9);
		this.BtnOK.Name = "BtnOK";
		this.BtnOK.ShowFocusRect = false;
		this.BtnOK.ShowOutline = false;
		this.BtnOK.Size = new System.Drawing.Size(113, 31);
		this.BtnOK.SupportThemes = false;
		this.BtnOK.TabIndex = 1;
		this.BtnOK.Text = "確定轉入";
		this.BtnOK.Click += new System.EventHandler(BtnOK_Click);
		this.gridMrsBase._ExcelFileName = "";
		this.gridMrsBase._ExcelSheeName = "";
		this.gridMrsBase._IsOpenExcelAfterExport = false;
		this.gridMrsBase.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free;
		this.gridMrsBase.AutoResize = false;
		this.gridMrsBase.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridMrsBase.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridMrsBase.ColumnInfo = resources.GetString("gridMrsBase.ColumnInfo");
		this.gridMrsBase.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridMrsBase.ExtendLastCol = true;
		this.gridMrsBase.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridMrsBase.ForeColor = System.Drawing.Color.Black;
		this.gridMrsBase.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.gridMrsBase.IsProcessUndo = false;
		this.gridMrsBase.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.gridMrsBase.Location = new System.Drawing.Point(0, 60);
		this.gridMrsBase.Name = "gridMrsBase";
		this.gridMrsBase.Rows.Fixed = 2;
		this.gridMrsBase.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridMrsBase.ShowCursor = true;
		this.gridMrsBase.ShowToolTipOnNarrowColumn = true;
		this.gridMrsBase.Size = new System.Drawing.Size(792, 440);
		this.gridMrsBase.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridMrsBase.Styles"));
		this.gridMrsBase.TabIndex = 11;
		this.gridMrsBase.UndoMax = 10;
		this.gridMrsBase.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridMrsBase_StartEdit);
		this.gridMrsBase.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridMrsBase_AfterEdit);
		this.gridMrsBase.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridMrsBase_BeforeEdit);
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.BtnCancel;
		base.ClientSize = new System.Drawing.Size(792, 543);
		base.Controls.Add(this.gridMrsBase);
		base.Controls.Add(this.panel6);
		base.Controls.Add(this.panel5);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Margin = new System.Windows.Forms.Padding(4);
		base.Name = "FormCommMrsImport";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "匯入共通項目";
		base.Load += new System.EventHandler(FormCommMrsImport_Load);
		base.Resize += new System.EventHandler(FormCommMrsImport_Resize);
		base.ResizeEnd += new System.EventHandler(FormCommMrsImport_ResizeEnd);
		this.panel5.ResumeLayout(false);
		this.panel6.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
