using DesignPatterns.Factory.Instrument;

namespace DesignPatterns.Factory.Factory
{
    public interface IInstrumentFactory
    {
        InstrumentType InstrumentType { get; }

        IInstrument CreateInstrument();
    }
}
