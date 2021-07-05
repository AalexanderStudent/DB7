using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization; //
using System.Runtime.CompilerServices;

namespace Database_7
{
    static class Program
    {
        public static OleDbCommand AddParams(OleDbCommand cmd, List<String> qparams)
        {
            string toReplace = "";
            for (int i = 0; i < qparams.Count; i++)
            {
                toReplace = "@param" + (i + 1).ToString(CultureInfo.InvariantCulture);
                Console.WriteLine("{0}", toReplace);
                cmd.Parameters.AddWithValue(toReplace, qparams[i]);
            }
            return cmd;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Txt(string textField)
        {
            return '"' + textField + '"';  // like in f
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OleDbCommand CreateOleDbCommand(OleDbConnection cn, string query)
        {
            return new OleDbCommand
            {
                Connection = cn,
                CommandText = query
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OleDbConnection ConnectToMyDatabase()
        {
            return new OleDbConnection(
                @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\ADMIN\Desktop\Университет.accdb");
        }

        public static List<String> GetReaderDataList(OleDbDataReader readerData, int columsInTheTable)
        {
            List<String> readerDataList = new List<String>();
            if (readerData.HasRows)
            {
                while (readerData.Read())
                {
                    for (int i = 0; i < columsInTheTable; i++)
                    {
                        if (readerData[i].ToString() == "")
                            readerDataList.Add("NULL");
                        else 
                            readerDataList.Add(readerData[i].ToString());
                    }
                    readerDataList.Add("");
                }
            }
            return readerDataList;
        }

        public static void ExecuteQuery(string query, List<String> qparams)
        {
            OleDbConnection cn = ConnectToMyDatabase();
            try
            {
                cn.Open();
                OleDbCommand cmd = AddParams(CreateOleDbCommand(cn, query), qparams);
                //Console.WriteLine("{0}, {1}", cmd.CommandText, cmd.Parameters.ToString()); //
                cmd.ExecuteNonQuery();
            }
            finally
            {
                cn.Close();
            }
        }

        /*
        public static List<String> ExecuteReaderQuery(string query, int columsInTheTable) // no longer in use
        {
            OleDbConnection cn = ConnectToMyDatabase();
            List<String> result;
            try 
            { 
                cn.Open();
                OleDbCommand cmd = CreateOleDbCommand(cn, query);
                // Console.WriteLine("{0}", cmd.);
                result = GetReaderDataList(cmd.ExecuteReader(), columsInTheTable);
            }
            finally
            { 
                cn.Close();
            }
            return result;
        }
        

        public static String PushString(string[] columnsContent) // inefficient
        {
            string insertionValues = "";
            for (int i = 0; i < columnsContent.Length - 1; i++)
            {
                insertionValues += columnsContent[i] + ',';
            }
            insertionValues += columnsContent[columnsContent.Length - 1];
            return insertionValues;
        }
        */

        public static List<String> SelectFromDB(string Table, string Condition, List<String> qparams, int columsInTheTable)
        {

            string selectQuery = "SELECT * FROM " + Table + " WHERE " + Condition + ";";
            OleDbConnection cn = ConnectToMyDatabase();
            List<String> result;
            try
            {
                cn.Open();
                OleDbCommand cmd = CreateOleDbCommand(cn, selectQuery);
                if (qparams?.Any() != false)
                {
                    cmd = AddParams(cmd, qparams);
                }
                else
                    Console.WriteLine("Notice: Command has no params.");
                Console.WriteLine("{0}", cmd.CommandText); 
                result = GetReaderDataList(cmd.ExecuteReader(), columsInTheTable);
            }
            finally
            {
                cn.Close();
            }
            return result;
            //return ExecuteReaderQuery(selectQuery, columsInTheTable);
        }

        public static void InsertIntoDB(string table, List<String> qparams)
        {
            string insertQuery = "INSERT INTO " + table + " VALUES (";
            for (int i = 0; i < qparams.Count - 1; i++)
                insertQuery += "@param" + (i + 1).ToString(CultureInfo.InvariantCulture) + ", ";
            insertQuery += "@param" + (qparams.Count).ToString(CultureInfo.InvariantCulture) + ");";
            ExecuteQuery(insertQuery, qparams);
        }

        public static void InsertIntoDBComplex(string table, string columns, string complexPart, List<String> qparams)
        {
            // complexPart should be already assembled
            string insertQuery = "INSERT INTO " + table + " (" + columns + ") " + complexPart;
            ExecuteQuery(insertQuery, qparams);
        }

        public static void DeleteFromDB(string table, string condition, List<String> qparams)
        {
            string deleteQuery = "DELETE FROM " + table + " WHERE " + condition + ";";
            ExecuteQuery(deleteQuery, qparams);
            Console.WriteLine("{0}", deleteQuery);
        }

        public static void HandleSelectResult(List<String> results, ListBox listBoxOnly)
        {
            if (results?.Any() == false)
                listBoxOnly.Items.Add("No entry matching your specification found");
            else
            {
                foreach (String i in results)
                    Console.WriteLine("{0}", i);
                foreach (String i in results)
                    listBoxOnly.Items.Add(i);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OleDbConnection ConnectToMySystemDatabase()
        {
            return new OleDbConnection(
                @"Provider = Microsoft.ACE.OLEDB.12.0;" +
                @"Data Source = C:\Users\ADMIN\Desktop\Университет.accdb;" +
                @"Jet OLEDB:Create System Database=true;" + // разрешение на доступ
                @"Jet OLEDB:System database=C:\Users\ADMIN\AppData\Roaming\Microsoft\Access\System.mdw");
        }

        public static List<String> SystemDatabaseReader(string whereQuery, List<String> qparams)
        {
            OleDbConnection cn = ConnectToMySystemDatabase();
            List<String> result;
            try
            {
                cn.Open();
                OleDbCommand cmd2;
                if (qparams?.Any() == false)
                { 
                    if (whereQuery == "")
                        cmd2 = CreateOleDbCommand(cn, "SELECT * FROM MSysObjects;");
                    else
                        cmd2 = CreateOleDbCommand(cn, "SELECT * FROM MSysObjects WHERE " + whereQuery + ";");
                }
                else
                {
                    cmd2 = AddParams(CreateOleDbCommand(cn, "SELECT * FROM MSysObjects WHERE " + whereQuery + ";")
                        , qparams);
                }
                Console.WriteLine("{0}", cmd2.CommandText); //
                result = GetReaderDataList(cmd2.ExecuteReader(), 17);
            }
            finally
            { 
                cn.Close();
            }
            return result;
        }

        public static List<string> BreakMessageIntoParts(string message, int portionSize)
        {
            List<string> messageParts = new List<string>();
            int messageLength = message.Length;
            int numberOfSegments = (int)(messageLength / portionSize) + 1;
            int sizeOfTheLastPart = messageLength - (numberOfSegments - 1) * portionSize;
            //Console.WriteLine("{0}, {1}, {2}", messageLength, numberOfSegments, sizeOfTheLastPart);
            string temporaryStorage = "";
            for (int i = 0; i < numberOfSegments - 1; i++)
            {
                for (int j = 0; j < portionSize; j++)
                    temporaryStorage += message[j + i * portionSize];
                messageParts.Add(temporaryStorage + "-");
                //Console.WriteLine("{0}", temporaryStorage);
                temporaryStorage = "";
            }
            for (int j = 0; j < sizeOfTheLastPart; j++)
                temporaryStorage += message[j + (numberOfSegments - 1) * portionSize];
            messageParts.Add(temporaryStorage);
            return messageParts;
        }

        public static void ReportOleDbExceptionMessage(OleDbException e, ListBox listBoxOnly)
        {
            int portionSize = 40;
            if (e.Message.Length > portionSize)
            {
                List<string> messageParts = BreakMessageIntoParts(e.Message, portionSize);
                foreach (string i in messageParts)
                    listBoxOnly.Items.Add(i);
            }
            else
            { 
                listBoxOnly.Items.Add(e.Message);
            }
            Console.WriteLine("{0}", e.Message);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form0());
        }
    }
}
