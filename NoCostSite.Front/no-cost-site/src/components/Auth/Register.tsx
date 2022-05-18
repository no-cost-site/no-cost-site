import React from "react";
import {AuthApi} from "../../Api";
import {Button, Form, Password, Select} from "../../controls";
import {TokenContainer, Lock} from "../../utils";

interface Props {
    onLogin: () => void;
}

interface State {
    password: string,
    passwordConfirm: string,
    language: string,
}

export const Register = (props: Props): JSX.Element => {
    const [state, setState] = React.useState<State>({password: "", passwordConfirm: "", language: "en"});
    const [lock, setLock] = React.useState<boolean>(false);

    const onChangeState = (value: string, name?: string) => {
        setState(x => ({...x, [name!]: value}));
    }

    const register = async () => {
        await Lock.in(async () => {
            await AuthApi.Register({
                Settings: {
                    Language: state.language,
                },
                Password: state.password,
                PasswordConfirm: state.passwordConfirm,
            });

            const responseLogin = await AuthApi.Login({
                Password: state.password
            });

            TokenContainer.set(responseLogin.Token);

            props.onLogin();
        }, setLock)
    }

    const languages = [
        {text: "English", value: "en"},
        {text: "Русский", value: "ru"},
    ]

    return (
        <Form center vertical border>
            <Form.Header>Sign up</Form.Header>
            <Form.Input text="Password">
                <Password name="password" value={state.password} onChange={onChangeState}/>
            </Form.Input>
            <Form.Input text="Confirm password">
                <Password name="passwordConfirm" value={state.passwordConfirm} onChange={onChangeState}/>
            </Form.Input>
            <Form.Input text="Language">
                <Select name="language" value={state.language} values={languages} onChange={onChangeState}/>
            </Form.Input>
            <Form.Buttons>
                <Button name="register" text="Sing up" loading={lock} onClick={register}/>
            </Form.Buttons>
        </Form>
    )
}