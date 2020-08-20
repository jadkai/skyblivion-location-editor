# skyblivion-location-editor
Tool to help with quickly editing location hierarchies for a Skyrim mod.

**IMPORTANT**

This tool relies on data exported from xEdit in a specific format. Specifically, it requires a text file containing locations where each line in the file has the form ID and editor ID of a location in the following format:

`<Form ID>;<Editor ID>`

This tool **DOES NOT** edit ESP, ESM, or ESL files. It simply dumps data out into a format that is easy to work with in basic xEdit scripts for applying those changes.

While you _can_ save your work and pick up again where you left off, this tool also **DOES NOT** read ESP, ESM, or ESL files. It relies entirely on the files it generates for resuming work. This also means that you would have to generate the files it's expecting in the correct format in order to edit the location hierarchy from an existing mod that you'd never used this tool on before, where the mod already had some of its location hierarchy set up that you didn't want to lose.

# Tool capabilities

- Edit location hierarchies

- Edit location-cell associations

- Edit location keywords

# UI elements

- **Load locations** button is the starting point. It loads the location file and any previously saved data. You'll get error messages if those other files don't exist, but they're harmless.

- **Save hierarchy** button actually saves all work.

- **Load hierarchy** and **Load cell mappings** buttons are obsolete. They load the supporting data as individual commands, but the **Load locations** button does all of that now.

- **Set parent** sets the location selected in the location tree as the parent location for all of the locations selected in the flat location list.

- **Merge children** collapses a location hierarchy by moving all of the cell associations and keywords of child locations into the parent location selected in the location tree. It also removes the child locations from the hierarchy altogether. This can be a useful command, but it is also **very** destructive, so use it carefully.

- Right-clicking on a location in the location tree allows you to set keywords for the location. The **Add keyword** menu item pops up a window that lets you select one or more keywords to add. The other commands in the context menu, such as **City** and **City with inn** set multiple keywords that usually get set on a location of that type in a Skyrim mod.

- The **Cell mappings** text box is a list of cell editor IDs, one per line, whose location should be set to the currently selected location in the location tree.

- The text box above the **Flat location list** list filters that list to those locations that contain the filter text.

- The **Hide parented** checkbox is useful when starting with data that has no hierarchy at all yet. It hides locations from the flat location list if those locations have already been assigned a parent.

