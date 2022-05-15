import {IconType, Show, TopMenu as TopMenuUI} from "../../controls";
import React from "react";
import {useNavigate} from "react-router-dom";

export const TopMenu = (): JSX.Element => {
    const navigate = useNavigate();

    return (
        <Show.OnMobile>
            <TopMenuUI title="no-cost-site.CRM">
                <TopMenuUI.Item icon={IconType.Page} onClick={() => navigate("/pages")}/>
                <TopMenuUI.Item icon={IconType.Code} onClick={() => navigate("/templates")}/>
            </TopMenuUI>
        </Show.OnMobile>
    )
}