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
      EndDialogWithKeywords();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
    }

    private void keywordListBox_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left && this.keywordListBox.SelectedItems.Count != 0)
      {
        EndDialogWithKeywords();
      }
    }

    private void EndDialogWithKeywords()
    {
      this.SelectedKeywords = new string[this.keywordListBox.SelectedItems.Count];
      this.keywordListBox.SelectedItems.CopyTo(this.SelectedKeywords, 0);
      this.DialogResult = DialogResult.OK;
    }
  }
}
