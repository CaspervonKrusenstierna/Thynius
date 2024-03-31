import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'

import CreateGroupViewSideBar from './components/creategroupviewsidebar/CreateGroupViewSideBar'
import "../../../shared/styles/DashboardContainer.css"
import CreateGroupSubmitButton from './components/creategroupsubmitbutton/CreateGroupSubmitButton'
import ResolveCreateGroupContent from './hooks/ResolveCreateGroupContent'


const CreateGroupView = (props) => {
  const pageContent = ResolveCreateGroupContent();

  return (
    <div className='DashboardContainer-Container'>
      <div className='DashboardContainer'>
        <CreateGroupViewSideBar></CreateGroupViewSideBar>
        <div className='DashboardContainer-Content'>
          {pageContent}
        </div>
        <CreateGroupSubmitButton></CreateGroupSubmitButton>
      </div>
    </div>
  )
}

export default CreateGroupView