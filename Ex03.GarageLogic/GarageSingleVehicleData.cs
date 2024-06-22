namespace Ex03.GarageLogic
{
    public class GarageSingleVehicleData
    {
        private Vehicle m_Vehicle;
        private eVehicleStatus m_VehicleStatus;

        public GarageSingleVehicleData(Vehicle vehicle)
        {
            m_Vehicle = vehicle;
        }

        public Vehicle Vehicle
        {
            get
            {
                return m_Vehicle;
            }
        }

        public string OwnerName { get; set;}

        public string OwnerPhoneNumber { get; set;}

        public eVehicleStatus VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }

            set
            {
                m_VehicleStatus = value;
            }
        }
        
    }
}
