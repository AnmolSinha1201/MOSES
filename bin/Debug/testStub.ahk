#Include CLR.ahk


CLR_Start()

asm := CLR_LoadLibrary("MOSES.dll")
obj := CLR_CreateObject(asm, "MOSES.Runtime")

obj := ""
ExitApp

esc::ExitApp	