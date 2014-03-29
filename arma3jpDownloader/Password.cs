using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace arma3jpDownloader {
    /// <summary>
    /// パスワード暗号化復号化管理クラス。
    /// 実際にはPasswordStringEncrypterクラスが行う。
    /// </summary>
    class Password {
        private string sourceString;
        private string password;

        public Password(string username, string password) {
            this.sourceString = password;
            this.password = username;
        }

        // 暗号化
        internal string Encrypt() {
            return PasswordStringEncrypter.EncryptString(sourceString, Salt.salt, password);
        }
        // 復号化
        internal string Decrypt() {
            return PasswordStringEncrypter.DecryptString(sourceString, Salt.salt, password);
        }
    }
}
