using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_App
{
    internal class Board
    {
        public List<CardContent> Todo { get; set; }
        public List<CardContent> Progress { get; set; }
        public List<CardContent> Done { get; set; }

        public Board()
        {
            Todo = new List<CardContent>()
            {
                new CardContent("Analiz","Analiz tasarım dokümanı oluştur",1234,(Sizes.Size)2),
                new CardContent("Test","Test Yapılacak", 9101, (Sizes.Size)3)
            };
            Progress = new List<CardContent>()
            {
                new CardContent("İnceleme", "Konu ile ilgili araştırma yap", 2345, (Sizes.Size)4)
            };
            Done = new List<CardContent>()
            {
                new CardContent("Geliştirme","Geliştirme aşamalarını tamamla",5678, (Sizes.Size)5)
            };
        }
    }
}
