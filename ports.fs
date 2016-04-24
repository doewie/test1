\ ports.fs
\
\ port handling for project BlinkPlug
\
\ needs registers.fs
\
\
\ interesting articles about port settings:
\ http://www.scriptoriumdesigns.com/embedded/gpio_in.php
\
\

: PA4.SET ( -- ) \ set PA4 to input with pullup
	\ PA4 set in GPIOA.CRL
	\ PA4 MODE4=%00 (input) in bits 16-17, CNF4=%10 (pullup-down) in bits 18-19
	
	16 bit GPIOA.CRL bic!	\ reset bit 16
	17 bit GPIOA.CRL bic!	\ reset bit 17
	18 bit GPIOA.CRL bic!	\ reset bit 18
	19 bit GPIOA.CRL bis!	\ set bit 19
	
	\ enable pullup by setting bit 4 ( ODR4 ) in GPIOA.ODR
	\ set via the GPIOA.BSRR register to use atomic (re)setting
	4 bit GPIOA.BSRR bis!	\ set bit 4
;

: PA4. ( -- ) \ report input value of PA4
	GPIOA.IDR @	\ read Input Data Register (IDR)
	4 bit AND	\ isolate bit 4
	4 rshift	\ right shift to bit 0
	. CR		\ print result
; 