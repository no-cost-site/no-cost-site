import {Navigate} from "react-router-dom";
import React from "react";
import {Context} from "../Context/AppContext";

export const Files = (): JSX.Element => {
    const {files} = React.useContext(Context);

    return (
        <Navigate to={`/files/file/${files[0].Id}`}/>
    )
}