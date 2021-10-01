using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Ev_M_P01.Models;
using static Ev_M_P01.Models.DbModel;

namespace Ev_M_P01.Controllers
{
    [EnableCors("*","*","*")]
    public class TraineesController : ApiController
    {
        private TraineeDbContext db = new TraineeDbContext();

        // GET: api/Trainees
        public IQueryable<Trainee> GetTrainees()
        {
            return db.Trainees;
        }

        // GET: api/Trainees/5
        [ResponseType(typeof(Trainee))]
        public IHttpActionResult GetTrainee(int id)
        {
            Trainee trainee = db.Trainees.Find(id);
            if (trainee == null)
            {
                return NotFound();
            }

            return Ok(trainee);
        }

        // PUT: api/Trainees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTrainee(int id, Trainee trainee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trainee.TraineeId)
            {
                return BadRequest();
            }

            db.Entry(trainee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TraineeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        [HttpPut,Route("api/Edit/{id}")]
        public IHttpActionResult Edit(int id, TraineeViewModel t)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t.TraineeId)
            {
                return BadRequest();
            }

            var tr = db.Trainees.First(x => x.TraineeId == id);
            tr.TraineeName = t.TraineeName;
            tr.Email = t.Email;
            tr.AdmitDate = t.AdmitDate;
            if (!string.IsNullOrEmpty(t.Picture))
            {
                string fileName = "";
                if (t.ImageType == "image/jpeg") fileName = Guid.NewGuid() + ".jpg";
                if (t.ImageType == "image/jpg") fileName = Guid.NewGuid() + ".jpg";
                if (t.ImageType == "image/png") fileName = Guid.NewGuid() + ".png";
                if (t.ImageType == "image/gif") fileName = Guid.NewGuid() + ".gif";

                byte[] bytes = Convert.FromBase64String(t.Picture);

                File.WriteAllBytes(Path.Combine(HttpContext.Current.Server.MapPath("~/Images/"), fileName), bytes);
                tr.Picture = fileName;
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TraineeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(tr);
        }
        // POST: api/Trainees
        [ResponseType(typeof(Trainee))]
        public IHttpActionResult PostTrainee(Trainee trainee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Trainees.Add(trainee);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = trainee.TraineeId }, trainee);
        }
        ///////////////////////////////////////////////////
        /// Cutom action
        ////////////////////////////////////////////////////
        [HttpPost, Route("api/Create")]
        public IHttpActionResult Create(TraineeViewModel t)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Trainee tr = new Trainee { TraineeName = t.TraineeName, AdmitDate = t.AdmitDate, Email=t.Email, Picture = "no-pic.png" };
            string fileName = "";
            if (t.ImageType == "image/jpeg") fileName = Guid.NewGuid() + ".jpg";
            if (t.ImageType == "image/jpg") fileName = Guid.NewGuid() + ".jpg";
            if (t.ImageType == "image/png") fileName = Guid.NewGuid() + ".png";
            if (t.ImageType == "image/gif") fileName = Guid.NewGuid() + ".gif";
            byte[] bytes = Convert.FromBase64String(t.Picture);
            File.WriteAllBytes(Path.Combine(HttpContext.Current.Server.MapPath("~/Images/"), fileName), bytes);
            tr.Picture = fileName;
            db.Trainees.Add(tr);
            db.SaveChanges();

            return Ok(tr);
        }
        // DELETE: api/Trainees/5
        [ResponseType(typeof(Trainee))]
        public IHttpActionResult DeleteTrainee(int id)
        {
            Trainee trainee = db.Trainees.Find(id);
            if (trainee == null)
            {
                return NotFound();
            }

            db.Trainees.Remove(trainee);
            db.SaveChanges();

            return Ok(trainee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TraineeExists(int id)
        {
            return db.Trainees.Count(e => e.TraineeId == id) > 0;
        }
    }
}