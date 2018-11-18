using Demo.DataAccess;
using Demo.Model;
using Demo.Response;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Demo.Businesslogic
{
    public class TodoBusinesslogic : ITodoBusinesslogic
    {
        #region constant
        private string connectstring = "ConnectionString";
        #endregion

        #region private
        private ITodoDataAccess _iTodoDataAccess;
        #endregion

        #region public
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="todoDataAccess"></param>
        public TodoBusinesslogic(ITodoDataAccess todoDataAccess)
        {
            _iTodoDataAccess = todoDataAccess;
        }

        /// <summary>
        /// Get all record
        /// </summary>
        /// <returns></returns>
        public BaseListResponse<TodoModel> GetAll()
        {
            string connStr = ConfigurationManager.ConnectionStrings[connectstring].ConnectionString;
            var listModel = new List<TodoModel>();
            listModel = _iTodoDataAccess.GetAll(connStr);
            return new BaseListResponse<TodoModel>
            {
                Result = true,
                Data = listModel,
                Message = "get data success",
                TotalRecord = listModel.Count()
            };
        }

        /// <summary>
        /// Get record by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseResponse<TodoModel> GetById(int id)
        {
            string connStr = ConfigurationManager.ConnectionStrings[connectstring].ConnectionString;
            var model = new TodoModel();
            model = _iTodoDataAccess.GetById(connStr,id);
            return new BaseResponse<TodoModel>
            {
                Result = true,
                Data = model,
                Message = "get data success"
            };
        }

        /// <summary>
        /// Insert record
        /// </summary>
        /// <param name="todoModel"></param>
        /// <returns></returns>
        public BaseResponse<string> Create(TodoModel todoModel)
        {
            string connStr = ConfigurationManager.ConnectionStrings[connectstring].ConnectionString;
            _iTodoDataAccess.Create(connStr, todoModel);
            return new BaseResponse<string>
            {
                Result = true,
                Message = "Insert Success!"
            };
        }

        /// <summary>
        /// Update record
        /// </summary>
        /// <param name="todoModel"></param>
        /// <returns></returns>
        public BaseResponse<string> Update(TodoModel todoModel)
        {
            string connStr = ConfigurationManager.ConnectionStrings[connectstring].ConnectionString;
            _iTodoDataAccess.Update(connStr, todoModel);
            return new BaseResponse<string>
            {
                Result = true,
                Message = "Update Success!"
            };
        }

        /// <summary>
        /// Delete record : update field DeleteDate
        /// </summary>
        /// <param name="todoModel"></param>
        /// <returns></returns>
        public BaseResponse<string> Delete(TodoModel todoModel)
        {
            string connStr = ConfigurationManager.ConnectionStrings[connectstring].ConnectionString;
            _iTodoDataAccess.Delete(connStr, todoModel);
            return new BaseResponse<string>
            {
                Result = true,
                Message = "Delete Success!"
            };
        }

        #endregion
    }
}
