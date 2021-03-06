import React, {PropsWithChildren} from "react";
import {Affix, Nav, Sidenav} from "rsuite";
import {IconType, Icon} from "../Icon";

const affixStyles = {
    maxWidth: 56,
}

const menuStyles = {
    height: "100%",
}

const headerStyles = {
    padding: 18,
    fontSize: 20,
    height: 56,
    marginBottom: 15,
};

const headerIconStyles = {
    marginRight: 12,
    color: "white",
};

interface BrandProps {
    icon: IconType;
    href: string;
}

interface ItemProps {
    icon: IconType;
    name?: string;
    onClick?: (name?: string) => void;
}

export const Brand = (props: BrandProps): JSX.Element => {
    return (
        <Sidenav.Header style={headerStyles}>
            <a href={props.href}>
                <Icon type={props.icon} style={headerIconStyles}/>
            </a>
        </Sidenav.Header>
    );
};

export const Item = (props: PropsWithChildren<ItemProps>): JSX.Element => {
    return <Nav.Item onSelect={props.onClick} icon={<Icon type={props.icon}/>}>{props.children}</Nav.Item>;
};

export class LeftNavbar extends React.Component<PropsWithChildren<{}>> {
    public static readonly Brand = Brand;
    public static readonly Item = Item;

    public render(): JSX.Element {
        return (
            <Affix style={affixStyles} className="height-100">
                <Sidenav expanded={false} appearance="inverse" style={menuStyles}>
                    <Sidenav.Body>
                        <Nav>
                            {this.props.children}
                        </Nav>
                    </Sidenav.Body>
                </Sidenav>
            </Affix>
        );
    }
}
