using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestauranteWeb.Models;
using RestauranteWeb.Contexto;
using RestauranteWeb.ViewModels;
using System.Data.Entity.Infrastructure;

namespace RestauranteWeb.Controllers
{
    public class RestauranteController : Controller
    {
        private Contextos db = new Contextos();

        // GET: /Restaurante/
        public ActionResult Index()
        {
            return View(db.Restaurantes.ToList());
        }

        // GET: /Restaurante/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurante restaurante = db.Restaurantes.Find(id);
            if (restaurante == null)
            {
                return HttpNotFound();
            }
            return View(restaurante);
        }

        // GET: /Restaurante/Create
        public ActionResult Create()
        {
            var restaurante = new Restaurante();
            restaurante.Pratos = new List<Prato>();
            ArmazenaPratoMarcadoNoBanco(restaurante);
            return View();
        }

        // POST: /Restaurante/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NomeRestaurante")] Restaurante restaurante, string[] pratosSelecionados)
        {
             if (pratosSelecionados != null)
            {
                restaurante.Pratos = new List<Prato>();
                foreach (var prato in pratosSelecionados)
                {
                    var pratosAdd = db.Pratos.Find(int.Parse(prato));
                    restaurante.Pratos.Add(pratosAdd);
                }
            }
            if (ModelState.IsValid)
            {
                db.Restaurantes.Add(restaurante);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        
            ArmazenaPratoMarcadoNoBanco(restaurante);
            return View(restaurante);
        }

        // GET: /Restaurante/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Restaurante restaurante = db.Restaurantes.Find(id);
            Restaurante restaurante = db.Restaurantes
                .Include(r => r.Pratos)
                .Where(i => i.ID == id)
                .Single();
            if (restaurante == null)
            {
                return HttpNotFound();
            }
            ArmazenaPratoMarcadoNoBanco(restaurante);
            return View(restaurante);
        }

        private void ArmazenaPratoMarcadoNoBanco(Restaurante restaurante)
        {
            var todosPratos = db.Pratos;
            var restPratos = new HashSet<int>(restaurante.Pratos.Select(p => p.ID));
            var viewModel = new List<RestaurantePratoView>();
            foreach (var prato in todosPratos)
            {
                viewModel.Add(new RestaurantePratoView
                {
                    PratoID = prato.ID,
                    NomePrato = prato.NomePrato,
                    PrecoPrato = prato.PrecoPrato,
                    Marcado = restPratos.Contains(prato.ID)
                });
            }
            ViewBag.Pratos = viewModel;
        }

        // POST: /Restaurante/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] pratosSelecionados)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var restaurantToUpdate = db.Restaurantes
                .Include(r => r.Pratos)
                .Where(i => i.ID == id)
                .Single();
            if (TryUpdateModel(restaurantToUpdate, "",
                   new string[] { "ID", "NomeRestaurante"}))
            {
                try
                {
                    AtualizarRestPratos(pratosSelecionados, restaurantToUpdate);

                    db.Entry(restaurantToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Não foi possivel salvar as alterações. Tente Novamente.");
                }
            }
            ArmazenaPratoMarcadoNoBanco(restaurantToUpdate);
            return View(restaurantToUpdate);
        }
        private void AtualizarRestPratos(string[] pratosSelecionados, Restaurante restaurantToUpdate)
        {
            if (pratosSelecionados == null)
            {
                restaurantToUpdate.Pratos = new List<Prato>();
                return;
            }

            var selectedDishesHS = new HashSet<string>(pratosSelecionados);
            var restPratos = new HashSet<int>
                (restaurantToUpdate.Pratos.Select(r => r.ID));
            foreach (var prato in db.Pratos)
            {
                if (selectedDishesHS.Contains(prato.ID.ToString()))
                {
                    if (!restPratos.Contains(prato.ID))
                    {
                        restaurantToUpdate.Pratos.Add(prato);
                    }
                }
                else
                {
                    if (restPratos.Contains(prato.ID))
                    {
                        restaurantToUpdate.Pratos.Remove(prato);
                    }
                }
            }
        }

        // GET: /Restaurante/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurante restaurante = db.Restaurantes.Find(id);
            if (restaurante == null)
            {
                return HttpNotFound();
            }
            return View(restaurante);
        }

        // POST: /Restaurante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurante restaurante = db.Restaurantes.Find(id);
            db.Restaurantes.Remove(restaurante);
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
