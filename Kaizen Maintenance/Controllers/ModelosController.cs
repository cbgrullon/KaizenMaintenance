using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kaizen_Maintenance.Models;

namespace Kaizen_Maintenance.Controllers
{
    [Authorize(Roles="Admin")]
    public class ModelosController : Controller
    {
        private KaizenDBEntities db = new KaizenDBEntities();

        // GET: Modeloes
        public ActionResult Index()
        {
            var modelos = db.Modelos.Include(m => m.Marca);
            return View(modelos.ToList());
        }

        // GET: Modeloes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modelo modelo = db.Modelos.Find(id);
            if (modelo == null)
            {
                return HttpNotFound();
            }
            return View(modelo);
        }

        // GET: Modeloes/Create
        public ActionResult Create()
        {
            ViewBag.IdMarca = new SelectList(db.Marcas, "IdMarca", "Descripcion");
            return View();
        }

        // POST: Modeloes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Modelo modelo)
        {
            modelo.Fecha_Adicion = DateTime.Now;
            modelo.Adicionado_Por = User.Identity.Name;
            modelo.Fecha_Modificacion = DateTime.Now;
            modelo.Modificado_Por = User.Identity.Name;
            if (ModelState.IsValid)
            {
                db.Modelos.Add(modelo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdMarca = new SelectList(db.Marcas, "IdMarca", "Descripcion", modelo.IdMarca);
            return View(modelo);
        }

        // GET: Modeloes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modelo modelo = db.Modelos.Find(id);
            if (modelo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMarca = new SelectList(db.Marcas, "IdMarca", "Descripcion", modelo.IdMarca);
            return View(modelo);
        }

        // POST: Modeloes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Modelo modelo)
        {
            modelo.Modificado_Por = User.Identity.Name;
            modelo.Fecha_Modificacion = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(modelo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMarca = new SelectList(db.Marcas, "IdMarca", "Descripcion", modelo.IdMarca);
            return View(modelo);
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
