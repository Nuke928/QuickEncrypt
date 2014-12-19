using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EncryptLib;
using NUnit.Framework;
using System.IO;

namespace Encryption
{
    [TestFixture]
    class EncryptionTest
    {
        [Test]
        public void EncryptAndDecrypt()
        {
            Encrypt.Seed = (byte)new Random().Next();
            string contents = "Install Gentoo";
            string fileName = "testfile";
            File.WriteAllText(fileName, contents);

            Encrypt.EncryptFile(fileName);

            Assert.AreNotEqual(File.ReadAllText(fileName), contents, "Encrypted content and decrypted content should not be the same!");

            Encrypt.DecryptFile(fileName);

            Assert.AreEqual(File.ReadAllText(fileName), contents, "Decrypted content should be original!");
        }

        [Test]
        public async void EncryptAndDecryptAsync()
        {
            Encrypt.Seed = (byte)new Random().Next();
            string contents = "Install Gentoo";
            string fileName = "testfile";
            File.WriteAllText(fileName, contents);

            await Encrypt.EncryptFileAsync(fileName);

            Assert.AreNotEqual(File.ReadAllText(fileName), contents, "Encrypted content and decrypted content should not be the same!");

            await Encrypt.DecryptFileAsync(fileName);

            Assert.AreEqual(File.ReadAllText(fileName), contents, "Decrypted content should be original!");
        }

        [TestFixtureTearDown]
        public void Cleanup()
        {
            File.Delete("testfile");
        }
    }
}
