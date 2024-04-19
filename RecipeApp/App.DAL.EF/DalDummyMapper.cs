using App.Domain;
using Base.Contracts.DAL;

namespace App.DAL.EF;

public class DalDummyMapper<TLeft, TRight> : IDalMapper<TLeft, TRight> 
    where TLeft : class 
    where TRight : class
{
    public TLeft? Map(TRight? inObject)
    {
        return inObject as TLeft;
    }

    public TRight? MapLR(TLeft? inObject)
    {
        return inObject as TRight;
    }
}