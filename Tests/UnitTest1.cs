using FluentAssertions;
using Shared;

namespace Tests
{
    public class SaveStateManagerTests
    {
        private const SaveStateManager.ColorCode InvalidColorCode = (SaveStateManager.ColorCode) 42;
        private bool _isDirty;
        private SaveStateManager.ColorCode _colorCode = InvalidColorCode;
        private int _saveCounter;
        private readonly SaveStateManager _sut;

        public SaveStateManagerTests()
        {
            _sut = new SaveStateManager(
                () => _isDirty,
                c => _colorCode = c,
                () =>
                {
                    _isDirty = false;
                    _saveCounter++;
                });
        }


        [Fact]
        public void Start_With_Clean()
        {
            _isDirty = false;
            _sut.Tick();
            _colorCode.Should().Be(SaveStateManager.ColorCode.Clean);
            _saveCounter.Should().Be(0);
        }

        [Fact]
        public void Start_With_Dirty()
        {
            _isDirty = true;
            _sut.Tick();
            _colorCode.Should().Be(SaveStateManager.ColorCode.Dirty);
            _saveCounter.Should().Be(0);
        }

        [Fact]
        public void Dirty_And_SaveRequested()
        {
            _isDirty = true;
            _sut.RequestSave();
            _sut.Tick();
            _sut.Tick();
            _colorCode.Should().Be(SaveStateManager.ColorCode.SaveRequested);
            _saveCounter.Should().Be(0);
            _sut.Tick();
            _saveCounter.Should().Be(1);
            _colorCode.Should().Be(SaveStateManager.ColorCode.Clean);
        }

        [Fact]
        public void Dirty_Then_SaveRequested()
        {
            _isDirty = true;
            _sut.Tick();
            _sut.Tick();
            _sut.RequestSave();
            _colorCode.Should().Be(SaveStateManager.ColorCode.SaveRequested);
            _saveCounter.Should().Be(0);
            _sut.Tick();
            _saveCounter.Should().Be(1);
            _colorCode.Should().Be(SaveStateManager.ColorCode.Clean);
        }
        [Fact]
        public void Clean_And_SaveRequested()
        {
            _isDirty = false;
            _sut.RequestSave();
            _sut.Tick();
            _colorCode.Should().Be(SaveStateManager.ColorCode.Clean);
            _saveCounter.Should().Be(0);
        }
        [Fact]
        public void Clean_Then_SaveRequested()
        {
            _isDirty = false;
            _sut.Tick();
            _sut.RequestSave();
            _colorCode.Should().Be(SaveStateManager.ColorCode.SaveRequested);
            _saveCounter.Should().Be(0);
            _sut.Tick();
            _colorCode.Should().Be(SaveStateManager.ColorCode.Clean);
            _saveCounter.Should().Be(0);
        }
        [Fact]
        public void JustBecameDirty_Then_SaveRequested()
        {
            _isDirty = false;
            _sut.Tick();
            _isDirty = true;
            _sut.Tick();
            _colorCode.Should().Be(SaveStateManager.ColorCode.Dirty);
            _sut.RequestSave();
            _colorCode.Should().Be(SaveStateManager.ColorCode.SaveRequested);
            _saveCounter.Should().Be(0);
            _sut.Tick();
            _colorCode.Should().Be(SaveStateManager.ColorCode.SaveRequested);
            _saveCounter.Should().Be(0);
            _sut.Tick();
            _colorCode.Should().Be(SaveStateManager.ColorCode.Clean);
            _saveCounter.Should().Be(1);
        }
        [Fact]
        public void Internal_State_Should_Reset_To_Clean()
        {
            _isDirty = true;
            _sut.RequestSave();
            _sut.Tick();
            _sut.Tick();
            _sut.Tick();
            _colorCode.Should().Be(SaveStateManager.ColorCode.Clean);
            _saveCounter.Should().Be(1);

            _isDirty = true;
            _sut.RequestSave();
            _sut.Tick();
            _saveCounter.Should().Be(1);
            _sut.Tick();
            _saveCounter.Should().Be(1);
            _sut.Tick();
            _saveCounter.Should().Be(2);
            _colorCode.Should().Be(SaveStateManager.ColorCode.Clean);
        }
    }
}