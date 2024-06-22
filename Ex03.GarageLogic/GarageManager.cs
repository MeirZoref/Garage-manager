using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private Dictionary<string, GarageSingleVehicleData> m_AllGarageVehiclesData = new Dictionary<string, GarageSingleVehicleData>();
        private const int k_NumOfMinutesInHour = 60;
 
        public Vehicle AddNewVehicleObjectToGarage(eVehicleType i_VehicleType, string i_LicenseNumber)
        {
            try
            {
                Vehicle referenceToNewVehicle = VehicleFactory.CreateVehicle(i_VehicleType);
                referenceToNewVehicle.LicenseNumber = i_LicenseNumber;
                m_AllGarageVehiclesData.Add(i_LicenseNumber, new GarageSingleVehicleData(referenceToNewVehicle));

                return referenceToNewVehicle;
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"A vehicle with the license number {i_LicenseNumber} is already in the garage.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SetPropertyForVehicle(string i_LicensePlate, string i_PropertyName, string i_PropertyValue)
        {
            try
            {
                switch (i_PropertyName)
                {
                    case "Owner name":
                    {
                        m_AllGarageVehiclesData[i_LicensePlate].OwnerName = i_PropertyValue;
                        break;
                    }
                    case "Owner phone number":
                    {
                        m_AllGarageVehiclesData[i_LicensePlate].OwnerPhoneNumber = i_PropertyValue;
                        break;
                    }
                    default:
                    {
                        m_AllGarageVehiclesData[i_LicensePlate].Vehicle.SetProperty(i_PropertyName, i_PropertyValue);
                        break;
                    }
                }
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("This vehicle is not in the garage.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<KeyValuePair<string,string>> GetListOfNeededPropertiesAndPossibleValues(string i_LicenseNumber)
        {
            try
            {
                Vehicle vehicle = m_AllGarageVehiclesData[i_LicenseNumber].Vehicle;
                List<KeyValuePair<string, string>> properties = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Owner name", "String (any set of characters)"),
                    new KeyValuePair<string, string>("Owner phone number", "Numbers and other charcters")
                };
                properties.AddRange(vehicle.GetListOfPropertiesAndPossibleValues());
                
                return properties;
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("This vehicle is not in the garage.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsVehicleInGarage(string i_LicensePlate)
        {
            return m_AllGarageVehiclesData.ContainsKey(i_LicensePlate);
        }

        public List<string> GetListOfAllLicensePlates()
        {
            List<string> licensePlates = new List<string>();
            foreach (string licensePlate in m_AllGarageVehiclesData.Keys)
            {
                licensePlates.Add(licensePlate);
            }

            return licensePlates;
        }

        public List<string> GetListOfLicensePlatesByStatus(eVehicleStatus i_Status)
        {
            List<string> licensePlates = new List<string>();
            foreach (KeyValuePair<string, GarageSingleVehicleData> vehicleData in m_AllGarageVehiclesData)
            {
                if (vehicleData.Value.VehicleStatus == i_Status)
                {
                    licensePlates.Add(vehicleData.Key);
                }
            }

            return licensePlates;
        }

        public bool TryChangeVehicleStatus(string i_LicensePlate, eVehicleStatus i_NewStatus)
        {
            eVehicleStatus vehicleStatus = m_AllGarageVehiclesData[i_LicensePlate].VehicleStatus;
            bool isStatusChanged = false;
            if (vehicleStatus != i_NewStatus)
            {
                m_AllGarageVehiclesData[i_LicensePlate].VehicleStatus = i_NewStatus;
                isStatusChanged = true;
            }

            return isStatusChanged;
        }

        public bool InflateVehicleWheelsToMax(string i_LicensePlate)
        {
            bool allWhellsAreAlreadyInflatedToMax = false;

            if (!m_AllGarageVehiclesData.ContainsKey(i_LicensePlate))
            {
                throw new ArgumentException("This vehicle is not in the garage.");
            }
           
            allWhellsAreAlreadyInflatedToMax = m_AllGarageVehiclesData[i_LicensePlate].Vehicle.InflateWheelsToMax();

            return allWhellsAreAlreadyInflatedToMax;
        }

        public bool RefuelVehicle(string i_LicensePlate, eFuelType i_FuelType, float i_FuelAmountInLiters)
        {
            try
            {
                Vehicle vehicle = m_AllGarageVehiclesData[i_LicensePlate].Vehicle;

                if(!IsVehicleFuelBased(i_LicensePlate))
                {
                    throw new ArgumentException("This vehicle is not a fuel vehicle.");
                }

                //IsFuelTypeMatch can throw an exception of it's own -- if the vehicle is not a fuel vehicle    
                if (!isFuelTypeMatch(i_LicensePlate, i_FuelType))
                {
                    throw new ArgumentException("The fuel type does not match the vehicle's fuel type. Please try again");
                }

                bool isEnergyAdded = vehicle.TryFillEnergy(i_FuelAmountInLiters, i_FuelType);

                return isEnergyAdded;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsVehicleFuelBased(string i_LicensePlate)
        {
            return !IsElectricVehicle(i_LicensePlate);
        }
        
        private bool isFuelTypeMatch(string i_LicensePlate, eFuelType i_FuelType)
        {
            try
            {
                eFuelType fuelType = getFuelTypeOfVehicle(i_LicensePlate);
                return fuelType == i_FuelType;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private eFuelType getFuelTypeOfVehicle(string i_LicensePlate)
        {
            try
            {
                eFuelType? eFuelType = null;
                Vehicle vehicle = m_AllGarageVehiclesData[i_LicensePlate].Vehicle;

                if (!IsVehicleFuelBased(i_LicensePlate))
                {
                    throw new ArgumentException("This vehicle is not a fuel vehicle.");
                }
                else
                {
                    eFuelType = vehicle.VehicleFuelType;

                    if (eFuelType.HasValue)
                    {
                        return eFuelType.Value;
                    }
                    else
                    {
                        throw new ArgumentException("This vehicle doesn't have fuel type set.");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RehargeVehicle(string i_LicensePlate, int i_ElectricityAmountInMinutes)
        {
            try
            {   
                if(!IsElectricVehicle(i_LicensePlate))
                {
                    throw new ArgumentException("This vehicle is not an electric vehicle.");
                }
                else
                {
                    Vehicle vehicle = m_AllGarageVehiclesData[i_LicensePlate].Vehicle;
                    float i_ElectricityAmountInHours = convertNumOfMinutesToHours(i_ElectricityAmountInMinutes);
                    bool isEnergyAdded = vehicle.TryFillEnergy(i_ElectricityAmountInHours, null);

                    return isEnergyAdded;
                }
            }
            catch (Exception)
            {
                throw;
            }           
        }

        public bool IsElectricVehicle(string i_LicensePlate)
        {
            try
            {
                Vehicle vehicle = m_AllGarageVehiclesData[i_LicensePlate].Vehicle;
                if (vehicle.IsElectric.HasValue)
                {
                    return vehicle.IsElectric.Value;
                }
                else
                {
                    throw new ArgumentException("This vehicle doesn't have energy type set.");

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private float convertNumOfMinutesToHours(int i_ElectricityAmountInMinutes)
        {
            return (float)i_ElectricityAmountInMinutes / k_NumOfMinutesInHour;
        }

        public string GetVehicleDataWithStatus(string i_LicensePlate)
        {
            try
            {
                List<string> listOfAllData = new List<string>();
                Vehicle vehicle = m_AllGarageVehiclesData[i_LicensePlate].Vehicle;

                listOfAllData.Add($"Data of vehicle with license plate: {i_LicensePlate}");
                listOfAllData.Add($"Owner name: {m_AllGarageVehiclesData[i_LicensePlate].OwnerName}");
                listOfAllData.Add($"Owner phone number: {m_AllGarageVehiclesData[i_LicensePlate].OwnerPhoneNumber}");
                listOfAllData.Add($"Vehicle status: {FormatEnumValue(m_AllGarageVehiclesData[i_LicensePlate].VehicleStatus)}");
                listOfAllData.Add(vehicle.ToString());

                return string.Join(Environment.NewLine, listOfAllData);
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("This vehicle is not in the garage.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string FormatEnumValue(Enum i_EnumValue)
        {
            string text = i_EnumValue.ToString();
            text = Regex.Replace(text, "(\\B[A-Z])", " $1");
            text = text.ToLower();
            if (text.Length > 0)
            {
                text = char.ToUpper(text[0]) + text.Substring(1);
            }

            return text;
        }
                
        public List<string> GetListOfSupportedVehicleTypesAsStrings()
        {
            List<string> supportedVehicleTypes = new List<string>();
            foreach (eVehicleType possibleVehicleType in Enum.GetValues(typeof(eVehicleType)))
            {
                string vehicleType = possibleVehicleType.ToString();
                vehicleType = Regex.Replace(vehicleType, "(\\B[A-Z])", " $1");
                supportedVehicleTypes.Add(vehicleType.ToString());
            }

            return supportedVehicleTypes;
        }
    }
}
