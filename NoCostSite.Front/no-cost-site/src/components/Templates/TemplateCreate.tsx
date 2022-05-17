import {useNavigate} from "react-router-dom";
import React, {useState} from "react";
import {TemplateDto} from "../../Api/dto";
import {TemplatesApi} from "../../Api";
import {Button, Form, Input, HtmlEditor} from "../../controls";
import {Context} from "../Context/AppContext";
import {Guid, Lock} from "../../utils";

const pageStyles = {
    maxWidth: "100%",
}

const newTemplate: TemplateDto = {
    Id: "",
    Name: "Template name",
    Content: "<!-- Content -->",
}

export const TemplateCreate = (): JSX.Element => {
    const navigate = useNavigate();

    const {readAll} = React.useContext(Context);
    const [template, setTemplate] = useState<TemplateDto>({...newTemplate, Id: Guid.new()});
    const [lock, setLock] = React.useState<boolean>(false);

    const save = async (): Promise<void> => {
        await Lock.in(async () => {
            await TemplatesApi.Upsert({Template: template});
            await readAll({templates: true});
            navigate(`/templates/template/${template.Id}`);
        }, setLock)
    }

    const onChangeState = (value: string, name?: string) => {
        setTemplate(x => ({...x!, [name!]: value}));
    }

    return (
        <>
            <h1>Create template</h1>
            <Form style={pageStyles}>
                <Form.Input text="Name">
                    <Input name="Name" value={template.Name} onChange={onChangeState}/>
                </Form.Input>
                <Form.Input
                    text="Content"
                    help="Use tags <!-- Title -->, <!-- Description -->, <!-- Keywords -->, <!-- Content --> in template"
                >
                    <HtmlEditor name="Content" value={template.Content} onChange={onChangeState}/>
                </Form.Input>
                <Form.Buttons>
                    <Button name="create" text="Create" loading={lock} onClick={save}/>
                </Form.Buttons>
            </Form>
        </>
    )
}