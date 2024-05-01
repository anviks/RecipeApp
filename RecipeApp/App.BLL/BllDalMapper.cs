using AutoMapper;
using Base.Contracts.BLL;
using Base.Contracts.DAL;

namespace App.BLL;

public class BllDalMapper<TLeftObject, TRightObject>(IMapper mapper) 
    : IBllMapper<TLeftObject, TRightObject> 
    where TLeftObject : class 
    where TRightObject : class
{
    public TLeftObject? Map(TRightObject? inObject)
    {
        return mapper.Map<TLeftObject>(inObject);
    }

    public TRightObject? Map(TLeftObject? inObject)
    {
        return mapper.Map<TRightObject>(inObject);
    }
}