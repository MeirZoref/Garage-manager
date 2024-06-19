using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageSingleVehicleData
    {
        Vehicle m_Vehicle;
        //string m_OwnerName;
        //string m_OwnerPhoneNumber;
        eVehicleStatus m_VehicleStatus;
        eVehicleType m_VehicleType;

        public GarageSingleVehicleData(Vehicle vehicle)
        {
            m_Vehicle = vehicle;
        }

       //public GarageVehicleData(Vehicle vehicle, string ownerName, string ownerPhoneNumber, eVehicleStatus vehicleStatus)
       // {
       //     try
       //     {
       //         m_VehicleType = GetVehicleType(vehicle); //if VehicleType is not supported, the method will throw an exception
       //         m_Vehicle = vehicle;
       //         m_OwnerName = ownerName;
       //         m_OwnerPhoneNumber = ownerPhoneNumber;
       //         m_VehicleStatus = vehicleStatus;


       //         //if (vehicle == null)
       //         //{
       //         //    throw new ArgumentNullException("Vehicle is null");
       //         //}
       //         //if (IsVehicleTypeSupported(vehicle, out m_VehicleType))
       //         //{
       //         //    throw new ArgumentException("Vehicle type is not supported");
       //         //}
       //     }
       //     catch (Exception)
       //     {
       //         throw; // new ArgumentException("Vehicle type is not supported", ex);
       //     }

       // }

        

        //private bool IsVehicleTypeSupported(Vehicle vehicle, out eVehicleType m_VehicleType)
        //{
        //    try
        //    {
        //        eVehicleType eVehicleType = GetVehicleType(vehicle);
        //        m_VehicleType = eVehicleType;
        //    }

        //}

        public Vehicle Vehicle
        {
            get
            {
                return m_Vehicle;
            }
        }

        public string OwnerName { get; set;}
        //{
        //    get
        //    {
        //        return m_OwnerName;
        //    }
        //}

        public string OwnerPhoneNumber { get; set;}
        //{
        //    get
        //    {
        //        return m_OwnerPhoneNumber;
        //    }
        //}

        public eVehicleStatus VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }

            set
            {
                m_VehicleStatus = value;
            }
        }

        public eVehicleType VehicleType
        {
            get
            {
                return m_VehicleType;
            }
        }

        public eVehicleType GetVehicleType(Vehicle vehicle)
        {
            eVehicleType? vehicleType = null;

            switch (vehicle)
            {
                case FuelCar fuelCar:
                    {
                        vehicleType = eVehicleType.FuelCar;
                        break;
                    }
                case FuelMotorcycle fuelMotorcycle:
                    {
                        vehicleType = eVehicleType.FuelMotorcycle;
                        break;
                    }
                case FuelTruck fuelTruck:
                    {
                        vehicleType = eVehicleType.FuelTruck;
                        break;
                    }
                case ElectricCar electricCar:
                    {
                        vehicleType = eVehicleType.ElectricCar;
                        break;
                    }
                case ElectricMotorcycle electricMotorcycle:
                    {
                        vehicleType = eVehicleType.ElectricMotorcycle;
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("Vehicle type is not supported");
                    }
            }
            return (eVehicleType)vehicleType;

        }
        
        public override string ToString()
        {
            StringBuilder generalVehicleData = new StringBuilder();
            string specificVehicleData = string.Empty;
            generalVehicleData.AppendFormat("Owner name: {0}{1}", m_OwnerName, Environment.NewLine);
            generalVehicleData.AppendFormat("Owner phone number: {0}{1}", m_OwnerPhoneNumber, Environment.NewLine);
            generalVehicleData.AppendFormat("Vehicle status: {0}{1}", m_VehicleStatus, Environment.NewLine);
            
            switch (m_Vehicle)
            {
                case FuelCar fuelCar:
                {
                    specificVehicleData = fuelCar.ToString();
                    break;
                }
                case FuelMotorcycle fuelMotorcycle:
                {
                    specificVehicleData = fuelMotorcycle.ToString();
                    break;
                }
                case FuelTruck fuelTruck:
                {
                    specificVehicleData = fuelTruck.ToString();
                    break;
                }
                case ElectricCar electricCar:
                {
                    specificVehicleData = electricCar.ToString();
                    break;
                }
                case ElectricMotorcycle electricMotorcycle:
                {
                    specificVehicleData = electricMotorcycle.ToString();
                    break;
                }
            }
            //vehicleData.AppendFormat("Vehicle details: {0}{1}", m_Vehicle.ToString(), Environment.NewLine);
            return $"{generalVehicleData.ToString()}{Environment.NewLine}{specificVehicleData}";
        }
    }
}


    //if (m_Vehicle is FuelCar)
    //{
    //    m_VehicleType = eVehicleType.FuelCar;
    //}
    //else if (m_Vehicle is FuelMotorcycle)
    //{
    //    m_VehicleType = eVehicleType.FuelMotorcycle;
    //}
    //else if (m_Vehicle is FuelTruck)
    //{
    //    m_VehicleType = eVehicleType.FuelTruck;
    //}
    //else if (m_Vehicle is ElectricCar)
    //{
    //    m_VehicleType = eVehicleType.ElectricCar;
    //}
    //else if (m_Vehicle is ElectricMotorcycle)
    //{
    //    m_VehicleType = eVehicleType.ElectricMotorcycle;
    //}
