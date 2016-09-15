#Include CLR.ahk


CLR_Start()

asm := CLR_LoadLibrary("VSE.dll")

;~ obj := CLR_CreateObject(asm, "VSE.Interop")
;~ qwe := RegisterCallback("asd")
;~ obj.test(qwe)

obj := CLR_CreateObject(asm, "VSE.Class1")
obj.test("testFile.txt")

obj := ""
ExitApp


esc::ExitApp	

asd(asd, qwe)
{
	MsgBox, % asd "`n" StrGet(qwe, "UTF-16") "," qwe
}