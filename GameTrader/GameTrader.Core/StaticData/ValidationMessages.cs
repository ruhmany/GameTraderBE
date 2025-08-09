using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.StaticData
{
    public static class ValidationMessages
    {
        public const string PasswordIsRequired = "Password Is required";
        public const string PasswordMinLength = "Password must be at least 8 characters long";
        public const string PasswordComplexity = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character";
        public const string BadRequest = "Bad Request";
        public const string RequestSuccess = "Request Success";
        public const string RequestFalid = "Request Falid";
        public const string OperationSucceded = "Operation Succeded"; 
        public const string OperationFaild = "Operation Faild";
        public const string Unauthorized = "Unauthorized";
        public const string NotFound = "Not Found";
        public const string NoDataFound = "No Data Found";
        public const string UnauthrizedManager = "Manager Is Not Allowed To Edit Or Delete Any Manager Or Superadmin Data";
        public const string InvalidEmailOrPassword = "Incorrect Email or Password";
        public const string DisabledUser = "Your account has been locked due to multiple unsuccessful login attempts. Please try again in 10 minutes";
        public const string LoginSuccess = "Login Success";
        public const string LogOutSuccess = "You have been logged out successfully.";
        public const string LogOutFailed = "Something went wrong";
        public const string FirstNameIsRequired = "First Name Is required";
        public const string LastNameIsRequired = "Last NameIs required";
        public const string EmailIsRequired = "Email Is required";
        public const string PhoneNumberIsRequired = "Phone Number Is required";
        public const string UserRoleIsRequired = "User role Is required";
        public const string StatusIsRequired = "Status Is required";
        public const string MaxLength = "Max Length is 50 characters";
        public const string MaxLength30 = "Max Length is 30 characters";
        public const string LettersOnly = "Use Letters Only";
        public const string PhoneNumberNotValid = "Invalid Phone Number";
        public const string EmailNotValid = "Invalid Email";
        public const string InvalidRole = "Invalid Role";
        public const string UserNotActive = "Please confirm your email";
        public const string InvalidFileSize = "Image upload failed: File size exceeds the 5MB limit. Please upload a smaller image";
        public const string EnteredOldPassword = "Your password has been reset. Please use the temporary password sent to your email";
        public const string InvalidDateRange = "Invalid date range. The 'From' date cannot be after the 'To' date";
        public const string NotAllowFeatureDate = "Future date must not be allowed";
        public const string DashboardLoadingError = "Unable to fetch dashboard data. Please try again later";  
        public const string AccountCreatedSuccessfully = "Account created successfully.";
        public const string AccountCreationFailed = "Account creation failed. Please try again.";
        public const string AccountNotFound = "Account not found.";
        public const string AccountUpdatedSuccessfully = "Account Updated Successfully";
        public const string AccountUpdateFailed = "Account Update Failed. Please try again.";
        public const string ItemNotFound = "Item not found.";
        public const string ItemAddedSuccessfully = "Item added successfully.";
        public const string ItemAdditionFailed = "Item addition failed. Please try again.";
        public const string ItemUpdatedSuccessfully = "Item updated successfully.";
        public const string ItemUpdateFailed = "Item update failed. Please try again.";
        public const string ItemDeletedSuccessfully = "Item deleted successfully.";
        public const string ItemDeletionFailed = "Item deletion failed. Please try again.";
        public const string RequestCreatedSuccessfully = "Request created successfully.";
        public const string RequestCreationFailed = "Request creation failed. Please try again.";
        public const string RequestUpdatedSuccessfully = "Request updated successfully.";
        public const string RequestUpdateFailed = "Request update failed. Please try again.";
        public const string RequestDeletedSuccessfully = "Request deleted successfully.";
        public const string RequestDeletionFailed = "Request deletion failed. Please try again.";
        public const string RequestNotFound = "Request not found.";
    }


}
