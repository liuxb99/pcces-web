using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.PccesMain.Library;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain;

public class FormModuleFlowMap : Form
{
	private IContainer components = null;

	private UltraButton btnProjectImport;

	private UltraPictureBox ultraPictureBox1;

	private ImageList imageList1;

	private Label label1;

	private UltraTabControl tabFlow;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl ultraTabPageControl1;

	private UltraTabPageControl ultraTabPageControl2;

	private UltraButton btnEstimateEvaluate;

	private Label label7;

	private UltraButton btnEditContract;

	private Label label6;

	private UltraButton btnCreateBudgetFile;

	private Label label5;

	private UltraButton btnEditBudget;

	private Label label4;

	private UltraButton btnCreateProject;

	private Label label3;

	private UltraButton btnMarBaseCreate;

	private Label label2;

	private UltraButton BtnCancel;

	private UltraPictureBox ultraPictureBox2;

	private UltraPictureBox ultraPictureBox4;

	private UltraPictureBox ultraPictureBox3;

	private UltraPictureBox ultraPictureBox8;

	private UltraPictureBox ultraPictureBox7;

	private UltraPictureBox ultraPictureBox6;

	private UltraPictureBox ultraPictureBox5;

	private UltraPictureBox ultraPictureBox9;

	private UltraPictureBox ultraPictureBox10;

	private UltraButton btnCreateBidFile;

	private Label label8;

	private UltraButton btnEditBid;

	private Label label9;

	private UltraButton btnBidImport;

	private Label label10;

	private ModuleFlowMapButtonID F_ButtonID = ModuleFlowMapButtonID.None;

	public ModuleFlowMapButtonID PressedButtonID => F_ButtonID;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.FormModuleFlowMap));
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
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraPictureBox8 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.ultraPictureBox7 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.ultraPictureBox6 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.ultraPictureBox5 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.ultraPictureBox4 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.ultraPictureBox3 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.btnEstimateEvaluate = new Infragistics.Win.Misc.UltraButton();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.label7 = new System.Windows.Forms.Label();
		this.btnEditContract = new Infragistics.Win.Misc.UltraButton();
		this.label6 = new System.Windows.Forms.Label();
		this.btnCreateBudgetFile = new Infragistics.Win.Misc.UltraButton();
		this.label5 = new System.Windows.Forms.Label();
		this.btnEditBudget = new Infragistics.Win.Misc.UltraButton();
		this.label4 = new System.Windows.Forms.Label();
		this.btnCreateProject = new Infragistics.Win.Misc.UltraButton();
		this.label3 = new System.Windows.Forms.Label();
		this.btnMarBaseCreate = new Infragistics.Win.Misc.UltraButton();
		this.label2 = new System.Windows.Forms.Label();
		this.btnProjectImport = new Infragistics.Win.Misc.UltraButton();
		this.ultraPictureBox2 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.label1 = new System.Windows.Forms.Label();
		this.ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraPictureBox9 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.ultraPictureBox10 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.btnCreateBidFile = new Infragistics.Win.Misc.UltraButton();
		this.label8 = new System.Windows.Forms.Label();
		this.btnEditBid = new Infragistics.Win.Misc.UltraButton();
		this.label9 = new System.Windows.Forms.Label();
		this.btnBidImport = new Infragistics.Win.Misc.UltraButton();
		this.label10 = new System.Windows.Forms.Label();
		this.ultraPictureBox1 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.tabFlow = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.BtnCancel = new Infragistics.Win.Misc.UltraButton();
		this.ultraTabPageControl1.SuspendLayout();
		this.ultraTabPageControl2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.tabFlow).BeginInit();
		this.tabFlow.SuspendLayout();
		base.SuspendLayout();
		this.ultraTabPageControl1.Controls.Add(this.ultraPictureBox8);
		this.ultraTabPageControl1.Controls.Add(this.ultraPictureBox7);
		this.ultraTabPageControl1.Controls.Add(this.ultraPictureBox6);
		this.ultraTabPageControl1.Controls.Add(this.ultraPictureBox5);
		this.ultraTabPageControl1.Controls.Add(this.ultraPictureBox4);
		this.ultraTabPageControl1.Controls.Add(this.ultraPictureBox3);
		this.ultraTabPageControl1.Controls.Add(this.btnEstimateEvaluate);
		this.ultraTabPageControl1.Controls.Add(this.label7);
		this.ultraTabPageControl1.Controls.Add(this.btnEditContract);
		this.ultraTabPageControl1.Controls.Add(this.label6);
		this.ultraTabPageControl1.Controls.Add(this.btnCreateBudgetFile);
		this.ultraTabPageControl1.Controls.Add(this.label5);
		this.ultraTabPageControl1.Controls.Add(this.btnEditBudget);
		this.ultraTabPageControl1.Controls.Add(this.label4);
		this.ultraTabPageControl1.Controls.Add(this.btnCreateProject);
		this.ultraTabPageControl1.Controls.Add(this.label3);
		this.ultraTabPageControl1.Controls.Add(this.btnMarBaseCreate);
		this.ultraTabPageControl1.Controls.Add(this.label2);
		this.ultraTabPageControl1.Controls.Add(this.btnProjectImport);
		this.ultraTabPageControl1.Controls.Add(this.ultraPictureBox2);
		this.ultraTabPageControl1.Controls.Add(this.label1);
		this.ultraTabPageControl1.Location = new System.Drawing.Point(1, 20);
		this.ultraTabPageControl1.Name = "ultraTabPageControl1";
		this.ultraTabPageControl1.Size = new System.Drawing.Size(461, 425);
		appearance1.Image = resources.GetObject("appearance1.Image");
		this.ultraPictureBox8.Appearance = appearance1;
		this.ultraPictureBox8.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox8.Image = resources.GetObject("ultraPictureBox8.Image");
		this.ultraPictureBox8.ImageTransparentColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraPictureBox8.Location = new System.Drawing.Point(146, 142);
		this.ultraPictureBox8.Name = "ultraPictureBox8";
		this.ultraPictureBox8.Size = new System.Drawing.Size(69, 45);
		this.ultraPictureBox8.TabIndex = 27;
		appearance2.Image = resources.GetObject("appearance2.Image");
		this.ultraPictureBox7.Appearance = appearance2;
		this.ultraPictureBox7.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox7.Image = resources.GetObject("ultraPictureBox7.Image");
		this.ultraPictureBox7.ImageTransparentColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraPictureBox7.Location = new System.Drawing.Point(272, 54);
		this.ultraPictureBox7.Name = "ultraPictureBox7";
		this.ultraPictureBox7.Size = new System.Drawing.Size(69, 45);
		this.ultraPictureBox7.TabIndex = 26;
		appearance3.Image = resources.GetObject("appearance3.Image");
		this.ultraPictureBox6.Appearance = appearance3;
		this.ultraPictureBox6.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox6.Image = resources.GetObject("ultraPictureBox6.Image");
		this.ultraPictureBox6.ImageTransparentColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraPictureBox6.Location = new System.Drawing.Point(281, 142);
		this.ultraPictureBox6.Name = "ultraPictureBox6";
		this.ultraPictureBox6.Size = new System.Drawing.Size(69, 45);
		this.ultraPictureBox6.TabIndex = 25;
		appearance4.Image = resources.GetObject("appearance4.Image");
		this.ultraPictureBox5.Appearance = appearance4;
		this.ultraPictureBox5.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox5.Image = resources.GetObject("ultraPictureBox5.Image");
		this.ultraPictureBox5.ImageTransparentColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraPictureBox5.Location = new System.Drawing.Point(146, 53);
		this.ultraPictureBox5.Name = "ultraPictureBox5";
		this.ultraPictureBox5.Size = new System.Drawing.Size(69, 45);
		this.ultraPictureBox5.TabIndex = 24;
		appearance5.Image = resources.GetObject("appearance5.Image");
		this.ultraPictureBox4.Appearance = appearance5;
		this.ultraPictureBox4.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox4.Image = resources.GetObject("ultraPictureBox4.Image");
		this.ultraPictureBox4.ImageTransparentColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraPictureBox4.Location = new System.Drawing.Point(248, 329);
		this.ultraPictureBox4.Name = "ultraPictureBox4";
		this.ultraPictureBox4.Size = new System.Drawing.Size(29, 52);
		this.ultraPictureBox4.TabIndex = 23;
		this.ultraPictureBox4.Visible = false;
		appearance6.Image = resources.GetObject("appearance6.Image");
		this.ultraPictureBox3.Appearance = appearance6;
		this.ultraPictureBox3.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox3.Image = resources.GetObject("ultraPictureBox3.Image");
		this.ultraPictureBox3.ImageTransparentColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraPictureBox3.Location = new System.Drawing.Point(248, 229);
		this.ultraPictureBox3.Name = "ultraPictureBox3";
		this.ultraPictureBox3.Size = new System.Drawing.Size(29, 52);
		this.ultraPictureBox3.TabIndex = 22;
		this.ultraPictureBox3.Visible = false;
		appearance7.Image = 30;
		this.btnEstimateEvaluate.Appearance = appearance7;
		this.btnEstimateEvaluate.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnEstimateEvaluate.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnEstimateEvaluate.ImageList = this.imageList1;
		this.btnEstimateEvaluate.ImageSize = new System.Drawing.Size(24, 24);
		this.btnEstimateEvaluate.Location = new System.Drawing.Point(206, 375);
		this.btnEstimateEvaluate.Name = "btnEstimateEvaluate";
		this.btnEstimateEvaluate.Size = new System.Drawing.Size(34, 31);
		this.btnEstimateEvaluate.TabIndex = 20;
		this.btnEstimateEvaluate.Visible = false;
		this.btnEstimateEvaluate.Click += new System.EventHandler(btnXXX_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "");
		this.imageList1.Images.SetKeyName(1, "");
		this.imageList1.Images.SetKeyName(2, "");
		this.imageList1.Images.SetKeyName(3, "");
		this.imageList1.Images.SetKeyName(4, "");
		this.imageList1.Images.SetKeyName(5, "");
		this.imageList1.Images.SetKeyName(6, "");
		this.imageList1.Images.SetKeyName(7, "");
		this.imageList1.Images.SetKeyName(8, "");
		this.imageList1.Images.SetKeyName(9, "");
		this.imageList1.Images.SetKeyName(10, "");
		this.imageList1.Images.SetKeyName(11, "");
		this.imageList1.Images.SetKeyName(12, "");
		this.imageList1.Images.SetKeyName(13, "");
		this.imageList1.Images.SetKeyName(14, "");
		this.imageList1.Images.SetKeyName(15, "");
		this.imageList1.Images.SetKeyName(16, "");
		this.imageList1.Images.SetKeyName(17, "");
		this.imageList1.Images.SetKeyName(18, "");
		this.imageList1.Images.SetKeyName(19, "");
		this.imageList1.Images.SetKeyName(20, "");
		this.imageList1.Images.SetKeyName(21, "");
		this.imageList1.Images.SetKeyName(22, "");
		this.imageList1.Images.SetKeyName(23, "");
		this.imageList1.Images.SetKeyName(24, "");
		this.imageList1.Images.SetKeyName(25, "");
		this.imageList1.Images.SetKeyName(26, "");
		this.imageList1.Images.SetKeyName(27, "");
		this.imageList1.Images.SetKeyName(28, "");
		this.imageList1.Images.SetKeyName(29, "");
		this.imageList1.Images.SetKeyName(30, "");
		this.imageList1.Images.SetKeyName(31, "");
		this.imageList1.Images.SetKeyName(32, "");
		this.imageList1.Images.SetKeyName(33, "");
		this.imageList1.Images.SetKeyName(34, "");
		this.imageList1.Images.SetKeyName(35, "");
		this.imageList1.Images.SetKeyName(36, "");
		this.imageList1.Images.SetKeyName(37, "");
		this.imageList1.Images.SetKeyName(38, "");
		this.imageList1.Images.SetKeyName(39, "");
		this.imageList1.Images.SetKeyName(40, "");
		this.imageList1.Images.SetKeyName(41, "");
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(246, 384);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(53, 12);
		this.label7.TabIndex = 21;
		this.label7.Text = "估驗計價";
		this.label7.Visible = false;
		appearance8.Image = 4;
		this.btnEditContract.Appearance = appearance8;
		this.btnEditContract.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnEditContract.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnEditContract.ImageList = this.imageList1;
		this.btnEditContract.ImageSize = new System.Drawing.Size(24, 24);
		this.btnEditContract.Location = new System.Drawing.Point(206, 287);
		this.btnEditContract.Name = "btnEditContract";
		this.btnEditContract.Size = new System.Drawing.Size(34, 31);
		this.btnEditContract.TabIndex = 18;
		this.btnEditContract.Visible = false;
		this.btnEditContract.Click += new System.EventHandler(btnXXX_Click);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(246, 296);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(53, 12);
		this.label6.TabIndex = 19;
		this.label6.Text = "契約編製";
		this.label6.Visible = false;
		appearance9.Image = resources.GetObject("appearance9.Image");
		this.btnCreateBudgetFile.Appearance = appearance9;
		this.btnCreateBudgetFile.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnCreateBudgetFile.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnCreateBudgetFile.ImageList = this.imageList1;
		this.btnCreateBudgetFile.ImageSize = new System.Drawing.Size(24, 24);
		this.btnCreateBudgetFile.Location = new System.Drawing.Point(206, 193);
		this.btnCreateBudgetFile.Name = "btnCreateBudgetFile";
		this.btnCreateBudgetFile.Size = new System.Drawing.Size(34, 31);
		this.btnCreateBudgetFile.TabIndex = 16;
		this.btnCreateBudgetFile.Click += new System.EventHandler(btnXXX_Click);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(246, 202);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(65, 12);
		this.label5.TabIndex = 17;
		this.label5.Text = "製作電子檔";
		appearance10.Image = 24;
		this.btnEditBudget.Appearance = appearance10;
		this.btnEditBudget.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnEditBudget.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnEditBudget.ImageList = this.imageList1;
		this.btnEditBudget.ImageSize = new System.Drawing.Size(24, 24);
		this.btnEditBudget.Location = new System.Drawing.Point(307, 103);
		this.btnEditBudget.Name = "btnEditBudget";
		this.btnEditBudget.Size = new System.Drawing.Size(34, 31);
		this.btnEditBudget.TabIndex = 14;
		this.btnEditBudget.Click += new System.EventHandler(btnXXX_Click);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(347, 112);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(77, 12);
		this.label4.TabIndex = 15;
		this.label4.Text = "既有預算編製";
		appearance11.Image = 9;
		this.btnCreateProject.Appearance = appearance11;
		this.btnCreateProject.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnCreateProject.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnCreateProject.ImageList = this.imageList1;
		this.btnCreateProject.ImageSize = new System.Drawing.Size(24, 24);
		this.btnCreateProject.Location = new System.Drawing.Point(115, 103);
		this.btnCreateProject.Name = "btnCreateProject";
		this.btnCreateProject.Size = new System.Drawing.Size(34, 31);
		this.btnCreateProject.TabIndex = 12;
		this.btnCreateProject.Click += new System.EventHandler(btnXXX_Click);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(155, 112);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(65, 12);
		this.label3.TabIndex = 13;
		this.label3.Text = "新專案建立";
		appearance12.Image = resources.GetObject("appearance12.Image");
		this.btnMarBaseCreate.Appearance = appearance12;
		this.btnMarBaseCreate.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnMarBaseCreate.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnMarBaseCreate.ImageList = this.imageList1;
		this.btnMarBaseCreate.ImageSize = new System.Drawing.Size(24, 24);
		this.btnMarBaseCreate.Location = new System.Drawing.Point(221, 30);
		this.btnMarBaseCreate.Name = "btnMarBaseCreate";
		this.btnMarBaseCreate.Size = new System.Drawing.Size(34, 31);
		this.btnMarBaseCreate.TabIndex = 10;
		this.btnMarBaseCreate.Click += new System.EventHandler(btnXXX_Click);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(261, 39);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(89, 12);
		this.label2.TabIndex = 11;
		this.label2.Text = "基本資料庫維護";
		appearance13.Image = 29;
		this.btnProjectImport.Appearance = appearance13;
		this.btnProjectImport.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnProjectImport.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnProjectImport.ImageList = this.imageList1;
		this.btnProjectImport.ImageSize = new System.Drawing.Size(24, 24);
		this.btnProjectImport.Location = new System.Drawing.Point(19, 53);
		this.btnProjectImport.Name = "btnProjectImport";
		this.btnProjectImport.Size = new System.Drawing.Size(34, 31);
		this.btnProjectImport.TabIndex = 0;
		this.btnProjectImport.Click += new System.EventHandler(btnXXX_Click);
		appearance14.Image = resources.GetObject("appearance14.Image");
		this.ultraPictureBox2.Appearance = appearance14;
		this.ultraPictureBox2.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox2.Image = resources.GetObject("ultraPictureBox2.Image");
		this.ultraPictureBox2.ImageTransparentColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraPictureBox2.Location = new System.Drawing.Point(19, 77);
		this.ultraPictureBox2.Name = "ultraPictureBox2";
		this.ultraPictureBox2.Size = new System.Drawing.Size(181, 345);
		this.ultraPictureBox2.TabIndex = 2;
		this.ultraPictureBox2.Visible = false;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(59, 62);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(53, 12);
		this.label1.TabIndex = 9;
		this.label1.Text = "專案轉入";
		this.ultraTabPageControl2.Controls.Add(this.ultraPictureBox9);
		this.ultraTabPageControl2.Controls.Add(this.ultraPictureBox10);
		this.ultraTabPageControl2.Controls.Add(this.btnCreateBidFile);
		this.ultraTabPageControl2.Controls.Add(this.label8);
		this.ultraTabPageControl2.Controls.Add(this.btnEditBid);
		this.ultraTabPageControl2.Controls.Add(this.label9);
		this.ultraTabPageControl2.Controls.Add(this.btnBidImport);
		this.ultraTabPageControl2.Controls.Add(this.label10);
		this.ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabPageControl2.Name = "ultraTabPageControl2";
		this.ultraTabPageControl2.Size = new System.Drawing.Size(461, 425);
		appearance15.Image = resources.GetObject("appearance15.Image");
		this.ultraPictureBox9.Appearance = appearance15;
		this.ultraPictureBox9.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox9.Image = resources.GetObject("ultraPictureBox9.Image");
		this.ultraPictureBox9.ImageTransparentColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraPictureBox9.Location = new System.Drawing.Point(80, 151);
		this.ultraPictureBox9.Name = "ultraPictureBox9";
		this.ultraPictureBox9.Size = new System.Drawing.Size(69, 45);
		this.ultraPictureBox9.TabIndex = 35;
		appearance16.Image = resources.GetObject("appearance16.Image");
		this.ultraPictureBox10.Appearance = appearance16;
		this.ultraPictureBox10.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox10.Image = resources.GetObject("ultraPictureBox10.Image");
		this.ultraPictureBox10.ImageTransparentColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraPictureBox10.Location = new System.Drawing.Point(252, 151);
		this.ultraPictureBox10.Name = "ultraPictureBox10";
		this.ultraPictureBox10.Size = new System.Drawing.Size(69, 45);
		this.ultraPictureBox10.TabIndex = 34;
		appearance17.Image = resources.GetObject("appearance17.Image");
		this.btnCreateBidFile.Appearance = appearance17;
		this.btnCreateBidFile.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnCreateBidFile.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnCreateBidFile.ImageList = this.imageList1;
		this.btnCreateBidFile.ImageSize = new System.Drawing.Size(24, 24);
		this.btnCreateBidFile.Location = new System.Drawing.Point(166, 212);
		this.btnCreateBidFile.Name = "btnCreateBidFile";
		this.btnCreateBidFile.Size = new System.Drawing.Size(34, 31);
		this.btnCreateBidFile.TabIndex = 32;
		this.btnCreateBidFile.Click += new System.EventHandler(btnXXX_Click);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(206, 221);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(65, 12);
		this.label8.TabIndex = 33;
		this.label8.Text = "製作電子檔";
		appearance18.Image = 9;
		this.btnEditBid.Appearance = appearance18;
		this.btnEditBid.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnEditBid.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnEditBid.ImageList = this.imageList1;
		this.btnEditBid.ImageSize = new System.Drawing.Size(24, 24);
		this.btnEditBid.Location = new System.Drawing.Point(282, 93);
		this.btnEditBid.Name = "btnEditBid";
		this.btnEditBid.Size = new System.Drawing.Size(34, 31);
		this.btnEditBid.TabIndex = 30;
		this.btnEditBid.Click += new System.EventHandler(btnXXX_Click);
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(322, 102);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(77, 12);
		this.label9.TabIndex = 31;
		this.label9.Text = "既有標單填寫";
		appearance19.Image = 29;
		this.btnBidImport.Appearance = appearance19;
		this.btnBidImport.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnBidImport.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnBidImport.ImageList = this.imageList1;
		this.btnBidImport.ImageSize = new System.Drawing.Size(24, 24);
		this.btnBidImport.Location = new System.Drawing.Point(38, 93);
		this.btnBidImport.Name = "btnBidImport";
		this.btnBidImport.Size = new System.Drawing.Size(34, 31);
		this.btnBidImport.TabIndex = 28;
		this.btnBidImport.Click += new System.EventHandler(btnXXX_Click);
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(78, 102);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(77, 12);
		this.label10.TabIndex = 29;
		this.label10.Text = "空白標單轉入";
		this.ultraPictureBox1.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox1.Location = new System.Drawing.Point(113, 206);
		this.ultraPictureBox1.Name = "ultraPictureBox1";
		this.ultraPictureBox1.Size = new System.Drawing.Size(100, 50);
		this.ultraPictureBox1.TabIndex = 1;
		this.tabFlow.Controls.Add(this.ultraTabSharedControlsPage1);
		this.tabFlow.Controls.Add(this.ultraTabPageControl1);
		this.tabFlow.Controls.Add(this.ultraTabPageControl2);
		this.tabFlow.Location = new System.Drawing.Point(12, 12);
		this.tabFlow.Name = "tabFlow";
		this.tabFlow.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.tabFlow.Size = new System.Drawing.Size(463, 446);
		this.tabFlow.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel;
		this.tabFlow.TabIndex = 10;
		ultraTab1.Key = "Budget";
		ultraTab1.TabPage = this.ultraTabPageControl1;
		ultraTab1.Text = "預算編製流程";
		ultraTab2.Key = "Bid";
		ultraTab2.TabPage = this.ultraTabPageControl2;
		ultraTab2.Text = "投標單編製流程";
		this.tabFlow.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2] { ultraTab1, ultraTab2 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(461, 425);
		appearance20.Image = resources.GetObject("appearance20.Image");
		appearance20.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnCancel.Appearance = appearance20;
		this.BtnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.BtnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnCancel.Location = new System.Drawing.Point(362, 463);
		this.BtnCancel.Name = "BtnCancel";
		this.BtnCancel.ShowFocusRect = false;
		this.BtnCancel.ShowOutline = false;
		this.BtnCancel.Size = new System.Drawing.Size(112, 31);
		this.BtnCancel.SupportThemes = false;
		this.BtnCancel.TabIndex = 11;
		this.BtnCancel.Text = "結束編製流程";
		this.BtnCancel.Click += new System.EventHandler(BtnCancel_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.ClientSize = new System.Drawing.Size(489, 498);
		base.Controls.Add(this.BtnCancel);
		base.Controls.Add(this.tabFlow);
		base.Controls.Add(this.ultraPictureBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormModuleFlowMap";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "流程導覽";
		base.Load += new System.EventHandler(FormModuleFlowMap_Load);
		this.ultraTabPageControl1.ResumeLayout(false);
		this.ultraTabPageControl1.PerformLayout();
		this.ultraTabPageControl2.ResumeLayout(false);
		this.ultraTabPageControl2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.tabFlow).EndInit();
		this.tabFlow.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public FormModuleFlowMap()
	{
		InitializeComponent();
	}

	private void BtnCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnXXX_Click(object sender, EventArgs e)
	{
		if (sender is UltraButton { Name: var name })
		{
			switch (name)
			{
			case "btnProjectImport":
				F_ButtonID = ModuleFlowMapButtonID.ProjectImport;
				break;
			case "btnEditContract":
				F_ButtonID = ModuleFlowMapButtonID.EditContract;
				break;
			case "btnCreateBudgetFile":
				F_ButtonID = ModuleFlowMapButtonID.CreateBudgetFile;
				break;
			case "btnEditBudget":
				F_ButtonID = ModuleFlowMapButtonID.EditBudget;
				break;
			case "btnCreateProject":
				F_ButtonID = ModuleFlowMapButtonID.CreateProject;
				break;
			case "btnMarBaseCreate":
				F_ButtonID = ModuleFlowMapButtonID.MarBaseCreate;
				break;
			case "btnCreateBidFile":
				F_ButtonID = ModuleFlowMapButtonID.CreateBidFile;
				break;
			case "btnEditBid":
				F_ButtonID = ModuleFlowMapButtonID.EditBid;
				break;
			case "btnBidImport":
				F_ButtonID = ModuleFlowMapButtonID.BidImport;
				break;
			case "btnEstimateEvaluate":
				F_ButtonID = ModuleFlowMapButtonID.EstimateEvaluate;
				break;
			}
		}
	}

	private void FormModuleFlowMap_Load(object sender, EventArgs e)
	{
		ModuleManager oManager = new ModuleManager();
		tabFlow.Tabs[tabFlow.Tabs.IndexOf("Budget")].Visible = oManager.EnableBudgetMdoule;
		tabFlow.Tabs[tabFlow.Tabs.IndexOf("Bid")].Visible = oManager.EnableBidMdoule;
	}
}
