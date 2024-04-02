import React, { createContext, useRef } from 'react'
import CreateGroupViewSideBar from './components/creategroupviewsidebar/CreateGroupViewSideBar'
import "../../../shared/styles/DashboardContainer.css"
import CreateGroupSubmitButton from './components/creategroupsubmitbutton/CreateGroupSubmitButton'
import ResolveCreateGroupContent from './hooks/ResolveCreateGroupContent'

export const createGroupContext = createContext();

const CreateGroupView = (props) => {
  const groupData = useRef({groupMembers: [], currentMembersPage: 1, groupName: "", groupImg: null})
  const pageContent = ResolveCreateGroupContent();
  
  return (
    <createGroupContext.Provider value={groupData}>
      <div className='DashboardContainer-Container'>
        <div className='DashboardContainer'>
          <CreateGroupViewSideBar></CreateGroupViewSideBar>
          <div className='DashboardContainer-Content'>
            {pageContent}
          </div>
          <CreateGroupSubmitButton></CreateGroupSubmitButton>
        </div>
      </div>
    </createGroupContext.Provider>
  )
}

export default CreateGroupView