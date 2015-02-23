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

        [Test]
        public void MinimumChange_NoOneDenomination_ThrowsArgumentException() {
            /* Arrange */
            ISet<int> denominations = new HashSet<int> { 2 };

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

        [TestCase( 1, new int[] { 1, 3, 5 } )]
        [TestCase( 3, new int[] { 1, 3, 5 } )]
        [TestCase( 5, new int[] { 1, 3, 5 } )]
        public void CoinsCount_AmountEqualToDenomination_ReturnsOne( int amount, int[] denominationsArray ) {
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

        [TestCase( 1, new int[] { 1, 3, 5 }, new int[] { 1 } )]
        [TestCase( 3, new int[] { 1, 3, 5 }, new int[] { 3 } )]
        [TestCase( 5, new int[] { 1, 3, 5 }, new int[] { 5 } )]
        public void GetChange_AmountEqualToDenomination_ReturnsDenomination( int amount, int[] denominationsArray, int[] change ) {
            /* Arrange */
            ISet<int> denominations = new HashSet<int>( denominationsArray );
            MinimumChange mc = new MinimumChange( denominations );

            IList<int> expectedChange = new List<int>( change );

            /* Act */
            IList<int> actualChange = mc.GetChange( amount );

            /* Assert */
            CollectionAssert.AreEquivalent(
                expectedChange,
                actualChange
            );
        }

        [TestCase( 4, new int[] { 1, 1, 1, 1 } )]
        [TestCase( 9, new int[] { 5, 1, 1, 1, 1 } )]
        [TestCase( 42, new int[] { 25, 10, 5, 1, 1 } )]
        [TestCase( 50, new int[] { 25, 25 } )]
        [TestCase( 68, new int[] { 25, 25, 10, 5, 1, 1, 1 } )]
        [TestCase( 230, new int[] { 200, 25, 5 } )]
        [TestCase( 250, new int[] { 200, 25, 25 } )]
        [TestCase( 330, new int[] { 200, 100, 25, 5 } )]
        [TestCase( 500, new int[] { 200, 200, 100 } )]
        public void GetChange_CanadianDenominationAndValidAmount_ReturnsCount( int amount, int[] change ) {
            /* Arrange */
            ISet<int> denominations = new HashSet<int> { 1, 5, 10, 25, 100, 200 };
            MinimumChange mc = new MinimumChange( denominations );

            IList<int> expectedChange = new List<int>( change );

            /* Act */
            IList<int> actualChange = mc.GetChange( amount );

            /* Assert */
            CollectionAssert.AreEquivalent(
                expectedChange,
                actualChange
            );
        }

        [TestCase( 6, new int[] { 1, 3, 4 }, new int[] { 3, 3 } )]
        [TestCase( 7, new int[] { 1, 3, 4, 5 }, new int[] { 4, 3 } )]
        [TestCase( 194, new int[] { 1, 3, 5, 10, 12, 50, 100 }, new int[] { 100, 50, 12, 12, 10, 10 } )]
        public void GetChange_IrregularDenominationAndValidAmount_ReturnsCount( int amount, int[] customDenominations, int[] change ) {
            /* Arrange */
            ISet<int> denominations = new HashSet<int>( customDenominations );
            MinimumChange mc = new MinimumChange( denominations );

            IList<int> expectedChange = new List<int>( change );

            /* Act */
            IList<int> actualChange = mc.GetChange( amount );

            /* Assert */
            CollectionAssert.AreEquivalent(
                expectedChange,
                actualChange
            );
        }

        [TestCase( 1, new int[] { 1, 3, 4 }, new int[] { 1 } )]
        [TestCase( 3, new int[] { 1, 3, 4 }, new int[] { 3 } )]
        [TestCase( 4, new int[] { 1, 3, 4 }, new int[] { 4 } )]
        public void GetChange_IrregularDenominationAndAmountEqualToDenomination_ReturnsDenomination( int amount, int[] denominationsArray, int[] change ) {
            /* Arrange */
            ISet<int> denominations = new HashSet<int>( denominationsArray );
            MinimumChange mc = new MinimumChange( denominations );

            IList<int> expectedChange = new List<int>( change );

            /* Act */
            IList<int> actualChange = mc.GetChange( amount );

            /* Assert */
            CollectionAssert.AreEquivalent(
                expectedChange,
                actualChange
            );
        }

        [Test]
        public void CacheReuses_AmountDecreases_AlwaysUsesCache() {
            /* Arrange */
            ISet<int> denominations = new HashSet<int> { 1, 5, 10, 25, 100, 200 };
            int numCacheHits = 9;
            MinimumChange mc = new MinimumChange( denominations );

            /* Act */
            mc.GetChange( numCacheHits );     // First request is always a cache miss
            for( int amount = numCacheHits; amount > 0; amount-- ) {
                mc.GetChange( amount );
            }
            
            /* Assert */
            Assert.AreEqual(
                numCacheHits,
                mc.CacheHits
            );
        }

        [Test]
        public void CacheAppends_AmountIncreases_AlwaysAppendsToCache() {
            /* Arrange */
            ISet<int> denominations = new HashSet<int> { 1, 5, 10, 25, 100, 200 };
            int numCacheAppends = 13;
            MinimumChange mc = new MinimumChange( denominations );

            /* Act */
            for( int amount = 0; amount < numCacheAppends; amount++ ) {
                mc.GetChange( amount );
            }

            /* Assert */
            Assert.AreEqual(
                numCacheAppends,
                mc.CacheUpdates
            );
        }
    }
}
