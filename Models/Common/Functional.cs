namespace HRM_Project.Models.Common
{
    class Functional
    {
        public const string allFunctionals = "allFunctionals";

        public const string userList = "userList";
        public const string userCreate = "userCreate";
        public const string userUpdate = "userUpdate";
        public const string userDelete = "userDelete";

        public const string roleList = "roleList";
        public const string roleCreate = "roleCreate";
        public const string roleUpdate = "roleUpdate";
        public const string roleDelete = "roleDelete";

        public const string authDetailList = "authDetailList";
        public const string authDetailCreate = "authDetailCreate";
        public const string authDetailUpdate = "authDetailUpdate";
        public const string authDetailDelete = "authDetailDelete";


        public static IList<string> GetAllFunctionals()
        {
            var type = typeof(Functional);
            return type.GetFields().Select(x => x.GetValue(null)!.ToString()).ToList()!;
        }
    }
}
