using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization;
using MirrSharp.Driver;
using System.IO;
using System.Runtime.Serialization.Json;

namespace WebDesktopDaemon
{
    [DataContract]
    public class DesktopSnapshot
    {
        [DataMember]
        public int Width;
        [DataMember]
        public int Height;
        [DataMember]
        public int X;
        [DataMember]
        public int Y;
        [DataMember]
        public string DesktopBase64;
    }
    [CollectionDataContract(Name = "DesktopSnapshots", Namespace = "")]
    public class DesktopSnapshotList : List<DesktopSnapshot>
    {

    }
    public class DesktopUtil
    {
        private static object syncObject = new object();
        private static  DesktopUtil instance;
        public static DesktopUtil Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncObject)
                    {
                        instance = new DesktopUtil();
                        instance.initialDriver();
                    }
                }
                return instance;
            }
        }
        private DesktopUtil()
        {
             _mirror= new DesktopMirror();
           _mirror.DesktopChange += _mirror_DesktopChange;
        }
        readonly DesktopMirror _mirror;//
        private object desktopChangeLock = new object();

        private DesktopSnapshotList decodedImages = new DesktopSnapshotList();
        private int index = 0;
        void _mirror_DesktopChange(object sender, DesktopMirror.DesktopChangeEventArgs e)
        {
            lock (desktopChangeLock)
            {
                System.IO.MemoryStream m = new System.IO.MemoryStream();
                System.Drawing.Image bp = _mirror.GetRegion(e.x1, e.y1, e.x2, e.y2);//new System.Drawing.Bitmap(@“c:/demo.GIF”);
                if (bp != null)
                {
                    bp.Save(m, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] b = m.GetBuffer();
                    string base64string = Convert.ToBase64String(b);
                    DesktopSnapshot s = new DesktopSnapshot();
                    s.X = e.x1;
                    s.Y = e.y1;
                    s.DesktopBase64 = base64string;
                    if (decodedImages.Count < 10)
                    {
                        decodedImages.Add(s);
                    }else
                    {
                        index = index % 10;
                        decodedImages[index] = s;
                    }
                }
            }
        }
        public string GetChangedImages()
        {
            lock (desktopChangeLock)
            {
                MemoryStream stream1 = new MemoryStream();
                DataContractJsonSerializer ser =
                  new DataContractJsonSerializer(typeof(DesktopSnapshotList));
                ser.WriteObject(stream1, this.decodedImages);
                stream1.Position = 0;
                StreamReader sr = new StreamReader(stream1);
                string instanceStr = sr.ReadToEnd();
                this.decodedImages.Clear();
                return instanceStr;
            }
        }
        private void initialDriver()
        {
            _mirror.Load();
            _mirror.Connect();
            _mirror.Start();
        }
        private void unloadDriver()
        {
            _mirror.Stop();
            _mirror.Disconnect();
            _mirror.Unload();
            
        }
    }
}
