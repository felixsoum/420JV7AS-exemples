using GoblinTribe;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoblinTribeTests
{
    [TestClass]
    public class DragonTests
    {
        [TestInitialize]
        public void Init()
        {
            Goblin.Reset();
        }

        [TestMethod]
        public void TestHp()
        {
            Assert.IsTrue(new Dragon().GetHp() > new Goblin().GetHp());
        }

        [TestMethod]
        public void TestFireTribe()
        {
            Assert.AreNotEqual(Goblin.Tribe, "Fire");

            new Dragon();

            Assert.AreEqual(Goblin.Tribe, "Fire");
        }
    }
}
