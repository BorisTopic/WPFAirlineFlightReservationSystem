using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AirlineReservation
{
    public class clsFlight
    {
        /// <summary>
        /// Stores flight's number.
        /// </summary>
        private string sFlightNumber;
        /// <summary>
        /// Stores flight's aircraft type.
        /// </summary>
        private string sAircraftType;
        /// <summary>
        /// Stores flight's ID.
        /// </summary>
        private string sFlightID;
        /// <summary>
        /// Get and Set for sFlightNumber.
        /// </summary>
        public string SFlightNumber { get => sFlightNumber; set => sFlightNumber = value; }
        /// <summary>
        /// Get and Set for sAircraftType.
        /// </summary>
        public string SAircraftType { get => sAircraftType; set => sAircraftType = value; }
        /// <summary>
        /// Get and Set for sFlightID.
        /// </summary>
        public string SFlightID { get => sFlightID; set => sFlightID = value; }

        /// <summary>
        /// Formats the flight number and aircraft type to display correctly.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            try
            {
                return "#" + sFlightNumber + " - " + sAircraftType;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
