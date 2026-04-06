# CLTimer
![GitHub Created At](https://img.shields.io/github/created-at/verticalperformance/CLTimer?color=bright-green&style=flat-square)
![GitHub contributors](https://img.shields.io/github/contributors/verticalperformance/CLTimer?color=bright-green&style=flat-square)

Control line race timing project for the Whiteman Park, WA world champs control line facility.

## Table of Contents

- [Background](#background)
- [Compile and Install](#compile-and-install)
- [Usage](#usage)

## Background
CLTimer was written to support the Whiteman Park Control Line Racing facility built for the 2016 C/L World Championships in Perth, Western Australia.

The CLTimer project consists of three parts
- [User Interface Running on a Personal Computer](#ui)
- [Display Microcontroller](#display-microcontroller)
- [PC Interface Microcontroller](#pc-interface-microcontroller)
- [Radio Link](#radio-link)

## UI
The User Interface was written in Visual Basic 2010, and has since been re-compiled in 2019.

## Display Microcontroller
The display microcontroller is a Microchip PIC device. The code for this is compiled using Microchip MPLab, a programming adapter (PICKit 3) is required to reprogram the devices. Each display board has a set of programming header pins on the back side for this purpose.
## PC Interface Microcontroller
The PC interface microcontroller is a Microchip PIC device. The code for this is compiled using Microchip MPLab, a programming adapter (PICKit 3) is required to reprogram the devices and the device has to be removed from the board for reprogramming.
This micro handles the switches. It “debounces” the switch input so that what is sent to the computer does not have errors. For each valid switch pressed, a character is sent to the computer via an RS232 to USB adapter.
## Radio Link
The radio link to the display is a pair of Radiometrix modules running on 433 MHz. This is a low power, public use band that does not require a license. This setup has been used for RC pylon racing timing for many years with no issues. The transmitter only transmits when there is data to be sent. If no display data is currently being sent, a small burst is sent every 3 seconds just to keep the clock receiver decoder / receive LED running. The yellow LED the PC interface box is on when data is being sent.

## Compile and Install
The code is compiled into an executable program “CL Timer.exe”.
The source files are supplied in the folder \CL Timer. Run the CL Timer.sln (Solution file) to load the source code.
To compile, use Project, Publish. This will create an installation package. If some minor change is made, the program does not need to be installed again. The required CL Timer.exe application file can be found in the CL Timer\bin\Release folder. Just replace the currently used file with the new one. Move the original to a backup first!

## Usage
To be done...
