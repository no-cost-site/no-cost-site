import React from "react";
import {Button as ButtonUI} from "rsuite";

export interface ButtonProps {
    name: string;
    text: string;
    type?: "default" | "primary" | "link" | "subtle" | "ghost";
    loading?: boolean;
    onClick: (name?: string) => void;
}

export const Button = (props: ButtonProps): JSX.Element => {
    const onClick = () => props.onClick(props.name);

    return <ButtonUI onClick={onClick} appearance={props.type || "primary"} loading={props.loading}>{props.text}</ButtonUI>;
};