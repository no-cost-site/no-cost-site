import React from 'react';
import {Context} from "../Context/AppContext";
import {IconType, LeftMenu as LeftMenuUI} from "../../controls";
import {DirectoryDto, FileItemDto} from "../../Api/dto";
import {useNavigate} from "react-router-dom";

export const FilesLeftMenu = (): JSX.Element => {
    const navigate = useNavigate();
    const {files, filesDirectory} = React.useContext(Context);

    const onClick = (file: FileItemDto) => {
        navigate(`/files/file/${file!.Id}`);
    }

    const onCreate = async () => {
        navigate("/files/create");
    }

    const onUpload = async () => {
        navigate("/files/upload");
    }

    const onUploadZip = async () => {
        navigate("/files/upload/zip");
    }

    return (
        <LeftMenuUI>
            <LeftMenuUI.Header icon={IconType.Tree}>Files</LeftMenuUI.Header>
            <LeftMenuUI.Tree header="/">
                <FilesLeftMenuItems
                    files={files}
                    directories={filesDirectory.Child}
                    url={filesDirectory.Url}
                    onClick={onClick}
                />
            </LeftMenuUI.Tree>
            <LeftMenuUI.ItemMain onClick={onCreate} icon={IconType.Plus}>Create new</LeftMenuUI.ItemMain>
            <LeftMenuUI.ItemMain onClick={onUpload} icon={IconType.Import}>Upload file</LeftMenuUI.ItemMain>
            <LeftMenuUI.ItemMain onClick={onUploadZip} icon={IconType.Attachment}>Upload zip archive</LeftMenuUI.ItemMain>
        </LeftMenuUI>
    )
}

interface FilesLeftMenuItemsProps {
    files: FileItemDto[];
    directories: DirectoryDto[];
    url: string;
    onClick: (file: FileItemDto) => void;
}

const FilesLeftMenuItems = (props: FilesLeftMenuItemsProps): JSX.Element => {
    const files = props.files.filter(x => x.Url === props.url);

    return (
        <>
            {props.directories.map(x => (
                <LeftMenuUI.TreeChild key={x.Url} header={x.Name} eventKey={x.Url}>
                    <FilesLeftMenuItems
                        key={x.Url}
                        files={props.files}
                        directories={x.Child}
                        url={x.Url}
                        onClick={props.onClick}
                    />
                </LeftMenuUI.TreeChild>
            ))}
            {files.map(x => (
                <LeftMenuUI.TreeItem key={x.Id} onClick={() => props.onClick(x)}>{x.Name}</LeftMenuUI.TreeItem>
            ))}
        </>
    )
}