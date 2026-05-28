using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;

namespace Archnowledge.Pcces.PccesMain.ArchControls;

public class LevelSwitchButton : UserControl
{
	public delegate void LevelSwitchButtonClickHandler();

	private int maxLevel = 8;

	private int selectedLevel;

	private IContainer components = null;

	private ToolStrip LevelButtons;

	private ToolStripButton Level1;

	private ToolStripButton Level2;

	private ToolStripButton Level3;

	private ToolStripButton Level4;

	private ToolStripButton Level5;

	private ToolStripButton Level6;

	private ToolStripButton Level7;

	private ToolStripButton Level8;

	public int MaxLevel
	{
		set
		{
			maxLevel = ((value >= 1 && value <= 8) ? value : 8);
			((ToolStripButton)LevelButtons.Items[maxLevel - 1]).Checked = true;
			selectedLevel = maxLevel;
			for (int i = maxLevel; i < 8; i++)
			{
				LevelButtons.Items[i].Enabled = false;
			}
		}
	}

	public int SelectedLevel => selectedLevel;

	public event LevelSwitchButtonClickHandler LevelSwitchButtonsClicked;

	public LevelSwitchButton()
	{
		InitializeComponent();
	}

	private void Level_Clicked(object sender, EventArgs e)
	{
		int CheckedIndex = (selectedLevel = ArchConvert.Obj2Int(((ToolStripButton)sender).Name.Substring(5)));
		for (int i = 0; i < maxLevel; i++)
		{
			((ToolStripButton)LevelButtons.Items[i]).Checked = i == CheckedIndex - 1;
		}
		OnLevelSwitchButtonsClicked();
	}

	protected virtual void OnLevelSwitchButtonsClicked()
	{
		if (this.LevelSwitchButtonsClicked != null)
		{
			this.LevelSwitchButtonsClicked();
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.ArchControls.LevelSwitchButton));
		this.LevelButtons = new System.Windows.Forms.ToolStrip();
		this.Level1 = new System.Windows.Forms.ToolStripButton();
		this.Level2 = new System.Windows.Forms.ToolStripButton();
		this.Level3 = new System.Windows.Forms.ToolStripButton();
		this.Level4 = new System.Windows.Forms.ToolStripButton();
		this.Level5 = new System.Windows.Forms.ToolStripButton();
		this.Level6 = new System.Windows.Forms.ToolStripButton();
		this.Level7 = new System.Windows.Forms.ToolStripButton();
		this.Level8 = new System.Windows.Forms.ToolStripButton();
		this.LevelButtons.SuspendLayout();
		base.SuspendLayout();
		this.LevelButtons.AutoSize = false;
		this.LevelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
		this.LevelButtons.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.LevelButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
		this.LevelButtons.Items.AddRange(new System.Windows.Forms.ToolStripItem[8] { this.Level1, this.Level2, this.Level3, this.Level4, this.Level5, this.Level6, this.Level7, this.Level8 });
		this.LevelButtons.Location = new System.Drawing.Point(0, 0);
		this.LevelButtons.Name = "LevelButtons";
		this.LevelButtons.Size = new System.Drawing.Size(165, 22);
		this.LevelButtons.TabIndex = 0;
		this.LevelButtons.Text = "toolStrip1";
		this.Level1.AutoSize = false;
		this.Level1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.Level1.Image = (System.Drawing.Image)resources.GetObject("Level1.Image");
		this.Level1.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.Level1.Name = "Level1";
		this.Level1.Size = new System.Drawing.Size(20, 19);
		this.Level1.Text = "1";
		this.Level1.Click += new System.EventHandler(Level_Clicked);
		this.Level2.AutoSize = false;
		this.Level2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.Level2.Image = (System.Drawing.Image)resources.GetObject("Level2.Image");
		this.Level2.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.Level2.Name = "Level2";
		this.Level2.Size = new System.Drawing.Size(20, 19);
		this.Level2.Text = "2";
		this.Level2.Click += new System.EventHandler(Level_Clicked);
		this.Level3.AutoSize = false;
		this.Level3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.Level3.Image = (System.Drawing.Image)resources.GetObject("Level3.Image");
		this.Level3.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.Level3.Name = "Level3";
		this.Level3.Size = new System.Drawing.Size(20, 19);
		this.Level3.Text = "3";
		this.Level3.Click += new System.EventHandler(Level_Clicked);
		this.Level4.AutoSize = false;
		this.Level4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.Level4.Image = (System.Drawing.Image)resources.GetObject("Level4.Image");
		this.Level4.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.Level4.Name = "Level4";
		this.Level4.Size = new System.Drawing.Size(20, 19);
		this.Level4.Text = "4";
		this.Level4.Click += new System.EventHandler(Level_Clicked);
		this.Level5.AutoSize = false;
		this.Level5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.Level5.Image = (System.Drawing.Image)resources.GetObject("Level5.Image");
		this.Level5.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.Level5.Name = "Level5";
		this.Level5.Size = new System.Drawing.Size(20, 19);
		this.Level5.Text = "5";
		this.Level5.Click += new System.EventHandler(Level_Clicked);
		this.Level6.AutoSize = false;
		this.Level6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.Level6.Image = (System.Drawing.Image)resources.GetObject("Level6.Image");
		this.Level6.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.Level6.Name = "Level6";
		this.Level6.Size = new System.Drawing.Size(20, 19);
		this.Level6.Text = "6";
		this.Level6.Click += new System.EventHandler(Level_Clicked);
		this.Level7.AutoSize = false;
		this.Level7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.Level7.Image = (System.Drawing.Image)resources.GetObject("Level7.Image");
		this.Level7.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.Level7.Name = "Level7";
		this.Level7.Size = new System.Drawing.Size(20, 19);
		this.Level7.Text = "7";
		this.Level7.Click += new System.EventHandler(Level_Clicked);
		this.Level8.AutoSize = false;
		this.Level8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.Level8.Image = (System.Drawing.Image)resources.GetObject("Level8.Image");
		this.Level8.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.Level8.Name = "Level8";
		this.Level8.Size = new System.Drawing.Size(20, 19);
		this.Level8.Text = "8";
		this.Level8.Click += new System.EventHandler(Level_Clicked);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.LevelButtons);
		base.Name = "LevelSwitchButton";
		base.Size = new System.Drawing.Size(165, 22);
		this.LevelButtons.ResumeLayout(false);
		this.LevelButtons.PerformLayout();
		base.ResumeLayout(false);
	}
}
