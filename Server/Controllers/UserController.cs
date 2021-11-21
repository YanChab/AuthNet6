using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using AuthNet6.Server.Services;
using AuthNet6.Shared;



namespace AuthNet6.Server.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IMongoCollection<User> _Users;
        public UserController(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _Users = database.GetCollection<User>(settings.CollectionName);
        }
        [HttpGet]
        [Route("api/user")]
        public async Task<List<User>> GetUser()
        {
            try
            {
                return await _Users.Find(_ => true).ToListAsync().ConfigureAwait(false);
            }
            catch
            {
                throw;
            }
        }
        [HttpPost]
        [Route("api/user/create")]
        public async Task CreateUser(User user)
        {
            try
            {
                await _Users.InsertOneAsync(user);
            }
            catch
            {
                throw;

            }

        }
        [HttpDelete]
        [Route("api/user/delete/{id}")]
        public async Task DeleteUser(string id){
            try
            {
                FilterDefinition<User> user = Builders<User>.Filter.Eq("Id", id);
                await _Users.DeleteOneAsync(user);
                
            }
            catch
            {
                throw;
            }
        }
    }
}