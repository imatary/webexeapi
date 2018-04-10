As with all SmallBasic extensions, copy the extension dll and xml to a lib sub-folder of the SmallBasic installation folder, usually in one of the following locations:

C:\Program Files\Microsoft\Small Basic\lib
C:\Program Files (x86)\Microsoft\Small Basic\lib

You may require to create the lib folder if it doesn't exist.

The required lib folder files are:

LitDev.dll
LitDev.xml
LitDev.De.xml (optional German intellisense)

Copy ALL other files (documentation and samples) elsewhere.

If the extension commands fail to appear in Small Basic (type LD and no options appear) then you may need to unblock the downloaded dll.  Right click LitDev.dll and select Properties and clear the block if present.  If it fails to unblock, then copy LitDev.dll to a folder where you have write access (e.g. Documents), unblock it there and then move back to the Small Basic lib folder.  Alternatively, unblock the downloaded zip file before unzipping the content.

