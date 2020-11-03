import React, { Component } from 'react';
import { ElevadoresFrequentacao } from './ElevadoresFrequentacao';
import AndarMenosUtilizado from './AndarMenosUtilizado';
import { PercentualUsoElevador } from './PercentualUsoElevador';
import { ElevadorPeriodoUtilizacao } from './ElevadorPeriodoUtilizacao';
import { PeriodoMaiorUtilizacaoConjuntoElevadores } from './PeriodoMaiorUtilizacaoConjuntoElevadores';

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <div className="text-center">
                <div className="row text-center">
                    <div className="col col-lg-2">
                        <PercentualUsoElevador elevador="A" />
                    </div>
                    <div className="col col-lg-2">
                        <PercentualUsoElevador elevador="B" />
                    </div>
                    <div className="col col-lg-2">
                        <PercentualUsoElevador elevador="C" />
                    </div>

                    <div className="col col-lg-2">
                        <PercentualUsoElevador elevador="D" />
                    </div>
                    <div className="col col-lg-2">
                        <PercentualUsoElevador elevador="E" />
                    </div>
                </div>

                <div className="row">
                    <div className="col col-sm-4">
                        <AndarMenosUtilizado />
                    </div>
                    <div className="col col-sm-4">
                        <ElevadoresFrequentacao tipo="Mais" />
                    </div>
                    <div className="col col-sm-4">
                        <ElevadoresFrequentacao tipo="Menos" />
                    </div>
                </div>
                <div className="row">
                    <div className="col col-sm-4">
                        <PeriodoMaiorUtilizacaoConjuntoElevadores />
                    </div>
                    <div className="col col-sm-4">
                        <ElevadorPeriodoUtilizacao tipo="Maior" />
                    </div>
                    <div className="col col-sm-4">
                        <ElevadorPeriodoUtilizacao tipo="Menor" />
                    </div>
                </div>
            </div >
        );
    }
}
