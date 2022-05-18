import {Button, Form, Select} from "../../controls";
import React from "react";
import {SettingsApi} from "../../Api";
import {Lock} from "../../utils";
import {SettingsDto} from "../../Api/dto";
import {Context} from "../Context/AppContext";

export const Settings = (): JSX.Element => {
    const {settings, readAll} = React.useContext(Context);
    const [state, setState] = React.useState<SettingsDto>({...settings});
    const [lock, setLock] = React.useState<boolean>(false);

    const onChangeState = (value: string, name?: string) => {
        setState(x => ({...x, [name!]: value}));
    }

    const changePassword = async () => {
        await Lock.in(async () => {
            await SettingsApi.Upsert({Settings: state});
            await readAll({settings: true});
        }, setLock)
    }

    const languages = [
        {text: "English", value: "en"},
        {text: "Русский", value: "ru"},
    ]

    return (
        <>
            <h1>Settings</h1>
            <Form>
                <Form.Input text="Language">
                    <Select name="Language" value={state.Language} values={languages} onChange={onChangeState}/>
                </Form.Input>
                <Form.Row>
                    <Button name="save" text="Save" loading={lock} onClick={changePassword}/>
                </Form.Row>
            </Form>
        </>
    )
}