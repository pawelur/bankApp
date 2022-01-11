using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bankApp
{
    public class HistoryItem
    {
        private DateTime date;
        private string operationName;
        private int amount;

        public HistoryItem(DateTime date, string operationName, int amount)
        {
            this.date = date;
            this.operationName = operationName; 
            this.amount = amount;
        }

        public DateTime Date
        {
            get => date; 
            set => date = value; 
        }

        public string OperationName { 
            get => operationName;
            set => operationName = value; 
        }  

        public int Amount
        {
            get => amount; 
        }
    }
}
