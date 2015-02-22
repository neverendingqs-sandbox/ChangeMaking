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
            ISet<int> denominations = new HashSet<int> {};

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
    }
}
