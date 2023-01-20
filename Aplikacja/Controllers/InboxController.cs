using Aplikacja.DTOS.InboxDtos;
using Aplikacja.Entities.InboxModel;
using Aplikacja.Repositories.CatRepository;
using Aplikacja.Repositories.RaportRepository;
using Aplikacja.Repositories.UserRepository;
using InboxMicroservice.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Aplikacja.Controllers
{
    [Authorize]
    public class InboxController : Controller
    {
        private readonly IInboxRepository _inboxRepository;
        private readonly ICatRepository _catRepository;
        private readonly IRaportRepository _raportRepository;

        public InboxController(IInboxRepository inboxRepository, ICatRepository catRepository, IRaportRepository raportRepository)
        {
            _inboxRepository = inboxRepository ?? throw new ArgumentNullException(nameof(inboxRepository));
            _catRepository = catRepository ?? throw new ArgumentNullException(nameof(catRepository));
            _raportRepository = raportRepository ?? throw new ArgumentNullException(nameof(raportRepository));
        }

        [HttpGet]
        public async Task<ActionResult<Inbox>> Inbox()
        {
            var myInbox = await _inboxRepository.GetMyInbox(int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value));
            return View(myInbox);
        }

        [HttpGet]
        public async Task<ActionResult<InboxItemDTO>> UpdateInboxItem([FromRoute] int inboxItemId)
        {
            InboxItemDTO myInboxItem = await _inboxRepository.GetMyInboxItem(inboxItemId);
            return Ok(myInboxItem);
        }

        [HttpPost]
        public async Task<ActionResult<InboxItem>> UpdateInboxItem([FromForm] UpdateInboxItemDto updateInboxItemDto,int inboxItemId,DateTime entryDate)
        {       
            var authenticatedId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            await _inboxRepository.UpdateInboxItem(updateInboxItemDto, inboxItemId);
            var catItem = await _catRepository.CreateCat(authenticatedId, inboxItemId,entryDate);
            await _raportRepository.CreateRaport(authenticatedId, catItem, inboxItemId);
            return RedirectToAction("Inbox", "Inbox");
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteInboxItem([FromRoute] int inboxItemId)
        {
            bool myInboxItem = await _inboxRepository.DeleteInboxItem(int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value), inboxItemId);

            return Ok(true);
        }
    }
}
