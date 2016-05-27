; source code for Control Line system displays
;    Displays all have same code and use id switches on start up to set location
;	There are 3 displays, each has 8 digits (board no's)
;Pulse timing assumes xtal at 3.579 MHz  

;History
;

;10/8/15 Test in debug looked OK - ready to test on hardware
;18/8 call to test_count on start displayed 0 - 8 OK (once h/w issues fixed)
;23/8 Set ID, display correct digit on start Ok.
;   Decode of 422 data in OK.  
;18/10 Changed data monitor to TMR1. Tested in debug, data fail at 4.8 secs
;20/10 tested data fail and fixed display dim OK
;    
;14/3/16 Tested OK
;               ^^^^^^^^^^^^^^^^^^^^^^^^


	list      p=16f73           ; list directive to define processor
	#include <p16F73.inc>        ; processor specific variable definitions

	__CONFIG   _CP_OFF & _WDT_OFF & _PWRTE_ON & _XT_OSC


scratch		equ	0x20

; variables

flag1		equ   	scratch+0x1
temp		equ		scratch+0x2
rx_data		equ		scratch+0x3	
fail		equ		scratch+0x4
var2		equ		scratch+0x05
disp_id		equ 	scratch+0x06
board_id	equ 	scratch+0x07
digit_id	equ 	scratch+0x08
curr_no		equ 	scratch+0x09
intens		equ		scratch+0x10
aux_off		equ		scratch+0x11
	

; constants
indf	equ 0
fsr		equ 4
status	equ 3	; status register, also at 0x83
porta	equ	5
portb	equ	6
portc	equ	7
trisa	equ	0x5	; port ddr - in bank 1 (=85)
trisb	equ	0x6
trisc	equ	0x7
tmr0	equ	1
opt		equ	1	;TMR0 option reg - in bank 1 (=81)
tmr1l	equ	0x0e
tmr1h	equ	0x0f
t1con	equ	0x10
t2con	equ	0x12
t2flag	equ	0x0c
tmr2	equ	0x11
pr2		equ	0x12	; bank1 (=92)
adcon0	equ	0x1f
adres	equ	0x1e
txreg	equ	0x19
pir1	equ	0x0c
pclath	equ	0x0a
txsta	equ 0x18		;bank1 =98
rcsta	equ	0x18
rcreg	equ	0x1a
intcon	equ	0x0b
pie1	equ	0x0c	;bank1 8c
adcon1	equ 0x9f
spbrg		equ 0x09	;bank1 {=99}
ccpr1l	equ 0x15
ccp1con	equ	0x17

; Bit Definitions


rp0		equ 5	;register page
ferr	equ	2	;rx framing error
oerr	equ	1	; rx overun
cren	equ	4	;  rx en
rcif	equ	5	; rvcd data flag
tmr2if	equ 1
tmr1if	equ 0	;tmr1 flag	
t0if	equ 2	;tmr0 flag

;flag1 bits
tim_fl	equ	0
id		equ	1
dim		equ 2	;port c, high = display off	
aux		equ 3	;flag that aux o/p is on. 
	
	

;************************************************************
; port assignment
;	RA0 = D1 Led
;	RA1 = D2 Led
;	RA2 = D3 Led
;	RA3 = D4 Led
;	RA4 = D5 Led
;	RA5 = D6 Led
;	RB0-3 = BCD value of display digit
;	RB4 = DP Display 0 = On
;	RB5 = Stat	0 = On
;	RB6 = aux	1 = On
;	RC0 = ID1
;	RC1 = ID2
;	RC3 = ID3
;	RC4 = ID4
;	RC5 = ID5
;	RC7 = Rx Data In

;###########################################################################
; begin
	org	0

	goto	start
	org 4
	goto do_irq

	org	10	
start
	clrf	flag1	
	clrf	fail
	banksel	0				;set bank 0
	clrf	porta			;clear port output latches
	clrf	portb
	clrf	portc	
    bsf		status,rp0 			;bank1
	movlw   b'00000000'		; set port B as output
   	movwf	trisb  
	movlw	b'00000000' 
	movwf	trisa			;set port A 0,2-5 to output
	movlw	b'00000111'
	movwf	adcon1			;TEMP - REQUIRED FOR A0-3 AS OUTPUT
	movlw	b'11111011'
	movwf	trisc			;set port C 2 as output
	banksel	0				;set bank0

	call	init_tmr21		;for .5 sec timer
    call	init_uart
	bsf		portb,4			;DP Leds off
	call	clr_disp
	call	tst_display
	bsf		portb,5			;stat led off

;--------------------------------------------------------
;	bsf		portb,6			;Aux O/P on
;	call 	wait05
;	bcf		portb,6
;*****************************************************************
	movlw	0x00			;preset display intensity to max
	movwf	intens
	call	set_id			;read the id switches

	call	init_tmr2		;for PWM Output	Currently set for 221Hz, 50% ?? see www.micro examples
	call	init_tmr1		;for no_data monitor


;18/10 set all off
;	bsf		intcon,7		;enable all interupts
;	bsf		intcon,6		;Peripheral int enable
;	bcf		pir1,rcif
;	bcf 	pir1,tmr2if

main
	btfsc 	pir1,tmr1if		;timer 1 flag
	call	no_data			;timer was not reset by valid data in
	btfsc	fail,4			;has fail been incremented up to 0x10? (4.8 Secs)
	call	data_fail
	btfsc	intcon,t0if		;TMR0 Flag, TMR started by aux output set on
	call	clr_aux
	btfsc	pir1,rcif		;check for data in buffer
	call	do_rcv			;recieved data at serial port
	goto 	main

do_rcv
	call	read_port 		;returns serial data in w #rx_data
	call	tst_char		;test the data in w and deal with it
	clrf	tmr1h			;received any char, reset the timer
	clrf	tmr1l
	clrf	fail			;reset Fail counter
	bcf		portb,5			;stat led on
	return

read_port
	btfsc	rcsta,ferr	;chk for framing error
	goto	rcv_error1
	btfsc	rcsta,oerr	;rcv buffer overun error
	goto	rcv_error2
	movfw	rcreg
	return

rcv_error1
	bcf	rcsta,cren	;disable rcv to clear error
	bsf	rcsta,cren
	movfw	rcreg
	bcf	porta,5
	return
rcv_error2
	bcf	rcsta,cren	;disable rcv to clear error
	bsf	rcsta,cren
	movfw	rcreg
	bcf	porta,5
	return

no_data
	incf	fail,f
	bcf 	pir1,tmr1if
	return

data_fail
	call	clr_disp
	bsf		portb,5		;led off
	clrf	fail
	return

clr_aux
	bcf		intcon,t0if
	decfsz	aux_off,f
	return
	bcf		portb,6		;set aux o/p off
	bcf		flag1,aux
	bsf		status,5
	movlw	b'11111111'		;tmr 0 off
	movwf	opt
	bcf		status,5	
	return

;***************************************************
tst_char
	btfsc	flag1,id
	goto	rcv_no		;have received board id (A/B/C), now do numbers

	movwf	var2
	comf	var2,f	
	incf	var2,f		;rcvd value = complement +1

cha	movlw	'A'			;Display 1
	addwf	var2,w		;  if =, Z will be set
	skpz
	goto	chb			;char is not A	
	btfss	disp_id,0	
	goto	chb			;Display is not #1
	bsf		flag1,id	;Set flag, this display for read digit in
	movlw	b'00000001'
	movwf	digit_id
	return

chb	movlw	'B'			;Display 2
	addwf	var2,w		;  if =, Z will be set
	skpz
	goto	chc			;char is not B	
	btfss	disp_id,1	
	goto	chc			;Display is not #2
	bsf		flag1,id	;Set flag, this display for read digit in
	movlw	b'00000001'
	movwf	digit_id
	return

chc	movlw	'C'			;Display 3
	addwf	var2,w		;  if =, Z will be set
	skpz
	goto	chx			;char is not C	
	btfss	disp_id,2	
	goto	chz			;Display is not #3
	bsf		flag1,id	;Set flag, this display for read digit in
	movlw	b'00000001'
	movwf	digit_id
	return

chx	movlw	'X'			;
	addwf	var2,w		
	skpz
	goto	chi			;char is not X	
;	bcf		portb,5		;Stat Led On	4/11 moved to do_rcv
	return

chi	movlw	'I'			;Increment Intensity
	addwf	var2,w		;  if =, Z will be set
	skpz
	goto	chd			;char is not C	
;check if already at max  (00)
	tstf	intens
	skpnz		
	return	

	movlw	0x20
	subwf	intens,f
	movfw	intens
	movwf	ccpr1l
	return

chd	movlw	'D'			;Decrement Intensity
	addwf	var2,w		;  if =, Z will be set
	skpz
	goto	chs			;char is not C	

;check if already min (C0)
	movlw	0xc0
	subwf	intens,w
	skpnz
	return

	movlw	0x20
	addwf	intens,f
	movfw	intens
	movwf	ccpr1l
	return

chs	movlw	'S'			;
	addwf	var2,w		
	skpz
	goto	chz			;char is not X	
	bsf		portb,6		;Aux O/P (Sounder) On
	bsf		flag1,aux	; set flag Aux is On, cleared by timer0
	call	init_tmr0	;for turning off Aux (sounder) after 1 sec
	movlw	0x10
	movwf	aux_off		;setup tmr0 x 10 = 1 second
	return

chz	movlw	'Z'
	addwf	var2,w		;  if =, Z will be set
	skpnz
	call	clr_disp
	return

rcv_no
	movwf	rx_data		;save it
	movfw	digit_id
	xorwf	board_id,w	;check if board id bits = current digit read in
	skpnz
	goto	disp_no
	clrc				;make sure carry is clear so it does't get rotated into digit_id
	rlf		digit_id,f	;shift digit id bit left for next char in
	return				;digit is not for this display

disp_no			
;display value in rx_data bits 0 - 3 (bcd value)
;	bcf		portb,5			;Stat Led On.4/11 delete, is in main loop.
	bcf		rx_data,4
	bcf		rx_data,5		;Clear ASCII high bits
	movfw	rx_data
	movwf	curr_no			;save current displayed no. - for corner led displays (disp_cnr)	
	movfw	portb
	andlw	0xf0			;Clear low bits
	xorwf	rx_data,w		;add display bits 0-3 to existing port values in bits 4-7
	movwf	portb			;display data to port
	bcf		portb,4			;DP leds on for any digit
	bcf		flag1,id	
	clrf	digit_id
	call	disp_cnr	
	return

clr_disp
	movfw	portb
	movwf	temp
	bsf		temp,0
	bsf		temp,1
	bsf		temp,2
	bsf		temp,3		;set bits 0-3 high (= display blank)
	bsf		temp,4		;DP bit
	movfw	temp		
	movwf	portb
	clrf	porta		;clear corner leds		
	return

disp_cnr
	;turn on the 6 additional display leds as required, use value in curr_no to work out correct combination
	tstf	curr_no
	skpnz
	goto	cnr0
	decf	curr_no,f
	tstf	curr_no
	skpnz
	goto	cnr1
	decf	curr_no,f
	tstf	curr_no
	skpnz
	goto	cnr2
	decf	curr_no,f
	tstf	curr_no
	skpnz
	goto	cnr3
	decf	curr_no,f
	tstf	curr_no
	skpnz
	goto	cnr4
	decf	curr_no,f
	tstf	curr_no
	skpnz
	goto	cnr5
	decf	curr_no,f
	tstf	curr_no
	skpnz
	goto	cnr6
	decf	curr_no,f
	tstf	curr_no
	skpnz
	goto	cnr7
	decf	curr_no,f
	tstf	curr_no
	skpnz
	goto	cnr8
	decf	curr_no,f
	tstf	curr_no
	skpnz
	goto	cnr9
	clrf	porta			;not 0 - 9 (is 0x0F) so all leds off
	return					

cnr0
	movlw	b'00111111'
	movwf	porta
	return
cnr1
	movlw	b'00000111'
	movwf	porta
	return
cnr2
	movlw	b'00111111'
	movwf	porta
	return
cnr3
	movlw	b'00111111'
	movwf	porta
	return
cnr4
	movlw	b'00110111'
	movwf	porta
	return
cnr5
	movlw	b'00111111'
	movwf	porta
	return
cnr6
	movlw	b'00111111'
	movwf	porta
	return
cnr7
	movlw	b'00100111'
	movwf	porta
	return
cnr8
	movlw	b'00111111'
	movwf	porta
	return
cnr9
	movlw	b'00110111'
	movwf	porta
	return


set_id
		;read id bits and set display id, board id
	movlw	01
	movwf	board_id		;preset to board 1
	movfw	portc
	movwf	temp
	bcf		temp,7
	bcf		temp,6
	bcf		temp,2			;clear the port bits not used here
	tstf	temp
	skpnz			
	goto	all_zero		;all zero, not valid
	btfsc	temp,1		
	goto	id1			
	movlw	b'00000001'		;id 01, so display = 1
	movwf	disp_id
	goto	set_board

id1	btfsc	temp,0
	goto	id2	
	movlw	b'00000010'		;id = x0, so display = 2
	movwf	disp_id
	goto	set_board

id2	movlw	b'00000100'		;set id to display 3
	movwf	disp_id
set_board
	bcf		temp,0
	bcf		temp,1
	rrf 	temp,f			;move id bits 3-5 to 0-3
	rrf		temp,f
	rrf		temp,f
nxt_id	
	tstf	temp	
	skpnz			
	goto	id_done			;	
	decf	temp,f
	rlf		board_id,f
	goto	nxt_id
id_done
	return

all_zero				;ID all zero
	call	test_count	;display 0 to 8
az1	bsf		portb,5		;then stop and flash stat led
	call	wait05
	bcf		portb,5
	call	wait05
	goto	az1


tst_display
	;Get board ID, dipslay corresponding number.
	;  eg if board ID = '00000100', display "3"
	movfw	portc
	movwf	temp
	movlw	b'00111000'
	andwf	temp,f			;clear other port bits
	rrf		temp,f
	rrf		temp,f
	rrf		temp,f			;move bits to 1 -3
	incf	temp,f			;add 1 so display 000 displays as "1" etc.
	movfw	temp
	movwf	rx_data
	call	disp_no
	call	wait05			;delay 1/2 sec
	call	clr_disp
	return


wait05	;0.5 sec delay => timer2 x value in temp
;	movlw	0x20	
	movlw	0x80		;temp changed
	movwf	temp
w1	call 	wait_t2
	decfsz 	temp,f
	goto	w1	
	return

wait_t2
	bcf		pir1,1	;clear flag  tmr2 = 65536uS (value of temp)
d1	btfss	pir1,1
	goto	d1
	bcf		pir1,1
	return

do_irq
	bsf		flag1,tim_fl	
	bcf		pir1,0		; clr int flag
	retfie	;return

init_tmr21
	movlw	b'01111110'		;prescale16, postscale 16, tmr on
	movwf	t2con			
	movlw	0x39			; = 14.6ms to TMR2IF flag
	bsf		status,5
	movwf	pr2
	bcf		status,5
	return

init_tmr2	;Timer used to dim display. Generates PWM out at RC2.Duty cycle is 4.5mS, Period set by CCPR1l value
	movlw	b'00000110'		;prescale16, tmr on
	movwf	t2con			
	movlw	0xff			; 
	bsf		status,5
	movwf	pr2
	bcf		status,5
;	movlw	b'01111101'
	movfw	intens
	movwf	ccpr1l
	movlw	b'00001100'
	movwf	ccp1con			;enable pwm ???
	return

init_tmr1
	movlw	b'00100001'		;tmr on, int clk, prescale 4
	movwf	t1con			;Time = 0.3 secs to TMR1IF
	return

init_tmr0
	clrf	tmr0			;clear counter
	bsf		status,5
	movlw	b'11000111'		;tmr en, prescale 256
	movwf	opt
	bcf		status,5
	return
init_uart
        bsf      status,5
        movlw    d'46'          ;set 1200 baud
        movwf    0x19                        
        movlw    b'00100000'
        movwf    0x18           ;enable uart xmit, 8bit, async              
        bcf      status,5
        movlw    b'10010000'
        movwf    0x18           ;enable rx/tx port
        return

test_count					;display 1 - 8
	call	wait05
	clrf	rx_data
	call	disp_no
	call	wait05
	incf	rx_data,f
	call	disp_no
	call	wait05
	incf	rx_data,f
	call	disp_no
	call	wait05
	incf	rx_data,f
	call	disp_no
	call	wait05
	incf	rx_data,f
	call	disp_no
	call	wait05
	incf	rx_data,f
	call	disp_no
	call	wait05
	incf	rx_data,f
	call	disp_no
	call	wait05
	incf	rx_data,f
	call	disp_no
	call	wait05
	incf	rx_data,f
	call	disp_no
	call	wait05
	call	clr_disp
	goto	test_count

	return	

	end


;**********************************************************************

