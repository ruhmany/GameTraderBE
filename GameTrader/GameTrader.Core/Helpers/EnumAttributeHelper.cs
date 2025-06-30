using GameTrader.Core.Attributes;
using GameTrader.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.Helpers
{
    public static class EnumAttributeHelper
    {
        public static string GetDescription(this Enum value)
        {
            var attribute = value.GetAttribute<DescriptionAttribute>();
            return attribute == null ? "" : attribute.Description;
        }

        public static string GetDisplayName(this Enum value)
        {
            if (value is null) return "";
            var attribute = value.GetAttribute<Core.Attributes.DisplayNameAttribute>();
            return attribute == null ? "" : attribute.DisplayName;
        }

        public static string GetURL(this Enum value)
        {
            var attribute = value.GetAttribute<Core.Attributes.UrlAttribute>();
            return attribute == null ? "" : attribute.Url;
        }

        public static RoleEnum[] GetPermissionRoles(this Enum value)
        {
            var attribute = value.GetAttribute<Core.Attributes.RolesAttribute>();
            return attribute == null ? [] : attribute.Roles;
        }

        public static string GetId(this Enum value)
        {
            var attribute = value.GetAttribute<Core.Attributes.RoleIdAttribute>();
            return attribute == null ? "" : attribute.RoleId;
        }
        private static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var memberInfo = value.GetType().GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0 ? (T)attributes[0] : null;
        }
    }
}
