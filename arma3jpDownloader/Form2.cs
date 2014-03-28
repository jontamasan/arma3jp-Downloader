using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace arma3jpDownloader {
    /// <summary>
    /// ログ表示フォーム
    /// </summary>
    public partial class Form2 : Form {
        private string _strParam;

        public Form2() {
            InitializeComponent();
        }

        public string strParam {
            set { 
                _strParam = value;
                textBox1.AppendText(_strParam);
            }
            get {
                return _strParam;
            }
        }
    }
}
