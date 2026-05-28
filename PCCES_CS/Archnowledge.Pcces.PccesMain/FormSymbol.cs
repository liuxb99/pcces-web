using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain;

public class FormSymbol : Form
{
	public delegate void UserRequest(object sender, EventArgs e);

	private GroupBox groupBox1;

	private UltraButton BtnHelp;

	private Container components = null;

	private string F_SymbolFileName = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "Symbols.ini";

	public event UserRequest OnUserRequest;

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.FormSymbol));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.BtnHelp = new Infragistics.Win.Misc.UltraButton();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Location = new System.Drawing.Point(8, 24);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(259, 136);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.BtnHelp.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.ImageHAlign = Infragistics.Win.HAlign.Center;
		this.BtnHelp.Appearance = appearance1;
		this.BtnHelp.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button;
		this.BtnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.BtnHelp.Location = new System.Drawing.Point(242, 4);
		this.BtnHelp.Name = "BtnHelp";
		this.BtnHelp.ShowFocusRect = false;
		this.BtnHelp.ShowOutline = false;
		this.BtnHelp.Size = new System.Drawing.Size(24, 24);
		this.BtnHelp.SupportThemes = false;
		this.BtnHelp.TabIndex = 1;
		this.BtnHelp.Click += new System.EventHandler(BtnHelp_Click);
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 18);
		base.ClientSize = new System.Drawing.Size(274, 168);
		base.Controls.Add(this.BtnHelp);
		base.Controls.Add(this.groupBox1);
		this.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.Name = "FormSymbol";
		this.Text = "符號表";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormSymbol_FormClosing);
		base.Load += new System.EventHandler(FormSymbol_Load);
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

	public FormSymbol()
	{
		InitializeComponent();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		string ssText = (sender as Button).Text;
		this.OnUserRequest(this, new UserRequestEventArgs(ssText));
	}

	private void FormSymbol_Load(object sender, EventArgs e)
	{
		string sType = CommonMethods.IniReadValue(F_SymbolFileName, "Symbols", "Type");
		string sSymbols1 = CommonMethods.IniReadValue(F_SymbolFileName, "Symbols", "Symbols1");
		string sSymbols2 = CommonMethods.IniReadValue(F_SymbolFileName, "Symbols", "Symbols");
		string sMarkk = "";
		sMarkk = ((!(sType == "1")) ? "αβγπθλμντ∮ω◎●★☆％「」『』【】℃℉≒≠≦≧√∞〃㊣㎏㎜㎝㎞㎡㏎㏑㏒" : "α,β,γ,π,θ,λ,μ,ν,τ,∮,ω,◎,●,★,☆,％,「,」,『,』,【,】,℃,℉,≒,≠,≦,≧,√,∞,〃,㊣,㎏,㎜,㎝,㎞,㎡,㏎,㏑,㏒");
		DataTable DT_Table = new DataTable();
		DT_Table.Columns.Add("Mark", Type.GetType("System.String"));
		if (sType == "1")
		{
			if (sSymbols1 != "")
			{
				sMarkk = sSymbols1;
				string[] Split1 = sMarkk.Split(',');
				for (int i = 0; i < Split1.Length; i++)
				{
					DataRow DR = DT_Table.NewRow();
					DR["Mark"] = Split1[i];
					DT_Table.Rows.Add(DR);
				}
			}
		}
		else
		{
			if (sSymbols2 != "")
			{
				sMarkk = sSymbols2;
			}
			for (int i = 0; i < sMarkk.Length; i++)
			{
				DataRow DR = DT_Table.NewRow();
				DR["Mark"] = sMarkk[i];
				DT_Table.Rows.Add(DR);
			}
		}
		for (int i = 0; i < DT_Table.Rows.Count; i++)
		{
			Button BT = new Button();
			BT.Name = "BT" + i;
			BT.Text = DT_Table.Rows[i]["Mark"].ToString();
			BT.Width = 32;
			BT.Height = 32;
			BT.Font = new Font("細明體", 11f);
			BT.Click += button1_Click;
			BT.Left = i % 8 * 32 + 2;
			BT.Top = i / 8 * 32 + 10;
			groupBox1.Controls.Add(BT);
		}
		int iInt = DT_Table.Rows.Count / 8;
		int iRemain = ((DT_Table.Rows.Count % 8 > 0) ? 1 : 0);
		base.Height = (iInt + iRemain + 1) * 32 + 15 + 28;
	}

	private void FormSymbol_FormClosing(object sender, FormClosingEventArgs e)
	{
		e.Cancel = true;
		Hide();
	}

	private void BtnHelp_Click(object sender, EventArgs e)
	{
		MessageBox.Show(this, "使用者可以自行維護符號表，請修改\n" + AppDomain.CurrentDomain.BaseDirectory + "Symbols.ini", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}
}
