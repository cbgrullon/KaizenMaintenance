using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Index()
        {
            var equipos = db.Equipos.Include(e => e.Modelo);
            return View(await equipos.ToListAsync());
        }

        // GET: Equipos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipos equipos = await db.Equipos.FindAsync(id);
            if (equipos == null)
            {
                return HttpNotFound();
            }
            return View(equipos);
        }

        // GET: Equipos/Create
        public ActionResult Create()
        {
            ViewBag.IdModelo = new SelectList(db.Modelos.Where(x => x.Estado == "A"), "IdModelo", "Descripcion");
            return View();
        }

        // POST: Equipos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdEquipo,IdModelo,Estado,Serial,No_Activo,Descripcion")] Equipos equipos)
        {
            equipos.Adicionado_Por = equipos.Modificado_Por = User.Identity.Name;
            equipos.Fecha_Adicion = equipos.Fecha_Modificacion = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Equipos.Add(equipos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdModelo = new SelectList(db.Modelos.Where(x => x.Estado == "A"), "IdModelo", "Descripcion", equipos.IdModelo);
            return View(equipos);
        }

        // GET: Equipos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipos equipos = await db.Equipos.FindAsync(id);
            if (equipos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdModelo = new SelectList(db.Modelos.Where(x => x.Estado == "A"), "IdModelo", "Descripcion", equipos.IdModelo);
            return View(equipos);
        }

        // POST: Equipos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdEquipo,IdModelo,Adicionado_Por,Fecha_Adicion,Estado,Serial,No_Activo,Descripcion")] Equipos equipos)
        {
            equipos.Modificado_Por = User.Identity.Name;
            equipos.Fecha_Modificacion = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(equipos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdModelo = new SelectList(db.Modelos.Where(x=>x.Estado=="A"), "IdModelo", "Descripcion", equipos.IdModelo);
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
