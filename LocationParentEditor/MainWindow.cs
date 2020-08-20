using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocationParentEditor
{
  public partial class MainWindow : Form
  {
    /// <summary>
    /// Maps location form IDs to location editor IDs.
    /// </summary>
    
    private Dictionary<string, string> formIdToEdid = new Dictionary<string, string>();

    /// <summary>
    /// Maps location editor IDs to location form IDs.
    /// </summary>

    private Dictionary<string, string> edidToFormId = new Dictionary<string, string>();

    /// <summary>
    /// Maps location editor IDs to strings that contain the editor IDs of
    /// the cells that are "in" that location, separated by Environment.NewLine.
    /// </summary>

    private Dictionary<string, string> locEdidToCells = new Dictionary<string, string>();

    /// <summary>
    /// Maps keyword editor IDs to keyword form IDs.
    /// </summary>

    private Dictionary<string, string> keywordEdidToFormId = new Dictionary<string, string>();

    /// <summary>
    /// Maps keyword form IDs to keyword editor IDs.
    /// </summary>

    private Dictionary<string, string> keywordFormIdToEdid = new Dictionary<string, string>();

    /// <summary>
    /// Maps location editor IDs to lists of keywords that apply to those
    /// locations.
    /// </summary>

    private Dictionary<string, BindingList<string>> locEdidToKeywordList = new Dictionary<string, BindingList<string>>();

    /// <summary>
    /// List of all the location editor IDs.
    /// </summary>

    private List<string> locList = new List<string>();

    /// <summary>
    /// Editor IDs for locations that have been removed by the merge children
    /// command.
    /// </summary>

    private List<string> removedLocs = new List<string>();

    /// <summary>
    /// List of all the keyword editor IDs.
    /// </summary>

    private List<string> keywordList = new List<string>();

    /// <summary>
    /// The editor ID of the location that's selected in the location tree.
    /// </summary>

    private string selectedLoc = null;

    /// <summary>
    /// File name where editor IDs for "child" locations are stored.
    /// Corresponds to the file identified by ParentFormIdFile. Line x in the
    /// parent file is the form ID of the parent location for the location
    /// whose editor ID is at line x in this file.
    /// 
    /// Each editor ID should only appear once in this file.
    /// </summary>

    const string LocEdidFile = "locsForSetParent.txt";

    /// <summary>
    /// File name where form IDs for "parent" locations are stored.
    /// Corresponds to the file identified by LocEdidFile. Line x in the
    /// LocEdidFile is the editor ID of one of the child locations of the
    /// location whose form ID is stored at line x of this file.
    /// 
    /// The same form ID can appear many times in this file, indicating that
    /// the location has multiple direct children.
    /// </summary>

    const string ParentFormIdFile = "parentsForSetParent.txt";

    /// <summary>
    /// File name where editor IDs for cells that are "in" a location are
    /// stored. Corresponds to the file identified by LocFormIdForCellFile.
    /// Line x in the LocFormIdForCellFile is the location for the cell whose
    /// editor ID is stored at line x of this file.
    /// 
    /// Each editor ID should only appear once in this file.
    /// </summary>

    const string CellEdidFile = "cellListForLocMapping.txt";

    /// <summary>
    /// File name where form IDs for cell locations are stored. Corresponds
    /// to the file identified by CellEdidFile. Line x in the CellEdidFile is
    /// the editor ID of a cell that is "in" the location whose form ID is
    /// stored at line x of this file.
    /// 
    /// Many cells can be in the same location, so the same form ID can appear
    /// multiple times.
    /// </summary>

    const string LocFormIdForCellFile = "cellLocFormIdMapping.txt";

    const string LocEdidsForSetKeywordsFile = "locEdidsForSetKeywords.txt";
    const string KeywordsForSetKeywordsFile = "keywordFormIdsForSetKeywords.txt";

    public MainWindow()
    {
      InitializeComponent();

      AddKeywordSetContextMenuItem(KeywordSets.CitySet);
      AddKeywordSetContextMenuItem(KeywordSets.CityWithInnSet);
      AddKeywordSetContextMenuItem(KeywordSets.FarmSet);
      AddKeywordSetContextMenuItem(KeywordSets.FortSet);
      AddKeywordSetContextMenuItem(KeywordSets.HouseSet);
      AddKeywordSetContextMenuItem(KeywordSets.InnSet);
      AddKeywordSetContextMenuItem(KeywordSets.JailSet);
      AddKeywordSetContextMenuItem(KeywordSets.TownSet);
      AddKeywordSetContextMenuItem(KeywordSets.TownWithInnSet);
    }

    /// <summary>
    /// Handles clicks of the "add keyword" context menu item.
    /// </summary>

    private void addKeywordMenuItem_Click(object sender, EventArgs e)
    {
      var selectedLoc = this.locTree.SelectedNode?.Text;

      if (selectedLoc != null)
      {
        var addKeywordForm = new AddKeywordForm(this.keywordList);

        if (addKeywordForm.ShowDialog() == DialogResult.OK)
        {
          AddKeywordsToLocation(selectedLoc, addKeywordForm.SelectedKeywords);
        }
      }
    }

    /// <summary>
    /// Handles changing the value of the "hide parented" checkbox. When this
    /// checkbox is checked, the flat location list box doesn't show locations
    /// that have already been assigned a parent.
    /// </summary>

    private void hideParentedCheckbox_CheckedChanged(object sender, EventArgs e)
    {
      UpdateFiltering();
    }

    /// <summary>
    /// Handles changing the values of the filter text box. The flat location
    /// list box will only show locations that contain the filter text (case
    /// insensitive and combines with "hide parented").
    /// </summary>

    private void filterTextBox_TextChanged(object sender, EventArgs e)
    {
      UpdateFiltering();
    }

    /// <summary>
    /// Handles "load cell mappings" button.
    /// </summary>

    private void loadCellMappingsButton_Click(object sender, EventArgs e)
    {
      LoadCellMappings();
    }

    /// <summary>
    /// Handles "load hierarchy" button.
    /// </summary>

    private void loadHierarchyButton_Click(object sender, EventArgs e)
    {
      LoadHierarchy();
    }

    /// <summary>
    /// Handles "load locations" button.
    /// </summary>

    private void loadLocationsButton_Click(object sender, EventArgs e)
    {
      LoadLocations();
      LoadHierarchy();
      LoadCellMappings();
      LoadKeywords();
      LoadKeywordMappings();
    }

    /// <summary>
    /// Handles selection changes in the location tree.
    /// </summary>

    private void locTree_AfterSelect(object sender, TreeViewEventArgs e)
    {
      this.CommitSelectedLocCellMapping();

      var newSelectedLoc = this.locTree.SelectedNode?.Text;

      if (newSelectedLoc == null)
      {
        this.cellsTextBox.Text = "<No location selected>";
        this.keywordListBox.DataSource = null;
      }
      else
      {
        if (this.locEdidToCells.TryGetValue(newSelectedLoc, out var cellText))
        {
          this.cellsTextBox.Text = cellText;
        }
        else
        {
          this.cellsTextBox.Text = string.Empty;
        }

        this.keywordListBox.DataSource = this.locEdidToKeywordList[newSelectedLoc];
      }

      this.selectedLoc = newSelectedLoc;
    }

    /// <summary>
    /// Handles "merge children" button.
    /// </summary>

    private void mergeChildrenToParentButton_Click(object sender, EventArgs e)
    {
      var selectedNode = this.locTree.SelectedNode;

      if (selectedNode != null)
      {
        MergeChildrenToParent(selectedNode);
      }
    }

    /// <summary>
    /// Handles mouse clicks on the location tree.
    /// </summary>

    private void locTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        this.locTree.SelectedNode = e.Node;
        this.keywordContextMenu.Show(this.locTree.PointToScreen(e.Location));
      }
    }

    /// <summary>
    /// Handles clicks of context menu items corresponding to KeywordSets.
    /// </summary>
    /// 
    /// <param name="keywordSet">
    /// The KeywordSet whose menu item was clicked.
    /// </param>

    private void OnKeywordSetMenuItemClicked(KeywordSet keywordSet)
    {
      var selectedLoc = this.locTree.SelectedNode?.Text;

      if (selectedLoc != null)
      {
        AddKeywordsToLocation(selectedLoc, keywordSet.Keywords);
      }
    }

    /// <summary>
    /// Handles "save hierarchy" button.
    /// </summary>

    private void saveHierarchyButton_Click(object sender, EventArgs e)
    {
      CommitSelectedLocCellMapping();

      SaveHierarchy();

      using (var removedWriter = File.CreateText("RemovedLocations.txt"))
      {
        foreach (var removedLoc in this.removedLocs)
        {
          removedWriter.WriteLine(removedLoc);
        }
      }

      SaveCellMappings();
      SaveKeywordMappings();
    }

    /// <summary>
    /// Handles "set parent" button. If there is a location selected in the
    /// location tree, that location becomes the parent of the locations that
    /// are selected in the flat location list box. Also checks to make sure
    /// you're not attempting to do weird things like set a location as its
    /// own parent.
    /// </summary>

    private void setParentButton_Click(object sender, EventArgs e)
    {
      var selectedParent = this.locTree.SelectedNode;

      if (selectedParent == null)
      {
        MessageBox.Show("You must select a parent node.");
        return;
      }

      var selectedLocs = new List<string>();

      foreach (string edid in this.locListBox.SelectedItems)
      {
        selectedLocs.Add(edid);
      }

      SetParent(selectedParent, selectedLocs);
      UpdateFiltering();
    }

    /// <summary>
    /// Adds a menu item for a KeywordSet to the keyword context menu.
    /// </summary>
    /// 
    /// <param name="keywordSet">
    /// The KeywordSet to create a menu item for. Must not be null.
    /// </param>

    private void AddKeywordSetContextMenuItem(KeywordSet keywordSet)
    {
      var menuItem = new ToolStripMenuItem(keywordSet.Name);
      menuItem.Click += (sender, e) => OnKeywordSetMenuItemClicked(keywordSet);
      this.keywordContextMenu.Items.Add(menuItem);
    }

    private void AddKeywordsToLocation(string locEdid, IEnumerable<string> keywords)
    {
      var keywordList = this.locEdidToKeywordList[locEdid];

      foreach (var keyword in keywords)
      {
        if (!keywordList.Contains(keyword))
        {
          keywordList.Add(keyword);
        }
      }
    }

    /// <summary>
    /// Clears the cells associated to all locations without prompting for
    /// confirmation first.
    /// </summary>

    private void ClearCellsNoPrompt()
    {
      foreach (var key in this.locList)
      {
        locEdidToCells[key] = string.Empty;
      }

      this.cellsTextBox.Text = string.Empty;
    }

    /// <summary>
    /// Saves the cell mappings for the currently selected location (as
    /// indicated by this.selectedLoc).
    /// </summary>

    private void CommitSelectedLocCellMapping()
    {
      if (this.selectedLoc != null)
      {
        var cellText = this.cellsTextBox.Text;
        this.locEdidToCells[this.selectedLoc] = cellText;
      }
    }

    /// <summary>
    /// Finds the item in the location tree with the specified editor ID.
    /// Searches recursively.
    /// </summary>
    /// 
    /// <param name="nodeCollection">
    /// The node collection to search. Must not be null.
    /// </param>
    /// 
    /// <param name="edid">
    /// The editor ID to search for. Must not be null.
    /// </param>
    /// 
    /// <returns>
    /// The TreeItem if it was found; otherwise null.
    /// </returns>

    private TreeNode FindTreeItem(TreeNodeCollection nodeCollection, string edid)
    {
      // Might be possible to replace this with nodeCollection.Find, but this
      // does the job and doesn't introduce arrays into the mix

      var item = nodeCollection[edid];

      if (item != null)
      {
        return item;
      }

      for (int i = 0; i < nodeCollection.Count; ++i)
      {
        var node = nodeCollection[i];
        item = FindTreeItem(node.Nodes, edid);

        if (item != null)
        {
          return item;
        }
      }

      return null;
    }

    /// <summary>
    /// Loads the mappings of the cells that are "in" a given location.
    /// </summary>

    private void LoadCellMappings()
    {
      try
      {
        var locFormIds = File.ReadAllLines(LocFormIdForCellFile).Select(line => line.Trim());
        var cellList = File.ReadAllLines(CellEdidFile).Select(line => line.Trim());

        ClearCellsNoPrompt();

        foreach (var pair in locFormIds.Zip(
          cellList, (locFormId, cell) => new { LocFormId = locFormId, Cell = cell }))
        {
          if (!string.IsNullOrEmpty(pair.LocFormId) && !string.IsNullOrEmpty(pair.Cell))
          {
            if (this.formIdToEdid.TryGetValue(pair.LocFormId, out var locEdid))
            {
              var value = pair.Cell;

              if (this.locEdidToCells.TryGetValue(locEdid, out var cells))
              {
                if (cells != string.Empty)
                {
                  value = cells + Environment.NewLine + value;
                }
              }

              this.locEdidToCells[locEdid] = value;
            }
          }
        }

        var selectedLoc = this.locTree.SelectedNode?.Text;

        if (selectedLoc != null)
        {
          var cellText = this.locEdidToCells[selectedLoc];
          this.cellsTextBox.Text = cellText;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("Couldn't load mappings.\n\n" + ex.Message);
      }
    }

    /// <summary>
    /// Loads the location hierarchy data indicating which locations are
    /// children of which other locations.
    /// </summary>

    private void LoadHierarchy()
    {
      try
      {
        var edids = File.ReadAllLines(LocEdidFile);
        var parents = File.ReadAllLines(ParentFormIdFile);

        foreach (var pair in edids.Zip(
          parents, (edid, parent) => new { Edid = edid, Parent = parent }))
        {
          if (!formIdToEdid.ContainsKey(pair.Parent))
          {
            MessageBox.Show("Couldn't find parent for FormID " + pair.Parent);
            continue;
          }

          var parentEdid = formIdToEdid[pair.Parent];
          var parentNode = FindTreeItem(this.locTree.Nodes, parentEdid);

          SetParent(parentNode, new string[] { pair.Edid });
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("Couldn't load files.\n\n" + ex.Message);
      }
    }

    /// <summary>
    /// Loads the set of keywords that will be available to assign to
    /// locations.
    /// </summary>

    private void LoadKeywords()
    {
      try
      {
        keywordEdidToFormId.Clear();
        keywordFormIdToEdid.Clear();

        var keywords = File.ReadAllLines("keywordsWithFormIDs.txt")
          .Select(line => line.Trim())
          .Where(line => !string.IsNullOrEmpty(line))
          .Select(line =>
          {
            var splitLine = line.Trim().Split(';');

            if (splitLine.Length != 2)
            {
              throw new Exception("Bad line in keyword file: " + line);
            }

            return new { FormId = splitLine[0], Edid = splitLine[1] };
          })
          .Where(keyword => keyword.Edid.Contains("LocType"));

        foreach (var keyword in keywords)
        {
          this.keywordEdidToFormId.Add(keyword.Edid, keyword.FormId);
          this.keywordFormIdToEdid.Add(keyword.FormId, keyword.Edid);
          this.keywordList.Add(keyword.Edid);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("Couldn't load keyword list: " + ex.Message);
      }
    }

    /// <summary>
    /// Loads the mappings of keywords to locations.
    /// </summary>

    private void LoadKeywordMappings()
    {
      try
      {
        var locEdids = File.ReadAllLines(LocEdidsForSetKeywordsFile);
        var keywords = File.ReadAllLines(KeywordsForSetKeywordsFile);

        foreach (var pair in locEdids.Zip(keywords,
          (locEdid, keyword) => new { LocEdid = locEdid, KeywordFormId = keyword }))
        {
          var keywordList = this.locEdidToKeywordList[pair.LocEdid];
          var keywordEdid = this.keywordFormIdToEdid[pair.KeywordFormId];

          if (!keywordList.Contains(keywordEdid))
          {
            keywordList.Add(keywordEdid);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("Couldn't load keyword mappings: " + ex.Message);
      }
    }

    /// <summary>
    /// Loads the set of locations that will be available to edit.
    /// </summary>

    private void LoadLocations()
    {
      if (this.openFileDialog.ShowDialog() == DialogResult.OK)
      {
        var lines = File.ReadAllLines(this.openFileDialog.FileName).Select(line => line.Trim());

        foreach (var line in lines)
        {
          var splitLine = line.Split(';');

          if (splitLine.Length != 2)
          {
            MessageBox.Show(
              "Unexpected file format. Bad line: " + line,
              "Error",
              MessageBoxButtons.OK,
              MessageBoxIcon.Error);

            return;
          }

          var formId = splitLine[0];
          var edid = splitLine[1];

          this.formIdToEdid[formId] = edid;
          this.edidToFormId[edid] = formId;

          if (!locList.Contains(edid))
          {
            locList.Add(edid);
            this.locListBox.Items.Add(edid);
          }
        }

        this.locList.Sort();

        foreach (string edid in this.locListBox.Items)
        {
          this.locTree.Nodes.Add(edid, edid);
          this.locEdidToKeywordList.Add(edid, new BindingList<string>());
        }

        this.ClearCellsNoPrompt();
      }
    }

    /// <summary>
    /// Merges the child locations of the specified parent into the parent.
    /// "Merging" means that the cells and keywords assigned to the child
    /// locations are now assigned to the parent, and the child locations are
    /// removed.
    /// </summary>
    /// 
    /// <param name="parent">
    /// The parent whose children should be merged into it. Must not be null.
    /// </param>

    private void MergeChildrenToParent(TreeNode parent)
    {
      var parentEdid = parent.Text;
      var parentCells = this.locEdidToCells[parentEdid].Trim();
      var parentKeywords = this.locEdidToKeywordList[parentEdid];

      for (int i = 0; i < parent.Nodes.Count; ++i)
      {
        var child = parent.Nodes[i];
        var childEdid = child.Text;

        MergeChildrenToParent(child);

        var childCells = this.locEdidToCells[childEdid].Trim();
        parentCells += Environment.NewLine + childCells;
        this.locEdidToCells.Remove(childEdid);

        var childKeywords = this.locEdidToKeywordList[childEdid];

        foreach (var childKeyword in childKeywords)
        {
          if (!parentKeywords.Contains(childKeyword))
          {
            parentKeywords.Add(childKeyword);
          }
        }

        this.locEdidToKeywordList.Remove(childEdid);
        this.edidToFormId.Remove(childEdid);
        this.removedLocs.Add(childEdid);
      }

      parent.Nodes.Clear();
      this.locEdidToCells[parent.Text] = parentCells;
      this.cellsTextBox.Text = parentCells;
    }

    /// <summary>
    /// Saves hierarchy data.
    /// </summary>

    private void SaveHierarchy()
    {
      using (var locWriter = File.CreateText(LocEdidFile))
      {
        using (var parentWriter = File.CreateText(ParentFormIdFile))
        {
          var nodes = this.locTree.Nodes;

          WriteHierarchy(nodes, locWriter, parentWriter);
        }
      }
    }

    /// <summary>
    /// Saves cell mapping data.
    /// </summary>

    private void SaveCellMappings()
    {
      using (var formIdWriter = File.CreateText(LocFormIdForCellFile))
      {
        using (var cellEdidWriter = File.CreateText(CellEdidFile))
        {
          foreach (string locEdid in this.locList)
          {
            var cells = this.locEdidToCells[locEdid];
            var formId = this.edidToFormId[locEdid];

            using (var strReader = new StringReader(cells))
            {
              string line;

              while ((line = strReader.ReadLine()) != null)
              {
                cellEdidWriter.WriteLine(line.Trim());

                // This will write the same form ID a bunch of times if there
                // are multiple cells mapped to the location, but that's on
                // purpose. It's so that the xEdit script can work with these
                // as parallel arrays in the absence of a working dictionary
                // type (at least that I can find in some basic attempts).

                formIdWriter.WriteLine(formId);
              }
            }
          }
        }
      }
    }

    /// <summary>
    /// Saves keyword mapping data.
    /// </summary>

    private void SaveKeywordMappings()
    {
      try
      {
        using (var locEdidWriter = File.CreateText(LocEdidsForSetKeywordsFile))
        {
          using (var keywordWriter = File.CreateText(KeywordsForSetKeywordsFile))
          {
            foreach (var locEdid in this.locList)
            {
              foreach (var keyword in this.locEdidToKeywordList[locEdid])
              {
                locEdidWriter.WriteLine(locEdid);
                keywordWriter.WriteLine(this.keywordEdidToFormId[keyword]);
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("Failed to write keyword mappings: " + ex.Message);
      }
    }

    /// <summary>
    /// Sets the parent of a set of locations.
    /// </summary>
    /// 
    /// <param name="newParent">
    /// The TreeNode corresponding to the new parent for the locations. Must
    /// not be null.
    /// </param>
    /// 
    /// <param name="childrenEdids">
    /// The editor IDs of the locations that should become children of the
    /// specified parent.
    /// </param>

    private void SetParent(TreeNode newParent, IEnumerable<string> childrenEdids)
    {
      var selectedNodes = new List<TreeNode>();

      foreach (string edid in childrenEdids)
      {
        var selectedNode = FindTreeItem(this.locTree.Nodes, edid);

        if (selectedNode == null)
        {
          MessageBox.Show("Couldn't find selected location: " + edid);
          return;
        }

        if (selectedNode == newParent)
        {
          MessageBox.Show("Can't set item as its own parent: " + edid);
          return;
        }

        selectedNodes.Add(selectedNode);

        var parent = newParent.Parent;

        while (parent != null)
        {
          if (parent == selectedNode)
          {
            MessageBox.Show("Can't make a parent a child of one of its own children.");
            return;
          }

          parent = parent.Parent;
        }
      }

      foreach (var node in selectedNodes)
      {
        if (node.Parent != null)
        {
          node.Parent.Nodes.Remove(node);
        }
        else
        {
          this.locTree.Nodes.Remove(node);
        }

        newParent.Nodes.Add(node);
      }
    }

    /// <summary>
    /// Updates the filtering of the flat location list box.
    /// </summary>

    private void UpdateFiltering()
    {
      this.locListBox.BeginUpdate();
      this.locListBox.Items.Clear();

      var filteredForParenting =
        this.hideParentedCheckbox.Checked ?
        this.locList.Where(edid => this.locTree.Nodes[edid] != null) :
        this.locList;

      if (this.filterTextBox.TextLength > 0)
      {
        var filterText = this.filterTextBox.Text;

        this.locListBox.Items.AddRange(filteredForParenting
          .Where(edid => edid.ToLower().Contains(filterText.ToLower()))
          .ToArray());
      }
      else
      {
        this.locListBox.Items.AddRange(filteredForParenting.ToArray());
      }

      this.locListBox.EndUpdate();
    }

    /// <summary>
    /// Writes the specified TreeNodeCollection to the given output streams.
    /// </summary>
    /// 
    /// <param name="nodes">
    /// The TreeNodeCollection to write. This collcetion and all of its child
    /// collections will be written. Must not be null.
    /// </param>
    /// 
    /// <param name="locWriter">
    /// Writer where the child location editor IDs are written.
    /// </param>
    /// 
    /// <param name="parentWriter">
    /// Writer where the parent location form IDs are written.
    /// </param>

    private void WriteHierarchy(
      TreeNodeCollection nodes,
      StreamWriter locWriter,
      StreamWriter parentWriter)
    {
      for (int i = 0; i < nodes.Count; ++i)
      {
        var node = nodes[i];

        if (node.Parent != null)
        {
          locWriter.WriteLine(node.Text);
          parentWriter.WriteLine(this.edidToFormId[node.Parent.Text]);
        }

        WriteHierarchy(node.Nodes, locWriter, parentWriter);
      }
    }
  }
}
