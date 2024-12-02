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
        public Level1Control_vystraha()
        {
            InitializeComponent();

            LoadLastDownloadedImage();

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
