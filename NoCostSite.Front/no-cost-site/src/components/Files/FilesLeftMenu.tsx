import React from 'react';
import {Context} from "../Context/AppContext";
import {IconType, LeftMenu as LeftMenuUI} from "../../controls";
import {DirectoryDto, FileItemDto} from "../../Api/dto";
import {useNavigate} from "react-router-dom";

export const FilesLeftMenu = (): JSX.Element => {
    const navigate = useNavigate();
    const {files, directory} = React.useContext(Context);

    const onClick = (file: FileItemDto) => {
        navigate(`/files/file/${file!.Id}`);
    }

    const onCreate = async () => {
        navigate("/files/file/create");
    }

    return (
        <LeftMenuUI>
            <LeftMenuUI.Header icon={IconType.Tree}>Files</LeftMenuUI.Header>
            <LeftMenuUI.Tree header="/">
                <FilesLeftMenuItems
                    files={files}
                    directories={directory.Child}
                    url={directory.Url}
                    onClick={onClick}
                />
            </LeftMenuUI.Tree>
            <LeftMenuUI.ItemMain onClick={onCreate} icon={IconType.Plus}>Create new</LeftMenuUI.ItemMain>
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