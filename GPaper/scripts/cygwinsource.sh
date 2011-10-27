#!/bin/bash
SDBROOT="/cygdrive/d/WorkSpace/Orips3WorkSpace/playground/sdb1.3.4"
SDB_USER="root"
SDB_PASSWORD="root"
CLASSPATH=".:$JAVA_HOME/jre/lib.rt.jar"
SDB_JDBC="/cygdrive/d/WorkSpace/Orips3WorkSpace/playground/mysql/mysql-connector-java-5.1.7-bin.jar"
JAVA_HOME="/cygdrive/d/JavaTools/jdk1.6.0_24_X86"
export SDBROOT
export SDB_USER
export SDB_PASSWORD
export SDB_JDBC
for f in `ls $SDBROOT/lib/*.jar`
do
	#echo $f	
	CLASSPATH="$f:""$CLASSPATH"
	#echo $CLASSPATH
done

export CLASSPATH
#echo $CLASSPATH
export PATH="$SDBROOT/bin:""$PATH"

