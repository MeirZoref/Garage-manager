using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class FuelTruck : Truck
    {
        private FuelTank m_FuelTankOfTruck;

        //public FuelTruck(string i_ModelName, string i_LicenseNumber, eFuelType i_FuelType, float i_CurrentFuelAmountLiters, float i_MaxFuelAmountLiters,
        //                           List<Wheel> i_WheelsList, bool i_IsCarryingDangerousMaterials, float i_CargoVolume)
        //    : base(i_ModelName, i_LicenseNumber, i_CurrentFuelAmountLiters, i_WheelsList, i_IsCarryingDangerousMaterials, i_CargoVolume)
        //{
        //    m_FuelTankOfTruck = new FuelTank(i_FuelType, i_CurrentFuelAmountLiters, i_MaxFuelAmountLiters);
        //}

        public void SetFuelTruckValues(string i_ModelName, string i_LicenseNumber, List<Wheel> i_WheelsList,
                                       float i_CurrentFuelAmountLiters, float i_MaxFuelAmountLiters,
                                       bool i_IsCarryingDangerousMaterials, float i_CargoVolume, eFuelType i_FuelType)
        {
            try
            {
                SetTruckValues(i_ModelName, i_LicenseNumber, i_WheelsList, i_CurrentFuelAmountLiters, i_IsCarryingDangerousMaterials, i_CargoVolume);
                FuelTankOfTruck.SetFuelTankValues(i_FuelType, i_CurrentFuelAmountLiters, i_MaxFuelAmountLiters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override List<string> GetListOfProperties()
        {
            List<string> properties = base.GetListOfProperties();
            properties.AddRange(m_FuelTankOfTruck.GetListOfProperties());
            return properties;
        }

        public override void FillEnergy(float i_FuelToAddLiters, eFuelType? i_FuelType)
        {
            try
            {
                m_FuelTankOfTruck.Refuel(i_FuelToAddLiters, m_FuelTankOfTruck.FuelType);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Refuel(float i_FuelToAddLiters, eFuelType i_FuelType)
        {
            m_FuelTankOfTruck.Refuel(i_FuelToAddLiters, i_FuelType);
        }

        public FuelTank FuelTankOfTruck
        {
            get
            {
                return m_FuelTankOfTruck;
            }
            set
            {
                if (value != null)
                {
                    m_FuelTankOfTruck = value;
                }
                else
                {
                    throw new ArgumentNullException("Fuel tank of truck is not valid");
                }
            }
        }

        public override string ToString() //TODO: 
        {
            StringBuilder truckDetails = new StringBuilder();
            truckDetails.Append(base.ToString());
            truckDetails.AppendFormat("Fuel type: {0}{1}", m_FuelTankOfTruck.FuelType, Environment.NewLine);
            truckDetails.AppendFormat("Current fuel amount: {0}{1}", m_FuelTankOfTruck.CurrentFuelAmountLiters, Environment.NewLine);
            truckDetails.AppendFormat("Max fuel amount: {0}{1}", m_FuelTankOfTruck.MaxFuelAmountLiters, Environment.NewLine);
            return truckDetails.ToString();
        }
    }
}
