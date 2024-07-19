using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using TrainCrew;

class TrainConvConnector: Form
{
	public static void Main()
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
		Application.Run(new TrainConvConnector());
	}

	private const int gridSize = 22;
	private UIText uiText;

	private MenuStrip mainMenu;
	private ToolStripMenuItem languageMenu, languageMenuJapanese, languageMenuEnglish;

	private GroupBox denConvStatusBox;
	private Label denConvPowerNameLabel, denConvPowerValueLabel;
	private Label denConvBrakeNameLabel, denConvBrakeValueLabel;
	private Label denConvATCNameLabel, denConvATCValueLabel;
	private Label denConvATCNoticeNameLabel, denConvATCNoticeValueLabel;

	private GroupBox trainCrewStatusBox;
	private Label trainCrewPowerNameLabel, trainCrewPowerValueLabel;
	private Label trainCrewBrakeNameLabel, trainCrewBrakeValueLabel;
	private Label trainCrewDoorCloseNameLabel, trainCrewDoorCloseValueLabel;
	private Label trainCrewCarModelNameLabel, trainCrewCarModelValueLabel;
	private Label trainCrewSpeedNameLabel, trainCrewSpeedValueLabel;
	private Label trainCrewSpeedLimitNameLabel, trainCrewSpeedLimitValueLabel;
	private Label trainCrewSpeedLimitNoticeNameLabel, trainCrewSpeedLimitNoticeValueLabel;
	private Label trainCrewATSNameLabel, trainCrewATSValueLabel;
	private Label trainCrewInGameNameLabel, trainCrewInGameValueLabel;
	private Label trainCrewBCPressureNameLabel, trainCrewBCPressureValueLabel;
	private Label trainCrewDistanceNameLabel, trainCrewDistanceValueLabel;

	private GroupBox operationModeBox;
	private RadioButton operationModeAutoRadioButton;
	private Label operationModeAutoStatusLabel;
	private RadioButton operationMode4000RadioButton;
	private RadioButton operationMode3020RadioButton;
	private RadioButton operationModeOtherRadioButton;
	private CheckBox shinkansenTweakCheckBox;

	private GroupBox configFor4000Box;
	private RadioButton configFor4000NoHoldingRadioButton;
	private RadioButton configFor4000UseHoldingRadioButton;

	private GroupBox configFor3020Box;
	private RadioButton configFor3020NoHolding8RadioButton;
	private RadioButton configFor3020NoHolding7RadioButton;
	private RadioButton configFor3020UseHolding7RadioButton;

	private GroupBox configForOtherBox;
	private RadioButton configForOtherNoHoldingRadioButton;
	private RadioButton configForOtherUseHoldingRadioButton;

	private GroupBox configForATCBox;
	private RadioButton configForATCATSRadioButton;
	private RadioButton configForATCRawATSRadioButton;
	private RadioButton configForATCSpeedLimitRadioButton;
	private RadioButton configForATCSpeedLimitWithoutNoticeRadioButton;

	private Label denConvConfigurationGuideLabel;

	private static readonly string RegistrySubKey = "Software\\MikeCAT\\TrainConvConnector";
	private static readonly string RegistryPath = "HKEY_CURRENT_USER\\" + RegistrySubKey;

	private static readonly string LanguageValueName = "Language";
	private static readonly string LanguageJapaneseValue = "Japanese";
	private static readonly string LanguageEnglishValue = "English";

	private static readonly string OperationModeValueName = "OperationMode";
	private static readonly string OperationModeAutoValue = "Auto";
	private static readonly string OperationMode4000Value = "4000";
	private static readonly string OperationMode3020Value = "3020";
	private static readonly string OperationModeOtherValue = "Other";
	private static readonly string ShinkansenTweakValueName = "ShinkansenTweak";

	private static readonly string ConfigFor4000ValueName = "4000Mode";
	private static readonly string ConfigFor4000NoHoldingValue = "NoHolding";
	private static readonly string ConfigFor4000UseHoldingValue = "UseHolding";

	private static readonly string ConfigFor3020ValueName = "3020Mode";
	private static readonly string ConfigFor3020NoHolding8Value = "NoHolding8";
	private static readonly string ConfigFor3020NoHolding7Value = "NoHolding7";
	private static readonly string ConfigFor3020UseHolding7Value = "UseHolding7";

	private static readonly string ConfigForOtherValueName = "OtherCarModelMode";
	private static readonly string ConfigForOtherNoHoldingValue = "NoHolding";
	private static readonly string ConfigForOtherUseHoldingValue = "UseHolding";

	private static readonly string ConfigForATCValueName = "ATCMode";
	private static readonly string ConfigForATCATSValue = "ATS";
	private static readonly string ConfigForATCRawATSValue = "RawATS";
	private static readonly string ConfigForATCSpeedLimitValue = "SpeedLimit";
	private static readonly string ConfigForATCSpeedLimitWithoutNoticeValue = "SpeedLimitWithoutNotice";

	private IntPtr hBeaconWnd;
	private Timer conversionTimer;
	private bool trainCrewInputEnabled = false;
	private bool prevInGame = false;
	private int prevConvertedPower = -99, prevConvertedBrake = -99;

	private Point GetGridPoint(float x, float y, bool useMenuOffset)
	{
		return new Point((int)(gridSize * x), (int)(gridSize * y) + (useMenuOffset ? mainMenu.Height : 0));
	}

	private Size GetGridSize(float width, float height)
	{
		return new Size((int)(gridSize * width), (int)(gridSize * height));
	}

	private GroupBox AddGroupBox(Control parent, float x, float y, float width, float height, bool useMenuOffset = false)
	{
		GroupBox groupBox = new GroupBox();
		groupBox.Location = GetGridPoint(x, y, useMenuOffset);
		groupBox.Size = GetGridSize(width, height);
		if (parent != null) parent.Controls.Add(groupBox);
		return groupBox;
	}

	private Label AddLabel(Control parent, float x, float y, bool useMenuOffset = false)
	{
		Label label = new Label();
		label.AutoSize = true;
		label.Location = GetGridPoint(x, y, useMenuOffset);
		if (parent != null) parent.Controls.Add(label);
		return label;
	}

	private RadioButton AddRadioButton(Control parent, float x, float y, bool useMenuOffset = false)
	{
		RadioButton radioButton = new RadioButton();
		radioButton.AutoSize = true;
		radioButton.Location = GetGridPoint(x, y, useMenuOffset);
		if (parent != null) parent.Controls.Add(radioButton);
		return radioButton;
	}

	private CheckBox AddCheckBox(Control parent, float x, float y, bool useMenuOffset = false)
	{
		CheckBox checkBox = new CheckBox();
		checkBox.AutoSize = true;
		checkBox.Location = GetGridPoint(x, y, useMenuOffset);
		if (parent != null) parent.Controls.Add(checkBox);
		return checkBox;
	}

	private void SetControlTexts()
	{
		denConvStatusBox.Text = uiText.DenConvStatus;
		denConvPowerNameLabel.Text = uiText.DenConvPowerName;
		denConvBrakeNameLabel.Text = uiText.DenConvBrakeName;
		denConvATCNameLabel.Text = uiText.DenConvATCName;
		denConvATCNoticeNameLabel.Text = uiText.DenConvATCNoticeName;

		trainCrewStatusBox.Text = uiText.TrainCrewStatus;
		trainCrewPowerNameLabel.Text = uiText.TrainCrewPowerName;
		trainCrewBrakeNameLabel.Text = uiText.TrainCrewBrakeName;
		trainCrewDoorCloseNameLabel.Text = uiText.TrainCrewDoorCloseName;
		trainCrewCarModelNameLabel.Text = uiText.TrainCrewCarModelName;
		trainCrewSpeedNameLabel.Text = uiText.TrainCrewSpeedName;
		trainCrewSpeedLimitNameLabel.Text = uiText.TrainCrewSpeedLimitName;
		trainCrewSpeedLimitNoticeNameLabel.Text = uiText.TrainCrewSpeedLimitNoticeName;
		trainCrewATSNameLabel.Text = uiText.TrainCrewATSName;
		trainCrewInGameNameLabel.Text = uiText.TrainCrewInGameName;
		trainCrewBCPressureNameLabel.Text = uiText.TrainCrewBCPressureName;
		trainCrewDistanceNameLabel.Text = uiText.TrainCrewDistanceName;

		operationModeBox.Text = uiText.OperationMode;
		operationModeAutoRadioButton.Text = uiText.OperationModeAutoName;
		operationMode4000RadioButton.Text = uiText.OperationMode4000Name;
		operationMode3020RadioButton.Text = uiText.OperationMode3020Name;
		operationModeOtherRadioButton.Text = uiText.OperationModeOtherName;
		shinkansenTweakCheckBox.Text = uiText.ShinkansenTweakName;

		configFor4000Box.Text = uiText.ConfigFor4000;
		configFor4000NoHoldingRadioButton.Text = uiText.ConfigFor4000NoHolding;
		configFor4000UseHoldingRadioButton.Text = uiText.ConfigFor4000UseHolding;

		configFor3020Box.Text = uiText.ConfigFor3020;
		configFor3020NoHolding8RadioButton.Text = uiText.ConfigFor3020NoHolding8;
		configFor3020NoHolding7RadioButton.Text = uiText.ConfigFor3020NoHolding7;
		configFor3020UseHolding7RadioButton.Text = uiText.ConfigFor3020UseHolding7;

		configForOtherBox.Text = uiText.ConfigForOther;
		configForOtherNoHoldingRadioButton.Text = uiText.ConfigForOtherNoHolding;
		configForOtherUseHoldingRadioButton.Text = uiText.ConfigForOtherUseHolding;

		configForATCBox.Text = uiText.ConfigForATC;
		configForATCATSRadioButton.Text = uiText.ConfigForATCATS;
		configForATCRawATSRadioButton.Text = uiText.ConfigForATCRawATS;
		configForATCSpeedLimitRadioButton.Text = uiText.ConfigForATCSpeedLimit;
		configForATCSpeedLimitWithoutNoticeRadioButton.Text = uiText.ConfigForATCSpeedLimitWithoutNotice;

		denConvConfigurationGuideLabel.Text = uiText.DenConvConfigurationGuide;
	}

	public TrainConvConnector()
	{
		mainMenu = new MenuStrip();
		languageMenu = new ToolStripMenuItem();
		languageMenu.Text = "言語 / Language (&L)";
		languageMenuJapanese = new ToolStripMenuItem();
		languageMenuJapanese.Text = "日本語 (&J)";
		languageMenuEnglish = new ToolStripMenuItem();
		languageMenuEnglish.Text = "English (&E)";
		languageMenu.DropDownItems.Add(languageMenuJapanese);
		languageMenu.DropDownItems.Add(languageMenuEnglish);
		mainMenu.Items.Add(languageMenu);
		this.Controls.Add(mainMenu);
		this.MainMenuStrip = mainMenu;

		this.Text = "TrainConvConnector 0.1.0";
		this.Font = new Font("MS UI Gothic", 16, GraphicsUnit.Pixel);
		this.FormBorderStyle = FormBorderStyle.FixedSingle;
		this.MaximizeBox = false;
		this.ClientSize = GetGridSize(37, 19);
		this.Height += mainMenu.Height;

		SuspendLayout();

		const float col1x = 0.5f, col2x = 5.5f, col3x = 10.5f, col4x = 15.5f;
		const float leftBoxWidth = 20;
		denConvStatusBox = AddGroupBox(this, 0.5f, 0.5f, leftBoxWidth, 3.5f, true);
		denConvPowerNameLabel = AddLabel(denConvStatusBox, col1x, 1);
		denConvPowerValueLabel = AddLabel(denConvStatusBox, col2x, 1);
		denConvPowerValueLabel.Text = "P?";
		denConvBrakeNameLabel = AddLabel(denConvStatusBox, col3x, 1);
		denConvBrakeValueLabel = AddLabel(denConvStatusBox, col4x, 1);
		denConvBrakeValueLabel.Text = "B?";
		denConvATCNameLabel = AddLabel(denConvStatusBox, col1x, 2);
		denConvATCValueLabel = AddLabel(denConvStatusBox, col2x, 2);
		denConvATCValueLabel.Text = "???";
		denConvATCNoticeNameLabel = AddLabel(denConvStatusBox, col3x, 2);
		denConvATCNoticeValueLabel = AddLabel(denConvStatusBox, col4x, 2);
		denConvATCNoticeValueLabel.Text = "???";

		trainCrewStatusBox = AddGroupBox(this, 0.5f, 4.5f, leftBoxWidth, 8.5f, true);
		trainCrewPowerNameLabel = AddLabel(trainCrewStatusBox, col1x, 1);
		trainCrewPowerValueLabel = AddLabel(trainCrewStatusBox, col2x, 1);
		trainCrewPowerValueLabel.Text = "?";
		trainCrewBrakeNameLabel = AddLabel(trainCrewStatusBox, col3x, 1);
		trainCrewBrakeValueLabel = AddLabel(trainCrewStatusBox, col4x, 1);
		trainCrewBrakeValueLabel.Text = "??? kPa";
		trainCrewDoorCloseNameLabel = AddLabel(trainCrewStatusBox, col1x, 2);
		trainCrewDoorCloseValueLabel = AddLabel(trainCrewStatusBox, col2x, 2);
		trainCrewDoorCloseValueLabel.Text = "??";
		trainCrewCarModelNameLabel = AddLabel(trainCrewStatusBox, col3x, 2);
		trainCrewCarModelValueLabel = AddLabel(trainCrewStatusBox, col4x, 2);
		trainCrewCarModelValueLabel.Text = "????";
		trainCrewCarModelNameLabel = AddLabel(trainCrewStatusBox, col3x, 2);
		trainCrewSpeedNameLabel = AddLabel(trainCrewStatusBox, col1x, 3);
		trainCrewSpeedValueLabel = AddLabel(trainCrewStatusBox, col2x, 3);
		trainCrewSpeedValueLabel.Text = "??.??????";
		trainCrewSpeedLimitNameLabel = AddLabel(trainCrewStatusBox, col1x, 4);
		trainCrewSpeedLimitValueLabel = AddLabel(trainCrewStatusBox, col2x, 4);
		trainCrewSpeedLimitValueLabel.Text = "???";
		trainCrewSpeedLimitNoticeNameLabel = AddLabel(trainCrewStatusBox, col3x, 4);
		trainCrewSpeedLimitNoticeValueLabel = AddLabel(trainCrewStatusBox, col4x, 4);
		trainCrewSpeedLimitNoticeValueLabel.Text = "???";
		trainCrewATSNameLabel = AddLabel(trainCrewStatusBox, col1x, 5);
		trainCrewATSValueLabel = AddLabel(trainCrewStatusBox, col2x, 5);
		trainCrewATSValueLabel.Text = "???";
		trainCrewInGameNameLabel = AddLabel(trainCrewStatusBox, col3x, 5);
		trainCrewInGameValueLabel = AddLabel(trainCrewStatusBox, col4x, 5);
		trainCrewInGameValueLabel.Text = "??????";
		trainCrewBCPressureNameLabel = AddLabel(trainCrewStatusBox, col1x, 6);
		trainCrewBCPressureValueLabel = AddLabel(trainCrewStatusBox, col2x, 6);
		trainCrewBCPressureValueLabel.Text = "???.??????";
		trainCrewDistanceNameLabel = AddLabel(trainCrewStatusBox, col1x, 7);
		trainCrewDistanceValueLabel = AddLabel(trainCrewStatusBox, col2x, 7);
		trainCrewDistanceValueLabel.Text = "????.??????";

		operationModeBox = AddGroupBox(this, 0.5f, 13.5f, leftBoxWidth, 3.5f, true);
		operationModeAutoRadioButton = AddRadioButton(operationModeBox, col1x, 1);
		operationModeAutoStatusLabel = AddLabel(operationModeBox, col2x, 1);
		operationModeAutoStatusLabel.Text = "(????)";
		operationMode4000RadioButton = AddRadioButton(operationModeBox, col1x, 2);
		operationMode3020RadioButton = AddRadioButton(operationModeBox, 8, 2);
		operationModeOtherRadioButton = AddRadioButton(operationModeBox, 14, 2);
		shinkansenTweakCheckBox = AddCheckBox(operationModeBox, col3x, 1);

		const float rightBoxX = leftBoxWidth + 1, rightBoxWidth = 15.5f;
		configFor4000Box = AddGroupBox(this, rightBoxX, 0.5f, rightBoxWidth, 3.5f, true);
		configFor4000NoHoldingRadioButton = AddRadioButton(configFor4000Box, col1x, 1);
		configFor4000UseHoldingRadioButton = AddRadioButton(configFor4000Box, col1x, 2);

		configFor3020Box = AddGroupBox(this, rightBoxX, 4.5f, rightBoxWidth, 4.5f, true);
		configFor3020NoHolding8RadioButton = AddRadioButton(configFor3020Box, col1x, 1);
		configFor3020NoHolding7RadioButton = AddRadioButton(configFor3020Box, col1x, 2);
		configFor3020UseHolding7RadioButton = AddRadioButton(configFor3020Box, col1x, 3);

		configForOtherBox = AddGroupBox(this, rightBoxX, 9.5f, rightBoxWidth, 3.5f, true);
		configForOtherNoHoldingRadioButton = AddRadioButton(configForOtherBox, col1x, 1);
		configForOtherUseHoldingRadioButton = AddRadioButton(configForOtherBox, col1x, 2);

		const float col2xATC = 6;
		configForATCBox = AddGroupBox(this, rightBoxX, 13.5f, rightBoxWidth, 3.5f, true);
		configForATCATSRadioButton = AddRadioButton(configForATCBox, col1x, 1);
		configForATCRawATSRadioButton = AddRadioButton(configForATCBox, col2xATC, 1);
		configForATCSpeedLimitRadioButton = AddRadioButton(configForATCBox, col1x, 2);
		configForATCSpeedLimitWithoutNoticeRadioButton = AddRadioButton(configForATCBox, col2xATC, 2);

		denConvConfigurationGuideLabel = AddLabel(this, 0.5f, 17.5f, true);

		ResumeLayout();
		Load += LoadHandler;
		Shown += ShownHandler;
	}

	private void LoadHandler(object sender, EventArgs e)
	{
		RegistryKey regKey = null;
		try
		{
			regKey = Registry.CurrentUser.CreateSubKey(RegistrySubKey);

			object language = regKey.GetValue(LanguageValueName);
			if (LanguageEnglishValue.CompareTo(language) == 0)
			{
				languageMenuEnglish.Checked = true;
			}

			object operationMode = regKey.GetValue(OperationModeValueName);
			if (OperationMode4000Value.CompareTo(operationMode) == 0)
			{
				operationMode4000RadioButton.Checked = true;
			}
			else if (OperationMode3020Value.CompareTo(operationMode) == 0)
			{
				operationMode3020RadioButton.Checked = true;
			}
			else if (OperationModeOtherValue.CompareTo(operationMode) == 0)
			{
				operationModeOtherRadioButton.Checked = true;
			}
			else
			{
				operationModeAutoRadioButton.Checked = true;
			}

			object shinkansenTweak = regKey.GetValue(ShinkansenTweakValueName);
			if (shinkansenTweak != null && !shinkansenTweak.Equals(0))
			{
				shinkansenTweakCheckBox.Checked = true;
			}

			object configFor4000 = regKey.GetValue(ConfigFor4000ValueName);
			if (ConfigFor4000UseHoldingValue.CompareTo(configFor4000) == 0)
			{
				configFor4000UseHoldingRadioButton.Checked = true;
			}
			else
			{
				configFor4000NoHoldingRadioButton.Checked = true;
			}

			object configFor3020 = regKey.GetValue(ConfigFor3020ValueName);
			if (ConfigFor3020NoHolding7Value.CompareTo(configFor3020) == 0)
			{
				configFor3020NoHolding7RadioButton.Checked = true;
			}
			else if (ConfigFor3020UseHolding7Value.CompareTo(configFor3020) == 0)
			{
				configFor3020UseHolding7RadioButton.Checked = true;
			}
			else
			{
				configFor3020NoHolding8RadioButton.Checked = true;
			}

			object configForOther = regKey.GetValue(ConfigForOtherValueName);
			if (ConfigForOtherNoHoldingValue.CompareTo(configForOther) == 0)
			{
				configForOtherNoHoldingRadioButton.Checked = true;
			}
			else
			{
				configForOtherUseHoldingRadioButton.Checked = true;
			}

			object atcMode = regKey.GetValue(ConfigForATCValueName);
			if (ConfigForATCRawATSValue.CompareTo(atcMode) == 0)
			{
				configForATCRawATSRadioButton.Checked = true;
			}
			else if (ConfigForATCSpeedLimitValue.CompareTo(atcMode) == 0)
			{
				configForATCSpeedLimitRadioButton.Checked = true;
			}
			else if (ConfigForATCSpeedLimitWithoutNoticeValue.CompareTo(atcMode) == 0)
			{
				configForATCSpeedLimitWithoutNoticeRadioButton.Checked = true;
			}
			else
			{
				configForATCATSRadioButton.Checked = true;
			}
		}
		catch (Exception)
		{
			// 何もしない (握りつぶす)
		}
		finally
		{
			try
			{
				if (regKey != null) regKey.Close();
			}
			catch (Exception)
			{
				// 何もしない (握りつぶす)
			}
		}
		if (languageMenuEnglish.Checked)
		{
			uiText = new EnglishUIText();
		}
		else
		{
			languageMenuJapanese.Checked = true;
			uiText = new JapaneseUIText();
		}
		SetControlTexts();
		languageMenuJapanese.Click += LanguageChangeHandler;
		languageMenuEnglish.Click += LanguageChangeHandler;
		operationModeAutoRadioButton.Click += OperationModeChangeHandler;
		operationMode4000RadioButton.Click += OperationModeChangeHandler;
		operationMode3020RadioButton.Click += OperationModeChangeHandler;
		operationModeOtherRadioButton.Click += OperationModeChangeHandler;
		shinkansenTweakCheckBox.Click += ShinkansenTweakChangeHandler;
		configFor4000NoHoldingRadioButton.Click += ConfigFor4000ChangeHandler;
		configFor4000UseHoldingRadioButton.Click += ConfigFor4000ChangeHandler;
		configFor3020NoHolding8RadioButton.Click += ConfigFor3020ChangeHandler;
		configFor3020NoHolding7RadioButton.Click += ConfigFor3020ChangeHandler;
		configFor3020UseHolding7RadioButton.Click += ConfigFor3020ChangeHandler;
		configForOtherNoHoldingRadioButton.Click += ConfigForOtherChangeHandler;
		configForOtherUseHoldingRadioButton.Click += ConfigForOtherChangeHandler;
		configForATCATSRadioButton.Click += ConfigForATCChangeHandler;
		configForATCRawATSRadioButton.Click += ConfigForATCChangeHandler;
		configForATCSpeedLimitRadioButton.Click += ConfigForATCChangeHandler;
		configForATCSpeedLimitWithoutNoticeRadioButton.Click += ConfigForATCChangeHandler;
	}

	private void SetRegValueAndIgnoreExceptions(string valueName, object value)
	{
		try
		{
			Registry.SetValue(RegistryPath, valueName, value);
		}
		catch (Exception)
		{
			// 何もしない (握りつぶす)
		}
	}

	private void LanguageChangeHandler(object sender, EventArgs e)
	{
		if (sender == languageMenuJapanese)
		{
			uiText = new JapaneseUIText();
			languageMenuJapanese.Checked = true;
			languageMenuEnglish.Checked = false;
			SetRegValueAndIgnoreExceptions(LanguageValueName, LanguageJapaneseValue);
		}
		else if (sender == languageMenuEnglish)
		{
			uiText = new EnglishUIText();
			languageMenuJapanese.Checked = false;
			languageMenuEnglish.Checked = true;
			SetRegValueAndIgnoreExceptions(LanguageValueName, LanguageEnglishValue);
		}
		else
		{
			return;
		}
		SetControlTexts();
	}

	private void OperationModeChangeHandler(object sender, EventArgs e)
	{
		string newMode;
		if (sender == operationModeAutoRadioButton) newMode = OperationModeAutoValue;
		else if (sender == operationMode4000RadioButton) newMode = OperationMode4000Value;
		else if (sender == operationMode3020RadioButton) newMode = OperationMode3020Value;
		else if (sender == operationModeOtherRadioButton) newMode = OperationModeOtherValue;
		else return;
		SetRegValueAndIgnoreExceptions(OperationModeValueName, newMode);
	}

	private void ShinkansenTweakChangeHandler(object sender, EventArgs e)
	{
		int newValue = shinkansenTweakCheckBox.Checked ? 1 : 0;
		SetRegValueAndIgnoreExceptions(ShinkansenTweakValueName, newValue);
	}

	private void ConfigFor4000ChangeHandler(object sender, EventArgs e)
	{
		string newMode;
		if (sender == configFor4000NoHoldingRadioButton) newMode = ConfigFor4000NoHoldingValue;
		else if (sender == configFor4000UseHoldingRadioButton) newMode = ConfigFor4000UseHoldingValue;
		else return;
		SetRegValueAndIgnoreExceptions(ConfigFor4000ValueName, newMode);
	}

	private void ConfigFor3020ChangeHandler(object sender, EventArgs e)
	{
		string newMode;
		if (sender == configFor3020NoHolding8RadioButton) newMode = ConfigFor3020NoHolding8Value;
		else if (sender == configFor3020NoHolding7RadioButton) newMode = ConfigFor3020NoHolding7Value;
		else if (sender == configFor3020UseHolding7RadioButton) newMode = ConfigFor3020UseHolding7Value;
		else return;
		SetRegValueAndIgnoreExceptions(ConfigFor3020ValueName, newMode);
	}

	private void ConfigForOtherChangeHandler(object sender, EventArgs e)
	{
		string newMode;
		if (sender == configForOtherNoHoldingRadioButton) newMode = ConfigForOtherNoHoldingValue;
		else if (sender == configForOtherUseHoldingRadioButton) newMode = ConfigForOtherUseHoldingValue;
		else return;
		SetRegValueAndIgnoreExceptions(ConfigForOtherValueName, newMode);
	}

	private void ConfigForATCChangeHandler(object sender, EventArgs e)
	{
		string newMode;
		if (sender == configForATCATSRadioButton) newMode = ConfigForATCATSValue;
		else if (sender == configForATCRawATSRadioButton) newMode = ConfigForATCRawATSValue;
		else if (sender == configForATCSpeedLimitRadioButton) newMode = ConfigForATCSpeedLimitValue;
		else if (sender == configForATCSpeedLimitWithoutNoticeRadioButton) newMode = ConfigForATCSpeedLimitWithoutNoticeValue;
		else return;
		SetRegValueAndIgnoreExceptions(ConfigForATCValueName, newMode);
	}

	private void ShownHandler(object sender, EventArgs e)
	{
		if (!DenConvCommunicator.Initialize())
		{
			DialogResult res = MessageBox.Show(
				uiText.CommunicationMemoryAllocationFailed,
				uiText.ErrorDialogTitle,
				MessageBoxButtons.RetryCancel,
				MessageBoxIcon.Exclamation
			);
			if (res == DialogResult.Retry)
			{
				Process.Start(Application.ExecutablePath);
			}
			this.Close();
			return;
		}
		hBeaconWnd = BeaconWindow.CreateBeaconWindow(this.Handle);
		if (IntPtr.Zero.Equals(hBeaconWnd))
		{
			MessageBox.Show(
				uiText.BeaconWindowCreationFailed,
				uiText.ErrorDialogTitle,
				MessageBoxButtons.OK,
				MessageBoxIcon.Stop
			);
			this.Close();
			return;
		}
		TrainCrewInput.Init();
		trainCrewInputEnabled = true;
		FormClosed += FormClosedHandler;
		conversionTimer = new Timer();
		conversionTimer.Interval = 15;
		conversionTimer.Tick += ConversionTimerTickHandler;
		conversionTimer.Start();
	}

	private void FormClosedHandler(object sender, EventArgs e)
	{
		if (conversionTimer != null) conversionTimer.Stop();
		trainCrewInputEnabled = false;
		TrainCrewInput.Dispose();
	}

	private static float StringToFloat(string str, float defaultValue)
	{
		float result;
		if (Single.TryParse(str, out result))
		{
			return result;
		}
		else
		{
			return defaultValue;
		}
	}

	private void ConversionTimerTickHandler(object sender, EventArgs e)
	{
		// 電車でＧｏ！コントローラー変換器から情報を取得する
		DenConvCommunicator.PowerAndBrake pb = DenConvCommunicator.GetPowerAndBrake(shinkansenTweakCheckBox.Checked);
		denConvPowerValueLabel.Text = string.Format("P{0}", pb.Power);
		denConvBrakeValueLabel.Text = pb.Brake == 0 ?
			string.Format("{0} (0)", uiText.DenConvBrakeRelease) :
			string.Format(pb.Brake >= 9 ? "EB ({0})" : "B{0}", pb.Brake);

		// TRAIN CREW から情報を取得する
		if (!trainCrewInputEnabled) return;
		TrainState trainState = TrainCrewInput.GetTrainState();
		GameState gameState = TrainCrewInput.gameState;
		bool doorClosed = trainState.AllClose;
		bool carValid = trainState.CarStates.Count > 0;
		string carModel = carValid ? trainState.CarStates[0].CarModel : "-";
		float speed = trainState.Speed;
		float speedLimit = trainState.speedLimit;
		float speedLimitNotice = trainState.nextSpeedLimit;
		string ats = trainState.ATS_Speed;
		float atsValue = StringToFloat(ats, -1);
		bool inGame = gameState.gameScreen == GameScreen.MainGame ||
			gameState.gameScreen == GameScreen.MainGame_Pause;
		float bcPressure = carValid ? trainState.CarStates[0].BC_Press : -1;
		float distance = trainState.nextUIDistance;

		// 取得した情報を画面に表示する
		trainCrewDoorCloseValueLabel.Text = doorClosed ? uiText.TrainCrewDoorCloseTrue : uiText.TrainCrewDoorCloseFalse;
		trainCrewCarModelValueLabel.Text = carValid ? carModel : "-";
		trainCrewSpeedValueLabel.Text = speed.ToString();
		trainCrewSpeedLimitValueLabel.Text = speedLimit.ToString();
		trainCrewSpeedLimitNoticeValueLabel.Text = speedLimitNotice == -1 ? "-" : speedLimitNotice.ToString();
		trainCrewATSValueLabel.Text = ats;
		trainCrewInGameValueLabel.Text = inGame ? uiText.TrainCrewInGameTrue : uiText.TrainCrewInGameFalse;
		trainCrewBCPressureValueLabel.Text = carValid ? bcPressure.ToString() : "-";
		trainCrewDistanceValueLabel.Text = distance.ToString();

		// 取得した情報を電車でＧｏ！コントローラー変換器に伝える
		DenConvCommunicator.SetDoorClosed(doorClosed);
		DenConvCommunicator.SetPressure(carValid ? bcPressure : 0);
		DenConvCommunicator.SetDistance(distance);
		DenConvCommunicator.SetSpeed(speed);
		DenConvCommunicator.SetATCActive(inGame);
		float atcValue = -1, atcNoticeValue = -1;
		if (configForATCATSRadioButton.Checked)
		{
			if (atsValue == 112) atcValue = 110;
			else if (atsValue == 300) atcValue = -1;
			else atcValue = atsValue;
		}
		else if (configForATCRawATSRadioButton.Checked)
		{
			atcValue = atsValue;
		}
		else if (configForATCSpeedLimitRadioButton.Checked)
		{
			atcValue = speedLimit;
			atcNoticeValue = speedLimitNotice;
		}
		else if (configForATCSpeedLimitWithoutNoticeRadioButton.Checked)
		{
			atcValue = speedLimit;
		}
		DenConvCommunicator.SetATC(atcValue, atcNoticeValue);
		denConvATCValueLabel.Text = atcValue >= 0 ? atcValue.ToString() : "-";
		denConvATCNoticeValueLabel.Text = atcNoticeValue >= 0 ? atcNoticeValue.ToString() : "-";

		// マスコンとブレーキの変換を行い、TRAIN CREW に伝える
		// TODO: 車種の設定を反映する
		int convertedPower = 0, convertedBrake = 0;
		convertedPower = pb.Power > 5 ? 5 : pb.Power;
		convertedBrake = pb.Brake >= 9 ? 8 : (pb.Brake == 8 ? 7 : pb.Brake);
		if (convertedPower != prevConvertedPower || convertedBrake != prevConvertedBrake ||
			(inGame && !prevInGame))
		{
			TrainCrewInput.SetNotch(convertedPower, convertedBrake);
		}
		trainCrewPowerValueLabel.Text = convertedPower > 0 ? string.Format("P{0}", convertedPower) :
			(convertedPower < 0 ? string.Format("HB{0}", -convertedPower) : "N");
		// TODO: 車種に応じた表示を行う
		trainCrewBrakeValueLabel.Text = convertedBrake.ToString();

		prevConvertedPower = convertedPower;
		prevConvertedBrake = convertedBrake;
		prevInGame = inGame;
	}
}
