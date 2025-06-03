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
            public const string REQUESTOR_NOT_FOUND = "Requestor not found";
            public const string USER_NOT_AUTHORIZED = "User not authorized";
        }

        public static class ROLE
        {
            public const string ADMIN = "Admin";
        }

        public static class SUBSCRIPTION_VALIDATIONS
        {
            public const string NAME_REQUIRED = "Name is required";
            public const string PRICE_MUST_BE_POSITIVE = "Price must be greater than zero";
            public const string PROJECTS_ALLOWED_MUST_BE_POSITIVE = "Projects allowed must be greater than zero";
            public const string USERS_ALLOWED_MUST_BE_POSITIVE = "Users allowed must be greater than zero";
            public const string NAME_TOO_LONG = "Name cannot exceed 100 characters";
            public const string DESCRIPTION_TOO_LONG = "Description cannot exceed 500 characters";
            public const string PRICE_TOO_HIGH = "Price cannot exceed 1000000";
            public const string SUBSCRIPTION_WITH_NAME_ALREADY_EXISTS = "Subscription with this name already exists";
            public const string SUBSCRIPTION_NOT_FOUND = "Subscription not found";
            public const string INVALID_ID = "Invalid subscription id";
            public const string CREATED_SUCCESSFULLY = "Subscription created successfully";
            public const string DELETED_SUCCESSFULLY = "Subscription deleted successfully";
        }
    }
}