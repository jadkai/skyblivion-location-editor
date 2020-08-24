{
	Script description: Sets parent locations for a set of locations.

  NOTE: This script assumes that all locations you've selected are either:
  a) in the target ESP
  b) not yet overridden in the target ESP but should be
}

// This is the unit name that will contain all the script functions
unit SkyblivionSetLocationParents;

// Global variables
var locEsp: IwbFile; // The file to edit
var isValidFile: boolean; // Whether this is the file we mean to editr
var areFilesMismatched: boolean; // The input files don't have the same number of entries
var ParentFormIDs: TStringList; // The Form IDs of the parent locations
var LocationEdids: TStringList; // The editor IDs of the locations whose parents should be set
var UnsetLocs: TStringList; // Processed location records whose parents weren't set

// Called when the script starts
function Initialize : integer;
begin
  ParentFormIDs := TStringList.Create;
  LocationEdids := TStringList.Create;
  UnsetLocs := TStringList.Create;

  {
    These two files are basically parallel arrays. There are better ways to
    do this, but I didn't know them at the time I wrote this script, and I
    haven't gone back to improve it.

    Overview: line x in parentsForSetParent.txt is the form ID of the Location
    that should be set as the PNAM value for the location whose editor ID is at
    line x of locsForSetParent.txt.
  }
  
  ParentFormIDs.LoadFromFile(ProgramPath + 'Edit Scripts\parentsForSetParent.txt');
  LocationEdids.LoadFromFile(ProgramPath + 'Edit Scripts\locsForSetParent.txt');

  if ParentFormIDs.Count = 0 then AddMessage('WARNING: No parent locations!');
  if LocationEdids.Count = 0 then AddMessage('WARNING: No location names!');

  areFilesMismatched := ParentFormIDs.Count <> LocationEdids.Count;

  if areFilesMismatched then AddMessage('WARNING: Mismatch in location EDIDs and parent form IDs');
  
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
  locEdid: string;
  parentLocFormId: string;
  locIndex: integer;
  loc: IInterface;
begin
  if not isValidFile then exit; // not editing the file we meant to
  if areFilesMismatched then exit; // input files have different numbers of entries - probably an error

  if Signature(e) <> 'LCTN' then exit; // not a Location record

  locEdid := EditorID(e);

  if locEdid = '' then exit; // skip locations with no editor ID (shouldn't actually happen)

  locIndex := LocationEdids.IndexOf(locEdid);

  if locIndex = -1 then begin
    AddMessage('WARNING: Couldn''t find entry for location with edid: ' + locEdid);
    UnsetLocs.Add(locEdid);
    exit;
  end;

  if GetFile(e) <> locEsp then begin
    // Location record that we're processing right now is not in target ESP.
    // Override it. Make sure to do a deep copy to avoid unintentional changes
    // to original record.
    loc := wbCopyElementToFile(e, locEsp, false, true); // false, true = copy as override, deep copy
  end else begin
    loc := e;
  end;

  if not Assigned(loc) then begin
    AddMessage('WARNING: Failed to copy location with edid: ' + locEdid);
    exit;
  end;

  parentLocFormId := ParentFormIDs[locIndex]; // parent Form ID is at the same index as location editor ID

  if parentLocFormId = '' then begin
    AddMessage('WARNING: Couldn''t find parent loc form ID for loc with edid: ' + locEdid);
    UnsetCells.Add(locEdid);
    exit;
  end;

  SetElementEditValues(loc, 'PNAM', parentLocFormId);
end;

// Called after the script has finished processing every record
function Finalize : integer;
begin
  UnsetLocs.SaveToFile(ProgramPath + 'Edit Scripts\SkyblivionLocParentsNotSet.txt');
  UnsetLocs.Free;
  LocationEdids.Free;
  ParentFormIDs.Free;
end;

end.
