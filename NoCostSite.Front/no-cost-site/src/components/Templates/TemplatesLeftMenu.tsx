import React from 'react';
import {Context} from "../Context/AppContext";
import {IconType, LeftMenu as LeftMenuUI} from "../../controls";
import {TemplateItemDto} from "../../Api/dto";
import {useNavigate} from "react-router-dom";
import {TemplatesApi} from "../../Api";

export const TemplatesLeftMenu = (): JSX.Element => {
    const navigate = useNavigate();
    const {templates, readAll} = React.useContext(Context);

    const onClick = (template: TemplateItemDto) => {
        navigate(`/templates/template/${template!.Id}`);
    }

    const onCreate = async () => {
        await TemplatesApi.Upsert({
            Template: {Name: "NewTemplate", Content: "<!-- Content -->"} as any
        });
        await readAll({templates: true});
    }

    return (
        <LeftMenuUI>
            <LeftMenuUI.Header icon={IconType.Code}>Templates</LeftMenuUI.Header>
            {templates.map(x => (
                <LeftMenuUI.Item key={x.Id} onClick={() => onClick(x)}>{x.Name}</LeftMenuUI.Item>
            ))}
            <LeftMenuUI.ItemMain onClick={onCreate} icon={IconType.Plus}>Create new</LeftMenuUI.ItemMain>
        </LeftMenuUI>
    )
}