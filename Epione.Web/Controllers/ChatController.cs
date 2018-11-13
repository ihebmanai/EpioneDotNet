using Epione.Service;
using Epione.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Epione.Web.Controllers
{

    public class ChatController : Controller
    {
        // GET: Chat
        public async Task<ActionResult> ChatBox()
        {
            ChatViewModel Model = new ChatViewModel();
            ServiceUser UserService = new ServiceUser();
            ServiceDiscussion serviceDiscussion = new ServiceDiscussion();
            Model.currentUser = await UserService.getUserByIdAsync(1);
            System.Web.HttpContext.Current.Session.Add("IUser" ,Model.currentUser);
            System.Web.HttpContext.Current.Items.Add("IUser", Model.currentUser);
            Model.discussions = await serviceDiscussion.getDiscussionsByIdUserAsync(Model.currentUser.id);
            return View(Model);
        }
    }
}