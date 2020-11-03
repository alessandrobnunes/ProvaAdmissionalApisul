import React, { useEffect, useState } from 'react';

export const PeriodoMaiorUtilizacaoConjuntoElevadores = () => {
    const displayName = `Período Maior Utilização Conjunto Elevadores`;

    const [elevadores, setElevadores] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if (elevadores.length === 0)
            populateElevadores();
    });


    const populateElevadores = async () => {

        let response = await fetch('api/elevador/periodomaiorutilizacaoconjuntoelevadores');

        const data = await response.json();
        setElevadores(data);
        setLoading(false);
    }
    const renderElevadores = () => {
        return (

            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Turno</th>
                        <th>Utilização</th>
                    </tr>
                </thead>
                <tbody>
                    {elevadores.map(result =>
                        <tr key={result.turno}>
                            <td>{result.turno}</td>
                            <td>{result.utilizacao}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }


    let contents = loading
        ? <p><em>Loading...</em></p>
        : renderElevadores();

    return (
        <div className="card">
            <div className="card-header text-center">
                <h5 id="tabelLabel" >{displayName}</h5>
            </div>
            <div className="card-body">
                {contents}
            </div>
        </div>
    );

}
export default PeriodoMaiorUtilizacaoConjuntoElevadores;