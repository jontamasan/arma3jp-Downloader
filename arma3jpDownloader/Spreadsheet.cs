using Google.GData.Client;
using Google.GData.Spreadsheets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace arma3jpDownloader
{
    /// Google スプレッドシートを取得するクラス
    /// https://developers.google.com/google-apps/spreadsheets/

    class Spreadsheet {
        private string USERNAME;
        private string PASSWORD;
        private SpreadsheetsService service;

        public Spreadsheet(string username, string password) {
            this.USERNAME = username;
            this.PASSWORD = password;
            this.service = new SpreadsheetsService("arma3Downloader-v1");

        }

        internal void setUser() {
            // ユーザー設定
            service.setUserCredentials(USERNAME, PASSWORD);
        }

        internal CellFeed fetch(string uri) {
            try {
                // フィードアドレス設定
                FeedQuery singleQuery = new FeedQuery();
                singleQuery.Uri = new Uri(uri);

                AtomFeed feed;
                SpreadsheetEntry spreadsheet;

                // フィード取得
                feed = service.Query(singleQuery); // 認証エラーはここから
                if (feed.Entries.Count == 0) {
                    return null;
                }
                spreadsheet = (SpreadsheetEntry)feed.Entries[0];
                WorksheetFeed wsFeed = spreadsheet.Worksheets;
                WorksheetEntry worksheet = (WorksheetEntry)wsFeed.Entries[0];
                // Fetch the cell feed of the worksheet.
                CellQuery cellQuery = new CellQuery(worksheet.CellFeedLink);
                cellQuery.MinimumRow = 3;
                cellQuery.MinimumColumn = 2;
                cellQuery.MaximumColumn = 4;

                return service.Query(cellQuery);
                
            } catch (InvalidCredentialsException) {
                // 認証エラー
                throw;
            } catch (GDataRequestException) {
                // Execution of request failed: {address}
                throw;
            }
        }
    }
}
