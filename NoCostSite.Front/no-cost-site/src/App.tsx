import React from 'react';
import {HashRouter, Routes, Route, Navigate} from "react-router-dom";
import {Auth, Pages} from "./components";
import {AppContext} from "./components/Context/AppContext";

function App() {
    return (
        <Auth>
            <AppContext>
                <HashRouter>
                    <Routes>
                        <Route path="/" element={<Navigate to="/pages"/>}/>
                        <Route path="/pages" element={<Pages/>}/>
                    </Routes>
                </HashRouter>
            </AppContext>
        </Auth>
    );
}

export default App;
