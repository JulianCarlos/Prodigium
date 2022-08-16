public interface ISaveable<T>
{
    public object CaptureState();
    public void RestoreState(T state);
}
