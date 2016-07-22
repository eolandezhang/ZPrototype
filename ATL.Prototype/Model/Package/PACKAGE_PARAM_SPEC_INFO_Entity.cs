
namespace Model.Package
{
    public class PACKAGE_PARAM_SPEC_INFO_Entity
    {
        public decimal TOTAL { get; set; }
        public string PACKAGE_NO { get; set; }//
        public string GROUP_NO { get; set; }//
        public string VERSION_NO { get; set; }//
        public string FACTORY_ID { get; set; }//
        public string PARAMETER_ID { get; set; }//
        public string SPEC_TYPE { get; set; }//FAI:首件;PMI:过程;OI:出货
        public string PARAM_UNIT { get; set; }//
        public string TARGET { get; set; }//目标值
        public string USL { get; set; }//上限值
        public string LSL { get; set; }//下限值
        public string UPDATE_USER { get; set; }//
        public string UPDATE_DATE { get; set; }//

        //关联
        public string PARAM_NAME { get; set; }
        public string PARAM_DESC { get; set; }
        public string PARAM_TYPE_ID { get; set; }
        public string PROCESS_ID { get; set; }     
        public string SAMPLING_FREQUENCY { get; set; }
        public string CONTROL_METHOD { get; set; }

        public decimal PARAM_ORDER_NO { get; set; }
        public decimal DISP_ORDER_IN_SC { get; set; }

        //辅助
        public string GROUPS { get; set; }


    }
}
