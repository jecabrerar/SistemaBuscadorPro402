using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Respositories
{
    public class LoginRepository
    {
        public bool UserExist(string usuario, string password)
        {
            var resultado = false;
            string connectionstring = @"server=DESKTOP-T8N6TK6\SQLEXPRESS; database=PRO402BD; Integrated Security=true;";
            //string query = string.Format("select COUNT(1) from usuarios where usuario = '{0}' and password = '{1}'", usuario, password);
            string query = "SP_CHECK_USER";
            using SqlConnection sql = new SqlConnection(connectionstring);
            using SqlCommand cmd = new SqlCommand(query, sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add( new SqlParameter("@user", usuario));
            cmd.Parameters.Add(new SqlParameter("@password",  password));

            sql.Open();
            int bdResult = (int)cmd.ExecuteScalar();

            if (bdResult > 0)
            {
                resultado = true;
            }            

            return resultado;
        }
    }
}
