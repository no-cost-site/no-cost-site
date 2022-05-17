import {useNavigate} from "react-router-dom";
import React from "react";
import {Loader} from "../../controls";
import {TokenContainer} from "../../utils";
import {AuthContext, State} from "../Auth/Auth";

export const SignOut = (): JSX.Element => {
    const navigate = useNavigate();
    const {updateState} = React.useContext(AuthContext);

    React.useEffect(() => {
        TokenContainer.clear();
        updateState(State.Check);
        navigate("/pages");
    }, []);

    return (
        <Loader.Placeholder/>
    )
}