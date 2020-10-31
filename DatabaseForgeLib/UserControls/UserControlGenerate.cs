using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;
using Codenesium.DataConversionExtensions;
using Codenesium.DatabaseContracts;
using Codenesium.DatabaseContracts.DependencyResolver;

namespace Codenesium.DatabaseForgeLib.UserControls
{
    public partial class UserControlGenerate : MetroUserControl
    {
        DatabaseContainer _database;
        ForgeSettings _settings;
        public UserControlGenerate(ForgeSettings settings)
        {
            this._settings = settings;
            InitializeComponent();
          
        }

        public void Generate(DatabaseContainer database, ISQLGenerator generator)
        {
            this._database = database;
            textBoxOutput.Text = generator.GenerateCreateDatabase(this._database);
        }

      
        public void GenerateInsertStatmentsForDatabase(DatabaseContainer database)
        {
            ISQLGenerator generator = SqlGeneratorFactory.Factory(database.DatabaseType);
            this._database = database;
            textBoxOutput.Text = generator.GenerateInsertStatmentsForDatabase(this._database);
        }

        /// <summary>
        /// Iterates a database one table at a time building the dependency tree for each table and outputing the tree to the screen. 
        /// </summary>
        /// <param name="database"></param>
        public void BuildDependencyTree(DatabaseContainer database)
        {
            Resolver resolver = new Resolver();
            this._database = database;
            List<DependencyTable> convertedTables = resolver.ConvertToDependencyTables(database);
            string output = string.Empty;

            Action<DependencyTable> recursivePrintTable = null;
            int depth = 0;
            recursivePrintTable = (t) =>
            {
     
                t.Columns.ForEach(c =>
                {
                    string refersTo = string.Empty;
                    if (c.RefersTo != null)
                    {
                        refersTo += $" > {c.RefersTo.Table.Schema}.{ c.RefersTo.Table.Name}.{ c.RefersTo.Name}";
                    }

                    string tabs = "";
                    for(int i=0; i< depth; i++)
                    {
                        tabs += "---";
                    }

                    string item = $"{tabs} {t.Schema }.{t.Name}.{c.Name} {refersTo}" + Environment.NewLine;

                    output += item;

                    if (c.RefersTo != null)
                    {
                        depth++;
                        recursivePrintTable(c.RefersTo.Table);
                        depth--;
                    }
                });
         
            };


            string currentTable = string.Empty;
            convertedTables.ForEach(t =>
            {
                if(t.Name != currentTable)
                {
                    output += Environment.NewLine;
                }
                currentTable = t.Name;

                resolver.ClearValues(t);
                resolver.BuildDependsOnList(this._database.GetForeignKeys(), convertedTables, t);
                recursivePrintTable(t);
              
            });

            textBoxOutput.Text = output;
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
     
        }
    }
} 
