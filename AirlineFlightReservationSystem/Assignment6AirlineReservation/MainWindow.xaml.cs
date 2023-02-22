using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AirlineReservation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Class to store the flight's information.
        /// </summary>
        clsFlight clsFlight;
        /// <summary>
        /// Class to manage the flight's information.
        /// </summary>
        clsFlightManager clsFlightManager;
        /// <summary>
        /// Window to enter passenger's information.
        /// </summary>
        wndAddPassenger wndAddPass;
        /// <summary>
        /// Indicates if the user wants to select a new seat.
        /// </summary>
        bool chooseNewSeat = false;
        /// <summary>
        /// Indicates if the user wants to add a new passenger.
        /// </summary>
        bool addNewPassenger = false;
        /// <summary>
        /// Indicates if the user wants to delete a passenger.
        /// </summary>
        bool deletePassenger = false;
        /// <summary>
        /// Indicates if the seat being clicked is for a new passenger.
        /// </summary>
        bool newPassengerSeat = false;
        /// <summary>
        /// Indicates if the user wants to click on a seat to see who the passenger is.
        /// </summary>
        bool clickTakenSeat = true;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

                clsFlight = new clsFlight();
                clsFlightManager = new clsFlightManager();

                cbChooseFlight.ItemsSource = clsFlightManager.GetFlights();

                Flight_Title.Content = "";
                Flight_Title2.Content = "";

                #region OldCode
                /*
                DataSet ds = new DataSet();
                //Should probably not have SQL statements behind the UI
                string sSQL = "SELECT Flight_ID, Flight_Number, Aircraft_Type FROM FLIGHT";
                int iRet = 0;
                clsData = new clsDataAccess();
                */
                //This should probably be in a new class.  Would be nice if this new class
                //returned a list of Flight objects that was then bound to the combo box
                //Also should show the flight number and aircraft type together
                /*
                ds = clsData.ExecuteSQLStatement(sSQL, ref iRet);

                for(int i = 0; i < iRet; i++)
                {
                    cbChooseFlight.Items.Add(ds.Tables[0].Rows[i][0]);
                }
                */
                #endregion
            }

            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
                
        }
        /// <summary>
        /// Allows user to switch between flights.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChooseFlight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //Appropriate flight should be displayed based on what is selected.
                //Flight Name Label should be changed to appropriate flight name.
                if (((clsFlight)cbChooseFlight.SelectedItem).SAircraftType == "Airbus A380")
                {
                    Canvas767.Visibility = Visibility.Hidden;
                    CanvasA380.Visibility = Visibility.Visible;
                    Flight_Title2.Content = "A380";
                }
                else if (((clsFlight)cbChooseFlight.SelectedItem).SAircraftType == "Boeing 767")
                {
                    CanvasA380.Visibility = Visibility.Hidden;
                    Canvas767.Visibility = Visibility.Visible;
                    Flight_Title.Content = "767";
                }

                //"Choose Passenger" should be enabled and filled with the correct data.
                cbChoosePassenger.IsEnabled = true;
                cmdAddPassenger.IsEnabled = true;
                if (((clsFlight)cbChooseFlight.SelectedItem).SFlightID == "1")
                {
                    cbChoosePassenger.ItemsSource = clsFlightManager.GetPassengersForFlightID1();
                }
                else if (((clsFlight)cbChooseFlight.SelectedItem).SFlightID == "2")
                {
                    cbChoosePassenger.ItemsSource = clsFlightManager.GetPassengersForFlightID2();
                }

                //When a user switches between flights, we need to unselect a passenger
                //if one was selected before they switched to a different flight.
                lblPassengersSeatNumber.Content = "";

                //We need to make sure each seat is color coded correctly.
                List<string> listOfNumbers = new List<string>();

                clsPassengers passengerInfo;

                for (int i = 0; i < cbChoosePassenger.Items.Count; i++)
                {
                    passengerInfo = (clsPassengers)cbChoosePassenger.Items[i];

                    listOfNumbers.Add(passengerInfo.SSeat_Number);
                }

                if (((clsFlight)cbChooseFlight.SelectedItem).SAircraftType == "Boeing 767")
                {
                    foreach (Label MyLabel in c767_Seats.Children)
                    {
                        if (listOfNumbers.Contains(MyLabel.Content))
                        {
                            MyLabel.Background = lblSeatTakenSquare.Background;
                        }
                    }
                }
                else if (((clsFlight)cbChooseFlight.SelectedItem).SAircraftType == "Airbus A380")
                {
                    foreach (Label MyLabel in cA380_Seats.Children)
                    {
                        if (listOfNumbers.Contains(MyLabel.Content))
                        {
                            MyLabel.Background = lblSeatTakenSquare.Background;
                        }
                    }
                }

                if (cbChoosePassenger.SelectedIndex == -1)
                {
                    cmdChangeSeat.IsEnabled = false;
                    cmdDeletePassenger.IsEnabled = false;
                }

                

                #region OldCode
                /*
                string selection = cbChooseFlight.SelectedItem.ToString();
                cbChoosePassenger.IsEnabled = true;
                gPassengerCommands.IsEnabled = true;
                DataSet ds = new DataSet();                
                int iRet = 0;

                if (selection == "1")
                {
                    CanvasA380.Visibility = Visibility.Hidden;
                    Canvas767.Visibility = Visibility.Visible;
                }
                else
                {
                    Canvas767.Visibility = Visibility.Hidden;
                    CanvasA380.Visibility = Visibility.Visible;
                }

                //I think this should be in a new class to hold SQL statments
                string sSQL = "SELECT Passenger.Passenger_ID, First_Name, Last_Name, FPL.Seat_Number " +
                              "FROM Passenger, Flight_Passenger_Link FPL " +
                              "WHERE Passenger.Passenger_ID = FPL.Passenger_ID AND " +
                              "Flight_ID = " + cbChooseFlight.SelectedItem.ToString();//If the cbChooseFlight was bound to a list of Flights, the selected object would have the flight ID
                //Probably put in a new class
                ds = clsData.ExecuteSQLStatement(sSQL, ref iRet);

                cbChoosePassenger.Items.Clear();

                //Would be nice if code from another class executed the SQL above, added each passenger into a Passenger object, then into a list of Passengers to be returned and bound to the combo box
                for (int i = 0; i < iRet; i++)
                {
                    cbChoosePassenger.Items.Add(ds.Tables[0].Rows[i][1] + " " + ds.Tables[0].Rows[i][2]);
                }
                */
                #endregion
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Locks the UI for the user to add a new passenger.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAddPassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cbChooseFlight.IsEnabled = false;
                cbChoosePassenger.IsEnabled = false;
                cmdChangeSeat.IsEnabled = false;
                cmdAddPassenger.IsEnabled = false;
                cmdDeletePassenger.IsEnabled = false;
                addNewPassenger = true;
                wndAddPass = new wndAddPassenger();
                wndAddPass.ShowDialog();
                if (wndAddPass.DialogResult == true)
                {
                    newPassengerSeat = true;
                }
                else
                {
                    newPassengerSeat = false;
                    cbChooseFlight.IsEnabled = true;
                    cbChoosePassenger.IsEnabled = true;
                    cmdChangeSeat.IsEnabled = true;
                    cmdAddPassenger.IsEnabled = true;
                    cmdDeletePassenger.IsEnabled = true;
                    addNewPassenger = false;
                    if (cbChoosePassenger.SelectedIndex == -1)
                    {
                        cmdChangeSeat.IsEnabled = false;
                        cmdDeletePassenger.IsEnabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Handles the error.
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
        /// <summary>
        /// Allows user to switch between passengers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChoosePassenger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cmdChangeSeat.IsEnabled = true;
                cmdDeletePassenger.IsEnabled = true;
                if (cbChoosePassenger.SelectedIndex != -1)
                {
                    clsPassengers clsPassengers;

                    clsPassengers = (clsPassengers)cbChoosePassenger.SelectedItem;

                    lblPassengersSeatNumber.Content = clsPassengers.SSeat_Number;

                    if (((clsFlight)cbChooseFlight.SelectedItem).SAircraftType == "Boeing 767")
                    {
                        foreach (Label MyLabel in c767_Seats.Children)
                        {   
                            //MyLabel.Background = lblSeatEmptySquare.Background;
                            if (MyLabel.Background == lblSeatSelectedSquare.Background)
                            {
                                MyLabel.Background = lblSeatTakenSquare.Background;
                            }

                            if (MyLabel.Content.ToString() == clsPassengers.SSeat_Number)
                            {
                                MyLabel.Background = lblSeatSelectedSquare.Background;
                            }
                            
                        }
                    }
                    else if (((clsFlight)cbChooseFlight.SelectedItem).SAircraftType == "Airbus A380")
                    {
                        foreach (Label MyLabel in cA380_Seats.Children)
                        {
                            //MyLabel.Background = lblSeatEmptySquare.Background;
                            if (MyLabel.Background == lblSeatSelectedSquare.Background)
                            {
                                MyLabel.Background = lblSeatTakenSquare.Background;
                            }

                            if (MyLabel.Content.ToString() == clsPassengers.SSeat_Number)
                            {
                                MyLabel.Background = lblSeatSelectedSquare.Background;
                            }
                        }
                    }
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Allows the user to click on a seat if they would like to change a seat of an existing passenger or add a new passenger.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Seat_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                


                if (chooseNewSeat == true)
                {
                    clickTakenSeat = false;

                    Label MyLabel = (Label)sender;
                    string sSeatNumber;
                    clsPassengers Passenger;

                    if (MyLabel.Background == lblSeatEmptySquare.Background)
                    {
                        foreach (Label MyLabelLoop in cA380_Seats.Children)
                        {
                            if (MyLabelLoop.Background == lblSeatSelectedSquare.Background)
                            {
                                MyLabelLoop.Background = lblSeatEmptySquare.Background;
                            }
                        }

                        foreach (Label MyLabelLoop in c767_Seats.Children)
                        {
                            if (MyLabelLoop.Background == lblSeatSelectedSquare.Background)
                            {
                                MyLabelLoop.Background = lblSeatEmptySquare.Background;
                            }
                        }

                    }

                    if (MyLabel.Background == lblSeatEmptySquare.Background)
                    {
                        MyLabel.Background = lblSeatSelectedSquare.Background;
                        sSeatNumber = MyLabel.Content.ToString();

                        for (int i = 0; i < cbChoosePassenger.Items.Count; i++)
                        {
                            Passenger = (clsPassengers)cbChoosePassenger.Items[i];

                            if (sSeatNumber == Passenger.SSeat_Number)
                            {
                                cbChoosePassenger.SelectedIndex = i;
                            }
                        }

                        //Update the seat in the database
                        string newSeatNumber = MyLabel.Content.ToString();
                        string flightID = ((clsFlight)cbChooseFlight.SelectedItem).SFlightID;
                        string passengerID = ((clsPassengers)cbChoosePassenger.SelectedItem).SPassenger_ID;
                        ((clsPassengers)cbChoosePassenger.SelectedItem).SSeat_Number = newSeatNumber;
                        ((clsPassengers)cbChoosePassenger.SelectedItem).SPassenger_ID = passengerID;

                        clsFlightManager.updateSeat(newSeatNumber, flightID, passengerID);


                        //Unlock the UI
                        cbChooseFlight.IsEnabled = true;
                        cbChoosePassenger.IsEnabled = true;
                        cmdChangeSeat.IsEnabled = true;
                        cmdAddPassenger.IsEnabled = true;
                        cmdDeletePassenger.IsEnabled = true;
                        chooseNewSeat = false;
                        lblPassengersSeatNumber.Content = newSeatNumber;
                        lblChangeSeat.Content = "";
                        clickTakenSeat = true;
                    }
                }

                if (addNewPassenger == true)
                {
                    clickTakenSeat = false;

                    Label MyLabel = (Label)sender;
                    string sSeatNumber;
                    clsPassengers Passenger;

                    if (newPassengerSeat == true)
                    {
                        foreach (Label MyLabelLoop in cA380_Seats.Children)
                        {
                            if (MyLabelLoop.Background == lblSeatSelectedSquare.Background)
                            {
                                MyLabelLoop.Background = lblSeatTakenSquare.Background;
                            }
                        }

                        foreach (Label MyLabelLoop in c767_Seats.Children)
                        {
                            if (MyLabelLoop.Background == lblSeatSelectedSquare.Background)
                            {
                                MyLabelLoop.Background = lblSeatTakenSquare.Background;
                            }
                        }

                        newPassengerSeat = false;
                    }


                    if (MyLabel.Background == lblSeatEmptySquare.Background)
                    {
                        foreach (Label MyLabelLoop in cA380_Seats.Children)
                        {
                            if (MyLabelLoop.Background == lblSeatSelectedSquare.Background)
                            {
                                MyLabelLoop.Background = lblSeatEmptySquare.Background;
                            }
                        }

                        foreach (Label MyLabelLoop in c767_Seats.Children)
                        {
                            if (MyLabelLoop.Background == lblSeatSelectedSquare.Background)
                            {
                                MyLabelLoop.Background = lblSeatEmptySquare.Background;
                            }
                        }

                    }

                    if (MyLabel.Background == lblSeatEmptySquare.Background)
                    {
                        MyLabel.Background = lblSeatSelectedSquare.Background;
                        sSeatNumber = MyLabel.Content.ToString();

                        for (int i = 0; i < cbChoosePassenger.Items.Count; i++)
                        {
                            Passenger = (clsPassengers)cbChoosePassenger.Items[i];

                            if (sSeatNumber == Passenger.SSeat_Number)
                            {
                                cbChoosePassenger.SelectedIndex = i;
                            }
                        }

                        //Let the user choose a seat for the new passenger
                        string firstName = wndAddPass.txtFirstName.Text;
                        string lastName = wndAddPass.txtLastName.Text;

                        string flightID = ((clsFlight)cbChooseFlight.SelectedItem).SFlightID;
                        string passengerID = clsFlightManager.getPassengerID(firstName, lastName);
                        string seatNumber = MyLabel.Content.ToString();

                        clsFlightManager.newPassengersSeat(flightID, passengerID, seatNumber);

                        //Unlock UI
                        cbChooseFlight.IsEnabled = true;
                        cbChoosePassenger.IsEnabled = true;
                        cmdChangeSeat.IsEnabled = true;
                        cmdAddPassenger.IsEnabled = true;
                        cmdDeletePassenger.IsEnabled = true;
                        addNewPassenger = false;
                        lblPassengersSeatNumber.Content = MyLabel.Content.ToString();
                        clickTakenSeat = true;

                        //Update Dropdown Box
                        if (((clsFlight)cbChooseFlight.SelectedItem).SFlightID == "1")
                        {
                            cbChoosePassenger.ItemsSource = clsFlightManager.GetPassengersForFlightID1();
                        }
                        else if (((clsFlight)cbChooseFlight.SelectedItem).SFlightID == "2")
                        {
                            cbChoosePassenger.ItemsSource = clsFlightManager.GetPassengersForFlightID2();
                        }

                        //Fill in cbChoosePassenger with the correct name.
                        cbChoosePassenger.SelectedIndex = cbChoosePassenger.Items.Count - 1;
                    }
                }


                if (clickTakenSeat == true)
                {
                    Label MyLabel = (Label)sender;
                    string sSeatNumber;
                    clsPassengers Passenger;

                    if (MyLabel.Background == lblSeatTakenSquare.Background)
                    {
                        MyLabel.Background = lblSeatSelectedSquare.Background;

                        sSeatNumber = MyLabel.Content.ToString();

                        for (int i = 0; i < cbChoosePassenger.Items.Count; i++)
                        {
                            Passenger = (clsPassengers)cbChoosePassenger.Items[i];

                            if (sSeatNumber == Passenger.SSeat_Number)
                            {
                                cbChoosePassenger.SelectedIndex = i;
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            


        }
        /// <summary>
        /// Locks UI for the process of a passenger changing their seat.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChangeSeat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cbChooseFlight.IsEnabled = false;
                cbChoosePassenger.IsEnabled = false;
                cmdChangeSeat.IsEnabled = false;
                cmdAddPassenger.IsEnabled = false;
                cmdDeletePassenger.IsEnabled = false;
                lblChangeSeat.Content = "Please click an empty seat.";
                chooseNewSeat = true;
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Allows the user to delete a selected passenger.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDeletePassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                deletePassenger = true;
                if (deletePassenger == true)
                {
                    //Deleting a Passenger
                    cmdChangeSeat.IsEnabled = false;
                    cmdDeletePassenger.IsEnabled = false;

                    if (MessageBox.Show("Are you sure you want to delete this passenger?",
                        "Delete Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        string flightID = ((clsFlight)cbChooseFlight.SelectedItem).SFlightID;
                        string passengerID = ((clsPassengers)cbChoosePassenger.SelectedItem).SPassenger_ID;

                        clsFlightManager.deletePassengerLink(flightID, passengerID);
                        clsFlightManager.deletePassenger(passengerID);

                        cbChoosePassenger.SelectedIndex = -1;
                        lblPassengersSeatNumber.Content = "";
                        deletePassenger = false;
                        cmdChangeSeat.IsEnabled = false;
                        cmdDeletePassenger.IsEnabled = false;

                        //Reload Dropdown Box
                        if (((clsFlight)cbChooseFlight.SelectedItem).SFlightID == "1")
                        {
                            cbChoosePassenger.ItemsSource = clsFlightManager.GetPassengersForFlightID1();
                        }
                        else if (((clsFlight)cbChooseFlight.SelectedItem).SFlightID == "2")
                        {
                            cbChoosePassenger.ItemsSource = clsFlightManager.GetPassengersForFlightID2();
                        }

                        //Reload UI
                        foreach (Label MyLabelLoop in cA380_Seats.Children)
                        {
                            if (MyLabelLoop.Background == lblSeatSelectedSquare.Background)
                            {
                                MyLabelLoop.Background = lblSeatEmptySquare.Background;
                            }
                        }

                        foreach (Label MyLabelLoop in c767_Seats.Children)
                        {
                            if (MyLabelLoop.Background == lblSeatSelectedSquare.Background)
                            {
                                MyLabelLoop.Background = lblSeatEmptySquare.Background;
                            }
                        }


                    }
                    else
                    {
                        cmdChangeSeat.IsEnabled = true;
                        cmdDeletePassenger.IsEnabled = true;
                        deletePassenger = false;
                        //this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }



    }
}
