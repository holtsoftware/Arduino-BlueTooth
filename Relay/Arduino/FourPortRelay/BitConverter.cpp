#include "BitConverter.h"

byte* System::BitConverter::GetBytes(Int16 value)
{
	byte bytes[2];

	bytes[0] = (value & (255 << 8)) >> 8;
	bytes[1] = (value & 255);

	return bytes;
}

Int16 System::BitConverter::ToInt16(byte* value, Int16 startIndex = 0)
{
	if(value != null && sizeof(value) >= startIndex + 2)
	{
		return ((Int16)value[startIndex] << 8) | (Int16)value[startIndex + 1];
	}

	return -1;
}

