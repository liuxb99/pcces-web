using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.CTRClass;
using Infragistics.UltraChart.Resources;
using Infragistics.UltraChart.Shared.Styles;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinChart;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.Invoice;

public class FormInvoiceGraphic : Form
{
	private const string CallFormHelp = "FormInvoiceGraphic";

	private UltraToolbarsManager ultraToolbarsManager1;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Bottom;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Right;

	private Panel panel1;

	private IContainer components;

	private Panel panel2;

	internal GroupBox GroupBox3;

	internal ListBox printList;

	internal GroupBox GroupBox2;

	internal CheckBox chkColor;

	internal CheckBox chkLandscape;

	private GroupBox groupBox1;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	internal Button cmdPrint;

	private PrinterSettings printer;

	private PageSettings pageSetting;

	private DataTable ldt_AccItem;

	private DataTable DT1;

	private string F_ProjectCode;

	private string F_ProjectCName;

	private string F_SubProjectCode;

	private string F_Issue;

	private UltraChart ultraChart1;

	private string F_UserID;

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

	public string _ProjectCName
	{
		get
		{
			return F_ProjectCName;
		}
		set
		{
			F_ProjectCName = value;
		}
	}

	public string _SubProjectCode
	{
		get
		{
			return F_SubProjectCode;
		}
		set
		{
			F_SubProjectCode = value;
		}
	}

	public string _Issue
	{
		get
		{
			return F_Issue;
		}
		set
		{
			F_Issue = value;
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
		}
	}

	public FormInvoiceGraphic()
	{
		InitializeComponent();
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
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool2");
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPrint");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExit");
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPrint");
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Invoice.FormInvoiceGraphic));
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExit");
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this._FormSys_B_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraChart1 = new Infragistics.Win.UltraWinChart.UltraChart();
		this.panel2 = new System.Windows.Forms.Panel();
		this.GroupBox3 = new System.Windows.Forms.GroupBox();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.printList = new System.Windows.Forms.ListBox();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.cmdPrint = new System.Windows.Forms.Button();
		this.GroupBox2 = new System.Windows.Forms.GroupBox();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.chkColor = new System.Windows.Forms.CheckBox();
		this.chkLandscape = new System.Windows.Forms.CheckBox();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraChart1).BeginInit();
		this.panel2.SuspendLayout();
		this.GroupBox3.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.GroupBox2.SuspendLayout();
		base.SuspendLayout();
		appearance1.FontData.Name = "Arial";
		appearance1.FontData.SizeInPoints = 11f;
		this.ultraToolbarsManager1.Appearance = appearance1;
		appearance2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance2.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.DockAreaAppearance = appearance2;
		this.ultraToolbarsManager1.DockWithinContainer = this;
		this.ultraToolbarsManager1.ImageSizeSmall = new System.Drawing.Size(20, 20);
		this.ultraToolbarsManager1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraToolbarsManager1.LockToolbars = true;
		appearance3.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance3.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance3.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.MenuSettings.HotTrackAppearance = appearance3;
		appearance4.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance4.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsManager1.MenuSettings.IconAreaAppearance = appearance4;
		appearance5.BackColor = System.Drawing.Color.White;
		appearance5.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.MenuSettings.ToolAppearance = appearance5;
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager1.ShowQuickCustomizeButton = false;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.True;
		appearance6.TextVAlign = Infragistics.Win.VAlign.Middle;
		ultraToolbar1.Settings.Appearance = appearance6;
		ultraToolbar1.Text = "Tool2";
		buttonTool2.InstanceProps.IsFirstInGroup = true;
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool1, buttonTool2 });
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[1] { ultraToolbar1 });
		appearance7.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance7.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance7;
		appearance8.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance8.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance8.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance8;
		appearance9.Image = resources.GetObject("appearance9.Image");
		buttonTool3.SharedProps.AppearancesSmall.Appearance = appearance9;
		buttonTool3.SharedProps.Caption = "開啟列印選項...";
		buttonTool3.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		appearance10.Image = resources.GetObject("appearance10.Image");
		buttonTool4.SharedProps.AppearancesSmall.Appearance = appearance10;
		buttonTool4.SharedProps.Caption = "關 閉";
		buttonTool4.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool3, buttonTool4 });
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this._FormSys_B_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormSys_B_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormSys_B_Toolbars_Dock_Area_Top.Name = "_FormSys_B_Toolbars_Dock_Area_Top";
		this._FormSys_B_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(792, 31);
		this._FormSys_B_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 573);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Name = "_FormSys_B_Toolbars_Dock_Area_Bottom";
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(792, 0);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormSys_B_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 31);
		this._FormSys_B_Toolbars_Dock_Area_Left.Name = "_FormSys_B_Toolbars_Dock_Area_Left";
		this._FormSys_B_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 542);
		this._FormSys_B_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormSys_B_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(792, 31);
		this._FormSys_B_Toolbars_Dock_Area_Right.Name = "_FormSys_B_Toolbars_Dock_Area_Right";
		this._FormSys_B_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 542);
		this._FormSys_B_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.ultraChart1);
		this.panel1.Controls.Add(this.panel2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 31);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(792, 542);
		this.panel1.TabIndex = 31;
		this.ultraChart1.ChartType = Infragistics.UltraChart.Shared.Styles.ChartType.BarChart;
		this.ultraChart1.Axis.X.Labels.Flip = false;
		this.ultraChart1.Axis.X.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
		this.ultraChart1.Axis.X.Labels.ItemFormat = Infragistics.UltraChart.Shared.Styles.AxisItemLabelFormat.DataValue;
		this.ultraChart1.Axis.X.Labels.ItemFormatString = "<DATA_VALUE:00.00>";
		this.ultraChart1.Axis.X.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
		this.ultraChart1.Axis.X.Labels.OrientationAngle = 0;
		this.ultraChart1.Axis.X.Labels.SeriesFormatString = "";
		this.ultraChart1.Axis.X.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
		this.ultraChart1.Axis.X.RangeMax = 100.0;
		this.ultraChart1.Axis.X.RangeType = Infragistics.UltraChart.Shared.Styles.AxisRangeType.Custom;
		this.ultraChart1.Axis.X.ScrollScale.Height = 10;
		this.ultraChart1.Axis.X.ScrollScale.Visible = false;
		this.ultraChart1.Axis.X.ScrollScale.Width = 15;
		this.ultraChart1.Axis.X.TickmarkInterval = 0.0;
		this.ultraChart1.Axis.X2.Labels.Flip = false;
		this.ultraChart1.Axis.X2.Labels.ItemFormat = Infragistics.UltraChart.Shared.Styles.AxisItemLabelFormat.DataValue;
		this.ultraChart1.Axis.X2.Labels.ItemFormatString = "<DATA_VALUE:00.00>";
		this.ultraChart1.Axis.X2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
		this.ultraChart1.Axis.X2.Labels.OrientationAngle = 0;
		this.ultraChart1.Axis.X2.Labels.SeriesFormatString = "";
		this.ultraChart1.Axis.X2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
		this.ultraChart1.Axis.X2.ScrollScale.Height = 10;
		this.ultraChart1.Axis.X2.ScrollScale.Visible = false;
		this.ultraChart1.Axis.X2.ScrollScale.Width = 15;
		this.ultraChart1.Axis.X2.TickmarkInterval = 0.0;
		this.ultraChart1.Axis.Y.Labels.Flip = false;
		this.ultraChart1.Axis.Y.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far;
		this.ultraChart1.Axis.Y.Labels.ItemFormat = Infragistics.UltraChart.Shared.Styles.AxisItemLabelFormat.ItemLabel;
		this.ultraChart1.Axis.Y.Labels.ItemFormatString = "<ITEM_LABEL>";
		this.ultraChart1.Axis.Y.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
		this.ultraChart1.Axis.Y.Labels.OrientationAngle = 0;
		this.ultraChart1.Axis.Y.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
		this.ultraChart1.Axis.Y.ScrollScale.Height = 10;
		this.ultraChart1.Axis.Y.ScrollScale.Visible = true;
		this.ultraChart1.Axis.Y.ScrollScale.Width = 20;
		this.ultraChart1.Axis.Y.TickmarkInterval = 0.0;
		this.ultraChart1.Axis.Y2.Labels.Flip = false;
		this.ultraChart1.Axis.Y2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
		this.ultraChart1.Axis.Y2.Labels.ItemFormat = Infragistics.UltraChart.Shared.Styles.AxisItemLabelFormat.ItemLabel;
		this.ultraChart1.Axis.Y2.Labels.ItemFormatString = "<ITEM_LABEL>";
		this.ultraChart1.Axis.Y2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
		this.ultraChart1.Axis.Y2.Labels.OrientationAngle = 0;
		this.ultraChart1.Axis.Y2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
		this.ultraChart1.Axis.Y2.ScrollScale.Height = 10;
		this.ultraChart1.Axis.Y2.ScrollScale.Visible = false;
		this.ultraChart1.Axis.Y2.ScrollScale.Width = 15;
		this.ultraChart1.Axis.Y2.TickmarkInterval = 0.0;
		this.ultraChart1.Axis.Z.Labels.Flip = false;
		this.ultraChart1.Axis.Z.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
		this.ultraChart1.Axis.Z.Labels.ItemFormat = Infragistics.UltraChart.Shared.Styles.AxisItemLabelFormat.None;
		this.ultraChart1.Axis.Z.Labels.ItemFormatString = "";
		this.ultraChart1.Axis.Z.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
		this.ultraChart1.Axis.Z.Labels.OrientationAngle = 0;
		this.ultraChart1.Axis.Z.Labels.SeriesFormatString = "";
		this.ultraChart1.Axis.Z.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
		this.ultraChart1.Axis.Z.ScrollScale.Height = 10;
		this.ultraChart1.Axis.Z.ScrollScale.Visible = false;
		this.ultraChart1.Axis.Z.ScrollScale.Width = 15;
		this.ultraChart1.Axis.Z.TickmarkInterval = 0.0;
		this.ultraChart1.Axis.Z2.Labels.Flip = false;
		this.ultraChart1.Axis.Z2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
		this.ultraChart1.Axis.Z2.Labels.ItemFormat = Infragistics.UltraChart.Shared.Styles.AxisItemLabelFormat.None;
		this.ultraChart1.Axis.Z2.Labels.ItemFormatString = "";
		this.ultraChart1.Axis.Z2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
		this.ultraChart1.Axis.Z2.Labels.OrientationAngle = 0;
		this.ultraChart1.Axis.Z2.Labels.SeriesFormatString = "";
		this.ultraChart1.Axis.Z2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
		this.ultraChart1.Axis.Z2.ScrollScale.Height = 10;
		this.ultraChart1.Axis.Z2.ScrollScale.Visible = false;
		this.ultraChart1.Axis.Z2.ScrollScale.Width = 15;
		this.ultraChart1.Axis.Z2.TickmarkInterval = 0.0;
		this.ultraChart1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraChart1.Location = new System.Drawing.Point(0, 0);
		this.ultraChart1.Name = "ultraChart1";
		this.ultraChart1.ScrollBarImage = (System.Drawing.Bitmap)resources.GetObject("ultraChart1.ScrollBarImage");
		this.ultraChart1.Size = new System.Drawing.Size(792, 430);
		this.ultraChart1.TabIndex = 3;
		this.panel2.Controls.Add(this.GroupBox3);
		this.panel2.Controls.Add(this.groupBox1);
		this.panel2.Controls.Add(this.GroupBox2);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 430);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(792, 112);
		this.panel2.TabIndex = 1;
		this.panel2.Visible = false;
		this.GroupBox3.Controls.Add(this.ultraLabel1);
		this.GroupBox3.Controls.Add(this.printList);
		this.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GroupBox3.Font = new System.Drawing.Font("新細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GroupBox3.Location = new System.Drawing.Point(160, 0);
		this.GroupBox3.Name = "GroupBox3";
		this.GroupBox3.Size = new System.Drawing.Size(464, 112);
		this.GroupBox3.TabIndex = 27;
		this.GroupBox3.TabStop = false;
		this.ultraLabel1.AutoSize = true;
		this.ultraLabel1.Font = new System.Drawing.Font("新細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(8, 16);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(85, 20);
		this.ultraLabel1.TabIndex = 14;
		this.ultraLabel1.Text = "選擇印表機:";
		this.printList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.printList.Font = new System.Drawing.Font("新細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.printList.ItemHeight = 15;
		this.printList.Location = new System.Drawing.Point(10, 40);
		this.printList.Name = "printList";
		this.printList.Size = new System.Drawing.Size(440, 64);
		this.printList.TabIndex = 13;
		this.printList.SelectedIndexChanged += new System.EventHandler(printList_SelectedIndexChanged);
		this.groupBox1.Controls.Add(this.cmdPrint);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
		this.groupBox1.Font = new System.Drawing.Font("新細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.groupBox1.Location = new System.Drawing.Point(624, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(168, 112);
		this.groupBox1.TabIndex = 29;
		this.groupBox1.TabStop = false;
		this.cmdPrint.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.cmdPrint.Location = new System.Drawing.Point(27, 49);
		this.cmdPrint.Name = "cmdPrint";
		this.cmdPrint.Size = new System.Drawing.Size(115, 27);
		this.cmdPrint.TabIndex = 22;
		this.cmdPrint.Text = "列印";
		this.cmdPrint.Click += new System.EventHandler(cmdPrint_Click);
		this.GroupBox2.Controls.Add(this.ultraLabel2);
		this.GroupBox2.Controls.Add(this.chkColor);
		this.GroupBox2.Controls.Add(this.chkLandscape);
		this.GroupBox2.Dock = System.Windows.Forms.DockStyle.Left;
		this.GroupBox2.Font = new System.Drawing.Font("新細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GroupBox2.Location = new System.Drawing.Point(0, 0);
		this.GroupBox2.Name = "GroupBox2";
		this.GroupBox2.Size = new System.Drawing.Size(160, 112);
		this.GroupBox2.TabIndex = 28;
		this.GroupBox2.TabStop = false;
		this.ultraLabel2.AutoSize = true;
		this.ultraLabel2.Font = new System.Drawing.Font("新細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel2.Location = new System.Drawing.Point(8, 16);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(40, 20);
		this.ultraLabel2.TabIndex = 17;
		this.ultraLabel2.Text = "選項:";
		this.chkColor.Location = new System.Drawing.Point(12, 47);
		this.chkColor.Name = "chkColor";
		this.chkColor.Size = new System.Drawing.Size(128, 26);
		this.chkColor.TabIndex = 16;
		this.chkColor.Text = "列印顏色";
		this.chkColor.CheckedChanged += new System.EventHandler(chkColor_CheckedChanged);
		this.chkLandscape.Location = new System.Drawing.Point(12, 75);
		this.chkLandscape.Name = "chkLandscape";
		this.chkLandscape.Size = new System.Drawing.Size(132, 29);
		this.chkLandscape.TabIndex = 15;
		this.chkLandscape.Text = "橫印";
		this.chkLandscape.CheckedChanged += new System.EventHandler(chkLandscape_CheckedChanged);
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(792, 573);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Bottom);
		base.KeyPreview = true;
		base.MinimizeBox = false;
		base.Name = "FormInvoiceGraphic";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "統計圖表";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormInvoiceGraphic_KeyDown);
		base.Load += new System.EventHandler(FormInvoiceGraphic_Load);
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraChart1).EndInit();
		this.panel2.ResumeLayout(false);
		this.GroupBox3.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		this.GroupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void FormInvoiceGraphic_Load(object sender, EventArgs e)
	{
		InitialGraphic();
		LoadData();
		DrawGraphic();
	}

	private void InitialGraphic()
	{
		Util.DemoSetup(ultraChart1);
		ultraChart1.Axis.X.Extent = 40;
		ultraChart1.Axis.X.Labels.Orientation = TextOrientation.Custom;
		ultraChart1.Axis.X.Labels.Flip = true;
		ultraChart1.Axis.X.Labels.OrientationAngle = 210;
		ultraChart1.Axis.Y.Extent = 400;
		ultraChart1.Axis.Y.Labels.ItemFormat = AxisItemLabelFormat.Custom;
		ultraChart1.Axis.Y.Labels.ItemFormatString = "<SERIES_LABEL>";
		ultraChart1.Axis.Y.Labels.Orientation = TextOrientation.Horizontal;
		ultraChart1.TitleLeft.Text = "";
		ultraChart1.TitleRight.Text = "";
		ultraChart1.TitleTop.Text = "【" + _ProjectCode + "】" + F_ProjectCName + "第 " + F_Issue + " 期 " + DateTime.Today.ToShortDateString();
		ultraChart1.TitleBottom.Text = "聯宏資通股份有限公司";
		printer = new PrinterSettings();
		pageSetting = new PageSettings(printer);
		pageSetting.Margins = new System.Drawing.Printing.Margins(10, 0, 10, 0);
		cmdPrint.Enabled = false;
		if (PrinterSettings.InstalledPrinters.Count <= 0)
		{
			return;
		}
		cmdPrint.Enabled = true;
		foreach (string str in PrinterSettings.InstalledPrinters)
		{
			printList.Items.Add(str);
		}
		printList.SelectedIndex = 0;
	}

	private void LoadData()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("讀取資料--估驗計價--繪圖");
		submfq MfqCom = new submfq(tmp_AL1);
		ldt_AccItem = MfqCom.ListItem("", F_Issue, F_SubProjectCode, F_ProjectCode);
		MfqCom = null;
		DT1 = new DataTable();
		DT1.Columns.Add("CName", Type.GetType("System.String"));
		DT1.Columns.Add("Prgss", Type.GetType("System.Double"));
		for (int i = ldt_AccItem.Rows.Count - 1; i >= 0; i--)
		{
			DataRow DR = DT1.NewRow();
			DR["CName"] = ldt_AccItem.Rows[i]["Cname"];
			DR["Prgss"] = ldt_AccItem.Rows[i]["acc_prec"];
			DT1.Rows.Add(DR);
		}
	}

	private void DrawGraphic()
	{
		ultraChart1.Data.DataSource = DT1;
		ultraChart1.Data.DataBind();
		double dRate = 1.0;
		int TotalRows = DT1.Rows.Count;
		dRate = ((TotalRows > 20) ? (20.0 / (double)TotalRows) : 1.0);
		ultraChart1.Axis.Y.ScrollScale.Scale = dRate;
		ultraChart1.Axis.Y.ScrollScale.Scroll = 1.0;
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		switch (e.Tool.Key)
		{
		case "mnuPrint":
			Do_Print();
			break;
		case "mnuExit":
			Do_Exit();
			break;
		}
	}

	private void Do_Print()
	{
		if (!panel2.Visible)
		{
			panel2.Visible = true;
			ultraToolbarsManager1.Tools["mnuPrint"].SharedProps.Caption = "關閉列印選項";
		}
		else
		{
			panel2.Visible = false;
			ultraToolbarsManager1.Tools["mnuPrint"].SharedProps.Caption = "開啟列印選項";
		}
	}

	private void Do_Exit()
	{
		base.DialogResult = DialogResult.OK;
	}

	private void cmdPrint_Click(object sender, EventArgs e)
	{
		ultraChart1.PrintChart(printer, pageSetting);
	}

	private void printList_SelectedIndexChanged(object sender, EventArgs e)
	{
		printer.PrinterName = printList.Items[printList.SelectedIndex].ToString();
	}

	private void chkColor_CheckedChanged(object sender, EventArgs e)
	{
		pageSetting.Color = chkColor.Checked;
	}

	private void chkLandscape_CheckedChanged(object sender, EventArgs e)
	{
		pageSetting.Landscape = chkLandscape.Checked;
	}

	private void FormInvoiceGraphic_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormInvoiceGraphic");
		}
	}
}
