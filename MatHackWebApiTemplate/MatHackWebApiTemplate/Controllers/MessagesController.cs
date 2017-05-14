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
using System.Data.Entity.SqlServer;


namespace MatHackWebApiTemplate.Controllers
{
    public class MessagesController : ApiController
    {
        private MatHackWebApiTemplateContext db = new MatHackWebApiTemplateContext();

        public IQueryable<Message> GetMessages(string hof) {
            
            var query = from p in db.Messages
                        where (p.LikesNum - p.DislikeNum) > 20
                        select p;
            return query;
        }

        // GET: api/Messages/lan,lgt
        public IEnumerable<Message> GetMessages(double lat, double lng)
        {
            double distance = 0.05;
            var query = from it in db.Messages
                        let facilityLatitude = it.Lat
                        let facilityLongitude = it.Lng
                        let theta = ((lng - facilityLongitude) * Math.PI / 180.0)
                        let requestLat = (lat * Math.PI / 180.0)
                        let facilityLat = (facilityLatitude * Math.PI / 180.0)
                        let dist = (SqlFunctions.Sin(requestLat) * SqlFunctions.Sin(facilityLat)) + (SqlFunctions.Cos(requestLat) * SqlFunctions.Cos(facilityLat) * SqlFunctions.Cos(theta))
                        let cosDist = SqlFunctions.Acos(dist)
                        let degDist = (cosDist / Math.PI * 180.0)
                        let absoluteDist = degDist * 60 * 1.1515
                        let distInKM = absoluteDist * 1.609344
                        where distInKM < distance
                        select it;

            return query.OrderBy(t => t.LikesNum - t.DislikeNum);
        }

        // GET: api/Messages
        public IQueryable<Message> GetMessages()
        {
            return db.Messages;
        }

        // GET: api/Messages/5
        [ResponseType(typeof(Message))]
        public IHttpActionResult GetMessage(int id)
        {
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        // PUT: api/Messages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMessage()
        {
            MockData();
            return Ok();
           
        }

        // POST: api/Messages
        [ResponseType(typeof(Message))]
        public IHttpActionResult PostMessage(Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Messages.Add(message);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = message.MessageId }, message);
        }

        // DELETE: api/Messages/5
        [ResponseType(typeof(Message))]
        public IHttpActionResult DeleteMessage(int id)
        {
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            db.Messages.Remove(message);
            db.SaveChanges();

            return Ok(message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessageExists(int id)
        {
            return db.Messages.Count(e => e.MessageId == id) > 0;
        }


        private void MockData()
        {
            String[] category = {null,"Community","Entertainment Website","Personal Blog","Musician/Band","Theatrical Play",
                                "Local Business","Local/Travel Website","Fictional Character","Entertainment Website",
                                "Religious Organization","Artist","Magazine","Art","Amateur Sports Team","Musician",
                                "Movie Character","Personal Blog","Library"};
            String[] words = {"Ovo", "Su", "Neke", "Reci", "Koje", "Kombinujemo", "U", "Po", "Nekim", "Pravilima",
                              "Kombinacija", "Je", "Jeste","Mora","Da","Bude"};
            Random rand = new Random();
            for (int i = 0; i < 100; i++)
            {
                Message msg = new Message();
                int c = rand.Next(2, 6);
                String msgText = "";
                for (int j = 0; j < c; j++)
                     msgText += words[rand.Next(0, words.Length)] + " ";
                msg.MessageText = msgText;
                msg.Lat = rand.NextDouble() * 2 + 43;
                msg.Lng = rand.NextDouble() * 2 + 19;

                msg.MessageId = 0;
                msg.messageCategory = category[rand.Next(0,category.Length)];
                msg.LikesNum = rand.Next(0,50);
                msg.DislikeNum = rand.Next(0, 50);
                msg.CreateTime = new DateTime(2017, 5, 17, rand.Next(0, 24), rand.Next(0, 60), rand.Next(0, 59));

                db.Messages.Add(msg);
                db.SaveChanges();
            }
        }
    }
}