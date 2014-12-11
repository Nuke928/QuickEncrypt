using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EncryptLib
{
    public static class Encrypt
    {
        public static byte Seed { get; set; }

        static Encrypt()
        {
            Seed = 0;
        }

        public static void EncryptFile(string fileName)
        {
            var fileData = File.ReadAllBytes(fileName);

            int i = 0;
            foreach (byte j in fileData)
            {
                fileData[i] += (byte)Seed;
                i++;
            }

            File.WriteAllBytes(fileName, fileData);
        }

        public static void DecryptFile(string fileName)
        {
            var fileData = File.ReadAllBytes(fileName);

            int i = 0;
            foreach (byte j in fileData)
            {
                fileData[i] -= Seed;
                i++;
            }

            File.WriteAllBytes(fileName, fileData);
        }

        public static async Task EncryptFileAsync(string fileName)
        {
            var fileData = await Task.Run(() => ReadAllFileAsync(fileName));

            int i = 0;
            foreach (byte j in fileData)
            {
                fileData[i] += (byte)Seed;
                i++;
            }

            await WriteAllFileAsync(fileName, fileData);
        }

        public static async Task DecryptFileAsync(string fileName)
        {
            var fileData = await Task.Run(() => ReadAllFileAsync(fileName));

            int i = 0;
            foreach (byte j in fileData)
            {
                fileData[i] -= (byte)Seed;
                i++;
            }

            await WriteAllFileAsync(fileName, fileData);
        }

        static async Task<byte[]> ReadAllFileAsync(string fileName)
        {
            using (var file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            {
                byte[] buff = new byte[file.Length];
                await file.ReadAsync(buff, 0, (int)file.Length);
                return buff;
            }
        }

        static async Task WriteAllFileAsync(string fileName, byte[] data)
        {
            using (var file = new FileStream(fileName, FileMode.Open, FileAccess.Write, FileShare.Write, 4096, true))
            {
                await file.WriteAsync(data, 0, data.Length);
            }
        }
    }
}
