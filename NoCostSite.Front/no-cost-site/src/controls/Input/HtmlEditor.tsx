import React from "react";
import {TextArea} from "./Input";

interface HtmlEditorProps {
    name: string,
    value: string,
    onChange: (value: string, name?: string) => void,
}

export const HtmlEditor = (props: HtmlEditorProps): JSX.Element => {
    const onChange = (value: string) => {
        props.onChange(value, props.name);
    }

    return (
        <TextArea
            name={props.name}
            value={props.value}
            onChange={onChange}
        />
    );
};