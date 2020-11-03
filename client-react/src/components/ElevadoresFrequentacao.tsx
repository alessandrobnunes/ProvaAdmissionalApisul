import React, { useEffect, useState } from 'react';

interface Props {
    tipo: string;
}

export const ElevadoresFrequentacao: React.FC<Props> = (props: Props) => {
    const displayName = `Elevadores ${props.tipo} Frequentados`;

    const [elevadores, setElevadores] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if (elevadores.length === 0)
            populateElevadores();
    });


    const populateElevadores = async () => {

        let response;
        if (props.tipo === "Mais") {
            response = await fetch('api/elevador/elevadormaisfrequentado');
        }
        else {
            response = await fetch('api/elevador/elevadormenosfrequentado');
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
                        <th>Frequentação</th>
                    </tr>
                </thead>
                <tbody>
                    {elevadores.map(result =>
                        <tr key={result.elevador}>
                            <td>{result.elevador}</td>
                            <td>{result.frequentacao}</td>
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
export default ElevadoresFrequentacao;