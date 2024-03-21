/* eslint-disable react-hooks/rules-of-hooks */

import { BrowserRouter, Routes, Route } from 'react-router-dom';
import './App.css';
import { HomePage, LoginPage, RegisterPage } from "./pages";
import React, { createContext, useEffect } from 'react';
import GroupsViewPage from './pages/groupsviewpage/GroupsViewPage';
import GroupPage from './pages/grouppage/GroupPage';
import "./index.css"
import DashboardHome from './pages/dashboardhome/DashboardHome';
import useSessionInfo from "./shared/hooks/useSessionInfo"
import CreateGroupPage from './pages/creategrouppage/CreateGroupPage';
import CalendarPage from './pages/calendarpage/CalendarPage';

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
                    <Route path="/dashboard/home" element={<DashboardHome></DashboardHome>}></Route>
                    <Route path="/dashboard/calendar" element={<CalendarPage></CalendarPage>}></Route>
                    <Route path="/dashboard/group/:id/:page" element={<GroupPage></GroupPage>}></Route>
                    <Route path="/dashboard/groups" element={<GroupsViewPage></GroupsViewPage>}></Route>
                    <Route path="/dashboard/creategroup" element={<CreateGroupPage></CreateGroupPage>}></Route>
                </Routes>
            </BrowserRouter >
        </sessionInfoContext.Provider>
    )
}

export default App;