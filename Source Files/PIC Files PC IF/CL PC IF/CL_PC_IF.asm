; Source code for CL System switch encoder.
; 
;Pulse timing assumes xtal at 3.579Mhz
 
;	Lap switches = xmit A,B,C
;	Start switch = xmit D
;	Idle = xmit X

;26/7/15 
; Tested OK
;4/1/16 Changed swtime to 7 (delay between switch presses = 7*0.1 = 0.7 seconds)
;                  ^^^^^^^^^^^^^^^^^^^^^^^^


   list      p=16f73           ; list directive to define processor
   #include <p16f73.inc>        ; processor specific variable definitions

   __CONFIG   _CP_OFF & _WDT_OFF & _PWRTE_ON & _XT_OSC


scratch	equ	0x20

; variables

swflag		equ	scratch+0               
sw1count	equ	scratch+1
sw2count    equ	scratch+2
sw3count	equ	scratch+3               
sw4count	equ scratch+4
var1		equ scratch+5
swtime      equ	scratch+6
;swtimeoff   equ	scratch+7
;flag1       equ	scratch+8
time2       equ	scratch+9
var2        equ scratch+0xd
inhibit     equ	scratch+0xe


; -----------------------                               

; constants
indf  		equ 0
fsr         equ 4
status     	equ 3              ; status register, also at 0x83
porta       equ 5
portb      	equ 6
portc       equ 7
trisa    	equ 0x5          ; port ddr - in bank 1 (=85)
trisb       equ 0x6
trisc       equ 0x7
tmr0        equ 1
opt         equ 1              ;TMR0 option reg - in bank 1 (=81)
tmr1l       equ 0x0e
tmr1h      	equ 0x0f
t1con      	equ 0x10
t2con      	equ 0x12
t2flag      equ 0x0c
tmr2        equ 0x11
pr2         equ 0x12        ; bank1 (=92)
adcon0   	equ 0x1f
adres      	equ 0x1e
txreg       equ 0x19
pir1        equ 0x0c
pclath     	equ 0x0a
rcsta       equ 0x18
rcreg       equ 0x1a
intcon     	equ 0x0b
pie1        equ 0x0c        ;bank1 8c
adcon1  	equ 0x9f

; Bit Definitions

rp0         equ 5		      ;register page
ferr        equ 2            ;rx framing error
oerr        equ 1            ; rx overun
cren        equ 4              ; uart rx en
rcif        equ 5              ; rvcd data flag

;inhit flag bits
inh1        equ 0
inh2        equ 1
inh3        equ 2
inh4        equ 3



;************************************************************

; port assignment
;
;               RB0 = Sw1 i/p 
;               RB1 = Sw2 i/p
;               RB2 = Sw3 i/p
;               RB3 = Sw4 i/p
;               RA0 = Led1 o/p
;               RA1 = Led2 o/p
;               RA2 = Led3 o/p
;               RA3 = Led4 o/p
;               RC1 = Pwr Led o/p		1 = on
;               RC6 = Tx Data
                        
;###########################################################################
; begin
		org          0
        goto        start
        org          10                            
start        

		clrf	porta			;clear port output latches
		clrf	portb
 		clrf	portc

		bsf		status,rp0		;bank1
		movlw   b'11111111'		; set port B as input
    	movwf   trisb
 		movlw    b'00000111'	
		movwf	adcon1			;enable port a outputs??     
		movlw	b'11110000'     ; set port A 0-3 as output
		movwf	trisa
        movlw	b'10111101'    ;set port C 1,6 as output
        movwf   trisc
        movlw	0xae
       	movwf	PR2            ;Preset TMR2 period for time = 50ms
        banksel 0				;set bank0

		movlw	b'00110001'		;timer 1, prescale 8, timer mode 
 		movwf	t1con
        movlw    b'01111110'    ;timer 2, prescale 16, postscale 16
        movwf    t2con

        clrf	sw1count
        clrf	sw2count
        clrf	sw3count
        clrf	sw4count
        clrf	swflag
        clrf	inhibit
        call	init_uart
		bsf		portc,1			;power led on
        call	test            ;cycle lamps on power up

        movlw   .07
        movwf   swtime          ;Set time delay between switch presses to 0.7 sec
 

; now done with initialising
        ; *** Main loop ***
main
   		movlw   0xd3
        movwf   tmr1h            ;preset timer count to $d39c (0.1 secs)
        movlw   0x00
        movwf   tmr1l
				
pollswitch              

       	btfsc   portb,0
   		goto    sw1             ;switch 1 is on
        bcf     inhibit,inh1    ;switch is off, allow next switch press to be processed at "sw1"

s2      btfsc   portb,1
        goto    sw2
        bcf     inhibit,inh2            

s3      btfsc   portb,2
        goto    sw3
        bcf     inhibit,inh3            

s4 		btfsc   portb,3
        goto    sw4         
        bcf     inhibit,inh4

s5      btfsc   pir1,1        ;test TMR2 flag
        call    SendX

        btfss   pir1,0        ;check if timer1 got to FFFF & set flag 
        goto    pollswitch

        bcf     pir1,0        ; every 0.1 secs check lamp status....
        tstf    sw1count          ;if any lamps are on, turn them off after 1 sec
        skpz
        call    lamp1off 		;if went to 0, turn the lamp off
        tstf    sw2count           ;if any lamps are on, turn them off after 1 sec
        skpz
        call    lamp2off 
        tstf    sw3count              
        skpz
        call    lamp3off 
        tstf    sw4count              
        skpz
        call    lamp4off 

        goto    main


sw1
        tstf    sw1count
        skpnz
        goto    sw1a
        bsf     inhibit,inh1           
        goto    s2              ;not 0, wait "swtime" x timer1 from last sw press
sw1a
        btfsc   inhibit,inh1          
        goto    s2              ;wait for switch release

        movfw   swtime
        movwf   sw1count        ;reset count (is decremented by timer1 in "pollswitch" loop
        movlw   b'00000001'     ;turn lamp on
        iorwf   porta,f
        movlw   a'A'
        call    xmit            ; transmit "A" for sw 1
        goto    s2
sw2
        tstf    sw2count
        skpnz
        goto    sw2a
        bsf     inhibit,inh2            
        goto    s3              ;not 0, wait "swtime" x timer1 from last sw press
sw2a
        btfsc   inhibit,inh2          
        goto    s3              ;wait for switch release                                       
        movfw   swtime
        movwf   sw2count        ;reset count (is decremented by timer1 in "pollswitch" loop
        movlw   b'00000010'     ;turn lamp on
        iorwf   porta,f
        movlw   a'B'
        call    xmit            ; transmit "B" for sw 2
        goto    s3

sw3
		tstf    sw3count
        skpnz
        goto    sw3a
        bsf     inhibit,inh3            
        goto    s4              ;not 0, wait "swtime" x timer1 from last sw press
sw3a    
 		btfsc	inhibit,inh3            
    	goto    s4              ;wait for switch release                                       

        movfw   swtime
        movwf   sw3count        ;reset count (is decremented by timer1 in "pollswitch" loop
        movlw   b'00000100'     ;turn lamp on
        iorwf   porta,f
        movlw   a'C'
        call    xmit            ; transmit "C" for sw 3
        goto    s4
sw4
        tstf    sw4count
        skpnz
        goto    sw4a
        bsf     inhibit,inh4            
        goto    s5              ;not 0, wait "swtime" x timer1 from last sw press
sw4a
        btfsc   inhibit,inh4            
        goto    s5              ;wait for switch release                                                       

        movfw   swtime
        movwf   sw4count        ;reset count (is decremented by timer1 in "pollswitch" loop
        movlw   b'0001000'      ;turn lamp on
        iorwf   porta,f
        movlw   a'D'
        call    xmit            ; transmit "D" for sw 4
        goto    s5


lamp1off                         ;lamp1 count was not 0
		decf	sw1count,f       ;decrement the switch count (if not 0) every 0.1 seconds
        skpz
        return                   ;stil not 0
        movlw   b'11111110'
        andwf   porta,f          ;clear porta bit 0 (lamp1)
        return

lamp2off                         ;lamp2 was not 0
        decf    sw2count,f       ;decrement the switch count (if not 0) every 0.1 seconds
        skpz
        return                   ;stil not 0
        movlw   b'11111101'
        andwf   porta,f          ;clear porta bit 1 (lamp2)
        return
lamp3off                         ;lamp3 was not 0
        decf    sw3count,f       ;decrement the switch count (if not 0) every 0.1 seconds
        skpz
        return                   ;stil not 0
        movlw   b'11111011'
        andwf   porta,f          ;clear porta bit 2 (lamp3)
        return
lamp4off                         ;lamp4 was not 0
        decf    sw4count,f       ;decrement the switch count (if not 0) every 0.1 seconds
        skpz
        return                   ;stil not 0
        movlw   b'11110111'
        andwf   porta,f          ;clear porta bit 3 (lamp4)
        return     

SendX
		bcf		pir1,1      	;clr tmr2 flag
        tstf    time2
        skpz
        goto    dectime2        ;time delay is TMR2 (50ms) x time2 value
        movlw   'X'
        call    xmit
        return     
dectime2
        decf    time2,f    
        return

;***************************

test
                ;cycle lamps on power up, 0.5 secs on 
		movlw    b'00110001'	;timer 1, prescale 8, timer mode 
        movwf    t1con                      ; 
        movlw    0x22
        movwf    tmr1h          ;preset timer count to $2200
        clrf     tmr1l

		movlw    b'00000001'
        movwf    porta          ;set lamp1 on
        call     wait_tmr1
		movlw    b'00000010'
        movwf    porta          ;set lamp2 on
        call     wait_tmr1

        movlw    b'00000100'
        movwf    porta          ;set lamp3 on
        call     wait_tmr1

        movlw    b'00001000'
        movwf    porta          ;set lamp4 on
        call     wait_tmr1       
        clrf     porta
        return

wait_tmr1
        btfss    pir1,0         ;wait for timer to count to ffff and set flag
        goto     wait_tmr1
        movlw    0x22
        movwf    tmr1h          ;preset timer count to $2200 = 0.5secs
        clrf     tmr1l
        bcf      pir1,0      
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

                ; write w to serial port
xmit         
		btfss	pir1,4          ;test for TXIF flag, wait if clr 
        goto    xmit         
        movwf   txreg
        clrf    tmr2            ;reset Timer2, wait for timeout before sending another "X"
        bcf     pir1,1          ;clr tmr2 flag
        movlw   .20             ;preset external count to 20 (20 x 50ms = 1 second)
        movwf   time2
        return

       end
;---------------------------------------------------------------------




