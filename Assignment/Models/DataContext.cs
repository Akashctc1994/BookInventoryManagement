using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Assignment.Models
{
    public class DataContext
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;

        public List<Books> GetBook()
        {
            List<Books> objLst = new List<Books>();
            string query = "select * from BIM";
            da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                Books obj = new Books();
                obj.Id = (int)item["Id"];
                obj.Titles = (string)item["Titles"];
                obj.Author = (string)item["Author"];
                obj.ISBN = (long)item["ISBN"];
                obj.Publisher = (string)item["Publisher"];
                obj.Year = (int)item["Year"];
                objLst.Add(obj);
            }
            return objLst;
        }

        public Books GetBooks(int n)
        {
            Books obj = new Books();
            string query = "select * from BIM where Id =" + n;
            cmd = new SqlCommand(query, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                dr.Read();
                obj.Id = (int)dr["Id"];
                obj.Titles = (string)dr["Titles"];
                obj.Author = (string)dr["Author"];
                obj.ISBN = (long)dr["ISBN"];
                obj.Publisher = (string)dr["Publisher"];
                obj.Year = (int)dr["Year"];
            }
            con.Close();
            return obj;
        }
        public List<Books> SearchBooks(string Titles, string Author, long ISBN)
        {
            cmd = new SqlCommand("select * from BIM where Titles = '"+Titles+"' or Author = '"+Author+"' or ISBN = " + ISBN, con);
            List<Books> objLst = new List<Books>();
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                Books obj = new Books();
                obj.Id = (int)item["Id"];
                obj.Titles = (string)item["Titles"];
                obj.Author = (string)item["Author"];
                obj.ISBN = (long)item["ISBN"];
                obj.Publisher = (string)item["Publisher"];
                obj.Year = (int)item["Year"];
                objLst.Add(obj);
            }
            return objLst;
        }

        private void ParameterMethod(Books obj, string query)
        {
            cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@p1", obj.Id);
            cmd.Parameters.AddWithValue("@p2", obj.Titles);
            cmd.Parameters.AddWithValue("@p3", obj.Author);
            cmd.Parameters.AddWithValue("@p4", obj.ISBN);
            cmd.Parameters.AddWithValue("@p5", obj.Publisher);
            cmd.Parameters.AddWithValue("@p6", obj.Year);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void InsertRec(Books obj)
        {
            string query = "insert BIM (Titles,Author,ISBN,Publisher,Year) values(@p2,@p3,@p4,@p5,@p6)";
            ParameterMethod(obj, query);
        }
        public void updateRec(Books obj)
        {
            string query = "update BIM set Titles=@p2,Author=@p3,ISBN=@p4,Publisher=@p5,Year=@p6 where id = @p1";
            ParameterMethod(obj, query);
        }

        public void DeleteRec(int id)
        {
            string query = "Delete from BIM where id=" + id;
            cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
    }
}