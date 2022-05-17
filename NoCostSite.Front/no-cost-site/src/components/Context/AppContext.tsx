import React, {PropsWithChildren} from 'react';
import {DirectoryDto, FileItemDto, PageItemDto, TemplateItemDto} from "../../Api/dto";
import {PagesApi, TemplatesApi, UploadApi} from "../../Api";
import {Loader} from '../../controls';

interface IContextReadAll {
    pages?: boolean;
    templates?: boolean;
    files?: boolean;
}

interface IContext {
    pages: PageItemDto[];
    pagesDirectory: DirectoryDto;
    templates: TemplateItemDto[];
    files: FileItemDto[];
    filesDirectory: DirectoryDto;
    readAll: (update?: IContextReadAll) => Promise<void>;
}

const defaultContext: IContext = {
    pages: [],
    pagesDirectory: {} as any,
    templates: [],
    files: [],
    filesDirectory: {} as any,
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
            setState(x => ({...x, pages: resultPages.Items, pagesDirectory: resultPages.Directory}));
        }

        if (!update || !!update.templates) {
            const resultTemplates = await TemplatesApi.ReadAll();
            setState(x => ({...x, templates: resultTemplates.Items}));
        }

        if (!update || !!update.files) {
            const resultFiles = await UploadApi.ReadAllFiles();
            setState(x => ({...x, files: resultFiles.Files, filesDirectory: resultFiles.Directory}));
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