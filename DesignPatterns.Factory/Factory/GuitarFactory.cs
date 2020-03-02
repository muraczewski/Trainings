using DesignPatterns.Factory.Instrument;

namespace DesignPatterns.Factory.Factory
{
    public class GuitarFactory : IInstrumentFactory

    {
        public InstrumentType InstrumentType => InstrumentType.Guitar;

        public IInstrument CreateInstrument()
        {
            return new Guitar();
        }
    }
}
