using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.ShellLib;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.SysMaintain;

public class FormSys_J : UserControl
{
	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private GroupBox groupBox1;

	private UltraButton BtnUpdate;

	private Container components = null;

	private UltraLabel lblVer;

	private string F_UserID = "";

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

	public FormSys_J()
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.SysMaintain.FormSys_J));
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.BtnUpdate = new Infragistics.Win.Misc.UltraButton();
		this.lblVer = new Infragistics.Win.Misc.UltraLabel();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.ultraLabel1.Location = new System.Drawing.Point(16, 16);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(416, 23);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "本程式提供線上更新";
		this.ultraLabel2.Location = new System.Drawing.Point(16, 41);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(416, 23);
		this.ultraLabel2.TabIndex = 1;
		this.ultraLabel2.Text = "當你確定執行後，主程式(Pcces Win 4.01)將會自動關閉";
		this.groupBox1.Controls.Add(this.lblVer);
		this.groupBox1.Location = new System.Drawing.Point(16, 72);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(432, 80);
		this.groupBox1.TabIndex = 2;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "目前系統版本";
		appearance1.Image = resources.GetObject("appearance1.Image");
		this.BtnUpdate.Appearance = appearance1;
		this.BtnUpdate.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnUpdate.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnUpdate.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnUpdate.Location = new System.Drawing.Point(336, 164);
		this.BtnUpdate.Name = "BtnUpdate";
		this.BtnUpdate.ShowFocusRect = false;
		this.BtnUpdate.ShowOutline = false;
		this.BtnUpdate.Size = new System.Drawing.Size(112, 31);
		this.BtnUpdate.SupportThemes = false;
		this.BtnUpdate.TabIndex = 3;
		this.BtnUpdate.Text = "執行更新";
		this.BtnUpdate.Click += new System.EventHandler(BtnUpdate_Click);
		this.lblVer.Location = new System.Drawing.Point(24, 32);
		this.lblVer.Name = "lblVer";
		this.lblVer.Size = new System.Drawing.Size(392, 23);
		this.lblVer.TabIndex = 0;
		this.lblVer.Text = "lblVer";
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.Controls.Add(this.BtnUpdate);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.ultraLabel2);
		base.Controls.Add(this.ultraLabel1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Name = "FormSys_J";
		base.Size = new System.Drawing.Size(464, 212);
		base.Load += new System.EventHandler(FormSys_J_Load);
		this.groupBox1.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void FormSys_J_Load(object sender, EventArgs e)
	{
		lblVer.Text = "PCCES 【" + PccesVersion.PccesAssemblyVersion + "】";
	}

	private void BtnUpdate_Click(object sender, EventArgs e)
	{
		ShellExecute SHExe = new ShellExecute();
		SHExe.OwnerHandle = base.Handle;
		SHExe.Path = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "Pcces4.01Updater.exe";
		SHExe.Execute();
		SHExe = null;
		(base.ParentForm.ParentForm as frmPccesMain).TerminatePCCES();
	}
}
