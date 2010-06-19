#include <iostream>
#include <fstream>
#include <openssl/md5.h>

using namespace std;
class synchutil{
public: 
unsigned char* md5(unsigned char origin[],int size){
	printf("%d\n",MD5_DIGEST_LENGTH);
	MD5_CTX    ctx,ctx1;
	unsigned char *final=new unsigned char[MD5_DIGEST_LENGTH];
	MD5_Init(&ctx);
           //input: one hundred char array, each char has the letter 'A'
	MD5_Update(&ctx, origin,size);
	MD5_Final(final, &ctx);
/*	int count = 0;
	for( int i=0; i<MD5_DIGEST_LENGTH; i++)
	{
	printf( "%x", final[i] );
        if( (++count)%2 == 0 )
        printf( " " );
	}*/
	return final;
}
};
