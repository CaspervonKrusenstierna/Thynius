/* eslint-disable react-hooks/rules-of-hooks */

import { BrowserRouter, Routes, Route } from 'react-router-dom';
import React, { Suspense, createContext, lazy } from 'react';
const HomePage = lazy(() => import("./pages/homepage"))
const GroupPage = lazy(() => import("./pages/grouppage"))
const GroupsViewPage = lazy(() => import("./pages/groupsviewpage"))
const CreateGroupPage = lazy(() => import("./pages/creategrouppage"))
const DashboardHomePage = lazy(() => import("./pages/dashboardhome"))
const TextsViewPage = lazy(() => import("./pages/textsviewpage"))
const TextViewPage = lazy(() => import("./pages/textviewpage"))
const AssignmentPage = lazy(() => import("./pages/assignmentpage"))
const SubmitTextPage = lazy(() => import("./pages/submittextpage"))
const SubmittmentPage = lazy(() => import("./pages/submittmentpage"))
import "./index.css"
import "./output.css"

import useSessionInfo from "./shared/hooks/useSessionInfo"
import EditGroupPage from './pages/editgrouppage/EditGroupPage';
import LoginBox from './pages/homepage/loginbox/LoginBox';
import RegisterBox from './pages/homepage/registerbox/RegisterBox';
export const sessionInfoContext = createContext();

function App() {
    const [sessionInfo, updateSessionInfo] = useSessionInfo();

    return (
        <sessionInfoContext.Provider value={{sessionInfo: sessionInfo, updateSessionInfo: updateSessionInfo}}>
            <BrowserRouter>
                <Routes>
                        <Route path="home" element={<HomePage></HomePage>}>
                            <Route path="/register" element={<RegisterBox></RegisterBox>}></Route>
                            <Route path="/login" element={<LoginBox></LoginBox>}></Route>
                        </Route>
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
            </BrowserRouter>
        </sessionInfoContext.Provider>
    )
}

export default App;