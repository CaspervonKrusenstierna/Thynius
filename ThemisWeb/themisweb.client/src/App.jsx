/* eslint-disable react-hooks/rules-of-hooks */

import { BrowserRouter, Routes, Route } from 'react-router-dom';
import React, { createContext, useEffect } from 'react';
import { HomePage, LoginPage, RegisterPage, GroupPage, GroupsViewPage, ProfilePage, CalendarPage, CreateGroupPage, DashboardHomePage } from "./pages";
import "./index.css"
import './App.css';
import useSessionInfo from "./shared/hooks/useSessionInfo"
import TextsViewPage from './pages/textsviewpage/TextsViewPage';

export const sessionInfoContext = createContext();

function App() {
    const sessionInfo = useSessionInfo();

    return (
        <sessionInfoContext.Provider value={sessionInfo}>
            <BrowserRouter>
                <Routes>
                    <Route path="/" element={<HomePage></HomePage>}></Route>
                    <Route path="/register" element={<RegisterPage></RegisterPage>}></Route>
                    <Route path="/login" element={<LoginPage></LoginPage>}></Route>
                    <Route path="/dashboard/home" element={<DashboardHomePage></DashboardHomePage>}></Route>
                    <Route path="/dashboard/calendar" element={<CalendarPage></CalendarPage>}></Route>
                    <Route path="/dashboard/group/:id/:page" element={<GroupPage></GroupPage>}></Route>
                    <Route path="/dashboard/groups" element={<GroupsViewPage></GroupsViewPage>}></Route>
                    <Route path="/dashboard/creategroup" element={<CreateGroupPage></CreateGroupPage>}></Route>
                    <Route path="/dashboard/profile" element={<ProfilePage></ProfilePage>}></Route>
                    <Route path="/dashboard/texts" element={<TextsViewPage></TextsViewPage>}></Route>
                </Routes>
            </BrowserRouter >
        </sessionInfoContext.Provider>
    )
}

export default App;