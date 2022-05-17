import {useNavigate, useParams} from "react-router-dom";
import React, {useState} from "react";
import {FileDto} from "../../Api/dto";
import {UploadApi} from "../../Api";
import {Button, Form, Input, Loader, HtmlEditor, Upload} from "../../controls";
import {Context} from "../Context/AppContext";
import {Lock} from "../../utils";

const pageStyles = {
    maxWidth: "100%",
}

export const File = (): JSX.Element => {
    const navigate = useNavigate();
    const fileId = useParams().fileId;

    const {files, readAll} = React.useContext(Context);
    const [file, setFile] = useState<FileDto | null>(null);
    const [currentFile, setCurrentFile] = useState<FileDto | null>(null);
    const [lock, setLock] = React.useState<boolean>(false);

    const read = async (): Promise<void> => {
        const fileItem = files.filter(x => x.Id === fileId)[0];
        if (!fileItem) {
            navigate("/files");
        }

        const response = await UploadApi.ReadFile({Url: fileItem.Url, FileName: fileItem.Name});
        setFile(response.File);
        setCurrentFile(response.File);
    }

    const upload = async (): Promise<void> => {
        await Lock.in(async () => {
            if (currentFile!.Url !== file!.Url || currentFile!.Name !== file!.Name) {
                await UploadApi.DeleteFile({Url: currentFile!.Url, FileName: currentFile!.Name});
            }
            await UploadApi.UpsertFileContent({Url: file!.Url, FileName: file!.Name, Content: file!.Content});
            await readAll({files: true});
            setCurrentFile(file!);
        }, setLock)
    }

    const uploadFile = async (data: number[]): Promise<void> => {
        await Lock.in(async () => {
            if (currentFile!.Url !== file!.Url || currentFile!.Name !== file!.Name) {
                await UploadApi.DeleteFile({Url: currentFile!.Url, FileName: currentFile!.Name});
            }
            await UploadApi.UpsertFile({Url: file!.Url, FileName: file!.Name, Data: data});
            await readAll({files: true});
            await read();
        }, setLock)
    }

    const open = () => {
        const url = file!.Url ? `/${file!.Url}` : "";
        window.open(`http://no-cost-site.olrix.net.website.yandexcloud.net${url}/${file!.Name}`);
    }

    const deleteFile = async (): Promise<void> => {
        await Lock.in(async () => {
            await UploadApi.DeleteFile({Url: file!.Url, FileName: file!.Name});
            await readAll({files: true});
            navigate("/files");
        }, setLock)
    }

    const onChangeState = (value: string, name?: string) => {
        setFile(x => ({...x!, [name!]: value}));
    }

    React.useEffect(() => {
        setFile(null);
        read();
    }, [fileId]);

    if (!file) {
        return <Loader.Center/>;
    }

    return (
        <>
            <h1>{file.Name}</h1>
            <Form style={pageStyles}>
                <Form.Input text="Name">
                    <Input name="Name" value={file.Name} onChange={onChangeState}/>
                </Form.Input>
                <Form.Input text="Url">
                    <Input name="Url" value={file.Url} onChange={onChangeState}/>
                </Form.Input>
                <Form.Input text="Content">
                    <HtmlEditor name="Content" value={file.Content} onChange={onChangeState}/>
                </Form.Input>
                <Form.Buttons>
                    <Button name="upload" text="Save" loading={lock} onClick={upload}/>
                    <Upload name="upload-file" text="Upload file" loading={lock} onUpload={uploadFile}/>
                    <Button name="delete" text="Delete" type="subtle" loading={lock} onClick={deleteFile}/>
                    <Button name="open" text="Open on site" type="link" onClick={open}/>
                </Form.Buttons>
            </Form>
        </>
    )
}