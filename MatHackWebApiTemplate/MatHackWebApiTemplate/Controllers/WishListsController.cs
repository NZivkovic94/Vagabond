using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MatHackWebApiTemplate.Models;

namespace MatHackWebApiTemplate.Controllers
{
    public class WishListsController : ApiController
    {
        private MatHackWebApiTemplateContext db = new MatHackWebApiTemplateContext();

        // GET: api/WishLists
        public IQueryable<WishList> GetWishLists()
        {
            return db.WishLists;
        }

        // GET: api/WishLists/5
        [ResponseType(typeof(WishList))]
        public IHttpActionResult GetWishList(int id)
        {
            var query = from p in db.WishLists
                        where p.UserId == id
                        select p;
            
            if (query.Count() == 0)
            {
                return NotFound();
            }

            return Ok(query);
        }

        // PUT: api/WishLists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWishList(int id, WishList wishList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != wishList.UserId)
            {
                return BadRequest();
            }

            db.Entry(wishList).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WishListExists(id))
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

        // POST: api/WishLists
        [ResponseType(typeof(WishList))]
        public IHttpActionResult PostWishList(WishList wishList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WishLists.Add(wishList);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = wishList.UserId }, wishList);
        }

        // DELETE: api/WishLists/5
        [ResponseType(typeof(WishList))]
        public IHttpActionResult DeleteWishList(int userId, int messageId)
        {
            var query = from p in db.WishLists
                        where p.UserId == userId && p.MessageId == messageId
                        select p;
            if (query.Count() == 0)
            {
                return NotFound();
            }

            db.WishLists.Remove(query.First());
            db.SaveChanges();

            return Ok(query.First());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WishListExists(int id)
        {
            return db.WishLists.Count(e => e.UserId == id) > 0;
        }
    }
}