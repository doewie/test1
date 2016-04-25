\ \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
\
\ interupt setting and handling
\
\ also re tutorial on http://embedded-lab.com/blog/stm32-external-interrupt/
\
\ \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ 

\ \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
\ NVIC tools


\ ----------------------------
\  Interrupt controller tools
\ ---------------------------
\ -
\ Nested Vector Interrupt Controller (NVIC)

$E000E100 constant NVIC				\ base

	NVIC $00 + constant NVIC.ISER0	\ Interrupt Set Enable ( # 00..31 )
	NVIC $04 + constant NVIC.ISER1	\ Interrupt Set Enable ( # 32..63 )

	NVIC $80 + constant NVIC.ICER0	\ Interrupt Clear Enable ( # 00..31 )
	NVIC $80 + constant NVIC.ICER1	\ Interrupt Clear Enable ( # 32..63 )
	

\ $E000E100 constant en0 ( Interrupt Set Enable  0-31  )
\ $E000E104 constant en1 ( Interrupt Set Enable 32-63  )
\ $E000E108 constant en2 ( Interrupt Set Enable 64-95  )
\ $E000E10C constant en3 ( Interrupt Set Enable 96-127 )

\ $E000E180 constant dis0 ( Interrupt Clear Enable  0-31  )
\ $E000E184 constant dis1 ( Interrupt Clear Enable 32-63  )
\ $E000E188 constant dis2 ( Interrupt Clear Enable 64-95  )
\ $E000E18C constant dis3 ( Interrupt Clear Enable 96-127 )


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


: HANDLE.INT	\ handling of the interrupt on EXTI1
	." x" CR	\ display x on every interrupt
	
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
	
	\ set vector of interrupt handling HANDLE.INT to irq-exti4
	['] HANDLE.INT irq-exti4 ! \ address of interrupthandling to vector table
	
	\ enable interrupt in the mask register
	4 bit EXTI.IMR bis!			\ set bit 4

	\ 10 nvic-enable			\ enable interrupt TO BE TESTED !!!!!!!
	10 bit NVIC.ISER0 bis! 		\ enable bit 10 in ISER
;