using AutoMapper;

namespace Helpers;

public class EntityMapper<TLeft, TRight>(IMapper mapper)
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