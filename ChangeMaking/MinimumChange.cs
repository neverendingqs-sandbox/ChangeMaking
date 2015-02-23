using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMaking {

    public class MinimumChange {

        private IList<int> _dp = new List<int>();

        public ISet<int> Denominations { get; set; }
        public int CacheHits { get; set; }
        public int CacheUpdates { get; set; }

        public MinimumChange( ISet<int> denominations ) {
            if( denominations == null ) {
                throw new ArgumentNullException();
            }

            if( ! denominations.Contains(1)) {
                throw new ArgumentException();
            }
            
            this.Denominations = denominations;
        }

        public IList<int> GetChange( int amount ) {
            UpdateDp( amount );

            int currAmount = amount;
            IList<int> change = new List<int>();

            while( currAmount > 0 ) {
                foreach( int coin in Denominations ) {
                    if( currAmount - coin < 0 ) {
                        continue;
                    }

                    if( _dp[currAmount - coin] == _dp[currAmount] - 1 ) {
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

        private void UpdateDp( int amount ) {
            if( _dp.Count > amount ) {
                CacheHits++;
                return;
            }

            CacheUpdates++;
            for( int subAmount = _dp.Count; subAmount <= amount; subAmount++ ) {
                int minCoins = int.MaxValue;
                foreach( int coin in Denominations ) {
                    if( coin > subAmount ) {
                        continue;
                    }

                    if( minCoins > _dp[subAmount - coin] + 1 ) {
                        minCoins = _dp[subAmount - coin] + 1;
                    }
                }

                _dp.Add(minCoins);
            }
        }
    }
}
