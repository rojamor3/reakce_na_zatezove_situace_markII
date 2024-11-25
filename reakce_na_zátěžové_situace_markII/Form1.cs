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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Point initialLocation = this.Location;

            int pocet_pokusu = 0;

            this.TopMost = true;
            this.MinimizeBox = false;
            this.label1.Text = $"Pocet pokusů: {pocet_pokusu.ToString()}";
            //this.FormBorderStyle = FormBorderStyle.None; // Skryje tlačítka zavření, minimalizace a maximalizace
            this.WindowState = FormWindowState.Maximized; // Okno bude vždy maximalizované


            // Zamezit zavření okna
            this.FormClosing += (sender, e) =>
            {
                if (pocet_pokusu < 3)
                {
                    pocet_pokusu++;
                    e.Cancel = true;    // Zamezí zavření aplikace
                    this.label1.Text = $"Pocet pokusů: {pocet_pokusu.ToString()}" ;
                }
            };

            this.Move += (sender, e) =>
            {
                // Po každém pohybu okna ho vrátíme na původní pozici
                this.Location = initialLocation;
            };
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form1_Move(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
    }
}