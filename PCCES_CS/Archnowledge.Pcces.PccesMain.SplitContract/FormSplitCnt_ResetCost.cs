using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.BusinessLogical;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.DomainModule.LogicalBase;
using Archnowledge.Pcces.DomainModule.Sub;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.SplitContract;

public class FormSplitCnt_ResetCost : Form
{
	private const string CallFormHelp = "FormSplitCnt_ResetCost";

	private UltraLabel lblTotal;

	private UltraLabel ultraLabel3;

	private UltraTextEditor txtRatio;

	private UltraTextEditor txtAmount;

	private UltraLabel ultraLabel6;

	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_A;

	private UltraTabPageControl Tab_B;

	private Panel panel5;

	private UltraButton ultraButton4;

	private UltraButton BtnPick;

	private UltraLabel lbMessage;

	private UltraLabel ultraLabel4;

	private Panel panel1;

	private UltraButton ultraButton2;

	private UltraLabel ultraLabel16;

	private UltraTabPageControl Tab_C;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel18;

	private UltraLabel ultraLabel17;

	private UltraCheckEditor CB_UnRestoreCost;

	private System.Windows.Forms.ToolTip toolTip1;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel8;

	private RadioButton RB2;

	private RadioButton RB1;

	private UltraLabel lblOldTotal;

	private UltraLabel ultraLabel19;

	private PccesFormAction F_ActionName = PccesFormAction.SplitContract;

	private string F_UserID;

	private double F_TotalAmount;

	private string F_ProjectCode;

	private Archnowledge.Pcces.DomainModule.LogicalBase.Project theProject = new SubProject();

	private double F_OldTotalAmount;

	private IContainer components;

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

	public double _TotalAmount
	{
		get
		{
			return F_TotalAmount;
		}
		set
		{
			F_TotalAmount = value;
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

	public double _OldTotalAmount
	{
		get
		{
			return F_OldTotalAmount;
		}
		set
		{
			F_OldTotalAmount = value;
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
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.SplitContract.FormSplitCnt_ResetCost));
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
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.RB1 = new System.Windows.Forms.RadioButton();
		this.RB2 = new System.Windows.Forms.RadioButton();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel18 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel17 = new Infragistics.Win.Misc.UltraLabel();
		this.CB_UnRestoreCost = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraButton4 = new Infragistics.Win.Misc.UltraButton();
		this.BtnPick = new Infragistics.Win.Misc.UltraButton();
		this.txtAmount = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.lblTotal = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.txtRatio = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lblOldTotal = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.lbMessage = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_C = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraLabel16 = new Infragistics.Win.Misc.UltraLabel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.ultraLabel19 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_A.SuspendLayout();
		this.panel5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRatio).BeginInit();
		this.Tab_B.SuspendLayout();
		this.Tab_C.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.RB1);
		this.Tab_A.Controls.Add(this.RB2);
		this.Tab_A.Controls.Add(this.ultraLabel7);
		this.Tab_A.Controls.Add(this.ultraLabel5);
		this.Tab_A.Controls.Add(this.ultraLabel18);
		this.Tab_A.Controls.Add(this.ultraLabel17);
		this.Tab_A.Controls.Add(this.CB_UnRestoreCost);
		this.Tab_A.Controls.Add(this.ultraLabel1);
		this.Tab_A.Controls.Add(this.panel5);
		this.Tab_A.Controls.Add(this.txtAmount);
		this.Tab_A.Controls.Add(this.ultraLabel6);
		this.Tab_A.Controls.Add(this.lblTotal);
		this.Tab_A.Controls.Add(this.ultraLabel3);
		this.Tab_A.Controls.Add(this.txtRatio);
		this.Tab_A.Controls.Add(this.lblOldTotal);
		this.Tab_A.Controls.Add(this.ultraLabel8);
		this.Tab_A.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(376, 326);
		this.RB1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.RB1.Checked = true;
		this.RB1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.RB1.Location = new System.Drawing.Point(16, 56);
		this.RB1.Name = "RB1";
		this.RB1.Size = new System.Drawing.Size(216, 24);
		this.RB1.TabIndex = 39;
		this.RB1.TabStop = true;
		this.RB1.Text = "總價比例調整";
		this.RB1.UseVisualStyleBackColor = false;
		this.RB2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.RB2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.RB2.Location = new System.Drawing.Point(16, 256);
		this.RB2.Name = "RB2";
		this.RB2.Size = new System.Drawing.Size(216, 24);
		this.RB2.TabIndex = 38;
		this.RB2.Text = "總價回復";
		this.RB2.UseVisualStyleBackColor = false;
		appearance1.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel7.Appearance = appearance1;
		this.ultraLabel7.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraLabel7.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel7.Location = new System.Drawing.Point(0, 184);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(139, 23);
		this.ultraLabel7.TabIndex = 37;
		this.ultraLabel7.Text = "調整後總金額:";
		appearance2.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance2;
		this.ultraLabel5.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraLabel5.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel5.Location = new System.Drawing.Point(0, 152);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(139, 23);
		this.ultraLabel5.TabIndex = 36;
		this.ultraLabel5.Text = "調整比例:";
		this.ultraLabel18.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel18.Location = new System.Drawing.Point(136, 8);
		this.ultraLabel18.Name = "ultraLabel18";
		this.ultraLabel18.Size = new System.Drawing.Size(228, 28);
		this.ultraLabel18.TabIndex = 35;
		this.ultraLabel18.Text = "依目前總價來進行調價，打折前不先回復成最原始總價";
		this.toolTip1.SetToolTip(this.ultraLabel18, "勾選此一方式：使用者將以最後調出來的價格，再繼續進行調價(即可累進式調價)。");
		appearance3.ForeColor = System.Drawing.Color.Red;
		this.ultraLabel17.Appearance = appearance3;
		this.ultraLabel17.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel17.Location = new System.Drawing.Point(136, 40);
		this.ultraLabel17.Name = "ultraLabel17";
		this.ultraLabel17.Size = new System.Drawing.Size(228, 23);
		this.ultraLabel17.TabIndex = 34;
		this.ultraLabel17.Text = "(勾選此種方式無法回復到最原始的總價)";
		this.ultraLabel17.Visible = false;
		appearance4.FontData.SizeInPoints = 9f;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.CB_UnRestoreCost.Appearance = appearance4;
		this.CB_UnRestoreCost.Location = new System.Drawing.Point(120, 1);
		this.CB_UnRestoreCost.Name = "CB_UnRestoreCost";
		this.CB_UnRestoreCost.Size = new System.Drawing.Size(16, 26);
		this.CB_UnRestoreCost.TabIndex = 33;
		appearance5.ForeColor = System.Drawing.Color.FromArgb(255, 128, 0);
		this.ultraLabel1.Appearance = appearance5;
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(32, 216);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(307, 32);
		this.ultraLabel1.TabIndex = 32;
		this.ultraLabel1.Text = "調整後的金額，會因為取位原則，以致調整後的總金額不會相等於，上面粗算的金額。";
		this.panel5.Controls.Add(this.ultraButton4);
		this.panel5.Controls.Add(this.BtnPick);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel5.Location = new System.Drawing.Point(0, 290);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(376, 36);
		this.panel5.TabIndex = 31;
		this.ultraButton4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance6.Image = resources.GetObject("appearance6.Image");
		appearance6.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton4.Appearance = appearance6;
		this.ultraButton4.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton4.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		appearance7.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance7.BackColor2 = System.Drawing.Color.White;
		appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.ultraButton4.HotTrackAppearance = appearance7;
		this.ultraButton4.HotTracking = true;
		this.ultraButton4.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton4.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton4.Location = new System.Drawing.Point(292, 3);
		this.ultraButton4.Name = "ultraButton4";
		this.ultraButton4.ShowFocusRect = false;
		this.ultraButton4.ShowOutline = false;
		this.ultraButton4.Size = new System.Drawing.Size(80, 28);
		this.ultraButton4.SupportThemes = false;
		this.ultraButton4.TabIndex = 10;
		this.ultraButton4.Text = "取消";
		this.BtnPick.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance8.Image = resources.GetObject("appearance8.Image");
		appearance8.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnPick.Appearance = appearance8;
		this.BtnPick.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		appearance9.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance9.BackColor2 = System.Drawing.Color.White;
		appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.BtnPick.HotTrackAppearance = appearance9;
		this.BtnPick.HotTracking = true;
		this.BtnPick.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnPick.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnPick.Location = new System.Drawing.Point(210, 3);
		this.BtnPick.Name = "BtnPick";
		this.BtnPick.ShowFocusRect = false;
		this.BtnPick.ShowOutline = false;
		this.BtnPick.Size = new System.Drawing.Size(80, 28);
		this.BtnPick.SupportThemes = false;
		this.BtnPick.TabIndex = 9;
		this.BtnPick.Text = "確定";
		this.BtnPick.Click += new System.EventHandler(BtnPick_Click);
		appearance10.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance10.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
		appearance10.FontData.Italic = Infragistics.Win.DefaultableBoolean.False;
		appearance10.FontData.Name = "細明體";
		appearance10.FontData.SizeInPoints = 11.25f;
		appearance10.FontData.Strikeout = Infragistics.Win.DefaultableBoolean.False;
		appearance10.FontData.Underline = Infragistics.Win.DefaultableBoolean.False;
		appearance10.TextHAlign = Infragistics.Win.HAlign.Right;
		this.txtAmount.Appearance = appearance10;
		this.txtAmount.AutoSize = true;
		this.txtAmount.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.txtAmount.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.txtAmount.Location = new System.Drawing.Point(144, 184);
		this.txtAmount.Name = "txtAmount";
		this.txtAmount.Size = new System.Drawing.Size(208, 24);
		this.txtAmount.TabIndex = 28;
		this.txtAmount.ValueChanged += new System.EventHandler(txtAmount_ValueChanged);
		appearance11.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel6.Appearance = appearance11;
		this.ultraLabel6.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(328, 152);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(20, 23);
		this.ultraLabel6.TabIndex = 27;
		this.ultraLabel6.Text = "%";
		appearance12.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance12.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblTotal.Appearance = appearance12;
		this.lblTotal.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.lblTotal.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblTotal.Location = new System.Drawing.Point(148, 122);
		this.lblTotal.Name = "lblTotal";
		this.lblTotal.Size = new System.Drawing.Size(200, 23);
		this.lblTotal.TabIndex = 9;
		this.lblTotal.Text = "0";
		appearance13.TextHAlign = Infragistics.Win.HAlign.Right;
		this.ultraLabel3.Appearance = appearance13;
		this.ultraLabel3.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraLabel3.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel3.Location = new System.Drawing.Point(8, 122);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(120, 23);
		this.ultraLabel3.TabIndex = 8;
		this.ultraLabel3.Text = "目前總金額:";
		appearance14.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance14.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
		appearance14.FontData.Italic = Infragistics.Win.DefaultableBoolean.False;
		appearance14.FontData.Name = "細明體";
		appearance14.FontData.SizeInPoints = 11.25f;
		appearance14.FontData.Strikeout = Infragistics.Win.DefaultableBoolean.False;
		appearance14.FontData.Underline = Infragistics.Win.DefaultableBoolean.False;
		appearance14.TextHAlign = Infragistics.Win.HAlign.Right;
		this.txtRatio.Appearance = appearance14;
		this.txtRatio.AutoSize = true;
		this.txtRatio.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.txtRatio.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.txtRatio.Location = new System.Drawing.Point(144, 152);
		this.txtRatio.MaxLength = 10;
		this.txtRatio.Name = "txtRatio";
		this.txtRatio.Size = new System.Drawing.Size(184, 24);
		this.txtRatio.TabIndex = 29;
		this.txtRatio.Text = "0";
		this.txtRatio.ValueChanged += new System.EventHandler(txtRatio_ValueChanged);
		appearance15.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance15.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblOldTotal.Appearance = appearance15;
		this.lblOldTotal.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.lblOldTotal.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblOldTotal.Location = new System.Drawing.Point(148, 96);
		this.lblOldTotal.Name = "lblOldTotal";
		this.lblOldTotal.Size = new System.Drawing.Size(200, 23);
		this.lblOldTotal.TabIndex = 9;
		this.lblOldTotal.Text = "0";
		appearance16.TextHAlign = Infragistics.Win.HAlign.Right;
		this.ultraLabel8.Appearance = appearance16;
		this.ultraLabel8.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraLabel8.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel8.Location = new System.Drawing.Point(8, 96);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(120, 23);
		this.ultraLabel8.TabIndex = 8;
		this.ultraLabel8.Text = "最原始總金額:";
		this.Tab_B.Controls.Add(this.lbMessage);
		this.Tab_B.Controls.Add(this.ultraLabel4);
		this.Tab_B.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(376, 326);
		this.lbMessage.Location = new System.Drawing.Point(16, 80);
		this.lbMessage.Name = "lbMessage";
		this.lbMessage.Size = new System.Drawing.Size(348, 23);
		this.lbMessage.TabIndex = 8;
		this.lbMessage.Text = "這個動作會花些時間，請稍候。";
		this.ultraLabel4.Location = new System.Drawing.Point(16, 48);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(408, 23);
		this.ultraLabel4.TabIndex = 7;
		this.ultraLabel4.Text = "總價調整運算中....";
		this.Tab_C.Controls.Add(this.ultraLabel19);
		this.Tab_C.Controls.Add(this.ultraLabel16);
		this.Tab_C.Controls.Add(this.panel1);
		this.Tab_C.Location = new System.Drawing.Point(0, 0);
		this.Tab_C.Name = "Tab_C";
		this.Tab_C.Size = new System.Drawing.Size(376, 326);
		appearance17.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		this.ultraLabel16.Appearance = appearance17;
		this.ultraLabel16.Location = new System.Drawing.Point(120, 64);
		this.ultraLabel16.Name = "ultraLabel16";
		this.ultraLabel16.Size = new System.Drawing.Size(140, 23);
		this.ultraLabel16.TabIndex = 8;
		this.ultraLabel16.Text = "總價調整運算完畢";
		this.panel1.Controls.Add(this.ultraButton2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 290);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(376, 36);
		this.panel1.TabIndex = 7;
		this.ultraButton2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance18.Image = resources.GetObject("appearance18.Image");
		appearance18.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton2.Appearance = appearance18;
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton2.DialogResult = System.Windows.Forms.DialogResult.OK;
		appearance19.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance19.BackColor2 = System.Drawing.Color.White;
		appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.ultraButton2.HotTrackAppearance = appearance19;
		this.ultraButton2.HotTracking = true;
		this.ultraButton2.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton2.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton2.Location = new System.Drawing.Point(290, 4);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(80, 28);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 9;
		this.ultraButton2.Text = "確定";
		this.ultraButton2.Click += new System.EventHandler(ultraButton2_Click);
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_A);
		this.Tab_Ctrl.Controls.Add(this.Tab_B);
		this.Tab_Ctrl.Controls.Add(this.Tab_C);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(376, 326);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabIndex = 31;
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "tab1";
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "tab2";
		ultraTab3.TabPage = this.Tab_C;
		ultraTab3.Text = "tab3";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[3] { ultraTab1, ultraTab2, ultraTab3 });
		this.Tab_Ctrl.SelectedTabChanged += new Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventHandler(Tab_Ctrl_SelectedTabChanged);
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(376, 326);
		this.toolTip1.AutoPopDelay = 15000;
		this.toolTip1.InitialDelay = 500;
		this.toolTip1.ReshowDelay = 100;
		this.ultraLabel19.Location = new System.Drawing.Point(12, 118);
		this.ultraLabel19.Name = "ultraLabel19";
		this.ultraLabel19.Size = new System.Drawing.Size(352, 86);
		this.ultraLabel19.TabIndex = 9;
		this.ultraLabel19.Text = "總價調整運算完畢";
		this.ultraLabel19.Visible = false;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.ultraButton4;
		base.ClientSize = new System.Drawing.Size(376, 326);
		base.Controls.Add(this.Tab_Ctrl);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormSplitCnt_ResetCost";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "契約管理--總價調整";
		base.Load += new System.EventHandler(FormSplitCnt_ResetCost_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormSplitCnt_ResetCost_KeyDown);
		this.Tab_A.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRatio).EndInit();
		this.Tab_B.ResumeLayout(false);
		this.Tab_C.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
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

	public FormSplitCnt_ResetCost()
	{
		InitializeComponent();
	}

	private void ProgressEventHandler(string Message)
	{
		Application.DoEvents();
		lbMessage.Text = Message;
	}

	private void FormSplitCnt_ResetCost_Load(object sender, EventArgs e)
	{
		string sRestoreCostFirst = CommonMethods.GetIniValue("FormBudget", "RestoreCostFirst");
		if (sRestoreCostFirst.ToUpper() == "TRUE")
		{
			CB_UnRestoreCost.Checked = true;
		}
		else
		{
			CB_UnRestoreCost.Checked = false;
		}
		if (F_OldTotalAmount != 0.0)
		{
			lblOldTotal.Text = $"{F_OldTotalAmount:N0}";
		}
		else
		{
			lblOldTotal.Text = $"{F_TotalAmount:N0}";
		}
		lblTotal.Text = $"{F_TotalAmount:N0}";
		txtRatio.Text = $"{100:N6}";
		txtAmount.Text = $"{F_TotalAmount:N0}";
		if (Convert.ToDouble(lblTotal.Text) == 0.0)
		{
			txtRatio.Enabled = false;
			txtAmount.Enabled = false;
			MessageBox.Show(this, "總價為 0 ，無法進行總價調整。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			Close();
		}
		txtRatio.Appearance.BackColor = Color.White;
		txtAmount.Appearance.BackColor = Color.White;
	}

	private void txtRatio_ValueChanged(object sender, EventArgs e)
	{
		if (!txtRatio.IsInEditMode)
		{
			return;
		}
		double theRation = 0.0;
		try
		{
			theRation = Convert.ToDouble(txtRatio.Text);
			txtAmount.Text = $"{F_TotalAmount * theRation / 100.0:N0}";
		}
		catch (Exception)
		{
			MessageBox.Show(this, "輸入比例有誤，請重新輸入!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtRatio.Focus();
		}
	}

	private void txtAmount_ValueChanged(object sender, EventArgs e)
	{
		if (!txtAmount.IsInEditMode)
		{
			return;
		}
		double theAmount = 0.0;
		try
		{
			theAmount = Math.Abs(Convert.ToDouble(txtAmount.Text));
			txtRatio.Text = $"{theAmount / F_TotalAmount * 100.0:N6}";
		}
		catch (Exception)
		{
			MessageBox.Show(this, "輸入比例有誤，請重新輸入!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtAmount.Focus();
		}
	}

	private void BtnPick_Click(object sender, EventArgs e)
	{
		MessageBox.Show(this, "行政院公共工程委員會92年6月5日工程企字第 09200229070 號令修正『政府採購錯誤行為樣態』， \n 明列『不考慮廠商單價是否合理而強以機關預算單價調整廠商單價』為錯誤行為樣態之一。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		bool EnableNewCalculateCost = false;
		if (theProject != null)
		{
			Archnowledge.Pcces.DomainModule.General.PubProject thePubProject = new Archnowledge.Pcces.DomainModule.General.PubProject();
			EnableNewCalculateCost = thePubProject.GetPubProjectEnableNewCalculateCost(F_ProjectCode);
		}
		if (EnableNewCalculateCost)
		{
			DoNewCalculate();
		}
		else
		{
			DoOldCalculate();
		}
	}

	private void DoNewCalculate()
	{
		CommonMethods.WriteIniValue("FormBudget", "RestoreCostFirst", CB_UnRestoreCost.Checked ? "True" : "False");
		Cursor = Cursors.WaitCursor;
		Tab_B.Tab.Selected = true;
		ExecResult ER = new ExecResult();
		Application.DoEvents();
		DiscountCalculate theDiscountCalculate = new DiscountCalculate(F_ActionName, F_ProjectCode, 0);
		theDiscountCalculate.ps_IsRestoreCostFirst = (CB_UnRestoreCost.Checked ? "Y" : "N");
		bool IsCalculateOnce = true;
		try
		{
			IsCalculateOnce = true;
			if (RB1.Checked)
			{
				ER = theDiscountCalculate.SetAmountFaster(Convert.ToDouble(txtAmount.Text), IsCalculateOnce, ProgressEventHandler, F_ActionName);
			}
			else if (RB2.Checked)
			{
				theDiscountCalculate.RestoreCost(ProgressEventHandler);
			}
			if (ER.ReturnCode == 0)
			{
				ItemCalculate theItemCalculate = new ItemCalculate(F_ActionName, F_ProjectCode, 0);
				ER = theItemCalculate.CalculateAll(IncludeResource: true, IncludeMrs: true, ProgressEventHandler, null);
			}
		}
		catch (Exception ex)
		{
			ER.ReturnCode = 1;
			ER.Message = "總價調整失敗 : " + ex.Message;
		}
		if (ER.ReturnCode != 0)
		{
			try
			{
				theDiscountCalculate.RestoreCost(ProgressEventHandler);
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetResetCost.cs" + ex.Message);
			}
			ultraLabel16.Text = "總價調整失敗";
			ultraLabel19.Text = "請檢查目前專案的相關設定是否正確 : " + ER.Message;
			ultraLabel16.Appearance.ForeColor = Color.Red;
			ultraLabel19.Appearance.ForeColor = Color.Red;
			ultraLabel19.Visible = true;
			Tab_C.Tab.Selected = true;
			Cursor = Cursors.Default;
		}
		Tab_C.Tab.Selected = true;
		Cursor = Cursors.Default;
	}

	private void DoOldCalculate()
	{
		CommonMethods.WriteIniValue("FormBudget", "RestoreCostFirst", CB_UnRestoreCost.Checked ? "True" : "False");
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(subctr) 契約書總計調整");
		bool bExecResult = true;
		Tab_B.Tab.Selected = true;
		Application.DoEvents();
		string AppLocation = AppDomain.CurrentDomain.BaseDirectory;
		string IsOldReCal = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BDGT", "IsOldReCal");
		ReSetCost RST_CST = new ReSetCost(tmp_AL1);
		RST_CST.ps_IsRestoreCostFirst = (CB_UnRestoreCost.Checked ? "Y" : "N");
		RST_CST.ps_IsOldReCalc = ((IsOldReCal.ToUpper() == "TRUE") ? "Y" : "N");
		if (RB1.Checked)
		{
			bExecResult = RST_CST.SetAmount(F_ProjectCode, CommonMethods.GetActionNameString(F_ActionName), Convert.ToDouble(txtAmount.Text), 1);
		}
		else if (RB2.Checked)
		{
			try
			{
				RST_CST.RestoreCost(F_ProjectCode, CommonMethods.GetActionNameString(F_ActionName));
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetResetCost.cs" + ex.Message);
			}
		}
		try
		{
			Archnowledge.Pcces.BUDClass.ItemA dbItemA = new Archnowledge.Pcces.BUDClass.ItemA(tmp_AL1);
			dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			if (IsOldReCal.ToUpper() == "TRUE")
			{
				dbItemA.ReCalcCost2(F_ProjectCode, mode: true, noShare: true);
			}
			else if (IsOldReCal.ToUpper() == "FALSE")
			{
				dbItemA.ReCalcCost2(F_ProjectCode);
			}
			else
			{
				dbItemA.ps_SmallCalcuMode = "THIRD";
				dbItemA.ReCalcCost2(F_ProjectCode, mode: true, noShare: true);
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetResetCost.cs" + ex.Message);
		}
		Application.DoEvents();
		Tab_C.Tab.Selected = true;
	}

	private void Tab_Ctrl_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
	{
	}

	private void ultraButton2_Click(object sender, EventArgs e)
	{
	}

	private void FormSplitCnt_ResetCost_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormSplitCnt_ResetCost");
		}
	}
}
