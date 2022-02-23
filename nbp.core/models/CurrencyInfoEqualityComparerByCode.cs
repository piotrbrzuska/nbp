using System.Collections.Generic;

namespace nbp.core.dto
{
    public class CurrencyInfoEqualityComparerByCode : IEqualityComparer<CurrencyInfo>
    {
        public bool Equals(CurrencyInfo x, CurrencyInfo y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Code == y.Code;
        }

        public int GetHashCode(CurrencyInfo obj)
        {
            return (obj.Code != null ? obj.Code.GetHashCode() : 0);
        }
    }
}