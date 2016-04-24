\ development code
\ based on the 'ztw' project of JeeLabs
\ The STM32F103 is preloaded with the h/l files of this project

\ test-app for the JeeLabs BlinkPlug.
\ the BlinkPlug is connected as follows:
\ green LED to port
\ red LED to port

\ also re: 
\	http://jeelabs.net/projects/hardware/wiki/Blink_Plug
\	

\ remove ram-compiled stuff and reset Forth system
reset

\ include application specific source files
include version.fs
include registers.fs
include ports.fs

: SETUP ( -- )
	VER.           				\ show version
	PA4.SET						\ set PA4 input, pullup
	." Press key to stop..."
	CR
;

: MAINLOOP ( -- ) 				\ endless loop until key pressed
	begin 
		1000 MS					\ wait 1 second
		PA4@					\ get inputvalue on PA4
		. CR					\ report input value on PA4
		KEY? 					\ repeat until key pressed
	until
;

: MAIN ( -- )
	SETUP
	MAINLOOP
;




