CC=g++
all: syncher
syncher: syncherutil.o syncher.o
	$(CC) -o syncher syncher.o
syncher.o: syncher.cpp
	$(CC) -c syncher.cpp
syncherutil.o: syncherutil.cpp
	$(CC) -c syncherutil.cpp

