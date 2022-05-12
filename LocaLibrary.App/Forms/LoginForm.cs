﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocaLibrary.App.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        public void OpenMainForm()
        {
            Hide();
            new MainForm().Show();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            OpenMainForm();
        }
    }
}