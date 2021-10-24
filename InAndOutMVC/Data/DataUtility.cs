using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOutMVC.Data
{
    public class DataUtility
    {
        public static string GetConnectionString(IConfiguration configuration)
        {
            //The default connection string will come from appSettings as default
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            //It will be automatically overwritten if we are running on Heroku
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            //If databaseUrl is empty, use connectionString(locally) otherwise build connection string based on the value of databaseUrl
            return string.IsNullOrEmpty(databaseUrl) ? connectionString : BuildConnectionString(databaseUrl);
        }

        public static string BuildConnectionString(string databaseUrl)
        {
            //Provides an object representation of a uniform resource identifier, uri and split the string with colon for easier processing
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            //Provides a simple way to create and manage the contents of connection string
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'), //Trims the / off
                SslMode = SslMode.Prefer,
                TrustServerCertificate = true
            };

            //Convert builder to string and return it
            return builder.ToString();
        }
    }
}
