using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_App
{
    internal class CardsFunc
    {
        //Board board = new Board();
        TeamMembers teamMembers = new TeamMembers();

        // Board'daki her line göre cardları listeliyoruz.
        public void ForeachCards(List<CardContent> cardLine)
        {

            foreach (CardContent card in cardLine) 
            { 
                TeamMembers assignedTeamMember = teamMembers.TeamMembersList.Find(tm => tm.Id == card.AssignedTeamMemberId);
                Console.WriteLine("Başlık: " + card.Title);
                Console.WriteLine("İçerik: " + card.Content);
                Console.WriteLine("Atanan Kişi: " + assignedTeamMember.FirstName + " " + assignedTeamMember.LastName);
                Console.WriteLine("Büyüklük: " + card.Size);
                Console.WriteLine("-------------------");
            }
        }

        // Delete ve Carry işlemlerinde kartları board'da arıyoruz.
        public List<CardContent> DeleteAndCarryTxnFunc(List<CardContent> card,string cardTitle,Board board)
        {
            for (int i = 0; i < board.Todo.Count; i++)
            {
                if (board.Todo[i].Title.ToLower() == cardTitle)
                {
                    card.Add(board.Todo[i]);
                }
            }

            for (int i = 0; i < board.Progress.Count; i++)
            {
                if (board.Progress[i].Title.ToLower() == cardTitle)
                {
                    card.Add(board.Progress[i]);
                }
            }

            for (int i = 0; i < board.Done.Count; i++)
            {
                if (board.Done[i].Title.ToLower() == cardTitle)
                {
                    card.Add(board.Todo[i]);
                }
            }

            return card;
        }
    }

   
}
