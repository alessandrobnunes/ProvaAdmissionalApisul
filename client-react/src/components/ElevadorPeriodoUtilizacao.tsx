import React, { useEffect, useState } from 'react';

interface Props {
    tipo: string;
}

export const ElevadorPeriodoUtilizacao: React.FC<Props> = (props: Props) => {
    const displayName = `Período ${props.tipo} Fluxo Elevadores ${props.tipo === "Maior" ? "Mais" : "Menos"} Frequentados`;

    const [elevadores, setElevadores] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if (elevadores.length === 0)
            populateElevadores();
    });


    const populateElevadores = async () => {

        let response;
        if (props.tipo === "Maior") {
            response = await fetch('api/elevador/periodomaiorfluxoelevadormaisfrequentado');
        }
        else {
            response = await fetch('api/elevador/periodomenorfluxoelevadormenosfrequentado');
        }

        const data = await response.json();
        setElevadores(data);
        setLoading(false);
    }
    const renderElevadores = () => {
        return (

            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Elevador</th>
                        <th>Turno</th>
                        <th>Utilização</th>
                    </tr>
                </thead>
                <tbody>
                    {elevadores.map(result =>
                        <tr key={result.turno}>
                            <td>{result.elevador}</td>
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
export default ElevadorPeriodoUtilizacao;