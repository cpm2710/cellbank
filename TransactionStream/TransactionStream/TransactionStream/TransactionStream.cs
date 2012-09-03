using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionStream
{
    public class TransactionFileStream : Stream
    {
        private FileStream fileStream;
        private const String ORIGIN_CONSTANT = "ORIGIN";
        private const String FILE_FLAG_CONSTANT = "{7B8ED1B5-14BE-4728-9359-D86D9AC90641}";
        private String BACKED_UP_PATH;
        private String LOG_PATH;
        private String PATH;
        public TransactionFileStream(string path, FileAccess fileAccess)
        {
            PATH = path;
            BACKED_UP_PATH = Path.Combine(new string[] { path + ORIGIN_CONSTANT });
            LOG_PATH = Path.Combine(new string[] { path + FILE_FLAG_CONSTANT });
            switch (fileAccess)
            {
                case FileAccess.Read:
                    {
                        fileStream = new FileStream(PATH, FileMode.OpenOrCreate, fileAccess);
                        break;
                    }
                case FileAccess.Write:
                    {
                        
                        if (!File.Exists(path))
                        {
                            File.Create(path);
                        }
                        if (!File.Exists(path) && File.Exists(BACKED_UP_PATH))
                        {
                            recoverBackup();
                        }
                        else
                        {
                            fileStream = new FileStream(LOG_PATH, FileMode.OpenOrCreate, fileAccess);
                        }
                        break;
                    }
                default:{
                    throw new Exception();
                }
            }
           
        }
        public override void Close()
        {
            fileStream.Close();
            Commit();
        }
        private void Commit()
        {
            if (File.Exists(PATH) && File.Exists(BACKED_UP_PATH))
            {
                File.Delete(BACKED_UP_PATH);
            }
            File.Move(PATH, BACKED_UP_PATH);

            File.Move(LOG_PATH, PATH);
            if (File.Exists(BACKED_UP_PATH))
            {
                File.Delete(BACKED_UP_PATH);
            }
        }


        private void recoverBackup()
        {
            File.Move(BACKED_UP_PATH, PATH);
        }
        public override bool CanRead
        {
            get { return this.fileStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return this.fileStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return this.fileStream.CanWrite; }
        }

        public override void Flush()
        {
            fileStream.Flush();
            //throw new NotImplementedException();
        }

        public override long Length
        {
            get { return fileStream.Length; }
        }

        public override long Position
        {
            get
            {
                return fileStream.Position;
            }
            set
            {
                fileStream.Position = value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return fileStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return fileStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            fileStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            fileStream.Write(buffer, offset, count);
        }

    }
}
