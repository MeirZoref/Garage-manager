using System;
using System.Collections.Generic;

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
        
        public static string GetListOfCurrentSupportedVehicles()
        {
            List<string> currentlySupportedVehicles = new List<string>
            {
                "Supported vehicle types are:",
                "1. Fuel Car with 5 wheels, max air pressure of 31, Tank capacity of 45 liters, Fuel type: Octan 95.",
                "2. Electric Car with 5 wheels, max air pressure of 31, max battery capacity of 3.5 hours.",
                "3. Fuel Motorcycle with 2 wheels, max air pressure of 33, Tank capacity of 5.5 liters, Fuel type: Octan 98.",
                "4. Electric Motorcycle with 2 wheels, max air pressure of 33, max battery capacity of 2.5 hours.",
                "5. Fuel Truck with 12 wheels, max air pressure of 28, Tank capacity of 120 liters, Fuel type: Soler."
            };
            
            return string.Join(Environment.NewLine, currentlySupportedVehicles);
        }
    }
}
