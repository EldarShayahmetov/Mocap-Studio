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

        public BitmapEventArgs(Bitmap bitmap)
        {
            _bitmap = new Bitmap(bitmap);
        }
    }
}
