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
include interrupt.fs

: SETUP ( -- )
	VER.           				\ show version
	PA4.SET						\ set PA4 input, pullup
	SETUP.INT					\ setup interrupt handling on PA4
	." Press key to stop..."
	CR
;

: MAINLOOP ( -- ) 				\ endless loop until key pressed
	begin 
		KEY? 					\ repeat until key pressed
	until
;

: MAIN ( -- )
	SETUP
	MAINLOOP
;




