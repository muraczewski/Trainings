using DesignPatterns.Factory.Instrument;

namespace DesignPatterns.Factory.Services
{
    public interface IInstrumentService
    {
        IInstrument CreateInstrument(InstrumentType instrumentType);
    }
}
