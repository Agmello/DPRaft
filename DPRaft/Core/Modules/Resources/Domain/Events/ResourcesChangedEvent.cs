using Core.Infrastructure.Logger;
using Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Resources.Domain.Events
{
    public class ResourcesChangedEvent : Event
    {
        public double NewAmount { get; }
        public double AmountChanged { get; }
        public string Resource { get; }
        public ResourcesChangedEvent(string resource, double newAmount, double amountChanged)
        {
            Resource = resource ?? throw new ArgumentNullException(nameof(resource));
            NewAmount = newAmount;
            AmountChanged = amountChanged;
        }

        public override string LogMessage()
        {
            return $"{nameof(ResourcesChangedEvent)}: {Resource} <{NewAmount}> ({AmountChanged})";
        }
    }
}
