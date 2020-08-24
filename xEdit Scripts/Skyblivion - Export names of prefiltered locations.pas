{
	Script description: Exports names of the cells that correspond to locations
  in a filtered list.

  The idea here is that you don't want to export the names of ALL cells.
  Instead, you want to pare down the list of locations where you will be
  combining multiple cells into a single location.

  For example, you might have cells named:
  
  RolandJensericsHouse
  RolandJensericsHouseUpstairs
  RolandJensericsHouseBasement

  But you might only want to create:

  RolanJensericHouseLocation

  to correspond to all three.

  In that case only that location should appear in the input file.

  This is all based on the idea that you've created a set of location editor
  IDs that are just the editor IDs of a bunch of cells with "Location" added
  to the end (whether you've actually added those location editor IDs to your
  ESP is irrelevant), and you want to get the names of the cells corresponding
  to those overarching locations.
}

// This is the unit name that will contain all the script functions
unit SkyblivionExportPrefilteredLocNames;

// Global variables
var PrefilteredLocations: TStringList; // list of locations whose corresponding cell names you want
var CellNames: TStringList; // the names of the cells corresponding to the locations

// Called when the script starts
function Initialize : integer;
var
  i: integer;
  locRecord: IwbElement;
begin
  PrefilteredLocations := TStringList.Create;
  CellNames := TStringList.Create;
  
  PrefilteredLocations.LoadFromFile(ProgramPath + 'Edit Scripts\finalFilteredLocations.txt');

  if PrefilteredLocations.Count = 0 then AddMessage('WARNING: No locations!');
end;

// Called for each selected record in the TES5Edit tree
// If an entire plugin is selected then all records in the plugin will be processed
function Process(e : IInterface) : integer;
var
  cellEdid: string;
  cellName: string;
  locEdid: string;
  locRecord: IwbMainRecord;
  locIndex: integer;
begin
	if Signature(e) = 'CELL' then begin
    cellEdid := GetElementEditValues(e, 'EDID');

    if cellEdid = '' then exit;

    // This is seeing if the cell editor ID + "Location" is in the list that
    // you want to export.
    //
    // It would probably simpler to just list the cell editor IDs that you
    // want to export the names of, but it made sense to me at the time I was
    // actually writing this code.

    locEdid := cellEdid + 'Location';

    locIndex := PrefilteredLocations.IndexOf(locEdid);

    if locIndex <> -1 then begin
      cellName := GetElementEditValues(e, 'FULL');

      if cellName = '' then cellName := '<NoName>';

      // This ultimately exports a set of lines formatted as:
      //
      // <EDID of location>;<Name you should give to location>

      CellNames.Add(locEdid + ';' + cellName);
    end;
  end;
end;

// Called after the script has finished processing every record
function Finalize : integer;
var
  recordCount: integer;
begin
  CellNames.SaveToFile(ProgramPath + 'Edit Scripts\locsWithNames.txt');
  PrefilteredLocations.Free;
  CellNames.Free;
end;

end.
