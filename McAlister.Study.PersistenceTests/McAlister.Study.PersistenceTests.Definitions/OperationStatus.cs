using System;
using System.Runtime.Serialization;

namespace McAlister.Study.PersistenceTests.Definitions
{
    [DataContract]
    public class OperationStatus
    {
        [DataMember]
        public bool Status { get; set; }
        [DataMember]
        public int RecordsAffected { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public Object OperationID { get; set; }
        [DataMember]
        public string ExceptionMessage { get; set; }

        public static OperationStatus CreateFromException(string message, Exception ex)
        {
            OperationStatus opStatus = new OperationStatus
            {
                Status = false,
                Message = message,
                OperationID = null
            };

            if (ex != null)
            {
                opStatus.ExceptionMessage = ex.Message;
            }

            return opStatus;
        }
    }
}
