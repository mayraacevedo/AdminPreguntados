using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using ProyectoG.Context;



namespace ProyectoG.Basedatos
{
    public class BaseDatos
    {
        public GameContext contexto= new GameContext();
        public string Error { get; set; }
        
        public DataTable ObtenerDatos(string consulta)
        {
            
            DataTable table = new DataTable();
           
            using (IDbCommand oaCommand = contexto.Database.Connection.CreateCommand())
            {
                oaCommand.CommandTimeout = 2200;

                oaCommand.CommandText = consulta;
                oaCommand.Connection.Open();

                using (IDataReader reader = oaCommand.ExecuteReader())
                {
                    table.Load(reader);
                }

                oaCommand.Connection.Close();
            }
            return table;
        }

    
      
        public bool EjecutarQuery(string consulta)
        {
            return EjecutarQuery(consulta, true);
        }
        public bool EjecutarQuery(string consulta, bool conTransaccion)
        {
            bool resultado = true;

            consulta.Replace("\r\n", " ");
            using (var connection = contexto.Database.Connection)
            {
                
                //IDbTransaction transaction;
                connection.Open();

                DbTransaction transaction = null;
                if (conTransaccion)
                    transaction = connection.BeginTransaction();

                try
                {
                    IDbCommand oaCommand;
                    oaCommand = connection.CreateCommand();
                    if (transaction != null)
                        oaCommand.Transaction = transaction;
                    oaCommand.CommandText = consulta;
                    oaCommand.CommandTimeout = 60 * 30;
                    oaCommand.ExecuteNonQuery();
                    if (transaction != null)
                        transaction.Commit();
                    connection.Close();
                    resultado = true;
                }
                catch (Exception e)
                {
                    Error = e.Message;
                    if (transaction != null)
                        transaction.Rollback();
                    resultado = false;

                }
            }
            return resultado;
        }

     
       
    }
}
