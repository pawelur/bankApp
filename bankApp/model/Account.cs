using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bankApp
{

    public class Account
    {
        public static int id = 0;
        public static List<Account> bankAccounts = new List<Account>();

        private string name;
        private string surname;
        private string address;
        private DateTime dateOfBirth;
        private DateTime dateOfAccountCreation;

        private int avaliableBalance;
        private int blockedBalance;

        private Guid publicKey;

        private List<HistoryItem> history;

        public Account(string name, string surname, string address, DateTime dateOfBirth)
        {
            this.name = name;
            this.surname = surname;
            this.address = address;
            this.dateOfBirth = dateOfBirth;

            this.dateOfAccountCreation = DateTime.Now;

            this.avaliableBalance = 0;
            this.blockedBalance = 0;

            this.history = new List<HistoryItem>();

            this.publicKey = Guid.NewGuid();

            id++;
            bankAccounts.Add(this);
        }

        private void addHistory(DateTime date, string operationName, int amount)
        {
            HistoryItem historyItem = new HistoryItem(date, operationName, amount);
            this.history.Add(historyItem);
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Surname
        {
            get => surname;
            set => surname = value;
        }

        public string Address
        {
            get => address;
            set => address = value;
        }
        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set => dateOfBirth = value;
        }

        public int AvaliableBalance
        {
            get => avaliableBalance;
        }

        public int BlockedBalance
        {
            get => blockedBalance;
        }

        public Guid PublicKey
        {
            get => publicKey;
        }

        public List<HistoryItem> History
        {
            get => history;
        }

        public int getFullBalance()
        {
            return avaliableBalance + blockedBalance;
        }

        public string getFullName()
        {
            return String.Format("{0} {1}", name, surname);
        }

        public void topUpAmount(int amount, bool saveHistory = true)
        {
            if (amount > 0 && amount < 1000)
            {
                avaliableBalance += amount;
            }

            if (saveHistory)
            {
                this.addHistory(DateTime.Now, "Wpłacono środki", amount);
            }
            //TODO: raise error when amout is invalid
        }

        public int payOutAmount(int amount, bool saveHistory = true)
        {
            int takenBalance = amount;

            if (amount <= avaliableBalance)
            {
                avaliableBalance -= amount;
            }
            else
            {
                takenBalance = avaliableBalance;
                avaliableBalance = 0;
            }

            if (saveHistory)
            {
                this.addHistory(DateTime.Now, "Wypłacono środki", amount);
            }

            return takenBalance;
        }

        public void sendMoney(Guid recieverPublicKey, int amount, bool saveHistory = true)
        {
            //TODO: make it more efficient
            Account reciever = bankAccounts.Where(x => x.PublicKey == recieverPublicKey).FirstOrDefault();

            if (reciever == null)
            {
                throw new ApplicationException("no account exists with that id");
            }

            // reciever gets same amout that was took from sender
            int moneyTransfered = this.payOutAmount(amount, false);
            reciever.topUpAmount(moneyTransfered, false);

            if (saveHistory)
            {
                this.addHistory(DateTime.Now, "Wysłano środki", amount);
                reciever.addHistory(DateTime.Now, "Odebrano środki", amount);
            }
        }
    }
}
