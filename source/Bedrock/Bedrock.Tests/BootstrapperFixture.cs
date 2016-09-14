using System;
using System.Linq;
using Bedrock.Logging;
using Bedrock.Modularity;
using Bedrock.Regions;
using Bedrock.Regions.Behaviors;
using Bedrock.Views;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bedrock.Tests
{
    [TestClass]
    public class BootstrapperFixture
    {
        [TestMethod]
        public void LoggerDefaultsToNull()
        {
            var bootstrapper = new DefaultBootstrapper();

            Assert.IsNull(bootstrapper.BaseLogger);
        }

        [TestMethod]
        public void ModuleCatalogDefaultsToNull()
        {
            var bootstrapper = new DefaultBootstrapper();

            Assert.IsNull(bootstrapper.BaseModuleCatalog);
        }

        [TestMethod]
        public void ShellDefaultsToNull()
        {
            var bootstrapper = new DefaultBootstrapper();

            Assert.IsNull(bootstrapper.BaseShell);
        }

        [TestMethod]
        public void CreateLoggerInitializesLogger()
        {
            var bootstrapper = new DefaultBootstrapper();
            bootstrapper.CallCreateLogger();

            Assert.IsNotNull(bootstrapper.BaseLogger);

            Assert.IsInstanceOfType(bootstrapper.BaseLogger, typeof(TextLogger));
        }

        [TestMethod]
        public void CreateModuleCatalogShouldInitializeModuleCatalog()
        {
            var bootstrapper = new DefaultBootstrapper();

            bootstrapper.CallCreateModuleCatalog();

            Assert.IsNotNull(bootstrapper.BaseModuleCatalog);
        }

        [TestMethod]
        public void RegisterFrameworkExceptionTypesShouldRegisterActivationException()
        {
            var bootstrapper = new DefaultBootstrapper();

            bootstrapper.CallRegisterFrameworkExceptionTypes();

            Assert.IsTrue(ExceptionExtensions.IsFrameworkExceptionRegistered(
                typeof(Microsoft.Practices.ServiceLocation.ActivationException)));
        }

        [TestMethod]
        public void ConfigureDefaultRegionBehaviorsShouldAddSevenDefaultBehaviors()
        {
            var bootstrapper = new DefaultBootstrapper();

            CreateAndConfigureServiceLocatorWithDefaultRegionBehaviors();

            bootstrapper.CallConfigureDefaultRegionBehaviors();

            Assert.AreEqual(7, bootstrapper.DefaultRegionBehaviorTypes.Count());
        }

        private static void CreateAndConfigureServiceLocatorWithDefaultRegionBehaviors()
        {
            Mock<ServiceLocatorImplBase> serviceLocator = new Mock<ServiceLocatorImplBase>();
            var regionBehaviorFactory = new RegionBehaviorFactory(serviceLocator.Object);
            serviceLocator.Setup(sl => sl.GetInstance<IRegionBehaviorFactory>()).Returns(new RegionBehaviorFactory(serviceLocator.Object));

            ServiceLocator.SetLocatorProvider(() => serviceLocator.Object);
        }

        [TestMethod]
        public void ConfigureDefaultRegionBehaviorsShouldAddAutoPopulateRegionBehavior()
        {
            var bootstrapper = new DefaultBootstrapper();

            CreateAndConfigureServiceLocatorWithDefaultRegionBehaviors();

            bootstrapper.CallConfigureDefaultRegionBehaviors();

            Assert.IsTrue(bootstrapper.DefaultRegionBehaviorTypes.ContainsKey(AutoPopulateRegionBehavior.BehaviorKey));
        }

    }

    internal class DefaultBootstrapper : Bootstrapper
    {
        public IRegionBehaviorFactory DefaultRegionBehaviorTypes;

        public ILoggerFacade BaseLogger
        {
            get
            {
                return base.Logger;
            }
        }

        public IModuleCatalog BaseModuleCatalog
        {
            get { return base.ModuleCatalog; }
            set { base.ModuleCatalog = value; }
        }

        public IView BaseShell
        {
            get { return base.Startup; }
            set { base.Startup = value; }
        }

        public void CallCreateLogger()
        {
            this.Logger = base.CreateLogger();
        }

        public void CallCreateModuleCatalog()
        {
            this.ModuleCatalog = base.CreateModuleCatalog();
        }

        public override void Run(bool runWithDefaultConfiguration)
        {
            throw new NotImplementedException();
        }

        protected override IView CreateStartup()
        {
            throw new NotImplementedException();
        }

        protected override void ShowStartup()
        {
            throw new NotImplementedException();
        }

        protected override void ConfigureServiceLocator()
        {
            throw new NotImplementedException();
        }

        public void CallRegisterFrameworkExceptionTypes()
        {
            base.RegisterFrameworkExceptionTypes();
        }

        public IRegionBehaviorFactory CallConfigureDefaultRegionBehaviors()
        {
            this.DefaultRegionBehaviorTypes = base.ConfigureDefaultRegionBehaviors();
            return this.DefaultRegionBehaviorTypes;
        }
    }

}
