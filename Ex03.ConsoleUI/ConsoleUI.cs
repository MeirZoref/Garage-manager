using System;
using System.Collections.Generic;
using System.Linq;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class ConsoleUI
    {
        private GarageManager m_GarageManager = new GarageManager();
        const int k_MinOptionRange = 1;
        const int k_MaxOptionRange = 8;

        public void LaunchUserInterface()
        {
            Console.WriteLine("Welcome to the garage!", Environment.NewLine);
            Console.WriteLine($"The garage is now open.{Environment.NewLine}");
            Console.WriteLine(VehicleFactory.GetListOfCurrentSupportedVehicles());
            manageGarageUI();
        }
        
        private void manageGarageUI()
        {
            eUserInterfaceOptions? userChoiceFromUIOptions = null;

            do
            {
                try
                {
                    userChoiceFromUIOptions = getUserChoice();
                    handleUserChoice(userChoiceFromUIOptions);
                }
                catch (Exception i_Exception)
                {
                    printExceptionAndMessageAboutReturningToMainMenu(i_Exception);
                }

            } while (userChoiceFromUIOptions != eUserInterfaceOptions.Exit);
        }

        private eUserInterfaceOptions? getUserChoice()
        {
            eUserInterfaceOptions? userChoiceFromUIOptions = null;
            bool isValidInput = false;
            int userInput;

            while (!isValidInput)
            {
                Console.Write(Environment.NewLine + Environment.NewLine);
                printMenu();
                string input = Console.ReadLine();
                if (int.TryParse(input, out userInput) && Enum.IsDefined(typeof(eUserInterfaceOptions), userInput))
                {
                    userChoiceFromUIOptions = (eUserInterfaceOptions)userInput;
                    isValidInput = true;
                }
                else
                {
                    printInvalidInputChoiceMessage();
                }
            }

            return userChoiceFromUIOptions;
        }

        private void handleUserChoice(eUserInterfaceOptions? i_UserChoice)
        {
            try
            {
                switch (i_UserChoice)
                {
                    case eUserInterfaceOptions.InsertVehicle:
                        {
                            manageInsertionOfNewVehicle();
                            break;
                        }
                    case eUserInterfaceOptions.DisplayLicenseNumbers:
                        {
                            displayLicenseNumbers();
                            break;
                        }
                    case eUserInterfaceOptions.ChangeVehicleStatus:
                        {
                            changeVehicleStatus();
                            break;
                        }
                    case eUserInterfaceOptions.InflateWheelsToMax:
                        {
                            inflateWheelsToMax();
                            break;
                        }
                    case eUserInterfaceOptions.RefuelVehicle:
                        {
                            const bool v_isElectricVehicle = false;
                            manageAddingEnergyToVehicle(v_isElectricVehicle);
                            break;
                        }
                    case eUserInterfaceOptions.RechargeVehicle:
                        {
                            const bool v_isElectricVehicle = true;
                            manageAddingEnergyToVehicle(v_isElectricVehicle);
                            break;
                        }
                    case eUserInterfaceOptions.DisplayVehicleDetails:
                        {
                            displayVehicleDetails();
                            break;
                        }
                    case eUserInterfaceOptions.Exit:
                        {
                            Console.WriteLine("Goodbye!");
                            Console.WriteLine("Press enter key to exit.");
                            Console.ReadLine();
                            break;
                        }
                    case null:
                    {
                        printInvalidInputMessage();
                        break;
                    }
                    default:
                        break;
                }
            }
            catch (Exception i_Exception)
            {
                printExceptionAndMessageAboutReturningToMainMenu(i_Exception);
            }
        }

        private void printMenu()
        {
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. Insert a new vehicle to the garage.");
            Console.WriteLine("2. Display license numbers of vehicles in the garage.");
            Console.WriteLine("3. Change vehicle status.");
            Console.WriteLine("4. Inflate wheels to maximum.");
            Console.WriteLine("5. Refuel a fuel-based vehicle.");
            Console.WriteLine("6. Charge an electric vehicle.");
            Console.WriteLine("7. Display all vehicle data by license number.");
            Console.WriteLine("8. Exit.");
        }

        private void manageInsertionOfNewVehicle()
        {
            try
            {
                string licenseNumber;
                bool isNewVehicleObjectCreated = tryInsertNewVehicleObjectToGarage(out licenseNumber);
                if (isNewVehicleObjectCreated)
                {
                    getAndSetInputPropertiesForVehicle(licenseNumber);
                    Console.WriteLine($"{Environment.NewLine}Vehicle inserted successfully");
                }
            }
            catch (Exception i_Exception)
            {
                printExceptionAndMessageAboutReturningToMainMenu(i_Exception);
            }
        }
        
        private bool tryInsertNewVehicleObjectToGarage(out string o_LicenseNumber)
        {    
            bool isNewVehicleObjectCreated = false;
            try
            {
                printMessageAskingLicenseNumber();
                o_LicenseNumber = Console.ReadLine();

                if (m_GarageManager.IsVehicleInGarage(o_LicenseNumber))
                {
                    m_GarageManager.TryChangeVehicleStatus(o_LicenseNumber, eVehicleStatus.InRepair);
                    Console.WriteLine("Vehicle already exists in the garage, status changed to 'In Repair'");
                }
                else
                {
                    try
                    {
                        eVehicleType eWantedVehicleType = getWantedVehicleType();
                        m_GarageManager.AddNewVehicleObjectToGarage(eWantedVehicleType, o_LicenseNumber);
                        isNewVehicleObjectCreated = true;
                    }
                    catch (Exception i_Exception)
                    {
                        Console.WriteLine(i_Exception.Message);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
           
            return isNewVehicleObjectCreated;
        }

        private void getAndSetInputPropertiesForVehicle(string i_LicenseNumber)
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
                        Console.WriteLine($"Possible values: {Environment.NewLine}{property.Value}");
                        string propertyValue = Console.ReadLine();
                        try
                        {
                            m_GarageManager.SetPropertyForVehicle(i_LicenseNumber, property.Key, propertyValue);
                            propertySetSuccessfully = true;
                        }
                        catch (Exception i_Exception)
                        {
                            Console.WriteLine($"Error: {i_Exception.Message}");
                        }
                    }
                    Console.WriteLine("Property set successfully" + Environment.NewLine);
                }
            }
            catch (Exception i_Exception)
            {
                printExceptionAndMessageAboutReturningToMainMenu(i_Exception);
            }
        }
        
        private void displayLicenseNumbers()
        {
            try
            { 
                bool isUserInputValid = false;

                while (!isUserInputValid)
                {
                    Console.WriteLine("Press 1 to display all license numbers");
                    Console.WriteLine("Press 2 to display license numbers by status");
                    string userInput = Console.ReadLine();

                    if (userInput == "1" || userInput == "2")
                    {
                        isUserInputValid = true;
                        if (userInput == "1")
                        {
                            List <string> allLicencePlates = m_GarageManager.GetListOfAllLicensePlates();
                            if (allLicencePlates.Count == 0)
                            {
                                Console.WriteLine("There are no vehicles in the garage");
                            }
                            else
                            {
                                Console.WriteLine("The license numbers of all vehicles in the garage are:");
                                foreach (string licensePlate in allLicencePlates)
                                {
                                    Console.WriteLine(licensePlate);
                                }
                            }
                        }
                        else if (userInput == "2")
                        {
                            displayLicenseNumbersByStatus();
                        }
                    }
                    else
                    {
                        printInvalidInputMessage();
                    }
                }
            }
            catch (Exception i_Exception)
            {
                printExceptionAndMessageAboutReturningToMainMenu(i_Exception);
            }
        }
        
        private void displayLicenseNumbersByStatus()
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
                        Console.WriteLine($"{index}. {m_GarageManager.FormatEnumValue(possibleStatus)}");
                        index++;
                    }

                    Console.WriteLine("Please enter the number corresponding to the status you want to filter by:");
                    string statusInput = Console.ReadLine();
                    if (int.TryParse(statusInput, out int statusChoice) && statusChoice >= 1 && statusChoice <= statusOptions.Count)
                    {
                        eVehicleStatus selectedStatus = statusOptions[statusChoice - 1];
                        List<string> licensePlates = m_GarageManager.GetListOfLicensePlatesByStatus(selectedStatus);
                        if (licensePlates.Count == 0)
                        {
                            Console.WriteLine("There are no vehicles with the selected status");
                        }
                        else
                        {
                            Console.WriteLine("The license numbers with the selected status are:");
                            Console.WriteLine(string.Join(Environment.NewLine, licensePlates));
                        }

                        isValidChoice = true;
                    }
                    else
                    {
                        printInvalidInputMessage();
                    }
                }
            }
            catch (Exception i_Exception)
            {
                printExceptionAndMessageAboutReturningToMainMenu(i_Exception);
            }
        }

        private void changeVehicleStatus()
        {
            try
            {
                bool isVehicleInGarage = tryGetVehicleLicenseNumber(out string licenseNumber);

                if (isVehicleInGarage)  
                {
                    List<eVehicleStatus> statusOptions = Enum.GetValues(typeof(eVehicleStatus)).Cast<eVehicleStatus>().ToList();
                    bool isValidChoice = false;

                    while (!isValidChoice)
                    {
                        Console.WriteLine("The status options are:");
                        int index = 1;
                        foreach (eVehicleStatus possibleStatus in statusOptions)
                        {
                            Console.WriteLine($"{index}. {m_GarageManager.FormatEnumValue(possibleStatus)}");
                            index++;
                        }

                        Console.WriteLine("Please enter the number corresponding to the status you want to change to:");
                        string statusInput = Console.ReadLine();
                        if (int.TryParse(statusInput, out int statusChoice) && statusChoice >= 1 && statusChoice <= statusOptions.Count)
                        {
                            eVehicleStatus selectedStatus = statusOptions[statusChoice - 1];
                            if(m_GarageManager.TryChangeVehicleStatus(licenseNumber, selectedStatus))
                            {
                                Console.WriteLine($"Status changed successfully to: {selectedStatus.ToString()}");
                            }
                            else
                            {
                                Console.WriteLine($"Status was already set to the selected status: {selectedStatus.ToString()}");
                            }
                            isValidChoice = true;
                        }
                        else
                        {
                            printInvalidInputMessage();
                        }
                    }
                }
            }
            catch (Exception i_Exception)
            {
                printExceptionAndMessageAboutReturningToMainMenu(i_Exception);
            }
        }
        
        private void inflateWheelsToMax()
        {
            try
            {
                bool isVehicleInGarage = tryGetVehicleLicenseNumber(out string licenseNumber);

                if (isVehicleInGarage)
                {
                    bool allWhellsAreAlreadyInflatedToMax = m_GarageManager.InflateVehicleWheelsToMax(licenseNumber);
                    m_GarageManager.TryChangeVehicleStatus(licenseNumber, eVehicleStatus.InRepair);
                    if (allWhellsAreAlreadyInflatedToMax)
                    {
                        Console.WriteLine("All wheels are already inflated to maximum.");
                    }
                    else
                    {
                        Console.WriteLine("Wheels inflated to maximum successfully.");
                    }
                }
            }
            catch (Exception i_Exception)
            {
                printExceptionAndMessageAboutReturningToMainMenu(i_Exception);
            }
        }

        private void manageAddingEnergyToVehicle(bool i_ShouldBeElectric)
        {
            try
            {
                bool isVehicleInGarage = tryGetVehicleLicenseNumber(out string licenseNumber);

                if (isVehicleInGarage)
                {
                    bool isElectricVehicle = m_GarageManager.IsElectricVehicle(licenseNumber);

                    if (i_ShouldBeElectric && !isElectricVehicle)
                    {
                        Console.WriteLine("Requested operation is for an electric vehicle, but the selected vehicle is not electric.");
                        Console.WriteLine("Returning to main menu.", Environment.NewLine);
                    }
                    else if (!i_ShouldBeElectric && isElectricVehicle)
                    {
                        Console.WriteLine("Requested operation is for a fuel-based vehicle, but the selected vehicle is not fuel-based.");
                        Console.WriteLine("Returning to main menu.", Environment.NewLine);
                    }
                    else
                    {
                        if (i_ShouldBeElectric)
                        {
                            tryRechargeVehicle(licenseNumber);
                        }
                        else
                        {
                            tryRefuelVehicle(licenseNumber);
                        }

                        m_GarageManager.TryChangeVehicleStatus(licenseNumber, eVehicleStatus.InRepair);
                    }
                }
            }
            catch (Exception i_Exception)
            {
                printExceptionAndMessageAboutReturningToMainMenu(i_Exception);
            }
        }

        private void tryRefuelVehicle(string i_LicenseNumber)
        {
            try
            {
                List<eFuelType> fuelTypes = Enum.GetValues(typeof(eFuelType)).Cast<eFuelType>().ToList();
                bool isValidChoice = false;

                while (!isValidChoice)
                {
                    Console.WriteLine("The fuel type options are:");
                    int index = 1;
                    foreach (eFuelType possibleFuelType in fuelTypes)
                    {
                        Console.WriteLine($"{index}. {possibleFuelType}");
                        index++;
                    }

                    Console.WriteLine("Please enter the number corresponding to the fuel type you want to refuel with:");
                    string fuelTypeInput = Console.ReadLine();
                    if (int.TryParse(fuelTypeInput, out int fuelTypeChoice) && fuelTypeChoice >= 1 && fuelTypeChoice <= fuelTypes.Count)
                    {
                        isValidChoice = true;
                        eFuelType selectedFuelType = fuelTypes[fuelTypeChoice - 1];
                        Console.WriteLine("Please enter the amount of fuel liters you want to add (positive float number):");
                        string fuelAmountInput = Console.ReadLine();
                        if (float.TryParse(fuelAmountInput, out float fuelAmount))
                        {
                            bool isFuelAdded = m_GarageManager.RefuelVehicle(i_LicenseNumber, selectedFuelType, fuelAmount);
                            if (isFuelAdded)
                            {
                                Console.WriteLine("Vehicle refueled successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Vehicle was not refueled. The fuel tank is already full.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input, expected a positive float number. please try again.");
                        }
                    }
                    else
                    {
                        printInvalidInputMessage();
                    }
                }
            }
            catch (Exception i_Exception)
            {
                Console.WriteLine(i_Exception.Message);
            }
        }

        private void tryRechargeVehicle(string i_LicenseNumber)
        {
            try
            { 
                Console.WriteLine("Please enter the amount of energy you want to add to the battery.");
                Console.WriteLine("Please enter it in minutes (positive integer number):");
                string electricityAmountInput = Console.ReadLine();
                if (int.TryParse(electricityAmountInput, out int electricityAmountInMinutes))
                {
                    bool isEnergyAdded = m_GarageManager.RehargeVehicle(i_LicenseNumber, electricityAmountInMinutes);
                    if (isEnergyAdded)
                    {
                        Console.WriteLine("Vehicle recharged successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Vehicle was not recharged. The battery is already full.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input, expected a positive integer number. please try again.");
                }
            }
            catch (Exception i_Exception)
            {
                Console.WriteLine(i_Exception.Message);
            }
        }

        private void displayVehicleDetails()
        {
            try
            {               
                bool isVehicleInGarage = tryGetVehicleLicenseNumber(out string licenseNumber);

                if (isVehicleInGarage)
                {
                    Console.WriteLine(m_GarageManager.GetVehicleDataWithStatus(licenseNumber));
                }
            }
            catch (Exception i_Exception)
            {
                Console.WriteLine(i_Exception.Message);
            }
        }

        private bool tryGetVehicleLicenseNumber(out string licenseNumber)
        {
            printMessageAskingLicenseNumber();
            licenseNumber = Console.ReadLine();
            Console.Write(Environment.NewLine);
            bool isVehicleInGarage = m_GarageManager.IsVehicleInGarage(licenseNumber);
            
            if (!isVehicleInGarage)
            {
                Console.WriteLine($"Vehicle with license number: {licenseNumber} is not in the garage.");
                Console.WriteLine("Returning to main menu.", Environment.NewLine);
            }

            return isVehicleInGarage;
        }

        private void printMessageAskingLicenseNumber()
        {
            Console.WriteLine("Please enter the vehicle's license number:");
        }

        private void printExceptionAndMessageAboutReturningToMainMenu(Exception i_Exception)
        {
            Console.WriteLine(i_Exception.Message);
            Console.WriteLine("Returning to main menu.", Environment.NewLine);
        }

        private void printInvalidInputChoiceMessage()
        {
            printInvalidInputMessage();
            Console.WriteLine($"Please enter a number from the menu (an integer from {k_MinOptionRange} to {k_MaxOptionRange})");
        }

        private void printInvalidInputMessage()
        {
            Console.WriteLine("Invalid input, please try again");
        }

        private eVehicleType getWantedVehicleType()
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
                    Console.WriteLine($"You selected: {vehicleTypes[userInput - 1]}{Environment.NewLine}" );

                }
                else
                {
                    printInvalidInputMessage();
                }
            }

            return wantedVehicleType;
        }
    }
}
