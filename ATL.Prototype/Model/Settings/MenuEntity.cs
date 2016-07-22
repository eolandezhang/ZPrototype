using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Settings
{
    public class MenuEntity
    {
        public decimal ID { get; set; }
        public decimal PID { get; set; }
        public string NAME { get; set; }
        public string TITLE { get; set; }       
        public string URL { get; set; }
        public string OPEN { get; set; }
        public string TARGET { get; set; }
        public string ICON { get; set; }
        public decimal SORT { get; set; }        

        public string PNAME { get; set; }
    }
}
//MENU
//(
//  ID      NUMBER,
//  PID     NUMBER,
//  NAME    NVARCHAR2(30),
//  TITLE   NVARCHAR2(30),
//  URL     NVARCHAR2(100),
//  OPEN    NVARCHAR2(10),
//  TARGET  NVARCHAR2(10),
//  ICON    NVARCHAR2(100),
//  SORT    NUMBER
//)