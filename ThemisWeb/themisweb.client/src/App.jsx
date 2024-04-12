/* eslint-disable react-hooks/rules-of-hooks */

import { BrowserRouter, Routes, Route } from 'react-router-dom';
import React, { Suspense, createContext, lazy } from 'react';
const HomePage = lazy(() => import("./pages/homepage/HomePage"))
const GroupPage = lazy(() => import("./pages/grouppage/GroupPage"))
const GroupsViewPage = lazy(() => import("./pages/groupsviewpage/GroupsViewPage"))
const CreateGroupPage = lazy(() => import("./pages/creategrouppage/CreateGroupPage"))
const DashboardHomePage = lazy(() => import("./pages/dashboardhome/DashboardHome"))
const TextsViewPage = lazy(() => import("./pages/textsviewpage/TextsViewPage"))
const TextViewPage = lazy(() => import("./pages/textviewpage/TextViewPage"))
const AssignmentPage = lazy(() => import("./pages/assignmentpage/AssignmentPage"))
const SubmitTextPage = lazy(() => import("./pages/submittextpage/SubmitTextPage"))
const SubmittmentPage = lazy(() => import("./pages/submittmentpage/SubmittmentPage"))
import "./index.css"
import "./output.css"

import useSessionInfo from "./shared/hooks/useSessionInfo"
import EditGroupPage from './pages/editgrouppage/EditGroupPage';
import LoginBox from './pages/homepage/loginbox/LoginBox';
import RegisterBox from './pages/homepage/registerbox/RegisterBox';
import { DarknessEffect } from './shared/components/homepage';
export const sessionInfoContext = createContext();

function App() {
    const [sessionInfo, updateSessionInfo] = useSessionInfo();

    return (
        <sessionInfoContext.Provider value={{sessionInfo: sessionInfo, updateSessionInfo: updateSessionInfo}}>
            <BrowserRouter>
                <Routes>
                        <Route path="/" element={<HomePage/>}>
                            <Route path="/register" element={<><DarknessEffect/><RegisterBox/></>}></Route>
                            <Route path="/login" element={<><DarknessEffect/><LoginBox/></>}></Route>
                        </Route>
                        
                        <Route path="/dashboard/home" element={<Suspense fallback={<></>}><DashboardHomePage/></Suspense>}></Route>
                        <Route path="/dashboard/group/:id/:page" element={<Suspense fallback={<></>}><GroupPage/></Suspense>}></Route>
                        <Route path="/dashboard/group/:id/:page/:assignmentid" element={<Suspense fallback={<></>}><GroupPage/></Suspense>}></Route>
                        <Route path="/dashboard/groups" element={<Suspense fallback={<></>}><GroupsViewPage/></Suspense>}></Route>
                        <Route path="/dashboard/creategroup/:page" element={<Suspense fallback={<></>}><CreateGroupPage/></Suspense>}></Route>
                        <Route path="/dashboard/texts" element={<Suspense fallback={<></>}><TextsViewPage/></Suspense>}></Route>
                        <Route path="/dashboard/text/:id" element={<Suspense fallback={<></>}><TextViewPage/></Suspense>}></Route>
                        <Route path='/dashboard/assignment/:id/:page' element={<Suspense fallback={<></>}><AssignmentPage/></Suspense>}></Route>
                        <Route path='/dashboard/text/:id/submit' element={<Suspense fallback={<></>}><SubmitTextPage/></Suspense>}></Route>
                        <Route path='/dashboard/submittment/:id' element={<Suspense fallback={<></>}><SubmittmentPage/></Suspense>}></Route>
                        <Route path='/dashboard/editgroup/:id/:page' element={<Suspense fallback={<></>}><EditGroupPage/></Suspense>}></Route>
                </Routes>
            </BrowserRouter>
        </sessionInfoContext.Provider>
    )
}

export default App;