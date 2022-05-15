import React from "react";
import {SelectPicker} from "rsuite";

export interface SelectProps {
    name: string,
    value: string,
    values: SelectValue[],
    placeholder?: string,
    onChange: (value: string, name?: string) => void,
}

interface SelectValue {
    text: string,
    value: string,
}

export const Select = (props: SelectProps): JSX.Element => {
    const onChange = (value: string) => {
        props.onChange(value, props.name);
    }

    const values = props.values.map(x => ({
        value: x.value,
        label: x.text,
    }))

    return (
        <SelectPicker<string>
            value={props.value}
            placeholder={props.placeholder}
            data={values}
            block
            onChange={onChange}
        />
    );
};
