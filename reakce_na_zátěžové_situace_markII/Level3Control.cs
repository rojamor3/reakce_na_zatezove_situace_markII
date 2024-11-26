using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reakce_na_zátěžové_situace_markII
{
    public partial class Level3Control : UserControl
    {
        public event EventHandler Form_Closing;

        int pocet_pokusu = 0;
        bool show_counter = false;

        public Level3Control()
        {
            InitializeComponent();

            label1.Font = new Font(label1.Font.FontFamily, this.Width / 25); // Velikost textu label1
            label2.Font = new Font(label2.Font.FontFamily, this.Width / 25); // Velikost textu label1

        }



        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Získání rodičovského formuláře
            var parentForm = this.FindForm();
            if (parentForm != null)
            {
                parentForm.FormClosing += ParentForm_FormClosing; // Připojení k události FormClosing
            }
        }

        private void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Přejete si odejít?", "Potvrzení", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Zpracování odpovědi uživatele
            if (result == DialogResult.Yes)
            {
                pocet_pokusu++;
            }
            else if (result == DialogResult.No)
            {
                // Akce při volbě "Ne" (např. nic neudělá)
                MessageBox.Show("Díky, že zustáváte s námi, skupina ČEZ.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (pocet_pokusu == 3)
            {
                label2.Visible = true;
                pictureBox1.Visible = true; pictureBox2.Visible = true;
                show_counter = true;
            }
            if (show_counter)
                label1.Text = $"Počet pokusů: {pocet_pokusu}";


            if (pocet_pokusu == 5)
                label2.Text = "Ale snažíš se a to se počítá!";
            if (pocet_pokusu == 8)
                label2.Text = "A jednou to třeba i vyjde...";

            if (pocet_pokusu == 10)
                Form_Closing?.Invoke(this, EventArgs.Empty);


        }

        protected override void Dispose(bool disposing)
        {
            // Odpojení od události při zničení UserControl
            var parentForm = this.FindForm();
            if (disposing && parentForm != null)
            {
                parentForm.FormClosing -= ParentForm_FormClosing;
            }
            base.Dispose(disposing);
        }
    }
}
