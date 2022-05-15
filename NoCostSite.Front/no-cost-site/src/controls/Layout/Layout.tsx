import React, {PropsWithChildren} from "react";
import {Container, Content} from "rsuite";

const bodyStyles = {
    padding: 15,
};

export const Body = (props: PropsWithChildren<{}>): JSX.Element => {
    return <Container><Content style={bodyStyles}>{props.children}</Content></Container>;
};

export class Layout extends React.Component<PropsWithChildren<{}>> {
    public static readonly Body = Body;

    public render(): JSX.Element {
        return <Container>{this.props.children}</Container>;
    }
}
