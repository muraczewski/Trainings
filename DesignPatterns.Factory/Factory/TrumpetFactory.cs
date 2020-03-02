using DesignPatterns.Factory.Instrument;

namespace DesignPatterns.Factory.Factory
{
    public class TrumpetFactory : IInstrumentFactory
    {
        public InstrumentType InstrumentType => InstrumentType.Trumpet;

        public IInstrument CreateInstrument()
        {
            return new Trumpet();
        }
    }
}
