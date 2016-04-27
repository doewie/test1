\ interrupt.fs
\
\ interupt setting and handling for project BlinkPlug
\
\

\ : nvic-enable ( irq# -- )
\   16 - \ Cortex Core Vectors
\   dup 32 u< if      1 swap lshift NVIC.ISER0 ! exit then
\   dup 64 u< if 32 - 1 swap lshift en1 ! exit then
\   dup 96 u< if 64 - 1 swap lshift en2 ! exit then
\                96 - 1 swap lshift en3 !
\ ;

\ : nvic-disable ( irq# -- )
\   16 - \ Cortex Core Vectors
\   dup 32 u< if      1 swap lshift dis0 ! exit then
\   dup 64 u< if 32 - 1 swap lshift dis1 ! exit then
\   dup 96 u< if 64 - 1 swap lshift dis2 ! exit then
\                96 - 1 swap lshift dis3 !
\ ;
 
\ : nvic-priority ( priority irq# -- )
\   $E000E400 + c!
\ ;

0 VARIABLE EXTI4.FLAG			\ will be set on interrupt EXTI4
0 VARIABLE COUNT.INT        	\ count # of interrupts
0 VARIABLE COUNT.HANDLING   	\ count # of interrupt handlings
0 VARIABLE DEBOUNCE.DELAY		\ delay for debounce wait

: HANDLE.BUTTON					\ handling of the interrupt on EXTI1
	COUNT.HANDLING @ 1+			\ incr. COUNT.HANDLING
	COUNT.HANDLING !
	
	COUNT.HANDLING @ 10 >= if	\ print and reset counters every 10 handlings
		COUNT.INT @ .
		."  - "
		COUNT.HANDLING @ .
		CR
		0 COUNT.INT !
		0 COUNT.HANDLING !
	then
	
	DEBOUNCE.DELAY @ ms			\ wait for debounce button
	0 EXTI4.FLAG !				\ reset the interrupt flag after handling
;

: EXTI4.HANDLE ( -- )
	COUNT.INT @ 1+				\ incr. COUNT.INT
	COUNT.INT !
	1 EXTI4.FLAG !				\ set flag for interrupt handling

	\ set bit 10 in EXTI.PR to re-enable interrupt no. 10 ( EXTI1 )
	\ this is the position # in table 63, page 203 of the STM32F1 manual. 
	\ for interrupt # 10
	10 bit EXTI.PR bis!	
;

: SETUP.INT \ setup handling of interrupt on PA4

	\ enable peripherals for AFIO 
	0 bit RCC.APB2ENR bis!	\ set bit 0 (= AFIOEN)				
				
	\ EXTICR2 connect a port to an external interrupt #
	\ PA4 to External Interrupt #4
	\ connect external interupt 4 (EXTI2, bits 0..3) to PA (0x0000) 	
	\ reset value = $0, so default all connected to PA[x]
		
	\ Select falling edge interrupt on EXTI4	
	4 bit EXTI.FTSR bis!		\ set bit 4 (= TR4 )
	
	\ set vector of interrupt handling HANDLE.EXTI4 to irq-exti4
	['] EXTI4.HANDLE irq-exti4 ! \ address of interrupthandling to vector table
	
	\ enable interrupt in the mask register
	4 bit EXTI.IMR bis!			\ set bit 4

	\ 10 nvic-enable			\ enable interrupt TO BE TESTED !!!!!!!
	10 bit NVIC.ISER0 bis! 		\ enable bit 10 in ISER
;