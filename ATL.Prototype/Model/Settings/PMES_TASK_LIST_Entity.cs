namespace Model.Settings
{
    public class PMES_TASK_LIST_Entity
    {
        public decimal TOTAL { get; set; }
        public string PMES_TASK_ID { get; set; }
        public string FACTORY_ID { get; set; }
        public string UPDATE_USER { get; set; }
        public string UPDATE_DATE { get; set; }
        public string TASK_NAME { get; set; }
        public string TASK_DESC { get; set; }
        public string PROGRAM_NAME { get; set; }
        public string MODULE_NAME { get; set; }
        /// <summary>
        /// IUDS
        /// </summary>
        public string FUNCTIONS { get; set; }
        public string VALID_FLAG { get; set; }
        public string MENU_NAME { get; set; }
        public decimal MENU_LAYER { get; set; }
        public string PARENT_MENU { get; set; }
    }
}
