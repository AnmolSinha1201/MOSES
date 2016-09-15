#Include CLR.ahk


CLR_Start()

asm := CLR_LoadLibrary("MOSES.dll")

;~ obj := CLR_CreateObject(asm, "VSE.Interop")
;~ qwe := RegisterCallback("asd")
;~ obj.test(qwe)

obj := CLR_CreateObject(asm, "MOSES.Runtime")

obj := ""
ExitApp