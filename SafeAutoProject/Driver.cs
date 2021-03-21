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

        public bool AddTrip(Trip trip)
        {
            if (trip.ShouldCount)
            {
                Trips.Add(trip);
                TotalMiles += trip.MilesDriven;
                TotalHours += trip.TripLengthInHours;

                return true;
            }

            return false;
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
