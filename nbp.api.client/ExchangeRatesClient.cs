using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using nbp.api.client.queries;

namespace nbp.api.client
{
    public abstract class ExchangeRatesClient<T> : ExchangeClient
    {

        protected ExchangeRatesClient(string apiBaseUrl) : base(apiBaseUrl)
        {
        }
        public abstract Task<T> FetchRatesSeries(ExchangeRatesSeriesQuery query);

        public abstract Task<IEnumerable<T>> FetchRatesSeriesList(ExchangeRatesSeriesQuery query);
        
        public static string GetEndpointUrl(ExchangeRatesSeriesQuery query)
        {
            var sb = new StringBuilder("api/exchangerates/");
            if (string.IsNullOrEmpty(query.Code))
            {
                sb.Append("tables/");
                sb.Append(query.Table);
                sb.Append('/');
            }
            else
            {
                sb.Append("rates/");
                sb.Append(query.Table);
                sb.Append('/');
                sb.Append(query.Code);
                sb.Append('/');
            }
            if (query is ExchangeRatesSeriesLastQuery lastQuery)
            {
                sb.Append("last/");
                sb.Append(lastQuery.Count);
                sb.Append('/');
            }
            else if (query is ExchangeRatesSeriesTodayQuery todayQuery)
            {
                sb.Append("today/");
            }
            else if (query is ExchangeRatesSeriesDateQuery dateQuery)
            {
                sb.Append(dateQuery.Date.ToString("yyyy-MM-dd"));
                sb.Append('/');
            }
            else if (query is ExchangeRatesSeriesDateRangeQuery dateRangeQuery)
            {
                sb.Append(dateRangeQuery.StartDate.ToString("yyyy-MM-dd"));
                sb.Append('/');
                sb.Append(dateRangeQuery.EndDate.ToString("yyyy-MM-dd"));
                sb.Append('/');
            }
            return sb.ToString().ToLower();
        }

    }
}
