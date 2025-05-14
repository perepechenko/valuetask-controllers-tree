using NUnit.Framework;

namespace Playtika.Controllers.Substitute.Tests
{
    [TestFixture]
    public class SubstituteControllerFactoryTests
    {
        [Test]
        public void CreateControllerBaseByInterface_ControllerIsCorrectType()
        {
            var factory = new SubstituteControllerFactory();
            var controller = factory.Create<TypesForTest.ITestController>();
            Assert.That(controller, Is.Not.Null);
            Assert.That(controller, Is.TypeOf<SubstituteController>());
        }

        [Test]
        public void CreateControllerBaseByClass_ControllerIsCorrectType()
        {
            var factory = new SubstituteControllerFactory();
            var controller = factory.Create<TypesForTest.TestController>();
            Assert.That(controller, Is.Not.Null);
            Assert.That(controller, Is.TypeOf<SubstituteController>());
        }

        [Test]
        public void CreateControllerBaseWithArgsByInterface_ControllerIsCorrectType()
        {
            var factory = new SubstituteControllerFactory();
            var controller = factory.Create<TypesForTest.ITestControllerWithArgs>();
            Assert.That(controller, Is.Not.Null);
            Assert.That(controller, Is.TypeOf<SubstituteController<int>>());
        }

        [Test]
        public void CreateControllerBaseWithArgsByClass_ControllerIsCorrectType()
        {
            var factory = new SubstituteControllerFactory();
            var controller = factory.Create<TypesForTest.TestControllerWithArgs>();
            Assert.That(controller, Is.Not.Null);
            Assert.That(controller, Is.TypeOf<SubstituteController<int>>());
        }

        [Test]
        public void CreateControllerWithResultBaseByInterface_ControllerIsCorrectType()
        {
            var factory = new SubstituteControllerFactory();
            var controller = factory.Create<TypesForTest.ITestControllerWithResult>();
            Assert.That(controller, Is.Not.Null);
            Assert.That(controller, Is.TypeOf<SubstituteControllerWithResult<EmptyControllerArg, EmptyControllerResult>>());
        }

        [Test]
        public void CreateControllerWithResultBaseTByClass_ControllerIsCorrectType()
        {
            var factory = new SubstituteControllerFactory();
            var controller = factory.Create<TypesForTest.TestControllerWithResultT>();
            Assert.That(controller, Is.Not.Null);
            Assert.That(controller, Is.TypeOf<SubstituteControllerWithResult<EmptyControllerArg, bool>>());
        }

        [Test]
        public void CreateControllerWithResultBaseTByInterface_ControllerIsCorrectType()
        {
            var factory = new SubstituteControllerFactory();
            var controller = factory.Create<TypesForTest.ITestControllerWithResultT>();
            Assert.That(controller, Is.Not.Null);
            Assert.That(controller, Is.TypeOf<SubstituteControllerWithResult<EmptyControllerArg, bool>>());
        }

        [Test]
        public void CreateControllerWithResultBaseByClass_ControllerIsCorrectType()
        {
            var factory = new SubstituteControllerFactory();
            var controller = factory.Create<TypesForTest.TestControllerWithResult>();
            Assert.That(controller, Is.Not.Null);
            Assert.That(controller, Is.TypeOf<SubstituteControllerWithResult<EmptyControllerArg, EmptyControllerResult>>());
        }

        [Test]
        public void CreateControllerWithResultBaseWithArgsByInterface_ControllerIsCorrectType()
        {
            var factory = new SubstituteControllerFactory();
            var controller = factory.Create<TypesForTest.ITestControllerWithResultWithArgs>();
            Assert.That(controller, Is.Not.Null);
            Assert.That(controller, Is.TypeOf<SubstituteControllerWithResult<int, EmptyControllerResult>>());
        }

        [Test]
        public void CreateControllerWithResultTBaseWithArgsByInterface_ControllerIsCorrectType()
        {
            var factory = new SubstituteControllerFactory();
            var controller = factory.Create<TypesForTest.ITestControllerWithResultTWithArgs>();
            Assert.That(controller, Is.Not.Null);
            Assert.That(controller, Is.TypeOf<SubstituteControllerWithResult<int, bool>>());
        }

        [Test]
        public void CreateControllerWithResultTBaseWithArgsByClass_ControllerIsCorrectType()
        {
            var factory = new SubstituteControllerFactory();
            var controller = factory.Create<TypesForTest.TestControllerWithResultWithArg>();
            Assert.That(controller, Is.Not.Null);
            Assert.That(controller, Is.TypeOf<SubstituteControllerWithResult<int, bool>>());
        }

        [Test]
        public void CreateControllerWithResultBaseGenericByInterface_ControllerIsCorrectType()
        {
            var factory = new SubstituteControllerFactory();
            var controller = factory.Create<TypesForTest.ITestControllerWithResultGeneric<float>>();
            Assert.That(controller, Is.Not.Null);
            Assert.That(controller, Is.TypeOf<SubstituteControllerWithResult<EmptyControllerArg, float>>());
        }

        [Test]
        public void CreateControllerWithResultBaseGenericByClass_ControllerIsCorrectType()
        {
            var factory = new SubstituteControllerFactory();
            var controller = factory.Create<TypesForTest.TestControllerWithResultGeneric<float>>();
            Assert.That(controller, Is.Not.Null);
            Assert.That(controller, Is.TypeOf<SubstituteControllerWithResult<EmptyControllerArg, float>>());
        }

        [Test]
        public void CreateControllerWithResultBaseGenericWithArgsGenericByInterface_ControllerIsCorrectType()
        {
            var factory = new SubstituteControllerFactory();
            var controller = factory.Create<TypesForTest.ITestControllerWithResultGenericWithArgsGeneric<string, double>>();
            Assert.That(controller, Is.Not.Null);
            Assert.That(controller, Is.TypeOf<SubstituteControllerWithResult<string, double>>());
        }

        [Test]
        public void CreateControllerWithResultBaseGenericWithArgsGenericByClass_ControllerIsCorrectType()
        {
            var factory = new SubstituteControllerFactory();
            var controller = factory.Create<TypesForTest.TestControllerWithResultGenericWithArgsGeneric<string, double>>();
            Assert.That(controller, Is.Not.Null);
            Assert.That(controller, Is.TypeOf<SubstituteControllerWithResult<string, double>>());
        }

        [Test]
        public void Create2IdenticalControllers_ControllersAreNotEquals()
        {
            var factory = new SubstituteControllerFactory();
            var c1 = factory.Create<TypesForTest.ITestController>();
            var c2 = factory.Create<TypesForTest.ITestController>();

            Assert.That(c1, Is.Not.SameAs(c2));
        }
    }
}