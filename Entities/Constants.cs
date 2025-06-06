using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace Entities
{
    public static class Constants
    {
        public const string USERS = "Users";
        public const string DATABASE = "MongoDb";
        public const string CONNECTION_STRING = "ConnectionString";
        public const string LOOPDESK = "LoopDesk";
        public const string SOMETHING_WENT_WRONG = "Something went wrong";

        public static class Keys
        {
            public const string EMAIL = "Email";
            public const string NAME = "Name";
            public const string PASSWORD = "Password";
        }

        public static class USER_VALIDATIONS
        {
            public const string CANNOT_BE_EMPTY = "{0} cannot be empty";
            public const string USER_ALREADY_EXISTS_WITH_EMAIL = "User already exists with this email";
            public const string USER_NOT_FOUND = "User not found";
            public const string ROLE_NOT_FOUND = "Role not found";
            public const string REQUESTOR_NOT_FOUND = "Requestor not found";
            public const string USER_NOT_AUTHORIZED = "User not authorized";
            public const string USER_CREATED_SUCCESSFULLY = "User created successfully";
        }

        public static class ROLE
        {
            public const string ADMIN = "Admin";
            public const string COMPANY_ADMIN = "CompanyAdmin";
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
            public const string FETCHED_SUCCESSFULLY = "Subscriptions fetched successfully";

        }

        public static class COMPANY_VALIDATIONS
        {
            public const string NAME_REQUIRED = "Company name is required";
            public const string DOMAIN_REQUIRED = "Domain is required";
            public const string ADDRESS_REQUIRED = "Address is required";
            public const string PHONE_REQUIRED = "Phone number is required";
            public const string LOGOURL_REQUIRED = "Logo URL is required";
            public const string DESCRIPTION_REQUIRED = "Description is required";
            public const string COUNTRYID_REQUIRED = "Country ID is required";
            public const string STATEID_REQUIRED = "State ID is required";
            public const string CITYID_REQUIRED = "City ID is required";
            public const string COUNTRY_NOT_FOUND = "Country not found";
            public const string STATE_NOT_FOUND = "State not found";
            public const string CITY_NOT_FOUND = "City not found";
            public const string COMAPNY_ALREADY_EXISTS = "Company already exists for this owner";
            public const string CREATED_SUCCESSFULLY = "Company created successfully";
            public const string DOMAIN_INVALID = "Domain is invalid";
            public const string PHONE_INVALID = "Phone number is invalid";
            public const string NAME_REQUIREMENTS = "Company name must be between 3 and 50 characters";
            public const string DOMAIN_REQUIREMENTS = "Domain must be between 3 and 100 characters";
            public const string ADDRESS_REQUIREMENTS = "Address must be between 5 and 200 characters";
            public const string PHONE_REQUIREMENTS = "Phone number must be between 10 and 15 characters";
            public const string COMPANY_NOT_FOUND = "Company not found";
        }

        public static class USER_SUBSCRIPTION_VALIDATIONS
        {
            public const string USER_SUBSCRIPTION_ALREADY_EXISTS = "User subscription already exists";
            public const string USER_SUBSCRIPTION_NOT_FOUND = "User subscription not found";
            public const string USER_SUBSCRIPTION_CREATED_SUCCESSFULLY = "User subscription created successfully";
            public const string USER_SUBSCRIPTION_DELETED_SUCCESSFULLY = "User subscription deleted successfully";
            public const string USER_SUBSCRIPTION_FETCHED_SUCCESSFULLY = "User subscriptions fetched successfully";
            public const string USER_SUBSCRIPTION_CREATION_FAILED = "User subscription creation failed";
        }
    }
}