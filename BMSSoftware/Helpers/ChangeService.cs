using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSSoftware.Helpers
{
    public delegate void ChangeLanguageDelegate(string language);
    public class ChangeService
    {
        public event ChangeLanguageDelegate OnChangeLanguage;
        public void ChangeLanguagePublish(string language)
        {
            if (OnChangeLanguage != null)
            {
                OnChangeLanguage(language);
            }
        }
    }

    public class ChangeInfo
    {
        public static ChangeService Service { get; set; }
    }
}
