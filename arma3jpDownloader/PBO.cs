using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace arma3jpDownloader {
    class PBOHeader {
        public Byte[] filename;
        public UInt32 packingMethod;
        public UInt32 originalSize;
        public UInt32 reserved;
        public UInt32 timeStamp;
        public UInt32 dataSize;

        public PBOHeader() {
            filename = Encoding.ASCII.GetBytes("\0");
            packingMethod = 0x56657273;
            originalSize = 0;
            reserved = 0;
            timeStamp = 0;
            dataSize = 0;
        }
        public PBOHeader(UInt32 timeStamp, UInt32 dataSize) {
            filename = Encoding.ASCII.GetBytes("\0stringtable.xml\0");
            packingMethod = 0;
            originalSize = 0;
            reserved = 0;
            this.timeStamp = timeStamp;
            this.dataSize = dataSize;
        }
        public void Write(ref byte[] data, FileStream fs) {
            data = filename;
            fs.Write(data, 0, data.Length);
            data = BitConverter.GetBytes(packingMethod);
            fs.Write(data, 0, data.Length);
            data = BitConverter.GetBytes(originalSize);
            fs.Write(data, 0, data.Length);
            data = BitConverter.GetBytes(reserved);
            fs.Write(data, 0, data.Length);
            data = BitConverter.GetBytes(timeStamp);
            fs.Write(data, 0, data.Length);
            data = BitConverter.GetBytes(dataSize);
            fs.Write(data, 0, data.Length);
        }
        public void WritePadding(FileStream fs) {
            Byte[] zeroData = new Byte[21];
            fs.Write(zeroData, 0, 21);
        }
    }

    public static class PBO {
        internal static void CreatePBO(Key[] keys) {
            foreach (Key key in keys) {
                string pbo_filename = Directory.GetCurrentDirectory() + "\\" + key.ID + ".pbo";
                string xml_filename = Directory.GetCurrentDirectory() + "\\(" + key.ID + ")stringtable.xml";
                try {
                    using (FileStream pbo_fs = new FileStream(pbo_filename, FileMode.Create, FileAccess.ReadWrite))
                    using (FileStream xml_fs = new FileStream(xml_filename, FileMode.Open, FileAccess.Read)) {
                    byte[] data = new byte[xml_fs.Length + 21 + 17 + 20 + 21];
                        // ヘッダ書込み
                        PBOHeader header = new PBOHeader();
                        header.Write(ref data, pbo_fs);

                        // 拡張ヘッダ書込み
                        int size = (int)xml_fs.Length; // xmlファイルサイズ
                        header = new PBOHeader(GetTimeStamp(), (UInt32)size); // 現在時刻
                        header.Write(ref data, pbo_fs);
                        header.WritePadding(pbo_fs);

                        // データボディ書込み
                        int offset = 0;
                        int remain = size;
                        int readSize = 0;
                        data = new byte[size];
                        while (remain > 0) {
                            // 1024バイトづつ読込み
                            readSize = xml_fs.Read(data, offset, Math.Min(1024, remain));
                            offset += readSize;
                            remain -= readSize;
                        }
                        pbo_fs.Write(data, 0, data.Length);
                        pbo_fs.Dispose();
                        // sha1ハッシュ計算
                        WriteHash(pbo_filename);
                    } // using
                } catch (FileNotFoundException ioex) {
                    throw ioex;
                }
            } // foreach (Key key in keys)
        }

        public static UInt32 GetTimeStamp() {
            var timestamp = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (uint)timestamp.TotalSeconds;
        }

        private static void WriteHash(string name) {
            FileStream pbo_fs = new FileStream(name, FileMode.Open, FileAccess.ReadWrite);
            // 1byteパディングデータ
            byte[] data = new byte[1];
            data[0] = 0x0;
            // sha1計算
            SHA1 crypto = new SHA1CryptoServiceProvider();
            byte[] hash = crypto.ComputeHash(pbo_fs);
            //書込み
            pbo_fs.Write(data, 0, data.Length);
            pbo_fs.Write(hash, 0, hash.Length);
            crypto.Clear();
            pbo_fs.Dispose();
        }
    }
}