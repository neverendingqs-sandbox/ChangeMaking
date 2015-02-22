using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangeMaking;
using NUnit.Framework;

namespace ChangeMakingTests {

    [TestFixture]
    public class MinimumChangeTests {

        [Test]
        public void MinimumChange_CreateObject_ReturnsObject() {
            /* Arrange */
            ISet<int> denominations = new HashSet<int> { 1, 3, 5 };

            /* Act */
            MinimumChange mc = new MinimumChange( denominations );

            /* Assert */
            Assert.IsNotNull( mc );
            CollectionAssert.AreEqual(
                denominations,
                mc.Denominations
            );
        }

        [Test]
        public void MinimumChange_EmptyDenominations_ThrowsArgumentException() {
            /* Arrange */
            ISet<int> denominations = new HashSet<int> {
            };

            /* Act, Assert */
            Assert.Throws<ArgumentException>( () => new MinimumChange( denominations ) );
        }

        [Test]
        public void MinimumChange_NullDenominations_ThrowsArgumentNullException() {
            /* Arrange */
            ISet<int> denominations = null;

            /* Act, Assert */
            Assert.Throws<ArgumentNullException>( () => new MinimumChange( denominations ) );
        }

        [TestCase( new int[] { 2 } )]
        [TestCase( new int[] { 2, 5, 9 } )]
        [TestCase( new int[] { 5, 9, 100 } )]
        public void MinimumChange_NoOneDenomination_ThrowsArgumentException( int[] invalidDenominations ) {
            /* Arrange */
            ISet<int> denominations = new HashSet<int>( invalidDenominations );

            /* Act, Assert */
            Assert.Throws<ArgumentException>( () => new MinimumChange( denominations ) );
        }

        [Test]
        public void CoinsCount_ZeroDollars_ReturnsZero() {
            /* Arrange */
            ISet<int> denominations = new HashSet<int> { 1, 3, 5 };
            MinimumChange mc = new MinimumChange( denominations );
            int amount = 0;

            /* Act */
            int numCoins = mc.CoinsCount( amount );

            /* Assert */
            Assert.AreEqual(
                0,
                numCoins
            );
        }

        [TestCase( new int[] { 1, 3, 5 }, 1 )]
        [TestCase( new int[] { 1, 3, 5 }, 3 )]
        [TestCase( new int[] { 1, 3, 5 }, 5 )]
        public void CoinsCount_AmountEqualToDenomination_ReturnsOne( int[] denominationsArray, int amount ) {
            /* Arrange */
            ISet<int> denominations = new HashSet<int>( denominationsArray );
            MinimumChange mc = new MinimumChange( denominations );

            /* Act */
            int numCoins = mc.CoinsCount( amount );

            /* Assert */
            Assert.AreEqual(
                1,
                numCoins
            );
        }

        [TestCase( 4, 4 )]
        [TestCase( 9, 5 )]
        [TestCase( 42, 5 )]
        [TestCase( 50, 2 )]
        [TestCase( 68, 7 )]
        [TestCase( 230, 3 )]
        [TestCase( 250, 3 )]
        [TestCase( 330, 4 )]
        [TestCase( 500, 3 )]
        public void CoinsCount_CanadianDenominationAndValidAmount_ReturnsCount( int amount, int minCoins ) {
            /* Arrange */
            ISet<int> denominations = new HashSet<int> { 1, 5, 10, 25, 100, 200 };
            MinimumChange mc = new MinimumChange( denominations );

            /* Act */
            int numCoins = mc.CoinsCount( amount );

            /* Assert */
            Assert.AreEqual(
                minCoins,
                numCoins
            );
        }
    }
}
