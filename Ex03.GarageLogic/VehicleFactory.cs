using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        public static Vehicle CreateVehicle(eVehicleType i_VehicleType)
        {
            Vehicle newVehicle = null;
            
            switch (i_VehicleType)
            {
                case eVehicleType.ElectricCar:
                {
                    newVehicle = new ElectricCar();
                    break;
                }
                case eVehicleType.ElectricMotorcycle:
                {
                    newVehicle = new ElectricMotorcycle();
                    break;
                }
                case eVehicleType.FuelCar:
                {
                    newVehicle = new FuelCar();
                    break;
                }
                case eVehicleType.FuelMotorcycle:
                {
                    newVehicle = new FuelMotorcycle();
                    break;
                }
                case eVehicleType.FuelTruck:
                {
                    newVehicle = new FuelTruck();
                    break;
                }
                default:
                {
                    throw new ArgumentException("Vehicle type is not supported");
                }
            }

            return newVehicle;
        }
        
        ////change to specific vehicle types ???
        ///
        //public List<string> SupportedVehicleTypesList
        //{
        //    get
        //    {
        //        return Enum.GetNames(typeof(eVehicleType)).ToList();
        //        //return Enum.GetValues(typeof(eVehicleType));
        //    }
        //}



        //public static Vehicle CreateVehicle(eVehicleType i_VehicleType, string i_ModelName, string i_LicensePlate, string i_WheelManufacturerName,float i_CurrentAirPressure, float i_MaxAirPressure, string i_OwnerName, string i_OwnerPhoneNumber, eVehicleStatus i_VehicleStatus, float i_CurrentEnergy, float i_MaxEnergy, eFuelType i_FuelType, float i_CurrentFuelAmountLiters, float i_MaxFuelAmountLiters)
        //{
        //    Vehicle newVehicle = null;
        //    switch (i_VehicleType)
        //    {
        //        case eVehicleType.ElectricCar:
        //            newVehicle = new ElectricCar(i_ModelName, i_LicensePlate, i_WheelManufacturerName, i_CurrentAirPressure, i_MaxAirPressure, i_OwnerName, i_OwnerPhoneNumber, i_VehicleStatus, i_CurrentEnergy, i_MaxEnergy);
        //            break;
        //        case eVehicleType.ElectricMotorcycle:
        //            newVehicle = new ElectricMotorcycle(i_ModelName, i_LicensePlate, i_WheelManufacturerName, i_CurrentAirPressure, i_MaxAirPressure, i_OwnerName, i_OwnerPhoneNumber, i_VehicleStatus, i_CurrentEnergy, i_MaxEnergy);
        //            break;
        //        case eVehicleType.FuelCar:
        //            newVehicle = new FuelCar(i_ModelName, i_LicensePlate, i_WheelManufacturerName, i_CurrentAirPressure, i_MaxAirPressure, i_OwnerName, i_OwnerPhoneNumber, i_VehicleStatus, i_CurrentFuelAmountLiters, i_MaxFuelAmountLiters, i_FuelType);
        //            break;
        //        case eVehicleType.FuelMotorcycle:
        //            newVehicle = new FuelMotorcycle(i_ModelName, i_LicensePlate, i_WheelManufacturerName, i_CurrentAirPressure, i_MaxAirPressure, i_OwnerName, i_OwnerPhoneNumber, i_VehicleStatus, i_CurrentFuelAmountLiters, i_MaxFuelAmountLiters, i_FuelType);
        //            break;
        //        case eVehicleType.Truck:
        //            newVehicle = new Truck(i_ModelName, i_LicensePlate, i_WheelManufacturerName, i_CurrentAirPressure, i_MaxAirPressure, i_OwnerName, i_OwnerPhoneNumber, i_VehicleStatus, i_CurrentFuelAmountLiters, i_MaxFuelAmountLiters, i_FuelType);
        //            break;
        //    }

        //    return newVehicle;
        //}
    }
}
