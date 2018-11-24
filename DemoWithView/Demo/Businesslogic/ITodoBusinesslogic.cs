using Demo.Models;
using Demo.Response;

namespace Demo.Businesslogic
{
    public interface ITodoBusinesslogic
    {
        BaseListResponse<TodoModel> GetAll();
        BaseResponse<TodoModel> GetById(int id);
        BaseResponse<string> Create(TodoModel todoModel);
        BaseResponse<string> Update(TodoModel todoModel);
        BaseResponse<string> Delete(TodoModel todoModel);
    }
}
