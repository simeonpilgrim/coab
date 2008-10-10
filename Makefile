CSC=gmcs
FRAMEWORF_REFS = -r:System.dll
CLASSES_REFS = -r:Logging.dll $(FRAMEWORK_REFS)
ENGINE_REFS = -r:Classes.dll -r:Logging.dll $(FRAMEWORK_REFS)
LOGGING_REFS = $(FRAMEWORK_REFS)

classes_srcs = Classes/Action.cs Classes/Affect.cs Classes/Area1.cs Classes/Area2.cs \
	Classes/AssemblyInfo.cs Classes/Cheats.cs Classes/ClassStatsMin.cs Classes/DataIO.cs \
	Classes/DaxArray.cs Classes/DaxBlock.cs Classes/Display.cs Classes/EclBlock.cs \
	Classes/File.cs Classes/Gbl.cs Classes/HillsFarPlayer.cs Classes/Item.cs \
	Classes/ListBase.cs Classes/Opperation.cs Classes/Player.cs Classes/PoolRadPlayer.cs \
	Classes/Registers.cs Classes/RestTime.cs Classes/Set.cs Classes/SteppingPath.cs \
	Classes/StringList.cs Classes/Struct_189B4.cs Classes/Struct_19AEC.cs Classes/Struct_1A35E.cs \
	Classes/Struct_1ADF6.cs Classes/Struct_1B2CA.cs Classes/Struct_1C020.cs Classes/Struct_1D183.cs \
	Classes/Struct_1D1BC.cs Classes/Struct_1D530.cs Classes/Struct_1D885.cs Classes/Sys.cs    
	
engine_srcs = engine/AssemblyInfo.cs engine/ovr002.cs engine/ovr003.cs engine/ovr004.cs \
	engine/ovr005.cs engine/ovr006.cs engine/ovr007.cs engine/ovr008.cs engine/ovr009.cs \
	engine/ovr010.cs engine/ovr011.cs engine/ovr012.cs engine/ovr013.cs engine/ovr014.cs \
	engine/ovr015.cs engine/ovr016.cs engine/ovr017.cs engine/ovr018.cs engine/ovr019.cs \
	engine/ovr020.cs engine/ovr021.cs engine/ovr022.cs engine/ovr023.cs engine/ovr024.cs \
	engine/ovr025.cs engine/ovr026.cs engine/ovr027.cs engine/ovr028.cs engine/ovr029.cs \
	engine/ovr030.cs engine/ovr031.cs engine/ovr032.cs engine/ovr033.cs engine/ovr034.cs \
	engine/ovr038.cs engine/seg001.cs engine/seg037.cs engine/seg039.cs engine/seg040.cs \
	engine/seg041.cs engine/seg042.cs engine/seg043.cs engine/seg044.cs engine/seg046.cs \
	engine/seg049.cs engine/seg051.cs engine/VmOpp.cs
            
logging_srcs = Logging/Logging.cs

Classes.dll: $(classes_srcs) Logging.dll 
	$(CSC) $(DEFINES) -target:library -out:$@ $(classes_srcs) $(CLASSES_REFS)

Logging.dll: $(logging_srcs)
	$(CSC) $(DEFINES) -target:library -out:$@ $(logging_srcs) $(LOGGING_REFS)
	
Engine.dll: $(engine_srcs) Classes.dll Logging.dll
	$(CSC) $(DEFINES) -target:library -out:$@ $(engine_srcs) $(ENGINE_REFS)
