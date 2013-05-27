#include "Relay.h"
#include <Arduino.h>

#define RELAY1_PIN		5
#define RELAY2_PIN		6
#define RELAY3_PIN		7
#define RELAY4_PIN		8

Relay::Relay() :
	relay1(false),
	relay2(false),
	relay3(false),
	relay4(false)
{
	pinMode(RELAY1_PIN, OUTPUT);
	pinMode(RELAY2_PIN, OUTPUT);
	pinMode(RELAY3_PIN, OUTPUT);
	pinMode(RELAY4_PIN, OUTPUT);
}

bool Relay::Get_Relay1()
{
	return relay1;
}

void Relay::Set_Relay1(bool on)
{
	relay1 = on;
	digitalWrite(RELAY1_PIN, (on)?HIGH:LOW);
}

bool Relay::Get_Relay2()
{
	return relay2;
}

void Relay::Set_Relay2(bool on)
{
	relay2 = on;
	digitalWrite(RELAY2_PIN, (on)?HIGH:LOW);
}

bool Relay::Get_Relay3()
{
	return relay3;
}

void Relay::Set_Relay3(bool on)
{
	relay3 = on;
	digitalWrite(RELAY3_PIN, (on)?HIGH:LOW);
}

bool Relay::Get_Relay4()
{
	return relay4;
}

void Relay::Set_Relay4(bool on)
{
	relay4 = on;
	digitalWrite(RELAY4_PIN, (on)?HIGH:LOW);
}