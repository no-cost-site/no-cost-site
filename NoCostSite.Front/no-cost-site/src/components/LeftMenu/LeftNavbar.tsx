import React from 'react';
import {IconType, LeftNavbar as LeftNavbarUI, Show} from "../../controls";
import {useNavigate} from "react-router-dom";

export const LeftNavbar = (): JSX.Element => {
    const navigate = useNavigate();

    return (
        <Show.OnDesktop>
            <LeftNavbarUI>
                <LeftNavbarUI.Brand icon={IconType.Email} href="/"/>
                <LeftNavbarUI.Item icon={IconType.Page} onClick={() => navigate("/pages")}>Pages</LeftNavbarUI.Item>
                <LeftNavbarUI.Item icon={IconType.Code} onClick={() => navigate("/templates")}>Templates</LeftNavbarUI.Item>
            </LeftNavbarUI>
        </Show.OnDesktop>
    )
}