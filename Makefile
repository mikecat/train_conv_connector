TARGET=TrainConvConnector.exe
OPTIONS= \
	/target:winexe \
	/optimize+ \
	/warn:4 \
	/codepage:65001 \
	/win32icon:dent.ico \
	/reference:TrainCrewInput.dll

SOURCES= \
	AssemblyInfo.cs \
	TrainConvConnector.cs \
	UIText.cs \
	JapaneseUIText.cs \
	EnglishUIText.cs \
	BeaconWindow.cs \
	DenConvCommunicator.cs

$(TARGET): $(SOURCES)
	csc /out:$@ $(OPTIONS) $^
