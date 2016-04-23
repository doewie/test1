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

: setup ( -- )
	." Started. Press key to stop..."
;

: mainloop ( -- ) 			\ endless loop until key pressed
	begin 
		key? 
	until
;

: main ( -- )
	ver.           					\ show version
	setup
	mainloop						\ start main loop
;