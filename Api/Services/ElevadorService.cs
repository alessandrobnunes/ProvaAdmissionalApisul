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
            var result = new List<AndarUtilizacaoModel>();
            var utilizacao = 0;

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

                if (query == null || query.Utilizacao <= utilizacao)
                {
                    utilizacao = query == null ? 0 : query.Utilizacao;
                    result.Add(new AndarUtilizacaoModel
                    {
                        Andar = item,
                        Utilizacao = utilizacao
                    });
                }
            }

            return result.ToList();

        }

        public List<ElevadorFrequentacaoModel> ElevadorMaisFrequentado()
        {
            var frequentacao = 0;
            var result = new List<ElevadorFrequentacaoModel>();
            var query = _inputList.GroupBy(x => x.Elevador)
                .Select(x => new ElevadorFrequentacaoModel
                {
                    Elevador = x.Key,
                    Frequentacao = x.Count()
                })
                .OrderByDescending(o => o.Frequentacao).ToList();

            foreach (var item in query)
            {
                if (item.Frequentacao >= frequentacao)
                {
                    frequentacao = item.Frequentacao;
                    result.Add(new ElevadorFrequentacaoModel
                    {
                        Elevador = item.Elevador,
                        Frequentacao = item.Frequentacao
                    });
                }
            }

            return result;
        }



        public List<ElevadorPeriodoUtilizacaoModel> PeriodoMaiorFluxoElevadorMaisFrequentado()
        {
            var result = new List<ElevadorPeriodoUtilizacaoModel>();
            var elevadorMaisFrequentadoList = ElevadorMaisFrequentado();

            var utilizacao = 0;
            foreach (var elevador in elevadorMaisFrequentadoList)
            {
                var query = _inputList
                    .Where(w => w.Elevador == elevador.Elevador)
                    .GroupBy(x => x.Turno)
                    .Select(x => new ElevadorPeriodoUtilizacaoModel
                    {
                        Elevador = elevador.Elevador,
                        Turno = x.Key,
                        Utilizacao = x.Count()
                    })
                    .OrderByDescending(o => o.Utilizacao).ToList();

                foreach (var item in query)
                {
                    if (item.Utilizacao >= utilizacao)
                    {
                        result.Add(new ElevadorPeriodoUtilizacaoModel
                        {
                            Elevador=elevador.Elevador,
                            Turno = item.Turno,
                            Utilizacao = item.Utilizacao
                        });
                    }

                    utilizacao = item.Utilizacao;
                }
            }
            return result;
        }

        public List<ElevadorFrequentacaoModel> ElevadorMenosFrequentado()
        {
            var frequentacao = 1;
            var elevadorMenosFrequentadoList = new List<ElevadorFrequentacaoModel>();

            foreach (var item in _elevadorList)
            {
                var query = _inputList
                    .GroupBy(x => x.Elevador)
                        .Where(w => w.Key == item)
                        .Select(x => new ElevadorFrequentacaoModel
                        {
                            Elevador = x.Key,
                            Frequentacao = x.Count()
                        })
                        .OrderBy(o => o.Frequentacao)
                        .FirstOrDefault();

                if (query == null || query.Frequentacao <= frequentacao)
                {
                    frequentacao = query == null ? 0 : query.Frequentacao;
                    elevadorMenosFrequentadoList.Add(new ElevadorFrequentacaoModel
                    {
                        Elevador = item,
                        Frequentacao = frequentacao
                    });
                }
            }

            return elevadorMenosFrequentadoList;
        }

        public List<ElevadorPeriodoUtilizacaoModel> PeriodoMenorFluxoElevadorMenosFrequentado()
        {
            var elevadorMenosFrequentadoList = ElevadorMenosFrequentado();
            var utilizacao = 1;
            var result = new List<ElevadorPeriodoUtilizacaoModel>();
            foreach (var elevador in elevadorMenosFrequentadoList)
            {
                var query = _inputList
                   .Where(w => w.Elevador == elevador.Elevador)
                   .GroupBy(x => x.Turno)
                   .Select(x => new ElevadorPeriodoUtilizacaoModel
                   {
                       Turno = x.Key,
                       Utilizacao = x.Count()
                   })
                   .OrderBy(o => o.Utilizacao).ToList();

                foreach (var item in query)
                {
                    if (query == null || item.Utilizacao <= utilizacao)
                    {
                        utilizacao = query == null ? 0 : item.Utilizacao;
                        result.Add(new ElevadorPeriodoUtilizacaoModel
                        {
                            Elevador = elevador.Elevador,
                            Turno = item.Turno,
                            Utilizacao = item.Utilizacao
                        });
                    }
                }
            }

            return result;
        }

        public List<PeriodoUtilizacaoModel> PeriodoMaiorUtilizacaoConjuntoElevadores()
        {
            var periodoUtilizacaoList = new List<PeriodoUtilizacaoModel>();
            var query = _inputList.GroupBy(x => x.Turno)
                    .Select(x => new PeriodoUtilizacaoModel
                    {
                        Turno = x.Key,
                        Utilizacao = x.Count()
                    })
                    .OrderByDescending(o => o.Utilizacao).ToList();

            var utilizacao = 0;
            foreach (var item in query)
            {
                if (item.Utilizacao >= utilizacao)
                {
                    periodoUtilizacaoList.Add(new PeriodoUtilizacaoModel
                    {
                        Turno = item.Turno,
                        Utilizacao = item.Utilizacao
                    });
                }
                utilizacao = item.Utilizacao;
            }

            return periodoUtilizacaoList;
        }

        public decimal PercentualDeUsoElevador(string elevador)
        {
            decimal usoTotal = _inputList.Count();
            decimal usoElevador = _inputList
                .Where(w => w.Elevador == elevador)
                .Count();

            decimal result = Math.Round((usoElevador / usoTotal) * 100, 2);

            return result;
        }



    }
}