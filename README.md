Arm3jp Downloaderのソースコード

https://code.google.com/p/google-gdata/downloads/list
↑からGoogle Data APIをダウンロードして
Google.GData.Client.dll
Google.GData.Extensions.dll
Google.GData.Spreadsheets.dll
を参照設定すること。

Form1.cs がメイン処理。
Spreadsheet.cs が要のスプレッドシート取得部分です。
参考: Google Spreadsheets API version 3.0
https://developers.google.com/google-apps/spreadsheets/#authorizing_requests_with_clientloginGoogle

アカウント用パスワードの保存は軽く暗号化するので
Salt_Dummy.cs を Salt.cs にリネーム、10行目に8バイト以上の暗号鍵用文字列を設定すること。

※カレントディレクトリに config フォルダとファイルが必要。
