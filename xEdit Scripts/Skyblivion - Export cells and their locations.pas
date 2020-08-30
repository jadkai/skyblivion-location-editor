{
	Script description: Exports cells and their XLCNs
}

// This is the unit name that will contain all the script functions
unit SkyblivionExportCellsAndXlcns;

// Global variables
var ExportedCells: TStringList;

// Called when the script starts
function Initialize : integer;
begin
  ExportedCells := TStringList.Create;
end;

// Called for each selected record in the TES5Edit tree
// If an entire plugin is selected then all records in the plugin will be processed
function Process(e : IInterface) : integer;
var
  cellEdid: string;
  cellFormId: string;
  cellXlcn: string;
  container: IInterface;
  prevContainer: IInterface;
  recordThatContainsContainer: IInterface;
  foundParent: boolean;
  worldFormId: string;
begin

	if Signature(e) = 'CELL' then begin
    cellEdid := EditorID(e);
    cellFormId := FormID(e);

    if not IsWinningOverride(e) then begin
      AddMessage('Skipping ' + cellEdid + ' because not winning override');
      exit;
    end;

    foundParent := false;
    worldFormId := '';
    prevContainer := nil;

    while not foundParent do begin
      container := GetContainer(e);

      if Equals(container, prevContainer) then foundParent := true;
      if not Assigned(container) then foundParent := true;

      // "ChildrenOf" is the worst-named function ever. I think it returns
      // the PARENT record of the group you pass to it
      recordThatContainsContainer := ChildrenOf(container);

      if Signature(recordThatContainsContainer) = 'WRLD' then begin
        // CELL is in a worldspace

        worldFormId := FormID(recordThatContainsContainer);
        foundParent := true;
      end else if Signature(recordThatContainsContainer) = '' then begin
        // CELL is not in a worldspace (should mean it's an Interior)

        foundParent := true;
      end;
    end;

    if worldFormId = '' then begin
      worldFormId := 'Interior'
    end else begin
      worldFormId := IntToHex(worldFormId, 8);
    end;

    cellXlcn := GetElementEditValues(e, 'XLCN');

    if cellEdid = '' then exit;

    if not Assigned(cellXlcn) then begin
      cellXlcn := '<Unassigned>';
    end;

    ExportedCells.Add(cellEdid + ';' + IntToHex(cellFormId, 8) + ';' + worldFormId + ';' + cellXlcn);
  end;
end;

// Called after the script has finished processing every record
function Finalize : integer;
begin
  ExportedCells.SaveToFile(ProgramPath + 'Edit Scripts\cells with location form IDs.txt');
  ExportedCells.Free;
end;

end.
