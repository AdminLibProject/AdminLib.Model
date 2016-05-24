﻿using AdminLib.Data.Handler.SQL.Model;
using AdminLib.Data.Handler.SQL.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web;

namespace AdminLib.Data.Handler.SQL {
    public abstract class DjangoModel<Self> : IModel
        where Self : DjangoModel<Self> {

        /******************** Static attributes ********************/
        public static ModelStructure structure;

        /******************** Attributes ********************/
        private AStructure model {
            get{
                return DjangoModel<Self>.structure;
            }
        }

        /******************** Static Methods ********************/

        public static FieldFilter CreateFilter(string field, FilterOperator type, string value) {

            return new FieldFilter ( rootModel : structure
                                   , path      : field
                                   , type      : type
                                   , value     : value);

        }

        public static FieldFilter CreateFilter(string field, FilterOperator type, string[] value) {

            return new FieldFilter ( rootModel : structure
                                   , path      : field
                                   , type      : type
                                   , value     : value);

        }

        public static FieldFilter CreateFilter(string field, FilterOperator type, int value) {

            return new FieldFilter ( rootModel : structure
                                   , path      : field
                                   , type      : type
                                   , value     : value);

        }

        public static FieldFilter CreateFilter(string field, FilterOperator type, int[] value) {

            return new FieldFilter ( rootModel : structure
                                   , path      : field
                                   , type      : type
                                   , value     : value);

        }

        public static ModelStructure Initialize() {
            DjangoModel<Self>.structure = new ModelStructure(typeof(Self));

            if (DjangoModel<Self>.structure.primaryKeys.Length != 1)
                throw new Exception("The model have an incorrect number of primary keys");

            return DjangoModel<Self>.structure;
        }

        public static ModelStructure GetModelStructure() {
            return structure;
        }

        public static SqlQuery GetQuery(Filter filter, string[] fields, OrderBy[] orderBy) {

            SqlQuery sqlQuery;

            sqlQuery = new SqlQuery ( model   : DjangoModel<Self>.structure
                                    , fields  : fields
                                    , filter  : filter
                                    , sorting : orderBy);

            return sqlQuery;
        }

        public static Self QueryItem(IConnection connection, int id, string[] fields) {

            return (Self) DjangoModel<Self>.QueryItem ( connection : connection
                                                      , id         : id
                                                      , fields     : fields);

        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="connection">Connection to use</param>
        /// <param name="filter">List of filters</param>
        /// <param name="fields">API Name of the fields to return</param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static Self[] QueryItems ( IConnection connection
                                        , Filter      filter
                                        , string[]    fields
                                        , OrderBy[]   orderBy) {

            Object[] items;
            Self[]   results;

            items = DjangoModel<Self>.structure.QueryItems ( connection : connection
                                                           , filter     : filter
                                                           , fields     : fields
                                                           , orderBy    : orderBy);

            results = new Self[items.Length];

            items.CopyTo(results, 0);

            return results;
        }

        /******************** Methods ********************/
        public virtual void Add<Model>(IConnection connection, Model model, string path=null) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        ///     Create the instance into the database.
        ///     Note that if the model is sequence based and no ID has been provided
        ///     then the function will create a new ID (based on the sequence) and use
        ///     it for the database reccord.
        ///     The function will then update the ID of the object with the calculated one.
        ///     The function will NOT create subelements such as foreign key objects or
        ///     list fields.
        ///
        ///     Example :
        ///         
        ///         country : {id:null, code:'F'}
        ///         country.id; // null
        ///         country.Create(connection);
        ///         country.id; // 1
        ///         
        ///         country = {id:2, code:'G'}
        ///         country.id; // 2
        ///         country.Create(connection);
        ///         country.id; // 2
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="fields"></param>
        public virtual void Create(IConnection connection, string[] fields=null) {

            int? id;

            id = DML.Create ( connection : connection
                            , model      : this.model
                            , instance   : this
                            , fields     : fields);

            if (this.model.sequenceBased && id != null)
                this.model.IdField.SetValue ( instance : this
                                            , value    : id);
        }

        public virtual void Delete(IConnection connection) {
            DML.Delete ( connection : connection
                       , model      : DjangoModel<Self>.structure
                       , instance   : this);
        }

        /// <summary>
        ///     This function is to remove one item from the instance.
        ///     For example :
        ///     
        /// 
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="connection"></param>
        /// <param name="model"></param>
        /// <param name="path"></param>
        public virtual void Remove<Model>(IConnection connection, Model model, string path=null) {
            throw new NotImplementedException();
        }

        public virtual void Update(IConnection connection, string[] fields=null, string[] emptyFields=null) {

            DML.Update ( connection   : connection
                       , model       : DjangoModel<Self>.structure
                       , instance    : this
                       , fields      : fields
                       , emptyFields : emptyFields);

        }
    }
}