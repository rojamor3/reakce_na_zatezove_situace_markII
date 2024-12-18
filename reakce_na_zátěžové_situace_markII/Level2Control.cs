﻿using System;
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
    public partial class Level2Control : UserControl
    {
        public event EventHandler GoToNextLevel;
        public Level2Control()
        {
            InitializeComponent();

            button1.Click += (s, e) => GoToNextLevel?.Invoke(this, EventArgs.Empty);
        }
    }
}
