using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private Dictionary<string, GarageSingleVehicleData> m_AllGarageVehiclesData = new Dictionary<string, GarageSingleVehicleData>();
        private const int k_NumOfMinutesInHour = 60;

        //private VehicleFactory m_VehicleFactory = new VehicleFactory();

        //public GarageManager()
        //{
        //    m_GarageVehiclesData = ;
        //}



        public bool IsVehicleInGarage(string i_LicensePlate)
        {
            //this.ChangeVehicleStatus(i_LicensePlate, eVehicleStatus.InRepair);
            return m_AllGarageVehiclesData.ContainsKey(i_LicensePlate);
        }

        //public eVehicleStatus GetVehicleStatus(string i_LicensePlate)
        //{
        //    return m_AllGarageVehiclesData[i_LicensePlate].VehicleStatus;
        //}
    
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
       
        //public eVehicleType GetVehicleType(string i_LicensePlate)
        //{
        //    return m_AllGarageVehiclesData[i_LicensePlate].VehicleType;
        //}
        
        //public string GetListOfVehicleTypes()
        //{
        //    StringBuilder vehicleTypes = new StringBuilder();
        //    foreach (eVehicleType vehicleType in Enum.GetValues(typeof(eVehicleType)))
        //    {
        //        vehicleTypes.AppendLine(vehicleType.ToString());
        //    }

        //    return vehicleTypes.ToString();
        //}   

        //public void addVehicleToGarageByLicensePlate(string i_LicensePlate, Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        //{
        //    GarageSingleVehicleData vehicleData = new GarageVehicleData(i_Vehicle, i_OwnerName, i_OwnerPhoneNumber, eVehicleStatus.InRepair);
        //    m_AllGarageVehiclesData.Add(i_LicensePlate, vehicleData);
        //}
        //public void AddVehicleToGarage(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        //{
        //    GarageSingleVehicleData vehicleData = new GarageVehicleData(i_Vehicle, i_OwnerName, i_OwnerPhoneNumber, eVehicleStatus.InRepair);
        //    m_AllGarageVehiclesData.Add(i_Vehicle.LicensePlate, vehicleData);
        //}

        //public void AddPropertiesToVehicleAccordingToActualType(string i_LicensePlate, Dictionary<string, string> i_Properties)
        //{
        //    Vehicle vehicle = m_AllGarageVehiclesData[i_LicensePlate].Vehicle;
        //    try
        //    {
        //        switch (vehicle)
        //        {
        //            case ElectricCar electricCar:
        //                {
        //                    electricCar.SetProperties(i_Properties);
        //                    break;
        //                }
        //            case ElectricMotorcycle electricMotorcycle:
        //                {
        //                    electricMotorcycle.SetProperties(i_Properties);
        //                    break;
        //                }
        //            case FuelCar fuelCar:
        //                {
        //                    fuelCar.SetProperties(i_Properties);
        //                    break;
        //                }
        //            case FuelMotorcycle fuelMotorcycle:
        //                {
        //                    fuelMotorcycle.SetProperties(i_Properties);
        //                    break;
        //                }
        //            case FuelTruck fuelTruck:
        //                {
        //                    fuelTruck.SetProperties(i_Properties);
        //                    break;
        //                }
        //            default:
        //                {
        //                    throw new ArgumentException("Vehicle type is not supported");
        //                }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public void AddPropertiesToElectricCar(string i_LicensePlate, string i_OwnerName, string i_OwnerPhoneNumber,
        //                                       string i_ModelName, string i_LicenseNumber, List<Wheel> i_WheelsList,
        //                                       float i_CurrentBatteryEnergyLevel, float i_MaxBatteryEnergyLevel,
        //                                       eColor i_Color, eNumOfDoors i_NumOfDoors)
        //{
        //    if (m_AllGarageVehiclesData.TryGetValue(i_LicensePlate, out GarageSingleVehicleData vehicleData))
        //    {
        //        ElectricCar electricCar = vehicleData.Vehicle as ElectricCar;
        //        if (electricCar != null)
        //        {
        //            // Now you have an ElectricCar reference, you can set its properties
        //            m_AllGarageVehiclesData[i_LicensePlate].OwnerName = i_OwnerName;
        //            m_AllGarageVehiclesData[i_LicensePlate].OwnerPhoneNumber = i_OwnerPhoneNumber;
        //            electricCar.SetElectricCarValues(i_ModelName, i_LicenseNumber, i_WheelsList,
        //                                             i_CurrentBatteryEnergyLevel, i_MaxBatteryEnergyLevel,
        //                                             i_Color, i_NumOfDoors);  
        //        }
        //        else
        //        {
        //            throw new ArgumentException("The vehicle with the given license plate is not an Electric Car.");
        //        }
        //    }
        //    else
        //    {
        //        throw new KeyNotFoundException("The vehicle with the given license plate was not found in the garage.");
        //    }
        //}
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
            // throw new ArgumentException("Vehicle type is not supported"); ?

        }

        public void SetProperty(string i_LicensePlate, string i_PropertyName, string i_PropertyValue)
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
                //m_AllGarageVehiclesData[i_LicensePlate].Vehicle.SetProperty(i_PropertyName, i_PropertyValue);
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
                    new KeyValuePair<string, string>("Owner name", "String (any set of charcters)"),
                    new KeyValuePair<string, string>("Owner phone number", "numbers and/or hyphens")
                };
                properties.AddRange(vehicle.GetListOfPropertiesAndPossibleValues());
                //vehicle.GetListOfPropertiesAndPossibleValues();
                //List<string> properties = new List<string>
                //{
                //    "Owner name", "String (any set of charcters)",
                //    "Owner phone number", "numbers and/or hyphens"
                //};
                //properties.AddRange(vehicle.GetListOfPropertiesAndPossibleValues());              
                
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

        public bool InflateVehicleWheelsToMax(string i_LicensePlate)
        {
            bool allWhellsAreAlreadyInflatedToMax = false;

            if (!m_AllGarageVehiclesData.ContainsKey(i_LicensePlate))
            {
                throw new ArgumentException("This vehicle is not in the garage.");
            }

            //foreach (Wheel wheel in m_GarageVehiclesData[i_LicensePlate].Vehicle.Wheels)
            //{
            //    wheel.InflateWheel(wheel.MaxAirPressure - wheel.CurrentAirPressure);
            //}
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
                if (!IsFuelTypeMatch(i_LicensePlate, i_FuelType))
                {
                    throw new ArgumentException("The fuel type does not match the vehicle's fuel type.");
                }

                bool isEnergyAdded = vehicle.TryFillEnergy(i_FuelAmountInLiters, i_FuelType);

                return isEnergyAdded;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsFuelTypeMatch(string i_LicensePlate, eFuelType i_FuelType)
        {
            try
            {
                eFuelType fuelType = GetFuelTypeOfVehicle(i_LicensePlate);
                return fuelType == i_FuelType;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private eFuelType GetFuelTypeOfVehicle(string i_LicensePlate)
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

            ////The method will get here only if the vehicle is a fuel vehicle,
            ////and it must have a fuel type in this case (the ctors of the fuel vehicles set the fuel type)
            //switch (vehicle)
            //{
            //    case FuelCar fuelCar:
            //        {
            //            eFuelType = fuelCar.FuelTankOfCar.FuelType;
            //            break;
            //        }
            //    case FuelTruck fuelTruck:
            //        {
            //            eFuelType = fuelTruck.FuelTankOfTruck.FuelType;
            //            break;
            //        }
            //    case FuelMotorcycle fuelMotorcycle:
            //        {
            //            eFuelType = fuelMotorcycle.FuelTankOfMotorcycle.FuelType;
            //            break;
            //        }
            //}
            
            //return eFuelType.Value;
        }

        public bool IsVehicleFuelBased(string i_LicensePlate)
        {
            return !IsElectricVehicle(i_LicensePlate);
            //return eVehicleType vehicleType = m_AllGarageVehiclesData[i_LicensePlate].VehicleType;
            //return vehicleType == eVehicleType.FuelCar || vehicleType == eVehicleType.FuelMotorcycle || vehicleType == eVehicleType.FuelTruck;
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
                    float i_ElectricityAmountInHours = ConvertNumOfMinutesToHours(i_ElectricityAmountInMinutes);
                    bool isEnergyAdded = vehicle.TryFillEnergy(i_ElectricityAmountInHours, null);

                    return isEnergyAdded;
                }
            }
            catch (Exception)
            {
                throw;
            }           
        }

        private float ConvertNumOfMinutesToHours(int i_ElectricityAmountInMinutes)
        {
            return (float)i_ElectricityAmountInMinutes / k_NumOfMinutesInHour;
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
            //eVehicleType vehicleType = m_AllGarageVehiclesData[i_LicensePlate].VehicleType;
            //return vehicleType == eVehicleType.ElectricCar || vehicleType == eVehicleType.ElectricMotorcycle;
        }

        //public string GetVehicleData(string i_LicensePlate)
        //{
        //    return m_GarageVehiclesData[i_LicensePlate].Vehicle.ToString();
        //}

        //implement this method in the UI component?
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

        public string FormatEnumValue(Enum enumValue)
        {
            string text = enumValue.ToString();
            // Insert space before each uppercase letter, except the first one.
            text = Regex.Replace(text, "(\\B[A-Z])", " $1");
            // Convert the entire string to lowercase
            text = text.ToLower();
            // Capitalize the first letter of the entire string
            if (text.Length > 0)
            {
                text = char.ToUpper(text[0]) + text.Substring(1);
            }
            return text;
        }


        //public GarageSingleVehicleData GetGarageVehicleData(string i_LicensePlate)
        //{
        //    try
        //    {
        //        return m_AllGarageVehiclesData[i_LicensePlate];
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        throw new ArgumentException("This vehicle is not in the garage.");
        //    }
        //}


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

        public List<string> GetListOfSupportedVehicleTypesAsStrings()
        {
            List<string> supportedVehicleTypes = new List<string>();
            foreach (eVehicleType possibleVehicleType in Enum.GetValues(typeof(eVehicleType)))
            {
                string vehicleType = possibleVehicleType.ToString();
                vehicleType = Regex.Replace(vehicleType, "(\\B[A-Z])", " $1");
                supportedVehicleTypes.Add(vehicleType.ToString());
            }
            //foreach (eVehicleType possibleVehicleType in Enum.GetValues(typeof(eVehicleType)))
            //{
            //    string vehicleType = possibleVehicleType.ToString();
            //    vehicleType = Regex.Replace(vehicleType, "(\\B[A-Z])", " $1");
            //}

            return supportedVehicleTypes;
        }


        //public List<eColor> eColorsOptions
        //{
        //    get
        //    {
        //        return Enum.GetValues(typeof(eColor)).Cast<eColor>().ToList();
        //    }
        //}

        //public List<eFuelType> eFuelTypesOptions
        //{
        //    get
        //    {
        //        return Enum.GetValues(typeof(eFuelType)).Cast<eFuelType>().ToList();
        //    }
        //}

        //public List<eLicenseType> eLicenseTypesOptions
        //{
        //    get
        //    {
        //        return Enum.GetValues(typeof(eLicenseType)).Cast<eLicenseType>().ToList();
        //    }
        //}

        //public List<eNumOfDoors> eNumOfDoorsOptions
        //{
        //    get
        //    {
        //        return Enum.GetValues(typeof(eNumOfDoors)).Cast<eNumOfDoors>().ToList();
        //    }
        //}

        //public List<eVehicleStatus> eVehicleStatusesOptions
        //{
        //    get
        //    {
        //        return Enum.GetValues(typeof(eVehicleStatus)).Cast<eVehicleStatus>().ToList();
        //    }
        //}

        //public List<eVehicleType> eVehicleTypesOptions
        //{
        //    get
        //    {
        //        return Enum.GetValues(typeof(eVehicleType)).Cast<eVehicleType>().ToList();
        //    }
        //}




        //public List<string> GetLicensePlates
    }
}


////FuelTank fuelTank;
//bool isFuelTypeMatch = false;   
//eFuelType? eFuelType = null;
//Vehicle vehicle = m_GarageVehiclesData[i_LicensePlate].Vehicle;

//switch (vehicle)
//{
//    case FuelCar fuelCar:
//        {
//            eFuelType = fuelCar.FuelTankOfCar.FuelType;
//            break;
//        }
//    case FuelTruck fuelTruck:
//        {
//            eFuelType = fuelTruck.FuelTankOfTruck.FuelType;
//            break;
//        }
//    case FuelMotorcycle fuelMotorcycle:
//        {
//            eFuelType = fuelMotorcycle.FuelTankOfMotorcycle.FuelType;
//            break;
//        }
//    default:
//        //{
//        //    throw new ArgumentException("This fuel type is not available.");
//        //        // Handle the case where it's none of the expected types
//        //}
//        break;
//}
////eFuelType fuelType = ((FuelTank)m_GarageVehiclesData[i_LicensePlate].Vehicle.EnergySource).FuelType;
////FuelTank fuelTank = ((FuelTank)m_GarageVehiclesData[i_LicensePlate].Vehicle.EnergySource);
//if (eFuelType.HasValue)
//{
//    isFuelTypeMatch = eFuelType.Value == i_FuelType;    
//}

//return isFuelTypeMatch;