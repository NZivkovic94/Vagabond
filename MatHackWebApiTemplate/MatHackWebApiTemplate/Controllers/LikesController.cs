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
    public class LikesController : ApiController
    {
        private MatHackWebApiTemplateContext db = new MatHackWebApiTemplateContext();

        // GET: api/Likes
        public IQueryable<Like> GetLikes()
        {
            return db.Likes;
        }

        // GET: api/Likes/5
        [ResponseType(typeof(Like))]
        public IHttpActionResult GetLike(int UserId)
        {
            var query = from p in db.Likes
                        where p.UserId == UserId
                        select p;
            if (query.LongCount() == 0)
            {
                return NotFound();
            }

            return Ok(query);
        }


        // POST: api/Likes
        [ResponseType(typeof(Like))]
        public IHttpActionResult PostLike(Like like)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Likes.Add(like);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LikeExists(like.UserId, like.MessageId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            var query = db.Messages.Find(like.MessageId);
            if (like.Liked)
                query.LikesNum++;
            else
                query.DislikeNum++;

            db.Entry(query).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(query.MessageId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = like.UserId }, like);
        }

        // DELETE: api/Likes/5
        [ResponseType(typeof(Like))]
        public IHttpActionResult DeleteLike(int UserId, int messageId)
        {
            var query = from p in db.Likes
                        where p.UserId == UserId && p.MessageId == messageId
                        select p;
            Like like = query.First();
            if (like == null)
            {
                return NotFound();
            }

            db.Likes.Remove(like);
            db.SaveChanges();

            var query1 = db.Messages.Find(like.MessageId);
            if (like.Liked)
                if(query1.LikesNum > 0)
                     query1.LikesNum--;
            else
                if (query1.DislikeNum > 0)
                    query1.DislikeNum--;


            db.Entry(query1).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(query1.MessageId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            return Ok(like);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LikeExists(int userId, int messageId)
        {
            return db.Likes.Count(e => e.UserId == userId && e.MessageId == messageId) > 0;
        }

        private bool MessageExists(int id)
        {
            return db.Messages.Count(e => e.MessageId == id) > 0;
        }

        private void Mock_Likes() {
            






        }

    }
}