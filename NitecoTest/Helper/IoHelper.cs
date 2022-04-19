using System;
using System.IO;
using System.Text;

namespace NitecoTest.Helper
{
    public class IoHelper
    {
        public static void CreateFileObject(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    File.WriteAllText(fileName, "init log");
                }
                else
                {
                    ///
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
        public static async void WriteToTextFile(string filePath, string objectToWrite, bool append = false)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(filePath, append, Encoding.UTF8))
                {
                    await streamWriter.WriteLineAsync(objectToWrite);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.StackTrace}");
            }
        }
        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }
        public static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormater = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormater.Deserialize(stream);
            }
        }
        public static string ReadFile(string fileName)
        {
            try
            {
                using (StreamReader r = new StreamReader(fileName))
                {
                    string str = r.ReadToEnd();
                    return str;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }
    }
}
