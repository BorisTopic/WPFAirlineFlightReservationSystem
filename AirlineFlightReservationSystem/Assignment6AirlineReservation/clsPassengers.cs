using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AirlineReservation
{
    public class clsPassengers
    {
        /// <summary>
        /// Stores passenger's ID.
        /// </summary>
        private string sPassenger_ID;
        /// <summary>
        /// Stores passenger's first name.
        /// </summary>
        private string sFirst_Name;
        /// <summary>
        /// Stores passenger's last name.
        /// </summary>
        private string sLast_Name;
        /// <summary>
        /// Stores passenger's seat.
        /// </summary>
        private string sSeat_Number;
        /// <summary>
        /// Get and Set for sPassenger_ID.
        /// </summary>
        public string SPassenger_ID { get => sPassenger_ID; set => sPassenger_ID = value; }
        /// <summary>
        /// Get and Set for sFirst_Name.
        /// </summary>
        public string SFirst_Name { get => sFirst_Name; set => sFirst_Name = value; }
        /// <summary>
        /// Get and Set for sLast_Name.
        /// </summary>
        public string SLast_Name { get => sLast_Name; set => sLast_Name = value; }
        /// <summary>
        /// Get and Set for sSeat_Number.
        /// </summary>
        public string SSeat_Number { get => sSeat_Number; set => sSeat_Number = value; }

        /// <summary>
        /// Format passenger's first name and last name to display correctly.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            try
            {
                return sFirst_Name + " " + sLast_Name;
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
