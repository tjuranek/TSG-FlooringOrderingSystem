using System;
using System.Collections.Generic;
using System.IO;
using FOS.Models;
using FOS.Models.Interfaces;

namespace FOS.Data.Production
{
    public class ProductionTaxesRepository : ITaxRepository
    {
        private string _path = "";

        public ProductionTaxesRepository(string path)
        {
            _path = path;
        }

        public List<Tax> RetrieveTaxes()
        {
            List<Tax> taxes = new List<Tax>();

            try
            {
                using (StreamReader sr = new StreamReader(_path))
                {
                    sr.ReadLine(); //this skips the header line in the file

                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] columns = line.Split('|');

                        Tax tax = new Tax();
                        tax.StateAbbreviation = columns[0];
                        tax.StateName = columns[1];
                        tax.TaxRate = decimal.Parse(columns[2]);

                        taxes.Add(tax);
                    } 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured reading the file: {ex.Message}");
            }

            if (taxes.Count > 0)
                return taxes;

            return null;
        }
    }
}
