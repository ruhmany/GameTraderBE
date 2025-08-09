using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.StaticData
{
    public static class AllowedPermissions
    {
        public static List<string> NoAuthenticationRoutes()
                => new()
                {
                    "/user/login",
                    "/workbase",
                    "/role",
                    "/user/create",
                    "/user/sendemail",
                    "/user/ConfirmEmail",
                    "/user/CreateProfile",
                    "/user/get-all",
                    "/account/create",
                    "/Payment/Request-To-Buy-Items"
                };

        public static List<string> RequireAuthenticationRoutes()
               => new()
               {
                   "/user/logout",
                    "/user/changepassword",
                    "/upload",
                    "/user/getUserPermissions",
                    "/Lookup/get-all-countries",
                    "/Lookup/get-all-workbases",
                    "/Lookup/get-provinces-by-countryId",
                    "/Lookup/get-districts-by-provinceId",
                    "/Lookup/get-towns-by-districtId",
                    "/Lookup/get-wards-by-townId",
                    "/Lookup/get-townships-by-wardId",
                    "/Lookup/get-municipalities-by-districtId",
                    "/Lookup/get-all-municipalities",
                    "/Lookup/get-all-wards",
                    "/Lookup/get-all-provinces",

               };
    }
}
