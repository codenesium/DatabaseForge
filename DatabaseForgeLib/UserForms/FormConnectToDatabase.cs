using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Codenesium.ConfigTools;
using Codenesium.DatabaseContracts;
using MetroFramework.Forms;
using NLog;

namespace Codenesium.DatabaseForgeLib.UserForms
{
	public partial class FormConnectToDatabase : MetroForm
	{
		protected static Logger Logger = LogManager.GetCurrentClassLogger();

		public bool Saved { get; private set; }

		public string Provider { get; private set; }

		public string ConnectionString { get; private set; }

		public FormConnectToDatabase()
		{
            this.InitializeComponent();
		}

		public void LoadForm()
		{
			Logger.Debug("Entering LoadForm");
            this.textBoxInstance.Text = ConfigHelper.ReadAppSetting("DatabaseInstance");
            this.textBoxUsername.Text = ConfigHelper.ReadAppSetting("DatabaseUsername");
            this.comboBoxProvider.SelectedItem = ConfigHelper.ReadAppSetting("DatabaseProvider");

			//if (String.IsNullOrWhiteSpace(textBoxInstance.Text))
			//{
			//    textBoxInstance.Select();
			//}
			//else
			//{
			//    textBoxUsername.Select();
			//}

			//if (!String.IsNullOrWhiteSpace(textBoxInstance.Text) && !String.IsNullOrWhiteSpace(textBoxUsername.Text))
			//{
			//    textBoxPassword.Select();
			//}

			//if (comboBoxProvider.SelectedIndex < 0)
			//{
			//    comboBoxProvider.SelectedIndex = 0;
			//}

#if DEBUG
			//textBoxPassword.Text = "Passw0rd";
			//buttonTestConnection_Click(this, new EventArgs());
#endif
			Logger.Debug("Exiting LoadForm");
		}



		private async Task LoadDatabases()
		{
			try
			{
				IDatabaseInterface sqlInterface = DatabaseInterfaceFactory.Factory(this.comboBoxProvider.SelectedItem.ToString());
				if(this.checkBoxWindowsAuth.Checked)
				{
					this.ConnectionString = sqlInterface.CreateConnectionStringUsingWindowsAuthentication(this.textBoxInstance.Text, this.comboBoxDatabases.SelectedValue?.ToString());
				}
				else
				{
					this.ConnectionString = sqlInterface.CreateConnectionString(this.textBoxInstance.Text, this.comboBoxDatabases.SelectedValue?.ToString(), this.textBoxUsername.Text, this.textBoxPassword.Text);
				}

                this.progressSpinnerDefault.Visible = true;
                this.comboBoxDatabases.DataSource = null;
                this.ResetUserMessage();
				sqlInterface.SetConnectionString(this.ConnectionString);
				bool result = sqlInterface.TestConnection();
				if (result)
				{
                    this.SetUserMessage("Connected!", false);

                    this.comboBoxDatabases.DataSource = await Task<List<string>>.Run(() =>
				   {
					   List<string> databases = sqlInterface.GetDatabaseList();
					   databases.Sort();
					   return databases;
				   });
				}
				else
				{
                    this.SetUserMessage("Unable to Connect...", true);
				}
			}
			catch (Exception ex)
			{
                this.SetUserMessage($"Unable to Connect...Error={ex.Message}", true);
			}
			finally
			{
                this.progressSpinnerDefault.Visible = false;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (this.comboBoxDatabases.SelectedIndex > -1)
			{
				this.Saved = true;
				this.Provider = this.comboBoxProvider.Text;
				IDatabaseInterface sqlInterface = DatabaseInterfaceFactory.Factory(this.comboBoxProvider.SelectedItem.ToString());

				if(this.checkBoxWindowsAuth.Checked)
				{
					this.ConnectionString = sqlInterface.CreateConnectionStringUsingWindowsAuthentication(this.textBoxInstance.Text, this.comboBoxDatabases.SelectedValue?.ToString());

				}
				else
				{
					this.ConnectionString = sqlInterface.CreateConnectionString(this.textBoxInstance.Text, this.comboBoxDatabases.SelectedValue?.ToString(), this.textBoxUsername.Text, this.textBoxPassword.Text);
				}

				ConfigHelper.WriteAppSetting("DatabaseInstance", this.textBoxInstance.Text);
				ConfigHelper.WriteAppSetting("DatabaseUsername", this.textBoxUsername.Text);
				ConfigHelper.WriteAppSetting("DatabaseProvider", this.comboBoxProvider.SelectedItem?.ToString());
				this.Close();
			}
			else
			{
                this.SetUserMessage("Database not selected...", true);
			}
		}

		private void SetUserMessage(string text, bool error)
		{

            this.labelUserMessage.Text = text;

			if (error)
			{
                this.labelUserMessage.ForeColor = Color.Red;
			}
			else
			{
                this.labelUserMessage.ForeColor = Color.Green;
			}

            this.labelUserMessage.Visible = true;
		}

		private void ResetUserMessage()
		{
            this.labelUserMessage.Text = "";
            this.labelUserMessage.Visible = false;
		}

		private async void TextBoxPassword_Leave(object sender, EventArgs e)
		{
			await this.TestConnection();
		}

		private async void ButtonTestConnection_Click(object sender, EventArgs e)
		{
			await this.TestConnection();
		}

		private async Task TestConnection()
		{

            this.progressSpinnerDefault.Visible = true;
			IDatabaseInterface sqlInterface = DatabaseInterfaceFactory.Factory(this.comboBoxProvider.SelectedItem.ToString());

			if (this.checkBoxWindowsAuth.Checked)
			{
				this.ConnectionString = sqlInterface.CreateConnectionStringUsingWindowsAuthentication(this.textBoxInstance.Text, this.comboBoxDatabases.SelectedValue?.ToString());
			}
			else
			{
				this.ConnectionString = sqlInterface.CreateConnectionString(this.textBoxInstance.Text, this.comboBoxDatabases.SelectedValue?.ToString(), this.textBoxUsername.Text, this.textBoxPassword.Text);
			}

			sqlInterface.SetConnectionString(this.ConnectionString);
			if (await sqlInterface.TestConnectionAsync())
			{
				this.SetUserMessage("Connected!", false);
				await this.LoadDatabases();
			}
			else
			{
				this.SetUserMessage("Unable to Connect...", true);
			}
            this.progressSpinnerDefault.Visible = false;

		}


        private async void ComboBoxProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBoxDatabases.DataSource = null;
			if((Providers)Enum.Parse(typeof(Providers), this.comboBoxProvider.SelectedItem.ToString()) == Providers.MSSQL)
			{
				this.checkBoxWindowsAuth.Visible = true;
			}
			else
			{
				this.checkBoxWindowsAuth.Visible = false;
			}
        }

		private void CheckBoxWindowsAuth_CheckedChanged(object sender, EventArgs e)
		{
			if(((CheckBox)(sender)).Checked)
			{
				this.textBoxPassword.Enabled = false;
				this.textBoxUsername.Enabled = false;
				this.textBoxUsername.Clear();
				this.textBoxPassword.Clear();
			}
			else
			{
				this.textBoxPassword.Enabled = true;
				this.textBoxUsername.Enabled = true;
			}
		}
	}
}
