using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace MoCap2
{

    class BlobDetectorEventArgs: EventArgs
    {
        public PointF[] _points { get; private set; }
        public int _deviceNum { get; private set; }

        public BlobDetectorEventArgs(PointF[] points, int deviceNum)
        {
            _points = points;
            _deviceNum = deviceNum;
        }

    }
}
