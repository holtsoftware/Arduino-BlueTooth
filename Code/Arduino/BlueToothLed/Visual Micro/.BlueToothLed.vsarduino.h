#ifndef _VSARDUINO_H_
#define _VSARDUINO_H_
//Board = Arduino Uno
#define __AVR_ATmega328P__
#define ARDUINO 104
#define __AVR__
#define F_CPU 16000000L
#define __cplusplus
#define __attribute__(x)
#define __inline__
#define __asm__(x)
#define __extension__
#define __ATTR_PURE__
#define __ATTR_CONST__
#define __inline__
#define __asm__ 
#define __volatile__
#define __builtin_va_list
#define __builtin_va_start
#define __builtin_va_end
#define __DOXYGEN__
#define prog_void
#define PGM_VOID_P int
#define NOINLINE __attribute__((noinline))

typedef unsigned char byte;
extern "C" void __cxa_pure_virtual() {;}

LedCommand GetLedCommand(byte bytes[]);
//
//

#include "C:\arduino\hardware\arduino\variants\standard\pins_arduino.h" 
#include "C:\arduino\hardware\arduino\cores\arduino\arduino.h"
#include "S:\github\Arduino-BlueTooth\Code\Arduino\BlueToothLed\BlueToothLed.ino"
#include "S:\github\Arduino-BlueTooth\Code\Arduino\BlueToothLed\LedCommand.h"
#endif
