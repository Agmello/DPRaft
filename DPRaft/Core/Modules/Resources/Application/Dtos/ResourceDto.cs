using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Resources.Application.Dtos
{
    public class ResourceDto
    {
        public string Key { get; set; }
        public double Amount { get; set; }
        public ResourceDto(string key, double amount)
        {
            Key = key;
            Amount = amount;
        }
    }
}
