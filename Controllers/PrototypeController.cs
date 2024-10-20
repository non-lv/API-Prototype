using API_Prototype.Database;
using API_Prototype.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Prototype.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PrototypeController(Context context) : ControllerBase
    {
        [HttpGet]
        public IEnumerable<MessageThread> GetAll()
        {
            return context.Threads.Include(x => x.Entries).ToList();
        }

        [HttpGet]
        public MessageThread? GetThread(int id)
        {
            return context.Threads.Where(x => x.Id == id).SingleOrDefault();
        }

        [HttpPut]
        public int CreateThread(string description)
        {
            var thread = context.Threads.Add(new MessageThread { Description = description, Entries = [] });
            context.SaveChanges();

            return thread.Entity.Id;
        }

        [HttpPatch]
        public ActionResult EditThreadDescription(int id, string description)
        {
            var thread = context.Threads.Where(x => x.Id == id).SingleOrDefault();
            if (thread is null)
                return new NotFoundResult();

            thread.Description = description;
            context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public ActionResult DeleteThread(int id)
        {
            var thread = context.Threads.Where(x => x.Id == id).Include(x => x.Entries).SingleOrDefault();
            if (thread is null)
                return new NotFoundResult();

            context.Threads.Remove(thread);
            context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public ActionResult PostMessage(int threadId, string message)
        {
            var thread = context.Threads.Include(x => x.Entries).SingleOrDefault(x => x.Id == threadId);
            if (thread is null)
                return new NotFoundResult();

            var msg = new Entry { Message = message, Edited = false, TimeStamp = DateTime.UtcNow };
            thread.Entries.Add(msg);
            context.SaveChanges();
            
            return Ok(msg.Id);
        }

        [HttpPatch]
        public ActionResult UpdateMessage(int messageId, string message)
        {
            var msg = context.Messages.SingleOrDefault(x => x.Id == messageId);
            if (msg is null)
                return new NotFoundResult();

            msg.Message = message;
            msg.Edited = true;
            context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public ActionResult DeleteMessage(int messageId)
        {
            var msg = context.Messages.SingleOrDefault(x => x.Id == messageId);
            if (msg is null)
                return new NotFoundResult();

            context.Messages.Remove(msg);
            context.SaveChanges();

            return Ok();
        }
    }
}
