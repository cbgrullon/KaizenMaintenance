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
    [Authorize(Roles ="Admin")]
    public class EquiposController : Controller
    {
        private KaizenDBEntities db = new KaizenDBEntities();

        // GET: Equipos
        public ActionResult Index()
        {
            var equipos = db.Equipos.Include(e => e.Modelo);
            return View(equipos.ToList());
        }

        // GET: Equipos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipos equipos = db.Equipos.Find(id);
            if (equipos == null)
            {
                return HttpNotFound();
            }
            return View(equipos);
        }

        // GET: Equipos/Create
        public ActionResult Create()
        {
            ViewBag.IdModelo = new SelectList(db.Modelos, "IdModelo", "Descripcion");
            return View();
        }

        // POST: Equipos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Equipos equipos)
        {
            equipos.Fecha_Modificacion = DateTime.Now;
            equipos.Fecha_Adicion = DateTime.Now;
            equipos.Modificado_Por = User.Identity.Name;
            equipos.Adicionado_Por = User.Identity.Name;
            if (ModelState.IsValid)
            {
                db.Equipos.Add(equipos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdModelo = new SelectList(db.Modelos, "IdModelo", "Descripcion", equipos.IdModelo);
            return View(equipos);
        }

        // GET: Equipos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipos equipos = db.Equipos.Find(id);
            if (equipos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdModelo = new SelectList(db.Modelos, "IdModelo", "Descripcion", equipos.IdModelo);
            return View(equipos);
        }

        // POST: Equipos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Equipos equipos)
        {
            equipos.Modificado_Por = User.Identity.Name;
            equipos.Fecha_Modificacion = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(equipos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdModelo = new SelectList(db.Modelos, "IdModelo", "Descripcion", equipos.IdModelo);
            return View(equipos);
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
