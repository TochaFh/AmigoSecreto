using AmigoSecreto.Models;
using AmigoSecreto.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using FastConsole;
using System.Threading.Tasks;

namespace AmigoSecreto.Controllers
{
    public class SiteController : Controller
    {
        private readonly ILogger<SiteController> _logger;

        private readonly IAmigoSecretoService _service;

        public SiteController(ILogger<SiteController> logger, IAmigoSecretoService service)
        {
            _logger = logger;
            _service = service;
        }

        [Route("/")]
        public IActionResult Index(string model)
        {
            return View();
        }

        [Route("NewRoom")]
        public IActionResult NewRoom()
        {
            return View();
        }

        [Route("Created/{id:int}")]
        public async Task<IActionResult> CreatedRoom(int id)
        {
            BasicRoom room = await _service.GetBasicRoom(id);

            if (room == null)
            {
                return RedirectToAction("PageNotFound");
            }

            string roomUrl = Url.RouteUrl("room", new { id }, Request.Scheme);

            return View(new CreatedRoomViewModel(roomUrl, room.Title));
        }

        [Route("Room/{id:int}/{errorId:int?}", Name = "room")]
        public async Task<IActionResult> Room(int id, int? errorId = null)
        {
            BasicRoom room = await _service.GetBasicRoom(id);

            if (room == null)
            {
                return RedirectToAction("PageNotFound");
            }

            return View(new RoomViewModel(room, errorId));
        }

        [Route("SecretFriend/{roomId:int}/{personId:int}")]
        public async Task<IActionResult> SecretFriend(int roomId, int personId)
        {
            var secret = await _service.GetSecretFriend(roomId, personId);

            if (secret.Result == Sfr.AlreadyRevelead)
            {
                return RedirectToAction("Room", new { id = roomId, errorId = secret.Person.Id });
            }
            if (secret.Result == Sfr.NotFound)
            {
                return RedirectToAction("PageNotFound");
            }

            return View(secret);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("NotFound")]
        public IActionResult PageNotFound()
        {
            return View("Error", new ErrorViewModel());
        }
    }
}
