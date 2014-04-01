/////////////////////////////////
//    Arma3jp Downloader   //
//    Ver 1.0                         //
/////////////////////////////////
Arma3 日本語化作業シートから翻訳ファイルを一括ダウンロードして言語ファイルを作成します。

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
　stringtable.xml にリネームして各自pboファイルに取り込んでください。
　
　参考
　pboファイル作成: Arma3 日本語化wiki - 翻訳・日本語化方法
　http://www59.atwiki.jp/arma3jptranslation/pages/19.html

◎仕様
　config/stringtable_[パッケージ名].xml とIDを照合して翻訳を反映したxmlファイルを作成します。

　翻訳シートが増えた場合は以下のフォーマットで config/settings.xml に追加します。

　・スプレッドシートの指定
　　<feedKey> Googleスプレッドシートのkey </feedKey>
　　https://docs.google.com/spreadsheet/ccc?key=「ここが key になります」&usp=drive_web#gid=0

　・パッケージ名の指定
　　<Key name=" パッケージ名 ">
　　ここで指定した文字列が作成されるファイルに接頭辞として付加されます。
　　オリジナル stringtable.xml の先頭にある <Package name=""> を参考にするといいでしょう。

　【例】
　========
　　「Survive - Drawdown 2031」と「Survive - Situation Nomal」を (Languagemissions_EPA)stringtable.xml として作成する。

	    <Key name="Languagemissions_EPA">
	          <feedKey>0AoIUQevB0Yo9dHlwaTN4ZnhUckV3RWlibjZuQWY3RWc</feedKey>
	    </Key>
	    <Key name="Languagemissions_EPA">
	          <feedKey>0AoIUQevB0Yo9dGs0ZTJDYTZ4YlA3Wkw2dXdFYXN5TlE</feedKey>
	    </Key>
　========
　　「Adapt - Signal Lost」を (Languagemissions_EPB)stringtable.xml として作成する。

	    <Key name="Languagemissions_EPB">
	          <feedKey>0AoIUQevB0Yo9dDNENFpmQ0Itd0xoeDdVWXUxUkVCMEE</feedKey>
	    </Key>
　=========

　・あらかじめ config フォルダには
　　languagemissions_f_epa.pbo と languagemissions_f_epb.pbo のオリジナル stringtable.xml から、
　　stringtable_Languagemissions_EPA.xml と stringtable_Languagemissions_EPB.xml
　　として用意してあります。
　　新たに翻訳するファイルが増えた場合には stringtable_[パッケージ名].xml として各自用意してください。

◎免責事項その他
　・当プログラムを使用した事によるいかなる損害も作者は責任を負いません。
　・自己責任の上でご使用ください。
　・当ソフトで入力されたGoogleアカウント情報はGoogleスプレッドシートの取得にのみ使われます。
