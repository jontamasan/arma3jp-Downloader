/////////////////////////////////
//      Arma3jp Downloader     //
//      Ver 2.0                //
/////////////////////////////////
Arma3 日本語化作業シートから翻訳ファイルを一括ダウンロードして言語ファイルを作成します。
Ver2ではpboファイルの作成も行います。

◎動作環境
　・.NET Framework 4.5で記述しています。
　・Windows7pro 64bitで動作確認を行っています。

◎インストール
　ダウンロードしたファイルを解凍してください。

◎アンインストール
　フォルダとファイルを削除してください。

◎使用方法
　Googleアカウントを入力してスタートボタンを押してしばらくすると
　実行ファイルと同じ場所に (パッケージ名)stringtable.xml が出来上がります。
　同時にpboも作成されます。
　
◎仕様
　config/stringtable_[パッケージ名].xml とIDを照合して翻訳を反映したxmlファイルを作成します。

　翻訳シートが増えた場合は以下のフォーマットで config/settings.xml に追加します。

　・スプレッドシートの指定
　　<feedKey> Googleスプレッドシートのkey </feedKey>
　　https://docs.google.com/spreadsheet/ccc?key=＜ここが key になります＞&usp=drive_web#gid=0

　・パッケージ名の指定
　　<Key name=" パッケージ名 ">
　　ここで指定した文字列が作成されるPBOファイル名になります。

　【例】
　========
　　「Survive - Drawdown 2031」と「Survive - Situation Nomal」を languagemissions_f_epa.pbo として作成する。

	    <Key name="languagemissions_f_epa">
	          <feedKey>0AoIUQevB0Yo9dHlwaTN4ZnhUckV3RWlibjZuQWY3RWc</feedKey>
	          <feedKey>0AoIUQevB0Yo9dGs0ZTJDYTZ4YlA3Wkw2dXdFYXN5TlE</feedKey>
	    </Key>
　========
　　「Adapt - Signal Lost」を languagemissions_f_epb.pbo として作成する。

	    <Key name="languagemissions_f_epb">
	          <feedKey>0AoIUQevB0Yo9dDNENFpmQ0Itd0xoeDdVWXUxUkVCMEE</feedKey>
	    </Key>
　=========

　・あらかじめ config フォルダにはゲームオリジナルの
　　languagemissions_f_epa.pbo と 
　　languagemissions_f_epb.pbo と
　　languagemissions_f_epc.pbo それぞれ stringtable.xml を抽出して、

　　stringtable_languagemissions_f_epa.xml と
　　stringtable_languagemissions_f_epa.xml と
　　stringtable_languagemissions_f_epc.xml が用意してあります。

　　新たに翻訳するファイルが増えた場合には stringtable_[パッケージ名].xml として各自用意してください。

◎更新履歴
　・2014/03/08 Ver 1.0 公開
　・2014/04/01 Ver 2.0 公開　pboファイル作成対応

◎免責事項その他
　・当プログラムを使用した事によるいかなる損害も作者は責任を負いません。
　・自己責任の上でご使用ください。
　・当ソフトで入力されたGoogleアカウント情報はGoogleスプレッドシートの取得にのみ使われます。
　・営利目的を除いて再配布は自由です。改変する場合はソースコードも含めてください。

◎日本語化作業所シート
　https://docs.google.com/spreadsheet/ccc?key=0AoIUQevB0Yo9dHE5MkFwU1d3ekFLRGVudDROQmp4d2c

◎ソースコード
　https://github.com/jontamasan/arma3jp-Downloader/tree/v2.0

@jontamasan