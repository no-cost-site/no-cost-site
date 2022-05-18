import React from 'react';
import {IconType, LeftMenu as LeftMenuUI} from "../../controls";
import {useNavigate} from "react-router-dom";

export const AccountLeftMenu = (): JSX.Element => {
    const navigate = useNavigate();

    const onSettings = async () => {
        navigate("/account/settings");
    }

    const onChangePassword = async () => {
        navigate("/account/changePassword");
    }

    const onSignOut = async () => {
        navigate("/account/signOut");
    }

    return (
        <LeftMenuUI>
            <LeftMenuUI.Header icon={IconType.Admin}>Account</LeftMenuUI.Header>
            <LeftMenuUI.ItemMain onClick={onSettings} icon={IconType.Gear}>Settings</LeftMenuUI.ItemMain>
            <LeftMenuUI.ItemMain onClick={onChangePassword} icon={IconType.DataAuthorize}>Change password</LeftMenuUI.ItemMain>
            <LeftMenuUI.ItemMain onClick={onSignOut} icon={IconType.Exit}>Sign out</LeftMenuUI.ItemMain>
        </LeftMenuUI>
    )
}