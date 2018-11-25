using Demo.Businesslogic;
using Demo.Models;
using Demo.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace Demo.Controllers
{
    public class TodosController : ApiController
    {
        #region constant
        private string connectstring = "MyConnectionStr";
        #endregion

        //private readonly ITodoBusinesslogic _iTodoBusinesslogic;


        ///// <summary>
        ///// Contructor
        ///// </summary>
        ///// <param name="iTodoBusinesslogic"></param>
        //public TodosController(ITodoBusinesslogic iTodoBusinesslogic)
        //{
        //    _iTodoBusinesslogic = iTodoBusinesslogic;
        //}

        #region Api
        // GET api/todos
        public BaseListResponse<TodoModel> Get()
        {
            var response = new BaseListResponse<TodoModel>();
            //response = _iTodoBusinesslogic.GetAll();
            //return response;

            string connectString = ConfigurationManager.ConnectionStrings[connectstring].ConnectionString;
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
                md.Name = reader.GetValue(2).ToString();
                md.Description = reader.GetValue(1).ToString();
                if (!string.IsNullOrEmpty(reader.GetValue(5).ToString()))
                {
                    md.Ordering = Convert.ToInt32(reader.GetValue(5));
                }

                listModel.Add(md);
            }
            response.Data = listModel;
            response.Result = true;
            response.Message = "get data success";
            response.TotalRecord = listModel.Count;
            return response;
        }

        public BaseListResponse<TodoModel> Get(string search)
        {
            var response = new BaseListResponse<TodoModel>();
            //response = _iTodoBusinesslogic.GetAll();
            //return response;

            string connectString = ConfigurationManager.ConnectionStrings[connectstring].ConnectionString;
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connectString;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select * from Todo where DeleteDate IS NULL AND Name LIKE '%" + search + "%'";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            TodoModel md = null;
            var listModel = new List<TodoModel>();
            while (reader.Read())
            {
                md = new TodoModel();
                md.Id = Convert.ToInt32(reader.GetValue(0));
                md.Name = reader.GetValue(2).ToString();
                md.Description = reader.GetValue(1).ToString();
                if (!string.IsNullOrEmpty(reader.GetValue(5).ToString()))
                {
                    md.Ordering = Convert.ToInt32(reader.GetValue(5));
                }

                listModel.Add(md);
            }
            response.Data = listModel;
            response.Result = true;
            response.Message = "get data success";
            response.TotalRecord = listModel.Count;
            return response;
        }

        // GET api/todos/5
        public BaseResponse<TodoModel> Get(int id)
        {
            var response = new BaseResponse<TodoModel>();
            //response = _iTodoBusinesslogic.GetById(id);

            string connectString = ConfigurationManager.ConnectionStrings[connectstring].ConnectionString;
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
                md.Name = reader.GetValue(2).ToString();
                md.Description = reader.GetValue(1).ToString();
                if (!string.IsNullOrEmpty(reader.GetValue(5).ToString()))
                {
                    md.Ordering = Convert.ToInt32(reader.GetValue(5));
                }
            }

            response.Result = true;
            response.Message = "get data success";
            response.Data = md;
            return response;
        }

        // Post api/todos
        public BaseResponse<string> Post(TodoModel todoModel)
        {
            var response = new BaseResponse<string>();
            //response = _iTodoBusinesslogic.Create(todoModel);

            string connectString = ConfigurationManager.ConnectionStrings[connectstring].ConnectionString;
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

            if (rowInserted > 0 )
            {
                response.Result = true;
                response.Message = "Insert Success!";
            }
            return response;
        }

        // PUT api/todos
        public BaseResponse<string> Put(TodoModel todoModel)
        {
            var response = new BaseResponse<string>();
            //response = _iTodoBusinesslogic.Update(todoModel);

            string connectString = ConfigurationManager.ConnectionStrings[connectstring].ConnectionString;
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
            if (rowInserted > 0)
            {
                response.Result = true;
                response.Message = "Update Success!";
            }
            return response;
        }

        // DELETE api/values
        public BaseResponse<string> Delete(TodoModel todoModel)
        {
            var response = new BaseResponse<string>();
            //response = _iTodoBusinesslogic.Delete(todoModel);

            string connectString = ConfigurationManager.ConnectionStrings[connectstring].ConnectionString;
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
            if (rowInserted > 0)
            {
                response.Result = true;
                response.Message = "Delete Success!";
            }
            return response;
        }
        #endregion
    }
}
