import React, {PropsWithChildren} from "react";
import {Loader as LoaderUI, Placeholder as PlaceholderUI} from "rsuite";

interface CenterProps {
    text?: string;
}

interface PlaceholderProps {
    rows?: number;
    graph?: boolean | 'circle' | 'square' | 'image';
}

export const CenterBackdrop = (props: CenterProps): JSX.Element => {
    return <LoaderUI backdrop content={props.text || "Loading..."} vertical speed="slow" size="lg" />;
};

export const Center = (props: CenterProps): JSX.Element => {
    return <LoaderUI center content={props.text || "Loading..."} vertical speed="slow" size="lg" />;
};

export const Placeholder = (props: PlaceholderProps): JSX.Element => {
    return <PlaceholderUI rows={props.rows || 10} graph={props.graph} />;
};

export class Loader extends React.Component<PropsWithChildren<{}>> {
    public static readonly CenterBackdrop = CenterBackdrop;
    public static readonly Center = Center;
    public static readonly Placeholder = Placeholder;

    public render(): JSX.Element {
        return <>{this.props.children}</>;
    }
}