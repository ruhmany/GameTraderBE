using GameTrader.Core.Attributes;
using GameTrader.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.Helpers
{
    public static class EnumAttributeHelper
    {
        public static string GetDisplayName(this Enum value)
        {
            if (value is null) return "";
            var attribute = value.GetAttribute<Core.Attributes.DisplayNameAttribute>();
            return attribute != null ? attribute.DisplayName : "";
        }

        public static string GetURL(this Enum value)
        {
            if (value is null) return "";
            var attribute = value.GetAttribute<Core.Attributes.UrlAttribute>();
            return attribute != null ? attribute.Url : "";
        }

        public static RoleEnum[] GetRoles(this Enum value)
        {
            if (value is null) return Array.Empty<RoleEnum>();
            var attribute = value.GetAttribute<Core.Attributes.RolesAttribute>();
            return attribute != null ? attribute.Roles : Array.Empty<RoleEnum>();
        }

        public static string GetId(this Enum value)
        {
            if (value is null) return "";
            var attribute = value.GetAttribute<Core.Attributes.RoleIdAttribute>();
            return attribute != null ? attribute.RoleId : "";
        }

        private static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var memberInfos = value.GetType().GetMember(value.ToString());
            var attribute = memberInfos[0].GetCustomAttributes(typeof(T), false);
            return attribute.Length > 0 ? (T)attribute[0] : null;
        }
    }
}
