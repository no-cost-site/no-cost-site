import {Navigate} from "react-router-dom";
import React from "react";
import {Context} from "../Context/AppContext";

export const Files = (): JSX.Element => {
    const {files} = React.useContext(Context);

    const url = files.length ? `/files/file/${files[0].Id}` : "/files/create";

    return (
        <Navigate to={url}/>
    )
}