import {useNavigate, useParams} from "react-router-dom";
import React, {useState} from "react";
import {TemplateDto} from "../../Api/dto";
import {TemplatesApi, UploadApi} from "../../Api";
import {Button, Form, Input, Loader, HtmlEditor} from "../../controls";
import {Context} from "../Context/AppContext";
import {Guid} from "../../utils";

const pageStyles = {
    maxWidth: "100%",
}

const newTemplate: TemplateDto = {
    Id: "",
    Name: "Template name",
    Content: "<!-- Content -->",
}

export const Template = (): JSX.Element => {
    const navigate = useNavigate();
    const templateId = useParams().templateId;
    const isCreate = templateId === "create";

    const {readAll} = React.useContext(Context);
    const [template, setTemplate] = useState<TemplateDto | null>(null);
    const [lock, setLock] = React.useState<boolean>(false);

    const inLock = async (action: () => Promise<void>): Promise<void> => {
        setLock(true);

        try {
            await action();
        } catch (e) {
            console.log(e);
        }

        setLock(false);
    }

    const read = async (): Promise<void> => {
        if (isCreate) {
            setTemplate({...newTemplate, Id: Guid.new()});
        } else {
            const response = await TemplatesApi.Read({Id: templateId!});
            setTemplate(response.Template);
        }
    }

    const saveAdnPublish = async (): Promise<void> => {
        await inLock(async () => {
            await TemplatesApi.Upsert({Template: template!});
            await UploadApi.UpsertTemplate({TemplateId: template!.Id});
            await readAll({templates: true});
        })
    }

    const save = async (): Promise<void> => {
        await inLock(async () => {
            await TemplatesApi.Upsert({Template: template!});
            await readAll({templates: true});

            if (isCreate) {
                navigate(`/templates/template/${template!.Id}`)
            }
        })
    }

    const deleteTemplate = async (): Promise<void> => {
        await inLock(async () => {
            await TemplatesApi.Delete({Id: template!.Id});
            await readAll({templates: true});
            navigate("/templates")
        })
    }

    const onChangeState = (value: string, name?: string) => {
        setTemplate(x => ({...x!, [name!]: value}));
    }

    React.useEffect(() => {
        setTemplate(null);
        read();
    }, [templateId]);

    if (!template) {
        return <Loader.Center/>;
    }

    return (
        <>
            <h1>{template.Name}</h1>
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
                    {isCreate && (
                        <Button name="create" text="Create" loading={lock} onClick={save}/>
                    )}
                    {!isCreate && (
                        <>
                            <Button name="save-and-publish" text="Save and publish" loading={lock}
                                    onClick={saveAdnPublish}/>
                            <Button name="save" text="Save draft" type="default" loading={lock} onClick={save}/>
                            <Button name="delete" text="Delete" type="subtle" loading={lock} onClick={deleteTemplate}/>
                        </>
                    )}
                </Form.Buttons>
            </Form>
        </>
    )
}