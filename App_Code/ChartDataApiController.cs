using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Umbraco.Web.WebApi;
using Umbraco.Web.Editors;
using Umbraco.Core.Persistence;

namespace My.Controllers
{
    [Umbraco.Web.Mvc.PluginController("My")]
    public class ChartDataApiController : UmbracoApiController
    {
        public IEnumerable<ChartSeries> GetAll()
        {
            var db = UmbracoContext.Application.DatabaseContext.Database;
            var q = new Sql()
                .Select("CAST([dCreatedDate] AS DATE) as 'd',[sUpdatedBy] as 's', count(*) as 'c'")
                .From("Message")
                .Where("[sUpdatedBy] is not null")
                .GroupBy("CAST([dCreatedDate] AS DATE),[sUpdatedBy]");
            var cd = db.Fetch<ChartData>(q);
            var creators = (from row in cd
                           select row.s).Distinct();

            List<ChartSeries> allSeries = new List<ChartSeries>();
            foreach(var creator in creators)
            {
                var series = (from row in cd select row).Where(p => p.s == creator).ToList();
                ChartSeries chartSeries = new ChartSeries();

                chartSeries.s = creator;
                chartSeries.cd = series;
                allSeries.Add(chartSeries);
            }
            return allSeries;
        }

        public IEnumerable<ChartData> GetAll2()
        {
            var db = UmbracoContext.Application.DatabaseContext.Database;
            var q = new Sql()
                .Select("CAST([dCreatedDate] AS DATE) as 'd',[sUpdatedBy] as 's', count(*) as 'c'")
                .From("Message")
                .Where("[sUpdatedBy] is not null")
                .GroupBy("CAST([dCreatedDate] AS DATE),[sUpdatedBy]");
            return db.Fetch<ChartData>(q);
        }
    }
    public class ChartSeries
    {
        public string s { get; set; }
        public List<ChartData> cd { get; set; }
    }
    public class ChartData
    {
        public DateTime d { get; set; }
        public string s { get; set; }
        public uint c { get; set; }
    }
}