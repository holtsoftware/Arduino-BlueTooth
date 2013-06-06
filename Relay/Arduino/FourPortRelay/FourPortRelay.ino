#include "Main.h"
#include <SoftwareSerial.h>
Sannel::Relay::Main mainClass;

void setup()
{
	Serial.begin(9600);
	Serial.println("setup");
	mainClass.Init();
}

void loop()
{
	Serial.print("loop ");
	Serial.println(millis());
	mainClass.Loop();
	delay(500);
}
