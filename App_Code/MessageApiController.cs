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
    public class MessageApiController : UmbracoApiController
    {
        public IEnumerable<Message> GetAll()
        {
            //get the database
            var db = UmbracoContext.Application.DatabaseContext.Database;
            //build a query to select everything the people table
            var query = new Sql().Select("sCreatedBy as 'From', sSubject as 'Content', dCreatedDate as 'Date'").From("Message");
            //fetch data from DB with the query and map to Person object
            return db.Fetch<Message>(query);
        }
    }
    public class Message
    {
        public string From { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}