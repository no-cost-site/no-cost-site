import React, {PropsWithChildren} from "react";
import {ButtonToolbar, Form as FormUI} from "rsuite";
import {HtmlProps} from "../../utils";

const fromStyles = {
    marginTop: 10,
    marginBottom: 30,
    width: "100%",
    maxWidth: 500,
}

const fromCenterStyles = {
    marginLeft: "auto",
    marginRight: "auto",
}

const fromVerticalStyles = {
    marginTop: 100,
}

const fromBorderStyles = {
    border: "1px solid #e5e5ea",
    padding: 40,
    borderRadius: 5,
}

const headerStyles = {
    textAlign: "center",
    marginBottom: 40,
}

interface FormProps {
    center?: boolean;
    vertical?: boolean;
    border?: boolean;
}

interface InputProps {
    text: string;
    help?: string;
}

const Input = (props: PropsWithChildren<InputProps>): JSX.Element => {
    return (
        <FormUI.Group>
            <FormUI.ControlLabel>{props.text}</FormUI.ControlLabel>
            {props.children}
            {props.help && <FormUI.HelpText>{props.help}</FormUI.HelpText>}
        </FormUI.Group>
    );
};

const Row = (props: PropsWithChildren<{}>): JSX.Element => {
    return (
        <FormUI.Group>
            {props.children}
        </FormUI.Group>
    );
};

const Buttons = (props: PropsWithChildren<{}>): JSX.Element => {
    return (
        <FormUI.Group>
            <ButtonToolbar>
                {props.children}
            </ButtonToolbar>
        </FormUI.Group>
    );
};

const Header = (props: PropsWithChildren<{}>): JSX.Element => {
    return (
        <h2 style={headerStyles as any}>
            {props.children}
        </h2>
    );
};

export class Form extends React.Component<PropsWithChildren<FormProps & HtmlProps>> {
    public static readonly Input = Input;
    public static readonly Row = Row;
    public static readonly Buttons = Buttons;
    public static readonly Header = Header;

    public render(): JSX.Element {
        let styles = {...fromStyles,}

        if (this.props.center) {
            styles = {...styles, ...fromCenterStyles};
        }
        if (this.props.vertical) {
            styles = {...styles, ...fromVerticalStyles};
        }
        if (this.props.border) {
            styles = {...styles, ...fromBorderStyles};
        }

        return <FormUI style={{...styles, ...this.props.style}}>{this.props.children}</FormUI>;
    }
}