using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq; //for select

namespace TeamVaxxers
{






    public class Beacon
    {
        public double D1 { get; set; }
        public double D2 { get; set; }
        public double D3 { get; set; }
        public double D4 { get; set; }
        public long Id { get; set; }
        public long Time { get; set; }

        public double X { get; set; } // x coordinate
        public double Y { get; set; } // y coordinate


    }

    public class Beacons
    {

        public int Total { get; set; }
        public Beacon[] data { get; set; }

    }
  
    public class ParkingMap
    {
   
    //    public int Total { get; set; }
        public Position[] Position {get; set;}
       // public List<Position> Positions { get; set; }

        public int ID { get; set; }
     //   public ParkingSlot[] ParkingSlots { get; set; }


    }

    public class Sensor
    {

        public double S1 { get; set; }
        public double S2 { get; set; }
        public double S3 { get; set; }
        public double S4 { get; set; }
     //public Position[] Position { get; set; }

    public Position Position { get; set; }
        //public List<Position> Positions { get; set; }
        public int Total { get; set; }
        public int ID { get; set; }
       // public Sens[] Sensor{ get; set; }


    }


    public class Sensors
    {
        public int Total { get; set; }
        public Sensor[] Data { get; set; }


    }
    public class ParkingSlot
    {


        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public ParkingSlot(int id, int x, int y, int width, int height)
        {
            ID = id;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

    }
    public class Slot
    {
        public Position[] Position { get; set; }
    }

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }


   //     public Position(int x, int y)
     //   {
       //     X = x;
         //   Y = y; 
        //}
    }




    public class Admin
    {
        public string Name;
        

    }


    /*
     *   public class ParkingSlot
    {
        public int SlotNumber { get; set; }
        public List<Position> Position { get; set; }
        public Position[] Positions { get; set; }

        public int ID { get; set; }
        public Slot Slot { get; set; }

    }
     */

    /*
     *  
    public class ParkingMap
    {
   
        public int Total { get; set; }
        public Position[] Position {get; set;}
        public List<Position> Positions { get; set; }

        public int ID { get; set; }
        public ParkingSlot[] ParkingSlots { get; set; }


    }

     */




    /*
     * 
    public class Beacon
    {
        public double D1 { get; set; }
        public double D2 { get; set; }
        public double D3 { get; set; }
        public double D4 { get; set; }
        public long Id { get; set; }
        public long Time { get; set; }

        public double X { get; set; } // x coordinate
        public double Y { get; set; } // y coordinate


    }

    public class Beacons
    {

        public int Total { get; set; }
        public Beacon[] data { get; set; }

    }
  
     */
    /*
    public class Sensor
    {

        public double S1 { get; set; }
        public double S2 { get; set; }
        public double S3 { get; set; }
        public double S4 { get; set; }


        public double X { get; set; } // x coordinate
        public double Y { get; set; } // y coordinate
                                      //  public Point position { get; set; }
                                      //  public Position[] Position { get; set; }
                                      //   public List<Position> Positions { get; set; }
        /*
        int Total { get; set; }

        public Position[] Position { get; set; }
        public List<Position> Positions { get; set; }

        public int ID { get; set; }

        public Sensors[] data { get; set; }

        /*
        // public int Total { get; set; }
        public double S1 { get; set; }
        public double S2 { get; set; }
        public double S3 { get; set; }
        public double S4 { get; set; }
        //     public Position[] positions { get; set; }
        //public int Total { get; set; }
        //public Position[] Position { get; set; }
        public double X { get; set; } // x coordinate
        public double Y { get; set; } // y coordinate
        public Position[] Position { get; set; }
        */
    /*
    }

    public class Sensors
    {
        public int Total { get; set; }
        public Sensor[] data { get; set; }
        //public int Total { get; set; }
        //    public List<> Data { get; set; }
        //public Position[] Position { get; set; }
        //public List<Position> Positions { get; set; }

        //  public Sensor[] data{ get; set; }
    }
    */

    /*
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

     

        public Point(int x, int y)
        {
            X = x;
            Y = y;
  
        }
    }

    */

    /*
    public class Sensor
    {
        public double S1 { get; set; }
        public double S2 { get; set; }
        public double S3 { get; set; }
        public double S4 { get; set; }
        public Point[] position { get; set; } // receives position x and y
    }

    */

    /*public class Sensors
    {
        public int Total { set; get; }  // total sensor
        public Sensor[] data { get; set; }  // get info for every sensor

    }


    */

    /*

    public class Slots
    {
        public Slot Slot1 { get; set; }
        public Slot Slot2 { get; set; }
        public Slot Slot3 { get; set; }
        public Slot Slot4 { get; set; }
        public Slot Slot5 { get; set; }
        public Slot Slot6 { get; set; }

    }

    public class Admin
    {

    }
    public class Password
    {

    }

    /*
    public class ParkingMap
    {
        public int Total { get; set; }
        //public Slot[] data { get; set; }
    }
    */

    /*public class Slot
    {
        public string label { get; set; }
        public Slot[] position { get; set; }

    }
    */

    /*

    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class Sensor
    {

    }

    public class Sensors
    {
        public int Total { get; set; }
        // public Sensor[] data { get; set; }
    }
    */


    public struct Points
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }


    public class Response
    {
        public bool success { get; set; }
        public int index { get; set; }
        public string message { get; set; }
        public string received { get; set; }
        public string companyId { get; set; }
        public string color { get; set; }
        public int[] infected { get; set; }

    }

}
