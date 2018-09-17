using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MoveGoods;

namespace MoveGoods.Controllers
{
    public class StoresData
    {
        public int managerId { get; set; }
        public string[] storesId { get; set; }
    }

    public class StoreData
    {
        public int managerId { get; set; }
        public string storeId { get; set; }
    }

    public class StoreModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class StoresMgrController : ApiController
    {
        private MGDBContainer db = new MGDBContainer();

        // GET: api/StoresMgr
        [HttpGet]
        public IEnumerable<Store> GetStores(int managerId)
        {
            try
            {
                return db.ManagerSet.First(x => x.Id == managerId).Stores.ToList();
            }
            catch
            {
                return null;
            }
        }

        [HttpPost]
        public IEnumerable<StoreModel> PostStore([FromBody] StoresData data)
        {
            try
            {
                var m = db.ManagerSet.First(x => x.Id == data.managerId);                                              
                foreach (string storeId in data.storesId)
                {                    
                    if (m.Stores.FirstOrDefault(x => x.Id.Equals(storeId)) == null)
                    {
                        Store s = db.StoreSet.First(x => x.Id.Equals(storeId) == true);
                        m.Stores.Add(s);                        
                    }
                }
                db.SaveChanges();
                return m.Stores.Select((x,y) => new StoreModel() { Id = x.Id, Name = x.Name });
            }
            catch
            {
                return null ;
            }
        }

        [HttpDelete]
        public bool DeleteStore([FromBody] StoreData store)
        {            
            try
            {
                Store s = new Store()
                {
                    Id = store.storeId
                };
                db.StoreSet.Attach(s);
                db.ManagerSet.First(x => x.Id == store.managerId).Stores.Remove(s);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
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