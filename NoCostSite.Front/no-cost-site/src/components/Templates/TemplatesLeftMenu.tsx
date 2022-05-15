import React from 'react';
import {Context} from "../Context/AppContext";
import {IconType, LeftMenu as LeftMenuUI} from "../../controls";
import {TemplateItemDto} from "../../Api/dto";
import {useNavigate} from "react-router-dom";

export const TemplatesLeftMenu = (): JSX.Element => {
    const navigate = useNavigate();
    const {templates} = React.useContext(Context);

    const onClick = (template: TemplateItemDto) => {
        navigate(`/templates/template/${template!.Id}`);
    }

    return (
        <LeftMenuUI>
            <LeftMenuUI.Header icon={IconType.Code}>Templates</LeftMenuUI.Header>
            {templates.map(x => (
                <LeftMenuUI.Item key={x.Id} onClick={() => onClick(x)}>{x.Name}</LeftMenuUI.Item>
            ))}
        </LeftMenuUI>
    )
}