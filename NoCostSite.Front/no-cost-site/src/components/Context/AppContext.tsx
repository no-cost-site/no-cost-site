import React, {PropsWithChildren} from 'react';
import {
    DirectoryDto,
    FileItemDto,
    PageItemDto,
    SettingsDto,
    TemplateItemDto
} from "../../Api/dto";
import {PagesApi, SettingsApi, TemplatesApi, UploadApi} from "../../Api";
import {Loader} from '../../controls';

interface IContextReadAll {
    pages?: boolean;
    templates?: boolean;
    files?: boolean;
    settings?: boolean;
}

interface IContext {
    pages: PageItemDto[];
    pagesDirectory: DirectoryDto;
    templates: TemplateItemDto[];
    files: FileItemDto[];
    filesDirectory: DirectoryDto;
    settings: SettingsDto;
    readAll: (update?: IContextReadAll) => Promise<void>;
}

const defaultContext: IContext = {
    pages: [],
    pagesDirectory: {} as any,
    templates: [],
    files: [],
    filesDirectory: {} as any,
    settings: {} as any,
    readAll: async () => {
    },
}

export const Context = React.createContext<IContext>({...defaultContext});

export const AppContext = (props: PropsWithChildren<{}>): JSX.Element => {
    const [state, setState] = React.useState<IContext>(defaultContext);
    const [init, setInit] = React.useState<boolean>(false);

    const readAll = async (update?: IContextReadAll): Promise<void> => {
        const [resultPages, resultTemplates, resultFiles, resultSettings] = await Promise.all([
            readPages(update), readTemplates(update), readFiles(update), readSettings(update)
        ]);

        setState(x => ({...x, ...resultPages, ...resultTemplates, ...resultFiles, ...resultSettings}));
    }

    const readPages = async (update?: IContextReadAll): Promise<{ pages: PageItemDto[], pagesDirectory: DirectoryDto } | {}> => {
        if (!update || !!update.pages) {
            const result = await PagesApi.ReadAll();
            return {pages: result.Items, pagesDirectory: result.Directory,}
        }
        return {};
    }

    const readTemplates = async (update?: IContextReadAll): Promise<{ templates: TemplateItemDto[] } | {}> => {
        if (!update || !!update.templates) {
            const result = await TemplatesApi.ReadAll();
            return {templates: result.Items};
        }
        return {};
    }

    const readFiles = async (update?: IContextReadAll): Promise<{files: FileItemDto[], filesDirectory: DirectoryDto} | {}> => {
        if (!update || !!update.files) {
            const result = await UploadApi.ReadAllFiles();
            return {files: result.Files, filesDirectory: result.Directory};
        }
        return {};
    }

    const readSettings = async (update?: IContextReadAll): Promise<{settings: SettingsDto} | {}> => {
        if (!update || !!update.settings) {
            const result = await SettingsApi.Read();
            return {settings: result.Settings};
        }
        return {};
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