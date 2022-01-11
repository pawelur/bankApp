using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bankApp
{


    public partial class Form1 : Form
    {
        Account loggedAccount;
        Panel scene;
        List<Label> renderedHistoryItems = new List<Label>();
        public Form1(Account loggedAccount)
        {
            this.loggedAccount = loggedAccount;

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            renderStanKonta();
            renderNumerKonta();
            renderHistory();
            this.scene = mojeKontoView;
        }

        private void renderNumerKonta()
        {
            numerKonta.Text = loggedAccount.PublicKey.ToString();
        }

        private void renderStanKonta()
        {
            stanKonta.Text = String.Format("{0} zł", loggedAccount.AvaliableBalance);
        }

        private void renderHistory()
        {
            int lastLocation = 19;
            foreach (Label label in renderedHistoryItems)
            {
                this.history_panel.Controls.Remove(label);
            }

            foreach (HistoryItem item in loggedAccount.History)
            {
                Label historyItem = new System.Windows.Forms.Label();
                historyItem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                historyItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                historyItem.Location = new System.Drawing.Point(0, lastLocation);
                historyItem.Name = "historyItem";
                historyItem.Size = new System.Drawing.Size(280, 60);
                historyItem.TabIndex = 10;
                historyItem.Text = String.Format("dnia: {0},\n {1} - {2} ZŁ", item.Date, item.OperationName, item.Amount);
                lastLocation += 65;
                this.history_panel.Controls.Add(historyItem);
                renderedHistoryItems.Add(historyItem);
            }
        }
        private void changeScene(Panel newScene)
        {
            this.scene.Visible = false;
            newScene.Visible = true;
            this.scene = newScene;
        }

        private void mojeKontoButton_Click(object sender, EventArgs e)
        {
            changeScene(mojeKontoView);
            renderHistory();
            renderStanKonta();
        }

        private void wplacButton_Click(object sender, EventArgs e)
        {
            changeScene(wplacView);
        }

        private void wykonajButton_Click(object sender, EventArgs e)
        {
            changeScene(wykonajView);
        }

        private void wplacActionButton_Click(object sender, EventArgs e)
        {
            if (int.TryParse(wplacTextBox.Text, out int amount))
            {
                loggedAccount.topUpAmount(amount);
            }
        }

        private void wyplacActionButton_Click(object sender, EventArgs e)
        {
            if (int.TryParse(wyplacTextBox.Text, out int amount))
            {
                loggedAccount.payOutAmount(amount);
            }
        }

        private void przelejActionButton_Click(object sender, EventArgs e)
        {

            if (int.TryParse(kwotaTextBox.Text, out int amount))
            {
                loggedAccount.sendMoney(Guid.Parse(NumerKontaTextBox.Text), amount);
            }
        }
    }
}
