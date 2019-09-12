using System.Collections.Generic;
using FOS.Models;
using FOS.Models.Interfaces;

namespace FOS.Data.Development
{
    public class DevelopmentTaxesRepository : ITaxRepository
    {
        public List<Tax> RetrieveTaxes()
        {
            List<Tax> taxes = new List<Tax>();

            taxes.Add(new Tax
            {
                StateAbbreviation = "OH",
                StateName = "Ohio",
                TaxRate = 6.25m
            });

            taxes.Add(new Tax
            {
                StateAbbreviation = "PA",
                StateName = "Pensylvania",
                TaxRate = 6.75m
            });

            taxes.Add(new Tax
            {
                StateAbbreviation = "MI",
                StateName = "Michigan",
                TaxRate = 5.75m
            });

            taxes.Add(new Tax
            {
                StateAbbreviation = "IN",
                StateName = "Indiana",
                TaxRate = 6.00m
            });

            return taxes;
        }
    }
}
