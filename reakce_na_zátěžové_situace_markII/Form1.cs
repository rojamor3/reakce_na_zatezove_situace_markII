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

            this.TopMost = false ;
            this.WindowState = FormWindowState.Maximized; // Okno bude vždy maximalizované

            bool closing_var = true ;
            this.FormClosing += (sender, e) =>
            {
                e.Cancel = closing_var;    // Zamezí zavření aplikace
            };

            //***********__Main část__***********
            var Level1 = new Level1Control();
            var Level1_1 = new Level1Control_vystraha();
            var Level2 = new Level2Control();
            var Level3 = new Level3Control();


            ShowUserControl(Level1);
            Level1.GoToNextLevel += (s, e) => ShowUserControl(Level2); // Nahradí panel obsahem Level2Control
            Level1.OpenSubLevel += (s, e) => ShowUserControl(Level1_1); // Nahradí panel obsahem Level2Control
            Level1_1.CloseSubLevel += (s, e) => ShowUserControl(Level1); // Nahradí panel obsahem Level2Control


            Level2.GoToNextLevel += (s, e) => { ShowUserControl(Level3); this.FormBorderStyle = FormBorderStyle.FixedSingle; };

            Level3.Form_Closing += (s, e) => { closing_var = !closing_var; };


        }

        private void ShowUserControl(UserControl control)
        {
            panel1.Controls.Clear(); // Odstraní aktuální obsah panelu
            control.Dock = DockStyle.Fill; // Upraví velikost na celý panel
            panel1.Controls.Add(control); // Přidá nový UserControl
        }



        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112; // Zpráva pro systémové příkazy
            const int SC_MINIMIZE = 0xF020;   // Minimalizace
            const int SC_RESTORE = 0xF120;   // Obnovení okna

            if (m.Msg == WM_SYSCOMMAND &&
                (m.WParam.ToInt32() == SC_MINIMIZE || m.WParam.ToInt32() == SC_RESTORE))
            {
                // Zamezení minimalizace nebo obnovení z maximalizovaného stavu
                return;
            }

            base.WndProc(ref m);
        }

        // Neustálé maximalizování okna
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // Pokud by uživatel přesto změnil velikost, obnovíme maximalizovaný stav
            if (this.WindowState != FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }
    }
}