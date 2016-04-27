\ define version string
\ TODO: let git change the content of this file.

: VER. ( -- )  \ output version info
	CR
	CR ." name   : BlinkPlug"
	CR ." version: 0.3"
	CR
	CR	." debounce delay: "
		DEBOUNCE.DELAY @ .
		."  ms"
	CR
	CR
;