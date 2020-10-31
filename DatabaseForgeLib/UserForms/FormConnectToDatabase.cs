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
		protected static Logger logger = LogManager.GetCurrentClassLogger();

		public bool Saved { get; private set; }

		public string Provider { get; private set; }

		public string ConnectionString { get; private set; }

		public FormConnectToDatabase()
		{
			InitializeComponent();
		}

		public void LoadForm()
		{
			logger.Debug("Entering LoadForm");
			textBoxInstance.Text = ConfigHelper.ReadAppSetting("DatabaseInstance");
			textBoxUsername.Text = ConfigHelper.ReadAppSetting("DatabaseUsername");
			comboBoxProvider.SelectedItem = ConfigHelper.ReadAppSetting("DatabaseProvider");

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
			logger.Debug("Exiting LoadForm");
		}



		private async Task LoadDatabases()
		{
			try
			{
				IDatabaseInterface sqlInterface = DatabaseInterfaceFactory.Factory(comboBoxProvider.SelectedItem.ToString());
				if(this.checkBoxWindowsAuth.Checked)
				{
					this.ConnectionString = sqlInterface.CreateConnectionStringUsingWindowsAuthentication(textBoxInstance.Text, comboBoxDatabases.SelectedValue?.ToString());
				}
				else
				{
					this.ConnectionString = sqlInterface.CreateConnectionString(textBoxInstance.Text, comboBoxDatabases.SelectedValue?.ToString(), textBoxUsername.Text, textBoxPassword.Text);
				}

				progressSpinnerDefault.Visible = true;
				comboBoxDatabases.DataSource = null;
				resetUserMessage();
				sqlInterface.SetConnectionString(this.ConnectionString);
				bool result = sqlInterface.TestConnection();
				if (result)
				{
					SetUserMessage("Connected!", false);

					comboBoxDatabases.DataSource = await Task<List<string>>.Run(() =>
				   {
					   List<string> databases = sqlInterface.GetDatabaseList();
					   databases.Sort();
					   return databases;
				   });
				}
				else
				{
					SetUserMessage("Unable to Connect...", true);
				}
			}
			catch (Exception ex)
			{
				SetUserMessage($"Unable to Connect...Error={ex.Message}", true);
			}
			finally
			{
				progressSpinnerDefault.Visible = false;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (comboBoxDatabases.SelectedIndex > -1)
			{
				this.Saved = true;
				this.Provider = comboBoxProvider.Text;
				IDatabaseInterface sqlInterface = DatabaseInterfaceFactory.Factory(comboBoxProvider.SelectedItem.ToString());

				if(this.checkBoxWindowsAuth.Checked)
				{
					this.ConnectionString = sqlInterface.CreateConnectionStringUsingWindowsAuthentication(textBoxInstance.Text, comboBoxDatabases.SelectedValue?.ToString());

				}
				else
				{
					this.ConnectionString = sqlInterface.CreateConnectionString(textBoxInstance.Text, comboBoxDatabases.SelectedValue?.ToString(), textBoxUsername.Text, textBoxPassword.Text);
				}

				ConfigHelper.WriteAppSetting("DatabaseInstance", textBoxInstance.Text);
				ConfigHelper.WriteAppSetting("DatabaseUsername", textBoxUsername.Text);
				ConfigHelper.WriteAppSetting("DatabaseProvider", comboBoxProvider.SelectedItem?.ToString());
				this.Close();
			}
			else
			{
				SetUserMessage("Database not selected...", true);
			}
		}

		private void SetUserMessage(string text, bool error)
		{

			labelUserMessage.Text = text;

			if (error)
			{
				labelUserMessage.ForeColor = Color.Red;
			}
			else
			{
				labelUserMessage.ForeColor = Color.Green;
			}

			labelUserMessage.Visible = true;
		}

		private void resetUserMessage()
		{
			labelUserMessage.Text = "";
			labelUserMessage.Visible = false;
		}

		private async void textBoxPassword_Leave(object sender, EventArgs e)
		{
			await this.TestConnection();
		}

		private async void buttonTestConnection_Click(object sender, EventArgs e)
		{
			await this.TestConnection();
		}

		private async Task TestConnection()
		{

			progressSpinnerDefault.Visible = true;
			IDatabaseInterface sqlInterface = DatabaseInterfaceFactory.Factory(comboBoxProvider.SelectedItem.ToString());

			if (checkBoxWindowsAuth.Checked)
			{
				this.ConnectionString = sqlInterface.CreateConnectionStringUsingWindowsAuthentication(textBoxInstance.Text, comboBoxDatabases.SelectedValue?.ToString());
			}
			else
			{
				this.ConnectionString = sqlInterface.CreateConnectionString(textBoxInstance.Text, comboBoxDatabases.SelectedValue?.ToString(), textBoxUsername.Text, textBoxPassword.Text);
			}

			sqlInterface.SetConnectionString(this.ConnectionString);
			if (await sqlInterface.TestConnectionAsync())
			{
				this.SetUserMessage("Connected!", false);
				await LoadDatabases();
			}
			else
			{
				this.SetUserMessage("Unable to Connect...", true);
			}
			progressSpinnerDefault.Visible = false;

		}


        private async void comboBoxProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxDatabases.DataSource = null;
			if((Providers)Enum.Parse(typeof(Providers),comboBoxProvider.SelectedItem.ToString()) == Providers.MSSQL)
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
