#include "RGBLed.h"
#include <Arduino.h>

RGBLed::RGBLed(uint8_t redPin, uint8_t greenPin, uint8_t bluePin) :
	red(0),
	green(0),
	blue(0)
{
	this->redPin = redPin;
	this->greenPin = greenPin;
	this->bluePin = bluePin;

	pinMode(redPin, OUTPUT);
	pinMode(greenPin, OUTPUT);
	pinMode(bluePin, OUTPUT);
	analogWrite(redPin, 0);
	analogWrite(greenPin, 0);
	analogWrite(bluePin, 0);
}

void RGBLed::Set_Value(int value)
{
	this->red = (byte)((value & (255 << 16)) >> 16);
	this->green = (byte)((value & (255 << 8)) >> 8);
	this->blue = (byte)value & 255;

	analogWrite(redPin, red);
	analogWrite(greenPin, green);
	analogWrite(bluePin, blue);
}

int RGBLed::Get_Value()
{
	return (this->red << 16) | (this->green << 8) | this->blue;
}

byte RGBLed::Get_Red()
{
	return this->red;
}

void RGBLed::Set_Red(byte red)
{
	this->red = red;
	analogWrite(this->redPin, this->red);
}

byte RGBLed::Get_Green()
{
	return this->green;
}

void RGBLed::Set_Green(byte green)
{
	this->green = green;
	analogWrite(this->greenPin, this->green);
}

byte RGBLed::Get_Blue()
{
	return this->blue;
}

void RGBLed::Set_Blue(byte blue)
{
	this->blue = blue;
	analogWrite(this->bluePin, this->blue);
}