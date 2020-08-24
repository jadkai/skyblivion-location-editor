{
	Script description: Sets keywords for a location

  This script is based loosely on Skyrim - Add keywords
}

// This is the unit name that will contain all the script functions
unit SkyblivionSetLocationKeywords;

// Global variables
var areFilesMismatched: boolean; // input files don't have the same numbers of entries
var KeywordFormIDs: TStringList; // list of strings that are each a list of keyword form IDs to assign to a location
var LocationEdids: TStringList; // the editor IDs of the locations whose keywords should be set
var UnsetLocs: TStringList; // locations that were processed but whose keywords were not set

// Called when the script starts
function Initialize : integer;
begin
  KeywordFormIDs := TStringList.Create;
  LocationEdids := TStringList.Create;
  UnsetLocs := TStringList.Create;

  {
    These two files could be combined into one, but I didn't bother with it,
    because I was able to achieve what I needed to without it.

    Line x in keywordFormIdsForSetKeywords.txt is a space-separated list of
    form IDs for keywords that should be assigned to the location at line x in
    locEdidsForSetKeywords.txt.
  }

  KeywordFormIDs.LoadFromFile(ProgramPath + 'Edit Scripts\keywordFormIdsForSetKeywords.txt');
  LocationEdids.LoadFromFile(ProgramPath + 'Edit Scripts\locEdidsForSetKeywords.txt');

  if KeywordFormIDs.Count = 0 then AddMessage('WARNING: No keywords!');
  if LocationEdids.Count = 0 then AddMessage('WARNING: No location names!');

  areFilesMismatched := KeywordFormIDs.Count <> LocationEdids.Count;

  if areFilesMismatched then AddMessage('WARNING: Mismatch in location EDIDs and keyword form IDs');
end;

// Called for each selected record in the TES5Edit tree
// If an entire plugin is selected then all records in the plugin will be processed
function Process(e : IInterface) : integer;
var
  kwda, k: IInterface;
  i, j: integer;
  exists: boolean;
  locEdid: string;
  keywordFormId: string;
  keywordFormIdListStr: string;
  locIndex: integer;
  loc: IInterface;
  keywordFormIdList: TStringList;
begin
  Result := 0;

  if areFilesMismatched then exit; // input files have different numbers of entries - probably an error

  if Signature(e) <> 'LCTN' then exit; // not a Location record

  locEdid := EditorID(e);

  if locEdid = '' then exit; // skip locations with no editor ID (shouldn't actually happen)

  AddMessage('Setting keywords for ' + locEdid);

  locIndex := LocationEdids.IndexOf(locEdid);

  if locIndex = -1 then begin
    AddMessage('WARNING: Couldn''t find entry for location with edid: ' + locEdid);
    UnsetLocs.Add(locEdid);
    exit;
  end;

  keywordFormIdListStr := KeywordFormIDs[locIndex];

  if keywordFormIdListStr = '' then begin
    AddMessage('WARNING: Couldn''t find keyword form ID for loc with edid: ' + locEdid);
    UnsetLocs.Add(locEdid);
    exit;
  end;

  // The next few lines of code turn a line in the input file that looks like:
  //
  // <FormID1> <FormID2> <FormID3>
  //
  // into an array like: [<FormID1>, <FormID2>, <FormID3>]

  keywordFormIdList := TStringList.Create;
  keywordFormIdList.Delimiter := ' ';
  keywordFormIdList.StrictDelimiter := true;
  keywordFormIdList.DelimitedText := keywordFormIdListStr;

  if keywordFormIdList.Count = 0 then begin
    AddMessage('No keywords for ' + locEdid);
    UnsetLocs.Add(locEdid);
    keywordFormIdList.Free;
    exit;
  end;

  // Attempt to get existing keyword set for Location
  kwda := ElementBySignature(e, 'KWDA');

  if not Assigned(kwda) then begin
    // Location has no existing keyword set - create one
    kwda := Add(e, 'KWDA', true);
  end;

  if not Assigned(kwda) then begin
    AddMessage('No keywords subrecord in ' + Name(e));
    Result := 1;
    UnsetLocs.Add(locEdid);
    keywordFormIdList.Free;
    exit;
  end;

  for i := 0 to keywordFormIdList.Count - 1 do begin
    keywordFormId := keywordFormIdList[i];

    AddMessage('Keyword: ' + keywordFormId);

    if keywordFormId = '' then continue;

    exists := false;

    for j := 0 to ElementCount(kwda) - 1 do begin
      if IntToHex(GetNativeValue(ElementByIndeX(kwda, j)), 8) = keywordFormId then begin
        AddMessage('Keyword ' + keywordFormId + ' already exists on ' + locEdid);
        exists := true;
        break;
      end;
    end;

    if exists then continue;

    {
      NOTE: This whole bit about "CK likes to save empty..." appeared in the
      "Skyrim - Add keywords.pas" file that this was based on, but in practice
      when this code was not commented out the first keyword in the set would
      not be correctly assigned to the Location. Without that code, everything
      worked fine.
    }

    // CK likes to save empty KWDA with only a single NULL form, use it if so
    // if ElementCount(kwda) = 1 and GetNativeValue(ElementByIndex(kwda, 0)) = 0 then begin
    //   AddMessage('Hit the weird special case');
    //   SetEditValue(ElementByIndex(kwda, 0), keywordFormId);
    // end else begin
      k := ElementAssign(kwda, HighInteger, nil, false);

      if not Assigned(k) then begin
        AddMessage('Can''t add keyword to ' + Name(e));
        UnsetLocs.Add(locEdid);
        keywordFormIdList.Free;
        exit;
      end;

      SetEditValue(k, keywordFormId);
    // end;

    AddMessage('KWDA element count is now ' + IntToStr(ElementCount(kwda)));
  end;

  // Update the number of keywords

  if not ElementExists(e, 'KSIZ') then begin
    Add(e, 'KSIZ', True);
  end;

  SetElementNativeValues(e, 'KSIZ', ElementCount(kwda));
  keywordFormIdList.Free;
end;

// Called after the script has finished processing every record
function Finalize : integer;
begin
  UnsetLocs.SaveToFile(ProgramPath + 'Edit Scripts\SkyblivionLocKeywordsNotSet.txt');
  UnsetLocs.Free;
  LocationEdids.Free;
  KeywordFormIDs.Free;
end;

end.
