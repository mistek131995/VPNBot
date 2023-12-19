﻿namespace Core.Model.File
{
    public class File(int id, string tag, string name, byte[] data, string contentType, string version)
    {
        public int Id = id;
        public string Tag = tag;
        public string Name = name;
        public byte[] Data = data;
        public string ContentType = contentType;
        public string Version = version;
    }
}
