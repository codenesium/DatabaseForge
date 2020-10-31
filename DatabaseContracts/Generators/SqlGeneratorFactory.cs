using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codenesium.DatabaseContracts
{
    public class SqlGeneratorFactory
    {
        public static ISQLGenerator Factory(string provider)
        {
            if(string.IsNullOrWhiteSpace(provider))
            {
                provider = "MSSQL";
            }

            Providers parsedProvider = (Providers)Enum.Parse(typeof(Providers), provider, true);

            if (parsedProvider == Providers.MSSQL)
            {
                return new MssqlGenerator();
            }
            else if (parsedProvider == Providers.PostgreSQL)
            {
                return new PostgresGenerator();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
