using System.Linq;
using Matrix.PA.Models;
using Matrix.PA.Models.Api;
using Matrix.PA.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Matrix.PA.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IRepository<UserModel> _repositoryUser;

        public UserController(IRepository<UserModel> repositoryUser)
        {
            this._repositoryUser = repositoryUser;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _repositoryUser.All().Select(l => l.ToApi()).ToList();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(long id)
        {
            var model = _repositoryUser.ById(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model.ToApi());
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserApi userApi)
        {
            if (ModelState.IsValid)
            {
                var userModel = userApi.ToModel();
                userModel.Id = 0;

                if (_repositoryUser.Save(userModel))
                    return Created($"/api/user/{userModel.Id}", userModel.ToApi());
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserApi userApi)
        {
            if (ModelState.IsValid)
            {
                var userModel = userApi.ToModel();
                _repositoryUser.Save(userModel);
                return Ok();
            }
            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var model = _repositoryUser.ById(id);
            if (model == null)
            {
                return NotFound();
            }
            _repositoryUser.Delete(model.Id);
            return NoContent();
        }


    }
}
