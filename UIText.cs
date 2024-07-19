abstract class UIText
{
	public abstract string DenConvStatus { get; }
	public abstract string DenConvPowerName { get; }
	public abstract string DenConvBrakeName { get; }
	public abstract string DenConvBrakeRelease { get; }
	public abstract string DenConvATCName { get; }
	public abstract string DenConvATCNoticeName { get; }

	public abstract string TrainCrewStatus { get; }
	public abstract string TrainCrewPowerName { get; }
	public abstract string TrainCrewBrakeName { get; }
	public abstract string TrainCrewBrakeRelease { get; }
	public abstract string TrainCrewBrakeHolding { get; }
	public abstract string TrainCrewDoorCloseName { get; }
	public abstract string TrainCrewDoorCloseTrue { get; }
	public abstract string TrainCrewDoorCloseFalse { get; }
	public abstract string TrainCrewCarModelName { get; }
	public abstract string TrainCrewSpeedName { get; }
	public abstract string TrainCrewSpeedLimitName { get; }
	public abstract string TrainCrewSpeedLimitNoticeName { get; }
	public abstract string TrainCrewATSName { get; }
	public abstract string TrainCrewInGameName { get; }
	public abstract string TrainCrewInGameTrue { get; }
	public abstract string TrainCrewInGameFalse { get; }
	public abstract string TrainCrewBCPressureName { get; }
	public abstract string TrainCrewDistanceName { get; }

	public abstract string OperationMode { get; }
	public abstract string OperationModeAutoName { get; }
	public abstract string OperationMode4000Name { get; }
	public abstract string OperationMode3020Name { get; }
	public abstract string OperationModeOtherName { get; }
	public abstract string ShinkansenTweakName { get; }

	public abstract string ConfigFor4000 { get; }
	public abstract string ConfigFor4000NoHolding { get; }
	public abstract string ConfigFor4000UseHolding { get; }

	public abstract string ConfigFor3020 { get; }
	public abstract string ConfigFor3020NoHolding8 { get; }
	public abstract string ConfigFor3020NoHolding7 { get; }
	public abstract string ConfigFor3020UseHolding7 { get; }

	public abstract string ConfigForOther { get; }
	public abstract string ConfigForOtherNoHolding { get; }
	public abstract string ConfigForOtherUseHolding { get; }

	public abstract string ConfigForATC { get; }
	public abstract string ConfigForATCATS { get; }
	public abstract string ConfigForATCRawATS { get; }
	public abstract string ConfigForATCSpeedLimit { get; }
	public abstract string ConfigForATCSpeedLimitWithoutNotice { get; }

	public abstract string DenConvConfigurationGuide { get; }

	public abstract string ErrorDialogTitle { get; }
	public abstract string BeaconWindowCreationFailed { get; }
	public abstract string CommunicationMemoryAllocationFailed { get; }
}
