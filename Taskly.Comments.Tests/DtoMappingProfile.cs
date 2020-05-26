using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Taskly.Comments.Tests
{
    [TestClass]
    public class DtoMappingProfile
    {
        [TestMethod]
        public void ConfigurationIsValid()
        {
            var configuration = new MapperConfiguration(cfg => { cfg.AddMaps(typeof(DtoMappingProfile)); });
            configuration.AssertConfigurationIsValid();
        }
    }
}
