﻿using System;
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

        public eVehicleStatus GetVehicleStatus(string i_LicensePlate)
        {
            return m_AllGarageVehiclesData[i_LicensePlate].VehicleStatus;
        }
    
        public void ChangeVehicleStatus(string i_LicensePlate, eVehicleStatus i_NewStatus)
        {
            m_AllGarageVehiclesData[i_LicensePlate].VehicleStatus = i_NewStatus;
        }
       
        public eVehicleType GetVehicleType(string i_LicensePlate)
        {
            return m_AllGarageVehiclesData[i_LicensePlate].VehicleType;
        }
        
        public string GetListOfVehicleTypes()
        {
            StringBuilder vehicleTypes = new StringBuilder();
            foreach (eVehicleType vehicleType in Enum.GetValues(typeof(eVehicleType)))
            {
                vehicleTypes.AppendLine(vehicleType.ToString());
            }

            return vehicleTypes.ToString();
        }   

        public void addVehicleToGarageByLicensePlate(string i_LicensePlate, Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            GarageSingleVehicleData vehicleData = new GarageVehicleData(i_Vehicle, i_OwnerName, i_OwnerPhoneNumber, eVehicleStatus.InRepair);
            m_AllGarageVehiclesData.Add(i_LicensePlate, vehicleData);
        }
        public void AddVehicleToGarage(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            GarageSingleVehicleData vehicleData = new GarageVehicleData(i_Vehicle, i_OwnerName, i_OwnerPhoneNumber, eVehicleStatus.InRepair);
            m_AllGarageVehiclesData.Add(i_Vehicle.LicensePlate, vehicleData);
        }

        public void AddPropertiesToVehicleAccordingToActualType(string i_LicensePlate, Dictionary<string, string> i_Properties)
        {
            Vehicle vehicle = m_AllGarageVehiclesData[i_LicensePlate].Vehicle;
            try
            {
                switch (vehicle)
                {
                    case ElectricCar electricCar:
                        {
                            electricCar.SetProperties(i_Properties);
                            break;
                        }
                    case ElectricMotorcycle electricMotorcycle:
                        {
                            electricMotorcycle.SetProperties(i_Properties);
                            break;
                        }
                    case FuelCar fuelCar:
                        {
                            fuelCar.SetProperties(i_Properties);
                            break;
                        }
                    case FuelMotorcycle fuelMotorcycle:
                        {
                            fuelMotorcycle.SetProperties(i_Properties);
                            break;
                        }
                    case FuelTruck fuelTruck:
                        {
                            fuelTruck.SetProperties(i_Properties);
                            break;
                        }
                    default:
                        {
                            throw new ArgumentException("Vehicle type is not supported");
                        }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddPropertiesToElectricCar(string i_LicensePlate, string i_OwnerName, string i_OwnerPhoneNumber,
                                               string i_ModelName, string i_LicenseNumber, List<Wheel> i_WheelsList,
                                               float i_CurrentBatteryEnergyLevel, float i_MaxBatteryEnergyLevel,
                                               eColor i_Color, eNumOfDoors i_NumOfDoors)
        {
            if (m_AllGarageVehiclesData.TryGetValue(i_LicensePlate, out GarageSingleVehicleData vehicleData))
            {
                ElectricCar electricCar = vehicleData.Vehicle as ElectricCar;
                if (electricCar != null)
                {
                    // Now you have an ElectricCar reference, you can set its properties
                    m_AllGarageVehiclesData[i_LicensePlate].OwnerName = i_OwnerName;
                    m_AllGarageVehiclesData[i_LicensePlate].OwnerPhoneNumber = i_OwnerPhoneNumber;
                    electricCar.SetElectricCarValues(i_ModelName, i_LicenseNumber, i_WheelsList,
                                                     i_CurrentBatteryEnergyLevel, i_MaxBatteryEnergyLevel,
                                                     i_Color, i_NumOfDoors);  
                }
                else
                {
                    throw new ArgumentException("The vehicle with the given license plate is not an Electric Car.");
                }
            }
            else
            {
                throw new KeyNotFoundException("The vehicle with the given license plate was not found in the garage.");
            }
        }
        public void AddNewVehicleObjectToGarage(eVehicleType i_VehicleType, string i_LicenseNumber)
        {
            try
            {
                Vehicle currentVehicle = VehicleFactory.CreateVehicle(i_VehicleType);
                m_AllGarageVehiclesData.Add(i_LicenseNumber, new GarageSingleVehicleData(currentVehicle));
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

        public List<string> GetListOfNeededProperties(string i_LicenseNumber)
        {
            try
            {
                Vehicle vehicle = m_AllGarageVehiclesData[i_LicenseNumber].Vehicle;
                List<string> properties = new List<string>
                {
                    "Owner name",
                    "Owner phone number"
                };

                switch (vehicle)
                {
                    case ElectricCar electricCar:
                        {
                            properties = electricCar.GetListOfProperties();
                            break;
                        }
                    case ElectricMotorcycle electricMotorcycle:
                        {
                            properties = electricMotorcycle.GetListOfProperties();
                            break;
                        }
                    case FuelCar fuelCar:
                        {
                            properties = fuelCar.GetListOfProperties();
                            break;
                        }
                    case FuelMotorcycle fuelMotorcycle:
                        {
                            properties = fuelMotorcycle.GetListOfProperties();
                            break;
                        }
                    case FuelTruck fuelTruck:
                        {
                            properties = fuelTruck.GetListOfProperties();
                            break;
                        }
                    default:
                        {
                            throw new ArgumentException("Vehicle type is not supported");
                        }
                }
                
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

        public void InflateVehicleWheelsToMax(string i_LicensePlate)
        {
            //foreach (Wheel wheel in m_GarageVehiclesData[i_LicensePlate].Vehicle.Wheels)
            //{
            //    wheel.InflateWheel(wheel.MaxAirPressure - wheel.CurrentAirPressure);
            //}
            m_AllGarageVehiclesData[i_LicensePlate].Vehicle.InflateWheelsToMax();
        }

        public void RefuelVehicle(string i_LicensePlate, eFuelType i_FuelType, float i_FuelAmountInLiters)
        {
            Vehicle vehicle = m_AllGarageVehiclesData[i_LicensePlate].Vehicle;
            
            try
            {
                //IsFuelTypeMatch can throw an exception of it's own -- if the vehicle is not a fuel vehicle    
                if (!IsFuelTypeMatch(i_LicensePlate, i_FuelType))
                { 
                    throw new ArgumentException("The fuel type does not match the vehicle's fuel type.");
                }
               
                switch (vehicle)
                {
                    case FuelCar fuelCar:
                    {
                        fuelCar.FillEnergy(i_FuelAmountInLiters, i_FuelType);
                        break;
                    }
                    case FuelTruck fuelTruck:
                    {
                        fuelTruck.FillEnergy(i_FuelAmountInLiters, i_FuelType);
                        break;
                    }
                    case FuelMotorcycle fuelMotorcycle:
                    {
                        fuelMotorcycle.FillEnergy(i_FuelAmountInLiters, i_FuelType);
                        break;
                    }
                }
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
            eFuelType? eFuelType = null;
            Vehicle vehicle = m_AllGarageVehiclesData[i_LicensePlate].Vehicle;
            
            if (!IsFuelVehicle(i_LicensePlate))
            {
                throw new ArgumentException("This vehicle is not a fuel vehicle.");
            }

            //The method will get here only if the vehicle is a fuel vehicle,
            //and it must have a fuel type in this case (the ctors of the fuel vehicles set the fuel type)
            switch (vehicle)
            {
                case FuelCar fuelCar:
                    {
                        eFuelType = fuelCar.FuelTankOfCar.FuelType;
                        break;
                    }
                case FuelTruck fuelTruck:
                    {
                        eFuelType = fuelTruck.FuelTankOfTruck.FuelType;
                        break;
                    }
                case FuelMotorcycle fuelMotorcycle:
                    {
                        eFuelType = fuelMotorcycle.FuelTankOfMotorcycle.FuelType;
                        break;
                    }
            }
            
            return eFuelType.Value;
        }

        private bool IsFuelVehicle(string i_LicensePlate)
        {
            eVehicleType vehicleType = m_AllGarageVehiclesData[i_LicensePlate].VehicleType;
            return vehicleType == eVehicleType.FuelCar || vehicleType == eVehicleType.FuelMotorcycle || vehicleType == eVehicleType.FuelTruck;
        }

        public void ChargeVehicle(string i_LicensePlate, float i_ElectricityAmountInMinutes)
        {
            Vehicle vehicle = m_AllGarageVehiclesData[i_LicensePlate].Vehicle; 
            
            if (!IsElectricVehicle(i_LicensePlate))
            {
                throw new ArgumentException("This vehicle is not an electric vehicle.");
            }

            try
            {
                switch (vehicle)
                {
                    case ElectricCar electricCar:
                        {
                            electricCar.FillEnergy(i_ElectricityAmountInMinutes, null);
                            break;
                        }
                    case ElectricMotorcycle electricMotorcycle:
                        {
                            electricMotorcycle.FillEnergy(i_ElectricityAmountInMinutes, null);
                            break;
                        }
                }
            }
            catch (Exception)
            {
                throw;
            }

            //ElectricBattery electricBattery = ((ElectricBattery)m_GarageVehiclesData[i_LicensePlate].Vehicle.EnergySource);
            //electricBattery.ChargeBattery(i_EnergyAmount);
        }
        
        private bool IsElectricVehicle(string i_LicensePlate)
        {
            eVehicleType vehicleType = m_AllGarageVehiclesData[i_LicensePlate].VehicleType;
            return vehicleType == eVehicleType.ElectricCar || vehicleType == eVehicleType.ElectricMotorcycle;
        }

        //public string GetVehicleData(string i_LicensePlate)
        //{
        //    return m_GarageVehiclesData[i_LicensePlate].Vehicle.ToString();
        //}

        //implement this method in the UI component?
        public string GetVehicleDataWithStatus(string i_LicensePlate)
        {
            Vehicle vehicle = m_AllGarageVehiclesData[i_LicensePlate].Vehicle;
            string vehicleData = string.Empty;

            //switch (vehicle)
            //{
            //    case ElectricCar electricCar:
            //        {
            //            return string.Format("{0}{1}Vehicle status: {2}{1}", electricCar.ToString(), Environment.NewLine,
            //                m_GarageVehiclesData[i_LicensePlate].VehicleStatus);
            //        }
            //}
            
            return string.Format("{0}{1}Vehicle status: {2}{1}", m_AllGarageVehiclesData[i_LicensePlate].Vehicle.ToString(), Environment.NewLine, m_AllGarageVehiclesData[i_LicensePlate].VehicleStatus);
        }

        public GarageSingleVehicleData GetGarageVehicleData(string i_LicensePlate)
        {
            try
            {
                return m_AllGarageVehiclesData[i_LicensePlate];
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("This vehicle is not in the garage.");
            }
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

        public List<eColor> eColorsOptions
        {
            get
            {
                return Enum.GetValues(typeof(eColor)).Cast<eColor>().ToList();
            }
        }

        public List<eFuelType> eFuelTypesOptions
        {
            get
            {
                return Enum.GetValues(typeof(eFuelType)).Cast<eFuelType>().ToList();
            }
        }

        public List<eLicenseType> eLicenseTypesOptions
        {
            get
            {
                return Enum.GetValues(typeof(eLicenseType)).Cast<eLicenseType>().ToList();
            }
        }

        public List<eNumOfDoors> eNumOfDoorsOptions
        {
            get
            {
                return Enum.GetValues(typeof(eNumOfDoors)).Cast<eNumOfDoors>().ToList();
            }
        }

        public List<eVehicleStatus> eVehicleStatusesOptions
        {
            get
            {
                return Enum.GetValues(typeof(eVehicleStatus)).Cast<eVehicleStatus>().ToList();
            }
        }

        public List<eVehicleType> eVehicleTypesOptions
        {
            get
            {
                return Enum.GetValues(typeof(eVehicleType)).Cast<eVehicleType>().ToList();
            }
        }




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