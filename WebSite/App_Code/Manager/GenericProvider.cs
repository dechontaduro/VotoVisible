using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Configuration;

using log4net;


namespace com.VotoVisible.Manager
{
    /// <summary>
    /// Clase: GenericProvider
    /// Descripción: Proveedor generico con Factory.
    /// Basado en Subgurim.DAL
    /// Fecha: 11 de abril de 2008
    /// Autor: Juan Carlos González Cardona  
    /// </summary>
    
    public class GenericProvider
    {
        private string _connectionStringName;
        private string _connectionString;
        private string _provider;
        private int _commandTimeout;

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string ConnectionString
        {
            get
            {return _connectionString;}
            set
            {_connectionString = value;}
        }

        public string Provider
        {
            get
            { return _provider; }
            set
            { _provider = value; }
        }

        public string ConnectionStringName
        {
            get
            { return _connectionStringName; }
            set
            { 
                _connectionStringName = value;
                ConnectionString = ConfigurationManager.ConnectionStrings[value].ConnectionString;
                Provider = ConfigurationManager.ConnectionStrings[value].ProviderName;
            }
        }

        public int CommandTimeout
        {
            get
            { return _commandTimeout; }
            set
            { _commandTimeout = value; }
        }

        public DbProviderFactory dpf
        {
            get
            { return DbProviderFactories.GetFactory(Provider); }
        }



        public GenericProvider()
        {

        }

        public GenericProvider(string ConnectionStringName)
        {
            this.ConnectionStringName = ConnectionStringName;
            int CommandTimeoutTemp = 180;
            if (ConfigurationManager.AppSettings["CommandTimeout"] != null)
                CommandTimeoutTemp = int.Parse(ConfigurationManager.AppSettings["CommandTimeout"]);
            this.CommandTimeout = CommandTimeoutTemp;
        }

        public GenericProvider(string ConnectionStringName, int CommandTimeout)
        {
            this.ConnectionStringName = ConnectionStringName;
            this.CommandTimeout = CommandTimeout;
        }

        /// <summary>
        /// Método: GetDBParameter
        /// Descripción: Retorna un parámetro con tipo de dato String, si es nulo, con valor DBNull.
        /// </summary>
        // Fecha: 14 de abril de 2008
        // Autor: Juan Carlos González Cardona
        public DbParameter GetDBParameter(string name, string value)
        {
            DbParameter par;

            par = dpf.CreateParameter();
            
            par.ParameterName = name;

            if (value != null)
                par.Value = value;
            else
                par.Value = DBNull.Value;

            return par;
        }

        /// <summary>
        /// Método: GetDBParameter
        /// Descripción: Retorna un parámetro con tipo de dato int, si es nulo, con valor DBNull.
        /// </summary>
        // Fecha: 14 de abril de 2008
        // Autor: Juan Carlos González Cardona
        public DbParameter GetDBParameter(string name, int? value)
        {
            DbParameter par;

            par = dpf.CreateParameter();

            par.ParameterName = name;

            if (value != null)
                par.Value = value;
            else
                par.Value = DBNull.Value;

            return par;
        }

        /// <summary>
        /// Método: GetDBParameter
        /// Descripción: Retorna un parámetro con tipo de dato float, si es nulo, con valor DBNull.
        /// </summary>
        // Fecha: 14 de abril de 2008
        // Autor: Juan Carlos González Cardona
        public DbParameter GetDBParameter(string name, float? value)
        {
            DbParameter par;

            par = dpf.CreateParameter();

            par.ParameterName = name;

            if (value != null)
                par.Value = value;
            else
                par.Value = DBNull.Value;

            return par;
        }

        /// <summary>
        /// Método: GetDBParameter
        /// Descripción: Retorna un parámetro con tipo de dato DateTime, si es nulo, con valor DBNull.
        /// </summary>
        // Fecha: 14 de abril de 2008
        // Autor: Juan Carlos González Cardona
        public DbParameter GetDBParameter(string name, DateTime? value)
        {
            DbParameter par;

            par = dpf.CreateParameter();

            par.ParameterName = name;

            if (value != null)
                par.Value = value;
            else
                par.Value = DBNull.Value;

            return par;
        }

        /// <summary>
        /// Método: GetDBParameter
        /// Descripción: Retorna un parámetro con tipo de dato bool, si es nulo, con valor DBNull.
        /// </summary>
        // Fecha: 14 de abril de 2008
        // Autor: Juan Carlos González Cardona
        public DbParameter GetDBParameter(string name, bool? value)
        {
            DbParameter par;

            par = dpf.CreateParameter();

            par.ParameterName = name;

            if (value != null)
                par.Value = value;
            else
                par.Value = DBNull.Value;

            return par;
        }

        /// <summary>
        /// Método: GetDBParameter
        /// Descripción: Retorna un parámetro con tipo de dato long, si es nulo, con valor DBNull.
        /// </summary>
        // Fecha: 14 de abril de 2008
        // Autor: Juan Carlos González Cardona
        public DbParameter GetDBParameter(string name, long? value)
        {
            DbParameter par;

            par = dpf.CreateParameter();

            par.ParameterName = name;

            if (value != null)
                par.Value = value;
            else
                par.Value = DBNull.Value;

            return par;
        }

        /// <summary>
        /// Método: GetDBParameterReturnValue
        /// Descripción: Retorna el parámetro que captura el returnvalue de un SP
        /// </summary>
        // Fecha: 14 de abril de 2008
        // Autor: Juan Carlos González Cardona
        public DbParameter GetDBParameterReturnValue()
        {
            DbParameter par;

            par = dpf.CreateParameter();
            par.ParameterName = "@RETURN_VALUE";
            par.DbType = DbType.Int32;
            par.Direction = ParameterDirection.ReturnValue;

            return par;
        }


        /// <summary>
        /// Método: GetTable
        /// Descripción: retorna una tabla de acuerdo a una consulta usando factory
        /// Ejemplo:
        ///     string sql = @"SELECT * FROM tabla WHERE campo1 = @campo1 AND campo2 = @campo2";
        ///     GPClase.GenericProvider gp = new GPClase.GenericProvider(EasyConfigH.getString("SETGROUP", "ConnectionStringAPP"));
        ///     return  gp.GetTable(sql, CommandType.Text
        ///                     , gp.GetDBParameter("@campo1", variable1)
        ///                     , gp.GetDBParameter("@campo2", variable2)
        ///                     );
        /// </summary>
        // Fecha: 14 de abril de 2008
        // Autor: Juan Carlos González Cardona
        public DataTable GetTable(string selectCommand
                                        , CommandType selectType
                                        , params DbParameter[] parameters)
        {
            if (log.IsInfoEnabled) log.Info(selectCommand + " " + parametersToString(parameters));
            DataTable dt = null;
            
            try
            {
                using (DbConnection con = dpf.CreateConnection())
                {
                    con.ConnectionString = ConnectionString;

                    using (
                        DbCommand cmd = dpf.CreateCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = selectCommand;
                        cmd.CommandType = selectType;
                        cmd.CommandTimeout = CommandTimeout;

                        foreach (DbParameter param in parameters)
                            cmd.Parameters.Add(param);

                        using(DbDataAdapter ada = dpf.CreateDataAdapter())
                        using(dt = new DataTable())
                        {
                            ada.SelectCommand = cmd;
                            con.Open();
                            
                            ada.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled) log.Error(selectCommand + " " + parametersToString(parameters), e);
                Console.Write(e.Message);
            }
            return dt;
        }

        /// <summary>
        /// Método: ExecuteNonQuery
        /// Descripción: Executa un comando que no retorna datos
        /// </summary>
        // Fecha: 14 de abril de 2008
        // Autor: Juan Carlos González Cardona
        public int ExecuteNonQuery(string selectCommand
                                        , CommandType selectType
                                        , params DbParameter[] parameters)
        {
            if (log.IsInfoEnabled) log.Info(selectCommand + " " + parametersToString(parameters));

            int rowsAffected = 0;

            try
            {
                using (DbConnection con = dpf.CreateConnection())
                {
                    con.ConnectionString = ConnectionString;

                    using (DbCommand cmd = dpf.CreateCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = selectCommand;
                        cmd.CommandType = selectType;
                        cmd.CommandTimeout = CommandTimeout;

                        foreach (DbParameter param in parameters)
                            cmd.Parameters.Add(param);
                        
                        con.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled) log.Error(selectCommand + " " + parametersToString(parameters), e);
                throw e;
            }
            return rowsAffected;
        }

        public Object GetScalar(string selectCommand
                                        , CommandType selectType
                                        , params DbParameter[] parameters)
        {
            if (log.IsInfoEnabled) log.Info(selectCommand + " " + parametersToString(parameters));

            Object scalar = null;
            try
            {
                using (DbConnection con = dpf.CreateConnection())
                {
                    con.ConnectionString = ConnectionString;

                    using (DbCommand cmd = dpf.CreateCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = selectCommand;
                        cmd.CommandType = selectType;
                        cmd.CommandTimeout = CommandTimeout;

                        foreach (DbParameter param in parameters)
                            cmd.Parameters.Add(param);

                        con.Open();
                        
                        scalar = cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled) log.Error(selectCommand + " " + parametersToString(parameters), e);
            }
            return scalar;
        }

        public Object GetXmlReader(string selectCommand
                                        , CommandType selectType
                                        , params DbParameter[] parameters)
        {
            if (log.IsInfoEnabled) log.Info(selectCommand + " " + parametersToString(parameters));

            System.Xml.XmlReader xml = null;
            System.Text.StringBuilder resultado= new System.Text.StringBuilder();
            try
            {
                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection())
                {
                    con.ConnectionString = ConnectionString;

                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = selectCommand;
                        cmd.CommandType = selectType;
                        cmd.CommandTimeout = CommandTimeout;

                        foreach (DbParameter param in parameters)
                            cmd.Parameters.Add(param);

                        con.Open();

                        xml = cmd.ExecuteXmlReader();
                        xml.Read();
                        while (!xml.EOF)
                        {
                            resultado.Append(xml.ReadOuterXml());
                        }
                        xml.Close();
                    }
                }
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled) log.Error(selectCommand + " " + parametersToString(parameters), e);
            }
            return resultado.ToString();
        }

        public string parametersToString(params DbParameter[] parameters)
        {
            System.Text.StringBuilder par = new System.Text.StringBuilder();
            foreach (DbParameter param in parameters)
            {
                if (param.Direction != ParameterDirection.ReturnValue)
                {
                    par.Append(param.ParameterName + "=" + param.Value.ToString() + "|");
                }
            }

            return par.ToString();
        }


    }
}