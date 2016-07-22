namespace Model.BaseInfo
{
    public class RECIPE_TYPE_LIST_Entity
    {
        public decimal TOTAL { get; set; }
        /// <summary>
        /// AMIX:阳极配方;CMIX:阴极配方;ABLM:阳极底涂配方;SEPM:隔离膜配方;ESNM:电解液配方
        /// </summary>
        public string RECIPE_TYPE_ID { get; set; }
        public string FACTORY_ID { get; set; }
        public string PRODUCT_TYPE_ID { get; set; }
        public string PRODUCT_PROC_TYPE_ID { get; set; }
        public string UPDATE_USER { get; set; }
        public string UPDATE_DATE { get; set; }
        public string VALID_FLAG { get; set; }
        public string RECIPE_TYPE_NAME { get; set; }
        public string RECIPE_TYPE_DESC { get; set; }
    }
}
