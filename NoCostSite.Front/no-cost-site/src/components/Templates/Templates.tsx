import {Navigate} from "react-router-dom";
import React from "react";
import {Context} from "../Context/AppContext";

export const Templates = (): JSX.Element => {
    const {templates} = React.useContext(Context);

    const url = templates.length ? `/templates/template/${templates[0].Id}` : "/templates/create";

    return (
        <Navigate to={url}/>
    )
}