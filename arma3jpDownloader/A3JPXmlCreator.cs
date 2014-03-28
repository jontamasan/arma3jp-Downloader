using System;
using System.IO;
using System.Text;
using System.Xml;

namespace arma3jpDownloader {
    class A3JPXmlCreator {
        XmlTextWriter writer;
        XmlDocument xmlDoc;
        XmlNodeList nodeList;
        private string destFileName;

        /// <summary>
        /// temp.xml 用コンストラクタ
        /// </summary>
        /// <param name="fs"></param>
        public A3JPXmlCreator(FileStream fs) {
            try {
                writer = new XmlTextWriter(fs, Encoding.GetEncoding("utf-8"));
            } catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// stringtable.xml 用コンストラクタ
        /// </summary>
        /// <param name="destFileName"></param>
        public A3JPXmlCreator(string destFileName) {
            try {
                this.destFileName = destFileName;
                this.xmlDoc = new XmlDocument();
                this.xmlDoc.Load(this.destFileName);
                XmlElement rootElement = this.xmlDoc.DocumentElement;
                this.nodeList = rootElement.GetElementsByTagName("Key");
            } catch (Exception) {
                throw;
            }
        }

        internal void StartTempFile(string missionID) {
            try {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement(missionID);
            } catch (Exception) {
                throw;
            }
        }

        internal void WriteTempFile(uint column, string elementString) {
            try {
                switch (column) {
                    case 2:
                        writer.WriteStartElement("key");
                        writer.WriteAttributeString("ID", elementString);
                        break;
                    case 3:
                        writer.WriteElementString("Original", elementString);
                        break;
                    case 4:
                        writer.WriteElementString("English", elementString);
                        writer.WriteEndElement();
                        break;
                }
            } catch (Exception) {
                throw;
            }
        }

        internal void EndTempFile() {
            try {
                writer.WriteEndElement();
                writer.WriteEndDocument();
            } catch (Exception) {
                throw;
            } finally {
                if (writer != null) {
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// stringtable.xml 置換
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="p">置き換え文字</param>
        internal void ReplaceText(string id, string p) {
            try {
                 for (int i = 0; i < nodeList.Count; ++i) {
                    XmlElement node = (XmlElement)this.nodeList.Item(i);

                    if (node.GetAttribute("ID").Equals(id)) {
                        XmlNodeList elementList = node.GetElementsByTagName("English");
                        if (elementList.Item(0).InnerText.Length == 0) {
                            // セルが空だったので原文入れる
                            p = node.GetElementsByTagName("Original").Item(0).InnerText;
                        }
                        elementList.Item(0).InnerText = p;
                    }
                    //Console.WriteLine(elementList.Item(0).InnerText);
                 }
            } catch (XmlException) {
                throw;
            }
        }

        /// <summary>
        /// stringtable.xml 保存
        /// </summary>
        internal void Save() {
            try {
                this.xmlDoc.Save(this.destFileName);
            } catch (XmlException) {
                throw;
            }
        }
    }

}
