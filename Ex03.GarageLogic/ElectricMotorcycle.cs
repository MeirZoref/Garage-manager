using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        private ElectricBattery m_ElectricBatteryOfMotorcycle = new ElectricBattery();
        private const int k_NumOfWheels = 2;
        private const float k_ElectricMotorcycleMaxAirPressure = 33;
        private const float k_ElectricMotorcycleMaxBatteryHours = 2.5f;

        public ElectricMotorcycle()
        {
            IsElectric = true;
            m_ElectricBatteryOfMotorcycle.MaxBatteryEnergyLevel = k_ElectricMotorcycleMaxBatteryHours;

            for (int i = 0; i < k_NumOfWheels; i++)
            {
                WheelsList.Add(new Wheel());
            }

            foreach (Wheel wheel in WheelsList)
            {
                wheel.MaxAirPressure = k_ElectricMotorcycleMaxAirPressure;
            }
        }   

        public override void SetProperty(string i_PropertyName, string i_PropertyValue)
        {
            try
            {
                if (i_PropertyName == "Current battery energy level")
                {
                    m_ElectricBatteryOfMotorcycle.SetBatteryProperty(i_PropertyName, i_PropertyValue);
                    CurrentEnergyLevelInPercentage = m_ElectricBatteryOfMotorcycle.CurrentBatteryEnergyLevel / m_ElectricBatteryOfMotorcycle.MaxBatteryEnergyLevel;
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
            propertiesAndValues.AddRange(m_ElectricBatteryOfMotorcycle.GetListOfPropertiesAndPossibleValues());

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

                bool isEnergyAdded = m_ElectricBatteryOfMotorcycle.TryChargeBattery(i_ElectricityAmountInHours);
                if (isEnergyAdded)
                {
                    CurrentEnergyLevelInPercentage = m_ElectricBatteryOfMotorcycle.CurrentBatteryEnergyLevel / m_ElectricBatteryOfMotorcycle.MaxBatteryEnergyLevel;
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
            StringBuilder electricMotorcycleDetails = new StringBuilder();
            electricMotorcycleDetails.Append(base.ToString());
            electricMotorcycleDetails.AppendLine(m_ElectricBatteryOfMotorcycle.ToString());

            return electricMotorcycleDetails.ToString();
        }
    }
}
