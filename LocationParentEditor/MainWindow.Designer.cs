﻿namespace LocationParentEditor
{
  partial class MainWindow
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.locTree = new System.Windows.Forms.TreeView();
      this.label1 = new System.Windows.Forms.Label();
      this.loadLocationsButton = new System.Windows.Forms.Button();
      this.saveHierarchyButton = new System.Windows.Forms.Button();
      this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
      this.locListBox = new System.Windows.Forms.ListBox();
      this.label2 = new System.Windows.Forms.Label();
      this.setParentButton = new System.Windows.Forms.Button();
      this.filterTextBox = new System.Windows.Forms.TextBox();
      this.hideLocsWithParentsCheckbox = new System.Windows.Forms.CheckBox();
      this.label3 = new System.Windows.Forms.Label();
      this.cellsTextBox = new System.Windows.Forms.TextBox();
      this.mergeChildrenToParentButton = new System.Windows.Forms.Button();
      this.label4 = new System.Windows.Forms.Label();
      this.keywordListBox = new System.Windows.Forms.ListBox();
      this.addKeywordContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.addKeywordMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.keywordListContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.addKeywordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.deleteKeywordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.flatLocListContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.addKeywordsToFlatLocMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.addKeywordContextMenu.SuspendLayout();
      this.keywordListContextMenu.SuspendLayout();
      this.flatLocListContextMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // locTree
      // 
      this.locTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.locTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.locTree.HideSelection = false;
      this.locTree.Location = new System.Drawing.Point(12, 32);
      this.locTree.Name = "locTree";
      this.locTree.Size = new System.Drawing.Size(518, 595);
      this.locTree.TabIndex = 0;
      this.locTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.locTree_AfterSelect);
      this.locTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.locTree_NodeMouseClick);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(97, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Location hierarchy:";
      // 
      // loadLocationsButton
      // 
      this.loadLocationsButton.Location = new System.Drawing.Point(1009, 32);
      this.loadLocationsButton.Name = "loadLocationsButton";
      this.loadLocationsButton.Size = new System.Drawing.Size(124, 34);
      this.loadLocationsButton.TabIndex = 2;
      this.loadLocationsButton.Text = "Load locations";
      this.loadLocationsButton.UseVisualStyleBackColor = true;
      this.loadLocationsButton.Click += new System.EventHandler(this.loadLocationsButton_Click);
      // 
      // saveHierarchyButton
      // 
      this.saveHierarchyButton.Location = new System.Drawing.Point(1009, 72);
      this.saveHierarchyButton.Name = "saveHierarchyButton";
      this.saveHierarchyButton.Size = new System.Drawing.Size(124, 34);
      this.saveHierarchyButton.TabIndex = 3;
      this.saveHierarchyButton.Text = "Save hierarchy";
      this.saveHierarchyButton.UseVisualStyleBackColor = true;
      this.saveHierarchyButton.Click += new System.EventHandler(this.saveHierarchyButton_Click);
      // 
      // locListBox
      // 
      this.locListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.locListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.locListBox.FormattingEnabled = true;
      this.locListBox.IntegralHeight = false;
      this.locListBox.ItemHeight = 20;
      this.locListBox.Location = new System.Drawing.Point(536, 67);
      this.locListBox.Name = "locListBox";
      this.locListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.locListBox.Size = new System.Drawing.Size(467, 560);
      this.locListBox.Sorted = true;
      this.locListBox.TabIndex = 4;
      this.locListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.locListBox_MouseDown);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(533, 9);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(79, 13);
      this.label2.TabIndex = 5;
      this.label2.Text = "Flat location list";
      // 
      // setParentButton
      // 
      this.setParentButton.Location = new System.Drawing.Point(1009, 190);
      this.setParentButton.Name = "setParentButton";
      this.setParentButton.Size = new System.Drawing.Size(124, 34);
      this.setParentButton.TabIndex = 6;
      this.setParentButton.Text = "Set parent";
      this.setParentButton.UseVisualStyleBackColor = true;
      this.setParentButton.Click += new System.EventHandler(this.setParentButton_Click);
      // 
      // filterTextBox
      // 
      this.filterTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.filterTextBox.Location = new System.Drawing.Point(536, 35);
      this.filterTextBox.Name = "filterTextBox";
      this.filterTextBox.Size = new System.Drawing.Size(467, 26);
      this.filterTextBox.TabIndex = 7;
      this.filterTextBox.TextChanged += new System.EventHandler(this.filterTextBox_TextChanged);
      // 
      // hideLocsWithParentsCheckbox
      // 
      this.hideLocsWithParentsCheckbox.AutoSize = true;
      this.hideLocsWithParentsCheckbox.Location = new System.Drawing.Point(659, 8);
      this.hideLocsWithParentsCheckbox.Name = "hideLocsWithParentsCheckbox";
      this.hideLocsWithParentsCheckbox.Size = new System.Drawing.Size(130, 17);
      this.hideLocsWithParentsCheckbox.TabIndex = 8;
      this.hideLocsWithParentsCheckbox.Text = "Hide locs with parents";
      this.hideLocsWithParentsCheckbox.UseVisualStyleBackColor = true;
      this.hideLocsWithParentsCheckbox.CheckedChanged += new System.EventHandler(this.hideLocsWithParentsCheckbox_CheckedChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(1136, 9);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(72, 13);
      this.label3.TabIndex = 10;
      this.label3.Text = "Cell mappings";
      // 
      // cellsTextBox
      // 
      this.cellsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cellsTextBox.Location = new System.Drawing.Point(1139, 32);
      this.cellsTextBox.Multiline = true;
      this.cellsTextBox.Name = "cellsTextBox";
      this.cellsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.cellsTextBox.Size = new System.Drawing.Size(405, 277);
      this.cellsTextBox.TabIndex = 11;
      // 
      // mergeChildrenToParentButton
      // 
      this.mergeChildrenToParentButton.Location = new System.Drawing.Point(1009, 275);
      this.mergeChildrenToParentButton.Name = "mergeChildrenToParentButton";
      this.mergeChildrenToParentButton.Size = new System.Drawing.Size(124, 34);
      this.mergeChildrenToParentButton.TabIndex = 13;
      this.mergeChildrenToParentButton.Text = "Merge children";
      this.mergeChildrenToParentButton.UseVisualStyleBackColor = true;
      this.mergeChildrenToParentButton.Click += new System.EventHandler(this.mergeChildrenToParentButton_Click);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(1136, 322);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(53, 13);
      this.label4.TabIndex = 14;
      this.label4.Text = "Keywords";
      // 
      // keywordListBox
      // 
      this.keywordListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.keywordListBox.FormattingEnabled = true;
      this.keywordListBox.IntegralHeight = false;
      this.keywordListBox.ItemHeight = 20;
      this.keywordListBox.Location = new System.Drawing.Point(1139, 344);
      this.keywordListBox.Name = "keywordListBox";
      this.keywordListBox.Size = new System.Drawing.Size(405, 284);
      this.keywordListBox.Sorted = true;
      this.keywordListBox.TabIndex = 15;
      this.keywordListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.keywordListBox_MouseDown);
      // 
      // addKeywordContextMenu
      // 
      this.addKeywordContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addKeywordMenuItem,
            this.toolStripSeparator1});
      this.addKeywordContextMenu.Name = "addKeywordContextMenu";
      this.addKeywordContextMenu.Size = new System.Drawing.Size(154, 32);
      // 
      // addKeywordMenuItem
      // 
      this.addKeywordMenuItem.Name = "addKeywordMenuItem";
      this.addKeywordMenuItem.Size = new System.Drawing.Size(153, 22);
      this.addKeywordMenuItem.Text = "Add keyword...";
      this.addKeywordMenuItem.Click += new System.EventHandler(this.addKeywordMenuItem_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(150, 6);
      // 
      // keywordListContextMenu
      // 
      this.keywordListContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addKeywordToolStripMenuItem,
            this.deleteKeywordToolStripMenuItem,
            this.toolStripSeparator2});
      this.keywordListContextMenu.Name = "keywordListContextMenu";
      this.keywordListContextMenu.Size = new System.Drawing.Size(156, 54);
      // 
      // addKeywordToolStripMenuItem
      // 
      this.addKeywordToolStripMenuItem.Name = "addKeywordToolStripMenuItem";
      this.addKeywordToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
      this.addKeywordToolStripMenuItem.Text = "Add keyword...";
      this.addKeywordToolStripMenuItem.Click += new System.EventHandler(this.addKeywordMenuItem_Click);
      // 
      // deleteKeywordToolStripMenuItem
      // 
      this.deleteKeywordToolStripMenuItem.Name = "deleteKeywordToolStripMenuItem";
      this.deleteKeywordToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
      this.deleteKeywordToolStripMenuItem.Text = "Delete keyword";
      this.deleteKeywordToolStripMenuItem.Click += new System.EventHandler(this.deleteKeywordToolStripMenuItem_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(152, 6);
      // 
      // flatLocListContextMenu
      // 
      this.flatLocListContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addKeywordsToFlatLocMenuItem,
            this.toolStripSeparator3});
      this.flatLocListContextMenu.Name = "flatLocListContextMenu";
      this.flatLocListContextMenu.Size = new System.Drawing.Size(159, 32);
      // 
      // addKeywordsToFlatLocMenuItem
      // 
      this.addKeywordsToFlatLocMenuItem.Name = "addKeywordsToFlatLocMenuItem";
      this.addKeywordsToFlatLocMenuItem.Size = new System.Drawing.Size(158, 22);
      this.addKeywordsToFlatLocMenuItem.Text = "Add keywords...";
      this.addKeywordsToFlatLocMenuItem.Click += new System.EventHandler(this.addKeywordsToFlatListLoc_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(155, 6);
      // 
      // MainWindow
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1556, 639);
      this.Controls.Add(this.keywordListBox);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.mergeChildrenToParentButton);
      this.Controls.Add(this.cellsTextBox);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.hideLocsWithParentsCheckbox);
      this.Controls.Add(this.filterTextBox);
      this.Controls.Add(this.setParentButton);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.locListBox);
      this.Controls.Add(this.saveHierarchyButton);
      this.Controls.Add(this.loadLocationsButton);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.locTree);
      this.Name = "MainWindow";
      this.Text = "Location Editor";
      this.addKeywordContextMenu.ResumeLayout(false);
      this.keywordListContextMenu.ResumeLayout(false);
      this.flatLocListContextMenu.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

        #endregion

        private System.Windows.Forms.TreeView locTree;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button loadLocationsButton;
        private System.Windows.Forms.Button saveHierarchyButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ListBox locListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button setParentButton;
        private System.Windows.Forms.TextBox filterTextBox;
        private System.Windows.Forms.CheckBox hideLocsWithParentsCheckbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox cellsTextBox;
        private System.Windows.Forms.Button mergeChildrenToParentButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox keywordListBox;
        private System.Windows.Forms.ContextMenuStrip addKeywordContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addKeywordMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip keywordListContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addKeywordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteKeywordToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ContextMenuStrip flatLocListContextMenu;
    private System.Windows.Forms.ToolStripMenuItem addKeywordsToFlatLocMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
  }
}

