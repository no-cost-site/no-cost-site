import {useNavigate} from "react-router-dom";
import React, {useState} from "react";
import {PageDto} from "../../Api/dto";
import {PagesApi, UploadApi} from "../../Api";
import {Button, Form, Input, HtmlEditor, Select} from "../../controls";
import {Context} from "../Context/AppContext";
import {Guid, Lock} from "../../utils";

const pageStyles = {
    maxWidth: "100%",
}

const newPage: PageDto = {
    Id: "",
    TemplateId: "",
    Name: "Page name",
    Url: "",
    Title: "",
    Description: "",
    Keywords: "",
    Content: "",
}

export const PageCreate = (): JSX.Element => {
    const navigate = useNavigate();

    const {templates, readAll} = React.useContext(Context);
    const [page, setPage] = useState<PageDto>({...newPage, Id: Guid.new(), TemplateId: templates[0].Id});
    const [lock, setLock] = React.useState<boolean>(false);

    const saveAdnPublish = async (): Promise<void> => {
        await Lock.in(async () => {
            await PagesApi.Upsert({Page: page!});
            await UploadApi.UpsertPage({PageId: page!.Id});
            await readAll({pages: true, files: true});
            navigate(`/pages/page/${page!.Id}`);
        }, setLock)
    }

    const save = async (): Promise<void> => {
        await Lock.in(async () => {
            await PagesApi.Upsert({Page: page!});
            await readAll({pages: true});
            navigate(`/pages/page/${page!.Id}`);
        }, setLock)
    }

    const onChangeState = (value: string, name?: string) => {
        setPage(x => ({...x!, [name!]: value}));
    }

    const templateValues = templates.map(x => ({text: x.Name, value: x.Id}));

    return (
        <>
            <h1>Create page</h1>
            <Form style={pageStyles}>
                <Form.Input text="Name">
                    <Input name="Name" value={page.Name} onChange={onChangeState}/>
                </Form.Input>
                <Form.Input text="Url">
                    <Input name="Url" value={page.Url} onChange={onChangeState}/>
                </Form.Input>
                <Form.Input text="Template">
                    <Select name="TemplateId" value={page.TemplateId} values={templateValues} onChange={onChangeState}/>
                </Form.Input>
                <Form.Input text="Title" help="Tag <!-- Title --> in template">
                    <Input name="Title" value={page.Title} onChange={onChangeState}/>
                </Form.Input>
                <Form.Input text="Description" help="Tag <!-- Description --> in template">
                    <Input name="Description" value={page.Description} onChange={onChangeState}/>
                </Form.Input>
                <Form.Input text="Keywords" help="Tag <!-- Keywords --> in template">
                    <Input name="Keywords" value={page.Keywords} onChange={onChangeState}/>
                </Form.Input>
                <Form.Input text="Content" help="Tag <!-- Content --> in template">
                    <HtmlEditor name="Content" value={page.Content} onChange={onChangeState}/>
                </Form.Input>
                <Form.Buttons>
                    <Button name="create-and-publish" text="Create and publish" loading={lock}
                            onClick={saveAdnPublish}/>
                    <Button name="create" text="Create draft" type="default" loading={lock} onClick={save}/>
                </Form.Buttons>
            </Form>
        </>
    )
}