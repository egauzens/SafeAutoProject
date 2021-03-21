using System;

namespace SafeAutoProject
{
    public class Trip
    {
        public Trip(string name, double start, double stop, double miles)
        {
            DriverName = name;
            StartTime = start;
            StopTime = stop;
            MilesDriven = miles;

            SetTripLengthInHours();
        }
        public string DriverName { get; private set; }

        // Format is HH.MM Using 24 hr clock notation where the decimal point is the hour minute separator i.e. 23.15 equates to 11:15 PM
        public double StartTime { get; private set; }
        // Format is HH.MM Using 24 hr clock notation where the decimal point is the hour minute separator i.e. 23.15 equates to 11:15 PM
        public double StopTime { get; private set; }

        public double MilesDriven { get; private set; }

        public double Mph { get; private set; }

        public double TripLengthInHours { get; private set; }

        private void SetTripLengthInHours()
        {
            var time = StopTime - StartTime;
            var minutes = time % 1 * 100;
            TripLengthInHours = (int)(time / 1) + minutes / 60.0;
        }
    }
}
