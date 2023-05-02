using Firebase.Database;
using Firebase.Database.Query;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace TeamVaxxers
{
    public partial class ParkingLot : Form
    {


        FirebaseClient client = new FirebaseClient("https://heymotocarro-1a1d4.firebaseio.com/");
        Graphics G;//for Graphics


        public ParkingLot() //For Compnenet 
        {
            InitializeComponent();

        }

        //Loading Parking lot interface
        private void ParkingLot_Load_1(object sender, EventArgs e)
        {
            G = this.CreateGraphics();
        }


        //p1-2 are for sensors since they are each at the corners
        //r1-r3 are for the beacons 
        //missing a refernce point for one of the beacons but thats fine (hopefully)
        public static Point Trilaterate(Point p1, Point p2, Point p3, int r1, int r2, int r3)
        {
           
            int A = 2 * (p2.X - p1.X);
            int B = 2 * (p2.Y - p1.Y);
            int C = r1 * r1 - r2 * r2 - p1.X * p1.X + p2.X * p2.X - p1.Y * p1.Y + p2.Y * p2.Y;
            int D = 2 * (p3.X - p2.X);
            int E = 2 * (p3.Y - p2.Y);
            int F = r2 * r2 - r3 * r3 - p2.X * p2.X + p3.X * p3.X - p2.Y * p2.Y + p3.Y * p3.Y;
            int x = (C * E - F * B) / (E * A - B * D);
            int y = (C * D - A * F) / (B * D - A * E);
            return new Point(x, y);
        }

        private async Task getBeaconDataAsync() // grabs population from database 
        {
            //******************** Get initial list of beacons ***********************//
            var BeaconsSet = await client
               .Child("Beacons/")//Prospect list
               .OnceSingleAsync<Beacons>();

            //******************** Get changes on beacons ***********************//
            onChildChanged();
            await Task.Delay(Timeout.Infinite);

        }

        private async Task getMapDataAsync() // grabs population from database 
        {
            //******************** Get initial list of Map points ***********************//
            var parkingMapData = await client
               .Child("ParkingMap/")//Prospect list
               .OnceSingleAsync<ParkingMap>();

            //******************** Get changes of Parking Map ***********************//
            onParkingMapChanged();
            await Task.Delay(Timeout.Infinite);

        }

        private async Task getSensorDataAsync() // grabs population from database 
        {
            //******************** Get initial list of beacons ***********************//
            var SensorSet = await client
               .Child("Sensors/")//Prospect list
               .OnceSingleAsync<Sensors>();

            //******************** Get changes on beacons ***********************//
            onSensorChildChanged();
            await Task.Delay(Timeout.Infinite);

        }


        /*Get Beacon Information in the console*/
      //  private Point beaconPoint;

        private void onChildChanged() // Waits for data base to start with variable
        {
            var child = client.Child("Beacons/data");
            var observable = child.AsObservable<Beacon>();
            var subscription = observable.Subscribe(x =>
                {
                    //List Beacon Information on Console

                    Console.WriteLine($"Beacon {x.Object.Id} changed:");
                    Console.WriteLine($"  D1: {x.Object.D1}");
                    Console.WriteLine($"  D2: {x.Object.D2}");
                    Console.WriteLine($"  D3: {x.Object.D3}");
                    Console.WriteLine($"  D4: {x.Object.D4}");
                    Console.WriteLine($"  X: {x.Object.X}");
                    Console.WriteLine($"  Y: {x.Object.Y}");
                    Console.WriteLine();


                    //wasn't able to dynamically get the points for sensors indivdually
                    Point sensor1 = new Point(0, 0);
                    Point sensor2 = new Point(108, 0);
                    Point sensor3 = new Point(108, 108);

                   
                    var child1 = client.Child("Sensors/data");
                    var sobservable = child1.AsObservable<Sensor>();
                    var subscription2 = sobservable.Subscribe(s =>
                    {
                       

                        var child3 = client.Child("ParkingMap/data");
                        var observable3 = child3.AsObservable<ParkingMap>();
                        var subscription3 = observable3.Subscribe(x3 =>
                        {


                            for (int i = 0; i < 4; i++) //rectangles dont have more than 5 points (0-3)
                            {
                                Console.WriteLine($"X{i}: {x3.Object.Position[i].X}, Y{i}: {x3.Object.Position[i].Y}");
                            }


           
                                 if (nextID > maxID)
                                {
                                    nextID = 1; // reset the ID to 1
                                }
                                x3.Object.ID = nextID++; // assign the ID and increment nextID


                                 Console.WriteLine($"    Rectangle ID: {x3.Object.ID}"); //Have rectangle ID printed in the Console




                            // add the new ParkingSlot object to the list
                            /****************Getting the measurments for the Rectangle**********************/
                            /*
                             *  *<-Pos[3]              *<-Pos[2]
                             * 
                             * 
                             * 
                             *  * <-(X,Y) Pos[0]       * <-Pos[1]
                             * 
                             * )
                             */


                            int xvalue = x3.Object.Position[0].X;
                            int yvalue = x3.Object.Position[0].Y;


                            //each rectangle is 36 points in width
                            //and 48 points in height
                            int width = (x3.Object.Position[2].X - x3.Object.Position[0].X);
                            int height = (x3.Object.Position[2].Y - x3.Object.Position[0].Y);
                            Graphics graphics1 = CreateGraphics();
                            Pen pen1 = new Pen(Color.DarkOrange, 5);
                            graphics1.DrawRectangle(pen1, xvalue, yvalue, width, height); //Dtawing rectangle

                            ParkingSlot parkingSlot = new ParkingSlot(nextID, xvalue, yvalue, width, height);

                            Console.WriteLine($"Estimate Location for Beacon  {x.Object.Id} ");
                            Point estimatedLocation = Trilaterate(sensor1, sensor2, sensor3, Convert.ToInt32(x.Object.D1), Convert.ToInt32(x.Object.D2), Convert.ToInt32(x.Object.D3)); ;
                            Console.WriteLine(estimatedLocation);


                            int estimatedX = estimatedLocation.X - xvalue;
                            int estimatedY = estimatedLocation.Y - yvalue;

                            // Display rectangle and estimated point
                            Graphics graphics = CreateGraphics();
                            Brush brush = new SolidBrush(Color.Red);
                            Pen pen = new Pen(Color.DarkOrange, 5);
                            graphics.DrawRectangle(pen, xvalue, yvalue, width, height);

                            graphics.FillEllipse(brush, xvalue + estimatedX - 5, yvalue + estimatedY - 5, 10, 10);

                        });

                    });
               

                });




        }



        /*Will get information from the Parking Map to display in console
         Will show 6 charts with the 4 points for the placement of each slot*/

        private int nextID = 0; //To get ID number for each Beacon 
        private int maxID = 6;//because there are 6 parking slots
        List<ParkingSlot> parkingSlots = new List<ParkingSlot>();
        private void onParkingMapChanged() // Waits for data base to start with variable
        {

            var child = client.Child("ParkingMap/data");
            var observable = child.AsObservable<ParkingMap>();
            var subscription = observable.Subscribe(x =>
            {


                for (int i = 0; i < 4; i++) //rectangles dont have more than 5 points (0-3)
                {
                    Console.WriteLine($"X{i}: {x.Object.Position[i].X}, Y{i}: {x.Object.Position[i].Y}");
                }


                Graphics graphics = CreateGraphics();
                Pen pen = new Pen(Color.DarkOrange, 5);


                //  x.Object.ID = nextID++;//giving each recrangle an ID 

                // inside the loop
                if (nextID > maxID)
                {
                    nextID = 1; // reset the ID to 1
                }
                x.Object.ID = nextID++; // assign the ID and increment nextID


                Console.WriteLine($"    Rectangle ID: {x.Object.ID}"); //Have rectangle ID printed in the Console




                // add the new ParkingSlot object to the list



                /****************Getting the measurments for the Rectangle**********************/
                /*
                 *  *<-Pos[3]              *<-Pos[2]
                 * 
                 * 
                 * 
                 *  * <-(X,Y) Pos[0]       * <-Pos[1]
                 * 
                 * )
                 */


                int xvalue = x.Object.Position[0].X;
                int yvalue = x.Object.Position[0].Y;


                //each rectangle is 36 points in width
                //and 48 points in height
                int width = (x.Object.Position[2].X - x.Object.Position[0].X);
                int height = (x.Object.Position[2].Y - x.Object.Position[0].Y);

                graphics.DrawRectangle(pen, xvalue, yvalue, width, height); //Dtawing rectangle
            
                ParkingSlot parkingSlot = new ParkingSlot(nextID, xvalue, yvalue, width, height);


                // add the new ParkingSlot object to the list


                /*if (x.Object.ID == 1) //If ID =1 it will fill it in
                  {
                      pen = new Pen(Color.Red, 5);
                      SolidBrush brush = new SolidBrush(Color.Red);
                      graphics.FillRectangle(brush, xvalue, yvalue, width, height);
                  }
                  else
                  {
                      pen = new Pen(Color.DarkOrange, 5);
                  }
                  */

            });

        }


        private void onSensorChildChanged() // Waits for data base to start with variable
        {
            var child = client.Child("Sensors/data");
            var observable = child.AsObservable<Sensor>();
            var subscription = observable.Subscribe(x =>
            {
                //  Sensor sensor = new Sensor();
                //  sensor.Position = new Point( x.Object.Position.X }, x.Object.Position.Y);

                Console.WriteLine("--------Sensor Positions:!     ");
                Point p = new Point((int)x.Object.Position.X, (int)x.Object.Position.Y);
                Console.WriteLine($"X: {p.X}, Y: {p.Y}");
             //   Console.WriteLine($"X: {x.Object.Position.X}, Y: {x.Object.Position.Y}");

               // var p1= new Point(x.Object.Position.X , x.Object.Position.Y);
                
             //   Console.WriteLine(p1);
                

            });
        }
                


        /*This is Information for the form*: The Screen Images, Buttons, Boxes, etc */
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParkingLot));
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.button9 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Location = new System.Drawing.Point(29, 235);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(295, 76);
            this.button4.TabIndex = 0;
            this.button4.Text = "Update Parking Lot Statistics\r\n";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Tan;
            this.button5.Location = new System.Drawing.Point(69, 403);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(90, 49);
            this.button5.TabIndex = 1;
            this.button5.Text = "Add";
            this.button5.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Tan;
            this.button6.Location = new System.Drawing.Point(174, 403);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(90, 49);
            this.button6.TabIndex = 2;
            this.button6.Text = "Remove";
            this.button6.UseVisualStyleBackColor = false;
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.Tan;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.comboBox1.Location = new System.Drawing.Point(29, 353);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(295, 28);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.Text = "Select Parking Slot";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(414, 22);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(444, 64);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(126, 690);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(97, 76);
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button9.Location = new System.Drawing.Point(90, 590);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(159, 58);
            this.button9.TabIndex = 11;
            this.button9.Text = "Log Out";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // ParkingLot
            // 
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(938, 771);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Name = "ParkingLot";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ParkingLot_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }




        //Button will update information from firebase and display it in the console
        //Name: Update Parking Lot Statistics 
        private void button4_Click(object sender, EventArgs e)
        {
            InitializeComponent();
            getBeaconDataAsync();
            getMapDataAsync();
            getSensorDataAsync();
            onChildChanged();


        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Graphics graphics = CreateGraphics();
            Pen pen = new Pen(Color.DarkOrange, 5);

            Console.Write("Enter the ID of the parking slot you want to color in: ");
            //  int idToColor = int.Parse(Console.ReadLine());
            // loop through the parkingSlots list and find the ParkingSlot with the matching ID
            
          //   int idToColor = int.Parse(textBox1.Text);
            foreach (ParkingSlot slot in parkingSlots)
            {
                if (slot.ID == 2)
                {
                    SolidBrush brush = new SolidBrush(Color.Blue);
                    graphics.FillRectangle(brush, slot.X, slot.Y, slot.Width, slot.Height);
                    break;
                }


            }

        }

    
     
        //log out button! yayaya 
        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            var loginForm = new Login();
            loginForm.ShowDialog();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }



    }

