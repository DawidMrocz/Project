using Aplikacja.DTOS.CatDto;
using Aplikacja.Entities.CatModels;
using Aplikacja.Repositories.CatRepository;
using Aplikacja.Repositories.UserRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aplikacja.Controllers
{
    [Authorize]
    public class CatController : Controller
    {
        private readonly ICatRepository _catRepository;
        private readonly IMapper _mapper;

        public CatController(ICatRepository catRepository, IMapper mapper)
        {
            _catRepository = catRepository ?? throw new ArgumentNullException(nameof(catRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<CatDTO>> Cat(int id)
        {
            CatDTO myCat = _mapper.Map<CatDTO>(await _catRepository.GetCat(id));
            return View(myCat);
        }

        [HttpGet]
        public async Task<ActionResult<List<CatDTO>>> Cats()
        {
            List<CatDTO> myCats = _mapper.Map<List<CatDTO>>(await _catRepository.GetCats());
            return View(myCats);
        }
    }
}
