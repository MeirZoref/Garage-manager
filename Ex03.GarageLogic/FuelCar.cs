using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class FuelCar : Car
    {
        private FuelTank m_FuelTankOfCar = new FuelTank();
        private const int k_NumOfWheels = 5;
        private const float k_FuelCarMaxAirPressure = 31;
        private const eFuelType k_FuelCarFuelType = eFuelType.Octan95;
        private const float k_FuelCarMaxFuelAmountInLiters = 45;

        public FuelCar()
        {
            IsElectric = false;
            VehicleFuelType = k_FuelCarFuelType;
            m_FuelTankOfCar.FuelType = k_FuelCarFuelType;
            m_FuelTankOfCar.MaxFuelAmountLiters = k_FuelCarMaxFuelAmountInLiters;

            for (int i = 0; i < k_NumOfWheels; i++)
            {
                WheelsList.Add(new Wheel());
            }

            foreach (Wheel wheel in WheelsList)
            {
                wheel.MaxAirPressure = k_FuelCarMaxAirPressure;
            }
        }   

        public override void SetProperty(string i_PropertyName, string i_PropertyValue)
        {
            try
            {
                if (i_PropertyName == "Current fuel amount in liters")
                {
                    m_FuelTankOfCar.SetCurrentFuelAmountInLiters(i_PropertyName, i_PropertyValue);
                    CurrentEnergyLevelInPercentage = m_FuelTankOfCar.CurrentFuelAmountLiters / m_FuelTankOfCar.MaxFuelAmountLiters;
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
            propertiesAndValues.AddRange(m_FuelTankOfCar.GetListOfPropertiesAndPossibleValues());

            return propertiesAndValues;
        }

        public override bool TryFillEnergy(float i_FuelToAddLiters, eFuelType? i_FuelType)
        {
            bool isFuelAdded = false;
            try
            {
                if (m_FuelTankOfCar.TryRefuel(i_FuelToAddLiters, i_FuelType))
                {
                    isFuelAdded = true;
                    CurrentEnergyLevelInPercentage = m_FuelTankOfCar.CurrentFuelAmountLiters / m_FuelTankOfCar.MaxFuelAmountLiters;
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
            StringBuilder carDetails = new StringBuilder();
            carDetails.Append(base.ToString());
            carDetails.Append(m_FuelTankOfCar.ToString());

            return carDetails.ToString();
        }
    }
}
