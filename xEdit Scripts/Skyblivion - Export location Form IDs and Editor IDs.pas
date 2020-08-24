{
	Script description: Creates locations form IDs and editor IDs
}

// This is the unit name that will contain all the script functions
unit SkyblivionExportLocFormIdEdid;

// Global variables
var LocExportStrs: TStringList;

// Called when the script starts
function Initialize : integer;
begin
  LocExportStrs := TStringList.Create;
end;

// Called for each selected record in the TES5Edit tree
// If an entire plugin is selected then all records in the plugin will be processed
function Process(e : IInterface) : integer;
var
  locEdid: string;
  locFormId: string;
begin

	if Signature(e) = 'LCTN' then begin
    // This was originally exporting the FixedFormID of the record, but to be
    // useful in the other scripts that this is meant to work with, it is
    // necessary to adjust them back to load-order form IDs, anyway

    locFormId := FormID(e);
    locEdid := EditorID(e);

    LocExportStrs.Add(IntToHex(locFormId, 8) + ';' + locEdid);
  end;
end;

// Called after the script has finished processing every record
function Finalize : integer;
begin
  LocExportStrs.SaveToFile(ProgramPath + 'Edit Scripts\locsWithFormIDs.txt');
  LocExportStrs.Free;
end;

end.
