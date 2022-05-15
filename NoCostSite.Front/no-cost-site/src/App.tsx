import React from 'react';
import {HashRouter, Routes, Route, Navigate} from "react-router-dom";
import {
    Auth,
    Pages,
    Body,
    AppContext,
    PagesLeftMenu,
    Page,
    Templates,
    TemplatesLeftMenu,
    Template,
    Files,
    File,
    FilesLeftMenu
} from "./components";

function App() {
    return (
        <Auth>
            <AppContext>
                <HashRouter>
                    <Routes>
                        <Route path="/" element={<Navigate to="/pages"/>}/>
                        <Route path="/pages" element={<Body body={<Pages/>} leftMenu={<PagesLeftMenu/>}/>}/>
                        <Route path="/pages/page/:pageId" element={<Body body={<Page/>} leftMenu={<PagesLeftMenu/>}/>}/>
                        <Route path="/templates" element={<Body body={<Templates/>} leftMenu={<TemplatesLeftMenu/>}/>}/>
                        <Route path="/templates/template/:templateId" element={<Body body={<Template/>} leftMenu={<TemplatesLeftMenu/>}/>}/>
                        <Route path="/files" element={<Body body={<Files/>} leftMenu={<FilesLeftMenu/>}/>}/>
                        <Route path="/files/file/:fileId" element={<Body body={<File/>} leftMenu={<FilesLeftMenu/>}/>}/>
                    </Routes>
                </HashRouter>
            </AppContext>
        </Auth>
    );
}

export default App;
