using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_App
{
    internal class AppFunctions
    {
        // board listeleme
        // kart ekleme
        // kart silme 
        // kart taşıma

        Board board = new Board();
        TeamMembers teamMembers = new TeamMembers();
        CardsFunc cardCards = new CardsFunc();

        #region Board Listeleme
        public void BoardList()
        {
            Console.WriteLine("TODO LINE:");
            Console.WriteLine("**********************");
            cardCards.ForeachCards(board.Todo);


            Console.WriteLine("PROGRESS LINE:");
            Console.WriteLine("**********************");
            cardCards.ForeachCards(board.Progress);

            Console.WriteLine("DONE LINE:");
            Console.WriteLine("**********************");
            cardCards.ForeachCards(board.Done);

        }
        #endregion Board Listeleme


        // burada kartı todo line'a ekleyeceğiz, diğer linelara taşıma işlemini kart taşıma(carryCard)'da yapacağız.
        #region Board'a kart ekleme
        public void AddCard()
        {
            Console.Write("Lütfen başlık giriniz                                : ");
            string title = Console.ReadLine();
            Console.Write("Lütfen içerik giriniz                                : ");
            string content = Console.ReadLine();
            Console.Write("Lütfen atanacak bir takım üyesinin Id'sini giriniz   : ");
            int memberId = int.Parse(Console.ReadLine());
            Console.Write("Lütfen büyüklük giriniz XS(1), S(2), M(3), XL(4)     : ");
            int userInput = int.Parse(Console.ReadLine());
            Sizes.Size size = (Sizes.Size)userInput;


            CardContent newCard = new CardContent(title, content, memberId, size);

            TeamMembers assignedTeamMember = null;

            foreach (var members in teamMembers.TeamMembersList)
            {
                if (memberId == members.Id)
                {
                    assignedTeamMember = members;
                }
            }

            if(assignedTeamMember == null)
            {
                Console.WriteLine("");
                Console.WriteLine("Geçersiz Takım Üyesi Id'si girdiniz.");
            }
            else
            {
                board.Todo.Add(newCard);
                Console.WriteLine("");
                Console.WriteLine("Kart ekleme işlemi başarıyla gerçekleşti");
            }
        }
        #endregion Board'a kart ekleme

        #region Kart Silme
        public int DeleteCard()
        {
            int txnNumber;

            Console.WriteLine("");
            Console.WriteLine(" Öncelikle silmek istediğiniz kartı seçmeniz gerekiyor.\r\n Lütfen kart başlığını yazınız:  ");
            string cardTitle = Console.ReadLine().ToLower();
            List<CardContent> willDeleteCards = new List<CardContent>();

            willDeleteCards = cardCards.DeleteAndCarryTxnFunc(willDeleteCards, cardTitle, board);

            if(willDeleteCards.Count > 0)
            {
                Console.WriteLine("");
                Console.WriteLine("Arama sonuçlarınız bunlar silme işlemine devam etmek istiyor musunuz?");
                Console.WriteLine("");
           
                cardCards.ForeachCards(willDeleteCards);

                Console.WriteLine("Silme İşlemine devam et (y)");
                Console.WriteLine("İşlemi durdur (n)");
                string ch = Console.ReadLine();

                if(ch == "y")
                {
                    board.Todo.RemoveAll(a => a.Title.ToLower() == cardTitle);
                    board.Progress.RemoveAll(a => a.Title.ToLower() == cardTitle);
                    board.Done.RemoveAll(a => a.Title.ToLower() == cardTitle);
                    willDeleteCards.RemoveAll(b => b.AssignedTeamMemberId > 0);

                    Console.WriteLine("");
                    Console.WriteLine("Silme işlemi gerçekleşti");
                    return 0;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Silme işlemi iptal edildi"); 
                    return 0;
                }
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("Aradığınız kriterlere uygun kart bulunamamıştır.");
                Console.WriteLine("* Silmeyi sonladırmak için : (1)");
                Console.WriteLine("* Yeniden denemek için : (2)");
                txnNumber = int.Parse(Console.ReadLine());

                if(txnNumber == 2)
                {
                    DeleteCard();
                }else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Silme işlemi iptal edildi");
                    return 0;
                }

            }

            return 0;

        }
        #endregion Kart Silme

        #region Kart Taşıma
        public int CarryCard()
        {
            int txnNumber;

            Console.WriteLine("");
            Console.WriteLine("Öncelikle taşımak istediğiniz kartı seçmeniz gerekiyor. \nLütfen kart başlığını yazınız:");
            string cardTitle = Console.ReadLine().ToLower();
            List<CardContent> willCarryCards = new List<CardContent>();

            willCarryCards = cardCards.DeleteAndCarryTxnFunc(willCarryCards, cardTitle, board);


            if (willCarryCards.Count > 0)
            {
                Console.WriteLine("");
                Console.WriteLine("Arama sonuçlarınız bunlar taşıma işlemine devam etmek istiyor musunuz?");
                Console.WriteLine("");

                cardCards.ForeachCards(willCarryCards);

                Console.WriteLine("Taşıma İşlemine devam et (y)");
                Console.WriteLine("İşlemi durdur (n)");
                string ch = Console.ReadLine();

                if (ch == "y")
                {
                    Console.WriteLine(" Lütfen taşımak istediğiniz Line'ı seçiniz: (1) TODO (2) IN PROGRESS (3) DONE");
                    int lineNumber = Convert.ToInt16(Console.ReadLine());

                    if (lineNumber == 1)
                    {
                        board.Todo.RemoveAll(a => a.Title.ToLower() == cardTitle);
                        board.Progress.RemoveAll(a => a.Title.ToLower() == cardTitle);
                        board.Done.RemoveAll(a => a.Title.ToLower() == cardTitle);
                        board.Todo.Add(willCarryCards[0]);
                        willCarryCards.RemoveAll(b => b.AssignedTeamMemberId > 0);

                        Console.WriteLine("");
                        Console.WriteLine("Taşıma işlemi gerçekleşti");
                    }
                    else if(lineNumber == 2)
                    {
                        board.Todo.RemoveAll(a => a.Title.ToLower() == cardTitle);
                        board.Progress.RemoveAll(a => a.Title.ToLower() == cardTitle);
                        board.Done.RemoveAll(a => a.Title.ToLower() == cardTitle);
                        board.Progress.Add(willCarryCards[0]);
                        willCarryCards.RemoveAll(b => b.AssignedTeamMemberId > 0);

                        Console.WriteLine("");
                        Console.WriteLine("Taşıma işlemi gerçekleşti");
                    }
                    else if(lineNumber == 3){
                        board.Todo.RemoveAll(a => a.Title.ToLower() == cardTitle);
                        board.Progress.RemoveAll(a => a.Title.ToLower() == cardTitle);
                        board.Done.RemoveAll(a => a.Title.ToLower() == cardTitle);
                        board.Done.Add(willCarryCards[0]);
                        willCarryCards.RemoveAll(b => b.AssignedTeamMemberId > 0);

                        Console.WriteLine("");
                        Console.WriteLine("Taşıma işlemi gerçekleşti");
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Hatalı numara girdiniz!");
                        Console.WriteLine("* Taşımayı sonladırmak için : (1)");
                        Console.WriteLine("* Yeniden denemek için : (2)");
                        txnNumber = int.Parse(Console.ReadLine());

                        if (txnNumber == 2)
                        {
                            CarryCard();
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Taşıma işlemi iptal edildi");
                            return 0;
                        }
                    }    

                    return 0;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Taşıma işlemi iptal edildi");
                    return 0;
                }
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("Aradığınız kriterlere uygun kart bulunamamıştır.");
                Console.WriteLine("* Taşımayı sonladırmak için : (1)");
                Console.WriteLine("* Yeniden denemek için : (2)");
                txnNumber = int.Parse(Console.ReadLine());

                if (txnNumber == 2)
                {
                    CarryCard();
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Taşıma işlemi iptal edildi");
                    return 0;
                }

            }

            return 0;
        }
        #endregion Kart Taşıma

    }
}
