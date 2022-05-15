import React, {PropsWithChildren} from "react";

interface State {
    matches: boolean;
}

interface ShowWhenProps {
    media: string;
}

const ShowWhen = (props: PropsWithChildren<ShowWhenProps>): JSX.Element | null => {
    const [state, setState] = React.useState<State>({matches: window.matchMedia(props.media).matches});

    const onChange = (e: any) => setState(x => ({...x, matches: e.matches}));

    React.useEffect(() => {
        window.matchMedia(props.media).addEventListener("change", onChange);
    }, []);

    return state.matches ? <>{props.children}</> : null;
};

const OnMobile = (props: PropsWithChildren<{}>): JSX.Element | null => (
    <ShowWhen media="(max-width: 800px)">{props.children}</ShowWhen>
);

const OnDesktop = (props: PropsWithChildren<{}>): JSX.Element | null => (
    <ShowWhen media="(min-width: 800px)">{props.children}</ShowWhen>
);

export class Show extends React.Component<PropsWithChildren<{}>> {
    public static readonly OnMobile = OnMobile;
    public static readonly OnDesktop = OnDesktop;

    public render(): JSX.Element {
        return <>{this.props.children}</>;
    }
}