import React from 'react';
import {HashRouter, Routes, Route, Navigate} from "react-router-dom";
import {Auth, Pages, Body, AppContext, PagesLeftMenu, Page} from "./components";

function App() {
    return (
        <Auth>
            <AppContext>
                <HashRouter>
                    <Routes>
                        <Route path="/" element={<Navigate to="/pages"/>}/>
                        <Route path="/pages" element={<Body body={<Pages/>} leftMenu={<PagesLeftMenu/>}/>}/>
                        <Route path="/pages/page/:pageId" element={<Body body={<Page/>} leftMenu={<PagesLeftMenu/>}/>}/>
                    </Routes>
                </HashRouter>
            </AppContext>
        </Auth>
    );
}

export default App;
