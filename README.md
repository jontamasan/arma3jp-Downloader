Arm3jp Downloaderのソースコード

https://code.google.com/p/google-gdata/downloads/list
↑からGoogle Data APIをダウンロードして
Google.GData.Client.dll
Google.GData.Extensions.dll
Google.GData.Spreadsheets.dll
を参照設定すること。

Googleアカウント用パスワードの保存は軽く暗号化するので
Form1.cs 20行目 string salt に8バイト以上の文字列を設定すること。
Spreadsheet.cs が要のスプレッドシート取得部分です。

参考: Google Spreadsheets API version 3.0
https://developers.google.com/google-apps/spreadsheets/#authorizing_requests_with_clientlogin

※カレントディレクトリに config フォルダとファイルが必要。
