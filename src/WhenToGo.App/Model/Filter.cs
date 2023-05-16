namespace WhenToGo
{
    public class Filter<T> where T: class
    {
        private Func<T, T> _filteringFunc;

        public Filter(Func<T, T> filteringFunc)
        {
            _filteringFunc = filteringFunc;
        }

        public T FilterValues(T values)
        {
            return _filteringFunc?.Invoke(values);
        }
    }
}
