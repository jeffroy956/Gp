using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpCore.Model.Common
{
    public static class DbUtil
    {
        public static int? GetSafeInt32(this DbDataReader r, int ordinal)
        {
            if (r.IsDBNull(ordinal))
            {
                return null;
            }

            return r.GetInt32(ordinal);
        }
        public static decimal? GetSafeDecimal(this DbDataReader r, int ordinal)
        {
            if (r.IsDBNull(ordinal))
            {
                return null;
            }

            return r.GetDecimal(ordinal);
        }

        public static long? GetSafeInt64(this DbDataReader r, int ordinal)
        {
            if (r.IsDBNull(ordinal))
            {
                return null;
            }

            return r.GetInt64(ordinal);
        }

        public static bool? GetSafeBool(this DbDataReader r, int ordinal)
        {
            if (r.IsDBNull(ordinal))
            {
                return null;
            }

            return r.GetBoolean(ordinal);
        }

        public static string GetSafeString(this DbDataReader r, int ordinal)
        {
            if (r.IsDBNull(ordinal))
            {
                return null;
            }

            return r.GetString(ordinal);
        }
        public static DateTime? GetSafeDateTime(this DbDataReader r, int ordinal)
        {
            if (r.IsDBNull(ordinal))
            {
                return null;
            }

            return r.GetDateTime(ordinal);
        }

        public static object GetDbParamValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }

            return value;
        }


    }
}
