namespace LocationParentEditor
{
  partial class AddKeywordForm
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
      this.keywordListBox = new System.Windows.Forms.ListBox();
      this.okButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // keywordListBox
      // 
      this.keywordListBox.FormattingEnabled = true;
      this.keywordListBox.Location = new System.Drawing.Point(12, 12);
      this.keywordListBox.Name = "keywordListBox";
      this.keywordListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.keywordListBox.Size = new System.Drawing.Size(303, 524);
      this.keywordListBox.TabIndex = 0;
      // 
      // okButton
      // 
      this.okButton.Location = new System.Drawing.Point(91, 542);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(109, 27);
      this.okButton.TabIndex = 1;
      this.okButton.Text = "OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelButton.Location = new System.Drawing.Point(206, 542);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(109, 27);
      this.cancelButton.TabIndex = 2;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // AddKeywordForm
      // 
      this.AcceptButton = this.okButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cancelButton;
      this.ClientSize = new System.Drawing.Size(327, 581);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.keywordListBox);
      this.Name = "AddKeywordForm";
      this.Text = "Add Keywords";
      this.ResumeLayout(false);

    }

        #endregion

        private System.Windows.Forms.ListBox keywordListBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}