using System.IO;
using Windows.Storage;

using Xamarin.Forms;
using XamarinTodo.Services;
using XamarinTodo.UWP;

[assembly: Dependency(typeof(FileHelper))]
namespace XamarinTodo.UWP
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}
