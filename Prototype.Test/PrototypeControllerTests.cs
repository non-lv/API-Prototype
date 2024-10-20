using API_Prototype.Controllers;
using API_Prototype.Database;
using API_Prototype.Database.Models;
using AutoFixture;
using Microsoft.EntityFrameworkCore;

namespace Prototype.Test
{
    public class PrototypeControllerTests : IDisposable
    {
        private readonly Context context;
        private readonly PrototypeController controller;

        public PrototypeControllerTests()
        {
            var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: "test")
            .Options;

            context = new Context(options);
            controller = new PrototypeController(context);
        }

        [Fact]
        public void GetAll_ReturnsAllData()
        {
            PopulateDatabase();


            var result = controller.GetAll();


            Assert.Equal(context.Threads.Count(), result.Count());
            Assert.Equal(context.Messages.Count(), result.SelectMany(x => x.Entries).Distinct().Count());
        }

        [Fact]
        public void GetThread_ReturnsSpecificThread()
        {
            PopulateDatabase();

            // Picking a random Row
            Random rand = new Random();
            int toSkip = rand.Next(0, context.Threads.Count());

            var expected = context.Threads.Skip(toSkip).Take(1).First();


            var result = controller.GetThread(expected.Id);


            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetThread_ReturnsNothing()
        {
            var result = controller.GetThread((context.Threads.LastOrDefault()?.Id ?? 0) + 1);
            Assert.Null(result);

        }

        [Fact]
        public void CreateThread_CreatesThread()
        {
            var fixture = new Fixture();
            var descr = fixture.Create<string>();

            var id = controller.CreateThread(descr);


            var thread = context.Threads.Where(x => x.Description == descr).First();


            Assert.NotNull(thread);
        }

        [Fact]
        public void EditThread_EditsThread()
        {
            PopulateDatabase();
            Random rand = new Random();
            int toSkip = rand.Next(0, context.Threads.Count());
            var expected = context.Threads.Skip(toSkip).Take(1).First();

            var fixture = new Fixture();
            var descr = fixture.Create<string>();


            controller.EditThreadDescription(expected.Id, descr);


            Assert.Equal(expected.Description, descr);
        }

        [Fact]
        public void DeleteThread_DeletesThread()
        {
            PopulateDatabase();
            Random rand = new Random();
            int toSkip = rand.Next(0, context.Threads.Count());
            var toDelete = context.Threads.Skip(toSkip).Take(1).First();
            var toDeleteId = toDelete.Id;


            controller.DeleteThread(toDeleteId);


            Assert.Null(context.Threads.FirstOrDefault(x => x.Id == toDeleteId));
        }


        [Fact]
        public void PostMessage_PostsMessage()
        {
            PopulateDatabase();
            Random rand = new Random();
            int toSkip = rand.Next(0, context.Threads.Count());
            var thread = context.Threads.Skip(toSkip).Take(1).First();
            var fixture = new Fixture();
            var msg = fixture.Create<string>();


            controller.PostMessage(thread.Id, msg);


            Assert.Equal(msg, context.Messages.SingleOrDefault(x => msg == x.Message)?.Message);
        }

        [Fact]
        public void UpdateMessage_UpdatesMessage()
        {
            PopulateDatabase();
            Random rand = new Random();
            int toSkip = rand.Next(0, context.Messages.Count());
            var message = context.Messages.Skip(toSkip).Take(1).First();
            var fixture = new Fixture();
            var msg = fixture.Create<string>();


            controller.UpdateMessage(message.Id, msg);


            Assert.Equal(message.Message, msg);
        }

        private void PopulateDatabase()
        {
            var fixture = new Fixture();

            var threads = fixture.Build<MessageThread>()
                .Without(x => x.Id)
                .With(x => x.Entries, fixture.Build<Entry>().With(x => x.Type, EntryType.Regular).Without(x => x.Id).CreateMany().ToList())
                .CreateMany();
            var messages = threads.SelectMany(x => x.Entries).ToList();

            context.Threads.AddRange(threads);
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}