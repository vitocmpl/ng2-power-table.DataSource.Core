using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;

namespace Ng2PowerTable.DataSource.Core
{
    public static class ExtensionMethods
    {
        public static async Task<DataSourceResult<T>> ToDataSourceResult<T>(this IQueryable<T> query, DataSourceRequest parameters)
        {
            return await Task.Run(() => {
                int totalCount = query.Count();

                if(parameters.orders.Any())
                {
                    string orderBy = string.Join(",", 
                        parameters.orders.Where(p => p.dir == "asc" || p.dir == "desc")
                            .Select(p => string.Format("{0}{1}", p.name, p.dir == "desc" ? " descending" : ""))
                            .ToArray());
                    query = query.OrderBy(orderBy);
                }

                if(!string.IsNullOrEmpty(parameters.fullTextFilter))
                {
                    string fullTextSearchValue = parameters.fullTextFilter.Trim().ToLower();
                    string fullTextWhere = string.Join(" Or ", 
                        parameters.filters.Where(p => p.type == "default" && typeof(T).GetProperty(p.name)?.PropertyType == typeof(string)).Select(p => 
                            string.Format(@"{0}.Trim().ToLower().Contains(""{1}"")", p.name, fullTextSearchValue)).ToList());

                    if(!string.IsNullOrEmpty(fullTextWhere))
                    {
                        query = query.Where(fullTextWhere);
                    }
                }

                foreach (var filter in parameters.filters.Where(p => p.value != null))
                {
                    if(typeof(T).GetProperty(filter.name) == null)
                        continue;

                    if(filter.value is string && string.IsNullOrEmpty(filter.value))
                        continue;

                    if(filter.type == "default" || filter.type == "text")
                    {
                        string searchValue = filter.value.ToString().Trim().ToLower();
                        if(typeof(T).GetProperty(filter.name)?.PropertyType == typeof(string))
                        {
                            query = query.Where(string.Format("{0}.Trim().ToLower().Contains(@0)", filter.name), searchValue);
                        }
                    }
                    else
                    {
                        object realValue = filter.value is string ? 
                            TypeDescriptor.GetConverter(typeof(T).GetProperty(filter.name)?.PropertyType).ConvertFromString((string)filter.value) : 
                            filter.value;
                        
                        if(filter.type == "equals")
                        {
                            query = query.Where(string.Format("{0} == @0", filter.name), realValue);
                        }
                    }
                }

                var displayedMembers = query
                    .Skip(parameters.start)
                    .Take(parameters.length)
                    .ToList();

                return new DataSourceResult<T>
                {
                    recordsTotal = totalCount,
                    recordsFiltered = query.Count(),
                    data = displayedMembers
                };
            });
        }

        public static DataSourceResult<TResult> Select<TSource, TResult>(this DataSourceResult<TSource> startResult, Func<TSource, TResult> selector)
        {
            DataSourceResult<TResult> result = new DataSourceResult<TResult>
            {
                recordsTotal = startResult.recordsTotal,
                recordsFiltered = startResult.recordsFiltered
            };

            result.data = startResult.data.Select(selector).ToList();

            return result;
        } 
    }
}