class JapaneseUIText: UIText
{
	public override string DenConvStatus { get { return "電車でＧｏ！コントローラー変換器の状態"; }}
	public override string DenConvPowerName { get { return "マスコン"; }}
	public override string DenConvBrakeName { get { return "ブレーキ"; }}
	public override string DenConvBrakeRelease { get { return "解除"; }}
	public override string DenConvATCName { get { return "ATC"; }}
	public override string DenConvATCNoticeName { get { return "ATC予告"; }}

	public override string TrainCrewStatus { get { return "TRAIN CREW の状態"; }}
	public override string TrainCrewPowerName { get { return "マスコン"; }}
	public override string TrainCrewBrakeName { get { return "ブレーキ"; }}
	public override string TrainCrewDoorCloseName { get { return "戸閉"; }}
	public override string TrainCrewDoorCloseTrue { get { return "点"; }}
	public override string TrainCrewDoorCloseFalse { get { return "滅"; }}
	public override string TrainCrewCarModelName { get { return "車種"; }}
	public override string TrainCrewSpeedName { get { return "速度"; }}
	public override string TrainCrewSpeedLimitName { get { return "制限速度"; }}
	public override string TrainCrewSpeedLimitNoticeName { get { return "制限速度予告"; }}
	public override string TrainCrewATSName { get { return "ATS"; }}
	public override string TrainCrewInGameName { get { return "ゲーム中？"; }}
	public override string TrainCrewInGameTrue { get { return "はい"; }}
	public override string TrainCrewInGameFalse { get { return "いいえ"; }}
	public override string TrainCrewBCPressureName { get { return "BC圧力"; }}
	public override string TrainCrewDistanceName { get { return "残り距離"; }}

	public override string OperationMode { get { return "動作モード"; }}
	public override string OperationModeAutoName { get { return "自動"; }}
	public override string OperationMode4000Name { get { return "4000 / 4000R"; }}
	public override string OperationMode3020Name { get { return "3020"; }}
	public override string OperationModeOtherName { get { return "その他"; }}
	public override string ShinkansenTweakName { get { return "新幹線補正"; }}

	public override string ConfigFor4000 { get { return "4000 / 4000R 設定"; }}
	public override string ConfigFor4000NoHolding { get { return "B1～B7→B1～B7、B8→B7"; }}
	public override string ConfigFor4000UseHolding { get { return "B1→抑速1、B2～B8→B1～B7"; }}

	public override string ConfigFor3020 { get { return "3020 設定"; }}
	public override string ConfigFor3020NoHolding8 { get { return "B1～B8→B1～B8"; }}
	public override string ConfigFor3020NoHolding7 { get { return "B1～B7→0～400kPa、B8→400kPa"; }}
	public override string ConfigFor3020UseHolding7 { get { return "B1→抑速1、B2～B8→0～400kPa"; }}

	public override string ConfigForOther { get { return "その他車種設定"; }}
	public override string ConfigForOtherNoHolding { get { return "B1～B6→B1～B6、B7→B6、B8→B6"; }}
	public override string ConfigForOtherUseHolding { get { return "B1→抑速、B2～B7→B1～B6、B8→B6"; }}

	public override string ConfigForATC { get { return "ATC設定"; }}
	public override string ConfigForATCATS { get { return "ATS"; }}
	public override string ConfigForATCRawATS { get { return "ATS (補正無)"; }}
	public override string ConfigForATCSpeedLimit { get { return "制限速度"; }}
	public override string ConfigForATCSpeedLimitWithoutNotice { get { return "制限速度 (予告無)"; }}

	public override string DenConvConfigurationGuide { get {
		return "電車でＧｏ！コントローラー変換器では「プロ仕様-本格版/20040526」を選択してください。";
	}}

	public override string ErrorDialogTitle { get { return "エラー"; }}
	public override string BeaconWindowCreationFailed { get {
		return "連携用ウィンドウの作成に失敗しました。";
	}}
	public override string CommunicationMemoryAllocationFailed { get {
		return "連携用メモリの確保に失敗しました。\n" +
			"なぜか時々失敗することが知られています。\n" +
			"お手数をおかけしますが、1回～数回起動しなおしてください。";
	}}
}
