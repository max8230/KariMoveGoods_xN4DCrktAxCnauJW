using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MoveGoods.Controllers
{
    public class StoreItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class StoresSearchController : ApiController
    {
        private MGDBContainer db = new MGDBContainer();
        // GET: api/StoresSearch
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public IEnumerable<StoreItem> Get(string filter)
        {
            try
            {
                return db.StoreSet.Where(x => x.Name.IndexOf(filter) >= 0 || x.Id.IndexOf(filter) >= 0).ToList().Select((y, z) => new StoreItem() { Id = y.Id, Name = y.Name });                
            }
            catch
            {
                return null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
