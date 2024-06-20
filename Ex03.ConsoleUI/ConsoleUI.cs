using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class ConsoleUI
    {
        static GarageManager m_GarageManager = new GarageManager();
        //private VehicleFactory m_VehicleFactory = new VehicleFactory();
        const int k_MinOptionRange = 1;
        const int k_MaxOptionRange = 8;

        public void LaunchUserInterface()
        {
            Console.WriteLine("Welcome to the garage!", Environment.NewLine);
            ManageGarageUI();
        }
        public static void ManageGarageUI()
        {
            eUserInterfaceOptions userChoice = eUserInterfaceOptions.Invalid;

            do
            {
                try
                {
                    userChoice = GetUserChoice();
                    HandleUserChoice(userChoice);

                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input, please try again");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Invalid input, please try again");
                }
                catch (ValueOutOfRangeException)
                {
                    Console.WriteLine("Invalid input, please try again");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            } while (userChoice != eUserInterfaceOptions.Exit);
        }


        private static void HandleUserChoice(eUserInterfaceOptions userChoice)
        {
            try
            {
                switch (userChoice)
                {
                    case eUserInterfaceOptions.InsertVehicle:
                        {
                            //use try catch?
                            ManageInsertionOfNewVehicle();
                            break;
                        }
                    case eUserInterfaceOptions.DisplayLicenseNumbers:
                        {
                            DisplayLicenseNumbers();
                            break;
                        }
                    case eUserInterfaceOptions.ChangeVehicleStatus:
                        {
                            ChangeVehicleStatus();
                            break;
                        }
                    case eUserInterfaceOptions.InflateWheelsToMax:
                        {
                            InflateWheelsToMax();
                            break;
                        }
                    case eUserInterfaceOptions.RefuelVehicle:
                        {
                            RefuelVehicle();
                            break;
                        }
                    case eUserInterfaceOptions.RechargeVehicle:
                        {
                            RechargeVehicle();
                            break;
                        }
                    case eUserInterfaceOptions.DisplayVehicleDetails:
                        {
                            DisplayVehicleDetails();
                            break;
                        }
                    case eUserInterfaceOptions.Exit:
                        {
                            Console.WriteLine("Goodbye!");
                            break;
                        }
                    default:
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input, please try again");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid input, please try again");
            }
            catch (ValueOutOfRangeException)
            {
                Console.WriteLine("Invalid input, please try again");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ManageInsertionOfNewVehicle()
        {
            try
            {
                string licenseNumber;
                bool isNewVehicleObjectCreated = TryInsertNewVehicleObjectToGarage(out licenseNumber);
                if (isNewVehicleObjectCreated)
                {
                    GetAndSetInputPropertiesForVehicle(licenseNumber);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        private static bool TryInsertNewVehicleObjectToGarage(out string i_LicenseNumber)
        {
            bool isNewVehicleObjectCreated = false;
            Console.WriteLine("Please enter the vehicle's license number:");
            i_LicenseNumber = Console.ReadLine();

            if (m_GarageManager.IsVehicleInGarage(i_LicenseNumber))
            {
                m_GarageManager.ChangeVehicleStatus(i_LicenseNumber, eVehicleStatus.InRepair);
                Console.WriteLine("Vehicle already exists in the garage, status changed to 'In Repair'");
            }
            else
            {
                try
                {
                    eVehicleType eWantedVehicleType = GetWantedVehicleType();
                    m_GarageManager.AddNewVehicleObjectToGarage(eWantedVehicleType, i_LicenseNumber);
                    isNewVehicleObjectCreated = true;
                }
                catch (Exception ex) //?
                {
                    Console.WriteLine(ex.Message);
                }

            }

            return isNewVehicleObjectCreated;
        }
        public static void GetAndSetInputPropertiesForVehicle(string i_LicenseNumber)
        {
            try
            {
                List<KeyValuePair<string, string>> neededPropertiesKeyValuePairs = m_GarageManager.GetListOfNeededPropertiesAndPossibleValues(i_LicenseNumber);

                Console.WriteLine("Please enter the vehicle's properties:");

                foreach (KeyValuePair<string, string> property in neededPropertiesKeyValuePairs)
                {
                    bool propertySetSuccessfully = false;
                    while (!propertySetSuccessfully)
                    {
                        Console.WriteLine($"Please enter {property.Key}");
                        Console.WriteLine($"Possible values are {property.Value}");
                        string propertyValue = Console.ReadLine();
                        try
                        {
                            m_GarageManager.SetProperty(i_LicenseNumber, property.Key, propertyValue);
                            propertySetSuccessfully = true; // If SetProperty succeeds, exit the loop
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}"); // Print the exception message and ask again
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void DisplayLicenseNumbers()
        {
            bool isUserInputValid = false;

            while (!isUserInputValid)
            {
                Console.WriteLine("Press 1 to display all license numbers");
                Console.WriteLine("Press 2 to display license numbers by status");
                string userInput = Console.ReadLine();

                //use enum and switch case
                if (userInput == "1" || userInput == "2")
                {
                    isUserInputValid = true;
                    if (userInput == "1")
                    {
                        Console.WriteLine(m_GarageManager.GetListOfAllLicensePlates());
                    }
                    else if (userInput == "2")
                    {
                        DisplayLicenseNumbersByStatus();
                    }

                }
                else
                {
                    Console.WriteLine("Invalid input, please try again");
                }
            }

        }
        private static void DisplayLicenseNumbersByStatus()
        {
            try
            {
                List<eVehicleStatus> statusOptions = Enum.GetValues(typeof(eVehicleStatus)).Cast<eVehicleStatus>().ToList();
                bool isValidChoice = false;

                while (!isValidChoice)
                {
                    Console.WriteLine("The status options are:");
                    int index = 1;
                    foreach (eVehicleStatus possibleStatus in statusOptions)
                    {
                        Console.WriteLine($"{index}. {possibleStatus}");
                        index++;
                    }

                    Console.WriteLine("Please enter the number corresponding to the status you want to filter by:");
                    string statusInput = Console.ReadLine();
                    if (int.TryParse(statusInput, out int statusChoice) && statusChoice >= 1 && statusChoice <= statusOptions.Count)
                    {
                        Console.WriteLine("The license numbers with the selected status are:");
                        eVehicleStatus selectedStatus = statusOptions[statusChoice - 1];
                        Console.WriteLine(m_GarageManager.GetListOfLicensePlatesByStatus(selectedStatus));
                        isValidChoice = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, please try again");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        //Console.WriteLine("Please enter the number corresponding to the status you want to filter by:");
        //string statusInput = Console.ReadLine();
        //if (int.TryParse(statusInput, out int statusChoice) && statusChoice >= 1 && statusChoice <= statusOptions.Count)
        //{
        //    Console.WriteLine("The license numbers with the selected status are:");
        //    eVehicleStatus selectedStatus = statusOptions[statusChoice - 1];
        //    Console.WriteLine(m_GarageManager.GetListOfLicensePlatesByStatus(selectedStatus));
        //}
        //else
        //{
        //    Console.WriteLine("Invalid input, please try again");
        //}

        //Console.WriteLine("The status options are:");
        //List<string> statusOptions = new List<string>();
        //foreach (eVehicleStatus possibleStatus in Enum.GetValues(typeof(eVehicleStatus)))
        //{
        //    statusOptions.Add(possibleStatus.ToString());
        //}
        //Console.WriteLine("Please enter the status you want to filter by:");
        //string status = Console.ReadLine();
        //Console.WriteLine(m_GarageManager.GetListOfLicensePlatesByStatus(status));

        private static void ChangeVehicleStatus()
        {
            throw new NotImplementedException();
        }
        private static void InflateWheelsToMax()
        {
            throw new NotImplementedException();
        }
        private static void RefuelVehicle()
        {
            throw new NotImplementedException();
        }
        private static void RechargeVehicle()
        {
            throw new NotImplementedException();
        }
        private static void DisplayVehicleDetails()
        {
            throw new NotImplementedException();
        }


        private static eUserInterfaceOptions GetUserChoice()
        {
            eUserInterfaceOptions eUserChoice = eUserInterfaceOptions.Invalid; // Default to Invalid
            bool isValidInput = false;
            int userInput;

            while (!isValidInput)
            {
                PrintMenu();
                string input = Console.ReadLine();
                if (int.TryParse(input, out userInput) && Enum.IsDefined(typeof(eUserInterfaceOptions), userInput))
                {
                    eUserChoice = (eUserInterfaceOptions)userInput;
                    isValidInput = true;
                }
                else
                {
                    PrintInvalidInputChoiceMessage();
                }
            }

            return eUserChoice;
        }

        public static void PrintMenu()
        {
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. Insert a new vehicle to the garage");
            Console.WriteLine("2. Display license numbers of vehicles in the garage");
            Console.WriteLine("3. Change vehicle status");
            Console.WriteLine("4. Inflate wheels to maximum");
            Console.WriteLine("5. Refuel a fuel-based vehicle");
            Console.WriteLine("6. Charge an electric vehicle");
            Console.WriteLine("7. Display full vehicle data");
            Console.WriteLine("8. Exit");
        }

        private static void PrintInvalidInputChoiceMessage()
        {
            Console.WriteLine("Invalid input, please try again");
            Console.WriteLine($"Please enter a number from the menu (an integer from {k_MinOptionRange} to {k_MaxOptionRange})");
        }

        private static void displayFullVehicleData(GarageManager i_GarageManager)
        {
            Console.WriteLine("Please enter the vehicle's license number:");
            string licenseNumber = Console.ReadLine();
            Console.WriteLine(i_GarageManager.GetVehicleData(licenseNumber));
        }


        //isAllInputValid = SetVehicleProperties(i_LicenseNumber, neededPropertiesKeyValuePairs, inputProperties);
        //if (!isAllInputValid)
        //{
        //    Console.WriteLine("Invalid input, please try again");
        //    inputProperties.Clear();
        //}


        //while (!isAllInputValid)
        //{
        //    Console.WriteLine("Invalid input, please try again");
        //    inputProperties.Clear();
        //    foreach (KeyValuePair<string, string> property in neededPropertiesKeyValuePairs)
        //    {
        //        Console.WriteLine($"Please enter {property.Key}");
        //        Console.WriteLine($"Possible values are {property.Value}");
        //        string propertyValue = Console.ReadLine();
        //        inputProperties.Add(propertyValue);
        //    }

        //    isAllInputValid = SetVehicleProperties(i_LicenseNumber, neededPropertiesKeyValuePairs, inputProperties);
        //}

        //for (int i = 0; i < neededProperties.Count; i++)
        //{
        //    Console.WriteLine(neededProperties[i]);
        //    string propertyValue = Console.ReadLine();
        //    inputProperties.Add(propertyValue);
        //}
        //bool isVehicleInserted = m_GarageManager.InsertVehicleToGarage(licenseNumber, inputProperties);

        //bool isAllInputValid = SetVehicleProperties(licenseNumber, neededPropertiesKeyValuePairs, inputProperties);

        //private static bool SetVehicleProperties(string i_LicenseNumber,
        //                                        List<KeyValuePair<string, string>> i_neededProperties,
        //                                        List<string> i_InputProperties)
        //{
        //    bool isAllInputValid = false;
        //    try
        //    {
        //        List<Exception> errors = new List<Exception>();
        //        int neededPropertIndex = 0;
        //        foreach (string propertyValue in i_InputProperties)
        //        {
        //            try
        //            {
        //                m_GarageManager.SetProperty(i_LicenseNumber, i_neededProperties[neededPropertIndex].Key, propertyValue);
        //                neededPropertIndex++;
        //            }
        //            catch (Exception ex)
        //            {
        //                errors.Add(ex);
        //                isAllInputValid = false;

        //                break;
        //            }
        //        }

        //        isAllInputValid = true;

        //    }
        //    catch (Exception ex) // suppose to print the property name where 
        //    {
        //        Console.WriteLine(ex.Message);
        //    }

        //    return isAllInputValid;
        //}

        private static void SetVehicleDataAccordingToVehicleType(eVehicleType eWantedVehicleType, string licenseNumber, List<string> inputProperties)
        {
            switch (eWantedVehicleType)
            {
                case eVehicleType.FuelCar:
                    m_GarageManager.AddPropertiesToElectricCar(licenseNumber, inputProperties);
                    break;
                case eVehicleType.ElectricCar:
                    m_GarageManager.AddPropertiesToElectricCar(licenseNumber, inputProperties);
                    break;
                case eVehicleType.FuelMotorcycle:
                    m_GarageManager.SetMotorcycleValues(licenseNumber, inputProperties);
                    break;
                case eVehicleType.ElectricMotorcycle:
                    m_GarageManager.SetMotorcycleValues(licenseNumber, inputProperties);
                    break;
                case eVehicleType.Truck:
                    m_GarageManager.SetTruckValues(licenseNumber, inputProperties);
                    break;
                default:
                    break;
            }

        }

        private static eVehicleType GetWantedVehicleType()
        {
            bool isInputValid = false;
            eVehicleType wantedVehicleType = default;

            while (!isInputValid)
            {
                Console.WriteLine("Please enter the type of vehicle you want to insert.");
                Console.WriteLine("Possible options are:");
                List<string> vehicleTypes = m_GarageManager.GetListOfSupportedVehicleTypesAsStrings();
                for (int i = 0; i < vehicleTypes.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {vehicleTypes[i]}");
                }

                Console.WriteLine("Please enter the number corresponding to the vehicle type:");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int userInput) && userInput >= 1 && userInput <= vehicleTypes.Count)
                {
                    isInputValid = true;
                    wantedVehicleType = (eVehicleType)userInput; // This assumes enum values start from 1
                }
                else
                {
                    Console.WriteLine("Invalid input, please try again.");
                }
            }

            return wantedVehicleType;
        }


        //    private static eVehicleType GetWantedVehicleType()
        //    {
        //        bool isInputValid = false;
        //        eVehicleType wantedVehicleType;
        //        string userInput = string.Empty;
        //        List<string> vehicleTypes = m_GarageManager.GetListOfSupportedVehicleTypes();

        //        while (!isInputValid)
        //        {
        //            Console.WriteLine("Please enter the type of vehicle you want to insert.");
        //            Console.WriteLine("Possible options are:");
        //            List<string> vehicleTypes = m_GarageManager.GetListOfSupportedVehicleTypes();
        //            //foreach (eVehicleType possibleVehicleType in Enum.GetValues(typeof(eVehicleType)))
        //            //{
        //            //    string vehicleType = possibleVehicleType.ToString();
        //            //    vehicleType = Regex.Replace(vehicleType, "(\\B[A-Z])", " $1");
        //            //    Console.WriteLine(vehicleType);
        //            //}
        //        //    Console.WriteLine(Environment.NewLine, "Please enter the type exactly as written above (case-sensitive):");
        //        //    //PrintVehicleTypes();
        //        //    userInput = Console.ReadLine();
        //        //    if (Enum.TryParse(userInput, out wantedVehicleType))
        //        //    {
        //        //        isInputValid = true;
        //        //        //break;
        //        //    }
        //        //    else
        //        //    {
        //        //        Console.WriteLine("Invalid input, please try again");
        //        //    }
        //        //}

        //        //Enum.Parse(userInput, wantedVehicleType);

        //        return wantedVehicleType;
        //        //Console.WriteLine("Please enter the type of vehicle you want to insert.");
        //        //Console.WriteLine("Possible options are:");
        //        //List<string> vehicleTypes = new List<string>();
        //        //foreach (eVehicleType vehicleType in Enum.GetValues(typeof(eVehicleType)))
        //        //{
        //        //    Console.WriteLine(vehicleType.ToString());
        //        //}
        //        //Console.WriteLine("Please enter the type exactly as written (case-sensitive)");

        //    }
        //}
    }
}

//while (isRunning)
//{

//    string userInput = Console.ReadLine();
//    switch (userInput)
//    {
//        case "1":
//            insertNewVehicle(garageManager);
//            break;
//        case "2":
//            displayLicenseNumbers(garageManager);
//            break;
//        case "3":
//            changeVehicleStatus(garageManager);
//            break;
//        case "4":
//            inflateWheelsToMax(garageManager);
//            break;
//        case "5":
//            refuelVehicle(garageManager);
//            break;
//        case "6":
//            chargeVehicle(garageManager);
//            break;
//        case "7":
//            displayFullVehicleData(garageManager);
//            break;
//        case "8":
//            isRunning = false;
//            break;
//        default:
//            Console.WriteLine("Invalid input, please try again");
//            break;
//    }
//}