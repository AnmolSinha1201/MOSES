#Include, CLR.ahk

asm := CLR_LoadLibrary("MOSES.dll") 
obj := CLR_CreateObject(asm, "MOSES.Runtime", "testFile.txt")
obj.execute()