namespace NoRM.Tests
{
    using Xunit;

    public class ConnectionProviderFactoryTests
    {
        [Fact]
        public void ReturnsAPooledConnectionProvider()
        {
            Assert.IsType(typeof(PooledConnectionProvider), ConnectionProviderFactory.Create("mongodb://localhost/test?pooling=true"));
        }
        [Fact]
        public void ReturnsNormalConnectionProvider()
        {
            Assert.IsType(typeof(NormalConnectionProvider), ConnectionProviderFactory.Create("mongodb://localhost/test?pooling=false"));
        }
        
        [Fact]
        public void ReturnsTheSameProviderForTheSameConnectionString()
        {
            const string connectionString = "mongodb://localhost/test?pooling=false";
            var original = ConnectionProviderFactory.Create(connectionString);
            Assert.Same(original, ConnectionProviderFactory.Create(connectionString));
        }
        [Fact]
        public void ReturnsDifferentProvidersForDifferentConnectionStrings()
        {            
            var original = ConnectionProviderFactory.Create("mongodb://localhost/test?pooling=false");
            Assert.NotSame(original, ConnectionProviderFactory.Create("mongodb://localhost/test?pooling=false&strict=false"));
        }
    }
}