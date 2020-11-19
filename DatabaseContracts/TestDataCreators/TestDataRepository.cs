using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Codenesium.DatabaseContracts.DependencyResolver
{
    public class TestDataRepository
    {
        public List<int> tinyInts = new List<int>
        {
            1,
            2,
            3,
            4,
            5,
            6,
            7,
            8,
            9
        };

        public List<int> smallInts = new List<int>
        {
            1,
            2,
            3,
            4,
            5,
            6,
            7,
            8,
            9,
            10

        };

        public List<int> integers = new List<int>
        {
            0,
            1,
            2,
            3,
            4,
            5,
            6,
            7,
            8,
            9

        };


        public List<double> moneys = new List<double>()
        {
            1.00,
            12.00,
            5396.30,
            3810.59,
            8162.13,
            8394.74,
            1121.08,
            4652.89,
            6313.23,
            6740.12,
        };

        public List<double> decimals = new List<double>()
        {
            1.00,
            2.00,
            3.00,
            8394.74,
            1121.08,
            4652.89,
            6313.23,
            6740.12,
            2440.31,
            1489.98,

        };


        public List<DateTime> dateTimes = new List<DateTime>()
        {
              DateTime.Parse("1987-01-01"),
              DateTime.Parse("1988-01-01"),
              DateTime.Parse("1989-01-01"),
              DateTime.Parse("1990-01-01"),
              DateTime.Parse("1991-01-01"),
              DateTime.Parse("1992-01-01"),
              DateTime.Parse("1993-01-01"),
              DateTime.Parse("1994-01-01"),
              DateTime.Parse("1995-01-01"),
              DateTime.Parse("1996-01-01")
        };


        public List<TimeSpan> timeSpans = new List<TimeSpan>()
        {
             TimeSpan.Parse("01:00:00.0000000"),
             TimeSpan.Parse("02:00:00.0000000"),
             TimeSpan.Parse("03:00:00.0000000"),
             TimeSpan.Parse("04:00:00.0000000"),
             TimeSpan.Parse("05:00:00.0000000"),
             TimeSpan.Parse("06:00:00.0000000"),
             TimeSpan.Parse("07:00:00.0000000"),
             TimeSpan.Parse("08:00:00.0000000"),
             TimeSpan.Parse("09:00:00.0000000"),
             TimeSpan.Parse("10:00:00.0000000"),
        };

        /// <summary>
        /// Returns  test value for a CLR type. The type needs to be in the format
        /// that would be in c# code not in System.Int32 format for example. 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public object GetTestValueCLR(string type, int index = 0) 
        {
            switch (type)
            {
                case "int":
                case "int?":
                case "short":
                case "short?":
                case "long":
                case "long?":
                    {
                        return index + 1; // 0 as test data stinks so make it 1
                    }
                case "bool":
                case "bool?":
                    {
                        return "true";
                    }
                case "byte[]":
                case "byte[]?":
                    {
                        return $"BitConverter.GetBytes({index + 1})";
                    }
                case "string":
                    {
                        return "\"" + $"{this.GetStringValue(index)}" + "\"";
                    }
                case "DateTime":
                case "DateTime?":
                    {
                        return "DateTime.Parse(\"" + $"{this.GetDateTimeValue(index)}" + "\")";
                    }
                case "DateTimeOffset":
                case "DateTimeOffset?":
                    {
                        return "DateTimeOffset.Parse(\"" + $"{this.GetDateTimeValue(index)}" + "\")";
                    }
                case "decimal":
                case "decimal?":
                    {
                        return this.GetDecimalValue(index) + "m";
                    }
                case "double":
                case "double?":
                case "float":
                case "float?":
                    {
                        return this.GetDecimalValue(index);
                    }
                case "object":
                case "object?":
                    {
                        return index + 1;
                    }
                case "Guid":
                case "Guid?":
                    {
                        return "Guid.Parse(\"" + $"{this.GetGuidValue(index)}" + "\")";
                    }
                case "TimeSpan":
                case "TimeSpan?":
                    {
                        return "TimeSpan.Parse(\"" + this.GetTimeValue(index) + "\")"; 
                    }
                default:
                    {
                        return $"TestDataRepository_GetTestValueCLR_unknown_field_type  type={type}";
                    }
            }
        }

        public object GetTestValue(Column column, int index=0)
        {

            switch (column.DataType)
            {
                case "bigint":
                    {
                        return index;
                    }
                case "binary":
                    {
                        return $"cast({index} as BINARY)";
                    }
                case "bit":
                    {
                        return 1;
                    }
                case "char":
                    {
                        return $"'{this.GetStringValue(index)}'";
                    }
                case "date":
                    {
                        return $"convert(datetime,'{this.GetDateTimeValue(index)}')";
                    }
                case "datetime":
                    {
                        return $"convert(datetime,'{this.GetDateTimeValue(index)}')";
                    }
                case "datetime2":
                    {
                        return  $"convert(datetime,'{this.GetDateTimeValue(index)}')";
                    }
                case "datetimeoffset":
                    {
                        return "CAST('2007-05-08 12:35:29.1234567 +12:15' AS datetimeoffset(7))";
                    }
                case "decimal":
                    {
                        return this.GetDecimalValue(index);
                    }
                case "float":
                    {
                        return this.GetDecimalValue(index);
                    }
                case "geography":
                    {
                        return "geography::STGeomFromText('LINESTRING(-122.360 47.656, -122.343 47.656 )', 4326)";
                    }
                case "geometry":
                    {
                        return "geometry::STGeomFromText('LINESTRING (100 100, 20 180, 180 180)', 0)";
                    }
                case "hierarchyid":
                    {
                        return $"'{this.GetGuidValue(index).ToString()}'";
                    }
                case "image":
                    {
                        return $"cast({index} as BINARY)";
                    }
                case "int":
                    {
                        return index;
                    }
                case "money":
                    {
                        return this.GetMoneyValue(index);
                    }
                case "nchar":
                    {
                        return $"'{this.GetStringValue(index)}'";
                    }
                case "ntext":
                    {
                        return $"'{this.GetStringValue(index)}'";
                    }
                case "numeric":
                    {
                        return this.GetDecimalValue(index);
                    }
                case "nvarchar":
                    {
                        return $"'{this.GetStringValue(index)}'";
                    }
                case "real":
                    {
                        return this.GetDecimalValue(index);
                    }
                case "smalldatetime":
                    {
                        return $"convert(datetime,'{this.GetDateTimeValue(index)}')";
                    }
                case "smallint":
                    {
                        return index;
                    }
                case "smallmoney":
                    {
                       return this.GetMoneyValue(index);
                    }
                case "sql_variant":
                    {
                        return index; // not sure what to put here. Index should be unique.
                    }
                case "text":
                    {
                        return $"'{this.GetStringValue(index)}'";
                    }
                case "time":
                    {
                        return $"'{this.GetTimeValue(index)}'";
                    }
                case "timestamp":
                    {
                        return $"DEFAULT";
                    }
                case "tinyint":
                    {
                        return this.GetTinyIntTestValue(index);
                    }
                case "uniqueidentifier":
                    {
                        return  $"'{this.GetGuidValue(index)}'";
                    }
                case "varbinary":
                    {
                        return $"cast({index} as BINARY)";
                    }
                case "varchar":
                    {
                        return $"'{this.GetStringValue(index)}'";
                    }
                case "xml":
                    {
                        return $"CONVERT(XML, '{this.GetXMLValue(index)}')";
                    }
                default:
                    {
                        return $"TestDataRepository_GetTestValue_unknown_field_type  type={column.DataType} name={column.Name}";
                    }
            }

        }



        public int GetIntTestValue(int index = 0)
        {
            return index;
            if (index > this.integers.Count)
            {
                throw new ArgumentException($"{index} is too large");
            }

            return this.integers[index];
        }


        public int GetSmallIntTestValue(int index = 0)
        {
            if (index > this.smallInts.Count)
            {
                throw new ArgumentException($"{index} is too large");
            }

            return this.smallInts[index];
        }

        public int GetTinyIntTestValue(int index = 0)
        {
            if (index > this.tinyInts.Count)
            {
                throw new ArgumentException($"{index} is too large");
            }

            return this.tinyInts[index];
        }

        public string GetStringValue(int index = 0)
        {
            if (index == 0)
            {
                return "A";

            }
            else if (index == 1)
            {
                return "B";
            }
            else if (index == 2)
            {
                return "C";
            }
            else
            {
                return index.ToString();
            }
        }

        public decimal GetDecimalValue(int index = 0)
        {
            if (index > this.decimals.Count)
            {
                throw new ArgumentException($"{index} is too large");
            }

            return Convert.ToDecimal(this.decimals[index]);
        }

        public decimal GetMoneyValue(int index = 0)
        {
            if (index > this.moneys.Count)
            {
                throw new ArgumentException($"{index} is too large");
            }

            return Convert.ToDecimal(this.moneys[index]);

        }
        public Guid GetGuidValue(int index = 0)
        {
            return this.GetDeterministicGuid(index.ToString());
        }
        public DateTime GetDateTimeValue(int index = 0)
        {
            return this.dateTimes[index];
        }

        public TimeSpan GetTimeValue(int index = 0)
        {
            if (index > this.timeSpans.Count)
            {
                throw new ArgumentException($"{index} is too large");
            }

            return this.timeSpans[index];
        }

        public string GetXMLValue(int index = 0)
        {
            return $"<xml>{index}</xml>";
        }

        public DateTime GetDateValue(int index = 0)
        {
            return this.dateTimes[index];
        }

        //http://geekswithblogs.net/EltonStoneman/archive/2008/06/26/generating-deterministic-guids.aspx
        private Guid GetDeterministicGuid(string input)

        {

            //use MD5 hash to get a 16-byte hash of the string:

            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();

            byte[] inputBytes = Encoding.Default.GetBytes(input);

            byte[] hashBytes = provider.ComputeHash(inputBytes);

            //generate a guid from the hash:

            Guid hashGuid = new Guid(hashBytes);

            return hashGuid;

        }
    }
}
 