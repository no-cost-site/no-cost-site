import React, {PropsWithChildren} from "react";
import {Dropdown, Nav, Sidebar, Sidenav} from "rsuite";
import {IconType, Icon} from "../Icon";

const leftMenuStyles = {
    backgroundColor: "#f7f7fa",
};

const headerStyles = {
    padding: 18,
    fontSize: 20,
    height: 56,
    marginBottom: 15,
};

const headerIconStyles = {
    marginRight: 12,
};

const itemStyles = {
    marginRight: 12,
    paddingLeft: 30,
    paddingTop: 9,
    paddingBottom: 10,
    fontSize: 14,
};

interface HeaderProps {
    icon: IconType;
}

interface ItemProps {
    name?: string;
    onClick?: (name?: string) => void;
}

interface TreeProps {
    header: string;
}

export const Header = (props: PropsWithChildren<HeaderProps>): JSX.Element => {
    return (
        <Sidenav.Header style={headerStyles}>
            <Icon type={props.icon} style={headerIconStyles}/>
            {props.children}
        </Sidenav.Header>
    );
};

export const Item = (props: PropsWithChildren<ItemProps>): JSX.Element => {
    return <Nav.Item onSelect={props.onClick} style={itemStyles}>{props.children}</Nav.Item>;
};

export const Tree = (props: PropsWithChildren<TreeProps>): JSX.Element => {
    return <Dropdown title={props.header} eventKey="1">{props.children}</Dropdown>;
};

export const TreeItem = (props: PropsWithChildren<{}>): JSX.Element => {
    return <Dropdown.Item>{props.children}</Dropdown.Item>;
};

export const TreeDivider = (): JSX.Element => {
    return <Dropdown.Item divider/>;
};

export const Menu = (props: PropsWithChildren<{}>): JSX.Element => {
    return <Dropdown.Menu>{props.children}</Dropdown.Menu>;
};

export const MenuItem = (props: PropsWithChildren<{}>): JSX.Element => {
    return <Dropdown.Item>{props.children}</Dropdown.Item>;
};

export const MenuDivider = (): JSX.Element => {
    return <Dropdown.Item divider/>;
};

export class LeftMenu extends React.Component<PropsWithChildren<{}>> {
    public static readonly Header = Header;
    public static readonly Item = Item;
    public static readonly Tree = Tree;
    public static readonly TreeItem = TreeItem;
    public static readonly TreeDivider = TreeDivider;
    public static readonly Menu = Menu;
    public static readonly MenuItem = MenuItem;
    public static readonly MenuDivider = MenuDivider;

    public render(): JSX.Element {
        return (
            <Sidebar style={leftMenuStyles}>
                <Sidenav defaultOpenKeys={['1']}>
                    <Sidenav.Body>
                        <Nav>
                            {this.props.children}
                        </Nav>
                    </Sidenav.Body>
                </Sidenav>
            </Sidebar>
        );
    }
}
