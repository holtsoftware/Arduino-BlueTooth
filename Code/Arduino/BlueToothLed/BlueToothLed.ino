#include <SoftwareSerial.h>
#include <Servo.h>
#include "LedCommand.h"
#define INVALID_COMMAND 0
#define SET_COMMAND		1
#define GET_COMMAND		2

#define ALL_OBJECTS		0
#define LED_OBJECT		1
#define SERVO_OBJECT	2
#define RELAY_OBJECT	3

#define RED_LED			3
#define GREEN_LED		5
#define BLUE_LED		6
#define RELAY1			A0
#define RELAY2			A1
#define RELAY3			A2
#define RELAY4			A3

int redValue = 0;
int greenValue = 0;
int blueValue = 0;
bool relay1Value = false;
bool relay2Value = false;
bool relay3Value = false;
bool relay4Value = false;

typedef struct LedCommand LedCommand;

SoftwareSerial bluetoothSerial(10, 11);
Servo moter;

LedCommand GetLedCommand(byte bytes[])
{
	LedCommand cmd;
	cmd.Command = INVALID_COMMAND;

	cmd.Command = bytes[0];
	cmd.Object = bytes[1];
	cmd.Index = bytes[2];
	cmd.Value = bytes[3];


	return cmd;
}

void setup()
{
	pinMode(RED_LED, OUTPUT);
	pinMode(GREEN_LED, OUTPUT);
	pinMode(BLUE_LED, OUTPUT);
	pinMode(RELAY1, OUTPUT);
	pinMode(RELAY2, OUTPUT);
	pinMode(RELAY3, OUTPUT);
	pinMode(RELAY4, OUTPUT);
	
	digitalWrite(RELAY1, HIGH);
	digitalWrite(RELAY2, HIGH);
	digitalWrite(RELAY3, HIGH);
	digitalWrite(RELAY4, HIGH);
	
	moter.attach(9);
	Serial.begin(115200);
	bluetoothSerial.begin(9600);
	/*Serial.println("test");
	while(!bluetoothSerial.available())
	{
	}
	bluetoothSerial.write("$$$\r");
	bluetoothSerial.write("D\r");
	bluetoothSerial.write("---\r");
	*/

}

bool ready = false;
byte bytes[4];
int index = 0;

void loop()
{
	/*while(bluetoothSerial.available())
	{
	Serial.write(bluetoothSerial.read());
	}
	return;*/
	if(bluetoothSerial.available())
	{
		while(bluetoothSerial.available())
		{
			if(index > 3)
			{
				index = 0;
			}

			bytes[index] = bluetoothSerial.read();
			index++;
			if(index >= 4)
			{
				ready = true;
				break;
			}
		}


		if(ready)
		{
			if(Serial.available())
			{
				Serial.print(bytes[0]);
				Serial.print(',');
				Serial.print(bytes[1]);
				Serial.print(',');
				Serial.print(bytes[2]);
				Serial.print(',');
				Serial.println(bytes[3]);
			}

			if(bytes[0] == SET_COMMAND && bytes[1] == LED_OBJECT)
			{
				if(bytes[3] >= 0 && bytes[3] <= 255 && bytes[2] >= 0 && bytes[2] <= 2)
				{
					switch(bytes[2])
					{
					case 0:
						redValue = bytes[3];
						analogWrite(RED_LED, redValue);
						break;

					case 1:
						greenValue = bytes[3];
						analogWrite(GREEN_LED, greenValue);
						break;

					case 2:
						blueValue = bytes[3];
						analogWrite(BLUE_LED, blueValue);
					}
				}
			}
			/*else if(bytes[0] == SET_COMMAND && bytes[1] == SERVO_OBJECT)
			{
				if(bytes[2] == 1)
				{
					moter.writeMicroseconds(bytes[3] * 10);
				}
			}*/
			else if(bytes[0] == SET_COMMAND && bytes[1] == RELAY_OBJECT)
			{
				switch(bytes[2])
				{
				case 0:
					relay1Value = (bool)bytes[3];
					digitalWrite(RELAY1, (relay1Value)?LOW:HIGH);
					break;

				case 1:
					relay2Value = (bool)bytes[3];
					digitalWrite(RELAY2, (relay2Value)?LOW:HIGH);
					break;

				case 2:
					relay3Value = (bool)bytes[3];
					digitalWrite(RELAY3, (relay3Value)?LOW:HIGH);
					break;

				case 3:
					relay4Value = (bool)bytes[3];
					digitalWrite(RELAY4, (relay4Value)?LOW:HIGH);
					break;
				}
			}
			else if(bytes[0] == GET_COMMAND && bytes[1] == ALL_OBJECTS)
			{
				bluetoothSerial.print("LED,0,");
				bluetoothSerial.print(redValue);
				bluetoothSerial.print("\r\n");
				bluetoothSerial.print("LED,1,");
				bluetoothSerial.print(greenValue);
				bluetoothSerial.print("\r\n");
				bluetoothSerial.print("LED,2,");
				bluetoothSerial.print(blueValue);
				bluetoothSerial.print("\r\n");
				bluetoothSerial.print("RELAY,0,");
				bluetoothSerial.print(relay1Value);
				bluetoothSerial.print("\r\n");
				bluetoothSerial.print("RELAY,1,");
				bluetoothSerial.print(relay2Value);
				bluetoothSerial.print("\r\n");
				bluetoothSerial.print("RELAY,2,");
				bluetoothSerial.print(relay3Value);
				bluetoothSerial.print("\r\n");
				bluetoothSerial.print("RELAY,3,");
				bluetoothSerial.print(relay4Value);
				bluetoothSerial.print("\r\n");
			}
			delay(1);
			ready = false;
		}
	}

	/*for(int i=0;i<255;i++)
	{
	analogWrite(RED_LED, i);
	analogWrite(GREEN_LED, i);
	analogWrite(BLUE_LED, i);
	delay(50);
	}*/
}
