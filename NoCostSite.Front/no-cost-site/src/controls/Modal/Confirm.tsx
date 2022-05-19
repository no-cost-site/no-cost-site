import React, {PropsWithChildren} from "react";
import {Modal as ModalUI} from "rsuite";
import {Button} from "../Button";

export interface ConfirmProps {
    open: boolean;
    title: string;
    onOk: () => void;
    onCancel: () => void;
}

export const Confirm = (props: PropsWithChildren<ConfirmProps>): JSX.Element => {
    return (
        <ModalUI open={props.open} onClose={props.onCancel}>
            <ModalUI.Header>
                <ModalUI.Title>{props.title}</ModalUI.Title>
            </ModalUI.Header>
            <ModalUI.Body>
                {props.children}
            </ModalUI.Body>
            <ModalUI.Footer>
                <Button name="ok" text="Ok" onClick={props.onOk}/>
                <Button name="cancel" text="Cancel" onClick={props.onCancel}/>
            </ModalUI.Footer>
        </ModalUI>
    )
};

export interface ConfirmDeleteProps {
    open: boolean;
    onOk: () => void;
    onCancel: () => void;
}

export const ConfirmDelete = (props: ConfirmDeleteProps): JSX.Element => {
    return (
        <Confirm open={props.open} title="Confirm delete" onOk={props.onOk} onCancel={props.onCancel}>
            Are you sure?
        </Confirm>
    )
}