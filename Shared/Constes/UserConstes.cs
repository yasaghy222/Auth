namespace Auth.Shared.Constes
{
    public static class UserConstes
    {
        #region  Base
        public const string Base_Title = "User";
        public const string Base_Url = "/user";
        #endregion Base

        #region  Users
        public const string Admin_Id = "01J5Q6HDSW0J4G3SRWGJNZYJFD";
        public const string Admin_Username = "AdminAuthService1";
        public const string Admin_Password = "AdminAuth@123";
        #endregion Users

        #region Resources
        public const string Create_Resource_Id = "01J75XD2VG53V5JMXKT078BSCM";
        public const string Create_Resource_Title = $"{BaseConstes.Auth_Service_Title}.{Base_Title}.Create";
        public const string Create_Resource_Url = $"{Base_Url}";

        public const string Update_Resource_Id = "01J766GEZFB6AWAWBA8Y978TDD";
        public const string Update_Resource_Title = $"{BaseConstes.Auth_Service_Title}.{Base_Title}.Update";
        public const string Update_Resource_Url = $"{Base_Url}";

        public const string Delete_Resource_Id = "01J7116S6NREKY0K41KXX8RJG9";
        public const string Delete_Resource_Title = $"{BaseConstes.Auth_Service_Title}.{Base_Title}.Delete";
        public const string Delete_Resource_Url = $"{Base_Url}";

        public const string Login_Resource_Id = "01J78RQD19F0HKFZ86Q6RMXFRN";
        public const string Login_Resource_Title = $"{BaseConstes.Auth_Service_Title}.{Base_Title}.Login";
        public const string Login_Resource_Url = $"{Base_Url}/login";

        public const string Change_Password_Resource_Id = "01J7672H9BAKWQTDSWGJQQY4VB";
        public const string Change_Password_Resource_Title = $"{BaseConstes.Auth_Service_Title}.{Base_Title}.ChangePassword";
        public const string Change_Password_Resource_Url = $"{Base_Url}/change-password";

        public const string Reset_Password_Resource_Id = "01J767KEK4MCQ9S81HM1J67QPC";
        public const string Reset_Password_Resource_Title = $"{BaseConstes.Auth_Service_Title}.{Base_Title}.ResetPassword";
        public const string Reset_Password_Resource_Url = $"{Base_Url}/reset-password";

        public const string Get_Profile_Resource_Id = "01J767VZBPGXJ99B0N6P2RSRER";
        public const string Get_Profile_Resource_Title = $"{BaseConstes.Auth_Service_Title}.{Base_Title}.Get.Profile";
        public const string Get_Profile_Resource_Url = $"{Base_Url}/profile";

        public const string Update_Profile_Resource_Id = "01J781QHZE3R9N1T51S7P309TZ";
        public const string Update_Profile_Resource_Title = $"{BaseConstes.Auth_Service_Title}.{Base_Title}.Update.Profile";
        public const string Update_Profile_Resource_Url = $"{Base_Url}/profile";

        public const string Get_Id_Resource_Id = "01J767RDK36KS41HG0Z1RSZV1E";
        public const string Get_Id_Resource_Title = $"{BaseConstes.Auth_Service_Title}.{Base_Title}.Get.Id";
        public const string Get_Id_Resource_Url = Base_Url + "/{id}";

        public const string Get_List_Filter_Resource_Id = $"01J712XPPNQQYTS5M07S5ACW5Q";
        public const string Get_List_Filter_Resource_Title = $"{BaseConstes.Auth_Service_Title}.{Base_Title}.List.Filter";
        public const string Get_List_Filter_Resource_Url = $"{Base_Url}/list/filter";
        #endregion Resources

        #region  Permissions
        public const string Create_Permission_Id = "01J769CN7G5BJD4KB80J2V86DR";
        public const string Update_Permission_Id = "01J769CTVZJV7H8XSMDR3KTH6A";
        public const string Delete_Permission_Id = "01J780T96RA71288TN35P3PJAK";
        public const string Get_Id_Permission_Id = "01J780VWZZ5C0A3T56G1DE49M5";
        public const string Get_List_Filter_Permission_Id = "01J780WFSNSQ99FA78VKFSQ62D";
        #endregion Permissions
    }
}