import React, {PropsWithChildren} from "react";
import {AuthApi} from "../../Api";
import {Loader} from "../../controls";
import {Login} from "./Login";
import {Register} from "./Register";

export enum State {
    Check,
    Auth,
    NotInit,
    NotAuth,
}

interface IContext {
    state: State;
    updateState: (state: State) => void;
}

const defaultContext: IContext = {
    state: State.Check,
    updateState: (state: State) => {
    },
}

export const AuthContext = React.createContext<IContext>({...defaultContext});

export const Auth = (props: PropsWithChildren<{}>): JSX.Element => {
    const [state, setState] = React.useState<IContext>(defaultContext);

    const updateState = (state: State) => {
        setState(x => ({...x, state}));

        if(state === State.Check){
            check();
        }
    }

    const check = async (): Promise<void> => {
        const responseIsInit = await AuthApi.IsInit();
        if (!responseIsInit.IsInit) {
            setState(x => ({...x, state: State.NotInit}));
            return;
        }

        const responseCheck = await AuthApi.Check();
        if (!responseCheck.IsSuccess) {
            setState(x => ({...x, state: State.NotAuth}));
            return;
        }

        setState(x => ({...x, state: State.Auth}));
    }

    const onLogin = () => {
        updateState(State.Auth);
    }

    React.useEffect(() => {
        check();
    }, []);

    switch (state.state) {
        case State.Check:
            return <Loader.CenterBackdrop/>;

        case State.NotInit:
            return <Register onLogin={onLogin}/>;

        case State.NotAuth:
            return <Login onLogin={onLogin}/>;

        default:
            return (
                <AuthContext.Provider value={{...state, updateState}}>
                    {props.children}
                </AuthContext.Provider>
            )
    }
}