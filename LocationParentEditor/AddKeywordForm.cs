using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocationParentEditor
{
  public partial class AddKeywordForm : Form
  {
    public string[] SelectedKeywords { get; private set; }

    public AddKeywordForm(IEnumerable<string> keywords)
    {
      InitializeComponent();

      foreach (var keyword in keywords)
      {
        this.keywordListBox.Items.Add(keyword);
      }
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      this.SelectedKeywords = new string[this.keywordListBox.SelectedItems.Count];
      this.keywordListBox.SelectedItems.CopyTo(this.SelectedKeywords, 0);
      this.DialogResult = DialogResult.OK;
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
    }
  }
}
