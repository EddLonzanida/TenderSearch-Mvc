
@echo off
if exist _LayoutCreateContents.cshtml del _LayoutCreateContents.cshtml
if exist _LayoutDeleteContents.cshtml del _LayoutDeleteContents.cshtml
if exist _LayoutDetailsContents.cshtml del _LayoutDetailsContents.cshtml
if exist _LayoutEditContents.cshtml del _LayoutEditContents.cshtml
if exist _LayoutIndexContents.cshtml del _LayoutIndexContents.cshtml
if exist CreateWithParent.cshtml del CreateWithParent.cshtml
if exist IndexWithParent.cshtml del IndexWithParent.cshtml
copy /y xEdit.cshtml Edit.cshtml
copy /y xCreate.cshtml Create.cshtml
copy /y xDelete.cshtml Delete.cshtml
copy /y xDetails.cshtml Details.cshtml
copy /y xIndex.cshtml Index.cshtml