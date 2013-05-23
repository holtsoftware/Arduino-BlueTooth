#ifndef _RELAY_H_
#define _RELAY_H_

class Relay
{
public:
	Relay();

	bool Get_Relay1();
	void Set_Relay1(bool);

	bool Get_Relay2();
	void Set_Relay2(bool);
	
	bool Get_Relay3();
	void Set_Relay3(bool);

	bool Get_Relay4();
	void Set_Relay4(bool);

private:
	bool relay1;
	bool relay2;
	bool relay3;
	bool relay4;
};
#endif