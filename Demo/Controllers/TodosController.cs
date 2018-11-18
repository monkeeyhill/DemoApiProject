using Demo.Businesslogic;
using Demo.Model;
using Demo.Response;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : Controller
    {
        private readonly ITodoBusinesslogic _iTodoBusinesslogic;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="iTodoBusinesslogic"></param>
        public TodosController(ITodoBusinesslogic iTodoBusinesslogic)
        {
            _iTodoBusinesslogic = iTodoBusinesslogic;
        }

        #region Api
        // GET api/todos
        [HttpGet]
        public BaseListResponse<TodoModel> Get()
        {
            var response = new BaseListResponse<TodoModel>();
            response = _iTodoBusinesslogic.GetAll();
            return response;
        }

        // GET api/todos/5
        [HttpGet("{id}")]
        public BaseResponse<TodoModel> Get(int id)
        {
            var response = new BaseResponse<TodoModel>();
            response = _iTodoBusinesslogic.GetById(id);
            return response;
        }

        // Post api/todos
        [HttpPost()]
        public BaseResponse<string> Post(TodoModel todoModel)
        {
            var response = new BaseResponse<string>();
            response = _iTodoBusinesslogic.Create(todoModel);
            return response;
        }

        // PUT api/todos
        [HttpPut()]
        public BaseResponse<string> Put(TodoModel todoModel)
        {
            var response = new BaseResponse<string>();
            response = _iTodoBusinesslogic.Update(todoModel);
            return response;
        }

        // DELETE api/values
        [HttpDelete()]
        public BaseResponse<string> Delete(TodoModel todoModel)
        {
            var response = new BaseResponse<string>();
            response = _iTodoBusinesslogic.Delete(todoModel);
            return response;
        }
        #endregion
    }
}
