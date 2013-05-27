#ifndef _RGBLED_H_
#define _RGBLED_H_

#include "Common.h"

class RGBLed
{
public:
	RGBLed(byte redPin, byte greenPin, byte bluePin);

	void Set_Value(Int32 value);
	Int32 Get_Value();

	byte Get_Red();
	void Set_Red(byte value);

	byte Get_Green();
	void Set_Green(byte value);

	byte Get_Blue();
	void Set_Blue(byte value);

private:
	byte red;
	byte green;
	byte blue;
	byte redPin;
	byte greenPin;
	byte bluePin;
};
#endif