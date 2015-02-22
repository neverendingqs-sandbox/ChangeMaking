using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMaking {

    public class MinimumChange {

        public ISet<int> Denominations { get; set; }

        public MinimumChange( ISet<int> denominations ) {
            if( denominations == null ) {
                throw new ArgumentNullException();
            }

            if( denominations.Count <= 0 || ! denominations.Contains(1)) {
                throw new ArgumentException();
            }
            
            this.Denominations = denominations;
        }

        public IList<int> GetChange( int amount ) {
            IList<int> change = new List<int>();
            int remainingAmount = amount;

            IList<int> sortedDenominations = Denominations.OrderByDescending( d => d ).ToList();

            foreach( int coin in sortedDenominations ) {
                while( remainingAmount >= coin ) {
                    remainingAmount -= coin;
                    change.Add(coin);
                }
            }

            return change;
        }

        public int CoinsCount( int amount ) {
            return GetChange( amount ).Count;
        }
    }
}
