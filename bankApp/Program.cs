using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bankApp
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Account konto = new Account("Dawid", "Warsiński", "Poznan", DateTime.Now);
            Account konto1 = new Account("Szymon", "Kowalski", "Warszawa", DateTime.Now);
            Account konto2 = new Account("Kuba", "Maciejewkski", "Bydgoszcz", DateTime.Now);
            Account konto3 = new Account("Grzegorz", "Wasilewski", "Poznan", DateTime.Now);

            konto.topUpAmount(150);
            konto.topUpAmount(250);
            konto.topUpAmount(450);
            Console.WriteLine(konto2.PublicKey);

            Console.WriteLine(konto.AvaliableBalance);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(konto));
        }
    }
}
