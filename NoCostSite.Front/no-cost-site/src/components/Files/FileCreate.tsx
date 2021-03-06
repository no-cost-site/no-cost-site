import {useNavigate} from "react-router-dom";
import React, {useState} from "react";
import {FileDto} from "../../Api/dto";
import {UploadApi} from "../../Api";
import {Button, Form, Input, HtmlEditor} from "../../controls";
import {Context} from "../Context/AppContext";
import {Lock} from "../../utils";

const pageStyles = {
    maxWidth: "100%",
}

const newFile: FileDto = {
    Id: "",
    Url: "",
    Name: "FileName",
    Content: "",
}

export const FileCreate = (): JSX.Element => {
    const navigate = useNavigate();
    const {readAll} = React.useContext(Context);
    const [file, setFile] = useState<FileDto>({...newFile});
    const [lock, setLock] = React.useState<boolean>(false);

    const upload = async (): Promise<void> => {
        await Lock.in(async () => {
            const response = await UploadApi.UpsertFileContent({Url: file.Url, FileName: file.Name, Content: file.Content});
            await readAll({files: true});
            navigate(`/files/file/${response.FileId}`);
        }, setLock)
    }

    const onChangeState = (value: string, name?: string) => {
        setFile(x => ({...x!, [name!]: value}));
    }

    return (
        <>
            <h1>Create file</h1>
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
                    <Button name="upload" text="Create" loading={lock} onClick={upload}/>
                </Form.Buttons>
            </Form>
        </>
    )
}