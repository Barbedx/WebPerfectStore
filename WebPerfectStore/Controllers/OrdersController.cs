using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using WebPerfectStore.Models;
using WebPerfectStore.ViewModels;
using System;
using System.Data.SqlClient;

namespace WebPerfectStore.Controllers
{
    [Authorize]
    public class OrdersController : BaseController
    {


        // GET: Orders
        public async Task<ActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var orders = db.Orders
                .Include(o => o.DocType)
                .Include(o => o.MasterAgent)
                .Include(o => o.Outlet)
                .Where(x => x.MasterFID == user.FId);
            return View(await orders.ToListAsync());
        }
        // GET: Orders/Details/5
        public async Task<ActionResult> Details(int? orid)
        {
            if (orid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await GetCurrentUserAsync();

            var order = user.Orders.FirstOrDefault(o => o.ID == orid);

            if (order == null)
            {
                return HttpNotFound();
            }

            OrderViewModel orderViewModel = PopulateOrderVM(order);

            //var answers = await db.Answers.Where(a => a.OrderID == orid && a.MasterFid == user.FId).ToListAsync();
            //Order order = await db.Orders.Include(o => o.Answers).FirstOrDefaultAsync(x => x.ID == orid && x.MasterFID == user.FId);



            return View(orderViewModel);
        }

        private static OrderViewModel PopulateOrderVM(Order order)
        {
            var answers = order.Answers.ToList();

            var items = answers.GroupBy(g => g.Item).Select(group => group.Key).ToList();//answers.Select(x => x.Item);

            var questions = new List<QuestionViewModel>();

            foreach (var question in items)
            {
                var q = new QuestionViewModel()
                {
                    Item = question,
                    Answers = new List<AnswerViewModel>()
                };
                var itemAnswers = answers.Where(w => w.ItemId == q.Item.ID).ToList();
                foreach (var answer in itemAnswers)
                {
                    q.Answers.Add(new AnswerViewModel()
                    {
                        Attribute = answer.Attribute,// await db.Attributes.FirstOrDefaultAsync(a => a.AttrID == answer.AttrId),
                        Value = answer.Value
                    });
                }
                questions.Add(q);

            }
            var orderViewModel = new OrderViewModel()
            {
                Order = order,
                Questions = questions
            };
            return orderViewModel;
        }

        // GET: Orders/Create
        public async Task<ActionResult> Create(int? outletID, int? dtid)
        {
            if (outletID == null || dtid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await GetCurrentUserAsync();

            //var order = user.Orders.FirstOrDefault(o => o.ID == orid);

            //if (order == null)
            //{
            //    return HttpNotFound();
            //}
            var order = new Order()
            {
                OutletID = outletID,
                DocTypeID = dtid,
                MasterFID = user.FId,
                Outlet = await db.Outlets.FirstOrDefaultAsync(o => o.fID == outletID),
                MasterAgent = user
            };

            OrderViewModel orderViewModel = await GetOrderViewModel(order);

            return View(orderViewModel);


        }
        /// <summary>
        /// Все вопросЫ сс ответами и атрибутами для документа, или с пустыми если таких нету.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private async Task<OrderViewModel> GetOrderViewModel(Order order)
        {
            var docitems = await db.DocTypes.Include(x => x.Items).FirstOrDefaultAsync(x => x.DocTypeID == order.DocTypeID);
            //var answers = order.Answers.ToList();
            var headers = docitems.Attributes.OrderBy(x => x.Sort);
            var items = docitems.Items.OrderBy(x => x.Sort);// answers.GroupBy(g => g.Item).Select(group => group.Key).ToList();//answers.Select(x => x.Item);


            var questions = new List<QuestionViewModel>();

            foreach (var question in items)
            {
                var q = new QuestionViewModel()
                {
                    Item = question,
                    Answers = new List<AnswerViewModel>()
                };
                //var itemAnswers = answers.Where(w => w.ItemId == q.Item.ID).ToList();
                foreach (var answer in headers)
                {
                    q.Answers.Add(new AnswerViewModel()
                    {
                        Attribute = answer,// await db.Attributes.FirstOrDefaultAsync(a => a.AttrID == answer.AttrId),

                        Value = order.Answers.FirstOrDefault(a =>
                        a.AttrId == answer.AttrID &&
                        a.ItemId == question.ID &&
                        a.MasterFid == order.MasterFID &&
                        a.OrderID == order.ID)?.Value
                    });
                }
                questions.Add(q);
            }

            var orderViewModel = new OrderViewModel()
            {
                Order = order,
                Questions = questions
            };
            return orderViewModel;
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MasterFID,ID,Number,OutletID,OrderDate,Comment,Sum,DocTypeID,Condition,MasterDocID,MasterDocMasterfID")] Order order)
        public async Task<ActionResult> Create([Bind] OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                Order order = PopulateOrderFromVM(orderViewModel);
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Orders.Add(order);
                        await db.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
                // orderViewModel.Order.Create();
                //db.Orders.Add(order);
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }

            //ViewBag.DocTypeID = new SelectList(db.DocTypes, "DocTypeID", "DocTypeName", order.DocTypeID);
            //ViewBag.MasterFID = new SelectList(db.Agents, "FId", "Name", order.MasterFID);
            return RedirectToAction(nameof(Index));
        }

        private static Order PopulateOrderFromVM(OrderViewModel orderViewModel)
        {
            var order = orderViewModel.Order;
            foreach (var question in orderViewModel.Questions)
            {
                var q = question.Item;
                foreach (var answer in question.Answers)
                {
                    order.Answers.Add(new Answer(MasterFid: order.MasterFID, orID: order.ID, ItemId: q.ID, AttrId: answer.Attribute.AttrID, Value: answer.Value));
                }
            }
            return order;
        }

        // GET: Orders/Edit/5
        public async Task<ActionResult> Edit(int? orid)
        {
            if (orid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await GetCurrentUserAsync();

            var order = user.Orders.FirstOrDefault(o => o.ID == orid);
            if (order == null)
            {
                return HttpNotFound();
            }

            var orderVM = await GetOrderViewModel(order);
            return View(orderVM);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind] OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                var order = PopulateOrderFromVM(orderViewModel);
                db.Entry(order).State = EntityState.Modified;
                foreach (var answer in order.Answers)
                {
                    db.Entry(answer).State = string.IsNullOrWhiteSpace(answer.Value) ?
                     EntityState.Deleted :
                     EntityState.Added;

                    //   Тре забабахати якусь проверуку чи така запісь є і це обновление, чи создание нової.. бо дєла не буде.//Answe
                    //процедура апдейта сама проверит
                }
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewBag.DocTypeID = new SelectList(db.DocTypes, "DocTypeID", "DocTypeName", order.DocTypeID);
            //ViewBag.MasterFID = new SelectList(db.Agents, "FId", "Name", order.MasterFID);
            return RedirectToAction(nameof(Index));
        }

        // GET: Orders/Delete/5
        public async Task<ActionResult> Delete(int? orid)
        {
            if (orid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await GetCurrentUserAsync();

            var order = user.Orders.FirstOrDefault(o => o.ID == orid);

            //TODO: Get by keys
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeActivity(int orid)
        {
            var user = await GetCurrentUserAsync();
            Order order = user.Orders.FirstOrDefault(o => o.ID == orid);
            order.Activity = !order.Activity;
            db.Entry(order).State = EntityState.Modified;
            await db.SaveChangesAsync();
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
