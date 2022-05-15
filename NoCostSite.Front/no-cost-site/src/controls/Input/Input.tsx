import React from "react";
import {Input as InputUI} from "rsuite";
import {HtmlProps} from "../../utils";

interface InputProps {
    name: string,
    value: string,
    placeholder?: string,
    onChange: (value: string, name?: string) => void,
}

export const Input = (props: InputProps): JSX.Element => {
    const onChange = (value: string) => {
        props.onChange(value, props.name);
    }

    return (
        <InputUI
            name={props.name}
            value={props.value}
            placeholder={props.placeholder}
            onChange={onChange}
        />
    );
};

export const Password = (props: InputProps): JSX.Element => {
    const onChange = (value: string) => {
        props.onChange(value, props.name);
    }

    return (
        <InputUI
            type="password"
            value={props.value}
            placeholder={props.placeholder}
            onChange={onChange}
        />
    );
};

export const TextArea = (props: InputProps & HtmlProps): JSX.Element => {
    const onChange = (value: string) => {
        props.onChange(value, props.name);
    }

    return (
        <InputUI
            style={{...props.style}}
            name={props.name}
            as="textarea"
            rows={30}
            value={props.value}
            placeholder={props.placeholder}
            onChange={onChange}
        />
    );
};
