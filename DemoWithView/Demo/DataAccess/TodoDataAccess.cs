using Demo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Demo.DataAccess
{
    public class TodoDataAccess : ITodoDataAccess
    {
        /// <summary>
        /// Get all record
        /// </summary>
        /// <param name="connectString"></param>
        /// <returns></returns>
        public List<TodoModel> GetAll(string connectString)
        {           

            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connectString;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select * from Todo where DeleteDate IS NULL";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            TodoModel md = null;
            var listModel = new List<TodoModel>();
            while (reader.Read())
            {
                md = new TodoModel();
                md.Id = Convert.ToInt32(reader.GetValue(0));
                md.Name = reader.GetValue(1).ToString();
                md.Description = reader.GetValue(2).ToString();
                if (!string.IsNullOrEmpty(reader.GetValue(5).ToString()))
                {
                    md.Ordering = Convert.ToInt32(reader.GetValue(5));
                }

                listModel.Add(md);
            }
            return listModel;
        }

        /// <summary>
        /// Get record by id
        /// </summary>
        /// <param name="connectString"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public TodoModel GetById(string connectString, int id)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connectString;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select * from Todo where Id=" + id + " AND DeleteDate IS NULL";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            TodoModel md = null;
            while (reader.Read())
            {
                md = new TodoModel();
                md.Id = Convert.ToInt32(reader.GetValue(0));
                md.Name = reader.GetValue(1).ToString();
                md.Description = reader.GetValue(2).ToString();
                if (!string.IsNullOrEmpty(reader.GetValue(5).ToString()))
                {
                    md.Ordering = Convert.ToInt32(reader.GetValue(5));
                }
            }

            return md;
        }

        /// <summary>
        /// Create new record
        /// </summary>
        /// <param name="connectString"></param>
        /// <param name="todoModel"></param>
        public void Create(string connectString, TodoModel todoModel)
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connectString;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO Todo (Name,Description,Ordering,CreateDate) Values (@Name,@Description,@Ordering,@CreateDate)";
            sqlCmd.Connection = myConnection;

            var createDate = DateTime.Now;
            sqlCmd.Parameters.AddWithValue("@Name", todoModel.Name);
            sqlCmd.Parameters.AddWithValue("@Description", todoModel.Description);
            sqlCmd.Parameters.AddWithValue("@Ordering", todoModel.Ordering);
            sqlCmd.Parameters.AddWithValue("@CreateDate", createDate);
            myConnection.Open();
            int rowInserted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }

        /// <summary>
        /// Update record
        /// </summary>
        /// <param name="connectString"></param>
        /// <param name="todoModel"></param>
        public void Update(string connectString, TodoModel todoModel)
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connectString;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Update Todo Set Name = @Name ,Description = @Description,Ordering = @Ordering, UpdateDate = @UpdateDate Where Id = " + todoModel.Id;
            sqlCmd.Connection = myConnection;

            var updateDate = DateTime.Now;
            sqlCmd.Parameters.AddWithValue("@Name", todoModel.Name);
            sqlCmd.Parameters.AddWithValue("@Description", todoModel.Description);
            sqlCmd.Parameters.AddWithValue("@Ordering", todoModel.Ordering);
            sqlCmd.Parameters.AddWithValue("@UpdateDate", updateDate);
            myConnection.Open();
            int rowInserted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }

        /// <summary>
        /// Delete Record : update field DeleteDate
        /// </summary>
        /// <param name="connectString"></param>
        /// <param name="todoModel"></param>
        public void Delete(string connectString, TodoModel todoModel)
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connectString;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Update Todo set DeleteDate = @DeleteDate Where Id = " + todoModel.Id;
            sqlCmd.Connection = myConnection;

            var deleteDate = DateTime.Now;
            sqlCmd.Parameters.AddWithValue("@DeleteDate", deleteDate);
            myConnection.Open();
            int rowInserted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }
    }
}
