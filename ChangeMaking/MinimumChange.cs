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
    }
}
