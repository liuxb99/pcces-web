using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTabs;

namespace Archnowledge.Pcces.PccesMain.Invoice;

public class FormInvoiceIndexNumber : Form
{
	private const string CallFormHelp = "FormInvoiceIndexNumber";

	private Panel panel9;

	private UltraButton A1_Btn_Next;

	private GroupBox groupBox5;

	private UltraButton A1_Btn_Cncl;

	private Panel panel1;

	private Label label1;

	private UltraTabControl ultraTabControl1;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl ultraTabPageControl1;

	private Panel panel2;

	private UltraTabPageControl ultraTabPageControl2;

	private Panel panel4;

	private RichTextBox richTextBox1;

	private UltraTextEditor txt_B;

	private UltraTextEditor txt_C;

	private Label label2;

	private Label label3;

	private UltraLabel ultraLabel2;

	private UltraLabel lblRate;

	private Label label4;

	private UltraTextEditor txt_E;

	private UltraLabel ultraLabel1;

	private Label label5;

	private UltraLabel ultraLabel3;

	private UltraTextEditor txt_T;

	private Label label6;

	private Label label7;

	private UltraTextEditor txt_A;

	private Label label8;

	private Label label9;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel6;

	private UltraLabel lbl_Total;

	private Button button1;

	private System.Windows.Forms.ToolTip toolTip1;

	private IContainer components;

	private string F_UserID;

	private string F_ProjectCode;

	private PccesFormAction F_ActionName;

	private string F_Issue;

	private string F_AccAdv;

	private string F_ContractTotal;

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

	public string _AccAdv
	{
		get
		{
			return F_AccAdv;
		}
		set
		{
			F_AccAdv = value;
		}
	}

	public string _ContractTotal
	{
		get
		{
			return F_ContractTotal;
		}
		set
		{
			F_ContractTotal = value;
		}
	}

	public FormInvoiceIndexNumber()
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Invoice.FormInvoiceIndexNumber));
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.button1 = new System.Windows.Forms.Button();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.lbl_Total = new Infragistics.Win.Misc.UltraLabel();
		this.label9 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.txt_A = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.txt_T = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.label5 = new System.Windows.Forms.Label();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.txt_E = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.label4 = new System.Windows.Forms.Label();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.lblRate = new Infragistics.Win.Misc.UltraLabel();
		this.label3 = new System.Windows.Forms.Label();
		this.txt_C = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.label2 = new System.Windows.Forms.Label();
		this.txt_B = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.panel2 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.richTextBox1 = new System.Windows.Forms.RichTextBox();
		this.panel4 = new System.Windows.Forms.Panel();
		this.panel9 = new System.Windows.Forms.Panel();
		this.A1_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.A1_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.ultraTabPageControl1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txt_A).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txt_T).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txt_E).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txt_C).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txt_B).BeginInit();
		this.ultraTabPageControl2.SuspendLayout();
		this.panel9.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraTabControl1).BeginInit();
		this.ultraTabControl1.SuspendLayout();
		base.SuspendLayout();
		this.ultraTabPageControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.ultraTabPageControl1.Controls.Add(this.button1);
		this.ultraTabPageControl1.Controls.Add(this.ultraLabel6);
		this.ultraTabPageControl1.Controls.Add(this.ultraLabel5);
		this.ultraTabPageControl1.Controls.Add(this.lbl_Total);
		this.ultraTabPageControl1.Controls.Add(this.label9);
		this.ultraTabPageControl1.Controls.Add(this.label8);
		this.ultraTabPageControl1.Controls.Add(this.txt_A);
		this.ultraTabPageControl1.Controls.Add(this.label7);
		this.ultraTabPageControl1.Controls.Add(this.label6);
		this.ultraTabPageControl1.Controls.Add(this.ultraLabel3);
		this.ultraTabPageControl1.Controls.Add(this.txt_T);
		this.ultraTabPageControl1.Controls.Add(this.label5);
		this.ultraTabPageControl1.Controls.Add(this.ultraLabel1);
		this.ultraTabPageControl1.Controls.Add(this.txt_E);
		this.ultraTabPageControl1.Controls.Add(this.label4);
		this.ultraTabPageControl1.Controls.Add(this.ultraLabel2);
		this.ultraTabPageControl1.Controls.Add(this.lblRate);
		this.ultraTabPageControl1.Controls.Add(this.label3);
		this.ultraTabPageControl1.Controls.Add(this.txt_C);
		this.ultraTabPageControl1.Controls.Add(this.label2);
		this.ultraTabPageControl1.Controls.Add(this.txt_B);
		this.ultraTabPageControl1.Controls.Add(this.panel2);
		this.ultraTabPageControl1.Controls.Add(this.label1);
		this.ultraTabPageControl1.Location = new System.Drawing.Point(2, 29);
		this.ultraTabPageControl1.Name = "ultraTabPageControl1";
		this.ultraTabPageControl1.Size = new System.Drawing.Size(556, 361);
		this.button1.BackColor = System.Drawing.SystemColors.Control;
		this.button1.Location = new System.Drawing.Point(406, 310);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(96, 25);
		this.button1.TabIndex = 22;
		this.button1.Text = "重新計算";
		this.button1.Click += new System.EventHandler(button1_Click);
		this.ultraLabel6.Location = new System.Drawing.Point(509, 219);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(24, 23);
		this.ultraLabel6.TabIndex = 21;
		this.ultraLabel6.Text = "元";
		this.ultraLabel5.Location = new System.Drawing.Point(285, 312);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(24, 23);
		this.ultraLabel5.TabIndex = 20;
		this.ultraLabel5.Text = "元";
		appearance1.TextHAlign = Infragistics.Win.HAlign.Right;
		this.lbl_Total.Appearance = appearance1;
		this.lbl_Total.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.Solid;
		this.lbl_Total.Location = new System.Drawing.Point(104, 312);
		this.lbl_Total.Name = "lbl_Total";
		this.lbl_Total.Size = new System.Drawing.Size(176, 23);
		this.lbl_Total.TabIndex = 19;
		this.lbl_Total.Text = "100";
		this.label9.Location = new System.Drawing.Point(11, 312);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(93, 23);
		this.label9.TabIndex = 18;
		this.label9.Text = "\u3000\u3000\u3000\u3000 = ";
		this.label8.Location = new System.Drawing.Point(106, 280);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(325, 23);
		this.label8.TabIndex = 17;
		this.label8.Text = "A x (1-E) x (指數增減率之絕對值 - 2.5%) x F";
		this.txt_A.Location = new System.Drawing.Point(336, 216);
		this.txt_A.Name = "txt_A";
		this.txt_A.Size = new System.Drawing.Size(168, 24);
		this.txt_A.TabIndex = 16;
		this.txt_A.Text = "0";
		this.toolTip1.SetToolTip(this.txt_A, "請填入本期估驗金額之直接工程費");
		this.txt_A.ValueChanged += new System.EventHandler(txt_B_ValueChanged);
		this.label7.Location = new System.Drawing.Point(11, 217);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(325, 23);
		this.label7.TabIndex = 15;
		this.label7.Text = "直接工程費(A) = ";
		this.label6.Location = new System.Drawing.Point(11, 280);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(93, 23);
		this.label6.TabIndex = 14;
		this.label6.Text = "調整金額 = ";
		this.ultraLabel3.Location = new System.Drawing.Point(438, 186);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(24, 23);
		this.ultraLabel3.TabIndex = 13;
		this.ultraLabel3.Text = "%";
		this.txt_T.Location = new System.Drawing.Point(336, 184);
		this.txt_T.Name = "txt_T";
		this.txt_T.Size = new System.Drawing.Size(100, 24);
		this.txt_T.TabIndex = 12;
		this.txt_T.Text = "5";
		this.txt_T.ValueChanged += new System.EventHandler(txt_B_ValueChanged);
		this.label5.Location = new System.Drawing.Point(11, 184);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(325, 23);
		this.label5.TabIndex = 11;
		this.label5.Text = "營業稅率(T) = ";
		this.ultraLabel1.Location = new System.Drawing.Point(437, 152);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(24, 23);
		this.ultraLabel1.TabIndex = 10;
		this.ultraLabel1.Text = "%";
		this.txt_E.Location = new System.Drawing.Point(336, 152);
		this.txt_E.Name = "txt_E";
		this.txt_E.Size = new System.Drawing.Size(100, 24);
		this.txt_E.TabIndex = 9;
		this.txt_E.Text = "30";
		this.txt_E.ValueChanged += new System.EventHandler(txt_B_ValueChanged);
		this.label4.Location = new System.Drawing.Point(11, 152);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(325, 23);
		this.label4.TabIndex = 8;
		this.label4.Text = "已付預付款之最高額佔契約總價之百分比(E) = ";
		this.ultraLabel2.Location = new System.Drawing.Point(352, 92);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(24, 23);
		this.ultraLabel2.TabIndex = 7;
		this.ultraLabel2.Text = "%";
		appearance2.TextHAlign = Infragistics.Win.HAlign.Right;
		this.lblRate.Appearance = appearance2;
		this.lblRate.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.Solid;
		this.lblRate.Location = new System.Drawing.Point(288, 89);
		this.lblRate.Name = "lblRate";
		this.lblRate.Size = new System.Drawing.Size(64, 23);
		this.lblRate.TabIndex = 6;
		this.lblRate.Text = "100";
		this.label3.Location = new System.Drawing.Point(11, 92);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(277, 23);
		this.label3.TabIndex = 5;
		this.label3.Text = "指數增減率 =  [ ( B / C ) - 1 ] x 100%  =";
		this.txt_C.Location = new System.Drawing.Point(184, 60);
		this.txt_C.Name = "txt_C";
		this.txt_C.Size = new System.Drawing.Size(100, 24);
		this.txt_C.TabIndex = 4;
		this.txt_C.Text = "100";
		this.txt_C.ValueChanged += new System.EventHandler(txt_B_ValueChanged);
		this.label2.Location = new System.Drawing.Point(11, 62);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(168, 23);
		this.label2.TabIndex = 3;
		this.label2.Text = "開標當月總指數(C) = ";
		this.txt_B.Location = new System.Drawing.Point(184, 32);
		this.txt_B.Name = "txt_B";
		this.txt_B.Size = new System.Drawing.Size(100, 24);
		this.txt_B.TabIndex = 2;
		this.txt_B.Text = "100";
		this.txt_B.ValueChanged += new System.EventHandler(txt_B_ValueChanged);
		this.panel2.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(554, 8);
		this.panel2.TabIndex = 1;
		this.label1.Location = new System.Drawing.Point(11, 35);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(168, 23);
		this.label1.TabIndex = 0;
		this.label1.Text = "估驗日當月總指數(B) = ";
		this.ultraTabPageControl2.Controls.Add(this.richTextBox1);
		this.ultraTabPageControl2.Controls.Add(this.panel4);
		this.ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabPageControl2.Name = "ultraTabPageControl2";
		this.ultraTabPageControl2.Size = new System.Drawing.Size(556, 361);
		this.richTextBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.richTextBox1.Location = new System.Drawing.Point(6, 17);
		this.richTextBox1.Name = "richTextBox1";
		this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
		this.richTextBox1.Size = new System.Drawing.Size(544, 336);
		this.richTextBox1.TabIndex = 3;
		this.richTextBox1.Text = "壹、處理措施\n一、機關辦理工程採購，實際完工日期在九十二年十月一日以後者，因\n\u3000\u3000近期國內營建物價劇烈變動，廠商要求依本處理原則協議調整工程\n\u3000\u3000款且機關原預算相關經費足敷支應者，無論原契約是否訂有物價調\n\u3000\u3000整規定，機關應同意以行政院主計處公布之台灣地區營造工程物價\n\u3000\u3000指數表內之總指數（以下簡稱總指數），就漲跌幅超過2.5%部分，\n\u3000\u3000辦理工程款調整(含增加或扣減應給付之契約價金)，惟應先辦理契\n\u3000\u3000約變更，加列物價指數調整相關規定。\n\n二、前點契約變更後加列之物價指數調整規定，廠商要求溯及適用於九\n\u3000\u3000十二年十月一日以後施作部分之工程款者，機關應予同意。\n\n三、機關依第一點辦理契約變更，應一併載明下列事項：\n（一）適用期限至九十三年十二月三十一日以前施作部分之工程款。\n（二）未完成契約變更前已辦理估驗之工程款是否適用契約變更後加列之\n\u3000\u3000\u3000物價指數調整規定。屬適用者，應載明其得追溯之期間。惟不得溯\n\u3000\u3000\u3000及於九十二年九月三十日以前施作部分。\n（三）契約原有依其他指數（例如金屬製品類指數等）調整之營建物價調\n\u3000\u3000\u3000整規定，應自前款追溯期間之起始日起停止適用。前述起始日之後\n\u3000\u3000\u3000施作部分，已辦理營建物價調整者，應重新核算營建物價調整款。\n（四）逾履約期限之部分，應以估驗當期總指數與契約規定履約期限當月\n\u3000\u3000\u3000總指數二者較低者為調整依據。但逾期履約係非可歸責於廠商者，\n\u3000\u3000\u3000應以估驗當期總指數為調整依據。\n（五）物價指數基期更換時，換基當月起實際施作之數量，自動適用新基\n\u3000\u3000\u3000期指數核算工程調整款，原依舊基期指數調整之工程款不予追溯核\n\u3000\u3000\u3000算。\n（六）原契約所訂估驗計價保留款規定，適用於物價調整款之支付。\n（七）各期物價調整款應於各該期總指數公布後方予核算。\n（八）物價調整款如因相關節餘款無法支應而需以次一年度編列預算支付\n\u3000\u3000\u3000者，須俟預算完成法定程序後，無息支付。\n（九）物價調整款之計算方式。\n（十）其他必要事項。\n\n四、機關依本處理原則辦理物價調整所需增加之經費，應優先自各該工\n\u3000\u3000程發包節餘款支應或各該計畫奉核定預算內勻支，如有不足，則依\n\u3000\u3000規定在相關科目內勻用，或於行政院核定之次一年度歲出概算額度\n\u3000\u3000內編列支應。\n\n五、法人或團體接受機關補助辦理工程採購，不適用本處理原則。\n\n貳、計算方式\n一、\u3000原契約未有依營造工程物價總指數調整計算方式之規定者，依下列方\n\u3000\u3000\u3000式辦理，並載明於契約：\n\n（一）估驗日當月總指數比較開標當月總指數，其指數增減率之絕對值超過\n\u3000\u3000\u3000 2.5%者，就漲跌幅超過2.5%部分，於估驗完成後就估驗款中之直接工\n\u3000\u3000\u3000程費調整工程款；指數增減率之絕對值在2.5%以內者，不予調整。\n\u3000\u3000\u3000指數增減率＝[（Ｂ／Ｃ）－１] ×100%\n\u3000\u3000\u3000其中\u3000Ｂ＝估驗日當月總指數。﹝1.估驗日係指估驗內容之最後施工\n\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000日；2.估驗內容跨越依本處理原則辦理契約變更所約定之\n\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000適用期限末日（暫定為93年12月31日）者，當期之B值為\n\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000該適用期限末日所對應之當月總指數﹞\n\u3000\u3000\u3000\u3000\u3000\u3000Ｃ＝開標當月總指數。但就契約新增單價項目之工程款，為該\n\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000項目議價完成日當月總指數。\n\u3000\u3000\u3000\u3000\u3000\u3000指數增減率由百分比換算為數值後，計算至小數點以下第四位\n\u3000\u3000\u3000\u3000\u3000\u3000(第五位四捨五入)。\n\n（二）每期估驗款均以下列公式計算調整金額(計算至元，元以下四捨五入):\n\u3000\u3000\u3000Ａ×(１－Ｅ)×(指數增減率之絕對值－2.5%)×F\n\u3000\u3000\u3000其中\u3000Ａ＝當期估驗之直接工程費﹝1.跨越92年10月1日者，指該日以\n\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000後施作部分之金額；2.跨越依本處理原則辦理契約變更所\n\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000約定之適用期限末日（暫定為93年12月31日）者，指該適\n\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000用期限末日以前施作部分之金額﹞\n\u3000\u3000\u3000\u3000\u3000\u3000\u3000＝當期估驗款扣除其中之規費、規劃費、設計費、土地及權\n\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000利費用、法律費用、承商管理費、保險費、利潤、利息及\n\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000稅雜費。直接工程費難以計算者，得經雙方合意以當期估\n\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000驗款之百分之八十代之。\n\u3000\u3000\u3000\u3000\u3000\u3000Ｅ＝已付預付款之最高額佔契約總價之百分比\n\u3000\u3000\u3000\u3000\u3000\u3000Ｆ＝(1+營業稅率)。營業稅率應核實計之。另依離島建設條例\n\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000第十條規定免徵營業稅廠商，其F值，得以1.05代之。\n\n（三）指數增減率為正值者，就上開調整金額給予補償﹔指數增減率為負值\n\u3000\u3000\u3000者，就上開調整金額自估驗款中扣減。\n\n二、原契約已訂有依營造工程物價總指數調整規定者，得經雙方合意，\n\u3000\u3000依原契約規定之方式辦理。\n\n參、範例\n某標工程已知條件(所用數值為假設值)\n一、開標(92年1月8日)當月之台灣地區營造工程物價指數表5-1內之總指數為\n\u3000\u3000104.19。\n\n二、93年2月5日辦理估驗請款，當期估驗內容之最後施工日為同年1月18日，\n\u3000\u3000查93年1月份當月之台灣地區營造工程物價指數表5-1內之總指數為113.79。\n\n三、機關已付預付款為契約總價之百分之30%。\n\n四、假設93年2月5日辦理估驗之工程款為15,000,000元，其中直接工程費為\n\u3000\u300012,000,000元，則當期估驗款之物價調整計算如下：\n\n\u3000\u3000１、指數增減率=[(113.79/104.19)-1] x 100% =9.21%\n\u3000\u3000２、調整金額\n\u3000\u3000\u3000\u3000Ａ×(１－Ｅ)×(指數增減率之絕對值－2.5%)×F\n\u3000\u3000\u3000     =12,000,000x(1-0.3)x(0.0921-0.025)x1.05=591822\n\u3000\u3000\u3000\u3000故當期估驗款之物價調整金額為補償591,822元";
		this.richTextBox1.WordWrap = false;
		this.panel4.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel4.Location = new System.Drawing.Point(0, 0);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(556, 8);
		this.panel4.TabIndex = 2;
		this.panel9.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel9.Controls.Add(this.A1_Btn_Next);
		this.panel9.Controls.Add(this.groupBox5);
		this.panel9.Controls.Add(this.A1_Btn_Cncl);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel9.Location = new System.Drawing.Point(0, 402);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(568, 44);
		this.panel9.TabIndex = 22;
		this.A1_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.ImageHAlign = Infragistics.Win.HAlign.Left;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A1_Btn_Next.Appearance = appearance3;
		this.A1_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.A1_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A1_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.A1_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.A1_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.A1_Btn_Next.Location = new System.Drawing.Point(381, 9);
		this.A1_Btn_Next.Name = "A1_Btn_Next";
		this.A1_Btn_Next.ShowFocusRect = false;
		this.A1_Btn_Next.ShowOutline = false;
		this.A1_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.A1_Btn_Next.SupportThemes = false;
		this.A1_Btn_Next.TabIndex = 4;
		this.A1_Btn_Next.Text = "確定";
		this.A1_Btn_Next.Click += new System.EventHandler(A1_Btn_Next_Click);
		this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox5.Location = new System.Drawing.Point(0, 0);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(568, 8);
		this.groupBox5.TabIndex = 3;
		this.groupBox5.TabStop = false;
		this.A1_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance4.Image = resources.GetObject("appearance4.Image");
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A1_Btn_Cncl.Appearance = appearance4;
		this.A1_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A1_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A1_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A1_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A1_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A1_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A1_Btn_Cncl.Location = new System.Drawing.Point(472, 9);
		this.A1_Btn_Cncl.Name = "A1_Btn_Cncl";
		this.A1_Btn_Cncl.ShowFocusRect = false;
		this.A1_Btn_Cncl.ShowOutline = false;
		this.A1_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A1_Btn_Cncl.SupportThemes = false;
		this.A1_Btn_Cncl.TabIndex = 2;
		this.A1_Btn_Cncl.Text = "取消";
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.ultraTabControl1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(568, 402);
		this.panel1.TabIndex = 23;
		appearance5.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance5.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance5.ForeColor = System.Drawing.Color.Black;
		appearance5.TextVAlign = Infragistics.Win.VAlign.Top;
		this.ultraTabControl1.ActiveTabAppearance = appearance5;
		this.ultraTabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance6.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance6.BackColor2 = System.Drawing.Color.FromArgb(102, 153, 255);
		appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance6.TextVAlign = Infragistics.Win.VAlign.Top;
		this.ultraTabControl1.Appearance = appearance6;
		appearance7.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance7.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraTabControl1.ClientAreaAppearance = appearance7;
		this.ultraTabControl1.Controls.Add(this.ultraTabSharedControlsPage1);
		this.ultraTabControl1.Controls.Add(this.ultraTabPageControl1);
		this.ultraTabControl1.Controls.Add(this.ultraTabPageControl2);
		this.ultraTabControl1.FlatMode = true;
		this.ultraTabControl1.HotTrack = true;
		this.ultraTabControl1.InterTabSpacing = new Infragistics.Win.DefaultableInteger(0);
		this.ultraTabControl1.Location = new System.Drawing.Point(4, 8);
		this.ultraTabControl1.MultiRowSelectionStyle = Infragistics.Win.UltraWinTabs.MultiRowSelectionStyle.SwapRow;
		this.ultraTabControl1.Name = "ultraTabControl1";
		this.ultraTabControl1.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.ultraTabControl1.ShowButtonSeparators = true;
		this.ultraTabControl1.Size = new System.Drawing.Size(560, 392);
		this.ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
		this.ultraTabControl1.TabIndex = 11;
		this.ultraTabControl1.TabPadding = new System.Drawing.Size(1, 3);
		appearance8.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance8.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance8.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		appearance8.BorderColor3DBase = System.Drawing.Color.FromArgb(96, 145, 234);
		ultraTab1.ActiveAppearance = appearance8;
		appearance9.BorderColor = System.Drawing.Color.Transparent;
		ultraTab1.Appearance = appearance9;
		ultraTab1.TabPage = this.ultraTabPageControl1;
		ultraTab1.Text = "物價調整款計算";
		appearance10.TextVAlign = Infragistics.Win.VAlign.Top;
		ultraTab2.Appearance = appearance10;
		ultraTab2.TabPage = this.ultraTabPageControl2;
		ultraTab2.Text = "物調調整處理原則";
		this.ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2] { ultraTab1, ultraTab2 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(556, 361);
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.CancelButton = this.A1_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(568, 446);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel9);
		base.KeyPreview = true;
		base.Name = "FormInvoiceIndexNumber";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "物價調整處理";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormInvoiceIndexNumber_KeyDown);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormInvoiceIndexNumber_FormClosing);
		base.Load += new System.EventHandler(FormInvoiceIndexNumber_Load);
		this.ultraTabPageControl1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txt_A).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txt_T).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txt_E).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txt_C).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txt_B).EndInit();
		this.ultraTabPageControl2.ResumeLayout(false);
		this.panel9.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraTabControl1).EndInit();
		this.ultraTabControl1.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void txt_B_ValueChanged(object sender, EventArgs e)
	{
		lblRate.Text = Convert.ToString(PubTools.ARound(PubTools.Str2Double(txt_B.Text) / PubTools.Str2Double(txt_C.Text) - 1.0, 4L) * 100.0);
		int iSign = 1;
		if (PubTools.Str2Double(lblRate.Text) < 0.0)
		{
			iSign = -1;
		}
		lbl_Total.Text = $"{(double)iSign * PubTools.Str2Double(txt_A.Text) * (1.0 - PubTools.Str2Double(txt_E.Text) / 100.0) * (Math.Abs(PubTools.Str2Double(lblRate.Text) / 100.0) - 0.025) * (1.0 + PubTools.Str2Double(txt_T.Text) / 100.0):N0}";
		if (Math.Abs(PubTools.Str2Double(lblRate.Text)) < 2.5)
		{
			lbl_Total.Text = "0";
		}
	}

	private void FormInvoiceIndexNumber_Load(object sender, EventArgs e)
	{
		if (!LoadFromDataBase())
		{
			double d_RateE = PubTools.Str2Double(F_AccAdv) / PubTools.Str2Double(F_ContractTotal);
			if (PubTools.Str2Double(F_ContractTotal) == 0.0)
			{
				d_RateE = 0.0;
			}
			txt_E.Text = (PubTools.ARound(d_RateE, 4L) * 100.0).ToString();
		}
		CalculateRate();
		LoadingScreen();
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("InvoiceIndexNumber", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("InvoiceIndexNumber", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("InvoiceIndexNumber", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("InvoiceIndexNumber", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("InvoiceIndexNumber", "PK_Height"));
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

	private void CalculateRate()
	{
		lblRate.Text = Convert.ToString(PubTools.ARound(PubTools.Str2Double(txt_B.Text) / PubTools.Str2Double(txt_C.Text) - 1.0, 4L) * 100.0);
		int iSign = 1;
		if (PubTools.Str2Double(lblRate.Text) < 0.0)
		{
			iSign = -1;
		}
		lbl_Total.Text = $"{(double)iSign * PubTools.Str2Double(txt_A.Text) * (1.0 - PubTools.Str2Double(txt_E.Text) / 100.0) * (Math.Abs(PubTools.Str2Double(lblRate.Text) / 100.0) - 0.025) * (1.0 + PubTools.Str2Double(txt_T.Text) / 100.0):N0}";
		if (Math.Abs(PubTools.Str2Double(lblRate.Text)) < 2.5)
		{
			lbl_Total.Text = "0";
		}
	}

	private bool LoadFromDataBase()
	{
		bool RetV = false;
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		DataTable DT_SUBINDEX = DBCLS.GetUserDefine("Select * from SubAccIndex where ProjectCode='" + F_ProjectCode + "' and chgCount=" + F_Issue + " ");
		if (DT_SUBINDEX.Rows.Count > 0)
		{
			txt_B.Text = DT_SUBINDEX.Rows[0]["Index_B"].ToString();
			txt_C.Text = DT_SUBINDEX.Rows[0]["Index_C"].ToString();
			txt_E.Text = DT_SUBINDEX.Rows[0]["Index_E"].ToString();
			txt_T.Text = DT_SUBINDEX.Rows[0]["Index_T"].ToString();
			txt_A.Text = DT_SUBINDEX.Rows[0]["Index_A"].ToString();
			lbl_Total.Text = DT_SUBINDEX.Rows[0]["Total"].ToString();
			RetV = true;
		}
		DBCLS = null;
		return RetV;
	}

	private void A1_Btn_Next_Click(object sender, EventArgs e)
	{
		try
		{
			Convert.ToDouble(txt_B.Text);
			Convert.ToDouble(txt_C.Text);
			Convert.ToDouble(txt_E.Text);
			Convert.ToDouble(txt_T.Text);
			Convert.ToDouble(txt_A.Text);
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Invoice.FormInvoiceIndexNumber.cs" + ex.Message);
			MessageBox.Show(this, "發現有非數字之文字!! 請先校正。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		string sCount = DBCLS.GetUserDefine_String("Select Count(*) as iCount From SubAccIndex Where ProjectCode='" + F_ProjectCode + "' and chgCount=" + F_Issue + " ", "iCount");
		string sSQL = "";
		string sTotal = PubTools.Str2Double(lbl_Total.Text).ToString();
		sSQL = ((PubTools.Str2Int(sCount) <= 0) ? ("Insert Into SubAccIndex(ProjectCode, chgCount, Index_B, Index_C, Index_E, Index_T, Index_A, Total)   values('" + F_ProjectCode + "'," + F_Issue + "," + txt_B.Text + "," + txt_C.Text + "," + txt_E.Text + "," + txt_T.Text + "," + txt_A.Text + "," + sTotal + ") ") : ("Update SubAccIndex Set Index_B=" + txt_B.Text + ", Index_C=" + txt_C.Text + ", Index_E=" + txt_E.Text + ",  Index_T=" + txt_T.Text + ", Index_A=" + txt_A.Text + ", Total=" + sTotal + "  Where ProjectCode='" + F_ProjectCode + "' and chgCount=" + F_Issue + " "));
		DBCLS.ExecuteCommand(sSQL);
		DBCLS = null;
		(base.Owner as FormInvoiceSubAcInfo)._IndexNumTotal = lbl_Total.Text;
		base.DialogResult = DialogResult.OK;
	}

	private void button1_Click(object sender, EventArgs e)
	{
		CalculateRate();
	}

	private void FormInvoiceIndexNumber_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("InvoiceIndexNumber", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("InvoiceIndexNumber", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("InvoiceIndexNumber", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("InvoiceIndexNumber", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("InvoiceIndexNumber", "WindowState", base.WindowState.ToString());
	}

	private void FormInvoiceIndexNumber_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormInvoiceIndexNumber");
		}
	}
}
