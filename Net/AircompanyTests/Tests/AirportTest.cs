using Aircompany;
using Aircompany.Models;
using Aircompany.Planes;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AircompanyTests.Tests
{
    [TestFixture]
    public class AirportTest
    {
        private List<Plane> planes = new List<Plane>(){
           new PassengerPlane("Boeing-737", 900, 12000, 60500, 164),
           new PassengerPlane("Boeing-737-800", 940, 12300, 63870, 192),
           new PassengerPlane("Boeing-747", 980, 16100, 70500, 242),
           new PassengerPlane("Airbus A320", 930, 11800, 65500, 188),
           new PassengerPlane("Airbus A330", 990, 14800, 80500, 222),
           new PassengerPlane("Embraer 190", 870, 8100, 30800, 64),
           new PassengerPlane("Sukhoi Superjet 100", 870, 11500, 50500, 140),
           new PassengerPlane("Bombardier CS300", 920, 11000, 60700, 196),
           new MilitaryPlane("B-1B Lancer", 1050, 21000, 80000, MilitaryPlaneType.BOMBER),
           new MilitaryPlane("B-2 Spirit", 1030, 22000, 70000, MilitaryPlaneType.BOMBER),
           new MilitaryPlane("B-52 Stratofortress", 1000, 20000, 80000, MilitaryPlaneType.BOMBER),
           new MilitaryPlane("F-15", 1500, 12000, 10000, MilitaryPlaneType.FIGHTER),
           new MilitaryPlane("F-22", 1550, 13000, 11000, MilitaryPlaneType.FIGHTER),
           new MilitaryPlane("C-130 Hercules", 650, 5000, 110000, MilitaryPlaneType.TRANSPORT)
   };

        private bool ThisTypeMilitaryPlaneIsHere(List<MilitaryPlane> militaryPlanes, MilitaryPlaneType type)
        {
            foreach (MilitaryPlane militaryPlane in militaryPlanes)
            {
                if ((militaryPlane.PlaneTypeIs() == MilitaryPlaneType.TRANSPORT))
                {
                    return true;
                }
            }
            return false;
        }
        [Test]
        public void GetAtLastOneTransportMilitaryPlane()
        {
            Airport airport = new Airport(planes);
            List<MilitaryPlane> transportMilitaryPlanes = airport.GetTransportTypeMilitaryPlanes().ToList();           
            Assert.IsTrue(ThisTypeMilitaryPlaneIsHere(transportMilitaryPlanes, MilitaryPlaneType.TRANSPORT));
        }

        [Test]
        public void ExceedsLimitMaxPassengersCapacity()
        {
            Airport airport = new Airport(planes);
            PassengerPlane expectedPlaneWithMaxPassengersCapacity = airport.GetPassengerPlaneWithMaxPassengersCapacity();
            Assert.IsTrue(airport.GetPassengerPlaneWithMaxPassengersCapacity().GetPassengersCapacity() <= new PassengerPlane("Boeing-747", 980, 16100, 70500, 242).GetPassengersCapacity());
        }

        private bool CorrectSortingByMaxLoadCapacity(List<Plane> planesSortedByMaxLoadCapacity)
        {
            for (int i = 0; i < planesSortedByMaxLoadCapacity.Count - 1; i++)
            {
                if (planesSortedByMaxLoadCapacity[i].GetMaxLoadCapacity() > planesSortedByMaxLoadCapacity[i + 1].GetMaxLoadCapacity())
                {
                    return false;
                }
            }
            return true;
        }

        [Test]
        public void SortingByMaxLoadCapacity()
        {
            Airport airport = new Airport(planes);
            Assert.That(CorrectSortingByMaxLoadCapacity(airport.SortByMaxLoadCapacity().GetPlanes().ToList()));
        }
    }
}
