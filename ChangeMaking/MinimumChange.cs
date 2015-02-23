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
            int[] dp = new int[amount + 1];

            for( int subAmount = 1; subAmount <= amount; subAmount++ ) {
                int minCoins = int.MaxValue;
                foreach( int coin in Denominations ) {
                    if( coin > subAmount ) {
                        continue;
                    }

                    if( minCoins > dp[subAmount - coin] + 1 ) {
                        minCoins = dp[subAmount - coin] + 1;
                    }
                }

                dp[subAmount] = minCoins;
            }

            int currAmount = amount;
            IList<int> change = new List<int>();
            while( currAmount > 0 ) {
                foreach( int coin in Denominations ) {
                    if( currAmount - coin < 0 ) {
                        continue;
                    }

                    if( dp[currAmount - coin] == dp[currAmount] - 1 ) {
                        change.Add( coin );
                        currAmount -= coin;
                        continue;
                    }
                }
            }

            return change;
        }

        public int CoinsCount( int amount ) {
            return GetChange( amount ).Count;
        }
    }
}
