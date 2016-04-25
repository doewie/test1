\ registers.fs
\
\ register definitions for the BlinkPlug project.
\

\ General Purpose I/O register (GPIO)

$40010800 constant GPIOA-BASE

	GPIOA-BASE $00 + constant GPIOA.CRL		\ reset $44444444	port Conf Register Low
	GPIOA-BASE $04 + constant GPIOA.CRH   	\ reset $44444444	port Conf Register High
	GPIOA-BASE $08 + constant GPIOA.IDR   	\ RO				Input Data Register
	GPIOA-BASE $0C + constant GPIOA.ODR   	\ reset 0			Output Data Register
	GPIOA-BASE $10 + constant GPIOA.BSRR  	\ reset 0			port Bit Set/Reset Reg
	GPIOA-BASE $14 + constant GPIOA.BRR   	\ reset 0			port Bit Reset Register

\ Alternate Functions I/O register (AFIO)

\	$40010000 constant AFIO 				\ Already defined in flib\hal-stm32f1.fs

	AFIO $08 + constant AFIO.EXTICR1		\ External Interrupt Configuration Register 1 for EXTI0 .. EXTI3
	AFIO $0C + constant AFIO.EXTICR2		\ External Interrupt Configuration Register 1 for EXTI4 .. EXTI7
	AFIO $10 + constant AFIO.EXTICR3		\ External Interrupt Configuration Register 1 for EXTI8 .. EXTI11
	AFIO $14 + constant AFIO.EXTICR4		\ External Interrupt Configuration Register 1 for EXTI2 .. EXTI15

\ Reset and Clock Control register (RCC)

\	$40021000 constant RCC					\ Already defined in flib\hal-stm32f1.fs

	RCC $00 + constant RCC.CR				\ Clock Control Register
	RCC $04 + constant RCC.CFGR				\ Clock Configuration Register
	RCC $10 + constant RCC.APB1RSTR			\ APB1 Peripheral Reset Register
	RCC $14 + constant RCC.AHBENR			\ AHB Peripheral Clock Enable Register
	RCC $18 + constant RCC.APB2ENR			\ AHB2 Peripheral Clock Enable Register
	RCC $1C + constant RCC.APB1ENR			\ AHB1 Peripheral Clock Enable Register
 
\ EXTernal Interrupt/event register (EXTI)

	$40010400 constant EXTI-BASE
	
	EXTI-BASE $00 + constant EXTI.IMR		\ Interrupt Mask Register
	EXTI-BASE $04 + constant EXTI.EMR		\ Event Mask Register
	EXTI-BASE $08 + constant EXTI.RTSR		\ Rising Trigger Selection Register
	EXTI-BASE $0C + constant EXTI.FTSR		\ Falling Trigger Selection Register
	EXTI-BASE $10 + constant EXTI.SWIER		\ Software Interrupt Event Register	
	EXTI-BASE $14 + constant EXTI.PR		\ Pending Register
	
\ Nested Vector Interrupt Controller (NVIC)

	$E000E100 constant NVIC					\ base

	NVIC $00 + constant NVIC.ISER0			\ Interrupt Set Enable ( # 00..31 )
	NVIC $04 + constant NVIC.ISER1			\ Interrupt Set Enable ( # 32..63 )
	NVIC $80 + constant NVIC.ICER0			\ Interrupt Clear Enable ( # 00..31 )
	NVIC $80 + constant NVIC.ICER1			\ Interrupt Clear Enable ( # 32..63 )
	

