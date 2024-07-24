TrainConvConnector
==================

## Overview

This software relays between TRAIN CREW and DenConv to enable you control trains
via controllers that are not officially supported in TRAIN CREW,
such as two-handle controllers.

This software is unofficial, and has no relationships with creators of TRAIN CREW and DenConv.

Note:
DenConv ver3.72 is not working well in developer's environment,
so this software is not tested with the version. I recommend using ver3.70.

## Merits

While TRAIN CREW can be controlled via the keyboard output function of DenConv,
using this software has these merits:

* We can control trains without worrying about slipping of control.
* Detailed conversions for each car models in TRAIN CREW are available.
* Status of trains can be displayed on supported controllers while controlling trains.

## How to use

1. Connect your controller to use to your PC, and set up it if needed.
   Please refer to documentations of DenConv for details of how to set up it.

2. Launch TRAIN CREW, DenConv, and TrainConvConnector.
   The order of launching them is not important.

3. Configure TRAIN CREW.
   Enable "External device I/O" in "Input Device" setting.
   Also, when using controllers that are recognized as game pads,
   disable input from corresponding controllers to prevent unexpected behaviors.

4. Configure DenConv.
   Set "Select Game" to "Professional Shiyou-Authentic".
   (TrainConvConnector is designed to work with this mode)
   Also, configure input device to match your environment.
   If you want to do operations other than power and brake (buzzer, reverser, etc.)
   from your controller, please assign controls via keyboard here.

5. Configure TrainConvConnector.
   Setting car model in "Operation Mode" to "Auto" is recommended in usual situations.
   Set other items according to your environment and preferences.

6. Drive trains.
   Firstly checking if conversion works well using practice modes is recommended.

## Status

### DenConv Status

* Power

  The status of power received from DenConv.

* Brake

  The status of brake received from DenConv.
  Adjusted value is displayed when "Tweak for Shinkansen" is enabled.

* ATC

  The value of ATC to send to DenConv.
  When "-" is displayed, "65535" is sent.

* ATC Notice

  The value of ATC (blinking) to send to DenConv.
  When "-" is displayed, "65535" is sent.

### TRAIN CREW Status

* Power

  The value of Power to send to TRAIN CREW.

* Brake

  The value of Brake to send to TRAIN CREW.
  The expected status on the game and the actual value to send via API are shown.

* Door Closed

  The status of door close lamp (whether all doors on the cars are closed).
  This value is sent to DenConv.

* Car Model

  The car model currently driving.
  The first item in the list is used.

* Speed

  The current speed [km/s] of the train.
  The absolute value of this value is sent to DenConv.

* Speed Limit

  The current speed limit [km/s] of the train.

* Limit Notice

  The coming speed limit [km/s] of the train that is currently displayed.

* ATS

  The current speed [km/s] on the ATS display.

* Playing?

  Whether if currently the main game screen is shown (including paused).
  The loading screen is not included.

* BC Pressure

  The current BC Pressure [kPa] of the current car.
  The first item in the list is used.
  This value is sent to DenConv.

* Distance

  The distance to the next stop/pass point [m] on the UI of TRAIN CREW.
  This value is sent to DenConv.
  (Only the distance in the range of ±12.5cm is sent according to the design of DenConv.)

## Configuration

### Language

Select the language to use in the UI.

### Operation Mode

#### Selecting Car Model

When "Auto" is selected, the operation mode is automatically set
according to the information of current car model.
The mode to be used is displayed on the right of the "Auto".

When "4000 / 4000R", "3020", or "Other" are selected, the mode for
selected car model is used regardless of the information of current car model.
These choice may be helpful when driving new car models
that are not supported by TrainConvConnector.

#### Tweak for Shinkansen

Most controllers for Densha de Go! has 10-step brakes: Release, B1 to B8, and EB.
On the other hand, ones for Shinkansen have 9-step brakes: Release, B1 to B7, and EB.

When controllers for Shinkansen are used, DenConv reports B6 as B7, and B7 as B8.

When "Tweak for Shinkansen" is enabled, the inverse of this conversion is performed
to receive as-is input of B1 to B7.

### Configuration for 4000 / 4000R

Configure conversion for car model 4000 and 4000R.
These models have 3-step holding brakes on the power handle.
Also they have 9-step brakes: Release, B1 to B7, and EB.

* B1～B7→B1～B7, B8→B7

  Straightly assign input B1 to B7 to output B1 to B7.
  Input B8 is assigned to output B7.

* B1→Holding 1, B2～B8→B1～B7

  Input B1 is assigned to output Release.
  Input B2 to B8 are assigned to output B1 to B7.
  Also, when the input is brake B1 and power P0, HB1 is outputted for power.

### Configuration for 3020

Configure conversion for car model 3020.
This model has 3-step holding brake on the power handle.
The brake is specified as release, EB, or a numeric value from 0 to 400 [kPa].
Also 8 steps from B1 to B8 can be used instead of the numeric values.

* B1～B8→B1～B8

  Straightly assign input B1 to B8 to output B1 to B8.

* B1～B7→0～400kPa, B8→400kPa

  Input B1 to B7 are assigned to output 0 to 400 [kPa], evenly divided.
  Input B8 is assigned to output 400 [kPa].
  This mode should enable you to drive this model like other models with 7-step brakes.

  Note: This mode is currently unavailable due to a problem in the API to set the pressure.

* B1→Holding 1, B2～B8→0～400kPa

  Input B1 is assigned to output Release.
  Input B2 to B8 are assigned to output 0 to 400 [kPa], evenly divided.
  Also, when the input is brake B1 and power P0, HB1 is outputted for power.

  Note: This mode is currently unavailable due to a problem in the API to set the pressure.

### Configuration for other models

Configure conversion for other car models.
These models have 9-step brakes: Release, Holding brake, B1 to B6, and EB.

Note: this mayn't be true for new models, but this is assumed in this software.

* B1～B6→B1～B6, B7→B6, B8→B6

  Input B1 to B6 are assigned to output B1 to B6.
  Input B7 and B8 are assigned to output B6.
  Holding brake won't appear in output.
  Using this mode, you can ignore "Holding brake", which isn't familiar in Densha de Go!

* B1→Holding, B2～B7→B1～B6, B8→B6

  Input B1 is assigned to output Holding brake.
  Input B2 to B7 are assigned to output B1 to B6.
  Input B8 is assigned to output B7.

### ATC Configuration

Select what to send as "ATC" value to DenConv.

* ATS

  Send the value of ATS in TRAIN CREW.
  The value "112" is sent as "110", and the value "300" is sent as "65535"
  to match with the value on the screen.

* Raw ATS

  Send the value of ATS in TRAIN CREW.
  The value is sent as-is without the conversion done in the "ATS" mode.

* Speed Limit

  Send the value of speed limit in TRAIN CREW.
  Also the value of speed limit notice is sent when available.

* Speed Limit (no Notice)

  Send the value of speed limit in TRAIN CREW.
  Only the current limit is sent (no notice is sent).

## When to set converted power and brake

TrainConvConnector sets converted power and brake to TRAIN CREW when:

* The conversion results change (including changes caused by changes of operation modes)
* The game starts

The status on TrainConvConnector and one on TRAIN CREW may differ,
for example, when power and/or brake are changed via operations on TRAIN CREW.

## Related links

* TrainConvConnector
  * https://github.com/mikecat/train_conv_connector
* TRAIN CREW
  * https://acty-soft.com/traincrew/
  * https://store.steampowered.com/app/1618290/TRAIN_CREW/
* DenConv
  * https://autotraintas.hariko.com/
  * ver3.70 archive: https://web.archive.org/web/20240529001140/https://autotraintas.hariko.com/

## License

TrainConvConnector is licensed under the MIT License.

```
Copyright (c) 2024 みけCAT

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

TrainCrewInput.dll (the library for communicating with TRAIN CREW) is
created by 溝月レイル/Acty, and isn't licensed under the MIT License.
Analyzation and modification of TrainCrewInput.dll are prohibited.
