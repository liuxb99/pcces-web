using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.Bid;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.DomainModule.LogicalBase;
using Archnowledge.Pcces.DomainModule.Sub;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinProgressBar;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetCombine : Form
{
	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_A;

	private UltraTabPageControl Tab_B;

	private Panel panel4;

	private Panel panel7;

	private UltraButton ultraButton5;

	private UltraButton ultraButton4;

	private UltraButton ultraButton1;

	private UltraButton BtnAll;

	private Panel panel5;

	private GridBudget GridSource;

	private UltraLabel ultraLabel3;

	private Panel panel10;

	private Panel panel6;

	private GridBudget GridDestination;

	private Panel panel8;

	private UltraButton ultraButton3;

	private UltraButton ultraButton2;

	private UltraLabel ultraLabel4;

	private Panel panel9;

	private Panel panel3;

	private UltraLabel ultraLabel1;

	private Panel panel2;

	private UltraButton ultraButton6;

	private UltraButton A_Btn_Cncl;

	private Panel panel1;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel5;

	private Panel pnl_Proc;

	private UltraProgressBar Prog1;

	private IContainer components;

	private string F_UserID;

	private string F_ProjectCode;

	private PccesFormAction F_ActionName;

	public string _UserID
	{
		get
		{
			return F_UserID;
		}
		set
		{
			F_UserID = value;
		}
	}

	public PccesFormAction _ActionName
	{
		get
		{
			return F_ActionName;
		}
		set
		{
			F_ActionName = value;
		}
	}

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

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetCombine));
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel4 = new System.Windows.Forms.Panel();
		this.panel7 = new System.Windows.Forms.Panel();
		this.ultraButton5 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton4 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.BtnAll = new Infragistics.Win.Misc.UltraButton();
		this.panel6 = new System.Windows.Forms.Panel();
		this.GridDestination = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.panel8 = new System.Windows.Forms.Panel();
		this.ultraButton3 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.panel5 = new System.Windows.Forms.Panel();
		this.GridSource = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.panel10 = new System.Windows.Forms.Panel();
		this.panel9 = new System.Windows.Forms.Panel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.ultraButton6 = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.pnl_Proc = new System.Windows.Forms.Panel();
		this.Prog1 = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.Tab_A.SuspendLayout();
		this.panel4.SuspendLayout();
		this.panel7.SuspendLayout();
		this.panel6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridDestination).BeginInit();
		this.panel8.SuspendLayout();
		this.panel5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridSource).BeginInit();
		this.panel3.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel1.SuspendLayout();
		this.Tab_B.SuspendLayout();
		this.pnl_Proc.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.panel4);
		this.Tab_A.Controls.Add(this.panel3);
		this.Tab_A.Controls.Add(this.panel2);
		this.Tab_A.Controls.Add(this.panel1);
		this.Tab_A.Location = new System.Drawing.Point(0, 0);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(782, 541);
		this.panel4.Controls.Add(this.panel7);
		this.panel4.Controls.Add(this.panel6);
		this.panel4.Controls.Add(this.panel5);
		this.panel4.Controls.Add(this.panel10);
		this.panel4.Controls.Add(this.panel9);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel4.Location = new System.Drawing.Point(0, 40);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(782, 417);
		this.panel4.TabIndex = 7;
		this.panel7.Controls.Add(this.ultraButton5);
		this.panel7.Controls.Add(this.ultraButton4);
		this.panel7.Controls.Add(this.ultraButton1);
		this.panel7.Controls.Add(this.BtnAll);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel7.Location = new System.Drawing.Point(345, 0);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(92, 417);
		this.panel7.TabIndex = 2;
		appearance1.FontData.SizeInPoints = 9f;
		this.ultraButton5.Appearance = appearance1;
		this.ultraButton5.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton5.Location = new System.Drawing.Point(4, 198);
		this.ultraButton5.Name = "ultraButton5";
		this.ultraButton5.ShowFocusRect = false;
		this.ultraButton5.ShowOutline = false;
		this.ultraButton5.Size = new System.Drawing.Size(85, 30);
		this.ultraButton5.SupportThemes = false;
		this.ultraButton5.TabIndex = 4;
		this.ultraButton5.Text = "< 移除";
		this.ultraButton5.Click += new System.EventHandler(ultraButton5_Click);
		appearance2.FontData.SizeInPoints = 9f;
		this.ultraButton4.Appearance = appearance2;
		this.ultraButton4.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton4.Location = new System.Drawing.Point(4, 165);
		this.ultraButton4.Name = "ultraButton4";
		this.ultraButton4.ShowFocusRect = false;
		this.ultraButton4.ShowOutline = false;
		this.ultraButton4.Size = new System.Drawing.Size(85, 30);
		this.ultraButton4.SupportThemes = false;
		this.ultraButton4.TabIndex = 3;
		this.ultraButton4.Text = "選取 >";
		this.ultraButton4.Click += new System.EventHandler(ultraButton4_Click);
		appearance3.FontData.SizeInPoints = 9f;
		this.ultraButton1.Appearance = appearance3;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton1.Location = new System.Drawing.Point(4, 231);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(85, 30);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 1;
		this.ultraButton1.Text = "<< 全部移除";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		appearance4.FontData.SizeInPoints = 9f;
		this.BtnAll.Appearance = appearance4;
		this.BtnAll.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnAll.Location = new System.Drawing.Point(4, 132);
		this.BtnAll.Name = "BtnAll";
		this.BtnAll.ShowFocusRect = false;
		this.BtnAll.ShowOutline = false;
		this.BtnAll.Size = new System.Drawing.Size(85, 30);
		this.BtnAll.SupportThemes = false;
		this.BtnAll.TabIndex = 0;
		this.BtnAll.Text = "全選 >>";
		this.BtnAll.Click += new System.EventHandler(BtnAll_Click);
		this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel6.Controls.Add(this.GridDestination);
		this.panel6.Controls.Add(this.panel8);
		this.panel6.Controls.Add(this.ultraLabel4);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel6.Location = new System.Drawing.Point(437, 0);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(330, 417);
		this.panel6.TabIndex = 1;
		this.GridDestination._ExcelFileName = "";
		this.GridDestination._ExcelSheeName = "";
		this.GridDestination._IsOpenExcelAfterExport = false;
		this.GridDestination.AllowEditing = false;
		this.GridDestination.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridDestination.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.GridDestination.ColumnInfo = "2,0,0,0,0,110,Columns:0{Name:\"ProjectCode\";Caption:\"專案代碼\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t1{Width:200;Name:\"ProjectNameC\";Caption:\"專案名稱\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t";
		this.GridDestination.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridDestination.ExtendLastCol = true;
		this.GridDestination.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
		this.GridDestination.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridDestination.ForeColor = System.Drawing.Color.Black;
		this.GridDestination.Location = new System.Drawing.Point(0, 28);
		this.GridDestination.Name = "GridDestination";
		this.GridDestination.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.GridDestination.ShowCursor = true;
		this.GridDestination.ShowToolTipOnNarrowColumn = true;
		this.GridDestination.Size = new System.Drawing.Size(328, 355);
		this.GridDestination.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11pt;BackColor:237, 243, 254;ForeColor:Black;Border:Flat,1,Silver,Both;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;TextAlign:GeneralTop;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;TextAlign:LeftCenter;Format:\"###,###,###,##0\";}\tFocus{Font:細明體, 9.75pt;BackColor:White;Border:Double,1,96, 145, 234,Both;}\tSearch{Font:細明體, 9.75pt;BackColor:White;ForeColor:HighlightText;Border:Double,1,96, 145, 234,Both;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.GridDestination.TabIndex = 5;
		this.GridDestination.Tree.Column = 1;
		this.GridDestination.Tree.LineColor = System.Drawing.Color.Gray;
		this.panel8.Controls.Add(this.ultraButton3);
		this.panel8.Controls.Add(this.ultraButton2);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel8.Location = new System.Drawing.Point(0, 383);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(328, 32);
		this.panel8.TabIndex = 4;
		appearance5.FontData.SizeInPoints = 9f;
		appearance5.Image = resources.GetObject("appearance5.Image");
		appearance5.ImageVAlign = Infragistics.Win.VAlign.Middle;
		appearance5.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraButton3.Appearance = appearance5;
		this.ultraButton3.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.ultraButton3.Location = new System.Drawing.Point(175, 2);
		this.ultraButton3.Name = "ultraButton3";
		this.ultraButton3.ShowFocusRect = false;
		this.ultraButton3.ShowOutline = false;
		this.ultraButton3.Size = new System.Drawing.Size(60, 28);
		this.ultraButton3.SupportThemes = false;
		this.ultraButton3.TabIndex = 7;
		this.ultraButton3.Text = "下移";
		this.ultraButton3.Click += new System.EventHandler(ultraButton3_Click);
		appearance6.FontData.SizeInPoints = 9f;
		appearance6.Image = resources.GetObject("appearance6.Image");
		appearance6.ImageVAlign = Infragistics.Win.VAlign.Middle;
		appearance6.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraButton2.Appearance = appearance6;
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.ultraButton2.Location = new System.Drawing.Point(107, 2);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(60, 28);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 6;
		this.ultraButton2.Text = "上移";
		this.ultraButton2.Click += new System.EventHandler(ultraButton2_Click);
		appearance7.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance7.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel4.Appearance = appearance7;
		this.ultraLabel4.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.ultraLabel4.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel4.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(328, 28);
		this.ultraLabel4.TabIndex = 3;
		this.ultraLabel4.Text = "已選取的專案";
		this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel5.Controls.Add(this.GridSource);
		this.panel5.Controls.Add(this.ultraLabel3);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel5.Location = new System.Drawing.Point(15, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(330, 417);
		this.panel5.TabIndex = 9;
		this.GridSource._ExcelFileName = "";
		this.GridSource._ExcelSheeName = "";
		this.GridSource._IsOpenExcelAfterExport = false;
		this.GridSource.AllowEditing = false;
		this.GridSource.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridSource.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.GridSource.ColumnInfo = "2,0,0,0,0,110,Columns:0{Name:\"ProjectCode\";Caption:\"專案代碼\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t1{Width:300;Name:\"ProjectNameC\";Caption:\"專案名稱\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t";
		this.GridSource.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridSource.ExtendLastCol = true;
		this.GridSource.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
		this.GridSource.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridSource.ForeColor = System.Drawing.Color.Black;
		this.GridSource.Location = new System.Drawing.Point(0, 28);
		this.GridSource.Name = "GridSource";
		this.GridSource.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.GridSource.ShowCursor = true;
		this.GridSource.ShowToolTipOnNarrowColumn = true;
		this.GridSource.Size = new System.Drawing.Size(328, 387);
		this.GridSource.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11pt;BackColor:237, 243, 254;ForeColor:Black;Border:Flat,1,Silver,Both;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;TextAlign:GeneralTop;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;TextAlign:LeftCenter;Format:\"###,###,###,##0\";}\tFocus{Font:細明體, 9.75pt;BackColor:White;Border:Double,1,96, 145, 234,Both;}\tSearch{Font:細明體, 9.75pt;BackColor:White;ForeColor:HighlightText;Border:Double,1,96, 145, 234,Both;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.GridSource.TabIndex = 1;
		this.GridSource.Tree.Column = 1;
		this.GridSource.Tree.LineColor = System.Drawing.Color.Gray;
		appearance8.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance8.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance8.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel3.Appearance = appearance8;
		this.ultraLabel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel3.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(328, 28);
		this.ultraLabel3.TabIndex = 2;
		this.ultraLabel3.Text = "專案列表";
		this.panel10.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel10.Location = new System.Drawing.Point(0, 0);
		this.panel10.Name = "panel10";
		this.panel10.Size = new System.Drawing.Size(15, 417);
		this.panel10.TabIndex = 8;
		this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel9.Location = new System.Drawing.Point(767, 0);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(15, 417);
		this.panel9.TabIndex = 7;
		this.panel3.Controls.Add(this.ultraLabel1);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel3.Location = new System.Drawing.Point(0, 457);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(782, 48);
		this.panel3.TabIndex = 6;
		appearance9.ForeColor = System.Drawing.Color.FromArgb(0, 51, 153);
		this.ultraLabel1.Appearance = appearance9;
		this.ultraLabel1.Location = new System.Drawing.Point(15, 9);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(688, 35);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "合併專案時，當工項代碼有重覆時會依挑選專案的順序，以先挑選的專案取代後挑的專案中的單位、單價、單價分析等資料！";
		this.panel2.Controls.Add(this.ultraButton6);
		this.panel2.Controls.Add(this.A_Btn_Cncl);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 505);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(782, 36);
		this.panel2.TabIndex = 5;
		this.ultraButton6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance10.Image = resources.GetObject("appearance10.Image");
		appearance10.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton6.Appearance = appearance10;
		this.ultraButton6.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton6.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton6.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton6.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton6.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton6.Location = new System.Drawing.Point(588, 3);
		this.ultraButton6.Name = "ultraButton6";
		this.ultraButton6.ShowFocusRect = false;
		this.ultraButton6.ShowOutline = false;
		this.ultraButton6.Size = new System.Drawing.Size(88, 31);
		this.ultraButton6.SupportThemes = false;
		this.ultraButton6.TabIndex = 4;
		this.ultraButton6.Text = "確定";
		this.ultraButton6.Click += new System.EventHandler(ultraButton6_Click);
		this.A_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance11.Image = resources.GetObject("appearance11.Image");
		appearance11.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Cncl.Appearance = appearance11;
		this.A_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Cncl.Location = new System.Drawing.Point(679, 3);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.ShowFocusRect = false;
		this.A_Btn_Cncl.ShowOutline = false;
		this.A_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Cncl.SupportThemes = false;
		this.A_Btn_Cncl.TabIndex = 3;
		this.A_Btn_Cncl.Text = "取消";
		this.panel1.Controls.Add(this.ultraLabel2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(782, 40);
		this.panel1.TabIndex = 4;
		this.ultraLabel2.Location = new System.Drawing.Point(14, 12);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(688, 20);
		this.ultraLabel2.TabIndex = 1;
		this.ultraLabel2.Text = "挑選要合併的專案";
		this.Tab_B.Controls.Add(this.pnl_Proc);
		this.Tab_B.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(782, 541);
		this.pnl_Proc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pnl_Proc.Controls.Add(this.Prog1);
		this.pnl_Proc.Controls.Add(this.ultraLabel5);
		this.pnl_Proc.Location = new System.Drawing.Point(0, 0);
		this.pnl_Proc.Name = "pnl_Proc";
		this.pnl_Proc.Size = new System.Drawing.Size(190, 60);
		this.pnl_Proc.TabIndex = 2;
		this.Prog1.Location = new System.Drawing.Point(12, 28);
		this.Prog1.Name = "Prog1";
		this.Prog1.Size = new System.Drawing.Size(164, 23);
		this.Prog1.SupportThemes = false;
		this.Prog1.TabIndex = 3;
		this.Prog1.Text = "[Formatted]";
		this.ultraLabel5.Location = new System.Drawing.Point(8, 8);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(168, 23);
		this.ultraLabel5.TabIndex = 2;
		this.ultraLabel5.Text = "資料處理中...";
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_A);
		this.Tab_Ctrl.Controls.Add(this.Tab_B);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(782, 541);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabIndex = 0;
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "tab1";
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "tab2";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2] { ultraTab1, ultraTab2 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(782, 541);
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.A_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(782, 541);
		base.Controls.Add(this.Tab_Ctrl);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormBudgetCombine";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "併標";
		base.Load += new System.EventHandler(FormBudgetCombine_Load);
		this.Tab_A.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		this.panel6.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridDestination).EndInit();
		this.panel8.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridSource).EndInit();
		this.panel3.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.Tab_B.ResumeLayout(false);
		this.pnl_Proc.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).EndInit();
		this.Tab_Ctrl.ResumeLayout(false);
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

	public FormBudgetCombine()
	{
		InitializeComponent();
	}

	private void FormBudgetCombine_Load(object sender, EventArgs e)
	{
		GridDestination.Rows.Count = 1;
		Archnowledge.Pcces.DomainModule.General.PubProject pubProject = new Archnowledge.Pcces.DomainModule.General.PubProject();
		DataSet ds = pubProject.GetProjectList(F_UserID);
		BindToGrid(ds.Tables[0]);
	}

	private void BindToGrid(DataTable dt)
	{
		DataView dv = dt.DefaultView;
		dv.RowFilter = "Bud IS NOT NULL";
		GridSource.Rows.Count = dv.Count + 1;
		for (int i = 0; i < dv.Count; i++)
		{
			GridSource[i + 1, "ProjectCode"] = dv[i]["projectCode"].ToString();
			GridSource[i + 1, "ProjectNameC"] = dv[i]["ProjCName"].ToString();
		}
		GridSource.AutoSizeCols();
	}

	private void ultraButton6_Click(object sender, EventArgs e)
	{
		Tab_B.Tab.Selected = true;
		base.FormBorderStyle = FormBorderStyle.None;
		base.Height = 60;
		base.Width = 190;
		int iH = Screen.PrimaryScreen.WorkingArea.Height;
		int iW = Screen.PrimaryScreen.WorkingArea.Width;
		base.Top = iH / 2 - pnl_Proc.Height / 2;
		base.Left = iW / 2 - pnl_Proc.Width / 2;
		Application.DoEvents();
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("【併標】專案挑選--" + F_ProjectCode + "(" + IPStr + ")");
		Archnowledge.Pcces.BUDClass.ItemA ItemACom = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
		ItemACom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		Archnowledge.Pcces.BUDClass.Project ProjCom = new Archnowledge.Pcces.BUDClass.Project(aArr);
		ProjCom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		Prog1.Minimum = 0;
		Prog1.Maximum = GridDestination.Rows.Count - 1;
		Prog1.Value = 0;
		for (int i = 1; i < GridDestination.Rows.Count; i++)
		{
			Prog1.Value++;
			string exp_projcode = GridDestination[i, "ProjectCode"].ToString();
			string imp_projcode = F_ProjectCode;
			ExecResult ER = (F_ActionName switch
			{
				PccesFormAction.BUD => new BudProject(), 
				PccesFormAction.BID => new BidProject(), 
				PccesFormAction.SplitContract => new SubProject(), 
				_ => new Archnowledge.Pcces.DomainModule.LogicalBase.Project(), 
			}).CombineProject(imp_projcode.Trim(), exp_projcode.Trim());
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show("合併專案時發生錯誤,訊息:" + ER.Message);
			}
			ProjCom.ps_projectCode = exp_projcode;
			ProjCom.ps_mainProj = imp_projcode;
			ProjCom.UpdItem();
		}
		ItemACom = null;
		ProjCom = null;
		PubTools.WriteRoughlyLog(aArr);
		base.DialogResult = DialogResult.OK;
	}

	private void BtnAll_Click(object sender, EventArgs e)
	{
		string sStr = "";
		for (int i = GridSource.Rows.Count - 1; i > 0; i--)
		{
			sStr = GridSource[i, 0].ToString() + "\t" + GridSource[i, 1].ToString();
			GridDestination.AddItem(sStr, 1);
			GridSource.RemoveItem(i);
		}
		GridSource.AutoSizeCols();
		GridDestination.AutoSizeCols();
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		string sStr = "";
		for (int i = GridDestination.Rows.Count - 1; i > 0; i--)
		{
			sStr = GridDestination[i, 0].ToString() + "\t" + GridDestination[i, 1].ToString();
			GridSource.AddItem(sStr, 1);
			GridDestination.RemoveItem(i);
		}
		GridSource.AutoSizeCols();
		GridDestination.AutoSizeCols();
	}

	private void ultraButton4_Click(object sender, EventArgs e)
	{
		string sStr = "";
		for (int i = GridSource.Rows.Count - 1; i > 0; i--)
		{
			if (GridSource.Rows[i].Selected)
			{
				sStr = GridSource[i, 0].ToString() + "\t" + GridSource[i, 1].ToString();
				GridDestination.AddItem(sStr);
				GridSource.RemoveItem(i);
			}
		}
		GridSource.AutoSizeCols();
		GridDestination.AutoSizeCols();
	}

	private void ultraButton5_Click(object sender, EventArgs e)
	{
		if (GridDestination.SelectedRowCount == 0)
		{
			string sWarning = "請先選取要移除的專案!";
			MessageBox.Show(this, sWarning, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		string sStr = "";
		for (int i = GridDestination.Rows.Count - 1; i > 0; i--)
		{
			if (GridDestination.Rows[i].Selected)
			{
				sStr = GridDestination[i, 0].ToString() + "\t" + GridDestination[i, 1].ToString();
				GridSource.AddItem(sStr);
				GridDestination.RemoveItem(i);
			}
		}
		GridSource.AutoSizeCols();
		GridDestination.AutoSizeCols();
	}

	private void ultraButton2_Click(object sender, EventArgs e)
	{
		ArrayList SelItems = new ArrayList();
		int iIdx = -1;
		for (int i = 1; i < GridDestination.Rows.Count; i++)
		{
			if (GridDestination.Rows[i].Selected)
			{
				SelItems.Add(GridDestination[i, "ProjectCode"]);
			}
		}
		for (int i = 0; i < SelItems.Count; i++)
		{
			iIdx = GridDestination.FindRow((string)SelItems[i], 1, GridDestination.Cols["ProjectCode"].SafeIndex, wrap: false);
			if (iIdx == 1)
			{
				break;
			}
			if (iIdx > -1)
			{
				GridDestination.Rows[iIdx].Move(iIdx - 1);
			}
		}
		for (int i = 0; i < SelItems.Count; i++)
		{
			GridDestination.Rows[Get_RealRow2(SelItems[i].ToString())].Selected = true;
		}
	}

	private void ultraButton3_Click(object sender, EventArgs e)
	{
		ArrayList SelItems = new ArrayList();
		int iIdx = -1;
		for (int i = 1; i < GridDestination.Rows.Count; i++)
		{
			if (GridDestination.Rows[i].Selected)
			{
				SelItems.Add(GridDestination[i, "ProjectCode"]);
			}
		}
		for (int i = SelItems.Count - 1; i >= 0; i--)
		{
			iIdx = GridDestination.FindRow((string)SelItems[i], 1, GridDestination.Cols["ProjectCode"].SafeIndex, wrap: false);
			if (iIdx == GridDestination.Rows.Count - 1)
			{
				break;
			}
			if (iIdx > -1)
			{
				GridDestination.Rows[iIdx].Move(iIdx + 1);
			}
		}
		for (int i = 0; i < SelItems.Count; i++)
		{
			GridDestination.Rows[Get_RealRow2(SelItems[i].ToString())].Selected = true;
		}
	}

	private int Get_RealRow2(string sPubCode)
	{
		int RetV = -1;
		for (int i = 1; i < GridDestination.Rows.Count; i++)
		{
			if (GridDestination[i, "ProjectCode"].ToString() == sPubCode)
			{
				RetV = i;
				break;
			}
		}
		return RetV;
	}
}
