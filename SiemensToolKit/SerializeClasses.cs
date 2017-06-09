using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Serializers
{
    [Serializable]
    public class MemoryBinarySerializer
    {
        public byte[] WriteMemory()
        {
            byte[] bytes;
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, this);
                bytes = stream.ToArray();
            }
            return bytes;
        }
        public static object ReadMemory(byte[] bytes)
        {
            object obj = null;
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                //stream.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                BinaryFormatter bf = new BinaryFormatter();
                obj = (object)bf.Deserialize(stream);
            }
            return obj;
        }
    }
    [Serializable]
    public class FileBinarySerializer
    {
        public void Write(string path)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, this);
                }
            }
            catch { }
        }
        public static object Read(string path)
        {
            object obj = new object();
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(path,
                                          FileMode.Open,
                                          FileAccess.Read,
                                          FileShare.Read);
                obj = (object)formatter.Deserialize(stream);
                stream.Close();
            }
            catch { }
            return obj;
        }
    }
    [Serializable]
    public class BinarySerializer : FileBinarySerializer
    {
        public byte[] WriteMemory()
        {
            byte[] bytes;
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, this);
                bytes = stream.ToArray();
            }
            return bytes;
        }
        public static object ReadMemory(byte[] bytes)
        {
            object obj = null;
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                //stream.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                BinaryFormatter bf = new BinaryFormatter();
                obj = (object)bf.Deserialize(stream);
            }
            return obj;
        }
    }
   
}
