using System;
using System.Windows.Forms;
using System.Drawing;

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
	}

	private void LoadHandler(object sender, EventArgs e)
	{
		languageMenuJapanese.Checked = true;
		uiText = new JapaneseUIText();
		SetControlTexts();
		languageMenuJapanese.Click += LanguageChangeHandler;
		languageMenuEnglish.Click += LanguageChangeHandler;
	}

	private void LanguageChangeHandler(object sender, EventArgs e)
	{
		if (sender == languageMenuJapanese)
		{
			uiText = new JapaneseUIText();
			languageMenuJapanese.Checked = true;
			languageMenuEnglish.Checked = false;
		}
		else if (sender == languageMenuEnglish)
		{
			uiText = new EnglishUIText();
			languageMenuJapanese.Checked = false;
			languageMenuEnglish.Checked = true;
		}
		else
		{
			return;
		}
		SetControlTexts();
	}
}
