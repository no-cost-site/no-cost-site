import React, {PropsWithChildren} from "react";
import {Loader as LoaderUI} from "rsuite";

interface CenterProps {
    text?: string;
}

export const CenterBackdrop = (props: CenterProps): JSX.Element => {
    return <LoaderUI backdrop content={props.text || "Loading..."} vertical speed="slow" size="lg" />;
};

export const Center = (props: CenterProps): JSX.Element => {
    return <LoaderUI center content={props.text || "Loading..."} vertical speed="slow" size="lg" />;
};

export class Loader extends React.Component<PropsWithChildren<{}>> {
    public static readonly CenterBackdrop = CenterBackdrop;
    public static readonly Center = Center;

    public render(): JSX.Element {
        return <>{this.props.children}</>;
    }
}