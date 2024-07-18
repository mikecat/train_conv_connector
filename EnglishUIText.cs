class EnglishUIText: UIText
{
	public override string DenConvStatus { get { return "DenConv Status"; }}
	public override string DenConvPowerName { get { return "Power"; }}
	public override string DenConvBrakeName { get { return "Brake"; }}
	public override string DenConvBrakeRelease { get { return "Release"; }}
	public override string DenConvATCName { get { return "ATC"; }}
	public override string DenConvATCNoticeName { get { return "ATC Notice"; }}

	public override string TrainCrewStatus { get { return "TRAIN CREW Status"; }}
	public override string TrainCrewPowerName { get { return "Power"; }}
	public override string TrainCrewBrakeName { get { return "Brake"; }}
	public override string TrainCrewDoorCloseName { get { return "Door Closed"; }}
	public override string TrainCrewDoorCloseTrue { get { return "Yes"; }}
	public override string TrainCrewDoorCloseFalse { get { return "No"; }}
	public override string TrainCrewCarModelName { get { return "Car Model"; }}
	public override string TrainCrewSpeedName { get { return "Speed"; }}
	public override string TrainCrewSpeedLimitName { get { return "Speet Limit"; }}
	public override string TrainCrewSpeedLimitNoticeName { get { return "Limit Notice"; }}
	public override string TrainCrewATSName { get { return "ATS"; }}
	public override string TrainCrewInGameName { get { return "Playing?"; }}
	public override string TrainCrewInGameTrue { get { return "Yes"; }}
	public override string TrainCrewInGameFalse { get { return "No"; }}
	public override string TrainCrewBCPressureName { get { return "BC Pressure"; }}
	public override string TrainCrewDistanceName { get { return "Distance"; }}

	public override string OperationMode { get { return "Operation Mode"; }}
	public override string OperationModeAutoName { get { return "Auto"; }}
	public override string OperationMode4000Name { get { return "4000 / 4000R"; }}
	public override string OperationMode3020Name { get { return "3020"; }}
	public override string OperationModeOtherName { get { return "Other"; }}
	public override string ShinkansenTweakName { get { return "Tweak for Shinkansen"; }}

	public override string ConfigFor4000 { get { return "Configuration for 4000 / 4000R"; }}
	public override string ConfigFor4000NoHolding { get { return "B1～B7→B1～B7, B8→B7"; }}
	public override string ConfigFor4000UseHolding { get { return "B1→Holding 1, B2～B8→B1～B7"; }}

	public override string ConfigFor3020 { get { return "Configuration for 3020"; }}
	public override string ConfigFor3020NoHolding8 { get { return "B1～B8→B1～B8"; }}
	public override string ConfigFor3020NoHolding7 { get { return "B1～B7→0～400kPa, B8→400kPa"; }}
	public override string ConfigFor3020UseHolding7 { get { return "B1→Holding 1, B2～B8→0～400kPa"; }}

	public override string ConfigForOther { get { return "Configuration for other models"; }}
	public override string ConfigForOtherNoHolding { get { return "B1～B6→B1～B6, B7→B6, B8→B6"; }}
	public override string ConfigForOtherUseHolding { get { return "B1→Holding, B2～B7→B1～B6, B8→B6"; }}

	public override string ConfigForATC { get { return "ATC Configuration"; }}
	public override string ConfigForATCATS { get { return "ATS"; }}
	public override string ConfigForATCRawATS { get { return "Raw ATS"; }}
	public override string ConfigForATCSpeedLimit { get { return "Speed Limit"; }}
	public override string ConfigForATCSpeedLimitWithoutNotice { get { return "Speed Limit (no Notice)"; }}

	public override string DenConvConfigurationGuide { get {
		return "Please select \"Professional Shiyou-Authentic\" on DenConv.";
	}}

	public override string ErrorDialogTitle { get { return "Error"; }}
	public override string BeaconWindowCreationFailed { get {
		return "Failed to create a window for being found.";
	}}
	public override string CommunicationMemoryAllocationFailed { get {
		return "Failed to allocate memory for communication.\n" +
			"It is known that this sometimes happens.\n" +
			"Please try launching one or several more times.\n" +
			"Sorry for inconvenience.";
	}}
}
