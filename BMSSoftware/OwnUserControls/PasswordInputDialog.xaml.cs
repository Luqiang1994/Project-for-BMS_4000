using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BMSSoftware.OwnUserControls
{
    /// <summary>
    /// PasswordInputDialog.xaml 的交互逻辑
    /// </summary>
    public partial class PasswordInputDialog : UserControl
    {
        private Window _dialogWindow;

        public PasswordInputDialog()
        {
            InitializeComponent();
            InitializeDialog();
        }

        private void InitializeDialog()
        {
            // 创建一个Window来承载UserControl
            _dialogWindow = new Window
            {
                Width = Width,
                Height = Height,
                WindowStyle = WindowStyle.None,
                ResizeMode = ResizeMode.NoResize,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Content = this
            };

            // 绑定关闭事件
            _dialogWindow.Closed += (s, e) =>
            {
                if (Closed != null)
                {
                    Closed(this, EventArgs.Empty);
                }
            };
        }

        public bool? ShowDialog()
        {
            return _dialogWindow.ShowDialog();
        }

        public string EnteredPassword
        {
            get => _enteredPassword;
            private set => _enteredPassword = value;
        }
        private string _enteredPassword;

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            EnteredPassword = PasswordBox.Password;
            _dialogWindow.DialogResult = true;
            _dialogWindow.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _dialogWindow.DialogResult = false;
            _dialogWindow.Close();
        }

        public event EventHandler Closed;
    }
}
