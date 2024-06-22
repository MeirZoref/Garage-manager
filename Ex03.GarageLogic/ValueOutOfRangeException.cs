using System;
using System.Runtime.Serialization;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        public float MaxValue { get; set; }
        public float MinValue { get; set; }

        public ValueOutOfRangeException(string i_ValueName, float i_MinValue, float i_MaxValue)
                : base(string.Format("The value of - {0} - is out of range. The value should be between {1} and {2}.", i_ValueName, i_MinValue, i_MaxValue))
        {
            MinValue = i_MinValue;
            MaxValue = i_MaxValue;
        }

        public ValueOutOfRangeException(string i_ValueName, float i_MaxValue)
                : base(string.Format("The value of - {0} - is out of range. The value should be a positive number (greater than 0) and less than {1}.", i_ValueName, i_MaxValue))
        {
            MaxValue = i_MaxValue;
        }

    }
}