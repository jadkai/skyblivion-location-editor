# skyblivion-location-editor
Tool to help with quickly editing location hierarchies for a Skyrim mod.

**IMPORTANT**

This tool relies on data exported from xEdit in a specific format. Specifically, it requires a text file containing locations where each line in the file has the form ID and editor ID of a location in the following format (see Preparing data later in this README):

`<Form ID>;<Editor ID>`

This tool **DOES NOT** edit ESP, ESM, or ESL files. It simply dumps data out into a format that is easy to work with in basic xEdit scripts for applying those changes.

While you _can_ save your work and pick up again where you left off, this tool also **DOES NOT** read ESP, ESM, or ESL files. It relies entirely on the files it generates for resuming work. This also means that you would have to generate the files it's expecting in the correct format in order to edit the location hierarchy from an existing mod that you'd never used this tool on before, where the mod already had some of its location hierarchy set up that you didn't want to lose.

# Tool capabilities

- Edit location hierarchies

- Edit location-cell associations

- Edit location keywords

# Preparing data

## You've got cells but no locations

You've got a good bit of work to start with then.

1. Export the editor IDs of the cells and append Location to each of those.
2. Determine where you can reduce the set of locations by combining multiple cells into one location. Delete the locations you don't need from your file.
3. You'll probably also want to give names to those locations. Run `Skyblivion - Export names of prefiltered locations.pas` to help with that.
4. Once you have your location editor IDs and names created, use `Skyblivion - Create Locations.pas` to actually make the records.

## You've got locations but no hierarchy

1. Run the script `Skyblivion - Export location Form IDs and Editor IDs.pas`, which will put the location data in the correct format to be read.
2. Run the script `Skyblivion - Export keyword Form IDs and Editor IDs.pas` to get the keywords you need. Make sure you have selected the Keyword group in all plugins whose keywords you might want to use.

## You've already created some of the hierarchy or already have some keywords assigned

You'll need to create some scripts to export data from your ESP in the format that the editor utility in this repo expects. I don't have that documented, but the formats are generally pretty simple and should be easy enough to figure out by looking at the code. Unfortunately, there are no existing scripts for this part right now.

# UI elements

- **Load locations** button is the starting point. It loads the location file and any previously saved data. You'll get error messages if those other files don't exist, but they're harmless.

- **Save hierarchy** button actually saves all work.

- **Set parent** sets the location selected in the location tree as the parent location for all of the locations selected in the flat location list.

- **Merge children** collapses a location hierarchy by moving all of the cell associations and keywords of child locations into the parent location selected in the location tree. It also removes the child locations from the hierarchy altogether. This can be a useful command, but it is also **very** destructive, so use it carefully.

- Right-clicking on a location in the location tree allows you to set keywords for the location. The **Add keyword** menu item pops up a window that lets you select one or more keywords to add. The other commands in the context menu, such as **City** and **City with inn** set multiple keywords that usually get set on a location of that type in a Skyrim mod.

- Right-clicking in the flat location list allows you to set the keywords for multiple locations at once. **Note:** the keyword list box won't necessarily update with these new keywords if you don't have one of those locations selected in the location tree.

- Right-clicking in the keyword list allows you to add a keyword to the selected location in the location tree or to remove a keyword from the keyword list.

- The **Cell mappings** text box is a list of cell editor IDs, one per line, whose location should be set to the currently selected location in the location tree.

- The text box above the **Flat location list** list filters that list to those locations that contain the filter text.

- The **Hide locs with parents** checkbox is useful when starting with data that has no hierarchy at all yet. It hides locations from the flat location list if those locations have already been assigned a parent.

# xEdit scripts

There is a set of xEdit scripts included in this repository that are meant for taking some of the data generated by the location editor tool and actually applying it to a real ESP file.

Note that I was learning xEdit scripting while creating these, so it's very likely that these aren't written the best way that they could be, but they get the job done if you use them correctly.