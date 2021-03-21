using System;
using System.Collections;

namespace SafeAutoProject
{
    public class Driver
    {
        public Driver(string name)
        {
            Name = name;
            Trips = new ArrayList();
        }

        public ArrayList Trips { get; }
        public string Name { get; private set; }

        public void AddTrip(Trip trip)
        {
            Trips.Add(trip);
            TotalMiles += trip.MilesDriven;
            TotalHours += trip.TripLengthInHours;
        }

        public double TotalMiles
        {
            get;
            private set;
        }

        public double TotalHours
        {
            get;
            private set;
        }

        public double AverageMph => TotalMiles == 0 ? 0 : TotalMiles / TotalHours;
    }
}
