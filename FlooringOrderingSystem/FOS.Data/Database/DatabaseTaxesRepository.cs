using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using FOS.Models;
using FOS.Models.Interfaces;

namespace FOS.Data.Database
{
    public class DatabaseTaxesRepository : ITaxRepository
    {
        public List<Tax> RetrieveTaxes()
        {
            List<Tax> taxes = new List<Tax>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Data Source=DESKTOP-VN0TUCD; Initial Catalog=Flooring; Integrated Security=True; Trusted_Connection=true";
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT State.StateAbbreviation, State.StateName, Tax.TaxRate FROM StateTax INNER JOIN State ON State.StateID = StateTax.StateID INNER JOIN Tax ON Tax.TaxID = StateTax.TaxID;", conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        taxes.Add(new Tax
                        {
                            StateAbbreviation = reader[0].ToString(),
                            StateName = reader[1].ToString(),
                            TaxRate = decimal.Parse(reader[2].ToString())
                        });
                    }
                }
            }

            return taxes;
        }
    }
}
