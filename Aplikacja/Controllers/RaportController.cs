using Aplikacja.DTOS.RaportDto;
using Aplikacja.Entities.RaportModels;
using Aplikacja.Repositories.RaportRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aplikacja.Controllers
{
    [Authorize]
    public class RaportController : Controller
    {
        private readonly IRaportRepository _raportRepository;

        public RaportController(IRaportRepository raportRepository)
        {
            _raportRepository = raportRepository;
        }

        [HttpGet]
        public async Task<ActionResult<RaportDTO>> Raport([FromRoute] int id)
        {
            var myRaport = await _raportRepository.GetRaport(id);
            return View(myRaport);
        }

        [HttpGet]
        public async Task<ActionResult<List<Raport>>> Raports()
        {
            var myRaports = await _raportRepository.GetRaports();
            return View(myRaports);
        }
    }
}
