#include "Buttons4.h"

#define BUTTON1_PIN		A0
#define BUTTON2_PIN		A1
#define BUTTON3_PIN		A2
#define BUTTON4_PIN		A3

using namespace Sannel::Relay;

Buttons4::Buttons4(Relay4Port *relay) :
	button1Last(0),
	button2Last(0),
	button3Last(0),
	button4Last(0)
{
	this->relay = relay;
	pinMode(BUTTON1_PIN, INPUT);
	pinMode(BUTTON2_PIN, INPUT);
	pinMode(BUTTON3_PIN, INPUT);
	pinMode(BUTTON4_PIN, INPUT);
}

void Buttons4::CheckButtons()
{
	Int16 value = digitalRead(BUTTON1_PIN);
	if(value == 0 && button1Last == 1)
	{
		relay->Set_Relay1(!relay->Get_Relay1());
		button1Last = 0;
	}
	else if(value == 1 && button1Last == 0)
	{
		button1Last = 1;
	}

	value = digitalRead(BUTTON2_PIN);
	if(value == 0 && button2Last == 1)
	{
		relay->Set_Relay2(!relay->Get_Relay2());
		button2Last = 0;
	}
	else if(value == 1 && button2Last == 0)
	{
		button2Last = 1;
	}

	value = digitalRead(BUTTON3_PIN);
	if(value == 0 && button3Last == 1)
	{
		relay->Set_Relay3(!relay->Get_Relay3());
		button3Last = 0;
	}
	else if(value == 1 && button3Last == 0)
	{
		button3Last = 1;
	}

	value = digitalRead(BUTTON4_PIN);
	if(value == 0 && button4Last == 1)
	{
		relay->Set_Relay4(!relay->Get_Relay4());
		button4Last = 0;
	}
	else if(value == 1 && button4Last == 0)
	{
		button4Last = 1;
	}
}