﻿using ADO_Lesson_1.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Lesson_1.Services
{
    public class SqlService
    {
        public static bool ExecuteNonQuery(string query)
        {
            try
            {
                using (SqlConnection conn = new(App.Options.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception) { return false; }
            return true;
        }

        public static void ExecuteNonQuerys(string querys)
        {
            try
            {
                var fileContent = querys;
                var sqlqueries = fileContent.Split(new[] { " GO " }, StringSplitOptions.RemoveEmptyEntries);

                SqlConnection con = new SqlConnection(App.Options.ConnectionString);
                SqlCommand cmd = new SqlCommand("query", con);
                con.Open();
                foreach (var query in sqlqueries)
                {
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (Exception) { }
        }



        public static void ExecuteReader(string query, ref SqlDataReader reader)
        {
            try
            {
                using (SqlConnection conn = new(App.Options.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    reader = cmd.ExecuteReader();
                }
            }
            catch (Exception) { }

        }
    }
}
