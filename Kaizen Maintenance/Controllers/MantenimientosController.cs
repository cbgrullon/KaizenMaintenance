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
    public class MantenimientosController : Controller
    {
        private KaizenDBEntities db = new KaizenDBEntities();

        // GET: Mantenimientos
        public async Task<ActionResult> Index()
        {
            var mantenimientos = db.Mantenimientos.Include(m => m.Equipos).Include(m => m.TIempos);
            return View(await mantenimientos.ToListAsync());
        }

        // GET: Mantenimientos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mantenimiento mantenimiento = await db.Mantenimientos.FindAsync(id);
            if (mantenimiento == null)
            {
                return HttpNotFound();
            }
            return View(mantenimiento);
        }

        // GET: Mantenimientos/Create
        public ActionResult Create()
        {
            ViewBag.IdEquipo = new SelectList(db.Equipos.Where(x => x.Estado == "A"), "IdEquipo", "Descripcion");
            ViewBag.IdTiempo = new SelectList(db.TIempos, "IdTiempo", "Descripcion");
            return View();
        }

        // POST: Mantenimientos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdMantenimiento,IdEquipo,Descripcion,QueHacer,IdTiempo,TiempoPorMantenimiento,Estado")] Mantenimiento mantenimiento)
        {
            mantenimiento.Fecha_Adicion = mantenimiento.Fecha_Modificacion = DateTime.Now;
            mantenimiento.Adicionado_Por = mantenimiento.Modificado_Por = User.Identity.Name;
            if (ModelState.IsValid)
            {
                db.Mantenimientos.Add(mantenimiento);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdEquipo = new SelectList(db.Equipos.Where(x => x.Estado == "A"), "IdEquipo", "Descripcion", mantenimiento.IdEquipo);
            ViewBag.IdTiempo = new SelectList(db.TIempos, "IdTiempo", "Descripcion", mantenimiento.IdTiempo);
            return View(mantenimiento);
        }

        // GET: Mantenimientos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mantenimiento mantenimiento = await db.Mantenimientos.FindAsync(id);
            if (mantenimiento == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEquipo = new SelectList(db.Equipos.Where(x => x.Estado == "A"), "IdEquipo", "Descripcion", mantenimiento.IdEquipo);
            ViewBag.IdTiempo = new SelectList(db.TIempos, "IdTiempo", "Descripcion", mantenimiento.IdTiempo);
            return View(mantenimiento);
        }

        // POST: Mantenimientos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdMantenimiento,IdEquipo,Descripcion,QueHacer,IdTiempo,TiempoPorMantenimiento,Estado,Adicionado_Por,Fecha_Adicion")] Mantenimiento mantenimiento)
        {
            mantenimiento.Modificado_Por = User.Identity.Name;
            mantenimiento.Fecha_Modificacion = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(mantenimiento).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdEquipo = new SelectList(db.Equipos.Where(x=>x.Estado=="A"), "IdEquipo", "Descripcion", mantenimiento.IdEquipo);
            ViewBag.IdTiempo = new SelectList(db.TIempos, "IdTiempo", "Descripcion", mantenimiento.IdTiempo);
            return View(mantenimiento);
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
