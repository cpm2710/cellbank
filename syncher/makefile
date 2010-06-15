CC=g++
all: syncher
syncher: syncher.o synchutil.o
	$(CC) -lcrypto syncher.o -o syncher
syncher.o: syncher.cpp
	$(CC)  -lcrypto -c -o syncher.o syncher.cpp
synchutil.o: synchutil.cpp
	$(CC) -lcrypto -c -o synchutil.o synchutil.cpp
