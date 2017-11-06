using System.Collections.Generic;

namespace FileEtl.Checknet
{
    public class StagedOrderLine
    {
        public List<StagedOrderLineItem> LineItems { get; set; }
    }
}