{
	Script description: Creates locations with names
}

// This is the unit name that will contain all the script functions
unit SkyblivionCreateLocations;

// Global variables
var locEsp: IwbFile; // the ESP that's being edited
var locGrup: IwbGroupRecord; // the Location group that we're adding records to
var PrefilteredLocations: TStringList; // list of location EDIDs to add
var LocNames: TStringList; // list of names for the locations to add (must be same length as PrefilteredLocations)
var isValidFile: boolean; // we selected the file we actually wanted to edit
var areFilesMismatched: boolean; // the input files don't have the same number of entries

// Called when the script starts
function Initialize : integer;
begin
  PrefilteredLocations := TStringList.Create;
  LocNames := TStringList.Create;

  {
    locationEdids.txt and locationNames.txt are effectively parallel arrays.
    I wrote this script while I was still learning how to write them and
    didn't know a better way, but this should probably be updated to use the
    delimited strings capability of TStringList.
  }
  
  PrefilteredLocations.LoadFromFile(ProgramPath + 'Edit Scripts\locationEdids.txt');
  LocNames.LoadFromFile(ProgramPath + 'Edit Scripts\locationNames.txt');

  if PrefilteredLocations.Count = 0 then AddMessage('WARNING: No locations!');
  if LocNames.Count = 0 then AddMessage('WARNING: No location names!');

  areFilesMismatched := PrefilteredLocations.Count <> LocNames.Count;

  if areFilesMismatched then AddMessage('WARNING: Mismatch in loc EDIDs and names');

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

  // Assumes there's already a Location group in the file

  locGrup := GroupBySignature(locEsp, 'LCTN');

  if Assigned(locEsp) then AddMessage('selected file is ' + GetFileName(locEsp));
  if not Assigned(locEsp) then AddMessage('couldn''t get file');

  if Assigned(locGrup) then AddMessage('group was assigned');
  if not Assigned(locGrup) then AddMessage('group was not assigned');
end;

// Called for each selected record in the TES5Edit tree
// If an entire plugin is selected then all records in the plugin will be processed
function Process(e : IInterface) : integer;
var
  cellEdid: string;
  locEdid: string;
  locRecord: IwbMainRecord;
  locIndex: integer;
begin

  if not isValidFile then exit;
  if areFilesMismatched then exit;

  // This script just creates records, so it isn't really processing anything.
end;

// Called after the script has finished processing every record
function Finalize : integer;
var
  recordCount: integer;
  i: integer;
  locRecord: IwbElement;
begin
  AddMessage('Adding locations');

  if isValidFile and not areFilesMismatched then begin
    for i := 0 to Pred(PrefilteredLocations.Count) do begin
      if PrefilteredLocations[i] <> '' then begin
        locRecord := Add(locGrup, 'LCTN', True);

        if not Assigned(locRecord) then AddMessage('Failed to create LCTN record');

        SetElementEditValues(locRecord, 'EDID', PrefilteredLocations[i]);
        SetElementEditValues(locRecord, 'FULL', LocNames[i]);
      end;
    end;
  end;

  recordCount := ElementCount(locGrup);
  AddMessage('Done creating location records. There are now ' + IntToStr(recordCount) + ' records in group.');
  PrefilteredLocations.Free;
  LocNames.Free;
end;

end.
