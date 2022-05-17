import {Button, Form, Password} from "../../controls";
import React from "react";
import {UsersApi} from "../../Api";
import {Lock} from "../../utils";

interface State {
    OldPassword: string;
    NewPassword: string;
    PasswordConfirm: string;
}

const defaultState = {
    OldPassword: "",
    NewPassword: "",
    PasswordConfirm: ""
}

export const ChangePassword = (): JSX.Element => {
    const [state, setState] = React.useState<State>({...defaultState});
    const [lock, setLock] = React.useState<boolean>(false);

    const onChangeState = (value: string, name?: string) => {
        setState(x => ({...x, [name!]: value}));
    }

    const changePassword = async () => {
        await Lock.in(async () => {
            await UsersApi.ChangePassword(state);
            setState({...defaultState});
        }, setLock)
    }
    return (
        <>
            <h1>Change password</h1>
            <Form>
                <Form.Input text="Old password">
                    <Password name="OldPassword" value={state.OldPassword} onChange={onChangeState}/>
                </Form.Input>
                <Form.Input text="New password">
                    <Password name="NewPassword" value={state.NewPassword} onChange={onChangeState}/>
                </Form.Input>
                <Form.Input text="New password confirm">
                    <Password name="PasswordConfirm" value={state.PasswordConfirm} onChange={onChangeState}/>
                </Form.Input>
                <Form.Row>
                    <Button name="change-password" text="Change" loading={lock} onClick={changePassword}/>
                </Form.Row>
            </Form>
        </>
    )
}