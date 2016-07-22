
namespace Model.Package
{
    public class PACKAGE_BASE_INFO_Entity
    {
        public decimal TOTAL { get; set; }
        public string PACKAGE_NO { get; set; }
        public string FACTORY_ID { get; set; }
        public string VERSION_NO { get; set; }
        /// <summary>
        /// PKG的分组数
        /// </summary>
        public decimal GROUPS { get; set; }
        /// <summary>
        /// 每组编号清单
        /// </summary>
        public string GROUP_NO_LIST { get; set; }
        /// <summary>
        /// PKG分组目的
        /// </summary>
        public string GROUPS_PURPOSE { get; set; }
        public string PRODUCT_TYPE_ID { get; set; }
        public string PRODUCT_PROC_TYPE_ID { get; set; }
        /// <summary>
        /// PKG种类，参考
        /// </summary>
        public string PACKAGE_TYPE_ID { get; set; }
        /// <summary>
        /// PKG生效日期
        /// </summary>
        public string EFFECT_DATE { get; set; }
        public string BATTERY_MODEL { get; set; }
        public string BATTERY_TYPE { get; set; }
        public decimal BATTERY_LAYERS { get; set; }
        /// <summary>
        /// 需求数量
        /// </summary>
        public decimal BATTERY_QTY { get; set; }
        /// <summary>
        /// 产品PN
        /// </summary>
        public string BATTERY_PARTNO { get; set; }
        /// <summary>
        /// 项目代码
        /// </summary>
        public string PROJECT_CODE { get; set; }
        /// <summary>
        /// 客户代码
        /// </summary>
        public string CUSTOMER_CODE { get; set; }
        /// <summary>
        /// PKG的目的
        /// </summary>
        public string PURPOSE { get; set; }
        /// <summary>
        /// PKG的费用类型
        /// </summary>
        public string ORDER_TYPE { get; set; }
        public string SO_NO { get; set; }
        /// <summary>
        /// 加急？
        /// </summary>
        public string IS_URGENT { get; set; }
        /// <summary>
        /// 要求的出货日期
        /// </summary>
        public string OUTPUT_TARGET_DATE { get; set; }
        /// <summary>
        /// 加急原因
        /// </summary>
        public string REASON_FORURGENT { get; set; }
        /// <summary>
        /// PKG准备人
        /// </summary>
        public string PREPARED_BY { get; set; }
        /// <summary>
        /// PKG准备时间
        /// </summary>
        public string PREPARED_DATE { get; set; }
        /// <summary>
        /// PKG批准流程
        /// </summary>
        public string APPROVE_FLOW_ID { get; set; }
        public string UPDATE_USER { get; set; }
        public string UPDATE_DATE { get; set; }
        /// <summary>
        /// 每组数量清单
        /// </summary>
        public string GROUP_QTY_LIST { get; set; }
        /// <summary>
        /// 1：开启，0：关闭
        /// </summary>
        public string VALID_FLAG { get; set; }
        /// <summary>
        /// 1：已删除，0：未删除
        /// </summary>
        public string DELETE_FLAG { get; set; }
        /// <summary>
        /// 1：草稿，2：送审，3：退回，4:签审，5:发布
        /// </summary>
        public string STATUS { get; set; }

        public string PRODUCT_CHANGE_HL { get; set; }
        public string PROCESS_CHANGE_HL { get; set; }
        public string MATERIAL_CHANGE_HL { get; set; }
        public string OTHER_CHANGE_HL { get; set; }

        //辅助
        public int VERSION_NUM_DIGITAL { get; set; }//将字母转换为数字

        //复制
        public string ORA_PACKAGE_NO { get; set; }
        public string ORA_VERSION_NO { get; set; }

        //关联
        public string WF_SET_STEP_NAME { get; set; }

    }
}
