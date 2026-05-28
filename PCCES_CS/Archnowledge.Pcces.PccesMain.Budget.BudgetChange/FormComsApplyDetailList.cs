using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;

namespace Archnowledge.Pcces.PccesMain.Budget.BudgetChange;

public class FormComsApplyDetailList : Form
{
	private string projectcode;

	private string projectName;

	private DataSet ComsApplyDetailList;

	private string[] BCA_UIDs;

	private IContainer components = null;

	private CheckedListBox chkboxComsApplyDetailVersion;

	private Button btnOK;

	private Button btnCancel;

	private Label label1;

	private Label labProjectName;

	private Label label2;

	public string _projectcode
	{
		set
		{
			projectcode = value;
		}
	}

	public string _projectName
	{
		set
		{
			projectName = value;
		}
	}

	public DataSet _ComsApplyDetailList
	{
		get
		{
			return ComsApplyDetailList;
		}
		set
		{
			ComsApplyDetailList = value;
		}
	}

	public string[] _BCA_UID
	{
		get
		{
			return BCA_UIDs;
		}
		set
		{
			BCA_UIDs = value;
		}
	}

	public FormComsApplyDetailList()
	{
		InitializeComponent();
	}

	private void FormComsApplyDetailList_Load(object sender, EventArgs e)
	{
		labProjectName.Text = projectcode + " " + projectName;
		if (ComsApplyDetailList != null)
		{
			DataView dv = new DataView(ComsApplyDetailList.Tables[0]);
			dv.RowFilter = "RowLock ='Y'";
			for (int i = 0; i < dv.Count; i++)
			{
				EstApplyDetail theEstApplyDetail = new EstApplyDetail(ArchConvert.Obj2String(dv[i]["Num"]), ArchConvert.Obj2DateTime(dv[i]["InputDate"]), ArchConvert.Obj2String(dv[i]["InputUserName"]), ArchConvert.Obj2String(dv[i]["ChangTitle"]));
				chkboxComsApplyDetailVersion.Items.Add(theEstApplyDetail);
			}
			dv.Dispose();
		}
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		int n = chkboxComsApplyDetailVersion.CheckedItems.Count;
		if (n > 0)
		{
			string[] BCA_UID = new string[n];
			DataView dvComsApplyDetailList = new DataView(ComsApplyDetailList.Tables[0]);
			for (int i = 0; i < n; i++)
			{
				EstApplyDetail theEstApplyDetail = chkboxComsApplyDetailVersion.CheckedItems[i] as EstApplyDetail;
				string Num = theEstApplyDetail.Num;
				dvComsApplyDetailList.RowFilter = "Num = '" + Num + "'";
				if (dvComsApplyDetailList.Count > 0)
				{
					BCA_UID[i] = dvComsApplyDetailList[0]["BCA_UID"].ToString().Trim();
				}
			}
			dvComsApplyDetailList.Dispose();
			dvComsApplyDetailList = null;
			BCA_UIDs = BCA_UID;
		}
		else
		{
			MessageBox.Show("注意：未選取任何資料。");
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.BudgetChange.FormComsApplyDetailList));
		this.chkboxComsApplyDetailVersion = new System.Windows.Forms.CheckedListBox();
		this.btnOK = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.labProjectName = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.chkboxComsApplyDetailVersion.Font = new System.Drawing.Font("新細明體", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.chkboxComsApplyDetailVersion.FormattingEnabled = true;
		this.chkboxComsApplyDetailVersion.Location = new System.Drawing.Point(9, 64);
		this.chkboxComsApplyDetailVersion.Name = "chkboxComsApplyDetailVersion";
		this.chkboxComsApplyDetailVersion.Size = new System.Drawing.Size(534, 274);
		this.chkboxComsApplyDetailVersion.TabIndex = 0;
		this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnOK.Location = new System.Drawing.Point(387, 353);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new System.Drawing.Size(75, 29);
		this.btnOK.TabIndex = 2;
		this.btnOK.Text = "確認";
		this.btnOK.UseVisualStyleBackColor = true;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Location = new System.Drawing.Point(468, 353);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(75, 29);
		this.btnCancel.TabIndex = 3;
		this.btnCancel.Text = "取消";
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("新細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label1.Location = new System.Drawing.Point(9, 12);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(46, 13);
		this.label1.TabIndex = 4;
		this.label1.Text = "專案：";
		this.labProjectName.AutoSize = true;
		this.labProjectName.Location = new System.Drawing.Point(61, 12);
		this.labProjectName.Name = "labProjectName";
		this.labProjectName.Size = new System.Drawing.Size(127, 12);
		this.labProjectName.TabIndex = 5;
		this.labProjectName.Text = "ProjectCode_ProjectName";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("新細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label2.Location = new System.Drawing.Point(9, 34);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(46, 13);
		this.label2.TabIndex = 6;
		this.label2.Text = "版次：";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(555, 394);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.labProjectName);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.btnOK);
		base.Controls.Add(this.chkboxComsApplyDetailVersion);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormComsApplyDetailList";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "已核准之預算變更";
		base.Load += new System.EventHandler(FormComsApplyDetailList_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
