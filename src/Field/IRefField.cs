using AdminLib.Data.Handler.SQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminLib.Data.Handler.SQL.Field {
    public interface IRefField : IField {
        AStructure GetRefModel();
        BaseField  GetRefField();
    }
}