using GoblinTribe;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoblinTribeTests
{
    [TestClass]
    public class GoblinTests
    {
        [TestInitialize]
        public void Init()
        {
            Goblin.Reset();
        }

        [TestMethod]
        public void TestAttack()
        {
            var attacker = new Goblin();
            var defender = new Goblin();
            int initialHp = defender.GetHp();

            attacker.Attack(defender);

            Assert.AreNotEqual(initialHp, defender.GetHp());
        }

        [TestMethod]
        public void TestNumber()
        {
            var alice = new Goblin();
            var bob = new Goblin();
            var charlie = new Goblin();

            Assert.AreEqual(1, alice.Number);
            Assert.AreEqual(2, bob.Number);
            Assert.AreEqual(3, charlie.Number);
        }
    }
}
