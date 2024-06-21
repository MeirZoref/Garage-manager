using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class FuelMotorcycle : Motorcycle
    {
        private FuelTank m_FuelTankOfMotorcycle = new FuelTank();

        //public FuelMotorcycle(string i_ModelName, string i_LicenseNumber, eFuelType i_FuelType, float i_CurrentFuelAmountLiters, float i_MaxFuelAmountLiters,
        //               List<Wheel> i_WheelsList, eLicenseType i_LicenseType, int i_EngineVolume)
        //    : base(i_ModelName, i_LicenseNumber, i_CurrentFuelAmountLiters, i_WheelsList, i_LicenseType, i_EngineVolume)
        //{
        //    m_FuelTankOfMotorcycle = new FuelTank(i_FuelType, i_CurrentFuelAmountLiters, i_MaxFuelAmountLiters);
        //}

        //public void SetFuelMotorcycleValues(string i_ModelName, string i_LicenseNumber, List<Wheel> i_WheelsList,
        //                                    float i_CurrentFuelAmountLiters, float i_MaxFuelAmountLiters,
        //                                    eLicenseType i_LicenseType, int i_EngineVolume, eFuelType i_FuelType)
        //{
        //    try
        //    {
        //        SetMotorcycleValues(i_ModelName, i_LicenseNumber, i_WheelsList, i_CurrentFuelAmountLiters, i_LicenseType, i_EngineVolume);
        //        m_FuelTankOfMotorcycle.SetFuelTankValues(i_FuelType, i_CurrentFuelAmountLiters, i_MaxFuelAmountLiters);
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
                    m_FuelTankOfMotorcycle.SetFuelTankProperty(i_PropertyName, i_PropertyValue);
                    IsElectric = false;

                    if (i_PropertyName == "Fuel type")
                    // Will get here only if i_PropertyName == "Fuel type" and the Fuel type provided is valid.
                    // if fuel type is not valid - SetFuelTankProperty that gets "Fuel type" will throw an exception
                    {
                        VehicleFuelType = m_FuelTankOfMotorcycle.FuelType;
                    }

                    if (i_PropertyName == "Current fuel amount in liters")
                    {
                        CurrentEnergyLevelInPercentage = m_FuelTankOfMotorcycle.CurrentFuelAmountLiters / m_FuelTankOfMotorcycle.MaxFuelAmountLiters;
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

        //public override void FillEnergy(float i_FuelToAddLiters, eFuelType? i_FuelType)
        //{
        //    try
        //    {
        //        m_FuelTankOfMotorcycle.Refuel(i_FuelToAddLiters, i_FuelType);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    //catch (ValueOutOfRangeException)
        //    //{
        //    //    throw new ValueOutOfRangeException(0, m_FuelTankOfMotorcycle.MaxFuelAmountLiters - m_FuelTankOfMotorcycle.CurrentFuelAmountLiters);
        //    //}
        //}

        //private void Refuel(float i_FuelToAddLiters, eFuelType i_FuelType)
        //{
        //    m_FuelTankOfMotorcycle.Refuel(i_FuelToAddLiters, i_FuelType);
        //}

        public FuelTank FuelTankOfMotorcycle
        {
            get
            {
                if (m_FuelTankOfMotorcycle != null)
                {
                    return m_FuelTankOfMotorcycle;
                }
                else
                {
                    throw new NullReferenceException("Fuel tank of motorcycle is not valid");
                }
            }
        }

        public override List<KeyValuePair<string, string>> GetListOfPropertiesAndPossibleValues()
        {
            List<KeyValuePair<string, string>> propertiesAndValues = base.GetListOfPropertiesAndPossibleValues();
            propertiesAndValues.AddRange(m_FuelTankOfMotorcycle.GetListOfPropertiesAndPossibleValues());

            return propertiesAndValues;
        }

        //public override List<string> GetListOfPropertiesAndPossibleValues()
        //{
        //    List<string> properties = base.GetListOfPropertiesAndPossibleValues();
        //    properties.AddRange(m_FuelTankOfMotorcycle.GetListOfPropertiesAndPossibleValues());
        //    return properties;
        //}

        public override string ToString() //TODO: take out energy precentage that came from vehicle?
        {
            StringBuilder motorcycleDetails = new StringBuilder();
            motorcycleDetails.Append(base.ToString());
            motorcycleDetails.Append(m_FuelTankOfMotorcycle.ToString());
            
            return motorcycleDetails.ToString();
        }
    }
}
