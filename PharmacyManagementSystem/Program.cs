using System;
using System.Windows.Forms;
using PharmacyManagementSystem.UI.Forms;

namespace PharmacyManagementSystem
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetDefaultFont(new System.Drawing.Font("Segoe UI", 9F));

            // Show login form first
            var loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // If login successful, show main form and set the user
                var mainForm = new MainForm();
                mainForm.SetUser(loginForm.LoggedInUser);
                Application.Run(mainForm);
            }
        }
    }
}