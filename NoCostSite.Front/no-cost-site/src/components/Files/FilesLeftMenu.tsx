import React from 'react';
import {Context} from "../Context/AppContext";
import {IconType, LeftMenu as LeftMenuUI} from "../../controls";
import {FileItemDto} from "../../Api/dto";
import {useNavigate} from "react-router-dom";
import {UploadApi} from "../../Api";

export const FilesLeftMenu = (): JSX.Element => {
    const navigate = useNavigate();
    const {files, readAll} = React.useContext(Context);

    const onClick = (template: FileItemDto) => {
        navigate(`/files/file/${template!.Id}`);
    }

    const onCreate = async () => {
        await UploadApi.UpsertFileContent({FileName: "NewFile", Url: "", Content: ""});
        await readAll({files: true});
    }

    return (
        <LeftMenuUI>
            <LeftMenuUI.Header icon={IconType.Tree}>Files</LeftMenuUI.Header>
            {files.map(x => (
                <LeftMenuUI.Item key={x.Id} onClick={() => onClick(x)}>{x.Url}/{x.Name}</LeftMenuUI.Item>
            ))}
            <LeftMenuUI.ItemMain onClick={onCreate} icon={IconType.Plus}>Create new</LeftMenuUI.ItemMain>
        </LeftMenuUI>
    )
}