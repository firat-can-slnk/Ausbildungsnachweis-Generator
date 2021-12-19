using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Pickers;

namespace AusbildungsnachweisGenerator.Helper
{
    public static class IOHelper
    {
        public static async Task<string> SelectFolder(List<string> filter = null, string path = null)
        {
            filter ??= new() { "*" };

            var picker = new FolderPicker(); 
            
            AppHelper.InitializeWithWindow(picker);

            filter.ForEach(filter => picker.FileTypeFilter.Add(filter));
            
            if(string.IsNullOrEmpty(path))
                picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            
            var folder = await picker.PickSingleFolderAsync();
            return folder?.Path;
        }
        public static async Task<string> SelectSaveFile(string extension = "*", string filename = "")
        {
            var picker = new FileSavePicker();

            AppHelper.InitializeWithWindow(picker);

            picker.DefaultFileExtension = extension;
            picker.SuggestedFileName = filename;

            var file = await picker.PickSaveFileAsync();
            return file?.Path;
        }
        public static async Task<string> SelectFile(List<string> filters = null)
        {
            filters ??= new() { "*" };

            var picker = new FileOpenPicker();

            AppHelper.InitializeWithWindow(picker);

            filters.ForEach(filter => picker.FileTypeFilter.Add(filter));
            var file = await picker.PickSingleFileAsync();
            return file?.Path;
        }
        public static void OpenWithDefaultProgram(string path)
        {
            using Process fileopener = new Process();

            fileopener.StartInfo.FileName = "explorer";
            fileopener.StartInfo.Arguments = "\"" + path + "\"";
            fileopener.Start();
        }
    }
}
