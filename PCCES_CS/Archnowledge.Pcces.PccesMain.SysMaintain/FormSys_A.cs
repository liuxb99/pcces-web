using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinToolbars;
using Infragistics.Win.UltraWinTree;

namespace Archnowledge.Pcces.PccesMain.SysMaintain;

public class FormSys_A : UserControl
{
	private Panel panel2;

	private Panel panel4;

	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_A;

	private UltraTabPageControl Tab_B;

	private Panel panel6;

	private Panel panel7;

	private UltraTree ultraTree1;

	private Panel panel3;

	private Panel panel9;

	private Panel panel11;

	private Panel panel10;

	private UltraButton BtnAddUser;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel2;

	private Panel panel1;

	private UltraLabel ultraLabel13;

	private Panel panel8;

	private Panel panel15;

	private UltraLabel ultraLabel4;

	private Splitter splitter2;

	private Panel panel16;

	private UltraLabel ultraLabel6;

	private Panel panel13;

	private Panel panel14;

	private UltraLabel ultraLabel8;

	private UltraLabel ultraLabel9;

	private UltraLabel ultraLabel10;

	private UltraLabel ultraLabel11;

	private UltraLabel ultraLabel12;

	private Panel panel12;

	private Panel panel17;

	private Panel panel18;

	private Splitter splitter1;

	private Panel panel19;

	private UltraLabel ultraLabel7;

	private UltraTree ultraTree2;

	private UltraLabel ultraLabel14;

	private UltraLabel ultraLabel15;

	private UltraComboEditor Cbo1;

	private UltraLabel ultraLabel16;

	public GridMrsBase GridGroups;

	private UltraButton BtnSaveGroup;

	private UltraTextEditor txtGroupID;

	private UltraTextEditor txtGroupName;

	private UltraButton BtnGRP_Del;

	private UltraButton BtnUser_Add;

	private UltraButton BtnUser_Del;

	public GridMrsBase GridUsers;

	private UltraTextEditor txtPwdConfirm;

	private UltraTextEditor txtPwd;

	private UltraTextEditor txtUserName;

	private UltraTextEditor txtUserID;

	private UltraButton BtnUser_Edt;

	private UltraButton ultraButton1;

	public GridMrsBase GridGroupUsers;

	public GridMrsBase GridUserGroups;

	private UltraButton BtnSaveUser;

	private UltraButton ultraButton2;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Bottom;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Right;

	private Label _Lbl1 = new Label();

	private UltraStatusBar ultraStatusBar1;

	private ImageList imageList2;

	private GroupBox groupBox1;

	private Panel panel5;

	private GroupBox groupBox2;

	private Panel panel20;

	private IContainer components;

	private UltraToolbarsManager ultraToolbarsManager1;

	private string FORM_STATUS = "INITIAL";

	private bool IsMouseClick = false;

	private int iAuthorityMSG_Count = 0;

	private bool IsCtrl = false;

	private DataTable DT_Nodes = new DataTable();

	private DataTable DT_Leaves = new DataTable();

	private DBClass DBCLS = new DBClass();

	private DataTable DT_Groups = new DataTable();

	private DataTable DT_GroupFuncs = new DataTable();

	private DataTable DT_GRPChk = new DataTable();

	private DataTable DT_GroupUsers = new DataTable();

	private DataTable DT_Users = new DataTable();

	private DataTable DT_UserFuncs = new DataTable();

	private DataTable DT_UserGroups = new DataTable();

	private DataTable DT_UsrChk = new DataTable();

	private string F_UserID;

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

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.UltraWinTree.UltraTreeNode ultraTreeNode1 = new Infragistics.Win.UltraWinTree.UltraTreeNode();
		Infragistics.Win.UltraWinTree.Override _override1 = new Infragistics.Win.UltraWinTree.Override();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.SysMaintain.FormSys_A));
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTree.UltraTreeNode ultraTreeNode2 = new Infragistics.Win.UltraWinTree.UltraTreeNode();
		Infragistics.Win.UltraWinTree.Override _override2 = new Infragistics.Win.UltraWinTree.Override();
		Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance61 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance62 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance63 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.Appearance appearance64 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance65 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance66 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance67 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance68 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance69 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance70 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDeleteGrp");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool1 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.Appearance appearance71 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance72 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDeleteGrp");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool2 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.Appearance appearance73 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Popup1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDeleteGrp");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool2 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Popup2");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEditUsr");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDeleteUsr");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDeleteUsr");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEditUsr");
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel8 = new System.Windows.Forms.Panel();
		this.ultraTree1 = new Infragistics.Win.UltraWinTree.UltraTree();
		this.BtnSaveGroup = new Infragistics.Win.Misc.UltraButton();
		this.splitter2 = new System.Windows.Forms.Splitter();
		this.panel16 = new System.Windows.Forms.Panel();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.GridGroupUsers = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.panel15 = new System.Windows.Forms.Panel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.panel9 = new System.Windows.Forms.Panel();
		this.panel20 = new System.Windows.Forms.Panel();
		this.BtnGRP_Del = new Infragistics.Win.Misc.UltraButton();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.panel10 = new System.Windows.Forms.Panel();
		this.txtGroupID = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel16 = new Infragistics.Win.Misc.UltraLabel();
		this.BtnAddUser = new Infragistics.Win.Misc.UltraButton();
		this.txtGroupName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.panel11 = new System.Windows.Forms.Panel();
		this.GridGroups = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.panel6 = new System.Windows.Forms.Panel();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel18 = new System.Windows.Forms.Panel();
		this.ultraTree2 = new Infragistics.Win.UltraWinTree.UltraTree();
		this.BtnSaveUser = new Infragistics.Win.Misc.UltraButton();
		this.splitter1 = new System.Windows.Forms.Splitter();
		this.panel19 = new System.Windows.Forms.Panel();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.GridUserGroups = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.panel17 = new System.Windows.Forms.Panel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel12 = new System.Windows.Forms.Panel();
		this.panel5 = new System.Windows.Forms.Panel();
		this.BtnUser_Edt = new Infragistics.Win.Misc.UltraButton();
		this.BtnUser_Del = new Infragistics.Win.Misc.UltraButton();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.panel14 = new System.Windows.Forms.Panel();
		this.BtnUser_Add = new Infragistics.Win.Misc.UltraButton();
		this.Cbo1 = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ultraLabel15 = new Infragistics.Win.Misc.UltraLabel();
		this.txtPwdConfirm = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.txtPwd = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.txtUserName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.txtUserID = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.panel13 = new System.Windows.Forms.Panel();
		this.GridUsers = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.panel7 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this._FormSys_B_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.Tab_A.SuspendLayout();
		this.panel8.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraTree1).BeginInit();
		this.panel16.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridGroupUsers).BeginInit();
		this.panel3.SuspendLayout();
		this.panel9.SuspendLayout();
		this.panel20.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.panel10.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtGroupID).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGroupName).BeginInit();
		this.panel11.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridGroups).BeginInit();
		this.Tab_B.SuspendLayout();
		this.panel18.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraTree2).BeginInit();
		this.panel19.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridUserGroups).BeginInit();
		this.panel1.SuspendLayout();
		this.panel12.SuspendLayout();
		this.panel5.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.panel14.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Cbo1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPwdConfirm).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPwd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserID).BeginInit();
		this.panel13.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridUsers).BeginInit();
		this.panel2.SuspendLayout();
		this.panel4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.panel8);
		this.Tab_A.Controls.Add(this.panel15);
		this.Tab_A.Controls.Add(this.panel3);
		this.Tab_A.Controls.Add(this.panel6);
		this.Tab_A.Location = new System.Drawing.Point(2, 29);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(636, 586);
		this.panel8.Controls.Add(this.ultraTree1);
		this.panel8.Controls.Add(this.BtnSaveGroup);
		this.panel8.Controls.Add(this.splitter2);
		this.panel8.Controls.Add(this.panel16);
		this.panel8.Controls.Add(this.ultraLabel4);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel8.Location = new System.Drawing.Point(275, 6);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(361, 580);
		this.panel8.TabIndex = 10;
		this.ultraTree1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraTree1.HideSelection = false;
		this.ultraTree1.Location = new System.Drawing.Point(0, 30);
		this.ultraTree1.Name = "ultraTree1";
		ultraTreeNode1.CheckedState = System.Windows.Forms.CheckState.Indeterminate;
		ultraTreeNode1.Key = "Root";
		ultraTreeNode1.Text = "Pcces Win 4.3  功能清單";
		this.ultraTree1.Nodes.AddRange(new Infragistics.Win.UltraWinTree.UltraTreeNode[1] { ultraTreeNode1 });
		_override1.NodeStyle = Infragistics.Win.UltraWinTree.NodeStyle.CheckBox;
		_override1.SelectionType = Infragistics.Win.UltraWinTree.SelectType.Single;
		this.ultraTree1.Override = _override1;
		this.ultraTree1.Size = new System.Drawing.Size(361, 381);
		this.ultraTree1.SupportThemes = false;
		this.ultraTree1.TabIndex = 8;
		this.ultraTree1.Visible = false;
		this.ultraTree1.AfterCollapse += new Infragistics.Win.UltraWinTree.AfterNodeChangedEventHandler(ultraTree1_AfterCollapse);
		this.ultraTree1.AfterExpand += new Infragistics.Win.UltraWinTree.AfterNodeChangedEventHandler(ultraTree1_AfterExpand);
		this.ultraTree1.MouseDown += new System.Windows.Forms.MouseEventHandler(ultraTree1_MouseDown);
		this.ultraTree1.Leave += new System.EventHandler(ultraTree1_Leave);
		this.ultraTree1.AfterCheck += new Infragistics.Win.UltraWinTree.AfterNodeChangedEventHandler(ultraTree1_AfterCheck);
		this.ultraTree1.MouseUp += new System.Windows.Forms.MouseEventHandler(ultraTree1_MouseUp);
		this.ultraTree1.KeyUp += new System.Windows.Forms.KeyEventHandler(ultraTree1_KeyUp);
		this.ultraTree1.KeyDown += new System.Windows.Forms.KeyEventHandler(ultraTree1_KeyDown);
		this.BtnSaveGroup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Top;
		this.BtnSaveGroup.Appearance = appearance1;
		this.BtnSaveGroup.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.BtnSaveGroup.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.BtnSaveGroup.Location = new System.Drawing.Point(259, 3);
		this.BtnSaveGroup.Name = "BtnSaveGroup";
		this.BtnSaveGroup.Size = new System.Drawing.Size(96, 23);
		this.BtnSaveGroup.SupportThemes = false;
		this.BtnSaveGroup.TabIndex = 12;
		this.BtnSaveGroup.Text = "儲存功能清單";
		this.BtnSaveGroup.Visible = false;
		this.BtnSaveGroup.Click += new System.EventHandler(BtnSaveGroup_Click);
		this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.splitter2.Location = new System.Drawing.Point(0, 411);
		this.splitter2.Name = "splitter2";
		this.splitter2.Size = new System.Drawing.Size(361, 5);
		this.splitter2.TabIndex = 10;
		this.splitter2.TabStop = false;
		this.panel16.Controls.Add(this.ultraButton1);
		this.panel16.Controls.Add(this.GridGroupUsers);
		this.panel16.Controls.Add(this.ultraLabel6);
		this.panel16.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel16.Location = new System.Drawing.Point(0, 416);
		this.panel16.Name = "panel16";
		this.panel16.Size = new System.Drawing.Size(361, 164);
		this.panel16.TabIndex = 11;
		this.ultraButton1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Top;
		this.ultraButton1.Appearance = appearance2;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton1.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton1.Location = new System.Drawing.Point(259, 4);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.Size = new System.Drawing.Size(96, 23);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 13;
		this.ultraButton1.Text = "群組成員變更";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		this.GridGroupUsers._ExcelFileName = "";
		this.GridGroupUsers._ExcelSheeName = "";
		this.GridGroupUsers._IsOpenExcelAfterExport = false;
		this.GridGroupUsers.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.GridGroupUsers.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.GridGroupUsers.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridGroupUsers.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.GridGroupUsers.ColumnInfo = resources.GetString("GridGroupUsers.ColumnInfo");
		this.GridGroupUsers.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridGroupUsers.ExtendLastCol = true;
		this.GridGroupUsers.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridGroupUsers.ForeColor = System.Drawing.Color.Black;
		this.GridGroupUsers.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.GridGroupUsers.IsProcessUndo = false;
		this.GridGroupUsers.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.GridGroupUsers.Location = new System.Drawing.Point(0, 30);
		this.GridGroupUsers.Name = "GridGroupUsers";
		this.GridGroupUsers.Rows.Count = 1;
		this.GridGroupUsers.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.GridGroupUsers.ShowCursor = true;
		this.GridGroupUsers.ShowToolTipOnNarrowColumn = true;
		this.GridGroupUsers.Size = new System.Drawing.Size(361, 134);
		this.GridGroupUsers.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("GridGroupUsers.Styles"));
		this.GridGroupUsers.TabIndex = 11;
		this.GridGroupUsers.UndoMax = 10;
		appearance3.FontData.Name = "細明體";
		appearance3.ForeColor = System.Drawing.Color.White;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel6.Appearance = appearance3;
		this.ultraLabel6.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel6.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel6.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(361, 30);
		this.ultraLabel6.TabIndex = 10;
		this.ultraLabel6.Text = " 群組成員";
		appearance4.FontData.Name = "細明體";
		appearance4.ForeColor = System.Drawing.Color.White;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance4;
		this.ultraLabel4.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel4.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel4.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(361, 30);
		this.ultraLabel4.TabIndex = 9;
		this.ultraLabel4.Text = " 功能清單";
		this.panel15.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel15.Location = new System.Drawing.Point(270, 6);
		this.panel15.Name = "panel15";
		this.panel15.Size = new System.Drawing.Size(5, 580);
		this.panel15.TabIndex = 11;
		this.panel3.Controls.Add(this.panel9);
		this.panel3.Controls.Add(this.ultraLabel2);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel3.Location = new System.Drawing.Point(0, 6);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(270, 580);
		this.panel3.TabIndex = 9;
		this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel9.Controls.Add(this.panel20);
		this.panel9.Controls.Add(this.groupBox2);
		this.panel9.Controls.Add(this.panel11);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel9.Location = new System.Drawing.Point(0, 30);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(270, 550);
		this.panel9.TabIndex = 7;
		this.panel20.BackColor = System.Drawing.Color.Gray;
		this.panel20.Controls.Add(this.BtnGRP_Del);
		this.panel20.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel20.Location = new System.Drawing.Point(0, 518);
		this.panel20.Name = "panel20";
		this.panel20.Size = new System.Drawing.Size(268, 30);
		this.panel20.TabIndex = 13;
		this.BtnGRP_Del.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance5.FontData.Name = "細明體";
		appearance5.FontData.SizeInPoints = 9f;
		appearance5.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnGRP_Del.Appearance = appearance5;
		this.BtnGRP_Del.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnGRP_Del.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.BtnGRP_Del.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnGRP_Del.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnGRP_Del.Location = new System.Drawing.Point(193, 3);
		this.BtnGRP_Del.Name = "BtnGRP_Del";
		this.BtnGRP_Del.ShowFocusRect = false;
		this.BtnGRP_Del.ShowOutline = false;
		this.BtnGRP_Del.Size = new System.Drawing.Size(70, 24);
		this.BtnGRP_Del.SupportThemes = false;
		this.BtnGRP_Del.TabIndex = 12;
		this.BtnGRP_Del.Text = "刪除群組";
		this.BtnGRP_Del.Click += new System.EventHandler(BtnGRP_Del_Click);
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.panel10);
		this.groupBox2.Location = new System.Drawing.Point(5, -4);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(259, 132);
		this.groupBox2.TabIndex = 11;
		this.groupBox2.TabStop = false;
		this.panel10.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel10.Controls.Add(this.txtGroupID);
		this.panel10.Controls.Add(this.ultraLabel16);
		this.panel10.Controls.Add(this.BtnAddUser);
		this.panel10.Controls.Add(this.txtGroupName);
		this.panel10.Controls.Add(this.ultraLabel5);
		this.panel10.Controls.Add(this.ultraLabel3);
		this.panel10.Location = new System.Drawing.Point(4, 23);
		this.panel10.Name = "panel10";
		this.panel10.Size = new System.Drawing.Size(248, 112);
		this.panel10.TabIndex = 9;
		appearance6.FontData.Name = "細明體";
		appearance6.FontData.SizeInPoints = 11f;
		this.txtGroupID.Appearance = appearance6;
		this.txtGroupID.AutoSize = true;
		this.txtGroupID.Location = new System.Drawing.Point(96, 28);
		this.txtGroupID.MaxLength = 20;
		this.txtGroupID.Name = "txtGroupID";
		this.txtGroupID.Size = new System.Drawing.Size(140, 24);
		this.txtGroupID.TabIndex = 1;
		this.txtGroupID.Validating += new System.ComponentModel.CancelEventHandler(txtGroupID_Validating);
		appearance7.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel16.Appearance = appearance7;
		this.ultraLabel16.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel16.Location = new System.Drawing.Point(20, 29);
		this.ultraLabel16.Name = "ultraLabel16";
		this.ultraLabel16.Size = new System.Drawing.Size(96, 23);
		this.ultraLabel16.TabIndex = 10;
		this.ultraLabel16.Text = "群組帳號";
		this.BtnAddUser.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance8.FontData.Name = "細明體";
		appearance8.FontData.SizeInPoints = 9f;
		appearance8.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnAddUser.Appearance = appearance8;
		this.BtnAddUser.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnAddUser.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.BtnAddUser.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnAddUser.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnAddUser.Location = new System.Drawing.Point(166, 84);
		this.BtnAddUser.Name = "BtnAddUser";
		this.BtnAddUser.ShowFocusRect = false;
		this.BtnAddUser.ShowOutline = false;
		this.BtnAddUser.Size = new System.Drawing.Size(70, 24);
		this.BtnAddUser.SupportThemes = false;
		this.BtnAddUser.TabIndex = 9;
		this.BtnAddUser.Text = "新增";
		this.BtnAddUser.Click += new System.EventHandler(BtnAddUser_Click);
		appearance9.FontData.Name = "細明體";
		appearance9.FontData.SizeInPoints = 11f;
		this.txtGroupName.Appearance = appearance9;
		this.txtGroupName.AutoSize = true;
		this.txtGroupName.Location = new System.Drawing.Point(96, 54);
		this.txtGroupName.MaxLength = 50;
		this.txtGroupName.Name = "txtGroupName";
		this.txtGroupName.Size = new System.Drawing.Size(140, 24);
		this.txtGroupName.TabIndex = 2;
		this.txtGroupName.Validating += new System.ComponentModel.CancelEventHandler(txtGroupID_Validating);
		appearance10.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance10;
		this.ultraLabel5.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel5.Location = new System.Drawing.Point(20, 55);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(96, 23);
		this.ultraLabel5.TabIndex = 3;
		this.ultraLabel5.Text = "群組名稱";
		this.ultraLabel3.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel3.Location = new System.Drawing.Point(4, 2);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(100, 20);
		this.ultraLabel3.TabIndex = 0;
		this.ultraLabel3.Text = "新增群組";
		this.panel11.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel11.Controls.Add(this.GridGroups);
		this.panel11.Location = new System.Drawing.Point(2, 132);
		this.panel11.Name = "panel11";
		this.panel11.Size = new System.Drawing.Size(264, 384);
		this.panel11.TabIndex = 10;
		this.GridGroups._ExcelFileName = "";
		this.GridGroups._ExcelSheeName = "";
		this.GridGroups._IsOpenExcelAfterExport = false;
		this.GridGroups.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.GridGroups.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridGroups.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.GridGroups.ColumnInfo = resources.GetString("GridGroups.ColumnInfo");
		this.ultraToolbarsManager1.SetContextMenuUltra(this.GridGroups, "Popup1");
		this.GridGroups.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridGroups.ExtendLastCol = true;
		this.GridGroups.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridGroups.ForeColor = System.Drawing.Color.Black;
		this.GridGroups.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.GridGroups.IsProcessUndo = false;
		this.GridGroups.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.GridGroups.Location = new System.Drawing.Point(0, 0);
		this.GridGroups.Name = "GridGroups";
		this.GridGroups.Rows.Count = 1;
		this.GridGroups.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.GridGroups.ShowCursor = true;
		this.GridGroups.ShowToolTipOnNarrowColumn = true;
		this.GridGroups.Size = new System.Drawing.Size(264, 384);
		this.GridGroups.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("GridGroups.Styles"));
		this.GridGroups.TabIndex = 8;
		this.GridGroups.UndoMax = 10;
		this.GridGroups.BeforeSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(GridGroups_BeforeSelChange);
		this.GridGroups.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(GridGroups_AfterSelChange);
		this.GridGroups.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(GridGroups_AfterEdit);
		this.GridGroups.KeyDown += new System.Windows.Forms.KeyEventHandler(GridGroups_KeyDown);
		this.GridGroups.MouseDown += new System.Windows.Forms.MouseEventHandler(GridGroups_MouseDown);
		this.GridGroups.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(GridGroups_BeforeEdit);
		appearance42.FontData.Name = "細明體";
		appearance42.ForeColor = System.Drawing.Color.White;
		appearance42.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance42;
		this.ultraLabel2.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel2.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(270, 30);
		this.ultraLabel2.TabIndex = 4;
		this.ultraLabel2.Text = " 群組列表";
		this.panel6.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel6.Location = new System.Drawing.Point(0, 0);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(636, 6);
		this.panel6.TabIndex = 7;
		this.Tab_B.Controls.Add(this.panel18);
		this.Tab_B.Controls.Add(this.panel17);
		this.Tab_B.Controls.Add(this.panel1);
		this.Tab_B.Controls.Add(this.panel7);
		this.Tab_B.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(636, 586);
		this.panel18.Controls.Add(this.ultraTree2);
		this.panel18.Controls.Add(this.BtnSaveUser);
		this.panel18.Controls.Add(this.splitter1);
		this.panel18.Controls.Add(this.panel19);
		this.panel18.Controls.Add(this.ultraLabel14);
		this.panel18.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel18.Location = new System.Drawing.Point(275, 6);
		this.panel18.Name = "panel18";
		this.panel18.Size = new System.Drawing.Size(361, 580);
		this.panel18.TabIndex = 13;
		this.ultraTree2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraTree2.HideSelection = false;
		this.ultraTree2.Location = new System.Drawing.Point(0, 30);
		this.ultraTree2.Name = "ultraTree2";
		ultraTreeNode2.Text = "Pcces Win 4.3 ";
		this.ultraTree2.Nodes.AddRange(new Infragistics.Win.UltraWinTree.UltraTreeNode[1] { ultraTreeNode2 });
		_override2.NodeStyle = Infragistics.Win.UltraWinTree.NodeStyle.CheckBox;
		_override2.SelectionType = Infragistics.Win.UltraWinTree.SelectType.Single;
		this.ultraTree2.Override = _override2;
		this.ultraTree2.Size = new System.Drawing.Size(361, 381);
		this.ultraTree2.SupportThemes = false;
		this.ultraTree2.TabIndex = 8;
		this.ultraTree2.Visible = false;
		this.ultraTree2.AfterExpand += new Infragistics.Win.UltraWinTree.AfterNodeChangedEventHandler(ultraTree2_AfterExpand);
		this.ultraTree2.MouseDown += new System.Windows.Forms.MouseEventHandler(ultraTree2_MouseDown);
		this.ultraTree2.Leave += new System.EventHandler(ultraTree2_Leave);
		this.ultraTree2.AfterCheck += new Infragistics.Win.UltraWinTree.AfterNodeChangedEventHandler(ultraTree2_AfterCheck);
		this.ultraTree2.MouseUp += new System.Windows.Forms.MouseEventHandler(ultraTree2_MouseUp);
		this.ultraTree2.KeyUp += new System.Windows.Forms.KeyEventHandler(ultraTree1_KeyUp);
		this.ultraTree2.KeyDown += new System.Windows.Forms.KeyEventHandler(ultraTree1_KeyDown);
		this.BtnSaveUser.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance43.TextVAlign = Infragistics.Win.VAlign.Top;
		this.BtnSaveUser.Appearance = appearance43;
		this.BtnSaveUser.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.BtnSaveUser.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.BtnSaveUser.Location = new System.Drawing.Point(259, 3);
		this.BtnSaveUser.Name = "BtnSaveUser";
		this.BtnSaveUser.Size = new System.Drawing.Size(96, 23);
		this.BtnSaveUser.SupportThemes = false;
		this.BtnSaveUser.TabIndex = 13;
		this.BtnSaveUser.Text = "儲存功能清單";
		this.BtnSaveUser.Visible = false;
		this.BtnSaveUser.Click += new System.EventHandler(BtnSaveUser_Click);
		this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.splitter1.Location = new System.Drawing.Point(0, 411);
		this.splitter1.Name = "splitter1";
		this.splitter1.Size = new System.Drawing.Size(361, 5);
		this.splitter1.TabIndex = 10;
		this.splitter1.TabStop = false;
		this.panel19.Controls.Add(this.ultraButton2);
		this.panel19.Controls.Add(this.GridUserGroups);
		this.panel19.Controls.Add(this.ultraLabel7);
		this.panel19.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel19.Location = new System.Drawing.Point(0, 416);
		this.panel19.Name = "panel19";
		this.panel19.Size = new System.Drawing.Size(361, 164);
		this.panel19.TabIndex = 11;
		this.ultraButton2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance44.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton2.Appearance = appearance44;
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton2.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton2.Location = new System.Drawing.Point(252, 4);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(100, 23);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 14;
		this.ultraButton2.Text = "使用者隸屬變更";
		this.ultraButton2.Click += new System.EventHandler(ultraButton2_Click);
		this.GridUserGroups._ExcelFileName = "";
		this.GridUserGroups._ExcelSheeName = "";
		this.GridUserGroups._IsOpenExcelAfterExport = false;
		this.GridUserGroups.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.GridUserGroups.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.GridUserGroups.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridUserGroups.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.GridUserGroups.ColumnInfo = resources.GetString("GridUserGroups.ColumnInfo");
		this.GridUserGroups.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridUserGroups.ExtendLastCol = true;
		this.GridUserGroups.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridUserGroups.ForeColor = System.Drawing.Color.Black;
		this.GridUserGroups.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.GridUserGroups.IsProcessUndo = false;
		this.GridUserGroups.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.GridUserGroups.Location = new System.Drawing.Point(0, 30);
		this.GridUserGroups.Name = "GridUserGroups";
		this.GridUserGroups.Rows.Count = 1;
		this.GridUserGroups.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.GridUserGroups.ShowCursor = true;
		this.GridUserGroups.ShowToolTipOnNarrowColumn = true;
		this.GridUserGroups.Size = new System.Drawing.Size(361, 134);
		this.GridUserGroups.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("GridUserGroups.Styles"));
		this.GridUserGroups.TabIndex = 11;
		this.GridUserGroups.UndoMax = 10;
		appearance45.FontData.Name = "細明體";
		appearance45.ForeColor = System.Drawing.Color.White;
		appearance45.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel7.Appearance = appearance45;
		this.ultraLabel7.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel7.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel7.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(361, 30);
		this.ultraLabel7.TabIndex = 10;
		this.ultraLabel7.Text = " 使用者隸屬於";
		appearance46.FontData.Name = "細明體";
		appearance46.ForeColor = System.Drawing.Color.White;
		appearance46.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel14.Appearance = appearance46;
		this.ultraLabel14.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel14.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel14.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(361, 30);
		this.ultraLabel14.TabIndex = 9;
		this.ultraLabel14.Text = " 功能清單";
		this.panel17.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel17.Location = new System.Drawing.Point(270, 6);
		this.panel17.Name = "panel17";
		this.panel17.Size = new System.Drawing.Size(5, 580);
		this.panel17.TabIndex = 12;
		this.panel1.Controls.Add(this.panel12);
		this.panel1.Controls.Add(this.ultraLabel13);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel1.Location = new System.Drawing.Point(0, 6);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(270, 580);
		this.panel1.TabIndex = 10;
		this.panel12.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel12.Controls.Add(this.panel5);
		this.panel12.Controls.Add(this.groupBox1);
		this.panel12.Controls.Add(this.panel13);
		this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel12.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel12.Location = new System.Drawing.Point(0, 30);
		this.panel12.Name = "panel12";
		this.panel12.Size = new System.Drawing.Size(270, 550);
		this.panel12.TabIndex = 7;
		this.panel5.BackColor = System.Drawing.Color.Gray;
		this.panel5.Controls.Add(this.BtnUser_Edt);
		this.panel5.Controls.Add(this.BtnUser_Del);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel5.Location = new System.Drawing.Point(0, 516);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(268, 32);
		this.panel5.TabIndex = 12;
		this.BtnUser_Edt.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance47.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnUser_Edt.Appearance = appearance47;
		this.BtnUser_Edt.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnUser_Edt.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.BtnUser_Edt.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnUser_Edt.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnUser_Edt.Location = new System.Drawing.Point(120, 5);
		this.BtnUser_Edt.Name = "BtnUser_Edt";
		this.BtnUser_Edt.ShowFocusRect = false;
		this.BtnUser_Edt.ShowOutline = false;
		this.BtnUser_Edt.Size = new System.Drawing.Size(70, 24);
		this.BtnUser_Edt.SupportThemes = false;
		this.BtnUser_Edt.TabIndex = 14;
		this.BtnUser_Edt.Text = "編輯帳號";
		this.BtnUser_Edt.Click += new System.EventHandler(BtnUser_Edt_Click);
		this.BtnUser_Del.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance48.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnUser_Del.Appearance = appearance48;
		this.BtnUser_Del.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnUser_Del.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.BtnUser_Del.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnUser_Del.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnUser_Del.Location = new System.Drawing.Point(193, 5);
		this.BtnUser_Del.Name = "BtnUser_Del";
		this.BtnUser_Del.ShowFocusRect = false;
		this.BtnUser_Del.ShowOutline = false;
		this.BtnUser_Del.Size = new System.Drawing.Size(70, 24);
		this.BtnUser_Del.SupportThemes = false;
		this.BtnUser_Del.TabIndex = 13;
		this.BtnUser_Del.Text = "刪除帳號";
		this.BtnUser_Del.Click += new System.EventHandler(BtnUser_Del_Click);
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.panel14);
		this.groupBox1.Location = new System.Drawing.Point(5, -4);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(259, 206);
		this.groupBox1.TabIndex = 11;
		this.groupBox1.TabStop = false;
		this.panel14.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel14.Controls.Add(this.BtnUser_Add);
		this.panel14.Controls.Add(this.Cbo1);
		this.panel14.Controls.Add(this.ultraLabel15);
		this.panel14.Controls.Add(this.txtPwdConfirm);
		this.panel14.Controls.Add(this.ultraLabel8);
		this.panel14.Controls.Add(this.txtPwd);
		this.panel14.Controls.Add(this.ultraLabel9);
		this.panel14.Controls.Add(this.txtUserName);
		this.panel14.Controls.Add(this.ultraLabel10);
		this.panel14.Controls.Add(this.txtUserID);
		this.panel14.Controls.Add(this.ultraLabel11);
		this.panel14.Controls.Add(this.ultraLabel12);
		this.panel14.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel14.Location = new System.Drawing.Point(1, 10);
		this.panel14.Name = "panel14";
		this.panel14.Size = new System.Drawing.Size(251, 192);
		this.panel14.TabIndex = 9;
		this.BtnUser_Add.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance49.FontData.Name = "細明體";
		appearance49.FontData.SizeInPoints = 9f;
		appearance49.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnUser_Add.Appearance = appearance49;
		this.BtnUser_Add.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnUser_Add.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.BtnUser_Add.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnUser_Add.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnUser_Add.Location = new System.Drawing.Point(174, 163);
		this.BtnUser_Add.Name = "BtnUser_Add";
		this.BtnUser_Add.ShowFocusRect = false;
		this.BtnUser_Add.ShowOutline = false;
		this.BtnUser_Add.Size = new System.Drawing.Size(70, 24);
		this.BtnUser_Add.SupportThemes = false;
		this.BtnUser_Add.TabIndex = 12;
		this.BtnUser_Add.Text = "新增";
		this.BtnUser_Add.Click += new System.EventHandler(BtnUser_Add_Click);
		appearance50.FontData.Name = "細明體";
		appearance50.FontData.SizeInPoints = 11f;
		this.Cbo1.Appearance = appearance50;
		this.Cbo1.AutoSize = true;
		this.Cbo1.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		valueListItem1.DataValue = "1";
		valueListItem1.DisplayText = "系統管理員";
		valueListItem2.DataValue = "2";
		valueListItem2.DisplayText = "一般使用者";
		this.Cbo1.Items.Add(valueListItem1);
		this.Cbo1.Items.Add(valueListItem2);
		this.Cbo1.Location = new System.Drawing.Point(104, 132);
		this.Cbo1.Name = "Cbo1";
		this.Cbo1.Size = new System.Drawing.Size(140, 24);
		this.Cbo1.TabIndex = 6;
		this.Cbo1.Text = null;
		appearance51.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel15.Appearance = appearance51;
		this.ultraLabel15.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel15.Location = new System.Drawing.Point(17, 133);
		this.ultraLabel15.Name = "ultraLabel15";
		this.ultraLabel15.Size = new System.Drawing.Size(77, 23);
		this.ultraLabel15.TabIndex = 10;
		this.ultraLabel15.Text = "身份別";
		appearance52.FontData.Name = "細明體";
		appearance52.FontData.SizeInPoints = 11f;
		this.txtPwdConfirm.Appearance = appearance52;
		this.txtPwdConfirm.AutoSize = true;
		this.txtPwdConfirm.Location = new System.Drawing.Point(104, 106);
		this.txtPwdConfirm.MaxLength = 20;
		this.txtPwdConfirm.Name = "txtPwdConfirm";
		this.txtPwdConfirm.PasswordChar = '*';
		this.txtPwdConfirm.Size = new System.Drawing.Size(140, 24);
		this.txtPwdConfirm.TabIndex = 5;
		this.txtPwdConfirm.Validating += new System.ComponentModel.CancelEventHandler(txtGroupID_Validating);
		appearance53.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel8.Appearance = appearance53;
		this.ultraLabel8.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel8.Location = new System.Drawing.Point(17, 107);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(96, 23);
		this.ultraLabel8.TabIndex = 7;
		this.ultraLabel8.Text = "確認密碼";
		appearance54.FontData.Name = "細明體";
		appearance54.FontData.SizeInPoints = 11f;
		this.txtPwd.Appearance = appearance54;
		this.txtPwd.AutoSize = true;
		this.txtPwd.Location = new System.Drawing.Point(104, 80);
		this.txtPwd.MaxLength = 20;
		this.txtPwd.Name = "txtPwd";
		this.txtPwd.PasswordChar = '*';
		this.txtPwd.Size = new System.Drawing.Size(140, 24);
		this.txtPwd.TabIndex = 4;
		this.txtPwd.Validating += new System.ComponentModel.CancelEventHandler(txtGroupID_Validating);
		appearance55.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel9.Appearance = appearance55;
		this.ultraLabel9.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel9.Location = new System.Drawing.Point(17, 81);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(96, 23);
		this.ultraLabel9.TabIndex = 5;
		this.ultraLabel9.Text = "使用者密碼";
		appearance56.FontData.Name = "細明體";
		appearance56.FontData.SizeInPoints = 11f;
		this.txtUserName.Appearance = appearance56;
		this.txtUserName.AutoSize = true;
		this.txtUserName.Location = new System.Drawing.Point(104, 54);
		this.txtUserName.MaxLength = 10;
		this.txtUserName.Name = "txtUserName";
		this.txtUserName.Size = new System.Drawing.Size(140, 24);
		this.txtUserName.TabIndex = 3;
		this.txtUserName.Validating += new System.ComponentModel.CancelEventHandler(txtGroupID_Validating);
		appearance57.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel10.Appearance = appearance57;
		this.ultraLabel10.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel10.Location = new System.Drawing.Point(17, 55);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(96, 23);
		this.ultraLabel10.TabIndex = 3;
		this.ultraLabel10.Text = "使用者名稱";
		appearance58.FontData.Name = "細明體";
		appearance58.FontData.SizeInPoints = 11f;
		this.txtUserID.Appearance = appearance58;
		this.txtUserID.AutoSize = true;
		this.txtUserID.Location = new System.Drawing.Point(104, 28);
		this.txtUserID.MaxLength = 10;
		this.txtUserID.Name = "txtUserID";
		this.txtUserID.Size = new System.Drawing.Size(140, 24);
		this.txtUserID.TabIndex = 2;
		this.txtUserID.Validating += new System.ComponentModel.CancelEventHandler(txtGroupID_Validating);
		appearance59.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel11.Appearance = appearance59;
		this.ultraLabel11.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel11.Location = new System.Drawing.Point(17, 29);
		this.ultraLabel11.Name = "ultraLabel11";
		this.ultraLabel11.Size = new System.Drawing.Size(96, 23);
		this.ultraLabel11.TabIndex = 1;
		this.ultraLabel11.Text = "使用者帳號";
		this.ultraLabel12.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel12.Location = new System.Drawing.Point(2, 4);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(100, 20);
		this.ultraLabel12.TabIndex = 0;
		this.ultraLabel12.Text = "新增使用者";
		this.panel13.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel13.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel13.Controls.Add(this.GridUsers);
		this.panel13.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel13.Location = new System.Drawing.Point(2, 205);
		this.panel13.Name = "panel13";
		this.panel13.Size = new System.Drawing.Size(264, 308);
		this.panel13.TabIndex = 10;
		this.GridUsers._ExcelFileName = "";
		this.GridUsers._ExcelSheeName = "";
		this.GridUsers._IsOpenExcelAfterExport = false;
		this.GridUsers.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.GridUsers.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridUsers.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.GridUsers.ColumnInfo = resources.GetString("GridUsers.ColumnInfo");
		this.ultraToolbarsManager1.SetContextMenuUltra(this.GridUsers, "Popup2");
		this.GridUsers.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridUsers.ExtendLastCol = true;
		this.GridUsers.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridUsers.ForeColor = System.Drawing.Color.Black;
		this.GridUsers.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.GridUsers.IsProcessUndo = false;
		this.GridUsers.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.GridUsers.Location = new System.Drawing.Point(0, 0);
		this.GridUsers.Name = "GridUsers";
		this.GridUsers.Rows.Count = 1;
		this.GridUsers.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.GridUsers.ShowCursor = true;
		this.GridUsers.ShowToolTipOnNarrowColumn = true;
		this.GridUsers.Size = new System.Drawing.Size(264, 308);
		this.GridUsers.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("GridUsers.Styles"));
		this.GridUsers.TabIndex = 8;
		this.GridUsers.UndoMax = 10;
		this.GridUsers.BeforeSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(GridUsers_BeforeSelChange);
		this.GridUsers.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(GridUsers_AfterSelChange);
		this.GridUsers.KeyDown += new System.Windows.Forms.KeyEventHandler(GridUsers_KeyDown);
		this.GridUsers.MouseDown += new System.Windows.Forms.MouseEventHandler(GridUsers_MouseDown);
		this.GridUsers.MouseUp += new System.Windows.Forms.MouseEventHandler(GridUsers_MouseUp);
		appearance60.FontData.Name = "細明體";
		appearance60.ForeColor = System.Drawing.Color.White;
		appearance60.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel13.Appearance = appearance60;
		this.ultraLabel13.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel13.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel13.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(270, 30);
		this.ultraLabel13.TabIndex = 4;
		this.ultraLabel13.Text = " 使用者帳號列表";
		this.panel7.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel7.Location = new System.Drawing.Point(0, 0);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(636, 6);
		this.panel7.TabIndex = 7;
		this.panel2.Controls.Add(this.panel4);
		this.panel2.Controls.Add(this.ultraStatusBar1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(640, 640);
		this.panel2.TabIndex = 2;
		this.panel4.Controls.Add(this.Tab_Ctrl);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel4.Location = new System.Drawing.Point(0, 0);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(640, 617);
		this.panel4.TabIndex = 1;
		appearance61.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance61.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance61.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance61.BorderColor = System.Drawing.Color.White;
		appearance61.TextVAlign = Infragistics.Win.VAlign.Top;
		this.Tab_Ctrl.ActiveTabAppearance = appearance61;
		appearance62.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance62.BackColor2 = System.Drawing.Color.FromArgb(102, 153, 255);
		appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance62.TextVAlign = Infragistics.Win.VAlign.Top;
		this.Tab_Ctrl.Appearance = appearance62;
		appearance63.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance63.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.Tab_Ctrl.ClientAreaAppearance = appearance63;
		this.Tab_Ctrl.Controls.Add(this.Tab_A);
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_B);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(640, 617);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
		this.Tab_Ctrl.TabIndex = 5;
		this.Tab_Ctrl.TabPadding = new System.Drawing.Size(1, 3);
		ultraTab1.Key = "Tab_A";
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "群組權限";
		ultraTab2.Key = "Tab_B";
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "使用者權限";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2] { ultraTab1, ultraTab2 });
		this.Tab_Ctrl.SelectedTabChanged += new Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventHandler(Tab_Ctrl_SelectedTabChanged);
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(636, 586);
		appearance64.BackColor = System.Drawing.SystemColors.Control;
		appearance64.FontData.SizeInPoints = 11f;
		this.ultraStatusBar1.Appearance = appearance64;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 617);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Text = "資料筆數:";
		ultraStatusPanel1.Width = 200;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		appearance65.TextHAlign = Infragistics.Win.HAlign.Right;
		ultraStatusPanel3.Appearance = appearance65;
		ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel3.Text = "客服電話:(02)2708-8090";
		ultraStatusPanel3.Width = 200;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[3] { ultraStatusPanel1, ultraStatusPanel2, ultraStatusPanel3 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(640, 23);
		this.ultraStatusBar1.SupportThemes = false;
		this.ultraStatusBar1.TabIndex = 19;
		this.ultraStatusBar1.Text = "ultraStatusBar1";
		appearance66.FontData.Name = "Arial";
		appearance66.FontData.SizeInPoints = 9f;
		this.ultraToolbarsManager1.Appearance = appearance66;
		appearance67.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance67.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.DockAreaAppearance = appearance67;
		this.ultraToolbarsManager1.DockWithinContainer = this;
		this.ultraToolbarsManager1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraToolbarsManager1.LockToolbars = true;
		appearance68.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance68.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance68.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.MenuSettings.HotTrackAppearance = appearance68;
		appearance69.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance69.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsManager1.MenuSettings.IconAreaAppearance = appearance69;
		appearance70.BackColor = System.Drawing.Color.White;
		appearance70.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.MenuSettings.ToolAppearance = appearance70;
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
		ultraToolbar1.Settings.AllowDockTop = Infragistics.Win.DefaultableBoolean.True;
		ultraToolbar1.Text = "Tool1";
		labelTool1.InstanceProps.IsFirstInGroup = true;
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { buttonTool1, labelTool1, comboBoxTool1, buttonTool2 });
		ultraToolbar1.Visible = false;
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[1] { ultraToolbar1 });
		appearance71.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance71.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance71;
		appearance72.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance72.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance72.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance72;
		buttonTool3.SharedProps.Caption = "刪除群組";
		buttonTool3.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		labelTool2.SharedProps.Caption = "尋找:";
		labelTool2.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool2.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool2.SharedProps.Caption = "輸入關鍵字";
		comboBoxTool2.SharedProps.Width = 200;
		comboBoxTool2.ValueList = valueList1;
		appearance73.Image = resources.GetObject("appearance41.Image");
		buttonTool4.SharedProps.AppearancesSmall.Appearance = appearance73;
		buttonTool4.SharedProps.Caption = "Go";
		popupMenuTool1.SharedProps.Caption = "右鍵功能表1";
		popupMenuTool1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[1] { buttonTool5 });
		popupMenuTool2.SharedProps.Caption = "右鍵功能表2";
		buttonTool7.InstanceProps.IsFirstInGroup = true;
		popupMenuTool2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool6, buttonTool7 });
		buttonTool8.SharedProps.Caption = "刪除帳號";
		buttonTool8.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool9.SharedProps.Caption = "編輯帳號";
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[8] { buttonTool3, labelTool2, comboBoxTool2, buttonTool4, popupMenuTool1, popupMenuTool2, buttonTool8, buttonTool9 });
		this.ultraToolbarsManager1.BeforeToolbarListDropdown += new Infragistics.Win.UltraWinToolbars.BeforeToolbarListDropdownEventHandler(ultraToolbarsManager1_BeforeToolbarListDropdown);
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this._FormSys_B_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormSys_B_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormSys_B_Toolbars_Dock_Area_Top.Name = "_FormSys_B_Toolbars_Dock_Area_Top";
		this._FormSys_B_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(640, 0);
		this._FormSys_B_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 640);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Name = "_FormSys_B_Toolbars_Dock_Area_Bottom";
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(640, 0);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormSys_B_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 0);
		this._FormSys_B_Toolbars_Dock_Area_Left.Name = "_FormSys_B_Toolbars_Dock_Area_Left";
		this._FormSys_B_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 640);
		this._FormSys_B_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormSys_B_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(640, 0);
		this._FormSys_B_Toolbars_Dock_Area_Right.Name = "_FormSys_B_Toolbars_Dock_Area_Right";
		this._FormSys_B_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 640);
		this._FormSys_B_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Magenta;
		this.imageList2.Images.SetKeyName(0, "");
		this.imageList2.Images.SetKeyName(1, "");
		this.imageList2.Images.SetKeyName(2, "");
		this.imageList2.Images.SetKeyName(3, "");
		this.imageList2.Images.SetKeyName(4, "");
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Bottom);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Name = "FormSys_A";
		base.Size = new System.Drawing.Size(640, 640);
		base.Load += new System.EventHandler(FormSys_A_Load);
		this.Tab_A.ResumeLayout(false);
		this.panel8.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraTree1).EndInit();
		this.panel16.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridGroupUsers).EndInit();
		this.panel3.ResumeLayout(false);
		this.panel9.ResumeLayout(false);
		this.panel20.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.panel10.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtGroupID).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGroupName).EndInit();
		this.panel11.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridGroups).EndInit();
		this.Tab_B.ResumeLayout(false);
		this.panel18.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraTree2).EndInit();
		this.panel19.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridUserGroups).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel12.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		this.panel14.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Cbo1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPwdConfirm).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPwd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserID).EndInit();
		this.panel13.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridUsers).EndInit();
		this.panel2.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).EndInit();
		this.Tab_Ctrl.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
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

	public FormSys_A()
	{
		InitializeComponent();
		CellStyle cs1 = GridGroups.Styles.Add("EditMode");
		cs1.DataType = typeof(Image);
		cs1.ImageAlign = ImageAlignEnum.RightCenter;
		CellStyle cs2 = GridGroupUsers.Styles.Add("EditMode");
		cs2.DataType = typeof(Image);
		cs2.ImageAlign = ImageAlignEnum.RightCenter;
		CellStyle cs3 = GridUserGroups.Styles.Add("EditMode");
		cs3.DataType = typeof(Image);
		cs3.ImageAlign = ImageAlignEnum.RightCenter;
		CellStyle cs4 = GridUsers.Styles.Add("EditMode");
		cs4.DataType = typeof(Image);
		cs4.ImageAlign = ImageAlignEnum.RightCenter;
	}

	private void BindFuncTree()
	{
		FORM_STATUS = "BIND_TREE";
		Cursor = Cursors.WaitCursor;
		Get_NodesData();
		Get_LeavesData();
		ultraTree1.Nodes.Clear();
		ultraTree2.Nodes.Clear();
		UltraTreeNode node1 = ultraTree1.Nodes.Add("ROOT", "Pcces Win 4.3  功能清單");
		UltraTreeNode node2 = ultraTree2.Nodes.Add("ROOT", "Pcces Win 4.3  功能清單");
		PopulateLevel1(node1);
		PopulateLevel1(node2);
		ultraTree1.Nodes[0].Expanded = true;
		ultraTree2.Nodes[0].Expanded = true;
		Cursor = Cursors.Default;
		FORM_STATUS = "NORMAL";
	}

	private void Get_NodesData()
	{
		DBClass DBClass1 = new DBClass();
		DT_Nodes = DBClass1.GetFuncParent();
	}

	private void Get_LeavesData()
	{
		DBClass DBClass1 = new DBClass();
		DT_Leaves = DBClass1.GetFuncChild();
	}

	private void PopulateLevel1(UltraTreeNode treeNode)
	{
		treeNode.Nodes.Clear();
		UltraTreeNode node = null;
		foreach (DataRow row in DT_Nodes.Rows)
		{
			string itemCode = row["FuncID"] as string;
			string cName = row["FuncName"] as string;
			node = treeNode.Nodes.Add(itemCode, cName.Trim());
			node.Override.NodeAppearance.ForeColor = Color.Red;
			PopulateLevel2(node);
		}
	}

	private void PopulateLevel2(UltraTreeNode treeNode)
	{
		treeNode.Nodes.Clear();
		string filterExp = " FuncID Like '" + treeNode.Key + "%' And Len(FuncID) =" + (treeNode.Key.Length + 4);
		string sortExp = " FuncID ASC ";
		DataRow[] rows = DT_Leaves.Select(filterExp, sortExp);
		UltraTreeNode node = null;
		DataRow[] array = rows;
		foreach (DataRow row in array)
		{
			node = treeNode.Nodes.Add(row["FuncID"] as string, row["FuncName"] as string);
			switch (row["Remark"].ToString())
			{
			case "Grid":
				node.Override.NodeAppearance.ForeColor = Color.FromArgb(66, 153, 160);
				break;
			case "SubGrid":
				node.Override.NodeAppearance.ForeColor = Color.FromArgb(51, 153, 102);
				break;
			case "Toolbar":
				node.Override.NodeAppearance.ForeColor = Color.FromArgb(0, 0, 255);
				break;
			case "SubToolbar":
				node.Override.NodeAppearance.ForeColor = Color.FromArgb(51, 102, 255);
				break;
			case "Function":
				node.Override.NodeAppearance.ForeColor = Color.OrangeRed;
				break;
			case "SubFunction":
				node.Override.NodeAppearance.ForeColor = Color.Orange;
				break;
			}
			node.Tag = new ExtendedNodeInfo(typeof(string), "FuncID");
			PopulateLevel2(node);
		}
	}

	private void ultraTree1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control)
		{
			IsCtrl = true;
		}
	}

	private void ultraTree1_KeyUp(object sender, KeyEventArgs e)
	{
		IsCtrl = false;
	}

	private void ultraTree1_AfterExpand(object sender, NodeEventArgs e)
	{
		if (!IsCtrl && IsMouseClick)
		{
			e.TreeNode.ExpandAll();
		}
		else
		{
			e.TreeNode.Expanded = true;
		}
	}

	private void ClearNodesCheck(ref UltraTree ATree)
	{
		for (int i = 0; i < ATree.Nodes[0].Nodes.Count; i++)
		{
			ClearNodesCheck1(ATree.Nodes[0].Nodes[i]);
		}
	}

	private void ClearNodesCheck1(UltraTreeNode treeNode)
	{
		treeNode.CheckedState = CheckState.Unchecked;
		if (treeNode.HasNodes)
		{
			for (int i = 0; i < treeNode.Nodes.Count; i++)
			{
				ClearNodesCheck1(treeNode.Nodes[i]);
			}
		}
	}

	private void BindToGroups()
	{
		FORM_STATUS = "BINDING";
		DT_Groups = DBCLS.GetGroupList();
		GridGroups.Rows.Count = DT_Groups.Rows.Count + 1;
		ultraStatusBar1.Panels[0].Text = "群組數：" + DT_Groups.Rows.Count;
		for (int i = 0; i < DT_Groups.Rows.Count; i++)
		{
			GridGroups[i + 1, "GroupID"] = DT_Groups.Rows[i]["GroupID"].ToString().Trim();
			GridGroups[i + 1, "GroupName"] = DT_Groups.Rows[i]["GroupName"].ToString().Trim();
		}
		SetColsEditSymbol(ref GridGroups);
		GridGroups.AutoSizeCols();
		FORM_STATUS = "NORMAL";
	}

	private void SetColsEditSymbol(ref GridMrsBase C1FlexGrid)
	{
		for (int i = 1; i < C1FlexGrid.Cols.Count; i++)
		{
			if (C1FlexGrid.Cols[i].AllowEditing)
			{
				CellRange rg = C1FlexGrid.GetCellRange(0, i);
				rg.Style = C1FlexGrid.Styles["EditMode"];
				rg.Image = imageList2.Images[2];
			}
		}
	}

	private void BtnAddUser_Click(object sender, EventArgs e)
	{
		if (F_UserID != "PccesUser" && !DBClass.ChkAuthority(F_UserID, "F001000600010001"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F001000600010001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (txtGroupID.Text.Trim() == "")
		{
			MessageBox.Show(this, "群組帳號不可空白", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtGroupID.Focus();
			return;
		}
		if (txtGroupName.Text.Trim() == "")
		{
			MessageBox.Show(this, "群組名稱不可空白", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtGroupName.Focus();
			return;
		}
		for (int i = 0; i < txtGroupID.Text.Length; i++)
		{
			if (txtGroupID.Text[i] > '\u007f')
			{
				MessageBox.Show(this, "群組帳號不可以是中文或特殊字元", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtGroupID.Focus();
				return;
			}
		}
		int iResult = DBCLS.InsertGroups(txtGroupID.Text.Trim(), txtGroupName.Text.Trim());
		if (iResult == -1)
		{
			MessageBox.Show(this, "相同群組帳號資料已存在。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtGroupID.Focus();
			return;
		}
		BindToGroups();
		for (int i = 1; i < GridGroups.Rows.Count; i++)
		{
			if (GridGroups[i, "GroupID"].ToString().Trim() == txtGroupID.Text.Trim())
			{
				GridGroups.Select(i, 0);
				GridGroups_AfterSelChange(null, null);
				break;
			}
		}
	}

	private void BtnGRP_Del_Click(object sender, EventArgs e)
	{
		if (F_UserID != "PccesUser" && !DBClass.ChkAuthority(F_UserID, "F001000600010002"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F001000600010002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else if (GridGroups.Row > 0)
		{
			if (MessageBox.Show(this, "確定要刪除選取的群組嗎?\n相關的設定都會一併刪除", "警示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				DBCLS.DeleteGroup(GridGroups[GridGroups.Row, "GroupID"].ToString().Trim());
				BindToGroups();
			}
			GridGroups.Row = 0;
			ultraTree1.Visible = false;
		}
	}

	private void GridGroups_AfterEdit(object sender, RowColEventArgs e)
	{
		DBCLS.SaveGroups(GridGroups[e.Row, "GroupID"].ToString().Trim(), GridGroups[e.Row, "GroupName"].ToString().Trim());
	}

	private void GridGroups_AfterSelChange(object sender, RangeEventArgs e)
	{
		if (!(FORM_STATUS != "NORMAL") && GridGroups.Row > 0)
		{
			int colIndex = GridGroups.MouseCol;
			if (!GridGroups.Cols[colIndex].AllowEditing)
			{
				e.Cancel = true;
				GridGroups.Col = 0;
			}
			if (GridGroups[GridGroups.Row, "GroupID"] != null)
			{
				ultraTree1.Visible = true;
				ProcessGroupAuthority(GridGroups[GridGroups.Row, "GroupID"].ToString().Trim());
			}
			DT_GroupUsers = DBCLS.GetGroupUsers(GridGroups[GridGroups.Row, "GroupID"].ToString().Trim());
			BindToGroupUsers();
		}
	}

	private void ProcessGroupAuthority(string sGroupID)
	{
		FORM_STATUS = "BINDING";
		try
		{
			ClearNodesCheck(ref ultraTree1);
			DT_GroupFuncs = DBCLS.GetGroupFuncs(sGroupID);
			for (int i = 0; i < DT_GroupFuncs.Rows.Count; i++)
			{
				string sKey = DT_GroupFuncs.Rows[i]["FuncID"].ToString().Trim();
				ultraTree1.GetNodeByKey(sKey).CheckedState = CheckState.Checked;
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "SysMaintain.FormSys_A.cs.cs" + ex.Message);
			MessageBox.Show(ex.Message);
		}
		FORM_STATUS = "NORMAL";
	}

	private void BindToGroupUsers()
	{
		FORM_STATUS = "BIND_GROUP_USER";
		int iRowsCount = 1;
		for (int i = 0; i < DT_GroupUsers.Rows.Count; i++)
		{
			if (DT_GroupUsers.Rows[i]["GroupID"].ToString().Trim() != "")
			{
				iRowsCount++;
			}
		}
		GridGroupUsers.Rows.Count = iRowsCount;
		if (iRowsCount == 1)
		{
			return;
		}
		int iCount = 1;
		for (int i = 0; i < DT_GroupUsers.Rows.Count; i++)
		{
			if (DT_GroupUsers.Rows[i]["GroupID"].ToString().Trim() != "")
			{
				GridGroupUsers[iCount, "UserID"] = DT_GroupUsers.Rows[i]["UserID"].ToString().Trim();
				GridGroupUsers[iCount, "UserName"] = DT_GroupUsers.Rows[i]["UserName"].ToString().Trim();
				iCount++;
			}
		}
		SetColsEditSymbol(ref GridGroupUsers);
		GridGroupUsers.AutoSizeCols();
		FORM_STATUS = "NORMAL";
	}

	private void BtnSaveGroup_Click(object sender, EventArgs e)
	{
		if (GridGroups.Row <= 0)
		{
			if (FORM_STATUS != "BEFORE_GROUP")
			{
				MessageBox.Show(this, "請先挑選一個群組。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return;
		}
		try
		{
			DT_GRPChk.Clear();
			for (int i = 0; i < ultraTree1.Nodes[0].Nodes.Count; i++)
			{
				GetChecked(ultraTree1.Nodes[0].Nodes[i]);
			}
			DBCLS.SaveGroupFuncs(GridGroups[GridGroups.Row, "GroupID"].ToString(), DT_GRPChk);
			if (FORM_STATUS != "BEFORE_GROUP")
			{
				MessageBox.Show(this, "儲存完畢!", "存檔", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch
		{
			if (FORM_STATUS != "BEFORE_GROUP")
			{
				MessageBox.Show(this, "儲存失敗! \n請確認後重試", "存檔", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		if (FORM_STATUS == "BEFORE_GROUP")
		{
			FORM_STATUS = "NORMAL";
		}
	}

	private void GetChecked(UltraTreeNode treeNode)
	{
		if (treeNode.CheckedState != CheckState.Checked)
		{
			return;
		}
		DataRow DR = DT_GRPChk.NewRow();
		DR["FuncID"] = treeNode.Key;
		DT_GRPChk.Rows.Add(DR);
		if (treeNode.HasNodes)
		{
			for (int i = 0; i < treeNode.Nodes.Count; i++)
			{
				GetChecked(treeNode.Nodes[i]);
			}
		}
	}

	private void BindToUsers()
	{
		FORM_STATUS = "BIND_USER";
		DT_Users = DBCLS.GetUserList();
		GridUsers.Rows.Count = DT_Users.Rows.Count + 1;
		ultraStatusBar1.Panels[0].Text = "使用者數：" + DT_Users.Rows.Count;
		int iAdminCount = 0;
		for (int i = 0; i < DT_Users.Rows.Count; i++)
		{
			GridUsers[i + 1, "UserID"] = DT_Users.Rows[i]["UserID"].ToString().Trim();
			GridUsers[i + 1, "UserName"] = DT_Users.Rows[i]["UserName"].ToString().Trim();
			GridUsers[i + 1, "Power"] = DT_Users.Rows[i]["Power"].ToString().Trim() + "." + ((DT_Users.Rows[i]["Power"].ToString().Trim() == "1") ? "系統管理員" : "一般使用者");
			GridUsers[i + 1, "Password"] = DT_Users.Rows[i]["Pwd"].ToString().Trim();
			if (DT_Users.Rows[i]["Power"].ToString().Trim() == "1")
			{
				iAdminCount++;
			}
		}
		SetColsEditSymbol(ref GridUsers);
		GridUsers.AutoSizeCols();
		FORM_STATUS = "NORMAL";
		if (iAdminCount <= 0)
		{
			MessageBox.Show(this, "建議，至少應有一個系統管理員帳號存在。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void GetChecked_User(UltraTreeNode treeNode)
	{
		if (treeNode.CheckedState != CheckState.Checked)
		{
			return;
		}
		DataRow DR = DT_UsrChk.NewRow();
		DR["FuncID"] = treeNode.Key;
		DT_UsrChk.Rows.Add(DR);
		if (treeNode.HasNodes)
		{
			for (int i = 0; i < treeNode.Nodes.Count; i++)
			{
				GetChecked_User(treeNode.Nodes[i]);
			}
		}
	}

	private void FormSys_A_Load(object sender, EventArgs e)
	{
		DT_GRPChk.Columns.Add("FuncID", Type.GetType("System.String"));
		DT_UsrChk.Columns.Add("FuncID", Type.GetType("System.String"));
		BindFuncTree();
		FORM_STATUS = "NORMAL";
		GridGroups.Row = 0;
		GridUsers.Row = 0;
	}

	private void ultraTree1_AfterCollapse(object sender, NodeEventArgs e)
	{
		e.TreeNode.CollapseAll();
	}

	private void Tab_Ctrl_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
	{
		switch (Tab_Ctrl.ActiveTab.Key)
		{
		case "Tab_A":
			BindToGroups();
			break;
		case "Tab_B":
			BindToUsers();
			GridUsers.Row = 0;
			Cbo1.SelectedIndex = 1;
			break;
		}
	}

	private void GridUsers_AfterSelChange(object sender, RangeEventArgs e)
	{
		if (!(FORM_STATUS != "NORMAL") && GridUsers.Row > 0)
		{
			if (!GridUsers.Cols[GridUsers.MouseCol].AllowEditing)
			{
				e.Cancel = true;
				GridUsers.Col = 0;
			}
			if (GridUsers[GridUsers.Row, "UserID"] != null)
			{
				ultraTree2.Visible = true;
				ProcessUserAuthority(GridUsers[GridUsers.Row, "UserID"].ToString().Trim());
			}
			DT_UserGroups = DBCLS.GetUserGroups(GridUsers[GridUsers.Row, "UserID"].ToString().Trim());
			BindToUserGroups();
		}
	}

	private void ProcessUserAuthority(string sUserID)
	{
		FORM_STATUS = "BINDING";
		ClearNodesCheck(ref ultraTree2);
		DT_UserFuncs = DBCLS.GetUserFuncs(sUserID);
		for (int i = 0; i < DT_UserFuncs.Rows.Count; i++)
		{
			string sKey = DT_UserFuncs.Rows[i]["FuncID"].ToString().Trim();
			if (ultraTree2.GetNodeByKey(sKey) != null)
			{
				ultraTree2.GetNodeByKey(sKey).CheckedState = CheckState.Checked;
			}
		}
		FORM_STATUS = "NORMAL";
	}

	private void BindToUserGroups()
	{
		FORM_STATUS = "BIND_USER_GROUP";
		int iRowsCount = 1;
		for (int i = 0; i < DT_UserGroups.Rows.Count; i++)
		{
			if (DT_UserGroups.Rows[i]["GRP"].ToString().Trim() != "")
			{
				iRowsCount++;
			}
		}
		GridUserGroups.Rows.Count = iRowsCount;
		if (iRowsCount == 1)
		{
			return;
		}
		int iCount = 1;
		for (int i = 0; i < DT_UserGroups.Rows.Count; i++)
		{
			if (DT_UserGroups.Rows[i]["GRP"].ToString().Trim() != "")
			{
				GridUserGroups[iCount, "GroupID"] = DT_UserGroups.Rows[i]["GroupID"].ToString().Trim();
				GridUserGroups[iCount, "GroupName"] = DT_UserGroups.Rows[i]["GroupName"].ToString().Trim();
				iCount++;
			}
		}
		SetColsEditSymbol(ref GridUserGroups);
		GridGroupUsers.AutoSizeCols();
		FORM_STATUS = "NORMAL";
	}

	private void ultraTree2_AfterExpand(object sender, NodeEventArgs e)
	{
		if (!IsCtrl && IsMouseClick)
		{
			e.TreeNode.ExpandAll();
		}
		else
		{
			e.TreeNode.Expanded = true;
		}
	}

	private void ultraTree2_AfterCheck(object sender, NodeEventArgs e)
	{
		if (!(FORM_STATUS == "NORMAL"))
		{
			return;
		}
		if (!IsCtrl && IsMouseClick)
		{
			for (int i = 0; i < e.TreeNode.Nodes.Count; i++)
			{
				e.TreeNode.Nodes[i].CheckedState = e.TreeNode.CheckedState;
			}
		}
		if (e.TreeNode.CheckedState == CheckState.Checked && e.TreeNode.Parent != null)
		{
			IsMouseClick = false;
			e.TreeNode.Parent.CheckedState = CheckState.Checked;
			IsMouseClick = true;
		}
	}

	private void BtnUser_Add_Click(object sender, EventArgs e)
	{
		if (F_UserID != "PccesUser" && !DBClass.ChkAuthority(F_UserID, "F001000600020001"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F001000600020001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (txtUserID.Text.Trim() == "")
		{
			MessageBox.Show(this, "使用者帳號不可空白", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtUserID.Focus();
			return;
		}
		if (txtUserName.Text.Trim() == "")
		{
			MessageBox.Show(this, "使用者名稱不可空白", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtUserName.Focus();
			return;
		}
		for (int i = 0; i < txtUserID.Text.Length; i++)
		{
			if (txtUserID.Text[i] > '\u007f')
			{
				MessageBox.Show(this, "使用者帳號不可以是中文或特殊字元", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtUserID.Focus();
				return;
			}
		}
		if (txtPwd.Text.Trim() != txtPwdConfirm.Text.Trim())
		{
			MessageBox.Show(this, "密碼並未確認正確。請確定您輸入的密碼和確認的密碼完全相符。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtPwd.Text = "";
			txtPwdConfirm.Text = "";
			txtUserID.Focus();
			return;
		}
		int iResult = DBCLS.InsertUsers(txtUserID.Text.Trim(), txtUserName.Text.Trim(), txtPwd.Text.Trim(), Cbo1.Value.ToString());
		if (iResult == -1)
		{
			MessageBox.Show(this, "相同使用者帳號資料已存在。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtUserID.Focus();
			return;
		}
		txtUserID.Text = "";
		txtUserName.Text = "";
		txtPwd.Text = "";
		txtPwdConfirm.Text = "";
		BindToUsers();
		for (int i = 1; i < GridUsers.Rows.Count; i++)
		{
			if (GridUsers[i, "UserID"].ToString().Trim() == txtUserID.Text.Trim())
			{
				GridUsers.Select(i, 0);
				GridUsers_AfterSelChange(null, null);
				break;
			}
		}
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		if (F_UserID != "PccesUser" && !DBClass.ChkAuthority(F_UserID, "F001000600010003"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F001000600010003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (GridGroups.Row <= 0)
		{
			MessageBox.Show(this, "請先挑選一個群組。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		FORM_STATUS = "GROUPS";
		FormSys_A_GrpMember FM_GRP_MBR = new FormSys_A_GrpMember();
		FM_GRP_MBR._GroupID = GridGroups[GridGroups.Row, "GroupID"].ToString().Trim();
		if (FM_GRP_MBR.ShowDialog(this) == DialogResult.OK)
		{
			DT_GroupUsers = DBCLS.GetGroupUsers(GridGroups[GridGroups.Row, "GroupID"].ToString().Trim());
			BindToGroupUsers();
		}
		FM_GRP_MBR.Close();
		FM_GRP_MBR.Dispose();
		FM_GRP_MBR = null;
		FORM_STATUS = "NORMAL";
	}

	private void BtnUser_Del_Click(object sender, EventArgs e)
	{
		if (F_UserID != "PccesUser" && !DBClass.ChkAuthority(F_UserID, "F001000600020003"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F001000600020003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else if (GridUsers.Row > 0)
		{
			if (MessageBox.Show(this, "確定要刪除選取的使用者嗎?\n相關的設定都會一併刪除", "警示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				DBCLS.DeleteUser(GridUsers[GridUsers.Row, "UserID"].ToString().Trim());
				BindToUsers();
			}
			GridUsers.Row = 0;
			ultraTree2.Visible = false;
		}
	}

	private void BtnSaveUser_Click(object sender, EventArgs e)
	{
		if (F_UserID != "PccesUser" && !DBClass.ChkAuthority(F_UserID, "F001000600020005"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F001000600020005") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (GridUsers.Row <= 0)
		{
			if (FORM_STATUS != "BEFORE_USERS")
			{
				MessageBox.Show(this, "請先挑選一個使用者。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return;
		}
		try
		{
			DT_UsrChk.Clear();
			for (int i = 0; i < ultraTree2.Nodes[0].Nodes.Count; i++)
			{
				GetChecked_User(ultraTree2.Nodes[0].Nodes[i]);
			}
			DBCLS.SaveUserFuncs(GridUsers[GridUsers.Row, "UserID"].ToString(), DT_UsrChk);
			if (FORM_STATUS != "BEFORE_USERS")
			{
				MessageBox.Show(this, "使用者功能儲存完畢!", "存檔", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "SysMaintain.FormSys_A.cs.cs" + ex.Message);
			if (FORM_STATUS != "BEFORE_USERS")
			{
				MessageBox.Show(this, ex.Message, "存檔", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		if (FORM_STATUS == "BEFORE_USERS")
		{
			FORM_STATUS = "NORMAL";
		}
	}

	private void ultraButton2_Click(object sender, EventArgs e)
	{
		if (F_UserID != "PccesUser" && !DBClass.ChkAuthority(F_UserID, "F001000600020004"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F001000600020004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (GridUsers.Row <= 0)
		{
			MessageBox.Show(this, "請先挑選一個使用者。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		FormSys_A_UsrGroup FM_USR_GRP = new FormSys_A_UsrGroup();
		FM_USR_GRP._UserID = GridUsers[GridUsers.Row, "UserID"].ToString().Trim();
		if (FM_USR_GRP.ShowDialog(this) == DialogResult.OK)
		{
			DT_UserGroups = DBCLS.GetUserGroups(GridUsers[GridUsers.Row, "UserID"].ToString().Trim());
			BindToUserGroups();
			ProcessUserAuthority(GridUsers[GridUsers.Row, "UserID"].ToString().Trim());
		}
		FM_USR_GRP.Close();
		FM_USR_GRP.Dispose();
		FM_USR_GRP = null;
	}

	private void BtnUser_Edt_Click(object sender, EventArgs e)
	{
		if (F_UserID != "PccesUser" && !DBClass.ChkAuthority(F_UserID, "F001000600020002"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F001000600020002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (GridUsers.Row <= 0)
		{
			MessageBox.Show(this, "請先挑選一個使用者。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		FormSys_A_Edit FM_A_EDT = new FormSys_A_Edit();
		FM_A_EDT._UserID = GridUsers[GridUsers.Row, "UserID"].ToString();
		FM_A_EDT._UserName = GridUsers[GridUsers.Row, "UserName"].ToString();
		FM_A_EDT._Password = GridUsers[GridUsers.Row, "Password"].ToString();
		FM_A_EDT._Power = GridUsers[GridUsers.Row, "Power"].ToString().Substring(0, 1);
		if (FM_A_EDT.ShowDialog(this) == DialogResult.OK)
		{
			BindToUsers();
		}
		FM_A_EDT.Close();
		FM_A_EDT.Dispose();
		FM_A_EDT = null;
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		switch (e.Tool.Key)
		{
		case "mnuDeleteGrp":
			BtnGRP_Del_Click(this, EventArgs.Empty);
			break;
		case "mnuDeleteUsr":
			BtnUser_Del_Click(this, EventArgs.Empty);
			break;
		case "mnuEditUsr":
			BtnUser_Edt_Click(this, EventArgs.Empty);
			break;
		}
	}

	private void GridGroups_BeforeEdit(object sender, RowColEventArgs e)
	{
		if (F_UserID != "PccesUser" && !DBClass.ChkAuthority(F_UserID, "F0010006000100050001"))
		{
			iAuthorityMSG_Count++;
			if (iAuthorityMSG_Count <= 1)
			{
				MessageBox.Show(this, DBClass.GetFuncName("F0010006000100050001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			iAuthorityMSG_Count = 0;
			e.Cancel = true;
			GridGroups.Col = 0;
		}
	}

	private void ultraTree1_MouseDown(object sender, MouseEventArgs e)
	{
		IsMouseClick = true;
	}

	private void ultraTree1_MouseUp(object sender, MouseEventArgs e)
	{
		IsMouseClick = false;
		FORM_STATUS = "NORMAL";
	}

	private void ultraTree2_MouseDown(object sender, MouseEventArgs e)
	{
		IsMouseClick = true;
	}

	private void ultraTree2_MouseUp(object sender, MouseEventArgs e)
	{
		IsMouseClick = false;
	}

	private void GridGroups_BeforeSelChange(object sender, RangeEventArgs e)
	{
		if (!(FORM_STATUS == "BINDING"))
		{
			int iRow = GridGroups.Row;
			if (ultraTree1.Visible && iRow > 0 && FORM_STATUS == "MOUSE_DOWN")
			{
				FORM_STATUS = "BEFORE_GROUP";
				BtnSaveGroup_Click(sender, e);
			}
		}
	}

	private void GridUsers_BeforeSelChange(object sender, RangeEventArgs e)
	{
		if (!(FORM_STATUS == "BINDING"))
		{
			int iRow = GridUsers.Row;
			if (ultraTree2.Visible && iRow > 0 && FORM_STATUS == "MOUSE_DOWN")
			{
				FORM_STATUS = "BEFORE_USERS";
				BtnSaveUser_Click(sender, e);
			}
		}
	}

	private void GridGroups_MouseDown(object sender, MouseEventArgs e)
	{
		if (GridGroups.Row > 0)
		{
			FORM_STATUS = "MOUSE_DOWN";
		}
	}

	private void GridUsers_MouseDown(object sender, MouseEventArgs e)
	{
		FORM_STATUS = "MOUSE_DOWN";
	}

	private void ultraTree1_Leave(object sender, EventArgs e)
	{
		int iRow = GridGroups.Row;
		FORM_STATUS = "MOUSE_DOWN";
		if (iRow > 0 && FORM_STATUS == "MOUSE_DOWN")
		{
			FORM_STATUS = "BEFORE_GROUP";
			BtnSaveGroup_Click(sender, e);
		}
	}

	private void ultraTree2_Leave(object sender, EventArgs e)
	{
		int iRow = GridUsers.Row;
		FORM_STATUS = "MOUSE_DOWN";
		if (iRow > 0 && FORM_STATUS == "MOUSE_DOWN")
		{
			FORM_STATUS = "BEFORE_USERS";
			BtnSaveUser_Click(sender, e);
		}
	}

	private void ultraTree1_AfterCheck(object sender, NodeEventArgs e)
	{
		if (!(FORM_STATUS == "NORMAL"))
		{
			return;
		}
		if (!IsCtrl && IsMouseClick)
		{
			for (int i = 0; i < e.TreeNode.Nodes.Count; i++)
			{
				e.TreeNode.Nodes[i].CheckedState = e.TreeNode.CheckedState;
			}
		}
		if (e.TreeNode.CheckedState == CheckState.Checked && e.TreeNode.Parent != null)
		{
			IsMouseClick = false;
			e.TreeNode.Parent.CheckedState = CheckState.Checked;
			IsMouseClick = true;
		}
	}

	private void GridUsers_MouseUp(object sender, MouseEventArgs e)
	{
		IsMouseClick = false;
		FORM_STATUS = "NORMAL";
	}

	private void ultraToolbarsManager1_BeforeToolbarListDropdown(object sender, BeforeToolbarListDropdownEventArgs e)
	{
		e.Cancel = true;
	}

	private void txtGroupID_Validating(object sender, CancelEventArgs e)
	{
		if (!CommonMethods.CheckValidString((sender as UltraTextEditor).Text))
		{
			e.Cancel = true;
		}
		if (!CommonMethods.IsStrByteLenValid(txtGroupID.Text, 45))
		{
			MessageBox.Show(this, "群組帳號的長度不可超過 40 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtGroupID.Focus();
			return;
		}
		if (!CommonMethods.IsStrByteLenValid(txtGroupName.Text, 50))
		{
			MessageBox.Show(this, "群組名稱的長度不可超過 50 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtGroupName.Focus();
			return;
		}
		if (!CommonMethods.IsStrByteLenValid(txtUserID.Text, 10))
		{
			MessageBox.Show(this, "使用者帳號的長度不可超過 10 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtUserID.Focus();
			return;
		}
		if (txtPwd.Text.IndexOf(" ") > 0)
		{
			MessageBox.Show(this, "密碼不可以含空白字元", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtPwd.Focus();
			return;
		}
		for (int i = 0; i < txtUserID.Text.Length; i++)
		{
			if (!CommonMethods.EngNumValid(txtUserID.Text[i]))
			{
				MessageBox.Show(this, "不可輸入非數字或英文字母及的字", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtUserID.Focus();
				break;
			}
		}
	}

	private void GridGroups_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
		{
			FORM_STATUS = "MOUSE_DOWN";
		}
	}

	private void GridUsers_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
		{
			FORM_STATUS = "MOUSE_DOWN";
		}
	}
}
