
namespace Model.Package
{
    public class PACKAGE_PARAM_SETTING_Entity
    {
        public decimal TOTAL { get; set; }
        public string PACKAGE_NO { get; set; }//
        public string GROUP_NO { get; set; }//
        public string FACTORY_ID { get; set; }//
        public string VERSION_NO { get; set; }//
        public string PARAMETER_ID { get; set; }//
        public string PARAM_TYPE_ID { get; set; }//参数类型
        public string PROCESS_ID { get; set; }//工序编号
        public string PROC_TASK_ID { get; set; }//工序作业编号        
        public decimal DISP_ORDER_IN_SC { get; set; }//在规格牌中的顺序
        public string PARAM_IO { get; set; }//1:输入参数;2:输出参数
        public string IS_GROUP_PARAM { get; set; }//分组参数？
        public string IS_FIRST_CHECK_PARAM { get; set; }//首件参数？
        public string IS_PROC_MON_PARAM { get; set; }//过程参数？
        public string IS_OUTPUT_PARAM { get; set; }//出货参数？
        public string PARAM_UNIT { get; set; }//参数单位
        public string PARAM_DATATYPE { get; set; }//参数数据类型：STRING/NUMBER
        public string TARGET { get; set; }//目标值
        public string USL { get; set; }//上限值
        public string LSL { get; set; }//下限值
        public string ILLUSTRATION_ID { get; set; }//插图ID
        public string UPDATE_USER { get; set; }//
        public string UPDATE_DATE { get; set; }//
        public string SAMPLING_FREQUENCY { get; set; }//抽样频率
        public string CONTROL_METHOD { get; set; }//控制方法
        public string IS_SC_PARAM { get; set; }


        //关联
        public string PARAM_NAME { get; set; }
        public string PARAM_DESC { get; set; }
        public decimal PARAM_ORDER_NO { get; set; }

        public string PRODUCT_TYPE_ID { get; set; }
        public string PRODUCT_PROC_TYPE_ID { get; set; }

        public string SPEC_TYPE { get; set; }

        //批量
        public string GROUPS { get; set; }
        public string PARAMS { get; set; }

        //参数类型
        public string TYPE { get; set; }

        //辅助
        public string EQUIPMENT_CLASS_ID { get; set; }
        public string EQUIPMENT_ID { get; set; }
       
    }
}
