using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shapes
{
    public partial class Form1 : Form
    {
        //Stupid Spelling Proofing
        const bool flase = false;

        //Video Capture
        private VideoCapture _capture;
        private Thread _captureThread;

        //Serial Communication
        SerialPort arduinoSerial = new SerialPort();
        bool enableCoordinateSending = true;
        Thread serialMonitoringThread;

        //Variables
        private bool serial = true;
        private string portName = "COM7";
        private int baudRate = 9600;
        private int cameraNumber = 0;
        private Mat background;

        public Form1()  //Startp everything 
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) //Startup of all subcomponents 
        {
            //Initalize Video Capture
            _capture = new VideoCapture(cameraNumber);
            _captureThread = new Thread(DisplayWebcam);
            _captureThread.Start();


            //Initalize Serial Communication
            if (serial == true)
            {
                try
                {
                    arduinoSerial.PortName = portName;
                    arduinoSerial.BaudRate = baudRate;
                    arduinoSerial.Open();
                    serialMonitoringThread = new Thread(MonitorSerialData);
                    serialMonitoringThread.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Initializing COM port");
                    Close();
                }
            }
        }

        private void DisplayWebcam() //Capture and display video frames 
        {

            while (_capture.IsOpened)
            {
                Mat frame = _capture.QueryFrame();

                int nHeight = (frame.Size.Height * cameraBox.Size.Width) / frame.Size.Width;
                Size nSize = new Size(cameraBox.Size.Width, nHeight);
                CvInvoke.Resize(frame, frame, nSize);

                cameraBox.Image = frame.Bitmap;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) //Stop everything 
        {
            _captureThread.Abort();
            if (serial == true)
            {
                serialMonitoringThread.Abort();
            }

        }

        private void capture_Click(object sender, EventArgs e) //Capture background and display 
        {
            background = _capture.QueryFrame();

            int nHeight = (background.Size.Height * cameraBox.Size.Width) / background.Size.Width;
            Size nSize = new Size(cameraBox.Size.Width, nHeight);
            CvInvoke.Resize(background, background, nSize);

            captureBox.Image = background.Bitmap;
        }

        private void shape_Click(object sender, EventArgs e) //Starts the loop of finding and moving shapes
        {
                if (enableCoordinateSending == true)
                {
                    FindShapes();
                }
        }

        private bool FindShapes()  //Captures current camera frame, and finds shapes, returns false when no shapes are present, otherwise returns true 
        {
            //Declerations
            var blurredImage = new Mat();
            var cannyImage = new Mat();
            var decoratedImage = new Mat();
            double Numshapes = 0;
            int trianglesN = 0, squaresN = 0;
            List<Triangle2DF> triangles = new List<Triangle2DF>();
            List<RotatedRect> rectangles = new List<RotatedRect>();
            //List<MCvBox2D> squares = new List<MCvBox2D>();

            //Capture current frame and resize
            Mat frame = _capture.QueryFrame();

            int nHeight = (frame.Size.Height * cameraBox.Size.Width) / frame.Size.Width;
            Size nSize = new Size(cameraBox.Size.Width, nHeight);
            CvInvoke.Resize(frame, frame, nSize);


            //Find the differnce aganist the background image
            CvInvoke.AbsDiff(frame, background, frame);

            //Blur image and use canny edge to find edges
            CvInvoke.GaussianBlur(frame, blurredImage, new Size(3, 3), 0);
            CvInvoke.CvtColor(blurredImage, blurredImage, typeof(Bgr), typeof(Gray));
            CvInvoke.Canny(blurredImage, cannyImage, 150, 255);
            CvInvoke.CvtColor(cannyImage, decoratedImage, typeof(Gray), typeof(Bgr));


            //Find shapes and outline
            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
            {
                CvInvoke.FindContours(cannyImage, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);

                Numshapes = contours.Size;

                for (int i = 0; i < contours.Size; i++)
                {
                    using (VectorOfPoint contour = contours[i])
                    using (VectorOfPoint approxContour = new VectorOfPoint())
                    {
                        CvInvoke.ApproxPolyDP(contour, approxContour, CvInvoke.ArcLength(contour, true) * 0.05, true);
                        if (approxContour.Size == 3)
                        {
                            trianglesN++;
                            Point[] pts = approxContour.ToArray();
                            triangles.Add(new Triangle2DF(
                               pts[0],
                               pts[1],
                               pts[2]
                               ));
                            CvInvoke.Polylines(decoratedImage, contour, true, new Bgr(Color.Red).MCvScalar);
                        }
                        else if (approxContour.Size == 4 && CvInvoke.ContourArea(approxContour) < 10000)
                        {
                            squaresN++;
                            rectangles.Add(CvInvoke.MinAreaRect(approxContour));
                            CvInvoke.Polylines(decoratedImage, contour, true, new Bgr(Color.Green).MCvScalar);
                        }
                        else
                        {
                            CvInvoke.Polylines(decoratedImage, contour, true, new Bgr(Color.Black).MCvScalar);
                        }

                    }
                }
            }

            double xCoord = 0, yCoord = 0;
            int shape = 0;

            //Output outlined shapes image
            shapesBox.Image = decoratedImage.Bitmap;

            if (trianglesN > 0)
            {
                //Get the coordinate of the first triangle
                xCoord = triangles[0].Centeroid.X;
                yCoord = triangles[0].Centeroid.Y;
                shape = 1;
            }

            else if (squaresN > 0)
            {
                //Get the coordinate of the first rectangle
                xCoord = rectangles[0].Center.X;
                yCoord = rectangles[0].Center.Y;
                shape = 2;
            }

            //No shapes, No data
            else
            {
                xCoord = -1;
                yCoord = -1;
                shape = 0;

                return false;
            }

            CoordinateHandler(yCoord, xCoord, shape);

            return true;

        }

        private void CoordinateHandler(double xCoord, double yCoord, int shape) //Convert rectangular to polar and send to arduino 
        {
            //Definitions
            double angle = 0, radius = 0;

            //No shapes, No data
            if (xCoord == -1 && yCoord == -1)
            {
                angle = -1;
                radius = -1;
            }

            
            else
            {
                //Transform to polar coordinates
                yCoord = 210 - yCoord;

                radius = xCoord;
                xCoord = xCoord + 258.82;
                

                angle = yCoord / xCoord;
                angle = Math.Atan(angle);
                angle = angle * (180 / Math.PI);

                xCoord = radius;

                radius = Math.Sqrt((xCoord * xCoord) + (yCoord * yCoord));
            }
            

            xCoord = Math.Round(xCoord, 2);
            yCoord = Math.Round(yCoord, 2);
            radius = Math.Round(radius, 2);
            angle = Math.Round(angle, 2);

            //Print out Coordinates
            Invoke(new Action(() =>
            {
                xCord.Text = xCoord.ToString();
            })); //Radius Coord
            Invoke(new Action(() =>
            {
                yCord.Text = angle.ToString();
            })); //Angle Coord

            //Send coordinates to arduino
            if (radius > -1 && angle > -1)
            {
                if (serial == true)
                {
                    while (!SendCoord(xCoord, angle, shape)) { }
                }
            }
        }

        private void MonitorSerialData() //Read data in from the arduino 
        {
            while (true)
            {
                //Block until \n character is received, extract command data
                string msg = arduinoSerial.ReadLine();

                //Confirm the string has both < and > characters
                if (msg.IndexOf("<") == -1 || msg.IndexOf(">") == -1)
                {
                    continue;
                }

                //Only read data between < >
                msg = msg.Substring(msg.IndexOf("<") + 1);
                msg = msg.Remove(msg.IndexOf(">"));

                //Ignore if string is empty, then parse the string
                if (msg.Length == 0)
                {
                    continue;
                }
                if (msg.Substring(0, 1) == "S")
                {
                    // command is to suspend, toggle states accordingly:
                    ToggleFieldAvailability(msg.Substring(1, 1) == "1");
                }
            }
        }

        private void ToggleFieldAvailability(bool suspend) //Show visually if arduino is accepting or not 
        {
            Invoke(new Action(() =>
            {
                enableCoordinateSending = !suspend;
                status.Text = $"State: {(suspend ? "Locked" : "Unlocked")}";
            }));
        }

        private bool SendCoord(double xcord, double ycord, int shape) //Send coordinates to the arduino 
        {
            //Make sure arduino is ready to accept data
            if (!enableCoordinateSending)
            {
                MessageBox.Show("Locked");
                return false;
            }

            //Generate buffer to send to arduino
            byte[] buffer = new byte[5]
             {
               Encoding.ASCII.GetBytes("<")[0],
               Convert.ToByte((int)xcord),
               Convert.ToByte((int)ycord),
               Convert.ToByte(shape),
               Encoding.ASCII.GetBytes(">")[0]
             };

            //Send to the arduino
            arduinoSerial.Write(buffer, 0, 5);
            return true;
        }
    }
}
