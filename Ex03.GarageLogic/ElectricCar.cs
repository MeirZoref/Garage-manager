using System;
using System.Text;
using System.Collections.Generic;


namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        private ElectricBattery m_ElectricBatteryOfCar = new ElectricBattery();
        private const float k_ElectricCarMaxBatteryHours = 3.5f;
        private const int k_NumOfWheels = 5;
        private const float k_ElectricCarMaxAirPressure = 31;

        public ElectricCar()
        {
            IsElectric = true;
            m_ElectricBatteryOfCar.MaxBatteryEnergyLevel = k_ElectricCarMaxBatteryHours;
            
            for (int i = 0; i < k_NumOfWheels; i++)
            {
                WheelsList.Add(new Wheel());
            }

            foreach (Wheel wheel in WheelsList)
            {
                wheel.MaxAirPressure = k_ElectricCarMaxAirPressure;
            }
        }
       
        public override void SetProperty(string i_PropertyName, string i_PropertyValue)
        {
            try
            {
                if (i_PropertyName == "Current battery energy level")
                {
                    m_ElectricBatteryOfCar.SetBatteryProperty(i_PropertyName, i_PropertyValue);
                    CurrentEnergyLevelInPercentage = m_ElectricBatteryOfCar.CurrentBatteryEnergyLevel / m_ElectricBatteryOfCar.MaxBatteryEnergyLevel;
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
            propertiesAndValues.AddRange(m_ElectricBatteryOfCar.GetListOfPropertiesAndPossibleValues());

            return propertiesAndValues;
        }

        public override bool TryFillEnergy(float i_ElectricityAmountInHours, eFuelType? i_FuelType)
        {
            try
            {
                if (i_FuelType != null)
                {
                    throw new ArgumentException("Electric car does not run on fuel");
                }

                bool isEnergyAdded = m_ElectricBatteryOfCar.TryChargeBattery(i_ElectricityAmountInHours);
                if (isEnergyAdded)
                {
                    CurrentEnergyLevelInPercentage = m_ElectricBatteryOfCar.CurrentBatteryEnergyLevel / m_ElectricBatteryOfCar.MaxBatteryEnergyLevel;
                }

                return isEnergyAdded;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override string ToString()
        {
            StringBuilder ElectricCarDetails = new StringBuilder();
            ElectricCarDetails.Append(base.ToString());
            ElectricCarDetails.AppendLine(m_ElectricBatteryOfCar.ToString());

            return ElectricCarDetails.ToString();
        }
    }
}
