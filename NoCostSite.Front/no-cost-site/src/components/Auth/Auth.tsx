import React, {PropsWithChildren} from "react";
import {AuthApi} from "../../Api";
import {Loader} from "../../controls";
import {Login} from "./Login";
import {Register} from "./Register";

enum State {
    Check,
    Auth,
    NotInit,
    NotAuth,
}

export const Auth = (props: PropsWithChildren<{}>): JSX.Element => {
    const [state, setState] = React.useState<State>(State.Check);

    const check = async (): Promise<void> => {
        const responseIsInit = await AuthApi.IsInit();
        if (!responseIsInit.IsInit) {
            setState(State.NotInit);
            return;
        }

        const responseCheck = await AuthApi.Check();
        if (!responseCheck.IsSuccess) {
            setState(State.NotAuth);
            return;
        }

        setState(State.Auth);
    }

    const onLogin = () => {
        setState(State.Auth);
    }

    React.useEffect(() => {
        check();
    }, []);

    switch (state) {
        case State.Check:
            return <Loader.Center/>;

        case State.NotInit:
            return <Register onLogin={onLogin}/>;

        case State.NotAuth:
            return <Login onLogin={onLogin}/>;

        default:
            return <>{props.children}</>;
    }
}