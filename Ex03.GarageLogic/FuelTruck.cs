using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class FuelTruck : Truck
    {
        private FuelTank m_FuelTankOfTruck = new FuelTank();
        private const int k_NumberOfWheels = 12;
        private const float k_FuelTruckMaxAirPressure = 28;
        private const eFuelType k_FuelTruckFuelType = eFuelType.Soler;
        private const float k_FuelTruckMaxFuelAmountInLiters = 120f;

        public FuelTruck()
        {
            IsElectric = false;
            VehicleFuelType = k_FuelTruckFuelType;
            m_FuelTankOfTruck.FuelType = k_FuelTruckFuelType;
            m_FuelTankOfTruck.MaxFuelAmountLiters = k_FuelTruckMaxFuelAmountInLiters;

            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                WheelsList.Add(new Wheel());
            }

            foreach (Wheel wheel in WheelsList)
            {
                wheel.MaxAirPressure = k_FuelTruckMaxAirPressure;
            }
        }

        public override void SetProperty(string i_PropertyName, string i_PropertyValue)
        {
            try
            {
                if (i_PropertyName == "Current fuel amount in liters")
                {
                    m_FuelTankOfTruck.SetCurrentFuelAmountInLiters(i_PropertyName, i_PropertyValue);
                    CurrentEnergyLevelInPercentage = m_FuelTankOfTruck.CurrentFuelAmountLiters / m_FuelTankOfTruck.MaxFuelAmountLiters;
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

        public override string ToString() 
        {
            StringBuilder truckDetails = new StringBuilder();
            truckDetails.Append(base.ToString());
            truckDetails.Append(m_FuelTankOfTruck.ToString());

            return truckDetails.ToString();
        }
    }
}
