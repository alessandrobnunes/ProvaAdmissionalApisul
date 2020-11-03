import React, { useEffect, useState } from 'react';

interface Props {
    elevador: string;
}
export const PercentualUsoElevador: React.FC<Props> = (props: Props) => {
    const displayName = `Percentual de Uso Elevador ${props.elevador}`;
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        getData();
    });


    const getData = async () => {
        const response = await fetch(`api/elevador/percentualusoelevador/${props.elevador}`);
        const data = await response.json();
        setData(data);
        setLoading(false);
    }
   

    let contents = loading
        ? <p><em>Loading...</em></p>
        : data;

    return (
        <div className="card">
            <div className="card-header text-center">
                <h5 id="tabelLabel" >{displayName}</h5>
            </div>
            <div className="card-body text-center">
                <h2>{contents}%</h2>
            </div>
        </div>
    );

}
export default PercentualUsoElevador;