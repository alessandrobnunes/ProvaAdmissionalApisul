using ProvaAdmissionalCSharpApisul.Services;
using Xunit;
using Xunit.Abstractions;

namespace ProvaAdmissionalCSharpApisul.Tests
{
    public class InputTests
    {
        private readonly ElevadorService _elevadorService;
        private readonly ITestOutputHelper _output;

        public InputTests(ITestOutputHelper output)
        {
            _elevadorService = new ElevadorService();
            _output = output;
        }

        [Fact]
        public void Andar_List_Record_Count_Is_16_Test()
        {
            var andarList = _elevadorService.GetAndarList();

            Assert.True(andarList.Count == 16);
        }

        [Fact]
        public void Turno_List_Record_Count_Is_3_Test()
        {
            var turnoList = _elevadorService.GetTurnoList();

            Assert.True(turnoList.Count == 3);
        }


        [Fact]
        public void Andar_8_Menos_Utilizado_Value_Is_Zero_Test()
        {
            var result = _elevadorService.AndarMenosUtilizado();
            bool test = false;

            foreach (var item in result)
            {
                if (test == false)
                    test = item.Andar == 8 && item.Utilizacao == 0;

                _output.WriteLine($"Andar: {item.Andar}, Utilizacao: {item.Utilizacao}");
            }
            _output.WriteLine($"Record Count: {result.Count}");

            Assert.True(test);
        }

        [Fact]
        public void Periodo_Maior_Fluxo_Elevador_Mais_Frequentado_Test()
        {
            var result = _elevadorService.PeriodoMaiorFluxoElevadorMaisFrequentado();

            foreach (var item in result)
            {
                _output.WriteLine($"Elevador: {item.Elevador}, Turno: {item.Turno}, " +
                    $"Utilizacao: {item.Utilizacao}");
            }
        }

        [Fact]
        public void Periodo_Menor_Fluxo_Elevador_Menos_Frequentado_Test()
        {
            var result = _elevadorService.PeriodoMenorFluxoElevadorMenosFrequentado();

            foreach (var item in result)
                _output.WriteLine($"Elevador: {item.Elevador}, Turno: {item.Turno}, " +
                    $"Utilizacao: {item.Utilizacao}");

            _output.WriteLine($"Record Count: {result.Count}");

        }

        [Fact]
        public void Sum_Of_Percentual_Uso_Elevadores_Is_100_Percent_Test()
        {
            decimal result = 0M;

            foreach (var elevador in _elevadorService.GetElevadorList())
                result += _elevadorService.PercentualDeUsoElevador(elevador);

            Assert.True(result == 100);
        }

        [Fact]
        public void Input_List_Record_Count_Is_23_Test()
        {
            var result = _elevadorService.GetInputList();

            foreach (var item in result)
                _output.WriteLine($"Elevador: {item.Elevador}, Andar: {item.Andar}, " +
                    $"Turno: {item.Turno}");

            _output.WriteLine($"Record Count: {result.Count}");

            Assert.True(result.Count == 23);
        }

        

    }
}
