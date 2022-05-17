import React from 'react';
import {HashRouter, Routes, Route, Navigate} from "react-router-dom";
import {
    Auth,
    Pages,
    Body,
    AppContext,
    PagesLeftMenu,
    Page,
    PageCreate,
    Templates,
    TemplatesLeftMenu,
    Template,
    TemplateCreate,
    Files,
    File,
    FilesLeftMenu,
    FileUpload,
    FileCreate,
    FileUploadZip,
    ChangePassword,
    AccountLeftMenu,
    Account
} from "./components";

function App() {
    return (
        <Auth>
            <AppContext>
                <HashRouter>
                    <Routes>
                        <Route path="/" element={<Navigate to="/pages"/>}/>
                        <Route path="/account" element={<Body body={<Account/>} leftMenu={<AccountLeftMenu/>}/>}/>
                        <Route path="/account/changePassword" element={<Body body={<ChangePassword/>} leftMenu={<AccountLeftMenu/>}/>}/>
                        <Route path="/pages" element={<Body body={<Pages/>} leftMenu={<PagesLeftMenu/>}/>}/>
                        <Route path="/pages/create" element={<Body body={<PageCreate/>} leftMenu={<PagesLeftMenu/>}/>}/>
                        <Route path="/pages/page/:pageId" element={<Body body={<Page/>} leftMenu={<PagesLeftMenu/>}/>}/>
                        <Route path="/templates" element={<Body body={<Templates/>} leftMenu={<TemplatesLeftMenu/>}/>}/>
                        <Route path="/templates/create" element={<Body body={<TemplateCreate/>} leftMenu={<TemplatesLeftMenu/>}/>}/>
                        <Route path="/templates/template/:templateId" element={<Body body={<Template/>} leftMenu={<TemplatesLeftMenu/>}/>}/>
                        <Route path="/files" element={<Body body={<Files/>} leftMenu={<FilesLeftMenu/>}/>}/>
                        <Route path="/files/create" element={<Body body={<FileCreate/>} leftMenu={<FilesLeftMenu/>}/>}/>
                        <Route path="/files/upload" element={<Body body={<FileUpload/>} leftMenu={<FilesLeftMenu/>}/>}/>
                        <Route path="/files/upload/zip" element={<Body body={<FileUploadZip/>} leftMenu={<FilesLeftMenu/>}/>}/>
                        <Route path="/files/file/:fileId" element={<Body body={<File/>} leftMenu={<FilesLeftMenu/>}/>}/>
                    </Routes>
                </HashRouter>
            </AppContext>
        </Auth>
    );
}

export default App;
