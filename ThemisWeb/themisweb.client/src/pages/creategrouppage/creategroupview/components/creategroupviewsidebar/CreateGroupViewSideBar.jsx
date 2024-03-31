import React from 'react'
import CGSideBarHeader from './components/CGSideBarHeader/CGSideBarHeader'
import { CGSideBarNav } from './components/CGSideBarNav/CGSideBarNav'
import "../../../../../shared/styles/DashboardContainer.css"

const CreateGroupViewSideBar = () => {
  return (
    <div className="DashboardContainer-SideBar">
        <CGSideBarHeader></CGSideBarHeader>
        <CGSideBarNav></CGSideBarNav>
    </div>
  )
}

export default CreateGroupViewSideBar