using System.Collections.Generic;

namespace Ng2PowerTable.DataSource.Core
{
    public class DataSourceResult<T>
    {
        public int recordsTotal { get; set; }

        public int recordsFiltered { get; set; }

        public List<T> data { get; set; }
    }
}