using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reakce_na_zátěžové_situace_markII
{
    public partial class Level1Control_vystraha : UserControl
    {
        public event EventHandler CloseSubLevel;
        public Level1Control_vystraha()
        {
            InitializeComponent();
            button1.FlatAppearance.BorderSize = 3; // Nastaví tloušťku rámečku
            button1.FlatAppearance.BorderColor = Color.FromArgb(150, 150, 150); // Nastaví barvu rámečku
            button1.Font = new Font(button1.Font.FontFamily, button1.Width / 6); // Velikost písma tlačítka
            label1.Font = new Font(label1.Font.FontFamily, this.Width / 20); // Velikost textu label1
            label2.Font = new Font(label2.Font.FontFamily, this.Width / 33); // Velikost textu label1
            label3.Font = new Font(label3.Font.FontFamily, this.Width / 16); // Velikost textu label1
            label4.Font = new Font(label4.Font.FontFamily, this.Width / 50); // Velikost textu label1
            label5.Font = new Font(label5.Font.FontFamily, this.Width / 45); // Velikost textu label1






            LoadLastDownloadedImage();

            button1.Click += (s, e) => CloseSubLevel?.Invoke(this, EventArgs.Empty);
        }

        private void LoadLastDownloadedImage()
        {
            try
            {
                // Nejprve zkusíme načíst obrázek z Downloads
                string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                string lastImageFile = GetLastImageFile(downloadsPath);

                if (lastImageFile == null)
                {
                    // Pokud nenajdeme obrázek v Downloads, zkusíme Desktop
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    lastImageFile = GetLastImageFile(desktopPath);
                }

                if (lastImageFile != null)
                {
                    // Načtení obrázku do PictureBoxu
                    pictureBox1.Image = Image.FromFile(lastImageFile);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; // Přizpůsobení obrázku
                }
                else
                {
                    MessageBox.Show("V Downloads ani na Ploše nebyl nalezen žádný obrázek.", "Upozornění", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při načítání obrázku: {ex.Message}", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetLastImageFile(string folderPath)
        {
            // Seznam podporovaných přípon obrázků
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };

            // Získání nejnovějšího souboru v dané složce
            return new DirectoryInfo(folderPath)
                .GetFiles()
                .Where(file => imageExtensions.Contains(file.Extension.ToLower()))
                .OrderByDescending(file => file.LastWriteTime)
                .Select(file => file.FullName)
                .FirstOrDefault();
        }
    }
}
