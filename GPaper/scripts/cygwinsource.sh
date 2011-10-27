#!/bin/bash
JAVA_HOME="/cygdrive/d/JavaTools/jdk1.6.0_24_X86"
SDBROOT="/cygdrive/d/WorkSpace/Orips3WorkSpace/playground/sdb1.3.4"
SDB_USER="root"
SDB_PASSWORD="root"
CLASSPATH=".;$JAVA_HOME/jre/lib/rt.jar"
SDB_JDBC="/cygdrive/d/WorkSpace/Orips3WorkSpace/playground/mysql/mysql-connector-java-5.1.7-bin.jar"

for f in `ls $SDBROOT/lib/*.jar`
do
	#echo $f	
	CLASSPATH="$f;""$CLASSPATH"
	#echo $CLASSPATH
done
SDBROOT="$(cygpath -w $SDBROOT)"
SDB_JDBC="$(cygpath -w $SDB_JDBC)"
CLASSPATH="$(cygpath -w $CLASSPATH)"

export CLASSPATH
export JAVA_HOME
export SDBROOT
export SDB_USER
export SDB_PASSWORD
export SDB_JDBC
#echo $CLASSPATH
export PATH="$SDBROOT/bin:""$PATH"

