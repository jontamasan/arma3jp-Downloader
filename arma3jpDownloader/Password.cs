
namespace arma3jpDownloader {
    /// <summary>
    /// パスワード暗号化復号化管理クラス。
    /// 実際にはPasswordStringEncrypterクラスが行う。
    /// </summary>
    public static class Password {

        // 暗号化
        public static string Encrypt(string password, string sourceString) {
            return PasswordStringEncrypter.EncryptString(sourceString, Salt.salt, password);
        }
        // 復号化
        public static string Decrypt(string password, string sourceString) {
            return PasswordStringEncrypter.DecryptString(sourceString, Salt.salt, password);
        }
    }
}
