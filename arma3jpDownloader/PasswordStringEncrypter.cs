using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace arma3jpDownloader {
    /// <summary>
    /// パスワードで文字列を暗号化するクラス
    /// http://koganegames.blog.fc2.com/blog-entry-12.html
    /// </summary>
    public static class PasswordStringEncrypter {
        /// <summary>
        /// 文字列を暗号化する
        /// </summary>
        /// <param name="sourceString">暗号化する文字列</param>
        /// <param name="saltString">パスワードに付加する文字</param>
        /// <param name="password">暗号化に使用するパスワード</param>
        /// <returns>暗号化された文字列</returns>
        public static string EncryptString(string sourceString, string saltString, string password) {
            // RijndaelManagedオブジェクトを作成する
            RijndaelManaged rijndael = new RijndaelManaged();

            // パスワードから共有キーと初期化ベクタを作成する
            byte[] key, iv;

            GenerateKeyFromPassword(
                saltString, password, rijndael.KeySize, out key, rijndael.BlockSize, out iv);

            rijndael.Key = key;
            rijndael.IV = iv;

            // 文字列をバイト型配列に変換する
            byte[] strBytes = Encoding.UTF8.GetBytes(sourceString);

            // 対称暗号化オブジェクトを作成する
            ICryptoTransform encryptor = rijndael.CreateEncryptor();

            // バイト型配列を暗号化する
            // 復号化に失敗すると例外CryptographicExceptionが発生する
            byte[] encBytes = encryptor.TransformFinalBlock(strBytes, 0, strBytes.Length);

            // 閉じる
            encryptor.Dispose();

            // バイト型配列を文字列に変換して返す
            return Convert.ToBase64String(encBytes);
        }

        /// <summary>
        /// 暗号化された文字列を復号化する
        /// </summary>
        /// <param name="sourceString">暗号化された文字列</param>
        /// <param name="saltString">パスワードに付加する文字</param>
        /// <param name="password">暗号化に使用したパスワード</param>
        /// <returns>復号化された文字列</returns>
        public static string DecryptString(string sourceString, string saltString, string password) {
            // RijndaelManagedオブジェクトを作成する
            RijndaelManaged rijndael = new RijndaelManaged();

            // パスワードから共有キーと初期化ベクタを作成する
            byte[] key, iv;

            GenerateKeyFromPassword(
                saltString, password, rijndael.KeySize, out key, rijndael.BlockSize, out iv);

            rijndael.Key = key;
            rijndael.IV = iv;

            // 文字列をバイト型配列に戻す
            byte[] strBytes = Convert.FromBase64String(sourceString);

            // 対称暗号化オブジェクトを作成する
            ICryptoTransform decryptor = rijndael.CreateDecryptor();

            // バイト型配列を復号化する
            byte[] decBytes = decryptor.TransformFinalBlock(strBytes, 0, strBytes.Length);

            // 閉じる
            decryptor.Dispose();

            // バイト型配列を文字列に戻して返す
            return Encoding.UTF8.GetString(decBytes);
        }

        /// <summary>
        /// パスワードから共有キーと初期化ベクタを生成する
        /// </summary>
        /// <param name="saltString">パスワードに付加する文字</param>
        /// <param name="password">基になるパスワード</param>
        /// <param name="keySize">共有キーのサイズ（ビット）</param>
        /// <param name="key">作成された共有キー</param>
        /// <param name="blockSize">初期化ベクタのサイズ（ビット）</param>
        /// <param name="iv">作成された初期化ベクタ</param>
        private static void GenerateKeyFromPassword(string saltString, string password, int keySize, out byte[] key, int blockSize, out byte[] iv) {
            // パスワードから共有キーと初期化ベクタを作成する
            // saltを決める
            byte[] salt = Encoding.UTF8.GetBytes(saltString);

            // Rfc2898DeriveBytesオブジェクトを作成する
            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, salt);

            // 反復処理回数を指定する
            deriveBytes.IterationCount = 1000;

            // 共有キーと初期化ベクタを生成する
            key = deriveBytes.GetBytes(keySize / 8);
            iv = deriveBytes.GetBytes(blockSize / 8);
        }
    }
}
