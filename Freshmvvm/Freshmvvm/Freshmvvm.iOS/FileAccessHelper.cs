using System;

namespace Freshmvvm.iOS
{
    public static string GetLocalFilePath(string filename)
    {
        var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        var libFolder = System.IO.Path.Combine(docFolder, "..", "Library");

        if (!System.IO.Directory.Exists(libFolder))
        {
            System.IO.Directory.CreateDirectory(libFolder);
        }

        return System.IO.Path.Combine(libFolder, filename);
    }
}
