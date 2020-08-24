{
	Script description: Exports keyword form IDs and editor IDs
}

// This is the unit name that will contain all the script functions
unit SkyblivionExportKeywordFormIdEdid;

// Global variables
var KeywordExportStrs: TStringList;

// Called when the script starts
function Initialize : integer;
begin
  KeywordExportStrs := TStringList.Create;
end;

// Called for each selected record in the TES5Edit tree
// If an entire plugin is selected then all records in the plugin will be processed
function Process(e : IInterface) : integer;
var
  keywordEdid: string;
  keywordFormId: string;
begin

	if Signature(e) = 'KYWD' then begin
    keywordFormId := FormID(e);
    keywordEdid := EditorID(e);

    KeywordExportStrs.Add(IntToHex(keywordFormId, 8) + ';' + keywordEdid);
  end;
end;

// Called after the script has finished processing every record
function Finalize : integer;
begin
  KeywordExportStrs.SaveToFile(ProgramPath + 'Edit Scripts\keywordsWithFormIDs.txt');
  KeywordExportStrs.Free;
end;

end.
