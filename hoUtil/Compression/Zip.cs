﻿using System;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace hoUtils.Compression
{
    public static class Zip
    {
        const string TempDirectory = "hoReqIf";

        /// <summary>
        /// Extract the zip file to an extract directory. If no extractDirectory exists a temp directory is created
        /// </summary>
        /// <param name="zipPath"></param>
        /// <param name="extractPath"></param>
        /// <returns></returns>
        public static string ExtractZip(string zipPath, string extractPath = "")
        {


            // estimate extract path for a temporary dictionars
            if (String.IsNullOrWhiteSpace(extractPath))
            {
                extractPath = Path.GetTempPath();
                extractPath = Path.Combine(extractPath, TempDirectory);
                if (Directory.Exists(extractPath))
                    Directory.Delete(extractPath,recursive:true);
                Directory.CreateDirectory(extractPath);
            }

            // Ensures that the last character on the extraction path
            // is the directory separator char. 
            // Without this, a malicious zip file could try to traverse outside of the expected
            // extraction path.
            if (!extractPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                extractPath += Path.DirectorySeparatorChar;
            using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Update))
            {
                try
                {
                    archive.ExtractToDirectory(extractPath);
                }
                catch (Exception e)
                {
                    MessageBox.Show($@"Achive: '{extractPath}'

{e}", 
                        @"Can't extract *.reqifz file");
                    return "";
                }
               

                return extractPath;
            }
        }

    }

}
