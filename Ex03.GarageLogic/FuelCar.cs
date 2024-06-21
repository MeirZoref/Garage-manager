using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class FuelCar : Car
    {
        private FuelTank m_FuelTankOfCar = new FuelTank();

        //public FuelCar(string i_ModelName, string i_LicenseNumber, eFuelType i_FuelType, float i_CurrentFuelAmountLiters, float i_MaxFuelAmountLiters,
        //    List<Wheel> i_WheelsList, eColor i_Color, eNumOfDoors i_NumOfDoors)
        //    : base(i_ModelName, i_LicenseNumber, i_CurrentFuelAmountLiters, i_WheelsList, i_Color, i_NumOfDoors)
        //{
        //    m_FuelTankOfCar = new FuelTank(i_FuelType, i_CurrentFuelAmountLiters, i_MaxFuelAmountLiters);
        //}

        //public void SetFuelCarValues(string i_ModelName, string i_LicenseNumber, List<Wheel> i_WheelsList,
        //                               float i_CurrentFuelAmountLiters, float i_MaxFuelAmountLiters,
        //                               eColor i_Color, eNumOfDoors i_NumOfDoors, eFuelType i_FuelType)
        //{
        //    try
        //    {
        //        SetCarValues(i_ModelName, i_LicenseNumber, i_WheelsList, i_CurrentFuelAmountLiters, i_Color, i_NumOfDoors);
        //        FuelTankOfCar.SetFuelTankValues(i_FuelType, i_CurrentFuelAmountLiters, i_MaxFuelAmountLiters); 
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
                    m_FuelTankOfCar.SetFuelTankProperty(i_PropertyName, i_PropertyValue);
                    IsElectric = false;

                    if (i_PropertyName == "Fuel type")
                    // Will get here only if i_PropertyName == "Fuel type" and the Fuel type provided is valid.
                    // if fuel type is not valid - SetFuelTankProperty that gets "Fuel type" will throw an exception
                    {
                        VehicleFuelType = m_FuelTankOfCar.FuelType;
                    }

                    if (i_PropertyName == "Current fuel amount in liters")
                    {
                        CurrentEnergyLevelInPercentage = m_FuelTankOfCar.CurrentFuelAmountLiters / m_FuelTankOfCar.MaxFuelAmountLiters;
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
            propertiesAndValues.AddRange(m_FuelTankOfCar.GetListOfPropertiesAndPossibleValues());

            return propertiesAndValues;
        }

        //public override List<string> GetListOfPropertiesAndPossibleValues()
        //{
        //    List<string> properties = base.GetListOfPropertiesAndPossibleValues();
        //    properties.AddRange(m_FuelTankOfCar.GetListOfPropertiesAndPossibleValues());
        //    //properties.Add(m_FuelTankOfCar.GetListOfProperties());
        //    //properties.Add($"Supported fuel types are:{GetListOfSupportedFuelTypes()}");
        //    //properties.Add("Current fuel amount");
        //    //properties.Add("Max fuel amount");
        //    return properties;
        //}

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

        public FuelTank FuelTankOfCar
        {
            get
            {
                return m_FuelTankOfCar;
            }
            set
            {
                if (value != null)
                {
                    m_FuelTankOfCar = value;
                }
                else
                {
                    throw new NullReferenceException("Fuel tank of car is not valid");
                }
            }
        }

        //public void Refuel(float i_FuelToAddLiters, eFuelType i_FuelType)
        //{
        //    m_FuelTankOfCar.Refuel(i_FuelToAddLiters, i_FuelType);
        //}

        //public FuelTank FuelTankOfCar
        //{
        //    get
        //    {
        //        return m_FuelTankOfCar;
        //    }
        //}

        //public float CurrentFuelAmountLiters
        //{
        //    get
        //    {
        //        return m_FuelTank.CurrentFuelAmountLiters;
        //    }
        //}

        public override string ToString()
        {
            StringBuilder carDetails = new StringBuilder();
            carDetails.Append(base.ToString());
            carDetails.Append(m_FuelTankOfCar.ToString());

            return carDetails.ToString();
        }
    }
}
