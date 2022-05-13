﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;

using LocaLibrary.App.Services;

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

        private string validateForm()
        {
            var email = inputEmail.Text;
            var password = inputPassword.Text;

            if (email == "")
            {
                return "Email is required";
            }
            if (password == "")
            {
                return "Password is required";
            }
            if (!UtilService.IsValidEmail(email))
            {
                return "Email is invalid";
            }
            return null;
        }

        private string validateCredentials()
        {
            var sql = @"SELECT TOP 1 Id, Email, FullName, IsAdmin, IsLocked
                        FROM Employee
                        WHERE Email = @Email AND PWDCOMPARE(@Password, PasswordHash) = 1";
            var command = DatabaseService.CreateCommand(sql, CommandType.Text);
            command.Parameters.AddWithValue("@Email", inputEmail.Text);
            command.Parameters.AddWithValue("@Password", inputPassword.Text);

            var dataTable = DatabaseService.GetDataTable(command);
            if (dataTable.Rows.Count == 0)
            {
                return "Email and password do not match";
            }
            if (dataTable.Rows[0]["IsLocked"].ToString() == "True")
            {
                return "Account is locked";
            }
            return null;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            var error = validateForm();
            if (error != null)
            {
                MessageBox.Show(error, "Error");
                return;
            }
            var credentialsError = validateCredentials();
            if (credentialsError != null)
            {
                MessageBox.Show(credentialsError, "Error");
                return;
            }
            OpenMainForm();
        }

        private void linkForgetPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Contact any admins for a new password", "Information");
        }
    }
}
