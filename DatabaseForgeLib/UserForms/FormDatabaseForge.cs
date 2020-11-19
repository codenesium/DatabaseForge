using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Codenesium.DatabaseContracts;
using Codenesium.DatabaseForgeLib.UserControls;
using MetroFramework.Forms;

namespace Codenesium.DatabaseForgeLib.UserForms
{
    public partial class FormDatabaseForge : MetroForm
    {
        UserControls.UserControlDBForgeEditor _userControlDBForgeEditor;
        ForgeSettings _settings = new ForgeSettings();
        public DatabaseContainer DatabaseContainer { get; private set; }
        public string ConnectionString { get; private set; }
        /// <summary>
        /// Check this variable to know if the user clicked Complete Schema Editing vs Cancel or just closing the form.
        /// If true it means they clicked complete schema editing. 
        /// </summary>
        public bool Saved { get; private set; } = false;

        public FormDatabaseForge(bool codenesiumMode = false, DatabaseContainer container = null)
        {
            this._settings.CodenesiumMode = codenesiumMode;
            this.DatabaseContainer = container;
            this.InitializeComponent();
            this.initializeUserControls();
            this.setPanelUserControl(this._userControlDBForgeEditor);
        }

        private void initializeUserControls()
        {
            this._userControlDBForgeEditor = new UserControlDBForgeEditor(this._settings, this.DatabaseContainer);
            this._userControlDBForgeEditor.SchemaEditingCompleted += (o, e) =>
            {
                if(e.Database != null)
                {
                    this.Saved = true;
                }
                this.DatabaseContainer = e.Database;
                this.Close();
            };
        }

        private void setPanelUserControl(UserControl control)
        {
            Action addcontrol = () =>
            {
                this.panelContainer.Controls.Clear();
                this.panelContainer.Controls.Add(control);
            };
            if (this.panelContainer.InvokeRequired)
            {
                this.panelContainer.Invoke(new MethodInvoker(delegate
                {
                    addcontrol();
                }));
            }
            else
            {
                addcontrol();
            }
        }

        private async void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await this._userControlDBForgeEditor.SaveProject();
        }

        private async void loadProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await this._userControlDBForgeEditor.LoadProject();
        }

        private async void loadFromDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormConnectToDatabase connectToDatabase = new FormConnectToDatabase();
            connectToDatabase.LoadForm();
            connectToDatabase.ShowDialog();
            if (connectToDatabase.Saved)
            {
                this._userControlDBForgeEditor.displayUserMessageSuccess("Loading database schema...");
                this._userControlDBForgeEditor.showSpinner();
                IDatabaseInterface sqlInterface = DatabaseInterfaceFactory.Factory(connectToDatabase.Provider);
                sqlInterface.SetConnectionString(connectToDatabase.ConnectionString);
                DatabaseContainer databaseContainer = null;

                await Task.Run(() =>
                {
                    databaseContainer = sqlInterface.GetDatabaseStructure();
                });

                this._userControlDBForgeEditor.LoadProject(databaseContainer);
                this.ConnectionString = connectToDatabase.ConnectionString;
                this._userControlDBForgeEditor.displayUserMessageSuccess("Database schema loaded...");
                this._userControlDBForgeEditor.hideSpinner();
            }
        }

        private void buttonMenu_MouseDown(object sender, MouseEventArgs e)
        {
            this.contextMenuStripMenu.Show(this.buttonMenu, e.Location);
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }

        private void mSSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._userControlDBForgeEditor.LoadProject(new DatabaseContainer("", DatabaseContainer.DatabaseTypeMSSQL));
        }

        private void postgreSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._userControlDBForgeEditor.LoadProject(new DatabaseContainer("", DatabaseContainer.DatabaseTypePostgreSQL));
        }

        private void buttonMenu_Click(object sender, EventArgs e)
        {

        }
    }
}
