#include "Common.h"
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
	mainClass.Loop();
}
