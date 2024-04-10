namespace Base.Contracts.DAL;

public interface IDalMapper<TLeftObject, TRightObject>
    where TLeftObject : class
    where TRightObject : class

{
    TLeftObject? MapRL(TRightObject? inObject);
    TRightObject? MapLR(TLeftObject? inObject);
}