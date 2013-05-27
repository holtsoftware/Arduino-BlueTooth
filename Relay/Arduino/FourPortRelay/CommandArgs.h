#ifndef _COMMANDARGS_H_
#define _COMMANDARGS_H_

#include "Common.h"

enum Major { Get = 0, Set = 1 };

class CommandArgs
{
public:
	CommandArgs();
	CommandArgs(Major, Int16, Int16, Int16);

	Major Get_Major();
	void Set_Major(Major);

	Int16 Get_Minor();
	void Set_Minor(Int16);

	Int16 Get_Value1();
	void Set_Value1(Int16);

	Int16 Get_Value2();
	void Set_Value2(Int16);
private:
	Major majorCommand;
	Int16 minorCommand;
	Int16 value1;
	Int16 value2;
};
#endif