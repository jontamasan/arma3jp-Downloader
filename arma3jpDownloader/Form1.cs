﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.GData.Client;
using System.IO;
using System.Xml.Serialization;
using Google.GData.Spreadsheets;
using System.Xml;

namespace arma3jpDownloader
{
    public partial class Form1 : Form
    {
        private Context context = new Context(); // コンフィグ
        private static readonly string configDirPath = Directory.GetCurrentDirectory() + "\\config";
        private static readonly string configFilePath = configDirPath + "\\settings.xml";
        private static readonly string tempFilename = Directory.GetCurrentDirectory() + "\\temp.dat";
        private static readonly string salt = "パスワード暗号化用文字列";


        public Form1()
        {
            context.username = "";
            context.password = "";
            context.preserve = false; // CheckBox

            // 設定読込み
             try {
                 using (FileStream fs = File.OpenRead(configFilePath)) {
                    XmlSerializer serializer = new XmlSerializer(typeof(Context));
                    context = (Context)serializer.Deserialize(fs);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message + "\nプログラムを終了します。");
                this.Close();
            }

            // パスワード復号化
            if (context.password.Length != 0) {
                context.password = PasswordStringEncrypter.DecryptString(context.password, salt, context.username);
            }



            InitializeComponent();

            checkBox1.Checked = context.preserve;

            if (context.username.Length != 0) {
                textBox1.Text = context.username;
            }
            if (context.password.Length != 0) {
                textBox2.Text = context.password;
            }
            this.AcceptButton = this.button1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked) {
                // ユーザー情報を保存
                context.username = textBox1.Text;
                context.password = textBox2.Text;
                context.preserve = true;
            } else {
                context.username = "";
                context.password = "";
                context.preserve = false;
            }
            string USERNAME = textBox1.Text;
            string PASSWORD = textBox2.Text;

            Console.WriteLine(this.checkBox1.Checked);
            
            if (context.password.Length != 0) {
                // パスワード暗号化
                context.password = PasswordStringEncrypter.EncryptString(context.password, salt, context.username);
            }

            // 設定ファイル書込み
            try {
                using (FileStream fs = File.Create(configFilePath)) {
                    XmlSerializer serializer = new XmlSerializer(typeof(Context));
                    serializer.Serialize(fs, context);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message + "\nプログラムを終了します。");
                throw;
            }

            Form2 form2 = new Form2();
            form2.Show();
            this.button1.Enabled = false;
            this.button2.Enabled = false;

            Spreadsheet sheet = new Spreadsheet(USERNAME, PASSWORD);
            try {
                // ログイン設定
                sheet.setUser();
                // シート取得
                string feedURI = context.feedURI;
                foreach (Key key in context.keys) {
                    string uri = feedURI + key.feedKey;
                    
                    form2.strParam =  uri + "\r\nフィードを取得中...\r\n";
                   
                    CellFeed cellFeed = sheet.fetch(uri);
                    if (cellFeed == null) {
                        MessageBox.Show(uri + "\r\nスプレッドシートがありません。");
                        break; // abort
                    }

                    // stringtable_original.xmlコピー
                    string originalFileName = configDirPath + "\\stringtable_" + key.ID + ".xml";
                    string destFileName = Directory.GetCurrentDirectory() + "\\(" + key.ID + ")" +"stringtable.xml";
                    if (!File.Exists(destFileName)) {
                        File.Copy(originalFileName, destFileName);
                    }

                    // 中間ファイル作成
                    using (FileStream fs = new FileStream(tempFilename, FileMode.Append, FileAccess.Write)) {
                        A3JPXmlCreator xmlTemp = new A3JPXmlCreator(fs);
                        string missionID = key.ID;
                        xmlTemp.StartTempFile(missionID);

                        // Iterate through each cell, printing its value.
                        foreach (CellEntry cell in cellFeed.Entries) {
                            xmlTemp.WriteTempFile(cell.Column, cell.Value);
                        }
                        xmlTemp.EndTempFile();
                        
                    }

                    form2.strParam = "\r\nstringtable.xml を作成中...\r\n\r\n";

                    // 置換
                    A3JPXmlCreator xmlStringtable = new A3JPXmlCreator(destFileName);
                    XmlTextReader source = new XmlTextReader(tempFilename);
                    while (source.Read()) {
                        string id = "";
                        string p = "";
                        source.MoveToContent();
                        if (source.NodeType == XmlNodeType.Element && source.HasAttributes) {
                            source.MoveToAttribute("ID");
                            id = source.Value; // <Key ID="" />
                            source.MoveToElement();
                            for (int i = 0; i < 4; ++i) {
                                // ノード進める
                                source.Read();
                                source.MoveToContent();
                            }
                            p = source.ReadString(); // <English></English>
                            
                            // stringtable.xml 置換
                            xmlStringtable.ReplaceText(id, p); 
                            
                        }
                    }
                    source.Close();
                    xmlStringtable.Save();

                    // 中間ファイル削除
                    if (File.Exists(tempFilename)) {
                        File.Delete(tempFilename);
                    }
                } // foreach (Key key in context.keys)

                form2.strParam = "終了しました。";

            } catch (InvalidCredentialsException ex) {
                // 認証エラー
                MessageBox.Show(ex.Message + "\r\nログインできません。");
                form2.Close();
            } catch (GDataRequestException ex) {
                // リクエストエラー
                MessageBox.Show(ex.Message + "\r\nフィードが取得できません。プログラムを終了します。");
                this.Close();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message + "\nプログラムを終了します。");
                this.Close();
            } finally {
                this.button1.Enabled = true;
                this.button2.Enabled = true;
                // 中間ファイル削除
                if (File.Exists(tempFilename)) {
                    File.Delete(tempFilename);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }


    }
}
