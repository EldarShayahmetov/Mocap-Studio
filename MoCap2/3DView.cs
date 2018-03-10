using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpGL;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Assets;
using System.Windows.Forms;


using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.Features2D;
using Emgu.CV.CvEnum;

namespace MoCap2
{
    public class _3DView
    {
        OpenGLControl glControl;
        OpenGL gl;
        private float rot = 45;
        private float scale = 0.1f;

        Matrix<double> pose = new Matrix<double>(3,1);
        Matrix<double> points;

        Object locker = new Object();
        


        #region Mouse 
        bool isDown = false;
        bool isRotate = false;
        float lastX;
        float lastY;
        float glX = 0;
        float glY = 0;
        float glZ = -40;
        float ang = 45;
        float angY = 45;
        float angX = 0;
        float MouseDensity = 100;
        float yoffset = 3f;
        #endregion


        public _3DView(OpenGLControl glControl, StereoPair sp)
        {
            gl = glControl.OpenGL;
            glControl.OpenGLDraw += DrawPoints;

            sp.OnTriangulated += TrianPointsHandler;

            glControl.MouseDown += MouseDown;
            glControl.MouseMove += Pan;
            glControl.MouseUp += MouseUp;
            glControl.MouseWheel += MouseWheel;
            glControl.KeyDown += RotateOn;
            glControl.KeyUp += RotateOff;

        }

        private void TrianPointsHandler(TriangulationEventArgs args)
        {
            lock (locker)
            {
                pose = args._campos;
                points = new Matrix<double>(args._points4D.Data);
            }
        }


        private void RotateOn(Object sender ,KeyEventArgs e)
        {
            if(e.KeyValue == 17)
                isRotate = true;
         
        }


        private void RotateOff(Object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 17)
                isRotate = false;
         
        }

        private void MouseDown(Object sender, MouseEventArgs e)
        {
            isDown = true;

            if (e.Button == MouseButtons.Right)
                isRotate = true;

        }

        private void Pan(Object sender, MouseEventArgs e)
        {
            if (isDown)
            {
                float dx = e.X - lastX;
                float dy = e.Y - lastY;

                if (!isRotate)
                {
                    glX += dx / MouseDensity;
                    glY += dy / MouseDensity;
                }
                else
                {
                    if (Math.Abs(dx) > Math.Abs(dy))
                    {

                        angX += dx*10 / MouseDensity;
                    }
                    else
                    {

                        angY += dy*10 / MouseDensity;
                    }
                }

            }
          
            lastX = e.X;
            lastY = e.Y;
        }

        private void MouseUp(Object sender, MouseEventArgs e)
        {
            isDown = false;

            if (e.Button == MouseButtons.Right)
                isRotate = false;
        }


        private void MouseWheel(Object sender, MouseEventArgs e)
        {

            int sign = Math.Sign(e.Delta);
            float dz = 0;
            dz += 2*sign;
            glZ += dz;

        }

        public void DrawPoints(object sender, RenderEventArgs args)
        {

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            IntPtr glQ = gl.NewQuadric();


            //DRAW 3D Points
            #region Draw triang points

            lock (locker)
            {
                if (points != null)
                { 

                    if (points.Cols == 3)
                    {
                        gl.Color(0f, 1f, 0f);
                        gl.PushMatrix();
                        gl.Translate(points[0, 0] / scale, (points[1, 0] / scale) + yoffset, points[2, 0] / scale);
                        gl.Sphere(glQ, 0.15, 20, 20);
                        gl.PopMatrix();

                      
                        gl.PushMatrix();
                        gl.Translate(points[0, 1] / scale, (points[1, 1] / scale) + yoffset, points[2, 1] / scale);
                        gl.Sphere(glQ, 0.15, 20, 20);
                        gl.PopMatrix();

                        gl.PushMatrix();
                        gl.Translate(points[0, 2] / scale, (points[1, 2] / scale) + yoffset, points[2, 2] / scale);
                        gl.Sphere(glQ, 0.15, 20, 20);
                        gl.PopMatrix();

                        DrawWand();

                        DrawWandWidth(5, 5);
                    }

                }
            }
            #endregion


            gl.LoadIdentity();


            

            gl.Translate(glX, -glY, glZ);
            gl.Rotate(angY, 1, 0, 0);
            gl.Rotate(angX, 0, 1, 0);

           // IntPtr glQ = gl.NewQuadric();
         //   gl.Sphere(glQ, 0.5, 20, 20);

            //Start drawing 
            gl.PointSize(4f);
            gl.Begin(OpenGL.GL_POINTS);

            gl.Color(1f, 0f, 0f);
            gl.Vertex(3f, 0f, 0f);

            gl.Color(0f, 1f, 0f);
            gl.Vertex(0f, 3f, 0f);

            gl.Color(0f, 0f, 1f);
            gl.Vertex(0f, 0f, 3f);
            gl.End();


            gl.PointSize(10f);
            gl.Begin(OpenGL.GL_POINTS);
            gl.Color(1f, 0f, 1f);
            gl.Vertex(pose[0,0] * 10, pose[1,0] * 10, pose[2,0] * 10);
            gl.End();


            DrawCoordinates(gl);
            DrawGrid(gl, 20);


           

            gl.Flush();
        }


        void DrawGrid(OpenGL gl, int HALF_GRID_SIZE)
        {
            gl.LineWidth(1f);
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(0.4f, 0.4f, 0.4f);
            for (int i = -HALF_GRID_SIZE; i <= HALF_GRID_SIZE; i++)
            {
                gl.Vertex((float)i, 0, (float)-HALF_GRID_SIZE);
                gl.Vertex((float)i, 0, (float)HALF_GRID_SIZE);

                gl.Vertex((float)-HALF_GRID_SIZE, 0, (float)i);
                gl.Vertex((float)HALF_GRID_SIZE, 0, (float)i);
            }
            gl.End();
        }

        void DrawCoordinates(OpenGL gl)
        {
            gl.LineWidth(8f);
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(1f, 0f, 0f);
            gl.Vertex(0f, 0f, 0f);
            gl.Color(1f, 0f, 0f);
            gl.Vertex(3f, 0f, 0f);


            gl.Color(0f, 1f, 0f);
            gl.Vertex(0f, 0f, 0f);
            gl.Color(0f, 1f, 0f);
            gl.Vertex(0f, 3f, 0f);

            gl.Color(0f, 0f, 1f);
            gl.Vertex(0f, 0f, 0f);
            gl.Color(0f, 0f, 1f);
            gl.Vertex(0f, 0f, 3f);
            gl.End();
        }


        void DrawRays()
        {
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(1f, 1f, 0f);
            gl.Vertex(pose[0, 0] * 10, pose[1, 0] * 10, pose[2, 0] * 10);
            gl.Color(1f, 1f, 0f);
            gl.Vertex(points[0, 0] / scale, (points[1, 0] / scale) + yoffset, points[2, 0] / scale);


            gl.Color(1f, 1f, 0f);
            gl.Vertex(0, 0, 0);
            gl.Color(1f, 1f, 0f);
            gl.Vertex(points[0, 0] / scale, (points[1, 0] / scale) + yoffset, points[2, 0] / scale);
            gl.End();


            gl.Begin(OpenGL.GL_LINES);
            gl.Color(1f, 1f, 0f);
            gl.Vertex(pose[0, 0] * 10, pose[1, 0] * 10, pose[2, 0] * 10);
            gl.Color(1f, 1f, 0f);
            gl.Vertex(points[0, 1] / scale, (points[1, 1] / scale) + yoffset, points[2, 1] / scale);


            gl.Color(1f, 1f, 0f);
            gl.Vertex(0, 0, 0);
            gl.Color(1f, 1f, 0f);
            gl.Vertex(points[0, 1] / scale, (points[1, 1] / scale) + yoffset, points[2, 1] / scale);

            gl.End();


            gl.Begin(OpenGL.GL_LINES);
            gl.Color(1f, 1f, 0f);
            gl.Vertex(pose[0, 0] * 10, pose[1, 0] * 10, pose[2, 0] * 10);
            gl.Color(1f, 1f, 0f);
            gl.Vertex(points[0, 2] / scale, (points[1, 2] / scale) + yoffset, points[2, 2] / scale);


            gl.Color(1f, 1f, 0f);
            gl.Vertex(0, 0, 0);
            gl.Color(1f, 1f, 0f);
            gl.Vertex(points[0, 2] / scale, (points[1, 2] / scale) + yoffset, points[2, 2] / scale);
            gl.End();

        }


        void DrawWand()
        {
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(1f, 1f, 0f);
            gl.Vertex(points[0, 1] / scale, (points[1, 1] / scale) + yoffset, points[2, 1] / scale);
            gl.Color(1f, 1f, 0f);
            gl.Vertex(points[0, 0] / scale, (points[1, 0] / scale) + yoffset, points[2, 0] / scale);


            gl.Color(1f, 1f, 0f);
            gl.Vertex(points[0, 0] / scale, (points[1, 0] / scale) + yoffset, points[2, 0] / scale);
            gl.Color(1f, 1f, 0f);
            gl.Vertex(points[0, 2] / scale, (points[1, 2] / scale) + yoffset, points[2, 2] / scale);

            gl.Color(1f, 1f, 0f);
            gl.Vertex(points[0, 2] / scale, (points[1, 2] / scale) + yoffset, points[2, 2] / scale);
            gl.Color(1f, 1f, 0f);
            gl.Vertex(points[0, 1] / scale, (points[1, 1] / scale) + yoffset, points[2, 1] / scale);
            gl.End();
        }


        void DrawWandWidth(int x, int y)
        {
            double width = Math.Sqrt(Math.Pow(points[0, 0] - points[0, 2], 2) + Math.Pow(points[1, 0] - points[1, 2], 2) + Math.Pow(points[2, 0] - points[2, 2], 2))/scale;
            gl.DrawText(x, y, 1, 1, 1, "Arial", 30, "Wand Width = " + width.ToString());
        }
    }
}
