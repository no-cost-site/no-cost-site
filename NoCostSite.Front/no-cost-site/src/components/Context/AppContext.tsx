import React, {PropsWithChildren} from 'react';
import {PageItemDto, TemplateItemDto} from "../../Api/dto";
import {PagesApi, TemplatesApi} from "../../Api";
import { Loader } from '../../controls';

interface IContextReadAll {
    pages?: boolean;
    templates?: boolean;
}

interface IContext {
    pages: PageItemDto[];
    templates: TemplateItemDto[];
    readAll: (update?: IContextReadAll) => Promise<void>;
}

const defaultContext: IContext = {
    pages: [],
    templates: [],
    readAll: async () => {
    },
}

export const Context = React.createContext<IContext>({...defaultContext});

export const AppContext = (props: PropsWithChildren<{}>): JSX.Element => {
    const [state, setState] = React.useState<IContext>(defaultContext);
    const [init, setInit] = React.useState<boolean>(false);

    const readAll = async (update?: IContextReadAll): Promise<void> => {
        if (!update || !!update.pages) {
            const resultPages = await PagesApi.ReadAll();
            setState(x => ({...x, pages: resultPages.Items}));
        }

        if (!update || !!update.templates) {
            const resultTemplates = await TemplatesApi.ReadAll();
            setState(x => ({...x, templates: resultTemplates.Items}));
        }
    }

    React.useEffect(() => {
        readAll().then(() => setInit(true));
    }, []);

    if (!init) {
        return <Loader.CenterBackdrop/>;
    }

    return (
        <Context.Provider value={{...state, readAll}}>
            {props.children}
        </Context.Provider>
    )
}