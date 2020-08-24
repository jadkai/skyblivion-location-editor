{
	Script description: Sets the locations (XLCN field) for CELL records after
  copying the CELL record to the target ESP.

  NOTE: This script DOES NOT check to see if the record has already been
  copied to the target ESP. It assumes it HAS NOT.
}

// This is the unit name that will contain all the script functions
unit SkyblivionSetCellLocations;

// Global variables
var locEsp: IwbFile; // the file where the CELL overrides should go
var isValidFile: boolean; // whether we're editing the file we mean to
var areFilesMismatched: boolean; // if the input files don't have the same number of entries
var LocFormIDs: TStringList; // the form IDs to set as XLCN for each cell EDID
var CellEdids: TStringList; // the EDIDs of the cells to set the locations for
var UnsetCells: TStringList; // the cells from the processed set whose locations were not set

// Called when the script starts
function Initialize : integer;
begin
  LocFormIDs := TStringList.Create;
  CellEdids := TStringList.Create;
  UnsetCells := TStringList.Create;
  
  {
    These two files are basically parallel arrays. There are better ways to
    do this, but I didn't know them at the time I wrote this script, and I
    haven't gone back to improve it.

    Overview: line x in cellLocFormIdMapping.txt is the form ID of the Location
    that should be set as the XLCN value for the cell whose editor ID is at
    line x of cellListForLocMapping.txt.
  }

  LocFormIDs.LoadFromFile(ProgramPath + 'Edit Scripts\cellLocFormIdMapping.txt');
  CellEdids.LoadFromFile(ProgramPath + 'Edit Scripts\cellListForLocMapping.txt');

  if LocFormIDs.Count = 0 then AddMessage('WARNING: No locations!');
  if CellEdids.Count = 0 then AddMessage('WARNING: No location names!');

  areFilesMismatched := LocFormIDs.Count <> CellEdids.Count;

  if areFilesMismatched then AddMessage('WARNING: Mismatch in cell EDIDs and loc form IDs');
  
  {
    This is assuming that it's running for an ESP whose master is Skyblivion
    so that the file indices are:

    0 - Skyrim.esm
    1 - Skyrim.exe
    2 - Skyblivion.esm
    3 - The ESP you want to change with this script

    There's probably a better way to do this, but it got the job done, and I
    didn't feel like messing with it further.
  }

  locEsp := FileByIndex(3);

  if GetIsESM(locEsp) then begin
    isValidFile := false;
    exit;
  end else begin
    isValidFile := true;
  end;

  AddMessage('Working on file: ' + GetFileName(locEsp));

  if Assigned(locEsp) then AddMessage('selected file is ' + GetFileName(locEsp));
  if not Assigned(locEsp) then AddMessage('couldn''t get file');
end;

// Called for each selected record in the TES5Edit tree
// If an entire plugin is selected then all records in the plugin will be processed
function Process(e : IInterface) : integer;
var
  cellEdid: string; // editor ID of CELL record
  locFormId: string; // form ID to set as the XLCN for the CELL record
  cellIndex: integer; // index in the list of CELL records to edit of the cell with editor ID cellEdid
  copiedCell: IwbElement; // the record copied to the target ESP
begin
  if not isValidFile then exit; // we're not editing the file we want to
  if areFilesMismatched then exit; // input files don't have same number of entries - probably an error

  if Signature(e) <> 'CELL' then exit; // not a CELL record

  cellEdid := EditorID(e);

  if cellEdid = '' then exit; // skip cells with no editor ID

  cellIndex := CellEdids.IndexOf(cellEdid);

  if cellIndex = -1 then begin
    AddMessage('WARNING: Couldn''t find entry for cell with edid: ' + cellEdid);
    UnsetCells.Add(cellEdid);
    exit;
  end;

  // Copy CELL record to ESP.
  //
  // false, true = copy as override, deep copy
  //
  // If you don't deep copy then this will actually change values from the
  // original by overriding, which we don't want.
  
  copiedCell := wbCopyElementToFile(e, locEsp, false, true);

  if not Assigned(copiedCell) then begin
    AddMessage('WARNING: Failed to copy cell with edid: ' + cellEdid);
    exit;
  end;

  locFormId := LocFormIDs[cellIndex]; // form ID for XLCN should be at same index as cell editor ID was

  if locFormId = '' then begin
    AddMessage('WARNING: Couldn''t find loc form ID for cell with edid: ' + cellEdid);
    UnsetCells.Add(cellEdid);
    exit;
  end;

  SetElementEditValues(copiedCell, 'XLCN', locFormId);
end;

// Called after the script has finished processing every record
function Finalize : integer;
begin
  UnsetCells.SaveToFile(ProgramPath + 'Edit Scripts\SkyblivionCellLocsNotSet.txt');
  LocFormIDs.Free;
  UnsetCells.Free;
  CellEdids.Free;
end;

end.
