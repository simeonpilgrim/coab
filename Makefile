CSC=gmcs
FRAMEWORF_REFS = -r:System.dll
CLASSES_REFS = -r:Logging.dll -r:GDIPLUS.DLL $(FRAMEWORK_REFS)
LOGGING_REFS = $(FRAMEWORK_REFS)

classes_srcs = $(wildcard Classes/*.cs)
logging_srcs = $(wildcard Logging/*.cs)

Classes.dll: $(classes_srcs) Logging.dll 
	$(CSC) $(DEFINES) -target:library -out:$@ $(classes_srcs) $(CLASSES_REFS)

Logging.dll: $(logging_srcs)
	$(CSC) $(DEFINES) -target:library -out:$@ $(logging_srcs) $(LOGGING_REFS)
