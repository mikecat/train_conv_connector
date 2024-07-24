TrainConvConnector
==================

## 概要

TRAIN CREW と電車でGo！コントローラー変換器を接続し、ツーハンドルなどの
TRAIN CREW が公式で対応していないコントローラーで電車を運転できるようにします。

このソフトウェアは非公式であり、
TRAIN CREW や電車でGo！コントローラー変換器の作者様とは関係ありません。

※電車でGo！コントローラー変換器 ver3.72 は開発者の環境で正常に動作せず、
連携の確認がとれていません。
ver3.70 を使用することを推奨します。

## 利点

電車でGo！コントローラー変換器のキーボード出力機能でも TRAIN CREW を操作する
ことはできますが、本ソフトウェアを用いることで以下のような利点が得られます。

* 操作のズレの心配をせずに操作できる
* TRAIN CREW の各車種に合わせた細かい変換ができる
* 操作を行うだけでなく、電車の状態を対応コントローラーに表示できる

## 使い方

1. 使用するコントローラーをPCに接続し、必要に応じてセットアップを行います。
   セットアップの詳細は、「電車でGo！コントローラー変換器」のマニュアルを参照してください。

2. TRAIN CREW・電車でGo！コントローラー変換器・TrainConvConnector を起動します。
   これらを起動する順番は、重要ではありません。

3. TRAIN CREW の設定を行います。
   操作設定で「外部デバイス入出力」を有効にします。
   また、ゲームパッドとして認識されるタイプのコントローラーを使用する場合は、
   干渉を防ぐため、対応するコントローラーの入力を無効にします。

4. 電車でGo！コントローラー変換器の設定を行います。
   「制御するソフト」を「プロ仕様-本格版/20040526」に設定します。
   (TrainConvConnector はこのモードに合わせて設計されています)
   また、環境に合わせて入力デバイスの設定を行います。
   コントローラーでマスコン・ブレーキ以外の操作 (ブザー・レバーサなど) を行う場合は、
   ここで対応するキーボード操作を割り当てます。

5. TrainConvConnector の設定を行います。
   「動作モード」の車種設定は、通常は「自動」を推奨します。
   その他の項目は、環境や好みに合わせて設定します。

6. 電車を運転します。
   最初は、練習用のモードで変換が正常に動作するか確かめるとよいでしょう。

## 各状態の解説

### 電車でGo！コントローラー変換器の状態

* マスコン

  電車でGo！コントローラー変換器から受け取ったマスコンの状態です。

* ブレーキ

  電車でGo！コントローラー変換器から受け取ったブレーキの状態です。
  「新幹線補正」が有効の場合は、補正後の状態を表示します。

* ATC

  電車でGo！コントローラー変換器に出力するATCの値です。
  「-」の場合は、「65535」を出力しています。

* ATC予告

  電車でGo！コントローラー変換器に出力するATC (点滅表示) の値です。
  「-」の場合は、「65535」を出力しています。

### TRAIN CREW の状態

* マスコン

  TRAIN CREW に出力するマスコンの状態です。

* ブレーキ

  TRAIN CREW に出力するブレーキの状態です。
  想定するゲーム上の表示と、APIで実際に渡す値を表示しています。

* 戸閉

  戸閉灯の状態 (全車両のドアが閉じているか) です。
  この情報は、電車でGo！コントローラー変換器に出力します。

* 車種

  現在運転中の車種です。
  リストの先頭の情報を使用しています。

* 速度

  現在の電車の速度 [km/h] です。
  この情報は、絶対値を電車でGo！コントローラー変換器に出力します。

* 制限速度

  現在の電車の制限速度 [km/h] です。

* 制限速度予告

  現在予告されている電車の制限速度 [km/h] です。

* ATS

  現在のATS表示器に表示される速度 [km/h] です。

* ゲーム中？

  現在、メインゲーム画面 (ポーズ画面を含む) になっているかを表します。
  ロード中の画面は含みません。

* BC圧力

  現在の車両のBC圧力 [kPa] です。
  リストの先頭の情報を使用しています。
  この情報は、電車でGo！コントローラー変換器に出力します。

* 残り距離

  TRAIN CREW のUIに表示される、次の停車/通過位置までの距離 [m] です。
  この情報は、電車でGo！コントローラー変換器に出力します。
  (電車でGo！コントローラー変換器の仕様に合わせ、±12.5cmの範囲で出力します)

## 設定項目

### 言語

UIの表示に使用する言語を選択します。

### 動作モード

#### 車種選択

「自動」では、取得した車種の情報に応じて動作モードを自動的に設定します。
用いるモードは、「自動」の右に表示されます。

「4000 / 4000R」「3020」「その他」では、
取得した車種にかかわらず、指定した車種用の動作モードを用います。
TrainConvConnector が未対応の新しい車種を運転する際に役立つかもしれません。

#### 新幹線補正

電車でGO！用の多くのコントローラーのブレーキは解除・B1～B8・非常の10段階ですが、
新幹線コントローラーのブレーキは解除・B1～B7・非常の9段階です。

新幹線コントローラーを使用する場合、「電車でGo！コントローラー変換器」は、
B6をB7、B7をB8として報告してきます。

「新幹線補正」をオンにすると、この逆変換を行い、
そのままB1～B7の入力を受け取れるようにします。

### 4000 / 4000R 設定

4000形・4000R形用の変換動作を設定します。
これらの車種では、マスコンに3段階の抑速が割り当てられています。
ブレーキはユルメ、B1～B7、非常の9段階です。

* B1～B7→B1～B7、B8→B7

  入力のB1～B7を、そのまま出力のB1～B7に割り当てます。
  入力のB8は、出力のB7に割り当てます。

* B1→抑速1、B2～B8→B1～B7

  入力のB1を、出力のユルメに割り当てます。
  入力のB2～B8を、出力のB1～B7に割り当てます。
  また、入力のブレーキがB1、マスコンがP0のとき、マスコンに抑速1を出力します。

### 3020 設定

3020形用の変換動作を設定します。
この車種では、マスコンに3段階の抑速が割り当てられています。
ブレーキはユルメ、非常に加え、0～400kPaの数値で指定できます。
また、数値のかわりにB1～B8の8段階でも指定できます。

* B1～B8→B1～B8

  入力のB1～B8を、そのまま出力のB1～B8に割り当てます。

* B1～B7→0～400kPa、B8→400kPa

  入力のB1～B7を、0～400kPaを7等分した数値に割り当てて出力します。
  入力のB8は、400kPaに割り当てて出力します。
  これにより、7段階のブレーキを用いる他の車種と同様に運転できることが期待できます。

  ※圧力を指定するAPIの不具合のため、現在このモードは使用できません。

* B1→抑速1、B2～B8→0～400kPa

  入力のB1を、出力のユルメに割り当てます。
  入力のB2～B8を、0～400kPaを7等分した数値に割り当てて出力します。
  また、入力のブレーキがB1、マスコンがP0のとき、マスコンに抑速1を出力します。

  ※圧力を指定するAPIの不具合のため、現在このモードは使用できません。

### その他車種設定

その他の車種用の変換動作を設定します。
これらの車種のブレーキは、ユルメ・抑速・B1～B6・非常の9段階です。

※新しい車種では当てはまらないかもしれませんが、本ソフトウェアではこれを仮定します。

* B1～B6→B1～B6、B7→B6、B8→B6

  入力のB1～B6を、出力のB1～B6に割り当てます。
  入力のB7・B8は、出力のB6に割り当てます。
  抑速は出力しません。
  電車でGO！では馴染みのない「抑速」の存在を無視することができます。

* B1→抑速、B2～B7→B1～B6、B8→B6

  入力のB1を、出力の抑速に割り当てます。
  入力のB2～B7を、出力のB1～B6に割り当てます。
  入力のB8を、出力のB7に割り当てます。

### ATC設定

電車でGo！コントローラー変換器の「ATC」の情報として何を出力するかを設定します。

* ATS

  TRAIN CREW の ATS の情報を出力します。
  ただし、画面表示が「110」のとき取得できる値は「112」となり、
  画面表示が「F」のとき取得できる値は「300」となるようなので、
  これらの逆変換を行って画面表示に合わせた値を出力します。

* ATS (補正無)

  TRAIN CREW の ATS の情報を出力します。
  前述の逆変換を行わず、取得した値をそのまま出力します。

* 制限速度

  TRAIN CREW の制限速度の情報を出力します。
  制限速度変化の予告が出ている場合は、その情報も出力します。

* 制限速度 (予告無)

  TRAIN CREW の制限速度の情報を出力します。
  制限速度変化の予告の情報は出力せず、現在の制限速度のみを出力します。

## マスコンとブレーキの変換結果の出力タイミング

TrainConvConnector では、マスコンとブレーキの変換結果を以下のタイミングで出力しています。

* 変換結果が変化した時 (動作モードの変更による変化を含む)
* ゲームの開始を検出した時

TRAIN CREW 側でマスコンやブレーキの操作を行った場合など、
TrainConvConnector 上の表示と TRAIN CREW 上の表示が一致しない可能性があります。

## 関連リンク

* TrainConvConnector
  * TBD (GitHubのURLを貼る)
* TRAIN CREW
  * https://acty-soft.com/traincrew/
  * https://store.steampowered.com/app/1618290/TRAIN_CREW/
* 電車でGo！コントローラー変換器
  * https://autotraintas.hariko.com/
  * ver3.70 のアーカイブ: https://web.archive.org/web/20240529001140/https://autotraintas.hariko.com/

## ライセンス

TrainConvConnector は、MITライセンスです。

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

TrainCrewInput.dll (TRAIN CREW 入出力ライブラリ) は
溝月レイル/Acty様の制作物であり、MITライセンスの対象外です。
TrainCrewInput.dll の解析や改変は禁止されています。
