﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdminLib.Data.Handler.SQL;
using AdminLib.Data.Handler.SQL.Model;

namespace AdminLib.Data.Handler.SQL {
    public static class API {

        /******************** Methods ********************/

        public static FilterOperator? GetFilterOperator(string operation) {
            return FilterType_extension.StringToFilter(operation);
        }

        public static bool IsFilterOperator(string operation) {
            return GetFilterOperator(operation) != null;
        }

        public static GroupOperator? GetGroupOperator(string operation) {
            return GroupOperator_extension.Get(operation);
        }

        public static bool IsGroupOperator(string operation) {
            return GetGroupOperator(operation) != null;
        }

    }
}