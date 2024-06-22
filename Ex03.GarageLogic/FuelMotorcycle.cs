using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class FuelMotorcycle : Motorcycle
    {
        private FuelTank m_FuelTankOfMotorcycle = new FuelTank();
        private const int k_NumberOfWheels = 2;
        private const float k_FuelMotorcycleMaxAirPressure = 33;
        private const eFuelType k_FuelMotorcycleFuelType = eFuelType.Octan98;
        private const float k_FuelMotorcycleMaxFuelAmountInLiters = 5.5f;

        public FuelMotorcycle()
        {
            IsElectric = false;
            VehicleFuelType = k_FuelMotorcycleFuelType;
            m_FuelTankOfMotorcycle.FuelType = k_FuelMotorcycleFuelType;
            m_FuelTankOfMotorcycle.MaxFuelAmountLiters = k_FuelMotorcycleMaxFuelAmountInLiters;

            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                WheelsList.Add(new Wheel());
            }

            foreach (Wheel wheel in WheelsList)
            {
                wheel.MaxAirPressure = k_FuelMotorcycleMaxAirPressure;
            }
        }

        public override void SetProperty(string i_PropertyName, string i_PropertyValue)
        {
            try
            {
                if (i_PropertyName == "Current fuel amount in liters")
                {
                    m_FuelTankOfMotorcycle.SetCurrentFuelAmountInLiters(i_PropertyName, i_PropertyValue);
                    CurrentEnergyLevelInPercentage = m_FuelTankOfMotorcycle.CurrentFuelAmountLiters / m_FuelTankOfMotorcycle.MaxFuelAmountLiters;
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

        public override bool TryFillEnergy(float i_FuelToAddLiters, eFuelType? i_FuelType)
        {
            bool isFuelAdded = false;
            try
            {
                if (m_FuelTankOfMotorcycle.TryRefuel(i_FuelToAddLiters, i_FuelType))
                {
                    isFuelAdded = true;
                    CurrentEnergyLevelInPercentage = m_FuelTankOfMotorcycle.CurrentFuelAmountLiters / m_FuelTankOfMotorcycle.MaxFuelAmountLiters;
                }

                return isFuelAdded;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override List<KeyValuePair<string, string>> GetListOfPropertiesAndPossibleValues()
        {
            List<KeyValuePair<string, string>> propertiesAndValues = base.GetListOfPropertiesAndPossibleValues();
            propertiesAndValues.AddRange(m_FuelTankOfMotorcycle.GetListOfPropertiesAndPossibleValues());

            return propertiesAndValues;
        }

        public override string ToString()
        {
            StringBuilder motorcycleDetails = new StringBuilder();
            motorcycleDetails.Append(base.ToString());
            motorcycleDetails.Append(m_FuelTankOfMotorcycle.ToString());
            
            return motorcycleDetails.ToString();
        }
    }
}
