import React from "react";
import {AuthApi} from "../../Api";
import {Button, Form, Password} from "../../controls";
import {TokenContainer} from "../../utils";

interface Props {
    onLogin: () => void;
}

interface State {
    password: string,
    passwordConfirm: string,
}

export const Register = (props: Props): JSX.Element => {
    const [state, setState] = React.useState<State>({password: "", passwordConfirm: ""});
    const [lock, setLock] = React.useState<boolean>(false);

    const onChangeState = (value: string, name?: string) => {
        setState(x => ({...x, [name!]: value}));
    }

    const inLock = async (action: () => Promise<void>): Promise<void> => {
        setLock(true);

        try {
            await action();
        } catch (e) {
            console.log(e);
        }

        setLock(false);
    }

    const register = async () => {
        await inLock(async () => {
            await AuthApi.Register({
                Settings: {
                    Language: "en",
                },
                Password: state.password,
                PasswordConfirm: state.passwordConfirm,
            });

            const responseLogin = await AuthApi.Login({
                Password: state.password
            });

            TokenContainer.set(responseLogin.Token);

            props.onLogin();
        })
    }

    return (
        <Form center vertical border>
            <Form.Header>Sign up</Form.Header>
            <Form.Input text="Password">
                <Password name="password" value={state.password} onChange={onChangeState}/>
            </Form.Input>
            <Form.Input text="Confirm password">
                <Password name="passwordConfirm" value={state.passwordConfirm} onChange={onChangeState}/>
            </Form.Input>
            <Form.Buttons>
                <Button name="register" text="Sing up" loading={lock} onClick={register}/>
            </Form.Buttons>
        </Form>
    )
}