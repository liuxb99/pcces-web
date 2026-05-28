using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.CTRClass;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1Sizer;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.Invoice;

public class FormInvoiceSubAcInfo : Form
{
	private const string CallFormHelp = "FormInvoiceSubAcInfo";

	private Panel panel16;

	private GroupBox groupBox6;

	private UltraButton D_Btn_Cncl;

	private UltraButton D_Btn_Next;

	private Panel panel1;

	private C1Sizer c1Sizer1;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel8;

	private UltraLabel ultraLabel9;

	private UltraLabel ultraLabel10;

	private UltraLabel ultraLabel11;

	private UltraLabel ultraLabel12;

	private UltraLabel ultraLabel13;

	private UltraLabel ultraLabel15;

	private UltraLabel lb_Adv1;

	private UltraLabel lb_Aldv1;

	private UltraLabel lb_Res1;

	private UltraLabel lb_ResTn1;

	private UltraLabel lb_Oth1;

	private UltraLabel lb_Iou1;

	private UltraLabel lb_Duc1;

	private UltraLabel lb_Add1;

	private UltraLabel lb_Realpay1;

	private UltraLabel lb_Total1;

	private Panel panel2;

	private UltraLabel lb_Total2;

	private CheckBox cb_Aldv;

	private CheckBox cb_Res2;

	private UltraTextEditor Tb_Adv2;

	private UltraTextEditor Tb_Aldv2;

	private UltraTextEditor Tb_Res2;

	private UltraTextEditor Tb_ResTn2;

	private UltraTextEditor Tb_Oth2;

	private UltraTextEditor Tb_Iou2;

	private UltraTextEditor Tb_Duc2;

	private UltraTextEditor Tb_Add2;

	private UltraLabel lb_Realpay2;

	private UltraButton btn_dudect;

	private UltraButton btn_ReClca;

	private Panel panel3;

	private UltraLabel lb_Oth3;

	private UltraLabel lb_Realpay3;

	private UltraLabel lb_Res3;

	private UltraLabel lb_Duc3;

	private UltraLabel lb_Aldv3;

	private UltraLabel lb_ResTn3;

	private UltraLabel lb_Add3;

	private UltraLabel lb_Total3;

	private UltraLabel lb_Adv3;

	private UltraLabel lb_Iou3;

	private Panel panel4;

	private UltraLabel lb_Queue;

	private UltraLabel lb_CtrAmount;

	private UltraTextEditor Tb_ThisPrec;

	private UltraLabel ultraLabel14;

	private UltraLabel ultraLabel16;

	private UltraLabel lb_ThisPrec;

	private UltraButton btn_IndexNumber;

	private Container components = null;

	private double F_ContractTotal;

	private bool lb_Lock;

	private DataTable ldt_Acc;

	private string F_UserID;

	private string F_ProjectCode;

	private string F_SubProjectCode;

	private string F_Issue;

	private string F_TotalPrec;

	private UltraButton ultraButton1;

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

	public string __Duc2
	{
		set
		{
			Tb_Duc2.Text = value;
		}
	}

	public string __Add2
	{
		set
		{
			Tb_Add2.Text = value;
		}
	}

	public string _IndexNumTotal
	{
		set
		{
			Tb_Iou2.Text = value;
		}
	}

	public string _TotalPrec
	{
		get
		{
			return F_TotalPrec;
		}
		set
		{
			F_TotalPrec = value;
		}
	}

	public FormInvoiceSubAcInfo()
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
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Invoice.FormInvoiceSubAcInfo));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
		this.panel16 = new System.Windows.Forms.Panel();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.D_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.D_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
		this.btn_IndexNumber = new Infragistics.Win.Misc.UltraButton();
		this.btn_dudect = new Infragistics.Win.Misc.UltraButton();
		this.Tb_Adv2 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.cb_Aldv = new System.Windows.Forms.CheckBox();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel15 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_Total1 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_Adv1 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_Aldv1 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_Res1 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_ResTn1 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_Oth1 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_Iou1 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_Duc1 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_Add1 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_Realpay1 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.lb_Total2 = new Infragistics.Win.Misc.UltraLabel();
		this.cb_Res2 = new System.Windows.Forms.CheckBox();
		this.Tb_Aldv2 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.Tb_Res2 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.Tb_ResTn2 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.Tb_Oth2 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.Tb_Iou2 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.Tb_Duc2 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.Tb_Add2 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lb_Realpay2 = new Infragistics.Win.Misc.UltraLabel();
		this.btn_ReClca = new Infragistics.Win.Misc.UltraButton();
		this.panel3 = new System.Windows.Forms.Panel();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.lb_Oth3 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_Realpay3 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_Res3 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_Duc3 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_Aldv3 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_ResTn3 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_Add3 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_Total3 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_Adv3 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_Iou3 = new Infragistics.Win.Misc.UltraLabel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.lb_Queue = new Infragistics.Win.Misc.UltraLabel();
		this.lb_CtrAmount = new Infragistics.Win.Misc.UltraLabel();
		this.Tb_ThisPrec = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel16 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_ThisPrec = new Infragistics.Win.Misc.UltraLabel();
		this.panel16.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1Sizer1).BeginInit();
		this.c1Sizer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tb_Adv2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Tb_Aldv2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Tb_Res2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Tb_ResTn2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Tb_Oth2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Tb_Iou2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Tb_Duc2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Tb_Add2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Tb_ThisPrec).BeginInit();
		base.SuspendLayout();
		this.panel16.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel16.Controls.Add(this.groupBox6);
		this.panel16.Controls.Add(this.D_Btn_Cncl);
		this.panel16.Controls.Add(this.D_Btn_Next);
		this.panel16.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel16.Location = new System.Drawing.Point(0, 361);
		this.panel16.Name = "panel16";
		this.panel16.Size = new System.Drawing.Size(706, 44);
		this.panel16.TabIndex = 21;
		this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox6.Location = new System.Drawing.Point(0, 0);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(706, 8);
		this.groupBox6.TabIndex = 4;
		this.groupBox6.TabStop = false;
		this.D_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Cncl.Appearance = appearance1;
		this.D_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.D_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Cncl.Location = new System.Drawing.Point(610, 9);
		this.D_Btn_Cncl.Name = "D_Btn_Cncl";
		this.D_Btn_Cncl.ShowFocusRect = false;
		this.D_Btn_Cncl.ShowOutline = false;
		this.D_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Cncl.SupportThemes = false;
		this.D_Btn_Cncl.TabIndex = 2;
		this.D_Btn_Cncl.Text = "取消";
		this.D_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Next.Appearance = appearance2;
		this.D_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Next.Location = new System.Drawing.Point(514, 9);
		this.D_Btn_Next.Name = "D_Btn_Next";
		this.D_Btn_Next.ShowFocusRect = false;
		this.D_Btn_Next.ShowOutline = false;
		this.D_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Next.SupportThemes = false;
		this.D_Btn_Next.TabIndex = 1;
		this.D_Btn_Next.Text = "確定";
		this.D_Btn_Next.Click += new System.EventHandler(D_Btn_Next_Click);
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.Controls.Add(this.c1Sizer1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(706, 361);
		this.panel1.TabIndex = 22;
		this.c1Sizer1.AllowDrop = true;
		this.c1Sizer1.Controls.Add(this.ultraButton1);
		this.c1Sizer1.Controls.Add(this.btn_IndexNumber);
		this.c1Sizer1.Controls.Add(this.btn_dudect);
		this.c1Sizer1.Controls.Add(this.Tb_Adv2);
		this.c1Sizer1.Controls.Add(this.cb_Aldv);
		this.c1Sizer1.Controls.Add(this.ultraLabel1);
		this.c1Sizer1.Controls.Add(this.ultraLabel2);
		this.c1Sizer1.Controls.Add(this.ultraLabel3);
		this.c1Sizer1.Controls.Add(this.ultraLabel4);
		this.c1Sizer1.Controls.Add(this.ultraLabel5);
		this.c1Sizer1.Controls.Add(this.ultraLabel6);
		this.c1Sizer1.Controls.Add(this.ultraLabel7);
		this.c1Sizer1.Controls.Add(this.ultraLabel8);
		this.c1Sizer1.Controls.Add(this.ultraLabel9);
		this.c1Sizer1.Controls.Add(this.ultraLabel10);
		this.c1Sizer1.Controls.Add(this.ultraLabel11);
		this.c1Sizer1.Controls.Add(this.ultraLabel12);
		this.c1Sizer1.Controls.Add(this.ultraLabel13);
		this.c1Sizer1.Controls.Add(this.ultraLabel15);
		this.c1Sizer1.Controls.Add(this.lb_Total1);
		this.c1Sizer1.Controls.Add(this.lb_Adv1);
		this.c1Sizer1.Controls.Add(this.lb_Aldv1);
		this.c1Sizer1.Controls.Add(this.lb_Res1);
		this.c1Sizer1.Controls.Add(this.lb_ResTn1);
		this.c1Sizer1.Controls.Add(this.lb_Oth1);
		this.c1Sizer1.Controls.Add(this.lb_Iou1);
		this.c1Sizer1.Controls.Add(this.lb_Duc1);
		this.c1Sizer1.Controls.Add(this.lb_Add1);
		this.c1Sizer1.Controls.Add(this.lb_Realpay1);
		this.c1Sizer1.Controls.Add(this.panel2);
		this.c1Sizer1.Controls.Add(this.lb_Total2);
		this.c1Sizer1.Controls.Add(this.cb_Res2);
		this.c1Sizer1.Controls.Add(this.Tb_Aldv2);
		this.c1Sizer1.Controls.Add(this.Tb_Res2);
		this.c1Sizer1.Controls.Add(this.Tb_ResTn2);
		this.c1Sizer1.Controls.Add(this.Tb_Oth2);
		this.c1Sizer1.Controls.Add(this.Tb_Iou2);
		this.c1Sizer1.Controls.Add(this.Tb_Duc2);
		this.c1Sizer1.Controls.Add(this.Tb_Add2);
		this.c1Sizer1.Controls.Add(this.lb_Realpay2);
		this.c1Sizer1.Controls.Add(this.btn_ReClca);
		this.c1Sizer1.Controls.Add(this.panel3);
		this.c1Sizer1.Controls.Add(this.lb_Oth3);
		this.c1Sizer1.Controls.Add(this.lb_Realpay3);
		this.c1Sizer1.Controls.Add(this.lb_Res3);
		this.c1Sizer1.Controls.Add(this.lb_Duc3);
		this.c1Sizer1.Controls.Add(this.lb_Aldv3);
		this.c1Sizer1.Controls.Add(this.lb_ResTn3);
		this.c1Sizer1.Controls.Add(this.lb_Add3);
		this.c1Sizer1.Controls.Add(this.lb_Total3);
		this.c1Sizer1.Controls.Add(this.lb_Adv3);
		this.c1Sizer1.Controls.Add(this.lb_Iou3);
		this.c1Sizer1.Controls.Add(this.panel4);
		this.c1Sizer1.Controls.Add(this.lb_Queue);
		this.c1Sizer1.Controls.Add(this.lb_CtrAmount);
		this.c1Sizer1.Controls.Add(this.Tb_ThisPrec);
		this.c1Sizer1.Controls.Add(this.ultraLabel14);
		this.c1Sizer1.Controls.Add(this.ultraLabel16);
		this.c1Sizer1.Controls.Add(this.lb_ThisPrec);
		this.c1Sizer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1Sizer1.GridDefinition = "6.37119113573407:False:False;6.64819944598338:False:False;6.09418282548477:False:False;6.64819944598338:False:False;6.37119113573407:False:False;6.64819944598338:False:False;6.64819944598338:False:False;6.37119113573407:False:False;6.37119113573407:False:False;6.37119113573407:False:False;6.92520775623269:False:False;6.09418282548477:False:False;6.92520775623269:False:False;\t1.41643059490085:False:True;20.5382436260623:False:True;18.413597733711:False:True;18.413597733711:False:False;12.7478753541076:False:True;18.413597733711:False:True;3.54107648725212:False:True;1.41643059490085:False:True;";
		this.c1Sizer1.Location = new System.Drawing.Point(0, 0);
		this.c1Sizer1.Name = "c1Sizer1";
		this.c1Sizer1.Size = new System.Drawing.Size(706, 361);
		this.c1Sizer1.TabIndex = 0;
		this.c1Sizer1.TabStop = false;
		appearance3.TextHAlign = Infragistics.Win.HAlign.Left;
		this.btn_IndexNumber.Appearance = appearance3;
		this.btn_IndexNumber.BackColor = System.Drawing.SystemColors.Control;
		this.btn_IndexNumber.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btn_IndexNumber.Location = new System.Drawing.Point(435, 250);
		this.btn_IndexNumber.Name = "btn_IndexNumber";
		this.btn_IndexNumber.ShowFocusRect = false;
		this.btn_IndexNumber.ShowOutline = false;
		this.btn_IndexNumber.Size = new System.Drawing.Size(90, 23);
		this.btn_IndexNumber.SupportThemes = false;
		this.btn_IndexNumber.TabIndex = 7;
		this.btn_IndexNumber.Text = " 物調計算...";
		this.btn_IndexNumber.Click += new System.EventHandler(btn_IndexNumber_Click);
		appearance4.TextHAlign = Infragistics.Win.HAlign.Left;
		this.btn_dudect.Appearance = appearance4;
		this.btn_dudect.BackColor = System.Drawing.SystemColors.Control;
		this.btn_dudect.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btn_dudect.Location = new System.Drawing.Point(435, 277);
		this.btn_dudect.Name = "btn_dudect";
		this.btn_dudect.ShowFocusRect = false;
		this.btn_dudect.ShowOutline = false;
		this.btn_dudect.Size = new System.Drawing.Size(90, 25);
		this.btn_dudect.SupportThemes = false;
		this.btn_dudect.TabIndex = 4;
		this.btn_dudect.Text = " 應扣明細...";
		this.btn_dudect.Click += new System.EventHandler(btn_dudect_Click);
		this.Tb_Adv2.Location = new System.Drawing.Point(301, 113);
		this.Tb_Adv2.Name = "Tb_Adv2";
		this.Tb_Adv2.Size = new System.Drawing.Size(130, 24);
		this.Tb_Adv2.TabIndex = 3;
		this.Tb_Adv2.Text = "Tb_Adv2";
		this.cb_Aldv.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.cb_Aldv.Location = new System.Drawing.Point(435, 140);
		this.cb_Aldv.Name = "cb_Aldv";
		this.cb_Aldv.Size = new System.Drawing.Size(90, 24);
		this.cb_Aldv.TabIndex = 2;
		this.cb_Aldv.Text = "自動計算";
		this.cb_Aldv.CheckedChanged += new System.EventHandler(cb_Aldv_CheckedChanged);
		appearance5.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance5.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance5;
		this.ultraLabel1.Location = new System.Drawing.Point(18, 4);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(145, 23);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "估驗期別:";
		appearance6.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance6.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance6;
		this.ultraLabel2.Location = new System.Drawing.Point(18, 85);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(145, 24);
		this.ultraLabel2.TabIndex = 0;
		this.ultraLabel2.Text = "工程款:";
		appearance7.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance7;
		this.ultraLabel3.Location = new System.Drawing.Point(18, 113);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(145, 23);
		this.ultraLabel3.TabIndex = 0;
		this.ultraLabel3.Text = "預付款:";
		appearance8.ForeColor = System.Drawing.Color.Red;
		appearance8.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance8.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance8;
		this.ultraLabel4.Location = new System.Drawing.Point(18, 140);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(145, 24);
		this.ultraLabel4.TabIndex = 0;
		this.ultraLabel4.Text = "扣回預付款:";
		appearance9.ForeColor = System.Drawing.Color.Red;
		appearance9.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance9.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance9;
		this.ultraLabel5.Location = new System.Drawing.Point(18, 168);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(145, 24);
		this.ultraLabel5.TabIndex = 0;
		this.ultraLabel5.Text = "保留款:";
		appearance10.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance10.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel6.Appearance = appearance10;
		this.ultraLabel6.Location = new System.Drawing.Point(18, 196);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(145, 23);
		this.ultraLabel6.TabIndex = 0;
		this.ultraLabel6.Text = "退回保留款:";
		appearance11.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance11.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel7.Appearance = appearance11;
		this.ultraLabel7.Location = new System.Drawing.Point(18, 223);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(145, 23);
		this.ultraLabel7.TabIndex = 0;
		this.ultraLabel7.Text = "預付材料金額:";
		appearance12.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance12.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel8.Appearance = appearance12;
		this.ultraLabel8.Location = new System.Drawing.Point(18, 250);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(145, 23);
		this.ultraLabel8.TabIndex = 0;
		this.ultraLabel8.Text = "物價指數調整金額:";
		appearance13.ForeColor = System.Drawing.Color.Red;
		appearance13.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance13.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel9.Appearance = appearance13;
		this.ultraLabel9.Location = new System.Drawing.Point(18, 277);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(145, 25);
		this.ultraLabel9.TabIndex = 0;
		this.ultraLabel9.Text = "其他應扣金額:";
		appearance14.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance14.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel10.Appearance = appearance14;
		this.ultraLabel10.Location = new System.Drawing.Point(18, 306);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(145, 22);
		this.ultraLabel10.TabIndex = 0;
		this.ultraLabel10.Text = "其他應增金額:";
		appearance15.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance15.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel11.Appearance = appearance15;
		this.ultraLabel11.Location = new System.Drawing.Point(18, 332);
		this.ultraLabel11.Name = "ultraLabel11";
		this.ultraLabel11.Size = new System.Drawing.Size(145, 25);
		this.ultraLabel11.TabIndex = 0;
		this.ultraLabel11.Text = "估驗實付金額:";
		appearance16.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance16.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel12.Appearance = appearance16;
		this.ultraLabel12.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.ultraLabel12.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel12.Location = new System.Drawing.Point(167, 59);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(130, 22);
		this.ultraLabel12.TabIndex = 0;
		this.ultraLabel12.Text = "截至上期累計";
		appearance17.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance17.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel13.Appearance = appearance17;
		this.ultraLabel13.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel13.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel13.Location = new System.Drawing.Point(301, 59);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(224, 22);
		this.ultraLabel13.TabIndex = 0;
		this.ultraLabel13.Text = "本期";
		appearance18.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance18.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel15.Appearance = appearance18;
		this.ultraLabel15.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
		this.ultraLabel15.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel15.Location = new System.Drawing.Point(529, 59);
		this.ultraLabel15.Name = "ultraLabel15";
		this.ultraLabel15.Size = new System.Drawing.Size(159, 22);
		this.ultraLabel15.TabIndex = 0;
		this.ultraLabel15.Text = "截至本期共計";
		appearance19.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance19.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_Total1.Appearance = appearance19;
		this.lb_Total1.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.lb_Total1.Location = new System.Drawing.Point(167, 85);
		this.lb_Total1.Name = "lb_Total1";
		this.lb_Total1.Size = new System.Drawing.Size(130, 24);
		this.lb_Total1.TabIndex = 0;
		this.lb_Total1.Text = "[lb_Total1]";
		appearance20.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance20.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_Adv1.Appearance = appearance20;
		this.lb_Adv1.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.lb_Adv1.Location = new System.Drawing.Point(167, 113);
		this.lb_Adv1.Name = "lb_Adv1";
		this.lb_Adv1.Size = new System.Drawing.Size(130, 23);
		this.lb_Adv1.TabIndex = 0;
		this.lb_Adv1.Text = "[lb_Adv1]";
		appearance21.ForeColor = System.Drawing.Color.Red;
		appearance21.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance21.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_Aldv1.Appearance = appearance21;
		this.lb_Aldv1.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.lb_Aldv1.Location = new System.Drawing.Point(167, 140);
		this.lb_Aldv1.Name = "lb_Aldv1";
		this.lb_Aldv1.Size = new System.Drawing.Size(130, 24);
		this.lb_Aldv1.TabIndex = 0;
		this.lb_Aldv1.Text = "[lb_Aldv1]";
		appearance22.ForeColor = System.Drawing.Color.Red;
		appearance22.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance22.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_Res1.Appearance = appearance22;
		this.lb_Res1.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.lb_Res1.Location = new System.Drawing.Point(167, 168);
		this.lb_Res1.Name = "lb_Res1";
		this.lb_Res1.Size = new System.Drawing.Size(130, 24);
		this.lb_Res1.TabIndex = 0;
		this.lb_Res1.Text = "[lb_Res1]";
		appearance23.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance23.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_ResTn1.Appearance = appearance23;
		this.lb_ResTn1.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.lb_ResTn1.Location = new System.Drawing.Point(167, 196);
		this.lb_ResTn1.Name = "lb_ResTn1";
		this.lb_ResTn1.Size = new System.Drawing.Size(130, 23);
		this.lb_ResTn1.TabIndex = 0;
		this.lb_ResTn1.Text = "[lb_ResTn1]";
		appearance24.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance24.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_Oth1.Appearance = appearance24;
		this.lb_Oth1.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.lb_Oth1.Location = new System.Drawing.Point(167, 223);
		this.lb_Oth1.Name = "lb_Oth1";
		this.lb_Oth1.Size = new System.Drawing.Size(130, 23);
		this.lb_Oth1.TabIndex = 0;
		this.lb_Oth1.Text = "[lb_Oth1]";
		appearance25.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance25.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_Iou1.Appearance = appearance25;
		this.lb_Iou1.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.lb_Iou1.Location = new System.Drawing.Point(167, 250);
		this.lb_Iou1.Name = "lb_Iou1";
		this.lb_Iou1.Size = new System.Drawing.Size(130, 23);
		this.lb_Iou1.TabIndex = 0;
		this.lb_Iou1.Text = "[lb_Iou1]";
		appearance26.ForeColor = System.Drawing.Color.Red;
		appearance26.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance26.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_Duc1.Appearance = appearance26;
		this.lb_Duc1.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.lb_Duc1.Location = new System.Drawing.Point(167, 277);
		this.lb_Duc1.Name = "lb_Duc1";
		this.lb_Duc1.Size = new System.Drawing.Size(130, 25);
		this.lb_Duc1.TabIndex = 0;
		this.lb_Duc1.Text = "[lb_Duc1]";
		appearance27.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance27.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_Add1.Appearance = appearance27;
		this.lb_Add1.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.lb_Add1.Location = new System.Drawing.Point(167, 306);
		this.lb_Add1.Name = "lb_Add1";
		this.lb_Add1.Size = new System.Drawing.Size(130, 22);
		this.lb_Add1.TabIndex = 0;
		this.lb_Add1.Text = "[lb_Add1]";
		appearance28.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance28.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_Realpay1.Appearance = appearance28;
		this.lb_Realpay1.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.lb_Realpay1.Location = new System.Drawing.Point(167, 332);
		this.lb_Realpay1.Name = "lb_Realpay1";
		this.lb_Realpay1.Size = new System.Drawing.Size(130, 25);
		this.lb_Realpay1.TabIndex = 0;
		this.lb_Realpay1.Text = "[lb_Realpay1]";
		this.panel2.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.panel2.Location = new System.Drawing.Point(167, 59);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(130, 298);
		this.panel2.TabIndex = 1;
		appearance29.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance29.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_Total2.Appearance = appearance29;
		this.lb_Total2.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.lb_Total2.Location = new System.Drawing.Point(301, 85);
		this.lb_Total2.Name = "lb_Total2";
		this.lb_Total2.Size = new System.Drawing.Size(130, 24);
		this.lb_Total2.TabIndex = 0;
		this.lb_Total2.Text = "[lb_Total2]";
		this.cb_Res2.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.cb_Res2.Location = new System.Drawing.Point(435, 168);
		this.cb_Res2.Name = "cb_Res2";
		this.cb_Res2.Size = new System.Drawing.Size(90, 24);
		this.cb_Res2.TabIndex = 2;
		this.cb_Res2.Text = "自動計算";
		this.cb_Res2.CheckedChanged += new System.EventHandler(cb_Res2_CheckedChanged);
		appearance30.ForeColor = System.Drawing.Color.Red;
		this.Tb_Aldv2.Appearance = appearance30;
		this.Tb_Aldv2.Location = new System.Drawing.Point(301, 140);
		this.Tb_Aldv2.Name = "Tb_Aldv2";
		this.Tb_Aldv2.Size = new System.Drawing.Size(130, 24);
		this.Tb_Aldv2.TabIndex = 3;
		this.Tb_Aldv2.Text = "Tb_Aldv2";
		appearance31.ForeColor = System.Drawing.Color.Red;
		this.Tb_Res2.Appearance = appearance31;
		this.Tb_Res2.Location = new System.Drawing.Point(301, 168);
		this.Tb_Res2.Name = "Tb_Res2";
		this.Tb_Res2.Size = new System.Drawing.Size(130, 24);
		this.Tb_Res2.TabIndex = 3;
		this.Tb_Res2.Text = "Tb_Res2";
		this.Tb_ResTn2.Location = new System.Drawing.Point(301, 196);
		this.Tb_ResTn2.Name = "Tb_ResTn2";
		this.Tb_ResTn2.Size = new System.Drawing.Size(130, 24);
		this.Tb_ResTn2.TabIndex = 3;
		this.Tb_ResTn2.Text = "Tb_ResTn2";
		this.Tb_Oth2.Location = new System.Drawing.Point(301, 223);
		this.Tb_Oth2.Name = "Tb_Oth2";
		this.Tb_Oth2.Size = new System.Drawing.Size(130, 24);
		this.Tb_Oth2.TabIndex = 3;
		this.Tb_Oth2.Text = "Tb_Oth2";
		this.Tb_Iou2.Location = new System.Drawing.Point(301, 250);
		this.Tb_Iou2.Name = "Tb_Iou2";
		this.Tb_Iou2.Size = new System.Drawing.Size(130, 24);
		this.Tb_Iou2.TabIndex = 3;
		this.Tb_Iou2.Text = "Tb_Iou2";
		appearance32.ForeColor = System.Drawing.Color.Red;
		this.Tb_Duc2.Appearance = appearance32;
		this.Tb_Duc2.Location = new System.Drawing.Point(301, 277);
		this.Tb_Duc2.Name = "Tb_Duc2";
		this.Tb_Duc2.Size = new System.Drawing.Size(130, 24);
		this.Tb_Duc2.TabIndex = 3;
		this.Tb_Duc2.Text = "Tb_Duc2";
		this.Tb_Add2.Location = new System.Drawing.Point(301, 306);
		this.Tb_Add2.Name = "Tb_Add2";
		this.Tb_Add2.Size = new System.Drawing.Size(130, 24);
		this.Tb_Add2.TabIndex = 3;
		this.Tb_Add2.Text = "Tb_Add2";
		appearance33.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance33.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_Realpay2.Appearance = appearance33;
		this.lb_Realpay2.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.lb_Realpay2.Location = new System.Drawing.Point(301, 332);
		this.lb_Realpay2.Name = "lb_Realpay2";
		this.lb_Realpay2.Size = new System.Drawing.Size(130, 25);
		this.lb_Realpay2.TabIndex = 0;
		this.lb_Realpay2.Text = "[lb_Total2]";
		this.btn_ReClca.BackColor = System.Drawing.SystemColors.Control;
		this.btn_ReClca.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btn_ReClca.Location = new System.Drawing.Point(18, 31);
		this.btn_ReClca.Name = "btn_ReClca";
		this.btn_ReClca.ShowFocusRect = false;
		this.btn_ReClca.ShowOutline = false;
		this.btn_ReClca.Size = new System.Drawing.Size(145, 24);
		this.btn_ReClca.SupportThemes = false;
		this.btn_ReClca.TabIndex = 4;
		this.btn_ReClca.Text = "重新計算";
		this.btn_ReClca.Click += new System.EventHandler(btn_ReClca_Click);
		this.panel3.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.panel3.Location = new System.Drawing.Point(301, 59);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(224, 269);
		this.panel3.TabIndex = 5;
		appearance34.TextHAlign = Infragistics.Win.HAlign.Left;
		this.ultraButton1.Appearance = appearance34;
		this.ultraButton1.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton1.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ultraButton1.Location = new System.Drawing.Point(435, 306);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(90, 22);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 5;
		this.ultraButton1.Text = " 應增明細...";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		appearance35.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance35.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_Oth3.Appearance = appearance35;
		this.lb_Oth3.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
		this.lb_Oth3.Location = new System.Drawing.Point(529, 223);
		this.lb_Oth3.Name = "lb_Oth3";
		this.lb_Oth3.Size = new System.Drawing.Size(159, 23);
		this.lb_Oth3.TabIndex = 0;
		this.lb_Oth3.Text = "[lb_Oth3]";
		appearance36.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance36.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_Realpay3.Appearance = appearance36;
		this.lb_Realpay3.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
		this.lb_Realpay3.Location = new System.Drawing.Point(529, 332);
		this.lb_Realpay3.Name = "lb_Realpay3";
		this.lb_Realpay3.Size = new System.Drawing.Size(159, 25);
		this.lb_Realpay3.TabIndex = 0;
		this.lb_Realpay3.Text = "[lb_Realpay3]";
		appearance37.ForeColor = System.Drawing.Color.Red;
		appearance37.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance37.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_Res3.Appearance = appearance37;
		this.lb_Res3.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
		this.lb_Res3.Location = new System.Drawing.Point(529, 168);
		this.lb_Res3.Name = "lb_Res3";
		this.lb_Res3.Size = new System.Drawing.Size(159, 24);
		this.lb_Res3.TabIndex = 0;
		this.lb_Res3.Text = "[lb_Res3]";
		appearance38.ForeColor = System.Drawing.Color.Red;
		appearance38.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance38.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_Duc3.Appearance = appearance38;
		this.lb_Duc3.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
		this.lb_Duc3.Location = new System.Drawing.Point(529, 277);
		this.lb_Duc3.Name = "lb_Duc3";
		this.lb_Duc3.Size = new System.Drawing.Size(159, 25);
		this.lb_Duc3.TabIndex = 0;
		this.lb_Duc3.Text = "[lb_Duc3]";
		appearance39.ForeColor = System.Drawing.Color.Red;
		appearance39.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance39.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_Aldv3.Appearance = appearance39;
		this.lb_Aldv3.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
		this.lb_Aldv3.Location = new System.Drawing.Point(529, 140);
		this.lb_Aldv3.Name = "lb_Aldv3";
		this.lb_Aldv3.Size = new System.Drawing.Size(159, 24);
		this.lb_Aldv3.TabIndex = 0;
		this.lb_Aldv3.Text = "[lb_Aldv3]";
		appearance40.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance40.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_ResTn3.Appearance = appearance40;
		this.lb_ResTn3.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
		this.lb_ResTn3.Location = new System.Drawing.Point(529, 196);
		this.lb_ResTn3.Name = "lb_ResTn3";
		this.lb_ResTn3.Size = new System.Drawing.Size(159, 23);
		this.lb_ResTn3.TabIndex = 0;
		this.lb_ResTn3.Text = "[lb_ResTn3]";
		appearance41.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance41.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_Add3.Appearance = appearance41;
		this.lb_Add3.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
		this.lb_Add3.Location = new System.Drawing.Point(529, 306);
		this.lb_Add3.Name = "lb_Add3";
		this.lb_Add3.Size = new System.Drawing.Size(159, 22);
		this.lb_Add3.TabIndex = 0;
		this.lb_Add3.Text = "[lb_Add3]";
		appearance42.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance42.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_Total3.Appearance = appearance42;
		this.lb_Total3.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
		this.lb_Total3.Location = new System.Drawing.Point(529, 85);
		this.lb_Total3.Name = "lb_Total3";
		this.lb_Total3.Size = new System.Drawing.Size(159, 24);
		this.lb_Total3.TabIndex = 0;
		this.lb_Total3.Text = "[lb_Total3]";
		appearance43.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance43.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_Adv3.Appearance = appearance43;
		this.lb_Adv3.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
		this.lb_Adv3.Location = new System.Drawing.Point(529, 113);
		this.lb_Adv3.Name = "lb_Adv3";
		this.lb_Adv3.Size = new System.Drawing.Size(159, 23);
		this.lb_Adv3.TabIndex = 0;
		this.lb_Adv3.Text = "[lb_Adv3]";
		appearance44.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance44.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_Iou3.Appearance = appearance44;
		this.lb_Iou3.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
		this.lb_Iou3.Location = new System.Drawing.Point(529, 250);
		this.lb_Iou3.Name = "lb_Iou3";
		this.lb_Iou3.Size = new System.Drawing.Size(159, 23);
		this.lb_Iou3.TabIndex = 0;
		this.lb_Iou3.Text = "[lb_Iou3]";
		this.panel4.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
		this.panel4.Location = new System.Drawing.Point(529, 59);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(159, 298);
		this.panel4.TabIndex = 6;
		appearance45.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance45.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_Queue.Appearance = appearance45;
		this.lb_Queue.BackColor = System.Drawing.Color.White;
		this.lb_Queue.Location = new System.Drawing.Point(167, 4);
		this.lb_Queue.Name = "lb_Queue";
		this.lb_Queue.Size = new System.Drawing.Size(130, 23);
		this.lb_Queue.TabIndex = 0;
		this.lb_Queue.Text = "[lb_Queue]";
		appearance46.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance46.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_CtrAmount.Appearance = appearance46;
		this.lb_CtrAmount.BackColor = System.Drawing.Color.White;
		this.lb_CtrAmount.Location = new System.Drawing.Point(167, 31);
		this.lb_CtrAmount.Name = "lb_CtrAmount";
		this.lb_CtrAmount.Size = new System.Drawing.Size(358, 24);
		this.lb_CtrAmount.TabIndex = 0;
		this.lb_CtrAmount.Text = "[lb_CtrAmount]";
		this.Tb_ThisPrec.Location = new System.Drawing.Point(529, 4);
		this.Tb_ThisPrec.Name = "Tb_ThisPrec";
		this.Tb_ThisPrec.Size = new System.Drawing.Size(130, 24);
		this.Tb_ThisPrec.TabIndex = 3;
		this.Tb_ThisPrec.Text = "Tb_ThisPrec";
		appearance47.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance47.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel14.Appearance = appearance47;
		this.ultraLabel14.Location = new System.Drawing.Point(663, 4);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(25, 23);
		this.ultraLabel14.TabIndex = 0;
		this.ultraLabel14.Text = "%";
		appearance48.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance48.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel16.Appearance = appearance48;
		this.ultraLabel16.Location = new System.Drawing.Point(301, 4);
		this.ultraLabel16.Name = "ultraLabel16";
		this.ultraLabel16.Size = new System.Drawing.Size(130, 23);
		this.ultraLabel16.TabIndex = 0;
		this.ultraLabel16.Text = "本期進度:";
		appearance49.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance49.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_ThisPrec.Appearance = appearance49;
		this.lb_ThisPrec.BackColor = System.Drawing.Color.White;
		this.lb_ThisPrec.Location = new System.Drawing.Point(435, 4);
		this.lb_ThisPrec.Name = "lb_ThisPrec";
		this.lb_ThisPrec.Size = new System.Drawing.Size(90, 23);
		this.lb_ThisPrec.TabIndex = 0;
		this.lb_ThisPrec.Text = "[lb_ThisPrec]";
		this.lb_ThisPrec.Visible = false;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.CancelButton = this.D_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(706, 405);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel16);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.Name = "FormInvoiceSubAcInfo";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "本期總計金額";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormInvoiceSubAcInfo_KeyDown);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormInvoiceSubAcInfo_FormClosing);
		base.Load += new System.EventHandler(FormInvoiceSubAcInfo_Load);
		this.panel16.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1Sizer1).EndInit();
		this.c1Sizer1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tb_Adv2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Tb_Aldv2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Tb_Res2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Tb_ResTn2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Tb_Oth2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Tb_Iou2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Tb_Duc2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Tb_Add2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Tb_ThisPrec).EndInit();
		base.ResumeLayout(false);
	}

	private void FormInvoiceSubAcInfo_Load(object sender, EventArgs e)
	{
		GetIssueData();
		LoadingScreen();
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("InvoiceSubAcInfo", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("InvoiceSubAcInfo", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("InvoiceSubAcInfo", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("InvoiceSubAcInfo", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("InvoiceSubAcInfo", "PK_Height"));
		if (iLoc_X > 0 && iLoc_Y > 0)
		{
			base.Location = new Point(iLoc_X, iLoc_Y);
		}
		if (iSiz_W > 0)
		{
			base.Width = iSiz_W;
		}
		if (iSiz_H > 0)
		{
			base.Height = iSiz_H;
		}
	}

	private void GetIssueData()
	{
		lb_Queue.Text = "第 " + F_Issue + " 期";
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("讀取--本期總計");
		sub_acc AccCom = new sub_acc(tmp_AL1);
		lb_Lock = AccCom.GetLockMode("9999", F_SubProjectCode, F_ProjectCode);
		AccCom = null;
		sub_acc SubAccCom = new sub_acc(tmp_AL1);
		double ld_Amount = (F_ContractTotal = SubAccCom.CtrAmount(F_Issue, F_SubProjectCode, F_ProjectCode));
		lb_CtrAmount.Text = "契約金額： " + PubTools.StrFormat(ld_Amount, 0);
		SubAccCom = null;
		BindData();
	}

	private void BindData()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("讀取--本期總計");
		sub_info SubInfoCom = new sub_info(tmp_AL1);
		DataTable ldt_Info = SubInfoCom.ListItem(F_SubProjectCode, F_ProjectCode);
		SubInfoCom = null;
		sub_acc SubAccCom = new sub_acc(tmp_AL1);
		ldt_Acc = SubAccCom.ListItem("", F_SubProjectCode, F_ProjectCode);
		SubAccCom = null;
		double ld_RealProjADV = 0.0;
		if (ldt_Info.Rows.Count > 0)
		{
			ld_RealProjADV = Convert.ToDouble(ldt_Info.Rows[0]["ProjADV"].ToString());
		}
		double ld_Total1 = 0.0;
		double ld_Total2 = 0.0;
		double ld_Adv1 = 0.0;
		double ld_Adv2 = 0.0;
		double ld_Aldv1 = 0.0;
		double ld_Aldv2 = 0.0;
		double ld_Res1 = 0.0;
		double ld_Res2 = 0.0;
		double ld_ResTn1 = 0.0;
		double ld_ResTn2 = 0.0;
		double ld_Oth1 = 0.0;
		double ld_Oth2 = 0.0;
		double ld_Iou1 = 0.0;
		double ld_Iou2 = 0.0;
		double ld_Duc1 = 0.0;
		double ld_Duc2 = 0.0;
		double ld_Add1 = 0.0;
		double ld_Add2 = 0.0;
		double ld_Realpay1 = 0.0;
		double ld_Realpay2 = 0.0;
		foreach (DataRow dr in ldt_Acc.Rows)
		{
			if (PubTools.Str2Int(dr["queue"].ToString()) == PubTools.Str2Int(F_Issue))
			{
				Tb_ThisPrec.Text = dr["This_Prec"].ToString();
				lb_ThisPrec.Text = dr["This_Prec"].ToString();
				if (dr["AutoAldv"].ToString() == "0")
				{
					cb_Aldv.Checked = false;
				}
				else
				{
					cb_Aldv.Checked = true;
				}
				if (dr["AutoRes"].ToString() == "0")
				{
					cb_Res2.Checked = false;
				}
				else
				{
					cb_Res2.Checked = true;
				}
				ld_Total2 = PubTools.Str2Double(dr["AccTotal"].ToString());
				ld_Adv2 = PubTools.Str2Double(dr["Advancepay"].ToString());
				ld_Aldv2 = PubTools.Str2Double(dr["Advance"].ToString());
				ld_Res2 = PubTools.Str2Double(dr["Reserve"].ToString());
				ld_ResTn2 = PubTools.Str2Double(dr["Reservertn"].ToString());
				ld_Oth2 = PubTools.Str2Double(dr["Material"].ToString());
				ld_Iou2 = PubTools.Str2Double(dr["Indexmat"].ToString());
				ld_Duc2 = PubTools.Str2Double(dr["Deduct"].ToString());
				ld_Add2 = PubTools.Str2Double(dr["AccAdd"].ToString());
				ld_Realpay2 = PubTools.Str2Double(dr["Realpay"].ToString());
			}
			if (PubTools.Str2Int(dr["queue"].ToString()) < PubTools.Str2Int(F_Issue))
			{
				ld_Total1 += PubTools.Str2Double(dr["AccTotal"].ToString());
				ld_Adv1 += PubTools.Str2Double(dr["Advancepay"].ToString());
				ld_Aldv1 += PubTools.Str2Double(dr["Advance"].ToString());
				ld_Res1 += PubTools.Str2Double(dr["Reserve"].ToString());
				ld_ResTn1 += PubTools.Str2Double(dr["Reservertn"].ToString());
				ld_Oth1 += PubTools.Str2Double(dr["Material"].ToString());
				ld_Iou1 += PubTools.Str2Double(dr["Indexmat"].ToString());
				ld_Duc1 += PubTools.Str2Double(dr["Deduct"].ToString());
				ld_Add1 += PubTools.Str2Double(dr["AccAdd"].ToString());
				ld_Realpay1 += PubTools.Str2Double(dr["Realpay"].ToString());
			}
		}
		lb_Total1.Text = PubTools.StrFormat(ld_Total1, 0);
		lb_Total2.Text = PubTools.StrFormat(ld_Total2, 0);
		lb_Total3.Text = PubTools.StrFormat(ld_Total1 + ld_Total2, 0);
		if (ld_Aldv1 > ld_RealProjADV)
		{
			lb_Aldv1.Text = PubTools.StrFormat(ld_RealProjADV, 0);
			Tb_Aldv2.Text = PubTools.StrFormat(0.0, 0);
			lb_Aldv3.Text = PubTools.StrFormat(ld_RealProjADV, 0);
		}
		else
		{
			lb_Aldv1.Text = PubTools.StrFormat(ld_Aldv1, 0);
			if (ld_Aldv1 + ld_Aldv2 > ld_RealProjADV)
			{
				Tb_Aldv2.Text = PubTools.StrFormat(ld_RealProjADV - ld_Aldv1, 0);
				lb_Aldv3.Text = PubTools.StrFormat(ld_RealProjADV, 0);
			}
			else
			{
				Tb_Aldv2.Text = PubTools.StrFormat(ld_Aldv2, 0);
				lb_Aldv3.Text = PubTools.StrFormat(ld_Aldv1 + ld_Aldv2, 0);
			}
		}
		lb_Adv1.Text = PubTools.StrFormat(ld_Adv1, 0);
		Tb_Adv2.Text = PubTools.StrFormat(ld_Adv2, 0);
		lb_Adv3.Text = PubTools.StrFormat(ld_Adv1 + ld_Adv2, 0);
		lb_Res1.Text = PubTools.StrFormat(ld_Res1, 0);
		Tb_Res2.Text = PubTools.StrFormat(ld_Res2, 0);
		lb_Res3.Text = PubTools.StrFormat(ld_Res1 + ld_Res2, 0);
		lb_ResTn1.Text = PubTools.StrFormat(ld_ResTn1, 0);
		Tb_ResTn2.Text = PubTools.StrFormat(ld_ResTn2, 0);
		lb_ResTn3.Text = PubTools.StrFormat(ld_ResTn1 + ld_ResTn2, 0);
		lb_Oth1.Text = PubTools.StrFormat(ld_Oth1, 0);
		Tb_Oth2.Text = PubTools.StrFormat(ld_Oth2, 0);
		lb_Oth3.Text = PubTools.StrFormat(ld_Oth1 + ld_Oth2, 0);
		lb_Iou1.Text = PubTools.StrFormat(ld_Iou1, 0);
		Tb_Iou2.Text = PubTools.StrFormat(ld_Iou2, 0);
		lb_Iou3.Text = PubTools.StrFormat(ld_Iou1 + ld_Iou2, 0);
		lb_Duc1.Text = PubTools.StrFormat(ld_Duc1, 0);
		Tb_Duc2.Text = PubTools.StrFormat(ld_Duc2, 0);
		lb_Duc3.Text = PubTools.StrFormat(ld_Duc1 + ld_Duc2, 0);
		lb_Add1.Text = PubTools.StrFormat(ld_Add1, 0);
		Tb_Add2.Text = PubTools.StrFormat(ld_Add2, 0);
		lb_Add3.Text = PubTools.StrFormat(ld_Add1 + ld_Add2, 0);
		lb_Realpay1.Text = PubTools.StrFormat(ld_Realpay1, 0);
		lb_Realpay2.Text = PubTools.StrFormat(ld_Realpay2, 0);
		lb_Realpay3.Text = PubTools.StrFormat(ld_Realpay1 + ld_Realpay2, 0);
		if (lb_Lock)
		{
			Tb_ThisPrec.ReadOnly = true;
			Tb_Adv2.ReadOnly = true;
			Tb_Aldv2.ReadOnly = true;
			Tb_Res2.ReadOnly = true;
			Tb_ResTn2.ReadOnly = true;
			Tb_Oth2.ReadOnly = true;
			Tb_Iou2.ReadOnly = true;
			Tb_Duc2.ReadOnly = true;
			Tb_Add2.ReadOnly = true;
			btn_ReClca.Enabled = false;
			cb_Aldv.Enabled = false;
			cb_Res2.Enabled = false;
		}
	}

	private void cb_Aldv_CheckedChanged(object sender, EventArgs e)
	{
		string ls_mode = "1";
		ls_mode = ((!cb_Aldv.Checked) ? "0" : "1");
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("本期總計");
		sub_acc SubAccCom = new sub_acc(tmp_AL1);
		SubAccCom.SetAutoAldv(ls_mode, F_Issue, F_SubProjectCode, F_ProjectCode);
		SubAccCom = null;
	}

	private void cb_Res2_CheckedChanged(object sender, EventArgs e)
	{
		string ls_mode = "1";
		ls_mode = ((!cb_Res2.Checked) ? "0" : "1");
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("本期總計");
		sub_acc SubAccCom = new sub_acc(tmp_AL1);
		SubAccCom.SetAutoRes(ls_mode, F_Issue, F_SubProjectCode, F_ProjectCode);
		SubAccCom = null;
	}

	private void btn_ReClca_Click(object sender, EventArgs e)
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("本期總計--重新計算");
		sub_acc SubAccCom = new sub_acc(tmp_AL1);
		if (!cb_Res2.Checked)
		{
			SubAccCom.ps_reserve = PubTools.Str2Double(Tb_Res2.Text).ToString();
		}
		if (!cb_Aldv.Checked)
		{
			SubAccCom.ps_advance = PubTools.Str2Double(Tb_Aldv2.Text).ToString();
		}
		SubAccCom.ps_Advancepay = PubTools.Str2Double(Tb_Adv2.Text).ToString();
		SubAccCom.ps_Reservertn = PubTools.Str2Double(Tb_ResTn2.Text).ToString();
		SubAccCom.ps_material = PubTools.Str2Double(Tb_Oth2.Text).ToString();
		SubAccCom.ps_indexmat = PubTools.Str2Double(Tb_Iou2.Text).ToString();
		SubAccCom.ps_deduct = PubTools.Str2Double(Tb_Duc2.Text).ToString();
		SubAccCom.ps_accadd = PubTools.Str2Double(Tb_Add2.Text).ToString();
		SubAccCom.ps_prjcode = F_ProjectCode;
		SubAccCom.ps_subcode = F_SubProjectCode;
		SubAccCom.ps_queue = F_Issue;
		SubAccCom.UpdItem();
		if (PubTools.Str2Double(Tb_ThisPrec.Text) != PubTools.Str2Double(lb_ThisPrec.Text))
		{
			SubAccCom.SetThisPrec(F_Issue, F_SubProjectCode, F_ProjectCode, PubTools.Str2Double(Tb_ThisPrec.Text));
		}
		else
		{
			SubAccCom.CtrInsp(F_Issue, F_SubProjectCode, F_ProjectCode);
		}
		BindData();
	}

	private void btn_dudect_Click(object sender, EventArgs e)
	{
		FormInvoiceDec2 FM_INVDEC = new FormInvoiceDec2();
		FM_INVDEC._ProjectCode = F_ProjectCode;
		FM_INVDEC._SubProjectCode = F_SubProjectCode;
		FM_INVDEC._UserID = F_UserID;
		FM_INVDEC._Issue = F_Issue;
		FM_INVDEC._flag = "-";
		FM_INVDEC.Owner = this;
		FM_INVDEC.ShowDialog();
		FM_INVDEC.Close();
		FM_INVDEC.Dispose();
		FM_INVDEC = null;
		btn_ReClca_Click(null, null);
	}

	private void D_Btn_Next_Click(object sender, EventArgs e)
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("本期總計--重新計算");
		sub_acc SubAccCom = new sub_acc(tmp_AL1);
		if (!cb_Res2.Checked)
		{
			SubAccCom.ps_reserve = PubTools.Str2Double(Tb_Res2.Text).ToString();
		}
		if (!cb_Aldv.Checked)
		{
			SubAccCom.ps_advance = PubTools.Str2Double(Tb_Aldv2.Text).ToString();
		}
		SubAccCom.ps_Advancepay = PubTools.Str2Double(Tb_Adv2.Text).ToString();
		SubAccCom.ps_Reservertn = PubTools.Str2Double(Tb_ResTn2.Text).ToString();
		SubAccCom.ps_material = PubTools.Str2Double(Tb_Oth2.Text).ToString();
		SubAccCom.ps_indexmat = PubTools.Str2Double(Tb_Iou2.Text).ToString();
		SubAccCom.ps_deduct = PubTools.Str2Double(Tb_Duc2.Text).ToString();
		SubAccCom.ps_accadd = PubTools.Str2Double(Tb_Add2.Text).ToString();
		SubAccCom.ps_prjcode = F_ProjectCode;
		SubAccCom.ps_subcode = F_SubProjectCode;
		SubAccCom.ps_queue = F_Issue;
		SubAccCom.UpdItem();
		if (PubTools.Str2Double(Tb_ThisPrec.Text) != PubTools.Str2Double(lb_ThisPrec.Text))
		{
			SubAccCom.SetThisPrec(F_Issue, F_SubProjectCode, F_ProjectCode, PubTools.Str2Double(Tb_ThisPrec.Text));
		}
		else
		{
			SubAccCom.CtrInsp(F_Issue, F_SubProjectCode, F_ProjectCode);
		}
		base.DialogResult = DialogResult.OK;
	}

	private void btn_IndexNumber_Click(object sender, EventArgs e)
	{
		FormInvoiceIndexNumber FM_INDEX = new FormInvoiceIndexNumber();
		FM_INDEX._UserID = F_UserID;
		FM_INDEX._ProjectCode = F_ProjectCode;
		FM_INDEX._ActionName = PccesFormAction.Invoice;
		FM_INDEX._Issue = F_Issue;
		FM_INDEX._AccAdv = lb_Adv3.Text;
		FM_INDEX._ContractTotal = F_ContractTotal.ToString();
		FM_INDEX.Owner = this;
		FM_INDEX.ShowDialog();
		FM_INDEX.Close();
		FM_INDEX.Dispose();
		FM_INDEX = null;
	}

	private void FormInvoiceSubAcInfo_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("InvoiceSubAcInfo", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("InvoiceSubAcInfo", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("InvoiceSubAcInfo", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("InvoiceSubAcInfo", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("InvoiceSubAcInfo", "WindowState", base.WindowState.ToString());
	}

	private void FormInvoiceSubAcInfo_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormInvoiceSubAcInfo");
		}
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		FormInvoiceDec2 FM_INVDEC = new FormInvoiceDec2();
		FM_INVDEC._ProjectCode = F_ProjectCode;
		FM_INVDEC._SubProjectCode = F_SubProjectCode;
		FM_INVDEC._UserID = F_UserID;
		FM_INVDEC._Issue = F_Issue;
		FM_INVDEC._flag = "+";
		FM_INVDEC.Owner = this;
		FM_INVDEC.ShowDialog();
		FM_INVDEC.Close();
		FM_INVDEC = null;
		btn_ReClca_Click(null, null);
	}
}
