using BMSSoftware.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BMSSoftware
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ChangeInfo.Service = new ChangeService();
            ChangeInfo.Service.OnChangeLanguage += ChangeInfo_Service_OnChangeLanguage;
        }

        private void ChangeInfo_Service_OnChangeLanguage(string language)
        {
            ChangeLanguage(language);
        }

        public void ChangeLanguage(string language)
        {
            if (language == "Chinese")
            {
                var path = "pack://application:,,,/Languages.Sources;component/Chinese.xaml";
                Resources.MergedDictionaries[3].Source = new Uri(path);
            }
            else if (language == "English")
            {
                var path = "pack://application:,,,/Languages.Sources;component/English.xaml";
                Resources.MergedDictionaries[3].Source = new Uri(path);
            }
        }
    }
}
