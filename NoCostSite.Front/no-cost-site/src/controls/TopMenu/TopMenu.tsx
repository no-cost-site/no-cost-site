import React, {PropsWithChildren} from "react";
import {Dropdown, Header, Nav, Navbar} from "rsuite";
import {Icon, IconType} from "../Icon";

const topMenuStyles = {
    height: 56,
}

const treeIconStyles = {
    marginLeft: 20,
    marginRight: 20,
    marginTop: 18,
    marginBottom: 15,
    fontSize: 20,
};

const itemIconStyles = {
    fontSize: 20,
    marginRight: 0,
};

const titleStyles = {
    marginLeft: 20,
    lineHeight: 2.7,
    fontSize: 20,
}

interface TopMenuProps {
    title: string;
}

interface ItemProps {
    icon?: IconType;
    name?: string;
    onClick?: (name?: string) => void;
}

interface TreeItemProps {
    icon?: IconType;
    name?: string;
    onClick?: (name?: string) => void;
}

const Toggle = (props: any): JSX.Element => {
    return (
        <Icon {...props} type={IconType.Menu} style={treeIconStyles}/>
    )
};

export const Item = (props: PropsWithChildren<ItemProps>): JSX.Element => {
    return <Nav.Item onSelect={props.onClick} icon={<Icon type={props.icon} style={itemIconStyles}/>}>{props.children}</Nav.Item>;
};

export const Tree = (props: PropsWithChildren<{}>): JSX.Element => {
    return <Dropdown renderToggle={Toggle} placement="bottomEnd">{props.children}</Dropdown>;
};

export const TreeItem = (props: PropsWithChildren<TreeItemProps>): JSX.Element => {
    return <Dropdown.Item onSelect={props.onClick} icon={<Icon type={props.icon}/>}>{props.children}</Dropdown.Item>;
};

export const Title = (props: PropsWithChildren<{}>): JSX.Element => {
    return <span style={titleStyles}>{props.children}</span>;
};

export class TopMenu extends React.Component<PropsWithChildren<TopMenuProps>> {
    public static readonly Item = Item;
    public static readonly Tree = Tree;
    public static readonly TreeItem = TreeItem;

    public render(): JSX.Element {
        return (
            <Header>
                <Navbar appearance="inverse" style={topMenuStyles}>
                    <Nav>
                        <Title>{this.props.title}</Title>
                    </Nav>
                    <Nav pullRight>
                        {this.props.children}
                    </Nav>
                </Navbar>
            </Header>
        );
    }
}
