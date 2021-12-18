using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Pickers;

namespace AusbildungsnachweisGenerator.Helper
{
    public static class IOHelper
    {
        public static async Task<string> SelectFolder(List<string> filter = null)
        {
            filter ??= new() { "*" };

            // Need to get the hwnd (“window” is a pointer to a WinUI Window object). 
            // WinRT.Interop namespace is provided by C#/WinRT projected interop wrappers for .NET 5+
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.Window);

            var picker = new FolderPicker();

            // Need to initialize the picker object with the hwnd / IInitializeWithWindow 
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

            // Now we can use the picker object as normal
            filter.ForEach(filter => picker.FileTypeFilter.Add(filter));
            var folder = await picker.PickSingleFolderAsync();
            return folder?.Path;
        }
        public static async Task<string> SelectSaveFile(string extension = "*", string filename = "")
        {

            // Need to get the hwnd (“window” is a pointer to a WinUI Window object). 
            // WinRT.Interop namespace is provided by C#/WinRT projected interop wrappers for .NET 5+
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.Window);

            var picker = new FileSavePicker();

            // Need to initialize the picker object with the hwnd / IInitializeWithWindow 
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

            // Now we can use the picker object as normal
            picker.DefaultFileExtension = extension;
            picker.SuggestedFileName = filename;

            var file = await picker.PickSaveFileAsync();
            return file?.Path;
        }
        public static async Task<string> SelectFile(List<string> filters = null)
        {
            filters ??= new() { "*" };

            // Need to get the hwnd (“window” is a pointer to a WinUI Window object). 
            // WinRT.Interop namespace is provided by C#/WinRT projected interop wrappers for .NET 5+
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.Window);

            var picker = new FileOpenPicker();

            // Need to initialize the picker object with the hwnd / IInitializeWithWindow 
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

            // Now we can use the picker object as normal
            filters.ForEach(filter => picker.FileTypeFilter.Add(filter));
            var file = await picker.PickSingleFileAsync();
            return file?.Path;
        }
    }
}
