import React from "react";
import {Input as InputUI} from "rsuite";

export interface InputProps {
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
