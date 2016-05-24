using AdminLib.Data.Handler.SQL.Model;
using Oracle.ManagedDataAccess.Client;

namespace AdminLib.Data.Handler.SQL.Field {
    public interface IField {

        void         Validate();
        string       GetDbColumn();
        void         Initialize(AStructure model);
        OracleDbType GetDbType();
    }
}