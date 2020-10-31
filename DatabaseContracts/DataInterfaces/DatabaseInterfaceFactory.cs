using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Codenesium.DatabaseContracts
{
    public class DatabaseInterfaceFactory
    {
        public static IDatabaseInterface Factory(string provider)
        {
            Providers parsedProvider = (Providers)Enum.Parse(typeof(Providers), provider,true);

            if(parsedProvider == Providers.MSSQL)
            {
                return new MSSQLInterface();
            }
            else if(parsedProvider == Providers.PostgreSQL)
            {
                return new PostgresqlInterface();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}