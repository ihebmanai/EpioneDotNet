using Epione.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epione.Web.Models
{
    public class ChatViewModel
    {
       public user currentUser;
       public List<discussion> discussions;
    }
}