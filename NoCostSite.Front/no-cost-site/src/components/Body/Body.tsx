import React from 'react';
import {Layout, Show} from '../../controls';
import {TopMenu} from "../TopMenu";
import {LeftNavbar} from "../LeftMenu";

interface BodyProps {
    body?: JSX.Element,
    leftMenu?: JSX.Element,
}

export const Body = (props: BodyProps): JSX.Element => {
    return (
        <Layout>
            <TopMenu/>
            <Layout>
                <LeftNavbar/>
                <Show.OnDesktop>
                    {props.leftMenu}
                </Show.OnDesktop>
                <Layout.Body>
                    {props.body}
                </Layout.Body>
            </Layout>
        </Layout>
    )
}