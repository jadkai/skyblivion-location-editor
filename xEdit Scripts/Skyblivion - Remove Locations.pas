{
	Script description: Removes a set of locations from their files

  When running this, be careful to only select records from the file you want
  to edit, because it doesn't check what file the record is coming from.
}

// This is the unit name that will contain all the script functions
unit SkyblivionRemoveLocations;

// Global variables
var isValidFile: boolean; // this is the file we mean to edit
var LocationEdids: TStringList; // the editor IDs of the locations to remove

// Called when the script starts
function Initialize : integer;
begin
  LocationEdids := TStringList.Create;
  LocationEdids.LoadFromFile(ProgramPath + 'Edit Scripts\RemovedLocations.txt');

  if LocationEdids.Count = 0 then AddMessage('WARNING: No location names!');
end;

// Called for each selected record in the TES5Edit tree
// If an entire plugin is selected then all records in the plugin will be processed
function Process(e : IInterface) : integer;
var
  locEdid: string;
  locIndex: integer;
begin
  if not isValidFile then exit;

  if Signature(e) <> 'LCTN' then exit; // not a location record

  locEdid := EditorID(e);

  if locEdid = '' then exit; // skip locations with no editor ID (shouldn't actually happen)

  locIndex := LocationEdids.IndexOf(locEdid);

  if locIndex = -1 then exit; // location isn't in list of locations to remove

  AddMessage('Removing location ' + locEdid);

  Remove(e);
end;

// Called after the script has finished processing every record
function Finalize : integer;
begin
  LocationEdids.Free;
end;

end.
