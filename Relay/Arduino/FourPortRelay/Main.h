#ifndef _MAIN_H_
#define _MAIN_H_

#include "CommandProcesser.h";

class Main
{
public:
	Main();

	void Init(void);
	void Loop(void);
private:
	CommandProcesser processer;
};

#endif

