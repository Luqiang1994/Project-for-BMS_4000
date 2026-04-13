using BMSSoftware.ViewModels;
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
using System.Windows.Shapes;

namespace BMSSoftware.Views
{
    /// <summary>
    /// CommunityParamsView.xaml 的交互逻辑
    /// </summary>
    public partial class CommunityParamsView : Window
    {
        public CommunityParamsView()
        {
            InitializeComponent();
            this.DataContext = new CommunityParamsViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
