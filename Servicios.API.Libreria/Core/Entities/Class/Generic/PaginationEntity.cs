using System.Collections.Generic;

namespace Servicios.API.Libreria.Core.Entities.Class.Generic
{
    public class PaginationEntity<TDocument>
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
        public string Sort { get; set; }
        public string SortDirection { get; set; }
        public FilterValue FilterValue {get; set; }
        public int TotalRows { get; set; }
        public int PageQuantity { get; set; }
        public IEnumerable<TDocument> Data { get; set; }
    }

    public class FilterValue
    {
        public string Propiedad { get; set; }
        public string Valor { get; set; }
    }
}
