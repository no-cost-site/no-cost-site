import {Navigate} from "react-router-dom";
import React from "react";
import {Context} from "../Context/AppContext";

export const Templates = (): JSX.Element => {
    const {templates} = React.useContext(Context);

    return (
        <Navigate to={`/templates/template/${templates[0].Id}`}/>
    )
}