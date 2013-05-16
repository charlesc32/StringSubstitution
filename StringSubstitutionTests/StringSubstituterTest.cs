using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StringSubstitutionTests
{
    
    
    /// <summary>
    ///This is a test class for StringSubstituterTest and is intended
    ///to contain all StringSubstituterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StringSubstituterTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for CreateSubstitutedString
        ///</summary>
        [TestMethod()]
        public void CreateSubstitutedStringTest()
        {
            string originalString = "10011011001;0110,1001,1001,0,10,11";
            string expected = "11100110";
            string actual;
            actual = StringSubstituter.CreateSubstitutedString(originalString);
            Assert.AreEqual(expected, actual);
        }
    }
}
