namespace Entities
{
    public static class Constants
    {
        public const string USERS = "Users";
        public const string DATABASE = "MongoDb";
        public const string CONNECTION_STRING = "ConnectionString";
        public const string LOOPDESK = "LoopDesk";
        public const string SOMETHING_WENT_WRONG = "Something went wrong";

        public static class USER_VALIDATIONS
        {
            public const string CANNOT_BE_EMPTY = "{0} cannot be empty";
            public const string USER_ALREADY_EXISTS_WITH_EMAIL = "User already exists with this email";
            public const string USER_NOT_FOUND = "User not found";
            public const string ROLE_NOT_FOUND = "Role not found";
        }
    }
}