import {useNavigate, useParams} from "react-router-dom";
import React, {useState} from "react";
import {PageDto} from "../../Api/dto";
import {PagesApi, UploadApi} from "../../Api";
import {Button, Form, Input, Loader, HtmlEditor, Select} from "../../controls";
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

export const Page = (): JSX.Element => {
    const navigate = useNavigate();
    const pageId = useParams().pageId;
    const isCreate = pageId === "create";

    const {templates, readAll} = React.useContext(Context);
    const [page, setPage] = useState<PageDto | null>(null);
    const [currentPage, setCurrentPage] = useState<PageDto | null>(null);
    const [lock, setLock] = React.useState<boolean>(false);

    const read = async (): Promise<void> => {
        if (isCreate) {
            const createPage = {...newPage, Id: Guid.new(), TemplateId: templates[0].Id};
            setPage(createPage);
            setCurrentPage(createPage);
            return;
        }

        const response = await PagesApi.Read({Id: pageId!});
        setPage(response.Page);
        setCurrentPage(response.Page);
    }

    const saveAdnPublish = async (): Promise<void> => {
        await Lock.in(async () => {
            if (!isCreate && currentPage!.Url !== page!.Url) {
                await UploadApi.DeletePage({PageId: page!.Id})
            }

            await PagesApi.Upsert({Page: page!});
            await UploadApi.UpsertPage({PageId: page!.Id});
            await readAll({pages: true, files: true});
            setCurrentPage(page!);

            if (isCreate) {
                navigate(`/pages/page/${page!.Id}`)
            }
        }, setLock)
    }

    const save = async (): Promise<void> => {
        await Lock.in(async () => {
            await PagesApi.Upsert({Page: page!});
            await readAll({pages: true});

            if (isCreate) {
                navigate(`/pages/page/${page!.Id}`)
            }
        }, setLock)
    }

    const open = () => {
        window.open(`http://no-cost-site.olrix.net.website.yandexcloud.net/${page!.Url}`);
    }

    const deletePage = async (): Promise<void> => {
        await Lock.in(async () => {
            await UploadApi.DeletePage({PageId: page!.Id});
            await PagesApi.Delete({Id: page!.Id});
            await readAll({pages: true, files: true});
            navigate("/pages")
        }, setLock)
    }

    const onChangeState = (value: string, name?: string) => {
        setPage(x => ({...x!, [name!]: value}));
    }

    React.useEffect(() => {
        setPage(null);
        read();
    }, [pageId]);

    if (!page) {
        return <Loader.Center/>;
    }

    const templateValues = templates.map(x => ({text: x.Name, value: x.Id}));

    return (
        <>
            <h1>{page.Name}</h1>
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
                    {isCreate && (
                        <>
                            <Button name="create-and-publish" text="Create and publish" loading={lock}
                                    onClick={saveAdnPublish}/>
                            <Button name="create" text="Create draft" type="default" loading={lock} onClick={save}/>
                        </>
                    )}
                    {!isCreate && (
                        <>
                            <Button name="save-and-publish" text="Save and publish" loading={lock}
                                    onClick={saveAdnPublish}/>
                            <Button name="save" text="Save draft" type="default" loading={lock} onClick={save}/>
                            <Button name="open" text="Open on site" type="link" loading={lock} onClick={open}/>
                            <Button name="delete" text="Delete" type="subtle" loading={lock} onClick={deletePage}/>
                        </>
                    )}

                </Form.Buttons>
            </Form>
        </>
    )
}