using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using AxCrp92Ocx;

namespace Archnowledge.Pcces.PccesMain.Report;

public class ucCrystalViewer : UserControl
{
	private string F_ReportPath;

	private string F_ReportName;

	private string F_RealDBFName;

	private int F_CompHeight;

	private int F_CompWidth;

	private string F_Params;

	private AxActiveXCrp92 axActiveXCrp921;

	private Container components = null;

	public string _ReportPath
	{
		get
		{
			return F_ReportPath;
		}
		set
		{
			F_ReportPath = value;
		}
	}

	public string _ReportName
	{
		get
		{
			return F_ReportName;
		}
		set
		{
			F_ReportName = value;
		}
	}

	public string _DBFName
	{
		get
		{
			return F_RealDBFName;
		}
		set
		{
			F_RealDBFName = value;
		}
	}

	public int _CompHeight
	{
		get
		{
			return F_CompHeight;
		}
		set
		{
			F_CompHeight = value;
		}
	}

	public int _CompWidth
	{
		get
		{
			return F_CompWidth;
		}
		set
		{
			F_CompWidth = value;
		}
	}

	public string _Params
	{
		get
		{
			return F_Params;
		}
		set
		{
			F_Params = value;
		}
	}

	public ucCrystalViewer()
	{
		InitializeComponent();
	}

	public void Execute()
	{
		Application.DoEvents();
		axActiveXCrp921.Width = axActiveXCrp921.Parent.Width;
		axActiveXCrp921.Height = axActiveXCrp921.Parent.Height;
		axActiveXCrp921.CompHeight = F_CompHeight.ToString();
		axActiveXCrp921.CompWidth = F_CompWidth.ToString();
		axActiveXCrp921.RealDBFName = F_RealDBFName;
		axActiveXCrp921.ReportPath = F_ReportPath;
		axActiveXCrp921.ReportName = F_ReportName;
		string sButtons = CommonMethods.GetIniValue("CRP", "CRP_BUTTONS");
		if (sButtons != "")
		{
			axActiveXCrp921.ExpBtnValue = sButtons;
		}
		else
		{
			axActiveXCrp921.ExpBtnValue = "16";
		}
		axActiveXCrp921.Params = F_Params;
		axActiveXCrp921.ShowReport = "[WINFORM]";
	}

	private void InitializeComponent()
	{
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Report.ucCrystalViewer));
		this.axActiveXCrp921 = new AxCrp92Ocx.AxActiveXCrp92();
		((System.ComponentModel.ISupportInitialize)this.axActiveXCrp921).BeginInit();
		base.SuspendLayout();
		this.axActiveXCrp921.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.axActiveXCrp921.Location = new System.Drawing.Point(1, 1);
		this.axActiveXCrp921.Name = "axActiveXCrp921";
		this.axActiveXCrp921.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("axActiveXCrp921.OcxState");
		this.axActiveXCrp921.Size = new System.Drawing.Size(651, 495);
		this.axActiveXCrp921.TabIndex = 0;
		base.Controls.Add(this.axActiveXCrp921);
		base.Name = "ucCrystalViewer";
		base.Size = new System.Drawing.Size(660, 504);
		((System.ComponentModel.ISupportInitialize)this.axActiveXCrp921).EndInit();
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
