TARGET=TrainConvConnector.exe
OPTIONS= \
	/target:winexe \
	/optimize+ \
	/warn:4 \
	/codepage:65001

SOURCES= \
	TrainConvConnector.cs \
	UIText.cs \
	JapaneseUIText.cs \
	EnglishUIText.cs

$(TARGET): $(SOURCES)
	csc /out:$@ $(OPTIONS) $^
