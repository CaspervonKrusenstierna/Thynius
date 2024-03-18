/* eslint-disable react-hooks/rules-of-hooks */

import { BrowserRouter, Routes, Route } from 'react-router-dom';
import './App.css';
import { HomePage, LoginPage, RegisterPage } from "./pages";
import useSessionInfo from './shared/hooks/useSessionInfo';
import React, { createContext, useEffect } from 'react';
import GroupsViewPage from './pages/groupsviewpage/GroupsViewPage';

export const sessionInfoContext = createContext();

function App() {
    let sessionInfo = useSessionInfo();

    return (
        <sessionInfoContext.Provider value={sessionInfo}>
            <BrowserRouter >
                <Routes>
                    <Route path="/" element={<HomePage></HomePage>}></Route>
                    <Route path="/register" element={<RegisterPage></RegisterPage>}></Route>
                    <Route path="/login" element={<LoginPage></LoginPage>}></Route>
                    <Route path="/group/:id"></Route>
                    <Route path="/groupsview" element={<GroupsViewPage></GroupsViewPage>}></Route>
                </Routes>
            </BrowserRouter >
        </sessionInfoContext.Provider>
    )
}

export default App;