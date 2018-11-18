using Demo.Model;
using System.Collections.Generic;

namespace Demo.DataAccess
{
    public interface ITodoDataAccess
    {
        List<TodoModel> GetAll(string connectString);
        TodoModel GetById(string connectString, int id);
        void Create(string connectString, TodoModel todoModel);
        void Update(string connectString, TodoModel todoModel);
        void Delete(string connectString, TodoModel todoModel);
    }
}
