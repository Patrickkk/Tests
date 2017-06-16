using System;
using System.Collections.Generic;

namespace FileEtl.FileReaders.DataTableSetup
{
    public interface IDataTableConfig
    {
        List<IDataTableRecord> Records { get; }
    }

    public interface IDataTableRecord
    {
        string Name { get; set; }

        string Description { get; set; }

        string TableName { get; set; }

        bool Required { get; set; }

        List<IDataRecordField> Fields { get; }
    }

    public class DataRecordConfigBase : IDataTableConfig
    {
        public List<IDataTableRecord> Records { get; } = new List<IDataTableRecord>();
    }

    public interface IDataRecordField
    {
        string Name { get; set; }

        Type Type { get; set; }

        int Position { get; set; }

        object DefaultValue { get; set; }
    }
}