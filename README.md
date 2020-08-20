# skyblivion-location-editor
Tool to help with quickly editing location hierarchies for a Skyrim mod.

**IMPORTANT**

This tool relies on data exported from xEdit in a specific format. Specifically, it requires a text file containing locations where each line in the file has the form ID and editor ID of a location in the following format:

`<Form ID>;<Editor ID>`

This tool **DOES NOT** edit ESP, ESM, or ESL files. It simply dumps data out into a format that is easy to work with in basic xEdit scripts for applying those changes.
