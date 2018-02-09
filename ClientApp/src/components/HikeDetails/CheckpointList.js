import React from "react";
import CheckpointListRow from "./ChecpointListRow";

const CheckpointList = (props) => (
    <div>
        <table>
            <tbody>
                <tr>
                    <th>Név</th>
                    <th>Nyitás</th>
                    <th>Zárás</th>
                    <th>Távolság a rajttól</th>
                    <th>Leírás</th>
                </tr>
                {props.checkpoints.map((cp) =>
                    (<CheckpointListRow checkpoint={cp} key={cp.id} />))
                }
            </tbody>
        </table>
    </div>
)

export default CheckpointList;
