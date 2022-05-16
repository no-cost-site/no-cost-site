import React from 'react';
import {Context} from "../Context/AppContext";
import {IconType, LeftMenu as LeftMenuUI} from "../../controls";
import {PageItemDto} from "../../Api/dto";
import {useNavigate} from "react-router-dom";

export const PagesLeftMenu = (): JSX.Element => {
    const navigate = useNavigate();
    const {pages} = React.useContext(Context);

    const onClick = (page: PageItemDto) => {
        navigate(`/pages/page/${page!.Id}`);
    }

    const onCreate = async () => {
        navigate("/pages/page/create");
    }

    return (
        <LeftMenuUI>
            <LeftMenuUI.Header icon={IconType.Page}>Pages</LeftMenuUI.Header>
            {pages.map(x => (
                <LeftMenuUI.Item key={x.Id} onClick={() => onClick(x)}>{x.Name}</LeftMenuUI.Item>
            ))}
            <LeftMenuUI.ItemMain onClick={onCreate} icon={IconType.Plus}>Create new</LeftMenuUI.ItemMain>
        </LeftMenuUI>
    )
}