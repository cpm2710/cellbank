// Maze.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "stdafx.h"
#include <iostream>
#include "string.h"
#include "stdio.h"
using namespace std;


double dMeans=0,dWalkLen=10000; //dMeans表示走出迷宫的方法，dWalkLen表示当前走出迷宫最少步数            
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
};            //迷宫
int MazeFlag[10][51];  //迷宫的标志：0表示未走过，i(i=1,2,3,4)表示已经走过了,i表示方向。
int MazeMin[10][51];   //路径最小的迷宫的标志
void Walk(int nx,int ny); //走迷宫的函数，nx是列，ny是行
void PrintOut();         //打印路径及迷宫的函数，同时比较获取路径较短的行走方法
int  Judge(int nx,int ny,int i); //判断在第nx列ny行向第i个方向走是否可以,可以返回1否则返回0。
//i=1表示向右，2表示向下，3表示向左，4表示向上

void Walk(int nx,int ny)
{
	if (Maze[nx][ny]=='@') //判断是否走出迷宫，@是迷宫出口标志
		PrintOut();       //走出则打印出迷宫及行走路径
	else                  //未走出迷宫
	{
		for (int i=1; i<=4; i++) //四个方向逐个行走，i=1向右 2向下 3向左 4向上
		{
			if (Judge(nx,ny,i))  //如果列为nx行为ny的位置向i方向是否可以行走
			{                  
				MazeFlag[nx][ny]=i; //将标志位置i表明该位置向i方向可行走
				if (i==1)          //分散处理，根据不同的i来确定下一步的位置，以便行走。
					Walk(nx,ny+1);
				else if (i==2)
					Walk(nx+1,ny);
				else if (i==3)
					Walk(nx,ny-1);
				else if (i==4)
					Walk(nx-1,ny);
			}
		}
		MazeFlag[nx][ny]=0; //如果4个方向都走不通，或者回朔则清空该点标志位，置为0表明未走过。
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
			if (Maze[nx][ny]=='#') //＃表示墙
			{
				//cout<<"#";
			}
			else if (MazeFlag[nx][ny]==0) //不是墙但未走过的地方用空格表示
			{
				//cout<<" ";
			}
			else                 //不是墙且走过的地方用*表示
			{
				//cout<<".";
				dCount++;       //走一步总步数加1
			}
		}
		//cout<<endl;
	}
	cout<<"This way used "<<dCount<<" steps"<<endl;
	if (dCount<dWalkLen) //如果此种方法的步数比以前方法中最少步数还要少，
	{                   //则将此种方法列为当前最少行走步数
		for (nx=0;nx<10;nx++)
			for(ny=0;ny<51;ny++)
				MazeMin[nx][ny]=MazeFlag[nx][ny];
		dWalkLen=dCount;
	}
}


int Judge(int nx,int ny,int i)
{
	if (i==1) //判断向右可否行走
	{
		if (ny<50&&(Maze[nx][ny+1]==' '||Maze[nx][ny+1]=='@')&&MazeFlag[nx][ny+1]==0)
			return 1;
		else
			return 0;
	}
	else if (i==2) //判断向下可否行走
	{
		if (nx<9&&(Maze[nx+1][ny]==' '||Maze[nx+1][ny]=='@')&&MazeFlag[nx+1][ny]==0)
			return 1;
		else
			return 0;
	}
	else if (i==3) //判断向左可否行走
	{
		if (ny>0&&(Maze[nx][ny-1]==' '||Maze[nx][ny-1]=='@')&&MazeFlag[nx][ny-1]==0)
			return 1;
		else
			return 0;
	}
	else if (i==4) //判断向上可否行走
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
cout<<"迷宫游戏: "<<endl;

for (ni=0;ni<10;ni++) //输出迷宫形状，并且找到迷宫的入口，同时将迷宫标志初始化
{ 
	for(nj=0;nj<51;nj++)
	{
		cout<<Maze[ni][nj];
		MazeFlag[ni][nj]=0; //将迷宫标志初始化为0表示未走过
		if (Maze[ni][nj]=='%')
		{
			nx=ni; //迷宫入口列坐标
			ny=nj; //迷宫入口行坐标
		}
	}
	cout<<endl;
}

cout<<endl<<"入口坐标:"<<endl<<"nx= "<<nx<<"   "<<"ny= "<<ny<<endl; 
Walk(nx,ny); //调用行走迷宫函数，从入口处开始行走
cout<<endl<<"The MinLen way is: "<<endl;
for (nx=0;nx<10;nx++) //输出最短路径
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