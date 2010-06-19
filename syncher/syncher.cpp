#include <iostream>
#include "synchutil.cpp"
using namespace std;
int main(){
synchutil util;
char *buf=new char[100];
util.md5(buf,100);
return 1;
}
