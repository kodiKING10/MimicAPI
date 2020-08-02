using Microsoft.EntityFrameworkCore;
using MimicAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MimicAPI.Database
{
    public class MimicContext : DbContext
    {
        public MimicContext(DbContextOptions<MimicContext>options) :base(options)
        {

        }

        //public void ExecuteWithoutReturn(string strQuery)
        //{
        //    var cmdComand = new SqlCommand
        //    {
        //        CommandText = strQuery,
        //        CommandType = CommandType.Text,
        //        Connection = connection
        //    };

        //    cmdComand.ExecuteNonQuery();
        //}

        //public SqlDataReader ExecuteWithReturn(string strQuery)
        //{
        //    var cmdComandSelect = new SqlCommand(strQuery, connection);
        //    return cmdComandSelect.ExecuteReader();
        //}

        public DbSet<Palavra> Palavras { get; set; }
    }
}
