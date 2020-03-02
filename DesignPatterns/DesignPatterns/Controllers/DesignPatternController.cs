using System.Threading;
using System.Threading.Tasks;
using DesignPatterns.Factory;
using DesignPatterns.Factory.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesignPatterns.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DesignPatternController : Controller
    {
        private readonly IInstrumentService _instrumentService;

        public DesignPatternController(IInstrumentService instrumentService)
        {
            _instrumentService = instrumentService;
        }

        [HttpGet("SimpleSingleton")]
        public async Task<IActionResult> GetSingleton(CancellationToken cancellationToken)
        {
            var firstSingleton = Singleton.Singleton.GetInstance;
            var secondSingleton = Singleton.Singleton.GetInstance;
            return Ok($"Counter: {firstSingleton.GetCount}, {secondSingleton.GetCount}");
        }

        [HttpGet("SingletonWithDoubleChecking")]
        public async Task<IActionResult> GetSingletonWithDoubleChecking(int id, CancellationToken cancellationToken)
        {
            var firstSingleton = Singleton.SingletonWithDoubleCheck.GetInstance;
            var secondSingleton = Singleton.SingletonWithDoubleCheck.GetInstance;

            return Ok($"Counter: {firstSingleton.GetCount}, {secondSingleton.GetCount}");
        }

        [HttpGet("Factory:CreateGuitar")]
        public async Task<IActionResult> GetFactoryGuitar(CancellationToken cancellationToken)
        {
            var instrument = _instrumentService.CreateInstrument(InstrumentType.Guitar);
            return Ok(instrument);
        }
    }
}
