import React from 'react';
import {Context} from "../Context/AppContext";
import {IconType, LeftMenu as LeftMenuUI} from "../../controls";
import {DirectoryDto, FileItemDto} from "../../Api/dto";
import {useNavigate} from "react-router-dom";
import {UploadApi} from "../../Api";

const leftMenuStyles = {
    width: 500,
    flex: "0 0 500px"
}

export const FilesLeftMenu = (): JSX.Element => {
    const navigate = useNavigate();
    const {files, directory, readAll} = React.useContext(Context);

    const onClick = (file: FileItemDto) => {
        navigate(`/files/file/${file!.Id}`);
    }

    const onCreate = async () => {
        await UploadApi.UpsertFileContent({FileName: "NewFile", Url: "", Content: ""});
        await readAll({files: true});
    }

    return (
        <LeftMenuUI style={leftMenuStyles}>
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
    const styles = {marginLeft: 10};

    return (
        <>
            {props.directories.map(x => (
                <LeftMenuUI.TreeChild key={x.Url} header={x.Name} eventKey={x.Url} style={styles}>
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
                <LeftMenuUI.TreeItem key={x.Id} onClick={() => props.onClick(x)} style={styles}>{x.Name}</LeftMenuUI.TreeItem>
            ))}
        </>
    )
}