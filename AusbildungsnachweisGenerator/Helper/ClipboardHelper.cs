using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace AusbildungsnachweisGenerator.Helper
{
    public static class ClipboardHelper
    {
        public static void Set(string s)
        {
            var dp = new DataPackage();
            dp.SetText(s);
            Clipboard.SetContent(dp);
        }
        public static async Task<string> GetStringAsync() => await GetContent()?.GetTextAsync();
        public static DataPackageView GetContent() => Clipboard.GetContent();
    }
}
