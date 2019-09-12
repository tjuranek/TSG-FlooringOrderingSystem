using System.Collections.Generic;

namespace FOS.Models.Interfaces
{
    public interface ITaxRepository
    {
        List<Tax> RetrieveTaxes();
    }
}
