// Maze.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "stdafx.h"
#include <iostream>
#include "string.h"
#include "stdio.h"
using namespace std;


double dMeans=0,dWalkLen=10000; //dMeans��ʾ�߳��Թ��ķ�����dWalkLen��ʾ��ǰ�߳��Թ����ٲ���            
char Maze[10][52]={
	{"###################################################"},
	{"% ## ####           ###                  ### # ####"},
	{"# ##  # ###  ### ###### ### ############ #   #    #"},
	{"# ## ## ###  ##  ##     # # ## #           #   ####"},
	{"# #    # ## ##  ### #          # ######### # # # ##"},
	{"#      # #   # ##     ########## ####   ##   #    #"},
	{"# ## ### ## ## ### #### ##    ##    # # ######### #"},
	{"# #  #      ## ##       #  ##  #### # #  ##    ####"},
	{"####   ## ####    ####    ##  # ###   ##    ##    @"},
	{"###################################################"},
};            //�Թ�
int MazeFlag[10][51];  //�Թ��ı�־��0��ʾδ�߹���i(i=1,2,3,4)��ʾ�Ѿ��߹���,i��ʾ����
int MazeMin[10][51];   //·����С���Թ��ı�־
void Walk(int nx,int ny); //���Թ��ĺ�����nx���У�ny����
void PrintOut();         //��ӡ·�����Թ��ĺ�����ͬʱ�Ƚϻ�ȡ·���϶̵����߷���
int  Judge(int nx,int ny,int i); //�ж��ڵ�nx��ny�����i���������Ƿ����,���Է���1���򷵻�0��
//i=1��ʾ���ң�2��ʾ���£�3��ʾ����4��ʾ����

void Walk(int nx,int ny)
{
	if (Maze[nx][ny]=='@') //�ж��Ƿ��߳��Թ���@���Թ����ڱ�־
		PrintOut();       //�߳����ӡ���Թ�������·��
	else                  //δ�߳��Թ�
	{
		for (int i=1; i<=4; i++) //�ĸ�����������ߣ�i=1���� 2���� 3���� 4����
		{
			if (Judge(nx,ny,i))  //�����Ϊnx��Ϊny��λ����i�����Ƿ��������
			{                  
				MazeFlag[nx][ny]=i; //����־λ��i������λ����i���������
				if (i==1)          //��ɢ�������ݲ�ͬ��i��ȷ����һ����λ�ã��Ա����ߡ�
					Walk(nx,ny+1);
				else if (i==2)
					Walk(nx+1,ny);
				else if (i==3)
					Walk(nx,ny-1);
				else if (i==4)
					Walk(nx-1,ny);
			}
		}
		MazeFlag[nx][ny]=0; //���4�������߲�ͨ�����߻�˷����ոõ��־λ����Ϊ0����δ�߹���
	}
}


void PrintOut()
{
	int nx,ny;
	double dCount=0;
	dMeans++;
	cout<<"The "<<dMeans<<" ways is: "<<endl;

	for (nx=0;nx<10;nx++)
	{
		for (ny=0;ny<51;ny++)
		{
			if (Maze[nx][ny]=='#') //����ʾǽ
			{
				//cout<<"#";
			}
			else if (MazeFlag[nx][ny]==0) //����ǽ��δ�߹��ĵط��ÿո��ʾ
			{
				//cout<<" ";
			}
			else                 //����ǽ���߹��ĵط���*��ʾ
			{
				//cout<<".";
				dCount++;       //��һ���ܲ�����1
			}
		}
		//cout<<endl;
	}
	cout<<"This way used "<<dCount<<" steps"<<endl;
	if (dCount<dWalkLen) //������ַ����Ĳ�������ǰ���������ٲ�����Ҫ�٣�
	{                   //�򽫴��ַ�����Ϊ��ǰ�������߲���
		for (nx=0;nx<10;nx++)
			for(ny=0;ny<51;ny++)
				MazeMin[nx][ny]=MazeFlag[nx][ny];
		dWalkLen=dCount;
	}
}


int Judge(int nx,int ny,int i)
{
	if (i==1) //�ж����ҿɷ�����
	{
		if (ny<50&&(Maze[nx][ny+1]==' '||Maze[nx][ny+1]=='@')&&MazeFlag[nx][ny+1]==0)
			return 1;
		else
			return 0;
	}
	else if (i==2) //�ж����¿ɷ�����
	{
		if (nx<9&&(Maze[nx+1][ny]==' '||Maze[nx+1][ny]=='@')&&MazeFlag[nx+1][ny]==0)
			return 1;
		else
			return 0;
	}
	else if (i==3) //�ж�����ɷ�����
	{
		if (ny>0&&(Maze[nx][ny-1]==' '||Maze[nx][ny-1]=='@')&&MazeFlag[nx][ny-1]==0)
			return 1;
		else
			return 0;
	}
	else if (i==4) //�ж����Ͽɷ�����
	{
		if (nx>0&&(Maze[nx-1][ny]==' '||Maze[nx-1][ny]=='@')&&MazeFlag[nx-1][ny]==0)
			return 1;
		else
			return 0;
	}
	else
		return 0;
}

int _tmain(int argc, _TCHAR* argv[])
{int nx,ny,ni,nj;
cout<<"�Թ���Ϸ: "<<endl;

for (ni=0;ni<10;ni++) //����Թ���״�������ҵ��Թ�����ڣ�ͬʱ���Թ���־��ʼ��
{ 
	for(nj=0;nj<51;nj++)
	{
		cout<<Maze[ni][nj];
		MazeFlag[ni][nj]=0; //���Թ���־��ʼ��Ϊ0��ʾδ�߹�
		if (Maze[ni][nj]=='%')
		{
			nx=ni; //�Թ����������
			ny=nj; //�Թ����������
		}
	}
	cout<<endl;
}

cout<<endl<<"�������:"<<endl<<"nx= "<<nx<<"   "<<"ny= "<<ny<<endl; 
Walk(nx,ny); //���������Թ�����������ڴ���ʼ����
cout<<endl<<"The MinLen way is: "<<endl;
for (nx=0;nx<10;nx++) //������·��
{
	for (ny=0;ny<51;ny++)
	{
		if (Maze[nx][ny]=='#')
			cout<<"#";
		else if (MazeMin[nx][ny]==0)
			cout<<" ";
		else
		{
			cout<<".";
		}
	}
	cout<<endl;
	
}
getchar();
return 0;
}