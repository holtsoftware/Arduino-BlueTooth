#ifndef _CONVERT_H_
#define _CONVERT_H_

#include <Arduino.h>
#include <stdint.h>
#include "Common.h"

namespace System
{
	class BitConverter
	{
	public:
		static byte* GetBytes(Int16 value);

		static Int16 ToInt16(byte* value, Int16 startIndex);
	};
}
#endif