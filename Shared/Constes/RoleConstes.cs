namespace Auth.Shared.Constes
{
    public class RoleConstes
    {
        public const string Base_Title = "Role";
        public const string Base_Url = "/role";

        public const string Admin_Role_Id = $"01J5NAK6SJGDKN0NJ00YX7MWXP";
        public const string Admin_Role_Name = $"Admin.{OrganizationConstes.Auth_Service_Title}";

        #region Resources
        public const string Create_Resource_Id = "01J874QSS0EVCMF3BEBNG92G8F";
        public const string Create_Resource_Title = $"{BaseConstes.Auth_Service_Title}.{Base_Title}.Create";
        public const string Create_Resource_Url = $"{Base_Url}";

        public const string Update_Resource_Id = "01J874S1ZCP3T6DNJMAVD91MKY";
        public const string Update_Resource_Title = $"{BaseConstes.Auth_Service_Title}.{Base_Title}.Update";
        public const string Update_Resource_Url = $"{Base_Url}";

        public const string Delete_Resource_Id = "01J874SAREFNM5DYES68F4RKN5";
        public const string Delete_Resource_Title = $"{BaseConstes.Auth_Service_Title}.{Base_Title}.Delete";
        public const string Delete_Resource_Url = Base_Url + "/{id}";

        public const string Get_Id_Resource_Id = "01J874SX40GRZQHAZ1ATRKNSRA";
        public const string Get_Id_Resource_Title = $"{BaseConstes.Auth_Service_Title}.{Base_Title}.Get.Id";
        public const string Get_Id_Resource_Url = Base_Url + "/{id}";

        public const string Get_List_Filter_Resource_Id = $"01J874TE1QXCP4FTYMCKH2FFNB";
        public const string Get_List_Filter_Resource_Title = $"{BaseConstes.Auth_Service_Title}.{Base_Title}.List.Filter";
        public const string Get_List_Filter_Resource_Url = $"{Base_Url}/list/filter";
        #endregion Resources

        #region  Permissions
        public const string Create_Permission_Id = "01J874VA65CC1FN2F8S1EX6GHZ";
        public const string Update_Permission_Id = "01J874VGKV59ZNYKSWYSTATM04";
        public const string Delete_Permission_Id = "01J874VPS4A7GBBX91YGMT7W07";
        public const string Get_Id_Permission_Id = "01J874W1FNXSG6BTGFZS73V375";
        public const string Get_List_Filter_Permission_Id = "01J874YXA19YE9513B9VYQ22KR";
        #endregion Permissions
    }
}