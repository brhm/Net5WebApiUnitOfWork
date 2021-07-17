using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Net5WebApiUnitOfWork.Core.IConfiguration;
using Net5WebApiUnitOfWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5WebApiUnitOfWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(ILogger<UsersController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _unitOfWork.Users.All();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var item = await _unitOfWork.Users.GetById(id);
            if (item == null)
                return NotFound();
            
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            if(ModelState.IsValid)
            {
                user.Id = Guid.NewGuid();
                await _unitOfWork.Users.Add(user);
                await _unitOfWork.ComplateAsync();
                return CreatedAtAction("GetItem", new { user.Id },user);

            }
            return new JsonResult("Something Went wrong") { StatusCode=500};
        }

       [HttpPut("{id}")]
       public async Task<IActionResult> UpdateItem(Guid id,User user)
        {
            if (id != user.Id)
                return BadRequest();
            
                    await _unitOfWork.Users.Update(user);
            await _unitOfWork.ComplateAsync();

            //Following up the Rest standart on update we need to return NoContent
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _unitOfWork.Users.GetById(id);

            if (item == null)
                return BadRequest();
            await _unitOfWork.Users.Delete(id);
            await _unitOfWork.ComplateAsync();
            
            return Ok(item);
        }

    }
}
