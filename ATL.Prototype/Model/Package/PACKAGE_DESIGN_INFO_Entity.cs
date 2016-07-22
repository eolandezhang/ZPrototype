
namespace Model.Package
{
    public class PACKAGE_DESIGN_INFO_Entity
    {
        public decimal TOTAL { get; set; }
        public string PACKAGE_NO { get; set; }//
        public string GROUP_NO { get; set; }//
        public string VERSION_NO { get; set; }//
        public string FACTORY_ID { get; set; }//
        public decimal CELL_CAP { get; set; }//电池容量
        public decimal BEG_VOL { get; set; }//起始电压
        public decimal END_VOL { get; set; }//截至电压
        public string ANODE_STUFF_ID { get; set; }//阳极材料
        public string ANODE_FORMULA_ID { get; set; }//阳极配方ID
        public decimal ANODE_COATING_WEIGHT { get; set; }//阳极涂布重量
        public decimal ANODE_DENSITY { get; set; }//阳极压实密度
        public string ANODE_FOIL_ID { get; set; }//阳极集流体材料
        public string CATHODE_STUFF_ID { get; set; }//阴极材料
        public string CATHODE_FORMULA_ID { get; set; }//阴极配方ID
        public decimal CATHODE_COATING_WEIGHT { get; set; }//阴极涂布重量
        public decimal CATHODE_DENSITY { get; set; }//阴极压实密度
        public string CATHODE_FOIL_ID { get; set; }//阴极集流体材料
        public string SEPARATOR_ID { get; set; }//隔离膜材料
        public string ELECTROLYTE_ID { get; set; }//电解液配方
        public decimal INJECTION_QTY { get; set; }//注液量
        public decimal LIQUID_PER { get; set; }//保液系数
        public string MODEL_DESC { get; set; }//补充说明
        public string VALID_FLAG { get; set; }//
        public string DESIGN_DATE { get; set; }//设计日期
        public string UPDATE_USER { get; set; }//
        public string UPDATE_DATE { get; set; }//
        public decimal ANODE_THICKNESS { get; set; }//阳极集流体厚度
        public decimal CATHODE_THICKNESS { get; set; }//阴极集流体厚度

        //关联
        public string PRODUCT_TYPE_ID { get; set; }
        public string PRODUCT_PROC_TYPE_ID { get; set; }

        //辅助
        public decimal CELL_CAP_tolerance { get; set; }
        public decimal BEG_VOL_tolerance { get; set; }
        public decimal END_VOL_tolerance { get; set; }
        public decimal ANODE_COATING_WEIGHT_tolerance { get; set; }
        public decimal ANODE_DENSITY_tolerance { get; set; }
        public decimal CATHODE_COATING_WEIGHT_tolerance { get; set; }
        public decimal CATHODE_DENSITY_tolerance { get; set; }
        public decimal INJECTION_QTY_tolerance { get; set; }
        public decimal LIQUID_PER_tolerance { get; set; }
        public decimal ANODE_THICKNESS_tolerance { get; set; }
        public decimal CATHODE_THICKNESS_tolerance { get; set; }

        //是否是查询条件
        public bool check_CELL_CAP { get; set; }//电池容量
        public bool check_BEG_VOL { get; set; }//起始电压
        public bool check_END_VOL { get; set; }//截至电压
        public bool check_ANODE_STUFF_ID { get; set; }//阳极材料
        public bool check_ANODE_FORMULA_ID { get; set; }//阳极配方ID
        public bool check_ANODE_COATING_WEIGHT { get; set; }//阳极涂布重量
        public bool check_ANODE_DENSITY { get; set; }//阳极压实密度
        public bool check_ANODE_FOIL_ID { get; set; }//阳极集流体材料
        public bool check_CATHODE_STUFF_ID { get; set; }//阴极材料
        public bool check_CATHODE_FORMULA_ID { get; set; }//阴极涂布重量
        public bool check_CATHODE_COATING_WEIGHT { get; set; }//阴极涂布重量
        public bool check_CATHODE_DENSITY { get; set; }//阴极压实密度
        public bool check_CATHODE_FOIL_ID { get; set; }//阴极集流体材料
        public bool check_SEPARATOR_ID { get; set; }//隔离膜材料
        public bool check_ELECTROLYTE_ID { get; set; }//电解液配方
        public bool check_INJECTION_QTY { get; set; }//注液量
        public bool check_LIQUID_PER { get; set; }//保液系数
        public bool check_ANODE_THICKNESS { get; set; }//阳极集流体厚度
        public bool check_CATHODE_THICKNESS { get; set; }//阴极集流体厚度

    }
}
