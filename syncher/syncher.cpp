#include <iostream>
#include <fstream>
#include "synchutil.cpp"
using namespace std;

int MAX_BUFFER_SIZE=4;
int main(){
synchutil util;
fstream data_to_synch("xxx.vdi");
data_to_synch.seekg(0,ios::end);
int size=data_to_synch.tellg();

int times=size/MAX_BUFFER_SIZE;

for(int i=0;i<times;i++){
//synch code here
}

printf("%d\n",size);
unsigned char *tmp=new unsigned char[MAX_BUFFER_SIZE];
 
util.md5(tmp,100);
return 1;
}
