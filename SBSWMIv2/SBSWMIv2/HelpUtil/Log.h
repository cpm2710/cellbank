#include "stdafx.h"
using namespace std;
class Log{
public:
	Log(){
	}
	static void LogMessage(string a);
	static void LogMessage(const wchar_t *a);
};