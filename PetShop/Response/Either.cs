namespace PetShop.Response;

public class Either<TLeft, TRight>
{
    private readonly TLeft _left;
    private readonly TRight _right;
    private readonly bool _isLeft;

    private Either(TLeft left)
    {
        _left = left;
        _isLeft = true;
    }

    private Either(TRight right)
    {
        _right = right;
        _isLeft = false;
    }

    public static implicit operator Either<TLeft, TRight>(TLeft left) => new Either<TLeft, TRight>(left);
    public static implicit operator Either<TLeft, TRight>(TRight right) => new Either<TLeft, TRight>(right);

    public TResult Match<TResult>(Func<TLeft, TResult> leftFunc, Func<TRight, TResult> rightFunc)
    {
        return _isLeft ? leftFunc(_left) : rightFunc(_right);
    }
}
