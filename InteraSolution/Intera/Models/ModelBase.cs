using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Intera.Models
{
    public abstract class ModelBase : IDisposable
    {
        protected SqlConnection connection = null;

        public ModelBase()
        {
            string strConn = @"Data Source=localhost; Initial Catalog=BDIntera; Integrated Security=true";
            connection = new SqlConnection(strConn);
                connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}