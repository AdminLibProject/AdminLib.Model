using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminLib.Data.Handler.SQL.Field {
    public interface IAttributeField {
        Field.BaseField GetField();
    }
}