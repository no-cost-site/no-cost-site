import React from "react";
import {Button, Form, Password} from "../../controls";
import {TokenContainer, Lock} from "../../utils";
import {AuthApi} from "../../Api";

interface Props {
    onLogin: () => void;
}

interface State {
    password: string,
}

export const Login = (props: Props): JSX.Element => {
    const [state, setState] = React.useState<State>({password: ""});
    const [lock, setLock] = React.useState<boolean>(false);

    const onChangeState = (value: string, name?: string) => {
        setState(x => ({...x, [name!]: value}));
    }

    const login = async () => {
        await Lock.in(async () => {
            const responseLogin = await AuthApi.Login({
                Password: state.password
            });

            TokenContainer.set(responseLogin.Token);

            props.onLogin();
        }, setLock)
    }

    return (
        <Form center vertical border>
            <Form.Header>Sign in</Form.Header>
            <Form.Input text="Password">
                <Password name="password" value={state.password} onChange={onChangeState}/>
            </Form.Input>
            <Form.Buttons>
                <Button name="login" text="Sing in" loading={lock} onClick={login}/>
            </Form.Buttons>
        </Form>
    )
}