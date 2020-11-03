using ProvaAdmissionalCSharpApisul.Models;
using System.Collections.Generic;

namespace ProvaAdmissionalCSharpApisul.Services
{
    public interface IElevadorService
    {
        /// <summary> Deve retornar uma List contendo o(s) andar(es) menos utilizado(s). </summary> 
        List<AndarUtilizacaoModel> AndarMenosUtilizado();
        /// <summary> Deve retornar uma List contendo o(s) elevador(es) mais frequentado(s). </summary> 
        List<ElevadorFrequentacaoModel> ElevadorMaisFrequentado();
        /// <summary> Deve retornar uma List contendo o período de maior fluxo de cada um dos elevadores mais frequentados (se houver mais de um). </summary> 
        List<ElevadorPeriodoUtilizacaoModel> PeriodoMaiorFluxoElevadorMaisFrequentado();
        /// <summary> Deve retornar uma List contendo o(s) elevador(es) menos frequentado(s). </summary> 
        List<ElevadorFrequentacaoModel> ElevadorMenosFrequentado();
        /// <summary> Deve retornar uma List contendo o período de menor fluxo de cada um dos elevadores menos frequentados (se houver mais de um). </summary> 
        List<ElevadorPeriodoUtilizacaoModel> PeriodoMenorFluxoElevadorMenosFrequentado();
        /// <summary> Deve retornar uma List contendo o(s) periodo(s) de maior utilização do conjunto de elevadores. </summary> 
        List<PeriodoUtilizacaoModel> PeriodoMaiorUtilizacaoConjuntoElevadores();
        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador A em relação a todos os serviços prestados. </summary> 
        decimal PercentualDeUsoElevador(string elevador);

    }
}