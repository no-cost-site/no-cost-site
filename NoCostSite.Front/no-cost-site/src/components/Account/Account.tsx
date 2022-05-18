import {Navigate} from "react-router-dom";
import React from "react";

export const Account = (): JSX.Element => {
    return (
        <Navigate to="/account/settings"/>
    )
}