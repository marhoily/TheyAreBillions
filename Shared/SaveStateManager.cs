namespace Shared;

public class SaveStateManager
{
    public enum ColorCode
    {
        Clean, Dirty, SaveRequested
    }
    private enum State
    {
        Clean, Dirty, JustBecameDirty
    }
    private State _state;
    private bool _isSaveRequested;
    private readonly Func<bool> _checkDirty;
    private readonly Action<ColorCode> _updateColor;
    private readonly Action _save;

    public SaveStateManager(Func<bool> checkDirty,
        Action<ColorCode> updateColor, Action save)
    {
        _checkDirty = checkDirty;
        _updateColor = updateColor;
        _save = save;
    }

    public void Tick()
    {
        if (_isSaveRequested && _state == State.Dirty)
            _save();
        if (_state == State.JustBecameDirty)
            _state = State.Dirty;
        if (_checkDirty())
        {
            if (_state == State.Clean)
            {
                _state = State.JustBecameDirty;
            }
            _updateColor(_isSaveRequested ? ColorCode.SaveRequested : ColorCode.Dirty);
        }
        else
        {
            _state = State.Clean;
            _isSaveRequested = false;
            _updateColor(ColorCode.Clean);
        }
    }

    public void RequestSave()
    {
        _isSaveRequested = true;
        _updateColor(ColorCode.SaveRequested);
    }
}