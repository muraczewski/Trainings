using System;
using System.Collections.Generic;
using System.Linq;
using DesignPatterns.Factory.Factory;
using DesignPatterns.Factory.Instrument;

namespace DesignPatterns.Factory.Services
{
    public class InstrumentService : IInstrumentService
    {
        private readonly IEnumerable<IInstrumentFactory> _factories;

        public InstrumentService(IEnumerable<IInstrumentFactory> factories)
        {
            _factories = factories;
        }

        public IInstrument CreateInstrument(InstrumentType instrumentType)
        {
            var properFactory = _factories.SingleOrDefault(f => f.InstrumentType == instrumentType);

            if (properFactory == null)
            {
                throw new NotImplementedException();
            }

            return properFactory.CreateInstrument();
        }
    }
}
