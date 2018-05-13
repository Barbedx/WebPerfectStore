using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebPerfectStore.Models;

namespace WebPerfectStore.Controllers
{
    [Authorize]
    public class OutletsController : BaseController
    {
        // GET: Outlets
        [Authorize]
        public async Task<ActionResult> IndexAsync()
        {
            var user = await GetCurrentUserAsync();
            return View(user.Outlets.ToList());
        }
        public    ActionResult Index()
        {
            var user = CurrentUser;
            return View(CurrentUser.Outlets.ToList());
        }
        public async Task<ActionResult> SelectDocument(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.OutletID = id;
            return View(await db.DocTypes.Where(x => x.Activeflag==1).ToListAsync());
        }
        // GET: Outlets/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outlet outlets = db.Outlets.Include(x => x.OutletAttributes).FirstOrDefault(x => x.fID == id);
            if (outlets == null)
            {
                return HttpNotFound();
            }
            return View(outlets);
        }

        // GET: Outlets/Create
        public ActionResult Create()
        {
            ViewBag.fID = new SelectList(db.OutletAttributes, "Id", "AttrName");
            return View();
        }

        // POST: Outlets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fID,fActiveFlag,fName,fAddress,CityC,POP,naid,outletTC,StoreArea,Tier,Traffic,fcomment,Coordinates,masterfid")] Outlet outlet)
        {
            if (ModelState.IsValid)
            {
                db.Outlets.Add(outlet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fID = new SelectList(db.OutletAttributes, "Id", "AttrName", outlet.fID);
            return View(outlet);
        }

        // GET: outlets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outlet outlets = db.Outlets.Find(id);
            if (outlets == null)
            {
                return HttpNotFound();
            }
            ViewBag.fID = new SelectList(db.OutletAttributes, "Id", "AttrName", outlets.fID);
            return View(outlets);
        }

        // POST: outlets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fID,fActiveFlag,fName,fAddress,CityC,POP,naid,outletTC,StoreArea,Tier,Traffic,fcomment,Coordinates,masterfid")] Outlet outlets)
        {
            if (ModelState.IsValid)
            {
                db.Entry(outlets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fID = new SelectList(db.OutletAttributes, "Id", "AttrName", outlets.fID);
            return View(outlets);
        }

        // GET: outlets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outlet outlets = db.Outlets.Find(id);
            if (outlets == null)
            {
                return HttpNotFound();
            }
            return View(outlets);
        }

        // POST: outlets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Outlet outlet = db.Outlets.Find(id);
            db.Outlets.Remove(outlet);
            db.SaveChanges();
            return RedirectToAction("Index");
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
