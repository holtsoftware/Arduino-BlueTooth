#ifndef _RGBLED_H_
#define _RGBLED_H_

#include <Arduino.h>

class RGBLed
{
public:
	RGBLed(uint8_t redPin, uint8_t greenPin, uint8_t bluePin);

	void Set_Value(int value);
	int Get_Value();

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
	uint8_t redPin;
	uint8_t greenPin;
	uint8_t bluePin;
};
#endif