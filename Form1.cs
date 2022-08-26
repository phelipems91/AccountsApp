using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountsApp
{
    public partial class frmAccounts : Form
    {
        private List<Account> accounts = new List<Account>();
        Account accountFound;
        public frmAccounts()
        {
            InitializeComponent();
        }

        private void frmAccounts_Load(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void rbtnChecking_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnChecking.Checked)
            {
                txtLimit.Enabled = true;
                txtInterestRate.Enabled = false;
            }
            else
            {
                txtLimit.Enabled = false;
                txtInterestRate.Enabled = true;
            }

        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbtnChecking.Checked)
                {
                    CheckingAccount checkingAccount = new CheckingAccount(int.Parse(txtAccountNumber.Text), txtClientName.Text, double.Parse(txtBalance.Text), double.Parse(txtLimit.Text));
                    accounts.Add(checkingAccount);
                }
                else
                {
                    SavingsAccount savingsAccount = new SavingsAccount(int.Parse(txtAccountNumber.Text), txtClientName.Text, double.Parse(txtBalance.Text), double.Parse(txtInterestRate.Text));
                    accounts.Add(savingsAccount);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid Format");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ClearFields();

            MessageBox.Show($"Total number of accounts: {accounts.Count}");
        }

        private void ClearFields()
        {
            txtAccountNumber.Text = "";
            txtClientName.Text = "";
            txtBalance.Text = "";
            txtLimit.Text = "";
            txtInterestRate.Text = "";
            rbtnChecking.Checked = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ClearFields();
            
            accountFound = null;

            foreach(Account a in accounts)
                if(a.Number == int.Parse(txtSearch.Text))
                    accountFound = a;

            if(accountFound != null)
            {
                txtAccountNumber.Text = accountFound.Number.ToString();
                txtClientName.Text = accountFound.Name;
                txtBalance.Text = accountFound.Balance.ToString();

                if (accountFound.GetType() == typeof(CheckingAccount))
                {
                    txtLimit.Text = ((CheckingAccount)accountFound).Limit.ToString();
                    rbtnChecking.Checked = true;
                }
                else
                {
                    txtInterestRate.Text = ((SavingsAccount)accountFound).Interest.ToString();
                    rbtnSavings.Checked = true;
                }
            }
            else
            {
                MessageBox.Show("Account not found!");
            }
        }
    }
}
