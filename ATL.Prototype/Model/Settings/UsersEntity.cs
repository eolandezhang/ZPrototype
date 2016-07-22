using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Settings
{
    public class UsersEntity
    {
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string DEPARTMENT { get; set; }
        public string TITLE { get; set; }
        public string DESCRIPTION { get; set; }
        public string MAIL { get; set; }
        public string CREATEDBY { get; set; }
        public string CREATEDAT { get; set; }
        public string MODIFIEDBY { get; set; }
        public string MODIFIEDAT { get; set; }
        public string CNNAME { get; set; }
        public string FACTORY_ID { get; set; }

        public decimal TOTAL { get; set; }
    }
}
