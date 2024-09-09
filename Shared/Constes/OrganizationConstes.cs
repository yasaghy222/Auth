namespace Auth.Shared.Constes
{
    public class OrganizationConstes
    {
        public const string Base_Title = "Organization";
        public const string Base_Url = "/organization";

        #region  Services
        public const string Auth_Service_Id = "01J5N8HWG80H4Y3T6WRQ2PGBQ8";
        public const string Auth_Service_Title = $"{BaseConstes.Auth_Service_Title}.Service";

        public const string Accounting_Service_Id = "01J5N8P83VSNQRJNJ8FD6E0M8E";
        public const string Accounting_Service_Title = $"{BaseConstes.Accounting_Service_Title}.Service";

        public const string RedSense_Service_Id = "01J5N8QDTDVH64T5K3B9RCRB88";
        public const string RedSense_Service_Title = $"{BaseConstes.RedSense_Service_Title}.Service";

        public const string RedGuard_Update_Service_Id = "01J5N3BYB6VXQ4GYFERCAGE5Z8";
        public const string RedGuard_Update_Service_Title = $"{BaseConstes.RedGuard_Update_Service_Title}.Service";
        #endregion Services

        #region Resources
        public const string Create_Resource_Id = "01J7BB1K2NH6AT2W2MQVZA4BS0";
        public const string Create_Resource_Title = $"{BaseConstes.Auth_Service_Title}.{Base_Title}.Create";
        public const string Create_Resource_Url = $"{Base_Url}";

        public const string Update_Resource_Id = "01J7BAPE08D1S4PAZMA10BJHN8";
        public const string Update_Resource_Title = $"{BaseConstes.Auth_Service_Title}.{Base_Title}.Update";
        public const string Update_Resource_Url = $"{Base_Url}";

        public const string Delete_Resource_Id = "01J7BAPJRJD6Y2TKHZZYSJ0QGA";
        public const string Delete_Resource_Title = $"{BaseConstes.Auth_Service_Title}.{Base_Title}.Delete";
        public const string Delete_Resource_Url = $"{Base_Url}";

        public const string Get_Id_Resource_Id = "01J7BAPS99BHBE0BPB30GH2CB8";
        public const string Get_Id_Resource_Title = $"{BaseConstes.Auth_Service_Title}.{Base_Title}.Get.Id";
        public const string Get_Id_Resource_Url = Base_Url + "/{id}";

        public const string Get_List_Filter_Resource_Id = $"01J7BAQ11B5ZXDK9FAK593YR3Y";
        public const string Get_List_Filter_Resource_Title = $"{BaseConstes.Auth_Service_Title}.{Base_Title}.List.Filter";
        public const string Get_List_Filter_Resource_Url = $"{Base_Url}/list/filter";
        #endregion Resources

        #region  Permissions
        public const string Create_Permission_Id = "01J7BB0ZKTSDV44XB7QMNSX86Y";
        public const string Update_Permission_Id = "01J7BAMWYK68R2C9HSA8P20E2G";
        public const string Delete_Permission_Id = "01J7BAN17716QSSDSTCG72TZ9D";
        public const string Get_Id_Permission_Id = "01J7BAN5HWB906RFW0F1B87XJH";
        public const string Get_List_Filter_Permission_Id = "01J7BANANFZA38EYFWT9DMDQA2";
        #endregion Permissions

    }
}