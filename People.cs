using System;
using System.Collections.Generic;
using System.Runtime.Serialization;



namespace JSONWebServiceCall
{

    //Creating a Data Contract which matches the JSON...JSON will be deserilaized into the following .NET Object
    [DataContract]
    public class People
    {
        [DataMember]
        public string name
        {
            get;
            set;
        }
        [DataMember]
        public string gender
        {
            get;
            set;
        }
        [DataMember]
        public int age
        {
            get;
            set;
        }
        [DataMember]
        public List<Pets> pets { get; set; }


    }
}
