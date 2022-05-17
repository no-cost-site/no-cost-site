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

const inputFileStyle = {
    display: "none",
}

export interface UploadProps {
    name: string;
    text: string;
    loading?: boolean;
    onUpload: (value: number[], name?: string) => void;
}

export const Upload = (props: UploadProps): JSX.Element => {
    const inputFile = React.useRef(null);

    const onFileChange = async (event: any) => {
        const file = event.target.files[0];

        const reader = new FileReader();
        reader.onload = function() {
            const uint8Array = new Uint8Array(this.result as any);
            const value = Array.from(uint8Array);
            props.onUpload(value, props.name);
            (inputFile!.current as any).value = null;
        }
        reader.readAsArrayBuffer(file);
    };

    const onClick = () => {
        (inputFile!.current as any).click();
    }

    return (
        <>
            <Button name={props.name} text={props.text} loading={props.loading} onClick={onClick}/>
            <input ref={inputFile} type="file" onChange={onFileChange} style={inputFileStyle}/>
        </>
    );
}