// Maze2.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"



#include <iostream>
#include <stack>
using namespace std;

bool isVaild( int x )
{
    return ( x >= 0 && 2 > x );
}

struct Node
{
    int x;
    int y;
    bool visit;
    int value;
    bool hasUp()
    {
        return ( isVaild( x - 1 ) && isVaild( y ) && matrix[ x - 1 ][ y ].visit == false && matrix[ x - 1 ][ y ].value == 0 );
    }
    bool hasDown()
    {
        return ( isVaild( x + 1 ) && isVaild( y ) && matrix[ x + 1 ][ y ].visit == false && matrix[ x + 1 ][ y ].value == 0 );
    }
    bool hasRight()
    {
        return ( isVaild( x ) && isVaild( y + 1 ) && matrix[ x ][ y + 1 ].visit == false && matrix[ x ][ y + 1 ].value == 0 );
    }
    bool hasLeft()
    {
        return ( isVaild( x  ) && isVaild( y - 1 ) && matrix[ x ][ y - 1 ].visit == false && matrix[ x ][ y - 1 ].value == 0 );
    }
/*    Node getLeft()
    {
        return matrix[ x - 1 ][ y ];
    }
    Node getRigth()
    {
        return matrix[ x + 1 ][ y ];
    }
    Node getUp()
    {
        return matrix[ x ][ y + 1 ];
    }
    Node getDown()
    {
        return matrix[ x ][ y - 1 ];
    }*/
}matrix[ 10 ][ 10 ];

int sum = 0;
int nearest = 100;
stack< Node > s;

void print( stack< Node > s, int endx, int endy )
{
    sum ++;
    cout << "ans " << sum << " :";
    stack< Node > t;
    while( !s.empty() )
    {
        t.push( s.top() );
        s.pop();
    }
    while( !t.empty() )
    {
        Node n = t.top();
        cout << "( " << n.x << ", " << n.y << " ) -> ";
        s.push( n );
        t.pop();
    }
    cout << "( " << endx << ", " << endy << " )";
    cout << endl;
}



void search( int startx, int starty, int endx, int endy )
{
    s.push( matrix[ startx ][ starty ] );

    while( !s.empty() )
   {
        if( s.top().hasUp() )
        {
            if( ( s.top().x - 1 == endx ) && ( s.top().y == endy ) )
            {
                print( s, endx, endy );
                s.pop();
            }
            else
            {
                matrix[ s.top().x - 1 ][ s.top().y ].visit = true; 
                s.push( matrix[ s.top().x - 1 ][ s.top().y ] );
            }
        }

        else if( s.top().hasDown() )
        {
            if( ( s.top().x + 1 == endx ) && ( s.top().y == endy ) )
            {
                print( s, endx, endy );
                s.pop();
            }
            else
            {
                matrix[ s.top().x + 1 ][ s.top().y ].visit = true; 
                s.push( matrix[ s.top().x + 1 ][ s.top().y ] );
            }
        }

        else if( s.top().hasLeft() )
        {
            if( ( s.top().x == endx ) && ( s.top().y - 1 == endy ) )
            {
                print( s, endx, endy );
                s.pop();
            }
            else
            {
                matrix[ s.top().x ][ s.top().y - 1 ].visit = true; 
                s.push( matrix[ s.top().x ][ s.top().y - 1 ] );
            }
        }

        else if( s.top().hasRight() )
        {
            if( ( s.top().x == endx ) && ( s.top().y + 1 == endy ) )
            {
                print( s, endx, endy );
                s.pop();
            }
            else
            {
                matrix[ s.top().x ][ s.top().y + 1 ].visit = true; 
                s.push( matrix[ s.top().x ][ s.top().y + 1 ] );
            }
        }

        else 
        {
            s.top().visit = false;
            s.pop();
        }

    }
}

int _tmain(int argc, _TCHAR* argv[])
{
    int i, j;
    for( i = 0; i < 3; i++ )
        for( j = 0; j < 3; j++ )
        {
            matrix[ i ][ j ].x = i;
            matrix[ i ][ j ].y = j;
            matrix[ i ][ j ].value = 0;
        }

    matrix[ 0 ][ 0 ].visit = true;
    search( 0, 0, 2, 2 );

    if( sum == 0 )
        cout << " no route " << endl;
	getchar();

    return 0;
}




