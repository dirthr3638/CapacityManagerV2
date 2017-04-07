using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Search.Interop;
using System.Data;
using System.Data.OleDb;

namespace CapacityManagerMain.SysIndexSearch
{
    class IndexSearcher
    {
        private void Write(DataTable dt, string outputFilePath)
        {
            int[] maxLengths = new int[dt.Columns.Count];

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                maxLengths[i] = dt.Columns[i].ColumnName.Length;

                foreach (DataRow row in dt.Rows)
                {
                    if (!row.IsNull(i))
                    {
                        int length = row[i].ToString().Length;

                        if (length > maxLengths[i])
                        {
                            maxLengths[i] = length;
                        }
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter(outputFilePath, false))
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sw.Write(dt.Columns[i].ColumnName.PadRight(maxLengths[i] + 2));
                }

                sw.WriteLine();

                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (!row.IsNull(i))
                        {
                            sw.Write(row[i].ToString().PadRight(maxLengths[i] + 2));
                        }
                        else
                        {
                            sw.Write(new string(' ', maxLengths[i] + 2));
                        }
                    }

                    sw.WriteLine();
                }

                sw.Close();
            }
        }

        public void test()
        {
            String pattern = "*";

            CSearchManager manager = new CSearchManager();

            // SystemIndex catalog is the default catalog in Windows
            ISearchCatalogManager catalogManager = manager.GetCatalog("SystemIndex");

            // Get the ISearchQueryHelper which will help us to translate AQS --> SQL necessary to query the indexer
            ISearchQueryHelper queryHelper = catalogManager.GetQueryHelper();

            // Set the number of results we want. Don't set this property if all results are needed.
            //queryHelper.QueryMaxResults = 99999;

            // Set list of columns we want
            queryHelper.QuerySelectColumns = "\"System.ItemNameDisplay\",\"System.FileExtension\",\"System.ItemFolderPathDisplay\"";
            
            // Set additional query restriction
            //queryHelper.QueryWhereRestrictions = "AND scope='file:'";

            // convert file pattern if it is not '*'. Don't create restriction for '*' as it includes all files.
            if (pattern != "*")
            {
                pattern = pattern.Replace("*", "%");
                pattern = pattern.Replace("?", "_");

                if (pattern.Contains("%") || pattern.Contains("_"))
                {
                    queryHelper.QueryWhereRestrictions += " AND System.FileName LIKE '" + pattern + "' ";
                }
                else
                {
                    // if there are no wildcards we can use a contains which is much faster as it uses the index
                    queryHelper.QueryWhereRestrictions += " AND Contains(System.FileName, '" + pattern + "') ";
                }
            }

            //queryHelper.QuerySorting = "System.DateModified DESC";

            // Generate SQL from our parameters, converting the userQuery from AQS->WHERE clause
            string sqlQuery = queryHelper.GenerateSQLFromUserQuery("*");
            //Console.WriteLine(sqlQuery);

            using (System.Data.OleDb.OleDbConnection conn = new OleDbConnection(queryHelper.ConnectionString))
            {
                // open the connection
                conn.Open();
                // now create an OleDB command object with the query we built above and the connection we just opened.
                using (OleDbCommand command = new OleDbCommand(sqlQuery, conn))
                {
                    OleDbDataAdapter dsAdapt = new OleDbDataAdapter(command);

                    DataSet ds = new DataSet();

                    dsAdapt.Fill(ds);
                    ds.WriteXml("20170216.xml");
                }
                
            }

        }

    }
}
