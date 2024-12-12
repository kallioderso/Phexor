using System;
using System.IO;

namespace Phexor.Utilities;

public static class FileTypeHelper
{
    //TODO: vielleicht sowas wie FontAwesome nutzen oder doch auf Bilder gehen?
    public static string GetSymbolForFileType(string fileName)
    {
        var extension = Path.GetExtension(fileName)?.ToLower();
        return extension switch
        {
            ".txt" => "\uE8A5", // Text File
            ".pdf" => "\uE8A1", // PDF File
            ".jpg" or ".jpeg" or ".png" => "\uEB9F", // Image File
            ".doc" or ".docx" => "\uE8A2", // Word Document
            ".xls" or ".xlsx" => "\uE8A0", // Excel File
            ".ppt" or ".pptx" => "\uE8B0", // PowerPoint File
            _ => "\uE7C3" // Default File Icon
        };
    }
}