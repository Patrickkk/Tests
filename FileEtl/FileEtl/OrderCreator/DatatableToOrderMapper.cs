using FileEtl.Checknet;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileEtl.Core.OrderCreator
{
    public class DatatableToOrderMapper
    {
        private DatatableToOrderMapperConfiguration configuration;

        public StagedOrder MapDataTableToStagedOrder(DataSet data)
        {
            var config = this.configuration;
            var table = data.Tables[config.StagedOrderDataTableName];

            var orders = CreateOrdersFromTable(config, table);

            throw new NotImplementedException();
        }

        private IEnumerable<StagedOrder> CreateOrdersFromTable(DatatableToOrderMapperConfiguration config, DataTable table)
        {
            var orderRows = table.Rows
                                 .Cast<DataRow>()
                                 .GroupBy(x => GroupByFieldsKeySelector(x, config.OrderFields));

            throw new NotImplementedException();
        }

        private string GroupByFieldsKeySelector(DataRow row, IEnumerable<string> fields)
        {
            return string.Join("-|-", fields.Select(x => row[x].ToString()));
        }

        // default mapping using fieldnames. Optional override different source or sources depending
        // on first filled value.
    }
}