using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;

namespace AirlineReservation
{
    public class clsFlightManager
    {
        /// <summary>
        /// Class to access database.
        /// </summary>
        clsDataAccess db;

        /// <summary>
        /// Returns the flight's information in order to fill up the combobox.
        /// </summary>
        /// <returns></returns>
        public List<clsFlight> GetFlights()
        {
            try
            {
                db = new clsDataAccess();

                List<clsFlight> lstFlight = new List<clsFlight>();

                int iNumRetValues = 0;
                DataSet ds;
                string sSQL = "SELECT Flight_ID, Flight_Number, Aircraft_Type FROM FLIGHT";
                ds = db.ExecuteSQLStatement(sSQL, ref iNumRetValues);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsFlight flight = new clsFlight();
                    flight.SAircraftType = dr["Aircraft_Type"].ToString();
                    flight.SFlightID = dr["Flight_ID"].ToString();
                    flight.SFlightNumber = dr["Flight_Number"].ToString();
                    lstFlight.Add(flight);
                }

                return lstFlight;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Returns passenger's information for Flight_ID 1.
        /// </summary>
        /// <returns></returns>
        public List<clsPassengers> GetPassengersForFlightID1()
        {
            try
            {
                db = new clsDataAccess();

                List<clsPassengers> lstPassengers = new List<clsPassengers>();

                int iNumRetValues = 0;
                DataSet ds;
                string sSQL = "SELECT PASSENGER.Passenger_ID, First_Name, Last_Name, Seat_Number " +
                                "FROM FLIGHT_PASSENGER_LINK, FLIGHT, PASSENGER " +
                                "WHERE FLIGHT.FLIGHT_ID = FLIGHT_PASSENGER_LINK.FLIGHT_ID AND " +
                                "FLIGHT_PASSENGER_LINK.PASSENGER_ID = PASSENGER.PASSENGER_ID AND " +
                                "FLIGHT.FLIGHT_ID = 1";
                ds = db.ExecuteSQLStatement(sSQL, ref iNumRetValues);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsPassengers passenger = new clsPassengers();
                    passenger.SPassenger_ID = dr["Passenger_ID"].ToString();
                    passenger.SFirst_Name = dr["First_Name"].ToString();
                    passenger.SLast_Name = dr["Last_Name"].ToString();
                    passenger.SSeat_Number = dr["Seat_Number"].ToString();
                    lstPassengers.Add(passenger);
                }

                return lstPassengers;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Returns passenger's information for Flight_ID 2.
        /// </summary>
        /// <returns></returns>
        public List<clsPassengers> GetPassengersForFlightID2()
        {
            try
            {
                db = new clsDataAccess();

                List<clsPassengers> lstPassengers = new List<clsPassengers>();

                int iNumRetValues = 0;
                DataSet ds;
                string sSQL = "SELECT PASSENGER.Passenger_ID, First_Name, Last_Name, Seat_Number " +
                                "FROM FLIGHT_PASSENGER_LINK, FLIGHT, PASSENGER " +
                                "WHERE FLIGHT.FLIGHT_ID = FLIGHT_PASSENGER_LINK.FLIGHT_ID AND " +
                                "FLIGHT_PASSENGER_LINK.PASSENGER_ID = PASSENGER.PASSENGER_ID AND " +
                                "FLIGHT.FLIGHT_ID = 2";
                ds = db.ExecuteSQLStatement(sSQL, ref iNumRetValues);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsPassengers passenger = new clsPassengers();
                    passenger.SPassenger_ID = dr["Passenger_ID"].ToString();
                    passenger.SFirst_Name = dr["First_Name"].ToString();
                    passenger.SLast_Name = dr["Last_Name"].ToString();
                    passenger.SSeat_Number = dr["Seat_Number"].ToString();
                    lstPassengers.Add(passenger);
                }

                return lstPassengers;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Updates the passenger's seat number when they want to change it.
        /// </summary>
        /// <param name="seatNumber"></param>
        /// <param name="flightID"></param>
        /// <param name="passengerID"></param>
        public void updateSeat(string seatNumber, string flightID, string passengerID)
        {
            try
            {
                db = new clsDataAccess();

                string sSQL = "UPDATE FLIGHT_PASSENGER_LINK " +
                              "SET Seat_Number = " + seatNumber + " " +
                              "WHERE FLIGHT_ID = " + flightID + " AND  PASSENGER_ID = " + passengerID;
                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Allows user to add a new passenger to the plane.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        public void addNewPassenger(string firstName, string lastName)
        {
            try
            {
                db = new clsDataAccess();

                string sSQL = "INSERT INTO PASSENGER(First_Name, Last_Name) VALUES('" + firstName + "','" + lastName + "')";

                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Allows user to choose a seat for the new passenger.
        /// </summary>
        public void newPassengersSeat(string flightID, string passengerID, string seatNumber)
        {
            try
            {
                db = new clsDataAccess();

                string sSQL = "INSERT INTO Flight_Passenger_Link(Flight_ID, Passenger_ID, Seat_Number) " +
                                "VALUES( " + flightID + " , " + passengerID + " , " + seatNumber + ")";

                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Gets the new passenger's ID.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        public string getPassengerID(string firstName, string lastName)
        {
            try
            {
                db = new clsDataAccess();

                string sSQL = "SELECT Passenger_ID from Passenger where First_Name = '" + firstName +"' AND Last_Name = '"+ lastName +"'";

                string passengerID = db.ExecuteScalarSQL(sSQL);
                return passengerID;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Delete the passenger's link in the database.
        /// </summary>
        /// <param name="flightID"></param>
        /// <param name="passengerID"></param>
        public void deletePassengerLink(string flightID, string passengerID)
        {
            try
            {
                db = new clsDataAccess();

                string sSQL = "Delete FROM FLIGHT_PASSENGER_LINK " +
                              "WHERE FLIGHT_ID = " + flightID + " AND " +
                              "PASSENGER_ID = " + passengerID;

                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Delete the passenger in the database.
        /// </summary>
        /// <param name="passengerID"></param>
        public void deletePassenger(string passengerID)
        {
            try
            {
                db = new clsDataAccess();

                string sSQL = "Delete FROM PASSENGER " +
                              "WHERE PASSENGER_ID = " + passengerID;

                db.ExecuteNonQuery(sSQL);
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
