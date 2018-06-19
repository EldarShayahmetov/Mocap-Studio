using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
    public enum ViewMode
    {
        DrawAll,
        DrawWand,
        RecordPoints
    }

    struct Ratio
    {
        public static float startScale = 0.1f;
        public static int multiplyier = 1;
        public static int cameraOffsetX = 3;
        public static int cameraOffsetY = 1;
        public static int cameraOffsetZ = 1;
    }

    public delegate void Find(bool find);

    public class _3DView
    {
        OpenGLControl glControl;
        OpenGL gl;
        private float realScale = 1f;
        private float scale = 1f;
        private float wandWidthCalib = 500;
        private bool shot = false;
        private Timer timer = new Timer();
        private Matrix<double> savedPoints;

        public int numberOfPoints = 0;

        public Find find;


        Matrix<double> pose = new Matrix<double>(3, 1);
        Matrix<double> rot = new Matrix<double>(3, 1);
        Matrix<double> points;

        Object locker = new Object();

        public ViewMode mode = ViewMode.DrawWand;

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

            timer.Interval = 30;
            timer.Tick += TimerHandler;
        }

        private void TimerHandler(object sender, EventArgs e)
        {
            if (!shot)
                shot = true;
        }

        public void StartRecord(int interval=30)
        {
            shot = false;
            timer.Interval = interval;
            mode = ViewMode.RecordPoints;
            timer.Start();
        }

        private void TrianPointsHandler(TriangulationEventArgs args)
        {
            lock (locker)
            {
                pose = args._campos;
                rot = args._camrot;
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

            scale = Ratio.multiplyier * Ratio.startScale;

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //DRAW 3D Points
            #region Draw triang points

            lock (locker)
            {
                if (mode == ViewMode.DrawAll)
                    DrawAllTriangPoints();
                else if (mode == ViewMode.DrawWand)
                    DrawWandPoints();
                else if (mode == ViewMode.RecordPoints)
                    if(shot)
                DrawAndRecordPoints();
            }
            #endregion


            gl.LoadIdentity();      

            gl.Translate(glX, -glY, glZ);
            gl.Rotate(angY, 1, 0, 0);
            gl.Rotate(angX, 0, 1, 0);

            DrawCamerasPoses();
            DrawCoordinates(gl);
            DrawGrid(gl, 20);
            gl.Flush();
        }



        void DrawAndRecordPoints()
        {

            if(points.Cols == numberOfPoints && points.Cols != 0)
            {
                find(true);
                Matrix<double> curPoints = new Matrix<double>(numberOfPoints,3);
                for (int i = 0; i < numberOfPoints; i++)
                {
                    IntPtr glQ = gl.NewQuadric();
                    gl.Color(0f, 1f, 0f);
                    gl.PushMatrix();
                    gl.Translate(points[0, i] / scale, (points[1, i] / scale) + Ratio.cameraOffsetY, points[2, i] / scale);
                    gl.Sphere(glQ, 0.15, 20, 20);
                    gl.PopMatrix();

                    curPoints[i, 0] = points[0, i] * wandWidthCalib;
                    curPoints[i, 1] = points[1, i] * wandWidthCalib;
                    curPoints[i, 2] = points[2, i] * wandWidthCalib;
                }

                if(savedPoints == null)
                {
                    savedPoints = new Matrix<double>(curPoints.Data);
                }
                else
                {
                    savedPoints = savedPoints.ConcateVertical(curPoints);
                }
                find(false);
                shot = false;
            }
        }

        public void StopAndSavePoints()
        {
            shot = false;
            mode = ViewMode.DrawAll;
            if (savedPoints != null)
            {
                SaveMatrix(savedPoints, numberOfPoints.ToString() + ".csv");
            }
            numberOfPoints = 0;
            savedPoints = null;
        }

        private void SaveMatrix(Matrix<double> mat, string fileName)
        {
            //SavingResultsToMATLAB
            string[] line = new string[mat.Rows];
            for (int i = 0; i < mat.Rows; i++)
            {
                for (int j = 0; j < mat.Cols; j++)
                {
                    line[i] += ChangeToDot(mat[i, j].ToString());
                    if (j != mat.Cols - 1)
                    {
                        line[i] += ",";
                    }
                }
            }
            string path = "C:\\Users\\Эльдар\\Desktop\\MocapCalibrations\\" + fileName;
            File.WriteAllLines(path, line);
        }

        private string ChangeToDot(string value)
        {
            string dotString = "";
            foreach (char symbol in value)
            {
                dotString += symbol != ',' ? symbol.ToString() : ".";
            }
            return dotString;
        }


        void DrawWandPoints()
        {
            if (points != null)
            {

                if (points.Cols == 3)
                {

                    IntPtr glQ = gl.NewQuadric();

                    gl.Color(0f, 1f, 0f);
                    gl.PushMatrix();
                    gl.Translate(points[0, 0] / scale + Ratio.cameraOffsetX, (points[1, 0] / scale) + Ratio.cameraOffsetY, points[2, 0] / scale + Ratio.cameraOffsetZ);
                    gl.Sphere(glQ, 0.15, 20, 20);
                    gl.PopMatrix();


                    gl.PushMatrix();
                    gl.Translate(points[0, 1] / scale + Ratio.cameraOffsetX, (points[1, 1] / scale) + Ratio.cameraOffsetY, points[2, 1] / scale + Ratio.cameraOffsetZ);
                    gl.Sphere(glQ, 0.15, 20, 20);
                    gl.PopMatrix();

                    gl.PushMatrix();
                    gl.Translate(points[0, 2] / scale + Ratio.cameraOffsetX, (points[1, 2] / scale) + Ratio.cameraOffsetY, points[2, 2] / scale + Ratio.cameraOffsetZ);
                    gl.Sphere(glQ, 0.15, 20, 20);
                    gl.PopMatrix();

                    DrawWandLines();
                    DrawWandWidthText(5, 5);
                }
            }
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
            gl.Vertex(points[0, 0] / scale, (points[1, 0] / scale) + Ratio.cameraOffsetY, points[2, 0] / scale);


            gl.Color(1f, 1f, 0f);
            gl.Vertex(0, 0, 0);
            gl.Color(1f, 1f, 0f);
            gl.Vertex(points[0, 0] / scale, (points[1, 0] / scale) + Ratio.cameraOffsetY, points[2, 0] / scale);
            gl.End();


            gl.Begin(OpenGL.GL_LINES);
            gl.Color(1f, 1f, 0f);
            gl.Vertex(pose[0, 0] * 10, pose[1, 0] * 10, pose[2, 0] * 10);
            gl.Color(1f, 1f, 0f);
            gl.Vertex(points[0, 1] / scale, (points[1, 1] / scale) + Ratio.cameraOffsetY, points[2, 1] / scale);


            gl.Color(1f, 1f, 0f);
            gl.Vertex(0, 0, 0);
            gl.Color(1f, 1f, 0f);
            gl.Vertex(points[0, 1] / scale, (points[1, 1] / scale) + Ratio.cameraOffsetY, points[2, 1] / scale);

            gl.End();


            gl.Begin(OpenGL.GL_LINES);
            gl.Color(1f, 1f, 0f);
            gl.Vertex(pose[0, 0] * 10, pose[1, 0] * 10, pose[2, 0] * 10);
            gl.Color(1f, 1f, 0f);
            gl.Vertex(points[0, 2] / scale, (points[1, 2] / scale) + Ratio.cameraOffsetY, points[2, 2] / scale);


            gl.Color(1f, 1f, 0f);
            gl.Vertex(0, 0, 0);
            gl.Color(1f, 1f, 0f);
            gl.Vertex(points[0, 2] / scale, (points[1, 2] / scale) + Ratio.cameraOffsetY, points[2, 2] / scale);
            gl.End();

        }


        void DrawWandLines()
        {
            if (points != null)
            {
                gl.Begin(OpenGL.GL_LINES);
                gl.Color(1f, 1f, 0f);
                gl.Vertex(points[0, 1] / scale + Ratio.cameraOffsetX, (points[1, 1] / scale) + Ratio.cameraOffsetY, points[2, 1] / scale + Ratio.cameraOffsetZ);
                gl.Color(1f, 1f, 0f);
                gl.Vertex(points[0, 0] / scale + Ratio.cameraOffsetX, (points[1, 0] / scale) + Ratio.cameraOffsetY, points[2, 0] / scale + Ratio.cameraOffsetZ);


                gl.Color(1f, 1f, 0f);
                gl.Vertex(points[0, 0] / scale +Ratio.cameraOffsetX, (points[1, 0] / scale) + Ratio.cameraOffsetY, points[2, 0] / scale + Ratio.cameraOffsetZ);
                gl.Color(1f, 1f, 0f);
                gl.Vertex(points[0, 2] / scale + Ratio.cameraOffsetX, (points[1, 2] / scale) + Ratio.cameraOffsetY, points[2, 2] / scale + Ratio.cameraOffsetZ);

                gl.Color(1f, 1f, 0f);
                gl.Vertex(points[0, 2] / scale + Ratio.cameraOffsetX, (points[1, 2] / scale) + Ratio.cameraOffsetY, points[2, 2] / scale + Ratio.cameraOffsetZ);
                gl.Color(1f, 1f, 0f);
                gl.Vertex(points[0, 1] / scale + Ratio.cameraOffsetX, (points[1, 1] / scale) + Ratio.cameraOffsetY, points[2, 1] / scale + Ratio.cameraOffsetZ);
                gl.End();
            }
        }


        void DrawCamera()
        {
            gl.Begin(OpenGL.GL_TRIANGLES);           // Begin drawing the pyramid with 4 triangles                                           // Front
            gl.Color(1.0f, 0.0f, 0.0f);     // Red
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 1.0f, 0.0f);     // Green
            gl.Vertex(-1.0f, -1.0f, 1.0f);
            gl.Color(0.0f, 0.0f, 1.0f);     // Blue
            gl.Vertex(1.0f, -1.0f, 1.0f);

            // Right
            gl.Color(1.0f, 0.0f, 0.0f);     // Red
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 0.0f, 1.0f);     // Blue
            gl.Vertex(1.0f, -1.0f, 1.0f);
            gl.Color(0.0f, 1.0f, 0.0f);     // Green
            gl.Vertex(1.0f, -1.0f, -1.0f);

            // Back
            gl.Color(1.0f, 0.0f, 0.0f);     // Red
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 1.0f, 0.0f);     // Green
            gl.Vertex(1.0f, -1.0f, -1.0f);
            gl.Color(0.0f, 0.0f, 1.0f);     // Blue
            gl.Vertex(-1.0f, -1.0f, -1.0f);

            // Left
            gl.Color(1.0f, 0.0f, 0.0f);       // Red
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 0.0f, 1.0f);       // Blue
            gl.Vertex(-1.0f, -1.0f, -1.0f);
            gl.Color(0.0f, 1.0f, 0.0f);       // Green
            gl.Vertex(-1.0f, -1.0f, 1.0f);
            gl.End();   // Done drawing the pyramid
        }


        void DrawWandWidthText(int x, int y)
        {
            double width = Math.Sqrt(Math.Pow(points[0, 0] - points[0, 2], 2) + Math.Pow(points[1, 0] - points[1, 2], 2) + Math.Pow(points[2, 0] - points[2, 2], 2))*wandWidthCalib;
            realScale = (float)width / wandWidthCalib;
            gl.DrawText(x, y, 1, 1, 1, "Arial", 30, "Wand Width = " + width.ToString());
        }


        void DrawAllTriangPoints()
        {
            if(points != null)
            {
                for (int i = 0; i < points.Cols; i++)
                {
                    IntPtr glQ = gl.NewQuadric();
                    gl.Color(0f, 1f, 0f);
                    gl.PushMatrix();
                    gl.Translate(points[0, i] / scale + Ratio.cameraOffsetX, (points[1, i] / scale) + Ratio.cameraOffsetY, points[2, i] / scale + Ratio.cameraOffsetZ);
                    gl.Sphere(glQ, 0.15, 20, 20);
                    gl.PopMatrix();
                }
            }
        }

        void DrawCamerasPoses()
        {
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.PushMatrix();
            gl.Scale(0.7, 0.7, 0.7);
            gl.Translate(Ratio.cameraOffsetX, 2 * Ratio.cameraOffsetY, Ratio.cameraOffsetZ);
            gl.Rotate(90, 0, 0);
            DrawCamera();
            gl.PopMatrix();



                  
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.PushMatrix();
            gl.Scale(0.7, 0.7, 0.7);
            gl.Translate(pose[0, 0] * 10+Ratio.cameraOffsetX, pose[1, 0] * 10 + 2 * Ratio.cameraOffsetY, pose[2, 0] * 10+Ratio.cameraOffsetZ);
            gl.Rotate(180 * (float)rot[0, 0], -180 * (float)rot[1, 0], -180 * (float)rot[2, 0]);
            DrawCamera();
            gl.PopMatrix();
            
        }
    }
}
