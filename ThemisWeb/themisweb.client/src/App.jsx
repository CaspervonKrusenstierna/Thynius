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
const OrganizationsPage = lazy(() => import("./pages/organizationspage/OrganizationsPage"))
const EditGroupPage = lazy(() => import('./pages/editgrouppage/EditGroupPage'))
const TeachersPage = lazy(() => import('./pages/teacherspage/TeachersPage'))
const PrivacyPolicyPage = lazy(() => import("./pages/privacypolicypage/PrivacyPolicyPage"))
const CookiesPage = lazy(() => import("./pages/cookiespage/CookiesPage"))
const ContactPage = lazy(() => import("./pages/contactpage/ContactPage"))
const AboutPage = lazy(() => import("./pages/aboutpage/AboutPage"))

import "./index.css"
import "./output.css"

import useSessionInfo from "./shared/hooks/useSessionInfo"
import LoginBox from './pages/homepage/loginbox/LoginBox';
import RegisterBox from './pages/homepage/registerbox/RegisterBox';
import { DarknessEffect } from './shared/components/homepage';
export const sessionInfoContext = createContext();

function App() {
    const [loading, sessionInfo, updateSessionInfo] = useSessionInfo();
    if(loading){
        return <></>
    }
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
                        <Route path='/dashboard/organizations' element={<Suspense fallback={<></>}><OrganizationsPage/></Suspense>}></Route>
                        <Route path='/dashboard/teachers' element={<Suspense fallback={<></>}><TeachersPage/></Suspense>}></Route>
                        <Route path='/contact' element={<Suspense fallback={<></>}><ContactPage/></Suspense>}></Route>
                        <Route path='/privacy-policy' element={<Suspense fallback={<></>}><PrivacyPolicyPage/></Suspense>}></Route>
                        <Route path='/cookie-policy' element={<Suspense fallback={<></>}><CookiesPage/></Suspense>}></Route>
                        <Route path='/about' element={<Suspense fallback={<></>}><AboutPage/></Suspense>}></Route>
                </Routes>
            </BrowserRouter>
        </sessionInfoContext.Provider>
    )
}

export default App;