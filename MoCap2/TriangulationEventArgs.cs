using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.Features2D;
using Emgu.CV.CvEnum;

namespace MoCap2
{
    public class TriangulationEventArgs : EventArgs
    {
       public Matrix<double> _points4D { get;}
       public Matrix<double> _campos { get; }
        public Matrix<double> _camrot { get; }

        public TriangulationEventArgs(Matrix<double> points4D, Matrix<double> campos, Matrix<double> camrot)
        {
            _points4D = points4D;
            _campos = campos;
            _camrot = camrot;
        }
        

    }
}
