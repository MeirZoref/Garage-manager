using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        //private const float k_MaxBatteryTime = 1.8f;
        //private const eFuelType k_FuelType = eFuelType.Electric;
        private ElectricBattery m_ElectricBatteryOfMotorcycle;

        //public ElectricMotorcycle(string i_ModelName, string i_LicenseNumber, float i_CurrentBatteryEnergyLevel, float i_MaxBatteryEnergyLevel,
        //    List<Wheel> i_WheelsList, eLicenseType i_LicenseType, int i_EngineVolume)
        //    : base(i_ModelName, i_LicenseNumber, i_CurrentBatteryEnergyLevel, i_WheelsList, i_LicenseType, i_EngineVolume)
        //{
        //    m_ElectricBatteryOfMotorcycle = new ElectricBattery(i_CurrentBatteryEnergyLevel, i_MaxBatteryEnergyLevel);
        //}

        //public void SetElectricMotorcycleValues(string i_ModelName, string i_LicenseNumber, List<Wheel> i_WheelsList,
        //                                        float i_CurrentBatteryEnergyLevel, float i_MaxBatteryEnergyLevel,
        //                                        eLicenseType i_LicenseType, int i_EngineVolume)
        //{
        //    try
        //    {
        //        SetMotorcycleValues(i_ModelName, i_LicenseNumber, i_WheelsList, i_CurrentBatteryEnergyLevel, i_LicenseType, i_EngineVolume);
        //        m_ElectricBatteryOfMotorcycle.SetBatteryValues(i_CurrentBatteryEnergyLevel, i_MaxBatteryEnergyLevel);
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
                if (i_PropertyName == "Max battery energy level" || i_PropertyName == "Current battery energy level")
                {
                    m_ElectricBatteryOfMotorcycle.SetBatteryProperty(i_PropertyName, i_PropertyValue);
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

        public override List<string> GetListOfPropertiesAndPossibleValues()
        {
            List<string> properties = base.GetListOfPropertiesAndPossibleValues();
            properties.AddRange(m_ElectricBatteryOfMotorcycle.GetListOfPropertiesAndPossibleValues());
            return properties;
        }   
        
        public ElectricBattery ElectricBatteryOfMotorcycle
        {
            get
            {
                return m_ElectricBatteryOfMotorcycle;
            }
            set
            {
                if (value != null)
                {
                    m_ElectricBatteryOfMotorcycle = value;
                }
                else
                {
                    throw new ArgumentNullException("Electric battery of motorcycle is not valid");
                }
            }

        }

        public override void FillEnergy(float i_ElectricityAmountInMinutes, eFuelType? i_FuelType)
        {
            try
            {
                if (i_FuelType != null)
                {
                    throw new ArgumentException("Electric motorcycle does not run on fuel");
                }

                m_ElectricBatteryOfMotorcycle.ChargeBattery(i_ElectricityAmountInMinutes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override string ToString() 
         //TODO: take out energy precentage that came from vehicle, because of duplication with ElectricBattery
         //maybe add a method to ElectricBattery that will return the energy precentage
        {
            StringBuilder electricMotorcycleDetails = new StringBuilder();
            electricMotorcycleDetails.Append(base.ToString());
            electricMotorcycleDetails.AppendLine(m_ElectricBatteryOfMotorcycle.ToString());
            return electricMotorcycleDetails.ToString();
        }
    }
}
