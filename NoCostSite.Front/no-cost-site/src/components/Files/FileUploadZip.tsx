import {useNavigate} from "react-router-dom";
import React, {useState} from "react";
import {FileDto} from "../../Api/dto";
import {UploadApi} from "../../Api";
import {Form, Input, Upload} from "../../controls";
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

export const FileUploadZip = (): JSX.Element => {
    const navigate = useNavigate();
    const {readAll} = React.useContext(Context);
    const [file, setFile] = useState<FileDto>({...newFile});
    const [lock, setLock] = React.useState<boolean>(false);

    const upload = async (data: number[]): Promise<void> => {
        await Lock.in(async () => {
            await UploadApi.UpsertZip({Url: file.Url, Data: data});
            await readAll({files: true});
            navigate("/files");
        }, setLock)
    }

    const onChangeState = (value: string, name?: string) => {
        setFile(x => ({...x!, [name!]: value}));
    }

    return (
        <>
            <h1>Upload zip archive</h1>
            <Form style={pageStyles}>
                <Form.Input text="Url">
                    <Input name="Url" value={file.Url} onChange={onChangeState}/>
                </Form.Input>
                <Form.Buttons>
                    <Upload name="upload" text="Upload" loading={lock} onUpload={upload}/>
                </Form.Buttons>
            </Form>
        </>
    )
}