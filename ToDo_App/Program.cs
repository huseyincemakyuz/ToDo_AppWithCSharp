using System;
using System.Collections.Generic;
using ToDo_App;

namespace ToDo_app  
{
    class program
    {
        public static void Main(string[] args)
        {
            TeamMembers teamMembers = new TeamMembers();    
       
            // Menü - Navigasyon
            Console.WriteLine("Lütfen yapmak istediğiniz işlemin numarasını giriniz");
            Console.WriteLine("****************************************************");
            Console.WriteLine("(1) Board Listelemek");
            Console.WriteLine("(2) Board'a Kart Eklemek");
            Console.WriteLine("(3) Board'dan Kart Silmek");
            Console.WriteLine("(4) Kart Taşımak");
            Console.WriteLine("(5) Çıkış");


            // TeamMembers listesini sağ üst köşeye yazdırıyoruz.
            Console.SetCursorPosition(Console.WindowWidth - 30, 0);
            Console.WriteLine("Team Members:");
            Console.SetCursorPosition(Console.WindowWidth - 30, 1);
            Console.WriteLine("--------------");
            int row = 2;
            foreach (TeamMembers member in teamMembers.TeamMembersList)
            {
                Console.SetCursorPosition(Console.WindowWidth - 30, row++);
                Console.WriteLine(member.FirstName + " " + member.LastName + " " + member.Id);
            }

            Console.SetCursorPosition(0, 8); // imleci tekrar nav'ın altında konumlandırdık.

            
            int txnNumber = Convert.ToInt16(Console.ReadLine()); // İşlem numarasını alıyoruz.
            int[] checkArray = { 1, 2, 3, 4 };     // while loop kontrolu icin.

            AppFunctions appFunctions = new AppFunctions();
            

            while (checkArray.Contains(txnNumber) == true)
            {
                switch (txnNumber)
                {
                    case 1:
                        appFunctions.BoardList();
                        txnNumber = 0;
                        break;

                    case 2:
                        appFunctions.AddCard();
                        break;

                    case 3:
                        appFunctions.DeleteCard();
                        break;

                    case 4:
                        appFunctions.CarryCard(); 
                        break;
                }
                Console.WriteLine();
                Console.WriteLine("Yapmak istediginiz baska bir islem varsa tuslayiniz");
                txnNumber = int.Parse(Console.ReadLine());

            }
        }
    }
}
