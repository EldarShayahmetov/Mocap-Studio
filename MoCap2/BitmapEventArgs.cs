using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoCap2
{
    class BitmapEventArgs: EventArgs
    {
        public Bitmap _bitmap { get; private set;}
        public int _fps { get; private set; }
        public int _deviceNum { get; private set; }

        public BitmapEventArgs(Bitmap bitmap, int fps, int deviceNum)
        {
            _bitmap = bitmap;
            _fps = fps;
            _deviceNum = deviceNum;
        }
    }
}
