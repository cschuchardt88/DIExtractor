using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIExtractor
{
    public class DIExtract
    {
        private readonly string _filename;
        private readonly string _dirname;

        public uint TotalFiles { get; set; }
        public IList<DIFileInfo> FileNames { get; set; }

        public DIExtract()
        {
            FileNames= new List<DIFileInfo>();
        }

        public DIExtract(string filename) : this()
        {
            _filename = Path.GetFileNameWithoutExtension(filename);
            _dirname = Path.GetDirectoryName(filename);
            using var fs = File.OpenRead(filename);
            using var br = new BinaryReader(fs);

            DIFileInfo dif = null;

            fs.Seek(4, SeekOrigin.Begin);

            this.TotalFiles = br.ReadUInt32();

            for (int i = 0; i < this.TotalFiles; i++)
            {
                var name_length = br.ReadUInt16();
                var tmp = br.ReadBytes(name_length);

                dif = new DIFileInfo()
                {
                    Filename = System.Text.Encoding.UTF8.GetString(tmp),
                    Offset = br.ReadUInt32(),
                    Length = br.ReadUInt32(),
                    Index= br.ReadUInt32() / 2,
                };
                this.FileNames.Add(dif);
            }
        }

        public void ExtractFile(int index)
        {
            var dif = FileNames[index];
            if (dif.Length== 0) { return; }
            var pindex = dif.Index == 0 ? "" : dif.Index.ToString();
            var mpkName = $"{_filename}{pindex}.mpk";

            byte[] buffer = new byte[dif.Length];
            using var fs = File.OpenRead(Path.Combine(_dirname, mpkName));

            fs.Seek(dif.Offset, SeekOrigin.Begin);
            fs.Read(buffer, 0, buffer.Length);
            fs.Close();

            Directory.CreateDirectory(Path.GetDirectoryName(Path.Combine(_dirname, dif.Filename)));
            File.WriteAllBytes(Path.Combine(_dirname, dif.Filename), buffer);
            buffer = null;
        }
    }
}
