import React from "react";
import {
    DataAuthorize,
    EmailFill,
    Menu,
    Message,
    Notice,
    Send,
    Shield,
    FolderFill,
    List,
    Page,
    Detail,
    Code,
    Plus,
    Tree,
    Minus,
    ArrowRightLine,
    ArrowRight,
    FileUpload,
    Import,
} from "@rsuite/icons";
import {HtmlProps} from "../../utils";

export enum IconType {
    Message,
    Send,
    Notice,
    Email,
    Menu,
    Shield,
    DataAuthorize,
    FolderFill,
    List,
    Page,
    Detail,
    Code,
    Plus,
    Tree,
    Minus,
    ArrowRightLine,
    ArrowRight,
    FileUpload,
    Import,
}

interface IconProps {
    type?: IconType;
}

export const Icon = (props: IconProps & HtmlProps) => {
    if (props.type == null) {
        return <></>;
    }

    switch (props.type) {
        case IconType.Message:
            return <Message {...props}/>;
        case IconType.Send:
            return <Send {...props}/>;
        case IconType.Notice:
            return <Notice {...props}/>;
        case IconType.Email:
            return <EmailFill {...props}/>;
        case IconType.Menu:
            return <Menu {...props}/>;
        case IconType.Shield:
            return <Shield {...props}/>;
        case IconType.DataAuthorize:
            return <DataAuthorize {...props}/>;
        case IconType.FolderFill:
            return <FolderFill {...props}/>;
        case IconType.List:
            return <List {...props}/>;
        case IconType.Page:
            return <Page {...props}/>;
        case IconType.Detail:
            return <Detail {...props}/>;
        case IconType.Code:
            return <Code {...props}/>;
        case IconType.Plus:
            return <Plus {...props}/>;
        case IconType.Tree:
            return <Tree {...props}/>;
        case IconType.Minus:
            return <Minus {...props}/>;
        case IconType.ArrowRightLine:
            return <ArrowRightLine {...props}/>;
        case IconType.ArrowRight:
            return <ArrowRight {...props}/>;
        case IconType.FileUpload:
            return <FileUpload {...props}/>;
        case IconType.Import:
            return <Import {...props}/>;
        default:
            throw new Error(`Icon ${props.type} not found`);
    }
};