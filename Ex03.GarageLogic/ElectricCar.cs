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


        //public ElectricCar(string i_ModelName, string i_LicenseNumber, float i_CurrentBatteryEnergyLevel, float i_MaxBatteryEnergyLevel, List<Wheel> i_WheelsList,
        //    eColor i_Color, eNumOfDoors i_NumOfDoors)
        //    :base(i_ModelName, i_LicenseNumber, i_CurrentBatteryEnergyLevel, i_WheelsList,i_Color, i_NumOfDoors)
        //{
        //    m_ElectricBatteryOfCar = new ElectricBattery(i_CurrentBatteryEnergyLevel, i_MaxBatteryEnergyLevel);
        //}

        //public void SetElectricCarValues(string i_ModelName, string i_LicenseNumber, List<Wheel> i_WheelsList,
        //                                 float i_CurrentBatteryEnergyLevel, float i_MaxBatteryEnergyLevel,
        //                                 eColor i_Color, eNumOfDoors i_NumOfDoors)
        //{
        //    try
        //    {
        //        SetCarValues(i_ModelName, i_LicenseNumber, i_WheelsList, i_CurrentBatteryEnergyLevel, i_Color, i_NumOfDoors);
        //        m_ElectricBatteryOfCar.SetBatteryValues(i_CurrentBatteryEnergyLevel, i_MaxBatteryEnergyLevel);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public ElectricCar()
        {
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
                if (i_PropertyName == "Max battery energy level" || i_PropertyName == "Current battery energy level")
                {
                    m_ElectricBatteryOfCar.SetBatteryProperty(i_PropertyName, i_PropertyValue);
                    IsElectric = true;
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


        //public override List<string> GetListOfPropertiesAndPossibleValues()
        //{
        //    List<string> properties = base.GetListOfPropertiesAndPossibleValues();
        //    properties.AddRange(m_ElectricBatteryOfCar.GetListOfPropertiesAndPossibleValues());
        //    return properties;
        //}

        public void ChargeBattery(float i_EnergyToAdd)
        {
            m_ElectricBatteryOfCar.TryChargeBattery(i_EnergyToAdd);
        }

        public ElectricBattery ElectricBatteryOfCar
        {
            get
            {
                return m_ElectricBatteryOfCar;
            }
            set
            {
                if (value != null)
                {
                    m_ElectricBatteryOfCar = value;
                }
                else
                {
                    throw new ArgumentNullException("Electric battery of car is not valid");
                }
            }

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

        //public float CurrentEnergy
        //{
        //    get
        //    {
        //        return m_ElectricBatteryOfCar.CurrentEnergy;
        //    }
        //}

        //public float MaxEnergy
        //{
        //    get
        //    {
        //        return m_ElectricBattery.MaxEnergy;
        //    }
        //}

        //public int NumOfWheels
        //{
        //    get
        //    {
        //        return NumOfWheels;
        //    }
        //}

        //public eColor Color
        //{
        //    get
        //    {
        //        return Color;
        //    }
        //}

        //public eNumOfDoors NumOfDoors
        //{
        //    get
        //    {
        //        return NumOfDoors;
        //    }
        //}

        public override string ToString()
        //TODO: take out energy precentage that came from vehicle, because of duplication with ElectricBattery -
        //maybe add a method to ElectricBattery that will return the energy precentage
        {
            StringBuilder ElectricCarDetails = new StringBuilder();
            ElectricCarDetails.Append(base.ToString());
            ElectricCarDetails.AppendLine(m_ElectricBatteryOfCar.ToString());

            return ElectricCarDetails.ToString();
        }
    }
}
