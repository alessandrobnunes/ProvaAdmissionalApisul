using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ProvaAdmissionalCSharpApisul.Services;

namespace ProvaAdmissionalCSharpApisul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElevadorController : ControllerBase
    {
        private readonly IElevadorService _elevadorService;

        public ElevadorController(IElevadorService elevadorService)
        {
            _elevadorService = elevadorService;
        }

        [HttpGet("andarmenosutilizado")]
        public IActionResult GetAndarMenosUtilizado() =>
            Ok(_elevadorService.AndarMenosUtilizado());

        [HttpGet("elevadormaisfrequentado")]
        public IActionResult GetElevadorMaisFrequentado() =>
            Ok(_elevadorService.ElevadorMaisFrequentado());

        [HttpGet("elevadormenosfrequentado")]
        public IActionResult GetElevadorMenosFrequentado() =>
            Ok(_elevadorService.ElevadorMenosFrequentado());

        [HttpGet("percentualusoelevador/{elevador}")]
        public IActionResult GetPercentalUsoElevador(string elevador) =>
            Ok(_elevadorService.PercentualDeUsoElevador(elevador));

        [HttpGet("periodomaiorfluxoelevadormaisfrequentado")]
        public IActionResult GetPeriodoMaiorFluxoElevadorMaisFrequentado() =>
            Ok(_elevadorService.PeriodoMaiorFluxoElevadorMaisFrequentado());

        [HttpGet("periodomenorfluxoelevadormenosfrequentado")]
        public IActionResult GetPeriodoMenorFluxoElevadorMenosFrequentado() =>
            Ok(_elevadorService.PeriodoMenorFluxoElevadorMenosFrequentado());

        [HttpGet("periodomaiorutilizacaoconjuntoelevadores")]
        public IActionResult GetPeriodoMaiorUtilizacaoConjuntoElevadores() =>
            Ok(_elevadorService.PeriodoMaiorUtilizacaoConjuntoElevadores());

    }
}
