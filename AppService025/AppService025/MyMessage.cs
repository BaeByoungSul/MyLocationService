using System;
using System.Collections.Generic;
using System.Text;

namespace AppService025
{
    public class MyStartMessage
    {
        public int IntervalSecond { get; set; }
    }
    public class MyStopMessage
    {

    }
    public class MyLocationMessage
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude  { get; set; }
        public DateTime PointTime { get; set; }

    }
    public class MyLocationErrorMessage
    {
        public string   ErrorMessage{ get; set; }
        public DateTime ErrorTime { get; set; }

    }


}
