#ifndef _COMMANDARGS_H_
#define _COMMANDARGS_H_

#define RESETBYTE 255

enum Major { Get = 0, Set = 1 };

class CommandArgs
{
public:
	CommandArgs();
	CommandArgs(Major, int, int, int);

	Major Get_Major();
	void Set_Major(Major);

	int Get_Minor();
	void Set_Minor(int);

	int Get_Value1();
	void Set_Value1(int);

	int Get_Value2();
	void Set_Value2(int);
private:
	Major majorCommand;
	int minorCommand;
	int value1;
	int value2;
};
#endif