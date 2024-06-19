using System;
using System.Runtime.Serialization;

namespace Ex03.GarageLogic
{
    //[Serializable]
    public class ValueOutOfRangeException : Exception
    {
        private int v1;
        private float v2;

        public ValueOutOfRangeException()
        {
        }

        public ValueOutOfRangeException(string message) : base(message)
        {
        }

        public ValueOutOfRangeException(int v1, float v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public ValueOutOfRangeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ValueOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}