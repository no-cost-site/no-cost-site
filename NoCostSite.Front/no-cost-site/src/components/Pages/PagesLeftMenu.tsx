import React from 'react';
import {Context} from "../Context/AppContext";
import {IconType, LeftMenu as LeftMenuUI} from "../../controls";
import {DirectoryDto, PageItemDto} from "../../Api/dto";
import {useNavigate} from "react-router-dom";

export const PagesLeftMenu = (): JSX.Element => {
    const navigate = useNavigate();
    const {pages, pagesDirectory} = React.useContext(Context);

    const onClick = (page: PageItemDto) => {
        navigate(`/pages/page/${page!.Id}`);
    }

    const onCreate = async () => {
        navigate("/pages/create");
    }

    return (
        <LeftMenuUI>
            <LeftMenuUI.Header icon={IconType.Page}>Pages</LeftMenuUI.Header>
            <LeftMenuUI.Tree header="/">
                <PagesLeftMenuItems
                    pages={pages}
                    directories={pagesDirectory.Child}
                    url={pagesDirectory.Url}
                    onClick={onClick}
                />
            </LeftMenuUI.Tree>
            <LeftMenuUI.ItemMain onClick={onCreate} icon={IconType.Plus}>Create new</LeftMenuUI.ItemMain>
        </LeftMenuUI>
    )
}

interface PagesLeftMenuItemsProps {
    pages: PageItemDto[];
    directories: DirectoryDto[];
    url: string;
    onClick: (page: PageItemDto) => void;
}

const PagesLeftMenuItems = (props: PagesLeftMenuItemsProps): JSX.Element => {
    const pages = props.pages.filter(x => x.Url === props.url);

    return (
        <>
            {props.directories.map(x => (
                <LeftMenuUI.TreeChild key={x.Url} header={x.Name} eventKey={x.Url}>
                    <PagesLeftMenuItems
                        key={x.Url}
                        pages={props.pages}
                        directories={x.Child}
                        url={x.Url}
                        onClick={props.onClick}
                    />
                </LeftMenuUI.TreeChild>
            ))}
            {pages.map(x => (
                <LeftMenuUI.TreeItem key={x.Id} onClick={() => props.onClick(x)}>{x.Name}</LeftMenuUI.TreeItem>
            ))}
        </>
    )
}