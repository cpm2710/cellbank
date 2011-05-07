!include macros.def

BPACheck.exe:BPACheck.cs BPACheckFrame.cs
	csc $(COMPILE_FLAGS) /out:$@ BPACheck.cs BPACheckFrame.cs /r:$(REFERENCES)

clean:
	del *.exe

