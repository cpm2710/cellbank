#include <iostream>
#include "synchutil.cpp"
using namespace std;
int main(){
cout<<"abcdefg"<<endl;
synchutil util;
char *buf=new char[100];
util.md5(buf);
return 1;
}
