
@echo off
del _LayoutCreateContents.cshtml
del _LayoutDeleteContents.cshtml
del _LayoutDetailsContents.cshtml
del _LayoutEditContents.cshtml
del _LayoutIndexContents.cshtml
copy /y xEdit.cshtml Edit.cshtml
copy /y xCreate.cshtml Create.cshtml
copy /y xDelete.cshtml Delete.cshtml
copy /y xDetails.cshtml Details.cshtml
copy /y xIndex.cshtml Index.cshtml