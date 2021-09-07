using System;
using System.Runtime.Serialization;

namespace McAlister.Study.PersistenceTests.Definitions
{
    [DataContract]
    public class APIResponse
    {
        [DataMember]
        public int Status { get; set; } = 200;
        [DataMember]
        public Boolean Successful { get; set; }
        [DataMember]
        public Object Payload { get; set; }
        [DataMember]
        public String Message { get; set; }
    }
}
