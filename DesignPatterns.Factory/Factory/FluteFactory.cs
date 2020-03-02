using System;
using DesignPatterns.Factory.Instrument;

namespace DesignPatterns.Factory.Factory
{
    [Obsolete]
    public class FluteFactory : IInstrumentFactory
    {
        public InstrumentType InstrumentType => InstrumentType.Flute;

        public IInstrument CreateInstrument()
        {
            return new Flute();
        }
    }
}
