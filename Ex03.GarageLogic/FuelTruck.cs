using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class FuelTruck : Truck
    {
        private FuelTank m_FuelTankOfTruck = new FuelTank();

        //public FuelTruck(string i_ModelName, string i_LicenseNumber, eFuelType i_FuelType, float i_CurrentFuelAmountLiters, float i_MaxFuelAmountLiters,
        //                           List<Wheel> i_WheelsList, bool i_IsCarryingDangerousMaterials, float i_CargoVolume)
        //    : base(i_ModelName, i_LicenseNumber, i_CurrentFuelAmountLiters, i_WheelsList, i_IsCarryingDangerousMaterials, i_CargoVolume)
        //{
        //    m_FuelTankOfTruck = new FuelTank(i_FuelType, i_CurrentFuelAmountLiters, i_MaxFuelAmountLiters);
        //}

        //public void SetFuelTruckValues(string i_ModelName, string i_LicenseNumber, List<Wheel> i_WheelsList,
        //                               float i_CurrentFuelAmountLiters, float i_MaxFuelAmountLiters,
        //                               bool i_IsCarryingDangerousMaterials, float i_CargoVolume, eFuelType i_FuelType)
        //{
        //    try
        //    {
        //        SetTruckValues(i_ModelName, i_LicenseNumber, i_WheelsList, i_CurrentFuelAmountLiters, i_IsCarryingDangerousMaterials, i_CargoVolume);
        //        FuelTankOfTruck.SetFuelTankValues(i_FuelType, i_CurrentFuelAmountLiters, i_MaxFuelAmountLiters);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public override void SetProperty(string i_PropertyName, string i_PropertyValue)
        {
            try
            {
                if (i_PropertyName == "Fuel type" || i_PropertyName == "Max fuel amount in liters" || i_PropertyName == "Current fuel amount in liters")
                {
                    m_FuelTankOfTruck.SetFuelTankProperty(i_PropertyName, i_PropertyValue);
                    IsElectric = false;

                    if (i_PropertyName == "Fuel type")
                    // Will get here only if i_PropertyName == "Fuel type" and the Fuel type provided is valid.
                    // if fuel type is not valid - SetFuelTankProperty that gets "Fuel type" will throw an exception
                    {
                        VehicleFuelType = m_FuelTankOfTruck.FuelType;
                    }

                    if (i_PropertyName == "Current fuel amount in liters")
                    {
                        CurrentEnergyLevelInPercentage = m_FuelTankOfTruck.CurrentFuelAmountLiters / m_FuelTankOfTruck.MaxFuelAmountLiters;
                    }
                }
                else
                {
                    base.SetProperty(i_PropertyName, i_PropertyValue);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override List<KeyValuePair<string, string>> GetListOfPropertiesAndPossibleValues()
        {
            List<KeyValuePair<string, string>> propertiesAndValues = base.GetListOfPropertiesAndPossibleValues();
            propertiesAndValues.AddRange(m_FuelTankOfTruck.GetListOfPropertiesAndPossibleValues());

            return propertiesAndValues;
        }

        //public override List<string> GetListOfPropertiesAndPossibleValues()
        //{
        //    List<string> properties = base.GetListOfPropertiesAndPossibleValues();
        //    properties.AddRange(m_FuelTankOfTruck.GetListOfPropertiesAndPossibleValues());
        //    return properties;
        //}

        public override bool TryFillEnergy(float i_FuelToAddLiters, eFuelType? i_FuelType)
        {
            bool isFuelAdded = false;
            try
            {
                if (m_FuelTankOfTruck.TryRefuel(i_FuelToAddLiters, i_FuelType))
                {
                    isFuelAdded = true;
                    CurrentEnergyLevelInPercentage = m_FuelTankOfTruck.CurrentFuelAmountLiters / m_FuelTankOfTruck.MaxFuelAmountLiters;
                }

                return isFuelAdded;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public override void FillEnergy(float i_FuelToAddLiters, eFuelType? i_FuelType)
        //{
        //    try
        //    {
        //        m_FuelTankOfTruck.Refuel(i_FuelToAddLiters, m_FuelTankOfTruck.FuelType);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public void Refuel(float i_FuelToAddLiters, eFuelType i_FuelType)
        //{
        //    m_FuelTankOfTruck.Refuel(i_FuelToAddLiters, i_FuelType);
        //}

        public FuelTank FuelTankOfTruck
        {
            get
            {
                return m_FuelTankOfTruck;
            }
            set
            {
                if (value != null)
                {
                    m_FuelTankOfTruck = value;
                }
                else
                {
                    throw new ArgumentNullException("Fuel tank of truck is not valid");
                }
            }
        }

        public override string ToString() //TODO: 
        {
            StringBuilder truckDetails = new StringBuilder();
            truckDetails.Append(base.ToString());
            truckDetails.Append(m_FuelTankOfTruck.ToString());

            return truckDetails.ToString();
        }
    }
}
