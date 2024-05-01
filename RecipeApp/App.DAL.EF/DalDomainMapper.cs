using App.Domain;
using AutoMapper;
using Base.Contracts.DAL;

namespace App.DAL.EF;

public class DalDomainMapper<TLeft, TRight>(IMapper mapper) : IDalMapper<TLeft, TRight>
    where TLeft : class
    where TRight : class
{
    public TLeft? Map(TRight? inObject)
    {
        return mapper.Map<TLeft>(inObject);
    }

    public TRight? Map(TLeft? inObject)
    {
        return mapper.Map<TRight>(inObject);
    }
}