using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_App
{
    internal class CardContent
    {
        //Başlık      : 
        //İçerik      :
        //Atanan Kişi :
        //Büyüklük    :

        public string Title { get; set; }
        public string Content { get; set; }
        public int AssignedTeamMemberId { get; set; }
        public Sizes.Size Size { get; set; }
        public CardContent(string title, string content, int assignedTeamMemberId, Sizes.Size size)
        {
            Title = title;
            Content = content;
            AssignedTeamMemberId = assignedTeamMemberId;
            this.Size = size;
        }


    }
}
