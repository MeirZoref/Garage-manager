using System;
using System.Text;
using System.Collections.Generic;


namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        private ElectricBattery m_ElectricBatteryOfCar;

        //public ElectricCar(string i_ModelName, string i_LicenseNumber, float i_CurrentBatteryEnergyLevel, float i_MaxBatteryEnergyLevel, List<Wheel> i_WheelsList,
        //    eColor i_Color, eNumOfDoors i_NumOfDoors)
        //    :base(i_ModelName, i_LicenseNumber, i_CurrentBatteryEnergyLevel, i_WheelsList,i_Color, i_NumOfDoors)
        //{
        //    m_ElectricBatteryOfCar = new ElectricBattery(i_CurrentBatteryEnergyLevel, i_MaxBatteryEnergyLevel);
        //}

        public void SetElectricCarValues(string i_ModelName, string i_LicenseNumber, List<Wheel> i_WheelsList,
                                         float i_CurrentBatteryEnergyLevel, float i_MaxBatteryEnergyLevel,
                                         eColor i_Color, eNumOfDoors i_NumOfDoors)
        {
            try
            {
                SetCarValues(i_ModelName, i_LicenseNumber, i_WheelsList, i_CurrentBatteryEnergyLevel, i_Color, i_NumOfDoors);
                m_ElectricBatteryOfCar.SetBatteryValues(i_CurrentBatteryEnergyLevel, i_MaxBatteryEnergyLevel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override List<string> GetListOfProperties()
        {
            List<string> properties = base.GetListOfProperties();
            properties.AddRange(m_ElectricBatteryOfCar.GetListOfProperties());
            return properties;
        }

        public void ChargeBattery(float i_EnergyToAdd)
        {
            m_ElectricBatteryOfCar.ChargeBattery(i_EnergyToAdd);
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

        public override void FillEnergy(float i_ElectricityAmountInMinutes, eFuelType? i_FuelType)
        {
            try
            {
                if (i_FuelType != null)
                {
                    throw new ArgumentException("Electric car does not run on fuel");
                }

                m_ElectricBatteryOfCar.ChargeBattery(i_ElectricityAmountInMinutes);
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
