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
    public partial class Level1Control : UserControl
    {
        public event EventHandler GoToNextLevel;
        public event EventHandler OpenSubLevel;


        public Level1Control()
        {
            InitializeComponent();

            button1.FlatStyle = FlatStyle.Flat; // Nastaví styl tlačítka na plochý
            button2.FlatStyle = FlatStyle.Flat; // Nastaví styl tlačítka na plochý
            button1.FlatAppearance.BorderSize = 2; // Nastaví tloušťku rámečku
            button2.FlatAppearance.BorderSize = 2; // Nastaví tloušťku rámečku
            button1.FlatAppearance.BorderColor = Color.FromArgb(55, 55, 55); // Nastaví barvu rámečku
            button2.FlatAppearance.BorderColor = Color.FromArgb(55, 55, 55); // Nastaví barvu rámečku
            button1.Font = new Font(button1.Font.FontFamily, button1.Width / 10); // Velikost písma tlačítka
            button2.Font = new Font(button2.Font.FontFamily, button1.Width / 10); // Velikost písma tlačítka


            label1.Font = new Font(label1.Font.FontFamily, this.Width / 25); // Velikost textu label1
            label2.Font = new Font(label2.Font.FontFamily, this.Width / 35); // Velikost textu label2

            button1.Click += (s, e) => GoToNextLevel?.Invoke(this, EventArgs.Empty);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Šupáku neštvi a řekni, že se bojíš! \n Nebo snad potřebuješ přesvědčit?",           // Text zprávy
                "",                        // Titulek okna
                MessageBoxButtons.YesNo,    // Tlačítka
                MessageBoxIcon.Warning,           // Ikona
                MessageBoxDefaultButton.Button2  // Výchozí tlačítko
            );

            if (result == DialogResult.Yes)
            {
                OpenSubLevel?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                // Kód pro zamítnutí akce
                MessageBox.Show("Akce zrušena.");
            }
        }
    }
}
