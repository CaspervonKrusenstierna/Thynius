import React from 'react'
import GroupSidebarNavigation from './GroupSidebarNavigation/GroupSidebarNavigation'
import "./GroupSideBar.css"

const GroupSideBar = (props) => {
  return (
    <div className='GroupSideBar'>
        <div className='AlignerDiv'>
          <p className='GroupSideBar-Title'>{props.GroupName}</p>
        </div>
        <GroupSidebarNavigation groupId={props.groupId}></GroupSidebarNavigation>
    </div>
  )
}

export default GroupSideBar