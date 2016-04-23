\ registers.fs
\
\ register definitions for the BlinkPlug project.
\
\

\ GPIOA registers

$40010800 constant GPIOA-BASE

	GPIOA-BASE $00 + constant GPIOA.CRL		\ reset $44444444	port Conf Register Low
	GPIOA-BASE $04 + constant GPIOA.CRH   	\ reset $44444444	port Conf Register High
	GPIOA-BASE $08 + constant GPIOA.IDR   	\ RO				Input Data Register
	GPIOA-BASE $0C + constant GPIOA.ODR   	\ reset 0			Output Data Register
	GPIOA-BASE $10 + constant GPIOA.BSRR  	\ reset 0			port Bit Set/Reset Reg
	GPIOA-BASE $14 + constant GPIOA.BRR   	\ reset 0			port Bit Reset Register


