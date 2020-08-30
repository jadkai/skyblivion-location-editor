{
	Script description: Exports worldspace form IDs, editor IDs, and locations
}

// This is the unit name that will contain all the script functions
unit SkyblivionExportWorldspaceIdsAndLocations;

// Global variables
var ExportedWorldspace: TStringList;

// Called when the script starts
function Initialize : integer;
begin
  ExportedWorldspace := TStringList.Create;
end;

// Called for each selected record in the TES5Edit tree
// If an entire plugin is selected then all records in the plugin will be processed
function Process(e : IInterface) : integer;
var
  worldEdid: string;
  worldFormId: string;
  worldLocation: string;
begin

	if Signature(e) = 'WRLD' then begin
    worldFormId := FormID(e);
    worldEdid := EditorID(e);

    if not IsWinningOverride(e) then begin
      AddMessage('Skipping ' + worldEdid + ' because not winning override');
      exit;
    end;

    worldLocation := GetElementEditValues(e, 'XLCN');

    ExportedWorldspace.Add(IntToHex(worldFormId, 8) + ';' + worldEdid + ';' + worldLocation);
  end;
end;

// Called after the script has finished processing every record
function Finalize : integer;
begin
  ExportedWorldspace.SaveToFile(ProgramPath + 'Edit Scripts\worldspace IDs and locations.txt');
  ExportedWorldspace.Free;
end;

end.
