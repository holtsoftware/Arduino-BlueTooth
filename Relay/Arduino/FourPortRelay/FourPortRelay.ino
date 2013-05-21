#include "Main.h"
#include <SoftwareSerial.h>
Main mainClass;

void setup()
{
	mainClass.Init();
}

void loop()
{
	mainClass.Loop();
}
