using System.Collections.Generic;

namespace FileEtl.Core.OrderCreator
{
    public class DatatableToOrderMapperConfiguration
    {
        public string AddressTableName { get; set; } = "";

        public IEnumerable<string> OrderFields { get; internal set; }
        public string StagedOrderDataTableName { get; set; } = "StagedOrderData";
    }
}