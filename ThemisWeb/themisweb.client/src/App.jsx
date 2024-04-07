/* eslint-disable react-hooks/rules-of-hooks */

import { BrowserRouter, Routes, Route } from 'react-router-dom';
import React, { createContext } from 'react';
import { HomePage, LoginPage, RegisterPage, GroupPage, GroupsViewPage,CreateGroupPage, DashboardHomePage, TextsViewPage, TextViewPage, AssignmentPage, SubmitTextPage, SubmittmentPage } from "./pages";

import "./index.css"
import "./output.css"

import useSessionInfo from "./shared/hooks/useSessionInfo"
import EditGroupPage from './pages/editgrouppage/EditGroupPage';
export const sessionInfoContext = createContext();

function App() {
    const [sessionInfo, updateSessionInfo] = useSessionInfo();

    return (
        <sessionInfoContext.Provider value={{sessionInfo: sessionInfo, updateSessionInfo: updateSessionInfo}}>
            <BrowserRouter>
                <Routes>
                        <Route path="/" element={<HomePage></HomePage>}></Route>
                        <Route path="/register" element={<RegisterPage></RegisterPage>}></Route>
                        <Route path="/login" element={<LoginPage></LoginPage>}></Route>
                        <Route path="/dashboard/home" element={<DashboardHomePage></DashboardHomePage>}></Route>
                        <Route path="/dashboard/group/:id/:page" element={<GroupPage></GroupPage>}></Route>
                        <Route path="/dashboard/group/:id/:page/:assignmentid" element={<GroupPage></GroupPage>}></Route>
                        <Route path="/dashboard/groups" element={<GroupsViewPage></GroupsViewPage>}></Route>
                        <Route path="/dashboard/creategroup/:page" element={<CreateGroupPage></CreateGroupPage>}></Route>
                        <Route path="/dashboard/texts" element={<TextsViewPage></TextsViewPage>}></Route>
                        <Route path="/dashboard/text/:id" element={<TextViewPage></TextViewPage>}></Route>
                        <Route path='/dashboard/assignment/:id/:page' element={<AssignmentPage></AssignmentPage>}></Route>
                        <Route path='/dashboard/text/:id/submit' element={<SubmitTextPage></SubmitTextPage>}></Route>
                        <Route path='/dashboard/submittment/:id' element={<SubmittmentPage></SubmittmentPage>}></Route>
                        <Route path='/dashboard/editgroup/:id/:page' element={<EditGroupPage></EditGroupPage>}></Route>
                </Routes>
            </BrowserRouter >
        </sessionInfoContext.Provider>
    )
}

export default App;