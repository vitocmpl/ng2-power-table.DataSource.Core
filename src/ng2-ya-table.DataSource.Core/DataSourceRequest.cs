namespace Ng2YaTable.DataSource.Core
{
    public class DataSourceRequest
    {
        public int start { get; set; }

        public int length { get; set; }

        public DataSourceRequestOrder[] orders { get; set; }

        public DataSourceRequestFilter[] filters { get; set; }

        public string fullTextFilter { get; set; }
    }

    public class DataSourceRequestOrder
    {
        public string name { get; set; }

        public string dir { get; set; }
    }

    public class DataSourceRequestFilter
    {
        public string name { get; set; }

        public dynamic value { get; set; }

        public string type { get; set; }
    }
}