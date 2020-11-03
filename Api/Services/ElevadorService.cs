using Newtonsoft.Json;
using ProvaAdmissionalCSharpApisul.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProvaAdmissionalCSharpApisul.Services
{
    public class ElevadorService : IElevadorService
    {
        private readonly List<int> _andarList = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        private readonly List<string> _elevadorList = new List<string>() { "A", "B", "C", "D", "E" };
        private readonly List<string> _turnoList = new List<string>() { "M", "V", "N" };
        private readonly IEnumerable<InputModel> _inputList =
            JsonConvert.DeserializeObject<List<InputModel>>(File.ReadAllText(@"data/input.json")).ToList();

        public ElevadorService() { }

        public List<InputModel> GetInputList() => _inputList.ToList();
        public List<string> GetElevadorList() => _elevadorList;
        public List<int> GetAndarList() => _andarList;
        public List<string> GetTurnoList() => _turnoList;

        public List<AndarUtilizacaoModel> AndarMenosUtilizado()
        {
            var andarMenosUtilizadoList = new List<AndarUtilizacaoModel>();
            foreach (var item in _andarList)
            {
                var query = _inputList
                    .GroupBy(x => x.Andar)
                        .Where(w => w.Key == item)
                        .Select(x => new AndarUtilizacaoModel
                        {
                            Andar = x.Key,
                            Utilizacao = x.Count()
                        })
                        .OrderBy(o => (o.Utilizacao, o.Andar))
                        .FirstOrDefault();

                andarMenosUtilizadoList.Add(new AndarUtilizacaoModel
                {
                    Andar = item,
                    Utilizacao = query == null ? 0 : query.Utilizacao
                });
            }

            return andarMenosUtilizadoList.OrderBy(o => o.Utilizacao).ToList();

        }

        public List<ElevadorFrequentacaoModel> ElevadorMaisFrequentado() =>
            _inputList.GroupBy(x => x.Elevador)
                  .Select(x => new ElevadorFrequentacaoModel
                  {
                      Elevador = x.Key,
                      Frequentacao = x.Count()
                  })
                  .OrderByDescending(o => o.Frequentacao).ToList();

        public List<ElevadorPeriodoUtilizacaoModel> PeriodoMaiorFluxoElevadorMaisFrequentado() =>
            ElevadorPeriodoUtilizacaoList()
                .OrderByDescending(o => (o.Frequentacao, o.Utilizacao)).ToList();

        public List<ElevadorFrequentacaoModel> ElevadorMenosFrequentado() =>
            _inputList.GroupBy(x => x.Elevador)
                  .Select(x => new ElevadorFrequentacaoModel
                  {
                      Elevador = x.Key,
                      Frequentacao = x.Count()
                  })
                  .OrderBy(o => o.Frequentacao).ToList();

        public List<ElevadorPeriodoUtilizacaoModel> PeriodoMenorFluxoElevadorMenosFrequentado() =>
            ElevadorPeriodoUtilizacaoList()
                .OrderBy(o => (o.Frequentacao, o.Utilizacao)).ToList();

        public List<PeriodoUtilizacaoModel> PeriodoMaiorUtilizacaoConjuntoElevadores() =>
            _inputList.GroupBy(x => x.Turno)
                 .Select(x => new PeriodoUtilizacaoModel
                 {
                     Turno = x.Key,
                     Utilizacao = x.Count()
                 })
                 .OrderByDescending(o => o.Utilizacao).ToList();

        public decimal PercentualDeUsoElevador(string elevador)
        {
            decimal usoTotal = _inputList.Count();
            decimal usoElevador = _inputList
                .Where(w => w.Elevador == elevador)
                .Count();

            decimal result = Math.Round((usoElevador / usoTotal) * 100, 2);

            return result;
        }

        // TODO: NÃO ESTÁ NA ORDEM CORRETA. REVISAR.
        private List<ElevadorPeriodoUtilizacaoModel> ElevadorPeriodoUtilizacaoList()
        {
            var elevadorMaisFrequentado = ElevadorMaisFrequentado();
            var elevadorPeriodoUtilizacao = new List<ElevadorPeriodoUtilizacaoModel>();

            foreach (var item in elevadorMaisFrequentado)
            {
                var periodo = _inputList
                    .Where(w => w.Elevador == item.Elevador)
                    .GroupBy(g => g.Turno)
                    .Select(s => new { Turno = s.Key, Utilizacao = s.Count() }).ToList();

                if (periodo.Count > 1)
                    foreach (var p in periodo)
                        elevadorPeriodoUtilizacao.Add(
                            new ElevadorPeriodoUtilizacaoModel
                            {
                                Elevador = item.Elevador,
                                Turno = p.Turno,
                                Frequentacao = item.Frequentacao,
                                Utilizacao = p.Utilizacao
                            });
            }

            return elevadorPeriodoUtilizacao;
        }

    }
}