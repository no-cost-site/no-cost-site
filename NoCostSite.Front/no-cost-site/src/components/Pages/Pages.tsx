import {Navigate} from "react-router-dom";
import React from "react";
import {Context} from "../Context/AppContext";

export const Pages = (): JSX.Element => {
    const {pages} = React.useContext(Context);

    return (
        <Navigate to={`/pages/page/${pages[0].Id}`}/>
    )
}