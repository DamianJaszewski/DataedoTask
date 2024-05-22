using DataedoTask.Models;
using DataedoTask.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DataedoTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //1. Użyj protokołu HttpDelete aby controller był zgodny z zasadami REST.
        [HttpDelete("delete/{id}")]
        //2. Użyj typu zwracanego ActionResult w przypadku kontrolera, obecnie kompilator generuje błąd.
        //3. Wykorzystaj asynchroniczność aby pozwolić na wykonywanie innych zapytań i odciążyć serwer.
        public async Task<ActionResult> Delete(uint id)
        {

            //4. Dodaj block try catch w celu przechwycenia błędów np. problemu z bazą danych
            try
            {
                //5. Wykorzystaj dodatkową klasę UserService to pomoże zaimplementowć wzorce projektowe SOLID.
                //W szczególnośc Single Responsible oraz Inversion of Control w przypadku zastosowania Interfejsu.
                //Wykorzystanie interfejsu jest dobrą praktyką np. w przypadku testów jednostkowych.
                //Nowe klasy zostaną zaimplementowane z użyciem wstrzykiwania zależności.
                User user = await _userService.GetUserByIdAsync(id);

                //6. Dodaj informacje w przypadku braku użytkownika w bazie danych. 
                if (user == null) return BadRequest("User not found");

                await _userService.DeleteUser(user);

                Debug.WriteLine($"The user with Login={user.Name} has been deleted.");
            }
            catch (Exception ex )
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
