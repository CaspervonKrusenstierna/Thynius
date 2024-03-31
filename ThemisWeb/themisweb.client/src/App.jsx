/* eslint-disable react-hooks/rules-of-hooks */

import { BrowserRouter, Routes, Route } from 'react-router-dom';
import React, { createContext } from 'react';
import { HomePage, LoginPage, RegisterPage, GroupPage, GroupsViewPage, ProfilePage, CreateGroupPage, DashboardHomePage, TextsViewPage, TextViewPage, AssignmentPage, SubmitTextPage } from "./pages";

import "./index.css"
import "./output.css"

import useSessionInfo from "./shared/hooks/useSessionInfo"

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
                    <Route path="/dashboard/group/:id/:page" element={<GroupPage></GroupPage>}></Route>
                    <Route path="/dashboard/groups" element={<GroupsViewPage></GroupsViewPage>}></Route>
                    <Route path="/dashboard/creategroup/:page" element={<CreateGroupPage></CreateGroupPage>}></Route>
                    <Route path="/dashboard/profile" element={<ProfilePage></ProfilePage>}></Route>
                    <Route path="/dashboard/texts" element={<TextsViewPage></TextsViewPage>}></Route>
                    <Route path="/dashboard/text/:id" element={<TextViewPage></TextViewPage>}></Route>
                    <Route path='/dashboard/assignment/:id/:page' element={<AssignmentPage></AssignmentPage>}></Route>
                    <Route path='/dashboard/text/:id/submit' element={<SubmitTextPage></SubmitTextPage>}></Route>
                </Routes>
            </BrowserRouter >
        </sessionInfoContext.Provider>
    )
}

export default App;