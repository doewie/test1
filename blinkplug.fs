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
include registers.fs
include ports.fs
include interrupt.fs
include version.fs

: SETUP ( delay -- )			\ delay = debouncing delay
	DEBOUNCE.DELAY !			\ set flag DEBOUNCE.DELAY
	PA4.SET						\ set PA4 input, pullup
	SETUP.INT					\ setup interrupt handling on PA4
;

: MAINLOOP ( -- ) 				\ endless loop until key pressed
	begin 
		EXTI4.FLAG @ 1 = if   	
			HANDLE.BUTTON		\ handle button if EXTI.FLAG set
		then								
		KEY? 					\ repeat until key pressed
	until
;

/ start with 'u main' u = debouncing delay
: MAIN ( delay -- )				\ delay = debouncing delay
	SETUP
	
	VER.           				\ show version
	
	CR	." Press key to stop..."
	CR
	
	MAINLOOP
;




