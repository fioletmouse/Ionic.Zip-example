using System.Collections.Generic;
using System.IO;
using System.Text;
using Ionic.Zip;
using System;

namespace ZipFileIonic
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var compressedFileStream = new MemoryStream())
            {
                // Create an archive
                using (ZipFile zipFile = new ZipFile())
                {
                    // Set encoding for Russian characters
                    zipFile.AlternateEncodingUsage = ZipOption.Always;
                    zipFile.AlternateEncoding = Encoding.GetEncoding(866); 

                    // Swap GetBytes on any List of files
                    foreach (var file in GetBytes())
                    {
                        zipFile.AddEntry(file.FileName, file.FileData);
                    }
                    
                    // zipFile.Save(); - save the archive in stream
                    zipFile.Save(@"C:\Users\TestUser\Documents\TestArchive.zip");
                }
            }
        }

        private static IEnumerable<PdfFile> GetBytes()
        {
            List<PdfFile> byteList = new List<PdfFile>();
            byteList.Add(CreateByteArray("Тест1"));
            byteList.Add(CreateByteArray("Тест2"));
            byteList.Add(CreateByteArray("Тест3"));
            byteList.Add(CreateByteArray("Тест4"));
            byteList.Add(CreateByteArray("Тест5"));
            
            return byteList;
        }

        private static PdfFile CreateByteArray(string p)
        {
            return new PdfFile() { FileData = Encoding.UTF8.GetBytes(p), FileName = p };
        }
    }

    // Structure sample.
    public struct PdfFile
    {
        public string FileName;
        public byte[] FileData;
    }
}