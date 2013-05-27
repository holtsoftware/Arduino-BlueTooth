#ifndef _COMMON_H_
#define _COMMON_H_

#ifdef ARDUINO
#include <Arduino.h>
#include <stdint.h>

typedef int16_t Int16;
typedef int32_t Int32;
typedef int64_t Int64;

typedef uint16_t UInt16;
typedef uint32_t UInt32;
typedef uint64_t UInt64;
#define null NULL
#endif

#define Interface class



#endif