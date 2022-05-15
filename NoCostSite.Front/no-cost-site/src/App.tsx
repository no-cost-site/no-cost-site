import React from 'react';
import {HashRouter, Routes, Route, Navigate} from "react-router-dom";
import {Auth, Pages, Body, AppContext, PagesLeftMenu, Page, Templates, TemplatesLeftMenu, Template} from "./components";

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
                    </Routes>
                </HashRouter>
            </AppContext>
        </Auth>
    );
}

export default App;
